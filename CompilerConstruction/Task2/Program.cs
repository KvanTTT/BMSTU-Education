using System;

namespace CompilerConstructionLib
{
	class Program
	{
		static void Main(string[] args)
		{
			var g = TestRightEmptyPart.GetGrammar();

			Console.WriteLine("Initial grammar:");
			Console.WriteLine(g.ToString());
			
			var graph = new GrammarGraph(g);

			graph.RemoveEmptyRightPartRools();
			graph.RemoveUselessSymbols();
			graph.RemoveUnreachableSymbols();

			/*Console.WriteLine();
			var system = new SystemOfRegularEquations(graph.Grammar);
			var result = system.Solve();
			Console.WriteLine("Result: ");
			for (int i = 0; i < result.Length; i++)
				Console.WriteLine(result[i]);
			*/
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Final grammar:");
			Console.Write(graph.Grammar.ToString());

			Console.ReadKey();
		}
	}
}
