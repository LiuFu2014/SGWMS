namespace App
{
    using Microsoft.Win32;
    using SunEast;
    using SunEast.App;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using CommBase;
    using DBCommInfo;
    using FileFun;

    public class frmMain : Form
    {
        private WMSAppInfo ainfo = null;
        private bool bIsUseIOMIDDB = false;
        private bool bIsUseMIDForeGround = false;
        private BackgroundWorker bkWorker;
        private IContainer components = null;
        private ToolStripMenuItem dddToolStripMenuItem;
        private ToolStripMenuItem ddToolStripMenuItem;
        private ToolStripMenuItem ddToolStripMenuItem1;
        private ToolStripMenuItem dPS库管理ToolStripMenuItem;
        private ToolStripMenuItem eRP对接ToolStripMenuItem;
        private List<Form> frmChildList = new List<Form>();
        private frmPromptForIOMIDData frmPrompIODB = null;
        private PictureBox imgBackground;
        private ToolStripMenuItem mi_Help_Abot;
        private MenuStrip mmMain;
        private ToolStripMenuItem mToolStripMenuItem;
        private int nCount_IF_BillCheck = 0;
        private int nCount_IF_BillIn = 0;
        private int nCount_IF_BillOut = 0;
        private int nCount_IF_BillRemove = 0;
        private Panel pnlBackground;
        private ToolStripDropDownButton stBtnErpIF;
        private ToolStripStatusLabel stlbCompany;
        private ToolStripStatusLabel stlbDate;
        private ToolStripStatusLabel stlbDept;
        private ToolStripStatusLabel stlbUser;
        private ToolStripStatusLabel stlbWeek;
        private ToolStripMenuItem stMi_BillCheck;
        private ToolStripMenuItem stMi_BillIn;
        private ToolStripMenuItem stMi_BillOut;
        private ToolStripMenuItem stMi_BillRemove;
        private StatusStrip sttMain;
        private Timer tmrMain;
        private WMSUserInfo userInfo = null;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 基础信息管理ToolStripMenuItem;
        private ToolStripMenuItem 库存管理ToolStripMenuItem;
        private ToolStripMenuItem 立库管理ToolStripMenuItem;
        private ToolStripMenuItem 平库管理ToolStripMenuItem;
        private ToolStripMenuItem 综合查询ToolStripMenuItem;

        public frmMain()
        {
            this.InitializeComponent();
            string str = Application.StartupPath + @"\pic\";
            if (File.Exists(str + "mainBk.bmp"))
            {
                this.imgBackground.Image = Image.FromFile(str + "mainBk.bmp");
            }
            else if (File.Exists(str + "mainBk.jpg"))
            {
                this.imgBackground.Image = Image.FromFile(str + "mainBk.jpg");
            }
            else if (File.Exists(str + "mainBk0.bmp"))
            {
            }
        }

        private void bkWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.GetMIDDB();
        }

        private void bkWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((((this.nCount_IF_BillCheck + this.nCount_IF_BillIn) + this.nCount_IF_BillOut) + this.nCount_IF_BillRemove) > 0)
            {
                if (this.frmPrompIODB == null)
                {
                    this.frmPrompIODB = new frmPromptForIOMIDData();
                    this.frmPrompIODB.ShowformClose = new ShowFormClose(this.FormShowClose);
                    this.frmPrompIODB.Location = new Point((base.Left + base.Width) - this.frmPrompIODB.Width, (base.Top + base.Height) - this.frmPrompIODB.Height);
                    this.frmPrompIODB.AppInformation = this.ainfo;
                    this.frmPrompIODB.UserInformation = this.userInfo;
                    this.frmPrompIODB.IsDoDBForeGround = this.bIsUseMIDForeGround;
                    this.frmPrompIODB.lbl_Count_Check.Text = this.nCount_IF_BillCheck.ToString();
                    this.frmPrompIODB.lbl_Count_In.Text = this.nCount_IF_BillIn.ToString();
                    this.frmPrompIODB.lbl_Count_Out.Text = this.nCount_IF_BillOut.ToString();
                    this.frmPrompIODB.lbl_Count_Remove.Text = this.nCount_IF_BillRemove.ToString();
                    this.frmPrompIODB.TopMost = false;
                    this.frmPrompIODB.TopMost = true;
                    this.frmPrompIODB.Show();
                }
                else
                {
                    this.frmPrompIODB.lbl_Count_Check.Text = this.nCount_IF_BillCheck.ToString();
                    this.frmPrompIODB.lbl_Count_In.Text = this.nCount_IF_BillIn.ToString();
                    this.frmPrompIODB.lbl_Count_Out.Text = this.nCount_IF_BillOut.ToString();
                    this.frmPrompIODB.lbl_Count_Remove.Text = this.nCount_IF_BillRemove.ToString();
                    this.frmPrompIODB.Update();
                    this.frmPrompIODB.TopMost = false;
                    this.frmPrompIODB.TopMost = true;
                }
            }
            else
            {
                if (this.frmPrompIODB != null)
                {
                    this.frmPrompIODB.Close();
                }
                this.frmPrompIODB = null;
            }
            if (!(this.bkWorker.CancellationPending || (this.frmPrompIODB == null)))
            {
                this.tmrMain.Enabled = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("关于");
        }

        private void DoExitClick(object sender, EventArgs e)
        {
            base.Close();
        }

        private void DoHelpClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            if (item != null)
            {
                string str = "";
                if (item.Tag != null)
                {
                    str = item.Tag.ToString();
                }
                if ((str == "") || (str == "0"))
                {
                    frmSQLCMD msqlcmd = new frmSQLCMD {
                        AppInformation = this.ainfo,
                        UserInformation = this.userInfo
                    };
                    msqlcmd.ShowDialog();
                    msqlcmd.Dispose();
                }
                else
                {
                    MessageBox.Show(str + "  的帮助");
                }
            }
        }

        private void DoMenuItemClick(object sender, EventArgs e)
        {
            bool bIsOK = false;
            bool flag2 = true;
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            if ((item != null) && (item.Tag != null))
            {
                string str4;
                string[] strArray = item.Tag.ToString().Trim().Split(new char[] { '^' });
                string str2 = "select * from TPB_Rights where cRId='" + item.Name.ToString().Trim() + "'";
                DataSet set = null;
                SeDBClient client = new SeDBClient(DBSocketServerType.dbsstDotNet);
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    FromSysType = "DotNet",
                    PageIndex = 0,
                    PageSize = 0,
                    SqlText = str2.ToString(),
                    SqlType = SqlCommandType.sctSql,
                    MyEncoding = Encoding.UTF8,
                    DataTableName = "TPB_Rights"
                };
                string sErr = "";
                set = client.GetDataSet(this.ainfo.SvrSocket, cmdInfo, false, out sErr);
                DataTable table = set.Tables["TPB_Rights"];
                if ((table != null) && (table.Rows.Count > 0))
                {
                    flag2 = table.Rows[0]["cRType"].ToString() != "2";
                }
                set.Clear();
                if (flag2)
                {
                    if ((strArray != null) && (strArray.Length > 2))
                    {
                        str4 = this.ainfo.AppPath + @"\" + strArray[0];
                        if (File.Exists(str4))
                        {
                            object[] param = new object[] { this.ainfo, this.userInfo, item.Name };
                            MyCallSafetyDll.DoCallMyDll(str4, strArray[1], strArray[2], param, out bIsOK);
                        }
                        else
                        {
                            MessageBox.Show(this.ainfo.AppPath + @"\" + strArray[0] + "  不存在！");
                        }
                    }
                }
                else if ((strArray != null) && (strArray.Length == 1))
                {
                    str4 = this.ainfo.AppPath + @"\" + strArray[0];
                    if (File.Exists(str4))
                    {
                        if (!MyExeFile.CheckExeIsRunning(str4))
                        {
                            MyExeFile.CallExe(str4, ProcessWindowStyle.Normal, new string[] { this.userInfo.UserId.ToString() });
                        }
                    }
                    else
                    {
                        MessageBox.Show(this.ainfo.AppPath + @"\" + strArray[0] + "  不存在！");
                    }
                }
            }
        }

        private void FormShowClose(object sender)
        {
            this.frmPrompIODB.Dispose();
            this.frmPrompIODB = null;
            this.tmrMain.Enabled = this.bIsUseIOMIDDB;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要退出整个系统吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else if (this.frmChildList.Count > 0)
            {
                foreach (Form form in this.frmChildList)
                {
                    form.Close();
                    form.Dispose();
                }
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.ainfo.AppICON = base.Icon;
            //new MyRegister { RootKey = Registry.LocalMachine }.RegWrite(@"Software\SunEast\WMS", "WMSPath", Application.StartupPath);
            if (this.userInfo != null)
            {
                this.stlbCompany.Text = "单位【" + this.userInfo.UnitName + "】";
                this.stlbDept.Text = "部门【" + this.userInfo.DeptName + "】";
                this.stlbUser.Text = "用户【" + this.userInfo.UserName + "】";
            }
            this.stlbDate.Text = "日期【" + DateTime.Now.ToString("yyyy-MM-dd") + "】";
            string str = "";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    str = "日";
                    break;

                case DayOfWeek.Monday:
                    str = "一";
                    break;

                case DayOfWeek.Tuesday:
                    str = "二";
                    break;

                case DayOfWeek.Wednesday:
                    str = "三";
                    break;

                case DayOfWeek.Thursday:
                    str = "四";
                    break;

                case DayOfWeek.Friday:
                    str = "五";
                    break;

                case DayOfWeek.Saturday:
                    str = "六";
                    break;
            }
            this.stlbWeek.Text = "【星期" + str + "】";
            this.GetUserRights(this.userInfo);
            this.frmMain_SizeChanged(null, null);
            string sSql = "select isnull(cParValue,0) cParValue from TPS_SysPar where cParId='bIsDoMidDBByForeGround'";
            string sErr = "";
            bool flag = false;
            object objValue = null;
            flag = DBFuns.GetValueBySql(sSql, "", "cParValue", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (flag && (objValue != null))
            {
                this.bIsUseMIDForeGround = Convert.ToInt16(objValue) == 1;
            }
            sSql = "select isnull(cParValue,0) cParValue from TPS_SysPar where cParId='nIsLinkMis'";
            flag = DBFuns.GetValueBySql(sSql, "", "cParValue", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (flag && (objValue != null))
            {
                this.bIsUseIOMIDDB = Convert.ToInt16(objValue) == 1;
            }
            this.tmrMain.Enabled = this.bIsUseIOMIDDB;
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            int left = base.Left;
            int top = base.Top;
            if (base.Width < this.imgBackground.Width)
            {
                left = 0;
            }
            else
            {
                left = (base.Width - this.imgBackground.Width) / 2;
            }
            if (base.Height < this.imgBackground.Height)
            {
                top = 0;
            }
            else
            {
                top = base.Top + 2;
            }
            this.imgBackground.Left = left;
            this.imgBackground.Top = top;
        }

        private void GetMIDDB()
        {
            StringBuilder builder = new StringBuilder("");
            bool flag = false;
            object objValue = null;
            string sErr = "";
            builder.Remove(0, builder.Length);
            this.nCount_IF_BillIn = 0;
            this.nCount_IF_BillOut = 0;
            this.nCount_IF_BillCheck = 0;
            this.nCount_IF_BillRemove = 0;
            builder.Append("select count(*) nCount from TMID_BillIn where nRWTag=0 and nBClass=1");
            flag = DBFuns.GetValueBySql(this.ainfo.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (flag && (objValue != null))
            {
                this.nCount_IF_BillIn = Convert.ToInt32(objValue);
            }
            builder.Remove(0, builder.Length);
            builder.Append("select count(*) nCount from TMID_BillIn where nRWTag=0 and nBClass=2");
            flag = DBFuns.GetValueBySql(this.ainfo.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (flag && (objValue != null))
            {
                this.nCount_IF_BillOut = Convert.ToInt32(objValue);
            }
            builder.Remove(0, builder.Length);
            builder.Append("select count(*) nCount from TMID_BillCheck where nRWTag=0");
            flag = DBFuns.GetValueBySql(this.ainfo.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (flag && (objValue != null))
            {
                this.nCount_IF_BillCheck = Convert.ToInt32(objValue);
            }
            builder.Remove(0, builder.Length);
            builder.Append("select count(*) nCount from TMID_BillRemove where nRWTag=0");
            flag = DBFuns.GetValueBySql(this.ainfo.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (flag && (objValue != null))
            {
                this.nCount_IF_BillRemove = Convert.ToInt32(objValue);
            }
            builder.Remove(0, builder.Length);
            builder = null;
        }

        public bool GetUserRights(WMSUserInfo uinfo)
        {
            StringBuilder builder = new StringBuilder("");
            ToolStripMenuItem item = null;
            this.mmMain.Items.Clear();
            if (uinfo != null)
            {
                if (uinfo.UType == UserType.utSupervisor)
                {
                    builder.Append("select * from tpb_rights where cRType <= 2");
                }
                else
                {
                    builder.Append("select * from tpb_rights where ( cRType <= 2) and cRId in (select cRId from tpb_Urts where cUserId='" + uinfo.UserId + "')");
                }
                builder.Append(" order by nSort,cRId");
                DataSet set = null;
                SeDBClient client = new SeDBClient(DBSocketServerType.dbsstDotNet);
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    FromSysType = "DotNet",
                    PageIndex = 0,
                    PageSize = 0,
                    SqlText = builder.ToString(),
                    SqlType = SqlCommandType.sctSql,
                    MyEncoding = Encoding.UTF8
                };
                string sErr = "";
                set = client.GetDataSet(this.ainfo.SvrSocket, cmdInfo, false, out sErr);
                if (sErr.Length > 0)
                {
                    MessageBox.Show(sErr);
                }
                if (set != null)
                {
                    DataTable tbX = set.Tables["data"];
                    if ((tbX != null) && (tbX.Rows.Count > 0))
                    {
                        DataRow[] rowArray = tbX.Select("cPRId='0'");
                        if ((rowArray != null) && (rowArray.Length > 0))
                        {
                            foreach (DataRow row in rowArray)
                            {
                                item = null;
                                item = new ToolStripMenuItem {
                                    Name = row["cRId"].ToString(),
                                    Text = row["cName"].ToString(),
                                    Tag = row["cRId"].ToString(),
                                    ShowShortcutKeys = false
                                };
                                int num = 0;
                                if (row["cRType"] != null)
                                {
                                    num = int.Parse(row["cRType"].ToString());
                                }
                                if (num == 1)
                                {
                                    item.Click += new EventHandler(this.DoMenuItemClick);
                                    item.ToolTipText = item.Text;
                                }
                                this.mmMain.Items.Add(item);
                                this.LoadUserMenuItem(item, tbX);
                            }
                        }
                    }
                }
            }
            ToolStripMenuItem item2 = null;
            item = new ToolStripMenuItem {
                Name = "mnHelp",
                Text = "帮助"
            };
            this.mmMain.Items.Add(item);
            item2 = new ToolStripMenuItem {
                Name = "mnHelp_Help",
                Text = "帮助",
                ToolTipText = item.Text
            };
            item2.Click += new EventHandler(this.DoHelpClick);
            item.DropDownItems.Add(item2);
            item2 = null;
            item.DropDownItems.Add(this.mi_Help_Abot);
            //item = null;
            item = new ToolStripMenuItem {
                Name = "mnExit",
                Text = "退出",
                ToolTipText = item.Text
            };
            item.Click += new EventHandler(this.DoExitClick);
            this.mmMain.Items.Add(item);
            return false;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmMain));
            this.mmMain = new MenuStrip();
            this.mToolStripMenuItem = new ToolStripMenuItem();
            this.基础信息管理ToolStripMenuItem = new ToolStripMenuItem();
            this.dddToolStripMenuItem = new ToolStripMenuItem();
            this.ddToolStripMenuItem = new ToolStripMenuItem();
            this.ddToolStripMenuItem1 = new ToolStripMenuItem();
            this.立库管理ToolStripMenuItem = new ToolStripMenuItem();
            this.平库管理ToolStripMenuItem = new ToolStripMenuItem();
            this.dPS库管理ToolStripMenuItem = new ToolStripMenuItem();
            this.库存管理ToolStripMenuItem = new ToolStripMenuItem();
            this.eRP对接ToolStripMenuItem = new ToolStripMenuItem();
            this.综合查询ToolStripMenuItem = new ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new ToolStripMenuItem();
            this.mi_Help_Abot = new ToolStripMenuItem();
            this.sttMain = new StatusStrip();
            this.stlbCompany = new ToolStripStatusLabel();
            this.stlbDept = new ToolStripStatusLabel();
            this.stlbUser = new ToolStripStatusLabel();
            this.stlbDate = new ToolStripStatusLabel();
            this.stlbWeek = new ToolStripStatusLabel();
            this.stBtnErpIF = new ToolStripDropDownButton();
            this.stMi_BillIn = new ToolStripMenuItem();
            this.stMi_BillOut = new ToolStripMenuItem();
            this.stMi_BillCheck = new ToolStripMenuItem();
            this.stMi_BillRemove = new ToolStripMenuItem();
            this.pnlBackground = new Panel();
            this.imgBackground = new PictureBox();
            this.tmrMain = new Timer(this.components);
            this.bkWorker = new BackgroundWorker();
            this.mmMain.SuspendLayout();
            this.sttMain.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            ((ISupportInitialize) this.imgBackground).BeginInit();
            base.SuspendLayout();
            this.mmMain.Items.AddRange(new ToolStripItem[] { this.mToolStripMenuItem, this.基础信息管理ToolStripMenuItem, this.立库管理ToolStripMenuItem, this.平库管理ToolStripMenuItem, this.dPS库管理ToolStripMenuItem, this.库存管理ToolStripMenuItem, this.eRP对接ToolStripMenuItem, this.综合查询ToolStripMenuItem, this.帮助ToolStripMenuItem });
            this.mmMain.Location = new Point(0, 0);
            this.mmMain.Name = "mmMain";
            this.mmMain.Size = new Size(0x453, 0x18);
            this.mmMain.TabIndex = 0;
            this.mmMain.Text = "menuStrip1";
            this.mToolStripMenuItem.Name = "mToolStripMenuItem";
            this.mToolStripMenuItem.Size = new Size(0x41, 20);
            this.mToolStripMenuItem.Text = "系统管理";
            this.基础信息管理ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.dddToolStripMenuItem });
            this.基础信息管理ToolStripMenuItem.Name = "基础信息管理ToolStripMenuItem";
            this.基础信息管理ToolStripMenuItem.Size = new Size(0x59, 20);
            this.基础信息管理ToolStripMenuItem.Text = "基础信息管理";
            this.dddToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.ddToolStripMenuItem, this.ddToolStripMenuItem1 });
            this.dddToolStripMenuItem.Name = "dddToolStripMenuItem";
            this.dddToolStripMenuItem.Size = new Size(0x58, 0x16);
            this.dddToolStripMenuItem.Text = "ddd";
            this.ddToolStripMenuItem.Name = "ddToolStripMenuItem";
            this.ddToolStripMenuItem.Size = new Size(0x52, 0x16);
            this.ddToolStripMenuItem.Text = "dd";
            this.ddToolStripMenuItem1.Name = "ddToolStripMenuItem1";
            this.ddToolStripMenuItem1.Size = new Size(0x52, 0x16);
            this.ddToolStripMenuItem1.Text = "dd";
            this.立库管理ToolStripMenuItem.Name = "立库管理ToolStripMenuItem";
            this.立库管理ToolStripMenuItem.Size = new Size(0x41, 20);
            this.立库管理ToolStripMenuItem.Text = "立库管理";
            this.平库管理ToolStripMenuItem.Name = "平库管理ToolStripMenuItem";
            this.平库管理ToolStripMenuItem.Size = new Size(0x41, 20);
            this.平库管理ToolStripMenuItem.Text = "平库管理";
            this.dPS库管理ToolStripMenuItem.Name = "dPS库管理ToolStripMenuItem";
            this.dPS库管理ToolStripMenuItem.Size = new Size(0x47, 20);
            this.dPS库管理ToolStripMenuItem.Text = "DPS库管理";
            this.库存管理ToolStripMenuItem.Name = "库存管理ToolStripMenuItem";
            this.库存管理ToolStripMenuItem.Size = new Size(0x41, 20);
            this.库存管理ToolStripMenuItem.Text = "库存管理";
            this.eRP对接ToolStripMenuItem.Name = "eRP对接ToolStripMenuItem";
            this.eRP对接ToolStripMenuItem.Size = new Size(0x3b, 20);
            this.eRP对接ToolStripMenuItem.Text = "ERP对接";
            this.综合查询ToolStripMenuItem.Name = "综合查询ToolStripMenuItem";
            this.综合查询ToolStripMenuItem.Size = new Size(0x41, 20);
            this.综合查询ToolStripMenuItem.Text = "综合查询";
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.mi_Help_Abot });
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new Size(0x29, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.mi_Help_Abot.Name = "mi_Help_Abot";
            this.mi_Help_Abot.Size = new Size(0x5e, 0x16);
            this.mi_Help_Abot.Text = "关于";
            this.mi_Help_Abot.Click += new EventHandler(this.mi_Help_Abot_Click);
            this.sttMain.Items.AddRange(new ToolStripItem[] { this.stlbCompany, this.stlbDept, this.stlbUser, this.stlbDate, this.stlbWeek, this.stBtnErpIF });
            this.sttMain.Location = new Point(0, 0x220);
            this.sttMain.Name = "sttMain";
            this.sttMain.Size = new Size(0x453, 0x16);
            this.sttMain.TabIndex = 1;
            this.sttMain.Text = "statusStrip1";
            this.stlbCompany.Name = "stlbCompany";
            this.stlbCompany.Size = new Size(0x83, 0x11);
            this.stlbCompany.Text = "toolStripStatusLabel1";
            this.stlbDept.Name = "stlbDept";
            this.stlbDept.Size = new Size(0x83, 0x11);
            this.stlbDept.Text = "toolStripStatusLabel1";
            this.stlbUser.Name = "stlbUser";
            this.stlbUser.Size = new Size(0x83, 0x11);
            this.stlbUser.Text = "toolStripStatusLabel1";
            this.stlbDate.Name = "stlbDate";
            this.stlbDate.Size = new Size(0x83, 0x11);
            this.stlbDate.Text = "toolStripStatusLabel1";
            this.stlbWeek.Name = "stlbWeek";
            this.stlbWeek.Size = new Size(0x83, 0x11);
            this.stlbWeek.Text = "toolStripStatusLabel1";
            this.stBtnErpIF.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.stBtnErpIF.DropDownItems.AddRange(new ToolStripItem[] { this.stMi_BillIn, this.stMi_BillOut, this.stMi_BillCheck, this.stMi_BillRemove });
            this.stBtnErpIF.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.stBtnErpIF.ForeColor = SystemColors.ActiveCaption;
            this.stBtnErpIF.Image = (Image) manager.GetObject("stBtnErpIF.Image");
            this.stBtnErpIF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stBtnErpIF.Name = "stBtnErpIF";
            this.stBtnErpIF.Size = new Size(0x60, 20);
            this.stBtnErpIF.Text = "有新数据导入";
            this.stBtnErpIF.Visible = false;
            this.stMi_BillIn.Name = "stMi_BillIn";
            this.stMi_BillIn.Size = new Size(0x87, 0x16);
            this.stMi_BillIn.Text = "入库单数据";
            this.stMi_BillOut.Name = "stMi_BillOut";
            this.stMi_BillOut.Size = new Size(0x87, 0x16);
            this.stMi_BillOut.Text = "出库单数据";
            this.stMi_BillCheck.Name = "stMi_BillCheck";
            this.stMi_BillCheck.Size = new Size(0x87, 0x16);
            this.stMi_BillCheck.Text = "盘点单数据";
            this.stMi_BillRemove.Name = "stMi_BillRemove";
            this.stMi_BillRemove.Size = new Size(0x87, 0x16);
            this.stMi_BillRemove.Text = "调拨单数据";
            this.pnlBackground.BackColor = System.Drawing.Color.Lavender;
            this.pnlBackground.BackgroundImage = (Image) manager.GetObject("pnlBackground.BackgroundImage");
            this.pnlBackground.BackgroundImageLayout = ImageLayout.Stretch;
            this.pnlBackground.Controls.Add(this.imgBackground);
            this.pnlBackground.Dock = DockStyle.Fill;
            this.pnlBackground.Location = new Point(0, 0x18);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new Size(0x453, 520);
            this.pnlBackground.TabIndex = 3;
            this.imgBackground.Dock = DockStyle.Fill;
            this.imgBackground.Location = new Point(0, 0);
            this.imgBackground.Name = "imgBackground";
            this.imgBackground.Size = new Size(0x453, 520);
            this.imgBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            this.imgBackground.TabIndex = 0;
            this.imgBackground.TabStop = false;
            this.tmrMain.Interval = 0x1388;
            this.tmrMain.Tick += new EventHandler(this.tmrMain_Tick);
            this.bkWorker.DoWork += new DoWorkEventHandler(this.bkWorker_DoWork);
            this.bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bkWorker_RunWorkerCompleted);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x453, 0x236);
            base.Controls.Add(this.pnlBackground);
            base.Controls.Add(this.sttMain);
            base.Controls.Add(this.mmMain);
            this.DoubleBuffered = true;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MainMenuStrip = this.mmMain;
            base.Name = "frmMain";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "自动化仓储管理系统";
            base.WindowState = FormWindowState.Maximized;
            base.SizeChanged += new EventHandler(this.frmMain_SizeChanged);
            base.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
            base.KeyDown += new KeyEventHandler(this.frmMain_KeyDown);
            base.Load += new EventHandler(this.frmMain_Load);
            this.mmMain.ResumeLayout(false);
            this.mmMain.PerformLayout();
            this.sttMain.ResumeLayout(false);
            this.sttMain.PerformLayout();
            this.pnlBackground.ResumeLayout(false);
            ((ISupportInitialize) this.imgBackground).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadUserMenuItem(ToolStripMenuItem mnItem, DataTable tbX)
        {
            if (((mnItem != null) && (mnItem.Tag != null)) && ((tbX != null) && (tbX.Rows.Count != 0)))
            {
                string name = mnItem.Name;
                int num = 0;
                DataRow[] rowArray = tbX.Select("cPRId='" + name + "'");
                if ((rowArray != null) && (rowArray.Length > 0))
                {
                    foreach (DataRow row in rowArray)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem {
                            Name = row["cRId"].ToString(),
                            Text = row["cName"].ToString()
                        };
                        if (row["cRCode"] != null)
                        {
                            item.Tag = row["cRCode"].ToString();
                        }
                        item.ShowShortcutKeys = false;
                        num = 0;
                        if (row["cRType"] != null)
                        {
                            num = int.Parse(row["cRType"].ToString());
                        }
                        if (num <= 2)
                        {
                            item.Click += new EventHandler(this.DoMenuItemClick);
                            item.ToolTipText = item.Text;
                        }
                        mnItem.DropDownItems.Add(item);
                        this.LoadUserMenuItem(item, tbX);
                    }
                }
            }
        }

        private void mi_Help_Abot_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
            about.Dispose();
        }

        private void rohua()
        {
            if (this.imgBackground.Image != null)
            {
                int height = this.imgBackground.Image.Height;
                int width = this.imgBackground.Image.Width;
                Bitmap bitmap = new Bitmap(width, height);
                Bitmap image = (Bitmap) this.imgBackground.Image;
                int[] numArray = new int[] { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
                for (int i = 1; i < (width - 1); i++)
                {
                    for (int j = 1; j < (height - 1); j++)
                    {
                        int red = 0;
                        int green = 0;
                        int blue = 0;
                        int index = 0;
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int m = -1; m <= 1; m++)
                            {
                                System.Drawing.Color pixel = image.GetPixel(i + m, j + k);
                                red += pixel.R * numArray[index];
                                green += pixel.G * numArray[index];
                                blue += pixel.B * numArray[index];
                                index++;
                            }
                        }
                        red /= 0x10;
                        green /= 0x10;
                        blue /= 0x10;
                        red = (red > 0xff) ? 0xff : red;
                        red = (red < 0) ? 0 : red;
                        green = (green > 0xff) ? 0xff : green;
                        green = (green < 0) ? 0 : green;
                        blue = (blue > 0xff) ? 0xff : blue;
                        blue = (blue < 0) ? 0 : blue;
                        bitmap.SetPixel(i - 1, j - 1, System.Drawing.Color.FromArgb(red, green, blue));
                    }
                }
                this.imgBackground.Image = bitmap;
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            this.tmrMain.Enabled = false;
            if (!this.bkWorker.CancellationPending)
            {
                this.bkWorker.RunWorkerAsync();
            }
        }

        public WMSAppInfo AInfo
        {
            get
            {
                return this.ainfo;
            }
            set
            {
                this.ainfo = value;
            }
        }

        public WMSUserInfo UserInfo
        {
            get
            {
                return this.userInfo;
            }
            set
            {
                this.userInfo = value;
            }
        }
    }
}

