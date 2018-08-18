using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

namespace Networking
{
	/// <summary>
	/// Summary description for ServerBase.
	/// </summary>
	public class ServerBase : NetworkUnit
	{
		private const int BACKLOG = 10;		        		

		public List<ClientInfo> clients;
        ClientInfo CurInfo;
		public event NetworkEventHandler ClientConnected;
		public event NetworkEventHandler ClientDisConnected;
        private AsyncCallback acceptCallback;

        public event NetworkEventHandler DataRecieved;

        public ServerBase(string LocalIP, ushort Port) : base(LocalIP, Port)
        {
            this.clients = new List<ClientInfo>(1);

            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mainSocket.Bind(new IPEndPoint(IPAddress.Parse(LocalIP), Port));
            acceptCallback = new AsyncCallback(OnAccept);
            dataRecievedCallback = new AsyncCallback(OnDataRecieved);
        }
        
        public void Start()
        {
            mainSocket.Listen(BACKLOG);
            mainSocket.BeginAccept(acceptCallback, null);
        }

        public void Stop()
        {
            if (mainSocket.Connected)
                mainSocket.Shutdown(SocketShutdown.Both);
        }		

		public virtual void OnClientConnected(ClientInfo info)
		{
			if (ClientConnected != null)
			{
				ClientConnected(this, new NetworkEventArgs(info));

				clients.Add(info);
				//Connections.Add(mainSocket.EndAccept((IAsyncResult)info));
				//WaitForData()
			}
		}

		public virtual void OnClientDisConnected(ClientInfo info)
		{
			if (ClientDisConnected != null)
			{
				ClientDisConnected(this, new NetworkEventArgs(info));
				clients.Remove(info);
				if (mainSocket.Connected)
					mainSocket.Shutdown(SocketShutdown.Receive);//new IPEndPoint(IPAddress.Parse(info.Socket.), port));
			}
		}	

        private void OnAccept(IAsyncResult AsyncResult)
        {
            Socket clientSoc = mainSocket.EndAccept(AsyncResult);

            ClientInfo info = new ClientInfo(clientSoc, new byte[BUFFERSIZE]);

            OnClientConnected(info);

            WaitForData(info);

            mainSocket.BeginAccept(acceptCallback, null);
        }
        
		public void Send(byte[] data) // в последствии нужно исправить, чтобы можно было с разными клиентам работать
		{
			clients[0].Socket.Send(data);
		}

        private void WaitForData(ClientInfo info)
        {
            if (dataRecievedCallback == null)
            {
                // Specify the call back function which is to be 
                // invoked when there is any write activity by the 
                // connected client
                dataRecievedCallback = new AsyncCallback(OnDataRecieved);
            }

            info.Socket.BeginReceive(info.Buffer, 0, info.Buffer.Length, SocketFlags.None, dataRecievedCallback, info);
        }

        public virtual void OnDataRecieved()
        {
            if (DataRecieved != null)
                DataRecieved(this, new NetworkEventArgs(CurInfo));
        }

		protected void OnDataRecieved(IAsyncResult ar)
		{
			// free up some memory.
			GC.Collect();

			ClientInfo CurInfo = (ClientInfo)ar.AsyncState;
			
			int byteCount = 0;

            byteCount = CurInfo.Socket.EndReceive(ar);

			if (byteCount == 0)
			{
                OnClientDisConnected(CurInfo);
                CurInfo.Dispose();
                CurInfo = null;
			}
			else
			{
                OnDataRecieved();

                WaitForData(CurInfo);
			}
		}        
	}
}
