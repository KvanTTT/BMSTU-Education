using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Project5_DOMforXML
{
    class Program
    {
        private static XmlDocument Document;

        class ConsoleMenu
        {
            List<string> Items;

            public ConsoleMenu(List<string> Items)
            {
                this.Items = Items;
            }

            public int Select()
            {
                Console.WriteLine("Select action: ");
                for (int i = 0; i < Items.Count; i++)
                    Console.WriteLine(i + " - " + Items[i]);

                int Action = -1;
                int.TryParse(Console.ReadLine(), out Action);
                return Action;
            }
        }

        static void Main()
        {
            ConsoleMenu Menu = new ConsoleMenu(new List<string>(){
                "Open document", 
                "Search", 
                "Accsess to nodes", 
                "Change document", 
                "Exit"});

            while (true)
            {
                switch (Menu.Select())
                {
                    case 0:
                        OpenXmlDocument(@"..\..\..\Data\UsersDOM.xml");
                        Console.WriteLine("Document has been opened");
                        break;
                    case 1:
                        SearchInXml();
                        break;
                    case 2:
                        NodesReview();
                        break;
                    case 3:
                        Editing();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void OpenXmlDocument(string filePath)
        {
            Document = new XmlDocument();
            FileStream File = new FileStream(filePath, FileMode.Open);
            XmlReaderSettings Set = new XmlReaderSettings();
            Set.ProhibitDtd = false;
            XmlReader Reader = XmlReader.Create(File, Set);
            Document.Load(Reader);
            File.Close();
        }

        static void SearchInXml()
        {
            if (Document != null)
            {
                ConsoleMenu MenuSearch = new ConsoleMenu(new List<string>(){
                    "GetElementsByTagName",
                    "GetElementById",
                    "SelectNodes",
                    "SelectSingleNode",
                    "Back"});

                switch (MenuSearch.Select())
                {
                    case 0:
                        Console.WriteLine("Enter Tag Name");
                        string tagName = Console.ReadLine();

                        XmlNodeList myNodes = Document.GetElementsByTagName(tagName);
                        for (int i = 0; i < myNodes.Count; i++)
                            Console.WriteLine(myNodes[i].ChildNodes[0].Name + ": " + myNodes[i].ChildNodes[0].Value);

                         //   Console.Write("Wrong Tag Name");
                        break;
                    case 1:
                        Console.WriteLine("Enter ID");
                        string idValue = Console.ReadLine();

                        try
                        {
                            XmlElement element = Document.GetElementById(idValue);
                            foreach (XmlNode Node in element.ChildNodes)
                                Console.WriteLine(Node.Name + ": " + Node.ChildNodes[0].Value);
                        }
                        catch
                        {
                            Console.WriteLine("Wrong ID");
                        }

                        break;
                    case 2:
                        Console.WriteLine("Enter Xpath string");
                        string xPathStr = Console.ReadLine();// //User/Team/text()[../../RegDate/text()='2007/9/20'];

                        try
                        {
                            XmlNodeList selectNodes = Document.SelectNodes(xPathStr);
                            for (int i = 0; i < selectNodes.Count; i++)
                                Console.WriteLine(selectNodes[i].Value);
                        }
                        catch
                        {
                            Console.Write("Wrong Xpath string");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter Xpath string");
                        xPathStr = Console.ReadLine();// //User/Team/text()[../../RegDate/text()='2007/9/20'];

                        try
                        {
                            XmlNode selectNode = Document.SelectSingleNode(xPathStr);
                            if (selectNode != null)
                                Console.WriteLine(selectNode.Value);
                        }
                        catch
                        {
                            Console.Write("Wrong Xpath string");
                        }
                           // Console.Write("Wrong Xpath string");
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
            else
            {
                Console.WriteLine("XML document has not been opened");
            }
        }

        static void NodesReview()
        {
            if (Document != null)
            {
                ConsoleMenu MenuReview = new ConsoleMenu(new List<string>(){
                    "XmlElement",
                    "XmlТext",
                    "XmlComment",
                    "XmlProcessingInstruction",
                    "XmlAttribute",
                    "Back"});


                switch (MenuReview.Select())
                {
                    case 0:
                        XmlElement element1 = (XmlElement)Document.ChildNodes[4].ChildNodes[2];
                        Console.WriteLine(element1.ChildNodes[0].LastChild.Value);
                        break;
                    case 1:
                        Console.WriteLine(Document.DocumentElement.ChildNodes[1].OuterXml);
                        break;
                    case 2:
                        Console.WriteLine(Document.DocumentElement.ChildNodes[3].Value);
                        break;
                    case 3:
                        XmlProcessingInstruction myPI = (XmlProcessingInstruction)Document.DocumentElement.ChildNodes[1].ChildNodes[0];
                        Console.WriteLine("Name: " + myPI.Name);
                        Console.WriteLine("Data: " + myPI.Data);
                        break;
                    case 4:
                        XmlAttributeCollection myAttributes = Document.DocumentElement.ChildNodes[2].Attributes;
                        for (int i = 0; i < myAttributes.Count; i++)
                            Console.WriteLine("Attribute: " + myAttributes[i].Name + " = " + myAttributes[i].Value);
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }

            }
            else
            {
                Console.WriteLine("XML document has not been opened");
            }
        }

        static void Editing()
        {
            if (Document != null)
            {

                ConsoleMenu MenuEdit = new ConsoleMenu(new List<string>(){
                    "Delete content",
                    "Change content",
                    "Create content",
                    "Add attribute",
                    "back"});

                switch (MenuEdit.Select())
                {
                    case 0:
                        Console.WriteLine("Enter user number ");
                        int n = int.Parse(Console.ReadLine());
                        Document.DocumentElement.RemoveChild(Document.DocumentElement.ChildNodes[n]);
                        Document.Save(@"..\..\..\Data\Users51.xml");
                        break;
                    case 1:
                        XmlNodeList NodeList = Document.SelectNodes("//User/RegDate/text()");
                        for (int i = 0; i < NodeList.Count; i++)
                            NodeList[i].Value = NodeList[i].Value.Remove(4, NodeList[i].Value.Length - 4);
                        Document.Save(@"..\..\..\Data\Users52.xml");
                        break;
                    case 2:
                         
                        XmlElement UserElement = Document.CreateElement("User");
                        XmlElement TeamElement = Document.CreateElement("Team");
                        XmlText TeamText = Document.CreateTextNode("Russian");
                        XmlElement CountryElement = Document.CreateElement("Country");
                        XmlText CountryText = Document.CreateTextNode("Russian Federation");
                        XmlElement ProjectElement = Document.CreateElement("Project");
                        XmlText ProjectText = Document.CreateTextNode("Folding@Home");
                        XmlElement AllScoreElement = Document.CreateElement("AllScore");
                        XmlText AllScoreText = Document.CreateTextNode("1000000");
                        TeamElement.AppendChild(TeamText);
                        CountryElement.AppendChild(CountryText);
                        ProjectElement.AppendChild(ProjectText);
                        AllScoreElement.AppendChild(AllScoreText);

                        UserElement.AppendChild(TeamElement);
                        UserElement.AppendChild(CountryElement);
                        UserElement.AppendChild(ProjectElement);
                        UserElement.AppendChild(AllScoreElement);
                        UserElement.SetAttribute("ID", Convert.ToString(1001));
                        UserElement.SetAttribute("UserName", "KvanTTT");

                        Document.DocumentElement.AppendChild(UserElement);
                        Document.Save(@"..\..\..\Data\Users53.xml");
                        break;

                    case 3:
                        Document.DocumentElement.SetAttribute("Update", DateTime.Now.ToString());
                        Document.Save(@"..\..\..\Data\Users54.xml");
                        break;

                    case 4:
                        break;

                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
            else
            {
                Console.WriteLine("XML document has not been opened");
            }
        }
    }
}
