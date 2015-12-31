namespace RF.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using Zdx.LogManage;

    public class ThreadServer
    {
        private Thread _acceptThread;
        private List<ConnectionInfo> _connections;
        private bool _isbinded;
        private bool _IsStart;
        private int _port;
        private Socket _serverSocket;
        private string _SvrIP;
        private string _SvrName;
        private DoSckRecievedData doRecievedData;
        private StringBuilder sbErr;

        public ThreadServer(int port)
        {
            this._port = 0x1fa4;
            this._SvrIP = "";
            this._SvrName = "";
            this.sbErr = new StringBuilder("");
            this._IsStart = false;
            this.doRecievedData = null;
            this._isbinded = false;
            this._connections = new List<ConnectionInfo>();
            this._port = port;
            this._SvrIP = "";
        }

        public ThreadServer(string SvrIp, int port)
        {
            this._port = 0x1fa4;
            this._SvrIP = "";
            this._SvrName = "";
            this.sbErr = new StringBuilder("");
            this._IsStart = false;
            this.doRecievedData = null;
            this._isbinded = false;
            this._connections = new List<ConnectionInfo>();
            this._port = port;
            this._SvrIP = SvrIp;
        }

        private void AcceptConnections()
        {
            while (true)
            {
                Socket socket = this._serverSocket.Accept();
                ConnectionInfo parameter = new ConnectionInfo {
                    Socket = socket,
                    Thread = new Thread(new ParameterizedThreadStart(this.ProcessConnection))
                };
                parameter.Thread.Start(parameter);
                lock (this._connections)
                {
                    this._connections.Add(parameter);
                }
            }
        }

        private void ProcessConnection(object state)
        {
            ConnectionInfo item = (ConnectionInfo) state;
            MemoryStream mmRec = new MemoryStream();
            byte[] buffer = new byte[item.Socket.ReceiveBufferSize];
            Thread.Sleep(50);
            bool flag = false;
            try
            {
                while (!flag)
                {
                    int count = item.Socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    if (count > 0)
                    {
                        flag = true;
                        while (count > 0)
                        {
                            mmRec.Write(buffer, 0, count);
                            count = item.Socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                        }
                    }
                }
                if ((mmRec != null) && (mmRec.Length > 0L))
                {
                    mmRec.Position = 0L;
                    if (this.doRecievedData != null)
                    {
                        this.doRecievedData(item.Socket, mmRec);
                    }
                }
            }
            catch (SocketException exception)
            {
                this.sbErr.Remove(0, this.sbErr.Length);
                this.sbErr.Append(exception.Message);
                new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
            }
            finally
            {
                item.Socket.Close();
                lock (this._connections)
                {
                    this._connections.Remove(item);
                }
            }
        }

        public void SetupServerSocket()
        {
            if (!this._isbinded)
            {
                this._SvrName = Dns.GetHostName();
                IPHostEntry hostEntry = Dns.GetHostEntry(this._SvrName);
                IPEndPoint localEP = null;
                bool flag = false;
                if (this._SvrIP.Trim().Length > 0)
                {
                    foreach (IPAddress address in hostEntry.AddressList)
                    {
                        if (address.ToString() == this._SvrIP.Trim())
                        {
                            localEP = new IPEndPoint(address, this._port);
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    localEP = new IPEndPoint(hostEntry.AddressList[0], this._port);
                    this._SvrIP = hostEntry.AddressList[0].ToString();
                }
                this._serverSocket = new Socket(localEP.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                this._serverSocket.Bind(localEP);
                this._serverSocket.Listen(0x7fffffff);
                this._isbinded = true;
            }
        }

        public void Start()
        {
            this.SetupServerSocket();
            this._acceptThread = new Thread(new ThreadStart(this.AcceptConnections));
            this._acceptThread.Start();
        }

        public void Stop()
        {
            if (this._IsStart)
            {
                this._IsStart = false;
            }
            try
            {
                if (this._acceptThread != null)
                {
                    if (this._acceptThread.ThreadState == ThreadState.Running)
                    {
                        this._acceptThread.Suspend();
                    }
                    foreach (ConnectionInfo info in this._connections)
                    {
                        if (info.Thread.ThreadState == ThreadState.Running)
                        {
                            info.Thread.Suspend();
                            if (info.Socket.Connected)
                            {
                                info.Socket.Close();
                            }
                            info.Thread.Abort();
                        }
                        lock (this._connections)
                        {
                            this._connections.Remove(info);
                        }
                    }
                    if (this._serverSocket.Connected)
                    {
                        this._serverSocket.Close();
                    }
                    this._acceptThread.Abort();
                }
            }
            catch (SocketException exception)
            {
                this.sbErr.Remove(0, this.sbErr.Length);
                this.sbErr.Append(exception.Message);
                new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
            }
        }

        public int ConnectionCount
        {
            get
            {
                return this._connections.Count;
            }
        }

        public DoSckRecievedData DoRecievedData
        {
            set
            {
                this.doRecievedData = value;
            }
        }

        public string ErrInfo
        {
            get
            {
                return this.sbErr.ToString();
            }
        }

        public bool IsStart
        {
            get
            {
                return this._IsStart;
            }
            set
            {
                this._IsStart = value;
                if (!this._IsStart)
                {
                    this.Stop();
                }
            }
        }

        public bool IsSvrSocketBinded
        {
            get
            {
                return this._isbinded;
            }
        }

        public string SvrIPAddress
        {
            get
            {
                return this._SvrIP;
            }
        }

        public string SvrName
        {
            get
            {
                return this._SvrName;
            }
        }

        public int SvrPort
        {
            get
            {
                return this._port;
            }
        }

        private class ConnectionInfo
        {
            public System.Net.Sockets.Socket Socket;
            public System.Threading.Thread Thread;
        }
    }
}

