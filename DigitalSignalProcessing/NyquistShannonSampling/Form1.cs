using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NyquistShannonSampling
{
	public partial class Form1 : Form
	{
		private int SamplingRate;
		private double Magnitude;

		private double t0;
		private double T;
		private double Mean, Variance;

		private double DeltaFunc(double arg)
		{
			return Magnitude * (arg == t0 ? 1 : 0);
		}

		private double RectFunc(double arg)
		{
			return Magnitude * Rect((arg - T) / T);
		}

		private double Rect(double arg)
		{
			return arg >= 0 && arg <= 1 ? 1 : 0;
		}

		private double GaussianFunc(double arg)
		{
			return Magnitude * Math.Exp(-(arg - Mean) * (arg - Mean) / Variance);
		}

		private double SinC(double x)
		{
			return Math.Sin(x) / x;
		}

		private double[] Signal(int size, Func<double, double> func, double delta)
		{
			var result = new double[size];

			for (int i = 0; i < size; i++)
				for (int k = 0; k < size; k++)
					result[i] += func(k * delta) * SinC(Math.PI / delta * (i - k * delta));

			return result;
		}

		private double[] GenerateData(Func<double, double> signal)
		{
			var result = new double[SamplingRate];
			for (int i = 0; i < SamplingRate; i++)
				result[i] = signal(i);
			return result;
		}

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void btnRecalc_Click(object sender, EventArgs e)
		{
			double[] funcData = new double[0];
			SamplingRate = Convert.ToInt32(udSamplingRate.Value);
			Magnitude = Convert.ToDouble(udMagnitude.Value);
			Func<double, double> func = null;

			if (rbDelta.Checked)
			{
				t0 = Convert.ToDouble(udt0.Value);
				funcData = GenerateData(DeltaFunc);
				func = DeltaFunc;
			}
			else if (rbRectangle.Checked)
			{
				T = Convert.ToDouble(udT.Value);
				funcData = GenerateData(RectFunc);
				func = RectFunc;
			}
			else if (rbGaussian.Checked)
			{
				Mean = Convert.ToDouble(udMu.Value);
				Variance = Convert.ToDouble(udSigma.Value);
				funcData = GenerateData(GaussianFunc);
				func = GaussianFunc;
			}

			int size = (int)udSamplingRate.Value;

			var x = new double[size];
			for (int i = 0; i < size; i++)
				x[i] = i;

			var graphPane1 = zedGraphControl1.GraphPane;
			graphPane1.CurveList.Clear();

			graphPane1.AddCurve("original", x, funcData, Color.IndianRed, ZedGraph.SymbolType.None);

			var result = Signal(size, func, Convert.ToDouble(udDelta.Text));

			graphPane1.AddCurve("", x, result, Color.CornflowerBlue, ZedGraph.SymbolType.None);

			graphPane1.AxisChange();
			zedGraphControl1.Refresh();
		}
	}
}
