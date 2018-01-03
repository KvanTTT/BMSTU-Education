program WorkingWithText;

{ Программа выполняющая различиные операции с текстом }
{  по выбору пользователя такие как выравние текста,  }
{     расчёт арифметических операций внутри текста    }
{     и различные преобразования внутри текста        }

{$APPTYPE CONSOLE}

uses
  SysUtils,
  Math,
  StrUtils;

type
  Text = array [1..6] of string;  // Тип массива строк для передачи в процедуру
  Act = array [1..10] of string;  // Список меню действий с текстом
  PosText = record   // Начало и конец предложения
    Col:Integer;
    Row:Integer;
  end;
  PText = Array [1..10,1..2] of PosText; // Массив начал и концов предложений



var
   // Текст
  S: Text = ('Бла Это первое Бла предложение текста Бла. Да будет же здесь выражение 3/6+2/4!',
             'Здесь же разместим самое длинное слово самоедлинноеслово!',
             'Здесь же самое короткое ы и выражение 3-5*6.',
             'Ровно как и здесь 3-10+3*4-14/7 же.',
             'Что же можно расположить здесь?',
             'Пожалуй что уже ничего же.');

   // Список пунктов меню
  Menu: Act = ('1 - [Найти самое длинное слово]',
               '2 - [Найти количество слов между самым длинным и самым коротким]',
               '3 - [Найти слово, которое встречается в каждом предложении]',
               '4 - [Расчитать все арифметические операции в тексте]',
               '5 - [Выровнять текст по ширине]',
               '6 - [Выровнять текст по правому краю]',
               '7 - [Выровнять текст по левому краю]',
               '8 - [Заменить слово из предложения]',
               '9 - [Удалить из текста слово]',
               '0 - [Выход]');

  I: Integer; { Счётчик цикла }
  C: Char;  { Переменная для выбора пункта меню }
  IndexSant: PText; { Массив начал и концов предложений }
  CountSant: Integer; { Колличество предложений }

    { Процедура для определения начал и концов предложений }
    {              и определения их колличества            }
  Procedure InitText (var NumPr: Integer; var IndexSant:PText);
  var
    I,I1: Integer; { Счётчики циклов }
  begin
    IndexSant[1,1].Row := 1;
    IndexSant[1,1].Col := 1;
    NumPr := 1;
    for I := 1 to 6 do
      for I1 := 1 to Length(S[I])do
      begin
        if S[I][I1] in ['!','?','.'] then   // запись конца предложения
        begin
          IndexSant[NumPr,2].Row := I;
          IndexSant[NumPr,2].Col := I1;
          NumPr := NumPr + 1;
        end;
        if not(S[I][I1] in ['!','?',' ','.']) and (IndexSant[NumPr,1].Row = 0) then
        begin
          IndexSant[NumPr,1].Row := I;    // запист начала предложения
          IndexSant[NumPr,1].Col := I1;
        end
      end;
      NumPr := NumPr - 1;
  end;


  { Преобразование строки для вывода русских букв }
  Function ConvStr (S: String):String;
  var
    Code: Integer;
    I: Integer;
  begin
    Result:='';
    for I :=1 to Length(S) do
    begin
      Code := Ord(S[I]);
      Case S[I] of
        'А'..'п': Dec(Code,64);
        'р'..'я': Dec(Code,16);
        'ё':Inc(Code,57);
      end;
      Result := Result + Chr(Code);
    end;
  end;

  { Процедура перевода Unicode в ASCII }
  Function StrConv (S: String):String;
  var
    Code1: Integer;
    I1: Integer;
  begin
    Result := S;
    for I1 :=1 to Length(S) do
    begin
      if (Ord(S[I1]) in [128..175]) then
        Result[I1] := Chr(Ord(S[I1]) + 64);
      if (Ord(S[I1]) > 223 ) then
        Result[I1] := Chr(Ord(S[I1]) + 16);
    end;
  end;


  { Функция для расчёта арифметических выражений в строке }
  Procedure ArithRes (PosStr: Integer; Posp: Integer);
  var
  I, I1 : Integer; { Счётчики циклов                }
  Resul: String; { Результат выражения              }
  StartP, EndP: Integer; { Начало и конец выражения }
  Size : Integer; { Размер выражения                }

    { Процедура, считающая мультипликативные выражения }
    Procedure OperatMulti (PosStr,PosOp:Integer; var Res: String; var StartP: Integer; var EndP: Integer);
    var
      Op1, Op2: Real; { Левый и правый операнты }
      Answer: Real; { Результат операции        }
      J: Integer;  { Счётчик                    }
      Sign: Integer; { Знак операнта            }
      S1: String;  { Вспомогательная строка     }

    begin

      Sign := 1;
      J := PosOp-1;
      S1 := '';

      { Выделение левого операнта }
      while ( not(S[PosStr][J] in ['+','-','*','/','(',')',' ']) and (J > 0)) do
      begin
        S1 :=  S[PosStr][J] + S1;
        J := J - 1;
      end;

      if S[PosStr][J] = '-' then
        Sign := -1;
      If (S[PosStr][J] = '-') or (S[PosStr][J] = '+') then
        StartP := J
      else
        StartP := J + 1;

      Op1 := Sign * StrToFloat(S1);

      J := PosOp + 1;
      S1 := '';
      Sign := 1;

      { Выделение правого операнта }
      if S[PosStr][J] = '-' then
      begin
        Sign := -1;
        J := J + 1;
      end
      else
        while ( not(S[PosStr][J] in ['+','-','*','/','(',')',' ','.','!','?',','])
                and (S[PosStr][J] <> ' ')) do
        begin
          S1 := S1 + S[PosStr][J];
          J := J + 1;
        end;

      EndP := J - 1;

      Op2 := Sign * StrToFloat(S1);

      { Обработка операции }
      if S[PosStr][PosOp] = '*' then
        Answer := Op1 * Op2;
      if S[PosStr][PosOp] = '/' then
      begin
         if Op2 = 0 then
           Answer := Op1 * Op2
         else
           Answer := Op1 / Op2;
      end;

      Res := FloatToStr(Answer);
      If Answer >=0 then
        Res := '+' + Res;

    end;

     { Процедура, считающая аддъетивные выражения }
    procedure OperateAdd( PosStr,PosOp:Integer; var Res:String; var StartP: Integer; var EndP: Integer);
    var
      Sum: Real; { Сумма                   }
      Tec: Real; { Текущий член выражения  }
      SR: String; { Вспомогательная строка }
      K: Integer; { Счётчик                }
    begin

      K := PosOp;
      StartP := K;
      Sr:='';

      repeat
        Repeat
          SR := SR + S[PosStr][K];  // Выделение очередного члена
          K := K + 1;
        until S[PosStr][K] in [' ','.','!','?','+','-'];

        Tec := StrToFloat(SR);
        Sum := Sum + Tec;           // Добавление очередного члена к сумме
        Sr:='';

      until S[PosStr][K] in [' ','.','!','?'];

      EndP := K - 1;
      Resul := FloatToStr(Sum);

    end;

  begin
    for I := PosStr to 6 do
      for I1 := 1 to Length(S[I]) do
        if S[I][I1] in ['*','/'] then { Поиск всех мультипликативых выражений }
        begin
          OperatMulti(I,I1, Resul, StartP, EndP);
          Size :=  EndP - StartP + 1;
          Delete(S[I], StartP, Size);
          Insert(Resul, S[I], StartP)
        end;

     for I := 1 to 6 do
     begin
     I1 := 1;
     repeat
        { Поиск всех аддъетивных выражений }
        if S[I][I1] in ['+','-','1','2','3','4','5','6','7','8','9','0'] then
        begin
          OperateAdd(I,I1, Resul, StartP, EndP);
          Size :=  EndP - StartP + 1;
          Delete(S[I], StartP, Size);
          Insert(Resul, S[I], StartP);
          I1 := I1 + Size;
        end;
        I1 := I1 + 1;
     until I1 > Length(S[I]);
     end;

     for I := 1 to 6 do
       WriteLn(COnvStr(S[I]));
  end;

  { Процедура, ищущая в тесте самое большое слово }
  Procedure FindBigWord();
  var
    J,J1: Integer; { Счётчики циклов                        }
    MaxL: Integer; { Максимальная длина                     }
    StP,StrB: Integer; { Позиция слова в строках и столбцах }
    StMp: Integer; { Пизиция Максимального слова...         }
    TecL: Integer;  { Длина текущего слова                  }
  begin
  MaxL := 0;
  StP := 0;
  TecL := 0;
  StMp := 0;
    // Поиск...
    For J := 1 to 6 do
      for J1 := 1 to Length(S[J]) do
      begin
          if S[J][J1] in [' ','.','!','?',','] then
          begin
            if TecL > MaxL then
            begin
              MaxL := TecL;
              StMp := StP;
              StrB := J;
            end;
            TecL := 0;
          end
          else
          begin
            If TecL = 0 then
              StP := J1;
            TecL := TecL + 1;
          end;
        end;
     // Вывод
      J := Stmp;
      Write(ConvStr('Самое длинное слово - '));
      repeat
        Write(ConvStr(S[StrB][J]));
        J := J + 1;
      until S[StrB][J] in [' ','.','!','?'];
      WriteLn;
  end;

  { Процедура, считающая количество слов между самым коротким }
  {                  и самым длинным                          }
  procedure WordsBet();
  var
    MinRowPos, MaxRowPos: Integer; { Позиция максимального слова и минимального в строках  }
    MinColPos, MaxColPos: Integer; { Позиция максимального слова и минимального в столбцах }
    TecColPos: Integer;  { Позиция текущего слова в строке                                 }
    J, J1: Integer; { Счётчики циклов                                                      }
    I,I1 : Integer; { Счётчики циклов                                                      }
    JE,J1E: Integer; { Вспомогательный переменные                                          }
    MinL,MaxL,TecL : Integer; { Минимальная максимальная и текущая длины слов              }
    Count: Integer; { Колличество слов между самым длинным и коротким словами              }
    IsWord: Boolean;
  begin
    MinL := 254;
    MaxL := 0;
    TecL := 0;

    { Поиск самого короткого и самого длинного слова }
    For J := 1 to 6 do
      for J1 := 1 to Length(S[J]) do
      begin
        if (S[J][J1] in [' ','.','!','?',',']) and not(S[J][J1+1] in [' ',',']) then
        begin
          if TecL > MaxL then
          begin
            MaxL := TecL;
            MaxColPos := TecColPos;
            MaxRowPos  := J;
          end;
          if TecL < MinL then
          begin
            MinL := TecL;
            MinColPos := TecColPos;
            MinRowPos  := J;
          end;
          TecL := 0;
        end
        else
        begin
          If TecL = 0 then
            TecColPos := J1;
          TecL := TecL + 1;
        end;
      end;

    { Определение  начала и конца цикла подсчёта слов }
    if MinRowPos > MaxRowPos Then
    begin
      J := MaxRowPos;
      JE := MinRowPos;
      J1 := MaxColPos;
      J1E := MinColPos;
    end;
    if MinRowPos < MaxRowpos then
    begin
      J := MinRowPos;
      JE := MaxRowPos;
      J1 := MinColPos;
      J1E := MaxColPos;
    end;
    if MinRowPos = MaxRowpos then
    begin
      J := MinRowPos;
      JE := MinRowPos;
      if MinColPos < MaxColpos then
      begin
        J1 := MinColPos;
        J1E := MaxColPos;
      end
      else
      begin
        J1 := MaxColPos;
        J1E := MinColPos;
      end;
    end;

  Count := 0;
  I1 := J1;
  I := J;

  // Подсчёт слов
  repeat
    If not(S[I][I1] in ['.','!','?',' ']) then
      begin
        If Not(IsWord) then
          Count := Count + 1;
        IsWord := True;
      end;
      if (S[I][I1] in ['.','!','?',' ']) then
      begin
        IsWord := False;
      end;

    I1 := I1 + 1;
    if I1 > Length(S[I]) then
    begin
      I := I+1;
      I1 := 1;
    end;
  until (I = JE) and (I1 = J1E);

  WriteLn;
  Count := Count - 1;
  Write(ConvStr('Между словами "'));
  repeat
    Write(ConvStr(S[MaxRowPos][MaxColPos]));
    MaxColPos := MaxColPos + 1;
  until S[MaxRowPos][MaxColPos] in [' ','.','!','?'];
  Write(ConvStr('" и "'));
  repeat
    Write(ConvStr(S[MinRowPos][MinColPos]));
    MinColPos := MinColPos + 1;
  until S[MinRowPos][MinColPos] in [' ','.','!','?'];
  Write('" ',Count,ConvStr(' слов'));
  WriteLn;
  end;

  { Процедура, ищущая слово, которое встречается в каждом предложении }
  procedure FindWordSent(CountP: Integer; IndexSant: PText);
  var
    FWord: String; { слово из первого предложения        }
    FSWord: String; { Вспомогательная строка             }
    IsWord: Boolean; { есть ли слово в каждо предложении }
    IsWordS:Boolean;  { есть ли слово в предложении      }
    I, I1, I2, I3: Integer; { Счётчики циклов            }
  begin
    I1 := IndexSant[1,1].Col;
    I := IndexSant[1,1].Row;
    FWord := '';
    IsWord := False;
    repeat
    { Выделение слова в первом предложении }
      While not(S[I][I1] in [' ','!','?','.',',']) do
      begin
        Fword := FWord + S[I][I1];
        I1 := I1 +1;
        if (I1 > Length(S[I])) then
        begin
          I := I + 1;
          I1 := 1;
        end;
      end;
      for I2 := 2 to CountP do
      begin
        IsWordS := False;
        I3 := IndexSant[I2,1].Row;
        repeat
        { Поиск слова в текущем предложении }
          If I3 =  IndexSant[I2,1].Row then
          begin
            if PosEx(FWord,S[I3],IndexSant[I2,1].Col) <> 0 then
              IsWords := True;
          end
          else
          begin
            if I3 =  IndexSant[I2,2].Row then
            begin
              if (PosEx(FWord,S[I3],1) <> 0) and
              (PosEx(FWord,S[I3],1) < IndexSant[I2,2].Col) then
                IsWords := True;
            end
            else
              if (I3 <>  IndexSant[I2,1].Row) and (I3 <>  IndexSant[I2,2].Row) then
                if PosEx(FWord,S[I3],1) <> 0 then
                  IsWords := True;
          end;
            I3 := I3 + 1;
        until I3 > IndexSant[I2,2].Row;
        If not(IsWordS) then
            Break;
      end;
      if (I2-1 = CountP) and IsWordS then
      begin
        IsWord := True;
        break;
      end;
     FWord := '';
     If not(S[I][I1] in ['!','?','.']) then
       I1 := I1+1;
    Until S[I][I1] in ['!','?','.'];

      WriteLn;
    if (IsWord) then
      WriteLn(ConvStr('Слово, присутствующее в каждом предложении - '+FWord))
    else
      WriteLn(ConvStr('Такого слова нет'));

  end;

  { Выравнивание текста по ширине }
  Procedure WidthText (S:Text);
  var
    CountW: Integer; { Количество слов в предложении                   }
    CountNeed: Integer; { Необходимое колличество пробелов м/у словами }
    CountOst: Integer; { Остаток пробелов                              }
    CountTec: Integer; { Имеющееся количество пробелов между словами   }
    K, I1, I2: Integer; { Счётчики циклов                              }
    IsWord: Boolean; { Было ли начало слова                            }
  begin
    K := 1;
   repeat
     CountW := 0;
     CountTec := 0;
     { Расчёт необходимого колиества пробелов }
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
      Write(ConvStr(S[K][I1]));
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
   Until K = 7;

  end;

  { Выравни