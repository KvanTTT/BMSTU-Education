/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             lock.c
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
#pragma alloc_text(PAGE, Ext2LockControl)
#endif

NTSTATUS
Ext2LockControl (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	BOOLEAN         CompleteRequest;
	NTSTATUS        Status = STATUS_UNSUCCESSFUL;
	PFILE_OBJECT    FileObject;
	PEXT2_FCB       Fcb;
	PIRP            Irp;
	
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
		
		FileObject = IrpContext->FileObject;
		
		Fcb = (PEXT2_FCB) FileObject->FsContext;
		
		ASSERT(Fcb != NULL);
		
		if (Fcb->Identifier.Type == VCB)
		{
			CompleteRequest = TRUE;
			Status = STATUS_INVALID_PARAMETER;
			__leave;
		}
		
		ASSERT((Fcb->Identifier.Type == FCB) &&
			(Fcb->Identifier.Size == sizeof(EXT2_FCB)));
		
		if (FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
		{
			CompleteRequest = TRUE;
			Status = STATUS_INVALID_PARAMETER;
			__leave;
		}
		
		Irp = IrpContext->Irp;
		
		//
		// While the file has any byte range locks we set IsFastIoPossible to
		// FastIoIsQuestionable so that the FastIoCheckIfPossible function is
		// called to check the locks for any fast I/O read/write operation.
		//
		if (Fcb->CommonFCBHeader.IsFastIoPossible != FastIoIsQuestionable)
		{
#if DBG
			KdPrint(("Ext2LockControl: %-16.16s %-31s %s\n",
				Ext2GetCurrentProcessName(),
				"FastIoIsQuestionable",
				Fcb->AnsiFileName.Buffer
				));
#endif			
			Fcb->CommonFCBHeader.IsFastIoPossible = FastIoIsQuestionable;
		}
		
		//
		// FsRtlProcessFileLock acquires FileObject->FsContext->Resource while
		// modifying the file locks and calls IoCompleteRequest when it's done.
		//
		
		CompleteRequest = FALSE;
		
		Status = FsRtlProcessFileLock(
			&Fcb->FileLockAnchor,
			Irp,
			NULL		);
		
		if (Status != STATUS_SUCCESS)
		{
#if DBG
			KdPrint(("Ext2LockControl: %-16.16s %-31s *** Status: %s (%#x) ***\n",
				Ext2GetCurrentProcessName(),
				"IRP_MJ_LOCK_CONTROL",
				Ext2NtStatusToString(Status),
				Status			));
#endif
		}
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
