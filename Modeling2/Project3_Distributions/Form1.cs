using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Project3_Distributions
{
    public partial class Form1 : Form
    {
        double IndentCoef = 0.05f;
        double[] RandomNumbers;

        double TheorUniformDensity(double x, double a, double b)
        {
            if (x >= a && x <= b)
                return 1 / (b - a);
            else
                return 0;
        }

        double TheorUniformDistrib(double x, double a, double b)
        {
            if (x <= a)
                return 0;
            else
                if (x >= b)
                    return 1;
                else
                    return (x - a) / (b - a);
        }  

        double TheorErlangDensity(double x, double k, double θ)
        {
            if (x >= 0)
                return Math.Pow(x, k - 1) * Math.Exp(-x / θ) / (Math.Pow(θ, k) * alglib.gammafunction(k));
            else
                return 0;
        }

        double TheorErlangDistrib(double x, double k, double θ)
        {
            return alglib.incompletegamma(k, x / θ);
        }

        double ExperemDensity(double a, double dx)
        {
            int IntervalCount = 0;
            for (int i = 0; i < RandomNumbers.Length; i++)
                if (RandomNumbers[i] >= a && RandomNumbers[i] < a + dx)
                    IntervalCount++;
            return (double)IntervalCount / RandomNumbers.Length / dx;
        }

        double ExperemDistrib(double x, double dx)
        {
            int LeftCount = 0;
            for (int i = 0; i < RandomNumbers.Length; i++)
                if (RandomNumbers[i] <= x)
                    LeftCount++;
            return (double)LeftCount / RandomNumbers.Length;
        }

        void GenerateUniformRandomNumbers(double a, double b, int Count)
        {
            RandomNumbers = new double[Count];
            Random Rand = new Random();
            for (int i = 0; i < Count; i++)
                RandomNumbers[i] = a + (b - a) * Rand.NextDouble();
        }

        void GenerateErlangRandomNumbers(double k, double θ, int Count)
        {
            RandomNumbers = new double[Count];            
            Random Rand = new Random();
            int Integer = (int)k;
            double Fraction = k - Integer;
            double LnSum;  
            double V1, V2;
            double v0 = Math.E / (Math.E + Fraction);
            double Em, Nm;

            for (int i = 0; i < Count; i++)
            {
                if (Fraction == 0)
                    Em = 0;
                else
                {
                    do
                    {
                        V1 = 1.0 - Rand.NextDouble();
                        V2 = 1.0 - Rand.NextDouble();
                        if (V1 <= v0)
                        {
                            Em = Math.Pow(V1 / v0, 1 / Fraction);
                            Nm = V2 * Math.Pow(Em, Fraction - 1);
                        }
                        else
                        {
                            Em = 1 - Math.Log((V1 - v0) / (1.0 - v0));
                            Nm = V2 * Math.Exp(-Em);
                        }
                    }
                    while (Nm > Math.Pow(Em, Fraction - 1) * Math.Exp(-Em));
                }

                LnSum = 0;
                for (int j = 0; j < Integer; j++)
                    LnSum += Math.Log(1.0 - Rand.NextDouble());

                RandomNumbers[i] = θ * (Em - LnSum);
            }
        }

        public Form1()
        {
            InitializeComponent();
            rbUniform.Checked = true;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            splitter1.SplitPosition = panel1.Width / 2;
        }


        void DrawAll()
        {
            GraphPane Pane = graphDistribFunc.GraphPane;
            Pane.CurveList.Clear();
            Pane.Title.Text = "Distribution function";
            Pane.XAxis.Title.Text = "X";

            PointPairList list1 = new PointPairList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double Left = Convert.ToDouble(tbLeft.Text);
            double Right = Convert.ToDouble(tbRight.Text);
            double Indent = IndentCoef * (Right - Left);
            double step = (Right - Left + Indent * 2) / graphDensityFunc.ClientSize.Width;
            double x;
            PointPairList DensityFuncList = new PointPairList(), DistribFuncList = new PointPairList();
            GraphPane DensityFuncPane, DistribFuncPane;

            DensityFuncPane = graphDensityFunc.GraphPane;
            DensityFuncPane.CurveList.Clear();
            DensityFuncPane.Title.Text = "Density function";
            DensityFuncPane.XAxis.Title.Text = "X";
            DensityFuncPane.YAxis.Title.Text = "Y";            

            DistribFuncPane = graphDistribFunc.GraphPane;
            DistribFuncPane.CurveList.Clear();
            DistribFuncPane.Title.Text = "Distribution function";
            DistribFuncPane.XAxis.Title.Text = "X";
            DistribFuncPane.YAxis.Title.Text = "Y";

            if (rbUniform.Checked)
            {                
                GenerateUniformRandomNumbers(Left, Right, (int)udCount.Value);

                #region Density function
                x = Left - Indent;           
                for (int i = 0; i < graphDensityFunc.ClientSize.Width; i++)
                {
                    DensityFuncList.Add(x, TheorUniformDensity(x, Left, Right));
                    x += step;
                }                  
                #endregion

                #region Distribution function
                x = Left - Indent;
                for (int i = 0; i < graphDensityFunc.ClientSize.Width; i++)
                {
                    DistribFuncList.Add(x, TheorUniformDistrib(x, Left, Right));
                    x += step;
                }
                #endregion

                lblTheorMean.Text = ((Left + Right) / 2.0).ToString("0.0000");
                lblTheorVariance.Text = ((Right - Left) * (Right - Left) / 12.0).ToString("0.0000");
            }
            else
            {
                double k = Convert.ToDouble(tbKParam.Text);
                double θ = Convert.ToDouble(tbθParam.Text);
                GenerateErlangRandomNumbers(k, θ, (int)udCount.Value);

                #region Density function
                x = Left - Indent;
                for (int i = 0; i < graphDensityFunc.ClientSize.Width; i++)
                {
                    DensityFuncList.Add(x, TheorErlangDensity(x, k, θ));
                    x += step;
                }
                #endregion

                #region Distribution function
                x = Left - Indent;
                for (int i = 0; i < graphDensityFunc.ClientSize.Width; i++)
                {
                    DistribFuncList.Add(x, TheorErlangDistrib(x, k, θ));
                    x += step;
                }
                #endregion

                lblTheorMean.Text = (k * θ).ToString("0.0000");
                lblTheorVariance.Text = (k * θ * θ).ToString("0.0000");
            }

            LineItem Curve = DensityFuncPane.AddCurve("Theoretical", DensityFuncList, Color.FromArgb(100, Color.Red), SymbolType.None);
            Curve.Line.Width = 2f;
            Curve = DistribFuncPane.AddCurve("Theoretical", DistribFuncList, Color.FromArgb(100, Color.Red), SymbolType.None);
            Curve.Line.Width = 2f;

            DensityFuncList = new PointPairList();
            x = Left - Indent;
            for (int i = 0; i < graphDensityFunc.ClientSize.Width; i++)
            {
                DensityFuncList.Add(x, ExperemDensity(x, step));
                x += step;
            }          
            
            DistribFuncList = new PointPairList();
            x = Left - Indent;
            for (int i = 0; i < graphDistribFunc.ClientSize.Width; i++)
            {
                DistribFuncList.Add(x, ExperemDistrib(x, step));
                x += step;
            }

            DensityFuncPane.AddCurve("Experimental", DensityFuncList, Color.FromArgb(150, Color.Blue), SymbolType.None);
            DistribFuncPane.AddCurve("Experimental", DistribFuncList, Color.FromArgb(150, Color.Blue), SymbolType.None);


            graphDensityFunc.AxisChange();
            graphDensityFunc.Refresh();
            graphDistribFunc.AxisChange();
            graphDistribFunc.Refresh();            

            double Mean = 0, Variance = 0;
            for (int i = 0; i < RandomNumbers.Length; i++)
                Mean += RandomNumbers[i];
            Mean /= RandomNumbers.Length;
            for (int i = 0; i < RandomNumbers.Length; i++)
                Variance += (RandomNumbers[i] - Mean) * (RandomNumbers[i] - Mean);
            Variance /= RandomNumbers.Length;

            lblExperimMean.Text = Mean.ToString("0.0000");
            lblExperimVariance.Text = Variance.ToString("0.0000");
        }


        private void rbUniform_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUniform.Checked)
            {
                lblKParam.Visible = false;
                lblθParam.Visible = false;
                tbKParam.Visible = false;
                tbθParam.Visible = false;
            }
            else
            {
                lblKParam.Visible = true;
                lblθParam.Visible = true;
                tbKParam.Visible = true;
                tbθParam.Visible = true;
            }
            button1_Click(sender, e);
        }
    }
}
