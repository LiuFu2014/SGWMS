using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data ;
using System.Xml;
using System.Net.Sockets;
using DBCommInfo;
using Zqm.Net;
using System.Windows.Forms;


namespace SunEast
{
    public enum DBSocketServerType {dbsstNormal = 0, dbsstDotNet=1}
    public class SeDBClient
    {
        #region 属性
            private DBSocketServerType dbstServer ;//= DBSocketServerType.dbsstNormal;
            public DBSocketServerType DBSTServer
            {
                get { return dbstServer; }
                set { dbstServer = value; }
            }

            private string serverAddress = "127.0.0.1";
            public string ServerAddress
            {
                get { return serverAddress; }
                set { serverAddress = value; }
            }

            private int port = 8100;
            public int Port
            {
                get { return port; }
                set { port = value; }
            }

            private string cfgFile = "";
            public string CfgFile 
            {
                get {return cfgFile ;}
                set {cfgFile = value ;}
            }

            private int recevieBufferSize = 1024;
            public int RecevieBufferSize
            {
                get { return recevieBufferSize; }
                set { recevieBufferSize = value; }
            }

            private string dataTableName = "data";
            public string DataTableName
            {
                get { return dataTableName.Trim(); }
                set { dataTableName = value.Trim(); }
            }

            private System.Text.Encoding myEncoder = System.Text.Encoding.Default;
            public System.Text.Encoding MyEncoder
            {
                get { return myEncoder; }
                set { myEncoder = value; }                
            }
    

        #endregion
        public SeDBClient(DBSocketServerType dbst)
        {
            dbstServer = dbst;
        }
        public SeDBClient()
        {
            dbstServer = DBSocketServerType.dbsstNormal;
        }

        #region 公共方法
            public bool ReadConfigInfo(string sFile ,out string sErr)
            {
                bool bOK = false;
                sErr = "";
                string sF = sFile;
                if (sF.Trim() == "")
                {
                    sF = Application.StartupPath + "\\AppConfig.xml";
                }
                if (!System.IO.File.Exists(sF))
                {
                    sErr = "文件：" +sFile + "  不存在，读取配置信息出错！";
                    return false;
                }
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    string sX = "";
                    xmlDoc.Load(sF);
                    XmlNode ndConfig = null;
                    ndConfig = xmlDoc.SelectSingleNode("config/remoteserver");
                    if (ndConfig != null)
                    {
                        if (ndConfig.Attributes["server"] != null)
                        {
                            sX = ndConfig.Attributes["server"].Value.Trim();
                            serverAddress = sX;
                        }

                        if (ndConfig.Attributes["port"] != null)
                        {
                            sX = ndConfig.Attributes["port"].Value.Trim();
                            if (sX.Trim().Length > 0)
                            {
                                int iX = int.Parse(sX.Trim());
                                port = iX;
                            }                        
                        }

                        if (ndConfig.Attributes["servertype"] != null)
                        {
                            sX = ndConfig.Attributes["servertype"].Value.Trim();
                            if (sX.Trim().Length > 0)
                            {
                                int iX = int.Parse(sX.Trim());
                                dbstServer = (DBSocketServerType)iX;
                            }
                        }

                        if (ndConfig.Attributes["buffersize"] != null)
                        {
                            sX = ndConfig.Attributes["buffersize"].Value.Trim();
                            if (sX.Trim().Length > 0)
                            {
                                int iX = int.Parse(sX.Trim());
                                recevieBufferSize = iX;
                            }
                        }
                        if (ndConfig.Attributes["myencoding"] != null)
                        {
                            sX = ndConfig.Attributes["myencoding"].Value.Trim();
                            if (sX.Trim().Length > 0)
                            {
                                myEncoder = System.Text.Encoding.GetEncoding(sX);
                            }
                        }
                        bOK = true;
                    }
                    else
                    {
                        sErr = "配置文件里找不到 config/remoteserver 路径的节点，读取配置数据出错！";
                        bOK = false;
                    }
                }
                catch (Exception err)
                {
                    sErr = err.Message;
                    bOK = false;
                }
                return bOK;
            }
            public bool ReadConfigInfo( out string sErr)
            {
                bool bOK = false;
                sErr = "";
                string sFile = cfgFile;
                bOK = ReadConfigInfo(sFile, out sErr);
                return bOK;
            }

            public bool SaveConfigInfoToFile(string sFile, string sServer,int iPort,DBSocketServerType svrType, int iBufferSize, System.Text.Encoding myEncoder, out string sErr)
            {
                bool bOK = false;
                sErr = "";
                XmlDocument xmlDoc = new XmlDocument();
                XmlWriter xmlwrt = XmlWriter.Create(sFile);
                
                try
                {
                    string sX = "";
                    xmlDoc.Load(sFile);
                    XmlNode ndConfig = null;
                    ndConfig = xmlDoc.SelectSingleNode("config/remoteserver");
                    if (ndConfig != null)
                    {
                        if (ndConfig.Attributes["server"] != null)
                        {
                            ndConfig.Attributes["server"].Value = sServer;
                        }

                        if (ndConfig.Attributes["port"] != null)
                        {
                            ndConfig.Attributes["port"].Value = iPort.ToString();
                        }

                        if (ndConfig.Attributes["servertype"] != null)
                        {
                            ndConfig.Attributes["servertype"].Value = ((int)svrType).ToString();
                        }

                        if (ndConfig.Attributes["buffersize"] != null)
                        {
                            ndConfig.Attributes["buffersize"].Value = iBufferSize.ToString();
                        }
                        if (ndConfig.Attributes["myencoding"] != null)
                        {
                            ndConfig.Attributes["myencoding"].Value = myEncoder.HeaderName;
                        }
                        bOK = true;
                    }
                    else
                    {
                        sErr = "配置文件里找不到 config/remoteserver 路径的节点，读取配置数据出错！";
                        bOK = false;
                    }
                }
                catch (Exception err)
                {
                    sErr = err.Message;
                    bOK = false;
                }
                return bOK;
            }


            public DataSet GetDataSet(DBCommInfo.DBSQLCommandInfo cmdInfo, out string sErr)
            {
                bool bOK = false;
                DataSet dsResult = null;
                sErr = "";

                dataTableName = cmdInfo.DataTableName;
                dsResult = GetDataSet(null, cmdInfo, false, out sErr);
                return dsResult;
            }
            public DataSet GetDataSet(Socket sct, DBCommInfo.DBSQLCommandInfo cmdInfo, bool bIsSaveDataXmlFile, out string sErr)
            {
                bool bOK = false;
                DataSet dsResult = null;
                sErr = "";
                bOK = ReadConfigInfo(out sErr);
                if (!bOK)
                {
                    sErr = "读取客户端配置文件出错";
                    return null;
                }
                dataTableName = cmdInfo.DataTableName;
                MemoryStream ms = null;
                ms = new MemoryStream();
                bOK = cmdInfo.GetSqlXMLText(out ms, out sErr);
                if (bOK)
                {
                    if (dbstServer == DBSocketServerType.dbsstNormal)
                    {
                        dsResult = GetDataFromNormalSvr(sct, ms, bIsSaveDataXmlFile, out sErr);
                    }
                    else
                    {
                        dsResult = GetDataFromDotNetSvr(sct, ms, bIsSaveDataXmlFile, out sErr);
                    }
                }
                return dsResult;
            }
            public DataSet GetDataSet(DBCommInfo.DBSQLCommandInfo cmdInfo, bool bIsSaveDataXmlFile, out string sErr)
            {
                sErr = "";
                return (GetDataSet(null, cmdInfo, bIsSaveDataXmlFile, out sErr));
            }

            private DataSet GetDataFromNormalSvr(MemoryStream msSql, bool bIsSaveDataXml, out string sErr)
            {
                sErr = "";
                return (GetDataFromNormalSvr(null, msSql, bIsSaveDataXml, out sErr));
            }
            private DataSet GetDataFromNormalSvr(Socket sct, MemoryStream msSql, bool bIsSaveDataXml, out string sErr)
            {

                DataSet dsX = null;
                bool bOK = false;
                sErr = "";
                byte[] bArr = null;
                MemoryStream ms = null;
                MemoryStream msDes = null;
                System.IO.FileStream fs = null;
                int iX = 0;
                long iPos = 0;
                if (msSql == null || msSql.Length == 0)
                {
                    return dsX;
                }
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    //
                    msSql.Position = 0;
                    fs = new System.IO.FileStream("cltSqlCmd.xml", System.IO.FileMode.Create);
                    fs.Write(msSql.ToArray(), 0, (int)msSql.Length);
                    fs.Close();
                    //fs.Dispose();
                    msSql.Position = 0;
                    if (sct == null)
                    {
                        sct = SocketClient.ConnectSocket(serverAddress, port);
                    }
                    if (!sct.Connected)
                    {
                        sct = SocketClient.ConnectSocket(serverAddress, port);
                    }
                    if (sct != null && sct.Connected)
                    {
                        bOK = SocketClient.SocketSend(sct, msSql, out sErr);
                        if (bOK)
                        {
                            //System.Threading.Thread.Sleep(500);
                            bOK = SocketClient.SocketReceiveData(sct, "</result>\n\n", recevieBufferSize, out ms, out sErr);
                            if (bIsSaveDataXml)
                            {
                                if (ms != null && ms.Length > 0)
                                {
                                    ms.Position = 0;
                                    fs = new System.IO.FileStream("cltDataOld.xml", System.IO.FileMode.Create);
                                    fs.Write(ms.ToArray(), 0, (int)ms.Length);
                                    fs.Close();
                                    //fs.Dispose();
                                }
                            }
                            if (bOK)
                            {
                                if (ms != null && ms.Length > 0)
                                {
                                    bArr = new byte[1024];
                                    if (bIsSaveDataXml)
                                    {
                                        fs = new System.IO.FileStream("cltDataNew.xml", System.IO.FileMode.Create);
                                    }
                                    ms.Position = 0;
                                    //iX = ms.Read(bArr, 0, bArr.Length);
                                    iX = 1;
                                    //iPos = ms.Position;
                                    if (ms.Length > 0)
                                    {
                                        string sText = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                                        iPos = Encoding.UTF8.GetBytes(sText.Trim()).Length;
                                        ms.Position = iPos;// sText.Length;
                                        //sText = Encoding.UTF8.GetString(bArr, 0, iX);
                                        sText = sText.Replace("\"utf-8\"", "\"gb2312\"");
                                        msDes = new System.IO.MemoryStream();
                                        //iPos = msDes.Position;

                                        msDes.Write(Encoding.UTF8.GetBytes(sText.Trim()), 0, Encoding.UTF8.GetBytes(sText.Trim()).Length);
                                        if (bIsSaveDataXml)
                                        {
                                            fs.Write(Encoding.UTF8.GetBytes(sText.Trim()), 0, Encoding.UTF8.GetBytes(sText.Trim()).Length);
                                        }
                                        //ms.Position = iX;
                                        bArr = null;
                                        bArr = new byte[recevieBufferSize];
                                        iX = ms.Read(bArr, 0, bArr.Length);
                                        while (iX > 0)
                                        {
                                            //iPos = msDes.Position;
                                            msDes.Write(bArr, 0, iX);
                                            if (bIsSaveDataXml)
                                            {
                                                fs.Write(bArr, 0, iX);
                                            }
                                            //iPos = msDes.Position;
                                            iX = ms.Read(bArr, 0, bArr.Length);
                                            //iPos = ms.Position;
                                        }
                                        msDes.Position = 0;
                                        if (bIsSaveDataXml)
                                        {
                                            fs.Close();
                                            //fs.Dispose();
                                        }
                                        dsX = XmlToDataSet(msDes, out sErr);
                                        //dsX = XmlToDataSet(ms, out sErr);
                                        msDes.Close();
                                        //msDes.Dispose();
                                    }
                                }
                            }
                        }
                    }
                    if (ms != null)
                    {
                        ms.Close();
                        //ms.Dispose();
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                return dsX;
            }
            private DataSet GetDataFromNormalSvr(Socket sct, MemoryStream msSql, out string sErr)
            {

                DataSet dsX = null;
                bool bOK = false;
                sErr = "";
                dsX = GetDataFromNormalSvr(sct, msSql, false, out sErr);
                return dsX;
            }

            private DataSet GetDataFromDotNetSvr(Socket sct, MemoryStream msSql, bool bIsSaveDataXml, out string sErr)
            {

                DataSet dsX = null;
                bool bOK = false;
                long iDataSize = 0;
                sErr = "";
                byte[] bArr = null;
                MemoryStream ms = null;
                MemoryStream msDes = null;
                System.IO.FileStream fs = null;
                int iX = 0;
                long iPos = 0;
                if (msSql == null || msSql.Length == 0)
                {
                    return dsX;
                }
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (sct == null)
                    {
                        sct = SocketClient.ConnectSocket(serverAddress, port);
                    }
                    if (!sct.Connected)
                    {
                        sct = SocketClient.ConnectSocket(serverAddress, port);
                    }
                    if (sct != null && sct.Connected)
                    {
                        bOK = SocketClient.SocketSend(sct, msSql, out sErr);
                        if (bOK)
                        {
                            do
                            {
                                if (sct.Available > 0)
                                    break;
                            } while (sct.Available == 0);
                            bOK = SocketClient.SocketReceiveData(sct, iDataSize, recevieBufferSize, out ms, out sErr);
                            if (sct.Connected)
                                sct.Close();
                            if (bIsSaveDataXml)
                            {
                                if (ms != null && ms.Length > 0)
                                {
                                    ms.Position = 0;
                                    fs = new System.IO.FileStream("cltDotNetData.xml", System.IO.FileMode.OpenOrCreate);
                                    fs.Write(ms.ToArray(), 0, (int)ms.Length);
                                    fs.Close();
                                    //fs.Dispose();
                                }
                            }
                            if (bOK)
                            {
                                if (ms != null && ms.Length > 0)
                                {

                                    ms.Position = 0;

                                    System.Xml.Serialization.XmlSerializer xmlS = new System.Xml.Serialization.XmlSerializer(typeof(DataSet));
                                    dsX = (DataSet)xmlS.Deserialize(ms);
                                    if (dsX.Tables.Count > 1)
                                    {
                                        dsX.Tables[1].TableName = dataTableName;
                                    }

                                    ms.Close();
                                    //ms.Dispose();
                                }
                            }
                            ////先获取大小
                            //bOK = SocketClient.SocketReceiveData(sct, -1, 1024, out ms, out sErr);// -1 一次性读取完
                            //if (bOK)
                            //{
                            //    string sTT = Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
                            //    if (sTT.Trim().Length > 0)
                            //    {
                            //        iDataSize = long.Parse(sTT.Trim());
                            //    }
                            //    bOK = SocketClient.SocketSend(sct, ">>ok", out sErr);
                            //    //
                            //    bOK = SocketClient.SocketReceiveData(sct, iDataSize, recevieBufferSize, out ms, out sErr);
                            //    if (bIsSaveDataXml)
                            //    {
                            //        if (ms != null && ms.Length > 0)
                            //        {
                            //            ms.Position = 0;
                            //            fs = new System.IO.FileStream("cltDotNetData.xml", System.IO.FileMode.OpenOrCreate);
                            //            fs.Write(ms.ToArray(), 0, (int)ms.Length);
                            //            fs.Close();
                            //            //fs.Dispose();
                            //        }
                            //    }
                            //    if (bOK)
                            //    {
                            //        if (ms != null && ms.Length > 0)
                            //        {

                            //            ms.Position = 0;

                            //            System.Xml.Serialization.XmlSerializer xmlS = new System.Xml.Serialization.XmlSerializer(typeof(DataSet));
                            //            dsX = (DataSet)xmlS.Deserialize(ms);
                            //            if (dsX.Tables.Count > 1)
                            //            {
                            //                dsX.Tables[1].TableName = dataTableName;
                            //            }

                            //            ms.Close();
                            //            //ms.Dispose();
                            //        }
                            //    }
                            //}

                        }
                    }
                    if (ms != null)
                    {
                        ms.Close();
                        //ms.Dispose();
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                return dsX;
            }
            private DataSet GetDataFromDotNetSvr(Socket sct, MemoryStream msSql, out string sErr)
            {

                DataSet dsX = null;
                bool bOK = false;
                sErr = "";
                dsX = GetDataFromDotNetSvr(sct, msSql, false, out sErr);
                return dsX;
            }
            private DataSet GetDataFromDotNetSvr(MemoryStream msSql, out string sErr)
            {
                sErr = "";
                return (GetDataFromDotNetSvr(null, msSql, out sErr));
            }

            public DataSet XmlToDataSet(MemoryStream msData, out string sErr)
            {
                DataSet dsX = null;
                XmlDocument doc = null;
                XmlElement root = null;
                sErr = "";
                DataTable dt0 = null;
                if (msData == null || msData.Length == 0)
                {
                    sErr = "数据流为空，转DataSet 失败！";
                    return dsX;
                }
                doc = new XmlDocument();
                doc.Load(msData);
                root = doc.DocumentElement;
                dt0 = new DataTable("result");
                dt0.Columns.Add("returncode");
                string returncode = GetNodeValue(root, "/return", "/returncode");
                dt0.Rows.Add(returncode);
                int count = GetNodeCount(root, "/return/resultset/fields", "td");
                DataTable dt;
                dt = new DataTable(dataTableName);
                //dt.Clear();
                for (int i = 1; i <= count; i++)
                {
                    string xpath1 = "/td[" + i + "]";
                    string name1 = GetNodeValue(root, "/return/resultset/fields", xpath1);
                    dt.Columns.Add(name1);
                }
                int countTr = GetNodeCount(root, "/return/resultset", "tr");
                for (int j = 1; j <= countTr; j++)
                {
                    string xpath2 = "/return/resultset/tr[" + j + "]";
                    object[] ob = new object[count];
                    for (int k = 1; k <= count; k++)
                    {
                        string xpath3 = "/td[" + k + "]";
                        string value = GetNodeValue(root, xpath2, xpath3);
                        ob[k - 1] = value;
                    }
                    dt.Rows.Add(ob);
                }
                dsX = new DataSet();
                dsX.Tables.Add(dt0);
                dsX.Tables.Add(dt);
                return dsX;
            }
            public static string GetNodeValue(System.Xml.XmlElement root, string prefixPath, string xRelativePath)
            {
                //if (doc == null)
                //{
                //    LoadXmlFile(XmlFileInfo);
                //}
                //LoadXmlFile();
                string xPath = string.Empty;
                if (!string.IsNullOrEmpty(xRelativePath))
                {
                    if (!string.IsNullOrEmpty(prefixPath))
                    {
                        xPath = prefixPath + xRelativePath;
                    }
                    else
                    {
                        xPath = xRelativePath;
                    }
                }
                //xPath = xPath.Replace("/", "/ns:");
                XmlNode node = root.SelectSingleNode(xPath);
                if (node == null)
                {
                    return null;
                }
                return node.InnerXml;
            }
            public static int GetNodeCount(System.Xml.XmlElement root, string prefixPath, string xRelativePath)
            {
                //LoadXmlFile();
                string xPath = prefixPath;
                //xPath = xPath.Replace("/", "/ns:");
                XmlNode node = root.SelectSingleNode(xPath);
                int count = 0;
                if (node == null) return 0;
                foreach (XmlElement tr in node.ChildNodes)
                {
                    if (tr.Name == xRelativePath)
                        count++;
                }
                return count;
            }
        #endregion 
    }
}
