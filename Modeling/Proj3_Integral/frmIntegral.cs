using AdvanceMath;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proj3_Integral
{
	public partial class frmIntegral : Form
    {
        static double p0;
        static double T_0;
        static double T0;
        static double Tw;
        static int n;
        static int N;
        static double Eps;
        static double Eps1;

        double c;
        public delegate double Double2Func(double X, double Y);
        DoubleFunc Integral;

        static double[] Temps = new double[]
            {
                0,
                1000,
                2000,
                3000,
                4000,
                5000,
                6000,
                7000,
                8000,
                9000,
                10000,
                11000,
                12000,
                13000,
                14000,
                15000,
                16000,
                17000,
                18000,
                19000,
                20000
            };

        static double[,] Densities = new double[,]
            {
                {5, 15, 25},              // 0
                {11.67, 35.015, 58.355},  // 1000
                {18.34, 55.03, 91.71},    // 2000
                {12.23, 36.69, 61.14},    // 3000
                {9.17,  27.50, 45.86},    // 4000
                {7.35, 22.05, 36.75},     // 5000
                {6.12, 18.37, 30.62},     // 6000
                {5.24, 15.71, 26.23},     // 7000
                {4.55, 13.71, 22.87},     // 8000
                {3.97, 12.04, 20.15},     // 9000
                {3.43, 10.56, 17.77},     // 10000
                {2.88, 9.17, 15.53},      // 11000
                {2.37, 7.79, 13.36},      // 12000
                {1.93, 6.47, 11.32},      // 13000
                {1.60, 5.43, 9.52},       // 14000
                {1.38, 4.59, 8.07},       // 15000
                {1.23, 4.00, 6.99},       // 16000
                {1.12, 3.57, 6.22},       // 17000
                {1.04, 3.25, 5.64},       // 18000
                {0.96, 2.96, 5.19},       // 19000
                {0.88, 2.69, 4.81}        // 20000
            };

        static double[] LegendreCoefs;
        static LegendrePolynom LegendrePolynom;

        static double T(double z)
        {
            return T0 + (Tw - T0) * Math.Pow(z, n);
        }

        static double Density(double P, double T)
        {
            /*int TNearBottomInd = (int)(T / 1000);
            int TNearTopInd = TNearBottomInd + 1;
            if (TNearBottomInd < 0)
            {
                TNearBottomInd = 0;
                TNearBottomInd = 0;
            }
            else
            if (TNearBottomInd >= 20)
            {
                TNearBottomInd = 20;
                TNearTopInd = 20;
            }

            double r = (T / 1000 - TNearBottomInd);
            List<PointD> Points = new List<PointD>(3);
            Points.Add(new PointD(5, Densities[TNearBottomInd, 0] +
                r * (Densities[TNearTopInd, 0] - Densities[TNearBottomInd, 0])));
            Points.Add(new PointD(15, Densities[TNearBottomInd, 1] +
                r * (Densities[TNearTopInd, 1] - Densities[TNearBottomInd, 1])));
            Points.Add(new PointD(25, Densities[TNearBottomInd, 2] +
                r * (Densities[TNearTopInd, 2] - Densities[TNearBottomInd, 2])));
            
            NewtonPolynom Polynom = new NewtonPolynom(Points);

            double Result = Polynom.GetValueAt(P);
            return Result;*/

           /* List<PointD> NearPoints = new List<PointD>(GenPoints);
            NearPoints.Sort((A, B) => Math.Abs(A.X - T).CompareTo(Math.Abs(B.X - T)));
            NearPoints.RemoveRange((int)tbPower.Value + 1, NearPoints.Count - (int)tbPower.Value - 1);
            NearPoints.Sort((A, B) => A.X.CompareTo(B.X));*/

            int TNearBottomInd1 = (int)(T / 1000);
            int TNearBottomInd2 = TNearBottomInd1 - 1;
            int TNearTopInd1 = TNearBottomInd1 + 1;
            int TNearTopInd2 = TNearBottomInd1 + 2;
            
            if (TNearBottomInd1 <= 0)
            {
                TNearBottomInd2 = 0;
                TNearBottomInd1 = 1;
                TNearTopInd1 = 2;
                TNearTopInd2 = 3;
            }
            else
                if (TNearBottomInd1 >= 19)
                {
                    TNearBottomInd2 = 17;
                    TNearBottomInd1 = 18;
                    TNearTopInd1 = 19;
                    TNearTopInd2 = 20;
                }
            double TBottom2 = TNearBottomInd2 * 1000;
            double TBottom1 = TNearBottomInd1 * 1000;
            double TTop1 = TNearTopInd1 * 1000;
            double TTop2 = TNearTopInd2 * 1000;

            List<PointD> PPoints = new List<PointD>(3);
            List<PointD> Points;
            NewtonPolynom Polynom;

            Points = new List<PointD>(4);
            Points.Add(new PointD(TBottom2, Densities[TNearBottomInd2, 0]));
            Points.Add(new PointD(TBottom1, Densities[TNearBottomInd1, 0]));
            Points.Add(new PointD(TTop1, Densities[TNearTopInd1, 0]));
            Points.Add(new PointD(TTop2, Densities[TNearTopInd2, 0]));
            Polynom = new NewtonPolynom(Points);           
            PPoints.Add(new PointD(5, Polynom.GetValueAt(T)));

            Points = new List<PointD>(4);
            Points.Add(new PointD(TBottom2, Densities[TNearBottomInd2, 1]));
            Points.Add(new PointD(TBottom1, Densities[TNearBottomInd1, 1]));
            Points.Add(new PointD(TTop1, Densities[TNearTopInd1, 1]));
            Points.Add(new PointD(TTop2, Densities[TNearTopInd2, 1]));
            Polynom = new NewtonPolynom(Points);
            PPoints.Add(new PointD(15, Polynom.GetValueAt(T)));

            Points = new List<PointD>(4);
            Points.Add(new PointD(TBottom2, Densities[TNearBottomInd2, 2]));
            Points.Add(new PointD(TBottom1, Densities[TNearBottomInd1, 2]));
            Points.Add(new PointD(TTop1, Densities[TNearTopInd1, 2]));
            Points.Add(new PointD(TTop2, Densities[TNearTopInd2, 2]));
            Polynom = new NewtonPolynom(Points);
            PPoints.Add(new PointD(25, Polynom.GetValueAt(T)));

            return new NewtonPolynom(PPoints).GetValueAt(P);
        }

        static double Integrand(double P, double z)
        {
            double Result = Density(P, T(z)) * z;
            return Result;
        }

        static double Trapez(double p)
        {
            double Result = 0;
            double dx = 1 / (double)N;
            double x = dx;

            //for (int i = 1; i < N; i++)
            while (x < 1)
            {
                Result += Integrand(p, x);
                x += dx;
            }
            Result += (Integrand(p, 0) + Integrand(p, 1)) * 0.5;
            Result *= dx;
            return Result;
        }

        static double Simpson(double p)
        {
            double Result = 0;
            double dx = 1 / (double)N;
            double dx2 = 2 * dx;
            double x = dx2;

            while (x < 1)
            {
                Result += Integrand(p, x) + 2 * Integrand(p, x + dx);
                x += dx2;
            }
            Result += (Integrand(p, 0) + Integrand(p, 1)) * 0.5 + 2 * Integrand(p, dx);
            Result *= dx2 / 3;
            return Result;
        }

        static double Trapez(DoubleFunc Func)
        {
            double Result = 0;
            double dx = 1 / (double)N;
            double x = dx;

            //for (int i = 1; i < N; i++)
            while (x < 1)
            {
                Result += Func(x);
                x += dx;
            }
            Result += (Func(0) + Func(1)) * 0.5;
            Result *= dx;
            return Result;
        }

        static double Simpson(DoubleFunc Func)
        {
            double Result = 0;
            double dx = 1 / (double)N;
            double dx2 = 2 * dx;
            double x = dx2;

            while (x < 1)
            {
                Result += Func(x) + 2 * Func(x + dx);
                x += dx2;
            }
            Result += (Func(0) + Func(1)) * 0.5 + 2 * Func(dx);
            Result *= dx2 / 3;
            return Result;
        }

        static double Legendre(int N, double X)
        {
            double Result = 0;
            double a = 1;
            double b = X;

            if (N == 0)
                return 1;
            if (N == 1)
                return X;
            for (int i = 2; i <= N; i++)
            {
                Result = ((2 * i - 1) * X * b - (i - 1) * a) / i;
                a = b;
                b = Result;
            }
            return Result;
        }

        static void LegendreCoeffs(int N, out double[] C)
        {
            int i = 0;

            C = new double[N + 1];
            for (i = 0; i <= N; i++)
                C[i] = 0;
            C[N] = 1;
            for (i = 1; i <= N; i++)
            {
                C[N] = C[N] * (N + i) / 2 / i;
            }
            for (i = 0; i <= N / 2 - 1; i++)
            {
                C[N - 2 * (i + 1)] = -(C[N - 2 * i] * (N - 2 * i) * (N - 2 * i - 1) / 2 / (i + 1) / (2 * (N - i) - 1));
            }
        }

        static int LegendreCoeffs2(int N, out double[] X)
        {
            X = new double[N];
            int NStep = 100;
            bool Miss;
            int Count = 0;
            do
            {
                Miss = false;
                NStep *= 2;
                double StepX = 1; StepX /= NStep;
                Count = 0;
                for (double Xi = -1; Xi <= 0; Xi += StepX)
                {
                    if (Math.Abs(Legendre(N, Xi)) < Eps)
                    {
                        X[Count] = Xi;
                        Count++;
                        X[N - Count] = -Xi;
                        continue;
                    }
                    if (Legendre(N, Xi) * Legendre(N, Xi + StepX) < 0)
                    {
                        X[Count] = PolynomDichotomy(Xi, Xi + StepX, N);
                        if (X[Count] != Double.NaN)
                            Count++;
                        else
                            Miss = true;
                        X[N - Count] = -X[Count - 1];
                    }
                }
            } 
            while (Miss || Count < N / 2);
            return 0;
        }

        static double PolynomDichotomy(double A, double B, int N)
        {
            if (Math.Abs(Legendre(N, A) - Legendre(N, B)) < Eps)
                return A;
            else
            {
                double M = (A + B) / 2;
                if (Legendre(N, A) * Legendre(N, M) < 0)
                    return PolynomDichotomy(A, M, N);
                else if (Legendre(N, M) * Legendre(N, B) < 0)
                    return PolynomDichotomy(M, B, N);
                else
                    return Double.NaN;
            }
        }

        static double Gauss(double p)
        {
            double Result = 0;
            double[,] Matrix = new double[N, N+1];

            LegendrePolynom.GetValueAt(0);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    if (i == 0)
                        Matrix[i, j] = 1;
                    else
                        Matrix[i, j] = Math.Pow(LegendrePolynom.Roots[j], i);

                if (i % 2 == 0)
                    Matrix[i, N] = 2.0 / (i + 1);
                else
                    Matrix[i, N] = 0;
            }
            double[] ResultColumn = SLE.GaussSolve(Matrix, N);

            for (int i = 0; i < N; i++)
            {
                double z = 0.5 + LegendreCoefs[i] * 0.5;
                Result += ResultColumn[i] * Integrand(p, z);
            }
            Result *= 0.5;
            return Result;
        }

        double MainFunc(double X)
        {
            double Result = Integral(X) - c;
            return Result;
        }

        double Dihotomy(double A, double B)
        {
            if (MainFunc(A) * MainFunc(B) > 0)
                return Double.NaN;

            double x1 = A, x2 = B;
            double x;

            do
            {
                x = (x1 + x2) / 2;
                if (MainFunc(x1) * MainFunc(x) < 0)
                    x2 = x;
                else
                    x1 = x;
            }
            //while (Math.Abs(x1 - x2) > Eps * Math.Abs(x) + Eps1 && Math.Abs(x) > Eps);
            while (Math.Abs((x1 - x2) / x) > Eps);

            return x;
        }

        double Sqrt3(double X)
        {
            return Math.Pow(X, 1.0 / 3.0);
        }

        void Eitken(double h, double q)
        { 
	        double h1 = h;
	        double h2 = q * h;
	        double h3 = q * h2;

            N = (int)Math.Round(1 / h1);
	        double F1 = Trapez(Sqrt3);
            N = (int)Math.Round(1 / h2);
            double F2 = Trapez(Sqrt3);
            N = (int)Math.Round(1 / h3);
            double F3 = Trapez(Sqrt3);

	        label15.Text = ((1.0 / Math.Log(q)) * Math.Log(Math.Abs((F3 - F2) / (F2 - F1)))).ToString();

            N = (int)Math.Round(1 / h1);
            F1 = Simpson(Sqrt3);
            N = (int)Math.Round(1 / h2);
            F2 = Simpson(Sqrt3);
            N = (int)Math.Round(1 / h3);
            F3 = Simpson(Sqrt3);

            label16.Text = ((1.0 / Math.Log(q)) * Math.Log(Math.Abs((F3 - F2) / (F2 - F1)))).ToString();
        }
        
        public frmIntegral()
        {
            InitializeComponent();
            cmbIntMethod.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Eps = Convert.ToDouble(tbEps.Text);
            Eps1 = Convert.ToDouble(tbEps1.Text);
            
            p0 = Convert.ToDouble(tbP.Text);
            T_0 = Convert.ToDouble(tbT_0.Text);
            T0 = Convert.ToDouble(tbT0.Text);
            Tw = Convert.ToDouble(tbTw.Text);
            n = Convert.ToInt32(tbN.Text);
            
            //c = p0 * 7242 / T_0;
            c = p0 * 3621 / T_0;

            N = (int)UpDownNTrapez.Value;
            Integral = Trapez;
            lblAnswer.Text = "Trapezium: p = " + Dihotomy(0, 50).ToString();

            N = (int)UpDownNSimpson.Value;
            Integral = Simpson;
            lblAnswer3.Text = "Simpson: p = " + Dihotomy(0, 50).ToString();

            N = (int)UpDownNGauss.Value;
            LegendrePolynom = new LegendrePolynom((int)UpDownNGauss.Value, Eps);
            
            Integral = Gauss;
            LegendreCoeffs2(N, out LegendreCoefs);
            lblAnswer2.Text = "Gauss: p = " + Dihotomy(0, 50).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Eitken(Convert.ToDouble(tbh.Text), Convert.ToDouble(tbq.Text));
        }
    }
}
