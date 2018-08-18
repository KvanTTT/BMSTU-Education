#include <stdlib.h>
#include <stdio.h>
//  #include <mpi.h>
#include <omp.h>

int main(int argc, char **argv) 
{ 
   int size,rank,sum,niii;
   sum=0;
   niii=0;
   #pragma omp parallel private(rank)
   {
     double time1;
     int ni,i,nii;
          
     rank = omp_get_thread_num();
     size = omp_get_num_threads();
     
     printf( "Hello World !!\nI am %d of %d!!\n", rank, size );
     
     for(i=0; i<12; i++){
        printf( "LOOP 1  i=%d t=%d\n", i,rank );
       
     }

     #pragma omp for
     for(i=0; i<12; i++){
        printf( "LOOP 2  i=%d t=%d\n", i,rank );
       
     }

     #pragma omp for schedule(static,2)
     for(i=0; i<12; i++){
        printf( "LOOP 3  i=%d t=%d\n", i,rank );
       
     }
     
     nii=0;
     time1 = omp_get_wtime();
     #pragma omp for
     for(i=0; i<120000000; i++){
        nii++;   
     }
     
     #pragma omp single 
     {
       printf("LOOP 2a time=%lf rank=%d\n", omp_get_wtime()-time1, rank);
     }


     ni=0;
     time1 = omp_get_wtime();
     #pragma omp for schedule(dynamic,2)
     for(i=0; i<120000000; i++){
       ni++;
       #pragma omp atomic
       sum++;
     }
     printf( "LOOP 4  i=%d t=%d\n", ni,rank );     
     printf("LOOP 4 time=%lf rank=%d\n", omp_get_wtime()-time1, rank);

     
     #pragma omp master
     {
        printf("LOOP 4 sum=%d\n",sum);
	
	sum = 0;
     }
     #pragma omp barrier     

     niii=0;
     time1 = omp_get_wtime();
     #pragma omp for schedule(dynamic,2)
     for(i=0; i<120000000; i++){
       ni++;
     }
     #pragma omp critical
     {
       sum+=ni;
     }
     printf("LOOP 5 time=%lf rank=%d\n", omp_get_wtime()-time1, rank);
     printf("LOOP 5  i=%d t=%d\n", ni,rank );     
     
     #pragma omp master
     {
        printf("LOOP 5 sum=%d\n",sum);
	
	sum = 0;
     }

     #pragma omp barrier     

     
     time1 = omp_get_wtime();
     #pragma omp for schedule(dynamic,2) reduction(+:niii)
     for(i=0; i<120000000; i++){
       niii++;
     }
     printf("LOOP 6 time=%lf rank=%d\n", omp_get_wtime()-time1, rank);
     printf("LOOP 6  i=%d t=%d\n", niii,rank );     
     
     #pragma omp master
     {
        printf("LOOP 6 sum=%d\n",niii);
	
	sum = 0;
     }
     

     
   }
   

   return (0);
}
 
