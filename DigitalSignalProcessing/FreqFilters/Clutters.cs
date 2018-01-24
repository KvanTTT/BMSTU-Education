using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreqFilters
{
    class Clutters
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        List<double> gaussnoise = new List<double>();
        List<double> impulsenoise = new List<double>();

        double min, max;
        double step = 0.01;

        public Clutters(double min, double max, int impcount)
        {
            this.min = min;
            this.max = max;

            GenerateGaussNoise();
            GenerateImpulseNoise(impcount);
        }

        public double Impulse(double x)
        {
            return impulsenoise[(int)Math.Truncate((x - min) / step)];
        }

        public double Gauss(double x)
        {
            return gaussnoise[(int)Math.Truncate((x - min) / step)];
        }

        public void GenerateImpulseNoise(int impulsecount)
        {
            List<int> imp_i = new List<int>();
            int index;
            // Сгенерируем impulsecount индексов для импульсов
            for (int i = 0; i < impulsecount; i++)
            {
                do
                {
                    index = rnd.Next((int)((max - min) / step));
                } while (imp_i.Contains(index));
                imp_i.Add(index);
            }
            
            // Создадим импульсный шум
            for (int i = 0; i < (max - min) / step; i++)
                if (imp_i.Contains(i))
                    impulsenoise.Add(rnd.NextDouble());
                else
                    impulsenoise.Add(0);
        }

        public void GenerateGaussNoise()
        {
            double sum;
            for (int i = 0; i < (max - min) / step; i++)
            {
                sum = 0;
                for (int j = 0; j < 12; j++)
                    sum += (2 * rnd.NextDouble() - 1)/ 60;

                gaussnoise.Add(sum);
            }            
        }
    }
}
