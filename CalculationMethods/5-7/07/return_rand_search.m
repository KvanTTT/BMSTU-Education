function [x, pnts] = return_rand_search(f, x0, alpha, eps, rand_n)
    %rand_n = 10; 
    un_cycle = 500;
    delta = 0.5;
    N = 2; %dimensions
    pnts = cell(0, 1);

    xk = x0;

    j = 1;              %кол-во попыток перемещения из последней удачной т.
    fk = f(xk);
    pnts{end+1} = xk;

    epsf = 1e-4;
%     eps = 1e-3;
  
    while true
        r = rand(N);
        xNew = xk + alpha * r / norm(r);
        fNew = f(xNew);
        df = abs(fNew-fk);
        un_cycle = un_cycle-1;
     
        if (fNew < fk)
            xk = xNew;
            fk = fNew;
            pnts{end+1} = xk;          
            j = 0;      
        else
            if (j < rand_n)
                j = j + 1;
            elseif((alpha >= eps)||(df >= epsf))        %много неудачных попыток перемещения
                alpha = alpha * delta;
                j = 1;
            end
        end
        if ((alpha < eps)&&(df < epsf)||(un_cycle==0))%
          x = xk;
          return;
        end   
    end
end

function [r] = rand(N)
    r = 2*randn(1, N) - ones(1, N); %[1x2]in[-1;1]
%%     r = random('Normal', 0,1,1,2);
 	r=r(1,:)./norm(r);
end