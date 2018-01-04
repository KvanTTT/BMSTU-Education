program Horizont;

uses
  Forms,
  Main in 'Main.pas' {MainForm},
  Grids in 'Grids.pas' {Grid},
  Algorithm in 'Algorithm.pas';

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TMainForm, MainForm);
  Application.CreateForm(TGrid, Grid);
  Application.Run;
end.
