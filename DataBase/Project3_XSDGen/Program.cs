using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Project3_XSDGen
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Create <xsd:schema>
            XmlSchema Schema = new XmlSchema();

            // Create <xsd:element name="Users"/>
            XmlSchemaElement elementUsers = new XmlSchemaElement();
            elementUsers.Name = "Users";
           /* elementUsers.MinOccurs = 0;
            elementUsers.MaxOccurs = 1;*/
            Schema.Items.Add(elementUsers);

            // Create <xsd:complexType>
            XmlSchemaComplexType tblUsers = new XmlSchemaComplexType();
            elementUsers.SchemaType = tblUsers;

            /*XmlSchemaChoice Choice = new XmlSchemaChoice();
            Choice.MinOccurs = 0;
            Choice.MaxOccursString = "unbounded";
            elementUsers.Items.Add(Choice);*/

            // Create <xsd:sequence>
            XmlSchemaSequence seq0 = new XmlSchemaSequence();
            tblUsers.Particle = seq0;

            // Create <xsd:element name="User"/>
            XmlSchemaElement elementUser = new XmlSchemaElement();
            elementUser.Name = "User";
            seq0.Items.Add(elementUser);


            // Create <xsd:complexType>
            XmlSchemaComplexType tblUser = new XmlSchemaComplexType();
            elementUser.SchemaType = tblUser;

            // Create <xsd:sequence>
            XmlSchemaSequence seq = new XmlSchemaSequence();
            tblUser.Particle = seq;

            // Create <xsd:element name="Team" type="xsd:string"/>
            XmlSchemaElement elementTeam = new XmlSchemaElement();
            elementTeam.Name = "Team";
            elementTeam.SchemaTypeName = new XmlQualifiedName(
                "string", "http://www.w3.org/2001/XMLSchema");
            seq.Items.Add(elementTeam);

            // Create <xsd:element name="elementCountry" type="xsd:CountryList"/>
            XmlSchemaElement elementCountry = new XmlSchemaElement();
            elementCountry.Name = "Country";
            elementCountry.SchemaTypeName = new XmlQualifiedName(
                "CountryList");
            seq.Items.Add(elementCountry);

            // Create <xsd:element name="elementProject" type="xsd:string"/>
            XmlSchemaElement elementProject = new XmlSchemaElement();
            elementProject.Name = "Project";
            elementProject.SchemaTypeName = new XmlQualifiedName(
                "string", "http://www.w3.org/2001/XMLSchema");
            seq.Items.Add(elementProject);

            // Create <xsd:element name="elementRegDate" type="xsd:date"/>
            XmlSchemaElement elementRegDate = new XmlSchemaElement();
            elementRegDate.Name = "Project";
            elementRegDate.SchemaTypeName = new XmlQualifiedName(
                "date", "http://www.w3.org/2001/XMLSchema");
            seq.Items.Add(elementRegDate);

            // Create <xsd:element name="AvgScore" type="xsd:float"/>
            XmlSchemaElement elementAvgScore = new XmlSchemaElement();
            elementAvgScore.Name = "AvgScore";
            elementAvgScore.SchemaTypeName = new XmlQualifiedName(
                "float", "http://www.w3.org/2001/XMLSchema");
            seq.Items.Add(elementAvgScore);

            // Create <xsd:element name="AllScore" type="xsd:int"/>
            XmlSchemaElement elementAllScore = new XmlSchemaElement();
            elementAllScore.Name = "AllScore";
            elementAllScore.SchemaTypeName = new XmlQualifiedName(
                "int", "http://www.w3.org/2001/XMLSchema");
            seq.Items.Add(elementAllScore);

            // Create <xsd:attribute name="UserID" type="xsd:int" use="required"/>
            XmlSchemaAttribute attributeUserID = new XmlSchemaAttribute();
            attributeUserID.Name = "UserID";
            attributeUserID.SchemaTypeName = new XmlQualifiedName(
                "int", "http://www.w3.org/2001/XMLSchema");
            attributeUserID.Use = XmlSchemaUse.Required;
            tblUser.Attributes.Add(attributeUserID);

            // Create <xsd:attribute name="UserName" type="xsd:string" use="required"/>
            XmlSchemaAttribute attributeUserName = new XmlSchemaAttribute();
            attributeUserName.Name = "UserName";
            attributeUserName.SchemaTypeName = new XmlQualifiedName(
                "string", "http://www.w3.org/2001/XMLSchema");
            attributeUserName.Use = XmlSchemaUse.Required;
            tblUser.Attributes.Add(attributeUserName);


            string[] Countries = new string[]
            {
                "UNITED STATES", "FRANCE", "GERMANY", "JAPAN", "UNITED KINGDOM", "CANADA", "BRAZIL", 
                "ITALY", "AUSTRALIA", "SPAIN", "NETHERLANDS", "CHINA", "CZECH REPUBLIC", "POLAND", 
                "BELGIUM", "RUSSIAN FEDERATION", "INDIA", "FINLAND", "TAIWAN", "DENMARK", "SWEDEN", 
                "SWITZERLAND", "HUNGARY", "AUSTRIA", "NORWAY", "PORTUGAL", "ARGENTINA", "TURKEY", 
                "MEXICO", "ISRAEL", "HONG KONG", "ROMANIA", "IRELAND", "NEW ZEALAND", "SLOVAKIA", 
                "SOUTH AFRICA", "THAILAND", "GREECE", "UKRAINE", "MALAYSIA", "BULGARIA", "SINGAPORE", 
                "CROATIA", "KOREA, REPUBLIC OF", "VENEZUELA", "CHILE", "COLOMBIA", "PHILIPPINES", 
                "SLOVENIA", "INDONESIA"
            };

            XmlSchemaSimpleType CountryList = new XmlSchemaSimpleType();
            CountryList.Name = "CountryList";
            //elementCountry.SchemaType = CountryList;
            Schema.Items.Add(CountryList);

            // Create <xsd:restriction base="xsd:string">
            XmlSchemaSimpleTypeRestriction restriction = new XmlSchemaSimpleTypeRestriction();
            restriction.BaseTypeName = new XmlQualifiedName(
                "string", "http://www.w3.org/2001/XMLSchema");
            CountryList.Content = restriction;

            // Create three <xsd:enumeration...> 
            foreach (string Country in Countries)
            {
                XmlSchemaEnumerationFacet e = new XmlSchemaEnumerationFacet();
                e.Value = Country;
                restriction.Facets.Add(e);
            }


            // Compile the schema in memory, to check syntax and semantics
            Schema.Compile(new ValidationEventHandler(MyHandler));

            // Display schema on the screen
            Schema.Write(Console.Out);
            Console.WriteLine("\nSchema has been written to console");

            // Also write schema to "Users.xsd"
            FileStream fs = new FileStream("Users.xsd",
                                           FileMode.Create, FileAccess.Write);
            Schema.Write(fs);
            Console.WriteLine("Schema has been written to file Users.xsd");

            Console.Write("Press any key to exit . . . ");
            Console.ReadKey();
        }

        // Validation event handler method
        public static void MyHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
