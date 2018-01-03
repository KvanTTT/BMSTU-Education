// Sorting(Lab5).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "stdlib.h"
#include "time.h"
#include "windows.h"
#include "Sorting.h"

void AllSortsAndOutput();

void main()
{
	AllSortsAndOutput();
	getch();	
}

void AllSortsAndOutput()
{
	int Size;
	int Bottom, Top;
	int i;
	
	printf("%s", "Enter size of array: ");
	scanf("%i", &Size);
	printf("%s", "Enter bottom and top border of array: ");
	scanf("%i %i", &Bottom, &Top);

	srand(time(NULL));
	int *Arr = InitArr(Size, Bottom, Top);
	int *Arr1 = Copy(Arr, Size);
	QuickSort(Arr1, Size);
	int *Arr2 = Copy(Arr1, Size);
	Reverse(Arr2, Size);


	AllSortsRandom(Arr, Size);
	AllSortsSorted(Arr1, Size);
	AllSortsReverseSorted(Arr2, Size);
}









