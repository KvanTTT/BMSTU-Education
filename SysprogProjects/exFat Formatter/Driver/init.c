/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             init.c
 * PROGRAMMER:       Matt Wu <mattwu@163.com>
 * HOMEPAGE:		 http://ext2.yeah.net
 * UPDATE HISTORY: 
 */

/* INCLUDES *****************************************************************/

#include "ntifs.h"
#include "ext2fs.h"

/* GLOBALS ***************************************************************/

PEXT2_GLOBAL	Ext2Global = NULL;

/* DEFINITIONS ***********************************************************/

NTSTATUS
DriverEntry(
    IN PDRIVER_OBJECT DriverObject,
    IN PUNICODE_STRING RegistryPath   );

#ifdef ALLOC_PRAGMA
#pragma alloc_text(INIT, DriverEntry)
#if EXT2_UNLOAD
#pragma alloc_text(PAGE, DriverUnload)
#endif
#endif

/* FUNCTIONS ***************************************************************/

#if EXT2_UNLOAD

VOID
DriverUnload (IN PDRIVER_OBJECT DriverObject)
/*
 * FUNCTION: Вызывается системой, чтобы выгрузить драйвер
 * ARGUMENTS:
 *           DriverObject = объект, описывающий этот драйвер
 * RETURNS:  None
 */
{
	UNICODE_STRING	DosDeviceName;

	KdPrint(("Ext2Fsd: Unloading routine.\n"));

	RtlInitUnicodeString(&DosDeviceName, DOS_DEVICE_NAME);
	IoDeleteSymbolicLink(&DosDeviceName);

	ExDeleteResourceLite(&Ext2Global->Resource);

	ExDeleteNPagedLookasideList(&(Ext2Global->Ext2IrpContextLookasideList));
	
	IoDeleteDevice(Ext2Global->DeviceObject);
}

#endif

NTSTATUS
DriverEntry (
	     IN PDRIVER_OBJECT   DriverObject,
	     IN PUNICODE_STRING  RegistryPath
	     )
/*
 * FUNCTION: Вызываются системой, чтобы инициализировать драйвер
 * ARGUMENTS:
 *           DriverObject = объект, описывающий этот драйвер
 *           RegistryPath = путь к нашим записям конфигурации 
 * RETURNS: Success or failure
 */
{
	PDEVICE_OBJECT		    DeviceObject;  // указатель на объект драйвера
	PFAST_IO_DISPATCH           FastIoDispatch;
	PCACHE_MANAGER_CALLBACKS    CacheManagerCallbacks;
	PEXT2FS_EXT		    DeviceExt;

	UNICODE_STRING              DeviceName;  // указатель на раздел реестра
#if EXT2_UNLOAD
	UNICODE_STRING              DosDeviceName;
#endif
	NTSTATUS                    Status;

	LARGE_INTEGER		    CurrentTime;
	LARGE_INTEGER		    LocalTime;
	TIME_FIELDS				TimeFields;

	KeQuerySystemTime( &CurrentTime);
	KdPrint(("CurrentTime: %xh\n", CurrentTime.QuadPart));
	RtlTimeToTimeFields(
		&CurrentTime,
		&TimeFields);
	KdPrint(("CurrentTime: %d-%d-%d %d:%d:%d:%d\n",
		TimeFields.Year,
		TimeFields.Month,
		TimeFields.Day,
		TimeFields.Hour,
		TimeFields.Minute,
		TimeFields.Second,
		TimeFields.Milliseconds));

	RtlZeroMemory(&TimeFields, sizeof(TIME_FIELDS));
	TimeFields.Year = 1970;
	TimeFields.Month = 1;
	TimeFields.Day = 1;
	TimeFields.Hour = 8;
	TimeFields.Minute = 0;
	TimeFields.Second = 0;
	TimeFields.Milliseconds = 0;

	if (RtlTimeFieldsToTime(
		&TimeFields,
		&(LocalTime)))
	{
		KdPrint(("LocalTime: %I64xh\n", LocalTime.QuadPart));
	}
	else
		KdPrint(("FiledstoTime error.\n"));

  
	KdPrint(("Ext2 File System Driver by Matt Wu.\n"));
	KdPrint(("Ext2 DriverEntry...\n"));

    
    // Печать некоторой информации о драйвере
 
    DbgPrint(DRIVER_NAME ": " __DATE__ " " __TIME__
#ifdef EXT2_RO
    ", read-only"
#endif
#if DBG
    ", checked"
#endif
    ", _WIN32_WINNT=%#x.\n", _WIN32_WINNT);


	RtlInitUnicodeString(&DeviceName, DEVICE_NAME);

	/* Создаём свой FDO и получаем указатель на него в fdo. 
	Размер структуры EXT2FS_EXT передаётся для того, 
	чтобы при создании FDO выделить под неё память.*/

	Status = IoCreateDevice(
		DriverObject,
		sizeof(EXT2FS_EXT),
		&DeviceName,
		FILE_DEVICE_DISK_FILE_SYSTEM,
		0,
		FALSE,
		&DeviceObject );
	
	if (!NT_SUCCESS(Status))
	{
		KdPrint(("Ext2 IoCreateDevice error.\n"));
		return Status;
	}

	DeviceExt = (PEXT2FS_EXT) DeviceObject->DeviceExtension;
	RtlZeroMemory(DeviceExt, sizeof(EXT2FS_EXT));
	
	Ext2Global = &(DeviceExt->Ext2Global);
	Ext2Global->TimeZone.QuadPart = LocalTime.QuadPart;
	KdPrint(("TimeZone: %I64xh\n", Ext2Global->TimeZone.QuadPart));


	Ext2Global->Identifier.Type = FGD;
	Ext2Global->Identifier.Size = sizeof(EXT2_GLOBAL);
	Ext2Global->DeviceObject = DeviceObject;
	Ext2Global->DriverObject = DriverObject;
	
	// Экспортируем точки входа в драйвер

	DriverObject->MajorFunction[IRP_MJ_CREATE] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_CLOSE] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_READ] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_QUERY_INFORMATION] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_SET_INFORMATION] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_QUERY_VOLUME_INFORMATION] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_DIRECTORY_CONTROL] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_FILE_SYSTEM_CONTROL] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_DEVICE_CONTROL] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_LOCK_CONTROL] = Ext2BuildRequest;
	DriverObject->MajorFunction[IRP_MJ_CLEANUP] = Ext2BuildRequest;
	DriverObject->DriverUnload = NULL;


	//Инициализация точек входа быстрого ввода - вывода
	
	FastIoDispatch = &(Ext2Global->FastIoDispatch);
	
	FastIoDispatch->SizeOfFastIoDispatch = sizeof(FAST_IO_DISPATCH);
	FastIoDispatch->FastIoCheckIfPossible = Ext2FastIoCheckIfPossible;
#if DBG
	FastIoDispatch->FastIoRead = Ext2FastIoRead;
#else
	FastIoDispatch->FastIoRead = FsRtlCopyRead;
#endif
	FastIoDispatch->FastIoQueryBasicInfo = Ext2FastIoQueryBasicInfo;
	FastIoDispatch->FastIoQueryStandardInfo = Ext2FastIoQueryStandardInfo;
	FastIoDispatch->FastIoLock = Ext2FastIoLock;
	FastIoDispatch->FastIoUnlockSingle = Ext2FastIoUnlockSingle;
	FastIoDispatch->FastIoUnlockAll = Ext2FastIoUnlockAll;
	FastIoDispatch->FastIoUnlockAllByKey = Ext2FastIoUnlockAllByKey;
	FastIoDispatch->FastIoQueryNetworkOpenInfo = Ext2FastIoQueryNetworkOpenInfo;

	DriverObject->FastIoDispatch = FastIoDispatch;

    switch ( MmQuerySystemSize() ) {

    case MmSmallSystem:

        Ext2Global->MaxDepth = 4;
        break;

    case MmMediumSystem:

        Ext2Global->MaxDepth = 8;
        break;

    case MmLargeSystem:

        Ext2Global->MaxDepth = 16;
        break;
    }

	
	// Инициализация возвратных вызовов менеджера кэша
	
	CacheManagerCallbacks = &(Ext2Global->CacheManagerCallbacks);
	CacheManagerCallbacks->AcquireForLazyWrite = Ext2AcquireForLazyWrite;
	CacheManagerCallbacks->ReleaseFromLazyWrite = Ext2ReleaseFromLazyWrite;
	CacheManagerCallbacks->AcquireForReadAhead = Ext2AcquireForReadAhead;
	CacheManagerCallbacks->ReleaseFromReadAhead = Ext2ReleaseFromReadAhead;

    Ext2Global->CacheManagerNoOpCallbacks.AcquireForLazyWrite  = Ext2NoOpAcquire;
    Ext2Global->CacheManagerNoOpCallbacks.ReleaseFromLazyWrite = Ext2NoOpRelease;
    Ext2Global->CacheManagerNoOpCallbacks.AcquireForReadAhead  = Ext2NoOpAcquire;
    Ext2Global->CacheManagerNoOpCallbacks.ReleaseFromReadAhead = Ext2NoOpRelease;


	// Инициализация глобальных данных

	InitializeListHead(&(Ext2Global->VcbList));
	ExInitializeResourceLite(&(Ext2Global->Resource));

    ExInitializeNPagedLookasideList( &(Ext2Global->Ext2IrpContextLookasideList),
                                     NULL,
                                     NULL,
                                     0,
                                     sizeof(EXT2_IRP_CONTEXT),
                                     '2TXE',
                                     Ext2Global->MaxDepth);

#if EXT2_UNLOAD
	RtlInitUnicodeString(&DosDeviceName, DOS_DEVICE_NAME);
	IoCreateSymbolicLink(&DosDeviceName, &DeviceName);
#endif

#if DBG
	ProcessNameOffset = Ext2GetProcessNameOffset();
#endif

	IoRegisterFileSystem(DeviceObject);
	
	return Status;
}
