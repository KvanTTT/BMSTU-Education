function draw(f)

x = -10:0.5:10;
    y = -10:0.5:10;
    for i = 1:length(x)
        for j = 1:length(y)
            z(j, i) = f([x(i); y(j)]);
        end
    end
    disp(min(min(z)));
    disp(max(max(z)));
	surf(x,y,z);
end