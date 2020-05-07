function y=f2(x)
global FunctionCallCount;



th = 1e-4;
if(cos(x(1)*x(2) / 10) <= th)
	y = 10 / x(1)*x(2) + 3*x(1)^2 + x(2)^2 + x(1) + 2 * acos(th) * 10;
else
	y = 10 / cos(x(1)*x(2) / 10) + 3*x(1)^2 + x(2)^2 + x(1) + 2*x(1)*x(2);
end

FunctionCallCount=FunctionCallCount+1;

end