using System;

namespace AdvanceMath
{
    public class SLE
    {
        static bool MakeTriangle(ref double[,] Matrix, int N)
        {
            int q;
            int i, j;

            for (i = 0; i < N; i++)
            {
                if (Matrix[i, i] == 0)
                {
                    q = -1;
                    for (j = i + 1; j < N; j++)
                        if (Matrix[j, j] != 0)
                        {
                            q = j;
                            break;
                        }
                    if (q == -1)
                        return false;
                    else
                        for (j = 0; j <= N; j++)
                        {
                            double t = Matrix[i, j];
                            Matrix[i, j] = Matrix[q, j];
                            Matrix[q, j] = t;
                        }
                }
                for (j = N; j >= i; j--)                             // т.к. последний столбец, то N!!!
                    Matrix[i, j] /= Matrix[i, i];
                for (j = i + 1; j < N; j++)
                    for (int k = N; k >= 0; k--)
                        Matrix[j, k] -= Matrix[i, k] * Matrix[j, i];
            }

            return true;
        }

        static double[] GetSolveFromTriangle(double[,] Matrix, int N)
        {
            double[] Result = new double[N];
            double s;
            for (int i = N - 1; i >= 0; i--)
            {
                s = 0;
                for (int j = i + 1; j < N; j++)
                    s += Matrix[i, j] * Result[j];
                Result[i] = Matrix[i, N] - s;
            }
            return Result;
        }

        // Решение СЛАУ методом Гаусса
        public static double[] GaussSolve(double[,] Matrix, int N)
        {
            double[] Result;

            if (MakeTriangle(ref Matrix, N))
                Result = GetSolveFromTriangle(Matrix, N);
            else
                throw new Exception();

            return Result;
        }
    }
}
