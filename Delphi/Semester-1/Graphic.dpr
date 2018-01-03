program Graphic;

// ��������� ��� ������� ���� ������� � ��������� ���������, ��������
//   �������� � �����, � ����� ���������� ����� �� �������

{$APPTYPE CONSOLE}

var
  X: Real;                    // �������� �������� �������
  Y1, Y2, Y3: Real;           // �������� �������, �������� ��������
  Y3Max, Y3Min: Real;         // ������������ � ����������� �������� ������� Y3
  X3Min: Real;                // �������� ���������, ��� ������� �����������
                              //   ����������� �������� ������� Y3
  Y3I: Integer;               // ������� �������� ������� Y3 � ���������� ������
  Start, Finish: Real;
  I, J: Integer;              // ��������� ������
  StepY, StepX: Real;         // ���� �� �x � �y ��������������
  Mark: Real;                 // ���������� �������
  C, C1: Real;                // ����������, ����������� ��� ����������
                              //   ��� ��

begin

  // ���� ������
  WriteLn('Enter starting, step and finishing value X');
  ReadLn(Start, StepX, Finish);
  WriteLn('Y1 = 2^X - 4X');
  WriteLn('Y2 = X^3 - 3*X^2 + 1');
  WriteLn('Y3 =(Y1+Y2)/10');
  WriteLn('':100);
  Write('':5, #218#196#196#196#196#196#196#196#196#196#196#194#196#196#196);
  Write(#196#196#196#196#196#196#196#194#196#196#196#196#196#196#196#196);
  WriteLn(#196#196#194#196#196#196#196#196#196#196#196#196#196#191);

  Write('':5, #179, '    X     ', #179, '    Y1    ', #179, '    Y2    ');
  WriteLn(#179, '    Y3    ', #179);
  Write('':5, #195#196#196#196#196#196#196#196#196#196#196#197#196#196#196);
  Write(#196#196#196#196#196#196#196#197#196#196#196#196#196#196#196#196);
  WriteLn(#196#196#197#196#196#196#196#196#196#196#196#196#196#180);

  // ������ ������� �������� �������
  Y3Max := -1e10;
  Y3Min := 1e10;
  X := Start;
  for I := 0 to Trunc((Finish-Start)/StepX) + 1 do
  begin
    Y1:= Exp(X*Ln(2)) - 4*X;  // Y1:= 2^X - 4X
    Y2:= X*X*(X-3) + 1;       // Y2:= X^3 - 3*X^2 + 1
    Y3:= (Y1+Y2)/10;
    if Y3 > Y3Max then
      Y3Max := Y3;
    if Y3 < Y3Min then
    begin
      Y3Min := Y3;
      X3Min := X;
    end;
    WriteLn('':5, #179, X:6, #179, Y1:6, #179, Y2:6, #179, Y3:6, #179);
    Write('':5, #195#196#196#196#196#196#196#196#196#196#196#197#196#196);
    Write(#196#196#196#196#196#196#196#196#197#196#196#196#196#196#196);
    WriteLn(#196#196#196#196#197#196#196#196#196#196#196#196#196#196#196#180);
    X:= X + StepX;
  end;
  WriteLn('':100);

  // ���������� ������������ �������� ������� �������
  WriteLn('Minimal value of Y3 and his argument: ');
  WriteLn('Y3 = ', Y3Min:6, ' with X = ', X3Min:6);
  WriteLn('':100);


  WriteLn('Graphic of Y3 function:');
  // ����� �������� ������� �� ��� Y
  Write('':5);
  StepY := (Y3Max - Y3Min)/8;
  for I := 1 to 9 do
  begin
    Mark := Y3Min + StepY*I;
    Write(Mark:5:2, '  ');
  end;

  // ���������� ��� Y
  WriteLn('');
  Write('  ':7, #218);
  for I := 1 to 56 do
    if I mod 7 = 0 then
      Write(#197)
  else
    Write(#196);
  Write(#26);
  WriteLn('');

  // ���������� ��� �, � ����� ������ �������
  X := Start;
  StepY := (Y3Max-Y3Min)/56;
  for I := 0 to Trunc((Finish-Start)/StepX) + 1 do
  begin
    Write(' ', X:6:2, #197);
    Y1:= Exp(X*Ln(2)) - 4*X;  // Y1:= 2^X - 4X
    Y2:= X*X*(X-3) + 1;       // Y2:= X^3 - 3*X^2 + 1
    Y3:= (Y1+Y2)/10;
    Y3I:= Trunc((Y3-Y3Min)*56/(Y3Max-Y3Min));
    C := Y3Min;
    for J := 0 to 56 do
    begin
      C1 := C;
      C := C + StepY;
      if C * C1 <= 0 then
        Write(#179)
      else
      if J = Y3I then
        Write(#15)
      else
      Write(' ');
    end;
    WriteLn('');
    X:= X + StepX;
  end;
  WriteLn('  ':7, #25);

  ReadLn;
end.

// ����� ��������� ���������� �������
