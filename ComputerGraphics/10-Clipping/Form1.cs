using Geometry2d;
using Graphics2d;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Clipping
{
	public partial class Form1 : Form
	{
		bool step = false;
		int curstep = 0;
		GraphPolygon Result;

		List<GraphPolygon> Polygons;
		GraphPolygon ClipPolygon;
		Bitmap B;
		Graphics G;

		public Form1()
		{
			InitializeComponent();
		}

		private void Clear()
		{
			Brush brush = new SolidBrush(pnlBackground.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);
		}

		public bool PolygonClip(GraphPolygon Source, ref GraphPolygon Result, GraphPolygon Clipper)
		{
			if ((Clipper.Count < 3) || (Clipper.Convexity == -1))
			{
				MessageBox.Show("Polygon is not convex");
				return false;
			}

			int i, j;
			PointD R;
			PointD S = new PointD(), F = new PointD();
			List<PointD> TempSource = new List<PointD>();
			if (curstep == 0)
				Result.Vertexes = new List<PointD>(Source.Vertexes);
			Result.Closed = true;
			int t;
			if (Clipper.Square > 0)
				t = 1;
			else t = -1;
			Clipper.Add(Clipper[0]);


			for (i = curstep; i < Clipper.Count - 1; i++)
			{
				TempSource.Clear();
				F = Result.Vertexes[0]; // end vertex
				S = Result.Vertexes[0]; // begin vertex
				if (Geom2d.Square(S, Clipper.Vertexes[i], Clipper.Vertexes[i + 1]) * t >= 0)
					TempSource.Add(S);
				for (j = 1; j < Result.Vertexes.Count; j++)
				{
					if (Geom2d.SegmentLineIntersect(S, Result.Vertexes[j], Clipper.Vertexes[i], Clipper.Vertexes[i + 1], out R) == 1)
						TempSource.Add(R);

					S = Result.Vertexes[j];
					if (Geom2d.Square(S, Clipper.Vertexes[i], Clipper.Vertexes[i + 1]) * t > 0)
						TempSource.Add(S);
				}

				if ((TempSource.Count != 0) && (Geom2d.SegmentLineIntersect(S, F, Clipper.Vertexes[i], Clipper.Vertexes[i + 1], out R) == 1))
					TempSource.Add(R);

				Result.Clear();
				Result.Vertexes.AddRange(TempSource);
				if (step)
				{
					curstep = i + 1;
					step = false;
					Clipper.RemoveLast();
					return true;
				}
			}

			Clipper.RemoveLast();
			return true;
		}

		private void UpdateAll()
		{
			Brush brush = new SolidBrush(pnlBackground.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);
			for (int i = 0; i < Polygons.Count; i++)
				Polygons[i].Draw();
			ClipPolygon.Draw();
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			Brush brush = new SolidBrush(pnlBackground.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);

			PointD P = new PointD(e.X, e.Y);
			if (rbIntercept.Checked)
			{
				if (e.Button == MouseButtons.Left)
				{
					if (Polygons[Polygons.Count - 1].Count != 0)
					{
						if (IsShiftDown() == true)
						{
							PointD PT = new PointD();
							PT = Polygons[Polygons.Count - 1][Polygons[Polygons.Count - 1].Count - 1];
							if (Math.Abs(e.X - PT.X) < Math.Abs(e.Y - PT.Y))
							{
								P.X = PT.X;
								P.Y = e.Y;
							}
							else
							{
								P.X = e.X;
								P.Y = PT.Y;
							}
						}
					}
					Polygons[Polygons.Count - 1].Add(P);
					//Polygons[Polygons.Count-1].DrawLastSegment();
				}
				else
					if (e.Button == MouseButtons.Right)
				{
					Polygons[Polygons.Count - 1].Closed = true;
					//Polygons[Polygons.Count - 1].DrawBeginEndSegment();
					Polygons.Add(new GraphPolygon(B, G, new Pen(pnlLine.BackColor)));
				}
			}
			else
			{
				if (e.Button == MouseButtons.Left)
				{
					if (ClipPolygon.Closed == true)
					{
						ClipPolygon.Clear();
						ClipPolygon.Closed = false;
					}
					else
					{
						if ((ClipPolygon.Count >= 1) && (IsShiftDown() == true))
						{
							Point PT = new Point();
							PT = ClipPolygon[ClipPolygon.Count - 1];
							if (Math.Abs(e.X - PT.X) < Math.Abs(e.Y - PT.Y))
							{
								P.X = PT.X;
								P.Y = e.Y;
							}
							else
							{
								P.X = e.X;
								P.Y = PT.Y;
							}
						}
					}
					ClipPolygon.Add(P);
					ClipPolygon.UpdatePolygon();
				}
				else
					if (e.Button == MouseButtons.Right)
				{
					//ClipPolygon.Add(ClipPolygon[0]);
					ClipPolygon.Closed = true;
				}
				//ClipPolygon.DrawLastSegment(); 
				if (ClipPolygon.Square > 0)
					lblBypass.Text = "Против час. стрелки";
				else
					if (ClipPolygon.Square < 0)
					lblBypass.Text = "По час. стрелке";
				else
					lblBypass.Text = "---";

				if (ClipPolygon.Convexity > 0)
					lblConvex.Text = "Да";
				else
					if (ClipPolygon.Convexity < 0)
					lblConvex.Text = "Нет";
				else
					lblConvex.Text = "---";

				if (ClipPolygon.SelfIntersection > 0)
					lblSelfintersect.Text = "Есть";
				else
					if (ClipPolygon.SelfIntersection < 0)
					lblSelfintersect.Text = "Нет";
				else
					lblSelfintersect.Text = "---";

			}
			UpdateAll();
			//ClipAndDraw();
			pictureBox1.Refresh();
		}

		private void ClipAndDraw()
		{
			if (curstep == 0)
				Result = new GraphPolygon(B, G, new Pen(pnlCuttingLine.BackColor, 2));
			for (int i = 0; i < Polygons.Count - 1; i++)
			{
				if (PolygonClip(Polygons[i], ref Result, ClipPolygon))
					Result.Draw();
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			B = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			pictureBox1.Image = B;
			G = Graphics.FromImage(B);
			Polygons = new List<GraphPolygon>();
			ClipPolygon = new GraphPolygon(B, G, new Pen(pnlCuttingRect.BackColor));
			Polygons.Add(new GraphPolygon(B, G, new Pen(pnlLine.BackColor)));

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

		private void pnlLine_MouseUp(object sender, MouseEventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				(sender as Panel).BackColor = colorDialog1.Color;
				UpdateAll();
				ClipAndDraw();
				pictureBox1.Refresh();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Polygons.Clear();
			ClipPolygon.Clear();
			Polygons.Add(new GraphPolygon(B, G, new Pen(pnlLine.BackColor)));
			Brush brush = new SolidBrush(pnlBackground.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);
			UpdateAll();
			pictureBox1.Refresh();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			step = true;
			UpdateAll();
			ClipAndDraw();
			pictureBox1.Refresh();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			step = false;
			curstep = 0;
			UpdateAll();
			ClipAndDraw();
			pictureBox1.Refresh();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			step = false;
			curstep = 0;
			UpdateAll();
			pictureBox1.Refresh();
		}
	}
}