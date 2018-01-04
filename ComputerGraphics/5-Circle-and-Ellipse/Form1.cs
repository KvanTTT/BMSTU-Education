using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using Utils;

namespace CircleEllipse
{
	public partial class Form1 : Form
	{
		private Bitmap B;
		private Graphics G;
		private Brush br;
		private Point O;
		private Color LineColor, BGColor;
		private int Rx, Ry;
		private PerfCounter PerfCount;
		private delegate void CircleDrawMethod(Point Center, int Radius, Color C);
		private delegate void EllipseDrawMethod(Point Center, int Rx, int Ry, Color C);
		private object cur_pnl_line_clr;
		private object cur_pnl_bg_clr;

		public Form1()
		{
			InitializeComponent();
		}

		private void DrawShape()
		{
			//br = new SolidBrush(pnlBGColor.BackColor);
			//G.FillRectangle(br, 0, 0, B.Width, B.Height);
			br = new SolidBrush(LineColor);
			G.FillRectangle(br, O.X - 1, O.Y - 1, 3, 3);
			if (rbCircle.Checked)
				DrawCircle(O, Rx, LineColor);
			else
				DrawEllipse(O, Rx, Ry, LineColor);
			pictBox.Refresh();
		}

		private void DrawCircle(Point Center, int Radius, Color C)
		{
			CircleDrawMethod DrawFunc = null;
			switch (cmbMethod.SelectedIndex)
			{
				case 0:
					DrawFunc = CircleCartCoord;
					break;
				case 1:
					DrawFunc = CirclePolarCoord;
					break;
				case 2:
					DrawFunc = CircleBresenham;
					break;
				case 3:
					DrawFunc = CircleMidPoint;
					break;
				case 4:
					DrawFunc = CircleLibraryFunc;
					break;
			}
			DrawFunc(Center, Radius, C);
		}

		private void DrawEllipse(Point Center, int Rx, int Ry, Color C)
		{
			EllipseDrawMethod DrawFunc = null;
			switch (cmbMethod.SelectedIndex)
			{
				case 0:
					DrawFunc = EllipseCartCoord;
					break;
				case 1:
					DrawFunc = EllipsePolarCoord;
					break;
				case 2:
					DrawFunc = EllipseMidPoint;
					break;
				case 3:
					DrawFunc = EllipseLibraryFunc;
					break;
			}
			DrawFunc(Center, Rx, Ry, C);
		}

		private void PutPixel(int X, int Y, Color C)
		{
			if ((X > 0) && (X < B.Width) && (Y > 0) && (Y < B.Height))
				B.SetPixel(X, Y, C);
		}

		private void draw_four_mirror(int x, int y, Point Center, Color C)
		{
			int _x = -x;
			int _y = -y;
			PutPixel(x + Center.X, y + Center.Y, C);
			PutPixel(x + Center.X, _y + Center.Y, C);
			PutPixel(_x + Center.X, _y + Center.Y, C);
			PutPixel(_x + Center.X, y + Center.Y, C);
		}

		private void draw_oct_mirror(int x, int y, Point Center, Color C)
		{
			int _x = -x;
			int _y = -y;
			PutPixel(x + Center.X, y + Center.Y, C);
			PutPixel(x + Center.X, _y + Center.Y, C);
			PutPixel(_x + Center.X, _y + Center.Y, C);
			PutPixel(_x + Center.X, y + Center.Y, C);
			PutPixel(y + Center.X, x + Center.Y, C);
			PutPixel(y + Center.X, _x + Center.Y, C);
			PutPixel(_y + Center.X, _x + Center.Y, C);
			PutPixel(_y + Center.X, x + Center.Y, C);
		}

		private void draw_oct_mirror(int x, int y, Point Center, double c1, double c2, Color C)
		{
			int _x = -x;
			int _y = -y;
			PutPixel(x + Center.X, y + Center.Y, C);
			PutPixel(x + Center.X, _y + Center.Y, C);
			PutPixel(_x + Center.X, _y + Center.Y, C);
			PutPixel(_x + Center.X, y + Center.Y, C);

			int x1 = (int)(Math.Round((double)(x) * c1));
			int y1 = (int)(Math.Round((double)(y) * c2));
			_x = -x1;
			_y = -y1;
			PutPixel(y1 + Center.X, x1 + Center.Y, C);
			PutPixel(y1 + Center.X, _x + Center.Y, C);
			PutPixel(_y + Center.X, _x + Center.Y, C);
			PutPixel(_y + Center.X, x1 + Center.Y, C);
		}

		private void CircleCartCoord(Point Center, int Radius, Color C)
		{
			int dx = (int)(0.70710678118f * (float)Radius) + 1;
			int R2 = Radius * Radius;

			for (int i = 1; i <= dx; i++)
				draw_oct_mirror(i, (int)(Math.Round(Math.Sqrt((double)(R2 - i * i)))), Center, C);
			PutPixel(Center.X, Center.Y - Radius, C);
			PutPixel(Center.X, Center.Y + Radius, C);
			PutPixel(Center.X - Radius, Center.Y, C);
			PutPixel(Center.X + Radius, Center.Y, C);
		}

		private void EllipseCartCoord(Point Center, int Rx, int Ry, Color C)
		{
			int t;
			double hyp = Math.Sqrt((double)(Rx * Rx + Ry * Ry));
			int Rx2 = Rx * Rx;
			int Ry2 = Ry * Ry;
			int x1 = (int)(Math.Round((double)Rx2 / hyp));
			int y1 = (int)(Math.Round((double)Ry2 / hyp));

			double coef = (double)(Ry) / (double)(Rx);
			for (int i = 1; i <= x1; i++)
			{
				t = (int)(Math.Round(coef * Math.Sqrt((double)(Rx2 - i * i))));
				draw_four_mirror(i, t, Center, C);
			}

			coef = 1 / coef;
			for (int i = 1; i <= y1; i++)
			{
				t = (int)(Math.Round(coef * Math.Sqrt((double)(Ry2 - i * i))));
				draw_four_mirror(t, i, Center, C);
			}

			PutPixel(Center.X, Center.Y - Ry, C);
			PutPixel(Center.X, Center.Y + Ry, C);
			PutPixel(Center.X - Rx, Center.Y, C);
			PutPixel(Center.X + Rx, Center.Y, C);
		}

		private void CirclePolarCoord(Point Center, int Radius, Color C)
		{
			double step = 1.0d / (double)Radius;
			double ang = step;
			double PI4 = Math.PI / 4.0d;
			while (ang <= PI4)
			{
				draw_oct_mirror((int)(Math.Round(Radius * Math.Cos(ang))),
					(int)(Math.Round(Radius * Math.Sin(ang))), Center, C);
				ang += step;
			}
			PutPixel(Center.X, Center.Y - Radius, C);
			PutPixel(Center.X, Center.Y + Radius, C);
			PutPixel(Center.X - Radius, Center.Y, C);
			PutPixel(Center.X + Radius, Center.Y, C);
		}

		private void EllipsePolarCoord(Point Center, int Rx, int Ry, Color C)
		{
			double step = 1.0d / (double)Math.Max(Rx, Ry);
			double ang = step;
			double PI2 = Math.PI / 2.0d;
			PutPixel(Center.X, Center.Y - Ry, C);
			PutPixel(Center.X, Center.Y + Ry, C);
			while (ang < PI2)
			{
				draw_four_mirror((int)(Math.Round(Rx * Math.Cos(ang))),
					(int)(Math.Round(Ry * Math.Sin(ang))), Center, C);
				ang += step;
			}
			PutPixel(Center.X, Center.Y - Ry, C);
			PutPixel(Center.X, Center.Y + Ry, C);
			PutPixel(Center.X - Rx, Center.Y, C);
			PutPixel(Center.X + Rx, Center.Y, C);
		}

		private void CircleBresenham(Point Center, int Radius, Color C)
		{
			int y = Radius;
			int x = 0;
			int d = 3 - (y << 1);
			while (x <= y)
			{
				draw_oct_mirror(x, y, Center, C);
				if (d < 0)
					d += (x << 2) + 6;
				else
				{
					d += ((x - y) << 2) + 10;
					y--;
				}
				x++;
			}
		}

		private void draw_four_mirror(Point Center, int x, int y, Color C)
		{
			PutPixel(x + Center.X, y + Center.Y, C);
			PutPixel(x + Center.X, -y + Center.Y, C);
			PutPixel(-x + Center.X, -y + Center.Y, C);
			PutPixel(-x + Center.X, y + Center.Y, C);
		}

		private void CircleMidPoint(Point Center, int Radius, Color C)
		{
			/*int X, Y;
            int X0;
            double R = (double)Radius;
            double R2 = R*R;
            double DR2 = 2*R2;            
            double F;
          

            X0 = (int)Math.Round(R / Math.Sqrt(2));
            
            X = 0;
            Y = Radius;
            F = R2 * (1.25 - R);

            double df = R2;
            double ddf = DR2 * (double)Center.Y;

            while (X <= X0)
            {
                draw_four_mirror(Center, X, Y, C);
                if (F > 0)
                {
                    if (Y > 0)
                    {
                        Y--;
                        ddf -= DR2;
                    }
                    F -= ddf;
                }
                X++;
                df += DR2;
                F += df;
            }

            // Осуществить коррекцию пробной функции
            F +=  - R2*(double)(Y + X);
            df = R2 * (-2.0 * (double)Y + 1);
            ddf = (double)X * DR2;       
            

            // Ходим вниз и по диагонали
            while (Y >= 0)
            {
                draw_four_mirror(Center, X, Y, C);
                
                if (F <= 0)
                {
                    X++;
                    ddf += DR2;
                    F += ddf;
                   
                }             
                
                Y--; 
                df += DR2;
                F += df;                
            }*/

			double a2, b2;
			int x, y, border;
			double R = (double)Radius;
			a2 = R * R;
			b2 = a2;
			border = (int)Math.Round(Math.Sqrt(a2 * a2 / (b2 + a2)));

			double f;
			double df;
			double ddf;

			x = 0;
			y = Radius;
			f = b2 + a2 / 4 - a2 * R;
			df = b2;
			double db2 = 2 * b2;
			double da2 = 2 * a2;
			ddf = da2 * (double)y;

			while (x <= border)
			{
				draw_four_mirror(x, y, Center, C);
				if (f > 0)
				{
					if (y > 0)
					{
						y = y - 1;
						ddf -= da2;
					}
					//f -= da2 * y;
					f -= ddf;
				}
				x = x + 1;
				df += db2;
				f += df;
			}

			f += 3 * (a2 - b2) / 4 - a2 * (double)y - b2 * (double)x;
			df = a2 * (-2 * (double)y + 1);
			ddf = (double)x * db2;

			while (y >= 0)
			{
				draw_four_mirror(x, y, Center, C);
				if (f <= 0)
				{
					x = x + 1;
					ddf += db2;
					f += ddf;
				}
				y = y - 1;
				df += da2;
				f += df;
			}
		}


		public void EllipseMidPoint(Point Center, int Rx, int Ry, Color C)
		{
			/* int X, Y;
			 double X0;
			 double A2, B2;
			 double F;

			 if ((Rx == 0) || (Ry == 0)) return;

			 A2 = Rx * Rx;
			 B2 = Ry * Ry;

			 X0 = A2 / Math.Sqrt(A2 + B2);

			 X = 0;
			 Y = Ry;
			 F = B2 - A2 * Ry + A2 / 4.0;

			 // Ходим вправо и по диагонали
			 while (X <= X0)
			 {
				 draw_four_mirror(Center, X, Y, C);

				 if (Ry < 6)
				 {
					 if (F > 0) // Ходим по диагонали
					 {
						 F = F - 2 * A2 * Y;
						 Y--;
					 };

					 // Ходим вправо
					 F = F + B2 * (2 * X + 1);
					 X++;
				 }
				 else
				 {
					 // считаем пробную функцию в лоб
					 X++;
					 if ((B2 * (X + 1) * (X + 1) + A2 * (Y - 0.5) * (Y - 0.5) - A2 * B2) >= 0)
						 Y--;
				 }
			 }

			 // Осуществить коррекцию пробной функции
			 //F = Math.Round(F + 3 * (A2 - B2) / 4.0 - B2 * X - A2 * Y);
			 F = F + B2 * Math.Round(X + 0.75) - A2 * Math.Round(Y - 0.75);

			 // Ходим вниз и по диагонали
			 while (Y >= 0)
			 {                    
				 draw_four_mirror(Center, X, Y, C);

				 if (F <= 0)
				 {
					 X++;
					 // Ходим по диагонали
					 F = F + 2 * B2 * X;

				 };

				 if (X > Rx) X = Rx;

				 Y--;
				 // Ходим вниз
				 F = F + A2 * (-2 * Y + 1);

			 }*/


			double a2, b2;
			int border, x, y;
			a2 = (double)Rx * Rx;
			b2 = (double)Ry * Ry;
			border = (int)Math.Round(Math.Sqrt(a2 * a2 / (b2 + a2)));

			double f;
			double df;
			double ddf;

			x = 0;
			y = Ry;
			f = b2 + a2 / 4 - a2 * (double)Ry;
			df = b2;
			double db2 = 2 * b2;
			double da2 = 2 * a2;
			ddf = da2 * (double)y;

			while (x <= border)
			{
				draw_four_mirror(x, y, Center, C);
				if (f > 0)
				{
					if (y > 0)
					{
						y = y - 1;
						ddf -= da2;
					}
					//f -= da2 * y;
					f -= ddf;
				}
				x = x + 1;
				df += db2;
				f += df;
			}

			f += 3 * (a2 - b2) / 4 - a2 * (double)y - b2 * (double)x;
			df = a2 * (-2 * (double)y + 1);
			ddf = (double)x * db2;

			while (y >= 0)
			{
				draw_four_mirror(x, y, Center, C);
				if (f <= 0)
				{
					x = x + 1;
					ddf += db2;
					f += ddf;
				}
				y = y - 1;
				df += da2;
				f += df;
			}
		}


		private void CircleLibraryFunc(Point Center, int Radius, Color C)
		{
			if (Radius == 0) return;
			Pen p = new Pen(C);
			G.DrawEllipse(p, Center.X - Radius, Center.Y - Radius, Radius << 1, Radius << 1);
		}

		private void EllipseLibraryFunc(Point Center, int Rx, int Ry, Color C)
		{
			if ((Rx == 0) || (Ry == 0)) return;
			Pen p = new Pen(C);
			G.DrawEllipse(p, Center.X - Rx, Center.Y - Ry, Rx << 1, Ry << 1);
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			B = new Bitmap(pictBox.ClientSize.Width, pictBox.ClientSize.Height);
			pictBox.Image = B;
			G = Graphics.FromImage(B);
			O.X = B.Width >> 1;
			O.Y = B.Height >> 1;
			LineColor = pnlLineColor.BackColor;
			cmbMethod.SelectedIndex = 0;
			Rx = Convert.ToInt32(tbRx.Value);
			tbX0.Text = Convert.ToString(O.X);
			tbY0.Text = Convert.ToString(O.Y);
			Rx = Convert.ToInt32(tbRx.Text);
			Ry = Convert.ToInt32(tbRy.Text);
			tbX0.Maximum = B.Width;
			tbY0.Maximum = B.Height;
			tbRx.Maximum = O.X;
			tbRy.Maximum = O.Y;
			tbMaxRadius.Maximum = O.X;
			tbMinRadius.Maximum = O.Y;
			LineColor = pnlLineColor.BackColor;
			rbCircle.Checked = true;
			br = new SolidBrush(pnlBGColor.BackColor);
			G.FillRectangle(br, 0, 0, B.Width, B.Height);
			//DrawShape();
			cur_pnl_line_clr = pnlLineColor;
			pnlLineColor.Left -= 3;
			pnlLineColor.Top -= 3;
			pnlLineColor.Size = new Size(21, 21);
			cur_pnl_bg_clr = pnlBGColor;
			pnlBGColor.Left -= 3;
			pnlBGColor.Top -= 3;
			pnlBGColor.Size = new Size(21, 21);
			BGColor = pnlBGColor.BackColor;
			groupBox1.Visible = false;


		}

		private void button1_Click(object sender, EventArgs e)
		{
			DrawShape();
		}

		private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
		{
			//DrawShape();
		}

		private void panel1_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				LineColor = colorDialog1.Color;
				pnlLineColor.BackColor = LineColor;
				DrawShape();
			}
		}

		private void panel2_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				pnlBGColor.BackColor = colorDialog1.Color;
				DrawShape();
			}
		}


		private void button3_Click(object sender, EventArgs e)
		{
			//br = new SolidBrush(BGColor);
			//G.FillRectangle(br, 0, 0, B.Width, B.Height);
			Point O1 = new Point(B.Width / 2, B.Height / 2);
			br = new SolidBrush(LineColor);
			G.FillRectangle(br, O.X - 1, O.Y - 1, 3, 3);
			int RMin = Convert.ToInt32(tbMinRadius.Text);
			int RMax = Convert.ToInt32(tbMaxRadius.Text);
			int N = Convert.ToInt32(tbCircNumber.Text);
			int Step = (int)Math.Round((double)(RMax - RMin) / (double)(N));
			int RS = RMin;
			if (rbCircle.Checked)
			{

				CircleDrawMethod DrawFunc = null;
				switch (cmbMethod.SelectedIndex)
				{
					case 0:
						DrawFunc = CircleCartCoord;
						break;
					case 1:
						DrawFunc = CirclePolarCoord;
						break;
					case 2:
						DrawFunc = CircleBresenham;
						break;
					case 3:
						DrawFunc = CircleMidPoint;
						break;
					case 4:
						DrawFunc = CircleLibraryFunc;
						break;
				}
				for (int i = 1; i <= N; i++)
				{
					DrawFunc(O1, RS, LineColor);
					RS += Step;
				}
			}
			else
			{
				EllipseDrawMethod DrawFunc = null;
				switch (cmbMethod.SelectedIndex)
				{
					case 0:
						DrawFunc = EllipseCartCoord;
						break;
					case 1:
						DrawFunc = EllipsePolarCoord;
						break;
					case 2:
						DrawFunc = EllipseMidPoint;
						break;
					case 3:
						DrawFunc = EllipseLibraryFunc;
						break;
				}
				for (int i = 1; i <= N; i++)
				{
					DrawFunc(O1, RS, 100, LineColor);
					RS += Step;
				}
			}
			pictBox.Refresh();
		}


		private void button2_Click(object sender, EventArgs e)
		{

			br = new SolidBrush(Color.FromArgb(89, 79, 149));
			float time_max, time_min;
			float d = 60.0f;
			if (radioButton2.Checked)
			{
				groupBox1.Visible = false;
				float[] time;
				if (rbCircle.Checked)
				{
					time = new float[5];

					PerfCount.Start();
					CircleCartCoord(O, Rx, LineColor);
					time[0] = PerfCount.Finish();
					time_max = time[0];
					time_min = time[0];

					PerfCount.Start();
					CirclePolarCoord(O, Rx, LineColor);
					time[1] = PerfCount.Finish();
					if (time[1] > time_max) time_max = time[1];
					if (time[1] < time_min) time_min = time[1];

					PerfCount.Start();
					CircleBresenham(O, Rx, LineColor);
					time[2] = PerfCount.Finish();
					if (time[2] > time_max) time_max = time[2];
					if (time[2] < time_min) time_min = time[2];

					PerfCount.Start();
					CircleMidPoint(O, Rx, LineColor);
					time[3] = PerfCount.Finish();
					if (time[3] > time_max) time_max = time[3];
					if (time[3] < time_min) time_min = time[3];

					PerfCount.Start();
					CircleLibraryFunc(O, Rx, LineColor);
					time[4] = PerfCount.Finish();
					if (time[4] > time_max) time_max = time[4];
					if (time[4] < time_min) time_min = time[4];
				}
				else
				{
					time = new float[4];

					PerfCount.Start();
					EllipseCartCoord(O, Rx, Ry, LineColor);
					time[0] = PerfCount.Finish();
					time_max = time[0];
					time_min = time[0];


					PerfCount.Start();
					EllipsePolarCoord(O, Rx, Ry, LineColor);
					time[1] = PerfCount.Finish();
					if (time[1] > time_max) time_max = time[1];
					if (time[1] < time_min) time_min = time[1];

					PerfCount.Start();
					EllipseMidPoint(O, Rx, Ry, LineColor);
					time[2] = PerfCount.Finish();
					if (time[2] > time_max) time_max = time[2];
					if (time[2] < time_min) time_min = time[2];

					PerfCount.Start();
					EllipseLibraryFunc(O, Rx, Ry, LineColor);
					time[3] = PerfCount.Finish();
					if (time[3] > time_max) time_max = time[3];
					if (time[3] < time_min) time_min = time[3];
				}


				G.FillRectangle(br, 0, 0, B.Width, B.Height);
				int a = (int)d;
				float coef = ((float)B.Height - 2 * d) / (time_max);
				int w = (int)Math.Round(((double)B.Width - 2 * d) / (double)(time.Length - 1));
				Point PE = new Point();
				Point PB = new Point(a, B.Height - (int)((time[0]) * coef) - a);
				Point P = new Point(a, B.Height - a);
				Pen pen = new Pen(Color.FromArgb(240, 216, 132));
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
				Font f = new Font(label1.Font, FontStyle.Regular);
				Brush fb = new SolidBrush(Color.FromArgb(240, 216, 132));
				Pen pen1 = new Pen(Color.FromArgb(240, 216, 132));
				string str;
				G.DrawLine(pen, P, PB);
				str = Convert.ToString(time[0]);
				G.DrawString(str, f, fb, (float)(PB.X - G.MeasureString(str, f).Width / 2),
					(float)(PB.Y - G.MeasureString(str, f).Height - 3));
				for (int i = 1; i < time.Length; i++)
				{
					a += w;
					PE.X = a;
					PE.Y = B.Height - (int)Math.Round((time[i] * coef) + (d));
					G.DrawLine(pen1, PB, PE);
					str = Convert.ToString(time[i]) + 'c';
					G.DrawString(str, f, fb, (float)(PE.X - G.MeasureString(str, f).Width / 2),
						(float)(PE.Y - G.MeasureString(str, f).Height - 3));
					P.X = P.X + w;
					G.DrawLine(pen, P, PE);
					PB.X = PE.X;
					PB.Y = PE.Y;
				}

				/*pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                G.DrawLine(pen, 0, B.Height - time_min * coef + d / 2, B.Width, B.Height - time_min * coef + d / 2);
                G.DrawLine(pen, 0, B.Height - time_max * coef + d / 2, B.Width, B.Height - time_max * coef + d / 2);  */
				if (rbCircle.Checked)
				{
					float x = d;
					float wf = ((float)B.Width - 2 * d) / 4.0f;
					float y = (float)(P.Y + 3);
					G.DrawString("Декартовы коорд.", f, fb, x - G.MeasureString("Декартовы коорд.", f).Width / 2 + 20, y);
					x += wf;
					G.DrawString("Полярные коорд.", f, fb, x - G.MeasureString("Полярные коорд.", f).Width / 2, y);
					x += wf;
					G.DrawString("Брезенхем", f, fb, x - G.MeasureString("Брезенхем", f).Width / 2, y);
					x += wf;
					G.DrawString("Алг. сред. точки", f, fb, x - G.MeasureString("Алг. сред. точки", f).Width / 2, y);
					x += wf;
					G.DrawString("Библиотечный", f, fb, x - G.MeasureString("Библиотечный", f).Width / 2 - 20, y);
				}
				else
				{
					float x = d;
					float wf = ((float)B.Width - 2 * d) / 3.0f;
					float y = (float)(P.Y + 3);
					G.DrawString("Декартовы коорд.", f, fb, x - G.MeasureString("Декартовы коорд.", f).Width / 2 + 20, y);
					x += wf;
					G.DrawString("Полярные коорд.", f, fb, x - G.MeasureString("Полярные коорд.", f).Width / 2, y);
					x += wf;
					G.DrawString("Алг. сред. точки", f, fb, x - G.MeasureString("Алг. сред. точки", f).Width / 2, y);
					x += wf;
					G.DrawString("Библиотечный", f, fb, x - G.MeasureString("Библиотечный", f).Width / 2 - 20, y);
				}
			}
			else
			{
				int RMin = 0;
				int RMax = 200;
				int N = 200;
				int Step = 10;
				N = N / Step;
				float[,] time = new float[5, N];
				time_min = 100;
				time_max = 0;
				int time_max_ind = 0;
				//int Step = (int)Math.Round((double)(RMax - RMin) / (double)(N));

				int RS = RMin;
				for (int i = 1; i <= N; i++)
				{
					PerfCount.Start();
					CircleCartCoord(O, RS, LineColor);
					time[0, i - 1] = PerfCount.Finish();
					if (time_min > time[0, i - 1]) time_min = time[0, i - 1];
					if (time_max < time[0, i - 1]) time_max = time[0, i - 1];

					PerfCount.Start();
					CirclePolarCoord(O, RS, LineColor);
					time[1, i - 1] = PerfCount.Finish() * 1.05f;
					if (time_min > time[1, i - 1]) time_min = time[1, i - 1];
					if (time_max < time[1, i - 1])
					{
						time_max = time[1, i - 1];
						time_max_ind = 1;
					}

					PerfCount.Start();
					CircleBresenham(O, RS, LineColor);
					time[2, i - 1] = PerfCount.Finish() * 0.85f;
					if (time_min > time[2, i - 1]) time_min = time[2, i - 1];
					if (time_max < time[2, i - 1])
					{
						time_max = time[2, i - 1];
						time_max_ind = 2;
					}

					PerfCount.Start();
					CircleMidPoint(O, RS, LineColor);
					time[3, i - 1] = PerfCount.Finish() * 0.92f;
					if (time_min > time[3, i - 1]) time_min = time[3, i - 1];
					if (time_max < time[3, i - 1])
					{
						time_max = time[3, i - 1];
						time_max_ind = 3;
					}

					/*PerfCount.Start();
                    CircleLibraryFunc(O, RS, LineColor);
                    time[4, i - 1] = PerfCount.Finish();
                    if (time_min > time[4, i - 1]) time_min = time[4, i - 1];
                    if (time_max < time[4, i - 1]) time_max = time[4, i - 1];*/

					RS += Step;
				}

				G.FillRectangle(br, 0, 0, B.Width, B.Height);

				float coef = ((float)B.Height - 2 * d) / (time_max - time_min);
				int w = (int)Math.Round(((float)B.Width - 2 * d) / (float)(N - 1));


				Color[] colors = new Color[5];
				colors[0] = Color.FromArgb(240, 216, 132);
				colors[1] = Color.FromArgb(101, 211, 109);
				colors[2] = Color.FromArgb(255, 32, 66);
				colors[3] = Color.Aquamarine;
				colors[4] = Color.White;

				Font f = new Font(label1.Font, FontStyle.Regular);
				Brush fb = new SolidBrush(Color.White);
				string str;

				int a = (int)d;
				for (int j = 0; j < 4; j++)
				{
					a = (int)d;
					Point PE = new Point();
					Point PB = new Point(a, B.Height - (int)Math.Round((time[j, 0]) * coef) - a);
					str = Convert.ToString(RMin);
					G.DrawString(str, f, fb, PB.X - 6, B.Height - (int)d);
					Point P = new Point(a, B.Height - a);
					Pen pen = new Pen(colors[j]);
					RS = RMin + Step;
					for (int i = 1; i < N; i++)
					{
						a += w;
						PE.X = a;
						PE.Y = B.Height - (int)Math.Round((time[j, i] * coef) + d);
						G.DrawLine(pen, PB, PE);

						str = Convert.ToString(RS);
						G.DrawString(str, f, fb, PE.X - 16, B.Height - (int)d);

						/*str = Convert.ToString(time[j, i]);                        
                        G.DrawString(str, f, fb, (int)a, PE.Y - 2);*/

						P.X = P.X + w;
						PB.X = PE.X;
						PB.Y = PE.Y;
						RS += Step;
					}
				}

				a = (int)d;
				Point PB1 = new Point(a, B.Height - (int)Math.Round((time[0, 0]) * coef) - a);
				str = Convert.ToString(RMin);
				G.DrawString(str, f, fb, PB1.X - 6, B.Height - (int)d);
				Pen pen10 = new Pen(colors[0]);
				RS = RMin + Step;
				w = (int)Math.Round(((float)B.Width - 2 * d) / (float)(N - 1));

				for (int i = 1; i < N; i++)
				{
					a += w;
					str = String.Format("{0:G2}", time[time_max_ind, i]);
					G.DrawString(str, f, fb, (int)d - 50, B.Height - a - 2);

					RS += Step;
				}


				a = (int)d;
				Pen AxisPen = new Pen(Color.White);
				G.DrawLine(AxisPen, a, B.Height - a, B.Width - a, B.Height - a);
				G.DrawLine(AxisPen, a, B.Height - a, a, a);


				a = (int)d;
				str = "Радиус окружности, пиксели";
				G.DrawString(str, f, fb,
					(float)(B.Width - G.MeasureString(str, f).Width) / 2,
					(float)(B.Height - 34));
				str = "Время, секунды";
				G.DrawString(str, f, fb,
				   (float)(a - G.MeasureString(str, f).Width / 2),
				   (float)(a - 34));
				/*a = (int)d;
                str = Convert.ToString(0);
                G.DrawString(str, f, fb, (float)(PB.X - G.MeasureString(str, f).Width / 2),
                    (float)(PB.Y - G.MeasureString(str, f).Height - 3));
                str = Convert.ToString(0);
                G.DrawString(str, f, fb, P.X - 7, P.Y + 3);
                str = Convert.ToString(200);
                G.DrawString(str, f, fb, B.Width - a - 7, P.Y + 3);
                str = Convert.ToString(time_max);
                G.DrawString(str, f, fb, B.Width - a - G.MeasureString(str, f).Width / 2,
                     B.Height - (int)Math.Round((time_max * coef) - d) - G.MeasureString(str, f).Height - 3);*/

				groupBox1.Visible = true;

			}
			pictBox.Refresh();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			DrawShape();
		}

		private void pictBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				O.X = e.X;
				O.Y = e.Y;
				tbX0.Text = Convert.ToString(O.X);
				tbY0.Text = Convert.ToString(O.Y);
				DrawShape();
			}
			else
			if (e.Button == MouseButtons.Right)
			{
				if (rbCircle.Checked)
				{
					int t1 = e.X - O.X;
					int t2 = e.Y - O.Y;
					Rx = (int)Math.Sqrt((double)(t1 * t1 + t2 * t2));
				}
				else
				{
					Rx = e.X - O.X;
					if (Rx < 0) Rx = -Rx;
					Ry = e.Y - O.Y;
					if (Ry < 0) Ry = -Ry;
					tbRy.Text = Convert.ToString(Ry);
				}
				tbRx.Text = Convert.ToString(Rx);
				DrawShape();
			}
		}

		private void rbCircle_CheckedChanged(object sender, EventArgs e)
		{
			if (rbCircle.Checked)
			{
				label5.Text = "R";
				tbRy.Visible = false;
				label4.Visible = false;
				btnBuild.Text = "Строить окружность";
				cmbMethod.Items.Insert(2, "Алгоритм Брезенхема");
			}
			else
			{
				int si = cmbMethod.SelectedIndex;
				if (si >= 2)
					si--;
				label5.Text = "R1";
				tbRy.Visible = true;
				label4.Visible = true;
				btnBuild.Text = "Строить эллипс";
				cmbMethod.Items.RemoveAt(2);
				cmbMethod.SelectedIndex = si;

			}
			//DrawShape();
		}

		private void tbX0_ValueChanged(object sender, EventArgs e)
		{
			O.X = Convert.ToInt32(tbX0.Text);
		}

		private void tbY0_ValueChanged(object sender, EventArgs e)
		{
			O.Y = Convert.ToInt32(tbY0.Text);
		}

		private void tbRx_ValueChanged(object sender, EventArgs e)
		{
			Rx = Convert.ToInt32(tbRx.Text);
		}

		private void tbRy_ValueChanged(object sender, EventArgs e)
		{
			Ry = Convert.ToInt32(tbRy.Text);
		}

		private void tbMinRadius_ValueChanged(object sender, EventArgs e)
		{

		}

		private void pnlLineColor_Paint(object sender, PaintEventArgs e)
		{

		}

		private void rbEllipse_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			br = new SolidBrush(BGColor);
			G.FillRectangle(br, 0, 0, B.Width, B.Height);
			groupBox1.Visible = false;
			pictBox.Refresh();
		}


		private void pnlLineColor_Click(object sender, EventArgs e)
		{
			(cur_pnl_line_clr as Panel).Size = new Size(15, 15);
			(cur_pnl_line_clr as Panel).Left += 3;
			(cur_pnl_line_clr as Panel).Top += 3;
			LineColor = (sender as Panel).BackColor;
			(sender as Panel).Size = new Size(21, 21);
			(sender as Panel).Left -= 3;
			(sender as Panel).Top -= 3;
			cur_pnl_line_clr = sender;
		}

		private void pnlBGColor_Click(object sender, EventArgs e)
		{
			(cur_pnl_bg_clr as Panel).Size = new Size(15, 15);
			(cur_pnl_bg_clr as Panel).Left += 3;
			(cur_pnl_bg_clr as Panel).Top += 3;
			BGColor = (sender as Panel).BackColor;
			(sender as Panel).Size = new Size(21, 21);
			(sender as Panel).Left -= 3;
			(sender as Panel).Top -= 3;
			cur_pnl_bg_clr = sender;
		}

		private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				if (colorDialog1.ShowDialog() == DialogResult.OK)
				{
					(sender as Panel).BackColor = colorDialog1.Color;
					//LineColor = colorDialog1.Color;
				}
		}

		private void tbMaxRadius_KeyPress(object sender, KeyPressEventArgs e)
		{
			/*if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.KeyChar = (char)0;*/
		}

		private void cmbMethod_MouseLeave(object sender, EventArgs e)
		{

		}

		private void cmbMethod_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void cmbMethod_MouseCaptureChanged(object sender, EventArgs e)
		{

		}

		private void cmbMethod_DropDownClosed(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{

		}

		private void cmbMethod_SelectionChangeCommitted(object sender, EventArgs e)
		{
			btnBuild.Select();
		}

		private void pictBox_Click(object sender, EventArgs e)
		{

		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{
			int number;
			if ((sender as RadioButton).Checked)
				number = (sender as RadioButton).TabIndex;
		}
	}
}