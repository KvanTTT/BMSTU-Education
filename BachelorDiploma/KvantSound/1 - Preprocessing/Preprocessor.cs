using System;

namespace KvantSound
{
	public class Preprocessor : Processor
	{
		#region Public Methods
		
		/// <summary>
		/// Process samples from byte array
		/// We process only one channel
		/// </summary>
		/// <param name="SampleData"></param>
		public short[] ConvertToShortArray(WavFormat Format, byte[] SampleData)
		{
			int Increment = Format.Channels * Format.BitsPerSample / 8;

			short[] Result = new short[(SampleData.Length + Increment - 1) / Increment];
			
			if (Format.BitsPerSample == 8)
			{
				for (int i = 0; i < Result.Length; i++)
					Result[i] = SampleData[i * Increment];
			}
			else // if 16
			{
				for (int i = 0; i < Result.Length; i++)
					Result[i] = (short)((SampleData[i * Increment + 1] << 8) + SampleData[i * Increment]);
			}

			return Result;
		}

		#endregion

		#region Processor Implemantation

		internal override void Process(Sample Sample)
		{
			Sample.PreparedData = ConvertToShortArray(Sample.Format, Sample.Data);
		}

		#endregion
	}
}
