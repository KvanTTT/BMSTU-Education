using System;

namespace KvantSound
{
	public enum NoteLetter
	{
		C, D, E, F, G, A, B // C, C#, D, D#, E, F, F#, G, G#, A, A#, B
	}

	public enum NoteAccidental
	{
		Sharp,
		Flat,
		Natural
	}

	public enum NoteOctave
	{
		SubSubContra = 0,
		SubContra = 1,
		Contra = 2,
		Great = 3,
		Small = 4,
		OneLine = 5,
		TwoLine = 6,
		ThreeLine = 7,
		FourLine = 8,
		FiveLine = 9,
		SixLine = 10
	}

	public enum NoteDotted
	{
		None,
		Single,
		Double,
		Triple
	}

	public enum NoteValue
	{
		 HundredTwentyEighth, SixtyFourth, ThirtyDecond, 
		 Sixteenth, Eighth, Quarter, Half, Whole,
		 DoubleWhole, Longa
	}

	public class MidiNote
	{
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
		static NoteLetter[] IDToNote = null;
		static NoteAccidental[] IDToAccidental = null;


		static MidiNote()
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

			IDToNote = new NoteLetter[]
			{
				NoteLetter.C, NoteLetter.C, NoteLetter.D, NoteLetter.D, NoteLetter.E, NoteLetter.F, 
				NoteLetter.F, NoteLetter.G, NoteLetter.G, NoteLetter.A, NoteLetter.A, NoteLetter.B
			};

			IDToAccidental = new NoteAccidental[]
			{
				 NoteAccidental.Natural, NoteAccidental.Sharp, 
				 NoteAccidental.Natural, NoteAccidental.Sharp, 
				 NoteAccidental.Natural, 
				 NoteAccidental.Natural, NoteAccidental.Sharp,
				 NoteAccidental.Natural, NoteAccidental.Sharp,
				 NoteAccidental.Natural, NoteAccidental.Sharp,
				 NoteAccidental.Natural
			};
		}

		#region Main Read-only properties

		public readonly int ID;

		public readonly long StartTime;

		public readonly long Duration;
		#endregion
		



		public NoteLetter Letter
		{
			get
			{
				return IDToNote[Math.Abs((ID - 69 + 9) % 12)];
			}
		}

		public NoteAccidental Accidental
		{
			get
			{
				return IDToAccidental[Math.Abs((ID - 69 + 9) % 12)];
			}
		}

		public NoteOctave Octave
		{
			get
			{
				return (NoteOctave)(ID / 12);
			}
		}

		public double Freq
		{
			get
			{
				 if (ID < 0)
					 return MinFreq;
				 if (ID > NoteToFrequencyTable.Length)
					 return MaxFreq;
				 return NoteToFrequencyTable[ID];
			}
		   // set;
		}
		

		public NoteDotted Dotted;
		public NoteValue Value;

		public MidiNote(int ID, long StartTime = 0, long Duration = 0)
		{
			this.ID = ID;
			this.StartTime = StartTime;
			this.Duration = Duration;
		}

		public MidiNote(double Freq)
		{
			ID = FreqToID(Freq);
		}

		public MidiNote(double Freq, long StartTime = 0, long Duration = 0)
		{
			ID = FreqToID(Freq);
			this.StartTime = StartTime;
			this.Duration = Duration;
		}



		/// <summary>
		/// Converts the specified frequency to a note.
		/// </summary>
		/// <param name="frequency">
		/// The frequency to convert.
		/// </param>
		/// <returns>
		/// The ID of the note closest to the specified frequency.
		/// </returns>
		public static double ToFreq(int NoteID)
		{
			if (NoteID > 127)
				return NoteToFrequencyTable[127] * Sqrt_2_12;
			if (NoteID < 0)
				return NoteToFrequencyTable[0] / Sqrt_2_12;

			return NoteToFrequencyTable[NoteID]; // minFreq * Math.Pow(Sqrt_2_12, ID - MinID); 
		}

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
				return -1;
			if (Frequency > MaxFreq)
				return +1;

			return (int)Math.Round(Math.Log(Frequency / MinFreq, Sqrt_2_12));
		}

		public override string ToString()
		{
			return Freq.ToString("F2") + " " + ID.ToString() + "  " + Letter.ToString() + Accidental.ToString() + " " + Octave.ToString();
		}
	}
}
