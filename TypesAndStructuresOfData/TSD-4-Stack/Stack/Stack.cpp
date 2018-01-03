// Stack.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "windows.h"

typedef unsigned int uint;

struct StringStack
{
	char *Data;
	uint Size;
	uint RealSize;
	uint FirstInd;
	uint LastInd;
};

struct CharElem
{
	CharElem *NextElem;
	char Value;
};

struct CharElemElem
{
	CharElemElem *NextElem;
	CharElem *Value;
};

struct CharStack
{
	CharElem *CurElem;
	CharElemElem *FreeElem;
	CharElemElem *AddElem;
	uint Size;
};

void Rus(char &C);
void LittleCase(char &C);

void Init(CharStack &CS);
void Clear(CharStack &CS);
void AddElem(CharStack &CS, char Value);
char GetElem(CharStack &CS);
void PrintStack(CharStack &CS);
void Clear(CharStack &CS);

void Init(StringStack &SS, uint Size);
void Clear(StringStack &SS);
void AddElem(StringStack &SS, char Value);
char GetElem(StringStack &SS);
void PrintStack(StringStack &SS);
void Reset(StringStack &SS);

bool IsLetter(char C);
bool IsPunctSigns(char C);

bool IsItPalindrom(CharStack &CS);
bool IsItPalindrom(StringStack &CS);

int main()
{
	StringStack SS;
	CharStack CS;
	Init(SS, 80);
	while (1)
	{
		long long Q1, Q2, F;
		Init(CS);

		printf("Enter string: \n");

		char c = getch();
		while (!IsLetter(c))
		{
			c = getch();
		}
		while (c != '.')
		{
			if (IsPunctSigns(c) || (c == ' '))
				putchar(c);
			if (IsLetter(c))
			{
				putchar(c);
				LittleCase(c);
				AddElem(CS, c);
				printf("\n%d\n\n", CS.AddElem->Value);
				AddElem(SS, c);
			}
			if (c == '\b')
			{
				putchar('<');
				putchar(GetElem(CS));
				if (CS.FreeElem != NULL)
					printf("\n%d\n\n", CS.FreeElem->Value);
				GetElem(SS);
			}
			c = getch();
		}

		printf("\n\n Stack: ");
		PrintStack(CS);

		QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
		bool Pal = IsItPalindrom(CS);
		QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
		QueryPerformanceFrequency((LARGE_INTEGER*) &F);
		printf("\n\n List method: %9.7f sec", (Q2-Q1)/(double)F);

		QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
		Pal = IsItPalindrom(SS);
		QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
		QueryPerformanceFrequency((LARGE_INTEGER*) &F);
		printf("\n Array method: %9.7f sec", (Q2-Q1)/(double)F);
		
		if (Pal)
			printf("\n\n It is palindrom");
		else
			printf("\n\n It is not palindrom");

		Clear(CS);
		Reset(SS);

		printf("\n----------------------------------------------------------\n\n");	
		
	}
	return 0;
}

void Init(CharStack &CS)
{
	CS.CurElem = NULL;
	CS.Size = 0;
	CS.FreeElem = NULL;	
	CS.AddElem = NULL;
}

void Clear(CharStack &CS)
{
	CharElem *T;
	while (CS.CurElem != NULL)
	{
		T = CS.CurElem;
		CS.CurElem = (*CS.CurElem).NextElem;
		delete T;
	}
	CS.Size = 0;	
	CS.FreeElem = NULL;
	CS.AddElem = NULL;
}

void AddElem(CharStack &CS, char Value)
{
	if (CS.CurElem == NULL)
	{
		CS.CurElem = new CharElem;
		(*CS.CurElem).Value = Value;
		(*CS.CurElem).NextElem = NULL;
	}
	else
	{
		CharElem *T = new CharElem;
		(*T).Value = Value;
		(*T).NextElem = CS.CurElem;
		CS.CurElem = T;
	}

	if (CS.AddElem == NULL)
	{
		CS.AddElem = new CharElemElem;
		(*CS.AddElem).Value = CS.CurElem;
		(*CS.AddElem).NextElem = NULL;
	}
	else
	{
		CharElemElem *T = new CharElemElem;
		(*T).Value = CS.CurElem;
		(*T).NextElem = CS.AddElem;
		CS.AddElem = T;
	}

	CS.Size++;
}

char GetElem(CharStack &CS)
{
	if (CS.CurElem != NULL)
	{
		if (CS.FreeElem == NULL)
		{
			CS.FreeElem = new CharElemElem;
			(*CS.FreeElem).Value = CS.CurElem;
			(*CS.FreeElem).NextElem = NULL;
		}
		else
		{
			CharElemElem *T = new CharElemElem;
			(*T).Value = CS.CurElem;
			(*T).NextElem = CS.FreeElem;
			CS.FreeElem = T;
		}

		char Result = (*CS.CurElem).Value;
		CharElem *T = (*CS.CurElem).NextElem;
		delete CS.CurElem;
		CS.CurElem = T;
		CS.Size--;
		return Result;
	}
	else
	{
		CS.FreeElem = NULL;
		//CS.FreeElem->NextElem = NULL;
		return '\0';
	}
}

void PrintStack(CharStack &CS)
{
	CharElem *T = CS.CurElem;
	printf("\n  ");
	while (T != NULL)
	{
		printf("%c", T->Value);
		T = T->NextElem;
	}	
	/*T = CS.CurElem;
	printf("\n Adreses\n  ");
	while (T != NULL)
	{
		printf("%d ", &T->Value);
		T = T->NextElem;
	}*/
}

void Init(StringStack &SS, uint Size)
{
	SS.Data = new char[Size];
	SS.Size = Size;
	SS.FirstInd = Size-1;
	SS.LastInd = Size-1;
	SS.RealSize = 0;
}

void Clear(StringStack &SS)
{
}

void AddElem(StringStack &SS, char Value)
{
	if (!SS.FirstInd)
		SS.FirstInd = SS.Size - 1;
	else
		SS.FirstInd--;
	SS.Data[SS.FirstInd] = Value;
	SS.RealSize++;
}

char GetElem(StringStack &SS)
{
	char Result = SS.Data[SS.FirstInd];
	if (SS.FirstInd == SS.Size - 1)
		SS.FirstInd = 0;
	else
		SS.FirstInd++;
	SS.RealSize--;
	return Result;
}

void PrintStack(StringStack &SS)
{
	for (int I = SS.FirstInd; I < SS.LastInd; I++)
		printf("%c", SS.Data[I]);
}

void Reset(StringStack &SS)
{
	SS.FirstInd = SS.Size-1;
	SS.LastInd = SS.Size-1;
	SS.RealSize = 0;
}

void Rus(char &C)
{
	if ((C >= 'А') || (C <= 'п'))
		C -= 64;
	else
	if ((C >= 'р') || (C <= 'я'))
		C -= 16;
}

void LittleCase(char &C)
{
	if ((C >= 'A') && (C <= 'Z'))
		C = C - 'A' + 'a';
	else
	if ((C >= 'А') && (C <= 'Я'))
		C = C - 'А' + 'а';
	else
	if (C == 'Ё')
		C = 'ё';
}

bool IsLetter(char C)
{
	if (((C >= 'A') && (C <= 'Z')) || ((C >= 'a') && (C <= 'z')) || ((C >= 'А') && (C <= 'я')))
		return true;
	else
		return false;
}

bool IsPunctSigns(char C)
{
	if ((C == '.') || (C == ',') || (C == '!') || (C == '?') || (C == '"') || (C == ':') || (C == ';'))
		return true;
	else
		return false;
}

bool IsItPalindrom(CharStack &CS)
{
	CharStack CS1;
	Init(CS1);
	int I;
	bool b = true;

	uint S2 = (uint)(CS.Size/2);
	for (I = 0; I < S2; I++)
		AddElem(CS1, GetElem(CS));
	if (CS.Size > CS1.Size)
		GetElem(CS);

	for (I = 0; I < S2; I++)
		if (GetElem(CS) != GetElem(CS1))
			{
				b = false;
				break;
			}

	/*CharElemElem *T1;
	CharElemElem *T = CS.AddElem;
	printf("\n Add\n  ");
	while (T != NULL)
	{
		printf("%d ", T->Value);
		T = T->NextElem;
	}

	bool b1;
	T = CS.FreeElem;
	printf("\n Free\n  ");
	while (T != NULL)
	{
		b1 = true;
		T1 = CS.AddElem;
		while (T1 != NULL)
		{
			if (T1->Value == T->Value)
			{
				b1 = false;
				break;
			}					
			T1 = T1->NextElem;
		}
		//if (b1 == true)
			printf("%d ", T->Value);
		T = T->NextElem;
	}*/
	
	if (b)
		return true;
	else
		return false;
}

bool IsItPalindrom(StringStack &SS)
{
	StringStack SS1;
	Init(SS1, SS.RealSize);
	int I;

	uint S2 = (uint)(SS.RealSize/2);
	for (I = 0; I < S2; I++)
		AddElem(SS1, GetElem(SS));
	if (SS.RealSize > SS1.RealSize)
		GetElem(SS);
	for (I = 0; I < S2; I++)
		if (GetElem(SS) != GetElem(SS1))
			return false;

	return true;
}
