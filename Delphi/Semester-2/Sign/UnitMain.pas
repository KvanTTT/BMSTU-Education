unit UnitMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, Math, Buttons, StdCtrls, Spin;

type
  TPolygon = array of TPoint;

type
  TRealPoint = record
    X, Y: Real;
  end;

type TMatrix = array[0..2, 0..2] of Real;

type TSign = record
  Center: TRealPoint;
  R1: Real;
  R2: Real;
  Vehicle: array[1..12] of TRealPoint;
  Autocar: array[1..14] of TRealPoint;
  CarGlass: array[1..4] of TRealPoint;
end;

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Image: TImage;
    Timer1: TTimer;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    pnlBkgCl: TPanel;
    Label5: TLabel;
    SpeedButton1: TSpeedButton;
    Panel2: TPanel;
    Label6: TLabel;
    ColorDialog1: TColorDialog;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Edit1: TEdit;
    edtAngle: TEdit;
    edtSpeed: TEdit;
    Button1: TButton;
    procedure FormCreate(Sender: TObject);
    procedure SpinEdit1Change(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure SpinEdit2Change(Sender: TObject);
    procedure pnlBkgClClick(Sender: TObject);
    procedure Panel2Click(Sender: TObject);
    procedure SpinEdit3Change(Sender: TObject);
    procedure SpinButton1UpClick(Sender: TObject);
    procedure SpinButton1DownClick(Sender: TObject);
    procedure SpeedButton1Click(Sender: TObject);
    procedure edtSpeedKeyPress(Sender: TObject; var Key: Char);
    procedure edtSpeedChange(Sender: TObject);
    procedure edtAngleKeyPress(Sender: TObject; var Key: Char);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
    procedure Edit1Exit(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    procedure DrawSign(Sign: TSign);
  public
    { Public declarations }
  end;

const
  PI2: Real = 6.283185307179586476925286766559;
  D: Integer = 200;

var
  Form1: TForm1;
  Sign: TSign;
  M: TMatrix;
  V: TRealPoint;
  Angle: Real;
  RotAngle: Real;
  Slope: array[1..3] of TPoint;
  SinA, CosA: Real;
  S: Real;
  SignAngle: Real;

implementation

{$R *.dfm}

function RealPoint(X, Y: Real): TRealPoint; inline;
begin
  Result.X := X;
  Result.Y := Y;
end;

function Distance(P1, P2: TRealPoint): Real; inline;
begin
  Result := Sqrt(Sqr(P2.X-P1.X) + Sqr(P2.Y-P1.Y));
end;

function DistanceSqr(P1, P2: TRealPoint): Real;
begin
  Result := Sqr(P2.X-P1.X) + Sqr(P2.Y-P1.Y);
end;

function ParallelogramSquare(P1, P2, P3: TRealPoint): Real;
begin
  Result := Abs((P2.X-P1.X)*(P3.Y-P1.Y) - (P3.X-P1.X)*(P2.Y-P1.Y));
end;

function Perimeter(P1, P2, P3: TRealPoint): Real;
begin
  Result := Distance(P1, P2) + Distance(P1, P3) + Distance(P2, P3);
end;

procedure NormalizeAngle(var Angle: Real);
begin
  if Angle > PI2 then
    Angle := Angle - PI2;
end;

function CenterOfInsCircle(P1, P2, P3: TRealPoint): TRealPoint;
var
  R: Real;
  OneDivOnePlusR: Real;
  A1, A2, B1, B2, C1, NegC2: Real;
  B: Real;
begin
  B := DistanceSqr(P1, P2);

  R := Sqrt(DistanceSqr(P1, P3)/B);
  OneDivOnePlusR := 1/(R + 1);
  A1 := P1.Y - (P3.Y + P2.Y*R)*OneDivOnePlusR;
  B1 := (P3.X + P2.X*R)*OneDivOnePlusR - P1.X;
  C1 := -(A1*P1.X + B1*P1.Y);

  R := Sqrt(B/DistanceSqr(P2, P3));
  OneDivOnePlusR := 1/(R + 1);
  A2 := P2.Y - (P1.Y + P3.Y*R)*OneDivOnePlusR;
  B2 := (P1.X + P3.X*R)*OneDivOnePlusR - P2.X;
  NegC2 := A2*P2.X + B2*P2.Y;

  Result.Y := (A2*C1 + A1*NegC2)/(A1*B2 - A2*B1);
  Result.X := (NegC2 - B2*Result.Y)/A2;
end;

function CenterOfCircCircle(P1, P2, P3: TRealPoint): TRealPoint;
var
  A1, A2, B1, B2, C1, NegC2: Real;
  S: Real;
begin
  A1 := P3.X-P1.X;
  A2 := P3.X-P2.X;
  B1 := P3.Y-P1.Y;
  B2 := P3.Y-P2.Y;
  C1 := -0.5*(A1*(P3.X+P1.X) + B1*(P3.Y+P1.Y));
  NegC2 := 0.5*(A2*(P3.X+P2.X) + B2*(P3.Y+P2.Y));
  S := A1*B2 - A2*B1;
  Result.Y := (A2*C1 + A1*NegC2)/(A1*B2 - A2*B1);
  Result.X := (NegC2 - B2*Result.Y)/A2;
end;

function RadiusOfInsCircle(P1, P2, P3: TRealPoint): Real;
begin
  Result := ParallelogramSquare(P1, P2, P3)/Perimeter(P1, P2, P3);
end;

function RadiusOfCircCircle(P1, P2, P3: TRealPoint): Real;
begin
  Result := 0.5*Sqrt(DistanceSqr(P1, P2)*DistanceSqr(P1, P3)*DistanceSqr(P2, P3))/
    ParallelogramSquare(P1, P2, P3);
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

procedure AddTranslation(var M: TMatrix; DX, DY: Real);
begin
  M[2, 0] := M[2, 0] + DX;
  M[2, 1] := M[2, 1] + DY;
end;

procedure SetPosition(var M: TMatrix; OldPos, NewPos: TRealPoint);
var
  DX, DY: Real;
begin
  DX := NewPos.X - OldPos.X;
  DY := NewPos.Y - OldPos.Y;
  M[2, 0] := M[2, 0] + DX;
  M[2, 1] := M[2, 1] + DY;
end;

procedure AddRotation(var M: TMatrix; Angle: Real; Point: TRealPoint);
var
  CosA, SinA: Real;
  C: Real;
begin
  SinA := Sin(Angle);
  CosA := Cos(Angle);
	C := M[0, 0];
	M[0, 0] := M[0, 0]*CosA - M[0, 1]*SinA;
	M[0, 1] := C*SinA + M[0, 1]*CosA;
	C := M[1, 0];
	M[1, 0] := M[1, 0]*CosA - M[1, 1]*SinA;
	M[1, 1] := C*SinA + M[1, 1]*CosA;
	C := M[2, 0];
	M[2, 0] := M[2, 0]*CosA - M[2, 1]*SinA - Point.X*CosA + Point.Y*SinA + Point.X;
	M[2, 1] := C*SinA + M[2, 1]*CosA - Point.X*SinA - Point.Y*CosA + Point.Y;
end;

procedure AddScaling(var M: TMatrix; var Sign: TSign; SX, SY: Real; Point: TRealPoint);
begin
	M[0, 0] := M[0, 0] * SX;
	M[1, 0] := M[1, 0] * SX;
  M[2, 0] := M[2, 0] * SX + Point.X*(1-SX);
	M[0, 1] := M[0, 1] * SY;
	M[1, 1] := M[1, 1] * SY;
  M[2, 1] := M[2, 1] * SY + Point.Y*(1-SY);
  Sign.R1 := Sign.R1 * SX;
  Sign.R2 := Sign.R2 * SX;
end;

procedure VectorTransform(var V: TRealPoint; M: TMatrix);
var
  T: Real;
begin
	T := V.X;
	V.X := V.X*M[0, 0] + V.Y*M[1, 0] + M[2, 0];
	V.Y := T*M[0, 1]    + V.Y*M[1, 1] + M[2, 1];
end;

procedure ApplyTransform(var Sign: TSign; M: TMatrix);
var
  I: Integer;
begin
  for I := 1 to 12 do
    VectorTransform(Sign.Vehicle[I], M);
  for I := 1 to 14 do
    VectorTransform(Sign.AutoCar[I], M);
  for I := 1 to 4 do
    VectorTransform(Sign.CarGlass[I], M);
  VectorTransform(Sign.Center, M);
end;


procedure TForm1.Button1Click(Sender: TObject);
var
  X1, Y1: Real;
  Key: Char;
begin
  S := 1;
  SignAngle := 0;
  Sign.Center := RealPoint(7.5, 7.5);
  Sign.R1 := 7.5;
  Sign.R2 := 6.5;

  Sign.Vehicle[1] := RealPoint(3, 4);
  Sign.Vehicle[2] := RealPoint(7, 4);
  Sign.Vehicle[3] := RealPoint(7, 9);
  Sign.Vehicle[4] := RealPoint(6.5, 9);
  Sign.Vehicle[5] := RealPoint(6.5, 10);
  Sign.Vehicle[6] := RealPoint(6, 10);
  Sign.Vehicle[7] := RealPoint(6, 9.2);
  Sign.Vehicle[8] := RealPoint(4, 9.2);
  Sign.Vehicle[9] := RealPoint(4, 10);
  Sign.Vehicle[10] := RealPoint(3.5, 10);
  Sign.Vehicle[11] := RealPoint(3.5, 9);
  Sign.Vehicle[12] := RealPoint(3, 9);

  Sign.Autocar[1] := RealPoint(8.5, 6);
  Sign.Autocar[2] := RealPoint(11, 6);
  Sign.Autocar[3] := RealPoint(12, 7.5);
  Sign.Autocar[4] := RealPoint(12, 9);
  Sign.Autocar[5] := RealPoint(11.5, 9);
  Sign.Autocar[6] := RealPoint(11.5, 10);
  Sign.Autocar[7] := RealPoint(11, 10);
  Sign.Autocar[8] := RealPoint(11, 9);
  Sign.Autocar[9] := RealPoint(8.5, 9);
  Sign.Autocar[10] := RealPoint(8.5, 10);
  Sign.Autocar[11] := RealPoint(8, 10);
  Sign.Autocar[12] := RealPoint(8, 9);
  Sign.Autocar[13] := RealPoint(7.5, 9);
  Sign.Autocar[14] := RealPoint(7.5, 7.5);

  Sign.CarGlass[1] := RealPoint(8.7, 6.3);
  Sign.CarGlass[2] := RealPoint(10.8, 6.3);
  Sign.CarGlass[3] := RealPoint(11.7, 7.5);
  Sign.CarGlass[4] := RealPoint(7.8, 7.5);

  Slope[1].X := D;
  Slope[1].Y := Image.ClientHeight;
  Slope[2].X := Image.ClientWidth;
  Slope[2].Y := Image.ClientHeight;
  Slope[3].X := Image.ClientWidth;
  Slope[3].Y := Image.ClientHeight - Round(Image.ClientWidth*Tan(StrToFloat(edtAngle.Text)));

  Angle := StrToFloat(edtAngle.Text);
  MakeIdentity(M);
  AddScaling(M, Sign, 12, 12, RealPoint(0, 0));
  AddTranslation(M, 200, 0);
  ApplyTransform(Sign, M);

  Key := #13;
  edtAngle.OnKeyPress(Self, Key);
  edtSpeed.OnKeyPress(Self, Key);
  Edit1.OnKeyPress(Self, Key);
end;

procedure TForm1.DrawSign(Sign: TSign);
var
  Center: TPoint;
  R1, R2: Integer;
begin
  Center.X := Round(Sign.Center.X);
  Center.Y := Round(Sign.Center.Y);
  R1 := Round(Sign.R1);
  R2 := Round(Sign.R2);
  with Image.Canvas do
  begin
    Pen.Color := clRed;
    Brush.Color := clRed;
    Ellipse(Center.X-R1, Center.Y-R1, Center.X+R1, Center.Y+R1);
    Pen.Color := clWhite;
    Brush.Color := clWhite;
    Ellipse(Center.X-R2, Center.Y-R2, Center.X+R2, Center.Y+R2);
    Pen.Color := clRed;
    Brush.Color := clRed;
    Polygon([Point(Round(Sign.Vehicle[1].X), Round(Sign.Vehicle[1].Y)),
             Point(Round(Sign.Vehicle[2].X), Round(Sign.Vehicle[2].Y)),
             Point(Round(Sign.Vehicle[3].X), Round(Sign.Vehicle[3].Y)),
             Point(Round(Sign.Vehicle[4].X), Round(Sign.Vehicle[4].Y)),
             Point(Round(Sign.Vehicle[5].X), Round(Sign.Vehicle[5].Y)),
             Point(Round(Sign.Vehicle[6].X), Round(Sign.Vehicle[6].Y)),
             Point(Round(Sign.Vehicle[7].X), Round(Sign.Vehicle[7].Y)),
             Point(Round(Sign.Vehicle[8].X), Round(Sign.Vehicle[8].Y)),
             Point(Round(Sign.Vehicle[9].X), Round(Sign.Vehicle[9].Y)),
             Point(Round(Sign.Vehicle[10].X), Round(Sign.Vehicle[10].Y)),
             Point(Round(Sign.Vehicle[11].X), Round(Sign.Vehicle[11].Y)),
             Point(Round(Sign.Vehicle[12].X), Round(Sign.Vehicle[12].Y))]);
    Pen.Color := clBlack;
    Brush.Color := clBlack;
    Polygon([Point(Round(Sign.Autocar[1].X), Round(Sign.Autocar[1].Y)),
             Point(Round(Sign.Autocar[2].X), Round(Sign.Autocar[2].Y)),
             Point(Round(Sign.Autocar[3].X), Round(Sign.Autocar[3].Y)),
             Point(Round(Sign.Autocar[4].X), Round(Sign.Autocar[4].Y)),
             Point(Round(Sign.Autocar[5].X), Round(Sign.Autocar[5].Y)),
             Point(Round(Sign.Autocar[6].X), Round(Sign.Autocar[6].Y)),
             Point(Round(Sign.Autocar[7].X), Round(Sign.Autocar[7].Y)),
             Point(Round(Sign.Autocar[8].X), Round(Sign.Autocar[8].Y)),
             Point(Round(Sign.Autocar[9].X), Round(Sign.Autocar[9].Y)),
             Point(Round(Sign.Autocar[10].X), Round(Sign.Autocar[10].Y)),
             Point(Round(Sign.Autocar[11].X), Round(Sign.Autocar[11].Y)),
             Point(Round(Sign.Autocar[12].X), Round(Sign.Autocar[12].Y)),
             Point(Round(Sign.Autocar[13].X), Round(Sign.Autocar[13].Y)),
             Point(Round(Sign.Autocar[14].X), Round(Sign.Autocar[14].Y))]);
    Pen.Color := clWhite;
    Brush.Color := clWhite;
    Polygon([Point(Round(Sign.CarGlass[1].X), Round(Sign.CarGlass[1].Y)),
             Point(Round(Sign.CarGlass[2].X), Round(Sign.CarGlass[2].Y)),
             Point(Round(Sign.CarGlass[3].X), Round(Sign.CarGlass[3].Y)),
             Point(Round(Sign.CarGlass[4].X), Round(Sign.CarGlass[4].Y))]);
  end;                                                                     
end;

procedure TForm1.Edit1Exit(Sender: TObject);
begin
    MakeIdentity(M);
    AddScaling(M, Sign, S*StrToFloat(Edit1.Text), S*StrToFloat(Edit1.Text),
    RealPoint(Sign.Center.X + Sign.R1*SinA, Sign.Center.Y + Sign.R1*CosA));
    ApplyTransform(Sign, M);
    //Timer1.Enabled := True;
    with Image.Canvas do
    begin
      Pen.Color := pnlBkgCl.Color;
      Brush.Color := pnlBkgCl.Color;
      Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
      Pen.Color := Panel2.Color;
      Brush.Color := Panel2.Color;
      Polygon(Slope);
    end;
    RotAngle := -StrToFloat(edtSpeed.Text)/(Sign.R1)*Timer1.Interval/1000;
    DrawSign(Sign);
    S := 1/StrToFloat(Edit1.Text);
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
var
  M: TMatrix;
begin
  if not(Key in ['0'..'9', #8, '.', ',', #13]) then
    Key := #0;
  if Key = '.' then
    Key := ',';
  if Key <> #13 then
    Exit;

    MakeIdentity(M);
    AddScaling(M, Sign, S*StrToFloat(Edit1.Text), S*StrToFloat(Edit1.Text),
    RealPoint(Sign.Center.X + Sign.R1*SinA, Sign.Center.Y + Sign.R1*CosA));
    ApplyTransform(Sign, M);
    //Timer1.Enabled := True;
    with Image.Canvas do
    begin
    Pen.Color := pnlBkgCl.Color;
    Brush.Color := pnlBkgCl.Color;
    Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
    Pen.Color := Panel2.Color;
    Brush.Color := Panel2.Color;
    Polygon(Slope);
    end;
    DrawSign(Sign);
    RotAngle := -StrToFloat(edtSpeed.Text)/(Sign.R1)*Timer1.Interval/1000;
    S := 1/StrToFloat(Edit1.Text);
end;

procedure TForm1.edtAngleKeyPress(Sender: TObject; var Key: Char);
var
  X1, Y1: Real;
  SlopeAngle: Real;
begin
  if not(Key in ['0'..'9', #8, #13]) then
    Key := #0;

  if Key = #13 then
  begin

  if (edtAngle.Text = '') then
  begin
    ShowMessage('Недопустипое значение угла');
    edtAngle.Text := '0';
  end;
    SlopeAngle := StrToFloat(edtAngle.Text);
  if (SlopeAngle > 90) or (SlopeAngle < 0) then
  begin
    ShowMessage('Недопустипое значение угла');
    edtAngle.Text := '0';
  end;

  Angle := StrToFloat(edtAngle.Text);
  if Angle = 90 then
    Angle := 89.99;
  Angle := Angle/180*Pi;
  SinA := Sin(Angle);
  CosA := Cos(Angle);
  V.X := -CosA*StrToFloat(edtSpeed.Text)*Timer1.Interval/1000;
  V.Y := SinA*StrToFloat(edtSpeed.Text)*Timer1.Interval/1000;
  Slope[3].Y := Image.ClientHeight - Round((Image.ClientWidth-D)*SinA/CosA);
  MakeIdentity(M);
  if Slope[3].Y < 0 then
  begin
    X1 := -Slope[3].Y*(Image.ClientWidth-D)/(-Slope[3].Y+Image.ClientHeight);
    X1 := Image.ClientWidth - X1 - Sign.R1/SinA;
    SetPosition(M, Sign.Center, RealPoint(X1, 0));
  end
  else
  begin
    {Y1 := Sqrt(Sqr(Image.ClientWidth)+Sqr(Image.ClientHeight-Slope[3].Y))*Sign.R1/Image.ClientWidth;
    Y1 := Slope[3].Y - Y1;   }
    Y1 := Slope[3].Y - Sign.R1/CosA;
    SetPosition(M, Sign.Center, RealPoint(Image.ClientWidth, Y1));
  end;
  ApplyTransform(Sign, M);
  with Image.Canvas do
  begin
    Pen.Color := pnlBkgCl.Color;
    Brush.Color := pnlBkgCl.Color;
    Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
    Pen.Color := Panel2.Color;
    Brush.Color := Panel2.Color;
    Polygon(Slope);
  end;
  DrawSign(Sign);
  end;
end;

procedure TForm1.edtSpeedChange(Sender: TObject);
var
  Speed: Real;
begin
    {SinA := Sin(Angle);
    CosA := Cos(Angle);
    RotAngle := -StrToFloat(edtSpeed.Text)/180*PI;
    Speed := -RotAngle*Sign.R1/(Timer1.Interval/1000);
    V.X := -CosA*Speed;
    V.Y := SinA*Speed;
    DrawSign(Sign);
    Label9.Caption := FloatToStrF(Speed, ffFixed, 6, 4) + 'пикс/сек';   }


end;

procedure TForm1.edtSpeedKeyPress(Sender: TObject; var Key: Char);
var
  Speed: Real;
begin
  if not(Key in ['0'..'9', #8, #13]) then
    Key := #0;

  if Key = #13 then
  begin

  if (edtSpeed.Text = '') then
  begin
    ShowMessage('Недопустипое значение скорости');
    edtSpeed.Text := '0';
    Exit;
  end;
    Speed :=  StrToFloat(edtSpeed.Text);
  if (Speed >= 1000) then
  begin
    ShowMessage('Недопустипое значение скорости');
    edtSpeed.Text := '0';
    Speed := 0;
  end;
    Angle := StrToFloat(edtAngle.Text)/180*PI;
    SinA := Sin(Angle);
    CosA := Cos(Angle);
    V.X := -CosA*Speed*Timer1.Interval/1000;
    V.Y := SinA*Speed*Timer1.Interval/1000;
    RotAngle := -Speed/(Sign.R1)*Timer1.Interval/1000;
    Label9.Caption := FloatToStrF(Speed/(PI2*Sign.R1)/Pi*180,
    ffFixed, 6, 4) + ' градусов/сек';
    DrawSign(Sign);
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  P: TRealPoint;
  R: Real;
  Key: Char;
  X1, Y1: Real;
begin
  S := 1;
  SignAngle := 0;
  Sign.Center := RealPoint(7.5, 7.5);
  Sign.R1 := 7.5;
  Sign.R2 := 6.5;

  Sign.Vehicle[1] := RealPoint(3, 4);
  Sign.Vehicle[2] := RealPoint(7, 4);
  Sign.Vehicle[3] := RealPoint(7, 9);
  Sign.Vehicle[4] := RealPoint(6.5, 9);
  Sign.Vehicle[5] := RealPoint(6.5, 10);
  Sign.Vehicle[6] := RealPoint(6, 10);
  Sign.Vehicle[7] := RealPoint(6, 9.2);
  Sign.Vehicle[8] := RealPoint(4, 9.2);
  Sign.Vehicle[9] := RealPoint(4, 10);
  Sign.Vehicle[10] := RealPoint(3.5, 10);
  Sign.Vehicle[11] := RealPoint(3.5, 9);
  Sign.Vehicle[12] := RealPoint(3, 9);

  Sign.Autocar[1] := RealPoint(8.5, 6);
  Sign.Autocar[2] := RealPoint(11, 6);
  Sign.Autocar[3] := RealPoint(12, 7.5);
  Sign.Autocar[4] := RealPoint(12, 9);
  Sign.Autocar[5] := RealPoint(11.5, 9);
  Sign.Autocar[6] := RealPoint(11.5, 10);
  Sign.Autocar[7] := RealPoint(11, 10);
  Sign.Autocar[8] := RealPoint(11, 9);
  Sign.Autocar[9] := RealPoint(8.5, 9);
  Sign.Autocar[10] := RealPoint(8.5, 10);
  Sign.Autocar[11] := RealPoint(8, 10);
  Sign.Autocar[12] := RealPoint(8, 9);
  Sign.Autocar[13] := RealPoint(7.5, 9);
  Sign.Autocar[14] := RealPoint(7.5, 7.5);

  Sign.CarGlass[1] := RealPoint(8.7, 6.3);
  Sign.CarGlass[2] := RealPoint(10.8, 6.3);
  Sign.CarGlass[3] := RealPoint(11.7, 7.5);
  Sign.CarGlass[4] := RealPoint(7.8, 7.5);

  Slope[1].X := D;
  Slope[1].Y := Image.ClientHeight;
  Slope[2].X := Image.ClientWidth;
  Slope[2].Y := Image.ClientHeight;
  Slope[3].X := Image.ClientWidth;
  Slope[3].Y := Image.ClientHeight - Round(Image.ClientWidth*Tan(StrToFloat(edtAngle.Text)));


  Angle := StrToFloat(edtAngle.Text);
  MakeIdentity(M);
  AddScaling(M, Sign, 12, 12, RealPoint(0, 0));
  AddTranslation(M, 200, 0);
  ApplyTransform(Sign, M);

  Key := #13;
  edtAngle.OnKeyPress(Self, Key);
  edtSpeed.OnKeyPress(Self, Key);


end;

procedure TForm1.Panel2Click(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    Panel2.Color := ColorDialog1.Color;
      with Image.Canvas do
  begin
    Pen.Color := pnlBkgCl.Color;
    Brush.Color := pnlBkgCl.Color;
    Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
    Pen.Color := Panel2.Color;
    Brush.Color := Panel2.Color;
    Polygon(Slope);
  end;
  end;
end;

procedure TForm1.pnlBkgClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlBkgCl.Color := ColorDialog1.Color;
  with Image.Canvas do
  begin
    Pen.Color := pnlBkgCl.Color;
    Brush.Color := pnlBkgCl.Color;
    Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
    Pen.Color := Panel2.Color;
    Brush.Color := Panel2.Color;
    Polygon(Slope);
  end;
  end;
end;

procedure TForm1.SpeedButton1Click(Sender: TObject);
begin
  if SpeedButton1.Down then
  begin
    SpeedButton1.Down := True;
    SpeedButton1.Caption := 'Стоп';
    Timer1.Enabled := True
  end
  else
  begin
    SpeedButton1.Down := False;
    SpeedButton1.Caption := 'Старт';
    Timer1.Enabled := False
  end;
  SignAngle := 0;
end;

procedure TForm1.SpinButton1DownClick(Sender: TObject);
begin
  if Edit1.Text > '0,1' then
  begin
    //Timer1.Enabled := False;
    Edit1.Text := FloatToStr(StrToFloat(Edit1.Text) - 0.1);
  end;
end;

procedure TForm1.SpinButton1UpClick(Sender: TObject);
begin
  if Edit1.Text < '3' then
  begin
    //Timer1.Enabled := False;
    Edit1.Text := FloatToStr(StrToFloat(Edit1.Text) + 0.1);
  end;
end;

procedure TForm1.SpinEdit1Change(Sender: TObject);
var
  X1, Y1: Real;
begin
  Angle := StrToFloat(edtAngle.Text)/180*PI;
  SinA := Sin(Angle);
  CosA := Cos(Angle);
  V.X := -CosA*StrToFloat(edtSpeed.Text)*Timer1.Interval/1000;
  V.Y := SinA*StrToFloat(edtSpeed.Text)*Timer1.Interval/1000;
  Slope[3].Y := Image.ClientHeight - Round(Image.ClientWidth*SinA/CosA);
  MakeIdentity(M);
  if Slope[3].Y < 0 then
  begin
    X1 := Image.ClientHeight*Image.ClientWidth/(Image.ClientHeight-Slope[3].Y);
    X1 := X1 + Sqrt(Sqr(Slope[3].Y)+Sqr(Image.Width-X1))*Sign.R1/Slope[3].Y;
    SetPosition(M, Sign.Center, RealPoint(X1, 0));
  end
  else
  begin
    Y1 := Sqrt(Sqr(Image.ClientWidth)+Sqr(Image.ClientHeight-Slope[3].Y))*Sign.R1/Image.ClientWidth;
    Y1 := Slope[3].Y - Y1;
    SetPosition(M, Sign.Center, RealPoint(Image.ClientWidth, Y1));
  end;
  ApplyTransform(Sign, M);
  with Image.Canvas do
  begin
    Pen.Color := pnlBkgCl.Color;
    Brush.Color := pnlBkgCl.Color;
    Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
    Pen.Color := Panel2.Color;
    Brush.Color := Panel2.Color;
    Polygon(Slope);
  end;
  DrawSign(Sign);
end;

procedure TForm1.SpinEdit2Change(Sender: TObject);
begin
  Angle := StrToFloat(edtAngle.Text)/180*PI;
  SinA := Sin(Angle);
  CosA := Cos(Angle);
  V.X := -CosA*StrToFloat(edtSpeed.Text)*Timer1.Interval/1000;
  V.Y := SinA*StrToFloat(edtSpeed.Text)*Timer1.Interval/1000;
  RotAngle := -StrToFloat(edtSpeed.Text)/(Sign.R1)*Timer1.Interval/1000;
  Label9.Caption := FloatToStrF(StrToFloat(edtSpeed.text)/(PI2*Sign.R1)/Pi*180,
    ffFixed, 6, 4) + ' градусов/сек';
  DrawSign(Sign);
end;

procedure TForm1.SpinEdit3Change(Sender: TObject);
begin
  //edtSpeed.Text := IntToStr(Round(StrToInt(SpinEdit3.Text)/180*Pi*Sign.R1));
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  with Image.Canvas do
  begin
    Pen.Color := pnlBkgCl.Color;
    Brush.Color := pnlBkgCl.Color;
    Ellipse(Round(Sign.Center.X - Sign.R1), Round(Sign.Center.Y - Sign.R1),
            Round(Sign.Center.X + Sign.R1), Round(Sign.Center.Y + Sign.R1));
  end;
  MakeIdentity(M);
  AddRotation(M, RotAngle, Sign.Center);
  SignAngle := SignAngle + RotAngle;
  NormalizeAngle(SignAngle);
  AddTranslation(M, V.X, V.Y);
  ApplyTransform(Sign, M);
  DrawSign(Sign);
end;

end.
