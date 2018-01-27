#include "KvntUsbDrvr.h"

#ifdef ALLOC_PRAGMA
#pragma alloc_text(PAGE, PnP_Dispatch)
#endif

NTSTATUS PnP_Dispatch(IN PDEVICE_EXTENSION DeviceExtension, IN PIRP Irp)
{
    PIO_STACK_LOCATION IrpSp;

    BOOLEAN completeIrpHere = FALSE;
    BOOLEAN justReturnStatus = FALSE;
    NTSTATUS status = STATUS_SUCCESS;

    IrpSp = IoGetCurrentIrpStackLocation(Irp);

    switch (IrpSp->MinorFunction)
	{
		  // инициализация устройства
		case IRP_MN_START_DEVICE:	
			DeviceExtension->state = STATE_STARTING;

			IoCopyCurrentIrpStackLocationToNext(Irp);
			status = CallNextDriverSync(DeviceExtension, Irp);

			if (!NT_SUCCESS(status))
				DeviceExtension->state = STATE_START_FAILED;

			completeIrpHere = TRUE;
			break;
		  // остановка устройства с потенциальной возможностью перезапуска или удаления из системы
		case IRP_MN_STOP_DEVICE: 
			if (DeviceExtension->state == STATE_SUSPENDED)
			{
				status = STATUS_DEVICE_POWER_FAILURE;
				completeIrpHere = TRUE;
			}
			else 
				if (DeviceExtension->state == STATE_STARTED)
					DeviceExtension->state = STATE_STOPPED;
			break;
		  // удаление устройства без предварительного предупреждения
		case IRP_MN_SURPRISE_REMOVAL: 
			Irp->IoStatus.Status = STATUS_SUCCESS;
			DeviceExtension->state = STATE_REMOVING;
			break;
		  // удаление устройства из системы - следует выполнить работу обратную той, что была продлена в AddDevice
	    case IRP_MN_REMOVE_DEVICE:	
			if (DeviceExtension->state != STATE_REMOVED)
			{
				DeviceExtension->state = STATE_REMOVED;
				IoCopyCurrentIrpStackLocationToNext(Irp);
				status = IoCallDriver(DeviceExtension->topDevObj, Irp);
				justReturnStatus = TRUE;

				// сброс буфера URB-пакетов в лог-файл
				if ((KeGetCurrentIrql() == PASSIVE_LEVEL) && (DeviceExtension->PreparedToLog))
				{
					PUCHAR Data = MmGetSystemAddressForMdl(DeviceExtension->UrbPackets.Mdl);

					if (Data)
						status = WriteBufferToLogFile(Data, DeviceExtension->UrbPackets.CurrentSize,
							DeviceExtension->LogFileHandle);
					CloseLogFile(DeviceExtension->LogFileHandle);
					FreeBuffer(&DeviceExtension->UrbPackets);
				}

				DecrementPendingActionCount(DeviceExtension);
				KeWaitForSingleObject(&DeviceExtension->removeEvent,
					Executive, KernelMode, FALSE, NULL);

				IoDeleteSymbolicLink(&DeviceExtension->SymLink);

				IoDetachDevice(DeviceExtension->topDevObj);	
				IoDeleteDevice(DeviceExtension->filterDevObj);
			}	
			break;
		default: break;
	}

	if (!justReturnStatus)
		if (completeIrpHere)
			{
			Irp->IoStatus.Status = status;
			IoCompleteRequest(Irp, IO_NO_INCREMENT);
			}
		else
			{
			IoCopyCurrentIrpStackLocationToNext(Irp);
			status = IoCallDriver(DeviceExtension->topDevObj, Irp);
			}

    return status;
}