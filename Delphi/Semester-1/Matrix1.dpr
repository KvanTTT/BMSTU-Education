program Matrix1;

{ Вычисление суммы ряда с заданной точностью,
    Введение матрицы, вектора и операции с ними}

{$APPTYPE CONSOLE}

var
  M: array[1..15, 1..20] of Real; // Исходная матрица
  X: array of Real;               // Массив, в который будут записываться
                                  //   значения из матрицы, большие чем сумма ряда
  Row, Column: Integer;           // Количество строк и столбцов в матрице
  XMax: Real;                     // Максимальное значение элемента в векторе X
  XMaxIndex: Integer;             // Номер максимального значения вектора Х
  T: Real;                        // Сумма ряда
  N: Integer;                     // Количество итераций цикла
  A: Real;                        // Параметр цикла
  Eps: Real;                      // Точность, с которой рассчитывается сумма ряда
  Cur: Real;                      // N-ный член ряда
  SqrA: Real;                     // А*А
  I, J: Integer;                  // Параметры цикла
  Temp: Real;                     // Переменная, необходимая для обмена
                                  //   значениями между переменными

begin

  // Ввод параметра А, матрицы
  WriteLn('Enter const A and precision: ');
  ReadLn(A, Eps);
  WriteLn('Enter matrix size Row x Column :  ');
  ReadLn(Row, Column);
  WriteLn('Enter matrix');
  for I := 1 to Row do
    for J := 1 to Column do
      Read(M[I, J]);
  ReadLn;

  // Рассчет суммы ряда
  Cur := A;
  SqrA := A*A;
  T := 0;
  N := 0;
  WriteLn('N = 0', ' Cur = ', Cur:6:4);
  while Cur > Eps do
  begin
    N := N + 1;
    Cur := Cur*SqrA/((N+1)*(N+2));
    WriteLn('N = ', N, ' Cur = ', Cur:6:4);
    T := T + Cur;
  end;


  // Формирование вектора Х
  for I := 1 to Row do
    for J := 1 to Column do
      if T < M[I, J] then
      begin
        SetLength(X, Length(X) + 1);
        X[High(X)] := M[I, J];
      end;

  // Нахождение максимального элемента в векторе Х и его номер
  XMax := X[0];
  for I := 0 to High(X) do
  begin
    if X[I] > XMax then
    begin
      XMax := X[I];
      XMaxIndex := I;
    end;
  end;

  // Обмен значений между первым и максимальным элементом
  Temp := X[0];
  X[0] := X[XMaxIndex];
  X[XMaxIndex] := Temp;

  // Вывод суммы ряда, матрицы и вектора
  WriteLn('');
  WriteLn('Sum of row: ', T:6:4, ' with precision: ', Eps:6);

  WriteLn('');
  WriteLn('Matrix M:');
  for I := 1 to Row do
  begin
    for J := 1 to Column do
      Write(M[I, J]:7:4, '  ');
      WriteLn('');
  end;

  WriteLn('');
  WriteLn('Vector X: ');
  for I := 0 to High(X) do
  if (I+1) mod 5 = 0 then
    WriteLn('X[', I, '] = ', X[I]:7:4,  ', ')
  else
    Write('X[', I, '] = ', X[I]:7:4,  ', ');

  ReadLn;
end.

// Конец программы
