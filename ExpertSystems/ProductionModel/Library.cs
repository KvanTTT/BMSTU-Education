namespace ProductModel
{
    static class Library
    {
        public static char[] StandartOperations = { '&', '|' };

        private static string BracketExprPattern = @"\(.+\)";
        public static string TermPattern = @"[a-zа-я0-9]\w*";
        public static string VariablePattern = @"[A-ZА-Я_]\w*";
        public static string OperationPattern = @"[\w\\\-\+=\*]+";

        private static string ArgPattern = @"(" + BracketExprPattern + @"|" + VariablePattern + @"|" + TermPattern + @")";

        public static string FactPattern = @"\s*" + ArgPattern + @"\s+(" + OperationPattern + @")\s+" + ArgPattern + @"\s*";
    }
}
