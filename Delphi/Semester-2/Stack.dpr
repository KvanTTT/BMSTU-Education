program Stack;

{$APPTYPE CONSOLE}

uses
  SysUtils;

type
  PElemOfStack = ^TElemOfStack;
  TElemOfStack = record
    Value: Integer;
    NextElem: PElemOfStack;
  end;

var
  FirstElem: PElemOfStack;

procedure AddElement;
var
  P: PElemOfStack;
  Value: Integer;
begin
  WriteLn('Enter value');
  ReadLn(Value);
  if FirstElem = nil then
  begin
    New(FirstElem);
    FirstElem^.Value := Value;
    FirstElem^.NextElem := nil;
  end
  else
  begin
    New(P);
    P^.NextElem := FirstElem;
    P^.Value := Value;
    FirstElem := P;
  end;
end;

procedure DeleteElement;
var
  P: PElemOfStack;
begin
  if FirstElem = nil then
    WriteLn('Stack is already empty. Impossible to delete element')
  else
  begin
    P := FirstElem;
    FirstElem := P^.NextElem;
    Dispose(P);
    WriteLn('Element is succsesfully deleted');
  end;
end;

procedure ClearStack;
var
  P: PElemOfStack;
begin
  if FirstElem = nil then
    WriteLn('Stack is already empty. Impossible to clear stack')
  else
  begin
    P := FirstElem;
    repeat
      FirstElem := P;
      Dispose(P);
      P := FirstElem^.NextElem;
    until P = nil;
    Dispose(FirstElem^.NextElem);
    FirstElem := nil;
  end;
end;

procedure GetElement;
begin
  if FirstElem = nil then
    WriteLn('Stack is empty')
  else
  begin
    WriteLn('First element of stack is ', FirstElem^.Value);
    DeleteElement;
  end;
end;

procedure ShowStack;
var
  P: PElemOfStack;
  I: Integer;
begin
  if FirstElem = nil then
  begin
    WriteLn('Stack is empty');
    Exit;
  end;
  WriteLn('Stack:');
  P := FirstElem;
  I := 0;
  while P <> nil do
  begin
    Inc(I);
    WriteLn(I, '. ', P^.Value);
    P := P^.NextElem;
  end;
end;

function Menu: Byte;
begin
  WriteLn('');
  WriteLn('0 - Add Element');
  WriteLn('1 - Delete Element');
  WriteLn('2 - Get Element');
  WriteLn('3 - Show Stack');
  WriteLn('4 - Clear Stack');
  WriteLn('5 - Exit');
  WriteLn('');
  WriteLn('Chose action:');
  Read(Result);
  WriteLn('');
end;

begin

  FirstElem := nil;
  while 1 <> 0 do
  case Menu of
    0: AddElement;
    1: DeleteElement;
    2: GetElement;
    3: ShowStack;
    4: ClearStack;
    5: Exit;
  end;

end.
