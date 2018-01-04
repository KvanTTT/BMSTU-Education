namespace Geometry2d
{
	public struct PointD
    {
        public double X, Y;
    }

    public static class Geom2d
    {
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
            O.X = 0;
            O.Y = 0;
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

            O.X = A1.X + k11 * t1;
            O.Y = A1.Y + k12 * t1;
            return 1;
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
    }

    public class Object2d
    {

    }

    public class PointDArray : Object2d
    {
        protected ArrayList Vertexes;
        protected int FVertexCount;

        public PointDArray() { }

        public PointD this[int index]
        {
            get
            {
                return (PointD)Vertexes[index % VertexCount];
            }
            set
            {
                Vertexes[index % VertexCount] = (PointD)value;
            }
        }

        public int Length
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
    }

    public class Polygon : PointDArray
    {
        bool FConvexity;
        bool FConvexityDefined;
        double FSquare;
        double FDoubleSquare;
        bool FSquareDefined;


        public Polygon()
        {
            FVertexCount = 0;
            FSquare = 0;
            FConvexity = true;
            FConvexityDefined = false;
            FSquareDefined = false;
        }

        public PointD this[int index]
        {
            get
            {
                return (PointD)Vertexes[index % VertexCount];
            }
            set
            {
                Vertexes[index % VertexCount] = (PointD)value;
            }
        }

        protected void GetConvexity()
        {
            if (FVertexCount > 3)
            {
                double S = Square((PointD)Vertexes[0], (PointD)Vertexes[1], (PointD)Vertexes[2]);
                for (int i = 0; i < FVertexCount - 2; i++)
                {
                    S1 = Square((PointD)Vertexes[i], (PointD)Vertexes[i + 1], (PointD)Vertexes[i + 2]);
                    if (((S1 < 0) && (S > 0)) || ((S1 > 0) && (S < 0)))
                        FConvexity = false;

                }
            }
        }

        protected void GetSquareConvexity()
        {
            FDoubleSquare = 0;
            FConvexityDefined = false;
            FConvexity = true;
            if (FVertexCount >= 3)
            {                
                double S = Square((PointD)Vertexes[0], (PointD)Vertexes[1], (PointD)Vertexes[2]);
                double S1;
                for (int i = 1; i < FVertexCount - 2; i++)
                {
                    S1 = Geom2d.Square((PointD)Vertexes[i], (PointD)Vertexes[i + 1], (PointD)Vertexes[i + 2]);
                    if ((FConvexityDefined == false) && (((S1 < 0) && (S > 0)) || ((S1 > 0) && (S < 0))))
                    {
                        FConvexity = false;
                        FConvexityDefined = true;
                    }
                    FDoubleSquare += S1;
                    S = S1;
                }

                S1 += Geom2d.Square((PointD)Vertexes[FVertexCount - 2], (PointD)Vertexes[FVertexCount - 1], (PointD)Vertexes[0]);
                if ((FConvexityDefined == false) && (((S1 < 0) && (S > 0)) || ((S1 > 0) && (S < 0))))
                {
                    FConvexity = false;
                    FConvexityDefined = true;
                }
                FDoubleSquare += S1;
                S = S1;
                
                S1 = Geom2d.Square((PointD)Vertexes[FVertexCount - 1], (PointD)Vertexes[0], (PointD)Vertexes[1]);
                if ((FConvexityDefined == false) && (((S1 < 0) && (S > 0)) || ((S1 > 0) && (S < 0))))
                    FConvexity = false;
                
                FDoubleSquare += S1;
                FSquare = FDoubleSquare * 0.5;
            }
            else
            {
                FDoubleSquare = 0;
                FSquare = 0;
                FConvexity = true;
            }

            FSquareConvexityDefined = true;
        }

        public double Square
        {
            get
            {
                return square;                    
            }
        }

        public double Convexity
        {
            get
            {
                return convexity;
            }
        }

        public void Add(PointD Point)
        {
            Vertexes.Add(Point);

            if (VertexCount >= 3)
            {
                double S1 = Geom2d.Square(Vertexes[VertexCount - 2], Vertexes[VertexCount - 1], Vertexes[VertexCount]);
                if (((S1 < 0) && (S > 0)) || ((S1 > 0) && (S < 0)))
                    FConvexity = false;

                S += S1;
            }

            VertexCount++;
        }

        public void RemoveLast()
        {
            if (VertexCount >= 3)
            {
                double S1 = Geom2d.Square(Vertexes[VertexCount - 3], Vertexes[VertexCount - 2], Vertexes[VertexCount - 1]);
                S -= S1;
            }

            VertexCount--;
            Vertexes.RemoveAt(VertexCount);
        }
     
    }*/
}