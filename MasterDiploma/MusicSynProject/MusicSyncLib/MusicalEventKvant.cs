namespace MusicSyncLib
{
	public class MusicalEventKvant
	{
		#region Properties

		public MusicalEvent Event
		{
			get;
			protected set;
		}

		public int LocalNumber
		{
			get;
			protected set;
		}

		public int GlobalNumber
		{
			get;
			protected set;
		}

		public double Duration
		{
			get;
			protected set;
		}

		public int MidiNote
		{
			get;
			protected set;
		}

		public int MidiNoteHmm
		{
			get;
			protected set;
		}

		#endregion

		#region Constructors
		
		public MusicalEventKvant(MusicalEvent musEvent, int localNumber, int globalNumber, double duration, int midiNote, int midiNoteHmm)
		{
			LocalNumber = localNumber;
			GlobalNumber = globalNumber;
			Event = musEvent;
			Duration = duration;
			MidiNote = midiNote;
			MidiNoteHmm = MidiNoteHmm;
		}
		
		#endregion

		#region Overrides

		public override string ToString()
		{
			return string.Format("Duration: {0}, MidiNote: {1}, MidiNoteHmm: {2}; (Event:{3})", Duration, MidiNote, MidiNoteHmm, Event.ToString());
		}

		#endregion
	}
}
