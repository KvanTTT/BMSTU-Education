using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SegmentCut
{
	public partial class Form1 : Form
	{
		private Bitmap B;
		private Graphics G;
		private Point P1, P2;
		private Point RP1, RP2;
		private bool SecondPoint = false;
		private ArrayList Points = new ArrayList();

		const int LEFT = 1;  /* двоичное 0001 */
		const int RIGHT = 2;  /* двоичное 0010 */
		const int BOT = 4;  /* двоичное 0100 */
		const int TOP = 8;  /* двоичное 1000 */

		public Form1()
		{
			InitializeComponent();
		}

		public int vcode(Point LB, Point RT, Point P)
		{
			return (((P.X < LB.X) ? LEFT : 0) +  /* +1 если точка левее прямоугольника */
			((P.X > RT.X) ? RIGHT : 0) +         /* +2 если точка правее прямоугольника */
			((P.Y < LB.Y) ? BOT : 0) +        /* +4 если точка ниже прямоугольника */
			((P.Y > RT.Y) ? TOP : 0));             /* +8 если точка выше прямоугольника */
		}

		/* если отрезок ab не пересекает прямоугольник r, функция возвращает -1;
           если отрезок ab пересекает прямоугольник r, функция возвращает 0 и отсекает
           те части отрезка, которые находятся вне прямоугольника */
		int CohenSutherland(Point LB, Point RT, Point A, Point B, ref Point A1, ref Point B1)
		{
			int code_a, code_b, code; /* код конечных точек отрезка */
			Point C; /* одна из точек */

			A1 = A;
			B1 = B;

			code_a = vcode(LB, RT, A1);
			code_b = vcode(LB, RT, B1);

			/* пока одна из точек отрезка вне прямоугольника */
			while ((code_a != 0) || (code_b) != 0)
			{
				/* если обе точки с одной стороны прямоугольника, то отрезок не пересекает прямоугольник */
				if ((code_a & code_b) != 0)
					return -1;

				/* выбираем точку c с ненулевым кодом */
				if (code_a != 0)
				{
					code = code_a;
					C = A;
				}
				else
				{
					code = code_b;
					C = B;
				}

				/* если c левее r, то передвигаем c на прямую x = R.x_min
		           если c правее r, то передвигаем c на прямую x = R.x_max */
				if ((code & LEFT) != 0)
				{
					C.Y += (A.Y - B.Y) * (LB.X - C.X) / (A.X - B.X);
					C.X = LB.X;
				}
				else
				if ((code & RIGHT) != 0)
				{
					C.Y += (A.Y - B.Y) * (RT.X - C.X) / (A.X - B.X);
					C.X = RT.X;
				}

				/* если c ниже r, то передвигаем c на прямую y = R.y_min
		           если c выше r, то передвигаем c на прямую y = R.y_max */
				if ((code & BOT) != 0)
				{
					C.X += (A.X - B.X) * (LB.Y - C.Y) / (A.Y - B.Y);
					C.Y = LB.Y;
				}
				else
				if ((code & TOP) != 0)
				{
					C.X += (A.X - B.X) * (RT.Y - C.Y) / (A.Y - B.Y);
					C.Y = RT.Y;
				}


				/* обновляем код */
				if (code == code_a)
				{
					A1 = C;
					code_a = vcode(LB, RT, A1);
				}
				else
				{
					B1 = C;
					code_b = vcode(LB, RT, B1);
				}
			}

			/* оба кода равны 0, следовательно обе точки в прямоугольнике */
			return 0;
		}

		private void PutPixel(int X, int Y, Color C)
		{
			if ((X > 0) && (X < B.Width) && (Y > 0) && (Y < B.Height))
				B.SetPixel(X, Y, C);
		}

		private void UpdateAll()
		{
			Brush brush = new SolidBrush(pnlBackgroud.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);
			Pen pen = new Pen(pnlLine.BackColor);
			Pen pCuttingLine = new Pen(pnlCuttingLine.BackColor);
			Pen pLine = new Pen(pnlLine.BackColor);
			pLine.Width = 2;
			Point A1 = new Point();
			Point B1 = new Point();
			for (int i = 0; i < (Points.Count - Points.Count % 2); i = i + 2)
			{
				G.DrawLine(pen, (Point)Points[i], (Point)Points[i + 1]);
				G.DrawLine(pCuttingLine, (Point)Points[i], (Point)Points[i + 1]);
				if (CohenSutherland(RP1, RP2, (Point)Points[i], (Point)Points[i + 1], ref A1, ref B1) == 0)
					G.DrawLine(pLine, A1, B1);
			}

			pen.Color = pnlCuttingRect.BackColor;
			G.DrawRectangle(pen, RP1.X, RP1.Y, RP2.X - RP1.X, RP2.Y - RP1.Y);

			pictBox.Refresh();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			B = new Bitmap(pictBox.ClientSize.Width, pictBox.ClientSize.Height);
			pictBox.Image = B;
			G = Graphics.FromImage(B);

			btnUpdate_Click(null, null);
		}

		private int AbsI(int X)
		{
			if (X < 0)
				return -X;
			else
				return X;
		}

		private void pictBox_MouseUp_1(object sender, MouseEventArgs e)
		{
			if ((IsCtrlDown() == true) || (rbClipper.Checked == true))
			{
				if (e.Button == MouseButtons.Left)
				{
					//if (SecondRPoint == true)
					{
						RP1.X = e.X;
						RP1.Y = e.Y;
						tbRXb.Text = Convert.ToString(RP1.X);
						tbRYb.Text = Convert.ToString(RP1.Y);
					}
					/*else
					{
						RP2.X = e.X;
						RP2.Y = e.Y;
						tbRXe.Text = Convert.ToString(RP2.X);
						tbRYe.Text = Convert.ToString(RP2.Y);
						SecondRPoint = true;
					}*/
				}
				else
					if (e.Button == MouseButtons.Right)
				{
					RP2.X = e.X;
					RP2.Y = e.Y;
					tbRXe.Text = Convert.ToString(RP2.X);
					tbRYe.Text = Convert.ToString(RP2.Y);
				}

				int T;
				if (RP1.X > RP2.X)
				{
					T = RP1.X;
					RP1.X = RP2.X;
					RP2.X = T;
				}
				if (RP1.Y > RP2.Y)
				{
					T = RP1.Y;
					RP1.Y = RP2.Y;
					RP2.Y = T;
				}
			}
			else
			{
				if (IsShiftDown() == false)
				{
					if (e.Button == MouseButtons.Left)
					{
						if (SecondPoint == true)
						{
							P2.X = e.X;
							P2.Y = e.Y;
							tbXe.Text = Convert.ToString(P2.X);
							tbYe.Text = Convert.ToString(P2.Y);
							Points.Add(P2);
							SecondPoint = false;
						}
						else
						{
							P1.X = e.X;
							P1.Y = e.Y;
							tbXb.Text = Convert.ToString(P1.X);
							tbYb.Text = Convert.ToString(P1.Y);
							Points.Add(P1);
							SecondPoint = true;
						}
					}
					else
						if (e.Button == MouseButtons.Right)
					{
						P2.X = e.X;
						P2.Y = e.Y;
						tbXe.Text = Convert.ToString(P2.X);
						tbYe.Text = Convert.ToString(P2.Y);
					}
				}
				else
				{
					if (SecondPoint == false)
					{
						if (AbsI(e.X - P2.X) < AbsI(e.Y - P2.Y))
						{
							P1.X = P2.X;
							P1.Y = e.Y;
						}
						else
						{
							P1.X = e.X;
							P1.Y = P2.Y;
						}
						tbXb.Text = Convert.ToString(P1.X);
						tbYb.Text = Convert.ToString(P1.Y);
						Points.Add(P1);
						SecondPoint = true;
					}
					else
							if (AbsI(e.X - P1.X) < AbsI(e.Y - P1.Y))
					{
						P2.X = P1.X;
						P2.Y = e.Y;
					}
					else
					{
						P2.X = e.X;
						P2.Y = P1.Y;
					}
					tbXe.Text = Convert.ToString(P2.X);
					tbYe.Text = Convert.ToString(P2.Y);
					Points.Add(P2);
					SecondPoint = false;
				}
			}
			UpdateAll();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			Brush brush = new SolidBrush(pnlBackgroud.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);
			pictBox.Refresh();
		}

		private void tbYb_KeyPress(object sender, KeyPressEventArgs e)
		{

			if ((e.KeyChar != (char)8) && ((e.KeyChar < '0') || (e.KeyChar > '9')))
				e.KeyChar = (char)0;
		}

		const int VK_SHIFT = 16;
		const int VK_CTRL = 17;
		const ushort MASK = 0x8000;

		[DllImport("User32.dll")]
		static extern short GetKeyState(int nVirtKey);

		public static bool IsShiftDown()
		{
			return ((GetKeyState(VK_SHIFT) & MASK) > 0);
		}

		public static bool IsCtrlDown()
		{
			return ((GetKeyState(VK_CTRL) & MASK) > 0);
		}

		private void pnlCuttingLine_MouseUp(object sender, MouseEventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				(sender as Panel).BackColor = colorDialog1.Color;
				UpdateAll();
			}
		}

		private void pictBox_Click(object sender, EventArgs e)
		{
			btnUpdate_Click(null, null);
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			P1.X = Convert.ToInt32(tbXb.Text);
			P1.Y = Convert.ToInt32(tbYb.Text);
			P2.X = Convert.ToInt32(tbXe.Text);
			P2.Y = Convert.ToInt32(tbYe.Text);
			RP1.X = Convert.ToInt32(tbRXb.Text);
			RP1.Y = Convert.ToInt32(tbRYb.Text);
			RP2.X = Convert.ToInt32(tbRXe.Text);
			RP2.Y = Convert.ToInt32(tbRYe.Text);
			UpdateAll();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Points.Clear();
			UpdateAll();
		}
	}
}