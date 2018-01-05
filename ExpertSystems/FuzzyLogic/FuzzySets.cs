using System.Collections.Generic;
using System;

public class TFuzzySet
{
    private List<Graphic> Graphics;

    public TFuzzySet()
    {
        Graphics = new List<Graphic>();
    }

    public void Add(Graphic InGraphic)
    {
        Graphics.Add(InGraphic);
    }

    public Parameter[] Fuzzy(double Value)
    {
        List<Parameter> Result=new List<Parameter>();

        foreach (Graphic Graphic in Graphics)
        {
            Result.Add(new Parameter(Graphic.Term,Graphic.GiveProbability(Value)));
        }
        return Result.ToArray();
    }

    public double Defuzzy(Parameter[] Values)
    {
        double a, b;
        double S=0;
        double Integral=0;

        a = MinLimit();
        b = MaxLimit();
        double CurrentX = a;

        while (CurrentX < b)
        {

            Integral += Max(GiveSetValuesOnGraphics(CurrentX, Values)) * CurrentX;
            S += Max(GiveSetValuesOnGraphics(CurrentX, Values));

            CurrentX += 1;
        }

        if (S == 0)
            return 0;
        else
            return Integral / S;
    }

    private double[] GiveSetValuesOnGraphics(double Value,Parameter[] Values)
    {
        List <double> Result=new List<double>();

        foreach (Graphic Graphic in Graphics)
        {
            Result.Add(Math.Min(Graphic.GetY(Value)[0],GiveUpperRestrict(Graphic.Term,Values)));
        }

        return Result.ToArray();
    }

    private double GiveUpperRestrict(string Term, Parameter[] Values)
    {
        foreach (Parameter Parameter in Values)
        {
            if (Term == Parameter.Term)
                return Parameter.Probability;
        }
        return 0;
    }

    private double MinLimit()
    {
        double Result = Graphics[0].Points[0].X;

        foreach (Graphic Graphic in Graphics)
        {
            Result = Math.Min(Result, Graphic.Points[0].X);
        }
        return Result;
    }

    private double MaxLimit()
    {
        double Result = Graphics[0].Points[Graphics[0].Points.Length-1].X;

        foreach (Graphic Graphic in Graphics)
        {
            Result = Math.Max(Result, Graphic.Points[Graphic.Points.Length - 1].X);
        }
        return Result;
    }

    private double Max(double[]Values)
    {
        double Result = Values[0];

        for (int i = 0; i < Values.Length; i++)
        {
            Result = Math.Max(Result, Values[i]);
        }

        return Result;
    }
}