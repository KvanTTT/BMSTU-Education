using System.Collections.Generic;

namespace MusicNotationLib
{
	public class Note : MusicalSymbol
	{
		#region Protected fields

		protected int midiPitch;
		protected MusicSymbolDuration duration;
		protected string step;
		protected int octave;
		protected int alter;
		protected bool customStemEndPosition = false;
		protected string noteFlagCharacter = " ";
		protected string noteFlagCharacterRev = " ";
		protected NoteStemDirection stemDirection = NoteStemDirection.Up;
		protected NoteTieType tieType = NoteTieType.None;
		protected List<NoteBeamType> beamList = new List<NoteBeamType>();
		protected TupletType tuplet = TupletType.None;
		protected List<LyricsType> lyrics = new List<LyricsType>();
		protected ArticulationType articulation = ArticulationType.None;
		protected ArticulationPlacementType articulationPlacement = ArticulationPlacementType.Below;
		protected List<string> lyricTexts = new List<string>();
		protected bool hasNatural = false;
		protected bool isGraceNote = false;
		protected bool isChordElement = false;
		protected int currentTempo = 120;
		protected NoteTrillMark trillMark = NoteTrillMark.None;
		protected NoteSlurType slur = NoteSlurType.None;
		protected bool hasFermataSign = false;
		protected int tremoloLevel = 0; //1 - eights (quavers), 2 - sixteenths (semiquavers), etc. / 1 - ósemki, 2 - szesnastki, itp.
		protected int voice = 1;
		protected int dynamics = 80;
		protected Point location = new Point();
		protected Point stemEndLocation = new Point();

		#endregion

		#region Properties

		public bool CustomStemEndPosition { get { return customStemEndPosition; } set { customStemEndPosition = value; } }
		public bool HasFermataSign { get { return hasFermataSign; } set { hasFermataSign = value; } }
		public NoteSlurType Slur { get { return slur; } set { slur = value; } }
		public NoteTrillMark TrillMark { get { return trillMark; } set { trillMark = value; } }
		public int CurrentTempo { get { return currentTempo; } set { currentTempo = value; } }
		public float StemDefaultY { get; set; }
		public TupletType Tuplet { get { return tuplet; } set { tuplet = value; } }
		public List<LyricsType> Lyrics { get { return lyrics; } set { lyrics = value; } }
		public ArticulationType Articulation { get { return articulation; } set { articulation = value; } }
		public ArticulationPlacementType ArticulationPlacement
		{
			get { return articulationPlacement; }
			set { articulationPlacement = value; }
		}
		public List<string> LyricTexts { get { return lyricTexts; } set { lyricTexts = value; } }
		public bool HasNatural { get { return hasNatural; } set { hasNatural = value; } }
		public bool IsGraceNote { get { return isGraceNote; } set { isGraceNote = value; } }
		public bool IsChordElement { get { return isChordElement; } set { isChordElement = value; } }
		public int TremoloLevel { get { return tremoloLevel; } set { tremoloLevel = value; } }
		public string NoteFlagCharacter { get { return noteFlagCharacter; } }
		public string NoteFlagCharacterRev { get { return noteFlagCharacterRev; } }
		public Point Location { get { return location; } set { location = value; } }
		public Point StemEndLocation { get { return stemEndLocation; } set { stemEndLocation = value; } }

		public int NumberOfDots
		{
			get;
			set;
		}
		public NoteStemDirection StemDirection { get { return stemDirection; } }
		public NoteTieType TieType { get { return tieType; } set { tieType = value; } }
		public List<NoteBeamType> BeamList { get { return beamList; } }
		public MusicSymbolDuration Duration { get { return duration; } }
		public string Step { get { return step; } }
		public int Octave { get { return octave; } }
		public int Alter { get { return alter; } }
		public int MidiPitch { get { return midiPitch; } }
		public int Voice { get { return voice; } set { voice = value; } }
		public int Dynamics { get { return dynamics; } set { dynamics = value; } }

		#endregion

		#region Constructor

		public Note()
		{

		}

		public Note(string noteStep, int noteAlter, int noteOctave, MusicSymbolDuration noteDuration,
			NoteStemDirection noteStemDirection, NoteTieType noteTieType, List<NoteBeamType> noteBeamList,
			int numberOfDots = 0)
		{
			type = MusicSymbolType.Note;
			duration = noteDuration;
			step = noteStep;
			octave = noteOctave;
			alter = noteAlter;
			stemDirection = noteStemDirection;
			beamList = noteBeamList;
			tieType = noteTieType;
			midiPitch = MusicalSymbol.ToMidiPitch(step, alter, octave);
			NumberOfDots = numberOfDots;
			DetermineMusicalCharacter();
		}

		#endregion

		#region Private methods

		private void DetermineMusicalCharacter()
		{
			if (duration == MusicSymbolDuration.Whole) musicalCharacter = MusicalCharacters.WholeNote;
			else if (duration == MusicSymbolDuration.Half) musicalCharacter = MusicalCharacters.WhiteNoteHead;
			else if (duration == MusicSymbolDuration.Quarter) musicalCharacter = MusicalCharacters.BlackNoteHead;
			else if (duration == MusicSymbolDuration.Eighth) musicalCharacter = MusicalCharacters.BlackNoteHead;
			else if (duration == MusicSymbolDuration.Sixteenth) musicalCharacter = MusicalCharacters.BlackNoteHead;
			else if (duration == MusicSymbolDuration.ThirtySecond) musicalCharacter = MusicalCharacters.BlackNoteHead;
			else if (duration == MusicSymbolDuration.SixtyFourth) musicalCharacter = MusicalCharacters.BlackNoteHead;
			else if (duration == MusicSymbolDuration.HundredTwentyEighth) musicalCharacter = MusicalCharacters.BlackNoteHead;
			else if (duration == MusicSymbolDuration.Unknown) musicalCharacter = MusicalCharacters.BlackNoteHead;

			if (duration == MusicSymbolDuration.Eighth)
			{
				noteFlagCharacter = MusicalCharacters.NoteFlagEighth;
				noteFlagCharacterRev = MusicalCharacters.NoteFlagEighthRev;
			}
			else if (duration == MusicSymbolDuration.Sixteenth)
			{
				noteFlagCharacter = MusicalCharacters.NoteFlagSixteenth;
				noteFlagCharacterRev = MusicalCharacters.NoteFlagSixteenthRev;
			}
			else if (duration == MusicSymbolDuration.ThirtySecond)
			{
				noteFlagCharacter = MusicalCharacters.NoteFlag32nd;
				noteFlagCharacterRev = MusicalCharacters.NoteFlag32ndRev;
			}
			else if (duration == MusicSymbolDuration.SixtyFourth)
			{
				noteFlagCharacter = MusicalCharacters.NoteFlag64th;
				noteFlagCharacterRev = MusicalCharacters.NoteFlag64thRev;
			}
			else if (duration == MusicSymbolDuration.HundredTwentyEighth)
			{
				noteFlagCharacter = MusicalCharacters.NoteFlag128th;
				noteFlagCharacterRev = MusicalCharacters.NoteFlag128thRev;
			}
		}

		#endregion

		#region Public methods

		public int StepToStepNumber()
		{
			if (step == "C") return 0;
			else if (step == "D") return 1;
			else if (step == "E") return 2;
			else if (step == "F") return 3;
			else if (step == "G") return 4;
			else if (step == "A") return 5;
			else if (step == "B") return 6;
			else return 0;
		}

		#endregion

		#region Public static functions

		public static Note CreateNoteFromMidiPitch(int midiPitch, MusicSymbolDuration noteDuration,
			int numberOfDots)
		{
			int midiPitchInLowestOctave = midiPitch;
			string step = "A";
			int alter = 0;
			int octave = 0;
			while (midiPitchInLowestOctave > 32) midiPitchInLowestOctave -= 12;
			if (midiPitchInLowestOctave == 21) { step = "A"; alter = 0; }
			else if (midiPitchInLowestOctave == 22) { step = "B"; alter = -1; }
			else if (midiPitchInLowestOctave == 23) { step = "B"; alter = 0; }
			else if (midiPitchInLowestOctave == 24) { step = "C"; alter = 0; }
			else if (midiPitchInLowestOctave == 25) { step = "C"; alter = 1; }
			else if (midiPitchInLowestOctave == 26) { step = "D"; alter = 0; }
			else if (midiPitchInLowestOctave == 27) { step = "E"; alter = -1; }
			else if (midiPitchInLowestOctave == 28) { step = "E"; alter = 0; }
			else if (midiPitchInLowestOctave == 29) { step = "F"; alter = 0; }
			else if (midiPitchInLowestOctave == 30) { step = "F"; alter = 1; }
			else if (midiPitchInLowestOctave == 31) { step = "G"; alter = 0; }
			else if (midiPitchInLowestOctave == 32) { step = "G"; alter = 1; }

			if (midiPitch < 24) octave = 0;
			else if (midiPitch < 36) octave = 1;
			else if (midiPitch < 48) octave = 2;
			else if (midiPitch < 60) octave = 3;
			else if (midiPitch < 72) octave = 4;
			else if (midiPitch < 84) octave = 5;
			else if (midiPitch < 96) octave = 6;
			else if (midiPitch < 108) octave = 7;
			else if (midiPitch < 120) octave = 8;

			//octave--;

			return new Note(step, alter, octave, noteDuration, NoteStemDirection.Up,
				NoteTieType.None,
				new List<NoteBeamType>() { NoteBeamType.Single }, numberOfDots);
		}

		#endregion
	}
}
