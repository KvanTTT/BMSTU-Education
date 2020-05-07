function [xmin, points] = golden_ratio(f, a, b, e)
    psi = 5 ^ 0.5 / 2 - 0.5; 
    x1 = b - psi * (b - a);
    x2 = a + psi * (b - a);

    a_b_next = (b - a) / 2;

    points = [a; b];

    if (a_b_next > e) 
        f1 = f(x1);
        f2 = f(x2);

        while (a_b_next > e)
           if (f1 <= f2)
               b = x2;
               x2 = x1;
               f2 = f1;
               x1 = b - psi * (b - a);
               f1 = f(x1);
           else
               a = x1;
               x1 = x2;
               f1 = f2;
               x2 = a + psi * (b - a);
               f2 = f(x2);
           end
           points(:, end + 1) = [a, b];
           a_b_next = psi * a_b_next;
        end
    end
    
    xmin = (a + b) / 2;
end
