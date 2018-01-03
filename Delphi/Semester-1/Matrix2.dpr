program Matrix2;

{$APPTYPE CONSOLE}

{ ������������ ��������� �������� �������� ������� �� ������������,
    � ����� ����� �������� � �������, ��������� �� 7 � ��������� �� � ������  }

var
  X: array[1..10] of array[1..8] of Integer; //  �������� �������
  A: array of Integer;                       //  ������, � ������� ���������
                                             //    �������� �� �������, ��������� �� 7
  Row, Column: Integer;                      //  ���������� ����� � �������� � ������� �
  I, J: Integer;                             //  ��������� �����
  Min: Real;                                 //  ����������� ������� � ������ ������� ������� �
  MinIndex: Integer;                         //  ����� ������������ �������� � ������ ������� ������� �
  M: Integer;                                //  ��������� ������������ ���� ��������� �
                                             //    ������� ������� �� ������������ ��������

begin

  //  ���� �������
  WriteLn('Enter matrix size Row x Column :  ');
  ReadLn(Row, Column);
  WriteLn('Enter matrix X');
  for I := 1 to Row do
    for J := 1 to Column do
      Read(X[I, J]);
  ReadLn;

  //  ����� �������� �������
  WriteLn(' ');
  WriteLn('Matrix X:');
  for I := 1 to Row do
  begin
    for J := 1 to Column do
      Write(X[I, J]:4);
      WriteLn('');
  end;

  //  ������������ ���� ��������� � ������� ������� �� ������������ ��������
  for I := 1 to Column do
  begin
    Min := X[1, I];
    M := X[1, I];
    for J := 1 to Row do
      if X[J, I] < Min then
      begin
        Min := X[J, I];
        MinIndex := J;
      end;
    for J := 2 to MinIndex - 1 do
        M := M * X[J, I];
    X[MinIndex, I] := M;
  end;

  // ����� � ������� ��������, ��������� �� 7 � �� ��������� � ������
  for I := 1 to Row do
    for J := 1 to Column do
      if X[I, J] mod 7 = 0 then
      begin
        SetLength(A, Length(A) + 1);
        A[High(A)] := X[I, J];
      end;

  // ����� ��������������� �������
  WriteLn('');
  WriteLn('Transformed matrix:');
  for I := 1 to Row do
  begin
    for J := 1 to Column do
      Write(X[I, J]:4);
      WriteLn('');
  end;

  // ����� ������� �
  WriteLn('');
  WriteLn('Vector A: ');
  if Length(A) <> 0 then
  begin
    for I := 0 to High(A) do
      if (I+1) mod 5 = 0 then
        WriteLn('A[', I, '] = ', A[I], ', ')
      else
        Write('A[', I, '] = ', A[I], ', ');
  end
  else
    WriteLn('There are 0 elements, which divided by 7');

  ReadLn;
  ReadLn;
end.
