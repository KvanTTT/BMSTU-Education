using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Networking;

namespace DataBase
{
	public class BooksDatabaseClient : ClientBase
	{
        public string LogPas;

		public BooksDatabase BooksDatabase;

		public event EventHandler RecieveCompleted;

        public BooksDatabaseClient(string serverIP, ushort port, InvokeDelegate Invoke)
			: base(serverIP, port)
		{
			this.BooksDatabase = new BooksDatabase(this.Send, Invoke, 0);			
		}

		public void OnRecieveCompleted()
		{
			if (RecieveCompleted != null)
				RecieveCompleted(this, new EventArgs());
		}

        public void Login()
        {
            List<byte> SendData = new List<byte>();
            byte[] ar = Encoding.UTF8.GetBytes(LogPas);
            SendData.Add((byte)(2 + ar.Length));
            SendData.Add((byte)DataBaseCommand.CheckLogin);
            SendData.AddRange(ar);
            Send(0, SendData.ToArray());
        }

		public override void OnDataRecieved(byte[] data)
		{            
            BooksDatabase.Update(data);
            base.OnDataRecieved(data);
		}

		public void Send(byte NetworkUnitNubmer, byte[] Buffer)
		{
			base.Send(Buffer);
		}


	}
}
