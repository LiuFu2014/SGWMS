namespace WareStore
{
    using DataImpExp;
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;
    using WareStore.Rpts;
    using WareStoreMS;
    using Zqm.CommBase;

    public class frmSlackMatCount : FrmSTable
    {
        private BindingSource bdsMain;
        private bool bIsMainOpened = false;
        private ToolStripButton btn_M_Help;
        private Button btnFindInfo;
        private Button btnPrintReceSum;
        private Button btnPrintSum;
        private Button button1;
        private ComboBox cmbWHId;
        private DataGridViewTextBoxColumn cMNo;
        private DataGridViewTextBoxColumn col_cMatStyle;
        private DataGridViewTextBoxColumn col_cMName;
        private DataGridViewTextBoxColumn col_cPosId;
        private DataGridViewTextBoxColumn col_cSpec;
        private DataGridViewTextBoxColumn col_cWHId;
        private DataGridViewTextBoxColumn col_dLastDate;
        private DataGridViewTextBoxColumn col_Dtl_cMName;
        private DataGridViewTextBoxColumn col_Dtl_dLastDate;
        private DataGridViewTextBoxColumn col_fQty;
        private DataGridViewTextBoxColumn colcSpec;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cUnit;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private SaveFileDialog dlgSave;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private DataGridView grdDtl;
        private DataGridView grdMain;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label7;
        private Label label9;
        private Label lblReceCount;
        private Panel pnl_Conidtion;
        private Panel pnl_Main;
        private Panel pnl_MainCount;
        private DataTable tbMain = null;
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

        public frmSlackMatCount()
        {
            this.InitializeComponent();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (this.bIsMainOpened)
            {
                DataRowView current = null;
                if (this.bdsMain.Count != 0)
                {
                    string sErr = "";
                    current = (DataRowView) this.bdsMain.Current;
                    if (current != null)
                    {
                        string pMNo = current["cMNo"].ToString();
                        string pWHId = "";
                        if ((this.cmbWHId.Text.Trim() != "") && (this.cmbWHId.SelectedValue != null))
                        {
                            pWHId = this.cmbWHId.SelectedValue.ToString();
                        }
                        string pDateFrom = this.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00");
                        string pDateTo = this.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59");
                        DataTable table = null;
                        table = WareStoreMS.DBFuns.sp_GetSlackMatDtl(base.AppInformation.SvrSocket, pWHId, pMNo, pDateFrom, pDateTo, out sErr);
                        if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                        {
                            MessageBox.Show(sErr);
                        }
                        else
                        {
                            this.grdDtl.DataSource = table;
                        }
                    }
                }
            }
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            if (this.bdsMain.Count == 0)
            {
                MyTools.MessageBox("当前没有数据可以进行导入！");
            }
            else if (this.dlgSave.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.dlgSave.FileName;
                DataIE.DataGridViewToExcel(this.grdMain, fileName, this.Text);
            }
        }

        private void btnFindInfo_Click(object sender, EventArgs e)
        {
            string pWHId = "";
            string sErr = "";
            if (this.dtpFrom.Value > this.dtpTo.Value)
            {
                MessageBox.Show("开始日期不能大于截止日期！");
                this.dtpFrom.Focus();
            }
            else
            {
                if ((this.cmbWHId.Text.Trim() != "") && (this.cmbWHId.SelectedValue != null))
                {
                    pWHId = this.cmbWHId.SelectedValue.ToString();
                }
                this.tbMain = WareStoreMS.DBFuns.sp_GetSlackMatCount(base.AppInformation.SvrSocket, pWHId, this.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00"), this.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59"), out sErr);
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else if (this.tbMain != null)
                {
                    this.bIsMainOpened = false;
                    this.bdsMain.DataSource = this.tbMain;
                    this.grdMain.DataSource = this.bdsMain;
                    this.lblReceCount.Text = this.bdsMain.Count.ToString();
                    this.bIsMainOpened = true;
                }
            }
        }

        private void btnPrintSum_Click(object sender, EventArgs e)
        {
            if ((this.tbMain == null) || (this.bdsMain.Count == 0))
            {
                MessageBox.Show("无数据记录记录！");
            }
            else
            {
                frmRptViewer viewer = new frmRptViewer {
                    RptTitle = "呆滞物料汇总报表"
                };
                rptSlackMatCount count = new rptSlackMatCount();
                count.SetDataSource(this.tbMain);
                viewer.RptObj = count;
                viewer.SetReport();
                viewer.ShowDialog();
                viewer.Dispose();
                count.Dispose();
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

        private void frmSlackMatCount_Load(object sender, EventArgs e)
        {
            this.dtpFrom.Value = DateTime.Now.AddYears(-10);
            this.dtpTo.Value = DateTime.Now.AddMonths(-6);
            this.LoadWareHouseList();
            this.grdMain.AutoGenerateColumns = false;
            this.grdDtl.AutoGenerateColumns = false;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmSlackMatCount));
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
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
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.pnl_Conidtion = new Panel();
            this.btnPrintReceSum = new Button();
            this.btnPrintSum = new Button();
            this.btnFindInfo = new Button();
            this.button1 = new Button();
            this.label4 = new Label();
            this.dtpTo = new DateTimePicker();
            this.dtpFrom = new DateTimePicker();
            this.label2 = new Label();
            this.cmbWHId = new ComboBox();
            this.label1 = new Label();
            this.grdMain = new DataGridView();
            this.cMNo = new DataGridViewTextBoxColumn();
            this.col_cWHId = new DataGridViewTextBoxColumn();
            this.col_cMName = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.col_fQty = new DataGridViewTextBoxColumn();
            this.cUnit = new DataGridViewTextBoxColumn();
            this.col_dLastDate = new DataGridViewTextBoxColumn();
            this.col_cMatStyle = new DataGridViewTextBoxColumn();
            this.pnl_Main = new Panel();
            this.pnl_MainCount = new Panel();
            this.lblReceCount = new Label();
            this.label9 = new Label();
            this.label7 = new Label();
            this.grdDtl = new DataGridView();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.col_cPosId = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.col_Dtl_cMName = new DataGridViewTextBoxColumn();
            this.col_cSpec = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.col_Dtl_dLastDate = new DataGridViewTextBoxColumn();
            this.bdsMain = new BindingSource(this.components);
            this.dlgSave = new SaveFileDialog();
            this.tlbMain.SuspendLayout();
            this.pnl_Conidtion.SuspendLayout();
            ((ISupportInitialize) this.grdMain).BeginInit();
            this.pnl_Main.SuspendLayout();
            this.pnl_MainCount.SuspendLayout();
            ((ISupportInitialize) this.grdDtl).BeginInit();
            ((ISupportInitialize) this.bdsMain).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.tlb_M_New, this.tlb_M_Edit, this.tlb_M_Delete, this.tlb_M_Undo, this.tlb_M_Save, this.tlb_M_Refresh, this.toolStripSeparator4, this.toolStripSeparator5, this.tlb_M_Find, this.toolStripSeparator1, this.toolStripSeparator3, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x3e3, 0x19);
            this.tlbMain.TabIndex = 0x10;
            this.tlbMain.Text = "toolStrip1";
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
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
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 0x19);
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
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(6, 0x19);
            this.tlbSaveSysRts.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = (Image) manager.GetObject("tlbSaveSysRts.Image");
            this.tlbSaveSysRts.ImageTransparentColor = Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new Size(0x51, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.pnl_Conidtion.Controls.Add(this.btnPrintReceSum);
            this.pnl_Conidtion.Controls.Add(this.btnPrintSum);
            this.pnl_Conidtion.Controls.Add(this.btnFindInfo);
            this.pnl_Conidtion.Controls.Add(this.button1);
            this.pnl_Conidtion.Controls.Add(this.label4);
            this.pnl_Conidtion.Controls.Add(this.dtpTo);
            this.pnl_Conidtion.Controls.Add(this.dtpFrom);
            this.pnl_Conidtion.Controls.Add(this.label2);
            this.pnl_Conidtion.Controls.Add(this.cmbWHId);
            this.pnl_Conidtion.Controls.Add(this.label1);
            this.pnl_Conidtion.Dock = DockStyle.Top;
            this.pnl_Conidtion.Location = new Point(0, 0x19);
            this.pnl_Conidtion.Name = "pnl_Conidtion";
            this.pnl_Conidtion.Size = new Size(0x3e3, 0x4f);
            this.pnl_Conidtion.TabIndex = 0x11;
            this.btnPrintReceSum.Location = new Point(0x2c8, 0x2b);
            this.btnPrintReceSum.Name = "btnPrintReceSum";
            this.btnPrintReceSum.Size = new Size(0xa3, 0x17);
            this.btnPrintReceSum.TabIndex = 0x17;
            this.btnPrintReceSum.Text = "打印汇总明细报表";
            this.btnPrintReceSum.UseVisualStyleBackColor = true;
            this.btnPrintReceSum.Visible = false;
            this.btnPrintSum.Location = new Point(0x2c8, 14);
            this.btnPrintSum.Name = "btnPrintSum";
            this.btnPrintSum.Size = new Size(0xa3, 0x17);
            this.btnPrintSum.TabIndex = 0x17;
            this.btnPrintSum.Text = "打印汇总报表";
            this.btnPrintSum.UseVisualStyleBackColor = true;
            this.btnPrintSum.Click += new EventHandler(this.btnPrintSum_Click);
            this.btnFindInfo.Location = new Point(0x277, 0x2c);
            this.btnFindInfo.Name = "btnFindInfo";
            this.btnFindInfo.Size = new Size(0x4b, 0x17);
            this.btnFindInfo.TabIndex = 0x16;
            this.btnFindInfo.Text = "查询(&F)";
            this.btnFindInfo.UseVisualStyleBackColor = true;
            this.btnFindInfo.Click += new EventHandler(this.btnFindInfo_Click);
            this.button1.Location = new Point(0x277, 14);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x16;
            this.button1.Text = "重置(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xd1, 0x2f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x13;
            this.label4.Text = "——";
            this.dtpTo.Location = new Point(0x103, 0x2b);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new Size(0x79, 0x15);
            this.dtpTo.TabIndex = 0x12;
            this.dtpFrom.Location = new Point(0x49, 0x2b);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new Size(0x79, 0x15);
            this.dtpFrom.TabIndex = 0x11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(13, 0x2f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "起止日期";
            this.cmbWHId.FormattingEnabled = true;
            this.cmbWHId.Location = new Point(0x49, 14);
            this.cmbWHId.Name = "cmbWHId";
            this.cmbWHId.Size = new Size(0x79, 20);
            this.cmbWHId.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库";
            this.grdMain.AllowUserToAddRows = false;
            this.grdMain.Columns.AddRange(new DataGridViewColumn[] { this.cMNo, this.col_cWHId, this.col_cMName, this.colcSpec, this.col_fQty, this.cUnit, this.col_dLastDate, this.col_cMatStyle });
            this.grdMain.Dock = DockStyle.Fill;
            this.grdMain.Location = new Point(0, 0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ReadOnly = true;
            this.grdMain.RowHeadersVisible = false;
            this.grdMain.RowTemplate.Height = 0x17;
            this.grdMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdMain.Size = new Size(0x3e3, 0x10a);
            this.grdMain.TabIndex = 0x12;
            this.cMNo.DataPropertyName = "cMNo";
            this.cMNo.HeaderText = "物料编号";
            this.cMNo.Name = "cMNo";
            this.cMNo.ReadOnly = true;
            this.col_cWHId.DataPropertyName = "cWHId";
            this.col_cWHId.HeaderText = "仓库";
            this.col_cWHId.Name = "col_cWHId";
            this.col_cWHId.ReadOnly = true;
            this.col_cMName.DataPropertyName = "cMName";
            this.col_cMName.HeaderText = "物料名";
            this.col_cMName.Name = "col_cMName";
            this.col_cMName.ReadOnly = true;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "数量";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            this.cUnit.DataPropertyName = "cUnit";
            this.cUnit.HeaderText = "单位";
            this.cUnit.Name = "cUnit";
            this.cUnit.ReadOnly = true;
            this.col_dLastDate.DataPropertyName = "dLastDate";
            this.col_dLastDate.HeaderText = "最近一次操作时间";
            this.col_dLastDate.Name = "col_dLastDate";
            this.col_dLastDate.ReadOnly = true;
            this.col_cMatStyle.DataPropertyName = "cMatStyle";
            this.col_cMatStyle.HeaderText = "物料款式";
            this.col_cMatStyle.Name = "col_cMatStyle";
            this.col_cMatStyle.ReadOnly = true;
            this.pnl_Main.Controls.Add(this.pnl_MainCount);
            this.pnl_Main.Controls.Add(this.grdMain);
            this.pnl_Main.Controls.Add(this.grdDtl);
            this.pnl_Main.Dock = DockStyle.Fill;
            this.pnl_Main.Location = new Point(0, 0x68);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new Size(0x3e3, 0x1b0);
            this.pnl_Main.TabIndex = 20;
            this.pnl_MainCount.Controls.Add(this.lblReceCount);
            this.pnl_MainCount.Controls.Add(this.label9);
            this.pnl_MainCount.Controls.Add(this.label7);
            this.pnl_MainCount.Dock = DockStyle.Bottom;
            this.pnl_MainCount.Location = new Point(0, 0xe9);
            this.pnl_MainCount.Name = "pnl_MainCount";
            this.pnl_MainCount.Size = new Size(0x3e3, 0x21);
            this.pnl_MainCount.TabIndex = 20;
            this.lblReceCount.AutoSize = true;
            this.lblReceCount.Font = new Font("宋体", 18f);
            this.lblReceCount.Location = new Point(0x60, 4);
            this.lblReceCount.Name = "lblReceCount";
            this.lblReceCount.Size = new Size(0x16, 0x18);
            this.lblReceCount.TabIndex = 3;
            this.lblReceCount.Text = "0";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x15, 11);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "记录条数：";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x9f, 11);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "条";
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewTextBoxColumn9, this.col_cPosId, this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn1, this.col_Dtl_cMName, this.col_cSpec, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.col_Dtl_dLastDate });
            this.grdDtl.Dock = DockStyle.Bottom;
            this.grdDtl.Location = new Point(0, 0x10a);
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 0x17;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdDtl.Size = new Size(0x3e3, 0xa6);
            this.grdDtl.TabIndex = 0x13;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "cWHId";
            this.dataGridViewTextBoxColumn9.HeaderText = "仓库";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.col_cPosId.DataPropertyName = "cPosId";
            this.col_cPosId.HeaderText = "货位号";
            this.col_cPosId.Name = "col_cPosId";
            this.col_cPosId.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "nPalletId";
            this.dataGridViewTextBoxColumn8.HeaderText = "托盘号";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cMNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "物料编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.col_Dtl_cMName.DataPropertyName = "cMName";
            this.col_Dtl_cMName.HeaderText = "物料名称";
            this.col_Dtl_cMName.Name = "col_Dtl_cMName";
            this.col_Dtl_cMName.ReadOnly = true;
            this.col_cSpec.DataPropertyName = "cSpec";
            this.col_cSpec.HeaderText = "规格";
            this.col_cSpec.Name = "col_cSpec";
            this.col_cSpec.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "fQty";
            this.dataGridViewTextBoxColumn5.HeaderText = "数量";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "cUnit";
            this.dataGridViewTextBoxColumn6.HeaderText = "单位";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.col_Dtl_dLastDate.DataPropertyName = "dLastDate";
            this.col_Dtl_dLastDate.HeaderText = "最近一次操作时间";
            this.col_Dtl_dLastDate.Name = "col_Dtl_dLastDate";
            this.col_Dtl_dLastDate.ReadOnly = true;
            this.bdsMain.AllowNew = false;
            this.bdsMain.PositionChanged += new EventHandler(this.bdsMain_PositionChanged);
            this.dlgSave.DefaultExt = "xls";
            this.dlgSave.Filter = "Excel 文件|*.xls";
            this.dlgSave.Title = "保存文件";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3e3, 0x218);
            base.Controls.Add(this.pnl_Main);
            base.Controls.Add(this.pnl_Conidtion);
            base.Controls.Add(this.tlbMain);
            base.Name = "frmSlackMatCount";
            this.Text = "呆滞物料统计";
            base.Load += new EventHandler(this.frmSlackMatCount_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.pnl_Conidtion.ResumeLayout(false);
            this.pnl_Conidtion.PerformLayout();
            ((ISupportInitialize) this.grdMain).EndInit();
            this.pnl_Main.ResumeLayout(false);
            this.pnl_MainCount.ResumeLayout(false);
            this.pnl_MainCount.PerformLayout();
            ((ISupportInitialize) this.grdDtl).EndInit();
            ((ISupportInitialize) this.bdsMain).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadWareHouseList()
        {
            string sSql = "select cwhid,cName from TWC_WareHouse where bUsed=1";
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')";
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

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

