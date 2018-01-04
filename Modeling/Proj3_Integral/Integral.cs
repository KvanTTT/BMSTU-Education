using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvanceMath;

namespace Proj3_Integral
{
    class Integral
    {
        static double Trapec(double A, double B, int N, DoubleFunc Func)
        {
            double Result = 0;
            double t = Func(A);
            double dx = (B - A) / N;
            double t1 = Func(A + dx);
            double x = A;

            while (x < B)
            {
                Result += (t + t1) * 0.5;
                x += dx;
                t = t1;
                t1 = Func(x);
            }
            return Result;
        }


    }
}
