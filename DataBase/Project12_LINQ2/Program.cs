using System;
using System.Data;
using System.Data.Linq;
using System.Linq;

namespace Project12_LINQ2
{
    class Program
    {
        static void Main(string[] args)
        {
            string Connect = @"Data Source=(local)\SQLEXPRESS; Integrated Security = true; database = DistribComp";

            DistribCompDataContext Context = new DistribCompDataContext(Connect);

            Table<User> Users = Context.GetTable<User>();
           // Table<Payment> Payments = Context.GetTable<Payment>();

            var Query1 = from U in Users
                         where U.RegisterDate >= new DateTime(2005, 01, 01) &&
                               U.RegisterDate < new DateTime(2006, 01, 01)
                         select U;
            foreach (var Q in Query1)
                Console.WriteLine("{0} {1} {2} {3}", Q.UserID, Q.UserName, Q.RegisterDate, Q.AvgScorePerDay);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            /*
            var Query2 = from U in Users
                         join T in Payments on U.UserID equals T.UserID
                         where T.PaymentDate < new DateTime(2010, 01, 01) && U.Project == "Rosetta@home"
                         orderby T.PaymentCredit descending
                         select new 
                         { 
                             ID = U.UserID,
                             TaskID = T.PaymentID,
                             UserName = U.UserName,
                             Project = U.Project,
                             PaymentDate = T.PaymentDate,                             
                             PaymentCredit = T.PaymentCredit 
                         };

            foreach (var Q in Query2)
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", Q.ID, Q.TaskID, Q.UserName, Q.Project,
                    Q.PaymentDate, Q.PaymentCredit);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();*/
            

            var Query3 = from U in Users
                         where U.UserID > 1003
                         select U;
            Users.DeleteAllOnSubmit<User>(Query3);

            
            User User = new User();
            var Query4 = from U in Users
                         where U.UserName.Contains("google")
                         select U.UserName;

            User.UserID = 1100;
            User.UserName = Query4.First<string>();
            User.Team = "[H]ardOCP";
            User.Country = "GERMANY";
            User.Project = "rosetta@home";
            User.RegisterDate = DateTime.Now;
            User.AvgScorePerDay = 100000;
            User.AllScore = 10000000;
            Context.Users.InsertOnSubmit(User);

            var Query5 = from U in Users
                         where (U.UserID % 3 == 0) && U.UserID > 1000
                         select U;

            foreach (var Q in Query5)
                Q.Country = "JAPAN";

            Context.SubmitChanges();

            var QQ = from U in Users
                     where U.UserID > 1000
                     select U;

            foreach (var Q in QQ)
                Console.WriteLine("{0} {1} {2} {3}", 
                    Q.UserID, Q.UserName, Q.Project, Q.Country);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
