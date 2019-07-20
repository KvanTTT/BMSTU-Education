using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicSyncLib
{
	public class ZCR : PitchDetector
	{
		public ZCR(float sampleRate, bool parallelization)
			: base(sampleRate, parallelization)
		{
		}

		public override float Detect(float[] pcm, float[] fft)
		{
			int intersectionCount = 0;
			for (int i = 1; i < pcm.Length; i++)
			{
				if (pcm[i - 1] * pcm[i] <= 0)
					intersectionCount++;
			}
			return intersectionCount / 2 * SamplingRate / pcm.Length;
		}
	}
}
