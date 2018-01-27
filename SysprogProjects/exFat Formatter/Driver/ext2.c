/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             Ext2.c
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
#pragma alloc_text(PAGE, Ext2LoadSuper)
#pragma alloc_text(PAGE, Ext2LoadGroup)
#pragma alloc_text(PAGE, Ext2GetInodeLba)
#pragma alloc_text(PAGE, Ext2LoadInode)
#pragma alloc_text(PAGE, Ext2GetBlock)
#pragma alloc_text(PAGE, Ext2BlockMap)
#endif

/* FUNCTIONS ***************************************************************/

struct ext2_super_block *
Ext2LoadSuper(IN PDEVICE_OBJECT pDeviceObject)
{
	PVOID		Buffer;
	NTSTATUS	Status;
	
	Buffer = ExAllocatePool(NonPagedPool, 2 * SECTOR_SIZE);
	if (!Buffer)
	{
		KdPrint(("Ext2LoadSuper: no enough memory.\n"));
		return NULL;
	}

    Status = Ext2ReadDisk(pDeviceObject, 2, 0, SECTOR_SIZE * 2, Buffer);
	if (!NT_SUCCESS(Status))
	{
		KdPrint(("Ext2ReadDisk: Read Block Device error.\n"));
                ExFreePool(Buffer);
		return NULL;
	}

	return (struct ext2_super_block *)Buffer;
}

struct ext2_group_desc *
Ext2LoadGroup(IN PEXT2_VCB vcb)
{
	ULONG		size;
	PVOID		Buffer;
	LONGLONG	lba;
	IO_STATUS_BLOCK	IoStatus;

	PDEVICE_OBJECT pDeviceObject = vcb->TargetDeviceObject;
	struct ext2_super_block * sb = vcb->ext2_super_block;

	vcb->ext2_block = EXT2_MIN_BLOCK << sb->s_log_block_size;
	vcb->ext2_frag = EXT2_MIN_FRAG << sb->s_log_frag_size;

	vcb->ext2_groups = (sb->s_blocks_count - sb->s_first_data_block +
		sb->s_blocks_per_group - 1) / sb->s_blocks_per_group;

	size = sizeof(struct ext2_group_desc) * vcb->ext2_groups;

	Buffer = ExAllocatePool(NonPagedPool, size);
	if (!Buffer)
	{
		KdPrint(("Ext2LoadSuper: no enough memory.\n"));
		return NULL;
	}

	if (vcb->ext2_block == EXT2_MIN_BLOCK)
	{
		lba = (LONGLONG)2 * vcb->ext2_block;
	}

	if (vcb->ext2_block > EXT2_MIN_BLOCK)
	{
		lba = (LONGLONG) (vcb->ext2_block);
	}

    IoStatus.Status = Ext2ReadDisk(vcb->TargetDeviceObject, (ULONG)(lba / SECTOR_SIZE), 0, size, Buffer);

	if (!NT_SUCCESS(IoStatus.Status))
	{
		ExFreePool(Buffer);
		Buffer = NULL;
	}

	return (struct ext2_group_desc *)Buffer;
}

BOOLEAN
Ext2GetInodeLba (IN PEXT2_VCB	vcb,
		 IN	 ULONG	inode,
		 OUT PLONGLONG offset)
{
	LONGLONG loc;

	if (inode < 1 || inode > vcb->ext2_super_block->s_inodes_count)
	{
		KdPrint(("Ext2GetInodeLba: Inode value %xh is invalid.\n",inode));
		*offset = 0;
		return FALSE;
	}

	loc = (vcb->ext2_group_desc[(inode - 1) / vcb->ext2_super_block->s_inodes_per_group].bg_inode_table);
	loc = loc * vcb->ext2_block;
	loc = loc + ((inode - 1) % (vcb->ext2_super_block->s_inodes_per_group)) * sizeof(struct ext2_inode);

	*offset = loc;

//	KdPrint(("Ext2GetInodeLba: inode=%xh lba=%xh offset=%xh\n",
//		inode, *lba, *offset));
	return TRUE;
}

BOOLEAN
Ext2LoadInode (IN PEXT2_VCB	vcb,
	       IN ULONG		inode,
	       IN struct ext2_inode *ext2_inode)
{
	IO_STATUS_BLOCK		IoStatus;
	LONGLONG			Offset;	

	if (!Ext2GetInodeLba(vcb, inode, &Offset))
	{
		KdPrint(("Ext2LoadInode: error get inode(%xh)'s addr.\n", inode));
		return FALSE;
	}

	if (!Ext2CopyRead(
			vcb->StreamObj,
			(PLARGE_INTEGER)&Offset,
			sizeof(EXT2_INODE),
			TRUE,
			(PVOID)ext2_inode,
			&IoStatus ));

	if (!NT_SUCCESS(IoStatus.Status))
	{
		return FALSE;
	}

	return TRUE;
}

/*
BOOLEAN
Ext2SaveInode (IN PEXT2_VCB vcb,
	       IN ULONG inode,
	       IN struct ext2_inode *ext2_inode)
{
	ULONG		lba;
	ULONG		offset;
	NTSTATUS	Status;

	if (!Ext2GetInodeLba(vcb, inode, &lba, &offset))
	{
		KdPrint(("Ext2LoadInode: error get inode(%xh)'s addr.\n", inode));
		return FALSE;
	}

	Status = Ext2WriteDisk(vcb->TargetDeviceObject,
		lba,
		offset,
		sizeof(EXT2_INODE),
		(PVOID)ext2_inode);

	if (!NT_SUCCESS(Status))
	{
		return FALSE;
	}

	return TRUE;
}
*/

ULONG Ext2GetBlock(IN PEXT2_VCB vcb,
		   ULONG dwContent,
		   ULONG Index,
		   int	 layer )
{
	ULONG		*pData = NULL;
	LONGLONG	Offset = 0;
	ULONG		i = 0, j = 0, temp = 1;
	ULONG		dwBlk = 0;
	IO_STATUS_BLOCK	IoStatus;


	Offset = (LONGLONG) dwContent;
	Offset = Offset * vcb->ext2_block;

	if (layer == 0)
	{
		dwBlk = dwContent;
	}
	else if (layer <= 3)
	{
		pData = (ULONG *)ExAllocatePool(NonPagedPool,
					vcb->ext2_block);
		if (!pData)
		{
			KdPrint(("Ext2GetBlock: no enough memory.\n"));
			return dwBlk;
		}

		if (!Ext2CopyRead(
				vcb->StreamObj,
				(PLARGE_INTEGER)&Offset,
				vcb->ext2_block,
				TRUE,
				pData,
				&IoStatus ));

		if (!NT_SUCCESS(IoStatus.Status))
		{
			return 0;
		}

		for (i=0; i<(ULONG)(layer - 1); i++)
		{
			temp *= (vcb->ext2_block/4);
		}

		i = Index / temp;
		j = Index % temp;

		dwBlk = Ext2GetBlock(vcb, pData[i], j, layer - 1);

		ExFreePool(pData);
	}
	
	return dwBlk;
}

ULONG Ext2BlockMap(IN PEXT2_VCB vcb,
		   IN struct ext2_inode* ext2_inode,
		   IN ULONG Index)
{
	ULONG dwSizes[3] = {vcb->ext2_block / 4, 0, 0};
	int   i;

	if (Index >= ext2_inode->i_blocks)
	{
		KdPrint(("Ext2BlockMap: error input paramters.\n"));
		return 0;
	}

	if (Index < 12)
	{
		return ext2_inode->i_block[Index];
	}

	Index -= 12;

	for (i = 0; i < 3; i++)
	{
		if (i > 0)
		{
			dwSizes[i] = vcb->ext2_block/4 * dwSizes[i-1];
		}
		if (Index < dwSizes[i])
		{
			return Ext2GetBlock(vcb, ext2_inode->i_block[i + 12], Index , i + 1); 
		}
		Index -= dwSizes[i];
	}

	return 0;
}



ULONG Ext2BuildBDL(IN PEXT2_VCB Vcb,
					 IN struct ext2_inode* ext2_inode,
					 IN ULONG offset, 
					 IN ULONG size, 
					 OUT PEXT2_BDL *ext2_bdl)
{
	ULONG	nBeg, nEnd, nBlocks;
	ULONG	dwBlk;
	ULONG   i;
	LONGLONG lba;

	PEXT2_BDL	ext2bdl;

	*ext2_bdl = NULL;

	if (offset >= ext2_inode->i_size)
	{
		KdPrint(("Ext2BuildBDL: beyond the file range.\n"));
		return 0;
	}

	if (offset + size > ext2_inode->i_size)
	{
		size = ext2_inode->i_size - offset;
	}

	nBeg = offset / Vcb->ext2_block;
	nEnd = (size + offset + Vcb->ext2_block - 1) / Vcb->ext2_block;

	nBlocks = nEnd - nBeg;

	if (nBlocks > 0)
	{
		ext2bdl = ExAllocatePool(PagedPool, sizeof(EXT2_BDL) * nBlocks);

		if (ext2bdl)
		{

			RtlZeroMemory(ext2bdl, sizeof(EXT2_BDL) * nBlocks);

			for (i = nBeg; i < nEnd; i++)
			{
				dwBlk = Ext2BlockMap(Vcb, ext2_inode, i);

				lba = (LONGLONG) dwBlk;
				lba = lba * Vcb->ext2_block;

				ext2bdl[i - nBeg].Lba = lba;
				ext2bdl[i - nBeg].Length = Vcb->ext2_block;
				ext2bdl[i - nBeg].Offset = (i - nBeg) * (Vcb->ext2_block);
			}

			*ext2_bdl = ext2bdl;
			return nBlocks;
		}
	}

	// Error
	return 0;
}
