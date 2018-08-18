using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Networking;
using DataBase;

namespace ServerConsole
{
    class Program
    {
        static BooksDatabaseServer Server;

        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            Server = new BooksDatabaseServer(ipAddress.ToString(), 11000,
                @"..\..\..\ServerStorage\BooksDataBase.xml",
                @"..\..\..\ServerStorage\LoginPasswords",
                @"..\..\..\ServerStorage\Transactions.log");

            Server.ClientConnected += new NetworkEventHandler(ServerClientConnected);
            Server.ClientDisConnected += new NetworkEventHandler(ServerClientDisConnected);
            Server.DataRecieved += new NetworkEventHandler(ServerDataRecieved);
            Server.RecieveCompleted += new EventHandler(ServerRecieveCompleted);
            Server.Start();

            string Line;
            
            do
            {
                Line = Console.ReadLine();
                if (Line.Equals("exit"))
                {
                    Server.Stop();
                    break;
                }

            }
            while (true);

            Server.Stop();
        }

        static private void ServerClientConnected(object sender, NetworkEventArgs ne)
        {
            Console.WriteLine(ne.Info.Socket.RemoteEndPoint.ToString() +
                ne.Info.LogPas + " connected.\r\n");
        }

        static private void ServerClientDisConnected(object sender, NetworkEventArgs ne)
        {
            Console.WriteLine(ne.Info.Socket.RemoteEndPoint.ToString() + " disconnected.\r\n");
        }

        static private void ServerDataRecieved(object sender, NetworkEventArgs ne)
        {
            
            //Console.WriteLine("recieving");
        }

        static private void ServerRecieveCompleted(object sender, EventArgs e)
        {
            //Console.WriteLine("recieve completed");
        }


    }
}
