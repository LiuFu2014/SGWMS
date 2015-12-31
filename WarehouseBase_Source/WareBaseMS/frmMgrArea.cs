namespace WareBaseMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;
    using DBCommInfo;

    public class frmMgrArea : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private Color clrLblBackGround;
        private ComboBox cmb_bUsed;
        private DataGridViewTextBoxColumn cName;
        private DataGridViewTextBoxColumn colbUsed;
        private DataGridViewTextBoxColumn colcUsed;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cWHId;
        private DataGridView dataGridView_Main;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label6;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cMAreaId";
        private string strTbNameMain = "TWC_MgrArea";
        private TextBox textBox_cAMNameQ;
        private TextBox textBox_cMAName;
        private TextBox textBox_cMAreaId;
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
        private ToolTip tlpMain;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;

        public frmMgrArea()
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
            sSql = "select cMAreaId,cMAName,bUsed,case isnull(bUsed,1) when 0 then '停用' else '启用' end cUsed from TWC_MgrArea   " + SqlStrConditon;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                base.DBDataSet = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
            }
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
                    this.bindingSource_Main_PositionChanged(null, null);
                }
                catch (Exception exception2)
                {
                    this.bDSIsOpenForMain = false;
                    MessageBox.Show(exception2.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
                return flag;
            }
            this.ClearUIValues(this.panel_Edit);
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
                    if (current != null)
                    {
                        this.DataRowViewToUI(current, this.panel_Edit);
                    }
                }
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void dataGridView_Main_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.ColumnIndex == 2) && (e.RowIndex >= 0))
            {
                string str = "0";
                if (e.Value != null)
                {
                    str = e.Value.ToString();
                }
                if (str.Trim() == "")
                {
                    str = "0";
                }
                if (str.Trim() == "0")
                {
                    this.dataGridView_Main.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    this.dataGridView_Main.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
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

        private bool DoDelete()
        {
            if (this.dataGridView_Main.RowCount >= 2)
            {
                bool flag = false;
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
                if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return flag;
                }
                string sSql = "delete " + this.strTbNameMain + " where " + this.strKeyFld + "='" + current[this.strKeyFld].ToString() + "'";
                string sErr = "";
                if (DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, "", out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optDelete;
                    this.BandDataSet(" ", this.dataGridView_Main);
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.OptMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.OptMain);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("删除成功！");
                    this.DoRefresh();
                    return flag;
                }
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return true;
        }

        private bool DoEdit()
        {
            if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
            {
                MessageBox.Show("对不起，当前正处于编辑状态！");
                return false;
            }
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("对不起，无数据可修改！");
                return false;
            }
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("对不起，无数据可修改！");
                return false;
            }
            this.OptMain = OperateType.optEdit;
            current.BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cMAName.SelectAll();
            this.textBox_cMAName.Focus();
            return false;
        }

        public bool DoNew()
        {
            bool flag = true;
            if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
            {
                MessageBox.Show("对不起，当前正处于编辑状态！");
                return false;
            }
            this.OptMain = OperateType.optNew;
            try
            {
                DataRowView drv = (DataRowView) this.bindingSource_Main.AddNew();
                drv["cMAreaId"] = "";
                drv["cMAName"] = "";
                drv["bUsed"] = 1;
                this.DataRowViewToUI(drv, this.panel_Edit);
                this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.DisplayState(this.stbState, this.OptMain);
                this.CtrlControlReadOnly(this.panel_Edit, true);
                this.textBox_cMAreaId.ReadOnly = true;
                this.textBox_cMAName.Focus();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                flag = false;
            }
            return flag;
        }

        private bool DoRefresh()
        {
            this.sbCondition.Remove(0, this.sbCondition.Length);
            string str = "";
            if (this.textBox_cAMNameQ.Text.Trim().Length > 0)
            {
                str = " where (cMAreaId like '%" + this.textBox_cAMNameQ.Text.ToString().Trim() + "%') or (cMAName like '%" + this.textBox_cAMNameQ.Text.ToString().Trim() + "%') ";
            }
            this.sbCondition.Append(str);
            this.BandDataSet(this.sbCondition.ToString(), this.dataGridView_Main);
            return false;
        }

        private bool DoSave()
        {
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
            bool flag = false;
            string sSql = "";
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current.IsEdit || current.IsNew)
            {
                this.UIToDataRowView(current, this.panel_Edit);
                if (current[this.strKeyFld].ToString() == "")
                {
                    current[this.strKeyFld] = DBFuns.GetNewId(base.AppInformation.SvrSocket, this.strTbNameMain, this.strKeyFld, 3, "M", out sErr);
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, "cUsed", true);
                }
                else
                {
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, "cUsed", false);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                if (DBFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, DBSQLCommandInfo.GetFieldsForDate(current), out sErr))
                {
                    this.OptMain = OperateType.optSave;
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OptMain = OperateType.optNone;
                    this.DoRefresh();
                }
                else
                {
                    MessageBox.Show("保存数据失败：" + sErr, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                return flag;
            }
            MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return flag;
        }

        private bool DoUndo()
        {
            DataRowView drv = null;
            drv = (DataRowView) this.bindingSource_Main.Current;
            if (drv != null)
            {
                if (this.OptMain == OperateType.optEdit)
                {
                    drv.CancelEdit();
                }
                else if (this.OptMain == OperateType.optNew)
                {
                    drv.Delete();
                }
                base.DBDataSet.Tables[this.strTbNameMain].AcceptChanges();
                this.OptMain = OperateType.optUndo;
                drv = (DataRowView) this.bindingSource_Main.Current;
                if (drv != null)
                {
                    this.DataRowViewToUI(drv, this.panel_Edit);
                }
                else
                {
                    this.ClearUIValues(this.panel_Edit);
                }
                this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.OptMain = OperateType.optNone;
                this.DisplayState(this.stbState, this.OptMain);
                this.CtrlControlReadOnly(this.panel_Edit, false);
            }
            return false;
        }

        private void frmMgrArea_Load(object sender, EventArgs e)
        {
            this.tlbSaveSysRts.Visible = base.UserInformation.UserName == "Admin5118";
            string sErr = "";
            StringBuilder builder = new StringBuilder("select * from TPB_Rights where cPRId ='" + base.ModuleRtsId.Trim() + "'");
            if (base.UserInformation.UserName != "Admin5118")
            {
                builder.Append(" and cRId in (select cRId from TPB_URTS where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            DataSet set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, builder.ToString(), "UserRights", "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (base.UserInformation.UserName != "Admin5118")
            {
                this.CheckRights(this.tlbMain, set.Tables["UserRights"]);
            }
            this.BandDataSet("", this.dataGridView_Main);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmMgrArea));
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
            this.bindingSource_Main = new BindingSource(this.components);
            this.tlpMain = new ToolTip(this.components);
            this.panel1 = new Panel();
            this.dataGridView_Main = new DataGridView();
            this.cWHId = new DataGridViewTextBoxColumn();
            this.cName = new DataGridViewTextBoxColumn();
            this.colbUsed = new DataGridViewTextBoxColumn();
            this.colcUsed = new DataGridViewTextBoxColumn();
            this.groupBox1 = new GroupBox();
            this.btn_Qry = new Button();
            this.textBox_cAMNameQ = new TextBox();
            this.label5 = new Label();
            this.panel_Edit = new Panel();
            this.cmb_bUsed = new ComboBox();
            this.label6 = new Label();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cMAreaId = new TextBox();
            this.textBox_cMAName = new TextBox();
            this.tlbMain.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.stbMain.SuspendLayout();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x309, 0x19);
            this.tlbMain.TabIndex = 0x12;
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
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(6, 0x19);
            this.tlbSaveSysRts.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = (Image) manager.GetObject("tlbSaveSysRts.Image");
            this.tlbSaveSysRts.ImageTransparentColor = Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new Size(0x51, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x156, 0x1d0);
            this.panel1.TabIndex = 0x15;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cWHId, this.cName, this.colbUsed, this.colcUsed });
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 70);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x156, 0x18a);
            this.dataGridView_Main.TabIndex = 11;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellPainting += new DataGridViewCellPaintingEventHandler(this.dataGridView_Main_CellPainting);
            this.cWHId.DataPropertyName = "cAreaId";
            this.cWHId.HeaderText = "货区代码";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            this.cName.DataPropertyName = "cAreaName";
            this.cName.HeaderText = "货区名称";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.colbUsed.DataPropertyName = "bUsed";
            this.colbUsed.HeaderText = "是否启用";
            this.colbUsed.Name = "colbUsed";
            this.colbUsed.ReadOnly = true;
            this.colbUsed.Visible = false;
            this.colcUsed.DataPropertyName = "cUsed";
            this.colcUsed.HeaderText = "是否启用";
            this.colcUsed.Name = "colcUsed";
            this.colcUsed.ReadOnly = true;
            this.groupBox1.Controls.Add(this.btn_Qry);
            this.groupBox1.Controls.Add(this.textBox_cAMNameQ);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x156, 70);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            this.btn_Qry.Location = new Point(0x114, 0x1b);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(60, 0x17);
            this.btn_Qry.TabIndex = 0x12;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.textBox_cAMNameQ.Location = new Point(0x31, 0x1c);
            this.textBox_cAMNameQ.Name = "textBox_cAMNameQ";
            this.textBox_cAMNameQ.Size = new Size(0xdd, 0x15);
            this.textBox_cAMNameQ.TabIndex = 6;
            this.textBox_cAMNameQ.Tag = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 0x1f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "货区：";
            this.panel_Edit.Controls.Add(this.cmb_bUsed);
            this.panel_Edit.Controls.Add(this.label6);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cMAreaId);
            this.panel_Edit.Controls.Add(this.textBox_cMAName);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(0x156, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x1b3, 0x1d0);
            this.panel_Edit.TabIndex = 0x16;
            this.cmb_bUsed.CausesValidation = false;
            this.cmb_bUsed.FormattingEnabled = true;
            this.cmb_bUsed.Items.AddRange(new object[] { "停用", "启用" });
            this.cmb_bUsed.Location = new Point(0x86, 0xc3);
            this.cmb_bUsed.Name = "cmb_bUsed";
            this.cmb_bUsed.Size = new Size(100, 20);
            this.cmb_bUsed.TabIndex = 2;
            this.cmb_bUsed.Tag = "102";
            this.cmb_bUsed.Text = "Bind SelectedIndex";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x34, 0xc6);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x1a;
            this.label6.Text = "是否启用";
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
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x35, 0xa2);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "管理货区名称";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x35, 0x72);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "管理货区代码";
            this.textBox_cMAreaId.Location = new Point(0x88, 0x69);
            this.textBox_cMAreaId.Name = "textBox_cMAreaId";
            this.textBox_cMAreaId.ReadOnly = true;
            this.textBox_cMAreaId.Size = new Size(0x73, 0x15);
            this.textBox_cMAreaId.TabIndex = 0;
            this.textBox_cMAreaId.Tag = "0";
            this.textBox_cMAName.Location = new Point(0x88, 0x99);
            this.textBox_cMAName.Name = "textBox_cMAName";
            this.textBox_cMAName.Size = new Size(0xe0, 0x15);
            this.textBox_cMAName.TabIndex = 1;
            this.textBox_cMAName.Tag = "0";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1e9);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.KeyPreview = true;
            base.Name = "frmMgrArea";
            this.Text = "仓库管理货区管理";
            base.Load += new EventHandler(this.frmMgrArea_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadCommboxItemByValue()
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

