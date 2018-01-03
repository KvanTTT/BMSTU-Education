// Sparse Matrix Processing (TSD Lab6).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "SparseMatrix.h"
#include <iostream>
#include "windows.h"
#include "time.h"

using namespace std;

void main()
{
	Matrix M1, M2, M;
	SparseMatrix SM1, SM2, SM;
	uint SizeX, SizeY;
	float MinValue, MaxValue;
	float Density;
	long long QM1, QM2, QSM1, QSM2, QF;
	bool P;
	
	srand(time(NULL));

	DWORD ProcessID = GetCurrentProcessId();
	HANDLE ProcessHandle = OpenProcess(PROCESS_SET_INFORMATION, false, ProcessID);
	HANDLE ThreadHandle = GetCurrentThread();

	while (1)
	{
		cout << "Enter SizeX, SizeY, MinValue, MaxValue, Density(0..1), Printable\n";
		cin >> SizeX >> SizeY >> MinValue >> MaxValue >> Density >> P;
		/*SizeX = 5;
		SizeY = 10;
		MinValue = -5;
		MaxValue = 5;
		Density = 0.2;
		P = 1;*/

		SetPriorityClass(ProcessHandle, REALTIME_PRIORITY_CLASS);
		SetThreadPriority(ThreadHandle, THREAD_PRIORITY_TIME_CRITICAL);

		M1 = Init(SizeX, SizeY, MinValue, MaxValue, Density);
		M2 = Init(SizeX, SizeY, MinValue, MaxValue, Density);

		SM1 = Init(M1);
		SM2 = Init(M2);

		QueryPerformanceCounter((LARGE_INTEGER*) &QM1);
		M = Sum(M1, M2);
		QueryPerformanceCounter((LARGE_INTEGER*) &QM2);
		/*cout << "\n\nMatrix 1:\n";
		Print(M1);
		cout << "\n\n";
		PrintStruct(SM1);
		cout << "\n\nMatrix 2:\n";
		Print(M2);
		cout << "\n\n";
		PrintStruct(SM2);*/

		QueryPerformanceCounter((LARGE_INTEGER*) &QSM1);
		SM = Sum(SM1, SM2);
		QueryPerformanceCounter((LARGE_INTEGER*) &QSM2);

		QueryPerformanceFrequency((LARGE_INTEGER*) &QF);

		if (P)
		{
			cout << "\n\nMatrix 1:\n";
			Print(SM1);

			cout << "\n\nMatrix 2:\n";
			Print(SM2);

			cout << "\n\nSum:\n";
			Print(SM);

			/*cout << "\n\n";
			Print(M);*/
		}

		cout << "\n\nTime of sum (in seconds):";
		printf("\n  Matrix:         %9.7f", (QM2 - QM1)/(double)QF);
		printf("\n  Sparse Matrix:  %9.7f", (QSM2 - QSM1)/(double)QF);
		printf("\n  Coeff:          %9.3f", (QSM2 - QSM1)/(double)(QM2 - QM1));

		cout <<"\n\nSize of structs (in bytes):";
		printf("\n  Matrix:         %9i", SizeOf(M));
		printf("\n  Sparse Matrix:  %9i", SizeOf(SM));
		printf("\n  Coeff:          %9.3f", SizeOf(SM)/(double)SizeOf(M));
		printf("\n----------------------------------------------------------------------\n\n\n");

		Clear(M1);
		Clear(M2);
		Clear(M);
		Clear(SM1);
		Clear(SM2);
		Clear(SM);

		SetPriorityClass(ProcessHandle, NORMAL_PRIORITY_CLASS);
		SetThreadPriority(ThreadHandle, THREAD_PRIORITY_NORMAL);

		getch();
	}
}

