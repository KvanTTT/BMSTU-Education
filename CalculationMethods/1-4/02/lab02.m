function lab02(a, b, e)
    global count_of_call;
    count_of_call = 0;
    f = @func;
    [x, p] = golden_ratio(f, a, b, e);
    fprintf(['fmin = %e\n' ...
             'with x = %e\n' ... 
             'called %d count\n'], f(x), x, count_of_call-1);
    x = a:0.01:b;
    y = feval(f, x);
    yp = feval(f, p);
    plot(x, y, 'm', p, yp, 'ro', ...
         'MarkerFaceColor', 'g', 'MarkerSize', 3)
end