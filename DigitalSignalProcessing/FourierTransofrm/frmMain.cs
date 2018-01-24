using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace FourierTransofrm
{
	public partial class frmMain : Form
	{
		private int SamplingRate;
		private double Magnitude;

		private double t0;
		private double T;
		private double Mean, Variance;

		private double DeltaFunc(int arg)
		{
			return Magnitude * arg == t0 ? 1 : 0;
		}

		private double RectFunc(int arg)
		{
			return Magnitude * Rect((arg - T) / T);
		}

		private double Rect(double arg)
		{
			return arg >= 0 && arg <= 1 ? 1 : 0;
		}

		private double GaussianFunc(int arg)
		{
			return Magnitude * Math.Exp(-(arg - Mean) * (arg - Mean) / Variance);
		}

		#region Dft

		private double[] CalculateDft(double[] data)
		{
			int n = data.Length;
			double[] real = new double[n];
			double[] imag = new double[n];
			double[] result = new double[n];
			double twoPiDivN = 2.0 * Math.PI / n;
			for (int i = 0; i < n; i++)
			{
				double a = i * twoPiDivN;
				for (int j = 0; j < n; j++)
				{
					real[i] += data[j] * Math.Cos(a * j);
					imag[i] += data[j] * Math.Sin(a * j);
				}
				result[i] = Math.Sqrt(real[i] * real[i] + imag[i] * imag[i]) / n;
			}

			return result;
		}

		#endregion

		#region Fft

		public double[] CalculateFft(double[] x, bool inverse = false)
		{
			Complex[] Data = CalculateComplex(x, inverse);

			// calculate spectrogram
			double[] spectrogram = new double[Data.Length];
			for (int i = 0; i < spectrogram.Length; i++)
				spectrogram[i] = Math.Sqrt(Data[i].Real * Data[i].Real + Data[i].Imaginary * Data[i].Imaginary);
			return spectrogram;
		}

		public double[] CalculateFft(Complex[] x, bool inverse = false)
		{
			Complex[] Data = CalculateComplex(x, inverse);

			// calculate spectrogram
			double[] spectrogram = new double[Data.Length];
			for (int i = 0; i < spectrogram.Length; i++)
			{
				spectrogram[i] = Math.Sqrt(Data[i].Real * Data[i].Real + Data[i].Imaginary * Data[i].Imaginary);
				if (inverse)
					spectrogram[i] /= spectrogram.Length;
			}
			return spectrogram;
		}

		public Complex[] CalculateComplex(double[] x, bool inverse = false)
		{
			return CalculateComplex(x.Select(y => new Complex(y, 0)).ToArray(), inverse);
		}

		public Complex[] CalculateComplex(Complex[] x, bool inverse = false)
		{
			int length;
			int bitsInLength;
			if (IsPowerOfTwo(x.Length))
			{
				length = x.Length;
				bitsInLength = Log2(length) - 1;
			}
			else
			{
				bitsInLength = Log2(x.Length);
				length = 1 << bitsInLength;
			}

			// bit reversal
			Complex[] data = new Complex[length];
			for (uint i = 0; i < x.Length; i++)
			{
				uint j = ReverseBits2(i, bitsInLength);
				data[j] = x[i];
			}

			// Cooley-Tukey 
			for (int i = 0; i < bitsInLength; i++)
			{
				int m = 1 << i;
				int n = m * 2;
				double alpha = (2 * Math.PI / n);

				for (int k = 0; k < m; k++)
				{
					// e^(-2*pi/N*k)

					Complex oddPartMultiplier = inverse ?
						Complex.Exp(new Complex(0, alpha * k)) :
						Complex.Exp(new Complex(0, -alpha * k));

					for (int j = k; j < length; j += n)
					{
						Complex evenPart = data[j];
						Complex oddPart = oddPartMultiplier * data[j + m];
						data[j] = evenPart + oddPart;
						data[j + m] = evenPart - oddPart;
					}
				}
			}

			return data;
		}

		/// <summary>
		/// Gets number of significat bytes.
		/// </summary>
		/// <param name="n">Number</param>
		/// <returns>Amount of minimal bits to store the number.</returns>
		private static int Log2(int n)
		{
			int i = 0;
			while (n > 0)
			{
				++i;
				n >>= 1;
			}
			return i;
		}

		/// <summary>
		/// Reverses bits in the number.
		/// </summary>
		/// <param name="n">Number</param>
		/// <param name="bitsCount">Significant bits in the number.</param>
		/// <returns>Reversed binary number.</returns>
		private static int ReverseBits(int n, int bitsCount)
		{
			int reversed = 0;
			for (int i = 0; i < bitsCount; i++)
			{
				int nextBit = n & 1;
				n >>= 1;

				reversed <<= 1;
				reversed |= nextBit;
			}
			return reversed;
		}

		/// <summary>
		/// Reverses bits in the number.
		/// </summary>
		/// <param name="n">Number</param>
		/// <param name="bitsCount">Significant bits in the number.</param>
		/// <returns>Reversed binary number.</returns>
		private static uint ReverseBits2(uint n, int bitsCount)
		{
			// swap odd and even bits
			n = ((n >> 1) & 0x55555555) | ((n & 0x55555555) << 1);
			// swap consecutine pairs
			n = ((n >> 2) & 0x33333333) | ((n & 0x33333333) << 2);
			// swap nibbles ... 
			n = ((n >> 4) & 0x0F0F0F0F) | ((n & 0x0F0F0F0F) << 4);
			// swap bytes
			n = ((n >> 8) & 0x00FF00FF) | ((n & 0x00FF00FF) << 8);
			// swap 2-byte long pairs
			n = (n >> 16) | (n << 16);

			n = n >> (32 - bitsCount);
 
			return n;
		}

		/// <summary>
		/// Checks if number is power of 2.
		/// </summary>
		/// <param name="n">number</param>
		/// <returns>true if n=2^k and k is positive integer</returns>
		private static bool IsPowerOfTwo(int n)
		{
			return n > 1 && (n & (n - 1)) == 0;
		}

		public static double Abs2(Complex value)
		{
			return value.Real * value.Real + value.Imaginary * value.Imaginary;
		}

		#endregion

		private double[] GenerateData(Func<int, double> signal)
		{
			var result = new double[SamplingRate];
			for (int i = 0; i < SamplingRate; i++)
				result[i] = signal(i);
			return result;
		}

		private double[] RemoveTwinsEffect(double[] signal)
		{
			var result = new double[SamplingRate];
			for (int i = 0; i < signal.Length; i++)
				result[i] = signal[i] * (i % 2 == 0 ? 1 : - 1);
			return result;
		}

		public frmMain()
		{
			InitializeComponent();
		}

		private void rbDelta_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender is RadioButton) && !(sender as RadioButton).Checked)
				return;

			double[] funcData = new double[0];
			SamplingRate = Convert.ToInt32(udSamplingRate.Value);
			Magnitude = Convert.ToDouble(udMagnitude.Value);

			if (rbDelta.Checked)
			{
				t0 = Convert.ToDouble(udt0.Value);
				funcData = RemoveTwinsEffect(GenerateData(DeltaFunc));
			}
			else if (rbRectangle.Checked)
			{
				T = Convert.ToDouble(udT.Value);
				funcData = RemoveTwinsEffect(GenerateData(RectFunc));
			}
			else if (rbGaussian.Checked)
			{
				Mean = Convert.ToDouble(udMu.Value);
				Variance = Convert.ToDouble(udSigma.Value);
				funcData = RemoveTwinsEffect(GenerateData(GaussianFunc));
			}

			var x = new double[SamplingRate];
			for (int i = 0; i < SamplingRate; i++)
				x[i] = i;

			var graphPane1 = zedGraphControl1.GraphPane;
			var graphPane2 = zedGraphControl2.GraphPane;
			
			graphPane1.CurveList.Clear();
			//graphPane1.AddCurve("signal", x, funcData, Color.IndianRed, ZedGraph.SymbolType.None);

			lblDftTime.Text = null;
			lblFftTime.Text = null;

			graphPane2.CurveList.Clear();
			if (cbDFT.Checked)
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();
				var dft = CalculateDft(funcData);
				stopWatch.Stop();
				lblDftTime.Text = stopWatch.Elapsed.ToString();
				graphPane2.AddCurve("dft", x, dft, Color.CornflowerBlue, ZedGraph.SymbolType.None);
			}

			if (cbFFT.Checked)
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();
				var complexFft = CalculateComplex(funcData);
				var fft = CalculateFft(funcData);
				stopWatch.Stop();
				lblFftTime.Text = stopWatch.Elapsed.ToString();
				graphPane2.AddCurve("fft", x, fft, Color.LawnGreen, ZedGraph.SymbolType.None);

				var restoredSignal = CalculateFft(complexFft, true);
				graphPane1.AddCurve("restored fft", x, restoredSignal, Color.DarkBlue, ZedGraph.SymbolType.None);
			}

			graphPane1.AxisChange();
			graphPane2.AxisChange();
			zedGraphControl1.Refresh();
			zedGraphControl2.Refresh();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			rbDelta_CheckedChanged(sender, e);
		}
	}
}
