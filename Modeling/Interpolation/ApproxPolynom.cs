using System;
using System.Collections.Generic;

namespace AdvanceMath
{
	public delegate double ApproxFunc(double X, int Power);

    struct WeightPointD
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

    class ApproxPolynom : Polynom
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

        bool MakeTriangle(ref double[,] Matrix, int N)
        {
            int q;
            int i, j;

            for (i = 0; i < N; i++)
            {
                if (Matrix[i, i] == 0)
                {
                    q = -1;
                    for (j = i + 1; j < N; j++)
                        if (Matrix[j, j] != 0)
                        {
                            q = j;
                            break;
                        }
                    if (q == -1) 
                        return false;
                    else
                        for (j = 0; j <= N; j++)
                        {
                            double t = Matrix[i, j];
                            Matrix[i, j] = Matrix[q, j];
                            Matrix[q, j] = t;
                        }
                }
                for (j = N; j >= i; j--)                             // т.к. последний столбец, то N!!!
                    Matrix[i, j] /= Matrix[i, i];
                for (j = i + 1; j < N; j++)
                    for (int k = N; k >= 0; k--)
                        Matrix[j, k] -= Matrix[i, k] * Matrix[j, i];
            }

            return true;
        }

        double[] GetSolveFromTriangle(double[,] Matrix, int N)
        {
            double[] Result = new double[N];
            double s;
            for (int i = N - 1; i >= 0; i--)
            {
                s = 0;
                for (int j = i + 1; j < N; j++)
                    s += Matrix[i, j] * Result[j];
                Result[i] = Matrix[i, N] - s;
            }
            return Result;
        }

        // Решение СЛАУ
        double[] SLESolve(double[,] Matrix, int N)
        {
            double[] Result;

            if (MakeTriangle(ref Matrix, N))
                Result = GetSolveFromTriangle(Matrix, N);
            else
                throw new Exception();

            return Result;
        }

        void Build()
        {
            int i, j, k;
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

            coefs = SLESolve(tempCoefs, power + 1);
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

