program Strings;

{ Программа, оставляющая в каждом слове только первое вхождение каждой буквы,
  а затем печатающая все слова, кроме последнего }


{$APPTYPE CONSOLE}

var
  St, St1: string[210];  //  Исходная строка
  Word: string;     //  Последнее слово в строке
  C: string;        //  Одинаковые подряд идущие буквы
  I, J: Integer;    //  Параметры цикла
  N: Integer;       //  Позиция первого вхождения последнего слова
                    //    в преобразованную строку
  Count: Integer;   //  Количество повторяющихся букв в каждом слове

begin

  // Ввод данных
  WriteLn('Enter string: ');
  ReadLn(St);

  // Удаление из исходной строки повторяющихся букв
  I := 1;
  while I < Length(St) do
  begin
    if St[I] <> ' ' then
    begin
      C := St[I];
      J := I + 1;
      Count := 0;
      while St[J] = C do
      begin
        J := J + 1;
        Count := Count + 1;
      end;
      if Count > 0 then
        Delete(St, I + 1, Count);
    end;
    I := I + 1;
  end;

  // Поиск последнего слова
  for I := Length(St) downto 1 do
  begin
    if (St[I] <> ' ') and (St[I] <> '.') and (St[I] <> '!') and (St[I] <> '?') then
      Break;
  end;
  J := I;
  while St[J] <> ' ' do
  begin
    J := J - 1;
  end;
  Word := Copy(St, J + 1, I - J);

  // Удаление всех слов, схожих с последним
  repeat
    N := Pos(Word, St);
    St1 := '';
    St1 := Copy(St1, 1, N-1);
    if (St[N+Length(Word)+1] = ' ') or (St[N+Length(Word)+1] = '.') then
      Delete(St, N, Length(Word)+1)
    else
    begin
      I := N;
      while St[I] <> ' ' do
        I := I + 1;
      St1 := Copy(St, N, I - N);
      Delete(St, N, I);
    end;
  until N = 0;
  St1[Length(St1)] := '.';

  // Вывод преобразованной строки
  WriteLn;
  WriteLn('Formatted string: ');
  WriteLn(St1);

  ReadLn;
end.
