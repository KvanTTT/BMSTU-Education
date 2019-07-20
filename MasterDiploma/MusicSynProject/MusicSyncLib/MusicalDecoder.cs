using System.Collections.Generic;

namespace MusicSyncLib
{
	public abstract class MusicalNotesDecoder
	{
		public int Tempo
		{
			get;
			protected set;
		}

		public int Beats
		{
			get;
			protected set;
		}

		public int BeatType
		{
			get;
			protected set;
		}

		public List<MusicalEvent> Events
		{
			get;
			protected set;
		}
	}
}
