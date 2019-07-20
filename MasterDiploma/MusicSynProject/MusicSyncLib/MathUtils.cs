using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicSyncLib
{
	public static class MathUtils
	{
		public static double[,] TransposeMatrix(double[,] matrix)
		{
			double[,] result = new double[matrix.GetLength(1), matrix.GetLength(0)];

			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					result[j, i] = matrix[i, j];

			return result;
		}

		public static double[] Exp(double[] value)
		{
			double[] r = new double[value.Length];
			for (int i = 0; i < value.Length; i++)
				r[i] = System.Math.Exp(value[i]);
			return r;
		}

		public static double[,] Exp(double[,] value)
		{
			int rows = value.GetLength(0);
			int cols = value.GetLength(1);

			double[,] r = new double[rows, cols];
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					r[i, j] = System.Math.Exp(value[i, j]);
			return r;
		}

		public static double[,] Log(double[,] value)
		{
			int rows = value.GetLength(0);
			int cols = value.GetLength(1);

			double[,] r = new double[rows, cols];
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					r[i, j] = System.Math.Log(value[i, j]);
			return r;
		}

		public static double[] Log(double[] value)
		{
			double[] result = new double[value.Length];
			for (int i = 0; i < value.Length; i++)
				result[i] = System.Math.Log(value[i]);
			return result;
		}

		public static double GetValue(double[] array, int index, double defaultValue = 0)
		{
			if (index >= 0 && index < array.Length)
				return array[index];
			else
				return defaultValue;
		}
	}
}
