function x = find_min(f, x, p, e)
    global gx; 
    global gp; 
    global gf;
    gx = x;
    gp = p;
    gf = f;
    tmp=@(a)gf(gx + gp*a);
    par = fminunc(tmp, 0, optimset('TolX', e, ...
        'TolFun', e, 'LargeScale', 'off', 'Display', 'off'));
    x = x + p * par;
    
end