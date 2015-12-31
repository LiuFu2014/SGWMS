namespace App
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using FileFun;

    public class frmPromptForIOMIDData : FrmSTable
    {
        private bool _IsDoDBForeGround = false;
        private ShowFormClose _ShowformClose = null;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        public Label lbl_Count_Check;
        public Label lbl_Count_In;
        public Label lbl_Count_Out;
        public Label lbl_Count_Remove;
        private int nCount_IF_BillCheck = 0;
        private int nCount_IF_BillIn = 0;
        private int nCount_IF_BillOut = 0;
        private int nCount_IF_BillRemove = 0;
        private int nCounter = 0;
        private Timer tmrMain;

        public frmPromptForIOMIDData()
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

        private void frmPromptForIOMIDData_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this._ShowformClose != null)
            {
                this._ShowformClose(sender);
            }
        }

        private void frmPromptForIOMIDData_Load(object sender, EventArgs e)
        {
            this.tmrMain.Enabled = !this._IsDoDBForeGround;
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
            flag = DBFuns.GetValueBySql(base.AppInformation.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
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
            flag = DBFuns.GetValueBySql(base.AppInformation.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
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
            flag = DBFuns.GetValueBySql(base.AppInformation.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
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
            flag = DBFuns.GetValueBySql(base.AppInformation.SvrSocket, builder.ToString(), "", "nCount", out objValue, out sErr);
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

        private void InitializeComponent()
        {
            this.components = new Container();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.lbl_Count_In = new Label();
            this.lbl_Count_Out = new Label();
            this.lbl_Count_Remove = new Label();
            this.lbl_Count_Check = new Label();
            this.tmrMain = new Timer(this.components);
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "入库单数据:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x83, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "出库单数据:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x83, 0x2f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "调拨单数据:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x2f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x47, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "盘点单数据:";
            this.lbl_Count_In.AutoSize = true;
            this.lbl_Count_In.Cursor = Cursors.Hand;
            this.lbl_Count_In.Font = new Font("宋体", 9f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_Count_In.ForeColor = SystemColors.ActiveCaption;
            this.lbl_Count_In.Location = new Point(0x59, 0x12);
            this.lbl_Count_In.Name = "lbl_Count_In";
            this.lbl_Count_In.Size = new Size(12, 12);
            this.lbl_Count_In.TabIndex = 4;
            this.lbl_Count_In.Text = "0";
            this.lbl_Count_In.Click += new EventHandler(this.lbl_Count_In_Click);
            this.lbl_Count_Out.AutoSize = true;
            this.lbl_Count_Out.Cursor = Cursors.Hand;
            this.lbl_Count_Out.Font = new Font("宋体", 9f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_Count_Out.ForeColor = SystemColors.ActiveCaption;
            this.lbl_Count_Out.Location = new Point(0xd0, 0x12);
            this.lbl_Count_Out.Name = "lbl_Count_Out";
            this.lbl_Count_Out.Size = new Size(12, 12);
            this.lbl_Count_Out.TabIndex = 5;
            this.lbl_Count_Out.Text = "0";
            this.lbl_Count_Out.Click += new EventHandler(this.lbl_Count_In_Click);
            this.lbl_Count_Remove.AutoSize = true;
            this.lbl_Count_Remove.Cursor = Cursors.Hand;
            this.lbl_Count_Remove.Font = new Font("宋体", 9f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_Count_Remove.ForeColor = SystemColors.ActiveCaption;
            this.lbl_Count_Remove.Location = new Point(0xd0, 0x2f);
            this.lbl_Count_Remove.Name = "lbl_Count_Remove";
            this.lbl_Count_Remove.Size = new Size(12, 12);
            this.lbl_Count_Remove.TabIndex = 6;
            this.lbl_Count_Remove.Text = "0";
            this.lbl_Count_Remove.Click += new EventHandler(this.lbl_Count_In_Click);
            this.lbl_Count_Check.AutoSize = true;
            this.lbl_Count_Check.Cursor = Cursors.Hand;
            this.lbl_Count_Check.Font = new Font("宋体", 9f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_Count_Check.ForeColor = SystemColors.ActiveCaption;
            this.lbl_Count_Check.Location = new Point(0x59, 0x2f);
            this.lbl_Count_Check.Name = "lbl_Count_Check";
            this.lbl_Count_Check.Size = new Size(12, 12);
            this.lbl_Count_Check.TabIndex = 7;
            this.lbl_Count_Check.Text = "0";
            this.lbl_Count_Check.Click += new EventHandler(this.lbl_Count_In_Click);
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 0x1388;
            this.tmrMain.Tick += new EventHandler(this.tmrMain_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0xf7, 0x55);
            base.Controls.Add(this.lbl_Count_Check);
            base.Controls.Add(this.lbl_Count_Remove);
            base.Controls.Add(this.lbl_Count_Out);
            base.Controls.Add(this.lbl_Count_In);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "frmPromptForIOMIDData";
            base.Opacity = 0.6;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "同步数据提醒";
            base.TopMost = true;
            base.TransparencyKey = Color.FromArgb(0xc0, 0xff, 0xff);
            base.Load += new EventHandler(this.frmPromptForIOMIDData_Load);
            base.FormClosed += new FormClosedEventHandler(this.frmPromptForIOMIDData_FormClosed);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lbl_Count_In_Click(object sender, EventArgs e)
        {
            if (this._IsDoDBForeGround)
            {
                this.tmrMain.Enabled = false;
                Label label = (Label) sender;
                string s = label.Text.Trim();
                if ((s != "") && (int.Parse(s) != 0))
                {
                    string name = label.Name;
                    string funName = "";
                    string str5 = name;
                    if (str5 != null)
                    {
                        if (!(str5 == "lbl_Count_In"))
                        {
                            if (str5 == "lbl_Count_Out")
                            {
                                funName = "DataImpBillOut";
                            }
                            else if (str5 == "lbl_Count_Remove")
                            {
                                funName = "DataImpBillRemove";
                            }
                            else if (str5 == "lbl_Count_Check")
                            {
                                funName = "DataImpBillCheck";
                            }
                        }
                        else
                        {
                            funName = "DataImpBillIn";
                        }
                    }
                    string path = base.AppInformation.AppPath + @"\DataInFromMid.dll";
                    if (File.Exists(path))
                    {
                        bool bIsOK = false;
                        object[] param = new object[] { base.AppInformation, base.UserInformation };
                        MyCallSafetyDll.DoCallMyDll(path, "DataInFromMid.DataInFromMid", funName, param, out bIsOK);
                    }
                    else
                    {
                        MessageBox.Show(path + "  不存在！");
                    }
                    this.tmrMain.Enabled = true;
                }
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            this.GetMIDDB();
            this.lbl_Count_Check.Text = this.nCount_IF_BillCheck.ToString();
            this.lbl_Count_In.Text = this.nCount_IF_BillIn.ToString();
            this.lbl_Count_Out.Text = this.nCount_IF_BillOut.ToString();
            this.lbl_Count_Remove.Text = this.nCount_IF_BillRemove.ToString();
            base.Update();
            base.TopMost = false;
            base.TopMost = true;
        }

        public bool IsDoDBForeGround
        {
            get
            {
                return this._IsDoDBForeGround;
            }
            set
            {
                this._IsDoDBForeGround = value;
            }
        }

        public ShowFormClose ShowformClose
        {
            get
            {
                return this._ShowformClose;
            }
            set
            {
                this._ShowformClose = value;
            }
        }
    }
}

