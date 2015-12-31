namespace SeDBSctServer
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public delegate void DoSckRecievedData(Socket skt, MemoryStream mmRec);
}

