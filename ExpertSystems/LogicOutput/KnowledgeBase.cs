using System.Collections.Generic;

namespace Lab1
{
    class KnowledgeBase
    {
        public List<Rule> BR;
        public List<Fact> BF;

        public KnowledgeBase()
        {
            BR = new List<Rule>();
            BF = new List<Fact>();
        }

        public void AddRule(Rule R)
        {
            BR.Add(new Rule(R));
        }

        public void AddFact(Fact F)
        {
            BF.Add(new Fact(F));
        }
    }
}
