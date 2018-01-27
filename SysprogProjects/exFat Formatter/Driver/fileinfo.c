/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             fileinfo.c
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
#pragma alloc_text(PAGE, Ext2QueryInformation)
#pragma alloc_text(PAGE, Ext2SetInformation)
#endif


NTSTATUS
Ext2QueryInformation (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT          DeviceObject;
	NTSTATUS                Status = STATUS_UNSUCCESSFUL;
	PFILE_OBJECT            FileObject;
	PEXT2_FCB               Fcb;
	PEXT2_CCB               Ccb;
	PIRP                    Irp;
	PIO_STACK_LOCATION      IoStackLocation;
	FILE_INFORMATION_CLASS  FileInformationClass;
	ULONG                   Length;
	PVOID                   Buffer;
	BOOLEAN                 FcbResourceAcquired = FALSE;
	
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
		
		FileObject = IrpContext->FileObject;
		
		Fcb = (PEXT2_FCB) FileObject->FsContext;
		
		ASSERT(Fcb != NULL);
		
		//
		// This request is not allowed on volumes
		//
		if (Fcb->Identifier.Type == VCB)
		{
			Status = STATUS_INVALID_PARAMETER;
			__leave;
		}
		
		ASSERT((Fcb->Identifier.Type == FCB) &&
			(Fcb->Identifier.Size == sizeof(EXT2_FCB)));
		
#ifndef EXT2_RO
		if (!FlagOn(Fcb->Flags, FCB_PAGE_FILE))
#endif
		{
			if (!ExAcquireResourceSharedLite(
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
		
		ASSERT(Ccb != NULL);
		
		ASSERT((Ccb->Identifier.Type == CCB) &&
			(Ccb->Identifier.Size == sizeof(EXT2_CCB)));
		
		Irp = IrpContext->Irp;
		
		IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
		
		FileInformationClass =
			IoStackLocation->Parameters.QueryFile.FileInformationClass;
		
		Length = IoStackLocation->Parameters.QueryFile.Length;
		
		Buffer = Irp->AssociatedIrp.SystemBuffer;
		
		RtlZeroMemory(Buffer, Length);
		
		switch (FileInformationClass)
		{
		case FileBasicInformation:
			{
				PFILE_BASIC_INFORMATION FileBasicInformation;
				
				if (Length < sizeof(FILE_BASIC_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FileBasicInformation = (PFILE_BASIC_INFORMATION) Buffer;
				
				FileBasicInformation->CreationTime = Ext2SysTime(Fcb->ext2_inode->i_ctime + TIMEZONE);
				
				FileBasicInformation->LastAccessTime = Ext2SysTime(Fcb->ext2_inode->i_atime + TIMEZONE);
				
				FileBasicInformation->LastWriteTime = Ext2SysTime(Fcb->ext2_inode->i_mtime + TIMEZONE);
				
				FileBasicInformation->ChangeTime = Ext2SysTime(Fcb->ext2_inode->i_mtime + TIMEZONE);
				
				FileBasicInformation->FileAttributes = Fcb->FileAttributes;
				
				Irp->IoStatus.Information = sizeof(FILE_BASIC_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}

#if (_WIN32_WINNT >= 0x0500)

		case FileAttributeTagInformation:
			{
				PFILE_ATTRIBUTE_TAG_INFORMATION FATI;
				
				if (Length < sizeof(FILE_ATTRIBUTE_TAG_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FATI = (PFILE_ATTRIBUTE_TAG_INFORMATION) Buffer;
				
				FATI->FileAttributes = Fcb->FileAttributes;
				FATI->ReparseTag = 0;
				
				Irp->IoStatus.Information = sizeof(FILE_ATTRIBUTE_TAG_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
#endif // (_WIN32_WINNT >= 0x0500)


		case FileStandardInformation:
			{
				PFILE_STANDARD_INFORMATION FileStandardInformation;
				
				if (Length < sizeof(FILE_STANDARD_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FileStandardInformation = (PFILE_STANDARD_INFORMATION) Buffer;
				
				FileStandardInformation->AllocationSize.QuadPart =
					(LONGLONG)(Fcb->ext2_inode->i_size);
				
				FileStandardInformation->EndOfFile.QuadPart =
					(LONGLONG)(Fcb->ext2_inode->i_size);
				
				FileStandardInformation->NumberOfLinks = Fcb->ext2_inode->i_links_count;
				
#ifndef EXT2_RO
				
				FileStandardInformation->DeletePending = Fcb->DeletePending;
				
#else
				
				FileStandardInformation->DeletePending = FALSE;
				
#endif
				
				if (Fcb->FileAttributes & FILE_ATTRIBUTE_DIRECTORY)
				{
					FileStandardInformation->Directory = TRUE;
				}
				else
				{
					FileStandardInformation->Directory = FALSE;
				}
				
				Irp->IoStatus.Information = sizeof(FILE_STANDARD_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileInternalInformation:
			{
				PFILE_INTERNAL_INFORMATION FileInternalInformation;
				
				if (Length < sizeof(FILE_INTERNAL_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FileInternalInformation = (PFILE_INTERNAL_INFORMATION) Buffer;
				
				// The "inode number"
				FileInternalInformation->IndexNumber = Fcb->IndexNumber;
				
				Irp->IoStatus.Information = sizeof(FILE_INTERNAL_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileEaInformation:
			{
				PFILE_EA_INFORMATION FileEaInformation;
				
				if (Length < sizeof(FILE_EA_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FileEaInformation = (PFILE_EA_INFORMATION) Buffer;
				
				// Romfs doesn't have any extended attributes
				FileEaInformation->EaSize = 0;
				
				Irp->IoStatus.Information = sizeof(FILE_EA_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileNameInformation:
			{
				PFILE_NAME_INFORMATION FileNameInformation;
				
				if (Length < sizeof(FILE_NAME_INFORMATION) +
					Fcb->FileName.Length - sizeof(WCHAR))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FileNameInformation = (PFILE_NAME_INFORMATION) Buffer;
				
				FileNameInformation->FileNameLength = Fcb->FileName.Length;
				
				RtlCopyMemory(
					FileNameInformation->FileName,
					Fcb->FileName.Buffer,
					Fcb->FileName.Length
					);
				
				Irp->IoStatus.Information = sizeof(FILE_NAME_INFORMATION) +
					Fcb->FileName.Length - sizeof(WCHAR);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FilePositionInformation:
			{
				PFILE_POSITION_INFORMATION FilePositionInformation;
				
				if (Length < sizeof(FILE_POSITION_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FilePositionInformation = (PFILE_POSITION_INFORMATION) Buffer;
				
				FilePositionInformation->CurrentByteOffset =
					FileObject->CurrentByteOffset;
				
				Irp->IoStatus.Information = sizeof(FILE_POSITION_INFORMATION);
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		case FileAllInformation:
			{
				PFILE_ALL_INFORMATION       FileAllInformation;
				PFILE_BASIC_INFORMATION     FileBasicInformation;
				PFILE_STANDARD_INFORMATION  FileStandardInformation;
				PFILE_INTERNAL_INFORMATION  FileInternalInformation;
				PFILE_EA_INFORMATION        FileEaInformation;
				PFILE_POSITION_INFORMATION  FilePositionInformation;
				PFILE_NAME_INFORMATION      FileNameInformation;
				
				if (Length < sizeof(FILE_ALL_INFORMATION))
				{
					Status = STATUS_INFO_LENGTH_MISMATCH;
					__leave;
				}
				
				FileAllInformation = (PFILE_ALL_INFORMATION) Buffer;
				
				FileBasicInformation =
					&FileAllInformation->BasicInformation;
				
				FileStandardInformation =
					&FileAllInformation->StandardInformation;
				
				FileInternalInformation =
					&FileAllInformation->InternalInformation;
				
				FileEaInformation =
					&FileAllInformation->EaInformation;
				
				FilePositionInformation =
					&FileAllInformation->PositionInformation;
				
				FileNameInformation =
					&FileAllInformation->NameInformation;
				
				FileBasicInformation->CreationTime = Ext2SysTime(Fcb->ext2_inode->i_ctime + TIMEZONE);
				
				FileBasicInformation->LastAccessTime = Ext2SysTime(Fcb->ext2_inode->i_atime + TIMEZONE);
				
				FileBasicInformation->LastWriteTime = Ext2SysTime(Fcb->ext2_inode->i_mtime + TIMEZONE);
				
				FileBasicInformation->ChangeTime = Ext2SysTime(Fcb->ext2_inode->i_mtime + TIMEZONE);
				
				FileBasicInformation->FileAttributes = Fcb->FileAttributes;
				
				FileStandardInformation->AllocationSize.QuadPart =
					(LONGLONG)(Fcb->ext2_inode->i_size);
				
				FileStandardInformation->EndOfFile.QuadPart =
					(LONGLONG)(Fcb->ext2_inode->i_size);
				
				FileStandardInformation->NumberOfLinks = Fcb->ext2_inode->i_links_count;
#ifndef EXT2_RO
				FileStandardInformation->DeletePending = Fcb->DeletePending;
#else
				FileStandardInformation->DeletePending = FALSE;
#endif
				
				if (FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
				{
					FileStandardInformation->Directory = TRUE;
				}
				else
				{
					FileStandardInformation->Directory = FALSE;
				}
				
				// The "inode number"
				FileInternalInformation->IndexNumber = Fcb->IndexNumber;
				
				// Romfs doesn't have any extended attributes
				FileEaInformation->EaSize = 0;
				
				FilePositionInformation->CurrentByteOffset =
					FileObject->CurrentByteOffset;
				
				if (Length < sizeof(FILE_ALL_INFORMATION) +
					Fcb->FileName.Length - sizeof(WCHAR))
				{
					Irp->IoStatus.Information = sizeof(FILE_ALL_INFORMATION);
					Status = STATUS_BUFFER_OVERFLOW;
					__leave;
				}
				
				FileNameInformation->FileNameLength = Fcb->FileName.Length;
				
				RtlCopyMemory(
					FileNameInformation->FileName,
					Fcb->FileName.Buffer,
					Fcb->FileName.Length
					);
				
				Irp->IoStatus.Information = sizeof(FILE_ALL_INFORMATION) +
					Fcb->FileName.Length - sizeof(WCHAR);
				Status = STATUS_SUCCESS;
				__leave;
            }
	    
	    /*
	    case FileAlternateNameInformation:
            {
	    // TODO: Handle FileAlternateNameInformation
	    
	      // Here we would like to use RtlGenerate8dot3Name but I don't
	      // know how to use the argument PGENERATE_NAME_CONTEXT
	      }
	      */
	      
        case FileNetworkOpenInformation:
		{
			PFILE_NETWORK_OPEN_INFORMATION FileNetworkOpenInformation;
			
			if (Length < sizeof(FILE_NETWORK_OPEN_INFORMATION))
			{
				Status = STATUS_INFO_LENGTH_MISMATCH;
				__leave;
			}
			
			FileNetworkOpenInformation =
				(PFILE_NETWORK_OPEN_INFORMATION) Buffer;
			
			FileNetworkOpenInformation->CreationTime = Ext2SysTime(Fcb->ext2_inode->i_ctime + TIMEZONE);
			
			FileNetworkOpenInformation->LastAccessTime = Ext2SysTime(Fcb->ext2_inode->i_atime + TIMEZONE);
			
			FileNetworkOpenInformation->LastWriteTime = Ext2SysTime(Fcb->ext2_inode->i_mtime + TIMEZONE);
			
			FileNetworkOpenInformation->ChangeTime = Ext2SysTime(Fcb->ext2_inode->i_mtime + TIMEZONE);
			
			FileNetworkOpenInformation->AllocationSize.QuadPart =
				(LONGLONG)(Fcb->ext2_inode->i_size);
			
			FileNetworkOpenInformation->EndOfFile.QuadPart =
				(LONGLONG)(Fcb->ext2_inode->i_size);
			
			FileNetworkOpenInformation->FileAttributes =
				Fcb->FileAttributes;
			
			Irp->IoStatus.Information =
				sizeof(FILE_NETWORK_OPEN_INFORMATION);
			Status = STATUS_SUCCESS;
			__leave;
		}
		
        default:
		Status = STATUS_INVALID_INFO_CLASS;
        }
    }
    __finally
    {
	    if (FcbResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Fcb->MainResource,
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
				    (NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT)
				    );
			    
			    Ext2FreeIrpContext(IrpContext);
		    }
	    }
    }
    
    return Status;
}


NTSTATUS
Ext2SetInformation (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT          DeviceObject;
	NTSTATUS                Status = STATUS_UNSUCCESSFUL;
	PEXT2_VCB               Vcb;
	PFILE_OBJECT            FileObject;
	PEXT2_FCB               Fcb;
	PEXT2_CCB               Ccb;
	PIRP                    Irp;
	PIO_STACK_LOCATION      IoStackLocation;
	FILE_INFORMATION_CLASS  FileInformationClass;
	ULONG                   Length;
	PVOID                   Buffer;
#ifndef EXT2_RO
	BOOLEAN                 VcbResourceAcquired = FALSE;
#endif
	BOOLEAN                 FcbMainResourceAcquired = FALSE;
#ifndef EXT2_RO
	BOOLEAN                 FcbPagingIoResourceAcquired = FALSE;
#endif

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
		
		FileObject = IrpContext->FileObject;
		
		Fcb = (PEXT2_FCB) FileObject->FsContext;
		
		ASSERT(Fcb != NULL);
		
		//
		// This request is not allowed on volumes
		//
		if (Fcb->Identifier.Type == VCB)
		{
			Status = STATUS_INVALID_PARAMETER;
			__leave;
		}
		
		ASSERT((Fcb->Identifier.Type == FCB) &&
			(Fcb->Identifier.Size == sizeof(EXT2_FCB)));
		
		Ccb = (PEXT2_CCB) FileObject->FsContext2;
		
		ASSERT(Ccb != NULL);
		
		ASSERT((Ccb->Identifier.Type == CCB) &&
			(Ccb->Identifier.Size == sizeof(EXT2_CCB)));
		
		Irp = IrpContext->Irp;
		
		IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
		
		FileInformationClass =
			IoStackLocation->Parameters.SetFile.FileInformationClass;
		
		Length = IoStackLocation->Parameters.SetFile.Length;
		
		Buffer = Irp->AssociatedIrp.SystemBuffer;
		
#ifndef EXT2_RO
		
		if (FileInformationClass == FileDispositionInformation ||
			FileInformationClass == FileRenameInformation ||
			FileInformationClass == FileLinkInformation)
		{
			if (!ExAcquireResourceExclusiveLite(
				&Vcb->MainResource,
				IrpContext->IsSynchronous ))
			{
				Status = STATUS_PENDING;
				__leave;
			}
			
			VcbResourceAcquired = TRUE;
		}
		
#endif // !EXT2_RO
		
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
			
			FcbMainResourceAcquired = TRUE;
		}
		
#ifndef EXT2_RO
		
		if (FileInformationClass == FileDispositionInformation ||
			FileInformationClass == FileRenameInformation ||
			FileInformationClass == FileLinkInformation ||
			FileInformationClass == FileAllocationInformation ||
			FileInformationClass == FileEndOfFileInformation)
		{
			if (!ExAcquireResourceExclusiveLite(
				&Fcb->PagingIoResource,
				IrpContext->IsSynchronous ))
			{
				Status = STATUS_PENDING;
				__leave;
			}
			
			FcbPagingIoResourceAcquired = TRUE;
		}
		
#endif // !EXT2_RO
		
#ifndef EXT2_RO
		if (if (FlagOn(Vcb->Flags, VCB_READ_ONLY)))
#endif
		{
			if (FileInformationClass != FilePositionInformation)
			{
				Status = STATUS_MEDIA_WRITE_PROTECTED;
				__leave;
			}
		}
		
		switch (FileInformationClass)
		{
			//
			// This is the only set file information request supported on read
			// only file systems
			//
		case FilePositionInformation:
			{
				PFILE_POSITION_INFORMATION FilePositionInformation;
				
				if (Length < sizeof(FILE_POSITION_INFORMATION))
				{
					Status = STATUS_INVALID_PARAMETER;
					__leave;
				}
				
				FilePositionInformation = (PFILE_POSITION_INFORMATION) Buffer;
				
				if ((FlagOn(FileObject->Flags, FO_NO_INTERMEDIATE_BUFFERING)) &&
					(FilePositionInformation->CurrentByteOffset.LowPart &
					DeviceObject->AlignmentRequirement) )
				{
					Status = STATUS_INVALID_PARAMETER;
					__leave;
				}
				
				FileObject->CurrentByteOffset =
					FilePositionInformation->CurrentByteOffset;
				
				Status = STATUS_SUCCESS;
				__leave;
			}
			
		default:
			Status = STATUS_INVALID_INFO_CLASS;
		}
    }
    __finally
    {
	    
#ifndef EXT2_RO
	    
	    if (FcbPagingIoResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Fcb->PagingIoResource,
			    ExGetCurrentResourceThread() );
	    }
	    
#endif // !EXT2_RO
	    
	    if (FcbMainResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Fcb->MainResource,
			    ExGetCurrentResourceThread() );
	    }
	    
#ifndef EXT2_RO
	    
	    if (VcbResourceAcquired)
	    {
		    ExReleaseResourceForThreadLite(
			    &Vcb->MainResource,
			    ExGetCurrentResourceThread() );
	    }
	    
#endif // !EXT2_RO
	    
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
