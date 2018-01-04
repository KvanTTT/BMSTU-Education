using AdvanceMath;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Proj4_MultIntegral
{
	public partial class frmMultIntegral : Form
    {
        double R;
        double X1, X2, Y1, Y2;
        int StepsXCount, StepsYCount;
        double Eps, Eps1;
        double EpsNu;

        public delegate double Double2Func(double X, double Y);    

        double L(double Omega, double Phi)
        {
            double Sin = Math.Sin(Omega);
            double Cos = Math.Cos(Phi);
            return 2 * R * Math.Cos(Omega) / (1 - Sin * Sin * Cos * Cos);
        }

        double Integrand(double Omega, double Phi)
        {
            double SinOmega = Math.Sin(Omega);
            double CosOmega = Math.Cos(Omega);
            double CosPhi = Math.Cos(Phi);            
            return (1 - Math.Exp(-2 * R * CosOmega / (1 - SinOmega * SinOmega * CosPhi * CosPhi))) * CosOmega * SinOmega;
        }

        void CalculGaussCoefs(int N, out double[] LegendreRoots, out double[] GaussCoefs)
        {
            LegendrePolynom Polynom = new LegendrePolynom(N, 1e-9);
            double[,] Matrix = new double[N, N + 1];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    if (i == 0)
                        Matrix[i, j] = 1;
                    else
                        Matrix[i, j] = Math.Pow(Polynom.Roots[j], i);

                if (i % 2 == 0)
                    Matrix[i, N] = 2.0 / (i + 1);
                else
                    Matrix[i, N] = 0;
            }
            GaussCoefs = SLE.GaussSolve(Matrix, N);
            LegendreRoots = Polynom.Roots;
        }

        double DoubleIntegral(double X1, double X2, double Y1, double Y2, double[] LegendreRoots, double[] GaussCoefs, Double2Func Func)
        {
            double Result = 0;
            for (int i = 0; i < GaussCoefs.Length; i++)
            {
                double X = ((X2 + X1) + (X2 - X1) * LegendreRoots[i]) * 0.5;
                for (int j = 0; j < GaussCoefs.Length; j++)
                {                    
                    double Y = ((Y2 + Y1) + (Y2 - Y1) * LegendreRoots[j]) * 0.5;
                    Result += GaussCoefs[i] * GaussCoefs[j] * Func(X, Y);
                }
            }
            Result *= (X2 - X1) * (Y2 - Y1) * 0.25;
            return Result;
        }

        double DoubleIntegralCells(double X1, double X2, double Y1, double Y2, Double2Func Func)
        {
            double hx = (X2 - X1) / StepsXCount,
                   hy = (Y2 - Y1) / StepsYCount,
                   x = X1, y = Y1,
                   Result = 0.0;

            for (int i = 0; i < StepsYCount; i++, y += hy, x = X1)
                for (int j = 0; j < StepsXCount; j++, x += hx)
                    Result += Func(x - hx * 0.5, y - hy * 0.5);

            return Result * hx * hy;
        }

        double DoubleIntegralCells(double X)
        {
            double hx = (X2 - X1) / StepsXCount,
                   hy = (Y2 - Y1) / StepsYCount,
                   x = X1, y = Y1,
                   Result = 0.0;

            for (int i = 0; i < StepsYCount; i++, y += hy, x = X1)
                for (int j = 0; j < StepsXCount; j++, x += hx)
                {
                    R = X;
                    Result += Integrand(x - hx * 0.5, y - hy * 0.5);
                }

            return Result * hx * hy;
        }

        double EquFunc(double X)
        {
            return 4 / Math.PI * DoubleIntegralCells(X) - EpsNu;
        }

        double Dihotomy(double A, double B)
        {
            if (EquFunc(A) * EquFunc(B) > 0)
                return Double.NaN;

            double x1 = A, x2 = B;
            double x;

            do
            {
                x = (x1 + x2) / 2;
                if (EquFunc(x1) * EquFunc(x) < 0)
                    x2 = x;
                else
                    x1 = x;
            }
            while (Math.Abs((x1 - x2) / x) > Eps + Eps1 && Math.Abs(x) > Eps);

            return x;
        }


        public frmMultIntegral()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StepsXCount = (int)udXCount.Value;
            StepsYCount = (int)udXCount.Value;
            Eps = Convert.ToDouble(tbEps.Text);
            Eps1 = Convert.ToDouble(tbEps1.Text);

            EpsNu = Convert.ToDouble(textBox1.Text);
            int N = Convert.ToInt32(udN.Value);
            double TauStart = Convert.ToDouble(tbTauStart.Text);
            double TauFin = Convert.ToDouble(tbTauFin.Text);
            R = Convert.ToDouble(tbTauNu.Text);

            X1 = 0;
            X2 = Math.PI / 2;
            Y1 = 0;
            Y2 = Math.PI / 2;

            double[] GaussCoefs, LegendreRoots;
            CalculGaussCoefs((int)udNGauss.Value, out LegendreRoots, out GaussCoefs);

            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();
            myPane.Title.Text = "ε(τ)";
            myPane.XAxis.Title.Text = "τ";
            myPane.YAxis.Title.Text = "ε(τ)";

            PointPairList list1 = new PointPairList();
            double Step = (TauFin - TauStart) / N;
            R = TauStart;

            if (rbCells.Checked)
            for (int i = 0; i < N; i++)
            {
                list1.Add(R, 4 / Math.PI * DoubleIntegralCells(X1, X2, Y1, Y2, Integrand));
                R += Step;
            }
            else
            for (int i = 0; i < N; i++)
            {
                list1.Add(R, 4 / Math.PI * DoubleIntegral(X1, X2, Y1, Y2, LegendreRoots, GaussCoefs, Integrand));
                R += Step;
            }

            LineItem myCurve1 = myPane.AddCurve("ε(τ)", list1, Color.Red, SymbolType.None);

            zgc.AxisChange();
            zgc.Refresh();

            label8.Text = Dihotomy(TauStart, TauFin).ToString();
        }
    }
}
