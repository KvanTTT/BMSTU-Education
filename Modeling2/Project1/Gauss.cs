using System;

namespace Project1
{
    class Gauss
    {
        private static bool MakeTriangle(ref double[,] Matrix, int N)
        {
            int q;

            for (int i = 0; i < N; i++)
            {
                if (Matrix[i, i] == 0)
                {
                    q = -1;
                    for (int j = i + 1; j < N; j++)
                        if (Matrix[j, j] != 0)
                        {
                            q = j;
                            break;
                        }

                    if (q == -1)
                        return false;
                    else
                        for (int j = 0; j <= N; j++)
                        {
                            double t = Matrix[i, j];
                            Matrix[i, j] = Matrix[q, j];
                            Matrix[q, j] = t;
                        }
                }

                for (int j = N; j >= i; j--)                             
                    Matrix[i, j] /= Matrix[i, i];

                for (int j = i + 1; j < N; j++)
                    for (int k = N; k >= 0; k--)
                        Matrix[j, k] -= Matrix[i, k] * Matrix[j, i];
            }

            return true;
        }

        private static double[] GetSolveFromTriangle(double[,] Matrix, int N)
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

        private static double GetDet(double[,] Matrix, int N)
        {
            double Det = 1;

            for (int i = 0; i < N; i++)
                Det *= Matrix[i, i];

            return Det;
        }

        public static double[] Solve(double[,] Matrix)
        {
            double[] Result;

            int N = Matrix.GetLength(0);
            if (MakeTriangle(ref Matrix, N))
                Result = GetSolveFromTriangle(Matrix, N);
            else
                throw new Exception("Singular matrix. No solution (det = 0)");

            return Result;
        }
    }
}
