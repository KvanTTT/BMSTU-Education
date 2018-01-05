// Cryptography.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Cryptography.h"
#include "omp.h"


dllexport void Encode(ulong *InArray, ulong *OutArray, int Length, ulong Key)
{
	ulong Key56 = Key56Select(Key);
	GenKey48(Key56);

	//ulong *inArray = (ulong*)InArray;
	//ulong *outArray = (ulong*)OutArray;

	#pragma omp parallel for
	for (int i = 0; i < Length; i++)
		OutArray[i] = EncodeBlock(InArray[i], Key56);

	//OutArray = (char*)outArray;
}

dllexport void Decode(ulong *InArray, ulong *OutArray, int Length, ulong Key)
{
	ulong Key56 = Key56Select(Key);
	GenKey48Decode(Key56);

	#pragma omp parallel for
	for (int i = 0; i < Length; i++)
		OutArray[i] = DecodeBlock(InArray[i], Key56);
}

void GenKey48(ulong &Key56)
{
	for (int i = 0; i < 16; i++)
	{
		KeyCyclicShift(Key56, KeyRol[i]);
		Key48[i] = KeyCompress(Key56);
	}
}

void GenKey48Decode(ulong &Key56)
{
	for (int i = 0; i < 16; i++)
	{
		KeyCyclicShiftDecode(Key56, KeyRor[i]);
		Key48[i] = KeyCompress(Key56);
	}
}

void KeyCyclicShift(ulong &Key56, int Shift) 
{
	ulong Left = Key56 >> 28 & 0xFFFFFFF;
	ulong Right = Key56 & 0xFFFFFFF;

	ulong t = (Left >> (28 - Shift));
	Left = (Left << Shift | t) & 0xFFFFFFF;
	t = (Right >> (28 - Shift));
	Right = (Right << Shift | t) & 0xFFFFFFF;

	Key56 = Left << 28 | Right;
}

void KeyCyclicShiftDecode(ulong &Key56, int Shift)
{
	ulong Left = Key56 >> 28 & 0xFFFFFFF;
	ulong Right = Key56 & 0xFFFFFFF;

	ulong mask = (1 << Shift) - 1;

	ulong t = mask & Left;
	Left = (Left >> Shift) | t << (28 - Shift);
	t = mask & Right;
	Right = (Right >> Shift) | t << (28 - Shift);

	Key56 = Left << 28 | Right;
}

ulong EncodeBlock(ulong Block, ulong Key56)
{
	ulong BlockPerm = StartPerm(Block);
	ulong Left = (BlockPerm >> 32) & 0xFFFFFFFF;
	ulong Right = BlockPerm & 0xFFFFFFFF;
	ulong t;

	for (int i = 0; i < 16; i++)
	{
		t = Right;
		Right = F(Right, Key48[i]) ^ Left;
		Left = t;
	}

	return FinalPerm((Left << 32) | Right);
}

ulong DecodeBlock(ulong Block, ulong Key56)
{
	ulong BlockPerm = StartPerm(Block);
	ulong Left = (BlockPerm >> 32) & 0xFFFFFFFF;
	ulong Right = BlockPerm & 0xFFFFFFFF;
	ulong t;

	for (int i = 0; i < 16; i++)
	{
		t = Left;
		Left = F(Left, Key48[i]) ^ Right;
		Right = t;
	}

	return FinalPerm(Left << 32 | Right);
}

ulong BlockSCompress(ulong block48)
{
	ulong Result = 0;
	for (int i = 0; i < 8; i++)
	{
		ulong block6 = (block48 >> i*6) & 0x3F;
		ulong a = block6 & 0x1 | (block6 & 0x2) >> 4;
		ulong b = block6 >> 1 & 0xF;
		ulong block4 = STransform[i][a*16 + b];
		Result |= block4 << i*4;
	}
	return Result;
}

ulong F(ulong R, ulong Key)
{
	ulong block48 = BlockWide(R) ^ Key;
	return BlockPPermut(BlockSCompress(block48));
}

ulong StartPerm(ulong src)
{
	ulong Result = 0;
	for (int i = 0; i < 64; i++)
		Result |= (src & StartPermut[i]) >> StartPermutPos[i] << i;

	return Result;
}

ulong FinalPerm(ulong src)
{
	ulong Result = 0;
	for (int i = 0; i < 64; i++)
		Result |= (src & FinalPermut[i]) >> FinalPermutPos[i] << i;
	return Result;
}

ulong Key56Select(ulong Key)
{
	ulong Result = 0;
	for (int i = 0; i < 56; i++)
		Result |= (Key & KeyPermut[i]) >> KeyPermutPos[i] << i;

	return Result;
}

ulong KeyCompress(ulong Key56)
{
	ulong Result = 0;
	for (int i = 0; i < 48; i++)
		Result |= (Key56 & KeySelect[i]) >> KeySelectPos[i] << i;

	return Result;
}

ulong BlockWide(ulong block32)
{
	ulong Result = 0;
	for (int i = 0; i < 48; i++)
		Result |= (block32 & WidePermut[i]) >> WidePermutPos[i] << i;
	return Result;
}

ulong BlockPPermut(ulong block32)
{
	ulong Result = 0;
	for (int i = 0; i < 32; i++)
		Result |= (block32 & PPermut[i]) >> PPermutPos[i] << i;
	return Result;
}















