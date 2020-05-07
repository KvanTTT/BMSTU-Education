function embed(a, b, e)
    global count_of_call;
    count_of_call = 0;
    [x, y] = fminbnd(@func, a, b, optimset('TolX', e));
    fprintf(['fmin = %e\n' ...
             'with x = %e\n' ... 
             'called %d count\n'], y, x, count_of_call-1);
end