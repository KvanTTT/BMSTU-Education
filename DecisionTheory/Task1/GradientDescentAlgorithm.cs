using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1
{
	public class GradientDescentAlgorithm
	{
		public readonly int VariableCount;
		public readonly MultiVarFunction Function;
		protected double[] StartPoint;

		public double Epsilon;
		public double DeriveStep;

		#region Constructors
		
		public GradientDescentAlgorithm()
		{
			Epsilon = 0.001;
			DeriveStep = 0.0001;
		}

		public GradientDescentAlgorithm(int variableCount, double[] startPoint, MultiVarFunction function)
			: this()
		{
			VariableCount = variableCount;
			StartPoint = startPoint;
			Function = function;
		}

		#endregion

		#region Calculations

		public void GetMinimum(out double minimum, out double[] variables)
		{
			variables = StartPoint;
			minimum = Function(variables);
			double[] oldPoint = variables;
			double[] gradient;
			double oldPointValue;

			do
			{
				oldPoint = variables;
				oldPointValue = minimum;
				gradient = GetAntiGradient(variables);
				Mult(gradient, Epsilon);
				variables = Append(oldPoint, gradient);
				minimum = Function(variables);
			}
			while (Math.Abs(minimum - oldPointValue) > Epsilon);
		}

		protected double[] GetAntiGradient(double[] point)
		{
			var result = GetGradient(point);
			Inverse(result);
			return result;
		}

		protected double[] GetGradient(double[] point)
		{
			var result = new double[VariableCount];
			var incrementedPoint =  new double[VariableCount];
			Array.Copy(point, incrementedPoint, VariableCount);

			for (int i = 0; i < point.Length; i++)
			{
				incrementedPoint[i] += DeriveStep;
				result[i] = (Function(incrementedPoint) - Function(point)) / DeriveStep;
				incrementedPoint[i] -= DeriveStep;
			}

			return result;
		}

		protected void Inverse(double[] vector)
		{
			for (int i = 0; i < vector.Length; i++)
				vector[i] = -vector[i];
		}

		protected void Mult(double[] vector, double coef)
		{
			for (int i = 0; i < vector.Length; i++)
				vector[i] *= coef;
		}

		protected double[] Append(double[] point, double[] vector)
		{
			var result = new double[VariableCount];
			for (int i = 0; i < point.Length; i++)
				result[i] = point[i] + vector[i];
			return result;
		}

		#endregion
	}
}
