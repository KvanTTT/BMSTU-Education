/*
Polish System for Archivising Music Control Library (PSAM Control Library)
http://www.archiwistykamuzyczna.pl/index.php?article=download&lang=en#MusicNotationLib

Copyright (c) 2010, Jacek Salamon
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list
  of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list
  of conditions and the following disclaimer in the documentation and/or other
  materials provided with the distribution.
* Neither the name of Jacek Salamon nor the names of contributors may be used to
  endorse or promote products derived from this software without specific prior
  written permission.
 
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT
OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR
TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 

============================================================================================
Fugue Icons
Copyright (C) 2009 Yusuke Kamiyamane. All rights reserved.
The icons are licensed under a Creative Commons Attribution 3.0 license.
*/


using System;

namespace MusicNotationLib
{
	#region Public enumeration types
	
	
	public enum TupletType { None, Start, Stop };
	public enum LyricsType { None, Begin, Middle, End , Single};
	public enum ArticulationType { None, Staccato, Accent };
	public enum ArticulationPlacementType { Above, Below };
	public enum DirectionPlacementType { Above, Below, Custom };
	public enum TimeSignatureType { Common, Cut, Numbers };
	public enum NoteTrillMark { None, Above, Below };
	public enum NoteSlurType { None, Start, Stop };
	public enum RepeatSignType { None, Forward, Backward };

	#endregion

	#region Base class: MusicalSymbol

	public class MusicalSymbol
	{
		#region Protected fields

		protected MusicSymbolType type;
		protected string musicalCharacter = " ";
		protected string musicalCharacterColor = "FF000000";

		#endregion

		#region Properties

		public MusicSymbolType Type { get { return type; } }
		public string MusicalCharacter { get { return musicalCharacter; } }
		public string Color { get { return musicalCharacterColor; } set { musicalCharacterColor = value; } }

		#endregion

		#region Constructor

		public MusicalSymbol()
		{
			type = MusicSymbolType.Unknown;
		}

		#endregion

		#region Public static functions

		public static int ToMidiPitch(string step, int alter, int octave)
		{
			int pitch;
			if (step == "A") pitch = 21;
			else if (step == "B") pitch = 23;
			else if (step == "C") pitch = 24;
			else if (step == "D") pitch = 26;
			else if (step == "E") pitch = 28;
			else if (step == "F") pitch = 29;
			else if (step == "G") pitch = 31;
			else return 0;
			//Dźwięki A i B(H) są w standardzie MIDI w oktawie 0:
			//Notes A0 and B0 are in octave 0 in MIDI standard:
			if ((step == "A") || (step == "B")) pitch = pitch + octave * 12;
			else pitch = pitch + (octave - 1)* 12;  //The other are in octave 1 / A pozostałe w pierwszej oktawie
			pitch = pitch + alter;
			return pitch;
		}

		public static int FrequencyToMidiPitch(double freq)
		{
			double i=0;
			//27,5 Hz to częstotliwość dźwięku A subkontra (najniższego w standardzie MIDI)
			//27,5 Hz is the frequency of note A subcontra (A0) (the lowest in MIDI standard)
			while (true)
			{
				if ((freq < 27.5f * Math.Pow(2, 1.0f / 36) * Math.Pow(2, i / 12)) &&
					(freq >= 27.5f * Math.Pow(2, -1.0f / 18) * Math.Pow(2, i / 12)))
					break;
				i++;
				if (i > 100) break;
			}
			return (int)i + 21;
		}



		public static int ToClefMidiPitch(ClefType type)
		{
			if (type == ClefType.CClef) return 60;
			else if (type == ClefType.FClef) return 53;
			else if (type == ClefType.GClef) return 67;
			else return 0;
		}
		public static int StepDifference(Clef a, Note b)
		{
			int step1int = 0, step2int = 0;
			string step1 = a.Step;
			string step2 = b.Step;
			if (step1 == "C") step1int = 0;
			else if (step1 == "D") step1int = 1;
			else if (step1 == "E") step1int = 2;
			else if (step1 == "F") step1int = 3;
			else if (step1 == "G") step1int = 4;
			else if (step1 == "A") step1int = 5;
			else if (step1 == "B") step1int = 6;

			step1int += a.Octave * 7;

			if (step2 == "C") step2int = 0;
			else if (step2 == "D") step2int = 1;
			else if (step2 == "E") step2int = 2;
			else if (step2 == "F") step2int = 3;
			else if (step2 == "G") step2int = 4;
			else if (step2 == "A") step2int = 5;
			else if (step2 == "B") step2int = 6;

			step2int += b.Octave * 7;

			return step1int - step2int;
		}

		#endregion

	}

	#endregion

}