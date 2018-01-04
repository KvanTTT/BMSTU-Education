unit Main;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  Menus, ImgList, ExtCtrls, Math, Grids, Algorithm;

type
  TMainForm = class(TForm)
    Image: TImage;
    MainMenu: TMainMenu;
    N1: TMenuItem;
    mnuFunction: TMenuItem;
    mnuF1: TMenuItem;
    mnuF2: TMenuItem;
    mnuF3: TMenuItem;
    mnuGrid: TMenuItem;
    procedure mnuGridClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure mnuClearClick(Sender: TObject);
    procedure ClearImage;
    procedure mnuF1Click(Sender: TObject);
    procedure mnuF2Click(Sender: TObject);
    procedure mnuF3Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  MainForm: TMainForm;

implementation

var
  F          : _function;
  fMin, fMAx : Real;
  Dphi       : Real = 5;
  Dpsi       : Real = 4;
  DEta       : Real = 4.5;
  DX         : Real = 0.2;
  Psi        : Real = 10;
  Phi        : Real = 30;
  Eta        : Real = 0;
  Ax, Bx, Ay, By : Real;
  E1, E2 : array [0 .. 2] of Real;

{$R *.DFM}

procedure get_max_values(F : _function; var fMin, fMax : Real);
  var I, J : Integer;
     SX, SY : Real;
     Value  : Real;
     X, Y   : Real;
begin        // определяем максимум и минимум функции
  with Grid do begin
    SX := (XStart - XEnd) / StepX;
    SY := (YStart - YEnd) / StepY;
    X := XStart;
    fMin := F(X, YStart);
    fMax := fMin;
    for I := 1 to StepX do begin
      Y := YStart;
      for J := 1 to StepY do begin
        Value := F(X, Y);
        if Value > fMax then fMax := Value;
        if Value < fMin then fMin := Value;
        Y := Y + SY;
      end;
      X := X + SX;
    end;
  end;
end;

procedure horizont;
var Xmin, Xmax, Ymin, Ymax : real;
begin
  get_max_values(F, fMin, fMax);

  // определяем Xmin Xmax Ymin YMax
  if E1[0] >= 0 then Xmin := Grid.XStart * E1[0]
  else Xmin := Grid.XEnd * E1[0];
  if E1[1] >= 0 then Xmin := Xmin + Grid.YStart * E1[1]
  else Xmin := Xmin + Grid.YEnd * E1[1];

  if E1[0] >= 0 then Xmax := Grid.XEnd * E1[0]
  else Xmax := Grid.XStart * E1[0];
  if E1[1] >= 0 then Xmax := Xmax + Grid.YEnd * E1[1]
  else Xmax := Xmax + Grid.YStart * E1[1];

  if E2[0] >= 0 then Ymin := Grid.XStart * E2[0]
  else Ymin := Grid.XEnd * E2[0];
  if E2[1] >= 0 then Ymin := Ymin + Grid.YStart * E2[1]
  else Ymin := Ymin + Grid.YEnd * E2[1];

  if E2[0] >= 0 then Ymax := Grid.XEnd * E2[0]
  else Ymax := Grid.XStart * E2[0];
  if E2[1] >= 0 then Ymax := Ymax + Grid.YEnd * E2[1]
  else Ymax := Ymax + Grid.YStart * E2[1];

  if E2[2] >= 0 then begin
    Ymin := Ymin + fMin * E2[2];
    Ymax := Ymax + fMax * E2[2];
  end
  else begin
    Ymin := Ymin + fMax * E2[2];
    yMax := Ymax + fMin * E2[2];
  end;

  // вычисляем коэффициенты преобразования
  Ax := 10 - MainForm.Image.Width * xMin / (xMax - xMin);
  Bx := MainForm.Image.Width  / (xMax - xMin);
  Ay := 20 - MainForm.Image.Height  * yMin / (yMax - yMin);
  By := -(MainForm.Image.Height)  / (yMax - yMin) + 50;

  MainForm.ClearImage;

  with Grid do
    draw_surface(MainForm.Image, XStart, YStart, XEnd, YEnd, F, fMin, fMax, StepX, StepY,
    DegToRad(Phi), DegToRad(Psi), DegToRad(Eta), Ax, Bx, Ay, By);
end;

procedure TMainForm.ClearImage;
begin
  with Image.Canvas do begin
    Pen.Color := $00310D3C;
    Brush.Color := 0;
    FillRect(Rect(0, 0, Image.Width, Image.Height));
  end;
end;

procedure TMainForm.mnuGridClick(Sender: TObject);
begin
  if Grid.ShowModal = mrOK then Horizont;
end;

procedure TMainForm.FormCreate(Sender: TObject);
  var cPhi, sPhi, cPsi, sPsi, sEta, cEta : Real;
begin
  MainForm.DoubleBuffered := true;
  ClearImage;
  F := F1;
  {cPhi := Cos(DegToRad(30));
  sPhi := Sin(DegToRad(30));
  sPsi := Sin(DegToRad(20));
  cPSi := Cos(DegToRad(20));
  E1[0] := cPhi; E1[1] := sPhi; E1[2] := 0;
  E2[0] := sPsi * sPhi; E2[1] :=  -sPsi * cPhi;
  E2[2] := cPsi;     }
  cPhi := Cos(DegToRad(30));
  sPhi := Sin(DegToRad(30));
  sPsi := Sin(DegToRad(20));
  cPSi := Cos(DegToRad(20));
  sEta := Sin(DegToRad(0));
  cEta := Cos(DegToRad(0));

  E1[0] := cPhi * cEta;
  E1[1] := sPhi;
  E1[2] := -cPhi*sEta;

  E2[0] := cEta*sPsi*sPhi + sEta*cPsi;
  E2[1] := -sPsi*cPhi;
  E2[2] := -sPsi*sPhi*sEta + cPsi*cEta;
end;

procedure TMainForm.mnuClearClick(Sender: TObject);
begin
  ClearImage;
end;

procedure TMainForm.mnuF1Click(Sender: TObject);
begin
  F := F1;
  mnuF1.Checked := True;;
  horizont;
end;

procedure TMainForm.mnuF2Click(Sender: TObject);
begin
  F := F2;
  mnuF2.Checked := True;
  horizont;
end;

procedure TMainForm.mnuF3Click(Sender: TObject);
begin
  F := F3;
  mnuF3.Checked := True;
  horizont;
end;

procedure TMainForm.FormShow(Sender: TObject);
begin
  horizont;
end;


procedure TMainForm.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  case Key of
    VK_LEFT  : begin
                 if Phi > (-50 + Dphi + 4) then Phi := Phi - DPhi;
               end;
    VK_RIGHT : begin
                 if Phi < (80 - Dphi - 4)  then Phi := Phi + DPhi;
               end;
    VK_UP    : begin
                 if Psi < (90 - Dpsi - 4)  then Psi := Psi + Dpsi;
               end;
    VK_DOWN  : begin
                 if Psi > (-90 + Dpsi + 4) then Psi := Psi - Dpsi;
               end;
    VK_SHIFT : begin
                 if Eta < (90 - DEta - 4)  then Eta := Eta + DEta;
    end;
    VK_CONTROL : begin
                 if Eta > (-90 + DEta + 4) then Eta := Eta - DEta;
    end;
    VK_PRIOR : begin
                 with Grid do
                 if XEnd < 5 then begin
                   XStart := XStart - DX;
                   XEnd := XEnd + DX;
                   YStart := YStart - DX;
                   YEnd := YEnd + DX;
                 end;
               end;
    VK_NEXT  : begin
                 with Grid do
                 if XEnd > 1 then begin
                   XStart := XStart + DX;
                   XEnd := XEnd - DX;
                   YStart := YStart + DX;
                   YEnd := YEnd - DX;
                 end;
               end;
  end;
  horizont;
end;

end.
