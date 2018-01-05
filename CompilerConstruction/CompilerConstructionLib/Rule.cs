using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerConstructionLib
{
	/// <summary>
	/// Не логично сделано правило, потому что включает в себя не одно слово, а несколько слов, разделенных "|".
	/// Из-за этого приходится иногда делать лишний цикл.
	/// </summary>
	[Serializable]
	public class Rule
	{
		public Rule()
		{
			RightPartArray = new List<List<string>>();
		}

		public Rule(string leftPart, List<List<string>> rightPart)
		{
			LeftPart = leftPart;
			RightPartArray = rightPart;
		}

		public List<List<string>> RightPartArray
		{
			get;
			set;
		}

		public string LeftPart
		{
			get;
			set;
		}

		public string RightPart
		{
			get
			{
				StringBuilder builder = new StringBuilder();
				RightPartArray.ToList().ForEach(word => 
				{
					word.ForEach(symbol => builder.AppendFormat("{0}{1}", symbol, Helper.Multiply));
					builder.Remove(builder.Length - 1, 1);
					builder.Append("|");
				});
				if (builder.Length != 0)
					builder.Remove(builder.Length - 1, 1);
				return builder.ToString();
			}
		}

		public bool IsEmptyRightPart
		{
			get
			{
				return RightPart == Helper.EmptySymbol;
			}
		}

		public void RemoveWordsContainsSymbol(string Symbol)
		{
			RightPartArray = new List<List<string>>(RightPartArray.Except(
				RightPartArray.Where(word => word.Any(symbol => symbol == Symbol))));
		}

		public override string ToString()
		{
			return String.Format("{0} → {1}", LeftPart, RightPart);
		}
	}
}
