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

    public class FrmStockPositInfo : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private DataGridViewTextBoxColumn cAreaId;
        private DataGridViewTextBoxColumn CCMPId;
        private ComboBox cmb_cAreaId;
        private ComboBox cmb_cMAreaId;
        private ComboBox cmb_cPalletSpec;
        private ComboBox cmb_cWHId;
        private ComboBox cmb_nStatusWork;
        private DataGridViewTextBoxColumn CMNo;
        private DataGridViewTextBoxColumn cName;
        private DataGridViewTextBoxColumn col_cPosId;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cPalletSpec;
        private DataGridViewTextBoxColumn cRemark;
        private DataGridView dataGridView_Main;
        private GroupBox grpCondition;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private DataGridViewTextBoxColumn nCol;
        private DataGridViewTextBoxColumn nDPSAddr;
        private DataGridViewTextBoxColumn nLayer;
        private DataGridViewTextBoxColumn nRow;
        private DataGridViewTextBoxColumn nStatusWork;
        private DataGridViewTextBoxColumn nType;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private ToolStripProgressBar prgState;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cPosId";
        private string strTbNameMain = "TWC_WareCell";
        private TextBox textBox_cMNo;
        private TextBox textBox_cPosId;
        private TextBox textBox_cRemark;
        private TextBox textBox_nCol;
        private TextBox textBox_nDPSAddr;
        private TextBox textBox_nLayer;
        private TextBox textBox_nPalletId;
        private TextBox textBox_nRow;
        private ToolStripButton tlb_AutoSetPltId;
        private ToolStripButton tlb_BatchUpdatePos;
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
        private TextBox txtQ_nCol;
        private TextBox txtQ_nLayer;
        private TextBox txtQ_nRow;
        private TextBox txtQ_PltId;

        public FrmStockPositInfo()
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
            sSql = "select * from  " + this.strTbNameMain + " " + SqlStrConditon;
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

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            if ((this.txtQ_nRow.Text.Trim() != "") && !FrmSTable.IsNumberic(this.txtQ_nRow.Text.Trim()))
            {
                MessageBox.Show("请录入正确的行号！");
                this.txtQ_nRow.Focus();
            }
            else if ((this.txtQ_nCol.Text.Trim() != "") && !FrmSTable.IsNumberic(this.txtQ_nCol.Text.Trim()))
            {
                MessageBox.Show("请录入正确的列号！");
                this.txtQ_nCol.Focus();
            }
            else if ((this.txtQ_nLayer.Text.Trim() != "") && !FrmSTable.IsNumberic(this.txtQ_nLayer.Text.Trim()))
            {
                MessageBox.Show("请录入正确的层号！");
                this.txtQ_nLayer.Focus();
            }
            else
            {
                this.DoRefresh();
            }
        }

        private void cmb_SelectedValue_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox_cAreaId_DropDown(object sender, EventArgs e)
        {
            this.LoadAreaList(this.cmb_cWHId.SelectedValue.ToString());
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
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
            this.OptMain = OperateType.optEdit;
            ((DataRowView) this.bindingSource_Main.Current).BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cPosId.ReadOnly = true;
            this.textBox_nCol.ReadOnly = true;
            this.textBox_nLayer.ReadOnly = true;
            this.textBox_nRow.ReadOnly = true;
            this.cmb_cPalletSpec.Enabled = false;
            return false;
        }

        public bool DoNew()
        {
            this.OptMain = OperateType.optNew;
            DataRowView view = (DataRowView) this.bindingSource_Main.AddNew();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.DisplayState(this.stbState, this.OptMain);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            return false;
        }

        private bool DoRefresh()
        {
            this.sbCondition.Remove(0, this.sbCondition.Length);
            this.sbCondition.Append(this.GetCondition());
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
                if (this.cmb_cPalletSpec.Text.Trim() == "")
                {
                    MessageBox.Show("托盘规格不能为空！");
                    this.cmb_cPalletSpec.Focus();
                    return false;
                }
                if (this.cmb_nStatusWork.Text.Trim() == "")
                {
                    MessageBox.Show("工作状态不能为空！");
                    this.cmb_nStatusWork.Focus();
                    return false;
                }
                this.UIToDataRowView(current, this.panel_Edit);
                if (current[this.strKeyFld].ToString() == "")
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 6, current["cWHId"].ToString());
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
                if (PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, "dLastDate", out sErr).Tables[0].Rows[0][0].ToString() == "0")
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
                base.DBDataSet.Tables[this.strTbNameMain].AcceptChanges();
                drv = (DataRowView) this.bindingSource_Main.Current;
                this.DataRowViewToUI(drv, this.panel_Edit);
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
            this.tlb_AutoSetPltId.Visible = this.tlbSaveSysRts.Visible;
            this.textBox_nPalletId.Enabled = this.tlbSaveSysRts.Visible;
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
            this.LoadCommboxItemByValue();
            this.LoadStockList("");
            this.LoadAreaList("");
            this.LoadPalletSpecList();
            this.LoadMgrAreaList();
            this.BandDataSet("", this.dataGridView_Main);
        }

        private string GetCondition()
        {
            StringBuilder builder = new StringBuilder("");
            if (base.UserInformation.UType == UserType.utSupervisor)
            {
                builder.Append(" where 1=1 ");
            }
            else
            {
                builder.Append(" where (isnull(cMAreaId,'M00') in (select distinct cMAreaId from TPB_UserMgrArea where  cUserId='" + base.UserInformation.UserId + "') or (isnull(cMAreaId,'M00') = 'M00'))");
            }
            if (this.cmb_cWHId.Text.Trim() != "")
            {
                object selectedValue = this.cmb_cWHId.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and cWHId='" + selectedValue.ToString().Trim() + "'");
                }
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(" and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            if (this.txtQ_nRow.Text.Trim() != "")
            {
                builder.Append(" and nRow=" + this.txtQ_nRow.Text.Trim());
            }
            if (this.txtQ_nCol.Text.Trim() != "")
            {
                builder.Append(" and nCol=" + this.txtQ_nCol.Text.Trim());
            }
            if (this.txtQ_nLayer.Text.Trim() != "")
            {
                builder.Append(" and nLayer=" + this.txtQ_nLayer.Text.Trim());
            }
            if (this.txtQ_PltId.Text.Trim() != "")
            {
                builder.Append(" and isnull(nPalletId,' ') ='" + this.txtQ_PltId.Text.Trim() + "'");
            }
            return builder.ToString();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockPositInfo));
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
            this.tlb_BatchUpdatePos = new ToolStripButton();
            this.tlb_M_Refresh = new ToolStripButton();
            this.tlb_M_Find = new ToolStripButton();
            this.tlb_M_Print = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.tlb_AutoSetPltId = new ToolStripButton();
            this.panel1 = new Panel();
            this.dataGridView_Main = new DataGridView();
            this.grpCondition = new GroupBox();
            this.txtQ_PltId = new TextBox();
            this.label14 = new Label();
            this.btn_Qry = new Button();
            this.txtQ_nRow = new TextBox();
            this.txtQ_nCol = new TextBox();
            this.txtQ_nLayer = new TextBox();
            this.label12 = new Label();
            this.label15 = new Label();
            this.label16 = new Label();
            this.cmb_cWHId = new ComboBox();
            this.label2 = new Label();
            this.bindingSource_Main = new BindingSource(this.components);
            this.panel_Edit = new Panel();
            this.label24 = new Label();
            this.label23 = new Label();
            this.label22 = new Label();
            this.label21 = new Label();
            this.label20 = new Label();
            this.label19 = new Label();
            this.label18 = new Label();
            this.cmb_cMAreaId = new ComboBox();
            this.label17 = new Label();
            this.cmb_cPalletSpec = new ComboBox();
            this.cmb_nStatusWork = new ComboBox();
            this.cmb_cAreaId = new ComboBox();
            this.textBox_cMNo = new TextBox();
            this.textBox_cRemark = new TextBox();
            this.label13 = new Label();
            this.label11 = new Label();
            this.textBox_nRow = new TextBox();
            this.textBox_nCol = new TextBox();
            this.textBox_nLayer = new TextBox();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.textBox_nDPSAddr = new TextBox();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.prgState = new ToolStripProgressBar();
            this.label3 = new Label();
            this.label1 = new Label();
            this.textBox_cPosId = new TextBox();
            this.textBox_nPalletId = new TextBox();
            this.col_cPosId = new DataGridViewTextBoxColumn();
            this.nType = new DataGridViewTextBoxColumn();
            this.cName = new DataGridViewTextBoxColumn();
            this.cAreaId = new DataGridViewTextBoxColumn();
            this.nStatusWork = new DataGridViewTextBoxColumn();
            this.nDPSAddr = new DataGridViewTextBoxColumn();
            this.nRow = new DataGridViewTextBoxColumn();
            this.nCol = new DataGridViewTextBoxColumn();
            this.nLayer = new DataGridViewTextBoxColumn();
            this.cRemark = new DataGridViewTextBoxColumn();
            this.cPalletSpec = new DataGridViewTextBoxColumn();
            this.CMNo = new DataGridViewTextBoxColumn();
            this.CCMPId = new DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.grpCondition.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            this.panel_Edit.SuspendLayout();
            this.stbMain.SuspendLayout();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_BatchUpdatePos, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, 
                this.toolStripSeparator7, this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts, this.tlb_AutoSetPltId
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
            this.tlb_BatchUpdatePos.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_BatchUpdatePos.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_BatchUpdatePos.ForeColor = SystemColors.ActiveCaption;
            this.tlb_BatchUpdatePos.Image = (Image) manager.GetObject("tlb_BatchUpdatePos.Image");
            this.tlb_BatchUpdatePos.ImageTransparentColor = Color.Magenta;
            this.tlb_BatchUpdatePos.Name = "tlb_BatchUpdatePos";
            this.tlb_BatchUpdatePos.Size = new Size(0x57, 0x16);
            this.tlb_BatchUpdatePos.Tag = "08";
            this.tlb_BatchUpdatePos.Text = "批量修改数据";
            this.tlb_BatchUpdatePos.Click += new EventHandler(this.tlb_BatchUpdatePos_Click);
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
            this.tlb_AutoSetPltId.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_AutoSetPltId.Image = (Image) manager.GetObject("tlb_AutoSetPltId.Image");
            this.tlb_AutoSetPltId.ImageTransparentColor = Color.Magenta;
            this.tlb_AutoSetPltId.Name = "tlb_AutoSetPltId";
            this.tlb_AutoSetPltId.Size = new Size(0x5d, 0x16);
            this.tlb_AutoSetPltId.Text = "自动设置托盘号";
            this.tlb_AutoSetPltId.ToolTipText = "自动批量设置托盘号";
            this.tlb_AutoSetPltId.Visible = false;
            this.tlb_AutoSetPltId.Click += new EventHandler(this.tlb_AutoSetPltId_Click);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.grpCondition);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x156, 0x1f7);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.col_cPosId, this.nType, this.cName, this.cAreaId, this.nStatusWork, this.nDPSAddr, this.nRow, this.nCol, this.nLayer, this.cRemark, this.cPalletSpec, this.CMNo, this.CCMPId });
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 0x3f);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x156, 440);
            this.dataGridView_Main.TabIndex = 11;
            this.dataGridView_Main.Tag = "8";
            this.grpCondition.Controls.Add(this.txtQ_PltId);
            this.grpCondition.Controls.Add(this.label14);
            this.grpCondition.Controls.Add(this.btn_Qry);
            this.grpCondition.Controls.Add(this.txtQ_nRow);
            this.grpCondition.Controls.Add(this.txtQ_nCol);
            this.grpCondition.Controls.Add(this.txtQ_nLayer);
            this.grpCondition.Controls.Add(this.label12);
            this.grpCondition.Controls.Add(this.label15);
            this.grpCondition.Controls.Add(this.label16);
            this.grpCondition.Controls.Add(this.cmb_cWHId);
            this.grpCondition.Controls.Add(this.label2);
            this.grpCondition.Dock = DockStyle.Top;
            this.grpCondition.Location = new Point(0, 0);
            this.grpCondition.Name = "grpCondition";
            this.grpCondition.Size = new Size(0x156, 0x3f);
            this.grpCondition.TabIndex = 10;
            this.grpCondition.TabStop = false;
            this.grpCondition.Text = "条件";
            this.txtQ_PltId.Location = new Point(210, 0x24);
            this.txtQ_PltId.Name = "txtQ_PltId";
            this.txtQ_PltId.Size = new Size(0x44, 0x15);
            this.txtQ_PltId.TabIndex = 0x30;
            this.txtQ_PltId.Tag = "0";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0xa8, 0x27);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x29, 12);
            this.label14.TabIndex = 0x2f;
            this.label14.Text = "托盘号";
            this.btn_Qry.Location = new Point(0x119, 0x24);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x37, 0x17);
            this.btn_Qry.TabIndex = 0x2e;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.txtQ_nRow.Location = new Point(0x24, 0x24);
            this.txtQ_nRow.Name = "txtQ_nRow";
            this.txtQ_nRow.Size = new Size(0x1c, 0x15);
            this.txtQ_nRow.TabIndex = 0x2d;
            this.txtQ_nRow.Tag = "0";
            this.txtQ_nCol.Location = new Point(0x56, 0x24);
            this.txtQ_nCol.Name = "txtQ_nCol";
            this.txtQ_nCol.Size = new Size(0x1c, 0x15);
            this.txtQ_nCol.TabIndex = 0x2c;
            this.txtQ_nCol.Tag = "0";
            this.txtQ_nLayer.Location = new Point(130, 0x24);
            this.txtQ_nLayer.Name = "txtQ_nLayer";
            this.txtQ_nLayer.Size = new Size(0x1c, 0x15);
            this.txtQ_nLayer.TabIndex = 0x2b;
            this.txtQ_nLayer.Tag = "0";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x73, 0x2b);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 12);
            this.label12.TabIndex = 0x2a;
            this.label12.Text = "层";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x42, 0x29);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x11, 12);
            this.label15.TabIndex = 0x29;
            this.label15.Text = "列";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(12, 0x24);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x11, 12);
            this.label16.TabIndex = 40;
            this.label16.Text = "行";
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0x23, 13);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(0xf5, 20);
            this.cmb_cWHId.TabIndex = 12;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.Text = "Bind SelectedValue";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "仓库";
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.panel_Edit.Controls.Add(this.label24);
            this.panel_Edit.Controls.Add(this.label23);
            this.panel_Edit.Controls.Add(this.label22);
            this.panel_Edit.Controls.Add(this.label21);
            this.panel_Edit.Controls.Add(this.label20);
            this.panel_Edit.Controls.Add(this.label19);
            this.panel_Edit.Controls.Add(this.label18);
            this.panel_Edit.Controls.Add(this.cmb_cMAreaId);
            this.panel_Edit.Controls.Add(this.label17);
            this.panel_Edit.Controls.Add(this.cmb_cPalletSpec);
            this.panel_Edit.Controls.Add(this.cmb_nStatusWork);
            this.panel_Edit.Controls.Add(this.cmb_cAreaId);
            this.panel_Edit.Controls.Add(this.textBox_cMNo);
            this.panel_Edit.Controls.Add(this.textBox_cRemark);
            this.panel_Edit.Controls.Add(this.label13);
            this.panel_Edit.Controls.Add(this.label11);
            this.panel_Edit.Controls.Add(this.textBox_nRow);
            this.panel_Edit.Controls.Add(this.textBox_nCol);
            this.panel_Edit.Controls.Add(this.textBox_nLayer);
            this.panel_Edit.Controls.Add(this.label10);
            this.panel_Edit.Controls.Add(this.label9);
            this.panel_Edit.Controls.Add(this.label8);
            this.panel_Edit.Controls.Add(this.label7);
            this.panel_Edit.Controls.Add(this.label6);
            this.panel_Edit.Controls.Add(this.label5);
            this.panel_Edit.Controls.Add(this.label4);
            this.panel_Edit.Controls.Add(this.textBox_nDPSAddr);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Controls.Add(this.label3);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cPosId);
            this.panel_Edit.Controls.Add(this.textBox_nPalletId);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(0x156, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x1b3, 0x1f7);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.label24.AutoSize = true;
            this.label24.ForeColor = Color.Red;
            this.label24.Location = new Point(0xf4, 0x11d);
            this.label24.Name = "label24";
            this.label24.Size = new Size(11, 12);
            this.label24.TabIndex = 0x33;
            this.label24.Text = "*";
            this.label23.AutoSize = true;
            this.label23.ForeColor = Color.Red;
            this.label23.Location = new Point(0xbf, 0xc9);
            this.label23.Name = "label23";
            this.label23.Size = new Size(11, 12);
            this.label23.TabIndex = 50;
            this.label23.Text = "*";
            this.label22.AutoSize = true;
            this.label22.ForeColor = Color.Red;
            this.label22.Location = new Point(0xbf, 0x73);
            this.label22.Name = "label22";
            this.label22.Size = new Size(11, 12);
            this.label22.TabIndex = 0x31;
            this.label22.Text = "*";
            this.label21.AutoSize = true;
            this.label21.ForeColor = Color.Red;
            this.label21.Location = new Point(0x18f, 0x49);
            this.label21.Name = "label21";
            this.label21.Size = new Size(11, 12);
            this.label21.TabIndex = 0x30;
            this.label21.Text = "*";
            this.label20.AutoSize = true;
            this.label20.ForeColor = Color.Red;
            this.label20.Location = new Point(0x152, 0x49);
            this.label20.Name = "label20";
            this.label20.Size = new Size(11, 12);
            this.label20.TabIndex = 0x2f;
            this.label20.Text = "*";
            this.label19.AutoSize = true;
            this.label19.ForeColor = Color.Red;
            this.label19.Location = new Point(270, 0x49);
            this.label19.Name = "label19";
            this.label19.Size = new Size(11, 12);
            this.label19.TabIndex = 0x2e;
            this.label19.Text = "*";
            this.label18.AutoSize = true;
            this.label18.ForeColor = Color.Red;
            this.label18.Location = new Point(0xc2, 0x48);
            this.label18.Name = "label18";
            this.label18.Size = new Size(11, 12);
            this.label18.TabIndex = 0x2d;
            this.label18.Text = "*";
            this.cmb_cMAreaId.Enabled = false;
            this.cmb_cMAreaId.FormattingEnabled = true;
            this.cmb_cMAreaId.Location = new Point(0x58, 0x11a);
            this.cmb_cMAreaId.Name = "cmb_cMAreaId";
            this.cmb_cMAreaId.Size = new Size(0x97, 20);
            this.cmb_cMAreaId.TabIndex = 0x2b;
            this.cmb_cMAreaId.Tag = "101";
            this.cmb_cMAreaId.Text = "Bind SelectedValue";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(10, 0x11e);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x4d, 12);
            this.label17.TabIndex = 0x2c;
            this.label17.Text = "管理货区编码";
            this.cmb_cPalletSpec.Enabled = false;
            this.cmb_cPalletSpec.FormattingEnabled = true;
            this.cmb_cPalletSpec.Location = new Point(0x58, 0x6f);
            this.cmb_cPalletSpec.Name = "cmb_cPalletSpec";
            this.cmb_cPalletSpec.Size = new Size(100, 20);
            this.cmb_cPalletSpec.TabIndex = 4;
            this.cmb_cPalletSpec.Tag = "101";
            this.cmb_cPalletSpec.Text = "Bind SelectedValue";
            this.cmb_nStatusWork.FormattingEnabled = true;
            this.cmb_nStatusWork.Location = new Point(0x58, 0xc6);
            this.cmb_nStatusWork.Name = "cmb_nStatusWork";
            this.cmb_nStatusWork.Size = new Size(100, 20);
            this.cmb_nStatusWork.TabIndex = 8;
            this.cmb_nStatusWork.Tag = "101";
            this.cmb_nStatusWork.Text = "Bind SelectedValue";
            this.cmb_cAreaId.FormattingEnabled = true;
            this.cmb_cAreaId.Location = new Point(0x129, 0x73);
            this.cmb_cAreaId.Name = "cmb_cAreaId";
            this.cmb_cAreaId.Size = new Size(100, 20);
            this.cmb_cAreaId.TabIndex = 5;
            this.cmb_cAreaId.Tag = "101";
            this.cmb_cAreaId.Text = "Bind SelectedValue";
            this.cmb_cAreaId.DropDown += new EventHandler(this.comboBox_cAreaId_DropDown);
            this.textBox_cMNo.Location = new Point(0x129, 0xc6);
            this.textBox_cMNo.Name = "textBox_cMNo";
            this.textBox_cMNo.Size = new Size(100, 0x15);
            this.textBox_cMNo.TabIndex = 9;
            this.textBox_cMNo.Tag = "0";
            this.textBox_cRemark.Location = new Point(0x57, 0xef);
            this.textBox_cRemark.Name = "textBox_cRemark";
            this.textBox_cRemark.Size = new Size(0x135, 0x15);
            this.textBox_cRemark.TabIndex = 10;
            this.textBox_cRemark.Tag = "0";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(230, 0xcb);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x2a;
            this.label13.Text = "物料代码";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(11, 0x73);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 40;
            this.label11.Text = "托盘规格";
            this.textBox_nRow.Enabled = false;
            this.textBox_nRow.Location = new Point(0xf1, 0x45);
            this.textBox_nRow.Name = "textBox_nRow";
            this.textBox_nRow.Size = new Size(0x1c, 0x15);
            this.textBox_nRow.TabIndex = 1;
            this.textBox_nRow.Tag = "0";
            this.textBox_nRow.ReadOnlyChanged += new EventHandler(this.textBox_nRow_ReadOnlyChanged);
            this.textBox_nCol.Enabled = false;
            this.textBox_nCol.Location = new Point(310, 0x45);
            this.textBox_nCol.Name = "textBox_nCol";
            this.textBox_nCol.Size = new Size(0x1c, 0x15);
            this.textBox_nCol.TabIndex = 2;
            this.textBox_nCol.Tag = "0";
            this.textBox_nCol.ReadOnlyChanged += new EventHandler(this.textBox_nRow_ReadOnlyChanged);
            this.textBox_nLayer.Enabled = false;
            this.textBox_nLayer.Location = new Point(0x171, 0x45);
            this.textBox_nLayer.Name = "textBox_nLayer";
            this.textBox_nLayer.Size = new Size(0x1c, 0x15);
            this.textBox_nLayer.TabIndex = 3;
            this.textBox_nLayer.Tag = "0";
            this.textBox_nLayer.ReadOnlyChanged += new EventHandler(this.textBox_nRow_ReadOnlyChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x15f, 0x4e);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x11, 12);
            this.label10.TabIndex = 0x24;
            this.label10.Text = "层";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x121, 0x4e);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 12);
            this.label9.TabIndex = 0x23;
            this.label9.Text = "列";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(11, 0xf5);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x22;
            this.label8.Text = "备    注";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xde, 0x4e);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 12);
            this.label7.TabIndex = 0x21;
            this.label7.Text = "行";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xe2, 0x77);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x20;
            this.label6.Text = "货区代码";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(230, 0x9f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2f, 12);
            this.label5.TabIndex = 0x1f;
            this.label5.Text = "DPS地址";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0xc9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "工作状态";
            this.textBox_nDPSAddr.Location = new Point(0x129, 0x9c);
            this.textBox_nDPSAddr.Name = "textBox_nDPSAddr";
            this.textBox_nDPSAddr.Size = new Size(100, 0x15);
            this.textBox_nDPSAddr.TabIndex = 7;
            this.textBox_nDPSAddr.Tag = "0";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime, this.prgState });
            this.stbMain.Location = new Point(0, 0x1e1);
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
            this.prgState.Name = "prgState";
            this.prgState.Size = new Size(200, 0x10);
            this.prgState.ToolTipText = "正在处理";
            this.prgState.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x9f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "托盘号码";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x4e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "货位代码";
            this.textBox_cPosId.Location = new Point(0x58, 0x45);
            this.textBox_cPosId.Name = "textBox_cPosId";
            this.textBox_cPosId.ReadOnly = true;
            this.textBox_cPosId.Size = new Size(100, 0x15);
            this.textBox_cPosId.TabIndex = 0;
            this.textBox_cPosId.Tag = "0";
            this.textBox_cPosId.ReadOnlyChanged += new EventHandler(this.textBox_nRow_ReadOnlyChanged);

            this.textBox_nPalletId.Location = new Point(0x58, 0x9c);
            this.textBox_nPalletId.Name = "textBox_nPalletId";
            this.textBox_nPalletId.Size = new Size(100, 0x15);
            this.textBox_nPalletId.TabIndex = 6;
            this.textBox_nPalletId.Tag = "0";
            this.col_cPosId.DataPropertyName = "cPosId";
            this.col_cPosId.HeaderText = "货位代码";
            this.col_cPosId.Name = "col_cPosId";
            this.col_cPosId.ReadOnly = true;
            this.nType.DataPropertyName = "nPalletId";
            this.nType.HeaderText = "托盘号码";
            this.nType.Name = "nType";
            this.nType.ReadOnly = true;
            this.cName.DataPropertyName = "cWHId";
            this.cName.HeaderText = "仓库代码";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cAreaId.DataPropertyName = "cAreaId";
            this.cAreaId.HeaderText = "货区代码";
            this.cAreaId.Name = "cAreaId";
            this.cAreaId.ReadOnly = true;
            this.nStatusWork.DataPropertyName = "nStatusWork";
            this.nStatusWork.HeaderText = "工作状态";
            this.nStatusWork.Name = "nStatusWork";
            this.nStatusWork.ReadOnly = true;
            this.nStatusWork.Resizable = DataGridViewTriState.True;
            this.nStatusWork.Width = 80;
            this.nDPSAddr.DataPropertyName = "nDPSAddr";
            this.nDPSAddr.HeaderText = "DPS地址";
            this.nDPSAddr.Name = "nDPSAddr";
            this.nDPSAddr.ReadOnly = true;
            this.nRow.DataPropertyName = "nRow";
            this.nRow.HeaderText = "行号";
            this.nRow.Name = "nRow";
            this.nRow.ReadOnly = true;
            this.nCol.DataPropertyName = "nCol";
            this.nCol.HeaderText = "列号";
            this.nCol.Name = "nCol";
            this.nCol.ReadOnly = true;
            this.nLayer.DataPropertyName = "nLayer";
            this.nLayer.HeaderText = "层号";
            this.nLayer.Name = "nLayer";
            this.nLayer.ReadOnly = true;
            this.cRemark.DataPropertyName = "cRemark";
            this.cRemark.HeaderText = "备注";
            this.cRemark.Name = "cRemark";
            this.cRemark.ReadOnly = true;
            this.cPalletSpec.DataPropertyName = "cPalletSpec";
            this.cPalletSpec.HeaderText = "托盘规格";
            this.cPalletSpec.Name = "cPalletSpec";
            this.cPalletSpec.ReadOnly = true;
            this.CMNo.DataPropertyName = "cMNo";
            this.CMNo.HeaderText = "物料代码";
            this.CMNo.Name = "CMNo";
            this.CMNo.ReadOnly = true;
            this.CCMPId.DataPropertyName = "cCmptId";
            this.CCMPId.HeaderText = "单位代码";
            this.CCMPId.Name = "CCMPId";
            this.CCMPId.ReadOnly = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x210);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.KeyPreview = true;
            base.Name = "FrmStockPositInfo";
            this.Text = "货位管理";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.grpCondition.ResumeLayout(false);
            this.grpCondition.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadAreaList(string StockId)
        {
            string sErr = "";
            string sSql = "select cAreaId,cAreaName from TWC_WArea ";
            if (StockId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + StockId + "'";
            }
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            this.cmb_cAreaId.DataSource = set.Tables["data"];
            this.cmb_cAreaId.DisplayMember = "cAreaName";
            this.cmb_cAreaId.ValueMember = "cAreaId";
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(0, "空闲"));
            list.Add(new DictionaryEntry(1, "预定"));
            list.Add(new DictionaryEntry(2, "工作"));
            list.Add(new DictionaryEntry(3, "暂锁"));
            list.Add(new DictionaryEntry(4, "禁用"));
            this.cmb_nStatusWork.DisplayMember = "Value";
            this.cmb_nStatusWork.ValueMember = "Key";
            this.cmb_nStatusWork.DataSource = list;
        }

        private void LoadMgrAreaList()
        {
            string sErr = "";
            string sSql = "select cMAreaId,cMAName,bUsed from TWC_MgrArea where bUsed=1";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            this.cmb_cMAreaId.DataSource = set.Tables["data"];
            this.cmb_cMAreaId.DisplayMember = "cMAName";
            this.cmb_cMAreaId.ValueMember = "cMAreaId";
        }

        private void LoadPalletSpecList()
        {
            string sErr = "";
            string sSql = "select cPalletSpecid, cPalletSpec from twc_palletspec ";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            this.cmb_cPalletSpec.DataSource = set.Tables["data"];
            this.cmb_cPalletSpec.DisplayMember = "cPalletSpec";
            this.cmb_cPalletSpec.ValueMember = "cPalletSpec";
        }

        private void LoadStockList(string sCmptId)
        {
            string sErr = "";
            string sSql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (sCmptId.Trim() != "")
            {
                sSql = sSql + " and  cCmptId='" + sCmptId + "'";
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')";
            }
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            this.cmb_cWHId.DataSource = set.Tables["data"];
            this.cmb_cWHId.DisplayMember = "cName";
            this.cmb_cWHId.ValueMember = "cWHId";
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox_nRow_ReadOnlyChanged(object sender, EventArgs e)
        {
            base.ChangeTextBoxBkColorByReadOnly(sender, this.panel_Edit.BackColor, Color.White);
        }

        private void tlb_AutoSetPltId_Click(object sender, EventArgs e)
        {
            DataTable table = null;
            DataTable table2 = null;
            string sErr = "";
            StringBuilder builder = new StringBuilder("");
            DataSet set = null;
            int num = 0;
            builder.Append("select * from TWC_WareCell where isnull(nPalletId,' ') in ('', ' ')");
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "PosList", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if ((set != null) && (set.Tables["PosList"] != null))
            {
                table2 = set.Tables["PosList"].Copy();
                set.Clear();
                builder.Remove(0, builder.Length);
                builder.Append("select plt.*,ps.cPalletSpec from TWC_PalletCell plt ");
                builder.Append(" inner join TWC_PalletSpec ps on plt.cPalletSpec=ps.cPalletSpecId where plt.nPalletId not in (select nPalletId from TWC_WareCell where isnull(nPalletId,' ') not in('', ' '))");
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "PltList", 0, 0, "", out sErr);
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else if ((set != null) && (set.Tables["PltList"] != null))
                {
                    table = set.Tables["PltList"].Copy();
                    this.prgState.Maximum = table2.Rows.Count;
                    this.prgState.Minimum = 0;
                    this.prgState.Value = 0;
                    this.prgState.ToolTipText = "正在批量设置货位托盘号...";
                    this.prgState.Visible = true;
                    foreach (DataRow row in table2.Rows)
                    {
                        string str2 = row["cPosId"].ToString();
                        table.Select("cPalletSpec='" + row["cPalletSpec"].ToString().Trim() + "'", "nPalletId asc");
                        DataRow row2 = null;
                        string str4 = "";
                        if (table.Rows.Count > 0)
                        {
                            row2 = table.Rows[0];
                            str4 = row2["nPalletId"].ToString();
                        }
                        if (str4.Trim() != "")
                        {
                            builder.Remove(0, builder.Length);
                            builder.Append("update TWC_WareCell set nPalletId='" + str4 + "' where cPosId='" + str2 + "'");
                            if (DBFuns.DoExecSql(base.AppInformation.SvrSocket, builder.ToString(), "", out sErr))
                            {
                                row2.Delete();
                                num++;
                                table.AcceptChanges();
                            }
                            else
                            {
                                MessageBox.Show(sErr);
                            }
                        }
                        this.prgState.Value++;
                    }
                    this.prgState.Visible = false;
                }
            }
        }

        private void tlb_BatchUpdatePos_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count != 0)
            {
                if (this.dataGridView_Main.SelectedRows.Count == 0)
                {
                    MessageBox.Show("对不起，没有选择需要修改的货位！");
                }
                else
                {
                    StringBuilder builder = new StringBuilder("");
                    frmBatchUpdatePosInfo info = new frmBatchUpdatePosInfo {
                        AppInformation = base.AppInformation,
                        UserInformation = base.UserInformation
                    };
                    if (this.cmb_cWHId.SelectedValue != null)
                    {
                        info.WHId = this.cmb_cWHId.SelectedValue.ToString();
                    }
                    info.ShowDialog();
                    if (info.bIsOK)
                    {
                        int count = this.dataGridView_Main.SelectedRows.Count;
                        this.prgState.Maximum = count;
                        this.prgState.Minimum = 0;
                        this.prgState.Value = 0;
                        this.prgState.Visible = true;
                        count = 0;
                        foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                        {
                            string sErr = "";
                            builder.Remove(0, builder.Length);
                            builder.Append(string.Format(info.SqlText, row.Cells["col_cPosId"].Value.ToString()));
                            if (DBFuns.DoExecSql(base.AppInformation.SvrSocket, builder.ToString(), "", out sErr))
                            {
                                count++;
                            }
                            this.prgState.Value++;
                        }
                        this.prgState.Visible = false;
                        MessageBox.Show("成功更新了：" + count.ToString() + " 条数据！");
                        if (count > 0)
                        {
                            this.btn_Qry_Click(null, null);
                        }
                    }
                    info.Dispose();
                }
            }
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
            Form_StockPositFilter filter = new Form_StockPositFilter {
                Icon = base.AppInformation.AppICON
            };
            filter.ShowDialog();
            if (filter.bIsOK)
            {
                this.DoRefresh();
            }
            filter.Dispose();
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

