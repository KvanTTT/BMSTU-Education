using System;

namespace MusicNotationLib
{
	public class Key : MusicalSymbol
	{
		#region Private fields

		private readonly int fifths;

		#endregion

		#region Properties

		public int Fifths { get { return fifths; } }

		#endregion

		#region Constructor

		public Key(int numberOfFifths)
		{
			type = MusicSymbolType.Key;
			fifths = numberOfFifths;
			if (fifths > 0)
				musicalCharacter = MusicalCharacters.Sharp;
			else if (fifths < 0)
				musicalCharacter = MusicalCharacters.Flat;
			else
				musicalCharacter = " ";
		}

		#endregion

		#region Public methods

		public int StepToAlter(string step)
		{
			int[] alterTable = new int[7];
			int numberOfStepsToAlter = Math.Abs(fifths);
			for (int i = 0; i < numberOfStepsToAlter; i++)
			{
				alterTable[i] += 1;
				if (i == 6)
				{
					i = -1;
					numberOfStepsToAlter -= 6;
				}

			}
			if (fifths > 0)
			{
				if (step == "C") return alterTable[1];
				else if (step == "D") return alterTable[3];
				else if (step == "E") return alterTable[5];
				else if (step == "F") return alterTable[0];
				else if (step == "G") return alterTable[2];
				else if (step == "A") return alterTable[4];
				else if (step == "B") return alterTable[6];
			}
			else if (fifths < 0)
			{
				if (step == "C") return alterTable[5] * -1;
				else if (step == "D") return alterTable[3] * -1;
				else if (step == "E") return alterTable[1] * -1;
				else if (step == "F") return alterTable[6] * -1;
				else if (step == "G") return alterTable[4] * -1;
				else if (step == "A") return alterTable[2] * -1;
				else if (step == "B") return alterTable[0] * -1;
			}

			return 0;
		}

		#endregion

	}
}
