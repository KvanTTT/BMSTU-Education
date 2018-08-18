using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Networking  
{
	public class ServerInfo : IDisposable
	{
		public string Login;
		public string Password;

		public Socket Socket
		{
			get;
			set;
		}

		public byte[] Buffer
		{
			get;
			set;
		}

		public ServerInfo(Socket Socket, byte[] Buffer)
		{
			this.Socket = Socket;
			this.Buffer = Buffer;
		}
		#region IDisposable Members

		public void Dispose()
		{
			Buffer = null;
			if (Socket.Connected)
			{
				Socket.Shutdown(SocketShutdown.Both);
				Socket.Close();
			}
			Socket = null;
		}

		#endregion
	}
}
