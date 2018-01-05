using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvantSound
{
	public class VolumeSample : NormalizedSample
	{
		public readonly double AvgVolume;

		public static short GetAvgVolume(short[] Sample)
		{
			int Sum = 0;
			Array.ForEach(Sample, A => Sum += Math.Abs(A));
			return (short)(Sum / Sample.Length);
		}

		public static short GetMaxVolume(short[] Sample)
		{
			return Sample.Max();
		}

		public VolumeSample(NormalizedSample Sample, double AvgVolume) : base(Sample.Time, Sample.Duration, Sample.Data)
		{
			this.AvgVolume = AvgVolume;
		}

		public static VolumeSample[] Split(NormalizedSample Sample, double MinVolume, double MaxVolume)
		{
			List<VolumeSample> Result = new List<VolumeSample>();

			double VolumeSum;
			double[] SampleData = Sample.Data;
			int StartInd;
			for (int i = 0; i < SampleData.Length; i++)
				if (Math.Abs(SampleData[i]) >= MinVolume && Math.Abs(SampleData[i]) <= MaxVolume)
				{
					StartInd = i;
					VolumeSum = 0;
					do
					{
						VolumeSum += Math.Abs(SampleData[i]);
						i++;						
					}
					while (i < SampleData.Length && Math.Abs(SampleData[i]) >= MinVolume && Math.Abs(SampleData[i]) <= MaxVolume);

					long StartTime = (long)Math.Round(Sample.Time + (double)StartInd / SampleData.Length * Sample.Duration);
					long Duration = (long)Math.Round((double)(i - StartInd) / SampleData.Length * Sample.Duration);
					double[] Temp = new double[i - StartInd];
					Array.Copy(SampleData, StartInd, Temp, 0, i - StartInd);
					Result.Add(new VolumeSample(new NormalizedSample(StartTime, Duration, Temp), VolumeSum / (i - StartTime)));
				}
				else
				{
					StartInd = i;
					VolumeSum = 0;
					do
					{
						VolumeSum += Math.Abs(SampleData[i]);
						i++;
					}
					while (i < SampleData.Length && Math.Abs(SampleData[i]) < MinVolume && Math.Abs(SampleData[i]) > MaxVolume);

					long StartTime = (long)Math.Round(Sample.Time + (double)StartInd / SampleData.Length * Sample.Duration);
					long Duration = (long)Math.Round((double)(i - StartInd) / SampleData.Length * Sample.Duration);
					double[] Temp = new double[i - StartInd];
					Array.Copy(SampleData, StartInd, Temp, 0, i - StartInd);
					Result.Add(new VolumeSample(new NormalizedSample(StartTime, Duration, Temp), VolumeSum / (i - StartTime)));
				}

			return Result.ToArray();
		}
	}
}
