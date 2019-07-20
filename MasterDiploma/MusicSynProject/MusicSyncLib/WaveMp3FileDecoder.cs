using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace MusicSyncLib
{
	public class WaveMp3FileDecoder
	{
		#region Properties & fields

		private PitchTracker _pitchTracker;

		public int PieceLengthInMSec
		{
			get;
			set;
		}

		public double DetectLevelThreshold
		{
			get;
			set;
		}

		public int BitsPerSample
		{
			get;
			protected set;
		}

		public int ChannelCount
		{
			get;
			protected set;
		}

		public int SampleRate
		{
			get;
			protected set;
		}

		public float[] PcmData
		{
			get;
			protected set;
		}

		public List<PitchRecord> Pitchs
		{
			get;
			protected set;
		}

		public int Beats;

		public int BeatType;

		public int Divisions;

		#endregion

		#region Constructors

		public WaveMp3FileDecoder()
		{
			PieceLengthInMSec = 200;
			DetectLevelThreshold = 0.01;
		}

		#endregion

		#region Init Methods

		public void LoadAudioFile(string fileName)
		{
			byte[] buffer;
			int inc;
			double totalSec;

			string extension = Path.GetExtension(fileName);
			WaveStream stream = null;
			switch (extension)
			{
				case ".wav":
				case ".wave":
					stream = new WaveFileReader(fileName);
					break;
				case ".mp3":
					stream = new Mp3FileReader(fileName);
					break;
			}

			if (stream != null)
			{
				using (stream)
				{
					BitsPerSample = stream.WaveFormat.BitsPerSample;
					ChannelCount = stream.WaveFormat.Channels;
					inc = ChannelCount * BitsPerSample / 8;
					totalSec = stream.TotalTime.TotalSeconds;
					SampleRate = stream.WaveFormat.SampleRate;
					buffer = new byte[stream.Length];
					stream.Read(buffer, 0, buffer.Length);
				}

				PcmData = new float[(buffer.Length + inc - 1) / inc];

				if (BitsPerSample == 8)
				{
					for (int i = 0; i < PcmData.Length; i++)
						PcmData[i] = (float)buffer[i * inc] / 127;
				}
				else if (BitsPerSample == 16)
				{
					for (int i = 0; i < PcmData.Length; i++)
						PcmData[i] = (float)(short)((buffer[i * inc + 1] << 8) + buffer[i * inc]) / short.MaxValue;
				}

				_pitchTracker = new PitchTracker();
				_pitchTracker.PitchRecordsPerSecond = 1 / PieceLengthInMSec;
				_pitchTracker.SampleRate = SampleRate;
				_pitchTracker.RecordPitchRecords = true;
				_pitchTracker.DetectLevelThreshold = (float)DetectLevelThreshold;
			}
		}

		public void InitRealtimeMode(int channelCount, int bitsPerSample, int sampleRate, int sampleLengthMSec, PitchDetectedHandler handler)
		{
			ChannelCount = channelCount;
			BitsPerSample = bitsPerSample;
			PieceLengthInMSec = sampleLengthMSec;

			_pitchTracker = new PitchTracker();
			_pitchTracker.PitchRecordsPerSecond = 1000.0 / PieceLengthInMSec;
			_pitchTracker.SampleRate = sampleRate;
			_pitchTracker.RecordPitchRecords = true;
			_pitchTracker.DetectLevelThreshold = (float)DetectLevelThreshold;
			_pitchTracker.PitchDetected += handler;
		}

		#endregion

		#region Methods

		public void RecognizePitchs()
		{
			_pitchTracker.ProcessBuffer(PcmData);
			Pitchs = _pitchTracker.PitchRecords.Cast<PitchRecord>().ToList();
		}

		public void AddDataInRealTime(float[] pcmData)
		{
			_pitchTracker.ProcessBuffer(pcmData);
		}

		#endregion
	}
}
