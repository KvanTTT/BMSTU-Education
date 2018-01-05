using CompilerConstructionLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
	public class CYKParser
	{
		private const int Indent = 8;

		Grammar Grammar;
		string[] InputSymbols;

		public string[,] VTable;

		public CYKParser(Grammar grammar, string inputString)
		{
			Grammar = grammar;
			InputSymbols = inputString.Split(new char[] { Helper.Multiply, '.', ' ' });

			VTable = new string[InputSymbols.Length, InputSymbols.Length];
			for (int i = 0; i < InputSymbols.Length; i++)
				for (int j = 0; j < InputSymbols.Length; j++)
					VTable[i, j] = "";

			VTable[0, 0] = "";
			for (int i = 0; i < InputSymbols.Length; i++)
				for (int j = 0; j < Grammar.Rules.Count; j++)
					for (int k = 0; k < Grammar.Rules[j].RightPartArray.Count; k++)
						if (InputSymbols[i] == Grammar.Rules[j].RightPartArray[k][0])
							if (string.IsNullOrEmpty(VTable[i, 0]))
								VTable[i, 0] = Grammar.Rules[j].LeftPart;
							else
								VTable[i, 0] += Helper.Multiply + Grammar.Rules[j].LeftPart;

			for (int row = 1; row < InputSymbols.Length; row++)
				for (int col = 0; col < InputSymbols.Length - row; col++)
					for (int k = 0; k < row; k++)
						for (int m = 0; m < Grammar.Rules.Count; m++)
							for (int l = 0; l < Grammar.Rules[m].RightPartArray.Count; l++)
							{
								var productionRules = Grammar.Rules[m].RightPartArray[l];
								var leftPart = Grammar.Rules[m].LeftPart;

								if (productionRules.Count > 1)
									if (!string.IsNullOrEmpty(VTable[col + k + 1, row - k - 1]) &&
										!string.IsNullOrEmpty(VTable[col, k]))
										if (VTable[col, k].IndexOf(productionRules[0]) != -1 &&
											VTable[col + k + 1, row - k - 1].IndexOf(productionRules[1]) != -1)
											CheckDuplicates(col, row, leftPart, productionRules);
							}
		}

		private void CheckDuplicates(int col, int row, string leftPart, List<string> productionRules)
		{
			if (!string.IsNullOrEmpty(VTable[col, row]))
			{
				bool present = false;

				for (int z = 0; z < VTable[col, row].Length; z++)
					if (VTable[col, row].IndexOf(leftPart) != -1)
						present = true;

				if (!present)
					VTable[col, row] += Helper.Multiply + leftPart;
			}
			else
				VTable[col, row] = leftPart;
		}

		public bool IsInGrammar()
		{
			return VTable[0, InputSymbols.Length - 1].IndexOf(Grammar.StartSymbol) != -1;
		}

		public override string ToString()
		{
			var result = new StringBuilder(1024);
			result.Append("VTable:" + Environment.NewLine);
			for (int i = 0; i < InputSymbols.Length; i++)
			{
				for (int j = 0; j < InputSymbols.Length; j++)
				{
					if (string.IsNullOrEmpty(VTable[i, j]) && i + j < InputSymbols.Length)
						VTable[i, j] = "null";
					result.Append(String.Format("{0," + Indent + "}", VTable[i, j]));
				}
				result.AppendLine();
			}

			if (IsInGrammar())
				result.AppendLine("Input string is in L(G)");
			else
				result.AppendLine("This string is not a member of L(G)");

			return result.ToString();
		}
	}
}
