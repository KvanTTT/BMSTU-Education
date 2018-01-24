using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FreqFilters
{
    public static class Filters
    {
        public static double tau = 0.166666666;
        public static double B = 25;
        public static double sigma = 20;
        public static double coeff = 0.05;

        static double pi = Math.PI;

        static double G0 = 1;
        static double a1 = 1.41421;
        static double a2 = 1;

        public static Func<double, double> clutter;
        public static Func<double, double> signal;

        public static Complex LF_IIR_Butterwort(double f)
        {
            double k = f / B;
            return G0 / (1 + a1 * Complex.ImaginaryOne * k - a2 * k * k);
        }

        public static Complex LF_FIR_Butterwort(double f)
        {
            double k = f / B;
            return 1 / (1 + k * k);
        }

        public static Complex LF_FIR_Gauss(double f)
        {
            return Math.Exp(- f * f / (2 * sigma * sigma));
        }

        public static Complex HF_IIR_Butterwort(double f)
        {
            return 1 - LF_IIR_Butterwort(f);
        }

        public static Complex HF_FIR_Butterwort(double f)
        {
            double k = B / f;
            return 1 / (1 + k * k);
        }

        public static Complex HF_FIR_Gauss(double f)
        {
            return 1 - LF_FIR_Gauss(f);
        }

        public static Complex Wiener(double f)
        {
            double cappa = clutter(f), betha = signal(f);
            return MuchLess(cappa * cappa, betha * betha) ? 1 : 0;
        }

        private static bool MuchLess(double a, double b)
        {
            return a <= coeff * b;
        }
    }
}
