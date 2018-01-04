unit Algorithm;

interface

uses Windows, Graphics, extctrls, Math;

type _function = function(X, Y : Real): Real;

const
  NO_VALUE  : Integer = -2147483647;
  UpColor   = $0043DE6A;
  DownColor = $00E4A843;
  MAX_COUNT = 639;

procedure draw_surface(Image : TImage; X1, Y1, X2, Y2 : Real; F : _function; Fmin, Fmax : Real; N1, N2 : Integer;
  Phi, Psi, Eta, Ax, Bx, Ay, By : Real);
function F1(X, Y : Real) : Real;
function F2(X, Y : Real) : Real;
function F3(X, Y : Real) : Real;

implementation

var
  Y_Max, Y_Min : array [0 .. MAX_COUNT] of Integer;

function F1(X, Y : Real) : Real;
begin
  F1 := 1.5 * Sin(3 * X) * sin (2 * Y) * Cos(X * Y);
end;

function F2(X, Y : Real) : Real;
begin
  F2 := 2 * Cos(X) - Sin(X) * Cos(Y);
end;

function F3(X, Y : Real) : Real;
begin
  //F3 :=  Cos(X*Y);
  F3 := Sin(X + Exp(Y)) - Cos(Y)*Sinh(X);
end;

procedure init;
  var I : Word;
begin
  for I := 0 to MAX_COUNT do begin
    Y_MAX[I] := NO_VALUE;
    Y_MIN[I] := NO_VALUE;
  end;
end;

procedure line_brez(P1, P2 : TPoint; HDC : THandle);
// процерура построени€ линии по модифицированному алгоритму Ѕрезенхема
  var Dx, Dy, Sx, Sy : Integer;
      X, Y, I : Integer;
      D, D1, D2 : Integer;
      M1, M2 : Integer;
begin
  Dx := Abs(P2.X - P1.X);
  Dy := Abs(P2.Y - P1.Y);
  if P2.X >= P1.X then Sx := 1 else Sx := -1;
  if P2.Y >= P1.Y then Sy := 1 else Sy := -1;
  if Dy <= Dx then  begin
    D := -Dx;
    D1 := Dy shl 1;
    D2 := (Dy - Dx) shl 1;
    X := P1.X;
    Y := P1.Y;
    // цикл построени€, модифицирующий верхний и нижний горизонты
    for I := 0 to Dx do begin
      if Y_MIN[X] = NO_VALUE then begin
        SetPixel(HDC, X, Y, UpColor);
        Y_MIN[X] := Y;
        Y_MAX[X] := Y;
      end
      else if Y < Y_MIN[X] then begin
        SetPixel(HDC, X, Y, UpColor);
        Y_MIN[X] := Y;
      end
      else if Y > Y_MAX[X] then begin
        SetPixel(HDC, X, Y, DownColor);
        Y_MAX[X] := Y;
      end;
      if D > 0 then begin
        D := D + D2;
        Y := Y + Sy;
      end
      else D := D + D1;
      X := X + Sx;
    end;
  end
  else begin
    D := -Dy;
    D1 := Dx shl 1;
    D2 := (Dx - Dy) shl 1;
    M1 := Y_MIN[P1.X];
    M2 := Y_MAX[P1.X];
    X := P1.X;
    Y := P1.Y;
    for I := 0 to Dy do begin
      if Y_MIN[X] = NO_VALUE then begin
        SetPixel(HDC, X, Y, UpColor);
        //HDC.CAnvas.Pixels[X,Y] := UpColor;
        Y_MIN[X] := Y;
        Y_MAX[X] := Y;
      end
      else if Y < M1 then begin
        SetPixel(HDC, X, Y, UpColor);
        //HDC.Canvas.Pixels[X,Y] := UpColor;
        if Y < Y_MIN[X] then Y_MIN[X] := Y;
      end
      else if Y > M2 then begin
        SetPixel(HDC, X, Y, DownColor);
        //HDC.Canvas.Pixels[X, Y] := DownColor;
        if Y > Y_MAX[X] then Y_MAX[X] := Y;
      end;
      if D > 0 then begin
        D := D + D2;
        X := X + Sx;
        M1 := Y_MIN[X];
        M2 := Y_MAX[X];
      end
      else D := D + D1;
      Y := Y + Sy;
    end;
  end;
end;

procedure draw_surface(Image : TImage; X1, Y1, X2, Y2 : Real; F : _function; Fmin, Fmax : Real; N1, N2 : Integer;
Phi, Psi, Eta, Ax, Bx, Ay, By : Real);
                 // построение поверхности
  var
     sPhi, cPhi, sPsi, cPsi, sEta, cEta : Real;       // синусы и косинусы углов поворота
     E1, E2 : array [0..3] of Real;       // массивы преобразовани€ координат
     X, Y,                                // текущие координаты точки
     Hx, Hy : Real;                       // шаги по ос€м
     I, J, K : Integer;                   // параметры циклов
     curLine, nextLine : array of TPoint; // массивы предыдущей и текущей линий
     T : real;
begin
  cPhi := Cos(Phi);
  sPhi := Sin(Phi);
  sPsi := Sin(Psi);
  cPSi := Cos(Psi);
  sEta := Sin(Eta);
  cEta := Cos(Eta);

  E1[0] := cPhi * cEta;
  E1[1] := sPhi;
  E1[2] := -cPhi * sEta;

  E2[0] := cEta*sPsi*sPhi + sEta*cPsi;
  E2[1] := -sPsi*cPhi;
  E2[2] := -sPsi*sPhi*sEta + cPsi*cEta;

  Hx := (X2 - X1) / N1;
  Hy := (Y2 - Y1) / N2;

  // инициализируем данные
  init;
  SetLength(curLine, N1);
  SetLength(nextLine, N1);

  // строим линии
  Y := Y1 + (N2 - 1) * Hy;
  for I := 0 to N1 - 1 do begin
    X := X1 + I * Hx;
    T := F(x,Y);
    curLine[I].X := Round(Ax + Bx * (X * E1[0] + Y * E1[1] + F(X,Y)*E1[2]));
    curLine[I].Y := Round(Ay + By * (X * E2[0] + Y * E2[1] + F(X,Y)*E2[2]));
  end;
  for I := N2 - 1 downto 0 do begin
    for J := 0 to N1 - 2 do line_brez(curLine[J], CurLine[J + 1], Image.Canvas.Handle);
    Y := Y1 + (I - 1) * Hy;
    if I > 0 then
      for J := 0 to N1 - 1 do begin
        X := X1 + J * Hx;
        nextLine[J].X := Round(Ax + Bx * (X * E1[0] + Y * E1[1] + F(X,Y)*E1[2]));
        nextLine[J].Y := Round(Ay + By * (X * E2[0] + Y * E2[1] + F(X,Y)*E2[2]));
        line_brez(curLine[J], nextLine[J], Image.Canvas.Handle);
        curLine[J] := nextLine[J];
      end;
  end;
  nextLine := nil;
  curLine := nil;
end;

end.
