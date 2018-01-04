using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SegmentPolygonCut
{
	public partial class Form1 : Form
	{
		private Bitmap B;
		private Graphics G;
		private PointF P1, P2;
		private List<PointF> Polygon;
		private bool PBuild = false;
		private bool SecondPoint = false;
		private List<PointF> Points = new List<PointF>();
		int Conv, SelfIntersect;
		float S;

		public Form1()
		{
			InitializeComponent();
		}

		float PMSquare(PointF P1, PointF P2)
		{
			return (P2.X * P1.Y - P1.X * P2.Y);
		}

		float PMSquare(PointF P1, PointF P2, PointF P3)
		{
			return ((P3.X - P1.X) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P3.Y - P1.Y));
		}

		int LineIntersect(PointF A1, PointF A2, PointF B1, PointF B2, ref PointF O)
		{
			float a1 = A2.Y - A1.Y;
			float b1 = A1.X - A2.X;
			float d1 = -a1 * A1.X - b1 * A1.Y;
			float a2 = B2.Y - B1.Y;
			float b2 = B1.X - B2.X;
			float d2 = -a2 * B1.X - b2 * B1.Y;
			float t = a2 * b1 - a1 * b2;

			if (t == 0)
				return -1;

			O.Y = (a1 * d2 - a2 * d1) / t;
			O.X = (b2 * d1 - b1 * d2) / t;

			if (A1.X > A2.X)
			{
				if ((O.X < A2.X) || (O.X > A1.X))
					return 0;
			}
			else
			{
				if ((O.X < A1.X) || (O.X > A2.X))
					return 0;
			}

			if (A1.Y > A2.Y)
			{
				if ((O.Y < A2.Y) || (O.Y > A1.Y))
					return 0;
			}
			else
			{
				if ((O.Y < A1.Y) || (O.Y > A2.Y))
					return 0;
			}

			if (B1.X > B2.X)
			{
				if ((O.X < B2.X) || (O.X > B1.X))
					return 0;
			}
			else
			{
				if ((O.X < B1.X) || (O.X > B2.X))
					return 0;
			}

			if (B1.Y > B2.Y)
			{
				if ((O.Y < B2.Y) || (O.Y > B1.Y))
					return 0;
			}
			else
			{
				if ((O.Y < B1.Y) || (O.Y > B2.Y))
					return 0;
			}

			return 1;
		}

		bool LineIntersectParam(PointF A1, PointF A2, PointF B1, PointF B2, ref PointF O)
		{
			float k11 = A2.X - A1.X;
			float k12 = A2.Y - A1.Y;
			float k21 = B2.X - B1.X;
			float k22 = B2.Y - B1.Y;
			float dx = B1.X - A1.X;
			float dy = A1.Y - B1.Y;

			float d = 1 / (k22 * k11 - k21 * k12);

			float t1 = (k22 * dx + k21 * dy) * d;
			if ((t1 < 0) || (t1 > 1))
				return false;

			float t2 = (k12 * dx + k11 * dy) * d;
			if ((t2 < 0) || (t2 > 1))
				return false;

			O.X = A1.X + k11 * t1;
			O.Y = A1.Y + k12 * t1;
			return true;
		}

		float Square(List<PointF> Polygon)
		{
			float S = 0;
			if (Polygon.Count >= 3)
			{
				for (int i = 0; i < Polygon.Count - 1; i++)
					S += PMSquare((PointF)Polygon[i], (PointF)Polygon[i + 1]);
				S += PMSquare((PointF)Polygon[Polygon.Count - 1], (PointF)Polygon[0]);
			}
			return S;
		}

		int SelfIntersection(List<PointF> Polygon)
		{
			if (Polygon.Count < 3)
				return 0;
			int High = Polygon.Count - 1;
			PointF O = new PointF();
			int i;
			for (i = 0; i < High; i++)
			{
				for (int j = i + 2; j < High; j++)
				{
					if (LineIntersect(Polygon[i], Polygon[i + 1],
									  Polygon[j], Polygon[j + 1], ref O) == 1)
						return 1;
				}
			}
			for (i = 1; i < High - 1; i++)
				if (LineIntersect(Polygon[i], Polygon[i + 1], Polygon[High], Polygon[0], ref O) == 1)
					return 1;
			return -1;
		}

		int PointInPolygon(PointF P, List<PointF> Polygon)
		{
			float S1;
			if (Square(Polygon) > 0)
			{
				for (int i = 0; i < Polygon.Count - 1; i++)
				{
					S1 = PMSquare(Polygon[i], Polygon[i + 1], P);
					if (S1 < 0)
						return -1;
					else
						if (S1 == 0)
						return 0;
				}
				S1 = PMSquare(Polygon[Polygon.Count - 1], Polygon[0], P);
				if (S1 < 0)
					return -1;
				else
					if (S1 == 0)
					return 0;
			}
			else
			{
				for (int i = 0; i < Polygon.Count - 1; i++)
				{
					S1 = PMSquare(Polygon[i], Polygon[i + 1], P);
					if (S1 > 0)
						return -1;
					else
						if (S1 == 0)
						return 0;
				}
				S1 = PMSquare(Polygon[Polygon.Count - 1], Polygon[0], P);
				if (S1 > 0)
					return -1;
				else
					if (S1 == 0)
					return 0;
			}
			return 1;
		}

		int SegmentCutting(List<PointF> Polygon, PointF P1, PointF P2, ref PointF NP1, ref PointF NP2)
		{
			Polygon.Add(Polygon[0]);
			Polygon.Add(Polygon[1]);

			float Tn = 0, Tv = 1, t, Ds, Ws;
			PointF D = new PointF();
			PointF f = new PointF();
			PointF w = new PointF();
			PointF n = new PointF();
			PointF r = new PointF();

			D.X = P2.X - P1.X;
			D.Y = P2.Y - P1.Y;
			int k = Polygon.Count - 2;

			for (int i = 0; i < k; i++)
			{
				f.X = (Polygon[i].X + Polygon[i + 1].X) / 2;
				f.Y = (Polygon[i].Y + Polygon[i + 1].Y) / 2;
				w.X = P1.X - f.X;
				w.Y = P1.Y - f.Y;

				//нахождение внутр. нормали
				n.X = Polygon[i].Y - Polygon[i + 1].Y;
				n.Y = Polygon[i + 1].X - Polygon[i].X;

				r.X = Polygon[i + 2].X - Polygon[i].X;
				r.Y = Polygon[i + 2].Y - Polygon[i].Y;
				if (CrossProduct(r, n) < 0)
				{
					n.X = -n.X;
					n.Y = -n.Y;
				}

				Ds = CrossProduct(D, n);
				Ws = CrossProduct(w, n);

				if (Ds == 0)
				{
					if (Ws < 0)
					{
						Polygon.RemoveRange(Polygon.Count - 2, 2);
						return -1;
					}
				}
				else
				{
					t = -Ws / Ds;
					if (Ds > 0)
					{
						if (t > 1)
						{
							Polygon.RemoveRange(Polygon.Count - 2, 2);
							return -1;
						}
						Tn = Math.Max(t, Tn);
					}
					else
					{
						if (t < 0)
						{
							Polygon.RemoveRange(Polygon.Count - 2, 2);
							return -1;
						}
						Tv = Math.Min(t, Tv);
					}
				}
			}

			if (Tn > Tv)
			{
				Polygon.RemoveRange(Polygon.Count - 2, 2);
				return -1;
			}
			NP1 = new PointF(P1.X + (P2.X - P1.X) * Tn, P1.Y + (P2.Y - P1.Y) * Tn);
			NP2 = new PointF(P1.X + (P2.X - P1.X) * Tv, P1.Y + (P2.Y - P1.Y) * Tv);

			Polygon.RemoveRange(Polygon.Count - 2, 2);
			return 0;
		}

		int Convex(List<PointF> Polygon)
		{
			if (Polygon.Count >= 3)
			{
				if (Square(Polygon) > 0)
				{
					for (int i = 0; i < Polygon.Count - 2; i++)
						if (PMSquare(Polygon[i], Polygon[i + 1], Polygon[i + 2]) < 0)
							return -1;
					if (PMSquare(Polygon[Polygon.Count - 2], Polygon[Polygon.Count - 1], Polygon[0]) < 0)
						return -1;
					if (PMSquare(Polygon[Polygon.Count - 1], Polygon[0], Polygon[1]) < 0)
						return -1;
				}
				else
				{
					for (int i = 0; i < Polygon.Count - 2; i++)
						if (PMSquare(Polygon[i], Polygon[i + 1], Polygon[i + 2]) > 0)
							return -1;
					if (PMSquare(Polygon[Polygon.Count - 2], Polygon[Polygon.Count - 1], Polygon[0]) > 0)
						return -1;
					if (PMSquare(Polygon[Polygon.Count - 1], Polygon[0], Polygon[1]) > 0)
						return -1;
				}
				return 1;
			}
			return 0;
		}

		float CrossProduct(PointF P1, PointF P2)
		{
			return (P1.X * P2.X + P1.Y * P2.Y);
		}

		float NormSqr(PointF V)
		{
			return (V.X * V.X + V.Y * V.Y);
		}

		float Norm(PointF V)
		{
			return (float)Math.Sqrt(V.X * V.X + V.Y * V.Y);
		}

		float Cos(List<PointF> Polygon, int CenterInd, int V1Ind, int V2Ind)
		{
			PointF A = new PointF(((PointF)Polygon[V1Ind]).X - ((PointF)Polygon[CenterInd]).X,
								  ((PointF)Polygon[V1Ind]).Y - ((PointF)Polygon[CenterInd]).Y);
			PointF B = new PointF(((PointF)Polygon[V2Ind]).X - ((PointF)Polygon[CenterInd]).X,
								  ((PointF)Polygon[V2Ind]).Y - ((PointF)Polygon[CenterInd]).Y);
			return (CrossProduct(A, B) / (float)Math.Sqrt(NormSqr(A) * NormSqr(B)));
		}

		bool Intersect(List<PointF> Polygon, int Vertex1Ind, int Vertex2Ind, int Vertex3Ind)
		{
			float S1, S2, S3;
			for (int i = 0; i < Polygon.Count; i++)
			{
				if ((i == Vertex1Ind) || (i == Vertex2Ind) || (i == Vertex3Ind))
					continue;
				S1 = PMSquare(Polygon[Vertex1Ind], Polygon[Vertex2Ind], Polygon[i]);
				S2 = PMSquare(Polygon[Vertex2Ind], Polygon[Vertex3Ind], Polygon[i]);
				if (((S1 < 0) && (S2 > 0)) || ((S1 > 0) && (S2 < 0)))
					continue;
				S3 = PMSquare(Polygon[Vertex3Ind], Polygon[Vertex1Ind], Polygon[i]);
				if (((S3 >= 0) && (S2 >= 0)) || ((S3 <= 0) && (S2 <= 0)))
					return true;
			}
			return false;
		}

		int SegmentCutting(List<PointF> Polygon, PointF P1, PointF P2, ref ArrayList NPoints)
		{
			List<PointF> TempPolygon = new List<PointF>(Polygon);
			List<PointF> ConvPolygon = new List<PointF>();

			PointF NP1 = new PointF();
			PointF NP2 = new PointF();
			int begin_ind = 0;
			int cur_ind;
			int begin_ind1;
			int N = Polygon.Count;
			int Range;

			Pen pConvPolygon = new Pen(pnlCuttingRect.BackColor);
			pConvPolygon.Color = Color.FromArgb(pConvPolygon.Color.R + (pnlBackgroud.BackColor.R - pConvPolygon.Color.R) / 2,
												pConvPolygon.Color.G + (pnlBackgroud.BackColor.G - pConvPolygon.Color.G) / 2,
												pConvPolygon.Color.B + (pnlBackgroud.BackColor.B - pConvPolygon.Color.B) / 2);

			if (Square(TempPolygon) < 0)
				TempPolygon.Reverse();

			while (N >= 3)
			{
				while ((PMSquare(TempPolygon[begin_ind], TempPolygon[(begin_ind + 1) % N],
						  TempPolygon[(begin_ind + 2) % N]) < 0) ||
						  (Intersect(TempPolygon, begin_ind, (begin_ind + 1) % N, (begin_ind + 2) % N) == true))
				{
					begin_ind++;
					begin_ind %= N;
				}
				cur_ind = (begin_ind + 1) % N;
				ConvPolygon.Add(TempPolygon[begin_ind]);
				ConvPolygon.Add(TempPolygon[cur_ind]);
				ConvPolygon.Add(TempPolygon[(begin_ind + 2) % N]);

				if (cbTriag.Checked == false)
				{
					begin_ind1 = cur_ind;
					while ((PMSquare(TempPolygon[cur_ind], TempPolygon[(cur_ind + 1) % N],
									TempPolygon[(cur_ind + 2) % N]) > 0) && ((cur_ind + 2) % N != begin_ind))
					{
						if ((Intersect(TempPolygon, begin_ind, (cur_ind + 1) % N, (cur_ind + 2) % N) == true) ||
							(PMSquare(TempPolygon[begin_ind], TempPolygon[(begin_ind + 1) % N],
									  TempPolygon[(cur_ind + 2) % N]) < 0))
							break;
						ConvPolygon.Add(TempPolygon[(cur_ind + 2) % N]);
						cur_ind++;
						cur_ind %= N;
					}
				}

				Range = cur_ind - begin_ind;
				if (Range > 0)
				{
					TempPolygon.RemoveRange(begin_ind + 1, Range);
				}
				else
				{
					TempPolygon.RemoveRange(begin_ind + 1, N - begin_ind - 1);
					TempPolygon.RemoveRange(0, cur_ind + 1);
				}
				N = TempPolygon.Count;
				begin_ind++;
				begin_ind %= N;


				if ((SegmentCutting(ConvPolygon, P1, P2, ref NP1, ref NP2)) >= 0)
				{
					NPoints.Add(NP1);
					NPoints.Add(NP2);
				}

				if (checkBox1.Checked == true)
					G.DrawPolygon(pConvPolygon, (PointF[])ConvPolygon.ToArray());
				ConvPolygon.Clear();
			}


			TempPolygon.Clear();

			return 0;
		}

		bool HorizIntersec(PointF P1, PointF P2, PointF Line)
		{
			if (P1.Y > P2.Y)
			{
				if ((Line.Y < P2.Y) || (Line.Y > P1.Y))
					return false;
			}
			else
			{
				if ((Line.Y < P1.Y) || (Line.Y > P2.Y))
					return false;
			}

			float a1 = P2.Y - P1.Y;
			float b1 = P1.X - P2.X;
			float d1 = -a1 * P1.X - b1 * P1.Y;

			float b2 = Line.X;
			float d2 = -b2 * Line.Y;
			float t = -a1 * b2;

			if (t == 0)
				return false;

			float X = (b2 * d1 - b1 * d2) / t;

			if (X > Line.X)
				return false;

			return true;
		}

		int NumberOfIntersec(List<PointF> Polygon, PointF P)
		{
			int result = 0;

			for (int i = 0; i < Polygon.Count - 1; i++)
				if (HorizIntersec(Polygon[i], Polygon[i + 1], P) == true)
					result++;
			if (HorizIntersec(Polygon[Polygon.Count - 1], Polygon[0], P) == true)
				result++;

			return result;
		}

		void SegmentCutting2(List<PointF> Polygon, PointF P1, PointF P2, ref List<PointF> NPoints)
		{
			if (Polygon.Count == 0)
				return;
			PointF O = new PointF();
			bool b;
			if (Math.Abs(P2.X - P1.X) >= Math.Abs(P2.Y - P1.Y))
			{
				for (int i = 0; i < Polygon.Count - 1; i++)
					if (LineIntersect(P1, P2, Polygon[i], Polygon[i + 1], ref O) == 1)
					{
						b = false;
						for (int j = 0; j < NPoints.Count; j++)
							if (O.X < NPoints[j].X)
							{
								NPoints.Insert(j, O);
								b = true;
								break;
							}
						if (b == false)
							NPoints.Add(O);
					}
				if (LineIntersect(P1, P2, Polygon[Polygon.Count - 1], Polygon[0], ref O) == 1)
				{
					b = false;
					for (int j = 0; j < NPoints.Count; j++)
						if (O.X < NPoints[j].X)
						{
							NPoints.Insert(j, O);
							b = true;
							break;
						}
					if (b == false)
						NPoints.Add(O);
				}
				if (NPoints.Count == 0)
				{
					if (NumberOfIntersec(Polygon, P1) % 2 == 1)
					{
						NPoints.Add(P1);
						NPoints.Add(P2);
					}
					return;
				}
				if (P1.X <= NPoints[0].X)
				{
					if (NumberOfIntersec(Polygon, P1) % 2 == 1)
						NPoints.Insert(0, P1);
					if (NumberOfIntersec(Polygon, P2) % 2 == 1)
						NPoints.Add(P2);
				}
				else
				{
					if (NumberOfIntersec(Polygon, P2) % 2 == 1)
						NPoints.Insert(0, P2);
					if (NumberOfIntersec(Polygon, P1) % 2 == 1)
						NPoints.Add(P1);
				}
			}
			else
			{
				for (int i = 0; i < Polygon.Count - 1; i++)
					if (LineIntersect(P1, P2, Polygon[i], Polygon[i + 1], ref O) == 1)
					{
						b = false;
						for (int j = 0; j < NPoints.Count; j++)
							if (O.Y < (NPoints[j]).Y)
							{
								NPoints.Insert(j, O);
								b = true;
								break;
							}
						if (b == false)
							NPoints.Add(O);
					}
				if (LineIntersect(P1, P2, Polygon[Polygon.Count - 1], Polygon[0], ref O) == 1)
				{
					b = false;
					for (int j = 0; j < NPoints.Count; j++)
						if (O.Y < (NPoints[j]).Y)
						{
							NPoints.Insert(j, O);
							b = true;
							break;
						}
					if (b == false)
						NPoints.Add(O);
				}
				if (NPoints.Count == 0)
				{
					if (NumberOfIntersec(Polygon, P1) % 2 == 1)
					{
						NPoints.Add(P1);
						NPoints.Add(P2);
					}
					return;
				}
				if (P1.Y <= NPoints[0].Y)
				{
					if (NumberOfIntersec(Polygon, P1) % 2 == 1)
						NPoints.Insert(0, P1);
					if (NumberOfIntersec(Polygon, P2) % 2 == 1)
						NPoints.Add(P2);
				}
				else
				{
					if (NumberOfIntersec(Polygon, P2) % 2 == 1)
						NPoints.Insert(0, P2);
					if (NumberOfIntersec(Polygon, P1) % 2 == 1)
						NPoints.Add(P1);
				}
			}
		}

		private void UpdateLastSegment()
		{
			PointF A1 = new PointF();
			PointF A2 = new PointF();
			Pen pCuttingLine = new Pen(pnlCuttingLine.BackColor);
			Pen pLine = new Pen(pnlLine.BackColor);
			pLine.Width = 2;
			//pCuttingLine.Width = 2;
			if (Points.Count % 2 == 0)
			{
				G.DrawLine(pCuttingLine, Points[Points.Count - 2], Points[Points.Count - 1]);
				if (SegmentCutting(Polygon, Points[Points.Count - 2], Points[Points.Count - 1], ref A1, ref A2) >= 0)
					G.DrawLine(pLine, A1, A2);
			}

			pictBox.Refresh();
		}

		public bool PolygonParams(List<PointF> Polygon, out float S, out int Conv, out int SelfIntersect)
		{
			S = 0;
			Conv = 0;
			SelfIntersect = 0;
			if (Polygon.Count < 3)
				return false;
			else
			{
				S = Square(Polygon);
				Conv = Convex(Polygon);
				SelfIntersect = SelfIntersection(Polygon);
				return true;
			}
		}

		private void UpdateAll()
		{
			Brush brush = new SolidBrush(pnlBackgroud.BackColor);
			G.FillRectangle(brush, 0, 0, B.Width, B.Height);

			if (PolygonParams(Polygon, out S, out Conv, out SelfIntersect) == false)
			{
				lblBypass.Text = "---";
				lblConvex.Text = "---";
				lblSelfintersect.Text = "---";
			}
			else
			{
				if (S > 0)
					lblBypass.Text = "Против час. стрелки";
				else
					if (S < 0)
					lblBypass.Text = "По час. стрелке";

				if (Conv > 0)
					lblConvex.Text = "Да";
				else
					if (Conv < 0)
					lblConvex.Text = "Нет";

				if (SelfIntersect > 0)
					lblSelfintersect.Text = "Есть";
				else
					if (SelfIntersect < 0)
					lblSelfintersect.Text = "Нет";
			}

			PointF A1 = new PointF();
			PointF A2 = new PointF();
			Pen pCuttingLine = new Pen(pnlCuttingLine.BackColor);
			Pen pLine = new Pen(pnlLine.BackColor);
			pLine.Width = 2;

			for (int i = 0; i < (Points.Count - Points.Count % 2); i = i + 2)
			{
				if (checkBox2.Checked == true)
					G.DrawLine(pCuttingLine, Points[i], Points[i + 1]);
				if (rbConvexDivide.Checked)
				{
					if (Conv > 0)
					{
						if (SegmentCutting(Polygon, Points[i], Points[i + 1], ref A1, ref A2) >= 0)
							G.DrawLine(pLine, A1, A2);
					}
					else
					{
						if (SelfIntersect < 0)
						{
							ArrayList NPoints = new ArrayList();
							SegmentCutting(Polygon, (PointF)Points[i], (PointF)Points[i + 1], ref NPoints);
							if (NPoints.Count >= 2)
							{
								for (int j = 0; j < NPoints.Count - 1; j += 2)
									G.DrawLine(pLine, (PointF)NPoints[j], (PointF)NPoints[j + 1]);
							}
							NPoints.Clear();
						}
					}
				}
				else
				{
					List<PointF> NPoints = new List<PointF>();
					SegmentCutting2(Polygon, Points[i], Points[i + 1], ref NPoints);
					if (NPoints.Count >= 2)
					{
						for (int j = 0; j < NPoints.Count - 1; j += 2)
							G.DrawLine(pLine, NPoints[j], NPoints[j + 1]);
					}
					NPoints.Clear();
				}
			}

			Pen pCuttingRect = new Pen(pnlCuttingRect.BackColor);
			Brush bCuttingRect = new SolidBrush(pCuttingRect.Color);
			if (Polygon.Count > 0)
				G.FillEllipse(bCuttingRect, (Polygon[0]).X - 2.5f, (Polygon[0]).Y - 2.5f, 5, 5);
			if (Polygon.Count > 1)
			{
				if (PBuild == true)
					G.DrawLines(pCuttingRect, (PointF[])(Polygon.ToArray()));
				else
					G.DrawPolygon(pCuttingRect, (PointF[])(Polygon.ToArray()));
			}

			pictBox.Refresh();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			B = new Bitmap(pictBox.ClientSize.Width, pictBox.ClientSize.Height);
			pictBox.Image = B;
			G = Graphics.FromImage(B);
			Polygon = new List<PointF>();
			G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			btnUpdate_Click(null, null);
		}

		private void pictBox_MouseUp_1(object sender, MouseEventArgs e)
		{
			if ((IsCtrlDown() == true) || (rbClipper.Checked == true))
			{
				PointF P = new PointF(e.X, e.Y);
				if (e.Button == MouseButtons.Left)
				{
					if (PBuild == false)
					{
						Polygon.Clear();
						PBuild = true;
					}
					else
					{
						if (IsShiftDown() == true)
						{
							PointF PT = new PointF();
							PT = Polygon[Polygon.Count - 1];
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
					Polygon.Add(P);
				}
				else
					if (e.Button == MouseButtons.Right)
				{
					PBuild = false;
				}
				UpdateAll();
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
				}
				else
				{
					if (SecondPoint == false)
					{
						if (Math.Abs(e.X - P2.X) < Math.Abs(e.Y - P2.Y))
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
						if (Math.Abs(e.X - P1.X) < Math.Abs(e.Y - P1.Y))
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
				UpdateAll();
			}
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
			UpdateAll();
		}

		private void tbYb_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar != (char)8) && ((e.KeyChar < '0') || (e.KeyChar > '9')))
				e.KeyChar = (char)0;
		}

		private void dgvPolygon_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			Polygon.RemoveAt(e.RowIndex);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Polygon.Clear();
			UpdateAll();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Points.Clear();
			UpdateAll();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			UpdateAll();
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			UpdateAll();
		}

		private void button3_Click_1(object sender, EventArgs e)
		{
			Points.Clear();
			PointF P = new PointF();
			Random R = new Random();
			for (int i = 0; i < edtSegmentCount.Value * 2; i++)
			{
				P.X = R.Next(0, pictBox.Width);
				P.Y = R.Next(0, pictBox.Height);
				Points.Add(P);
			}
			UpdateAll();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Polygon.Clear();
			PointF P = new PointF();
			Random R = new Random();

			int X, Y;
			P.X = R.Next(0, pictBox.Width);
			P.Y = R.Next(0, pictBox.Height);
			Polygon.Add(P);
			X = (int)((PointF)Polygon[Polygon.Count - 1]).X;
			Y = (int)((PointF)Polygon[Polygon.Count - 1]).Y;
			P.X = R.Next(X - pictBox.Width / 16, (int)(X + pictBox.Width / 16));
			P.Y = R.Next(Y - pictBox.Height / 16, (int)(Y + pictBox.Height / 16));
			P.X = R.Next(0, pictBox.Width);
			P.Y = R.Next(0, pictBox.Height);
			Polygon.Add(P);

			for (int i = 2; i < edtEdgesCount.Value; i++)
			{
				P.X = R.Next(0, pictBox.Width);
				P.Y = R.Next(0, pictBox.Height);
				Polygon.Add(P);
				if (cbConvex.Checked == true)
				{
					while ((Convex(Polygon) <= 0) || (SelfIntersection(Polygon) == 1))
					{
						Polygon.RemoveAt(Polygon.Count - 1);
						P.X = R.Next(0, pictBox.Width);
						P.Y = R.Next(0, pictBox.Height);
						Polygon.Add(P);
					}
				}
				else
					while (SelfIntersection(Polygon) == 1)
					{
						Polygon.RemoveAt(Polygon.Count - 1);
						P.X = R.Next(0, pictBox.Width);
						P.Y = R.Next(0, pictBox.Height);
						Polygon.Add(P);
					}
			}
			UpdateAll();
		}

		private void cbTriag_CheckedChanged(object sender, EventArgs e)
		{
			UpdateAll();
		}

		private void rbInterlace_CheckedChanged(object sender, EventArgs e)
		{
			UpdateAll();
		}
	}
}