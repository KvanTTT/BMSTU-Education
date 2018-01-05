using MusicNotationLib;
using System;
using System.Collections.Generic;

namespace KvantSound
{
	public struct DurationSymbol
	{
		public long TimeDuration;
		public MusicSymbolDuration SymbolDuration;
		public bool Dotted;
	}

	public class MusicNotationCreator : MusicNotation
	{
		public override MusicNotationMode Mode
		{
			get
			{
				return MusicNotationMode.Create;
			}
		}

		#region Fields

		bool PrevSilent = true;

		#endregion

		#region Properties

		#region Realtime properties

		public int Tempo
		{
			get
			{
				return tempo;
			}
			set
			{
				if (tempo != value)
				{
					tempo = value;
					CalculDurationSymbols();
				}
			}
		}

		public TimeSignature TimeSignature
		{
			get
			{
				return timeSignature;
			}
			set
			{
				if (timeSignature != value)
				{
					timeSignature = value;
					CalculDurationSymbols();
				}
			}
		}

		public MusicSymbolDuration MinSymbolDuration
		{
			get
			{
				return minSymbolDuration;
			}
			set
			{
				if (minSymbolDuration != value)
				{
					minSymbolDuration = value;
					CalculDurationSymbols();
				}
			}
		}

		public MusicSymbolDuration MaxSymbolDuration
		{
			get
			{
				return maxSymbolDuration;
			}
			set
			{
				if (maxSymbolDuration != value)
				{
					maxSymbolDuration = value;
					CalculDurationSymbols();
				}
			}
		}

		public bool DottedMusicSymbolDuration
		{
			get
			{
				return dottedMusicSymbolDuration;
			}
			set
			{
				if (dottedMusicSymbolDuration != value)
				{
					dottedMusicSymbolDuration = value;
					CalculDurationSymbols();
				}
			}
		}

		public bool LegatoSupport
		{
			get;
			set;
		}

		#endregion
		
		#endregion

		#region Delegates

		public UpdateMuscialSymbolDelegate AddMuscialSymbol
		{
			get;
			set;
		}
		
		#endregion

		#region Private | Protected Methods

		private int AvgNoteId(IList<int> NoteIds)
		{
			int Sum = 0;
			int Min = int.MaxValue, Max = int.MinValue;

			int RestCount = 0;
			int NoteCount = 0;
			Dictionary<int, int> NoteIdCount = new Dictionary<int,int>();
			foreach (var NoteId in NoteIds)
			{
				if (NoteId == -1)
					RestCount++;
				else
				{
					if (!NoteIdCount.ContainsKey(NoteId))
						NoteIdCount.Add(NoteId, 1);
					else
						NoteIdCount[NoteId]++;

					NoteCount++;

					if (NoteId > Max)
						Max = NoteId;
					if (NoteId < Min)
						Min = NoteId;
				}
			}

			if (RestCount >= NoteCount)
				return -1;
			else
			{
				var MaxCountValue = int.MinValue;
				KeyValuePair<int, int> MaxCount = new KeyValuePair<int,int>();
				foreach (var KeyValuePair in NoteIdCount)
					if (KeyValuePair.Value > MaxCountValue)
					{
						MaxCountValue = KeyValuePair.Value;
						MaxCount = KeyValuePair;
					}
				foreach (var KeyValuePair in NoteIdCount)
				{
					Sum = 0;
					NoteCount = 0;
					if (Math.Abs(KeyValuePair.Key - MaxCount.Key) < 3
						&& KeyValuePair.Key != -1)
					{
						Sum += KeyValuePair.Key * KeyValuePair.Value;
						NoteCount += KeyValuePair.Value;
					}
				}
				if (NoteCount == 0)
					return -1;
				else
					return (int)Math.Round((double)Sum / NoteCount);
			}
		}

		protected virtual MusicalSymbol SendMusicSymbol()
		{
			DurationSymbol DurationSymbol = GetDurationSymbol(LastNoteDuration);
			MusicalSymbol Result;

			/*if (PrevSilent)
				Result = new Rest(DurationSymbol.SymbolDuration, Convert.ToInt32(DurationSymbol.Dotted));
			else
			{
				List<int> NoteIds = new List<int>(Samples.Count - LastNoteInd);
				for (int i = LastNoteInd; i < Samples.Count; i++)
					NoteIds.Add(Samples[i].ID);

				LastNoteId = AvgNoteId(NoteIds);

				if (LastNoteId == -1)
					Result = new Rest(DurationSymbol.SymbolDuration, Convert.ToInt32(DurationSymbol.Dotted));
				else
					Result = Note.CreateNoteFromMidiPitch((int)LastNoteId, DurationSymbol.SymbolDuration,
						Convert.ToInt32(DurationSymbol.Dotted));
			}
			*/
			
			if (LastNoteId == -1)
				Result = new Rest(DurationSymbol.SymbolDuration, Convert.ToInt32(DurationSymbol.Dotted));
			else
				Result = Note.CreateNoteFromMidiPitch((int)LastNoteId, DurationSymbol.SymbolDuration,
					Convert.ToInt32(DurationSymbol.Dotted));
			
			if (AddMuscialSymbol != null)
				AddMuscialSymbol(Result);

			return Result;
		}

		private void SendMusicSymbol(MusicalSymbol Symbol)
		{
			if (AddMuscialSymbol != null)
				AddMuscialSymbol(Symbol);
		}
		
		#endregion

		#region Internal Methods

		internal override void Process(Sample Sample)
		{
			Samples.Add(Sample);
			
			/*if (Sample.Silent ^ PrevSilent == true)
			{
				//if (LastNoteDuration >= DurationSymbols[0].TimeDuration)
				{
					SendMusicSymbol();
					PrevNoteDuration = LastNoteDuration;
					PrevNoteInd = LastNoteInd;
					LastNoteDuration = 0;
					LastNoteInd = Samples.Count - 1;
				}
				/*else
				{
					LastNoteDuration += PrevNoteDuration;
					LastNoteInd = PrevNoteInd;
				}*/
			/*}

			LastNoteDuration += Sample.Duration;
			if (LastNoteDuration >= DurationSymbols[DurationSymbols.Length - 1].TimeDuration)
			{
				SendMusicSymbol();
				PrevNoteDuration = LastNoteDuration;
				PrevNoteInd = LastNoteInd;
				LastNoteDuration = 0;
				LastNoteInd = Samples.Count - 1;
			}

			PrevSilent = Sample.Silent;*/
			
			
			if (LastNoteId == null)
			{
				PrevNoteId = LastNoteId;
				PrevNoteDuration = LastNoteDuration;

				LastNoteId = Sample.ID;
				LastNoteDuration = Sample.Duration;

				LastNoteInd = Samples.Count - 1;
			}
			else
				if (Sample.ID == LastNoteId)
				{
					LastNoteDuration += Sample.Duration;
					if (LastNoteDuration >= DurationSymbols[DurationSymbols.Length - 1].TimeDuration)
					{
						SendMusicSymbol();
						LastNoteId = null;
						LastNoteDuration = 0;
					}
				}
				else
				{
					SendMusicSymbol();
					LastNoteId = null;
					LastNoteDuration = 0;

					LastNoteDuration += Sample.Duration;
					if (LastNoteDuration >= DurationSymbols[DurationSymbols.Length - 1].TimeDuration)
					{
						SendMusicSymbol();
						LastNoteId = null;
						LastNoteDuration = 0;
					}
					
					/*if (LastNoteDuration < DurationSymbols[0].TimeDuration)
					{
						LastNoteId = PrevNoteId;
						LastNoteDuration = PrevNoteDuration + LastNoteDuration;
						if (LastNoteDuration >= DurationSymbols[DurationSymbols.Length - 1].TimeDuration)
						{
							SendMusicSymbol();
							LastNoteId = null;
							LastNoteDuration = 0;
						}
					}
					else
					{
						if (LastNoteId != -1)
						{
							SendMusicSymbol();
							LastNoteId = null;
							LastNoteDuration = 0;
						}

						PrevNoteId = LastNoteId;
						PrevNoteDuration = LastNoteDuration;

						LastNoteId = Sample.ID;
						LastNoteDuration = Sample.Duration;
					}*/
				}
		}

		public static void AppendShort(List<byte> buffer, ushort value)
		{
			buffer.Add((byte)(value >> 8));
			buffer.Add((byte)(value));
		}

		public static void AppendInt(List<byte> buffer, uint value)
		{
			buffer.Add((byte)(value >> 24));
			buffer.Add((byte)(value >> 16));
			buffer.Add((byte)(value >> 8));
			buffer.Add((byte)(value));
		}

		public static void AppendPackedInt(List<byte> buffer, uint value)
		{
			if (value < 0x80)
				buffer.Add((byte)(value & 0xFF));
			else
				if (value < 0x4000)
				{
					buffer.Add((byte)(0x80 | (value >> 7)));
					buffer.Add((byte)(0x7F & value));
				}
				else
					if (value < 0x200000)
					{
						buffer.Add((byte)(0x80 | (value >> 14)));
						buffer.Add((byte)(0x80 | (value >> 7)));
						buffer.Add((byte)(0x7F & value));
					}
					else
					{
						buffer.Add((byte)(0x80 | (value >> 21)));
						buffer.Add((byte)(0x80 | (value >> 14)));
						buffer.Add((byte)(0x80 | (value >> 7)));
						buffer.Add((byte)(0x7F & value));
					}
		}

		public byte[] SaveToMidi()
		{
			ushort TicksPerBeat = 480; // тикков на четверть

			List<byte> Buffer = new List<byte>(512);

			// chunk ID
			Buffer.Add(0x4D);
			Buffer.Add(0x54);
			Buffer.Add(0x68);
			Buffer.Add(0x64);

			// chunk size
			Buffer.Add(0x00);
			Buffer.Add(0x00);
			Buffer.Add(0x00);
			Buffer.Add(0x06);

			// format type
			Buffer.Add(0x00);
			Buffer.Add(0x00); // SMF = 1

			// number of tracks
			Buffer.Add(0x00);
			Buffer.Add(0x01);

			// time division
			AppendShort(Buffer, TicksPerBeat);

			// Track Chunk
			// chunk ID
			Buffer.Add(0x4D);
			Buffer.Add(0x54);
			Buffer.Add(0x72);
			Buffer.Add(0x6B);

			AppendInt(Buffer, 0);
			int BufferLengthInd = Buffer.Count - 1;

			Buffer.Add(0x00);
			Buffer.Add(0xFF);
			Buffer.Add(0x58);
			Buffer.Add(0x04);

			// 4/4
			Buffer.Add(0x04);
			Buffer.Add(0x02);

			// methronome
			Buffer.Add(0x18);
			Buffer.Add(0x08);

			// ms per quater
			Buffer.Add(0x00);
			Buffer.Add(0xFF);
			Buffer.Add(0x51);
			Buffer.Add(0x03);

			uint MsPerQuater = (uint)Math.Round(60.0 / Tempo * 1000000.0);
			Buffer.Add((byte)(MsPerQuater >> 16));
			Buffer.Add((byte)(MsPerQuater >> 8));
			Buffer.Add((byte)(MsPerQuater));

			// channel
			Buffer.Add(0x00);
			Buffer.Add(0xC0);
			Buffer.Add(0x02);

			uint OffsetTime = 0;

			int NoteCounter = 0;
			for (int i = 0; i < Samples.Count; i++)
			{
				if (!Samples[i].Silent)
				{
					AppendPackedInt(Buffer, OffsetTime);

					// Note On on channel 0
					Buffer.Add(0x90);

					// Note Number
					Buffer.Add((byte)Samples[i].ID);

					// Velocity
					Buffer.Add(127);

					OffsetTime += (uint)Math.Round(Samples[i].Duration / 100000000.0 * 60.0 / tempo * TicksPerBeat);

					AppendPackedInt(Buffer, OffsetTime);

					// Note Off on channel 0
					Buffer.Add(0x80);

					// Note Number
					Buffer.Add((byte)Samples[i].ID);

					// Velocity
					Buffer.Add(127);

					OffsetTime = 0;

					NoteCounter++;
				}
				else
						OffsetTime += (uint)Math.Round(Samples[i].Duration / 100000000.0 * 60.0 / tempo * TicksPerBeat);
			}

			// The end of track
			Buffer.Add(0x00);
			Buffer.Add(0xFF);
			Buffer.Add(0x2F);
			Buffer.Add(0x00);

			int TrackLength = Buffer.Count - BufferLengthInd - 1;
			Buffer[BufferLengthInd - 3] = (byte)(TrackLength >> 24);
			Buffer[BufferLengthInd - 2] = (byte)(TrackLength >> 16);
			Buffer[BufferLengthInd - 1] = (byte)(TrackLength >> 8);
			Buffer[BufferLengthInd - 0] = (byte)(TrackLength);

			return Buffer.ToArray();
		}

		#endregion

		#region Constructors

		public MusicNotationCreator(UpdateMuscialSymbolDelegate AddMuscialSymbolDelegate = null, 
			TimeSignature TimeSignature = null,
			int Tempo = 120,
			MusicSymbolDuration MinSymbolDuration = MusicSymbolDuration.Sixteenth,
			MusicSymbolDuration MaxSymbolDuration = MusicSymbolDuration.Whole,
			bool DottedMusicSymbolDuration = false,
			bool LegatoSupport = false)
		{
			Samples = new List<Sample>(16);
			AddMuscialSymbol = AddMuscialSymbolDelegate;

			if (timeSignature == null)
				this.timeSignature = new TimeSignature(TimeSignatureType.Common, 4, 4);
			else
				this.timeSignature = TimeSignature;
			tempo = Tempo;
			minSymbolDuration = MinSymbolDuration;
			maxSymbolDuration = MaxSymbolDuration;
			dottedMusicSymbolDuration = DottedMusicSymbolDuration;
			CalculDurationSymbols();
			this.LegatoSupport = LegatoSupport;

			SendMusicSymbol(new Clef(ClefType.GClef, 2));
			SendMusicSymbol(new Key(0));
			
			SendMusicSymbol(timeSignature);
		}		

		#endregion
	}
}
