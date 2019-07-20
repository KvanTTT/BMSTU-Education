using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSyncLib
{
	public class MusicalHmmOnline
	{
		#region Settings
		
		public int _windowSize;
		public int _observationsHistoryCount;

		#endregion

		#region Fields

		private int _oldPos;
		private int _currentPos;
		private List<int> _observations;
		private List<int> _lastObservations;
		private List<int> _path;
		private List<int> _lastPath;

		#endregion

		#region Properties

		public MusicalHmmData Data
		{
			get;
			protected set;
		}

		public int WindowSize 
		{
			get
			{
				return _windowSize;
			}
			set
			{
				_windowSize = value;
			}
		}

		public int LastObservationsCount
		{
			get
			{
				return _observationsHistoryCount;
			}
			set
			{
				_observationsHistoryCount = value;
				if (_lastObservations == null)
					_lastObservations = new List<int>();
			}
		}

		public int OldPos
		{
			get
			{
				return _oldPos;
			}
		}
		
		public int LastPos
		{
			get
			{
				return _currentPos;
			}
		}

		public HmmWindow Bounds
		{
			get;
			set;
		}

		public bool Started
		{
			get
			{
				return LastPos != -1;
			}
		}

		public List<int> Observations
		{
			get
			{
				return _observations;
			}
		}

		public List<int> LastObservations
		{
			get
			{
				return _lastObservations;
			}
		}

		public List<int> Path
		{
			get
			{
				return _path;
			}
		}

		public List<int> LastPath
		{
			get
			{
				return _lastPath;
			}
		}

		public int LastEventNumber
		{
			get
			{
				return Data.EventKvants[_currentPos / (Data.GhostStatesCount + 1)].Event.Number;
			}
		}

		public int LastLocalKvantNumber
		{
			get
			{
				return Data.EventKvants[_currentPos / (Data.GhostStatesCount + 1)].LocalNumber;
			}
		}

		public int LastGlobalKvantNumber
		{
			get
			{
				return Data.EventKvants[_currentPos / (Data.GhostStatesCount + 1)].GlobalNumber;
			}
		}

		public bool IsLastEventKvantGhost
		{
			get
			{
				return _currentPos % (Data.GhostStatesCount + 1) == 0;
			}
		}

		public bool IsLastEventInRightSeq
		{
			get
			{
				return !IsLastEventKvantGhost && (_path.Count < 2 || _currentPos > _path[_path.Count - 2]);
			}
		}

		public bool IsEnded
		{
			get
			{
				return _currentPos >= Data.Emissions.GetLength(0) - 2;
			}
		}

		#endregion

		#region Constructors

		public MusicalHmmOnline(MusicalHmmData data, int windowSize = 13, int observsCount = 6)
		{
			Data = data;
			WindowSize = windowSize;
			LastObservationsCount = observsCount;

			Reset();
		}
		
		#endregion

		#region Methods

		public void Reset()
		{
			_observations = new List<int>();
			_lastObservations = new List<int>();
			_path = new List<int>();
			_oldPos = -1;
			_currentPos = -1;

			Bounds = new HmmWindow(_windowSize, 0, Data.InitialLogs.Length);
		}

		public void AddNewObservation(int observation)
		{
			_observations.Add(observation);
			if (_lastObservations.Count >= LastObservationsCount)
				_lastObservations.RemoveAt(0);
			_lastObservations.Add(Data.MidiNoteToHmmMidiNote(observation));
		}

		public void UpdateCurrentPos()
		{
			_oldPos = _currentPos;
			_currentPos = _lastPath[_lastPath.Count - 1];



			Bounds = new HmmWindow(_windowSize, _currentPos, Data.InitialLogs.Length);
		}

		public int DecodeEvent(out double logLikelihood)
		{
			// Viterbi-forward algorithm.
			int observationsCount = _observations.Count;
			int StatesCountCount = Data.TransitionsLogs.GetLength(0);
			int maxState;
			double maxWeight;
			double weight;

			int[,] s = new int[StatesCountCount, observationsCount];
			double[,] lnFwd = new double[StatesCountCount, observationsCount];

			// Base
			for (int i = 0; i < StatesCountCount; i++)
				lnFwd[i, 0] = Data.InitialLogs[i] + Data.EmissionsLogs[i, _observations[0]];

			// Induction
			for (int t = 1; t < observationsCount; t++)
			{
				for (int j = 0; j < StatesCountCount; j++)
				{
					maxState = 0;
					maxWeight = lnFwd[0, t - 1] + Data.TransitionsLogs[0, j];

					for (int i = 1; i < StatesCountCount; i++)
					{
						weight = lnFwd[i, t - 1] + Data.TransitionsLogs[i, j];

						if (weight > maxWeight)
						{
							maxState = i;
							maxWeight = weight;
						}
					}

					lnFwd[j, t] = maxWeight + Data.EmissionsLogs[j, _observations[t]];
					s[j, t] = maxState;
				}
			}


			// Find maximum value for time T-1
			maxState = 0;
			maxWeight = lnFwd[0, observationsCount - 1];

			for (int i = 1; i < StatesCountCount; i++)
			{
				if (lnFwd[i, observationsCount - 1] > maxWeight)
				{
					maxState = i;
					maxWeight = lnFwd[i, observationsCount - 1];
				}
			}


			// Trackback
			int[] path = new int[observationsCount];
			path[observationsCount - 1] = maxState;

			for (int t = observationsCount - 2; t >= 0; t--)
				path[t] = s[path[t + 1], t + 1];


			// Returns the sequence probability as an out parameter
			logLikelihood = maxWeight;

			// Returns the most likely (Viterbi path) for the given sequence
			return path[path.Length - 1];
		}

		public IEnumerable<int> DecodeEventWindow(out double logLikelihood, bool parallel)
		{
			int statesCount = WindowSize;
			int observsCount = _lastObservations.Count;

			int maxState;
			double maxWeight;

			int[,] forwardStates = new int[statesCount, observsCount];
			double[,] forwardLogs = new double[statesCount, observsCount];

			for (int i = Bounds.LeftBound; i < Bounds.RightBound; i++)
			{
				if (i % 2 == 0)
					forwardLogs[i - Bounds.LeftBound, 0] = Math.Log(MathUtils.GetValue(MusicalHmmData.GhostsInitials, (i - Bounds.LeftBound) / 2, 0.0001));
				else
					forwardLogs[i - Bounds.LeftBound, 0] = Math.Log(MathUtils.GetValue(MusicalHmmData.DefaultInitials, (i - Bounds.LeftBound) / 2, 0.001));
				forwardLogs[i - Bounds.LeftBound, 0] += Data.EmissionsLogs[i, _lastObservations[0]];
			}

			for (int k = 1; k < observsCount; k++)
			{
				if (parallel)
				{
					Parallel.For(Bounds.LeftBound, Bounds.RightBound, j =>
					{
						DecodePart(forwardLogs, forwardStates, _lastObservations, k, j);
					});
				}
				else
				{
					for (int j = Bounds.LeftBound; j < Bounds.RightBound; j++)
						DecodePart(forwardLogs, forwardStates, _lastObservations, k, j);
				}
			}

			maxState = Bounds.LeftBound;
			maxWeight = forwardLogs[0, observsCount - 1];
			for (int i = 1; i < statesCount; i++)
			{
				if (forwardLogs[i, observsCount - 1] > maxWeight)
				{
					maxState = Bounds.LeftBound + i;
					maxWeight = forwardLogs[i, observsCount - 1];
				}
			}

			_lastPath = new List<int>(new int[observsCount]);
			_lastPath[observsCount - 1] = maxState;

			for (int t = observsCount - 2; t >= 0; t--)
				_lastPath[t] = forwardStates[_lastPath[t + 1] - Bounds.LeftBound, t + 1];

			_path.Add(_lastPath[observsCount - 1]);

			logLikelihood = maxWeight;

			return _lastPath;
		}

		private void DecodePart(double[,] forwardLogs, int[,] forwardStates, List<int> hmmObservations, int k, int j)
		{
			int maxState = Bounds.LeftBound;
			double maxWeight = forwardLogs[0, k - 1] + Data.TransitionsLogs[Bounds.LeftBound, j];
			for (int i = Bounds.LeftBound + 1; i < Bounds.RightBound; i++)
			{
				double weight = forwardLogs[i - Bounds.LeftBound, k - 1] + Data.TransitionsLogs[i, j];
				if (weight > maxWeight)
				{
					maxState = i;
					maxWeight = weight;
				}
			}
			forwardLogs[j - Bounds.LeftBound, k] = maxWeight + Data.EmissionsLogs[j, _lastObservations[k]];
			forwardStates[j - Bounds.LeftBound, k] = maxState;
		}

		public double EvaluateWindow()
		{
			double likelihood = 0;
			double[] coefficients;

			ForwardWindow(out coefficients);

			for (int i = 0; i < coefficients.Length; i++)
				likelihood += Math.Log(coefficients[i]);

			return Math.Exp(likelihood);
		}

		#endregion

		#region Utils

		private double[,] ForwardWindow(out double[] c)
		{
			int statesCount = _windowSize;
			int observsCount = _lastObservations.Count;

			double[,] fwd = new double[observsCount, statesCount];
			c = new double[observsCount];

			HmmWindow window = new HmmWindow(_windowSize, _currentPos, Data.InitialLogs.Length);

			// 1. Initialization
			for (int i = window.LeftBound; i < window.RightBound; i++)
			{
				fwd[0, i - window.LeftBound] = Data.InitialLogs[i] * Data.EmissionsLogs[i, _lastObservations[0]];
				c[0] += fwd[0, i - window.LeftBound];
			}

			if (c[0] != 0)
			{
				for (int i = 0; i < statesCount; i++)
					fwd[0, i] = fwd[0, i] / c[0];
			}

			// 2. Induction
			for (int t = 1; t < observsCount; t++)
			{
				for (int i = window.LeftBound; i < window.RightBound; i++)
				{
					double p = Data.EmissionsLogs[i, _lastObservations[t]];

					double sum = 0.0;
					for (int j = window.LeftBound; j < window.RightBound; j++)
						sum += fwd[t - 1, j - window.LeftBound] * Data.TransitionsLogs[j, i];
					fwd[t, i - window.LeftBound] = sum * p;

					c[t] += fwd[t, i - window.LeftBound]; // scaling coefficient
				}

				if (c[t] != 0) // Scaling
				{
					for (int i = 0; i < statesCount; i++)
						fwd[t, i] = fwd[t, i] / c[t];
				}
			}

			return fwd;
		}

		private double[,] BackwardWindow(double[] c)
		{
			int statesCount = _windowSize;
			int observsCount = _lastObservations.Count;

			double[,] bwd = new double[observsCount, statesCount];

			HmmWindow window = new HmmWindow(_windowSize, _currentPos, Data.InitialLogs.Length);

			for (int i = window.LeftBound; i < window.RightBound; i++)
				bwd[0, i - window.LeftBound] = Data.InitialLogs[i] * Data.EmissionsLogs[i, _lastObservations[0]];

			// 1. Initialization
			if (c[0] != 0)
			{
				for (int i = 0; i < statesCount; i++)
					bwd[0, i] = bwd[0, i] / c[0];
			}

			// 2. Induction
			for (int t = observsCount - 2; t >= 0; t--)
			{
				for (int i = window.LeftBound; i < window.RightBound; i++)
				{
					double sum = 0.0;
					for (int j = window.LeftBound; j < window.RightBound; j++)
						sum += bwd[t + 1, j - window.LeftBound] * Data.TransitionsLogs[i, j] * Data.EmissionsLogs[j, _lastObservations[t + 1]];
					bwd[t, i - window.LeftBound] += sum;
				}

				if (c[t] != 0) // Scaling
				{
					for (int i = 0; i < statesCount; i++)
						bwd[t, i] = bwd[t, i] / c[t];
				}
			}

			return bwd;
		}

		#endregion
	}
}
