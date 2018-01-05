namespace MusicNotationLib
{
	public class Rest : MusicalSymbol
	{
		#region Protected fields

		protected MusicSymbolDuration duration;
		protected int numberOfDots;
		protected TupletType tuplet = TupletType.None;
		protected int multiMeasure = 0;
		protected int currentTempo = 120;
		protected bool hasFermataSign = false;
		protected int voice = 1;

		#endregion

		#region Properties

		public MusicSymbolDuration Duration { get { return duration; } }
		public int NumberOfDots { get { return numberOfDots; } set { numberOfDots = value; } }
		public TupletType Tuplet { get { return tuplet; } set { tuplet = value; } }
		public int MultiMeasure { get { return multiMeasure; } set { multiMeasure = value; } }
		public int CurrentTempo { get { return currentTempo; } set { currentTempo = value; } }
		public bool HasFermataSign { get { return hasFermataSign; } set { hasFermataSign = value; } }
		public int Voice { get { return voice; } set { voice = value; } }

		#endregion

		#region Constructor

		public Rest(MusicSymbolDuration restDuration, int numberOfDots = 0)
		{
			type = MusicSymbolType.Rest;
			duration = restDuration;
			NumberOfDots = numberOfDots;
			DetermineMusicalCharacter();
		}

		#endregion

		#region Private methods

		private void DetermineMusicalCharacter()
		{
			if (duration == MusicSymbolDuration.Whole) musicalCharacter = MusicalCharacters.WholeRest;
			else if (duration == MusicSymbolDuration.Half) musicalCharacter = MusicalCharacters.HalfRest;
			else if (duration == MusicSymbolDuration.Quarter) musicalCharacter = MusicalCharacters.QuarterRest;
			else if (duration == MusicSymbolDuration.Eighth) musicalCharacter = MusicalCharacters.EighthRest;
			else if (duration == MusicSymbolDuration.Sixteenth) musicalCharacter = MusicalCharacters.SixteenthRest;
		}

		#endregion
	}
}
