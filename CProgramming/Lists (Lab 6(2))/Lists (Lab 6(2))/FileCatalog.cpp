#include "stdafx.h"
#include "FileCatalog.h"
#include "stdlib.h"
#include "time.h"

int rand(int LowValue, int HighValue)
{
	return rand() % (HighValue - LowValue + 1) + LowValue;
}

char *RandomWord(char Begin, char End, uint MinLength, uint MaxLength)
{
	uint Length = rand(MinLength, MaxLength);
	char *Result = new char[Length+1];
	for (int i = 0; i < Length; i++)
		Result[i] = rand(Begin, End);
	Result[Length] = '\0';
	return Result;
}

char *DataToChar(uint Data)
{
	char *Result = new char[11];

	uint T = (uint)(Data / 10000);
	uint Years = Data - T * 10000; 
	uint T1 = (uint)(T / 100);
	uint Months = T - T1 * 100; 
	uint T2 = (uint)(T1 / 100);
	uint Days = T1 - T2 * 100;

	Result[10] = '\0';

	T = Years;
	Result[9] = T % 10 + '0';
	T = (uint)(T / 10);	
	Result[8] = T % 10 + '0';
	T = (uint)(T / 10);	
	Result[7] = T % 10 + '0';
	T = (uint)(T / 10);	
	Result[6] = T + '0';

	Result[5] = '.';

	T1 = Months;
	Result[4] = T1 % 10 + '0';
	T1 = (uint)(T1 / 10);
	Result[3] = T1 + '0';

	Result[2] = '.';

	T2 = Days;
	Result[1] = T2 % 10 + '0';
	T2 = (uint)(T2 / 10);
	Result[0] = T2 + '0';

	return Result;
}

inline bool IsDigit(char Ch)
{
	return ((Ch >= '0') && (Ch <= '9'));
}

bool CharToData(char *String, uint &Data)
{
	char *Ch = String;
	int Length = 0;

	while (*Ch != '\0')
	{
		Ch++;
		Length++;
	}

	if ((Length != 10) || (!IsDigit(String[0])) || (!IsDigit(String[1])) || (String[2] != '.') ||
		(!IsDigit(String[3])) || (!IsDigit(String[4])) || (String[5] != '.') || (!IsDigit(String[6])) ||
		(!IsDigit(String[7])) || (!IsDigit(String[8])) || (!IsDigit(String[9])) )
			return false;

	uint Days =   (String[0]-'0')*10 + (String[1]-'0');
	uint Months = (String[3]-'0')*10 + (String[4]-'0');	
	uint Years =  (String[6]-'0')*1000 + (String[7]-'0')*100 + (String[8]-'0')*10 + (String[9]-'0');	
	if ((Days < 1) || (Days > 31) || (Months < 1) || (Months > 12))
		return false;

	Data = Days * 1000000 + Months * 10000 + Years;
	return true;
}

void Init(FileCatalog &FC)
{
	FC.FirstFile = NULL;
	FC.LastFile = NULL;
	FC.Count = 0;
}

void Add(FileCatalog &FC, char *Name, uint Data, uint AccsessCount)
{
	//FC.LastFile->NextFile = new FileCatalogElem;
	//FC.LastFile = FC.LastFile->NextFile;
	//FC.LastFile->Name = 
}


void Generate(FileCatalog &FC, uint Count, uint MinLength, uint MaxLength, uint MinAccess, uint MaxAccsess, uint MinData, uint MaxData)
{
	FC.FirstFile = new FileCatalogElem;
	FC.FirstFile->AccsessCount = rand(MinAccess, MaxAccsess);
	FC.FirstFile->Data = rand(MinData, MaxData);
	FC.FirstFile->Name = RandomWord('A', 'Z', MinLength, MaxLength);
	FileCatalogElem *T = FC.FirstFile;
	FC.Count = 1;

	while (FC.Count < Count)
	{
		T->NextFile = new FileCatalogElem;
		T = T->NextFile;
		T->AccsessCount = rand(MinAccess, MaxAccsess);
		T->Data = rand(MinData, MaxData);
		T->Name = RandomWord('A', 'Z', MinLength, MaxLength);
		FC.Count++;
	}

	T->NextFile = NULL;
	FC.LastFile = T;
}

void Print(FileCatalog &FC)
{
	printf("All Files:\n");
	printf("--------------------------------------------------------\n");
	printf("    %16s | %12s | %15s |\n", "Name", "Data", "Accses Count");
	printf("--------------------------------------------------------\n");

	FileCatalogElem *T = FC.FirstFile;

	while (T != NULL)
	{
		printf("    %16s | %12s | %15i |\n", T->Name, DataToChar(T->Data), T->AccsessCount);
		T = T->NextFile;
	}

	printf("--------------------------------------------------------\n");
}

void DeleteEarlies(FileCatalog &FC, uint Data)
{
	FileCatalogElem *T = FC.FirstFile->NextFile, *T1 = FC.FirstFile;
    while (T != NULL)
    {
		if (T->Data < Data)
        {
			T1->NextFile = T->NextFile;
			delete T;
			FC.Count--;
			T = T1->NextFile;
			continue;
        }
        T1 = T;
		T = T->NextFile;
    }
	if (FC.FirstFile->Data < Data)
	{
		T = FC.FirstFile->NextFile;
		delete FC.FirstFile;
		FC.Count--;
		FC.FirstFile = T;
	}
}

FileCatalogElem *MaxAccsessFile(FileCatalog &FC)
{
	FileCatalogElem *T = FC.FirstFile;
	FileCatalogElem *MaxAccsess = T;

	while (T != NULL)
	{
		if (T->AccsessCount > MaxAccsess->AccsessCount)
			MaxAccsess = T;
		T = T->NextFile;
	}

	return MaxAccsess;
}

