namespace WareStore
{
    using App;
    using DataImpExp;
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;
    using WareStore.Rpts;
    using Zqm.CommBase;

    public class frmRptInOutRece : FrmSTable
    {
        private ToolStripButton btn_M_Help;
        private Button btnFindInfo;
        private Button btnPrintReceSum;
        private Button btnPrintSum;
        private Button button1;
        private ComboBox cmbBillType;
        private ComboBox cmbUserId;
        private ComboBox cmbWHId;
        private DataGridViewTextBoxColumn cMNo;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column9;
        private IContainer components = null;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridView dgvInOutRece;
        private DataGridView dgvRillInfo;
        private SaveFileDialog dlgSave;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStatus;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label lblReceCount;
        private Label lblSumNum;
        private string maillerStr = "";
        private DataTable mydt = new DataTable();
        private DataTable mydtAll = new DataTable();
        private WMSAppInfo objApp;
        private WMSUserInfo objUser;
        private Panel panel1;
        private Panel panel2;
        private string receType = "";
        private string timeEnd = "";
        private string timeStatus = "";
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_Edit;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Find;
        public ToolStripButton tlb_M_New;
        public ToolStripButton tlb_M_Print;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Save;
        public ToolStripButton tlb_M_Undo;
        public ToolStrip tlbMain;
        private ToolStripButton tlbSaveSysRts;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private TextBox txtMNo;
        private string userId = "";
        private string userName = "";
        private string WHId = "";
        private string WHName = "";

        public frmRptInOutRece()
        {
            this.InitializeComponent();
        }

        private void BindData()
        {
            this.dtpStatus.Value = DateTime.Now.AddDays(-1.0);
            this.dtpEnd.Value = DateTime.Now;
            this.BindDataWHInfo();
            this.bindDataUserList();
        }

        private void bindDataUserList()
        {
            string sSql = string.Format("select cName,cUserId  from TPB_User where bUsed=1 ", this.objUser.DeptId);
            string sErr = "";
            if (this.objUser.UType == UserType.utAdmin)
            {
                sSql = sSql + string.Format(" and cDeptid='{0}' ", this.objUser.DeptId);
            }
            else if (this.objUser.UType == UserType.utNormal)
            {
                sSql = sSql + string.Format(" and cUserId='{0}' ", this.objUser.UserId);
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            switch (sErr)
            {
                case "":
                case "0":
                    this.cmbUserId.DisplayMember = "cName";
                    this.cmbUserId.ValueMember = "cUserId";
                    this.cmbUserId.DataSource = dataBySql.Tables["data"];
                    break;
            }
        }

        private void BindDataWHInfo()
        {
            string sSql = "select cwhid,cName from TWC_WareHouse where bUsed=1";
            if (this.objUser.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + this.objUser.UserId.Trim() + "')";
            }
            string sErr = "";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            switch (sErr)
            {
                case "":
                case "0":
                    this.cmbWHId.DisplayMember = "cName";
                    this.cmbWHId.ValueMember = "cwhid";
                    this.cmbWHId.DataSource = dataBySql.Tables["data"];
                    break;
            }
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            if (this.dgvInOutRece.Rows.Count == 0)
            {
                MyTools.MessageBox("当前没有数据可以进行导入！");
            }
            else if (this.dlgSave.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.dlgSave.FileName;
                DataIE.DataGridViewToExcel(this.dgvInOutRece, fileName, this.Text);
            }
        }

        private void btnFindInfo_Click(object sender, EventArgs e)
        {
            this.RefeashData();
        }

        private void btnPrintSum_Click(object sender, EventArgs e)
        {
            FrmRptShow.dsRpt = new DataSet();
            FrmRptShow.dsRpt.Tables.Add(this.mydt.Copy());
            FrmRptShow.dsRpt.Tables[0].TableName = "InOutRece";
            FrmRptShow.CountType = "0";
            FrmRptShow.Paramets = new string[] { this.timeStatus, this.timeEnd, this.userName, this.WHName, this.maillerStr };
            if (this.receType == "1")
            {
                FrmRptShow.rpsTitleStr = "入库物料汇总报表";
            }
            else if (this.receType == "2")
            {
                FrmRptShow.rpsTitleStr = "出库物料汇总报表";
            }
            else
            {
                FrmRptShow.rpsTitleStr = "出 / 入库物料汇总报表";
            }
            new FrmRptShow { Text = "出入库物料汇总" }.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtMNo.Text = "";
            this.cmbUserId.SelectedIndex = -1;
            this.cmbUserId.Text = "";
            this.cmbWHId.SelectedIndex = -1;
            this.cmbWHId.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.loadReceInfo("", true);
            FrmRptShow.dsRpt = new DataSet();
            FrmRptShow.dsRpt.Tables.Add(this.mydtAll.Copy());
            FrmRptShow.dsRpt.Tables[0].TableName = "InOutReceAll";
            FrmRptShow.CountType = "1";
            FrmRptShow.Paramets = new string[] { this.timeStatus, this.timeEnd, this.userName, this.WHName, this.maillerStr };
            if (this.receType == "1")
            {
                FrmRptShow.rpsTitleStr = "入库物料汇总报表";
            }
            else if (this.receType == "2")
            {
                FrmRptShow.rpsTitleStr = "出库物料汇总报表";
            }
            else
            {
                FrmRptShow.rpsTitleStr = "出 / 入库物料汇总报表";
            }
            new FrmRptShow { Text = "出入库物料明细汇总" }.ShowDialog();
        }

        private void dgvInOutRece_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvInOutRece.SelectedRows.Count == 0)
            {
                this.dgvRillInfo.DataSource = null;
            }
            else
            {
                string matId = "";
                if (this.dgvInOutRece.SelectedRows[0].Cells["cMNo"].Value != null)
                {
                    matId = this.dgvInOutRece.SelectedRows[0].Cells["cMNo"].Value.ToString();
                    this.loadReceInfo(matId);
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

        private void DisReceSun()
        {
            double num = 0.0;
            int count = this.mydt.Rows.Count;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    num += Convert.ToDouble(this.mydt.Rows[i]["fqty"].ToString());
                }
            }
            catch
            {
            }
            this.lblReceCount.Text = count + "";
            this.lblSumNum.Text = num + "";
        }

        private void frmInOutRece_Load(object sender, EventArgs e)
        {
            this.LoadBillClass();
            this.objApp = base.AppInformation;
            this.objUser = base.UserInformation;
            this.BindData();
            this.dgvInOutRece.AutoGenerateColumns = false;
            this.dgvRillInfo.AutoGenerateColumns = false;
            this.cmbBillType.SelectedIndex = 0;
            this.RefeashData();
        }

        private DataTable GetData()
        {
            this.GetUIValue();
            string sSql = string.Format("select his.*,mat.cname,mat.cspec,mat.cunit from (select a.cmno,sum(fqty) fqty from twb_stockdtl_his a inner join twb_billin b on a.cbno=b.cbno where dintime >= '{0}' and dintime <= '{1}' ", this.timeStatus, this.timeEnd);
            if (this.userName != "")
            {
                sSql = sSql + string.Format("  and b.cpayer='{0}' ", this.userName);
            }
            else
            {
                sSql = sSql + string.Format("  and b.cpayer in (select CNAME from TPB_USER where CDEPTID ='{0}') ", base.UserInformation.DeptId);
            }
            if (this.WHId != "")
            {
                sSql = sSql + string.Format("  and a.cwhid='{0}' ", this.WHId);
            }
            if (this.receType != "0")
            {
                sSql = sSql + string.Format(" and a.nBClass={0} ", this.receType);
            }
            sSql = sSql + string.Format(" group by a.cmno) his inner join tpc_material mat on his.cmno=mat.cmno where 0=0 ", new object[0]);
            if (this.maillerStr != "")
            {
                sSql = sSql + string.Format(" and (mat.cname like '%{0}%' or mat.cMNo like '%{1}%' or mat.cPYJM like '%{2}%' or mat.cWBJM like '%{3}%') ", new object[] { this.maillerStr, this.maillerStr, this.maillerStr, this.maillerStr });
            }
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "data", 0, 0, "dintime", out sErr);
            if ((sErr != "") && (sErr != "0"))
            {
                MyTools.MessageBox(sErr);
                return new DataTable();
            }
            return set.Tables["data"];
        }

        private void GetUIValue()
        {
            this.timeStatus = this.dtpStatus.Value.ToString("yyyy-MM-dd 00:00:00");
            this.timeEnd = this.dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59");
            this.receType = "0";
            if (((this.cmbBillType.Text.Trim() != "") && (this.cmbBillType.SelectedValue != null)) && (this.cmbBillType.SelectedValue.ToString() != "0"))
            {
                this.receType = this.cmbBillType.SelectedValue.ToString();
            }
            this.maillerStr = this.txtMNo.Text.Trim();
            this.WHId = "";
            if (this.cmbWHId.SelectedIndex != -1)
            {
                this.WHId = this.cmbWHId.SelectedValue.ToString();
            }
            this.WHName = this.cmbWHId.Text.ToString();
            this.userId = "";
            if (this.cmbUserId.SelectedIndex != -1)
            {
                this.userId = this.cmbUserId.SelectedValue.ToString();
            }
            this.userName = this.cmbUserId.Text.ToString();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmRptInOutRece));
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.panel1 = new Panel();
            this.btnPrintReceSum = new Button();
            this.btnPrintSum = new Button();
            this.btnFindInfo = new Button();
            this.button1 = new Button();
            this.cmbBillType = new ComboBox();
            this.label6 = new Label();
            this.cmbUserId = new ComboBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.dtpEnd = new DateTimePicker();
            this.dtpStatus = new DateTimePicker();
            this.label2 = new Label();
            this.tlbMain = new ToolStrip();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.tlb_M_New = new ToolStripButton();
            this.tlb_M_Edit = new ToolStripButton();
            this.tlb_M_Delete = new ToolStripButton();
            this.tlb_M_Undo = new ToolStripButton();
            this.tlb_M_Save = new ToolStripButton();
            this.tlb_M_Refresh = new ToolStripButton();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.tlb_M_Find = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tlb_M_Print = new ToolStripButton();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.tlbSaveSysRts = new ToolStripButton();
            this.txtMNo = new TextBox();
            this.label3 = new Label();
            this.cmbWHId = new ComboBox();
            this.label1 = new Label();
            this.dgvInOutRece = new DataGridView();
            this.cMNo = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.panel2 = new Panel();
            this.lblReceCount = new Label();
            this.lblSumNum = new Label();
            this.label9 = new Label();
            this.label7 = new Label();
            this.dgvRillInfo = new DataGridView();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.groupBox1 = new GroupBox();
            this.dlgSave = new SaveFileDialog();
            this.panel1.SuspendLayout();
            this.tlbMain.SuspendLayout();
            ((ISupportInitialize) this.dgvInOutRece).BeginInit();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.dgvRillInfo).BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(6, 0x19);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 0x19);
            this.panel1.Controls.Add(this.btnPrintReceSum);
            this.panel1.Controls.Add(this.btnPrintSum);
            this.panel1.Controls.Add(this.btnFindInfo);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmbBillType);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbUserId);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.dtpStatus);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tlbMain);
            this.panel1.Controls.Add(this.txtMNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbWHId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(890, 0x74);
            this.panel1.TabIndex = 8;
            this.btnPrintReceSum.Location = new Point(0x2cb, 0x4e);
            this.btnPrintReceSum.Name = "btnPrintReceSum";
            this.btnPrintReceSum.Size = new Size(0xa3, 0x17);
            this.btnPrintReceSum.TabIndex = 0x17;
            this.btnPrintReceSum.Text = "打印汇总明细报表";
            this.btnPrintReceSum.UseVisualStyleBackColor = true;
            this.btnPrintReceSum.Click += new EventHandler(this.button2_Click);
            this.btnPrintSum.Location = new Point(0x2cb, 0x31);
            this.btnPrintSum.Name = "btnPrintSum";
            this.btnPrintSum.Size = new Size(0xa3, 0x17);
            this.btnPrintSum.TabIndex = 0x17;
            this.btnPrintSum.Text = "打印汇总报表";
            this.btnPrintSum.UseVisualStyleBackColor = true;
            this.btnPrintSum.Click += new EventHandler(this.btnPrintSum_Click);
            this.btnFindInfo.Location = new Point(0x27a, 0x4f);
            this.btnFindInfo.Name = "btnFindInfo";
            this.btnFindInfo.Size = new Size(0x4b, 0x17);
            this.btnFindInfo.TabIndex = 0x16;
            this.btnFindInfo.Text = "查询(&F)";
            this.btnFindInfo.UseVisualStyleBackColor = true;
            this.btnFindInfo.Click += new EventHandler(this.btnFindInfo_Click);
            this.button1.Location = new Point(0x27a, 0x31);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x16;
            this.button1.Text = "重置(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.cmbBillType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbBillType.FormattingEnabled = true;
            this.cmbBillType.Items.AddRange(new object[] { "所有单据", "入库单据", "出库单据" });
            this.cmbBillType.Location = new Point(0x1e7, 0x30);
            this.cmbBillType.Name = "cmbBillType";
            this.cmbBillType.Size = new Size(0x79, 20);
            this.cmbBillType.TabIndex = 0x15;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x1ab, 0x34);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "单据类型";
            this.cmbUserId.FormattingEnabled = true;
            this.cmbUserId.Location = new Point(0x1e7, 80);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Size = new Size(0x79, 20);
            this.cmbUserId.TabIndex = 0x15;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x1ab, 0x54);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "仓管员";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xea, 0x34);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x13;
            this.label4.Text = "——";
            this.dtpEnd.Location = new Point(0x11c, 0x30);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new Size(0x79, 0x15);
            this.dtpEnd.TabIndex = 0x12;
            this.dtpStatus.Location = new Point(0x62, 0x30);
            this.dtpStatus.Name = "dtpStatus";
            this.dtpStatus.Size = new Size(0x79, 0x15);
            this.dtpStatus.TabIndex = 0x11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x26, 0x34);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "起止日期";
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.tlb_M_New, this.tlb_M_Edit, this.tlb_M_Delete, this.tlb_M_Undo, this.tlb_M_Save, this.tlb_M_Refresh, this.toolStripSeparator4, this.toolStripSeparator5, this.tlb_M_Find, this.toolStripSeparator1, this.toolStripSeparator3, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(890, 0x19);
            this.tlbMain.TabIndex = 15;
            this.tlbMain.Text = "toolStrip1";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.tlb_M_New.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_New.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_New.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_New.Image = (Image) manager.GetObject("tlb_M_New.Image");
            this.tlb_M_New.ImageTransparentColor = Color.Magenta;
            this.tlb_M_New.Name = "tlb_M_New";
            this.tlb_M_New.Size = new Size(0x23, 0x16);
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Visible = false;
            this.tlb_M_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = (Image) manager.GetObject("tlb_M_Edit.Image");
            this.tlb_M_Edit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new Size(0x3d, 0x16);
            this.tlb_M_Edit.Text = "手动过账";
            this.tlb_M_Edit.Visible = false;
            this.tlb_M_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = (Image) manager.GetObject("tlb_M_Delete.Image");
            this.tlb_M_Delete.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new Size(0x3d, 0x16);
            this.tlb_M_Delete.Text = "删除指令";
            this.tlb_M_Delete.Visible = false;
            this.tlb_M_Undo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = (Image) manager.GetObject("tlb_M_Undo.Image");
            this.tlb_M_Undo.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new Size(0x57, 0x16);
            this.tlb_M_Undo.Text = "取消未下指令";
            this.tlb_M_Undo.Visible = false;
            this.tlb_M_Save.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Save.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Save.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Save.Image = (Image) manager.GetObject("tlb_M_Save.Image");
            this.tlb_M_Save.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Save.Name = "tlb_M_Save";
            this.tlb_M_Save.Size = new Size(0x23, 0x16);
            this.tlb_M_Save.Text = "保存";
            this.tlb_M_Save.Visible = false;
            this.tlb_M_Refresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Refresh.Image = (Image) manager.GetObject("tlb_M_Refresh.Image");
            this.tlb_M_Refresh.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new Size(0x23, 0x16);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new EventHandler(this.tlb_M_Refresh_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(6, 0x19);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(6, 0x19);
            this.tlb_M_Find.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Find.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Find.Image = (Image) manager.GetObject("tlb_M_Find.Image");
            this.tlb_M_Find.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new Size(0x23, 0x16);
            this.tlb_M_Find.Text = "统计";
            this.tlb_M_Find.ToolTipText = "统计";
            this.tlb_M_Find.Click += new EventHandler(this.tlb_M_Find_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.tlb_M_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Print.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = (Image) manager.GetObject("tlb_M_Print.Image");
            this.tlb_M_Print.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new Size(0x23, 0x16);
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Click += new EventHandler(this.tlb_M_Print_Click);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new Size(6, 0x19);
            this.btn_M_Help.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btn_M_Help.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.btn_M_Help.ForeColor = SystemColors.ActiveCaption;
            this.btn_M_Help.Image = (Image) manager.GetObject("btn_M_Help.Image");
            this.btn_M_Help.ImageTransparentColor = Color.Magenta;
            this.btn_M_Help.Name = "btn_M_Help";
            this.btn_M_Help.Size = new Size(0x23, 0x16);
            this.btn_M_Help.Text = "导出";
            this.btn_M_Help.Click += new EventHandler(this.btn_M_Help_Click);
            this.tlb_M_Exit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Exit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Exit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Exit.Image = (Image) manager.GetObject("tlb_M_Exit.Image");
            this.tlb_M_Exit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Exit.Name = "tlb_M_Exit";
            this.tlb_M_Exit.Size = new Size(0x23, 0x16);
            this.tlb_M_Exit.Text = "退出";
            this.tlb_M_Exit.Click += new EventHandler(this.tlb_M_Exit_Click);
            this.tlbSaveSysRts.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = (Image) manager.GetObject("tlbSaveSysRts.Image");
            this.tlbSaveSysRts.ImageTransparentColor = Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new Size(0x51, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.txtMNo.Location = new Point(0x61, 80);
            this.txtMNo.Name = "txtMNo";
            this.txtMNo.Size = new Size(0x79, 0x15);
            this.txtMNo.TabIndex = 9;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x3e, 0x54);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "物料";
            this.cmbWHId.FormattingEnabled = true;
            this.cmbWHId.Location = new Point(0x11c, 80);
            this.cmbWHId.Name = "cmbWHId";
            this.cmbWHId.Size = new Size(0x79, 20);
            this.cmbWHId.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xea, 0x54);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库";
            this.dgvInOutRece.AllowUserToAddRows = false;
            this.dgvInOutRece.Columns.AddRange(new DataGridViewColumn[] { this.cMNo, this.Column4, this.colcSpec, this.Column5, this.Column9 });
            this.dgvInOutRece.Dock = DockStyle.Fill;
            this.dgvInOutRece.Location = new Point(0, 0x74);
            this.dgvInOutRece.Name = "dgvInOutRece";
            this.dgvInOutRece.ReadOnly = true;
            this.dgvInOutRece.RowHeadersVisible = false;
            this.dgvInOutRece.RowTemplate.Height = 0x17;
            this.dgvInOutRece.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvInOutRece.Size = new Size(890, 0xf5);
            this.dgvInOutRece.TabIndex = 7;
            this.dgvInOutRece.SelectionChanged += new EventHandler(this.dgvInOutRece_SelectionChanged);
            this.cMNo.DataPropertyName = "cMNo";
            this.cMNo.HeaderText = "物料编号";
            this.cMNo.Name = "cMNo";
            this.cMNo.ReadOnly = true;
            this.Column4.DataPropertyName = "cName";
            this.Column4.HeaderText = "物料名";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.Column5.DataPropertyName = "fQty";
            this.Column5.HeaderText = "数量";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column9.DataPropertyName = "cUnit";
            this.Column9.HeaderText = "单位";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.panel2.Controls.Add(this.lblReceCount);
            this.panel2.Controls.Add(this.lblSumNum);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(890, 0x2c);
            this.panel2.TabIndex = 9;
            this.lblReceCount.AutoSize = true;
            this.lblReceCount.Font = new Font("宋体", 20f);
            this.lblReceCount.Location = new Point(0xf6, 9);
            this.lblReceCount.Name = "lblReceCount";
            this.lblReceCount.Size = new Size(0x1a, 0x1b);
            this.lblReceCount.TabIndex = 0;
            this.lblReceCount.Text = "0";
            this.lblSumNum.AutoSize = true;
            this.lblSumNum.Font = new Font("宋体", 20f);
            this.lblSumNum.Location = new Point(0x1ab, 9);
            this.lblSumNum.Name = "lblSumNum";
            this.lblSumNum.Size = new Size(0x1a, 0x1b);
            this.lblSumNum.TabIndex = 0;
            this.lblSumNum.Text = "0";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xab, 0x10);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "记录条数：";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x135, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "条    数量总和：";
            this.dgvRillInfo.AllowUserToAddRows = false;
            this.dgvRillInfo.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn9, this.dataGridViewTextBoxColumn10, this.dataGridViewTextBoxColumn11, this.dataGridViewTextBoxColumn12, this.Column1 });
            this.dgvRillInfo.Dock = DockStyle.Fill;
            this.dgvRillInfo.Location = new Point(3, 0x11);
            this.dgvRillInfo.Name = "dgvRillInfo";
            this.dgvRillInfo.ReadOnly = true;
            this.dgvRillInfo.RowHeadersVisible = false;
            this.dgvRillInfo.RowTemplate.Height = 0x17;
            this.dgvRillInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvRillInfo.Size = new Size(0x374, 0xa6);
            this.dgvRillInfo.TabIndex = 8;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cMNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "物料编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "fQty";
            this.dataGridViewTextBoxColumn5.HeaderText = "数量";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "cUnit";
            this.dataGridViewTextBoxColumn6.HeaderText = "单位";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "cPayer";
            this.dataGridViewTextBoxColumn7.HeaderText = "操作员";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "nPalletId";
            this.dataGridViewTextBoxColumn8.HeaderText = "托盘号";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "cWHId";
            this.dataGridViewTextBoxColumn9.HeaderText = "仓库";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "cBTypeNew";
            this.dataGridViewTextBoxColumn10.HeaderText = "单据类别";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "cBNo";
            this.dataGridViewTextBoxColumn11.HeaderText = "单号";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "dInTime";
            this.dataGridViewTextBoxColumn12.HeaderText = "操作时间";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.Column1.DataPropertyName = "cReMark";
            this.Column1.HeaderText = "备注";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.groupBox1.Controls.Add(this.dgvRillInfo);
            this.groupBox1.Dock = DockStyle.Bottom;
            this.groupBox1.Location = new Point(0, 0x195);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(890, 0xba);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "明细记录";
            this.dlgSave.DefaultExt = "xls";
            this.dlgSave.Filter = "Excel 文件|*.xls";
            this.dlgSave.Title = "保存文件";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(890, 0x24f);
            base.Controls.Add(this.dgvInOutRece);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.MinimizeBox = false;
            base.Name = "frmRptInOutRece";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "frmInOutRece";
            base.Load += new EventHandler(this.frmInOutRece_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((ISupportInitialize) this.dgvInOutRece).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize) this.dgvRillInfo).EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadBillClass()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry("0", "全部单据"));
            list.Add(new DictionaryEntry("1", "入库单"));
            list.Add(new DictionaryEntry("2", "出库单"));
            list.Add(new DictionaryEntry("4", "盘点调整单"));
            this.cmbBillType.DataSource = list;
            this.cmbBillType.DisplayMember = "Value";
            this.cmbBillType.ValueMember = "Key";
        }

        private void loadReceInfo(string matId)
        {
            this.loadReceInfo(matId, false);
        }

        private void loadReceInfo(string matId, bool isAllRece)
        {
            this.GetUIValue();
            string sSql = string.Format("select a.nPalletId,a.cBoxId,a.cMNo,a.cWhId,a.dInTime,a.fQty,a.cBNo,a.cReMark,a.cUnit,b.cpayer,case a.nBClass when 1 then '入库单据' when 2 then '出库单据' else '' end cBTypeNew from twb_stockdtl_his a inner join twb_billin b on a.cbno=b.cbno where dintime >= '{0}' and dintime <= '{1}' ", this.timeStatus, this.timeEnd);
            if (this.userName != "")
            {
                sSql = sSql + string.Format("  and b.cpayer='{0}' ", this.userName);
            }
            if (this.WHId != "")
            {
                sSql = sSql + string.Format("  and a.cwhid='{0}' ", this.WHId);
            }
            if (this.receType != "0")
            {
                sSql = sSql + string.Format(" and a.nBClass={0} ", this.receType);
            }
            if (!isAllRece)
            {
                sSql = sSql + string.Format(" and a.cmno='{0}' ", matId);
            }
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "data", 0, 0, "dintime", out sErr);
            if ((sErr != "") && (sErr != "0"))
            {
                MyTools.MessageBox(sErr);
            }
            else
            {
                this.mydtAll = set.Tables["data"];
                if (!isAllRece)
                {
                    this.dgvRillInfo.DataSource = this.mydtAll;
                }
            }
        }

        private void RefeashData()
        {
            if (this.dtpStatus.Value > this.dtpEnd.Value)
            {
                MyTools.MessageBox("查询的开始日期不能大于结束日期！");
                this.dtpStatus.Focus();
            }
            else
            {
                this.mydt = this.GetData();
                this.DisReceSun();
                this.dgvInOutRece.DataSource = this.mydt;
            }
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            this.RefeashData();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            this.btnPrintSum_Click(null, null);
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.RefeashData();
        }
    }
}

