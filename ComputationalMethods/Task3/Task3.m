function u = Task3(countIt)
eps = 1e-04; 
a = 1;
b = 1;  
n = 51; 

u2 = puasson(a, b, 2*n - 1, eps, countIt);
u = puasson(a, b, n, eps, countIt);

err = zeros(n, n);
for i = 1:n
    for j = 1:n
        err(i,j) = abs(u(i,j) - u2(2*i-1, 2*j-1))/3;
    end;
end;

mx = max(err(:));
av = mean(err(:));

[y x] = meshgrid(0:b/(n-1):b, 0:a/(n-1):a);

surf(y, x, u);   
xlabel('Y','FontSize', 14);  
ylabel('X','FontSize', 14);

fprintf('Max error: %6.6f\n', mx);
fprintf('Avg error: %6.6f\n', av);

function [u] = puasson(a, b, n, eps, countIt)
u = zeros(n);   

hX = a / (n - 1);  
hY = b / (n - 1);

[y x] = meshgrid(0 : hY : b, 0 : hX : a);   

u(1, :) = ux0(y(1, :));    
u(n, :) = uxa(y(n, :));
u(:, 1) = uy0(x(:, 1));
u(:, n) = uyb(x(:, n));

tmpU = u; 

x = 0 : hX : a;
y = 0 : hY : b;

w = 2 / (1+sin(pi/(n-1)));    

tmp = 2 * (hX^2 + hY^2);
kY = hY^2 / tmp;    
kX = hX^2 / tmp; 
kXY =  hY^2 * hX^2 / tmp;
    
for Iter = 1:countIt    
    for i = 2:n - 1
        for j = 2:n - 1
            half = kY * (tmpU(i-1,j) + u(i+1,j)) + ...
                   kX * (tmpU(i,j-1) + u(i,j+1)) + kXY * func(x(i),y(j));
            tmpU(i,j) = w * half + (1 - w) * u(i,j);
        end;
    end;
    
    norma = norm(tmpU - u, 1);
    u = tmpU;
    if norma < (2 - w) * eps 
        fprintf('Iter: %d\n', Iter);
        break;
    end;
end;

function res = ux0(y)
    res = y;

function res = uxa(y)
    res = y .^ 2;

function res = uy0(x)
    res = 2 .* x .* (1 - x);

function res = uyb(x)    
    res = 1;
    
function res = func(x, y)
    res = 0;
