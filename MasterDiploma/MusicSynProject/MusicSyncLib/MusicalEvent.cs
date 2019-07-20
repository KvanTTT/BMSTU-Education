using System.Collections.Generic;

namespace MusicSyncLib
{
	public class MusicalEvent
	{
		#region Fields

		private int _number;
		private int _midiNote;
		private double _pitch = -1;
		private double _duration;

		#endregion

		#region Properties

		public int Number
		{
			get
			{
				return _number;
			}
			set
			{
				_number = value;
			}
		}
		
		public double Duration
		{
			get
			{
				return _duration;
			}
			set
			{
				_duration = value;
			}
		}

		public int MidiNote
		{
			get
			{
				return _midiNote;
			}
			set
			{
				_midiNote = value;
				_pitch = -1;
			}
		}

		public double Pitch
		{
			get
			{
				if (_pitch == -1)
					_pitch = MusicalTemperament.MidiNoteToPitch(MidiNote);
				return _pitch;
			}
		}

		#endregion

		public List<MusicalEventKvant> Kvants
		{
			get;
			set;
		}

		public override string ToString()
		{
			return "NoteId: " + MidiNote + "; Duration: " + Duration;
		}
	}
}
