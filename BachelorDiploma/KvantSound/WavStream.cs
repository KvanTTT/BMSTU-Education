using System.IO;

namespace KvantSound
{
	public class WavStream
	{
		public readonly WavFormat Format;
		public readonly MemoryStream Stream;
		
		public WavStream(WavFormat Format, int InitStreamSize = 4096)
		{
			this.Format = Format;
			Stream = new MemoryStream(InitStreamSize);
		}

		public WavStream(int InitStreamSize = 4096) : this(WavFormat.Default, InitStreamSize)
		{
		}

		public void AddBytes(byte[] Raw)
		{
			Stream.Write(Raw, 0, Raw.Length);
		}
	}
}
