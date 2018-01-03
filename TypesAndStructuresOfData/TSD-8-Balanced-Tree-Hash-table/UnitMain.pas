unit UnitMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ComCtrls, Grids;

type
  PTreeNode = ^TTreeNode;
  TTreeNode = record
    LeftNode: PTreeNode;
    RightNode: PTreeNode;
    Level: Integer;
    Name: string;
    asdf: Integer;
  end;

  TTree = class
  private
    Count: Integer;
    MaxLevel: Integer;
  public
    FirstNode: PTreeNode;
    Size: Integer;
    Compares: Integer;

    constructor Create();
    procedure Add(const S: string);
    procedure LoadFromFile(const FileName: string);
    procedure Draw(const Image: TImage);
    procedure SearchAndDraw(const Word: string;
      const Image: TImage; var Finded: Boolean; var SearchTime: Real);
    destructor Free();
  end;

  PBalanceTreeNode = ^TBalanceTreeNode;
  TBalanceTreeNode = record
    LeftNode: PBalanceTreeNode;
    RightNode: PBalanceTreeNode;
    Count: Integer;
    Key: string;
    Bal: Integer;
    Level: Integer;
  end;

//--------------------------------------------------------
//--------------------------------------------------------
//--------------------------------------------------------
// реализация сбалансированного дерева

  TBalanceTree = class
  private
    Count: Integer;
    MaxLevel: Integer;
  public
    FirstNode: PBalanceTreeNode;
    Size: Integer;
    Compares: Integer;

    constructor Create();
    procedure Add(const Str: String; var Root: PBalanceTreeNode; var h: Boolean);
    procedure DefineLevels(Node: PBalanceTreeNode; var Level: Integer);
    procedure LoadFromFile(const FileName: string);
    procedure Draw(const Image: TImage);
    procedure SearchAndDraw(const Word: string;
      const Image: TImage; var Finded: Boolean; var SearchTime: Real);
    procedure Restruct(const FileName: string);
    destructor Free();
  end;

//--------------------------------------------------------
//--------------------------------------------------------
//--------------------------------------------------------
// реализация хеш таблицы со списками

  PHashTableElem = ^THashTableElem;
  THashTableElem = record
    Value: string;
    NextElem: PHashTableElem;
  end;

  THashTableList = class
  private
    MaxLevel: Integer;
    Table: array of PHashTableElem;
    function HashFunc(const Str: string): Integer;
  public
    Size: Integer;
    Compares: Integer;

    constructor Create();
    function Add(const Str: string): Boolean;
    function Delete(const Str: string): Boolean;
    procedure LoadFromFile(const FileName: string);
    procedure Restruct(const FileName: string);
    procedure SearchAndDraw(const Word: string;
      const Image: TImage; var Finded: Boolean; var SearchTime: Real);
    procedure Free();
  end;

//--------------------------------------------------------
//--------------------------------------------------------
//--------------------------------------------------------
// реализация хеш таблицы с закрытой адресацией

  THashTableClosed = class
  private
    MaxLevel: Integer;
    Table: array of string;
    function HashFunc(const Str: string): Integer;
  public
    Size: Integer;
    Compares: Integer;

    constructor Create();
    function Add(const Str: string): Boolean;
    function Delete(const Str: string): Boolean;
    procedure LoadFromFile(const FileName: string);
    procedure SearchAndDraw(const Word: string;
      const Image: TImage; var Finded: Boolean; var SearchTime: Real);
    procedure Restruct(const FileName: string);
    destructor Free();
  end;


  PStackElem = ^TStackElem;
  TStackElem = record
    Value: PBalanceTreeNode;
    NextElem: PStackElem;
  end;

  TStack = record
    FirstElem: PStackElem;
    Count: Integer;
  end;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Edit1: TEdit;
    Button2: TButton;
    OpenDialog1: TOpenDialog;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    TabSheet3: TTabSheet;
    imgBalanceTree: TImage;
    imgTree: TImage;
    imgHashTableList: TImage;
    Label10: TLabel;
    TabSheet4: TTabSheet;
    imgHashTableClosed: TImage;
    SG: TStringGrid;
    Button3: TButton;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
    procedure SGClick(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
  public
    BalanceTree: TBalanceTree;
    Tree: TTree;
    HTL: THashTableList;
    HTC: THashTableClosed;
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

const RD: Integer = 3;

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

function LowerCaseStr(const Str: string): string;
var
  I: Integer;
begin
  SetLength(Result, Length(Str));
  for I := 1 to Length(Str) do
    Result[I] := LowerCaseChr(Str[I]);
end;

procedure Init(Stack: TStack);
begin
  Stack.FirstElem := nil;
  Stack.Count := 0;
end;

procedure AddToStack(Stack: TStack; Value: PBalanceTreeNode); overload;
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

function GetFromStack(Stack: TStack): PBalanceTreeNode;
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
  TreeTime, BalanceTreeTime, HTLTime, HTCTime: Real;
  Finded: Boolean;
begin
  Tree.SearchAndDraw(Edit1.Text, imgTree, Finded, TreeTime);
  BalanceTree.SearchAndDraw(Edit1.Text, imgBalanceTree, Finded, BalanceTreeTime);
  HTL.SearchAndDraw(Edit1.Text, imgHashTableList, Finded, HTLTime);
  HTC.SearchAndDraw(Edit1.Text, imgHashTableClosed, Finded, HTCTime);
  SG.Cells[0, 0] := 'Слово "' + Edit1.Text;
  if Finded then
    SG.Cells[0, 0] := SG.Cells[0, 0] + '" найдено'
  else
    SG.Cells[0, 0] := SG.Cells[0, 0] + '" не найдено';
  SG.Cells[1, 1] := FloatToStrF(TreeTime, ffFixed, 9, 7) + ' c';
  SG.Cells[1, 2] := FloatToStrF(BalanceTreeTime, ffFixed, 9, 7) + ' c';
  SG.Cells[1, 3] := FloatToStrF(HTLTime, ffFixed, 9, 7) + ' c';
  SG.Cells[1, 4] := FloatToStrF(HTCTime, ffFixed, 9, 7) + ' c';

  SG.Cells[2, 1] := IntToStr(Tree.Size);
  SG.Cells[2, 2] := IntToStr(BalanceTree.Size);
  SG.Cells[2, 3] := IntToStr(HTL.Size);
  SG.Cells[2, 4] := IntToStr(HTC.Size);

  SG.Cells[3, 1] := IntToStr(Tree.Compares);
  SG.Cells[3, 2] := IntToStr(BalanceTree.Compares);
  SG.Cells[3, 3] := IntToStr(HTL.Compares);
  SG.Cells[3, 4] := IntToStr(HTC.Compares);
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  if OpenDialog1.Execute then
  begin
    Tree.Free;
    Tree := TTree.Create;
    Tree.LoadFromFile(OpenDialog1.FileName);

    BalanceTree.Free;
    BalanceTree := TBalanceTree.Create;
    BalanceTree.LoadFromFile(OpenDialog1.FileName);

    HTL.Free;
    HTL := THashTableList.Create;
    HTL.LoadFromFile(OpenDialog1.FileName);

    HTC.Free;
    HTC := THashTableClosed.Create;
    HTC.LoadFromFile(OpenDialog1.FileName);

    Form1.Caption := 'Дерево, сбалансированное дерево и хеш таблицы для ';
    Form1.Caption := Form1.Caption + '"' + OpenDialog1.FileName + '"';

    Button1.Click;
  end;
end;

procedure TForm1.Button3Click(Sender: TObject);
var
  Finded: Boolean;
  SearchTime: Real;
begin
  HTC.Free;
  HTC := THashTableClosed.Create;
  HTC.Restruct(OpenDialog1.FileName);
  HTC.SearchAndDraw('', ImgHashTableClosed, Finded, SearchTime);
  HTL.Free;
  HTL := THashTableList.Create;
  HTL.Restruct(OpenDialog1.FileName);
  HTL.SearchAndDraw('', ImgHashTableList, Finded, SearchTime);
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
begin
  if not (Key in ['a'..'z', 'а'..'я', '0'..'9', #8]) then
    Key := #0
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Application.Terminate;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  SG.Cells[1, 0] := 'Время поиска (сек)';
  SG.Cells[2, 0] := 'Затраты памяти (байт)';
  SG.Cells[3, 0] := 'Количество сравнений';
  SG.Cells[0, 1] := 'Дерево';
  SG.Cells[0, 2] := 'Сбалансированное дерево';
  SG.Cells[0, 3] := 'Хеш таблица со списками';
  SG.Cells[0, 4] := 'Хеш таблица закрытая';

  Tree := TTree.Create;
  Tree.LoadFromFile('file.txt');

  BalanceTree := TBalanceTree.Create;
  BalanceTree.LoadFromFile('file.txt');

  HTL := THashTableList.Create;
  HTL.LoadFromFile('file.txt');

  HTC := THashTableClosed.Create;
  HTC.LoadFromFile('file.txt');

  OpenDialog1.FileName := ExtractFilePath(Application.ExeName) + 'file.txt';
  Button1.OnClick(Self);

end;

procedure TForm1.SGClick(Sender: TObject);
begin

end;

{ TTree }

procedure TTree.Add(const S: string);
var
  N, PrevN: PTreeNode;
  L: Boolean;
begin
  if Count = 0 then
  begin
    New(FirstNode);
    FirstNode.Name := S;
    FirstNode.Level := 0;
    MaxLevel := 0;
    FirstNode.LeftNode := nil;
    FirstNode.RightNode := nil;
    Inc(Count);
    Size := Size + 12 + Length(FirstNode.Name);
    Size := Size;
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
    N.Name := S;
    N.Level := PrevN.Level + 1;
    if N.Level > MaxLevel  then
      MaxLevel := N.Level;
    Inc(Count);
    if L then
      PrevN.LeftNode := N
    else
      PrevN.RightNode := N;
    Size := Size + 12 + Length(N.Name);
  end;
end;

constructor TTree.Create;
begin
  Count := 0;
  Size := 0;
  Compares := 0;
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
    Image.Canvas.Rectangle(X - TW2 - RD, Y - TH2 - RD, X + TW2 + RD, Y + TH2 + RD);
    Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Name);

    W := Image.ClientWidth div (TwoInPower(Node.Level)*4);
    if Node.LeftNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X - W, Y + YStep - TH2 - RD);
      DrawBranch(Node.LeftNode, X - W, Y + YStep, YStep);
    end;

    if Node.RightNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X + W, Y + YStep - TH2 - RD);
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
      Add(LowerCaseStr(Copy(Str, J, I-J)));

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

  Size := Size + 8;
end;

procedure TTree.SearchAndDraw(const Word: string;
  const Image: TImage; var Finded: Boolean; var SearchTime: Real);
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
    if Length(Node.Name) <> 0 then
    
    if Node.Name = Word then
    begin
      Image.Canvas.Pen.Color := RGB(255, 0, 0);
      Image.Canvas.Pen.Width := RD;
      Image.Canvas.Brush.Color := RGB(223, 228, 218);
      Image.Canvas.Rectangle(X - TW2 - RD, Y - TH2 - RD, X + TW2 + RD, Y + TH2 + RD);
      Image.Canvas.Brush.Color := RGB(251, 244, 200);
      Image.Canvas.Pen.Color := RGB(40, 0, 147);
      Image.Canvas.Pen.Width := 1;
      Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Name);
    end
    else
      Image.Canvas.Rectangle(X - TW2 - RD, Y - TH2 - RD, X + TW2 + RD, Y + TH2 + RD);

    Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Name);

    W := Image.ClientWidth div (TwoInPower(Node.Level)*4);
    if Node.LeftNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X - W, Y + YStep - TH2 - RD);
      DrawBranch(Node.LeftNode, X - W, Y + YStep, YStep);
    end;

    if Node.RightNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X + W, Y + YStep - TH2 - RD);
      DrawBranch(Node.RightNode, X + W, Y + YStep, YStep);
    end;
end;

procedure FindWord(Node: PTreeNode);
begin
  if ((Node.Name = Word) or (Finded)) then
  begin
    Finded := True;
    Inc(Compares);
    Exit;
  end
  else
    Inc(Compares);
  if Node.LeftNode <> nil then
    FindWord(Node.LeftNode);
  //Inc(Compares);
  if Node.RightNode <> nil then
    FindWord(Node.RightNode);
  //Inc(Compares);
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

    if Word = '' then
    begin
      SearchTime := 0;
      Finded := False;
      Exit;
    end;
    Compares := 0;
    QueryPerformanceCounter(Q1);
    Finded := False;
    FindWord(FirstNode);
    QueryPerformanceCounter(Q2);
    QueryPerformanceFrequency(F);
    SearchTime := (Q2-Q1)/F;
  end;
end;

{ TBalanceTree }

procedure TBalanceTree.Add(const Str: String; var Root: PBalanceTreeNode; var H: Boolean);
var
  p1,p2: PBalanceTreeNode;
begin
  if Root = nil then
  begin
    New(Root);
    H := True;
    Root^.Key := Str;
    Root^.count := 1;
    Root^.LeftNode := nil;
    Root^.RightNode := nil;
    Root^.bal := 0;
    Size := Size + Length(Root.Key) + 16;
  end
  else
    if Root^.Key > Str then
    begin
      Add(Str,Root^.LeftNode,H);
      if H then
        case Root^.bal of
          1:
          begin
            Root^.bal := 0;
            H := False;
          end;
          0: Root^.bal:=-1;
         -1:
          begin
            p1:=Root^.LeftNode;
            if p1^.bal = -1 then
            begin
              Root^.LeftNode := p1^.RightNode;
              p1^.RightNode := Root;
              Root^.bal:=0;
              Root:=p1;
            end
            else
            begin
              p2 := p1^.RightNode;
              p1^.RightNode := p2^.LeftNode;
              p2^.LeftNode := p1;
              Root^.LeftNode := p2^.RightNode;
              p2^.RightNode := Root;
              if p2^.bal=-1 then
                Root^.bal := 1
              else
                Root^.bal :=0;

              if p2^.bal = 1 then
                p1^.bal := -1
              else
                p1^.bal := 0;

              Root:=p2;
            end;
            Root^.bal := 0;
            H := False;
          end;
        end;
    end

    else
      if Root^.Key < Str then
      begin
        Add(Str,Root^.RightNode, H);
        if H then
          case Root^.bal of
            -1:
            begin
              Root^.bal :=0;
              H:=False;
            end;
            0: Root^.bal:=1;
            1:
            begin
              p1:=Root^.RightNode;
              if p1^.bal = 1 then
              begin
                Root^.RightNode := p1^.LeftNode;
                p1^.LeftNode := Root;
                Root^.bal:=0;
                Root:=p1;
              end
              else
              begin
                p2 := p1^.LeftNode;
                p1^.LeftNode := p2^.RightNode;
                p2^.RightNode := p1;
                Root^.RightNode := p2^.LeftNode;
                p2^.LeftNode := Root;
                if p2^.bal=1 then
                  Root^.bal := -1
                else
                  Root^.bal :=0;

                if p2^.bal = -1 then
                  p1^.bal := 1
                else
                  p1^.bal := 0;

                Root:=p2;
              end;
              Root^.bal := 0;
              H := False;
            end;
          end;
      end
      else
      begin
        Root^.count := Root^.count + 1;
        H:=false;
      end;
end;

constructor TBalanceTree.Create;
begin
  Count := 0;
  Size := 0;
end;

procedure TBalanceTree.DefineLevels(Node: PBalanceTreeNode; var Level: Integer);
begin
    Node.Level := Level;
    Inc(Level);
    if Node.LeftNode <> nil then
    begin
      DefineLevels(Node.LeftNode, Level);
    end;

    if Node.RightNode <> nil then
    begin
      DefineLevels(Node.RightNode, Level);
    end;
    Dec(Level);

    if Level > MaxLevel then
      MaxLevel := Level;
end;

procedure TBalanceTree.Draw(const Image: TImage);

procedure DrawBranch(Node: PBalanceTreeNode; X, Y: Integer; YStep: Integer);
var
  X1: Integer;
  TH2, TW2: Integer;
  W: Integer;
begin
    TH2 := Image.Canvas.TextHeight(Node.Key) div 2;
    TW2 := Image.Canvas.TextWidth(Node.Key) div 2;
    Image.Canvas.Rectangle(X - TW2 - RD, Y - TH2 - RD, X + TW2 + RD, Y + TH2 + RD);
    Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Key);

    W := Image.ClientWidth div (TwoInPower(Node.Level)*4);
    if Node.LeftNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X - W, Y + YStep - TH2 - RD);
      DrawBranch(Node.LeftNode, X - W, Y + YStep, YStep);
    end;

    if Node.RightNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X + W, Y + YStep - TH2 - RD);
      DrawBranch(Node.RightNode, X + W, Y + YStep, YStep);
    end;
end;

var
  XStep, YStep: Integer;
  T: PBalanceTreeNode;
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

destructor TBalanceTree.Free();

procedure Clear(Node: PBalanceTreeNode);
var L, R: PBalanceTreeNode;
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

procedure TBalanceTree.LoadFromFile(const FileName: string);
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
  H: Boolean;
  Root: PBalanceTreeNode;
  Level: Integer;
begin
  Assign(F, FileName);
  Reset(F);
  Root := nil;
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
      Add(LowerCaseStr(Copy(Str, J, I-J)), Root, H);

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
  FirstNode := Root;
  Level := 0;
  DefineLevels(FirstNode, Level);

  Size := Size + 8;
end;


procedure TBalanceTree.Restruct(const FileName: string);
begin

end;

procedure TBalanceTree.SearchAndDraw(const Word: string;
  const Image: TImage; var Finded: Boolean; var SearchTime: Real);
var
  XStep, YStep: Integer;
  Q1, Q2, F: Int64;

procedure DrawBranch(Node: PBalanceTreeNode; X, Y: Integer; YStep: Integer);
var
  X1: Integer;
  TH2, TW2: Integer;
  W: Integer;

begin
    TH2 := Image.Canvas.TextHeight(Node.Key) div 2;
    TW2 := Image.Canvas.TextWidth(Node.Key) div 2;
    if Node.Key = Word then
    begin
      Image.Canvas.Pen.Color := RGB(255, 0, 0);
      Image.Canvas.Pen.Width := RD;
      Image.Canvas.Brush.Color := RGB(223, 228, 218);
      Image.Canvas.Rectangle(X - TW2 - RD, Y - TH2 - RD, X + TW2 + RD, Y + TH2 + RD);
      Image.Canvas.Brush.Color := RGB(251, 244, 200);
      Image.Canvas.Pen.Color := RGB(40, 0, 147);
      Image.Canvas.Pen.Width := 1;
    end
    else
      Image.Canvas.Rectangle(X - TW2 - RD, Y - TH2 - RD, X + TW2 + RD, Y + TH2 + RD);

    Image.Canvas.TextOut(X - TW2, Y - TH2, Node.Key);

    W := Image.ClientWidth div (TwoInPower(Node.Level)*4);
    if Node.LeftNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X - W, Y + YStep - TH2 - RD);
      DrawBranch(Node.LeftNode, X - W, Y + YStep, YStep);
    end;

    if Node.RightNode <> nil then
    begin
      Image.Canvas.MoveTo(X, Y + TH2 + RD);
      Image.Canvas.LineTo(X + W, Y + YStep - TH2 - RD);
      DrawBranch(Node.RightNode, X + W, Y + YStep, YStep);
    end;
end;

procedure FindWords(Node: PBalanceTreeNode);
begin
  if ((Node.Key = Word) or Finded) then
  begin
    Finded := True;
    Inc(Compares);
    Exit;
  end
  else
    Inc(Compares);
  if Node.LeftNode <> nil then
    FindWords(Node.LeftNode);
  //Inc(Compares);
  if Node.RightNode <> nil then
    FindWords(Node.RightNode);
  //Inc(Compares);
end;

begin
  with Image.Canvas do
  begin
    Pen.Width := 1;
    Pen.Color := RGB(40, 0, 147);
    Font.Color := RGB(71, 22, 80);
    Font.Size := 13;

    YStep := Image.ClientHeight div (MaxLevel+2);
    XStep := Image.ClientWidth div 2;

    Brush.Color := RGB(181, 249, 255);
    Image.Canvas.FillRect(Rect(0, 0, Image.ClientWidth, Image.ClientHeight));
    Brush.Color := RGB(251, 244, 200);
    DrawBranch(FirstNode, XStep, YStep, YStep);
  end;
  if Word = '' then
  begin
    SearchTime := 0;
    Finded := False;
    Exit;
  end;
  Compares := 0;
  QueryPerformanceCounter(Q1);
  Finded := False;
  FindWords(FirstNode);
  QueryPerformanceCounter(Q2);
  QueryPerformanceFrequency(F);
  SearchTime := (Q2-Q1)/F;
end;

{ THashTable }

constructor THashTableList.Create();
begin
  SetLength(Table, 0);
  MaxLevel := 0;
  Size := 0;
end;

function THashTableList.Add(const Str: string): Boolean;
var
  Ind: Integer;
  PT: PHashTableElem;
  PPrevT: PHashTableElem;
  Level: Integer;
begin
  Ind := HashFunc(Str);
  if Table[Ind] = nil then
  begin
    New(Table[Ind]);
    Table[Ind].Value := Str;
    Table[Ind].NextElem := nil;
    Result := True;
    Size := Size + Length(Table[Ind].Value) + 8;
  end
  else
  begin
    Level := 0;
    if Table[Ind].Value = Str then
      Exit;
    PPrevT := Table[Ind];
    PT := PPrevT.NextElem;
    while PT <> nil do
    begin
      if PT.Value = Str then
      begin
        Result := False;
        Exit;
      end;
      Inc(Level);
      PPrevT := PT;
      PT := PT.NextElem;
    end;
    if Level > MaxLevel then
      MaxLevel := Level;
    New(PPrevT.NextElem);
    PPrevT := PPrevT.NextElem;
    PPrevT.Value := Str;
    PPrevT.NextElem := nil;
    Result := True;
    Size := Size + Length(Table[Ind].Value) + 8;
  end;
end;

function THashTableList.Delete(const Str: string): Boolean;
var
  Ind: Integer;
  PT: PHashTableElem;
  PPrevT: PHashTableElem;
begin
  Ind := HashFunc(Str);
  if Table[Ind] = nil then
    Result := True
  else
  begin
    if Table[Ind].Value = Str then
    begin
      PT := Table[Ind].NextElem;
      Dispose(Table[Ind]);
      Table[Ind] := PT;
      Exit;
    end;
    PPrevT := Table[Ind];
    PT := PPrevT.NextElem;
    while PT <> nil do
    begin
      if PT.Value = Str then
      begin
        PPrevT.NextElem := PT.NextElem;
        Dispose(PT);
        Result := True;
        Exit;
      end;
      PPrevT := PT;
      PT := PT.NextElem;
    end;
    Result := False;
  end;
end;

procedure THashTableList.Free;
var
  I: Integer;
  PT: PHashTableElem;
  PT1: PHashTableElem;
begin
  for I := 0 to High(Table) do
  begin
    PT := Table[I];
    while PT <> nil do
    begin
      PT1 := PT.NextElem;
      Dispose(PT);
      PT := PT1;
    end;
  end;
  SetLength(Table, 0);
  //inherited Free;
end;

procedure THashTableList.LoadFromFile(const FileName: string);
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
  WordCount: Integer;
begin
  WordCount := 0;
  System.Assign(F, FileName);
  Reset(F);
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
      Inc(WordCount);

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['A'..'z', 'А'..'я', '0'..'9'];
    end;
  end;

  SetLength(Table, WordCount);

  Reset(F);
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
      Add(LowerCaseStr(Copy(Str, J, I-J)));

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

  Size := Size + Length(Table)*4 + 4;
end;

procedure THashTableList.Restruct(const FileName: string);
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
  WordCount: Integer;
begin
  WordCount := 0;
  System.Assign(F, FileName);
  Reset(F);
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
      Inc(WordCount);

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['A'..'z', 'А'..'я', '0'..'9'];
    end;
  end;

  SetLength(Table, WordCount*2);

  Reset(F);
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
      Add(LowerCaseStr(Copy(Str, J, I-J)));

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

  Size := Size + Length(Table)*2*4 + 4;
end;

procedure THashTableList.SearchAndDraw(const Word: string;
  const Image: TImage; var Finded: Boolean; var SearchTime: Real);
var
  XS, YS: Integer;
  RW, RH, RW2, RH2: Integer;
  X, Y: Integer;
  I: Integer;
  PT: PHashTableElem;
  PPrevT: PHashTableElem;
  TW, TH2: Integer;
  Q1, Q2, F: Int64;
  Hash: Integer;
begin
  with Image.Canvas do
  begin
    Pen.Width := 1;
    Pen.Color := RGB(40, 0, 147);
    Font.Color := RGB(9, 28, 8);
    Font.Size := 11;

    Brush.Color := RGB(253, 241, 197);
    Image.Canvas.FillRect(Rect(0, 0, Image.ClientWidth, Image.ClientHeight));
    Brush.Color := RGB(246, 227, 247);

    RH := TextHeight('a') + 2*RD;
    RH2 := RH div 2;
    RW := RH;
    RW2 := RH2;
    XS := 50;
    YS := (Image.ClientHeight - RH) div (High(Table) + 2);

    X := XS;
    Y := YS;
    for I := 0 to High(Table) do
    begin
      if Table[I] = nil then
      begin
        Brush.Color := RGB(200, 250, 248);
        Ellipse(X, Y, X + RW, Y + RH);
        Brush.Color := RGB(246, 227, 247);
      end
      else
      begin
        PT := Table[I];
        while PT <> nil do
        begin
          TW := TextWidth(PT.Value);
          if PT.Value = Word then
          begin
              Pen.Width := 3;
              Pen.Color := RGB(255, 83, 23);
              Rectangle(X, Y, X + TW + 2*RD, Y + RH);
              TextOut(X + RD, Y + RD, PT.Value);
              Pen.Color := RGB(40, 0, 147);
              Pen.Width := 1;
          end
          else
          begin
              Rectangle(X, Y, X + TW + 2*RD, Y + RH);
              TextOut(X + RD, Y + RD, PT.Value);
          end;

          if PT.NextElem <> nil then
          begin
            Brush.Color := RGB(255, 207, 222);
            if PT.Value = Word then
            begin
              Pen.Width := 3;
              Pen.Color := RGB(255, 83, 23);
              Rectangle(X + TW + 2*RD, Y + RH div 4,
                        X + TW + 2*RD + RH div 2, Y + 3 * RH div 4);
              Font.Style := [];
              Pen.Color := RGB(40, 0, 147);
            end
            else
              Rectangle(X + TW + 2*RD, Y + RH div 4,
                        X + TW + 2*RD + RH div 2, Y + 3 * RH div 4);
            Brush.Color := RGB(246, 227, 247);

            Pen.Width := 1;
            Pen.Style := psDot;
            MoveTo(X + TW + 2*RD + RH div 4, Y + RH2);
            LineTo(X + TW + 2*RD + RH div 4 + XS, Y + RH2);
            Pen.Width := 1;
            Pen.Style := psSolid;
            X := X + TW + 2*RD + RH div 4 + XS;
          end;

          PT := PT.NextElem;
        end;
        {Brush.Color := RGB(200, 250, 248);
        Ellipse(X, Y, X + RW, Y + RH);
        Brush.Color := RGB(246, 227, 247); }
      end;
      X := XS;
      Y := Y + YS;
    end;
  end;

  if Word = '' then
  begin
    SearchTime := 0;
    Finded := False;
    Exit;
  end;
  Compares := 0;
  QueryPerformanceCounter(Q1);
  Inc(Compares);
  Hash := HashFunc(Word);
  Finded := False;
  PT := Table[Hash];
  while PT <> nil do
  begin
    //Inc(Compares);
    if PT.Value = Word then
    begin
      Finded := True;
      Inc(Compares);
      Break;
    end
    else
      Inc(Compares);
    PT := PT.NextElem;
  end;
  //Inc(Compares);
  QueryPerformanceCounter(Q2);
  QueryPerformanceFrequency(F);
  SearchTime := (Q2-Q1)/F;
end;

function THashTableList.HashFunc(const Str: string): Integer;
var
  I: Integer;
begin
  Result := 0;
  for I := 1 to Length(Str) do
    Result := Result + Ord(Str[I]);
  Result := Result mod Length(Table);
end;

{ THashTableClose }

constructor THashTableClosed.Create();
begin
  SetLength(Table, 0);
  MaxLevel := 0;
  Size := 0;
end;

function THashTableClosed.Add(const Str: string): Boolean;
var
  Ind: Integer;
  Ind1: Integer;
begin
  Ind := HashFunc(Str);
  Ind1 := Ind;
  while Table[Ind] <> '' do
  begin
    if Table[Ind] = Str then
    begin
      Result := True;
      Exit;
    end;
    Inc(Ind);
    if Ind = Length(Table) then
      Ind := 0;
    if Ind = Ind1 then
    begin
      Result := False;
      Exit;
    end;
  end;
  Table[Ind] := Str;
  Size := Size + Length(Table[Ind]) + 4;
  Result := True;
end;

function THashTableClosed.Delete(const Str: string): Boolean;
var
  Ind: Integer;
begin
  Ind := HashFunc(Str);
  while Table[Ind] <> Str do
  begin
    if Table[Ind] = '' then
    begin
      Result := False;
      Exit;
    end;
    Inc(Ind);
    if Ind = Length(Table) then
    begin
      Result := True;
      Exit;
    end;
  end;
  Table[Ind] := '';
  Result := True;
end;

destructor THashTableClosed.Free;
begin
  SetLength(Table, 0);
end;

procedure THashTableClosed.LoadFromFile(const FileName: string);
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
  WordCount: Integer;
begin
  WordCount := 0;
  System.Assign(F, FileName);
  Reset(F);
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
      Inc(WordCount);

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['A'..'z', 'А'..'я', '0'..'9'];
    end;
  end;

  SetLength(Table, WordCount*2);

  Reset(F);
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
      Add(LowerCaseStr(Copy(Str, J, I-J)));

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

  Size := Size + Length(Table)*2*4 + 4;
end;


procedure THashTableClosed.Restruct(const FileName: string);
var
  F: TextFile;
  Str: string;
  L: Integer;
  I, J: Integer;
  WordCount: Integer;
begin
  WordCount := 0;
  System.Assign(F, FileName);
  Reset(F);
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
      Inc(WordCount);

      if I > L then
        Break;
      repeat
        Inc(I);
        if I > L then
          Break;
      until Str[I] in ['A'..'z', 'А'..'я', '0'..'9'];
    end;
  end;

  SetLength(Table, WordCount*4);

  Reset(F);
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
      Add(LowerCaseStr(Copy(Str, J, I-J)));

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

  Size := Size + Length(Table)*4*4 + 4;
end;


procedure THashTableClosed.SearchAndDraw(const Word: string;
      const Image: TImage; var Finded: Boolean; var SearchTime: Real);
var
  XS, YS: Integer;
  RW, RH, RW2, RH2: Integer;
  X, Y, XH: Integer;
  I: Integer;
  TW, TH2: Integer;
  C: Integer;
  RowCount: Integer;
  Q1, Q2, F: Int64;
  Hash: Integer;
begin
  with Image.Canvas do
  begin
    Pen.Width := 1;
    Pen.Color := RGB(40, 0, 147);
    Font.Color := RGB(9, 28, 8);
    Font.Size := 11;

    Brush.Color := RGB(253, 241, 197);
    Image.Canvas.FillRect(Rect(0, 0, Image.ClientWidth, Image.ClientHeight));
    Brush.Color := RGB(246, 227, 247);

    RowCount := 4;
    RH := TextHeight('a') + 2*RD;
    RH2 := RH div 2;
    RW := RH;
    RW2 := RH2;
    XH := Length(Table) div (RowCount-1);
    XS := Trunc(Image.ClientWidth/(XH + 0.5));
    YS := Trunc(Image.ClientHeight/(RowCount + 0.5));

    X := XS div 2;
    Y := YS div 2;
    C := 0;
    for I := 0 to High(Table) do
    begin
      if Table[I] = '' then
      begin
        Brush.Color := RGB(200, 250, 248);
        Ellipse(X, Y, X + RW, Y + RH);
        Brush.Color := RGB(246, 227, 247);
        Pen.Style := psDot;
        MoveTo(X + RW, Y + RH2);
        LineTo(X + XS, Y + RH2);
        Pen.Style := psSolid;
      end
      else
      begin
        TW := TextWidth(Table[I]);
        if Table[I] = Word then
        begin
          Pen.Width := 3;
          Pen.Color := RGB(255, 83, 23);
          //Font.Style := [fsBold];
          Rectangle(X, Y, X + TW + 2*RD, Y + RH);
          TextOut(X + RD, Y + RD, Table[I]);
          Font.Style := [];
          Pen.Color := RGB(40, 0, 147);
          Pen.Width := 1;
        end
        else
        begin
          Rectangle(X, Y, X + TW + 2*RD, Y + RH);
          TextOut(X + RD, Y + RD, Table[I]);
        end;
        Pen.Style := psDot;
        MoveTo(X + TW + 2*RD, Y + RH2);
        LineTo(X + XS, Y + RH2);
        Pen.Style := psSolid;

        {Polyline([Point(X + TW div 2 + RD, Y + Ys), Point(X + TW div 2 + RD - 3, Y + Ys - 5),
          Point(X + TW div 2 + RD + 3, Y + Ys - 5)]);
        {Brush.Color := RGB(255, 207, 222);
        Rectangle(X + TW + 2*RD, Y + RH div 4,
                  X + TW + 2*RD + RH div 2, Y + 3 * RH div 4);
        Brush.Color := RGB(246, 227, 247);}
      end;

      X := X + XS;
      Inc(C);
      if (C mod XH = 0) then
      begin
        Pen.Style := psDash;
        MoveTo(0, Y + YS div 2);
        LineTo(Image.ClientWidth, Y + YS div 2);
        Pen.Style := psDot;
        MoveTo(0, Y + YS + RH2);
        LineTo(XS div 2, Y + YS + RH2);
        {MoveTo(0, Y - YS + RH2);
        LineTo(Image.ClientWidth, Y - YS + RH2);  }
        Pen.Style := psSolid;
        X := XS div 2;
        Y := Y + YS;
        C := 0;
      end;
    end;
  end;

  if Word = '' then
  begin
    SearchTime := 0;
    Finded := False;
    Exit;
  end;
  Compares := 0;
  QueryPerformanceCounter(Q1);
  Inc(Compares);
  Hash := HashFunc(Word);
  Finded := True;
  I := Hash;
  while Table[I] <> Word do
  begin
    Inc(I);
    Inc(Compares);
    if I = Length(Table) then
    begin
      //Inc(Compares);
      I := 0;
    end
    else
      Inc(Compares);
    if I = Hash then
    begin
      Finded := False;
      //Inc(Compares);
      Break;
    end
    else
      //Inc(Compares);
  end;
  Inc(Compares);
  QueryPerformanceCounter(Q2);
  QueryPerformanceFrequency(F);
  SearchTime := (Q2-Q1)/F;
end;

function THashTableClosed.HashFunc(const Str: string): Integer;
var
  I: Integer;
begin
  Result := 0;
  for I := 1 to Length(Str) do
    Result := Result + Ord(Str[I]);
  Result := Result mod (Length(Table));
end;

end.
