unit Points;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Grids, ExtCtrls, StdCtrls, Buttons;

type
  TForm1 = class(TForm)
    stgPoints: TStringGrid;
    BitBtn1: TBitBtn;
    BitBtn2: TBitBtn;
    Label1: TLabel;
    Label2: TLabel;
    pnlPntCl: TPanel;
    pnlBgCl: TPanel;
    ColorDialog1: TColorDialog;
    BitBtn3: TBitBtn;
    sgDecision: TStringGrid;
    GroupBox2: TGroupBox;
    Label5: TLabel;
    Label6: TLabel;
    pnlPntCl2: TPanel;
    BitBtn4: TBitBtn;
    Label7: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    ScrollBox1: TScrollBox;
    imgCanvas: TImage;
    Label3: TLabel;
    Label4: TLabel;
    Label8: TLabel;
    Edit1: TEdit;
    Button1: TButton;
    Label13: TLabel;
    Label14: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure imgCanvasMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure pnlPntClClick(Sender: TObject);
    procedure pnlBgClClick(Sender: TObject);
    procedure BitBtn2Click(Sender: TObject);
    procedure stgPointsSelectCell(Sender: TObject; ACol, ARow: Integer;
      var CanSelect: Boolean);
    procedure BitBtn3Click(Sender: TObject);
    procedure imgCanvasMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure pnlPntCl2Click(Sender: TObject);
    procedure BitBtn4Click(Sender: TObject);
    procedure Edit1Exit(Sender: TObject);
    procedure Edit1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    procedure DrawAllPoints;
    procedure MakeTask;
  public
    { Public declarations }
  end;

type
  TDoublePoint = record
    X, Y: Double;
  end;
  
  TCircle = record
    Center: TDoublePoint;
    Radius: Double;
  end;

const
  R: Integer = 3;
  MaxPoints: Integer = 40;

var
  Form1: TForm1;
  PointsAr1: array of TDoublePoint;
  PointsAr2: array of TDoublePoint;
  SelRow, SelCol: Integer;
  ZoomCoef: Double = 1.1;
  OriginPoint: TDoublePoint;
  DCount: Integer;
  DN: Integer = 0;

implementation

{$R *.dfm}

function MakeCircle(const P1, P2, P3: TDoublePoint): TCircle;
var
  A1, A2, B1, B2, C1, C2: Double;
  S: Double;
begin
  A1 := P3.X-P1.X;
  A2 := P3.X-P2.X;
  B1 := P3.Y-P1.Y;
  B2 := P3.Y-P2.Y;
  C1 := -0.5*(A1*(P3.X+P1.X) + B1*(P3.Y+P1.Y));
  C2 := -0.5*(A2*(P3.X+P2.X) + B2*(P3.Y+P2.Y));
  S := A1*B2 - A2*B1;
  if S = 0 then
    Result.Radius := 0
  else
  begin
    S := 1/S;
    Result.Center.Y := (A2*C1 - A1*C2)*S;
    Result.Center.X := (B1*C2 - B2*C1)*S;
    Result.Radius := Sqrt(Sqr(Result.Center.X-P1.X) + Sqr(Result.Center.Y-P1.Y));
  end;
end;

function Distance(const P1, P2: TDoublePoint): Double; inline;
begin
  Result := Sqrt(Sqr(P2.X-P1.X) + Sqr(P2.Y-P1.Y));
end;

function DistanceSqr(const P1, P2: TDoublePoint): Double; inline;
begin
  Result := Sqr(P2.X-P1.X) + Sqr(P2.Y-P1.Y);
end;

function InsideTangents(const C1, C2: TCircle; var A1, B1, A2, B2: TDoublePoint): Boolean;
var
  M: TDoublePoint;
  N: TDoublePoint;
  D: Double;
  T1, T2, T3, T4: Double;
  SinA, CosA: Double;
  L: Double;
  B: Double;
begin
  D := Distance(C1.Center, C2.Center);
  if D = 0 then
  begin
    Result := false;
    Exit;
  end;
  T1 := 1/D;
  N.X := C2.Radius*(C1.Center.X - C2.Center.X)*T1 + C2.Center.X;
  N.Y := C2.Radius*(C1.Center.Y - C2.Center.Y)*T1 + C2.Center.Y;
  T1 := 1/(C1.Radius + C2.Radius);
  M.X := (C2.Center.X*C1.Radius + C1.Center.X*C2.Radius)*T1;
  M.Y := (C2.Center.Y*C1.Radius + C1.Center.Y*C2.Radius)*T1;
  B := C2.Radius*T1*D;
  L := B*B - Sqr(C2.Radius);
  if L < 0 then
  begin
    Result := false;
    Exit;
  end;
  T1 := 1/B;
  SinA := Sqrt(L)*T1;
  CosA := C2.Radius*T1;

  T1  := (N.X - C2.Center.X)*CosA + C2.Center.X;
  T2 := (N.Y - C2.Center.Y)*CosA + C2.Center.Y;
  T3 := (-N.Y + C2.Center.Y)*SinA;
  T4 := (N.X - C2.Center.X)*SinA;

  B1.X := T1 + T3;
  B1.Y := T2 + T4;

  B2.X := T1 - T3;
  B2.Y := T2 - T4;

  T1 := C1.Radius/C2.Radius;

  A1.X := (M.X - B1.X)*T1 + M.X;
  A1.Y := (M.Y - B1.Y)*T1 + M.Y;

  A2.X := (M.X - B2.X)*T1 + M.X;
  A2.Y := (M.Y - B2.Y)*T1 + M.Y;

  Result := True;
end;

procedure TForm1.BitBtn2Click(Sender: TObject);
var
  I: Integer;
begin
  if stgPoints.Cells[SelCol, SelRow] <> '' then
  begin
    if MessageDlg('Вы действительн хоите удалить выделенную точку?', mtConfirmation,
      [mbYes, mbNo], 0) = mrYes
    then
    begin
      if SelCol = 1 then
      begin
        for I := SelRow-1 to High(PointsAr1) - 1 do
        begin
          PointsAr1[I].X := PointsAr1[I+1].X;
          PointsAr1[I].Y := PointsAr1[I+1].Y;
          stgPoints.Cells[SelCol, I+1] := stgPoints.Cells[SelCol, I+2];
        end;
        stgPoints.Cells[SelCol, Length(PointsAr1)] := '';
        SetLength(PointsAr1, Length(PointsAr1)-1);
      end
      else
      begin
        for I := SelRow-1 to High(PointsAr2) - 1 do
        begin
          PointsAr2[I].X := PointsAr2[I+1].X;
          PointsAr2[I].Y := PointsAr2[I+1].Y;
          stgPoints.Cells[SelCol, I+1] := stgPoints.Cells[SelCol, I+2];
        end;
        stgPoints.Cells[SelCol, Length(PointsAr2)] := '';
        SetLength(PointsAr2, Length(PointsAr2)-1);
      end;
      DrawAllPoints;
      MakeTask;
    end;
  end;
end;

procedure TForm1.BitBtn3Click(Sender: TObject);
var
  I: Integer;
begin
  for I := 1 to MaxPoints do
  begin
    stgPoints.Cells[1, I] := '';
    stgPoints.Cells[2, I] := '';
  end;
  SetLength(PointsAr1, 0);
  SetLength(PointsAr2, 0);
  with imgCanvas.Canvas do
  begin
    Pen.Color := pnlBgCl.Color;
    Brush.Color := pnlBgCl.Color;
    Rectangle(0, 0, Width, Height);
  end;
  Label8.Caption := '';
  sgDecision.RowCount := 2;
  sgDecision.Cells[0, sgDecision.RowCount-1] := '';
  sgDecision.Cells[1, sgDecision.RowCount-1] := '';
  sgDecision.Cells[2, sgDecision.RowCount-1] := '';
  sgDecision.Cells[3, sgDecision.RowCount-1] := '';
  sgDecision.Cells[4, sgDecision.RowCount-1] := '';
  sgDecision.Cells[5, sgDecision.RowCount-1] := '';
  sgDecision.Cells[6, sgDecision.RowCount-1] := '';
end;

procedure TForm1.BitBtn4Click(Sender: TObject);
var
  S: string;
  X, Y: Double;
  L, L1: Integer;
  I: Integer;
begin
  if Length(PointsAr2) = MaxPoints then
  begin
    ShowMessage('Максимальное количество точек в множестве - MaxPoints');
    Exit;
  end;

  while True do
    begin
    if InputQuery('Добавление точки', 'Введите координату Х', S) then
    begin
      if TryStrToFloat(S, X) then
        Break
      else
        ShowMessage('Неправильный ввод');
    end
    else
      Exit;
  end;

  S := '';
  while True do
    begin
    if InputQuery('Добавление точки', 'Введите координату Y', S) then
    begin
      if TryStrToFloat(S, Y) then
        Break
      else
        ShowMessage('Неправильный ввод');
    end
    else
      Exit;
  end;

  for I := 0 to High(PointsAr2) do
    if (X = PointsAr2[I].X) and (Y = PointsAr2[I].Y) then
    begin
      ShowMessage('Точка с такими координатами уже существует');
      Exit;
    end;

  L := Length(PointsAr2);
  SetLength(PointsAr2, L+1);
  PointsAr2[L].X := X;
  PointsAr2[L].Y := Y;
  stgPoints.Cells[2, L+1] := '(' + FloatToStr(X) + '; ' + FloatToStr(Y) + ')';
  DrawAllPoints;
  MakeTask;
end;

procedure TForm1.Button1Click(Sender: TObject);
procedure GetCoords(const S: string; var P: TDoublePoint);
var
  I, J: Integer;
begin
  I := 2;
  while S[I] <> ';' do
    Inc(I);
  P.X := StrToFloat(Copy(S, 2, I-2));
  Inc(I);
  P.Y := StrToFloat(Copy(S, I, Length(S)-I));
end;

var
  Offset: Integer;
  P1, P2, P3, M1, M2, M3: TDoublePoint;
  XMin, YMin, XMax, YMax: Double;
  CoefX, CoefY: Double;
  C1, C2: TCircle;
  A1, A2, B1, B2: TDoublePoint;
  Coef: Real;
  I: Integer;
  XT, YT: Double;
  GR: TGridRect;

begin
  Offset := 10;

  if DCount = 0 then
  begin
    if (Length(PointsAr1) < 3) or (Length(PointsAr2) < 3)then
      ShowMessage('Невозможно решить задачу!' + #13 + 'В каждом множестве должно быть >= 3 точек')
    else
      ShowMessage('Нет решений');
    Exit;
  end;

  if sgDecision.Cells[1,1] <> '' then
  begin
    Inc(DN);
    if DN = sgDecision.RowCount-1 then
      DN := 1;

    GetCoords(sgDecision.Cells[1,DN], P1);
    GetCoords(sgDecision.Cells[2,DN], P2);
    GetCoords(sgDecision.Cells[3,DN], P3);
    GetCoords(sgDecision.Cells[4,DN], M1);
    GetCoords(sgDecision.Cells[5,DN], M2);
    GetCoords(sgDecision.Cells[6,DN], M3);
    P1.Y := ImgCanvas.ClientHeight - P1.Y;
    P2.Y := ImgCanvas.ClientHeight - P2.Y;
    P3.Y := ImgCanvas.ClientHeight - P3.Y;
    M1.Y := ImgCanvas.ClientHeight - M1.Y;
    M2.Y := ImgCanvas.ClientHeight - M2.Y;
    M3.Y := ImgCanvas.ClientHeight - M3.Y;    

    C1 := MakeCircle(P1, P2, P3);
    C2 := MakeCircle(M1, M2, M3);

    XMin := C1.Center.X - C1.Radius;
    if C2.Center.X - C2.Radius < XMin then
      XMin := C2.Center.X - C2.Radius;

    YMin := C1.Center.Y - C1.Radius;
    if C2.Center.Y - C2.Radius < YMin then
      YMin := C2.Center.Y - C2.Radius;

    XMax := C1.Center.X + C1.Radius;
    if C2.Center.X + C2.Radius > XMax then
      XMax := C2.Center.X + C2.Radius;

    YMax := C1.Center.Y + C1.Radius;
    if C2.Center.Y + C2.Radius > YMax then
      YMax := C2.Center.Y + C2.Radius;

    CoefX := (imgCanvas.ClientWidth - 2*Offset)/(XMax - XMin);
    CoefY := (imgCanvas.ClientHeight - 2*Offset)/(YMax - YMin);
    if CoefX < CoefY then
      Coef := CoefX
    else
      Coef := CoefY;

    InsideTangents(C1, C2, A1, B1, A2, B2);

    C1.Center.X := (C1.Center.X - XMin)*Coef + Offset;
    C1.Center.Y := (C1.Center.Y - YMin)*Coef + Offset;
    C1.Radius := C1.Radius*Coef;
    C2.Center.X := (C2.Center.X - XMin)*Coef + Offset;
    C2.Center.Y := (C2.Center.Y - YMin)*Coef + Offset;
    C2.Radius := C2.Radius*Coef;

    P1.X := (P1.X - XMin)*Coef + Offset;
    P1.Y := (P1.Y - YMin)*Coef + Offset;
    P2.X := (P2.X - XMin)*Coef + Offset;
    P2.Y := (P2.Y - YMin)*Coef + Offset;
    P3.X := (P3.X - XMin)*Coef + Offset;
    P3.Y := (P3.Y - YMin)*Coef + Offset;

    M1.X := (M1.X - XMin)*Coef + Offset;
    M1.Y := (M1.Y - YMin)*Coef + Offset;
    M2.X := (M2.X - XMin)*Coef + Offset;
    M2.Y := (M2.Y - YMin)*Coef + Offset;
    M3.X := (M3.X - XMin)*Coef + Offset;
    M3.Y := (M3.Y - YMin)*Coef + Offset;

    with imgCanvas.Canvas do
    begin
      Pen.Color := pnlBgCl.Color;
      Brush.Color := pnlBgCl.Color;
      Rectangle(0, 0, Width, Height);
      Pen.Style := psSolid;
      Pen.Width := 1;
      Pen.Color := pnlPntCl.Color;
      Ellipse(Round(C1.Center.X-C1.Radius),
        Round(C1.Center.Y-C1.Radius),
        Round(C1.Center.X+C1.Radius),
        Round(C1.Center.Y+C1.Radius));
      Pen.Color := pnlPntCl2.Color;
      Ellipse(Round(C2.Center.X-C2.Radius),
        Round(C2.Center.Y-C2.Radius),
        Round(C2.Center.X+C2.Radius),
        Round(C2.Center.Y+C2.Radius));

      Brush.Color := pnlPntCl.Color;
      Pen.Color := Brush.Color;
      Pen.Width := 1;
      for I := 0 to High(PointsAr1) do
      begin
        XT := (PointsAr1[I].X - XMin)*Coef + Offset;
        YT := (PointsAr1[I].Y - YMin)*Coef + Offset;
          Ellipse(Round(XT-R), Round(YT-R),
            Round(XT+R), Round(YT+R));
      end;
      Brush.Color := pnlPntCl2.Color;
      Pen.Color := Brush.Color;
      Pen.Width := 1;
      for I := 0 to High(PointsAr2) do
      begin
        XT := (PointsAr2[I].X - XMin)*Coef + Offset;
        YT := (PointsAr2[I].Y - YMin)*Coef + Offset;
          Ellipse(Round(XT-R), Round(YT-R),
            Round(XT+R), Round(YT+R));
      end;

      Brush.Color := pnlBgCl.Color;
      if Abs(B1.X - A1.X) < StrToFloat(Edit1.Text) then
      begin
        A1.X := (A1.X - XMin)*Coef + Offset;
        A1.Y := (A1.Y - YMin)*Coef + Offset;
        B1.X := (B1.X - XMin)*Coef + Offset;
        B1.Y := (B1.Y - YMin)*Coef + Offset;
        Pen.Color := RGB(136, 51, 0);
        Pen.Style := psDash;
        MoveTo(Round(A1.X), Round(A1.Y));
        LineTo(Round(B1.X), Round(B1.Y));
      end
      else
      begin
        A2.X := (A2.X - XMin)*Coef + Offset;
        A2.Y := (A2.Y - YMin)*Coef + Offset;
        B2.X := (B2.X - XMin)*Coef + Offset;
        B2.Y := (B2.Y - YMin)*Coef + Offset;
        Pen.Color := RGB(136, 51, 0);
        Pen.Style := psDash;
        MoveTo(Round(A2.X), Round(A2.Y));
        LineTo(Round(B2.X), Round(B2.Y));
      end;

      GR.Left := 1;
      GR.Top := DN;
      GR.Right := 6;
      GR.Bottom := DN;

      sgDecision.Selection := GR;
    end;
  end;
end;

procedure TForm1.DrawAllPoints;
var
  I: Integer;
begin
  with imgCanvas.Canvas do
  begin
    Pen.Color := pnlBgCl.Color;
    Brush.Color := pnlBgCl.Color;
    Rectangle(0, 0, Width, Height);
    Brush.Color := pnlPntCl.Color;
    Pen.Color := Brush.Color;
    Pen.Width := 1;
    for I := 0 to High(PointsAr1) do
      Ellipse(Round(PointsAr1[I].X-R+OriginPoint.X),
      imgCanvas.ClientHeight - Round(PointsAr1[I].Y-R+OriginPoint.Y),
      Round(PointsAr1[I].X+R+OriginPoint.X),
      imgCanvas.ClientHeight - Round(PointsAr1[I].Y+R+OriginPoint.Y));
    Brush.Color := pnlPntCl2.Color;
    Pen.Color := Brush.Color;
    Pen.Width := 1;
    for I := 0 to High(PointsAr2) do
      Ellipse(Round(PointsAr2[I].X-R+OriginPoint.X),
      imgCanvas.ClientHeight - Round(PointsAr2[I].Y-R+OriginPoint.Y),
      Round(PointsAr2[I].X+R+OriginPoint.X),
      imgCanvas.ClientHeight - Round(PointsAr2[I].Y+R+OriginPoint.Y));
  end;
end;

procedure TForm1.MakeTask;
var
  I, J, K, I1, J1, K1: Integer;
  C1, C2: TCircle;
  HP1, HP2: Integer;
  A1, B1, A2, B2: TDoublePoint;
  H: Integer;
  B: Boolean;
  Eps: Double;
begin
  DCount := 0;
  sgDecision.RowCount := 2;
  sgDecision.Cells[0, 1] := '';
  sgDecision.Cells[1, 1] := '';
  sgDecision.Cells[2, 1] := '';
  sgDecision.Cells[3, 1] := '';
  sgDecision.Cells[4, 1] := '';
  sgDecision.Cells[5, 1] := '';
  sgDecision.Cells[6, 1] := '';
  if (not TryStrToFloat(Edit1.Text, Eps)) or (Eps < 0) then
  begin
    ShowMessage('Введите корректную точность > 0!');
    Exit;
  end;
  HP1 := High(PointsAr1);
  HP2 := High(PointsAr2);
  imgCanvas.Canvas.Brush.Style := bsClear;
  imgCanvas.Canvas.Pen.Width := 1;
  for I := 0 to HP1-2 do
    for J := I+1 to HP1-1 do
      for K := J+1 to HP1 do
      begin
        C1 := MakeCircle(PointsAr1[I], PointsAr1[J], PointsAr1[K]);
        if C1.Radius = 0 then
          Continue;
        for I1 := 0 to HP2-2 do
          for J1 := I1+1 to HP2-1 do
            for K1 := J1+1 to HP2 do
            begin
              C2 := MakeCircle(PointsAr2[I1], PointsAr2[J1], PointsAr2[K1]);
              if C2.Radius = 0 then
                Continue;
              B := False;
              if InsideTangents(C1, C2, A1, B1, A2, B2) then
              begin
                  if Abs(B1.X-A1.X) <= Eps then
                  with imgCanvas.Canvas do
                  begin
                    Pen.Style := psSolid;
                    Pen.Width := 1;
                    Pen.Color := pnlPntCl.Color;
                    Ellipse(Round(C1.Center.X-C1.Radius),
                      imgCanvas.ClientHeight - Round(C1.Center.Y-C1.Radius),
                      Round(C1.Center.X+C1.Radius),
                      imgCanvas.ClientHeight - Round(C1.Center.Y+C1.Radius));
                    Pen.Color := pnlPntCl2.Color;
                    Ellipse(Round(C2.Center.X-C2.Radius),
                      imgCanvas.ClientHeight - Round(C2.Center.Y-C2.Radius),
                      Round(C2.Center.X+C2.Radius),
                      imgCanvas.ClientHeight - Round(C2.Center.Y+C2.Radius));
                    Pen.Color := RGB(136, 51, 0);
                    Pen.Style := psDash;
                    MoveTo(Round(A1.X), imgCanvas.ClientHeight - Round(A1.Y));
                    LineTo(Round(B1.X), imgCanvas.ClientHeight - Round(B1.Y));
                    H := sgDecision.RowCount;
                    sgDecision.Cells[0, H-1] := IntToStr(H-1);
                    sgDecision.Cells[1, H-1] := '(' + FloatToStr(PointsAr1[I].X) + '; ' +
                      FloatToStr(PointsAr1[I].Y) + ')';
                    sgDecision.Cells[2, H-1] := '(' + FloatToStr(PointsAr1[J].X) + '; ' +
                      FloatToStr(PointsAr1[J].Y) + ')';
                    sgDecision.Cells[3, H-1] := '(' + FloatToStr(PointsAr1[K].X) + '; ' +
                      FloatToStr(PointsAr1[K].Y) + ')';
                    sgDecision.Cells[4, H-1] := '(' + FloatToStr(PointsAr2[I1].X) + '; ' +
                      FloatToStr(PointsAr2[I1].Y) + ')';
                    sgDecision.Cells[5, H-1] := '(' + FloatToStr(PointsAr2[J1].X) + '; ' +
                      FloatToStr(PointsAr2[J1].Y) + ')';
                    sgDecision.Cells[6, H-1] := '(' + FloatToStr(PointsAr2[K1].X) + '; ' +
                      FloatToStr(PointsAr2[K1].Y) + ')';
                    sgDecision.RowCount := sgDecision.RowCount + 1;
                    Inc(DCount);
                    Label14.Caption := IntToStr(DCount);
                    B := True;
                  end;
                  if Abs(B2.X-A2.X) <= Eps then
                  with imgCanvas.Canvas do
                  begin
                    Pen.Style := psSolid;
                    Pen.Width := 1;
                    Pen.Color := pnlPntCl.Color;
                    Ellipse(Round(C1.Center.X-C1.Radius),
                      imgCanvas.ClientHeight - Round(C1.Center.Y-C1.Radius),
                      Round(C1.Center.X+C1.Radius),
                      imgCanvas.ClientHeight - Round(C1.Center.Y+C1.Radius));
                    Pen.Color := pnlPntCl2.Color;
                    Ellipse(Round(C2.Center.X-C2.Radius),
                      imgCanvas.ClientHeight - Round(C2.Center.Y-C2.Radius),
                      Round(C2.Center.X+C2.Radius),
                      imgCanvas.ClientHeight - Round(C2.Center.Y+C2.Radius));
                    Pen.Color := RGB(136, 51, 0);
                    Pen.Style := psDash;
                    MoveTo(Round(A2.X), imgCanvas.ClientHeight - Round(A2.Y));
                    LineTo(Round(B2.X), imgCanvas.ClientHeight - Round(B2.Y));
                    if B = False then
                    begin
                      H := sgDecision.RowCount;
                      sgDecision.Cells[0, H-1] := IntToStr(H-1);
                      sgDecision.Cells[1, H-1] := '(' + FloatToStr(PointsAr1[I].X) + '; ' +
                        FloatToStr(PointsAr1[I].Y) + ')';
                      sgDecision.Cells[2, H-1] := '(' + FloatToStr(PointsAr1[J].X) + '; ' +
                        FloatToStr(PointsAr1[J].Y) + ')';
                      sgDecision.Cells[3, H-1] := '(' + FloatToStr(PointsAr1[K].X) + '; ' +
                        FloatToStr(PointsAr1[K].Y) + ')';
                      sgDecision.Cells[4, H-1] := '(' + FloatToStr(PointsAr2[I1].X) + '; ' +
                        FloatToStr(PointsAr2[I1].Y) + ')';
                      sgDecision.Cells[5, H-1] := '(' + FloatToStr(PointsAr2[J1].X) + '; ' +
                        FloatToStr(PointsAr2[J1].Y) + ')';
                      sgDecision.Cells[6, H-1] := '(' + FloatToStr(PointsAr2[K1].X) + '; ' +
                        FloatToStr(PointsAr2[K1].Y) + ')';
                      sgDecision.RowCount := sgDecision.RowCount + 1;
                      Inc(DCount);
                      Label14.Caption := IntToStr(DCount);
                    end;
                  end;
              end;
            end;
     end;
  sgDecision.Cells[0, sgDecision.RowCount-1] := '';
  sgDecision.Cells[1, sgDecision.RowCount-1] := '';
  sgDecision.Cells[2, sgDecision.RowCount-1] := '';
  sgDecision.Cells[3, sgDecision.RowCount-1] := '';
  sgDecision.Cells[4, sgDecision.RowCount-1] := '';
  sgDecision.Cells[5, sgDecision.RowCount-1] := '';
  sgDecision.Cells[6, sgDecision.RowCount-1] := '';
end;

procedure TForm1.Edit1Change(Sender: TObject);
begin
  DrawAllPoints;
  MakeTask;
end;

procedure TForm1.Edit1Exit(Sender: TObject);
begin
  DrawAllPoints;
  MakeTask;
end;

procedure TForm1.BitBtn1Click(Sender: TObject);
var
  S: string;
  X, Y: Double;
  L, L1: Integer;
  I: Integer;
begin
  if Length(PointsAr1) = MaxPoints then
  begin
    ShowMessage('Максимальное количество точек в множестве - MaxPoints');
    Exit;
  end;

  while True do
    begin
    if InputQuery('Добавление точки', 'Введите координату Х', S) then
    begin
      if TryStrToFloat(S, X) then
        Break
      else
        ShowMessage('Неправильный ввод');
    end
    else
      Exit;
  end;

  S := '';
  while True do
    begin
    if InputQuery('Добавление точки', 'Введите координату Y', S) then
    begin
      if TryStrToFloat(S, Y) then
        Break
      else
        ShowMessage('Неправильный ввод');
    end
    else
      Exit;
  end;

  for I := 0 to High(PointsAr1) do
    if (X = PointsAr1[I].X) and (Y = PointsAr1[I].Y) then
    begin
      ShowMessage('Точка с такими координатами уже существует');
      Exit;
    end;

  L := Length(PointsAr1);
  SetLength(PointsAr1, L+1);
  PointsAr1[L].X := X;
  PointsAr1[L].Y := Y;
  stgPoints.Cells[1, L+1] := '(' + FloatToStr(X) + '; ' + FloatToStr(Y) + ')';
  DrawAllPoints;
  MakeTask;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  I: Integer;
begin
  stgPoints.Cells[0, 0] := '№';
  stgPoints.Cells[1, 0] := '1';
  stgPoints.Cells[2, 0] := '2';
  stgPoints.RowCount := MaxPoints + 1;
  for I := 1 to MaxPoints do
    stgPoints.Cells[0, I] := IntToStr(I);
  sgDecision.Cells[0, 0] := 'Решение';
  sgDecision.Cells[1, 0] := 'A1';
  sgDecision.Cells[2, 0] := 'A2';
  sgDecision.Cells[3, 0] := 'A3';
  sgDecision.Cells[4, 0] := 'B1';
  sgDecision.Cells[5, 0] := 'B2';
  sgDecision.Cells[6, 0] := 'B3';
  DrawAllPoints;
end;

procedure TForm1.imgCanvasMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
  Label3.Caption := 'X: ' + IntToStr(X);
  Label4.Caption := 'Y: ' + IntToStr(ImgCanvas.ClientHeight - Y);
end;

procedure TForm1.imgCanvasMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
var
  L, L1: Integer;
  I, J: Integer;
  SqrR: Integer;
begin
  if Button = mbLeft then
  begin
    if not(ssCtrl in Shift) then
    begin
      if Length(PointsAr1) = MaxPoints then
      begin
        ShowMessage('Максимальное количество точек в множестве - MaxPoints');
        Exit;
      end;
      for I := 0 to High(PointsAr1) do
        if (X = PointsAr1[I].X) and (imgCanvas.ClientHeight - Y = PointsAr1[I].Y) then
        begin
          ShowMessage('Точка с такими координатами уже существует');
          Exit;
        end;
      L := Length(PointsAr1);
      SetLength(PointsAr1, L+1);
      PointsAr1[L].X := X;
      PointsAr1[L].Y := imgCanvas.ClientHeight - Y;
      stgPoints.Cells[1, L+1] := '(' + FloatToStr(X) + '; ' + FloatToStr(ImgCanvas.ClientHeight - Y) + ')';

    end
    else
    begin
      SqrR := 4*Sqr(R);

      for J := 0 to High(PointsAr1) do
        if (Sqr(X-PointsAr1[J].X) + Sqr(((imgCanvas.ClientHeight - Y) - PointsAr1[J].Y))) <= SqrR then
        begin
          for I := J to High(PointsAr1) - 1 do
          begin
            PointsAr1[I].X := PointsAr1[I+1].X;
            PointsAr1[I].Y := PointsAr1[I+1].Y;
            stgPoints.Cells[1, I+1] := stgPoints.Cells[1, I+2];
          end;
          stgPoints.Cells[1, Length(PointsAr1)] := '';
          SetLength(PointsAr1, Length(PointsAr1)-1);
          Break;
        end;

      for J := 0 to High(PointsAr2) do
        if (Sqr(X-PointsAr2[J].X) + Sqr(((imgCanvas.ClientHeight - Y) - PointsAr2[J].Y))) <= SqrR then
        begin
          for I := J to High(PointsAr2) - 1 do
          begin
            PointsAr2[I].X := PointsAr2[I+1].X;
            PointsAr2[I].Y := PointsAr2[I+1].Y;
            stgPoints.Cells[2, I+1] := stgPoints.Cells[2, I+2];
          end;
          stgPoints.Cells[2, Length(PointsAr2)] := '';
          SetLength(PointsAr2, Length(PointsAr2)-1);
          Break;
        end;
    end;
  end
  else
  if Button = mbRight then
  begin
    if Length(PointsAr2) = MaxPoints then
    begin
      ShowMessage('Максимальное количество точек в множестве - MaxPoints');
      Exit;
    end;
    for I := 0 to High(PointsAr2) do
    if (X = PointsAr2[I].X) and (imgCanvas.ClientHeight - Y = PointsAr2[I].Y) then
    begin
      ShowMessage('Точка с такими координатами уже существует');
      Exit;
    end;
    L := Length(PointsAr2);
    SetLength(PointsAr2, L+1);
    PointsAr2[L].X := X;
    PointsAr2[L].Y := imgCanvas.ClientHeight - Y;
    stgPoints.Cells[2, L+1] := '(' + FloatToStr(X) + '; ' + FloatToStr(ImgCanvas.ClientHeight - Y) + ')';
  end;
  DrawAllPoints;
  MakeTask;
end;

procedure TForm1.pnlBgClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlBgCl.Color := ColorDialog1.Color;
    DrawAllPoints;
    MakeTask;
  end;
end;

procedure TForm1.pnlPntCl2Click(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlPntCl2.Color := ColorDialog1.Color;
    DrawAllPoints;
    MakeTask;
  end;
end;

procedure TForm1.pnlPntClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlPntCl.Color := ColorDialog1.Color;
    DrawAllPoints;
    MakeTask;
  end;
end;

procedure TForm1.stgPointsSelectCell(Sender: TObject; ACol, ARow: Integer;
  var CanSelect: Boolean);
begin
  SelRow := ARow;
  SelCol := ACol;
end;

end.
