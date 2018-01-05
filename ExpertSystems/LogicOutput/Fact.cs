namespace Lab1
{
    class Fact
    {
        string name;
        int count;
        public Arg []args;

        public Fact(string Name, int CountArgs)
        {
            name = Name;
            count = CountArgs;
            args = new Arg[count];
            for (int i = 0; i < count; ++i)
                args[i] = new Arg();
        }

        public Fact(string Name, int CountArgs, Arg[] Arguments) : this(Name, CountArgs)
        {
            for (int i = 0; i < count; ++i)
                args[i] = new Arg(Arguments[i]);
        }

        public Fact(Fact F)
        {
            name = F.Name;
            count = F.CountArgs;
            args = new Arg[count];
            for (int i = 0; i < count; ++i)
                args[i] = new Arg(F.GetArg(i));
        }

        public void AddArg(Arg A, int Num)
        {
            args[Num] = new Arg(A);
        }

        public Arg GetArg(int Num)
        {
            return args[Num];
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int CountArgs
        {
            get
            {
                return count;
            }
        }

    }
}
