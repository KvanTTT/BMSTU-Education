using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;


namespace AdvanceMath
{
    public class Formula : MarshalByRefObject
    {
        public DoubleFunc Calc
        {
            protected set;
            get;
        }

        public string Expression
        {
            protected set;
            get;
        }

        string FormattedExpression;
        string CSharpExpression;
        List<string> MathFuncNames;
        List<string> MathFuncPatterns;

        public Formula()
        {
            PrepareMathFuncNames();
        }

        public Formula(string Expression)
        {
            this.Expression = Expression;
            PrepareMathFuncNames();
            CompilerErrorCollection Errors;
            FromString(out Errors);
        }

        public Formula(string Expression, out CompilerErrorCollection Errors)
        {
            this.Expression = Expression;
            PrepareMathFuncNames();
            FromString(out Errors);
        }


        public void Parse(string Expression, out CompilerErrorCollection Errors)
        {
            this.Expression = Expression;
            FromString(out Errors);
        }

        const string prefix =
        "using System;namespace AdvanceMath{public static class DynFunc{public static double GetValue(double X){return ";

        const string postfix =
        ";}}}";

        void FormatExpression()
        {
            FormattedExpression = Regex.Replace(Expression, @"\s+", "", RegexOptions.ExplicitCapture);
        }

        void ConvertToCSharp()
        {
            CSharpExpression = Expression;
            //MathFuncPatterns.Add(@"[+-*/](\S)+^(\S)+[+-*/$]");
            //MathFuncNames.Add("Math.Power
            for (int i = 0; i < MathFuncNames.Count; i++)
            {
                CSharpExpression = Regex.Replace(CSharpExpression, MathFuncPatterns[i], "Math." + MathFuncNames[i],
                    RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            }
        }

        void PrepareMathFuncNames()
        {
            MathFuncNames = new List<string>(16);
            MathFuncPatterns = new List<string>(16);
            Type MathType = typeof(Math);

            Attribute[] attribs = Attribute.GetCustomAttributes(MathType);
            MethodInfo[] methods = MathType.GetMethods();
            foreach (MethodInfo nextMethod in methods)
            {
                if (attribs != null)
                {
                    if (MathFuncNames.Count == 0)
                    {                        
                        MathFuncNames.Add(nextMethod.Name);
                        MathFuncPatterns.Add(@"\b" + nextMethod.Name);
                    }
                    else
                        if (MathFuncNames[MathFuncNames.Count - 1] != nextMethod.Name)
                        {
                            MathFuncNames.Add(nextMethod.Name);
                            MathFuncPatterns.Add(@"\b" + nextMethod.Name);
                        }
                }
            }
        }


        void FromString(out CompilerErrorCollection Errors)
        {
            FormatExpression();
            ConvertToCSharp();

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
                sb.Append(CSharpExpression);
                sb.Append(postfix);

                results = provider.CompileAssemblyFromSource(options, sb.ToString());
            }


            if (results.Errors.HasErrors)
                Errors = results.Errors;
            else
            {
                Errors = null;

                Type DynFuncType = results.CompiledAssembly.GetType("AdvanceMath.DynFunc");
                MethodInfo methodInfo = DynFuncType.GetMethod("GetValue");

                Calc = (DoubleFunc)Delegate.CreateDelegate(typeof(DoubleFunc), methodInfo);
                
            }
        }

        public Formula Derive()
        {
            //string str = Regex.Replace(Expression, "\\bSin(?<Arg>\\S+)\\b", "X*Cos${Arg}");
            string str = FormattedExpression;
           /* while (str.IndexOf('*') != -1)
                str = Regex.Replace(str, @"(?<Arg1>[^+-(]*)\*(?<Arg2>\S*)", "Mult(${Arg1},${Arg2})");
            while (str.IndexOf('+') != -1)
                str = Regex.Replace(str, @"(?<Arg1>[^+-]*)\+(?<Arg2>\S*)", "Add(${Arg1},${Arg2})");
            while (str.IndexOf('-') != -1)
                str = Regex.Replace(str, @"(?<Arg1>[^+-]*)\-(?<Arg2>\S*)", "Sub(${Arg1},${Arg2})");*/

            
            return new Formula(str);
        }
    }

}
