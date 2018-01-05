using System;
using System.Collections.Generic;
using System.Data.Clustering;
using System.Drawing;
using System.Windows.Forms;

namespace KMeans
{
    public partial class Form1 : Form
    {
        List<ClusterCentroid> Centers;
        List<ClusterPoint> Points;
        Graphics FieldGraphics;
        Bitmap FieldBitmap;
        const int Radius = 4;
        Color[] Colors;

        public Form1()
        {
            InitializeComponent();
        }

        void RecalcAll()
        {
            FieldBitmap = new Bitmap(pbField.Width, pbField.Height);
            FieldGraphics = Graphics.FromImage(FieldBitmap);
            FieldGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            pbField.Image = FieldBitmap;   
            FieldGraphics = Graphics.FromImage(FieldBitmap);

            KMeansAlgorithm CMeans = new KMeansAlgorithm(Points, Centers, Convert.ToSingle(tbFuzzy.Text));

            FieldGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0,
                pbField.Width, pbField.Height);            
            for (int i = 0; i < Points.Count; i++)
            {
                FieldGraphics.FillEllipse(new SolidBrush(Colors[(int)Math.Round(Points[i].ClusterIndex)]), Points[i].X - 3,
                    Points[i].Y - 3, 6, 6);
            }
            float d = 10;
            for (int i = 0; i < Centers.Count; i++)
            {
                Pen LinePen = new Pen(Colors[i]);
                //Pen LinePen = new Pen(Color.Black);
                FieldGraphics.DrawLine(LinePen, new PointF(Centers[i].X - d, Centers[i].Y), new PointF(Centers[i].X + d, Centers[i].Y));
                FieldGraphics.DrawLine(LinePen, new PointF(Centers[i].X, Centers[i].Y - d), new PointF(Centers[i].X, Centers[i].Y + d));
            }
            pbField.Refresh();
        }

        double DistanceSqr(PointF P1, PointF P2)
        {
            return (P1.X - P2.X) * (P1.X - P2.X) + (P1.Y - P2.Y) * (P1.Y * P2.Y);
        }

        double Distance(PointF P1, PointF P2)
        {
            return Math.Sqrt((P1.X - P2.X) * (P1.X - P2.X) + (P1.Y - P2.Y) * (P1.Y * P2.Y));
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            Random Rand = new Random();
            int CentersCount = Rand.Next(1, (int)udCenterCount.Maximum + 1);
            int PointsCount = Rand.Next(CentersCount * 2, (int)udPointsCount.Maximum + 1);
            Centers = new List<ClusterCentroid>(CentersCount);
            Points = new List<ClusterPoint>(PointsCount);
            udCenterCount.Value = CentersCount;
            udPointsCount.Value = PointsCount;
            for (int i = 0; i < CentersCount; i++)
                Centers.Add(new ClusterCentroid((float)(Rand.NextDouble() * pbField.Width),
                    (float)(Rand.NextDouble() * pbField.Height)));
            for (int i = 0; i < PointsCount; i++)
                Points.Add(new ClusterPoint((float)(Rand.NextDouble() * pbField.Width),
                    (float)(Rand.NextDouble() * pbField.Height)));
            Colors = new Color[Centers.Count];
            for (int i = 0; i < Colors.Length; i++)
                Colors[i] = Color.FromArgb(Rand.Next(32, 256), Rand.Next(32, 256), Rand.Next(32, 256));

            RecalcAll();
        }

        private void pbField_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Points.Add(new ClusterPoint(e.X, e.Y));
                udPointsCount.Value = Points.Count;
            }
            else
                if (e.Button == MouseButtons.Right)
                {
                    Centers.Add(new ClusterCentroid(e.X, e.Y));

                    udCenterCount.Value = Centers.Count;
                }
            RecalcAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random Rand = new Random();
            int CentersCount = (int)udCenterCount.Value;
            int PointsCount = (int)udPointsCount.Value;
            /*Centers = new List<ClusterCentroid>(CentersCount);
            Points = new List<ClusterPoint>(PointsCount);
            udCenterCount.Value = CentersCount;
            udPointsCount.Value = PointsCount;
            for (int i = 0; i < CentersCount; i++)
                Centers.Add(new ClusterCentroid((float)(Rand.NextDouble() * pbField.Width),
                    (float)(Rand.NextDouble() * pbField.Height)));
            for (int i = 0; i < PointsCount; i++)
                Points.Add(new ClusterPoint((float)(Rand.NextDouble() * pbField.Width),
                    (float)(Rand.NextDouble() * pbField.Height)));*/            

            RecalcAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random Rand = new Random();
            int CentersCount = (int)udCenterCount.Value;
            int PointsCount = (int)udPointsCount.Value;
            Centers = new List<ClusterCentroid>(CentersCount);
            Points = new List<ClusterPoint>(PointsCount);
            udCenterCount.Value = CentersCount;
            udPointsCount.Value = PointsCount;
            for (int i = 0; i < CentersCount; i++)
                Centers.Add(new ClusterCentroid((float)(Rand.NextDouble() * pbField.Width),
                    (float)(Rand.NextDouble() * pbField.Height)));
            for (int i = 0; i < PointsCount; i++)
                Points.Add(new ClusterPoint((float)(Rand.NextDouble() * pbField.Width),
                    (float)(Rand.NextDouble() * pbField.Height)));
            Colors = new Color[Centers.Count];
            for (int i = 0; i < Colors.Length; i++)
                Colors[i] = Color.FromArgb(Rand.Next(32, 256), Rand.Next(32, 256), Rand.Next(32, 256));            

            RecalcAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Centers = new List<ClusterCentroid>();
            Points = new List<ClusterPoint>();
            RecalcAll();
        }
    }
}
