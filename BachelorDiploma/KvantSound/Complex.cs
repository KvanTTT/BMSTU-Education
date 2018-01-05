using System;

namespace KvantSound
{
	/// <summary>
	/// Complex number.
	/// </summary>
	public struct Complex
	{
		public double Real;
		public double Imaginary;

		public Complex(double re)
		{
			this.Real = re;
			this.Imaginary = 0;
		}

		public Complex(double re, double im)
		{
			this.Real = re;
			this.Imaginary = im;
		}

		public static Complex operator *(Complex n1, Complex n2)
		{
			return new Complex(n1.Real * n2.Real - n1.Imaginary * n2.Imaginary,
				n1.Imaginary * n2.Real + n1.Real * n2.Imaginary);
		}

		public static Complex operator +(Complex n1, Complex n2)
		{
			return new Complex(n1.Real + n2.Real, n1.Imaginary + n2.Imaginary);
		}

		public static Complex operator -(Complex n1, Complex n2)
		{
			return new Complex(n1.Real - n2.Real, n1.Imaginary - n2.Imaginary);
		}

		public static Complex operator -(Complex n)
		{
			return new Complex(-n.Real, -n.Imaginary);
		}

		public static implicit operator Complex(double n)
		{
			return new Complex(n, 0);
		}

		public double Exp2()
		{
			double e = Math.Exp(Real);
			return e;
			//return new Complex(e * Math.Cos(Imaginary), e * Math.Sin(Imaginary));
		}

		public static Complex Exp(Complex x)
		{
			double e = Math.Exp(x.Real);
			return new Complex(e * Math.Cos(x.Imaginary), e * Math.Sin(x.Imaginary));
		}

		public double Power2()
		{
			return Real * Real - Imaginary * Imaginary;
		}

		public double AbsPower2()
		{
			return Real * Real + Imaginary * Imaginary;
		}

		public override string ToString()
		{
			return String.Format("{0}+i*{1}", Real, Imaginary);
		}
	}
}
