program Queue;

{$APPTYPE CONSOLE}

//  Программа, позволяющая добавлять, удалять,
//   показывать элементы очереди.

const
  NN = 100;             //Максимальный размер массива

var
  Ochered: array[1..NN] of Integer; // Сама очередь
  L: Integer;                       // Длина очереди
  FirstElem, LastElem: Integer;     // Индексы первого и последнего элемента
  D: Integer = 0;                   // Номер действия меню
  N: Integer;                       // Количество оставшихся элементов


// Увеличение индекса в массива
procedure IncIndexOfArray(var I: Integer; Length: Integer); inline;
begin
  if I = Length then
    I := 1
  else
    Inc(I);
end;

// Уменьшение индекса в массива
procedure DecIndexOfArray(var I: Integer; Length: Integer); inline;
begin
  if I = 1 then
    I := Length
  else
    Dec(I);
end;

// Добавление элемента в массив
procedure AddElement;
var
  Value: Integer;
begin
  if LastElem = FirstElem then
    if N = 0 then
    begin
      WriteLn('Queue is full. It''s impossible to add the element');
      Exit;
    end;

  WriteLn('Enter element:');
  ReadLn(Value);
  Ochered[LastElem] := Value;
  IncIndexOfArray(LastElem, L);
  Dec(N);
end;

// Удаление элемента из массива
procedure DeleteElement;
begin
  if N = L then
  begin
    WriteLn('Queue is already empty, impossible to delete elemets');
    Exit;
  end;

  IncIndexOfArray(FirstElem, L);
  Inc(N);
  WriteLn('Element succsesfully deleted');
  if FirstElem = LastElem then
    WriteLn('Queue is empty');
end;

// Вывод и удаление элемента из массива
procedure GetElement;
begin
  if N = L then
  begin
    WriteLn('Queue is empty');
    Exit;
  end;
  WriteLn('Last element of queue = ', Ochered[FirstElem]);
  DeleteElement;
end;

// Показ всех элементов массива
procedure ShowElements;
var
  I, J: Integer;
begin
  if N = L then
  begin
    WriteLn('Queue is empty');
    Exit;
  end;

  WriteLn('Elements of queue:');
  if FirstElem < LastElem then
    for I := FirstElem to LastElem - 1 do
      WriteLn(I, '.  ', Ochered[I])
  else
  begin
    I := FirstElem;
    J := 0;
    while I <= L do
    begin
      Inc(J);
      WriteLn(J, '.  ', Ochered[I]);
      Inc(I);
    end;
    for I := 1 to LastElem - 1 do
    begin
      Inc(J);
      WriteLn(J, '.  ', Ochered[I])
    end;
  end;
end;

// Очистка массива
procedure ClearArray;
begin
  N := L;
  FirstElem := 1;
  LastElem := 1;
  WriteLn('Array succsesfully cleared');
end;

function Menu: Integer;
begin
  WriteLn('Choose action:');
  WriteLn('   0 - Add element');
  WriteLn('   1 - Delete element');
  WriteLn('   2 - Get element');
  WriteLn('   3 - Show elements');
  WriteLn('   4 - Clear array');
  WriteLn('   5 - Exit');
  ReadLn(Result);
end;

begin
  WriteLn('Enter length of queue');
  ReadLn(L);
  N := L;

  FirstElem := 1;
  LastElem := 1;

  while D = 0 do
  begin
    case Menu of
      0: AddElement;
      1: DeleteElement;
      2: GetElement;
      3: ShowElements;
      4: ClearArray;
      5: Exit;
    end;
  end;

end.
