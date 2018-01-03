unit Unit1;

// Программа, решающая квадратные уравнения

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TForm1 = class(TForm)
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Button1: TButton;
    Button2: TButton;
    lblX1: TLabel;
    lblX2: TLabel;
    Button3: TButton;
    Label4: TLabel;
    Label5: TLabel;
    Equation: TLabel;
    Label6: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    function Calculate(A, B, C: Real; var X1, X2: string): Boolean;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  A, B, C: Real;
  var S: string;
  Astr, BStr, CStr: string;
  Koefs: array[1..3] of Real;
  KoefsNames: array[1..3] of string;
  K2, K3: Boolean;
  I: Integer;

implementation

{$R *.dfm}

// Решение и вывод корней Х1 и Х2
function TForm1.Calculate(A, B, C: Real; var X1, X2: string): Boolean;
var
  D: Real;
  SqrtD: Real;
begin
  D := B*B - 4*A*C;
  if D < 0 then
  begin
    X1 := 'Нет корней в множестве действительных чисел';
    X2 := '';
    Equation.Visible := True;
    Label5.Visible := True;
    Equation.Caption := FloatToStrF(A, fffixed, 6, 2) + '*X^2 + ' +
      FloatToStrF(B, fffixed, 6, 2) + '*X + ' + FloatToStrF(C, fffixed, 6, 2) +
      ' = 0';
  end
  else
  begin
    if A <> 0 then
    begin
      SqrtD := Sqrt(D);
      X1 := 'X1 = ' + FloatToStrF((-B + SqrtD)/(2*A), ffFixed, 6, 4);
      X2 := 'X2 = ' + FloatToStrF((-B - SqrtD)/(2*A), ffFixed, 6, 4);
      Equation.Visible := True;
      Label5.Visible := True;
      Equation.Caption := FloatToStrF(A, ffFixed, 6, 2) + '*X^2 + ' +
        FloatToStrF(B, ffFixed, 6, 2) + '*X + ' + FloatToStrF(C, ffFixed, 6, 2) +
        ' = 0';
    end
    else
    begin
      if B = 0 then
      begin
        if C = 0 then
        begin
          X1 := 'X - любое вещественное';
          X2 := '';
          Equation.Visible := False;
          Label5.Visible := False;
        end
        else
        begin
          X1 := 'Нет корней';
          X2 := '';
          Equation.Visible := False;
          Label5.Visible := False;
        end;
      end
      else
      begin
        X1 := 'X = ' + FloatToStrF(-C/B, ffFixed, 6, 4) + ' - единственный корень';
        X2 := '';
        Equation.Visible := True;
        Label5.Visible := True;
        Equation.Caption := FloatToStrF(B, fffixed, 6, 2) + '*X + ';
        if C <> 0 then
          Equation.Caption := Equation.Caption + FloatToStrF(C, fffixed, 6, 2);
        Equation.Caption := Equation.Caption + ' = 0'
      end;
    end;
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  KoefsNames[1] := 'A';
  KoefsNames[2] := 'B';
  KoefsNames[3] := 'C';
  I := 1;
end;

procedure TForm1.Button1Click(Sender: TObject);
label end1;
var
  X1, X2: string;
  A, B, C: string;
begin
  A := InputBox('Ввод параметров', 'Введите параметр A:', '');
  if Trim(A) = '' then
    goto end1;
  B := InputBox('Ввод параметров', 'Введите параметр B:', '');
  if Trim(B) = '' then
    goto end1;
  C := InputBox('Ввод параметров', 'Введите параметр C:', '');
  if Trim(C) = '' then
    goto end1;

  Edit1.Text := A;
  Edit2.Text := B;
  Edit3.Text := C;
  Calculate(StrToFloat(A), StrToFloat(B), StrToFloat(C), X1, X2);
  lblX1.Caption := X1;
  lblX2.Caption := X2;
  Exit;

  end1:
  Edit1.Text := '';
  Edit2.Text := '';
  Edit3.Text := '';
  lblX1.Caption := 'X1 = ';
  lblX2.Caption := 'X2 = ';
  Label5.Caption := '';
  Equation.Caption := ''
end;

procedure TForm1.Button2Click(Sender: TObject);
var
  X1, X2: string;
  S: string;
begin

  S := '';
  if I = 1 then
  begin
    Edit1.Clear;
    Edit2.Clear;
    Edit3.Clear;
  end;
  while I <= Length(Koefs) do
  begin
    if InputQuery('Ввод параметров', 'Введите параметр ' + KoefsNames[I] + ':', S) then
    begin
    if Trim(S) = '' then
    begin
      Exit;
    end;
      Koefs[I] := StrToFloat(S);
      case I of
        1: Edit1.Text := S;
        2: Edit2.Text := S;
        3: Edit3.Text := S;
      end;
      S := '';
    end
    else
      Break;
    Inc(I);
  end;

  if I-1 = Length(Koefs) then
  begin
    Calculate(Koefs[1], Koefs[2], Koefs[3], X1, X2);
    Edit1.Text := FloatToStr(Koefs[1]);
    Edit2.Text := FloatToStr(Koefs[2]);
    Edit3.Text := FloatToStr(Koefs[3]);
    lblX1.Caption := X1;
    lblX2.Caption := X2;
    I := 1;
    Exit;
  end;

  //MessageDlg('Вы прервали процедуру ввода', mtWarning, [mbOK], 0);
  Edit1.Text := '';
  Edit2.Text := '';
  Edit3.Text := '';
  lblX1.Caption := 'X1 = ';
  lblX2.Caption := 'X2 = ';
  Label5.Caption := '';
  Equation.Caption := '';
  I := 1;
end;

procedure TForm1.Button3Click(Sender: TObject);
var
  X1, X2: string;
begin
  if (Edit1.Text = '') or (Edit2.Text = '')
  or (Edit3.Text = '') then
    Exit;
  Calculate(StrToFloat(Edit1.Text), StrToFloat(Edit2.Text),
    StrToFloat(Edit3.Text), X1, X2);
  lblX1.Caption := X1;
  lblX2.Caption := X2;
end;

end.
