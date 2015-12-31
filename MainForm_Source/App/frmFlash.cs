namespace App
{
    using SunEast;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using Zqm.Net;

    public class frmFlash : Form
    {
        private string _AppTitle = "";
        public bool bIsOK = false;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label4;
        public Panel panel1;
        public Panel pnlTitle;
        public Socket SktClient = null;
        private Timer tmrMain;

        public frmFlash()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmFlash_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.pnlTitle = new Panel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.label4 = new Label();
            this.tmrMain = new Timer(this.components);
            this.pnlTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.pnlTitle.BackColor = Color.FloralWhite;
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Dock = DockStyle.Top;
            this.pnlTitle.Location = new Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new Size(0x22f, 0x6b);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.UseWaitCursor = true;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("隶书", 30f, FontStyle.Bold, GraphicsUnit.Pixel, 0x86);
            this.label2.ForeColor = SystemColors.GradientActiveCaption;
            this.label2.Location = new Point(0x7c, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x124, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "自动化仓储管理系统";
            this.label2.UseWaitCursor = true;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("华文行楷", 20f, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Pixel, 0x86);
            this.label1.ForeColor = SystemColors.GradientActiveCaption;
            this.label1.Location = new Point(12, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5e, 0x15);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎使用";
            this.label1.UseWaitCursor = true;
            this.panel1.BackColor = Color.White;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x6b);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x22f, 50);
            this.panel1.TabIndex = 1;
            this.panel1.UseWaitCursor = true;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("华文行楷", 20f, FontStyle.Italic, GraphicsUnit.Pixel, 0x86);
            this.label4.ForeColor = Color.Blue;
            this.label4.Location = new Point(0x37, 0x11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xbf, 0x15);
            this.label4.TabIndex = 0;
            this.label4.Text = "系统正在初始化中...";
            this.label4.UseWaitCursor = true;
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 500;
            this.tmrMain.Tick += new EventHandler(this.tmrMain_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Khaki;
            base.ClientSize = new Size(0x22f, 0x9d);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pnlTitle);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "frmFlash";
            base.Opacity = 0.65;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "系统启动中...";
            base.UseWaitCursor = true;
            base.Load += new EventHandler(this.frmFlash_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void ReadConf()
        {
            Encoding encoding;
            string sErr = "";
            string sFile = Application.StartupPath + @"\AppConfig.xml";
            string serverAddress = "";
            int port = 0;
            DBSocketServerType dbsstDotNet = DBSocketServerType.dbsstDotNet;
            int recevieBufferSize = 0;
            if (this.ReadConfigInfo(sFile, out serverAddress, out port, out dbsstDotNet, out recevieBufferSize, out encoding, out sErr))
            {
                Exception exception;
                if (this.SktClient == null)
                {
                    try
                    {
                        this.SktClient = SocketClient.ConnectSocket(serverAddress, port);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        MessageBox.Show(exception.Message);
                    }
                    this.bIsOK = (this.SktClient != null) && this.SktClient.Connected;
                }
                else
                {
                    if (this.SktClient.Connected)
                    {
                        this.SktClient.Close();
                    }
                    try
                    {
                        this.SktClient = SocketClient.ConnectSocket(serverAddress, port);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        MessageBox.Show(exception.Message);
                    }
                    this.bIsOK = (this.SktClient != null) && this.SktClient.Connected;
                }
                if (!this.bIsOK && (MessageBox.Show("连接中间件服务器参数(" + serverAddress + ":" + port.ToString() + ")不对或中间件服务器没有启动，\n请修改参数或启动中间件服务器后，点“是”按钮，继续，否则退出。\n是否继续？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                    this.ReadConf();
                }
            }
            base.Close();
        }

        public bool ReadConfigInfo(string sFile, out string serverAddress, out int port, out DBSocketServerType dbstServer, out int recevieBufferSize, out Encoding myEncoder, out string sErr)
        {
            serverAddress = "";
            port = 0;
            dbstServer = DBSocketServerType.dbsstDotNet;
            recevieBufferSize = 0;
            myEncoder = Encoding.UTF8;
            sErr = "";
            if (sFile.Length == 0)
            {
                sFile = Application.StartupPath + @"\AppConfig.xml";
            }
            bool flag = false;
            if (!File.Exists(sFile))
            {
                sErr = "文件：" + sFile + "  不存在，读取配置信息出错！";
                return false;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                string name = "";
                document.Load(sFile);
                XmlNode node = null;
                node = document.SelectSingleNode("config/remoteserver");
                if (node != null)
                {
                    int num;
                    if (node.Attributes["server"] != null)
                    {
                        name = node.Attributes["server"].Value.Trim();
                        serverAddress = name;
                    }
                    if (node.Attributes["Port"] != null)
                    {
                        name = node.Attributes["Port"].Value.Trim();
                        if (name.Trim().Length > 0)
                        {
                            num = int.Parse(name.Trim());
                            port = num;
                        }
                    }
                    if (node.Attributes["servertype"] != null)
                    {
                        name = node.Attributes["servertype"].Value.Trim();
                        if (name.Trim().Length > 0)
                        {
                            num = int.Parse(name.Trim());
                            dbstServer = (DBSocketServerType) num;
                        }
                    }
                    if (node.Attributes["buffersize"] != null)
                    {
                        name = node.Attributes["buffersize"].Value.Trim();
                        if (name.Trim().Length > 0)
                        {
                            num = int.Parse(name.Trim());
                            recevieBufferSize = num;
                        }
                    }
                    if (node.Attributes["myencoding"] != null)
                    {
                        name = node.Attributes["myencoding"].Value.Trim();
                        if (name.Trim().Length > 0)
                        {
                            myEncoder = Encoding.GetEncoding(name);
                        }
                    }
                    flag = true;
                    node = document.SelectSingleNode("config/appmain");
                    if ((node != null) && (node.Attributes["sysName"] != null))
                    {
                        name = node.Attributes["sysName"].Value.Trim();
                        this._AppTitle = name;
                    }
                    return flag;
                }
                sErr = "配置文件里找不到 config/remoteserver 路径的节点，读取配置数据出错！";
                return false;
            }
            catch (Exception exception)
            {
                sErr = exception.Message;
                return false;
            }
        }

        public bool SaveConfigInfoToFile(string sFile, string sServer, int iPort, DBSocketServerType svrType, int iBufferSize, Encoding myEncoder, out string sErr)
        {
            sErr = "";
            XmlDocument document = new XmlDocument();
            XmlWriter writer = XmlWriter.Create(sFile);
            try
            {
                document.Load(sFile);
                XmlNode node = null;
                node = document.SelectSingleNode("config/remoteserver");
                if (node != null)
                {
                    if (node.Attributes["server"] != null)
                    {
                        node.Attributes["server"].Value = sServer;
                    }
                    if (node.Attributes["port"] != null)
                    {
                        node.Attributes["port"].Value = iPort.ToString();
                    }
                    if (node.Attributes["servertype"] != null)
                    {
                        node.Attributes["servertype"].Value = ((int) svrType).ToString();
                    }
                    if (node.Attributes["buffersize"] != null)
                    {
                        node.Attributes["buffersize"].Value = iBufferSize.ToString();
                    }
                    if (node.Attributes["myencoding"] != null)
                    {
                        node.Attributes["myencoding"].Value = myEncoder.HeaderName;
                    }
                    return true;
                }
                sErr = "配置文件里找不到 config/remoteserver 路径的节点，读取配置数据出错！";
                return false;
            }
            catch (Exception exception)
            {
                sErr = exception.Message;
                return false;
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            this.tmrMain.Enabled = false;
            if (this.SktClient == null)
            {
                this.ReadConf();
            }
        }

        public string AppTitle
        {
            get
            {
                return this._AppTitle;
            }
            set
            {
                this._AppTitle = value;
            }
        }
    }
}

