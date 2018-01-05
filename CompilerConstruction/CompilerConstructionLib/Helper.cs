using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CompilerConstructionLib
{
	public static class Helper
	{
		public static string GetString(this List<Rule> rools)
		{
			var stringBuilder = new StringBuilder();
			rools.ForEach(rool => 
			{
				stringBuilder.Append(rool.ToString());
				stringBuilder.Append("," + Environment.NewLine);
			});
			var l = ("," + Environment.NewLine).Length;
			if (stringBuilder.Length >= l)
				stringBuilder.Remove(stringBuilder.Length - l, l);

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Perform a deep Copy of the object.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}

			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using (stream)
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}

		public const string EmptySymbol = "Ø";

		public const string IdentitySymbol = "λ";

		public const char IterationSymbol = '*';

		public const char NewStartSymbolSuffix = '1';

		public const char Plus = '+';

		public const char Multiply = '∙';

		public const char VarName = 'X';

		public const char LeftBracket = '(';

		public const char RightBracket = ')';

		public const char Arrow = '→';

		public const char Or = '|';
	}
}
