using System;

namespace ControlWork2
{
	enum CommandType
	{
		If,
		Inc,
		Dec,
		Print,
		Move,
		Call
	}

	struct Command
	{
		public int Number;
		public CommandType CommandType;
		public string arg1;
		public string arg2;
		public string arg3;

		public override string ToString()
		{
			return String.Format("{0} {1} {2}{3}{4}", 
				Number, CommandType.ToString().ToLowerInvariant(), arg1, 
				(string.IsNullOrEmpty(arg2) ? "" : " " + arg2), (string.IsNullOrEmpty(arg3) ? "" : " " + arg3));
		}
	}
}
