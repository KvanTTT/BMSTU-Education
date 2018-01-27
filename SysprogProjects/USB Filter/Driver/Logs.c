#include "KvntUsbDrvr.h"
#include "Strings.h"

NTSTATUS InitBuffer(IN ULONG Size, IN OUT PBUFFER Packets)
{
	Packets->MaxSize     = Size;
	Packets->CurrentSize = 0;

	Packets->Buffer = ExAllocatePool(PagedPool, Size);
	if(!Packets->Buffer) 
		return STATUS_INSUFFICIENT_RESOURCES;

	Packets->Mdl = MmCreateMdl(NULL, Packets->Buffer, Size);
	if(!Packets->Mdl) 
		return STATUS_INSUFFICIENT_RESOURCES;
	
	MmProbeAndLockPages(Packets->Mdl, KernelMode, IoModifyAccess);

	return STATUS_SUCCESS;
}

VOID FreeBuffer(IN OUT PBUFFER Packets)
{
	MmUnlockPages(Packets->Mdl);
	IoFreeMdl(Packets->Mdl);
	ExFreePool(Packets->Buffer);
	Packets->MaxSize = Packets->CurrentSize = 0;
}

NTSTATUS CreateLogFile(IN  PUNICODE_STRING LogFileName,
					   OUT PHANDLE         LogFile)
{
	NTSTATUS status = STATUS_SUCCESS;

    IO_STATUS_BLOCK   iosb;    
    OBJECT_ATTRIBUTES oa;

    InitializeObjectAttributes(&oa, LogFileName, OBJ_KERNEL_HANDLE | OBJ_CASE_INSENSITIVE, NULL, NULL);
        
    status = ZwCreateFile(LogFile, FILE_READ_DATA | FILE_WRITE_DATA | FILE_APPEND_DATA | SYNCHRONIZE,
						 &oa, &iosb, 0,
						  FILE_ATTRIBUTE_NORMAL, FILE_SHARE_READ, FILE_OPEN_IF,
						  FILE_RANDOM_ACCESS | FILE_SYNCHRONOUS_IO_NONALERT,
						  NULL, 0);
	return status;
}

NTSTATUS WriteBufferToLogFile(IN PVOID  Buffer, IN ULONG  BufferSize, IN HANDLE LogFile)
{
	NTSTATUS status = STATUS_SUCCESS;
    IO_STATUS_BLOCK ioSB;

	status = ZwWriteFile(LogFile, NULL, NULL, NULL, &ioSB, Buffer, BufferSize, 0, NULL);

	return status;
}

VOID CloseLogFile(IN HANDLE LogFile)
{
	ZwClose(LogFile);
}

VOID WriteNumber(IN OUT PBUFFER UrbPackets, IN ULONG Number, IN ULONG Radix, IN BOOLEAN NewLine)
{
	ULONG IncSize;

	if (NewLine)
		IncSize = NUMBER_LENGTH + sizeof(NEW_LINE) - 1;
	else 
		IncSize = NUMBER_LENGTH + sizeof(SEMI) - 1;;

	if (UrbPackets->CurrentSize <= UrbPackets->MaxSize - IncSize)
	{
		PUCHAR Data = MmGetSystemAddressForMdl(UrbPackets->Mdl);
		CHAR str[NUMBER_LENGTH];
		RtlZeroMemory(str, NUMBER_LENGTH);
		_itoa(Number, str, Radix);

		  // проверка полученного указателя на наш буфер
		if (Data)
		{
			UCHAR NumLength;

			  // удаляем нулевые символы из правой части нашей строки
			NumLength = NUMBER_LENGTH - 1;
			while (!str[NumLength])
			{
				NumLength--;
			}

			if (NewLine)
			{			
				RtlCopyMemory(Data + UrbPackets->CurrentSize                , str     , NumLength        + 1);
			    RtlCopyMemory(Data + UrbPackets->CurrentSize + NumLength + 1, NEW_LINE, sizeof(NEW_LINE) - 1);
				UrbPackets->CurrentSize += (NumLength + sizeof(NEW_LINE));
			}
			else
			{
				RtlCopyMemory(Data + UrbPackets->CurrentSize, str, NumLength + 1);
				RtlCopyMemory(Data + UrbPackets->CurrentSize + NumLength + 1, SEMI, sizeof(SEMI) - 1);
				UrbPackets->CurrentSize += (NumLength + sizeof(SEMI));
			}
		}
	}
}

VOID WriteString(IN OUT PBUFFER UrbPackets, IN PVOID FuncHeader, IN ULONG HeaderSize)
{
	if (UrbPackets->CurrentSize <= UrbPackets->MaxSize - HeaderSize)
	{
		PUCHAR Data = MmGetSystemAddressForMdl(UrbPackets->Mdl);

		if (Data)
		{
			RtlCopyMemory(Data + UrbPackets->CurrentSize, FuncHeader, HeaderSize);
			UrbPackets->CurrentSize += HeaderSize;
		}
	}
}

VOID WriteDeviceDescriptorInfo(IN OUT PDEVICE_EXTENSION DeviceExtension, IN PUSB_DEVICE_DESCRIPTOR DeviceDescriptor)
{
	WriteString(&DeviceExtension->UrbPackets, STR_DEVICE_DESCRIPTOR, sizeof(STR_DEVICE_DESCRIPTOR) - 1);

	WriteString(&DeviceExtension->UrbPackets, STR_USB_VERSION, sizeof(STR_USB_VERSION));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->bcdUSB, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_USB_CLASS_CODE, sizeof(STR_USB_CLASS_CODE));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->bDeviceClass, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_USB_SUBCLASS, sizeof(STR_USB_SUBCLASS));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->bDeviceSubClass, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_USB_PROTOCOL_CODE, sizeof(STR_USB_PROTOCOL_CODE));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->bDeviceProtocol, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_MAX_PACK_EP0, sizeof(STR_MAX_PACK_EP0));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->bMaxPacketSize0, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_VENDOR_ID, sizeof(STR_VENDOR_ID));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->idVendor, 16, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_PROD_ID, sizeof(STR_PROD_ID));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->idProduct, 16, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_NUM_CONFIGS, sizeof(STR_NUM_CONFIGS));
	WriteNumber(&DeviceExtension->UrbPackets, DeviceDescriptor->bNumConfigurations, 10, TRUE);
}

VOID WriteConfigDescriptorInfo(IN OUT PDEVICE_EXTENSION DeviceExtension,
							   IN PUSB_CONFIGURATION_DESCRIPTOR ConfigDescriptor)
{
	WriteString(&DeviceExtension->UrbPackets, STR_CONFIG_DESCRIPTOR, sizeof(STR_CONFIG_DESCRIPTOR) - 1);

	WriteString(&DeviceExtension->UrbPackets, STR_NUM_INTERFACES, sizeof(STR_NUM_INTERFACES));
	WriteNumber(&DeviceExtension->UrbPackets, ConfigDescriptor->bNumInterfaces, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_CONFIG_VALUE, sizeof(STR_CONFIG_VALUE));
	WriteNumber(&DeviceExtension->UrbPackets, ConfigDescriptor->bConfigurationValue, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_CONFIG_ATTRS, sizeof(STR_CONFIG_ATTRS));
	WriteNumber(&DeviceExtension->UrbPackets, ConfigDescriptor->bmAttributes, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_MAX_POWER, sizeof(STR_MAX_POWER));
	WriteNumber(&DeviceExtension->UrbPackets, ConfigDescriptor->MaxPower, 10, TRUE);
}

VOID WriteInterfaceInfo(IN OUT PDEVICE_EXTENSION DeviceExtension, IN PUSBD_INTERFACE_INFORMATION Interface)
{
	WriteString(&DeviceExtension->UrbPackets, STR_INTERFACE_INFORMATION, sizeof(STR_INTERFACE_INFORMATION) - 1);

	WriteString(&DeviceExtension->UrbPackets, STR_INTERFACE_NUMBER, sizeof(STR_INTERFACE_NUMBER));
	WriteNumber(&DeviceExtension->UrbPackets, Interface->InterfaceNumber, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_ALT_SETTING, sizeof(STR_ALT_SETTING));
	WriteNumber(&DeviceExtension->UrbPackets, Interface->AlternateSetting, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_CLASS, sizeof(STR_CLASS));
	WriteNumber(&DeviceExtension->UrbPackets, Interface->Class, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_SUBCLASS, sizeof(STR_SUBCLASS));
	WriteNumber(&DeviceExtension->UrbPackets, Interface->SubClass, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_PROTOCOL, sizeof(STR_PROTOCOL));
	WriteNumber(&DeviceExtension->UrbPackets, Interface->Protocol, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_INTERFACE_HANDLE, sizeof(STR_INTERFACE_HANDLE));
	WriteNumber(&DeviceExtension->UrbPackets, (ULONG)Interface->InterfaceHandle, 16, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_NUM_PIPES, sizeof(STR_NUM_PIPES));
	WriteNumber(&DeviceExtension->UrbPackets, Interface->NumberOfPipes, 10, TRUE);
}

VOID WritePipeInfo(IN OUT PDEVICE_EXTENSION	DeviceExtension, IN USBD_PIPE_INFORMATION Pipe)
{
	WriteString(&DeviceExtension->UrbPackets, STR_PIPE_INFORMATION, sizeof(STR_PIPE_INFORMATION) - 1);

	WriteString(&DeviceExtension->UrbPackets, STR_MAX_PACKET_SIZE, sizeof(STR_MAX_PACKET_SIZE));
	WriteNumber(&DeviceExtension->UrbPackets, Pipe.MaximumPacketSize, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_ENDPNT_ADDRESS, sizeof(STR_ENDPNT_ADDRESS));
	WriteNumber(&DeviceExtension->UrbPackets, Pipe.EndpointAddress, 16, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_INTERVAL, sizeof(STR_INTERVAL));
	WriteNumber(&DeviceExtension->UrbPackets, Pipe.Interval, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_PIPE_TYPE, sizeof(STR_PIPE_TYPE));
	switch (Pipe.PipeType)
	{
		case UsbdPipeTypeControl	:
			WriteString(&DeviceExtension->UrbPackets, STR_CONTROL_PIPE, sizeof(STR_CONTROL_PIPE) - 1);		
			break;
		case UsbdPipeTypeIsochronous:
			WriteString(&DeviceExtension->UrbPackets, STR_ISOCHRONOUS_PIPE, sizeof(STR_ISOCHRONOUS_PIPE) - 1);				
			break;
		case UsbdPipeTypeBulk		:
			WriteString(&DeviceExtension->UrbPackets, STR_BULK_PIPE, sizeof(STR_BULK_PIPE) - 1);		
			break;
		case UsbdPipeTypeInterrupt  :
			WriteString(&DeviceExtension->UrbPackets, STR_INTERRUPT_PIPE, sizeof(STR_INTERRUPT_PIPE) - 1);
			break;
		default:
			WriteString(&DeviceExtension->UrbPackets, STR_UNKNOWN_PIPE, sizeof(STR_UNKNOWN_PIPE) - 1);
			break;
	}

	WriteString(&DeviceExtension->UrbPackets, STR_PIPE_HANDLE, sizeof(STR_PIPE_HANDLE));
	WriteNumber(&DeviceExtension->UrbPackets, (ULONG)Pipe.PipeHandle, 16, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_MAX_TRANSFER_SIZE, sizeof(STR_MAX_TRANSFER_SIZE));
	WriteNumber(&DeviceExtension->UrbPackets, Pipe.MaximumTransferSize, 10, FALSE);

	WriteString(&DeviceExtension->UrbPackets, STR_PIPE_FLAGS, sizeof(STR_PIPE_FLAGS));
	WriteNumber(&DeviceExtension->UrbPackets, Pipe.PipeFlags, 10, TRUE);
}