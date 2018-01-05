using System.Collections.Generic;

public class Graphic
{
    public TPoint[] Points;

    public string Term;

    public Graphic(TPoint[] InPoints, string InTerm)
    {
        Points = InPoints;
        Term = InTerm;
    }

    public double GiveProbability(double Value)
    {
      /*  double[]Result;
        if ((Result = getY(Value)).Length == 0)
            return 0;
        else
            return Result[0];*/

        return GetY(Value)[0];
    }


    public double[] GetY(double x)
    {
        List<double> Y = new List<double>();
        for (int i = 0; i < Points.Length - 1; i++)
        {
            if ((Points[i].X <= x && Points[i + 1].X > x) ||
                (Points[i + 1].X <= x && Points[i].X > x))
               Y.Add(GetY(Points[i], Points[i + 1], x));
        }

        if (Y.Count == 0) Y.Add(0);
        return Y.ToArray();
    }

    public double[] GetX(double y)
    {
        List<double> X = new List<double>();
        for (int i = 0; i < Points.Length - 1; i++)
        {
            if ((Points[i].Y <= y && Points[i + 1].Y > y) || (Points[i + 1].Y <= y && Points[i].Y > y))
                X.Add(GetX(Points[i], Points[i + 1], y));
        }

        return X.ToArray();
    }

    private double GetY(TPoint p1, TPoint p2, double x)
    {
        double a = (p1.Y - p2.Y) / (p1.X - p2.X);
        double b = p1.Y - a * p1.X;
        return a * x + b;
    }

    private double GetX(TPoint p1, TPoint p2, double y)
    {
        double a = (p1.X - p2.X) / (p1.Y - p2.Y);
        double b = p1.X - a * p1.Y;
        return a * y + b;
    }

}