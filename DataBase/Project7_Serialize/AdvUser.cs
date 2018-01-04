using System;
using System.Xml.Serialization;

namespace Project7_Serialize
{
    public enum CountryName
    {
        UNITED_STATES,
        FRANCE,
        GERMANY,
        JAPAN,
        UNITED_KINGDOM,
        CANADA,
        BRAZIL,
        ITALY,
        AUSTRALIA,
        SPAIN,
        NETHERLANDS,
        CHINA,
        CZECH_REPUBLIC,
        POLAND,
        BELGIUM,
        RUSSIAN_FEDERATION,
        INDIA,
        FINLAND,
        TAIWAN,
        DENMARK,
        SWEDEN,
        SWITZERLAND,
        HUNGARY,
        AUSTRIA,
        NORWAY,
        PORTUGAL,
        ARGENTINA,
        TURKEY,
        MEXICO,
        ISRAEL,
        HONG_KOG,
        ROMANIA,
        IRELAND,
        NEW_ZEALAND,
        SLOVAKIA,
        SOUTH_AFRICA,
        THAILAND,
        GREECE,
        UKRAINE,
        MALAYSIA,
        BULGARIA,
        SINGAPORE,
        CROATIA,
        KOREA_REPUBLIC_OF,
        VENEZUELA,
        CHILE,
        COLOMBIA,
        PHILIPPINES,
        SLOVENIA,
        INDONESIA,
    }

    public class RealInfo
    {
        public string FirstName;
        public string SecondName;
        public DateTime BirthDate;

        public RealInfo()
        {
        }

        public RealInfo(string FirstName, string SecondName, DateTime BirthDate)
        {
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.BirthDate = BirthDate;
        }
    }

    public class Record
    {
        public string Type;
    }


    public class AdvUser : Record
    {
        [XmlAttribute]
        public uint ID { get; set; }

        [XmlAttribute]
        public string UserName { get; set; }

        [XmlElement]
        public string Team { get; set; }

        [XmlElement]
        public CountryName Country { get; set; }

        [XmlElement]
        public string Project { get; set; }

        [XmlElement]
        public DateTime RegDate { get; set; }

        [XmlElement]
        public float AvgScore { get; set; }

        [XmlElement]
        public uint AllScore { get; set; }

        public RealInfo Info { get; set; }

        public string[] Computers;

        public AdvUser() { }

        public AdvUser(uint ID, string UserName, CountryName Country, string Project, DateTime RegDate,
            float AvgScore, uint AllScore) : base()
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Country = Country;
            this.Project = Project;
            this.RegDate = RegDate;
            this.AvgScore = AvgScore;
            this.AllScore = AllScore;

            Computers = new string[]
            {
                "Processor: Intel Core 2 Duo, 2Ghz; Ram: 2Gb; Hard: 500 Gb",
                "Processor: Intel Celeron, 2Ghz; Ram: 512Gb; Hard: 50 Gb",
                "Processor: AMD Duron, 800Mhz; Ram: 1Gb; Hard: 100 Gb",
            };

            this.Info = new RealInfo("Daniel", "Coen", new DateTime(1970, 04, 02));

            base.Type = "AdvUser";
        }

        public override string ToString()
        {
            return "ID = " + ID.ToString() + "; UserName = " + UserName +
                "; Country = " + Country + "; Project = " + Project +
                "; RegDate = " + RegDate.ToString() + "; AvgScore = " + AvgScore.ToString() +
                "; AllScore = " + AllScore.ToString();
        }
    }
}
