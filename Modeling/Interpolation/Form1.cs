using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using alglib;

namespace Interpolation
{
    public partial class Form1 : Form
    {
        double[] xarray, yarray;
        int n;
        ratint.barycentricinterpolant p;
        Graphics G;
        Bitmap B;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xarray = new double[100];
            yarray = new double[100];
            n = 0;
            p = new ratint.barycentricinterpolant();
            B = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = B;
            G = Graphics.FromImage(B);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            G.FillRectangle(new SolidBrush(Color.White), 0, 0, B.Width, B.Height);
        }

       

        private void BuildGraphic()
        {
            int y1, y2;
            Pen pen = new Pen(Color.Red);
            G.FillRectangle(new SolidBrush(Color.White), 0, 0, B.Width, B.Height);
            y1 = (int)Math.Round(ratint.barycentriccalc(ref p, 0));
            for (int i = 0; i < B.Width - 1; i++)
            {
                y2 = (int)Math.Round(ratint.barycentriccalc(ref p, i + 1));
                G.DrawLine(pen, i, y1, i + 1, y2);
                y1 = y2;  
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            xarray[n] = (double)e.X;
            yarray[n] = (double)e.Y;
            n++;

            polint.polynomialbuild(ref xarray, ref yarray, n, ref p);

            BuildGraphic();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
