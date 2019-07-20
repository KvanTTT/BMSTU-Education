using System;

namespace MusicSyncLib
{
	public class PitchUtils
	{
		public static double CreateSineWave(float[] buffer, int numSamples, float sampleRate,
											float freq, float amplitude, double startAngle,
											float freq2 = 0, float amplitude2 = 0)
		{
			var angleStep = freq / sampleRate * Math.PI * 2.0;
			var angleStep2 = freq2 / sampleRate * Math.PI * 2.0;
			var curAngle = startAngle;
			var curAngle2 = startAngle;

			for (int idx = 0; idx < numSamples; idx++)
			{
				buffer[idx] = (float)Math.Sin(curAngle) * amplitude + (float)Math.Sin(curAngle2) * amplitude2;

				curAngle += angleStep;
				curAngle2 += angleStep2;

				while (curAngle > Math.PI)
					curAngle -= Math.PI * 2.0;
				while (curAngle2 > Math.PI)
					curAngle2 -= Math.PI * 2.0;
			}

			return curAngle;
		}

		public static bool LevelIsAbove(float[] buffer, int len, float level)
		{
			if (buffer == null || buffer.Length == 0)
				return false;

			var endIdx = Math.Min(buffer.Length, len);

			for (int idx = 0; idx < endIdx; idx++)
			{
				if (Math.Abs(buffer[idx]) >= level)
					return true;
			}

			return false;
		}
	}
}
