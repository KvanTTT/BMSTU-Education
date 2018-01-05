using System;
using System.Collections.Generic;
using System.Drawing;

public class BorderOfTrinity
{
    public int Left, Right;
    public List<int> ListOfDefises;

    public BorderOfTrinity()
    {
        ListOfDefises = new List<int>();
    }
}

public static class Analyzer
{
    public static Rule AnalyzeRule(string Source)
    {
        Rule Result = new Rule();

        Source = Source.Trim();

        if (Source.Length < 7)
            throw new TPMException();

        if (Source.IndexOf("если") != 0)
            throw new TPMException();

        int i;
        i = Source.IndexOf(" то ");
        if (i == -1)
            throw new TPMException();


        string BufferList = Source.Substring(5, i - 5);
        Result.ListOfAntecendent = AnalizeListOfFacts(BufferList);


        BufferList = Source.Substring(i + 4);
        List<Trinity>BufferListItems = AnalizeListOfFacts(BufferList);

        if (BufferListItems.Count!=1)
            throw new TPMException();

        Result.Consequent = BufferListItems.ToArray()[0];

        return Result;
    }

    public static List<Trinity> AnalizeListOfFacts(string Source)
    {
        List<Trinity> Result = new List<Trinity>();
        Trinity BufferTrinity;
        List<BorderOfTrinity>BT = new List<BorderOfTrinity>();

        Source = Source.Trim();
        if (Source.Length == 0) throw new TPMException();

        // Ищем позиции запятых
        int LeftBorder=0;
        BorderOfTrinity SomeBT;
        for (int i=0;i<Source.Length;i++)
        {
            if (Source[i]=='&')
            {
                SomeBT = new BorderOfTrinity();
                SomeBT.Left=LeftBorder;
                SomeBT.Right=i;
                BT.Add(SomeBT);
                LeftBorder=i;

            }
        }

        SomeBT = new BorderOfTrinity();
        SomeBT.Left = LeftBorder;
        SomeBT.Right=Source.Length-1;
        BT.Add(SomeBT);

        foreach(BorderOfTrinity CommaInterval in BT)
        {
            for (int i = CommaInterval.Left; i < CommaInterval.Right; i++)
            {
                if (Source[i] == '-')
                    CommaInterval.ListOfDefises.Add(i);
            }
            if ((CommaInterval.ListOfDefises.Count < 1) || (CommaInterval.ListOfDefises.Count > 2))
                throw new TPMException();
        }

        string BufferTerm;
        int TypeCounter;
        int RightBorder;

        foreach (BorderOfTrinity CommaInterval in BT)
        {
            LeftBorder = CommaInterval.Left;
            TypeCounter = 0;
            BufferTrinity = new Trinity();
            foreach (int CycleParameter in CommaInterval.ListOfDefises)
            {
                RightBorder = CycleParameter;
                

                if ((Source[LeftBorder]=='-')||(Source[LeftBorder]=='&'))
                    LeftBorder++;

                if (!((Source[RightBorder]=='-')||(Source[RightBorder]=='&')))
                    RightBorder--;

                BufferTerm = Source.Substring(LeftBorder, RightBorder - LeftBorder).Trim();
                if (BufferTerm.Length == 0)
                    throw new TPMException();

                if (TypeCounter == 0)
                    BufferTrinity.Atribute = BufferTerm;
                if ((TypeCounter == 1))
                {
                    BufferTrinity.Object =  BufferTerm;
                }


                if ((Source[RightBorder] == '-') || (Source[RightBorder] == '&'))
                    RightBorder++;
                LeftBorder = RightBorder;

                TypeCounter++;
            }

            RightBorder = CommaInterval.Right+1;

            if ((Source[LeftBorder] == '-') || (Source[LeftBorder] == '&'))
                LeftBorder++;

            if (Source[RightBorder-1] == '&')
                RightBorder--;

            BufferTerm = Source.Substring(LeftBorder, RightBorder - LeftBorder).Trim();
            if (BufferTerm.Length == 0)
                throw new TPMException();


            if (TypeCounter == 1)
            {
                BufferTrinity.Value = BufferTerm;
                BufferTrinity.Object = String.Empty;
            }
            if ((TypeCounter == 2))
            {
                BufferTrinity.Value = BufferTerm;
            }

            Result.Add(BufferTrinity);
        }

        return Result;
    }
}