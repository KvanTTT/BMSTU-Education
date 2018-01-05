namespace ProductModel
{
    class Fact : Expression
    {
        private const ExpressionType type = ExpressionType.Fact;
        public string Name { get; private set; }
        private Expression leftarg;
        private Expression rightarg;

        public override ExpressionType Type { get { return type; } }

        public Expression LeftArg { get { return leftarg; } }
        public Expression RightArg { get { return rightarg; } }

        public override string ToString()
        {
            string Res = leftarg.ToString() + ' ' + Name + ' ' + rightarg.ToString();

            if (Name == Library.StandartOperations[0].ToString()) return Res;
            else return '(' + Res + ')';
        }
        public override bool IsEqualTo(Expression Expr)
        { 
            return (this.Name == ((Fact)Expr).Name) && 
                   (this.leftarg.IsEqualTo(((Fact)Expr).leftarg)) &&
                   (this.rightarg.IsEqualTo(((Fact)Expr).rightarg));
        }

        public Fact(string Name, Expression LeftArg, Expression RightArg)
        {
            this.Name = Name;
            this.leftarg = LeftArg;
            this.rightarg = RightArg;
        }

        public Fact(Fact Source)
        {
            this.Name = Source.Name;
            this.leftarg = Source.leftarg;
            this.rightarg = Source.rightarg;
        }
    }
}
