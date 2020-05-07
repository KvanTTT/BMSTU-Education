function [xmin, points] = newton(f, x1, x2, e, h)
    alpha = 1;
    points = [x2];
    xk = x2;
    x_prev = x2-2*e;
 
    while (abs(xk-x_prev) > e)    
        f_x_prev = f(xk-h);
        f_x = f(xk);
        f_x_next = f(xk + h);
        
         
        fx_prev = (f_x - f_x_prev) / h;
        fx = (f_x_next - f_x) / h;         
        fxx = (fx - fx_prev)/h;
        
        %fx = (f_x_next - f_x_prev) / (2 * h);
        %fxx = (f_x_next - 2 * f_x + f_x_prev) / (h * h);
        
        x_prev = xk;
        xk = xk - alpha * fx / fxx;
  
        while((xk<x1)||(xk>x2)) 
            fprintf('x[k+1]=%e не принадлежит (%e, %e) при alpha = %e\n', xk, x1,x2, alpha);
            alpha = alpha/2;
            xk = x_prev - alpha * fx / fxx;
        end;
        points(end+1) = xk;
    end
    
    xmin = (xk + x_prev) / 2;
end

