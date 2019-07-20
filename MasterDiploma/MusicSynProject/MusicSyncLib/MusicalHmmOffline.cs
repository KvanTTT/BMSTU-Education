﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSyncLib
{
	public class MusicalHmmOffline
	{
		#region Properties

		public MusicalHmmData Data
		{
			get;
			protected set;
		}

		#endregion

		#region Constructors

		public MusicalHmmOffline(MusicalHmmData data)
		{
			Data = data;
		}
		
		#endregion

		#region Methods

		/// <summary>
		///   Calculates the probability that this model has generated the given sequence.
		/// </summary>
		/// <remarks>
		///   Evaluation problem. Given the HMM  M = (A, B, pi) and  the observation
		///   sequence O = {o1, o2, ..., oK}, calculate the probability that model
		///   M has generated sequence O. This can be computed efficiently using the
		///   either the Viterbi or the Forward algorithms.
		/// </remarks>
		/// <param name="observations">
		///   A sequence of observations.
		/// </param>
		/// <param name="logarithm">
		///   True to return the log-likelihood, false to return
		///   the likelihood. Default is false.
		/// </param>
		/// <returns>
		///   The probability that the given sequence has been generated by this model.
		/// </returns>
		public double Evaluate(int[] observations)
		{
			// Forward algorithm
			double likelihood = 1;
			double[] coefficients;

			// Compute forward probabilities
			Forward(Data.ConvertToHmmMidiNotes(observations), out coefficients);

			for (int i = 0; i < coefficients.Length; i++)
				likelihood *= coefficients[i];

			// Return the sequence probability
			return likelihood;
		}

		public int[] Decode(int[] observations, out double logLikelihood, bool parallel)
		{
			int[] hmmObservations = Data.ConvertToHmmMidiNotes(observations);

			int observsCount = hmmObservations.Length;
			int statesCount = Data.StatesCount;

			int[,] forwardStates = new int[statesCount, observsCount];
			double[,] forwardLogs = new double[statesCount, observsCount];

			// Base
			for (int i = 0; i < statesCount; i++)
				forwardLogs[i, 0] = Data.InitialLogs[i] + Data.EmissionsLogs[i, hmmObservations[0]];

			// Induction
			for (int k = 1; k < observsCount; k++)
			{
				if (parallel)
				{
					Parallel.For(0, statesCount, j =>
					{
						DecodePart(forwardLogs, forwardStates, hmmObservations, k, j);
					});
				}
				else
				{
					for (int j = 0; j < statesCount; j++)
					{
						DecodePart(forwardLogs, forwardStates, hmmObservations, k, j);
					}
				}
			}

			// Find maximum value for time T-1
			int maxState = 0;
			double maxWeight = forwardLogs[0, observsCount - 1];

			for (int i = 1; i < statesCount; i++)
			{
				if (forwardLogs[i, observsCount - 1] > maxWeight)
				{
					maxState = i;
					maxWeight = forwardLogs[i, observsCount - 1];
				}
			}

			// Trackback
			int[] path = new int[observsCount];
			path[observsCount - 1] = maxState;

			for (int t = observsCount - 2; t >= 0; t--)
				path[t] = forwardStates[path[t + 1], t + 1];

			// Returns the sequence probability as an out parameter
			logLikelihood = maxWeight;

			// Returns the most likely (Viterbi path) for the given sequence
			return path;
		}

		private void DecodePart(double[,] forwardLogs, int[,] forwardStates, int[] hmmObservations, int k, int j)
		{
			int maxState = 0;
			double maxWeight = forwardLogs[0, k - 1] + Data.TransitionsLogs[0, j];
			for (int i = 1; i < forwardLogs.GetLength(0); i++)
			{
				double weight = forwardLogs[i, k - 1] + Data.TransitionsLogs[i, j];
				if (weight > maxWeight)
				{
					maxState = i;
					maxWeight = weight;
				}
			}
			forwardLogs[j, k] = maxWeight + Data.EmissionsLogs[j, hmmObservations[k]];
			forwardStates[j, k] = maxState;
		}

		/// <summary>
		///   Runs the Baum-Welch learning algorithm for hidden Markov models.
		/// </summary>
		/// <remarks>
		///   Learning problem. Given some training observation sequences O = {o1, o2, ..., oK}
		///   and general structure of HMM (numbers of hidden and visible StatesCount), determine
		///   HMM parameters M = (A, B, pi) that best fit training data. 
		/// </remarks>
		/// <param name="iterations">
		///   The maximum number of iterations to be performed by the learning algorithm. If
		///   specified as zero, the algorithm will learn until convergence of the model average
		///   likelihood respecting the desired limit.
		/// </param>
		/// <param name="observations">
		///   An array of observation sequences to be used to train the model.
		/// </param>
		/// <param name="tolerance">
		///   The likelihood convergence limit L between two iterations of the algorithm. The
		///   algorithm will stop when the change in the likelihood for two consecutive iterations
		///   has not changed by more than L percent of the likelihood. If left as zero, the
		///   algorithm will ignore this parameter and iterates over a number of fixed iterations
		///   specified by the previous parameter.
		/// </param>
		/// <returns>
		///   The average log-likelihood for the observations after the model has been trained.
		/// </returns>
		public double Learn(int[][] observations, int iterations, double tolerance)
		{
			int[][] hmmObservations = observations.Select(o => Data.ConvertToHmmMidiNotes(o)).ToArray();

			if (iterations == 0 && tolerance == 0)
				throw new ArgumentException("Iterations and limit cannot be both zero.");

			// Baum-Welch algorithm.

			// The Baum–Welch algorithm is a particular case of a generalized expectation-maximization
			// (GEM) algorithm. It can compute maximum likelihood estimates and posterior mode estimates
			// for the parameters (transition and emission probabilities) of an HMM, when given only
			// emissions as training data.

			// The algorithm has two steps:
			//  - Calculating the forward probability and the backward probability for each HMM state;
			//  - On the basis of this, determining the frequency of the transition-emission pair values
			//    and dividing it by the probability of the entire string. This amounts to calculating
			//    the expected count of the particular transition-emission pair. Each time a particular
			//    transition is found, the value of the quotient of the transition divided by the probability
			//    of the entire string goes up, and this value can then be made the new value of the transition.

			int statesCount = Data.StatesCount;
			int N = hmmObservations.Length;
			int currentIteration = 1;
			bool stop = false;

			// Initialization
			double[][,,] epsilon = new double[N][, ,]; // also referred as ksi or psi
			double[][,] gamma = new double[N][,];

			for (int i = 0; i < N; i++)
			{
				int T = hmmObservations[i].Length;
				epsilon[i] = new double[T, statesCount, statesCount];
				gamma[i] = new double[T, statesCount];
			}

			// Calculate initial model log-likelihood
			double oldLikelihood = Double.MinValue;
			double newLikelihood = 0;

			do // Until convergence or max iterations is reached
			{
				// For each sequence in the hmmObservations input
				for (int i = 0; i < N; i++)
				{
					var sequence = hmmObservations[i];
					int T = sequence.Length;
					double[] scaling;

					// 1st step - Calculating the forward probability and the
					//            backward probability for each HMM state.
					double[,] fwd = Forward(hmmObservations[i], out scaling);
					double[,] bwd = Backward(hmmObservations[i], scaling);


					// 2nd step - Determining the frequency of the transition-emission pair values
					//            and dividing it by the probability of the entire string.


					// Calculate gamma values for next computations
					for (int t = 0; t < T; t++)
					{
						double s = 0;

						for (int k = 0; k < statesCount; k++)
							s += gamma[i][t, k] = fwd[t, k] * bwd[t, k];

						if (s != 0) // Scaling
						{
							for (int k = 0; k < statesCount; k++)
								gamma[i][t, k] /= s;
						}
					}

					// Calculate epsilon values for next computations
					for (int t = 0; t < T - 1; t++)
					{
						double s = 0;

						for (int k = 0; k < statesCount; k++)
							for (int l = 0; l < statesCount; l++)
								s += epsilon[i][t, k, l] = fwd[t, k] * Data.Transitions[k, l] * bwd[t + 1, l] * Data.Emissions[l, sequence[t + 1]];

						if (s != 0) // Scaling
						{
							for (int k = 0; k < statesCount; k++)
								for (int l = 0; l < statesCount; l++)
									epsilon[i][t, k, l] /= s;
						}
					}

					// Compute log-likelihood for the given sequence
					for (int t = 0; t < scaling.Length; t++)
						newLikelihood += Math.Log(scaling[t]);
				}

				// Average the likelihood for all sequences
				newLikelihood /= hmmObservations.Length;

				// Check if the model has converged or we should stop
				if (CheckConvergence(oldLikelihood, newLikelihood, currentIteration, iterations, tolerance))
				{
					stop = true;
				}
				else
				{
					// 3. Continue with parameter re-estimation
					currentIteration++;
					oldLikelihood = newLikelihood;
					newLikelihood = 0.0;

					// 3.1 Re-estimation of initial state probabilities 
					for (int k = 0; k < statesCount; k++)
					{
						double sum = 0;
						for (int i = 0; i < N; i++)
							sum += gamma[i][0, k];
						Data.Initial[k] = sum / N;
					}

					// 3.2 Re-estimation of transition probabilities 
					for (int i = 0; i < statesCount; i++)
					{
						for (int j = 0; j < statesCount; j++)
						{
							double den = 0, num = 0;

							for (int k = 0; k < N; k++)
							{
								int T = hmmObservations[k].Length;

								for (int l = 0; l < T - 1; l++)
									num += epsilon[k][l, i, j];

								for (int l = 0; l < T - 1; l++)
									den += gamma[k][l, i];
							}

							Data.Transitions[i, j] = (den != 0) ? num / den : 0.0;
						}
					}

					// 3.3 Re-estimation of emission probabilities
					for (int i = 0; i < statesCount; i++)
					{
						for (int j = 0; j < Data.Emissions.GetLength(1); j++)
						{
							double den = 0, num = 0;

							for (int k = 0; k < N; k++)
							{
								int T = hmmObservations[k].Length;

								for (int l = 0; l < T; l++)
								{
									if (hmmObservations[k][l] == j)
										num += gamma[k][l, i];
								}

								for (int l = 0; l < T; l++)
									den += gamma[k][l, i];
							}

							// avoid locking a parameter in zero.
							Data.Emissions[i, j] = (num == 0) ? 1e-10 : num / den;
						}
					}
				}
			}
			while (!stop);

			Data.RecalcMatrixesLogs();

			// Returns the model average log-likelihood
			return newLikelihood;
		}

		#endregion

		#region Utils

		private double[,] Forward(int[] observations, out double[] c)
		{
			int statesCount = this.Data.Transitions.GetLength(0);
			int observsCount = observations.Length;

			double[,] fwd = new double[observsCount, statesCount];
			c = new double[observsCount];

			// 1. Initialization
			for (int i = 0; i < statesCount; i++)
			{
				fwd[0, i] = Data.Initial[i] * Data.Emissions[i, observations[0]];
				c[0] += fwd[0, i];
			}

			if (c[0] != 0)
			{
				for (int i = 0; i < statesCount; i++)
					fwd[0, i] = fwd[0, i] / c[0];
			}

			// 2. Induction
			for (int t = 1; t < observsCount; t++)
			{
				for (int i = 0; i < statesCount; i++)
				{
					double p = Data.Emissions[i, observations[t]];

					double sum = 0.0;
					for (int j = 0; j < statesCount; j++)
						sum += fwd[t - 1, j] * Data.Transitions[j, i];
					fwd[t, i] = sum * p;

					c[t] += fwd[t, i]; // scaling coefficient
				}

				if (c[t] != 0) // Scaling
				{
					for (int i = 0; i < statesCount; i++)
						fwd[t, i] = fwd[t, i] / c[t];
				}
			}

			return fwd;
		}

		private double[,] Backward(int[] observations, double[] c)
		{
			int statesCount = this.Data.Transitions.GetLength(0);
			int observsCount = observations.Length;

			double[,] bwd = new double[observations.Length, statesCount];

			for (int i = 0; i < statesCount; i++)
				bwd[0, i] = Data.Initial[i] * Data.Emissions[i, observations[0]];

			// 1. Initialization
			for (int i = 0; i < statesCount; i++)
				bwd[observsCount - 1, i] = 1.0 / c[observsCount - 1];

			// 2. Induction
			for (int t = observsCount - 2; t >= 0; t--)
			{
				for (int i = 0; i < statesCount; i++)
				{
					double sum = 0.0;
					for (int j = 0; j < statesCount; j++)
						sum += bwd[t + 1, j] * Data.Transitions[i, j] * Data.Emissions[j, observations[t + 1]];
					bwd[t, i] += sum / c[t];
				}
			}

			return bwd;
		}

		private static bool CheckConvergence(double oldValue, double newValue, int currentIteration, int iterations, double tolerance)
		{
			if (tolerance > 0)
			{
				// Stopping criteria is likelihood convergence
				double delta = Math.Abs(oldValue - newValue);

				if (delta <= tolerance)
					return true;

				if (iterations > 0)
				{
					// Maximum iterations should also be respected
					if (currentIteration >= iterations)
						return true;
				}
			}
			else
			{
				if (currentIteration == iterations)
					return true;
			}

			// Check if we have reached an invalid or perfectly separable answer
			if (Double.IsNaN(newValue) || Double.IsInfinity(newValue))
			{
				return true;
			}

			return false;
		}

		#endregion
	}
}
