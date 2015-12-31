namespace SunEast.App
{
    partial class frmBillOut
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillOut));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_ErpImp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Undo = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Find = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Check = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_UnCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_UpdateDtlQtyAfterDo = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_OverBWK = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Item = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.bdsMain = new System.Windows.Forms.BindingSource(this.components);
            this.bdsDtl = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.lbl_Bill_Count = new System.Windows.Forms.Label();
            this.lbl_bill = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_FinishedStatus = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtFindBillFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFindType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUnFind = new System.Windows.Forms.Button();
            this.cmbFindCheck = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbFindUser = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpFind_E = new System.Windows.Forms.DateTimePicker();
            this.dtpFind_B = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQry = new System.Windows.Forms.Button();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colcBId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_M_cLinkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcBTypeId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_M_cBNoFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Main_bIsChecked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcWHId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_Main_nBClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Main_bIsFinished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Main_cCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDtl = new System.Windows.Forms.Panel();
            this.lbl_Dtl_Count = new System.Windows.Forms.Label();
            this.lbl_Dtl = new System.Windows.Forms.Label();
            this.pnlBtns = new System.Windows.Forms.Panel();
            this.btn_Dtl_Delete = new System.Windows.Forms.Button();
            this.btn_Dtl_Edit = new System.Windows.Forms.Button();
            this.btn_Dtl_New = new System.Windows.Forms.Button();
            this.pnlDtlEdit = new System.Windows.Forms.Panel();
            this.txt_Dtl_cSpec = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Dtl_cMName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Dtl_cUnit = new System.Windows.Forms.TextBox();
            this.txt_Dtl_fFinished = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_Dtl_fPallet = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_Dtl_fQty = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.dtp_Dtl_dProdDate = new System.Windows.Forms.DateTimePicker();
            this.txt_Dtl_cBatchNo = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_Dtl_cMNo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_Dtl_nDoStatus = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmb_Dtl_nQCStatus = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.grdDtl = new System.Windows.Forms.DataGridView();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.lbl_BillTskIsOver = new System.Windows.Forms.Label();
            this.lbl_Check = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmb_cDept = new System.Windows.Forms.ComboBox();
            this.lbl_Customer = new System.Windows.Forms.Label();
            this.cmb_nPStatus = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmb_cPayer = new System.Windows.Forms.ComboBox();
            this.txt_cRemark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_dCreateDate = new System.Windows.Forms.DateTimePicker();
            this.cmb_cBTypeId = new System.Windows.Forms.ComboBox();
            this.txt_cBNoFrom = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_cChecker = new System.Windows.Forms.TextBox();
            this.lblChecker = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_cBNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ttpMain = new System.Windows.Forms.ToolTip(this.components);
            this.colnItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfPallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfFinished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnQCStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnDoStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcPStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldProdDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cWHIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cAreaIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cPosIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.stbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDtl)).BeginInit();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlDtl.SuspendLayout();
            this.pnlBtns.SuspendLayout();
            this.pnlDtlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDtl)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_New
            // 
            this.tlb_M_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_New.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_New.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_New.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_New.Image")));
            this.tlb_M_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_New.Name = "tlb_M_New";
            this.tlb_M_New.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_New.Tag = "01";
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Click += new System.EventHandler(this.tlb_M_New_Click);
            // 
            // tlb_M_Edit
            // 
            this.tlb_M_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Edit.Image")));
            this.tlb_M_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Edit.Tag = "02";
            this.tlb_M_Edit.Text = "修改";
            this.tlb_M_Edit.Click += new System.EventHandler(this.tlb_M_Edit_Click);
            // 
            // tlbMain
            // 
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.tlb_M_ErpImp,
            this.toolStripSeparator1,
            this.tlb_M_New,
            this.tlb_M_Edit,
            this.toolStripSeparator3,
            this.tlb_M_Undo,
            this.tlb_M_Delete,
            this.toolStripSeparator4,
            this.tlb_M_Save,
            this.toolStripSeparator5,
            this.tlb_M_Refresh,
            this.tlb_M_Find,
            this.tlb_M_Print,
            this.toolStripSeparator6,
            this.tlb_M_Check,
            this.tlb_M_UnCheck,
            this.toolStripSeparator7,
            this.tlb_M_UpdateDtlQtyAfterDo,
            this.tlb_M_OverBWK,
            this.tlb_M_Item,
            this.toolStripSeparator8,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(1016, 25);
            this.tlbMain.TabIndex = 30;
            this.tlbMain.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_ErpImp
            // 
            this.tlb_M_ErpImp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_ErpImp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_ErpImp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_ErpImp.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_ErpImp.Image")));
            this.tlb_M_ErpImp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_ErpImp.Name = "tlb_M_ErpImp";
            this.tlb_M_ErpImp.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_ErpImp.Tag = "20";
            this.tlb_M_ErpImp.Text = "导入数据";
            this.tlb_M_ErpImp.ToolTipText = "接口数据导入";
            this.tlb_M_ErpImp.Click += new System.EventHandler(this.tlb_M_ErpImp_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Undo
            // 
            this.tlb_M_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Undo.Image")));
            this.tlb_M_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Undo.Tag = "03";
            this.tlb_M_Undo.Text = "取消";
            this.tlb_M_Undo.Click += new System.EventHandler(this.tlb_M_Undo_Click);
            // 
            // tlb_M_Delete
            // 
            this.tlb_M_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Delete.Image")));
            this.tlb_M_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Delete.Tag = "04";
            this.tlb_M_Delete.Text = "删除";
            this.tlb_M_Delete.Click += new System.EventHandler(this.tlb_M_Delete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Save
            // 
            this.tlb_M_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Save.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Save.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Save.Image")));
            this.tlb_M_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Save.Name = "tlb_M_Save";
            this.tlb_M_Save.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Save.Tag = "05";
            this.tlb_M_Save.Text = "保存";
            this.tlb_M_Save.Click += new System.EventHandler(this.tlb_M_Save_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Refresh
            // 
            this.tlb_M_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Refresh.Image")));
            this.tlb_M_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new System.EventHandler(this.tlb_M_Refresh_Click);
            // 
            // tlb_M_Find
            // 
            this.tlb_M_Find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Find.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Find.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Find.Image")));
            this.tlb_M_Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Find.Text = "查找";
            this.tlb_M_Find.Click += new System.EventHandler(this.tlb_M_Find_Click);
            // 
            // tlb_M_Print
            // 
            this.tlb_M_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Print.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Print.Image")));
            this.tlb_M_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Print.Tag = "06";
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Click += new System.EventHandler(this.tlb_M_Print_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Check
            // 
            this.tlb_M_Check.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Check.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Check.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Check.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Check.Image")));
            this.tlb_M_Check.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Check.Name = "tlb_M_Check";
            this.tlb_M_Check.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Check.Tag = "07";
            this.tlb_M_Check.Text = "审核";
            this.tlb_M_Check.Click += new System.EventHandler(this.tlb_M_Check_Click);
            // 
            // tlb_M_UnCheck
            // 
            this.tlb_M_UnCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_UnCheck.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_UnCheck.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_UnCheck.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_UnCheck.Image")));
            this.tlb_M_UnCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_UnCheck.Name = "tlb_M_UnCheck";
            this.tlb_M_UnCheck.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_UnCheck.Tag = "08";
            this.tlb_M_UnCheck.Text = "取消审核";
            this.tlb_M_UnCheck.Click += new System.EventHandler(this.tlb_M_UnCheck_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_UpdateDtlQtyAfterDo
            // 
            this.tlb_M_UpdateDtlQtyAfterDo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_UpdateDtlQtyAfterDo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_UpdateDtlQtyAfterDo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_UpdateDtlQtyAfterDo.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_UpdateDtlQtyAfterDo.Image")));
            this.tlb_M_UpdateDtlQtyAfterDo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_UpdateDtlQtyAfterDo.Name = "tlb_M_UpdateDtlQtyAfterDo";
            this.tlb_M_UpdateDtlQtyAfterDo.Size = new System.Drawing.Size(87, 22);
            this.tlb_M_UpdateDtlQtyAfterDo.Tag = "11";
            this.tlb_M_UpdateDtlQtyAfterDo.Text = "修改明细数量";
            this.tlb_M_UpdateDtlQtyAfterDo.ToolTipText = "对执行中的单据修改明细数量\r\n(仅对特殊权限的人员开发)";
            this.tlb_M_UpdateDtlQtyAfterDo.Click += new System.EventHandler(this.tlb_M_UpdateDtlQtyAfterDo_Click);
            // 
            // tlb_M_OverBWK
            // 
            this.tlb_M_OverBWK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_OverBWK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_OverBWK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_OverBWK.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_OverBWK.Image")));
            this.tlb_M_OverBWK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_OverBWK.Name = "tlb_M_OverBWK";
            this.tlb_M_OverBWK.Size = new System.Drawing.Size(87, 22);
            this.tlb_M_OverBWK.Tag = "09";
            this.tlb_M_OverBWK.Text = "完成单据作业";
            this.tlb_M_OverBWK.Click += new System.EventHandler(this.tlb_M_OverBWK_Click);
            // 
            // tlb_M_Item
            // 
            this.tlb_M_Item.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Item.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Item.Image")));
            this.tlb_M_Item.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Item.Name = "tlb_M_Item";
            this.tlb_M_Item.Size = new System.Drawing.Size(36, 22);
            this.tlb_M_Item.Text = "物料";
            this.tlb_M_Item.Visible = false;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_M_Help
            // 
            this.btn_M_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_M_Help.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btn_M_Help.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_M_Help.Image = ((System.Drawing.Image)(resources.GetObject("btn_M_Help.Image")));
            this.btn_M_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_M_Help.Name = "btn_M_Help";
            this.btn_M_Help.Size = new System.Drawing.Size(35, 22);
            this.btn_M_Help.Text = "帮助";
            this.btn_M_Help.Visible = false;
            // 
            // tlb_M_Exit
            // 
            this.tlb_M_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Exit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Exit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Exit.Image")));
            this.tlb_M_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Exit.Name = "tlb_M_Exit";
            this.tlb_M_Exit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Exit.Text = "退出";
            this.tlb_M_Exit.Click += new System.EventHandler(this.tlb_M_Exit_Click);
            // 
            // tlbSaveSysRts
            // 
            this.tlbSaveSysRts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = ((System.Drawing.Image)(resources.GetObject("tlbSaveSysRts.Image")));
            this.tlbSaveSysRts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new System.Drawing.Size(84, 22);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new System.EventHandler(this.tlbSaveSysRts_Click);
            // 
            // stbDateTime
            // 
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new System.Drawing.Size(35, 17);
            this.stbDateTime.Text = "时间:";
            // 
            // tmrMain
            // 
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 5000;
            // 
            // stbUser
            // 
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new System.Drawing.Size(47, 17);
            this.stbUser.Text = "用户名:";
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbModul,
            this.stbUser,
            this.stbState,
            this.stbDateTime});
            this.stbMain.Location = new System.Drawing.Point(0, 626);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(1016, 22);
            this.stbMain.TabIndex = 28;
            this.stbMain.Text = "statusStrip1";
            // 
            // stbModul
            // 
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new System.Drawing.Size(35, 17);
            this.stbModul.Text = "模块:";
            // 
            // stbState
            // 
            this.stbState.Name = "stbState";
            this.stbState.Size = new System.Drawing.Size(35, 17);
            this.stbState.Text = "状态:";
            // 
            // bdsMain
            // 
            this.bdsMain.PositionChanged += new System.EventHandler(this.bdsMain_PositionChanged);
            // 
            // bdsDtl
            // 
            this.bdsDtl.PositionChanged += new System.EventHandler(this.bdsDtl_PositionChanged);
            // 
            // pnlSplit
            // 
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplit.Location = new System.Drawing.Point(0, 25);
            this.pnlSplit.Name = "pnlSplit";
            // 
            // pnlSplit.Panel1
            // 
            this.pnlSplit.Panel1.Controls.Add(this.lbl_Bill_Count);
            this.pnlSplit.Panel1.Controls.Add(this.lbl_bill);
            this.pnlSplit.Panel1.Controls.Add(this.groupBox1);
            this.pnlSplit.Panel1.Controls.Add(this.grdList);
            // 
            // pnlSplit.Panel2
            // 
            this.pnlSplit.Panel2.Controls.Add(this.pnlDtl);
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pnlSplit.Size = new System.Drawing.Size(1016, 601);
            this.pnlSplit.SplitterDistance = 287;
            this.pnlSplit.TabIndex = 31;
            // 
            // lbl_Bill_Count
            // 
            this.lbl_Bill_Count.AutoSize = true;
            this.lbl_Bill_Count.Location = new System.Drawing.Point(135, 579);
            this.lbl_Bill_Count.Name = "lbl_Bill_Count";
            this.lbl_Bill_Count.Size = new System.Drawing.Size(11, 12);
            this.lbl_Bill_Count.TabIndex = 59;
            this.lbl_Bill_Count.Text = "0";
            // 
            // lbl_bill
            // 
            this.lbl_bill.AutoSize = true;
            this.lbl_bill.Location = new System.Drawing.Point(46, 579);
            this.lbl_bill.Name = "lbl_bill";
            this.lbl_bill.Size = new System.Drawing.Size(83, 12);
            this.lbl_bill.TabIndex = 58;
            this.lbl_bill.Text = "单据记录条数:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_FinishedStatus);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.txtFindBillFrom);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbFindType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnUnFind);
            this.groupBox1.Controls.Add(this.cmbFindCheck);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.cmbFindUser);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.dtpFind_E);
            this.groupBox1.Controls.Add(this.dtpFind_B);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnQry);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 124);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cmb_FinishedStatus
            // 
            this.cmb_FinishedStatus.FormattingEnabled = true;
            this.cmb_FinishedStatus.Items.AddRange(new object[] {
            "未完成",
            "已完成",
            "全部"});
            this.cmb_FinishedStatus.Location = new System.Drawing.Point(207, 58);
            this.cmb_FinishedStatus.Name = "cmb_FinishedStatus";
            this.cmb_FinishedStatus.Size = new System.Drawing.Size(69, 20);
            this.cmb_FinishedStatus.TabIndex = 24;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(146, 62);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(65, 12);
            this.label44.TabIndex = 25;
            this.label44.Text = "完成状态：";
            // 
            // txtFindBillFrom
            // 
            this.txtFindBillFrom.Location = new System.Drawing.Point(38, 60);
            this.txtFindBillFrom.Name = "txtFindBillFrom";
            this.txtFindBillFrom.Size = new System.Drawing.Size(90, 21);
            this.txtFindBillFrom.TabIndex = 12;
            this.ttpMain.SetToolTip(this.txtFindBillFrom, "根据单号或来源单号或关联单号进行查询");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "单号：";
            // 
            // cmbFindType
            // 
            this.cmbFindType.FormattingEnabled = true;
            this.cmbFindType.Items.AddRange(new object[] {
            "平面库",
            "立体库"});
            this.cmbFindType.Location = new System.Drawing.Point(192, 32);
            this.cmbFindType.Name = "cmbFindType";
            this.cmbFindType.Size = new System.Drawing.Size(84, 20);
            this.cmbFindType.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "类型：";
            // 
            // btnUnFind
            // 
            this.btnUnFind.Location = new System.Drawing.Point(221, 83);
            this.btnUnFind.Name = "btnUnFind";
            this.btnUnFind.Size = new System.Drawing.Size(39, 23);
            this.btnUnFind.TabIndex = 16;
            this.btnUnFind.Text = "重置";
            this.btnUnFind.UseVisualStyleBackColor = true;
            this.btnUnFind.Click += new System.EventHandler(this.btnUnFind_Click);
            // 
            // cmbFindCheck
            // 
            this.cmbFindCheck.FormattingEnabled = true;
            this.cmbFindCheck.Items.AddRange(new object[] {
            "未审核",
            "审核",
            "全部"});
            this.cmbFindCheck.Location = new System.Drawing.Point(66, 85);
            this.cmbFindCheck.Name = "cmbFindCheck";
            this.cmbFindCheck.Size = new System.Drawing.Size(90, 20);
            this.cmbFindCheck.TabIndex = 13;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(1, 88);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 15;
            this.label28.Text = "审核状态：";
            // 
            // cmbFindUser
            // 
            this.cmbFindUser.FormattingEnabled = true;
            this.cmbFindUser.Items.AddRange(new object[] {
            "平面库",
            "立体库"});
            this.cmbFindUser.Location = new System.Drawing.Point(38, 34);
            this.cmbFindUser.Name = "cmbFindUser";
            this.cmbFindUser.Size = new System.Drawing.Size(89, 20);
            this.cmbFindUser.TabIndex = 10;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(0, 37);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(41, 12);
            this.label29.TabIndex = 13;
            this.label29.Text = "操作员";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.SystemColors.ControlText;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Location = new System.Drawing.Point(154, 19);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 1);
            this.label24.TabIndex = 10;
            // 
            // dtpFind_E
            // 
            this.dtpFind_E.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_E.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFind_E.Location = new System.Drawing.Point(192, 9);
            this.dtpFind_E.Name = "dtpFind_E";
            this.dtpFind_E.Size = new System.Drawing.Size(84, 21);
            this.dtpFind_E.TabIndex = 9;
            this.dtpFind_E.Tag = "2";
            // 
            // dtpFind_B
            // 
            this.dtpFind_B.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_B.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFind_B.Location = new System.Drawing.Point(38, 9);
            this.dtpFind_B.Name = "dtpFind_B";
            this.dtpFind_B.Size = new System.Drawing.Size(89, 21);
            this.dtpFind_B.TabIndex = 8;
            this.dtpFind_B.Tag = "2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "日期：";
            // 
            // btnQry
            // 
            this.btnQry.Location = new System.Drawing.Point(179, 84);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new System.Drawing.Size(39, 23);
            this.btnQry.TabIndex = 14;
            this.btnQry.Text = "查询";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new System.EventHandler(this.btnQry_Click);
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeight = 45;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcBId,
            this.col_M_cLinkId,
            this.colcBTypeId,
            this.col_M_cBNoFrom,
            this.col_Main_bIsChecked,
            this.colcWHId,
            this.col_Main_nBClass,
            this.col_Main_bIsFinished,
            this.col_Main_cCreator});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new System.Drawing.Point(3, 129);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 23;
            this.grdList.Size = new System.Drawing.Size(282, 442);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            // 
            // colcBId
            // 
            this.colcBId.DataPropertyName = "cBNo";
            this.colcBId.FillWeight = 50F;
            this.colcBId.Frozen = true;
            this.colcBId.HeaderText = "单号";
            this.colcBId.Name = "colcBId";
            this.colcBId.ReadOnly = true;
            this.colcBId.ToolTipText = "单号";
            this.colcBId.Width = 85;
            // 
            // col_M_cLinkId
            // 
            this.col_M_cLinkId.DataPropertyName = "cLinkId";
            this.col_M_cLinkId.HeaderText = "关联单号";
            this.col_M_cLinkId.Name = "col_M_cLinkId";
            this.col_M_cLinkId.ReadOnly = true;
            this.col_M_cLinkId.Width = 85;
            // 
            // colcBTypeId
            // 
            this.colcBTypeId.DataPropertyName = "cBTypeId";
            this.colcBTypeId.HeaderText = "出库类型";
            this.colcBTypeId.Name = "colcBTypeId";
            this.colcBTypeId.ReadOnly = true;
            this.colcBTypeId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colcBTypeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colcBTypeId.ToolTipText = "出库类型";
            // 
            // col_M_cBNoFrom
            // 
            this.col_M_cBNoFrom.DataPropertyName = "cBNoFrom";
            this.col_M_cBNoFrom.HeaderText = "来源单号";
            this.col_M_cBNoFrom.Name = "col_M_cBNoFrom";
            this.col_M_cBNoFrom.ReadOnly = true;
            // 
            // col_Main_bIsChecked
            // 
            this.col_Main_bIsChecked.DataPropertyName = "bIsChecked";
            this.col_Main_bIsChecked.HeaderText = "是否已审核";
            this.col_Main_bIsChecked.Name = "col_Main_bIsChecked";
            this.col_Main_bIsChecked.ReadOnly = true;
            // 
            // colcWHId
            // 
            this.colcWHId.DataPropertyName = "cWHId";
            this.colcWHId.HeaderText = "仓库";
            this.colcWHId.Name = "colcWHId";
            this.colcWHId.ReadOnly = true;
            this.colcWHId.ToolTipText = "仓库";
            this.colcWHId.Visible = false;
            // 
            // col_Main_nBClass
            // 
            this.col_Main_nBClass.DataPropertyName = "nBClass";
            this.col_Main_nBClass.HeaderText = "单据类别";
            this.col_Main_nBClass.Name = "col_Main_nBClass";
            this.col_Main_nBClass.ReadOnly = true;
            // 
            // col_Main_bIsFinished
            // 
            this.col_Main_bIsFinished.DataPropertyName = "bIsFinished";
            this.col_Main_bIsFinished.HeaderText = "是否单据作业完成";
            this.col_Main_bIsFinished.Name = "col_Main_bIsFinished";
            this.col_Main_bIsFinished.ReadOnly = true;
            // 
            // col_Main_cCreator
            // 
            this.col_Main_cCreator.DataPropertyName = "cCreator";
            this.col_Main_cCreator.HeaderText = "仓库员";
            this.col_Main_cCreator.Name = "col_Main_cCreator";
            this.col_Main_cCreator.ReadOnly = true;
            // 
            // pnlDtl
            // 
            this.pnlDtl.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pnlDtl.Controls.Add(this.lbl_Dtl_Count);
            this.pnlDtl.Controls.Add(this.lbl_Dtl);
            this.pnlDtl.Controls.Add(this.pnlBtns);
            this.pnlDtl.Controls.Add(this.pnlDtlEdit);
            this.pnlDtl.Controls.Add(this.grdDtl);
            this.pnlDtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDtl.Location = new System.Drawing.Point(0, 122);
            this.pnlDtl.Name = "pnlDtl";
            this.pnlDtl.Size = new System.Drawing.Size(725, 479);
            this.pnlDtl.TabIndex = 2;
            // 
            // lbl_Dtl_Count
            // 
            this.lbl_Dtl_Count.AutoSize = true;
            this.lbl_Dtl_Count.Location = new System.Drawing.Point(237, 242);
            this.lbl_Dtl_Count.Name = "lbl_Dtl_Count";
            this.lbl_Dtl_Count.Size = new System.Drawing.Size(11, 12);
            this.lbl_Dtl_Count.TabIndex = 57;
            this.lbl_Dtl_Count.Text = "0";
            // 
            // lbl_Dtl
            // 
            this.lbl_Dtl.AutoSize = true;
            this.lbl_Dtl.Location = new System.Drawing.Point(148, 242);
            this.lbl_Dtl.Name = "lbl_Dtl";
            this.lbl_Dtl.Size = new System.Drawing.Size(83, 12);
            this.lbl_Dtl.TabIndex = 56;
            this.lbl_Dtl.Text = "明细记录条数:";
            // 
            // pnlBtns
            // 
            this.pnlBtns.Controls.Add(this.btn_Dtl_Delete);
            this.pnlBtns.Controls.Add(this.btn_Dtl_Edit);
            this.pnlBtns.Controls.Add(this.btn_Dtl_New);
            this.pnlBtns.Location = new System.Drawing.Point(9, 359);
            this.pnlBtns.Name = "pnlBtns";
            this.pnlBtns.Size = new System.Drawing.Size(669, 27);
            this.pnlBtns.TabIndex = 4;
            // 
            // btn_Dtl_Delete
            // 
            this.btn_Dtl_Delete.Location = new System.Drawing.Point(261, 2);
            this.btn_Dtl_Delete.Name = "btn_Dtl_Delete";
            this.btn_Dtl_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Dtl_Delete.TabIndex = 3;
            this.btn_Dtl_Delete.Text = "删除";
            this.btn_Dtl_Delete.UseVisualStyleBackColor = true;
            this.btn_Dtl_Delete.Click += new System.EventHandler(this.btn_Dtl_Delete_Click);
            // 
            // btn_Dtl_Edit
            // 
            this.btn_Dtl_Edit.Location = new System.Drawing.Point(163, 2);
            this.btn_Dtl_Edit.Name = "btn_Dtl_Edit";
            this.btn_Dtl_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Dtl_Edit.TabIndex = 2;
            this.btn_Dtl_Edit.Text = "修改";
            this.btn_Dtl_Edit.UseVisualStyleBackColor = true;
            this.btn_Dtl_Edit.Click += new System.EventHandler(this.btn_Dtl_Edit_Click);
            // 
            // btn_Dtl_New
            // 
            this.btn_Dtl_New.Location = new System.Drawing.Point(65, 2);
            this.btn_Dtl_New.Name = "btn_Dtl_New";
            this.btn_Dtl_New.Size = new System.Drawing.Size(75, 23);
            this.btn_Dtl_New.TabIndex = 1;
            this.btn_Dtl_New.Text = "新增";
            this.btn_Dtl_New.UseVisualStyleBackColor = true;
            this.btn_Dtl_New.Click += new System.EventHandler(this.btn_Dtl_New_Click);
            // 
            // pnlDtlEdit
            // 
            this.pnlDtlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cSpec);
            this.pnlDtlEdit.Controls.Add(this.label11);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMName);
            this.pnlDtlEdit.Controls.Add(this.label10);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cUnit);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_fFinished);
            this.pnlDtlEdit.Controls.Add(this.label27);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_fPallet);
            this.pnlDtlEdit.Controls.Add(this.label26);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_fQty);
            this.pnlDtlEdit.Controls.Add(this.label25);
            this.pnlDtlEdit.Controls.Add(this.dtp_Dtl_dProdDate);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cBatchNo);
            this.pnlDtlEdit.Controls.Add(this.label21);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMNo);
            this.pnlDtlEdit.Controls.Add(this.label19);
            this.pnlDtlEdit.Controls.Add(this.label18);
            this.pnlDtlEdit.Controls.Add(this.label12);
            this.pnlDtlEdit.Controls.Add(this.cmb_Dtl_nDoStatus);
            this.pnlDtlEdit.Controls.Add(this.label16);
            this.pnlDtlEdit.Controls.Add(this.cmb_Dtl_nQCStatus);
            this.pnlDtlEdit.Controls.Add(this.label17);
            this.pnlDtlEdit.Location = new System.Drawing.Point(9, 260);
            this.pnlDtlEdit.Name = "pnlDtlEdit";
            this.pnlDtlEdit.Size = new System.Drawing.Size(669, 95);
            this.pnlDtlEdit.TabIndex = 3;
            // 
            // txt_Dtl_cSpec
            // 
            this.txt_Dtl_cSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cSpec.Location = new System.Drawing.Point(65, 38);
            this.txt_Dtl_cSpec.Name = "txt_Dtl_cSpec";
            this.txt_Dtl_cSpec.ReadOnly = true;
            this.txt_Dtl_cSpec.Size = new System.Drawing.Size(90, 21);
            this.txt_Dtl_cSpec.TabIndex = 75;
            this.txt_Dtl_cSpec.Tag = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 76;
            this.label11.Text = "规    格：";
            // 
            // txt_Dtl_cMName
            // 
            this.txt_Dtl_cMName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cMName.Location = new System.Drawing.Point(218, 7);
            this.txt_Dtl_cMName.Name = "txt_Dtl_cMName";
            this.txt_Dtl_cMName.ReadOnly = true;
            this.txt_Dtl_cMName.Size = new System.Drawing.Size(430, 21);
            this.txt_Dtl_cMName.TabIndex = 73;
            this.txt_Dtl_cMName.Tag = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(176, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 74;
            this.label10.Text = "名称：";
            // 
            // txt_Dtl_cUnit
            // 
            this.txt_Dtl_cUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cUnit.Location = new System.Drawing.Point(602, 43);
            this.txt_Dtl_cUnit.Name = "txt_Dtl_cUnit";
            this.txt_Dtl_cUnit.ReadOnly = true;
            this.txt_Dtl_cUnit.Size = new System.Drawing.Size(46, 21);
            this.txt_Dtl_cUnit.TabIndex = 72;
            this.txt_Dtl_cUnit.Tag = "0";
            // 
            // txt_Dtl_fFinished
            // 
            this.txt_Dtl_fFinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_fFinished.Location = new System.Drawing.Point(219, 71);
            this.txt_Dtl_fFinished.Name = "txt_Dtl_fFinished";
            this.txt_Dtl_fFinished.ReadOnly = true;
            this.txt_Dtl_fFinished.Size = new System.Drawing.Size(86, 21);
            this.txt_Dtl_fFinished.TabIndex = 70;
            this.txt_Dtl_fFinished.Tag = "0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(158, 71);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 71;
            this.label27.Text = "完成数量：";
            // 
            // txt_Dtl_fPallet
            // 
            this.txt_Dtl_fPallet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_fPallet.Location = new System.Drawing.Point(65, 69);
            this.txt_Dtl_fPallet.Name = "txt_Dtl_fPallet";
            this.txt_Dtl_fPallet.ReadOnly = true;
            this.txt_Dtl_fPallet.Size = new System.Drawing.Size(90, 21);
            this.txt_Dtl_fPallet.TabIndex = 68;
            this.txt_Dtl_fPallet.Tag = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 71);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 69;
            this.label26.Text = "已配数量：";
            // 
            // txt_Dtl_fQty
            // 
            this.txt_Dtl_fQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_fQty.Location = new System.Drawing.Point(485, 43);
            this.txt_Dtl_fQty.Name = "txt_Dtl_fQty";
            this.txt_Dtl_fQty.ReadOnly = true;
            this.txt_Dtl_fQty.Size = new System.Drawing.Size(75, 21);
            this.txt_Dtl_fQty.TabIndex = 66;
            this.txt_Dtl_fQty.Tag = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(446, 48);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 12);
            this.label25.TabIndex = 67;
            this.label25.Text = "数量：";
            // 
            // dtp_Dtl_dProdDate
            // 
            this.dtp_Dtl_dProdDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_Dtl_dProdDate.Enabled = false;
            this.dtp_Dtl_dProdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Dtl_dProdDate.Location = new System.Drawing.Point(219, 43);
            this.dtp_Dtl_dProdDate.Name = "dtp_Dtl_dProdDate";
            this.dtp_Dtl_dProdDate.Size = new System.Drawing.Size(86, 21);
            this.dtp_Dtl_dProdDate.TabIndex = 52;
            this.dtp_Dtl_dProdDate.Tag = "2";
            // 
            // txt_Dtl_cBatchNo
            // 
            this.txt_Dtl_cBatchNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cBatchNo.Location = new System.Drawing.Point(369, 43);
            this.txt_Dtl_cBatchNo.Name = "txt_Dtl_cBatchNo";
            this.txt_Dtl_cBatchNo.ReadOnly = true;
            this.txt_Dtl_cBatchNo.Size = new System.Drawing.Size(68, 21);
            this.txt_Dtl_cBatchNo.TabIndex = 58;
            this.txt_Dtl_cBatchNo.Tag = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(309, 47);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 59;
            this.label21.Text = "生产批号：";
            // 
            // txt_Dtl_cMNo
            // 
            this.txt_Dtl_cMNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cMNo.Location = new System.Drawing.Point(65, 5);
            this.txt_Dtl_cMNo.Name = "txt_Dtl_cMNo";
            this.txt_Dtl_cMNo.ReadOnly = true;
            this.txt_Dtl_cMNo.Size = new System.Drawing.Size(106, 21);
            this.txt_Dtl_cMNo.TabIndex = 54;
            this.txt_Dtl_cMNo.Tag = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 55;
            this.label19.Text = "物料编码：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(157, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 53;
            this.label18.Text = "生产日期：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(565, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 51;
            this.label12.Text = "单位：";
            // 
            // cmb_Dtl_nDoStatus
            // 
            this.cmb_Dtl_nDoStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_Dtl_nDoStatus.FormattingEnabled = true;
            this.cmb_Dtl_nDoStatus.Location = new System.Drawing.Point(369, 73);
            this.cmb_Dtl_nDoStatus.Name = "cmb_Dtl_nDoStatus";
            this.cmb_Dtl_nDoStatus.Size = new System.Drawing.Size(113, 20);
            this.cmb_Dtl_nDoStatus.TabIndex = 49;
            this.cmb_Dtl_nDoStatus.Tag = "101";
            this.cmb_Dtl_nDoStatus.Text = "Bind SelectedValue";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(311, 76);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 48;
            this.label16.Text = "执行状态：";
            // 
            // cmb_Dtl_nQCStatus
            // 
            this.cmb_Dtl_nQCStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_Dtl_nQCStatus.FormattingEnabled = true;
            this.cmb_Dtl_nQCStatus.Location = new System.Drawing.Point(545, 73);
            this.cmb_Dtl_nQCStatus.Name = "cmb_Dtl_nQCStatus";
            this.cmb_Dtl_nQCStatus.Size = new System.Drawing.Size(102, 20);
            this.cmb_Dtl_nQCStatus.TabIndex = 47;
            this.cmb_Dtl_nQCStatus.Tag = "101";
            this.cmb_Dtl_nQCStatus.Text = "Bind SelectedValue";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(483, 75);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 46;
            this.label17.Text = "质检状态：";
            // 
            // grdDtl
            // 
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.AllowUserToDeleteRows = false;
            this.grdDtl.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDtl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdDtl.ColumnHeadersHeight = 35;
            this.grdDtl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colnItem,
            this.colcMNo,
            this.colcMName,
            this.colcSpec,
            this.colcBatchNo,
            this.colfQty,
            this.colfPallet,
            this.colfFinished,
            this.colnQCStatus,
            this.colnDoStatus,
            this.colcPStatus,
            this.colcUnit,
            this.coldProdDate,
            this.col_Dtl_cWHIdErp,
            this.col_Dtl_cAreaIdErp,
            this.col_Dtl_cPosIdErp});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDtl.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdDtl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdDtl.Location = new System.Drawing.Point(11, 7);
            this.grdDtl.MultiSelect = false;
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDtl.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 23;
            this.grdDtl.Size = new System.Drawing.Size(709, 232);
            this.grdDtl.TabIndex = 2;
            this.grdDtl.Tag = "8";
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlEdit.Controls.Add(this.lbl_BillTskIsOver);
            this.pnlEdit.Controls.Add(this.lbl_Check);
            this.pnlEdit.Controls.Add(this.label30);
            this.pnlEdit.Controls.Add(this.label23);
            this.pnlEdit.Controls.Add(this.label20);
            this.pnlEdit.Controls.Add(this.label22);
            this.pnlEdit.Controls.Add(this.cmb_cDept);
            this.pnlEdit.Controls.Add(this.lbl_Customer);
            this.pnlEdit.Controls.Add(this.cmb_nPStatus);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.label14);
            this.pnlEdit.Controls.Add(this.cmb_cPayer);
            this.pnlEdit.Controls.Add(this.txt_cRemark);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.dtp_dCreateDate);
            this.pnlEdit.Controls.Add(this.cmb_cBTypeId);
            this.pnlEdit.Controls.Add(this.txt_cBNoFrom);
            this.pnlEdit.Controls.Add(this.label15);
            this.pnlEdit.Controls.Add(this.txt_cChecker);
            this.pnlEdit.Controls.Add(this.lblChecker);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.txt_cBNo);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(725, 122);
            this.pnlEdit.TabIndex = 0;
            // 
            // lbl_BillTskIsOver
            // 
            this.lbl_BillTskIsOver.AutoSize = true;
            this.lbl_BillTskIsOver.BackColor = System.Drawing.Color.Transparent;
            this.lbl_BillTskIsOver.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_BillTskIsOver.ForeColor = System.Drawing.Color.Red;
            this.lbl_BillTskIsOver.Location = new System.Drawing.Point(519, 67);
            this.lbl_BillTskIsOver.Name = "lbl_BillTskIsOver";
            this.lbl_BillTskIsOver.Size = new System.Drawing.Size(96, 12);
            this.lbl_BillTskIsOver.TabIndex = 56;
            this.lbl_BillTskIsOver.Text = "单据作业已完成";
            this.lbl_BillTskIsOver.Visible = false;
            // 
            // lbl_Check
            // 
            this.lbl_Check.AutoSize = true;
            this.lbl_Check.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Check.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Check.ForeColor = System.Drawing.Color.Red;
            this.lbl_Check.Location = new System.Drawing.Point(457, 67);
            this.lbl_Check.Name = "lbl_Check";
            this.lbl_Check.Size = new System.Drawing.Size(44, 12);
            this.lbl_Check.TabIndex = 55;
            this.lbl_Check.Text = "已审核";
            this.lbl_Check.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label30.Location = new System.Drawing.Point(644, 37);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(11, 12);
            this.label30.TabIndex = 54;
            this.label30.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label23.Location = new System.Drawing.Point(239, 35);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(11, 12);
            this.label23.TabIndex = 53;
            this.label23.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label20.Location = new System.Drawing.Point(431, 35);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 52;
            this.label20.Text = "*";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label22.Location = new System.Drawing.Point(643, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 12);
            this.label22.TabIndex = 51;
            this.label22.Text = "*";
            // 
            // cmb_cDept
            // 
            this.cmb_cDept.FormattingEnabled = true;
            this.cmb_cDept.Location = new System.Drawing.Point(307, 35);
            this.cmb_cDept.Name = "cmb_cDept";
            this.cmb_cDept.Size = new System.Drawing.Size(121, 20);
            this.cmb_cDept.TabIndex = 5;
            this.cmb_cDept.Tag = "1";
            this.cmb_cDept.Text = "Bind Text";
            // 
            // lbl_Customer
            // 
            this.lbl_Customer.AutoSize = true;
            this.lbl_Customer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Customer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Customer.Location = new System.Drawing.Point(249, 38);
            this.lbl_Customer.Name = "lbl_Customer";
            this.lbl_Customer.Size = new System.Drawing.Size(65, 12);
            this.lbl_Customer.TabIndex = 48;
            this.lbl_Customer.Text = "领料单位：";
            this.lbl_Customer.Click += new System.EventHandler(this.lbl_Customer_Click);
            // 
            // cmb_nPStatus
            // 
            this.cmb_nPStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_nPStatus.FormattingEnabled = true;
            this.cmb_nPStatus.Location = new System.Drawing.Point(307, 64);
            this.cmb_nPStatus.Name = "cmb_nPStatus";
            this.cmb_nPStatus.Size = new System.Drawing.Size(121, 20);
            this.cmb_nPStatus.TabIndex = 8;
            this.cmb_nPStatus.Tag = "101";
            this.cmb_nPStatus.Text = "Bind SelectedValue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(249, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 46;
            this.label9.Text = "单据状态：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(448, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 45;
            this.label14.Text = "仓库员：";
            // 
            // cmb_cPayer
            // 
            this.cmb_cPayer.FormattingEnabled = true;
            this.cmb_cPayer.Location = new System.Drawing.Point(519, 9);
            this.cmb_cPayer.Name = "cmb_cPayer";
            this.cmb_cPayer.Size = new System.Drawing.Size(121, 20);
            this.cmb_cPayer.TabIndex = 2;
            this.cmb_cPayer.Tag = "1";
            this.cmb_cPayer.Text = "Bind Text";
            // 
            // txt_cRemark
            // 
            this.txt_cRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cRemark.Location = new System.Drawing.Point(91, 90);
            this.txt_cRemark.Multiline = true;
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.ReadOnly = true;
            this.txt_cRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_cRemark.Size = new System.Drawing.Size(554, 30);
            this.txt_cRemark.TabIndex = 9;
            this.txt_cRemark.Tag = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "备注：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(448, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "单据日期：";
            // 
            // dtp_dCreateDate
            // 
            this.dtp_dCreateDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_dCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_dCreateDate.Location = new System.Drawing.Point(519, 35);
            this.dtp_dCreateDate.Name = "dtp_dCreateDate";
            this.dtp_dCreateDate.Size = new System.Drawing.Size(121, 21);
            this.dtp_dCreateDate.TabIndex = 7;
            this.dtp_dCreateDate.Tag = "2";
            // 
            // cmb_cBTypeId
            // 
            this.cmb_cBTypeId.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_cBTypeId.FormattingEnabled = true;
            this.cmb_cBTypeId.Location = new System.Drawing.Point(91, 32);
            this.cmb_cBTypeId.Name = "cmb_cBTypeId";
            this.cmb_cBTypeId.Size = new System.Drawing.Size(121, 20);
            this.cmb_cBTypeId.TabIndex = 3;
            this.cmb_cBTypeId.Tag = "101";
            this.cmb_cBTypeId.Text = "Bind SelectedValue";
            this.cmb_cBTypeId.SelectedValueChanged += new System.EventHandler(this.cmb_cBTypeId_SelectedValueChanged);
            // 
            // txt_cBNoFrom
            // 
            this.txt_cBNoFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cBNoFrom.Location = new System.Drawing.Point(91, 61);
            this.txt_cBNoFrom.Name = "txt_cBNoFrom";
            this.txt_cBNoFrom.ReadOnly = true;
            this.txt_cBNoFrom.Size = new System.Drawing.Size(121, 21);
            this.txt_cBNoFrom.TabIndex = 6;
            this.txt_cBNoFrom.Tag = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 28;
            this.label15.Text = "来源单号：";
            // 
            // txt_cChecker
            // 
            this.txt_cChecker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cChecker.Location = new System.Drawing.Point(307, 6);
            this.txt_cChecker.Name = "txt_cChecker";
            this.txt_cChecker.ReadOnly = true;
            this.txt_cChecker.Size = new System.Drawing.Size(123, 21);
            this.txt_cChecker.TabIndex = 1;
            this.txt_cChecker.Tag = "0";
            // 
            // lblChecker
            // 
            this.lblChecker.AutoSize = true;
            this.lblChecker.Location = new System.Drawing.Point(249, 11);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(53, 12);
            this.lblChecker.TabIndex = 24;
            this.lblChecker.Text = "审核人：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "出库类型：";
            // 
            // txt_cBNo
            // 
            this.txt_cBNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cBNo.Location = new System.Drawing.Point(91, 7);
            this.txt_cBNo.Name = "txt_cBNo";
            this.txt_cBNo.ReadOnly = true;
            this.txt_cBNo.Size = new System.Drawing.Size(122, 21);
            this.txt_cBNo.TabIndex = 0;
            this.txt_cBNo.Tag = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "单号：";
            // 
            // colnItem
            // 
            this.colnItem.DataPropertyName = "nItem";
            this.colnItem.FillWeight = 50F;
            this.colnItem.Frozen = true;
            this.colnItem.HeaderText = "项次";
            this.colnItem.Name = "colnItem";
            this.colnItem.ReadOnly = true;
            this.colnItem.ToolTipText = "项次";
            this.colnItem.Width = 65;
            // 
            // colcMNo
            // 
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编码";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编码";
            this.colcMNo.Width = 75;
            // 
            // colcMName
            // 
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            // 
            // colcSpec
            // 
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.colcSpec.Width = 70;
            // 
            // colcBatchNo
            // 
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            this.colcBatchNo.ToolTipText = "批号";
            this.colcBatchNo.Width = 75;
            // 
            // colfQty
            // 
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.ToolTipText = "数量";
            this.colfQty.Width = 75;
            // 
            // colfPallet
            // 
            this.colfPallet.DataPropertyName = "fPallet";
            this.colfPallet.HeaderText = "配盘数量";
            this.colfPallet.Name = "colfPallet";
            this.colfPallet.ReadOnly = true;
            this.colfPallet.ToolTipText = "配盘数量";
            this.colfPallet.Width = 75;
            // 
            // colfFinished
            // 
            this.colfFinished.DataPropertyName = "fFinished";
            this.colfFinished.HeaderText = "完成数";
            this.colfFinished.Name = "colfFinished";
            this.colfFinished.ReadOnly = true;
            this.colfFinished.ToolTipText = "完成数";
            this.colfFinished.Width = 75;
            // 
            // colnQCStatus
            // 
            this.colnQCStatus.DataPropertyName = "cQCStatus";
            this.colnQCStatus.HeaderText = "检验状态";
            this.colnQCStatus.Name = "colnQCStatus";
            this.colnQCStatus.ReadOnly = true;
            this.colnQCStatus.ToolTipText = "检验状态";
            this.colnQCStatus.Visible = false;
            // 
            // colnDoStatus
            // 
            this.colnDoStatus.DataPropertyName = "cDoStatus";
            this.colnDoStatus.HeaderText = "执行状态";
            this.colnDoStatus.Name = "colnDoStatus";
            this.colnDoStatus.ReadOnly = true;
            this.colnDoStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colnDoStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colnDoStatus.ToolTipText = "执行状态";
            this.colnDoStatus.Visible = false;
            this.colnDoStatus.Width = 50;
            // 
            // colcPStatus
            // 
            this.colcPStatus.DataPropertyName = "cPStatus";
            this.colcPStatus.HeaderText = "配盘状态";
            this.colcPStatus.Name = "colcPStatus";
            this.colcPStatus.ReadOnly = true;
            // 
            // colcUnit
            // 
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "计量单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.ToolTipText = "计量单位";
            this.colcUnit.Width = 50;
            // 
            // coldProdDate
            // 
            this.coldProdDate.DataPropertyName = "dProdDate";
            this.coldProdDate.HeaderText = "生产日期";
            this.coldProdDate.Name = "coldProdDate";
            this.coldProdDate.ReadOnly = true;
            this.coldProdDate.ToolTipText = "生产日期";
            // 
            // col_Dtl_cWHIdErp
            // 
            this.col_Dtl_cWHIdErp.DataPropertyName = "cWHIdErp";
            this.col_Dtl_cWHIdErp.HeaderText = "ERP仓库号";
            this.col_Dtl_cWHIdErp.Name = "col_Dtl_cWHIdErp";
            this.col_Dtl_cWHIdErp.ReadOnly = true;
            this.col_Dtl_cWHIdErp.Visible = false;
            // 
            // col_Dtl_cAreaIdErp
            // 
            this.col_Dtl_cAreaIdErp.DataPropertyName = "cAreaIdErp";
            this.col_Dtl_cAreaIdErp.HeaderText = "ERP货区号";
            this.col_Dtl_cAreaIdErp.Name = "col_Dtl_cAreaIdErp";
            this.col_Dtl_cAreaIdErp.ReadOnly = true;
            this.col_Dtl_cAreaIdErp.Visible = false;
            // 
            // col_Dtl_cPosIdErp
            // 
            this.col_Dtl_cPosIdErp.DataPropertyName = "cPosIdErp";
            this.col_Dtl_cPosIdErp.HeaderText = "ERP仓位号";
            this.col_Dtl_cPosIdErp.Name = "col_Dtl_cPosIdErp";
            this.col_Dtl_cPosIdErp.ReadOnly = true;
            this.col_Dtl_cPosIdErp.Visible = false;
            // 
            // frmBillOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1016, 648);
            this.Controls.Add(this.pnlSplit);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.KeyPreview = true;
            this.Name = "frmBillOut";
            this.Text = "立库出库管理";
            this.Load += new System.EventHandler(this.frmBillOut_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDtl)).EndInit();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel1.PerformLayout();
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlDtl.ResumeLayout(false);
            this.pnlDtl.PerformLayout();
            this.pnlBtns.ResumeLayout(false);
            this.pnlDtlEdit.ResumeLayout(false);
            this.pnlDtlEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDtl)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton tlb_M_New;
        public System.Windows.Forms.ToolStripButton tlb_M_Edit;
        public System.Windows.Forms.ToolStrip tlbMain;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton tlb_M_Undo;
        public System.Windows.Forms.ToolStripButton tlb_M_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton tlb_M_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripButton tlb_M_Refresh;
        public System.Windows.Forms.ToolStripButton tlb_M_Find;
        public System.Windows.Forms.ToolStripButton tlb_M_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        public System.Windows.Forms.ToolStripButton tlb_M_Check;
        private System.Windows.Forms.ToolStripButton tlb_M_UnCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tlb_M_Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btn_M_Help;
        private System.Windows.Forms.ToolStripButton tlb_M_Exit;
        private System.Windows.Forms.ToolStripButton tlbSaveSysRts;
        public System.Windows.Forms.ToolStripStatusLabel stbDateTime;
        public System.Windows.Forms.Timer tmrMain;
        public System.Windows.Forms.ToolStripStatusLabel stbUser;
        public System.Windows.Forms.StatusStrip stbMain;
        public System.Windows.Forms.ToolStripStatusLabel stbModul;
        public System.Windows.Forms.ToolStripStatusLabel stbState;
        private System.Windows.Forms.BindingSource bdsMain;
        private System.Windows.Forms.BindingSource bdsDtl;
        public System.Windows.Forms.SplitContainer pnlSplit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFindBillFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFindType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUnFind;
        private System.Windows.Forms.ComboBox cmbFindCheck;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbFindUser;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker dtpFind_E;
        private System.Windows.Forms.DateTimePicker dtpFind_B;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnQry;
        public System.Windows.Forms.DataGridView grdList;
        public System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label lbl_Check;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmb_cDept;
        private System.Windows.Forms.Label lbl_Customer;
        private System.Windows.Forms.ComboBox cmb_nPStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmb_cPayer;
        private System.Windows.Forms.TextBox txt_cRemark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_dCreateDate;
        private System.Windows.Forms.ComboBox cmb_cBTypeId;
        private System.Windows.Forms.TextBox txt_cBNoFrom;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_cChecker;
        private System.Windows.Forms.Label lblChecker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_cBNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlDtl;
        private System.Windows.Forms.Panel pnlBtns;
        private System.Windows.Forms.Button btn_Dtl_Delete;
        private System.Windows.Forms.Button btn_Dtl_Edit;
        private System.Windows.Forms.Button btn_Dtl_New;
        private System.Windows.Forms.Panel pnlDtlEdit;
        private System.Windows.Forms.TextBox txt_Dtl_cSpec;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Dtl_cMName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Dtl_cUnit;
        private System.Windows.Forms.TextBox txt_Dtl_fFinished;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txt_Dtl_fPallet;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_Dtl_fQty;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DateTimePicker dtp_Dtl_dProdDate;
        private System.Windows.Forms.TextBox txt_Dtl_cBatchNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_Dtl_cMNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmb_Dtl_nDoStatus;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmb_Dtl_nQCStatus;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.DataGridView grdDtl;
        private System.Windows.Forms.ToolStripButton tlb_M_OverBWK;
        private System.Windows.Forms.Label lbl_BillTskIsOver;
        public System.Windows.Forms.ToolStripButton tlb_M_ErpImp;
        private System.Windows.Forms.Label lbl_Dtl;
        private System.Windows.Forms.Label lbl_Bill_Count;
        private System.Windows.Forms.Label lbl_bill;
        private System.Windows.Forms.Label lbl_Dtl_Count;
        private System.Windows.Forms.ToolTip ttpMain;
        private System.Windows.Forms.ComboBox cmb_FinishedStatus;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBId;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_M_cLinkId;
        private System.Windows.Forms.DataGridViewComboBoxColumn colcBTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_M_cBNoFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Main_bIsChecked;
        private System.Windows.Forms.DataGridViewComboBoxColumn colcWHId;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Main_nBClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Main_bIsFinished;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Main_cCreator;
        public System.Windows.Forms.ToolStripButton tlb_M_UpdateDtlQtyAfterDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfPallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfFinished;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnQCStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnDoStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcPStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldProdDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Dtl_cWHIdErp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Dtl_cAreaIdErp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Dtl_cPosIdErp;
    }
}
