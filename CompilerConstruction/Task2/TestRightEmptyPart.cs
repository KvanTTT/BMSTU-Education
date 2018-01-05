using System.Collections.Generic;

namespace CompilerConstructionLib
{
	public static class TestRightEmptyPart
	{
		public static Grammar GetGrammar()
		{
			Grammar g = new Grammar();
			g.TerminalSymbols = new List<string>()
			{
				"a", "b", "c", Helper.EmptySymbol
			};

			g.NonterminalSymbols = new List<string>()
			{
				"S", "A", "B", "C",
			};

			g.Rules = new List<Rule>()
			{
				new Rule("S", new List<List<string>>() 
				{
					new List<string>() { "B", "C"},
					new List<string>() { "A", "b" }
				}),
				new Rule("B", new List<List<string>>() 
				{
					new List<string>() { Helper.EmptySymbol }
				}),
				new Rule("C", new List<List<string>>() 
				{
					new List<string>() { "c"}
				}),
				new Rule("A", new List<List<string>>() 
				{
					new List<string>() { "A", "a" },
					new List<string>() { Helper.EmptySymbol },
				})
			};

			g.StartSymbol = "S";

			return g;
		}
	}
}
