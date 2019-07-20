using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicSyncLib
{
	/// <summary>
	/// Stores one record
	/// </summary>
	public struct PitchRecord
	{
		/// <summary>
		/// The index of the pitch record since the last Reset call
		/// </summary>
		public int RecordIndex { get; set; }

		/// <summary>
		/// The detected pitch
		/// </summary>
		public float Pitch { get; set; }

		/// <summary>
		/// The detected MIDI note, or 0 for no pitch
		/// </summary>
		public int MidiNote { get; set; }

		/// <summary>
		/// The offset from the detected MIDI note in cents, from -50 to +50.
		/// </summary>
		public int MidiCents { get; set; }
	}

}
