using System;
using System.Windows.Media;
using System.IO;
using KvantSound;

namespace SilverClient.Audio
{	
	public class AudioRecorder : AudioSink
	{
		public readonly WavFormat Format;

		public bool IgnoreFirstSample
		{
			get;
			set;
		}

		public bool SaveToStream
		{
			get;
			set;
		}

		public AudioCaptureDevice Device
		{
			get;
			protected set;
		}

		public CaptureSource Source
		{
			get;
			protected set;
		}

		public MemoryStream Stream
		{
			get;
			protected set;
		}

		public int SampleNumber
		{
			get;
			protected set;
		}

		public event SampleEventHandler Samples;

		protected override void OnCaptureStarted()
		{
			Stream = new MemoryStream(1024);
		}

		protected override void OnCaptureStopped()
		{			
		}

		protected override void OnFormatChange(AudioFormat audioFormat)
		{
			/*if (audioFormat.WaveFormat != WaveFormatType.Pcm)
				throw new InvalidOperationException("MemoryAudioSink supports only PCM audio format.");

			Format = audioFormat;*/
		}


		protected override void OnSamples(long SampleTime, long SampleDuration, byte[] SampleData)
		{
			if (SaveToStream)
				Stream.Write(SampleData, 0, SampleData.Length);
			if ((SampleTime != 0 || !IgnoreFirstSample) && Samples != null)
			{
				Samples(this, new SampleEventArgs(new Sample(Format, SampleNumber++, SampleTime, SampleDuration, SampleData)));
			}
		}

		
		public AudioRecorder()
		{
			if (!EnsureAudioAccsess())
				throw new Exception("Accsess Denided: Cannot go on without a microphone...");

			Device = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
			var Devices = CaptureDeviceConfiguration.GetAvailableAudioCaptureDevices();
			Device = Devices[0];

			if (Device == null)
				throw new Exception("Cannot go on without a microphone...");

			foreach (AudioFormat F in Device.SupportedFormats)
				if (F.BitsPerSample == 16 && F.Channels == 1 && F.SamplesPerSecond == 44100)
				{
					Device.DesiredFormat = F;
					break;
				}

			Format = new WavFormat(Device.DesiredFormat.SamplesPerSecond, Device.DesiredFormat.BitsPerSample, Device.DesiredFormat.Channels);

			Source = new CaptureSource { AudioCaptureDevice = Device };
			CaptureSource = Source;

			SaveToStream = false;
			IgnoreFirstSample = true;
		}

		public AudioRecorder(int AudioFrameSize = 500)
			: this()
		{
			Device.AudioFrameSize = AudioFrameSize;
		}

		public AudioRecorder(SampleEventHandler SamplesEventHandler, int AudioFrameSize = 500) 
			: this()
		{
			Device.AudioFrameSize = AudioFrameSize;
			Samples += SamplesEventHandler;
		}

		public AudioRecorder(AudioFormat Format, SampleEventHandler SamplesEventHandler, int AudioFrameSize = 500) 
			: this()
		{
			Device.DesiredFormat = Format;
			Device.AudioFrameSize = AudioFrameSize;
			Samples += SamplesEventHandler;
		}

		public void Start()
		{
			Source.Start();
		}

		public void Stop()
		{
			Source.Stop();
		}

		public void SaveToWavStream(MemoryStream Stream)
		{
			WavManager.SavePcmToWav(this.Stream, Stream, Device.DesiredFormat);
		}

		public static bool EnsureAudioAccsess()
		{
			return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
		}

		
	}
}
