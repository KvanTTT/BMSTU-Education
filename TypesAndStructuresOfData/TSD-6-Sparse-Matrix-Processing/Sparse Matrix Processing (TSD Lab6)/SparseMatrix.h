typedef unsigned int uint;

struct IntElem
{
	uint Value;
	IntElem *NextElem;
};

struct IntList
{
	IntElem *FirstElem;
	IntElem *LastElem;
	uint Count;
};

struct FloatElem
{
	float Value;
	FloatElem *NextElem;
};

struct FloatList
{
	FloatElem *FirstElem;
	FloatElem *LastElem;
	uint Count;
};

struct Matrix
{
	float **A;
	uint SizeX;
	uint SizeY;
	uint Count;
};

struct SparseMatrix
{
	float *A;
	uint *JA;
	uint *IA;
	uint SizeX;
	uint SizeY;
	uint Count;
};

struct SparseListMatrix
{
	FloatList A;
	IntList IA;
	(IntElem*) *JA;
	(FloatElem*) *JAValue;
	uint Size;
	uint Count;
	uint AssignCount;
};


//list

inline uint rand(uint LowValue, uint HighValue);
inline uint rand_without0(uint LowValue, uint HighValue);

//matrix by array

Matrix Init(uint SizeX, uint SizeY, float MinValue, float MaxValue, float NumberDensity);
void Clear(Matrix &M);
Matrix Sum(Matrix &M1, Matrix &M2);
void Print(Matrix &M);
uint SizeOf(Matrix &M);

//sparse matrix by array

SparseMatrix Init(Matrix &M);
void Clear(SparseMatrix &M);
SparseMatrix Sum(SparseMatrix &M1, SparseMatrix &M2);
void Print(SparseMatrix &M);
void PrintStruct(SparseMatrix &M);
uint SizeOf(SparseMatrix &M);


void Insert2(uint Elem1, float Elem2, uint Ind, uint *Arr1, float *Arr2, uint AssignCount);

void Init(SparseMatrix &M, uint Size, float MinValue, float MaxValue, float NumberDensity);
void Set(SparseMatrix &M, float Value, uint Row, uint Column);
float Get(SparseMatrix &M, uint Row, uint Column);

// sparse matrix by list

void Init(SparseListMatrix &M, uint Size, float MinValue, float MaxValue, float NumbersDensity);
inline void Set(SparseListMatrix &M, uint Value, uint Row, uint Column);
inline uint Get(SparseListMatrix &M, uint Row, uint Column);

