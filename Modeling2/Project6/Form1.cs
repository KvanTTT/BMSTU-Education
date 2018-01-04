using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _7sem_model_6
{
    public partial class Form1 : Form
    {
        QueueSystem QSystem;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                QSystem = new QueueSystem();

                Generator G = new Generator(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                List<Door> D = new List<Door>();
                List<PayOffice> P = new List<PayOffice>();
                List<Turnstile> T = new List<Turnstile>();
                List<double> Mean = new List<double>();

                int Num = Convert.ToInt32(textBox11.Text);
                double IsToPayOffice = 2 * Convert.ToDouble(textBox12.Text), TotalTime = 0, MeanTime;

                for (int i = 0; i < (QueueSystem.NumDoors + QueueSystem.NumPayOffices + QueueSystem.NumTurnstiles) << 1; i++)
                    Mean.Add(0);

                D.Add(new Door(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text)));
                D.Add(new Door(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text)));

                P.Add(new PayOffice(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text)));
                P.Add(new PayOffice(Convert.ToInt32(textBox7.Text), Convert.ToInt32(textBox8.Text)));

                T.Add(new Turnstile(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)));
                T.Add(new Turnstile(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)));
                T.Add(new Turnstile(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)));
                T.Add(new Turnstile(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)));
                T.Add(new Turnstile(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)));
                T.Add(new Turnstile(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text)));

                QSystem.Processing(G, D, P, T, Num, IsToPayOffice, ref TotalTime, out MeanTime, ref Mean, Convert.ToDouble(textBox13.Text));

                label8.Text = "Общее время моделирования: " + TotalTime.ToString("F01");
                label9.Text = "Среднее время отъезда: " + MeanTime.ToString("F01");
                label20.Text = "Средняя количество людей в автобусе: " + Mean[0].ToString("F01") + " " + Mean[1].ToString("F01");
                label21.Text = "Средняя длина очередей перед консультантами: " + Mean[2].ToString("F01") + " " + Mean[3].ToString("F01");
                label22.Text = "Средняя длина очередей перед кассами: " + 
                                Mean[4].ToString("F01") + " " + Mean[5].ToString("F01") + " " + Mean[6].ToString("F01") + " " +
                                Mean[7].ToString("F01") + " " + Mean[8].ToString("F01") + " " + Mean[9].ToString("F01");
                label23.Text = "Макс. количество людей в автобусе: " + Mean[10].ToString("F00") + " " + Mean[11].ToString("F00");
                label24.Text = "Макс. длина очередей перед консультантами: " + Mean[12].ToString("F00") + " " + Mean[13].ToString("F00");
                label25.Text = "Макс. длина очередей перед кассами: " + 
                                Mean[14].ToString("F00") + " " + Mean[15].ToString("F00") + " " + Mean[16].ToString("F00") + " " +
                                Mean[17].ToString("F00") + " " + Mean[18].ToString("F00") + " " + Mean[19].ToString("F00");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
