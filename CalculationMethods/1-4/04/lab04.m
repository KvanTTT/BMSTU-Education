function lab04(x1, x2, e, h)
    global count_of_call;
    count_of_call = 0;
    f = @func;
    [x, p] = newton(f, x1, x2, e, h);
    fprintf(['fmin = %e\n' ...
             'with x = %e\n' ... 
             'called %d count\n'], f(x), x, count_of_call-1);
    x = x1:0.02:x2;
    y = feval(f, x);
    yp = feval(f, p);
    plot(x, y, p, yp, 'rs', 'MarkerEdgeColor', 'k',...
         'MarkerFaceColor', 'g', 'MarkerSize', 6)
end