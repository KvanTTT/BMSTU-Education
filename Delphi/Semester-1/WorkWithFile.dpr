program WorkWithFile;

{$APPTYPE CONSOLE}

procedure DeleteRepeatedSymbol(Pos: Integer; var Str: string);
var
  StartPos, EndPos: Integer;
begin
  StartPos := Pos;
  EndPos := Pos;
  while Str[StartPos] = Str[Pos] do
    StartPos := StartPos - 1;
  StartPos := StartPos + 1;
  while Str[EndPos] = Str[Pos] do
    EndPos := EndPos + 1;
  EndPos := EndPos - 1;
  Delete(Str, StartPos, EndPos-StartPos);
end;

procedure DeleteUnnecessarySpaces(Pos: Integer; var Str: string);
var
  StartPos, EndPos: Integer;
begin
  StartPos := Pos;
  EndPos := Pos;
  while Str[StartPos] = Str[Pos] do
    StartPos := StartPos - 1;
  StartPos := StartPos + 1;
  while Str[EndPos] = Str[Pos] do
    EndPos := EndPos + 1;
  EndPos := EndPos - 1;
  Delete(Str, StartPos, EndPos-StartPos);
  if Str[StartPos+1] in [',', '.', '!', '?', ';', ':'] then
  Delete(Str, StartPos, 1);
end;

var
  F: Text;
  I: Integer;
  Text: string;
  OneString: string;

begin
  AssignFile(F, 'Input.txt');
  Reset(F);

  Text := '';
  while not SeekEOF(F) do
  begin
    ReadLn(F, OneString);
    Text := Text + OneString + #13#10;
  end;

  WriteLn('Input text:');
  WriteLn(Text);

  WriteLn;
  WriteLn('Output text:');
  I := 0;
  while I < Length(Text) do
  begin
    I := I + 1;
    if Text[I] = ' ' then
      DeleteUnnecessarySpaces(I, Text);
  end;

  AssignFile(F, 'Output.txt');
  Rewrite(F);
  for I := 1 to Length(Text) do
    Write(F, Text[I]);
  CloseFile(F);

  WriteLn(Text);
  ReadLn;
end.
