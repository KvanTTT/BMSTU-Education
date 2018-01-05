using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab1
{
    class ParseRule
    {
        string Input;
        public ParseRule(string Rule)
        {
            Input = Rule;
        }

        OPERATION CheckCorrectness()
        {
            string Pattern1 = @"^(.+)\s*:\s*(.+)(?:\s*&\s*(.+))+\.$";
            string Pattern2 = @"^(.+)\s*:\s*(.+)(?:\s*\|\s*(.+))+\.$";
            string Pattern3 = @"^(.+)\s*:\s*(.+)(?:\s*\|\s*(.+))*\.$";
            if (Regex.IsMatch(Input, Pattern1))
                return OPERATION.AND;
            if (Regex.IsMatch(Input, Pattern2))
                return OPERATION.OR;
            if (Regex.IsMatch(Input, Pattern3))
                return OPERATION.AND;
            return OPERATION.NON_DEF;
        }
        
        public Rule GetRule()
        {
            OPERATION Operation = CheckCorrectness();
            if (Operation == OPERATION.NON_DEF)
                return null;
            string Pattern = @"^(.+\))\s*:\s*(.+)*\.$";
            Match M = Regex.Match(Input, Pattern);

            GroupCollection gc = M.Groups;
            List<string> Result = new List<string>();
            Result.Add(gc[1].Value + ".");

            string Pattern2 = @"^([a-z][a-z|0-9]*\(\s*[a-z|A-Z|0-9|_]+(?:,\s*[a-zA-Z0-9_]+)*\))(?:\s*[&\|]\s*([a-z][a-z|0-9]*\(\s*[a-z|A-Z|0-9|_]+(?:,\s*[a-zA-Z0-9_]+)*\)))*$";
            string Params = gc[2].Value;
            M = Regex.Match(Params, Pattern2);
            gc = M.Groups;
            if (gc[0].Value == "")
                return null;
            foreach (Group Gr in gc)
            {
                if (Gr.Value == Params)
                    continue;
                Result.Add(Gr.Value + ".");
            }

            Fact[] facts;
            facts = new Fact[Result.Count];
            for (int i = 0; i < Result.Count; ++i)
            {
                ParseFact parseFact = new ParseFact(Result[i]);
                if ((facts[i] = parseFact.GetFact()) == null)
                    return null;
            }

            Rule R = new Rule(facts, Operation);
            return R;
        }
    }
}
