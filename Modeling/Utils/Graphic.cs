using AdvanceMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdvanceDrawing
{
	public class GraphFunc
    {
        public bool Visible { get; set; }

        public DoubleFunc Func;
        public Pen Pen;

        public GraphFunc(DoubleFunc Func, Pen Pen)
        {
            this.Func = Func;
            this.Pen = Pen;
            this.Visible = true;
        }

        public GraphFunc(Pen Pen)
        {
            //this.Func = new DoubleFunc;
            this.Pen = Pen;
        }

        public static implicit operator DoubleFunc(GraphFunc GraphFunc)
        {
            return GraphFunc.Func;
        }
    }

    public class Graphic
    {
        PictureBox pictBox;
        Graphics graphics;
        Bitmap bitmap;
        Rectangle border;
        int fontDx = 5;
        int fontDy = 7;
        string FloatGraphFormat = "F03";
        
        public double X1, X2, Y1, Y2;  // временно

        public bool SaveProportion;
        public bool DrawAxis;
        public bool ConstAxis;
        public bool DrawGrid;
        public int gridXCount;
        public int gridYCount;

        double ScaleX, ScaleY;
        double MinY, MaxY;
        int AxisX, AxisY;

        public Pen AxisPen;
        public Pen TextPen;
        public Font TextFont;
        public Brush TextBrush;
        public Pen GridPen;

        public double X0;
        public List<PointD> Points;
        public GraphFunc[] Funcs;

        public Graphic(PictureBox PictureBox)
        {
            this.bitmap = new Bitmap(PictureBox.ClientSize.Width, PictureBox.ClientSize.Height);
            pictBox = PictureBox;
            pictBox.Image = bitmap;
            graphics = Graphics.FromImage(bitmap);

            TextFont = new Font(FontFamily.GenericSerif, 8, FontStyle.Regular);
            TextPen = new Pen(Color.Green);
            TextPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            TextPen.DashOffset = 2;
            TextBrush = new SolidBrush(Color.Black);
        }

        public Graphic(PictureBox PictureBox, Pen AxisPen, Pen TextPen, Font TextFont, Brush TextBrush)
        {
            this.bitmap = new Bitmap(PictureBox.ClientSize.Width, PictureBox.ClientSize.Height);
            pictBox = PictureBox;
            pictBox.Image = bitmap;
            graphics = Graphics.FromImage(bitmap);

            this.AxisPen = AxisPen;
            this.TextPen = TextPen;
            this.TextFont = TextFont;
            this.TextBrush = TextBrush;            
        }

        public void Clear()
        {
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
        }

        public void Recalc()
        {
            border = new Rectangle(40, 20, bitmap.Width - 40 * 2, bitmap.Height - 20 * 2);

            double y;
            MinY = Double.MaxValue;
            MaxY = Double.MinValue;

            ScaleX = (X2 - X1) / (double)(border.Width);
            for (int i = 1; i < border.Width - 1; i++)
            {
                for (int j = 0; j < Funcs.Length; j++)
                {
                    if (Funcs[j].Visible)
                    {
                        y = Funcs[j].Func(i * ScaleX + X1);
                        if (y < MinY)
                            MinY = y;
                        if (y > MaxY)
                            MaxY = y;
                    }
                }
            }            


            if (SaveProportion)
            {
                ScaleY = 1 / ScaleX;
            }
            else
            {
                ScaleY = border.Height / (MaxY - MinY);
            }

            if (ConstAxis)
            {
                AxisX = border.Left;
                AxisY = border.Top + border.Height;                
            }
            else
            {
                AxisX = border.Left - (int)Math.Round(X1 / ScaleX);
                if (AxisX < border.Left)
                    AxisX = border.Left;
                if (AxisX > border.Left + border.Width)
                    AxisX = border.Left + border.Width;

                if (SaveProportion)
                {
                    /*double TempAxisY = (MaxY - MinY) / 2;
                    if ((MaxY - MinY) * ScaleY / 2 > TempAxisY)
                    {
                        TempAxisY = (MaxY - MinY) * ScaleY / 2 - TempAxisY;

                        ((MaxY - MinY) * ScaleY / 2 > TempAxisY)
                    
                    }*/
                    //MinY = (MaxY - MinY) / 2;
                    //AxisY = border.Top + border.Height - (int)Math.Round((MaxY - MinY) / 2 * ScaleY);
                    AxisY = border.Top + border.Height - border.Height / 2;
                    //DY = 
                }
                else
                {
                    AxisY = border.Top + border.Height - (int)Math.Round(MinY * ScaleY);
                    if (AxisY < border.Top)
                        AxisY = border.Top;
                    if (AxisY > border.Top + border.Height)
                        AxisY = border.Top + border.Height;
                }
            }
        }

        public void Redraw()
        {
            //graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
            Recalc();

            Pen LinePen = new Pen(Color.LightSteelBlue);
            LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            LinePen.DashOffset = 5;            
            
            int[] y1 = new int[Funcs.Length];
            int[] y2 = new int[Funcs.Length];

            //--------------------
            // Drawing part
            //--------------------


            string str = MinY.ToString(FloatGraphFormat);
            SizeF str_size = graphics.MeasureString(str, TextFont);
            graphics.DrawString(str, TextFont, TextBrush, AxisX - str_size.Width - fontDx, border.Top + border.Height - str_size.Height / 2);
            str = MaxY.ToString(FloatGraphFormat);
            str_size = graphics.MeasureString(str, TextFont);
            graphics.DrawString(str, TextFont, TextBrush, AxisX - str_size.Width - fontDx, border.Top - str_size.Height / 2);

            int tx, ty;
            for (int i = 0; i < Points.Count; i++)
            {
                tx = (int)Math.Round((Points[i].X - X1) / ScaleX + border.Left);
                ty = border.Top + border.Height - (int)Math.Round(MinY * ScaleY);
                //graphics.DrawLine(LinePen, tx, AxisY, tx, border.Top + border.Height - (int)Math.Round((Funcs[0].Func(Points[i].X) - MinY) * ScaleY));
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.FillEllipse(new SolidBrush(Color.Red), tx - 3, border.Top + border.Height - (int)Math.Round((Funcs[0].Func(Points[i].X) - MinY) * ScaleY) - 3, 6, 6);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                str = Points[i].X.ToString(FloatGraphFormat);
                //graphics.DrawString(str, TextFont, TextBrush, tx - graphics.MeasureString(str, TextFont).Width / 2, AxisY + fontDy);
            }

            double dtx, dty;
            if (DrawGrid)
            {
                if (gridXCount != 0)
                {
                    double gridXStep = (X2 - X1) / gridXCount;
                    dtx = X1;
                    for (int i = 0; i <= gridXCount; i++)
                    {
                        tx = (int)Math.Round((dtx - X1) / ScaleX) + border.Left;
                        graphics.DrawLine(GridPen, tx, border.Top, tx, border.Top + border.Height);
                        str = dtx.ToString(FloatGraphFormat);
                        graphics.DrawString(str, TextFont, TextBrush, tx - graphics.MeasureString(str, TextFont).Width / 2, border.Top + border.Height + fontDy);
                        dtx += gridXStep;
                    }
                }
                if (gridYCount != 0)
                {
                    double gridYStep = (MaxY - MinY) / gridYCount;
                    dty = MinY;
                    for (int i = 0; i <= gridXCount; i++)
                    {
                        ty = border.Top + border.Height - (int)Math.Round((dty - MinY) * ScaleY);
                        graphics.DrawLine(GridPen, border.Left, ty, border.Left + border.Width, ty);
                        str = dty.ToString(FloatGraphFormat);
                        graphics.DrawString(str, TextFont, TextBrush, border.Left - graphics.MeasureString(str, TextFont).Width - fontDx,
                            ty - graphics.MeasureString(str, TextFont).Height / 2);
                        dty += gridYStep;
                    }
                }
            }

            if (DrawAxis)
            {
                graphics.DrawLine(AxisPen, border.Left, AxisY, border.Left + border.Width, AxisY);
                graphics.DrawLine(AxisPen, AxisX, border.Top + border.Height, AxisX, border.Top);
            }

           /* str_size = graphics.MeasureString("0", TextFont);
            graphics.DrawString("0", TextFont, TextBrush, border.Left - str_size.Width - fontDx, 
                  border.Top + border.Height + (int)Math.Round(MinY * ScaleY) - str_size.Height / 2);*/

            for (int j = 0; j < Funcs.Length; j++)
                y1[j] = border.Top + border.Height - (int)Math.Round((Funcs[j].Func(X1) - MinY) * ScaleY);
            for (int i = 0; i < border.Width - 1; i++)
            {
                for (int j = 0; j < Funcs.Length; j++)
                {
                    if (Funcs[j].Visible)
                    {
                        y2[j] = border.Top + border.Height - (int)Math.Round((Funcs[j].Func(i * ScaleX + X1) - MinY) * ScaleY);
                        graphics.DrawLine(Funcs[j].Pen, i + border.Left, y1[j], i + border.Left + 1, y2[j]);
                        y1[j] = y2[j];
                    }
                }
            }

            /*graphics.DrawLine(TextPen, (int)Math.Round((X0 - X1) / ScaleX + AxisX),
                border.Top + border.Height + (int)Math.Round(MinY * ScaleY),
                (int)Math.Round((X0 - X1) / ScaleX + AxisX),
                border.Top + border.Height - (int)Math.Round((Funcs[1].Func(X0) - MinY) * ScaleY));

            str = X0.ToString(FloatGraphFormat);
            graphics.DrawString(str, TextFont, TextBrush, (int)Math.Round((X0 - X1) / ScaleX + AxisX) - graphics.MeasureString(str, TextFont).Width / 2,
                border.Top + border.Height + (int)Math.Round(MinY * ScaleY) - graphics.MeasureString(str, TextFont).Height - fontDy);*/

            pictBox.Refresh();
        }

        public double Convert(int PictBoxX)
        {
            if (PictBoxX < border.Left || PictBoxX >= border.Left + border.Width)
                return Double.NaN;
            else
                return (PictBoxX - border.Left) / (double)(border.Width) * (X2 - X1) + X1;
        }        
    }
}
