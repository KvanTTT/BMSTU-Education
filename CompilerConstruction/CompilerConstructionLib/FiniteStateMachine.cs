using System.Collections.Generic;

namespace CompilerConstructionLib
{
	public class FiniteStateMachine
	{
		public List<FiniteStateMachineNode> Nodes;
		public FiniteStateMachineNode FirstNode;
		public List<FiniteStateMachineNode> LastNodes;

		public FiniteStateMachine(string expression)
		{
			var summands = SRECoefficient.Split(expression);
			Stack<int> leftBracketNodeNumbers = new Stack<int>();
			int multiplyPosition, oldPosition;

			Nodes = new List<FiniteStateMachineNode>();
			int currentNumber = 0;
			FirstNode = new FiniteStateMachineNode { Number = currentNumber++, Symbol = Helper.IdentitySymbol };
			Nodes.Add(FirstNode);
			LastNodes = new List<FiniteStateMachineNode>();

			foreach (var summand in summands)
			{
				var currentNode = new FiniteStateMachineNode { Number = currentNumber++ };
				Nodes.Add(currentNode);
				FirstNode.ChildrenNodes.Add(new KeyValuePair<string, FiniteStateMachineNode>(FirstNode.Symbol, currentNode));

				oldPosition = -1;

				bool endState = false;
				do
				{
					multiplyPosition = summand.IndexOf(Helper.Multiply, oldPosition + 1);
					if (multiplyPosition == -1)
						multiplyPosition = summand.Length; // Нужно учесть последнее слагаемое.
					
					if (multiplyPosition != -1)
					{
						if (summand[multiplyPosition - 1] == Helper.IterationSymbol)
						{
							if (summand[multiplyPosition - 2] == Helper.RightBracket)
							{
								if (summand[multiplyPosition - 3] == Helper.IterationSymbol)
								{
									var symbol = summand.Substring(oldPosition + 1, multiplyPosition - oldPosition - 4);
									currentNode.ChildrenNodes.Add(new KeyValuePair<string, FiniteStateMachineNode>(symbol, currentNode));
								}
								else
								{
									var newNode = new FiniteStateMachineNode { Number = currentNumber++ };
									var symbol = summand.Substring(oldPosition + 1, multiplyPosition - oldPosition - 3);
									currentNode.ChildrenNodes.Add(new KeyValuePair<string, FiniteStateMachineNode>(symbol, newNode));
									Nodes.Add(newNode);
									currentNode = newNode;
								}
								Nodes[leftBracketNodeNumbers.Peek()].ChildrenNodes.Add(new KeyValuePair<string, FiniteStateMachineNode>(Helper.IdentitySymbol, currentNode));
								currentNode.ChildrenNodes.Add(new KeyValuePair<string, FiniteStateMachineNode>(
									Nodes[leftBracketNodeNumbers.Peek() + 1].Symbol, Nodes[leftBracketNodeNumbers.Pop() + 1]));
								if (multiplyPosition == summand.Length)
									endState = true;
							}
							else
							{
								// Итерация узла на самого себя.
								if (multiplyPosition + 1 < summand.Length && summand[multiplyPosition + 1] == Helper.LeftBracket)
								{
									// Если следующий символ является скобкой, то нужно ввести дополнительный узел.
									var newNode = new FiniteStateMachineNode { Number = currentNumber++, Symbol = Helper.IdentitySymbol };
									currentNode = newNode;
								}
								else
								{
									var symbol = summand.Substring(oldPosition + 1, multiplyPosition - oldPosition - 2);
									currentNode.ChildrenNodes.Add(new KeyValuePair<string, FiniteStateMachineNode>(symbol, currentNode));
									if (multiplyPosition == summand.Length)
										endState = true;
								}
							}
						}
						else
						{
							// Не итерация.
							currentNode.Symbol = summand.Substring(oldPosition + 1, multiplyPosition - oldPosition - 1);
							if (multiplyPosition == summand.Length)
								endState = true;
						}

						if (!endState)
						{
							if (summand[multiplyPosition + 1] == Helper.LeftBracket)
							{
								leftBracketNodeNumbers.Push(currentNumber); // Запоминаем номер ноды.
								oldPosition = multiplyPosition + 1;
							}
							else
								oldPosition = multiplyPosition;

							if (summand[multiplyPosition - 1] != Helper.IterationSymbol)
							{
								var newNode = new FiniteStateMachineNode { Number = currentNumber++ };
								currentNode.ChildrenNodes.Add(new KeyValuePair<string,FiniteStateMachineNode>(currentNode.Symbol, newNode));
								Nodes.Add(newNode);
								currentNode = newNode;
							}
						}
					}
				}
				while (!endState);

				LastNodes.Add(currentNode);
			}
		}

		public bool CheckAccessory(string expression)
		{
			bool result = false;

			var summands = expression.Split(new char[] { Helper.Plus, Helper.Or });

			foreach (var summand in summands)
			{
				var factors = summand.Split(new char[] { Helper.Multiply, '.' });

				result = ContainsChain(factors);

				if (result)
					break;
			}

			return result;
		}

		private bool ContainsChain(string[] factors)
		{
			bool result = false;

			foreach (var node in FirstNode.ChildrenNodes)
			{
				ContainsChain(node.Value, FirstNode, node.Key, factors, 0, ref result);
				if (result)
					break;
			}

			return result;
		}

		private void ContainsChain(FiniteStateMachineNode currentNode, FiniteStateMachineNode prevNode,
			string key, string[] factors, int iterator, ref bool result)
		{
			if (!result)
			{
				if (iterator >= factors.Length)
					if (!LastNodes.Contains(prevNode))
						result = false;
					else
						result = true;
				else if (key != Helper.IdentitySymbol && factors[iterator] != key)
					result = false;
				else
				{
					foreach (var child in currentNode.ChildrenNodes)
						ContainsChain(child.Value, currentNode, child.Key, factors, key == Helper.IdentitySymbol ? iterator : iterator + 1, ref result);
				}
			}
		}
	}
}
