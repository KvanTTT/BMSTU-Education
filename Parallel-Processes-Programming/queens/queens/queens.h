#include <stdlib.h>
#include <stdio.h>
#include <mpi.h>
#include <pcontrol.h>
#include <string.h>
#include <time.h>

#ifdef OMP_MODE
  #include <omp.h>
#endif

#define FINISH_WORK		-1
#define WANT_TO_WORK		1
#define COL_NUMBER		2
#define FIELD_PACK		3
#define RESULT_PACK		4

clock_t time_start;
#define TIMER_START time_start = clock()
#define TIMER_GET ((double)clock() - time_start) / CLOCKS_PER_SEC

#define ABS(a) 		( a >= 0 ? a : -a )
#define MAX(a, b)	( a > b ? a : b )
#define MIN(a, b)	( a < b ? a : b )

#define NCOEF 8
static int ic[NCOEF] = { -2, -1, 1, 2, -2, -1, 1, 2 };
static int jc[NCOEF] = { 1, 2, 2, 1, -1, -2, -2, -1 };

#define MARK(field, i, j)	field[i][j]++
#define UMARK(field, i, j)	field[i][j]--

int** field_init(int m);
void field_clear(int** field);
int count_variants_top(int** field, int row1, int col1, int row2, int col2, int m);
int count_variants(int** field, int row, int m);
void queens_put(int** field, int i, int j, int m);
void queens_remove(int** field, int i, int j, int m);

int is_attacked(int row1, int col1, int row2, int col2);
int make_double_pos(int col1, int col2);
