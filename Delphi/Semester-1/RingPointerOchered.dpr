program RingPointerOchered;

{$APPTYPE CONSOLE}

type
  PElementOfOchered = ^TElementOfOchered;
  TElementOfOchered = record
    Value: Integer;
    PrevElement, NextElement: PElementOfOchered;
  end;

var
  L: Integer;
  PFirst, PLast: PElementOfOchered;
  I: Integer;
  D: Integer = 0;

procedure AddElement;
var
  P: PElementOfOchered;
  Value: Integer;
begin
  if I = L then
  begin
    WriteLn('Queue is full. It''s impossible to add the element');
    Exit;
  end;
  WriteLn('Enter element:');
  ReadLn(Value);
  New(P);
  if PFirst <> nil then
  begin
    PLast^.NextElement := P;
    P^.PrevElement := PLast;
    P^.NextElement := PFirst;
    PFirst^.PrevElement := P;
    P^.Value := Value;
    PLast := P;
  end
  else
  begin
    P^.NextElement := P;
    P^.PrevElement := P;
    P^.Value := Value;
    PFirst := P;
    PLast := P;
  end;
  Inc(I);
end;

procedure DeleteElement;
var
  P: PElementOfOchered;
begin
  if I = 0 then
  begin
    WriteLn('Queue is empty, impossible to delete elemets');
    Exit;
  end;
  if PFirst = PLast then
  begin
    Dispose(PFirst);
    I := 0;
    WriteLn('Queue is empty');
  end
  else
  begin
    P := PFirst;
    PFirst := PFirst^.NextElement;
    PFirst^.PrevElement := PLast;
    PLast^.NextElement := PFirst;
    Dispose(P);
    Dec(I);
  end;

  WriteLn('Element succsesfully deleted');
end;

procedure GetElement;
begin
  if PFirst = nil then
    WriteLn('Queue is empty')
  else
  begin
    WriteLn('Last element of queue = ', PFirst^.Value);
    DeleteElement;
  end;
end;

procedure ShowElements;
var
  P: PElementOfOchered;
  J: Integer;
begin
  if I = 0 then
  begin
    WriteLn('Queue is empty');
    Exit;
  end;
  WriteLn('Elements of Queue:');
  J := 0;
  P := PFirst;
  repeat
    Inc(J);
    WriteLn(J, '.  ', P^.Value);
    P := P^.NextElement;
  until P = PFirst;
end;

procedure ClearQueue;
var
  P: PElementOfOchered;
begin
  P := PFirst;
  repeat
    P := P^.NextElement;
    Dispose(P);
  until P = PFirst;
  Dispose(PFirst);
  I := 0;
end;

function Menu: Integer;
begin
  WriteLn('Chose action:');
  WriteLn('   0 - Add element');
  WriteLn('   1 - Delete element');
  WriteLn('   2 - Get element');
  WriteLn('   3 - Show Queue');
  WriteLn('   4 - Clear Queue');
  WriteLn('   5 - Exit');
  ReadLn(Result);
end;

begin
  WriteLn('Enter length of queue');
  ReadLn(L);
  I := 0;

  while D = 0 do
  begin
    case Menu of
    0: AddElement;
    1: DeleteElement;
    2: GetElement;
    3: ShowElements;
    4: ClearQueue;
    5: Exit;
    end;
  end;

end.
