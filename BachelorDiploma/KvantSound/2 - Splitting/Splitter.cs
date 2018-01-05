using System;

namespace KvantSound
{
	/// <summary>
	/// Splitting big not aligned samples on small aligned samples.
	/// </summary>
	public class Splitter : Processor
	{
		#region Fields

		private int sampleNumber;
		Sample processingSample;

		private int windowSize;
		private int overlappedWindowSize;

		private int overlappedRatioInt;
		private double overlappedRatioFrac;

		private long bufferDuration;
		private long overlappedBufferDuration;
		private long origSampleLugTime; // 

		private int LeftInd;
		private int RightInd;

		private double overlappedRatio = -1;

		private int samplesPerSecond;

		/// <summary>
		///  Buffer for processing and splitting
		/// </summary>
		private short[] buffer;
		private short[][] buffers;

		private int bufferPos;

		#endregion

		#region Delegates

		/// <summary>
		/// Occures when next aligned sample is processed
		/// </summary>
		public ProcessSplittedSample ProcessSplittedSample
		{
			get;
			set;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Window Size without included overlapped samples
		/// </summary>
		public int WindowSize
		{
			get
			{
				return windowSize;
			}
			set
			{
				if (windowSize != value)
				{
					windowSize = value;
					RecalcWindow();
				}
			}
		}

		/// <summary>
		/// Window Duration with included overlapped samples
		/// </summary>
		public long OverlappedWindowDuration
		{
			get
			{
				return overlappedBufferDuration;
			}
			set
			{
				windowSize = (int)Math.Round(value * samplesPerSecond / 10000000.0 / overlappedRatio);
				RecalcWindow();
			}
		}

		public double OverlappedWindowRatio
		{
			get
			{
				return overlappedRatio;
			}
			set
			{
				if (overlappedRatio != value)
				{
					overlappedRatio = value;
					RecalcWindow();
				}
			}
		}

		public int OverlappedWindowSize
		{
			get
			{
				return overlappedWindowSize;
			}
		}

		public int ProcessedSampleCount
		{
			get;
			private set;
		}

		public long GlobalTime
		{
			get;
			private set;
		}

		#endregion

		#region Private Methods

		private void RecalcWindow()
		{
			overlappedRatioInt = (int)Math.Floor(OverlappedWindowRatio + 0.99999999999);
			overlappedRatioFrac = OverlappedWindowRatio - overlappedRatioInt;

			overlappedRatioInt = 0;
			overlappedRatio = 0.5;
			overlappedRatioFrac = 0.5;

			overlappedWindowSize = (int)Math.Round(windowSize * overlappedRatio);
			overlappedBufferDuration = (int)Math.Round(10000000.0 / samplesPerSecond * overlappedWindowSize);

			LeftInd = (int)Math.Round(windowSize * (1 - overlappedRatioFrac));
			RightInd = (int)Math.Round(windowSize * overlappedRatioFrac);

			origSampleLugTime = (int)Math.Round(10000000.0 / samplesPerSecond * LeftInd);

			if (windowSize != 0)
			{
				buffer = new short[windowSize];
				if (OverlappedWindowSize != windowSize)
				{
					buffers = new short[overlappedRatioInt][];
					for (int i = 0; i < buffers.Length; i++)
						buffers[i] = new short[windowSize];
				}
				bufferDuration = (int)Math.Round(10000000.0 / samplesPerSecond * windowSize);
			}
		}

		private void LeftShift()
		{
			for (int i = 0; i < buffers.Length - 1; i++)
				Array.Copy(buffers[i + 1], buffers[i], WindowSize);
			if (buffers.Length > 0)
				Array.Copy(buffer, buffers[buffers.Length - 1], WindowSize);
		}

		private void ProcessBuffer()
		{
			if (WindowSize != 0)
			{
				if (OverlappedWindowSize != WindowSize)
				{
					LeftShift();

					short[] OverlappedBuffer = new short[OverlappedWindowSize];

					int Ind = RightInd;
					Array.Copy(buffers[0], LeftInd, OverlappedBuffer, 0, Ind);
					for (int i = 0; i < overlappedRatioInt; i++)
					{
						Array.Copy(buffers[i], 0, OverlappedBuffer, Ind, windowSize);
						Ind += windowSize;
					}
					Array.Copy(buffers[buffers.Length - 1], 0, OverlappedBuffer, Ind, RightInd);

					processingSample.PreparedData = OverlappedBuffer;
					processingSample.Time = GlobalTime;
					processingSample.Duration = bufferDuration;
					processingSample.OverlappedTime = GlobalTime - origSampleLugTime;
					processingSample.OverlappedDuration = overlappedBufferDuration;
					processingSample.OrigIndex = RightInd;
					processingSample.OrigLength = WindowSize;
				}
				else
				{
					processingSample.PreparedData = buffer;
					processingSample.Time = GlobalTime;
					processingSample.Duration = bufferDuration;
					processingSample.OverlappedTime = GlobalTime;
					processingSample.OverlappedDuration = bufferDuration;
					processingSample.OrigIndex = 0;
					processingSample.OrigLength = WindowSize;
				}
			}

			processingSample.Number = sampleNumber++;
			ProcessSplittedSample(processingSample);
			ProcessedSampleCount++;
			GlobalTime += bufferDuration;
		}

		#endregion

		#region Constructors

		public Splitter()
		{
		}

		public Splitter(ProcessSplittedSample ProcessSplittedSample, int WindowSize = 0, double OverlappedWindowRatio = 0,
			int SamplesPerSecond = 44100)
		{
			windowSize = WindowSize;
			overlappedRatio = OverlappedWindowRatio;
			this.samplesPerSecond = SamplesPerSecond;
			this.ProcessSplittedSample = ProcessSplittedSample;

			RecalcWindow();

			GlobalTime = 0;
		}

		#endregion

		#region Processor Implemantation

		/// <summary>
		/// Split on samples with fixed size and its processing
		/// </summary>
		/// <param name="EventArgs"></param>
		internal override void Process(Sample Sample)
		{
			processingSample = Sample;
			if (WindowSize == 0)
			{
				buffer = Sample.PreparedData;
				ProcessBuffer();
			}
			else
			{
				short[] SampleData = Sample.PreparedData;

				if (Sample.Data.Length + bufferPos == WindowSize - 1)
				{
					Array.Copy(SampleData, 0, buffer, bufferPos, SampleData.Length);
					ProcessBuffer();
					bufferPos = 0;
				}
				else
					if (SampleData.Length + bufferPos < WindowSize - 1)
					{
						Array.Copy(SampleData, 0, buffer, bufferPos, SampleData.Length);
						bufferPos += SampleData.Length;
					}
					else
					{
						int SamplesPos = buffer.Length - bufferPos;

						Array.Copy(SampleData, 0, buffer, bufferPos, SamplesPos);
						ProcessBuffer();
						bufferPos = 0;

						while (SampleData.Length - SamplesPos > buffer.Length)
						{
							Array.Copy(SampleData, SamplesPos, buffer, 0, buffer.Length);
							ProcessBuffer();
							SamplesPos += buffer.Length;
						}

						Array.Copy(SampleData, SamplesPos, buffer, 0, SampleData.Length - SamplesPos);
						bufferPos = SampleData.Length - SamplesPos;
					}
			}
		}

		#endregion
	}
}
