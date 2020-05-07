function y = func(x)
    global count_of_call
	y = asin((35*x.^2 - 30*x + 9)/20) + cos((10*x.^3 + 185*x.^2 + 340*x + 103)./(50*x.^2 + 100*x + 30)) + 0.5;
    count_of_call = count_of_call + 1;
end