using System;
using System.Drawing;
using Geometry2d;


namespace Graphics2d
{
    public class GraphPolygon : Polygon
    {
        public Pen pLine;
        public Brush bFill;
        public bool LineVisible;
        public bool FillVisible;
        public Pen pDivideColor;
        private Graphics G;
        private Bitmap B;
        public bool Closed = false;

        public GraphPolygon(Bitmap _B, Graphics _G, Pen _pLine)
        {
            B = _B;
            G = _G;
            pLine = _pLine;
            LineVisible = true;
            FillVisible = false;
            Closed = false;
        }

        public GraphPolygon(Bitmap _B, Graphics _G, Pen _pLine, Brush _bFill, bool _LineVisible, bool _FillVisible)
        {
            B = _B;
            G = _G;
            pLine = _pLine;
            bFill = _bFill;
            LineVisible = _LineVisible;
            FillVisible = _FillVisible;
        }


        public void Draw()
        {
            if (Vertexes.Count > 1)
            {
                Point[] Points = new Point[Vertexes.Count];
                for (int i = 0; i < Points.Length; i++)
                    Points[i] = Vertexes[i];

                if (FillVisible)
                    G.FillPolygon(bFill, Points);
                if (LineVisible)
                {
                    G.DrawLines(pLine, Points);
                    if (Closed == true)
                        G.DrawLine(pLine, Points[Points.Length - 1], Points[0]);
                }
            }
        }

        public void DrawLastSegment()
        {
            if ((VertexCount > 1) && (LineVisible))
                G.DrawLine(pLine, Vertexes[VertexCount - 2], Vertexes[VertexCount - 1]);
        }

        public void DrawBeginEndSegment()
        {
            if ((VertexCount > 1) && (LineVisible))
                G.DrawLine(pLine, Vertexes[VertexCount - 1], Vertexes[0]);
        }

        public void DrawConvDivide()
        {
            if (ConvPolygons.Count > 0)
            {
                int j;
                for (int i = 0; i < ConvPolygons.Count; i++)
                {
                    for (j = 0; j < ConvPolygons[i].Count - 1; j++)
                        G.DrawLine(pDivideColor, ConvPolygons[i][j], ConvPolygons[i][j + 1]);
                    G.DrawLine(pDivideColor, ConvPolygons[i][j], ConvPolygons[i][0]);
                }
                    
            }
        }


    }
}
