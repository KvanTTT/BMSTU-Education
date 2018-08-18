CC = mpicc-TV
SRC = queens.c
OBJ = queens
OBJ_OMP = queens_mpi

OMP_KEY=OMP_MODE

mpi: $(SRC)
	$(CC) $(SRC) -o $(OBJ)

omp: $(SRC)
	$(CC) -fopenmp -D$(OMP_KEY) $(SRC) -o $(OBJ_OMP)

clean:
	rm -f $(OBJ)
