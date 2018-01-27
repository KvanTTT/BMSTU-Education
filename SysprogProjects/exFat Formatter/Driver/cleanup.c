/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             cleanup.c
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
#pragma alloc_text(PAGE, Ext2Cleanup)
#endif


NTSTATUS
Ext2Cleanup (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	NTSTATUS        Status = STATUS_SUCCESS;
	PEXT2_VCB       Vcb;
	BOOLEAN         VcbResourceAcquired = FALSE;
	PFILE_OBJECT    FileObject;
	PEXT2_FCB       Fcb;
	BOOLEAN         FcbResourceAcquired = FALSE;
	PEXT2_CCB       Ccb;
	PIRP            Irp;

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
                 IrpContext->IsSynchronous
                 ))
        {
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
		
		if (Fcb->Identifier.Type == VCB)
		{
            if (FlagOn(Vcb->Flags, VCB_VOLUME_LOCKED))
            {
                ClearFlag(Vcb->Flags, VCB_VOLUME_LOCKED);

                Ext2ClearVpbFlag(Vcb->Vpb, VPB_LOCKED);
            }

			Status = STATUS_SUCCESS;
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
                     IrpContext->IsSynchronous
                     ))
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
		Irp = IrpContext->Irp;

		ExAcquireResourceExclusiveLite(&Fcb->CountResource, TRUE);
		Fcb->OpenHandleCount--;
		ExReleaseResourceForThreadLite(
				&Fcb->CountResource,
				ExGetCurrentResourceThread());

		ExAcquireResourceExclusiveLite(&Vcb->CountResource, TRUE);
		Vcb->OpenFileHandleCount--;
		ExReleaseResourceForThreadLite(
				&Vcb->CountResource,
				ExGetCurrentResourceThread());
	
		if (!FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
		{
			if (FileObject->PrivateCacheMap)
			{
				CcUninitializeCacheMap(FileObject,
				(PLARGE_INTEGER)(&(Fcb->CommonFCBHeader.FileSize)),
				NULL );
			}
		}
		
        if (FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
        {
            FsRtlNotifyCleanup(
                Vcb->NotifySync,
                &Vcb->NotifyList,
                FileObject->FsContext2
                );
        }
        else
        {
            //
            // Drop any byte range locks this process may have on the file.
            //
            FsRtlFastUnlockAll(
                &Fcb->FileLockAnchor,
                FileObject,
                IoGetRequestorProcess(Irp),
                NULL  );

            //
            // If there are no byte range locks owned by other processes on the
            // file the fast I/O read/write functions doesn't have to check for
            // locks so we set IsFastIoPossible to FastIoIsPossible again.
            //
            if (!FsRtlGetNextFileLock(&Fcb->FileLockAnchor, TRUE))
            {
                if (Fcb->CommonFCBHeader.IsFastIoPossible != FastIoIsPossible)
                {
#if DBG
                    KdPrint((
                        DRIVER_NAME ": %-16.16s %-31s %s\n",
                        Ext2GetCurrentProcessName(),
                        "FastIoIsPossible",
                        Fcb->AnsiFileName.Buffer
                        ));
#endif
                    Fcb->CommonFCBHeader.IsFastIoPossible = FastIoIsPossible;
                }
            }
        }


#ifndef EXT2_RO
		
		IoRemoveShareAccess(FileObject, &Fcb->ShareAccess);
		
#endif

		KdPrint(("Ext2Cleanup: OpenCount: %u ReferCount: %-7u %s\n",
			Fcb->OpenHandleCount,
			Fcb->ReferenceCount,
			Fcb->AnsiFileName.Buffer ));

		Status = STATUS_SUCCESS;
	}

	__finally
	{
		if (IrpContext->FileObject)
		{
			SetFlag(IrpContext->FileObject->Flags, FO_CLEANUP_COMPLETE);
    	}
		
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
				ExGetCurrentResourceThread());
		}
		
		if (!IrpContext->ExceptionInProgress)
		{
			if (Status == STATUS_PENDING)
			{
				Ext2QueueRequest(IrpContext);
			}
			else
			{
				IrpContext->Irp->IoStatus.Status = Status;
				
				Ext2CompleteRequest(
					IrpContext->Irp,
					(CCHAR)
					(NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT));
				
				Ext2FreeIrpContext(IrpContext);
			}
		}
	}
	
	return Status;
}
