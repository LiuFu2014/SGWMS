namespace WareStoreMS
{
    partial class frmBillRemove
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillRemove));
            this.txt_Dtl_cSpec = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Dtl_cMName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Dtl_cUnit = new System.Windows.Forms.TextBox();
            this.pnlDtlEdit = new System.Windows.Forms.Panel();
            this.txt_Dtl_fQty = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_Dtl_cMNo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbFindType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUnFind = new System.Windows.Forms.Button();
            this.cmbFindCheck = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpFind_E = new System.Windows.Forms.DateTimePicker();
            this.dtpFind_B = new System.Windows.Forms.DateTimePicker();
            this.pnlDtl = new System.Windows.Forms.Panel();
            this.grdDtl = new System.Windows.Forms.DataGridView();
            this.colcMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ppmDtl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_Dtl_PrintBarCode = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBtns = new System.Windows.Forms.Panel();
            this.btn_Dtl_Delete = new System.Windows.Forms.Button();
            this.btn_Dtl_Edit = new System.Windows.Forms.Button();
            this.btn_Dtl_New = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQry = new System.Windows.Forms.Button();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colcBId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcWHIdFrom = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colcWHIdTo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bdsMain = new System.Windows.Forms.BindingSource(this.components);
            this.bdsDtl = new System.Windows.Forms.BindingSource(this.components);
            this.txt_cRemark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_cWHIdFrom = new System.Windows.Forms.ComboBox();
            this.txt_cLinkId = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_cChecker = new System.Windows.Forms.TextBox();
            this.lblChecker = new System.Windows.Forms.Label();
            this.tlb_M_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Find = new System.Windows.Forms.ToolStripButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_cBNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_cPayer = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dtp_dCreateDate = new System.Windows.Forms.DateTimePicker();
            this.txtFindBillFrom = new System.Windows.Forms.TextBox();
            this.lbl_Check = new System.Windows.Forms.Label();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.txt_cBNoIn = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmb_cMatClass = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txt_cBNoOut = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmb_cWHIdTo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_nPStatus = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbFindUser = new System.Windows.Forms.ComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Undo = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Delete = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Check = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_UnCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_BldBillIn = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Item = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.tlb_M_ErpImp = new System.Windows.Forms.ToolStripButton();
            this.pnlDtlEdit.SuspendLayout();
            this.pnlDtl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDtl)).BeginInit();
            this.ppmDtl.SuspendLayout();
            this.pnlBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDtl)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.stbMain.SuspendLayout();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Dtl_cSpec
            // 
            this.txt_Dtl_cSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cSpec.Location = new System.Drawing.Point(241, 50);
            this.txt_Dtl_cSpec.Name = "txt_Dtl_cSpec";
            this.txt_Dtl_cSpec.ReadOnly = true;
            this.txt_Dtl_cSpec.Size = new System.Drawing.Size(106, 21);
            this.txt_Dtl_cSpec.TabIndex = 75;
            this.txt_Dtl_cSpec.Tag = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(200, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 76;
            this.label11.Text = "规格：";
            // 
            // txt_Dtl_cMName
            // 
            this.txt_Dtl_cMName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cMName.Location = new System.Drawing.Point(94, 14);
            this.txt_Dtl_cMName.Name = "txt_Dtl_cMName";
            this.txt_Dtl_cMName.ReadOnly = true;
            this.txt_Dtl_cMName.Size = new System.Drawing.Size(524, 21);
            this.txt_Dtl_cMName.TabIndex = 73;
            this.txt_Dtl_cMName.Tag = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 74;
            this.label10.Text = "名称：";
            // 
            // txt_Dtl_cUnit
            // 
            this.txt_Dtl_cUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cUnit.Location = new System.Drawing.Point(535, 50);
            this.txt_Dtl_cUnit.Name = "txt_Dtl_cUnit";
            this.txt_Dtl_cUnit.ReadOnly = true;
            this.txt_Dtl_cUnit.Size = new System.Drawing.Size(83, 21);
            this.txt_Dtl_cUnit.TabIndex = 72;
            this.txt_Dtl_cUnit.Tag = "0";
            // 
            // pnlDtlEdit
            // 
            this.pnlDtlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cSpec);
            this.pnlDtlEdit.Controls.Add(this.label11);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMName);
            this.pnlDtlEdit.Controls.Add(this.label10);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cUnit);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_fQty);
            this.pnlDtlEdit.Controls.Add(this.label25);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMNo);
            this.pnlDtlEdit.Controls.Add(this.label19);
            this.pnlDtlEdit.Controls.Add(this.label12);
            this.pnlDtlEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDtlEdit.Location = new System.Drawing.Point(0, 304);
            this.pnlDtlEdit.Name = "pnlDtlEdit";
            this.pnlDtlEdit.Size = new System.Drawing.Size(647, 93);
            this.pnlDtlEdit.TabIndex = 6;
            this.pnlDtlEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDtlEdit_Paint);
            // 
            // txt_Dtl_fQty
            // 
            this.txt_Dtl_fQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_fQty.Location = new System.Drawing.Point(388, 50);
            this.txt_Dtl_fQty.Name = "txt_Dtl_fQty";
            this.txt_Dtl_fQty.ReadOnly = true;
            this.txt_Dtl_fQty.Size = new System.Drawing.Size(106, 21);
            this.txt_Dtl_fQty.TabIndex = 66;
            this.txt_Dtl_fQty.Tag = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(347, 54);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 12);
            this.label25.TabIndex = 67;
            this.label25.Text = "数量：";
            // 
            // txt_Dtl_cMNo
            // 
            this.txt_Dtl_cMNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Dtl_cMNo.Location = new System.Drawing.Point(94, 50);
            this.txt_Dtl_cMNo.Name = "txt_Dtl_cMNo";
            this.txt_Dtl_cMNo.ReadOnly = true;
            this.txt_Dtl_cMNo.Size = new System.Drawing.Size(106, 21);
            this.txt_Dtl_cMNo.TabIndex = 54;
            this.txt_Dtl_cMNo.Tag = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(29, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 55;
            this.label19.Text = "物料编码：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(494, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 51;
            this.label12.Text = "单位：";
            // 
            // cmbFindType
            // 
            this.cmbFindType.FormattingEnabled = true;
            this.cmbFindType.Items.AddRange(new object[] {
            "平面库",
            "立体库"});
            this.cmbFindType.Location = new System.Drawing.Point(198, 40);
            this.cmbFindType.Name = "cmbFindType";
            this.cmbFindType.Size = new System.Drawing.Size(83, 20);
            this.cmbFindType.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "类型：";
            // 
            // btnUnFind
            // 
            this.btnUnFind.Location = new System.Drawing.Point(223, 89);
            this.btnUnFind.Name = "btnUnFind";
            this.btnUnFind.Size = new System.Drawing.Size(58, 23);
            this.btnUnFind.TabIndex = 16;
            this.btnUnFind.Text = "重置";
            this.btnUnFind.UseVisualStyleBackColor = true;
            // 
            // cmbFindCheck
            // 
            this.cmbFindCheck.FormattingEnabled = true;
            this.cmbFindCheck.Items.AddRange(new object[] {
            "全部",
            "审核",
            "未审核"});
            this.cmbFindCheck.Location = new System.Drawing.Point(74, 90);
            this.cmbFindCheck.Name = "cmbFindCheck";
            this.cmbFindCheck.Size = new System.Drawing.Size(83, 20);
            this.cmbFindCheck.TabIndex = 13;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(16, 94);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 15;
            this.label28.Text = "审核状态：";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.SystemColors.ControlText;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Location = new System.Drawing.Point(167, 19);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 1);
            this.label24.TabIndex = 10;
            // 
            // dtpFind_E
            // 
            this.dtpFind_E.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_E.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFind_E.Location = new System.Drawing.Point(198, 14);
            this.dtpFind_E.Name = "dtpFind_E";
            this.dtpFind_E.Size = new System.Drawing.Size(83, 21);
            this.dtpFind_E.TabIndex = 9;
            this.dtpFind_E.Tag = "2";
            // 
            // dtpFind_B
            // 
            this.dtpFind_B.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_B.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFind_B.Location = new System.Drawing.Point(74, 14);
            this.dtpFind_B.Name = "dtpFind_B";
            this.dtpFind_B.Size = new System.Drawing.Size(83, 21);
            this.dtpFind_B.TabIndex = 8;
            this.dtpFind_B.Tag = "2";
            // 
            // pnlDtl
            // 
            this.pnlDtl.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pnlDtl.Controls.Add(this.grdDtl);
            this.pnlDtl.Controls.Add(this.pnlDtlEdit);
            this.pnlDtl.Controls.Add(this.pnlBtns);
            this.pnlDtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDtl.Location = new System.Drawing.Point(0, 172);
            this.pnlDtl.Name = "pnlDtl";
            this.pnlDtl.Size = new System.Drawing.Size(647, 436);
            this.pnlDtl.TabIndex = 1;
            // 
            // grdDtl
            // 
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.AllowUserToDeleteRows = false;
            this.grdDtl.AllowUserToOrderColumns = true;
            this.grdDtl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcMNo,
            this.colcMName,
            this.colcSpec,
            this.colfQty,
            this.colcUnit});
            this.grdDtl.ContextMenuStrip = this.ppmDtl;
            this.grdDtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDtl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdDtl.Location = new System.Drawing.Point(0, 0);
            this.grdDtl.MultiSelect = false;
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDtl.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 23;
            this.grdDtl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDtl.Size = new System.Drawing.Size(647, 304);
            this.grdDtl.TabIndex = 5;
            this.grdDtl.Tag = "8";
            // 
            // colcMNo
            // 
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编码";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编码";
            // 
            // colcMName
            // 
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.FillWeight = 200F;
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            this.colcMName.Width = 200;
            // 
            // colcSpec
            // 
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            // 
            // colfQty
            // 
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.ToolTipText = "数量";
            // 
            // colcUnit
            // 
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "计量单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.ToolTipText = "计量单位";
            // 
            // ppmDtl
            // 
            this.ppmDtl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_Dtl_PrintBarCode});
            this.ppmDtl.Name = "ppmDtl";
            this.ppmDtl.Size = new System.Drawing.Size(119, 26);
            this.ppmDtl.Opening += new System.ComponentModel.CancelEventHandler(this.ppmDtl_Opening);
            // 
            // mi_Dtl_PrintBarCode
            // 
            this.mi_Dtl_PrintBarCode.Name = "mi_Dtl_PrintBarCode";
            this.mi_Dtl_PrintBarCode.Size = new System.Drawing.Size(118, 22);
            this.mi_Dtl_PrintBarCode.Text = "打印条码";
            // 
            // pnlBtns
            // 
            this.pnlBtns.Controls.Add(this.btn_Dtl_Delete);
            this.pnlBtns.Controls.Add(this.btn_Dtl_Edit);
            this.pnlBtns.Controls.Add(this.btn_Dtl_New);
            this.pnlBtns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBtns.Location = new System.Drawing.Point(0, 397);
            this.pnlBtns.Name = "pnlBtns";
            this.pnlBtns.Size = new System.Drawing.Size(647, 39);
            this.pnlBtns.TabIndex = 4;
            // 
            // btn_Dtl_Delete
            // 
            this.btn_Dtl_Delete.Location = new System.Drawing.Point(261, 8);
            this.btn_Dtl_Delete.Name = "btn_Dtl_Delete";
            this.btn_Dtl_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Dtl_Delete.TabIndex = 3;
            this.btn_Dtl_Delete.Text = "删除";
            this.btn_Dtl_Delete.UseVisualStyleBackColor = true;
            this.btn_Dtl_Delete.Click += new System.EventHandler(this.btn_Dtl_Delete_Click);
            // 
            // btn_Dtl_Edit
            // 
            this.btn_Dtl_Edit.Location = new System.Drawing.Point(163, 8);
            this.btn_Dtl_Edit.Name = "btn_Dtl_Edit";
            this.btn_Dtl_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Dtl_Edit.TabIndex = 2;
            this.btn_Dtl_Edit.Text = "修改";
            this.btn_Dtl_Edit.UseVisualStyleBackColor = true;
            this.btn_Dtl_Edit.Click += new System.EventHandler(this.btn_Dtl_Edit_Click);
            // 
            // btn_Dtl_New
            // 
            this.btn_Dtl_New.Location = new System.Drawing.Point(65, 8);
            this.btn_Dtl_New.Name = "btn_Dtl_New";
            this.btn_Dtl_New.Size = new System.Drawing.Size(75, 23);
            this.btn_Dtl_New.TabIndex = 1;
            this.btn_Dtl_New.Text = "新增";
            this.btn_Dtl_New.UseVisualStyleBackColor = true;
            this.btn_Dtl_New.Click += new System.EventHandler(this.btn_Dtl_New_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "日    期：";
            // 
            // btnQry
            // 
            this.btnQry.Location = new System.Drawing.Point(160, 89);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new System.Drawing.Size(58, 23);
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
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcBId,
            this.colcWHIdFrom,
            this.colcWHIdTo});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new System.Drawing.Point(0, 116);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 23;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(296, 492);
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
            // 
            // colcWHIdFrom
            // 
            this.colcWHIdFrom.DataPropertyName = "cWHIdFrom";
            this.colcWHIdFrom.HeaderText = "源仓库";
            this.colcWHIdFrom.Name = "colcWHIdFrom";
            this.colcWHIdFrom.ReadOnly = true;
            this.colcWHIdFrom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colcWHIdFrom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colcWHIdFrom.ToolTipText = "源仓库";
            // 
            // colcWHIdTo
            // 
            this.colcWHIdTo.DataPropertyName = "cWHIdTo";
            this.colcWHIdTo.HeaderText = "目标仓库";
            this.colcWHIdTo.Name = "colcWHIdTo";
            this.colcWHIdTo.ReadOnly = true;
            this.colcWHIdTo.ToolTipText = "目标仓库";
            // 
            // bdsMain
            // 
            this.bdsMain.PositionChanged += new System.EventHandler(this.bdsMain_PositionChanged);
            // 
            // txt_cRemark
            // 
            this.txt_cRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cRemark.Location = new System.Drawing.Point(109, 103);
            this.txt_cRemark.Multiline = true;
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.ReadOnly = true;
            this.txt_cRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_cRemark.Size = new System.Drawing.Size(502, 62);
            this.txt_cRemark.TabIndex = 9;
            this.txt_cRemark.Tag = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "备    注：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "单据日期：";
            // 
            // cmb_cWHIdFrom
            // 
            this.cmb_cWHIdFrom.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_cWHIdFrom.FormattingEnabled = true;
            this.cmb_cWHIdFrom.Location = new System.Drawing.Point(109, 30);
            this.cmb_cWHIdFrom.Name = "cmb_cWHIdFrom";
            this.cmb_cWHIdFrom.Size = new System.Drawing.Size(121, 20);
            this.cmb_cWHIdFrom.TabIndex = 3;
            this.cmb_cWHIdFrom.Tag = "101";
            this.cmb_cWHIdFrom.Text = "Bind SelectedValue";
            // 
            // txt_cLinkId
            // 
            this.txt_cLinkId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cLinkId.Location = new System.Drawing.Point(109, 53);
            this.txt_cLinkId.Name = "txt_cLinkId";
            this.txt_cLinkId.ReadOnly = true;
            this.txt_cLinkId.Size = new System.Drawing.Size(121, 21);
            this.txt_cLinkId.TabIndex = 6;
            this.txt_cLinkId.Tag = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 28;
            this.label15.Text = "来源单号：";
            // 
            // txt_cChecker
            // 
            this.txt_cChecker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cChecker.Location = new System.Drawing.Point(309, 7);
            this.txt_cChecker.Name = "txt_cChecker";
            this.txt_cChecker.ReadOnly = true;
            this.txt_cChecker.Size = new System.Drawing.Size(112, 21);
            this.txt_cChecker.TabIndex = 1;
            this.txt_cChecker.Tag = "0";
            // 
            // lblChecker
            // 
            this.lblChecker.AutoSize = true;
            this.lblChecker.Location = new System.Drawing.Point(249, 11);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new System.Drawing.Size(65, 12);
            this.lblChecker.TabIndex = 24;
            this.lblChecker.Text = "审 核 人：";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "源 仓 库：";
            // 
            // txt_cBNo
            // 
            this.txt_cBNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cBNo.Location = new System.Drawing.Point(109, 7);
            this.txt_cBNo.Name = "txt_cBNo";
            this.txt_cBNo.ReadOnly = true;
            this.txt_cBNo.Size = new System.Drawing.Size(122, 21);
            this.txt_cBNo.TabIndex = 0;
            this.txt_cBNo.Tag = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "单    号：";
            // 
            // cmb_cPayer
            // 
            this.cmb_cPayer.FormattingEnabled = true;
            this.cmb_cPayer.Location = new System.Drawing.Point(490, 7);
            this.cmb_cPayer.Name = "cmb_cPayer";
            this.cmb_cPayer.Size = new System.Drawing.Size(121, 20);
            this.cmb_cPayer.TabIndex = 2;
            this.cmb_cPayer.Tag = "1";
            this.cmb_cPayer.Text = "Bind Text";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label30.Location = new System.Drawing.Point(423, 34);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(11, 12);
            this.label30.TabIndex = 53;
            this.label30.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label23.Location = new System.Drawing.Point(613, 34);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(11, 12);
            this.label23.TabIndex = 52;
            this.label23.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label20.Location = new System.Drawing.Point(234, 34);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 51;
            this.label20.Text = "*";
            // 
            // dtp_dCreateDate
            // 
            this.dtp_dCreateDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_dCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_dCreateDate.Location = new System.Drawing.Point(491, 30);
            this.dtp_dCreateDate.Name = "dtp_dCreateDate";
            this.dtp_dCreateDate.Size = new System.Drawing.Size(120, 21);
            this.dtp_dCreateDate.TabIndex = 7;
            this.dtp_dCreateDate.Tag = "2";
            // 
            // txtFindBillFrom
            // 
            this.txtFindBillFrom.Location = new System.Drawing.Point(74, 64);
            this.txtFindBillFrom.Name = "txtFindBillFrom";
            this.txtFindBillFrom.Size = new System.Drawing.Size(207, 21);
            this.txtFindBillFrom.TabIndex = 12;
            // 
            // lbl_Check
            // 
            this.lbl_Check.AutoSize = true;
            this.lbl_Check.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Check.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Check.ForeColor = System.Drawing.Color.Red;
            this.lbl_Check.Location = new System.Drawing.Point(442, 83);
            this.lbl_Check.Name = "lbl_Check";
            this.lbl_Check.Size = new System.Drawing.Size(44, 12);
            this.lbl_Check.TabIndex = 54;
            this.lbl_Check.Text = "已审核";
            this.lbl_Check.Visible = false;
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlEdit.Controls.Add(this.txt_cBNoIn);
            this.pnlEdit.Controls.Add(this.dtp_dCreateDate);
            this.pnlEdit.Controls.Add(this.cmb_cPayer);
            this.pnlEdit.Controls.Add(this.label31);
            this.pnlEdit.Controls.Add(this.label14);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.cmb_cMatClass);
            this.pnlEdit.Controls.Add(this.label38);
            this.pnlEdit.Controls.Add(this.txt_cBNoOut);
            this.pnlEdit.Controls.Add(this.label13);
            this.pnlEdit.Controls.Add(this.lbl_Check);
            this.pnlEdit.Controls.Add(this.label30);
            this.pnlEdit.Controls.Add(this.label23);
            this.pnlEdit.Controls.Add(this.label20);
            this.pnlEdit.Controls.Add(this.label22);
            this.pnlEdit.Controls.Add(this.cmb_cWHIdTo);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.cmb_nPStatus);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.txt_cRemark);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.cmb_cWHIdFrom);
            this.pnlEdit.Controls.Add(this.txt_cLinkId);
            this.pnlEdit.Controls.Add(this.label15);
            this.pnlEdit.Controls.Add(this.txt_cChecker);
            this.pnlEdit.Controls.Add(this.lblChecker);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.txt_cBNo);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(647, 172);
            this.pnlEdit.TabIndex = 0;
            // 
            // txt_cBNoIn
            // 
            this.txt_cBNoIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cBNoIn.Location = new System.Drawing.Point(491, 53);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.ReadOnly = true;
            this.txt_cBNoIn.Size = new System.Drawing.Size(120, 21);
            this.txt_cBNoIn.TabIndex = 57;
            this.txt_cBNoIn.Tag = "0";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(431, 57);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 12);
            this.label31.TabIndex = 58;
            this.label31.Text = "入库单号：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(431, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 45;
            this.label14.Text = "仓 管 员：";
            // 
            // cmb_cMatClass
            // 
            this.cmb_cMatClass.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_cMatClass.FormattingEnabled = true;
            this.cmb_cMatClass.Items.AddRange(new object[] {
            "轻度",
            "中度",
            "重度"});
            this.cmb_cMatClass.Location = new System.Drawing.Point(309, 79);
            this.cmb_cMatClass.Name = "cmb_cMatClass";
            this.cmb_cMatClass.Size = new System.Drawing.Size(112, 20);
            this.cmb_cMatClass.TabIndex = 59;
            this.cmb_cMatClass.Tag = "1";
            this.cmb_cMatClass.Text = "Bind SelectedValue";
            this.cmb_cMatClass.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(249, 83);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(65, 12);
            this.label38.TabIndex = 60;
            this.label38.Text = "物资种类：";
            this.label38.Visible = false;
            // 
            // txt_cBNoOut
            // 
            this.txt_cBNoOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cBNoOut.Location = new System.Drawing.Point(309, 53);
            this.txt_cBNoOut.Name = "txt_cBNoOut";
            this.txt_cBNoOut.ReadOnly = true;
            this.txt_cBNoOut.Size = new System.Drawing.Size(112, 21);
            this.txt_cBNoOut.TabIndex = 55;
            this.txt_cBNoOut.Tag = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(249, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 56;
            this.label13.Text = "出库单号：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label22.Location = new System.Drawing.Point(613, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 12);
            this.label22.TabIndex = 50;
            this.label22.Text = "*";
            // 
            // cmb_cWHIdTo
            // 
            this.cmb_cWHIdTo.FormattingEnabled = true;
            this.cmb_cWHIdTo.Location = new System.Drawing.Point(309, 30);
            this.cmb_cWHIdTo.Name = "cmb_cWHIdTo";
            this.cmb_cWHIdTo.Size = new System.Drawing.Size(112, 20);
            this.cmb_cWHIdTo.TabIndex = 5;
            this.cmb_cWHIdTo.Tag = "101";
            this.cmb_cWHIdTo.Text = "Bind Text";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "目标仓库：";
            // 
            // cmb_nPStatus
            // 
            this.cmb_nPStatus.BackColor = System.Drawing.SystemColors.Control;
            this.cmb_nPStatus.FormattingEnabled = true;
            this.cmb_nPStatus.Location = new System.Drawing.Point(109, 79);
            this.cmb_nPStatus.Name = "cmb_nPStatus";
            this.cmb_nPStatus.Size = new System.Drawing.Size(121, 20);
            this.cmb_nPStatus.TabIndex = 8;
            this.cmb_nPStatus.Tag = "101";
            this.cmb_nPStatus.Text = "Bind SelectedValue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 46;
            this.label9.Text = "单据状态：";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(16, 44);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 21;
            this.label29.Text = "操 作 员：";
            // 
            // cmbFindUser
            // 
            this.cmbFindUser.FormattingEnabled = true;
            this.cmbFindUser.Items.AddRange(new object[] {
            "平面库",
            "立体库"});
            this.cmbFindUser.Location = new System.Drawing.Point(74, 40);
            this.cmbFindUser.Name = "cmbFindUser";
            this.cmbFindUser.Size = new System.Drawing.Size(83, 20);
            this.cmbFindUser.TabIndex = 20;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // stbUser
            // 
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new System.Drawing.Size(47, 17);
            this.stbUser.Text = "用户名:";
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
            this.tlb_M_ErpImp,
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
            this.tlb_M_Check,
            this.tlb_M_UnCheck,
            this.toolStripSeparator7,
            this.tlb_M_BldBillIn,
            this.tlb_M_Item,
            this.toolStripSeparator8,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(947, 25);
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
            // tlb_M_BldBillIn
            // 
            this.tlb_M_BldBillIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_BldBillIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_BldBillIn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_BldBillIn.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_BldBillIn.Image")));
            this.tlb_M_BldBillIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_BldBillIn.Name = "tlb_M_BldBillIn";
            this.tlb_M_BldBillIn.Size = new System.Drawing.Size(74, 22);
            this.tlb_M_BldBillIn.Tag = "10";
            this.tlb_M_BldBillIn.Text = "生成入库单";
            this.tlb_M_BldBillIn.Click += new System.EventHandler(this.tlb_M_BldBillIn_Click);
            // 
            // tlb_M_Item
            // 
            this.tlb_M_Item.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Item.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Item.Image")));
            this.tlb_M_Item.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Item.Name = "tlb_M_Item";
            this.tlb_M_Item.Size = new System.Drawing.Size(33, 22);
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
            this.tlbSaveSysRts.Size = new System.Drawing.Size(81, 22);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new System.EventHandler(this.tlbSaveSysRts_Click);
            // 
            // tmrMain
            // 
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 5000;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFindUser);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.txtFindBillFrom);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbFindType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnUnFind);
            this.groupBox1.Controls.Add(this.cmbFindCheck);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.dtpFind_E);
            this.groupBox1.Controls.Add(this.dtpFind_B);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnQry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 116);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "来源单号：";
            // 
            // stbState
            // 
            this.stbState.Name = "stbState";
            this.stbState.Size = new System.Drawing.Size(35, 17);
            this.stbState.Text = "状态:";
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbModul,
            this.stbUser,
            this.stbState,
            this.stbDateTime});
            this.stbMain.Location = new System.Drawing.Point(0, 633);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(947, 22);
            this.stbMain.TabIndex = 28;
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
            // pnlSplit
            // 
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplit.Location = new System.Drawing.Point(0, 25);
            this.pnlSplit.Name = "pnlSplit";
            // 
            // pnlSplit.Panel1
            // 
            this.pnlSplit.Panel1.Controls.Add(this.grdList);
            this.pnlSplit.Panel1.Controls.Add(this.groupBox1);
            // 
            // pnlSplit.Panel2
            // 
            this.pnlSplit.Panel2.Controls.Add(this.pnlDtl);
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pnlSplit.Size = new System.Drawing.Size(947, 608);
            this.pnlSplit.SplitterDistance = 296;
            this.pnlSplit.TabIndex = 29;
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
            // 
            // frmBillRemove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(947, 655);
            this.Controls.Add(this.pnlSplit);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmBillRemove";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "调拨单管理";
            this.Load += new System.EventHandler(this.frmBillRemove_Load);
            this.pnlDtlEdit.ResumeLayout(false);
            this.pnlDtlEdit.PerformLayout();
            this.pnlDtl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDtl)).EndInit();
            this.ppmDtl.ResumeLayout(false);
            this.pnlBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDtl)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Dtl_cSpec;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Dtl_cMName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Dtl_cUnit;
        private System.Windows.Forms.Panel pnlDtlEdit;
        private System.Windows.Forms.TextBox txt_Dtl_fQty;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_Dtl_cMNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbFindType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUnFind;
        private System.Windows.Forms.ComboBox cmbFindCheck;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker dtpFind_E;
        private System.Windows.Forms.DateTimePicker dtpFind_B;
        private System.Windows.Forms.Panel pnlDtl;
        public System.Windows.Forms.DataGridView grdDtl;
        private System.Windows.Forms.ContextMenuStrip ppmDtl;
        private System.Windows.Forms.ToolStripMenuItem mi_Dtl_PrintBarCode;
        private System.Windows.Forms.Panel pnlBtns;
        private System.Windows.Forms.Button btn_Dtl_Delete;
        private System.Windows.Forms.Button btn_Dtl_Edit;
        private System.Windows.Forms.Button btn_Dtl_New;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnQry;
        public System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.BindingSource bdsMain;
        private System.Windows.Forms.BindingSource bdsDtl;
        private System.Windows.Forms.TextBox txt_cRemark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_cWHIdFrom;
        private System.Windows.Forms.TextBox txt_cLinkId;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_cChecker;
        private System.Windows.Forms.Label lblChecker;
        public System.Windows.Forms.ToolStripButton tlb_M_Refresh;
        public System.Windows.Forms.ToolStripButton tlb_M_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton tlb_M_Find;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_cBNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_cPayer;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtp_dCreateDate;
        private System.Windows.Forms.TextBox txtFindBillFrom;
        private System.Windows.Forms.Label lbl_Check;
        public System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmb_cWHIdTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_nPStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbFindUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripButton tlb_M_New;
        public System.Windows.Forms.ToolStripButton tlb_M_Edit;
        public System.Windows.Forms.ToolStripStatusLabel stbUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStrip tlbMain;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton tlb_M_Undo;
        public System.Windows.Forms.ToolStripButton tlb_M_Delete;
        public System.Windows.Forms.ToolStripButton tlb_M_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        public System.Windows.Forms.ToolStripButton tlb_M_Check;
        private System.Windows.Forms.ToolStripButton tlb_M_UnCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tlb_M_Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btn_M_Help;
        private System.Windows.Forms.ToolStripButton tlb_M_Exit;
        private System.Windows.Forms.ToolStripButton tlbSaveSysRts;
        public System.Windows.Forms.Timer tmrMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ToolStripStatusLabel stbState;
        public System.Windows.Forms.StatusStrip stbMain;
        public System.Windows.Forms.ToolStripStatusLabel stbModul;
        public System.Windows.Forms.ToolStripStatusLabel stbDateTime;
        public System.Windows.Forms.SplitContainer pnlSplit;
        private System.Windows.Forms.TextBox txt_cBNoIn;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_cBNoOut;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBId;
        private System.Windows.Forms.DataGridViewComboBoxColumn colcWHIdFrom;
        private System.Windows.Forms.DataGridViewComboBoxColumn colcWHIdTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcUnit;
        private System.Windows.Forms.ToolStripButton tlb_M_BldBillIn;
        private System.Windows.Forms.ComboBox cmb_cMatClass;
        private System.Windows.Forms.Label label38;
        public System.Windows.Forms.ToolStripButton tlb_M_ErpImp;
    }
}
