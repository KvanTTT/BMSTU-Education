#include <stdlib.h>
#include <stdio.h>
#include <mpi.h>
#include <omp.h>

int main(int argc, char **argv) 
{ 
   int size,rank;
   char processor_name[MPI_MAX_PROCESSOR_NAME];
   int namelen, i;  
      
   int a[100];
   int sum;

   MPI_Init(&argc, &argv); 
   MPI_Comm_size(MPI_COMM_WORLD,&size);
   MPI_Comm_rank(MPI_COMM_WORLD,&rank);
   MPI_Get_processor_name(processor_name,&namelen);
   printf("Process %d of %d on %s\n",rank,size,processor_name);
  
   sum = 0;
   for( i=0; i<100; i++){
     a[i]=i;
     sum += a[i]; 
   } 
   printf( "[%d]sequental sum=%d\n",rank, sum );
   
   sum = 0;

// #pragma omp parallel for private(i) shared(a) reduction(+:sum)
#pragma omp parallel private(i) shared(a) reduction(+:sum)
   {
     int orank;
     orank = omp_get_thread_num();
#pragma omp for nowait 
     for( i=0; i<100; i++)
     {
       sum += a[i];
     }
    printf( "[%d,%d] private OpenMp sum=%d\n",rank, orank, sum );
 
   }
   printf( "[%d] shared OpenMp sum=%d\n",rank, sum );

   MPI_Finalize();
   return (0);
}
 
