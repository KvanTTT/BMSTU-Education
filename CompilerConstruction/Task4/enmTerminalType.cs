namespace Task4
{
	public enum enmTerminalType
	{
		Not,

		// Relation operations
		Equal,

		NotEqual,
		Less,
		LessOrEqual,
		Greater,
		GreaterOrEqual,
		
		// Addition operations
		Plus,
		Minus,
		Or,

		// Signs
		PlusSign,
		MinusSign,

		// Multiplication operations
		Mult,
		Divide,
		Div,
		Mod,
		And,

		// Multivalue
		Const,
		Id,

		//
		LeftBracket,
		RightBracket
	}
}
