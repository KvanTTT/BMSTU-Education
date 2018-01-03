program Integrals;

{����}

{$APPTYPE CONSOLE}

type
  Func = function(X: Real): Real;

function RightRectangles(A, B, H: Real; F: Func): Real;
// ���������� ��������� ������� ������ ���������������
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
// ���������� ��������� ������� ������
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

function F(X: Real): Real; // �������� �������
begin
  Result := 1/Exp(X*Ln(X));
end;

function ConvStr(S: string): string; // �������, �����������
                                     //  �������� ������� �������
  var
    Code: Integer;
    I: Integer;
  begin
    Result:='';
    for I :=1 to Length(S) do
    begin
      Code := Ord(S[I]);
      Case S[I] of
        '�'..'�': Dec(Code,64);
        '�'..'�': Dec(Code,16);
        '�':Inc(Code,57);
      end;
      Result := Result + Chr(Code);
    end;
  end;

var
  A, B: Real;       // ��������� � �������� ������� ��������������
  N1, N2: Integer;  // ���������� ���������
  I: Integer;       // �������� �����
  Eps: Real;        // �������� ��������
  S1RR, S2RR, S1U, S2U: Real; // �������� �������� ��� ������ �������
                              //  � ��������� ���������
  H, H1, H2: Real;  // �������� ����� ��� ������ ���������
  S, S1: Real;      // ������� ��� ���������� � �������� ���������

begin
  // ���� ������
  WriteLn(ConvStr('������� ��������� � �������� ��������'));
  ReadLn(A, B);
  {WriteLn(ConvStr('������� ���� �������� ���������, ������� 6-��'));
  ReadLn(N1, N2);       }
  WriteLn(ConvStr('������� ��������'));
  ReadLn(Eps);

  {// ���������� ����� �������������� � ����� ��������
  H1 := (B - A) / N1;
  H2 := (B - A) / N2;
  S1RR := RightRectangles(A, B, H1, F);
  S2RR := RightRectangles(A, B, H2, F);
  S1U  := Ueddls(A, B, H1, F);
  S2U  := Ueddls(A, B, H2, F);

  // ����� ������� � ����������� ���������
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

  // ���������� ���������� � �������� ���������
  {H := (B - A) / 2;
  S1 := RightRectangles(A, B, H, F);
  repeat
    S := S1;
    H := H / 2;
    S1 := RightRectangles(A, B, H, F);
  until Abs(S1 - S) < Eps;

  WriteLn(ConvStr('�������� ��������� ������� ������ ���������������: '));
  Write(S1, ConvStr(' � ��������� '), Eps:6:4);
  WriteLn;   }

  H := (B - A) / 6;
  S1 := Ueddls(A, B, H, F);
  repeat
    S := S1;
    H := H / 6;
    S1 := Ueddls(A, B, H, F);
  until Abs(S1 - S) < Eps;

  WriteLn(ConvStr('�������� ��������� ������� ������:'));
  Write(S1, ConvStr(' � ��������� '), Eps);

  ReadLn;
end.
