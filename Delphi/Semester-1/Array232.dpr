program Array232;

//  ���������, �������������� ������� �������������� ������������� ���������
//    ��������� �������

{$APPTYPE CONSOLE}

var
  A: Array[1..10] of Integer;  // �������� ������
  NMax: Integer;               // ����� ������������� ��������
  I: Integer;                  // �������� �����
  Neg: boolean;                // �������� ������������� ���������:
                               //   true - ����
                               //   false - ���
  S: Integer;                  // ����� ������������� ���������
  N: Integer;                  // ����� ������������� ���������
  Mean: Real;                  // ������� �������������� ������������� ���������

begin

  // ���� ������
  WriteLn('Enter length of array: ');
    Read(NMax);
  WriteLn('Enter elements of array: ');
  for I := 1 to NMax do
    Read(A[i]);
  ReadLn;

  // ����������
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

  // �����
  if Neg = true then begin
    Mean := S/N;
    WriteLn('Mean value of negate elements: ', Mean:6:4)
  end
  else
    WriteLn('Nothing negate elements');

  ReadLn;
end.
