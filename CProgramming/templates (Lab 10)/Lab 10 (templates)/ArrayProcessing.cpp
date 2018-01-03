#include "stdafx.h"
#include "ArrayProcessing.h"
#include <iostream>
#include "conio.h"

using namespace std;

const int UNFIND_POSITIV_ELEMS = 1;
const int UNFIND_SECOND_POSITIV_ELEM = 2;
	

template <typename T>
void EnterArray(T *ar, int Size)
{
	cout << "Enter array: ";
	for (int i = 0; i < Size; i++)
		cin >> ar[i]; 
}

template <typename T>
void PrintAll(T *ar, T MAE, T Sum, int Size)
{
	cout << "Max Abs Element: " << MAE << '\n';
	cout << "Sum between positive elements: " << Sum << '\n';
	cout << "Converted array: ";
	for (int i = 0; i < Size; i++)
		cout << ar[i] << ' ';
}


template<typename T>
T MaxAbsElement(T *ar, int Size)
{
	T result = ar[0];
	for (int i = 1; i < Size; i++)
		if (ar[i] < -result)
			result = -ar[i];
		else
		if (ar[i] > result)
			result = ar[i];

	return result;
}

template<typename T>
T Sum(T *ar, int Size)
{
	T result = 0;
	int i;
	int ind1 = -1;
	int ind2 = -1;
	for (i = 0; i < Size; i++)
		if (ar[i] > 0)
		{
			if (ind1 == -1)
			{
				ind1 = i;
				continue;
			}
			if (ind2 == -1)
			{
				ind2 = i;
				break;
			}
		}
	try
	{
		if (ind1 == -1)
			throw UNFIND_POSITIV_ELEMS;
		if (ind2 == -1)
			throw UNFIND_SECOND_POSITIV_ELEM;
		for (i = ind1+1; i < ind2; i++)
			result += ar[i];
	}
	catch (int e)
	{
		switch (e)
		{
			case (UNFIND_POSITIV_ELEMS):
				cout << "Unfind positiv elements\n";
				result = 0;
			break;
			case (UNFIND_SECOND_POSITIV_ELEM):
				cout << "Unfind second positiv element\n";
				for (i = ind1+1; i < Size; i++)
					result += ar[i];
			break;
		}
	}


	return result;

}

template<typename T>
void Convert(T *ar, int Size)
{
	int ind = Size-1;
	for (int i = 0; i < Size-1; i++)
		if (ar[i] == 0)
		{
			for (int j = i; j < Size-1; j++)
				ar[j] = ar[j+1];
			ar[Size-1] = 0;
			ind--;
		}

}


template<typename T>
void InputProcessOutput(T *ar, int Size)
{
	EnterArray(ar, Size);
	T MAE = MaxAbsElement(ar, Size);
	T S = Sum(ar, Size);
	Convert(ar, Size);	
	PrintAll(ar, MAE, S, Size);
}
