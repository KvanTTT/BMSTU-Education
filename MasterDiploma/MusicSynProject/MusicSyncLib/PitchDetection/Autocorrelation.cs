using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicSyncLib
{
	public class AutocorrelationDetector : PitchDetector
	{
		public bool Cyclic
		{
			get;
			set;
		}

		public AutocorrelationDetector(float sampleRate, bool parallelization, bool cyclic = false)
			: base(sampleRate, parallelization)
		{
			Cyclic = cyclic;
		}

		public override float Detect(float[] pcm, float[] fft)
		{
			float[] autocorrelation = Autocorrelation(pcm);
			int[] indexesOfMax = IndexesOfMax(autocorrelation);

			if (indexesOfMax.Length >= 2)
				return SamplingRate / indexesOfMax[1];
			else
				return 0;
		}

		#region Private

		private float[] Autocorrelation(float[] x)
		{
			float[] result = new float[(x.Length + 1) / 2];

			if (Parallelization)
			{
				Parallel.For(0, result.Length, i =>
				{
					result[i] = AutocorrelationPart(x, i, Cyclic);
				});
			}
			else
			{
				for (int i = 0; i < result.Length; i++)
					result[i] = AutocorrelationPart(x, i, Cyclic);
			}

			return result;
		}

		private static float AutocorrelationPart(float[] pcm, int i, bool cyclic)
		{
			float sum = 0;
			if (cyclic)
			{
				for (int j = 0; j < pcm.Length; j++)
					sum += pcm[j] * pcm[(j + i) % pcm.Length];
			}
			else
			{
				for (int j = 0; j < pcm.Length - i; j++)
					sum += pcm[j] * pcm[j + i];
			}
			return sum;
		}

		private static int[] IndexesOfMax(float[] x, int indCount = 2)
		{
			var result = new List<int>();

			if (x[0] >= x[1])
			{
				result.Add(0);
				if (result.Count == indCount)
					return result.ToArray();
			}
			for (int i = 1; i < x.Length - 1; i++)
				if (x[i] > x[i - 1] && x[i] >= x[i + 1])
				{
					result.Add(i);
					if (result.Count == indCount)
						return result.ToArray();
				}
			if (x[x.Length - 1] > x[x.Length - 2])
			{
				result.Add(x.Length - 1);
				if (result.Count == indCount)
					return result.ToArray();
			}

			return result.ToArray();
		}

		#endregion
	}
}
