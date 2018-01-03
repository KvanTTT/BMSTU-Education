program ProjectRecords;

uses
  Forms,
  UnitMain in 'UnitMain.pas' {Form1},
  NewRecord in 'NewRecord.pas' {Form3},
  UnitSearchResult in 'UnitSearchResult.pas' {Form2};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.CreateForm(TForm3, Form3);
  Application.CreateForm(TForm2, Form2);
  Application.Run;
end.
