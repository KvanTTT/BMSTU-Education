unit UnitMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, Math, Buttons, StdCtrls, Spin;

type
  TPolygon = array of TPoint;

const
  EL_POINTS = 31;
  EL_STEP = 2*PI/(EL_POINTS+1);

type
  TExtendedPoint = record
    X, Y: Extended;
  end;
  TCircle = record
    Center: TExtendedPoint;
    Dir: TExtendedPoint;
  end;
  TEllipse = record
    Center: TExtendedPoint;
    Dir1: TExtendedPoint;
    Dir2: TExtendedPoint;
    Points: array[0..EL_POINTS] of TExtendedPoint;
  end;

type TMatrix = array[0..2, 0..2] of Extended;

type THouse = record
  Center: TExtendedPoint;
  Wall: array[0..3] of TExtendedPoint;
  Roof: array[0..2] of TExtendedPoint;
  RoofWindow: TEllipse;
  WindowArc1: TEllipse;
  Window1: array[0..5] of TExtendedPoint;
  Window2: TEllipse;
  Window2Frames: array[0..3] of TExtendedPoint;
  Frames: array[0..7] of TExtendedPoint;
end;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Image: TImage;
    Timer1: TTimer;
    ColorDialog1: TColorDialog;
    Panel2: TPanel;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Label2: TLabel;
    edtDX: TEdit;
    edtDY: TEdit;
    GroupBox2: TGroupBox;
    Label3: TLabel;
    Label4: TLabel;
    Label7: TLabel;
    edtROX: TEdit;
    edtROY: TEdit;
    edtAng: TEdit;
    GroupBox3: TGroupBox;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    edtSx: TEdit;
    edtSy: TEdit;
    edtSox: TEdit;
    edtSoy: TEdit;
    BitBtn2: TBitBtn;
    cbTranslate: TCheckBox;
    cbRotate: TCheckBox;
    cbScale: TCheckBox;
    BitBtn1: TBitBtn;
    Button1: TButton;
    GroupBox4: TGroupBox;
    Label5: TLabel;
    Label6: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
    procedure Button1Click(Sender: TObject);
    procedure BitBtn2Click(Sender: TObject);
    procedure cbTranslateClick(Sender: TObject);
    procedure cbRotateClick(Sender: TObject);
    procedure cbScaleClick(Sender: TObject);
    procedure ImageMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure ImageMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure ImageMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure edtROXKeyPress(Sender: TObject; var Key: Char);
    procedure edtROXExit(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure edtSoxKeyPress(Sender: TObject; var Key: Char);
    procedure edtSoxExit(Sender: TObject);
  private
    procedure DrawHouse(House: THouse);
    procedure TranslationDefault;
    procedure RotationDefault;
    procedure ScalingDefault;
    procedure InitHouse(var House: THouse);
    procedure DrawEllipse(const Ellipse: TEllipse; const Image: TImage);
    procedure DrawPoint(var Point: TExtendedPoint; Color: TColor);
  public
    { Public declarations }
  end;

function ExtendedPoint(X, Y: Extended): TExtendedPoint; inline;

function Distance(P1, P2: TExtendedPoint): Extended; inline;
function Norm(V: TExtendedPoint): Extended; inline;
function DistanceSqr(P1, P2: TExtendedPoint): Extended; inline;
function ParallelogramSquare(P1, P2, P3: TExtendedPoint): Extended;
function Perimeter(P1, P2, P3: TExtendedPoint): Extended;
procedure NormalizeAngle(var Angle: Extended);

procedure MakeIdentity(var M: TMatrix);
procedure MakeZero(var M: TMatrix);
procedure SetPosition(var M: TMatrix; OldPos, NewPos: TExtendedPoint);

procedure AddTranslation(var M: TMatrix; DX, DY: Extended);
procedure AddRotation(var M: TMatrix; Angle: Extended; Point: TExtendedPoint);
procedure AddScaling(var M: TMatrix; SX, SY: Extended; Point: TExtendedPoint);

function Determinant(const M: TMatrix): Extended; inline;
//function Inverse(const M: TMatrix): TMatrix;
procedure Inverse(var M: TMatrix);

procedure PointTransform(var P: TExtendedPoint; const M: TMatrix); inline;
procedure VectorTransform(var V: TExtendedPoint; const M: TMatrix); inline;

function VectorCos(const V1, V2: TExtendedPoint): Extended; inline;
function VectorCot(const V1, V2: TExtendedPoint): Extended; inline;

const
  PI2: Extended = 6.283185307179586476925286766559;
  NumberOfInverseActions: Integer = 20;
  InvalidValue: string = 'Invalid value in hightlight field';
  InvalidTransform: string = 'Operation is not execute - dont affine transform';
  ErrorColor: TColor = $00D2B5FB;

var
  Form1: TForm1;
  House: THouse;
  M: TMatrix;
  V: TExtendedPoint;
  Angle: Extended;
  RotAngle: Extended;
  SinA, CosA: Extended;
  S: Extended;
  HouseAngle: Extended;
  InverseActions: Array[0..39] of TMatrix;
  LBDown, RBDown: Boolean;
  RPoint, SPoint: TExtendedPoint;
  XTS, YTS, XTR, YTR: Integer;

  D: Integer = 200;
  Window1Color: TColor = $00F3E4A9;
  Window2Color: TColor = $00F0F3A9;
  RoofColor: TColor = $00545494;
  RoofWindowColor: TColor = $00FBC4E3;
  FramesColor: TColor = $00070756;
  FramesWidth: Integer = 1;
  ContourWidth: Integer = 1;
  ContourColor: TColor = clBlack;
  WallColor: TColor = $006FA7EC;
  BcgColor: TColor = $00C4E6E6;


implementation

{$R *.dfm}

function VectorCos(const V1, V2: TExtendedPoint): Extended; inline;
begin
  Result := (V1.X*V2.X + V1.Y*V2.Y)/
    Sqrt((V1.X*V1.X + V1.Y*V1.Y)*(V2.X*V2.X + V2.Y*V2.Y));
end;

function VectorCot(const V1, V2: TExtendedPoint): Extended; inline;
begin
  Result := (V1.X*V2.X + V1.Y*V2.Y)/(V1.Y*V2.X - V1.X*V2.Y);
end;

procedure TForm1.TranslationDefault;
begin
  edtDX.Text := '0';
  edtDY.Text := '0';
end;

procedure TForm1.RotationDefault;
begin
  edtAng.Text := '0';
  edtROX.Text := '0';
  edtROY.Text := '0';
end;

procedure TForm1.ScalingDefault;
begin
  edtSX.Text := '1';
  edtSY.Text := '1';
  edtSOX.Text := '0';
  edtSOY.Text := '0';
end;

function SetFullscreenMode: Boolean;
var
  DeviceMode : TDevMode;
begin
with DeviceMode do begin
  dmSize:=SizeOf(DeviceMode);
  dmBitsPerPel:=16;
  dmPelsWidth:=640;
  dmPelsHeight:=480;
  dmFields:=DM_BITSPERPEL or DM_PELSWIDTH or DM_PELSHEIGHT;
  result:=False;
  if ChangeDisplaySettings(DeviceMode,CDS_TEST or CDS_FULLSCREEN) <> DISP_CHANGE_SUCCESSFUL
    then Exit;
  Result:=ChangeDisplaySettings(DeviceMode,CDS_FULLSCREEN) = DISP_CHANGE_SUCCESSFUL;
  end;
end;


procedure RestoreDefaultMode;
var
  T : TDevMode;
begin
  ChangeDisplaySettings(T,CDS_FULLSCREEN);
end;

function ExtendedPoint(X, Y: Extended): TExtendedPoint; inline;
begin
  Result.X := X;
  Result.Y := Y;
end;

function Distance(P1, P2: TExtendedPoint): Extended; inline;
begin
  Result := Sqrt(Sqr(P2.X-P1.X) + Sqr(P2.Y-P1.Y));
end;

function Norm(V: TExtendedPoint): Extended;
begin
  Result := Sqrt(SQr(V.X) + Sqr(V.Y));
end;

function DistanceSqr(P1, P2: TExtendedPoint): Extended; inline;
begin
  Result := Sqr(P2.X-P1.X) + Sqr(P2.Y-P1.Y);
end;

function NormSqr(V: TExtendedPoint): Extended; inline;
begin
  Result := Sqr(V.X) + Sqr(V.Y);
end;

function ParallelogramSquare(P1, P2, P3: TExtendedPoint): Extended;
begin
  Result := Abs((P2.X-P1.X)*(P3.Y-P1.Y) - (P3.X-P1.X)*(P2.Y-P1.Y));
end;

function Perimeter(P1, P2, P3: TExtendedPoint): Extended;
begin
  Result := Distance(P1, P2) + Distance(P1, P3) + Distance(P2, P3);
end;

procedure NormalizeAngle(var Angle: Extended);
begin
  if Angle > PI2 then
    Angle := Angle - PI2;
end;

procedure MakeIdentity(var M: TMatrix);
begin
  M[0, 0] := 1;
  M[1, 1] := 1;
  M[2, 2] := 1;
  M[1, 0] := 0;
  M[2, 0] := 0;
  M[0, 1] := 0;
  M[2, 1] := 0;
  M[0, 2] := 0;
  M[1, 2] := 0;
end;

procedure MakeZero(var M: TMatrix);
begin
  M[0, 0] := 0;
  M[1, 1] := 0;
  M[2, 2] := 0;
  M[1, 0] := 0;
  M[2, 0] := 0;
  M[0, 1] := 0;
  M[2, 1] := 0;
  M[0, 2] := 0;
  M[1, 2] := 0;
end;

procedure AddTranslation(var M: TMatrix; DX, DY: Extended);
begin
  M[2, 0] := M[2, 0] + DX;
  M[2, 1] := M[2, 1] + DY;
end;

procedure SetPosition(var M: TMatrix; OldPos, NewPos: TExtendedPoint);
var
  DX, DY: Extended;
begin
  DX := NewPos.X - OldPos.X;
  DY := NewPos.Y - OldPos.Y;
  M[2, 0] := M[2, 0] + DX;
  M[2, 1] := M[2, 1] + DY;
end;

function CreateRotMatrix(SinA, CosA: Extended; Point: TExtendedPoint): TMatrix;
begin
	Result[0, 0] := CosA;
	Result[1, 0] := -SinA;
	Result[2, 0] := Point.X*(1-CosA) + Point.Y*SinA;
	Result[0, 1] := SinA;
	Result[1, 1] := CosA;
  Result[2, 1] := Point.Y*(1-CosA) - Point.X*SinA;
  Result[0, 2] := 0;
  Result[1, 2] := 0;
  Result[2, 2] := 1;
end;

procedure AddRotation(var M: TMatrix; Angle: Extended; Point: TExtendedPoint);
var
  CosA, SinA: Extended;
  C: Extended;
begin
  SinCos(Angle, SinA, CosA);
  SinA := -SinA;
	C := M[0, 0];
	M[0, 0] := M[0, 0]*CosA - M[0, 1]*SinA;
	M[0, 1] := C*SinA + M[0, 1]*CosA;
	C := M[1, 0];
	M[1, 0] := M[1, 0]*CosA - M[1, 1]*SinA;
	M[1, 1] := C*SinA + M[1, 1]*CosA;
	C := M[2, 0];
  M[2, 0] := (M[2, 0]-Point.X)*CosA + (-M[2, 1]+Point.Y)*SinA + Point.X;
	M[2, 1] := (C      -Point.X)*SinA + (M[2, 1] -Point.Y)*CosA + Point.Y;
end;

procedure AddScaling(var M: TMatrix; SX, SY: Extended; Point: TExtendedPoint);
var
  A: Extended;
begin
	M[0, 0] := M[0, 0] * SX;
	M[1, 0] := M[1, 0] * SX;
  M[2, 0] := M[2, 0] * SX + Point.X*(1-SX);
	M[0, 1] := M[0, 1] * SY;
	M[1, 1] := M[1, 1] * SY;
  M[2, 1] := M[2, 1] * SY + Point.Y*(1-SY);
end;

function Determinant(const M: TMatrix): Extended;
begin
  Result := M[0,0]*M[1,1] - M[0,1]*M[1,0];
end;

{function Inverse(const M: TMatrix): TMatrix;
var
  OneDivDet: Extended;
begin
  OneDivDet := 1/(M[0,0]*M[1,1] - M[0,1]*M[1,0]);

  Result[2,0] := (M[1,0]*M[2,1] - M[1,1]*M[2,0])*OneDivDet;
  Result[2,1] := (M[2,0]*M[0,1] - M[0,0]*M[2,1])*OneDivDet;

  Result[0,0] := M[1,1]*OneDivDet;
  Result[1,0] := M[0,0]*OneDivDet;

  Result[1,0] := -M[1,0]*OneDivDet;
  Result[0,1] := -M[0,1]*OneDivDet;
end;  }

procedure Inverse(var M: TMatrix);
var
  OneDivDet: Extended;
  T: Extended;
begin
  OneDivDet := 1/(M[0,0]*M[1,1] - M[0,1]*M[1,0]);

  T := M[2,0];
  M[2,0] := (M[1,0]*M[2,1] - M[1,1]*     T)*OneDivDet;
  M[2,1] := (T     *M[0,1] - M[0,0]*M[2,1])*OneDivDet;

  T := M[0,0];
  M[0,0] := M[1,1]*OneDivDet;
  M[1,1] := T     *OneDivDet;

  M[1,0] := -M[1,0]*OneDivDet;
  M[0,1] := -M[0,1]*OneDivDet;
end;

procedure PointTransform(var P: TExtendedPoint; const M: TMatrix);
var
  T: Extended;
begin
	T := P.X;
	P.X := P.X*M[0, 0] + P.Y*M[1, 0] + M[2, 0];
	P.Y := T * M[0, 1] + P.Y*M[1, 1] + M[2, 1];
end;

procedure VectorTransform(var V: TExtendedPoint; const M: TMatrix);
var
  T: Extended;
begin
	T := V.X;
	V.X := V.X*M[0, 0] + V.Y*M[1, 0];
	V.Y := T * M[0, 1] + V.Y*M[1, 1];
end;

procedure TForm1.DrawEllipse(const Ellipse: TEllipse; const Image: TImage);
var
  A, B: Extended;
  SinA, CosA, CotA: Extended;
  RM: TMAtrix;
  TStep: Extended;
  Ang: Extended;
  X1, Y1: Extended;
  I: Integer;
  Points: array of TPoint;
  T: Real;
begin
  {A := Norm(Ellipse.Dir1);
  B := Norm(Ellipse.Dir2);
  SinA := Ellipse.Dir1.Y/A;
  CosA := Ellipse.Dir1.X/A;
  RM[0, 0] := CosA; RM[1, 0] := -SinA; RM[2, 0] := Ellipse.Center.X;
  RM[0, 1] := SinA; RM[1, 1] := CosA;  RM[2, 1] := Ellipse.Center.Y;
  CotA := -VectorCot(Ellipse.Dir1, Ellipse.Dir2);
  RM[0, 0] := CosA; RM[1, 0] := -SinA + CotA*CosA; RM[2, 0] := Ellipse.Center.X;
  RM[0, 1] := SinA; RM[1, 1] := CosA + CotA*SinA;  RM[2, 1] := Ellipse.Center.Y;
  TStep := 3/(A + B);
  SetLength(Points, Trunc(PI2/TStep));
  Ang := 0;
  CotA := -VectorCot(Ellipse.Dir1, Ellipse.Dir2);
  for I := 0 to High(Points) do
  begin
    SinCos(Ang, SinA, CosA);
    Y1 := B*SinA;
    X1 := A*CosA;
    Points[I].X := Round(X1*RM[0,0] + Y1*RM[1,0] + RM[2,0]);
    Points[I].Y := Round(X1*RM[0,1] + Y1*RM[1,1] + RM[2,1]);
    Ang := Ang + TStep;
  end;
  Image.Canvas.Polygon(Points);    }
    {A := Norm(Ellipse.Dir1);
    B := Norm(Ellipse.Dir2);
    SinA := Ellipse.Dir1.Y/A;
    CosA := Ellipse.Dir1.X/A;
    RM[0, 0] := CosA; RM[1, 0] := -SinA; RM[2, 0] := Ellipse.Center.X;
    RM[0, 1] := SinA; RM[1, 1] := CosA;  RM[2, 1] := Ellipse.Center.Y;
    TStep := 1/Max(A, B);
    SetLength(Points, Trunc(PI2/TStep));
    Ang := 0;
    I := 0;
    while Ang < PI2 do
    begin
      SinCos(Ang, SinA, CosA);
      X1 := A*CosA;
      Y1 := B*SinA;
      Points[I].X := Round(X1*RM[0,0] + Y1*RM[1,0] + RM[2,0]);
      Points[I].Y := Round(X1*RM[0,1] + Y1*RM[1,1] + RM[2,1]);
      Inc(I);
      T := Sqr(A*sin(Ang)) + Sqr(B*cos(Ang));
      Ang := Ang + 3*Abs(A*B)/(T*Sqrt(T));
    end;
    SetLength(Points, I);
    Brush.Color := Window2Color;
    Image.Canvas.Polygon(Points);  }
end;

procedure ApplyTransform(var House: THouse; M: TMatrix);
var
  I: Integer;
begin
  PointTransform(House.Center, M);

  for I := 0 to High(House.Wall) do
    PointTransform(House.Wall[I], M);

  for I := 0 to High(House.Roof) do
    PointTransform(House.Roof[I], M);

  PointTransform(House.RoofWindow.Center, M);
  VectorTransform(House.RoofWindow.Dir1, M);
  VectorTransform(House.RoofWindow.Dir2, M);
  for I := 0 to EL_POINTS do
  begin
    PointTransform(House.RoofWindow.Points[I], M);
  end;

  PointTransform(House.WindowArc1.Center, M);
  VectorTransform(House.WindowArc1.Dir1, M);
  VectorTransform(House.WindowArc1.Dir2, M);
  for I := 0 to EL_POINTS do
  begin
    PointTransform(House.WindowArc1.Points[I], M);
  end;

  for I := 0 to High(House.Window1) do
    PointTransform(House.Window1[I], M);

  PointTransform(House.Window2.Center, M);
  VectorTransform(House.Window2.Dir1, M);
  VectorTransform(House.Window2.Dir2, M);
  for I := 0 to EL_POINTS do
  begin
    PointTransform(House.Window2.Points[I], M);
  end;

  for I := 0 to High(House.Window2Frames) do
    PointTransform(House.Window2Frames[I], M);

  for I := 0 to High(House.Frames) do
    PointTransform(House.Frames[I], M);
end;

procedure TForm1.ImageMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  if Button = mbLeft then
  begin
    LBDown := True;
    Image.Canvas.Pen.Mode := pmNotXor;
    DrawPoint(RPoint, clGreen);
    RPoint.X := X;
    RPoint.Y := Y;
    DrawPoint(RPoint, clGreen);
    edtROX.Text := IntToStr(X);
    edtROY.Text := IntToStr(Y);
  end;
  if Button = mbRight then
  begin
    RBDown := True;
    edtSOX.Text := IntToStr(X);
    edtSOY.Text := IntToStr(Y);
    Image.Canvas.Pen.Mode := pmNotXor;
    DrawPoint(SPoint, clBlue);
    SPoint.X := X;
    SPoint.Y := Y;
    DrawPoint(SPoint, clBlue);
  end;
end;

procedure TForm1.ImageMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
  if LBDown = True then
  begin
    Image.Canvas.Pen.Mode := pmNotXor;
    DrawPoint(RPoint, clGreen);
    RPoint.X := X;
    RPoint.Y := Y;
    DrawPoint(RPoint, clGreen);
    edtROX.Text := IntToStr(X);
    edtROY.Text := IntToStr(Y);
  end;
  if RBDown = True then
  begin
    Image.Canvas.Pen.Mode := pmNotXor;
    DrawPoint(SPoint, clBlue);
    SPoint.X := X;
    SPoint.Y := Y;
    DrawPoint(SPoint, clBlue);
    edtSOX.Text := IntToStr(X);
    edtSOY.Text := IntToStr(Y);
  end;
end;

procedure TForm1.ImageMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  if Button = mbLeft then
  begin
    LBDown := False;
  end;
  if Button = mbRight then
  begin
    RBDown := False;
  end;
end;

procedure TForm1.InitHouse(var House: THouse);
var
  I: Integer;
  Ang, Step: Extended;
  SinA, CosA: Extended;
begin
  House.Center := ExtendedPoint(4, 4);

  House.Wall[0] := ExtendedPoint(0, 8);
  House.Wall[1] := ExtendedPoint(0, 3);
  House.Wall[2] := ExtendedPoint(8, 3);
  House.Wall[3] := ExtendedPoint(8, 8);

  House.Roof[0] := ExtendedPoint(0, 3);
  House.Roof[1] := ExtendedPoint(2, 0);
  House.Roof[2] := ExtendedPoint(8, 3);

  House.WindowArc1.Center := ExtendedPoint(2.5, 4.5);
  House.WindowArc1.Dir1 := ExtendedPoint(1.5, 0);
  House.WindowArc1.Dir2 := ExtendedPoint(0, 0.5);
  Ang := 0;
  for I := 0 to EL_POINTS do
  begin
    SinCos(Ang, SinA, CosA);
    House.WindowArc1.Points[I] := ExtendedPoint(1.5*CosA+2.5, 0.5*SinA+4.5);
    Ang := Ang + EL_STEP;
  end;

  House.Window1[0] := ExtendedPoint(1, 7);
  House.Window1[1] := ExtendedPoint(1, 4.5);
  House.Window1[2] := ExtendedPoint(4, 4.5);
  House.Window1[3] := ExtendedPoint(4, 7);
  House.Window1[4] := ExtendedPoint(2.5, 7);
  House.Window1[5] := ExtendedPoint(2.5, 4);

  House.Window2.Center := ExtendedPoint(6, 5.5);
  House.Window2.Dir1 := ExtendedPoint(0.75, 0);
  House.Window2.Dir2 := ExtendedPoint(0, 1.5);
  Ang := 0;
  for I := 0 to EL_POINTS do
  begin
    SinCos(Ang, SinA, CosA);
    House.Window2.Points[I] := ExtendedPoint(0.75*CosA+6, 1.5*SinA+5.5);
    Ang := Ang + EL_STEP;
  end;

  House.Window2Frames[0] := ExtendedPoint(6, 7);
  House.Window2Frames[1] := ExtendedPoint(6, 4);
  House.Window2Frames[2] := ExtendedPoint(5.25, 5.5);
  House.Window2Frames[3] := ExtendedPoint(6.75, 5.5);

  House.RoofWindow.Center := ExtendedPoint(2.30, 1.5);
  House.RoofWindow.Dir1 := ExtendedPoint(0.7, 0);
  House.RoofWindow.Dir2 := ExtendedPoint(0, 0.7);
  Ang := 0;
  for I := 0 to EL_POINTS do
  begin
    SinCos(Ang, SinA, CosA);
    House.RoofWindow.Points[I] := ExtendedPoint(0.7*CosA+2.3, 0.7*SinA+1.5);
    Ang := Ang + EL_STEP;
  end;


  MakeIdentity(M);
  AddTranslation(M, Image.ClientWidth/2 - House.Center.X,
    Image.ClientHeight/2 - House.Center.Y);
  AddScaling(M, 12.5, 12.5, ExtendedPoint(Image.ClientWidth/2, Image.ClientHeight/2));
  ApplyTransform(House, M);
end;


procedure TForm1.BitBtn1Click(Sender: TObject);
begin
  Inverse(M);
  ApplyTransform(House, M);
  DrawHouse(House);
end;

procedure TForm1.BitBtn2Click(Sender: TObject);
var
  T1, T2, T3, T4: Extended;
  Error: Boolean;
begin
  MakeIdentity(M);
  if cbTranslate.Checked then
  begin
    edtDX.Color := clWhite;
    edtDY.Color := clWhite;
    if not TryStrToFloat(edtDX.Text, T1) then
    begin
      edtDX.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    if not TryStrToFloat(edtDY.Text, T2) then
    begin
      edtDY.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    AddTranslation(M , T1, T2);
  end;
  if cbRotate.Checked then
  begin
    edtAng.Color := clWhite;
    edtROX.Color := clWhite;
    edtROY.Color := clWhite;
    if not TryStrToFloat(edtAng.Text, T1) then
    begin
      edtAng.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    if not TryStrToFloat(edtROX.Text, T2) then
    begin
      edtROX.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    if not TryStrToFloat(edtROY.Text, T3) then
    begin
      edtROY.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    AddRotation(M, T1, ExtendedPoint(T2, T3));
  end;
  if cbScale.Checked then
  begin
    edtSX.Color := clWhite;
    edtSY.Color := clWhite;
    edtSOX.Color := clWhite;
    edtSOY.Color := clWhite;
    if not TryStrToFloat(edtSX.Text, T1) then
    begin
      edtSX.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    if not TryStrToFloat(edtSY.Text, T2) then
    begin
      edtSY.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    if not TryStrToFloat(edtSOX.Text, T3) then
    begin
      edtSOX.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    if not TryStrToFloat(edtSOY.Text, T4) then
    begin
      edtSOY.Color := errorColor;
      ShowMessage(InvalidValue);
      Exit;
    end;
    AddScaling(M, T1, T2, ExtendedPoint(T3, T4));
    if Determinant(M) = 0 then
    begin
      edtSX.Color := errorColor;
      edtSY.Color := errorColor;
      ShowMessage(InvalidTransform);
      Exit;
    end;
  end;

  ApplyTransform(House, M);
  DrawHouse(House);
end;

procedure TForm1.Button1Click(Sender: TObject);
var
  Key: Char;
begin
  InitHouse(House);
  DrawHouse(House);
end;

procedure TForm1.cbRotateClick(Sender: TObject);
begin
  if cbRotate.Checked then
  begin
    edtAng.Enabled := True;
    edtROX.Enabled := True;
    edtROY.Enabled := True;
  end
  else
  begin
    //RotationDefault;
    edtAng.Enabled := False;
    edtROX.Enabled := False;
    edtROY.Enabled := False;
  end;
end;

procedure TForm1.cbScaleClick(Sender: TObject);
begin
  if cbScale.Checked then
  begin
    edtSX.Enabled := True;
    edtSY.Enabled := True;
    edtSOX.Enabled := True;
    edtSOY.Enabled := True;
  end
  else
  begin
    //ScalingDefault;
    edtSX.Enabled := False;
    edtSY.Enabled := False;
    edtSOX.Enabled := False;
    edtSOY.Enabled := False;
  end;
end;

procedure TForm1.cbTranslateClick(Sender: TObject);
begin
  if cbTranslate.Checked then
  begin
    edtDX.Enabled := True;
    edtDY.Enabled := True;
  end
  else
  begin
    //TranslationDefault;
    edtDX.Enabled := False;
    edtDY.Enabled := False;
  end;
end;

procedure TForm1.DrawHouse(House: THouse);
var
  Center: TPoint;
  I: Integer;
  Ang, TStep: Extended;
  T: Extended;
  TRadius: Extended;
  A, B: Extended;
  SinA, CosA: Extended;
  X1, Y1: Extended;
  RM: TMatrix;
  Points: array of TPoint;
  ElPoints: array[0..EL_POINTS] of TPoint;
begin
  with Image.Canvas do
  begin
    Pen.Mode := pmCopy;

    Pen.Color := BcgColor;
    Brush.Color := BcgColor;
    Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);

    Pen.Width := ContourWidth;
    Pen.Color := ContourColor;
    Brush.Color := WallColor;
    SetLength(Points, Length(House.Wall));
    for I := 0 to High(House.Wall) do
      Points[I] := Point(Round(House.Wall[I].X), Round(House.Wall[I].Y));
    Polygon(Points);

    Pen.Width := ContourWidth;
    Pen.Color := ContourColor;
    Brush.Color := RoofColor;
    SetLength(Points, Length(House.Roof));
    for I := 0 to High(House.Roof) do
      Points[I] := Point(Round(House.Roof[I].X), Round(House.Roof[I].Y));
    Polygon(Points);

    Pen.Color := FramesColor;
    Brush.Color := RoofWindowColor;
    for I := 0 to EL_POINTS do
      ElPoints[I] := Point(Round(House.RoofWindow.Points[I].X),
                         Round(House.RoofWindow.Points[I].Y));
    Polygon(ElPoints);

    Brush.Style := bsFDiagonal;
    Brush.Color := FramesColor;
    Polygon(ElPoints);

    Brush.Color := Window1Color;
    Brush.Style := bsSolid;
    for I := 0 to EL_POINTS do
      ElPoints[I] := Point(Round(House.WindowArc1.Points[I].X),
                         Round(House.WindowArc1.Points[I].Y));
    Polygon(ElPoints);

    SetLength(Points, 4);
    for I := 0 to 3 do
      Points[I] := Point(Round(House.Window1[I].X), Round(House.Window1[I].Y));
    Polygon(Points);
    MoveTo(Round(House.Window1[4].X), Round(House.Window1[4].Y));
    LineTo(Round(House.Window1[5].X), Round(House.Window1[5].Y));

    Brush.Color := Window2Color;
    for I := 0 to EL_POINTS do
      ElPoints[I] := Point(Round(House.Window2.Points[I].X),
                         Round(House.Window2.Points[I].Y));
    Polygon(ElPoints);

    MoveTo(Round(House.Window2Frames[0].X), Round(House.Window2Frames[0].Y));
    LineTo(Round(House.Window2Frames[1].X), Round(House.Window2Frames[1].Y));
    MoveTo(Round(House.Window2Frames[2].X), Round(House.Window2Frames[2].Y));
    LineTo(Round(House.Window2Frames[3].X), Round(House.Window2Frames[3].Y));

    DrawPoint(RPoint, clGreen);
    DrawPoint(SPoint, clBlue);
  end
end;

procedure TForm1.DrawPoint(var Point: TExtendedPoint; Color: TColor);
var
  S: Integer;
begin
  S := 10;
  with Image.Canvas do
  begin
    Pen.Color := Color;
    Pen.Width := 1;
    //Brush.Color := Pen.Color;
    MoveTo(Round(Point.X-S), Round(Point.Y));
    LineTo(Round(Point.X+S), Round(Point.Y));
    MoveTo(Round(Point.X), Round(Point.Y-S));
    LineTo(Round(Point.X), Round(Point.Y+S));
    //Ellipse(Round(Point.X-S/2), Round(Point.Y-S/2), Round(Point.X+S/2), Round(Point.Y+S/2));
  end;
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
var
  M: TMatrix;
begin
  if not(Key in ['0'..'9', #8, '.', ',', #13]) then
    Key := #0;
  if Key = '.' then
    Key := ',';
end;

procedure TForm1.edtROXExit(Sender: TObject);
begin
  Image.Canvas.Pen.Mode := pmNotXor;
  DrawPoint(RPoint, clGreen);
  if not TryStrToFloat(edtRoX.Text, RPoint.X) then
  begin
    edtROX.Color := ErrorColor;
    Exit;
  end;
  if not TryStrToFloat(edtRoY.Text, RPoint.Y) then
  begin
    edtROY.Color := ErrorColor;
    Exit;
  end;
  DrawPoint(RPoint, clGreen);
end;

procedure TForm1.edtROXKeyPress(Sender: TObject; var Key: Char);
begin
  if not(Key in ['0'..'9', #8, '.', ',', #13, '-']) then
    Key := #0;
  if Key = '.' then
    Key := ',';
  if Key <> #13 then
    Exit;
end;

procedure TForm1.edtSoxExit(Sender: TObject);
begin
  Image.Canvas.Pen.Mode := pmNotXor;
  DrawPoint(SPoint, clRed);
  if not TryStrToFloat(edtSOX.Text, SPoint.X) then
  begin
    edtSOX.Color := ErrorColor;
    Exit;
  end;
  if not TryStrToFloat(edtSOY.Text, SPoint.Y) then
  begin
    edtSOY.Color := ErrorColor;
    Exit;
  end;
  DrawPoint(SPoint, clRed);
end;

procedure TForm1.edtSoxKeyPress(Sender: TObject; var Key: Char);
begin
  if not(Key in ['0'..'9', #8, '.', ',', #13, '-']) then
    Key := #0;
  if Key = '.' then
    Key := ',';
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  Key: Char;
begin
  InitHouse(House);
  DrawHouse(House);

  cbTranslate.OnClick(Self);
  cbRotate.OnClick(Self);
  cbScale.OnClick(Self);
end;

end.
