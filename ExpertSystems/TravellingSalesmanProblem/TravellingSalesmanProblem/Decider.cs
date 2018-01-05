using System;
using System.Collections.Generic;

namespace TravellingSalesmanProblem
{
    class Solver
    {
        int Num, MaxNumRepeats, MaxNumPopulations, NumRepeats = 0, NumPopulations = 0;
        double PMut, MinLen;

        Random R = new Random();

        List<City> C;

        List<Chromosome> Population = new List<Chromosome>();

        public Solver(int Num, double PMut, int MaxNumRepeats, int MaxNumPopulations, List<City> C)
        {
            this.Num = Num;
            this.PMut = PMut;
            this.MaxNumRepeats = MaxNumRepeats;
            this.MaxNumPopulations = MaxNumPopulations;
            this.C = C;
        }

        public bool Decide(Painter P)
        {
            bool Flag = true;

            GenFirstPopulation(Num);

            MinLen = Population[0].GetSumLen(C);

            while (!IsFinish())
            {
                for (int i = 0; i < Num; i++)
                    if (IsMut())
                        Population[i].Mutation();
                    
                Population.Sort((A, B) => B.GetSumLen(C).CompareTo(A.GetSumLen(C)));

                for (int i = ((Num >> 1) + (Num % 2)); i < Num; i++)
                    Population.Add(new Chromosome(Population[i], Population[i + 1], R));

                Population.Sort((A, B) => B.GetSumLen(C).CompareTo(A.GetSumLen(C)));
                Population.RemoveRange(0, Num >> 1);

                NumPopulations++;

                if (Population[Num - 1].GetSumLen(C) == MinLen)
                    NumRepeats++;
                else
                {
                    NumRepeats = 0;

                    if (Population[Num - 1].GetSumLen(C) < MinLen)
                        MinLen = Population[Num - 1].GetSumLen(C);
                }

                Population[Num - 1].PaintChromosome(C, P);
            }

            if (NumPopulations > MaxNumPopulations)
                Flag = false;
            
            return Flag;
        }

        private bool IsFinish()
        {
            return ((NumPopulations > MaxNumPopulations) || (NumRepeats > MaxNumRepeats));
        }

        private bool IsMut()
        {
            Random R = new Random();

            return (R.NextDouble() < PMut); 
        }

        private void GenFirstPopulation(int Count)
        {
            for (int i = 0; i < Num; i++)
                Population.Add(new Chromosome(C.Count, R));
        }
    }
}
