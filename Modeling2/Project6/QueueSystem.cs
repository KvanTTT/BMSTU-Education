using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7sem_model_6
{
    class QueueSystem
    {
        public static int NumDoors = 2, NumPayOffices = 2, NumTurnstiles = 6;

        List<double> TDHand = new List<double>(NumDoors) { 0, 0 };
        List<bool> IsDHand = new List<bool>(NumDoors) { false, false };
        List<double> TPHand = new List<double>(NumPayOffices) { 0, 0 };
        List<bool> IsPHand = new List<bool>(NumPayOffices) { false, false };
        List<double> TTHand = new List<double>(NumTurnstiles) { 0, 0, 0, 0, 0, 0 };
        List<bool> IsTHand = new List<bool>(NumTurnstiles) { false, false, false, false, false, false };

        List<Queue<Request>> DQueues = new List<Queue<Request>>() { new Queue<Request>(), new Queue<Request>() };
        List<Queue<Request>> PQueues = new List<Queue<Request>>() { new Queue<Request>(), new Queue<Request>() };
        List<Queue<Request>> TQueues = new List<Queue<Request>>() { new Queue<Request>(), new Queue<Request>(), new Queue<Request>(),
                                                                    new Queue<Request>(), new Queue<Request>(), new Queue<Request>() };

        RandomGenerator RG = new RandomGenerator(1, 1);

        public void Processing(Generator G, List<Door> D, List<PayOffice> P, List<Turnstile> T, int Num, double IsToPayOffice,
                               ref double TotalTime, out double MeanTime, ref List<double> Mean, double RepeatProb)
        {
            double Ttek = 0, dT = 0.01, TGen = 0;
            int NumWentToStation = 0, ToPayOffice, RabI;
            List<int> RabMax = new List<int>();
            List<int> RabTotal = new List<int>();
            List<int> RabNum = new List<int>();

            for (int i = 0; i < NumDoors + NumPayOffices + NumTurnstiles; i++)
            {
                RabMax.Add(0);
                RabTotal.Add(0);
                RabNum.Add(0);
            }

            Random Rand = new Random();
            while (NumWentToStation < Num)
            {
                if (TGen <= 0)
                {
                    TGen = G.Gen();

                    if (RG.Gen() <= IsToPayOffice)
                        ToPayOffice = 0;
                    else
                        ToPayOffice = 1;
                }
                else
                    ToPayOffice = 2;

                for (int i = 0; i < NumDoors; i++)
                {
                    if (TDHand[i] <= 0)
                    {
                        if (IsDHand[i])
                        {
                            Request Rab = DQueues[i].Dequeue();

                            if (DQueues[i].Count > RabMax[i])
                                RabMax[i] = DQueues[i].Count;

                            RabTotal[i] += DQueues[i].Count;
                            RabNum[i]++;

                            if (Rab.IsToPayOffice)
                            {
                                RabI = GetMinPQNum();
                                PQueues[RabI].Enqueue(Rab);

                                if (PQueues[RabI].Count > RabMax[NumDoors + RabI])
                                    RabMax[NumDoors + RabI] = PQueues[RabI].Count;

                                RabTotal[NumDoors + RabI] += PQueues[RabI].Count;
                                RabNum[NumDoors + RabI]++;
                            }
                            else
                            {
                                RabI = GetMinTQNum();
                                TQueues[RabI].Enqueue(Rab);

                                if (TQueues[RabI].Count > RabMax[NumDoors + NumPayOffices + RabI])
                                    RabMax[NumDoors + NumPayOffices + RabI] = TQueues[RabI].Count;

                                RabTotal[NumDoors + NumPayOffices + RabI] += TQueues[RabI].Count;
                                RabNum[NumDoors + NumPayOffices + RabI]++;
                            }

                            IsDHand[i] = false;
                        }

                        if (DQueues[i].Count > 0)
                        {
                            TDHand[i] = D[i].Gen();
                            IsDHand[i] = true;
                        }
                    }

                    if (ToPayOffice != 2)
                    {
                        RabI = GetMinDQNum();
                        DQueues[RabI].Enqueue(new Request(Ttek, (ToPayOffice == 0)));
                        ToPayOffice = 2;

                        if (DQueues[RabI].Count > RabMax[RabI])
                            RabMax[RabI] = DQueues[RabI].Count;

                        RabTotal[RabI] += DQueues[RabI].Count;
                        RabNum[RabI]++;
                    }

                    TDHand[i] -= dT;
                }

                for (int i = 0; i < NumPayOffices; i++)
                {
                    if (TPHand[i] <= 0)
                    {
                        if (IsPHand[i])
                        {
                            RabI = GetMinTQNum();

                            if (Rand.NextDouble() < RepeatProb)
                            {
                                PQueues[i].Dequeue();
                                Num++;
                            }
                            else
                                TQueues[RabI].Enqueue(PQueues[i].Dequeue());

                            IsPHand[i] = false;

                            if (TQueues[RabI].Count > RabMax[NumDoors + NumPayOffices + RabI])
                                RabMax[NumDoors + NumPayOffices + RabI] = TQueues[RabI].Count;

                            RabTotal[NumDoors + NumPayOffices + RabI] += TQueues[RabI].Count;
                            RabNum[NumDoors + NumPayOffices + RabI]++;

                            if (PQueues[i].Count > RabMax[NumDoors + i])
                                RabMax[NumDoors + i] = PQueues[i].Count;

                            RabTotal[NumDoors + i] += PQueues[i].Count;
                            RabNum[NumDoors + i]++;
                        }

                        if (PQueues[i].Count > 0)
                        {
                            TPHand[i] = P[i].Gen();
                            IsPHand[i] = true;
                        }
                    }

                    TPHand[i] -= dT;
                }

                for (int i = 0; i < NumTurnstiles; i++)
                {
                    if (IsTHand[i])
                    {
                        NumWentToStation++;
                        IsTHand[i] = false;
                    }

                    if ((TTHand[i] <= 0) && TQueues[i].Count > 0)
                    {
                        TTHand[i] = T[i].Gen();
                        IsTHand[i] = true;
                        TotalTime += (Ttek - TQueues[i].Dequeue().T + TTHand[i]);

                        if (TQueues[i].Count > RabMax[NumDoors + NumPayOffices + i])
                            RabMax[NumDoors + NumPayOffices + i] = TQueues[i].Count;

                        RabTotal[NumDoors + NumPayOffices + i] += TQueues[i].Count;
                        RabNum[NumDoors + NumPayOffices + i]++;
                    }

                    TTHand[i] -= dT;
                }

                TGen -= dT;
                Ttek += dT;
            }

            MeanTime = TotalTime / Convert.ToDouble(NumWentToStation);

            for (int i = 0; i < NumDoors + NumPayOffices + NumTurnstiles; i++)
            {
                if (RabNum[i] > 0)
                    Mean[i] = RabTotal[i] / Convert.ToDouble(RabNum[i]);

                Mean[NumDoors + NumPayOffices + NumTurnstiles + i] = RabMax[i];
            }
        }

        private int GetMinDQNum()
        {
            int Count = DQueues[0].Count, Num = 0;

            for (int i = 1; i < NumDoors; i++)
                if (DQueues[i].Count < Count)
                {
                    Count = DQueues[i].Count;
                    Num = i;
                }

            return Num;
        }

        private int GetMinPQNum()
        {
            int Count = PQueues[0].Count, Num = 0;

            for (int i = 1; i < NumPayOffices; i++)
                if (PQueues[i].Count < Count)
                {
                    Count = PQueues[i].Count;
                    Num = i;
                }

            return Num;
        }

        private int GetMinTQNum()
        {
            int Count = TQueues[0].Count, Num = 0;

            for (int i = 1; i < NumTurnstiles; i++)
                if (TQueues[i].Count < Count)
                {
                    Count = TQueues[i].Count;
                    Num = i;
                }

            return Num;
        }
    }
}