// WorkWithData.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <conio.h>
#include <string>
#include <iostream>
#include <fstream>

using namespace std;


const int MAX_LENGTH = 140;
const char LT = 218;
const char H = 196;
const char RT = 191;
const char V = 179; 
const char RB = 217;
const char LB = 192;
const char T = 194;
const char B = 193;
const char R = 180;
const char L = 195;
const char C = 197;


int MaxWordLength(char *str, int arr[]);
void Copy(char *begin_copy, char *end_copy, char result[]);
char* GetNextWord(char *pos, char nextword[]);
bool IsDigit(char Word[]);
int Length(char Word[]);
void Output(ifstream *f, int begin, int max_length[], int column_count, int string_count);
void FindVarAndMaxColumnLength(ifstream *f, int &begin, int max_length[], int &column_count, int &string_count); 


int main()
{
	char str[MAX_LENGTH];
	char str1[MAX_LENGTH];
	ifstream fs("data.txt");
	if (!fs)
	{
		printf("ERROR OF OPENING");
		return 1;
	}
	int max_length[10] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
	int string_count = 0;
	int begin;
	int column_count;

	FindVarAndMaxColumnLength(&fs, begin, max_length, column_count, string_count); 
	Output(&fs, begin, max_length, column_count, string_count);

	getch();
	fs.close();
	return 0;
}

int MaxWordLength(char *str, int arr[])
{
	char *t;
	char *b;
	int i = 0;

	t = str;
	while ((*t != '\0') && (*t != 10))
	{
		b = t;
		while ((*t != ' ') & (*t != '\t') && (*t != '\0') && (*t != 10))
				t++;

		if (t - b > arr[i])
			arr[i] = t - b;
		i++;
		while (((*t == ' ') || (*t == '\t')) && (*t != '\0') && (*t != 10))
				t++;
	}	
	return i;
}

void Copy(char *begin_copy, char *end_copy, char result[])
{
	char *t = begin_copy;
	unsigned int i = 0;
	while (t != end_copy)
	{
		result[i] = *t;
		i++;
		t++;
	}
	result[i] = '\0';
}

char* GetNextWord(char *pos, char nextword[])
{
	char *t;
	char *b;

	b = pos;
	while ((*pos != ' ') & (*pos != '\t') & (*pos != '\0') & (*pos != '\n'))
		pos++;
	Copy(b, pos, nextword);
	while (((*pos == ' ') || (*pos == '\t')) & (*pos != '\0') & (*pos != '\n'))
		pos++;
	return pos;
}

bool IsDigit(char Word[])
{
	int i = 0;
	while (Word[i] != '\0')
	{
		if ((Word[i] == '1') || (Word[i] == '2') || (Word[i] == '3') || (Word[i] == '4') 
			|| (Word[i] == '5') || (Word[i] == '6') || (Word[i] == '7') || (Word[i] == '8') 
			|| (Word[i] == '9') || (Word[i] == '0'))
		{
			i++;
			continue;
		}
		else
			return false;
		
	}
	return true;
}

int Length(char Word[])
{
	int i = 0;
	while (Word[i] != '\0')
		i++;
	return i;
}

void Output(ifstream *fs, int begin, int max_length[], int column_count, int string_count)
{
	int i, j;
	char str1[MAX_LENGTH];
	char *Ind;

	//fs->seekg(begin);
	fs->close();
	fs->clear();
	fs->open("data.txt");

	while (!fs->eof())
	{
		fs->getline(str1, MAX_LENGTH);
		Ind = strstr(str1, "Var. #09");
		if (Ind != NULL)
			break;
	}
	fs->getline(str1, MAX_LENGTH);

	cout << "\n";
	char *p; 
	char Word[30];
	int k = 1;
	int length = 0;
	for (i = 0; i < column_count; i++)
		length += max_length[i] + 3;
	cout << LT;
	for (i = 0; i < column_count; i++)
	{
		for (j = 0; j < max_length[i] + 2; j++)
			cout << H; 
		cout << T;
	}
	cout << '\n';
	//fs->getline(str1, MAX_LENGTH);
	while (k != string_count-1)
	{	
		if (k == 1)
		{
			k++;
			fs->getline(str1, MAX_LENGTH);
			p = str1;
			for (i = 0; i < column_count; i++)
			{
				p = GetNextWord(p, Word);
				int L = Length(Word);
				cout << V;
				if ((L % 2) != 0)
				{
					for (j = 0; j <= ((max_length[i] - L) >> 1); j++)
						cout << ' ';
					cout.width(L + ((max_length[i] - L) >> 1) + 1);
					cout << left << Word;
				}
				else
				{
					for (j = 0; j <= ((max_length[i] - L) >> 1) + 1; j++)
						cout << ' ';
					if ((i == 4) || (i == 5))
						cout.width(L + ((max_length[i] - L) >> 1));
					else
						cout.width(L + ((max_length[i] - L) >> 1) + 1);
					cout << left << Word;
				}
				
			}
			cout << V << '\n';
			cout << L;
			for (int i = 0; i < column_count; i++)
			{		
				for (j = 0; j < max_length[i] + 2; j++)
					cout << H; 
				cout << C;
			}

			cout << '\n';
			continue;
		}
		if (k == 2)
		{
			k++;
			fs->getline(str1, MAX_LENGTH);
			continue;
		}
		fs->getline(str1, MAX_LENGTH);
		p = str1;
		cout << V;
		for (int i = 0; i < column_count; i++)
		{
			p = GetNextWord(p, Word);
			cout.width(max_length[i]);
			if (IsDigit(Word))	
				cout << right << Word;
			else
				cout << left << Word;
			if (i == 0)
				cout << ' ';
			cout << ' ' << V << ' ';
		}
		cout << '\n' << L;
		for (int i = 0; i < column_count; i++)
		{		
			for (j = 0; j < max_length[i] + 2; j++)
				cout << H; 
			cout << C;
		}
		cout << '\n';

		k++;
	}
}

void FindVarAndMaxColumnLength(ifstream *fs, int &begin, int max_length[], int &column_count, int &string_count)
{
	char *Ind, *Ind1;
	char str[MAX_LENGTH], str1[MAX_LENGTH];

	while (!fs->eof())
	{
		fs->getline(str, MAX_LENGTH);
		Ind = strstr(str, "Var. #09");
		if (Ind != NULL)
		{
			string_count++;
			cout << str << '\n';
			fs->getline(str1, MAX_LENGTH);
			begin = fs->tellg();
			while (!fs->eof())
			{
				fs->getline(str1, MAX_LENGTH);
				Ind1 = strstr(str1, "Var. #10");
				if (Ind1 != NULL)
					break;
				if (string_count == 1)
					column_count = MaxWordLength(str1, max_length);
				if (string_count != 2)
					MaxWordLength(str1, max_length);
				string_count++;
				cout << str1 << '\n';
			}
		}
	}
}











