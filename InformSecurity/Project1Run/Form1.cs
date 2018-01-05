using AdvForms;
using ComputerStat;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project1Run
{
    public partial class Form1 : Form
    {
        int dx = 3;
        int dy = 3;
        float fdx = 0.05f;
        float FontSize;

        public Form1()
        {
            FontSize = 22;
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left += dx;
            label1.Top += dy;
            if (label1.Left < 0)
            {
                label1.Left = 0;
                dx *= -1;
            }
            else
                if (label1.Left + label1.Width >= ClientSize.Width)
                {
                    label1.Left = ClientSize.Width - label1.Width;
                    dx *= -1;
                }
            if (label1.Top < 0)
            {
                label1.Top = 0;
                dy *= -1;
            }
            else
                if (label1.Top + label1.Height >= ClientSize.Height)
                {
                    label1.Top = ClientSize.Height - label1.Height;
                    dy *= -1;
                }
            if (FontSize >= 25)
                fdx = -0.2f;
            if (FontSize <= 20)
                fdx = 0.2f;
            FontSize += fdx;
            Font F = new Font(label1.Font.FontFamily, FontSize);
            label1.Font = F;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("licence.dat"))
            {
                MessageForm Form = new MessageForm();
                Form.Show();
                Hardware MachineID = new Hardware();
                Form.Close();

                StreamReader stream = new StreamReader("licence.dat");
                if (MachineID.HashCode != stream.ReadLine())
                {
                    MessageBox.Show("Invalid licence file or computer!", "", MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Licence file not found", "", MessageBoxButtons.OK);
                Application.Exit();
            }
        }
    }
}
