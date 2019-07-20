using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MusicSyncLib
{
	public class MusicalHmmData
	{
		#region Consts

		public static double[] GhostsInitials = new double[] { 0.1, 0.005, 0.001 };
		public static double[] DefaultInitials = new double[] { 0.75, 0.05, 0.04, 0.03, 0.02, 0.007, 0.003 };

		#endregion

		#region Properties

		public double MinDuration
		{
			get;
			set;
		}

		public int GhostStatesCount
		{
			get;
			set;
		}

		public int ObservsCount
		{
			get;
			protected set;
		}

		public int HmmPauseNote
		{
			get
			{
				return ObservsCount;
			}
		}

		#endregion

		#region Fields & Properties

		public List<MusicalEvent> Events
		{
			get;
			protected set;
		}

		public List<MusicalEventKvant> EventKvants
		{
			get;
			protected set;
		}

		public double[] Initial
		{
			get;
			protected set;
		}

		public double[] InitialLogs
		{
			get;
			protected set;
		}

		public double[,] Emissions
		{
			get;
			protected set;
		}

		public double[,] EmissionsLogs
		{
			get;
			protected set;
		}

		public double[,] Transitions
		{
			get;
			protected set;
		}

		public double[,] TransitionsLogs
		{
			get;
			protected set;
		}

		public int StatesCount
		{
			get
			{
				return Transitions.GetLength(0);
			}
		}

		#endregion

		#region Constructors

		public MusicalHmmData(List<MusicalEvent> events, double minDuration = 0.5, int ghostStatesCount = 0, int observsCount = 12)
		{
			MinDuration = minDuration;
			GhostStatesCount = ghostStatesCount;
			Events = events;
			ObservsCount = observsCount;
		}

		#endregion

		#region Public
		
		public void GenerateEventsAndMatricies()
		{
			GenerateEventKvants();
			GenerateMatricies();
		}
		
		public void GenerateEventKvants()
		{
			EventKvants = new List<MusicalEventKvant>();

			int kvantGlobalNumber = 0;
			foreach (var e in Events)
			{
				var kvants = new List<MusicalEventKvant>();
				int kvantLocalNumber = 0;
				for (int i = 0; i < (int)(e.Duration / MinDuration); i++)
				{
					var eventKvant = new MusicalEventKvant(e, kvantLocalNumber++, kvantGlobalNumber++,
						MinDuration, e.MidiNote, MidiNoteToHmmMidiNote(e.MidiNote));
					kvants.Add(eventKvant);
					EventKvants.Add(eventKvant);
				}
				e.Kvants = kvants;
			}
		}

		public void GenerateMatricies()
		{
			GenetateStateTransitions();
			GenerateEmissios();
			GenerateInitialProbabs();
			RecalcMatrixesLogs();
		}

		public void LoadProbabs(string stateTransFileName, string emissionProbsFileName, string initialProbsFileName)
		{
			LoadTransitions(stateTransFileName);
			LoadEmissions(emissionProbsFileName);
			LoadInitial(initialProbsFileName);
		}

		public void RecalcMatrixesLogs()
		{
			TransitionsLogs = MathUtils.Log(Transitions);
			EmissionsLogs = MathUtils.Log(Emissions);
			InitialLogs = MathUtils.Log(Initial);
		}

		#endregion

		#region Generate Utils

		private void GenetateStateTransitions()
		{
			int statesCount = EventKvants.Count * (GhostStatesCount + 1) + 1;
			Transitions = new double[statesCount, statesCount];

			for (int i = 0; i < statesCount; i++)
			{
				if (i % (GhostStatesCount + 1) == 0)
				{
					if (i != statesCount - 2)
					{
						/*CheckAndSet(Transitions, i, i + 1, 0.45);
						CheckAndSet(Transitions, i - 1, i + 1, 0.95);
						CheckAndSet(Transitions, i - 2, i + 1, 0.45);
						CheckAndSet(Transitions, i - 3, i + 1, 0.01);
						CheckAndSet(Transitions, i - 5, i + 1, 0.01);
						CheckAndSet(Transitions, i - 7, i + 1, 0.01);*/
						
						CheckAndSet(Transitions, i, i + 1, 0.45);
						CheckAndSet(Transitions, i, i + 2, 0.1);
						CheckAndSet(Transitions, i, i + 3, 0.45);
					}
					else
					{
						CheckAndSet(Transitions, i, i + 1, 0.5);
						CheckAndSet(Transitions, i + 1, i + 1, 0.5);
						CheckAndSet(Transitions, i - 1, i + 1, 0.98);
						CheckAndSet(Transitions, i - 2, i + 1, 0.45);
						CheckAndSet(Transitions, i - 3, i + 1, 0.01);
						CheckAndSet(Transitions, i - 5, i + 1, 0.01);
						CheckAndSet(Transitions, i - 7, i + 1, 0.01);
					}
				}
				else
				{
					if (i != statesCount - 1)
					{
						//CheckAndSet(Transitions, i, i + 1, 0.5); // 0.02
						
						//CheckAndSet(Transitions, i, i + 1, 0.02);
						//CheckAndSet(Transitions, i - 1, i + 1, 0.1);

						CheckAndSet(Transitions, i, i, 0.01);
						CheckAndSet(Transitions, i, i + 1, 0.02);
						CheckAndSet(Transitions, i, i + 2, 0.95);
						CheckAndSet(Transitions, i, i + 4, 0.01);
						CheckAndSet(Transitions, i, i + 6, 0.01);
						CheckAndSet(Transitions, i, i + 8, 0.01);
					}
					else
					{
						CheckAndSet(Transitions, i, i + 1, 0.5);
						CheckAndSet(Transitions, i - 1, i + 1, 0.5);
						CheckAndSet(Transitions, i + 1, i + 1, 1);
					}
				}
			}
		}

		private void GenerateEmissios()
		{
			int statesCount = EventKvants.Count * (GhostStatesCount + 1) + 1;
			Emissions = new double[statesCount, ObservsCount + 1];

			double wrongProbab = Math.Round(1.0 / ObservsCount, 4);
			double[] GhostNotes = new double[] { 0, 0.004, 0.008 };
			double[] GhostPauses = new double[] { wrongProbab, 0.079, 0.075 };
			double[] StateNotes = new double[] { 1, 0.95, 0.90 };
			double[] StatePauses = new double[] { 0, 0.05, 0.10 };

			int ind = 0;
			foreach (var e in Events)
			{
				int duration = (int)(e.Duration / MinDuration);
				int hmmMidiNote = MidiNoteToHmmMidiNote(e.MidiNote);

				for (int i = 0; i < duration; i++)
				{
					if (GhostStatesCount >= 1)
					{
						int ind2 = i * (GhostStatesCount + 1);

						FillRow(Emissions, ind + ind2, wrongProbab);
						Emissions[ind + ind2, hmmMidiNote] = GetByIndexOrLast(GhostNotes, i);
						Emissions[ind + ind2, HmmPauseNote] = GetByIndexOrLast(GhostPauses, i);

						FillRow(Emissions, ind + ind2 + 1, 0);
						Emissions[ind + ind2 + 1, hmmMidiNote] = GetByIndexOrLast(StateNotes, i);
						Emissions[ind + ind2 + 1, HmmPauseNote] = GetByIndexOrLast(StatePauses, i);
					}
					else
					{
						FillRow(Emissions, ind + i, 0);
						Emissions[ind + i, hmmMidiNote] = GetByIndexOrLast(StateNotes, i);
						Emissions[ind + i, HmmPauseNote] = GetByIndexOrLast(StatePauses, i);
					}
				}

				ind += duration * (GhostStatesCount + 1);
			}

			FillRow(Emissions, statesCount - 1, 1.0 / (ObservsCount + 1));
		}

		private void FillRow(double[,] array, int row, double value)
		{
			for (int i = 0; i < array.GetLength(1); i++)
				Emissions[row, i] = value;
		}

		private double GetByIndexOrLast(double[] array, int index)
		{
			if (index < 0)
				return array[0];
			else if (index >= array.Length)
				return array[array.Length - 1];
			else
				return array[index];
		}

		private void GenerateInitialProbabs()
		{
			int statesCount = EventKvants.Count * (GhostStatesCount + 1) + 1;
			int coef = (GhostStatesCount + 1);
			Initial = new double[statesCount];
			CheckAndSet(Initial, 0, 0.1);
			CheckAndSet(Initial, 1, 0.75);
			CheckAndSet(Initial, 1 + coef, 0.05);
			CheckAndSet(Initial, 1 + coef * 2, 0.04);
			CheckAndSet(Initial, 1 + coef * 3, 0.03);
			CheckAndSet(Initial, 1 + coef * 4, 0.02);
			CheckAndSet(Initial, 1 + coef * 5, 0.007);
			CheckAndSet(Initial, 1 + coef * 6, 0.003);
			/*for (int i = 1 + coef * 7; i < Initial.Length; i += coef)
			{
				CheckAndSet(Initial, i - 1, 0.001 / 7.5);
				CheckAndSet(Initial, i, 0.001);
			}*/
		}

		private static bool CheckAndSet(double[,] array, int i, int j, double value)
		{
			if (i >= 0 && i < array.GetLength(0) && j >= 0 && j < array.GetLength(1))
			{
				array[i, j] = value;
				return true;
			}
			else
				return false;
		}

		private static bool CheckAndSet(double[] array, int i, double value)
		{
			if (i >= 0 && i < array.Length)
			{
				array[i] = value;
				return true;
			}
			else
				return false;
		}

		#endregion

		#region Load Utils

		private void LoadTransitions(string stateTransFileName)
		{
			string data = File.ReadAllText(stateTransFileName);
			string[] lines = data.Split(new string[] { ";", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			string[] strs = lines[0].Split(',')[1].Split(new string[] { " ", "	" }, StringSplitOptions.RemoveEmptyEntries);

			Transitions = new double[lines.Length, strs.Length];
			for (int i = 0; i < lines.Length; i++)
			{
				strs = lines[i].Split(',');
				int ind = int.Parse(strs[0]);
				strs = strs[1].Split(new string[] { " ", "	" }, StringSplitOptions.RemoveEmptyEntries);

				for (int j = 0; j < strs.Length; j++)
					Transitions[ind, j] = double.Parse(strs[j]);
			}
		}

		private void LoadEmissions(string emissionProbsFileName)
		{
			string data = File.ReadAllText(emissionProbsFileName);
			string[] lines = data.Split(new string[] { ";", Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
			string[] strs = lines[0].Split(',')[1].Split(new string[] { " ", "	" }, StringSplitOptions.RemoveEmptyEntries);

			Emissions = new double[lines.Length, strs.Length];
			for (int i = 0; i < lines.Length; i++)
			{
				strs = lines[i].Split(',');
				int ind = int.Parse(strs[0]);
				strs = strs[1].Split(new string[] { " ", "	" }, StringSplitOptions.RemoveEmptyEntries);

				for (int j = 0; j < strs.Length; j++)
					Emissions[ind, j] = double.Parse(strs[j]);
			}
		}

		private void LoadInitial(string initialProbsFileName)
		{
			string data = File.ReadAllText(initialProbsFileName);

			string[] lines = data.Split(new string[] { ";", Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
			string[] strs;
			Initial = new double[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				strs = lines[i].Split(',');
				Initial[int.Parse(strs[0].Trim())] = double.Parse(strs[1].Trim());
			}
		}

		#endregion

		#region Misc

		public int MidiNoteToHmmMidiNote(int midiNote)
		{
			return midiNote <= 0 ? ObservsCount : (midiNote % ObservsCount);
		}

		public int[] ConvertToHmmMidiNotes(int[] midiNotes)
		{
			return midiNotes.Select(midiNote => MidiNoteToHmmMidiNote(midiNote)).ToArray();
		}

		#endregion
	}
}
