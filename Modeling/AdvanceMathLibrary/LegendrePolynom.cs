using System;

namespace AdvanceMath
{
    public class LegendrePolynom : Polynom
    {
        double[] Coefs;
        public double[] Roots;

        public LegendrePolynom(int N, double RootsEps)
        {
           int i;
           Coefs = new double[N + 1];
           Coefs[N] = 1;
           for (i = 1; i <= N; i++)
               Coefs[N] = Coefs[N] * (N + i) / 2 / i;
           for (i = 0; i <= N / 2 - 1; i++)
               Coefs[N - 2 * (i + 1)] = -(Coefs[N - 2 * i] * (N - 2 * i) * (N - 2 * i - 1) / 2 / (i + 1) / (2 * (N - i) - 1));

           GetRoots(out Roots, RootsEps);
        }

        public override double GetValueAt(double X)
        {
            double S = 1;
            double Result = 0;
            for (int i = 0; i < Coefs.Length; i++)
            {
                Result += Coefs[i] * S;
                S *= X;
            }
            return Result;
        }

        double Dihotomy(double A, double B, double Eps)
        {
            if (GetValueAt(A) * GetValueAt(B) >= 0)
                return double.NaN;            

            double x1 = A, x2 = B;
            double x;

            do
            {
                x = (x1 + x2) / 2;
                if (GetValueAt(x1) * GetValueAt(x) < 0)
                    x2 = x;
                else
                    x1 = x;
            }
            while (Math.Abs((x1 - x2) / x) > Eps && Math.Abs(x) > Eps);

            return x;
        }

        private void GetRoots(out double[] Roots, double Eps)
        {
            Roots = new double[Coefs.Length - 1];

            int RootCount2 = 0;
            double X = -1;
            double Step = 2.0 / (Coefs.Length - 1) / 6.0;
            int Length2 = (Coefs.Length - 1) / 2;

            double b;
            double Root;
            if (Coefs.Length % 2 == 0)
                b = -Step;
            else
                b = 0;
            while (X < b)
            {
                Root = Dihotomy(X, X + Step, Eps);
                if (!double.IsNaN(Root))
                {
                    Roots[RootCount2] = Root;
                    Roots[Coefs.Length - 1 - RootCount2 - 1] = -Root;
                    RootCount2++;
                    if (RootCount2 == Length2)
                        break;
                }
                X += Step;
            }
        }
    }
}
