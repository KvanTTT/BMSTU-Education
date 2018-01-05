using System;

namespace MusicNotationLib
{
	public class Barline : MusicalSymbol
	{
		#region Private fields

		private RepeatSignType repeatSign;

		#endregion

		#region Properties

		public RepeatSignType RepeatSign { get { return repeatSign; } set { repeatSign = value; } }

		#endregion

		#region Constructor

		public Barline()
		{
			type = MusicSymbolType.Barline;
			repeatSign = RepeatSignType.None;
		}
		#endregion
	}
}
