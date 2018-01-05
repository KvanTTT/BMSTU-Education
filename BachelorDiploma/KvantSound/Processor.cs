using System;

namespace KvantSound
{
	public abstract class Processor
	{
		public bool ParallelCalculs
		{
			get;
			set;
		}

		internal abstract void Process(Sample Sample);
	}
}
