// QueueProcessing (Lab5).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "QueueList.h"
#include "QueueVector.h"
#include "stdlib.h"
#include "time.h"
#include "math.h"
#include "windows.h"

const double CALL_COUNT = 1000;
const double EPS = 0.001;

inline double rand(double Low, double High);
void PrintState(QueueList &Q1, QueueList &Q2);
void PrintState(QueueRing &Q1, QueueRing &Q2);

void PrintAdress(QueueList &Q, int Count);

void main()
{
	double Time = 0;
	double Eps = 0.001;
	double T1, T2, T3, T4;
	double TT1 = 0.0, TT2 = 0.0, TT3 = 0.0, TT4 = 0.0;
	double IL1 = 1, IH1 = 5, OL1 = 0, OH1 = 4;
	double IL2 = 0, IH2 = 3, OL2 = 0, OH2 = 1;
	int C;
	long long Q1, Q2, F;
	double TotalTime, TimeModeling;

	srand(time(NULL));

	/*int M;
	printf("Enter number of method (0 - list, 1 - array): ");
	scanf("%i", &M);*/

	DWORD ProcessID = GetCurrentProcessId();
	HANDLE ProcessHandle = OpenProcess(PROCESS_SET_INFORMATION, false, ProcessID);
	HANDLE ThreadHandle = GetCurrentThread();
	SetPriorityClass(ProcessHandle, REALTIME_PRIORITY_CLASS);
	SetThreadPriority(ThreadHandle, THREAD_PRIORITY_TIME_CRITICAL);


	QueueList QL1;
	QueueList QL2;
	InitQueue(QL1);
	InitQueue(QL2);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	while (QL1.OutCount != 1000)
	{
		if (!TT1) 
			T1 = rand(IL1, IH1);
		if (!TT2) 
			T2 = rand(IL2, IH2);
		if (!TT3) 
			T3 = rand(OL1, OH1);
		if (!TT4) 
			T4 = rand(OL2, OH2);

		if (fabs(T1 - TT1) < Eps) 
		{
			Add(QL1, T1);
			TT1 = 0.0;
			if (QL1.Count == 1)
			{
				T3 = rand(OL1, OH1);
				TT3 = 0;
			}
		}
		else
			TT1 += Eps;

		if (fabs(T2 - TT2) < Eps)
		{
			Add(QL2, T2);
			TT2 = 0.0;
			if (QL2.Count == 1)
			{
				T4 = rand(OL2, OH2);
				TT4 = 0;
			}
		}
		else
			TT2 += Eps;


		if (QL1.Count)
		{
			if (fabs(T3 - TT3) < Eps)
			{
				C = Get(QL1, T3) + 1;
				if (!(C % 100) && C)
					PrintState(QL1, QL2);
				TT3 = 0.0;
			}
			else
				TT3 += Eps;
		}
		else
			QL1.StayTime += Eps;

		if (!QL1.Count)
		{
			if (QL2.Count)
			{
				if (fabs(T4 - TT4) < Eps)
				{
					Get(QL2, T4);
					TT4 = 0.0;
				}
				else
					TT4 += Eps;
			}
			else
				QL2.StayTime += Eps;
		}

		Time += Eps;
	}
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);

	printf("\n---------------------------------------------------------\n");
	printf("\nGlobal count of in/out calls of queue1: %i %i", QL1.InCount, QL1.OutCount);
	printf("\nGlobal count of in/out calls of queue2: %i %i", QL2.InCount, QL2.OutCount);
	//printf("\nPractitial out time: %6.4f %6.4f", QL1.OutTime/(double)QL1.OutCount, 
	//	(double)QL2.OutTime/QL2.OutCount);
	printf("\nTotal time of staying: %6.4f", QL2.StayTime);
	TotalTime = QL1.OutCount*(OH1 + OL1)/2 + QL2.OutCount*(OH2 + OL2)/2 + QL2.StayTime ;
	TimeModeling = QL1.OutTime + QL2.OutTime + QL2.StayTime;
	printf("\nTotal time of modeling(theoretical): %6.4f", TotalTime);
	printf("\nTime of modeling by out(practicial): %6.4f", (TimeModeling));
	printf("\nCoefs  %4.2f%%", fabs((TotalTime - TimeModeling)/TimeModeling*100));
	printf("\nReal time  %6.4f%", (Q2 - Q1)/(double)F);

	printf("\n\nEnter count of output adresses: ");
	scanf("%i", &C);
	PrintAdress(QL1, C);

	getch();
	printf("\n\n//////////------------------------------------\nRinged Queue:\n");


	// Vector
	Time = 0;
	TT1 = 0.0;
	TT2 = 0.0;
	TT3 = 0.0;
	TT4 = 0.0;

	QueueRing QV1;
	QueueRing QV2;
	InitQueue(QV1);
	InitQueue(QV2);

	QueryPerformanceCounter((LARGE_INTEGER*) &Q1);
	while (QV1.OutCount != 1000)
	{
		if (!TT1) 
			T1 = rand(IL1, IH1);
		if (!TT2) 
			T2 = rand(IL2, IH2);
		if (!TT3) 
			T3 = rand(OL1, OH1);
		if (!TT4) 
			T4 = rand(OL2, OH2);

		if (fabs(T1 - TT1) < Eps) 
		{
			Add(QV1, T1);
			TT1 = 0.0;
			if (QV1.Count == 1)
			{
				T3 = rand(OL1, OH1);
				TT3 = 0;
			}
		}
		else
			TT1 += Eps;

		if (fabs(T2 - TT2) < Eps)
		{
			Add(QV2, T2);
			TT2 = 0.0;
			if (QV2.Count == 1)
			{
				T4 = rand(OL2, OH2);
				TT4 = 0;
			}
		}
		else
			TT2 += Eps;


		if (QV1.Count)
		{
			if (fabs(T3 - TT3) < Eps)
			{
				C = Get(QV1, T3) + 1;
				if (!(C % 100) && C)
					PrintState(QV1, QV2);
				TT3 = 0.0;
			}
			else
				TT3 += Eps;
		}
		else
			QV1.StayTime += Eps;

		if (!QV1.Count)
		{
			if (QV2.Count)
			{
				if (fabs(T4 - TT4) < Eps)
				{
					Get(QV2, T4);
					TT4 = 0.0;
				}
				else
					TT4 += Eps;
			}
			else
				QV2.StayTime += Eps;
		}

		Time += Eps;
	}
	QueryPerformanceCounter((LARGE_INTEGER*) &Q2);
	QueryPerformanceFrequency((LARGE_INTEGER*) &F);

	printf("\n---------------------------------------------------------\n");
	printf("\nGlobal count of in/out calls of queue1: %i %i", QV1.InCount, QV1.OutCount);
	printf("\nGlobal count of in/out calls of queue2: %i %i", QV2.InCount, QV2.OutCount);
	//printf("\nPractitial out time: %6.4f %6.4f", QV1.OutTime/(double)QV1.OutCount, 
	//	(double)QV2.OutTime/QV2.OutCount);
	printf("\nTotal time of staying: %6.4f", QV2.StayTime);
	TotalTime = QV1.OutCount*(OH1 + OL1)/2 + QV2.OutCount*(OH2 + OL2)/2 + QV2.StayTime ;
	TimeModeling = QV1.OutTime + QV2.OutTime + QV2.StayTime;
	printf("\nTotal time of modeling(theoretical): %6.4f", TotalTime);
	printf("\nTime of modeling by out(practicial): %6.4f", (TimeModeling));
	printf("\nCoefs  %4.2f%%", fabs((TotalTime - TimeModeling)/TimeModeling*100));
	printf("\nReal time  %6.4f%", (Q2 - Q1)/(double)F);


	getch();
}

double rand(double Low, double High)
{
	return (((double)rand() / RAND_MAX) * (High - Low) + Low);
}

void PrintState(QueueList &Q1, QueueList &Q2)
{
	static int GlobalCount1 = 0;
	static int GlobalCount2 = 0;
	static int N1 = 0;
	static int N2 = 0;
	GlobalCount1 += Q1.Count;
	GlobalCount2 += Q2.Count;
	N1++;
	N2++;
	printf("\n\nLength of Queue1 and Queue1: %i %i", Q1.Count, Q2.Count);
	printf("\nAverage length of Queue1 and Queue2: %6.4f %6.4f", (double)(GlobalCount1/N1),
		(double)(GlobalCount2/N2));
	printf("\nIncoming call count of Queue1 and Queue2: %i %i", Q1.InCount, Q2.InCount);
	printf("\nOutcoming call count of Queue1 and Queue2: %i %i", Q1.OutCount, Q2.OutCount);
	printf("\nAverage stay time of queue1: %6.4f %6.4f", (double)((Q1.StayTime)/(Q1.OutCount)),
		(double)((Q2.StayTime)/(Q2.OutCount)));
}

void PrintState(QueueRing &Q1, QueueRing &Q2)
{
	static int GlobalCount1 = 0;
	static int GlobalCount2 = 0;
	static int N1 = 0;
	static int N2 = 0;
	GlobalCount1 += Q1.Count;
	GlobalCount2 += Q2.Count;
	N1++;
	N2++;
	printf("\n\nLength of Queue1 and Queue1: %i %i", Q1.Count, Q2.Count);
	printf("\nAverage length of Queue1 and Queue2: %6.4f %6.4f", (double)(GlobalCount1/N1),
		(double)(GlobalCount2/N2));
	printf("\nIncoming call count of Queue1 and Queue2: %i %i", Q1.InCount, Q2.InCount);
	printf("\nOutcoming call count of Queue1 and Queue2: %i %i", Q1.OutCount, Q2.OutCount);
	printf("\nAverage stay time of queue1: %6.4f %6.4f", (double)((Q1.StayTime)/(Q1.OutCount)),
		(double)((Q2.StayTime)/(Q2.OutCount)));
}

void PrintAdress(QueueList &Q, int Count)
{
	int C = 0;
	QueueAdressElem *T = Q.FirstAdress;
	while ((C != Count) && (T != NULL))
	{
		printf("%i ", T);
		T = T->NextElem;
		C++;
	}

}