using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Networking
{
    public class NetworkUnit
    {
        public event NetworkEventHandler DataRecieved;

        protected const int BUFFERSIZE = 8192;

        public Socket mainSocket
        {
            protected set;
            get;            
        }

        public ushort Port
        {
            protected set;
            get;
        }

        public string IP
        {
            protected set;
            get;
        }        

        protected AsyncCallback dataRecievedCallback;

        public NetworkUnit() 
        {

        }

        public NetworkUnit(string IP, ushort Port)
        {
            this.IP = IP;
            this.Port = Port;
        }

        //public virtual void Send(byte[] data);

        //public virtual void OnDataReceived();
    }
}
