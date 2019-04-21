function U = Task2()
    h = 2e-2;
    tau = 1e-2;
    
    T = 1;
    L = 1;
        
    N = T / tau;
    M = L / h;
    
    U = calc(h, tau, N+1, M+1);
    
    x = 0:h:L;
    t = 0:tau:T;
    
    [XG TG] = meshgrid(t, x);
    surf(XG, TG, U);   

    xlabel('t','FontSize',14);  
    ylabel('x','FontSize',14);
    
    h = h/2;
    tau = tau/2;
    N = T/tau;
    M = L/h;
    U2 = calc(h, tau, N+1, M+1);
    
    [nn mm] = size(U);
    Z = zeros(nn, mm);

    for i = 1:nn
        for j = 1:mm
            Z(i,j) = abs(U(i, j) - U2(2*i - 1, 2*j - 1)) / 3;
        end;
    end;
    
    fprintf('Max error: %6.6f\n', max(Z(:)));
    fprintf('Avg error: %6.6f\n', mean(Z(:)));

function U = calc(h, tau, N, M)
    U = zeros(M,N);   
    a = 1;
    
    cx = 0;
    for i=1:M
        U(i,1) = fi(cx);
        cx = cx + h;
    end;
    
    cx = h;
    for i=2:M-1
        U(i,2) = tau*psi(cx) + U(i,1);
        cx = cx + h;
    end;
    
    ct = tau;
    for j=2:N
        U(1,j) = m(ct);
        U(M,j) = n(ct);
        ct = ct + tau;
    end;
    
    for j=2:N-1
        for i=2:M-1
            coef = a * a * tau * tau / (h*h);
            U(i,j+1) = 2*U(i,j) - U(i,j-1) + coef * (U(i-1,j) - 2*U(i,j) + U(i+1,j));
        end;
    end;

function y = fi(x)
    y = (1-x) * cos(pi * x/2);

function y = psi(x)
    y = 2*x + 1;
   
function y = m(t)
    y = 1 + t - t^2;

function y = n(t)
    y = 0*t;