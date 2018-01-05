using System;

namespace KvantSound
{
	public class HFCNormalizer : Normalizer
	{
		public HFCNormalizer(double CompressParameter, double InThreshold, double OutThreshold) : 
			base(CompressParameter, InThreshold, OutThreshold)
		{

		}
	}
}
