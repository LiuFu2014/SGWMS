namespace WareStoreMS
{
    partial class FrmStockMAjust
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockMAjust));
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
            this.toolStripButton_Audit = new System.Windows.Forms.ToolStripButton();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView_Main = new System.Windows.Forms.DataGridView();
            this.cBNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nBClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cWHId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cChecker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dCheckDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLinkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBNoFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Qry = new System.Windows.Forms.Button();
            this.dtp_From = new System.Windows.Forms.DateTimePicker();
            this.cmbQ_Status = new System.Windows.Forms.ComboBox();
            this.dtp_To = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQ_BNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbQ_WHId = new System.Windows.Forms.ComboBox();
            this.panel_Edit = new System.Windows.Forms.Panel();
            this.dtp_dDate = new System.Windows.Forms.DateTimePicker();
            this.textBox = new System.Windows.Forms.TextBox();
            this.textBox_cBNoFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_cUser = new System.Windows.Forms.TextBox();
            this.comboBox_nStatus = new System.Windows.Forms.ComboBox();
            this.comboBox_cWHId = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_cBNo = new System.Windows.Forms.TextBox();
            this.ppmDtl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_New = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_DoAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_DoAccountAll = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource_Main = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource_Detail = new System.Windows.Forms.BindingSource(this.components);
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView_Detail = new System.Windows.Forms.DataGridView();
            this.nPalletId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBoxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nQCStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nStatusD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDtlcRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dtl_cWHIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dtl_cAreaIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dtl_cPosIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Main)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.ppmDtl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Detail)).BeginInit();
            this.stbMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Detail)).BeginInit();
            this.SuspendLayout();
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
            this.toolStripButton_Audit,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.toolStripSeparator8,
            this.tlbSaveSysRts,
            this.toolStripSeparator,
            this.toolStripSeparator9});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(911, 25);
            this.tlbMain.TabIndex = 15;
            this.tlbMain.Text = "toolStrip1";
            this.tlbMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlbMain_ItemClicked);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
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
            this.tlb_M_New.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_New.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_New.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_New.Image")));
            this.tlb_M_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_New.Name = "tlb_M_New";
            this.tlb_M_New.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_New.Text = "�½�";
            this.tlb_M_New.Visible = false;
            this.tlb_M_New.Click += new System.EventHandler(this.tlb_M_New_Click);
            // 
            // tlb_M_Edit
            // 
            this.tlb_M_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Edit.Image")));
            this.tlb_M_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Edit.Text = "�޸�";
            this.tlb_M_Edit.Visible = false;
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
            this.tlb_M_Undo.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Undo.Image")));
            this.tlb_M_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Undo.Text = "ȡ��";
            this.tlb_M_Undo.Visible = false;
            this.tlb_M_Undo.Click += new System.EventHandler(this.tlb_M_Undo_Click);
            // 
            // tlb_M_Delete
            // 
            this.tlb_M_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Delete.Image")));
            this.tlb_M_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Delete.Text = "ɾ��";
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
            this.tlb_M_Save.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Save.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Save.Image")));
            this.tlb_M_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Save.Name = "tlb_M_Save";
            this.tlb_M_Save.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Save.Text = "����";
            this.tlb_M_Save.Visible = false;
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
            this.tlb_M_Refresh.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Refresh.Image")));
            this.tlb_M_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Refresh.Text = "ˢ��";
            this.tlb_M_Refresh.Click += new System.EventHandler(this.tlb_M_Refresh_Click);
            // 
            // tlb_M_Find
            // 
            this.tlb_M_Find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Find.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Find.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Find.Image")));
            this.tlb_M_Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Find.Text = "����";
            this.tlb_M_Find.Visible = false;
            this.tlb_M_Find.Click += new System.EventHandler(this.tlb_M_Find_Click);
            // 
            // tlb_M_Print
            // 
            this.tlb_M_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Print.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Print.Image")));
            this.tlb_M_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Print.Text = "��ӡ";
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
            // toolStripButton_Audit
            // 
            this.toolStripButton_Audit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Audit.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButton_Audit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripButton_Audit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Audit.Image")));
            this.toolStripButton_Audit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Audit.Name = "toolStripButton_Audit";
            this.toolStripButton_Audit.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton_Audit.Text = "���";
            this.toolStripButton_Audit.Click += new System.EventHandler(this.toolStripButton_Audit_Click);
            // 
            // btn_M_Help
            // 
            this.btn_M_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_M_Help.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.btn_M_Help.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_M_Help.Image = ((System.Drawing.Image)(resources.GetObject("btn_M_Help.Image")));
            this.btn_M_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_M_Help.Name = "btn_M_Help";
            this.btn_M_Help.Size = new System.Drawing.Size(35, 22);
            this.btn_M_Help.Text = "����";
            this.btn_M_Help.Visible = false;
            // 
            // tlb_M_Exit
            // 
            this.tlb_M_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Exit.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Exit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tlb_M_Exit.Image")));
            this.tlb_M_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Exit.Name = "tlb_M_Exit";
            this.tlb_M_Exit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Exit.Text = "�˳�";
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
            this.tlbSaveSysRts.Text = "����ϵͳȨ��";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new System.EventHandler(this.tlbSaveSysRts_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 512);
            this.panel1.TabIndex = 16;
            // 
            // dataGridView_Main
            // 
            this.dataGridView_Main.AllowUserToAddRows = false;
            this.dataGridView_Main.AllowUserToDeleteRows = false;
            this.dataGridView_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cBNo,
            this.nBClass,
            this.cWHId,
            this.dDate,
            this.cChecker,
            this.dCheckDate,
            this.cLinkId,
            this.cBNoFrom,
            this.nStatus,
            this.cRemark});
            this.dataGridView_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Main.Location = new System.Drawing.Point(0, 108);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 23;
            this.dataGridView_Main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Main.Size = new System.Drawing.Size(285, 404);
            this.dataGridView_Main.TabIndex = 9;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            // 
            // cBNo
            // 
            this.cBNo.DataPropertyName = "cBNo";
            this.cBNo.HeaderText = "��������";
            this.cBNo.Name = "cBNo";
            this.cBNo.ReadOnly = true;
            // 
            // nBClass
            // 
            this.nBClass.DataPropertyName = "nBClass";
            this.nBClass.HeaderText = "��ҵ����";
            this.nBClass.Name = "nBClass";
            this.nBClass.ReadOnly = true;
            // 
            // cWHId
            // 
            this.cWHId.DataPropertyName = "cWHId";
            this.cWHId.HeaderText = "�̵�ֿ�";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            // 
            // dDate
            // 
            this.dDate.DataPropertyName = "dDate";
            this.dDate.HeaderText = "��������";
            this.dDate.Name = "dDate";
            this.dDate.ReadOnly = true;
            // 
            // cChecker
            // 
            this.cChecker.DataPropertyName = "cUser";
            this.cChecker.HeaderText = "������Ա";
            this.cChecker.Name = "cChecker";
            this.cChecker.ReadOnly = true;
            // 
            // dCheckDate
            // 
            this.dCheckDate.DataPropertyName = "dCreateDate";
            this.dCheckDate.HeaderText = "��������";
            this.dCheckDate.Name = "dCheckDate";
            this.dCheckDate.ReadOnly = true;
            // 
            // cLinkId
            // 
            this.cLinkId.DataPropertyName = "cCreateor";
            this.cLinkId.HeaderText = "������Ա";
            this.cLinkId.Name = "cLinkId";
            this.cLinkId.ReadOnly = true;
            // 
            // cBNoFrom
            // 
            this.cBNoFrom.DataPropertyName = "cBNoFrom";
            this.cBNoFrom.HeaderText = "������Դ";
            this.cBNoFrom.Name = "cBNoFrom";
            this.cBNoFrom.ReadOnly = true;
            // 
            // nStatus
            // 
            this.nStatus.DataPropertyName = "nStatus";
            this.nStatus.HeaderText = "����״̬";
            this.nStatus.Name = "nStatus";
            this.nStatus.ReadOnly = true;
            // 
            // cRemark
            // 
            this.cRemark.DataPropertyName = "cRemark";
            this.cRemark.HeaderText = "��ע";
            this.cRemark.Name = "cRemark";
            this.cRemark.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btn_Qry);
            this.panel2.Controls.Add(this.dtp_From);
            this.panel2.Controls.Add(this.cmbQ_Status);
            this.panel2.Controls.Add(this.dtp_To);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtQ_BNo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbQ_WHId);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 108);
            this.panel2.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "�ֿ�";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(222, 69);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(55, 23);
            this.btnReset.TabIndex = 43;
            this.btnReset.Text = "����";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "����";
            // 
            // btn_Qry
            // 
            this.btn_Qry.Location = new System.Drawing.Point(157, 69);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new System.Drawing.Size(55, 23);
            this.btn_Qry.TabIndex = 42;
            this.btn_Qry.Text = "��ѯ";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new System.EventHandler(this.btn_Qry_Click);
            // 
            // dtp_From
            // 
            this.dtp_From.Location = new System.Drawing.Point(44, 45);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new System.Drawing.Size(107, 21);
            this.dtp_From.TabIndex = 35;
            // 
            // cmbQ_Status
            // 
            this.cmbQ_Status.FormattingEnabled = true;
            this.cmbQ_Status.Location = new System.Drawing.Point(190, 18);
            this.cmbQ_Status.Name = "cmbQ_Status";
            this.cmbQ_Status.Size = new System.Drawing.Size(87, 20);
            this.cmbQ_Status.TabIndex = 41;
            this.cmbQ_Status.Tag = "101";
            this.cmbQ_Status.Text = "Bind SelectedValue";
            // 
            // dtp_To
            // 
            this.dtp_To.Location = new System.Drawing.Point(161, 45);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new System.Drawing.Size(116, 21);
            this.dtp_To.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "״̬";
            // 
            // txtQ_BNo
            // 
            this.txtQ_BNo.Location = new System.Drawing.Point(44, 70);
            this.txtQ_BNo.Name = "txtQ_BNo";
            this.txtQ_BNo.Size = new System.Drawing.Size(107, 21);
            this.txtQ_BNo.TabIndex = 23;
            this.txtQ_BNo.Tag = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "����";
            // 
            // cmbQ_WHId
            // 
            this.cmbQ_WHId.FormattingEnabled = true;
            this.cmbQ_WHId.Location = new System.Drawing.Point(44, 18);
            this.cmbQ_WHId.Name = "cmbQ_WHId";
            this.cmbQ_WHId.Size = new System.Drawing.Size(107, 20);
            this.cmbQ_WHId.TabIndex = 38;
            this.cmbQ_WHId.Tag = "101";
            this.cmbQ_WHId.Text = "Bind SelectedValue";
            // 
            // panel_Edit
            // 
            this.panel_Edit.Controls.Add(this.dtp_dDate);
            this.panel_Edit.Controls.Add(this.textBox);
            this.panel_Edit.Controls.Add(this.textBox_cBNoFrom);
            this.panel_Edit.Controls.Add(this.label6);
            this.panel_Edit.Controls.Add(this.textBox_cUser);
            this.panel_Edit.Controls.Add(this.comboBox_nStatus);
            this.panel_Edit.Controls.Add(this.comboBox_cWHId);
            this.panel_Edit.Controls.Add(this.label11);
            this.panel_Edit.Controls.Add(this.label10);
            this.panel_Edit.Controls.Add(this.label9);
            this.panel_Edit.Controls.Add(this.label8);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cBNo);
            this.panel_Edit.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Edit.Location = new System.Drawing.Point(285, 25);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new System.Drawing.Size(626, 108);
            this.panel_Edit.TabIndex = 17;
            this.panel_Edit.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Edit_Paint);
            // 
            // dtp_dDate
            // 
            this.dtp_dDate.Location = new System.Drawing.Point(78, 45);
            this.dtp_dDate.Name = "dtp_dDate";
            this.dtp_dDate.Size = new System.Drawing.Size(114, 21);
            this.dtp_dDate.TabIndex = 46;
            this.dtp_dDate.Tag = "2";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(78, 71);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(481, 21);
            this.textBox.TabIndex = 45;
            this.textBox.Tag = "0";
            // 
            // textBox_cBNoFrom
            // 
            this.textBox_cBNoFrom.Location = new System.Drawing.Point(270, 45);
            this.textBox_cBNoFrom.Name = "textBox_cBNoFrom";
            this.textBox_cBNoFrom.Size = new System.Drawing.Size(100, 21);
            this.textBox_cBNoFrom.TabIndex = 44;
            this.textBox_cBNoFrom.Tag = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "��Դ����";
            // 
            // textBox_cUser
            // 
            this.textBox_cUser.Location = new System.Drawing.Point(446, 45);
            this.textBox_cUser.Name = "textBox_cUser";
            this.textBox_cUser.Size = new System.Drawing.Size(113, 21);
            this.textBox_cUser.TabIndex = 41;
            this.textBox_cUser.Tag = "0";
            // 
            // comboBox_nStatus
            // 
            this.comboBox_nStatus.FormattingEnabled = true;
            this.comboBox_nStatus.Location = new System.Drawing.Point(446, 20);
            this.comboBox_nStatus.Name = "comboBox_nStatus";
            this.comboBox_nStatus.Size = new System.Drawing.Size(113, 20);
            this.comboBox_nStatus.TabIndex = 39;
            this.comboBox_nStatus.Tag = "101";
            this.comboBox_nStatus.Text = "Bind SelectedValue";
            // 
            // comboBox_cWHId
            // 
            this.comboBox_cWHId.FormattingEnabled = true;
            this.comboBox_cWHId.Location = new System.Drawing.Point(270, 20);
            this.comboBox_cWHId.Name = "comboBox_cWHId";
            this.comboBox_cWHId.Size = new System.Drawing.Size(100, 20);
            this.comboBox_cWHId.TabIndex = 37;
            this.comboBox_cWHId.Tag = "101";
            this.comboBox_cWHId.Text = "Bind SelectedValue";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(386, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 36;
            this.label11.Text = "����״̬";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 35;
            this.label10.Text = "������ע";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(211, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 34;
            this.label9.Text = "�����ֿ�";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "��������";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "������Ա";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "��������";
            // 
            // textBox_cBNo
            // 
            this.textBox_cBNo.Location = new System.Drawing.Point(78, 20);
            this.textBox_cBNo.Name = "textBox_cBNo";
            this.textBox_cBNo.Size = new System.Drawing.Size(113, 21);
            this.textBox_cBNo.TabIndex = 23;
            this.textBox_cBNo.Tag = "0";
            // 
            // ppmDtl
            // 
            this.ppmDtl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_New,
            this.mi_Edit,
            this.mi_Delete,
            this.toolStripMenuItem2,
            this.mi_DoAccount,
            this.mi_DoAccountAll});
            this.ppmDtl.Name = "contextMenuStrip1";
            this.ppmDtl.Size = new System.Drawing.Size(119, 120);
            this.ppmDtl.Text = "�·�����";
            // 
            // mi_New
            // 
            this.mi_New.Name = "mi_New";
            this.mi_New.Size = new System.Drawing.Size(118, 22);
            this.mi_New.Text = "�½���ϸ";
            this.mi_New.Click += new System.EventHandler(this.mi_New_Click);
            // 
            // mi_Edit
            // 
            this.mi_Edit.Name = "mi_Edit";
            this.mi_Edit.Size = new System.Drawing.Size(118, 22);
            this.mi_Edit.Text = "�޸���ϸ";
            this.mi_Edit.Click += new System.EventHandler(this.mi_Edit_Click);
            // 
            // mi_Delete
            // 
            this.mi_Delete.Name = "mi_Delete";
            this.mi_Delete.Size = new System.Drawing.Size(118, 22);
            this.mi_Delete.Text = "ɾ����ϸ";
            this.mi_Delete.Click += new System.EventHandler(this.mi_Delete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(115, 6);
            // 
            // mi_DoAccount
            // 
            this.mi_DoAccount.Name = "mi_DoAccount";
            this.mi_DoAccount.Size = new System.Drawing.Size(118, 22);
            this.mi_DoAccount.Text = "����";
            this.mi_DoAccount.Click += new System.EventHandler(this.mi_DoAccount_Click);
            // 
            // mi_DoAccountAll
            // 
            this.mi_DoAccountAll.Name = "mi_DoAccountAll";
            this.mi_DoAccountAll.Size = new System.Drawing.Size(118, 22);
            this.mi_DoAccountAll.Text = "��������";
            this.mi_DoAccountAll.Click += new System.EventHandler(this.mi_DoAccountAll_Click);
            // 
            // bindingSource_Main
            // 
            this.bindingSource_Main.CurrentChanged += new System.EventHandler(this.bindingSource_Main_CurrentChanged);
            this.bindingSource_Main.PositionChanged += new System.EventHandler(this.bindingSource_Main_PositionChanged);
            // 
            // bindingSource_Detail
            // 
            this.bindingSource_Detail.CurrentChanged += new System.EventHandler(this.bindingSource_Detail_CurrentChanged);
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbModul,
            this.stbUser,
            this.stbState,
            this.stbDateTime});
            this.stbMain.Location = new System.Drawing.Point(0, 537);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(911, 22);
            this.stbMain.TabIndex = 18;
            this.stbMain.Text = "statusStrip1";
            // 
            // stbModul
            // 
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new System.Drawing.Size(35, 17);
            this.stbModul.Text = "ģ��:";
            // 
            // stbUser
            // 
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new System.Drawing.Size(47, 17);
            this.stbUser.Text = "�û���:";
            // 
            // stbState
            // 
            this.stbState.Name = "stbState";
            this.stbState.Size = new System.Drawing.Size(35, 17);
            this.stbState.Text = "״̬:";
            // 
            // stbDateTime
            // 
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new System.Drawing.Size(35, 17);
            this.stbDateTime.Text = "ʱ��:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(285, 133);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(626, 404);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_Detail);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(618, 379);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "��������";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Detail
            // 
            this.dataGridView_Detail.AllowUserToAddRows = false;
            this.dataGridView_Detail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nPalletId,
            this.cMNo,
            this.cMName,
            this.cSpec,
            this.cUnit,
            this.cBatchNo,
            this.fQty,
            this.dataGridViewTextBoxColumn2,
            this.cBoxId,
            this.nQCStatus,
            this.dataGridViewTextBoxColumn1,
            this.nStatusD,
            this.colDtlcRemark,
            this.col_dtl_cWHIdErp,
            this.col_dtl_cAreaIdErp,
            this.col_dtl_cPosIdErp});
            this.dataGridView_Detail.ContextMenuStrip = this.ppmDtl;
            this.dataGridView_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Detail.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Detail.Name = "dataGridView_Detail";
            this.dataGridView_Detail.RowHeadersVisible = false;
            this.dataGridView_Detail.RowTemplate.Height = 23;
            this.dataGridView_Detail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Detail.Size = new System.Drawing.Size(612, 373);
            this.dataGridView_Detail.TabIndex = 11;
            this.dataGridView_Detail.Tag = "8";
            // 
            // nPalletId
            // 
            this.nPalletId.DataPropertyName = "nPalletId";
            this.nPalletId.HeaderText = "���̺���";
            this.nPalletId.Name = "nPalletId";
            this.nPalletId.Width = 75;
            // 
            // cMNo
            // 
            this.cMNo.DataPropertyName = "cMNo";
            this.cMNo.HeaderText = "���ϱ��";
            this.cMNo.Name = "cMNo";
            // 
            // cMName
            // 
            this.cMName.DataPropertyName = "cMName";
            this.cMName.HeaderText = "��������";
            this.cMName.Name = "cMName";
            // 
            // cSpec
            // 
            this.cSpec.DataPropertyName = "cSpec";
            this.cSpec.HeaderText = "���Ϲ��";
            this.cSpec.Name = "cSpec";
            // 
            // cUnit
            // 
            this.cUnit.DataPropertyName = "cUnit";
            this.cUnit.FillWeight = 70F;
            this.cUnit.HeaderText = "���ϵ�λ";
            this.cUnit.Name = "cUnit";
            this.cUnit.ToolTipText = "���ϵ�λ";
            this.cUnit.Width = 65;
            // 
            // cBatchNo
            // 
            this.cBatchNo.DataPropertyName = "cBatchNo";
            this.cBatchNo.HeaderText = "���δ���";
            this.cBatchNo.Name = "cBatchNo";
            this.cBatchNo.Width = 85;
            // 
            // fQty
            // 
            this.fQty.DataPropertyName = "fQty";
            this.fQty.HeaderText = "��������";
            this.fQty.Name = "fQty";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "cWHId";
            this.dataGridViewTextBoxColumn2.FillWeight = 70F;
            this.dataGridViewTextBoxColumn2.HeaderText = "�����ֿ�";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "�����ֿ�";
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // cBoxId
            // 
            this.cBoxId.DataPropertyName = "cBoxId";
            this.cBoxId.HeaderText = "��ת���";
            this.cBoxId.Name = "cBoxId";
            // 
            // nQCStatus
            // 
            this.nQCStatus.DataPropertyName = "nQCStatus";
            this.nQCStatus.HeaderText = "QC״̬";
            this.nQCStatus.Name = "nQCStatus";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cBNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "��������";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // nStatusD
            // 
            this.nStatusD.DataPropertyName = "nStatus";
            this.nStatusD.HeaderText = "���״̬";
            this.nStatusD.Name = "nStatusD";
            // 
            // colDtlcRemark
            // 
            this.colDtlcRemark.DataPropertyName = "cRemark";
            this.colDtlcRemark.HeaderText = "��ע";
            this.colDtlcRemark.Name = "colDtlcRemark";
            // 
            // col_dtl_cWHIdErp
            // 
            this.col_dtl_cWHIdErp.DataPropertyName = "cWHIdErp";
            this.col_dtl_cWHIdErp.HeaderText = "ERP�ֿ��";
            this.col_dtl_cWHIdErp.Name = "col_dtl_cWHIdErp";
            // 
            // col_dtl_cAreaIdErp
            // 
            this.col_dtl_cAreaIdErp.DataPropertyName = "cAreaIdErp";
            this.col_dtl_cAreaIdErp.HeaderText = "ERP������";
            this.col_dtl_cAreaIdErp.Name = "col_dtl_cAreaIdErp";
            // 
            // col_dtl_cPosIdErp
            // 
            this.col_dtl_cPosIdErp.DataPropertyName = "cPosIdErp";
            this.col_dtl_cPosIdErp.HeaderText = "ERP��λ��";
            this.col_dtl_cPosIdErp.Name = "col_dtl_cPosIdErp";
            // 
            // FrmStockMAjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(911, 559);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel_Edit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.MinimizeBox = false;
            this.Name = "FrmStockMAjust";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FrmStockMAjust";
            this.Load += new System.EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Main)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.ppmDtl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Detail)).EndInit();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Detail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView_Main;
        private System.Windows.Forms.BindingSource bindingSource_Main;
        private System.Windows.Forms.Panel panel_Edit;
        private System.Windows.Forms.BindingSource bindingSource_Detail;
        public System.Windows.Forms.StatusStrip stbMain;
        public System.Windows.Forms.ToolStripStatusLabel stbModul;
        public System.Windows.Forms.ToolStripStatusLabel stbUser;
        public System.Windows.Forms.ToolStripStatusLabel stbState;
        public System.Windows.Forms.ToolStripStatusLabel stbDateTime;
        private System.Windows.Forms.TextBox textBox_cBNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_cWHId;
        private System.Windows.Forms.ComboBox comboBox_nStatus;
        private System.Windows.Forms.ToolStripButton toolStripButton_Audit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ContextMenuStrip ppmDtl;
        private System.Windows.Forms.ToolStripMenuItem mi_Edit;
        private System.Windows.Forms.ToolStripMenuItem mi_New;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_cUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_cBNoFrom;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nBClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn cWHId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cChecker;
        private System.Windows.Forms.DataGridViewTextBoxColumn dCheckDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLinkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBNoFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn nStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQ_BNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtp_From;
        private System.Windows.Forms.DateTimePicker dtp_To;
        private System.Windows.Forms.ToolStripMenuItem mi_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mi_DoAccount;
        private System.Windows.Forms.ToolStripMenuItem mi_DoAccountAll;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_Detail;
        private System.Windows.Forms.ComboBox cmbQ_WHId;
        private System.Windows.Forms.ComboBox cmbQ_Status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btn_Qry;
        private System.Windows.Forms.DateTimePicker dtp_dDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nPalletId;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn fQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBoxId;
        private System.Windows.Forms.DataGridViewTextBoxColumn nQCStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nStatusD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDtlcRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dtl_cWHIdErp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dtl_cAreaIdErp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dtl_cPosIdErp;
    }
}
