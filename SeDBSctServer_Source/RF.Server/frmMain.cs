namespace RF.Server
{
    using App;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Data.OracleClient;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Zdx.LogManage;
    using DBBase;
    using DBCommInfo;
    using Zqm.Xml;
    using Zqm;

    public class frmMain : Form
    {
        private WMSAppInfo AInfo = new WMSAppInfo();
        private Button btn_ClearText;
        private Button btnMeun;
        private Button btnRun;
        private Button btnSave;
        private Button btnText;
        private CheckBox chk_Tranc;
        private IContainer components = null;
        private byte[] Data = new byte[0x400];
        private SaveFileDialog dlgSave;
        private string FileConfig = "";
        private GroupBox grpState;
        private int iClearText = 10;
        private int iFormH0 = 0x8a;
        private int iFormH1 = 0x13a;
        private int iFormW0 = 0x17a;
        private int iFormW1 = 0x17a;
        private ImageList imgList;
        private string ipAddress = "";
        private bool isCompress = false;
        private bool isServerRun = false;
        private Label label1;
        private Label label2;
        private Label lblDBName;
        private Label lblLocalIp;
        private Label lblServerHost;
        private Label lblState;
        private string LogFile = "svrErrLog.txt";
        private TcpListener objListener = null;
        private Panel pnl_InInfo;
        private int port = 0;
        private string sDBName = "";
        private string sDBPwd = "";
        private string sDBSvr = "";
        private string sDBUser = "";
        private int serializerType = 0;
        private string serverHostName = "";
        private IPAddress serverIp = null;
        private List<Socket> SktCltLIst = new List<Socket>();
        private Socket SktServer = null;
        private Thread thrdListener = null;
        private Thread thrdSktMain = null;
        private System.Windows.Forms.Timer tmrMain;
        private ToolTip toolTip;
        private TextBox txtClearText;
        private TextBox txtText;

        public frmMain()
        {
            this.InitializeComponent();
        }

        private void btn_ClearText_Click(object sender, EventArgs e)
        {
            this.txtText.Text = "";
        }

        private void btnMeun_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (this.btnRun.Tag.ToString() == "0")
            {
                this.btnRun.Text = "停止";
                this.btnRun.Tag = 1;
                try
                {
                    if (!this.isServerRun)
                    {
                        this.objListener.Start();
                    }
                    if (this.thrdListener.ThreadState == ThreadState.Suspended)
                    {
                        this.thrdListener.Resume();
                    }
                    this.isServerRun = true;
                }
                catch (SocketException exception)
                {
                    new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                    this.isServerRun = false;
                    this.btnRun.Text = "启动";
                    this.btnRun.Tag = 0;
                    MessageBox.Show(exception.Message);
                    this.lblState.Text = "服务停止";
                    return;
                }
                if (this.isServerRun)
                {
                    this.lblState.Text = "服务运行中...";
                }
            }
            else
            {
                this.btnRun.Text = "启动";
                this.btnRun.Tag = 0;
                this.lblState.Text = "服务已停止";
                if (this.thrdListener.ThreadState == ThreadState.Running)
                {
                    this.thrdListener.Suspend();
                }
                this.objListener.Stop();
                this.isServerRun = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.savelog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ： 测试", this.LogFile);
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            if ((base.Size.Height == this.iFormH0) && (base.Size.Width == this.iFormW0))
            {
                base.Height = this.iFormH1;
                base.Width = this.iFormW1;
                this.btnText.Text = "<<";
                this.toolTip.SetToolTip(this.btnText, "收拢");
            }
            else
            {
                base.Height = this.iFormH0;
                base.Width = this.iFormW0;
                this.btnText.Text = ">>";
                this.toolTip.SetToolTip(this.btnText, "展开");
            }
        }

        private void chk_Tranc_CheckedChanged(object sender, EventArgs e)
        {
            this.btn_ClearText.Visible = this.chk_Tranc.Checked;
        }

        private void chkCompress_Click(object sender, EventArgs e)
        {
        }

        private void cmbSerializer_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private MemoryStream DataSetToPHPStream(DBSQLCommandInfo cmdInfo, DataSet ds)
        {
            DataTable table = null;
            MemoryStream output = new MemoryStream();
            string str = "0";
            StringBuilder builder = new StringBuilder("");
            byte[] bytes = null;
            BinaryWriter writer = new BinaryWriter(output);
            bytes = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            writer.Write(bytes);
            bytes = null;
            bytes = Encoding.UTF8.GetBytes("<return>\n");
            writer.Write(bytes);
            table = ds.Tables["result"];
            if ((table != null) && (table.Rows.Count > 0))
            {
                str = table.Rows[0]["returncode"].ToString();
            }
            bytes = null;
            bytes = Encoding.UTF8.GetBytes("<returncode>" + str + "</returncode>\n");
            writer.Write(bytes);
            bytes = null;
            bytes = Encoding.UTF8.GetBytes("<resultset>\n");
            writer.Write(bytes);
            table = ds.Tables["data"];
            if (table != null)
            {
                bytes = null;
                bytes = Encoding.UTF8.GetBytes("<fields>\n");
                writer.Write(bytes);
                builder.Remove(0, builder.Length);
                foreach (DataColumn column in table.Columns)
                {
                    builder.Append("<td type=\"" + column.DataType.ToString().Replace("System.", "") + "\">" + column.ColumnName + "</td> ");
                }
                bytes = null;
                bytes = Encoding.UTF8.GetBytes(builder.ToString() + "\n");
                writer.Write(bytes);
                bytes = null;
                bytes = Encoding.UTF8.GetBytes("</fields>\n");
                writer.Write(bytes);
                foreach (DataRow row in table.Rows)
                {
                    bytes = null;
                    bytes = Encoding.UTF8.GetBytes("<tr>\n");
                    writer.Write(bytes);
                    builder.Remove(0, builder.Length);
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        object obj2 = row[i];
                        string str2 = "";
                        if (obj2 != null)
                        {
                            str2 = obj2.ToString();
                        }
                        builder.Append("<td>" + str2 + "</td>");
                    }
                    builder.Append(" </tr>");
                    bytes = null;
                    bytes = Encoding.UTF8.GetBytes(builder.ToString() + "\n");
                    writer.Write(bytes);
                    builder.Remove(0, builder.Length);
                }
            }
            bytes = null;
            bytes = Encoding.UTF8.GetBytes("</resultset>\n");
            writer.Write(bytes);
            bytes = null;
            bytes = Encoding.UTF8.GetBytes("</return>\n\n");
            writer.Write(bytes);
            writer.Flush();
            FileStream stream2 = new FileStream("svrPHPData.XML", FileMode.Create);
            stream2.Write(output.ToArray(), 0, (int) output.Length);
            stream2.Close();
            stream2.Dispose();
            return output;
        }

        private MemoryStream DataSetToStream(DBSQLCommandInfo cmdInfo, DataSet dsX)
        {
            MemoryStream stream = new MemoryStream();
            if (cmdInfo.FromSysType.ToLower().IndexOf("php") > 0)
            {
                return this.DataSetToPHPStream(cmdInfo, dsX);
            }
            new XmlSerializer(typeof(DataSet)).Serialize((Stream) stream, dsX);
            return stream;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoServerReceicedData(TcpClient client, NetworkStream nstrm, MemoryStream mmX)
        {
            string sErr = "";
            if ((mmX != null) && (mmX.Length != 0L))
            {
                this.SetTextInfo(false, "客户端：" + client.Client.LocalEndPoint.ToString());
                mmX.Position = 0L;
                byte[] bBuff = mmX.GetBuffer();
                if (this.chk_Tranc.Checked)
                {
                    this.savelog(bBuff, "SvrRecCmd.txt");
                }
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();
                if (!cmdInfo.LoadSQLXmlCmd(mmX, out sErr))
                {
                    this.SetTextInfo(false, "命令对象加载客户端请求命令数据出错：" + sErr);
                    this.savelog("命令对象加载客户端请求命令数据出错：" + sErr);
                }
                else
                {
                    this.SetTextInfo(false, "命令对象加载客户端请求命令数据成功(" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ")");
                    this.SetTextInfo(false, "命令类型：" + cmdInfo.SqlType.ToString() + "  CommandText：" + cmdInfo.SqlText);
                    if (this.chk_Tranc.Checked)
                    {
                        this.savelog("命令类型：" + cmdInfo.SqlType.ToString() + "  CommandText：" + cmdInfo.SqlText);
                    }
                    MemoryStream stream = new MemoryStream();
                    DataSet ds = this.DoSqlCmd(this.AInfo, cmdInfo, out sErr);
                    this.SetTextInfo(false, "DoSqlCmd:" + sErr);
                    if (sErr.Length == 0)
                    {
                        sErr = " is OK";
                    }
                    if (this.chk_Tranc.Checked)
                    {
                        this.savelog("执行命令 DoSqlCmd" + sErr);
                    }
                    if (ds != null)
                    {
                        if (cmdInfo.FromSysType.ToLower().IndexOf("php") >= 0)
                        {
                            stream = this.DataSetToPHPStream(cmdInfo, ds);
                            if (nstrm.CanWrite && (stream != null))
                            {
                                nstrm.Write(stream.ToArray(), 0, (int) stream.Length);
                            }
                        }
                        else
                        {
                            stream = this.DataSetToStream(cmdInfo, ds);
                            long length = stream.Length;
                            byte[] buffer = null;
                            DateTime now = DateTime.Now;
                            if (nstrm.CanWrite)
                            {
                                buffer = new byte[client.SendBufferSize];
                                stream.Position = 0L;
                                while (stream.Position < stream.Length)
                                {
                                    int count = stream.Read(buffer, 0, buffer.Length);
                                    nstrm.Write(buffer, 0, count);
                                }
                            }
                            TimeSpan span = (TimeSpan) (DateTime.Now - now);
                            double totalSeconds = span.TotalSeconds;
                            this.SetTextInfo(false, " 数据传输大小:" + stream.Length.ToString() + " 共用时：" + totalSeconds.ToString() + " 秒钟！");
                        }
                    }
                    stream.Close();
                    stream.Dispose();
                    nstrm.Close();
                    stream.Dispose();
                }
            }
        }

        private void DoSocketClient(object sktX)
        {
            List<Socket> list;
            Socket item = (Socket) sktX;
            if (sktX == null)
            {
                lock ((list = this.SktCltLIst))
                {
                    this.SktCltLIst.Remove(item);
                }
            }
            else
            {
                this.SetTextInfo(false, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "接收线程正在处理... ");
                MemoryStream mmX = new MemoryStream();
                byte[] buffer = new byte[item.ReceiveBufferSize];
                bool flag = false;
                try
                {
                    while (!flag)
                    {
                        int count = 0;
                        if (item.Available > 0)
                        {
                            flag = true;
                            while (item.Available > 0)
                            {
                                count = item.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                                mmX.Write(buffer, 0, count);
                            }
                            this.SetTextInfo(false, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "接收线程收到 数据大小 ：" + count.ToString());
                        }
                        if (mmX.Length > 8L)
                        {
                            byte[] buffer2 = new byte[8];
                            int num2 = ((int) mmX.Length) - 8;
                            mmX.Position = num2;
                            mmX.Read(buffer2, 0, 8);
                            if (Encoding.UTF8.GetString(buffer2) != "</sql>\n\n")
                            {
                                flag = false;
                            }
                        }
                    }
                    this.SetTextInfo(false, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "接收到：" + item.LocalEndPoint.ToString() + " 的命令数据大小：" + mmX.Length.ToString());
                    if ((mmX != null) && (mmX.Length > 0L))
                    {
                        mmX.Position = 0L;
                        this.DoSocketReceicedData(item, mmX);
                    }
                }
                catch (SocketException exception)
                {
                    new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                }
                finally
                {
                    if (item.Connected)
                    {
                        item.Close();
                    }
                    lock ((list = this.SktCltLIst))
                    {
                        this.SktCltLIst.Remove(item);
                    }
                }
            }
        }

        private void DoSocketReceicedData(Socket skt, MemoryStream mmX)
        {
            string sErr = "";
            if ((mmX != null) && (mmX.Length != 0L))
            {
                try
                {
                    this.SetTextInfo(false, "  客户端：" + skt.LocalEndPoint.ToString());
                    mmX.Position = 0L;
                    if (this.chk_Tranc.Checked)
                    {
                        byte[] bBuff = mmX.GetBuffer();
                        this.savelog(bBuff, "SvrRecCmd.txt");
                    }
                    DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();
                    if (!cmdInfo.LoadSQLXmlCmd(mmX, out sErr))
                    {
                        this.SetTextInfo(false, "命令对象加载客户端请求命令数据出错：" + sErr);
                        this.savelog("命令对象加载客户端请求命令数据出错：" + sErr);
                    }
                    else
                    {
                        this.SetTextInfo(false, "命令对象加载客户端请求命令数据成功(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ")");
                        this.SetTextInfo(false, "命令类型：" + cmdInfo.SqlType.ToString() + "  CommandText：" + cmdInfo.SqlText);
                        if (this.chk_Tranc.Checked)
                        {
                            this.savelog("命令类型：" + cmdInfo.SqlType.ToString() + "  CommandText：" + cmdInfo.SqlText);
                        }
                        MemoryStream msDis = new MemoryStream();
                        DataSet ds = this.DoSqlCmd(this.AInfo, cmdInfo, out sErr);
                        this.SetTextInfo(false, "DoSqlCmd:" + sErr);
                        if (sErr.Length == 0)
                        {
                            sErr = " is OK";
                        }
                        if (this.chk_Tranc.Checked)
                        {
                            this.savelog("执行命令 DoSqlCmd" + sErr);
                        }
                        if (ds != null)
                        {
                            DateTime now;
                            int num;
                            double totalSeconds;
                            TimeSpan span;
                            if (cmdInfo.FromSysType.ToLower().IndexOf("php") >= 0)
                            {
                                msDis = this.DataSetToPHPStream(cmdInfo, ds);
                                now = DateTime.Now;
                                if ((msDis.Length > 0L) && skt.Poll(100, SelectMode.SelectWrite))
                                {
                                    num = 0;
                                    byte[] buffer = new byte[skt.SendBufferSize];
                                    msDis.Position = 0L;
                                    while (msDis.Position < msDis.Length)
                                    {
                                        num = msDis.Read(buffer, 0, buffer.Length);
                                        skt.Send(buffer, 0, num, SocketFlags.None);
                                    }
                                }
                                span = (TimeSpan) (DateTime.Now - now);
                                totalSeconds = span.TotalSeconds;
                                this.SetTextInfo(false, " 数据传输大小:" + msDis.Length.ToString() + " 共用时：" + totalSeconds.ToString() + " 秒钟！");
                            }
                            else
                            {
                                if (cmdInfo.FromSysType.ToLower().IndexOf("rf") >= 0)
                                {
                                    string str2 = DSetHelper.DataSetToTextStream(ds, out msDis);
                                }
                                else
                                {
                                    msDis = this.DataSetToStream(cmdInfo, ds);
                                }
                                now = DateTime.Now;
                                if (((msDis != null) && (msDis.Length > 0L)) && skt.Poll(100, SelectMode.SelectWrite))
                                {
                                    byte[] buffer3 = new byte[skt.SendBufferSize];
                                    msDis.Position = 0L;
                                    while (msDis.Position < msDis.Length)
                                    {
                                        num = msDis.Read(buffer3, 0, buffer3.Length);
                                        skt.Send(buffer3, 0, num, SocketFlags.None);
                                    }
                                }
                                span = (TimeSpan) (DateTime.Now - now);
                                totalSeconds = span.TotalSeconds;
                                this.SetTextInfo(false, " 数据传输大小:" + msDis.Length.ToString() + " 共用时：" + totalSeconds.ToString() + " 秒钟！");
                            }
                        }
                        msDis.Close();
                        msDis = null;
                        if (skt.Connected)
                        {
                            skt.Close();
                        }
                    }
                }
                catch (Exception exception)
                {
                    new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                }
            }
        }

        private DataSet DoSqlCmd(WMSAppInfo aInfo, DBSQLCommandInfo cmdInfo, out string sErr)
        {
            DataSet set = new DataSet();
            DataTable table = null;
            DataTable table2 = new DataTable("result");
            int nPageCount = 0;
            int num2 = 0;
            if (this.chk_Tranc.Checked)
            {
                this.SetTextInfo(false, "执行 MS SQL:" + cmdInfo.GetFullSql(), true);
            }
            table2.Columns.Add("returncode", System.Type.GetType("System.Int32"));
            table2.Columns.Add("returndesc", System.Type.GetType("System.String"));
            sErr = "";
            try
            {
                switch (aInfo.dbtApp)
                {
                    case DataBaseType.dbtMSSQL:
                        table = this.GetDataForMSSql(aInfo, cmdInfo, out sErr, out nPageCount);
                        goto Label_00E5;

                    case DataBaseType.dbtMSAccess:
                        goto Label_00E5;

                    case DataBaseType.dbtOracle:
                        table = this.GetDataForOracle(aInfo, cmdInfo, out sErr, out nPageCount);
                        goto Label_00E5;
                }
            }
            catch (Exception exception)
            {
                Zdx.LogManage.LogManage manage = new Zdx.LogManage.LogManage();
                manage.WriteErrorInfoToFile(exception);
                sErr = exception.Message;
                num2 = -1;
            }
        Label_00E5:
            if ((sErr != null) && (sErr.Trim().Length > 0))
            {
                num2 = -1;
            }
            DataRow row = table2.NewRow();
            row["returncode"] = num2;
            row["returndesc"] = sErr;
            table2.Rows.Add(row);
            table2.TableName = "result";
            set.Tables.Add(table2);
            if (table != null)
            {
                if (table.Rows.Count == 0)
                {
                    new Zdx.LogManage.LogManage().WriteErrorInfoToFile("ResultTable count:=0 Col count:" + table.Columns.Count.ToString());
                }
                set.Tables.Add(table);
            }
            set.AcceptChanges();
            if (num2 != 0)
            {
                this.SetTextInfo(false, "执行语句:" + cmdInfo.SqlText + " 出现异常：" + sErr, true);
            }
            else
            {
                this.SetTextInfo(false, " 数据结果：ReturnCode=" + num2.ToString() + " Desc:" + sErr);
            }
            if (table != null)
            {
                this.SetTextInfo(false, " 数据结果：DataSet: Table1=" + table2.TableName + "  Rows:" + table2.Rows.Count.ToString() + "  Table2:" + table.TableName + " Rows:" + table.Rows.Count.ToString());
                return set;
            }
            this.SetTextInfo(false, " 数据结果：DataSet: Table1=" + table2.TableName + "  Rows:" + table2.Rows.Count.ToString() + "  Table2 为空，可能语句为Update/Insert/Delete 或 不含返回集的存储过程!");
            return set;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isServerRun)
            {
                if (MessageBox.Show("服务正在运行中，您确定要退出吗？", this.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        if (this.isServerRun)
                        {
                            this.isServerRun = false;
                        }
                        if (this.thrdListener != null)
                        {
                            this.thrdListener.Abort();
                        }
                        if (this.objListener != null)
                        {
                            this.objListener.Stop();
                        }
                        if ((this.thrdSktMain != null) && (this.thrdSktMain.ThreadState == ThreadState.Running))
                        {
                            this.thrdSktMain.Abort();
                        }
                        if (this.SktCltLIst.Count > 0)
                        {
                            for (int i = this.SktCltLIst.Count - 1; i == 0; i--)
                            {
                                try
                                {
                                    if (this.SktCltLIst[i].Connected)
                                    {
                                        this.SktCltLIst[i].Close();
                                    }
                                }
                                finally
                                {
                                    this.SktCltLIst.Remove(this.SktCltLIst[i]);
                                }
                            }
                        }
                        if (this.SktServer.Connected)
                        {
                            this.SktServer.Close();
                        }
                    }
                    catch (SocketException exception)
                    {
                        new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                    }
                }
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                this.chk_Tranc.Visible = !this.chk_Tranc.Visible;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bool flag = false;
            base.Width = this.iFormW0;
            base.Height = this.iFormH0;
            string sNodeKeyFullPath = "configuration/Server";
            string sValue = "";
            this.FileConfig = Application.StartupPath + @"\wmsapp.config";
            if (System.IO.File.Exists(this.FileConfig))
            {
                Exception exception;
                XmlWriteReader reader = new XmlWriteReader();
                try
                {
                    flag = reader.OpenXMLFile(this.FileConfig);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    Zdx.LogManage.LogManage manage = new Zdx.LogManage.LogManage();
                    manage.WriteErrorInfoToFile(exception);
                }
                if (flag)
                {
                    this.ipAddress = "";
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "ip", out this.ipAddress);
                    this.GetLocalIp(this.ipAddress);
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "port", out sValue);
                    switch (sValue)
                    {
                        case "":
                            sValue = "8100";
                            break;
                    }
                    this.port = int.Parse(sValue);
                    reader.SetNodeAtrribeValue(sNodeKeyFullPath, "ip", this.ipAddress);
                    reader.SetNodeAtrribeValue(sNodeKeyFullPath, "host", this.serverHostName);
                    sNodeKeyFullPath = "configuration/DBSet";
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "dbType", out sValue);
                    this.AInfo.dbtApp = (DataBaseType) int.Parse(sValue);
                    this.lblDBName.Text = "数据库类型：" + this.AInfo.dbtApp.ToString();
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "dbSvr", out this.sDBSvr);
                    this.lblDBName.Text = this.lblDBName.Text + "　　服务器名：" + this.sDBSvr;
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "dbName", out this.sDBName);
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "dbUser", out this.sDBUser);
                    reader.GetNodeAtrribeValue(sNodeKeyFullPath, "dbPwd", out this.sDBPwd);
                    this.lblLocalIp.Text = "IP：" + this.ipAddress + "  Port：" + this.port.ToString();
                    this.lblServerHost.Text = "名称：" + this.serverHostName;
                    base.Update();
                    sValue = DBOptrForComm.ConnOpen(this.AInfo.dbtApp, this.AInfo.AppConn, this.sDBSvr, this.sDBName.Trim(), this.sDBUser.Trim(), this.sDBPwd);
                    if ((sValue != "0") || (this.serverIp == null))
                    {
                        this.savelog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + sValue, this.LogFile);
                        MessageBox.Show(sValue);
                    }
                    else
                    {
                        IPEndPoint localEP = new IPEndPoint(this.serverIp, this.port);
                        if (sValue == "0")
                        {
                            try
                            {
                                this.SktServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                this.SktServer.Bind(localEP);
                                this.SktServer.Listen(50);
                                this.thrdSktMain = new Thread(new ParameterizedThreadStart(this.ThreadSckSvr));
                                this.thrdSktMain.IsBackground = true;
                                this.thrdSktMain.Start(this.SktServer);
                                this.isServerRun = true;
                                if (this.isServerRun)
                                {
                                    this.btnRun.Text = "停止";
                                    this.lblState.Text = "服务运行中...";
                                    base.WindowState = FormWindowState.Minimized;
                                }
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                                new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                            }
                        }
                    }
                }
            }
        }

        private string GetCmdSqlText(DbCommand cmdX)
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append(cmdX.CommandText);
            try
            {
                if (cmdX.CommandType == CommandType.StoredProcedure)
                {
                    builder.Append("(");
                    foreach (DbParameter parameter in cmdX.Parameters)
                    {
                        if (parameter.DbType != DbType.Object)
                        {
                            if (parameter.Direction == ParameterDirection.Output)
                            {
                                builder.Append(" out " + parameter.DbType.ToString() + " " + parameter.ParameterName + ",");
                            }
                            else
                            {
                                builder.Append(string.Concat(new object[] { parameter.DbType.ToString(), " ", parameter.ParameterName, "=", parameter.Value, "," }));
                            }
                        }
                    }
                    builder.Append(")");
                }
            }
            catch (Exception exception)
            {
                builder.Append("  错误：" + exception.Message);
                new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
            }
            return builder.ToString();
        }

        private DataTable GetDataForMSSql(WMSAppInfo aInfo, DBSQLCommandInfo cmdInfo, out string sErr, out int nPageCount)
        {
            DataTable table = new DataTable(cmdInfo.DataTableName);
            SqlCommand selectCommand = null;
            SqlDataAdapter adapter = null;
            nPageCount = 0;
            sErr = "";
            int pageSize = 0;
            int nPageSize = 0;
            int num3 = 0;
            int num4 = 0;
            pageSize = cmdInfo.PageSize;
            nPageSize = cmdInfo.PageIndex;
            if (nPageSize > 0)
            {
                num3 = (pageSize * (nPageSize - 1)) + 1;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            num4 = pageSize * nPageSize;
            using (SqlConnection connection = new SqlConnection(DBBase.GetConnectionString(this.AInfo.dbtApp, this.sDBSvr, this.sDBName, this.sDBUser, this.sDBPwd, false)))
            {
                Zdx.LogManage.LogManage manage;
                if (connection == null)
                {
                    sErr = "数据连接对象未实例化，获取数据失败！";
                    return null;
                }
                selectCommand = this.GetSqlCmd(cmdInfo, out sErr);
                if (selectCommand == null)
                {
                    sErr = "生成命令对象出错，获取数据失败！";
                    return null;
                }
                if (connection.State == ConnectionState.Closed)
                {
                    string connectionString = "";
                    try
                    {
                        connection.Open();
                        manage = new Zdx.LogManage.LogManage();
                        manage.WriteErrorInfoToFile(" Conn Open :" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ms"));
                    }
                    catch (SqlException exception)
                    {
                        Zdx.LogManage.LogManage manage2 = new Zdx.LogManage.LogManage();
                        manage2.WriteErrorInfoToFile(exception);
                        if (connection.ConnectionString != string.Empty)
                        {
                            connectionString = connection.ConnectionString;
                        }
                        sErr = "获取数据失败，打开连接时出错：" + exception.Message + " conStr:" + connectionString;
                        return null;
                    }
                }
                if (selectCommand != null)
                {
                    selectCommand.Connection = connection;
                    adapter = new SqlDataAdapter(selectCommand);
                    try
                    {
                        DataSet dataSet = null;
                        dataSet = new DataSet();
                        if (num3 > 0)
                        {
                            num3--;
                        }
                        adapter.Fill(dataSet, 0, 0, cmdInfo.DataTableName);
                        int nRows = 0;
                        if (dataSet.Tables[cmdInfo.DataTableName] != null)
                        {
                            nRows = dataSet.Tables[cmdInfo.DataTableName].Rows.Count;
                        }
                        if (nRows > 0)
                        {
                            nPageCount = this.GetPageCount(nPageSize, nRows);
                            if (dataSet.Tables[cmdInfo.DataTableName] != null)
                            {
                                if (dataSet.Tables[cmdInfo.DataTableName].Rows.Count > 0)
                                {
                                    table = dataSet.Tables[cmdInfo.DataTableName].Clone();
                                }
                                else
                                {
                                    table = dataSet.Tables[cmdInfo.DataTableName].Copy();
                                }
                            }
                        }
                        if (num4 > 0)
                        {
                            if (nRows < num4)
                            {
                                num4 = nRows;
                            }
                            if (num3 > nRows)
                            {
                                num3 = 0;
                            }
                            for (int i = num3; i < num4; i++)
                            {
                                DataRow row = table.NewRow();
                                int count = table.Columns.Count;
                                for (int j = 0; j < count; j++)
                                {
                                    row[j] = dataSet.Tables[cmdInfo.DataTableName].Rows[i][j];
                                }
                                table.Rows.Add(row);
                            }
                        }
                        else if (dataSet.Tables[cmdInfo.DataTableName] != null)
                        {
                            table = dataSet.Tables[cmdInfo.DataTableName].Copy();
                        }
                        dataSet.Clear();
                        if (table != null)
                        {
                            table.TableName = cmdInfo.DataTableName;
                        }
                        adapter.Dispose();
                        selectCommand.Dispose();
                        connection.Close();
                        manage = new Zdx.LogManage.LogManage();
                        manage.WriteErrorInfoToFile(" Conn normal close :" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ms"));
                    }
                    catch (Exception exception2)
                    {
                        new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception2);
                        sErr = string.Concat(new object[] { exception2.Message, exception2.Message, "\r\n", exception2.TargetSite, ":", exception2.StackTrace });
                        if (adapter != null)
                        {
                            adapter.Dispose();
                        }
                        if (selectCommand != null)
                        {
                            selectCommand.Dispose();
                        }
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                            new Zdx.LogManage.LogManage().WriteErrorInfoToFile(" Conn unnormal close :" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ms"));
                        }
                    }
                }
            }
            return table;
        }

        private DataTable GetDataForOleDb(WMSAppInfo aInfo, DBSQLCommandInfo cmdInfo, out string sErr, out int nPageCount)
        {
            DataTable table = new DataTable(cmdInfo.DataTableName);
            OleDbCommand selectCommand = null;
            OleDbDataAdapter adapter = null;
            nPageCount = 0;
            sErr = "";
            int pageSize = 0;
            int nPageSize = 0;
            int num3 = 0;
            int num4 = 0;
            pageSize = cmdInfo.PageSize;
            nPageSize = cmdInfo.PageIndex;
            if (nPageSize > 0)
            {
                num3 = (pageSize * (nPageSize - 1)) + 1;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            num4 = pageSize * nPageSize;
            using (OleDbConnection connection = new OleDbConnection(DBBase.GetConnectionString(this.AInfo.dbtApp, this.sDBSvr, this.sDBName, this.sDBUser, this.sDBPwd, true)))
            {
                if (connection == null)
                {
                    sErr = "连接对象未被实例化，获取数据失败！";
                    return null;
                }
                selectCommand = this.GetOleDbCmd(cmdInfo, out sErr);
                if (selectCommand == null)
                {
                    sErr = "生成命令对象出错，获取数据失败！";
                    return null;
                }
                if (connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        string connectionString = connection.ConnectionString;
                        connection.Open();
                    }
                    catch (OleDbException exception)
                    {
                        Zdx.LogManage.LogManage manage = new Zdx.LogManage.LogManage();
                        manage.WriteErrorInfoToFile(exception);
                        sErr = "打开数据连接出错，获取数据失败： " + exception.Message;
                        return null;
                    }
                }
                if (selectCommand != null)
                {
                    selectCommand.Connection = connection;
                    adapter = new OleDbDataAdapter(selectCommand);
                    try
                    {
                        DataSet dataSet = null;
                        dataSet = new DataSet();
                        if (num3 > 0)
                        {
                            num3--;
                        }
                        adapter.Fill(dataSet, 0, 0, cmdInfo.DataTableName);
                        int count = dataSet.Tables[cmdInfo.DataTableName].Rows.Count;
                        nPageCount = this.GetPageCount(nPageSize, count);
                        table = dataSet.Tables[cmdInfo.DataTableName].Clone();
                        if (num4 > 0)
                        {
                            if (count < num4)
                            {
                                num4 = count;
                            }
                            if (num3 > count)
                            {
                                num3 = 0;
                            }
                            for (int i = num3; i < num4; i++)
                            {
                                table.Rows.Add(dataSet.Tables[cmdInfo.DataTableName].Rows[i]);
                            }
                        }
                        else
                        {
                            table = dataSet.Tables[cmdInfo.DataTableName].Copy();
                        }
                        dataSet.Clear();
                        if (table != null)
                        {
                            table.TableName = cmdInfo.DataTableName;
                        }
                        adapter.Dispose();
                        selectCommand.Dispose();
                    }
                    catch (Exception exception2)
                    {
                        new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception2);
                        sErr = exception2.Message;
                        if (adapter != null)
                        {
                            adapter.Dispose();
                        }
                        if (selectCommand != null)
                        {
                            selectCommand.Dispose();
                        }
                    }
                }
            }
            return table;
        }

        private DataTable GetDataForOracle(WMSAppInfo aInfo, DBSQLCommandInfo cmdInfo, out string sErr, out int nPageCount)
        {
            Exception exception2;
            DataTable table = new DataTable(cmdInfo.DataTableName);
            OracleCommand cmdX = null;
            OracleDataAdapter adapter = null;
            int num = 0;
            nPageCount = 0;
            sErr = "";
            int pageSize = 0;
            int nPageSize = 0;
            int num4 = 0;
            int num5 = 0;
            pageSize = cmdInfo.PageSize;
            nPageSize = cmdInfo.PageIndex;
            if (nPageSize > 0)
            {
                num4 = (pageSize * (nPageSize - 1)) + 1;
            }
            if (num4 < 0)
            {
                num4 = 0;
            }
            num5 = pageSize * nPageSize;
            string connectionString = DBBase.GetConnectionString(this.AInfo.dbtApp, this.sDBSvr, this.sDBName, this.sDBUser, this.sDBPwd, false);
            try
            {
                num = 1;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    if (connection == null)
                    {
                        sErr = "连接对象未被实例化，获取数据失败！";
                        return null;
                    }
                    num = 2;
                    cmdX = this.GetOracleCmd(cmdInfo, out sErr);
                    if (this.chk_Tranc.Checked)
                    {
                        this.savelog(cmdX.CommandText, "SvrExecCmd.txt");
                    }
                    if (cmdX == null)
                    {
                        sErr = "生成命令对象出错，获取数据失败！";
                        return null;
                    }
                    num = 3;
                    if (this.chk_Tranc.Checked)
                    {
                        this.savelog(this.GetCmdSqlText(cmdX), "SvrExecCmd.txt");
                    }
                    if (connection.State == ConnectionState.Closed)
                    {
                        try
                        {
                            string str3 = connection.ConnectionString;
                            connection.Open();
                        }
                        catch (OracleException exception)
                        {
                            Zdx.LogManage.LogManage manage = new Zdx.LogManage.LogManage();
                            manage.WriteErrorInfoToFile(exception);
                            sErr = string.Concat(new object[] { "打开数据连接出错，获取数据失败： ", exception.Message, "\r\n", exception.TargetSite, ":", exception.StackTrace });
                            return null;
                        }
                    }
                    if (cmdX != null)
                    {
                        cmdX.Connection = connection;
                        adapter = new OracleDataAdapter(cmdX);
                        num = 4;
                        try
                        {
                            DataSet dataSet = null;
                            dataSet = new DataSet();
                            if (num4 > 0)
                            {
                                num4--;
                            }
                            num = 5;
                            adapter.Fill(dataSet, 0, 0, cmdInfo.DataTableName);
                            num = 6;
                            int nRows = 0;
                            if (dataSet.Tables[cmdInfo.DataTableName] != null)
                            {
                                nRows = dataSet.Tables[cmdInfo.DataTableName].Rows.Count;
                            }
                            if (nRows > 0)
                            {
                                nPageCount = this.GetPageCount(nPageSize, nRows);
                                if (dataSet.Tables[cmdInfo.DataTableName] != null)
                                {
                                    Zdx.LogManage.LogManage manage2 = new Zdx.LogManage.LogManage();
                                    manage2.WriteErrorInfoToFile("dsX.Tables[cmdInfo.DataTableName] Rows count :" + dataSet.Tables[cmdInfo.DataTableName].Rows.Count.ToString() + "  cols count:" + dataSet.Tables[cmdInfo.DataTableName].Columns.Count.ToString());
                                    if (dataSet.Tables[cmdInfo.DataTableName].Rows.Count > 0)
                                    {
                                        table = dataSet.Tables[cmdInfo.DataTableName].Clone();
                                        manage2.WriteErrorInfoToFile(" Clone");
                                    }
                                    else
                                    {
                                        table = dataSet.Tables[cmdInfo.DataTableName].Copy();
                                        manage2.WriteErrorInfoToFile(" Copy");
                                    }
                                    manage2.WriteErrorInfoToFile(" 0 : tbData Rows count :" + table.Rows.Count.ToString() + "  cols count:" + dataSet.Tables[cmdInfo.DataTableName].Columns.Count.ToString());
                                }
                            }
                            else if (dataSet.Tables[cmdInfo.DataTableName] != null)
                            {
                                table = dataSet.Tables[cmdInfo.DataTableName].Copy();
                            }
                            if (num5 > 0)
                            {
                                if (nRows < num5)
                                {
                                    num5 = nRows;
                                }
                                if (num4 > nRows)
                                {
                                    num4 = 0;
                                }
                                for (int i = num4; i < num5; i++)
                                {
                                    DataRow row = table.NewRow();
                                    int count = table.Columns.Count;
                                    for (int j = 0; j < count; j++)
                                    {
                                        row[j] = dataSet.Tables[cmdInfo.DataTableName].Rows[i][j];
                                    }
                                    table.Rows.Add(row);
                                }
                            }
                            else if (dataSet.Tables[cmdInfo.DataTableName] != null)
                            {
                                table = dataSet.Tables[cmdInfo.DataTableName].Copy();
                            }
                            dataSet.Clear();
                            if (table != null)
                            {
                                table.TableName = cmdInfo.DataTableName;
                            }
                            adapter.Dispose();
                            cmdX.Dispose();
                            connection.Close();
                            new Zdx.LogManage.LogManage().WriteErrorInfoToFile("tbData　ok Recordcount :" + table.Rows.Count.ToString());
                        }
                        catch (Exception exception3)
                        {
                            exception2 = exception3;
                            new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception2);
                            sErr = string.Concat(new object[] { exception2.Message, "\r\n", num.ToString(), ":", exception2.TargetSite, ":", exception2.StackTrace });
                            if (adapter != null)
                            {
                                adapter.Dispose();
                            }
                            if (cmdX != null)
                            {
                                cmdX.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception exception4)
            {
                exception2 = exception4;
                new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception2);
                sErr = string.Concat(new object[] { exception2.Message, "\r\n", num.ToString(), ":", exception2.TargetSite, ":", exception2.StackTrace });
            }
            this.SetTextInfo(false, sErr);
            return table;
        }

        private bool GetLocalIp(string sIp)
        {
            bool flag = false;
            this.serverHostName = Dns.GetHostName();
            //IPAddress[] hostAddresses = Dns.GetHostAddresses(this.serverHostName);
            //if (hostAddresses.Length > 0)
            //{
            //    foreach (IPAddress address in hostAddresses)
            //    {
            //        if (sIp == address.ToString())
            //        {
            //            this.serverIp = address;
            //            flag = true;
            //            break;
            //        }
            //    }
            //    if (!flag)
            //    {
            //        this.serverIp = hostAddresses[hostAddresses.Length - 1];
            //    }
            //    flag = true;
            //    this.ipAddress = this.serverIp.ToString();
            //}
            this.serverIp = IPAddress.Parse(sIp);
            this.ipAddress = this.serverIp.ToString();
            return flag;
        }

        private OleDbCommand GetOleDbCmd(DBSQLCommandInfo cmdInfo, out string sErr)
        {
            OleDbCommand command = null;
            sErr = "";
            int num = 0;
            string str = "";
            StringBuilder builder = new StringBuilder("");
            string str2 = "";
            string[] strArray = null;
            if (cmdInfo == null)
            {
                sErr = "命令对象未实例化,生成命令对象失败！";
                return null;
            }
            string str3 = "";
            str3 = cmdInfo.SqlText.ToLower();
            str = str3;
            switch (cmdInfo.SqlType)
            {
                case SqlCommandType.sctSql:
                    string str5;
                    strArray = null;
                    str2 = " from ";
                    strArray = str3.Split(new string[] { str2 }, StringSplitOptions.None);
                    num = 0;
                    foreach (string str4 in strArray)
                    {
                        if (num > 0)
                        {
                            str5 = str4;
                            if (str5.IndexOf("wms.") < 0)
                            {
                                str5 = "wms." + str5.Trim();
                                str3 = str3.Replace(str4.Trim(), str5);
                            }
                        }
                        num++;
                    }
                    strArray = null;
                    str2 = " join ";
                    strArray = str3.Split(new string[] { str2 }, StringSplitOptions.None);
                    num = 0;
                    foreach (string str4 in strArray)
                    {
                        if (num > 0)
                        {
                            str5 = str4;
                            if (str5.IndexOf("wms.") < 0)
                            {
                                str5 = "wms." + str5.Trim();
                                str3 = str3.Replace(str4.Trim(), str5);
                            }
                        }
                        num++;
                    }
                    if ((cmdInfo.Parameters != null) && (cmdInfo.Parameters.Count > 0))
                    {
                        foreach (ZqmParamter paramter in cmdInfo.Parameters)
                        {
                            builder.Remove(0, builder.Length);
                            string oldValue = ":" + paramter.ParameterName.Trim().ToLower();
                            string parameterValue = paramter.ParameterValue;
                            ZqmDataType dataType = paramter.DataType;
                            if (dataType != ZqmDataType.String)
                            {
                                if (dataType == ZqmDataType.DateTime)
                                {
                                    goto Label_0263;
                                }
                                goto Label_0278;
                            }
                            parameterValue = "'" + parameterValue + "'";
                            goto Label_0283;
                        Label_0263:
                            parameterValue = "'" + parameterValue + "'";
                            goto Label_0283;
                        Label_0278:
                            parameterValue = parameterValue.Trim();
                        Label_0283:
                            str3 = str3.Replace(oldValue, parameterValue);
                        }
                    }
                    return new OleDbCommand { CommandText = str3, CommandType = CommandType.Text };

                case SqlCommandType.sctProcedure:
                    strArray = cmdInfo.SqlText.Trim().ToLower().Split(new char[] { ' ' });
                    if ((strArray == null) || (strArray.Length <= 0))
                    {
                        goto Label_0535;
                    }
                    command = new OleDbCommand {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (strArray[0].ToLower().IndexOf("wms.") >= 0)
                    {
                        command.CommandText = strArray[0];
                        break;
                    }
                    command.CommandText = "wms." + strArray[0];
                    break;

                default:
                    return command;
            }
            if ((cmdInfo.Parameters != null) && (cmdInfo.Parameters.Count > 0))
            {
                foreach (ZqmParamter paramter in cmdInfo.Parameters)
                {
                    OleDbParameter parameter = new OleDbParameter();
                    string str8 = paramter.ParameterName.Trim();
                    parameter.ParameterName = str8;
                    switch (paramter.DataType)
                    {
                        case ZqmDataType.String:
                            parameter.Value = paramter.ParameterValue;
                            parameter.OleDbType = OleDbType.VarChar;
                            goto Label_04EB;

                        case ZqmDataType.Int:
                            parameter.Value = paramter.ParameterValue;
                            parameter.OleDbType = OleDbType.Integer;
                            goto Label_04EB;

                        case ZqmDataType.Double:
                            parameter.Value = double.Parse(paramter.ParameterValue.Trim());
                            parameter.OleDbType = OleDbType.Double;
                            goto Label_04EB;

                        case ZqmDataType.DateTime:
                            parameter.Value = paramter.ParameterValue;
                            parameter.OleDbType = OleDbType.VarChar;
                            goto Label_04EB;

                        case ZqmDataType.Bool:
                            if (!bool.Parse(paramter.ParameterValue.Trim()))
                            {
                                break;
                            }
                            parameter.Value = 1;
                            goto Label_047F;

                        default:
                            parameter.Value = paramter.ParameterValue;
                            parameter.OleDbType = OleDbType.VarChar;
                            goto Label_04EB;
                    }
                    parameter.Value = 0;
                Label_047F:
                    parameter.OleDbType = OleDbType.Integer;
                Label_04EB:
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);
                }
            }
        Label_0535:
            command.Parameters.Add("TCur_Result", OleDbType.PropVariant).Direction = ParameterDirection.Output;
            return command;
        }

        private OracleCommand GetOracleCmd(DBSQLCommandInfo cmdInfo, out string sErr)
        {
            OracleCommand command = null;
            sErr = "";
            string str = "";
            StringBuilder builder = new StringBuilder("");
            string[] strArray = null;
            if (cmdInfo == null)
            {
                sErr = "命令对象未实例化,生成命令对象失败！";
                return null;
            }
            string sText = "";
            string sqlText = cmdInfo.SqlText;
            sText = cmdInfo.SqlText;
            str = sText;
            switch (cmdInfo.SqlType)
            {
                case SqlCommandType.sctSql:
                    if ((cmdInfo.Parameters != null) && (cmdInfo.Parameters.Count > 0))
                    {
                        foreach (ZqmParamter paramter in cmdInfo.Parameters)
                        {
                            builder.Remove(0, builder.Length);
                            string oldValue = ":" + paramter.ParameterName.Trim();
                            string parameterValue = paramter.ParameterValue;
                            switch (paramter.DataType)
                            {
                                case ZqmDataType.String:
                                    parameterValue = "'" + parameterValue + "'";
                                    break;

                                case ZqmDataType.DateTime:
                                    parameterValue = "to_date('" + parameterValue + ",'yyyy-MM-dd hh24;mi:ss')";
                                    break;
                            }
                            sText = sText.Replace(oldValue, parameterValue);
                            sqlText = sqlText.Replace(oldValue, parameterValue);
                        }
                    }
                    this.SetTextInfo(false, sText, false);
                    sText = this.GetSqlOrcForDoIsNull(sText);
                    if (cmdInfo.FldsData.Trim().Length > 0)
                    {
                        sText = this.GetSqlOrcForDateFld(sText, cmdInfo.FldsData);
                    }
                    if (this.chk_Tranc.Checked)
                    {
                        this.savelog(sText, "SvrorcSql.txt");
                    }
                    this.SetTextInfo(false, sText);
                    command = new OracleCommand {
                        CommandText = sText,
                        CommandType = CommandType.Text
                    };
                    goto Label_054F;

                case SqlCommandType.sctProcedure:
                    strArray = cmdInfo.SqlText.Trim().ToLower().Split(new char[] { ' ' });
                    if ((strArray == null) || (strArray.Length <= 0))
                    {
                        goto Label_051D;
                    }
                    command = new OracleCommand {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (strArray[0].ToLower().IndexOf("wms.") >= 0)
                    {
                        command.CommandText = strArray[0];
                        break;
                    }
                    command.CommandText = "wms." + strArray[0];
                    break;

                default:
                    goto Label_054F;
            }
            builder.Append(command.CommandText + "(");
            if ((cmdInfo.Parameters != null) && (cmdInfo.Parameters.Count > 0))
            {
                foreach (ZqmParamter paramter in cmdInfo.Parameters)
                {
                    OracleParameter parameter = new OracleParameter();
                    string str7 = paramter.ParameterName.Trim();
                    parameter.ParameterName = str7;
                    switch (paramter.DataType)
                    {
                        case ZqmDataType.String:
                            parameter.Value = paramter.ParameterValue;
                            parameter.OracleType = OracleType.VarChar;
                            parameter.Size = 500;
                            goto Label_0454;

                        case ZqmDataType.Int:
                            parameter.Value = paramter.ParameterValue.Trim();
                            parameter.OracleType = OracleType.Int32;
                            goto Label_0454;

                        case ZqmDataType.Double:
                            parameter.Value = Convert.ToDouble(paramter.ParameterValue.Trim());
                            parameter.OracleType = OracleType.Double;
                            goto Label_0454;

                        case ZqmDataType.DateTime:
                            parameter.Value = Convert.ToDateTime(paramter.ParameterValue);
                            parameter.OracleType = OracleType.DateTime;
                            goto Label_0454;

                        case ZqmDataType.Bool:
                            if (!bool.Parse(paramter.ParameterValue.Trim()))
                            {
                                break;
                            }
                            parameter.Value = 1;
                            goto Label_03D6;

                        default:
                            parameter.Value = paramter.ParameterValue;
                            parameter.OracleType = OracleType.VarChar;
                            parameter.Size = 500;
                            goto Label_0454;
                    }
                    parameter.Value = 0;
                Label_03D6:
                    parameter.OracleType = OracleType.Number;
                Label_0454:
                    parameter.Direction = ParameterDirection.Input;
                    command.Parameters.Add(parameter);
                    builder.Append(parameter.OracleType.ToString() + " " + parameter.Direction.ToString() + ":" + parameter.ParameterName + "=" + parameter.Value.ToString() + " ,");
                }
            }
        Label_051D:
            command.Parameters.Add("Cur_Result", OracleType.Cursor).Direction = ParameterDirection.Output;
            builder.Append("Cursor Output :Cur_Result=?");
            builder.Append(")");
        Label_054F:
            if (this.chk_Tranc.Checked)
            {
                this.savelog(builder.ToString(), "SvrExecorcSql.txt");
            }
            return command;
        }

        private int GetPageCount(int nPageSize, int nRows)
        {
            int num = 0;
            int num2 = 0;
            if ((nPageSize * nRows) > 0)
            {
                num = nRows / nPageSize;
                num2 = nRows % nPageSize;
                if (num2 > 0)
                {
                    num++;
                }
            }
            return num;
        }

        private SqlCommand GetSqlCmd(DBSQLCommandInfo cmdInfo, out string sErr)
        {
            SqlCommand command = null;
            sErr = "";
            StringBuilder builder = new StringBuilder("");
            string[] strArray = null;
            if (cmdInfo == null)
            {
                sErr = "命令对象未实例化,生成命令对象失败！";
                return null;
            }
            switch (cmdInfo.SqlType)
            {
                case SqlCommandType.sctSql:
                {
                    string sqlText = cmdInfo.SqlText;
                    if ((cmdInfo.Parameters != null) && (cmdInfo.Parameters.Count > 0))
                    {
                        foreach (ZqmParamter paramter in cmdInfo.Parameters)
                        {
                            builder.Remove(0, builder.Length);
                            string oldValue = ":" + paramter.ParameterName.Trim().ToLower();
                            string parameterValue = paramter.ParameterValue;
                            ZqmDataType dataType = paramter.DataType;
                            if (dataType != ZqmDataType.String)
                            {
                                if (dataType == ZqmDataType.DateTime)
                                {
                                    goto Label_00FD;
                                }
                                goto Label_0112;
                            }
                            parameterValue = "'" + parameterValue + "'";
                            goto Label_011D;
                        Label_00FD:
                            parameterValue = "'" + parameterValue + "'";
                            goto Label_011D;
                        Label_0112:
                            parameterValue = parameterValue.Trim();
                        Label_011D:
                            sqlText = sqlText.Replace(oldValue, parameterValue);
                        }
                    }
                    return new SqlCommand { CommandText = sqlText, CommandType = CommandType.Text };
                }
                case SqlCommandType.sctProcedure:
                    strArray = cmdInfo.SqlText.Trim().ToLower().Split(new char[] { ' ' });
                    if ((strArray != null) && (strArray.Length > 0))
                    {
                        command = new SqlCommand {
                            CommandType = CommandType.StoredProcedure,
                            CommandText = strArray[0]
                        };
                        if ((cmdInfo.Parameters != null) && (cmdInfo.Parameters.Count > 0))
                        {
                            foreach (ZqmParamter paramter in cmdInfo.Parameters)
                            {
                                SqlParameter parameter = new SqlParameter();
                                string str4 = paramter.ParameterName.Trim();
                                parameter.ParameterName = str4;
                                switch (paramter.DataType)
                                {
                                    case ZqmDataType.String:
                                        parameter.Value = paramter.ParameterValue;
                                        parameter.SqlDbType = SqlDbType.VarChar;
                                        parameter.Size = 500;
                                        goto Label_0355;

                                    case ZqmDataType.Int:
                                        parameter.Value = paramter.ParameterValue;
                                        parameter.SqlDbType = SqlDbType.Int;
                                        goto Label_0355;

                                    case ZqmDataType.Double:
                                        parameter.Value = double.Parse(paramter.ParameterValue.Trim());
                                        parameter.SqlDbType = SqlDbType.Float;
                                        goto Label_0355;

                                    case ZqmDataType.DateTime:
                                        parameter.Value = paramter.ParameterValue;
                                        parameter.SqlDbType = SqlDbType.VarChar;
                                        goto Label_0355;

                                    case ZqmDataType.Bool:
                                        if (!bool.Parse(paramter.ParameterValue.Trim()))
                                        {
                                            break;
                                        }
                                        parameter.Value = 1;
                                        goto Label_02DF;

                                    default:
                                        parameter.Value = paramter.ParameterValue;
                                        parameter.SqlDbType = SqlDbType.VarChar;
                                        parameter.Size = 500;
                                        goto Label_0355;
                                }
                                parameter.Value = 0;
                            Label_02DF:
                                parameter.SqlDbType = SqlDbType.Bit;
                            Label_0355:
                                parameter.Direction = ParameterDirection.Input;
                                command.Parameters.Add(parameter);
                            }
                        }
                    }
                    return command;
            }
            return command;
        }

        private string GetSqlOrcForDateFld(string sSql, string sFldsDate)
        {
            string[] strArray4;
            string str = sSql;
            int startIndex = -1;
            startIndex = str.ToLower().IndexOf("insert ", 0);
            if (startIndex > -1)
            {
                int index = str.ToLower().IndexOf("into ", startIndex);
                if (index <= 0)
                {
                    return str;
                }
                if (str.Substring(startIndex + 7, (index - startIndex) - 7).Trim().Length != 0)
                {
                    return str;
                }
                string[] strArray = new string[] { "", "", "" };
                index += 5;
                strArray[0] = str.Substring(0, index);
                int length = str.IndexOf("(", index);
                if (length <= 0)
                {
                    return str;
                }
                strArray[0] = str.Substring(0, length);
                index = length + 1;
                length = str.IndexOf(")", index);
                if (length > 0)
                {
                    strArray[1] = str.Substring(index, length - index);
                }
                index = length + 1;
                index = str.IndexOf("(", index) + 1;
                length = str.Trim().Length - 1;
                if (length > 0)
                {
                    strArray[2] = str.Substring(index, length - index);
                }
                string[] strArray2 = null;
                strArray2 = strArray[1].Split(new char[] { ',' });
                string[] strArray3 = null;
                strArray3 = strArray[2].Split(new char[] { ',' });
                if (sFldsDate.Trim().Length > 0)
                {
                    strArray4 = null;
                    strArray4 = sFldsDate.Split(new char[] { ',' });
                    foreach (string str3 in strArray4)
                    {
                        for (int i = 0; i < strArray2.Length; i++)
                        {
                            if (str3.Trim().ToLower() == strArray2[i].Trim().ToLower())
                            {
                                strArray3[i] = "to_date(" + strArray3[i].Trim() + ",'yyyy-MM-dd hh24;mi:ss')";
                            }
                        }
                    }
                }
                StringBuilder builder = new StringBuilder("");
                builder.Append(strArray[0].Trim());
                builder.Append("(");
                builder.Append(strArray[1].Trim());
                builder.Append(") values(");
                startIndex = 0;
                foreach (string str4 in strArray3)
                {
                    if (startIndex == 0)
                    {
                        builder.Append(str4);
                    }
                    else
                    {
                        builder.Append("," + str4);
                    }
                    startIndex++;
                }
                builder.Append(")");
                return builder.ToString();
            }
            strArray4 = null;
            strArray4 = sFldsDate.Split(new char[] { ',' });
            foreach (string str5 in strArray4)
            {
                for (startIndex = str.ToLower().IndexOf(str5.ToLower(), 0); startIndex > 0; startIndex = str.ToLower().IndexOf(str5.ToLower(), (int) (startIndex + 1)))
                {
                    int num6;
                    int num7;
                    string str6;
                    bool flag = false;
                    int num5 = -1;
                    num5 = str.IndexOf("=", startIndex);
                    if ((num5 > 0) && (str.Substring(startIndex + str5.Length, (num5 - startIndex) - str5.Length).Trim().Length == 0))
                    {
                        num6 = str.IndexOfAny(new char[] { '\'' }, num5);
                        num7 = str.IndexOfAny(new char[] { '\'' }, num6 + 1);
                        string str4 = str.Substring(num6 + 1, (num7 - num6) - 1);
                        str4 = str5 + " = to_date('" + str4.Trim() + "','yyyy-MM-dd hh24;mi:ss')";
                        str6 = str.Substring(startIndex, (num7 - startIndex) + 1);
                        str = str.Replace(str6, str4);
                        startIndex = num7;
                        flag = true;
                    }
                    if (!flag)
                    {
                        num5 = str.IndexOf(">", startIndex);
                        if ((num5 > 0) && (str.Substring(startIndex + str5.Length, (num5 - startIndex) - str5.Length).Trim().Length == 0))
                        {
                            num6 = str.IndexOfAny(new char[] { '\'' }, num5);
                            num7 = str.IndexOfAny(new char[] { '\'' }, num6 + 1);
                            string str4 = str.Substring(num6 + 1, (num7 - num6) - 1);
                            if (str4.Trim() != "")
                            {
                                str4 = str5 + " > to_date('" + str4.Trim() + "','yyyy-MM-dd hh24;mi:ss')";
                            }
                            str6 = str.Substring(startIndex, (num7 - startIndex) + 1);
                            str = str.Replace(str6, str4);
                            startIndex = num7;
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        num5 = str.IndexOf("<", startIndex);
                        if ((num5 > 0) && (str.Substring(startIndex + str5.Length, (num5 - startIndex) - str5.Length).Trim().Length == 0))
                        {
                            num6 = str.IndexOfAny(new char[] { '\'' }, num5);
                            num7 = str.IndexOfAny(new char[] { '\'' }, num6 + 1);
                            string str4 = str.Substring(num6 + 1, (num7 - num6) - 1);
                            if (str4.Trim() != "")
                            {
                                str4 = str5 + " < to_date('" + str4.Trim() + "','yyyy-MM-dd hh24;mi:ss')";
                            }
                            str6 = str.Substring(startIndex, (num7 - startIndex) + 1);
                            str = str.Replace(str6, str4);
                            startIndex = num7;
                            flag = true;
                        }
                    }
                }
            }
            return str;
        }

        private string GetSqlOrcForDoIsNull(string sSql)
        {
            string str = sSql;
            string str2 = "isnull(";
            string newValue = "nvl(";
            int startIndex = -1;
            for (startIndex = str.ToLower().IndexOf(str2.ToLower(), 0); startIndex > 0; startIndex = str.ToLower().IndexOf(str2.ToLower(), (int) (startIndex + newValue.Length)))
            {
                string oldValue = str.Substring(startIndex, str2.Length);
                if (oldValue.Trim().Length >= 0)
                {
                    str = str.Replace(oldValue, newValue);
                }
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmMain));
            this.grpState = new GroupBox();
            this.lblDBName = new Label();
            this.lblServerHost = new Label();
            this.btnText = new Button();
            this.imgList = new ImageList(this.components);
            this.lblLocalIp = new Label();
            this.btnMeun = new Button();
            this.lblState = new Label();
            this.btnRun = new Button();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.dlgSave = new SaveFileDialog();
            this.toolTip = new ToolTip(this.components);
            this.btn_ClearText = new Button();
            this.pnl_InInfo = new Panel();
            this.chk_Tranc = new CheckBox();
            this.btnSave = new Button();
            this.txtClearText = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtText = new TextBox();
            this.grpState.SuspendLayout();
            this.pnl_InInfo.SuspendLayout();
            base.SuspendLayout();
            this.grpState.Controls.Add(this.lblDBName);
            this.grpState.Controls.Add(this.lblServerHost);
            this.grpState.Controls.Add(this.btnText);
            this.grpState.Controls.Add(this.lblLocalIp);
            this.grpState.Controls.Add(this.btnMeun);
            this.grpState.Controls.Add(this.lblState);
            this.grpState.Controls.Add(this.btnRun);
            this.grpState.Dock = DockStyle.Top;
            this.grpState.Location = new Point(0, 0);
            this.grpState.Name = "grpState";
            this.grpState.Size = new Size(370, 0x61);
            this.grpState.TabIndex = 0;
            this.grpState.TabStop = false;
            this.lblDBName.ForeColor = SystemColors.ActiveCaption;
            this.lblDBName.Location = new Point(6, 0x2d);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new Size(0x11a, 0x10);
            this.lblDBName.TabIndex = 6;
            this.lblDBName.Text = "IP:255.255.255.255";
            this.lblServerHost.AutoSize = true;
            this.lblServerHost.Location = new Point(0x8a, 0x11);
            this.lblServerHost.Name = "lblServerHost";
            this.lblServerHost.Size = new Size(0x29, 12);
            this.lblServerHost.TabIndex = 5;
            this.lblServerHost.Text = "名称：";
            this.btnText.ImageList = this.imgList;
            this.btnText.Location = new Point(0x3f, 0x43);
            this.btnText.Name = "btnText";
            this.btnText.Size = new Size(0x20, 0x1a);
            this.btnText.TabIndex = 4;
            this.btnText.Text = ">>";
            this.btnText.UseVisualStyleBackColor = true;
            this.btnText.Click += new EventHandler(this.btnText_Click);
            this.imgList.ColorDepth = ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new Size(0x10, 0x10);
            this.imgList.TransparentColor = Color.Transparent;
            this.lblLocalIp.Location = new Point(0x6f, 0x47);
            this.lblLocalIp.Name = "lblLocalIp";
            this.lblLocalIp.Size = new Size(0xb9, 0x10);
            this.lblLocalIp.TabIndex = 2;
            this.lblLocalIp.Text = "IP:255.255.255.255";
            this.lblLocalIp.Click += new EventHandler(this.lblLocalIp_Click);
            this.btnMeun.Location = new Point(1, 0x42);
            this.btnMeun.Name = "btnMeun";
            this.btnMeun.Size = new Size(0x34, 0x1c);
            this.btnMeun.TabIndex = 0;
            this.btnMeun.Text = "退出";
            this.btnMeun.UseVisualStyleBackColor = true;
            this.btnMeun.Click += new EventHandler(this.btnMeun_Click);
            this.lblState.Location = new Point(0x41, 0x11);
            this.lblState.Name = "lblState";
            this.lblState.Size = new Size(0x3f, 0x11);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "服务停止";
            this.btnRun.Location = new Point(2, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new Size(50, 0x1d);
            this.btnRun.TabIndex = 0;
            this.btnRun.Tag = "0";
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = true;
            //this.btnRun.Click += new EventHandler(this.btnRun_Click);
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 0x3e8;
            this.tmrMain.Tick += new EventHandler(this.tmrMain_Tick);
            this.dlgSave.DefaultExt = "txt";
            this.dlgSave.FileName = "RFServerLog";
            this.btn_ClearText.ImageList = this.imgList;
            this.btn_ClearText.Location = new Point(0x11d, 11);
            this.btn_ClearText.Name = "btn_ClearText";
            this.btn_ClearText.Size = new Size(0x34, 0x19);
            this.btn_ClearText.TabIndex = 15;
            this.btn_ClearText.Text = "清除";
            this.toolTip.SetToolTip(this.btn_ClearText, "清除日志");
            this.btn_ClearText.UseVisualStyleBackColor = true;
            this.btn_ClearText.Visible = false;
            this.btn_ClearText.Click += new EventHandler(this.btn_ClearText_Click);
            this.pnl_InInfo.Controls.Add(this.btn_ClearText);
            this.pnl_InInfo.Controls.Add(this.chk_Tranc);
            this.pnl_InInfo.Controls.Add(this.btnSave);
            this.pnl_InInfo.Controls.Add(this.txtClearText);
            this.pnl_InInfo.Controls.Add(this.label1);
            this.pnl_InInfo.Controls.Add(this.label2);
            this.pnl_InInfo.Dock = DockStyle.Top;
            this.pnl_InInfo.Location = new Point(0, 0x61);
            this.pnl_InInfo.Name = "pnl_InInfo";
            this.pnl_InInfo.Size = new Size(370, 40);
            this.pnl_InInfo.TabIndex = 10;
            this.chk_Tranc.AutoSize = true;
            this.chk_Tranc.Location = new Point(0x83, 13);
            this.chk_Tranc.Name = "chk_Tranc";
            this.chk_Tranc.Size = new Size(0x30, 0x10);
            this.chk_Tranc.TabIndex = 14;
            this.chk_Tranc.Text = "跟踪";
            this.chk_Tranc.UseVisualStyleBackColor = true;
            this.chk_Tranc.Visible = false;
            this.chk_Tranc.CheckedChanged += new EventHandler(this.chk_Tranc_CheckedChanged);
            this.btnSave.Location = new Point(0x3f, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(0x43, 0x19);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "日志保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.txtClearText.Location = new Point(240, 12);
            this.txtClearText.Name = "txtClearText";
            this.txtClearText.Size = new Size(0x1d, 0x15);
            this.txtClearText.TabIndex = 12;
            this.txtClearText.Text = "10";
            this.label1.Location = new Point(0xb7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x44, 0x13);
            this.label1.TabIndex = 13;
            this.label1.Text = "保留次数：";
            this.label2.Location = new Point(2, 15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x44, 0x11);
            this.label2.TabIndex = 11;
            this.label2.Text = "通讯内容：";
            this.txtText.Dock = DockStyle.Fill;
            this.txtText.Location = new Point(0, 0x89);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ScrollBars = ScrollBars.Vertical;
            this.txtText.Size = new Size(370, 0);
            this.txtText.TabIndex = 11;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(370, 0x68);
            base.Controls.Add(this.txtText);
            base.Controls.Add(this.pnl_InInfo);
            base.Controls.Add(this.grpState);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.KeyPreview = true;
            base.Name = "frmMain";
            this.Text = "中间件服务器";
            base.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
            base.KeyDown += new KeyEventHandler(this.frmMain_KeyDown);
            base.Load += new EventHandler(this.frmMain_Load);
            this.grpState.ResumeLayout(false);
            this.grpState.PerformLayout();
            this.pnl_InInfo.ResumeLayout(false);
            this.pnl_InInfo.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblLocalIp_Click(object sender, EventArgs e)
        {
        }

        private void savelog(string sText)
        {
            byte[] bytes = null;
            FileStream stream = new FileStream("sktlog.txt", FileMode.Create);
            bytes = Encoding.Default.GetBytes(sText);
            stream.Seek(0L, SeekOrigin.End);
            if (bytes.Length > 0)
            {
                try
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception exception)
                {
                    new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                }
            }
            stream.Close();
            stream.Dispose();
        }

        private void savelog(string sText, string sFile)
        {
            FileStream stream = new FileStream(sFile, FileMode.OpenOrCreate);
            byte[] bytes = null;
            bytes = Encoding.Default.GetBytes(sText);
            stream.Seek(0L, SeekOrigin.End);
            if (bytes.Length > 0)
            {
                try
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception exception)
                {
                    new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                }
            }
            stream.Close();
            stream.Dispose();
        }

        private void savelog(byte[] bBuff, string sFile)
        {
            if (bBuff != null)
            {
                FileStream stream = new FileStream(sFile, FileMode.OpenOrCreate);
                stream.Seek(0L, SeekOrigin.End);
                if (bBuff.Length > 0)
                {
                    try
                    {
                        stream.Write(bBuff, 0, bBuff.Length);
                    }
                    catch (Exception exception)
                    {
                        new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                    }
                }
                stream.Close();
                stream.Dispose();
            }
        }

        private void SetTextInfo(bool bIsClear, string sText)
        {
            this.SetTextInfo(bIsClear, sText, false);
        }

        private void SetTextInfo(bool bIsClear, string sText, bool isErr)
        {
            if (isErr || this.chk_Tranc.Checked)
            {
                if (this.txtText.InvokeRequired)
                {
                    ChangeTextBoxValue method = new ChangeTextBoxValue(this.SetTextInfo);
                    base.Invoke(method, new object[] { bIsClear, sText });
                }
                else
                {
                    if (bIsClear)
                    {
                        this.txtText.Text = "";
                    }
                    else
                    {
                        this.txtText.Text = string.Concat(new object[] { this.txtText.Text, '\r', '\n', sText });
                    }
                    base.Update();
                }
            }
        }

        private void ThreadDoTcpClient(TcpClient client)
        {
            client.NoDelay = true;
            do
            {
                NetworkStream nstrm = client.GetStream();
                if (nstrm.CanRead)
                {
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    MemoryStream mmX = new MemoryStream();
                    do
                    {
                        if (nstrm.DataAvailable)
                        {
                            int count = nstrm.Read(buffer, 0, buffer.Length);
                            mmX.Write(buffer, 0, count);
                        }
                    }
                    while (nstrm.DataAvailable);
                    this.DoServerReceicedData(client, nstrm, mmX);
                }
            }
            while (client.Connected);
        }

        private void ThreadSckSvr(object objSktSvr)
        {
            Socket socket = (Socket) objSktSvr;
            while (true)
            {
                if (this.isServerRun)
                {
                    try
                    {
                        this.SetTextInfo(false, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "服务器开始侦听...");
                        Socket item = socket.Accept();
                        this.SetTextInfo(false, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "接收到：" + item.LocalEndPoint.ToString() + " 的连接请求");
                        lock (this.SktCltLIst)
                        {
                            this.SktCltLIst.Add(item);
                        }
                        Thread thread = new Thread(new ParameterizedThreadStart(this.DoSocketClient)) {
                            IsBackground = true
                        };
                        this.SetTextInfo(false, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "接收线程准备运行 ");
                        thread.Start(item);
                    }
                    catch (Exception exception)
                    {
                        new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                    }
                }
            }
        }

        private void ThreadStartListen()
        {
            int num3 = 0;
            TcpClient client = null;
            if ((this.objListener != null) && (this.objListener != null))
            {
                SocketException exception;
                try
                {
                    this.SetTextInfo(false, "服务器开始监听...");
                    this.isServerRun = true;
                    this.objListener.Start();
                }
                catch (SocketException exception1)
                {
                    exception = exception1;
                    Zdx.LogManage.LogManage manage = new Zdx.LogManage.LogManage();
                    manage.WriteErrorInfoToFile(exception);
                    this.isServerRun = false;
                    this.SetTextInfo(false, "服务器监听启动出错：" + exception.Message);
                    this.objListener.Stop();
                    return;
                }
                while (true)
                {
                    if (this.isServerRun)
                    {
                        num3++;
                        try
                        {
                            this.SetTextInfo(false, "监听中，等待客户端连接...(" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ")");
                            client = this.objListener.AcceptTcpClient();
                            DoClientTcp tcp = new DoClientTcp {
                                DoRecivedData = new DoClientTcp.DoReciveData(this.DoServerReceicedData),
                                Tcpclient = client
                            };
                            new Thread(new ThreadStart(tcp.DoThreadFunc)).Start();
                        }
                        catch (SocketException exception2)
                        {
                            exception = exception2;
                            this.SetTextInfo(false, exception.Message);
                            new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
                            return;
                        }
                    }
                }
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            base.Update();
        }

        private void txtClearText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(this.txtClearText.Text.Trim());
                this.iClearText = num;
            }
            catch (Exception exception)
            {
                new Zdx.LogManage.LogManage().WriteErrorInfoToFile(exception);
            }
        }

        public bool IsCompress
        {
            get
            {
                return this.isCompress;
            }
            set
            {
                this.isCompress = value;
            }
        }

        public bool IsServerRun
        {
            get
            {
                return this.isServerRun;
            }
            set
            {
                this.isServerRun = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        public int SerializerType
        {
            get
            {
                return this.serializerType;
            }
            set
            {
                this.serializerType = value;
            }
        }

        public string ServerIP
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }

        public delegate void ChangeTextBoxValue(bool bIsClear, string sText);
    }
}

