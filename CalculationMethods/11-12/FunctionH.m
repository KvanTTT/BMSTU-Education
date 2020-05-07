function f = FunctionH(x)
    global FunctionConstraintsCallCount; 
    global gl_FunctionId;

    f1 = h1(x);
    f2 = h2(x);
    f3 = h3(x);
    
    if (gl_FunctionId == 1)
        % Метод штрафных функций
        f = gPenalty(f1) + gPenalty(f2) + gPenalty(f3);
    else
        DefaultNonexistentValue = 1000;
        DefaultAccuracy = 0.0001;
        if (abs(f1) <= DefaultAccuracy || abs(f2) <= DefaultAccuracy || abs(f3) <= DefaultAccuracy)
            f = DefaultNonexistentValue;
        else
            f = 1 / f1 + 1 / f2 + 1 / f3; 
        end
    end
    FunctionConstraintsCallCount = FunctionConstraintsCallCount + 1;
end

function g = gPenalty(fx)
    g = ((fx + abs(fx)) / 2) ^ 2;
end


function h = h1(x)
    h = -x(2) - 1 ;
end

function h = h2(x)
    h = 3*x(1) + 3*x(2) - 1;
end

function h = h3(x)
    h = x(1)^2 + x(2)^2 - 4;
end