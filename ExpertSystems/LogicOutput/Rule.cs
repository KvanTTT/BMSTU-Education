namespace Lab1
{
    class Rule
    {
        Fact baseFact;
        OPERATION op;
        public Fact[] proof;

        public Rule(Fact Base, OPERATION Oper, Fact[] Proof)
        {
            baseFact = new Fact(Base);
            op = Oper;
            proof = new Fact[Proof.Length];
            for (int i = 0; i < Proof.Length; ++i)
                proof[i] = new Fact(Proof[i]);
        }

        public Rule(Fact[] Facts, OPERATION Oper)
        {
            baseFact = new Fact(Facts[0]);
            op = Oper;
            proof = new Fact[Facts.Length - 1];
            for (int i = 1; i < Facts.Length; ++i)
                proof[i - 1] = new Fact(Facts[i]);
        }

        public Rule(Rule R)
        {
            baseFact = new Fact(R.BaseFact);
            op = R.Op;
            proof = new Fact[R.GetCountProof()];
            for (int i = 0; i < R.GetCountProof(); ++i)
                proof[i] = new Fact(R.GetFact(i));
        }

        public Fact BaseFact
        {
            get
            {
                return baseFact;
            }
        }

        public OPERATION Op
        {
            get
            {
                return op;
            }
        }

        public int GetCountProof()
        {
            return proof.Length;
        }

        public Fact GetFact(int Num)
        {
            return proof[Num];
        }
    }
}
