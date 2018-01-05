using System.Collections.Generic;
using System.Linq;

namespace CompilerConstructionLib
{
	/// <summary>
	/// Граф грамматики.
	/// </summary>
	public class GrammarGraph
	{
		Dictionary<string, GrammarGraphNode> Nodes;
		public Grammar Grammar
		{
			get;
			protected set;
		}

		public GrammarGraph(Grammar grammar)
		{
			Grammar = grammar.Clone();

			Nodes = new Dictionary<string, GrammarGraphNode>();

			Grammar.TerminalSymbols.ForEach(symbol =>
				Nodes.Add(symbol, new GrammarGraphNode
					{
						Type = enmSymbolType.Terminal
					}));

			Grammar.NonterminalSymbols.ForEach(symbol =>
				Nodes.Add(symbol, new GrammarGraphNode
				{
					Type = enmSymbolType.Nonterminal
				}));

			Grammar.Rules.ForEach(rool =>
				rool.RightPartArray.ToList().ForEach(word =>
					word.ForEach(symbol =>
						{
							if (Nodes.ContainsKey(rool.LeftPart))
								Nodes[rool.LeftPart].ConnectedNodes.Add(symbol);
						})));
		}

		/// <summary>
		/// Удаление бесполезных символов из графа и грамматики.
		/// </summary>
		public void RemoveUselessSymbols()
		{
			var markedSymbols = new HashSet<string>();

			Nodes.Where(node => node.Value.Type == enmSymbolType.Terminal).ToList().
				ForEach(node => markedSymbols.Add(node.Key));

			bool isRoolWithMarkedSymbolsExists;
			do
			{
				isRoolWithMarkedSymbolsExists = false;
				Grammar.Rules.Where(rool => !markedSymbols.Contains(rool.LeftPart)).ToList().ForEach(rool =>
					rool.RightPartArray.ToList().ForEach(word =>
					{
						if (word.All(symbol => markedSymbols.Contains(symbol)))
						{
							markedSymbols.Add(rool.LeftPart);
							isRoolWithMarkedSymbolsExists = true;
						}
					}));
			}
			while (isRoolWithMarkedSymbolsExists);

			var unmarkedSymbols = Nodes.Where(node => !markedSymbols.Contains(node.Key))
				.Select(node => node.Key).ToList();

			unmarkedSymbols.ForEach(symbol =>
				{
					Nodes.Remove(symbol);
					foreach (var node1 in Nodes)
						Nodes[node1.Key].ConnectedNodes.Remove(symbol);
					
					Grammar.NonterminalSymbols.Remove(symbol);
					if (symbol != Helper.IdentitySymbol && symbol != Helper.EmptySymbol)
						Grammar.TerminalSymbols.Remove(symbol);
					
					Rule rool;
					if ((rool = Grammar.Rules.FirstOrDefault(rool1 => rool1.LeftPart == symbol)) != null)
						Grammar.Rules.Remove(rool);
					Grammar.Rules.ForEach(rool2 => rool2.RemoveWordsContainsSymbol(symbol));
				});
		}

		/// <summary>
		/// Удаление недостижимых символов из графа и грамматики.
		/// </summary>
		public void RemoveUnreachableSymbols()
		{
			var markedSymbols = new HashSet<string>();

			markedSymbols.Add(Grammar.StartSymbol);

			List<string> newSymbols;

			int oldMarkedSymbolsLength;
			do
			{
				newSymbols = new List<string>();
				markedSymbols.ToList().ForEach(symbol =>
					{
						if (Nodes[symbol].Type == enmSymbolType.Nonterminal)
						{
							Nodes[symbol].ConnectedNodes.ToList().ForEach(node =>
								newSymbols.Add(node));
						}
					});
				oldMarkedSymbolsLength = markedSymbols.Count;
				newSymbols.ForEach(symbol => markedSymbols.Add(symbol));
			}
			while (markedSymbols.Count != oldMarkedSymbolsLength);

			var unmarkedSymbols = Nodes.Where(node => !markedSymbols.Contains(node.Key))
				.Select(node => node.Key).ToList();

			unmarkedSymbols.ForEach(symbol =>
			{
				Nodes.Remove(symbol);
				foreach (var node1 in Nodes)
					Nodes[node1.Key].ConnectedNodes.Remove(symbol);

				Grammar.NonterminalSymbols.Remove(symbol);
				if (symbol != Helper.IdentitySymbol && symbol != Helper.EmptySymbol)
					Grammar.TerminalSymbols.Remove(symbol);
				
				Rule rool;
				if ((rool = Grammar.Rules.FirstOrDefault(rool1 => rool1.LeftPart == symbol)) != null)
					Grammar.Rules.Remove(rool);
				Grammar.Rules.ForEach(rool2 => rool2.RemoveWordsContainsSymbol(symbol));
			});

			Grammar.Rules = Grammar.Rules.Except(Grammar.Rules.Where(rule2 => 
				{ return rule2.RightPartArray.Count == 0 || !Grammar.NonterminalSymbols.Contains(rule2.LeftPart); })).ToList();
		}

		public void RemoveEmptyRightPartRools()
		{
			// 1 step
			var markedSymbols = new HashSet<string>();
			markedSymbols.Add(Helper.IdentitySymbol);

			bool isRoolWithMarkedSymbolsExists;
			do
			{
				isRoolWithMarkedSymbolsExists = false;
				Grammar.Rules.Where(rool => !markedSymbols.Contains(rool.LeftPart)).ToList().ForEach(rool =>
					rool.RightPartArray.ToList().ForEach(word =>
					{
						if (word.All(symbol => markedSymbols.Contains(symbol)))
						{
							markedSymbols.Add(rool.LeftPart);
							isRoolWithMarkedSymbolsExists = true;
						}
					}));
			}
			while (isRoolWithMarkedSymbolsExists);

			// 2 step
			Nodes.Remove(Helper.IdentitySymbol);
			Nodes.ToList().ForEach(node => node.Value.ConnectedNodes.Remove(Helper.IdentitySymbol));

			Grammar.Rules =
				Grammar.Rules.Except(
					Grammar.Rules.Where(rool => rool.RightPart == Helper.IdentitySymbol)).ToList();

			Grammar.Rules.ForEach(rool => rool.RemoveWordsContainsSymbol(Helper.IdentitySymbol));

			// 3 step
			/*if (markedSymbols.Contains(Grammar.StartSymbol))
			{
				var newStartSymbol = Grammar.StartSymbol + Helper.NewStartSymbolPrefix;

				Nodes.Add(Helper.IdentitySymbol, new GrammarGraphNode
					{
						Type = enmSymbolType.Terminal
					});
				Nodes.Add(newStartSymbol, new GrammarGraphNode
					{
						Type = enmSymbolType.Nonterminal,
						ConnectedNodes = new HashSet<string>() 
						{ 
							Grammar.StartSymbol,
							Helper.IdentitySymbol
						}
					});

				Grammar.Rules.Add(new Rule(newStartSymbol,
					new List<List<string>>()
					{
						new List<string>() { Grammar.StartSymbol },
						new List<string>() { Helper.IdentitySymbol },
					}));
				Grammar.StartSymbol = newStartSymbol;
			}*/

			List<Rule> newRools = new List<Rule>();
			
			// 4 step
			Grammar.Rules.ForEach(rool => rool.RightPartArray.ToList().ForEach(word =>
				{
					Dictionary<int, MarkedNonterminal> nonterminalChainOccurrence = 
						new Dictionary<int, MarkedNonterminal>();
					for (int i = 0; i < word.Count(); i++)
					{
						if (markedSymbols.Contains(word[i]))
							nonterminalChainOccurrence.Add(i, new MarkedNonterminal()
								{
									Number = i,
									Nonterminal = word[i],
									Marked = false
								});
					}
					if (nonterminalChainOccurrence.Count != 0)
					{
						var newRool = new Rule()
						{
							LeftPart = rool.LeftPart
						};
						while (!nonterminalChainOccurrence.All(occur => occur.Value.Marked))
						{
							var newWorld = GetNewWord(word, nonterminalChainOccurrence);
							// Ignore empty right part and chains.
							if (newWorld.Count != 0 && !(newWorld.Count == 1 && newWorld[0] == rool.LeftPart))
								newRool.RightPartArray.Add(newWorld);
							NextCombination(nonterminalChainOccurrence);
						}
						newRools.Add(newRool);
					}
				}));

			newRools.ForEach(rool =>
				Grammar.Rules.FirstOrDefault(
					rool1 => rool1.LeftPart == rool.LeftPart)
					.RightPartArray
					.Union(rool.RightPartArray));
		}

		/// <summary>
		/// Удаление цепных правил (НЕ ДОДЕЛАНО).
		/// </summary>
		public void RemoveChainRools()
		{
			// 1 step
			var markedSymbols = new HashSet<string>();
			Nodes.Where(node => node.Value.Type == enmSymbolType.Nonterminal).ToList().
				ForEach(node => markedSymbols.Add(node.Key));

			bool isRoolWithMarkedSymbolsExists;
			do
			{
				isRoolWithMarkedSymbolsExists = false;
				Grammar.Rules.Where(rool => !markedSymbols.Contains(rool.LeftPart)).ToList().ForEach(rool =>
					rool.RightPartArray.ToList().ForEach(word =>
					{ 
						if (word.Count == 1 && Nodes[word[0]].Type == enmSymbolType.Nonterminal && 
							markedSymbols.Contains(word[0]))
						{
							markedSymbols.Add(rool.LeftPart);
							isRoolWithMarkedSymbolsExists = true;
						}
					}));
			}
			while (isRoolWithMarkedSymbolsExists);
		}

		#region Inernal methods & classes
		
		private class MarkedNonterminal
		{
			public int Number;
			public string Nonterminal;
			public bool Marked;
		}

		private void NextCombination(Dictionary<int, MarkedNonterminal> NonterminalChainOccurrence)
		{
			bool carryFlag = true;
			NonterminalChainOccurrence.ToList().ForEach(symbol =>
				{
					var oldSymbolMarked = carryFlag;
					symbol.Value.Marked = symbol.Value.Marked ^ carryFlag;
					carryFlag = oldSymbolMarked & carryFlag;
				});
		}

		private List<string> GetNewWord(List<string> word, Dictionary<int, MarkedNonterminal> combination)
		{
			var result = new List<string>();

			for (int i = 0; i < word.Count; i++)
			{
				if (combination.ContainsKey(i))
				{
					if (combination[i].Marked)
						result.Add(combination[i].Nonterminal);
				}
				else
					result.Add(word[i]);
			}

			return result;
		}

		#endregion
	}
}
