function [xmin, golden_points, quad_points] = ...
                                            golden_quadratic(f, a, b, e, n)
   
    fl = 0;
    
    golden_points = [];
    quad_points = [];
    
    fa = [];
    fb = [];
    while (~fl)
        [xmin, pnts, fl, x1, x3, x2, fa, fb, f_addl] = ...
                                            golden(f, a, b, fa, fb, e, n);
        golden_points = [golden_points pnts];
        if (fl)
            break;
        end
               
        [xmin, pnts, fl, a, b, fa, fb] = ...
                                quadratic(f, x1, x2, x3, fa, f_addl, fb, e, n);
        quad_points = [quad_points pnts];
    end
end
 
% передаем значения функций, чтобы не вычислять их еще раз
function [xmin, points, fl, x1, x3, f1, f3] = ...
                       quadratic(f, x1, x2, x3, f1, f2, f3, e, iteration_count)
        
    points = [];
    points(:, end + 1) = [x1; x2; x3];
    
    it = 0;
    fl = 0;
    while (iteration_count > it)
        diff23 = x2 - x3;
        diff31 = x3 - x1;
        diff12 = x1 - x2;
        p1 = f1 / (diff12*(-diff31));
        p2 = f2 / ((-diff12)*diff23);
        p3 = f3 / (diff31*(-diff23));
        a = p1 + p2 + p3;
        b = f1*(x2 + x3) / diff12 / diff31 + f2*(x3 + x1) / diff12 / diff23...
            + f3*(x1 + x2) / diff31 / diff23;
        x0 = -b / 2 /a; % из необх. усл. экстремума y' = 2ax+b = 0
        f0 = f(x0);
        if (x0 >= x2)
            if (f0 <= f2)
                x1 = x2;
                x2 = x0;
                f1 = f2;
                f2 = f0;
            else
                x3 = x0;
                f3 = f0;
            end
        else
            if (f0 <= f2)
                x3 = x2;
                x2 = x0;
                f3 = f2;
                f2 = f0;
            else
                x1 = x0;
                f1 = f0;
            end
        end
        points(:, end + 1) = [x1; x2; x3];
        if (x3 - x1 < e)
            fl = 1;
            break;
        end
        it = it + 1;
    end
    
    xmin = x2;
end

function [xmin, points, fl, a, b, x1, fa, fb, f1] = ... 
                                    golden(f, a, b, fa, fb, e, iteration_count)
    psi = 5 ^ 0.5 / 2 - 0.5; 
    x1 = b - psi * (b - a);
    x2 = a + psi * (b - a);
    a_b_next = (b - a) / 2;

    points = [a; b];
    
    f1 = [];
    fl = 1;
    if (a_b_next > e) 
        f1 = f(x1);
        f2 = f(x2);
        
        it = 0;
        fl = 0;
        while (iteration_count > it)
           if (f1 <= f2)
               b = x2;
               fb = f2;
               x2 = x1;
               f2 = f1;
               x1 = b - psi * (b - a);
               f1 = f(x1);
           else
               a = x1;
               fa = f1;
               x1 = x2;
               f1 = f2;
               x2 = a + psi * (b - a);
               f2 = f(x2);
           end
           points(:, end + 1) = [a, b];
           a_b_next = psi * a_b_next;
           if (a_b_next < e)
               fl = 1;
               break;
           end
           it = it + 1;
        end
    end
    
    xmin = (a + b) / 2;
end