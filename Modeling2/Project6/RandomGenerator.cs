using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7sem_model_6
{
    class RandomGenerator
    {
        int A, B;
        Random R = new Random();

        public RandomGenerator(int A, int B)
        {
            this.A = A;
            this.B = B;
        }

        public double Gen()
        {
            return A + (B - A) * R.NextDouble();
        }
    }
}
