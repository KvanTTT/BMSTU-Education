using AdvanceMath;
using System;
using System.Diagnostics;
using System.Threading;
using Utils;

namespace Test
{
	class Program
    {
        static double StatCalc(double X)
        {
            return Math.Cos(X) + Math.Sin(X) - X;
        }

        double Calc(double X)
        {
            return Math.Cos(X) + Math.Sin(X) - X;
        }
     

        static void Main(string[] args)
        {
            /*
                // debug
                straight func time: 7,900974 sec
                static func time:   10,49656 sec
                dynamic func time: 11,75082 sec

                straight func time: 7,908769 sec
                static func time:   9,859972 sec
                dynamic func time: 10,90511 sec

                straight func time: 7,898407 sec
                static func time:   10,48148 sec
                dynamic func time: 11,72088 sec


                // release
                straight func time: 8,883073 sec
                static func time:   9,085726 sec
                dynamic func time: 9,893035 sec

                straight func time: 8,867755 sec
                static func time:   9,073434 sec
                dynamic func time: 9,919697 sec

                straight func time: 8,320682 sec
                static func time:   9,591556 sec
                dynamic func time: 10,24124 sec
             */
             
            Console.WriteLine("Write any key.... ");
            Console.Read();

            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Process CurProcess = Process.GetCurrentProcess();
            CurProcess.PriorityClass = ProcessPriorityClass.RealTime;

            uint N = 100000000;

            double X;
            Formula F = new Formula("Cos(X) + Sin(X) - X");
            int i;
            X = 0;
            PerfCounter perfCount = new PerfCounter();

            /*while (true)
            {
                F = new Formula("Cos(X) + Sin(X) - X");
                Console.WriteLine("F created");
                Console.ReadKey();
            }*/

            perfCount.Start();            
            for (i = 0; i < N; i++)
                X = Math.Cos(X) + Math.Sin(X) - X;
            Console.WriteLine("straight func time: " + perfCount.Finish().ToString() + " sec");

            perfCount.Start();
            for (i = 0; i < N; i++)
                X = StatCalc(X);
            Console.WriteLine("static func time:   " + perfCount.Finish().ToString() + " sec");

            perfCount.Start();
            for (i = 0; i < N; i++)
                X = F.Calc(X);
            Console.WriteLine("dynamic func time: " + perfCount.Finish().ToString() + " sec");

            Console.WriteLine("Calculating done.... ");

            Thread.CurrentThread.Priority = ThreadPriority.Normal;
            CurProcess.PriorityClass = ProcessPriorityClass.Normal;

            Console.ReadKey();
        }
    }
}
