program Integrals;

{Прог}

{$APPTYPE CONSOLE}

type
  Func = function(X: Real): Real;

function RightRectangles(A, B, H: Real; F: Func): Real;
// Вычисление интеграла методом правых прямоугольников
var
  X: Real;
  Sum: Real;
begin
  Sum := 0;
  X := A;
  while X < B do
  begin
    X := X + H;
    Sum := Sum + F(X);
  end;
  Sum := Sum * H;
  Result := Sum;
end;

function Ueddls(A, B, H: Real; F: Func): Real;
// Вычисление интеграла методом Уэддля
var
  X1: Real;
  Sum: Real;
begin
  X1 := A;
  Sum := 0;
  while X1 < B - H do
  begin
    Sum := Sum + F(X1);
    X1 := X1 + H;
    Sum := Sum + 5*F(X1);
    X1 := X1 + H;
    Sum := Sum + F(X1);
    X1 := X1 + H;
    Sum := Sum + 6*F(X1);
    X1 := X1 + H;
    Sum := Sum + F(X1);
    X1 := X1 + H;
    Sum := Sum + 5*F(X1);
    X1 := X1 + H;
    Sum := Sum + F(X1);
  end;
  Result := Sum * 0.05 * 6 * H;
end;

function F(X: Real): Real; // Исходная функция
begin
  Result := 1/Exp(X*Ln(X));
end;

function ConvStr(S: string): string; // Функция, позволяющая
                                     //  печатать русские символы
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

var
  A, B: Real;       // Начальная и конечная граница интегрирования
  N1, N2: Integer;  // Количества разбиений
  I: Integer;       // Параметр цикла
  Eps: Real;        // Исходная точность
  S1RR, S2RR, S1U, S2U: Real; // Значения площадей для разных методов
                              //  и количеств разбиений
  H, H1, H2: Real;  // Величины шагов для разных разбиений
  S, S1: Real;      // Площади для вычисления с заданной точностью

begin
  // Ввод данных
  WriteLn(ConvStr('Введите начальное и конечное значения'));
  ReadLn(A, B);
  {WriteLn(ConvStr('Введите двое значения разбиений, кратных 6-ти'));
  ReadLn(N1, N2);       }
  WriteLn(ConvStr('Введите точность'));
  ReadLn(Eps);

  {// Вычисление Шагов интегрирования и самих площадей
  H1 := (B - A) / N1;
  H2 := (B - A) / N2;
  S1RR := RightRectangles(A, B, H1, F);
  S2RR := RightRectangles(A, B, H2, F);
  S1U  := Ueddls(A, B, H1, F);
  S2U  := Ueddls(A, B, H2, F);

  // Вывод таблицы с полученными площадями
  Write(#218);
  for I := 4 to 38 do
    if I mod 13 <> 0 then
      Write(#196)
    else
      Write(#194);
  Write(#191);
  WriteLn;
  WriteLn(#179, ' ':9, #179, ' ':10, '  ', #179, ' ':12, #179);
  WriteLn(#179, ' Method  ', #179, '  N = ', N1:5, ' ', #179, '  N = ', N2:5, ' ', #179);
  WriteLn(#179, ' ':9, #179, ' ':10, '  ', #179, ' ':12, #179);
  Write(#195);
  for I := 4 to 38 do
    if I mod 13 <> 0 then
      Write(#196)
    else
      Write(#197);
  Write(#180);

  WriteLn;
  WriteLn(#179, ' ':9, #179, ' ':10, '  ', #179, ' ':12, #179);
  WriteLn(#179, ' RRect   ', #179, '  ', S1RR:9:6, ' ', #179, '  ', S2RR:9:6, ' ', #179);
  WriteLn(#179, ' ':9, #179, ' ':10, '  ', #179, ' ':12, #179);
  Write(#195);
  for I := 4 to 38 do
    if I mod 13 <> 0 then
      Write(#196)
    else
      Write(#197);
  Write(#180);

  WriteLn;
  WriteLn(#179, ' ':9, #179, ' ':10, '  ', #179, ' ':12, #179);
  WriteLn(#179, ' Ueddls  ', #179, '  ', S1U:9:6, ' ', #179, '  ', S2U:9:6, ' ', #179);
  WriteLn(#179, ' ':9, #179, ' ':10, '  ', #179, ' ':12, #179);
  Write(#192);
  for I := 4 to 38 do
    if I mod 13 <> 0 then
      Write(#196)
    else
      Write(#193);
  Write(#217);      }

  WriteLn;

  // Вычисление интегралов с заданной точностью
  {H := (B - A) / 2;
  S1 := RightRectangles(A, B, H, F);
  repeat
    S := S1;
    H := H / 2;
    S1 := RightRectangles(A, B, H, F);
  until Abs(S1 - S) < Eps;

  WriteLn(ConvStr('Значение интеграла методом правых прямоугольников: '));
  Write(S1, ConvStr(' с точностью '), Eps:6:4);
  WriteLn;   }

  H := (B - A) / 6;
  S1 := Ueddls(A, B, H, F);
  repeat
    S := S1;
    H := H / 6;
    S1 := Ueddls(A, B, H, F);
  until Abs(S1 - S) < Eps;

  WriteLn(ConvStr('Значение интеграла методом Уэддля:'));
  Write(S1, ConvStr(' с точностью '), Eps);

  ReadLn;
end.
