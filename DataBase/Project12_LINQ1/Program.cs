using System;
using System.Collections.Generic;
using System.Linq;

namespace Project12_LINQ1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> Users = new List<User>
            {
                new User(1, "Cryopaul(www.overclockers.com)", "MozillaZine", CountryName.UNITED_KINGDOM, "Anansi", new DateTime(2001, 9, 7), 754.262f, 6158348),
                new User(2, "Bootstomp_Bergilicious", "Team Rage3D", CountryName.UNITED_STATES, "QMC@Home", new DateTime(2000, 2, 6), 810.044f, 9858891),
                new User(3, "louyeh", "p2p-community", CountryName.SLOVENIA, "SIMAP", new DateTime(2009, 10, 16), 769.017f, 4946335),
                new User(4, "bjay25", "Slickdeals", CountryName.JAPAN, "World Community Grid", new DateTime(2001, 1, 17), 28.563f, 9012072),
                new User(5, "KyserTheHun", "CHIP Indonesia Folding Fellow", CountryName.ITALY, "Climate Prediction", new DateTime(2009, 11, 1), 600.022f, 2827150),
                new User(6, "ZiGGA", "TSC! Russia", CountryName.JAPAN, "LHC@Home", new DateTime(2007, 7, 13), 581.937f, 2385230),
                new User(7, "TundraKid04", "Team Rage3D", CountryName.SLOVAKIA, "QMC@Home", new DateTime(2005, 6, 7), 440.371f, 9509408),
                new User(8, "shavol", "Overclockers Australia", CountryName.UNITED_STATES, "yoyo@home", new DateTime(2010, 2, 7), 784.091f, 965811),
                new User(9, "VGambit", "Maximum PC Magazine", CountryName.TURKEY, "Climate Prediction", new DateTime(2008, 4, 17), 772.299f, 5336607),
                new User(10, "stefruppen", "DSL Reports Team Helix", CountryName.UNITED_STATES, "SETI@Home", new DateTime(2005, 9, 25), 96.570f, 7256368),
                new User(11, "nobleswan", "PlanetAMD64", CountryName.GERMANY, "VTU@Home", new DateTime(2009, 6, 29), 635.208f, 4109880),
                new User(12, "rmw-jupiter", "CHIP Indonesia Folding Fellow", CountryName.JAPAN, "Climate Prediction", new DateTime(1999, 12, 15), 793.206f, 4316116),
                new User(13, "Mago", "EOC Folding@Home Team", CountryName.RUSSIAN_FEDERATION, "RALPH@Home", new DateTime(2010, 1, 28), 693.642f, 6401498),
                new User(14, "gsmith3311", "Custom PC & bit-tech", CountryName.SINGAPORE, "Orbit@Home", new DateTime(1999, 11, 29), 721.246f, 2938586),
                new User(15, "eddcosta", "www.overclockers.com", CountryName.GERMANY, "Superlink@Technion", new DateTime(2001, 1, 9), 154.987f, 3042253),
                new User(16, "bigdogvt", "Overclockers Australia", CountryName.IRELAND, "Enigma@Home", new DateTime(2001, 10, 7), 587.919f, 9517132),
                new User(17, "google219623130151099", "General [M]ayhem", CountryName.SINGAPORE, "NQueens Project", new DateTime(1998, 12, 24), 888.728f, 5030623),
                new User(18, "AH82", "ClubLexus", CountryName.UNITED_STATES, "Cels@Home", new DateTime(2005, 1, 5), 286.033f, 1538497),
                new User(19, "Axxl", "ClubLexus", CountryName.PORTUGAL, "SETI@Home", new DateTime(2006, 10, 22), 754.019f, 4842708),
                new User(20, "MichaelTaylor", "folding@evga", CountryName.SLOVENIA, "Climate Prediction", new DateTime(2000, 7, 29), 533.196f, 7529720),
                new User(21, "SevTrek", "MSI HQ Red Rockets", CountryName.FRANCE, "Einstein@Home", new DateTime(1999, 5, 18), 899.406f, 4860052),
                new User(22, "stkitts", "[H]ardOCP", CountryName.ARGENTINA, "SETI@Home", new DateTime(1999, 12, 20), 856.126f, 9223808),
                new User(23, "GreenVail", "Team Hack-a-Day", CountryName.UNITED_KINGDOM, "Cels@Home", new DateTime(2007, 4, 15), 886.366f, 7792113),
                new User(24, "google250285246873631", "Team MacOS X", CountryName.POLAND, "Quake Catcher Network", new DateTime(1999, 1, 10), 728.456f, 4445108),
                new User(25, "VRZ.Hurricane", "Maximum PC Magazine", CountryName.DENMARK, "Enigma@Home", new DateTime(2000, 10, 11), 428.332f, 6988006),
                new User(26, "YeuEmMaiMai", "Overclockers Australia", CountryName.INDONESIA, "Anansi", new DateTime(2004, 5, 19), 631.339f, 8574187),
                new User(27, "TRENTCLARK", "Taiwan Team", CountryName.FRANCE, "Spinhenge@home", new DateTime(2007, 3, 20), 27.362f, 6362025),
                new User(28, "benpinkpanther", "bitGAMER + Underground Gamer Folding", CountryName.KOREA_REPUBLIC_OF, "SETI@Home", new DateTime(2002, 5, 16), 590.137f, 7127658),
                new User(29, "jeblis", "[H]ardOCP", CountryName.RUSSIAN_FEDERATION, "Rosetta@Home", new DateTime(2007, 4, 19), 641.837f, 6856116),
                new User(30, "xXcrazysyrianXx", "The Folding Wolves", CountryName.IRELAND, "Climate Prediction", new DateTime(2009, 5, 10), 184.510f, 3869132)
            };

            var Query1 = from User in Users
                         where User.Country == CountryName.RUSSIAN_FEDERATION
                         orderby User.UserName descending
                         select new {UserName = User.UserName, Project = User.Project, RegDate = User.RegDate};

            Console.WriteLine("++Query1:");
            foreach (var Q in Query1)
                Console.WriteLine("{0} {1} {2}", Q.UserName, Q.Project, Q.RegDate);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();


            var Query2 =
                (from t in
                     from u in Users
                     group u by u.Team into c
                     select new
                     {
                         Team = c.Key,
                         AllScore = (from r1 in c select (int)r1.AllScore).Sum()
                     }
                 orderby t.AllScore descending, t.Team
                 select t);
            Console.WriteLine("++Query2:");
            foreach (var Q in Query2)
                Console.WriteLine("{0} {1}", Q.Team, Q.AllScore);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();


            List<Payment> Payments = new List<Payment>
            {
                new Payment(1,	3,	574,	new DateTime(2000, 09, 12),	74),
                new Payment(2, 29, 377, new DateTime(2008, 12, 29), 271),
                new Payment(3, 13, 371, new DateTime(1999, 09, 08), 276),
                new Payment(4, 109, 349, new DateTime(2000, 05, 31), 48),
                new Payment(5, 15, 38	, new DateTime(2009, 10, 16), 53),
                new Payment(6, 8, 387, new DateTime(2002, 06, 13), 170),
                new Payment(7, 1, 756, new DateTime(2004, 11, 03), 184),
                new Payment(8, 56, 613, new DateTime(2006, 08, 02), 175),
                new Payment(9, 10, 289, new DateTime(2002, 04, 16), 151),
                new Payment(10, 23, 753, new DateTime(2010, 03, 22), 67),
                new Payment(11, 22, 384, new DateTime(2002, 06, 02), 206),
                new Payment(12, 28, 566, new DateTime(2002, 06, 02), 206)
            };

            var Query3 = from u in Users
                         join p in Payments on u.ID equals p.UserID
                         select new
                         {
                             ID = u.ID,
                             UserName = u.UserName,
                             PaymentDate = p.Date,
                             PaymentCredit = p.Credit
                         };
            Console.WriteLine("++Query3:");
            foreach (var Q in Query3)
                Console.WriteLine("{0} {1} {2} {3}", Q.ID, Q.UserName, Q.PaymentDate, Q.PaymentCredit);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            

            var Query4 = from u in Users
                         where u.AvgScore > 600
                         orderby u.UserName ascending
                         select new
                         {
                             Name = u.UserName,
                             Num = (from L in u.UserName
                                    let letter = Convert.ToInt32(L)
                                    select letter).Average().ToString("F02")
                         };
            Console.WriteLine("++Query4:");
            foreach (var Q in Query4)
                Console.WriteLine("{0} {1}", Q.Name, Q.Num);
            Console.WriteLine("------------------------------------");
            Console.WriteLine();


            var Query5 = (from User in Users
                         select new { U = User }).Skip(5).Take(10);
            Console.WriteLine("++Query5:");
            foreach (var Q in Query5)
            {
                Console.WriteLine("{0}, ", Q.U.UserName);
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
