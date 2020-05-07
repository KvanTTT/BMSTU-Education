function fmin(f, x0, eps)
    global call_cnt;
    call_cnt = 0;
    [x, y] = fminsearch(f, x0, optimset('TolX', eps));
    fprintf(['min function value %4.4f\n' ...
             'with arg [%4.4f, %4.4f]\n' ... 
             'function was called %d count\n'], y, x, call_cnt);
         
         
end