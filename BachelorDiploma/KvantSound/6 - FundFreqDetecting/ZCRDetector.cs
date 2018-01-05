using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KvantSound
{
	class ZCRDetector : FundFreqDetector
	{
		public override FundFreqDetectionMode Mode
		{
			get { throw new NotImplementedException(); }
		}

		internal override void Process(Sample Sample)
		{
			throw new NotImplementedException();
		}
	}
}
