using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Networking
{
	/// <summary>
	/// Summary description for ClientBase.
	/// </summary>
	public class ClientBase
	{
		private const int BUFFERSIZE = 8192;

		private int port;
		private string serverIP;
		protected Socket mainSocket;
		public ClientInfo Info
		{
			get;
			protected set;
		}

		private AsyncCallback dataRecievedCallback;
		public event NetworkEventHandler DataRecieved;

		public bool Connected
		{
			get { return mainSocket.Connected; }
		}

		public ClientBase(string serverIP, ushort port)
		{
			this.serverIP = serverIP;
			this.port = port;			

			mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			dataRecievedCallback = new AsyncCallback(OnDataRecieved);
			Info = new ClientInfo(mainSocket, 0, new byte[BUFFERSIZE]);
		}

		public virtual void OnDataRecieved(byte[] data)
		{
			if (DataRecieved != null)
			{
				DataRecieved(this, new NetworkEventArgs(Info));
			}
		}

		public void Send(byte[] data)
		{
			mainSocket.Send(data);
		}

		public void Connect()
		{
			mainSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIP), port));
			if (mainSocket.Connected)
			{
				WaitForData();
			}
		}

		public void Disconnect()
		{
			if (mainSocket.Connected)
				mainSocket.Shutdown(SocketShutdown.Both);
		}

		public void WaitForData()
		{
			if (dataRecievedCallback == null)
				dataRecievedCallback = new AsyncCallback(OnDataRecieved);

			mainSocket.BeginReceive(Info.Buffer, 0, 
				Info.Buffer.Length, SocketFlags.None, dataRecievedCallback, Info);
		}


		private void OnDataRecieved(IAsyncResult ar)
		{
			GC.Collect();

			int byteCount = 0;

			byteCount = mainSocket.EndReceive(ar);

			if (byteCount == 0)
			{
				// server disconnected.
			}
			else
			{
				OnDataRecieved(Info.Buffer);

				WaitForData();
			}
		}
	}
}
