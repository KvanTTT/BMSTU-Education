unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls;

type TRealPoint = record
  X, Y: Real;
end;

type TRealRect = record
  LT, RB: TRealPoint;
end;

type TRealPolygon = array of TRealPoint;

type TFir = record
  Brunchs: array of TRealPolygon;
  Trunk: array of TRealPoint;
  BrunchColor: TColor;
  TrunkColor: TColor;
  BoundBox: TRealRect;
  DX, DY: Real;
  Center: TRealPoint;
  SP: TRealPoint;
end;


type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Image1: TImage;
    Label1: TLabel;
    Edit1: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    Edit2: TEdit;
    UpDown1: TUpDown;
    Button1: TButton;
    Timer1: TTimer;
    UpDown2: TUpDown;
    Button2: TButton;
    procedure Timer1Timer(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Edit2Change(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    procedure DrawFir(const Fir: TFir);
  end;

const
  TrunksColors: array[0..4] of TColor = ($0034413D, $003A3A5A, $00333625, $0039575B, $0040373A);
  BrunchsColors: array[0..4] of TColor = ($00168F10, $00808040, $007A8011, $0001A96F, $002E7662);
  BgClr: TColor = $00DCF5FC;

var
  Form1: TForm1;
  GrowthTime: Integer;
  TotalTime: Integer;
  Time: Integer;
  TimeId: Integer;
  Firs: array of TFir;
  CurFir: Integer;
  State: Boolean = False;
  BState: Boolean = True;

implementation

{$R *.dfm}

function RealPoint(X, Y: Real): TRealPoint;
begin
  Result.X := X;
  Result.Y := Y;
end;

procedure InitFir(var Fir: TFir);
begin
  with Fir do
  begin
    SetLength(Trunk, 4);
    Trunk[0] := RealPoint(1.5, 5);
    Trunk[1] := RealPoint(2.5, 5);
    Trunk[2] := RealPoint(2.5, 3.5);
    Trunk[3] := RealPoint(1.5, 3.5);
    SetLength(Brunchs, 4);
    SetLength(Brunchs[0], 3);
    Brunchs[0][0] := RealPoint(0, 3.5);
    Brunchs[0][1] := RealPoint(4, 3.5);
    Brunchs[0][2] := RealPoint(2, 2.6);
    SetLength(Brunchs[1], 3);
    Brunchs[1][0] := RealPoint(0.5, 2.6);
    Brunchs[1][1] := RealPoint(3.5, 2.6);
    Brunchs[1][2] := RealPoint(2, 1.6);
    SetLength(Brunchs[2], 3);
    Brunchs[2][0] := RealPoint(1, 1.6);
    Brunchs[2][1] := RealPoint(3, 1.6);
    Brunchs[2][2] := RealPoint(2, 0.8);
    SetLength(Brunchs[3], 3);
    Brunchs[3][0] := RealPoint(1.5, 0.8);
    Brunchs[3][1] := RealPoint(2.5, 0.8);
    Brunchs[3][2] := RealPoint(2, 0);
  end;
end;

procedure SetPos(var Fir: TFir; Pos: TRealPoint);
var
  I, J: Integer;
  D: TRealPoint;
begin
  with Fir do
  begin
    D.X := Pos.X - (Trunk[0].X + Trunk[1].X)/2;
    D.Y := Pos.Y - (Trunk[0].Y + Trunk[1].Y)/2;
    for I := 0 to High(Trunk) do
    begin
      Trunk[I].X := Trunk[I].X + D.X;
      Trunk[I].Y := Trunk[I].Y + D.Y;
    end;
    for I := 0 to High(Brunchs) do
      for J := 0 to High(Brunchs[I]) do
      begin
        Brunchs[I][J].X := Brunchs[I][J].X + D.X;
        Brunchs[I][J].Y := Brunchs[I][J].Y + D.Y;
      end;
  end;
end;

procedure AddScale(var Fir: TFir);
var
  SX, SY: Real;
  I: Integer;
  J: Integer;
begin
  SX := 1 + Fir.DX/(Fir.Trunk[2].X - Fir.SP.X);
  SY := 1 + Fir.DY/(Fir.SP.Y       - Fir.Trunk[2].Y);
  Fir.Trunk[0].X := Fir.Trunk[0].X - Fir.DX;
  Fir.Trunk[1].X := Fir.Trunk[1].X + Fir.DX;
  Fir.Trunk[2].X := Fir.Trunk[2].X + Fir.DX;
  Fir.Trunk[2].Y := Fir.Trunk[2].Y - Fir.DY;
  Fir.Trunk[3].X := Fir.Trunk[3].X - Fir.DX;
  Fir.Trunk[3].Y := Fir.Trunk[3].Y - Fir.DY;
  for I := 0 to High(Fir.Brunchs) do
    for J := 0 to High(Fir.Brunchs[I]) do
    begin
      Fir.Brunchs[I][J].X := (Fir.Brunchs[I][J].X - Fir.SP.X)*SX + Fir.SP.X;
      Fir.Brunchs[I][J].Y := Fir.SP.Y - (Fir.SP.Y - Fir.Brunchs[I][J].Y)*SY;
    end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  if State then
  begin
    State := False;
    Timer1.Enabled := False;
    Button1.Caption := 'Старт';
  end
  else
  begin
    if BState then
    begin
      State := True;
      Button2.OnClick(Sender);
    end
    else
    begin
      State := True;
      Button1.Caption := 'Стоп';
      Timer1.Enabled := True;
    end;
  end;
end;

procedure TForm1.Button2Click(Sender: TObject);
var
  I: Integer;
  X: Real;
  W: Real;
  FC, Per: Integer;
begin
  BState := False;

  Timer1.Enabled := False;
  Image1.Canvas.Pen.Color := BgClr;
  Image1.Canvas.Brush.Color := Image1.Canvas.Pen.Color;
  Image1.Canvas.Rectangle(0, 0, Image1.ClientWidth, Image1.ClientHeight);

  if not TryStrToInt(Edit2.Text, FC) then
  begin
    ShowMessage('Uncorrect numer of firs');
    Timer1.Enabled := False;
    Exit;
  end;
  if (FC <= 0) or (FC > 30) then
  begin
    SHowMessage('Firs count must be > 0 and <= 30');
    Timer1.Enabled := False;
    Exit;
  end;
  SetLength(Firs, FC);

  if not TryStrToInt(Edit1.Text, Per) then
  begin
    ShowMessage('Uncorrect numer of firs');
    Exit;
  end;
  if (Per <= 0) or (Per >= 100) then
  begin
    SHowMessage('Firs count must be > 0');
    Exit;
  end;
  TotalTime := Round(1000*100/Per);
  GrowthTime := Round(TotalTime/Length(Firs));
  Time := 0;
  CurFir := 0;

  W := image1.ClientWidth/Length(Firs);
  X := 0;
  for I := 0 to High(Firs) do
  begin
    InitFir(Firs[I]);
    Firs[I].BoundBox.LT.X := X + 10;
    Firs[I].BoundBox.RB.X := X + W - 10;
    Firs[I].BoundBox.LT.Y := 10 + Random(20);
    Firs[I].BoundBox.RB.Y := Image1.ClientHeight - 10 - Random(25);
    Firs[I].BrunchColor := BrunchsColors[Random(5)];
    Firs[I].TrunkColor := TrunksColors[Random(5)];
    Firs[I].Center := RealPoint((Firs[I].BoundBox.RB.X - Firs[I].BoundBox.LT.X)/2,
                                (Firs[I].BoundBox.RB.Y - Firs[I].BoundBox.LT.Y)/2);
    Firs[I].SP := RealPoint(X + W/2, Firs[I].BoundBox.RB.Y);
    Firs[I].DX := 0.25*(Firs[I].BoundBox.RB.X - Firs[I].BoundBox.LT.X - 4)/(GrowthTime/Timer1.Interval)/2;
    Firs[I].DY := 0.3 *(Firs[I].BoundBox.RB.Y - Firs[I].BoundBox.LT.Y - 5)/(GrowthTime/Timer1.Interval);
    SetPos(Firs[I], RealPoint(X + W/2, Firs[I].BoundBox.RB.Y));
    X := X + W;
  end;

  if State then
  begin
    Timer1.Enabled := True;
    Button1.Caption := 'Стоп';
  end
  else
    Button1.Caption := 'Старт';
end;

procedure TForm1.DrawFir(const Fir: TFir);
var
  Points: array of TPoint;
  I: Integer;
begin
  with Image1.Canvas do
  begin
    Pen.Color := BgClr;
    Brush.Color := Pen.Color;
    Rectangle(Round(Fir.BoundBox.LT.X), Round(Fir.BoundBox.LT.Y),
      Round(Fir.BoundBox.RB.X), Round(Fir.BoundBox.RB.Y));

    Pen.Color := Fir.TrunkColor;
    Brush.Color := Fir.TrunkColor;
    SetLength(Points, Length(Fir.Trunk));
    for I := 0 to High(Fir.Trunk) do
      Points[I] := Point(Round(Fir.Trunk[I].X), Round(Fir.Trunk[I].Y));
    Polygon(Points);

    Pen.Color := Fir.BrunchColor;
    Brush.Color := Fir.BrunchColor;
    SetLength(Points, Length(Fir.Brunchs[0]));
    for I := 0 to High(Fir.Brunchs[0]) do
      Points[I] := Point(Round(Fir.Brunchs[0][I].X), Round(Fir.Brunchs[0][I].Y));
    Polygon(Points);

    SetLength(Points, Length(Fir.Brunchs[1]));
    for I := 0 to High(Fir.Brunchs[1]) do
      Points[I] := Point(Round(Fir.Brunchs[1][I].X), Round(Fir.Brunchs[1][I].Y));
    Polygon(Points);

    SetLength(Points, Length(Fir.Brunchs[2]));
    for I := 0 to High(Fir.Brunchs[2]) do
      Points[I] := Point(Round(Fir.Brunchs[2][I].X), Round(Fir.Brunchs[2][I].Y));
    Polygon(Points);

    SetLength(Points, Length(Fir.Brunchs[3]));
    for I := 0 to High(Fir.Brunchs[3]) do
      Points[I] := Point(Round(Fir.Brunchs[3][I].X), Round(Fir.Brunchs[3][I].Y));
    Polygon(Points);
  end;
end;

procedure TForm1.Edit1Change(Sender: TObject);
begin
  Image1.Canvas.Pen.Color := BgClr;
  Image1.Canvas.Brush.Color := Image1.Canvas.Pen.Color;
  Image1.Canvas.Rectangle(0, 0, Image1.ClientWidth, Image1.ClientHeight);

  Timer1.Enabled := False;
  State := False;
  BState := True;
  Button1.Caption := 'Старт';
end;

procedure TForm1.Edit2Change(Sender: TObject);
begin
  Image1.Canvas.Pen.Color := BgClr;
  Image1.Canvas.Brush.Color := Image1.Canvas.Pen.Color;
  Image1.Canvas.Rectangle(0, 0, Image1.ClientWidth, Image1.ClientHeight);

  Timer1.Enabled := False;
  State := False;
  BState := True;
  Button1.Caption := 'Старт';
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  if Time >= GrowthTime then
  begin
    Time := 0;
    Inc(CurFir);
    if CurFir = Length(Firs) then
    begin
      Timer1.Enabled := False;
      BState := True;
      State := False;
      Button1.Caption := 'Старт';
      Exit;
    end;
  end;
  DrawFir(Firs[CurFir]);
  AddScale(Firs[CurFir]);
  Time := Time + Timer1.Interval;
end;

end.
