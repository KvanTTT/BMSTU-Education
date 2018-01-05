using System.Collections.Generic;

public class Trinity
{
    public string Atribute,Object,Value;
}

public class Rule
{
    public List<Trinity> ListOfAntecendent;
    public Trinity Consequent;

    public Rule()
    {
        ListOfAntecendent = new List<Trinity>();
    }
}

public class TPMException : System.Exception
{
}