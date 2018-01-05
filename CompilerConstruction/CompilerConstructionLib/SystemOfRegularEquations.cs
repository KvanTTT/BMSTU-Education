using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerConstructionLib
{
	public class SystemOfRegularEquations
	{
		public SRECoefficient[,] Coefs;

		public int EquationCount
		{
			get;
			private set;
		}

		public SystemOfRegularEquations(Grammar grammar)
		{
			EquationCount = grammar.NonterminalSymbols.Count;
			Coefs = new SRECoefficient[EquationCount, EquationCount + 1];

			for (int i = 0; i < EquationCount; i++)
			{
				var freeCoef =
					grammar.Rules[i].RightPartArray.FirstOrDefault(
						word => word.All(
							symbol => !grammar.NonterminalSymbols.Contains(symbol)));
				if (freeCoef == null)
					Coefs[i, EquationCount] = Helper.EmptySymbol;
				else
				{
					var symbol = string.Empty;
					freeCoef.Except(grammar.NonterminalSymbols).ToList().ForEach(coef => symbol += coef + Helper.Plus);
					symbol = symbol.Remove(symbol.Length - 1);
					Coefs[i, EquationCount].Symbol = symbol;
				}
				
				for (int j = 0; j < EquationCount; j++)
				{
					var terminalCoef = 
						grammar.Rules[i].RightPartArray.FirstOrDefault(
						word => word.Contains(grammar.NonterminalSymbols[j]));
					if (terminalCoef == null)
						Coefs[i, j] = Helper.EmptySymbol;
					else
					{
						var symbol = string.Empty;
						terminalCoef.ForEach(coef => symbol += grammar.NonterminalSymbols.Contains(coef) ? "" : coef + Helper.Multiply);
						symbol = symbol.Remove(symbol.Length - 1);
						Coefs[i, j].Symbol = symbol;
					}
				}
			}
		}

		public SRECoefficient[] Solve()
		{
			for (int i = 0; i < EquationCount; i++)
			{
				var iteration = Coefs[i, i].Iteration();
				for (int j = EquationCount; j >= i; j--)
					Coefs[i, j] = iteration * Coefs[i, j]; // optimization of expression Coefs[i, j] /= Coefs[i, i];
				for (int j = i + 1; j < EquationCount; j++)
					for (int k = EquationCount; k >= 0; k--)
						Coefs[j, k] += Coefs[j, i] * Coefs[i, k];
			}

			var result = new SRECoefficient[EquationCount];

			for (int i = EquationCount - 1; i >= 0; i--)
			{
				var Sum = new SRECoefficient(Helper.EmptySymbol);
				for (int j = i + 1; j < EquationCount; j++)
				{
					Sum += Coefs[i, j] * result[j];
					//Sum += SRECoefficient.SimpleMultiplication(Coefs[i, j], result[j]);
				}
				result[i] = Coefs[i, EquationCount] + Sum;
				//result[i] = SRECoefficient.SimpleAddition(Coefs[i, EquationCount], Sum);
			}

			return result;
		}

		public override string ToString()
		{
			var result = new StringBuilder(EquationCount * (EquationCount + 1) * 6);

			result.Append("      ");
			for (int i = 0; i < EquationCount; i++)
				result.AppendFormat("{0,6}", Helper.VarName + (i + 1).ToString());
			result.AppendLine();
			for (int i = 0; i < EquationCount; i++)
			{
				result.Append(Helper.VarName + (i + 1).ToString());
				for (int j = 0; j < EquationCount + 1; j++)
					result.AppendFormat("{0,6}", Coefs[i, j].Symbol);
				result.AppendFormat(Environment.NewLine);
			}

			return result.ToString();
		}
	}
}
