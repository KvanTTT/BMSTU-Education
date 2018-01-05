using System.Collections.Generic;
using System.Text;

namespace CompilerConstructionLib
{
	public class FiniteStateMachineNode
	{
		public string Symbol;

		public int Number;

		public List<KeyValuePair<string, FiniteStateMachineNode>> ChildrenNodes = new List<KeyValuePair<string,FiniteStateMachineNode>>();
		
		public override string ToString()
		{
			var result = new StringBuilder();

			result.AppendFormat("Symbol={0}; S{1}; Childs: ", Symbol, Number + 1);
			foreach (var child in ChildrenNodes)
				result.AppendFormat("S{0}, ", child.Value.Number + 1);

			return result.ToString();
		}
	}
}
