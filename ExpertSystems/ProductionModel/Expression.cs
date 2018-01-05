namespace ProductModel
{
    public enum ExpressionType { Term, Variable, Fact, Rule, CompositeFact , Unknown }

    abstract class Expression
    {
        public abstract ExpressionType Type { get; }
        public abstract override string ToString();
        public abstract bool IsEqualTo(Expression Expr);
    }
}
