using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR4_FreqFilters
{
    class Summator
    {
        Func<double, double> f1;
        Func<double, double> f2;

        public Summator(Func<double, double> f1, Func<double, double> f2)
        {
            this.f1 = f1;
            this.f2 = f2;
        }

        public double Sum(double x)
        {
            return f1(x) + f2(x);
        }
    }
}
