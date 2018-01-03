// Sorting(Lab5).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "stdlib.h"
#include "time.h"
#include "windows.h"


typedef unsigned int uint;

const uint SIZE_UINT = sizeof(uint);

inline int *InitArr(uint Size, int Bottom, int Top);
void PrintArr(int *Arr, uint Size, bool Numbers = false);
int *Copy(int *Arr, int Size);
void Reverse(int *Arr, int Size);

//--SORTS-----------------------------------------------
inline void QuickSort(int *Arr, int Size);
void QSort(int *Arr, int BottomInd, int TopInd);

void QSort2(int* Arr, int a, int b);
int partition(int* Arr, int a, int b); 
inline void QuickSort2(int* Arr, int Size); 

inline void MergeSort(int *Arr, int Size);
inline int *Merge(int *m1, int *m2, int l1, int l2);

int increment(int* inc, int size);
inline void ShellSort(int *Arr, int Size);

inline void DigitalSort(int *Arr, int Size, int MaxElem);

inline void InsertSort(int  *Arr, int Size);

void main()
{
	uint Size;
	int Bottom, Top;
	uint i;
	
	//Beep(25000, 10000);

	printf("%s", "Enter size of array: ");
	scanf("%i", &Size);
	printf("%s", "Enter bottom and top border of array: ");
	scanf("%i %i", &Bottom, &Top);

	int *Arr;
	srand((unsigned)time(NULL));
	srand(time(NULL));
	Arr = InitArr(Size, Bottom, Top);
	int *ArrFastSort = Copy(Arr, Size);
	int *ArrMergeSort = Copy(Arr, Size);
	int *ArrShellSort = Copy(Arr, Size);
	int *ArrDigitalSort = Copy(Arr, Size);
	int *ArrInsertSort = Copy(Arr, Size);

	//printf("\n%s", "Init array: ");
	//PrintArr(Arr, Size);

	long long Q1, Q2, F;
	DWORD ProcessID = GetCurrentProcessId();
	HANDLE ProcessHandle = OpenProcess(PROCESS_SET_INFORMATION, false, ProcessID);
	HANDLE ThreadHandle = GetCurrentThread();
	SetPriorityClass(ProcessHandle, REALTIME_PRIORITY_CLASS);
	SetThreadPriority(ThreadHandle, THREAD_PRIORITY_TIME_CRITICAL);

	////////////////////////////
	printf("\n\nRandom array:");

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	QuickSort(ArrFastSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	/*printf("\n\n%s", "Sorted array: ");
	PrintArr(ArrFastSort, Size);*/
	printf("\nTime of quick sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	MergeSort(ArrMergeSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of merge sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	ShellSort(ArrShellSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of shell sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	DigitalSort(ArrDigitalSort, Size, Top);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of digital sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	InsertSort(ArrInsertSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of insert sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	///////////////////////////
	printf("\n\nSorted array:");

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	QuickSort(ArrFastSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of quick sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	MergeSort(ArrMergeSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of merge sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	ShellSort(ArrShellSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of shell sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	DigitalSort(ArrDigitalSort, Size, Top);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of digital sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	InsertSort(ArrInsertSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of insert sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	///////////////////////////
	printf("\n\nRevers sorted array:");

	Reverse(ArrFastSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	QuickSort(ArrFastSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of quick sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	Reverse(ArrMergeSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	MergeSort(ArrMergeSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of merge sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	Reverse(ArrShellSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	ShellSort(ArrShellSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of shell sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	Reverse(ArrDigitalSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	DigitalSort(ArrDigitalSort, Size, Top);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of digital sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	Reverse(ArrInsertSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	InsertSort(ArrInsertSort, Size);
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);
	printf("\nTime of insert sorting(sec): %9.7f", (Q2 - Q1)/(double)F);

	SetThreadPriority(ThreadHandle, THREAD_PRIORITY_NORMAL);
	SetPriorityClass(ProcessHandle, NORMAL_PRIORITY_CLASS);


	getch();	
}

int *InitArr(uint Size, int Bottom, int Top)
{
	int *Result, *T;
	int Interval = Top - Bottom + 1;

	Result = new int[Size];
	T = Result;
	for (uint i = 0; i < Size; i++)
	{
		*T = rand() % Interval + Bottom;
		T++;
	}

	return Result;
}

void PrintArr(int *Arr, uint Size, bool Numbers)
{
	int *T = Arr;

	if (Numbers == false)
	{
		for (int i = 0; i < Size; i++)
		{
			printf("%i ", *T);
			T++;
		}
	}
	else
	{
		for (int i = 0; i < Size; i++)
		{
			printf("[%i] - %i; ", &i, *T);
			T++;
		}
	}
}

//-------------------------------------------------

void QSort(int *Arr, int Left, int Right)
{
	if (Left >= Right) return;                       // Условие выхода из рекурсии (достигнута база рекурсии)
 
    int Left1, Right1, M;
    int T;
    Left1 = Left; 
	Right1 = Right;                         // Запомнить начальное значение границ
    // Вычисление опорного элемента:
    M = (Right+Left)/2;                            // 1) середина отрезка;
    //M = rand() % (Right-Left) + Left;               // 2) случайный выбор опорного элемента;
    //M = Right;                                // 3) правый конец - приводит к КОЛОСАЛЬНОМУ времени обработки
                                            //      отсортированых(!) массивов.
    while (Left < Right) 
	{                            // Итеративное разделение массива
        while ((Arr[Left] <= Arr[M]) && (Left < M)) 
			Left++;    // Сдвиг левой границы
        while ((Arr[Right] >= Arr[M]) && (Right > M)) 
			Right--;    // Сдвиг правой границы
 
        T = Arr[Left];                         // Обмен граничных элементов
        Arr[Left] = Arr[Right];
        Arr[Right] = T;
        if (Left == M) 
			M = Right;                  // Обновление индекса опорного элемента в случае
		else 
		if (Right == M) 
			M = Left;             //      если опорный элемент участвовал в обмене
    }

    QSort(Arr, Left1, M-1);                       // Рекурсивная сортировка полученных массивов
    QSort(Arr, M+1, Right1);
}

void QuickSort(int *Arr, int Size)
{	
	QSort(Arr, 0, Size-1);
}

int *Merge(int *m1, int *m2, int l1, int l2)
{
    int *ret = new int[l1+l2];
    int n = 0;
    // Сливаем массивы, пока один не закончится
    while (l1 && l2){
        if (*m1 < *m2)
		{
           *(ret + n) = *m1;
			m1++; 
			l1--;
		}
        else 
		{
           *(ret + n) = *m2;
           m2++; 
		   l2--;
		}
        n++;
	}
    // Если закончился первый массив
    if (l1 == 0){
        for (int i = 0; i < l2; i++){
            *(ret + n++) = *m2++;
		}
	}
    // Если закончился второй массив
    else 
	{
		for (int i = 0; i < l1; i++)
           *(ret + n++) = *m1++;

	}
    return ret;
}

void MergeSort(int *Arr, int Size)
{
    int n = 1, l, ost;
    int *Arr1;
    while (n < Size)
	{
        l=0;
        while (l < Size){
           if (l+n >= Size) 
			   break;
           ost = ((l + n << 1 ) > Size) ? (Size - (l + n)) : n;
           Arr1 = Merge(Arr + l, Arr + l + n, n, ost);
           for (int i = 0; i < n + ost; i++) 
			   *(Arr + l + i) = *(Arr1 + i);
		   delete Arr1;
           l += n << 1;
		}
        n = n << 1;
	}
}



int *Copy(int *Arr, int Size)
{
	int *Result = new int[Size];
	int *T = Arr, *T1 = Result;
	for (int i = 0; i < Size; i++)
	{
		*T1 = *T;
		T++;
		T1++;
	}
	return Result;
}

void Reverse(int *Arr, int Size)
{
	int T;
	for (int i = 0; i < (Size << 1); i++)
	{	
		T = Arr[i];
		Arr[i] = Arr[Size-1-i];
		Arr[Size-1-i] = T;
	}
}

int partition(int* Arr, int a, int b) 
{
	  int i = a;
	  int T;
      for (int j = a; j <= b; j++)    // просматриваем с a по b
      {
         if (Arr[j] <= Arr[b])            // если элемент m[j] не превосходит m[b],
          {
			T = Arr[i];
			Arr[i] = Arr[j];
			Arr[j] = T;
                                       // меняем местами m[j] и m[a], m[a+1], m[a+2] и так далее...
                                      // то есть переносим элементы меньшие m[b] в начало,
                                      // а затем и сам m[b] «сверху»*/
            i++;                      // таким образом последний обмен: m[b] и m[i], после чего i++
          }
       }
      return i-1;                     // в индексе i хранится <новая позиция элемента m[b]> + 1
}
 
void QSort2(int* Arr, int a, int b) // a - начало подмножества, b - конец
{                                        // для первого вызова: a = 0, b = <элементов в массиве> - 1
     if (a >= b) return;
     int c = partition(Arr, a, b);
     QSort2(Arr, a, c-1);
     QSort2(Arr, c+1, b);
}

void QuickSort2(int* Arr, int Size) 
{                                        
     QSort2(Arr, 0, Size-1);
}

int increment(int* inc, int size) {
// inc[] массив, в которые заносятся инкременты
// size размерность этого массива
  int p1, p2, p3, s;
 
  p1 = p2 = p3 = 1;
  s = -1;
  do {// заполняем массив элементов по формуле Роберта Седжвика
    if (++s % 2) {
      inc[s] = 8*p1 - 6*p2 + 1;
    } else {
      inc[s] = 9*p1 - 9*p3 + 1;
      p2 *= 2;
      p3 *= 2;
    }
	p1 *= 2;
// заполняем массив, пока текущая инкремента хотя бы в 3 раза меньше количества элементов в массиве
  } while(3*inc[s] < size);  
 
  return s > 0 ? --s : 0;// возвращаем количество элементов в массиве
}

void DigitalSort(int *Arr, int Size, int MaxElem)
{
	int *N = new int[MaxElem+1];
	int i, j;
	for (i = 0; i <= MaxElem; i++)
		N[i] = 0;
	for (i = 0; i < Size; i++)
		N[Arr[i]]++;
	int K = 0;
	for (i = 0; i <= MaxElem; i++)
	{
		if (N[i] == 1)
		{
			Arr[K] = i;
			K++;
		}
		else
		for (j = 0; j < N[i]; j++)
		{
			Arr[K] = i;
			K++;
		}
	}
}
 
void ShellSort(int *Arr, int Size)
{
// inc инкремент, расстояние между элементами сравнения
// i и j стандартные переменные цикла
// seq[40] массив, в котором хранятся инкременты
  int inc, i, j, seq[40];
  int s;//количество элементов в массиве seq[40]
 
  // вычисление последовательности приращений
  s = increment(seq, Size);
  while (s >= 0) 
  {
	//извлекаем из массива очередную инкременту
	inc = seq[s--];
// сортировка вставками с инкрементами inc
    for (i = inc; i < Size; i++) 
	{
      int temp = Arr[i];
// сдвигаем элементы до тех пор, пока не дойдем до конца или не упорядочим в нужном порядке
      for (j = i-inc; (j >= 0) && (Arr[j] > temp); j -= inc)
        Arr[j+inc] = Arr[j];
// после всех сдвигов ставим на место j+inc элемент, который находился на i месте
      Arr[j+inc] = temp;
    }
  }
}

 
 void InsertSort(int  *Arr, int Size) 
      {
      int i, j;
      int  value;
      for (i = 1; i < Size; i++)
          {
          value = Arr[i];
          for (j = i-1; (j >= 0) && (Arr[j] > value); j--) {
              Arr[j+1] = Arr[j];
          }
          Arr[j+1] = value;
      }
 }







