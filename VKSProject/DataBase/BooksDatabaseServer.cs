using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Networking;

namespace DataBase
{
	public class BooksDatabaseServer : ServerBase
	{
		string Key = "N5kATk1NyGVY2fb5kCviF9waPRbbuSUh";
		string Vector = "pscywJtptRh4v0YvMxGDcdXUTh2yUsnB";
		string NetKey = "SWhu0CqD2chOgnZzejaEbDmlgIy0EDuj";
		string NetVector = "BX9NevM8zlzNAAkYGy9enVGkKFjNEWZp";

		private string[] LoginPassword;

		public BooksDatabase BooksDatabase;

		public event EventHandler RecieveCompleted;

		ClientInfo CurClient;
		SymmetricCryption Crypt;

		public BooksDatabaseServer(string localIP, ushort port, string DatabaseFileName, string LogPasFileName,
			string TransactLogFile)
			: base(localIP, port)
		{
			Crypt = new SymmetricCryption(Key, Vector); 
			/*StringBuilder sb = new StringBuilder();
			sb.Append("KvanTTT");
			sb.Append(' ');
			sb.Append("asdf");
			sb.Append(Environment.NewLine);
			sb.Append("Igor");
			sb.Append(' ');
			sb.Append("spartak");
			sb.Append(Environment.NewLine);

			Crypt.Encrypt(LogPasFileName, sb.ToString()); */    

			LoginPassword = Crypt.Decrypt(LogPasFileName).Split(new string[] {Environment.NewLine}, 
				StringSplitOptions.RemoveEmptyEntries);
			BooksDatabase = new BooksDatabase(this.Send, DatabaseFileName, TransactLogFile);
		}

		public void OnRecieveCompleted()
		{
			if (RecieveCompleted != null) 
				RecieveCompleted(this,new EventArgs());
		}
		

		public override void OnDataRecieved(ClientInfo info)
		{
			if (!info.Entered)
			{ 
				if ((DataBaseCommand)info.Buffer[1] == DataBaseCommand.CheckLogin)
				{
					int BufferLength = info.Buffer[0];
					byte[] ar = new byte[BufferLength - 2];
					Array.Copy(info.Buffer, 2, ar, 0, BufferLength - 2);
					/*SymmetricCryption LoginCrypt = new SymmetricCryption(NetKey, NetVector);
					string LogPas = LoginCrypt.Decrypt(ar);*/
					string LogPas = Encoding.UTF8.GetString(ar);
					foreach (string Line in LoginPassword)
						if (Line == LogPas)
						{
							info.Entered = true;
							break;
						}
                    if (!info.Entered)
                        info.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    
				}
			}
			else
			{
				BooksDatabase.Update(info.Buffer);

				// check if the stream is empty
				if (info.Socket.Available == 0)
					OnRecieveCompleted();
			}
			base.OnDataRecieved (info);
		}
		

		public override void OnClientConnected(ClientInfo info)
		{
			base.OnClientConnected(info);
			//BooksDatabase.Login();
			//BooksDatabase.Commit();
			//info.Socket.Send(new byte[2] { 1, (byte)DataBaseCommand.Login });
		}

		public override void OnClientDisConnected(ClientInfo info)
		{

			base.OnClientDisConnected(info);
		}

		public void Send(byte ClientNumber, byte[] Buffer)
		{
			base.Send(ClientNumber, Buffer);
		}
	}
}
