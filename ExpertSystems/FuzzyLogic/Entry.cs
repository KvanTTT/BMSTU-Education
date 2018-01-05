using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System;

public class TLR4
{
    Bitmap CarBitmap = new Bitmap("car.png");

    private int CarInInterval;
    private List<Car> Cars;
    private Timer CarsStreamTimer;
    private int CarsStreamCounter;
    private int CarsStreamToCreateCars;
    private Decimal CarsStreamIntensity;
    private Random Generator;

    private List<Rule>Rules;

    public TFuzzySet SpeedSet;

    public TFuzzySet DistanceSet;

    public TLR4(LR4.Form1 MainForm)
    {
        Cars = new List<Car>();
        CarsStreamTimer = new Timer();
        CarsStreamTimer.Interval = 1;
        CarsStreamCounter = 0;

        CarsStreamToCreateCars=0;
        CarsStreamTimer.Tick += new System.EventHandler(CarsStreamTimer_Tick);
        ReadFromGUI(MainForm);

        Generator = new Random(unchecked((int)(DateTime.Now.Ticks)));

        Rules = new List<Rule>();

    }

    public void StartStream()
    {
        CarsStreamTimer.Enabled = true;
    }

    public void StopStream()
    {
        CarsStreamTimer.Enabled = false;
    }

    public void Step(LR4.Form1 MainForm)
    {
        // Считываем информацию с формы
        ReadFromGUI(MainForm);

        // Проверяем, нужно ли создать новые авто
        if (CarsStreamToCreateCars != 0)
        {
            CreateNewCars((int)MainForm.numericUpDown2.Value,(int)MainForm.numericUpDown3.Value,MainForm.pictureBox1.Height);
        }
        
        // Здесь изменяем модель 


        // Передвигаем машину на дороге
        Act(MainForm.pictureBox1.Width, (int)MainForm.numericUpDown2.Value, (int)MainForm.numericUpDown3.Value);

        MainForm.pictureBox1.Invalidate();

        //Application.DoEvents();
    }

    private void CreateNewCars(int MinSpeed, int MaxSpeed, int WidthOfRoad)
    {
        for (int i = 0; i < CarsStreamToCreateCars; i++)
            Cars.Add(new Car(MinSpeed,MaxSpeed,WidthOfRoad,Cars,Generator));
        CarsStreamToCreateCars = 0;
    }

    private void ReadFromGUI(LR4.Form1 MainForm)
    {
        CarsStreamIntensity = MainForm.numericUpDown1.Value;
    }

    private void Act(int RoadLimit, int MinSpeed, int MaxSpeed)
    {
        foreach (Car Car in Cars)
        {
            Vector Speed = Car.GiveSpeed(MinSpeed,MaxSpeed,Cars,SpeedSet,DistanceSet,Rules);
            Car.X += Speed.X / MinSpeed;
            Car.Y += Speed.Y; 
        }

        for (int i = 0; i < Cars.Count; i++)
            if (Cars[i].X > RoadLimit + Constants.LengthCar)
                Cars.RemoveAt(i);
    }

    public void Redraw(System.Drawing.Graphics Picture, int Width, int Height)
    {
        // Рисуем дорожное полотно
        System.Drawing.SolidBrush RoadBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
        Picture.FillRectangle(RoadBrush, 0, 0, Width, Height);

        // Риcуем разметку
        float[] dashValues = { 5, 2};
        for (int i = 0; i < Height; i += (int)Math.Round(Constants.LinesDistance))
        {
            Pen whitePen = new Pen(Color.White, 1);
            whitePen.DashPattern = dashValues;
            Picture.DrawLine(whitePen, new Point(0, i), new Point(Width, i));
        }

        // Рисуем машиииииииииииинки
        foreach (Car Car in Cars)
        {
            int XMin = (int)Math.Round(Car.X - Constants.LengthCar);
            int YMin = (int)Math.Round(Car.Y - Constants.WidthCar);
            int XMax = (int)Math.Round(Constants.LengthCar * 2);
            int YMax = (int)Math.Round(Constants.WidthCar * 2);

            //System.Drawing.SolidBrush CarBrush = new System.Drawing.SolidBrush(Car.Color);
            //Picture.FillRectangle(CarBrush, XMin, YMin,XMax,YMax);
            Picture.DrawImage(CarBitmap, XMin, YMin, XMax, YMax);
        }

    }

    private void CarsStreamTimer_Tick(object sender, EventArgs e)
    {
        if (CarsStreamCounter > CarsStreamIntensity)
        {
            if (CarsStreamIntensity!=0)
               CarsStreamToCreateCars += CarsStreamCounter/(int)(CarsStreamIntensity+1);
            CarsStreamCounter = 0;
        }

        CarsStreamCounter += 1;
    }

    public void ParseRules(string [] RulesInStrings)
    {
        Rules = new List<Rule>();

        for (int i = 0; i < RulesInStrings.Length; i++)
            Rules.Add(Analyzer.AnalyzeRule(RulesInStrings[i]));
    }

    public void ClearRoad(PictureBox Canvas)
    {
        Cars = new List<Car>();
        Canvas.Invalidate();
    }
}