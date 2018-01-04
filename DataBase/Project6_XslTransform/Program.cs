using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Project6_XslTransform
{
    public class XslTransformExample
   {
      public static void Main()
      {
         StreamWriter output = new StreamWriter("Russian users.html", false);

         // Instantiate the XslTransform object
         XslTransform xslt = new XslTransform();

         // Load the XSLT style sheet
         xslt.Load("transform.xsl");

         // Create and define the XsltArgumentList.
         XsltArgumentList xslArg = new XsltArgumentList();
         xslArg.AddParam("TableOnly", "", "No");

         // Load XML source
         XPathDocument sourceXML = new XPathDocument("Russian users.xml");

         // Invoke the transform
         xslt.Transform(sourceXML, xslArg, output);

         // Close the StreamWriter
         output.Close();
      }
   }
}
