using System;

namespace KvantSound
{
	public abstract class FundFreqDetector : Processor
	{
		public abstract FundFreqDetectionMode Mode
		{
			get;
		}
	}
}
