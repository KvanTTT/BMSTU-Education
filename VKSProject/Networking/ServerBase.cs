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
	public class ServerBase
	{
		private const int BACKLOG = 10;
		private const int BUFFERSIZE = 1024;

		private ushort port;
		private string localIP;
		private Socket mainSocket;
		private AsyncCallback dataRecievedCallback;
		private AsyncCallback acceptCallback;

		public Dictionary<byte, ClientInfo> Clients;
		public event NetworkEventHandler ClientConnected;
		public event NetworkEventHandler ClientDisConnected;
		public event NetworkEventHandler DataRecieved;

		public virtual void OnDataRecieved(ClientInfo info)
		{
			if (DataRecieved != null)
				DataRecieved(this, new NetworkEventArgs(info));
		}

		public virtual void OnClientConnected(ClientInfo info)
		{
			if (ClientConnected != null)
			{
				ClientConnected(this, new NetworkEventArgs(info));

				Clients.Add(info.Number, info);

				//Connections.Add(mainSocket.EndAccept((IAsyncResult)info));
				//WaitForData()
			}
		}

		public virtual void OnClientDisConnected(ClientInfo info)
		{
			if (ClientDisConnected != null)
			{
				ClientDisConnected(this, new NetworkEventArgs(info));

				Clients.Remove(info.Number);

				if (mainSocket.Connected)
					mainSocket.Shutdown(SocketShutdown.Receive);//new IPEndPoint(IPAddress.Parse(info.Socket.), port));
			}
		}

		public ServerBase(string localIP, ushort port)
		{
			this.port = port;
			this.localIP = localIP;
			this.Clients = new Dictionary<byte, ClientInfo>(1);

			mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			mainSocket.Bind(new IPEndPoint(IPAddress.Parse(localIP), port));
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

		public void Send(byte ClientNumber, byte[] data) // в последствии нужно исправить, чтобы можно было с разными клиентам работать
		{
			Clients[ClientNumber].Socket.Send(data);
		}

		private void OnAccept(IAsyncResult AsyncResult)
		{
			Socket clientSoc = mainSocket.EndAccept(AsyncResult);

			ClientInfo info = null;
			for (byte i = 0; i < 255; i++)
				if (!Clients.ContainsKey(i))
				{
					info = new ClientInfo(clientSoc, i, new byte[BUFFERSIZE]);
					break;
				}

			OnClientConnected(info);
			
			WaitForData(info);

			mainSocket.BeginAccept(acceptCallback,null);
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

		private void OnDataRecieved(IAsyncResult ar)
		{
			// free up some memory.
			GC.Collect();

			ClientInfo info = (ClientInfo)ar.AsyncState;
			
			int byteCount = 0;

			byteCount = info.Socket.EndReceive(ar);

			if (byteCount == 0)
			{
				OnClientDisConnected(info);
				info.Dispose();
				info = null;
			}
			else
			{
				OnDataRecieved(info);

				WaitForData(info);
			}
		}
	}
}
