using System.Collections.Generic;


namespace AdvanceMath
{
    public delegate double ApproxFunc(double X, int Power);

    public struct WeightPointD
    {
        public double X;
        public double Y;
        public double Weight;

        public WeightPointD(PointD Point, double Weight)
        {
            this.X = Point.X;
            this.Y = Point.Y;
            this.Weight = Weight;
        }

        public WeightPointD(double X, double Y, double Weight)
        {
            this.X = X;
            this.Y = Y;
            this.Weight = Weight;
        }
    }

    public class ApproxPolynom : Polynom
    {
        List<WeightPointD> points;
        double[] coefs;
        int power;
        ApproxFunc approxFunc;

        public ApproxPolynom(List<WeightPointD> Points, int Power, ApproxFunc Func)
        {
            points = Points;
            power = Power;
            approxFunc = Func;
            Build();
        }

        void Build()
        {
            int i, j, k, l;
            double[,] tempCoefs = new double[power + 1, power + 1 + 1];
            double[] tx = new double[points.Count];
            double[] txx = new double[points.Count];
            double[] ty = new double[points.Count];

            for (k = 0; k < points.Count; k++)
            {
                ty[k] = points[k].Weight * points[k].Y;
                txx[k] = 1;
            }
            for (i = 0; i <= power; i++)
            {
                for (k = 0; k < points.Count; k++)
                    tx[k] = txx[k];

                for (j = i; j <= power; j++)
                {
                    for (k = 0; k < points.Count; k++)
                    {
                        tempCoefs[i, j] += points[k].Weight * tx[k];              
                        tx[k] *= points[k].X;
                    }
                    tempCoefs[j, i] = tempCoefs[i, j];
                }

                for (k = 0; k < points.Count; k++)
                {
                    txx[k] *= points[k].X * points[k].X;

                    tempCoefs[i, j] += ty[k];
                    ty[k] *= points[k].X;
                }
            }

            /*for (k = 0; k < points.Count; k++)
            {
                tx[k] = 1;
                ty[k] = points[k].Weight * points[k].Y;
                tempCoefs[0, 0] += points[k].Weight;
                tempCoefs[0, power + 1] += ty[k];
            }
            for (j = 1; j <= power; j++)
            {
                for (k = 0; k < points.Count; k++)
                {
                    tx[k] *= points[k].X;
                    ty[k] *= points[k].X;
                    tempCoefs[j, power + 1] += ty[k];
                }

                l = j;
                for (i = 0; i < j / 2 + 1; i++)
                {
                    for (k = 0; k < points.Count; k++)
                        tempCoefs[i, l] += points[k].Weight * tx[k];
                    tempCoefs[l, i] = tempCoefs[i, l];
                    l--;
                }
                for (k = 0; k < points.Count; k++)
                    tempCoefs[i, i] += points[k].Weight * tx[k];                
            }

            for (i = 1; i < power; i++)
            {
                for (k = 0; k < points.Count; k++)
                    tx[k] *= points[k].X;
                l = i;
                for (j = power; j > power / 2 + i / 2; j--)
                {
                    for (k = 0; k < points.Count; k++)
                        tempCoefs[l, j] += points[k].Weight * tx[k];
                    tempCoefs[j, l] = tempCoefs[l, j];
                    l++;
                }
                for (k = 0; k < points.Count; k++)
                    tempCoefs[j, j] += points[k].Weight * tx[k];
            }

            for (k = 0; k < points.Count; k++)
            {
                tx[k] *= points[k].X;
                tempCoefs[power, power] += points[k].Weight * tx[k];
            }*/

            coefs = SLE.GaussSolve(tempCoefs, power + 1);
        }

        public override double GetValueAt(double X)
        {
            double Result = 0;
            for (int i = 0; i < coefs.Length; i++)
                Result += coefs[i] * approxFunc(X, i);
            return Result;
        }
    }
}

