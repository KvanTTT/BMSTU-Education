using System;
using System.Net.Sockets;

namespace Networking
{
	/// <summary>
	/// Summary description for ClientInfo.
	/// </summary>
	public class ClientInfo : IDisposable
	{
        public string LogPas;
        public bool Entered
        {
            get;
            set;
        }

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

        public readonly byte Number;

        public IAsyncResult AsyncResult
        {
            get;
            set;
        }

        public ClientInfo(Socket Socket, byte Number, byte[] Buffer)
		{
			this.Socket = Socket;
            this.Buffer = Buffer;
            this.Entered = false;            
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
