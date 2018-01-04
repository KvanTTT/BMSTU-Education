using System;
using System.Xml;

namespace Project4_XMLReader
{
    class Program
    {
        static void Main()
        {
            XmlTextReader Reader = new XmlTextReader(@"..\..\..\Data\Users.xml");

            Console.Write("Type    Name  Value");
            Console.Write("--------------------------------");
            //Reader.WhitespaceHandling = WhitespaceHandling.Significant;
            while (Reader.Read())
            {
                Console.WriteLine(Reader.NodeType.ToString() + "    " +
                              Reader.Name + "  " + Reader.Value);
            }

            Reader.Close();

            Console.ReadKey();
        }
    }
}
