using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project4_Queue
{
    public partial class Form1 : Form
    {
        Random Rand = new Random();
        
        abstract class RandomGenerator
        {
            protected Random rand;

            public RandomGenerator()
            {
                rand = new Random();
            }

            public RandomGenerator(int seed)
            {
                rand = new Random(seed);
            }

            public abstract double Next();
        }

        class UniformGenerator : RandomGenerator
        {
            public readonly double x, interval;

            public UniformGenerator(double a, double b) : base() 
            {
                this.x = a;
                this.interval = b - a;
            }

            public UniformGenerator(double a, double b, int seed) : base(seed)
            {
                this.x = a;
                this.interval = b - a;
            }

            public override double Next()
            {
                return x + interval * rand.NextDouble();
            }
        }

        class GammaGenerator : RandomGenerator
        {
            public readonly double k_int, k_frac, θ;
            double v0;

            public GammaGenerator(double k, double θ)
                : base()
            {
                this.k_int = (int)k;
                this.k_frac = k - k_int;
                this.v0 = Math.E / (Math.E + k_frac);
                this.θ = θ;
            }

            public GammaGenerator(double k, double θ, int seed)
                : base(seed)
            {
                this.k_int = (int)k;
                this.k_frac = k - k_int;
                this.v0 = Math.E / (Math.E + k_frac);
                this.θ = θ;
            }

            public override double Next()
            {
                double LnSum;
                double V1, V2;
                double Em, Nm;

                if (k_frac == 0)
                    Em = 0;
                else
                {
                    do
                    {
                        V1 = 1.0 - rand.NextDouble();
                        V2 = 1.0 - rand.NextDouble();
                        if (V1 <= v0)
                        {
                            Em = Math.Pow(V1 / v0, 1 / k_frac);
                            Nm = V2 * Math.Pow(Em, k_frac - 1);
                        }
                        else
                        {
                            Em = 1 - Math.Log((V1 - v0) / (1.0 - v0));
                            Nm = V2 * Math.Exp(-Em);
                        }
                    }
                    while (Nm > Math.Pow(Em, k_frac - 1) * Math.Exp(-Em));
                }

                LnSum = 0;
                for (int j = 0; j < k_int; j++)
                    LnSum += Math.Log(1.0 - rand.NextDouble());

                double result = θ * (Em - LnSum);
                if (result < 0)
                    result = 0;

                return result;
            }
        }

        class Request
        {
            public int Number;
            public double EntranceTime;

            public Request(int Number, double EntranceTime)
            {
                this.Number = Number;
                this.EntranceTime = EntranceTime;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UniformGenerator UnifGen = new UniformGenerator(Convert.ToDouble(tbLeft.Text), Convert.ToDouble(tbRight.Text));
            GammaGenerator GammaGen = new GammaGenerator(Convert.ToDouble(tbKParam.Text), Convert.ToDouble(tbθParam.Text));
            int RequestCount = (int)udRequestCount.Value;
            double Time = Convert.ToDouble(tbTime.Text);
            double TotalTime = 0;
            double dT = Convert.ToDouble(tbTimeStep.Text);
            int i = 0;            
            Queue<Request> Queue = new Queue<Request>();
            double GenTime = UnifGen.Next();
            double ServiceMachineTime = GammaGen.Next();
            List<double> RequesTimes = new List<double>();
            int MaxQueueLength = 0;
            int MiddleQueueLength = 0;
            int QueueCount = 0;

            do
            {
                if (GenTime <= 0)
                {
                    GenTime = UnifGen.Next();
                    Queue.Enqueue(new Request(i, TotalTime));

                    if (Queue.Count > MaxQueueLength)
                        MaxQueueLength = Queue.Count;

                    MiddleQueueLength += Queue.Count;
                    QueueCount++;
                }

                if ((ServiceMachineTime <= 0) && (Queue.Count > 0))
                {
                    ServiceMachineTime = GammaGen.Next();

                    Request Req = Queue.Dequeue();

                    i++;

                    if (cbInverseLink.Checked)
                    {
                        Queue.Enqueue(new Request(i, TotalTime));

                        if (Queue.Count > MaxQueueLength)
                            MaxQueueLength = Queue.Count;

                        MiddleQueueLength += Queue.Count;
                        QueueCount++;
                    }

                    RequesTimes.Add(TotalTime - Req.EntranceTime);
                }

                TotalTime += dT;
                GenTime -= dT;
                ServiceMachineTime -= dT;
            }
            while (rbCount.Checked ? i < RequestCount : TotalTime < Time);



            double TimeSum = 0;
            foreach (double T in RequesTimes)
                TimeSum += T;
            double AverageTime = TimeSum / RequesTimes.Count;

            tbMaxQueueLength.Text = MaxQueueLength.ToString();
            tbAverageStayTime.Text = AverageTime.ToString("00.0000");
            //tbAverageQueueLength.Text = (MiddleQueueLength / QueueCount).ToString("00.0000");

            if (rbCount.Checked)
                textBox1.Text = TotalTime.ToString("0.0000");
            else
                textBox1.Text = i.ToString();
        }

        private void rbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCount.Checked)
                label3.Text = "Total time";
            else
                label3.Text = "Request count";
        }
    }
}
