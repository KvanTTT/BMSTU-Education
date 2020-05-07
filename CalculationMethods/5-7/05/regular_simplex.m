% метод минимизации по правильному симплексу
function [res, pnts] = regular_simplex(f, x0, a, e)
    [x, fv] = new_simplex(f, x0, a);
    [x, fv] = sort_rev(x, fv);
   
    pnts = cell(1);
    pnts{1} = x{1};
    pnts{2} = x{2};
    pnts{3} = x{3};
    
    while (1)       
        xn = - x{3} + x{2} + x{1};  %в x{3} - худший результат
        fn = f(xn);                 %отображенна¤ точка
        if (fn < fv(3))             %отображение удачно
            x{3} = xn;
            fv(3) = fn;
            pnts{end+1} = x{3};
        else                        %ст¤гиваемс¤ к лучшей вершине - x{1}
            [x, fv, a] = compress_simplex(x, fv, f, a); 
            pnts{end+1} = x{2};
            pnts{end+1} = x{3};
        end        
        [x, fv] = sort_rev(x, fv);
        ev  = ((fv(1)-fv(2))^2 + (fv(1)-fv(3))^2)/2; 
        if (abs(a) < e && ev < e)
            break;
        end
    end
    res = x{1};
end

function [xN, fN] = sort_rev(x, fv)
   [fN, ind] = sort(fv);
   xN = x(ind);
end

function [x, fv] = new_simplex(f, x0, a)
    x = cell(3, 1);
    x{1} = x0;
    d1 = a * (3^0.5 - 1) / 2^1.5;
    d2 =  a * (3^0.5 + 1) / 2^1.5;
    x{2} = x{1} + [d2;d1]; %x{2}=x1 => i = 1
    x{3} = x{1} + [d1;d2]; %x{3}=x2 => i = 2

    fv(1) = f(x{1});
    fv(2) = f(x{2});
    fv(3) = f(x{3});
end

function [x, fv, a] = compress_simplex(x, fv, f, a)
    a = a / 2;
    x{2} = (x{1} + x{2}) / 2;
    x{3} = (x{1} + x{3}) / 2;
    fv(2) = f(x{2});
    fv(3) = f(x{3});
end