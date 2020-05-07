function y=f1(x)
global FunctionCallCount;
y = 2*x(1)^2 + 5*x(2)^2 - 4*x(1)*x(2) - 4*sqrt(5)*x(1) + 4*sqrt(5)*x(2) + 4;
FunctionCallCount=FunctionCallCount+1;
end