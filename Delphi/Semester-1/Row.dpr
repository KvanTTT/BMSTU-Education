program Row;

{  ���������, ����������� ����� ���� }

{$APPTYPE CONSOLE}

var
  N: integer;       // ����������, �� ������� ����������� ����� ����
  Step: integer;    // ��� �� N
  S: Real;          // ������� ����� ���� �� N
  NP: Real;         // ������� ���� ����
  M: Integer;       // ������������ ����� ������ ����
  Eps: Real;        // ��������
  I: Integer = 0;   // �������� �����

begin

  // ���� ������
  Write('Enter step and maximum number of cycles ');
  Read(Step, M);
  Write('Enter precision ');
  Read(Eps);
  WriteLn('':100);
  Write('':5, #218#196#196#196#194#196#196#196#196#196#196#196#196#196#196);
  WriteLn(#196#196#196#191);
  WriteLn('':5, #179, ' N ', #179, ' Sum of row  ', #179);
  Write('':5, #195#196#196#196#197#196#196#196#196#196#196#196#196#196#196);
  WriteLn(#196#196#196#180);

  // ���������� � �����
  S:= 0;
  N:= 1;
  repeat
    Inc(I);
    NP := (2*N-1)/Exp(Ln(2)*N/2); // Np = (2N-1)/(sqrt(2)^N)
    S := S + NP;
    if N div 10 < 1 then
      WriteLn(' ':5, #179, ' ', N, ' ', #179, ' ', S:7:4, '':5, #179)
    else
      WriteLn(' ':5, #179, N, ' ', #179, ' ', S:7:4, '':5, #179);
    Write(' ':5, #195#196#196#196#197#196#196#196#196#196#196#196#196#196);
    WriteLn(#196#196#196#196#180);
    N := N + Step;
    if I >= M then begin
      WriteLn(' ':100);
      WriteLn('This row dont collapse at ', M, ' cycles');
      Break;
    end;
  until NP < Eps;
  WriteLn(' ':100);
  WriteLn('Sum of row: ', S:7:4, ' at ', I, ' cycles');

  ReadLn;
  ReadLn;
end.
