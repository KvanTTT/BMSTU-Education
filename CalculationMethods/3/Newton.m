function [x_min,x_vect]=Newton(f,x0,eps,h)
    x_vect = cell(0, 1);   
    fx = f(x0);
    x_vect{end+1} = x0;
    x=x0;
    while (1)
        %gradxx
        mH(1,1) = (fx - 2 * f([x(1)-h, x(2)]) + f([x(1)-2*h, x(2)])) / h ^ 2;
        %gradxy
        mH(1,2) = (fx - f([x(1)-h, x(2)])- f([x(1), x(2)-h]) + f([x(1)-h, x(2)-h])) / h ^ 2;
        %gradyx
        mH(2,1) = mH(1, 2);
        %gradyy
        mH(2,2) = (fx - 2 * f([x(1), x(2)-h]) + f([x(1), x(2)-2*h])) / h ^ 2;

        %grad
        w = [(fx - f([x(1)-h, x(2)])) / h, (fx - f([x(1), x(2)-h])) / h];
        if (sqrt(sum(w.^2)) < eps)
            break;
        end;  
        
        tmp = x;
        if (~isposdef(mH))
            x = x - w';
        else
            x = x - mH\w';                
        end;
        fx = f(x);
        x_vect{end+1} = x;
        
        if (sqrt(sum((x-tmp).^2)) < eps)
            break;
        end;
    end;
    x_min=x_vect{end};
end

function [result] = isposdef(M)
% Проверка положительной определённости матрицы М
    result = true;
    for i=1:length(M)
      if ( det( M(1:i, 1:i) ) <= 0 )
        result = false;
        break;
      end
    end
end