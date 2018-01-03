unit NewRecord;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, Spin, StdCtrls, Buttons, ComCtrls;

type
  TForm3 = class(TForm)
    Edit1: TEdit;
    Edit2: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    GroupBox1: TGroupBox;
    RadioButton1: TRadioButton;
    RadioButton2: TRadioButton;
    RadioButton3: TRadioButton;
    ComboBox1: TComboBox;
    Label3: TLabel;
    Composer: TLabel;
    Edit3: TEdit;
    Bevel1: TBevel;
    BitBtn1: TBitBtn;
    BitBtn2: TBitBtn;
    Edit4: TEdit;
    UpDown1: TUpDown;
    procedure BitBtn1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form3: TForm3;

implementation

{$R *.dfm}

procedure TForm3.BitBtn1Click(Sender: TObject);
begin
  Edit1.Text := Trim(Edit1.Text);
  Edit2.Text := Trim(Edit2.Text);
  Edit3.Text := Trim(Edit3.Text);
  if (Edit1.Text = '') or (Edit2.Text = '') then
  begin
    ShowMessage('One of more unvariant fields are empty! (fill it)');
    ModalResult := mrNone;
    Exit;
  end;
  if (not RadioButton1.Checked) and (not RadioButton2.Checked)
    and (not RadioButton3.Checked) then
  begin
    ShowMessage('Don''t checked performance type');
    ModalResult := mrNone;
    Exit;
  end;
  if RadioButton2.Checked and (ComboBox1.ItemIndex = -1) then
  begin
    ShowMessage('Don''t checked adult performance type');
    ModalResult := mrNone;
    Exit;
  end;
  if RadioButton3.Checked and (Edit3.Text = '') then
  begin
    ShowMessage('Don''t entered composer');
    ModalResult := mrNone;
    Exit;
  end;


end;

end.
