namespace RF.Server
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public class DoClientTcp
    {
        private DoReciveData doRecivedData = null;
        private TcpClient tcpclient = null;

        public void DoThreadFunc()
        {
            if (this.tcpclient != null)
            {
                do
                {
                    if (this.tcpclient.Connected)
                    {
                        bool flag = false;
                        NetworkStream nstrm = this.tcpclient.GetStream();
                        if (nstrm.DataAvailable && nstrm.CanRead)
                        {
                            byte[] buffer = new byte[this.tcpclient.ReceiveBufferSize];
                            MemoryStream mmX = new MemoryStream();
                            do
                            {
                                if (nstrm.DataAvailable)
                                {
                                    int count = nstrm.Read(buffer, 0, buffer.Length);
                                    mmX.Write(buffer, 0, count);
                                    flag = true;
                                }
                            }
                            while (nstrm.DataAvailable);
                            if (flag && (this.doRecivedData != null))
                            {
                                this.doRecivedData(this.tcpclient, nstrm, mmX);
                            }
                        }
                    }
                }
                while (this.tcpclient.Connected);
            }
        }

        public DoReciveData DoRecivedData
        {
            get
            {
                return this.doRecivedData;
            }
            set
            {
                this.doRecivedData = value;
            }
        }

        public TcpClient Tcpclient
        {
            get
            {
                return this.tcpclient;
            }
            set
            {
                this.tcpclient = value;
            }
        }

        public delegate void DoReciveData(TcpClient client, NetworkStream nstrm, MemoryStream mmX);
    }
}

