namespace ProductModel
{
    class Rule : Expression
    {
        private const ExpressionType type = ExpressionType.Rule;
        private Expression ifexpression;
        private Expression thenexpression;

        public override ExpressionType Type { get { return type; } }

        public Expression IfExpression { get { return ifexpression; } }
        public Expression ThenExpression { get { return thenexpression; } }

        public override string ToString() { return "Если " + ifexpression.ToString() + " То " + thenexpression.ToString(); }
        public override bool IsEqualTo(Expression Expr)
        {
            return this.ifexpression.IsEqualTo(((Rule)Expr).ifexpression) && this.thenexpression.IsEqualTo(((Rule)Expr).thenexpression);
        }

        public Rule(Expression IfExpr, Expression ThenExpr)
        {
            this.ifexpression = IfExpr;
            this.thenexpression = ThenExpr;
        }
    }
}
