function [fmin, xmin, points] = bitwise_search(f, a, b, e)

    points = [];

    d = (b-a) / 4;
    x0 = a;
    f0 = f(x0);

    while 1       
        x1 = x0 + d;
        f1 = f(x1);        
        x0 = x1;
            
        if(f0 < f1||a >= x0 || x0 >= b) 
            f0 = f1;
            if (abs(d) < e)
                break;
            end
            d = -d / 4; 
        end
        points(end + 1) = x0;
        f0 = f1;
    end

    fmin = f0;
    xmin = x0;

end



