struct QueueElem
{
	QueueElem *NextElem;
	int Number;
};

struct QueueAdressElem
{
	QueueAdressElem *NextElem;
	QueueElem *Adress;
};


struct QueueList
{
	QueueElem *FirstElem;
	QueueElem *LastElem;
	int Count;
	int GlobalCount;
	int InCount;
	int OutCount;
	float StayTime;
	float InTime, OutTime; 
	QueueAdressElem *FirstAdress;
	QueueAdressElem *LastAdress;
};

void InitQueue(QueueList &Q);
void Add(QueueList &Q, double Time);
int Get(QueueList &Q, double Time);


