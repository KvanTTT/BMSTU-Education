function lab07(f, x0, a, e, n)
    global call_cnt;
    call_cnt = 0;
    [x, p] = return_rand_search(f, x0, a, e, n);
    fprintf(['min function value %4.4f\n' ...
             'with arg [%4.4f, %4.4f]\n' ... 
             'function was called %d count\n'], f(x), x, call_cnt-1);
    xp = [];
    yp = [];
    for i = 1:length(p)
        xp(end+1) = p{i}(1);
        yp(end+1) = p{i}(2);
    end;   
    
    plot(xp, yp, 'r.');
    hold on;
    for i = 1:length(xp)
        text(xp(i), yp(i), int2str(i))
%         disp(f([xp(i); yp(i)]));
    end
    
    x = -7:0.2:7;
    y = -7:0.2:7;
    for i = 1:length(x)
        for j = 1:length(y)
            z(j, i) = f([x(i); y(j)]);
        end
    end
    levels = -56:3:237;
    contour(x, y, z, levels);
end