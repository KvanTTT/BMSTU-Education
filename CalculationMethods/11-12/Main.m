function Main
    global gl_FunctionId;

    %clear;
    %clc;

    TestKunTaker((1.0252186 + 1.0115953) / 2, (-0.6918790 + -0.6782286) / 2);
    
    RunMinimization();
    
    %gl_FunctionId = 1;
    %DrawFirstFunction(@FunctionH);
end

function y = TestKunTaker(x1, x2)
    y(1) = 4 * x1 - 4 * x2 - 4 * sqrt(5);
    y(2) = 10 * x2 - 4 * x1 + 4 * sqrt(5);

    fprintf('fX1=%f, fX2=%f', y(1), y(2));
end

function RunMinimization()
    global gl_H;
    
    gl_H = 1e-4;
    
    functionF = @FunctionF; 
    functionH = @FunctionH;
    methodDrawContour = @DrawContour;  
    
    xPen = [0; 2 ];
    xBar = [-1; 1 ];

    accuracy = 1.e-3;
    
    Run(@Penalty, functionF, functionH, xPen, accuracy, methodDrawContour);
    Run(@Barrier, functionF, functionH, xBar, accuracy, methodDrawContour);

end

function Run(method, functionF, functionH, x0, accuracy, methodDrawContour)
    global FunctionCallCount;
    global FunctionConstraintsCallCount;
    
    [x, f, xPoints] = Calculate(method, functionF, functionH, x0, accuracy);
    
    fprintf('Точность = %f\n', accuracy);
    fprintf('Количество вычислений функции f(x) = %d  hi(x) = %d\n', FunctionCallCount, FunctionConstraintsCallCount);
    fprintf('x = %.7f\n', x);
    fprintf('f(x) = %.7f\n', f);

    DrawResult(functionF, xPoints, methodDrawContour);
end

function [x, f, xPoints] = Calculate(method, functionF, functionH, x0, accuracy)
    global FunctionCallCount;
    global FunctionConstraintsCallCount;
    
    FunctionCallCount = 0;
    FunctionConstraintsCallCount = 0;
    [x, f, xPoints] = method(functionF, functionH, x0, accuracy);
end

function DrawResult(functionF, points, methodDrawContour)
    N = 50;
    
    %hold on;
    figure(1);

    
    xp = [];
    yp = [];
    for i = 1:length(points)
        xp(end+1) = points{i}(1);
        yp(end+1) = points{i}(2);
    end;   
    
    hold on;    
    
    plot(xp, yp, 'r.');
            
    methodDrawContour(functionF);
    methodDrawContour(@FunctionH);
    
    %DrawLines(points);

    hold off;

end

function DrawContour(functionF)
    interval = 0.2;
    xBegin = -12;
    xEnd = 2;
    yBegin = -8;
    yEnd = 2;
    
    x = xBegin : interval : xEnd;
    y = yBegin : interval : yEnd;
    for i = 1 : length(x)
        for j = 1 : length(y)
            z(j, i) = functionF([x(i); y(j)]);
        end
    end
    % линии уровня
    levels = -15 : 2 : 50;
    contour(x, y, z, levels);
end


function DrawLines(points)
    if (~isempty(points))       
        i = 1;
        while i < length(points)
            %line([points{i}(1) previousPoint(1)], [points{i}(2) previousPoint(2)], 'color', 'red', 'LineWidth', 1.5);
            line([points{i}(1) points{i+1}(1)], [points{i}(2) points{i+1}(2)], 'LineStyle', '-');
            i = i + 1;
            pause(0.3);
        end;   
    end;
end

%%%%%%%%%%%%%%%

function DrawFirstFunction(functionF)
    x = -12 : 0.2 : 2;
    y = -8 : 0.2 : 2;
    for i = 1 : length(x)
        for j = 1 : length(y)
            z(j, i) = functionF([x(i); y(j)]);
        end
    end
    figure(2);
    mesh(x,y,z);    
end

%%%%%%%%%%%%%%%%%
