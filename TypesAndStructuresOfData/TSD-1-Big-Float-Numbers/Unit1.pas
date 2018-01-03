unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, BigFloatNumbers, StdCtrls;

type
  TForm1 = class(TForm)
    Edit1: TEdit;
    Button1: TButton;
    Edit2: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Edit3: TEdit;
    Label4: TLabel;
    Label5: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
    procedure Edit2KeyPress(Sender: TObject; var Key: Char);
    procedure FormCreate(Sender: TObject);
    procedure Edit2Change(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  F1, F2: TBigFloat;
  C1: Integer = 0;
  C2: Integer = 0;

implementation

{$R *.dfm}


procedure TForm1.Button1Click(Sender: TObject);
var
  ErrorStr: string;
  Result: TBigFloat;
  I: Integer;
  S: string;
begin
  F1 := EnterBigFloat(Edit1.Text, ErrorStr);
  if ErrorStr <> '' then
  begin
    ShowMessage(ErrorStr);
    Exit;
  end;
  F2 := EnterBigFloat(Edit2.Text, ErrorStr);
  if ErrorStr <> '' then
  begin
    ShowMessage(ErrorStr);
    Exit;
  end;

  Result := Divide(F1, F2, ErrorStr);
  if ErrorStr <> '' then
  begin
    Edit3.Text := ErrorStr;
    Exit;
  end
  else
    Edit3.Text := OutputBigFloat(Result);

end;

procedure TForm1.Edit1Change(Sender: TObject);
begin
  Label4.Caption := IntToStr(Length(Edit1.Text));
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['0'..'9', '.', ',' , 'e', 'E', '+', '-', #8]) then
    Key := #0
  else
  if (Key = #8) then
  begin
    if (Length(Edit1.Text) = 0) then
      Exit;
    Dec(C1);
    Label4.Caption := IntToStr(C1);
  end
  else
  begin
    C1 := Length(Edit1.Text);
    Inc(C1);
    Label4.Caption := IntToStr(C1);
  end;
end;

procedure TForm1.Edit2Change(Sender: TObject);
begin
  Label5.Caption := IntToStr(Length(Edit2.Text));
end;

procedure TForm1.Edit2KeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['0'..'9', '.', ',' , 'e', 'E', '+', '-', #8]) then
    Key := #0
  else
  if (Key = #8) then
  begin
    if (Length(Edit2.Text) = 0) then
      Exit;
    C1 := Length(Edit2.Text);
    Dec(C1);
    Label5.Caption := IntToStr(C1);
  end
  else
  begin
    C1 := Length(Edit2.Text);
    Inc(C1);
    Label5.Caption := IntToStr(C1);
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Form1.Left := 300;
  Form1.Top := 200;
end;

end.
