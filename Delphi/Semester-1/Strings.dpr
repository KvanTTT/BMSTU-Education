program Strings;

{ ���������, ����������� � ������ ����� ������ ������ ��������� ������ �����,
  � ����� ���������� ��� �����, ����� ���������� }


{$APPTYPE CONSOLE}

var
  St, St1: string[210];  //  �������� ������
  Word: string;     //  ��������� ����� � ������
  C: string;        //  ���������� ������ ������ �����
  I, J: Integer;    //  ��������� �����
  N: Integer;       //  ������� ������� ��������� ���������� �����
                    //    � ��������������� ������
  Count: Integer;   //  ���������� ������������� ���� � ������ �����

begin

  // ���� ������
  WriteLn('Enter string: ');
  ReadLn(St);

  // �������� �� �������� ������ ������������� ����
  I := 1;
  while I < Length(St) do
  begin
    if St[I] <> ' ' then
    begin
      C := St[I];
      J := I + 1;
      Count := 0;
      while St[J] = C do
      begin
        J := J + 1;
        Count := Count + 1;
      end;
      if Count > 0 then
        Delete(St, I + 1, Count);
    end;
    I := I + 1;
  end;

  // ����� ���������� �����
  for I := Length(St) downto 1 do
  begin
    if (St[I] <> ' ') and (St[I] <> '.') and (St[I] <> '!') and (St[I] <> '?') then
      Break;
  end;
  J := I;
  while St[J] <> ' ' do
  begin
    J := J - 1;
  end;
  Word := Copy(St, J + 1, I - J);

  // �������� ���� ����, ������ � ���������
  repeat
    N := Pos(Word, St);
    St1 := '';
    St1 := Copy(St1, 1, N-1);
    if (St[N+Length(Word)+1] = ' ') or (St[N+Length(Word)+1] = '.') then
      Delete(St, N, Length(Word)+1)
    else
    begin
      I := N;
      while St[I] <> ' ' do
        I := I + 1;
      St1 := Copy(St, N, I - N);
      Delete(St, N, I);
    end;
  until N = 0;
  St1[Length(St1)] := '.';

  // ����� ��������������� ������
  WriteLn;
  WriteLn('Formatted string: ');
  WriteLn(St1);

  ReadLn;
end.
