unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, CheckLst, Grids;

type
  TVector = array of Integer;
  TMatrix = array of TVector;
  TBoolVector = array of Boolean;
  TBoolMatrix = array of TBoolVector;

  TGraph = class
    Size: Integer;
    AdjMatrix: TMatrix;
    ReachMatrix: TMatrix;
    Strongly, Connected, Weakly: Boolean;
    function LoadFromFile(FileName: string): Boolean;
    procedure Connectivity;
    procedure Draw(const Image: TImage);
  end;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Panel1: TPanel;
    lbcConnect: TCheckListBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Button1: TButton;
    OpenDialog1: TOpenDialog;
    StringGrid1: TStringGrid;
    StringGrid2: TStringGrid;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure lbcConnectClickCheck(Sender: TObject);
  private
    { Private declarations }
  public
    Graph: TGraph;
    Strongly, Connected, Weakly: Boolean;
    procedure LoadGraph(FileName: string);
  end;

var
  Form1: TForm1;

const
  Zero: Integer = 2147483647;
  Identity: Integer = 0;

  RadiusRatio: Real = 0.15;
  Curve: Real = 0.1;

  VertexesColors: array[0..8] of TColor =
    ($400080, $E60073, $6C00D9, $717100, $00329B, $228C37, $054E58, $4A4526, $0657FF);

function SRPlus(A, B: Integer): Integer; overload;
function SRMult(A, B: Integer): Integer; overload;
function SRMinus(A, B: Integer): Integer;
function SRDiv(A, B: Integer): Integer;

function SRPlus(A, B: Boolean): Boolean; overload;
function SRMult(A, B: Boolean): Boolean; overload;

implementation

{$R *.dfm}

function SRPlus(A, B: Integer): Integer;
begin
  if A < B then
    Result := A
  else
    Result := B;
end;

function SRMinus(A, B: Integer): Integer;
begin
  if A > B then
    Result := A
  else
    Result := B;
end;

function SRPlus(A, B: Boolean): Boolean;
begin
  Result := A and B;
end;

function SRMult(A, B: Boolean): Boolean;
begin
  Result := A or B;
end;

function SRMult(A, B: Integer): Integer;
begin
  Result := A + B;
  if Result < 0 then
    Result := Zero;
end;

function SRDiv(A, B: Integer): Integer;
var
  R: Int64;
begin
  Result := A - B;
  if Result < 0 then
    Result := 0;
end;

procedure TForm1.Button1Click(Sender: TObject);
var
  I: Integer;
  J: Integer;
  Str: string;
begin
  if OpenDialog1.Execute then
  begin
    Graph.Free;
    LoadGraph(OpenDialog1.FileName);
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  I, J: Integer;
  Str: string;
begin
  OpenDialog1.InitialDir := Application.ExeName;
  LoadGraph('Graph1.txt');
end;

procedure TForm1.lbcConnectClickCheck(Sender: TObject);
begin
    if Graph.Weakly then
      lbcConnect.Checked[0] := True
    else
      lbcConnect.Checked[0] := False;
    if Graph.Connected then
      lbcConnect.Checked[1] := True
    else
      lbcConnect.Checked[1] := False;
    if Graph.Strongly then
      lbcConnect.Checked[2] := True
    else
      lbcConnect.Checked[2] := False;
end;

procedure TForm1.LoadGraph(FileName: string);
var
  I, J: Integer;
begin
  Graph := TGraph.Create;
  Graph.LoadFromFile(FileName);
  Graph.Connectivity;
  Graph.Draw(Image1);
  lbcConnect.Checked[0] := False;
  lbcConnect.Checked[1] := False;
  lbcConnect.Checked[2] := False;
  if Graph.Weakly then
    lbcConnect.Checked[0] := True;
  if Graph.Connected then
    lbcConnect.Checked[1] := True;
  if Graph.Strongly then
    lbcConnect.Checked[2] := True;

  StringGrid1.ColCount := Graph.Size+1;
  StringGrid1.RowCount := StringGrid1.ColCount;
  StringGrid2.ColCount := StringGrid1.ColCount;
  StringGrid2.RowCount := StringGrid2.ColCount;
  for I := 0 to Graph.Size - 1 do
  begin
    StringGrid1.Cells[I+1, 0] := IntToStr(I+1);
    StringGrid1.Cells[0, I+1] := StringGrid1.Cells[I+1, 0];
    StringGrid2.Cells[I+1, 0] := StringGrid1.Cells[I+1, 0];
    StringGrid2.Cells[0, I+1] := StringGrid1.Cells[I+1, 0];
    for J := 0 to Graph.Size - 1 do
    begin
      if Graph.AdjMatrix[I, J] = Zero then
        StringGrid1.Cells[J+1, I+1] := '¤'
      else
        StringGrid1.Cells[J+1, I+1] := IntToStr(Graph.AdjMatrix[I, J]) + ' ';
      if Graph.ReachMatrix[I, J] = Zero then
        StringGrid2.Cells[J+1, I+1] := '¤'
      else
        StringGrid2.Cells[J+1, I+1] := IntToStr(Graph.ReachMatrix[I, J]) + ' ';
    end;
  end;
  Form1.Caption := 'Открыт граф ' + ExtractFileName(FileName);
end;

procedure TGraph.Connectivity;

procedure Copy(const FromV: TVector; var ToV: TVector); overload;
var
  I: Integer;
  H: Integer;
begin
  H := High(FromV);
  for I := 0 to H do
    ToV[I] := FromV[I];
end;

procedure Copy(const FromV: TBoolVector; var ToV: TBoolVector); overload;
var
  I: Integer;
  H: Integer;
begin
  H := High(FromV);
  for I := 0 to H do
    ToV[I] := FromV[I];
end;

procedure Nulling(var Ar: TVector);
var
  I: Integer;
begin
  for I := 0 to High(Ar) do
    Ar[I] := Zero;
end;

var
  I, J, K, L: Integer;
  Ind, Ind1, Ind2, Ind3: Integer;
  High: Integer;
  T: TVector;
  TB: TBoolVector;
  M: TMatrix;
  MB: TBoolMatrix;
  ReachMatrixB: TBoolMatrix;
  AdjMatrixB: TBoolMatrix;
  VCount: Integer;

begin
  High := Size-1;
  SetLength(ReachMatrix, Size, Size);
  SetLength(ReachMatrixB, Size, Size);
  SetLength(AdjMatrixB, Size, Size);
  SetLength(M, Size, Size);
  SetLength(MB, Size, Size);

  M[0, 0] := Identity;
  MB[0, 0] := True;
  AdjMatrixB[0, 0] := True;
  for I := 1 to High do
  begin
    for J := 0 to I-1 do
    begin
      M[I, J] := AdjMatrix[I, J];
      M[J, I] := AdjMatrix[J, I];
      if (M[I, J] <> Zero) then
      begin
        AdjMatrixB[I, J] := True;
        MB[I, J] := True;
        MB[J, I] := True;
      end;
      if (M[J, I] <> Zero) then
      begin
        AdjMatrixB[J, I] := True;
        MB[I, J] := True;
        MB[J, I] := True;
      end;
    end;
    M[I, I] := Identity;
    MB[I, I] := True;
    AdjMatrixB[I, I] := True;
  end;

// Bool Matrix Iteration. It's nesassary for check on weakly connectivity
  SetLength(TB, Size);
  Copy(MB[0], TB);
  Copy(MB[High], ReachMatrixB[High]);
  for J := 1 to High do
  begin
    for K := 0 to J-1 do
      for L := 0 to High do
        MB[J, L] := SRPlus(SRMult(MB[K, L], AdjMatrixB[J, K]), MB[J, L]);
  end;
  Copy(TB, MB[0]);
  Copy(MB[High], TB);
  Copy(ReachMatrixB[High], MB[High]);
  Copy(TB, ReachMatrixB[High]);

  for I := 1 to High do
  begin
    Copy(MB[I], TB);
    Copy(MB[I-1], ReachMatrixB[I-1]);
    Ind := I;
    for J := 1 to High do
    begin
      Ind := I+J;
      if Ind > High then
        Ind := Ind - High - 1;
      Ind2 := Ind-1;
      if Ind2 = -1 then
        Ind2 := High;
      for K := 0 to J-1 do
      begin
        Ind3 := K+Ind2;
        if Ind3 > High then
          Ind3 := Ind3 - High - 1;
        for L := 0 to High do
          MB[Ind, L] := SRPlus(SRMult(MB[Ind3, L], AdjMatrixB[Ind, Ind3]), MB[Ind, L]);
      end;
    end;
    Copy(TB, MB[I]);
    Copy(MB[I-1], TB);
    Copy(ReachMatrixB[I-1], MB[I-1]);
    Copy(TB, ReachMatrixB[I-1]);
  end;

{  for I := 1 to High do
    for J := 0 to I-1 do
      if not MB[I, J] then
        Exit; }
  Weakly := True;

//  Calculating Iteration of AdjMatrix and finding ReachMatrix
//  RM = AM* = 1 + AM + AM^2 + AM^3 + ...
  SetLength(T, Size);
  Copy(M[0], T);
  Copy(M[High], ReachMatrix[High]);
  Nulling(M[0]);
  M[0, 0] := Identity;
  for J := 1 to High do
  begin
    for K := 0 to J-1 do
      for L := 0 to High do
        M[J, L] := SRPlus(SRMult(M[K, L], AdjMatrix[J, K]), M[J, L]);
  end;
  Copy(T, M[0]);
  Copy(M[High], T);
  Copy(ReachMatrix[High], M[High]);
  Copy(T, ReachMatrix[High]);

  for I := 1 to High do
  begin
    Copy(M[I], T);
    Copy(M[I-1], ReachMatrix[I-1]);
    Nulling(M[I]);
    M[I, I] := Identity;
    Ind := I;
    for J := 1 to High do
    begin
      Ind := I+J;
      if Ind > High then
        Ind := Ind - High - 1;
      Ind2 := Ind-1;
      if Ind2 = -1 then
        Ind2 := High;
      for K := 0 to J-1 do
      begin
        Ind3 := K+Ind2;
        if Ind3 > High then
          Ind3 := Ind3 - High - 1;
        for L := 0 to High do
          M[Ind, L] := SRPlus(SRMult(M[Ind3, L], AdjMatrix[Ind, Ind3]), M[Ind, L]);
      end;
    end;
    Copy(T, M[I]);
    Copy(M[I-1], T);
    Copy(ReachMatrix[I-1], M[I-1]);
    Copy(T, ReachMatrix[I-1]);
  end;

  for I := 1 to High do
  begin
    for J := 0 to I-1 do
      if ((ReachMatrix[I, J] = Zero) and (ReachMatrix[J, I] = Zero)) then
        Exit;
  end;
  Connected := True;

  for I := 1 to High do
    for J := 0 to I-1 do
      if ((ReachMatrix[I, J] = Zero) or (ReachMatrix[J, I] = Zero)) then
        Exit;
  Strongly := True;
end;

procedure TGraph.Draw(const Image: TImage);

type TVector2 = record
  X, Y: Real;
end;

function Vector2(X, Y: Real): TVector2;
begin
  Result.X := X;
  Result.Y := Y;
end;

procedure Norm(var V: TVector2);
var
  R: Real;
begin
  R := 1/Sqrt(V.X*V.X + V.Y*V.Y);
  V.X := V.X*R;
  V.Y := V.Y*R;
end;

procedure Perp(var V: TVector2);
var
  T: Real;
begin
  T := -V.Y;
  V.Y := V.X;
  V.X := T;
end;

Function FindOuterRadius(A, B, C : TVector2; Var Rr : TVector2): Boolean;
Var M : Array[1..2,1..3] Of Extended;
    D, Dx, Dy : Extended;
Begin
 M[1, 1] := 2 * (A.X - B.X);
 M[1, 2] := 2 * (A.Y - B.Y);
 M[1, 3] := Sqr(A.X) +Sqr(A.Y) - (Sqr(B.X) + Sqr(B.Y));

 M[2, 1] := 2 * (B.X - C.X);
 M[2, 2] := 2 * (B.Y - C.Y);
 M[2, 3] := Sqr(B.X) +Sqr(B.Y) - (Sqr(C.X) + Sqr(C.Y));

 D := M[1, 1] * M[2, 2] - M[2, 1] * M[1, 2];
 Dx := M[1, 3] * M[2, 2] - M[2, 3] * M[1, 2];
 Dy := M[1, 1] * M[2, 3] - M[2, 1] * M[1, 3];

 If D <> 0 Then
  Begin
   Rr.X := Dx/D;
   Rr.Y := Dy/D;
   FindOuterRadius := True;
  End Else
  Begin
   Rr.X := 0;
   Rr.Y := 0;
   FindOuterRadius := False;
  End;
End;

procedure RadnomizeArray(var Ar: array of Integer);
var
  T: Integer;
  I: Integer;
  IR: Integer;
begin
  Randomize;
  for I := 0 to Length(Ar) div 2 do
  begin
    IR := Random(Length(Ar));
    T := Ar[I];
    Ar[I] := Ar[IR];
    Ar[IR] := T;
  end;
end;


var
  XStep, YStep: Integer;
  X, Y: Integer;
  XC, YC: Integer;
  I, J, K: Integer;
  Ind1, Ind2, C, C1: Integer;
  RV: Integer;
  P1, P2, P3, O: TVector2;
  RT: Real;
  TW2, TH2: Integer;
  ColorsInd: array of Integer;
  Count: Integer;
  A: Real;

begin
  YC := Round(Sqrt(Size));
  XC := Round(Size/YC + 0.499999999999);
  XStep := Image.ClientWidth div XC;
  YStep := Image.ClientHeight div YC;

  RV := Round(XStep*RadiusRatio);

  with Image.Canvas do
  begin

  Brush.Color := $DDF0FB;
  Pen.Color := Brush.Color;
  Rectangle(0, 0, Image.ClientWidth, Image.ClientHeight);

  Randomize;
  Pen.Width := 1;
  Font.Size := 13;
  Font.Style := [fsBold];
  Brush.Color := $DDF0FB;

  SetLength(ColorsInd, Size);
  for I := 0 to Size - 1 do
    ColorsInd[I] := I mod Length(VertexesColors);
  RadnomizeArray(ColorsInd);
  C := 0;
  Ind1 := 0;
  Ind2 := 0;
  Count := 0;
  for K := 0 to Size - 1 do
  begin
    Pen.Color := VertexesColors[ColorsInd[K]];
    Font.Color := Pen.Color;

    C := 0;
    C1 := 0;
    Y := YStep div 2;
    for I := 0 to YC-1 do
    begin
      X := XStep div 2;
      for J := 0 to XC-1 do
      begin
        if (AdjMatrix[K, C] <> Zero) then
        begin
          if (K = C) then
          begin
            if AdjMatrix[K, C] <> Identity then
            begin
              Pen.Width := 1;
              Arc(Round(XStep*(Ind1 + 0.5)-RV), Round(YStep*(Ind2 + 0.5)-2*RV),
                Round(XStep*(Ind1 + 0.5)+RV), Round(YStep*(Ind2 + 0.5)),
                Round(XStep*(Ind1 + 0.5)-RV), Round(YStep*(Ind2 + 0.5)-2*RV),
                Round(XStep*(Ind1 + 0.5)-RV), Round(YStep*(Ind2 + 0.5)-2*RV));
              TextOut(Round(XStep*(Ind1 + 0.5)), Round(YStep*(Ind2 + 0.5)-2*RV), IntToStr(AdjMatrix[K, C]));
            end;
          end
          else
          begin
            P2 := Vector2(XStep*(Ind1 + 0.5), YStep*(Ind2 + 0.5));
            P1 := Vector2(X, Y);
            P3 := Vector2(P2.X-P1.X, P2.Y-P1.Y);
            Perp(P3);
            P3.X := (P1.X+P2.X)/2 + P3.X*Curve;
            P3.Y := (P1.Y+P2.Y)/2 + P3.Y*Curve;
            FindOuterRadius(P1, P2, P3, O);
            RT := Sqrt(Sqr(P1.X-O.X) + Sqr(P1.Y-O.Y));
            Pen.Width := 1;
            Arc(Round(O.X-RT), Round(O.Y-RT), Round(O.X+RT), Round(O.Y+RT),
              Round(P1.X), Round(P1.Y), Round(P2.X), Round(P2.Y));
            Pen.Width := 5;
            A := 4/5;
            Arc(Round(O.X-RT), Round(O.Y-RT), Round(O.X+RT), Round(O.Y+RT),
              Round(P1.X), Round(P1.Y), Round((A*P3.X + P1.X)/(1+A)), Round((A*P3.Y + P1.Y)/(1+A)));
            TextOut(Round(P3.X), Round(P3.Y), IntToStr(AdjMatrix[K, C]));
          end;
        end;
        X := X + XStep;
        Inc(C);
        if C = Size then
          Break;
      end;
      Y := Y + YStep;
    end;

    Inc(Ind1);
    if Ind1 = XC then
    begin
      Ind1 := 0;
      Inc(Ind2);
    end;
  end;


  Pen.Width := 1;
  Font.Color := $ACF3F9;
  Font.Size := 13;
  Font.Style := [fsBold];
    C := 1;
    Y := YStep div 2;
    for I := 0 to YC-1 do
    begin
      X := XStep div 2;
      for J := 0 to XC-1 do
      begin
        Brush.Color := VertexesColors[ColorsInd[C-1]];
        Ellipse(X-RV, Y-RV, X+RV, Y+RV);
        TW2 := TextWidth(IntToStr(C)) div 2;
        TH2 := TextHeight(IntToStr(C)) div 2;
        TextOut(X-TW2, Y-TH2, IntToStr(C));
        X := X + XStep;
        if C = Size then
          Exit;
        Inc(C);
      end;
      Y := Y + YStep;
    end;

  end;
end;

function TGraph.LoadFromFile(FileName: string): Boolean;
var
  F: Text;
  Str: string;
  I, J: Integer;
  L: Integer;
  I1, I2: Integer;
begin
  Result := False;
  if not FileExists(FileName) then
    Exit;
  AssignFile(F, FileName);
  Reset(F);

  ReadLn(F, Size);
  SetLength(AdjMatrix, Size, Size);
  I1 := 0;
  while not SeekEOF(F) do
  begin
    I2 := 0;
    Read(F, Str);
    L := Length(Str);
    I := 1;
    while not (Str[I] in ['-', '+', '0'..'9']) do
      Inc(I);

    while I <= L do
    begin
      J := I;
      while (Str[I] in ['-', '+', '0'..'9']) do
        Inc(I);
      if not TryStrToInt(Copy(Str, J, I-J), AdjMatrix[I1, I2]) then
        Exit;
      if AdjMatrix[I1, I2] < 0 then
        AdjMatrix[I1, I2] := Zero;
      Inc(I2);

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['-', '+', '0'..'9'];
    end;
    Inc(I1);
  end;
  CloseFile(F);


end;

end.
