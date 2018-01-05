namespace KvantSound
{
	// Summary:
	//	 Reports the digital signal encoding format of an audio format.
	public enum WaveFormatType
	{
		// Summary:
		//	 Audio format uses pulse code modulation (PCM) encoding.
		Pcm = 1,
	}

	/// <remarks>
	/// Dublicat od AudioFormat in silvelight library
	/// </emarks>
	public class WavFormat
	{
		// Summary:
		//	 Gets the number of bits that are used to store the audio information for
		//	 a single sample of an audio format.
		//
		// Returns:
		//	 The number of bits that are used to store the audio information for a single
		//	 sample of an audio format.
		public readonly int BitsPerSample;

		//
		// Summary:
		//	 Gets the number of channels that are provided by the audio format.
		//
		// Returns:
		//	 The number of channels that are provided by the audio format.

		public readonly int Channels;

		//
		// Summary:
		//	 Gets the number of samples per second that are provided by the audio format.
		//
		// Returns:
		//	 The number of samples per second that are provided by the audio format.
		public readonly int SamplesPerSecond;

		//
		// Summary:
		//	 Gets the encoding format of the audio format as a System.Windows.Media.WaveFormatType
		//	 value.
		//
		// Returns:
		//	 The encoding format of the audio format.
		public WaveFormatType WaveFormat;

		public int Increment
		{
			get
			{
				return Channels * BitsPerSample / 8;
			}
		}

		public WavFormat(int SamplesPerSecond = 44100, int BitsPerSample = 16, int Channels = 1, WaveFormatType WaveFormat = WaveFormatType.Pcm)
		{
			this.SamplesPerSecond = SamplesPerSecond;
			this.BitsPerSample = BitsPerSample;			
			this.Channels = Channels;
			this.WaveFormat = WaveFormat;
		}

		public static WavFormat Default
		{
			get { return new WavFormat(); }
		}

		public short[] ConvertToShortArray(byte[] SampleData)
		{
			int Increment = Channels * BitsPerSample / 8;

			short[] Result = new short[(SampleData.Length + Increment - 1) / Increment];

			if (BitsPerSample == 8)
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


		
		public static double[] ConvertToDoubleArray(short[] Amplitudes)
		{
			double[] Result = new double[Amplitudes.Length];
			double Coef = 1.0 / 32768.0; 
			for (int i = 0; i < Amplitudes.Length; i++)
				Result[i] = Amplitudes[i] * Coef;
			return Result;
		}

		
	}
}
