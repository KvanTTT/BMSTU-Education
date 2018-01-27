/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             devctl.c
 * PROGRAMMER:       Matt Wu <mattwu@163.com>
 * HOMEPAGE:		 http://ext2.yeah.net
 * UPDATE HISTORY: 
 */

/* INCLUDES *****************************************************************/

#include "ntifs.h"
#include "ext2fs.h"

/* GLOBALS ***************************************************************/

extern PEXT2_GLOBAL	Ext2Global;

/* DEFINITIONS *************************************************************/

#ifdef ALLOC_PRAGMA
#if DBG
#pragma alloc_text(PAGE, Ext2DeviceControlCompletion)
#endif
#pragma alloc_text(PAGE, Ext2SearchDirMcb)
#pragma alloc_text(PAGE, Ext2DeviceControl)
#pragma alloc_text(PAGE, Ext2DeviceControlNormal)
#if EXT2_UNLOAD
#pragma alloc_text(PAGE, Ext2PrepareToUnload)
#endif
#pragma alloc_text(PAGE, Ext2DeviceControl)
#endif

#if DBG

NTSTATUS
Ext2DeviceControlCompletion (IN PDEVICE_OBJECT   DeviceObject,
			     IN PIRP             Irp,
			     IN PVOID            Context)
{
	if (Irp->PendingReturned)
	{
		IoMarkIrpPending(Irp);
	}
	
	KdPrint((DRIVER_NAME ": %-16.16s %-31s *** Status: %s (%#x) ***\n",
		PsGetCurrentProcess()->ImageFileName,
		"IRP_MJ_DEVICE_CONTROL",
		Ext2NtStatusToString(Irp->IoStatus.Status),
		Irp->IoStatus.Status ));
	
	return STATUS_SUCCESS;
}

#endif // DBG


NTSTATUS
Ext2DeviceControlNormal (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	BOOLEAN         CompleteRequest;
	NTSTATUS        Status = STATUS_UNSUCCESSFUL;
	PEXT2_VCB       Vcb;
	PIRP            Irp;
	PDEVICE_OBJECT  TargetDeviceObject;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;
	
		if (DeviceObject == Ext2Global->DeviceObject)
		{
			CompleteRequest = TRUE;
			Status = STATUS_INVALID_DEVICE_REQUEST;
			
			__leave;
		}
		
		Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
		
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		Irp = IrpContext->Irp;
		
		TargetDeviceObject = Vcb->TargetDeviceObject;
		
		//
		// Pass on the IOCTL to the driver below
		//
		
		CompleteRequest = FALSE;

		IoSkipCurrentIrpStackLocation(Irp);
#if DBG
		IoSetCompletionRoutine(
			Irp,
			Ext2DeviceControlCompletion,
			DeviceObject,
			FALSE,
			TRUE,
			TRUE );
#else
		IoSkipCurrentIrpStackLocation(Irp);
#endif
		
		Status = IoCallDriver(TargetDeviceObject, Irp);
	}

	__finally
	{
		if (!IrpContext->ExceptionInProgress)
		{
			if (CompleteRequest)
			{
				IrpContext->Irp->IoStatus.Status = Status;
				
				Ext2CompleteRequest(
					IrpContext->Irp,
					(CCHAR)
					(NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT)
					);
			}

			Ext2FreeIrpContext(IrpContext);
		}
	}
	
	return Status;
}


#if EXT2_UNLOAD

NTSTATUS
Ext2PrepareToUnload (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	NTSTATUS        Status = STATUS_UNSUCCESSFUL;
	BOOLEAN         GlobalDataResourceAcquired = FALSE;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;
		
		if (DeviceObject != Ext2Global->DeviceObject)
		{
			Status = STATUS_INVALID_DEVICE_REQUEST;
			__leave;
		}
		
		ExAcquireResourceExclusiveLite(
			&Ext2Global->Resource,
			TRUE );
		
		GlobalDataResourceAcquired = TRUE;
		
		if (FlagOn(Ext2Global->Flags, EXT2_UNLOAD_PENDING))
		{
			KdPrint(("Ext2PrepareUnload:  Already ready to unload.\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		if (!IsListEmpty(&(Ext2Global->VcbList)))
		{
			KdPrint(("Ext2PrepareUnload:  Mounted volumes exists.\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		IoUnregisterFileSystem(Ext2Global->DeviceObject);
		
		Ext2Global->DriverObject->DriverUnload = DriverUnload;
		
		SetFlag(Ext2Global->Flags ,EXT2_UNLOAD_PENDING);
		
		KdPrint(("Ext2PrepareToUnload: Driver is ready to unload.\n"));
		
		Status = STATUS_SUCCESS;
	}
	__finally
	{
		if (GlobalDataResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Ext2Global->Resource,
				ExGetCurrentResourceThread()
				);
		}
		
		if (!IrpContext->ExceptionInProgress)
		{
			IrpContext->Irp->IoStatus.Status = Status;
			
			Ext2CompleteRequest(
				IrpContext->Irp,
				(CCHAR)
				(NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT));
			
			Ext2FreeIrpContext(IrpContext);
		}
	}
	
	return Status;
}

#endif


NTSTATUS
Ext2DeviceControl (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PIRP                Irp;
	PIO_STACK_LOCATION  IoStackLocation;
	ULONG               IoControlCode;
	NTSTATUS            Status;
	
	ASSERT(IrpContext);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
	Irp = IrpContext->Irp;
	
	IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
	
	IoControlCode =
		IoStackLocation->Parameters.DeviceIoControl.IoControlCode;
	
	switch (IoControlCode)
	{
#if EXT2_UNLOAD
	case IOCTL_PREPARE_TO_UNLOAD:
		Status = Ext2PrepareToUnload(IrpContext);
		break;
#endif		
	default:
		Status = Ext2DeviceControlNormal(IrpContext);
	}
	
	return Status;
}
