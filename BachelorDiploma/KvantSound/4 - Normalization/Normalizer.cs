using System;

namespace KvantSound
{
	public class Normalizer : Processor
	{
		#region Processor Implementation

		internal override void Process(Sample Sample)
		{
			double[] CompressedSampleData = new double[Sample.EqualizedData.Length];
			double Norm;
			for (int i = 0; i < Sample.EqualizedData.Length; i++)
			{
				Norm = 1.0 - Math.Pow(1.0 - Math.Abs(Sample.EqualizedData[i]), CompressParameter);
				CompressedSampleData[i] = Norm * Math.Sign(Sample.EqualizedData[i]);
			}
			if (Sample.Number == 0)
				for (int i = 0; i < CompressedSampleData.Length; i++)
					RunningTotal += Math.Abs(CompressedSampleData[i]);
			else
				for (int i = 0; i < CompressedSampleData.Length; i++)
				{
					RunningTotal -= Math.Abs(PreviousCompressedSampleData[i]);
					RunningTotal += Math.Abs(CompressedSampleData[i]);
					/*double MovingAverage = RunningTotal / CompressedSampleData.Length;
					if (MovingAverage < InThreshold)
					{
						int OnsetPoint = i - (WindowLength / 2);
					}
					else
					{
						Sample.Silent = false;
					}*/
				}


			/*double Sum = 0;
			for (int i = 0; i < CompressedSampleData.Length; i++)
				Sum += Math.Abs(CompressedSampleData[i]);
			if (Math.Abs(Sum / CompressedSampleData.Length) < InThreshold)
				Sample.Silent = true;*/

			var Avg = Math.Abs(RunningTotal / CompressedSampleData.Length);

			if (Avg >= InThreshold)
				NoteOn = true;
			else
				if (Avg < OutThreshold)
					NoteOn = false;

			if (!NoteOn)
				Sample.Silent = true;

			PreviousCompressedSampleData = CompressedSampleData;
		}

		#endregion

		#region Constructors

		public Normalizer()
		{
		}

		public Normalizer(double CompressParameter, double InThreshold, double OutThreshold)
		{
			this.CompressParameter = CompressParameter;
			this.InThreshold = InThreshold;
			this.OutThreshold = OutThreshold;
		}
		
		#endregion

		#region Properties

		protected double[] PreviousCompressedSampleData
		{
			get;
			set;
		}

		public double RunningTotal
		{
			get;
			protected set;
		}

		public bool NoteOn
		{
			get;
			protected set;
		}

		public double CompressParameter
		{
			get;
			set;
		}

		public double InThreshold
		{
			get;
			set;
		}

		public double OutThreshold
		{
			get;
			set;
		}

		#endregion
	}
}
