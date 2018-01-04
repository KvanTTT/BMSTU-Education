using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Task1
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			double[] startPoint = tbStartPoint.Text.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => Convert.ToDouble(s)).ToArray();
			var function = MathFormula.Parse(tbFormula.Text);
			var algorithm = new GradientDescentAlgorithm((int)udVariableCount.Value, startPoint, function)
			{
				Epsilon = Convert.ToDouble(tbEpsilon.Text),
				DeriveStep = Convert.ToDouble(tbDeriveStep.Text)
			};

			double minimum;
			double[] minimumVariables;

			algorithm.GetMinimum(out minimum, out minimumVariables);

			tbMinimum.Text = minimum.ToString("0.0000");
			var minimumVarsString = new StringBuilder(algorithm.VariableCount);
			minimumVariables.ToList().ForEach(var => minimumVarsString.Append(var.ToString("0.000") + "; "));
			tbMinimumVariables.Text = minimumVarsString.ToString();
		}
	}
}
