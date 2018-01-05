using System;
using System.Collections.Generic;

namespace TravellingSalesmanProblem
{
    class Chromosome
    {
        List<int> Cities = new List<int>();
        Random R;

        public Chromosome(int Num, Random R)
        {
            this.R = R;

            int Rab;

            List<int> Nums = new List<int>();

            for (int i = 0; i < Num; i++)
                Nums.Add(i);

            while (Nums.Count > 0)
            {
                Rab = R.Next(Nums.Count);
                Cities.Add(Nums[Rab]);
                Nums.Remove(Nums[Rab]);
            }
        }

        public Chromosome(Chromosome C1, Chromosome C2, Random R)
        {
            this.R = R;

            int Count = C1.GetNum();

            for (int i = 0; i < (Count >> 1); i++)
                Cities.Add(C1.Cities[i]);

            for (int i = 0; i < Count; i++)
            {
                if (Cities.Count == Count)
                    break;

                if (Cities.IndexOf(C2.Cities[i]) == -1)
                    Cities.Add(C2.Cities[i]);
            }

            for (int i = (Count >> 1); i < Count; i++)
            {
                if (Cities.Count == Count)
                    break;

                if (Cities.IndexOf(C1.Cities[i]) == -1)
                    Cities.Add(C2.Cities[i]);
            }
        }

        public void Mutation()
        {
            int Rab1 = R.Next(Cities.Count), Rab2, Rab = Cities[Rab1];

            do
            {
                Rab2 = R.Next(Cities.Count);
            }
            while (Rab1 == Rab2);

            Cities[Rab1] = Cities[Rab2];
            Cities[Rab2] = Rab;
        }

        public void PaintChromosome(List<City> C, Painter P)
        {
            P.Clear();

            for (int i = 0; i < Cities.Count - 1; i++)
                P.PaintEdge(C[Cities[i]], C[Cities[i + 1]]);

            for (int i = 0; i < Cities.Count; i++)
                P.PaintCity(C[i].GetX(), C[i].GetY());

            P.Refresh();
        }

        public int GetNum()
        {
            return Cities.Count;
        }

        public double GetSumLen(List<City> C)
        {
            double Result = 0;

            for (int i = 0; i < Cities.Count - 2; i++)
                Result += GetLen(C[Cities[i]], C[Cities[i + 1]]);

            return Result;
        }

        private double GetLen(City C1, City C2)
        {
            return Math.Sqrt((C2.GetX() - C1.GetX()) * (C2.GetX() - C1.GetX()) + (C2.GetY() - C1.GetY()) * (C2.GetY() - C1.GetY()));
        }
    }
}
