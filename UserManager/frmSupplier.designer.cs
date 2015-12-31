namespace UserMS
{
    partial class frmSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSupplier));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlb_M_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Undo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Find = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Availability = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colcComptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coscCSName_J = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coscCSName_Q = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coscFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coscAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coscRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colbUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coscCmpId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnIsFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnIsInner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_cCmptId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_cRemark = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_cAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_cFax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmb_bUsed = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_nIsFactory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_nIsInner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_cCSNameQ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_cCSNameJ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cCSId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bdsMain = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.tlbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.stbMain.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).BeginInit();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.SuspendLayout();
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlbMain
            // 
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator2,
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
            this.tlb_M_Availability,
            this.toolStripSeparator7,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.toolStripSeparator8,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(777, 25);
            this.tlbMain.TabIndex = 16;
            this.tlbMain.Text = "toolStrip1";
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
            this.tlb_M_Find.Visible = false;
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
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Visible = false;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Availability
            // 
            this.tlb_M_Availability.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Availability.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Availability.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Availability.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Availability.Image")));
            this.tlb_M_Availability.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Availability.Name = "tlb_M_Availability";
            this.tlb_M_Availability.Size = new System.Drawing.Size(100, 22);
            this.tlb_M_Availability.Tag = "15";
            this.tlb_M_Availability.Text = "资质合法性维护";
            this.tlb_M_Availability.Click += new System.EventHandler(this.tlb_M_Availability_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tlbSaveSysRts
            // 
            this.tlbSaveSysRts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = ((System.Drawing.Image)(resources.GetObject("tlbSaveSysRts.Image")));
            this.tlbSaveSysRts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new System.Drawing.Size(81, 22);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new System.EventHandler(this.tlbSaveSysRts_Click);
            // 
            // stbUser
            // 
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new System.Drawing.Size(47, 17);
            this.stbUser.Text = "用户名:";
            // 
            // stbState
            // 
            this.stbState.Name = "stbState";
            this.stbState.Size = new System.Drawing.Size(35, 17);
            this.stbState.Text = "状态:";
            // 
            // tmrMain
            // 
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 5000;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcComptId,
            this.coscCSName_J,
            this.coscCSName_Q,
            this.coscFax,
            this.coscAddress,
            this.coscRemark,
            this.colbUsed,
            this.coscCmpId,
            this.colnIsFactory,
            this.colnIsInner});
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new System.Drawing.Point(3, 28);
            this.grdList.MultiSelect = false;
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
            this.grdList.Size = new System.Drawing.Size(235, 422);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            this.grdList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdList_DataBindingComplete);
            // 
            // colcComptId
            // 
            this.colcComptId.DataPropertyName = "cCSId";
            this.colcComptId.Frozen = true;
            this.colcComptId.HeaderText = "编号";
            this.colcComptId.Name = "colcComptId";
            this.colcComptId.ReadOnly = true;
            this.colcComptId.ToolTipText = "编号";
            // 
            // coscCSName_J
            // 
            this.coscCSName_J.DataPropertyName = "cCSNameJ";
            this.coscCSName_J.HeaderText = "简称";
            this.coscCSName_J.Name = "coscCSName_J";
            this.coscCSName_J.ReadOnly = true;
            // 
            // coscCSName_Q
            // 
            this.coscCSName_Q.DataPropertyName = "cCSNameQ";
            this.coscCSName_Q.HeaderText = "全称";
            this.coscCSName_Q.Name = "coscCSName_Q";
            this.coscCSName_Q.ReadOnly = true;
            // 
            // coscFax
            // 
            this.coscFax.DataPropertyName = "cFax";
            this.coscFax.HeaderText = "传真";
            this.coscFax.Name = "coscFax";
            this.coscFax.ReadOnly = true;
            // 
            // coscAddress
            // 
            this.coscAddress.DataPropertyName = "cAddress";
            this.coscAddress.HeaderText = "地址";
            this.coscAddress.Name = "coscAddress";
            this.coscAddress.ReadOnly = true;
            // 
            // coscRemark
            // 
            this.coscRemark.DataPropertyName = "cRemark";
            this.coscRemark.HeaderText = "备注";
            this.coscRemark.Name = "coscRemark";
            this.coscRemark.ReadOnly = true;
            this.coscRemark.ToolTipText = "备注";
            // 
            // colbUsed
            // 
            this.colbUsed.DataPropertyName = "bUsed";
            this.colbUsed.HeaderText = "是否起用";
            this.colbUsed.Name = "colbUsed";
            this.colbUsed.ReadOnly = true;
            this.colbUsed.ToolTipText = "是否起用";
            // 
            // coscCmpId
            // 
            this.coscCmpId.DataPropertyName = "cCmpId";
            this.coscCmpId.HeaderText = "单位编号";
            this.coscCmpId.Name = "coscCmpId";
            this.coscCmpId.ReadOnly = true;
            this.coscCmpId.ToolTipText = "单位编号";
            // 
            // colnIsFactory
            // 
            this.colnIsFactory.DataPropertyName = "nIsFactory";
            this.colnIsFactory.HeaderText = "是否生产厂家";
            this.colnIsFactory.Name = "colnIsFactory";
            this.colnIsFactory.ReadOnly = true;
            this.colnIsFactory.ToolTipText = "是否生产厂家";
            // 
            // colnIsInner
            // 
            this.colnIsInner.DataPropertyName = "nIsInner";
            this.colnIsInner.HeaderText = "是否内部单位";
            this.colnIsInner.Name = "colnIsInner";
            this.colnIsInner.ReadOnly = true;
            this.colnIsInner.ToolTipText = "是否内部单位";
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbModul,
            this.stbUser,
            this.stbState,
            this.stbDateTime});
            this.stbMain.Location = new System.Drawing.Point(0, 467);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(777, 22);
            this.stbMain.TabIndex = 15;
            this.stbMain.Text = "statusStrip1";
            // 
            // stbModul
            // 
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new System.Drawing.Size(35, 17);
            this.stbModul.Text = "模块:";
            // 
            // stbDateTime
            // 
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new System.Drawing.Size(35, 17);
            this.stbDateTime.Text = "时间:";
            // 
            // txt_cCmptId
            // 
            this.txt_cCmptId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCmptId.Location = new System.Drawing.Point(98, 193);
            this.txt_cCmptId.Name = "txt_cCmptId";
            this.txt_cCmptId.ReadOnly = true;
            this.txt_cCmptId.Size = new System.Drawing.Size(379, 21);
            this.txt_cCmptId.TabIndex = 8;
            this.txt_cCmptId.Tag = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "单位名称：";
            // 
            // txt_cRemark
            // 
            this.txt_cRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cRemark.Location = new System.Drawing.Point(98, 163);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.ReadOnly = true;
            this.txt_cRemark.Size = new System.Drawing.Size(379, 21);
            this.txt_cRemark.TabIndex = 7;
            this.txt_cRemark.Tag = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "备    注：";
            // 
            // txt_cAddress
            // 
            this.txt_cAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cAddress.Location = new System.Drawing.Point(98, 132);
            this.txt_cAddress.Name = "txt_cAddress";
            this.txt_cAddress.ReadOnly = true;
            this.txt_cAddress.Size = new System.Drawing.Size(379, 21);
            this.txt_cAddress.TabIndex = 6;
            this.txt_cAddress.Tag = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "地    址：";
            // 
            // txt_cFax
            // 
            this.txt_cFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cFax.Location = new System.Drawing.Point(98, 102);
            this.txt_cFax.Name = "txt_cFax";
            this.txt_cFax.ReadOnly = true;
            this.txt_cFax.Size = new System.Drawing.Size(379, 21);
            this.txt_cFax.TabIndex = 5;
            this.txt_cFax.Tag = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "传    真：";
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlEdit.Controls.Add(this.label15);
            this.pnlEdit.Controls.Add(this.label14);
            this.pnlEdit.Controls.Add(this.label12);
            this.pnlEdit.Controls.Add(this.label11);
            this.pnlEdit.Controls.Add(this.label13);
            this.pnlEdit.Controls.Add(this.cmb_bUsed);
            this.pnlEdit.Controls.Add(this.label10);
            this.pnlEdit.Controls.Add(this.cmb_nIsFactory);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.cmb_nIsInner);
            this.pnlEdit.Controls.Add(this.label1);
            this.pnlEdit.Controls.Add(this.txt_cCmptId);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.txt_cRemark);
            this.pnlEdit.Controls.Add(this.label8);
            this.pnlEdit.Controls.Add(this.txt_cAddress);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.txt_cFax);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.txt_cCSNameQ);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.txt_cCSNameJ);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.txt_cCSId);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Location = new System.Drawing.Point(20, 45);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(500, 347);
            this.pnlEdit.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(189, 259);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 55;
            this.label15.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(398, 230);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 54;
            this.label14.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(189, 233);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 53;
            this.label12.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(482, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 52;
            this.label11.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(482, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 51;
            this.label13.Text = "*";
            // 
            // cmb_bUsed
            // 
            this.cmb_bUsed.FormattingEnabled = true;
            this.cmb_bUsed.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmb_bUsed.Location = new System.Drawing.Point(120, 256);
            this.cmb_bUsed.Name = "cmb_bUsed";
            this.cmb_bUsed.Size = new System.Drawing.Size(63, 20);
            this.cmb_bUsed.TabIndex = 19;
            this.cmb_bUsed.Tag = "102";
            this.cmb_bUsed.Text = "Bind SelectedIndex";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 256);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "是否启用：";
            // 
            // cmb_nIsFactory
            // 
            this.cmb_nIsFactory.FormattingEnabled = true;
            this.cmb_nIsFactory.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmb_nIsFactory.Location = new System.Drawing.Point(329, 227);
            this.cmb_nIsFactory.Name = "cmb_nIsFactory";
            this.cmb_nIsFactory.Size = new System.Drawing.Size(63, 20);
            this.cmb_nIsFactory.TabIndex = 17;
            this.cmb_nIsFactory.Tag = "102";
            this.cmb_nIsFactory.Text = "Bind SelectedIndex";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(234, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "是否生产厂家：";
            // 
            // cmb_nIsInner
            // 
            this.cmb_nIsInner.FormattingEnabled = true;
            this.cmb_nIsInner.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmb_nIsInner.Location = new System.Drawing.Point(120, 230);
            this.cmb_nIsInner.Name = "cmb_nIsInner";
            this.cmb_nIsInner.Size = new System.Drawing.Size(63, 20);
            this.cmb_nIsInner.TabIndex = 15;
            this.cmb_nIsInner.Tag = "102";
            this.cmb_nIsInner.Text = "Bind SelectedIndex";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "是否内部单位：";
            // 
            // txt_cCSNameQ
            // 
            this.txt_cCSNameQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCSNameQ.Location = new System.Drawing.Point(98, 69);
            this.txt_cCSNameQ.Name = "txt_cCSNameQ";
            this.txt_cCSNameQ.ReadOnly = true;
            this.txt_cCSNameQ.Size = new System.Drawing.Size(379, 21);
            this.txt_cCSNameQ.TabIndex = 3;
            this.txt_cCSNameQ.Tag = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "全    称：";
            // 
            // txt_cCSNameJ
            // 
            this.txt_cCSNameJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCSNameJ.Location = new System.Drawing.Point(98, 40);
            this.txt_cCSNameJ.Name = "txt_cCSNameJ";
            this.txt_cCSNameJ.ReadOnly = true;
            this.txt_cCSNameJ.Size = new System.Drawing.Size(379, 21);
            this.txt_cCSNameJ.TabIndex = 2;
            this.txt_cCSNameJ.Tag = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "简    称：";
            // 
            // txt_cCSId
            // 
            this.txt_cCSId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCSId.Location = new System.Drawing.Point(98, 13);
            this.txt_cCSId.Name = "txt_cCSId";
            this.txt_cCSId.ReadOnly = true;
            this.txt_cCSId.Size = new System.Drawing.Size(379, 21);
            this.txt_cCSId.TabIndex = 1;
            this.txt_cCSId.Tag = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "编    号：";
            // 
            // bdsMain
            // 
            this.bdsMain.PositionChanged += new System.EventHandler(this.bdsMain_PositionChanged);
            // 
            // pnlSplit
            // 
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplit.Location = new System.Drawing.Point(0, 0);
            this.pnlSplit.Name = "pnlSplit";
            // 
            // pnlSplit.Panel1
            // 
            this.pnlSplit.Panel1.Controls.Add(this.grdList);
            // 
            // pnlSplit.Panel2
            // 
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pnlSplit.Size = new System.Drawing.Size(777, 489);
            this.pnlSplit.SplitterDistance = 241;
            this.pnlSplit.TabIndex = 17;
            // 
            // frmSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(777, 489);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.pnlSplit);
            this.Name = "frmSupplier";
            this.Load += new System.EventHandler(this.frmSupplier_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).EndInit();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStripButton tlb_M_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton tlb_M_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripButton tlb_M_Refresh;
        public System.Windows.Forms.ToolStripButton tlb_M_Undo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStrip tlbMain;
        public System.Windows.Forms.ToolStripButton tlb_M_New;
        public System.Windows.Forms.ToolStripButton tlb_M_Edit;
        public System.Windows.Forms.ToolStripButton tlb_M_Find;
        public System.Windows.Forms.ToolStripButton tlb_M_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btn_M_Help;
        private System.Windows.Forms.ToolStripButton tlb_M_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tlbSaveSysRts;
        public System.Windows.Forms.ToolStripStatusLabel stbUser;
        public System.Windows.Forms.ToolStripStatusLabel stbState;
        public System.Windows.Forms.Timer tmrMain;
        public System.Windows.Forms.DataGridView grdList;
        public System.Windows.Forms.StatusStrip stbMain;
        public System.Windows.Forms.ToolStripStatusLabel stbModul;
        public System.Windows.Forms.ToolStripStatusLabel stbDateTime;
        private System.Windows.Forms.TextBox txt_cCmptId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_cRemark;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_cAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_cFax;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.TextBox txt_cCSNameQ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_cCSNameJ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cCSId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bdsMain;
        public System.Windows.Forms.SplitContainer pnlSplit;
        private System.Windows.Forms.ComboBox cmb_nIsInner;
        private System.Windows.Forms.ComboBox cmb_bUsed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_nIsFactory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tlb_M_Availability;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcComptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn coscCSName_J;
        private System.Windows.Forms.DataGridViewTextBoxColumn coscCSName_Q;
        private System.Windows.Forms.DataGridViewTextBoxColumn coscFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn coscAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn coscRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colbUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn coscCmpId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnIsFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnIsInner;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
    }
}
