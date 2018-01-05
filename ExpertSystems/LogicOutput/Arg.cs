namespace Lab1
{
    class Arg
    {
        TYPE_OF_ARG type;
        bool Def;
        string value;

        public Arg()
        {
            type = TYPE_OF_ARG.NON_DEF;
            Def = false;
            value = "";
        }

        public Arg(string Val, TYPE_OF_ARG T)
        {
            type = T;
            Def = true;
            value = Val;
        }

        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public bool IsDefined()
        {
            return Def;
        }

        public TYPE_OF_ARG Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public Arg(Arg A)
        {
            type = A.Type;
            value = A.Value;
            Def = true;
        }

        public bool IsConst()
        {
            if ((type == TYPE_OF_ARG.NUMBER) || (type == TYPE_OF_ARG.SYMBOL))
                return true;
            else
                return false;
        }
    }
}
