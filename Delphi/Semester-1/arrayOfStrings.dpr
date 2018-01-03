program ArrayOfStrings;

{ Программа выполняющая различиные операции с текстом }
{  по выбору пользователя такие как выравние текста,  }
{     расчёт арифметических операций внутри текста    }
{     и различные преобразования внутри текста        }

{$APPTYPE CONSOLE}

uses
  SysUtils, StrUtils;

type
  TText = array[1..7] of string;
  TMenu = array[1..10] of string;

label
  BeginOperation;

var
  Text: TText =
(' Это кусок текста про процессоры ',
'The P1 and PMMX processors represent the fifth generation in the Intel x86 ',
'series of microprocessors, and their processor kernels are very similar. ',
'PPro, P2 and P3 all have the sixth generation kernel. These three processors ',
'are almost identical except for the fact that new instructions are added ',
'to each new model. Ну хватит уже про процессоры, пора что-нибудь посчитать ',
'выражение 1 + 4 * 9 + 7*2   - 4');
  Word: string;
  Text1: TText;
  Act: Integer;
  SubStr, NewSubStr: string;

Menu: TMenu =('1 - Найти самое длинное слово в каждой строке',
              '2 - Найти количество слов в самом длинном предложении',
              '3 - Найти слово, которое наиболее часто встречается',
              '4 - Расчитать все арифметические выражения в тексте',
              '5 - Выровнять текст по ширине',
              '6 - Выровнять текст по левому краю максимальной строки',
              '7 - Заменить одну подстроку на другую во всем тексте',
              '8 - Удалить из текста слова с цифрами',
              '9 - Восстановить исходный текст',
              '0 - Выход');

function ConvStr(Str: string): string;
// Функция, позволяющая печать русских символов
var
  Code: Integer;
  I: Integer;
begin
  Result:='';
  for I :=1 to Length(Str) do
  begin
    Code := Ord(Str[I]);
    Case Str[I] of
      'А'..'п': Dec(Code,64);
      'р'..'я': Dec(Code,16);
      'ё':Inc(Code,57);
    end;
    Result := Result + Chr(Code);
  end;
end;

procedure ConvStr1(var Str: string);
var
  Code: Integer;
  I: Integer;
begin
  for I :=1 to Length(Str) do
  begin
    Code := Ord(Str[I]);
    Case Str[I] of
      'А'..'п': Dec(Code,64);
      'р'..'я': Dec(Code,16);
      'ё':Inc(Code,57);
    end;
    Str[I] := Chr(Code);
  end;
end;


procedure InitText(var Text: TText);
// Удаление лишних пробелов, а также преобразование текста для вывода русских букв
var
  I: Integer;
begin
  for I := Low(Text) to High(Text) do
  begin
    Text[I] := Trim(Text[I]);
    ConvStr1(Text[I]);
  end;
end;

procedure InitMenu;
var
  I: Integer;
begin
  for I := Low(Menu) to High(Menu) do
    ConvStr1(Menu[I]);
end;

procedure WriteText(Text: TText);
// Печать тескта
var
  I, J: Integer;
begin
  for I := Low(Text) to High(Text) do
  begin
    for J := 1 to Length(Text[I]) do
      Write(Text[I][J]);
    WriteLn;
  end;
  WriteLn;
end;

procedure WriteMenu;
// Печать меню
var
  I, J: Integer;
begin
  for I := Low(Menu) to High(Menu) do
  begin
    for J := 1 to Length(Menu[I]) do
      Write(Menu[I][J]);
    WriteLn;
  end;
  WriteLn;
end;

function MaxLengthWord(var St: string): string;
// Поиск слова с максимальной длиной в строке
var
    MaxLengthOfWord: Integer;
    J, K: Integer;
begin
    MaxLengthOfWord := 0;
    J := 1;
    K := 1;
    St := St + ' ';
    while J < Length(St)-1 do
    begin
      K := J;
      while St[K] in [' '] do
        K := K + 1;
      J := K;
      while not (St[K] in [' ']) do
      begin
        K := K + 1;
      end;
      if K - J > MaxLengthOfWord then
      begin
        MaxLengthOfWord := K - J;
        Result := Copy(St, J, MaxLengthOfWord);
      end;
        J := K;
    end;
end;

procedure WriteMaxLengthWords(Text: TText);
// Печать самых длинных слов в каждой строке текста
var
  I: Integer;
begin
  WriteLn(ConvStr('Самое длинное слово в '));
  for I := Low(Text) to High(Text) do
    WriteLn(I, ConvStr(' строке -  '), MaxLengthWord(Text[I]));
end;


procedure NumberOfWordsInLongestSentence(Text: TText);
// Поиск и печать количества слов в самом длинном предложении
var
  I, J, K: Integer;
  NumberOfWords: Integer;
  MaxNumberOfWords: Integer;

begin
  NumberOfWords := 0;
  MaxNumberOfWords := 0;
  for I := Low(Text) to High(Text) do
  begin
    J := 0;
    while J < Length(Text[I]) do
    begin
      J := J + 1;
      if Text[I][J] in [' ', ';', ',', ':', '.', '!', '?'] then
      begin
        NumberOfWords := NumberOfWords + 1;
        if Text[I][J] in ['.', '!', '?'] then
        begin
          if MaxNumberOfWords < NumberOfWords then
            MaxNumberOfWords := NumberOfWords;
          NumberOfWords := 0;
        end;
        K := J;
        while (Text[I][K] in [' ', ';', ',', ':', '.', '!', '?']) and (K < Length(Text[I])) do
          K := K + 1;
        J := K;
      end;
    end;
    if not(Text[I][Length(Text[I])] in [';', ',', ':', '.', '!', '?']) then
      NumberOfWords := NumberOfWords + 1;
  end;

  WriteLn(ConvStr('Число слов в самом длинном предложении:'));
  WriteLn(MaxNumberOfWords);
end;

procedure DeleteWordsWithFigures(var Text: TText);
// Удаление слов с цифрами
var
  I, J, K: Integer;
  BeginPos, EndPos: Integer;
  Count: Integer;
begin
  for I := Low(Text) to High(Text) do
  begin
    J := 0;
    while J < Length(Text[I]) do
    begin
      J := J + 1;
      if Text[I][J] in ['0'..'9'] then
      begin
        K := J - 1;
        while Text[I][K] <> ' ' do
          K := K - 1;
        BeginPos := K + 1;
        K := J;
        while Text[I][K] <> ' ' do
          K := K + 1;
        EndPos := K;
        Count := EndPos - BeginPos;
        if Count > 1 then
        begin
          Delete(Text[I], BeginPos, Count + 1);
          J := BeginPos;
        end
        else
          J := EndPos + 1;
      end;
    end;
  end;
end;

procedure CalculateExpressions(var Text: TText);
// Рассчет математических операций в тексте
var
  I, J, K1, K2: Integer;
  Elem1, Elem2: Integer;
  Result: Integer;
  Number: string;
  K11, K21: Integer;

begin
  for I := Low(Text) to High(Text) do
  begin
    J := 0;
    while J < Length(Text[I]) do
    begin
      J := J + 1;
      if Text[I][J] in ['*','/'] then
      begin
        K1 := J - 1;
        if Text[I][K1] = ' '  then
        begin
          while Text[I][K1] = ' ' do
            K1 := K1 - 1
        end;
        if Text[I][K1] in ['0'..'9'] then
          Elem1 := Ord(Text[I][K1])-48
        else
          Continue;

        K2 := J + 1;
        if Text[I][K2] = ' '  then
        begin
          while Text[I][K2] = ' ' do
            K2 := K2 + 1
        end;
        if Text[I][K2] in ['0'..'9'] then
          Elem2 := Ord(Text[I][K2])-48
        else
        begin
          J := K2;
          Continue;
        end;

        if Text[I][J] = '*' then
          Result := Elem1 * Elem2
        else
          Result := Elem1 div Elem2;
        Number := Chr(Result div 10 + 48) + Chr(Result mod 10 + 48);
        Delete(Text[I], K1, K2-K1+1);
        Insert(Number, Text[I], K1);
        J := K1 + 1;
      end;
    end;
  end;

  for I := Low(Text) to High(Text) do
  begin
    J := 0;
    while J < Length(Text[I]) do
    begin
      J := J + 1;
      if Text[I][J] in ['+','-'] then
      begin
        K1 := J - 1;
        if Text[I][K1] = ' '  then
        begin
          while Text[I][K1] = ' ' do
            K1 := K1 - 1
        end;
        if Text[I][K1] in ['0'..'9'] then
        begin
          K11 := K1;
          if Text[I][K1-1] in ['0'..'9'] then
          begin
            Elem1 := (Ord(Text[I][K1-1])-48)*10 + Ord(Text[I][K1])-48;
            K11 := K1-1;
          end
          else
            Elem1 := Ord(Text[I][K1])-48;
        end
        else
          Continue;

        K2 := J + 1;
        if Text[I][K2] = ' '  then
        begin
          while Text[I][K2] = ' ' do
            K2 := K2 + 1
        end;
        if Text[I][K2] in ['0'..'9'] then
        begin
          K21 := K2;
          if Text[I][K2+1] in ['0'..'9'] then
          begin
            Elem2 := (Ord(Text[I][K2])-48)*10 + Ord(Text[I][K2+1])-48;
            K21 := K2+1;
          end
          else
            Elem2 := Ord(Text[I][K2])-48;
        end
        else
        begin
          J := K2;
          Continue;
        end;

        if Text[I][J] = '+' then
          Result := Elem1 + Elem2
        else
          Result := Elem1 - Elem2;
        Number := Chr(Result div 10 + 48) + Chr(Result mod 10 + 48);
        Delete(Text[I], K11, K21-K11+1);
        Insert(Number, Text[I], K11);
        J := K1 + 1;
      end;
    end;
  end;
end;

procedure AbundantWord(Text: TText);
// Поиск и печать наиболее часто втречающегося слова
label
  1;

var
  I, J, K, L: Integer;
  Words: array of string;
  Word: string;
  NumberOfWords: array of Integer;
  MaxNumberOfWords: Integer;
  Index: Integer;

begin
  for I := Low(Text) to High(Text) do
  begin
    J := 0;
    while J < Length(Text[I]) do
    begin
      J := J + 1;
      if not(Text[I][J] in [' ', ';', ',', ':', '.', '!', '?']) then
      begin
        K := J;
        while not(Text[I][K] in [' ', ';', ',', ':', '.', '!', '?']) do
          K := K + 1;

        Word := AnsiLowerCase(Copy(Text[I], J, K-J));
        for L := 0 to High(Words) do
          if Word = Words[L] then
          begin
            NumberOfWords[L] := NumberOfWords[L] + 1;
            goto 1;
          end;
        SetLength(Words, Length(Words) + 1);
        SetLength(NumberOfWords, Length(NumberOfWords) + 1);
        Words[High(Words)] := Word;
        NumberOfWords[High(NumberOfWords)] := 1;
        1:
        J := K;
      end;
    end;
  end;

  MaxNumberOfWords := NumberOfWords[0];
  for I := 0 to High(NumberOfWords) do
    if MaxNumberOfWords < NumberOfWords[I] then
    begin
      MaxNumberOfWords := NumberOfWords[I];
      Index := I;
    end;

  WriteLn(ConvStr('Наиболее часто встречающееся слово - '), Words[Index]);
end;


procedure TextWidthAlign(S:TText);
//  Выравнивание текста по ширине
var
    CountW: Integer;
    CountNeed: Integer;
    CountOst: Integer;
    CountTec: Integer;
    K, I1, I2: Integer;
    IsWord: Boolean;
begin
    K := 1;
repeat
     CountW := 0;
     CountTec := 0;
      for I1 := 1 to Length(S[K]) do
      begin
        if (S[K][I1] = ' ') then
        begin
          CountTec := CountTec + 1;
          IsWord := False;
        end;
        if (S[K][I1] <> ' ') then
        begin
          If IsWord <> True then
            CountW := CountW + 1;
          IsWord := True
        end
      end;
      if (K = 1) then CountW := CountW-1;
      CountNeed := (79 - Length(S[K]) + COuntTec) div (CountW );
      CountOst := (79 - Length(S[K]) + COuntTec) mod (CountW );

      { Вывод с выравниванием по ширине }
      CountTec := 0;
      for I1 := 1 to Length(S[K]) do
      begin
      Write(S[K][I1]);
        if S[K][I1] = ' ' then
          CountTec := CountTec + 1;
        if (S[K][I1] = ' ') and (S[K][I1+1] <> ' ') then
        begin
          for I2 := 1 to (CountNeed - CountTec) do
            Write(' ');
          if CountOst > 0 then
          begin
            CountOst := CountOst - 1;
            Write(' ');
          end;
        CountTec := 0;
        end;

      end;
      WriteLn;
      K := K+1;
   until K = Length(S)+1;
end;

procedure ReplaceSubstr(var Text: TText; Substr, NewSubStr: string);
// Замена одной подстроки на другую во всем тексте
var
  I: Integer;
begin
  for I := Low(Text) to High(Text) do
    Text[I] := ReplaceStr(Text[I], SubStr, NewSubStr);
end;


begin
  InitText(Text);
  InitMenu;
  Text1 := Text;
  WriteLn(ConvStr('Исходный текст:'), ' ':100);
  WriteText(Text1);

  BeginOperation:
  WriteLn;
  WriteLn(ConvStr('Выбирите действие'));
  WriteMenu;

  ReadLn(Act);
  Case Act of
    1: WriteMaxLengthWords(Text1);
    2: NumberOfWordsInLongestSentence(Text1);
    3: AbundantWord(Text1);
    4:
    begin
      CalculateExpressions(Text1);
      WriteText(Text1);
    end;
    5: TextWidthAlign(Text);
    6:     ;
    7:
    begin
      WriteLn(ConvStr('Введите заменяемую подстроку: '));
      ReadLn(SubStr);
      WriteLn(ConvStr('Введите новую подстроку: '));
      ReadLn(NewSubStr);
      ReplaceSubstr(Text1, Substr, NewSubStr);
      WriteText(Text1);
    end;
    8:
    begin
      CalculateExpressions(Text1);
      DeleteWordsWithFigures(Text1);
      WriteText(Text1);
    end;
    9: Text1 := Text;
    0: Exit;
  end;
  goto BeginOperation;

  ReadLn;
end.
