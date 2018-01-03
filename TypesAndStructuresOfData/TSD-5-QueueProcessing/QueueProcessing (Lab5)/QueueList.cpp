#include "stdafx.h"
#include "QueueList.h"

void InitQueue(QueueList &Q)
{
	Q.FirstElem = NULL;
	Q.LastElem = NULL;
	Q.FirstAdress = NULL;
	Q.LastAdress = NULL;
	Q.Count = 0;
	Q.GlobalCount = 0;
	Q.InCount = 0;
	Q.OutCount = 0;
	Q.StayTime = 0;
	Q.InTime = 0;
	Q.OutTime = 0;
}

void Add(QueueList &Q, double Time)
{
	if (Q.LastElem == NULL)
	{
		Q.LastElem = new QueueElem;
		Q.LastElem->NextElem = NULL;
		Q.LastElem->Number = Q.GlobalCount;
		Q.FirstElem = Q.LastElem;
	}
	else
	{
		QueueElem *T = new QueueElem;
		T->NextElem = NULL;
		T->Number = Q.GlobalCount;
		Q.LastElem->NextElem = T;
		Q.LastElem = T;
	}

	if (Q.LastAdress == NULL)
	{
		Q.LastAdress = new QueueAdressElem;
		Q.LastAdress->NextElem = NULL;
		Q.LastAdress->Adress = Q.LastElem;
		Q.FirstAdress = Q.LastAdress;
	}
	else
	{
		QueueAdressElem *T = new QueueAdressElem;
		T->NextElem = NULL;
		T->Adress = Q.LastElem;
		Q.LastAdress->NextElem = T;
		Q.LastAdress = T;
	}

	Q.Count++;
	Q.GlobalCount++;
	Q.InCount++;
	Q.InTime += Time;
}

int Get(QueueList &Q, double Time)
{
	if (Q.FirstElem == NULL)
		return -1;
	QueueElem *T = Q.FirstElem;
	int A = Q.FirstElem->Number;
	Q.FirstElem = Q.FirstElem->NextElem;
	if (Q.FirstElem == NULL)
		Q.LastElem = NULL;
	delete T;
	Q.Count--;
	Q.OutCount++;
	Q.OutTime += Time;
	return A;
}
