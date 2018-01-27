/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             create.c
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
#pragma alloc_text(PAGE, Ext2LookupFileName)
#pragma alloc_text(PAGE, Ext2SearchDirMcb)
#pragma alloc_text(PAGE, Ext2SearchMcb)
#pragma alloc_text(PAGE, Ext2ScanDir)
#pragma alloc_text(PAGE, Ext2OpenFile)
#pragma alloc_text(PAGE, Ext2Create)
#endif


NTSTATUS
Ext2LookupFileName (IN PEXT2_VCB    Vcb,
		    IN PUNICODE_STRING		FullFileName,
		    IN OUT PULONG			Offset,
		    IN OUT PULONG			Inode,
		    IN OUT PULONG			DirInode,
		    IN OUT PEXT2_INODE		ext2_inode)
{
	ULONG           RootInode = 2;
	UNICODE_STRING  FileName;
	NTSTATUS        Status = STATUS_OBJECT_NAME_NOT_FOUND;
	EXT2_DIR_ENTRY  ext2_dir;
	USHORT		i = 0;
	BOOLEAN		bRun = TRUE;
	BOOLEAN		bCurrent = FALSE;
	EXT2_INODE	in;
	ULONG		off = 0;

	*Offset = 0;
	*Inode = 0;

	if (*DirInode != 0 && FullFileName->Buffer[0] != L'\\')
	{
		bCurrent = TRUE;
		RootInode = *DirInode;
	}
	else
	{
		RootInode = 2;
		*DirInode = 2;
	}
	
	RtlZeroMemory(&ext2_dir, sizeof(EXT2_DIR_ENTRY));

	if (FullFileName->Length == 0)
	{
		return Status;
	}
	
	if (FullFileName->Length == 2 && FullFileName->Buffer[0] == L'\\')
	{
		if (!Ext2LoadInode(Vcb, RootInode, ext2_inode))
		{
			return Status;
		}
		*Inode = 2;
		*DirInode = 2;
		return STATUS_SUCCESS;
	}
	
	while (bRun && i < FullFileName->Length/2)
	{
		ULONG Length;

		if (bCurrent)
		{
			bCurrent = FALSE;
		}
		else
		{
			while(FullFileName->Buffer[i] == L'\\') i++;
		}

		Length = i;

		while(i < FullFileName->Length/2 && (FullFileName->Buffer[i] != L'\\'))
				i++;

		if (i - Length >0)
		{
			if (NT_SUCCESS(Status))
				RootInode = ext2_dir.inode;

			FileName = *FullFileName;
			FileName.Buffer += Length;
			FileName.Length = (USHORT)((i - Length) * 2);

			if (!Ext2LoadInode(Vcb, RootInode, &in))
			{
				return STATUS_OBJECT_NAME_NOT_FOUND;
			}

			if (!S_ISDIR(in.i_mode))
			{
				if (i < FullFileName->Length/2)
				{
					*Offset = 0;
					*Inode = 0;
					*DirInode = 0;

					Status =  STATUS_OBJECT_NAME_NOT_FOUND;
				}
				break;
			}

			Status = Ext2ScanDir (
				Vcb,
				RootInode,
				&FileName,
				&off,
				&in,
				&ext2_dir);

			if (!NT_SUCCESS(Status))
				bRun = FALSE;

		}
	}

	if (NT_SUCCESS(Status))
	{
		if (Inode)
			*Inode = ext2_dir.inode;
		if (DirInode)
			*DirInode = RootInode;
		
		if (Offset)
			*Offset = 0;
		if (ext2_inode)
			Ext2LoadInode(Vcb, ext2_dir.inode, ext2_inode);
	}

	return Status;
}


PEXT2_FCB
Ext2SearchDirMcb(IN	PEXT2_VCB	Vcb,
				 IN	PUNICODE_STRING	LongFileName,
				 OUT PUNICODE_STRING ShortName)
{
	BOOLEAN				bFound = FALSE;
	PLIST_ENTRY         Link;
	PEXT2_FCB           TmpFcb;
	UNICODE_STRING		FileName;
	USHORT				i;


	if (LongFileName->Length == 2 && LongFileName->Buffer[0] == L'\\')
	{
		return Ext2SearchMcb(Vcb, (ULONG) 2);
	}

	i = LongFileName->Length / 2 - 1;

	if (LongFileName->Buffer[i] == L'\\')
		i--;

	while(i > 0 && LongFileName->Buffer[i] != L'\\')
		i--;

	if (i <= 1)
		return NULL;

	FileName = *LongFileName;
	FileName.Length = i * 2;

	Link = Vcb->FcbList.Flink;
	
	while (!bFound && Link != &Vcb->FcbList)
	{
		TmpFcb = CONTAINING_RECORD(Link, EXT2_FCB, Next);
		
		if (TmpFcb && TmpFcb->Identifier.Type == FCB)
		{
			KdPrint(("[%s,%#x]\n", TmpFcb->AnsiFileName.Buffer, (ULONG)TmpFcb->IndexNumber.QuadPart));
			
			if (!RtlCompareUnicodeString(
				&FileName,
				&(TmpFcb->FileName),
				TRUE ))
			{
				KdPrint(("Ext2OpenFile: Found FCB for %xh.\n", TmpFcb->inode));
				bFound = TRUE;
				*ShortName = *LongFileName;
				ShortName->Buffer += i + 1;
				ShortName->Length = LongFileName->Length - (i + 1)*2;
			}
		}
		Link = Link->Flink;
	}

	if (bFound)
		return TmpFcb;
	else
		return NULL;
	
}


PEXT2_FCB
Ext2SearchMcb(	IN PEXT2_VCB    Vcb,
				IN ULONG        inode )
{
	BOOLEAN				bFound = FALSE;
	PLIST_ENTRY         Link;
	PEXT2_FCB           TmpFcb;

	Link = Vcb->FcbList.Flink;
	
	while (!bFound && Link != &Vcb->FcbList)
	{
		TmpFcb = CONTAINING_RECORD(Link, EXT2_FCB, Next);
		
		if (TmpFcb && TmpFcb->Identifier.Type == FCB)
		{
			KdPrint(("[%s,%#x]\n", TmpFcb->AnsiFileName.Buffer, (ULONG)TmpFcb->IndexNumber.QuadPart));
			
			if (TmpFcb->inode == inode)
			{
				KdPrint(("Ext2OpenFile: Found FCB for %xh.\n", inode));
				bFound = TRUE;
			}
		}
		Link = Link->Flink;
	}

	if (bFound)
		return TmpFcb;
	else
		return NULL;
	
}


NTSTATUS
Ext2ScanDir (IN PEXT2_VCB       Vcb,
	     IN ULONG				inode,
	     IN PUNICODE_STRING		FileName,
	     IN OUT PULONG			Index,
	     IN PEXT2_INODE			ext2_inode,
	     IN OUT PEXT2_DIR_ENTRY ext2_dir)
{
	NTSTATUS                Status = STATUS_UNSUCCESSFUL;
	USHORT                  InodeFileNameLength;
	UNICODE_STRING          InodeFileName;

	PVOID			DirectoryContent = NULL;
	struct			ext2_dir_entry* pDir = NULL;
	ULONG			dwBytes;
	BOOLEAN			bFound = FALSE;
	PEXT2_FCB		Dcb;
	LONGLONG		Offset = 0;
	IO_STATUS_BLOCK	IoStatus;	

	InodeFileName.Buffer = NULL;

	__try
	{

		DirectoryContent = ExAllocatePool(PagedPool,
			ext2_inode->i_size);
		if (!DirectoryContent)
		{
			Status = STATUS_INSUFFICIENT_RESOURCES;
			__leave;
		}

		Dcb = Ext2SearchMcb(Vcb, inode);

		if (Dcb)
		{
			if (!CcCopyRead(
					Dcb->StreamObj,
					(PLARGE_INTEGER)&Offset,
					ext2_inode->i_size,
					TRUE,
					DirectoryContent,
					&IoStatus ));

		}
		else
		{
			KdPrint(("Ext2ScanDir: Dcb does not exist.\n"));
			IoStatus.Status = Ext2ReadInode(
					NULL,
					Vcb,
					ext2_inode,
					0,
					DirectoryContent,
					ext2_inode->i_size,
					&(IoStatus.Information));
		}

		if (!NT_SUCCESS(IoStatus.Status))
		{
			KdPrint(("Ext2ScanDir: Reading Directory Content error.\n"));
			__leave;
		}

		dwBytes = 0;

		pDir = (struct ext2_dir_entry *) ((PUCHAR)DirectoryContent + dwBytes);

		while (!bFound && dwBytes < ext2_inode->i_size && pDir->inode)
		{
			InodeFileNameLength = pDir->name_len & 0xff;
			
			InodeFileName.Length = InodeFileName.MaximumLength =
				InodeFileNameLength * 2;
			
			InodeFileName.Buffer = ExAllocatePool(
				NonPagedPool,
				InodeFileNameLength * 2 + 2);

			RtlZeroMemory(InodeFileName.Buffer, InodeFileNameLength * 2 + 2);
			
			Ext2CharToWchar(
				InodeFileName.Buffer,
				pDir->name,
				InodeFileNameLength );

			if (!RtlCompareUnicodeString(
				FileName,
				&InodeFileName,
				TRUE ))
			{
				bFound = TRUE;
				*Index = dwBytes;
				RtlCopyMemory(ext2_dir, pDir, pDir->rec_len > sizeof(EXT2_DIR_ENTRY)
					? sizeof(EXT2_DIR_ENTRY) : pDir->rec_len);
				Status = STATUS_SUCCESS;
				KdPrint(("Ext2ScanDir: Found: inode=%xh\n", pDir->inode));
			}
			
			if (InodeFileName.Buffer != NULL)
			{
				ExFreePool(InodeFileName.Buffer);
				InodeFileName.Buffer = NULL;
			}
			
			dwBytes +=pDir->rec_len;
			pDir = (struct ext2_dir_entry *) ((PUCHAR)DirectoryContent + dwBytes);

		}

		if (!bFound)
			Status = STATUS_NO_SUCH_FILE;
	}

	__finally
	{
		if (InodeFileName.Buffer != NULL)
		{
			ExFreePool(InodeFileName.Buffer);
		}

		if (DirectoryContent)
			ExFreePool(DirectoryContent);
	}
	
	return Status;
}

NTSTATUS
Ext2OpenFile(PEXT2_VCB Vcb, PIRP Irp)
{
	NTSTATUS			Status = STATUS_UNSUCCESSFUL;
	PIO_STACK_LOCATION  io_stack;
	PEXT2_FCB           Fcb = NULL;
	PEXT2_FCB			pParentFcb = NULL;
	PEXT2_CCB           Ccb = NULL;
	ULONG               found_index = 0;
	struct ext2_inode*  ext2_inode;
	BOOLEAN             VcbResourceAcquired = FALSE;
	BOOLEAN				bDir = FALSE;
	BOOLEAN				bCreated = FALSE;
	ULONG				inode, dir_inode;
	UNICODE_STRING	    FileName;

	FileName.Buffer = NULL;

	io_stack = IoGetCurrentIrpStackLocation(Irp);

	__try
	{
		ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			TRUE );
		
		VcbResourceAcquired = TRUE;
		
		ext2_inode = ExAllocatePool(
			NonPagedPool,
			sizeof(struct ext2_inode) );

		if (!ext2_inode)
			__leave;

		FileName.MaximumLength = 0x400;

		if (io_stack->FileObject->RelatedFileObject)
		{
			pParentFcb = (PEXT2_FCB)(io_stack->FileObject->RelatedFileObject->FsContext);
		}

		FileName =	io_stack->FileObject->FileName;

		if (pParentFcb)
		{
			dir_inode = pParentFcb->inode;
		}
		else
		{
			dir_inode = 2;
		}
		KdPrint(("Ext2OpenFile: %S (name len=%xh)Opt: %xh.\n",
			FileName.Buffer, FileName.Length,
			io_stack->Parameters.Create.Options));

		Status = Ext2LookupFileName(
			Vcb,
			&FileName,
			&found_index,
			&inode,
			&dir_inode,
			ext2_inode );

		if (!NT_SUCCESS(Status))
		{
			KdPrint(("Ext2OpenFile: Ext2LookupFileName: File Not Found.\n"));
			__leave;
		}
		
		Fcb = Ext2SearchMcb(Vcb, inode);

		if (!Fcb)
		{
			Fcb = Ext2AllocateFcb (
				Vcb, &FileName,
				found_index, inode,
				dir_inode, ext2_inode);
			bCreated = TRUE;
		}
		
		if (Fcb)
		{
			Ccb = Ext2AllocateCcb();

			ExAcquireResourceExclusiveLite(&Fcb->CountResource, TRUE);
			Fcb->OpenHandleCount++;
			Fcb->ReferenceCount++;
			ExReleaseResourceForThreadLite(
					&Fcb->CountResource,
					ExGetCurrentResourceThread());

			ExAcquireResourceExclusiveLite(&Vcb->CountResource, TRUE);
			Vcb->OpenFileHandleCount++;
			Vcb->ReferenceCount++;
			ExReleaseResourceForThreadLite(
					&Vcb->CountResource,
					ExGetCurrentResourceThread());
			

			KdPrint(("Ext2OpenFile: %s refercount=%xh\n", Fcb->AnsiFileName.Buffer, Fcb->ReferenceCount));


			if (!FlagOn(Fcb->FileAttributes, FILE_ATTRIBUTE_DIRECTORY))
			{
				Fcb->CommonFCBHeader.IsFastIoPossible = FastIoIsPossible;
			}

			io_stack->FileObject->FsContext = (void*)Fcb;
			io_stack->FileObject->FsContext2 = (void*) Ccb;
			io_stack->FileObject->PrivateCacheMap = NULL;
			io_stack->FileObject->SectionObjectPointer = &(Fcb->SectionObject);
			io_stack->FileObject->Vpb = Vcb->Vpb;

			Irp->IoStatus.Information = FILE_OPENED;
			Status = STATUS_SUCCESS;

			KdPrint(("Ext2OpenFile: %s OpenCount: %u ReferCount: %u\n",
				Fcb->AnsiFileName.Buffer, Fcb->OpenHandleCount,	Fcb->ReferenceCount));

		}
		else
		{
			Irp->IoStatus.Information = 0;
			Status = STATUS_OBJECT_NAME_NOT_FOUND;
			KdPrint(("Ext2OpenFile: Create denied (CCB allocate error.)\n"));
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

		if (!bCreated)
		{
			if (ext2_inode)
				ExFreePool(ext2_inode);
		}
		else
		{
			if (!Fcb && ext2_inode)
				ExFreePool(ext2_inode);
		}
	}
	
	return Status;
}


NTSTATUS
Ext2Create (IN PEXT2_IRP_CONTEXT IrpContext)
{
	PDEVICE_OBJECT      DeviceObject;
	PIRP                Irp;
	PIO_STACK_LOCATION  io_stack;
	PEXT2_VCB           Vcb = 0;
	NTSTATUS			Status = STATUS_OBJECT_NAME_NOT_FOUND;

	DeviceObject = IrpContext->DeviceObject;

	Vcb = (PEXT2_VCB) DeviceObject->DeviceExtension;
	
	Irp = IrpContext->Irp;
	
	io_stack = IoGetCurrentIrpStackLocation(Irp);
	
	if (DeviceObject == Ext2Global->DeviceObject)
	{
		KdPrint(("Ext2Create: Create on main device object.\n"));
		
		Irp->IoStatus.Information = FILE_OPENED;
		Status = STATUS_SUCCESS;
		IrpContext->Irp->IoStatus.Status = Status;
		
		Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
		
		Ext2FreeIrpContext(IrpContext);
		
		return Status;
		
	}
	else if (io_stack->FileObject->FileName.Length == 0)
	{
		io_stack->FileObject->FsContext = Vcb;
		
		ExAcquireResourceExclusiveLite(
			&Vcb->MainResource,
			TRUE );
		
		Vcb->ReferenceCount++;
		
		ExReleaseResourceForThreadLite(
			&Vcb->MainResource,
			ExGetCurrentResourceThread() );
		
		Irp->IoStatus.Information = FILE_OPENED;
		Status = STATUS_SUCCESS;
		IrpContext->Irp->IoStatus.Status = Status;
		
		Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
		
		Ext2FreeIrpContext(IrpContext);
		
		return Status;
	}
	
	__try
	{
		Status = Ext2OpenFile(Vcb, Irp);
	}

	__finally
	{

		if (!IrpContext->ExceptionInProgress)
		{
			IrpContext->Irp->IoStatus.Status = Status;
			
			Ext2CompleteRequest(
				IrpContext->Irp,
				(CCHAR)
				(NT_SUCCESS(Status) ? IO_DISK_INCREMENT : IO_NO_INCREMENT)
				);
			
			Ext2FreeIrpContext(IrpContext);
			
			if (Vcb &&
				 FlagOn(Vcb->Flags, VCB_DISMOUNT_PENDING) &&
				!Vcb->ReferenceCount )
			{
				Ext2FreeVcb(Vcb);
			}
		}
	}
	
	return Status;
}
