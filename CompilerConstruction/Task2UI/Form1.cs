using CompilerConstructionLib;
using Microsoft.Msagl.Drawing;
using System;
using System.IO;
using System.Windows.Forms;

namespace Task2UI
{
	public partial class form1 : Form
	{
		Grammar Grammar;
		SystemOfRegularEquations SRE;
		FiniteStateMachine finiteStateMachine;

		public form1()
		{
			InitializeComponent();

			ofgOpenGrammar.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
		}

		private void btnSolve_Click(object sender, EventArgs e)
		{
			try
			{
				Grammar = Grammar.Parse(tbInputGrammar.Text);
			}
			catch (Exception exception)
			{
				MessageBox.Show("Wrong input: " + exception.Message);
			}

			try
			{
				var graph = new GrammarGraph(Grammar);
				/*
				graph.RemoveEmptyRightPartRools();
				graph.RemoveUselessSymbols();
				graph.RemoveUnreachableSymbols();*/
				
				tbOutputGrammar.Text = graph.Grammar.ToString();

				SRE = new SystemOfRegularEquations(graph.Grammar);

				tbCoefficients.Text = SRE.ToString();

				tbSolution.Text = string.Empty;
				var result = SRE.Solve();
				for (int i = 0; i < result.Length; i++)
					tbSolution.Text += String.Format("{0}{1} = {2}{3}", Helper.VarName, i + 1, result[i].Symbol, Environment.NewLine);

				finiteStateMachine = new FiniteStateMachine(result[0].Symbol);

				var nda = new Graph("NDA");
				foreach (var node in finiteStateMachine.Nodes)
					foreach (var children in node.ChildrenNodes)
					{
						var edge = nda.AddEdge("S" + (node.Number + 1).ToString(),
											   "S" + (children.Value.Number + 1).ToString());
						edge.Attr.Id = children.Key;
					}

				nda.FindNode("S1").Attr.FillColor = Color.MediumSeaGreen;

				foreach (var node in finiteStateMachine.LastNodes)
					nda.FindNode("S" + (node.Number + 1)).Attr.FillColor = new Color(0xFF, 0xFF, 0x99);

				ndaViewer.Graph = nda;

				lblAnswer.Visible = false;
			}
			catch (Exception exception)
			{
				MessageBox.Show("Incorrect input grammar: " + exception.Message);
			}
		}

		private void btnLoadGrammar_Click(object sender, EventArgs e)
		{
			if (ofgOpenGrammar.ShowDialog() == DialogResult.OK)
				tbInputGrammar.Text = File.ReadAllText(ofgOpenGrammar.FileName);
		}

		private void btnCheckAccessory_Click(object sender, EventArgs e)
		{
			if (finiteStateMachine == null)
				MessageBox.Show("Build automata at first!");
			else
			{
				bool isGrammarContainsChain = finiteStateMachine.CheckAccessory(tbInputString.Text);

				if (isGrammarContainsChain)
				{
					lblAnswer.Text = "String is derivable from regular expression";
					lblAnswer.ForeColor = System.Drawing.Color.Green;
					lblAnswer.Visible = true;
				}
				else
				{
					lblAnswer.Text = "String is NOT derivable from regular expresison";
					lblAnswer.ForeColor = System.Drawing.Color.Red;
					lblAnswer.Visible = true;
				}
			}
		}
	}
}
