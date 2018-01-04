using AdvanceMath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Interpolation
{
	public partial class frmPointsGen : Form
    {
        Formula formula;

        public frmPointsGen()
        {
            InitializeComponent();

            try
            {
                StreamReader Reader = new StreamReader("PointsGen.dat");
                cmbFunc.Text = Reader.ReadLine();
                udPointsCount.Value = decimal.Parse(Reader.ReadLine());
                radioButton1.Checked = bool.Parse(Reader.ReadLine());
                radioButton2.Checked = bool.Parse(Reader.ReadLine());
                textBox1.Text = Reader.ReadLine();
                tbXL.Text = Reader.ReadLine();
                tbXR.Text = Reader.ReadLine();
                Reader.Close();

                formula = new Formula(cmbFunc.Text);
            }
            catch { }
        }

        public DoubleFunc GetFunc(int Number)
        {
            return formula.Calc;
        }

        public List<PointD> GeneratePoints(ref int FuncNumber)
        {
            List<PointD> Result = new List<PointD>((int)udPointsCount.Value);
            //DoubleFunc AccurateFunc = GetFunc(cmbFunc.SelectedIndex);

            formula = new Formula(cmbFunc.Text);
            double XL = Convert.ToDouble(tbXL.Text);
            double XR = Convert.ToDouble(tbXR.Text);
            double h = (XR - XL) / (double)(udPointsCount.Value - 1);
            double x = XL;
            for (int i = 0; i < udPointsCount.Value; i++)
            {
                Result.Add(new PointD(x, formula.Calc(x)));
                x += h;
            }
            
            return Result;
        }

        private void frmPointsGen_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamWriter Writer = new StreamWriter("PointsGen.dat");
            Writer.WriteLine(cmbFunc.Text);
            Writer.WriteLine(udPointsCount.Value);
            Writer.WriteLine(radioButton1.Checked);
            Writer.WriteLine(radioButton2.Checked);
            Writer.WriteLine(textBox1.Text);
            Writer.WriteLine(tbXL.Text);
            Writer.WriteLine(tbXR.Text);
            Writer.Close();
        }
    }
}
