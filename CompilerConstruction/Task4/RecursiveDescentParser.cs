using System;
using System.Collections.Generic;

namespace Task4
{
	public class RecursiveDescentParser
	{
		private string InputExpression;
		private string[] Symbols;
		private int CurrentIndex;

		private readonly Dictionary<enmTerminalType, string> TerminalAbbreviations =
			new Dictionary<enmTerminalType, string>()
			{
				// Multiplication operations
				{ enmTerminalType.Mult, "*" },
				{ enmTerminalType.Divide, "/" },
				{ enmTerminalType.Div, "div" },
				{ enmTerminalType.Mod, "mod" },
				{ enmTerminalType.And, "and" },

				// Addition operations
				{ enmTerminalType.Plus, "+" },
				{ enmTerminalType.Minus, "-" },
				{ enmTerminalType.Or, "or" },

				// Sign
				{ enmTerminalType.PlusSign, "+" },
				{ enmTerminalType.MinusSign, "-" },

				// Relation operations
				{ enmTerminalType.Equal, "=" },
				{ enmTerminalType.NotEqual, "<>" },
				{ enmTerminalType.Less, "<" },
				{ enmTerminalType.LessOrEqual, "<=" },
				{ enmTerminalType.Greater, ">" },
				{ enmTerminalType.GreaterOrEqual, ">=" },

				{ enmTerminalType.Not, "not" },

				{ enmTerminalType.LeftBracket, "(" },
				{ enmTerminalType.RightBracket, ")" }
			};

		private readonly Dictionary<enmNonterminalType, string> NonterminalAbbreviations =
			new Dictionary<enmNonterminalType, string>()
			{
				{ enmNonterminalType.Expression, "Expr"},
				{ enmNonterminalType.SimpleExpression, "SimpExpr" },
				{ enmNonterminalType.RelationOperation, "RelatOp" },
				{ enmNonterminalType.Term, "Term" },
				{ enmNonterminalType.Sign, "Sign" },
				{ enmNonterminalType.AdditionOperation, "AddOp" },
				{ enmNonterminalType.Factor, "Factor" },
				{ enmNonterminalType.MultiplicationOperation, "MultOp" },
				{ enmNonterminalType.Identificator, "Id" },
				{ enmNonterminalType.Constant, "Const" },
			};

		private readonly Dictionary<enmNonterminalType, int> NonterminalRuleCount =
			new Dictionary<enmNonterminalType, int>()
			{
				{ enmNonterminalType.Expression, 2 },
				{ enmNonterminalType.SimpleExpression, 3 },
				{ enmNonterminalType.Term, 2 },
				{ enmNonterminalType.Factor, 4 }
			};

		public ParsingTreeNode FirstNode;

		public RecursiveDescentParser()
		{
		}

		public bool ContainsString(string expression)
		{
			InputExpression = expression.ToLowerInvariant();
			Symbols = InputExpression.Split(new char[] { '	', ' ' },
				StringSplitOptions.RemoveEmptyEntries);

			CurrentIndex = 0;
			
			FirstNode = Expression();

			return FirstNode == null ? false : true;
		}

		private ParsingTreeNode Expression()
		{
			ParsingTreeNode result = null;
			var index = CurrentIndex;
			ParsingTreeNode simpleExpressionNode;
			ParsingTreeNode relationOperationNode;
			ParsingTreeNode simpleExpressionNode2;

			if ((simpleExpressionNode = SimpleExpression()) != null)
			{
				if ((relationOperationNode = RelationOperation()) != null &&
				(simpleExpressionNode2 = SimpleExpression()) != null)
				{
					result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.Expression] };
					result.Childrens.Add(simpleExpressionNode);
					result.Childrens.Add(relationOperationNode);
					result.Childrens.Add(simpleExpressionNode2);
				}
				else
				{
					result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.Expression] };
					result.Childrens.Add(simpleExpressionNode);
				}
			}
			else
				CurrentIndex = index;

			return result;
		}

		private ParsingTreeNode SimpleExpression()
		{
			ParsingTreeNode result = null;
			var index = CurrentIndex;
			ParsingTreeNode termNode;
			ParsingTreeNode additionOperationNode;
			ParsingTreeNode termNode2;

			if ((termNode = Term()) != null)
			{
				if ((additionOperationNode = AdditionOperation()) != null &&
					(termNode2 = Term()) != null)
				{
					result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.SimpleExpression] };
					result.Childrens.Add(termNode);
					result.Childrens.Add(additionOperationNode);
					result.Childrens.Add(termNode2);
				}
				else
				{
					result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.SimpleExpression] };
					result.Childrens.Add(termNode);
				}
			}
			else
			{
				CurrentIndex = index;
				ParsingTreeNode signNode;
				if ((signNode = Sign()) != null && (termNode = Term()) != null)
				{
					if ((additionOperationNode = AdditionOperation()) != null &&
						(termNode2 = Term()) != null)
					{
						result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.SimpleExpression] };
						result.Childrens.Add(signNode);
						result.Childrens.Add(termNode);
						result.Childrens.Add(additionOperationNode);
						result.Childrens.Add(termNode2);
					}
					else
					{
						result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.SimpleExpression] };
						result.Childrens.Add(signNode);
						result.Childrens.Add(termNode);
					}
				}
				else
					CurrentIndex = index;
			}
		
			return result;
		}

		private ParsingTreeNode Term()
		{
			ParsingTreeNode result = null;
			var index = CurrentIndex;
			ParsingTreeNode factorNode;
			ParsingTreeNode multiplicationOperationNode;
			ParsingTreeNode factorNode2;

			if ((factorNode = Factor()) != null)
			{
				if ((multiplicationOperationNode = MultiplicationOperation()) != null &&
					(factorNode2 = Factor()) != null)
				{
					result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.Term] };
					result.Childrens.Add(factorNode);
					result.Childrens.Add(multiplicationOperationNode);
					result.Childrens.Add(factorNode2);
				}
				else
				{
					result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.Term] };
					result.Childrens.Add(factorNode);
				}
			}
			else
				CurrentIndex = index;
			return result;
		}

		private ParsingTreeNode Factor()
		{
			ParsingTreeNode result = null;
			var index = CurrentIndex;
			if (Accept(enmTerminalType.Id))
			{
				result = new ParsingTreeNode() 
					{ Value = NonterminalAbbreviations[enmNonterminalType.Factor] };
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = NonterminalAbbreviations[enmNonterminalType.Identificator] });
				result.Childrens[0].Childrens.Add(new ParsingTreeNode()
					{ Value = Symbols[CurrentIndex - 1] });
			}
			else if (Accept(enmTerminalType.Const))
			{
				result = new ParsingTreeNode() 
					{ Value = NonterminalAbbreviations[enmNonterminalType.Factor] };
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = NonterminalAbbreviations[enmNonterminalType.Constant] });
				result.Childrens[0].Childrens.Add(new ParsingTreeNode()
					{ Value = Symbols[CurrentIndex - 1] });
			}
			else
			{
				CurrentIndex = index;
				ParsingTreeNode simpleExpressionNode;
				if (Accept(enmTerminalType.LeftBracket))
				{
					if (((simpleExpressionNode = SimpleExpression()) != null) && Expect(enmTerminalType.RightBracket))
					{
						result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.Factor] };
						result.Childrens.Add(new ParsingTreeNode() { Value = TerminalAbbreviations[enmTerminalType.LeftBracket] });
						result.Childrens.Add(simpleExpressionNode);
						result.Childrens.Add(new ParsingTreeNode() { Value = TerminalAbbreviations[enmTerminalType.RightBracket] });
					}
				}
				else
				{
					CurrentIndex = index;
					ParsingTreeNode factorNode;
					if (Accept(enmTerminalType.Not) && ((factorNode = Factor()) != null))
					{
						result = new ParsingTreeNode() { Value = NonterminalAbbreviations[enmNonterminalType.Factor] };
						result.Childrens.Add(new ParsingTreeNode() { Value = TerminalAbbreviations[enmTerminalType.Not] });
						result.Childrens.Add(factorNode);
					}
					else
					{
						CurrentIndex = index;
					}
				}
			}
			return result;
		}

		private ParsingTreeNode RelationOperation()
		{
			ParsingTreeNode result = new ParsingTreeNode()
				{ Value = NonterminalAbbreviations[enmNonterminalType.RelationOperation] };
			if (Accept(enmTerminalType.Equal))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Equal] });
			}
			else if (Accept(enmTerminalType.NotEqual))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.NotEqual] });
			}
			else if (Accept(enmTerminalType.Less))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Less] });
			}
			else if (Accept(enmTerminalType.LessOrEqual))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.LessOrEqual] });
			}
			else if (Accept(enmTerminalType.Greater))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Greater] });
			}
			else if (Accept(enmTerminalType.GreaterOrEqual))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.GreaterOrEqual] });
			}
			else
			{
				result = null;
			}
			return result;
		}

		private ParsingTreeNode Sign()
		{
			ParsingTreeNode result = new ParsingTreeNode()
				{ Value = NonterminalAbbreviations[enmNonterminalType.Sign] };
			if (Accept(enmTerminalType.PlusSign))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.PlusSign] });
			}
			else if (Accept(enmTerminalType.MinusSign))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.MinusSign] });
			}
			else
			{
				result = null;
			}
			return result;
		}

		private ParsingTreeNode AdditionOperation()
		{
			ParsingTreeNode result = new ParsingTreeNode()
				{ Value = NonterminalAbbreviations[enmNonterminalType.AdditionOperation] };
			if (Accept(enmTerminalType.Plus))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Plus] });
			}
			else if (Accept(enmTerminalType.Minus))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Minus] });
			}
			else if (Accept(enmTerminalType.Or))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Or] });
			}
			else
			{
				result = null;
			}
			return result;
		}

		private ParsingTreeNode MultiplicationOperation()
		{
			ParsingTreeNode result = new ParsingTreeNode()
				{ Value = NonterminalAbbreviations[enmNonterminalType.MultiplicationOperation] };
			if (Accept(enmTerminalType.Mult))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Mult] });
			}
			else if (Accept(enmTerminalType.Divide))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Divide] });
			}
			else if (Accept(enmTerminalType.Div))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Div] });
			}
			else if (Accept(enmTerminalType.Mod))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.Mod] });
			}
			else if (Accept(enmTerminalType.And))
			{
				result.Childrens.Add(new ParsingTreeNode()
					{ Value = TerminalAbbreviations[enmTerminalType.And] });
			}
			else
			{
				result = null;
			}
			return result;
		}

		private bool Accept(enmTerminalType terminal)
		{
			if (CurrentIndex < Symbols.Length &&
				((terminal == enmTerminalType.Const && IsConst(Symbols[CurrentIndex])) ||
				(terminal == enmTerminalType.Id && IsId(Symbols[CurrentIndex])) ||
				(TerminalAbbreviations.ContainsKey(terminal) && TerminalAbbreviations[terminal] == Symbols[CurrentIndex])))
			{
				GetNextSymbol();
				return true;
			}
			else
				return false;
		}

		private bool Expect(enmTerminalType terminal)
		{
			if (Accept(terminal))
				return true;
			else
			{
				// Unexpected symbol
				return false;
			}
		}

		private void GetNextSymbol()
		{
			CurrentIndex++;
		}

		private bool IsId(string symbol)
		{
			double temp;
			if (!double.TryParse(symbol, out temp) && !TerminalAbbreviations.ContainsValue(symbol) &&
				symbol.Length > 0 && symbol[0] != '+' && symbol[0] != '-')
					return true;
			else
				return false;
		}

		private bool IsConst(string symbol)
		{
			double result;
			if (double.TryParse(symbol, out result))
				return true;
			else
				return false;
		}
	}
}
