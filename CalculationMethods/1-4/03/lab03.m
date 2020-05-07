function lab03(a, b, e, n)
    global count_of_call;
    count_of_call = 0;
    f = @func;
    [x, gp, qp] = golden_quadratic(f, a, b, e, n);
    fprintf(['fmin = %e\n' ...
             'with x = %e\n' ... 
             'called %d count\n'], f(x), x, count_of_call-1);
    x = a:0.02:b;
    
    y = feval(f, x);
    ygp = feval(f, gp);
    yqp = feval(f, qp);
    plot(x, y, 'm', gp, ygp, 'ro','MarkerFaceColor', 'g', 'MarkerSize', 3);
    hold on
    plot(qp, yqp, 'bs', 'MarkerSize', 6);
 end