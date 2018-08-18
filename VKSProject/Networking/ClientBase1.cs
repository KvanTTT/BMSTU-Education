using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Networking
{
	/// <summary>
	/// Summary description for ClientBase.
	/// </summary>
	public class ClientBase : NetworkUnit
	{
        public event NetworkEventHandler DataRecieved;

		public ClientInfo Info
		{
			get;
			protected set;
		}

		public bool Connected
		{
			get { return mainSocket.Connected; }
		}

		public ClientBase() : base()
		{
			mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			dataRecievedCallback = new AsyncCallback(OnDataRecieved);
			Info = new ClientInfo(mainSocket, new byte[BUFFERSIZE]);
		}

		public ClientBase(string ServerIP, ushort Port) : base(ServerIP, Port)
		{
			mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			dataRecievedCallback = new AsyncCallback(OnDataRecieved);
			Info = new ClientInfo(mainSocket, new byte[BUFFERSIZE]);
		}

        public void Connect()
        {
            mainSocket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port));
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

        public void Send(byte[] data)
        {
            mainSocket.Send(data);
        }

        public virtual void OnDataRecieved()
        {
            if (DataRecieved != null)
                DataRecieved(this, new NetworkEventArgs(Info));
        }		
	}
}
