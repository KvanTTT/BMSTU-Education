#include "stdafx.h"
#include "SparseMatrix.h"
#include "stdlib.h"
#include "time.h"

uint rand_without0(uint LowValue, uint HighValue)
{ 
	uint r;
	do
	{
		r = rand() % (HighValue - LowValue + 1) + LowValue;
	}
	while (!r);
	return r;
}

float rand_without0(float LowValue, float HighValue)
{ 
	float r;
	do
	{
		r = ((float)rand() / 32767 * (HighValue - LowValue) + LowValue);
	}
	while (!r);
	return r;
}

uint rand(uint LowValue, uint HighValue)
{ 
	return rand() % (HighValue - LowValue + 1) + LowValue;
}

/*void insert(uint *Ar, uint Ind, uint Value)
{
*/

Matrix Init(uint SizeX, uint SizeY, float MinValue, float MaxValue, float NumberDensity)
{
	uint i, j;
	uint Count = SizeX * SizeY;
	Matrix T;
	T.A = new float*[SizeY];
	T.SizeX = SizeX;
	T.SizeY = SizeY;
	T.Count = (uint)(NumberDensity * Count);

	for (i = 0; i < SizeY; i++)
	{
		T.A[i] = new float[SizeX];
		for (j = 0; j < SizeX; j++)
			T.A[i][j] = 0;
	}

	uint C = 0;
	while (C < T.Count)
	{
		i = rand(0, SizeY-1);
		j = rand(0, SizeX-1);
		if (!T.A[i][j])
		{
			T.A[i][j] = rand_without0(MinValue, MaxValue);
			C++;
		}
	}	

	return T;
}

void Clear(Matrix &M)
{
	for (uint i = 0; i < M.SizeY; i++)
		delete M.A[i];
	delete M.A;
	M.Count = 0;
	M.SizeX = 0;
	M.SizeY = 0;
}

Matrix Sum(Matrix &M1, Matrix &M2)
{
	Matrix T;
	T.Count = M1.Count;
	T.SizeX = M1.SizeX;
	T.SizeY = M1.SizeY;
	T.A = new float*[T.SizeY];

	for (uint i = 0; i < M1.SizeY; i++)
	{
		T.A[i] = new float[T.SizeX];
		for (uint j = 0; j < T.SizeX; j++)
			T.A[i][j] = M1.A[i][j] + M2.A[i][j];
	}

	return T;
}

void Print(Matrix &M)
{
	for (uint i = 0; i < M.SizeY; i++)
	{
		for (uint j = 0; j < M.SizeX; j++)
			printf("%6.2f", M.A[i][j]);
		putchar('\n');
	}
}

uint SizeOf(Matrix &M)
{
	return M.SizeX * M.SizeY * sizeof(float) + sizeof(M);
}

SparseMatrix Init(Matrix &M)
{
	SparseMatrix T;
	T.SizeX = M.SizeX;
	T.SizeY = M.SizeY;
	T.Count = M.Count;
	T.JA = new uint[T.SizeX+1];
	T.IA = new uint[T.Count];
	T.A = new float[T.Count];
	uint AC = 0;
	uint ColumnNoNull;

	for (uint i = 0; i < M.SizeX; i++)
	{
		T.JA[i] = AC;
		ColumnNoNull = 0;
		for (uint j = 0; j < M.SizeY; j++)
		{			
			if (M.A[j][i])
			{
				T.IA[AC] = j;
				T.A[AC] = M.A[j][i];
				AC++;
				ColumnNoNull++;
			}
		}
		if (!ColumnNoNull)
			T.JA[i] = AC;
	}
	T.JA[T.SizeX] = T.Count;

	return T;
}

void Clear(SparseMatrix &M)
{
	delete M.A;
	delete M.IA;
	delete M.JA;
	M.Count = 0;
	M.SizeX = 0;
	M.SizeY = 0;
}

SparseMatrix Sum(SparseMatrix &M1, SparseMatrix &M2)
{
	uint i, j;

	SparseMatrix SM;
	SM.Count = M1.Count + M2.Count;
	SM.SizeX = M1.SizeX;
	SM.SizeY = M1.SizeY;
	SM.JA = new uint[SM.SizeX+1];
	SM.IA = new uint[SM.Count];
	SM.A = new float[SM.Count];
	
	uint C1, C2;
	uint C = 0;
	uint Col = 0;
	for (i = 0; i < SM.SizeX; i++)
	{
		SM.JA[i] = C;
		if (M1.JA[i] == M1.JA[i+1])   // если в i-том столбце 1 матрицы нет элементов
		{
			if (M2.JA[i] != M2.JA[i+1])  //  если в i-том столбце 2 матрицы есть элементы
			{
				for (j = M2.JA[i]; j < M2.JA[i+1]; j++)
				{
					SM.IA[C] = M2.IA[j];
					SM.A[C] = M2.A[j];
					C++;
				}
			}        //  если в i-том столбце 2 матрицы нет элементов
		}
		else
		{
			if (M2.JA[i] == M2.JA[i+1])
			{
				for (j = M1.JA[i]; j < M1.JA[i+1]; j++)
				{
					SM.IA[C] = M1.IA[j];
					SM.A[C] = M1.A[j];
					C++;
				}
			}
			else
			{
				C1 = M1.JA[i];
				C2 = M2.JA[i];
				while ((C1 < M1.JA[i+1]) && (C2 < M2.JA[i+1]))
				{
					if (M1.IA[C1] < M2.IA[C2])
					{
						SM.IA[C] = M1.IA[C1];
						SM.A[C] = M1.A[C1];
						C++;
						C1++;
					}
					else
					if (M1.IA[C1] == M2.IA[C2])
					{
						float R = M1.A[C1] + M2.A[C2];
						if (R)
						{
							SM.IA[C] = M1.IA[C1];
							SM.A[C] = R;
							C++;
						}
						C1++;
						C2++;
					}
					else
					{
						SM.IA[C] = M2.IA[C2];
						SM.A[C] = M2.A[C2];
						C++;
						C2++;
					}
				}
				if (C1 == M1.JA[i+1])   // если в первой матрице кончилимсь строки, а во второй нет
				{
					for (j = C2; j < M2.JA[i+1]; j++)
					{
						SM.IA[C] = M2.IA[j];
						SM.A[C] = M2.A[j];
						C++;
					}
				}
				else
					for (j = C1; j < M1.JA[i+1]; j++)
					{
						SM.IA[C] = M1.IA[j];
						SM.A[C] = M1.A[j];
						C++;
					}
			}
		}
	}

	SparseMatrix SM1;
	SM1.Count = C;
	SM1.SizeX = SM.SizeX;
	SM1.SizeY = SM.SizeY;
	SM1.JA = new uint[SM1.SizeX+1];
	SM1.IA = new uint[SM1.Count];
	SM1.A = new float[SM1.Count];
	SM1.JA[SM.SizeX] = SM1.Count;

	for (i = 0; i < SM1.SizeX; i++)
		SM1.JA[i] = SM.JA[i];
	for (i = 0; i < SM1.Count; i++)
	{
		SM1.IA[i] = SM.IA[i];
		SM1.A[i] = SM.A[i];
	}

	delete SM.JA;
	delete SM.IA;
	delete SM.A;

	return SM1;
}

float Get(SparseMatrix &M, uint Row, uint Column)
{
	uint R = Row;
	for (uint i = M.JA[Column]; i < M.JA[Column+1]; i++)
		if (M.IA[i] == R)
			return M.A[i];
	return 0;
}


void Print(SparseMatrix &M)
{
	for (uint i = 0; i < M.SizeY; i++)
	{
		for (uint j = 0; j < M.SizeX; j++)
			printf("%6.2f", Get(M, i, j));
		putchar('\n');
	}
	/*uint i, j, k;
	for (i = 0; i < M.Size-1; i++)
	{
		k = M.JA[i];
		for (j = 0; j < M.Size; j++)
		{
			if (k = M.JA[i+1])
			{
				pruintf("%6.2f", 0);
				continue;
			}				
			if (j == M.IA[k])
			{
				pruintf("%6.2f", M.A[j]);
				k++;
			}
			else
				pruintf("%6.2f", 0);
		}
		putchar('\n');
	}*/
}

void PrintStruct(SparseMatrix &M)
{
	uint i;
	for (i = 0; i < M.Count; i++)
		printf("%6.2f", M.A[i]);
	printf("\n");
	for (i = 0; i < M.Count; i++)
		printf("%3i", M.IA[i]);
	printf("\n");
	for (i = 0; i <= M.SizeX; i++)
		printf("%3i", M.JA[i]);
	printf("\n");
}

uint SizeOf(SparseMatrix &M)
{
	return (M.Count) * sizeof(float) + (M.Count) * sizeof(uint) + 
		(M.SizeX + 1) * sizeof(uint) + sizeof(M);
}

void Init(SparseMatrix &M, uint Size, float MinValue, float MaxValue, float NumberDensity)
{
	/*uint i;
	uint SizeSize = Size*Size;

	srand(time(NULL));

	M.Count = (uint)(NumberDensity * SizeSize);
	M.Size = Size;
	M.A = new float[M.Count];
	M.IA = new uint[M.Count];
	M.JA = new uint[M.Size];
	//M.AssignCount = 0;

	for (i = 0; i < Size; i++)
		M.JA[i] = -1;

	uint C = 0;
	uint X, Y;
	while (C < M.Count)
	{
		X = rand(0, Size-1);
		Y = rand(0, Size-1);
		if (!Get(M, X, Y))
		{
			Set(M, rand_without0(MinValue, MaxValue), X, Y);
			C++;
		}
	}	*/
}


void Insert2(uint Elem1, float Elem2, uint Ind, uint *Arr1, float *Arr2, uint AssignCount)
{
	for (uint i = AssignCount; i > Ind; i--)	
	{
		Arr1[i] = Arr1[i-1];
		Arr2[i] = Arr2[i-1];
	}
	Arr1[Ind] = Elem1;
	Arr2[Ind] = Elem2;
}	

void Delete2(uint Ind, uint *Arr1, float *Arr2, uint AssignCount)
{
	for (uint i = Ind; i < AssignCount; i++)
	{
		Arr1[i] = Arr1[i+1];
		Arr2[i] = Arr2[i+1];
	}
}

void Set(SparseMatrix &M, float Value, uint Row, uint Column)
{
	/*uint i, j, k;
	uint ind;

	if (Value)
	{
		if (M.JA[Column] == -1)
		{
			j = Column;
			while (M.JA[j] == -1)
				j++;
			if (j == M.Size)
			{
//				ind = M.AssignCount;
				M.JA[Column] = ind;
				M.IA[ind] = Row;
				M.A[ind] = Value;
			}
			else
			{
				ind = M.JA[j];
				M.JA[Column] = ind;
//				Insert2(Row, Value, M.JA[Column], M.IA, M.A, M.AssignCount);
				for (k = Column+1; k < M.Size; k++)
					if (M.JA[k] != -1)
						M.JA[k]++;
			}
		}
		else
		{
			if (Column == M.Size - 1)
			{
				for (uint i = M.JA[Column]; i < M.Count; i++)
					if (M.IA[i] == Row)
					{
						M.A[i] = Value;
						exit(0);
					}
			}
			else
			{
				uint ind = Column+1;
				while (M.JA[ind] == -1)
					ind++;
				if (ind == M.Size)
					ind = M.Count;
				else
					ind = M.JA[ind];
				for (uint i = M.JA[Column]; i < ind; i++)
					if (M.IA[i] == Row)
					{
						M.A[i] = Value;
						exit(0);
					}
			}

//			Insert2(Row, Value, M.JA[Column], M.IA, M.A, M.AssignCount);
			for (k = Column+1; k < M.Size; k++)
				if (M.JA[k] != -1)
					M.JA[k]++;
		}
//		M.AssignCount++;
	}
	else
	{
		if (M.JA[Column] != -1)
		{
			if (Column == M.Size - 1)
			{
				for (uint i = M.JA[Column]; i < M.Count; i++)
					if (M.IA[i] == Row)
					{
//						M.AssignCount--;
//						Delete2(i, M.IA, M.A, M.AssignCount);
					}
			}
			else
			{
				uint ind = Column+1;
				while (M.JA[ind] == -1)
					ind++;
				if (ind == M.Size)
					ind = M.Count;
				else
					ind = M.JA[ind];
				for (uint i = M.JA[Column]; i < ind; i++)
					if (M.IA[i] == Row)
					{
//						M.AssignCount--;
//						Delete2(i, M.IA, M.A, M.AssignCount);
						for (k = Column+1; k < M.Size; k++)
							if (M.JA[k] != -1)
								M.JA[k]--;
					}
			}
		}

	}*/

}



void Init(SparseListMatrix &M, uint Size, uint MinValue, uint MaxValue, float NumbersDensity)
{
	/*uint i;
	uint SizeSize = Size*Size;

	srand(time(NULL));

	M.Count = (uint)(NumbersDensity * SizeSize);
	M.Size = Size;
	//M.JA = new uint[M.Size];
	M.AssignCount = 0;

	for (i = 0; i < Size; i++)
		M.JA[i] = 0;

	uint C = 0;
	uint X, Y;
	while (C < M.Count)
	{
		X = rand(0, Size-1);
		Y = rand(0, Size-1);
		if (!Get(M, X, Y))
		{
			Set(M, rand_without0(MinValue, MaxValue), X, Y);
			C++;
		}
	}	*/
}


inline void Set(SparseListMatrix &M, uint Value, uint Row, uint Column)
{
	/*uint i, j, k;
	uint ind;
	uintElem *IE;
	FloatElem *FE;
	uintElem *PI;
	FloatElem *PF;

	if (Value)
	{
		if (M.JARow[Column] == NULL) // если в текущем столбце нет элементов
		{
			j = Column;
			while (M.JARow[j] == NULL)  
			{
				j++;
				if (j = M.Size)
					break;
			}
			if (j == M.Size)  // если во всех остальных после текущего столбца нет элементов
			{
				IE = new uintElem; 
				IE->NextElem = NULL;
				IE->Value = Row;

				FE = new FloatElem;
				FE->NextElem = NULL;
				FE->Value = Value;

				M.JARow[Column] = IE;
				M.JAValue[Column] = FE;
				if (M.IA.LastElem == NULL)  // если в матрице вообще нет элементов
				{
					M.IA.LastElem = IE;
					M.IA.FirstElem = IE;
					M.A.LastElem = FE;
					M.A.LastElem = FE;
				}
				else
				{
					M.IA.LastElem->NextElem = IE;
					M.IA.LastElem = IE;
					M.A.LastElem->NextElem = FE;
					M.A.LastElem = FE;
				}
			}
			else
			{
				IE = new uintElem; 
				IE->NextElem = M.JARow[j];
				IE->Value = Row;

				FE = new FloatElem;
				FE->NextElem = M.JAValue[j];
				FE->Value = Value;

				M.JARow[Column] = IE;
				M.JAValue[Column] = FE;

				j1 = Column;
				while (M.JARow[j1] == NULL)  
				{
					j1--;
					if (j1 == -1)
						break;
				}

				if (j1 == -1)
				{
					M.IA.FirstElem = IE;
					M.A.FirstElem = IE;
				}
				else
				{
					uintElem *IE_T = M.JARow[j1];
					FloatElem *FE_T = M.JAValue[j1];

					while (IE_T != M.JARow[j])
					{
						PI = IE_T;
						PF = FE_T;
						IE_T = IE_T->NextElem;
						FE_T = FE_T->NextElem;
					}

					PI->NextElem = IE;
					PF->NextElem = FE;
				}
			}
		}
		else  // если в данном столбце есть элементы
		{
			IE = M.JARow[Column];
			FE = M.JAValue[Column];

			do
			{
				if (IE->Value == Row)
				{
					FE->Value = Value;
					exit(0);
				}
				PI = IE;
				PF = FE;
				IE = IE->NextElem;
				FE = FE->NextElem;
			}
			while (IE != NULL);

			IE = new uintElem;
			IE->NextElem = 


		}
		M.AssignCount++;
	}
	else
	{
		if (M.JA[Column] != -1)
		{
			if (Column == M.Size - 1)
			{
				for (uint i = M.JA[Column]; i < M.Count; i++)
					if (M.IA[i] == Row)
					{
						M.AssignCount--;
						Delete2(i, M.IA, M.A, M.AssignCount);
					}
			}
			else
			{
				uint ind = Column+1;
				while (M.JA[ind] == -1)
					ind++;
				if (ind == M.Size)
					ind = M.Count;
				else
					ind = M.JA[ind];
				for (uint i = M.JA[Column]; i < ind; i++)
					if (M.IA[i] == Row)
					{
						M.AssignCount--;
						Delete2(i, M.IA, M.A, M.AssignCount);
						for (k = Column+1; k < M.Size; k++)
							if (M.JA[k] != -1)
								M.JA[k]--;
					}
			}
		}

	}
*/
}

inline uint Get(SparseListMatrix &M, uint Row, uint Column)
{
	return 2;
}