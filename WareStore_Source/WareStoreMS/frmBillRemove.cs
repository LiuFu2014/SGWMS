namespace WareStoreMS
{
    using SunEast;
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using Zqm.CommBase;
    using Zqm.DBCommInfo;
    using Zqm.FileFun;

    public class frmBillRemove : FrmSTable
    {
        private ArrayList ArrBillState = new ArrayList();
        private ArrayList ArrExecState = new ArrayList();
        private ArrayList ArrExecState1 = new ArrayList();
        private ArrayList ArrQCState = new ArrayList();
        private ArrayList ArrQCState1 = new ArrayList();
        private BindingSource bdsDtl;
        private bool bDSIsOpenForDtl = false;
        private bool bDSIsOpenForMain = false;
        private BindingSource bdsMain;
        private Button btn_Dtl_Delete;
        private Button btn_Dtl_Edit;
        private Button btn_Dtl_New;
        private ToolStripButton btn_M_Help;
        private Button btnQry;
        private Button btnUnFind;
        private ComboBox cmb_cMatClass;
        private ComboBox cmb_cPayer;
        private ComboBox cmb_cWHIdFrom;
        private ComboBox cmb_cWHIdTo;
        private ComboBox cmb_nPStatus;
        private ComboBox cmbFindCheck;
        private ComboBox cmbFindType;
        private ComboBox cmbFindUser;
        private DataGridViewTextBoxColumn colcBId;
        private DataGridViewTextBoxColumn colcMName;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn colcUnit;
        private DataGridViewComboBoxColumn colcWHIdFrom;
        private DataGridViewComboBoxColumn colcWHIdTo;
        private DataGridViewTextBoxColumn colfQty;
        private IContainer components = null;
        private DataSet dsD = new DataSet();
        private DataSet dsM = new DataSet();
        private DateTimePicker dtp_dCreateDate;
        private DateTimePicker dtpFind_B;
        private DateTimePicker dtpFind_E;
        public DataGridView grdDtl;
        public DataGridView grdList;
        private GroupBox groupBox1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label31;
        private Label label38;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl_Check;
        private Label lblChecker;
        private ToolStripMenuItem mi_Dtl_PrintBarCode;
        private int nBClass = 6;
        private int nOperator = 1;
        private OperateType optDtl = OperateType.optNone;
        private OperateType optMain = OperateType.optNone;
        private Panel pnlBtns;
        private Panel pnlDtl;
        private Panel pnlDtlEdit;
        public Panel pnlEdit;
        public SplitContainer pnlSplit;
        private ContextMenuStrip ppmDtl;
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strCondition = "";
        private string strTbNameDtl = "TWB_BILLREMOVEDtl";
        private string strTbNameMain = "TWB_BILLREMOVE";
        private ToolStripButton tlb_M_BldBillIn;
        public ToolStripButton tlb_M_Check;
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_Edit;
        public ToolStripButton tlb_M_ErpImp;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Find;
        private ToolStripButton tlb_M_Item;
        public ToolStripButton tlb_M_New;
        public ToolStripButton tlb_M_Print;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Save;
        private ToolStripButton tlb_M_UnCheck;
        public ToolStripButton tlb_M_Undo;
        public ToolStrip tlbMain;
        private ToolStripButton tlbSaveSysRts;
        public Timer tmrMain;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private TextBox txt_cBNo;
        private TextBox txt_cBNoIn;
        private TextBox txt_cBNoOut;
        private TextBox txt_cChecker;
        private TextBox txt_cLinkId;
        private TextBox txt_cRemark;
        private TextBox txt_Dtl_cMName;
        private TextBox txt_Dtl_cMNo;
        private TextBox txt_Dtl_cSpec;
        private TextBox txt_Dtl_cUnit;
        private TextBox txt_Dtl_fQty;
        private TextBox txtFindBillFrom;
        private WareType wtWareType = WareType.wt3D;

        public frmBillRemove()
        {
            this.InitializeComponent();
        }

        private void bdsDtl_PositionChanged(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bdsDtl.Current;
            if (current != null)
            {
                this.ClearUIValues(this.pnlDtlEdit);
                if (!current.IsNew)
                {
                    this.DataRowViewToUI(current, this.pnlDtlEdit);
                }
            }
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            string str = "";
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current != null)
            {
                this.ClearUIValues(this.pnlEdit);
                this.lbl_Check.Visible = false;
                if (!current.IsNew)
                {
                    this.DataRowViewToUI(current, this.pnlEdit);
                    this.lbl_Check.Visible = current["bIsChecked"].ToString() == "1";
                    if ((current["bIsChecked"].ToString().Trim() == "0") && (current["cChecker"].ToString().Trim() != ""))
                    {
                        this.lblChecker.Text = "取消审核人";
                    }
                    else
                    {
                        this.lblChecker.Text = "审核人：";
                    }
                    if ((this.bdsMain.Count > 0) && (current["cBNo"] != null))
                    {
                        str = current["cBNo"].ToString();
                    }
                }
            }
            this.OpenDtlDataSet(" where cBNo='" + str + "'");
        }

        public void BindDtlDataSetToCtrls()
        {
            this.grdDtl.DataSource = null;
            this.grdDtl.DataSource = this.bdsDtl;
        }

        public void BindMainDataSetToCtrls()
        {
            this.grdList.DataSource = null;
            this.grdList.DataSource = this.bdsMain;
        }

        private void btn_Dtl_Delete_Click(object sender, EventArgs e)
        {
            if ((this.optMain == OperateType.optNew) || (this.optMain == OperateType.optEdit))
            {
                MessageBox.Show("对不起，主表编辑中，不能删除!");
            }
            else if ((this.bdsDtl != null) && (this.bdsMain.Count != 0))
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current["bIsChecked"].ToString().ToLower() == "1")
                {
                    MessageBox.Show("对不起，已被审核！");
                }
                else if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
                {
                    if ((base.UserInformation.UType == UserType.utNormal) && (base.UserInformation.UserName.Trim() != current["cPayer"].ToString().Trim()))
                    {
                        MessageBox.Show("对不起，你无权限完成此操作！");
                    }
                    else
                    {
                        DataRowView view2 = (DataRowView) this.bdsDtl.Current;
                        string sSql = string.Concat(new object[] { "delete from TWB_BillInDtl where cBNo='", view2["cBNo"].ToString(), "' and nItem= ", view2["nItem"] });
                        string sErr = "";
                        DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
                        if (dataBySql.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            this.OpenDtlDataSet(" where cBNo='" + current["cBNo"].ToString() + "'");
                        }
                        else
                        {
                            MessageBox.Show(dataBySql.Tables[0].Rows[0][0].ToString());
                        }
                    }
                }
            }
        }

        private void btn_Dtl_Edit_Click(object sender, EventArgs e)
        {
            if ((this.optMain == OperateType.optNew) || (this.optMain == OperateType.optEdit))
            {
                MessageBox.Show("对不起，主表未保存，请先保存主单数据，再修改明细!");
            }
            else if ((this.bdsDtl != null) && (this.bdsMain.Count != 0))
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current["bIsChecked"].ToString().ToLower() == "1")
                {
                    MessageBox.Show("对不起，已被审核！");
                }
                else if ((base.UserInformation.UType == UserType.utNormal) && (base.UserInformation.UserName.Trim() != current["cPayer"].ToString().Trim()))
                {
                    MessageBox.Show("对不起，你无权限完成此操作！");
                }
                else
                {
                    DataRowView view2 = (DataRowView) this.bdsDtl.Current;
                    using (FrmItemEditor editor = new FrmItemEditor())
                    {
                        editor.UserInformation = base.UserInformation;
                        editor.AppInformation = base.AppInformation;
                        editor.DrvItem = view2;
                        editor.IsOutBill = true;
                        editor.MatClass = current["cMatClass"].ToString();
                        editor.WHId = current["cWHIdFrom"].ToString();
                        editor.BIsNew = false;
                        editor.DataRowToUI();
                        editor.ShowDialog();
                        if (editor.BIsResult)
                        {
                            this.OpenDtlDataSet(" where cBNo='" + current["cBNo"].ToString() + "'");
                        }
                    }
                }
            }
        }

        private void btn_Dtl_New_Click(object sender, EventArgs e)
        {
            if ((this.optMain == OperateType.optNew) || (this.optMain == OperateType.optEdit))
            {
                MessageBox.Show("对不起，主表未保存，请先保存主单数据，再新增明细!");
            }
            else if ((this.bdsDtl != null) && (this.bdsMain.Count != 0))
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current["bIsChecked"].ToString().ToLower() == "1")
                {
                    MessageBox.Show("对不起，已被审核！");
                }
                else if ((base.UserInformation.UType == UserType.utNormal) && (base.UserInformation.UserName.Trim() != current["cPayer"].ToString().Trim()))
                {
                    MessageBox.Show("对不起，你无权限完成此操作！");
                }
                else
                {
                    string str = current["cWHIdFrom"].ToString();
                    DataRowView view2 = (DataRowView) this.bdsDtl.AddNew();
                    view2["fQty"] = 0;
                    view2["cUnit"] = "";
                    view2["cBNo"] = current["cBNo"];
                    using (FrmItemEditor editor = new FrmItemEditor())
                    {
                        editor.UserInformation = base.UserInformation;
                        editor.AppInformation = base.AppInformation;
                        editor.DrvItem = view2;
                        editor.IsOutBill = true;
                        editor.MatClass = current["cMatClass"].ToString();
                        editor.WHId = current["cWHIdFrom"].ToString();
                        editor.BIsNew = true;
                        editor.BType = current["cBTypeId"].ToString();
                        editor.WHId = str;
                        editor.DataRowToUI();
                        editor.ShowDialog();
                        if (editor.BIsResult)
                        {
                            this.OpenDtlDataSet(" where cBNo='" + current["cBNo"].ToString() + "'");
                        }
                    }
                }
            }
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(" where nBClass=" + this.nBClass.ToString());
            if (this.dtpFind_B.Text.Trim() != "")
            {
                builder.Append(" and dDate >='" + this.dtpFind_B.Value.ToString("yyyy-MM-dd 00:00:00") + "'");
            }
            if (this.dtpFind_E.Text.Trim() != "")
            {
                builder.Append(" and dDate <='" + this.dtpFind_E.Value.ToString("yyyy-MM-dd 23:59:29") + "'");
            }
            if (this.cmbFindUser.Text.Trim() != "")
            {
                builder.Append(" and cCreator='" + this.cmbFindUser.SelectedValue.ToString() + "'");
            }
            if (this.cmbFindType.Text.Trim() != "")
            {
                builder.Append(" and cBTypeId='" + this.cmbFindType.SelectedValue.ToString() + "'");
            }
            if ((this.cmbFindCheck.Text.Trim() != "") && (this.cmbFindCheck.Text.Trim() != "全部"))
            {
                if (this.cmbFindCheck.SelectedIndex == 1)
                {
                    builder.Append(" and bIsChecked =1");
                }
                else
                {
                    builder.Append(" and bIsChecked =0");
                }
            }
            if (this.txtFindBillFrom.Text.Trim() != "")
            {
                builder.Append(" and isnull(cBNoFrom,'') like '%" + this.txtFindBillFrom.Text.Trim() + "%'");
            }
            this.strCondition = builder.ToString();
            this.OpenMainDataSet(this.strCondition);
            builder.Remove(0, builder.Length);
        }

        private void btnUnFind_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime time = now.AddMonths(-1);
            this.dtpFind_B.Value = time;
            this.dtpFind_E.Value = now;
            this.cmbFindType.SelectedIndex = -1;
            this.cmbFindUser.SelectedIndex = -1;
            this.cmbFindCheck.SelectedIndex = 2;
            this.txtFindBillFrom.Text = "";
            base.Update();
            this.btnQry_Click(null, e);
        }

        private void DisplayState(ToolStripLabel stbSt, OperateType optX)
        {
            string str = "【状态】";
            if (stbSt != null)
            {
                switch (optX)
                {
                    case OperateType.optNew:
                        str = str + " 新建";
                        break;

                    case OperateType.optEdit:
                        str = str + " 修改";
                        break;

                    default:
                        str = str + "    ";
                        break;
                }
                base.Update();
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

        public void DoDDelete()
        {
            int optDtl = -1;
            optDtl = (int) this.optDtl;
            DataRowView current = (DataRowView) this.bdsDtl.Current;
            if (current == null)
            {
                MessageBox.Show("对不起,无明细数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((0 < optDtl) && (optDtl < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (false)
            {
                this.optDtl = OperateType.optDelete;
                this.OpenDtlDataSet(this.strCondition);
                this.CtrlOptButtons(this.pnlBtns, this.pnlDtlEdit, this.optDtl, (DataTable) this.bdsDtl.DataSource);
                this.optDtl = OperateType.optNone;
                this.DisplayState(this.stbState, this.optDtl);
                this.CtrlControlReadOnly(this.pnlDtlEdit, false);
            }
            else
            {
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void DoDEdit()
        {
            this.optDtl = OperateType.optEdit;
            DataRowView current = (DataRowView) this.bdsDtl.Current;
            current.BeginEdit();
            current["dEditDate"] = DateTime.Now;
            current["cEditor"] = base.UserInformation.UserName;
            current.EndEdit();
            this.CtrlOptButtons(this.pnlBtns, this.pnlDtlEdit, this.optDtl, (DataTable) this.bdsDtl.DataSource);
            this.txt_Dtl_cMNo.Focus();
            this.DisplayState(this.stbState, this.optDtl);
            this.CtrlControlReadOnly(this.pnlDtlEdit, true);
            this.txt_Dtl_cMNo.ReadOnly = true;
            this.txt_Dtl_cMNo.ReadOnly = true;
        }

        public void DoDNew()
        {
            DataRowView current = (DataRowView) this.bdsMain.Current;
            string billNo = current["cBNo"].ToString();
            this.optDtl = OperateType.optNew;
            DataRowView drv = (DataRowView) this.bdsDtl.AddNew();
            drv["nItem"] = this.GetNewItem(billNo);
            drv["dProdDate"] = DateTime.Now;
            drv.EndEdit();
            this.DataRowViewToUI(drv, this.pnlDtlEdit);
            this.CtrlOptButtons(this.pnlBtns, this.pnlDtlEdit, this.optDtl, (DataTable) this.bdsDtl.DataSource);
            this.txt_Dtl_cMNo.Focus();
            this.DisplayState(this.stbState, this.optDtl);
            this.CtrlControlReadOnly(this.pnlDtlEdit, true);
            this.txt_Dtl_cMNo.ReadOnly = true;
            this.txt_Dtl_cMNo.ReadOnly = true;
        }

        public void DoDSave()
        {
            this.txt_Dtl_cMNo.Focus();
            DataRowView current = (DataRowView) this.bdsDtl.Current;
            if ((this.optDtl == OperateType.optNew) || (this.optDtl == OperateType.optEdit))
            {
                bool flag = false;
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                this.UIToDataRowView(current, this.pnlDtlEdit);
                if (flag)
                {
                    this.optDtl = OperateType.optSave;
                    MessageBox.Show("保存明细数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OpenDtlDataSet(" where cBNo='" + current["cBNo"].ToString() + "'");
                    this.CtrlOptButtons(this.pnlBtns, this.pnlDtlEdit, this.optDtl, (DataTable) this.bdsDtl.DataSource);
                    this.optDtl = OperateType.optNone;
                    this.DisplayState(this.stbState, this.optDtl);
                    this.CtrlControlReadOnly(this.pnlDtlEdit, false);
                }
                else
                {
                    MessageBox.Show("保存主表数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void DoDUndo()
        {
            this.optDtl = OperateType.optUndo;
            DataRowView current = (DataRowView) this.bdsDtl.Current;
            if (current != null)
            {
                if (current.IsEdit)
                {
                    current.CancelEdit();
                }
                if (current.IsNew)
                {
                    current.Delete();
                }
                base.DBDataSet.Tables[this.strTbNameDtl].AcceptChanges();
                this.CtrlOptButtons(this.pnlBtns, this.pnlDtlEdit, this.optDtl, (DataTable) this.bdsDtl.DataSource);
                this.optDtl = OperateType.optNone;
                this.DisplayState(this.stbState, this.optDtl);
                this.CtrlControlReadOnly(this.pnlDtlEdit, false);
            }
        }

        public void DoMDelete()
        {
            int optMain = -1;
            optMain = (int) this.optMain;
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current == null)
            {
                MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((0 < optMain) && (optMain < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (current != null)
            {
                if (current["bIsChecked"].ToString().ToLower() == "1")
                {
                    MessageBox.Show("对不起，已经审核，不能修改！");
                }
                else if (MessageBox.Show("系统将永久删除数据，您确定要删除此数据吗？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
                {
                    if ((base.UserInformation.UType == UserType.utNormal) && (base.UserInformation.UserName.Trim() != current["cPayer"].ToString().Trim()))
                    {
                        MessageBox.Show("对不起，你无权限删除！");
                    }
                    else
                    {
                        string sErr = "";
                        if (PubDBCommFuns.sp_Pack_BillIODel(base.AppInformation.SvrSocket, current["cBNo"].ToString(), base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", out sErr) == "0")
                        {
                            this.optMain = OperateType.optDelete;
                            this.OpenMainDataSet(this.strCondition);
                            this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
                            this.optMain = OperateType.optNone;
                            this.DisplayState(this.stbState, this.optMain);
                            this.CtrlControlReadOnly(this.pnlEdit, false);
                        }
                        else
                        {
                            MessageBox.Show(sErr, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
        }

        public void DoMEdit()
        {
            this.optMain = OperateType.optEdit;
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current != null)
            {
                if (current["bIsChecked"].ToString().ToLower() == "1")
                {
                    MessageBox.Show("对不起，已经审核，不能修改！");
                }
                else if ((base.UserInformation.UType == UserType.utNormal) && (base.UserInformation.UserName.Trim() != current["cPayer"].ToString().Trim()))
                {
                    MessageBox.Show("对不起，你无权限修改！");
                }
                else
                {
                    current.BeginEdit();
                    current["dEditDate"] = DateTime.Now;
                    current["cEditor"] = base.UserInformation.UserName;
                    current.EndEdit();
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
                    this.txt_cBNo.Focus();
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, true);
                    this.txt_cBNo.ReadOnly = true;
                    this.cmb_nPStatus.BackColor = Color.FromName("Control");
                    this.txt_cChecker.ReadOnly = true;
                }
            }
        }

        public void DoMNew()
        {
            this.optMain = OperateType.optNew;
            DataTable dataSource = (DataTable) this.bdsMain.DataSource;
            int count = dataSource.Columns.Count;
            DataRowView drv = (DataRowView) this.bdsMain.AddNew();
            try
            {
                drv["cBNo"] = "";
                drv["nBClass"] = this.nBClass;
                drv["cBTypeId"] = "601";
                drv["bIsChecked"] = false;
                drv["dDate"] = DateTime.Now;
                drv["cPayer"] = base.UserInformation.UserName;
                drv["nPStatus"] = 0;
                drv["dCreateDate"] = DateTime.Now;
                drv["cCreator"] = base.UserInformation.UserName;
                drv["cCmptId"] = base.UserInformation.UnitId;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            this.DataRowViewToUI(drv, this.pnlEdit);
            this.lblChecker.Text = "审核人：";
            this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
            this.txt_cBNo.Focus();
            this.DisplayState(this.stbState, this.optMain);
            this.CtrlControlReadOnly(this.pnlEdit, true);
            this.txt_cBNo.ReadOnly = true;
            this.cmb_nPStatus.BackColor = Color.FromName("Control");
            this.txt_cChecker.ReadOnly = true;
        }

        public void DoMSave()
        {
            this.txt_cBNo.Focus();
            if (this.cmb_cWHIdFrom.Text.Trim() == "")
            {
                MessageBox.Show("对不起，入库类型不能为空！");
                this.cmb_cWHIdFrom.Focus();
            }
            else if ((this.cmb_cPayer.Text.Trim() == "") || (this.cmb_cPayer.SelectedValue == null))
            {
                MessageBox.Show("对不起，仓管员不能为空！");
                this.cmb_cPayer.Focus();
            }
            else if ((this.cmb_cWHIdTo.Text.Trim() == "") || (this.cmb_cWHIdTo.SelectedValue == null))
            {
                MessageBox.Show("对不起，供货单位不能为空！");
                this.cmb_cWHIdTo.Focus();
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if ((this.optMain == OperateType.optNew) || (this.optMain == OperateType.optEdit))
                {
                    if (current.IsEdit)
                    {
                        current.EndEdit();
                    }
                    this.UIToDataRowView(current, this.pnlEdit);
                    string sSql = "";
                    if (this.optMain == OperateType.optNew)
                    {
                        current["cBNo"] = this.GetNewId();
                        sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, "cBNo", true);
                    }
                    else
                    {
                        sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, "cBNo", false);
                    }
                    string sErr = "";
                    string str3 = "";
                    string str4 = "";
                    DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, sSql, DBSQLCommandInfo.GetFieldsForDate(current), out sErr);
                    str3 = set.Tables[0].Rows[0].ItemArray[0].ToString();
                    str4 = set.Tables[0].Rows[0].ItemArray[1].ToString();
                    if (str3 == "0")
                    {
                        this.optMain = OperateType.optSave;
                        MessageBox.Show("保存主表数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((DataTable) this.bdsMain.DataSource).AcceptChanges();
                        this.bdsMain_PositionChanged(null, null);
                        this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
                        this.optMain = OperateType.optNone;
                        this.DisplayState(this.stbState, this.optMain);
                        this.CtrlControlReadOnly(this.pnlEdit, false);
                    }
                    else
                    {
                        MessageBox.Show("保存主表数据失败！", sErr, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        public void DoMUndo()
        {
            this.optMain = OperateType.optUndo;
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current != null)
            {
                if (current.IsEdit)
                {
                    current.CancelEdit();
                }
                if (current.IsNew)
                {
                    current.Delete();
                }
                this.dsM.Tables["data"].AcceptChanges();
                this.bdsMain_PositionChanged(null, null);
                this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
                this.optMain = OperateType.optNone;
                this.DisplayState(this.stbState, this.optMain);
                this.CtrlControlReadOnly(this.pnlEdit, false);
            }
        }

        public void DoPrintBill()
        {
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("对不起，无单据数据可打印！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current == null)
                {
                    MessageBox.Show("对不起，无单据数据可打印！");
                }
            }
        }

        private void frmBillRemove_Load(object sender, EventArgs e)
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
            this.Text = this.GetTitleText();
            this.InitFormParameters();
            this.stbModul.Text = "【模块】" + base.ModuleRtsName;
            this.Text = base.ModuleRtsName;
            if (base.UserInformation != null)
            {
                this.stbUser.Text = "【用户】" + base.UserInformation.UserName;
            }
            this.stbState.Text = "【状态】   ";
            this.stbDateTime.Text = "【时间】" + DateTime.Now.ToString();
            this.LoadBaseItem();
            this.btnUnFind_Click(null, e);
        }

        public string GetNewId()
        {
            string strTbNameMain = this.strTbNameMain;
            string str2 = "cBNo";
            string str3 = "BR" + DateTime.Now.ToString("yyMMdd");
            int num = 12;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetNewId :pTbName,:pFldKey,:pLen,:pReplaceChar,:pHeader,:pFldCon,:pValueCon",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pTbName",
                ParameterValue = strTbNameMain,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pFldKey",
                ParameterValue = str2,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLen",
                ParameterValue = num.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pReplaceChar",
                ParameterValue = "0",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pHeader",
                ParameterValue = str3,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pFldCon",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pValueCon",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            return client.GetDataSet(base.AppInformation.SvrSocket, cmdInfo, false, out sErr).Tables["data"].Rows[0][0].ToString();
        }

        public int GetNewItem(string billNo)
        {
            string strTbNameDtl = this.strTbNameDtl;
            string str2 = "nItem";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetDtlSeq :TbName,:PFld,:SeqFld,:PValue",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "TbName",
                ParameterValue = strTbNameDtl,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "PFld",
                ParameterValue = "cBNo",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "SeqFld",
                ParameterValue = str2,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "PValue",
                ParameterValue = billNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(base.AppInformation.SvrSocket, cmdInfo, false, out sErr);
            table = set.Tables["data"];
            if (table == null)
            {
                set.Clear();
                MessageBox.Show(sErr);
                return -1;
            }
            if (table.Rows.Count == 0)
            {
                set.Clear();
                MessageBox.Show(" 获取明细序号无结果数据：" + sErr);
                return -1;
            }
            object obj2 = table.Rows[0][0];
            set.Clear();
            return int.Parse(obj2.ToString());
        }

        private string GetTitleText()
        {
            string str = "";
            switch (this.wtWareType)
            {
                case WareType.wt3D:
                    str = "——立体仓库";
                    break;

                case WareType.wt2D:
                    str = "——平面仓库";
                    break;

                case WareType.wtDPS:
                    str = "——DPS仓库";
                    break;
            }
            return (base.ModuleRtsName + str);
        }

        public override void InitFormParameters()
        {
            base.ModuleRtsId = "3408";
            base.ModuleRtsName = "调拨单管理";
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmBillRemove));
            this.txt_Dtl_cSpec = new TextBox();
            this.label11 = new Label();
            this.txt_Dtl_cMName = new TextBox();
            this.label10 = new Label();
            this.txt_Dtl_cUnit = new TextBox();
            this.pnlDtlEdit = new Panel();
            this.txt_Dtl_fQty = new TextBox();
            this.label25 = new Label();
            this.txt_Dtl_cMNo = new TextBox();
            this.label19 = new Label();
            this.label12 = new Label();
            this.cmbFindType = new ComboBox();
            this.label1 = new Label();
            this.btnUnFind = new Button();
            this.cmbFindCheck = new ComboBox();
            this.label28 = new Label();
            this.label24 = new Label();
            this.dtpFind_E = new DateTimePicker();
            this.dtpFind_B = new DateTimePicker();
            this.pnlDtl = new Panel();
            this.grdDtl = new DataGridView();
            this.colcMNo = new DataGridViewTextBoxColumn();
            this.colcMName = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.colfQty = new DataGridViewTextBoxColumn();
            this.colcUnit = new DataGridViewTextBoxColumn();
            this.ppmDtl = new ContextMenuStrip(this.components);
            this.mi_Dtl_PrintBarCode = new ToolStripMenuItem();
            this.pnlBtns = new Panel();
            this.btn_Dtl_Delete = new Button();
            this.btn_Dtl_Edit = new Button();
            this.btn_Dtl_New = new Button();
            this.label8 = new Label();
            this.btnQry = new Button();
            this.grdList = new DataGridView();
            this.colcBId = new DataGridViewTextBoxColumn();
            this.colcWHIdFrom = new DataGridViewComboBoxColumn();
            this.colcWHIdTo = new DataGridViewComboBoxColumn();
            this.bdsMain = new BindingSource(this.components);
            this.bdsDtl = new BindingSource(this.components);
            this.txt_cRemark = new TextBox();
            this.label6 = new Label();
            this.label3 = new Label();
            this.cmb_cWHIdFrom = new ComboBox();
            this.txt_cLinkId = new TextBox();
            this.label15 = new Label();
            this.txt_cChecker = new TextBox();
            this.lblChecker = new Label();
            this.tlb_M_Refresh = new ToolStripButton();
            this.tlb_M_Print = new ToolStripButton();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.tlb_M_Find = new ToolStripButton();
            this.label7 = new Label();
            this.txt_cBNo = new TextBox();
            this.label2 = new Label();
            this.cmb_cPayer = new ComboBox();
            this.label30 = new Label();
            this.label23 = new Label();
            this.label20 = new Label();
            this.dtp_dCreateDate = new DateTimePicker();
            this.txtFindBillFrom = new TextBox();
            this.lbl_Check = new Label();
            this.pnlEdit = new Panel();
            this.txt_cBNoIn = new TextBox();
            this.label31 = new Label();
            this.label14 = new Label();
            this.cmb_cMatClass = new ComboBox();
            this.label38 = new Label();
            this.txt_cBNoOut = new TextBox();
            this.label13 = new Label();
            this.label22 = new Label();
            this.cmb_cWHIdTo = new ComboBox();
            this.label5 = new Label();
            this.cmb_nPStatus = new ComboBox();
            this.label9 = new Label();
            this.label29 = new Label();
            this.cmbFindUser = new ComboBox();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.tlb_M_New = new ToolStripButton();
            this.tlb_M_Edit = new ToolStripButton();
            this.stbUser = new ToolStripStatusLabel();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tlb_M_Undo = new ToolStripButton();
            this.tlb_M_Delete = new ToolStripButton();
            this.tlb_M_Save = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.tlb_M_Check = new ToolStripButton();
            this.tlb_M_UnCheck = new ToolStripButton();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.tlb_M_BldBillIn = new ToolStripButton();
            this.tlb_M_Item = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.tlbSaveSysRts = new ToolStripButton();
            this.tmrMain = new Timer(this.components);
            this.groupBox1 = new GroupBox();
            this.label4 = new Label();
            this.stbState = new ToolStripStatusLabel();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.pnlSplit = new SplitContainer();
            this.tlb_M_ErpImp = new ToolStripButton();
            this.pnlDtlEdit.SuspendLayout();
            this.pnlDtl.SuspendLayout();
            ((ISupportInitialize) this.grdDtl).BeginInit();
            this.ppmDtl.SuspendLayout();
            this.pnlBtns.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            ((ISupportInitialize) this.bdsMain).BeginInit();
            ((ISupportInitialize) this.bdsDtl).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.stbMain.SuspendLayout();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            base.SuspendLayout();
            this.txt_Dtl_cSpec.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cSpec.Location = new Point(0xf1, 50);
            this.txt_Dtl_cSpec.Name = "txt_Dtl_cSpec";
            this.txt_Dtl_cSpec.ReadOnly = true;
            this.txt_Dtl_cSpec.Size = new Size(0x6a, 0x15);
            this.txt_Dtl_cSpec.TabIndex = 0x4b;
            this.txt_Dtl_cSpec.Tag = "0";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(200, 0x36);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x29, 12);
            this.label11.TabIndex = 0x4c;
            this.label11.Text = "规格：";
            this.txt_Dtl_cMName.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMName.Location = new Point(0x5e, 14);
            this.txt_Dtl_cMName.Name = "txt_Dtl_cMName";
            this.txt_Dtl_cMName.ReadOnly = true;
            this.txt_Dtl_cMName.Size = new Size(0x20c, 0x15);
            this.txt_Dtl_cMName.TabIndex = 0x49;
            this.txt_Dtl_cMName.Tag = "0";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x1d, 0x12);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x29, 12);
            this.label10.TabIndex = 0x4a;
            this.label10.Text = "名称：";
            this.txt_Dtl_cUnit.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cUnit.Location = new Point(0x217, 50);
            this.txt_Dtl_cUnit.Name = "txt_Dtl_cUnit";
            this.txt_Dtl_cUnit.ReadOnly = true;
            this.txt_Dtl_cUnit.Size = new Size(0x53, 0x15);
            this.txt_Dtl_cUnit.TabIndex = 0x48;
            this.txt_Dtl_cUnit.Tag = "0";
            this.pnlDtlEdit.BackColor = SystemColors.Info;
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cSpec);
            this.pnlDtlEdit.Controls.Add(this.label11);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMName);
            this.pnlDtlEdit.Controls.Add(this.label10);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cUnit);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_fQty);
            this.pnlDtlEdit.Controls.Add(this.label25);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMNo);
            this.pnlDtlEdit.Controls.Add(this.label19);
            this.pnlDtlEdit.Controls.Add(this.label12);
            this.pnlDtlEdit.Dock = DockStyle.Bottom;
            this.pnlDtlEdit.Location = new Point(0, 0x130);
            this.pnlDtlEdit.Name = "pnlDtlEdit";
            this.pnlDtlEdit.Size = new Size(0x287, 0x5d);
            this.pnlDtlEdit.TabIndex = 6;
            this.pnlDtlEdit.Paint += new PaintEventHandler(this.pnlDtlEdit_Paint);
            this.txt_Dtl_fQty.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_fQty.Location = new Point(0x184, 50);
            this.txt_Dtl_fQty.Name = "txt_Dtl_fQty";
            this.txt_Dtl_fQty.ReadOnly = true;
            this.txt_Dtl_fQty.Size = new Size(0x6a, 0x15);
            this.txt_Dtl_fQty.TabIndex = 0x42;
            this.txt_Dtl_fQty.Tag = "0";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0x15b, 0x36);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x29, 12);
            this.label25.TabIndex = 0x43;
            this.label25.Text = "数量：";
            this.txt_Dtl_cMNo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMNo.Location = new Point(0x5e, 50);
            this.txt_Dtl_cMNo.Name = "txt_Dtl_cMNo";
            this.txt_Dtl_cMNo.ReadOnly = true;
            this.txt_Dtl_cMNo.Size = new Size(0x6a, 0x15);
            this.txt_Dtl_cMNo.TabIndex = 0x36;
            this.txt_Dtl_cMNo.Tag = "0";
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x1d, 0x36);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x41, 12);
            this.label19.TabIndex = 0x37;
            this.label19.Text = "物料编码：";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x1ee, 0x36);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x29, 12);
            this.label12.TabIndex = 0x33;
            this.label12.Text = "单位：";
            this.cmbFindType.FormattingEnabled = true;
            this.cmbFindType.Items.AddRange(new object[] { "平面库", "立体库" });
            this.cmbFindType.Location = new Point(0xc6, 40);
            this.cmbFindType.Name = "cmbFindType";
            this.cmbFindType.Size = new Size(0x53, 20);
            this.cmbFindType.TabIndex = 11;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x9d, 0x2c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0x12;
            this.label1.Text = "类型：";
            this.btnUnFind.Location = new Point(0xdf, 0x59);
            this.btnUnFind.Name = "btnUnFind";
            this.btnUnFind.Size = new Size(0x3a, 0x17);
            this.btnUnFind.TabIndex = 0x10;
            this.btnUnFind.Text = "重置";
            this.btnUnFind.UseVisualStyleBackColor = true;
            this.cmbFindCheck.FormattingEnabled = true;
            this.cmbFindCheck.Items.AddRange(new object[] { "全部", "审核", "未审核" });
            this.cmbFindCheck.Location = new Point(0x4a, 90);
            this.cmbFindCheck.Name = "cmbFindCheck";
            this.cmbFindCheck.Size = new Size(0x53, 20);
            this.cmbFindCheck.TabIndex = 13;
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0x10, 0x5e);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x41, 12);
            this.label28.TabIndex = 15;
            this.label28.Text = "审核状态：";
            this.label24.BackColor = SystemColors.ControlText;
            this.label24.BorderStyle = BorderStyle.FixedSingle;
            this.label24.Location = new Point(0xa7, 0x13);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x11, 1);
            this.label24.TabIndex = 10;
            this.dtpFind_E.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_E.Format = DateTimePickerFormat.Custom;
            this.dtpFind_E.Location = new Point(0xc6, 14);
            this.dtpFind_E.Name = "dtpFind_E";
            this.dtpFind_E.Size = new Size(0x53, 0x15);
            this.dtpFind_E.TabIndex = 9;
            this.dtpFind_E.Tag = "2";
            this.dtpFind_B.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_B.Format = DateTimePickerFormat.Custom;
            this.dtpFind_B.Location = new Point(0x4a, 14);
            this.dtpFind_B.Name = "dtpFind_B";
            this.dtpFind_B.Size = new Size(0x53, 0x15);
            this.dtpFind_B.TabIndex = 8;
            this.dtpFind_B.Tag = "2";
            this.pnlDtl.BackColor = SystemColors.InactiveCaptionText;
            this.pnlDtl.Controls.Add(this.grdDtl);
            this.pnlDtl.Controls.Add(this.pnlDtlEdit);
            this.pnlDtl.Controls.Add(this.pnlBtns);
            this.pnlDtl.Dock = DockStyle.Fill;
            this.pnlDtl.Location = new Point(0, 0xac);
            this.pnlDtl.Name = "pnlDtl";
            this.pnlDtl.Size = new Size(0x287, 0x1b4);
            this.pnlDtl.TabIndex = 1;
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.AllowUserToDeleteRows = false;
            this.grdDtl.AllowUserToOrderColumns = true;
            this.grdDtl.Columns.AddRange(new DataGridViewColumn[] { this.colcMNo, this.colcMName, this.colcSpec, this.colfQty, this.colcUnit });
            this.grdDtl.ContextMenuStrip = this.ppmDtl;
            this.grdDtl.Dock = DockStyle.Fill;
            this.grdDtl.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdDtl.Location = new Point(0, 0);
            this.grdDtl.MultiSelect = false;
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.grdDtl.RowHeadersDefaultCellStyle = style;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 0x17;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdDtl.Size = new Size(0x287, 0x130);
            this.grdDtl.TabIndex = 5;
            this.grdDtl.Tag = "8";
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编码";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编码";
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.FillWeight = 200f;
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            this.colcMName.Width = 200;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.ToolTipText = "数量";
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "计量单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.ToolTipText = "计量单位";
            this.ppmDtl.Items.AddRange(new ToolStripItem[] { this.mi_Dtl_PrintBarCode });
            this.ppmDtl.Name = "ppmDtl";
            this.ppmDtl.Size = new Size(0x77, 0x1a);
            this.ppmDtl.Opening += new CancelEventHandler(this.ppmDtl_Opening);
            this.mi_Dtl_PrintBarCode.Name = "mi_Dtl_PrintBarCode";
            this.mi_Dtl_PrintBarCode.Size = new Size(0x76, 0x16);
            this.mi_Dtl_PrintBarCode.Text = "打印条码";
            this.pnlBtns.Controls.Add(this.btn_Dtl_Delete);
            this.pnlBtns.Controls.Add(this.btn_Dtl_Edit);
            this.pnlBtns.Controls.Add(this.btn_Dtl_New);
            this.pnlBtns.Dock = DockStyle.Bottom;
            this.pnlBtns.Location = new Point(0, 0x18d);
            this.pnlBtns.Name = "pnlBtns";
            this.pnlBtns.Size = new Size(0x287, 0x27);
            this.pnlBtns.TabIndex = 4;
            this.btn_Dtl_Delete.Location = new Point(0x105, 8);
            this.btn_Dtl_Delete.Name = "btn_Dtl_Delete";
            this.btn_Dtl_Delete.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_Delete.TabIndex = 3;
            this.btn_Dtl_Delete.Text = "删除";
            this.btn_Dtl_Delete.UseVisualStyleBackColor = true;
            this.btn_Dtl_Delete.Click += new EventHandler(this.btn_Dtl_Delete_Click);
            this.btn_Dtl_Edit.Location = new Point(0xa3, 8);
            this.btn_Dtl_Edit.Name = "btn_Dtl_Edit";
            this.btn_Dtl_Edit.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_Edit.TabIndex = 2;
            this.btn_Dtl_Edit.Text = "修改";
            this.btn_Dtl_Edit.UseVisualStyleBackColor = true;
            this.btn_Dtl_Edit.Click += new EventHandler(this.btn_Dtl_Edit_Click);
            this.btn_Dtl_New.Location = new Point(0x41, 8);
            this.btn_Dtl_New.Name = "btn_Dtl_New";
            this.btn_Dtl_New.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_New.TabIndex = 1;
            this.btn_Dtl_New.Text = "新增";
            this.btn_Dtl_New.UseVisualStyleBackColor = true;
            this.btn_Dtl_New.Click += new EventHandler(this.btn_Dtl_New_Click);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x12);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x41, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "日    期：";
            this.btnQry.Location = new Point(160, 0x59);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new Size(0x3a, 0x17);
            this.btnQry.TabIndex = 14;
            this.btnQry.Text = "查询";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new EventHandler(this.btnQry_Click);
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colcBId, this.colcWHIdFrom, this.colcWHIdTo });
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new Point(0, 0x74);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = SystemColors.Control;
            style2.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style2.ForeColor = SystemColors.WindowText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = style2;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 0x17;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new Size(0x128, 0x1ec);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            this.colcBId.DataPropertyName = "cBNo";
            this.colcBId.FillWeight = 50f;
            this.colcBId.Frozen = true;
            this.colcBId.HeaderText = "单号";
            this.colcBId.Name = "colcBId";
            this.colcBId.ReadOnly = true;
            this.colcBId.ToolTipText = "单号";
            this.colcWHIdFrom.DataPropertyName = "cWHIdFrom";
            this.colcWHIdFrom.HeaderText = "源仓库";
            this.colcWHIdFrom.Name = "colcWHIdFrom";
            this.colcWHIdFrom.ReadOnly = true;
            this.colcWHIdFrom.Resizable = DataGridViewTriState.True;
            this.colcWHIdFrom.SortMode = DataGridViewColumnSortMode.Automatic;
            this.colcWHIdFrom.ToolTipText = "源仓库";
            this.colcWHIdTo.DataPropertyName = "cWHIdTo";
            this.colcWHIdTo.HeaderText = "目标仓库";
            this.colcWHIdTo.Name = "colcWHIdTo";
            this.colcWHIdTo.ReadOnly = true;
            this.colcWHIdTo.ToolTipText = "目标仓库";
            this.bdsMain.PositionChanged += new EventHandler(this.bdsMain_PositionChanged);
            this.txt_cRemark.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cRemark.Location = new Point(0x6d, 0x67);
            this.txt_cRemark.Multiline = true;
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.ReadOnly = true;
            this.txt_cRemark.ScrollBars = ScrollBars.Vertical;
            this.txt_cRemark.Size = new Size(0x1f6, 0x3e);
            this.txt_cRemark.TabIndex = 9;
            this.txt_cRemark.Tag = "0";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x16, 0x67);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x41, 12);
            this.label6.TabIndex = 0x27;
            this.label6.Text = "备    注：";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1af, 0x22);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 0x23;
            this.label3.Text = "单据日期：";
            this.cmb_cWHIdFrom.BackColor = SystemColors.Control;
            this.cmb_cWHIdFrom.FormattingEnabled = true;
            this.cmb_cWHIdFrom.Location = new Point(0x6d, 30);
            this.cmb_cWHIdFrom.Name = "cmb_cWHIdFrom";
            this.cmb_cWHIdFrom.Size = new Size(0x79, 20);
            this.cmb_cWHIdFrom.TabIndex = 3;
            this.cmb_cWHIdFrom.Tag = "101";
            this.cmb_cWHIdFrom.Text = "Bind SelectedValue";
            this.txt_cLinkId.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cLinkId.Location = new Point(0x6d, 0x35);
            this.txt_cLinkId.Name = "txt_cLinkId";
            this.txt_cLinkId.ReadOnly = true;
            this.txt_cLinkId.Size = new Size(0x79, 0x15);
            this.txt_cLinkId.TabIndex = 6;
            this.txt_cLinkId.Tag = "0";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x16, 0x39);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x41, 12);
            this.label15.TabIndex = 0x1c;
            this.label15.Text = "来源单号：";
            this.txt_cChecker.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cChecker.Location = new Point(0x135, 7);
            this.txt_cChecker.Name = "txt_cChecker";
            this.txt_cChecker.ReadOnly = true;
            this.txt_cChecker.Size = new Size(0x70, 0x15);
            this.txt_cChecker.TabIndex = 1;
            this.txt_cChecker.Tag = "0";
            this.lblChecker.AutoSize = true;
            this.lblChecker.Location = new Point(0xf9, 11);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new Size(0x41, 12);
            this.lblChecker.TabIndex = 0x18;
            this.lblChecker.Text = "审 核 人：";
            this.tlb_M_Refresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Refresh.Image = (Image) manager.GetObject("tlb_M_Refresh.Image");
            this.tlb_M_Refresh.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new Size(0x23, 0x16);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new EventHandler(this.tlb_M_Refresh_Click);
            this.tlb_M_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Print.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = (Image) manager.GetObject("tlb_M_Print.Image");
            this.tlb_M_Print.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new Size(0x23, 0x16);
            this.tlb_M_Print.Tag = "06";
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Click += new EventHandler(this.tlb_M_Print_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(6, 0x19);
            this.tlb_M_Find.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Find.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Find.Image = (Image) manager.GetObject("tlb_M_Find.Image");
            this.tlb_M_Find.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new Size(0x23, 0x16);
            this.tlb_M_Find.Text = "查找";
            this.tlb_M_Find.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x16, 0x22);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x41, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "源 仓 库：";
            this.txt_cBNo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cBNo.Location = new Point(0x6d, 7);
            this.txt_cBNo.Name = "txt_cBNo";
            this.txt_cBNo.ReadOnly = true;
            this.txt_cBNo.Size = new Size(0x7a, 0x15);
            this.txt_cBNo.TabIndex = 0;
            this.txt_cBNo.Tag = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "单    号：";
            this.cmb_cPayer.FormattingEnabled = true;
            this.cmb_cPayer.Location = new Point(490, 7);
            this.cmb_cPayer.Name = "cmb_cPayer";
            this.cmb_cPayer.Size = new Size(0x79, 20);
            this.cmb_cPayer.TabIndex = 2;
            this.cmb_cPayer.Tag = "1";
            this.cmb_cPayer.Text = "Bind Text";
            this.label30.AutoSize = true;
            this.label30.ForeColor = SystemColors.ActiveCaption;
            this.label30.Location = new Point(0x1a7, 0x22);
            this.label30.Name = "label30";
            this.label30.Size = new Size(11, 12);
            this.label30.TabIndex = 0x35;
            this.label30.Text = "*";
            this.label23.AutoSize = true;
            this.label23.ForeColor = SystemColors.ActiveCaption;
            this.label23.Location = new Point(0x265, 0x22);
            this.label23.Name = "label23";
            this.label23.Size = new Size(11, 12);
            this.label23.TabIndex = 0x34;
            this.label23.Text = "*";
            this.label20.AutoSize = true;
            this.label20.ForeColor = SystemColors.ActiveCaption;
            this.label20.Location = new Point(0xea, 0x22);
            this.label20.Name = "label20";
            this.label20.Size = new Size(11, 12);
            this.label20.TabIndex = 0x33;
            this.label20.Text = "*";
            this.dtp_dCreateDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_dCreateDate.Format = DateTimePickerFormat.Custom;
            this.dtp_dCreateDate.Location = new Point(0x1eb, 30);
            this.dtp_dCreateDate.Name = "dtp_dCreateDate";
            this.dtp_dCreateDate.Size = new Size(120, 0x15);
            this.dtp_dCreateDate.TabIndex = 7;
            this.dtp_dCreateDate.Tag = "2";
            this.txtFindBillFrom.Location = new Point(0x4a, 0x40);
            this.txtFindBillFrom.Name = "txtFindBillFrom";
            this.txtFindBillFrom.Size = new Size(0xcf, 0x15);
            this.txtFindBillFrom.TabIndex = 12;
            this.lbl_Check.AutoSize = true;
            this.lbl_Check.BackColor = Color.Transparent;
            this.lbl_Check.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_Check.ForeColor = Color.Red;
            this.lbl_Check.Location = new Point(0x1ba, 0x53);
            this.lbl_Check.Name = "lbl_Check";
            this.lbl_Check.Size = new Size(0x2c, 12);
            this.lbl_Check.TabIndex = 0x36;
            this.lbl_Check.Text = "已审核";
            this.lbl_Check.Visible = false;
            this.pnlEdit.BackColor = SystemColors.Info;
            this.pnlEdit.Controls.Add(this.txt_cBNoIn);
            this.pnlEdit.Controls.Add(this.dtp_dCreateDate);
            this.pnlEdit.Controls.Add(this.cmb_cPayer);
            this.pnlEdit.Controls.Add(this.label31);
            this.pnlEdit.Controls.Add(this.label14);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.cmb_cMatClass);
            this.pnlEdit.Controls.Add(this.label38);
            this.pnlEdit.Controls.Add(this.txt_cBNoOut);
            this.pnlEdit.Controls.Add(this.label13);
            this.pnlEdit.Controls.Add(this.lbl_Check);
            this.pnlEdit.Controls.Add(this.label30);
            this.pnlEdit.Controls.Add(this.label23);
            this.pnlEdit.Controls.Add(this.label20);
            this.pnlEdit.Controls.Add(this.label22);
            this.pnlEdit.Controls.Add(this.cmb_cWHIdTo);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.cmb_nPStatus);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.txt_cRemark);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.cmb_cWHIdFrom);
            this.pnlEdit.Controls.Add(this.txt_cLinkId);
            this.pnlEdit.Controls.Add(this.label15);
            this.pnlEdit.Controls.Add(this.txt_cChecker);
            this.pnlEdit.Controls.Add(this.lblChecker);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.txt_cBNo);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Dock = DockStyle.Top;
            this.pnlEdit.Location = new Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new Size(0x287, 0xac);
            this.pnlEdit.TabIndex = 0;
            this.txt_cBNoIn.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cBNoIn.Location = new Point(0x1eb, 0x35);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.ReadOnly = true;
            this.txt_cBNoIn.Size = new Size(120, 0x15);
            this.txt_cBNoIn.TabIndex = 0x39;
            this.txt_cBNoIn.Tag = "0";
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x1af, 0x39);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x41, 12);
            this.label31.TabIndex = 0x3a;
            this.label31.Text = "入库单号：";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x1af, 11);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x41, 12);
            this.label14.TabIndex = 0x2d;
            this.label14.Text = "仓 管 员：";
            this.cmb_cMatClass.BackColor = SystemColors.Control;
            this.cmb_cMatClass.FormattingEnabled = true;
            this.cmb_cMatClass.Items.AddRange(new object[] { "轻度", "中度", "重度" });
            this.cmb_cMatClass.Location = new Point(0x135, 0x4f);
            this.cmb_cMatClass.Name = "cmb_cMatClass";
            this.cmb_cMatClass.Size = new Size(0x70, 20);
            this.cmb_cMatClass.TabIndex = 0x3b;
            this.cmb_cMatClass.Tag = "1";
            this.cmb_cMatClass.Text = "Bind SelectedValue";
            this.cmb_cMatClass.Visible = false;
            this.label38.AutoSize = true;
            this.label38.Location = new Point(0xf9, 0x53);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x41, 12);
            this.label38.TabIndex = 60;
            this.label38.Text = "物资种类：";
            this.label38.Visible = false;
            this.txt_cBNoOut.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cBNoOut.Location = new Point(0x135, 0x35);
            this.txt_cBNoOut.Name = "txt_cBNoOut";
            this.txt_cBNoOut.ReadOnly = true;
            this.txt_cBNoOut.Size = new Size(0x70, 0x15);
            this.txt_cBNoOut.TabIndex = 0x37;
            this.txt_cBNoOut.Tag = "0";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0xf9, 0x39);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x41, 12);
            this.label13.TabIndex = 0x38;
            this.label13.Text = "出库单号：";
            this.label22.AutoSize = true;
            this.label22.ForeColor = SystemColors.ActiveCaption;
            this.label22.Location = new Point(0x265, 11);
            this.label22.Name = "label22";
            this.label22.Size = new Size(11, 12);
            this.label22.TabIndex = 50;
            this.label22.Text = "*";
            this.cmb_cWHIdTo.FormattingEnabled = true;
            this.cmb_cWHIdTo.Location = new Point(0x135, 30);
            this.cmb_cWHIdTo.Name = "cmb_cWHIdTo";
            this.cmb_cWHIdTo.Size = new Size(0x70, 20);
            this.cmb_cWHIdTo.TabIndex = 5;
            this.cmb_cWHIdTo.Tag = "101";
            this.cmb_cWHIdTo.Text = "Bind Text";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xf9, 0x22);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 0x30;
            this.label5.Text = "目标仓库：";
            this.cmb_nPStatus.BackColor = SystemColors.Control;
            this.cmb_nPStatus.FormattingEnabled = true;
            this.cmb_nPStatus.Location = new Point(0x6d, 0x4f);
            this.cmb_nPStatus.Name = "cmb_nPStatus";
            this.cmb_nPStatus.Size = new Size(0x79, 20);
            this.cmb_nPStatus.TabIndex = 8;
            this.cmb_nPStatus.Tag = "101";
            this.cmb_nPStatus.Text = "Bind SelectedValue";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x16, 0x53);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 0x2e;
            this.label9.Text = "单据状态：";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(0x10, 0x2c);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x41, 12);
            this.label29.TabIndex = 0x15;
            this.label29.Text = "操 作 员：";
            this.cmbFindUser.FormattingEnabled = true;
            this.cmbFindUser.Items.AddRange(new object[] { "平面库", "立体库" });
            this.cmbFindUser.Location = new Point(0x4a, 40);
            this.cmbFindUser.Name = "cmbFindUser";
            this.cmbFindUser.Size = new Size(0x53, 20);
            this.cmbFindUser.TabIndex = 20;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(6, 0x19);
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
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new Size(0x2f, 0x11);
            this.stbUser.Text = "用户名:";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.tlb_M_ErpImp, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, 
                this.tlb_M_Check, this.tlb_M_UnCheck, this.toolStripSeparator7, this.tlb_M_BldBillIn, this.tlb_M_Item, this.toolStripSeparator8, this.btn_M_Help, this.tlb_M_Exit, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x3b3, 0x19);
            this.tlbMain.TabIndex = 30;
            this.tlbMain.Text = "toolStrip1";
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
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
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 0x19);
            this.tlb_M_Check.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Check.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Check.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Check.Image = (Image) manager.GetObject("tlb_M_Check.Image");
            this.tlb_M_Check.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Check.Name = "tlb_M_Check";
            this.tlb_M_Check.Size = new Size(0x23, 0x16);
            this.tlb_M_Check.Tag = "07";
            this.tlb_M_Check.Text = "审核";
            this.tlb_M_Check.Click += new EventHandler(this.tlb_M_Check_Click);
            this.tlb_M_UnCheck.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_UnCheck.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_UnCheck.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_UnCheck.Image = (Image) manager.GetObject("tlb_M_UnCheck.Image");
            this.tlb_M_UnCheck.ImageTransparentColor = Color.Magenta;
            this.tlb_M_UnCheck.Name = "tlb_M_UnCheck";
            this.tlb_M_UnCheck.Size = new Size(0x3d, 0x16);
            this.tlb_M_UnCheck.Tag = "08";
            this.tlb_M_UnCheck.Text = "取消审核";
            this.tlb_M_UnCheck.Click += new EventHandler(this.tlb_M_UnCheck_Click);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new Size(6, 0x19);
            this.tlb_M_BldBillIn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_BldBillIn.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_BldBillIn.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_BldBillIn.Image = (Image) manager.GetObject("tlb_M_BldBillIn.Image");
            this.tlb_M_BldBillIn.ImageTransparentColor = Color.Magenta;
            this.tlb_M_BldBillIn.Name = "tlb_M_BldBillIn";
            this.tlb_M_BldBillIn.Size = new Size(0x4a, 0x16);
            this.tlb_M_BldBillIn.Tag = "10";
            this.tlb_M_BldBillIn.Text = "生成入库单";
            this.tlb_M_BldBillIn.Click += new EventHandler(this.tlb_M_BldBillIn_Click);
            this.tlb_M_Item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Item.Image = (Image) manager.GetObject("tlb_M_Item.Image");
            this.tlb_M_Item.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Item.Name = "tlb_M_Item";
            this.tlb_M_Item.Size = new Size(0x21, 0x16);
            this.tlb_M_Item.Text = "物料";
            this.tlb_M_Item.Visible = false;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(6, 0x19);
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
            this.tlbSaveSysRts.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = (Image) manager.GetObject("tlbSaveSysRts.Image");
            this.tlbSaveSysRts.ImageTransparentColor = Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new Size(0x51, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new EventHandler(this.tlbSaveSysRts_Click);
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 0x1388;
            this.groupBox1.Controls.Add(this.cmbFindUser);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.txtFindBillFrom);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbFindType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnUnFind);
            this.groupBox1.Controls.Add(this.cmbFindCheck);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.dtpFind_E);
            this.groupBox1.Controls.Add(this.dtpFind_B);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnQry);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x128, 0x74);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x44);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 0x13;
            this.label4.Text = "来源单号：";
            this.stbState.Name = "stbState";
            this.stbState.Size = new Size(0x23, 0x11);
            this.stbState.Text = "状态:";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x279);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x3b3, 0x16);
            this.stbMain.TabIndex = 0x1c;
            this.stbMain.Text = "statusStrip1";
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new Size(0x23, 0x11);
            this.stbModul.Text = "模块:";
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new Size(0x23, 0x11);
            this.stbDateTime.Text = "时间:";
            this.pnlSplit.Dock = DockStyle.Fill;
            this.pnlSplit.Location = new Point(0, 0x19);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Panel1.Controls.Add(this.grdList);
            this.pnlSplit.Panel1.Controls.Add(this.groupBox1);
            this.pnlSplit.Panel2.Controls.Add(this.pnlDtl);
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = ImeMode.NoControl;
            this.pnlSplit.Size = new Size(0x3b3, 0x260);
            this.pnlSplit.SplitterDistance = 0x128;
            this.pnlSplit.TabIndex = 0x1d;
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3b3, 0x28f);
            base.Controls.Add(this.pnlSplit);
            base.Controls.Add(this.tlbMain);
            base.Controls.Add(this.stbMain);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "frmBillRemove";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "调拨单管理";
            base.Load += new EventHandler(this.frmBillRemove_Load);
            this.pnlDtlEdit.ResumeLayout(false);
            this.pnlDtlEdit.PerformLayout();
            this.pnlDtl.ResumeLayout(false);
            ((ISupportInitialize) this.grdDtl).EndInit();
            this.ppmDtl.ResumeLayout(false);
            this.pnlBtns.ResumeLayout(false);
            ((ISupportInitialize) this.grdList).EndInit();
            ((ISupportInitialize) this.bdsMain).EndInit();
            ((ISupportInitialize) this.bdsDtl).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadBaseItem()
        {
            this.LoadBaseItemFromDB();
            this.LoadBaseItemFromArr();
        }

        private void LoadBaseItemFromArr()
        {
            this.ArrBillState.Add(new DictionaryEntry("0", "初始化"));
            this.ArrBillState.Add(new DictionaryEntry("1", "明细"));
            this.ArrBillState.Add(new DictionaryEntry("2", "审核"));
            this.ArrBillState.Add(new DictionaryEntry("3", "已经分盘"));
            this.ArrBillState.Add(new DictionaryEntry("4", "已下达指令"));
            this.ArrBillState.Add(new DictionaryEntry("5", "执行指令"));
            this.ArrBillState.Add(new DictionaryEntry("9", "完成"));
            this.cmb_nPStatus.DataSource = this.ArrBillState;
            this.cmb_nPStatus.DisplayMember = "Value";
            this.cmb_nPStatus.ValueMember = "Key";
            this.ArrExecState.Add(new DictionaryEntry("0", "待组盘"));
            this.ArrExecState.Add(new DictionaryEntry("1", "组盘中"));
            this.ArrExecState.Add(new DictionaryEntry("2", "组盘结束"));
            this.ArrExecState.Add(new DictionaryEntry("3", "执行中"));
            this.ArrExecState.Add(new DictionaryEntry("4", "执行结束"));
            this.ArrQCState.Add(new DictionaryEntry("0", "待检"));
            this.ArrQCState.Add(new DictionaryEntry("1", "合格"));
            this.ArrQCState.Add(new DictionaryEntry("-1", "不合格"));
            this.ArrQCState1.Add(new DictionaryEntry("0", "待检"));
            this.ArrQCState1.Add(new DictionaryEntry("1", "合格"));
            this.ArrQCState1.Add(new DictionaryEntry("-1", "不合格"));
        }

        private void LoadBaseItemFromDB()
        {
            string sSql = "";
            string sErr = "";
            int wtWareType = (int) this.wtWareType;
            sSql = "select * from TWC_WareHouse where bUsed=1";
            sErr = "";
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            DataTable table3 = new DataTable();
            DataTable table4 = new DataTable();
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            if (sErr != "")
            {
                MessageBox.Show(sSql, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                table = dataBySql.Tables["data"].Copy();
                table2 = table.Copy();
                table3 = table.Copy();
                table4 = table.Copy();
                this.cmb_cWHIdFrom.DisplayMember = "cName";
                this.cmb_cWHIdFrom.ValueMember = "cWHId";
                this.cmb_cWHIdFrom.DataSource = table;
                this.cmb_cWHIdTo.DisplayMember = "cName";
                this.cmb_cWHIdTo.ValueMember = "cWHId";
                this.cmb_cWHIdTo.DataSource = table2;
                this.colcWHIdFrom.DisplayMember = "cName";
                this.colcWHIdFrom.ValueMember = "cWHId";
                this.colcWHIdFrom.DataPropertyName = "cWHIdFrom";
                this.colcWHIdFrom.DataSource = table3;
                this.colcWHIdTo.DisplayMember = "cName";
                this.colcWHIdTo.ValueMember = "cWHId";
                this.colcWHIdTo.DataPropertyName = "cWHIdTo";
                this.colcWHIdTo.DataSource = table4;
            }
            sSql = "select * from TPB_User where bUsed=1 ";
            DataSet set2 = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmbFindUser.DisplayMember = "cName";
            this.cmbFindUser.ValueMember = "cName";
            this.cmbFindUser.DataSource = set2.Tables["data"];
            DataTable table5 = set2.Tables["data"].Copy();
            this.cmb_cPayer.DisplayMember = "cName";
            this.cmb_cPayer.ValueMember = "cName";
            this.cmb_cPayer.DataSource = table5;
            sSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='物资种类' order by nSort,nId";
            DataSet set3 = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sSql, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                this.cmb_cMatClass.DisplayMember = "cItemName";
                this.cmb_cMatClass.ValueMember = "cItemName";
                this.cmb_cMatClass.DataSource = set3.Tables["data"];
            }
        }

        private void mi_Dtl_PrintBarCode_Click(object sender, EventArgs e)
        {
            if (this.bdsDtl.Count == 0)
            {
                MessageBox.Show("对不起，无物料明细可打印！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsDtl.Current;
                if (current != null)
                {
                    string str = current["cMNo"].ToString().Trim();
                    if (str.Trim() == "")
                    {
                        MessageBox.Show("物料条码为空！");
                    }
                    else
                    {
                        int num = 1;
                        string sValue = "";
                        if (UIPubMethode.InputMessage("录入打印份数：", "打印份数", "1", InputMsgType.ittInteger, out sValue))
                        {
                            num = int.Parse(sValue.Trim());
                        }
                        string str3 = Path.Combine(Application.StartupPath, @"rpt\rptMaterial5-3.fr3");
                        string dllFile = Path.Combine(Application.StartupPath, "BarCodeReport.dll");
                        string str5 = "";
                        bool bIsOK = false;
                        for (int i = 0; i < num; i++)
                        {
                            MyCallUnSafetyDll.DoCallMyDll(dllFile, "PrintBarCode", new object[] { str3, str, str5, 0 }, new Type[] { Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.Int32") }, new ModePass[] { ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue }, null, out bIsOK);
                        }
                    }
                }
            }
        }

        public bool OpenDtlDataSet(string sCon)
        {
            bool flag = false;
            string text = "";
            this.bDSIsOpenForDtl = false;
            this.grdDtl.AutoGenerateColumns = false;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sSql = "select cBNo,cMNo,cMName, cSpec,fQty,cUnit,cMatStyle,cMatQCLevel,cMatOther,cSupplier,cCSId  from V_RemoveDtl " + sCon;
            string sErr = "";
            this.dsD.Clear();
            this.dsD = PubDBCommFuns.GetDataBySql(sSql, "", out sErr);
            flag = sErr == "";
            if (!flag)
            {
                MessageBox.Show(text);
            }
            else
            {
                try
                {
                    this.bdsDtl.DataSource = this.dsD.Tables["data"];
                    this.BindDtlDataSetToCtrls();
                    this.ClearUIValues(this.pnlDtlEdit);
                    if (this.bdsDtl.Count > 0)
                    {
                        this.DataRowViewToUI((DataRowView) this.bdsDtl.Current, this.pnlDtlEdit);
                    }
                    flag = true;
                    this.optDtl = OperateType.optNone;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            this.bDSIsOpenForDtl = true;
            return flag;
        }

        public bool OpenMainDataSet(string sCon)
        {
            bool flag = false;
            string text = "";
            string str2 = "";
            this.bDSIsOpenForMain = false;
            this.grdList.AutoGenerateColumns = false;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sSql = "select * from TWB_BILLREMOVE " + sCon + " order by cBNo desc";
            string sErr = "";
            int position = this.bdsMain.Position;
            this.dsM.Clear();
            this.dsM = PubDBCommFuns.GetDataBySql(sSql, "dDate,dCheckDate,dCreateDate,dEditDate", out sErr);
            flag = sErr == "";
            if (!flag)
            {
                MessageBox.Show(text);
            }
            else
            {
                try
                {
                    str2 = "";
                    DataTable table = this.dsM.Tables["data"];
                    this.bdsMain.DataSource = table;
                    this.BindMainDataSetToCtrls();
                    this.bdsMain.Position = position;
                    this.ClearUIValues(this.pnlEdit);
                    this.lbl_Check.Visible = false;
                    if (this.bdsMain.Count > 0)
                    {
                        DataRowView current = (DataRowView) this.bdsMain.Current;
                        this.DataRowViewToUI(current, this.pnlEdit);
                        if ((current["bIsChecked"].ToString().Trim() == "0") && (current["cChecker"].ToString().Trim() != ""))
                        {
                            this.lblChecker.Text = "取消审核人";
                        }
                        else
                        {
                            this.lblChecker.Text = "审核人：";
                        }
                        this.lbl_Check.Visible = current["bIsChecked"].ToString().Trim() == "1";
                        str2 = current["cBNo"].ToString();
                    }
                    this.OpenDtlDataSet(" where cBNo='" + str2 + "'");
                    flag = true;
                    this.optMain = OperateType.optNone;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            this.bDSIsOpenForMain = true;
            return flag;
        }

        private void pnlDtlEdit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ppmDtl_Opening(object sender, CancelEventArgs e)
        {
        }

        private void tlb_M_BldBillIn_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current == null)
            {
                MessageBox.Show("对不起，无数据可审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (current["cBNoIn"].ToString().Trim() != "")
            {
                MessageBox.Show("对不起，该调拨单已经生成了调拨入库单：" + current["cBNoIn"].ToString().Trim(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string sErr = "";
                if (PubDBCommFuns.sp_Pack_BldRemoveInData(base.AppInformation.SvrSocket, int.Parse(current["nBClass"].ToString()), current["cBNo"].ToString(), base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", out sErr).Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show(sErr);
                    this.btnQry_Click(null, null);
                }
            }
        }

        private void tlb_M_Check_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current == null)
            {
                MessageBox.Show("对不起，无数据可审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((base.UserInformation.UType == UserType.utNormal) && (current["cCreator"].ToString().Trim() != base.UserInformation.UserName.Trim()))
            {
                MessageBox.Show("对不起，你无权限审核或取消审核");
            }
            else if (current["bIsChecked"].ToString().ToLower() != "1")
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
                    ((DataTable) this.bdsMain.DataSource).AcceptChanges();
                    this.DataRowViewToUI(current, this.pnlEdit);
                    this.lbl_Check.Visible = true;
                    this.lblChecker.Text = "审核人：";
                }
            }
            else
            {
                MessageBox.Show("对不起，该单已被审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            this.DoMDelete();
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            this.DoMEdit();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            this.DoMNew();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.btnQry_Click(sender, e);
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            this.DoMSave();
        }

        private void tlb_M_UnCheck_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current == null)
            {
                MessageBox.Show("对不起，无数据可审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((base.UserInformation.UType == UserType.utNormal) && (current["cCreator"].ToString().Trim() != base.UserInformation.UserName.Trim()))
            {
                MessageBox.Show("对不起，你无权限审核或取消审核");
            }
            else if (current["bIsChecked"].ToString().ToLower() == "1")
            {
                string sErr = "";
                if (PubDBCommFuns.sp_Pack_BillCheck(base.AppInformation.SvrSocket, int.Parse(current["nBClass"].ToString()), current["cBNo"].ToString(), 1, base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", out sErr).Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show("取消审核成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    current.BeginEdit();
                    current["bIsChecked"] = 0;
                    current["dCheckDate"] = DateTime.Now;
                    current["cChecker"] = base.UserInformation.UserName;
                    current.EndEdit();
                    ((DataTable) this.bdsMain.DataSource).AcceptChanges();
                    this.DataRowViewToUI(current, this.pnlEdit);
                    this.lbl_Check.Visible = false;
                    this.lblChecker.Text = "取消审核人：";
                }
            }
            else
            {
                MessageBox.Show("对不起，该单未被审核，不能取消审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            this.DoMUndo();
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

        private void txt_cBNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            base.ChangeTextBoxBkColorByReadOnly(sender, ((Control) sender).Parent.BackColor, Color.White);
        }

        [Description("仓库类型")]
        public WareType WTWareType
        {
            get
            {
                return this.wtWareType;
            }
            set
            {
                this.wtWareType = value;
                this.Text = this.GetTitleText();
            }
        }
    }
}

