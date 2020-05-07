% метод нахождени¤ минимума нерегул¤рным симплексом
function [res, pnts] = deform_simplex(f, x0, a, e, n)   
    k = [2, 1, 0.5, -0.5]; 
    % раст¤жени¤, отражени¤, сжати¤, редукции
    
    [x, fv] = create_simplex(f, x0, a);
    [x, fv] = sort_f(x, fv);
    
    pnts = cell(1);
    pnts{1} = x{1};
    pnts{2} = x{2};
    pnts{3} = x{3};
    
    nmb_it = 1;
    while (1)
        if (mod(nmb_it, n) == 0)
            [x, fv] = create_simplex(f, x0, a);
            pnts{end+1} = x{1};
            pnts{end+1} = x{2};
            pnts{end+1} = x{3};
        end
        
        xc = (x{1} + x{2}) / 2;
        p = xc - x{3};  
                
        fl = 1;
        for i = 1:length(k)
            xn = xc + k(i) * p;
            fn = f(xn);
            if fn < fv(3)
                x{3} = xn;
                pnts{end+1} = x{3};
                fv(3) = fn; 
                fl = 0;
                break;
            end
        end
            
        if (fl)
            [x, fv, a] = compress_simplex(x, fv, f, a); 
            pnts{end+1} = x{1};
            pnts{end+1} = x{2};
            pnts{end+1} = x{3};
        end        
        
        [x, fv] = sort_f(x, fv);
        ev  = ((fv(1)-fv(2))^2 + (fv(1)-fv(3))^2)/2; 
        if (max([norm(x{1}-x{2}), ...
			    norm(x{3}-x{2}), ...
				norm(x{1}-x{3})]) < e...
                && ev < e)
            break;
        end
    end
    res = x{1};
end

% сортирует переданные массивы
% по второму из них по убыванию
function [xn, fn] = sort_f(x, f)
   [fn, ind] = sort(f);
   xn = x(ind);
end

% сжимает симплекс к наименьшей вершине
% считаетс¤, что вершины отсортированы по убыванию
function [x, fv, a] = compress_simplex(x, fv, f, a)
    a = a / 2;
    x{2} = (x{1} + x{2}) / 2;
    x{3} = (x{1} + x{3}) / 2;
    fv(2) = f(x{2});
    fv(3) = f(x{3});
end

% строит симплекс из заданной точки,
% вершины симплекса не отсортированы
function [x, fv] = create_simplex(f, x0, a)
    x = cell(3, 1);
    x{1} = x0;
    d1 = a * (3^0.5 - 1) / 2^1.5;
    d2 =  a * (3^0.5 + 1) / 2^1.5;
    x{2} = x{1} + [d1; d2];
    x{3} = x{1} + [d2; d1];
    fv(1) = f(x{1});
    fv(2) = f(x{2});
    fv(3) = f(x{3});
end