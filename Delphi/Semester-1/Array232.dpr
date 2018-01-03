program Array232;

//  Программа, рассчитывающая среднее арифметическое отрицательных элементов
//    вводимого массива

{$APPTYPE CONSOLE}

var
  A: Array[1..10] of Integer;  // Вводимый массив
  NMax: Integer;               // Номер максимального элемента
  I: Integer;                  // Параметр цикла
  Neg: boolean;                // Проверка отрицательных элементов:
                               //   true - есть
                               //   false - нет
  S: Integer;                  // Сумма отрицательных элементов
  N: Integer;                  // Число отрицательных элементов
  Mean: Real;                  // Среднее арифметическое отрицательных элементов

begin

  // Ввод данных
  WriteLn('Enter length of array: ');
    Read(NMax);
  WriteLn('Enter elements of array: ');
  for I := 1 to NMax do
    Read(A[i]);
  ReadLn;

  // Вычисление
  Neg := false;
  S := 0;
  N := 0;
  for I := 1 to NMax do
    if A[I] < 0 then
    begin
      Neg := true;
      S := S + A[I];
      Inc(N);
    end;

  // Вывод
  if Neg = true then begin
    Mean := S/N;
    WriteLn('Mean value of negate elements: ', Mean:6:4)
  end
  else
    WriteLn('Nothing negate elements');

  ReadLn;
end.
