program Matrix2;

{$APPTYPE CONSOLE}

{ Перемножение элементов столбцов исходной матрицы до минимального,
    а также поиск значений в матрице, делящихся на 7 и занесение их в массив  }

var
  X: array[1..10] of array[1..8] of Integer; //  Исходная матрица
  A: array of Integer;                       //  Массив, в который заносятся
                                             //    элементы из матрицы, делящиеся на 7
  Row, Column: Integer;                      //  Количество строк и столбцов в матрице Х
  I, J: Integer;                             //  Параметры цикла
  Min: Real;                                 //  Минимальный элемент в каждом столбце матрицы Х
  MinIndex: Integer;                         //  Номер минимального элемента в каждом столбце матрицы Х
  M: Integer;                                //  Результат перемножение всех элементов в
                                             //    столбце матрицы до минимального элемента

begin

  //  Ввод матрицы
  WriteLn('Enter matrix size Row x Column :  ');
  ReadLn(Row, Column);
  WriteLn('Enter matrix X');
  for I := 1 to Row do
    for J := 1 to Column do
      Read(X[I, J]);
  ReadLn;

  //  Вывод исходной матрицы
  WriteLn(' ');
  WriteLn('Matrix X:');
  for I := 1 to Row do
  begin
    for J := 1 to Column do
      Write(X[I, J]:4);
      WriteLn('');
  end;

  //  Перемножение всех элементов в столбце матрицы до минимального элемента
  for I := 1 to Column do
  begin
    Min := X[1, I];
    M := X[1, I];
    for J := 1 to Row do
      if X[J, I] < Min then
      begin
        Min := X[J, I];
        MinIndex := J;
      end;
    for J := 2 to MinIndex - 1 do
        M := M * X[J, I];
    X[MinIndex, I] := M;
  end;

  // Поиск в матрице значений, делящихся на 7 и их занесение в массив
  for I := 1 to Row do
    for J := 1 to Column do
      if X[I, J] mod 7 = 0 then
      begin
        SetLength(A, Length(A) + 1);
        A[High(A)] := X[I, J];
      end;

  // Вывод преобразованной матрицы
  WriteLn('');
  WriteLn('Transformed matrix:');
  for I := 1 to Row do
  begin
    for J := 1 to Column do
      Write(X[I, J]:4);
      WriteLn('');
  end;

  // Вывод массива А
  WriteLn('');
  WriteLn('Vector A: ');
  if Length(A) <> 0 then
  begin
    for I := 0 to High(A) do
      if (I+1) mod 5 = 0 then
        WriteLn('A[', I, '] = ', A[I], ', ')
      else
        Write('A[', I, '] = ', A[I], ', ');
  end
  else
    WriteLn('There are 0 elements, which divided by 7');

  ReadLn;
  ReadLn;
end.
