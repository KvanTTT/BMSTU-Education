using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DistributedComputing
{
    class TasksGen
    {
        List<string> DataBase;

        public TasksGen() { }

        public void GenerateDataBase(int Count)
        {
            DataBase = new List<string>(Count);

            StringBuilder SB = new StringBuilder(100);
            Random Rand = new Random();
            byte[] TaskUID = new byte[16];
            DateTime DT1 = new DateTime(), DT2 = new DateTime();
            TimeSpan DeltaTime;
            TimeSpan MinTaskTime = new TimeSpan(0, 0, 10, 0, 0);     
            bool DateTimeError;

            for (int i = 0; i < Count; i++)
            {
                // Task ID
                SB.Append(i + 1);
                SB.Append((char)9);

                // Task UID
                Rand.NextBytes(TaskUID);
                for (int j = 0; j < TaskUID.Length; j++)
                    SB.Append(TaskUID[j].ToString("X2"));
                SB.Append((char)9);

                // Task Score
                SB.Append(Rand.Next(20, 301));
                SB.Append((char)9);

                // Task Start End DateTime
                DateTimeError = true;
                while (DateTimeError)
                {
                    try
                    {
                        DT1 = new DateTime(Rand.Next(1998, 2011), Rand.Next(1, 13), Rand.Next(1, 32),
                                       Rand.Next(24), Rand.Next(60), Rand.Next(60), DateTimeKind.Utc);
                        DeltaTime = new TimeSpan(Rand.Next(20) == 1 ? Rand.Next(32) : 0, Rand.Next(24), Rand.Next(60), Rand.Next(60), 0);

                        if (DeltaTime < MinTaskTime)
                            throw new Exception();

                        DT2 = DT1 + DeltaTime;
                        
                        if (DT2 >= DateTime.Now)
                            throw new Exception();

                        DateTimeError = false;
                    }
                    catch
                    {
                        DateTimeError = true;
                    }
                }
                /*SB.Append(DT1);
                SB.Append((char)9);
                SB.Append(DT2);*/
                
                SB.Append(DT1.Year);
                SB.Append('/');
                SB.Append(DT1.Month);
                SB.Append('/');
                SB.Append(DT1.Day);
                SB.Append((char)9);

                SB.Append(DT2.Year);
                SB.Append('/');
                SB.Append(DT2.Month);
                SB.Append('/');
                SB.Append(DT2.Day);
              
                DataBase.Add(SB.ToString());
                SB.Length = 0;
            }
        }

        public void WriteDataBase(string FileName)
        {
            StreamWriter StreamW = new StreamWriter(FileName);

            for (int i = 0; i < DataBase.Count; i++)
                StreamW.WriteLine(DataBase[i]);

            StreamW.Close();
        }
    }
}
