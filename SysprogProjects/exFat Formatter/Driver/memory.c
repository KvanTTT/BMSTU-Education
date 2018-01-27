/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             memory.c
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
#pragma alloc_text(PAGE, Ext2AllocateIrpContext)
#pragma alloc_text(PAGE, Ext2FreeIrpContext)
#pragma alloc_text(PAGE, Ext2AllocateFcb)
#pragma alloc_text(PAGE, Ext2FreeFcb)
#pragma alloc_text(PAGE, Ext2FreeCcb)
#pragma alloc_text(PAGE, Ext2AllocateCcb)
#pragma alloc_text(PAGE, Ext2FreeVcb)
#endif


PEXT2_IRP_CONTEXT
Ext2AllocateIrpContext (IN PDEVICE_OBJECT   DeviceObject,
			IN PIRP             Irp )
{
	PIO_STACK_LOCATION  IoStackLocation;
	PEXT2_IRP_CONTEXT    IrpContext;
	
	ASSERT(DeviceObject != NULL);
	ASSERT(Irp != NULL);
	
	IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
	
    IrpContext = (PEXT2_IRP_CONTEXT) (ExAllocateFromNPagedLookasideList( &(Ext2Global->Ext2IrpContextLookasideList)));

    if (IrpContext == NULL) {

        IrpContext = ExAllocatePool( NonPagedPool, sizeof(EXT2_IRP_CONTEXT) );

        //
        //  Zero out the irp context and indicate that it is from pool and
        //  not region allocated
        //

        RtlZeroMemory(IrpContext, sizeof(EXT2_IRP_CONTEXT));

        SetFlag(IrpContext->Flags, IRP_CONTEXT_FLAG_FROM_POOL);

    } else {

        //
        //  Zero out the irp context and indicate that it is from zone and
        //  not pool allocated
        //

        RtlZeroMemory(IrpContext, sizeof(EXT2_IRP_CONTEXT) );
    }
	
	if (!IrpContext)
	{
		return NULL;
	}
	
	IrpContext->Identifier.Type = ICX;
	IrpContext->Identifier.Size = sizeof(EXT2_IRP_CONTEXT);
	
	IrpContext->Irp = Irp;
	
	IrpContext->MajorFunction = IoStackLocation->MajorFunction;
	IrpContext->MinorFunction = IoStackLocation->MinorFunction;
	
	IrpContext->DeviceObject = DeviceObject;
	
	IrpContext->FileObject = IoStackLocation->FileObject;
	
	if (IrpContext->MajorFunction == IRP_MJ_FILE_SYSTEM_CONTROL ||
		IrpContext->MajorFunction == IRP_MJ_DEVICE_CONTROL ||
		IrpContext->MajorFunction == IRP_MJ_SHUTDOWN)
	{
		IrpContext->IsSynchronous = TRUE;
	}
	else if (IrpContext->MajorFunction == IRP_MJ_CLEANUP ||
		IrpContext->MajorFunction == IRP_MJ_CLOSE)
	{
		IrpContext->IsSynchronous = FALSE;
	}
	else
	{
		IrpContext->IsSynchronous = IoIsOperationSynchronous(Irp);
	}
	
	//
	// Temporary workaround for a bug in close that makes it reference a
	// fileobject when it is no longer valid.
	//
	if (IrpContext->MajorFunction == IRP_MJ_CLOSE)
	{
		IrpContext->IsSynchronous = TRUE;
	}
	
	IrpContext->IsTopLevel = (IoGetTopLevelIrp() == Irp);
	
	IrpContext->ExceptionInProgress = FALSE;
	
	return IrpContext;
}

VOID
Ext2FreeIrpContext (IN PEXT2_IRP_CONTEXT IrpContext)
{
	ASSERT(IrpContext != NULL);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
    //  Return the Irp context record to the region or to pool depending on
    //  its flag

    if (FlagOn(IrpContext->Flags, IRP_CONTEXT_FLAG_FROM_POOL)) {

        ExFreePool( IrpContext );

    } else {

        ExFreeToNPagedLookasideList(&(Ext2Global->Ext2IrpContextLookasideList), IrpContext);
    }
}

PEXT2_FCB
Ext2AllocateFcb (IN PEXT2_VCB   Vcb,
		 IN PUNICODE_STRING     FileName,
		 IN ULONG               IndexNumber,
		 IN ULONG				inode,
		 IN ULONG				dir_inode,
		 IN struct ext2_inode*  ext2_inode )
{
	PEXT2_FCB Fcb;
	PEXT2_FCB ParentFcb;
	
	Fcb = (PEXT2_FCB)ExAllocatePool(NonPagedPool, sizeof(EXT2_FCB));

	if (!Fcb)
	{
		return NULL;
	}
	
	RtlZeroMemory(Fcb, sizeof(EXT2_FCB));
	
	Fcb->Identifier.Type = FCB;
	Fcb->Identifier.Size = sizeof(EXT2_FCB);

	FsRtlInitializeFileLock (
		&Fcb->FileLockAnchor,
		NULL,
		NULL );
	
	Fcb->OpenHandleCount = 0;
	Fcb->ReferenceCount = 0;
	
	Fcb->FileName.Length = FileName->Length;

	Fcb->FileName.MaximumLength = Fcb->FileName.Length + sizeof(WCHAR);
	
	Fcb->FileName.Buffer = (PWSTR) ExAllocatePool(
		NonPagedPool,
		Fcb->FileName.MaximumLength	);
	
	if (!Fcb->FileName.Buffer)
	{
		ExFreePool(Fcb);
		return NULL;
	}

	RtlZeroMemory(Fcb->FileName.Buffer, Fcb->FileName.MaximumLength);
	
	RtlCopyUnicodeString(&Fcb->FileName, FileName );

	
    Fcb->AnsiFileName.Length = Fcb->FileName.Length / sizeof(WCHAR);

    Fcb->AnsiFileName.MaximumLength = Fcb->FileName.Length / sizeof(WCHAR) + 1;

    Fcb->AnsiFileName.Buffer = (PUCHAR) ExAllocatePool(
        NonPagedPool,
        Fcb->FileName.Length / sizeof(WCHAR) + 1   );

    if (!Fcb->AnsiFileName.Buffer)
    {
        ExFreePool(Fcb->FileName.Buffer);
        ExFreePool(Fcb);
        return NULL;
    }

    Ext2WcharToChar(
        Fcb->AnsiFileName.Buffer,
        Fcb->FileName.Buffer,
        Fcb->FileName.Length / sizeof(WCHAR)
        );

    Fcb->AnsiFileName.Buffer[Fcb->FileName.Length / sizeof(WCHAR)] = 0;
	
	Fcb->FileAttributes = FILE_ATTRIBUTE_NORMAL;
	
	if (S_ISDIR(ext2_inode->i_mode))
	{
		SetFlag(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY);
	}

#ifndef EXT2_RO
    if (FlagOn(Vcb->Flags, VCB_READ_ONLY))
#endif
    {
        SetFlag(Fcb->FileAttributes, FILE_ATTRIBUTE_READONLY);
    }
	
	Fcb->IndexNumber.QuadPart = IndexNumber;

	Fcb->Flags = 0;
	
	Fcb->ext2_inode = ext2_inode;
	Fcb->inode = inode;
	Fcb->dir_inode = dir_inode;
	
	RtlZeroMemory(&Fcb->CommonFCBHeader, sizeof(FSRTL_COMMON_FCB_HEADER));
	
    Fcb->CommonFCBHeader.NodeTypeCode = (USHORT) FCB;
    Fcb->CommonFCBHeader.NodeByteSize = sizeof(EXT2_FCB);
	Fcb->CommonFCBHeader.IsFastIoPossible = FastIoIsNotPossible;
	Fcb->CommonFCBHeader.Resource = &(Fcb->MainResource);
	Fcb->CommonFCBHeader.PagingIoResource = &(Fcb->PagingIoResource);
	Fcb->CommonFCBHeader.AllocationSize.QuadPart = (LONGLONG)(Fcb->ext2_inode->i_size);
	Fcb->CommonFCBHeader.FileSize.QuadPart = (LONGLONG)(Fcb->ext2_inode->i_size);
	Fcb->CommonFCBHeader.ValidDataLength.QuadPart = (LONGLONG)(Fcb->ext2_inode->i_size);
	
	Fcb->SectionObject.DataSectionObject = NULL;
	Fcb->SectionObject.SharedCacheMap = NULL;
	Fcb->SectionObject.ImageSectionObject = NULL;

	if (FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
	{
		Fcb->StreamObj = IoCreateStreamFileObject( NULL, Vcb->TargetDeviceObject);

		if (Fcb->StreamObj)
		{
			Fcb->StreamObj->SectionObjectPointer = &(Fcb->SectionObject);
			Fcb->StreamObj->ReadAccess = TRUE;
			Fcb->StreamObj->WriteAccess = FALSE;
			Fcb->StreamObj->DeleteAccess = FALSE;
			Fcb->StreamObj->FsContext = (PVOID) Fcb;
			Fcb->StreamObj->FsContext2 = NULL;
			Fcb->StreamObj->Vpb = Vcb->Vpb;
		}
		else
		{
			ExFreePool(Fcb->AnsiFileName.Buffer);
			ExFreePool(Fcb->FileName.Buffer);
			ExFreePool(Fcb);
			return NULL;
		}
		
		{
			CC_FILE_SIZES FileSizes;
			
			FileSizes.AllocationSize.QuadPart =
				FileSizes.FileSize.QuadPart =
				Fcb->CommonFCBHeader.AllocationSize.QuadPart;
			
			FileSizes.ValidDataLength.QuadPart= (LONGLONG)(0x7fffffffffffffff);
			
			CcInitializeCacheMap( Fcb->StreamObj,
				&FileSizes,
				TRUE,
				&(Ext2Global->CacheManagerCallbacks),
				Fcb );
		}

	}
	else
	{
		Fcb->StreamObj = NULL;
	}
	
	ExInitializeResourceLite(&(Fcb->MainResource));
	ExInitializeResourceLite(&(Fcb->PagingIoResource));

	ExInitializeResourceLite(&(Fcb->CountResource));

	InsertTailList(&Vcb->FcbList, &Fcb->Next);

	Fcb->ParentFcb = NULL;

	if (dir_inode != inode)
	{
		ParentFcb = Ext2SearchMcb(Vcb, dir_inode);

		if (ParentFcb)
		{
			Fcb->ParentFcb = ParentFcb;
			ExAcquireResourceExclusiveLite(&ParentFcb->CountResource, TRUE);
			ParentFcb->ReferenceCount++;
			ExReleaseResourceForThreadLite(
					&ParentFcb->CountResource,
					ExGetCurrentResourceThread());

		}
	}
	
	return Fcb;
}

VOID
Ext2FreeFcb (IN PEXT2_FCB Fcb)
{
	ASSERT(Fcb != NULL);
	
	ASSERT((Fcb->Identifier.Type == FCB) &&
		(Fcb->Identifier.Size == sizeof(EXT2_FCB)));

	FsRtlUninitializeFileLock(&Fcb->FileLockAnchor);

	ExDeleteResourceLite(&Fcb->CountResource);

	ExDeleteResourceLite(&Fcb->MainResource);
	
	ExDeleteResourceLite(&Fcb->PagingIoResource);
	
	RemoveEntryList(&Fcb->Next);

	if (FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
	{
		if (Fcb->StreamObj)
		{
			if (Fcb->StreamObj->PrivateCacheMap)
			{
				CcUninitializeCacheMap(Fcb->StreamObj,
				(PLARGE_INTEGER)(&(Fcb->CommonFCBHeader.FileSize)),
				NULL );
			}

			ObDereferenceObject(Fcb->StreamObj);
			Fcb->StreamObj = NULL;
		}
	}
	
	ExFreePool(Fcb->FileName.Buffer);
	ExFreePool(Fcb->AnsiFileName.Buffer);

	ExFreePool(Fcb->ext2_inode);

	if ( Fcb->ParentFcb )
	{
		ASSERT((Fcb->ParentFcb->Identifier.Type == FCB) &&
			(Fcb->ParentFcb->Identifier.Size == sizeof(EXT2_FCB)));
		
		ExAcquireResourceExclusiveLite(&Fcb->ParentFcb->CountResource, TRUE);
		Fcb->ParentFcb->ReferenceCount -- ;
		ExReleaseResourceForThreadLite(
			&Fcb->ParentFcb->CountResource,
			ExGetCurrentResourceThread());
		
		if (!Fcb->ParentFcb->ReferenceCount)
			Ext2FreeFcb(Fcb->ParentFcb);
	}

	ExFreePool(Fcb);
}

PEXT2_CCB
Ext2AllocateCcb (VOID)
{
	PEXT2_CCB Ccb;
	
	Ccb = (PEXT2_CCB)ExAllocatePool(NonPagedPool, sizeof(EXT2_CCB));
	
	if (!Ccb)
	{
		return NULL;
	}
	
	Ccb->Identifier.Type = CCB;
	Ccb->Identifier.Size = sizeof(EXT2_CCB);
	
	Ccb->CurrentByteOffset = 0;
	
	Ccb->DirectorySearchPattern.Length = 0;
	Ccb->DirectorySearchPattern.MaximumLength = 0;
	Ccb->DirectorySearchPattern.Buffer = 0;
	
	return Ccb;
}

VOID
Ext2FreeCcb (IN PEXT2_CCB Ccb)
{
	ASSERT(Ccb != NULL);
	
	ASSERT((Ccb->Identifier.Type == CCB) &&
		(Ccb->Identifier.Size == sizeof(EXT2_CCB)));
	
	if (Ccb->DirectorySearchPattern.Buffer != NULL)
	{
		ExFreePool(Ccb->DirectorySearchPattern.Buffer);
	}
	
	ExFreePool(Ccb);
}

VOID
Ext2FreeVcb (IN PEXT2_VCB Vcb )
{
	ASSERT(Vcb != NULL);
	
	ASSERT((Vcb->Identifier.Type == VCB) &&
		(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
	
	Ext2ClearVpbFlag(Vcb->Vpb, VPB_MOUNTED);

	FsRtlNotifyUninitializeSync(&Vcb->NotifySync);

	ExAcquireResourceExclusiveLite(
		&Ext2Global->Resource,
		TRUE );

	if (Vcb->StreamObj)
	{
		if (Vcb->StreamObj->PrivateCacheMap)
			Ext2SyncUninitializeCacheMap(Vcb->StreamObj);

		ObDereferenceObject(Vcb->StreamObj);
		Vcb->StreamObj = NULL;
	}
	
	RemoveEntryList(&Vcb->Next);
	
	ExReleaseResourceForThreadLite(
		&Ext2Global->Resource,
		ExGetCurrentResourceThread() );
	
	ExDeleteResourceLite(&Vcb->MainResource);
	
	ExDeleteResourceLite(&Vcb->PagingIoResource);
	
	if (Vcb->ext2_super_block)
		ExFreePool(Vcb->ext2_super_block);

	if (Vcb->ext2_group_desc)
		ExFreePool(Vcb->ext2_group_desc);
	
	IoDeleteDevice(Vcb->DeviceObject);
}
