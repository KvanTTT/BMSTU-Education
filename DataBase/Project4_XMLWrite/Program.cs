using System.IO;
using System.Text;
using System.Xml;

namespace Project4_XMLWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader Reader = new StreamReader(@"..\..\..\Data\Users.db", Encoding.Default);
            XmlTextWriter Writer = new XmlTextWriter(@"..\..\..\Data\Users.xml", null);

            Writer.Formatting = Formatting.Indented;

            Writer.WriteStartDocument();
            Writer.WriteStartElement("Users");

            while (!Reader.EndOfStream)
            {
                string[] Parts = Reader.ReadLine().Split('\t');

                Writer.WriteStartElement("User");
                Writer.WriteStartAttribute("ID");
                Writer.WriteString(Parts[0]);
                Writer.WriteEndAttribute();
                Writer.WriteStartAttribute("UserName");
                Writer.WriteString(Parts[1]);
                Writer.WriteEndAttribute();

                Writer.WriteElementString("Team", Parts[2]);
                Writer.WriteElementString("Country", Parts[3]);
                Writer.WriteElementString("Project", Parts[4]);
                Writer.WriteElementString("RegDate", Parts[5]);
                Writer.WriteElementString("AvgScore", Parts[6]);
                Writer.WriteElementString("AllScore", Parts[7]);
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();

            Writer.Close();
            Reader.Close();
        }
    }
}
