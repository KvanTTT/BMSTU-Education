function [x_min,x_vect]=gradientDescent(f,x0,eps,a)
 x_vect = cell(0, 1);
    
    fx = f(x0);
    x_vect{end+1} = x0;
    x=x0;
    p = 0;
    h=a;
    while (1)
        w = -grad(f, x, fx, h);
        if (sqrt(sum(w.^2)) < eps)
            break;
        end;
        
        if (p == 0)
            g = 0;
        else
            g = sum(w.^2) / sum(wpr.^2);
        end
        
        p = g * p + w;
        wpr = w;
        
        tmp = x;
        x = find_min(f, x, p, eps);        
        fx = f(x);
        x_vect{end+1} = x;
        if (sqrt(sum((x-tmp).^2)) < eps)
            break;
        end;
    end
    x_min=x_vect{end};
end