using System;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
	/// <summary>
	/// Summary description for NetworkEventArgs.
	/// </summary>
	public class NetworkEventArgs : EventArgs
	{
		public readonly ClientInfo Info;
		public readonly string ErrorMessage;

		public NetworkEventArgs(ClientInfo Info)
		{
			this.Info = Info;
		}
	}

    public delegate void NetworkEventHandler(object sender, NetworkEventArgs ne);
}
