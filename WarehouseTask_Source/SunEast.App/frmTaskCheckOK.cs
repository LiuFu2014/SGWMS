namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;
    using CommBase;

    public class frmTaskCheckOK : FrmSTable
    {
        private bool bIsOpenForDtl = false;
        private bool bIsOpenForMain = false;
        private BindingSource bsDtl;
        private BindingSource bsMain;
        private Button btn_Close;
        private Button btnOK;
        private DataGridViewTextBoxColumn colcBatchNo;
        private DataGridViewTextBoxColumn colcBNo;
        private DataGridViewTextBoxColumn colcBNoIn;
        private DataGridViewTextBoxColumn colcBoxId;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn colcName;
        private DataGridViewTextBoxColumn colcOptDesc;
        private DataGridViewTextBoxColumn colcOptTypeDesc;
        private DataGridViewTextBoxColumn colcPosId;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn colcSysType;
        private DataGridViewTextBoxColumn colcUnit;
        private DataGridViewTextBoxColumn colcUser;
        private DataGridViewTextBoxColumn colcWHId;
        private DataGridViewTextBoxColumn colcWHType;
        private DataGridViewTextBoxColumn colcWKStatusDesc;
        private DataGridViewTextBoxColumn colfQty;
        private DataGridViewTextBoxColumn colnItem;
        private DataGridViewTextBoxColumn colnItemIn;
        private DataGridViewTextBoxColumn colnPalletId;
        private DataGridViewTextBoxColumn colnWorkId;
        private IContainer components = null;
        private DataGridView grdDtl;
        private DataGridView grdMain;
        private GroupBox groupBox1;
        private Panel panel1;
        private string strTbDtl = "V_WorkTaskDtl";
        private string strTbMain = "V_WorkTask";

        public frmTaskCheckOK()
        {
            this.InitializeComponent();
        }

        private void bsMain_PositionChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenForMain)
            {
                string str = "";
                DataRowView current = (DataRowView) this.bsMain.Current;
                if (this.bsMain.Count > 0)
                {
                    str = current["nWorkId"].ToString().Trim();
                }
                this.OpenWorkDtlList(str.Trim());
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.bsMain.Count == 0)
            {
                MessageBox.Show("无任务数据！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bsMain.Current;
                if (current == null)
                {
                    MessageBox.Show("无任务数据！");
                }
                else
                {
                    string str = current["nWorkId"].ToString().Trim();
                    string sErr = "";
                    if (current["nWHType"].ToString().Trim().Trim() != "1")
                    {
                        MessageBox.Show("属于平库任务，系统将进行直接过账！");
                    }
                    string str4 = PubDBCommFuns.sp_Do_WKTaskDtlIsOK(base.AppInformation.SvrSocket, int.Parse(str.Trim()), base.UserInformation.UserName.Trim(), base.UserInformation.UnitId, "WMS", out sErr);
                    if (((str4.Trim() != "0") && (str4.Trim() != "")) && (sErr.Trim() != ""))
                    {
                        MessageBox.Show(sErr);
                    }
                    else
                    {
                        MessageBox.Show("任务确认成功！");
                        this.OpenWorkTaskList();
                    }
                }
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

        private void frmTaskCheckOK_Load(object sender, EventArgs e)
        {
            this.OpenWorkTaskList();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.btn_Close = new Button();
            this.btnOK = new Button();
            this.grdMain = new DataGridView();
            this.colnWorkId = new DataGridViewTextBoxColumn();
            this.colcSysType = new DataGridViewTextBoxColumn();
            this.colcWHId = new DataGridViewTextBoxColumn();
            this.colcPosId = new DataGridViewTextBoxColumn();
            this.colnPalletId = new DataGridViewTextBoxColumn();
            this.colcUser = new DataGridViewTextBoxColumn();
            this.colcWHType = new DataGridViewTextBoxColumn();
            this.colcOptTypeDesc = new DataGridViewTextBoxColumn();
            this.colcWKStatusDesc = new DataGridViewTextBoxColumn();
            this.grdDtl = new DataGridView();
            this.colcBNo = new DataGridViewTextBoxColumn();
            this.colnItem = new DataGridViewTextBoxColumn();
            this.colcMNo = new DataGridViewTextBoxColumn();
            this.colcName = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.colcBatchNo = new DataGridViewTextBoxColumn();
            this.colfQty = new DataGridViewTextBoxColumn();
            this.colcUnit = new DataGridViewTextBoxColumn();
            this.colcOptDesc = new DataGridViewTextBoxColumn();
            this.colcBoxId = new DataGridViewTextBoxColumn();
            this.colcBNoIn = new DataGridViewTextBoxColumn();
            this.colnItemIn = new DataGridViewTextBoxColumn();
            this.bsMain = new BindingSource(this.components);
            this.bsDtl = new BindingSource(this.components);
            this.groupBox1 = new GroupBox();
            this.panel1 = new Panel();
            ((ISupportInitialize) this.grdMain).BeginInit();
            ((ISupportInitialize) this.grdDtl).BeginInit();
            ((ISupportInitialize) this.bsMain).BeginInit();
            ((ISupportInitialize) this.bsDtl).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btn_Close.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Close.ForeColor = SystemColors.ActiveCaption;
            this.btn_Close.Location = new Point(0x2b6, 12);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new Size(0x4b, 0x17);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "退出(&C)";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new EventHandler(this.btn_Close_Click);
            this.btnOK.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnOK.ForeColor = SystemColors.ActiveCaption;
            this.btnOK.Location = new Point(0x257, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.grdMain.AllowUserToAddRows = false;
            this.grdMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMain.Columns.AddRange(new DataGridViewColumn[] { this.colnWorkId, this.colcSysType, this.colcWHId, this.colcPosId, this.colnPalletId, this.colcUser, this.colcWHType, this.colcOptTypeDesc, this.colcWKStatusDesc });
            this.grdMain.Dock = DockStyle.Fill;
            this.grdMain.Location = new Point(0, 0x2d);
            this.grdMain.Name = "grdMain";
            this.grdMain.ReadOnly = true;
            this.grdMain.RowHeadersVisible = false;
            this.grdMain.RowTemplate.Height = 0x17;
            this.grdMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdMain.Size = new Size(0x30d, 0xe0);
            this.grdMain.TabIndex = 0;
            this.colnWorkId.DataPropertyName = "nWorkId";
            this.colnWorkId.HeaderText = "任务号";
            this.colnWorkId.Name = "colnWorkId";
            this.colnWorkId.ReadOnly = true;
            this.colcSysType.DataPropertyName = "cSysType";
            this.colcSysType.HeaderText = "任务系统";
            this.colcSysType.Name = "colcSysType";
            this.colcSysType.ReadOnly = true;
            this.colcWHId.DataPropertyName = "cWHId";
            this.colcWHId.HeaderText = "仓库";
            this.colcWHId.Name = "colcWHId";
            this.colcWHId.ReadOnly = true;
            this.colcPosId.DataPropertyName = "cPosId";
            this.colcPosId.HeaderText = "货位号";
            this.colcPosId.Name = "colcPosId";
            this.colcPosId.ReadOnly = true;
            this.colnPalletId.DataPropertyName = "nPalletId";
            this.colnPalletId.HeaderText = "托盘号";
            this.colnPalletId.Name = "colnPalletId";
            this.colnPalletId.ReadOnly = true;
            this.colcUser.DataPropertyName = "cUser";
            this.colcUser.HeaderText = "操作员";
            this.colcUser.Name = "colcUser";
            this.colcUser.ReadOnly = true;
            this.colcWHType.DataPropertyName = "cWHType";
            this.colcWHType.HeaderText = "仓库类型";
            this.colcWHType.Name = "colcWHType";
            this.colcWHType.ReadOnly = true;
            this.colcOptTypeDesc.DataPropertyName = "cOptTypeDesc";
            this.colcOptTypeDesc.HeaderText = "操作类型";
            this.colcOptTypeDesc.Name = "colcOptTypeDesc";
            this.colcOptTypeDesc.ReadOnly = true;
            this.colcWKStatusDesc.DataPropertyName = "cWKStatusDesc";
            this.colcWKStatusDesc.HeaderText = "工作状态";
            this.colcWKStatusDesc.Name = "colcWKStatusDesc";
            this.colcWKStatusDesc.ReadOnly = true;
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDtl.Columns.AddRange(new DataGridViewColumn[] { this.colcBNo, this.colnItem, this.colcMNo, this.colcName, this.colcSpec, this.colcBatchNo, this.colfQty, this.colcUnit, this.colcOptDesc, this.colcBoxId, this.colcBNoIn, this.colnItemIn });
            this.grdDtl.Dock = DockStyle.Fill;
            this.grdDtl.Location = new Point(3, 0x11);
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 0x17;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdDtl.Size = new Size(0x307, 0xe4);
            this.grdDtl.TabIndex = 1;
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.HeaderText = "单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            this.colnItem.DataPropertyName = "nItem";
            this.colnItem.HeaderText = "明细序";
            this.colnItem.Name = "colnItem";
            this.colnItem.ReadOnly = true;
            this.colnItem.Width = 0x41;
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料号";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcName.DataPropertyName = "cName";
            this.colcName.HeaderText = "名称";
            this.colcName.Name = "colcName";
            this.colcName.ReadOnly = true;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            this.colcBatchNo.Width = 70;
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.Width = 70;
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.Width = 0x41;
            this.colcOptDesc.DataPropertyName = "cOptDesc";
            this.colcOptDesc.HeaderText = "操作";
            this.colcOptDesc.Name = "colcOptDesc";
            this.colcOptDesc.ReadOnly = true;
            this.colcOptDesc.Width = 0x4b;
            this.colcBoxId.DataPropertyName = "cBoxId";
            this.colcBoxId.HeaderText = "料箱号";
            this.colcBoxId.Name = "colcBoxId";
            this.colcBoxId.ReadOnly = true;
            this.colcBoxId.Width = 0x41;
            this.colcBNoIn.DataPropertyName = "cBNoIn";
            this.colcBNoIn.HeaderText = "库存单号";
            this.colcBNoIn.Name = "colcBNoIn";
            this.colcBNoIn.ReadOnly = true;
            this.colnItemIn.DataPropertyName = "nItemIn";
            this.colnItemIn.HeaderText = "库存明细序";
            this.colnItemIn.Name = "colnItemIn";
            this.colnItemIn.ReadOnly = true;
            this.bsMain.AllowNew = false;
            this.bsMain.PositionChanged += new EventHandler(this.bsMain_PositionChanged);
            this.bsDtl.AllowNew = false;
            this.groupBox1.Controls.Add(this.grdDtl);
            this.groupBox1.Dock = DockStyle.Bottom;
            this.groupBox1.Location = new Point(0, 0x10d);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x30d, 0xf8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务明细";
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btn_Close);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x30d, 0x2d);
            this.panel1.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x30d, 0x205);
            base.Controls.Add(this.grdMain);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.groupBox1);
            base.MinimizeBox = false;
            base.Name = "frmTaskCheckOK";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "任务确认";
            base.Load += new EventHandler(this.frmTaskCheckOK_Load);
            ((ISupportInitialize) this.grdMain).EndInit();
            ((ISupportInitialize) this.grdDtl).EndInit();
            ((ISupportInitialize) this.bsMain).EndInit();
            ((ISupportInitialize) this.bsDtl).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void OpenWorkDtlList(string sWorkId)
        {
            string sErr = "";
            string sSql = "select * from " + this.strTbDtl + " where nWorkId='" + sWorkId.Trim() + "'";
            Cursor.Current = Cursors.WaitCursor;
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbDtl, 0, 0, "", out sErr);
            Cursor.Current = Cursors.Default;
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开任务明细时，出错！");
            }
            else
            {
                this.bIsOpenForDtl = false;
                this.grdDtl.AutoGenerateColumns = false;
                this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.bsDtl.DataSource = set.Tables[this.strTbDtl];
                this.grdDtl.DataSource = this.bsDtl;
                this.bIsOpenForDtl = true;
            }
        }

        private void OpenWorkTaskList()
        {
            string sErr = "";
            string sSql = "select * from " + this.strTbMain + " where isnull(bIsAllowReturnBack,0)  < 1 and  (nWKStatus between 1 and 98 )";
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cUser='" + base.UserInformation.UserName.Trim() + "'";
            }
            Cursor.Current = Cursors.WaitCursor;
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbMain, 0, 0, "", out sErr);
            Cursor.Current = Cursors.Default;
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开任务主表时，出错！");
            }
            else
            {
                this.bIsOpenForMain = false;
                this.grdMain.AutoGenerateColumns = false;
                this.grdMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.bsMain.DataSource = set.Tables[this.strTbMain];
                this.grdMain.DataSource = this.bsMain;
                this.bIsOpenForMain = true;
                this.bsMain_PositionChanged(null, null);
            }
        }
    }
}

