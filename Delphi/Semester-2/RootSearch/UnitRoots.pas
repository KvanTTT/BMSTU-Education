unit UnitRoots;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Grids;

type
  TForm1 = class(TForm)
    edtLb: TEdit;
    edtRb: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    ComboBox1: TComboBox;
    strg: TStringGrid;
    Label3: TLabel;
    edtMaxIt: TEdit;
    Label4: TLabel;
    edtPrec: TEdit;
    Button1: TButton;
    Label5: TLabel;
    Label6: TLabel;
    edtStep: TEdit;
    GroupBox1: TGroupBox;
    Label7: TLabel;
    Label8: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormResize(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

const
  Functions: array[0..3] of string = ('X^2-4',
                                      'sin(X)',
                                      'X^X-2',
                                      '2^X-4*X');

var
  Form1: TForm1;

implementation

{$R *.dfm}

function F(Value: Extended; Number: Byte): Extended;
begin
  case Number of
    0: Result := Sqr(Value)-4;
    1: Result := Sin(Value);
    2: Result := Exp(Value*Ln(Value)) - 2;
    3: Result := Exp(Value*Ln(2)) - 4*Value;
  end;
end;

function D2F(Value: Extended; Number: Byte): Extended;
begin
  case Number of
    0: Result := 2;
    1: Result := -Sin(Value);
    2: Result := Exp(Value*Ln(Value))*
        (Sqr(Ln(Value)+1) + 1/Value);
    3: Result := Sqr(Ln(2))*Exp(Value*Ln(2));
  end;

end;

procedure SearchRootSecant(Number: Byte; A, B, Precision: Extended;
  MaxIt: Integer; var Error: Byte; var X: Extended; var Iterations: Word);
var
  T: Extended;
  I: Integer;
  C: Extended;
begin
  if F(A, Number) * F(B, Number) > 0 then
  begin
    Error := 2;
    Exit;
  end;
  if F(A, Number) = 0 then
  begin
    X := A;
    Error := 1;
    Exit;
  end;

  if F(B, Number) * D2F(B, Number) < 0 then
  begin
    T := B;
    B := A;
    A := T;
  end;

  I := 1;
  while (Abs(F(A, Number)-F(B, Number))>Precision) and (I < MaxIt) do
  begin
        I := I+1;
        C := B-F(B, Number)*(B-A)/(F(B, Number)-F(A, Number));
        A := B;
        B := C;
  end;

  if I <= MaxIt then
    Error := 0
  else
    Error := 3;
  Iterations := I;

  X := B;

end;

procedure TForm1.Button1Click(Sender: TObject);
var
  X1, X2: Extended;
  Step: Extended;
  Error: Byte;
  I, J: Word;
  X: Extended;
  Iterations: Word;
  S: string;

begin
  strg.RowCount := 1;
  X1 := StrToFloat(edtLb.Text);
  Step := StrToFloat(edtStep.Text);
  I := 0;
  while X1 < StrToFloat(edtRb.Text) do
  begin
    X2 := X1 + Step;
    SearchRootSecant(ComboBox1.ItemIndex, X1,
      X2, StrToFloat(EdtPrec.text), StrToInt(EdtMaxIt.Text),
      Error, X, Iterations);
    if (Error = 1) or (Error = 0) or (Error = 3) then
    begin
      S := FloatToStrF(X, ffFixed, 9, 7);
      for J := 1 to strg.RowCount - 1 do
        if S = Strg.Cells[1, J] then
        begin
          Error := 4;
          Break;
        end;
      if Error = 4 then
      begin
        X1 := X2;
        Continue;
      end;
      if X > StrToFloat(edtRb.Text) then
      begin
        X1 := X2;
        Continue;
      end;
      if X2 > StrToFloat(edtRb.Text) then
        X2 := StrToFloat(edtRb.Text);
      strg.RowCount := strg.RowCount + 1;
      Inc(I);
      strg.Cells[0, I] := FloatToStrF(X1, ffFixed, 4, 2)
        + '  -  ' + FloatToStrF(X2, ffFixed, 4, 2);
      strg.Cells[1, I] := FloatToStrF(X, ffFixed, 9, 7);
      S := FloatToStrF(F(X, ComboBox1.ItemIndex), ffExponent, 2, 2);
      if S[1] <> '-' then
        Insert('+', S, 1);
      Delete(S, 3, 2);
      S[2] := '1';
      strg.Cells[2, I] := S;
      strg.Cells[3, I] := IntToStr(Iterations);
      strg.Cells[4, I] := IntToStr(Error);
    end;
      X1 := X2;
  end;

  if F(X2, ComboBox1.ItemIndex) = 0 then
  begin
    X := X2;
    Error := 1;
    strg.RowCount := strg.RowCount + 1;
    Inc(I);
    strg.Cells[0, I] := FloatToStrF(X1, ffFixed, 4, 2)
        + '  -  ' + FloatToStrF(X2, ffFixed, 4, 2);
    strg.Cells[1, I] := FloatToStrF(X, ffFixed, 9, 7);
    strg.Cells[2, I] := FloatToStrF(F(X, ComboBox1.ItemIndex), ffExponent, 2, 2);
    strg.Cells[3, I] := IntToStr(Iterations);
    strg.Cells[4, I] := IntToStr(Error);
  end;
  strg.FixedRows := 1;

end;

procedure TForm1.FormCreate(Sender: TObject);
var
  I: Integer;
begin
  for I := 0 to High(Functions) do
    ComboBox1.Items.Add(Functions[I]);
  ComboBox1.ItemIndex := 0;
  strg.Cells[0, 0] := 'Отрезок';
  strg.Cells[1, 0] := 'Корень';
  strg.Cells[2, 0] := 'Значение функции';
  strg.Cells[3, 0] := 'Число итераций';
  strg.Cells[4, 0] := 'Код ошибки';

end;

procedure TForm1.FormResize(Sender: TObject);
begin
  strg.Width := Form1.Width - 25;
  strg.Height := Form1.Height - strg.Top - 45;

end;

end.
