/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             Ext2fs.h
 * PURPOSE:          Header file: ext2 structures
 * PROGRAMMER:       Matt Wu <mattwu@163.com>
 * HOMEPAGE:		 http://ext2.yeah.net
 * UPDATE HISTORY: 
 */

#ifndef _EXT2_HEADER_
#define _EXT2_HEADER_

/* INCLUDES *****************************************************************/

#include <ntdddisk.h>

/* DEBUG *****************************************************************/
#undef  DBG
#define DBG 1

#define EXT2_UNLOAD	1

/* STRUCTS & CONSTS******************************************************/

#define EXT2_RO

/* Ext2 releated ******************************************************/
#pragma pack(1)

#define SECTOR_SIZE (512)

typedef struct ext2_super_block {
	ULONG	s_inodes_count;		/* Inodes count */
	ULONG	s_blocks_count;		/* Blocks count */
	ULONG	s_r_blocks_count;	/* Reserved blocks count */
	ULONG	s_free_blocks_count;	/* Free blocks count */
	ULONG	s_free_inodes_count;	/* Free inodes count */
	ULONG	s_first_data_block;	/* First Data Block */
	ULONG	s_log_block_size;	/* Block size */
	LONG	s_log_frag_size;	/* Fragment size */
	ULONG	s_blocks_per_group;	/* # Blocks per group */
	ULONG	s_frags_per_group;	/* # Fragments per group */
	ULONG	s_inodes_per_group;	/* # Inodes per group */
	ULONG	s_mtime;		/* Mount time */
	ULONG	s_wtime;		/* Write time */
	USHORT	s_mnt_count;		/* Mount count */
	SHORT	s_max_mnt_count;	/* Maximal mount count */
	USHORT	s_magic;		/* Magic signature */
	USHORT	s_state;		/* File system state */
	USHORT	s_errors;		/* Behaviour when detecting errors */
	USHORT	s_minor_rev_level; 	/* minor revision level */
	ULONG	s_lastcheck;		/* time of last check */
	ULONG	s_checkinterval;	/* max. time between checks */
	ULONG	s_creator_os;		/* OS */
	ULONG	s_rev_level;		/* Revision level */
	USHORT	s_def_resuid;		/* Default uid for reserved blocks */
	USHORT	s_def_resgid;		/* Default gid for reserved blocks */
	/*
	 * These fields are for EXT2_DYNAMIC_REV superblocks only.
	 *
	 * Note: the difference between the compatible feature set and
	 * the incompatible feature set is that if there is a bit set
	 * in the incompatible feature set that the kernel doesn't
	 * know about, it should refuse to mount the filesystem.
	 * 
	 * e2fsck's requirements are more strict; if it doesn't know
	 * about a feature in either the compatible or incompatible
	 * feature set, it must abort and not try to meddle with
	 * things it doesn't understand...
	 */
	ULONG	s_first_ino; 		/* First non-reserved inode */
	USHORT  s_inode_size; 		/* size of inode structure */
	USHORT	s_block_group_nr; 	/* block group # of this superblock */
	ULONG	s_feature_compat; 	/* compatible feature set */
	ULONG	s_feature_incompat; 	/* incompatible feature set */
	ULONG	s_feature_ro_compat; 	/* readonly-compatible feature set */
	ULONG	s_reserved[230];	/* Padding to the end of the block */
} EXT2_SUPER_BLOCK, *PEXT2_SUPER_BLOCK;

/*
 * Codes for operating systems
 */
#define EXT2_OS_LINUX		0
#define EXT2_OS_HURD		1
#define EXT2_OS_MASIX		2
#define EXT2_OS_FREEBSD		3
#define EXT2_OS_LITES		4

/*
 * Revision levels
 */
#define EXT2_GOOD_OLD_REV	0	/* The good old (original) format */
#define EXT2_DYNAMIC_REV	1 	/* V2 format w/ dynamic inode sizes */

#define EXT2_CURRENT_REV	EXT2_GOOD_OLD_REV
#define EXT2_MAX_SUPP_REV	EXT2_DYNAMIC_REV

/*
 * The second extended file system magic number
 */
#define EXT2_SUPER_MAGIC	0xEF53

#define EXT2_MIN_BLOCK  1024
#define EXT2_MIN_FRAG   1024

/*
 * (Your local time - GMT) *3600 
 */
#define TIMEZONEDIFF    7200
#define TIMEZONE    TIMEZONEDIFF+14400

/*
 * Constants relative to the data blocks
 */
#define	EXT2_NDIR_BLOCKS		12
#define	EXT2_IND_BLOCK			EXT2_NDIR_BLOCKS
#define	EXT2_DIND_BLOCK			(EXT2_IND_BLOCK + 1)
#define	EXT2_TIND_BLOCK			(EXT2_DIND_BLOCK + 1)
#define	EXT2_N_BLOCKS			(EXT2_TIND_BLOCK + 1)

/*
 * Structure of an inode on the disk
 */
typedef struct ext2_inode {
	USHORT	i_mode;		/* File mode */
	USHORT	i_uid;		/* Owner Uid */
	ULONG	i_size;		/* Size in bytes */
	ULONG	i_atime;	/* Access time */
	ULONG	i_ctime;	/* Creation time */
	ULONG	i_mtime;	/* Modification time */
	ULONG	i_dtime;	/* Deletion Time */
	USHORT	i_gid;		/* Group Id */
	USHORT	i_links_count;	/* Links count */
	ULONG	i_blocks;	/* Blocks count */
	ULONG	i_flags;	/* File flags */
	union {
		struct {
			ULONG  l_i_reserved1;
		} linux1;
		struct {
			ULONG  h_i_translator;
		} hurd1;
		struct {
			ULONG  m_i_reserved1;
		} masix1;
	} osd1;				/* OS dependent 1 */
	ULONG	i_block[EXT2_N_BLOCKS];/* Pointers to blocks */
	ULONG	i_version;	/* File version (for NFS) */
	ULONG	i_file_acl;	/* File ACL */
	ULONG	i_dir_acl;	/* Directory ACL */
	ULONG	i_faddr;	/* Fragment address */
	union {
		struct {
			UCHAR	l_i_frag;	/* Fragment number */
			UCHAR	l_i_fsize;	/* Fragment size */
			USHORT	i_pad1;
			ULONG	l_i_reserved2[2];
		} linux2;
		struct {
			UCHAR	h_i_frag;	/* Fragment number */
			UCHAR	h_i_fsize;	/* Fragment size */
			USHORT	h_i_mode_high;
			USHORT	h_i_uid_high;
			USHORT	h_i_gid_high;
			ULONG	h_i_author;
		} hurd2;
		struct {
			UCHAR	m_i_frag;	/* Fragment number */
			UCHAR	m_i_fsize;	/* Fragment size */
			USHORT	m_pad1;
			ULONG	m_i_reserved2[2];
		} masix2;
	} osd2;				/* OS dependent 2 */
} EXT2_INODE, *PEXT2_INODE;

#if defined(__KERNEL__) || defined(__linux__)
#define i_reserved1	osd1.linux1.l_i_reserved1
#define i_frag		osd2.linux2.l_i_frag
#define i_fsize		osd2.linux2.l_i_fsize
#define i_reserved2	osd2.linux2.l_i_reserved2
#endif

#ifdef	__hurd__
#define i_translator	osd1.hurd1.h_i_translator
#define i_frag		osd2.hurd2.h_i_frag;
#define i_fsize		osd2.hurd2.h_i_fsize;
#define i_uid_high	osd2.hurd2.h_i_uid_high
#define i_gid_high	osd2.hurd2.h_i_gid_high
#define i_author	osd2.hurd2.h_i_author
#endif

#ifdef	__masix__
#define i_reserved1	osd1.masix1.m_i_reserved1
#define i_frag		osd2.masix2.m_i_frag
#define i_fsize		osd2.masix2.m_i_fsize
#define i_reserved2	osd2.masix2.m_i_reserved2
#endif

/*
 * Inode flags
 */

#define S_IFMT   0x0F000			/*017 0000 */

#define S_IFSOCK 0x0C000			/*014 0000 */
#define S_IFLNK  0x0A000			/*012 0000 */
#define S_IFFIL  0x08000			/*010 0000 */
#define S_IFBLK  0x06000			/*006 0000 */
#define S_IFDIR  0x04000			/*004 0000 */
#define S_IFCHR  0x02000			/*002 0000 */
#define S_IFIFO  0x01000			/*001 0000 */

#define S_ISSOCK(m)     (((m) & S_IFMT) == S_IFSOCK)
#define S_ISLNK(m)      (((m) & S_IFMT) == S_IFLNK)
#define S_ISFIL(m)      (((m) & S_IFMT) == S_IFFIL)
#define S_ISBLK(m)      (((m) & S_IFMT) == S_IFBLK)
#define S_ISDIR(m)      (((m) & S_IFMT) == S_IFDIR)
#define S_ISCHR(m)      (((m) & S_IFMT) == S_IFCHR)
#define S_ISFIFO(m)     (((m) & S_IFMT) == S_IFIFO)

/*not yet supported: */
#define S_IFUID  0x00800			/*000 4000 */
#define S_IFGID  0x00400			/*000 2000 */
#define S_IFVTX  0x00200			/*000 1000 */

#define	EXT2_SECRM_FL			0x00000001 /* Secure deletion */
#define	EXT2_UNRM_FL			0x00000002 /* Undelete */
#define	EXT2_COMPR_FL			0x00000004 /* Compress file */
#define EXT2_SYNC_FL			0x00000008 /* Synchronous updates */
#define EXT2_IMMUTABLE_FL		0x00000010 /* Immutable file */
#define EXT2_APPEND_FL			0x00000020 /* writes to file may only append */
#define EXT2_NODUMP_FL			0x00000040 /* do not dump file */
#define EXT2_RESERVED_FL		0x80000000 /* reserved for ext2 lib */

/*
 * Structure of a blocks group descriptor
 */
typedef struct ext2_group_desc
{
	ULONG	bg_block_bitmap;		/* Blocks bitmap block */
	ULONG	bg_inode_bitmap;		/* Inodes bitmap block */
	ULONG	bg_inode_table;		/* Inodes table block */
	USHORT	bg_free_blocks_count;	/* Free blocks count */
	USHORT	bg_free_inodes_count;	/* Free inodes count */
	USHORT	bg_used_dirs_count;	/* Directories count */
	USHORT	bg_pad;
	ULONG	bg_reserved[3];
} EXT2_GROUP_DESC, *PEXT2_GROUP_DESC;

#define EXT2_NAME_LEN 255

typedef struct ext2_dir_entry {
	ULONG	inode;			/* Inode number */
	USHORT	rec_len;		/* Directory entry length */
	USHORT	name_len;		/* Name length */
	char	name[EXT2_NAME_LEN];	/* File name */
} EXT2_DIR_ENTRY, *PEXT2_DIR_ENTRY;

/* File System releated ******************************************************/

#define DRIVER_NAME     "Ext2fs"
#define DEVICE_NAME     L"\\Ext2fs"

#if EXT2_UNLOAD
#define DOS_DEVICE_NAME L"\\DosDevices\\Ext2fs"

//
// Private IOCTL to make the driver ready to unload
//
#define IOCTL_PREPARE_TO_UNLOAD \
CTL_CODE(FILE_DEVICE_UNKNOWN, 2048, METHOD_NEITHER, FILE_WRITE_ACCESS)

#endif

#ifndef SetFlag
#define SetFlag(x,f)    ((x) |= (f))
#endif

#ifndef ClearFlag
#define ClearFlag(x,f)  ((x) &= ~(f))
#endif

//
// EXT2_IDENTIFIER_TYPE
//
// Identifiers used to mark the structures
//
typedef enum _EXT2_IDENTIFIER_TYPE {
	FGD = ':DGF',
	VCB = ':BCV',
	FCB = ':BCF',
	CCB = ':BCC',
	ICX = ':XCI',
	FSD = ':DSF'
} EXT2_IDENTIFIER_TYPE;

//
// EXT2_IDENTIFIER
//
// Header used to mark the structures
//
typedef struct _EXT2_IDENTIFIER {
	EXT2_IDENTIFIER_TYPE     Type;
	ULONG                    Size;
} EXT2_IDENTIFIER, *PEXT2_IDENTIFIER;


#define NodeType(Ptr) (*((EXT2_IDENTIFIER_TYPE *)(Ptr)))

//
// EXT2_GLOBAL_DATA
//
// Data that is not specific to a mounted volume
//
typedef struct _EXT2_GLOBAL {
	
	// Identifier for this structure
	EXT2_IDENTIFIER             Identifier;
	
	// Syncronization primitive for this structure
	ERESOURCE                   Resource;
	
	// Table of pointers to the fast I/O entry points
	FAST_IO_DISPATCH            FastIoDispatch;
	
	// Table of pointers to the Cache Manager callbacks
	CACHE_MANAGER_CALLBACKS     CacheManagerCallbacks;
	CACHE_MANAGER_CALLBACKS		CacheManagerNoOpCallbacks;
	
	// Pointer to the driver object
	PDRIVER_OBJECT              DriverObject;
	
	// Pointer to the main device object
	PDEVICE_OBJECT              DeviceObject;
	
	// List of mounted volumes
	LIST_ENTRY                  VcbList;

	// Look Aside table of IRP_CONTEXT
	USHORT						MaxDepth;
	NPAGED_LOOKASIDE_LIST		Ext2IrpContextLookasideList;
	
	// Global flags for the driver
	ULONG                       Flags;
	
	LARGE_INTEGER				TimeZone;
	
} EXT2_GLOBAL, *PEXT2_GLOBAL;

//
// Flags for EXT2_GLOBAL_DATA
//
#define EXT2_UNLOAD_PENDING 0x00000001

//
// Driver Extension define
//
typedef struct {
	EXT2_GLOBAL	Ext2Global;
} EXT2FS_EXT, *PEXT2FS_EXT;


typedef struct _EXT2_FCBVCB {
	
	// FCB header required by NT
	FSRTL_COMMON_FCB_HEADER         CommonFCBHeader;
	SECTION_OBJECT_POINTERS         SectionObject;
	ERESOURCE                       MainResource;
	ERESOURCE                       PagingIoResource;
	// end FCB header required by NT
	
	// Identifier for this structure
	EXT2_IDENTIFIER                  Identifier;
}EXT2_FCBVCB, *PEXT2_FCBVCB;

//
//  The Vcb (Volume control Block) record corresponds to every volume mounted
//  by the file system.  They are ordered in a queue off of FatData.VcbQueue.
//  This structure must be allocated from non-paged pool
//

typedef enum _VCB_CONDITION {
    VcbGood = 1,
    VcbNotMounted,
    VcbBad
} VCB_CONDITION;


//
// EXT2_VCB Volume Control Block
//
// Data that represents a mounted logical volume
// It is allocated as the device extension of the volume device object
//
typedef struct _EXT2_VCB {
	
	// FCB header required by NT
	// The VCB is also used as an FCB for file objects
	// that represents the volume itself
	FSRTL_COMMON_FCB_HEADER     CommonFCBHeader;
	SECTION_OBJECT_POINTERS     SectionObject;
	ERESOURCE                   MainResource;
	ERESOURCE                   PagingIoResource;
	// end FCB header required by NT
	
	// Identifier for this structure
	EXT2_IDENTIFIER             Identifier;
	
	LIST_ENTRY                  Next;
	
	// Count Lock
	ERESOURCE                   CountResource;

	// Incremented on IRP_MJ_CREATE, decremented on IRP_MJ_CLEANUP
	// for files on this volume.
	ULONG                       OpenFileHandleCount;
	
	// Incremented on IRP_MJ_CREATE, decremented on IRP_MJ_CLOSE
	// for both files on this volume and open instances of the
	// volume itself.
	ULONG                       ReferenceCount;
	
	// Pointer to the VPB in the target device object
	PVPB                        Vpb;

    //
    //  The internal state of the device.  This is a collection of fsd device
    //  state flags.
    //

    ULONG						VcbState;
    VCB_CONDITION				VcbCondition;

	// List of FCBs for open files on this volume
	LIST_ENTRY                  FcbList;

    // List of IRPs pending on directory change notify requests
    LIST_ENTRY                  NotifyList;

    // Pointer to syncronization primitive for this list
    PNOTIFY_SYNC                NotifySync;
	
	// This volumes device object
	PDEVICE_OBJECT              DeviceObject;
	
	// The physical device object (the disk)
	PDEVICE_OBJECT              TargetDeviceObject;
	
	// Information about the physical device object
	DISK_GEOMETRY               DiskGeometry;
	PARTITION_INFORMATION       PartitionInformation;
	
	// Pointer to the file system superblock
	PEXT2_SUPER_BLOCK			ext2_super_block;
	PEXT2_GROUP_DESC			ext2_group_desc;
	
	// Number of Group Decsciptions
	ULONG						ext2_groups;
	
	// Block and fragment size
	ULONG						ext2_block;
	ULONG						ext2_frag;
	
	// Flags for the volume
	ULONG                       Flags;

	// Streaming File Object
	PFILE_OBJECT					StreamObj;

	
} EXT2_VCB, *PEXT2_VCB;

//
// Flags for EXT2_VCB
//
#define VCB_VOLUME_LOCKED       0x00000001
#define VCB_DISMOUNT_PENDING    0x00000002
#define VCB_READ_ONLY           0x00000004

//
// EXT2_FCB File Control Block
//
// Data that represents an open file
// There is a single instance of the FCB for every open file
//
typedef struct _EXT2_FCB {
	
	// FCB header required by NT
	FSRTL_COMMON_FCB_HEADER         CommonFCBHeader;
	SECTION_OBJECT_POINTERS         SectionObject;
	ERESOURCE                       MainResource;
	ERESOURCE                       PagingIoResource;
	// end FCB header required by NT
	
	// Identifier for this structure
	EXT2_IDENTIFIER                  Identifier;
	
	// List of FCBs for this volume
	LIST_ENTRY                      Next;
	
#ifndef EXT2_RO
	
	// Share Access for the file object
	SHARE_ACCESS                    ShareAccess;
	
#endif

	// Count Lock
	ERESOURCE                   CountResource;

	// List of byte-range locks for this file
	FILE_LOCK_ANCHOR                FileLockAnchor;

	// Incremented on IRP_MJ_CREATE, decremented on IRP_MJ_CLEANUP
	ULONG                           OpenHandleCount;
	
	// Incremented on IRP_MJ_CREATE, decremented on IRP_MJ_CLOSE
	ULONG                           ReferenceCount;
	
	// The filename
	UNICODE_STRING                  FileName;
	ANSI_STRING                     AnsiFileName;	
	
	// The file attributes
	ULONG                           FileAttributes;
	
	// The offset in it's directory
	LARGE_INTEGER                   IndexNumber;

    // Flags for the FCB
    ULONG                           Flags;
	
	// Pointer to the inode
	struct ext2_inode*              ext2_inode;
	
	// Inode number and it's directory's inode number
	ULONG							inode;
	ULONG							dir_inode;


	// Parent Fcb
	struct _EXT2_FCB*				ParentFcb;

	// Streaming File Object
	PFILE_OBJECT					StreamObj;
	
} EXT2_FCB, *PEXT2_FCB;


//
// Flags for EXT2_FCB
//
#define FCB_PAGE_FILE               0x00000001
#define FCB_DELETE_PENDING          0x00000002

//
// EXT2_CCB Context Control Block
//
// Data that represents one instance of an open file
// There is one instance of the CCB for every instance of an open file
//
typedef struct _EXT2_CCB {
	
	// Identifier for this structure
	EXT2_IDENTIFIER  Identifier;
	
	// State that may need to be maintained
	ULONG           CurrentByteOffset;
	UNICODE_STRING  DirectorySearchPattern;
	
} EXT2_CCB, *PEXT2_CCB;

//
// EXT2_IRP_CONTEXT
//
// Used to pass information about a request between the drivers functions
//
typedef struct _EXT2_IRP_CONTEXT {
	
	// Identifier for this structure
	EXT2_IDENTIFIER     Identifier;
	
	// Pointer to the IRP this request describes
	PIRP                Irp;

	// Flags
	ULONG				Flags;
	
	// The major and minor function code for the request
	UCHAR               MajorFunction;
	UCHAR               MinorFunction;
	
	// The device object
	PDEVICE_OBJECT      DeviceObject;
	
	// The file object
	PFILE_OBJECT        FileObject;
	
	// If the request is synchronous (we are allowed to block)
	BOOLEAN             IsSynchronous;
	
	// If the request is top level
	BOOLEAN             IsTopLevel;
	
	// Used if the request needs to be queued for later processing
	WORK_QUEUE_ITEM     WorkQueueItem;
	
	// If an exception is currently in progress
	BOOLEAN             ExceptionInProgress;
	
	// The exception code when an exception is in progress
	NTSTATUS            ExceptionCode;
	
} EXT2_IRP_CONTEXT, *PEXT2_IRP_CONTEXT;


#define IRP_CONTEXT_FLAG_FROM_POOL       (0x00000001)
#define IRP_CONTEXT_FLAG_WAIT            (0x00000002)
#define IRP_CONTEXT_FLAG_WRITE_THROUGH   (0x00000004)
#define IRP_CONTEXT_FLAG_FLOPPY          (0x00000008)
#define IRP_CONTEXT_FLAG_RECURSIVE_CALL  (0x00000010)
#define IRP_CONTEXT_FLAG_DISABLE_POPUPS  (0x00000020)
#define IRP_CONTEXT_FLAG_DEFERRED_WRITE  (0x00000040)
#define IRP_CONTEXT_FLAG_VERIFY_READ     (0x00000080)
#define IRP_CONTEXT_STACK_IO_CONTEXT     (0x00000100)
#define IRP_CONTEXT_FLAG_IN_FSP          (0x00000200)
#define IRP_CONTEXT_FLAG_USER_IO         (0x00000400)       // for performance counters

//
// EXT2_ALLOC_HEADER
//
// In the checked version of the driver this header is put in the beginning of
// every memory allocation
//
typedef struct _EXT2_ALLOC_HEADER {
	EXT2_IDENTIFIER Identifier;
} EXT2_ALLOC_HEADER, *PEXT2_ALLOC_HEADER;

typedef struct _FCB_LIST_ENTRY {
	PEXT2_FCB    Fcb;
	LIST_ENTRY   Next;
} FCB_LIST_ENTRY, *PFCB_LIST_ENTRY;


// Block Description List
typedef struct _EXT2_BDL {
	LONGLONG	Lba;
	ULONG		Offset;
	ULONG		Length;
	PIRP		Irp;
} EXT2_BDL, *PEXT2_BDL;

#pragma pack()

/* FUNCTIONS DECLARATION *****************************************************/


//
//  The following macro is used to determine if an FSD thread can block
//  for I/O or wait for a resource.  It returns TRUE if the thread can
//  block and FALSE otherwise.  This attribute can then be used to call
//  the FSD & FSP common work routine with the proper wait value.
//

#define CanExt2Wait(IRP) IoIsOperationSynchronous(Irp)


LARGE_INTEGER
Ext2SysTime (IN ULONG i_time);

ULONG
Ext2InodeTime (IN LARGE_INTEGER SysTime);

VOID
Ext2SyncUninitializeCacheMap (
    IN PFILE_OBJECT FileObject    );

BOOLEAN 
Ext2CopyRead(
    IN PFILE_OBJECT  FileObject,
    IN PLARGE_INTEGER  FileOffset,
    IN ULONG  Length,
    IN BOOLEAN  Wait,
    OUT PVOID  Buffer,
    OUT PIO_STATUS_BLOCK  IoStatus   );

NTSTATUS
Ext2ReadBlocks(
    IN PEXT2_IRP_CONTEXT IrpContext,
    IN PEXT2_VCB		Vcb,
	IN PEXT2_BDL		Ext2BDL,
    IN ULONG			Count,
    IN BOOLEAN			bVerify );

NTSTATUS
Ext2ReadSync(
	IN PDEVICE_OBJECT	DeviceObject,
	IN LONGLONG			Offset,
	IN ULONG			Length,
	OUT PVOID			Buffer,
	BOOLEAN				bVerify	);

NTSTATUS
Ext2ReadDisk(IN PDEVICE_OBJECT pDeviceObject,
	     IN ULONG		lba,
	     IN ULONG		offset,
	     IN ULONG		Size,
	     IN PVOID		Buffer);

NTSTATUS 
Ext2DiskIoControl (IN PDEVICE_OBJECT   pDeviceObject,
		   IN ULONG            IoctlCode,
		   IN PVOID            InputBuffer,
		   IN ULONG            InputBufferSize,
		   IN OUT PVOID        OutputBuffer,
		   IN OUT PULONG       OutputBufferSize);

NTSTATUS
Ext2ReadDiskOverrideVerify (IN PDEVICE_OBJECT pDeviceObject,
			    IN ULONG		DiskSector, 
			    IN ULONG		SectorCount,
			    IN OUT PUCHAR	Buffer);

struct ext2_super_block *
Ext2LoadSuper(IN PDEVICE_OBJECT pDeviceObject);

NTSTATUS
Ext2SaveSuper(IN PDEVICE_OBJECT pDeviceObject,
	      IN struct ext2_super_block * sb);

struct ext2_group_desc *
Ext2LoadGroup(IN PEXT2_VCB vcb);

NTSTATUS
Ext2SaveGroup(IN PEXT2_VCB vcb,
	      IN struct ext2_group_desc * gd);
BOOLEAN
Ext2GetInodeLba (IN PEXT2_VCB vcb,
		 IN ULONG inode,
		 OUT PLONGLONG offset);

BOOLEAN
Ext2LoadInode (IN PEXT2_VCB vcb,
	       IN ULONG inode,
	       IN struct ext2_inode *ext2_inode);
BOOLEAN
Ext2SaveInode (IN PEXT2_VCB vcb,
	       IN ULONG inode,
	       IN struct ext2_inode *ext2_inode);

ULONG Ext2GetBlock(IN PEXT2_VCB vcb,
		   ULONG dwContent,
		   ULONG Index,
		   int layer	);

ULONG Ext2BlockMap(IN PEXT2_VCB vcb,
	       IN struct ext2_inode* ext2_inode,
	       IN ULONG Index	);

ULONG Ext2BuildBDL(IN PEXT2_VCB Vcb,
					 IN struct ext2_inode* ext2_inode,
					 IN ULONG offset, 
					 IN ULONG size, 
					 OUT PEXT2_BDL* ext2_bdl);

NTSTATUS
Ext2ReadInode (
			IN PEXT2_IRP_CONTEXT	IrpContext,
			IN PEXT2_VCB vcb,
			IN struct ext2_inode* ext2_inode,
			IN ULONG offset,
			IN PVOID Buffer,
			IN ULONG size,
			OUT PULONG dwReturn);

PEXT2_IRP_CONTEXT
Ext2AllocateIrpContext (IN PDEVICE_OBJECT   DeviceObject,
			IN PIRP             Irp );
VOID
Ext2FreeIrpContext (IN PEXT2_IRP_CONTEXT IrpContext);

PEXT2_FCB
Ext2AllocateFcb (IN PEXT2_VCB   Vcb,
		 IN PUNICODE_STRING     FileName,
		 IN ULONG               IndexNumber,
		 IN ULONG				inode,
		 IN ULONG				dir_inode,
		 IN struct ext2_inode*  ext2_inode );

VOID
Ext2FreeFcb (IN PEXT2_FCB Fcb);

PEXT2_CCB
Ext2AllocateCcb (VOID);

VOID
Ext2FreeCcb (IN PEXT2_CCB Ccb);

VOID
Ext2FreeVcb (IN PEXT2_VCB Vcb );


BOOLEAN
Ext2FastIoQueryBasicInfo (IN PFILE_OBJECT             FileObject,
			  IN BOOLEAN                  Wait,
			  OUT PFILE_BASIC_INFORMATION Buffer,
			  OUT PIO_STATUS_BLOCK        IoStatus,
			  IN PDEVICE_OBJECT           DeviceObject);

BOOLEAN
Ext2FastIoQueryStandardInfo (IN PFILE_OBJECT                 FileObject,
			     IN BOOLEAN                      Wait,
			     OUT PFILE_STANDARD_INFORMATION  Buffer,
			     OUT PIO_STATUS_BLOCK            IoStatus,
			     IN PDEVICE_OBJECT               DeviceObject);

BOOLEAN
Ext2FastIoLock (
	       IN PFILE_OBJECT         FileObject,
	       IN PLARGE_INTEGER       FileOffset,
	       IN PLARGE_INTEGER       Length,
	       IN PEPROCESS            Process,
	       IN ULONG                Key,
	       IN BOOLEAN              FailImmediately,
	       IN BOOLEAN              ExclusiveLock,
	       OUT PIO_STATUS_BLOCK    IoStatus,
	       IN PDEVICE_OBJECT       DeviceObject
	       );

BOOLEAN
Ext2FastIoUnlockSingle (
		       IN PFILE_OBJECT         FileObject,
		       IN PLARGE_INTEGER       FileOffset,
		       IN PLARGE_INTEGER       Length,
		       IN PEPROCESS            Process,
		       IN ULONG                Key,
		       OUT PIO_STATUS_BLOCK    IoStatus,
		       IN PDEVICE_OBJECT       DeviceObject
		       );

BOOLEAN
Ext2FastIoUnlockAll (
		    IN PFILE_OBJECT         FileObject,
		    IN PEPROCESS            Process,
		    OUT PIO_STATUS_BLOCK    IoStatus,
		    IN PDEVICE_OBJECT       DeviceObject
		    );

BOOLEAN
Ext2FastIoUnlockAllByKey (
			 IN PFILE_OBJECT         FileObject,
			 IN PEPROCESS            Process,
			 IN ULONG                Key,
			 OUT PIO_STATUS_BLOCK    IoStatus,
			 IN PDEVICE_OBJECT       DeviceObject
			 );
NTSTATUS
Ext2LockControl (IN PEXT2_IRP_CONTEXT IrpContext);

BOOLEAN
Ext2FastIoQueryNetworkOpenInfo (
	 IN PFILE_OBJECT                     FileObject,
	 IN BOOLEAN                          Wait,
	 OUT PFILE_NETWORK_OPEN_INFORMATION  Buffer,
	 OUT PIO_STATUS_BLOCK                IoStatus,
	 IN PDEVICE_OBJECT                   DeviceObject );

BOOLEAN
Ext2FastIoQueryNetworkOpenInfo (IN PFILE_OBJECT                     FileObject,
				IN BOOLEAN                          Wait,
				OUT PFILE_NETWORK_OPEN_INFORMATION  Buffer,
				OUT PIO_STATUS_BLOCK                IoStatus,
				IN PDEVICE_OBJECT                   DeviceObject);

BOOLEAN
Ext2AcquireForLazyWrite (IN PVOID    Context,
			 IN BOOLEAN  Wait );
VOID
Ext2ReleaseFromLazyWrite (IN PVOID Context);

BOOLEAN
Ext2AcquireForReadAhead (IN PVOID    Context,
			 IN BOOLEAN  Wait );

BOOLEAN
Ext2NoOpAcquire (IN PVOID Fcb,
				 IN BOOLEAN Wait );

VOID
Ext2NoOpRelease (IN PVOID Fcb    );

VOID
Ext2ReleaseFromReadAhead (IN PVOID Context);

NTSTATUS
Ext2Close (IN PEXT2_IRP_CONTEXT IrpContext);

VOID
Ext2QueueCloseRequest (IN PEXT2_IRP_CONTEXT IrpContext);

VOID
Ext2DeQueueCloseRequest (IN PVOID Context);

PEXT2_FCB
Ext2SearchMcb (	IN PEXT2_VCB    Vcb,
				IN ULONG            inode );
PEXT2_FCB
Ext2SearchDirMcb(IN	PEXT2_VCB	Vcb,
				 IN	PUNICODE_STRING	LongFileName,
				 OUT PUNICODE_STRING ShortName);

NTSTATUS
Ext2ScanDir (IN PEXT2_VCB        Vcb,
	     IN ULONG            inode,
	     IN PUNICODE_STRING  FileName,
	     IN OUT PULONG       Index,
	     IN PEXT2_INODE	 ext2_inode,
	     IN PEXT2_DIR_ENTRY  dir_entry);

NTSTATUS
Ext2LookupFileName (IN PEXT2_VCB        Vcb,
		    IN PUNICODE_STRING  FullFileName,
		    IN OUT PULONG       Offset,
		    IN OUT PULONG	Inode,
		    IN OUT PULONG	DirInode,
		    IN OUT PEXT2_INODE  ext2_inode);
NTSTATUS
Ext2OpenFile(PEXT2_VCB Vcb, PIRP Irp);

NTSTATUS
Ext2Create (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2Read (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2QueryInformation (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2SetInformation (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2QueryVolumeInformation (IN PEXT2_IRP_CONTEXT IrpContext);

VOID
Ext2CharToWchar (IN OUT PWCHAR   Destination,
		 IN PCHAR        Source,
		 IN ULONG        Length);
NTSTATUS
Ext2WcharToChar (IN OUT PCHAR    Destination,
		 IN PWCHAR       Source,
		 IN ULONG        Length);

NTSTATUS
Ext2LockUserBuffer (IN PIRP             Irp,
		    IN ULONG            Length,
		    IN LOCK_OPERATION   Operation);
PVOID
Ext2GetUserBuffer (IN PIRP Irp);

ULONG
Ext2GetInfoLength(IN FILE_INFORMATION_CLASS  FileInformationClass);

ULONG
Ext2ProcessDirEntry(IN PEXT2_VCB         Vcb,
		    IN FILE_INFORMATION_CLASS  FileInformationClass,
		    IN ULONG		 in,
		    IN PVOID		 Buffer,
		    IN ULONG		 UsedLength,
		    IN ULONG		 Length,
		    IN ULONG		 FileIndex,
		    IN UNICODE_STRING*	 pName,
		    IN BOOLEAN		 Single );

NTSTATUS
Ext2QueryDirectory (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2NotifyChangeDirectory (
    IN PEXT2_IRP_CONTEXT IrpContext    );

NTSTATUS
Ext2DirectoryControl (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2CompleteIrpContext (
    IN PEXT2_IRP_CONTEXT IrpContext,
    IN NTSTATUS Status );

NTSTATUS
Ext2QueueRequest (IN PEXT2_IRP_CONTEXT IrpContext);

VOID
Ext2DeQueueRequest (IN PVOID Context);

NTSTATUS
Ext2Cleanup (IN PEXT2_IRP_CONTEXT IrpContext);

#if DBG

NTSTATUS
Ext2DeviceControlCompletion (IN PDEVICE_OBJECT   DeviceObject,
			    IN PIRP             Irp,
			    IN PVOID            Context);
#endif

NTSTATUS
Ext2DeviceControlNormal (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2PrepareToUnload (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2DeviceControl (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2VerifyVolume (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2IsVolumeMounted (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2DismountVolume (IN PEXT2_IRP_CONTEXT IrpContext);

VOID
Ext2PurgeVolume (IN PEXT2_VCB Vcb,
		 IN BOOLEAN  FlushBeforePurge);
VOID
Ext2PurgeFile (IN PEXT2_FCB Fcb,
	       IN BOOLEAN  FlushBeforePurge);

NTSTATUS
Ext2LockVolume (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2UnlockVolume (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2UserFsRequest (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2MountVolume (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2FileSystemControl (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2DispatchRequest (IN PEXT2_IRP_CONTEXT IrpContext);

NTSTATUS
Ext2ExceptionFilter (IN PEXT2_IRP_CONTEXT    IrpContext,
		     IN NTSTATUS             ExceptionCode);
NTSTATUS
Ext2ExceptionHandler (IN PEXT2_IRP_CONTEXT IrpContext);

VOID
Ext2SetVpbFlag (IN PVPB     Vpb,
		IN USHORT   Flag );

VOID
Ext2ClearVpbFlag (IN PVPB     Vpb,
		  IN USHORT   Flag );

NTSTATUS
Ext2BuildRequest (IN PDEVICE_OBJECT   DeviceObject,
		  IN PIRP             Irp);

VOID
DriverUnload (IN PDRIVER_OBJECT DriverObject);

BOOLEAN
Ext2FastIoCheckIfPossible (
			  IN PFILE_OBJECT         FileObject,
			  IN PLARGE_INTEGER       FileOffset,
			  IN ULONG                Length,
			  IN BOOLEAN              Wait,
			  IN ULONG                LockKey,
			  IN BOOLEAN              CheckForReadOperation,
			  OUT PIO_STATUS_BLOCK    IoStatus,
			  IN PDEVICE_OBJECT       DeviceObject );

PUCHAR
Ext2NtStatusToString (IN NTSTATUS Status );

#if DBG

extern ULONG ProcessNameOffset;

#define Ext2GetCurrentProcessName() ( \
    (PUCHAR) PsGetCurrentProcess() + ProcessNameOffset \
)

ULONG 
Ext2GetProcessNameOffset (VOID);

BOOLEAN
Ext2FastIoRead (IN PFILE_OBJECT         FileObject,
		IN PLARGE_INTEGER       FileOffset,
		IN ULONG                Length,
		IN BOOLEAN              Wait,
		IN ULONG                LockKey,
		OUT PVOID               Buffer,
		OUT PIO_STATUS_BLOCK    IoStatus,
		IN PDEVICE_OBJECT       DeviceObject);
VOID
Ext2DbgPrintCall (IN PDEVICE_OBJECT   DeviceObject,
		  IN PIRP             Irp );

VOID
Ext2DbgPrintComplete (IN PIRP Irp);

#define Ext2CompleteRequest(Irp, PriorityBoost) \
        Ext2DbgPrintComplete(Irp); \
        IoCompleteRequest(Irp, PriorityBoost)

#else

#define Ext2DbgPrintCall(DeviceObject, Irp)

#define Ext2CompleteRequest(Irp, PriorityBoost) \
        IoCompleteRequest(Irp, PriorityBoost)

#endif /* DBG */

//
// I've heard these declarations is missing sometimes so we include them here
//



#endif /* _EXT2_HEADER_ */
