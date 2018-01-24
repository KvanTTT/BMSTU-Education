using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.Threading.Tasks;

namespace Convolutions
{
	public partial class frmMain : Form
	{
		private int SamplingRate;
		private double Magnitude;

		private double t0;
		private double T;
		private double Mean, Variance;

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

		/*double[] Convolution(double[] x, double[] h)
		{
			var result = new double[x.Length + h.Length - 1];
			for (int i = 0; i < x.Length; i++)
				for (int j = 0; j < h.Length; j++)
					result[i + j] += x[i] * h[j];
			return result;
		}*/

		double[] Convolution(double[] x, double[] h)
		{
			var result = new double[x.Length];
			for (int i = 0; i < x.Length; i++)
					result[i] = x[i] * h[i];
			return result;
		}

		Complex[] Convolution(Complex[] x, Complex[] h)
		{
			var result = new Complex[x.Length];
			for (int i = 0; i < x.Length; i++)
				result[i] = x[i] * h[i];
			return result;
		}

		public frmMain()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			brnCalc_Click(sender, e);
		}

		private double[] GenerateData(Func<int, double> signal)
		{
			var result = new double[SamplingRate];
			for (int i = 0; i < SamplingRate; i++)
				result[i] = signal(i);
			return result;
		}

		private void brnCalc_Click(object sender, EventArgs e)
		{
			List<double> rectFuncData = new List<double>();
			List<double> gaussFuncData = new List<double>();
			Complex[] fft1;
			Complex[] fft2;
			SamplingRate = Convert.ToInt32(udSamplingRate.Value);
			Magnitude = Convert.ToDouble(udMagnitude.Value);

            T = Convert.ToDouble(udT.Value);
			rectFuncData.AddRange(GenerateData(RectFunc));
			rectFuncData.AddRange(new double[rectFuncData.Count]);
			fft1 = CalculateComplex(rectFuncData.ToArray());

			Mean = Convert.ToDouble(udMu.Value);
			Variance = Convert.ToDouble(udSigma.Value);
			gaussFuncData.AddRange(GenerateData(GaussianFunc));
			gaussFuncData.AddRange(new double[gaussFuncData.Count]);
			fft2 = CalculateComplex(gaussFuncData.ToArray());

			Complex[] conv = new Complex[0];
			double[] restoredSignal = new double[0];

			var graphPane1 = zedGraphControl1.GraphPane;
			graphPane1.CurveList.Clear();
			var x = new double[fft1.Length];
			for (int i = 0; i < fft1.Length; i++)
				x[i] = i;

			if (rbRectRect.Checked)
			{
				conv = Convolution(fft1, fft1);
				graphPane1.AddCurve("signal 1", x, rectFuncData.ToArray(), Color.Pink, ZedGraph.SymbolType.None);
				graphPane1.AddCurve("signal 2", x, rectFuncData.ToArray(), Color.SandyBrown, ZedGraph.SymbolType.None);
			}
			else if (rbRectGauss.Checked)
			{
				conv = Convolution(fft1, fft2);
				graphPane1.AddCurve("signal 1", x, rectFuncData.ToArray(), Color.Pink, ZedGraph.SymbolType.None);
				graphPane1.AddCurve("signal 2", x, gaussFuncData.ToArray(), Color.SandyBrown, ZedGraph.SymbolType.None);
			}
			else if (rbGaussGauss.Checked)
			{
				conv = Convolution(fft2, fft2);
				graphPane1.AddCurve("signal 1", x, gaussFuncData.ToArray(), Color.Pink, ZedGraph.SymbolType.None);
				graphPane1.AddCurve("signal 2", x, gaussFuncData.ToArray(), Color.SandyBrown, ZedGraph.SymbolType.None);
			}

			restoredSignal = CalculateFft(conv, true);

			graphPane1.AddCurve("Convolution", x, restoredSignal, Color.DodgerBlue, ZedGraph.SymbolType.None);

			graphPane1.AxisChange();
			zedGraphControl1.Refresh();
		}
	}
}
