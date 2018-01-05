using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerConstructionLib
{
	[Serializable]
	public class Grammar
	{
		public List<string> TerminalSymbols
		{
			get;
			set;
		}

		public List<string> NonterminalSymbols
		{
			get;
			set;
		}
		
		public List<Rule> Rules
		{
			get;
			set;
		}

		public string StartSymbol
		{
			get;
			set;
		}

		public Grammar RemoveEmptyRightPartRools()
		{
			Grammar result = (Grammar)this.MemberwiseClone();

			HashSet<string> NonterminalsLeadToEmptyTerminals = new HashSet<string>();

			var emptyRightPartRools = result.Rules.Where(rool => rool.IsEmptyRightPart);

			return result;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			TerminalSymbols.ForEach(symbol =>
				{
					if (symbol != Helper.IdentitySymbol && symbol != Helper.EmptySymbol)
						result.Append(String.Format("{0}, ", symbol));
				});
			result.Remove(result.Length - 2, 2);
			result.Append(";" + Environment.NewLine);
			NonterminalSymbols.ForEach(symbol => result.Append(String.Format("{0}, ", symbol)));
			result.Remove(result.Length - 2, 2);
			result.Append(";" + Environment.NewLine);
			result.Append(Rules.GetString());
			result.Append(";" + Environment.NewLine);
			result.Append(StartSymbol);
			return result.ToString();
		}

		public static Grammar Parse(string str)
		{
			var result = new Grammar();
			str = str.Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty);
			var grammarComponents = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

			var terminalSymbols = grammarComponents[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var symbol in terminalSymbols)
				result.TerminalSymbols.Add(symbol);
			result.TerminalSymbols.Add(Helper.IdentitySymbol);
			result.TerminalSymbols.Add(Helper.EmptySymbol);

			var nonterminalSymbols = grammarComponents[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var symbol in nonterminalSymbols)
				result.NonterminalSymbols.Add(symbol);

			var rules = grammarComponents[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var rule in rules)
			{
				//var ruleParts = rule.Split(new char[] { Helper.Arrow, '-' });
				var arrowIndex = rule.IndexOfAny(new char[] { Helper.Arrow, '-' });
				var ruleParts = new string[2];
				ruleParts[0] = rule.Substring(0, arrowIndex).Trim();
				ruleParts[1] = rule.Substring(arrowIndex + 1).Trim();
				var words = ruleParts[1].Split(new char[] { Helper.Or });
				var rightPart = new List<List<string>>();
				foreach (var word in words)
					rightPart.Add(word.Split(new char[] { Helper.Multiply, '.' }).ToList());
				result.Rules.Add(new Rule(ruleParts[0], rightPart));
			}

			result.StartSymbol = grammarComponents[3];

			return result;
		}

		public Grammar()
		{
			TerminalSymbols = new List<string>();
			NonterminalSymbols = new List<string>();
			Rules = new List<Rule>();
		}
	}
}
