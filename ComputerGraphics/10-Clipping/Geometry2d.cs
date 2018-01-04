using System;
using System.Collections.Generic;

namespace Geometry2d
{
	public struct PointD
	{
		public double X, Y;

		public PointD(double x, double y)
		{
			X = x;
			Y = y;
		}

		public static implicit operator System.Drawing.PointF(PointD P)
		{
			return new System.Drawing.PointF((float)P.X, (float)P.Y);
		}

		public static implicit operator System.Drawing.Point(PointD P)
		{
			return new System.Drawing.Point((int)P.X, (int)P.Y);
		}

		/*public static implicit operator System.Drawing.Point[](PointD[] P)
        {
            System.Drawing.Point[] Points = new System.Drawing.Point[P.Length];
            for (int i = 0; i < P.Length; i++)
                Points[i] = P[i];
        }*/
	}

	public static class Geom2d
	{
		public static double DistanceSqr(PointD P1, PointD P2)
		{
			return P1.X * P1.X + P2.X * P2.X;
		}

		public static double Distance(PointD P1, PointD P2)
		{
			return Math.Sqrt(P1.X * P1.X + P2.X * P2.X);
		}

		public static double Square(PointD P1, PointD P2)
		{
			return (P2.X * P1.Y - P1.X * P2.Y);
		}

		public static double Square(PointD P1, PointD P2, PointD P3)
		{
			return ((P3.X - P1.X) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P3.Y - P1.Y));
		}

		public static int Intersection(PointD A1, PointD A2, PointD B1, PointD B2, out PointD O)
		{
			O.X = A1.X;
			O.Y = A1.Y;
			double k11 = A2.X - A1.X;
			double k12 = A2.Y - A1.Y;
			double k21 = B2.X - B1.X;
			double k22 = B2.Y - B1.Y;
			double dx = B1.X - A1.X;
			double dy = A1.Y - B1.Y;

			double d = (k22 * k11 - k21 * k12);
			if (d == 0)
				return -1;

			double t1 = (k22 * dx + k21 * dy) / d;
			if ((t1 < 0) || (t1 > 1))
				return 0;

			double t2 = (k12 * dx + k11 * dy) / d;
			if ((t2 < 0) || (t2 > 1))
				return 0;

			O.X += k11 * t1;
			O.Y += k12 * t1;
			return 1;
		}

		public static int SegmentLineIntersect(PointD S1, PointD S2, PointD L1, PointD L2, out PointD O)
		{
			O.X = S1.X;
			O.Y = S1.Y;
			double k11 = S2.X - S1.X;
			double k12 = S2.Y - S1.Y;
			double k21 = L2.X - L1.X;
			double k22 = L2.Y - L1.Y;
			double dx = L1.X - S1.X;
			double dy = S1.Y - L1.Y;

			double d = (k22 * k11 - k21 * k12);
			if (d == 0)
				return -1;

			double t1 = (k22 * dx + k21 * dy) / d;
			if ((t1 < 0) || (t1 > 1))
				return 0;

			O.X += k11 * t1;
			O.Y += k12 * t1;
			return 1;
		}

		public static bool LineIntersection(PointD A1, PointD A2, PointD B1, PointD B2, out PointD O)
		{
			O.X = A1.X;
			O.Y = A1.Y;
			double k11 = A2.X - A1.X;
			double k12 = A2.Y - A1.Y;
			double k21 = B2.X - B1.X;
			double k22 = B2.Y - B1.Y;
			double dx = B1.X - A1.X;
			double dy = A1.Y - B1.Y;

			double d = (k22 * k11 - k21 * k12);
			if (d == 0)
				return false;

			double t1 = (k22 * dx + k21 * dy) / d;
			O.X += k11 * t1;
			O.Y += k12 * t1;
			return true;
		}

		public static bool HorizIntersec(PointD P1, PointD P2, PointD Line)
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

			double a1 = P2.Y - P1.Y;
			double b1 = P1.X - P2.X;
			double d1 = -a1 * P1.X - b1 * P1.Y;

			double b2 = Line.X;
			double d2 = -b2 * Line.Y;
			double t = -a1 * b2;

			if (t == 0)
				return false;

			double X = (b2 * d1 - b1 * d2) / t;

			if (X > Line.X)
				return false;

			return true;
		}


		public static int NumberOfIntersec(List<PointD> Polygon, PointD P)
		{
			int result = 0;

			for (int i = 0; i < Polygon.Count - 1; i++)
				if (HorizIntersec(Polygon[i], Polygon[i + 1], P) == true)
					result++;
			if (HorizIntersec(Polygon[Polygon.Count - 1], Polygon[0], P) == true)
				result++;

			return result;
		}

		public static bool PointInPolygon(PointD P, List<PointD> Polygon)
		{
			if (NumberOfIntersec(Polygon, P) % 2 == 1)
				return true;
			else
				return false;
		}
	}

	/*public class ConvexPolygonException
    {
        public ConvexPolygonException() : base() { }

        public ConvexPolygonException(string str) : base(str) { }

        public override string ToString()
        {
            return Message;
        }
    }*/

	public class Object2d
	{

	}

	public class PointDArray : Object2d
	{
		public List<PointD> Vertexes;
		protected int VertexCount;

		public PointDArray()
		{
			Vertexes = new List<PointD>();
			VertexCount = 0;
		}

		public PointDArray(Polygon P)
		{
			Vertexes = new List<PointD>();
			for (int i = 0; i < P.Count; i++)
				Vertexes.Add(P[i]);
			VertexCount = Vertexes.Count;
		}

		public PointD this[int index]
		{
			get
			{
				return Vertexes[index % VertexCount];
			}
			set
			{
				Vertexes[index % VertexCount] = value;
			}
		}

		public int Count
		{
			get
			{
				return VertexCount;
			}
		}

		public virtual void Add(PointD Point)
		{
			Vertexes.Add(Point);
			VertexCount++;
		}

		public virtual void RemoveLast()
		{
			VertexCount--;
			Vertexes.RemoveAt(VertexCount);
		}

		public virtual void RemoveRange(int begin_ind, int end_ind)
		{
			end_ind %= VertexCount;
			begin_ind %= VertexCount;
			int Range = end_ind - begin_ind;
			if (Range > 0)
			{
				Vertexes.RemoveRange(begin_ind + 1, Range);
			}
			else
			{
				Vertexes.RemoveRange(begin_ind + 1, VertexCount - begin_ind - 1);
				Vertexes.RemoveRange(0, end_ind + 1);
			}
			VertexCount = Vertexes.Count;
		}

		public virtual void Clear()
		{
			Vertexes.Clear();
			VertexCount = 0;
		}

		public virtual void Reverse()
		{
			Vertexes.Reverse();
		}
	}

	public class Polygon : PointDArray
	{
		int convexity;
		int self_intersection;
		double square2;
		double perimeter;

		protected List<List<PointD>> ConvPolygons;

		public Polygon() : base()
		{
			VertexCount = 0;
			square2 = 0;
			convexity = 0;
			self_intersection = 0;
			perimeter = 0;
		}

		public Polygon(Polygon P)
		{
			Vertexes = new List<PointD>(P.Vertexes);
			VertexCount = 0;
			square2 = 0;
			convexity = 0;
			self_intersection = 0;
			perimeter = 0;
		}


		protected void UpdateSCS()
		{
			if (VertexCount == 3)
			{
				convexity = 1;
				self_intersection = -1;
				square2 = Geom2d.Square(Vertexes[0], Vertexes[1], Vertexes[2]);
			}
			else
			if (VertexCount > 3)
			{
				double S = Geom2d.Square(Vertexes[VertexCount - 2], Vertexes[VertexCount - 1], Vertexes[0]);
				if (((S < 0) && (square2 > 0)) || ((S > 0) && (square2 < 0)))
					convexity = -1;
				else
					if ((S == 0) && (square2 == 0))
					convexity = 0;

				square2 += S;


				PointD O;
				for (int i = 1; i <= VertexCount - 3; i++)
					if (Geom2d.Intersection(Vertexes[i], Vertexes[i + 1], Vertexes[VertexCount - 1], Vertexes[0], out O) == 1)
					{
						self_intersection = 1;
						break;
					}

				/*perimeter -= Geom2d.Distance(Vertexes[VertexCount - 1], Vertexes[0]);
                perimeter += Geom2d.Distance(Vertexes[VertexCount - 1], Vertexes[VertexCount]);
                perimeter += Geom2d.Distance(Vertexes[VertexCount], Vertexes[0]);*/
			}

		}

		public void UpdatePolygon()
		{
			if (VertexCount >= 3)
			{
				int i;
				convexity = 1;
				square2 = Geom2d.Square(Vertexes[VertexCount - 1], Vertexes[0], Vertexes[1]);
				double S;
				for (i = 0; i <= VertexCount - 3; i++)
				{
					S = Geom2d.Square(Vertexes[i], Vertexes[i + 1], Vertexes[i + 2]);
					if (((square2 < 0) && (S > 0)) || ((square2 > 0) && (S < 0)))
						convexity = -1;
					else
						if ((S == 0) && (square2 == 0))
						convexity = 0;
					square2 += S;
				}

				S = Geom2d.Square(Vertexes[VertexCount - 2], Vertexes[VertexCount - 1], Vertexes[0]);
				if (((square2 < 0) && (S > 0)) || ((square2 > 0) && (S < 0)))
					convexity = -1;
				else
					if ((S == 0) && (square2 == 0))
					convexity = 0;
				square2 += S;

				/*int High = VertexCount - 1;
                PointD O = new PointD();
                for (i = 0; i < High; i++)
                    for (int j = i + 2; j < High; j++)
                        if (Geom2d.Intersection(Vertexes[i], Vertexes[i + 1], Vertexes[j], Vertexes[j + 1], out O) == 1)
                        {
                            self_intersection = 1;
                            break;
                        }
                if (self_intersection != 1)
                    for (i = 1; i < High - 1; i++)
                        if (Geom2d.Intersection(Vertexes[i], Vertexes[i + 1], Vertexes[High], Vertexes[0], out O) == 1)
                            self_intersection = 1;*/
			}
			else
			{
				square2 = 0;
				convexity = 0;
				self_intersection = 0;
			}

		}

		public double Square
		{
			get
			{
				return square2 / 2;
			}
		}


		public int Convexity
		{
			get
			{
				return convexity;
			}
		}

		public int SelfIntersection
		{
			get
			{
				return self_intersection;
			}
		}

		public double Perimeter
		{
			get
			{
				return perimeter;
			}
		}

		protected bool Intersect(int Vertex1Ind, int Vertex2Ind, int Vertex3Ind)
		{
			double S1, S2, S3;
			Vertex1Ind %= VertexCount;
			Vertex2Ind %= VertexCount;
			Vertex3Ind %= VertexCount;
			for (int i = 0; i < VertexCount; i++)
			{
				if ((i == Vertex1Ind) || (i == Vertex2Ind) || (i == Vertex3Ind))
					continue;
				S1 = Geom2d.Square(Vertexes[Vertex1Ind], Vertexes[Vertex2Ind], Vertexes[i]);
				S2 = Geom2d.Square(Vertexes[Vertex2Ind], Vertexes[Vertex3Ind], Vertexes[i]);
				if (((S1 < 0) && (S2 > 0)) || ((S1 > 0) && (S2 < 0)))
					continue;
				S3 = Geom2d.Square(Vertexes[Vertex3Ind], Vertexes[Vertex1Ind], Vertexes[i]);
				if (((S3 >= 0) && (S2 >= 0)) || ((S3 <= 0) && (S2 <= 0)))
					return true;
			}
			return false;
		}


		public override void Add(PointD Point)
		{
			Vertexes.Add(Point);
			VertexCount++;
			UpdateSCS();
		}

		public override void RemoveLast()
		{
			VertexCount--;
			Vertexes.RemoveAt(VertexCount);
			UpdatePolygon();
		}

		public override void RemoveRange(int begin_ind, int end_ind)
		{
			base.RemoveRange(begin_ind, end_ind);
			UpdatePolygon();
		}

		public override void Clear()
		{
			base.Clear();
			convexity = 0;
			self_intersection = 0;
			square2 = 0;
		}

		public override void Reverse()
		{
			base.Reverse();
			square2 = -square2;
		}

		public void ConvexDivide(bool Triangulation)
		{
			PointDArray TempPolygon = new PointDArray(this);
			ConvPolygons = new List<List<PointD>>();
			int CPH = -1;

			int begin_ind = 0;
			int cur_ind;

			//if (Square(TempPolygon) < 0)
			//    TempPolygon.Reverse();

			while (TempPolygon.Count >= 3)
			{
				while ((Geom2d.Square(TempPolygon[begin_ind], TempPolygon[begin_ind + 1], TempPolygon[begin_ind + 2]) < 0) ||
					  (Intersect(begin_ind, begin_ind + 1, begin_ind + 2) == true))
					begin_ind++;

				cur_ind = begin_ind + 1;
				ConvPolygons.Add(new List<PointD>());
				CPH++;
				ConvPolygons[CPH].Add(TempPolygon[begin_ind]);
				ConvPolygons[CPH].Add(TempPolygon[cur_ind]);
				ConvPolygons[CPH].Add(TempPolygon[begin_ind + 2]);

				if (Triangulation == false)
				{
					while ((Geom2d.Square(TempPolygon[cur_ind], TempPolygon[cur_ind + 1], TempPolygon[cur_ind + 2]) > 0) && (cur_ind + 2 != begin_ind))
					{
						if ((Intersect(begin_ind, cur_ind + 1, cur_ind + 2) == true) ||
							(Geom2d.Square(TempPolygon[begin_ind], TempPolygon[begin_ind + 1], TempPolygon[cur_ind + 2]) < 0))
							break;
						ConvPolygons[CPH].Add(TempPolygon[cur_ind + 2]);
						cur_ind++;
					}
				}

				TempPolygon.RemoveRange(begin_ind, cur_ind);
				begin_ind++;
			}
		}
	}
}