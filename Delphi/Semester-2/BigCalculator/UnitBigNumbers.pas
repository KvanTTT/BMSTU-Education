unit UnitBigNumbers;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, UMathServices;

type
  TForm2 = class(TForm)
    Memo1: TMemo;
    StaticText1: TStaticText;
    Label1: TLabel;
    LabeledEdit1: TLabeledEdit;
    Button1: TButton;
    Button2: TButton;
    procedure Memo1KeyPress(Sender: TObject; var Key: Char);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form2: TForm2;
  Precision: string;

implementation

{$R *.dfm}

procedure TForm2.Button1Click(Sender: TObject);
var
  I, J, K: Integer;
  TempS: string;
  Number, Number1: string;
begin
  TempS := Memo1.Lines.Text;

  I := 1;
  while I < Length(TempS) do
  begin

    if TempS[I] = ',' then
      TempS[I] := '.';

    if TempS[I] = ' ' then
    begin
      J := I;
      while TempS[J] = ' ' do
        Inc(J);
      Delete(TempS, I, J-I);
    end;
    Inc(I);
  end;
  TempS := TempS + ' ';

  I := 1;
  J := 1;
  Number := '';
  Number1 := '';
  while I <= Length(TempS) do
  begin
    if TempS[I] in ['*', '/', '+', '-'] then
      J := I+1
    else
      if TempS[I] = '!' then
      begin
        Number := Copy(TempS, J, I-J);
        Delete(TempS, J, I-J+1);
        Insert(ulFact(Number), TempS, J);
      end;

    Inc(I);
  end;

  I := 1;
  J := 1;
  Number := '';
  Number1 := '';
  while I <= Length(TempS) do
  begin
    if TempS[I] in ['*', '/', '+', '-'] then
      J := I+1
    else
      if TempS[I] = '^' then
      begin
        Number := Copy(TempS, J, I-J);
        K := I+1;
        while (TempS[K] in ['0'..'9']) do
          Inc(K);
        Number1 := Copy(TempS, I+1, K-I-1);
        Delete(TempS, J, K-J);
        Insert(ulPower(Number, Number1), TempS, J);
      end;
    Inc(I);
  end;

  I := 1;
  J := 1;
  Number := '';
  Number1 := '';
  while I <= Length(TempS) do
  begin
    if TempS[I] in ['+', '-'] then
      J := I+1
    else
      if TempS[I] = '*' then
      begin
        Number := Copy(TempS, J, I-J);
        K := I+1;
        while (TempS[K] in ['0'..'9']) do
          Inc(K);
        Number1 := Copy(TempS, I+1, K-I-1);
        Delete(TempS, J, K-J);
        Insert(ulMPL(Number, Number1), TempS, J);
      end
    else
      if TempS[I] = '/' then
      begin
        Precision := InputBox('Большие числа', 'Введите точность (для деления): ', Precision);
        Number := Copy(TempS, J, I-J);
        K := I+1;
        while (TempS[K] in ['0'..'9']) do
          Inc(K);
        Number1 := Copy(TempS, I+1, K-I-1);
        Delete(TempS, J, K-J);
        Insert(ulDiv(Number, Number1, StrToInt(Precision)), TempS, J);
      end;
    Inc(I);
  end;

  I := 1;
  J := 1;
  Number := '';
  Number1 := '';
  while I <= Length(TempS) do
  begin
      if TempS[I] in ['+','-'] then
      begin
        Number := Copy(TempS, J, I-J);
        K := I+1;
        while (TempS[K] in ['0'..'9']) do
          Inc(K);
        Number1 := Copy(TempS, I, K-I);
        Delete(TempS, J, K-J);
        Insert(ulSum(Number, Number1), TempS, J);
      end
      else
      {if TempS[I] = '-' then
      begin
        Number := Copy(TempS, J, I-J);
        K := I+1;
        while not (TempS[K] in ['+', '-', ' ']) do
          Inc(K);
        Number1 := Copy(TempS, I+1, K-I-1);
        Delete(TempS, J, K-J);
        Insert(ulSub(Number, Number1), TempS, J);
      end; }
    Inc(I);
  end;

  LabeledEdit1.Text := TempS;
  {while I < Length(TempS) do
  begin
    while Str[I] <> ' ' do
    begin

    end;
  end;   }           

end;

procedure TForm2.Button2Click(Sender: TObject);
begin
  LabeledEdit1.SetFocus;
  LabeledEdit1.SelectAll;
end;

procedure TForm2.FormCreate(Sender: TObject);
begin
  Precision := '100';
end;

procedure TForm2.Memo1KeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['0'..'9', '+', '-', '*', '/', '^', '!', ' ', '.', ',', #8]) then
    Key := #0;
end;

end.
