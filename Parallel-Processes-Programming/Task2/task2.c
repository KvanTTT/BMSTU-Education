#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <mpi.h>
 
int main(int argc, char **argv)
{
  int size = 0;
  int rank = 0;
  int i = 0;
  char msg[255] = "";
  char msg1[255] = "";
  double time = 0.0;
  MPI_Status stat;
  const int N = 100;
  MPI_Request request;
 
  MPI_Init(&argc, &argv);
 
  MPI_Comm_size(MPI_COMM_WORLD, &size);
  MPI_Comm_rank(MPI_COMM_WORLD, &rank);
   
  if (rank % 2)
  {
     sprintf(msg, "hello there %d", rank);
     printf("send %s to %d\n", msg, rank-1);
 
     time = MPI_Wtime();
      
     for (i = 0; i < N; i++)
     {
        MPI_Isend(msg, strlen(msg)+1, MPI_BYTE, rank-1, 1, MPI_COMM_WORLD, &request);
        MPI_Wait(&request, &stat);
        sprintf(msg1, "some data; i=%d; rank=%d; time=%lf\n", i, rank, MPI_Wtime() - time);
        puts(msg1);
        MPI_Irecv(msg, sizeof(msg), MPI_BYTE, rank-1, 1, MPI_COMM_WORLD, &request);
        MPI_Wait(&request, &stat);
     }
 
     printf("rank %d time=%lf\n", rank, MPI_Wtime() - time);
  }
  else
  {
     for (i = 0; i < N; i++)
     {
        MPI_Irecv(msg, sizeof(msg), MPI_BYTE, rank+1, 1, MPI_COMM_WORLD, &request);
        MPI_Wait(&request, &stat);
        sprintf(msg1, "some data; i=%d; rank=%d; time=%lf\n", i, rank, MPI_Wtime() - time);
        puts(msg1);
        MPI_Isend(msg, strlen(msg)+1, MPI_BYTE, rank+1, 1, MPI_COMM_WORLD, &request);
        MPI_Wait(&request, &stat);
     }
 
     printf("recv %s from %d\n", msg, rank);
  }
 
  MPI_Finalize();
 
  return 0;
}