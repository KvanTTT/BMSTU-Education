using System;

namespace Project12_LINQ1
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

    public class User
    {
        public uint ID { get; set; }

        public string UserName { get; set; }

        public string Team { get; set; }

        public CountryName Country { get; set; }

        public string Project { get; set; }

        public DateTime RegDate { get; set; }

        public float AvgScore { get; set; }

        public uint AllScore { get; set; }

        public User() { }

        public User(uint ID, string UserName, string  Team, CountryName Country, string Project, DateTime RegDate,
            float AvgScore, uint AllScore)
        {
            this.ID = ID;
            this.UserName = UserName;
            this.Team = Team;
            this.Country = Country;
            this.Project = Project;
            this.RegDate = RegDate;
            this.AvgScore = AvgScore;
            this.AllScore = AllScore;
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
