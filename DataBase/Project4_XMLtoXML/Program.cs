using System.Xml;

namespace Project4_XMLtoXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTextReader Reader = new XmlTextReader(@"..\..\..\Data\Users.xml");
            XmlTextWriter Writer = new XmlTextWriter(@"..\..\..\Data\Users2.xml", null);

            Writer.Formatting = Formatting.Indented;

            while (Reader.Read())
            {
                if ((Reader.NodeType == XmlNodeType.Element) && (Reader.Name == "User"))
                {
                    Writer.WriteStartElement(Reader.Name);
                    Reader.MoveToNextAttribute();
                    Writer.WriteAttributeString("ID", "User" + Reader.Value);
                    Reader.MoveToNextAttribute();
                    Writer.WriteAttributeString("UserName", Reader.Value);
                }
                else
                {
                    switch (Reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Writer.WriteStartElement(Reader.Name);
                            Writer.WriteAttributes(Reader, false);
                            break;
                        case XmlNodeType.CDATA:
                            Writer.WriteCData(Reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            Writer.WriteEndElement();
                            break;
                        case XmlNodeType.EntityReference:
                            Writer.WriteEntityRef(Reader.Name);
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            Writer.WriteProcessingInstruction(Reader.Name, Reader.Value);
                            break;
                        case XmlNodeType.Text:
                        case XmlNodeType.SignificantWhitespace:
                            Writer.WriteString(Reader.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
            Reader.Close();
            Writer.Close();
        }
    }
}
