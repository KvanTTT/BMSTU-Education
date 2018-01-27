#include <wdm.h>
#include <usb.h>
#include <usb100.h>
#include <usbioctl.h>
#include <usbdlib.h>
#include <stdlib.h>

  // возможные состояния устройства
enum deviceState {STATE_INITIALIZED,
				  STATE_STARTING,
				  STATE_STARTED,
				  STATE_START_FAILED,
				  STATE_STOPPED,
				  STATE_SUSPENDED,
				  STATE_REMOVING,
				  STATE_REMOVED};

#define FILTER_TAG (ULONG)'tlFM'
#undef  ExAllocatePool
#define ExAllocatePool(type, size) ExAllocatePoolWithTag(type, size, FILTER_TAG)

#define _DEVICE_EXTENSION_SIGNATURE 'tlFM'

  // структура для сбора информации о URB-пакетах
typedef struct _BUFFER
{
	PVOID Buffer;
	PMDL  Mdl;
	ULONG MaxSize;
	ULONG CurrentSize;
} BUFFER, *PBUFFER;

  // структура расширения устройства нашего FDO
typedef struct _DEVICE_EXTENSION
{
	BUFFER UrbPackets; // буффер, хранящий информацию о прошедших URB-пакетах	 
	ULONG UrbCount;  // количество прошедших через драйвер Urb-пакетов	
	ULONG MaxLogSize;  // максимальный размер лог-файла	  
	UNICODE_STRING LogFileName;	// путь к каталогу для хранения протоколов перехвата запросов	  
	HANDLE LogFileHandle; // HANDLE лог-файла	  
	BOOLEAN PreparedToLog; // признак того, что подготовка к протоколированию прошла успешно	  
	PURB Urb; // обрабатываемый URB-пакет	  
    PDEVICE_OBJECT DevObj; // родительский PDO	  
    enum deviceState state; // PnP-состояние нашего FDO	  
    PDEVICE_OBJECT filterDevObj; // FDO, созданный нашим драйвером	  
    PDEVICE_OBJECT topDevObj; // верхушка стека устройств, к которой мы присоединились	  
    LONG pendingActionCount; // переменная для учета отложенных действий  
    KEVENT removeEvent; // событие для ожидания завершения всех отложенных действий перед обработкой REMOVE_DEVICE  
	UNICODE_STRING SymLink; // символьная ссылка основного объекта устройства драйвера
} DEVICE_EXTENSION, *PDEVICE_EXTENSION;

NTSTATUS InitBuffer(IN ULONG Size, IN OUT PBUFFER Packets);

VOID FreeBuffer(IN OUT PBUFFER Packets);

VOID WriteNumber(IN OUT PBUFFER UrbPackets, IN ULONG Number, IN ULONG Radix, IN BOOLEAN NewLine);

VOID WriteString(IN OUT PBUFFER UrbPackets, IN PVOID FuncHeader, IN ULONG HeaderSize);

VOID WriteDeviceDescriptorInfo(IN OUT PDEVICE_EXTENSION DeviceExtension, IN PUSB_DEVICE_DESCRIPTOR DeviceDescriptor);

VOID WriteConfigDescriptorInfo(IN OUT PDEVICE_EXTENSION         DeviceExtension,
							   IN PUSB_CONFIGURATION_DESCRIPTOR ConfigDescriptor);

VOID WriteInterfaceInfo(IN OUT PDEVICE_EXTENSION DeviceExtension, IN PUSBD_INTERFACE_INFORMATION Interface);

VOID WritePipeInfo(IN OUT PDEVICE_EXTENSION DeviceExtension, IN USBD_PIPE_INFORMATION Pipe);

VOID WriteUrbPacketToBuffer(IN OUT PDEVICE_EXTENSION DeviceExtension);

NTSTATUS CreateLogFile(IN  PUNICODE_STRING LogFileName, OUT PHANDLE LogFile);

NTSTATUS WriteBufferToLogFile(IN PVOID Buffer, IN ULONG BufferSize, IN HANDLE LogFile);

VOID CloseLogFile(IN HANDLE LogFile);

NTSTATUS DriverEntry(IN PDRIVER_OBJECT DriverObject, IN PUNICODE_STRING RegistryPath);

NTSTATUS AddDevice(IN PDRIVER_OBJECT DrvObj, IN PDEVICE_OBJECT DevObj);

VOID DriverUnload(IN PDRIVER_OBJECT DriverObject);

NTSTATUS CompleteRoutine(IN PDEVICE_OBJECT DeviceObject, IN	PIRP Irp, IN PVOID Context);

NTSTATUS DispatchInternalDeviceControl(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp);

NTSTATUS DispatchRoutine(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp);

NTSTATUS PnP_Dispatch(PDEVICE_EXTENSION DeviceExtension, PIRP Irp);

NTSTATUS CallNextDriverSync(PDEVICE_EXTENSION DeviceExtension, PIRP Irp);

NTSTATUS CallDriverSync(PDEVICE_OBJECT devObj, PIRP Irp);

NTSTATUS CallDriverSyncCompletion(IN PDEVICE_OBJECT devObj, IN PIRP Irp, IN PVOID Context);

VOID IncrementPendingActionCount(PDEVICE_EXTENSION DeviceExtension);

VOID DecrementPendingActionCount(PDEVICE_EXTENSION DeviceExtension);

NTSTATUS RegOpenDeviceSubKey(IN  PDEVICE_OBJECT pDeviceObj, IN PUNICODE_STRING pSubKeyName, OUT PHANDLE phKey);

NTSTATUS RegReadDword(IN HANDLE hKey, IN PUNICODE_STRING pValueName, OUT PULONG pResult);

NTSTATUS RegReadString(IN HANDLE hKey, IN PUNICODE_STRING pValueName, OUT PUNICODE_STRING pResult);