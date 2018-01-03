extern const int MAX_SIZE;

struct QueueRing
{
	int Arr[2000];
	int FirstInd;
	int LastInd;
	int Count;
	int GlobalCount;
	int InCount;
	int OutCount;
	float StayTime;
	float InTime, OutTime; 
};

void InitQueue(QueueRing &Q);
void Add(QueueRing &Q, double Time);
int Get(QueueRing &Q, double Time);