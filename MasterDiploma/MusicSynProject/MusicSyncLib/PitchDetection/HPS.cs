using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicSyncLib
{
	public class HPS : PitchDetector
	{
		public int DownSamplesCount
		{
			get;
			set;
		}

		public HPS(int sampleRate, bool parallelization, int downSamplesCount = 5)
			: base(sampleRate, parallelization)
		{
			DownSamplesCount = downSamplesCount;
		}

		public override float Detect(float[]pcm, float[] fft)
		{
			float[][] downSamples = new float[DownSamplesCount][];

			for (int i = 0; i < DownSamplesCount; i++)
			{
				int downSampleLength = fft.Length / (i + 2);
				float ratio = (float)downSampleLength / fft.Length;

				downSamples[i] = new float[downSampleLength];
				for (int j = 0; j < fft.Length; j++)
					downSamples[i][(int)(j * ratio)] += fft[j];
				/*for (int j = 0; j < downSamples[i].Length; j++)
					downSamples[i][j] *= ratio;*/
			}

			float[] sampleMult = new float[downSamples[DownSamplesCount - 1].Length];
			float max = 0;
			int indOfMax = 0;
			for (int i = 0; i < sampleMult.Length; i++)
			{
				sampleMult[i] = fft[i];
				for (int j = 0; j < DownSamplesCount; j++)
					sampleMult[i] *= downSamples[j][i];
				if (max < sampleMult[i])
				{
					max = sampleMult[i];
					indOfMax = i;
				}
			}

			return (float)indOfMax / fft.Length * SamplingRate;
		}
	}
}
