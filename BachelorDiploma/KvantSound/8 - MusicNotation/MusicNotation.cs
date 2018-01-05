using System;
using System.Collections.Generic;
using MusicNotationLib;

namespace KvantSound
{
	public abstract class MusicNotation : Processor
	{
		#region Fields

		protected int? LastNoteId;
		protected long LastNoteDuration;
		protected int LastNoteInd = -1;

		protected int? PrevNoteId;
		protected int PrevNoteInd = 0;
		protected long PrevNoteDuration;

		protected int tempo;
		protected TimeSignature timeSignature;
		protected MusicSymbolDuration minSymbolDuration;
		protected MusicSymbolDuration maxSymbolDuration;
		protected bool dottedMusicSymbolDuration;

		protected DurationSymbol[] DurationSymbols;

		#endregion

		#region Private | Protected Methods

		protected void CalculDurationSymbols()
		{
			int DurationSymbolsLength = (int)minSymbolDuration - (int)maxSymbolDuration + 1;

			if (!dottedMusicSymbolDuration)
				DurationSymbols = new DurationSymbol[DurationSymbolsLength];
			else
				DurationSymbols = new DurationSymbol[DurationSymbolsLength * 2];

			double QuaterTimeDuration = 60 / (double)tempo * 10000000;

			double PowerOfTwo = Math.Pow(2, (int)MusicSymbolDuration.Quarter - (int)minSymbolDuration);

			for (int i = 0; i < DurationSymbolsLength; i++)
			{
				if (!dottedMusicSymbolDuration)
					DurationSymbols[i] = new DurationSymbol()
					{
						SymbolDuration = (MusicSymbolDuration)((int)minSymbolDuration - i),
						TimeDuration = (long)(QuaterTimeDuration * PowerOfTwo),
						Dotted = false
					};
				else
				{
					DurationSymbols[2 * i] = new DurationSymbol()
					{
						SymbolDuration = (MusicSymbolDuration)((int)minSymbolDuration - i),
						TimeDuration = (long)(QuaterTimeDuration * PowerOfTwo),
						Dotted = false
					};
					DurationSymbols[2 * i + 1] = new DurationSymbol()
					{
						SymbolDuration = (MusicSymbolDuration)((int)minSymbolDuration - i),
						TimeDuration = (long)(QuaterTimeDuration * PowerOfTwo * 1.5),
						Dotted = true
					};
				}
				PowerOfTwo *= 2;
			}
		}

		protected DurationSymbol GetDurationSymbol(long Duration)
		{
			if (Duration <= DurationSymbols[0].TimeDuration)
				return DurationSymbols[0];
			if (Duration >= DurationSymbols[DurationSymbols.Length - 1].TimeDuration)
				return DurationSymbols[DurationSymbols.Length - 1];

			for (int i = 1; i < DurationSymbols.Length; i++)
			{
				if (Duration < DurationSymbols[i].TimeDuration)
					if (Duration - DurationSymbols[i - 1].TimeDuration < DurationSymbols[i].TimeDuration - Duration)
						return DurationSymbols[i - 1];
					else
						return DurationSymbols[i];
			}
			return DurationSymbols[DurationSymbols.Length - 1];
		}

		protected long GetDuration(MusicalSymbol Symbol)
		{
			double QuaterTimeDuration = 60 / (double)tempo * 10000000;
			long result = 0;
			if (Symbol is Note)
			{
				var note = Symbol as Note;
				result = (long)Math.Round(Math.Pow(2, 4 - (int)note.Duration) *
						(2.0 - Math.Pow(2, -note.NumberOfDots)) * QuaterTimeDuration);
			}
			else
				if (Symbol is Rest)
				{
					var rest = Symbol as Rest;
					result = (long)Math.Round(Math.Pow(2, 4 - (int)rest.Duration) *
							(2.0 - Math.Pow(2, -rest.NumberOfDots)) * QuaterTimeDuration);
				}
			return result;
		}

		#endregion

		public abstract MusicNotationMode Mode
		{
			get;
		}

		public List<Sample> Samples
		{
			get;
			protected set;
		}

		public MusicNotation()
		{
			Samples = new List<Sample>(1024);
		}
	}
}
