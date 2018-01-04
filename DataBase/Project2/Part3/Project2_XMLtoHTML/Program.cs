using System;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Project2_XMLtoHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Syntax: TRANSFORM xmldoc xsldoc");
                    return;
                }

                XPathDocument doc = new XPathDocument(args[0]);
                XslTransform xsl = new XslTransform();
                xsl.Load(args[1]);
                StreamWriter Writer = new StreamWriter(Path.GetFileNameWithoutExtension(args[0]) + ".html");
                xsl.Transform(doc, null, Writer);
                xsl.Transform(doc, null, Console.Out);
                Console.WriteLine();
                Console.WriteLine("All Done!!!");
                Writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
