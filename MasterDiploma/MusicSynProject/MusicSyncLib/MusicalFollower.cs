using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace MusicSyncLib
{
	public class MusicalFollower
	{
		#region Fields

		private Stopwatch _timer;
		private List<TimeSpan> _timeMeasures;
		private List<TimeSpan> _ghostTimeMeasures;
		private List<TimeSpan> _rightTimeMeasures;

		#endregion

		#region Properties

		public MusicalHmmOnline MusicalHmm
		{
			get;
			protected set;
		}

		public int Beats
		{
			get;
			protected set;
		}

		public int BeatType
		{
			get;
			protected set;
		}

		public int Tempo
		{
			get;
			protected set;
		}

		public double RealTempo
		{
			get;
			protected set;
		}

		public int TimeMeasuresCount
		{
			get;
			protected set;
		}

		public int KvantDurationMsec
		{
			get
			{
				return (int)Math.Round(60.0 / Tempo * MusicalHmm.Data.MinDuration * 1000);
			}
		}

		public IEnumerable<TimeSpan> TimeMeasures
		{
			get
			{
				return _timeMeasures;
			}
		}

		public double KvantPenalty
		{
			get;
			protected set;
		}

		public double TempoPenalty
		{
			get;
			protected set;
		}

		public double Rating
		{
			get;
			protected set;
		}

		#endregion

		#region Constructors

		public MusicalFollower(MusicalHmmOnline musicalHmm, int tempo, int beats, int beatType, int lastMeasuresCount = 8)
		{
			MusicalHmm = musicalHmm;
			Tempo = tempo;
			Beats = beats;
			BeatType = beatType;
			TimeMeasuresCount = lastMeasuresCount;
			_timer = new Stopwatch();
			_timeMeasures = new List<TimeSpan>();
			_ghostTimeMeasures = new List<TimeSpan>();
			_rightTimeMeasures = new List<TimeSpan>();
		}

		#endregion

		#region Methods

		public void Start()
		{
			_timer.Start();
			KvantPenalty = 0;
			TempoPenalty = 0;
			Rating = 0;
		}

		public void ProcessObservation(int observation, out double logLikelihood)
		{
			MusicalHmm.AddNewObservation(observation);
			MusicalHmm.DecodeEventWindow(out logLikelihood, true);
			MusicalHmm.UpdateCurrentPos();

			var elapsed = _timer.Elapsed;
			_timeMeasures.Add(elapsed);
			if (MusicalHmm.IsLastEventInRightSeq)
				_rightTimeMeasures.Add(elapsed);

			var path = MusicalHmm.Path;
			var observations = MusicalHmm.Observations;
			var events = MusicalHmm.Data.EventKvants;
			if (path.Count > 1)
			{
				int lastInd = path.Count() - 1;
				if (path[lastInd] < path[lastInd - 1])
					KvantPenalty += (path[lastInd - 1] - path[lastInd]) * 2;
				else if (path[lastInd] == path[lastInd - 1])
					KvantPenalty += 1;
				else if (path[lastInd] != path[lastInd - 1] + 2 || path[lastInd] % (MusicalHmm.Data.GhostStatesCount + 1) == 0)
				{
					KvantPenalty += (path[lastInd] - path[lastInd - 1]) * 1.5;
				}
				else if (events[path[lastInd] / (MusicalHmm.Data.GhostStatesCount + 1)].MidiNote != observations[lastInd])
				{
					KvantPenalty += 2;
				}
			}

			double currentTempo = GetCurrentTempo();
			double tempoMaxError = Tempo * 0.05;
			if (currentTempo > Tempo + tempoMaxError)
			{
				TempoPenalty += 0.03 * Math.Pow((currentTempo - Tempo) * 0.2, 1.5);
			}
			else if (currentTempo < Tempo - tempoMaxError)
			{
				TempoPenalty += 0.05 * Math.Pow((Tempo - currentTempo) * 0.2, 1.5);
			}

			Rating = KvantPenalty + TempoPenalty;
		}

		public double GetCurrentTempo()
		{
			TimeSpan mean = new TimeSpan();
			int timeMeasuresCount = 0;
			for (int i = _rightTimeMeasures.Count - 1; i >= _rightTimeMeasures.Count - TimeMeasuresCount && i - 1 >= 0; i--)
			{
				mean += _rightTimeMeasures[i] - _rightTimeMeasures[i - 1];
				timeMeasuresCount++;
			}

			if (timeMeasuresCount > 0)
			{
				mean = Div(mean, timeMeasuresCount);
				return 60 * MusicalHmm.Data.MinDuration / mean.TotalSeconds;
			}
			else
				return Tempo;
		}

		public void Stop()
		{
			_timer.Stop();
		}

		#endregion

		#region Utils

		public int GetTimerInterval(int tempo)
		{
			return (int)Math.Round(60.0 / tempo * MusicalHmm.Data.MinDuration * 1000);
		}

		public int GetMetronomeInterval(int tempo)
		{
			return (int)Math.Round(60.0 / tempo * (4.0 / BeatType) * 1000);
		}

		public static TimeSpan Average(IEnumerable<TimeSpan> timeSpans)
		{
			long averageTicks = Convert.ToInt64(timeSpans.Average(timeSpan => timeSpan.Ticks));
			return new TimeSpan(averageTicks);
		}

		public static TimeSpan Div(TimeSpan timeSpan, int value)
		{
			return new TimeSpan(timeSpan.Ticks / value);
		}
		
		#endregion
	}
}
