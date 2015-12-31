namespace WareStoreMS
{
    using DataInFromMid;
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using WareStore;
    using WareStore.Rpts;
    using Zqm.CommBase;
    using Zqm.DBCommInfo;

    public class FrmStockMCheck : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bdsList;
        private BindingSource bindingSource_Detail;
        private BindingSource bindingSource_Main;
        private DataGridViewTextBoxColumn bIsChecked;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private Button btnReset;
        private Button button1;
        private DataGridViewTextBoxColumn cBNo;
        private DataGridViewTextBoxColumn cChecker;
        private DataGridViewTextBoxColumn cCheckType;
        private CheckBox chkDate;
        private DataGridViewTextBoxColumn cLinkId;
        private ComboBox cmbQ_CheckType;
        private ComboBox cmbQ_nState;
        private ComboBox cmbQ_Ware;
        private DataGridViewTextBoxColumn colcMName;
        private DataGridViewTextBoxColumn colDtlBatchNo;
        private DataGridViewTextBoxColumn colDtlcBNoIn;
        private DataGridViewTextBoxColumn colDtlcBoxId;
        private DataGridViewTextBoxColumn colDtlcUser;
        private DataGridViewTextBoxColumn colDtlfBad;
        private DataGridViewTextBoxColumn colDtlfDiff;
        private DataGridViewTextBoxColumn colDtlfQty;
        private DataGridViewTextBoxColumn colDtlfRQty;
        private DataGridViewTextBoxColumn colDtlMNo;
        private DataGridViewTextBoxColumn colDtlnItemIn;
        private DataGridViewTextBoxColumn colDtlPalletId;
        private DataGridViewTextBoxColumn colDtlPosId;
        private DataGridViewTextBoxColumn colDtlSpec;
        private DataGridViewTextBoxColumn colDtlUnit;
        private DataGridViewTextBoxColumn colfSysDiff;
        private DataGridViewTextBoxColumn colListBatchNo;
        private DataGridViewTextBoxColumn colListcSpec;
        private DataGridViewTextBoxColumn colListfDiff;
        private DataGridViewTextBoxColumn colListfQty;
        private DataGridViewTextBoxColumn colListMNo;
        private DataGridViewTextBoxColumn colListUnit;
        private DataGridViewTextBoxColumn colLstfBad;
        private DataGridViewTextBoxColumn colLstfRQty;
        private DataGridViewTextBoxColumn colnStatus;
        private ComboBox comboBox_bIsChecked;
        private ComboBox comboBox_cCheckType;
        private ComboBox comboBox_cWHId;
        private ComboBox comboBox_nStatus;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cRemark;
        private DataGridViewTextBoxColumn cUser;
        private DataGridViewTextBoxColumn cWHId;
        private DataGridView dataGridView_Main;
        private DateTimePicker dateTimePicker_From;
        private DateTimePicker dateTimePicker_To;
        private DataSet DBDateSetDetail = null;
        private DataSet DBDateSetList = null;
        private DataGridViewTextBoxColumn dCheckDate;
        private DataGridViewTextBoxColumn dDate;
        private DataGridViewTextBoxColumn fErpQty;
        private DataGridView grdDtl;
        private DataGridView grdList;
        private GroupBox groupBox1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label8;
        private Label label9;
        private ToolStripMenuItem mi_Print_ChkDiffList;
        private ToolStripMenuItem mi_Print_ChkMatList;
        private ToolStripMenuItem mi_Print_StkDtl;
        private ToolStripMenuItem miAddMatDtl;
        private ToolStripMenuItem miDoTask;
        private ToolStripMenuItem miRegBatchNoDiff;
        private ToolStripMenuItem miRegDtl;
        private DataGridViewTextBoxColumn nQCStatus;
        private DataGridViewTextBoxColumn nStatus;
        private DataGridViewTextBoxColumn nStatusD;
        private OperateType OptDetail = OperateType.optNone;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private ProgressBar pgDtl;
        private ContextMenuStrip ppmDtl;
        private ContextMenuStrip ppmPrint;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cBNo";
        private string strKeyFldDetail = "cBNo";
        private string strTbNameDetail = "TWB_BillCheckDtl";
        private string strTbNameList = "TWB_BillCheckList";
        private string strTbNameMain = "TWB_BillCheck";
        private TabControl tbcMain;
        private TabPage tbpChkDtl;
        private TabPage tbpChkList;
        private TextBox textBox_cBNo;
        private TextBox textBox_cBNoQ;
        private TextBox textBox_cCreator;
        private TextBox textBox_cLinkId;
        private ToolStripButton tlb_M_Ajust;
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_Edit;
        public ToolStripButton tlb_M_ErpImp;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Find;
        public ToolStripButton tlb_M_New;
        private ToolStripDropDownButton tlb_M_Print;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Save;
        public ToolStripButton tlb_M_Undo;
        public ToolStrip tlbMain;
        private ToolStripButton tlbSaveSysRts;
        private ToolStripButton toolStripButton_Audit;
        public ToolStripLabel toolStripLabel1;
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
        private TextBox txt_cRemark;
        private TextBox txt_dDate;

        public FrmStockMCheck()
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
            sSql = "SELECT * FROM  " + this.strTbNameMain + " " + SqlStrConditon;
            Cursor.Current = Cursors.WaitCursor;
            base.DBDataSet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, "dDate,dCreateDate,dCheckDate", out sErr);
            Cursor.Current = Cursors.Default;
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
            this.tbcMain_SelectedIndexChanged(null, null);
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
                this.DBDateSetDetail = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameDetail, 0, 0, out sErr);
                Cursor.Current = Cursors.Default;
                flag = this.DBDateSetDetail != null;
                this.bindingSource_Detail.DataSource = this.DBDateSetDetail.Tables[this.strTbNameDetail];
                FDataGridView.DataSource = this.bindingSource_Detail;
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

        private bool BandDataSetList(string SqlStrConditon, DataGridView FDataGridView)
        {
            try
            {
                bool flag = true;
                string sSql = "";
                string sErr = "";
                FDataGridView.AutoGenerateColumns = false;
                FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                sSql = "SELECT t.*,(isnull(t.fQty,0)-isnull(t.fErpQty,0)) fSysDiff,fRQty FROM  " + this.strTbNameList + " t " + SqlStrConditon;
                Cursor.Current = Cursors.WaitCursor;
                this.DBDateSetList = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameList, 0, 0, out sErr);
                Cursor.Current = Cursors.Default;
                flag = this.DBDateSetList != null;
                this.bdsList.DataSource = this.DBDateSetList.Tables[this.strTbNameList];
                FDataGridView.DataSource = this.bdsList;
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

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDSIsOpenForMain)
            {
                this.ClearUIValues(this.panel_Edit);
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if ((current != null) && !current.IsNew)
                {
                    this.DataRowViewToUI(current, this.panel_Edit);
                    this.tbcMain_SelectedIndexChanged(null, null);
                }
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.cmbQ_CheckType.SelectedIndex = -1;
            this.cmbQ_Ware.SelectedIndex = -1;
            this.textBox_cBNoQ.Text = "";
            this.chkDate.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSlackMatCount count = new frmSlackMatCount {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            count.ShowDialog();
            count.Dispose();
        }

        private void cmb_nType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_SelectedValue_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.bindingSource_Detail.Count != 0)
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    int num = int.Parse(current["nStatus"].ToString());
                    int num2 = int.Parse(current["bIsChecked"].ToString());
                    this.miDoTask.Enabled = (num < 3) && (num2 == 1);
                    this.miRegDtl.Enabled = (num < 3) && (num2 == 1);
                    this.miAddMatDtl.Enabled = (num < 3) && (num2 == 1);
                    this.miRegBatchNoDiff.Enabled = (num < 3) && (num2 == 1);
                }
            }
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
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无盘点单数据可删除！");
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
                if (int.Parse(current["nStatus"].ToString()) > 0)
                {
                    MessageBox.Show("对不起，该盘点单已经开始盘点，不能删除！");
                    return false;
                }
                bool flag2 = true;
                string sErr = "";
                string str2 = PubDBCommFuns.sp_Chk_DelChkB(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", current["cBNo"].ToString().Trim(), out sErr);
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
            if (this.grdList.RowCount < 2)
            {
                return true;
            }
            this.OptDetail = OperateType.optEdit;
            DataRowView current = (DataRowView) this.bindingSource_Detail.Current;
            if (current == null)
            {
                return false;
            }
            current.BeginEdit();
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
            this.sbCondition.Append(" where 1=1 ");
            if (this.textBox_cBNoQ.Text.Trim() != "")
            {
                this.sbCondition.Append(" and  cBNo like '%" + this.textBox_cBNoQ.Text.ToString().Trim() + "%'");
            }
            if ((this.cmbQ_Ware.Text.Trim() != "") && ((this.cmbQ_Ware.Items.Count > 0) && (this.cmbQ_Ware.SelectedValue != null)))
            {
                this.sbCondition.Append(" and  cWHId = '" + this.cmbQ_Ware.SelectedValue.ToString().Trim() + "'");
            }
            if ((this.cmbQ_nState.Text.Trim() != "") && (this.cmbQ_nState.SelectedValue != null))
            {
                this.sbCondition.Append(" and nStatus =" + this.cmbQ_nState.SelectedValue.ToString());
            }
            if ((this.cmbQ_CheckType.Text.Trim() != "") && ((this.cmbQ_CheckType.Items.Count > 0) && (this.cmbQ_CheckType.SelectedValue != null)))
            {
                this.sbCondition.Append(" and  cCheckType = '" + this.cmbQ_CheckType.SelectedValue.ToString().Trim() + "'");
            }
            if (this.chkDate.Checked)
            {
                if (this.dateTimePicker_From.Value > this.dateTimePicker_To.Value)
                {
                    MessageBox.Show("对不起，开始日期不能大于截止日期！");
                    this.dateTimePicker_From.Focus();
                    return false;
                }
                this.sbCondition = this.sbCondition.Append(" and  dDate between '" + this.dateTimePicker_From.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + this.dateTimePicker_To.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
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
                DataSet set = null;
                set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, sSql, "dDate,dCreateDate,dCheckDate", out sErr);
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
            if (this.grdList.RowCount < 2)
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
            if (base.UserInformation.UserName != "Admin5118")
            {
                this.CheckRights(this.tlbMain, set.Tables["UserRights"]);
            }
            this.LoadCommboxItemByValue();
            this.LoadStockList("");
            this.LoadCheckType("");
            this.dateTimePicker_From.Value = DateTime.Now.AddDays(-30.0);
            this.BandDataSet("", this.dataGridView_Main);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockMCheck));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            DataGridViewCellStyle style9 = new DataGridViewCellStyle();
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.tlb_M_ErpImp = new ToolStripButton();
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
            this.toolStripSeparator = new ToolStripSeparator();
            this.toolStripButton_Audit = new ToolStripButton();
            this.tlb_M_Ajust = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.tlb_M_Print = new ToolStripDropDownButton();
            this.ppmPrint = new ContextMenuStrip(this.components);
            this.mi_Print_ChkMatList = new ToolStripMenuItem();
            this.mi_Print_StkDtl = new ToolStripMenuItem();
            this.mi_Print_ChkDiffList = new ToolStripMenuItem();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.toolStripSeparator9 = new ToolStripSeparator();
            this.panel1 = new Panel();
            this.dataGridView_Main = new DataGridView();
            this.cBNo = new DataGridViewTextBoxColumn();
            this.cCheckType = new DataGridViewTextBoxColumn();
            this.cWHId = new DataGridViewTextBoxColumn();
            this.dDate = new DataGridViewTextBoxColumn();
            this.dCheckDate = new DataGridViewTextBoxColumn();
            this.cChecker = new DataGridViewTextBoxColumn();
            this.cLinkId = new DataGridViewTextBoxColumn();
            this.bIsChecked = new DataGridViewTextBoxColumn();
            this.nStatus = new DataGridViewTextBoxColumn();
            this.cRemark = new DataGridViewTextBoxColumn();
            this.groupBox1 = new GroupBox();
            this.cmbQ_nState = new ComboBox();
            this.label5 = new Label();
            this.dateTimePicker_From = new DateTimePicker();
            this.cmbQ_CheckType = new ComboBox();
            this.cmbQ_Ware = new ComboBox();
            this.btnReset = new Button();
            this.btn_Qry = new Button();
            this.chkDate = new CheckBox();
            this.dateTimePicker_To = new DateTimePicker();
            this.textBox_cBNoQ = new TextBox();
            this.label12 = new Label();
            this.label15 = new Label();
            this.label13 = new Label();
            this.panel_Edit = new Panel();
            this.button1 = new Button();
            this.txt_cRemark = new TextBox();
            this.label3 = new Label();
            this.comboBox_cCheckType = new ComboBox();
            this.comboBox_nStatus = new ComboBox();
            this.comboBox_bIsChecked = new ComboBox();
            this.comboBox_cWHId = new ComboBox();
            this.label11 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.label4 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cBNo = new TextBox();
            this.textBox_cLinkId = new TextBox();
            this.txt_dDate = new TextBox();
            this.textBox_cCreator = new TextBox();
            this.tbcMain = new TabControl();
            this.tbpChkList = new TabPage();
            this.grdList = new DataGridView();
            this.tbpChkDtl = new TabPage();
            this.grdDtl = new DataGridView();
            this.ppmDtl = new ContextMenuStrip(this.components);
            this.miDoTask = new ToolStripMenuItem();
            this.miRegDtl = new ToolStripMenuItem();
            this.miAddMatDtl = new ToolStripMenuItem();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.bindingSource_Main = new BindingSource(this.components);
            this.bindingSource_Detail = new BindingSource(this.components);
            this.bdsList = new BindingSource(this.components);
            this.miRegBatchNoDiff = new ToolStripMenuItem();
            this.colDtlPosId = new DataGridViewTextBoxColumn();
            this.colDtlPalletId = new DataGridViewTextBoxColumn();
            this.colDtlcBoxId = new DataGridViewTextBoxColumn();
            this.colDtlMNo = new DataGridViewTextBoxColumn();
            this.colDtlSpec = new DataGridViewTextBoxColumn();
            this.colDtlBatchNo = new DataGridViewTextBoxColumn();
            this.colDtlfQty = new DataGridViewTextBoxColumn();
            this.colDtlfRQty = new DataGridViewTextBoxColumn();
            this.colDtlfDiff = new DataGridViewTextBoxColumn();
            this.colDtlfBad = new DataGridViewTextBoxColumn();
            this.colDtlUnit = new DataGridViewTextBoxColumn();
            this.colnStatus = new DataGridViewTextBoxColumn();
            this.colDtlcUser = new DataGridViewTextBoxColumn();
            this.colDtlcBNoIn = new DataGridViewTextBoxColumn();
            this.colDtlnItemIn = new DataGridViewTextBoxColumn();
            this.pgDtl = new ProgressBar();
            this.colListMNo = new DataGridViewTextBoxColumn();
            this.colcMName = new DataGridViewTextBoxColumn();
            this.colListcSpec = new DataGridViewTextBoxColumn();
            this.colListBatchNo = new DataGridViewTextBoxColumn();
            this.colListfQty = new DataGridViewTextBoxColumn();
            this.colLstfRQty = new DataGridViewTextBoxColumn();
            this.colListfDiff = new DataGridViewTextBoxColumn();
            this.colLstfBad = new DataGridViewTextBoxColumn();
            this.nQCStatus = new DataGridViewTextBoxColumn();
            this.colListUnit = new DataGridViewTextBoxColumn();
            this.fErpQty = new DataGridViewTextBoxColumn();
            this.colfSysDiff = new DataGridViewTextBoxColumn();
            this.nStatusD = new DataGridViewTextBoxColumn();
            this.cUser = new DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.ppmPrint.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tbpChkList.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            this.tbpChkDtl.SuspendLayout();
            ((ISupportInitialize) this.grdDtl).BeginInit();
            this.ppmDtl.SuspendLayout();
            this.stbMain.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            ((ISupportInitialize) this.bindingSource_Detail).BeginInit();
            ((ISupportInitialize) this.bdsList).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.tlb_M_ErpImp, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.toolStripSeparator, this.toolStripButton_Audit, 
                this.tlb_M_Ajust, this.toolStripSeparator6, this.tlb_M_Print, this.toolStripSeparator7, this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts, this.toolStripSeparator9
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x429, 0x19);
            this.tlbMain.TabIndex = 15;
            this.tlbMain.Text = "toolStrip1";
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.tlb_M_ErpImp.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_ErpImp.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_ErpImp.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_ErpImp.Image = (Image) manager.GetObject("tlb_M_ErpImp.Image");
            this.tlb_M_ErpImp.ImageTransparentColor = Color.Magenta;
            this.tlb_M_ErpImp.Name = "tlb_M_ErpImp";
            this.tlb_M_ErpImp.Size = new Size(0x3d, 0x16);
            this.tlb_M_ErpImp.Tag = "20";
            this.tlb_M_ErpImp.Text = "导入数据";
            this.tlb_M_ErpImp.ToolTipText = "接口数据导入";
            this.tlb_M_ErpImp.Click += new EventHandler(this.tlb_M_ErpImp_Click);
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
            this.tlb_M_Undo.Tag = "03";
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
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new Size(6, 0x19);
            this.toolStripButton_Audit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Audit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripButton_Audit.ForeColor = SystemColors.ActiveCaption;
            this.toolStripButton_Audit.Image = (Image) manager.GetObject("toolStripButton_Audit.Image");
            this.toolStripButton_Audit.ImageTransparentColor = Color.Magenta;
            this.toolStripButton_Audit.Name = "toolStripButton_Audit";
            this.toolStripButton_Audit.Size = new Size(0x23, 0x16);
            this.toolStripButton_Audit.Tag = "07";
            this.toolStripButton_Audit.Text = "审核";
            this.toolStripButton_Audit.Click += new EventHandler(this.toolStripButton_Audit_Click);
            this.tlb_M_Ajust.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Ajust.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Ajust.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Ajust.Image = (Image) manager.GetObject("tlb_M_Ajust.Image");
            this.tlb_M_Ajust.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Ajust.Name = "tlb_M_Ajust";
            this.tlb_M_Ajust.Size = new Size(0x3d, 0x16);
            this.tlb_M_Ajust.Tag = "09";
            this.tlb_M_Ajust.Text = "盘点结束";
            this.tlb_M_Ajust.Click += new EventHandler(this.tlb_M_Ajust_Click);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 0x19);
            this.tlb_M_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.DropDown = this.ppmPrint;
            this.tlb_M_Print.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Print.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = (Image) manager.GetObject("tlb_M_Print.Image");
            this.tlb_M_Print.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new Size(0x2c, 0x16);
            this.tlb_M_Print.Tag = "06";
            this.tlb_M_Print.Text = "打印";
            this.ppmPrint.Items.AddRange(new ToolStripItem[] { this.mi_Print_ChkMatList, this.mi_Print_StkDtl, this.mi_Print_ChkDiffList });
            this.ppmPrint.Name = "ppmPrint";
            this.ppmPrint.Size = new Size(0xbd, 70);
            this.mi_Print_ChkMatList.Name = "mi_Print_ChkMatList";
            this.mi_Print_ChkMatList.ShowShortcutKeys = false;
            this.mi_Print_ChkMatList.Size = new Size(0xbc, 0x16);
            this.mi_Print_ChkMatList.Text = "打印盘点物料清单";
            this.mi_Print_ChkMatList.Click += new EventHandler(this.mi_Print_ChkMatList_Click);
            this.mi_Print_StkDtl.Name = "mi_Print_StkDtl";
            this.mi_Print_StkDtl.ShowShortcutKeys = false;
            this.mi_Print_StkDtl.Size = new Size(0xbc, 0x16);
            this.mi_Print_StkDtl.Text = "打印盘点库存明细清单";
            this.mi_Print_StkDtl.Click += new EventHandler(this.mi_Print_StkDtl_Click);
            this.mi_Print_ChkDiffList.Name = "mi_Print_ChkDiffList";
            this.mi_Print_ChkDiffList.ShowShortcutKeys = false;
            this.mi_Print_ChkDiffList.Size = new Size(0xbc, 0x16);
            this.mi_Print_ChkDiffList.Text = "打印盘点差异清单";
            this.mi_Print_ChkDiffList.Click += new EventHandler(this.mi_Print_ChkDiffList_Click);
            this.mi_Print_ChkDiffList.Visible = false;
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
            this.tlbSaveSysRts.Click += new EventHandler(this.tlbSaveSysRts_Click);
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new Size(6, 0x19);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x133, 0x1f6);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.AllowUserToAddRows = false;
            this.dataGridView_Main.AllowUserToDeleteRows = false;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.dataGridView_Main.ColumnHeadersDefaultCellStyle = style;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cBNo, this.cCheckType, this.cWHId, this.dDate, this.dCheckDate, this.cChecker, this.cLinkId, this.bIsChecked, this.nStatus, this.cRemark });
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = SystemColors.Window;
            style2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style2.ForeColor = SystemColors.ControlText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.False;
            this.dataGridView_Main.DefaultCellStyle = style2;
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 0x70);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = SystemColors.Control;
            style3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style3.ForeColor = SystemColors.WindowText;
            style3.SelectionBackColor = SystemColors.Highlight;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.True;
            this.dataGridView_Main.RowHeadersDefaultCellStyle = style3;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Main.Size = new Size(0x133, 390);
            this.dataGridView_Main.TabIndex = 11;
            this.dataGridView_Main.Tag = "8";
            this.cBNo.DataPropertyName = "cBNo";
            this.cBNo.HeaderText = "盘点单号";
            this.cBNo.Name = "cBNo";
            this.cBNo.ReadOnly = true;
            this.cCheckType.DataPropertyName = "cCheckType";
            this.cCheckType.HeaderText = "盘点类型";
            this.cCheckType.Name = "cCheckType";
            this.cCheckType.ReadOnly = true;
            this.cWHId.DataPropertyName = "cWHId";
            this.cWHId.HeaderText = "盘点仓库";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            this.dDate.DataPropertyName = "dDate";
            this.dDate.HeaderText = "单据日期";
            this.dDate.Name = "dDate";
            this.dDate.ReadOnly = true;
            this.dCheckDate.DataPropertyName = "dCheckDate";
            this.dCheckDate.HeaderText = "审核日期";
            this.dCheckDate.Name = "dCheckDate";
            this.dCheckDate.ReadOnly = true;
            this.cChecker.DataPropertyName = "cChecker";
            this.cChecker.HeaderText = "盘点人员";
            this.cChecker.Name = "cChecker";
            this.cChecker.ReadOnly = true;
            this.cLinkId.DataPropertyName = "cLinkId";
            this.cLinkId.HeaderText = "关联编码";
            this.cLinkId.Name = "cLinkId";
            this.cLinkId.ReadOnly = true;
            this.bIsChecked.DataPropertyName = "bIsChecked";
            this.bIsChecked.HeaderText = "是否审核";
            this.bIsChecked.Name = "bIsChecked";
            this.bIsChecked.ReadOnly = true;
            this.nStatus.DataPropertyName = "nStatus";
            this.nStatus.HeaderText = "单据状态";
            this.nStatus.Name = "nStatus";
            this.nStatus.ReadOnly = true;
            this.cRemark.DataPropertyName = "cRemark";
            this.cRemark.HeaderText = "备注";
            this.cRemark.Name = "cRemark";
            this.cRemark.ReadOnly = true;
            this.groupBox1.Controls.Add(this.cmbQ_nState);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dateTimePicker_From);
            this.groupBox1.Controls.Add(this.cmbQ_CheckType);
            this.groupBox1.Controls.Add(this.cmbQ_Ware);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btn_Qry);
            this.groupBox1.Controls.Add(this.chkDate);
            this.groupBox1.Controls.Add(this.dateTimePicker_To);
            this.groupBox1.Controls.Add(this.textBox_cBNoQ);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x133, 0x70);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.cmbQ_nState.FormattingEnabled = true;
            this.cmbQ_nState.Location = new Point(0x3f, 0x3d);
            this.cmbQ_nState.Name = "cmbQ_nState";
            this.cmbQ_nState.Size = new Size(0xef, 20);
            this.cmbQ_nState.TabIndex = 0x37;
            this.cmbQ_nState.Tag = "101";
            this.cmbQ_nState.Text = "Bind SelectedValue";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(4, 0x41);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x36;
            this.label5.Text = "单据状态";
            this.dateTimePicker_From.Location = new Point(0x33, 0x24);
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            this.dateTimePicker_From.Size = new Size(0x6f, 0x15);
            this.dateTimePicker_From.TabIndex = 0x2f;
            this.cmbQ_CheckType.FormattingEnabled = true;
            this.cmbQ_CheckType.Location = new Point(0xdb, 12);
            this.cmbQ_CheckType.Name = "cmbQ_CheckType";
            this.cmbQ_CheckType.Size = new Size(0x53, 20);
            this.cmbQ_CheckType.TabIndex = 0x35;
            this.cmbQ_CheckType.Tag = "101";
            this.cmbQ_CheckType.Text = "Bind SelectedValue";
            this.cmbQ_Ware.FormattingEnabled = true;
            this.cmbQ_Ware.Location = new Point(0x33, 12);
            this.cmbQ_Ware.Name = "cmbQ_Ware";
            this.cmbQ_Ware.Size = new Size(0x73, 20);
            this.cmbQ_Ware.TabIndex = 0x34;
            this.cmbQ_Ware.Tag = "101";
            this.cmbQ_Ware.Text = "Bind SelectedValue";
            this.btnReset.Location = new Point(0xeb, 0x57);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(0x43, 0x15);
            this.btnReset.TabIndex = 0x33;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            this.btn_Qry.Location = new Point(0xa8, 0x57);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x43, 0x15);
            this.btn_Qry.TabIndex = 50;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new Point(4, 0x26);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new Size(0x30, 0x10);
            this.chkDate.TabIndex = 0x31;
            this.chkDate.Text = "日期";
            this.chkDate.UseVisualStyleBackColor = true;
            this.dateTimePicker_To.Location = new Point(0xa7, 0x24);
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            this.dateTimePicker_To.Size = new Size(0x87, 0x15);
            this.dateTimePicker_To.TabIndex = 0x30;
            this.textBox_cBNoQ.Location = new Point(0x33, 0x57);
            this.textBox_cBNoQ.Name = "textBox_cBNoQ";
            this.textBox_cBNoQ.Size = new Size(0x6f, 0x15);
            this.textBox_cBNoQ.TabIndex = 0x2b;
            this.textBox_cBNoQ.Tag = "0";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(4, 0x5b);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x2f, 12);
            this.label12.TabIndex = 0x2c;
            this.label12.Text = "单   号";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0xa6, 0x10);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x35, 12);
            this.label15.TabIndex = 0x2e;
            this.label15.Text = "盘点类型";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(4, 0x10);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x1d, 12);
            this.label13.TabIndex = 0x2d;
            this.label13.Text = "仓库";
            this.panel_Edit.Controls.Add(this.button1);
            this.panel_Edit.Controls.Add(this.txt_cRemark);
            this.panel_Edit.Controls.Add(this.label3);
            this.panel_Edit.Controls.Add(this.comboBox_cCheckType);
            this.panel_Edit.Controls.Add(this.comboBox_nStatus);
            this.panel_Edit.Controls.Add(this.comboBox_bIsChecked);
            this.panel_Edit.Controls.Add(this.comboBox_cWHId);
            this.panel_Edit.Controls.Add(this.label11);
            this.panel_Edit.Controls.Add(this.label10);
            this.panel_Edit.Controls.Add(this.label9);
            this.panel_Edit.Controls.Add(this.label8);
            this.panel_Edit.Controls.Add(this.label7);
            this.panel_Edit.Controls.Add(this.label4);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cBNo);
            this.panel_Edit.Controls.Add(this.textBox_cLinkId);
            this.panel_Edit.Controls.Add(this.txt_dDate);
            this.panel_Edit.Controls.Add(this.textBox_cCreator);
            this.panel_Edit.Dock = DockStyle.Top;
            this.panel_Edit.Location = new Point(0x133, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x2f6, 0x73);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.button1.Location = new Point(7, 0x57);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.txt_cRemark.Location = new Point(0x4e, 0x48);
            this.txt_cRemark.Multiline = true;
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.ScrollBars = ScrollBars.Vertical;
            this.txt_cRemark.Size = new Size(0x216, 0x25);
            this.txt_cRemark.TabIndex = 0x2a;
            this.txt_cRemark.Tag = "0";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x13, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x29;
            this.label3.Text = "备注";
            this.comboBox_cCheckType.FormattingEnabled = true;
            this.comboBox_cCheckType.Location = new Point(0x4e, 0x1b);
            this.comboBox_cCheckType.Name = "comboBox_cCheckType";
            this.comboBox_cCheckType.Size = new Size(0x7e, 20);
            this.comboBox_cCheckType.TabIndex = 40;
            this.comboBox_cCheckType.Tag = "101";
            this.comboBox_cCheckType.Text = "Bind SelectedValue";
            this.comboBox_nStatus.FormattingEnabled = true;
            this.comboBox_nStatus.Location = new Point(0x1e4, 0x1b);
            this.comboBox_nStatus.Name = "comboBox_nStatus";
            this.comboBox_nStatus.Size = new Size(0x80, 20);
            this.comboBox_nStatus.TabIndex = 0x27;
            this.comboBox_nStatus.Tag = "101";
            this.comboBox_nStatus.Text = "Bind SelectedValue";
            this.comboBox_bIsChecked.FormattingEnabled = true;
            this.comboBox_bIsChecked.Location = new Point(0x4e, 50);
            this.comboBox_bIsChecked.Name = "comboBox_bIsChecked";
            this.comboBox_bIsChecked.Size = new Size(0x7e, 20);
            this.comboBox_bIsChecked.TabIndex = 0x26;
            this.comboBox_bIsChecked.Tag = "101";
            this.comboBox_bIsChecked.Text = "Bind SelectedValue";
            this.comboBox_cWHId.FormattingEnabled = true;
            this.comboBox_cWHId.Location = new Point(0x110, 5);
            this.comboBox_cWHId.Name = "comboBox_cWHId";
            this.comboBox_cWHId.Size = new Size(0x7e, 20);
            this.comboBox_cWHId.TabIndex = 0x25;
            this.comboBox_cWHId.Tag = "101";
            this.comboBox_cWHId.Text = "Bind SelectedValue";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x19f, 0x1f);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x24;
            this.label11.Text = "单据状态";
            this.label11.Click += new EventHandler(this.label11_Click);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x13, 0x1f);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 0x23;
            this.label10.Text = "盘点类型";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(210, 9);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 0x22;
            this.label9.Text = "盘点仓库";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(210, 0x1f);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x21;
            this.label8.Text = "单据日期";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x13, 0x36);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x20;
            this.label7.Text = "是否审核";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x19f, 9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 0x1d;
            this.label4.Text = "关联编号";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(210, 0x36);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x1b;
            this.label2.Text = "操作人员";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x19;
            this.label1.Text = "盘点单号";
            this.textBox_cBNo.Location = new Point(0x4e, 5);
            this.textBox_cBNo.Name = "textBox_cBNo";
            this.textBox_cBNo.Size = new Size(0x7e, 0x15);
            this.textBox_cBNo.TabIndex = 0x17;
            this.textBox_cBNo.Tag = "0";
            this.textBox_cLinkId.Location = new Point(0x1e4, 5);
            this.textBox_cLinkId.Name = "textBox_cLinkId";
            this.textBox_cLinkId.Size = new Size(0x80, 0x15);
            this.textBox_cLinkId.TabIndex = 20;
            this.textBox_cLinkId.Tag = "0";
            this.txt_dDate.Location = new Point(0x110, 0x1b);
            this.txt_dDate.Name = "txt_dDate";
            this.txt_dDate.Size = new Size(0x7e, 0x15);
            this.txt_dDate.TabIndex = 0x12;
            this.txt_dDate.Tag = "0";
            this.textBox_cCreator.Location = new Point(0x110, 50);
            this.textBox_cCreator.Name = "textBox_cCreator";
            this.textBox_cCreator.Size = new Size(0x80, 0x15);
            this.textBox_cCreator.TabIndex = 0x10;
            this.textBox_cCreator.Tag = "0";
            this.tbcMain.Controls.Add(this.tbpChkList);
            this.tbcMain.Controls.Add(this.tbpChkDtl);
            this.tbcMain.Dock = DockStyle.Left;
            this.tbcMain.Location = new Point(0x133, 140);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new Size(0x2ec, 0x183);
            this.tbcMain.TabIndex = 0x11;
            this.tbcMain.SelectedIndexChanged += new EventHandler(this.tbcMain_SelectedIndexChanged);
            this.tbpChkList.Controls.Add(this.grdList);
            this.tbpChkList.Location = new Point(4, 0x16);
            this.tbpChkList.Name = "tbpChkList";
            this.tbpChkList.Padding = new Padding(3);
            this.tbpChkList.Size = new Size(740, 0x169);
            this.tbpChkList.TabIndex = 0;
            this.tbpChkList.Text = "盘点物料清单";
            this.tbpChkList.UseVisualStyleBackColor = true;
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            style4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style4.BackColor = SystemColors.Control;
            style4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style4.ForeColor = SystemColors.WindowText;
            style4.SelectionBackColor = SystemColors.Highlight;
            style4.SelectionForeColor = SystemColors.HighlightText;
            style4.WrapMode = DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = style4;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colListMNo, this.colcMName, this.colListcSpec, this.colListBatchNo, this.colListfQty, this.colLstfRQty, this.colListfDiff, this.colLstfBad, this.nQCStatus, this.colListUnit, this.fErpQty, this.colfSysDiff, this.nStatusD, this.cUser });
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.BackColor = SystemColors.Window;
            style5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style5.ForeColor = SystemColors.ControlText;
            style5.SelectionBackColor = SystemColors.Highlight;
            style5.SelectionForeColor = SystemColors.HighlightText;
            style5.WrapMode = DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = style5;
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.Location = new Point(3, 3);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            style6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style6.BackColor = SystemColors.Control;
            style6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style6.ForeColor = SystemColors.WindowText;
            style6.SelectionBackColor = SystemColors.Highlight;
            style6.SelectionForeColor = SystemColors.HighlightText;
            style6.WrapMode = DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = style6;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 0x17;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new Size(0x2de, 0x163);
            this.grdList.TabIndex = 10;
            this.grdList.Tag = "8";
            this.grdList.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.tbpChkDtl.Controls.Add(this.pgDtl);
            this.tbpChkDtl.Controls.Add(this.grdDtl);
            this.tbpChkDtl.Location = new Point(4, 0x16);
            this.tbpChkDtl.Name = "tbpChkDtl";
            this.tbpChkDtl.Size = new Size(740, 0x169);
            this.tbpChkDtl.TabIndex = 1;
            this.tbpChkDtl.Text = "库存明细清单";
            this.tbpChkDtl.UseVisualStyleBackColor = true;
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.AllowUserToDeleteRows = false;
            style7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style7.BackColor = SystemColors.Control;
            style7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style7.ForeColor = SystemColors.WindowText;
            style7.SelectionBackColor = SystemColors.Highlight;
            style7.SelectionForeColor = SystemColors.HighlightText;
            style7.WrapMode = DataGridViewTriState.True;
            this.grdDtl.ColumnHeadersDefaultCellStyle = style7;
            this.grdDtl.Columns.AddRange(new DataGridViewColumn[] { this.colDtlPosId, this.colDtlPalletId, this.colDtlcBoxId, this.colDtlMNo, this.colDtlSpec, this.colDtlBatchNo, this.colDtlfQty, this.colDtlfRQty, this.colDtlfDiff, this.colDtlfBad, this.colDtlUnit, this.colnStatus, this.colDtlcUser, this.colDtlcBNoIn, this.colDtlnItemIn });
            this.grdDtl.ContextMenuStrip = this.ppmDtl;
            style8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style8.BackColor = SystemColors.Window;
            style8.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style8.ForeColor = SystemColors.ControlText;
            style8.SelectionBackColor = SystemColors.Highlight;
            style8.SelectionForeColor = SystemColors.HighlightText;
            style8.WrapMode = DataGridViewTriState.False;
            this.grdDtl.DefaultCellStyle = style8;
            this.grdDtl.Dock = DockStyle.Fill;
            this.grdDtl.Location = new Point(0, 0);
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            style9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style9.BackColor = SystemColors.Control;
            style9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style9.ForeColor = SystemColors.WindowText;
            style9.SelectionBackColor = SystemColors.Highlight;
            style9.SelectionForeColor = SystemColors.HighlightText;
            style9.WrapMode = DataGridViewTriState.True;
            this.grdDtl.RowHeadersDefaultCellStyle = style9;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 0x17;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdDtl.Size = new Size(740, 0x169);
            this.grdDtl.TabIndex = 11;
            this.grdDtl.Tag = "8";
            this.ppmDtl.Items.AddRange(new ToolStripItem[] { this.miDoTask, this.miRegDtl, this.miAddMatDtl, this.miRegBatchNoDiff });
            this.ppmDtl.Name = "contextMenuStrip1";
            this.ppmDtl.Size = new Size(0xa1, 0x5c);
            this.ppmDtl.Text = "下发任务";
            this.ppmDtl.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
            this.miDoTask.Name = "miDoTask";
            this.miDoTask.Size = new Size(160, 0x16);
            this.miDoTask.Text = "下发任务";
            this.miDoTask.Click += new EventHandler(this.miDoTask_Click);
            this.miRegDtl.Name = "miRegDtl";
            this.miRegDtl.Size = new Size(160, 0x16);
            this.miRegDtl.Text = "实盘登记";
            this.miRegDtl.Click += new EventHandler(this.miRegDtl_Click);
            this.miAddMatDtl.Name = "miAddMatDtl";
            this.miAddMatDtl.Size = new Size(160, 0x16);
            this.miAddMatDtl.Text = "增加物料";
            this.miAddMatDtl.Click += new EventHandler(this.miAddMatDtl_Click);
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0x41f, 0x1f9);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(10, 0x16);
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
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.bindingSource_Detail.CurrentChanged += new EventHandler(this.bindingSource_Detail_CurrentChanged);
            this.miRegBatchNoDiff.Name = "miRegBatchNoDiff";
            this.miRegBatchNoDiff.Size = new Size(160, 0x16);
            this.miRegBatchNoDiff.Text = "批量无差异登记";
            this.miRegBatchNoDiff.Click += new EventHandler(this.miRegBatchNoDiff_Click);
            this.colDtlPosId.DataPropertyName = "cPosId";
            this.colDtlPosId.HeaderText = "货位号";
            this.colDtlPosId.Name = "colDtlPosId";
            this.colDtlPosId.ReadOnly = true;
            this.colDtlPalletId.DataPropertyName = "nPalletId";
            this.colDtlPalletId.HeaderText = "托盘号";
            this.colDtlPalletId.Name = "colDtlPalletId";
            this.colDtlPalletId.ReadOnly = true;
            this.colDtlPalletId.Width = 0x4b;
            this.colDtlcBoxId.DataPropertyName = "cBoxId";
            this.colDtlcBoxId.HeaderText = "周转箱号";
            this.colDtlcBoxId.Name = "colDtlcBoxId";
            this.colDtlcBoxId.ReadOnly = true;
            this.colDtlcBoxId.Visible = false;
            this.colDtlMNo.DataPropertyName = "cMNo";
            this.colDtlMNo.HeaderText = "物料编号";
            this.colDtlMNo.Name = "colDtlMNo";
            this.colDtlMNo.ReadOnly = true;
            this.colDtlSpec.DataPropertyName = "cSpec";
            this.colDtlSpec.HeaderText = "物料规格";
            this.colDtlSpec.Name = "colDtlSpec";
            this.colDtlSpec.ReadOnly = true;
            this.colDtlBatchNo.DataPropertyName = "cBatchNo";
            this.colDtlBatchNo.HeaderText = "批次代码";
            this.colDtlBatchNo.Name = "colDtlBatchNo";
            this.colDtlBatchNo.ReadOnly = true;
            this.colDtlfQty.DataPropertyName = "fQty";
            this.colDtlfQty.HeaderText = "帐面数";
            this.colDtlfQty.Name = "colDtlfQty";
            this.colDtlfQty.ReadOnly = true;
            this.colDtlfQty.ToolTipText = "帐面数";
            this.colDtlfRQty.DataPropertyName = "fRQty";
            this.colDtlfRQty.HeaderText = "实盘有效数";
            this.colDtlfRQty.Name = "colDtlfRQty";
            this.colDtlfRQty.ReadOnly = true;
            this.colDtlfRQty.ToolTipText = "实盘有效数";
            this.colDtlfDiff.DataPropertyName = "fDiff";
            this.colDtlfDiff.HeaderText = "差异数";
            this.colDtlfDiff.Name = "colDtlfDiff";
            this.colDtlfDiff.ReadOnly = true;
            this.colDtlfBad.DataPropertyName = "fBad";
            this.colDtlfBad.HeaderText = "不良品数";
            this.colDtlfBad.Name = "colDtlfBad";
            this.colDtlfBad.ReadOnly = true;
            this.colDtlfBad.ToolTipText = "不良品数";
            this.colDtlUnit.DataPropertyName = "cUnit";
            this.colDtlUnit.HeaderText = "单位";
            this.colDtlUnit.Name = "colDtlUnit";
            this.colDtlUnit.ReadOnly = true;
            this.colnStatus.DataPropertyName = "nStatus";
            this.colnStatus.HeaderText = "完成状态";
            this.colnStatus.Name = "colnStatus";
            this.colnStatus.ReadOnly = true;
            this.colDtlcUser.DataPropertyName = "cUser";
            this.colDtlcUser.HeaderText = "操作人员";
            this.colDtlcUser.Name = "colDtlcUser";
            this.colDtlcUser.ReadOnly = true;
            this.colDtlcBNoIn.DataPropertyName = "cBNoIn";
            this.colDtlcBNoIn.HeaderText = "入库单号";
            this.colDtlcBNoIn.Name = "colDtlcBNoIn";
            this.colDtlcBNoIn.ReadOnly = true;
            this.colDtlnItemIn.DataPropertyName = "nItemIn";
            this.colDtlnItemIn.HeaderText = "入库单项号";
            this.colDtlnItemIn.Name = "colDtlnItemIn";
            this.colDtlnItemIn.ReadOnly = true;
            this.pgDtl.Location = new Point(0x4a, 0x86);
            this.pgDtl.Name = "pgDtl";
            this.pgDtl.Size = new Size(0x260, 0x17);
            this.pgDtl.TabIndex = 12;
            this.pgDtl.Visible = false;
            this.colListMNo.DataPropertyName = "cMNo";
            this.colListMNo.HeaderText = "物料编号";
            this.colListMNo.Name = "colListMNo";
            this.colListMNo.ReadOnly = true;
            this.colListMNo.Width = 80;
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            this.colListcSpec.DataPropertyName = "cSpec";
            this.colListcSpec.HeaderText = "物料规格";
            this.colListcSpec.Name = "colListcSpec";
            this.colListcSpec.ReadOnly = true;
            this.colListBatchNo.DataPropertyName = "cBatchNo";
            this.colListBatchNo.HeaderText = "批号";
            this.colListBatchNo.Name = "colListBatchNo";
            this.colListBatchNo.ReadOnly = true;
            this.colListBatchNo.Width = 0x41;
            this.colListfQty.DataPropertyName = "fQty";
            this.colListfQty.HeaderText = "帐面数";
            this.colListfQty.Name = "colListfQty";
            this.colListfQty.ReadOnly = true;
            this.colListfQty.ToolTipText = "WMS帐面数量";
            this.colListfQty.Width = 0x41;
            this.colLstfRQty.DataPropertyName = "fRQty";
            this.colLstfRQty.HeaderText = "实盘有效数";
            this.colLstfRQty.Name = "colLstfRQty";
            this.colLstfRQty.ReadOnly = true;
            this.colLstfRQty.ToolTipText = "WMS实际库存有效数量(等于盘点时的实际数量 + 中途的出入库数量)";
            this.colLstfRQty.Width = 0x41;
            this.colListfDiff.DataPropertyName = "fDiff";
            this.colListfDiff.HeaderText = "差异数";
            this.colListfDiff.Name = "colListfDiff";
            this.colListfDiff.ReadOnly = true;
            this.colListfDiff.ToolTipText = "WMS帐物差异数量";
            this.colListfDiff.Width = 0x41;
            this.colLstfBad.DataPropertyName = "fBad";
            this.colLstfBad.HeaderText = "不良品数";
            this.colLstfBad.Name = "colLstfBad";
            this.colLstfBad.ReadOnly = true;
            this.colLstfBad.ToolTipText = "不良品数";
            this.nQCStatus.DataPropertyName = "nQCStatus";
            this.nQCStatus.HeaderText = "质检状态";
            this.nQCStatus.Name = "nQCStatus";
            this.nQCStatus.ReadOnly = true;
            this.nQCStatus.Visible = false;
            this.nQCStatus.Width = 50;
            this.colListUnit.DataPropertyName = "cUnit";
            this.colListUnit.HeaderText = "单位";
            this.colListUnit.Name = "colListUnit";
            this.colListUnit.ReadOnly = true;
            this.colListUnit.Width = 0x41;
            this.fErpQty.DataPropertyName = "fErpQty";
            this.fErpQty.HeaderText = "ERP数";
            this.fErpQty.Name = "fErpQty";
            this.fErpQty.ReadOnly = true;
            this.fErpQty.Width = 0x41;
            this.colfSysDiff.DataPropertyName = "fSysDiff";
            this.colfSysDiff.HeaderText = "系统差异";
            this.colfSysDiff.Name = "colfSysDiff";
            this.colfSysDiff.ReadOnly = true;
            this.colfSysDiff.ToolTipText = "WMS与ERP系统的账面差异数量";
            this.nStatusD.DataPropertyName = "nStatus";
            this.nStatusD.HeaderText = "状态";
            this.nStatusD.Name = "nStatusD";
            this.nStatusD.ReadOnly = true;
            this.cUser.DataPropertyName = "cUser";
            this.cUser.HeaderText = "操作员";
            this.cUser.Name = "cUser";
            this.cUser.ReadOnly = true;
            this.cUser.Width = 70;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x429, 0x20f);
            base.Controls.Add(this.stbMain);
            base.Controls.Add(this.tbcMain);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.MinimizeBox = false;
            base.Name = "FrmStockMCheck";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "FrmStockMCheck";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.ppmPrint.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.tbcMain.ResumeLayout(false);
            this.tbpChkList.ResumeLayout(false);
            ((ISupportInitialize) this.grdList).EndInit();
            this.tbpChkDtl.ResumeLayout(false);
            ((ISupportInitialize) this.grdDtl).EndInit();
            this.ppmDtl.ResumeLayout(false);
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            ((ISupportInitialize) this.bindingSource_Detail).EndInit();
            ((ISupportInitialize) this.bdsList).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void LoadCheckType(string TypeId)
        {
            string sErr = "";
            string sSql = "select cBTypeId,cBType  from  TPB_BillType where  nBClass=3";
            if (TypeId.Trim() != "")
            {
                sSql = sSql + " where cBTypeId='" + TypeId + "'";
            }
            DataTable table = PubDBCommFuns.GetDataBySql(sSql, out sErr).Tables["data"];
            this.comboBox_cCheckType.DataSource = table;
            this.comboBox_cCheckType.DisplayMember = "cBType";
            this.comboBox_cCheckType.ValueMember = "cBTypeId";
            DataTable table2 = table.Copy();
            this.cmbQ_CheckType.DataSource = table2;
            this.cmbQ_CheckType.DisplayMember = "cBType";
            this.cmbQ_CheckType.ValueMember = "cBTypeId";
            this.cmbQ_CheckType.SelectedIndex = -1;
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            list.Add(new DictionaryEntry(0, "未盘点"));
            list.Add(new DictionaryEntry(1, "盘点中"));
            list.Add(new DictionaryEntry(2, "盘点登记完成"));
            list.Add(new DictionaryEntry(3, "盘点结束"));
            list2.Add(new DictionaryEntry(0, "未盘点"));
            list2.Add(new DictionaryEntry(1, "盘点中"));
            list2.Add(new DictionaryEntry(2, "盘点登记完成"));
            list2.Add(new DictionaryEntry(3, "盘点结束"));
            this.comboBox_nStatus.DisplayMember = "Value";
            this.comboBox_nStatus.ValueMember = "Key";
            this.comboBox_nStatus.DataSource = list;
            this.cmbQ_nState.DisplayMember = "Value";
            this.cmbQ_nState.ValueMember = "Key";
            this.cmbQ_nState.DataSource = list2;
            this.cmbQ_nState.SelectedValue = 0;
            ArrayList list3 = new ArrayList();
            list3.Add(new DictionaryEntry(0, "未审核"));
            list3.Add(new DictionaryEntry(1, "已审核"));
            this.comboBox_bIsChecked.DisplayMember = "Value";
            this.comboBox_bIsChecked.ValueMember = "Key";
            this.comboBox_bIsChecked.DataSource = list3;
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
            DataTable table = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr).Tables["data"];
            this.comboBox_cWHId.DataSource = table;
            this.comboBox_cWHId.DisplayMember = "cName";
            this.comboBox_cWHId.ValueMember = "cWHId";
            DataTable table2 = table.Copy();
            this.cmbQ_Ware.DataSource = table2;
            this.cmbQ_Ware.DisplayMember = "cName";
            this.cmbQ_Ware.ValueMember = "cWHId";
            this.cmbQ_Ware.SelectedIndex = -1;
        }

        private void mi_Print_ChkDiffList_Click(object sender, EventArgs e)
        {
        }

        private void mi_Print_ChkMatList_Click(object sender, EventArgs e)
        {
            string sSql = "select bil.cBNo,bil.cWHId,bil.dDate,case bil.bIsChecked when 0 then '未审核' else '已审核' end cIsChecked, case bil.bIsFinished when 0 then '盘点未结束' else '盘点已结束' end cIsFinished,bil.cCreator cUser,bil.cRemark,  bil.cBNoAjust,bil.cBNoBad, bt.cBType from TWB_BillCheck bil left join TPB_BillType bt on bil.cCheckType=bt.cBTypeId ";
            string str2 = "select cMNo,cMName,cSpec,cBatchNo,case nQCStatus when -1 then '不合格' else '合格' end cQCStatus, fQty,fDiff,fBad,fErpQty,cUnit from TWB_BillCheckList";
            string str3 = "";
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("对不起，无盘点单数据可打印！");
            }
            else
            {
                DataRowView current = null;
                current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    string sErr = "";
                    str3 = current["cBNo"].ToString();
                    sSql = sSql + " where bil.cBNo='" + str3 + "'";
                    str2 = str2 + " where cBNo='" + str3 + "'";
                    DataSet set = null;
                    set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "tbBillCheck", 0, 0, "", out sErr);
                    if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                    {
                        MessageBox.Show(sErr);
                    }
                    else
                    {
                        DataTable table = null;
                        if (set.Tables["tbBillCheck"] != null)
                        {
                            table = set.Tables["tbBillCheck"].Copy();
                        }
                        if (table != null)
                        {
                            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, str2, "tbBillCheckList", 0, 0, "", out sErr);
                            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                            {
                                MessageBox.Show(sErr);
                            }
                            else
                            {
                                DataTable table2 = null;
                                if (set.Tables["tbBillCheckList"] != null)
                                {
                                    table2 = set.Tables["tbBillCheckList"].Copy();
                                }
                                if (table2 != null)
                                {
                                    set.Clear();
                                    DataSet set2 = new DataSet();
                                    set2.Tables.Add(table);
                                    set2.Tables.Add(table2);
                                    rptCheckList list = new rptCheckList();
                                    frmRptViewer viewer = new frmRptViewer {
                                        RptObj = list
                                    };
                                    list.SetDataSource(set2);
                                    viewer.RptTitle = "盘点物料清单报表";
                                    viewer.SetReport();
                                    viewer.ShowDialog();
                                    viewer.Dispose();
                                    list.Dispose();
                                    set2.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void mi_Print_StkDtl_Click(object sender, EventArgs e)
        {
            string sSql = "select bil.cBNo,bil.cWHId,bil.dDate,case bil.bIsChecked when 0 then '未审核' else '已审核' end cIsChecked, case bil.bIsFinished when 0 then '盘点未结束' else '盘点已结束' end cIsFinished,bil.cCreator cUser,bil.cRemark,  bil.cBNoAjust,bil.cBNoBad, bt.cBType from TWB_BillCheck bil left join TPB_BillType bt on bil.cCheckType=bt.cBTypeId ";
            string str2 = "select cPosId,nPalletId,cBoxId,cMNo,cMName,cSpec,cBatchNo,case nQCStatus when -1 then '不合格' else '合格' end cQCStatus, fQty,fDiff,fBad,cUnit,cBNoIn,nItemIn from TWB_BillCheckDtl ";
            string str3 = "";
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("对不起，无盘点单数据可打印！");
            }
            else
            {
                DataRowView current = null;
                current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    string sErr = "";
                    str3 = current["cBNo"].ToString();
                    sSql = sSql + " where bil.cBNo='" + str3 + "'";
                    str2 = str2 + " where cBNo='" + str3 + "'";
                    DataSet set = null;
                    set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "tbBillCheck", 0, 0, "", out sErr);
                    if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                    {
                        MessageBox.Show(sErr);
                    }
                    else
                    {
                        DataTable table = null;
                        if (set.Tables["tbBillCheck"] != null)
                        {
                            table = set.Tables["tbBillCheck"].Copy();
                        }
                        if (table != null)
                        {
                            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, str2, "tbBillCheckDtl", 0, 0, "", out sErr);
                            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                            {
                                MessageBox.Show(sErr);
                            }
                            else
                            {
                                DataTable table2 = null;
                                if (set.Tables["tbBillCheckDtl"] != null)
                                {
                                    table2 = set.Tables["tbBillCheckDtl"].Copy();
                                }
                                if (table2 != null)
                                {
                                    set.Clear();
                                    DataSet set2 = new DataSet();
                                    set2.Tables.Add(table);
                                    set2.Tables.Add(table2);
                                    rptCheckDtl dtl = new rptCheckDtl();
                                    frmRptViewer viewer = new frmRptViewer {
                                        RptObj = dtl
                                    };
                                    dtl.SetDataSource(set2);
                                    viewer.RptTitle = "盘点库存明细清单报表";
                                    viewer.SetReport();
                                    viewer.ShowDialog();
                                    viewer.Dispose();
                                    dtl.Dispose();
                                    set2.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void miAddMatDtl_Click(object sender, EventArgs e)
        {
            int num = 2;
            try
            {
                DataRowView current = (DataRowView) this.bindingSource_Detail.Current;
                DataRowView view2 = (DataRowView) this.bindingSource_Main.Current;
                if (current == null)
                {
                    MessageBox.Show("无盘点库存明细数据！");
                }
                else
                {
                    num = int.Parse(current["nStatus"].ToString());
                    frmChkDtlWrite write = new frmChkDtlWrite {
                        AppInformation = base.AppInformation,
                        UserInformation = base.UserInformation,
                        IsNewAddMat = true,
                        WHId = view2["cWHId"].ToString().Trim(),
                        CheckNo = view2["cBNo"].ToString().Trim(),
                        PosId = current["cPosId"].ToString().Trim(),
                        PalletId = current["nPalletId"].ToString().Trim(),
                        BoxId = current["cBoxId"].ToString().Trim(),
                        MNo = "",
                        MName = "",
                        BatchNo = "",
                        Spec = "",
                        Unit = "",
                        BNoIn = "库存初始化",
                        ItemIn = 0,
                        QCStatus = 1,
                        Qty = 0.0
                    };
                    write.ShowDialog();
                    if (write.IsOK)
                    {
                        this.tbcMain_SelectedIndexChanged(null, null);
                    }
                    write.Dispose();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void miDoTask_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无盘点库存明细数据！");
            }
            else
            {
                frmChkPosList list = new frmChkPosList {
                    AppInformation = base.AppInformation,
                    UserInformation = base.UserInformation,
                    CheckNo = current["cBNo"].ToString()
                };
                list.ShowDialog();
                if (list.IsOK)
                {
                    this.tbcMain_SelectedIndexChanged(null, null);
                }
                list.Dispose();
            }
        }

        private void miRegBatchNoDiff_Click(object sender, EventArgs e)
        {
            if (this.bdsList.Count == 0)
            {
                MessageBox.Show("对不起，无盘点数据！");
            }
            else
            {
                int count = this.grdDtl.SelectedRows.Count;
                if (count == 0)
                {
                    MessageBox.Show("对不起，无盘点明细数据！");
                }
                else
                {
                    string pWHId = "";
                    string pCheckNo = "";
                    DataRowView current = null;
                    current = (DataRowView) this.bdsList.Current;
                    if (current != null)
                    {
                        pWHId = current["cWHId"].ToString();
                        pCheckNo = current["cBNo"].ToString();
                        if (MessageBox.Show("你确定需要对你所选择的" + count.ToString() + "条数据进行无盘差异登记吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.No)
                        {
                            double pDiff = 0.0;
                            double pBad = 0.0;
                            int num4 = 0;
                            this.pgDtl.Maximum = count;
                            this.pgDtl.Minimum = 0;
                            this.pgDtl.Value = 0;
                            this.pgDtl.Visible = true;
                            foreach (DataGridViewRow row in this.grdDtl.SelectedRows)
                            {
                                string sErr = "";
                                string str4 = PubDBCommFuns.sp_Chk_WriteChkDtl(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", pWHId, row.Cells[this.colDtlPosId.Name].Value.ToString().Trim(), row.Cells[this.colDtlPalletId.Name].Value.ToString().Trim(), row.Cells[this.colDtlcBoxId.Name].Value.ToString().Trim(), row.Cells[this.colDtlMNo.Name].Value.ToString().Trim(), pDiff, pBad, row.Cells[this.colDtlUnit.Name].Value.ToString().Trim(), row.Cells[this.colDtlcBNoIn.Name].Value.ToString().Trim(), Convert.ToInt32(row.Cells[this.colDtlnItemIn.Name].Value), pCheckNo, out sErr);
                                if (((str4.Trim() != "0") && (str4.Trim() != "B")) && (sErr.Trim() != ""))
                                {
                                    MessageBox.Show(sErr);
                                }
                                else
                                {
                                    num4++;
                                }
                                this.pgDtl.Value++;
                            }
                            this.pgDtl.Visible = false;
                            MessageBox.Show("成功登记了：" + num4.ToString() + " 条数据！");
                        }
                    }
                }
            }
        }

        private void miRegDtl_Click(object sender, EventArgs e)
        {
            int num = 2;
            try
            {
                DataRowView current = (DataRowView) this.bindingSource_Detail.Current;
                DataRowView view2 = (DataRowView) this.bindingSource_Main.Current;
                if (current == null)
                {
                    MessageBox.Show("无盘点库存明细数据！");
                }
                else
                {
                    num = int.Parse(current["nStatus"].ToString());
                    frmChkDtlWrite write = new frmChkDtlWrite {
                        AppInformation = base.AppInformation,
                        UserInformation = base.UserInformation,
                        IsNewAddMat = false,
                        WHId = view2["cWHId"].ToString().Trim(),
                        CheckNo = view2["cBNo"].ToString().Trim(),
                        PosId = current["cPosId"].ToString().Trim(),
                        PalletId = current["nPalletId"].ToString().Trim(),
                        BoxId = current["cBoxId"].ToString().Trim(),
                        MNo = current["cMNo"].ToString().Trim(),
                        MName = current["cMName"].ToString().Trim(),
                        BatchNo = current["cBatchNo"].ToString().Trim(),
                        Spec = current["cSpec"].ToString().Trim(),
                        Unit = current["cUnit"].ToString().Trim(),
                        BNoIn = current["cBNoIn"].ToString().Trim(),
                        ItemIn = int.Parse(current["nItemIn"].ToString().Trim()),
                        QCStatus = int.Parse(current["nQCStatus"].ToString()),
                        Qty = double.Parse(current["fQty"].ToString())
                    };
                    write.ShowDialog();
                    if (write.IsOK)
                    {
                        this.tbcMain_SelectedIndexChanged(null, null);
                    }
                    write.Dispose();
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

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (!((current == null) || current.IsNew))
            {
                str = current["cBNo"].ToString();
            }
            string name = this.tbcMain.SelectedTab.Name;
            if (name != null)
            {
                if (!(name == "tbpChkDtl"))
                {
                    if (name == "tbpChkList")
                    {
                        this.BandDataSetList(" where cBNo='" + str + "'", this.grdList);
                    }
                }
                else
                {
                    this.BandDataSetDetail(" where cBNo='" + str + "'", this.grdDtl);
                }
            }
        }

        private void tlb_M_Ajust_Click(object sender, EventArgs e)
        {
            int num = 0;
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无盘点数据！");
            }
            else
            {
                num = int.Parse(current["nStatus"].ToString());
                if (num < 2)
                {
                    if (MessageBox.Show("该盘点单还存在未盘点登记数据,确认盘点结束吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (num == 3)
                {
                    MessageBox.Show("对不起，该单已盘点结束！");
                    return;
                }
                string sErr = "";
                if (PubDBCommFuns.sp_Pack_BillWKTskOver(base.AppInformation.SvrSocket, int.Parse(current["nBClass"].ToString()), current["cBNo"].ToString(), base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", out sErr).Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show("盘点结束成功！");
                    current.BeginEdit();
                    current["nStatus"] = 3;
                    current.EndEdit();
                    ((DataTable) this.bindingSource_Main.DataSource).AcceptChanges();
                    this.DataRowViewToUI(current, this.panel_Edit);
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
            this.DoEditDetail();
        }

        private void tlb_M_ErpImp_Click(object sender, EventArgs e)
        {
            DataInFromMid.DataImpBillCheck(base.AppInformation, base.UserInformation);
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
            new FrmStockMCheckFilter { AppInformation = base.AppInformation, UserInformation = base.UserInformation }.ShowDialog();
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

        private void toolStripButton_Audit_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无盘点数据！");
            }
            else if (int.Parse(current["nStatus"].ToString()) > 0)
            {
                MessageBox.Show("对不起，该单已经开始盘点，不能审核！");
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
                    current.BeginEdit();
                    current["bIsChecked"] = 1;
                    current["dCheckDate"] = DateTime.Now;
                    current["cChecker"] = base.UserInformation.UserName;
                    current.EndEdit();
                    ((DataTable) this.bindingSource_Main.DataSource).AcceptChanges();
                    this.DataRowViewToUI(current, this.panel_Edit);
                    this.DoRefresh();
                }
            }
        }
    }
}

