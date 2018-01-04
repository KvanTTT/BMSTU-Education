using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Proj5_RungeKutta
{
	public partial class frmRungeKutta : Form
    {
        static double Uo;
        static double Io;
        static double Rk;
        static double Lk;
        static double Ck;
        static double tL;
        static double tR;
        static double tH;
        //double alpha;

        static double Tw;
        static double Le;        
        static int NIntegral;

        static double LogInterp(double X1, double X2, double Y1, double Y2, double X)
        {
            double LnX = Math.Log(X);
            double LnX1 = Math.Log(X1);
            double LnX2 = Math.Log(X2);
            double LnY1 = Math.Log(Y1);
            double LnY2 = Math.Log(Y2);
            return Math.Pow(Math.E, LnY1 + (LnY2 - LnY1) * (LnX - LnX1) / (LnX2 - LnX1));
        }

        static double[,] ITm = 
        {
          // I,A   To,K  m
            {0.00000000001, 0.00000000001, 0.00000000001},
            {0.5, 6400, 0.4},
            {1, 6790, 0.55},
            {5, 7150, 1.7},
            {10, 7270, 3},
            {50, 8010, 11},
            {200, 9185, 32},
            {400, 10010, 40},
            {800, 11140, 41},
            {1200, 12010, 39}
        };

        static double T(double I, double z)
        {
            int i;
            if (I < 0)
                return Tw;

            for (i = 0; i <= 8; i++)
                if (I >= ITm[i, 0] && I < ITm[i + 1, 0])
                    break;
            if (i == 9)
                i = 8;

            /*double r = (I - ITm[i, 0]) / (ITm[i + 1, 0] - ITm[i, 0]);
            double To = ITm[i, 1] + (ITm[i + 1, 1] - ITm[i, 1]) * r;
            double m = ITm[i, 2] + (ITm[i + 1, 2] - ITm[i, 2]) * r;   */
            double ITm0 = Math.Log(ITm[i, 0]);
            double r = (Math.Log(I) - ITm0) / (Math.Log(ITm[i + 1, 0]) - ITm0);
            double ITm1 = Math.Log(ITm[i, 1]);
            double To = Math.Exp(ITm1 + (Math.Log(ITm[i + 1, 1]) - ITm1) * r);
            double ITm2 = Math.Log(ITm[i, 2]);
            double m = Math.Exp(ITm2 + (Math.Log(ITm[i + 1, 2]) - ITm2) * r);

            double Result = To + (Tw - To) * Math.Pow(z, m);
            return Result;
        }

        static double[] Sigmas = 
        {
            0,      // 0
            0.007,  // 1000
            0.015,  // 2000
            0.023,  // 3000
            0.0309,  // 4000
            0.27,   // 5000
            2.05,   // 6000
            6.06,   // 7000
            12.0,   // 8000
            19.9,   // 9000
            29.6,   // 10000 
            41.1,   // 11000
            54.1,   // 12000
            67.7,   // 13000
            81.5,   // 14000
            93.8,   // 15000
            105,    // 16000
            115,    // 17000
            124,    // 18000
            135,    // 19000
            150     // 20000
        };

        static double Sigma(double T)
        {
            if (T < 1e-9 || T == Double.NaN)
                T = 1e-9;
            int Ind = (int)(T / 1000);
            if (Ind > Sigmas.Length - 2)
                Ind = Sigmas.Length - 2;
            int Trunc = (int)(T / 1000) * 1000;
            if (Ind < 0)
            {
                Ind = 0;
                T = 1e-9;
            }
            /*double LnT = Math.Log(T);
            double LnT1 = Math.Log(Trunc);
            double LnT2 = Math.Log(Trunc + 1000);
            double LnSigma = Math.Log(Sigmas[Ind]);
            return Math.Pow(Math.E, LnSigma + (Math.Log(Sigmas[Ind + 1]) - LnSigma) * (LnT - LnT1) / (LnT2 - LnT1));*/
            return LogInterp(Trunc, Trunc + 1000, Sigmas[Ind], Sigmas[Ind + 1], T);
        }

        static double Integrand(double I, double x)
        {
            return Sigma(T(I, x)) * x;
        }

        static double Simpson(double I)
        {
            double Result = 0;
            double dx = 1 / (double)NIntegral;
            double dx2 = 2 * dx;
            double x = dx2;

            while (x < 1)
            {
                Result += Integrand(I, x) + 2 * Integrand(I, x + dx);
                x += dx2;
            }
            Result += (Integrand(I, 0) + Integrand(I, 1)) * 0.5 + 2 * Integrand(I, dx);
            Result *= dx2 / 3;
            return Result;
        }

        double Rp(double I)
        {
            double Result = Le / (2 * Math.PI * Simpson(I));
            return Result;
        }

        double dIdT(double Uc, double Rk, double I, double Lk)
        {
            return (Uc - (Rk + Rp(Math.Abs(I))) * I) / Lk;
            //return (Uc - Rk * I) / Lk;
        }

        double dUdT(double I, double Ck)
        {
            return (-I / Ck);
        }

        void SolveUITrap(double Uo, double Io, double Lk, 
            double Rk, double Ck, double hT,
            ref double outI, ref double outU)
        {
            const int NumIts = 1;

            double I = Io,
                   U = Uo,
                   cI = (hT / 2) * dIdT(Uo, Rk, Io, Lk),
                   cU = (hT / 2) * dUdT(Io, Ck);
            hT /= 2;

            for (int i = 0; i < NumIts; i++)
            {
                outI += cI + hT * dIdT(U, Rk, I, Lk);
                outU += cU + hT * dUdT(I, Ck);

                I = outI;
                U = outU;
            }
        }

        void Trapezium(double Uo, double Io,
                       double Lk, double Ck, double Rk,
                       double tL, double tR, double tH,
                       out double[] U, out double[] I)
        {
            int NumSteps = (int)((tR - tL) / tH);

            double PrevU = Uo,
                   PrevI = Io;

            U = new double[NumSteps];
            I = new double[NumSteps];

            for (int i = 0; i < NumSteps; i++)
            {
                U[i] = PrevU;
                I[i] = PrevI;

                SolveUITrap(PrevU, PrevI, Lk, Rk, Ck, tH, ref PrevI, ref PrevU);
            }
        }

        void SolveUIGir(double[] U, double[] I,
                         double Lk, double Rk, double Ck,
                         double hT,
                         ref double outU, ref double outI)
        {
            const int NumIts = 1;

            double mul1 = (double)(4.0) / (double)(3.0),
                        mul2 = (double)(2.0) * hT / (double)(3.0),
                        tmpI = I[1],
                        tmpU = U[1];

            for (int i = 0; i < NumIts; i++)
            {
                outU = mul1 * U[1] - (U[0] / (double)(3.0)) + mul2 * dUdT(tmpI, Ck);
                outI = mul1 * I[1] - (I[0] / (double)(3.0)) + mul2 * dIdT(tmpU, Rk, tmpI, Lk);

                tmpU = outU;
                tmpI = outI;
            }
        }


        void Gir(double Uo, double Io,
                 double Lk, double Ck, double Rk,
                 double tL, double tR, double tH,
                 out double[] U, out double[] I)
        {
            double[] Ui, Ii;

            RungeCutta4(Uo, Io, Lk, Ck, Rk, tL, tL + tH * 2, tH, out Ui, out Ii);

            int NumSteps = (int)((tR - tL) / tH);

            U = new double[NumSteps];
            I = new double[NumSteps];

            double tmpI = 0, tmpU = 0;

            for (int i = 0; i < NumSteps; i++)
            {
                U[i] = Ui[0];
                I[i] = Ii[0];

                SolveUIGir(Ui, Ii, Lk, Rk, Ck, tH, ref tmpU, ref tmpI);

                Ui[0] = Ui[1];
                Ui[1] = tmpU;

                Ii[0] = Ii[1];
                Ii[1] = tmpI;
            }
        }

        void RungeCutta2(double Uo, double Io, double Lk, double Ck, double Rk,
                         double tL, double tR, double tH, double alpha,
                         out double[] U, out double[] I)
        {
            int NumSteps = (int)((tR - tL) / tH);

            double PrevU = Uo,
                   PrevI = Io,
                   mul = tH / (2 * alpha);

            U = new double[NumSteps];
            I = new double[NumSteps];

            for (int i = 0; i < NumSteps; i++)
            {
                U[i] = PrevU;
                I[i] = PrevI;

                PrevI += tH * ((1 - alpha) * dIdT(PrevU, Rk, PrevI, Lk) + alpha * dIdT(
                    PrevU + mul * dIdT(PrevU, Rk, PrevI, Lk), Rk, PrevI + mul * dUdT(PrevI, Ck), Lk));
                PrevU += tH * ((1 - alpha) * dUdT(PrevI, Ck) + alpha * dUdT(
                    PrevI + mul * dIdT(PrevU, Rk, PrevI, Lk), Ck));
            }
        }

        void RungeCutta4(double Uo, double Io, double Lk, double Ck, double Rk,
                         double tL, double tR, double tH,
                         out double[] U, out double[] I)
        {
            int NumSteps = (int)((tR - tL) / tH);

            double PrevU = Uo,
                   PrevI = Io;

            double k1 = 0, k2 = 0, k3 = 0, k4 = 0,
                   q1 = 0, q2 = 0, q3 = 0, q4 = 0;

            U = new double[NumSteps];
            I = new double[NumSteps];

            for (int i = 0; i < NumSteps; i++)
            {
                U[i] = PrevU;
                I[i] = PrevI;

                k1 = dIdT(PrevU, Rk, PrevI, Lk);
                q1 = dUdT(PrevI, Ck);
                k2 = dIdT(PrevU + (tH * q1) / 2, Rk, PrevI + (tH * k1) / 2, Lk);
                q2 = dUdT(PrevI + (tH * k1) / 2, Ck);
                k3 = dIdT(PrevU + (tH * q2) / 2, Rk, PrevI + (tH * k2) / 2, Lk);
                q3 = dUdT(PrevI + (tH * k2) / 2, Ck);
                k4 = dIdT(PrevU + tH * q3, Rk, PrevI + tH * k3, Lk);
                q4 = dUdT(PrevI + tH * k3, Ck);

                PrevU += (tH / 6) * (q1 + 2 * q2 + 2 * q3 + q4);
                PrevI += (tH / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
            }
        }

        public frmRungeKutta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uo = Convert.ToDouble(tbUo.Text);
            Io = Convert.ToDouble(tbIo.Text);
            Rk = Convert.ToDouble(tbRk.Text);
            Lk = Convert.ToDouble(tbLk.Text);
            Ck = Convert.ToDouble(tbCk.Text);
            tL = Convert.ToDouble(tbStartT.Text);
            tR = Convert.ToDouble(tbEndT.Text);
            tH = Convert.ToDouble(tbDt.Text);
            //alpha = Convert.ToDouble(tbAlpha.Text);

            Tw = Convert.ToDouble(tbTw.Text);
            Le = Convert.ToDouble(tbLe.Text);
            NIntegral = Convert.ToInt32(UpDownNSimpson.Value);

            double[] U = null, I = null;
            if (rbRungeCutta2.Checked)
                RungeCutta2(Uo, Io, Lk, Ck, Rk, tL, tR, tH, 1, out U, out I);
            else
                if (rbRungeCutta4.Checked)
                    RungeCutta4(Uo, Io, Lk, Ck, Rk, tL, tR, tH, out U, out I);
                else
                    if (rbGir.Checked)
                        Gir(Uo, Io, Lk, Ck, Rk, tL, tR, tH, out U, out I);
                    else
                        if (rbTrapezium.Checked)
                            Trapezium(Uo, Io, Lk, Ck, Rk, tL, tR, tH, out U, out I);

            GraphPane Pane = zgc.GraphPane;
            Pane.CurveList.Clear();

            Pane.Title.Text = "U(t), I(t)";
            Pane.XAxis.Title.Text = "t";
            Pane.YAxis.Title.Text = "U(t)";

            int NumSteps = (int)((tR - tL) / tH);

            double tX = tL,
                   r  = (tR - tL) / NumSteps;

            if (cbUt.Checked)
            {
                PointPairList list1 = new PointPairList();
                list1.Add(tL, U[0]);
                for (int i = 1; i < NumSteps; i++)
                {
                    tX = tL + r * i;
                    list1.Add(tX, U[i]);
                }
                LineItem Curve1 = Pane.AddCurve("U(t)", list1, Color.Red, SymbolType.None);
            }

            if (cbIt.Checked)
            {
                PointPairList list2 = new PointPairList();
                list2.Add(tL, I[0]);
                for (int i = 1; i < NumSteps; i++)
                {
                    tX = tL + r * i;
                    list2.Add(tX, I[i]);
                }
                LineItem Curve2 = Pane.AddCurve("I(t)", list2, Color.Green, SymbolType.None);
            }
            
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void frmRungeKutta_Load(object sender, EventArgs e)
        {/*
            double MinValue = 1000000000;
            double ln4000 = Math.Log(4000);
            double Ln0031 = Math.Log(0.031);
            textBox1.Text = Math.Pow(Math.E, MinValue + (Ln0031 - MinValue) * (Math.Log(1000) - MinValue) / (ln4000 - MinValue)).ToString() + "   " +
                Math.Pow(Math.E, MinValue + (Ln0031 - MinValue) * (Math.Log(2000) - MinValue) / (ln4000 - MinValue)).ToString() + "   " +
                Math.Pow(Math.E, MinValue + (Ln0031 - MinValue) * (Math.Log(3000) - MinValue) / (ln4000 - MinValue)).ToString() + "   " +
                Math.Pow(Math.E, MinValue + (Ln0031 - MinValue) * (Math.Log(4000) - MinValue) / (ln4000 - MinValue)).ToString() + "   ";    */
            /*GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 9; i++)
            {
                //list1.Add(ITm[i, 0], ITm[i, 1]);
                list2.Add(ITm[i, 0], ITm[i, 2]);
            }
            //LineItem myCurve1 = myPane.AddCurve("To(I)", list1, Color.Red, SymbolType.None);
            LineItem myCurve2 = myPane.AddCurve("m(I)", list2, Color.Blue, SymbolType.None);
            zgc.AxisChange();
            zgc.Refresh();*/
        }
    }
}
