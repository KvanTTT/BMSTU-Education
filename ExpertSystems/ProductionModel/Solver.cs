using System;

namespace ProductModel
{
    enum SolveMethod { IntoDepth, IntoWidth }

    class MyTreeNode
    {
        public Expression Value { get; set; }
        public MyTreeNode Left = null, Right = null;
        public MyTreeNode SelfWay = null;

        public MyTreeNode(Expression Value)
        {
            this.Value = Value;
        }

        public MyTreeNode(Expression Value, MyTreeNode Left, MyTreeNode Right)
        {
            this.Value = Value;
            this.Left = Left;
            this.Right = Right;
        }

        public MyTreeNode this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Left;
                    case 1: return SelfWay;
                    case 2: return Right;
                    default: throw new Exception("Индекс находится за пределами диапазона");
                };
            }
        }
    }

    static class Solver
    {
        static int MaxStepCount = 30;
        static int StepCount;

        private static Fact TryFactBase(ExpressionBase FactBase, Fact Goal)
        {
            return (Fact)FactBase.FindExpression((fact) => fact.IsEqualTo(Goal));
        }

        private static bool TryThenExpression(Fact ThenExpression, Fact Goal)
        {
            return ThenExpression.IsEqualTo(Goal);
        }

        private static Rule TryRuleBase(ExpressionBase RuleBase, Fact Goal)
        {
            return (Rule)RuleBase.FindExpression((rule) => TryThenExpression((Fact)((Rule)rule).ThenExpression, Goal));
        }

        private static void VeryfyRules(ExpressionBase RuleBase, SolveMethod Method)
        {
            switch (Method)
            {
                case SolveMethod.IntoDepth:
                    foreach (EBCell cell in RuleBase)
                        if ((((Fact)((Rule)cell.Value).ThenExpression).LeftArg.Type == ExpressionType.Fact) ||
                            (((Fact)((Rule)cell.Value).ThenExpression).RightArg.Type == ExpressionType.Fact))
                            throw new Exception("Правая часть правила \"" + cell.Value.ToString() + "\" недопустима для данного метода решения");
                    break;
                case SolveMethod.IntoWidth:
                    foreach (EBCell cell in RuleBase)
                        if ((((Fact)((Rule)cell.Value).IfExpression).LeftArg.Type == ExpressionType.Fact) ||
                            (((Fact)((Rule)cell.Value).IfExpression).RightArg.Type == ExpressionType.Fact))
                            throw new Exception("Левая часть правила \"" + cell.Value.ToString() + "\" недопустима для данного метода решения");
                    break;
            };
        }

        public static MyTreeNode AchieveGoal(KnowledgeBase KB, Fact Goal, SolveMethod Method)
        {
            if (Goal.Name == Library.StandartOperations[0].ToString())
            {
                MyTreeNode TR = AchieveGoal(KB, (Fact)Goal.RightArg, Method);
                MyTreeNode TL = TR != null ? AchieveGoal(KB, (Fact)Goal.LeftArg, Method) : null;
                return TL != null ? new MyTreeNode(Goal, TR, TL) : null;
            }

            if (Goal.Name == Library.StandartOperations[1].ToString())
            {
                MyTreeNode TR = AchieveGoal(KB, (Fact)Goal.RightArg, Method);
                MyTreeNode TL = AchieveGoal(KB, (Fact)Goal.LeftArg, Method);
                return ((TL != null) || (TR != null)) ? new MyTreeNode(Goal, TR, TL) : null;
            }

            VeryfyRules(KB.BaseOfRules, Method);

            StepCount = 0;

            if (Method == SolveMethod.IntoDepth) return SearchIntoDepth(KB, Goal);
            if (Method == SolveMethod.IntoWidth) return SearchIntoWidth(KB, Goal);

            return null;
        }

        private static MyTreeNode SearchIntoDepth(KnowledgeBase KB, Fact Goal)
        {
            if (++StepCount > MaxStepCount) { StepCount--; return null; }

            MyTreeNode Res = new MyTreeNode(Goal);

            if (Goal.LeftArg.Type == ExpressionType.Fact)
                if (Goal.RightArg.Type == ExpressionType.Fact)      // Если искомое выражение составное
                {
                    if (Goal.Name == Library.StandartOperations[0].ToString())
                    {
                        MyTreeNode TR = SearchIntoDepth(KB, (Fact)Goal.RightArg);
                        MyTreeNode TL = TR != null ? SearchIntoDepth(KB, (Fact)Goal.LeftArg) : null;
                        StepCount--;
                        return TL != null ? new MyTreeNode(Goal, TR, TL) : null;
                    }

                    if (Goal.Name == Library.StandartOperations[1].ToString())
                    {
                        MyTreeNode TR = SearchIntoDepth(KB, (Fact)Goal.RightArg);
                        MyTreeNode TL = SearchIntoDepth(KB, (Fact)Goal.LeftArg);
                        StepCount--;
                        return ((TL != null) || (TR != null)) ? new MyTreeNode(Goal, TR, TL) : null;                       
                    }
                    Res.Left = SearchIntoDepth(KB, (Fact)Goal.LeftArg);
                    Res.Right = SearchIntoDepth(KB, (Fact)Goal.RightArg);
                }
                else throw new Exception("Невозможно осуществить операцию \"" + Goal.Name + "\" между фактом и не-фактом");
            else
                if (Goal.RightArg.Type == ExpressionType.Fact)
                    throw new Exception("Невозможно осуществить операцию \"" + Goal.Name + "\" между фактом и не-фактом");
                else
                {                               // Если искомое выражение - простой факт
                    Res.SelfWay = new MyTreeNode(TryFactBase(KB.BaseOfFacts, Goal));
                    if (Res.SelfWay.Value == null)
                    {                        
                        foreach (EBCell cell in KB.BaseOfRules)
                        {
                            Rule rul = (Rule)cell.Value;
                            if (rul.ThenExpression.IsEqualTo(Goal))
                            {
                                Res.SelfWay = SearchIntoDepth(KB, (Fact)rul.IfExpression);
                                if (Res.SelfWay == null) continue;
                            }
                        }
                    }
                }

            StepCount--;
            if (Res.SelfWay == null)
                if ((Res.Left == null) && (Res.Right == null)) return null;
                else ;
            else
                if (Res.SelfWay.Value == null) return null;
            
            return Res;
        }

        private static MyTreeNode SearchIntoWidth(KnowledgeBase KB, Fact Goal)
        {
            MyTreeNode Res = null;
            foreach (EBCell fcell in KB.BaseOfFacts)
            {
                Fact fct = (Fact)fcell.Value;
                Res = FactWay(KB, fct, Goal);
                if (Res != null) break;
            }

            return Res;
        }

        private static MyTreeNode FactWay(KnowledgeBase KB, Fact fct, Fact Goal)
        {
            if (++StepCount > MaxStepCount) { StepCount--; return null; }

            MyTreeNode Res = new MyTreeNode(fct);

            if ((fct.Name == Library.StandartOperations[0].ToString()) || (fct.Name == Library.StandartOperations[1].ToString()))
            {
                Res.Left = FactWay(KB, (Fact)fct.LeftArg, Goal);
                Res.Right = FactWay(KB, (Fact)fct.RightArg, Goal);
                if ((Res.Left == null) && (Res.Right == null)) Res = null;
            }
            else
                if (fct.IsEqualTo(Goal)) Res.SelfWay = new MyTreeNode(Goal);
                else
                {
                    foreach (EBCell rcell in KB.BaseOfRules)
                    {
                        Rule rul = (Rule)rcell.Value;
                        if (rul.IfExpression.IsEqualTo(fct)) Res.SelfWay = FactWay(KB, (Fact)rul.ThenExpression, Goal);
                    }
                    if (Res.SelfWay == null) Res = null;
                }
            
            StepCount--;

            return Res;
        }
    }
}
