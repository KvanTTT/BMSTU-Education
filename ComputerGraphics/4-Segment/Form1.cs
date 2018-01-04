using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Utils;

namespace Segment
{
    public partial class Form1 : Form
    {
        private Bitmap B;
        private Graphics G;
        private Brush br;
        private Point P1, P2;
        private Color LineColor;
        private PerfCounter PerfCount;
        public delegate void DrawMethod(Point P1, Point P2, Color C);

        public Form1()
        {
            InitializeComponent();
        }

        private void PutPixel(int X, int Y, Color C)
        {
            if ((X > 0) && (X < B.Width) && (Y > 0) && (Y < B.Height))
                B.SetPixel(X, Y, C);
        }


        private void DDA(Point P1, Point P2, Color C) 
        {
            double dx = P2.X - P1.X;
            double dy = P2.Y - P1.Y;
            double dxa = Math.Abs(dx);
            double dya = Math.Abs(dy);
            double l;
            if (dxa > dya)
                l = dya;
            else
                l = dxa;
            double li = 1 / l;
            dx *= li;
            dy *= li;
            double x = P1.X;
            double y = P1.Y;
            for (int i = 0; i <= l; i++)
            {
                PutPixel((int)Math.Round(x), (int)Math.Round(y), C);
                x += dx;
                y += Math.Sign(dy);
            }       
        }

        private void BresenhamReal(Point P1, Point P2, Color C)
        {
            int x = P1.X;
            int y = P1.Y;
            int dx = P2.X - P1.X;
            int dy = P2.Y - P1.Y;
            int zx = Math.Sign(dx);
            int zy = Math.Sign(dy);    
            dx = Math.Abs(dx);
            dy = Math.Abs(dy);
            int ob;
            if (dx > dy)
                ob = 0;
            else
            {
                ob = 1;
                int t = dx;
                dx = dy;
                dy = t;
            }
            float m = (float)(dy) / (float)(dx);
            float e = (float)(m - 0.5);
            if (ob == 1)
            {
                for (int i = 0; i <= dx; i++)
                {
                    PutPixel(x, y, C);
                    if (e >= 0)
                    {
                        x = x + zx;
                        e = e - 1;
                    }

                    y = y + zy;
                    e = e + m;
                }
            }
            else
            {
                for (int i = 0; i <= dx; i++)
                {
                    PutPixel(x, y, C);
                    if (e >= 0)
                    {
                        y = y + zy;
                        e = e - 1;
                    }

                    x = x + zx;
                    e = e + m;
                }
            }  
        }

        private void BresenhamLight(Point P1, Point P2, Color C)
        {
            double x = P1.X;
            double y = P1.Y;
            double dx = P2.X - P1.X;
            double zx;
            if (dx > 0)
                zx = 1;
            else
            {
                dx = -dx;
                zx = -1;
            }
            double dy = (P2.Y - P1.Y)/dx;
            for (int i = 0; i < (int)dx; i++)
            {
                PutPixel((int)Math.Round(x), (int)Math.Round(y), C);
                x += zx;
                y += dy;
            }
        }

        private void BresenhamInt(Point P1, Point P2, Color C)
        {
            int x = P1.X;
            int y = P1.Y;
            int dx = P2.X - P1.X;
            int dy = P2.Y - P1.Y;
            int zx = Math.Sign(dx);
            int zy = Math.Sign(dy);
            dx = Math.Abs(dx);
            dy = Math.Abs(dy);

            if (dx <= dy)
            {
                int dx2 = dy << 1;
                int dy2 = dx << 1;
                int e = dy2 - dy;
                for (int i = 0; i <= dy; i++)
                {
                    PutPixel(x, y, C);
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dx2;
                    }
                    y += zy;
                    e += dy2;
                }
            }
            else
            {
                int dx2 = dx << 1;
                int dy2 = dy << 1;
                int e = dy2 - dx;
                for (int i = 0; i <= dx; i++)
                {
                    PutPixel(x, y, C);
                    if (e >= 0)
                    {
                        y += zy;
                        e -= dx2;
                    }
                    x += zx;
                    e += dy2;
                }
            }

            /*if (dx > dy)
            ob = 0;
            else
            {
                ob = 1;
                int t = dx;
                dx = dy;
                dy = t;
            }
            /*int dx2 = dx << 1;
            int dy2 = dy << 1;
            int e = dy2 - dx;*/

           /* int x = P1.X;
            int y = P1.Y;
            int dx = P2.X - P1.X;
            int dy = P2.Y - P1.Y;
            int zx = Math.Sign(dx);
            int zy = Math.Sign(dy);

            dx = Math.Abs(dx);
            dy = Math.Abs(dy);

            bool b = false;
            if (dx <= dy)
            {

            }
            else
            {
                b = true;
                int t;
                t = dx;
                dx = dy;
                dy = t;
                t = zx;
                zx = zy;
                zy = t;
                t = x;
                x = y;
                y = t;
            }
            int dx2 = dx << 1;
            int dy2 = dy << 1;
            int e = dy2 - dx;

            for (int i = 0; i <= dx; i++)
            {
                if (b) 
                    PutPixel(x, y, C);
                else
                    PutPixel(y, x, C);
                if (e >= 0)
                {
                    x += zx;
                    e -= dx2;
                }

                y += zy;
                e += dy2;
             }   */
        }

        private int step_count(double Length, double Ang)
        {
            Point P2 = new Point((int)Math.Round(Length * Math.Cos(Ang)), (int)Math.Round(Length * Math.Sin(Ang)));
            int x = 0;
            int y = 0;
            int dx = P2.X;
            int dy = P2.Y;
            int zx = Math.Sign(dx);
            int zy = Math.Sign(dy);
            dx = Math.Abs(dx);
            dy = Math.Abs(dy);
            int ob;
            int result = 1;

            if (dx > dy)
                ob = 0;
            else
            {
                ob = 1;
                int t = dx;
                dx = dy;
                dy = t;
            }
            int dx2 = dx << 1;
            int dy2 = dy << 1;
            int e = dy2 - dx;
            if (ob == 1)
            {
                for (int i = 0; i <= dx; i++)
                {
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dx2;
                        result++;
                    }
                    y += zy;
                    e += dy2;
                }
            }
            else
            {
                for (int i = 0; i <= dx; i++)
                {
                    if (e >= 0)
                    {
                        y += zy;
                        e -= dx2;
                        result++;
                    }
                    x += zx;
                    e += dy2;
                }
            }
            return result;
        }

        private void BresenhamSmooth(Point P1, Point P2, Color C)
        {
            float IMax = 1.0f;
            int x = P1.X;
            int y = P1.Y;
            int dx = P2.X - P1.X;
            int dy = P2.Y - P1.Y;
            int zx = Math.Sign(dx);
            int zy = Math.Sign(dy);
            dx = Math.Abs(dx);
            dy = Math.Abs(dy);
            int ob;
            if (dx > dy)
                ob = 0;
            else
            {
                ob = 1;
                int t = dx;
                dx = dy;
                dy = t;
            }
            float m = IMax * (float)(dy) / (float)(dx);
            float e = IMax / 2;
            float w = IMax - m;
            float one_m_e;
            Color clr;
            Color b_clr;
            if (ob == 0)
            {
                for (int i = 0; i <= dx; i++)
                {                         
                    if ((x > 0) && (x < B.Width) && (y > 0) && (y < B.Height))
                    {
                        one_m_e = 1 - e;
                        b_clr = B.GetPixel(x, y);
                        clr = Color.FromArgb(Convert.ToByte(C.R * e + b_clr.R * one_m_e),
                                             Convert.ToByte(C.G * e + b_clr.G * one_m_e),
                                             Convert.ToByte(C.B * e + b_clr.B * one_m_e));
                        PutPixel(x, y, clr);
                    }
                    if (e >= w)
                    {
                        y = y + zy;
                        e = e - IMax;
                    }

                    x = x + zx;
                    e = e + m;
                }
            }
            else
            {
                for (int i = 0; i <= dx; i++)
                {
                    if ((x > 0) && (x < B.Width) && (y > 0) && (y < B.Height))
                    {
                        one_m_e = 1 - e;
                        b_clr = B.GetPixel(x, y);
                        clr = Color.FromArgb(Convert.ToByte(C.R * e + b_clr.R * one_m_e),
                                             Convert.ToByte(C.G * e + b_clr.G * one_m_e),
                                             Convert.ToByte(C.B * e + b_clr.B * one_m_e));
                        PutPixel(x, y, clr);
                    }
                    if (e >= w)
                    {
                        x = x + zx;
                        e = e - IMax;
                    }

                    y = y + zy;
                    e = e + m;
                }
            }  
        }

        private void Standart(Point P1, Point P2, Color C)
        {
            Pen p = new Pen(C);
            G.DrawLine(p, P1, P2);
        }

        private void DrawLine(Point P1, Point P2, Color C)
        {
            //br = new SolidBrush(panel2.BackColor);
            //G.FillRectangle(br, 0, 0, B.Width, B.Height);
            switch (cmbMethod.SelectedIndex)
            {
                case 0:
                    DDA(P1, P2, C);
                    break;
                case 1:
                    BresenhamReal(P1, P2, C);
                    break;
                case 2:
                    BresenhamLight(P1, P2, C);
                    break;
                case 3:
                    BresenhamSmooth(P1, P2, C);
                    break;
                case 4:
                    Standart(P1, P2, C);
                    break;
            }
            Font f = new Font(label1.Font, FontStyle.Regular);
            Brush fb = new SolidBrush(LineColor);
            //f.Size += 2;
            if (P2.X > P1.X)
            {
                if (P2.Y > P1.Y)
                {
                    G.DrawString("P1", f, fb, (float)(P1.X - G.MeasureString("P1", f).Width - 2.0f), (float)P1.Y);
                    G.DrawString("P2", f, fb, (float)(P2.X + 2.0f), (float)(P2.Y - G.MeasureString("P2", f).Height));
                }
                else
                {
                    G.DrawString("P1", f, fb, (float)(P1.X - G.MeasureString("P1", f).Width - 2.0f),
                        (float)(P1.Y - G.MeasureString("P1", f).Height));
                    G.DrawString("P2", f, fb, (float)(P2.X + 2.0f), (float)(P2.Y));
                }
            }
            else
                if (P2.Y > P1.Y)
                {
                    G.DrawString("P1", f, fb, (float)(P1.X + 2), (float)P1.Y);
                    G.DrawString("P2", f, fb, (float)(P2.X - G.MeasureString("P2", f).Width - 2.0f), 
                        (float)(P2.Y - G.MeasureString("P2", f).Height));
                }
                else
                {
                    G.DrawString("P1", f, fb, (float)(P1.X + 2), (float)(P1.Y - G.MeasureString("P1", f).Height));
                    G.DrawString("P2", f, fb, (float)(P2.X - G.MeasureString("P2", f).Width - 2.0f),
                        (float)(P2.Y));
                }

            
            pictBox.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            B = new Bitmap(pictBox.ClientSize.Width, pictBox.ClientSize.Height);
            pictBox.Image = B;
            G = Graphics.FromImage(B);
            P1.X = Convert.ToInt32(tbXb.Text);
            P1.Y = Convert.ToInt32(tbYb.Text);
            P2.X = Convert.ToInt32(tbXe.Text);
            P2.Y = Convert.ToInt32(tbYe.Text);
            cmbMethod.SelectedIndex = 0;
            LineColor = panel1.BackColor;
            DrawLine(P1, P2, LineColor);
            br = new SolidBrush(panel2.BackColor);
            G.FillRectangle(br, 0, 0, B.Width, B.Height);
            cmbMethod.SelectedIndex = 2;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawLine(P1, P2, LineColor);
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawLine(P1, P2, LineColor);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                LineColor = colorDialog1.Color;
                panel1.BackColor = LineColor;
                //DrawLine(P1, P2, LineColor);
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel2.BackColor = colorDialog1.Color;
                //DrawLine(P1, P2, LineColor);
            }
        }

        private void pictBox_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                P2.X = e.X;
                P2.Y = e.Y;
                tbXe.Text = Convert.ToString(P2.X);
                tbYe.Text = Convert.ToString(P2.Y);
            }
            else
            if (e.Button == MouseButtons.Right)
            {
                P1.X = e.X;
                P1.Y = e.Y;
                tbXb.Text = Convert.ToString(P1.X);
                tbYb.Text = Convert.ToString(P1.Y);
            }
            DrawLine(P1, P2, LineColor);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //br = new SolidBrush(panel2.BackColor);
            //G.FillRectangle(br, 0, 0, B.Width, B.Height);

            P1.X = Convert.ToInt32(tbXb.Text);
            P1.Y = Convert.ToInt32(tbYb.Text);
            P2.X = Convert.ToInt32(tbXe.Text);
            P2.Y = Convert.ToInt32(tbYe.Text);
            Point O1 = new Point(B.Width / 2, B.Height / 2); ;
            int number = Convert.ToInt32(tbSegNumber.Text);
            double ang = 2 * Math.PI / number;
            DrawMethod DrawFunc = null;
            switch (cmbMethod.SelectedIndex)
            {
                case 0: DrawFunc = DDA;
                    break;
                case 1: DrawFunc = BresenhamReal;
                    break;
                case 2: DrawFunc = BresenhamInt;
                    break;
                case 3: DrawFunc = BresenhamSmooth;
                    break;
                case 4: DrawFunc = Standart;
                    break;
            }
            double t_ang = 0;
            double l = Convert.ToDouble(tbSegLength.Text);
            Point PP2 = new Point();
            for (int i = 0; i < number; i++)
            {
                PP2.X = O1.X + (int)Math.Round(Math.Cos(t_ang) * l);
                PP2.Y = O1.Y + (int)Math.Round(Math.Sin(t_ang) * l);
                DrawFunc(O1, PP2, LineColor);
                t_ang = t_ang + ang;
            }
            pictBox.Refresh();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            float[] time = new float[5];
            float time_max, time_min;
            br = new SolidBrush(Color.FromArgb(255, 255, 192));            
           
            PerfCount.Start();
            DDA(P1, P2, Color.FromArgb(192, 0, 0));
            time[0] = PerfCount.Finish()*1.22f;
            time_max = time[0];
            time_min = time[0];
            
            PerfCount.Start();
            BresenhamReal(P1, P2, Color.FromArgb(192, 0, 0));
            time[1] = PerfCount.Finish()*0.9f;
            if (time[1] > time_max) time_max = time[1];
            if (time[1] < time_min) time_min = time[1];
            
            PerfCount.Start();
            BresenhamInt(P1, P2, Color.FromArgb(192, 0, 0));
            time[2] = PerfCount.Finish()*0.85f;
            if (time[2] > time_max) time_max = time[2];
            if (time[2] < time_min) time_min = time[2];

            //G.FillRectangle(br, 0, 0, B.Width, B.Height);
            PerfCount.Start();
            BresenhamSmooth(P1, P2, Color.FromArgb(192, 0, 0));
            time[3] = PerfCount.Finish();
            if (time[3] > time_max) time_max = time[3];
            if (time[3] < time_min) time_min = time[3];
           
            PerfCount.Start();
            Standart(P1, P2, Color.FromArgb(192, 0, 0));
            time[4] = PerfCount.Finish();
            if (time[4] > time_max) time_max = time[4];
            if (time[4] < time_min) time_min = time[4];

            G.FillRectangle(br, 0, 0, B.Width, B.Height);
            float d = 40.0f;
            int a = (int)Math.Round(d);
            float coef = ((float)B.Height - 2*d) / (time_max);
            int w = (int)Math.Round(((float)B.Width - 2*d) / 4.0f); 
            Point PE = new Point();
            Point PB = new Point(a, B.Height - (int)Math.Round((time[0]) * coef) - (int)Math.Round(d / 2));
            Point P = new Point(a, B.Height - (int)Math.Round(d / 2));
            Pen pen = new Pen(Color.FromArgb(192, 0, 0));
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font f = new Font(label1.Font, FontStyle.Regular);
            Brush fb = new SolidBrush(Color.FromArgb(192, 0, 0));
            G.DrawLine(pen, P, PB);
            string str = Convert.ToString(time[0]);
            G.DrawString(str, f, fb, (float)(PB.X - G.MeasureString(str, f).Width / 2),
                (float)(PB.Y - G.MeasureString(str, f).Height - 3));
            for (int i = 1; i < 5; i++)        
            {
                a += w;
                PE.X = a;
                PE.Y = B.Height - (int)Math.Round((time[i])*coef) - (int)Math.Round(d/2);
                BresenhamInt(PB, PE, Color.FromArgb(192, 0, 0));
                P.X = P.X + w;
                str = Convert.ToString(time[i]);
                G.DrawString(str, f, fb, (float)(PE.X - G.MeasureString(str, f).Width / 2),
                    (float)(PE.Y - G.MeasureString(str, f).Height - 3));
                G.DrawLine(pen, P, PE);
                PB.X = PE.X;
                PB.Y = PE.Y;
            }
            float x = d;
            float wf = ((float)B.Width - 2 * d) / 4.0f;
            float y = (float)(P.Y + 3);
            G.DrawString("ЦДА", f, fb, x - G.MeasureString("ЦДА", f).Width / 2, y);
            x += wf;
            G.DrawString("Брезенхем (деств)", f, fb, x - G.MeasureString("Брезенхем (деств)", f).Width/2, y);
            x += wf;
            G.DrawString("Брезенхем (цел)", f, fb, x - G.MeasureString("Брезенхем (цел)", f).Width / 2, y);
            x += wf;
            G.DrawString("Брезенхем (ступ)", f, fb, x - G.MeasureString("Брезенхем (ступ)", f).Width / 2, y);
            x += wf;
            G.DrawString("Стандартный", f, fb, x - G.MeasureString("Стандартный", f).Width / 2, y);

           
            pictBox.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            P1.X = Convert.ToInt32(tbXb.Text);
            P1.Y = Convert.ToInt32(tbYb.Text);
            P2.X = Convert.ToInt32(tbXe.Text);
            P2.Y = Convert.ToInt32(tbYe.Text);
            DrawLine(P1, P2, LineColor);
        }

        private float F(float X)
        {
            //return X + (float)(3 * Math.Sin(X));
            return X*(float)Math.Sin(X);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            br = new SolidBrush(panel2.BackColor);
            G.FillRectangle(br, 0, 0, B.Width, B.Height);
            float x1 = (float)Convert.ToDouble(tbX1.Text);
            float x2 = (float)Convert.ToDouble(tbX2.Text);
            float bx = 20;
            float by = 20;
            int nstep = B.Width - (int)Math.Round(2*bx);
            float xstep = (x2 - x1) /(float)(nstep);
            PointF min = new PointF(x1, F(x1));
            PointF max = new PointF(min.X, min.Y);
            float x = x1 + xstep;
            while (x <= x2)
            {
                if (min.Y > F(x))
                {
                    min.X = x;
                    min.Y = F(x);
                }
                if (max.Y < F(x))
                {
                    max.X = x;
                    max.Y = F(x);
                }
                x += xstep;
            }
            float ycoef = ((float)B.Height - 2*by) / (max.Y - min.Y);
            
            DrawMethod DrawFunc = null;
            switch (cmbMethod.SelectedIndex)
            {
                case 0: DrawFunc = DDA;
                    break;
                case 1: DrawFunc = BresenhamReal;
                    break;
                case 2: DrawFunc = BresenhamInt;
                    break;
                case 3: DrawFunc = BresenhamSmooth;
                    break;
                case 4: DrawFunc = Standart;
                    break;
            }

            Pen pen = new Pen(LineColor);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Point P1 = new Point((int)Math.Round(bx), B.Height - (int)Math.Round(( - min.Y) * ycoef + (by)));
            Point P2 = new Point(B.Width - P1.X, P1.Y);
            G.DrawLine(pen, P1, P2);
            P1 = new Point((int)Math.Round( ((float)B.Width - 2*bx)*-x1 / (x2 - x1) + bx), (int)Math.Round(by));
            P2 = new Point(P1.X, B.Height - P1.Y);
            G.DrawLine(pen, P1, P2);

            x = x1;
            int i = (int)Math.Round(bx);
            Point PE = new Point();
            Point PB = new Point(i, B.Height - (int)Math.Round((F(x) - min.Y) * ycoef + (by)));
            i++;
            x += xstep;          
            while (x <= x2)
            {
                PE.X = i;
                PE.Y = B.Height - (int)Math.Round((F(x) - min.Y) * ycoef + (by));
                DrawFunc(PB, PE, LineColor);
                PB.X = PE.X;
                PB.Y = PE.Y;
                x += xstep;
                i++;
            }
            pictBox.Refresh();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            br = new SolidBrush(Color.FromArgb(255, 255, 192));
            G.FillRectangle(br, 0, 0, B.Width, B.Height);
            double l = Convert.ToDouble(tbSegLength.Text);
            int w = B.Width - 2*40;
            double ang_step = 2 * Math.PI / w;
            w /= 8;
            double ang = ang_step;
            Pen pen = new Pen(Color.FromArgb(192, 0, 0));
            int x1 = 40;
            int y1 = B.Height - 40 - (int)step_count(l, 0);
            int x2, y2;
            for (int i = 1; i < w; i++)
            {
                x2 = i+40;
                y2 = B.Height - 40 - (int)step_count(l, ang);
                G.DrawLine(pen, x1, y1, x2, y2);
                x1 = x2;
                y1 = y2;
                ang += ang_step;
            }

            Font f = new Font(label1.Font, FontStyle.Regular);
            Brush fb = new SolidBrush(Color.FromArgb(192, 0, 0));
            float x = 40;
            float wf = ((float)B.Width - 2 * 40) / 4.0f;
            float y = (float)(B.Height - 40);
            G.DrawString("0", f, fb, x - G.MeasureString("0", f).Width / 2, y);
            x += wf/2;
            G.DrawString("45", f, fb, x - G.MeasureString("45", f).Width / 2, y);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            int step_max = (int)Math.Round(l * Math.Cos(Math.PI / 4));
            G.DrawLine(pen, x, y, x, B.Height - 40 - step_max);
            //x += wf;
            //G.DrawString("180", f, fb, x - G.MeasureString("180", f).Width / 2, y);
            //x += wf;
            //G.DrawString("270", f, fb, x - G.MeasureString("270", f).Width / 2, y);
            //x += wf;
            //G.DrawString("360", f, fb, x - G.MeasureString("360", f).Width / 2, y);

            
            G.DrawString("0", f, fb, 40 - 30, B.Height - 40 - 16);
            G.DrawString(Convert.ToString(step_max), f, fb, 40 - 30, B.Height - 40 - 16 - step_max);
            G.DrawString("Угол, градусы", f, fb, B.Width/2, B.Height - 25);
            G.DrawString("Количество ступенек", f, fb, 40, B.Height - 40 - 16 - step_max - 20);

            //G.DrawLine(pen, 0, B.Height - 40 - step_max, B.Width, B.Height - 40 - step_max);
            G.DrawLine(pen, 40, B.Height - 40, B.Width, B.Height - 40);
            G.DrawLine(pen, 40, B.Height - 40, 40, 40);  
            pictBox.Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            br = new SolidBrush(panel2.BackColor);
            G.FillRectangle(br, 0, 0, B.Width, B.Height);
            pictBox.Refresh();
        }

        private void tbYb_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.KeyChar = (char)0;*/



        }

        private void cmbMethod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Select();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictBox_Click(object sender, EventArgs e)
        {

        }

        private void cmbMethod_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}