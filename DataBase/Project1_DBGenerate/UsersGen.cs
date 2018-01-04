using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DistributedComputing
{
    class UsersGen
    {
        List<string> Users;
        List<string> Teams;
        List<string> Countries;
        List<string> Projects;

        List<string> DataBase;

        public UsersGen()
        {
            
        }

        public void ReadAllData(string SourcePath)
        {
            StreamReader StreamR = new StreamReader(SourcePath + "Users.db");
            Users = new List<string>(5000);
            while (StreamR.EndOfStream == false)
                Users.Add(StreamR.ReadLine());

            StreamR = new StreamReader(SourcePath + "Teams.db");
            Teams = new List<string>(100);
            while (StreamR.EndOfStream == false)
                Teams.Add(StreamR.ReadLine());

            StreamR = new StreamReader(SourcePath + "Countries.db");
            Countries = new List<string>(50);
            while (StreamR.EndOfStream == false)
                Countries.Add(StreamR.ReadLine());

            StreamR = new StreamReader(SourcePath + "Projects.db");
            Projects = new List<string>(50);
            while (StreamR.EndOfStream == false)
                Projects.Add(StreamR.ReadLine());
        }

        public void GenerateDataBase(int Count)
        {
            DataBase = new List<string>(Count);

            StringBuilder SB = new StringBuilder(100);
            Random Rand = new Random();
            bool DateTimeError;
            DateTime DT = new DateTime();

            List<string> TempUsers = new List<string>(Count);
            bool Dublicat;

            int UserInd;
            for (int i = 0; i < Count; i++)
            {
                SB.Append("new User(");
                // UserID
                //SB.Append("User");
                SB.Append(i + 1);
                SB.Append(',');
                SB.Append(' ');

                // User Name                
                do
                {
                    Dublicat = false;
                    UserInd = Rand.Next(Users.Count);
                    for (int j = 0; j < TempUsers.Count; j++)
                    {
                        if (TempUsers[j] == Users[UserInd])
                        {
                            Dublicat = true;
                            break;
                        }                        
                    }
                    if (!Dublicat)
                        break;
                }
                while (Dublicat);

                TempUsers.Add(Users[UserInd]);
                SB.Append('"' + Users[UserInd] + '"');
                SB.Append(',');
                SB.Append(' ');

                // User Team
                SB.Append('"' + Teams[Rand.Next(Teams.Count)] + '"');
                SB.Append(',');
                SB.Append(' ');

                // User Country
                SB.Append("CountryName." + Countries[Rand.Next(Countries.Count)]);
                SB.Append(',');
                SB.Append(' ');

                //User Project
                SB.Append('"' + Projects[Rand.Next(Projects.Count)] + '"');
                SB.Append(',');
                SB.Append(' ');

                //User Registration Date
                DateTimeError = true;
                while (DateTimeError)
                {
                    try
                    {
                        DT = new DateTime(Rand.Next(1998, 2011), Rand.Next(1, 13), Rand.Next(1, 32));
                        if (DT >= DateTime.Now)
                            throw new Exception();
                        DateTimeError = false;
                    }
                    catch
                    {
                        DateTimeError = true;
                    }
                }
                SB.Append("new DateTime(");
                SB.Append(DT.Year);
                SB.Append(", ");
                SB.Append(DT.Month);
                SB.Append(", ");
                SB.Append(DT.Day);
                SB.Append("),");
                SB.Append(' ');

                // User Average Score
                SB.Append((Rand.NextDouble() * 1000).ToString("F03").Replace(',', '.'));
                SB.Append("f,");
                SB.Append(' ');

                // User Total Score
                SB.Append(Rand.Next(10000000));
                SB.Append("),");

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
