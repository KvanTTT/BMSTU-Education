using System;

namespace KvantSound
{
	public class HPSDetector : FundFreqDetector
	{
		public override FundFreqDetectionMode Mode
		{
			get
			{
				return FundFreqDetectionMode.HPS;
			}
		}

		public int DownSamplesCount
		{
			get;
			set;
		}

		internal override void Process(Sample Sample)
		{
			double[] Spectr = new double[Sample.Spectrogram.Length];
			for (int i = 0; i < Spectr.Length; i++)
				Spectr[i] = Spectrometer.Abs2(Sample.Spectrogram[i]);

			double[][] DownSamples = new double[DownSamplesCount][];

			for (int i = 0; i < DownSamplesCount; i++)
			{
				int DownSampleLength = Spectr.Length / (i + 2);
				double Ratio = (double)DownSampleLength / Spectr.Length;

				DownSamples[i] = new double[DownSampleLength];
				for (int j = 0; j < Spectr.Length; j++)
					DownSamples[i][(int)(j * Ratio)] += Spectr[j];
				for (int j = 0; j < DownSamples[i].Length; j++)
					DownSamples[i][j] *= Ratio;
			}

			double[] SampleMult = new double[DownSamples[DownSamplesCount - 1].Length];
			double Max = 0;
			int IndOfMax = 0;
			for (int i = 0; i < SampleMult.Length; i++)
			{
				SampleMult[i] = Spectr[i];
				for (int j = 0; j < DownSamplesCount; j++)
					SampleMult[i] *= DownSamples[j][i];
				if (Max < SampleMult[i])
				{
					Max = SampleMult[i];
					IndOfMax = i;
				}
			}

			Sample.FundamentalFreq = (double)IndOfMax / Spectr.Length * Sample.Format.SamplesPerSecond;
		}

		public HPSDetector(int DownSamplesCount = 5)
		{
			this.DownSamplesCount = DownSamplesCount;
		}
	}
}
