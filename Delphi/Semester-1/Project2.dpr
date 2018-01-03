program Chars;

{ Программа, печатающая таблицу символов заданного вида,
    преобразовывает вводимую строку символов,
    складывает вводимую последовательность цифр и
    преобразовывает вещественное число в заданную форму     }

{$APPTYPE CONSOLE}

var
  I, J: Char;         // Параметры циклов в 1
  St: array of char;  // Вводимая строка во 2 и 3
  K: Char;            // Переменная для ввода строки 2-я и 3-я
  L: Integer;         // Длина вводимой строки для 2-ой и 3-ей
                      //   задачи для динамического массива
  N: Integer;         // Счетчик символов( N <= 10) во 2
  Sum: Integer;       // Сумма цифр в 3 задаче
  X: Real;            // Вводимое число в 4-ой задаче
  Power: Integer;     // Степень преобразованного числа
                      //   в 4-ой задаче
  NS: Integer;        // Счетчик строк во 2

begin

  // Задача 1
  WriteLn('Task 1: ');
  for I := '1' to '9' do
  begin
    WriteLn('':4);

    // 1 столбец
    for J := '1' to '9' do
      if J = I then
        Write(J)
      else
        Write('0');
    Write(' ', #179, ' ');

    // 2 столбец
    for J := '1' to '9' do
      if J >= I then
        Write(Chr((10-(Ord(J)-48))+48))
      else
        Write('0');
    Write(' ', #179, ' ');

    // 3 столбец
    for J := Pred(I) to '9' do
      Write(J);
    for J := '0' to Chr(Ord(I)-2) do
      Write(J);

  end;

  WriteLn;
  Write('':10, #179, '':11, #179, ' 9012345678');
  WriteLn('':100);

  // Задача 2
  WriteLn('Task 2: ');
  WriteLn('Enter string: ');
  Write('0');
  for L := 1 to 7 do
    Write(' ':9, L);
  WriteLn;

  // Ввод строки ва 2-ой задаче
  L := 0;
  while K <> '.' do
  begin
    Read(K);
    if (K = #10) or (K = #13) then
      Continue;
    L := L + 1;
    SetLength(St, L);
    St[L-1] := K;
  end;

  // Преобразование строки
  L := 0;
  NS := 0;
  if Length(St) > 1 then
  begin
    WriteLn('':100);
    WriteLn('Formatted string: ');
    N := 0;
    Write(NS:2, ' ');
    for L := 0 to High(St)-1 do
    begin
      Write(St[L]);
      N := N + 1;
      if (St[L] = ',') then
      begin
        N := 0;
        NS := NS + 1;
        WriteLn;
        Write(NS:2, ' ');
        Continue;
      end;
      if N mod 10 = 0 then
      begin
        N := 0;
        NS := NS + 1;
        WriteLn;
        Write(NS:2, ' ');
      end;
    end;
  end
  else
    WriteLn('You dont enter the text');
  WriteLn('':100);

  // Задача 3
  WriteLn('Task 3: ');
  K := ' ';
  L := 0;
  // Ввод последовательности символов
  while K <> '.' do
  begin
    Read(K);
    L := L + 1;
    SetLength(St, L);
    St[L-1] := K;
  end;

  // Сложение цифр
  Sum := 0;
  L := 0;
  if Length(St) > 1 then
  begin
    while L <= High(St)-2 do
    begin
      if (St[L] = '+') then
        Sum := Sum + Ord(St[L+1]) - Ord('0');
      if (St[L] = '-') then
        Sum := Sum - Ord(St[L+1]) + Ord('0');
      L := L + 2;
    end;
  end;
  WriteLn('Sum = ', Sum);
  WriteLn('':100);

  // Задача 4
  WriteLn('Task 4: ');
  WriteLn('Enter real value ');
  Read(X);

  // Преобразование числа в заданную форму
  if X <> 0 then
  begin
    if Abs(X) >= 1  then
    begin
      Power := 0;
      while Abs(X) >= 1 do
      begin
        X := X / 10;
        Power := Power + 1;
      end;
    end;
    if Abs(X) <= 0.01  then
    begin
      Power := 0;
      while Abs(X) <= 0.1 do
      begin
        X := X * 10;
        Power := Power - 1;
      end;
    end;
  end;

  // Вывод числа
  WriteLn;
  WriteLn('Answer :');
  if X >= 0 then
    Write('+');
  Write(X:10:9, 'E');
  if Power >= 0 then
    Write('+', Power)
  else
    Write(Power);

  ReadLn;
  ReadLn;
end.
