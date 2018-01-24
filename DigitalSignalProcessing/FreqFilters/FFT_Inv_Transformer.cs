using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Common
{
    public class FFT_Inv_Transformer
    {
        Func<double, Complex> fourier_img;
        int N, half_N;
        Complex[,] fft_inv;

        public FFT_Inv_Transformer(Func<double, Complex> fourier_img, int N)
        {
            this.N = N;
            this.half_N = N / 2;
            this.fourier_img = fourier_img;

            this.fft_inv = new Complex[(int)Math.Log(N, 2), N];

            CleanAndFill();
        }

        private void CleanAndFill()
        {
            ClearArray();
            FillArray();
        }

        private void ClearArray()
        {
            for (int i = 0; i < fft_inv.GetLength(0); i++)
                for (int j = 0; j < fft_inv.GetLength(1); j++)
					fft_inv[i, j] = -99999;
        }

        private void FillArray()
        {
            int i = -1;
            for (int k = 0; k < half_N; k++)
            {
                fft_inv[0, k] = S(k, 1, 0, ref i);
            }
        }

        public double FFT_Inv(double x)
        {
            int k = (int)Math.Round(x);
            if (k >= N)
                return 0;
            return fft_inv[0, k].Magnitude / N;
        }

        private double phi(int k, int N)
        {
            return (2 * Math.PI * k) / N;
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

			if (fft_inv[i, j] != -99999)
                res = fft_inv[i, j];
            else
            {
                Complex S0, wS1;

                if (a == half_N)
                {
                    S0 = fourier_img(j);
                    wS1 = fourier_img(j + half_N);
                }
                else
                {
                    S0 = S(k, a_next, b, ref i);
                    wS1 = S(k, a_next, a + b, ref i) * W(k, N / a);
                }

                res = S0 + wS1;
                fft_inv[i, j] = res;
                fft_inv[i, j + half_N] = S0 - wS1;
            }

            i--;
            return res;
        }
    }
}
