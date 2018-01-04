using DistributedComputing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Project1_DBGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            int N;
            ConsoleKeyInfo consoleKeyInfo;

            StreamReader Reader = new StreamReader("../../in/Countries1.db");
            List<string> Countries = new List<string>();
            StringBuilder SB = new StringBuilder();
            while (!Reader.EndOfStream)
                Countries.Add(Reader.ReadLine());
            foreach (string Str in Countries)
            {
                SB.Append(@"""" + Str + @""", ");
                //Console.WriteLine(@"""" + Str + @""", ");
            }
            Reader.Close();
            StreamWriter Writer = new StreamWriter("../../in/Countries2.db");
            Writer.Write(SB.ToString());
            Writer.Close();
            

            Console.Write("Enter number of records: ");
            N = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Press any key to generate Distributed Computing Users DataBase or ESC to exit");
            consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                UsersGen UserGenerator = new UsersGen();
                UserGenerator.ReadAllData("../../in/");
                UserGenerator.GenerateDataBase(N);
                //UserGenerator.WriteDataBase("../../out/Users.db");
                UserGenerator.WriteDataBase("../../../Data/UsersTemp.db");
                Console.WriteLine("Users Data Base succsessfully created!");
            }

            Console.WriteLine("Press any key to generate Distributed Computing Tasks DataBase or ESC to exit");
            consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                TasksGen TasksGenerator = new TasksGen();
                TasksGenerator.GenerateDataBase(N);
                TasksGenerator.WriteDataBase("../../out/Tasks.db");
                Console.Write("Tasks Data Base succsessfully created!");
            }

            Console.WriteLine("Press any key to generate Distributed Computing Payments DataBase or ESC to exit");
            consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                PaymentsGen PaymentsGenerator = new PaymentsGen();
                PaymentsGenerator.GenerateDataBase(N);
                PaymentsGenerator.WriteDataBase("../../out/Payments.db");
                Console.Write("Payments Data Base succsessfully created!");
            }

            Console.ReadKey();
        }
    }
}
