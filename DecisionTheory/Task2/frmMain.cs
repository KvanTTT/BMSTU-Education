using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Task1;
using Task2.Properties;

namespace Task2
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			var algorithm = new GeneticAlgorithm()
			{
				VariableCount = (int)udVariableCount.Value,
				Function = MathFormula.Parse(tbFormula.Text),
				StartPoint = tbStartPoint.Text.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(s => Convert.ToDouble(s)).ToArray(),
				SemiWidth = Convert.ToDouble(tbAreaSemiwidth.Text),
				PopulationSize = Convert.ToInt32(udPopulationSize.Value),
				ChildrenRatio = Convert.ToDouble(tbChildrenRatio.Text),
				MutationProbability = Convert.ToDouble(tbMutationProbability.Text),
				MutationOffset = Convert.ToDouble(tbMutationOffset.Text),
				MaxRepeatCount = (int)udRepeatCount.Value
			};

			double minimum;
			double[] minimumVariables;

			algorithm.GetMinimum(out minimum, out minimumVariables);

			tbMinimum.Text = minimum.ToString("0.0000");
			var minimumVarsString = new StringBuilder(algorithm.VariableCount);
			minimumVariables.ToList().ForEach(var => minimumVarsString.Append(var.ToString("0.000") + "; "));
			tbMinimumVariables.Text = minimumVarsString.ToString();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			udVariableCount.Value = Settings.Default.VariableCount;
			tbFormula.Text = Settings.Default.Formula;
			tbStartPoint.Text = Settings.Default.StartPoint;
			tbAreaSemiwidth.Text = Settings.Default.AreaSemiwidth.ToString();
			udPopulationSize.Value = Settings.Default.PopulationSize;
			tbChildrenRatio.Text = Settings.Default.ChildrenRatio.ToString();
			tbMutationProbability.Text = Settings.Default.MutationProbability.ToString();
			tbMutationOffset.Text = Settings.Default.MutationOffset.ToString();
			udRepeatCount.Value = Settings.Default.MaxRepeatCount;
		}

		private void udVariableCount_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.VariableCount = (int)udVariableCount.Value;
			Settings.Default.Save();
		}

		private void tbFormula_Leave(object sender, EventArgs e)
		{
			Settings.Default.Formula = tbFormula.Text;
			Settings.Default.Save();
		}

		private void tbStartPoint_Leave(object sender, EventArgs e)
		{
			Settings.Default.StartPoint = tbStartPoint.Text;
			Settings.Default.Save();
		}

		private void tbAreaSemiwidth_Leave(object sender, EventArgs e)
		{
			Settings.Default.AreaSemiwidth = double.Parse(tbAreaSemiwidth.Text);
			Settings.Default.Save();
		}

		private void udPopulationSize_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.PopulationSize = (int)udPopulationSize.Value;
			Settings.Default.Save();
		}

		private void tbChildrenRatio_Leave(object sender, EventArgs e)
		{
			Settings.Default.ChildrenRatio = double.Parse(tbChildrenRatio.Text);
			Settings.Default.Save();
		}

		private void tbMutationProbability_Leave(object sender, EventArgs e)
		{
			Settings.Default.MutationProbability = double.Parse(tbMutationProbability.Text);
			Settings.Default.Save();
		}

		private void tbMutationOffset_Leave(object sender, EventArgs e)
		{
			Settings.Default.MutationOffset = double.Parse(tbMutationOffset.Text);
			Settings.Default.Save();
		}

		private void udRepeatCount_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.MaxRepeatCount = (int)udRepeatCount.Value;
			Settings.Default.Save();
		}
	}
}
