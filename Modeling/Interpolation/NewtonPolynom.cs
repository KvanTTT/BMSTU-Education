using System.Collections.Generic;

namespace AdvanceMath
{
	abstract class Polynom
    {
        public Polynom() {}
        public abstract double GetValueAt(double X);
    }

    class NewtonPolynom : Polynom
    {
        List<PointD> Points;
        double[] Coefs;

        public NewtonPolynom(List<PointD> Points)
        {
            this.Points = Points;
            Coefs = new double[Points.Count];
            BuildIterat();
        }

        private double CalcCoef(int StartInd, int Ind)
        {
            if (Ind == 0)
                return Points[StartInd].Y;

            return (CalcCoef(StartInd, Ind - 1) - CalcCoef(StartInd + 1, Ind - 1)) /
                   (Points[StartInd].X - Points[StartInd + Ind].X);
        }

        private double CalcCoef(int Ind)
        {
            return CalcCoef(0, Ind);
        }


        private void Build()
        {
            for (int i = 0; i < Points.Count; i++)
                Coefs[i] = CalcCoef(0, i);
        }

        private void BuildIterat()
        {
            Coefs[0] = Points[0].Y;
            double C = 1;
            //double CInv;
            for (int i = 1; i < Points.Count; i++)
            {
                /*for (int j = 0; j < i; j++)
                    C *= Points[j].X - Points[i].X;
                CInv = 1 / C;
                Coefs[i] = 0;
                for (int j = 0; j <= i; j++)
                {
                    Coefs[i] += Points[j] * ;
                }*/
                for (int j = 0; j <= i; j++)
                {
                    C = 1;
                    for (int k = 0; k <= i; k++)
                        if (k != j)
                            C *= Points[j].X - Points[k].X;
                    Coefs[i] += Points[j].Y / C;
                }
            }
        }

        public override double GetValueAt(double X)
        {
            double S = 1;
            double Result = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                Result += Coefs[i] * S;
                S *= X - Points[i].X;
            }
            return Result;
        }
    }
}
