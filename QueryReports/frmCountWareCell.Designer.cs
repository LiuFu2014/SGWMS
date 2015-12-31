namespace SunEast.App
{
    partial class frmCountWareCell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCountWareCell));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpCellCount = new System.Windows.Forms.TabPage();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.grdCol_cWType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCol_cWHId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCol_cWName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCol_cAreaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCol_cPalletSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCol_cStatusStore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCol_nCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpUsedRate = new System.Windows.Forms.TabPage();
            this.lbl_UsedRate = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tlb_M_Find = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Layer_To = new System.Windows.Forms.TextBox();
            this.txt_Layer_From = new System.Windows.Forms.TextBox();
            this.txt_Col_To = new System.Windows.Forms.TextBox();
            this.txt_Col_From = new System.Windows.Forms.TextBox();
            this.txt_Row_To = new System.Windows.Forms.TextBox();
            this.txt_Row_From = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Area = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Delete = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Undo = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Save = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.cmbWHId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tbpCellCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.tbpUsedRate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbcMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 345);
            this.panel2.TabIndex = 3;
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpCellCount);
            this.tbcMain.Controls.Add(this.tbpUsedRate);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 0);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(777, 345);
            this.tbcMain.TabIndex = 0;
            this.tbcMain.SelectedIndexChanged += new System.EventHandler(this.tbcMain_SelectedIndexChanged);
            // 
            // tbpCellCount
            // 
            this.tbpCellCount.Controls.Add(this.grdData);
            this.tbpCellCount.Location = new System.Drawing.Point(4, 22);
            this.tbpCellCount.Name = "tbpCellCount";
            this.tbpCellCount.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCellCount.Size = new System.Drawing.Size(769, 319);
            this.tbpCellCount.TabIndex = 0;
            this.tbpCellCount.Tag = "0";
            this.tbpCellCount.Text = "货位统计";
            this.tbpCellCount.UseVisualStyleBackColor = true;
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdCol_cWType,
            this.grdCol_cWHId,
            this.grdCol_cWName,
            this.grdCol_cAreaName,
            this.grdCol_cPalletSpec,
            this.grdCol_cStatusStore,
            this.grdCol_nCount});
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(3, 3);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(763, 313);
            this.grdData.TabIndex = 0;
            // 
            // grdCol_cWType
            // 
            this.grdCol_cWType.DataPropertyName = "cWType";
            this.grdCol_cWType.HeaderText = "仓库类型";
            this.grdCol_cWType.Name = "grdCol_cWType";
            this.grdCol_cWType.ReadOnly = true;
            this.grdCol_cWType.Width = 80;
            // 
            // grdCol_cWHId
            // 
            this.grdCol_cWHId.DataPropertyName = "cWHId";
            this.grdCol_cWHId.HeaderText = "仓库号";
            this.grdCol_cWHId.Name = "grdCol_cWHId";
            this.grdCol_cWHId.ReadOnly = true;
            this.grdCol_cWHId.Width = 65;
            // 
            // grdCol_cWName
            // 
            this.grdCol_cWName.DataPropertyName = "cWName";
            this.grdCol_cWName.HeaderText = "仓库名";
            this.grdCol_cWName.Name = "grdCol_cWName";
            this.grdCol_cWName.ReadOnly = true;
            // 
            // grdCol_cAreaName
            // 
            this.grdCol_cAreaName.DataPropertyName = "cAreaName";
            this.grdCol_cAreaName.HeaderText = "货区名";
            this.grdCol_cAreaName.Name = "grdCol_cAreaName";
            this.grdCol_cAreaName.ReadOnly = true;
            this.grdCol_cAreaName.Width = 70;
            // 
            // grdCol_cPalletSpec
            // 
            this.grdCol_cPalletSpec.DataPropertyName = "cPalletSpec";
            this.grdCol_cPalletSpec.HeaderText = "托盘规格";
            this.grdCol_cPalletSpec.Name = "grdCol_cPalletSpec";
            this.grdCol_cPalletSpec.ReadOnly = true;
            // 
            // grdCol_cStatusStore
            // 
            this.grdCol_cStatusStore.DataPropertyName = "cStatusStore";
            this.grdCol_cStatusStore.HeaderText = "存货状态";
            this.grdCol_cStatusStore.Name = "grdCol_cStatusStore";
            this.grdCol_cStatusStore.ReadOnly = true;
            // 
            // grdCol_nCount
            // 
            this.grdCol_nCount.DataPropertyName = "nCount";
            this.grdCol_nCount.HeaderText = "货位数";
            this.grdCol_nCount.Name = "grdCol_nCount";
            this.grdCol_nCount.ReadOnly = true;
            // 
            // tbpUsedRate
            // 
            this.tbpUsedRate.Controls.Add(this.lbl_UsedRate);
            this.tbpUsedRate.Controls.Add(this.label9);
            this.tbpUsedRate.Location = new System.Drawing.Point(4, 22);
            this.tbpUsedRate.Name = "tbpUsedRate";
            this.tbpUsedRate.Padding = new System.Windows.Forms.Padding(3);
            this.tbpUsedRate.Size = new System.Drawing.Size(769, 319);
            this.tbpUsedRate.TabIndex = 1;
            this.tbpUsedRate.Tag = "1";
            this.tbpUsedRate.Text = "货位使用率";
            this.tbpUsedRate.UseVisualStyleBackColor = true;
            // 
            // lbl_UsedRate
            // 
            this.lbl_UsedRate.AutoSize = true;
            this.lbl_UsedRate.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.lbl_UsedRate.Location = new System.Drawing.Point(182, 79);
            this.lbl_UsedRate.Name = "lbl_UsedRate";
            this.lbl_UsedRate.Size = new System.Drawing.Size(57, 27);
            this.lbl_UsedRate.TabIndex = 1;
            this.lbl_UsedRate.Text = "0 %";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(54, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "货位使用率：";
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
            this.tlb_M_Find.Text = "统计";
            this.tlb_M_Find.ToolTipText = "统计";
            this.tlb_M_Find.Click += new System.EventHandler(this.tlb_M_Find_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.btn_M_Help.Text = "导出";
            this.btn_M_Help.Click += new System.EventHandler(this.btn_M_Help_Click);
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
            this.tlb_M_Print.Click += new System.EventHandler(this.tlb_M_Print_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
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
            this.tlb_M_Refresh.Click += new System.EventHandler(this.tlb_M_Find_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_Layer_To);
            this.panel1.Controls.Add(this.txt_Layer_From);
            this.panel1.Controls.Add(this.txt_Col_To);
            this.panel1.Controls.Add(this.txt_Col_From);
            this.panel1.Controls.Add(this.txt_Row_To);
            this.panel1.Controls.Add(this.txt_Row_From);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmb_Area);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tlbMain);
            this.panel1.Controls.Add(this.cmbWHId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 144);
            this.panel1.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(144, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 1);
            this.label8.TabIndex = 29;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(144, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 1);
            this.label7.TabIndex = 28;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(144, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 1);
            this.label6.TabIndex = 27;
            this.label6.Text = "label6";
            // 
            // txt_Layer_To
            // 
            this.txt_Layer_To.Location = new System.Drawing.Point(188, 111);
            this.txt_Layer_To.Name = "txt_Layer_To";
            this.txt_Layer_To.Size = new System.Drawing.Size(85, 21);
            this.txt_Layer_To.TabIndex = 26;
            // 
            // txt_Layer_From
            // 
            this.txt_Layer_From.Location = new System.Drawing.Point(58, 111);
            this.txt_Layer_From.Name = "txt_Layer_From";
            this.txt_Layer_From.Size = new System.Drawing.Size(78, 21);
            this.txt_Layer_From.TabIndex = 25;
            // 
            // txt_Col_To
            // 
            this.txt_Col_To.Location = new System.Drawing.Point(188, 82);
            this.txt_Col_To.Name = "txt_Col_To";
            this.txt_Col_To.Size = new System.Drawing.Size(85, 21);
            this.txt_Col_To.TabIndex = 24;
            // 
            // txt_Col_From
            // 
            this.txt_Col_From.Location = new System.Drawing.Point(58, 82);
            this.txt_Col_From.Name = "txt_Col_From";
            this.txt_Col_From.Size = new System.Drawing.Size(78, 21);
            this.txt_Col_From.TabIndex = 23;
            // 
            // txt_Row_To
            // 
            this.txt_Row_To.Location = new System.Drawing.Point(188, 55);
            this.txt_Row_To.Name = "txt_Row_To";
            this.txt_Row_To.Size = new System.Drawing.Size(85, 21);
            this.txt_Row_To.TabIndex = 22;
            // 
            // txt_Row_From
            // 
            this.txt_Row_From.Location = new System.Drawing.Point(58, 55);
            this.txt_Row_From.Name = "txt_Row_From";
            this.txt_Row_From.Size = new System.Drawing.Size(78, 21);
            this.txt_Row_From.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "层：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "列：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "行：";
            // 
            // cmb_Area
            // 
            this.cmb_Area.FormattingEnabled = true;
            this.cmb_Area.Location = new System.Drawing.Point(326, 31);
            this.cmb_Area.Name = "cmb_Area";
            this.cmb_Area.Size = new System.Drawing.Size(158, 20);
            this.cmb_Area.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "货区：";
            // 
            // tlbMain
            // 
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.tlb_M_New,
            this.tlb_M_Find,
            this.tlb_M_Edit,
            this.tlb_M_Delete,
            this.tlb_M_Undo,
            this.tlb_M_Save,
            this.tlb_M_Refresh,
            this.toolStripSeparator4,
            this.toolStripSeparator5,
            this.toolStripSeparator1,
            this.toolStripSeparator3,
            this.tlb_M_Print,
            this.toolStripSeparator6,
            this.toolStripSeparator7,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.toolStripSeparator8,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(777, 25);
            this.tlbMain.TabIndex = 15;
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
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Visible = false;
            // 
            // tlb_M_Edit
            // 
            this.tlb_M_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Edit.Image")));
            this.tlb_M_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_Edit.Text = "手动过账";
            this.tlb_M_Edit.Visible = false;
            // 
            // tlb_M_Delete
            // 
            this.tlb_M_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Delete.Image")));
            this.tlb_M_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_Delete.Text = "删除指令";
            this.tlb_M_Delete.Visible = false;
            // 
            // tlb_M_Undo
            // 
            this.tlb_M_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Undo.Image")));
            this.tlb_M_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new System.Drawing.Size(87, 22);
            this.tlb_M_Undo.Text = "取消未下指令";
            this.tlb_M_Undo.Visible = false;
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
            this.tlb_M_Save.Text = "保存";
            this.tlb_M_Save.Visible = false;
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
            this.tlbSaveSysRts.Size = new System.Drawing.Size(84, 22);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            // 
            // cmbWHId
            // 
            this.cmbWHId.FormattingEnabled = true;
            this.cmbWHId.Location = new System.Drawing.Point(58, 31);
            this.cmbWHId.Name = "cmbWHId";
            this.cmbWHId.Size = new System.Drawing.Size(215, 20);
            this.cmbWHId.TabIndex = 1;
            this.cmbWHId.SelectedIndexChanged += new System.EventHandler(this.cmbWHId_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库：";
            // 
            // frmCountWareCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(777, 489);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "frmCountWareCell";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "统计货位";
            this.Load += new System.EventHandler(this.frmCountWareCell_Load);
            this.panel2.ResumeLayout(false);
            this.tbcMain.ResumeLayout(false);
            this.tbpCellCount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.tbpUsedRate.ResumeLayout(false);
            this.tbpUsedRate.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ToolStripButton tlb_M_Find;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btn_M_Help;
        public System.Windows.Forms.ToolStripButton tlb_M_Print;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        public System.Windows.Forms.ToolStripButton tlb_M_Refresh;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ToolStrip tlbMain;
        public System.Windows.Forms.ToolStripButton tlb_M_New;
        public System.Windows.Forms.ToolStripButton tlb_M_Edit;
        public System.Windows.Forms.ToolStripButton tlb_M_Delete;
        public System.Windows.Forms.ToolStripButton tlb_M_Undo;
        public System.Windows.Forms.ToolStripButton tlb_M_Save;
        private System.Windows.Forms.ToolStripButton tlb_M_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tlbSaveSysRts;
        private System.Windows.Forms.ComboBox cmbWHId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpCellCount;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.TabPage tbpUsedRate;
        private System.Windows.Forms.ComboBox cmb_Area;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Col_To;
        private System.Windows.Forms.TextBox txt_Col_From;
        private System.Windows.Forms.TextBox txt_Row_To;
        private System.Windows.Forms.TextBox txt_Row_From;
        private System.Windows.Forms.TextBox txt_Layer_To;
        private System.Windows.Forms.TextBox txt_Layer_From;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_cWType;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_cWHId;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_cWName;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_cAreaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_cPalletSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_cStatusStore;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCol_nCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_UsedRate;
    }
}
