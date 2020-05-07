function x=tmp(a)
    global gx;
    global gp; 
    global gf;
    x = gf(gx + gp*a);
end