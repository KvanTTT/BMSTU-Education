program ProceduresAndFunctions;

{Программа, создающая матрицу случайных чисел, преобразовывающая ее по
формуле А[I,J]=A[I,J]/произведение сумм элементов строк матрицы,
а также формирование матриц В и СХ                                    }

{$APPTYPE CONSOLE}

type
  Matrix = array of array of Real;

procedure TransformMatrix(var M: Matrix; var ErrorDenom: boolean);
// Преобразование матрицы по формуле
// А[I,J]=A[I,J]/произведение сумм элементов строк матрицы
var
  P, S : Real;
  I, J: Integer;
begin
  P := 1;
  for I := 0 to High(M) do
  begin
    S := 0;
    for J := 0 to High(M[I]) do
      S := S + M[I, J];
    P := P * S;
  end;
  if P <> 0 then
  begin
    for I := 0 to High(M) do
      for J := 0 to High(M[I]) do
        M[I, J] := M[I, J]/P;
    ErrorDenom := false;
  end
  else
    ErrorDenom := true;
end;

label
  1, Start;

var
  A   : Matrix;  // Исходная матрица
  B, C: Matrix;  // Преобразованные матрицы B = A[4,4], C = A[5,5]
  N, M: Integer; // Количество строк и столбцов в исходной матрицы
  I, J: Integer; // Параметры цикла
  E: Char;       // Флаг конца программы
  S, F: Real;    // Нижняя и верхняя граница случайных числе
  RandomMatrix: Char;
  ErrorDenom: Boolean;

begin
  // Ввод исходных данных
  Start:
  WriteLn;
  WriteLn('Enter number of rows and columns (N x M):');
  ReadLn(N, M);
  SetLength(A, N, M);
  WriteLn('Generate random matrix?');
  ReadLn(RandomMatrix);
  if RandomMatrix in ['Y', 'y'] then
  begin
    WriteLn('Enter upper and lower limit of random values (Start, Finish):');
    ReadLn(S, F);
    // Генерирование матрицы А
    Randomize;
    for I :=  0 to N - 1 do
      for J := 0 to M - 1 do
        A[I, J] := Random*Abs(F-S) - Abs(S);
  end
  else
  begin
    WriteLn('Enter matrix:');
    for I := 0 to High(A) do
      for J := 0 to High(A[I]) do
        Read(A[I, J]);
  end;

  // Вывод матрицы А
  WriteLn;
  WriteLn('Initial matrix A:');
  for I := 0 to N - 1 do
  begin
    WriteLn;
    for J := 0 to M - 1 do
      Write(A[I, J]:9:4);
  end;

  // Преобразование матрицы А по заданной формуле,
  //  а также формирование матрицы В и С из А
  TransformMatrix(A, ErrorDenom);
  if ErrorDenom = true then
  begin
    WriteLn;
    WriteLn('Divide by zero!!! Error');
    WriteLn('Please input another matrix:');
    goto 1;
  end;
  

  // Сокращение матрицы M[n, m] до матрицы
  // M[RowCount, ColumnCount]
  SetLength(B, 4, 4);
  for I := 0 to High(B) do
    for J := 0 to High(B[I]) do
      B[I, J] := A[I, J];

  SetLength(C, 5, 5);
  for I := 0 to High(C) do
    for J := 0 to High(C[I]) do
      C[I, J] := A[I, J];

  // Вывод преобразованной матрицы А
  WriteLn(' ':100);
  WriteLn('Transformed matrix A:');
  for I := 0 to N - 1 do
  begin
    WriteLn;
    for J := 0 to M - 1 do
      Write(A[I, J]:9:4);
  end;

  // Вывод матрицы В
  WriteLn(' ':100);
  WriteLn('Matrix B(4 x 4):');
  for I := 0 to 3 do
  begin
    WriteLn;
    for J := 0 to 3 do
      Write(B[I, J]:9:4);
  end;

  // Вывод матрицы С
  WriteLn(' ':100);
  WriteLn('Matrix C(5 x 5):');
  for I := 0 to 4 do
  begin
    WriteLn;
    for J := 0 to 4 do
      Write(C[I, J]:9:4);
  end;

  1:
  WriteLn(' ':100);
  WriteLn('Do you want to enter another matrix? ( N - to exit )');
  ReadLn(E);
  if (E = 'N') or (E = 'n') then
    exit
  else
  if (E = 'Y') or (E = 'y') then
    goto Start
  else
    goto 1;

  ReadLn;
end.
