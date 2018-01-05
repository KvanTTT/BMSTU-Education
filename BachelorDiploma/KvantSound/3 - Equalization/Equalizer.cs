using System;

namespace KvantSound
{
	public class Equalizer : Processor
	{
		#region Private Methods

		private void NormalizeAndVolume(short[] Array, out double[] ResultArray, out double Volume, out double MaxVolume)
		{
			Volume = 0;
			MaxVolume = double.NegativeInfinity;
			ResultArray = new double[Array.Length];

			/*if (ParallelCalculs)
				Parallel.For(0, Array.Length, i =>
				{
					ResultArray[i] = (double)Array[i] / 32768; // Преобразование массива амплитуд
					Volume += Math.Abs(ResultArray[i]);
					if (Math.Abs(ResultArray[i]) > MaxVolume)
						MaxVolume = Math.Abs(ResultArray[i]);
				});
			else*/
			for (int i = 0; i < Array.Length; i++)
			{
				ResultArray[i] = (double)Array[i] / 32768; // Преобразование массива амплитуд
				Volume += Math.Abs(ResultArray[i]);
				if (Math.Abs(ResultArray[i]) > MaxVolume)
					MaxVolume = Math.Abs(ResultArray[i]);
			}
			Volume /= ResultArray.Length;
		}

		#endregion

		#region Properties

		public double MinFreq
		{
			get;
			set;
		}

		public double MaxFreq
		{
			get;
			set;
		}

		public double MinVolume
		{
			get;
			set;
		}

		public double MaxVolume
		{
			get;
			set;
		}

		#endregion

		#region Processor Implemantation

		internal override void Process(Sample Sample)
		{
			double[] Data;
			double Volume, MaxVolume;
			NormalizeAndVolume(Sample.PreparedData, out Data, out Volume, out MaxVolume);
			Sample.EqualizedData = Data;
			Sample.Volume = Volume;
			Sample.MaxVolume = MaxVolume;
			Sample.MinFreq = MinFreq;
			Sample.MaxFreq = MaxFreq;
		}

		#endregion

		#region Constructors

		public Equalizer()
		{
		}

		public Equalizer(double MinFreq, double MaxFreq)
		{
			this.MinFreq = MinFreq;
			this.MaxFreq = MaxFreq;
			this.MinVolume = MinVolume;
			this.MaxVolume = MaxVolume;
		}

		#endregion
	}
}