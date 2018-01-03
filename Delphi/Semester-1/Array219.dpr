program Array219;

//  Программа, рассчитывающая среднее арифметическое отрицательных элементов
//    вводимого массива

{$APPTYPE CONSOLE}

var
  A: Array[1..13] of Integer;  // Вводимый массив
  NMax: Integer;               // Длина массива
  MinIndex: Integer;           // Номер минимального элемента
  Min: Integer;                // Минимальное значение массива
  I: Integer;                  // Параметр цикла
  Temp: Integer;               // Переменная, необходима для обмена значениями
                               //   между двумя переменными

begin

  // Ввод данных
  WriteLn('Enter length of array: ');
    Read(NMax);
  WriteLn('Enter elements of array: ');
  for I := 1 to NMax do
    Read(A[i]);
  ReadLn;

  // Вычисление
  Min := A[1];
  for I := 1 to NMax do
  begin
    if A[I] < Min then
    begin
      Min := A[I];
      MinIndex := I;
    end;
  end;

  // Вывод
  if MinIndex <> 4 then
  begin
    Temp := A[4];
    A[4] := A[MinIndex];
    A[MinIndex] := Temp;
  end
  else
    WriteLn('Fouth element of array coincide with element with minimal value');

  WriteLn('Output reformed array: ');
  for I := 1 to NMax do
    Write(A[I],' ');

  ReadLn;
end.
