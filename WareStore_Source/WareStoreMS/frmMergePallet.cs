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

    public class frmMergePallet : FrmSTable
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
        private ToolStripButton btn_M_Help;
        private Button btn_SelPosFrom;
        private Button btn_SelPosTo;
        private Button btnQry;
        private Button btnUnFind;
        private ComboBox cmb_cCreator;
        private ComboBox cmb_FinishedStatus;
        private ComboBox cmb_nWkTskFromIsEmptyOut;
        private ComboBox cmbFindCheck;
        private ComboBox cmbFindUser;
        private DataGridViewTextBoxColumn col_Dtl_cBNoIn;
        private DataGridViewTextBoxColumn col_Dtl_cWHIdErp;
        private DataGridViewTextBoxColumn col_Dtl_dInTime;
        private DataGridViewTextBoxColumn col_Dtl_fFinished;
        private DataGridViewTextBoxColumn col_Dtl_nItemIn;
        private DataGridViewTextBoxColumn col_Main_bIsChecked;
        private DataGridViewTextBoxColumn col_Main_cCreator;
        private DataGridViewTextBoxColumn col_Main_nBClass;
        private DataGridViewTextBoxColumn colcBatchNo;
        private DataGridViewTextBoxColumn colcBId;
        private DataGridViewTextBoxColumn colcMName;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn colcUnit;
        private DataGridViewTextBoxColumn coldProdDate;
        private DataGridViewTextBoxColumn colfQty;
        private DataGridViewTextBoxColumn colnItem;
        private DataGridViewTextBoxColumn colnQCStatus;
        private IContainer components = null;
        private DataSet dsD = new DataSet();
        private DataSet dsM = new DataSet();
        private DateTimePicker dtp_dCreateDate;
        private DateTimePicker dtpFind_B;
        private DateTimePicker dtpFind_E;
        public DataGridView grdDtl;
        public DataGridView grdList;
        private GroupBox groupBox1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label14;
        private Label label15;
        private Label label17;
        private Label label2;
        private Label label20;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label4;
        private Label label44;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl_BillTskIsOver;
        private Label lbl_Check;
        private Label lbl_D_Count;
        private Label lbl_M_Count;
        private Label lblChecker;
        private OperateType optDtl = OperateType.optNone;
        private OperateType optMain = OperateType.optNone;
        private Panel panel1;
        public Panel pnlEdit;
        private Panel pnlListRecCount;
        private SplitContainer splitContainer1;
        private StatusStrip statusStrip1;
        private string strTbNameDtl = "TWB_BillMergePltDtl";
        private string strTbNameMain = "TWB_BillMergePlt";
        public ToolStripButton tlb_M_Check;
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_Edit;
        public ToolStripButton tlb_M_ErpImp;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Find;
        private ToolStripButton tlb_M_Item;
        public ToolStripButton tlb_M_New;
        private ToolStripButton tlb_M_OverBWK;
        public ToolStripButton tlb_M_Print;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Save;
        private ToolStripButton tlb_M_UnCheck;
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
        private TextBox txt_cBNo;
        private TextBox txt_cChecker;
        private TextBox txt_cPosIdFrom;
        private TextBox txt_cPosIdTo;
        private TextBox txt_nPalletIdFrom;
        private TextBox txt_nPalletIdTo;
        private TextBox txt_nWorkIdFrom;
        private TextBox txt_nWorkIdTo;
        private TextBox txtFindBillNo;
        private WareType wtWareType = WareType.wt3D;

        public frmMergePallet()
        {
            this.InitializeComponent();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDSIsOpenForMain)
            {
                string str = "";
                this.ClearUIValues(this.pnlEdit);
                DataRowView drv = null;
                drv = (DataRowView) this.bdsMain.Current;
                if (drv != null)
                {
                    if (!drv.IsNew)
                    {
                        this.DataRowViewToUI(drv, this.pnlEdit);
                    }
                    str = drv["cBNo"].ToString();
                }
                this.OpenDtlDataSet(" where cBNo='" + str + "'");
            }
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

        private void btnQry_Click(object sender, EventArgs e)
        {
            if (this.dtpFind_B.Value > this.dtpFind_E.Value)
            {
                MessageBox.Show("对不起，起始时间不能大于截止时间！");
                this.dtpFind_B.Focus();
            }
            else
            {
                StringBuilder builder = new StringBuilder("select * from V_TWB_BillMergePlt where (dCreateDate >='" + this.dtpFind_B.Value.ToString("yyyy-MM-dd 00:00:00") + "' and dCreateDate <='" + this.dtpFind_E.Value.ToString("yyyy-MM-dd 23:59:59") + "')");
                if (((this.cmbFindUser.Text.Trim() != "") && (this.cmbFindUser.Text.Trim() != "全部")) && (this.cmbFindUser.SelectedValue != null))
                {
                    builder.Append(" and cCreator='" + this.cmbFindUser.SelectedValue.ToString().Trim() + "'");
                }
                if (this.txtFindBillNo.Text.Trim() != "")
                {
                    builder.Append(" and cBNo like '%" + this.txtFindBillNo.Text.Trim() + "%'");
                }
                if ((((this.cmbFindCheck.Text.Trim() != "") && (this.cmbFindCheck.Text.Trim() != "全部")) && (this.cmbFindCheck.SelectedIndex >= 0)) && (this.cmbFindCheck.SelectedIndex <= 1))
                {
                    builder.Append(" and isnull(bIsChecked,0)='" + this.cmbFindCheck.SelectedIndex.ToString().Trim() + "'");
                }
                if ((((this.cmb_FinishedStatus.Text.Trim() != "") && (this.cmb_FinishedStatus.Text.Trim() != "全部")) && (this.cmb_FinishedStatus.SelectedIndex >= 0)) && (this.cmb_FinishedStatus.SelectedIndex <= 1))
                {
                    builder.Append(" and isnull(bIsFinished,0)='" + this.cmb_FinishedStatus.SelectedIndex.ToString().Trim() + "'");
                }
                DataSet set = null;
                string sErr = "";
                Cursor.Current = Cursors.WaitCursor;
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), this.strTbNameMain, 0, 0, "dCreateDate,dCheckDate", out sErr);
                Cursor.Current = Cursors.Default;
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else if (set == null)
                {
                    MessageBox.Show("获取数据时失败！");
                }
                else
                {
                    DataTable table = null;
                    if (set.Tables.Count > 0)
                    {
                        if (set.Tables[0].Rows[0][0].ToString() == "-1")
                        {
                            MessageBox.Show(set.Tables[0].Rows[0][1].ToString());
                            return;
                        }
                        if (set.Tables[this.strTbNameMain] != null)
                        {
                            table = set.Tables[this.strTbNameMain].Copy();
                        }
                        if (table == null)
                        {
                            MessageBox.Show("获取数据时失败！");
                            return;
                        }
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    this.bDSIsOpenForMain = false;
                    this.bdsMain.DataSource = table;
                    this.grdList.DataSource = this.bdsMain;
                    this.bDSIsOpenForMain = true;
                    this.lbl_M_Count.Text = this.bdsMain.Count.ToString();
                    Cursor.Current = Cursors.Default;
                    this.bdsMain_PositionChanged(null, null);
                }
            }
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
                            this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
                            this.optMain = OperateType.optNone;
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
                    this.CtrlControlReadOnly(this.pnlEdit, true);
                    this.txt_cBNo.ReadOnly = true;
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            this.DataRowViewToUI(drv, this.pnlEdit);
            this.lblChecker.Text = "审核人：";
            this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
            this.txt_cBNo.Focus();
            this.CtrlControlReadOnly(this.pnlEdit, true);
            this.txt_cBNo.ReadOnly = true;
            this.txt_cChecker.ReadOnly = true;
        }

        public void DoMSave()
        {
            this.txt_cBNo.Focus();
            if ((this.cmb_cCreator.Text.Trim() == "") || (this.cmb_cCreator.SelectedValue == null))
            {
                MessageBox.Show("对不起，仓管员不能为空！");
                this.cmb_cCreator.Focus();
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
                    if (PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, sSql, DBSQLCommandInfo.GetFieldsForDate(current), out sErr).Tables[0].Rows[0].ItemArray[0].ToString() == "0")
                    {
                        this.optMain = OperateType.optSave;
                        MessageBox.Show("保存主表数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((DataTable) this.bdsMain.DataSource).AcceptChanges();
                        this.bdsMain_PositionChanged(null, null);
                        this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, (DataTable) this.bdsMain.DataSource);
                        this.optMain = OperateType.optNone;
                        this.CtrlControlReadOnly(this.pnlEdit, false);
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
                this.CtrlControlReadOnly(this.pnlEdit, false);
            }
        }

        private void frmMergePallet_Load(object sender, EventArgs e)
        {
            this.dtpFind_B.Value = DateTime.Now.AddDays(-11.0);
            this.dtpFind_E.Value = DateTime.Now;
            this.grdList.AutoGenerateColumns = false;
            this.grdDtl.AutoGenerateColumns = false;
        }

        public string GetNewId()
        {
            string strTbNameMain = this.strTbNameMain;
            string str2 = "cBNo";
            string str3 = "BMP" + DateTime.Now.ToString("yyMMdd");
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
            base.ModuleRtsId = "3411";
            base.ModuleRtsName = "合盘管理";
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmMergePallet));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
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
            this.tlb_M_Print = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.tlb_M_Check = new ToolStripButton();
            this.tlb_M_UnCheck = new ToolStripButton();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.tlb_M_OverBWK = new ToolStripButton();
            this.tlb_M_Item = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.tlbSaveSysRts = new ToolStripButton();
            this.statusStrip1 = new StatusStrip();
            this.splitContainer1 = new SplitContainer();
            this.grdList = new DataGridView();
            this.pnlListRecCount = new Panel();
            this.lbl_M_Count = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.cmb_FinishedStatus = new ComboBox();
            this.label44 = new Label();
            this.txtFindBillNo = new TextBox();
            this.label4 = new Label();
            this.btnUnFind = new Button();
            this.cmbFindCheck = new ComboBox();
            this.label28 = new Label();
            this.cmbFindUser = new ComboBox();
            this.label29 = new Label();
            this.label24 = new Label();
            this.dtpFind_E = new DateTimePicker();
            this.dtpFind_B = new DateTimePicker();
            this.label8 = new Label();
            this.btnQry = new Button();
            this.panel1 = new Panel();
            this.lbl_D_Count = new Label();
            this.label17 = new Label();
            this.grdDtl = new DataGridView();
            this.pnlEdit = new Panel();
            this.btn_SelPosTo = new Button();
            this.btn_SelPosFrom = new Button();
            this.cmb_nWkTskFromIsEmptyOut = new ComboBox();
            this.label6 = new Label();
            this.txt_nWorkIdTo = new TextBox();
            this.label11 = new Label();
            this.txt_nWorkIdFrom = new TextBox();
            this.label9 = new Label();
            this.txt_nPalletIdTo = new TextBox();
            this.label12 = new Label();
            this.txt_nPalletIdFrom = new TextBox();
            this.label10 = new Label();
            this.txt_cPosIdTo = new TextBox();
            this.label7 = new Label();
            this.lbl_BillTskIsOver = new Label();
            this.lbl_Check = new Label();
            this.label30 = new Label();
            this.label23 = new Label();
            this.label20 = new Label();
            this.label22 = new Label();
            this.label14 = new Label();
            this.cmb_cCreator = new ComboBox();
            this.label3 = new Label();
            this.dtp_dCreateDate = new DateTimePicker();
            this.txt_cPosIdFrom = new TextBox();
            this.label15 = new Label();
            this.txt_cChecker = new TextBox();
            this.lblChecker = new Label();
            this.txt_cBNo = new TextBox();
            this.label5 = new Label();
            this.bdsMain = new BindingSource(this.components);
            this.bdsDtl = new BindingSource(this.components);
            this.colnItem = new DataGridViewTextBoxColumn();
            this.colcMNo = new DataGridViewTextBoxColumn();
            this.colcMName = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.colcBatchNo = new DataGridViewTextBoxColumn();
            this.colfQty = new DataGridViewTextBoxColumn();
            this.col_Dtl_fFinished = new DataGridViewTextBoxColumn();
            this.colnQCStatus = new DataGridViewTextBoxColumn();
            this.colcUnit = new DataGridViewTextBoxColumn();
            this.coldProdDate = new DataGridViewTextBoxColumn();
            this.col_Dtl_dInTime = new DataGridViewTextBoxColumn();
            this.col_Dtl_cBNoIn = new DataGridViewTextBoxColumn();
            this.col_Dtl_nItemIn = new DataGridViewTextBoxColumn();
            this.col_Dtl_cWHIdErp = new DataGridViewTextBoxColumn();
            this.colcBId = new DataGridViewTextBoxColumn();
            this.col_Main_cCreator = new DataGridViewTextBoxColumn();
            this.col_Main_nBClass = new DataGridViewTextBoxColumn();
            this.col_Main_bIsChecked = new DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            this.pnlListRecCount.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.grdDtl).BeginInit();
            this.pnlEdit.SuspendLayout();
            ((ISupportInitialize) this.bdsMain).BeginInit();
            ((ISupportInitialize) this.bdsDtl).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.tlb_M_ErpImp, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, 
                this.tlb_M_Check, this.tlb_M_UnCheck, this.toolStripSeparator7, this.tlb_M_OverBWK, this.tlb_M_Item, this.toolStripSeparator8, this.btn_M_Help, this.tlb_M_Exit, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x467, 0x19);
            this.tlbMain.TabIndex = 0x1f;
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
            this.tlb_M_ErpImp.Visible = false;
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
            this.tlb_M_Save.Visible = false;
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
            this.tlb_M_Refresh.Visible = false;
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
            this.tlb_M_OverBWK.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_OverBWK.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_OverBWK.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_OverBWK.Image = (Image) manager.GetObject("tlb_M_OverBWK.Image");
            this.tlb_M_OverBWK.ImageTransparentColor = Color.Magenta;
            this.tlb_M_OverBWK.Name = "tlb_M_OverBWK";
            this.tlb_M_OverBWK.Size = new Size(0x57, 0x16);
            this.tlb_M_OverBWK.Tag = "09";
            this.tlb_M_OverBWK.Text = "完成单据作业";
            this.tlb_M_OverBWK.Visible = false;
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
            this.statusStrip1.Location = new Point(0, 0x20c);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x467, 0x16);
            this.statusStrip1.TabIndex = 0x20;
            this.statusStrip1.Text = "statusStrip1";
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0x19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.grdList);
            this.splitContainer1.Panel1.Controls.Add(this.pnlListRecCount);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.grdDtl);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.pnlEdit);
            this.splitContainer1.Size = new Size(0x467, 0x1f3);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0x21;
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = style;
            this.grdList.ColumnHeadersHeight = 0x2d;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colcBId, this.col_Main_cCreator, this.col_Main_nBClass, this.col_Main_bIsChecked });
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = SystemColors.Window;
            style2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style2.ForeColor = SystemColors.ControlText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = style2;
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new Point(0, 0x7c);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = SystemColors.Control;
            style3.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style3.ForeColor = SystemColors.WindowText;
            style3.SelectionBackColor = SystemColors.Highlight;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = style3;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 0x17;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new Size(300, 0x159);
            this.grdList.TabIndex = 5;
            this.grdList.Tag = "8";
            this.pnlListRecCount.Controls.Add(this.lbl_M_Count);
            this.pnlListRecCount.Controls.Add(this.label2);
            this.pnlListRecCount.Dock = DockStyle.Bottom;
            this.pnlListRecCount.Location = new Point(0, 0x1d5);
            this.pnlListRecCount.Name = "pnlListRecCount";
            this.pnlListRecCount.Size = new Size(300, 30);
            this.pnlListRecCount.TabIndex = 4;
            this.lbl_M_Count.AutoSize = true;
            this.lbl_M_Count.Location = new Point(0x42, 9);
            this.lbl_M_Count.Name = "lbl_M_Count";
            this.lbl_M_Count.Size = new Size(11, 12);
            this.lbl_M_Count.TabIndex = 1;
            this.lbl_M_Count.Text = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "记录数：";
            this.groupBox1.Controls.Add(this.cmb_FinishedStatus);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.txtFindBillNo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnUnFind);
            this.groupBox1.Controls.Add(this.cmbFindCheck);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.cmbFindUser);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.dtpFind_E);
            this.groupBox1.Controls.Add(this.dtpFind_B);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnQry);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(300, 0x7c);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.cmb_FinishedStatus.FormattingEnabled = true;
            this.cmb_FinishedStatus.Items.AddRange(new object[] { "未完成", "已完成", "全部" });
            this.cmb_FinishedStatus.Location = new Point(0xd5, 60);
            this.cmb_FinishedStatus.Name = "cmb_FinishedStatus";
            this.cmb_FinishedStatus.Size = new Size(0x45, 20);
            this.cmb_FinishedStatus.TabIndex = 0x18;
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0x9d, 0x3f);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x41, 12);
            this.label44.TabIndex = 0x19;
            this.label44.Text = "完成状态：";
            this.txtFindBillNo.Location = new Point(0xc0, 0x21);
            this.txtFindBillNo.Name = "txtFindBillNo";
            this.txtFindBillNo.Size = new Size(90, 0x15);
            this.txtFindBillNo.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x9d, 0x26);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 0x13;
            this.label4.Text = "单号：";
            this.btnUnFind.Location = new Point(0xf3, 0x56);
            this.btnUnFind.Name = "btnUnFind";
            this.btnUnFind.Size = new Size(0x27, 0x17);
            this.btnUnFind.TabIndex = 0x10;
            this.btnUnFind.Text = "重置";
            this.btnUnFind.UseVisualStyleBackColor = true;
            this.cmbFindCheck.FormattingEnabled = true;
            this.cmbFindCheck.Items.AddRange(new object[] { "未审核", "审核", "全部" });
            this.cmbFindCheck.Location = new Point(0x3d, 60);
            this.cmbFindCheck.Name = "cmbFindCheck";
            this.cmbFindCheck.Size = new Size(0x4e, 20);
            this.cmbFindCheck.TabIndex = 13;
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0, 0x3f);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x41, 12);
            this.label28.TabIndex = 15;
            this.label28.Text = "审核状态：";
            this.cmbFindUser.FormattingEnabled = true;
            this.cmbFindUser.Items.AddRange(new object[] { "平面库", "立体库" });
            this.cmbFindUser.Location = new Point(0x26, 0x22);
            this.cmbFindUser.Name = "cmbFindUser";
            this.cmbFindUser.Size = new Size(0x59, 20);
            this.cmbFindUser.TabIndex = 10;
            this.label29.AutoSize = true;
            this.label29.Location = new Point(0, 0x25);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x29, 12);
            this.label29.TabIndex = 13;
            this.label29.Text = "操作员";
            this.label24.BackColor = SystemColors.ControlText;
            this.label24.BorderStyle = BorderStyle.FixedSingle;
            this.label24.Location = new Point(0x9a, 0x13);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x11, 1);
            this.label24.TabIndex = 10;
            this.dtpFind_E.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_E.Format = DateTimePickerFormat.Custom;
            this.dtpFind_E.Location = new Point(0xc0, 9);
            this.dtpFind_E.Name = "dtpFind_E";
            this.dtpFind_E.Size = new Size(90, 0x15);
            this.dtpFind_E.TabIndex = 9;
            this.dtpFind_E.Tag = "2";
            this.dtpFind_B.CustomFormat = "yyyy-MM-dd";
            this.dtpFind_B.Format = DateTimePickerFormat.Custom;
            this.dtpFind_B.Location = new Point(0x26, 9);
            this.dtpFind_B.Name = "dtpFind_B";
            this.dtpFind_B.Size = new Size(0x59, 0x15);
            this.dtpFind_B.TabIndex = 8;
            this.dtpFind_B.Tag = "2";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0, 14);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x29, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "日期：";
            this.btnQry.Location = new Point(0xb6, 0x56);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new Size(0x27, 0x17);
            this.btnQry.TabIndex = 14;
            this.btnQry.Text = "查询";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new EventHandler(this.btnQry_Click);
            this.panel1.Controls.Add(this.lbl_D_Count);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x1d5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x337, 30);
            this.panel1.TabIndex = 5;
            this.lbl_D_Count.AutoSize = true;
            this.lbl_D_Count.Location = new Point(0x42, 9);
            this.lbl_D_Count.Name = "lbl_D_Count";
            this.lbl_D_Count.Size = new Size(11, 12);
            this.lbl_D_Count.TabIndex = 1;
            this.lbl_D_Count.Text = "0";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(9, 9);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x35, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "记录数：";
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.AllowUserToDeleteRows = false;
            this.grdDtl.AllowUserToOrderColumns = true;
            style4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style4.BackColor = SystemColors.Control;
            style4.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style4.ForeColor = SystemColors.WindowText;
            style4.SelectionBackColor = SystemColors.Highlight;
            style4.SelectionForeColor = SystemColors.HighlightText;
            style4.WrapMode = DataGridViewTriState.True;
            this.grdDtl.ColumnHeadersDefaultCellStyle = style4;
            this.grdDtl.ColumnHeadersHeight = 0x23;
            this.grdDtl.Columns.AddRange(new DataGridViewColumn[] { this.colnItem, this.colcMNo, this.colcMName, this.colcSpec, this.colcBatchNo, this.colfQty, this.col_Dtl_fFinished, this.colnQCStatus, this.colcUnit, this.coldProdDate, this.col_Dtl_dInTime, this.col_Dtl_cBNoIn, this.col_Dtl_nItemIn, this.col_Dtl_cWHIdErp });
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.BackColor = SystemColors.Window;
            style5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style5.ForeColor = SystemColors.ControlText;
            style5.SelectionBackColor = SystemColors.Highlight;
            style5.SelectionForeColor = SystemColors.HighlightText;
            style5.WrapMode = DataGridViewTriState.False;
            this.grdDtl.DefaultCellStyle = style5;
            this.grdDtl.Dock = DockStyle.Fill;
            this.grdDtl.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdDtl.Location = new Point(0, 0x83);
            this.grdDtl.MultiSelect = false;
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            style6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style6.BackColor = SystemColors.Control;
            style6.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style6.ForeColor = SystemColors.WindowText;
            style6.SelectionBackColor = SystemColors.Highlight;
            style6.SelectionForeColor = SystemColors.HighlightText;
            style6.WrapMode = DataGridViewTriState.True;
            this.grdDtl.RowHeadersDefaultCellStyle = style6;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 0x17;
            this.grdDtl.Size = new Size(0x337, 0x152);
            this.grdDtl.TabIndex = 3;
            this.grdDtl.Tag = "8";
            this.pnlEdit.BackColor = SystemColors.Info;
            this.pnlEdit.Controls.Add(this.btn_SelPosTo);
            this.pnlEdit.Controls.Add(this.btn_SelPosFrom);
            this.pnlEdit.Controls.Add(this.cmb_nWkTskFromIsEmptyOut);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.txt_nWorkIdTo);
            this.pnlEdit.Controls.Add(this.label11);
            this.pnlEdit.Controls.Add(this.txt_nWorkIdFrom);
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.txt_nPalletIdTo);
            this.pnlEdit.Controls.Add(this.label12);
            this.pnlEdit.Controls.Add(this.txt_nPalletIdFrom);
            this.pnlEdit.Controls.Add(this.label10);
            this.pnlEdit.Controls.Add(this.txt_cPosIdTo);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.lbl_BillTskIsOver);
            this.pnlEdit.Controls.Add(this.lbl_Check);
            this.pnlEdit.Controls.Add(this.label30);
            this.pnlEdit.Controls.Add(this.label23);
            this.pnlEdit.Controls.Add(this.label20);
            this.pnlEdit.Controls.Add(this.label22);
            this.pnlEdit.Controls.Add(this.label14);
            this.pnlEdit.Controls.Add(this.cmb_cCreator);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.dtp_dCreateDate);
            this.pnlEdit.Controls.Add(this.txt_cPosIdFrom);
            this.pnlEdit.Controls.Add(this.label15);
            this.pnlEdit.Controls.Add(this.txt_cChecker);
            this.pnlEdit.Controls.Add(this.lblChecker);
            this.pnlEdit.Controls.Add(this.txt_cBNo);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Dock = DockStyle.Top;
            this.pnlEdit.Location = new Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new Size(0x337, 0x83);
            this.pnlEdit.TabIndex = 1;
            this.btn_SelPosTo.Location = new Point(0xce, 0x3e);
            this.btn_SelPosTo.Name = "btn_SelPosTo";
            this.btn_SelPosTo.Size = new Size(0x17, 0x17);
            this.btn_SelPosTo.TabIndex = 0x47;
            this.btn_SelPosTo.Text = "…";
            this.btn_SelPosTo.UseVisualStyleBackColor = true;
            this.btn_SelPosTo.Visible = false;
            this.btn_SelPosFrom.Location = new Point(0xce, 0x24);
            this.btn_SelPosFrom.Name = "btn_SelPosFrom";
            this.btn_SelPosFrom.Size = new Size(0x17, 0x17);
            this.btn_SelPosFrom.TabIndex = 70;
            this.btn_SelPosFrom.Text = "…";
            this.btn_SelPosFrom.UseVisualStyleBackColor = true;
            this.btn_SelPosFrom.Visible = false;
            this.cmb_nWkTskFromIsEmptyOut.FormattingEnabled = true;
            this.cmb_nWkTskFromIsEmptyOut.Items.AddRange(new object[] { "否", "是" });
            this.cmb_nWkTskFromIsEmptyOut.Location = new Point(0x210, 0x59);
            this.cmb_nWkTskFromIsEmptyOut.Name = "cmb_nWkTskFromIsEmptyOut";
            this.cmb_nWkTskFromIsEmptyOut.Size = new Size(0x79, 20);
            this.cmb_nWkTskFromIsEmptyOut.TabIndex = 0x44;
            this.cmb_nWkTskFromIsEmptyOut.Visible = false;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x1be, 0x5d);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x59, 12);
            this.label6.TabIndex = 0x45;
            this.label6.Text = "是否源盘整出：";
            this.label6.Visible = false;
            this.txt_nWorkIdTo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_nWorkIdTo.Location = new Point(0x133, 0x5b);
            this.txt_nWorkIdTo.Name = "txt_nWorkIdTo";
            this.txt_nWorkIdTo.ReadOnly = true;
            this.txt_nWorkIdTo.Size = new Size(0x79, 0x15);
            this.txt_nWorkIdTo.TabIndex = 0x42;
            this.txt_nWorkIdTo.Tag = "0";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xec, 0x5f);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x4d, 12);
            this.label11.TabIndex = 0x43;
            this.label11.Text = "目标任务号：";
            this.txt_nWorkIdFrom.BorderStyle = BorderStyle.FixedSingle;
            this.txt_nWorkIdFrom.Location = new Point(0x4b, 0x5c);
            this.txt_nWorkIdFrom.Name = "txt_nWorkIdFrom";
            this.txt_nWorkIdFrom.ReadOnly = true;
            this.txt_nWorkIdFrom.Size = new Size(0x83, 0x15);
            this.txt_nWorkIdFrom.TabIndex = 0x40;
            this.txt_nWorkIdFrom.Tag = "0";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(3, 0x60);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4d, 12);
            this.label9.TabIndex = 0x41;
            this.label9.Text = "来源任务号：";
            this.txt_nPalletIdTo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_nPalletIdTo.Location = new Point(0x133, 0x41);
            this.txt_nPalletIdTo.Name = "txt_nPalletIdTo";
            this.txt_nPalletIdTo.ReadOnly = true;
            this.txt_nPalletIdTo.Size = new Size(0x79, 0x15);
            this.txt_nPalletIdTo.TabIndex = 0x3e;
            this.txt_nPalletIdTo.Tag = "0";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xec, 0x45);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x4d, 12);
            this.label12.TabIndex = 0x3f;
            this.label12.Text = "目标托盘号：";
            this.txt_nPalletIdFrom.BorderStyle = BorderStyle.FixedSingle;
            this.txt_nPalletIdFrom.Location = new Point(0x133, 0x26);
            this.txt_nPalletIdFrom.Name = "txt_nPalletIdFrom";
            this.txt_nPalletIdFrom.ReadOnly = true;
            this.txt_nPalletIdFrom.Size = new Size(0x79, 0x15);
            this.txt_nPalletIdFrom.TabIndex = 0x3b;
            this.txt_nPalletIdFrom.Tag = "0";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0xed, 0x2a);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x4d, 12);
            this.label10.TabIndex = 60;
            this.label10.Text = "来源托盘号：";
            this.txt_cPosIdTo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cPosIdTo.Location = new Point(0x4c, 0x3f);
            this.txt_cPosIdTo.Name = "txt_cPosIdTo";
            this.txt_cPosIdTo.ReadOnly = true;
            this.txt_cPosIdTo.Size = new Size(0x83, 0x15);
            this.txt_cPosIdTo.TabIndex = 0x39;
            this.txt_cPosIdTo.Tag = "0";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(3, 0x45);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x41, 12);
            this.label7.TabIndex = 0x3a;
            this.label7.Text = "目标货位：";
            this.lbl_BillTskIsOver.AutoSize = true;
            this.lbl_BillTskIsOver.BackColor = Color.Transparent;
            this.lbl_BillTskIsOver.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_BillTskIsOver.ForeColor = Color.Red;
            this.lbl_BillTskIsOver.Location = new Point(0x20e, 0x41);
            this.lbl_BillTskIsOver.Name = "lbl_BillTskIsOver";
            this.lbl_BillTskIsOver.Size = new Size(0x60, 12);
            this.lbl_BillTskIsOver.TabIndex = 0x38;
            this.lbl_BillTskIsOver.Text = "单据作业已完成";
            this.lbl_BillTskIsOver.Visible = false;
            this.lbl_Check.AutoSize = true;
            this.lbl_Check.BackColor = Color.Transparent;
            this.lbl_Check.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lbl_Check.ForeColor = Color.Red;
            this.lbl_Check.Location = new Point(0x1c0, 0x41);
            this.lbl_Check.Name = "lbl_Check";
            this.lbl_Check.Size = new Size(0x2c, 12);
            this.lbl_Check.TabIndex = 0x37;
            this.lbl_Check.Text = "已审核";
            this.lbl_Check.Visible = false;
            this.label30.AutoSize = true;
            this.label30.ForeColor = SystemColors.ActiveCaption;
            this.label30.Location = new Point(0x284, 0x25);
            this.label30.Name = "label30";
            this.label30.Size = new Size(11, 12);
            this.label30.TabIndex = 0x36;
            this.label30.Text = "*";
            this.label23.AutoSize = true;
            this.label23.ForeColor = SystemColors.ActiveCaption;
            this.label23.Location = new Point(0xe3, 0x2b);
            this.label23.Name = "label23";
            this.label23.Size = new Size(11, 12);
            this.label23.TabIndex = 0x35;
            this.label23.Text = "*";
            this.label20.AutoSize = true;
            this.label20.ForeColor = SystemColors.ActiveCaption;
            this.label20.Location = new Point(0xe3, 0x43);
            this.label20.Name = "label20";
            this.label20.Size = new Size(11, 12);
            this.label20.TabIndex = 0x34;
            this.label20.Text = "*";
            this.label22.AutoSize = true;
            this.label22.ForeColor = SystemColors.ActiveCaption;
            this.label22.Location = new Point(0x283, 12);
            this.label22.Name = "label22";
            this.label22.Size = new Size(11, 12);
            this.label22.TabIndex = 0x33;
            this.label22.Text = "*";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x1c0, 12);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x35, 12);
            this.label14.TabIndex = 0x2d;
            this.label14.Text = "仓库员：";
            this.cmb_cCreator.FormattingEnabled = true;
            this.cmb_cCreator.Location = new Point(0x207, 9);
            this.cmb_cCreator.Name = "cmb_cCreator";
            this.cmb_cCreator.Size = new Size(0x79, 20);
            this.cmb_cCreator.TabIndex = 2;
            this.cmb_cCreator.Tag = "1";
            this.cmb_cCreator.Text = "Bind Text";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1c0, 0x24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 0x23;
            this.label3.Text = "单据日期：";
            this.dtp_dCreateDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_dCreateDate.Format = DateTimePickerFormat.Custom;
            this.dtp_dCreateDate.Location = new Point(0x207, 0x23);
            this.dtp_dCreateDate.Name = "dtp_dCreateDate";
            this.dtp_dCreateDate.Size = new Size(0x79, 0x15);
            this.dtp_dCreateDate.TabIndex = 7;
            this.dtp_dCreateDate.Tag = "2";
            this.txt_cPosIdFrom.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cPosIdFrom.Location = new Point(0x4c, 0x24);
            this.txt_cPosIdFrom.Name = "txt_cPosIdFrom";
            this.txt_cPosIdFrom.ReadOnly = true;
            this.txt_cPosIdFrom.Size = new Size(0x83, 0x15);
            this.txt_cPosIdFrom.TabIndex = 6;
            this.txt_cPosIdFrom.Tag = "0";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(3, 40);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x41, 12);
            this.label15.TabIndex = 0x1c;
            this.label15.Text = "来源货位：";
            this.txt_cChecker.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cChecker.Location = new Point(0x133, 6);
            this.txt_cChecker.Name = "txt_cChecker";
            this.txt_cChecker.ReadOnly = true;
            this.txt_cChecker.Size = new Size(0x7b, 0x15);
            this.txt_cChecker.TabIndex = 1;
            this.txt_cChecker.Tag = "0";
            this.lblChecker.AutoSize = true;
            this.lblChecker.Location = new Point(0xf9, 11);
            this.lblChecker.Name = "lblChecker";
            this.lblChecker.Size = new Size(0x35, 12);
            this.lblChecker.TabIndex = 0x18;
            this.lblChecker.Text = "审核人：";
            this.txt_cBNo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cBNo.Location = new Point(0x4b, 7);
            this.txt_cBNo.Name = "txt_cBNo";
            this.txt_cBNo.ReadOnly = true;
            this.txt_cBNo.Size = new Size(0x84, 0x15);
            this.txt_cBNo.TabIndex = 0;
            this.txt_cBNo.Tag = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 12);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "单号：";
            this.bdsMain.PositionChanged += new EventHandler(this.bdsMain_PositionChanged);
            this.colnItem.DataPropertyName = "nItem";
            this.colnItem.FillWeight = 50f;
            this.colnItem.Frozen = true;
            this.colnItem.HeaderText = "项次";
            this.colnItem.Name = "colnItem";
            this.colnItem.ReadOnly = true;
            this.colnItem.ToolTipText = "项次";
            this.colnItem.Width = 0x41;
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编码";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编码";
            this.colcMNo.Width = 0x4b;
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.colcSpec.Width = 70;
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            this.colcBatchNo.ToolTipText = "批号";
            this.colcBatchNo.Width = 0x4b;
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.ToolTipText = "数量";
            this.colfQty.Width = 0x4b;
            this.col_Dtl_fFinished.DataPropertyName = "fFinished";
            this.col_Dtl_fFinished.HeaderText = "完成数量";
            this.col_Dtl_fFinished.Name = "col_Dtl_fFinished";
            this.col_Dtl_fFinished.ReadOnly = true;
            this.col_Dtl_fFinished.ToolTipText = "完成数量";
            this.col_Dtl_fFinished.Width = 0x4b;
            this.colnQCStatus.DataPropertyName = "cQCStatus";
            this.colnQCStatus.HeaderText = "质检状态";
            this.colnQCStatus.Name = "colnQCStatus";
            this.colnQCStatus.ReadOnly = true;
            this.colnQCStatus.ToolTipText = "质检状态";
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "计量单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.ToolTipText = "计量单位";
            this.colcUnit.Width = 50;
            this.coldProdDate.DataPropertyName = "dProdDate";
            this.coldProdDate.HeaderText = "生产日期";
            this.coldProdDate.Name = "coldProdDate";
            this.coldProdDate.ReadOnly = true;
            this.coldProdDate.ToolTipText = "生产日期";
            this.col_Dtl_dInTime.DataPropertyName = "dInTime";
            this.col_Dtl_dInTime.HeaderText = "入库时间";
            this.col_Dtl_dInTime.Name = "col_Dtl_dInTime";
            this.col_Dtl_dInTime.ReadOnly = true;
            this.col_Dtl_cBNoIn.DataPropertyName = "cBNoIn";
            this.col_Dtl_cBNoIn.HeaderText = "入库单号";
            this.col_Dtl_cBNoIn.Name = "col_Dtl_cBNoIn";
            this.col_Dtl_cBNoIn.ReadOnly = true;
            this.col_Dtl_nItemIn.DataPropertyName = "nItemIn";
            this.col_Dtl_nItemIn.HeaderText = "入库单明细号";
            this.col_Dtl_nItemIn.Name = "col_Dtl_nItemIn";
            this.col_Dtl_nItemIn.ReadOnly = true;
            this.col_Dtl_cWHIdErp.DataPropertyName = "cWHIdErp";
            this.col_Dtl_cWHIdErp.HeaderText = "ERP仓库号";
            this.col_Dtl_cWHIdErp.Name = "col_Dtl_cWHIdErp";
            this.col_Dtl_cWHIdErp.ReadOnly = true;
            this.colcBId.DataPropertyName = "cBNo";
            this.colcBId.FillWeight = 50f;
            this.colcBId.Frozen = true;
            this.colcBId.HeaderText = "单号";
            this.colcBId.Name = "colcBId";
            this.colcBId.ReadOnly = true;
            this.colcBId.ToolTipText = "单号";
            this.colcBId.Width = 0x55;
            this.col_Main_cCreator.DataPropertyName = "cCreator";
            this.col_Main_cCreator.HeaderText = "仓库员";
            this.col_Main_cCreator.Name = "col_Main_cCreator";
            this.col_Main_cCreator.ReadOnly = true;
            this.col_Main_nBClass.DataPropertyName = "nBClass";
            this.col_Main_nBClass.HeaderText = "单据类别号";
            this.col_Main_nBClass.Name = "col_Main_nBClass";
            this.col_Main_nBClass.ReadOnly = true;
            this.col_Main_nBClass.Visible = false;
            this.col_Main_bIsChecked.DataPropertyName = "bIsChecked";
            this.col_Main_bIsChecked.HeaderText = "是否已审核";
            this.col_Main_bIsChecked.Name = "col_Main_bIsChecked";
            this.col_Main_bIsChecked.ReadOnly = true;
            this.col_Main_bIsChecked.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x467, 0x222);
            base.Controls.Add(this.splitContainer1);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.tlbMain);
            base.Name = "frmMergePallet";
            this.Text = "合盘单据管理";
            base.Load += new EventHandler(this.frmMergePallet_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((ISupportInitialize) this.grdList).EndInit();
            this.pnlListRecCount.ResumeLayout(false);
            this.pnlListRecCount.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.grdDtl).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            ((ISupportInitialize) this.bdsMain).EndInit();
            ((ISupportInitialize) this.bdsDtl).EndInit();
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
        }

        private void LoadBaseItemFromDB()
        {
            string sSql = "";
            string sErr = "";
            int wtWareType = (int) this.wtWareType;
            sSql = "select * from TWC_WareHouse where 1=1 ";
            if (this.wtWareType != WareType.wtNone)
            {
                sSql = sSql + " and nType=" + wtWareType.ToString();
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + "and cWHId in  (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId + "')";
            }
            sErr = "";
            sSql = "select cUserId,cName from TPB_User where bUsed=1 ";
            if (base.UserInformation.UType == UserType.utNormal)
            {
                sSql = sSql + " and cUserId='" + base.UserInformation.UserId + "'";
            }
            else if (base.UserInformation.UType == UserType.utAdmin)
            {
                sSql = sSql + " and cDeptId='" + base.UserInformation.DeptId.Trim() + "'";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmbFindUser.DisplayMember = "cName";
            this.cmbFindUser.ValueMember = "cName";
            DataTable table = dataBySql.Tables["data"];
            DataRow row = null;
            row = table.NewRow();
            row["cUserId"] = "ERP";
            row["cName"] = "ERP";
            table.Rows.Add(row);
            this.cmbFindUser.DataSource = table;
            DataTable table2 = table.Copy();
            this.cmb_cCreator.DisplayMember = "cName";
            this.cmb_cCreator.ValueMember = "cName";
            this.cmb_cCreator.DataSource = table2;
        }

        private void MyCallSafeDllFun(string sFile, string sClassName, string sFunName, object[] parms)
        {
            bool bIsOK = false;
            if (File.Exists(sFile))
            {
                MyCallSafetyDll.DoCallMyDll(sFile, sClassName, sFunName, parms, out bIsOK);
            }
            else
            {
                MessageBox.Show(sFile + "  不存在！");
            }
        }

        public bool OpenDtlDataSet(string sCon)
        {
            bool flag = false;
            string text = "";
            this.bDSIsOpenForDtl = false;
            this.grdDtl.AutoGenerateColumns = false;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sSql = "select * from V_TWB_BillMergePltDtl " + sCon;
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
                    this.grdDtl.DataSource = this.bdsDtl;
                    flag = true;
                    this.lbl_D_Count.Text = this.bdsDtl.Count.ToString();
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
            string sSql = "select * from TWB_BillIn " + sCon;
            if (this.wtWareType != WareType.wtNone)
            {
                sSql = sSql + " and cWHId in (select cWHId from TWC_WareHouse where nType=" + ((int) this.wtWareType).ToString() + ")";
            }
            sSql = sSql + " order by cBNo desc";
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
                    this.lbl_BillTskIsOver.Visible = false;
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
                        this.lbl_Check.Visible = true;
                        this.lbl_BillTskIsOver.Visible = true;
                        if (current["bIsChecked"].ToString() == "1")
                        {
                            this.lbl_Check.Text = "已审核";
                        }
                        else
                        {
                            this.lbl_Check.Text = "未审核";
                        }
                        if (current["bIsFinished"].ToString() == "1")
                        {
                            this.lbl_BillTskIsOver.Text = "单据作业已完成";
                        }
                        else
                        {
                            this.lbl_BillTskIsOver.Text = "单据作业未完成";
                        }
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

        private void tlb_M_Check_Click(object sender, EventArgs e)
        {
            if (this.grdList.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in this.grdList.SelectedRows)
                {
                    if ((base.UserInformation.UType == UserType.utNormal) && (row.Cells["cCreator"].Value.ToString().Trim() != base.UserInformation.UserName.Trim()))
                    {
                        MessageBox.Show("对不起，你无权限审核或取消审核");
                    }
                    else
                    {
                        string pBNo = "";
                        pBNo = row.Cells["colcBId"].Value.ToString();
                        int pBClass = 0;
                        pBClass = Convert.ToInt32(row.Cells["col_Main_nBClass"].Value);
                        if ((pBNo.Trim() != "") && (row.Cells["col_Main_bIsChecked"].Value.ToString().ToLower() != "1"))
                        {
                            string sErr = "";
                            if (PubDBCommFuns.sp_Pack_BillCheck(base.AppInformation.SvrSocket, pBClass, pBNo, 0, base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", out sErr).Trim() != "0")
                            {
                                MessageBox.Show(sErr);
                            }
                        }
                    }
                }
            }
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("对不起，无数据可删除！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current != null)
                {
                    string str = current["cBNo"].ToString();
                    if (current["bIsChecked"].ToString() == "1")
                    {
                        MessageBox.Show("对不起，改单已经审核，不能删除！");
                    }
                    else if (MessageBox.Show("您确定要删除该单号：" + str + "  吗 ？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.No)
                    {
                        string sErr = "";
                        if (!DBFuns.DoExecSql(base.AppInformation.SvrSocket, "delete TWB_BillMergePltDtl where cBNo='" + str + "'", "", out sErr))
                        {
                            MessageBox.Show("删除单据明细数据失败：" + sErr);
                        }
                        else if (!DBFuns.DoExecSql(base.AppInformation.SvrSocket, "delete TWB_BillMergePlt where cBNo='" + str + "'", "", out sErr))
                        {
                            MessageBox.Show("删除单据数据失败：" + sErr);
                        }
                        else
                        {
                            MessageBox.Show("删除单据数据成功！");
                            this.btnQry_Click(null, null);
                        }
                    }
                }
            }
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            frmSelPosFromAndTo to = new frmSelPosFromAndTo {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            to.ShowDialog();
            to.Dispose();
            to = null;
            this.btnQry_Click(null, null);
        }

        private void tlb_M_UnCheck_Click(object sender, EventArgs e)
        {
            if (this.grdList.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in this.grdList.SelectedRows)
                {
                    if ((base.UserInformation.UType == UserType.utNormal) && (row.Cells["cCreator"].Value.ToString().Trim() != base.UserInformation.UserName.Trim()))
                    {
                        MessageBox.Show("对不起，你无权限审核或取消审核");
                    }
                    else
                    {
                        string pBNo = "";
                        pBNo = row.Cells["colcBId"].Value.ToString();
                        int pBClass = 0;
                        pBClass = Convert.ToInt32(row.Cells["col_Main_nBClass"].Value);
                        if ((pBNo.Trim() != "") && (row.Cells["col_Main_bIsChecked"].Value.ToString().ToLower() != "0"))
                        {
                            string sErr = "";
                            if (PubDBCommFuns.sp_Pack_BillCheck(base.AppInformation.SvrSocket, pBClass, pBNo, 1, base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", out sErr).Trim() != "0")
                            {
                                MessageBox.Show(sErr);
                            }
                        }
                    }
                }
            }
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

