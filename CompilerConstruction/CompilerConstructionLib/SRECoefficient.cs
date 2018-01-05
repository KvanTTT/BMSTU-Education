using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerConstructionLib
{
	/// <summary>
	/// Представляет коэффициент в системе регулярных уравнений.
	/// Он может включать в себя операции "+", "∙", "*", а также парные "(" и ")", причем внутри них не может быть оператора "+".
	/// Это сделано для облегчения вычисления и оптимизации этих коэффициентов.
	/// </summary>
	public struct SRECoefficient
	{
		public string Symbol;

		public SRECoefficient(string symbol)
		{
			Symbol = symbol;
		}

		#region Operators overrides

		/// <summary>
		/// Не уверен, что правильно реализовал унарный минус. Но он нигде и не используется.
		/// </summary>
		/// <param name="coefficient"></param>
		/// <returns></returns>
		public static SRECoefficient operator -(SRECoefficient coefficient)
		{
			var summands = Split(coefficient);
			var result = new StringBuilder();

			foreach (var summand in summands)
			{
				var factors = summand.Split(new char[] { Helper.Multiply });
				for (int i = 0; i < factors.Length; i++)
					result.AppendFormat("{0}{1}", factors[factors.Length - 1 - i], Helper.Multiply);
				if (factors.Length != 0)
					result.Remove(result.Length - 1, 1);
				result.Append(Helper.Plus);
			}

			if (result.Length != 0)
				result.Remove(result.Length - 1, 1);
			else
				result.Append(Helper.EmptySymbol);

			return new SRECoefficient(result.ToString());
		}
		
		public static SRECoefficient operator +(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			var summands1 = Split(coefficient1);
			var summands2 = Split(coefficient2);

			StringBuilder result = new StringBuilder();

			foreach (var summand in summands1)
				if (!summand.Contains(Helper.EmptySymbol))
					result.AppendFormat("{0}{1}", summand, Helper.Plus);

			foreach (var summand in summands2)
				if (!summand.Contains(Helper.EmptySymbol))
					result.AppendFormat("{0}{1}", summand, Helper.Plus);

			if (result.Length != 0)
				result.Remove(result.Length - 1, 1);
			else
				result.Append(Helper.EmptySymbol);

			return new SRECoefficient(result.ToString());
		}

		public static SRECoefficient operator -(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			return coefficient1 + coefficient2;
		}

		public static SRECoefficient operator *(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			var summands1 = Split(coefficient1);
			var summands2 = Split(coefficient2);

			StringBuilder result = new StringBuilder();

			foreach (var summand1 in summands1)
				foreach (var summand2 in summands2)
					if (!summand1.Contains(Helper.EmptySymbol) && !summand2.Contains(Helper.EmptySymbol))
					{
						if (summand1.Contains(Helper.IdentitySymbol) && summand2.Contains(Helper.IdentitySymbol))
							result.AppendFormat("{0}{1}", Helper.IdentitySymbol, Helper.Plus);
						else
							if (summand1.Contains(Helper.IdentitySymbol))
								result.AppendFormat("{0}{1}", summand2, Helper.Plus);
							else
								if (summand2.Contains(Helper.IdentitySymbol))
									result.AppendFormat("{0}{1}", summand1, Helper.Plus);
								else
								{
									// Iteration optimization.
									/*if (summand1[summand1.Length - 1] == Helper.IterationSymbol && (
										summand2[0] == Helper.LeftBracket ||
										(summand2.IndexOf(Helper.Multiply) != -1 &&
											summand2[summand2.IndexOf(Helper.Multiply) - 1] == Helper.IterationSymbol)))
									{
										var factor = LastFactor(summand1);
										if (factor == FirstFactor(summand2))
										{
											string leftFactor = summand1;
											string rightFactor = summand2;
											RemoveFactors(ref leftFactor, ref rightFactor);

											string temp = string.Empty;
											if (!factor.Contains(Helper.Multiply))
												temp = string.Format("{0}{1}{2}{3}{4}{5}",
													leftFactor, Helper.Multiply, factor, Helper.IterationSymbol,
													Helper.Multiply, rightFactor);
											else
												temp = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
													leftFactor, Helper.Multiply, Helper.LeftBracket,
													factor, Helper.Multiply, Helper.RightBracket,
													Helper.IterationSymbol, rightFactor);

											if (temp[0] == Helper.Multiply)
												temp = temp.Substring(1);
											if (temp[temp.Length - 1] == Helper.Multiply)
												temp = temp.Remove(temp.Length - 1, 1);
											result.Append(temp + Helper.Plus);
										}
										else
											result.AppendFormat("{0}{1}{2}{3}", summand1, Helper.Multiply, summand2, Helper.Plus);
									}
									else*/
										result.AppendFormat("{0}{1}{2}{3}", summand1, Helper.Multiply, summand2, Helper.Plus);
								}
					}

			if (result.Length != 0)
				result.Remove(result.Length - 1, 1);
			else
				result.Append(Helper.EmptySymbol);

			return new SRECoefficient(result.ToString());
		}

		public static SRECoefficient operator /(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			return coefficient2.Iteration() * coefficient1;
		}

		public static bool operator ==(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			return coefficient1.Symbol == coefficient2.Symbol;
		}

		public static bool operator !=(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			return coefficient1.Symbol != coefficient2.Symbol;
		}

		public static implicit operator SRECoefficient(string symbol)
		{
			return new SRECoefficient(symbol);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Оператор "*" (Итерация).
		/// </summary>
		/// <returns></returns>
		public SRECoefficient Iteration()
		{
			var result = new StringBuilder(Symbol.Length);

			if (!Symbol.Contains(Helper.Multiply))
				result.AppendFormat("{0}{1}{2}", Symbol, Helper.IterationSymbol, Helper.Plus);
			else
				result.AppendFormat("{0}{1}{2}{3}{4}", Helper.LeftBracket, Symbol, Helper.RightBracket,
					Helper.IterationSymbol, Helper.Plus);
			/*
			var summands = Symbol.Split(new char[] { Helper.Plus });

			foreach (var summand in summands)
				if (!summand.Contains(Helper.Multiply))
					result.AppendFormat("{0}{1}{2}", summand, Helper.IterationSymbol, Helper.Plus);
				else
					result.AppendFormat("{0}{1}{2}{3}{4}", Helper.LeftBracket, summand, Helper.RightBracket,
						Helper.IterationSymbol, Helper.Plus);
			
			*/
			if (result.Length != 0)
				result.Remove(result.Length - 1, 1);
			else
				result.Append(Helper.EmptySymbol);

			return new SRECoefficient(result.ToString());
		}

		/// <summary>
		/// Сложение без оптимизации.
		/// </summary>
		/// <param name="coefficient1"></param>
		/// <param name="coefficient2"></param>
		/// <returns></returns>
		public static SRECoefficient SimpleAddition(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			return new SRECoefficient(coefficient1.Symbol + Helper.Plus + coefficient2.Symbol);
		}

		/// <summary>
		/// Умножение без оптимизации.
		/// </summary>
		/// <param name="coefficient1"></param>
		/// <param name="coefficient2"></param>
		/// <returns></returns>
		public static SRECoefficient SimpleMultiplication(SRECoefficient coefficient1, SRECoefficient coefficient2)
		{
			var factor1 = coefficient1.Symbol.IndexOfAny(new char[] { Helper.Plus }) != -1 ?
				Helper.LeftBracket + coefficient1.Symbol + Helper.RightBracket :
				coefficient1.Symbol;
			var factor2 = coefficient2.Symbol.IndexOfAny(new char[] { Helper.Plus }) != -1 ?
				Helper.LeftBracket + coefficient2.Symbol + Helper.RightBracket :
				coefficient2.Symbol;
			return new SRECoefficient(factor1 + Helper.Multiply + factor2);
		}

		#endregion

		#region Object overrides

		public override string ToString()
		{
			return Symbol;
		}

		#endregion

		#region Private methods

		private static int FindAppropriateBracket(string str, int bracketIndex)
		{
			if (str[bracketIndex] != Helper.LeftBracket && str[bracketIndex] != Helper.RightBracket)
				return -1;

			int direction = str[bracketIndex] == Helper.LeftBracket ? 1 : -1;
			int nesting = 0;
			int currentIndex = bracketIndex;
			do
			{
				if (currentIndex < 0 || currentIndex >= str.Length)
					return -1;

				if (str[currentIndex] == Helper.LeftBracket)
					nesting--;
				else
					if (str[currentIndex] == Helper.RightBracket)
						nesting++;

				currentIndex += direction;
			}
			while (nesting != 0);

			return currentIndex - direction;
		}

		private static string LastFactor(string str)
		{
			string result;
			int offset = str[str.Length - 1] == Helper.IterationSymbol ? 1 : 0;

			if (str[str.Length - 1 - offset] == Helper.RightBracket)
			{
				int index = FindAppropriateBracket(str, str.Length - 1 - offset);
				result = str.Substring(index + 1, str.Length - index - 2 - offset);
			}
			else
			{
				int index = str.LastIndexOf(Helper.Multiply) == -1 ? 0 : str.LastIndexOf(Helper.Multiply) + 1;
				result = str.Substring(index, str.Length - index - offset);
			}

			return result;
		}

		private static string FirstFactor(string str)
		{
			string result;

			if (str[0] == Helper.LeftBracket)
			{
				int index = FindAppropriateBracket(str, 0);
				result = str.Substring(1, index - 1);
			}
			else
			{
				int index = str.IndexOf(Helper.Multiply) == -1 ? str.Length : str.IndexOf(Helper.Multiply);
				result = str.Substring(0, str[index - 1] == Helper.IterationSymbol ? index - 1 : index);
			}

			return result;
		}

		private static void RemoveFactors(ref string leftFactor, ref string rightFactor)
		{
			int offset = leftFactor[leftFactor.Length - 1] == Helper.IterationSymbol ? 1 : 0;

			int index;
			if (leftFactor[leftFactor.Length - 1 - offset] == Helper.RightBracket)
				index = FindAppropriateBracket(leftFactor, leftFactor.Length - 1 - offset);
			else
				index = leftFactor.LastIndexOf(Helper.Multiply) == -1 ? 0 : leftFactor.LastIndexOf(Helper.Multiply);

			leftFactor = leftFactor.Remove(index);
			if (leftFactor.Length > 0 && leftFactor[leftFactor.Length - 1] == Helper.Multiply)
				leftFactor = leftFactor.Remove(leftFactor.Length - 1);

			if (rightFactor[0] == Helper.LeftBracket)
				index = FindAppropriateBracket(rightFactor, 0);
			else
				index = rightFactor.IndexOf(Helper.Multiply) == -1 ? 0 : rightFactor.IndexOf(Helper.Multiply);

			rightFactor = rightFactor.Substring(0, index);
			if (rightFactor.Length > 0 && rightFactor[0] == Helper.Multiply)
				rightFactor = rightFactor.Substring(1);
		}

		private static string[] Split(SRECoefficient coefficient)
		{
			return Split(coefficient.Symbol);
		}

		public static string[] Split(string expression)
		{
			return expression.Split(new char[] { Helper.Plus });
			
			int nesting = 0;
			var result = new List<string>();
			int prevPlusPosition = -1;

			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] == Helper.LeftBracket)
					nesting++;
				else if (expression[i] == Helper.RightBracket)
					nesting--;

				if (expression[i] == Helper.Plus && nesting == 0)
				{
					result.Add(expression.Substring(prevPlusPosition + 1, i - prevPlusPosition - 1));
					prevPlusPosition = i;
				}
			}
			result.Add(expression.Substring(prevPlusPosition + 1,
				expression.Length - prevPlusPosition - 1));

			return result.ToArray();
		}

		#endregion
	}
}
