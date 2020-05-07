function y=f2(x)
global FunctionCallCount;

y = 10 / cos(x(1)*x(2) / 10) + 3*x(1)^2 + x(2)^2 + x(1) + 2*x(1)*x(2);

FunctionCallCount=FunctionCallCount+1;

end