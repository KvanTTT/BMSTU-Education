using System;
using System.Numerics;

namespace KvantSound
{
	public class Sample
	{
		#region Properties

		#region Capturing

		public WavFormat Format
		{
			get;
			private set;
		}

		public byte[] Data
		{
			get;
			internal set;
		}

		public long Time
		{
			get;
			internal set;
		}

		public long Duration
		{
			get;
			internal set;
		}

		public int Number
		{
			get;
			internal set;
		}

		#endregion

		#region Preprocessing

		public short[] PreparedData
		{
			get;
			internal set;
		}

		#endregion

		#region Splitting

		public int OrigIndex
		{
			get;
			internal set;
		}

		public int OrigLength
		{
			get;
			internal set;
		}

		public long OverlappedTime
		{
			get;
			internal set;
		}

		public long OverlappedDuration
		{
			get;
			internal set;
		}

		#endregion

		#region Equalization

		public double[] EqualizedData
		{
			get;
			internal set;
		}

		public double Volume
		{
			get;
			internal set;
		}

		public double MaxVolume
		{
			get;
			internal set;
		}

		public double MinFreq
		{
			get;
			internal set;
		}

		public double MaxFreq
		{
			get;
			internal set;
		}

		public bool Silent
		{
			get;
			internal set;
		}

		#endregion

		#region Spectrogramming

		public Complex[] Spectrogram
		{
			get;
			internal set;
		}

		#endregion

		#region FundFreq Detecting

		public double FundamentalFreq
		{
			get;
			internal set;
		}

		#endregion	  

		#region Pitch Detecting

		public int ID
		{
			get;
			internal set;
		}

		#endregion

		#endregion

		#region Constructors

		public Sample()
		{
		}

		public Sample(Sample Sample) : this(Sample.Format, Sample.Number, Sample.Time,
			Sample.Duration, Sample.Data)
		{
		}

		public Sample(WavFormat Format, int Number, long Time, long Duration, byte[] Data = null)
		{
			this.Format = Format;
			this.Number = Number;
			this.Time = Time;
			this.Duration = Duration;
			this.Data = Data;
		}

		#endregion		
	}
}
