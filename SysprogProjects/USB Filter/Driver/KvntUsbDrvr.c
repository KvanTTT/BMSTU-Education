#include "KvntUsbDrvr.h"
#include "Strings.h"

#ifdef ALLOC_PRAGMA
#pragma alloc_text(INIT, DriverEntry)
#pragma alloc_text(PAGE, AddDevice)
#pragma alloc_text(PAGE, DriverUnload)
#endif

NTSTATUS DriverEntry(IN PDRIVER_OBJECT DriverObject, IN PUNICODE_STRING RegistryPath)
{
    ULONG i;

    UNREFERENCED_PARAMETER(RegistryPath);

    for (i = 0; i <= IRP_MJ_MAXIMUM_FUNCTION; i++)
      DriverObject->MajorFunction[i] = DispatchRoutine;

	DriverObject->MajorFunction[IRP_MJ_INTERNAL_DEVICE_CONTROL] = DispatchInternalDeviceControl;

    DriverObject->DriverExtension->AddDevice = AddDevice;
    DriverObject->DriverUnload				 = DriverUnload;

    return STATUS_SUCCESS;
}

VOID DriverUnload(IN PDRIVER_OBJECT DriverObject){}

NTSTATUS AddDevice(IN PDRIVER_OBJECT DrvObj, IN PDEVICE_OBJECT DevObj)
{
    NTSTATUS       status;
    PDEVICE_OBJECT filterDevObj = NULL;
	UNICODE_STRING DeviceName;

	RtlInitUnicodeString(&DeviceName, DEVICE_NAME);

	status = IoCreateDevice(DrvObj, sizeof(DEVICE_EXTENSION), &DeviceName, FILE_DEVICE_UNKNOWN,0, FALSE, &filterDevObj);
    
	if (NT_SUCCESS(status))
	{
        PDEVICE_EXTENSION DeviceExtension;

        UNICODE_STRING SubKeyName;
		UNICODE_STRING MaxLogSizeParam;
		UNICODE_STRING LogFileNameParam;
	    HANDLE		   hKeyMain;

        DeviceExtension = (PDEVICE_EXTENSION)filterDevObj->DeviceExtension;
        RtlZeroMemory(DeviceExtension, sizeof(DEVICE_EXTENSION));

		  // создаем символьную ссылку для основого устройства фильтра
		RtlInitUnicodeString(&DeviceExtension->SymLink, SYM_LINK);
		status = IoCreateSymbolicLink(&DeviceExtension->SymLink, &DeviceName);

		  // заполняем поля расширения
        DeviceExtension->state              = STATE_INITIALIZED;
        DeviceExtension->filterDevObj       = filterDevObj;
        DeviceExtension->DevObj             = DevObj;        
        DeviceExtension->pendingActionCount = 0;

		  // инициализируем объект события	
        KeInitializeEvent(&DeviceExtension->removeEvent, NotificationEvent, FALSE);

		  // присоединяем наш FDO к стеку устройств
        DeviceExtension->topDevObj = IoAttachDeviceToDeviceStack(filterDevObj, DevObj);

		  // мы не знаем, что за устройство находится под нашим фильтром, поэтому копируем его флаги
        filterDevObj->Flags |= (DeviceExtension->topDevObj->Flags & (DO_BUFFERED_IO | DO_DIRECT_IO));
        filterDevObj->Flags |= (DeviceExtension->topDevObj->Flags & (DO_POWER_INRUSH | DO_POWER_PAGABLE)); 
        filterDevObj->Flags &= ~DO_DEVICE_INITIALIZING;

		DeviceExtension->PreparedToLog = TRUE;

		  // открываем ключ настроек нашего фильтра, расположенный внутри ключа устройства
		RtlInitUnicodeString(&SubKeyName, L"KvntUsbDrvrParams");
        status = RegOpenDeviceSubKey(DevObj, &SubKeyName, &hKeyMain);
		if (NT_SUCCESS(status))
		{
			  // задаем имена параметров для считывания из реестра
			RtlInitUnicodeString(&MaxLogSizeParam,  L"MaxLogSize" );
			RtlInitUnicodeString(&LogFileNameParam, L"LogFileName");

			  // считываем максимальную длину лог-файла и его имя
			if (NT_SUCCESS(RegReadDword(hKeyMain, &MaxLogSizeParam, &DeviceExtension->MaxLogSize)) &&
				NT_SUCCESS(RegReadString(hKeyMain, &LogFileNameParam, &DeviceExtension->LogFileName)))
			{
				ZwClose(hKeyMain);

				  // создаем лог-файл
				status = CreateLogFile(&DeviceExtension->LogFileName, &DeviceExtension->LogFileHandle);
				if (NT_SUCCESS(status))
				{
					  // создаем буфер для URB-пакетов
					if (DeviceExtension->MaxLogSize > 1000)
					  status = InitBuffer(DeviceExtension->MaxLogSize, &DeviceExtension->UrbPackets);
					else status = STATUS_DEVICE_DATA_ERROR;

					  // подготовка завершилась неудачей
					if (!NT_SUCCESS(status))
					{
						WriteBufferToLogFile(STR_ERROR_BUFFER_ALLOCATION_FAILED,
									   sizeof(STR_ERROR_BUFFER_ALLOCATION_FAILED) - 1,
									   DeviceExtension->LogFileHandle);

						CloseLogFile(DeviceExtension->LogFileHandle);
						DeviceExtension->PreparedToLog = FALSE;

						return STATUS_SUCCESS;
					}
				}
				else
				{
					DeviceExtension->PreparedToLog = FALSE;

					return STATUS_SUCCESS;
				}
			}
			else
			{
				DeviceExtension->PreparedToLog = FALSE;

				ZwClose(hKeyMain);

				return STATUS_SUCCESS;
			}
		}
		else
		{
			DeviceExtension->PreparedToLog = FALSE;

			return STATUS_SUCCESS;
		}
	}
	else return status;

	return status;
}

  // основная процедура обработки IRP-пакетов
NTSTATUS DispatchRoutine(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp)
{
    NTSTATUS status;

	PIO_STACK_LOCATION IrpSp;

    BOOLEAN passIrpDown = TRUE;
    UCHAR   majorFunc, minorFunc;

	PDEVICE_EXTENSION DeviceExtension = (PDEVICE_EXTENSION)DeviceObject->DeviceExtension;

	  // получаем указатель на ячейку стека IRP-пакета, предназначенную для нашего драйвера
    IrpSp = IoGetCurrentIrpStackLocation(Irp);

	  // извлекаем главный и вспомогательный коды пакета
    majorFunc = IrpSp->MajorFunction;
    minorFunc = IrpSp->MinorFunction;

	  // увеличиваем счетчик обрабатываемых пакетов
    if (!((majorFunc == IRP_MJ_PNP) && (minorFunc == IRP_MN_REMOVE_DEVICE)))
      IncrementPendingActionCount(DeviceExtension);

    if ((majorFunc != IRP_MJ_PNP) && (majorFunc != IRP_MJ_CLOSE) &&
	   ((DeviceExtension->state == STATE_REMOVING) || (DeviceExtension->state == STATE_REMOVED)))
	  {
		  // пока устройство удаляется, мы пропускаем только IRP_MJ_PNP и IRP_MJ_CLOSE
        status = Irp->IoStatus.Status = STATUS_DELETE_PENDING;
        IoCompleteRequest(Irp, IO_NO_INCREMENT);
        passIrpDown = FALSE;
      }
      else
	    {
			switch (majorFunc)
			  {
				case IRP_MJ_PNP: status = PnP_Dispatch(DeviceExtension, Irp);
								 passIrpDown = FALSE;
								 break;
			      // все не поддерживаемые IRP просто пересылаем вниз по стеку
				default: break;
			  }
		}

	  // пересылка IRP следующему драйверу в стеке и вызов самого драйвера для обработки пакета	
    if (passIrpDown)
	  {
		IoCopyCurrentIrpStackLocationToNext(Irp);
		status = IoCallDriver(DeviceExtension->topDevObj, Irp);
	  }

	  // уменьшаем счетчик обрабатываемых пакетов, при тех же условиях при которых происходило увеличение
    if (!((majorFunc == IRP_MJ_PNP) && (minorFunc == IRP_MN_REMOVE_DEVICE)))
	  DecrementPendingActionCount(DeviceExtension);

    return status;
}

  // callback функция, позволяющая перехватывать уже обработанные запросы
NTSTATUS CompleteRoutine(IN PDEVICE_OBJECT DeviceObject, IN	PIRP Irp, IN PVOID Context)
{
	PDEVICE_EXTENSION DeviceExtension = (PDEVICE_EXTENSION)DeviceObject->DeviceExtension;
	PIO_STACK_LOCATION IrpSp = IoGetCurrentIrpStackLocation(Irp);

	InterlockedIncrement(&DeviceExtension->UrbCount);	

	  // переносим информацию о запросе в буфер
	DeviceExtension->Urb = IrpSp->Parameters.Others.Argument1;
	WriteUrbPacketToBuffer(DeviceExtension);

	return STATUS_SUCCESS;
}

NTSTATUS DispatchInternalDeviceControl(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp)
{
	PDEVICE_EXTENSION DeviceExtension = (PDEVICE_EXTENSION)DeviceObject->DeviceExtension;
	PIO_STACK_LOCATION IrpSp = IoGetCurrentIrpStackLocation(Irp);

	  // будем обрабатывать только пакеты с IOCTL_INTERNAL_USB_SUBMIT_URB, остальные просто пересылаем
	  // кроме того, проверяем, успешно ли прошла подготовка к протоколированию
	if ((IrpSp->Parameters.DeviceIoControl.IoControlCode != IOCTL_INTERNAL_USB_SUBMIT_URB) ||
		(!DeviceExtension->PreparedToLog))
	{
		IoSkipCurrentIrpStackLocation(Irp);
		return IoCallDriver(DeviceExtension->topDevObj, Irp);
	}

	IoCopyCurrentIrpStackLocationToNext(Irp);
    IoSetCompletionRoutine(Irp, (PIO_COMPLETION_ROUTINE)CompleteRoutine, NULL, TRUE, TRUE, TRUE);

	return IoCallDriver(DeviceExtension->topDevObj, Irp);
}

VOID WriteUrbPacketToBuffer(IN OUT PDEVICE_EXTENSION DeviceExtension)
{
	WriteNumber(&DeviceExtension->UrbPackets,
				 DeviceExtension->UrbCount,
				 10,
				 FALSE);

	switch (DeviceExtension->Urb->UrbHeader.Function)
	{
	case URB_FUNCTION_SELECT_INTERFACE:
		{
			UCHAR i;

			  // вывод заголовка функции
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_SELECT_INTERFACE,
						 sizeof(FUNC_SELECT_INTERFACE) - 1);

			  // вывод handle'а конфигурации
			WriteString(&DeviceExtension->UrbPackets,
						 STR_CONFIG_HANDLE,
						 sizeof(STR_CONFIG_HANDLE));
			WriteNumber(&DeviceExtension->UrbPackets,
						 (ULONG)DeviceExtension->Urb->UrbSelectInterface.ConfigurationHandle,
						 16,
						 TRUE);

			  // вывод информации об интерфейсе
			WriteInterfaceInfo(DeviceExtension, &DeviceExtension->Urb->UrbSelectInterface.Interface);

			  // вывод информации о конечных точках
			for (i = 0; i < DeviceExtension->Urb->UrbSelectInterface.Interface.NumberOfPipes; i++)
			  WritePipeInfo(DeviceExtension, DeviceExtension->Urb->UrbSelectInterface.Interface.Pipes[i]);

			break;
		}
	case URB_FUNCTION_SELECT_CONFIGURATION:
		{
			UCHAR i;

			  // вывод заголовка функции
			WriteString(&DeviceExtension->UrbPackets, FUNC_SELECT_CONFIGURATION, sizeof(FUNC_SELECT_CONFIGURATION) - 1);

			  // вывод информации о дескрипторе конфигурации
			WriteConfigDescriptorInfo(DeviceExtension, DeviceExtension->Urb->UrbSelectConfiguration.ConfigurationDescriptor);

			  // вывод handle'а конфигурации
			WriteString(&DeviceExtension->UrbPackets, STR_CONFIG_HANDLE, sizeof(STR_CONFIG_HANDLE));
			WriteNumber(&DeviceExtension->UrbPackets, (ULONG)DeviceExtension->Urb->UrbSelectConfiguration.ConfigurationHandle,
						 16, TRUE);

			  // вывод информации об интерфейсе
			WriteInterfaceInfo(DeviceExtension, &DeviceExtension->Urb->UrbSelectConfiguration.Interface);

			  // вывод информации о конечных точках
			for (i = 0; i < DeviceExtension->Urb->UrbSelectConfiguration.Interface.NumberOfPipes; i++)
			  WritePipeInfo(DeviceExtension, DeviceExtension->Urb->UrbSelectConfiguration.Interface.Pipes[i]);

			break;
		}
	case URB_FUNCTION_RESET_PIPE:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_RESET_PIPE, sizeof(FUNC_RESET_PIPE) - 1);
			WriteString(&DeviceExtension->UrbPackets, STR_PIPE_HANDLE, sizeof(STR_PIPE_HANDLE));
			WriteNumber(&DeviceExtension->UrbPackets, (ULONG)DeviceExtension->Urb->UrbPipeRequest.PipeHandle, 16, TRUE);
			break;
		}
	case URB_FUNCTION_ABORT_PIPE:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_ABORT_PIPE, sizeof(FUNC_ABORT_PIPE) - 1);
			WriteString(&DeviceExtension->UrbPackets, STR_PIPE_HANDLE, sizeof(STR_PIPE_HANDLE));
			WriteNumber(&DeviceExtension->UrbPackets, (ULONG)DeviceExtension->Urb->UrbPipeRequest.PipeHandle, 16, TRUE);
			break;
		}
	case URB_FUNCTION_GET_FRAME_LENGTH:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_GET_FRAME_LENGTH, sizeof(FUNC_GET_FRAME_LENGTH) - 1);
			break;
		}
	case URB_FUNCTION_SET_FRAME_LENGTH:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_SET_FRAME_LENGTH, sizeof(FUNC_SET_FRAME_LENGTH) - 1);
			break;
		}
	case URB_FUNCTION_GET_CURRENT_FRAME_NUMBER:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_GET_CURRENT_FRAME_NUMBER, sizeof(FUNC_GET_CURRENT_FRAME_NUMBER) - 1);
			break;
		}
	case URB_FUNCTION_CONTROL_TRANSFER:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_CONTROL_TRANSFER, sizeof(FUNC_CONTROL_TRANSFER) - 1);
			break;
		}
	case URB_FUNCTION_BULK_OR_INTERRUPT_TRANSFER:
		{
			  // вывод заголовка функции
			WriteString(&DeviceExtension->UrbPackets, FUNC_BULK_OR_INTERRUPT_TRANSFER, sizeof(FUNC_BULK_OR_INTERRUPT_TRANSFER) - 1);

			  // вывод handle'a канала
			WriteString(&DeviceExtension->UrbPackets,
						 STR_BoI_TRANSFER_PIPE_HANDLE,
						 sizeof(STR_BoI_TRANSFER_PIPE_HANDLE));
			WriteNumber(&DeviceExtension->UrbPackets,
						 (ULONG)DeviceExtension->Urb->UrbBulkOrInterruptTransfer.PipeHandle, 16, FALSE);

			  // вывод флагов передачи
			WriteString(&DeviceExtension->UrbPackets, STR_TRANSFER_FLAGS, sizeof(STR_TRANSFER_FLAGS));
			WriteNumber(&DeviceExtension->UrbPackets, DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferFlags,
						 10, FALSE);

			  // вывод длины буфера
			WriteString(&DeviceExtension->UrbPackets,
						 STR_BUFFER_LENGTH,
						 sizeof(STR_BUFFER_LENGTH));
			WriteNumber(&DeviceExtension->UrbPackets,
						 DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBufferLength, 10, FALSE);

			  // вывод буфера
			if (DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBuffer)
			{
				WriteString(&DeviceExtension->UrbPackets, STR_GENERIC_BUFFER, sizeof(STR_GENERIC_BUFFER) - 1);

				WriteString(&DeviceExtension->UrbPackets, DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBuffer,
							 DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBufferLength);
			}
			else
			if (DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBufferMDL)
			{
				PVOID Data = MmGetSystemAddressForMdl(DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBufferMDL);

				WriteString(&DeviceExtension->UrbPackets, STR_MDL_BUFFER, sizeof(STR_MDL_BUFFER) - 1);

				WriteString(&DeviceExtension->UrbPackets, Data,
							 DeviceExtension->Urb->UrbBulkOrInterruptTransfer.TransferBufferLength);
			}

			WriteString(&DeviceExtension->UrbPackets,
						 NEW_LINE,
						 sizeof(NEW_LINE) - 1);
			break;
		}
	case URB_FUNCTION_ISOCH_TRANSFER:
		{
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_ISOCH_TRANSFER,
						 sizeof(FUNC_ISOCH_TRANSFER) - 1);
			break;
		}
	case URB_FUNCTION_GET_DESCRIPTOR_FROM_DEVICE:
		{
			UCHAR DescriptorType = DeviceExtension->Urb->UrbControlDescriptorRequest.DescriptorType;
			PVOID Buffer         = DeviceExtension->Urb->UrbControlDescriptorRequest.TransferBuffer;
			PMDL  BufferMDL      = DeviceExtension->Urb->UrbControlDescriptorRequest.TransferBufferMDL;

			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_GET_DESC_FROM_DEVICE,
						 sizeof(FUNC_GET_DESC_FROM_DEVICE) - 1);

			switch (DescriptorType)
			{
			case USB_DEVICE_DESCRIPTOR_TYPE:
				{					
					if (Buffer)
					{
						WriteDeviceDescriptorInfo(DeviceExtension,
												  (PUSB_DEVICE_DESCRIPTOR)Buffer);
					}
					else
					if (BufferMDL)
					{
						WriteDeviceDescriptorInfo(DeviceExtension,
												  (PUSB_DEVICE_DESCRIPTOR)BufferMDL);
					}

					break;
				}
			case USB_CONFIGURATION_DESCRIPTOR_TYPE:
				{
					if (Buffer)
					{
						WriteConfigDescriptorInfo(DeviceExtension,
												  (PUSB_CONFIGURATION_DESCRIPTOR)Buffer);
					}
					else
					if (BufferMDL)
					{
						WriteConfigDescriptorInfo(DeviceExtension,
												  (PUSB_CONFIGURATION_DESCRIPTOR)BufferMDL);
					}

					break;
				}
			}

			break;
		}
	case URB_FUNCTION_GET_DESCRIPTOR_FROM_ENDPOINT:
		{
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_GET_DESC_FROM_ENDPOINT,
						 sizeof(FUNC_GET_DESC_FROM_ENDPOINT) - 1);
			break;
		}
	case URB_FUNCTION_GET_DESCRIPTOR_FROM_INTERFACE:
		{
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_GET_DESC_FROM_INTERFACE,
						 sizeof(FUNC_GET_DESC_FROM_INTERFACE) - 1);
			break;
		}
	case URB_FUNCTION_SET_DESCRIPTOR_TO_DEVICE:
		{
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_SET_DESC_TO_DEVICE,
						 sizeof(FUNC_SET_DESC_TO_DEVICE) - 1);
			break;
		}
	case URB_FUNCTION_SET_DESCRIPTOR_TO_ENDPOINT:
		{
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_SET_DESC_TO_ENDPOINT,
						 sizeof(FUNC_SET_DESC_TO_ENDPOINT) - 1);
			break;
		}
	case URB_FUNCTION_SET_DESCRIPTOR_TO_INTERFACE:
		{
			WriteString(&DeviceExtension->UrbPackets,
						 FUNC_SET_DESC_TO_INTERFACE,
						 sizeof(FUNC_SET_DESC_TO_INTERFACE) - 1);
			break;
		}
	case URB_FUNCTION_GET_STATUS_FROM_DEVICE:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_GET_STAT_FROM_DEVICE,
						 sizeof(FUNC_GET_STAT_FROM_DEVICE) - 1);
			break;
		}
	case URB_FUNCTION_GET_STATUS_FROM_INTERFACE:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_GET_STAT_FROM_INTERFACE,
						 sizeof(FUNC_GET_STAT_FROM_INTERFACE) - 1);
			break;
		}
	case URB_FUNCTION_GET_STATUS_FROM_ENDPOINT:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_GET_STAT_FROM_ENDPOINT,
						 sizeof(FUNC_GET_STAT_FROM_ENDPOINT) - 1);
			break;
		}
	case URB_FUNCTION_GET_STATUS_FROM_OTHER:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_GET_STAT_FROM_OTHER,
						 sizeof(FUNC_GET_STAT_FROM_OTHER) - 1);
			break;
		}
	case URB_FUNCTION_SET_FEATURE_TO_DEVICE:
		{
			WriteString(&DeviceExtension->UrbPackets,FUNC_SET_FEAT_TO_DEVICE,
						 sizeof(FUNC_SET_FEAT_TO_DEVICE) - 1);
			break;
		}
	case URB_FUNCTION_SET_FEATURE_TO_INTERFACE:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_SET_FEAT_TO_INTERFACE,
						 sizeof(FUNC_SET_FEAT_TO_INTERFACE) - 1);
			break;
		}
	case URB_FUNCTION_SET_FEATURE_TO_ENDPOINT:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_SET_FEAT_TO_ENDPOINT,
						 sizeof(FUNC_SET_FEAT_TO_ENDPOINT) - 1);
			break;
		}
	case URB_FUNCTION_SET_FEATURE_TO_OTHER:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_SET_FEAT_TO_OTHER,
						 sizeof(FUNC_SET_FEAT_TO_OTHER) - 1);
			break;
		}
	case URB_FUNCTION_CLEAR_FEATURE_TO_DEVICE:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_CLEAR_FEAT_TO_DEVICE,
						 sizeof(FUNC_CLEAR_FEAT_TO_DEVICE) - 1);
			break;
		}
	case URB_FUNCTION_CLEAR_FEATURE_TO_INTERFACE: 
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_CLEAR_FEAT_TO_INTERFACE,
						 sizeof(FUNC_CLEAR_FEAT_TO_INTERFACE) - 1);
			break;
		}
	case URB_FUNCTION_CLEAR_FEATURE_TO_ENDPOINT:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_CLEAR_FEAT_TO_ENDPOINT,
						 sizeof(FUNC_CLEAR_FEAT_TO_ENDPOINT) - 1);
			break;
		}
	case URB_FUNCTION_CLEAR_FEATURE_TO_OTHER:
		{
			WriteString(&DeviceExtension->UrbPackets, FUNC_CLEAR_FEAT_TO_OTHER,
						 sizeof(FUNC_CLEAR_FEAT_TO_OTHER) - 1);
			break;
		}
	}
}