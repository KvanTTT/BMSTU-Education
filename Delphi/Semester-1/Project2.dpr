program Chars;

{ ���������, ���������� ������� �������� ��������� ����,
    ��������������� �������� ������ ��������,
    ���������� �������� ������������������ ���� �
    ��������������� ������������ ����� � �������� �����     }

{$APPTYPE CONSOLE}

var
  I, J: Char;         // ��������� ������ � 1
  St: array of char;  // �������� ������ �� 2 � 3
  K: Char;            // ���������� ��� ����� ������ 2-� � 3-�
  L: Integer;         // ����� �������� ������ ��� 2-�� � 3-��
                      //   ������ ��� ������������� �������
  N: Integer;         // ������� ��������( N <= 10) �� 2
  Sum: Integer;       // ����� ���� � 3 ������
  X: Real;            // �������� ����� � 4-�� ������
  Power: Integer;     // ������� ���������������� �����
                      //   � 4-�� ������
  NS: Integer;        // ������� ����� �� 2

begin

  // ������ 1
  WriteLn('Task 1: ');
  for I := '1' to '9' do
  begin
    WriteLn('':4);

    // 1 �������
    for J := '1' to '9' do
      if J = I then
        Write(J)
      else
        Write('0');
    Write(' ', #179, ' ');

    // 2 �������
    for J := '1' to '9' do
      if J >= I then
        Write(Chr((10-(Ord(J)-48))+48))
      else
        Write('0');
    Write(' ', #179, ' ');

    // 3 �������
    for J := Pred(I) to '9' do
      Write(J);
    for J := '0' to Chr(Ord(I)-2) do
      Write(J);

  end;

  WriteLn;
  Write('':10, #179, '':11, #179, ' 9012345678');
  WriteLn('':100);

  // ������ 2
  WriteLn('Task 2: ');
  WriteLn('Enter string: ');
  Write('0');
  for L := 1 to 7 do
    Write(' ':9, L);
  WriteLn;

  // ���� ������ �� 2-�� ������
  L := 0;
  while K <> '.' do
  begin
    Read(K);
    if (K = #10) or (K = #13) then
      Continue;
    L := L + 1;
    SetLength(St, L);
    St[L-1] := K;
  end;

  // �������������� ������
  L := 0;
  NS := 0;
  if Length(St) > 1 then
  begin
    WriteLn('':100);
    WriteLn('Formatted string: ');
    N := 0;
    Write(NS:2, ' ');
    for L := 0 to High(St)-1 do
    begin
      Write(St[L]);
      N := N + 1;
      if (St[L] = ',') then
      begin
        N := 0;
        NS := NS + 1;
        WriteLn;
        Write(NS:2, ' ');
        Continue;
      end;
      if N mod 10 = 0 then
      begin
        N := 0;
        NS := NS + 1;
        WriteLn;
        Write(NS:2, ' ');
      end;
    end;
  end
  else
    WriteLn('You dont enter the text');
  WriteLn('':100);

  // ������ 3
  WriteLn('Task 3: ');
  K := ' ';
  L := 0;
  // ���� ������������������ ��������
  while K <> '.' do
  begin
    Read(K);
    L := L + 1;
    SetLength(St, L);
    St[L-1] := K;
  end;

  // �������� ����
  Sum := 0;
  L := 0;
  if Length(St) > 1 then
  begin
    while L <= High(St)-2 do
    begin
      if (St[L] = '+') then
        Sum := Sum + Ord(St[L+1]) - Ord('0');
      if (St[L] = '-') then
        Sum := Sum - Ord(St[L+1]) + Ord('0');
      L := L + 2;
    end;
  end;
  WriteLn('Sum = ', Sum);
  WriteLn('':100);

  // ������ 4
  WriteLn('Task 4: ');
  WriteLn('Enter real value ');
  Read(X);

  // �������������� ����� � �������� �����
  if X <> 0 then
  begin
    if Abs(X) >= 1  then
    begin
      Power := 0;
      while Abs(X) >= 1 do
      begin
        X := X / 10;
        Power := Power + 1;
      end;
    end;
    if Abs(X) <= 0.01  then
    begin
      Power := 0;
      while Abs(X) <= 0.1 do
      begin
        X := X * 10;
        Power := Power - 1;
      end;
    end;
  end;

  // ����� �����
  WriteLn;
  WriteLn('Answer :');
  if X >= 0 then
    Write('+');
  Write(X:10:9, 'E');
  if Power >= 0 then
    Write('+', Power)
  else
    Write(Power);

  ReadLn;
  ReadLn;
end.
