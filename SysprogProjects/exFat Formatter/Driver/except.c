/*
 * COPYRIGHT:		 See COPYRIGHT.TXT
 * PROJECT:          Ext2 File System Driver for WinNT/2K/XP
 * FILE:             except.c
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
#pragma alloc_text(PAGE, Ext2ExceptionFilter)
#pragma alloc_text(PAGE, Ext2ExceptionHandler)
#endif


NTSTATUS
Ext2ExceptionFilter (IN PEXT2_IRP_CONTEXT    IrpContext,
		     IN NTSTATUS             ExceptionCode)
{
	NTSTATUS Status;
	
	//
	// Only use a valid IrpContext
	//
	if (IrpContext)
	{
		if ((IrpContext->Identifier.Type != ICX) ||
			(IrpContext->Identifier.Size != sizeof(EXT2_IRP_CONTEXT)))
		{
			IrpContext = NULL;
		}
	}

    //
    //  For the purposes of processing this exception, let's mark this
    //  request as being able to wait, and neither write through nor on
    //  removable media if we aren't posting it.
    //

    SetFlag(IrpContext->Flags, IRP_CONTEXT_FLAG_WAIT);

	//
	// If the exception is expected execute our handler
	//
	if (FsRtlIsNtstatusExpected(ExceptionCode))
	{
		KdPrint(("Ext2ExceptionFilter: Catching exception %#x\n",
			ExceptionCode));
		
		Status = EXCEPTION_EXECUTE_HANDLER;
		
		if (IrpContext)
		{
			IrpContext->ExceptionInProgress = TRUE;
			IrpContext->ExceptionCode = ExceptionCode;
		}
	}
	//
	// else continue search for an higher level exception handler
	//
	else
	{
		KdPrint(("Ext2ExceptionFilter: Passing on exception %#x\n",
			ExceptionCode));
		
		Status = EXCEPTION_CONTINUE_SEARCH;
		
		if (IrpContext)
		{
			Ext2FreeIrpContext(IrpContext);
		}
	}
	
	return Status;
}


NTSTATUS
Ext2ExceptionHandler (IN PEXT2_IRP_CONTEXT IrpContext)
{
	NTSTATUS Status;
	
	if (IrpContext)
	{
		Status = IrpContext->ExceptionCode;
		
		if (IrpContext->Irp)
		{
			IrpContext->Irp->IoStatus.Status = Status;
			
			Ext2CompleteRequest(IrpContext->Irp, IO_NO_INCREMENT);
		}
		
		Ext2FreeIrpContext(IrpContext);
	}
	else
	{
		Status = STATUS_INSUFFICIENT_RESOURCES;
	}
	
	return Status;
}

