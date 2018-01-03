#include "stdafx.h"
#include "StringList.h"
#include <iostream>

using namespace std;

void LowerCase(char &C) // преобразование одного символа в нижний регистр
{
	if ((C >= 'A') && (C <= 'Z'))
		C = C - 'A' + 'a';
	else
	if ((C >= 'ј') && (C <= 'я'))
		C = C - 'ј' + 'а';
	else
	if (C == '®')
		C = 'Є';
}

void UpperCase(char &C) // преобразование одного символа в верхний регистр
{
	if ((C >= 'a') && (C <= 'z'))
		C = C - 'a' + 'A';
	else
	if ((C >= 'а') && (C <= '¤'))
		C = C - 'а' + 'ј';
	else
	if (C == 'Є')
		C = '®';
}

void Init(StringList &SL) // инициализация строки
{
	SL.FirstElem = NULL;
	SL.LastElem = NULL;
	SL.Count = 0;
}

void Init(StringList &SL, char String[])  // инициализаци¤ строки
{
	CharElem *T;
	char *T2 = String;

		if (*T2 != '\0')
		{
		SL.FirstElem = new CharElem;
		SL.FirstElem->Value = *T2;
		SL.FirstElem->PrevElem = NULL;
		SL.LastElem = SL.FirstElem;
		T2++;
		SL.Count = 1;

		while (*T2 != '\0')
		{
			T = new CharElem;
			T->Value = *T2;
			T->PrevElem = SL.LastElem;
			SL.LastElem->NextElem = T;
			SL.LastElem = SL.LastElem->NextElem;

			SL.Count++;
			T2++;
		}
		SL.LastElem->NextElem = NULL;
		}
}

void Copy(StringList &ToSL, StringList &FromSL)
{
	Clear(ToSL);

	ToSL.Count = FromSL.Count;
	CharElem *T = FromSL.FirstElem;
	CharElem *T1 = ToSL.FirstElem = new CharElem;
	CharElem *P;

	T1->PrevElem = NULL;
	while (T != NULL)
	{
		T1->Value = T->Value;
        T = T->NextElem;

		P = new CharElem;
		P->PrevElem = T1;
		T1->NextElem = P;
		T1 = T1->NextElem;
	}
    T1->NextElem = NULL;
    ToSL.LastElem = T1;

}


void Clear(StringList &SL) // очистка строки
{
	CharElem *T;
	try 
	{
		SL.Count = 0;
		if (SL.FirstElem == NULL)
			throw 1;
		while (SL.FirstElem != NULL)
		{
			T = SL.FirstElem->NextElem;
			delete SL.FirstElem;
			SL.FirstElem = T;
		}

	}
	catch (int e)
	{
		if (e == 1)
			cout << "String is already empty";
	}
}

void Print(StringList &SL) // печать строки
{
    CharElem *T = SL.FirstElem;
    while (T != NULL)
    {
        putchar(T->Value);
        T = T->NextElem;
    }
}

void AddBegin(StringList &SL, char C)
{
	if (SL.FirstElem != NULL)
	{
		CharElem *T = new CharElem;
		T->Value = C;
		T->NextElem = SL.FirstElem;
		SL.FirstElem->PrevElem = T;
		T->PrevElem = NULL;
		SL.FirstElem = T;
	}
	else
	{
		SL.FirstElem = new CharElem;
		SL.FirstElem->Value = C;
		SL.FirstElem->NextElem = NULL;
		SL.FirstElem->PrevElem = NULL;
		SL.LastElem = SL.FirstElem;
	}
	SL.Count++;
}

void AddEnd(StringList &SL, char C)
{
	if (SL.LastElem != NULL)
	{
		CharElem *T = new CharElem;
		T->Value = C;
		T->NextElem = NULL;
		T->PrevElem = SL.LastElem;
		SL.LastElem->NextElem = T;
		SL.LastElem = T;
	}
	else
	{
		SL.LastElem = new CharElem;
		SL.LastElem->Value = C;
		SL.LastElem->NextElem = NULL;
		SL.LastElem->PrevElem = NULL;
		SL.FirstElem = SL.LastElem;
	}
	SL.Count++;
}

void DeleteFirst(StringList &SL)
{
	if (SL.FirstElem != NULL)
	{
		CharElem *T = SL.FirstElem->NextElem;
		delete SL.FirstElem;
		SL.FirstElem = T;
		SL.FirstElem->PrevElem = NULL;
		SL.Count--;
	}
}

void DeleteLast(StringList &SL)
{
	if (SL.LastElem != NULL)
	{
		CharElem *T = SL.LastElem->PrevElem;
		delete SL.LastElem;
		SL.LastElem = T;
		SL.LastElem->NextElem = NULL;
		SL.Count--;
	}
}

int Equal(StringList &SL1, StringList &SL2)  // сравнение двух строк -1 < ; 0 = ; 1 >
{
	if (SL1.Count < SL2.Count)
		return -1;
	if (SL1.Count > SL2.Count)
		return 1;
	CharElem *T1 = SL1.FirstElem;
	CharElem *T2 = SL2.FirstElem;
	while (T1 != NULL)
	{
		if (T1->Value < T2->Value)
			return -1;
		if (T1->Value > T2->Value)
			return 1;
		T1 = T1->NextElem;
		T2 = T2->NextElem;
	}
	return 0;
}

int SubStr(StringList &SL, StringList &SubSL) // поиск подстроки и возврат индекса
{
	CharElem *T = SL.FirstElem, *T2;
	CharElem *T1;
	int Ind = 0;

	while (T != NULL)
	{
		T1 = SubSL.FirstElem;
		T2 = T;
		while ((T1 != NULL) && (T2 != NULL))
		{
			if (T1->Value != T2->Value)
				break;
			T1 = T1->NextElem;
			T2 = T2->NextElem;
		}
		if (T1 == NULL)
		{
			return Ind;
		}
		Ind++;
		T = T->NextElem;
	}

	return -1;
}

int DelSubStr(StringList &SL, StringList &SubSL) // удаление подстроки, если же она есть
{
	CharElem *T = SL.FirstElem, *T2;
	CharElem *T1;
	CharElem *TT2 = NULL;
	CharElem *P;
	int Ind = 0;
	int C, C1;

		while (T != NULL)
		{
			T1 = SubSL.FirstElem;
			T2 = T;
			C = 0;
			while ((T1 != NULL) && (T2 != NULL))
			{
				if (T1->Value != T2->Value)
					break;
				T1 = T1->NextElem;
				T2 = T2->NextElem;
				C++;
			}
			if (T1 == NULL)
			{
				C1 = C;
				T2 = T;
				while (C)
				{
					P = T2->NextElem;
					delete T2;
					T2 = P;
					C--;
				}		
				if (TT2 != NULL)
				{				
					if (T2 != NULL)
					{
						T2->PrevElem = TT2;
						TT2->NextElem = T2;
					}
					else
					{
						TT2->NextElem = NULL;
						SL.LastElem = TT2;
					}
				}
				else
				{
					if (T2 != NULL)
					{
						SL.FirstElem = T2;
						T2->PrevElem = NULL;
					}
					else
					{
						SL.FirstElem = NULL;
						SL.LastElem = NULL;
					}
				}
				SL.Count -= C1;
				return Ind;
			}				
			Ind++;
			TT2 = T;
			T = T->NextElem;
		}
	return -1;
}

void Reverse(StringList &SL)
{
	CharElem *T;
	CharElem *T1;
	CharElem *P = SL.FirstElem;
	while (P != NULL)
	{
		T = P;
		P = P->NextElem;

		T1 = T->NextElem;
		T->NextElem = T->PrevElem;
		T->PrevElem = T1;
	}  

	T = SL.FirstElem;
	SL.FirstElem = SL.LastElem;
	SL.LastElem = T;
}

void LowerCase(StringList &SL)   // преобразование всей строки в нижний регистр
{
    CharElem *T = SL.FirstElem;
    while (T != NULL)
    {
        LowerCase(T->Value);
        T = T->NextElem;
    }
}

void UpperCase(StringList &SL)  // преобразование всей строки в верхний регистр
{
    CharElem *T = SL.FirstElem;
    while (T != NULL)
    {
        UpperCase(T->Value);
        T = T->NextElem;
    }
}

void ConcatRight(StringList &ToSL, StringList &FromSL) // присоединение справа
{
	ToSL.Count = ToSL.Count + FromSL.Count;
	CharElem *T = FromSL.FirstElem;
	CharElem *T1 = ToSL.LastElem;

	while (T != NULL)
	{
        T1->NextElem = new CharElem;
        T1->NextElem->PrevElem = T1;
        T1 = T1->NextElem;
		T1->Value = T->Value;
        T = T->NextElem;
	}
    T1->NextElem = NULL;
    ToSL.LastElem = T1;
}

void ConcatLeft(StringList &ToSL, StringList &FromSL) // присоеднинение справа
{
	ToSL.Count = ToSL.Count + FromSL.Count;
	CharElem *T = FromSL.LastElem;
	CharElem *T1 = ToSL.FirstElem;

	while (T != NULL)
	{
        T1->PrevElem = new CharElem;
        T1->PrevElem->NextElem = T1;
        T1 = T1->PrevElem;
		T1->Value = T->Value;
        T = T->PrevElem;
	}
    T1->PrevElem = NULL;
    ToSL.FirstElem = T1;
}