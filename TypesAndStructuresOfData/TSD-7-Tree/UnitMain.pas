unit UnitMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls;

type
  PTreeNode = ^TTreeNode;
  TTreeNode = record
    LeftNode: PTreeNode;
    RightNode: PTreeNode;
    Level: Integer;
    Name: string;
  end;

  TTree = class
  private
    Count: Integer;
    MaxLevel: Integer;
    FFileName: string;
  public
    FirstNode: PTreeNode;

    constructor Create();
    procedure Add(const S: string);
    procedure Delete(const S: string);
    procedure LoadFromFile(const FileName: string);
    procedure Draw(const Image: TImage);
    function SearchAndDraw(Letter: Char; const Image: TImage; var FindCount: Integer): Real;
    destructor Free();
  end;

  PStackElem = ^TStackElem;
  TStackElem = record
    Value: PTreeNode;
    NextElem: PStackElem;
  end;

  TStack = record
    FirstElem: PStackElem;
    Count: Integer;
  end;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Button1: TButton;
    Label1: TLabel;
    Label2: TLabel;
    Edit1: TEdit;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Button2: TButton;
    OpenDialog1: TOpenDialog;
    Button3: TButton;
    Button4: TButton;
    LE: TLabeledEdit;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormKeyPress(Sender: TObject; var Key: Char);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
  private
    { Private declarations }
  public
    Tree: TTree;
    function SearchInFile(Letter: Char; const FileName: string; var FindCount: Integer): Real;
  end;

var
  Form1: TForm1;

const EngAlphabet: array[1..52] of Char =
('A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
'U','V','W','X','Y','Z',
'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t',
'u','v','w','x','y','z');

const RusAlphabet: array[1..64] of Char =
('А','Б','В','Г','Д','Е','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т',
'У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я',
'а','б','в','г','д','е','ж','з','и','й','к','л','м','н','о','п','р','с','т',
'у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я');

implementation

{$R *.dfm}

function LowerCaseChr(const Character: Char): Char;
var
  OrdCharacter: Integer;
begin
  OrdCharacter := Ord(Character);
  case OrdCharacter of
    65..90: Result := EngAlphabet[OrdCharacter-38];
    192..223: Result := RusAlphabet[OrdCharacter-159];
    168: Result := 'ё';
  else
    Result := Character;
  end;
end;



procedure Init(Stack: TStack);
begin
  Stack.FirstElem := nil;
  Stack.Count := 0;
end;

procedure AddToStack(Stack: TStack; Value: PTreeNode); overload;
var
  T: PStackElem;
begin
  if Stack.Count = 0 then
  begin
    New(Stack.FirstElem);
    Stack.FirstElem.Value := Value;
    Stack.FirstElem.NextElem := nil;
  end
  else
  begin
    New(T);
    T.Value := Value;
    T.NextElem := Stack.FirstElem;
    Stack.FirstElem := T;
  end;
  Inc(Stack.Count);
end;

function GetFromStack(Stack: TStack): PTreeNode;
var
  T: PStackElem;
begin
  if Stack.FirstElem = nil then
  begin
    Result := nil;
    Exit;
  end;  
  T := Stack.FirstElem.NextElem;
  Result := Stack.FirstElem.Value;
  Dispose(Stack.FirstElem);
  Stack.FirstElem := T;
  Dec(Stack.Count);
end;



procedure TForm1.Button1Click(Sender: TObject);
var
  TreeFindCount, FileFindCount: Integer;
  TreeTime, FileTime: Real;
begin
  TreeTime := Tree.SearchAndDraw(Edit1.Text[1], Image1, TreeFindCount);
  FileTime := SearchInFile(Edit1.Text[1], OpenDialog1.FileName, FileFindCount);
  Label2.Caption := IntToStr(FileFindCount);
  Label5.Caption := FloatToStrF(TreeTime, ffFixed, 9, 7) + ' c';
  Label6.Caption := FloatToStrF(FileTime, ffFixed, 9, 7) + ' c';
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  if OpenDialog1.Execute then
  begin
    Tree.Free;
    Tree := TTree.Create;
    Tree.LoadFromFile(OpenDialog1.FileName);
    Tree.Draw(Image1);
  end;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  LE.Text := Trim(LE.Text);
  if LE.Text = '' then
      Exit;
  Tree.Add(LE.Text);
  Tree.Draw(Image1);
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
  LE.Text := Trim(LE.Text);
  if LE.Text = '' then
      Exit;
  Tree.Delete(LE.Text);
  Tree.Draw(Image1);
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
begin
  if (not (Key in ['A'..'z', 'А'..'я', '0'..'9']) or (Length(Edit1.Text) > 1)) then
    Key := #0
  else
    Edit1.Text := Key;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Tree := TTree.Create;
  Tree.LoadFromFile('file2.txt');
  OpenDialog1.FileName := ExtractFilePath(Application.ExeName) + 'file2.txt';
  Button1.OnClick(Self);
end;

procedure TForm1.FormKeyPress(Sender: TObject; var Key: Char);
begin

end;

{ TTree }

procedure TTree.Add(const S: string);
var
  ST: string;
  N, PrevN: PTreeNode;
  L: Boolean;
begin
  ST := LowerCase(S);
  if Count = 0 then
  begin
    New(FirstNode);
    FirstNode.Name := ST;
    FirstNode.Level := 0;
    MaxLevel := 0;
    FirstNode.LeftNode := nil;
    FirstNode.RightNode := nil;
    Inc(Count);
  end
  else
  begin
    PrevN := FirstNode;
    N := FirstNode;
    while (N <> nil) do
    begin
      if (S < N.Name) then
      begin
        PrevN := N;
        N := N.LeftNode;
        L := True;
        continue;
      end
      else
      if (S > N.Name) then
      begin
        PrevN := N;
        N := N.RightNode;
        L := False;
        Continue;
      end
      else
      begin
        Exit;
      end;
    end;

    New(N);
    N.LeftNode := nil;
    N.RightNode := nil;
    N.Name := ST;
    N.Level := PrevN.Level + 1;
    if N.Level > MaxLevel  then
      MaxLevel := N.Level;
    Inc(Count);
    if L then
      PrevN.LeftNode := N
    else
      PrevN.RightNode := N;
  end;
end;

procedure TTree.Delete(const S: string);
var
  Right: Boolean;
  Del: Boolean;
  PPrevNode: PTreeNode;
  R: Boolean;
  T: PTreeNode;

procedure AddToLeft(Node: PTreeNode; AttachNode: PTreeNode);
var
  T, PrevT: PTreeNode;
begin
  PrevT := Node;
  T := Node.LeftNode;
  while T <> nil do
  begin
    PrevT := T;
    T := T.LeftNode;
  end;
  PrevT.LeftNode := AttachNode;
end;


procedure DeleteWord(Node: PTreeNode);
begin
  if Node.Name = S then
  begin
      if Node.RightNode = nil then
      begin
        if R then
          PPrevNode.RightNode := Node.LeftNode
        else
          PPrevNode.LeftNode := Node.LeftNode;
      end
      else
      begin
        if R then
          PPrevNode.RightNode := Node.RightNode
        else
          PPrevNode.LeftNode := Node.RightNode;
        AddToLeft(Node.RightNode, Node.LeftNode);
      end;
      Dispose(Node);
    Del := True;
  end;

  if Del then Exit;
  R := False;
  PPrevNode := Node;
  if Node.LeftNode <> nil then
  DeleteWord(Node.LeftNode);

  if Del then Exit;
  R := True;
  PPrevNode := Node;
  if Node.RightNode <> nil then
  DeleteWord(Node.RightNode);
end;

begin
  if FirstNode.Name = S then
  begin
    T := FirstNode;
    FirstNode := FirstNode.RightNode;
    AddToLeft(FirstNode, T.LeftNode);
    Dispose(T);
    Exit;
  end;
  DeleteWord(FirstNode);
end;

constructor TTree.Create;
begin
  Count := 0;
end;

function TwoInPower(A: Integer): Integer;
begin
  Result := 1 shl A;
end;

procedure TTree.Draw(const Image: TImage);

procedure DrawBranch(Node: PTreeNode; X, Y: Integer; YStep: Integer);
var
  X1: Integer;
  TH2, TW2: Integer;
  W: Integer;
begin
    TH2 := Image.Canvas.TextHeight(Node.Name) div 2;
    TW2 := Image.Canvas.TextWidth(Node.Name) div 2;
    Image.Canvas.Rectangle(X - TW2 - 3, Y - TH2 - 3, X + TW2 + 3, Y + TH2 + 3);
    Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Name);

    W := Image.ClientWidth div (TwoInPower(Node.Level)*4);
    if Node.LeftNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + 3);
      Image.Canvas.LineTo(X - W, Y + YStep - TH2 - 3);
      DrawBranch(Node.LeftNode, X - W, Y + YStep, YStep);
    end;

    if Node.RightNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + 3);
      Image.Canvas.LineTo(X + W, Y + YStep - TH2 - 3);
      DrawBranch(Node.RightNode, X + W, Y + YStep, YStep);
    end;
end;

var
  XStep, YStep: Integer;
  T: PTreeNode;
  X, Y: Integer;
begin
  with Image.Canvas do
  begin
    Pen.Width := 1;
    Pen.Color := RGB(40, 0, 147);
    Font.Color := RGB(71, 22, 80);
    //Font.Style := [fsBold];
    Font.Size := 13;

    YStep := Image.ClientHeight div (MaxLevel+2);
    XStep := Image.ClientWidth div 2;

    Brush.Color := RGB(181, 249, 255);
    Image.Canvas.FillRect(Rect(0, 0, Image.ClientWidth, Image.ClientHeight));
    Brush.Color := RGB(251, 244, 200);
    DrawBranch(FirstNode, XStep, YStep, YStep);

  end;
end;

destructor TTree.Free();

procedure Clear(Node: PTreeNode);
var L, R: PTreeNode;
begin
  L := Node.LeftNode;
  R := Node.RightNode;
  Dispose(Node);
  if L <> nil then
  begin
    Clear(L);
  end;
  if R <> nil then
  begin
    Clear(R);
  end;
end;

begin
  Clear(FirstNode);
  //inherited Free;
end;

procedure TTree.LoadFromFile(const FileName: string);
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
begin
  Assign(F, FileName);
  Reset(F);
  FFileName := FileName;
  while not SeekEOF(F) do
  begin
    Read(F, Str);
    L := Length(Str);
    I := 1;
    while not (Str[I] in ['A'..'z', 'А'..'я', '0'..'9']) do
      Inc(I);

    while I <= L do
    begin
      J := I;
      while (Str[I] in ['A'..'z', 'А'..'я', '0'..'9']) do
        Inc(I);
      Add(Copy(Str, J, I-J));

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['A'..'z', 'А'..'я', '0'..'9'];
    end;
  end;
  CloseFile(F);

end;

function TTree.SearchAndDraw(Letter: Char; const Image: TImage; var FindCount: Integer): Real;
var
  XStep, YStep: Integer;
  Q1, Q2, F: Int64;
procedure DrawBranch(Node: PTreeNode; X, Y: Integer; YStep: Integer);
var
  X1: Integer;
  TH2, TW2: Integer;
  W: Integer;

begin
    TH2 := Image.Canvas.TextHeight(Node.Name) div 2;
    TW2 := Image.Canvas.TextWidth(Node.Name) div 2;
    if LowerCaseChr(Node.Name[1]) = Letter then
    begin
      Image.Canvas.Pen.Color := RGB(255, 0, 0);
      Image.Canvas.Pen.Width := 3;
      Image.Canvas.Brush.Color := RGB(223, 228, 218);
      Image.Canvas.Rectangle(X - TW2 - 3, Y - TH2 - 3, X + TW2 + 3, Y + TH2 + 3);
      Image.Canvas.Brush.Color := RGB(251, 244, 200);
      Image.Canvas.Pen.Color := RGB(40, 0, 147);
      Image.Canvas.Pen.Width := 1;
    end
    else
      Image.Canvas.Rectangle(X - TW2 - 3, Y - TH2 - 3, X + TW2 + 3, Y + TH2 + 3);

    Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Name);

    W := Image.ClientWidth div (TwoInPower(Node.Level)*4);
    if Node.LeftNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + 3);
      Image.Canvas.LineTo(X - W, Y + YStep - TH2 - 3);
      DrawBranch(Node.LeftNode, X - W, Y + YStep, YStep);
    end;

    if Node.RightNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + 3);
      Image.Canvas.LineTo(X + W, Y + YStep - TH2 - 3);
      DrawBranch(Node.RightNode, X + W, Y + YStep, YStep);
    end;
end;

procedure FindWords(Node: PTreeNode);
begin
  if LowerCaseChr(Node.Name[1]) = Letter then
    Inc(FindCount);

  if Node.LeftNode <> nil then
  begin
    FindWords(Node.LeftNode);
  end;
  if Node.RightNode <> nil then
  begin
    FindWords(Node.RightNode);
  end;
end;

begin
  with Image.Canvas do
  begin
    Pen.Width := 1;
    Pen.Color := RGB(40, 0, 147);
    Font.Color := RGB(71, 22, 80);
    //Font.Style := [fsBold];
    Font.Size := 13;

    YStep := Image.ClientHeight div (MaxLevel+2);
    XStep := Image.ClientWidth div 2;

    Brush.Color := RGB(181, 249, 255);
    Image.Canvas.FillRect(Rect(0, 0, Image.ClientWidth, Image.ClientHeight));
    Brush.Color := RGB(251, 244, 200);
    DrawBranch(FirstNode, XStep, YStep, YStep);

    QueryPerformanceCounter(Q1);
    FindWords(FirstNode);
    QueryPerformanceCounter(Q2);
    QueryPerformanceFrequency(F);
    Result := (Q2-Q1)/F;


  end;
end;

function TForm1.SearchInFile(Letter: Char; const FileName: string; var FindCount: Integer): Real;
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
  Q1, Q2, Freq: Int64;
begin
  FindCount := 0;
  System.Assign(F, FileName);
  Reset(F);
  QueryPerformanceCounter(Q1);
  while not SeekEOF(F) do
  begin
    Read(F, Str);
    L := Length(Str);
    I := 1;
    while not (Str[I] in ['A'..'z', 'А'..'я', '0'..'9']) do
      Inc(I);

    while I <= L do
    begin
      J := I;
      while (Str[I] in ['A'..'z', 'А'..'я', '0'..'9']) do
        Inc(I);
      if (LowerCaseChr(Str[J]) = Letter) then
        Inc(FindCount);

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['A'..'z', 'А'..'я', '0'..'9'];
    end;
  end;
  QueryPerformanceCounter(Q2);
  QueryPerformanceFrequency(Freq);
  CloseFile(F);
  Result := (Q2-Q1)/Freq;
end;

end.
