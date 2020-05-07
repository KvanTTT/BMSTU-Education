global FunctionCallCount;
x0=[0;-sqrt(5)];
a=1;
eps=0.0001;
fprintf('\n!!!Start\n');
FunctionCallCount=0;
[x_min, x_vect]=gradientDescent(@FunctionF,x0,eps,eps);
fprintf('f1: Сопряженные градиенты %i [', FunctionCallCount);
fprintf('%i ', x_min);
fprintf('] %i \n\n', FunctionF(x_min));

DrawGr(@FunctionF,1,'gradientDescent FunctionF',x_vect,0,4,-3,3,0.1);


FunctionCallCount=0;
[x_min,x_vect]=Newton(@FunctionF,x0,eps,eps);
fprintf('f1: Метод Ньютона: %i [', FunctionCallCount);
fprintf('%i ', x_min);
fprintf('] %i \n\n', FunctionF(x_min));

DrawGr(@FunctionF,2,'Newton FunctionF',x_vect,0,4,-3,3,0.1);


FunctionCallCount=0;
[x_min,x_vect]=Dfp(@FunctionF,x0,eps,eps);
fprintf('f1: ДФП: %i [', FunctionCallCount);
fprintf('%i ', x_min);
fprintf('] %i \n\n', FunctionF(x_min));

DrawGr(@FunctionF,3,'Dfp FunctionF',x_vect,0,4,-3,3,0.1);

x0=[3; 3];
FunctionCallCount=0;
[x_min, x_vect]=gradientDescent(@FunctionFSecondExtended,x0,eps,eps);
fprintf('f2: Сопряженные градиенты %i [', FunctionCallCount);%fprintf('%i ', x_min);
fprintf('] %i \n\n', FunctionFSecondExtended(x_min));
DrawGr(@FunctionFSecondExtended,4,'gradientDescent FunctionFSecondExtended',x_vect,0,5,0,5,1);



FunctionCallCount=0;
[x_min,x_vect]=Newton(@FunctionFSecondExtended,x0,eps,eps);
fprintf('f2: Метод Ньютона %i [', FunctionCallCount);
fprintf('%i ', x_min);
fprintf('] %i \n\n', FunctionFSecondExtended(x_min));

DrawGr(@FunctionFSecondExtended,5,'Newton FunctionFSecondExtended',x_vect,0,5,0,5,0.1);


FunctionCallCount=0;
[x_min,x_vect]=Dfp(@FunctionFSecondExtended,x0,eps,eps);
fprintf('f2: ДФП %i [', FunctionCallCount);
fprintf('%i ', x_min);
fprintf('] %i \n\n', FunctionFSecondExtended(x_min));

DrawGr(@FunctionFSecondExtended,6,'Dfp FunctionFSecondExtended',x_vect,0,5,0,5,0.1);

fprintf('\nEnd\n');



%points=50;
%xr=0:6/points:6;
%yr=0:6/points:6;
%x=zeros(1,points*points);
%y=zeros(1,points^2);
%z=zeros(1,points^2);
%for i = 1:1:points
%    for j = 1:1:points
%        num=(i-1)*points+j;
%        x(num) = xr(i);
%        y(num) = yr(j);
%        z(num) = FunctionFSecondExtended([xr(i), yr(j)]);        
%    end
%end
%figure(7)
%plot3(x,y,z);

