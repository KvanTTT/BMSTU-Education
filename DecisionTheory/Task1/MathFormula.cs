using System;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace Task1
{
	public class MathFormula : MarshalByRefObject
	{
		#region Fields & constants

		const string prefix =
		"using System;" +
		"public static class DynamicFunction{public static double GetValue(double[] X){return ";

		const string postfix =
		";}}";

		List<string> MathFuncNames_;
		List<string> MathFuncPatterns_;

		#endregion

		#region Constructors

		public MathFormula(string expression) : this()
		{
			Expression = expression;
			var formattedExpression = Regex.Replace(expression, @"\s+", "", RegexOptions.ExplicitCapture);
			var cSharpExpression = ConvertToCSharpExpression(formattedExpression);
			Function = CompileFormula(cSharpExpression);
		}

		public MathFormula()
		{
			MathFuncNames_ = new List<string>(16);
			MathFuncPatterns_ = new List<string>(16);
			Type mathType = typeof(Math);

			Attribute[] attribs = Attribute.GetCustomAttributes(mathType);
			MethodInfo[] methods = mathType.GetMethods();
			foreach (MethodInfo nextMethod in methods)
			{
				if (attribs != null)
				{
					if (MathFuncNames_.Count == 0)
					{
						MathFuncNames_.Add(nextMethod.Name);
						MathFuncPatterns_.Add(@"\b" + nextMethod.Name);
					}
					else
						if (MathFuncNames_[MathFuncNames_.Count - 1] != nextMethod.Name)
						{
							MathFuncNames_.Add(nextMethod.Name);
							MathFuncPatterns_.Add(@"\b" + nextMethod.Name);
						}
				}
			}
		}

		#endregion

		#region Properties

		public MultiVarFunction Function
		{
			get;
			protected set;
		}

		public string Expression
		{
			get;
			protected set;
		}

		#endregion

		#region Methods

		protected string ConvertToCSharpExpression(string expression)
		{
			var result = expression;
			for (int i = 0; i < MathFuncNames_.Count; i++)
			{
				result = Regex.Replace(result, MathFuncPatterns_[i], "Math." + MathFuncNames_[i],
					RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
			}
			return result;
		}

		protected MultiVarFunction CompileFormula(string cSharpExpression)
		{
			CompilerResults results = null;
			using (CSharpCodeProvider provider = new CSharpCodeProvider())
			{
				CompilerParameters options = new CompilerParameters();

				options.CompilerOptions += @"/optimize+";
				options.IncludeDebugInformation = false;
				options.GenerateExecutable = false;
				options.GenerateInMemory = true;

				StringBuilder sb = new StringBuilder();
				sb.Append(prefix);
				sb.Append(cSharpExpression);
				sb.Append(postfix);

				results = provider.CompileAssemblyFromSource(options, sb.ToString());
			}

			MultiVarFunction result;
			if (results.Errors.HasErrors)
				result = null;
			else
			{
				Type DynFuncType = results.CompiledAssembly.GetType("DynamicFunction");
				MethodInfo methodInfo = DynFuncType.GetMethod("GetValue");

				result = (MultiVarFunction)Delegate.CreateDelegate(typeof(MultiVarFunction), methodInfo);
			}

			return result;
		}

		public static MultiVarFunction Parse(string expression)
		{
			return new MathFormula(expression).Function;
		}

		#endregion
	}
}
