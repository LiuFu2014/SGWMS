namespace UserMS
{
    partial class frmCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompany));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_cLinkId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.txt_cTel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_cUrl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_cEAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_cCAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_cEName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cCName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_cCmptName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cComptId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQry = new System.Windows.Forms.Button();
            this.txtFindName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeftTop = new System.Windows.Forms.Panel();
            this.bdsMain = new System.Windows.Forms.BindingSource(this.components);
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
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
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colcComptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcCmptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.pnlSplit = new System.Windows.Forms.SplitContainer();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlEdit.SuspendLayout();
            this.pnlLeftTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).BeginInit();
            this.tlbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.stbMain.SuspendLayout();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_cLinkId
            // 
            this.txt_cLinkId.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_cLinkId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cLinkId.Location = new System.Drawing.Point(122, 249);
            this.txt_cLinkId.Name = "txt_cLinkId";
            this.txt_cLinkId.ReadOnly = true;
            this.txt_cLinkId.Size = new System.Drawing.Size(144, 21);
            this.txt_cLinkId.TabIndex = 9;
            this.txt_cLinkId.Tag = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 251);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "关联编码：";
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.SystemColors.Info;
            this.pnlEdit.Controls.Add(this.label10);
            this.pnlEdit.Controls.Add(this.label18);
            this.pnlEdit.Controls.Add(this.txt_cLinkId);
            this.pnlEdit.Controls.Add(this.label11);
            this.pnlEdit.Controls.Add(this.txt_cTel);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.txt_cUrl);
            this.pnlEdit.Controls.Add(this.label8);
            this.pnlEdit.Controls.Add(this.txt_cEAddress);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.txt_cCAddress);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.txt_cEName);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.txt_cCName);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.txt_cCmptName);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.txt_cComptId);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Location = new System.Drawing.Point(32, 61);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(540, 294);
            this.pnlEdit.TabIndex = 0;
            // 
            // txt_cTel
            // 
            this.txt_cTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cTel.Location = new System.Drawing.Point(122, 222);
            this.txt_cTel.Name = "txt_cTel";
            this.txt_cTel.ReadOnly = true;
            this.txt_cTel.Size = new System.Drawing.Size(376, 21);
            this.txt_cTel.TabIndex = 8;
            this.txt_cTel.Tag = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "单位联系电话：";
            // 
            // txt_cUrl
            // 
            this.txt_cUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cUrl.Location = new System.Drawing.Point(122, 192);
            this.txt_cUrl.Name = "txt_cUrl";
            this.txt_cUrl.ReadOnly = true;
            this.txt_cUrl.Size = new System.Drawing.Size(376, 21);
            this.txt_cUrl.TabIndex = 7;
            this.txt_cUrl.Tag = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "单位网站：";
            // 
            // txt_cEAddress
            // 
            this.txt_cEAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cEAddress.Location = new System.Drawing.Point(122, 162);
            this.txt_cEAddress.Name = "txt_cEAddress";
            this.txt_cEAddress.ReadOnly = true;
            this.txt_cEAddress.Size = new System.Drawing.Size(376, 21);
            this.txt_cEAddress.TabIndex = 6;
            this.txt_cEAddress.Tag = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "单位英文地址：";
            // 
            // txt_cCAddress
            // 
            this.txt_cCAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCAddress.Location = new System.Drawing.Point(122, 132);
            this.txt_cCAddress.Name = "txt_cCAddress";
            this.txt_cCAddress.ReadOnly = true;
            this.txt_cCAddress.Size = new System.Drawing.Size(376, 21);
            this.txt_cCAddress.TabIndex = 5;
            this.txt_cCAddress.Tag = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "单位中文地址：";
            // 
            // txt_cEName
            // 
            this.txt_cEName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cEName.Location = new System.Drawing.Point(98, 102);
            this.txt_cEName.Name = "txt_cEName";
            this.txt_cEName.ReadOnly = true;
            this.txt_cEName.Size = new System.Drawing.Size(294, 21);
            this.txt_cEName.TabIndex = 4;
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
            this.txt_cCName.TabIndex = 3;
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
            // txt_cCmptName
            // 
            this.txt_cCmptName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cCmptName.Location = new System.Drawing.Point(98, 41);
            this.txt_cCmptName.Name = "txt_cCmptName";
            this.txt_cCmptName.ReadOnly = true;
            this.txt_cCmptName.Size = new System.Drawing.Size(294, 21);
            this.txt_cCmptName.TabIndex = 2;
            this.txt_cCmptName.Tag = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "单位简称：";
            // 
            // txt_cComptId
            // 
            this.txt_cComptId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cComptId.Location = new System.Drawing.Point(98, 14);
            this.txt_cComptId.Name = "txt_cComptId";
            this.txt_cComptId.ReadOnly = true;
            this.txt_cComptId.Size = new System.Drawing.Size(144, 21);
            this.txt_cComptId.TabIndex = 1;
            this.txt_cComptId.Tag = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "单位编号：";
            // 
            // btnQry
            // 
            this.btnQry.Location = new System.Drawing.Point(171, 4);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new System.Drawing.Size(39, 23);
            this.btnQry.TabIndex = 2;
            this.btnQry.Text = "查询";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new System.EventHandler(this.btnQry_Click);
            // 
            // txtFindName
            // 
            this.txtFindName.Location = new System.Drawing.Point(54, 4);
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new System.Drawing.Size(116, 21);
            this.txtFindName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "单位名：";
            // 
            // pnlLeftTop
            // 
            this.pnlLeftTop.Controls.Add(this.btnQry);
            this.pnlLeftTop.Controls.Add(this.txtFindName);
            this.pnlLeftTop.Controls.Add(this.label1);
            this.pnlLeftTop.Location = new System.Drawing.Point(3, 39);
            this.pnlLeftTop.Name = "pnlLeftTop";
            this.pnlLeftTop.Size = new System.Drawing.Size(213, 38);
            this.pnlLeftTop.TabIndex = 0;
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
            this.toolStripSeparator7,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.toolStripSeparator8,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(920, 25);
            this.tlbMain.TabIndex = 13;
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
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcComptId,
            this.colcCmptName,
            this.colcAddress});
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new System.Drawing.Point(3, 83);
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
            this.grdList.Size = new System.Drawing.Size(224, 367);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            // 
            // colcComptId
            // 
            this.colcComptId.DataPropertyName = "cComptId";
            this.colcComptId.Frozen = true;
            this.colcComptId.HeaderText = "单位编码";
            this.colcComptId.Name = "colcComptId";
            this.colcComptId.ReadOnly = true;
            this.colcComptId.ToolTipText = "单位编码";
            // 
            // colcCmptName
            // 
            this.colcCmptName.DataPropertyName = "cCmptName";
            this.colcCmptName.HeaderText = "单位名称";
            this.colcCmptName.Name = "colcCmptName";
            this.colcCmptName.ReadOnly = true;
            this.colcCmptName.ToolTipText = "单位名称";
            // 
            // colcAddress
            // 
            this.colcAddress.DataPropertyName = "cAddress";
            this.colcAddress.HeaderText = "单位地址";
            this.colcAddress.Name = "colcAddress";
            this.colcAddress.ReadOnly = true;
            this.colcAddress.ToolTipText = "单位地址";
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbModul,
            this.stbUser,
            this.stbState,
            this.stbDateTime});
            this.stbMain.Location = new System.Drawing.Point(0, 451);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(920, 22);
            this.stbMain.TabIndex = 12;
            this.stbMain.Text = "statusStrip1";
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
            this.pnlSplit.Size = new System.Drawing.Size(920, 473);
            this.pnlSplit.SplitterDistance = 230;
            this.pnlSplit.TabIndex = 14;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(248, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(11, 12);
            this.label18.TabIndex = 46;
            this.label18.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(398, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 47;
            this.label10.Text = "*";
            // 
            // frmCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 473);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.pnlSplit);
            this.KeyPreview = true;
            this.Name = "frmCompany";
            this.Text = "单位管理";
            this.Load += new System.EventHandler(this.frmCompany_Load);
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.pnlLeftTop.ResumeLayout(false);
            this.pnlLeftTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).EndInit();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_cLinkId;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.TextBox txt_cTel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_cUrl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_cEAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_cCAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_cEName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cCName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_cCmptName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cComptId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQry;
        private System.Windows.Forms.TextBox txtFindName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlLeftTop;
        private System.Windows.Forms.BindingSource bdsMain;
        private System.Windows.Forms.ToolStripButton btn_M_Help;
        public System.Windows.Forms.ToolStripButton tlb_M_Edit;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStrip tlbMain;
        public System.Windows.Forms.ToolStripButton tlb_M_New;
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
        private System.Windows.Forms.ToolStripButton tlb_M_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tlbSaveSysRts;
        public System.Windows.Forms.ToolStripStatusLabel stbModul;
        public System.Windows.Forms.ToolStripStatusLabel stbDateTime;
        public System.Windows.Forms.ToolStripStatusLabel stbUser;
        public System.Windows.Forms.ToolStripStatusLabel stbState;
        public System.Windows.Forms.Timer tmrMain;
        public System.Windows.Forms.DataGridView grdList;
        public System.Windows.Forms.StatusStrip stbMain;
        public System.Windows.Forms.SplitContainer pnlSplit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcComptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcCmptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
    }
}