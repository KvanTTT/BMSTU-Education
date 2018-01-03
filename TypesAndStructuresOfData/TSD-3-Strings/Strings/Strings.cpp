// Strings.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "windows.h"

typedef unsigned short int byte;

const int MAX_TEXT_LENGTH = 10000;
const int MAX_WORDS_COUNT = 100;
const int MAX_WORD_LENGTH = 20;

struct char_elem
{
	char_elem *prev_elem;
	char_elem *next_elem;
	char value;
};	

void Clear(char_elem *CE);

bool IsLittleLatin(char C);

// array
char *text;

char *GetNextWord(char *pos, char nextword[]);
void Copy(char *begin_copy, char *end_copy, char result[]);
bool Equal(char word1[], char word2[]);

// list
int DivideOnWords(char_elem *text, char_elem *words[]);
bool Equal(char_elem *ce1, char_elem *ce2);
void print_char_list(char_elem *ce);

int main()
{
	long long Q1, Q2, F;
	/*QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	printf("%i", Q1);*/

	int words_count;
	int dif_words_count = 0;
	short int equal_words_count[MAX_WORDS_COUNT];
	char words[MAX_WORDS_COUNT][MAX_WORD_LENGTH];
	int i = 0;

	DWORD ProcessID = GetCurrentProcessId();
	HANDLE ProcessHandle = OpenProcess(PROCESS_SET_INFORMATION, false, ProcessID);
	HANDLE ThreadHandle = GetCurrentThread();
	SetPriorityClass(ProcessHandle, REALTIME_PRIORITY_CLASS);
	SetThreadPriority(ThreadHandle, THREAD_PRIORITY_TIME_CRITICAL);

	
	//QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	//QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	//printf("%d %i %i %i\n\n", (Q2-Q1)/(double)F, Q1, Q2, F);

	/*while (1)
	{*/
	printf("%s", "Enter method of string form (0-array, 1-list): ");
	char c = getche();
	printf("\n");
	if (c == '0')
	{
		// ввод текста и его обработка
		printf("%s\n", "Enter text(""."" at the end): ");
		char *ttext = new char[MAX_TEXT_LENGTH];
		char *d = ttext, *d1;
		c = getch();
		while (!IsLittleLatin(c))
		{
			c = getch();
		}
		while (c != '.')
		{
			if (IsLittleLatin(c) || (c == ' '))
			{
				putchar(c);
				*d = c;
				d++;
			}
			c = getch();
		}
		*d = ' ';
		d++;
		*d = '\0';
		printf("\n\n");

		// Разбиение текста на отдельные слова
		i = 0;
		char* p = ttext;
		while (*p != '\0')
		{
			p = GetNextWord(p, words[i]);
			i++;
		}
		words_count = i;
		words[i][0] = '\0';

		QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
		// Решение задачи
		for (i = 0; i < MAX_WORDS_COUNT; i++)
			equal_words_count[i] = 0;
		int j;
		for (i = 0; i < words_count; i++)
		{
			if (equal_words_count[i] == 0)
			{
				equal_words_count[i] = 1;
				for (j = i+1; j < words_count; j++)
					if (Equal(words[i], words[j]))
					{
						equal_words_count[i]++;
						equal_words_count[j] = -1;
					}
				dif_words_count++;
			}
		}
		QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
		QueryPerformanceFrequency((LARGE_INTEGER*) &F);

		// вывод
		printf("%s", "Different words(count): ");
		for (i = 0; i < words_count; i++)
			if (equal_words_count[i] > 0)
				printf("%s(%i); ", words[i], equal_words_count[i]);
		delete ttext;
	}
	else
	{
		// ввод текста (работа с текстом при помощи списка)
		printf("%s\n", "Enter text(""."" at the end): ");
		char_elem *text_list = new char_elem;
		char_elem *t = text_list;
		c = getch();
		while (!IsLittleLatin(c))
		{
			c = getch();
		}
		(*t).prev_elem = NULL;
		while (c != '.')
		{
			if (IsLittleLatin(c) || (c == ' '))
			{
				putchar(c);
				(*t).value = c;
				(*t).next_elem = new char_elem;
				(*(*t).next_elem).prev_elem = t;
				t = t->next_elem;
			}
			c = getch();
		}
		(*t).value = ' ';
		(*t).next_elem = new char_elem;
		(*(*t).next_elem).prev_elem = t;
		t = t->next_elem;
		(*t).value = '\0';
		(*t).next_elem = NULL;
		printf("\n\n");


		// Разбиение текста на отдельные слова
		char_elem *words_list[MAX_WORDS_COUNT];
		words_count = DivideOnWords(text_list, words_list);		

		// Решение задачи
		QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
		for (i = 0; i < MAX_WORDS_COUNT; i++)
			equal_words_count[i] = 0;
		int j;
		for (i = 0; i < words_count; i++)
		{
			if (equal_words_count[i] == 0)
			{
				equal_words_count[i] = 1;
				for (j = i+1; j < words_count; j++)
					if (Equal(words_list[i], words_list[j]))
					{
						equal_words_count[i]++;
						equal_words_count[j] = -1;
					}
				dif_words_count++;
			}
		}
		QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
		QueryPerformanceFrequency((LARGE_INTEGER*) &F);

		// вывод
		printf("%s", "Different words(count): ");
		for (i = 0; i < words_count; i++)
			if (equal_words_count[i] > 0)
			{
				print_char_list(words_list[i]);
				printf("(%i); ", equal_words_count[i]);
			}	

		Clear(text_list);
		for (i = 0; i < words_count; i++)
			Clear(words_list[i]);
	}
	putchar('\n');
	printf("\n%s %i", "Words count: ", words_count);
	//printf("\n%s %i", "Dif words count :", dif_words_count);
	printf("\n%s %9.7f\n", "Time(sec): ", (Q2 - Q1)/(double)F);
	printf("%s\n\n", "-----------------------------------------------------------------------------");
	dif_words_count = 0;
	words_count = 0;

	//}*/

	getch();

	return 0;
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
	while (*pos != ' ')
		pos++;
	Copy(b, pos, nextword);
	while (*pos == ' ')
		pos++;
	return pos;
}

bool Equal(char word1[], char word2[])
{
	int i = 0;
	while (word1[i] != '\0')
	{
		if (word1[i] != word2[i])
			return false;
		i++;
	}
	if (word2[i] != '\0')
		return false;
	else
		return true;
}

int DivideOnWords(char_elem *text, char_elem *words[])
{
	char_elem *t = text, *cur_letter;
	char_elem *b, *e;
	int i = 0;

	while ((*t).value != '\0')
	{
			words[i] = new char_elem;
			cur_letter = words[i];
			i++;
			(*cur_letter).prev_elem = NULL;
			while ((*t).value != ' ')
			{
				(*cur_letter).value = (*t).value;
				(*cur_letter).next_elem = new char_elem;
				(*(*cur_letter).next_elem).prev_elem = cur_letter;
				cur_letter = cur_letter->next_elem;
				t = t->next_elem;
			}
			(*cur_letter).value = '\0';
			(*cur_letter).next_elem = NULL;

			while ((*t).value == ' ')
				t = t->next_elem;
	}
	words[i] = NULL;
	return i;
}

bool Equal(char_elem *ce1, char_elem *ce2)
{
	char_elem *t1 = ce1, *t2 = ce2;
	while (((*t1).value != '\0') && ((*t2).value != '\0'))
	{
		if ((*t1).value != (*t2).value)
			return false;
		t1 = t1->next_elem;
		t2 = t2->next_elem;
	}
	if ((*t1).value != (*t2).value)
		return false;
	else
		return true;
}

void print_char_list(char_elem *ce)
{
	char_elem* t = ce;
	while ((*t).value != '\0')
	{
		putchar((*t).value);
		t = t->next_elem;
	}
}

bool IsLittleLatin(char C)
{
	if ((C >= 'a') && (C <= 'z'))
		return true;
	else
		return false;
}

void Clear(char_elem *CE)
{
	char_elem *T;
	while (CE != NULL)
	{
		T = CE->next_elem;
		delete CE;
		CE = T;
	}
}