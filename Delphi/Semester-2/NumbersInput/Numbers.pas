unit Numbers;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls;

type
  Reals = array of Real;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Label1: TLabel;
    Memo1: TMemo;
    Label2: TLabel;
    Label3: TLabel;
    Edit1: TEdit;
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
    procedure Button3Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Label3Click(Sender: TObject);
  private
    { Private declarations }
  public
    function MyAlgorithm(Ar: Reals): Real;
    procedure AnalizDigit(var Str: string; var FirstPos: Word; var LastPos: Word);

  end;

var
  Form1: TForm1;
  Flag: Boolean;
  A, B: Integer;

implementation

{$R *.dfm}

// перевод на русский
function ConvStr (S: string): string;
  var
    I: Integer;
  begin
    Result:=S;
    {for I :=1 to Length(S) do
    begin
      if Ord(S[I])in[192..239] then
        Result[I] := Chr (Ord(S[I])-64)
      else
        if Ord(S[I])>239 then
        Result[I] := Chr (Ord(S[I])-16);
    end;      }
  end;

function TForm1.MyAlgorithm(Ar: Reals): Real;
type
  TMatrix = array of array of Real;

var
  N, NN: Byte;
  I, J, K: Byte;
  M: TMatrix;
  L: Integer;
  TempAr: Reals;
  P1, P2: Integer;
  SqrNN: Integer;
  S: string;

begin
  L := Length(Ar);
  if L = 0 then
  begin
    Result := 0;
    Exit;
  end;
  if L = 1 then
  begin
    Result := Ar[0];
    Exit;
  end;
  NN := Round(Sqrt(Length(Ar)) + 0.49999999999999);
  SetLength(M, NN, NN);
  SqrNN := Sqr(NN);
  SetLength(Ar, SqrNN);
  for I := SqrNN-1 downto L do
    Ar[I] := 0;

  S := '';
  K := 0;
  for I := 0 to NN - 1 do
  begin
    for J := 0 to NN - 1 do
    begin
      M[I, J] := Ar[K];
      S := S + FloatToStrF(Ar[K], ffFixed, 7, 3) + '  ';
      Inc(K);
    end;
    S := S + #13;
  end;
  Memo1.Lines.Text := S;

  K := 0;
  P1 := 0;
  P2 := NN-1;
  while P2 > P1 do
  begin
    for I := P1 to P2-1 do
    begin
      Ar[K] := M[P1, I];
      Inc(K);
    end;
    for I := P1 to P2-1 do
    begin
      Ar[K] := M[I, P2];
      Inc(K);
    end;
    for I := P2 downto P1+1 do
    begin
      Ar[K] := M[P2, I];
      Inc(K);
    end;
    for I := P2 downto P1+1 do
    begin
      Ar[K] := M[I, P1];
    Inc(K);
    end;
    Dec(P2);
    Inc(P1);
  end;
  if P2 = P1 then
  begin
    L := NN div 2;
    Ar[K] := M[L, L];
  end;

  Result := 0;

  if SqrNN mod 2 = 0 then
  begin
    for I := 0 to SqrNN div 2 - 1 do
      Result := Result + Ar[I]*Ar[SqrNN-I-1];
  end
  else
  begin
    for I := 0 to SqrNN div 2 - 1 do
      Result := Result + Ar[I]*Ar[SqrNN-I-1];
    Result := Result + Ar[(SqrNN-1) div 2];
  end;

end;

procedure TForm1.AnalizDigit(var Str: string; var FirstPos: Word; var LastPos: Word);
var
  I, J: Word;
  DoublePoint, DoubleExp: Boolean;
  Exp: Boolean;
  S1: string;

begin
  S1 := Copy(Str, FirstPos, LastPos-FirstPos+1);
  A := FirstPos;
  B := LastPos-FirstPos+1;
  if FirstPos = LastPos-1 then
    if not (Str[FirstPos]  in ['0'..'9']) then
    begin
      ShowMessage('Число: ' + S1 + ': Первый символ в числе должен быть цифрой');
      Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
      Delete(Str, FirstPos, 1);
      LastPos := FirstPos;
      Exit;
    end;

  DoubleExp := False;
  DoublePoint := False;
  Exp := False;

  I := FirstPos;
  if (Str[I] <> '+') and (Str[I] <> '-') then
  begin
    Insert('+', Str, I);
    Inc(LastPos);
  end;
  Inc(I);
  J := I;
  {while not (Str[J] in ['0'..'9']) do
    Inc(J);
  if J-I-1 - (I+1) > 0 then
    ShowMessage('Число: ' + S1 + ': Первый символ в числе должен быть цифрой');
  Delete(Str, I+1, J-I-1);  }
  while I <= LastPos do
  begin
    if Str[I] = 'E' then
    begin
      {if not(Str[I-1] in ['0'..'9']) or
        not(Str[I+1] in ['0'..'9', '+', '-'])  then
      begin
        Delete(Str, I, 1);
        Dec(LastPos);
        Continue;
      end
      else }
      begin
        if DoubleExp then
        begin
          ShowMessage('Число: ' + S1 + 'E может быть только одно!');
          Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
          Delete(Str, I, 1);
          Dec(LastPos);
          Continue;
        end;
        DoubleExp := True;
        Exp := True;
        if not(Str[I-1] in ['0'..'9']) then
        begin
          Edit1.SelStart := FirstPos;
          Edit1.SelLength := LastPos-FirstPos+1;
          ShowMessage('Число: ' + S1 + 'Перед Е должна быть цифра');
          Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
          Insert('1', Str, I);
          Inc(I);
          Inc(LastPos);
        end;
        if (Str[I+1] <> '+') and (Str[I+1] <> '-') then
        begin
          Insert('+', Str, I+1);
          I := I + 1;
          LastPos := LastPos + 1;
          if not (Str[I+1] in ['0'..'9']) then
          begin
            ShowMessage(ConvStr('Число: ' + S1 + 'После Е должна быть цифра'));
            Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
            Insert('1', Str, I+1);
            Inc(I);
            LastPos := LastPos + 1;
          end;
        end;
        Inc(I);
        Continue;
      end;
    end;
    if Str[I] = ',' then
    begin
      if Exp then
        begin
          ShowMessage('Число: ' + S1 + ' Не может быть запятой после Е');
          Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
          Delete(Str, I, 1);
          Dec(LastPos);
          Continue;
        end;
      if not(Str[I+1] in ['0'..'9']) or not(Str[I-1] in ['0'..'9']) then
      begin
        ShowMessage('Число: ' + S1 + 'Перед или после "," должна быть цифра');
        Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
        Delete(Str, I, 1);
        Dec(LastPos);
        Continue;
      end
      else
      begin
        if DoublePoint then
        begin
          ShowMessage('Число: ' + S1 + 'В числе не может быть больше 1 запятой');
          Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
          Delete(Str, I, 1);
          Dec(LastPos);
          Continue;
        end;
        DoublePoint := True;
        Inc(I);
        Continue;
      end;
    end;
    if Str[I] in  ['-', '+'] then
      if (I <> FirstPos) then
      if not(Str[I-1] = 'E') or
        not(Str[I+1] in ['0'..'9']) then
      begin
        ShowMessage('Число: ' + S1 + 'Знак "+" или "-" должнен быть после Е или перед числом, причем он единственный');
        Edit1.SetFocus;
      Edit1.SelStart := FirstPos-2;
      Edit1.SelLength := LastPos-FirstPos;
      Flag := False;
      Exit;
        Delete(Str, I, 1);
        Dec(LastPos);
        Continue;
      end;
    Inc(I);
  end;

  {if Str[I-3] = 'e' then
  begin
    Insert('1', Str, I-1);
  end;      }

  J := LastPos;
  while not (Str[J] in ['0'..'9']) do
    Dec(J);
  Delete(Str, J+1, LastPos-J-1);
  LastPos := J;

  {if Str[LastPos-1] in ['0'..'9'] then
  begin
    Delete(Str, LastPos-1, 1);
    Dec(LastPos);
  end;   }
  //Str[I] := '5';

end;

procedure TForm1.Button1Click(Sender: TObject);
var
  TempS: String;
  I, J, K: Word;
begin
  Flag := True;
  TempS := Edit1.Text;
  TempS := ' ' + TempS + ' ';
  I := 0;
  while I < Length(TempS) do
  begin
    Inc(I);
    if TempS[I] = 'e' then
      TempS[I] := 'E';
    if TempS[I] = '.' then
      TempS[I] := ',';
  end;

   I := 1;
  while I <= Length(TempS) do
  begin
    if TempS[I] <> ' ' then
    begin
      J := I;
      while TempS[J] <> ' ' do
        Inc(J);
      AnalizDigit(TempS, I, J);
      if Flag = False then
        Exit;
      I := J;
    end;
    Inc(I);
  end;

  //TempS[Length(TempS)] := #0;
  //Edit1.Text := Trim(TempS);

end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  Edit1.Text := '';
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  Close;
end;

procedure TForm1.Button4Click(Sender: TObject);
var
  TempS: String;
  I, J: Word;
  Numbers: Reals;
begin
  Button1.Click;
  if Flag = False then
    Exit;
  TempS := Edit1.Text;
  TempS := TempS + ' ';
  I := 1;
  while I <= Length(TempS) do
  begin
    if TempS[I] <> ' ' then
    begin
      J := I;
      while TempS[J] <> ' ' do
        Inc(J);
      SetLength(Numbers, Length(Numbers) + 1);
      Numbers[High(Numbers)] := StrToFloat(Copy(TempS, I, J-I));
      I := J;
    end;
    Inc(I);
  end;
  Label1.Caption := FloatToStr(MyAlgorithm(Numbers));
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['0'..'9', '-', '+', ',', '.', 'e', 'E', ' ', #8])  then
    Key := #0;
end;

procedure TForm1.Label3Click(Sender: TObject);
begin
  Edit1.SetFocus;
  Edit1.SelStart := 0;
  Edit1.SelLength := 1;
end;

end.
