using CompilerConstructionLib;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task1
{
	public partial class Form1 : Form
	{
		Grammar InputGrammar;

		public Form1()
		{
			InitializeComponent();

			ofgOpenGrammar.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
		}

		private void btnSplit_Click(object sender, EventArgs e)
		{
			try
			{
				InputGrammar = Grammar.Parse(tbInputGrammar.Text);
			}
			catch (Exception exception)
			{
				MessageBox.Show("Wrong input: " + exception.Message);
			}

			tbResult.Clear();

			var splittingSymbols = tbSplittingSymbols.Text.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
			
			var processedGrammar = InputGrammar.Clone();

			processedGrammar.NonterminalSymbols = InputGrammar.NonterminalSymbols
				.Except(splittingSymbols).ToList();

			processedGrammar.TerminalSymbols = InputGrammar.TerminalSymbols
				.Union(splittingSymbols.Select(symbol => symbol + Helper.NewStartSymbolSuffix)).ToList();

			foreach (var rule in processedGrammar.Rules)
				foreach (var word in rule.RightPartArray)
					foreach (var splitSymbol in splittingSymbols)
						for (int i = 0; i < word.Count; i++)
							word[i] = word[i].Replace(splitSymbol, splitSymbol + Helper.NewStartSymbolSuffix);

			Grammar[] resultGrammars = new Grammar[splittingSymbols.Length];

			for (int i = 0; i < splittingSymbols.Length; i++)
			{
				resultGrammars[i] = processedGrammar.Clone();

				resultGrammars[i].NonterminalSymbols.Add(splittingSymbols[i]);

				resultGrammars[i].StartSymbol = splittingSymbols[i];

				var grammarGraph = new GrammarGraph(resultGrammars[i]);
				grammarGraph.RemoveUselessSymbols();
				grammarGraph.RemoveUnreachableSymbols();


				resultGrammars[i] = grammarGraph.Grammar;

				tbResult.Text += resultGrammars[i].ToString() + Environment.NewLine + Environment.NewLine;
			}
		}

		private void btnLoadGrammar_Click(object sender, EventArgs e)
		{
			if (ofgOpenGrammar.ShowDialog() == DialogResult.OK)
				tbInputGrammar.Text = File.ReadAllText(ofgOpenGrammar.FileName);
		}
	}
}
