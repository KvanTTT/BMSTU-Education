// Lab5.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <cstdlib>
#include <conio.h>
#include <string>
#include <iomanip>
#include <ctime>
#include <Windows.h>

using namespace std;

const int size = 10000;

void Input_Reverse(int arr[size]);
void Input_Random(int arr[size]);
void QuickSort(int First, int Last, int arr[size]);
void ShellSort(int arr[size]);
void MergeSort(int First, int Last, int arr[size]);
void LinSort(int arr[size]);
void ShakerSort(int arr[size]);
void Change(int i, int j, int arr[size]);
void Merge(int First, int Last, int m, int arr[size]);
string ru(const string arg);

int _tmain(int argc, _TCHAR* argv[]){
	enum PsevdoGraph{Vert = 179, Horiz = 196, LL = 218, L = 193, R = 191, Cross = 192, CrossEnd = 195, 
                     RCross = 217, RCrossEnd = 180, LCross = 194, LC = 193, LCrossEnd = 197};
	int arr[size], I;
	double Time;
	LARGE_INTEGER Start, Finish, Freq;
     
	cout << (char) LL << setfill((char) Horiz) << setw(11) << right << (char) LCross;
	cout << setw(24) << (char) LCross << setw(16) << (char) LCross << setw(16) << (char) R << endl;
	cout << (char) Vert << setfill(' ');
	cout << setw(11) << (char) Vert << ru("Обратно отсортированный") << (char) Vert << ru("Отсортированный") << (char) Vert;
	cout << ru("Случайные числа") << (char) Vert << endl;
	cout << (char) CrossEnd << setfill((char) Horiz) << setw(11) << (char) LCrossEnd << setw(24) << (char) LCrossEnd;
	cout << setw(16) << (char) LCrossEnd << setw(16) << (char) RCrossEnd << endl;
	cout << (char) Vert << setw(10) << setfill(' ') << left << ru("Быстрая") << (char) Vert << right;
	
	Input_Reverse(arr);
	QueryPerformanceFrequency(&Freq);
	QueryPerformanceCounter(&Start);
   	QuickSort(0, size - 1, arr);
	QueryPerformanceCounter(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
	cout << setw(21) << setprecision(1) << fixed << Time << "ms" << (char) Vert;

	QueryPerformanceCounter(&Start);
	QuickSort(0, size - 1, arr);
	QueryPerformanceCounter(&Finish);
    Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert; 
	Input_Random(arr);
	QueryPerformanceCounter(&Start);
	QuickSort(0, size - 1, arr);
	QueryPerformanceCounter(&Finish);
    Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
	cout << setw(13) << Time << "ms" << (char) Vert << endl;

	cout << (char) CrossEnd << setfill((char) Horiz) << setw(11) << (char) LCrossEnd << setw(24) << (char) LCrossEnd;
	cout << setw(16) << (char) LCrossEnd << setw(16) << (char) RCrossEnd << endl;
	cout << (char) Vert << setw(10) << setfill(' ') << left << ru("Шелл") << (char) Vert << right;

	Input_Reverse(arr);
	QueryPerformanceCounter(&Start);
   	ShellSort(arr);
	QueryPerformanceCounter(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
	cout << setw(21) << Time << "ms" << (char) Vert;

	QueryPerformanceCounter(&Start);
	ShellSort(arr);
	QueryPerformanceFrequency(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert; 
	Input_Random(arr);
	QueryPerformanceCounter(&Start);
	ShellSort(arr);
	QueryPerformanceCounter(&Finish);
    Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert << endl; 

	cout << (char) CrossEnd << setfill((char) Horiz) << setw(11) << (char) LCrossEnd << setw(24) << (char) LCrossEnd;
	cout << setw(16) << (char) LCrossEnd << setw(16) << (char) RCrossEnd << endl;
	cout << (char) Vert << setw(10) << setfill(' ') << left << ru("Слияния") << (char) Vert << right;

	Input_Reverse(arr);
	QueryPerformanceCounter(&Start);
   	MergeSort(0, 9999, arr);
	QueryPerformanceCounter(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
	cout << setw(21) << Time << "ms" << (char) Vert;

	QueryPerformanceCounter(&Start);
	MergeSort(0, 9999, arr);
	QueryPerformanceFrequency(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert; 
	Input_Random(arr);
	QueryPerformanceCounter(&Start);
	MergeSort(0, 9999, arr);
	QueryPerformanceCounter(&Finish);
    Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert << endl; 

	cout << (char) CrossEnd << setfill((char) Horiz) << setw(11) << (char) LCrossEnd << setw(24) << (char) LCrossEnd;
	cout << setw(16) << (char) LCrossEnd << setw(16) << (char) RCrossEnd << endl;
	cout << (char) Vert << setw(10) << setfill(' ') << left << ru("Пр.выбором") << (char) Vert << right;

	Input_Reverse(arr);
	QueryPerformanceCounter(&Start);
   	LinSort(arr);
	QueryPerformanceCounter(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
	cout << setw(21) << Time << "ms" << (char) Vert;

	QueryPerformanceCounter(&Start);
	LinSort(arr);
	QueryPerformanceFrequency(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert; 
	Input_Random(arr);
	QueryPerformanceCounter(&Start);
	LinSort(arr);
	QueryPerformanceCounter(&Finish);
    Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert << endl; 

	cout << (char) CrossEnd << setfill((char) Horiz) << setw(11) << (char) LCrossEnd << setw(24) << (char) LCrossEnd;
	cout << setw(16) << (char) LCrossEnd << setw(16) << (char) RCrossEnd << endl;
	cout << (char) Vert << setw(10) << setfill(' ') << left << ru("Шейкер") << (char) Vert << right;

	Input_Reverse(arr);
	QueryPerformanceCounter(&Start);
   	ShakerSort(arr);
	QueryPerformanceCounter(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
	cout << setw(21) << Time << "ms" << (char) Vert;

	QueryPerformanceCounter(&Start);
	ShakerSort(arr);
	QueryPerformanceFrequency(&Finish);
	Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert; 
	Input_Random(arr);
	QueryPerformanceCounter(&Start);
	ShakerSort(arr);
	QueryPerformanceCounter(&Finish);
    Time = ((Finish.QuadPart - Start.QuadPart) / Freq.QuadPart) * 1000;
    cout << setw(13) << Time << "ms" << (char) Vert << endl; 

	cout << (char) Cross << setfill((char) Horiz) << setw(11) << right << (char) L;
	cout << setw(24) << (char) L << setw(16) << (char) L << setw(16) << (char) RCross << endl;

    getch();
	return 0;
}

void Input_Reverse(int arr[size]){
	register int I;

	for (I = 0; I <= (size - 1); I++)
		arr[size - I - 1] = I;
}

void Input_Random(int arr[size]){
	register int I;

	srand(time(0));
	for (I = 0; I <= (size - 1); I++)
		arr[I] = rand();
}

void QuickSort(int First, int Last, int arr[size]){
	int I, J, Mid;

    I = First;
	J = Last;
	Mid = arr[(int)((I + J) / 2)];

	do{
		while (arr[I] < Mid)
			I++;
		while (arr[J] > Mid)
			J--;
		if (I <= J){
			Change(I, J, arr);
			I++;
			J--;
		}
	} while (I <= J);
    
	if (First < J)
		QuickSort(First, J, arr);
	if (I < Last)
		QuickSort(I, Last, arr);
}

void ShellSort(int arr[size]){  
  int step = size >> 1, j;
  register int i;

  while (step > 0){
	  for (int i = 0; i < (size - step);i++){
		  j = i;
          while (j >= 0){
			  if (arr[j] > arr[j + step])
				  Change(j, j + step, arr);
			  j--;
          }
      }
      step >>= 1;
  }
}

void MergeSort(int First, int Last, int arr[size]){
	int m;

	if (First >= Last)
		return;
	m = (First + Last) / 2;
	MergeSort(First, m, arr);
	MergeSort(m + 1, Last, arr);
	Merge(First, m, Last, arr);
}

void LinSort(int arr[size]){
	register int i, j;

	for (i = 0; i < (size - 1); i++)
		for (j = i + 1; j < size; j++)
			if (arr[i] > arr[j])
				Change(i, j, arr);
}

void ShakerSort(int arr[size]){
	register int i, j;
    int First = 0, Last = size;

    while (Last > First){
        for (i = First; i < (Last - 1); i++)
            if (arr[i] > arr[i+1])
                Change(i, i + 1, arr);
        --Last;

        for (i = (Last - 1); i > First; i--)
            if (arr[i] < arr[i-1])
                Change(i, i - 1, arr);
        First++;
    }
}

void Change(int i, int j, int arr[10000]){
	int q;

	q = arr[i];
	arr[i] = arr[j];
	arr[j] = q;
}

static void Merge(int First, int Last, int m, int arr[size]){
//	int temp(arr[First], arr[Last + 1]);
	register int i, j, k;

	if ((First > Last) || (m < First) || (m > Last))
		return;
	if ((Last = (First + 1)) && (arr[First] > arr[Last])){
		Change(First, Last, arr);
		return;
	}
 
 /*   for (int i = First, j = 0, k = m - First + 1; i <= Last; i++){
        if (j > (m - Last))
			arr[i] = temp[First + k++];
        else if (k > (Last - First))
			arr[i] = &temp[First + j++];
        else 
			arr[i] = (temp[First + j] < temp[First + k]) ? temp[First + j++] : temp[First + k++];
	}	*/
}

string ru(const string arg)
{
   	string s = arg;
	unsigned int i, l;
	l = arg.size();
	for (i = 0; i <= l; i++)
	{
		switch (s[i])
		{
		    case 'ё': s[i] = 240; break;
		    case 'Ё': s[i] = 241; break;
		    case '№': s[i] = 252; break;
		}
		
		if ((s[i] >= 'А') & (s[i] <= 'п'))   s[i] = s[i] - 64;
		if ((s[i] >= 'р') & (s[i] <= 'я'))   s[i] = s[i] - 16;
	}
	return s;
}