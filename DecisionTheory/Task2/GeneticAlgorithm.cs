using System;
using System.Collections.Generic;
using System.Linq;
using Task1;

namespace Task2
{
	public class GeneticAlgorithm
	{
		public int VariableCount;
		public MultiVarFunction Function;

		public double[] StartPoint;
		public double SemiWidth;
		public int PopulationSize;
		public double ChildrenRatio;
		public double MutationProbability;
		public double MutationOffset;
		public int MaxRepeatCount;

		Random random;

		#region Constructors
		
		public GeneticAlgorithm()
		{
			random = new Random();
		}

		#endregion

		#region Calculations

		public void GetMinimum(out double minimum, out double[] variables)
		{
			var startPopulation = GenerateStartPopulation();
			var currentPopulation = startPopulation;

			for (int i = 0; i < MaxRepeatCount; i++)
			{
				AddMutation(currentPopulation);

				var childrensAndParents = new double[(int)(PopulationSize * ChildrenRatio)][];
				
				var childrenPopulation = GenerateChildrens(currentPopulation);
				currentPopulation.CopyTo(childrensAndParents, 0);
				childrenPopulation.CopyTo(childrensAndParents, PopulationSize);

				var tempList = childrensAndParents.ToList();
				tempList.Sort((vars1, vars2) => Function(vars1).CompareTo(Function(vars2)));
				tempList.CopyTo(0, currentPopulation, 0, PopulationSize);
			}

			minimum = Function(currentPopulation[0]);
			variables = currentPopulation[0];
		}

		protected double[][] GenerateStartPopulation()
		{
			var result = new double[PopulationSize][];

			for (int i = 0; i < PopulationSize; i++)
			{
				result[i] = new double[VariableCount];
				for (int j = 0; j < VariableCount; j++)
					result[i][j] = StartPoint[j] - SemiWidth + random.NextDouble() * SemiWidth * 2;
			}

			return result;
		}

		protected double[][] GenerateChildrens(double[][] population)
		{
			var result = new double[(int)(PopulationSize * (ChildrenRatio - 1))][];

			for (int i = 0; i < result.Length; i++)
			{
				int randomParent1 = random.Next(VariableCount);
				int randomParent2 = random.Next(VariableCount);
				while (randomParent2 == randomParent1)
					randomParent2 = random.Next(VariableCount);

				var indexes = ShuffleIndexes();
				result[i] = new double[VariableCount];
				for (int j = 0; j < indexes.Length / 2; j++)
					result[i][j] = population[randomParent1][indexes[j]];
				for (int j = indexes.Length / 2; j < indexes.Length; j++)
					result[i][j] = population[randomParent2][indexes[j]];
			}

			return result;
		}

		protected void AddMutation(double[][] population)
		{
			for (int i = 0; i < population.Length; i++)
				if (random.NextDouble() <= MutationProbability)
					for (int j = 0; j < VariableCount; j++)
					{
						population[i][j] = population[i][j] - MutationOffset + random.NextDouble() * MutationOffset * 2;
						if (population[i][j] < StartPoint[j] - SemiWidth)
							population[i][j] = StartPoint[j] - SemiWidth;
						else if (population[i][j] > StartPoint[j] + SemiWidth)
							population[i][j] = StartPoint[j] + SemiWidth;
					}
		}

		protected int[] ShuffleIndexes()
		{
			var result = new int[VariableCount];
			for (int i = 0; i < VariableCount; i++)
				result[i] = i;
			for (int i = 0; i < VariableCount; i++)
			{
				int t = result[i];
				int ind = random.Next(VariableCount);
				result[i] = result[ind];
				result[ind] = t;
			}
			return result;
		}

		#endregion
	}
}
