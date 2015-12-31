namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;

    public class FrmStockMExceptionSet : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private DataGridViewTextBoxColumn cBatchNo;
        private DataGridViewTextBoxColumn cBNo;
        private DataGridViewTextBoxColumn cChildId;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cParentId;
        private DataGridViewTextBoxColumn cRemark;
        private DataGridViewTextBoxColumn cUnit;
        private DataGridViewTextBoxColumn cUser;
        private DataGridView dataGridView_Main;
        private DataGridViewTextBoxColumn dOperateTime;
        private DataGridViewTextBoxColumn fQty;
        private Label label4;
        private Label label5;
        private DataGridViewTextBoxColumn nItem;
        private DataGridViewTextBoxColumn nStatus;
        private DataGridViewTextBoxColumn nWorkId;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel1;
        private StringBuilder sbCondition = new StringBuilder("");
        private string strKeyFld = "cWHID";
        private string strTbNameMain = "TPB_MaterialPackingHis";
        private TextBox textBox_cNameQ;
        private TextBox textBox_cWHIdQ;
        private TextBox textBox2;
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

        public FrmStockMExceptionSet()
        {
            this.InitializeComponent();
        }

        private bool BandDataSet(string SqlStrConditon, DataGridView FDataGridView)
        {
            bool flag = true;
            string sSql = "";
            string sErr = "";
            this.bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            sSql = "SELECT cId,nId,cChildId,fQty,cUnit,cBFromNo,nFromItem,cParentId,nLevel,dOperateTime,cUser,nStatus,cWorkFlow,cBNo,nItem,cRemark,cBatchNo,nQCStatus,nWorkId,nPlaceMode,dCreateDate,cCmptId FROM " + this.strTbNameMain + " " + SqlStrConditon;
            base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
            flag = base.DBDataSet != null;
            this.bindingSource_Main.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
            FDataGridView.DataSource = this.bindingSource_Main;
            if (this.bindingSource_Main.Count > 0)
            {
                try
                {
                    this.bDSIsOpenForMain = true;
                    this.OptMain = OperateType.optNone;
                }
                catch (Exception exception)
                {
                    this.bDSIsOpenForMain = false;
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            return flag;
        }

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDSIsOpenForMain && !((DataRowView) this.bindingSource_Main.Current).IsNew)
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void cmb_nType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_SelectedValue_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView_Main_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            new FrmStockMExceptionSetFilter { textBox_cChildId = { Text = current["cChildId"].ToString() }, textBox_fQty = { Text = current["fQty"].ToString() }, textBox_cParentId = { Text = current["cParentId"].ToString() }, textBox_dOperateTime = { Text = current["dOperateTime"].ToString() }, textBox_cBNo = { Text = current["cBNo"].ToString() }, textBox_nItem = { Text = current["nItem"].ToString() }, textBox_nWorkId = { Text = current["nWorkId"].ToString() }, textBox_cBatchNo = { Text = current["cBatchNo"].ToString() } }.ShowDialog();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoDelete()
        {
            return false;
        }

        private bool DoEdit()
        {
            return true;
        }

        public bool DoNew()
        {
            return true;
        }

        private bool DoRefresh()
        {
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
            this.sbCondition.Remove(0, this.sbCondition.Length);
            this.sbCondition = this.sbCondition.Append(" where  cWHId like '%" + this.textBox_cWHIdQ.Text.ToString().Trim() + "%'");
            this.sbCondition = this.sbCondition.Append(" and cName like '%" + this.textBox_cNameQ.Text.ToString().Trim() + "%'");
            this.BandDataSet(this.sbCondition.ToString(), this.dataGridView_Main);
            return false;
        }

        private bool DoSave()
        {
            return (this.dataGridView_Main.RowCount < 2);
        }

        private bool DoUndo()
        {
            return (this.dataGridView_Main.RowCount < 2);
        }

        private void FrmStockInfo_Load(object sender, EventArgs e)
        {
            this.LoadCommboxItemByValue();
            this.BandDataSet("", this.dataGridView_Main);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockMExceptionSet));
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tlb_M_New = new ToolStripButton();
            this.tlb_M_Edit = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tlb_M_Undo = new ToolStripButton();
            this.tlb_M_Delete = new ToolStripButton();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.tlb_M_Save = new ToolStripButton();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.tlb_M_Refresh = new ToolStripButton();
            this.tlb_M_Find = new ToolStripButton();
            this.tlb_M_Print = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.panel1 = new Panel();
            this.dataGridView_Main = new DataGridView();
            this.cChildId = new DataGridViewTextBoxColumn();
            this.fQty = new DataGridViewTextBoxColumn();
            this.cUnit = new DataGridViewTextBoxColumn();
            this.cParentId = new DataGridViewTextBoxColumn();
            this.dOperateTime = new DataGridViewTextBoxColumn();
            this.cUser = new DataGridViewTextBoxColumn();
            this.nStatus = new DataGridViewTextBoxColumn();
            this.cBNo = new DataGridViewTextBoxColumn();
            this.nItem = new DataGridViewTextBoxColumn();
            this.cRemark = new DataGridViewTextBoxColumn();
            this.cBatchNo = new DataGridViewTextBoxColumn();
            this.nWorkId = new DataGridViewTextBoxColumn();
            this.label4 = new Label();
            this.textBox2 = new TextBox();
            this.label5 = new Label();
            this.textBox_cWHIdQ = new TextBox();
            this.textBox_cNameQ = new TextBox();
            this.bindingSource_Main = new BindingSource(this.components);
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x309, 0x19);
            this.tlbMain.TabIndex = 15;
            this.tlbMain.Text = "toolStrip1";
            this.tlbMain.Visible = false;
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.tlb_M_New.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_New.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_New.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_New.Image = (Image) manager.GetObject("tlb_M_New.Image");
            this.tlb_M_New.ImageTransparentColor = Color.Magenta;
            this.tlb_M_New.Name = "tlb_M_New";
            this.tlb_M_New.Size = new Size(0x23, 0x16);
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Click += new EventHandler(this.tlb_M_New_Click);
            this.tlb_M_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = (Image) manager.GetObject("tlb_M_Edit.Image");
            this.tlb_M_Edit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new Size(0x23, 0x16);
            this.tlb_M_Edit.Text = "修改";
            this.tlb_M_Edit.Click += new EventHandler(this.tlb_M_Edit_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.tlb_M_Undo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = (Image) manager.GetObject("tlb_M_Undo.Image");
            this.tlb_M_Undo.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new Size(0x23, 0x16);
            this.tlb_M_Undo.Text = "取消";
            this.tlb_M_Undo.Click += new EventHandler(this.tlb_M_Undo_Click);
            this.tlb_M_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = (Image) manager.GetObject("tlb_M_Delete.Image");
            this.tlb_M_Delete.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new Size(0x23, 0x16);
            this.tlb_M_Delete.Text = "删除";
            this.tlb_M_Delete.Click += new EventHandler(this.tlb_M_Delete_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(6, 0x19);
            this.tlb_M_Save.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Save.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Save.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Save.Image = (Image) manager.GetObject("tlb_M_Save.Image");
            this.tlb_M_Save.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Save.Name = "tlb_M_Save";
            this.tlb_M_Save.Size = new Size(0x23, 0x16);
            this.tlb_M_Save.Text = "保存";
            this.tlb_M_Save.Click += new EventHandler(this.tlb_M_Save_Click);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(6, 0x19);
            this.tlb_M_Refresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Refresh.Image = (Image) manager.GetObject("tlb_M_Refresh.Image");
            this.tlb_M_Refresh.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new Size(0x23, 0x16);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new EventHandler(this.tlb_M_Refresh_Click);
            this.tlb_M_Find.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Find.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Find.Image = (Image) manager.GetObject("tlb_M_Find.Image");
            this.tlb_M_Find.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new Size(0x23, 0x16);
            this.tlb_M_Find.Text = "查找";
            this.tlb_M_Find.Visible = false;
            this.tlb_M_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Print.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = (Image) manager.GetObject("tlb_M_Print.Image");
            this.tlb_M_Print.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new Size(0x23, 0x16);
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Visible = false;
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
            this.btn_M_Help.Text = "帮助";
            this.btn_M_Help.Visible = false;
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
            this.tlbSaveSysRts.Size = new Size(0x54, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox_cWHIdQ);
            this.panel1.Controls.Add(this.textBox_cNameQ);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x309, 0x1e9);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.AllowUserToAddRows = false;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cChildId, this.fQty, this.cUnit, this.cParentId, this.dOperateTime, this.cUser, this.nStatus, this.cBNo, this.nItem, this.cRemark, this.cBatchNo, this.nWorkId });
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 0);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x309, 0x1e9);
            this.dataGridView_Main.TabIndex = 9;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellDoubleClick);
            this.dataGridView_Main.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            this.cChildId.DataPropertyName = "cChildId";
            this.cChildId.HeaderText = "物料编码";
            this.cChildId.Name = "cChildId";
            this.fQty.DataPropertyName = "fQty";
            this.fQty.HeaderText = "数量";
            this.fQty.Name = "fQty";
            this.cUnit.DataPropertyName = "cUnit";
            this.cUnit.HeaderText = "单位";
            this.cUnit.Name = "cUnit";
            this.cParentId.DataPropertyName = "cParentId";
            this.cParentId.HeaderText = "上级单码";
            this.cParentId.Name = "cParentId";
            this.dOperateTime.DataPropertyName = "dOperateTime";
            this.dOperateTime.HeaderText = "异动日期";
            this.dOperateTime.Name = "dOperateTime";
            this.cUser.DataPropertyName = "cUser";
            this.cUser.HeaderText = "操作人员";
            this.cUser.Name = "cUser";
            this.nStatus.DataPropertyName = "nStatus";
            this.nStatus.HeaderText = "状态";
            this.nStatus.Name = "nStatus";
            this.cBNo.DataPropertyName = "cBNo";
            this.cBNo.HeaderText = "单据号码";
            this.cBNo.Name = "cBNo";
            this.nItem.DataPropertyName = "nItem";
            this.nItem.HeaderText = "单据项次";
            this.nItem.Name = "nItem";
            this.cRemark.DataPropertyName = "cRemark";
            this.cRemark.HeaderText = "备注";
            this.cRemark.Name = "cRemark";
            this.cBatchNo.DataPropertyName = "cBatchNo";
            this.cBatchNo.HeaderText = "批次代号";
            this.cBatchNo.Name = "cBatchNo";
            this.nWorkId.DataPropertyName = "nWorkId";
            this.nWorkId.HeaderText = "任务号码";
            this.nWorkId.Name = "nWorkId";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "仓库代码";
            this.textBox2.Location = new Point(-331, -40);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(100, 0x15);
            this.textBox2.TabIndex = 7;
            this.textBox2.Tag = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xa8, 12);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "仓库名称";
            this.textBox_cWHIdQ.Location = new Point(0x3e, 3);
            this.textBox_cWHIdQ.Name = "textBox_cWHIdQ";
            this.textBox_cWHIdQ.Size = new Size(100, 0x15);
            this.textBox_cWHIdQ.TabIndex = 7;
            this.textBox_cWHIdQ.Tag = "0";
            this.textBox_cNameQ.Location = new Point(0xe3, 3);
            this.textBox_cNameQ.Name = "textBox_cNameQ";
            this.textBox_cNameQ.Size = new Size(100, 0x15);
            this.textBox_cNameQ.TabIndex = 6;
            this.textBox_cNameQ.Tag = "0";
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1e9);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.MinimizeBox = false;
            base.Name = "FrmStockMExceptionSet";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "FrmStockMExceptionSet";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadCommboxItemByValue()
        {
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            this.DoEdit();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            this.DoNew();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            this.DoSave();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            this.DoUndo();
        }
    }
}

