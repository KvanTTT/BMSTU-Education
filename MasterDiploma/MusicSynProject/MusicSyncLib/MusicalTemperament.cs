using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicSyncLib
{
	public class MusicalTemperament
	{
		private static string[] noteString = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
		public static readonly double InverseLog2 = 1.0 / Math.Log10(2.0);

		public static bool PitchToMidiNote(float pitch, out int note, out int cents)
		{
			if (pitch < 20.0f)
			{
				note = -1;
				cents = 0;
				return false;
			}

			var fNote = (float)((12.0 * Math.Log10(pitch / 55.0) * InverseLog2)) + 33.0f;
			note = (int)(fNote + 0.5f);
			cents = (int)((note - fNote) * 100);
			return true;
		}

		public static float PitchToMidiNote(float pitch)
		{
			if (pitch < 20.0f)
				return 0.0f;

			return (float)(12.0 * Math.Log10(pitch / 55.0) * InverseLog2) + 33.0f;
		}

		public static float MidiNoteToPitch(float note)
		{
			if (note < 33.0f)
				return 0.0f;

			var pitch = (float)Math.Pow(10.0, (note - 33.0f) / InverseLog2 / 12.0f) * 55.0f;
			return pitch;
		}

		public static string MidiNoteToNoteName(int note, bool sharps = true, bool showOctave = true)
		{
			if (note <= 0)
				return "-";

			int octave = (note / 12) - 1;
			int noteIndex = (note % 12);
			string result = noteString[noteIndex];

			if (!sharps)
				result = result.Replace('#', 'b');

			if (showOctave)
				result += " " + octave;

			return result;
		}

		public static int NoteNameToMidiNote(string noteAndOctave)
		{
			var strs = noteAndOctave.Split(' ');
			return NoteNameToMidiNote(strs[0], int.Parse(strs[1]));
		}

		public static int NoteNameToMidiNote(string note, int octave)
		{
			string s = note.Replace('b', '#').ToUpperInvariant();
			int ind = -1;
			for (int i = 0; i < noteString.Length; i++)
				if (s == noteString[i])
				{
					ind = i;
					break;
				}

			return (octave + 1) * 12 + ind;
		}

		public static int NoteNameToMidiNote(string note, int alter, int octave)
		{
			string s = note.ToUpperInvariant();
			int ind = -1;
			for (int i = 0; i < noteString.Length; i++)
				if (noteString[i].Contains(s))
				{
					ind = i;
					break;
				}

			if (alter > 0)
			{
				octave += (ind + alter) / 12;
				ind = (ind + alter) % 12;
			}
			else if (alter < 0)
			{
				ind = ind + alter;
				if (ind < 0)
				{
					octave += (ind - 11) / 12;
					ind = 12 + ind;
				}
			}

			return (octave + 1) * 12 + ind;
		}
	}
}
