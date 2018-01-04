using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DistributedComputing
{
    class PaymentsGen
    {
        List<string> DataBase;

        public PaymentsGen() { }

        public void GenerateDataBase(int Count)
        {
            DataBase = new List<string>(Count);

            StringBuilder SB = new StringBuilder(100);
            Random Rand = new Random();
            DateTime DT = new DateTime();  
            bool DateTimeError;

            for (int i = 0; i < Count; i++)
            {
                // Payment ID
                SB.Append(i + 1);
                SB.Append((char)9);

                // User ID
                SB.Append(Rand.Next(1, 1001));
                SB.Append((char)9);

                // Task ID
                SB.Append(Rand.Next(1, 1001));
                SB.Append((char)9);

                // Payment Date
                DateTimeError = true;
                while (DateTimeError)
                {
                    try
                    {
                        DT = new DateTime(Rand.Next(1998, 2011), Rand.Next(1, 13), Rand.Next(1, 32),
                                          Rand.Next(24), Rand.Next(60), Rand.Next(60), DateTimeKind.Utc);
                        if (DT >= DateTime.Now)
                            throw new Exception();
                        DateTimeError = false;
                    }
                    catch
                    {
                        DateTimeError = true;
                    }
                }
                /*SB.Append(DT);
                SB.Append((char)9);*/
                SB.Append(DT.Year);
                SB.Append('/');
                SB.Append(DT.Month);
                SB.Append('/');
                SB.Append(DT.Day);
                SB.Append((char)9);

                // Payment Credit
                SB.Append(Rand.Next(20, 301));
              
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
