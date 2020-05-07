function lab01(a, b, e)
    global count_of_call;
    count_of_call = 0;
    f = @func;
    [y, x, p] = bitwise_search(f, a, b, e);
    fprintf(['fmin = %e\n' ...
             'with x = %e\n' ... 
             'called %d count\n'], y, x, count_of_call);
    x = a:0.01:b;
    y = feval(f, x);
    yp = feval(f, p);
    plot(x, y,'m', p, yp, 'ro', ...
         'MarkerFaceColor', 'g', 'MarkerSize', 3)
end