using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicSyncLib
{
	public static class AudioUtils
	{
		public static float[] ConvertBuffer(byte[] buffer, int channelCount, int bitsPerSample)
		{
			int inc = channelCount * bitsPerSample / 8;
			float[] result = new float[(buffer.Length + inc - 1) / inc];

			if (bitsPerSample == 8)
			{
				for (int i = 0; i < result.Length; i++)
					result[i] = (float)buffer[i * inc] / 127;
			}
			else if (bitsPerSample == 16)
			{
				for (int i = 0; i < result.Length; i++)
					result[i] = (float)(short)((buffer[i * inc + 1] << 8) + buffer[i * inc]) / short.MaxValue;
			}

			return result;
		}

		public static double DecibelToPower(double db)
		{
			return (double)Math.Pow(10.0, db / 20.0);
		}

		public static double PowerToDecibel(double power)
		{
			return 20.0 * Math.Log10(power);
		}
	}
}
