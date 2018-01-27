/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             volinfo.c
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
#pragma alloc_text(PAGE, Ext2QueryVolumeInformation)
#endif


NTSTATUS
Ext2QueryVolumeInformation (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT          DeviceObject;
	NTSTATUS                Status = STATUS_UNSUCCESSFUL;
	PEXT2_VCB               Vcb;
	PIRP                    Irp;
	PIO_STACK_LOCATION      IoStackLocation;
	FS_INFORMATION_CLASS    FsInformationClass;
	ULONG                   Length;
	PVOID                   Buffer;
	BOOLEAN                 VcbResourceAcquired = FALSE;

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
		
		if (!ExAcquireResourceSharedLite(
			&Vcb->MainResource,
			IrpContext->IsSynchronous
			))
		{
			Status = STATUS_PENDING;
			__leave;
		}
		
		VcbResourceAcquired = TRUE;
		
		Irp = IrpContext->Irp;
		
		IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
		
		FsInformationClass =
			IoStackLocation->Parameters.QueryVolume.FsInformationClass;
		
		Length = IoStackLocation->Parameters.QueryVolume.Length;
		
		Buffer = Irp->AssociatedIrp.SystemBuffer;
		
		RtlZeroMemory(Buffer, Length);
		
		switch (FsInformationClass)
		{
		case FileFsVolumeInformation:
			{
				PFILE_FS_VOLUME_INFORMATION FsVolInfo;
				ULONG                       VolumeLabelLength;
				ULONG                       RequiredLength;
				
				if (Length < sizeof(FILE_FS_VOLUME_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FsVolInfo = (PFILE_FS_VOLUME_INFORMATION) Buffer;
				
				FsVolInfo->VolumeCreationTime.QuadPart = 0;
				
				FsVolInfo->VolumeSerialNumber = 'MATT';

				VolumeLabelLength = 1;
				
				FsVolInfo->VolumeLabelLength = VolumeLabelLength * 2;
				
				// I don't know what this means
				FsVolInfo->SupportsObjects = FALSE;

				RequiredLength = sizeof(FILE_FS_VOLUME_INFORMATION)
					+ VolumeLabelLength * 2 - sizeof(WCHAR);
				
				if (Length < RequiredLength)
				{
					Irp->IoStatus.Information =
						sizeof(FILE_FS_VOLUME_INFORMATION);
					Status = STATUS_BUFFER_OVERFLOW;
					__leave;
				}
				RtlZeroBytes(FsVolInfo->VolumeLabel, VolumeLabelLength);
/*
				Ext2CharToWchar(
					FsVolInfo->VolumeLabel,
					"\\",
					VolumeLabelLength );
*/
				Irp->IoStatus.Information = RequiredLength;
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileFsSizeInformation:
			{
				PFILE_FS_SIZE_INFORMATION FsSizeInfo;
				
				if (Length < sizeof(FILE_FS_SIZE_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FsSizeInfo = (PFILE_FS_SIZE_INFORMATION) Buffer;
				
#ifndef EXT2_RO
                if (!FlagOn(Vcb->Flags, VCB_READ_ONLY))
                {
                    FsSizeInfo->TotalAllocationUnits.QuadPart = 
                        Vcb->PartitionInformation.PartitionLength.QuadPart / Vcb->ext2_block;

                    FsSizeInfo->AvailableAllocationUnits.QuadPart = Vcb->ext2_block;
                }
                else
#endif // !EXT2_RO
                {
					FsSizeInfo->TotalAllocationUnits.QuadPart =
						Vcb->ext2_super_block->s_blocks_count;
					
					FsSizeInfo->AvailableAllocationUnits.QuadPart = Vcb->ext2_block;
				}
				
				FsSizeInfo->SectorsPerAllocationUnit =
					Vcb->ext2_block / Vcb->DiskGeometry.BytesPerSector;
				
				FsSizeInfo->BytesPerSector =
					Vcb->DiskGeometry.BytesPerSector;
				
				Irp->IoStatus.Information = sizeof(FILE_FS_SIZE_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileFsDeviceInformation:
			{
				PFILE_FS_DEVICE_INFORMATION FsDevInfo;
				
				if (Length < sizeof(FILE_FS_DEVICE_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FsDevInfo = (PFILE_FS_DEVICE_INFORMATION) Buffer;
				
				FsDevInfo->DeviceType =
					Vcb->TargetDeviceObject->DeviceType;
				
				FsDevInfo->Characteristics =
					Vcb->TargetDeviceObject->Characteristics;
				
#ifndef EXT2_RO
				if (FlagOn(Vcb->Flags, VCB_READ_ONLY))
#endif
				{
					SetFlag(FsDevInfo->Characteristics,
                        FILE_READ_ONLY_DEVICE   );
				}
				
				Irp->IoStatus.Information = sizeof(FILE_FS_DEVICE_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileFsAttributeInformation:
			{
				PFILE_FS_ATTRIBUTE_INFORMATION  FsAttrInfo;
				ULONG                           RequiredLength;
				
				if (Length < sizeof(FILE_FS_ATTRIBUTE_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FsAttrInfo =
					(PFILE_FS_ATTRIBUTE_INFORMATION) Buffer;
				
				FsAttrInfo->FileSystemAttributes =
					FILE_CASE_SENSITIVE_SEARCH | FILE_CASE_PRESERVED_NAMES;
				
				FsAttrInfo->MaximumComponentNameLength = EXT2_NAME_LEN;
				
				FsAttrInfo->FileSystemNameLength = sizeof(DRIVER_NAME) * 2;
				
				RequiredLength = sizeof(FILE_FS_ATTRIBUTE_INFORMATION) +
					sizeof(DRIVER_NAME) * 2 - sizeof(WCHAR);
				
				if (Length < RequiredLength)
				{
					Irp->IoStatus.Information =
						sizeof(FILE_FS_ATTRIBUTE_INFORMATION);
					Status = STATUS_BUFFER_OVERFLOW;
					__leave;
				}
				
				Ext2CharToWchar(
					FsAttrInfo->FileSystemName,
					DRIVER_NAME,
					sizeof(DRIVER_NAME));
				
				Irp->IoStatus.Information = RequiredLength;
				Status = STATUS_SUCCESS;
				__leave;
			}

#if (_WIN32_WINNT >= 0x0500)

        case FileFsFullSizeInformation:
            {
                PFILE_FS_FULL_SIZE_INFORMATION PFFFSI;

                if (Length < sizeof(FILE_FS_FULL_SIZE_INFORMATION))
                {
                    Status = STATUS_INFO_LENGTH_MISMATCH;
                    __leave;
                }

                PFFFSI = (PFILE_FS_FULL_SIZE_INFORMATION) Buffer;

/*
                typedef struct _FILE_FS_FULL_SIZE_INFORMATION {
                    LARGE_INTEGER   TotalQuotaAllocationUnits;
                    LARGE_INTEGER   AvailableQuotaAllocationUnits;
                    LARGE_INTEGER   AvailableAllocationUnits;
                    ULONG           SectorsPerAllocationUnit;
                    ULONG           BytesPerSector;
                } FILE_FS_FULL_SIZE_INFORMATION, *PFILE_FS_FULL_SIZE_INFORMATION;
*/

#ifndef EXT2_RO
                if (!FlagOn(Vcb->Flags, VCB_READ_ONLY))
                {
                    PFFFSI->TotalQuotaAllocationUnits.QuadPart =
                    PFFFSI->AvailableQuotaAllocationUnits.QuadPart =
                    PFFFSI->AvailableAllocationUnits.QuadPart = 0;
                }
                else
#endif // !EXT2_RO
                {
                    // On a readonly filesystem total size is the size of the
                    // contents and available size is zero
					PFFFSI->TotalAllocationUnits.QuadPart =
                        Vcb->ext2_super_block->s_blocks_count;

                    PFFFSI->ActualAvailableAllocationUnits.QuadPart =
                    PFFFSI->CallerAvailableAllocationUnits.QuadPart =   0;

				/*
                    PFFFSI->TotalQuotaAllocationUnits.QuadPart =
                        Vcb->ext2_super_block->s_blocks_count;

                    PFFFSI->AvailableQuotaAllocationUnits.QuadPart =
                    PFFFSI->AvailableAllocationUnits.QuadPart =   0;*/
                }

                PFFFSI->SectorsPerAllocationUnit =
                    Vcb->ext2_block / Vcb->DiskGeometry.BytesPerSector;

                PFFFSI->BytesPerSector = Vcb->DiskGeometry.BytesPerSector;

                Irp->IoStatus.Information = sizeof(FILE_FS_FULL_SIZE_INFORMATION);
                Status = STATUS_SUCCESS;
                __leave;
            }

#endif // (_WIN32_WINNT >= 0x0500)

		default:
			Status = STATUS_INVALID_INFO_CLASS;
        }
    }

    __finally
    {
	    if (VcbResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Vcb->MainResource,
			    ExGetCurrentResourceThread() );
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

