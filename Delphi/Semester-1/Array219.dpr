program Array219;

//  ���������, �������������� ������� �������������� ������������� ���������
//    ��������� �������

{$APPTYPE CONSOLE}

var
  A: Array[1..13] of Integer;  // �������� ������
  NMax: Integer;               // ����� �������
  MinIndex: Integer;           // ����� ������������ ��������
  Min: Integer;                // ����������� �������� �������
  I: Integer;                  // �������� �����
  Temp: Integer;               // ����������, ���������� ��� ������ ����������
                               //   ����� ����� �����������

begin

  // ���� ������
  WriteLn('Enter length of array: ');
    Read(NMax);
  WriteLn('Enter elements of array: ');
  for I := 1 to NMax do
    Read(A[i]);
  ReadLn;

  // ����������
  Min := A[1];
  for I := 1 to NMax do
  begin
    if A[I] < Min then
    begin
      Min := A[I];
      MinIndex := I;
    end;
  end;

  // �����
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
