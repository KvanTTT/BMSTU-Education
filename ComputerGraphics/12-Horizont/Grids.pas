unit Grids;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls;

type
  TGrid = class(TForm)
    GroupBox1: TGroupBox;
    EditSX: TEdit;
    Label3: TLabel;
    GroupBox2: TGroupBox;
    Label6: TLabel;
    EditSY: TEdit;
    btnOk: TButton;
    procedure btnOkClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormShow(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    XStart, YStart,
    XEnd,   YEnd  : Real;
    StepX,  StepY : Integer;
  end;

var
  Grid: TGrid;

implementation

uses Main;

{$R *.DFM}

procedure TGrid.btnOkClick(Sender: TObject);
begin
  btnOk.ModalResult := mrNone;
  // проверка корректности введенных значений
  try
    StepX := StrToInt(EditSX.Text);
    if (StepX < 1) or (StepX > 70) then raise ERangeError.Create('');
  except
    on EConvertError do begin
      MessageDlg('Число линий по оси Х введено некорректно', mtError, [mbOK], 0);
      EditSX.SetFocus;
      Exit;
    end;
    on ERangeError do begin
      MessageDlg('Число линий по оси Х одолжно лежать в пределах 0 < N <= 128', mtError, [mbOK], 0);
      EditSX.SetFocus;
      Exit;
    end;
  end;
  try
    StepY := StrToInt(EditSY.Text);
    if (StepY < 1) or (StepY > 70) then raise ERangeError.Create('');
  except
    on EConvertError do begin
      MessageDlg('Число линий по оси Y введено некорректно', mtError, [mbOK], 0);
      EditSY.SetFocus;
      Exit;
    end;
    on ERangeError do begin
      MessageDlg('Число линий по оси Y одолжно лежать в пределах 0 < N <= 128', mtError, [mbOK], 0);
      EditSY.SetFocus;
      Exit;
    end;
  end;
  btnOk.ModalResult := mrOK;
end;

procedure TGrid.FormCreate(Sender: TObject);
begin
  btnOK.ModalResult := mrNone;
  XStart :=-2;
  XEnd := 2;
  StepX := 40;
  YStart := -2;
  YEnd := 2;
  StepY := 40;
end;

procedure TGrid.FormShow(Sender: TObject);
begin
  Grid.Left := MainForm.Image.Left + MainForm.Left + 5;
  Grid.Top := MainForm.Image.Top + MainForm.Top + 40;
end;

end.
