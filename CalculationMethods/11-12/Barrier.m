function [xExtremum, fExtremum, points] = Barrier(f, h, x0, accuracy)
    global gl_FunctionId;

    fprintf('\nметод барьерных функций\n');
    gl_FunctionId = 2;

    points = [];
       
    x = x0;
    points{end+1} = x0;
    conditionExit = 0;
    
    
    global ff;
    ff = f;
    global fh;
    fh = h;
    global r;
    
    r = 20000;
    
    while (~conditionExit)
        tx = x;
        x = fminsearch(@fn, tx, optimset('TolFun', accuracy));
        points{end + 1} = x;
        
        conditionExit = (sum((tx - x).^2)^0.5 < accuracy);
        if (~conditionExit)
            r = r / 2.87;
        end
    end
    
    xExtremum = x;
    fExtremum = f(x);
end

function z = fn(x)
    global ff;
    global fh;
    global r;
    z = ff(x) - r * fh(x);
end

