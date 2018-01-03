unit UnitMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, UnitBigNumbers;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    Button6: TButton;
    Button7: TButton;
    Button8: TButton;
    Button9: TButton;
    Button10: TButton;
    Edit1: TEdit;
    Button11: TButton;
    Button12: TButton;
    Button13: TButton;
    Button14: TButton;
    Button15: TButton;
    Button16: TButton;
    Button17: TButton;
    Button18: TButton;
    Button19: TButton;
    Button20: TButton;
    Button21: TButton;
    Button22: TButton;
    Button23: TButton;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button22Click(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
    procedure FormKeyPress(Sender: TObject; var Key: Char);

  private
    { Private declarations }
  public
    procedure Operation(O: Char); 
    procedure MakeOperation(Operation: Integer; var Answer, Value: Extended); 
  end;

var
  Form1: TForm1;
  Answer: Extended;
  Op: Integer;
  Value: Extended;
  Point: Boolean;
  NewNumber: Boolean;
  Change: Boolean;
  FirstNumber: Boolean;
  Equal: Boolean;
  Error: Boolean;


implementation

{$R *.dfm}

procedure TForm1.MakeOperation(Operation: Integer; var Answer, Value: Extended);
begin
  case Operation of
    -1: Answer := Value;
    0: Answer := Answer + Value;
    1: Answer := Answer - Value;
    2: Answer := Answer * Value;
    3:
    begin
      if Value = 0 then
      begin
        Error := True;
        Edit1.Text := 'Деление на ноль запрещено';
        Edit1.ReadOnly := True;
        Exit;
      end;
      Answer := Answer / Value;
    end;
    4:
    begin
      if Value <= 0 then
      begin
        Error := True;
        Edit1.Text := 'Недопустимый аргумент функции';
        Edit1.ReadOnly := True;
        Exit;
      end;
      Answer := Ln(Value);
    end;
    5:
    begin
      if Value < 0 then
      begin
        Error := True;
        Edit1.Text := 'Недопустимый аргумент функции';
        Edit1.ReadOnly := True;
        Exit;
      end;
      Answer := Sqrt(Value);
    end;
  end;
end;

procedure TForm1.Operation(O: Char);
var
  A: Integer;
begin
  case O of
    '+': A := 0;
    '-': A := 1;
    '*': A := 2;
    '/': A := 3;
    'l': A := 4;
    's': A := 5;
  end;
  if A = 4 then
  begin
      Op := 4;
      Value := StrToFloat(Edit1.Text);
      MakeOperation(Op, Answer, Value);
      if Error then
        Exit;
      Edit1.Text := FloatToStr(Answer);
      Exit;
  end;
  if A = 5 then
  begin
      Op := 5;
      Value := StrToFloat(Edit1.Text);
      MakeOperation(Op, Answer, Value);
      if Error then
        Exit;
      Edit1.Text := FloatToStr(Answer);
      Exit;
  end;

  if FirstNumber then
  begin
    Answer := StrToFloat(Edit1.Text);
    FirstNumber := False;
    NewNumber := True;
  end
  else
  begin
    if Change then
    begin
      Value := StrToFloat(Edit1.Text);
      MakeOperation(Op, Answer, Value);
      if Error then
        Exit;
      Edit1.Text := FloatToStr(Answer);
      NewNumber := True;
      Change := False;
    end;
  end;
  Op := A;
end;


procedure TForm1.Button1Click(Sender: TObject);

var
  C: Char;
  S: string;
  I: Integer;
begin

  if (Sender is TButton) then
  begin
    C := (Sender as TButton).Caption[1];
    if (C <> 'C') and (Error) then
      Exit;
    case C of
      '0'..'9':
      begin
        if (Edit1.Text = '0') or (NewNumber) then
        begin
          Edit1.Text := '';
        end;
        Change := True;
        Edit1.Text := Edit1.Text + C;
        NewNumber := False;
        Equal := False;
      end;

      '-':
        Operation('-');

      '+':
      begin
        if (Sender as TButton).Caption[2] = '/' then
        begin
          S := Edit1.Text;
          if S = '0' then
            Exit;

            if (S[1] = '-') then
              Delete(S, 1, 1)
            else
              Insert('-', S, 1);

          Edit1.Text := S;
        end
        else
          Operation('+');
      end;

      '*':
        Operation('*');

      '/':
        Operation('/');

      ',':
      begin
        for I := 1 to Length(Edit1.Text) do
          if Edit1.Text[I] = ',' then
            Exit;

        Edit1.Text := Edit1.Text + C;
      end;

      'l':
        Operation('l');

      's':
        Operation('s');

      '=':
      begin
        if not Equal then
        begin
          Value := StrToFloat(Edit1.Text);
          Equal := True;
          NewNumber := True;
          FirstNumber := True;
        end;
        MakeOperation(Op, Answer, Value);
        if Error then
          Exit;
        Edit1.Text := FloatToStr(Answer);
      end;

      'C':
      begin
        Answer := 0;
        Op := -1;
        Edit1.Text := '0';
        FirstNumber := True;
        NewNumber := True;
        Equal := False;
        Value := 0;
        Error := False;
        Edit1.ReadOnly := False;
      end;

      '<':
      begin
        if Edit1.Text <> '0' then
        begin
          S := Edit1.Text;
          if (Length(S) = 1) then
            S[1] := '0'
          else
          begin
            SetLength(S, Length(S)-1);
            if (Length(S) = 1) and (S = '-')
              then S := '0'
          end;
          Edit1.Text := S;
        end;
      end;

    end;
  end;
end;


procedure TForm1.Button22Click(Sender: TObject);
begin
  Form2.Show;
end;

procedure TForm1.Edit1Change(Sender: TObject);
begin
  {if Edit1.Text = '' then
    Edit1.Text := '0';     }
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Button11.Click;
end;

procedure TForm1.FormKeyPress(Sender: TObject; var Key: Char);
var
  C: Char;
  S: string;
  I: Integer;
begin
  if Key = '.' then
    Key := ',';
  if not (Key in ['0'..'9', ',', #8, '+', '-', '*',
            '*', 'l', 's', 'C', 'c', '=']) then
    Key := #0;
  if Key = #8 then
    Key := '<';
  

  C := Key;
    if ((C <> 'C') and (Error)) or (Key = #0) then
      Exit;
    case C of
      '0'..'9':
      begin
        if (Edit1.Text = '0') or (NewNumber) then
        begin
          Edit1.Text := '';
        end;
        Change := True;
        Edit1.Text := Edit1.Text + C;
        NewNumber := False;
        Equal := False;
      end;

      '-':
        Operation('-');

      '+':
      begin
        Operation('+');
      end;

      '*':
        Operation('*');

      '/':
        Operation('/');

      ',':
      begin
        for I := 1 to Length(Edit1.Text) do
          if Edit1.Text[I] = ',' then
            Exit;

        Edit1.Text := Edit1.Text + C;
      end;

      'l':
        Operation('l');

      's':
        Operation('s');

      '=':
      begin
        if not Equal then
        begin
          Value := StrToFloat(Edit1.Text);
          Equal := True;
          NewNumber := True;
          FirstNumber := True;
        end;
        MakeOperation(Op, Answer, Value);
        if Error then
          Exit;
        Edit1.Text := FloatToStr(Answer);
      end;

      'C', 'c':
      begin
        Answer := 0;
        Op := -1;
        Edit1.Text := '0';
        FirstNumber := True;
        NewNumber := True;
        Equal := False;
        Value := 0;
        Error := False;
        Edit1.ReadOnly := False;
      end;

      '<':
      begin
        if Edit1.Text <> '0' then
        begin
          S := Edit1.Text;
          if (Length(S) = 1) then
            S[1] := '0'
          else
          begin
            SetLength(S, Length(S)-1);
            if (Length(S) = 1) and (S = '-')
              then S := '0'
          end;
          Edit1.Text := S;
        end;
        Key := #0;
      end;

    end;
end;

end.
