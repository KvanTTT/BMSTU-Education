namespace MusicNotationLib
{
	public class TimeSignature : MusicalSymbol
	{
		#region Properties

		public TimeSignatureType SignatureType
		{
			get;
			set;
		}

		public uint NumberOfBeats
		{
			get;
			set;
		}

		public uint TypeOfBeats
		{
			get;
			set;
		}

		#endregion

		#region Constructor

		public TimeSignature()
		{
			type = MusicSymbolType.TimeSignature;
			NumberOfBeats = 4;
			TypeOfBeats = 4;
			SignatureType = TimeSignatureType.Common;
		}

		public TimeSignature(uint beats, uint beatType)
		{
			type = MusicSymbolType.TimeSignature;
			NumberOfBeats = beats;
			TypeOfBeats = beatType;
			SignatureType = TimeSignatureType.Numbers;
		}

		public TimeSignature(TimeSignatureType sType, uint beats, uint beatType)
		{
			type = MusicSymbolType.TimeSignature;
			NumberOfBeats = beats;
			TypeOfBeats = beatType;
			SignatureType = sType;
		}

		#endregion

		public override string ToString()
		{
			return NumberOfBeats + "/" + TypeOfBeats;
		}

		public static implicit operator TimeSignature(string Value)
		{
			string[] Strs = Value.Split(new char[] { '/', '\\', '.' });
			return new TimeSignature(TimeSignatureType.Numbers,
				uint.Parse(Strs[0]), uint.Parse(Strs[1]));
		}
	}
}
