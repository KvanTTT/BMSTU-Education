using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1
{
    class ParseFact
    {
        string Input;
        public ParseFact(string F)
        {
            Input = F;
        }

        bool CheckCorrectness()
        {
            string Pattern = @"^([a-z][a-z|0-9]*)\(\s*([a-z|A-Z|0-9|_]+)(?:,\s*([a-z|A-Z|0-9|_]+))*\)\.$";

            return Regex.IsMatch(Input, Pattern);

        }

        public Fact GetFact()
        {
            if (!CheckCorrectness())
                return null;
            string Pattern = @"^([a-z][a-z|0-9]*)\(\s*([a-z|A-Z|0-9|_]+)(?:,\s*([a-zA-Z0-9_]+))*\)\.$";

            Match M = Regex.Match(Input, Pattern);

            GroupCollection gc = M.Groups;
            List<string> Result = new List<string>();
            foreach (Group Gr in gc)
            {
                if (Gr.Value == Input)
                    continue;
                CaptureCollection C = Gr.Captures;
                foreach (Capture capt in C)
                {
                    Result.Add(capt.Value);
                }
            }

            Arg[] args;
            args = new Arg[Result.Count - 1];
            for (int i = 0; i < Result.Count - 1; ++i)
            {
                TYPE_OF_ARG Type;
                if (Regex.IsMatch(Result[i + 1], @"^[A-Z]"))       
                    Type = TYPE_OF_ARG.VARIABLE;
                else if (Regex.IsMatch(Result[i + 1], @"^[0-9]+$"))
                    Type = TYPE_OF_ARG.NUMBER;
                else
                    Type = TYPE_OF_ARG.SYMBOL;
                args[i] = new Arg(Result[i + 1], Type);
            }
            Fact F = new Fact(Result[0], args.Length, args);
            return F;
        }

 /*       public Fact GetFactOld()
        {
            string Name;
            int NameLength = 0;
            int CountParams = 0;
            List<int> PositionArgs = new List<int>();
            for (int i = 0; i < Input.Length; ++i)
            {
                if (Input[i] == '(')
                {
                    NameLength = i;
                    PositionArgs.Add(i);
                }
                if ((Input[i] == ',') || (Input[i] == ')'))
                {
                    CountParams += 1;
                    PositionArgs.Add(i);
                }
            }

            char[] NameArray = new char[NameLength];

            Input.CopyTo(0, NameArray, 0, NameLength);
            Name = new string(NameArray);
            Arg[] args = new Arg[CountParams];
            for (int i = 1; i <= CountParams; ++i)
            {
                int Dif = PositionArgs[i] - PositionArgs[i - 1] - 2;
                NameArray = new char[Dif];
                Input.CopyTo(PositionArgs[i-1] + 2, NameArray, 0, Dif);
                args[i - 1] = new Arg(new string(NameArray), TYPE_OF_ARG.SYMBOL);
            }

            Fact F = new Fact(Name, CountParams, args);
            return F;
        }*/
    }
}
