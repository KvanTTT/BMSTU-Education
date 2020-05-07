function [x_min,x_vect]=Dfp(f,x0,eps,h)
    x_vect = cell(0, 1);   
    x=x0;
    fx = f(x);
    x_vect{end+1} = x;
    
    k = 1;        
    A = eye(2);    
    while(1)
        w = -grad(f,x,fx,h);

        if (sqrt(sum(w.^2)) < eps)
            break;
        end;

        p = A * w;
       
        xn = find_min(f, x, p, eps);
        fx = f(xn);        

  
            dx = xn - x;
            prev_w = w;
            w = -grad(f, xn, fx, h);
            dw = w - prev_w;
            A = A - (dx*dx')/(sum(dw.*dx))- A*dw*(dw')*A / sum((A*dw).*dw);            
            if (sqrt(sum(dx.^2)) < eps)
                break;
            end;
          
        
        x = xn;
        k = k + 1;
        x_vect{end+1} = x;
    end;
    x_min=x_vect{end};
end