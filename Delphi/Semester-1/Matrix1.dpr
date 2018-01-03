program Matrix1;

{ ���������� ����� ���� � �������� ���������,
    �������� �������, ������� � �������� � ����}

{$APPTYPE CONSOLE}

var
  M: array[1..15, 1..20] of Real; // �������� �������
  X: array of Real;               // ������, � ������� ����� ������������
                                  //   �������� �� �������, ������� ��� ����� ����
  Row, Column: Integer;           // ���������� ����� � �������� � �������
  XMax: Real;                     // ������������ �������� �������� � ������� X
  XMaxIndex: Integer;             // ����� ������������� �������� ������� �
  T: Real;                        // ����� ����
  N: Integer;                     // ���������� �������� �����
  A: Real;                        // �������� �����
  Eps: Real;                      // ��������, � ������� �������������� ����� ����
  Cur: Real;                      // N-��� ���� ����
  SqrA: Real;                     // �*�
  I, J: Integer;                  // ��������� �����
  Temp: Real;                     // ����������, ����������� ��� ������
                                  //   ���������� ����� �����������

begin

  // ���� ��������� �, �������
  WriteLn('Enter const A and precision: ');
  ReadLn(A, Eps);
  WriteLn('Enter matrix size Row x Column :  ');
  ReadLn(Row, Column);
  WriteLn('Enter matrix');
  for I := 1 to Row do
    for J := 1 to Column do
      Read(M[I, J]);
  ReadLn;

  // ������� ����� ����
  Cur := A;
  SqrA := A*A;
  T := 0;
  N := 0;
  WriteLn('N = 0', ' Cur = ', Cur:6:4);
  while Cur > Eps do
  begin
    N := N + 1;
    Cur := Cur*SqrA/((N+1)*(N+2));
    WriteLn('N = ', N, ' Cur = ', Cur:6:4);
    T := T + Cur;
  end;


  // ������������ ������� �
  for I := 1 to Row do
    for J := 1 to Column do
      if T < M[I, J] then
      begin
        SetLength(X, Length(X) + 1);
        X[High(X)] := M[I, J];
      end;

  // ���������� ������������� �������� � ������� � � ��� �����
  XMax := X[0];
  for I := 0 to High(X) do
  begin
    if X[I] > XMax then
    begin
      XMax := X[I];
      XMaxIndex := I;
    end;
  end;

  // ����� �������� ����� ������ � ������������ ���������
  Temp := X[0];
  X[0] := X[XMaxIndex];
  X[XMaxIndex] := Temp;

  // ����� ����� ����, ������� � �������
  WriteLn('');
  WriteLn('Sum of row: ', T:6:4, ' with precision: ', Eps:6);

  WriteLn('');
  WriteLn('Matrix M:');
  for I := 1 to Row do
  begin
    for J := 1 to Column do
      Write(M[I, J]:7:4, '  ');
      WriteLn('');
  end;

  WriteLn('');
  WriteLn('Vector X: ');
  for I := 0 to High(X) do
  if (I+1) mod 5 = 0 then
    WriteLn('X[', I, '] = ', X[I]:7:4,  ', ')
  else
    Write('X[', I, '] = ', X[I]:7:4,  ', ');

  ReadLn;
end.

// ����� ���������
