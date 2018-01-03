program Ex1;

{$APPTYPE CONSOLE}

uses
  SysUtils;

const 
  NN = 5;

type
  Zap = record
    Name: string[10];   { фамилия }
    Age:  Integer;      { возраст }
    Height: Byte;       {  рост   }
    Weight: Real;       {  вес    }
  end;
  Stud = array [1..NN] of Zap;
{-----------------------------------------------------------}

procedure Input_zap(var Student:Zap);
begin
  with Student do
  begin
    WriteLn('Input name, age, height and weight student');
    ReadLn(Name);
    ReadLn(Age, Height, Weight);
  end;
end; // процедуры Input_zap 
{-----------------------------------------------------------}

procedure Output_zap(Student:Zap);
begin
  with Student do
    WriteLn(Name, Age:5, Height:5, Weight:7:1);
end; // процедуры Output_zap 
{-----------------------------------------------------------}

procedure Sort_zap(var Student:Stud; N:integer);

var
  I, J, NMin: Integer;
  R: Zap;
  Min: Integer;
begin
  for I := 1 to N-1 do
  begin
    Min := Student[I].Age; 
    NMin := I;
    for J := I+1 to N do
      if Min>Student[J].Age then
      begin
        Min := Student[J].Age;
        NMin := J;
      end;
    R := Student[I];
    Student[I] := Student[NMin];
    Student[NMin] := R;
  end;
end; // процедуры Sort_zap 
{-----------------------------------------------------------}
var
  I, N:    Integer;
  Student: Stud;       // массив записей о студентах 

begin
  Write('Input number student: ');
  ReadLn(N);
  for I := 1 to N do
    Input_zap(Student[I]);

  Sort_zap(Student, N);

  WriteLn('result');
  for I := 1 to N do
    Output_zap(Student[I]);
  ReadLn;
end.
