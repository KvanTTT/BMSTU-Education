using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using System;
//using System.Drawing;
using System.Windows.Forms;

namespace Task4
{
	public partial class Form1 : Form
	{
		RecursiveDescentParser Parser;
		Graph Graph;

		public Form1()
		{
			InitializeComponent();

			Parser = new RecursiveDescentParser();
		}

		private void btnCheckAccessory_Click(object sender, EventArgs e)
		{
			bool isGrammarContainsChain = Parser.ContainsString(tbExpression.Text);

			if (isGrammarContainsChain)
			{
				DrawGraph();
				lblAnswer.Text = "String is derivable from Grammar";
				lblAnswer.ForeColor = System.Drawing.Color.Green;
				lblAnswer.Visible = true;
			}
			else
			{
				lblAnswer.Text = "String is NOT derivable from Grammar";
				lblAnswer.ForeColor = System.Drawing.Color.Red;
				lblAnswer.Visible = true;
			}
		}

		private void DrawGraph()
		{
			Graph = new Graph("Parsing Tree");

			if (Parser.FirstNode != null)
				BypassTree(Parser.FirstNode, null);

			Viewer.Graph = Graph;
		}

		private void BypassTree(ParsingTreeNode node, ParsingTreeNode parentNode)
		{
			Microsoft.Msagl.Drawing.Edge graphEdge = null;
			if (parentNode != null)
			{
				graphEdge = Graph.AddEdge(parentNode.GetHashCode().ToString(), node.GetHashCode().ToString());
				graphEdge.SourceNode.Attr.Id = parentNode.Value;
				graphEdge.TargetNode.Attr.Id = node.Value;
			}
			else
			{
				var graphNode = Graph.AddNode(node.GetHashCode().ToString());
				graphNode.Attr.Id = node.Value;
				graphNode.Attr.FillColor = Color.MediumSeaGreen;
			}
			if (node.Childrens.Count == 0)
			{
				if (graphEdge != null)
					graphEdge.TargetNode.Attr.FillColor = new Color(0xFF, 0xFF, 0x99); // draw leafs;
			}
			else
			{
				foreach (var child in node.Childrens)
					BypassTree(child, node);
			}
		}
	}
}
