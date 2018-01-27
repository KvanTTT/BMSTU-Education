/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             close.c
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
#pragma alloc_text(PAGE, Ext2Close)
#pragma alloc_text(PAGE, Ext2QueueCloseRequest)
#pragma alloc_text(PAGE, Ext2DeQueueCloseRequest)
#endif

NTSTATUS
Ext2Close (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	NTSTATUS        Status = STATUS_SUCCESS;
	PEXT2_VCB       Vcb;
	BOOLEAN         VcbResourceAcquired = FALSE;
	PFILE_OBJECT    FileObject;
	PEXT2_FCB       Fcb;
	BOOLEAN         FcbResourceAcquired = FALSE;
	PEXT2_CCB       Ccb;
	BOOLEAN         FreeVcb = FALSE;

	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;

		if (DeviceObject == Ext2Global->DeviceObject)
		{
			Status = STATUS_SUCCESS;
			__leave;
		}
		
		Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
		
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		if (!ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			IrpContext->IsSynchronous ))
		{
			KdPrint(("Ext2Close: PENDING ... Vcb: %xh/%xh\n", Vcb->OpenFileHandleCount, Vcb->ReferenceCount));
			Status = STATUS_PENDING;
			__leave;
		}
		
		VcbResourceAcquired = TRUE;
		
		FileObject = IrpContext->FileObject;
		
		Fcb = (PEXT2_FCB) FileObject->FsContext;
		
		if (!Fcb)
		{
			Status = STATUS_SUCCESS;
			__leave;
		}

		ASSERT(Fcb != NULL);
		
		if (Fcb->Identifier.Type == VCB)
		{
			ExAcquireResourceExclusiveLite(&Vcb->CountResource, TRUE);
            Vcb->ReferenceCount--;
			ExReleaseResourceForThreadLite(
					&Vcb->CountResource,
					ExGetCurrentResourceThread());

            if (!Vcb->ReferenceCount && FlagOn(Vcb->Flags, VCB_DISMOUNT_PENDING))
			{
				FreeVcb = TRUE;
			}
			
			Status = STATUS_SUCCESS;
			
			__leave;
		}
		
		if (Fcb->Identifier.Type != FCB || Fcb->Identifier.Size != sizeof(EXT2_FCB))
		{
			KdPrint(("Ext2Close: Strange IRP_MJ_CLOSE by system!\n"));
			// DbgBreakPoint();

			__leave;
		}

		ASSERT((Fcb->Identifier.Type == FCB) &&
			(Fcb->Identifier.Size == sizeof(EXT2_FCB)));
		
#ifndef EXT2_RO
        if (!FlagOn(Fcb->Flags, FCB_PAGE_FILE))
#endif
		{
			if (!ExAcquireResourceExclusiveLite(
				&Fcb->MainResource,
				IrpContext->IsSynchronous ))
			{
				Status = STATUS_PENDING;
				__leave;
			}
			
			FcbResourceAcquired = TRUE;
		}
		
		Ccb = (PEXT2_CCB) FileObject->FsContext2;

		if (!Ccb)
		{
			Status = STATUS_SUCCESS;
			__leave;
		}

		ASSERT((Ccb->Identifier.Type == CCB) &&
			(Ccb->Identifier.Size == sizeof(EXT2_CCB)));
		
		ExAcquireResourceExclusiveLite(&Fcb->CountResource, TRUE);
		Fcb->ReferenceCount--;
		ExReleaseResourceForThreadLite(
			&Fcb->CountResource,
			ExGetCurrentResourceThread());
		
		ExAcquireResourceExclusiveLite(&Vcb->CountResource, TRUE);
		Vcb->ReferenceCount--;
		ExReleaseResourceForThreadLite(
			&Vcb->CountResource,
			ExGetCurrentResourceThread());
		
        if (!Vcb->ReferenceCount && FlagOn(Vcb->Flags, VCB_DISMOUNT_PENDING))
        {
            FreeVcb = TRUE;
        }

        KdPrint((
            DRIVER_NAME ": OpenHandleCount: %-7u ReferenceCount: %-7u %s\n",
            Fcb->OpenHandleCount,
            Fcb->ReferenceCount,
            Fcb->AnsiFileName.Buffer ));

		if (Ccb)
		{
			Ext2FreeCcb(Ccb);
			FileObject->FsContext2 = Ccb = NULL;
		}
		
		if (!Fcb->ReferenceCount)
		{
			ExReleaseResourceForThreadLite(
				&Fcb->MainResource,
				ExGetCurrentResourceThread() );
			
			FcbResourceAcquired = FALSE;
			
			Ext2FreeFcb(Fcb);
		}
		
		Status = STATUS_SUCCESS;
    }
    __finally
    {
	    if (FcbResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Fcb->MainResource,
			    ExGetCurrentResourceThread() );
	    }

	    if (VcbResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Vcb->MainResource,
			    ExGetCurrentResourceThread()  );
	    }
	    
	    if (!IrpContext->ExceptionInProgress)
	    {
		    if (Status == STATUS_PENDING)
		    {
		    
			    Status = STATUS_SUCCESS;
			    
			    if (IrpContext->Irp != NULL)
			    {
				    IrpContext->Irp->IoStatus.Status = Status;
				    
				    Ext2CompleteRequest(
					    IrpContext->Irp,
					    (CCHAR)
					    (NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT)
					    );
				    
				    IrpContext->Irp = NULL;
			    }
			    
			    Ext2QueueCloseRequest(IrpContext);
		    }
		    else
		    {
			    if (IrpContext->Irp != NULL)
			    {
				    IrpContext->Irp->IoStatus.Status = Status;
				    
				    Ext2CompleteRequest(
					    IrpContext->Irp,
					    (CCHAR)
					    (NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT)
					    );
			    }
			    
			    Ext2FreeIrpContext(IrpContext);
			    
			    if (FreeVcb)
			    {
				    Ext2FreeVcb(Vcb);
			    }
		    }
	    }
    }
    
    return Status;
}

VOID
Ext2QueueCloseRequest (IN PEXT2_IRP_CONTEXT IrpContext)
{
	ASSERT(IrpContext);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
	// IsSynchronous means we can block (so we don't requeue it)
	IrpContext->IsSynchronous = TRUE;
	
	ExInitializeWorkItem(
		&IrpContext->WorkQueueItem,
		Ext2DeQueueCloseRequest,
		IrpContext);
	
	ExQueueWorkItem(&IrpContext->WorkQueueItem, CriticalWorkQueue);
}

VOID
Ext2DeQueueCloseRequest (IN PVOID Context)
{
	PEXT2_IRP_CONTEXT IrpContext;
	
	IrpContext = (PEXT2_IRP_CONTEXT) Context;
	
	ASSERT(IrpContext);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
	__try
	{
		__try
		{
			FsRtlEnterFileSystem();
			Ext2Close(IrpContext);
		}
		__except (Ext2ExceptionFilter(IrpContext, GetExceptionCode()))
		{
			Ext2ExceptionHandler(IrpContext);
		}
	}
	__finally
	{
		FsRtlExitFileSystem();
	}
}
