unit Graphic;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    Label5: TLabel;
    cmbFunc1: TComboBox;
    Label1: TLabel;
    Label2: TLabel;
    edtLb: TEdit;
    edtRb: TEdit;
    ckbGrid: TCheckBox;
    edtGridHorz: TEdit;
    UpDown1: TUpDown;
    Label3: TLabel;
    Label4: TLabel;
    edtGridVert: TEdit;
    UpDown2: TUpDown;
    Label6: TLabel;
    Label7: TLabel;
    cmbFunc2: TComboBox;
    Label10: TLabel;
    Label11: TLabel;
    pnlGrp1Cl: TPanel;
    pnlBgCl: TPanel;
    ColorDialog1: TColorDialog;
    Button1: TButton;
    Label12: TLabel;
    pnlGridCl: TPanel;
    ckbScale: TCheckBox;
    Label8: TLabel;
    pnlGrp2Cl: TPanel;
    Panel1: TPanel;
    imgGraphic: TImage;
    RadioGroup1: TRadioGroup;
    procedure FormCreate(Sender: TObject);
    procedure ckbGridClick(Sender: TObject);
    procedure pnlGrp1ClClick(Sender: TObject);
    procedure pnlBgClClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure pnlGridClClick(Sender: TObject);
    procedure pnlGrp2ClClick(Sender: TObject);
    procedure ckbScaleClick(Sender: TObject);
    procedure edtLbExit(Sender: TObject);
    procedure cmbFunc2Change(Sender: TObject);
    procedure edtGridHorzChange(Sender: TObject);
    procedure RadioGroup1Click(Sender: TObject);
    procedure FormActivate(Sender: TObject);
  private
    procedure DrawGraphic;
  public
    { Public declarations }
  end;

const
  Functions: array[0..3] of string = ('X^2-4',
                                      'Sin(X)*Cos(4X)',
                                      'e^(-x^2/2)/sqrt(2*PI)',
                                      'sqrt(1-x^2/9)*5');

var
  Form1: TForm1;

implementation

{$R *.dfm}

function F(Value: Extended; Number: Byte): Extended;
begin
  case Number of
    0: Result := Sqr(Value)-4;
    1: Result := Sin(Value)*cos(4*Value);
    2: Result := Exp(-Value*Value/2)/sqrt(2*PI);
    3: Result := Sqrt(1-Sqr(Value)/9)*5;
  end;
end;

function D2F(Value: Extended; Number: Byte): Extended;
begin
  case Number of
    0: Result := 2;
    1: Result := cos(Value)*cos(4*Value)-4*sin(Value)*sin(4*Value);
    2: Result := -(1/2)*Exp(-(1/2)*Sqr(Value))*Value*sqrt(2)/sqrt(PI);
    3: Result := -(5/3)*Value/sqrt(9-Sqr(Value));
  end;
end;

procedure SearchRootSecant(Number: Byte; A, B: Extended; var Error: Byte;
  var X: Extended);
const
  MaxIt: Integer = 50;
  Precision: Extended = 1e-3;
var
  T: Extended;
  I: Integer;
  C: Extended;
  Iterations: Word;
begin
  if F(A, Number) * F(B, Number) > 0 then
  begin
    Error := 2;
    Exit;
  end;
  if F(A, Number) = 0 then
  begin
    X := A;
    Error := 1;
    Exit;
  end;

  if F(B, Number) * D2F(B, Number) < 0 then
  begin
    T := B;
    B := A;
    A := T;
  end;

  I := 1;
  while (Abs(F(A, Number)-F(B, Number))>Precision) and (I < MaxIt) do
  begin
        I := I+1;
        C := B-F(B, Number)*(B-A)/(F(B, Number)-F(A, Number));
        A := B;
        B := C;
  end;

  if I <= MaxIt then
    Error := 0
  else
    Error := 3;
  Iterations := I;

  X := B;

end;

procedure TForm1.DrawGraphic;
var
  LB, RB: Real;
  Min, Max, Min1, Max1 : Real;
  Step, GridStep: Real;
  X, FX: Real;
  Number, Number1: Integer;
  SY, SY1: Real;
  I: Integer;
  GridStepHorz, GridStepVert: Integer;
  Error: Byte;
  W, H: Integer;
  S: string;
  Root: Extended;
  T: Integer;

begin

  Number := cmbFunc1.ItemIndex;
  Number1 := cmbFunc2.ItemIndex-1;
  LB := StrToFloat(edtLb.Text);
  RB := StrToFloat(edtRb.Text);
  Step := (RB - LB)/imgGraphic.ClientWidth;
  Min := F(LB, Number);
  Max := Min;
  X := LB;
  if Number1 = -1 then
  begin
    while X < RB do
    begin
      X := X + Step;
      FX := F(X, Number);
      if FX < Min then
        Min := FX
      else
      if FX > Max then
        Max := FX;
    end;
    SY := imgGraphic.ClientHeight/(Max - Min);
    SY1 := SY;
  end
  else
  begin
    Min1 := F(LB, Number1);
    Max1 := Min1;
    while X < RB do
    begin
      X := X + Step;
      FX := F(X, Number);
      if FX < Min then
        Min := FX
      else
      if FX > Max then
        Max := FX;
      FX := F(X, Number1);
      if FX < Min1 then
        Min1 := FX
      else
      if FX > Max1 then
        Max1 := FX;
    end;
    if ckbScale.Checked then
    begin
      if Max1 > Max then
        Max := Max1
      else
        Max1 := Max;
      if Min1 < Min then
        Min := Min1
      else
        Min1 := Min;
      SY := imgGraphic.ClientHeight/(Max - Min);
      SY1 := SY;
    end
    else
    begin
      SY := imgGraphic.ClientHeight/(Max - Min);
      SY1 := imgGraphic.ClientHeight/(Max1 - Min1);
    end;
  end;

  GridStepHorz := StrToInt(edtGridHorz.Text);
  GridStepVert := StrToInt(edtGridVert.Text);

  H := 0;
  W := 0;

  with imgGraphic.Canvas do
  begin
    Pen.Color := pnlBgCl.Color;
    Brush.Color := pnlBgCl.Color;
    Rectangle(0, 0, Width, Height);
    Form1.Canvas.Pen.Color := clBlack;
    if ckbGrid.Checked then
    begin
    GridStep := Step*GridStepHorz;
    Form1.Refresh;
    Form1.Canvas.Font.Color := clBlack;

    I := 0;
    X := LB;

    if RB*LB <= 0 then
    begin
      Pen.Color := clBlack;
      Pen.Width := 2;
      T := Round(-Lb*imgGraphic.ClientWidth/(Rb-Lb));
      MoveTo(T,0);
      LineTo(T, imgGraphic.ClientHeight);
    end;
    Pen.Color := pnlGridCl.Color;
    Pen.Width := 1;

    if Max*Min <= 0 then
    begin
      Pen.Color := clBlack;
      Pen.Width := 2;
      T := Round(-Min*SY) + 20;
      MoveTo(0, imgGraphic.ClientWidth - T);
      LineTo(imgGraphic.ClientWidth, imgGraphic.ClientWidth - T);
    end;
    Pen.Color := pnlGridCl.Color;
    Pen.Width := 1;

    while I <= imgGraphic.ClientWidth do
    begin
      MoveTo(I, 0);
      LineTo(I, imgGraphic.ClientHeight);
      S := FloatToStrF(X, ffFixed, 3, 2);
      Form1.Canvas.TextOut(Panel1.Left + I - Form1.Canvas.TextWidth(S) shr 1 + 3,
        Panel1.Top + Panel1.Height + 5, S);
      I := I + GridStepHorz;
      X := X + GridStep;
    end;
    H := Form1.Canvas.TextHeight(S);

    GridStep := GridStepVert/SY;
    Form1.Canvas.Font.Color := pnlGrp1Cl.Color;
    FX := Min;
    I := 0;
    while I <= imgGraphic.ClientHeight do
    begin
      MoveTo(0 , imgGraphic.ClientHeight - I);
      LineTo(imgGraphic.ClientWidth, imgGraphic.ClientHeight - I);
      S := FloatToStrF(FX, ffFixed, 3, 2);
      Form1.Canvas.TextOut(Panel1.Left - Form1.Canvas.TextWidth(S) - 5,
         Panel1.Top + Panel1.ClientHeight - H shr 1 - I - 3, S);
      I := I + GridStepVert;
      FX := FX + GridStep;
      if Form1.Canvas.TextWidth(S) > W then
        W := Form1.Canvas.TextWidth(S);
    end;
    H := H + 5;
    W := Form1.Canvas.TextWidth(S);

    if Number1 <> -1 then
    begin
      GridStep := GridStepVert/SY1;
      Form1.Canvas.Font.Color := pnlGrp2Cl.Color;
      FX := Min1;
      I := 0;
      while I <= imgGraphic.ClientHeight do
      begin
        MoveTo(0 , imgGraphic.ClientHeight - I);
        LineTo(imgGraphic.ClientWidth, imgGraphic.ClientHeight - I);
        S := FloatToStrF(FX, ffFixed, 3, 2);
        Form1.Canvas.TextOut(Panel1.Left + Panel1.Width + 5,
          Panel1.Top + Panel1.ClientHeight - Form1.Canvas.TextHeight(S) shr 1 - I - 3, S);
        I := I + GridStepVert;
        FX := FX + GridStep;
      end;
    end;
    end;

    //H := 18;
    //W := 28;
    Form1.Canvas.Font.Color := clBlack;
    Form1.Canvas.Font.Style := [fsBold];
    Text := FloatToStrF(LB, ffFixed, 3, 2);
    Form1.Canvas.TextOut(Panel1.Left - TextWidth(Text) shr 2 - 6,
      Panel1.Top + Panel1.Height + {H} + 5, Text);
    Text := FloatToStrF(RB, ffFixed, 3, 2);
    Form1.Canvas.TextOut(Panel1.Left + Panel1.ClientWidth - W shr 1,
      Panel1.Top + Panel1.Height + {H} + 5, FloatToStrF(RB, ffFixed, 3, 2));

    Text := FloatToStrF(Min, ffFixed, 3, 2);
    Form1.Canvas.Font.Color := pnlGrp1Cl.Color;
    Form1.Canvas.TextOut(Panel1.Left {- W} - Form1.Canvas.TextWidth(Text) - 5,
      Panel1.Top + Panel1.ClientHeight - 9, Text);
    Text := FloatToStrF(Max, ffFixed, 3, 2);
    Form1.Canvas.TextOut(Panel1.Left {- W}  - Form1.Canvas.TextWidth(Text) - 5,
      Panel1.Top, Text);

    if Number1 <> -1 then
    begin
      Form1.Canvas.Font.Color := pnlGrp2Cl.Color;
      Text := FloatToStrF(Min1, ffFixed, 3, 2);
      Form1.Canvas.TextOut(Panel1.Left + Panel1.Width + 5,
      Panel1.Top + Panel1.ClientHeight - 9, Text);
      Text := FloatToStrF(Max1, ffFixed, 3, 2);
      Form1.Canvas.TextOut(Panel1.Left + Panel1.Width + 5,
      Panel1.Top, Text);
    end;
    Form1.Canvas.Font.Style := [];

    X := LB;
    Pen.Color := pnlGrp1Cl.Color;
    Pen.Width := 2;
    MoveTo(0, imgGraphic.ClientHeight-Round((F(X, Number)-Min)*SY));
    for I := 1 to imgGraphic.ClientWidth do
    begin
      SearchRootSecant(Number, X, X + Step, Error, Root);
      X := X + Step;
      LineTo(I, imgGraphic.ClientHeight-Round((F(X, Number)-Min)*SY));
      if Error <> 2 then
      begin
        Pen.Width := 1;
        Pen.Style := psDot;
        //Pen.Mode := pmXor;
        Pen.Color := clMaroon;
        MoveTo(I, 0);
        LineTo(I, ImgGraphic.ClientHeight);
        MoveTo(I, imgGraphic.ClientHeight-Round((F(X, Number)-Min)*SY));
        Pen.Width := 2;
        Pen.Color := pnlGrp1Cl.Color;
        Pen.Style := psSolid;
        //Pen.Mode := pmCopy;
      end;
    end;
    
    Form1.Caption := 'Построение графиков';
    if Number1 = -1 then
      Exit;

    if Max1*Min1 <= 0 then
    begin
      Pen.Color := clBlack;
      Pen.Width := 2;
      T := Round(-Min1*SY1) + 20;
      MoveTo(0, imgGraphic.ClientWidth - T);
      LineTo(imgGraphic.ClientWidth, imgGraphic.ClientWidth - T);
    end;
    Pen.Color := pnlGridCl.Color;
    Pen.Width := 1;

    X := LB;
    Pen.Color := pnlGrp2Cl.Color;
    Pen.Width := 2;
    MoveTo(0, imgGraphic.ClientHeight-Round((F(X, Number1)-Min1)*SY1));
    for I := 1 to imgGraphic.ClientWidth do
    begin
      SearchRootSecant(Number1, X, X + Step, Error, Root);
      X := X + Step;
      LineTo(I, imgGraphic.ClientHeight-Round((F(X, Number1)-Min1)*SY1));
      if Error <> 2 then
      begin
        Pen.Width := 1;
        Pen.Style := psDot;
        Pen.Color := clNavy;
        MoveTo(I, 0);
        LineTo(I, ImgGraphic.ClientHeight);
        MoveTo(I, imgGraphic.ClientHeight-Round((F(X, Number1)-Min1)*SY1));
        Pen.Width := 2;
        Pen.Color := pnlGrp2Cl.Color;
        Pen.Style := psSolid;
      end;
    end;

  end;

end;

procedure TForm1.edtGridHorzChange(Sender: TObject);
begin
  DrawGraphic;
end;

procedure TForm1.edtLbExit(Sender: TObject);
begin
  DrawGraphic;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  DrawGraphic;
end;

procedure TForm1.ckbGridClick(Sender: TObject);
begin
  if ckbGrid.Checked then
  begin
    edtGridHorz.Enabled := True;
    edtGridVert.Enabled := True;
  end
  else
  begin
    edtGridHorz.Enabled := False;
    edtGridVert.Enabled := False;
  end;
end;

procedure TForm1.ckbScaleClick(Sender: TObject);
begin
  DrawGraphic;
end;

procedure TForm1.cmbFunc2Change(Sender: TObject);
begin
  DrawGraphic;
  Form1.Caption := 'Построение графиков';
end;

procedure TForm1.FormActivate(Sender: TObject);
begin
  Button1.Click;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  I: Integer;
begin
  cmbFunc2.Items.Add('');
  for I := 0 to High(Functions) do
  begin
    cmbFunc1.Items.Add(Functions[I]);
    cmbFunc2.Items.Add(Functions[I]);
  end;
  cmbFunc1.ItemIndex := 0;
  cmbFunc2.ItemIndex := 0;
  RadioGroup1.ItemIndex := 0;

end;

procedure TForm1.pnlBgClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlBgCl.Color := ColorDialog1.Color;
    DrawGraphic
  end;
end;

procedure TForm1.pnlGridClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlGridCl.Color := ColorDialog1.Color;
    DrawGraphic
  end;
end;

procedure TForm1.pnlGrp1ClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlGrp1Cl.Color := ColorDialog1.Color;
    DrawGraphic
  end;
end;

procedure TForm1.pnlGrp2ClClick(Sender: TObject);
begin
  if ColorDialog1.Execute then
  begin
    pnlGrp2Cl.Color := ColorDialog1.Color;
    DrawGraphic
  end;
end;

procedure TForm1.RadioGroup1Click(Sender: TObject);
begin
  case RadioGroup1.ItemIndex of
    0: ckbScale.Checked := True;
    1: ckbScale.Checked := False;
  end;
  DrawGraphic;
  Form1.Caption := 'Построение графиков';
end;

end.
