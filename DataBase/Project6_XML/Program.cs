using System;
using System.Net;
using System.Xml;
using System.Windows.Forms;

namespace Project6_XML
{
    public class XMLHTTPExample
    {
        public static void Main()
        {
            string site;
            //site = "http://bash.org.ru/rss/";
            //site = @"http://dxdy.ru/diskussionnye-temy-m-f28/forum.xml";
            //site = @"http://news.yandex.ru/hardware.rss";
            site = @"http://habrahabr.ru/rss/";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(site);

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

            XmlDocument xd = new XmlDocument();
            xd.Load(rsp.GetResponseStream());

            rsp.Close();

            MessageBox.Show(xd.OuterXml);

            Console.ReadKey();
        }
    }
}

