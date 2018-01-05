using System;

namespace KvantSound
{
	public class PitchDetector : Processor
	{
		#region Fields

		public const double Sqrt_2_12 = 1.0594630943592952645618252949463;

		public static double MinFreq
		{
			get;
			protected set;
		}
		public static double MaxFreq
		{
			get;
			protected set;
		}

		public readonly static double[] NoteToFrequencyTable = new double[128];

		#endregion

		#region Public Methods

		/// <summary>
		/// Converts the specified note to a frequency.
		/// </summary>
		/// <param name="noteID">
		/// The ID of the note to convert.
		/// </param>
		/// <returns>
		/// The frequency of the specified note.
		/// </returns>
		public static int FreqToID(double Frequency)
		{
			if (Frequency < MinFreq)
				return int.MinValue;
			if (Frequency > MaxFreq)
				return int.MaxValue;

			return (int)Math.Round(Math.Log(Frequency / MinFreq, Sqrt_2_12));
		}

		#endregion

		#region Processor Implemantation

		internal override void Process(Sample Sample)
		{
			if (Sample.Silent || Sample.FundamentalFreq == 0)
			{
				Sample.ID = -1;
				return;
			}

			Sample.ID = FreqToID(Sample.FundamentalFreq);
			if (Sample.ID < 60 || Sample.ID > 60 + 37)
				Sample.ID = -1;
		}

		#endregion		

		#region Constructors

		static PitchDetector()
		{
			double ReferenceFrequency = 440;
			int ReferenceNoteID = 69;

			int NotesPerOctave = 12;
			double Exponent;

			for (int i = 0; i < NoteToFrequencyTable.Length; i++)
			{
				Exponent = (double)(i - ReferenceNoteID) / NotesPerOctave;
				NoteToFrequencyTable[i] = ReferenceFrequency * Math.Pow(2.0, Exponent);
			}

			MinFreq = NoteToFrequencyTable[0];
			MaxFreq = NoteToFrequencyTable[NoteToFrequencyTable.Length - 1];
		}

		#endregion
	}
}
