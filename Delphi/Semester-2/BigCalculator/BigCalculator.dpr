program BigCalculator;

uses
  Forms,
  UnitMain in 'UnitMain.pas' {Form1},
  UnitBigNumbers in 'UnitBigNumbers.pas' {Form2};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.CreateForm(TForm2, Form2);
  Application.Run;
end.
