using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Logic
    {
        KnowledgeBase KB;
        public int MaxRec;
        public Logic(KnowledgeBase knowledgeBase)
        {
            KB = knowledgeBase;
            MaxRec = 10;
        }

        public bool CheckFact(Fact F)
        {
            LinkedList<Fact> Facts = new LinkedList<Fact>();
            Facts.AddFirst(F);
            return Check(Facts, 0);
        }
        
        private bool Check(LinkedList<Fact> Facts, int Rec)
        {
            if (Facts.Count == 0)
                return true;
            if (Rec > MaxRec)
                return false;
            Fact Checked = Facts.First();
            Facts.RemoveFirst();
            foreach (Fact F in KB.BF)
            {
                Fact Copy = new Fact(Checked);
                if (CompareFacts(Checked, F))
                {
                    if (Check(Facts, Rec + 1))
                        return true;
                    else
                        for (int i = 0; i < Copy.args.Length; ++i)
                        {
                            Checked.args[i].Type = Copy.args[i].Type;
                            Checked.args[i].Value = Copy.args[i].Value;
                        }
                }
                else
                {
                    for (int i = 0; i < Copy.args.Length; ++i)
                    {
                        Checked.args[i].Type = Copy.args[i].Type;
                        Checked.args[i].Value = Copy.args[i].Value;
                    }
                }
            } // Окончание цикла проверки фактов

            foreach (Rule R in KB.BR)
            {
                if (R.BaseFact.Name == Checked.Name)
                    if (R.BaseFact.CountArgs == Checked.CountArgs)
                    {
                        Dictionary<string, Arg> Vars = new Dictionary<string, Arg>();
                        Rule Work = new Rule(R);
                        for (int i = 0; i < Work.BaseFact.CountArgs; ++i)
                        {
                            if (Work.BaseFact.args[i].Type == TYPE_OF_ARG.VARIABLE)
                                Vars.Add(Work.BaseFact.args[i].Value, Work.BaseFact.args[i]);
                        }

                        for (int i = 0; i < Work.proof.Length; ++i)
                        {
                            for (int j = 0; j < Work.proof[i].CountArgs; ++j)
                            {
                                if (Work.proof[i].args[j].Type == TYPE_OF_ARG.VARIABLE)
                                {
                                    Arg Temp;
                                    if (Vars.TryGetValue(Work.proof[i].args[j].Value, out Temp))
                                    {
                                        Work.proof[i].args[j] = Temp;
                                    }
                                    else
                                        Vars.Add(Work.proof[i].args[j].Value, Work.proof[i].args[j]);
                                }
                            }
                        } // было создано правило, в котором все переменные связаны между собой

                        Fact Copy = new Fact(Checked);
                            // Необходимо проверить, не изменились ли аргументы-переменные
                        Fact CopyBase = new Fact(Work.BaseFact);
                        for (int k = 0; k < Work.BaseFact.args.Length; ++k)
                            CopyBase.args[k] = Work.BaseFact.args[k];

                        if (CompareFacts(Checked, Work.BaseFact))
                        {
                                // теперь проверяем, изменились ли аргументы-переменные
                            Vars = new Dictionary<string, Arg>();
                            for (int k = 0; k < CopyBase.args.Length; ++k)
                            {
                                if (CopyBase.args[k] != Work.BaseFact.args[k])
                                {
                                    Vars.Add(CopyBase.args[k].Value, Work.BaseFact.args[k]);                                 
                                }
                            }
                            if (Vars.Count != 0)
                            {
                                for (int i = 0; i < Work.proof.Length; ++i)
                                {
                                    for (int j = 0; j < Work.proof[i].CountArgs; ++j)
                                    {
                                        if (Work.proof[i].args[j].Type == TYPE_OF_ARG.VARIABLE)
                                        {
                                            Arg Temp;
                                            if (Vars.TryGetValue(Work.proof[i].args[j].Value, out Temp))
                                            {
                                                Work.proof[i].args[j] = Temp;
                                            }
                                        }
                                    }
                                }
                            }

                                // конец проверки изменения

                            if (Work.Op == OPERATION.AND)
                            {
                                for (int i = Work.proof.Length - 1; i >= 0; --i)
                                    Facts.AddFirst(Work.proof[i]);
                                if (Check(Facts, Rec + 1))
                                    return true;
                                else
                                {
                                    for (int i = 0; i < Work.proof.Length; ++i)
                                        Facts.RemoveFirst();
                                    for (int i = 0; i < Copy.CountArgs; ++i)
                                    {
                                        Checked.args[i].Type = Copy.args[i].Type;
                                        Checked.args[i].Value = Copy.args[i].Value;
                                    }
                                }
                            } // конец блока в случае операции AND
                            else if (Work.Op == OPERATION.OR)
                            {
                                for (int i = 0; i < Work.proof.Length; ++i)
                                {
                                    Facts.AddFirst(Work.proof[i]);
                                    if (Check(Facts, Rec + 1))
                                        return true;
                                    else
                                    {
                                        Facts.RemoveFirst();
                                        for (int j = 0; j < Copy.CountArgs; ++j)
                                        {
                                            Checked.args[j].Type = Copy.args[j].Type;
                                            Checked.args[j].Value = Copy.args[j].Value;
                                        }
                                    }
                                }
                            } // конец блока в случае операции OR
                        } // else для  блока if (Compare(Checked, Work.BaseFact))
                        else
                        {
                            for (int i = 0; i < Copy.CountArgs; ++i)
                            {
                                Checked.args[i].Type = Copy.args[i].Type;
                                Checked.args[i].Value = Copy.args[i].Value;
                            }
                        }
                    } // окончание сравнения кол-ва аргументов
            } // окончание цикла проверки правил

            Facts.AddFirst(Checked);
            return false;
        }

        private bool CompareFacts(Fact Checked, Fact Have)
        {
            if (Checked.Name != Have.Name)
                return false;

            if (Checked.CountArgs != Have.CountArgs)
                return false;

            for (int i = 0; i < Checked.CountArgs; ++i)
            {
                if (Checked.args[i].IsConst() && Have.args[i].IsConst())
                {
                    if (Checked.args[i].Type != Have.args[i].Type)
                        return false;
                    if (Checked.args[i].Value != Have.args[i].Value)
                        return false;
                }
                else if (Checked.args[i].IsConst() && (Have.args[i].Type == TYPE_OF_ARG.VARIABLE))
                {
                    Have.args[i].Type = Checked.args[i].Type;
                    Have.args[i].Value = Checked.args[i].Value;
                }
                else if ((Checked.args[i].Type == TYPE_OF_ARG.VARIABLE) && Have.args[i].IsConst())
                {
                    Checked.args[i].Type = Have.args[i].Type;
                    Checked.args[i].Value = Have.args[i].Value;
                }
                else if ((Checked.args[i].Type == TYPE_OF_ARG.VARIABLE) && (Have.args[i].Type == TYPE_OF_ARG.VARIABLE))
                {
                    Have.args[i] = Checked.args[i];
                }
            }

            return true;
        }
    }
}
