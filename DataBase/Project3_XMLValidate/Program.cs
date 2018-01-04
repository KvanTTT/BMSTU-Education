using System;
using System.Xml;
using System.Xml.Schema;

namespace Project3_XSD
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Create a cache of schemas, and add two schemas
            XmlSchemaSet Schema = new XmlSchemaSet();
            Schema.Add("", "Users1.xsd");

            XmlReaderSettings Settings = new XmlReaderSettings();
            Settings.ValidationType = ValidationType.Schema;
            Settings.ValidationEventHandler += new ValidationEventHandler(Program.MyHandler);
            Settings.Schemas.Add(Schema);
            XmlReader Reader = XmlReader.Create("Users.xml", Settings);

            try
            {
                while (Reader.Read());
            }
            catch (XmlException exception)
            {
                Console.WriteLine("XMLException occurred: " + exception.Message);
            }
            finally
            {
                Reader.Close();
            }
            Console.Write("Press any key to exit . . . ");
            Console.ReadKey();
        }


        public static void MyHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Error: " + e.Message);
        }
    }
}
