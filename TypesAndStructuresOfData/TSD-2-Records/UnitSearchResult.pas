unit UnitSearchResult;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls;

type
  TForm2 = class(TForm)
    ListView1: TListView;
    Button1: TButton;
    Label1: TLabel;
    Edit1: TEdit;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form2: TForm2;

implementation

{$R *.dfm}

uses UnitMain;

procedure TForm2.Button1Click(Sender: TObject);
var
  I: Integer;

begin
  Edit1.Text := Trim(Edit1.text);
  if Edit1.Text = '' then
    Exit;
  ListView1.Clear;

  for I := 0 to High-1 do
  begin
    if (Repertories[Keys[I]].PerformType = Music) and
      (Repertories[Keys[I]].Composer = Edit1.Text) then
    begin
      ListView1.Items.AddItem(Form1.RepertoryToListItem(Repertories[Keys[I]]));
    end;

  end;


end;

end.
