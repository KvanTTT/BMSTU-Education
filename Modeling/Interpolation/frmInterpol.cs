using AdvanceDrawing;
using AdvanceMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Interpolation
{
	public partial class frmInterpol : Form
    {
        List<PointD> GenPoints;
        Polynom Polynom;
        Polynom InvPolynom;
        Graphics G;
        Bitmap B;
        string FloatResultFormat = "F06";
        int FuncNumber = 0;

        double XL, XR, X0;

        Graphic Graphic;
        
        DoubleFunc InvInterpFunc;

        public frmInterpol()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            B = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.Image = B;
            G = Graphics.FromImage(B);
           
            Graphic = new Graphic(pictureBox1);
            frmGraphicCust.LoadGraphic(Graphic);
            LoadGenPoints();

            cmbMethod.SelectedIndex = 0;

            /*Pen AxisPen = new Pen(Color.Black);
            //AxisPen.CustomStartCap.BaseCap = System.Drawing.Drawing2D.LineCap.Triangle;
            AxisPen.DashCap = System.Drawing.Drawing2D.DashCap.Triangle;
            AxisPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            Pen TextPen = new Pen(Color.Green);
            TextPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            TextPen.DashOffset = 2;
            Graphic = new Graphic(pictureBox1, AxisPen, TextPen, new Font(FontFamily.GenericSerif, 8, FontStyle.Regular),
                new SolidBrush(Color.Black));
            Graphic.Funcs = new GraphFunc[2] { new GraphFunc(new Pen(Color.Red)), new GraphFunc(new Pen(Color.Blue)) };
            Graphic.DrawAxis = true;
            Graphic.SaveProportion = false;
            Graphic.Points = GenPoints;*/

        }

        private void BuildInterpFunc()
        {
            List<PointD> NearPoints = new List<PointD>(GenPoints);
            NearPoints.Sort((A, B) => Math.Abs(A.X - X0).CompareTo(Math.Abs(B.X - X0)));
            NearPoints.RemoveRange((int)tbPower.Value + 1, NearPoints.Count - (int)tbPower.Value - 1);
            NearPoints.Sort((A, B) => A.X.CompareTo(B.X));

            Graphic.Points = NearPoints;

            List<PointD> InvPoints = NearPoints.ConvertAll<PointD>(A => new PointD(A.Y, A.X));

            if (rbNewton.Checked)
            {
                Polynom = new NewtonPolynom(NearPoints);
                InvPolynom = new NewtonPolynom(InvPoints);
            }
            else
            {
                Polynom = new Spline(NearPoints);
                InvPoints.Sort((A, B) => A.X.CompareTo(B.X));
                InvPolynom = new Spline(InvPoints);
            }
            Graphic.Funcs[1].Func = Polynom.GetValueAt;
            InvInterpFunc = InvPolynom.GetValueAt;
        }

        double Power(double X, int N)
        {
            double Result = 1;
            for (int i = 0; i < N; i++)
                Result *= X;
            return Result;
        }

        void BuildApproxFunc()
        {
            List<WeightPointD> WeightPoints = new List<WeightPointD>(GenPoints.Count);
            for (int i = 0; i < GenPoints.Count; i++)
                WeightPoints.Add(new WeightPointD(GenPoints[i], Convert.ToDouble(dgvPointsTable[2, i].Value)));

            Polynom = new ApproxPolynom(WeightPoints, (int)tbPower.Value, (X, N) => Math.Pow(X, N));
            Graphic.Funcs[1].Func = Polynom.GetValueAt;

            Graphic.Points = GenPoints;
        }

        void GenPointsFromTable()
        {
            dgvPointsTable.Rows.Clear();
            GenPoints = new List<PointD>(dgvPointsTable.RowCount);
            for (int i = 0; i < dgvPointsTable.RowCount; i++)
                GenPoints[i] = new PointD(Convert.ToDouble(dgvPointsTable[0, i].Value),
                                          Convert.ToDouble(dgvPointsTable[1, i].Value));
        }

        void LoadGenPoints()
        {
            dgvPointsTable.Rows.Clear();
            GenPoints = new List<PointD>();
            frmPointsGen frm = new frmPointsGen();
            StreamReader Reader = new StreamReader("Settings.dat");

            //Graphic.Funcs = new GraphFunc[2];
            Graphic.Funcs[0].Func = frm.GetFunc(Convert.ToInt32(Reader.ReadLine()));
            while (!Reader.EndOfStream)
            {
                GenPoints.Add(new PointD(Convert.ToDouble(Reader.ReadLine()), Convert.ToDouble(Reader.ReadLine())));
                Reader.ReadLine();  // веса
                dgvPointsTable.Rows.Add(new object[] { GenPoints[GenPoints.Count-1].X.ToString(FloatResultFormat), 
                                                           GenPoints[GenPoints.Count-1].Y.ToString(FloatResultFormat),
                                                           1 });
                dgvPointsTable.Rows[dgvPointsTable.Rows.Count - 1].HeaderCell.Value = dgvPointsTable.Rows.Count - 1;
            }

            Reader.Close();
        }

        void SaveGenPoints()
        {
            StreamWriter Writer = new StreamWriter("Settings.dat");

            Writer.WriteLine(FuncNumber);
            int i;
            for (i = 0; i < GenPoints.Count - 1; i++)
            {
                Writer.WriteLine(GenPoints[i].X);
                Writer.WriteLine(GenPoints[i].Y);
                Writer.WriteLine(0);
            }
            Writer.WriteLine(GenPoints[i].X);
            Writer.WriteLine(GenPoints[i].Y);
            Writer.Write(0);

            Writer.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                double X;
                if ((X = Graphic.Convert(e.X)) != Double.NaN)
                    tbX.Text = X.ToString(FloatResultFormat);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           // try
            {
                XL = Convert.ToDouble(tbBottomBound.Text);
                XR = Convert.ToDouble(tbUpperBound.Text);
                X0 = Convert.ToDouble(tbX.Text);
                if ((X0 < XL) || (X0 > XR))
                    label12.Visible = true;
                else
                    label12.Visible = false;
                if (cmbMethod.SelectedIndex == 0)
                    BuildInterpFunc();
                else
                    BuildApproxFunc();
                Graphic.X0 = Convert.ToDouble(tbX.Text);
                Graphic.X1 = Convert.ToDouble(tbBottomBound.Value);
                Graphic.X2 = Convert.ToDouble(tbUpperBound.Value);
                
                Graphic.Clear();
                Graphic.Redraw();
                tbFuncResult.Text = Graphic.Funcs[0].Func(X0).ToString(FloatResultFormat);
                tbPolynomResult.Text = Graphic.Funcs[1].Func(X0).ToString(FloatResultFormat);
                tbError.Text = Math.Abs(Graphic.Funcs[0].Func(X0) - Graphic.Funcs[1].Func(X0)).ToString(FloatResultFormat);
                tbRoot.Text = InvInterpFunc(0.0).ToString(FloatResultFormat);
            }
            //catch
            {
            }        
        }

        private void cmbFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
        }

        private void dgvPointsTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new frmGraphicCust();
            frm.CustomizeGraphic(Graphic, true);
            btnUpdate_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new frmPointsGen();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GenPoints = frm.GeneratePoints(ref FuncNumber);
                Graphic.Funcs[0].Func = frm.GetFunc(FuncNumber);
                dgvPointsTable.Rows.Clear();
                for (int i = 0; i < GenPoints.Count; i++)
                {                    
                    dgvPointsTable.Rows.Add(new object[] { GenPoints[i].X.ToString(FloatResultFormat), 
                                                           GenPoints[i].Y.ToString(FloatResultFormat),
                                                           1 });
                    dgvPointsTable.Rows[i].HeaderCell.Value = i;
                }
                btnUpdate_Click(sender, e);
                tbBottomBound.Text = GenPoints[0].X.ToString(FloatResultFormat);
                tbUpperBound.Text = GenPoints[GenPoints.Count - 1].X.ToString(FloatResultFormat);
            }
        }       

        private void frmInterpol_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGenPoints();
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMethod.SelectedIndex == 0)
            {
                gbMethod.Text = "Метод интерполяции";
                rbNewton.Visible = true;
                rbSpline.Visible = true;
                tbSquares.Visible = false;
                dgvPointsTable.Columns[2].Visible = false;
                label11.Visible = true;
                tbRoot.Visible = true;
            }
            else
            {
                gbMethod.Text = "Метод аппроксимации";
                rbNewton.Visible = false;
                rbSpline.Visible = false;
                tbSquares.Visible = true;
                dgvPointsTable.Columns[2].Visible = true;
                label11.Visible = false;
                tbRoot.Visible = false;
            }
            btnUpdate_Click(sender, e);
        }
    }
}
