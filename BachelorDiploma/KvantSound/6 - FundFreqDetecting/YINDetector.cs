using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvantSound
{
	public class YINDetector : FundFreqDetector
	{
		public double ACF(Sample Sample, int Shift, int Time)
		{
			double Result = 0;
			for (int i = Time; i < Time + Sample.OrigLength; i++)
				Result += Sample.EqualizedData[i] * Sample.EqualizedData[i + Shift];

			return Result;
		}

		public double ACFEnvelope(Sample Sample, int Shift, int Time)
		{
			return Shift < Sample.OrigLength ? ACF(Sample, Shift, Time) * (1 - Shift / Sample.OrigLength) : 0;
		}

		public double DiffFunc(double[] SampleData, int Shift)
		{
			double Result = 0;
			for (int i = 0; i < SampleData.Length / 2; i++)
				Result += (SampleData[i] - SampleData[i + Shift]) *
					(SampleData[i] - SampleData[i + Shift]);
			return Result;
		}

		public double CumulativeMeanNormalizedDiffFunc(double[] SampleData, int Shift)
		{
			if (Shift == 0)
				return 1;
			else
			{
				double Result = 0;
				for (int i = 0; i < Shift; i++)
					Result += DiffFunc(SampleData, i);
				Result /= Shift;
				Result = DiffFunc(SampleData, Shift) / Result;
				return Result;
			}
		}

		public int AbsoluteThreshold(double[] CumulMeanNormDiffFuncArray)
		{
			/*double Threshold = 0.1;
			for (int i = 0; i < CumulMeanNormDiffFuncArray.Length; i++)
				if (CumulMeanNormDiffFuncArray[i] < Threshold)
					return i;*/

			int Result = 0;
			double MinValue = CumulMeanNormDiffFuncArray[0];
			for (int i = 1; i < CumulMeanNormDiffFuncArray.Length; i++)
			{
				if (CumulMeanNormDiffFuncArray[i] < MinValue)
				{
					Result = i;
					MinValue = CumulMeanNormDiffFuncArray[i];
				}
			}
			return Result;
		}

		internal override void Process(Sample Sample)
		{
			double[] Array = new double[Sample.EqualizedData.Length / 2];

			Array[0] = 1;
			for (int i = 1; i < Sample.EqualizedData.Length / 2; i++)
				Array[i] = DiffFunc(Sample.EqualizedData, i);

			int AbsThresholdInd = AbsoluteThreshold(Array);

			Sample.FundamentalFreq = Sample.Format.SamplesPerSecond / (Sample.EqualizedData.Length - AbsThresholdInd);
		}
	}
}
