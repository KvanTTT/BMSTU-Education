using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace LR4
{
	public partial class Form1 : Form
    {
        
        //------------------------
        TLR4 Lab;
        TPoint edit_point;

        TPoint[] LowSpeed = {new TPoint(-50,0) ,new TPoint(-30, 1), new TPoint(30, 1), new TPoint(50, 0) };
        TPoint[] MediumSpeed = { new TPoint(30, 0), new TPoint(50, 1), new TPoint(110, 1), new TPoint(130, 0) };
        TPoint[] FastSpeed = { new TPoint(110, 0), new TPoint(130, 1), new TPoint(180, 1), new TPoint(181, 0) };

        TPoint[] DistanceSmall = { new TPoint(1, 0), new TPoint(10, 1), new TPoint(20, 1), new TPoint(40, 0) };
        TPoint[] DistanceMiddle = { new TPoint(20, 0), new TPoint(30, 1), new TPoint(45, 1), new TPoint(60, 0) };
        TPoint[] DistanceLarge = { new TPoint(45, 0), new TPoint(60, 1), new TPoint(90, 1), new TPoint(91, 0) };
        //------------------------

        public Form1()
        {
            InitializeComponent();
            Lab = new TLR4(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            zedGraphControl1.GraphPane.CurveList.Clear();
            DrawGraphics(zedGraphControl1, LowSpeed, Color.Red, "Скорость низкая");
            DrawGraphics(zedGraphControl1, MediumSpeed, Color.Blue, "Скорость средняя");
            DrawGraphics(zedGraphControl1, FastSpeed, Color.Green, "Скорость высокая");

            zedGraphControl2.GraphPane.CurveList.Clear();
            DrawGraphics(zedGraphControl2, DistanceSmall, Color.Red, "Расстояние маленькое");
            DrawGraphics(zedGraphControl2, DistanceMiddle, Color.Blue, "Расстояние среднее");
            DrawGraphics(zedGraphControl2, DistanceLarge, Color.Green, "Расстояние большое");
            zedGraphControl1.GraphPane.Title.Text = "Скорость автомобиля";
            zedGraphControl2.GraphPane.Title.Text = "Дистанция до автомобиля"; 

        }

        private TFuzzySet CreatSpeedFuzzySet()
        {
            TFuzzySet Result = new TFuzzySet();

            Result.Add(new Graphic(LowSpeed,"низкая"));
            Result.Add(new Graphic(MediumSpeed, "средняя"));
            Result.Add(new Graphic(FastSpeed, "высокая"));

            return Result;
        }

        private TFuzzySet CreatDistanceFuzzySet()
        {
            TFuzzySet Result = new TFuzzySet();

            Result.Add(new Graphic(DistanceSmall, "близко"));
            Result.Add(new Graphic(DistanceMiddle, "средне"));
            Result.Add(new Graphic(DistanceLarge, "далеко"));

            return Result;
        }


        private bool zedGraphControl2_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zedGraphControl2.GraphPane.ReverseTransform(e.Location, out x, out y);
            SearchEditDistancePoint(x, y);
            return default(bool);
        }

        private bool zedGraphControl1_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zedGraphControl1.GraphPane.ReverseTransform(e.Location, out x, out y);
            // изменение координат точки
            if (edit_point != null)
            {
                edit_point.X = x - x % 5;
                if (x % 5 > 2) edit_point.X += 5;
                Redraw();
                edit_point = null;
            }
            return default(bool);
        }

        public void Redraw()
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            DrawGraphics(zedGraphControl1, LowSpeed, Color.Red, "Скорость низкая");
            DrawGraphics(zedGraphControl1, MediumSpeed, Color.Blue, "Скорость средняя");
            DrawGraphics(zedGraphControl1, FastSpeed, Color.Green, "Скорость высокая");

            zedGraphControl2.GraphPane.CurveList.Clear();
            DrawGraphics(zedGraphControl2, DistanceSmall, Color.Red, "Расстояние маленькое");
            DrawGraphics(zedGraphControl2, DistanceMiddle, Color.Blue, "Расстояние среднее");
            DrawGraphics(zedGraphControl2, DistanceLarge, Color.Green, "Расстояние большое");
        }

        private bool zedGraphControl1_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zedGraphControl1.GraphPane.ReverseTransform(e.Location, out x, out y);
            SearchEditSpeedPoint(x, y);
            return default(bool);
        }

        private void SearchEditSpeedPoint(double x, double y)
        {
            TPoint tmp = LowSpeed[0];
            foreach (TPoint point in LowSpeed)
            {
                if ((point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y) < (tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y))
                {
                    tmp = point;
                }
            }
            foreach (TPoint point in MediumSpeed)
            {
                if ((point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y) < (tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y))
                {
                    tmp = point;
                }
            }
            foreach (TPoint point in FastSpeed)
            {
                if ((point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y) < (tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y))
                {
                    tmp = point;
                }
            }
            if ((tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y) < 10) edit_point = tmp;
        }

        private void SearchEditDistancePoint(double x, double y)
        {
            TPoint tmp = LowSpeed[0];
            foreach (TPoint point in DistanceSmall)
            {
                if ((point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y) < (tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y))
                {
                    tmp = point;
                }
            }
            foreach (TPoint point in DistanceMiddle)
            {
                if ((point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y) < (tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y))
                {
                    tmp = point;
                }
            }
            foreach (TPoint point in DistanceLarge)
            {
                if ((point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y) < (tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y))
                {
                    tmp = point;
                }
            }
            if ((tmp.X - x) * (tmp.X - x) + (tmp.Y - y) * (tmp.Y - y) < 10) edit_point = tmp;
        }

        private bool zedGraphControl2_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zedGraphControl2.GraphPane.ReverseTransform(e.Location, out x, out y);
            // изменение координат точки
            if (edit_point != null)
            {
                edit_point.X = x - x % 5;
                if (x % 5 > 2) edit_point.X += 5;
                Redraw();
                edit_point = null;
            }
            return default(bool);
        }

        public void DrawGraphics(ZedGraphControl control, TPoint[] points, Color color, String name)
        {
            GraphPane pane = control.GraphPane;
            //  pane.CurveList.Clear();
            pane.XAxis.MajorGrid.IsVisible = true;

            PointPairList list = new PointPairList();

            for (int i = 0; i < points.Length; i++)
            {
                list.Add(points[i].X, points[i].Y);
            }

            LineItem myCurve = pane.AddCurve(name, list, color, SymbolType.Square);
            myCurve.IsSelectable = true;

            control.AxisChange();
            control.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Lab.Redraw(e.Graphics,pictureBox1.Width,pictureBox1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Lab.Step(this);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > numericUpDown3.Value)
                numericUpDown2.Value = numericUpDown3.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value < numericUpDown2.Value)
                numericUpDown3.Value = numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                try
                {
                    Lab.ParseRules(richTextBox1.Lines);

                    Lab.SpeedSet = CreatSpeedFuzzySet();
                    Lab.DistanceSet = CreatDistanceFuzzySet();

                    button1.Text = "Stop";
                    timer1.Enabled = true;

                    /*numericUpDown1.Enabled = false;
                    numericUpDown2.Enabled = false;
                    numericUpDown3.Enabled = false;
                    */

                    Lab.StartStream();
                }
                catch (TPMException Exc)
                {
                    MessageBox.Show("Синтаксическая ошибка в правилах!",
                    "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                button1.Text = "Поехали!";
                timer1.Enabled = false;

            /*    numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;*/

                Lab.StopStream();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab.ClearRoad(pictureBox1);
        }
    }
}
