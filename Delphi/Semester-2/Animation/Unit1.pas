unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, Math;

type
  TRealPoint = record
    X, Y: Real;
  end;

type TMatrix = array[0..2, 0..2] of Real;

type
  TLeaf = record
    Color: TColor;
    Points: array[0..19] of TRealPoint;
    Body: array[0..1] of TRealPoint;
    CenterOfRot: TRealPoint;
  end;

  TSpider = record
    Color: TColor;
    Body: array[0..19] of TRealPoint;
    LegsBegin: array[0..5] of TRealPoint;
    LegsEnd: array[0..5] of TRealPoint;
    Eye1: TRealPoint;
    Eye2: TRealPoint;
    EyeRadius: Real;
    CenterOfRot: TRealPoint;
  end;

  THole = record
    Color: TColor;
    Points: array[0..29] of TRealPoint;
    Center: TRealPoint;
  end;

  TMole = record
    Color: TColor;
    Center: TRealPoint;
    Points: array[0..19] of TRealPoint;
    Visible: Boolean;
  end;

const
  PI2: Real =  6.283185307179586476925286766559;
  PID2: Real = 1.570796326794896619231321691639;
  LeafsCount: Integer = 7;
  SpidersCount: Integer = 12;
  SpidersColors: array[0..4] of Integer =
    (4359260,
     2451297,
     8979253,
     2323625,
     1749939);

type
  TForm1 = class(TForm)
    Image: TImage;
    Timer1: TTimer;
    procedure Timer1Timer(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    procedure DrawAllLeafs;
    procedure DrawAllSpiders;
    procedure DrawHole;
    procedure DrawMole;
    procedure ApplyTransformToLeaf(M: TMatrix);
    procedure GenerateLeaf(var Leaf: TLeaf);
    procedure GenerateSpider(var Spider: TSpider);
    procedure GenerateHole(var Hole: THole);
    procedure GenerateMole(var Mole: TMole);
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  Leafs: array of TLeaf;
  Spiders: array of TSpider;
  Hole: THole;
  BackGround: TBitmap;
  LegAng: Real;
  Angles: array of Real;
  Tick: Integer;
  Mole: TMole;


implementation

{$R *.dfm}

procedure TForm1.DrawAllLeafs;
var
  I: Integer;
  LeafPoints: array[0..19] of TPoint;
  J: Integer;

begin
  with Image.Canvas do
  begin
    for I := 0 to High(Leafs) do
    begin
      Pen.Color := Leafs[I].Color;
      brush.Color := Leafs[I].Color;
      for J := 0 to High(Leafs[I].Points) do
        LeafPoints[J] := Point(Round(Leafs[I].Points[J].X), Round(Leafs[I].Points[J].Y));
      Polygon(LeafPoints);
      Pen.Color := RGB(50, 100, 50);
      MoveTo(Round(Leafs[I].Body[0].X), Round(Leafs[I].Body[0].Y));
      LineTo(Round(Leafs[I].Body[1].X), Round(Leafs[I].Body[1].Y));
    end;
  end;
end;

procedure TForm1.DrawAllSpiders;
var
  I: Integer;
  SpiderPoints: array[0..19] of TPoint;
  J: Integer;
  SER: Integer;
begin
  with Image.Canvas do
  begin
    for I := 0 to High(Leafs) do
    begin
      Pen.Color := Spiders[I].Color;
      brush.Color := Spiders[I].Color;
      for J := 0 to High(Leafs[I].Points) do
        SpiderPoints[J] := Point(Round(Spiders[I].Body[J].X), Round(Spiders[I].Body[J].Y));
      Polygon(SpiderPoints);
      Pen.Color := RGB(50, 100, 50);
      Pen.Width := 2;
      for J := 0 to High(Spiders[I].LegsBegin) do
      begin
        MoveTo(Round(Spiders[I].LegsBegin[J].X), Round(Spiders[I].LegsBegin[J].Y));
        LineTo(Round(Spiders[I].LegsEnd[J].X), Round(Spiders[I].LegsEnd[J].Y));
      end;
      Pen.Width := 1;
      SER := Round(Spiders[I].EyeRadius);
      Brush.Color := Rgb(15, Random(130-70) + 70, 132);
      Pen.Color := Brush.Color;
      Ellipse(Round(Spiders[I].Eye1.X)-SER, Round(Spiders[I].Eye1.Y)-SER,
        Round(Spiders[I].Eye1.X)+SER, Round(Spiders[I].Eye1.Y)+SER);
      Ellipse(Round(Spiders[I].Eye2.X)-SER, Round(Spiders[I].Eye2.Y)-SER,
        Round(Spiders[I].Eye2.X)+SER, Round(Spiders[I].Eye2.Y)+SER);
      Brush.Color := 12040119;
      Pen.Color := 12040119;
      Ellipse(Round(Spiders[I].Eye1.X-SER*0.5), Round(Spiders[I].Eye1.Y-SER*0.5),
        Round(Spiders[I].Eye1.X+SER*0.5), Round(Spiders[I].Eye1.Y+SER*0.5));
      Ellipse(Round(Spiders[I].Eye2.X-SER*0.5), Round(Spiders[I].Eye2.Y-SER*0.5),
        Round(Spiders[I].Eye2.X+SER*0.5), Round(Spiders[I].Eye2.Y+SER*0.5));
    end;
  end;
end;

procedure TForm1.DrawHole;
var
  I: Integer;
  HolePoints: array[0..29] of TPoint;
begin
  with Image.Canvas do
  begin
    Pen.Color := Hole.Color;
    Brush.Color := Hole.Color;
    for I := 0 to High(Hole.Points) do
      HolePoints[I] := Point(Round(Hole.Points[I].X), Round(Hole.Points[I].Y));
    Polygon(HolePoints);
  end;
end;

procedure TForm1.DrawMole;
begin
  if Mole.Visible then
  begin
    with Image.Canvas do
    begin
      Pen.Color := 10078446;
      Brush.Color := 10078446;
      Polygon([Point(Round(Mole.Center.X+12), Round(Mole.Center.Y-5)),
               Point(Round(Mole.Center.X+12), Round(Mole.Center.Y+5)),
               Point(Round(Mole.Center.X+20), Round(Mole.Center.Y))]);
      Pen.Color := 3026478;
      Brush.Color := 3026478;
      Ellipse(Round(Mole.Center.x-15), Round(Mole.Center.Y-10),
      Round(Mole.Center.x+15), Round(Mole.Center.Y+10));
      Pen.Color := RGB(150, 150, 150);
      Brush.Color := Pen.Color;
      ELlipse(Round(Mole.Center.X+7-2), Round(Mole.Center.Y-7-2),
        Round(Mole.Center.X+7+2), Round(Mole.Center.Y-7+2));
      ELlipse(Round(Mole.Center.X+7-2), Round(Mole.Center.Y+7-2),
        Round(Mole.Center.X+7+2), Round(Mole.Center.Y+7+2));
    end;

  end;

end;

function RealPoint(X, Y: Real): TRealPoint; inline; overload;
begin
  Result.X := X;
  Result.Y := Y;
end;

function RealPoint(X, Y: Integer): TRealPoint; inline; overload;
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
  if Angle < PI2 then
    Angle := Angle + PI2;
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

procedure AddScaling(var M: TMatrix; SX, SY: Real; Point: TRealPoint);
begin
	M[0, 0] := M[0, 0] * SX;
	M[1, 0] := M[1, 0] * SX;
  M[2, 0] := M[2, 0] * SX + Point.X*(1-SX);
	M[0, 1] := M[0, 1] * SY;
	M[1, 1] := M[1, 1] * SY;
  M[2, 1] := M[2, 1] * SY + Point.Y*(1-SY);
end;

procedure VectorTransform(var V: TRealPoint; M: TMatrix);
var
  T: Real;
begin
	T := V.X;
	V.X := V.X*M[0, 0] + V.Y*M[1, 0] + M[2, 0];
	V.Y := T*M[0, 1]    + V.Y*M[1, 1] + M[2, 1];
end;

procedure TForm1.ApplyTransformToLeaf(M: TMatrix);
var
  I, J: Integer;
  B: Boolean;
begin
  for I := 0 to High(Leafs) do
  begin
    VectorTransform(Leafs[I].Body[0], M);
    VectorTransform(Leafs[I].Body[1], M);
    VectorTransform(Leafs[I].CenterOfRot, M);
    for J := 0 to High(Leafs[I].Points) do
    begin
      VectorTransform(Leafs[I].Points[J], M);
      {if Leafs[I].Points[J].X < 0 then
        Leafs[I].Points[J].X := Image.ClientWidth - Leafs[I].Points[J].X;
      if Leafs[I].Points[J].Y < 0 then
        Leafs[I].Points[J].Y := Image.ClientHeight - Leafs[I].Points[J].Y;    }
    end;
    if (Leafs[I].Points[0].X > Image.ClientWidth) or
       (Leafs[I].Points[0].Y > Image.ClientHeight) then
    begin
    for J := 0 to High(Leafs[I].Points) do
    begin
      Leafs[I].Points[J].X := Image.ClientWidth - Leafs[I].Points[J].X;
      Leafs[I].Points[J].Y := Image.ClientHeight - Leafs[I].Points[J].Y;
    end;
      Leafs[I].Body[0].X := Image.ClientWidth - Leafs[I].Body[0].X;
      Leafs[I].Body[0].Y := Image.ClientHeight - Leafs[I].Body[0].Y;
      Leafs[I].Body[1].X := Image.ClientWidth - Leafs[I].Body[1].X;
      Leafs[I].Body[1].Y := Image.ClientHeight - Leafs[I].Body[1].Y;
      Leafs[I].CenterOfRot.X := Image.ClientWidth - Leafs[I].CenterOfRot.X;
      Leafs[I].CenterOfRot.Y := Image.ClientHeight - Leafs[I].CenterOfRot.Y;
    end;

    B := False;
  end;
end;

procedure TForm1.GenerateLeaf(var Leaf: TLeaf);
var
  I: Integer;
  A, B, Ang: Real;
  AngInc: Real;
  M: TMatrix;
  X: Real;
  Center: TRealPoint;
begin
  A := 2;
  B := 1;
  AngInc := PI2/20;
  Ang := 0;
  Center := RealPoint(Random(Image.ClientWidth), Random(Image.ClientHeight));
  for I := 0 to 19 do
  begin
    Leaf.Points[I].X := A*Cos(Ang) + Center.X;
    Leaf.Points[I].Y := B*Sin(Ang) + Center.Y;
    Ang := Ang + AngInc;
  end;
  Leaf.Body[0].X := Center.X-A;
  Leaf.Body[0].Y := Center.Y;
  Leaf.Body[1].X := Center.X-A-2;
  Leaf.Body[1].Y := Center.Y;

  Leaf.Color := RGB(0, 244, Random(140));
  MakeIdentity(M);
  X := Random*4+2;
  AddScaling(M, X, X, Center);
  for I := 0 to High(Leaf.Points) do
    VectorTransform(Leaf.Points[I], M);
  for I := 0 to 1 do
    VectorTransform(Leaf.Body[I], M);

  Leaf.CenterOfRot := Center;

end;

procedure TForm1.GenerateSpider(var Spider: TSpider);
var
  I: Integer;
  A, B, Ang: Real;
  AngInc: Real;
  M: TMatrix;
  X: Real;
  Center: TRealPoint;
begin
  A := 2;
  B := 2;
  AngInc := PI2/20;
  Ang := 0;
  Center := RealPoint(Random(Image.ClientWidth), Random(Image.ClientHeight));
  for I := 0 to 19 do
  begin
    Spider.Body[I].X := A*Cos(Ang) + Center.X;
    Spider.Body[I].Y := B*Sin(Ang) + Center.Y;
    Ang := Ang + AngInc;
  end;
  Ang := Pi/2;
  for I := 1 to 5 do
  begin
    if I = 3 then
    begin
      Ang := Ang + PI/4;
      Continue
    end;
    Spider.LegsBegin[I].X := A*Cos(Ang) + Center.X;
    Spider.LegsBegin[I].Y := B*Sin(Ang) + Center.Y;
    Spider.LegsEnd[I].X := A*2*Cos(Ang) + Center.X;
    Spider.LegsEnd[I].Y := B*2*Sin(Ang) + Center.Y;
    Ang := Ang + PI/4;
  end;
  Spider.Eye1.X := A*Cos(Pi/4) + Center.X;
  Spider.Eye1.Y := B*Sin(Pi/4) + Center.Y;
  Spider.Eye2.X := A*Cos(-Pi/4) + Center.X;
  Spider.Eye2.Y := B*Sin(-Pi/4) + Center.Y;

  Spider.Color := SpidersColors[Random(4)];
  //Spider.CenterOfRot := RealPoint(RandomRange(50, Image.ClientWidth-50), RandomRange(50, Image.ClientHeight-50));
  MakeIdentity(M);
  X := Random*6+2;
  Spider.EyeRadius := X*0.7;
  AddScaling(M, X, X, Center);
  for I := 0 to High(Spider.Body) do
    VectorTransform(Spider.Body[I], M);
  VectorTransform(Spider.Eye1, M);
  VectorTransform(Spider.Eye2, M);
  for I := 0 to High(Spider.LegsBegin) do
  begin
    VectorTransform(Spider.LegsBegin[I], M);
    VectorTransform(Spider.LegsEnd[I], M);
  end;

  Spider.CenterOfRot := RealPoint(Center.X, Center.Y + Random*(Image.ClientHeight-Center.Y));

end;

procedure TForm1.GenerateHole(var Hole: THole);
var
  I: Integer;
  J: Integer;
  X1, Y1: Real;
  A, B: Real;
  K: Integer;
  Ang, Ang1: REal;

begin
  K := 0;
  A := 2;
  B := 2;
  Hole.Center := RealPoint(Random(Image.ClientWidth div 2), Random(Image.ClientHeight-50)+25);
  Ang := 0;
  for I := 0 to 29 do
  begin
    Hole.Points[I].X := A*Cos(Ang)*(Random+1) + Hole.Center.X;
    Hole.Points[I].Y := B*Sin(Ang)*(Random+1) + Hole.Center.Y;
    {Ang1 := 0;
    for J := 0 to 4 do
    begin
      Hole.Points[K].X := A*Cos(Ang1) + X1;
      Hole.Points[K].Y := B*Sin(Ang1) + Y1;
      Inc(K);
      Ang1 := Ang1 + 2*Pi/5;
    end;  }
    Ang := Ang + 2*Pi/30;
  end;
  Hole.Color := RGB(88, 38, 14);
end;

procedure TForm1.GenerateMole(var Mole: TMole);
  var
  I: Integer;
  J: Integer;
  X1, Y1: Real;
  A, B: Real;
  K: Integer;
  Ang, Ang1: REal;

begin
  K := 0;
  A := 15;
  B := 10;
  Mole.Center := Hole.Center;
  Ang := 0;
  for I := 0 to 19 do
  begin
    Mole.Points[I].X := A*Cos(Ang)*(Random+1) + Hole.Center.X;
    Mole.Points[I].Y := B*Sin(Ang)*(Random+1) + Hole.Center.Y;
    {Ang1 := 0;
    for J := 0 to 4 do
    begin
      Hole.Points[K].X := A*Cos(Ang1) + X1;
      Hole.Points[K].Y := B*Sin(Ang1) + Y1;
      Inc(K);
      Ang1 := Ang1 + 2*Pi/5;
    end;  }
    Ang := Ang + 2*Pi/30;
  end;
  Hole.Color := RGB(88, 38, 14);
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  I: Integer;
begin
  Randomize;
  SetLength(Leafs, LeafsCount);
  for I := 0 to LeafsCount - 1 do
    GenerateLeaf(Leafs[I]);
  SetLength(Spiders, SpidersCount);
  for I := 0 to SpidersCount - 1 do
    GenerateSpider(Spiders[I]);
  GenerateHole(Hole);
  Background := TBitmap.Create;
  BackGround.LoadFromFile('Ôîí.bmp');
  LegAng := Pi/4;
  SetLength(Angles, SpidersCount);
  for I := 0 to SpidersCount - 1 do
    Angles[I] := -5/(Spiders[I].body[0].Y-Spiders[I].CenterOfRot.Y);
  Tick := 0;
  Mole.Visible := False;
  Mole.Center := Hole.Center;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
var
  M, M1: TMatrix;
  P: TRealPoint;
  I, J: Integer;
begin
  Image.Canvas.Brush.Color := 7977200;
  Image.Canvas.Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);
  //Image.Picture.Bitmap := BackGround;
  {AddRotation(M, Random*Pi/12,
    RealPoint(
    Random(Image.ClientWidth div 2)+Image.ClientWidth div 4,
    Random(Image.Clientheight div 2)+Image.ClientHeight div 4
    )
    );   }
  for I := 0 to High(Leafs) do
  begin
    MakeIdentity(M1);
    AddRotation(M1, Random*Pi/2, Leafs[I].CenterOfRot);
    VectorTransform(Leafs[I].Body[0], M1);
    VectorTransform(Leafs[I].Body[1], M1);
    for J := 0 to High(Leafs[I].Points) do
      VectorTransform(Leafs[I].Points[J], M1);
  end;
  LegAng := (-1)*LegAng;
  for I := 0 to High(Spiders) do
  begin
    MakeIdentity(M1);
    AddRotation(M1, Angles[I], Spiders[I].CenterOfRot);
    for J := 0 to High(Leafs[I].Points) do
      VectorTransform(Spiders[I].Body[J], M1);
    VectorTransform(Spiders[I].Eye1, M1);
    VectorTransform(Spiders[I].Eye2, M1);
    for J := 0 to High(Spiders[I].LegsBegin) do
    begin
      VectorTransform(Spiders[I].LegsBegin[J], M1);
      VectorTransform(Spiders[I].LegsEnd[J], M1);
    end;
    for J := 0 to High(Spiders[I].LegsBegin) do
    begin

      MakeIdentity(M1);
      AddRotation(M1, LegAng, Spiders[I].LegsBegin[J]);
      VectorTransform(Spiders[I].LegsEnd[J], M1);
    end;
  end;

  MakeIdentity(M);
  AddTranslation(M, 15, 15);
  ApplyTransformToLeaf(M);

  MakeIdentity(M);
  Tick := Tick + 10;

  DrawHole;

  if Tick > 600 then
  begin
    with Image.Canvas do
    begin
      Pen.Color := RGB(117, 62, 40);
      Brush.Color := Pen.Color;
      Ellipse(Round(Hole.Center.x-10), Round(Hole.Center.Y-10),
              Round(Hole.Center.x+10), Round(Hole.Center.Y+10));
    end;
  end;


  if Tick > 900 then
  begin
    Mole.Visible := True;
    Mole.Center.X := Mole.Center.X + 5;
    DrawMole;
  end;

  if (Tick mod 30 = 0) and (Tick < 700) then
  begin
    AddScaling(M, 1.1, 1.1, Hole.Center);
    for I := 0 to High(Hole.Points) do
      VectorTransform(Hole.Points[I], M);
  end;
  DrawAllSpiders;
  DrawAllLeafs;

end;

end.
