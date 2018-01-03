unit UnitMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, StdCtrls, NewRecord, UnitSearchResult, ExtCtrls, Spin;

type
  TPerformType = (Child, Adult, Music);
  TAdultPerformType = (Play, Drama, Comedy);

  TRepertory = record
    Caption: string[50];
    Perform: string[50];
    case PerformType: TPerformType of
      Child: (Age: Integer);
      Adult: (AdultPerformType: TAdultPerformType);
      Music: (Composer: string[25]);
  end;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Button4: TButton;
    Button5: TButton;
    Button6: TButton;
    ListView1: TListView;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    btnGenerate: TButton;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    SpinEdit1: TEdit;
    UpDown1: TUpDown;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
    procedure FormResize(Sender: TObject);
    procedure btnGenerateClick(Sender: TObject);
  private
    { Private declarations }
  public
    function RepertoryToListItem(const Repertory: TRepertory): TListItem;
    procedure UpdateListView;
  end;

const Theaters: array[0..14] of string = ('МXAT им. А.П. Чехова',
  'Мастерская П. Фоменко', 'Театр-студия п/р О. Табакова',
  'им. Вахтангова', 'им. Моссовета', 'на Таганке',
  'Эрмитаж', 'на Малой Бронной', 'Антона Чехова',
  'п/р Елены Камбуровой', 'Армена Джигарханяна',
  'Мастерская Олега Кудряшова', 'МХТ им. Чехова',
  'Сатирикон', 'Большой');

    PerformsChild: array[0..4] of string =
  ('ЖУРАВЛИНЫЕ ПЕРЬЯ', 'СЮРПРИЗ АЙБОЛИТА', 'ОСТОРОЖНО, СВЕТОФОР',
  'ДАЛЕКО ЛЬ, КУМА, ХОДИЛА?', 'БАБА-ЯГА И ЗАКОЛДОВАННЫЕ БРАТЬЯ');

    PerformsMusic: array[0..4] of string =
  ('БОГЕМА', 'ИОЛАНТА', 'Волшебная флейта', 'СНЕГУРОЧКА', 'ЛЮБОВНЫЙ НАПИТОК');

    PerformsAdult: array[0..4] of string =
  ('УЖИН С ДУРАКОМ', 'Смешанные чувства', 'Все как у людей', 'ОПАСНЫЙ ПОВОРОТ',
  'ЛЮБОВЬ ГЛАЗАМИ СЫЩИКА');

    Composers: array[0..4] of string =
  ('Н.А Римский-Корсаков', 'Дж. Пуччини', 'П.И. Чайковский',
  'Х. Левеншелль', 'П. Вальдгардт');


var
  Form1: TForm1;
  Repertories: array[0..9999] of TRepertory;
  High: Cardinal;
  ProcessID : DWORD;
  ProcessHandle : THandle;
  ThreadHandle : THandle;
  CurRate: Int64;
  Q1, Q2: Int64;
  AvgTime, AvgKeysTime: Real;
  Keys: array[0..9999] of Cardinal;

function Equal(Repertory1, Repertory2: TRepertory): Boolean;
function RandomRepertory: TRepertory;

implementation

{$R *.dfm}

function RandomRepertory: TRepertory;
var
  A, B: Integer;
begin
  Result.Caption := Theaters[Random(15)];
  A := Random(3);
  case A of
    0:
    begin
      Result.PerformType := Child;
      Result.Perform := PerformsChild[Random(5)];
      Result.Age := Random(14) + 5;
    end;
    1:
    begin
      Result.PerformType := Adult;
      Result.Perform := PerformsAdult[Random(5)];
      B := Random(3);
      case B of
        0: Result.AdultPerformType := Play;
        1: Result.AdultPerformType := Drama;
        2: Result.AdultPerformType := Comedy;
      end;
    end;
    2:
    begin
      Result.PerformType := Music;
      Result.Perform := PerformsMusic[Random(5)];
      Result.Composer := Composers[Random(5)];
    end;
  end;
end;

procedure TForm1.UpdateListView;
var
  I: Integer;
begin
  ListView1.Clear;
  for I := 0 to High - 1 do
    ListView1.Items.AddItem(RepertoryToListItem(Repertories[Keys[I]]));
end;

procedure QuickSort;

  procedure sort(l,r: integer);
  var
    i,j: integer;
    X: string;
    Y: TRepertory;
    YK: Integer;
  begin
    i := l;
    j := r;
    x := Repertories[(r+l) div 2].Caption; { x := Repertories[(r+l) div 2]; - для выбора среднего элемента }
    repeat
      while Repertories[i].Caption < x do
        i := i+1; { Repertories[i] > x  - сортировка по убыванию}
      while x < Repertories[j].Caption do
        j := j-1; { x > Repertories[j]  - сортировка по убыванию}
      if i <= j then
      begin
        if Repertories[i].Caption > Repertories[j].Caption then
        begin  { это условие можно убрать } {Repertories[i] < Repertories[j] при сортировке по убыванию}
          Y := Repertories[I];
          Repertories[I] := Repertories[J];
          //Keys[I] := I;
          Repertories[J] := Y;
          //Keys[J] := J;
        end;
        i:=i+1;
        j:=j-1;
      end;

    until i>j;
    if l<j then sort(l,j);
    if i<r then sort(i,r);
  end;
var
  I: Integer;
begin
  sort(0,High-1);
end;

procedure QuickSortKeys;

  procedure sort(l,r: integer);
  var
    i,j: integer;
    X: string;
    Y: Integer;
  begin
    i := l;
    j := r;
    x := Repertories[Keys[(r+l) div 2]].Caption; { x := Repertories[(r+l) div 2]; - для выбора среднего элемента }
    repeat
      while Repertories[Keys[i]].Caption < x do
        Inc(i); { Repertories[i] > x  - сортировка по убыванию}
      while x < Repertories[Keys[j]].Caption do
        Dec(j); { x > Repertories[j]  - сортировка по убыванию}
      if i <= j then
      begin
        if Repertories[Keys[i]].Caption > Repertories[Keys[j]].Caption then
        begin  { это условие можно убрать } {Repertories[i] < Repertories[j] при сортировке по убыванию}
          y := Keys[i];
          Keys[i] := Keys[j];
          Keys[j] := y;
        end;
        Inc(i);
        Dec(j);
      end;

    until i > j;
    if l < j then sort(l,j);
    if i < r then sort(i,r);
  end;

begin
  sort(0,High-1);
end;

function Equal(Repertory1, Repertory2: TRepertory): Boolean;
begin
  if (Repertory1.Caption = Repertory2.Caption)
    and (Repertory1.Perform = Repertory2.Perform)
    and (Repertory1.PerformType = Repertory2.PerformType) then
  begin
    case Repertory1.PerformType of
      Child: Result := Repertory1.Age = Repertory2.Age;
      Adult: Result := Repertory1.AdultPerformType = Repertory2.AdultPerformType;
      Music: Result := Repertory1.Composer = Repertory2.Composer;
    end;
  end
  else
    Result := False;
end;

function TForm1.RepertoryToListItem(const Repertory: TRepertory): TListItem;
begin
  Result := TListItem.Create(ListView1.Items);
  Result.SubItems.Add(Repertory.Caption);
  Result.SubItems.Add(Repertory.Perform);
  case Repertory.PerformType of
    Child:
    begin
      Result.SubItems.Add('Child');
      Result.SubItems.Add(IntToStr(Repertory.Age));
    end;
    Adult:
    begin
      Result.SubItems.Add('Adult');
      case Repertory.AdultPerformType of
        Play:   Result.SubItems.Add('Play');
        Drama:  Result.SubItems.Add('Drama');
        Comedy: Result.SubItems.Add('Comedy');
      end;
    end;
    Music:
    begin
      Result.SubItems.Add('Music');
      Result.SubItems.Add(Repertory.Composer);
    end;
  end;
  {if ListView1.Canvas.TextWidth(Repertory.Caption) > (ListView1.Columns[1].Width+7) then
    ListView1.Columns[1].Width := ListView1.Canvas.TextWidth(Repertory.Caption) + 7;
  if ListView1.Canvas.TextWidth(Repertory.Perform) > (ListView1.Columns[2].Width+7) then
    ListView1.Columns[2].Width := ListView1.Canvas.TextWidth(Repertory.Caption) + 7;  }
end;

procedure TForm1.btnGenerateClick(Sender: TObject);
var
  I: Integer;
begin
  High := StrToInt(SpinEdit1.Text);
  for I := 0 to High-1 do
  begin
    Repertories[I] := RandomRepertory;
    Keys[I] := I;
  end;
  UpdateListView;
end;

procedure TForm1.Button1Click(Sender: TObject);
var
  Repertory: TRepertory;
  I: Integer;
begin
  Form3.ShowModal;
  if Form3.ModalResult = mrOk then
  begin
    Repertories[High].Caption := Form3.Edit1.Text;
    Repertories[High].Perform := Form3.Edit2.Text;
    if Form3.RadioButton1.Checked then
    begin
      Repertories[High].PerformType := Child;
      Repertories[High].Age := StrToInt(Form3.Edit4.Text);
    end
    else
    if Form3.RadioButton2.Checked then
    begin
      Repertories[High].PerformType := Adult;
      case Form3.ComboBox1.ItemIndex of
        0: Repertories[High].AdultPerformType := Play;
        1: Repertories[High].AdultPerformType := Drama;
        2: Repertories[High].AdultPerformType := Comedy;
      end;
    end
    else
    begin
      Repertories[High].PerformType := Music;
      Repertories[High].Composer := Form3.Edit3.Text;
    end;

    Keys[High] := High;

    for I := 0 to High - 1 do
      if Equal(Repertories[I], Repertories[High]) then
      begin
        ShowMessage('Impossible create identical records');
        Exit;
      end;
    Inc(High);

    ListView1.Items.AddItem(RepertoryToListItem(Repertories[High-1]));
  end;
  Label5.Caption := IntToStr(High);

end;

procedure TForm1.Button2Click(Sender: TObject);
var
  I: Integer;
begin
  if ListView1.Selected <> nil then
  begin
    Dec(High);
    for I := ListView1.Selected.Index to High-1 do
    begin
      Repertories[I] := Repertories[I + 1];
    end;
    for I := 0 to High - 1 do
      Keys[I] := I;
    ListView1.DeleteSelected;
  end;
  Label5.Caption := IntToStr(High);
end;

procedure TForm1.Button4Click(Sender: TObject);
var
  Time: Real;
  I: Integer;
begin
 { SetPriorityClass(ProcessHandle, REALTIME_PRIORITY_CLASS);
  SetThreadPriority(ThreadHandle, THREAD_PRIORITY_TIME_CRITICAL); }
  QueryPerformanceCounter(Q1);
  QuickSort;
  QueryPerformanceCounter(Q2);
  QueryPerformanceFrequency(CurRate);
  for I := 0 to High - 1 do
    Keys[I] := I;
 { SetThreadPriority(ThreadHandle, THREAD_PRIORITY_NORMAL);                       }
  Time := (Q2-Q1)/CurRate;
  AvgTime := (Time + AvgTime)/2;
  Label8.Caption := FloatToStrF(Time, ffFixed, 9, 7);
  Label9.Caption := FloatToStrF(AvgTime, ffFixed, 9, 7);
  UpdateListView;
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
  Form2.ListView1.Clear;
  Form2.Show;
end;

procedure TForm1.Button6Click(Sender: TObject);
var
  Time: Real;
begin
  {SetPriorityClass(ProcessHandle, REALTIME_PRIORITY_CLASS);
  SetThreadPriority(ThreadHandle, THREAD_PRIORITY_TIME_CRITICAL);    }
  QueryPerformanceCounter(Q1);
  QuickSortKeys;
  QueryPerformanceCounter(Q2);
  QueryPerformanceFrequency(CurRate);
  {SetThreadPriority(ThreadHandle, THREAD_PRIORITY_NORMAL);          }
  Time := (Q2-Q1)/CurRate;
  AvgKeysTime := (Time + AvgKeysTime)/2;
  Label10.Caption := FloatToStrF(Time, ffFixed, 9, 7);
  Label11.Caption := FloatToStrF(AvgKeysTime, ffFixed, 9, 7);
  UpdateListView;
end;

procedure TForm1.FormCloseQuery(Sender: TObject; var CanClose: Boolean);
var
  F: file of TRepertory;
  FK: file of Cardinal;
  I: Integer;
  R: TModalResult;
begin
  CanClose := False;
  R := MessageDlg('Save data base?', mtWarning, [mbYes, mbNo, mbCancel], 0);
  if R = mrYes then
  begin
    AssignFile(F, 'Data Base.db');
    Rewrite(F);
    for I := 0 to High - 1 do
      Write(F, Repertories[I]);
    CloseFile(F);

    AssignFile(FK, 'Data Base.keys');
    Rewrite(FK);
    for I := 0 to High - 1 do
      Write(FK, Keys[I]);
    CloseFile(FK);

    CanClose := True;
  end
  else
  if R = mrNo then
    CanClose := True;
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  F: file of TRepertory;
  FK: file of Cardinal;
begin
  High := 0;
  Randomize;

  AssignFile(F, 'Data Base.db');
  Reset(F);
  while not EOF(F) do
  begin
    Read(F, Repertories[High]);
    Keys[High] := High;
    Inc(High);
  end;
  CloseFile(F);

  Label5.Caption := IntToStr(High);

  High := 0;
  AssignFile(FK, 'Data Base.keys');
  Reset(FK);
  while not EOF(FK) do
  begin
    Read(FK, Keys[High]);
    Inc(High);
  end;
  CloseFile(FK);

  UpdateListView;

  ProcessID := GetCurrentProcessID;
  ProcessHandle := OpenProcess(PROCESS_SET_INFORMATION,
  false, ProcessID);
  ThreadHandle := GetCurrentThread;
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  ListView1.Left := 184;
  ListView1.Width := Form1.ClientWidth - 184 - 5;
  ListView1.Height := Form1.ClientHeight - 5;
end;

end.
