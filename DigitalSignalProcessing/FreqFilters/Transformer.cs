using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using System.Numerics;

namespace Common
{
    public class Transformer
    {
        protected Func<double, double> signal;
        int _N, _half_N;
        double _min, _max, _step;

        Complex[,] fft_array;

        protected int N
        {
            get
            {
                return _N;
            }
            set
            {
                _N = value;
            }
        }

        protected int half_N
        {
            get
            {
                return _half_N;
            }
            set
            {
                _half_N = value;
            }
        }

        protected double min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        protected double max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        protected double step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value;
            }
        }

        public Transformer(Func<double, double> signal, int N, double min, double max)
        {
            this.signal = signal;
            this.N = N;
            this.half_N = N / 2;
            this.min = min;
            this.max = max;
            this.step = (max - min) / N;
            fft_array = new Complex[(int)Math.Log(N, 2), N];
            CleanAndFillFFTArray();
        }

        private void ClearFFT()
        {
            for (int i = 0; i < fft_array.GetLength(0); i++)
                for (int j = 0; j < fft_array.GetLength(1); j++)
					fft_array[i, j] = -99999;
        }

        /// <summary>
        /// Дискретизация сигнала
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public virtual double Discrete(double x)
        {
            int n = (int)Math.Round((x - min) / step);
            return signal(min + step * n) * Math.Pow(-1, n);
        }

        /// <summary>
        /// Дискретное преобразование Фурье
        /// </summary>
        /// <returns></returns>
        public virtual double DFT(double x)
        {
            int k = (int)Math.Round(x);
			return v(k).Magnitude;
        }

        /// <summary>
        /// Быстрое преобразование Фурье
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double FFT(double x)
        {
            int k = (int)Math.Round(x);
            if (k >= N)
                return 0;
            return fft_array[0, k].Magnitude;
        }

        /// <summary>
        /// Быстрое преобразование Фурье (комплексный результат)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Complex FFT_C(double x)
        {
            int k = (int)Math.Round(x);
            if (k >= N)
                return Complex.Zero;
            return fft_array[0, k];
        }        

        private void CleanAndFillFFTArray()
        {
            ClearFFT();
            FillFastFourierArray();
        }

        protected virtual void FillFastFourierArray()
        {            
            int i = -1;         
            for (int k = 0; k < half_N; k++)
            {          
                fft_array[0, k] = S(k, 1, 0, ref i);
            }
        }

        private Complex v(int k)
        {            
            Complex sum = Complex.Zero;
            double u, phi;

            phi = (-2 * Math.PI * k) / N;
            for (int n = 0; n < N; n++)
            {
                u = Discrete(min + n * step);
                sum += new Complex(u * Math.Cos(phi * n), u * Math.Sin(phi * n));
            }

            return sum;
        }

        private double phi(int k, int N)
        {
            return (-2 * Math.PI * k) / N;
        }

        private Complex W(int k, int N)
        {
            double _phi = phi(k, N);
            return new Complex(Math.Cos(_phi), Math.Sin(_phi));
        }

        private Complex S(int k, int a, int b, ref int i)
        {
            i++;
            Complex res;
            int a_next = a * 2;

            int j = a * k + b;

            if (fft_array[i, j] != -99999)
                res = fft_array[i, j];
            else
            {
                Complex S0, wS1;

                if (a == half_N)
                {
                    S0 = Discrete(min + j * step);
                    wS1 = Discrete(min + (j + half_N) * step);
                }
                else
                {
                    S0 = S(k, a_next, b, ref i);
                    wS1 = S(k, a_next, a + b, ref i) * W(k, N / a);
                }

                res = S0 + wS1;
                fft_array[i, j] = res;
                fft_array[i, j + half_N] = S0 - wS1;
            }

            i--;
            return res;
        }
    }
}
