#include "queens.h"

int main(int argc, char* argv[])
{
	int rank, size;
	MPI_Status stat;
	char* tracefile;
	double tm;
	int node, wfinish = FINISH_WORK;
	int res, tres;
	//int** field;
	int i, j, m, m2;
	#ifdef OMP_MODE
	int th, limit;
	#endif
	
	if (argc != 2)
		printf("M parameter forgotten\n");
	else
		if (sscanf(argv[1], "%d", &m) != 1)
			printf("Argument %s has wrong format. Integer value expected.\n");
		else
		{			
			m2 = m * m;
		
			MPI_Init(&argc, &argv);
			
			tracefile = getenv("TVTRACE");
			if( tracefile != NULL )
			  MPI_Pcontrol(TRACEFILES, NULL, tracefile, 0);
			else
			  MPI_Pcontrol(TRACEFILES, NULL, "trace", 0);

			MPI_Pcontrol(TRACELEVEL, 1, 1, 1);
			MPI_Pcontrol(TRACENODE, 1000000, 1, 1);
			
			MPI_Comm_rank(MPI_COMM_WORLD, &rank);
			MPI_Comm_size(MPI_COMM_WORLD, &size);
			
			if (rank == 0)
			{
				int** field = field_init(m);
				#ifdef OMP_MODE
				  th = omp_get_max_threads();
				  limit = (size - 1) * th;
				#endif
				
				TIMER_START;
				
				for (i = 0; i < m; i++)
					for (j = 0; j < m; j++)
						if (!is_attacked(0, i, 1, j))
						{
							int pos = make_double_pos(i, j);
							MPI_Recv(&node, 1, MPI_INT, MPI_ANY_SOURCE, WANT_TO_WORK, MPI_COMM_WORLD, &stat);
							MPI_Send(&pos, 1, MPI_INT, node, COL_NUMBER, MPI_COMM_WORLD);
						}
				
				res = 0;
				#ifdef OMP_MODE
				for (i = 0; i < limit; i++)
				#else
				for (i = 1; i < size; i++)
				#endif
				{
					MPI_Recv(&node, 1, MPI_INT, MPI_ANY_SOURCE, WANT_TO_WORK, MPI_COMM_WORLD, &stat);
					MPI_Send(&wfinish, 1, MPI_INT, node, COL_NUMBER, MPI_COMM_WORLD);
					MPI_Recv(&tres, 1, MPI_INT, node, RESULT_PACK, MPI_COMM_WORLD, &stat);
					
					res += tres;
				}
				
				tm = TIMER_GET;
				printf("Nodes involved: %d\nM = %d\nResult: %d variants\nTotal time: %f\n", size, m, tm, res);
				field_clear(field);
			}
			else
			{
				#ifdef OMP_MODE
				  #pragma omp parallel private(i, res, stat) shared(rank, m, m2)
				#endif
				{
				  res = 0;
				  int** field = field_init(m);
				  do
				  {   
				      #ifdef OMP_MODE
						#pragma omp critical
					  #endif
					  { 
						MPI_Send(&rank, 1, MPI_INT,  0, WANT_TO_WORK, MPI_COMM_WORLD);
					  }
					  #ifdef OMP_MODE
						#pragma omp critical
					  #endif
					  {
						MPI_Recv(&i, 1, MPI_INT, 0, COL_NUMBER, MPI_COMM_WORLD, &stat);
					  }
					  if (i == FINISH_WORK)
					  {
						  #ifdef OMP_MODE
						     #pragma omp critical
					      #endif
						  {
							MPI_Send(&res, 1, MPI_INT, 0, RESULT_PACK, MPI_COMM_WORLD);
						  }
					  }
					  else
					  {
						  memset(*field, 0, sizeof(int) * m2);
						  
						  int col1 = (i >> 16) & 0xFFFF;
						  int col2 = i & 0xFFFF;
						  res += count_variants_top(field, 0, col1, 1, col2, m);
					  }
				  } while (i != FINISH_WORK);
				  field_clear(field);
				}
			}

			MPI_Finalize();
		}
		
	return 0;
}

int** field_init(int m)
{
	int i;
	int size = m * m;
	int** field = (int**)calloc(m, sizeof(int*));
	int* buf = (int*)calloc(size, sizeof(int));
	
	for (i = 0; i < m; i++)
		field[i] = &buf[i * m];
		
	return field;
}

void field_clear(int** field)
{
	free(*field);
	free(field);
}

int count_variants_top(int** field, int row1, int col1, int row2, int col2, int m)
{
	int res;
	queens_put(field, row1, col1, m);
	queens_put(field, row2, col2, m);
	res = count_variants(field, row1 + 2, m);
	queens_remove(field, row2, col2, m);
	queens_remove(field, row1, col1, m);
	
	return res;
}

int count_variants(int** field, int row, int m)
{
	int j, res;
	
	if (row == m)
		res = 2;
	else
	{
		res = 0;

		for (j = 0; j < m; j++)
		{
			if (field[row][j] == 0)
			{
			  queens_put(field, row, j, m);
			  res += count_variants(field, row + 1, m);
			  queens_remove(field, row, j, m);
			}
		}
	}
	
	return res;
}

void queens_put(int** field, int i, int j, int m)
{
	int k, j1, i1;
	int k1 = m - MAX(i, j), k2 = MIN(i, m - 1 - j);
	int a = ABS(i - j);
	
	// Горизонталь и вертикаль
	for (k = 0; k < m; k++)
	{
		MARK(field, i, k);
		MARK(field, k, j);
	}
	
	// Главная диагональ
	for (k = -MIN(i, j); k < k1; k++)
		MARK(field, i + k, j + k);
	// Побочная дианональ
	for (k = -MIN(m - 1 - i, j); k <= k2; k++)
		MARK(field, i - k, j + k);
}

void queens_remove(int** field, int i, int j, int m)
{
	int k, j1, i1;
	int k1 = m - MAX(i, j), k2 = MIN(i, m - 1 - j);
	int a = ABS(i - j);
	
	// Горизонталь и вертикаль
	for (k = 0; k < m; k++)
	{
		UMARK(field, i, k);
		UMARK(field, k, j);
	}
	
	// Главная диагональ
	for (k = -MIN(i, j); k < k1; k++)
		UMARK(field, i + k, j + k);
	// Побочная дианональ
	for (k = -MIN(m - 1 - i, j); k <= k2; k++)
		UMARK(field, i - k, j + k);
}

int is_attacked(int row1, int col1, int row2, int col2)
{
	return (ABS(row1 - row2) <= 1) && (ABS(col1 - col2) <= 1);
}

int make_double_pos(int col1, int col2)
{
	return (col1 << 16) | col2;
}

void unmake_double_pos(int value, int* col1, int* col2)
{
	*col1 = (value >> 16) & 0xFFFF;
	*col2 = value & 0xFFFF;
}
