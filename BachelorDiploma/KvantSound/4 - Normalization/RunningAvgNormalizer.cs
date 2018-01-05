namespace KvantSound
{
	public class RunningAvgNormalizer : Normalizer
	{
		public RunningAvgNormalizer(double CompressParameter, double InThreshold, double OutThreshold) :
			base(CompressParameter, InThreshold, OutThreshold)
		{
		}
	}
}