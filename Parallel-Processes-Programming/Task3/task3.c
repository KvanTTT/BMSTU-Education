#include<mpi.h>
#include<stdio.h>
#include<stdlib.h>
#include <pcontrol.h>

#define an 2000
#define am 1500
#define bm 1200

int main (int argc,char **argv)
{
   double time,time_seq, time_par;
   int rank, size;

   char *tracefile;

   MPI_Init(&argc,&argv);
   
   tracefile = getenv("TVTRACE");
   if( tracefile != NULL ){
      printf( "tv tracefile=%s\n", tracefile );
      MPI_Pcontrol(TRACEFILES, NULL, tracefile, 0);      
   }
   else{
      MPI_Pcontrol(TRACEFILES, NULL, "trace", 0);
   }
   MPI_Pcontrol(TRACELEVEL, 1, 1, 1);
   MPI_Pcontrol(TRACENODE, 1000000, 1, 1);


   MPI_Comm_rank (MPI_COMM_WORLD,&rank);
   MPI_Comm_size (MPI_COMM_WORLD,&size);


   if( !rank ){
      double *a,*b,*c, *c0;
      int i,i1,j,k;
      int ann;
      MPI_Status *st;
      MPI_Request *rq,rq1;
      rq = (MPI_Request*) malloc( (size-1)*sizeof(MPI_Request) );
      st = (MPI_Status*) malloc( (size-1)*sizeof(MPI_Status) );


      ann=an/size+((an%size)?1:0);
//      printf("[%d]ann=%d\n", rank, ann );

      a=(double*) malloc(am*an*sizeof(double));
      b=(double*) malloc(am*bm*sizeof(double));
      c=(double*) malloc(an*bm*sizeof(double));
      for(i=0;i<am*an;i++)
        a[i]=rand()%301;
      for(i=0;i<am*bm;i++)
        b[i]=rand()%251;
      printf( "Data ready [%d]\n", rank );
     
      c0 = (double*)malloc(an*bm*sizeof(double));

     
      time = MPI_Wtime();  
      for (i=0; i<an; i++)
         for (j=0; j<bm; j++)
         {
            double s = 0.0;
            for (k=0; k<am; k++)
              s+= a[i*am+k]*b[k*bm+j];
            c0[i*bm+j] = s;
      } 
      time = MPI_Wtime() - time;
      printf("Time seq[%d] = %lf\n", rank, time );
      time_seq = time;

      MPI_Barrier( MPI_COMM_WORLD );
      time=MPI_Wtime();

      MPI_Bcast( b, am*bm, MPI_DOUBLE, 0, MPI_COMM_WORLD);
      printf( "Data Bcast [%d]\n", rank );

      for( i1=0, j=1; j<size; j++, i1+=ann*am ){
         printf( "Data to Send [%d] %016x[%4d] =>> %d\n", rank, a+i1, i1, j );
         MPI_Isend( a+i1, ann*am, MPI_DOUBLE, j, 101, MPI_COMM_WORLD, &rq1 );
         MPI_Request_free( &rq1 ); 
         printf( "Data Send [%d] =>> %d\n", rank, j );
      }
      printf( "Data Send [%d]\n", rank );
      
      MPI_Isend( a+i1, 1, MPI_DOUBLE, 0, 101, MPI_COMM_WORLD, &rq1 );
      MPI_Request_free( &rq1 ); 
      
      printf( "Data Send [%d] =>> %d\n", rank, j );


      for(i=(i1/am);i<an;i++)
        for(j=0;j<bm;j++){
          double s=0.0;
          for(k=0;k<am;k++)
             s+=a[i*am+k]*b[k*bm+j];
          c[i*bm+j]=s;
        }

      printf( "Job done  [%d]\n", rank );
      for( i1=0, j=1; j<size; j++, i1+=(ann*bm) ){
         printf( "Data to Recv [%d] %016x[%4d] =>> %d\n", rank, c+i1, i1/bm, j );
         MPI_Irecv( c+i1, ann*am, MPI_DOUBLE, j, 102, MPI_COMM_WORLD, rq+(j-1) );
      }         
      MPI_Waitall( size-1, rq, st );
      
      time=MPI_Wtime()-time;
      printf("time [%d]=%12.8lf\n",rank,time);
      time_par = time;

      printf( "Data collected [%d]\n", rank );
      
      time=MPI_Wtime();
      int ok = 1;
      for(i=0;i<an*bm;i++)
        if( c[i] != c0[i] ){
           ok = 0;
           printf( "Fail [%d %d] %lf != %lf\n", i/bm, i%bm, c[i], c0[i] );
           break;
        }
      time=MPI_Wtime()-time;
      if( ok ){
        printf( "Data verifeid [%d] time = %lf\n", rank, time );
        printf( "SpeedUp S(%d) = %14.10lf\n", size, time_seq/time_par );
        printf( "Efitncy E(%d) = %14.10lf\n", size, time_seq/(time_par*size) );
      }
        
   }
   else
   {
      int ann;
      double *a,*b,*c;
      MPI_Status st;
      int i,j,k;

      MPI_Pcontrol(TRACEEVENT, "entry", 0, 0, "");

      ann= an/size + ((an%size)?1:0);
//      if(rank==1)
//        printf("[%d]ann=%d = %d / %d \n", rank, ann, an, size );
        
      a=(double*)malloc(ann*am*sizeof(double));
      b=(double*)malloc(bm*am*sizeof(double));
      c=(double*)malloc(ann*bm*sizeof(double));
      printf( "Mem allocated [%d]\n", rank );

     
      MPI_Barrier( MPI_COMM_WORLD );
      MPI_Pcontrol(TRACEEVENT, "exit", 0, 0, "");
      time = MPI_Wtime();


      MPI_Pcontrol(TRACEEVENT, "entry", 1, 0, "");
      
      MPI_Bcast(b,am*bm,MPI_DOUBLE,0,MPI_COMM_WORLD);
      printf( "Data Bcast [%d]\n", rank );
      
      MPI_Recv( a, ann*am, MPI_DOUBLE, 0, 101, MPI_COMM_WORLD, &st);
      printf( "Data Recv [%d]\n", rank );
      
      MPI_Pcontrol(TRACEEVENT, "exit", 1, 0, "");
                                    
      MPI_Pcontrol(TRACEEVENT, "entry", 2, 0, "");
      for( i=0; i<ann; i++ )
        for(j=0;j<bm;j++){
            double s=0.0;
            
            for( k=0; k<am; k++ ){
               s+=a[i*am+k]*b[k*bm+j];
            }
/*    
            if(1==rank){
               if(0==j){
                  printf( "c[%d<%d %d] = %lf\n", i,ann,j, s );
               }
            }
*/
            c[i*bm+j]=s;
        }
      printf( "Job done  [%d]\n", rank );
      MPI_Pcontrol(TRACEEVENT, "exit", 2, 0, "");

      MPI_Pcontrol(TRACEEVENT, "entry", 3, 0, "");
      MPI_Send( c, ann*bm,  MPI_DOUBLE, 0, 102, MPI_COMM_WORLD);
      printf( "Data returned [%d]\n", rank );
      MPI_Pcontrol(TRACEEVENT, "exit", 3, 0, "");

      time=MPI_Wtime()-time;
      printf("time [%d]=%12.8lf\n",rank,time);
   }
   
   MPI_Finalize();
   return 0;
}
