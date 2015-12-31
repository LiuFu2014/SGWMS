namespace UserMS
{
    partial class frmDept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDept));
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colcDeptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlLeftTop = new System.Windows.Forms.Panel();
            this.cmb_Compt = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQry = new System.Windows.Forms.Button();
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.txt_cEDesc = new System.Windows.Forms.TextBox();
            this.txt_cCDesc = new System.Windows.Forms.TextBox();
            this.txt_cLinkId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_cCmptId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_cTel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_cEName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cCName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_cName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cDeptId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.bdsMain = new System.Windows.Forms.BindingSource(this.components);
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlLeftTop.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).BeginInit();
            this.stbMain.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlSplit.Panel1.Controls.Add(this.pnlLeftTop);
            // 
            // pnlSplit.Panel2
            // 
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pnlSplit.Size = new System.Drawing.Size(935, 530);
            this.pnlSplit.SplitterDistance = 237;
            this.pnlSplit.TabIndex = 15;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcDeptId,
            this.colcName,
            this.colcDesc});
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new System.Drawing.Point(3, 83);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 23;
            this.grdList.Size = new System.Drawing.Size(230, 398);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            // 
            // colcDeptId
            // 
            this.colcDeptId.DataPropertyName = "cDeptId";
            this.colcDeptId.Frozen = true;
            this.colcDeptId.HeaderText = "部门编码";
            this.colcDeptId.Name = "colcDeptId";
            this.colcDeptId.ReadOnly = true;
            this.colcDeptId.ToolTipText = "部门编码";
            // 
            // colcName
            // 
            this.colcName.DataPropertyName = "cName";
            this.colcName.HeaderText = "部门名称";
            this.colcName.Name = "colcName";
            this.colcName.ReadOnly = true;
            this.colcName.ToolTipText = "部门名称";
            // 
            // colcDesc
            // 
            this.colcDesc.DataPropertyName = "cDesc";
            this.colcDesc.HeaderText = "部门描述";
            this.colcDesc.Name = "colcDesc";
            this.colcDesc.ReadOnly = true;
            this.colcDesc.ToolTipText = "部门描述";
            // 
            // pnlLeftTop
            // 
            this.pnlLeftTop.Controls.Add(this.cmb_Compt);
            this.pnlLeftTop.Controls.Add(this.label8);
            this.pnlLeftTop.Controls.Add(this.btnQry);
            this.pnlLeftTop.Controls.Add(this.txtFindName);
            this.pnlLeftTop.Controls.Add(this.label1);
            this.pnlLeftTop.Location = new System.Drawing.Point(3, 28);
            this.pnlLeftTop.Name = "pnlLeftTop";
            this.pnlLeftTop.Size = new System.Drawing.Size(232, 51);
            this.pnlLeftTop.TabIndex = 0;
            // 
            // cmb_Compt
            // 
            this.cmb_Compt.CausesValidation = false;
            this.cmb_Compt.FormattingEnabled = true;
            this.cmb_Compt.Items.AddRange(new object[] {
            "普通用户",
            "管理员",
            "超级管理员"});
            this.cmb_Compt.Location = new System.Drawing.Point(38, 3);
            this.cmb_Compt.Name = "cmb_Compt";
            this.cmb_Compt.Size = new System.Drawing.Size(191, 20);
            this.cmb_Compt.TabIndex = 26;
            this.cmb_Compt.Tag = "";
            this.cmb_Compt.Text = "Bind SelectedIndex";
            this.cmb_Compt.SelectedValueChanged += new System.EventHandler(this.cmb_Compt_SelectedValueChanged);
            this.cmb_Compt.TextChanged += new System.EventHandler(this.cmb_Compt_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "单位：";
            // 
            // btnQry
            // 
            this.btnQry.Location = new System.Drawing.Point(171, 27);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new System.Drawing.Size(39, 23);
            this.btnQry.TabIndex = 2;
            this.btnQry.Text = "查询";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new System.EventHandler(this.btnQry_Click);
            // 
            // txtFindName
            // 
            this.txtFindName.Location = new System.Drawing.Point(54, 27);
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new System.Drawing.Size(116, 21);
            this.txtFindName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "部门名：";
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlEdit.Controls.Add(this.label14);
            this.pnlEdit.Controls.Add(this.label13);
            this.pnlEdit.Controls.Add(this.label12);
            this.pnlEdit.Controls.Add(this.txt_cEDesc);
            this.pnlEdit.Controls.Add(this.txt_cCDesc);
            this.pnlEdit.Controls.Add(this.txt_cLinkId);
            this.pnlEdit.Controls.Add(this.label11);
            this.pnlEdit.Controls.Add(this.txt_cCmptId);
            this.pnlEdit.Controls.Add(this.label10);
            this.pnlEdit.Controls.Add(this.txt_cTel);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.txt_cEName);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.txt_cCName);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.txt_cName);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.txt_cDeptId);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Location = new System.Drawing.Point(20, 83);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(554, 358);
            this.pnlEdit.TabIndex = 0;
            // 
            // txt_cEDesc
            // 
            this.txt_cEDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cEDesc.Location = new System.Drawing.Point(98, 211);
            this.txt_cEDesc.Multiline = true;
            this.txt_cEDesc.Name = "txt_cEDesc";
            this.txt_cEDesc.ReadOnly = true;
            this.txt_cEDesc.Size = new System.Drawing.Size(400, 63);
            this.txt_cEDesc.TabIndex = 5;
            this.txt_cEDesc.Tag = "0";
            // 
            // txt_cCDesc
            // 
            this.txt_cCDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCDesc.Location = new System.Drawing.Point(98, 132);
            this.txt_cCDesc.Multiline = true;
            this.txt_cCDesc.Name = "txt_cCDesc";
            this.txt_cCDesc.ReadOnly = true;
            this.txt_cCDesc.Size = new System.Drawing.Size(400, 63);
            this.txt_cCDesc.TabIndex = 4;
            this.txt_cCDesc.Tag = "0";
            // 
            // txt_cLinkId
            // 
            this.txt_cLinkId.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_cLinkId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cLinkId.Location = new System.Drawing.Point(354, 322);
            this.txt_cLinkId.Name = "txt_cLinkId";
            this.txt_cLinkId.ReadOnly = true;
            this.txt_cLinkId.Size = new System.Drawing.Size(144, 21);
            this.txt_cLinkId.TabIndex = 8;
            this.txt_cLinkId.Tag = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(293, 322);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "关联编码：";
            // 
            // txt_cCmptId
            // 
            this.txt_cCmptId.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_cCmptId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCmptId.Location = new System.Drawing.Point(98, 322);
            this.txt_cCmptId.Name = "txt_cCmptId";
            this.txt_cCmptId.ReadOnly = true;
            this.txt_cCmptId.Size = new System.Drawing.Size(144, 21);
            this.txt_cCmptId.TabIndex = 7;
            this.txt_cCmptId.Tag = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 322);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "单位编码：";
            // 
            // txt_cTel
            // 
            this.txt_cTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cTel.Location = new System.Drawing.Point(98, 288);
            this.txt_cTel.Name = "txt_cTel";
            this.txt_cTel.ReadOnly = true;
            this.txt_cTel.Size = new System.Drawing.Size(400, 21);
            this.txt_cTel.TabIndex = 6;
            this.txt_cTel.Tag = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "部门电话：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "英文描述：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "中文描述：";
            // 
            // txt_cEName
            // 
            this.txt_cEName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cEName.Location = new System.Drawing.Point(98, 102);
            this.txt_cEName.Name = "txt_cEName";
            this.txt_cEName.ReadOnly = true;
            this.txt_cEName.Size = new System.Drawing.Size(294, 21);
            this.txt_cEName.TabIndex = 3;
            this.txt_cEName.Tag = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "英文名：";
            // 
            // txt_cCName
            // 
            this.txt_cCName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCName.Location = new System.Drawing.Point(98, 71);
            this.txt_cCName.Name = "txt_cCName";
            this.txt_cCName.ReadOnly = true;
            this.txt_cCName.Size = new System.Drawing.Size(294, 21);
            this.txt_cCName.TabIndex = 2;
            this.txt_cCName.Tag = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "中文名：";
            // 
            // txt_cName
            // 
            this.txt_cName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cName.Location = new System.Drawing.Point(98, 41);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.ReadOnly = true;
            this.txt_cName.Size = new System.Drawing.Size(294, 21);
            this.txt_cName.TabIndex = 1;
            this.txt_cName.Tag = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "部门名称：";
            // 
            // txt_cDeptId
            // 
            this.txt_cDeptId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cDeptId.Location = new System.Drawing.Point(98, 12);
            this.txt_cDeptId.Name = "txt_cDeptId";
            this.txt_cDeptId.ReadOnly = true;
            this.txt_cDeptId.Size = new System.Drawing.Size(144, 21);
            this.txt_cDeptId.TabIndex = 0;
            this.txt_cDeptId.Tag = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "部门编码：";
            // 
            // stbModul
            // 
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new System.Drawing.Size(35, 17);
            this.stbModul.Text = "模块:";
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
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel8});
            this.stbMain.Location = new System.Drawing.Point(0, 508);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(935, 22);
            this.stbMain.TabIndex = 17;
            this.stbMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel5.Text = "模块:";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabel6.Text = "用户名:";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel7.Text = "状态:";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel8.Text = "时间:";
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
            this.toolStripSeparator7,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.toolStripSeparator8,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(935, 25);
            this.tlbMain.TabIndex = 18;
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(248, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 49;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(398, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 50;
            this.label13.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(248, 324);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 51;
            this.label14.Text = "*";
            // 
            // frmDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(935, 530);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.pnlSplit);
            this.KeyPreview = true;
            this.Name = "frmDept";
            this.Text = "部门信息管理";
            this.Load += new System.EventHandler(this.frmDept_Load);
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlLeftTop.ResumeLayout(false);
            this.pnlLeftTop.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).EndInit();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.SplitContainer pnlSplit;
        public System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcDeptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcDesc;
        private System.Windows.Forms.Panel pnlLeftTop;
        private System.Windows.Forms.Button btnQry;
        private System.Windows.Forms.TextBox txtFindName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.TextBox txt_cEDesc;
        private System.Windows.Forms.TextBox txt_cCDesc;
        private System.Windows.Forms.TextBox txt_cLinkId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_cCmptId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_cTel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_cEName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cCName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_cName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cDeptId;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ToolStripStatusLabel stbModul;
        public System.Windows.Forms.ToolStripStatusLabel stbUser;
        public System.Windows.Forms.ToolStripStatusLabel stbState;
        public System.Windows.Forms.ToolStripStatusLabel stbDateTime;
        private System.Windows.Forms.BindingSource bdsMain;
        public System.Windows.Forms.Timer tmrMain;
        public System.Windows.Forms.StatusStrip stbMain;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        public System.Windows.Forms.ToolStrip tlbMain;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton tlb_M_New;
        public System.Windows.Forms.ToolStripButton tlb_M_Edit;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btn_M_Help;
        private System.Windows.Forms.ToolStripButton tlb_M_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tlbSaveSysRts;
        private System.Windows.Forms.ComboBox cmb_Compt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;


    }
}
