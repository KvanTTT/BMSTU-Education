using System;
using System.Windows.Forms;

namespace Proj6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double lambda0 = Convert.ToDouble(tbLambda0.Text);
            double lambdaN = Convert.ToDouble(tbLambdaN.Text);
            double alpha = Convert.ToDouble(tbAlfa.Text);
            double l = Convert.ToDouble(tbLength.Text);
            double R = Convert.ToDouble(tbRadius.Text);
            double F0 = Convert.ToDouble(tbF0.Text);
            double T0 = Convert.ToDouble(tbT0.Text);
            int nodeCount = Convert.ToInt32(tbNodeCount.Text);

            /*double[] result = new BoundaryProblem(lambda0, lambdaN, alpha, R, l, F0, T0, nodeCount).Solve();

            GraphPane Pane = zgc.GraphPane;
            Pane.CurveList.Clear();

            PointPairList Points = new PointPairList();
            for (int i = 0; i < result.Length; i++)
                Points.Add(new PointPair((float)(i * l / nodeCount), (float)result[i]));

            LineItem myCurve1 = Pane.AddCurve("λ(t)", Points, Color.Red, SymbolType.None);

            zgc.AxisChange();
            zgc.Refresh();*/
        }
    }
}
