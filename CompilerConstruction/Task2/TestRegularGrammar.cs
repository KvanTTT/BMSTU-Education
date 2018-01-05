using System.Collections.Generic;

namespace CompilerConstructionLib
{
	public static class TestRegularGrammar
	{
		public static Grammar GetGrammar()
		{
			Grammar g = new Grammar();
			g.TerminalSymbols = new List<string>()
			{
				"0", "1", Helper.EmptySymbol, Helper.IdentitySymbol
			};

			g.NonterminalSymbols = new List<string>()
			{
				"S", "A", "B"
			};

			g.Rules = new List<Rule>()
			{
				new Rule("S", new List<List<string>>() 
				{
					new List<string>() { "0", "A"},
					new List<string>() { "1", "S" },
					new List<string>() { Helper.IdentitySymbol },
				}),
				new Rule("A", new List<List<string>>() 
				{
					new List<string>() { "0", "B"},
					new List<string>() { "1", "A" },
				}),
				new Rule("B", new List<List<string>>() 
				{
					new List<string>() { "0", "S"},
					new List<string>() { "1", "B" },
				})
			};

			g.StartSymbol = "S";

			return g;
		}
	}
}
