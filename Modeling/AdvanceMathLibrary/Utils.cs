namespace AdvanceMath
{
    public delegate double DoubleFunc(double X); 

    public class PointD
    {
        public double X, Y;

        public PointD(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}