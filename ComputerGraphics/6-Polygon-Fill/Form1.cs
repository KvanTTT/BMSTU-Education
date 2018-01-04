using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using Utils;

namespace PolygonFill
{
    public partial class Form1 : Form
    {
        bool PBuild = false;
        bool drawable = false;
        bool PointFillPoint = false;
        bool DiagramBuild = false;
        List<Point> Polygon;
        List<int> NextPolygon;
        Bitmap B;
        Graphics G;
        Point FillPoint;

        public Form1()
        {
            InitializeComponent();
        }

        // Graphics Utils
        private void Clear()
        {
            Brush brush = new SolidBrush(pnlBackground.BackColor);
            G.FillRectangle(brush, 0, 0, B.Width, B.Height);
        }

        public void UpdateLastSegment()
        {
            if (Polygon.Count > 1)
            {
                if (PBuild == false)
                {
                    Pen pLine = new Pen(pnlLine.BackColor);
                    G.DrawLine(pLine, Polygon[Polygon.Count - 1], Polygon[NextPolygon[NextPolygon.Count - 2]]);
                }
                else
                    if (drawable == true)
                    {
                        Pen pLine = new Pen(pnlLine.BackColor);
                        G.DrawLine(pLine, Polygon[Polygon.Count - 2], Polygon[Polygon.Count - 1]);
                    }
            }

            pictureBox1.Refresh();
        }

        private void UpdatePolygon(Color C)
        {
            Pen pFill = new Pen(C);
            pFill.Width = 1;

            if (Polygon.Count > 1)
            {
                for (int i = 0; i <= NextPolygon.Count - 2; i++)
                {
                    for (int j = NextPolygon[i]; j <= NextPolygon[i + 1] - 2; j++)
                        G.DrawLine(pFill, (Point)Polygon[j], Polygon[j + 1]);
                    G.DrawLine(pFill, (Point)Polygon[NextPolygon[i + 1] - 1], (Point)Polygon[NextPolygon[i]]);
                }
                /*for (int j = NextPolygon[NextPolygon.Count - 1]; j <= Polygon.Count - 2; j++)
                    G.DrawLine(pFill, Polygon[j], Polygon[j + 1]);
                G.DrawLine(pFill, Polygon[Polygon.Count - 1], Polygon[NextPolygon[NextPolygon.Count - 2]]);*/
            }
        }

        private void UpdatePolygon(Color C, bool Xor)
        {
            Pen pFill = new Pen(C);
            pFill.Width = 1;

            if (Polygon.Count > 1)
            {
                for (int i = 0; i <= NextPolygon.Count - 2; i++)
                {
                    BresenhamInt(Polygon[NextPolygon[i]], Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i] + 2],
                        Polygon[NextPolygon[i + 1] - 1], Xor);
                    for (int j = NextPolygon[i]+1; j <= NextPolygon[i + 1] - 3; j++)
                        BresenhamInt(Polygon[j], Polygon[j + 1], Polygon[j + 2], Polygon[j - 1], Xor);
                    BresenhamInt(Polygon[NextPolygon[i + 1] - 2], Polygon[NextPolygon[i + 1] - 1],
                                 Polygon[NextPolygon[i]], Polygon[NextPolygon[i + 1] - 3], Xor);
                    BresenhamInt(Polygon[NextPolygon[i + 1] - 1], Polygon[NextPolygon[i]],
                                 Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i + 1] - 2], Xor);
                }
            }
        }

        private void BresenhamInt(Point P1, Point P2, Point EP, Point BP, bool XOR)
        {
            int dy = P2.Y - P1.Y;
            int dye = P2.Y - EP.Y;          

            if (dy == 0)
            {
                int dyb = P1.Y - BP.Y;
                if (!(((dye <= 0) && (dyb >= 0)) || ((dye >= 0) && (dyb <= 0))))
                    if (XOR)
                    {
                        if (CompareColors(B.GetPixel(P2.X, P2.Y), pnlFill.BackColor))
                            B.SetPixel(P2.X, P2.Y, pnlBackground.BackColor);
                        else
                            B.SetPixel(P2.X, P2.Y, pnlFill.BackColor);
                    }
                    else
                        B.SetPixel(P2.X, P2.Y, pnlFill.BackColor);
                return;
            }
            int zy = Math.Sign(dy);
            int dxi = P2.X - P1.X;
            int dxa = Math.Abs(dxi);
            int dya = Math.Abs(dy);
            int y = P1.Y;

            if (dya >= dxa)
            {
                int x = P1.X;
                int zx = Math.Sign(dxi);

                int dy2 = dya << 1;
                int dx2 = dxa << 1;
                int e = dx2 - dya;
                if (!(((dye < 0) && (dy < 0)) || ((dye > 0) && (dy > 0))))
                    dya--;
                for (int k = 0; k <= dya; k++)
                {
                    if (XOR)
                    {
                        if (CompareColors(B.GetPixel(x, y), pnlFill.BackColor))
                            B.SetPixel(x, y, pnlBackground.BackColor);
                        else
                            B.SetPixel(x, y, pnlFill.BackColor);
                    }
                    else
                        B.SetPixel(x, y, pnlFill.BackColor);

                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                }
            }
            else
            {
                float dx = (float)dxi / (float)dya;
                float x = P1.X;

                for (int k = y; k != P2.Y; k += zy)
                {
                    if (XOR)
                    {
                        if (CompareColors(B.GetPixel((int)Math.Round(x), k), pnlFill.BackColor))
                            B.SetPixel((int)Math.Round(x), k, pnlBackground.BackColor);
                        else
                            B.SetPixel((int)Math.Round(x), k, pnlFill.BackColor);
                    }
                    else
                        B.SetPixel((int)Math.Round(x), k, pnlFill.BackColor);
                    x += dx;
                }
                if (((dye < 0) && (dy < 0)) || ((dye > 0) && (dy > 0)))
                {
                    if (XOR)
                    {
                        if (CompareColors(B.GetPixel(P2.X, P2.Y), pnlFill.BackColor))
                            B.SetPixel(P2.X, P2.Y, pnlBackground.BackColor);
                        else
                            B.SetPixel(P2.X, P2.Y, pnlFill.BackColor);
                    }
                    else
                        B.SetPixel(P2.X, P2.Y, pnlFill.BackColor);
                }

            }
        }


        public void UpdateAll()
        {
            Clear();
            UpdatePolygon(pnlLine.BackColor);
        }

        public bool CompareColors(Color C1, Color C2)
        {
            return ((C1.R == C2.R) && (C1.G == C2.G) && (C1.B == C2.B));
        }

        // Fill Utils
        private void HorLine(int X1, int X2, int Y)
        {
            for (int X = X1; X <= X2; X++)
                if (B.GetPixel(X, Y).ToArgb() == pnlFill.BackColor.ToArgb())
                    B.SetPixel(X, Y, pnlBackground.BackColor);
                else
                    B.SetPixel(X, Y, pnlFill.BackColor);
        }

        private void FillRight(Point P1, Point P2, Point EP, Point BP, int XMax)
        {
            int Count = 0;
            int dy = P2.Y - P1.Y;
            int dye = EP.Y - P2.Y;
            int dyb = BP.Y - P1.Y; 
            if (dy == 0)
            {
                if (((dye <= 0) && (dyb >= 0)) || ((dye >= 0) && (dyb <= 0)))
                {
                    HorLine(P1.X, XMax, P1.Y);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                }                
                return;
            }
            int zy = Math.Sign(dy);
            int dxi = P2.X - P1.X;
            int dxa = Math.Abs(dxi);
            int dya = Math.Abs(dy);
            int y = P1.Y;

            if (dya >= dxa)
            {
                int x = P1.X;
                int zx = Math.Sign(dxi);

                int dy2 = dya << 1;
                int dx2 = dxa << 1;
                int e = dx2 - dya;
                if (dyb == 0)
                {
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                    dya--;
                }
                for (int k = 0; k < dya; k++)
                {
                    HorLine(x, XMax, y);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                }
            }
            else
            {
                float dx = (float)dxi / (float)dya;
                float x = P1.X;

                if (dyb == 0)
                {
                    y += zy;
                    x += dx;
                }
                for (int k = y; k != P2.Y; k += zy)
                {
                    HorLine((int)Math.Round(x), XMax, k);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                    x += dx;
                }
            }

            if (((dy < 0) && (dye > 0)) || ((dy > 0) && (dye < 0)))
            {
                HorLine(P2.X, XMax, P2.Y);
                Application.DoEvents();
                if (cbDelay.Checked)
                {
                    Count++;
                    if (Count == tbDelay.Value)
                    {
                        Count = 0;
                        pictureBox1.Refresh();
                    }
                }
            }
        }

        private void FillRightPartition(Point P1, Point P2, Point EP, Point BP, int XPartiton)
        {
            int Count = 0;
            int dy = P2.Y - P1.Y;
            int dye = EP.Y - P2.Y;
            int dyb = BP.Y - P1.Y;
            if (dy == 0)
            {
                if (((dye <= 0) && (dyb >= 0)) || ((dye >= 0) && (dyb <= 0)))
                {
                    if (P1.X <= XPartiton)
                        HorLine(P1.X, XPartiton - 1, P1.Y);
                    else
                        HorLine(XPartiton, P1.X, P1.Y);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                }
                return;
            }
            int zy = Math.Sign(dy);
            int dxi = P2.X - P1.X;
            int dxa = Math.Abs(dxi);
            int dya = Math.Abs(dy);
            int y = P1.Y;

            if (dya >= dxa)
            {
                int x = P1.X;
                int zx = Math.Sign(dxi);

                int dy2 = dya << 1;
                int dx2 = dxa << 1;
                int e = dx2 - dya;
                if (dyb == 0)
                {
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                    dya--;
                }
                for (int k = 0; k < dya; k++)
                {
                    if (x <= XPartiton)
                        HorLine(x, XPartiton - 1, y);
                    else
                        HorLine(XPartiton, x, y);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                }
            }
            else
            {
                float dx = (float)dxi / (float)dya;
                float x = P1.X;

                if (dyb == 0)
                {
                    y += zy;
                    x += dx;
                }
                for (int k = y; k != P2.Y; k += zy)
                {
                    if (x <= XPartiton)
                        HorLine((int)Math.Round(x), XPartiton - 1, k);
                    else
                        HorLine(XPartiton, (int)Math.Round(x), k);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                    x += dx;
                }
            }

            if (((dy < 0) && (dye > 0)) || ((dy > 0) && (dye < 0)))
            {
                if (P2.X <= XPartiton)
                    HorLine(P2.X, XPartiton - 1, P2.Y);
                else
                    HorLine(XPartiton, P2.X, P2.Y);
                Application.DoEvents();
                if (cbDelay.Checked)
                {
                    Count++;
                    if (Count == tbDelay.Value)
                    {
                        Count = 0;
                        pictureBox1.Refresh();
                    }
                }
            }
        }

        private void AddVertexes(Point P1, Point P2, Point EP, Point BP, int YMin, List<List<int>> Coords)
        {
            // written by KvanTTT
            // It's optimized algorithm for adding Horisontal and Segment(P1, P2) Intersection Points to List
            int dy = P2.Y - P1.Y;
            int dye = EP.Y - P2.Y;
            int dyb = BP.Y - P1.Y; 
            if (dy == 0)
            {                
                if (((dye <= 0) && (dyb >= 0)) || ((dye >= 0) && (dyb <= 0)))
                    Coords[P2.Y - YMin].Add(P2.X);
                return;
            }            
            int zy = Math.Sign(dy);
            int dxi = P2.X - P1.X;
            int dxa = Math.Abs(dxi);
            int dya = Math.Abs(dy);
            int y = P1.Y;

            if (dya >= dxa)
            {                
                int x = P1.X;
                int zx = Math.Sign(dxi);

                int dy2 = dya << 1;
                int dx2 = dxa << 1;
                int e = dx2 - dya;
                if (dyb == 0)
                {
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                    dya--;
                }
                for (int k = 0; k < dya; k++)
                {
                    Coords[y - YMin].Add(x);
                    if (e >= 0)
                    {
                        x += zx;
                        e -= dy2;
                    }
                    y += zy;
                    e += dx2;
                }
            }
            else
            {
                /*       
                if (dyb == 0)
                {
                    y += zy;
                    x += dx;
                }
                for (int k = y; k != P2.Y; k += zy)
                {
                    Coords[k - YMin].Add(x);
                    if (e >= 0)
                    {
                        k += zk;
                        e -= dx2;
                    }
                    x += zx;
                    e += dy2;
                }
                 */
                float dx = (float)dxi / (float)dya;
                float x = P1.X;

                if (dyb == 0)
                {
                    y += zy;
                    x += dx;
                }
                for (int k = y; k != P2.Y; k += zy)
                {
                    Coords[k - YMin].Add((int)Math.Round(x));
                    x += dx;
                }
            }

            if (((dy < 0) && (dye > 0)) || ((dy > 0) && (dye < 0)))
                Coords[P2.Y - YMin].Add(P2.X);
        }


        // 1
        private void EdgeSortedList()
        {
            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            Clear();
            UpdatePolygon(pnlFill.BackColor, false);

            List<List<int>> Coords = new List<List<int>>();

            int YMin = 99999, YMax = -99999;
            int i, j, Count = 0;
            for (i = 0; i < Polygon.Count; i++)
            {
                if (Polygon[i].Y < YMin)
                    YMin = Polygon[i].Y;
                if (Polygon[i].Y > YMax)
                    YMax = Polygon[i].Y;
            }

            Coords.Capacity = YMax - YMin + 1;
            for (i = 0; i < Coords.Capacity; i++)
                Coords.Add(new List<int>());

            for (i = 0; i <= NextPolygon.Count - 2; i++)
            {
                AddVertexes(Polygon[NextPolygon[i]], Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i] + 2], 
                    Polygon[NextPolygon[i + 1] - 1], YMin, Coords);
                for (j = NextPolygon[i]+1; j <= NextPolygon[i + 1] - 3; j++)
                    AddVertexes(Polygon[j], Polygon[j + 1], Polygon[j + 2], Polygon[j - 1], YMin, Coords);
                AddVertexes(Polygon[NextPolygon[i + 1] - 2], Polygon[NextPolygon[i + 1] - 1], 
                            Polygon[NextPolygon[i]], Polygon[NextPolygon[i + 1] - 3], YMin, Coords);
                AddVertexes(Polygon[NextPolygon[i + 1] - 1], Polygon[NextPolygon[i]], 
                            Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i + 1] - 2], YMin, Coords);
            }

            Pen pFill = new Pen(pnlFill.BackColor);
            for (i = 0; i < Coords.Count; i++)
            {
                Coords[i].Sort();
                for (j = 0; j < Coords[i].Count - 1; j += 2)
                {
                    G.DrawLine(pFill, Coords[i][j], i + YMin, Coords[i][j + 1], i + YMin);
                    Application.DoEvents();
                    if (cbDelay.Checked)
                    {
                        Count++;
                        if (Count == tbDelay.Value)
                        {
                            Count = 0;
                            pictureBox1.Refresh();
                        }
                    }
                }
            }

            if (cbContour.Checked)
                UpdatePolygon(pnlLine.BackColor);

            pictureBox1.Refresh();

            thrd.Priority = ThreadPriority.Normal;
        }

        // 2
        private void EdgeFill()
        {
            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            Clear();
            UpdatePolygon(pnlFill.BackColor, true);

            int i, j;

            int XMax = -9999999;
            for (i = 0; i < Polygon.Count; i++)
                XMax = Math.Max(Polygon[i].X, XMax);

            for (i = 0; i <= NextPolygon.Count - 2; i++)
            {
                FillRight(Polygon[NextPolygon[i]], Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i] + 2],
                    Polygon[NextPolygon[i + 1] - 1], XMax);
                for (j = NextPolygon[i] + 1; j <= NextPolygon[i + 1] - 3; j++)
                    FillRight(Polygon[j], Polygon[j + 1], Polygon[j + 2], Polygon[j - 1], XMax);
                FillRight(Polygon[NextPolygon[i + 1] - 2], Polygon[NextPolygon[i + 1] - 1],
                            Polygon[NextPolygon[i]], Polygon[NextPolygon[i + 1] - 3], XMax);
                FillRight(Polygon[NextPolygon[i + 1] - 1], Polygon[NextPolygon[i]],
                            Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i + 1] - 2], XMax);
            }

            if (cbContour.Checked)
                UpdatePolygon(pnlLine.BackColor);

            pictureBox1.Refresh();
            thrd.Priority = ThreadPriority.Normal;
        }

        // 3
        private void EdgeFillWithPartition()
        {
            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            Clear();
            UpdatePolygon(pnlFill.BackColor, true);

            int i, j;
            int XPartiton = 0;
            for (i = 0; i < Polygon.Count; i++)
                XPartiton += Polygon[i].X;
            XPartiton /= Polygon.Count;

            for (i = 0; i <= NextPolygon.Count - 2; i++)
            {
                FillRightPartition(Polygon[NextPolygon[i]], Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i] + 2],
                    Polygon[NextPolygon[i + 1] - 1], XPartiton);
                for (j = NextPolygon[i] + 1; j <= NextPolygon[i + 1] - 3; j++)
                    FillRightPartition(Polygon[j], Polygon[j + 1], Polygon[j + 2], Polygon[j - 1], XPartiton);
                FillRightPartition(Polygon[NextPolygon[i + 1] - 2], Polygon[NextPolygon[i + 1] - 1],
                            Polygon[NextPolygon[i]], Polygon[NextPolygon[i + 1] - 3], XPartiton);
                FillRightPartition(Polygon[NextPolygon[i + 1] - 1], Polygon[NextPolygon[i]],
                            Polygon[NextPolygon[i] + 1], Polygon[NextPolygon[i + 1] - 2], XPartiton);
            }

            if (cbContour.Checked)
                UpdatePolygon(pnlLine.BackColor);

            pictureBox1.Refresh();

            thrd.Priority = ThreadPriority.Normal;
        }

        // 4
        private void EdgeListFlag()
        {
            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            Clear();
            UpdatePolygon(pnlFill.BackColor, true);

            int Count = 0;
            int XMin = 99999, XMax = -99999;
            int YMin = 99999, YMax = -99999;
            int i, j;

            for (i = 0; i < Polygon.Count; i++)
            {
                if (Polygon[i].Y < YMin)
                    YMin = Polygon[i].Y;
                if (Polygon[i].Y > YMax)
                    YMax = Polygon[i].Y;
                if (Polygon[i].X < XMin)
                    XMin = Polygon[i].X;
                if (Polygon[i].X > XMax)
                    XMax = Polygon[i].X;
            }

            bool InPolygon;
            for (j = YMin; j <= YMax; j++)
            {
                InPolygon = false;
                for (i = XMin; i <= XMax; i++)
                {
                    if (CompareColors(B.GetPixel(i, j), pnlBackground.BackColor))
                    {
                        if (InPolygon == true)     
                            B.SetPixel(i, j, pnlFill.BackColor);
                    }
                    else
                            InPolygon = !InPolygon;
                }
                Application.DoEvents();
                if (cbDelay.Checked)
                {
                    Count++;                    
                    if (Count == tbDelay.Value)
                    {
                        Count = 0;
                        pictureBox1.Refresh();
                    }
                }
            }

            
            if (cbContour.Checked)
                UpdatePolygon(pnlLine.BackColor);

            pictureBox1.Refresh();
            thrd.Priority = ThreadPriority.Normal;
        }

        // 5
        private void SimpleFill()
        {
            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            UpdatePolygon(pnlFill.BackColor);

            int Count = 0;
            Point P;
            Stack Pixels = new Stack();
            Pixels.Push(FillPoint);
            while (Pixels.Count != 0)
            {
                P = (Point)Pixels.Pop();
                B.SetPixel(P.X, P.Y, pnlFill.BackColor);
                Application.DoEvents();
                if (cbDelay.Checked)
                {
                    Count++;
                    if (Count == tbDelay.Value)
                    {
                        Count = 0;
                        pictureBox1.Refresh();
                    }
                }

                if (B.GetPixel(P.X - 1, P.Y).ToArgb() != pnlFill.BackColor.ToArgb())
                    Pixels.Push(new Point(P.X - 1, P.Y));

                if (B.GetPixel(P.X, P.Y - 1).ToArgb() != pnlFill.BackColor.ToArgb())
                    Pixels.Push(new Point(P.X, P.Y - 1));

                if (B.GetPixel(P.X + 1, P.Y).ToArgb() != pnlFill.BackColor.ToArgb())
                    Pixels.Push(new Point(P.X + 1, P.Y));

                if (B.GetPixel(P.X, P.Y + 1).ToArgb() != pnlFill.BackColor.ToArgb())
                    Pixels.Push(new Point(P.X, P.Y + 1));
            }


            if (cbContour.Checked)
                UpdatePolygon(pnlLine.BackColor);

            pictureBox1.Refresh();
            thrd.Priority = ThreadPriority.Normal;
        }

        // 6
        private void StringFill()
        {
            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            UpdatePolygon(pnlFill.BackColor);

            int Count = 0;
            int tx, XMin, XMax, Flag;
            Point P;
            Stack Pixels = new Stack();
            Pixels.Push(FillPoint);
            while (Pixels.Count != 0)
            {
                P = (Point)Pixels.Pop();
                B.SetPixel(P.X, P.Y, pnlFill.BackColor);
                Application.DoEvents();   
                if (cbDelay.Checked)
                {
                    Count++;                                     
                    if (Count == tbDelay.Value)
                    {
                        Count = 0;
                        pictureBox1.Refresh();
                    }
                }

                tx = P.X;
                P.X++;

                while (B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb())
                {
                    B.SetPixel(P.X, P.Y, pnlFill.BackColor);
                    P.X++;
                }

                XMax = P.X - 1;

                P.X = tx;
                P.X--;

                while (B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb())
                {
                    B.SetPixel(P.X, P.Y, pnlFill.BackColor);
                    P.X--;
                }


                XMin = P.X + 1;

                P.X = XMin;
                P.Y++;
                while (P.X <= XMax)
                {
                    if ((B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb()) && (P.X <= XMax))
                    {
                        P.X++;
                        while ((B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb()) && (P.X <= XMax))
                            P.X++;
                        if ((P.X == XMax) && (B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb()))
                            Pixels.Push(new Point(P.X, P.Y));
                        else
                            Pixels.Push(new Point(P.X - 1, P.Y));
                    }

                    tx = P.X;
                    while ((B.GetPixel(P.X, P.Y).ToArgb() == pnlFill.BackColor.ToArgb()) && (P.X <= XMax))
                        P.X++;

                    if (P.X == tx) P.X++;
                }

                P.X = XMin;
                P.Y -= 2;
                while (P.X <= XMax)
                {
                    if ((B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb()) && (P.X <= XMax))
                    {

                        P.X++;
                        while ((B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb()) && (P.X <= XMax))
                            P.X++;
                        if ((P.X == XMax) && (B.GetPixel(P.X, P.Y).ToArgb() != pnlFill.BackColor.ToArgb()))
                            Pixels.Push(new Point(P.X, P.Y));
                        else
                            Pixels.Push(new Point(P.X - 1, P.Y));
                    }

                    tx = P.X;
                    while ((B.GetPixel(P.X, P.Y).ToArgb() == pnlFill.BackColor.ToArgb()) && (P.X <= XMax))
                        P.X++;

                    if (P.X == tx) P.X++;
                }
            }

            if (cbContour.Checked)
                UpdatePolygon(pnlLine.BackColor);

            pictureBox1.Refresh();
            thrd.Priority = ThreadPriority.Normal;
        }



        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (DiagramBuild)
            {
                FillPoint.X = e.X;
                FillPoint.Y = e.Y;
                DiagramBuild = false;

                Diagram();
                return;
            }
            if (PointFillPoint == true)
            {
                FillPoint.X = e.X;
                FillPoint.Y = e.Y;
                PointFillPoint = false;

                if (radioButton5.Checked)
                    SimpleFill();
                else
                    StringFill();
                return;
            }
            Point P = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                if (PBuild == false)
                {                    
                    NextPolygon.Add(Polygon.Count);
                    PBuild = true;
                }
                else
                {
                    drawable = true;
                    if (IsShiftDown() == true)
                    {
                        Point PT = new Point();
                        PT = (Point)Polygon[Polygon.Count - 1];
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
                    if (NextPolygon.Count > 1)
                    if (NextPolygon[NextPolygon.Count - 1] != NextPolygon[NextPolygon.Count - 2])
                    {
                        if (Polygon.Count > 1)
                        if ((P.Y == Polygon[Polygon.Count - 1].Y) &&
                            (Polygon[Polygon.Count - 1].Y == Polygon[Polygon.Count - 2].Y))
                        {
                            Polygon.RemoveAt(Polygon.Count - 1);
                            NextPolygon[NextPolygon.Count - 1]--;
                        }
                    }
                }                
                Polygon.Add(P);
                NextPolygon[NextPolygon.Count - 1]++;
            }
            else
                if (e.Button == MouseButtons.Right)
                {
                    drawable = false;
                    PBuild = false;

                }
            UpdateLastSegment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Polygon.Clear();
            NextPolygon.Clear();
            drawable = false;
            PBuild = false;
            PointFillPoint = false;
            DiagramBuild = false;
            NextPolygon.Add(0);

            Clear();
            pictureBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            B = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = B;
            G = Graphics.FromImage(B);
            Polygon = new List<Point>();
            NextPolygon = new List<int>();
            NextPolygon.Add(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointFillPoint = false;
            PBuild = false;

            if (radioButton1.Checked)
                EdgeSortedList();
            else
                if (radioButton2.Checked)
                    EdgeFill();
                else
                    if (radioButton3.Checked)
                        EdgeFillWithPartition();
                    else
                        if (radioButton4.Checked)
                            EdgeListFlag();
                        else
                            if (radioButton5.Checked)
                            {
                                Clear();
                                UpdatePolygon(pnlLine.BackColor);
                                pictureBox1.Refresh();
                                MessageBox.Show("Укажите точку затравки");
                                PointFillPoint = true;
                                Clear();
                            }
                            else
                                if (radioButton6.Checked)
                                {
                                    Clear();
                                    UpdatePolygon(pnlLine.BackColor);
                                    pictureBox1.Refresh();
                                    MessageBox.Show("Укажите точку затравки");
                                    PointFillPoint = true;
                                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
            UpdatePolygon(pnlLine.BackColor);
            pictureBox1.Refresh();
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

        private void pnlLine_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                (sender as Panel).BackColor = colorDialog1.Color;
        }

        private void cbDelay_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDelay.Checked)
                tbDelay.Enabled = true;
            else
                tbDelay.Enabled = false;
        }

        private void Diagram()
        {
            float[] time = new float[6];
            float time_max, time_min;
            Brush br = new SolidBrush(pnlBackground.BackColor);
            PerfCounter PerfCount = new PerfCounter();

            Thread thrd = Thread.CurrentThread;
            thrd.Priority = ThreadPriority.Highest;

            cbDelay.Checked = false;
            cbDelay.Enabled = false;
            Clear();
            pictureBox1.Refresh();

            radioButton1.Checked = true;
            PerfCount.Start();
            EdgeSortedList();
            time[0] = PerfCount.Finish();
            time_max = time[0];
            time_min = time[0];

            radioButton2.Checked = true;
            PerfCount.Start();
            EdgeFill();
            time[1] = PerfCount.Finish();
            if (time[1] > time_max) time_max = time[1];
            if (time[1] < time_min) time_min = time[1];

            radioButton3.Checked = true;
            PerfCount.Start();
            EdgeFillWithPartition();
            time[2] = PerfCount.Finish();
            if (time[2] > time_max) time_max = time[2];
            if (time[2] < time_min) time_min = time[2];

            radioButton4.Checked = true;
            PerfCount.Start();
            EdgeListFlag();
            time[3] = PerfCount.Finish();
            if (time[3] > time_max) time_max = time[3];
            if (time[3] < time_min) time_min = time[3];

            radioButton5.Checked = true;
            Clear();
            UpdatePolygon(pnlLine.BackColor);
            pictureBox1.Refresh();
            PerfCount.Start();
            SimpleFill();
            time[4] = PerfCount.Finish();
            if (time[4] > time_max) time_max = time[4];
            if (time[4] < time_min) time_min = time[4];

            radioButton6.Checked = true;
            Clear();
            UpdatePolygon(pnlLine.BackColor);
            pictureBox1.Refresh();
            PerfCount.Start();
            StringFill();
            time[5] = PerfCount.Finish();
            if (time[5] > time_max) time_max = time[5];
            if (time[5] < time_min) time_min = time[5];

            radioButton6.Checked = false;

            G.FillRectangle(br, 0, 0, B.Width, B.Height);
            float d = 40.0f;
            int a = (int)Math.Round(d);
            float coef = ((float)B.Height - 2 * d) / (time_max);
            int w = (int)Math.Round(((float)B.Width - 2 * d) / 5.0f);
            Point PE = new Point();
            Point PB = new Point(a, B.Height - (int)Math.Round((time[0]) * coef) - (int)Math.Round(d / 2));
            Point P = new Point(a, B.Height - (int)Math.Round(d / 2));
            Pen pen = new Pen(pnlLine.BackColor);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font f = new Font(label1.Font, FontStyle.Regular);
            Brush fb = new SolidBrush(pnlLine.BackColor);
            G.DrawLine(pen, P, PB);
            string str = Convert.ToString(time[0]);
            Pen pLine = new Pen(pnlLine.BackColor);
            G.DrawString(str, f, fb, (float)(PB.X - G.MeasureString(str, f).Width / 2),
                (float)(PB.Y - G.MeasureString(str, f).Height - 3));
            for (int i = 1; i < 6; i++)
            {
                a += w;
                PE.X = a;
                PE.Y = B.Height - (int)Math.Round((time[i]) * coef) - (int)Math.Round(d / 2);
                G.DrawLine(pLine, PB, PE);
                P.X = P.X + w;
                str = Convert.ToString(time[i]);
                G.DrawString(str, f, fb, (float)(PE.X - G.MeasureString(str, f).Width / 2),
                    (float)(PE.Y - G.MeasureString(str, f).Height - 3));
                G.DrawLine(pen, P, PE);
                PB.X = PE.X;
                PB.Y = PE.Y;
            }
            float x = d;
            float wf = ((float)B.Width - 2 * d) / 5.0f;
            float y = (float)(P.Y + 3);
            G.DrawString("С упор. списком ребер", f, fb, 
                x - G.MeasureString("С упор. списком ребер", f).Width / 2, y);
            x += wf;
            G.DrawString("Заполнение по ребрам", f, fb, x - 
                G.MeasureString("Заполнение по ребрам", f).Width / 2, y);
            x += wf;
            G.DrawString("По ребрам с перегородкой", f, fb,
                x - G.MeasureString("По ребрам с перегородкой", f).Width / 2, y);
            x += wf;
            G.DrawString("Список ребер с флагом", f, fb,
                x - G.MeasureString("Список ребер с флагом", f).Width / 2, y);
            x += wf;
            G.DrawString("Простая затравка", f, fb,
                x - G.MeasureString("Простая затравка", f).Width / 2, y);
            x += wf;
            G.DrawString("Построчная затравка", f, fb,
                x - G.MeasureString("Построчная затравка", f).Width / 2 - 10, y);

            cbDelay.Enabled = true;
            pictureBox1.Refresh();
            thrd.Priority = ThreadPriority.Normal;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
            UpdatePolygon(pnlLine.BackColor);
            pictureBox1.Refresh();
            MessageBox.Show("Укажите точку затравки");
            DiagramBuild = true;   
        }

        private void cbContour_CheckedChanged(object sender, EventArgs e)
        {
            button3_Click(null, null);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1_MouseUp(null, new MouseEventArgs(MouseButtons.Left, 1, (int)udX.Value, (int)udY.Value, 0));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Polygon.Clear();
            NextPolygon.Clear();
            drawable = false;
            PBuild = false;
            PointFillPoint = false;
            DiagramBuild = false;
            NextPolygon.Add(0);
            Clear();

            Random R = new Random();
            for (int i = 0; i < udN.Value; i++)
                Polygon.Add(new Point(R.Next(pictureBox1.Width), R.Next(pictureBox1.Height)));
            NextPolygon.Add(Polygon.Count);

            UpdatePolygon(pnlLine.BackColor);
            pictureBox1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

    }
}