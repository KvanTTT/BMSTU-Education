namespace MusicSyncLib
{
	public abstract class PitchDetector
	{
		public float SamplingRate
		{
			get;
			protected set;
		}

		public bool Parallelization
		{
			get;
			set;
		}

		public PitchDetector(float sampleRate, bool parallelization = true)
		{
			SamplingRate = sampleRate;
			Parallelization = parallelization;
		}

		public abstract float Detect(float[] pcm, float[] fft);
	}
}
