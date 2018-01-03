unit BigFloatNumbers;

interface

uses SysUtils;

type
  TBigFloat = record
    Sign: Boolean;
    Mantissa: array of Byte;
    K: Integer;
  end;

  TBigInt = record
    Digits: array of Byte;
  end;

function EnterBigFloat(Str: string; var ErrorStr: string): TBigFloat;
function Divide(const BigFloat1, BigFloat2: TBigFloat; var ErrorStr: string): TBigFloat;
function OutputBigFloat(const BigFloat: TBigFloat): string;

implementation

function EnterBigFloat(Str: string; var ErrorStr: string): TBigFloat;
var
  D: Integer;
  I: Integer;
  K: Integer;
  IndE, IndDot: Integer;
  DoubleE, DoubleDot: Boolean;
  L: Integer;
  IndLastDigit, IndFirstDigit: Integer;
  H: Integer;
begin
  ErrorStr := '';
  IndE := 0;
  DoubleE := False;
  IndDot := 0;
  DoubleDot := False;
  L := Length(Str);

  if L = 0 then
  begin
    ErrorStr := 'Enter number!';
    Exit;
  end;

  D := 1;
  if Str[1] = '-' then
  begin
    Result.Sign := False;
    D := 2;
  end
  else
  begin
    Result.Sign := True;
    if Str[1] = '+' then
      D := 2;
  end;

  I := D;
  Str := Str + ' ';
  while I <= L do
  begin
    if Str[I] in ['.', ','] then
    begin
      if DoubleDot then
      begin
        ErrorStr := 'Number include only one "." (form: ±m.n E ±K)';
        Exit;
      end;
      if (I = D) or (DoubleE) or (Str[I+1] in ['e', 'E']) then
      begin
        ErrorStr := 'Wrong position of "." (form: ±m.n E ±K)';
        Exit;
      end;
      IndDot := I;
      DoubleDot := True;
      Inc(I);
      Continue;
    end;

    if Str[I] in ['e', 'E'] then
    begin
      if not (Str[I-1] in ['0'..'9']) then
      begin
        ErrorStr := 'Required number before "E"';
        Exit;
      end;
      if DoubleE then
      begin
        ErrorStr := 'Number include only one "E" (form: ±m.n E ±K)';
        Exit;
      end;
      if not (Str[I+1] in ['+', '-', '0'..'9']) then
      begin
        ErrorStr := 'Miss number after "E" (form: ±m.n E ±K)';
        Exit;
      end;
      IndE := I;
      DoubleE := True;
      if Str[I+1] in ['+', '-'] then
        Inc(I);
      Inc(I);
      Continue;
    end;

    if not (Str[I] in ['0'..'9']) then
    begin
      ErrorStr := 'Incorrect symbol (not digit, sign or "E")';
      Exit;
    end;
    Inc(I);
  end;

  SetLength(Str, L);
  if Str[L] in ['.', ','] then
  begin
    ErrorStr := 'Misses digits after dot (form: ±m.n E ±K)';
    Exit;
  end;

  if IndE = 0 then
  begin
    L := L + 2;
    SetLength(Str, L);
    Str[L-1] := 'e';
    Str[L] := '0';
    IndE := L-1;
  end;
  if IndDot = 0 then
  begin
    Insert('.0', Str, IndE);
    L := L + 2;
    IndDot := IndE;
    IndE := IndE + 2;
  end;

  IndFirstDigit := D;
  for I := D to IndDot-1 do
    if Str[I] = '0' then
      Inc(IndFirstDigit)
    else
      Break;

  IndLastDigit := IndE-1;
  for I := IndE-1 downto IndDot+1 do
    if Str[I] = '0' then
      Dec(IndLastDigit)
    else
      Break;

  {if IndFirstDigit = IndDot then
    Dec(IndFirstDigit);}
  {if IndLastDigit = IndDot then
    Inc(IndLastDigit);      }
  SetLength(Result.Mantissa, IndLastDigit-IndFirstDigit);
  for I := IndFirstDigit to IndDot-1 do
    Result.Mantissa[I-IndFirstDigit] := StrToInt(Str[I]);

  if IndLastDigit <> IndDot then
  begin
  for I := IndDot+1 to IndLastDigit do
    Result.Mantissa[I-IndDot-1+IndDot-IndFirstDigit] := StrToInt(Str[I]);
  end
  else
    //Inc(IndLastDigit);

  if Length(Result.Mantissa) > 30 then
  begin
    ErrorStr := 'Length of mantissa <= 30';
    Exit;
  end;

  if TryStrToInt(Copy(Str, IndE+1, L-IndE), Result.K) then
  begin
    if Abs(Result.K) <= 99999 then
    begin
      Result.K := Result.K - (IndLastDigit - IndDot);
      if Length(Result.Mantissa) = 0 then
      begin
        SetLength(Result.Mantissa, 1);
        Result.Mantissa[0] := 0;
      end;
    end
    else
      ErrorStr := 'range of order: -99999..99999';
  end
  else
    ErrorStr := 'range of order: -99999..99999';

  {H := High(Result.Mantissa);
  IndLastDigit := H;
  for I := H downto 0 do
    if Result.Mantissa[I] <> 0 then
      Break
    else
      IndLastDigit := I-1;
  if IndLastDigit <> H then
  begin
    //Dec(IndLastDigit);
    Result.K := Result.K + H - IndLastDigit;
    if IndLastDigit < 0 then
    begin
      SetLength(Result.Mantissa, 1);
      Result.Mantissa[0] := 0;
    end
    else
      SetLength(Result.Mantissa, IndLastDigit+1);
  end;   }
end;



function Divide(const BigFloat1, BigFloat2: TBigFloat; var ErrorStr: string): TBigFloat;

procedure SimplifyNumber(var BigFloat: TBigInt); 
var
  H: Integer;
  I: Integer;
  K: Integer;
begin
  I := 0;
  H := High(BigFloat.Digits);
  while I <= H do
  begin
    if BigFloat.Digits[I] <> 0 then
      Break;
    Inc(I);
  end;
  if I <> 0 then
  begin
    for K := I to H do
      BigFloat.Digits[K-I] := BigFloat.Digits[K];
    SetLength(BigFloat.Digits, H-I+1);
  end;
end;

function Greater(const BigFloat1, BigFloat2: TBigInt): Boolean;
var
  H1, H2: Integer;
  I: Integer;
begin
  H1 := High(BigFloat1.Digits);
  H2 := High(BigFloat2.Digits);
  if H1 > H2 then
  begin
    Result := True;
    Exit;
  end
  else
  if H1 < H2 then
  begin
    Result := False;
    Exit;
  end;

  for I := 0 to H1 do
    if BigFloat1.Digits[I] > BigFloat2.Digits[I] then
    begin
      Result := True;
      Exit;
    end
    else
    if BigFloat1.Digits[I] < BigFloat2.Digits[I] then
    begin
      Result := False;
      Exit;
    end;

  Result := False;
end;

function GreaterOrEqual(const BigFloat1, BigFloat2: TBigInt): Boolean;
var
  H1, H2: Integer;
  I: Integer;
begin
  H1 := High(BigFloat1.Digits);
  H2 := High(BigFloat2.Digits);
  if H1 > H2 then
  begin
    Result := True;
    Exit;
  end
  else
  if H1 < H2 then
  begin
    Result := False;
    Exit;
  end;

  I := 0;
  while I <= H1 do
  begin
    if BigFloat1.Digits[I] > BigFloat2.Digits[I] then
    begin
      Result := True;
      Exit;
    end
    else
    if BigFloat1.Digits[I] < BigFloat2.Digits[I] then
    begin
      Result := False;
      Exit;
    end;
    Inc(I);
  end;

  Result := True
end;

procedure Dec(var T: TBigInt; const D: TBigInt);
var
  I: Integer;
  A: Integer;
  H: Integer;
begin
  A := 0;
  H := High(T.Digits) - High(D.Digits);
  for I := High(T.Digits) downto H do
  begin
    A := T.Digits[I] - D.Digits[I-H] - A;
    if A < 0 then
    begin
      T.Digits[I] := 10 + A;
      A := 1;
    end
    else
    begin
      T.Digits[I] := A;
      A := 0;
    end;
  end;
  for I := H-1 downto 0 do
  begin
    A := T.Digits[I] - A;
    if A < 0 then
    begin
      T.Digits[I] := 10 + A;
      A := 1;
    end
    else
    begin
      T.Digits[I] := A;
      Break;
    end;
  end;
  SimplifyNumber(T);
end;

function SubSub(var T: TBigInt; const S: TBigInt): Integer;
begin
  Result := 0;
  while GreaterOrEqual(T, S) do
  begin
    Dec(T, S);
    Inc(Result);
  end;
end;

procedure IncBigFloat(var T: TBigFloat);
var
  I: Integer;
begin
  for I := 29 downto 0 do
  begin
    T.Mantissa[I] := T.Mantissa[I] + 1;
    if T.Mantissa[I] = 10 then
    begin
      T.Mantissa[I] := 0;
      Continue;
    end
    else
      Break;
  end;
  if T.Mantissa[0] = 10 then
  begin
    T.Mantissa[0] := 1;
    T.K := T.K + 1;
  end;

end;

var
  T: TBigInt;
  I: Integer;
  D: Integer;
  J: Integer;
  K: Integer;
  LT: Integer;
  Remainder: TBigInt;
  Divisior: TBigInt;
  Divisible: TBigInt;
  H1: Integer;
  IndDot: Integer;
  I1: Integer;
  B: Boolean;
begin
  ErrorStr := '';
  if (Length(BigFloat2.Mantissa) = 1) and
     (BigFloat2.Mantissa[0] = 0) then
  begin
    ErrorStr := 'Dividing by zero!';
    Exit;
  end;
  if (Length(BigFloat1.Mantissa) = 1) and
    (BigFloat1.Mantissa[0] = 0) then
  begin
    SetLength(Result.Mantissa, 1);
    Result.Mantissa[0] := 0;
    Result.K := 0;
    Exit;
  end;
  if (Length(BigFloat2.Mantissa) = 1) and
    (BigFloat2.Mantissa[0] = 1) and (BigFloat2.K = 1) then
  begin
    Result := BigFloat1;
    Result.K := Result.K + Length(BigFloat1.Mantissa);
    Result.Sign := not(BigFloat1.Sign xor BigFloat2.Sign);
    if Result.K > 99999 then
      ErrorStr := 'Top overflow'
    else if Result.K < -99999 then
      ErrorStr := 'Bottom overflow';
    Exit;
  end;

  Result.K := BigFloat1.K - BigFloat2.K;
  Result.Sign := not(BigFloat1.Sign xor BigFloat2.Sign);

  SetLength(Result.Mantissa, 31);
  SetLength(Divisior.Digits, Length(BigFloat2.Mantissa));
  for I := 0 to High(BigFloat2.Mantissa) do
    Divisior.Digits[I] := BigFloat2.Mantissa[I];

  SetLength(Divisible.Digits, Length(BigFloat1.Mantissa));
  for I := 0 to High(BigFloat1.Mantissa) do
    Divisible.Digits[I] := BigFloat1.Mantissa[I];

  SetLength(Divisible.Digits, 31 + Length(BigFloat2.Mantissa));
  {for I := 0 to High(BigFloat1.Mantissa) do
    Divisible.Digits[I] := BigFloat1.Mantissa[I];  }
  Result.K := Result.K -
    (31 + Length(BigFloat2.Mantissa) - Length(BigFloat1.Mantissa)) + 31;

  B := True;
  H1 := High(BigFloat1.Mantissa);
  D := 0;
  J := 0;
  SetLength(T.Digits, Length(Divisior.Digits));
  for I := 0 to High(Divisior.Digits) do
    T.Digits[I] := Divisible.Digits[I];
  D := Length(Divisior.Digits);
  if Greater(Divisior, T) then
  begin
      SetLength(T.Digits, Length(T.Digits)+1);
      T.Digits[High(T.Digits)] :=  Divisible.Digits[D];
      Inc(D);
  end;

  while J <= 30 do
  begin

    {LT := Length(T.Digits);
    SetLength(T.Digits, Length(Divisior.Digits));

    for I := D to Length(Divisior.Digits) - LT + D - 1 do
      T.Digits[I + LT - D] := Divisible.Digits[I];
    J := J + Length(Divisior.Digits) - LT - 1;
    D := D + Length(Divisior.Digits) - LT;    }


    {if (D >= High(BigFloat1.Mantissa)+1) and
    (J > Length(BigFloat1.Mantissa) - Length(BigFloat2.Mantissa)) and (B = True) then
    begin
        Result.K := Result.K + 1;
        B := False;
    end
    else
      B := False;   }


    if Greater(Divisior, T) then
    begin
      LT := Length(T.Digits);
      SetLength(T.Digits, LT+1);
      T.Digits[LT] :=  Divisible.Digits[D];
      //SimplifyNumber(T);
      Inc(D);
    end;

    while Greater(Divisior, T) do
    begin
      if D = Length(BigFloat1.Mantissa) then
      begin
        if J + High(Divisior.Digits) = High(BigFloat1.Mantissa) then
          Inc(Result.K);
      end;
      if J = 30 then
        Break;
      LT := Length(T.Digits);
      SetLength(T.Digits, LT+1);
      T.Digits[LT] :=  Divisible.Digits[D];
      SimplifyNumber(T);
      Inc(D);
      Inc(J);
    end;

    if D = Length(BigFloat1.Mantissa) then
    begin
      if J + High(Divisior.Digits) = High(BigFloat1.Mantissa) then
        Inc(Result.K);
    end;
    Result.Mantissa[J] := SubSub(T, Divisior);
    Inc(J);
  end;



  if Result.Mantissa[J-1] >= 5 then
    IncBigFloat(Result);
  SetLength(Result.Mantissa, 30);

  if Result.K > 99999 then
    ErrorStr := 'Top overflow'
  else if Result.K < -99999 then
    ErrorStr := 'Bottom overflow';


end;

function OutputBigFloat(const BigFloat: TBigFloat): string;
var
  I: Integer;
  IndEnd: Integer;
  J: Integer;
  T: string;
  K: Integer;
begin
  K := 0;
  SetLength(Result, 40);
  if BigFloat.Sign then
    Result[1] := '+'
  else
    Result[1] := '-';
  Result[2] := '0';
  Result[3] := '.';
  IndEnd := High(BigFloat.Mantissa);
  while BigFloat.Mantissa[IndEnd] = 0 do
  begin
    Dec(IndEnd);
    //Inc(K);
  end;
  I := 4;
  for J := 0 to IndEnd do
  begin
    Result[I] := Chr(BigFloat.Mantissa[J] + Ord('0'));
    Inc(I);
  end;
  Result[I] := 'E';
  Inc(I);
  if BigFloat.K >= 0 then
  begin
    Result[I] := '+';
    Inc(I);
  end;
  T := IntToStr(BigFloat.K + K);
  for J := 1 to Length(T) do
  begin
    Result[I] := T[J];
    Inc(I);
  end;
  SetLength(Result, I-1);
end;

end.
