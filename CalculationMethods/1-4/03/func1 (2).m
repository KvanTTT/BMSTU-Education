function y = func(x)
    global call_cnt
    y = (4 * x.^3 + 2 * x.^2 - 4 * x + 2) .^ (2 ^ 0.5) ...
         + asin((-x .* x + x + 5) .^ (-1)) - 5;
    call_cnt = call_cnt + 1;
end