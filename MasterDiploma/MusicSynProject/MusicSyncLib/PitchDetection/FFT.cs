using System;
using System.Numerics;
using System.Threading.Tasks;

namespace MusicSyncLib
{
	public class FFT
	{
		#region Properties

		public bool Parallelization
		{
			get;
			set;
		}

		public WindowType WindowType
		{
			get;
			set;
		}

		#endregion

		#region Constructors

		public FFT(WindowType WindowType = WindowType.Rectangle, bool parallelization = true)
		{
			this.WindowType = WindowType;
			Parallelization = parallelization;
		}

		#endregion

		#region Public Methods

		public double[] ConvertToMagnitude(double[] fft)
		{
			double[] result = new double[fft.Length];
			for (int i = 0; i < result.Length; i++)
				result[i] = Abs2(fft[i]);
			return result;
		}

		public float[] Calculate(float[] sample)
		{
			Complex[] Data = CalculateComplex(sample);

			float[] spectrogram = new float[Data.Length];
			for (int i = 0; i < spectrogram.Length; i++)
				spectrogram[i] = (float)Abs2(Data[i]);
			return spectrogram;
		}

		public Complex[] CalculateComplex(float[] sample)
		{
			//double[] x1 = new double[x.Length];
			//x.CopyTo(x1, 0);
			float[] x1 = sample;

			int length;
			int bitsInLength;
			if (IsPowerOfTwo(x1.Length))
			{
				length = x1.Length;
				bitsInLength = Log2(length) - 1;
			}
			else
			{
				bitsInLength = Log2(x1.Length);
				length = 1 << bitsInLength;
			}

			// bit reversal
			Complex[] data = new Complex[length];
			for (uint i = 0; i < x1.Length; i++)
			{
				uint j = ReverseBits(i, bitsInLength);
				data[j] = x1[i];
			}

			// Cooley-Tukey 
			for (int i = 0; i < bitsInLength; i++)
			{
				int m = 1 << i;
				int n = m * 2;
				double alpha = -(2 * Math.PI / n);

				if (Parallelization)
					Parallel.For(0, m, k =>
					{
						// e^(-2*pi/N*k)

						Complex oddPartMultiplier = Complex.Exp(new Complex(0, alpha * k));

						for (int j = k; j < length; j += n)
						{
							Complex evenPart = data[j];
							Complex oddPart = oddPartMultiplier * data[j + m];
							data[j] = evenPart + oddPart;
							data[j + m] = evenPart - oddPart;
						}
					});
				else
					for (int k = 0; k < m; k++)
					{
						// e^(-2*pi/N*k)
						Complex oddPartMultiplier = Complex.Exp(new Complex(0, alpha * k));

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

		#endregion

		#region Private Methods

		private void ApplyWindow(double[] sample)
		{
			double Coef;
			double[] SampleData = sample;
			switch (WindowType)
			{
				default:
				case WindowType.Rectangle:
					break;

				case WindowType.Sin:
					Coef = Math.PI / (SampleData.Length - 1);
					for (int i = 0; i < SampleData.Length; i++)
						SampleData[i] *= Math.Sin(i * Coef);
					break;

				case WindowType.Lanczos:
					Coef = 2.0 / (SampleData.Length - 1);
					double x;
					for (int i = 0; i < SampleData.Length; i++)
					{
						x = i * Coef - 1;
						SampleData[i] *= Math.Sin(Math.PI * i) / Math.PI * i;
					}
					break;

				case WindowType.Bartlett:
					Coef = (SampleData.Length - 1) / 2.0;
					for (int i = 0; i < SampleData.Length; i++)
						SampleData[i] *= 1 - Math.Abs(i / Coef - 1);
					break;

				case WindowType.Hann:
					Coef = 2 * Math.PI / (SampleData.Length - 1);
					for (int i = 0; i < SampleData.Length; i++)
						SampleData[i] *= 0.5 * (1 - Math.Cos(i * Coef));
					break;

				case WindowType.Hamming:
					Coef = 2 * Math.PI / (SampleData.Length - 1);
					for (int i = 0; i < SampleData.Length; i++)
						SampleData[i] *= 0.53836 - 0.46164 * Math.Cos(i * Coef);
					break;

				case WindowType.Blackman:
					const double alpha = 0.16;

					double a0 = (1 - alpha) * 0.5;
					const double a1 = 0.5;
					double a2 = alpha * 0.5;
					Coef = 2 * Math.PI / (SampleData.Length - 1);
					double Coef2 = Coef * 2;
					for (int i = 0; i < SampleData.Length; i++)
						SampleData[i] *= a0 - a1 * Math.Cos(Coef) + a2 * Math.Cos(Coef2);
					break;

			}
		}

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

		private static uint ReverseBits(uint n, int bitsCount)
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
	}
}
