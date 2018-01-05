using System;

namespace KvantSound
{
	public class AutocorrelationDetector : FundFreqDetector
	{
		public override FundFreqDetectionMode Mode
		{
			get
			{
				return FundFreqDetectionMode.Autocorrelation;
			}
		}

		internal override void Process(Sample Sample)
		{
			throw new NotImplementedException();
		}
	}
}
