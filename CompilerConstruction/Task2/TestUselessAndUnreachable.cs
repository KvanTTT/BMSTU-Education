using System.Collections.Generic;

namespace CompilerConstructionLib
{
	public static class TestUselessAndUnreachable
	{
		public static Grammar GetGrammar()
		{
			Grammar g = new Grammar();
			g.TerminalSymbols = new List<string>()
			{
				"a", "b", "c", "d", "e", Helper.EmptySymbol
			};

			g.NonterminalSymbols = new List<string>()
			{
				"S", "A", "B", "C", "D"
			};

			g.Rules = new List<Rule>()
			{
				new Rule("S", new List<List<string>>() 
				{
					new List<string>() { "a", "A", "B"},
					new List<string>() { "C" }
				}),
				new Rule("D", new List<List<string>>() 
				{
					new List<string>() { "c", "D", "c"},
					new List<string>() { "d" }
				}),
				new Rule("C", new List<List<string>>() 
				{
					new List<string>() { "a", "C", "D"}
				}),
				new Rule("A", new List<List<string>>() 
				{
					new List<string>() { "a", "A" },
					new List<string>() { "a" },
					new List<string>() { Helper.EmptySymbol },
				}),
				new Rule("B", new List<List<string>>()
				{
					new List<string>() { "b" }
				})
			};

			g.StartSymbol = "S";

			return g;
		}
	}
}
