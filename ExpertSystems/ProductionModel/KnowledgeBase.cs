namespace ProductModel
{
    class KnowledgeBase
    {
        public ExpressionBase BaseOfFacts { get; private set; }
        public ExpressionBase BaseOfRules { get; private set; }

        public KnowledgeBase() { BaseOfFacts = new ExpressionBase(); BaseOfRules = new ExpressionBase(); }
        
        public void AddFact(Fact fact) { BaseOfFacts.AddExpression(fact); }
        public void AddRule(Rule rule) { BaseOfRules.AddExpression(rule); }  
      
        public void RemoveFact(int index) { BaseOfFacts.RemoveExpression(index); }
        public void RemoveRule(int index) { BaseOfRules.RemoveExpression(index); }

        public void ClearFacts() { BaseOfFacts.Clear(); }
        public void ClearRules() { BaseOfRules.Clear(); }
        public void Clear() { ClearFacts(); ClearRules(); }
    }
}
