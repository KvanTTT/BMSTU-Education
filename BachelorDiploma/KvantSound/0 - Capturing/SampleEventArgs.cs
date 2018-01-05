using System;

namespace KvantSound
{
	public class SampleEventArgs : EventArgs
	{
		public readonly Sample Sample;

		public SampleEventArgs(Sample Sample)
		{
			this.Sample = Sample;
		}
	}
}
