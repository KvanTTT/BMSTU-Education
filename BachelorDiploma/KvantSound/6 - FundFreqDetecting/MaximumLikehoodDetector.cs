using System;

namespace KvantSound
{
	public class MaximumLikehoodDetector : FundFreqDetector
	{
		public override FundFreqDetectionMode Mode
		{
			get
			{
				return FundFreqDetectionMode.MaximumLikehood;
			}
		}
		
		#region Private Methods

		private static void ScanSignalIntervals(double[] x, int index, int length,
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

		private static int[] FindPeaks(double[] values, int index, int length, int peaksCount)
		{
			double[] peakValues = new double[peaksCount];
			int[] peakIndices = new int[peaksCount];

			for (int i = 0; i < peaksCount; i++)
			{
				peakIndices[i] = i + index;
				peakValues[i] = values[peakIndices[i]];
			}

			double minStoredPeak = peakValues[0];
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

		#region Processor Implementation

		internal override void Process(Sample Sample)
		{
			if (Sample.Silent)
				return;

			double minFreq = Sample.MinFreq;
			double maxFreq = Sample.MaxFreq;
			double sampleRate = Sample.Format.SamplesPerSecond;
			double[] spectr = new double[Sample.Spectrogram.Length];
			for (int i = 0; i < spectr.Length; i++)
				spectr[i] = Spectrometer.Abs2(Sample.Spectrogram[i]);

			int usefullMinSpectr = Math.Max(0,
				(int)(minFreq * spectr.Length / sampleRate));
			int usefullMaxSpectr = Math.Min(spectr.Length,
				(int)(maxFreq * spectr.Length / sampleRate) + 1);
			
			int[] peakIndices = FindPeaks(spectr, usefullMinSpectr, usefullMaxSpectr - usefullMinSpectr,
				PeaksCount);

			if (Array.IndexOf(peakIndices, usefullMinSpectr) >= 0)
			{
				Sample.FundamentalFreq = 0;
				return;
			}

			const int verifyFragmentOffset = 0;

			int verifyFragmentLength = (int)(sampleRate / minFreq);

			double minPeakValue = Double.PositiveInfinity;
			int minPeakIndex = 0;
			int minOptimalInterval = 0;
			for (int i = 0; i < peakIndices.Length; i++)
			{
				int index = peakIndices[i];
				int binIntervalStart = spectr.Length / (index + 1);
				int binIntervalEnd = spectr.Length / index;
				int interval;
				double peakValue;

				ScanSignalIntervals(Sample.EqualizedData, verifyFragmentOffset, verifyFragmentLength,
					binIntervalStart, binIntervalEnd, out interval, out peakValue);

				if (peakValue < minPeakValue)
				{
					minPeakValue = peakValue;
					minPeakIndex = index;
					minOptimalInterval = interval;
				}
			}

			Sample.FundamentalFreq = (double)sampleRate / minOptimalInterval;
		}

		#endregion

		#region Properties

		public int PeaksCount
		{
			get;
			set;
		}

		#endregion

		#region Contructors

		public MaximumLikehoodDetector(int PeaksCount = 5)
		{
			this.PeaksCount = PeaksCount;
		}

		#endregion  
	}
}
