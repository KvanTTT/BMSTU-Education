function z = func_1(x)
    global call_cnt;
    z = 2*x(1)^2 + 5*x(2)^2 - 4*x(1)*x(2) - 4*sqrt(5)*x(1) + 4*sqrt(5)*x(2) + 4;
    call_cnt = call_cnt + 1;
end