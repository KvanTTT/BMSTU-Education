using System;

namespace MusicSyncLib
{
	public class MaximumLikehoodDetector : PitchDetector
	{
		public int PeaksCount
		{
			get;
			set;
		}

		public float MinFreq
		{
			get;
			set;
		}

		public float MaxFreq
		{
			get;
			set;
		}

		public MaximumLikehoodDetector(int sampleRate, bool parallelization = true, int peaksCount = 5,
			float minFreq = 50, float maxFreq = 1500)
			: base(sampleRate, parallelization)
		{
			PeaksCount = peaksCount;
			MinFreq = minFreq;
			MaxFreq = maxFreq;
		}

		public override float Detect(float[] pcm, float[] fft)
		{
			int usefullMinSpectr = Math.Max(0,
				(int)(MinFreq * fft.Length / SamplingRate));
			int usefullMaxSpectr = Math.Min(fft.Length,
				(int)(MaxFreq * fft.Length / SamplingRate) + 1);

			int[] peakIndices = FindPeaks(fft, usefullMinSpectr, usefullMaxSpectr - usefullMinSpectr,
				PeaksCount);

			if (Array.IndexOf(peakIndices, usefullMinSpectr) >= 0)
			{
				return 0;
			}

			const int verifyFragmentOffset = 0;

			int verifyFragmentLength = (int)(SamplingRate / MinFreq);

			double minPeakValue = Double.PositiveInfinity;
			int minPeakIndex = 0;
			int minOptimalInterval = 0;
			for (int i = 0; i < peakIndices.Length; i++)
			{
				int index = peakIndices[i];
				int binIntervalStart = fft.Length / (index + 1);
				int binIntervalEnd = fft.Length / index;
				int interval;
				double peakValue;

				ScanSignalIntervals(pcm, verifyFragmentOffset, verifyFragmentLength,
					binIntervalStart, binIntervalEnd, out interval, out peakValue);

				if (peakValue < minPeakValue)
				{
					minPeakValue = peakValue;
					minPeakIndex = index;
					minOptimalInterval = interval;
				}
			}

			return (float)SamplingRate / minOptimalInterval;
		}

		#region Private Methods

		private static void ScanSignalIntervals(float[] x, int index, int length,
			int intervalMin, int intervalMax, out int optimalInterval, out double optimalValue)
		{
			optimalValue = Double.PositiveInfinity;
			optimalInterval = 0;

			const int MaxAmountOfSteps = 30;
			int steps = intervalMax - intervalMin;
			if (steps > MaxAmountOfSteps)
				steps = MaxAmountOfSteps;
			else if (steps <= 0)
				steps = 1;

			try
			{
				for (int i = 0; i < steps; i++)
				{
					int interval = intervalMin + (intervalMax - intervalMin) * i / steps;

					double sum = 0;
					for (int j = 0; j < length; j++)
					{
						double diff = x[index + j] - x[index + j + interval];
						sum += diff * diff;
					}
					if (optimalValue > sum)
					{
						optimalValue = sum;
						optimalInterval = interval;
					}
				}
			}
			catch
			{

			}
		}

		private static int[] FindPeaks(float[] values, int index, int length, int peaksCount)
		{
			float[] peakValues = new float[peaksCount];
			int[] peakIndices = new int[peaksCount];

			for (int i = 0; i < peaksCount; i++)
			{
				peakIndices[i] = i + index;
				peakValues[i] = values[peakIndices[i]];
			}

			float minStoredPeak = peakValues[0];
			int minIndex = 0;
			for (int i = 1; i < peaksCount; i++)
				if (minStoredPeak > peakValues[i])
				{
					minIndex = i;
					minStoredPeak = peakValues[minIndex];
				}

			for (int i = peaksCount; i < length; i++)
				if (minStoredPeak < values[i + index])
				{
					peakIndices[minIndex] = i + index;
					peakValues[minIndex] = values[peakIndices[minIndex]];

					minStoredPeak = peakValues[minIndex = 0];
					for (int j = 1; j < peaksCount; j++)
						if (minStoredPeak > peakValues[j])
						{
							minIndex = j;
							minStoredPeak = peakValues[minIndex];
						}
				}

			return peakIndices;
		}

		#endregion
	}
}
