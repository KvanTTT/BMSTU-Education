using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerConstructionLib
{
	public class GrammarGraphNode
	{
		public enmSymbolType Type;

		public HashSet<string> ConnectedNodes = new HashSet<string>();

		public override string ToString()
		{
			if (ConnectedNodes.Count == 0)
				return "";
			else
			{
				StringBuilder result = new StringBuilder("{");
				ConnectedNodes.ToList().ForEach(node => result.Append(node + ", "));
				result.Remove(result.Length - 2, 2);
				result.Append("}");
				return result.ToString();
			}
		}
	}
}
