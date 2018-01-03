program Project1;

{$APPTYPE CONSOLE}

var
  XMin, XMax: Real;
  XStep: Real;
  XCur: Real;
  H: Real;
  HMin, HMax: Real;
  HStep: Real;
  E: Integer;
  Mark: Real;
  I: Integer;
  C, C1: Real;

begin

  WriteLn('Enter start and finish value:');
  ReadLn(XMin, XMax);
  WriteLn('Enter step:');
  ReadLn(XStep);

  XCur := XMin;
  HMin := XMin*XMin - 9;
  HMax := XMin*XMin - 9;
  while XCur <= XMax do
  begin
    H := XCur*XCur - 9;
    if HMin < H then
      HMin := H;
    if HMax > H then
      HMax := H;
    XCur := XCur + XStep;
  end;

  WriteLn('Graphic of H function:');
  Write('':4);
  HStep := (HMax - HMin)/8;
  for I := 0 to 8 do
  begin
    Mark := HMin + HStep*I;
    E := 0;
    if (Abs(Mark) > 1e4) or (Abs(Mark) < 1e-3) then
    begin
      while Abs(Mark) > 10 do
      begin
        Mark := Mark / 10;
        Inc(E);
      end;
    while (Abs(Mark) < 1) and (Mark <> 0) do
      begin
        Mark := Mark * 10;
        Dec(E);
      end;
    end;
    if E = 0 then
      Write(Mark:5:2,' ':3)
    else
      Write(Mark:3:2,'E',E,' ');
  end;

  WriteLn('');
  Write('  ':7, #218);
  for I := 1 to 64 do
    if I mod 8 = 0 then
      Write(#197)
  else
    Write(#196);
  Write(#26);
  WriteLn('');

  XCur := XMin;
  HStep := (HMax-HMin)/64;
  while XCur <= XMax do
  begin
    Write(' ', XCur:6:2, #197);
    H:= Trunc((XCur*XCur-9-HMin)*64/(HMax-HMin));
    C := HMin;
    for I := 0 to 64 do
    begin
      C1 := C;
      C := C + HStep;
      if C * C1 <= 0 then
        Write(#179)
      else
      if I = H then
        Write(#15)
      else
      Write(' ');
    end;
    WriteLn('');
    XCur:= XCur + XStep;
  end;
  WriteLn('  ':7, #25);

  ReadLn;
end.
