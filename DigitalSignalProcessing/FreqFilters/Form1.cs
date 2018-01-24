using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using Common;

namespace FreqFilters
{
	public partial class Form1 : Form
	{
		FunctionLibrary flib = new FunctionLibrary(1, 0.5);
		Clutters clt;

		Func<double, double> clutter;
		Func<double, Complex> filter;

		Plotter plt;
		double pltstep = 0.01;
		double min = -3, max = 3;
		int N = 256;

		public Func<double, double> Clutter
		{
			get
			{
				return clutter;
			}
			set
			{
				clutter = value;
				Filters.clutter = new Transformer(clutter, N, min, max).FFT;
			}
		}

		Action _actPlotting;

		private Action ActPlotting
		{
			get
			{
				return _actPlotting;
			}
			set
			{
				_actPlotting = value;
			}
		}

		public double B
		{
			get
			{
				return Filters.B;
			}
			set
			{
				Filters.B = value;
			}
		}

		public double Sigma
		{
			get
			{
				return Filters.sigma;
			}
			set
			{
				Filters.sigma = value;
			}
		}

		public double WienerCoeff
		{
			get
			{
				return Filters.coeff;
			}
			set
			{
				Filters.coeff = value;
			}
		}

		private void PlotTime()
		{
			Func<double, double> f = x => clutter(x) + flib.Gauss(x);
			Transformer trns = new Transformer(f, N, min, max);
			Func<double, double> xmod;
			if (filter != Filters.Wiener)
				xmod = (x => N / 2 - x);
			else
				xmod = (x => x);
			FFT_Inv_Transformer ft = new FFT_Inv_Transformer(x => trns.FFT_C(x) * filter(xmod(x)), N);

			PlotableFunction f_in = new PlotableFunction(f, "Input Func", 1);
			PlotableFunction f_out = new PlotableFunction(x => ft.FFT_Inv((x - min) * N / (max - min)), "Result", 2);

			plt.Plot(new PlotableFunction[] { f_in, f_out }, min, max);
		}

		private void PlotSpectr()
		{
			Func<double, double> f = x => clutter(x) + flib.Gauss(x);
			Transformer trns = new Transformer(f, N, min, max);

			Func<double, double> xmod;
			if (filter != Filters.Wiener)
				xmod = (x => N / 2 - x);
			else
				xmod = (x => x);
			FFT_Inv_Transformer ft = new FFT_Inv_Transformer(x => trns.FFT_C(x) * filter(xmod(x)), N);

			PlotableFunction f_in = new PlotableFunction(trns.FFT, "Input Func", 1);
			PlotableFunction f_filt = new PlotableFunction(x => filter(xmod(x)).Magnitude, "Filter", 2);
			PlotableFunction f_res = new PlotableFunction(x => (trns.FFT_C(x) * filter(xmod(x))).Magnitude, "Result", 3);

			plt.Plot(new PlotableFunction[] { f_filt, f_in, f_res }, 0, N);
		}

		private void Redraw()
		{
			ActPlotting();
		}

		private void btnRecalc_Click(object sender, EventArgs e)
		{
			Redraw();
		}

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			zedGraphControl1.GraphPane.Title.Text = "Filtering";

			Filters.signal = new Transformer(flib.Gauss, N, min, max).FFT;
			clt = new Clutters(min, max, 10);
			plt = new Plotter(zedGraphControl1, pltstep);

			if (rbImpulse.Checked)
				Clutter = clt.Impulse;
			else
				Clutter = clt.Gauss;

			if (rbTime.Checked)
				ActPlotting = PlotTime;
			else
				ActPlotting = PlotSpectr;

			filter = Filters.LF_IIR_Butterwort;

			cmbFilterType.SelectedIndex = 0;
		}

		private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cmbFilterType.SelectedIndex)
			{
				case 0: filter = Filters.LF_IIR_Butterwort; break;
				case 1: filter = Filters.LF_FIR_Butterwort; break;
				case 2: filter = Filters.LF_FIR_Gauss; break;
				case 3: filter = Filters.HF_IIR_Butterwort; break;
				case 4: filter = Filters.HF_FIR_Butterwort; break;
				case 5: filter = Filters.HF_FIR_Gauss; break;
				case 6: filter = Filters.Wiener; break;
			};

			Redraw();
		}

		private void rbImpulse_CheckedChanged(object sender, EventArgs e)
		{
			if (rbImpulse.Checked)
				Clutter = clt.Impulse;
			else
				Clutter = clt.Gauss;

			Redraw();
		}

		private void rbTime_CheckedChanged(object sender, EventArgs e)
		{
			if (rbTime.Checked)
				ActPlotting = PlotTime;
			else
				ActPlotting = PlotSpectr;

			Redraw();
		}
	}
}
