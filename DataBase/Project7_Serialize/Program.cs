using System;
using System.IO;
using System.Xml.Serialization;

namespace Project7_Serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            User User1, User2, User3;
            User1 = new User(10000, "ProVK", "Zimbabve", "MalariaControl",
                new DateTime(2000, 01, 13), 0.01f, 666);
            User2 = new User(10000, "Ajven", "Australia", "AQUA@Home",
                new DateTime(2005, 06, 20), 97f, 3456735);
            User3 = new User(10000, "asdf", "Russian Federation", "MilkyWay@hom",
                new DateTime(2008, 01, 13), 211f, 634587);
            XmlSerializer Serializer = new XmlSerializer(typeof(User));
            Serializer.UnknownAttribute += 
                new XmlAttributeEventHandler(MyUnknownAttributeHandler);
            Serializer.UnknownElement += new XmlElementEventHandler(MyUnknownElementHandler);
            Serializer.UnknownNode += new XmlNodeEventHandler(MyUnknownNodeHandler);

            // Сериализация объекта User1
            TextWriter writer1 = new StreamWriter(@"..\..\..\Data\User1.xml");
            Serializer.Serialize(writer1, User1);
            writer1.Close();
            Console.WriteLine("User1 object has been serialized");

            // Сериализация объекта User2
            TextWriter writer2 = new StreamWriter(@"..\..\..\Data\User2.xml");
            Serializer.Serialize(writer2, User2);
            writer2.Close();
            Console.WriteLine("User2 object has been serialized");

            // Сериализация объекта User3
            TextWriter writer3 = new StreamWriter(@"..\..\..\Data\User3.xml");
            Serializer.Serialize(writer3, User3);
            writer3.Close();
            Console.WriteLine("User3 object has been serialized");

            User DeserializeUser1, DeserializeUser2, DeserializeUser3;

            // Десериализация в объект User1
            TextReader Reader1 = new StreamReader(@"..\..\..\Data\User1.xml");
            DeserializeUser1 = (User)Serializer.Deserialize(Reader1);            
            Console.WriteLine(DeserializeUser1.ToString());
            Reader1.Close();

            // Десериализация в объект User2
            TextReader Reader2 = new StreamReader(@"..\..\..\Data\User2.xml");
            DeserializeUser2 = (User)Serializer.Deserialize(Reader2);
            Console.WriteLine(DeserializeUser2.ToString());
            Reader2.Close();

            // Десериализация в объект User3
            TextReader Reader3 = new StreamReader(@"..\..\..\Data\User3.xml");
            DeserializeUser3 = (User)Serializer.Deserialize(Reader3);
            Console.WriteLine(DeserializeUser3.ToString());
            Reader3.Close();

            AdvUser AdvUser1 = new AdvUser(153467, "Q_W_E_R_T_Y", CountryName.NETHERLANDS, "world community grid",
                                            new DateTime(1999, 11, 02), 5.0f, 12345324);
            XmlSerializer Serializer1 = new XmlSerializer(typeof(AdvUser));
            // Сериализация объекта AdvUser
            TextWriter Writer4 = new StreamWriter(@"..\..\..\Data\AdvUser.xml");
            Serializer1.Serialize(Writer4, AdvUser1);
            Writer4.Close();
            Console.WriteLine("AdvUser object has been serialized");

            // Десериализация в объект AdvUser
            TextReader Reader4 = new StreamReader(@"..\..\..\Data\AdvUser.xml");
            AdvUser1 = (AdvUser)Serializer1.Deserialize(Reader4);
            Console.WriteLine(AdvUser1.ToString());
            Reader4.Close();
            

            Console.ReadKey();
        }

        public static void MyUnknownAttributeHandler(object sender, XmlAttributeEventArgs e)
        {
            string type = e.ObjectBeingDeserialized.GetType().ToString();
            Console.WriteLine("\nUnknown attribute when deserializing " + type);
            Console.WriteLine("Line number: " + e.LineNumber);
            Console.WriteLine("Line position: " + e.LinePosition);
            Console.WriteLine("Attribute name: " + e.Attr.Name);
            Console.WriteLine("Attribute XML: " + e.Attr.OuterXml);
            Console.WriteLine("------------------------------------------------------------------");
        }

        public static void MyUnknownElementHandler(object sender, XmlElementEventArgs e)
        {
            string type = e.ObjectBeingDeserialized.GetType().ToString();
            Console.WriteLine("\nUnknown element when deserializing " + type);
            Console.WriteLine("Line number: " + e.LineNumber);
            Console.WriteLine("Line position: " + e.LinePosition);
            Console.WriteLine("Element name: " + e.Element.Name);
            Console.WriteLine("Element XML: " + e.Element.OuterXml);
            Console.WriteLine("------------------------------------------------------------------");
        }

        public static void MyUnknownNodeHandler(object sender, XmlNodeEventArgs e)
        {
            string type = e.ObjectBeingDeserialized.GetType().ToString();
            Console.WriteLine("\nUnknown node when deserializing " + type);
            Console.WriteLine("Line number: " + e.LineNumber);
            Console.WriteLine("Line position: " + e.LinePosition);
            Console.WriteLine("Node name: " + e.Name);
            Console.WriteLine("Node text: " + e.Text);
            Console.WriteLine("Node type: " + e.NodeType.ToString());
            Console.WriteLine("------------------------------------------------------------------");
        }

    }
}
