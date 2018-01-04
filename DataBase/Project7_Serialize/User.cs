using System;
using System.Xml.Serialization;

namespace Project7_Serialize
{
    public class User
    {
        [XmlAttribute]
        public uint ID { get; set; }

        [XmlAttribute]
        public string UserName { get; set; }

        [XmlElement]
        public string Team { get; set; }

        [XmlElement]
        public string Country { get; set; }

        [XmlElement]
        public string Project { get; set; }

        [XmlElement]
        public DateTime RegDate { get; set; }

        [XmlElement]
        public float AvgScore { get; set; }

        [XmlElement]
        public uint AllScore { get; set; }

        public User() { }

        public User(uint ID, string UserName, string Country, string Project, DateTime RegDate,
            float AvgScore, uint AllScore)
        {
            this.ID = ID;
            this.UserName = UserName;
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
