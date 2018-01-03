program QuadricEqation;

{ программа для решения квадратного уравнения
  6 вариантов ответа                           }

{$APPTYPE CONSOLE}

uses SysUtils;

var A, B, C: Real;  // коэффициенты уравнения
    D: Real;        // дискриминант
    X: Real;        // корень уравнения если D=0
    X1, X2: Real;   // корни уравнения если D>0

begin
  // ввод данных
  WriteLn('AX^2+BX+C=0');
  WriteLn('':100);
  WriteLn('Input coef A:  ');
  ReadLn(A);
  WriteLn('Input coef B:  ');
  ReadLn(B);
  WriteLn('Input coef C:  ');
  ReadLn(C);
  WriteLn('':100);

  //решение
  if A=0 then
  begin
	  if B=0 then
    begin
		  if C=0 then
			  WriteLn('Answer: X - Anything real')
		  else
			  WriteLn('Answer: Nothing roots')
    end
    else
    begin
      X:= -C/B;
      WriteLn('Answer: X=', X:6:4);
      WriteLn('X - Root of linear eqation');
    end
  end
  else
  begin
	  D:= B*B-4*A*C;
	  if D=0 then
    begin
		  X:= -B/(2*A);
		  WriteLn('Answer: X=', X:6:4);
  	end
    else
    begin
      if D>0 then
      begin
    	  X1:= (-B+sqrt(D))/(2*A);
  	    X2:= (-B-sqrt(D))/(2*A);
  	  	WriteLn('Answer: X1=', X1:6:4);
	    	WriteLn('        X2=', X2:6:4);
	    end
      else
        WriteLn('Answer: Nothing roots in real set');
    end;
  end;

  ReadLn;
end.
