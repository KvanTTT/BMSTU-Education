unit Points;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Grids, ExtCtrls, StdCtrls, Buttons;

type
  TForm1 = class(TForm)
    imgCanvas: TImage;
    stgPoints: TStringGrid;
    BitBtn1: TBitBtn;
    BitBtn2: TBitBtn;
    Panel1: TPanel;
    Label1: TLabel;
    Label2: TLabel;
    pnlPntCl: TPanel;
    pnlBgCl: TPanel;
    ColorDialog1: TColorDialog;
    BitBtn3: TBitBtn;
    Button1: TButton;
    GroupBox1: TGroupBox;
    Label3: TLabel;
    Label4: TLabel;
    StringGrid1: TStringGrid;
    GroupBox2: TGroupBox;
    Label5: TLabel;
    Label6: TLabel;
    Label8: TLabel;
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
  private
    procedure DrawAllPoints;
    procedure MakeTask№2;
  public
    { Public declarations }
  end;

type
  TPoint = record
    X, Y: Integer;
  end;

const
  R: Integer = 3;
  E = 1E-9;

var
  Form1: TForm1;
  PointsAr: array of TPoint;
  SelRow: Integer;

implementation

{$R *.dfm}

procedure TForm1.BitBtn2Click(Sender: TObject);
var
  I: Integer;
begin
  if stgPoints.Cells[0, SelRow] <> '' then
  begin
    if MessageDlg('Вы действительн хоите удалить выделенную точку?', mtConfirmation,
      [mbYes, mbNo], 0) = mrYes
    then
    begin
      for I := SelRow - 1 to High(PointsAr) - 1 do
      begin
        PointsAr[I].X := PointsAr[I+1].X;
        PointsAr[I].Y := PointsAr[I+1].Y;
        stgPoints.Cells[1, I+1] := stgPoints.Cells[1, I+2];
        stgPoints.Cells[2, I+1] := stgPoints.Cells[2, I+2];
      end;
      SetLength(PointsAr, Length(PointsAr)-1);
      stgPoints.RowCount := stgPoints.RowCount - 1;
      stgPoints.Cells[0, stgPoints.RowCount-1] := '';
      stgPoints.Cells[1, stgPoints.RowCount-1] := '';
      stgPoints.Cells[2, stgPoints.RowCount-1] := '';
      DrawAllPoints;
      MakeTask№2;
    end;
  end;
end;

procedure TForm1.BitBtn3Click(Sender: TObject);
begin
  stgPoints.RowCount := 2;
  stgPoints.Cells[0, 1] := '';
  stgPoints.Cells[1, 1] := '';
  stgPoints.Cells[2, 1] := '';
  SetLength(PointsAr, 0);
  with imgCanvas.Canvas do
  begin
    Pen.Color := pnlBgCl.Color;
    Brush.Color := pnlBgCl.Color;
    Rectangle(0, 0, Width, Height);
  end;
  Label8.Caption := '';
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
    for I := 0 to High(PointsAr) do
      Ellipse(PointsAr[I].X-R, PointsAr[I].Y-R, PointsAr[I].X+R, PointsAr[I].Y+R);
  end;
end;

procedure TForm1.MakeTask№2;
function TriangleSquare(X1, Y1, X2, Y2, X3, Y3: Integer): Real;
var
  A1, A2, B1, B2: Integer;
begin
  A1 := X2 - X1;
  A2 := Y2 - Y1;
  B1 := X3 - X1;
  B2 := Y3 - Y1;
  Result := Abs(A1*B2 - A2*B1);
end;

function PointInTriangle(X, Y, X1, Y1, X2, Y2, X3, Y3: Integer): Boolean;
begin
  if TriangleSquare(X, Y, X1, Y1, X2, Y2) +
     TriangleSquare(X, Y, X1, Y1, X3, Y3) +
     TriangleSquare(X, Y, X2, Y2, X3, Y3) -
     TriangleSquare(X1, Y1, X2, Y2, X3, Y3) < E
  then
    Result := True
  else
    Result := False;
end;

var
  I, J, K: Integer;
  HP: Integer;
  A, B, C: Integer;
  OverCount, IntoCount: Integer;
  Diff, MinDiff: Integer;
  L: Integer;

begin
  A := -1;
  MinDiff := 20;
  HP := High(PointsAr);
  for I := 0 to HP-2 do
  begin
    for J := I+1 to HP-1 do
    begin
      for K := J+1 to HP do
      begin
        IntoCount := 0;
        OverCount := 0;
        for L := 0 to HP do
        begin
          if (L <> I) and (L <> J) and (L <> K) then
          begin
            if PointInTriangle(PointsAr[L].X, PointsAr[L].Y,
              PointsAr[I].X, PointsAr[I].Y, PointsAr[J].X, PointsAr[J].Y,
              PointsAr[K].X, PointsAr[K].Y)
            then
              Inc(IntoCount)
            else
              Inc(OverCount);
          end;
        end;
        Diff := Abs(IntoCount - OverCount);
        if (Diff < MinDiff) then
        begin
            A := I;
            B := J;
            C := K;
            MinDiff := Diff;
        end
      end;
    end;
  end;

  if A = -1 then
  begin
    StringGrid1.Cells[1, 1] := '';
    StringGrid1.Cells[2, 1] := '';
    StringGrid1.Cells[1, 2] := '';
    StringGrid1.Cells[2, 2] := '';
    StringGrid1.Cells[1, 3] := '';
    StringGrid1.Cells[2, 3] := '';
    Exit;
  end;
  with imgCanvas.Canvas do
  begin
    Pen.Width := 3;
    Pen.Color := pnlPntCl.Color;
    MoveTo(PointsAr[A].X, PointsAr[A].Y);
    LineTo(PointsAr[B].X, PointsAr[B].Y);
    LineTo(PointsAr[C].X, PointsAr[C].Y);
    LineTo(PointsAr[A].X, PointsAr[A].Y);
    StringGrid1.Cells[1, 1] := IntToStr(PointsAr[A].X);
    StringGrid1.Cells[2, 1] := IntToStr(PointsAr[A].Y);
    StringGrid1.Cells[1, 2] := IntToStr(PointsAr[B].X);
    StringGrid1.Cells[2, 2] := IntToStr(PointsAr[B].Y);
    StringGrid1.Cells[1, 3] := IntToStr(PointsAr[C].X);
    StringGrid1.Cells[2, 3] := IntToStr(PointsAr[C].Y);
    Label8.Caption := 'Разность ' + IntToStr(MinDiff);
  end;

end;

procedure TForm1.BitBtn1Click(Sender: TObject);
label
  1, 2;
var
  S: string;
  X, Y: Integer;
  L, L1: Integer;
begin
  1:
  if InputQuery('Добавление точки', 'Введите координату Х', S) then
  begin
    if TryStrToInt(S, X) then
    begin
      if (X < 0) or (X > imgCanvas.ClientWidth) then
      begin
        ShowMessage('X должен быть больше 0 и меньше ' +
          IntToStr(imgCanvas.ClientWidth));
        goto 1;
      end;
    end
    else
    begin
      ShowMessage('Неправильный ввод');
      goto 1;
    end;
  end
  else
    Exit;

  S := '';
  2:
  if InputQuery('Добавление точки', 'Введите координату Y', S) then
  begin
    if TryStrToInt(S, Y) then
    begin
      if (Y < 0) or (Y > imgCanvas.ClientHeight) then
      begin
        ShowMessage('Y должен быть больше 0 и меньше ' +
          IntToStr(imgCanvas.ClientHeight));
        goto 2;
      end;
    end
    else
    begin
      ShowMessage('Неправильный ввод');
      goto 2;
    end;
  end
  else
    Exit;

  if stgPoints.RowCount = 22 then
  begin
    ShowMessage('Максимальное количество точек - 20');
    Exit;
  end;
  L := Length(PointsAr);
  SetLength(PointsAr, L+1);
  PointsAr[L].X := X;
  PointsAr[L].Y := Y;
  L1 := stgPoints.RowCount-1;
  stgPoints.Cells[0, L1] := IntToStr(L1);
  stgPoints.Cells[1, L1] := IntToStr(X);
  stgPoints.Cells[2, L1] := IntToStr(Y);
  stgPoints.RowCount := stgPoints.RowCount + 1;
  DrawAllPoints;
  MakeTask№2;

end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  stgPoints.Cells[0, 0] := '№';
  stgPoints.Cells[1, 0] := 'X';
  stgPoints.Cells[2, 0] := 'Y';
  StringGrid1.Cells[0, 0] := '№';
  StringGrid1.Cells[1, 0] := 'X';
  StringGrid1.Cells[2, 0] := 'Y';
  StringGrid1.Cells[0, 1] := '1';
  StringGrid1.Cells[0, 2] := '2';
  StringGrid1.Cells[0, 3] := '3';
  DrawAllPoints;
end;

procedure TForm1.imgCanvasMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
begin
  Label3.Caption := 'X: ' + IntToStr(X);
  Label4.Caption := 'Y: ' + IntToStr(Y);
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
  if stgPoints.RowCount = 22 then
  begin
    ShowMessage('Максимальное количество точек - 20');
    Exit;
  end;
  L := Length(PointsAr);
  SetLength(PointsAr, L+1);
  PointsAr[L].X := X;
  PointsAr[L].Y := Y;
  L1 := stgPoints.RowCount-1;
  stgPoints.Cells[0, L1] := IntToStr(L1);
  stgPoints.Cells[1, L1] := IntToStr(X);
  stgPoints.Cells[2, L1] := IntToStr(Y);
  stgPoints.RowCount := stgPoints.RowCount + 1;
  DrawAllPoints;
  MakeTask№2;
  end
  else
  if Button = mbRight then
  begin
    SqrR := 4*Sqr(R);
    for J := 0 to High(PointsAr) do
      if (Sqr(X-PointsAr[J].X) + Sqr(Y-PointsAr[J].Y)) <= SqrR then
      begin
      for I := J to High(PointsAr) - 1 do
      begin
        PointsAr[I].X := PointsAr[I+1].X;
        PointsAr[I].Y := PointsAr[I+1].Y;
        stgPoints.Cells[1, I+1] := stgPoints.Cells[1, I+2];
        stgPoints.Cells[2, I+1] := stgPoints.Cells[2, I+2];
      end;
      SetLength(PointsAr, Length(PointsAr)-1);
      stgPoints.RowCount := stgPoints.RowCount - 1;
      stgPoints.Cells[0, stgPoints.RowCount-1] := '';
      stgPoints.Cells[1, stgPoints.RowCount-1] := '';
      stgPoints.Cells[2, stgPoints.RowCount-1] := '';
      DrawAllPoints;
      MakeTask№2;
      Break;
      end;
  end;
end;

procedure TForm1.pnlBgClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlBgCl.Color := ColorDialog1.Color;
    DrawAllPoints;
    MakeTask№2;
  end;
end;

procedure TForm1.pnlPntClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlPntCl.Color := ColorDialog1.Color;
    DrawAllPoints;
    MakeTask№2;
  end;
end;

procedure TForm1.stgPointsSelectCell(Sender: TObject; ACol, ARow: Integer;
  var CanSelect: Boolean);
begin
  SelRow := ARow;
end;

end.
