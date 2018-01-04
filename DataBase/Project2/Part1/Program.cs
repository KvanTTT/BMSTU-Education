using System;
using System.Data.SqlClient;
using System.Xml;

namespace Project2_XMLDataExtract
{
    public class FORXMLExample
    {
        public static void Main()
        {
            Console.Write("Enter FOR XML Mode (0 - AUTO, 1 - RAW): ");
            int Mode = Console.Read();
            string str = "AUTO";
            switch (Mode)
            {
                case '0': str = "AUTO"; break;
                case '1': str = "RAW"; break;
                //case '2': str = "EXPLICIT"; break;
            }

            string cmdText = "SELECT UserID, UserName, Team, Project " +
                             "FROM tblUsers FOR XML " + str + ", Elements";

            string connect = @"Data Source=(local)\SQLEXPRESS; Initial Catalog=DistribComp; Integrated Security=SSPI";

            SqlConnection sqlCN = new SqlConnection(connect);
            try
            {
                sqlCN.Open();

                SqlCommand sqlCMD = new SqlCommand(cmdText, sqlCN);

                XmlReader xr = sqlCMD.ExecuteXmlReader();
                while (xr.Read())
                {
                    switch (xr.NodeType)
                    {
                        case XmlNodeType.Element:    // Узел является элементом.
                            Console.Write("<" + xr.Name);
                            Console.WriteLine(">");
                            break;
                        case XmlNodeType.Text:       //Отображается текст в каждом элементе.
                            Console.WriteLine (xr.Value);
                            break;
                        case XmlNodeType.EndElement: //Отображается конец элемента.
                            Console.Write("</" + xr.Name);
                            Console.WriteLine(">");
                            break;
                    }
                }

                xr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (!(sqlCN == null))
                {
                    sqlCN.Close();
                }
            }

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}