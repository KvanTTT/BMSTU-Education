/*******************************************
 * Program Name: N_Queen_Parallel.c
 * Compilation: mpicc -DBOARD_SIZE=<boardsize> N_Queen_Parallel.c
 * Execution: mpirun -np 4 a.out
 * Date: April 14' 2009
 * MTH698: Parallel Numerical Algorithm Projectwork
 * Problem Statement: A Parallel solution to N Queen Problem implementing dynamic load balancing
 * Sample output > mpicc -DBOARD_SIZE=8 N_Queen_Parallel.c
 *		         > mpirun -np 4 ./a.out
 *				Node = 3 : Results computed = 50 
 *				Node = 2 : Results computed = 34 
 *				Node = 1 : Results computed = 8 
 *				Total number of Results = 92 
 *				Time elapsed: 0.000000
 * Author: Ramnik Arora
 *
 * NOTE: Here are the number of solutions to the N Queen problem as available at:
 *				http://jsomers.com/nqueen_demo/nqueens.html 
 * 1 	1 	
 * 2 	0 	
 * 3 	0 	
 * 4 	2 
 * 5 	10
 * 6 	4 
 * 7 	40
 * 8 	92 
 * 9 	352 
 * 10 	724
 * 11 	2680
 * 12 	14200
 * 13 	73712
 * 14 	365596
 * 15 	2279184
 * 16 	14772512
 * 17 	95815104
 * 18 	666090624
 * 19 	4968057848
 * 20 	39029188884
 * 21 	314666222712
 * 22 	2691008701644
 * 23 	24233937684440
*********************************************/

/*
 * Use column major format. The output specifies the line on which the Queen should be placed.
 */
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <mpi.h>
#include <stdio.h> 
#include <time.h>


int** board;
int boardSize, addOnCut = 1, matrixSize, noResults = 0;
const int requestWorkTag = 1000;

/*
 * Used to print the board. We will print only the placement of the Queen in the 
 * column and the corresponding position can be easily computed. Board elements have been assigned
 * a value of 77 on positions where the queen have been placed.
 */
void boardPrint(int** b){
	noResults++;
    /*
	int ii = 0, jj;
    for (; ii < boardSize; ii++){
        for(jj=0; jj < boardSize; jj++){
            if (b[jj][ii] == 77)
                printf("%d\t", jj);
        }
    }
	printf("\n");
	*/
}

/*
 * A function used to clear the board and reset all enteries to zero.
 */
void clean_board(int** b)
{
	int i = 0;
    for (; i < boardSize; i++)
    {
	memset(b[i], 0, sizeof(int) * boardSize);
    }
    
}

/*
 * Only elements where (b[i][j] == 0) are not threatened and Queen can be placed here. 
 * isCut tells us whether it is safe to place the next Queen in position [row][column]
 */
int isCut(int** b, int row, int column){
    if(b[row][column] == 0){
        return 0;
    }
    else {
        return 1;
    }
}

/*
 * We will represent the position where a Queen is place by making b[row][col] = 77.
 * Further, all positions that are cut, on the row, column, primary diagonal and the 
 * secondary diagonal have their corresponding values incremented by addOnCut.
 */
void queenPlacement(int** b, int row, int col){
    int i;
    // Column
    for (i = row + 1; i < boardSize; i++)
            b[i][col] += addOnCut;
    // Row
    for (i = col + 1; i < boardSize; i++)
        b[row][i] += addOnCut;
    
    int primary,secondary;
    primary = (row > col) ? row : col;
    secondary = ((row + 1) > (boardSize - col)) ? boardSize - col : row + 1;

    // Primary Diagonal
    for (i = 1; i < boardSize - primary; i++)
        b[row + i][col + i] += addOnCut;
    
	// Secondary Diagonal
    for (i = 1; i < secondary; i++)
        b[row - i][col + i] += addOnCut;    
    
	// Place Queen on position [row][col]
    b[row][col] = 77;
}

/* 
 * The exact opposite of queenPlacement. We hereby remove a Queen from [row][col]. 
 * We negate the operations carried out by queenPlacement. All positions where a Queen
 * placed at [row][col] was cutting are decreased by addOnCut and [row][col] is initialised
 * back to zero.
 */

void queenRemoval(int** b, int row, int col){
    int i;
    // Column
    for (i = row + 1; i < boardSize; i++)
            b[i][col] -= addOnCut;
    // Row
    for (i = col + 1; i < boardSize; i++)
        b[row][i] -= addOnCut;
    
    int primary,secondary;
    primary = (row > col) ? row : col;
    secondary = ((row + 1) > (boardSize - col)) ? boardSize - col : row + 1;

	// Primary Diagonal
    for (i = 1; i < boardSize - primary; i++)
        b[row + i][col + i] -= addOnCut;
    
	//Secondary Diagonal
    for (i = 1; i < secondary; i++)
        b[row - i][col + i] -= addOnCut;    
    
    b[row][col] = 0;
}

/*
 * We must initialise the board here. An important point here is that the memory units must 
 * be contiguous since we need to send the entire matrix between nodes. Therefore, we have 
 * used an allocator to define contiguous memory units.
 */
int** boardInitialisation(){
	int ii;
    int** c = (int**)malloc(boardSize * sizeof(int*));
	int* allocator = (int*) malloc(matrixSize * sizeof(int));
	memset(allocator, 0, matrixSize * sizeof(int));
    for(ii = 0; ii < boardSize; ii++){
/*
 *	Note: Can't directly generalise from Serial program since the entire 
 *	memory board must be contiguous.
 *		b[ii] = (int *) malloc(boardSize * sizeof (int));
 *      memset(b[ii], 0, boardSize * sizeof(int));
*/
		c[ii] = allocator + ii * boardSize;
    }
    return c;
}

/*
 * The core program where the Queen Positioning and back-tracking are 
 * handled. If the column is 0, then there is no further backtracking
 * not should the system attempt to try the next row on column 0. 
 * Furthermore, if the column is boardSize - 1, then we are in the last
 * column. If here we encounter a cell which has a value 0, then we arrive 
 * at a valid solution by placing a zero in this column and printing the board.
 * If the col > 0 and col < boardSize -1, then we will place the Queen on the
 * position where b[row][col] == 0 and continue onto next column.
 */
void NQueenSolution(int** b, int row, int col){
	if(col == 0){
		queenPlacement(b, row, col);
		NQueenSolution(b, row, col + 1);
		return;
	}
    int ii;
    for(ii = 0; ii < boardSize; ii++){
        if (b[ii][col] == 0){
            if(col == boardSize - 1){
				queenPlacement(b, ii, col);
	            boardPrint(b);
                queenRemoval(b,ii,col);
            }
            else{
                queenPlacement(b, ii, col);
                NQueenSolution(b, ii, col + 1);
                queenRemoval(b, ii, col);
            }
        }
    }
}

/*
 * After the computation we must destroy the board and free the memory allocated to the same.
 */
void boardDestruction(){
	free(board[0]);
	free(board);
}

/*
 * Owing to the fact that the program us run using a wrapper file on the sunclust, we are unable to 
 * provide an input for the console. Therefore, I have used a variable BOARD_SIZE which will be 
 * declared while compilation.
 * It has been implicitly been assumed that the node with rank 0 is the manager and entrusted with
 * the job of dynamic work allocation. Thus, the communication takes the following steps. 
 * 1. The manager (rank == 0), waits for workers to request work.
 * 2. The manager assigns work to the workers.
 * 3. Workers solve the assigned subproblem assigned to them parallely.
 * 4. Once the entire work has been assigned to all workers and work completed, the program exits.
 * Program execution time is the total time the manager was alive since the program can not exit 
 * without the manager ending operations.
 */
int main(int argc, char *argv[]){

//	if (argc != 2){
//		//***printf("Please abide by the input format\n");
//		return 0;
//	}
//	else{
//		boardSize = atoi(argv[1]);
//		matrixSize = boardSize * boardSize;
//		***printf ("Board Size = %d \n", boardSize);
//	}

	boardSize = BOARD_SIZE;
	matrixSize = boardSize * boardSize;
	int rank, size, ii, jj, node, work_req, col_place, board_send, result_computed, result_dummy, work_finished, row_proc, results_dummy_send, totalResults, tempResults;
	work_finished = -1, work_req = 10, col_place = 100, board_send = 1000, result_computed = 10000, results_dummy_send = 100, totalResults = 0;
	MPI_Status status, statustemp;
	board = boardInitialisation();
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	if (rank == 0){
		clock_t start = clock(); 
		for (ii = 0; ii < boardSize; ii++){
			MPI_Recv(&node, 1, MPI_INT, MPI_ANY_SOURCE, work_req, MPI_COMM_WORLD, &status);
			MPI_Send(&ii, 1, MPI_INT, node, col_place, MPI_COMM_WORLD);
			MPI_Send(&board[0][0], matrixSize, MPI_INT, node, board_send, MPI_COMM_WORLD);
		}
		for (ii = 1; ii < size; ii++){
			MPI_Recv(&node, 1, MPI_INT, MPI_ANY_SOURCE, work_req, MPI_COMM_WORLD, &status);
			MPI_Send(&work_finished, 1, MPI_INT, node, col_place, MPI_COMM_WORLD);
			MPI_Recv(&tempResults, 1, MPI_INT, MPI_ANY_SOURCE, result_computed, MPI_COMM_WORLD, &status);
			totalResults += tempResults;
		}
	printf("Total number of Results = %d \n", totalResults);
	printf("Time elapsed: %f\n", ((double)clock() - start) / CLOCKS_PER_SEC);	
	}
	else{
		while(1){
			MPI_Send(&rank, 1, MPI_INT, 0, work_req, MPI_COMM_WORLD);
			MPI_Recv(&row_proc, 1, MPI_INT, 0, col_place, MPI_COMM_WORLD, &status);
			if (row_proc != work_finished){
				MPI_Recv(&board[0][0], matrixSize, MPI_INT, 0, board_send, MPI_COMM_WORLD, &statustemp);
				NQueenSolution(board, row_proc, 0);
			}
			else{
				MPI_Send(&noResults, 1, MPI_INT, 0, result_computed, MPI_COMM_WORLD);
				printf ("Node = %d : Results computed = %d \n", rank, noResults);
				break;
			}
		}
	}
	boardDestruction();
	MPI_Finalize();
	return 0;
}