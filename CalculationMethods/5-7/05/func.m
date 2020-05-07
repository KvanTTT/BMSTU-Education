%z = (log(31-x(1)^2-x(2)^2))^2 +2*x(1)^2+2*x(1)*x(2)+x(2)^2
function z = func(x)
    global call_cnt;
%     if (31<=x(1)^2+x(2)^2)
%         z = inf;
%     else
%       z = (log(31-x(1)^2-x(2)^2))^2 +2*x(1)^2+2*x(1)*x(2)+x(2)^2;%;  
%     end

%     if(49-x(1)^2-x(2)^2 <=0)
%        z = 1/(1e-2)+x(1)-x(2);
%     else
%         z = 1/(sqrt(49-x(1)^2-x(2)^2))+x(1)-x(2);
%     end
    th = 1e-3;
    if(x(1)*x(2)<=th)
		z = 10 / cos(th / 10) + 3*x(1)^2 + x(2)^2 + x(1) + 2*th;
    else
		z = 10 / cos(x(1)*x(2) / 10) + 3*x(1)^2 + x(2)^2 + x(1) + 2*x(1)*x(2);
    end
    call_cnt = call_cnt + 1;
end