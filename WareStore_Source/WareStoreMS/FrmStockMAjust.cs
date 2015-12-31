namespace WareStoreMS
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
    using Zqm.CommBase;
    using Zqm.DBCommInfo;

    public class FrmStockMAjust : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Detail;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private Button btnReset;
        private DataGridViewTextBoxColumn cBatchNo;
        private DataGridViewTextBoxColumn cBNo;
        private DataGridViewTextBoxColumn cBNoFrom;
        private DataGridViewTextBoxColumn cBoxId;
        private DataGridViewTextBoxColumn cChecker;
        private DataGridViewTextBoxColumn cLinkId;
        private ComboBox cmbQ_Status;
        private ComboBox cmbQ_WHId;
        private DataGridViewTextBoxColumn cMName;
        private DataGridViewTextBoxColumn cMNo;
        private DataGridViewTextBoxColumn colDtlcRemark;
        private ComboBox comboBox_cWHId;
        private ComboBox comboBox_nStatus;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cRemark;
        private DataGridViewTextBoxColumn cSpec;
        private DataGridViewTextBoxColumn cUnit;
        private DataGridViewTextBoxColumn cWHId;
        private DataGridView dataGridView_Detail;
        private DataGridView dataGridView_Main;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataSet DBDateSetDetail = null;
        private DataGridViewTextBoxColumn dCheckDate;
        private DataGridViewTextBoxColumn dDate;
        private DateTimePicker dtp_dDate;
        private DateTimePicker dtp_From;
        private DateTimePicker dtp_To;
        private DataGridViewTextBoxColumn fQty;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ToolStripMenuItem mi_Delete;
        private ToolStripMenuItem mi_DoAccount;
        private ToolStripMenuItem mi_DoAccountAll;
        private ToolStripMenuItem mi_Edit;
        private ToolStripMenuItem mi_New;
        private DataGridViewTextBoxColumn nBClass;
        private DataGridViewTextBoxColumn nPalletId;
        private DataGridViewTextBoxColumn nQCStatus;
        private DataGridViewTextBoxColumn nStatus;
        private DataGridViewTextBoxColumn nStatusD;
        private OperateType OptDetail = OperateType.optNone;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private Panel panel2;
        private ContextMenuStrip ppmDtl;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cBNo";
        private string strKeyFldDetail = "cBNo";
        private string strTbNameDetail = "TWB_BillAjustDtl";
        private string strTbNameMain = "TWB_BillAjust";
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox textBox;
        private TextBox textBox_cBNo;
        private TextBox textBox_cBNoFrom;
        private TextBox textBox_cUser;
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
        private ToolStripButton toolStripButton_Audit;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private TextBox txtQ_BNo;

        public FrmStockMAjust()
        {
            this.InitializeComponent();
        }

        private bool BandDataSet(string SqlStrConditon, DataGridView FDataGridView)
        {
            bool flag = true;
            try
            {
                string sSql = "";
                string sErr = "";
                this.bDSIsOpenForMain = false;
                FDataGridView.AutoGenerateColumns = false;
                FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                base.DBDataSet.Clear();
                sSql = "SELECT * FROM  " + this.strTbNameMain + " " + SqlStrConditon;
                Cursor.Current = Cursors.WaitCursor;
                base.DBDataSet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, "dDate,dCreateDate,dCheckDate", out sErr);
                flag = base.DBDataSet != null;
                this.bindingSource_Main.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
                FDataGridView.DataSource = this.bindingSource_Main;
                Cursor.Current = Cursors.Default;
                string str3 = "";
                if (this.bindingSource_Main.Count > 0)
                {
                    DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                    str3 = current[this.strKeyFld].ToString().Trim();
                    try
                    {
                        this.bDSIsOpenForMain = true;
                        this.DataRowViewToUI(current, this.panel_Edit);
                        this.OptMain = OperateType.optNone;
                    }
                    catch (Exception exception)
                    {
                        this.bDSIsOpenForMain = false;
                        MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        flag = false;
                    }
                }
                this.BandDataSetDetail(" where cBNo='" + str3.Trim() + "'", this.dataGridView_Detail);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message);
            }
            return flag;
        }

        private bool BandDataSetDetail(string SqlStrConditon, DataGridView FDataGridView)
        {
            try
            {
                bool flag = true;
                string sSql = "";
                string sErr = "";
                FDataGridView.AutoGenerateColumns = false;
                FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                sSql = "SELECT * FROM   " + this.strTbNameDetail + " " + SqlStrConditon;
                Cursor.Current = Cursors.WaitCursor;
                this.DBDateSetDetail = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameDetail, 0, 0, "", out sErr);
                flag = this.DBDateSetDetail != null;
                this.bindingSource_Detail.DataSource = this.DBDateSetDetail.Tables[this.strTbNameDetail];
                FDataGridView.DataSource = this.bindingSource_Detail;
                Cursor.Current = Cursors.Default;
                if (this.bindingSource_Detail.Count <= 0)
                {
                    flag = false;
                }
                return flag;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        private void bindingSource_Detail_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void bindingSource_Main_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDSIsOpenForMain)
            {
                this.ClearUIValues(this.panel_Edit);
                if (!((DataRowView) this.bindingSource_Main.Current).IsNew)
                {
                    DataRowView drv = (DataRowView) this.bindingSource_Main.Current;
                    this.DataRowViewToUI(drv, this.panel_Edit);
                }
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                this.BandDataSetDetail(" where cBNo='" + current["cBNo"] + "'", this.dataGridView_Detail);
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cmbQ_Status.SelectedIndex = -1;
            this.cmbQ_WHId.SelectedIndex = -1;
            this.txtQ_BNo.Text = "";
            this.dtp_To.Value = DateTime.Now;
            this.dtp_From.Value = DateTime.Now.AddDays(-30.0);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                if (int.Parse(current["nStatus"].ToString()) > 0)
                {
                    MessageBox.Show("对不起，该调整单已经开始过账，不能删除！");
                    return false;
                }
                bool flag2 = true;
                string sErr = "";
                string str2 = PubDBCommFuns.sp_Ajust_DeleteBillData(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", current["cBNo"].ToString().Trim(), out sErr);
                if (((str2.Trim() != "") && (str2.Trim() != "0")) && (sErr.Trim() != ""))
                {
                    MessageBox.Show(sErr);
                    return false;
                }
                if (flag2)
                {
                    this.OptMain = OperateType.optDelete;
                    this.DoRefresh();
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.OptMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.OptMain);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("删除成功！");
                }
            }
            return flag;
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
            return false;
        }

        private bool DoEditDetail()
        {
            if (this.dataGridView_Detail.RowCount < 2)
            {
                return true;
            }
            this.OptDetail = OperateType.optEdit;
            ((DataRowView) this.bindingSource_Detail.Current).BeginEdit();
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
            if (this.dtp_From.Value > this.dtp_To.Value)
            {
                MessageBox.Show("对不起，开始日期不能大于截止日期！");
                this.dtp_From.Focus();
                return false;
            }
            this.sbCondition.Remove(0, this.sbCondition.Length);
            this.sbCondition.Append(" where (dDate >='" + this.dtp_From.Value.ToString("yyyy-MM-dd 00:00:00") + "' and dDate <='" + this.dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") + "')");
            if (((this.cmbQ_WHId.Text.Trim() != "") && (this.cmbQ_WHId.Items.Count > 0)) && (this.cmbQ_WHId.SelectedValue != null))
            {
                this.sbCondition.Append(" and (cWHId='" + this.cmbQ_WHId.SelectedValue.ToString().Trim() + "')");
            }
            if (((this.cmbQ_Status.Text.Trim() != "") && (this.cmbQ_Status.Items.Count > 0)) && (this.cmbQ_Status.SelectedValue != null))
            {
                this.sbCondition.Append(" and (nStatus =" + this.cmbQ_Status.SelectedValue.ToString().Trim() + ")");
            }
            if (this.txtQ_BNo.Text.Trim() != "")
            {
                this.sbCondition.Append(" and ( cBNo like '%" + this.txtQ_BNo.Text.Trim() + "%')");
            }
            this.BandDataSet(this.sbCondition.ToString(), this.dataGridView_Main);
            return true;
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
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 5, base.UserInformation.UnitId);
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
                DataSet dataBySql = null;
                dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
                if (base.DBDataSet.Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optSave;
                    try
                    {
                        this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
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

        private bool DoSaveDetail()
        {
            if (this.dataGridView_Detail.RowCount < 2)
            {
                return true;
            }
            bool flag = false;
            string sSql = "";
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_Detail.Current;
            if (current.IsEdit || current.IsNew)
            {
                if (current[this.strKeyFld].ToString() == "")
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameDetail, this.strKeyFld, 5, base.UserInformation.UnitId);
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameDetail, this.strKeyFld, true);
                }
                else
                {
                    sSql = string.Concat(new object[] { 
                        "Update TWB_BillCheckDtl set fRQty=", current["fRQty"], ",fDiff=", current["fQty"], "-", current["fRQty"], " where cBNo='", current["cBNo"], "' and nPalletId='", current["nPalletId"], "' and cBoxId='", current["cBoxId"], "' and cMNo='", current["cMNo"], "' and cBatchNo='", current["cBatchNo"], 
                        "' and nQCStatus='", current["nQCStatus"], "'"
                     });
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                if (PubDBCommFuns.GetDataBySql(sSql, out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptDetail = OperateType.optSave;
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OptDetail = OperateType.optNone;
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
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
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
            this.CheckRights(this.tlbMain, set.Tables["UserRights"]);
            this.LoadCommboxItemByValue();
            this.LoadStockList("");
            this.LoadCheckType("");
            this.dtp_To.Value = DateTime.Now;
            this.dtp_From.Value = DateTime.Now.AddDays(-30.0);
            this.BandDataSet("", this.dataGridView_Main);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockMAjust));
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
            this.toolStripButton_Audit = new ToolStripButton();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.toolStripSeparator = new ToolStripSeparator();
            this.toolStripSeparator9 = new ToolStripSeparator();
            this.panel1 = new Panel();
            this.dataGridView_Main = new DataGridView();
            this.cBNo = new DataGridViewTextBoxColumn();
            this.nBClass = new DataGridViewTextBoxColumn();
            this.cWHId = new DataGridViewTextBoxColumn();
            this.dDate = new DataGridViewTextBoxColumn();
            this.cChecker = new DataGridViewTextBoxColumn();
            this.dCheckDate = new DataGridViewTextBoxColumn();
            this.cLinkId = new DataGridViewTextBoxColumn();
            this.cBNoFrom = new DataGridViewTextBoxColumn();
            this.nStatus = new DataGridViewTextBoxColumn();
            this.cRemark = new DataGridViewTextBoxColumn();
            this.panel2 = new Panel();
            this.label12 = new Label();
            this.btnReset = new Button();
            this.label7 = new Label();
            this.btn_Qry = new Button();
            this.dtp_From = new DateTimePicker();
            this.cmbQ_Status = new ComboBox();
            this.dtp_To = new DateTimePicker();
            this.label5 = new Label();
            this.txtQ_BNo = new TextBox();
            this.label4 = new Label();
            this.cmbQ_WHId = new ComboBox();
            this.panel_Edit = new Panel();
            this.dtp_dDate = new DateTimePicker();
            this.textBox = new TextBox();
            this.textBox_cBNoFrom = new TextBox();
            this.label6 = new Label();
            this.textBox_cUser = new TextBox();
            this.comboBox_nStatus = new ComboBox();
            this.comboBox_cWHId = new ComboBox();
            this.label11 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cBNo = new TextBox();
            this.ppmDtl = new ContextMenuStrip(this.components);
            this.mi_New = new ToolStripMenuItem();
            this.mi_Edit = new ToolStripMenuItem();
            this.mi_Delete = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripSeparator();
            this.mi_DoAccount = new ToolStripMenuItem();
            this.mi_DoAccountAll = new ToolStripMenuItem();
            this.bindingSource_Main = new BindingSource(this.components);
            this.bindingSource_Detail = new BindingSource(this.components);
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.dataGridView_Detail = new DataGridView();
            this.nPalletId = new DataGridViewTextBoxColumn();
            this.cMNo = new DataGridViewTextBoxColumn();
            this.cMName = new DataGridViewTextBoxColumn();
            this.cSpec = new DataGridViewTextBoxColumn();
            this.cUnit = new DataGridViewTextBoxColumn();
            this.cBatchNo = new DataGridViewTextBoxColumn();
            this.fQty = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.cBoxId = new DataGridViewTextBoxColumn();
            this.nQCStatus = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.nStatusD = new DataGridViewTextBoxColumn();
            this.colDtlcRemark = new DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.panel2.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.ppmDtl.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            ((ISupportInitialize) this.bindingSource_Detail).BeginInit();
            this.stbMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Detail).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.toolStripButton_Audit, this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts, this.toolStripSeparator, this.toolStripSeparator9
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x38f, 0x19);
            this.tlbMain.TabIndex = 15;
            this.tlbMain.Text = "toolStrip1";
            this.tlbMain.ItemClicked += new ToolStripItemClickedEventHandler(this.tlbMain_ItemClicked);
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
            this.tlb_M_New.Visible = false;
            this.tlb_M_New.Click += new EventHandler(this.tlb_M_New_Click);
            this.tlb_M_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = (Image) manager.GetObject("tlb_M_Edit.Image");
            this.tlb_M_Edit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new Size(0x23, 0x16);
            this.tlb_M_Edit.Text = "修改";
            this.tlb_M_Edit.Visible = false;
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
            this.tlb_M_Undo.Visible = false;
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
            this.tlb_M_Save.Visible = false;
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
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Visible = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 0x19);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new Size(6, 0x19);
            this.toolStripButton_Audit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Audit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripButton_Audit.ForeColor = SystemColors.ActiveCaption;
            this.toolStripButton_Audit.Image = (Image) manager.GetObject("toolStripButton_Audit.Image");
            this.toolStripButton_Audit.ImageTransparentColor = Color.Magenta;
            this.toolStripButton_Audit.Name = "toolStripButton_Audit";
            this.toolStripButton_Audit.Size = new Size(0x23, 0x16);
            this.toolStripButton_Audit.Text = "审核";
            this.toolStripButton_Audit.Click += new EventHandler(this.toolStripButton_Audit_Click);
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
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new Size(6, 0x19);
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new Size(6, 0x19);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x11d, 0x200);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.AllowUserToAddRows = false;
            this.dataGridView_Main.AllowUserToDeleteRows = false;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cBNo, this.nBClass, this.cWHId, this.dDate, this.cChecker, this.dCheckDate, this.cLinkId, this.cBNoFrom, this.nStatus, this.cRemark });
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 0x6c);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Main.Size = new Size(0x11d, 0x194);
            this.dataGridView_Main.TabIndex = 9;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            this.cBNo.DataPropertyName = "cBNo";
            this.cBNo.HeaderText = "调整单号";
            this.cBNo.Name = "cBNo";
            this.cBNo.ReadOnly = true;
            this.nBClass.DataPropertyName = "nBClass";
            this.nBClass.HeaderText = "作业类型";
            this.nBClass.Name = "nBClass";
            this.nBClass.ReadOnly = true;
            this.cWHId.DataPropertyName = "cWHId";
            this.cWHId.HeaderText = "盘点仓库";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            this.dDate.DataPropertyName = "dDate";
            this.dDate.HeaderText = "单据日期";
            this.dDate.Name = "dDate";
            this.dDate.ReadOnly = true;
            this.cChecker.DataPropertyName = "cUser";
            this.cChecker.HeaderText = "操作人员";
            this.cChecker.Name = "cChecker";
            this.cChecker.ReadOnly = true;
            this.dCheckDate.DataPropertyName = "dCreateDate";
            this.dCheckDate.HeaderText = "创建日期";
            this.dCheckDate.Name = "dCheckDate";
            this.dCheckDate.ReadOnly = true;
            this.cLinkId.DataPropertyName = "cCreateor";
            this.cLinkId.HeaderText = "单据人员";
            this.cLinkId.Name = "cLinkId";
            this.cLinkId.ReadOnly = true;
            this.cBNoFrom.DataPropertyName = "cBNoFrom";
            this.cBNoFrom.HeaderText = "单据来源";
            this.cBNoFrom.Name = "cBNoFrom";
            this.cBNoFrom.ReadOnly = true;
            this.nStatus.DataPropertyName = "nStatus";
            this.nStatus.HeaderText = "单据状态";
            this.nStatus.Name = "nStatus";
            this.nStatus.ReadOnly = true;
            this.cRemark.DataPropertyName = "cRemark";
            this.cRemark.HeaderText = "备注";
            this.cRemark.Name = "cRemark";
            this.cRemark.ReadOnly = true;
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
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x11d, 0x6c);
            this.panel2.TabIndex = 0x2c;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(8, 0x16);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x1d, 12);
            this.label12.TabIndex = 0x22;
            this.label12.Text = "仓库";
            this.btnReset.Location = new Point(0xde, 0x45);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(0x37, 0x17);
            this.btnReset.TabIndex = 0x2b;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 0x4a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 12);
            this.label7.TabIndex = 0x19;
            this.label7.Text = "单号";
            this.btn_Qry.Location = new Point(0x9d, 0x45);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x37, 0x17);
            this.btn_Qry.TabIndex = 0x2a;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.dtp_From.Location = new Point(0x2c, 0x2d);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new Size(0x6b, 0x15);
            this.dtp_From.TabIndex = 0x23;
            this.cmbQ_Status.FormattingEnabled = true;
            this.cmbQ_Status.Location = new Point(190, 0x12);
            this.cmbQ_Status.Name = "cmbQ_Status";
            this.cmbQ_Status.Size = new Size(0x57, 20);
            this.cmbQ_Status.TabIndex = 0x29;
            this.cmbQ_Status.Tag = "101";
            this.cmbQ_Status.Text = "Bind SelectedValue";
            this.dtp_To.Location = new Point(0xa1, 0x2d);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new Size(0x74, 0x15);
            this.dtp_To.TabIndex = 0x23;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(160, 0x16);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "状态";
            this.txtQ_BNo.Location = new Point(0x2c, 70);
            this.txtQ_BNo.Name = "txtQ_BNo";
            this.txtQ_BNo.Size = new Size(0x6b, 0x15);
            this.txtQ_BNo.TabIndex = 0x17;
            this.txtQ_BNo.Tag = "0";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x31);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x27;
            this.label4.Text = "日期";
            this.cmbQ_WHId.FormattingEnabled = true;
            this.cmbQ_WHId.Location = new Point(0x2c, 0x12);
            this.cmbQ_WHId.Name = "cmbQ_WHId";
            this.cmbQ_WHId.Size = new Size(0x6b, 20);
            this.cmbQ_WHId.TabIndex = 0x26;
            this.cmbQ_WHId.Tag = "101";
            this.cmbQ_WHId.Text = "Bind SelectedValue";
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
            this.panel_Edit.Dock = DockStyle.Top;
            this.panel_Edit.Location = new Point(0x11d, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x272, 0x6c);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.dtp_dDate.Location = new Point(0x4e, 0x2d);
            this.dtp_dDate.Name = "dtp_dDate";
            this.dtp_dDate.Size = new Size(0x72, 0x15);
            this.dtp_dDate.TabIndex = 0x2e;
            this.dtp_dDate.Tag = "2";
            this.textBox.Location = new Point(0x4e, 0x47);
            this.textBox.Name = "textBox";
            this.textBox.Size = new Size(0x1e1, 0x15);
            this.textBox.TabIndex = 0x2d;
            this.textBox.Tag = "0";
            this.textBox_cBNoFrom.Location = new Point(270, 0x2d);
            this.textBox_cBNoFrom.Name = "textBox_cBNoFrom";
            this.textBox_cBNoFrom.Size = new Size(100, 0x15);
            this.textBox_cBNoFrom.TabIndex = 0x2c;
            this.textBox_cBNoFrom.Tag = "0";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xd3, 0x31);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x2b;
            this.label6.Text = "来源单号";
            this.textBox_cUser.Location = new Point(0x1be, 0x2d);
            this.textBox_cUser.Name = "textBox_cUser";
            this.textBox_cUser.Size = new Size(0x71, 0x15);
            this.textBox_cUser.TabIndex = 0x29;
            this.textBox_cUser.Tag = "0";
            this.comboBox_nStatus.FormattingEnabled = true;
            this.comboBox_nStatus.Location = new Point(0x1be, 20);
            this.comboBox_nStatus.Name = "comboBox_nStatus";
            this.comboBox_nStatus.Size = new Size(0x71, 20);
            this.comboBox_nStatus.TabIndex = 0x27;
            this.comboBox_nStatus.Tag = "101";
            this.comboBox_nStatus.Text = "Bind SelectedValue";
            this.comboBox_cWHId.FormattingEnabled = true;
            this.comboBox_cWHId.Location = new Point(270, 20);
            this.comboBox_cWHId.Name = "comboBox_cWHId";
            this.comboBox_cWHId.Size = new Size(100, 20);
            this.comboBox_cWHId.TabIndex = 0x25;
            this.comboBox_cWHId.Tag = "101";
            this.comboBox_cWHId.Text = "Bind SelectedValue";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x182, 0x18);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x24;
            this.label11.Text = "单据状态";
            this.label11.Click += new EventHandler(this.label11_Click);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x13, 0x4b);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 0x23;
            this.label10.Text = "备　　注";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xd3, 0x18);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 0x22;
            this.label9.Text = "调整仓库";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x13, 0x31);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x21;
            this.label8.Text = "单据日期";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x182, 0x31);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x1b;
            this.label2.Text = "操作人员";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x19;
            this.label1.Text = "调整单号";
            this.textBox_cBNo.Location = new Point(0x4e, 20);
            this.textBox_cBNo.Name = "textBox_cBNo";
            this.textBox_cBNo.Size = new Size(0x71, 0x15);
            this.textBox_cBNo.TabIndex = 0x17;
            this.textBox_cBNo.Tag = "0";
            this.ppmDtl.Items.AddRange(new ToolStripItem[] { this.mi_New, this.mi_Edit, this.mi_Delete, this.toolStripMenuItem2, this.mi_DoAccount, this.mi_DoAccountAll });
            this.ppmDtl.Name = "contextMenuStrip1";
            this.ppmDtl.Size = new Size(0x77, 120);
            this.ppmDtl.Text = "下发任务";
            this.mi_New.Name = "mi_New";
            this.mi_New.Size = new Size(0x76, 0x16);
            this.mi_New.Text = "新建明细";
            this.mi_New.Click += new EventHandler(this.mi_New_Click);
            this.mi_Edit.Name = "mi_Edit";
            this.mi_Edit.Size = new Size(0x76, 0x16);
            this.mi_Edit.Text = "修改明细";
            this.mi_Edit.Click += new EventHandler(this.mi_Edit_Click);
            this.mi_Delete.Name = "mi_Delete";
            this.mi_Delete.Size = new Size(0x76, 0x16);
            this.mi_Delete.Text = "删除明细";
            this.mi_Delete.Click += new EventHandler(this.mi_Delete_Click);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(0x73, 6);
            this.mi_DoAccount.Name = "mi_DoAccount";
            this.mi_DoAccount.Size = new Size(0x76, 0x16);
            this.mi_DoAccount.Text = "过账";
            this.mi_DoAccount.Click += new EventHandler(this.mi_DoAccount_Click);
            this.mi_DoAccountAll.Name = "mi_DoAccountAll";
            this.mi_DoAccountAll.Size = new Size(0x76, 0x16);
            this.mi_DoAccountAll.Text = "整单过账";
            this.mi_DoAccountAll.Click += new EventHandler(this.mi_DoAccountAll_Click);
            this.bindingSource_Main.CurrentChanged += new EventHandler(this.bindingSource_Main_CurrentChanged);
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.bindingSource_Detail.CurrentChanged += new EventHandler(this.bindingSource_Detail_CurrentChanged);
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x219);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x38f, 0x16);
            this.stbMain.TabIndex = 0x12;
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
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0x11d, 0x85);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x272, 0x194);
            this.tabControl1.TabIndex = 0x13;
            this.tabPage1.Controls.Add(this.dataGridView_Detail);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x26a, 0x17b);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "调整单据";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.dataGridView_Detail.AllowUserToAddRows = false;
            this.dataGridView_Detail.Columns.AddRange(new DataGridViewColumn[] { this.nPalletId, this.cMNo, this.cMName, this.cSpec, this.cUnit, this.cBatchNo, this.fQty, this.dataGridViewTextBoxColumn2, this.cBoxId, this.nQCStatus, this.dataGridViewTextBoxColumn1, this.nStatusD, this.colDtlcRemark });
            this.dataGridView_Detail.ContextMenuStrip = this.ppmDtl;
            this.dataGridView_Detail.Dock = DockStyle.Fill;
            this.dataGridView_Detail.Location = new Point(3, 3);
            this.dataGridView_Detail.Name = "dataGridView_Detail";
            this.dataGridView_Detail.RowHeadersVisible = false;
            this.dataGridView_Detail.RowTemplate.Height = 0x17;
            this.dataGridView_Detail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Detail.Size = new Size(0x264, 0x175);
            this.dataGridView_Detail.TabIndex = 11;
            this.dataGridView_Detail.Tag = "8";
            this.nPalletId.DataPropertyName = "nPalletId";
            this.nPalletId.HeaderText = "托盘号码";
            this.nPalletId.Name = "nPalletId";
            this.nPalletId.Width = 0x4b;
            this.cMNo.DataPropertyName = "cMNo";
            this.cMNo.HeaderText = "物料编号";
            this.cMNo.Name = "cMNo";
            this.cMName.DataPropertyName = "cMName";
            this.cMName.HeaderText = "物料名称";
            this.cMName.Name = "cMName";
            this.cSpec.DataPropertyName = "cSpec";
            this.cSpec.HeaderText = "物料规格";
            this.cSpec.Name = "cSpec";
            this.cUnit.DataPropertyName = "cUnit";
            this.cUnit.FillWeight = 70f;
            this.cUnit.HeaderText = "物料单位";
            this.cUnit.Name = "cUnit";
            this.cUnit.ToolTipText = "物料单位";
            this.cUnit.Width = 0x41;
            this.cBatchNo.DataPropertyName = "cBatchNo";
            this.cBatchNo.HeaderText = "批次代码";
            this.cBatchNo.Name = "cBatchNo";
            this.cBatchNo.Width = 0x55;
            this.fQty.DataPropertyName = "fQty";
            this.fQty.HeaderText = "调整数量";
            this.fQty.Name = "fQty";
            this.dataGridViewTextBoxColumn2.DataPropertyName = "cWHId";
            this.dataGridViewTextBoxColumn2.FillWeight = 70f;
            this.dataGridViewTextBoxColumn2.HeaderText = "调整仓库";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "调整仓库";
            this.dataGridViewTextBoxColumn2.Width = 70;
            this.cBoxId.DataPropertyName = "cBoxId";
            this.cBoxId.HeaderText = "周转箱号";
            this.cBoxId.Name = "cBoxId";
            this.nQCStatus.DataPropertyName = "nQCStatus";
            this.nQCStatus.HeaderText = "QC状态";
            this.nQCStatus.Name = "nQCStatus";
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cBNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "调整单号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.nStatusD.DataPropertyName = "nStatus";
            this.nStatusD.HeaderText = "完成状态";
            this.nStatusD.Name = "nStatusD";
            this.colDtlcRemark.DataPropertyName = "cRemark";
            this.colDtlcRemark.HeaderText = "备注";
            this.colDtlcRemark.Name = "colDtlcRemark";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x38f, 0x22f);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.Controls.Add(this.stbMain);
            base.MinimizeBox = false;
            base.Name = "FrmStockMAjust";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "FrmStockMAjust";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.ppmDtl.ResumeLayout(false);
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            ((ISupportInitialize) this.bindingSource_Detail).EndInit();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Detail).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void LoadCheckType(string TypeId)
        {
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(0, "未过账"));
            list.Add(new DictionaryEntry(1, "部分过账"));
            list.Add(new DictionaryEntry(2, "过账完成"));
            this.comboBox_nStatus.DisplayMember = "Value";
            this.comboBox_nStatus.ValueMember = "Key";
            this.comboBox_nStatus.DataSource = list;
            ArrayList list2 = new ArrayList();
            list2.Add(new DictionaryEntry(0, "未过账"));
            list2.Add(new DictionaryEntry(1, "部分过账"));
            list2.Add(new DictionaryEntry(2, "过账完成"));
            this.cmbQ_Status.DisplayMember = "Value";
            this.cmbQ_Status.ValueMember = "Key";
            this.cmbQ_Status.DataSource = list2;
        }

        private void LoadStockList(string StockId)
        {
            string sErr = "";
            string sSql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (StockId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + StockId + "'";
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.comboBox_cWHId.DataSource = dataBySql.Tables["data"];
            this.comboBox_cWHId.DisplayMember = "cName";
            this.comboBox_cWHId.ValueMember = "cWHId";
            DataTable table = dataBySql.Tables["data"].Copy();
            this.cmbQ_WHId.DataSource = table;
            this.cmbQ_WHId.DisplayMember = "cName";
            this.cmbQ_WHId.ValueMember = "cWHId";
        }

        private void mi_Delete_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无调整单数据可删除！");
            }
            else if (int.Parse(current["nStatus"].ToString()) == 2)
            {
                MessageBox.Show("该调整单：" + current["cBNo"].ToString() + "已经过账，不能删除");
            }
            else if (this.bindingSource_Detail.Count == 0)
            {
                MessageBox.Show("无调整明细数据可删除！");
            }
            else
            {
                DataRowView view2 = (DataRowView) this.bindingSource_Detail.Current;
                if (view2 == null)
                {
                    MessageBox.Show("无调整明细数据可删除！");
                }
                else
                {
                    string sErr = "";
                    string pBNo = view2["cBNo"].ToString().Trim();
                    int pItem = int.Parse(view2["nItem"].ToString());
                    if (int.Parse(view2["nStatus"].ToString()) == 1)
                    {
                        MessageBox.Show("对不起，该明细已经过账，不能删除！");
                    }
                    else
                    {
                        string str3 = PubDBCommFuns.sp_Ajust_DeleteBillDtl(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", pBNo, pItem, out sErr);
                        if (((str3.Trim() != "") && (str3.Trim() != "0")) && (sErr.Trim() != ""))
                        {
                            MessageBox.Show(sErr);
                        }
                        else
                        {
                            this.BandDataSetDetail(" where cBNo='" + pBNo + "'", this.dataGridView_Detail);
                        }
                    }
                }
            }
        }

        private void mi_DoAccount_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无调整单数据可过账！");
            }
            else if (int.Parse(current["nStatus"].ToString()) == 2)
            {
                MessageBox.Show("该调整单：" + current["cBNo"].ToString() + "已经过账");
            }
            else if (this.bindingSource_Detail.Count == 0)
            {
                MessageBox.Show("无调整明细数据可过账！");
            }
            else
            {
                DataRowView view2 = (DataRowView) this.bindingSource_Detail.Current;
                if (view2 == null)
                {
                    MessageBox.Show("无调整明细数据可过账！");
                }
                else
                {
                    string sErr = "";
                    string pBNo = view2["cBNo"].ToString().Trim();
                    int pItem = int.Parse(view2["nItem"].ToString());
                    if (int.Parse(view2["nStatus"].ToString()) == 1)
                    {
                        MessageBox.Show("对不起，该明细已经过账！");
                    }
                    else
                    {
                        string str3 = PubDBCommFuns.sp_Chk_UpdtQtyFromAjust(base.AppInformation.SvrSocket, base.UserInformation.UnitId, base.UserInformation.UserName, "WMS", pBNo, pItem, out sErr);
                        if (((str3.Trim() != "") && (str3.Trim() != "0")) && (sErr.Trim() != ""))
                        {
                            MessageBox.Show(sErr);
                        }
                        else
                        {
                            this.BandDataSetDetail(" where cBNo='" + pBNo + "'", this.dataGridView_Detail);
                        }
                    }
                }
            }
        }

        private void mi_DoAccountAll_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无调整单数据可过账！");
            }
            else if (int.Parse(current["nStatus"].ToString()) == 2)
            {
                MessageBox.Show("该调整单：" + current["cBNo"].ToString() + "已经被审核");
            }
            else
            {
                string sErr = "";
                if (PubDBCommFuns.sp_Pack_BillCheck(base.AppInformation.SvrSocket, int.Parse(current["nBClass"].ToString()), current["cBNo"].ToString(), 0, base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", out sErr).Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show("审核成功！");
                    this.DoRefresh();
                }
            }
        }

        private void mi_Edit_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current != null)
            {
                if (int.Parse(current["nStatus"].ToString()) == 3)
                {
                    MessageBox.Show("对不起，该调整单已经被过账，不能增加明细数据！");
                }
                else
                {
                    string str = current["cWHId"].ToString().Trim();
                    string str2 = current["cBNo"].ToString().Trim();
                    if (str.Trim() == "")
                    {
                        MessageBox.Show("对不起，调整单的仓库不能为空！");
                    }
                    else if (((str2.Trim() == "") || (str2.Trim() == "-1")) || (str2.Trim() == "0"))
                    {
                        MessageBox.Show("对不起，请先建调整单并保存后，再建明细！");
                    }
                    else
                    {
                        DataRowView view2 = (DataRowView) this.bindingSource_Detail.Current;
                        frmDtlAjust ajust = new frmDtlAjust {
                            AppInformation = base.AppInformation,
                            UserInformation = base.UserInformation,
                            WHId = str.Trim(),
                            DrvItem = view2,
                            BIsNew = false
                        };
                        ajust.ShowDialog();
                        if (ajust.BIsResult)
                        {
                            this.BandDataSetDetail(" where cBNo='" + str2.Trim() + "'", this.dataGridView_Detail);
                        }
                        ajust.Dispose();
                    }
                }
            }
        }

        private void mi_New_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if (int.Parse(current["nStatus"].ToString()) == 3)
                    {
                        MessageBox.Show("对不起，该调整单已经被过账，不能增加明细数据！");
                    }
                    else
                    {
                        string str = current["cWHId"].ToString().Trim();
                        string str2 = current["cBNo"].ToString().Trim();
                        if (str.Trim() == "")
                        {
                            MessageBox.Show("对不起，调整单的仓库不能为空！");
                        }
                        else if (((str2.Trim() == "") || (str2.Trim() == "-1")) || (str2.Trim() == "0"))
                        {
                            MessageBox.Show("对不起，请先建调整单并保存后，再建明细！");
                        }
                        else
                        {
                            DataRowView view2 = (DataRowView) this.bindingSource_Detail.AddNew();
                            view2["cWHId"] = str;
                            view2["cBNo"] = str2;
                            view2["cUser"] = base.UserInformation.UserName;
                            frmDtlAjust ajust = new frmDtlAjust {
                                AppInformation = base.AppInformation,
                                UserInformation = base.UserInformation,
                                WHId = str.Trim(),
                                DrvItem = view2,
                                BIsNew = true
                            };
                            ajust.ShowDialog();
                            if (ajust.BIsResult)
                            {
                                this.BandDataSetDetail(" where cBNo='" + str2.Trim() + "'", this.dataGridView_Detail);
                            }
                            ajust.Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
            this.DoEditDetail();
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
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            this.DoSave();
            this.DoSaveDetail();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            this.DoUndo();
        }

        private void tlbMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
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

        private void toolStripButton_Audit_Click(object sender, EventArgs e)
        {
            this.mi_DoAccountAll_Click(null, null);
        }
    }
}

