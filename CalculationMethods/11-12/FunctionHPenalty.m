function f = FunctionHPenalty(x)
    global FunctionConstraintsCallCount; 

    DefaultNonexistentValue = 1000;
    DefaultAccuracy = 0.0001;
    
    f1 = h1(x);
    f2 = h2(x);
    f3 = h3(x);
    
    f = gPenalty(f1) + gPenalty(f2) + gPenalty(f3);
        
    FunctionConstraintsCallCount = FunctionConstraintsCallCount + 1;
end

function g = gPenalty(fx)
    g = ((fx + abs(fx)) / 2) ^ 2;
end

function h = h1(x)
    h = -x(1) - 1;
end

function h = h2(x)
    h = x(1) - 2 * x(2) - 1;
end

function h = h3(x)
    h = x(1)^2 + x(2)^2 - 9;
end