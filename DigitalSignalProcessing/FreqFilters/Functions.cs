using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{     
    public class FunctionLibrary
    {
        double a = 1, t0 = 1;
        double rect_d = 0, rect_mult = 1;
        double sigma = 0.4;

        double eps = 0.00001;

        public double A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        public double T0
        {
            get
            {
                return t0;
            }
            set
            {
                t0 = value;
            }
        }

        public double Sigma
        {
            get
            {
                return sigma;
            }
            set
            {
                sigma = value;
            }
        }

        public Func<double, double> this[string name]
        {
            get
            {
                Func<double, double> result = null;
                switch (name)
                {
                    case "Impulse": result = this.Impulse; break;
                    case "RectImpulse": result = this.RectImpulse; break;
                    case "Gauss": result = this.Gauss; break;
                };

                return result;
            }            
        }

        public FunctionLibrary()
        {
        }

        public FunctionLibrary(double rect_d, double rect_mult)
        {
            this.rect_d = rect_d;
            this.rect_mult = rect_mult;
        }

        public double Impulse(double x)
        {
            return A * Delta(x);
        }

        public double RectImpulse(double x)
        {
            return A * Rect(rect_mult * (x + rect_d));
        }

        public double Gauss(double x)
        {
            return A * Math.Exp(-(x * x / (sigma * sigma)));
        }

        public double Delta(double x)
        {
            return Convert.ToDouble(Math.Abs(x - t0) < eps);
        }

        public double Rect(double x)
        {
            return Convert.ToDouble(x >= 0 && x <= 1);
        }
    }
}
