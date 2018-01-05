namespace ProductModel
{
    class Term : Expression
    {
        private const ExpressionType type = ExpressionType.Term;
        public string Value { get; private set; }

        public override ExpressionType Type { get { return type; } }

        public override string ToString() { return Value; }
        public override bool IsEqualTo(Expression Expr) { return this.Value == ((Term)Expr).Value; }

        public Term(string Value)
        {
            this.Value = Value;
        }
    }
}
