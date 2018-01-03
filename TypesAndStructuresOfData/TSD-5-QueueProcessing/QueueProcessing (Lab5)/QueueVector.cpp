#include "stdafx.h"
#include "QueueVector.h"

const int MAX_SIZE = 2000;

void InitQueue(QueueRing &Q)
{
	Q.FirstInd = 0;
	Q.LastInd = MAX_SIZE-1;
	Q.Count = 0;
	Q.GlobalCount = 0;
	Q.InCount = 0;
	Q.OutCount = 0;
	Q.StayTime = 0;
	Q.InTime = 0;
	Q.OutTime = 0;
}

void Add(QueueRing &Q, double Time)
{
	Q.LastInd++;
	if (Q.LastInd == MAX_SIZE)
		Q.LastInd = 0;
	Q.Arr[Q.LastInd] = Q.GlobalCount;
	Q.Count++;
	Q.GlobalCount++;
	Q.InCount++;
	Q.InTime += Time;
}


int Get(QueueRing &Q, double Time)
{
	if ((Q.LastInd < Q.FirstInd) || ((Q.FirstInd == 0) && (Q.LastInd == MAX_SIZE-1)))
		return -1;
	int Result = Q.Arr[Q.FirstInd];
	Q.FirstInd++;
	if (Q.FirstInd == MAX_SIZE)
		Q.FirstInd = 0;
	Q.Count--;
	Q.OutCount++;
	Q.OutTime += Time;
	return Result;
}
