namespace ProductModel
{
    class Variable : Expression
    {
        private const ExpressionType type = ExpressionType.Variable;
        public string Value { get; set; }
        public string Name { get; private set; }

        public override ExpressionType Type { get { return type; } }

        public override string ToString() { return Name; }
        public override bool IsEqualTo(Expression Expr) { return this.Value == ((Variable)Expr).Value; }

        public Variable(string Name) { this.Name = Name; }
        public Variable(string Name, string Value) { this.Name = Name; this.Value = Value; }
    }
}
