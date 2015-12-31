namespace WareBaseMS
{
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;
    using DBCommInfo;

    public class FrmStockInfo : FrmSTable
    {
        private bool bCodeIsManual = false;
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private ComboBox cmb_bUsed;
        private ComboBox cmb_nIsOCS;
        private ComboBox cmb_nType;
        private ComboBox cmb_nWholePacking;
        private DataGridViewTextBoxColumn cName;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cWHId;
        private DataGridView dataGridView_Main;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private DataGridViewTextBoxColumn nType;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cWHID";
        private string strTbNameMain = "TWC_WareHouse";
        private TextBox textBox_cName;
        private TextBox textBox_cNameQ;
        private TextBox textBox_cWHId;
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

        public FrmStockInfo()
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
            sSql = "select * from " + this.strTbNameMain + " " + SqlStrConditon;
            base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
            flag = base.DBDataSet != null;
            this.bindingSource_Main.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
            FDataGridView.DataSource = this.bindingSource_Main;
            if (this.bindingSource_Main.Count > 0)
            {
                try
                {
                    this.bDSIsOpenForMain = true;
                    this.DataRowViewToUI((DataRowView) this.bindingSource_Main.Current, this.panel_Edit);
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
            if (this.bDSIsOpenForMain)
            {
                this.ClearUIValues(this.panel_Edit);
                if (!((DataRowView) this.bindingSource_Main.Current).IsNew)
                {
                    DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                    this.DataRowViewToUI(current, this.panel_Edit);
                }
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
            bool flag = false;
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                MessageBox.Show("对不起，您无权限删除仓库信息！");
                return false;
            }
            int optMain = -1;
            optMain = (int) this.OptMain;
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                flag = true;
                MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return flag;
            }
            if ((optMain > 0) && (optMain < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
                string sSql = "delete " + this.strTbNameMain + " where " + this.strKeyFld + "='" + current[this.strKeyFld].ToString() + "'";
                if (PubDBCommFuns.GetDataBySql(sSql, out sSql).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optDelete;
                    this.BandDataSet(" ", this.dataGridView_Main);
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.OptMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.OptMain);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                }
            }
            return flag;
        }

        private bool DoEdit()
        {
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                MessageBox.Show("对不起，您无权限修改仓库信息！");
                return false;
            }
            this.OptMain = OperateType.optEdit;
            ((DataRowView) this.bindingSource_Main.Current).BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            return false;
        }

        public bool DoNew()
        {
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                MessageBox.Show("对不起，您无权限新增仓库！");
                return false;
            }
            this.OptMain = OperateType.optNew;
            DataRowView drv = (DataRowView) this.bindingSource_Main.AddNew();
            drv["cWHId"] = "";
            drv["cName"] = "";
            drv["nType"] = 1;
            drv["bUsed"] = 1;
            if (base.UserInformation.UnitId == "901")
            {
                drv["cCmptId"] = "101";
            }
            else
            {
                drv["cCmptId"] = base.UserInformation.UnitId;
            }
            drv["nIsOCS"] = 0;
            this.DataRowViewToUI(drv, this.panel_Edit);
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.DisplayState(this.stbState, this.OptMain);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            if (this.bCodeIsManual)
            {
                this.textBox_cWHId.ReadOnly = false;
                this.textBox_cWHId.Enabled = true;
                this.textBox_cWHId.Focus();
            }
            else
            {
                this.textBox_cWHId.ReadOnly = true;
                this.textBox_cWHId.Enabled = true;
                this.textBox_cName.Focus();
            }
            return false;
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
            bool flag = false;
            string sSql = "";
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current.IsEdit || current.IsNew)
            {
                this.UIToDataRowView(current, this.panel_Edit);
                if (current[this.strKeyFld].ToString() == "")
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 5, base.UserInformation.UnitId);
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, true);
                }
                else
                {
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, this.OptMain == OperateType.optNew);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                if (PubDBCommFuns.GetDataBySql(sSql, DBSQLCommandInfo.GetFieldsForDate(current), out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optSave;
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OptMain = OperateType.optNone;
                    return flag;
                }
                MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return flag;
            }
            MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return flag;
        }

        private bool DoUndo()
        {
            this.OptMain = OperateType.optUndo;
            DataRowView drv = null;
            drv = (DataRowView) this.bindingSource_Main.Current;
            if (drv != null)
            {
                if (drv.IsEdit)
                {
                    drv.CancelEdit();
                }
                if (drv.IsNew)
                {
                    drv.Delete();
                }
                this.textBox_cWHId.ReadOnly = true;
                base.DBDataSet.Tables[this.strTbNameMain].AcceptChanges();
                drv = (DataRowView) this.bindingSource_Main.Current;
                this.ClearUIValues(this.panel_Edit);
                if (drv != null)
                {
                    this.DataRowViewToUI(drv, this.panel_Edit);
                }
                this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.OptMain = OperateType.optNone;
                this.DisplayState(this.stbState, this.OptMain);
                this.CtrlControlReadOnly(this.panel_Edit, false);
            }
            return false;
        }

        private void FrmStockInfo_Load(object sender, EventArgs e)
        {
            this.tlbSaveSysRts.Visible = base.UserInformation.UserName == "Admin5118";
            string sErr = "";
            StringBuilder builder = new StringBuilder("select * from TPB_Rights where cPRId ='" + base.ModuleRtsId.Trim() + "'");
            if (base.UserInformation.UserName != "Admin5118")
            {
                builder.Append(" and cRId in (select cRId from TPB_URTS where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, builder.ToString(), "UserRights", "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (base.UserInformation.UserName != "Admin5118")
            {
                this.CheckRights(this.tlbMain, set.Tables["UserRights"]);
            }
            this.ReadSysPar();
            this.LoadCommboxItemByValue();
            this.BandDataSet("", this.dataGridView_Main);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockInfo));
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
            this.cWHId = new DataGridViewTextBoxColumn();
            this.cName = new DataGridViewTextBoxColumn();
            this.nType = new DataGridViewTextBoxColumn();
            this.label4 = new Label();
            this.textBox2 = new TextBox();
            this.label5 = new Label();
            this.textBox_cWHIdQ = new TextBox();
            this.textBox_cNameQ = new TextBox();
            this.panel_Edit = new Panel();
            this.label8 = new Label();
            this.cmb_nIsOCS = new ComboBox();
            this.label7 = new Label();
            this.cmb_bUsed = new ComboBox();
            this.label6 = new Label();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cmb_nType = new ComboBox();
            this.textBox_cWHId = new TextBox();
            this.textBox_cName = new TextBox();
            this.bindingSource_Main = new BindingSource(this.components);
            this.cmb_nWholePacking = new ComboBox();
            this.label10 = new Label();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.panel_Edit.SuspendLayout();
            this.stbMain.SuspendLayout();
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
            this.tlb_M_New.Tag = "01";
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Click += new EventHandler(this.tlb_M_New_Click);
            this.tlb_M_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = (Image) manager.GetObject("tlb_M_Edit.Image");
            this.tlb_M_Edit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new Size(0x23, 0x16);
            this.tlb_M_Edit.Tag = "02";
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
            this.tlb_M_Undo.Tag = "03";
            this.tlb_M_Undo.Text = "取消";
            this.tlb_M_Undo.Click += new EventHandler(this.tlb_M_Undo_Click);
            this.tlb_M_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = (Image) manager.GetObject("tlb_M_Delete.Image");
            this.tlb_M_Delete.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new Size(0x23, 0x16);
            this.tlb_M_Delete.Tag = "04";
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
            this.tlb_M_Save.Tag = "05";
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
            this.tlb_M_Find.Click += new EventHandler(this.tlb_M_Find_Click);
            this.tlb_M_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Print.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = (Image) manager.GetObject("tlb_M_Print.Image");
            this.tlb_M_Print.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new Size(0x23, 0x16);
            this.tlb_M_Print.Tag = "06";
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
            this.tlbSaveSysRts.Size = new Size(0x51, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new EventHandler(this.tlbSaveSysRts_Click);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox_cWHIdQ);
            this.panel1.Controls.Add(this.textBox_cNameQ);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x156, 0x1d0);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cWHId, this.cName, this.nType });
            this.dataGridView_Main.Location = new Point(0, 0x21);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x156, 0x1af);
            this.dataGridView_Main.TabIndex = 9;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            this.cWHId.DataPropertyName = "cWHId";
            this.cWHId.HeaderText = "仓库代码";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "仓库名称";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.nType.DataPropertyName = "nType";
            this.nType.HeaderText = "仓库类型";
            this.nType.Name = "nType";
            this.nType.ReadOnly = true;
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
            this.panel_Edit.Controls.Add(this.cmb_nWholePacking);
            this.panel_Edit.Controls.Add(this.label10);
            this.panel_Edit.Controls.Add(this.label8);
            this.panel_Edit.Controls.Add(this.cmb_nIsOCS);
            this.panel_Edit.Controls.Add(this.label7);
            this.panel_Edit.Controls.Add(this.cmb_bUsed);
            this.panel_Edit.Controls.Add(this.label6);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Controls.Add(this.label3);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.cmb_nType);
            this.panel_Edit.Controls.Add(this.textBox_cWHId);
            this.panel_Edit.Controls.Add(this.textBox_cName);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(0x156, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x1b3, 0x1d0);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xf4, 0x11c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4d, 12);
            this.label8.TabIndex = 0x1d;
            this.label8.Text = "(仅对平面库)";
            this.cmb_nIsOCS.CausesValidation = false;
            this.cmb_nIsOCS.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_nIsOCS.FormattingEnabled = true;
            this.cmb_nIsOCS.Items.AddRange(new object[] { "不启用", "启用" });
            this.cmb_nIsOCS.Location = new Point(0x9d, 0x119);
            this.cmb_nIsOCS.Name = "cmb_nIsOCS";
            this.cmb_nIsOCS.Size = new Size(0x51, 20);
            this.cmb_nIsOCS.TabIndex = 0x1b;
            this.cmb_nIsOCS.Tag = "102";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(50, 0x11c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x65, 12);
            this.label7.TabIndex = 0x1c;
            this.label7.Text = "是否起用车载系统";
            this.cmb_bUsed.CausesValidation = false;
            this.cmb_bUsed.FormattingEnabled = true;
            this.cmb_bUsed.Items.AddRange(new object[] { "不启用", "启用" });
            this.cmb_bUsed.Location = new Point(0x7f, 0xec);
            this.cmb_bUsed.Name = "cmb_bUsed";
            this.cmb_bUsed.Size = new Size(0x6f, 20);
            this.cmb_bUsed.TabIndex = 3;
            this.cmb_bUsed.Tag = "102";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(50, 0xef);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x1a;
            this.label6.Text = "是否起用";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x1ba);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x1b3, 0x16);
            this.stbMain.TabIndex = 0x10;
            this.stbMain.Text = "statusStrip1";
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new Size(0x23, 0x11);
            this.stbModul.Text = "模块:";
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new Size(0x2f, 0x11);
            this.stbUser.Text = "用户名:";
            this.stbState.Name = "stbState";
            this.stbState.Size = new Size(0x23, 0x11);
            this.stbState.Text = "状态:";
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new Size(0x23, 0x11);
            this.stbDateTime.Text = "时间:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x2e, 0xca);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "仓库类型";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x2e, 0x9d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "仓库名称";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2e, 0x70);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "仓库代码";
            this.cmb_nType.FormattingEnabled = true;
            this.cmb_nType.Location = new Point(0x7f, 0xc2);
            this.cmb_nType.Name = "cmb_nType";
            this.cmb_nType.Size = new Size(0x6f, 20);
            this.cmb_nType.TabIndex = 2;
            this.cmb_nType.Tag = "101";
            this.cmb_nType.SelectedIndexChanged += new EventHandler(this.cmb_nType_SelectedIndexChanged);
            this.textBox_cWHId.Location = new Point(0x7f, 0x67);
            this.textBox_cWHId.Name = "textBox_cWHId";
            this.textBox_cWHId.ReadOnly = true;
            this.textBox_cWHId.Size = new Size(0x6f, 0x15);
            this.textBox_cWHId.TabIndex = 0;
            this.textBox_cWHId.Tag = "0";
            this.textBox_cWHId.ReadOnlyChanged += new EventHandler(this.textBox_cWHId_ReadOnlyChanged);
            this.textBox_cName.Location = new Point(0x7f, 0x94);
            this.textBox_cName.Name = "textBox_cName";
            this.textBox_cName.Size = new Size(0xcf, 0x15);
            this.textBox_cName.TabIndex = 1;
            this.textBox_cName.Tag = "0";
            this.textBox_cName.ReadOnlyChanged += new EventHandler(this.textBox_cWHId_ReadOnlyChanged);
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.cmb_nWholePacking.CausesValidation = false;
            this.cmb_nWholePacking.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_nWholePacking.FormattingEnabled = true;
            this.cmb_nWholePacking.Items.AddRange(new object[] { "不区分零整货", "整货库", "零货库" });
            this.cmb_nWholePacking.Location = new Point(0x7f, 0x143);
            this.cmb_nWholePacking.Name = "cmb_nWholePacking";
            this.cmb_nWholePacking.Size = new Size(0x6f, 20);
            this.cmb_nWholePacking.TabIndex = 30;
            this.cmb_nWholePacking.Tag = "102";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(50, 0x146);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x41, 12);
            this.label10.TabIndex = 0x1f;
            this.label10.Text = "货件属性：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1e9);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.KeyPreview = true;
            base.Name = "FrmStockInfo";
            this.Text = "仓库管理";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(1, "立体仓库"));
            list.Add(new DictionaryEntry(2, "平面仓库"));
            list.Add(new DictionaryEntry(3, "DPS仓库"));
            this.cmb_nType.DisplayMember = "Value";
            this.cmb_nType.ValueMember = "Key";
            this.cmb_nType.DataSource = list;
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ReadSysPar()
        {
            string sErr = "";
            string sSql = "select * from tps_syspar where cParId='WarehouseIdIsManual'";
            DataSet dataBySql = null;
            dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
            }
            else if (dataBySql == null)
            {
                MessageBox.Show("从数据库中读取系统参数时，出错！");
            }
            else
            {
                DataTable table = dataBySql.Tables["data"];
                DataRow row = table.Rows[0];
                object obj2 = row["cParValue"];
                if (obj2 != null)
                {
                    this.bCodeIsManual = obj2.ToString().Trim() == "1";
                }
                dataBySql.Clear();
            }
        }

        private void textBox_cWHId_ReadOnlyChanged(object sender, EventArgs e)
        {
            base.ChangeTextBoxBkColorByReadOnly(sender, this.panel_Edit.BackColor, Color.White);
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

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
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

        private void tlbSaveSysRts_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in this.tlbMain.Items)
            {
                object tag = item.Tag;
                if (tag != null)
                {
                    string sErr = "";
                    string text = item.Text;
                    string name = item.Name;
                    string pRId = base.ModuleRtsId + tag.ToString();
                    PubDBCommFuns.sp_SaveSysRight(base.AppInformation.SvrSocket, base.ModuleRtsId, pRId, text, "", name, 3, "Sys", out sErr);
                }
            }
        }
    }
}

