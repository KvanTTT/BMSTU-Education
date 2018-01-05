using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TravellingSalesmanProblem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics G;
        Bitmap B;

        Painter P;

        List<City> C;

        private void Form1_Load(object sender, EventArgs e)
        {
            B = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = B;
            G = Graphics.FromImage(B);

            C = new List<City>();

            P = new Painter(G, B, pictureBox1);

            P.Clear();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            P.Clear();

            C.Add(new City(e.X, e.Y));

            for (int i = 0; i < C.Count; i++)
                P.PaintCity(C[i].GetX(), C[i].GetY());

            P.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int Num = Convert.ToInt32(textBox1.Text), MaxNumRepeats = Convert.ToInt32(textBox3.Text), MaxNumPopulations = Convert.ToInt32(textBox4.Text);
                double PMut = Convert.ToDouble(textBox2.Text);

                string S;

                Solver Dec = new Solver(Num, PMut, MaxNumRepeats, MaxNumPopulations, C);

               
                if (Dec.Decide(P))
                    S = "По количеству повторений!";
                else
                    S = "По количеству популяций!";

                MessageBox.Show("Решение найдено! " + S, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            C.Clear();
            P.Clear();
            P.Refresh();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != (char)8))
                e.KeyChar = (char)0;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != (char)8) && (e.KeyChar != ','))
                e.KeyChar = (char)0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int CityCount = (int)udCityCount.Value;

            P.Clear();
            C.Clear();

            Random Rand = new Random();
            for (int i = 0; i < CityCount; i++)
                C.Add(new City(Rand.Next(pictureBox1.ClientSize.Width), Rand.Next(pictureBox1.ClientSize.Height)));

            for (int i = 0; i < C.Count; i++)
                P.PaintCity(C[i].GetX(), C[i].GetY());

            P.Refresh();
        }
    }
}
