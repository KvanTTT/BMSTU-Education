using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProductModel
{
    static class Translator
    {
        private static bool TryPriory(char o1, char o2)  // true - приоритет o1 выше
        {
            int i = 0, j = 0;
            for (; o1 != Library.StandartOperations[i]; i++) ;
            for (; o2 != Library.StandartOperations[j]; j++) ;
            return i <= j;
        }

        private static ExpressionType CheckType(string Arg)
        {
            int Len = Arg.Length;

            return Regex.Match(Arg, Library.TermPattern).Length == Len ? ExpressionType.Term :
                Regex.Match(Arg, Library.VariablePattern).Length == Len ? ExpressionType.Variable :
                Regex.Match(Arg, Library.FactPattern).Length == Len ? ExpressionType.Fact :
                ExpressionType.Unknown;
        }

        private static string DeleteOuterBrackets(string Str)
        {
            if (Str[0] == '(')
                return Str.Substring(1, Str[Str.Length - 1] == ')' ? Str.Length - 2 : Str.Length - 1);
            else
                return Str;
        }

        private static string PutBrackets(string Str)
        {
            string[] OrArgs = Str.Split(Library.StandartOperations[1]);
            string[] AndArgs;

            for (int i = 0; i < OrArgs.Length; ++i)
            {
                AndArgs = OrArgs[i].Split(Library.StandartOperations[0]);
                for (int j = 0; j < AndArgs.Length - 1; ++j)
                    AndArgs[j + 1] = "(" + AndArgs[j].Trim() + " & " + AndArgs[j + 1].Trim() + ")";
                if (i < OrArgs.Length)
                    if (i == 0)
                        OrArgs[i] = AndArgs[AndArgs.Length - 1];
                    else
                        OrArgs[i] = "(" + OrArgs[i - 1] + " | " + AndArgs[AndArgs.Length - 1] + ")";
            }

            string Res = DeleteOuterBrackets(OrArgs[OrArgs.Length - 1]);
            return Res;
        }

        public static Fact StringToFact(string Str)
        {
            Match Expr = Regex.Match(Str, Library.FactPattern);

            string Arg1 = DeleteOuterBrackets(Expr.Groups[1].Value.Trim());
            string Arg2 = DeleteOuterBrackets(Expr.Groups[3].Value.Trim());

            return new Fact(Expr.Groups[2].Value, CheckType(Arg1) == ExpressionType.Term ? (Expression)new Term(Arg1) : 
                                                  StringToFact(Arg1),
                                                  CheckType(Arg2) == ExpressionType.Term ? (Expression)new Term(Arg2) :
                                                  StringToFact(Arg2));
        }

        public static Fact StringToRuleExpression(string Arg)
        {
            string Str = Arg.Substring(0);
            string Arg1 = null, Arg2 = null, Operation = null;
            bool fl = false;

            foreach (char ch in Library.StandartOperations)
                if (Str.Contains(ch.ToString()))
                { 
                    fl = true;
                    break;
                }

            if (!fl)
            {
                Match Expr = Regex.Match(Str, Library.FactPattern);

                Arg1 = DeleteOuterBrackets(Expr.Groups[1].Value.Trim());
                Arg2 = DeleteOuterBrackets(Expr.Groups[3].Value.Trim());
                Operation = Expr.Groups[2].Value;
                return new Fact(Operation, CheckType(Arg1) == ExpressionType.Term ? (Expression)new Term(Arg1) :
                                                      CheckType(Arg1) == ExpressionType.Variable ? (Expression)new Variable(Arg1) :
                                                      StringToFact(Arg1),
                                                      CheckType(Arg2) == ExpressionType.Term ? (Expression)new Term(Arg2) :
                                                      CheckType(Arg2) == ExpressionType.Variable ? (Expression)new Variable(Arg2) :
                                                      StringToFact(Arg2));
            }
            else
                return FactFromPN(ToPolishNotation(Arg));  
        }

        private static List<string> ToPolishNotation(string Arg)
        {
            List<string> Result = new List<string>();

            char ch, test;
            string str = "";
            Stack<char> stk = new Stack<char>();
            bool fl;
            for (int i = 0; i < Arg.Length; i++)
            {
                ch = Arg[i];
                if (ch == '(') stk.Push(ch);
                else
                    if (ch == ')')
                    {
                        fl = true;
                        do
                        {
                            test = stk.Pop();
                            if (Library.StandartOperations.Contains(test)) { Result.Add(test.ToString()); fl = false; }
                            else if (test != '(') str += str;
                        } 
                        while (test != '(');
                        if (fl) Result.Add(str.Trim());
                        str = "";
                    }
                    else
                        if (Library.StandartOperations.Contains(ch))
                        {
                            while ((stk.Count != 0) && (Library.StandartOperations.Contains(stk.Peek()) ? TryPriory(stk.Peek(), ch) : true))
                            {
                                if (stk.Peek() == '(') break;
                                Result.Add(stk.Pop().ToString());
                            }
                            stk.Push(ch);
                        }
                        else str += ch;
            }

            while (stk.Count != 0) Result.Add(stk.Pop().ToString());

            return Result;
        }

        private static Fact FactFromPN(List<string> args)
        {
            Stack<Fact> Res = new Stack<Fact>();
            Fact T1, T2;
            bool fl;
            string cur;
            int i = 0;
            while (i < args.Count())
            {
                fl = false;
                cur = args[i++];
                foreach (char ch in Library.StandartOperations)
                    if (cur.LastIndexOf(ch) >= 0) { fl = true; break; }

                if (!fl) Res.Push(StringToFact(cur));
                else
                {
                    T2 = Res.Pop(); T1 = Res.Pop();
                    Res.Push(new Fact(cur.Trim(), T1, T2));
                }
            }

            return Res.Pop();
        }

        public static Rule StringsToRule(string IfString, string ThenString)
        {
            return new Rule(StringToRuleExpression(IfString), StringToRuleExpression(ThenString));
        }
    }
}
