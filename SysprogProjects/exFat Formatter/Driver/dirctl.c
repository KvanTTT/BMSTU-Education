/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             dirctl.c
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
#pragma alloc_text(PAGE, Ext2GetInfoLength)
#pragma alloc_text(PAGE, Ext2ProcessDirEntry)
#pragma alloc_text(PAGE, Ext2QueryDirectory)
#pragma alloc_text(PAGE, Ext2NotifyChangeDirectory)
#pragma alloc_text(PAGE, Ext2DirectoryControl)
#endif

ULONG
Ext2GetInfoLength(IN FILE_INFORMATION_CLASS  FileInformationClass)
{
	switch (FileInformationClass)
	{
	case FileDirectoryInformation:
		return sizeof(FILE_DIRECTORY_INFORMATION);
		break;
		
	case FileFullDirectoryInformation:
		return sizeof(FILE_FULL_DIR_INFORMATION);
		break;
		
	case FileBothDirectoryInformation:
		return sizeof(FILE_BOTH_DIR_INFORMATION);
		break;
		
	case FileNamesInformation:
		return sizeof(FILE_NAMES_INFORMATION);
		break;
		
	default:
		break;
	}

	return 0;
}

/*
#define FillInfo (FI, BSize, Inode, Index, NSize, pName, Single) {\
	if (!Single) \
		FI->NextEntryOffset = BSize + NSize - sizeof(WCHAR); \
	else \
			FI->NextEntryOffset = 0; \
	FI->FileIndex = Index; \
	FI->CreationTime.QuadPart = Inode.i_ctime + TIMEZONE; \
	FI->LastAccessTime.QuadPart = Inode.i_atime + TIMEZONE; \
	FI->LastWriteTime.QuadPart = Inode.i_mtime + TIMEZONE; \
	FI->ChangeTime.QuadPart = Inode.i_mtime + TIMEZONE; \
	FI->EndOfFile.QuadPart = Inode.i_size; \
	FI->AllocationSize.QuadPart = Inode.i_size; \
	FI->LastAccessTime.QuadPart = Inode.i_atime + TIMEZONE; \
	FI->FileAttributes = FILE_ATTRIBUTE_NORMAL; \
	if (S_ISDIR(Inode->i_mode)) \
		FI->FileAttributes |= FILE_ATTRIBUTE_DIRECTORY; \
	FI->FileNameLength = NSize; \
	RtlCopyMemory(FI->FileName, pName->Buffer, NSize); \
	dwBytes = BSize + NSize - sizeof(WCHAR); }
*/

ULONG
Ext2ProcessDirEntry(IN PEXT2_VCB         Vcb,
		    IN FILE_INFORMATION_CLASS  FileInformationClass,
		    IN ULONG		 in,
		    IN PVOID		 Buffer,
		    IN ULONG		 UsedLength,
		    IN ULONG		 Length,
		    IN ULONG		 FileIndex,
		    IN PUNICODE_STRING	 pName,
		    IN BOOLEAN		 Single )
{
	EXT2_INODE inode;
	PFILE_DIRECTORY_INFORMATION FDI;
	PFILE_FULL_DIR_INFORMATION FFI;
	PFILE_BOTH_DIR_INFORMATION FBI;
	PFILE_NAMES_INFORMATION	FNI;

	ULONG InfoLength = 0;
	ULONG NameLength = 0;
	ULONG dwBytes = 0;

	NameLength = pName->Length;

	if (!in)
	{
		KdPrint(("Ext2PricessDirEntry: ext2_dir_entry is empty.\n"));
		return 0;
	}

	InfoLength = Ext2GetInfoLength(FileInformationClass);
	if (!InfoLength || InfoLength + NameLength - sizeof(WCHAR)> Length)
	{
		KdPrint(("Ext2PricessDirEntry: Size/Length error.\n"));
		return 0;
	}

	if(!Ext2LoadInode(Vcb, in, &inode))
	{
		KdPrint(("Ext2PricessDirEntry: Loading inode %xh error.\n", in));
		return 0;
	}

	switch(FileInformationClass)
	{
	case FileDirectoryInformation:
		FDI = (PFILE_DIRECTORY_INFORMATION) ((PUCHAR)Buffer + UsedLength);
		if (!Single)
			FDI->NextEntryOffset = InfoLength + NameLength - sizeof(WCHAR);
		else
			FDI->NextEntryOffset = 0;
		FDI->FileIndex = FileIndex;
		FDI->CreationTime = Ext2SysTime(inode.i_ctime + TIMEZONE);
		FDI->LastAccessTime = Ext2SysTime(inode.i_atime + TIMEZONE);
		FDI->LastWriteTime = Ext2SysTime(inode.i_mtime + TIMEZONE);
		FDI->ChangeTime = Ext2SysTime(inode.i_mtime + TIMEZONE);
		FDI->EndOfFile.QuadPart = inode.i_size;
		FDI->AllocationSize.QuadPart = inode.i_size;
		FDI->FileAttributes = FILE_ATTRIBUTE_NORMAL;

#ifndef EXT2_RO
	    if (FlagOn(Vcb->Flags, VCB_READ_ONLY))
#endif
		{
			SetFlag(FDI->FileAttributes, FILE_ATTRIBUTE_READONLY);
		}

		if (S_ISDIR(inode.i_mode))
			FDI->FileAttributes |= FILE_ATTRIBUTE_DIRECTORY;
		FDI->FileNameLength = NameLength;
		RtlCopyMemory(FDI->FileName, pName->Buffer, NameLength);
		dwBytes = InfoLength + NameLength - sizeof(WCHAR); 
		break;
		
	case FileFullDirectoryInformation:
		FFI = (PFILE_FULL_DIR_INFORMATION) ((PUCHAR)Buffer + UsedLength);
//		FillInfo (FFI, InfoLength, inode, FileIndex, NameLength, pName, Single)
		if (!Single)
			FFI->NextEntryOffset = InfoLength + NameLength - sizeof(WCHAR);
		else
			FFI->NextEntryOffset = 0;
		FFI->FileIndex = FileIndex;
		FFI->CreationTime = Ext2SysTime(inode.i_ctime + TIMEZONE);
		FFI->LastAccessTime = Ext2SysTime(inode.i_atime + TIMEZONE);
		FFI->LastWriteTime = Ext2SysTime(inode.i_mtime + TIMEZONE);
		FFI->ChangeTime = Ext2SysTime(inode.i_mtime + TIMEZONE);
		FFI->EndOfFile.QuadPart = inode.i_size;
		FFI->AllocationSize.QuadPart = inode.i_size;
		FFI->FileAttributes = FILE_ATTRIBUTE_NORMAL;
#ifndef EXT2_RO
	    if (FlagOn(Vcb->Flags, VCB_READ_ONLY))
#endif
		{
			SetFlag(FFI->FileAttributes, FILE_ATTRIBUTE_READONLY);
		}
		if (S_ISDIR(inode.i_mode))
			FFI->FileAttributes |= FILE_ATTRIBUTE_DIRECTORY;
		FFI->FileNameLength = NameLength;
		RtlCopyMemory(FFI->FileName, pName->Buffer, NameLength);
		dwBytes = InfoLength + NameLength - sizeof(WCHAR); 

		break;
		
	case FileBothDirectoryInformation:
		FBI = (PFILE_BOTH_DIR_INFORMATION) ((PUCHAR)Buffer + UsedLength);
//		FillInfo (FBI, InfoLength, inode, FileIndex, NameLength, pName, Single)
		if (!Single)
			FBI->NextEntryOffset = InfoLength + NameLength - sizeof(WCHAR);
		else
			FBI->NextEntryOffset = 0;
		FBI->CreationTime = Ext2SysTime(inode.i_ctime + TIMEZONE);
		FBI->LastAccessTime = Ext2SysTime(inode.i_atime + TIMEZONE);
		FBI->LastWriteTime = Ext2SysTime(inode.i_mtime + TIMEZONE);
		FBI->ChangeTime = Ext2SysTime(inode.i_mtime + TIMEZONE);

/*
		FBI->CreationTime.QuadPart = inode.i_ctime + TIMEZONE;
		FBI->LastAccessTime.QuadPart = inode.i_atime + TIMEZONE;
		FBI->LastWriteTime.QuadPart = inode.i_mtime + TIMEZONE;
		FBI->ChangeTime.QuadPart = inode.i_mtime + TIMEZONE;
*/
		FBI->EndOfFile.QuadPart = inode.i_size;
		FBI->AllocationSize.QuadPart = inode.i_size;
		FBI->FileAttributes = FILE_ATTRIBUTE_NORMAL;
#ifndef EXT2_RO
	    if (FlagOn(Vcb->Flags, VCB_READ_ONLY))
#endif
		{
			SetFlag(FBI->FileAttributes, FILE_ATTRIBUTE_READONLY);
		}

		if (S_ISDIR(inode.i_mode))
			FBI->FileAttributes |= FILE_ATTRIBUTE_DIRECTORY;
		FBI->FileNameLength = NameLength;
		RtlCopyMemory(FBI->FileName, pName->Buffer, NameLength);
		dwBytes = InfoLength + NameLength - sizeof(WCHAR); 

		break;
		
	case FileNamesInformation:
		FNI = (PFILE_NAMES_INFORMATION) ((PUCHAR)Buffer + UsedLength);
		if (!Single)
			FNI->NextEntryOffset = InfoLength + NameLength - sizeof(WCHAR);
		else
			FNI->NextEntryOffset = 0;
		FNI->FileNameLength = NameLength;
		RtlCopyMemory(FNI->FileName, pName->Buffer, NameLength);
		dwBytes = InfoLength + NameLength - sizeof(WCHAR); 

		break;
		
	default:
		break;
	}

	return dwBytes;
}


NTSTATUS
Ext2QueryDirectory (IN PEXT2_IRP_CONTEXT IrpContext)
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
	PUNICODE_STRING         FileName;
	ULONG                   FileIndex;
	BOOLEAN                 RestartScan;
	BOOLEAN                 ReturnSingleEntry;
	BOOLEAN                 IndexSpecified;
	PUCHAR                  Buffer;
	BOOLEAN                 FirstQuery;
	struct ext2_inode*      Inode = NULL;
	BOOLEAN                 FcbResourceAcquired = FALSE;
	ULONG                   UsedLength = 0;
	USHORT                  InodeFileNameLength;
	UNICODE_STRING          InodeFileName;
	PVOID			DirectoryContent = NULL;
	struct			ext2_dir_entry* pDir;
	ULONG			dwBytes;
	ULONG			dwTemp = 0;
	ULONG			dwSize = 0;
	ULONG			dwReturn = 0;
	BOOLEAN			bRun = TRUE;
	LONGLONG		ByteOffset;

	InodeFileName.Buffer = NULL;
	
	__try
	{
		ASSERT(IrpContext);
		
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
		
		ASSERT(Fcb);
		
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
		
		if (!FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
		{
			Status = STATUS_INVALID_PARAMETER;
			__leave;
		}
		
		Ccb = (PEXT2_CCB) FileObject->FsContext2;
		
		ASSERT(Ccb);
		
		ASSERT((Ccb->Identifier.Type == CCB) &&
			(Ccb->Identifier.Size == sizeof(EXT2_CCB)));
		
		Irp = IrpContext->Irp;
		
		IoStackLocation = IoGetCurrentIrpStackLocation(Irp);
		
#ifndef _GNU_NTIFS_
		
		FileInformationClass =
			IoStackLocation->Parameters.QueryDirectory.FileInformationClass;
		
		Length = IoStackLocation->Parameters.QueryDirectory.Length;
		
		FileName = IoStackLocation->Parameters.QueryDirectory.FileName;
		
		FileIndex = IoStackLocation->Parameters.QueryDirectory.FileIndex;
		
#else // _GNU_NTIFS_
		
		FileInformationClass = ((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Parameters.QueryDirectory.FileInformationClass;
		
		Length = ((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Parameters.QueryDirectory.Length;
		
		FileName = ((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Parameters.QueryDirectory.FileName;
		
		FileIndex = ((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Parameters.QueryDirectory.FileIndex;
		
#endif // _GNU_NTIFS_
		
        RestartScan = FlagOn(((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Flags, SL_RESTART_SCAN);
        ReturnSingleEntry = FlagOn(((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Flags, SL_RETURN_SINGLE_ENTRY);
        IndexSpecified = FlagOn(((PEXTENDED_IO_STACK_LOCATION)
			IoStackLocation)->Flags, SL_INDEX_SPECIFIED);
/*
        if (!Irp->MdlAddress && Irp->UserBuffer)
        {
            ProbeForWrite(Irp->UserBuffer, Length, 1);
        }
*/
        Buffer = Ext2GetUserBuffer(Irp);

        if (Buffer == NULL)
        {
            Status = STATUS_INVALID_USER_BUFFER;
            __leave;
        }
		
		if (!IrpContext->IsSynchronous)
		{
			Status = STATUS_PENDING;
			__leave;
		}
		
        if (!ExAcquireResourceSharedLite(
                 &Fcb->MainResource,
                 IrpContext->IsSynchronous
                 ))
        {
            Status = STATUS_PENDING;
            __leave;
        }

        FcbResourceAcquired = TRUE;
		
		if (FileName != NULL)
		{
	
			if (Ccb->DirectorySearchPattern.Buffer != NULL)
			{
				FirstQuery = FALSE;
			}
			else
			{
				FirstQuery = TRUE;
				
				Ccb->DirectorySearchPattern.Length =
					Ccb->DirectorySearchPattern.MaximumLength =
					FileName->Length;
				
				Ccb->DirectorySearchPattern.Buffer =
					ExAllocatePool(NonPagedPool, FileName->Length);
				
				if (Ccb->DirectorySearchPattern.Buffer == NULL)
				{
					Status = STATUS_INSUFFICIENT_RESOURCES;
					__leave;
				}
				
				RtlCopyMemory(
					Ccb->DirectorySearchPattern.Buffer,
					FileName->Buffer,
					FileName->Length );
			}
		}
		else if (Ccb->DirectorySearchPattern.Buffer != NULL)
		{
			FirstQuery = FALSE;
			FileName = &Ccb->DirectorySearchPattern;
		}
		else
		{
			FirstQuery = TRUE;
			
			Ccb->DirectorySearchPattern.Length =
				Ccb->DirectorySearchPattern.MaximumLength =
				sizeof(L"*");
			
			Ccb->DirectorySearchPattern.Buffer =
				ExAllocatePool(NonPagedPool, sizeof(L"*"));
			
			if (Ccb->DirectorySearchPattern.Buffer == NULL)
			{
				Status = STATUS_INSUFFICIENT_RESOURCES;
				__leave;
			}
			
			RtlCopyMemory(
				Ccb->DirectorySearchPattern.Buffer,
				L"*", sizeof(L"*"));
		}
		
		if (!IndexSpecified)
		{
			if (RestartScan || FirstQuery)
			{
				Fcb->IndexNumber.QuadPart = 0;
				FileIndex = (ULONG)Fcb->IndexNumber.QuadPart;
			}
			else
			{
				FileIndex = Ccb->CurrentByteOffset;
			}
		}
		
		Inode = (struct ext2_inode*) ExAllocatePool(
			NonPagedPool,
			sizeof(struct ext2_inode));
		
		if (Inode == NULL)
		{
			Status = STATUS_INSUFFICIENT_RESOURCES;
			__leave;
		}
		
		RtlZeroMemory(Buffer, Length);

		dwSize = Fcb->ext2_inode->i_size - FileIndex;
		if (dwSize <= 0)
		{
			Status = STATUS_NO_MORE_FILES;
			__leave;
		}
		
		DirectoryContent = ExAllocatePool(PagedPool,
			dwSize);
		if (!DirectoryContent)
		{
			Status = STATUS_INSUFFICIENT_RESOURCES;
			__leave;
		}
		
		dwBytes = 0;
		RtlZeroMemory(DirectoryContent, dwSize);
		//Ext2ReadInode(Vcb, Fcb->ext2_inode, FileIndex, DirectoryContent, dwSize);
		ByteOffset = (LONGLONG)FileIndex;
		if (!CcCopyRead(
				Fcb->StreamObj,	//FileObject,
				(PLARGE_INTEGER)&ByteOffset,
				dwSize,
				IrpContext->IsSynchronous,
				DirectoryContent,
				&Irp->IoStatus ))
		{
			Status = STATUS_PENDING;
			__leave;
		}

		dwTemp = 0;
		
		pDir = (struct ext2_dir_entry *) ((PUCHAR)DirectoryContent + dwBytes);

		while (bRun && UsedLength < Length  && dwBytes < dwSize && pDir->inode)
		{

			InodeFileNameLength = pDir->name_len & 0xff;
			
			InodeFileName.Length = InodeFileName.MaximumLength =
				InodeFileNameLength * 2;
			
			if (InodeFileName.Length <=0 )
				break;

			InodeFileName.Buffer = ExAllocatePool(
				NonPagedPool,
				InodeFileNameLength * 2 + 2);

			RtlZeroMemory(InodeFileName.Buffer, InodeFileNameLength * 2 + 2);
			
			Ext2CharToWchar(
				InodeFileName.Buffer,
				pDir->name,
				InodeFileNameLength );

			if (FsRtlDoesNameContainWildCards(FileName) ?
				FsRtlIsNameInExpression(
					FileName,
					&InodeFileName,
					TRUE,
					NULL) :
				!RtlCompareUnicodeString(
					FileName,
					&InodeFileName,
					TRUE) 			)
			{
				dwReturn = Ext2ProcessDirEntry(
					Vcb, FileInformationClass,
					pDir->inode, Buffer, UsedLength, Length - UsedLength,
					dwBytes, &InodeFileName,
					ReturnSingleEntry );
				if (dwReturn <= 0)
				{
					bRun = FALSE;
				}
				else
				{
					dwTemp = UsedLength;
					UsedLength += dwReturn;
				}
			}
			
			if (InodeFileName.Buffer != NULL)
			{
				ExFreePool(InodeFileName.Buffer);
				InodeFileName.Buffer = NULL;
			}
			
			if (bRun)
			{
				dwBytes +=pDir->rec_len;
				Ccb->CurrentByteOffset = FileIndex + dwBytes;
				pDir = (struct ext2_dir_entry *) ((PUCHAR)DirectoryContent + dwBytes);
			}

			if (UsedLength && ReturnSingleEntry)
			{
				Status = STATUS_SUCCESS;
				__leave;
			}
		}

		FileIndex += dwBytes;

		((PULONG)((PUCHAR)Buffer + dwTemp)) [0] = 0;

		if (!UsedLength)
		{
			if (FirstQuery)
			{
				Status = STATUS_NO_SUCH_FILE;
			}
			else
			{
				Status = STATUS_NO_MORE_FILES;
			}
		}
		else
		{
			Status = STATUS_SUCCESS;
		}
	}

	__finally
	{
	
		if (FcbResourceAcquired)
		{
			ExReleaseResourceForThreadLite(
				&Fcb->MainResource,
				ExGetCurrentResourceThread()
				);
		}
		
		if (Inode != NULL)
		{
			ExFreePool(Inode);
		}
		
		if (DirectoryContent != NULL)
		{
			ExFreePool(DirectoryContent);
			DirectoryContent = NULL;
		}
		
		if (InodeFileName.Buffer != NULL)
		{
			ExFreePool(InodeFileName.Buffer);
		}
		
		if (!IrpContext->ExceptionInProgress)
		{
			if (Status == STATUS_PENDING)
			{
				Status = Ext2LockUserBuffer(
					IrpContext->Irp,
					Length,
					IoWriteAccess );
				
				if (NT_SUCCESS(Status))
				{
					Status = Ext2QueueRequest(IrpContext);
				}
				else
				{
					IrpContext->Irp->IoStatus.Status = Status;
					Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
					Ext2FreeIrpContext(IrpContext);
				}
			}
			else
			{
				IrpContext->Irp->IoStatus.Information = UsedLength;
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

NTSTATUS
Ext2NotifyChangeDirectory (
    IN PEXT2_IRP_CONTEXT IrpContext
    )
{
    PDEVICE_OBJECT      DeviceObject;
    BOOLEAN             CompleteRequest;
    NTSTATUS            Status = STATUS_UNSUCCESSFUL;
    PEXT2_VCB           Vcb;
    PFILE_OBJECT        FileObject;
    PEXT2_FCB           Fcb;
    PIRP                Irp;
    PIO_STACK_LOCATION  IrpSp;
    ULONG               CompletionFilter;
    BOOLEAN             WatchTree;

    __try
    {
        ASSERT(IrpContext);

        ASSERT((IrpContext->Identifier.Type == ICX) &&
               (IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));

		//
		//  Always set the wait flag in the Irp context for the original request.
		//

		SetFlag( IrpContext->Flags, IRP_CONTEXT_FLAG_WAIT );

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

        FileObject = IrpContext->FileObject;

        Fcb = (PEXT2_FCB) FileObject->FsContext;

        ASSERT(Fcb);

        if (Fcb->Identifier.Type == VCB)
        {
            CompleteRequest = TRUE;
            Status = STATUS_INVALID_PARAMETER;
            __leave;
        }

        ASSERT((Fcb->Identifier.Type == FCB) &&
               (Fcb->Identifier.Size == sizeof(EXT2_FCB)));

        if (!FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
        {
            CompleteRequest = TRUE;
            Status = STATUS_INVALID_PARAMETER;
            __leave;
        }

        Irp = IrpContext->Irp;

        IrpSp = IoGetCurrentIrpStackLocation(Irp);

#ifndef _GNU_NTIFS_

        CompletionFilter =
            IrpSp->Parameters.NotifyDirectory.CompletionFilter;

#else // _GNU_NTIFS_

        CompletionFilter = ((PEXTENDED_IO_STACK_LOCATION)
            IrpSp)->Parameters.NotifyDirectory.CompletionFilter;

#endif // _GNU_NTIFS_

        WatchTree = FlagOn(IrpSp->Flags, SL_WATCH_TREE);

        CompleteRequest = FALSE;

        Status = STATUS_PENDING;

        FsRtlNotifyChangeDirectory(
            Vcb->NotifySync,
            FileObject->FsContext2,
            &Fcb->AnsiFileName,
            &Vcb->NotifyList,
            WatchTree,
            CompletionFilter,
            Irp
            );

/*
    Currently the driver is read-only but here is an example on how to use the
    FsRtl-functions to report a change:

    ANSI_STRING TestString;
    USHORT      FileNamePartLength;

    RtlInitAnsiString(&TestString, "\\ntifs.h");

    FileNamePartLength = 7;

    FsRtlNotifyReportChange(
        Vcb->NotifySync,            // PNOTIFY_SYNC NotifySync
        &Vcb->NotifyList,           // PLIST_ENTRY  NotifyList
        &TestString,                // PSTRING      FullTargetName
        &FileNamePartLength,        // PUSHORT      FileNamePartLength
        FILE_NOTIFY_CHANGE_NAME     // ULONG        FilterMatch
        );

    or

    ANSI_STRING TestString;

    RtlInitAnsiString(&TestString, "\\ntifs.h");

    FsRtlNotifyFullReportChange(
        Vcb->NotifySync,            // PNOTIFY_SYNC NotifySync
        &Vcb->NotifyList,           // PLIST_ENTRY  NotifyList
        &TestString,                // PSTRING      FullTargetName
        1,                          // USHORT       TargetNameOffset
        NULL,                       // PSTRING      StreamName OPTIONAL
        NULL,                       // PSTRING      NormalizedParentName OPTIONAL
        FILE_NOTIFY_CHANGE_NAME,    // ULONG        FilterMatch
        0,                          // ULONG        Action
        NULL                        // PVOID        TargetContext
        );
*/

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



NTSTATUS
Ext2DirectoryControl (IN PEXT2_IRP_CONTEXT IrpContext)
{
	NTSTATUS Status;
	
	ASSERT(IrpContext);
	
	ASSERT((IrpContext->Identifier.Type == ICX) &&
		(IrpContext->Identifier.Size == sizeof(EXT2_IRP_CONTEXT)));
	
	switch (IrpContext->MinorFunction)
	{
	case IRP_MN_QUERY_DIRECTORY:
		Status = Ext2QueryDirectory(IrpContext);
		break;

    case IRP_MN_NOTIFY_CHANGE_DIRECTORY:
        Status = Ext2NotifyChangeDirectory(IrpContext);
        break;
		
	default:
		Status = STATUS_INVALID_DEVICE_REQUEST;
		IrpContext->Irp->IoStatus.Status = Status;
		Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
		Ext2FreeIrpContext(IrpContext);
	}
	
	return Status;
}
