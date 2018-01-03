// Stack.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"

typedef unsigned int uint;

struct CharElem
{
	CharElem *NextElem;
	char Value;
};

struct CharStack
{
	CharElem *CurElem;
	uint Size;
};

void Rus(char &C);
void LittleLetter(char &C);

void Init(CharStack &CS);
void Clear(CharStack &CS);
void AddElem(CharStack &CS, char Value);
char GetElem(CharStack &CS);
void PrintStack(CharStack &CS);

bool IsLetter(char C);
bool IsPunctSigns(char C);

int main()
{
	CharStack CS;
	Init(CS);

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
			AddElem(CS, c);
		}
		c = getch();
	}



	getch();	
	return 0;
}

void Init(CharStack &CS)
{
	CS.CurElem = NULL;
	CS.Size = 0;
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
	CS.Size++;
}

char GetElem(CharStack &CS)
{
	if (CS.CurElem != NULL)
	{
		char Result = (*CS.CurElem).Value;
		CharElem *T = (*CS.CurElem).NextElem;
		delete CS.CurElem;
		CS.CurElem = T;
		CS.Size--;
		return Result;
	}
	else
		return '\0';
}

void PrintStack(CharStack &CS)
{
	CharElem *T = CS.CurElem;
	while (CS.CurElem != NULL)
	{
		printf("%c", CS.CurElem);
		T = T->NextElem;
	}	
}

void Rus(char &C)
{
	char Result = C;
	if ((C >= 'À') || (C <= 'ï'))
		C -= 64;
	else
	if ((C >= 'ð') || (C <= 'ÿ'))
		C -= 16;
}

void LittleLetter(char &C)
{
	/*if (((C >= 'A') && (C <= 'z')) || ((C >= 'À') && (C <= 'ÿ')))
		return true;
	else
		return false;*/
}


bool IsLetter(char C)
{
	if (((C >= 'A') && (C <= 'z')) || ((C >= 'À') && (C <= 'ÿ')))
		return true;
	else
		return false;
}

bool IsPunctSigns(char C)
{
	if ((C == '.') || (C == ',') || (C == '!') || (C == '?') || (C == '"'))
		return true;
	else
		return false;
}
