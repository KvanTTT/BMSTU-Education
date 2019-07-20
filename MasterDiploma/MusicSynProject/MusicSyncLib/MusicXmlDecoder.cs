using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace MusicSyncLib
{
	public class MusicXmlDecoder : MusicalNotesDecoder
	{
		private static XmlSerializer _xmlSerializer = new XmlSerializer(typeof(scorepartwise));

		public MusicXmlDecoder(string fileName)
			: base()
		{
			scorepartwise score;
			using (var reader = new StreamReader(fileName))
			{
				score = (scorepartwise)_xmlSerializer.Deserialize(reader);
			}

			Events = new List<MusicalEvent>();

			double divisions = 1;
			Beats = 4;
			BeatType = 4;
			Tempo = 120;

			int currentEventNumber = 0;
			foreach (var meause in score.part[0].measure)
			{
				foreach (var item in meause.Items)
				{
					var attributes = item as attributes;
					if (attributes != null)
					{
						if (attributes.divisions != 0)
							divisions = (double)(attributes.divisions);
						if (attributes.time != null)
							Beats = Convert.ToInt32(attributes.time[0].Items[0]);
						if (attributes.time != null)
							BeatType = Convert.ToInt32(attributes.time[0].Items[1]);
					}

					var direction = item as direction;
					if (direction != null)
					{
						var sound = direction.sound;
						if (sound != null && sound.tempo != 0.0m)
							Tempo = (int)sound.tempo;
					}

					var note = item as note;
					if (note != null && (note.staff == null || int.Parse(note.staff) == 1))
					{
						var pitch = note.Items[0] as pitch;
						if (pitch != null)
						{
							Events.Add(new MusicalEvent
							{
								Number = currentEventNumber++,
								MidiNote = MusicalTemperament.NoteNameToMidiNote(pitch.step.ToString(), (int)pitch.alter, int.Parse(pitch.octave)),
								Duration = Convert.ToDouble(note.Items[1]) / divisions
							});
						}
						else
						{
							var rest = note.Items[0] as rest;
							if (rest != null)
							{
								Events.Add(new MusicalEvent
								{
									Number = currentEventNumber++,
									MidiNote = -1,
									Duration = Convert.ToDouble(note.Items[1]) / divisions
								});
							}
						}
					}
				}
			}
		}

	}
}
