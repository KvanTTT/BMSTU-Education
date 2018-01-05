using System;
using System.Drawing;
using System.Collections.Generic;

public class Car
{
    public double X
    {
        get;
        set;
    }

    public double Y
    {
        get;
        set;
    }

    List<Car> Cars;

    //double Constants.LinesDistance;

    public int Speed
    {
        get;
        private set;
    }

    public Color Color;

    public Car(int MinSpeed, int MaxSpeed, int WidthOfRoad, List<Car>Cars, Random Generator)
    {
       // Constants.LinesDistance = (double)WidthOfRoad / Constants.LinesDistance;
        X = (Constants.LengthCar+Constants.MinDistance) * (-1);
        Y = Generator.Next(0, (int)Math.Round(WidthOfRoad / Constants.LinesDistance)) * Constants.LinesDistance
            + Constants.LinesDistance / 2;

        while (RoadIsBusy(X, Y, Cars))
        {
            X -= (Constants.LengthCar*2 + Constants.MinDistance);
        }

        Speed = Generator.Next(MinSpeed, MaxSpeed);

        Color = Color.FromArgb(Generator.Next(0, 255), Generator.Next(0, 255), Generator.Next(0, 255));

        this.Cars = Cars;
    }


    private bool RoadIsBusy(double X, double Y, List<Car> Cars)
    {
        foreach (Car Car in Cars)
        {
            if ((Y == Car.Y) && (X + Constants.LengthCar > Car.X - Constants.LengthCar - Constants.MinDistance))
            {
                return true;
            }
        }
        return false;
    }

    public Vector GiveSpeed(int MinSpeed, int MaxSpeed, List<Car> Cars,
        TFuzzySet SpeedSet, TFuzzySet DistanceSet, List<Rule>Rules)
    {
        double NormalizedDist;

        // Определяем расстояние до ближайшего автомобиля
        double Distance = DistanceBeforeNextCar();

        // Преобразуем расстояние
        if (Distance == int.MaxValue)
            NormalizedDist = 90.0;
        else
        {
            NormalizedDist = NormalizeDistance(Distance);
            if (NormalizedDist > 90.0) 
                NormalizedDist = 90.0; 
        }

        // Фаззифицируем расстояние
        Parameter[] Parameters = DistanceSet.Fuzzy(NormalizedDist);

        // Выводим скорость по правилам
        OutWithRules(Rules, Parameters);

        // Получаем скорость
        double NormalizedSpeed = SpeedSet.Defuzzy(Parameters);

        // Преобразуем скорость
        double Result = UnnormalizedSpeed(NormalizedSpeed,MinSpeed,MaxSpeed);

        if (Result > Speed) 
            Result = Speed;

        Reorganize();

        // Возвращаем скорость
        return new Vector(Result, 0);
    }

    private void OutWithRules(List<Rule> Rules, Parameter[] Parameters)
    {
        foreach (Parameter Parameter in Parameters)
        {
            foreach (Rule Rule in Rules)
            {
                if (Parameter.Term == Rule.ListOfAntecendent[0].Value)
                {
                    Parameter.Term = Rule.Consequent.Value;
                }
            }
        }
    }

    double Distance(Car Car)
    {
        return Math.Abs((Car.X - X)) - (2 * Constants.LengthCar);
    }

   /* public Car FrontCar
    {
        get
        {

        }
    }*/

    private double DistanceBeforeNextCar()
    {
        double Result = double.PositiveInfinity;
        foreach (Car Car in Cars)
        {
            if (Car != this)
            {
                if (Y < Car.Y + Constants.LinesDistance / 2 - 1 && Y > Car.Y - Constants.LinesDistance / 2 + 1)
                {
                    if (X < Car.X)
                    {
                        if (Result > Distance(Car))
                            Result = Distance(Car);
                    }
                }
            }
        }
        return Result;
    }

    private double Reorganize()
    {
        double Result = double.PositiveInfinity;
        foreach (Car Car in Cars)
        {
            if (Car != this)
            {                
                if (Y > Car.Y - Constants.LinesDistance / 2 + 1 && Y < Car.Y + Constants.LinesDistance / 2 - 1)
                {
                    if (DistanceBeforeNextCar() < 20)
                        {
                            if (LeftSideFree())
                                Y -= Constants.LinesDistance;
                            else
                                if (RightSideFree())
                                    Y += Constants.LinesDistance;
                        }
                }
            }
        }
        return Result;
    }

    private bool LeftSideFree()
    {
        if (Y < Constants.LinesDistance)
            return false;
        foreach (Car Car in Cars)
        {
            if (Car != this)
            {
                if (Car.Y > Y - 1.5 * Constants.LinesDistance + 1 && Car.Y < Y - 0.5 * Constants.LinesDistance - 1)
                {
                    if (Distance(Car) <= Constants.LengthCar * 2)
                        return false;
                }
            }
        }
        return true;
    }

    private bool RightSideFree()
    {
        if (Y > Constants.LinesDistance * 4)
            return false;
        foreach (Car Car in Cars)
        {
            if (Car != this)
            {
                if (Car.Y > Y + 0.5 * Constants.LinesDistance + 1 && Car.Y < Y + 1.5 * Constants.LinesDistance - 1)
                {
                    if (Distance(Car) <= Constants.LengthCar * 2)
                        return false;
                }
            }
        }
        return true;
    }

    private double DistanceBeforeNextLeftCar()
    {
        double Result = double.PositiveInfinity;
        foreach (Car Car in Cars)
        {
            if (Car != this)
            {
                if (Y < Car.Y - 0.5 * Constants.LinesDistance - 2 && Y > Car.Y - 1.5 * Constants.LinesDistance + 2)
                {
                    if (X < Car.X)
                    {
                        if (Result > ((Car.X - X) - (2 * Constants.LengthCar)))
                            Result = (Car.X - X) - (2 * Constants.LengthCar);
                    }
                }
            }
        }
        return Result;
    }

    private double DistanceBeforeNextRightCar()
    {
        double Result = double.PositiveInfinity;
        foreach (Car Car in Cars)
        {
            if (Car != this)
            {
                if (Y < Car.Y + 1.5 * Constants.LinesDistance - 2 && Y > Car.Y + 0.5 * Constants.LinesDistance + 2)
                {
                    if (X < Car.X)
                    {
                        if (Result > ((Car.X - X) - (2 * Constants.LengthCar)))
                            Result = (Car.X - X) - (2 * Constants.LengthCar);
                    }
                }
            }
        }
        return Result;
    }

    private double NormalizeDistance(double InDist)
    {
        return ((double)60 / ((double)(Constants.LengthCar)) * (double)InDist);
    }

    private int UnnormalizedSpeed(double Speed, int MinSpeed, int MaxSpeed)
    {
        return (int)(MinSpeed+(Speed/180.0)*((double)MaxSpeed-(double)MinSpeed));
    }
}