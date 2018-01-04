using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project5
{
    public partial class Form1 : Form
    {
        Random Rand;
        Bitmap B;
        int CurrentClientCount, 
            LostClientCount, 
            //PC1QueueMaxLength, // макс длина очереди запросов к ПК1
            //PC2QueueMaxLength, // макс длина очереди запросов к ПК2
            PC1QueueLength,    // текущая длина очереди запросов к ПК1
            PC2QueueLength,    // текущая длина очереди запросов к ПК1
            //PC1QueueDeviatedRequestsQty, // кол-во потерянных запросов к ПК1
            //PC2QueueDeviatedRequestsQty, // кол-во потерянных запросов к ПК1
            ProcessedRequestCount1, // кол-во обработанных запросов 1-м оператором
            ProcessedRequestCount2, // кол-во обработанных запросов 2-м оператором
            ProcessedRequestCount3, // кол-во обработанных запросов 3-м оператором
            PCProcessedRequestCount1, // кол-во обработанных запросов 1-м компьютером
            PCProcessedRequestCount2, // кол-во обработанных запросов 2-м компьютером
            Number,
            TotalRequestsQty; //Всего клиентов
        long Free;
        double Min;

        // ReqGenerated, Oper1Obrab, Oper2Obrab, Oper3Obrab, PC1Obrab, PC2Obrab
        //      0            1           2           3           4         5

        double[] SBS = new double[6]; // Список будущих событий

        //генерирует число от 0 до 1
        private double GetRandomOnInterval0_1()
        {
            double n = Rand.NextDouble();
            return n;
        }

        private double GetRandom(double Center, double HalfInterval)
        {
            return Center + (GetRandomOnInterval0_1() - 0.5) * 2 * HalfInterval;
        }

        //генерирует интервал времени через который приходят клиенты в центр
        private double GetGenTime()
        {
            return GetRandom(10, 2);
        }

        //генерирует время обслуживания клиента 1-м оператором
        private double GetOper1Time()
        {
            return GetRandom(20, 5);
        }

        //генерирует время обслуживания клиента 2-м оператором
        private double GetOper2Time()
        {
            return GetRandom(40, 10);
        }

        //генерирует время обслуживания клиента 3-м оператором
        private double GetOper3Time()
        {
            return GetRandom(40, 20);
        }

        //Время обработки запроса на 1-м ПК
        private double GetPC1Time()
        {
            return 15;
        }

        //Время обработки запроса на 2-м ПК
        private double GetPC2Time()
        {
            return 30;
        }

        //Генерация поступления клиента на обслуживание к одному из 3х операторов
        private void RequestGenerated()
        {
            int i;
            CurrentClientCount++; //увеличиваем текущее кол-во клиентов

            for (i = 1; i <= 3; i++) // просматриваем состояния трех операторов
            {
                if (SBS[i] == Free) //оператор свободен
                {
                    if (i == 1) SBS[i] = SBS[0] + GetOper1Time(); //время генерирования + время обслуживания клиента 1-м оператором
                    if (i == 2) SBS[i] = SBS[0] + GetOper2Time(); //время генерирования + время обслуживания клиента 2-м оператором
                    if (i == 3) SBS[i] = SBS[0] + GetOper3Time(); //время генерирования + время обслуживания клиента 3-м оператором
                    break;
                }
            }

            if (i > 3)
            {
                LostClientCount++; //увеличиваем кол-во потеряных клиентов
                label40.Text = LostClientCount.ToString();
                label40.Refresh();
            }
            if (CurrentClientCount == TotalRequestsQty)
                SBS[0] = SBS[0] + Free; //если текущее кол-во клиентов = общему кол-ву
            else
                SBS[0] = SBS[0] + GetGenTime(); //время генерирования + интервал времени через который приходят клиенты в центр
        }

        //---------------------------------------------------------------------------
        //Постановка запроса в очередь на обработку к  ПК1
        private void PutRequestInPC1Queue()
        {
            //if (PC1QueueLength == PC1QueueMaxLength)// если текущая длина очереди запросов к ПК1 равна максимальной
            // PC1QueueDeviatedRequestsQty++;// увеличиваем кол-во потерянных запросов к ПК1
            //else
            PC1QueueLength++; //увеличиваем текущую длину очереди запросов к ПК1
            label28.Text = Convert.ToString(PC1QueueLength);
            label28.Refresh();
        }

        //---------------------------------------------------------------------------
        //Постановка запроса в очередь на обработку к  ПК2
        private void PutRequestInPC2Queue()
        {
            //if (PC2QueueLength == PC2QueueMaxLength)// если текущая длина очереди запросов к ПК2 равна максимальной
            //PC2QueueDeviatedRequestsQty++;// увеличиваем кол-во потерянных запросов к ПК2
            //else
            PC2QueueLength++; //увеличиваем текущую длину очереди запросов к ПК2
            label29.Text = Convert.ToString(PC2QueueLength);
            label29.Refresh();
        }

        //---------------------------------------------------------------------------
        //Передача запроса с 1-го оператора на ПК1
        private void Oper1TreatedRequest()
        {
            ProcessedRequestCount1++; //увеличиваем кол-во обработанных запросов 1-м оператором
            //если ПК не занят то посылаем запрос к нему на обработку
            if (SBS[4] == Free)
                SBS[4] = SBS[1] + GetPC1Time();
            else //если занят то ставим запрос к нему в очередь
                PutRequestInPC1Queue();

            //освобождаем 1-го оператора
            SBS[1] = Free;
        }

        //---------------------------------------------------------------------------
        //Передача запроса с 2-го оператора на ПК1
        private void Oper2TreatedRequest()
        {
            ProcessedRequestCount2++; //увеличиваем кол-во обработанных запросов 2-м оператором
            //если ПК не занят то посылаем запрос к нему на обработку
            if (SBS[4] == Free)
                SBS[4] = SBS[2] + GetPC1Time();
            else //если занят то ставим запрос к нему в очередь
                PutRequestInPC1Queue();

            //освобождаем 2-го оператора
            SBS[2] = Free;
        }

        //---------------------------------------------------------------------------
        //Передача запроса с 3-го оператора на ПК2
        private void Oper3TreatedRequest()
        {
            ProcessedRequestCount3++;  //увеличиваем кол-во обработанных запросов 3-м оператором
            //если ПК не занят то посылаем запрос к нему на обработку
            if (SBS[5] == Free)
                SBS[5] = SBS[3] + GetPC2Time();
            else //если занят то ставим запрос к нему в очередь
                PutRequestInPC2Queue();
            //освобождаем 3-го оператора
            SBS[3] = Free;
        }

        //---------------------------------------------------------------------------
        //Конец обработки запроса в ПК1
        private void PC1TreatedRequest()
        {
            PCProcessedRequestCount1++; //увеличиваем кол-во обработанных запросов 1-м компьютером
            label38.Text = (PCProcessedRequestCount1 + PCProcessedRequestCount2).ToString();
            label38.Refresh();

            //Если сообщений в очереди к 1-му ПК нет то освобождаем его
            if (PC1QueueLength == 0)
                SBS[4] = Free;
            else //В противном случае берем следующий запрос из очереди
            {
                PC1QueueLength--;
                label28.Text = Convert.ToString(PC1QueueLength);
                label28.Refresh();
                SBS[4] = SBS[4] + GetPC1Time();
            }
        }
        //---------------------------------------------------------------------------
        //Конец обработки запроса в ПК2
        private void PC2TreatedRequest()
        {
            PCProcessedRequestCount2++; //увеличиваем кол-во обработанных запросов 2-м компьютером
            label38.Text = (PCProcessedRequestCount1 + PCProcessedRequestCount2).ToString();
            label38.Refresh();

            //Если сообщений в очереди ко 2-му ПК нет то освобождаем его
            if (PC2QueueLength == 0)
                SBS[5] = Free;
            else //В противном случае берем следующий запрос из очереди
            {
                PC2QueueLength--;
                label29.Text = Convert.ToString(PC2QueueLength);
                label29.Refresh();
                SBS[5] = SBS[5] + GetPC2Time();
            }
        }

        public Form1()
        {          
            InitializeComponent();

            Rand = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PC1QueueMaxLength = (int)numericUpDown2.Value;
            //PC2QueueMaxLength = (int)numericUpDown3.Value;
            label38.Text = "0"; label39.Text = "0"; label40.Text = "0";
            label38.Refresh(); label39.Refresh(); label40.Refresh(); pictureBox1.Refresh();
            TotalRequestsQty = (int)numericUpDown1.Value;
            Free = int.MaxValue;

            SBS[0] = 0;
            CurrentClientCount = 0;

            for (int h = 1; h <= 5; h++)
                SBS[h] = Free;

            LostClientCount = 0;
            //PC1QueueDeviatedRequestsQty = 0;
            //PC2QueueDeviatedRequestsQty = 0;
            PC1QueueLength = 0;
            PC2QueueLength = 0;
            ProcessedRequestCount1 = 0;
            ProcessedRequestCount2 = 0;
            ProcessedRequestCount3 = 0;
            PCProcessedRequestCount1 = 0;
            PCProcessedRequestCount2 = 0;

            //Моделирование системы с протяжкой времени
            while (true)
            {
                //if (radioButton2.Checked) for (int y = 0; y <= 100000; y++)

                if (SBS[1] == Free) label25.Text = "0";
                    else label25.Text = "1";
                label25.Refresh();

                if (SBS[2] == Free) label26.Text = "0";
                else label26.Text = "1";
                label26.Refresh();

                if (SBS[3] == Free) label27.Text = "0";
                else label27.Text = "1";
                label27.Refresh();

                if (SBS[4] == Free) label30.Text = "0";
                else label30.Text = "1";
                label30.Refresh();

                if (SBS[5] == Free) label31.Text = "0";
                else label31.Text = "1";
                label31.Refresh();


                Min = SBS[0];
                Number = 0;

                for (int f = 1; f <= 5; f++)
                {
                    if (SBS[f] < Min)
                    {
                        Min = SBS[f];
                        Number = f;
                    }
                }

                if (Min == Free) break;
                switch (Number)
                {
                    case 0:
                        RequestGenerated();  //Генерация поступления клиента на обслуживание к одному из 3х операторов
                        label39.Text = CurrentClientCount.ToString();
                        label39.Refresh();
                        break;
                    case 1:
                        Oper1TreatedRequest(); //Передача запроса с 1-го оператора на ПК1
                        break;
                    case 2:
                        Oper2TreatedRequest(); //Передача запроса с 2-го оператора на ПК1
                        break;
                    case 3:
                        Oper3TreatedRequest(); //Передача запроса с 3-го оператора на ПК2
                        break;
                    case 4:
                        PC1TreatedRequest(); //Конец обработки запроса в ПК1
                        break;
                    case 5:
                        PC2TreatedRequest(); //Конец обработки запроса в ПК2
                        break;
                }

                Application.DoEvents();
                System.Threading.Thread.Sleep(trackBar1.Value * 3);
                
            }

            label4.Text = Convert.ToString(LostClientCount); // Кол-во потерянных клиентов

            //  (Кол-во потерянных клиентов + кол-во потерянных запросов к ПК1 + кол-во потерянных запросов к ПК2) * 100 / Текущее кол-во клиентов
            //label5.Text = ( (AllOpersDeviatedRequestsQty + PC1QueueDeviatedRequestsQty + PC2QueueDeviatedRequestsQty) * 100.0 / Тек_Client_Count).ToString("0.00");

            //  Кол-во потерянных клиентов * 100 / Текущее кол-во клиентов
            label5.Text = ((LostClientCount) * 100.0 / CurrentClientCount).ToString("0.00");

            label11.Text = Convert.ToString(ProcessedRequestCount1); // кол-во обработанных запросов 1-м оператором
            label12.Text = Convert.ToString(ProcessedRequestCount2); // кол-во обработанных запросов 2-м оператором
            label13.Text = Convert.ToString(ProcessedRequestCount3); // кол-во обработанных запросов 3-м оператором
            label23.Text = Convert.ToString(PCProcessedRequestCount1); // кол-во обработанных запросов 1-м компьютером
            label24.Text = Convert.ToString(PCProcessedRequestCount2); // кол-во обработанных запросов 2-м компьютером

            //Потеряно заявок = кол-во обработанных запросов 1-м оператором + кол-во обработанных запросов 2-м оператором - кол-во обработанных запросов 1-м компьютером
            //label16.Text = Convert.ToString(Oper1_Obrab_Count + Oper2_Obrab_Count - PC1_Obrab_Count);

            //Потеряно заявок = кол-во обработанных запросов 3-м оператором - кол-во обработанных запросов 2-м компьютером
            //label17.Text = Convert.ToString(Oper3_Obrab_Count - PC2_Obrab_Count);
        }
    }
}
