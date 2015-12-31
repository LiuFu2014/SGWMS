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
    using SunEast;

    public class FrmStockAreaInfo : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private Color clrLblBackGround;
        private ComboBox cmb_bUsed;
        private ComboBox cmb_cWHId;
        private DataGridViewTextBoxColumn cName;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cWHId;
        private DataGridView dataGridView_Main;
        private ColorDialog dlgColor;
        private DataGridViewTextBoxColumn grdc_Color;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblColor;
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
        private string strKeyFld = "cAreaId";
        private string strTbNameMain = "TWC_WArea";
        private TextBox textBox_cAreaId;
        private TextBox textBox_cAreaName;
        private TextBox textBox_cAreaNameQ;
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
        private TextBox txt_cCmptId;

        public FrmStockAreaInfo()
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
            sSql = "select * from " + this.strTbNameMain + "  " + SqlStrConditon;
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
                    this.bindingSource_Main_PositionChanged(null, null);
                }
                catch (Exception exception)
                {
                    this.bDSIsOpenForMain = false;
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                this.lblColor.BackColor = this.clrLblBackGround;
                if (!((DataRowView) this.bindingSource_Main.Current).IsNew)
                {
                    DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                    this.DataRowViewToUI(current, this.panel_Edit);
                    string s = "0";
                    s = current["nColorValue"].ToString();
                    if (s.Trim() == "")
                    {
                        s = "0";
                    }
                    Color color = Color.FromArgb(int.Parse(s));
                    this.lblColor.BackColor = color;
                }
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void cmb_SelectedValue_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView_Main_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.ColumnIndex == 3) && (e.RowIndex >= 0))
            {
                string s = "0";
                if (e.Value != null)
                {
                    s = e.Value.ToString();
                }
                if (s.Trim() == "")
                {
                    s = "0";
                }
                Color color = Color.FromArgb(int.Parse(s));
                e.CellStyle.BackColor = color;
                e.CellStyle.ForeColor = color;
                e.CellStyle.SelectionBackColor = color;
                e.CellStyle.SelectionForeColor = color;
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
                if (PubDBCommFuns.GetDataBySql(sSql, out sSql).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optDelete;
                    this.BandDataSet(" ", this.dataGridView_Main);
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.OptMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.OptMain);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("删除成功！");
                    return flag;
                }
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return true;
        }

        private bool DoEdit()
        {
            this.OptMain = OperateType.optEdit;
            ((DataRowView) this.bindingSource_Main.Current).BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            return false;
        }

        public bool DoNew()
        {
            if (this.cmb_cWHId.SelectedValue == null)
            {
                MessageBox.Show("请选择所属仓库！");
                this.cmb_cWHId.Focus();
                return false;
            }
            this.OptMain = OperateType.optNew;
            DataRowView drv = (DataRowView) this.bindingSource_Main.AddNew();
            drv["cAreaId"] = "";
            drv["cAreaName"] = "";
            drv["cCmptId"] = base.UserInformation.UnitId;
            drv["bUsed"] = 1;
            drv["cWHId"] = this.cmb_cWHId.SelectedValue.ToString();
            drv["nColorValue"] = this.clrLblBackGround.ToArgb();
            this.DataRowViewToUI(drv, this.panel_Edit);
            this.lblColor.BackColor = this.clrLblBackGround;
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.DisplayState(this.stbState, this.OptMain);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cAreaId.ReadOnly = true;
            this.textBox_cAreaName.Focus();
            return false;
        }

        private bool DoRefresh()
        {
            this.sbCondition.Remove(0, this.sbCondition.Length);
            string str = "";
            if ((this.cmb_cWHId.Text.Trim().Length > 0) && (this.cmb_cWHId.SelectedValue != null))
            {
                str = " where cWHId='" + this.cmb_cWHId.SelectedValue.ToString().Trim() + "'";
            }
            this.sbCondition.Append(str);
            str = this.textBox_cAreaNameQ.Text.Trim();
            if (str.Length > 0)
            {
                if (this.sbCondition.Length == 0)
                {
                    this.sbCondition = this.sbCondition.Append(" where  ((cAreaId like '%" + str + "%') or (cAreaName like '%" + str + "%'))");
                }
                else
                {
                    this.sbCondition = this.sbCondition.Append(" and  ((cAreaId like '%" + str + "%') or (cAreaName like '%" + str + "%'))");
                }
            }
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
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, current["cWHId"].ToString().Trim().Length + 3, current["cWHId"].ToString());
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, true);
                }
                else
                {
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, false);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                if (PubDBCommFuns.GetDataBySql(sSql, out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optSave;
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OptMain = OperateType.optNone;
                }
                else
                {
                    MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void FrmStockInfo_Load(object sender, EventArgs e)
        {
            this.clrLblBackGround = this.lblColor.BackColor;
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
            this.LoadStockList("");
            this.BandDataSet("", this.dataGridView_Main);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockAreaInfo));
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
            this.grdc_Color = new DataGridViewTextBoxColumn();
            this.groupBox1 = new GroupBox();
            this.btn_Qry = new Button();
            this.cmb_cWHId = new ComboBox();
            this.label3 = new Label();
            this.textBox_cAreaNameQ = new TextBox();
            this.label5 = new Label();
            this.bindingSource_Main = new BindingSource(this.components);
            this.panel_Edit = new Panel();
            this.label4 = new Label();
            this.lblColor = new Label();
            this.label7 = new Label();
            this.txt_cCmptId = new TextBox();
            this.cmb_bUsed = new ComboBox();
            this.label6 = new Label();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cAreaId = new TextBox();
            this.textBox_cAreaName = new TextBox();
            this.dlgColor = new ColorDialog();
            this.tlpMain = new ToolTip(this.components);
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            this.panel_Edit.SuspendLayout();
            this.stbMain.SuspendLayout();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x271, 0x19);
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
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x156, 0x12f);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cWHId, this.cName, this.nType, this.grdc_Color });
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 70);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x156, 0xe9);
            this.dataGridView_Main.TabIndex = 11;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellPainting += new DataGridViewCellPaintingEventHandler(this.dataGridView_Main_CellPainting);
            this.dataGridView_Main.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            this.cWHId.DataPropertyName = "cAreaId";
            this.cWHId.HeaderText = "货区代码";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            this.cName.DataPropertyName = "cAreaName";
            this.cName.HeaderText = "货区名称";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.nType.DataPropertyName = "cWHId";
            this.nType.HeaderText = "所属仓库";
            this.nType.Name = "nType";
            this.nType.ReadOnly = true;
            this.grdc_Color.DataPropertyName = "nColorValue";
            this.grdc_Color.HeaderText = "货区颜色";
            this.grdc_Color.Name = "grdc_Color";
            this.grdc_Color.ReadOnly = true;
            this.grdc_Color.ToolTipText = "货区颜色";
            this.groupBox1.Controls.Add(this.btn_Qry);
            this.groupBox1.Controls.Add(this.cmb_cWHId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_cAreaNameQ);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x156, 70);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            this.btn_Qry.Location = new Point(0x114, 0x29);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(60, 0x17);
            this.btn_Qry.TabIndex = 0x12;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0x31, 15);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(250, 20);
            this.cmb_cWHId.TabIndex = 0x10;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.Text = "Bind SelectedValue";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(5, 0x13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 0x11;
            this.label3.Text = "仓库：";
            this.textBox_cAreaNameQ.Location = new Point(0x31, 0x2a);
            this.textBox_cAreaNameQ.Name = "textBox_cAreaNameQ";
            this.textBox_cAreaNameQ.Size = new Size(0xdd, 0x15);
            this.textBox_cAreaNameQ.TabIndex = 6;
            this.textBox_cAreaNameQ.Tag = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 0x2d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "货区：";
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.panel_Edit.Controls.Add(this.label4);
            this.panel_Edit.Controls.Add(this.lblColor);
            this.panel_Edit.Controls.Add(this.label7);
            this.panel_Edit.Controls.Add(this.txt_cCmptId);
            this.panel_Edit.Controls.Add(this.cmb_bUsed);
            this.panel_Edit.Controls.Add(this.label6);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cAreaId);
            this.panel_Edit.Controls.Add(this.textBox_cAreaName);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(0x156, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x11b, 0x12f);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x26, 0xd9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 0x1f;
            this.label4.Text = "标识颜色";
            this.lblColor.BorderStyle = BorderStyle.FixedSingle;
            this.lblColor.Location = new Point(0x75, 0xd8);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new Size(0x21, 30);
            this.lblColor.TabIndex = 30;
            this.tlpMain.SetToolTip(this.lblColor, "在编辑状态，双击修改颜色");
            this.lblColor.DoubleClick += new EventHandler(this.lblColor_DoubleClick);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x26, 0xb2);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x1d;
            this.label7.Text = "单位编码";
            this.txt_cCmptId.Location = new Point(0x77, 0xad);
            this.txt_cCmptId.Name = "txt_cCmptId";
            this.txt_cCmptId.ReadOnly = true;
            this.txt_cCmptId.Size = new Size(100, 0x15);
            this.txt_cCmptId.TabIndex = 3;
            this.txt_cCmptId.Tag = "0";
            this.txt_cCmptId.ReadOnlyChanged += new EventHandler(this.textBox_cAreaId_ReadOnlyChanged);
            this.cmb_bUsed.CausesValidation = false;
            this.cmb_bUsed.FormattingEnabled = true;
            this.cmb_bUsed.Items.AddRange(new object[] { "不启用", "启用" });
            this.cmb_bUsed.Location = new Point(0x75, 0x83);
            this.cmb_bUsed.Name = "cmb_bUsed";
            this.cmb_bUsed.Size = new Size(100, 20);
            this.cmb_bUsed.TabIndex = 2;
            this.cmb_bUsed.Tag = "102";
            this.cmb_bUsed.Text = "Bind SelectedIndex";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x23, 0x86);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x1a;
            this.label6.Text = "是否起用";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x119);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x11b, 0x16);
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
            this.label2.Location = new Point(0x24, 0x65);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "货区名称";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x24, 0x35);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "货区代码";
            this.textBox_cAreaId.Location = new Point(0x75, 0x2c);
            this.textBox_cAreaId.Name = "textBox_cAreaId";
            this.textBox_cAreaId.ReadOnly = true;
            this.textBox_cAreaId.Size = new Size(100, 0x15);
            this.textBox_cAreaId.TabIndex = 0;
            this.textBox_cAreaId.Tag = "0";
            this.textBox_cAreaId.ReadOnlyChanged += new EventHandler(this.textBox_cAreaId_ReadOnlyChanged);
            this.textBox_cAreaName.Location = new Point(0x75, 0x5c);
            this.textBox_cAreaName.Name = "textBox_cAreaName";
            this.textBox_cAreaName.Size = new Size(100, 0x15);
            this.textBox_cAreaName.TabIndex = 1;
            this.textBox_cAreaName.Tag = "0";
            this.textBox_cAreaName.ReadOnlyChanged += new EventHandler(this.textBox_cAreaId_ReadOnlyChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x271, 0x148);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.KeyPreview = true;
            base.Name = "FrmStockAreaInfo";
            this.Text = "货位区域管理";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblColor_DoubleClick(object sender, EventArgs e)
        {
            if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
            {
                Color backColor = this.lblColor.BackColor;
                this.dlgColor.Color = backColor;
                if (this.dlgColor.ShowDialog() == DialogResult.OK)
                {
                    backColor = this.dlgColor.Color;
                    this.lblColor.BackColor = backColor;
                    int num = backColor.ToArgb();
                    DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                    if ((current != null) && (current.IsEdit || current.IsNew))
                    {
                        current["nColorValue"] = num;
                    }
                }
            }
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(1, "立体仓库"));
            list.Add(new DictionaryEntry(2, "平面仓库"));
            list.Add(new DictionaryEntry(3, "DPS仓库"));
            this.cmb_cWHId.DisplayMember = "Value";
            this.cmb_cWHId.ValueMember = "Key";
            this.cmb_cWHId.DataSource = list;
        }

        private void LoadStockList(string StockId)
        {
            string sErr = "";
            string sSql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')";
            }
            if (StockId.Trim() != "")
            {
                sSql = " and cWHId='" + StockId + "'";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cWHId.DataSource = dataBySql.Tables["data"];
            this.cmb_cWHId.DisplayMember = "cName";
            this.cmb_cWHId.ValueMember = "cWHId";
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox_cAreaId_ReadOnlyChanged(object sender, EventArgs e)
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

