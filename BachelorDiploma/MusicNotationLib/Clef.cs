using System;

namespace MusicNotationLib
{
	public class Clef : MusicalSymbol
	{
		#region Private fields

		private ClefType typeOfClef;
		private int clefPitch;
		private int line;
		private string step = "G";
		private int octave = 4;

		#endregion

		#region Properties

		public string Step { get { return step; } }
		public int Octave { get { return octave; } }
		public ClefType TypeOfClef { get { return typeOfClef; } }
		public int Line { get { return line; } }
		public int ClefPitch { get { return clefPitch; } }

		#endregion

		#region Constructor

		public Clef(ClefType clefType, int whichLine)
		{
			type = MusicSymbolType.Clef;
			typeOfClef = clefType;
			line = whichLine;
			clefPitch = MusicalSymbol.ToClefMidiPitch(typeOfClef);
			if (typeOfClef == ClefType.GClef)
			{
				musicalCharacter = MusicalCharacters.GClef;
				step = "G";
				octave = 4;
			}
			else if (typeOfClef == ClefType.FClef)
			{
				musicalCharacter = MusicalCharacters.FClef;
				step = "F";
				octave = 3;
			}
			else if (typeOfClef == ClefType.CClef)
			{
				musicalCharacter = MusicalCharacters.CClef;
				step = "C";
				octave = 4;
			}
		}

		#endregion

		#region Public static functions

		public static Clef SuggestClefForANote(Note n)
		{
			if (n.MidiPitch < 55) return new Clef(ClefType.FClef, 4);
			else return new Clef(ClefType.GClef, 2);
		}
		public static Clef SuggestClefForANote(Note n, Clef currentClef)
		{
			if (currentClef.TypeOfClef == ClefType.GClef)
			{
				if ((currentClef.Line == 1) && (n.MidiPitch < 59)) return new Clef(ClefType.FClef, 4);
				else if ((currentClef.Line == 2) && (n.MidiPitch < 55)) return new Clef(ClefType.FClef, 4);
				else if ((currentClef.Line == 3) && (n.MidiPitch < 52)) return new Clef(ClefType.FClef, 4);
				else if ((currentClef.Line == 4) && (n.MidiPitch < 48)) return new Clef(ClefType.FClef, 4);
				else if ((currentClef.Line == 5) && (n.MidiPitch < 45)) return new Clef(ClefType.FClef, 4);
				else return currentClef;
			}
			else if (currentClef.TypeOfClef == ClefType.FClef)
			{
				if ((currentClef.Line == 1) && (n.MidiPitch > 74)) return new Clef(ClefType.GClef, 2);
				else if ((currentClef.Line == 2) && (n.MidiPitch > 71)) return new Clef(ClefType.GClef, 2);
				else if ((currentClef.Line == 3) && (n.MidiPitch > 67)) return new Clef(ClefType.GClef, 2);
				else if ((currentClef.Line == 4) && (n.MidiPitch > 64)) return new Clef(ClefType.GClef, 2);
				else if ((currentClef.Line == 5) && (n.MidiPitch > 60)) return new Clef(ClefType.GClef, 2);
				else return currentClef;
			}
			else return new Clef(ClefType.GClef, 2);
		}

		#endregion
	}
}
