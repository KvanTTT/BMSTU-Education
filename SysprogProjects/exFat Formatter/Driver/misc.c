/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             misc.c
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
#pragma alloc_text(PAGE, Ext2SysTime)
#pragma alloc_text(PAGE, Ext2InodeTime)
#pragma alloc_text(PAGE, Ext2CharToWchar)
#pragma alloc_text(PAGE, Ext2WcharToChar)
#endif


LARGE_INTEGER
Ext2SysTime (IN ULONG i_time)
{
	LARGE_INTEGER SysTime;
	SysTime.QuadPart = (LONGLONG)(i_time) * 10000000;
	SysTime.QuadPart += Ext2Global->TimeZone.QuadPart;
	return SysTime;
}

ULONG
Ext2InodeTime (IN LARGE_INTEGER SysTime)
{
	return (ULONG)((SysTime.QuadPart - Ext2Global->TimeZone.QuadPart) / 10000000);
}

VOID
Ext2CharToWchar (IN OUT PWCHAR   Destination,
		 IN PCHAR        Source,
		 IN ULONG        Length)
{
	ULONG Index;
	
	ASSERT(Destination != NULL);
	ASSERT(Source != NULL);
	
	for (Index = 0; Index < Length; Index++)
	{
		Destination[Index] = (WCHAR) Source[Index];
	}
}

NTSTATUS
Ext2WcharToChar (IN OUT PCHAR    Destination,
		 IN PWCHAR       Source,
		 IN ULONG        Length)
{
	ULONG       Index;
	NTSTATUS    Status;
	
	ASSERT(Destination != NULL);
	ASSERT(Source != NULL);
	
	for (Index = 0, Status = STATUS_SUCCESS; Index < Length; Index++)
	{
		Destination[Index] = (CHAR) Source[Index];
		
		//
		// Check that the wide character fits in a normal character
		// but continue with the conversion anyway
		//
		if ( ((WCHAR) Destination[Index]) != Source[Index] )
		{
			Status = STATUS_OBJECT_NAME_INVALID;
		}
	}
	
	return Status;
}
