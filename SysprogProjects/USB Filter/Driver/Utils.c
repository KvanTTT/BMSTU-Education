#include "KvntUsbDrvr.h"

#ifdef ALLOC_PRAGMA
#pragma alloc_text(PAGE, CallNextDriverSync)
#pragma alloc_text(PAGE, CallDriverSync)
#endif

NTSTATUS CallNextDriverSync(PDEVICE_EXTENSION DeviceExtension, PIRP Irp)
{
    NTSTATUS status;

    IncrementPendingActionCount(DeviceExtension);
    status = CallDriverSync(DeviceExtension->topDevObj, Irp);
    DecrementPendingActionCount(DeviceExtension);

    return status;
}

NTSTATUS CallDriverSync(PDEVICE_OBJECT devObj, PIRP Irp)
{
    KEVENT   event;
    NTSTATUS status;

	  // инициализируем состояние события как несигнальное
    KeInitializeEvent(&event, NotificationEvent, FALSE);

	  // регистрируем callback-процедуру заверешения обработки пакета
    IoSetCompletionRoutine(Irp, CallDriverSyncCompletion, &event, TRUE, TRUE, TRUE);

	  // отправляем пакет на обработку нижним драйверным слоям
    status = IoCallDriver(devObj, Irp);

	  // ждем, пока не закончится работа на нижних уровнях
    KeWaitForSingleObject(&event, Executive, KernelMode, FALSE, NULL);

    status = Irp->IoStatus.Status;

    return status;
}

NTSTATUS CallDriverSyncCompletion(IN PDEVICE_OBJECT devObjOrNULL, IN PIRP Irp, IN PVOID context)
{
    PKEVENT event = context;

	  // устанваливаем событие в сигнальное состояние
    KeSetEvent(event, 0, FALSE);

    return STATUS_MORE_PROCESSING_REQUIRED;
}

VOID IncrementPendingActionCount(PDEVICE_EXTENSION DeviceExtension)
{
    InterlockedIncrement(&DeviceExtension->pendingActionCount);    
}

VOID DecrementPendingActionCount(PDEVICE_EXTENSION DeviceExtension)
{
    InterlockedDecrement(&DeviceExtension->pendingActionCount);    

    if (DeviceExtension->pendingActionCount < 0)
      KeSetEvent(&DeviceExtension->removeEvent, 0, FALSE);
}