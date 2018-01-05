using CompilerConstructionLib;
using System;
using System.IO;
using System.Windows.Forms;

namespace Task3
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			ofgOpenGrammar.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var grammar = Grammar.Parse(tbInputGrammar.Text);

			var parser = new CYKParser(grammar, tbExpression.Text);

			dataGridView1.RowCount = parser.VTable.GetLength(0);
			dataGridView1.ColumnCount = parser.VTable.GetLength(1);
			
			for (int i = 0; i < parser.VTable.GetLength(0); i++)
			{
				for (int j = 0; j < parser.VTable.GetLength(1); j++)
				{
					if (i + j < parser.VTable.GetLength(0))
					{
						if (string.IsNullOrEmpty(parser.VTable[j, i]))
							dataGridView1[j, i].Value = "null";
						else
							dataGridView1[j, i].Value = parser.VTable[j, i];
					}
				}
			}

			if (parser.IsInGrammar())
			{
				lblAnswer.Text = "Grammar contains string";
				lblAnswer.ForeColor = System.Drawing.Color.Green;
				lblAnswer.Visible = true;
			}
			else
			{
				lblAnswer.Text = "Grammar has not contains string";
				lblAnswer.ForeColor = System.Drawing.Color.Red;
				lblAnswer.Visible = true;
			}
		}

		private void btnLoadGrammar_Click(object sender, EventArgs e)
		{
			if (ofgOpenGrammar.ShowDialog() == DialogResult.OK)
				tbInputGrammar.Text = File.ReadAllText(ofgOpenGrammar.FileName);
		}
	}
}
