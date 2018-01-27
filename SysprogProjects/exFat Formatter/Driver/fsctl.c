/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             fsctl.c
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
#pragma alloc_text(PAGE, Ext2SetVpbFlag)
#pragma alloc_text(PAGE, Ext2ClearVpbFlag)
#pragma alloc_text(PAGE, Ext2LockVolume)
#pragma alloc_text(PAGE, Ext2UnlockVolume)
#pragma alloc_text(PAGE, Ext2UserFsRequest)
#pragma alloc_text(PAGE, Ext2MountVolume)
#pragma alloc_text(PAGE, Ext2PurgeVolume)
#pragma alloc_text(PAGE, Ext2PurgeFile)
#pragma alloc_text(PAGE, Ext2DismountVolume)
#pragma alloc_text(PAGE, Ext2IsVolumeMounted)
#pragma alloc_text(PAGE, Ext2VerifyVolume)
#pragma alloc_text(PAGE, Ext2FileSystemControl)
#endif


VOID
Ext2SetVpbFlag (IN PVPB     Vpb,
		IN USHORT   Flag )
{
	KIRQL OldIrql;
	
	IoAcquireVpbSpinLock(&OldIrql);
	
	Vpb->Flags |= Flag;
	
	IoReleaseVpbSpinLock(OldIrql);
}

VOID
Ext2ClearVpbFlag (IN PVPB     Vpb,
		  IN USHORT   Flag )
{
	KIRQL OldIrql;
	
	IoAcquireVpbSpinLock(&OldIrql);
	
	Vpb->Flags &= ~Flag;
	
	IoReleaseVpbSpinLock(OldIrql);
}



NTSTATUS
Ext2LockVolume (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	NTSTATUS        Status = STATUS_UNSUCCESSFUL;
	PEXT2_VCB       Vcb;
	BOOLEAN         VcbResourceAcquired = FALSE;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;
		
		//
		// This request is not allowed on the main device object
		//
		if (DeviceObject == Ext2Global->DeviceObject)
		{
			Status = STATUS_INVALID_DEVICE_REQUEST;
			__leave;
		}
		
		Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
		
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		ExAcquireResourceSharedLite(
			&Vcb->MainResource,
			TRUE			);
		
		VcbResourceAcquired = TRUE;
		
		if (FlagOn(Vcb->Flags, VCB_VOLUME_LOCKED))
		{
			KdPrint(("Ext2LockVolume: Volume is already locked.\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		if (Vcb->OpenFileHandleCount)
		{
			KdPrint(("Ext2LockVolume: Open files exists.\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		ExReleaseResourceForThreadLite(
			&Vcb->MainResource,
			ExGetCurrentResourceThread()
			);
		
		VcbResourceAcquired = FALSE;
		
		Ext2PurgeVolume(Vcb, TRUE);
		
		ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			TRUE
			);
		
		VcbResourceAcquired = TRUE;
		
		if (!IsListEmpty(&Vcb->FcbList))
		{
			KdPrint(("Ext2LockVolume: Could not purge cached files.\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		SetFlag(Vcb->Flags, VCB_VOLUME_LOCKED);
		
		Ext2SetVpbFlag(Vcb->Vpb, VPB_LOCKED);
		
		KdPrint(("Ext2LockVolume: Volume locked.\n"));
		
		Status = STATUS_SUCCESS;
	}
	__finally
	{
		if (VcbResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Vcb->MainResource,
				ExGetCurrentResourceThread());
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

NTSTATUS
Ext2UnlockVolume (
		 IN PEXT2_IRP_CONTEXT IrpContext
		 )
{
	PDEVICE_OBJECT  DeviceObject;
	NTSTATUS        Status = STATUS_UNSUCCESSFUL;
	PEXT2_VCB       Vcb;
	BOOLEAN         VcbResourceAcquired = FALSE;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;
		
		//
		// This request is not allowed on the main device object
		//
		if (DeviceObject == Ext2Global->DeviceObject)
		{
			Status = STATUS_INVALID_DEVICE_REQUEST;
			__leave;
		}
		
		Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
		
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			TRUE );
		
		VcbResourceAcquired = TRUE;
		
		if (!FlagOn(Vcb->Flags, VCB_VOLUME_LOCKED))
		{
			KdPrint((": Ext2UnlockVolume: Volume is not locked .\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		ClearFlag(Vcb->Flags, VCB_VOLUME_LOCKED);
		
		Ext2ClearVpbFlag(Vcb->Vpb, VPB_LOCKED);
		
		KdPrint(("Ext2UnlockVolume: Volume unlocked.\n"));
		
		Status = STATUS_SUCCESS;
	}
	__finally
	{
		if (VcbResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Vcb->MainResource,
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


NTSTATUS
Ext2UserFsRequest (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PIRP                Irp;
	PIO_STACK_LOCATION  IoStackLocation;
	ULONG               FsControlCode;
	NTSTATUS            Status;
	
	ASSERT(IrpContext);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
	Irp = IrpContext->Irp;
	
	IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
	
#ifndef _GNU_NTIFS_
	FsControlCode =
		IoStackLocation->Parameters.FileSystemControl.FsControlCode;
#else
	FsControlCode = ((PEXTENDED_IO_STACK_LOCATION)
		IoStackLocation)->Parameters.FileSystemControl.FsControlCode;
#endif
	
	switch (FsControlCode)
	{
	case FSCTL_LOCK_VOLUME:
		Status = Ext2LockVolume(IrpContext);
		break;
		
	case FSCTL_UNLOCK_VOLUME:
		Status = Ext2UnlockVolume(IrpContext);
		break;
		
	case FSCTL_DISMOUNT_VOLUME:
		Status = Ext2DismountVolume(IrpContext);
		break;
		
	case FSCTL_IS_VOLUME_MOUNTED:
		Status = Ext2IsVolumeMounted(IrpContext);
		break;
		
	default:
		Status = STATUS_INVALID_DEVICE_REQUEST;
		IrpContext->Irp->IoStatus.Status = Status;
		Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
		Ext2FreeIrpContext(IrpContext);
	}
	
	return Status;
}

NTSTATUS
Ext2MountVolume (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT              MainDeviceObject;
	BOOLEAN                     GlobalDataResourceAcquired = FALSE;
	PIRP                        Irp;
	PIO_STACK_LOCATION          IoStackLocation;
	PDEVICE_OBJECT              TargetDeviceObject;
	NTSTATUS                    Status = STATUS_UNSUCCESSFUL;
	PDEVICE_OBJECT              VolumeDeviceObject = NULL;
	PEXT2_VCB                   Vcb;
	BOOLEAN                     VcbResourceInitialized = FALSE;
	struct ext2_super_block*    ext2_super_block = NULL;
	struct ext2_group_desc *    ext2_group_desc = NULL;
	USHORT                      VolumeLabelLength;
	ULONG                       IoctlSize;
	BOOLEAN						NotifySyncInitialized = FALSE;
	LONGLONG					DiskSize;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		MainDeviceObject = IrpContext->DeviceObject;

		//
		//  Make sure we can wait.
		//

		SetFlag(IrpContext->Flags, IRP_CONTEXT_FLAG_WAIT);

		//
		// This request is only allowed on the main device object
		//
		if (MainDeviceObject != Ext2Global->DeviceObject)
		{
			Status = STATUS_INVALID_DEVICE_REQUEST;
			__leave;
		}
		
		ExAcquireResourceExclusiveLite(
			&(Ext2Global->Resource),
			TRUE );
		
		GlobalDataResourceAcquired = TRUE;
		
		if (FlagOn(Ext2Global->Flags, EXT2_UNLOAD_PENDING))
		{
			Status = STATUS_UNRECOGNIZED_VOLUME;
			__leave;
		}
		
		Irp = IrpContext->Irp;
		
		IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
		
		TargetDeviceObject =
			IoStackLocation->Parameters.MountVolume.DeviceObject;
		
		ext2_super_block = Ext2LoadSuper(TargetDeviceObject);

		Status = STATUS_UNSUCCESSFUL;
		if (ext2_super_block)
		{
			if (ext2_super_block->s_magic == EXT2_SUPER_MAGIC)
			{
				KdPrint(("Ext2Fsd: Ext2fs is found.\n"));
				Status = STATUS_SUCCESS;
			}
		}

		if (!NT_SUCCESS(Status))
		{
			__leave;
		}
		
		Status = IoCreateDevice(
			MainDeviceObject->DriverObject,
			sizeof(EXT2_VCB),
			NULL,
			FILE_DEVICE_DISK_FILE_SYSTEM,
			0,
			FALSE,
			&VolumeDeviceObject );
		
		if (!NT_SUCCESS(Status))
		{
			__leave;
		}
		
		VolumeDeviceObject->StackSize = (CCHAR)(TargetDeviceObject->StackSize + 1);

        ClearFlag(VolumeDeviceObject->Flags, DO_DEVICE_INITIALIZING);

        if (TargetDeviceObject->AlignmentRequirement > VolumeDeviceObject->AlignmentRequirement) {

            VolumeDeviceObject->AlignmentRequirement = TargetDeviceObject->AlignmentRequirement;
        }

		(IoStackLocation->Parameters.MountVolume.Vpb)->DeviceObject =
			VolumeDeviceObject;
		
		Vcb = (PEXT2_VCB) VolumeDeviceObject->DeviceExtension;
		
		RtlZeroMemory(Vcb, sizeof(EXT2_VCB));
		
		Vcb->Identifier.Type = VCB;
		Vcb->Identifier.Size = sizeof(EXT2_VCB);

		Vcb->Vpb = IoStackLocation->Parameters.MountVolume.Vpb;
		Vcb->StreamObj = IoCreateStreamFileObject( NULL, Vcb->Vpb->RealDevice);

		if (Vcb->StreamObj)
		{
			Vcb->StreamObj->SectionObjectPointer = &(Vcb->SectionObject);
			Vcb->StreamObj->Vpb = Vcb->Vpb;
			Vcb->StreamObj->ReadAccess = TRUE;
			Vcb->StreamObj->WriteAccess = FALSE;
			Vcb->StreamObj->DeleteAccess = FALSE;
			Vcb->StreamObj->FsContext = (PVOID) Vcb;
			Vcb->StreamObj->FsContext2 = NULL;
			Vcb->StreamObj->Vpb = Vcb->Vpb;
		}
		else
		{
			__leave;
		}


#ifndef EXT2_RO
		Vcb->ReadOnly = TRUE;
#endif
		
		ExInitializeResourceLite(&Vcb->MainResource);
		ExInitializeResourceLite(&Vcb->PagingIoResource);

		ExInitializeResourceLite(&Vcb->CountResource);
		
		VcbResourceInitialized = TRUE;
		
		Vcb->Vpb = IoStackLocation->Parameters.MountVolume.Vpb;
		
		InitializeListHead(&Vcb->FcbList);

        InitializeListHead(&Vcb->NotifyList);

        FsRtlNotifyInitializeSync(&Vcb->NotifySync);

        NotifySyncInitialized = TRUE;

		Vcb->DeviceObject = VolumeDeviceObject;
		
		Vcb->TargetDeviceObject = TargetDeviceObject;
		
		Vcb->OpenFileHandleCount = 0;
		
		Vcb->ReferenceCount = 0;
		
		Vcb->Flags = 0;
		
		Vcb->ext2_super_block = ext2_super_block;
		
		VolumeLabelLength = 0;
		
		if (VolumeLabelLength > MAXIMUM_VOLUME_LABEL_LENGTH / 2)
		{
			VolumeLabelLength = MAXIMUM_VOLUME_LABEL_LENGTH / 2;
		}
		
		Vcb->Vpb->VolumeLabelLength = VolumeLabelLength * 2;

		Vcb->CommonFCBHeader.NodeTypeCode = (USHORT) VCB;
		Vcb->CommonFCBHeader.NodeByteSize = sizeof(EXT2_VCB);
		Vcb->CommonFCBHeader.IsFastIoPossible = FastIoIsNotPossible;
		Vcb->CommonFCBHeader.Resource = &(Vcb->MainResource);
		Vcb->CommonFCBHeader.PagingIoResource = &(Vcb->PagingIoResource);

		Vcb->Vpb->SerialNumber = 'MATT';

		IoctlSize = sizeof(DISK_GEOMETRY);
		
		Status = Ext2DiskIoControl(
			TargetDeviceObject,
			IOCTL_DISK_GET_DRIVE_GEOMETRY,
			NULL,
			0,
			&Vcb->DiskGeometry,
			&IoctlSize );
		
		if (!NT_SUCCESS(Status))
		{
			__leave;
		}
		
		DiskSize =
			Vcb->DiskGeometry.Cylinders.QuadPart *
			Vcb->DiskGeometry.TracksPerCylinder *
			Vcb->DiskGeometry.SectorsPerTrack *
			Vcb->DiskGeometry.BytesPerSector;

		IoctlSize = sizeof(PARTITION_INFORMATION);
		
		Status = Ext2DiskIoControl(
			TargetDeviceObject,
			IOCTL_DISK_GET_PARTITION_INFO,
			NULL,
			0,
			&Vcb->PartitionInformation,
			&IoctlSize );
		
		if (!NT_SUCCESS(Status))
		{
			Vcb->PartitionInformation.StartingOffset.QuadPart = 0;
			
			Vcb->PartitionInformation.PartitionLength.QuadPart =
				DiskSize;
			
			Status = STATUS_SUCCESS;
		}

		Vcb->CommonFCBHeader.AllocationSize.QuadPart =
		Vcb->CommonFCBHeader.FileSize.QuadPart = DiskSize;

		Vcb->CommonFCBHeader.ValidDataLength.QuadPart = 
			(LONGLONG)(0x7fffffffffffffff);
/*
		Vcb->CommonFCBHeader.AllocationSize.QuadPart = (LONGLONG)(ext2_super_block->s_blocks_count - ext2_super_block->s_free_blocks_count)
			* (EXT2_MIN_BLOCK << ext2_super_block->s_log_block_size);
		Vcb->CommonFCBHeader.FileSize.QuadPart = Vcb->CommonFCBHeader.AllocationSize.QuadPart;
		Vcb->CommonFCBHeader.ValidDataLength.QuadPart = Vcb->CommonFCBHeader.AllocationSize.QuadPart;
*/
		{
				CC_FILE_SIZES FileSizes;

				FileSizes.AllocationSize.QuadPart =
				FileSizes.FileSize.QuadPart =
					Vcb->CommonFCBHeader.AllocationSize.QuadPart;

				FileSizes.ValidDataLength.QuadPart= (LONGLONG)(0x7fffffffffffffff);

				CcInitializeCacheMap( Vcb->StreamObj,
									  &FileSizes,
									  TRUE,
									  &(Ext2Global->CacheManagerNoOpCallbacks),
									  Vcb );
		}

/*		
		Ext2CharToWchar(
			Vcb->Vpb->VolumeLabel,
			Vcb->romfs_super_block->name,
			VolumeLabelLength
			);
*/
		
		ext2_group_desc = Ext2LoadGroup(Vcb); 

		if (!ext2_group_desc)
		{
			Status = STATUS_UNSUCCESSFUL;
			__leave;
		}

		Vcb->ext2_group_desc = ext2_group_desc;

        //
        //  Set the removable media and floppy flag based on the real device's
        //  characteristics.
        //
/*
        if (FlagOn(Vcb->Vpb->RealDevice->Characteristics, FILE_REMOVABLE_MEDIA)) {

            SetFlag( Vcb->VcbState, VCB_STATE_FLAG_REMOVABLE_MEDIA );
        }

        if (FlagOn(Vcb->Vpb->RealDevice->Characteristics, FILE_FLOPPY_DISKETTE)) {

            SetFlag( Vcb->VcbState, VCB_STATE_FLAG_FLOPPY );
        }
*/
        Vcb->VcbCondition = VcbGood;


		InsertTailList(&(Ext2Global->VcbList), &Vcb->Next);
    }

    __finally
    {
	    if (GlobalDataResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Ext2Global->Resource,
			    ExGetCurrentResourceThread() );
	    }
	    
	    if (!NT_SUCCESS(Status))
	    {
            if (NotifySyncInitialized)
            {
                FsRtlNotifyUninitializeSync(&Vcb->NotifySync);
            }

		    if (ext2_super_block)
		    {
			    ExFreePool(ext2_super_block);
		    }
		    
		    if (VcbResourceInitialized)
		    {
			    ExDeleteResourceLite(&Vcb->MainResource);
			    ExDeleteResourceLite(&Vcb->PagingIoResource);
		    }
		    
		    if (VolumeDeviceObject)
		    {
			    IoDeleteDevice(VolumeDeviceObject);
		    }
	    }
	    
	    if (!IrpContext->ExceptionInProgress)
	    {
		    if (NT_SUCCESS(Status))
		    {
			    ClearFlag(VolumeDeviceObject->Flags, DO_DEVICE_INITIALIZING);
		    }
		    
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


NTSTATUS
Ext2VerifyVolume (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT          DeviceObject;
	NTSTATUS                Status = STATUS_UNSUCCESSFUL;
	PEXT2_SUPER_BLOCK		ext2_sb = NULL;
	PEXT2_VCB               Vcb;
	BOOLEAN                 VcbResourceAcquired = FALSE;
	PIRP                    Irp;
	PIO_STACK_LOCATION      IoStackLocation;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;
		//
		// This request is not allowed on the main device object
		//
		if (DeviceObject == Ext2Global->DeviceObject)
		{
			Status = STATUS_INVALID_DEVICE_REQUEST;
			__leave;
		}
		
		Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
		
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			TRUE );
		
		VcbResourceAcquired = TRUE;
		
		if (!FlagOn(Vcb->TargetDeviceObject->Flags, DO_VERIFY_VOLUME))
		{
			Status = STATUS_SUCCESS;
			__leave;
		}
		
 		Irp = IrpContext->Irp;

		IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
		
		ext2_sb = Ext2LoadSuper(Vcb->TargetDeviceObject);

		if (ext2_sb && ext2_sb->s_magic == EXT2_SUPER_MAGIC)
		{
			ClearFlag(Vcb->TargetDeviceObject->Flags, DO_VERIFY_VOLUME);
				
			KdPrint(("Ext2VerifyVolume: Volume verify succeeded.\n"));

			Status = STATUS_SUCCESS;

			__leave;
		}
		else
		{
			ExReleaseResourceForThreadLite(
                &Vcb->MainResource,
                ExGetCurrentResourceThread()
                );
			
            VcbResourceAcquired = FALSE;
			
            Ext2PurgeVolume(Vcb, FALSE);
			
            ExAcquireResourceExclusiveLite(
                &Vcb->MainResource,
                TRUE
                );
			
            VcbResourceAcquired = TRUE;
			
            SetFlag(Vcb->Flags, VCB_DISMOUNT_PENDING);
			
            ClearFlag(Vcb->TargetDeviceObject->Flags, DO_VERIFY_VOLUME);
			
            KdPrint(("Ext2VerifyVolume: Volume verify failed\n"));
			
            __leave;
		}

		__leave;
	}

	__finally
	{
		if (ext2_sb)
			ExFreePool(ext2_sb);

		if (VcbResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Vcb->MainResource,
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


NTSTATUS
Ext2IsVolumeMounted (IN PEXT2_IRP_CONTEXT IrpContext)
{
    ASSERT(IrpContext);

    ASSERT((IrpContext->Identifier.Type == ICX) &&
           (IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));

    return Ext2VerifyVolume(IrpContext);
}


NTSTATUS
Ext2DismountVolume (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT  DeviceObject;
	NTSTATUS        Status = STATUS_UNSUCCESSFUL;
	PEXT2_VCB       Vcb;
	BOOLEAN         VcbResourceAcquired = FALSE;
	
	__try
	{
		ASSERT(IrpContext != NULL);
		
		ASSERT((IrpContext->Identifier.Type == ICX) &&
			(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
		
		DeviceObject = IrpContext->DeviceObject;

		//
		// This request is not allowed on the main device object
		//
		if (DeviceObject == Ext2Global->DeviceObject)
		{
			Status = STATUS_INVALID_DEVICE_REQUEST;
			__leave;
		}
		
		Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
		
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			TRUE );
		
		VcbResourceAcquired = TRUE;
		
		if (!FlagOn(Vcb->Flags, VCB_VOLUME_LOCKED))
		{
			KdPrint(("Ext2Dismount: Volume is not locked.\n"));
			
			Status = STATUS_ACCESS_DENIED;
			
			__leave;
		}
		
		SetFlag(Vcb->Flags, VCB_DISMOUNT_PENDING);
		
		KdPrint(("Ext2Dismount: Volume dismount pending.\n"));
		
		Status = STATUS_SUCCESS;
	}
	__finally
	{
		if (VcbResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Vcb->MainResource,
				ExGetCurrentResourceThread()
				);
		}
		
		if (!IrpContext->ExceptionInProgress)
		{
			IrpContext->Irp->IoStatus.Status = Status;
			
			Ext2CompleteRequest(
				IrpContext->Irp,
				(CCHAR)
				(NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT)
				);
			
			Ext2FreeIrpContext(IrpContext);
		}
	}
	
	return Status;
}


VOID
Ext2PurgeVolume (IN PEXT2_VCB Vcb,
		 IN BOOLEAN  FlushBeforePurge )
{
	BOOLEAN         VcbResourceAcquired = FALSE;
	PEXT2_FCB       Fcb;
	LIST_ENTRY      FcbList;
	PLIST_ENTRY     ListEntry;
	PFCB_LIST_ENTRY FcbListEntry;
	
	__try
	{
		ASSERT(Vcb != NULL);
		
		ASSERT((Vcb->Identifier.Type == VCB) &&
			(Vcb->Identifier.Size == sizeof(EXT2_VCB)));
		
		ExAcquireResourceSharedLite(
			&Vcb->MainResource,
			TRUE );
		
		VcbResourceAcquired = TRUE;
		
#ifndef EXT2_RO
		if (FlagOn(Vcb->Flags, VCB_READ_ONLY))
		{
			Flush = FALSE;
		}
#endif
		
		InitializeListHead(&FcbList);
		
		for (
			ListEntry = Vcb->FcbList.Flink;
		ListEntry != &Vcb->FcbList;
		ListEntry = ListEntry->Flink
			)
		{
			Fcb = CONTAINING_RECORD(ListEntry, EXT2_FCB, Next);
			
			ExAcquireResourceExclusiveLite(
				&Fcb->CountResource,
				TRUE );
			
			Fcb->ReferenceCount++;
#if DBG
			KdPrint(("Ext2PurgeVolume: %s refercount=%xh\n", Fcb->AnsiFileName.Buffer, Fcb->ReferenceCount));
#endif	
			
			ExReleaseResourceForThreadLite(
				&Fcb->CountResource,
				ExGetCurrentResourceThread());
			
			FcbListEntry = ExAllocatePool(NonPagedPool, sizeof(FCB_LIST_ENTRY));
			
			FcbListEntry->Fcb = Fcb;
			
			InsertTailList(&FcbList, &FcbListEntry->Next);
		}
		
		ExReleaseResourceForThreadLite(
			&Vcb->MainResource,
			ExGetCurrentResourceThread() );
		
		VcbResourceAcquired = FALSE;
		
		while (!IsListEmpty(&FcbList))
		{
			ListEntry = RemoveHeadList(&FcbList);
			
			FcbListEntry = CONTAINING_RECORD(ListEntry, FCB_LIST_ENTRY, Next);
			
			Fcb = FcbListEntry->Fcb;
			
			Ext2PurgeFile(Fcb, FlushBeforePurge);
			
			if (!Fcb->OpenHandleCount && Fcb->ReferenceCount == 1)
			{
#if DBG
				KdPrint(("Ext2FreeFcb %s.\n", Fcb->AnsiFileName.Buffer));
#endif
				Ext2FreeFcb(Fcb);
			}
			
			ExFreePool(FcbListEntry);
		}
		
		KdPrint(("Ext2PurgeVolume: Volume flushed and purged.\n"));
	}
	__finally
	{
		if (VcbResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Vcb->MainResource,
				ExGetCurrentResourceThread() );
		}
	}
}

VOID
Ext2PurgeFile (IN PEXT2_FCB Fcb,
	       IN BOOLEAN  FlushBeforePurge )
{
	ASSERT(Fcb != NULL);
        
	ASSERT((Fcb->Identifier.Type == FCB) &&
		(Fcb->Identifier.Size == sizeof(EXT2_FCB)));
	
#ifndef EXT2_RO
	if (FlushBeforePurge)
	{
		KdPrint(("Ext2PurgeFile: CcFlushCache on %s.\n", Fcb->AnsiFileName.Buffer));
		
		CcFlushCache(&Fcb->SectionObject, NULL, 0, &IoStatus);
	}
#endif
	
	if (Fcb->SectionObject.ImageSectionObject)
	{
		KdPrint(("Ext2PurgeFile: MmFlushImageSection on %s.\n", Fcb->AnsiFileName.Buffer));
	
		MmFlushImageSection(&Fcb->SectionObject, MmFlushForWrite);
	}
	
	if (Fcb->SectionObject.DataSectionObject)
	{
		KdPrint(("Ext2PurgeFile: CcPurgeCacheSection on %s.\n", Fcb->AnsiFileName.Buffer));
		CcPurgeCacheSection(&Fcb->SectionObject, NULL, 0, FALSE);
	}
}


NTSTATUS
Ext2FileSystemControl (IN PEXT2_IRP_CONTEXT IrpContext)
{
	NTSTATUS	Status;
	
	ASSERT(IrpContext);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
	switch (IrpContext->MinorFunction)
	{
	case IRP_MN_USER_FS_REQUEST:
		Status = Ext2UserFsRequest(IrpContext);
		break;
		
	case IRP_MN_MOUNT_VOLUME:
		Status = Ext2MountVolume(IrpContext);
		break;
		
	case IRP_MN_VERIFY_VOLUME:
		Status = Ext2VerifyVolume(IrpContext);
		break;
		
	default:
		Status = STATUS_INVALID_DEVICE_REQUEST;
		IrpContext->Irp->IoStatus.Status = Status;
		Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
		Ext2FreeIrpContext(IrpContext);
	}
	
	return Status;
}
