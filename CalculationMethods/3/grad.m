function w = grad(f, x, fx, h)
    w = [(fx - f([x(1)-h, x(2)])) / h; (fx - f([x(1), x(2)-h])) / h];
end