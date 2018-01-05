using System;
using System.Text;

namespace ControlWorkCSharp
{
    class Program
	{
		const int varCount = (int)'z' - (int)'a' + 1;

		enum enmOperation
		{
			Plus,
			Minus
		}

		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					var inputString = Console.ReadLine();

					var processedStringBuilder = new StringBuilder(inputString.Length);
					var isVariableExists = new bool[varCount + 1];
					var values = new int[varCount + 1];
					var finalValues = new int[varCount + 1];

					for (int i = 0; i < values.Length; i++)
						values[i] = i;

					// Remove spaces and determine what variables exist.
					for (int i = 0; i < inputString.Length; i++)
						if (inputString[i] != ' ' && inputString[i] != '	')
						{
							processedStringBuilder.Append(inputString[i]);
							if (inputString[i] >= 'a' && inputString[i] <= 'z')
								isVariableExists[inputString[i] - (int)'a' + 1] = true;
							else
								if (inputString[i] != '+' && inputString[i] != '-')
									throw new Exception("Invalid input symbol (not in [a-z, ,+,-]");
						}

					var processedString = processedStringBuilder.ToString();

					// Remove pre increments and decrements.
					int j = 2;
					while (j < processedString.Length)
					{
						if (processedString[j] >= 'a' && processedString[j] <= 'z' &&
							processedString[j - 1] == processedString[j - 2])
						{
							if (processedString[j - 1] == '+')
								values[processedString[j] - (int)'a' + 1]++;
							else
								values[processedString[j] - (int)'a' + 1]--;
							processedString = processedString.Substring(0, j - 2) + processedString.Substring(j);
							j--;
						}
						j++;
					}

					values.CopyTo(finalValues, 0);

					// Remove post increments and decrements.
					j = 0;
					while (j < processedString.Length - 2)
					{
						if (processedString[j] >= 'a' && processedString[j] <= 'z' &&
							processedString[j + 1] == processedString[j + 2])
						{
							if (processedString[j + 1] == '+')
								finalValues[processedString[j] - (int)'a' + 1]++;
							else
								finalValues[processedString[j] - (int)'a' + 1]--;
							processedString = processedString.Substring(0, j + 1) + processedString.Substring(j + 3);
						}
						j++;
					}

					if (processedString[0] != '-')
						processedString = '+' + processedString;

					int result = 0;
					enmOperation operation = enmOperation.Plus;

					// Calculate final value.
					for (int i = 0; i < processedString.Length; i++)
						if (processedString[i] == '+')
							operation = enmOperation.Plus;
						else
							if (processedString[i] == '-')
								operation = enmOperation.Minus;
							else
								result = operation == enmOperation.Plus ? result + values[processedString[i] - 'a' + 1] :
																		  result - values[processedString[i] - 'a' + 1];

					// Output results.
					Console.WriteLine("Expresson: " + inputString);
					Console.WriteLine("    value = " + result);

					for (int i = 1; i < finalValues.Length; i++)
						if (isVariableExists[i])
							Console.WriteLine("    {0} = {1}", Convert.ToString((char)((int)'a' + (i - 1))), finalValues[i]);
				}
				catch (Exception e)
				{
					Console.WriteLine("Wrong input. Unable to process expression!");
					if (!string.IsNullOrEmpty(e.Message))
						Console.WriteLine(e.Message);
				}
				Console.WriteLine();
			}
		}
	}
}
