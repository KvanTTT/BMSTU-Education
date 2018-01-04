using System.Collections.Generic;

namespace AdvanceMath
{
	class Spline : Polynom
    {
        List<PointD> Points;
        int Count;

        double MinX, MaxX, YMinX;

        double[] H;
        double[] B;
        double[] F;
        double[] Ksi;
        double[] Nu;
        double[,] C;


        public Spline(List<PointD> Points)
        {
            this.Points = Points;
            this.Count = Points.Count;
            Build();
        }

        private void CalcMinMaxX()
        {
            MinX = Points[0].X;
            YMinX = Points[0].Y;
            MaxX = Points[0].X;
            for (int i = 1; i < Count; i++)
            {
                if (Points[i].X < MinX)
                {
                    MinX = Points[i].X;
                    YMinX = Points[i].Y;
                }
                if (Points[i].X > MaxX)
                    MaxX = Points[i].X;
            }
        }

        private void CalcH()
        {
            H = new double[Count];
            for (int i = 1; i < Count; i++)
                H[i] = Points[i].X - Points[i - 1].X;
        }

        private void CalcB()
        {
            B = new double[Count + 1];
            for (int i = 2; i < Count; i++)
                B[i] = -2 * (H[i - 1] + H[i]);
        }

        private void CalcF()
        {
            F = new double[Count + 1];
            for (int i = 2; i < Count; i++)
                F[i] = 3 * (-Points[i].Y + 2 * Points[i - 1].Y - Points[i - 2].Y) / H[i];
        }

        private void CalcCoefs()
        {
            Ksi = new double[Count + 2];
            Nu = new double[Count + 2];
            Ksi[2] = 0;
            Nu[2] = 0;
            for (int i = 3; i < Count + 1; i++)
            {
                Ksi[i] = H[i - 1] / (B[i - 1] - H[i - 2] * Ksi[i - 1]);
                Nu[i] = (H[i - 2] * Nu[i - 1] + F[i - 1]) / (B[i - 1] - H[i - 2] * Ksi[i - 1]);
            }
        }

        private void CalcC()
        {
            C = new double[Count, Count + 2];
            for (int j = 1; j < Count; j++)
            {
                C[j, Count + 1] = 0;
                for (int i = Count; i >= j; i--)
                    C[j, i] = Ksi[i + 1] * C[j, i + 1] + Nu[i + 1];
            }
        }

        private void Build()
        {
            CalcMinMaxX();
            CalcH();
            CalcB();
            CalcF();
            CalcCoefs();
            CalcC();
        }

        public override double GetValueAt(double X)
        {
            if ((X < MinX) || (X > MaxX))
                return YMinX - 10;

            if (X == MinX)
                return YMinX;
            
            int i;
            for (i = 0; X > Points[i].X; i++);
             
            double D = X - Points[i - 1].X;
            return (D * (D * ((D * 
                (C[i, i + 1] - C[i, i]) / (3 * H[i])) 
                + C[i, i]) + 
                (Points[i].Y - Points[i - 1].Y) / H[i] - H[i] * (C[i, i + 1] + 2 * C[i, i]) / 3) 
                + Points[i - 1].Y);
        }

    }
}
