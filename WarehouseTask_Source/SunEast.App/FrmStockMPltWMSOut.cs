using SunEast;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UI;
using CommBase;
using DBCommInfo;
using WarehouseTask;
namespace SunEast.App
{


    public class FrmStockMPltWMSOut : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Detail;
        private BindingSource bindingSource_HasPallet;
        private BindingSource bindingSource_Main;
        private bool bIsOpenForArea = false;
        private bool bIsOpenForDtl = false;
        private bool bIsOpenForWH = false;
        private bool bIsOpenUser = false;
        private ToolStripButton btn_M_Help;
        private Button btn_OK;
        private Button btn_OutByAuto;
        private Button btn_OutByManual;
        private Button btn_Query;
        private Button btn_Reset;
        private Button btn_SelPosId;
        private Button btn_UnDoCmd;
        private Button button_CancelTastWork;
        private Button button_WorkOrder;
        private DataGridViewTextBoxColumn cBatchNo;
        private DataGridViewTextBoxColumn cBNo;
        private DataGridViewTextBoxColumn cBType;
        private DataGridViewTextBoxColumn cDOStatus;
        private ComboBox cmb_cAreaId;
        private ComboBox cmb_cWHId;
        private ComboBox cmb_OptGroup;
        private ComboBox cmb_Port;
        private ComboBox cmb_User;
        private DataGridViewTextBoxColumn cMName;
        private DataGridViewTextBoxColumn cMNo;
        private DataGridViewTextBoxColumn col_Dtl_cDtlRemark;
        private DataGridViewTextBoxColumn col_Dtl_cLinkId;
        private DataGridViewTextBoxColumn col_Dtl_cMName;
        private DataGridViewTextBoxColumn col_Dtl_cSpecDesc;
        private DataGridViewTextBoxColumn col_Plt_cWKStatusDesc;
        private DataGridViewTextBoxColumn colcName;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private DataGridViewTextBoxColumn cSpec;
        private DataGridViewTextBoxColumn cStatus;
        private DataGridViewTextBoxColumn cUnit;
        private DataGridViewTextBoxColumn cWName;
        private DataGridView dataGridView_Detail;
        private DataGridView dataGridView_HasPallet;
        private DataGridView dataGridView_Main;
        private DataSet DBDateSetDetail = null;
        private DataSet dsHsPallet = null;
        private DataGridViewTextBoxColumn fFinished;
        private DataGridViewTextBoxColumn fPallet;
        private DataGridViewTextBoxColumn fQty;
        private DataGridViewTextBoxColumn grdc_Dtl_cAreaName;
        private DataGridViewTextBoxColumn grdc_Dtl_cBatchNo;
        private DataGridViewTextBoxColumn grdc_Dtl_cBNoIn;
        private DataGridViewTextBoxColumn grdc_Dtl_cBoxId;
        private DataGridViewTextBoxColumn grdc_Dtl_cPalletId;
        private DataGridViewTextBoxColumn grdc_Dtl_cPosId;
        private DataGridViewTextBoxColumn grdc_Dtl_cStatusWork;
        private DataGridViewTextBoxColumn grdc_Dtl_cWHId;
        private DataGridViewTextBoxColumn grdc_Dtl_cWHType;
        private DataGridViewTextBoxColumn grdc_Dtl_fQty;
        private DataGridViewTextBoxColumn grdc_Dtl_nItemIn;
        private DataGridViewTextBoxColumn grdc_Dtl_nStatusWork;
        private DataGridViewTextBoxColumn grdcUnPallet;
        private DataGridViewTextBoxColumn grdPlt_nWKStatus;
        private GroupBox groupBox_InType;
        private GroupBox groupBox1;
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
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label3;
        private Label label30;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl_MainCount;
        private DataGridViewTextBoxColumn nPalletId;
        private DataGridViewTextBoxColumn nWorkId;
        private OperateType OptDetail = OperateType.optNone;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel pnl_MainCount;
        private Panel pnl_PltByManual;
        private RadioButton radioButton_AllIn;
        private RadioButton radioButton_PartIn;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cBNo";
        private string strKeyFldDetail = "nWorkId";
        private string strTbNameDetail = "TWB_WorkTastDtl";
        private string strTbNameHasPallet = "V_WorkTaskDtl";
        private string strTbNameMain = "V_IOBillDetail";
        private TabControl tabControl1_HasPallet;
        private TabPage tabPage2;
        private TextBox textBox_cBatchNo;
        private TextBox textBox_cBNo;
        private TextBox textBox_cBType;
        private TextBox textBox_cDept;
        private TextBox textBox_CMName;
        private TextBox textBox_cMNo;
        private TextBox textBox_cSpec;
        private TextBox textBox_cStatus;
        private TextBox textBox_fFinished;
        private TextBox textBox_fPallet;
        private TextBox textBox_fQty;
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
        private ToolTip tlTip;
        private ToolStripButton toolStripButton_Audit;
        public ToolStripLabel toolStripLabel1;
        private ToolStripMenuItem toolStripMenuItem_Tast;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem tsmiLockPallInfo;
        private TextBox txt_BillNo;
        private TextBox txt_Col;
        private TextBox txt_Layer;
        private TextBox txt_MNo;
        private TextBox txt_PalletId;
        private TextBox txt_PltQty;
        private TextBox txt_PosId;
        private TextBox txt_Row;
        private WareType wtWareType = WareType.wtNone;

        public FrmStockMPltWMSOut()
        {
            this.InitializeComponent();
        }

        private bool BandDataSet(string SqlStrConditon, DataGridView FDataGridView)
        {
            int position = this.bindingSource_Main.Position;
            bool flag = true;
            string sSql = "";
            string sErr = "";
            this.bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            sSql = "select cPayer,nPStatus,cStatus,cBNo,nItem,cBNoSeq,cMNo,cUnit,cMName,cBTypeId,cBType,cDept,cSpec,cBatchNo,fQty,fPallet,fFinished,(isnull(fQty,0) - isnull(fPallet,0)-isnull(fFinished,0)) fUnPallet, nDoStatus,cDoStatus,cLinkId from V_IOBILLDETAIL_Ext  " + SqlStrConditon;
            base.DBDataSet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, out sErr);
            flag = base.DBDataSet != null;
            this.bindingSource_Main.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
            FDataGridView.DataSource = this.bindingSource_Main;
            this.bindingSource_Main.Position = position;
            this.lbl_MainCount.Text = this.bindingSource_Main.Count.ToString();
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

        private bool BandDataSetDetail(string[] sqlCondition, DataGridView FDataGridView)
        {
            bool flag = true;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetPosLstForPltIn :pUser,:pOptType,:pMNo,:pWHId",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = sqlCondition[0],
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = sqlCondition[1],
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = sqlCondition[2],
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = sqlCondition[3],
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet dataSet = null;
            dataSet = client.GetDataSet(cmdInfo, out sErr);
            flag = base.DBDataSet != null;
            this.bindingSource_Detail.DataSource = dataSet.Tables["data"];
            FDataGridView.DataSource = this.bindingSource_Detail;
            return flag;
        }

        private void bindingSource_Detail_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenForDtl)
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    DataRowView view2 = (DataRowView) this.bindingSource_Detail.Current;
                    if (view2 != null)
                    {
                        this.txt_PalletId.Text = view2["nPalletId"].ToString();
                        this.txt_PosId.Text = view2["cPosId"].ToString();
                        double num = 0.0;
                        if ((view2["fQty"] != null) && (view2["fQty"].ToString() != ""))
                        {
                            num = Convert.ToDouble(view2["fQty"]);
                            if (double.Parse(current["fUnPallet"].ToString()) > num)
                            {
                                this.txt_PltQty.Text = num.ToString();
                            }
                            else
                            {
                                this.txt_PltQty.Text = current["fUnPallet"].ToString();
                            }
                            if (this.cmb_OptGroup.Items.Count == 0)
                            {
                                string sWHId = "";
                                if ((this.cmb_cWHId.SelectedValue != null) && (this.cmb_cWHId.SelectedValue.ToString() != ""))
                                {
                                    sWHId = this.cmb_cWHId.SelectedValue.ToString();
                                }
                                this.LoadOptGroup(sWHId);
                            }
                            this.cmb_OptGroup_SelectedIndexChanged(null, null);
                        }
                        else
                        {
                            MessageBox.Show("可用配盘数据的数量为空！");
                        }
                    }
                }
            }
        }

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDSIsOpenForMain)
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                this.ClearUIValues(this.panel_Edit);
                if (current != null)
                {
                    this.DataRowViewToUI(current, this.panel_Edit);
                    this.GetPalletList(true);
                }
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            DataRowView view2 = (DataRowView) this.bindingSource_Detail.Current;
            if ((current != null) && (view2 != null))
            {
                if (this.txt_PosId.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，货位号不能为空！");
                    this.txt_PosId.Focus();
                }
                else if (this.txt_PltQty.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，配盘数量不能为空！");
                    this.txt_PltQty.Focus();
                }
                else if (this.txt_PltQty.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，配盘数量不能为空！");
                    this.txt_PltQty.Focus();
                }
                else
                {
                    string sErr = "";
                    int pOptType = 0;
                    int pOptStation = 0;
                    double num3 = double.Parse(this.txt_PltQty.Text.Trim());
                    if (this.radioButton_AllIn.Checked)
                    {
                        pOptType = 3;
                    }
                    else
                    {
                        pOptType = 4;
                    }
                    if (this.cmb_Port.Text.Trim() == "")
                    {
                        pOptStation = 0;
                    }
                    else
                    {
                        pOptStation = int.Parse(this.cmb_Port.Text.Trim());
                    }
                    if (PubDBCommFuns.sp_Pack_CheckPosIsdPltIdIsOK(base.AppInformation.SvrSocket, pOptType, this.txt_PosId.Text.Trim(), this.txt_PalletId.Text.Trim(), out sErr) != "0")
                    {
                        MessageBox.Show("校验货位号与托盘号时失败：" + sErr);
                    }
                    else
                    {
                        string pWHId = view2["cWHId"].ToString().Trim();
                        string pBNo = current["cBNo"].ToString().Trim();
                        string pItem = current["nItem"].ToString().Trim();
                        string pMNo = current["cMNo"].ToString().Trim();
                        string pBatchNo = view2["cBatchNo"].ToString().Trim();
                        string pPalletId = view2["nPalletId"].ToString().Trim();
                        string pBNoIn = view2["cBNoIn"].ToString();
                        string s = view2["nItemIn"].ToString();
                        string pBoxId = view2["cBoxId"].ToString().Trim();
                        if (pBoxId.Length == 0)
                        {
                            pBoxId = " ";
                        }
                        if (pPalletId.Trim().Length == 0)
                        {
                            MessageBox.Show("托盘号不能为空！");
                        }
                        else if (DBFuns.sp_Pack_CheckPltOutQtyIsOK(base.AppInformation.SvrSocket, pMNo, pWHId, pPalletId, pBoxId, pBNoIn, int.Parse(s), double.Parse(this.txt_PltQty.Text.Trim()), out sErr) != "0")
                        {
                            MessageBox.Show(sErr);
                            this.txt_PltQty.SelectAll();
                            this.txt_PltQty.Focus();
                        }
                        else if (DBFuns.sp_Pack_DoPltDtlOutManual(base.AppInformation.SvrSocket, base.UserInformation.UserName, 0, this.txt_PosId.Text.Trim(), pOptType, pOptStation, "WMS", pWHId, pBNo, pItem, pBNoIn, s, pMNo, pBatchNo, double.Parse(this.txt_PltQty.Text.Trim()), pPalletId, pBoxId, base.UserInformation.UnitId, 0, 0, out sErr) != "0")
                        {
                            MessageBox.Show("配盘时失败：" + sErr);
                        }
                        else
                        {
                            MessageBox.Show("配盘成功：");
                            this.cmb_User_TextChanged(null, null);
                            this.DoRefreshHasPallet();
                        }
                    }
                }
            }
        }

        private void btn_OutByAuto_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("对不起，没有待配盘的数据！");
            }
            else
            {
                frmOutStoreByAuto auto = new frmOutStoreByAuto {
                    AppInformation = base.AppInformation,
                    UserInformation = base.UserInformation
                };
                auto.ShowDialog();
                if (auto.IsOK)
                {
                    this.tlb_M_Refresh_Click(null, null);
                }
            }
        }

        private void btn_OutByManual_Click(object sender, EventArgs e)
        {
            this.pnl_PltByManual.Enabled = true;
            this.cmb_cWHId.Focus();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.txt_BillNo.Text = "";
            this.txt_MNo.Text = "";
            this.txt_BillNo.Focus();
        }

        private void btn_SelPosId_Click(object sender, EventArgs e)
        {
            frmWareCellState state = new frmWareCellState {
                BIsSelectCell = true
            };
            if ((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.SelectedValue != null))
            {
                state.WHId = this.cmb_cWHId.SelectedValue.ToString().Trim();
            }
            state.UserInformation = base.UserInformation;
            state.AppInformation = base.AppInformation;
            state.ShowDialog();
            if (state.BIsResultOK)
            {
                this.txt_PosId.Text = state.PosId.Trim();
                this.txt_PalletId.Text = state.PalletId.Trim();
                string[] strArray = state.SelectOtherValue.Trim().Trim().Split(new char[] { '-' });
                if ((strArray != null) && (strArray.Length > 0))
                {
                    this.txt_PltQty.Text = strArray[4];
                    this.cmb_Port.Text = "1";
                    double num = 0.0;
                    num = (double.Parse(this.textBox_fQty.Text.Trim()) - double.Parse(this.textBox_fPallet.Text.Trim())) - double.Parse(this.textBox_fFinished.Text.Trim());
                    double num2 = double.Parse(this.txt_PltQty.Text.Trim());
                    if (num2 < num)
                    {
                        this.txt_PltQty.Text = num2.ToString();
                    }
                    else
                    {
                        this.txt_PltQty.Text = num.ToString();
                    }
                }
                this.txt_PltQty.SelectAll();
                this.txt_PltQty.Focus();
            }
            state.Dispose();
        }

        private void btn_UnDoCmd_Click(object sender, EventArgs e)
        {
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_HasPallet.Current;
            if (current != null)
            {
                if (PubDBCommFuns.sp_UnDoTaskCMD(base.AppInformation.SvrSocket, int.Parse(current["nWorkId"].ToString()), "WMS", base.UserInformation.UnitId, base.UserInformation.UserName.Trim(), out sErr) != "0")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show("撤销任务操作成功：");
                    this.cmb_User_TextChanged(null, null);
                    this.DoRefreshHasPallet();
                }
            }
        }

        private void button_CancelTastWork_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView current = (DataRowView) this.bindingSource_HasPallet.Current;
                if (current != null)
                {
                    string sErr = "";
                    if (PubDBCommFuns.sp_Pack_DelWKTskDtl(base.AppInformation.SvrSocket, int.Parse(current["nWorkId"].ToString()), current["cBNo"].ToString().Trim(), current["nItem"].ToString().Trim(), current["cBNoIn"].ToString().Trim(), current["nItemIn"].ToString().Trim(), current["cBoxId"].ToString().Trim(), "WMS", base.UserInformation.UnitId.Trim(), base.UserInformation.UserName.Trim(), out sErr) == "0")
                    {
                        MessageBox.Show("取消配盘操作成功！");
                        this.cmb_User_TextChanged(null, null);
                        this.DoRefreshHasPallet();
                    }
                    else
                    {
                        MessageBox.Show(sErr);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button_WorkOrder_Click(object sender, EventArgs e)
        {
            int num = 0;
            try
            {
                if (this.dataGridView_HasPallet.SelectedRows.Count == 0)
                {
                    MessageBox.Show("对不起，没有选择需要下发执行的任务指令！");
                }
                else
                {
                    foreach (DataGridViewRow row in this.dataGridView_HasPallet.SelectedRows)
                    {
                        string str = "";
                        string sErr = "";
                        if (int.Parse(row.Cells["grdPlt_nWKStatus"].Value.ToString()) <= 0)
                        {
                            int pWorkId = 0;
                            pWorkId = int.Parse(row.Cells["nWorkId"].Value.ToString());
                            if (pWorkId != 0)
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                str = PubDBCommFuns.sp_DoTaskCMD(base.AppInformation.SvrSocket, pWorkId, "WMS", base.UserInformation.UnitId, base.UserInformation.UserName, out sErr);
                                Cursor.Current = Cursors.Default;
                                if ((str == "0") || (str.Trim() == ""))
                                {
                                    num++;
                                }
                                else
                                {
                                    MessageBox.Show(sErr);
                                }
                            }
                        }
                    }
                    MessageBox.Show("成功下发执行了 " + num.ToString() + " 条任务指令！");
                    this.cmb_User_TextChanged(null, null);
                    this.DoRefreshHasPallet();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void cmb_cAreaId_TextChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenForArea)
            {
                this.GetPalletList(false);
            }
        }

        private void cmb_cWHId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sWHId = "";
            if (this.bIsOpenForWH)
            {
                if (((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.Items.Count > 0)) && (this.cmb_cWHId.SelectedValue != null))
                {
                    sWHId = this.cmb_cWHId.SelectedValue.ToString().Trim();
                }
                this.LoadCombWAreaList(sWHId, this.cmb_cAreaId);
                this.LoadOptGroup(sWHId);
            }
        }

        private void cmb_cWHId_TextChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenForWH)
            {
                if (this.cmb_cWHId.Text.Trim() == "")
                {
                    this.cmb_cAreaId.Text = "";
                }
                this.GetPalletList(false);
            }
        }

        private void cmb_OptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cmb_OptGroup.Text.Trim() != "") && (this.txt_PosId.Text.Trim() != ""))
            {
                int posRowNo = this.GetPosRowNo(this.txt_PosId.Text.Trim());
                if (posRowNo > 0)
                {
                    this.LoadOptNoList("", this.cmb_OptGroup.Text.Trim(), posRowNo);
                }
            }
        }

        private void cmb_User_TextChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenUser)
            {
                this.DoRefresh(this.cmb_User.Text.Trim());
            }
        }

        private void dataGridView_Detail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btn_OK_Click(null, null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoRefresh(string sUser)
        {
            this.sbCondition.Remove(0, this.sbCondition.Length);
            this.sbCondition.Append(" where nBClass=2 ");
            this.sbCondition.Append(" and isnull(bIsChecked,0)=1  and isnull(bIsFinished,0)=0 and nPStatus<110 and isnull(fQty,0)>(isnull(fPallet,0) + isnull(fFinished,0))");
            if (sUser.Trim() != "")
            {
                this.sbCondition.Append(" and cPayer='" + sUser.Trim() + "'");
            }
            if (this.txt_BillNo.Text.Trim() != "")
            {
                this.sbCondition.Append(" and (cBNo like '%" + this.txt_BillNo.Text.Trim() + "%' or isnull(cLinkId,'') like '%" + this.txt_BillNo.Text.Trim() + "%')");
            }
            if (this.txt_MNo.Text.Trim() != "")
            {
                this.sbCondition.Append(" and (cMNo like '%" + this.txt_MNo.Text.Trim() + "%' or cMName like  '%" + this.txt_MNo.Text.Trim() + "%' or cPYJM like '%" + this.txt_MNo.Text.Trim() + "%' or cWBJM like '%" + this.txt_MNo.Text.Trim() + "%')");
            }
            this.BandDataSet(this.sbCondition.ToString(), this.dataGridView_Main);
            this.GetPalletList(true);
            return false;
        }

        private void DoRefreshHasPallet()
        {
            string sErr = "";
            StringBuilder builder = new StringBuilder("");
            builder.Append("select * from V_WorkTaskDtl where nBClass=2 and isnull(bIsFinished,0)<1  and nWKStatus < 18 ");
            if (base.UserInformation.UType == UserType.utNormal)
            {
                builder.Append(" and isnull(cUser,' ')='" + base.UserInformation.UserName.Trim() + "'");
            }
            builder.Append(" order by nWorkId desc ");
            Cursor.Current = Cursors.WaitCursor;
            this.dsHsPallet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), this.strTbNameHasPallet, 0, 0, "", out sErr);
            Cursor.Current = Cursors.Default;
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
            }
            else if (this.dsHsPallet == null)
            {
                MessageBox.Show("对不起，打开已配盘数据失败！");
            }
            else
            {
                this.dataGridView_HasPallet.AutoGenerateColumns = false;
                DataTable table = this.dsHsPallet.Tables[this.strTbNameHasPallet];
                this.bindingSource_HasPallet.DataSource = table;
                this.dataGridView_HasPallet.DataSource = this.bindingSource_HasPallet;
            }
        }

        private void FrmStockMPltWMSOut_Load(object sender, EventArgs e)
        {
            this.LoadCombWare(this.cmb_cWHId);
            this.LoadUser();
            string sWHId = "";
            if ((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.SelectedValue != null))
            {
                sWHId = this.cmb_cWHId.SelectedValue.ToString();
            }
            this.LoadOptGroup(sWHId);
            this.dataGridView_Detail.AutoGenerateColumns = false;
            this.dataGridView_HasPallet.AutoGenerateColumns = false;
            this.dataGridView_Main.AutoGenerateColumns = false;
            this.DoRefreshHasPallet();
        }

        private double GetMaterialQtyPallet(string sMNo)
        {
            double num = 0.0;
            string sErr = "";
            object objValue = null;
            string sSql = "select isnull(fQtyBox,0) fQtyBox from TPC_Material where cMNo='" + sMNo.Trim() + "'";
            PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "fQtyBox", out objValue, out sErr);
            if (sErr.Trim().Length > 0)
            {
                MessageBox.Show("获取物料的每盘/箱数量时出错： " + sErr);
                return num;
            }
            if (objValue != null)
            {
                num = double.Parse(objValue.ToString());
            }
            return num;
        }

        private void GetPalletList(bool bIsFirst)
        {
            int pIsWhole = 0;
            string pWHId = "";
            string pMNo = "";
            double pQty = 0.0;
            string sErr = "";
            string pWAreaId = "";
            this.txt_PalletId.Text = "";
            this.txt_PltQty.Text = "";
            this.cmb_Port.Text = "1";
            this.txt_PosId.Text = "";
            int pRow = 0;
            int pCol = 0;
            int pLayer = 0;
            if ((this.txt_Row.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_Row.Text.Trim()))
            {
                pRow = int.Parse(this.txt_Row.Text.Trim());
            }
            if ((this.txt_Col.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_Col.Text.Trim()))
            {
                pCol = int.Parse(this.txt_Col.Text.Trim());
            }
            if ((this.txt_Layer.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_Layer.Text.Trim()))
            {
                pLayer = int.Parse(this.txt_Layer.Text.Trim());
            }
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current != null)
            {
                if ((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.SelectedValue != null))
                {
                    pWHId = this.cmb_cWHId.SelectedValue.ToString().Trim();
                    if ((this.cmb_cAreaId.Text.Trim() != "") && (this.cmb_cAreaId.SelectedValue != null))
                    {
                        pWAreaId = this.cmb_cAreaId.SelectedValue.ToString().Trim();
                    }
                }
                pMNo = current["cMNo"].ToString().Trim();
                pQty = double.Parse(current["fUnPallet"].ToString());
                this.dataGridView_Detail.DataSource = null;
                this.dataGridView_Detail.AutoGenerateColumns = false;
                if (this.radioButton_AllIn.Checked)
                {
                    pIsWhole = 1;
                }
                else
                {
                    pIsWhole = 0;
                }
                DataTable table = null;
                Cursor cursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    table = PubDBCommFuns.sp_Pack_GetPosLstForPltOut(base.AppInformation.SvrSocket, pWHId, pMNo, pQty, pIsWhole, pWAreaId, pRow, pCol, pLayer, out sErr);
                }
                finally
                {
                    Cursor.Current = cursor;
                }
                if (sErr.Trim().Length > 0)
                {
                    MessageBox.Show("加载可配盘数据时出错！");
                }
                else if (table == null)
                {
                    MessageBox.Show("加载可配盘数据时出错！");
                }
                else
                {
                    this.bIsOpenForDtl = false;
                    this.bindingSource_Detail.DataSource = table;
                    this.dataGridView_Detail.DataSource = this.bindingSource_Detail;
                    this.bIsOpenForDtl = true;
                    if (this.bindingSource_Detail.Count > 0)
                    {
                        this.bindingSource_Detail_CurrentChanged(null, null);
                    }
                }
            }
        }

        private DataTable GetPallReceInfoData(string pallStr)
        {
            DataTable table = new DataTable();
            string sSql = string.Format("select sto.*,mat.cname,mat.cspec,mat.cmatstyle,mat.cmatother from (select nPalletId,cMNo,isnull(cBNoIn,'') cBNoIn,nItemIn,sum(fQty) fQty,max(cUnit) cUnit,max(cSupplier) cSupplier,max(cDtlRemark) cDtlRemark from twb_stockdtl where nPalletId='{0}' group by nPalletId,cMNo,isnull(cBNoIn,''),nItemIn having sum(fQty)>0) sto join tpc_material mat on sto.cMNo=mat.cmno", pallStr);
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "pallReceInfo", 0, 0, out sErr);
            if (sErr.Length > 0)
            {
                this.MyMessageBox(sErr, "提示！");
                return table;
            }
            return set.Tables["pallReceInfo"];
        }

        private int GetPosRowNo(string sPosId)
        {
            int num = 0;
            string sSql = "select nRow from TWC_WareCell where cPosId='" + sPosId.Trim() + "'";
            object objValue = null;
            string sErr = "";
            if (DBFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "nRow", out objValue, out sErr))
            {
                if (objValue != null)
                {
                    num = Convert.ToInt16(objValue);
                }
                return num;
            }
            MessageBox.Show("获取货位的行号时出错：" + sErr);
            return num;
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_SelPosId = new System.Windows.Forms.Button();
            this.txt_PosId = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.txt_PalletId = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.stbDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.txt_PltQty = new System.Windows.Forms.TextBox();
            this.cmb_cAreaId = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tast = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLockPallInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.stbModul = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.stbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbState = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_WorkOrder = new System.Windows.Forms.Button();
            this.button_CancelTastWork = new System.Windows.Forms.Button();
            this.tlb_M_Save = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Delete = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Find = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_New = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView_Main = new System.Windows.Forms.DataGridView();
            this.cBNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cLinkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdcUnPallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cWName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fPallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fFinished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDOStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_MainCount = new System.Windows.Forms.Panel();
            this.lbl_MainCount = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_BillNo = new System.Windows.Forms.TextBox();
            this.txt_MNo = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Query = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.cmb_User = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tlb_M_Undo = new System.Windows.Forms.ToolStripButton();
            this.bindingSource_Main = new System.Windows.Forms.BindingSource(this.components);
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.bindingSource_HasPallet = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource_Detail = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripButton_Audit = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Print = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView_Detail = new System.Windows.Forms.DataGridView();
            this.grdc_Dtl_cWHId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cAreaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cPosId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cSpecDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_fQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cPalletId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cStatusWork = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dtl_cDtlRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cBNoIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_nItemIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_nStatusWork = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cWHType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdc_Dtl_cBoxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_InType = new System.Windows.Forms.GroupBox();
            this.radioButton_PartIn = new System.Windows.Forms.RadioButton();
            this.radioButton_AllIn = new System.Windows.Forms.RadioButton();
            this.tlTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_UnDoCmd = new System.Windows.Forms.Button();
            this.btn_OutByManual = new System.Windows.Forms.Button();
            this.btn_OutByAuto = new System.Windows.Forms.Button();
            this.textBox_cBatchNo = new System.Windows.Forms.TextBox();
            this.textBox_fFinished = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.panel_Edit = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_fPallet = new System.Windows.Forms.TextBox();
            this.textBox_fQty = new System.Windows.Forms.TextBox();
            this.textBox_cSpec = new System.Windows.Forms.TextBox();
            this.textBox_cMNo = new System.Windows.Forms.TextBox();
            this.textBox_cDept = new System.Windows.Forms.TextBox();
            this.textBox_cBType = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_cStatus = new System.Windows.Forms.TextBox();
            this.textBox_cBNo = new System.Windows.Forms.TextBox();
            this.textBox_CMName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_HasPallet = new System.Windows.Forms.DataGridView();
            this.nWorkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nPalletId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdPlt_nWKStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Plt_cWKStatusDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_PltByManual = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmb_OptGroup = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cmb_Port = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_Layer = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_Col = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_Row = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmb_cWHId = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1_HasPallet = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.stbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Main)).BeginInit();
            this.pnl_MainCount.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_HasPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Detail)).BeginInit();
            this.groupBox_InType.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_HasPallet)).BeginInit();
            this.pnl_PltByManual.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1_HasPallet.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SelPosId
            // 
            this.btn_SelPosId.Location = new System.Drawing.Point(335, 54);
            this.btn_SelPosId.Name = "btn_SelPosId";
            this.btn_SelPosId.Size = new System.Drawing.Size(25, 23);
            this.btn_SelPosId.TabIndex = 73;
            this.btn_SelPosId.Text = "…";
            this.tlTip.SetToolTip(this.btn_SelPosId, "从仓位图选货位");
            this.btn_SelPosId.UseVisualStyleBackColor = true;
            this.btn_SelPosId.Visible = false;
            this.btn_SelPosId.Click += new System.EventHandler(this.btn_SelPosId_Click);
            // 
            // txt_PosId
            // 
            this.txt_PosId.Location = new System.Drawing.Point(237, 55);
            this.txt_PosId.Name = "txt_PosId";
            this.txt_PosId.ReadOnly = true;
            this.txt_PosId.Size = new System.Drawing.Size(96, 21);
            this.txt_PosId.TabIndex = 6;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OK.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_OK.Location = new System.Drawing.Point(683, 54);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(50, 23);
            this.btn_OK.TabIndex = 9;
            this.btn_OK.Text = "配盘";
            this.tlTip.SetToolTip(this.btn_OK, "配盘，生成任务数据");
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // txt_PalletId
            // 
            this.txt_PalletId.Location = new System.Drawing.Point(135, 54);
            this.txt_PalletId.Name = "txt_PalletId";
            this.txt_PalletId.ReadOnly = true;
            this.txt_PalletId.Size = new System.Drawing.Size(58, 21);
            this.txt_PalletId.TabIndex = 5;
            this.tlTip.SetToolTip(this.txt_PalletId, "托盘号");
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(198, 59);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 70;
            this.label22.Text = "货位号";
            // 
            // stbDateTime
            // 
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new System.Drawing.Size(35, 17);
            this.stbDateTime.Text = "时间:";
            // 
            // txt_PltQty
            // 
            this.txt_PltQty.Location = new System.Drawing.Point(414, 55);
            this.txt_PltQty.Name = "txt_PltQty";
            this.txt_PltQty.Size = new System.Drawing.Size(55, 21);
            this.txt_PltQty.TabIndex = 7;
            this.txt_PltQty.Text = "0";
            // 
            // cmb_cAreaId
            // 
            this.cmb_cAreaId.FormattingEnabled = true;
            this.cmb_cAreaId.Location = new System.Drawing.Point(297, 15);
            this.cmb_cAreaId.Name = "cmb_cAreaId";
            this.cmb_cAreaId.Size = new System.Drawing.Size(125, 20);
            this.cmb_cAreaId.TabIndex = 1;
            this.cmb_cAreaId.Tag = "101";
            this.cmb_cAreaId.TextChanged += new System.EventHandler(this.cmb_cAreaId_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(243, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 68;
            this.label20.Text = "仓库区域";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(94, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 67;
            this.label19.Text = "托盘号";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem_Tast,
            this.tsmiLockPallInfo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            this.contextMenuStrip1.Text = "下发任务";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem2.Text = "实盘登记";
            // 
            // toolStripMenuItem_Tast
            // 
            this.toolStripMenuItem_Tast.Name = "toolStripMenuItem_Tast";
            this.toolStripMenuItem_Tast.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_Tast.Text = "下发任务";
            // 
            // tsmiLockPallInfo
            // 
            this.tsmiLockPallInfo.Name = "tsmiLockPallInfo";
            this.tsmiLockPallInfo.Size = new System.Drawing.Size(124, 22);
            this.tsmiLockPallInfo.Text = "查看托盘";
            this.tsmiLockPallInfo.Click += new System.EventHandler(this.tsmiLockPallInfo_Click);
            // 
            // stbModul
            // 
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new System.Drawing.Size(35, 17);
            this.stbModul.Text = "模块:";
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbModul,
            this.stbUser,
            this.stbState,
            this.stbDateTime});
            this.stbMain.Location = new System.Drawing.Point(438, 724);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(737, 22);
            this.stbMain.TabIndex = 24;
            this.stbMain.Text = "statusStrip1";
            // 
            // stbUser
            // 
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new System.Drawing.Size(47, 17);
            this.stbUser.Text = "用户名:";
            // 
            // stbState
            // 
            this.stbState.Name = "stbState";
            this.stbState.Size = new System.Drawing.Size(35, 17);
            this.stbState.Text = "状态:";
            // 
            // button_WorkOrder
            // 
            this.button_WorkOrder.Location = new System.Drawing.Point(211, 141);
            this.button_WorkOrder.Name = "button_WorkOrder";
            this.button_WorkOrder.Size = new System.Drawing.Size(75, 23);
            this.button_WorkOrder.TabIndex = 62;
            this.button_WorkOrder.Text = "下发执行";
            this.tlTip.SetToolTip(this.button_WorkOrder, "将已配好盘的任务，下发执行");
            this.button_WorkOrder.UseVisualStyleBackColor = true;
            this.button_WorkOrder.Click += new System.EventHandler(this.button_WorkOrder_Click);
            // 
            // button_CancelTastWork
            // 
            this.button_CancelTastWork.Location = new System.Drawing.Point(13, 141);
            this.button_CancelTastWork.Name = "button_CancelTastWork";
            this.button_CancelTastWork.Size = new System.Drawing.Size(75, 23);
            this.button_CancelTastWork.TabIndex = 61;
            this.button_CancelTastWork.Text = "取消配盘";
            this.tlTip.SetToolTip(this.button_CancelTastWork, "取消已配盘但未下发执行的配盘数据");
            this.button_CancelTastWork.UseVisualStyleBackColor = true;
            this.button_CancelTastWork.Click += new System.EventHandler(this.button_CancelTastWork_Click);
            // 
            // tlb_M_Save
            // 
            this.tlb_M_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Save.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Save.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Save.Name = "tlb_M_Save";
            this.tlb_M_Save.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Save.Text = "保存";
            this.tlb_M_Save.Visible = false;
            // 
            // tlb_M_Delete
            // 
            this.tlb_M_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Delete.Text = "删除";
            this.tlb_M_Delete.Visible = false;
            // 
            // tlb_M_Find
            // 
            this.tlb_M_Find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Find.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Find.Text = "查找";
            this.tlb_M_Find.Visible = false;
            // 
            // tlb_M_Refresh
            // 
            this.tlb_M_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new System.EventHandler(this.tlb_M_Refresh_Click);
            // 
            // tlb_M_Edit
            // 
            this.tlb_M_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Edit.Text = "修改";
            this.tlb_M_Edit.Visible = false;
            // 
            // tlb_M_New
            // 
            this.tlb_M_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_New.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_New.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_New.Name = "tlb_M_New";
            this.tlb_M_New.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.pnl_MainCount);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.panel1.Size = new System.Drawing.Size(438, 721);
            this.panel1.TabIndex = 21;
            // 
            // dataGridView_Main
            // 
            this.dataGridView_Main.AllowUserToAddRows = false;
            this.dataGridView_Main.AllowUserToDeleteRows = false;
            this.dataGridView_Main.AllowUserToResizeRows = false;
            this.dataGridView_Main.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView_Main.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cBNo,
            this.col_Dtl_cLinkId,
            this.cMNo,
            this.grdcUnPallet,
            this.fQty,
            this.cMName,
            this.cSpec,
            this.cWName,
            this.cBType,
            this.cStatus,
            this.cUnit,
            this.cBatchNo,
            this.fPallet,
            this.fFinished,
            this.cDOStatus});
            this.dataGridView_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Main.Location = new System.Drawing.Point(3, 86);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 23;
            this.dataGridView_Main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Main.Size = new System.Drawing.Size(430, 594);
            this.dataGridView_Main.TabIndex = 15;
            this.dataGridView_Main.Tag = "8";
            // 
            // cBNo
            // 
            this.cBNo.DataPropertyName = "cBNoSeq";
            this.cBNo.HeaderText = "单据号码";
            this.cBNo.Name = "cBNo";
            this.cBNo.ReadOnly = true;
            // 
            // col_Dtl_cLinkId
            // 
            this.col_Dtl_cLinkId.DataPropertyName = "cLinkId";
            this.col_Dtl_cLinkId.HeaderText = "关联单号";
            this.col_Dtl_cLinkId.Name = "col_Dtl_cLinkId";
            this.col_Dtl_cLinkId.ReadOnly = true;
            // 
            // cMNo
            // 
            this.cMNo.DataPropertyName = "cMNo";
            this.cMNo.HeaderText = "物料编码";
            this.cMNo.Name = "cMNo";
            this.cMNo.ReadOnly = true;
            this.cMNo.Width = 80;
            // 
            // grdcUnPallet
            // 
            this.grdcUnPallet.DataPropertyName = "fUnPallet";
            this.grdcUnPallet.HeaderText = "待配盘数";
            this.grdcUnPallet.Name = "grdcUnPallet";
            this.grdcUnPallet.ReadOnly = true;
            this.grdcUnPallet.Width = 65;
            // 
            // fQty
            // 
            this.fQty.DataPropertyName = "fQty";
            this.fQty.HeaderText = "单据数量";
            this.fQty.Name = "fQty";
            this.fQty.ReadOnly = true;
            this.fQty.Width = 65;
            // 
            // cMName
            // 
            this.cMName.DataPropertyName = "cMName";
            this.cMName.HeaderText = "物料名称";
            this.cMName.Name = "cMName";
            this.cMName.ReadOnly = true;
            this.cMName.Width = 130;
            // 
            // cSpec
            // 
            this.cSpec.DataPropertyName = "cSpec";
            this.cSpec.HeaderText = "物料规格";
            this.cSpec.Name = "cSpec";
            this.cSpec.ReadOnly = true;
            // 
            // cWName
            // 
            this.cWName.DataPropertyName = "cWName";
            this.cWName.HeaderText = "仓库名称";
            this.cWName.Name = "cWName";
            this.cWName.ReadOnly = true;
            this.cWName.Width = 70;
            // 
            // cBType
            // 
            this.cBType.DataPropertyName = "cBType";
            this.cBType.HeaderText = "入库名称";
            this.cBType.Name = "cBType";
            this.cBType.ReadOnly = true;
            this.cBType.Width = 70;
            // 
            // cStatus
            // 
            this.cStatus.DataPropertyName = "cStatus";
            this.cStatus.HeaderText = "配盘状态";
            this.cStatus.Name = "cStatus";
            this.cStatus.ReadOnly = true;
            this.cStatus.Width = 50;
            // 
            // cUnit
            // 
            this.cUnit.DataPropertyName = "cUnit";
            this.cUnit.HeaderText = "物料单位";
            this.cUnit.Name = "cUnit";
            this.cUnit.ReadOnly = true;
            this.cUnit.Width = 50;
            // 
            // cBatchNo
            // 
            this.cBatchNo.DataPropertyName = "cBatchNo";
            this.cBatchNo.HeaderText = "批次代号";
            this.cBatchNo.Name = "cBatchNo";
            this.cBatchNo.ReadOnly = true;
            this.cBatchNo.Width = 50;
            // 
            // fPallet
            // 
            this.fPallet.DataPropertyName = "fPallet";
            this.fPallet.HeaderText = "已配盘数";
            this.fPallet.Name = "fPallet";
            this.fPallet.ReadOnly = true;
            this.fPallet.Width = 50;
            // 
            // fFinished
            // 
            this.fFinished.DataPropertyName = "fFinished";
            this.fFinished.HeaderText = "完成数";
            this.fFinished.Name = "fFinished";
            this.fFinished.ReadOnly = true;
            this.fFinished.Width = 50;
            // 
            // cDOStatus
            // 
            this.cDOStatus.DataPropertyName = "cDOStatus";
            this.cDOStatus.HeaderText = "完成状态";
            this.cDOStatus.Name = "cDOStatus";
            this.cDOStatus.ReadOnly = true;
            // 
            // pnl_MainCount
            // 
            this.pnl_MainCount.Controls.Add(this.lbl_MainCount);
            this.pnl_MainCount.Controls.Add(this.label27);
            this.pnl_MainCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_MainCount.Location = new System.Drawing.Point(3, 680);
            this.pnl_MainCount.Name = "pnl_MainCount";
            this.pnl_MainCount.Size = new System.Drawing.Size(430, 38);
            this.pnl_MainCount.TabIndex = 14;
            // 
            // lbl_MainCount
            // 
            this.lbl_MainCount.AutoSize = true;
            this.lbl_MainCount.Location = new System.Drawing.Point(78, 14);
            this.lbl_MainCount.Name = "lbl_MainCount";
            this.lbl_MainCount.Size = new System.Drawing.Size(11, 12);
            this.lbl_MainCount.TabIndex = 1;
            this.lbl_MainCount.Text = "0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(7, 14);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "记录条数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_BillNo);
            this.groupBox1.Controls.Add(this.txt_MNo);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.btn_Reset);
            this.groupBox1.Controls.Add(this.btn_Query);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.cmb_User);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 83);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // txt_BillNo
            // 
            this.txt_BillNo.Location = new System.Drawing.Point(51, 36);
            this.txt_BillNo.Name = "txt_BillNo";
            this.txt_BillNo.Size = new System.Drawing.Size(144, 21);
            this.txt_BillNo.TabIndex = 0;
            this.tlTip.SetToolTip(this.txt_BillNo, "按出库单号或关联单号查询");
            // 
            // txt_MNo
            // 
            this.txt_MNo.Location = new System.Drawing.Point(51, 58);
            this.txt_MNo.Name = "txt_MNo";
            this.txt_MNo.Size = new System.Drawing.Size(144, 21);
            this.txt_MNo.TabIndex = 1;
            this.tlTip.SetToolTip(this.txt_MNo, "按物料编码，名称或拼音五笔简码查询");
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 62);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 12);
            this.label28.TabIndex = 87;
            this.label28.Text = "物料：";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(358, 56);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(68, 23);
            this.btn_Reset.TabIndex = 86;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.Location = new System.Drawing.Point(288, 56);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(68, 23);
            this.btn_Query.TabIndex = 2;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.tlb_M_Refresh_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 40);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(41, 12);
            this.label26.TabIndex = 76;
            this.label26.Text = "单号：";
            // 
            // cmb_User
            // 
            this.cmb_User.FormattingEnabled = true;
            this.cmb_User.Location = new System.Drawing.Point(290, 34);
            this.cmb_User.Name = "cmb_User";
            this.cmb_User.Size = new System.Drawing.Size(134, 20);
            this.cmb_User.TabIndex = 75;
            this.cmb_User.Tag = "101";
            this.cmb_User.TextChanged += new System.EventHandler(this.cmb_User_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(231, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "仓管员：";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label18.Location = new System.Drawing.Point(6, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 16);
            this.label18.TabIndex = 11;
            this.label18.Text = "待配盘数据：";
            // 
            // tlb_M_Undo
            // 
            this.tlb_M_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Undo.Text = "取消";
            this.tlb_M_Undo.Visible = false;
            // 
            // bindingSource_Main
            // 
            this.bindingSource_Main.PositionChanged += new System.EventHandler(this.bindingSource_Main_PositionChanged);
            // 
            // tlbSaveSysRts
            // 
            this.tlbSaveSysRts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new System.Drawing.Size(84, 22);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            // 
            // bindingSource_Detail
            // 
            this.bindingSource_Detail.CurrentChanged += new System.EventHandler(this.bindingSource_Detail_CurrentChanged);
            // 
            // toolStripButton_Audit
            // 
            this.toolStripButton_Audit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Audit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButton_Audit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripButton_Audit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Audit.Name = "toolStripButton_Audit";
            this.toolStripButton_Audit.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton_Audit.Text = "审核";
            this.toolStripButton_Audit.Visible = false;
            // 
            // tlb_M_Print
            // 
            this.tlb_M_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Print.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.Visible = false;
            // 
            // tlb_M_Exit
            // 
            this.tlb_M_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Exit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Exit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Exit.Name = "tlb_M_Exit";
            this.tlb_M_Exit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Exit.Text = "退出";
            this.tlb_M_Exit.Click += new System.EventHandler(this.tlb_M_Exit_Click);
            // 
            // btn_M_Help
            // 
            this.btn_M_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_M_Help.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btn_M_Help.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_M_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_M_Help.Name = "btn_M_Help";
            this.btn_M_Help.Size = new System.Drawing.Size(35, 22);
            this.btn_M_Help.Text = "帮助";
            this.btn_M_Help.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 66;
            this.label6.Text = "配盘数量";
            // 
            // dataGridView_Detail
            // 
            this.dataGridView_Detail.AllowUserToAddRows = false;
            this.dataGridView_Detail.AllowUserToDeleteRows = false;
            this.dataGridView_Detail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdc_Dtl_cWHId,
            this.grdc_Dtl_cAreaName,
            this.grdc_Dtl_cPosId,
            this.col_Dtl_cMName,
            this.col_Dtl_cSpecDesc,
            this.grdc_Dtl_cBatchNo,
            this.grdc_Dtl_fQty,
            this.grdc_Dtl_cPalletId,
            this.grdc_Dtl_cStatusWork,
            this.col_Dtl_cDtlRemark,
            this.grdc_Dtl_cBNoIn,
            this.grdc_Dtl_nItemIn,
            this.grdc_Dtl_nStatusWork,
            this.grdc_Dtl_cWHType,
            this.grdc_Dtl_cBoxId});
            this.dataGridView_Detail.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Detail.Location = new System.Drawing.Point(0, 90);
            this.dataGridView_Detail.Name = "dataGridView_Detail";
            this.dataGridView_Detail.ReadOnly = true;
            this.dataGridView_Detail.RowHeadersVisible = false;
            this.dataGridView_Detail.RowTemplate.Height = 23;
            this.dataGridView_Detail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Detail.Size = new System.Drawing.Size(737, 310);
            this.dataGridView_Detail.TabIndex = 11;
            this.dataGridView_Detail.Tag = "8";
            this.dataGridView_Detail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Detail_CellDoubleClick);
            // 
            // grdc_Dtl_cWHId
            // 
            this.grdc_Dtl_cWHId.DataPropertyName = "cWHId";
            this.grdc_Dtl_cWHId.HeaderText = "仓库";
            this.grdc_Dtl_cWHId.Name = "grdc_Dtl_cWHId";
            this.grdc_Dtl_cWHId.ReadOnly = true;
            this.grdc_Dtl_cWHId.Width = 50;
            // 
            // grdc_Dtl_cAreaName
            // 
            this.grdc_Dtl_cAreaName.DataPropertyName = "cAreaName";
            this.grdc_Dtl_cAreaName.HeaderText = "区域";
            this.grdc_Dtl_cAreaName.Name = "grdc_Dtl_cAreaName";
            this.grdc_Dtl_cAreaName.ReadOnly = true;
            this.grdc_Dtl_cAreaName.ToolTipText = "货区区域";
            this.grdc_Dtl_cAreaName.Width = 65;
            // 
            // grdc_Dtl_cPosId
            // 
            this.grdc_Dtl_cPosId.DataPropertyName = "cPosId";
            this.grdc_Dtl_cPosId.HeaderText = "货位号";
            this.grdc_Dtl_cPosId.Name = "grdc_Dtl_cPosId";
            this.grdc_Dtl_cPosId.ReadOnly = true;
            // 
            // col_Dtl_cMName
            // 
            this.col_Dtl_cMName.DataPropertyName = "cMName";
            this.col_Dtl_cMName.HeaderText = "物料名";
            this.col_Dtl_cMName.Name = "col_Dtl_cMName";
            this.col_Dtl_cMName.ReadOnly = true;
            // 
            // col_Dtl_cSpecDesc
            // 
            this.col_Dtl_cSpecDesc.DataPropertyName = "cSpecDesc";
            this.col_Dtl_cSpecDesc.HeaderText = "物料规格";
            this.col_Dtl_cSpecDesc.Name = "col_Dtl_cSpecDesc";
            this.col_Dtl_cSpecDesc.ReadOnly = true;
            // 
            // grdc_Dtl_cBatchNo
            // 
            this.grdc_Dtl_cBatchNo.DataPropertyName = "cBatchNo";
            this.grdc_Dtl_cBatchNo.HeaderText = "批号";
            this.grdc_Dtl_cBatchNo.Name = "grdc_Dtl_cBatchNo";
            this.grdc_Dtl_cBatchNo.ReadOnly = true;
            this.grdc_Dtl_cBatchNo.Width = 65;
            // 
            // grdc_Dtl_fQty
            // 
            this.grdc_Dtl_fQty.DataPropertyName = "fQty";
            this.grdc_Dtl_fQty.HeaderText = "可用数量";
            this.grdc_Dtl_fQty.Name = "grdc_Dtl_fQty";
            this.grdc_Dtl_fQty.ReadOnly = true;
            this.grdc_Dtl_fQty.Width = 75;
            // 
            // grdc_Dtl_cPalletId
            // 
            this.grdc_Dtl_cPalletId.DataPropertyName = "nPalletId";
            this.grdc_Dtl_cPalletId.HeaderText = "托盘号";
            this.grdc_Dtl_cPalletId.Name = "grdc_Dtl_cPalletId";
            this.grdc_Dtl_cPalletId.ReadOnly = true;
            this.grdc_Dtl_cPalletId.Width = 75;
            // 
            // grdc_Dtl_cStatusWork
            // 
            this.grdc_Dtl_cStatusWork.DataPropertyName = "cStatusWork";
            this.grdc_Dtl_cStatusWork.HeaderText = "工作状态";
            this.grdc_Dtl_cStatusWork.Name = "grdc_Dtl_cStatusWork";
            this.grdc_Dtl_cStatusWork.ReadOnly = true;
            this.grdc_Dtl_cStatusWork.Width = 65;
            // 
            // col_Dtl_cDtlRemark
            // 
            this.col_Dtl_cDtlRemark.DataPropertyName = "cDtlRemark";
            this.col_Dtl_cDtlRemark.HeaderText = "备注";
            this.col_Dtl_cDtlRemark.Name = "col_Dtl_cDtlRemark";
            this.col_Dtl_cDtlRemark.ReadOnly = true;
            // 
            // grdc_Dtl_cBNoIn
            // 
            this.grdc_Dtl_cBNoIn.DataPropertyName = "cBNoIn";
            this.grdc_Dtl_cBNoIn.HeaderText = "入库单号";
            this.grdc_Dtl_cBNoIn.Name = "grdc_Dtl_cBNoIn";
            this.grdc_Dtl_cBNoIn.ReadOnly = true;
            this.grdc_Dtl_cBNoIn.Width = 80;
            // 
            // grdc_Dtl_nItemIn
            // 
            this.grdc_Dtl_nItemIn.DataPropertyName = "nItemIn";
            this.grdc_Dtl_nItemIn.HeaderText = "明细序号";
            this.grdc_Dtl_nItemIn.Name = "grdc_Dtl_nItemIn";
            this.grdc_Dtl_nItemIn.ReadOnly = true;
            this.grdc_Dtl_nItemIn.ToolTipText = "入库单明细序号";
            this.grdc_Dtl_nItemIn.Width = 65;
            // 
            // grdc_Dtl_nStatusWork
            // 
            this.grdc_Dtl_nStatusWork.DataPropertyName = "nStatusWork";
            this.grdc_Dtl_nStatusWork.HeaderText = "状态";
            this.grdc_Dtl_nStatusWork.Name = "grdc_Dtl_nStatusWork";
            this.grdc_Dtl_nStatusWork.ReadOnly = true;
            this.grdc_Dtl_nStatusWork.ToolTipText = "cStatusWork";
            this.grdc_Dtl_nStatusWork.Visible = false;
            // 
            // grdc_Dtl_cWHType
            // 
            this.grdc_Dtl_cWHType.DataPropertyName = "cWHType";
            this.grdc_Dtl_cWHType.HeaderText = "库类型";
            this.grdc_Dtl_cWHType.Name = "grdc_Dtl_cWHType";
            this.grdc_Dtl_cWHType.ReadOnly = true;
            this.grdc_Dtl_cWHType.Width = 65;
            // 
            // grdc_Dtl_cBoxId
            // 
            this.grdc_Dtl_cBoxId.DataPropertyName = "cBoxId";
            this.grdc_Dtl_cBoxId.HeaderText = "周转箱号";
            this.grdc_Dtl_cBoxId.Name = "grdc_Dtl_cBoxId";
            this.grdc_Dtl_cBoxId.ReadOnly = true;
            this.grdc_Dtl_cBoxId.Width = 65;
            // 
            // groupBox_InType
            // 
            this.groupBox_InType.Controls.Add(this.radioButton_PartIn);
            this.groupBox_InType.Controls.Add(this.radioButton_AllIn);
            this.groupBox_InType.Location = new System.Drawing.Point(13, 9);
            this.groupBox_InType.Name = "groupBox_InType";
            this.groupBox_InType.Size = new System.Drawing.Size(69, 73);
            this.groupBox_InType.TabIndex = 58;
            this.groupBox_InType.TabStop = false;
            this.groupBox_InType.Text = "出库类型";
            // 
            // radioButton_PartIn
            // 
            this.radioButton_PartIn.AutoSize = true;
            this.radioButton_PartIn.Checked = true;
            this.radioButton_PartIn.Location = new System.Drawing.Point(9, 22);
            this.radioButton_PartIn.Name = "radioButton_PartIn";
            this.radioButton_PartIn.Size = new System.Drawing.Size(47, 16);
            this.radioButton_PartIn.TabIndex = 57;
            this.radioButton_PartIn.TabStop = true;
            this.radioButton_PartIn.Text = "拣选";
            this.radioButton_PartIn.UseVisualStyleBackColor = true;
            // 
            // radioButton_AllIn
            // 
            this.radioButton_AllIn.AutoSize = true;
            this.radioButton_AllIn.Location = new System.Drawing.Point(9, 52);
            this.radioButton_AllIn.Name = "radioButton_AllIn";
            this.radioButton_AllIn.Size = new System.Drawing.Size(47, 16);
            this.radioButton_AllIn.TabIndex = 56;
            this.radioButton_AllIn.Text = "整出";
            this.radioButton_AllIn.UseVisualStyleBackColor = true;
            this.radioButton_AllIn.CheckedChanged += new System.EventHandler(this.radioButton_Hand_CheckedChanged);
            // 
            // tlTip
            // 
            this.tlTip.IsBalloon = true;
            // 
            // btn_UnDoCmd
            // 
            this.btn_UnDoCmd.Location = new System.Drawing.Point(112, 141);
            this.btn_UnDoCmd.Name = "btn_UnDoCmd";
            this.btn_UnDoCmd.Size = new System.Drawing.Size(75, 23);
            this.btn_UnDoCmd.TabIndex = 63;
            this.btn_UnDoCmd.Text = "取消下发";
            this.tlTip.SetToolTip(this.btn_UnDoCmd, "取消已下发但未执行的任务");
            this.btn_UnDoCmd.UseVisualStyleBackColor = true;
            this.btn_UnDoCmd.Click += new System.EventHandler(this.btn_UnDoCmd_Click);
            // 
            // btn_OutByManual
            // 
            this.btn_OutByManual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OutByManual.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_OutByManual.Location = new System.Drawing.Point(77, 5);
            this.btn_OutByManual.Name = "btn_OutByManual";
            this.btn_OutByManual.Size = new System.Drawing.Size(107, 23);
            this.btn_OutByManual.TabIndex = 26;
            this.btn_OutByManual.Text = "手动配盘";
            this.tlTip.SetToolTip(this.btn_OutByManual, "手动出库配盘");
            this.btn_OutByManual.UseVisualStyleBackColor = true;
            this.btn_OutByManual.Visible = false;
            this.btn_OutByManual.Click += new System.EventHandler(this.btn_OutByManual_Click);
            // 
            // btn_OutByAuto
            // 
            this.btn_OutByAuto.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OutByAuto.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_OutByAuto.Location = new System.Drawing.Point(195, 5);
            this.btn_OutByAuto.Name = "btn_OutByAuto";
            this.btn_OutByAuto.Size = new System.Drawing.Size(99, 23);
            this.btn_OutByAuto.TabIndex = 27;
            this.btn_OutByAuto.Text = "自动配盘";
            this.tlTip.SetToolTip(this.btn_OutByAuto, "自动出库配盘");
            this.btn_OutByAuto.UseVisualStyleBackColor = true;
            this.btn_OutByAuto.Visible = false;
            this.btn_OutByAuto.Click += new System.EventHandler(this.btn_OutByAuto_Click);
            // 
            // textBox_cBatchNo
            // 
            this.textBox_cBatchNo.Location = new System.Drawing.Point(62, 50);
            this.textBox_cBatchNo.Name = "textBox_cBatchNo";
            this.textBox_cBatchNo.ReadOnly = true;
            this.textBox_cBatchNo.Size = new System.Drawing.Size(100, 21);
            this.textBox_cBatchNo.TabIndex = 16;
            this.textBox_cBatchNo.Tag = "0";
            // 
            // textBox_fFinished
            // 
            this.textBox_fFinished.Location = new System.Drawing.Point(462, 50);
            this.textBox_fFinished.Name = "textBox_fFinished";
            this.textBox_fFinished.ReadOnly = true;
            this.textBox_fFinished.Size = new System.Drawing.Size(53, 21);
            this.textBox_fFinished.TabIndex = 49;
            this.textBox_fFinished.Tag = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(286, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 53;
            this.label14.Text = "已配盘数";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator5.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
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
            this.toolStripSeparator});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(1175, 25);
            this.tlbMain.TabIndex = 20;
            this.tlbMain.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator6.Visible = false;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator7.Visible = false;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator8.Visible = false;
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator.Visible = false;
            // 
            // panel_Edit
            // 
            this.panel_Edit.Controls.Add(this.label24);
            this.panel_Edit.Controls.Add(this.textBox_fFinished);
            this.panel_Edit.Controls.Add(this.textBox_cBatchNo);
            this.panel_Edit.Controls.Add(this.label14);
            this.panel_Edit.Controls.Add(this.label15);
            this.panel_Edit.Controls.Add(this.textBox_fPallet);
            this.panel_Edit.Controls.Add(this.textBox_fQty);
            this.panel_Edit.Controls.Add(this.textBox_cSpec);
            this.panel_Edit.Controls.Add(this.textBox_cMNo);
            this.panel_Edit.Controls.Add(this.textBox_cDept);
            this.panel_Edit.Controls.Add(this.textBox_cBType);
            this.panel_Edit.Controls.Add(this.label12);
            this.panel_Edit.Controls.Add(this.label11);
            this.panel_Edit.Controls.Add(this.label9);
            this.panel_Edit.Controls.Add(this.label8);
            this.panel_Edit.Controls.Add(this.label7);
            this.panel_Edit.Controls.Add(this.label5);
            this.panel_Edit.Controls.Add(this.label3);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cStatus);
            this.panel_Edit.Controls.Add(this.textBox_cBNo);
            this.panel_Edit.Controls.Add(this.textBox_CMName);
            this.panel_Edit.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Edit.Location = new System.Drawing.Point(438, 25);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new System.Drawing.Size(737, 75);
            this.panel_Edit.TabIndex = 23;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(528, 7);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 54;
            this.label24.Text = "物资种类";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(405, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 52;
            this.label15.Text = "已完成数";
            // 
            // textBox_fPallet
            // 
            this.textBox_fPallet.Location = new System.Drawing.Point(340, 50);
            this.textBox_fPallet.Name = "textBox_fPallet";
            this.textBox_fPallet.ReadOnly = true;
            this.textBox_fPallet.Size = new System.Drawing.Size(53, 21);
            this.textBox_fPallet.TabIndex = 50;
            this.textBox_fPallet.Tag = "0";
            this.textBox_fPallet.Text = "99999.99";
            // 
            // textBox_fQty
            // 
            this.textBox_fQty.Location = new System.Drawing.Point(221, 50);
            this.textBox_fQty.Name = "textBox_fQty";
            this.textBox_fQty.ReadOnly = true;
            this.textBox_fQty.Size = new System.Drawing.Size(53, 21);
            this.textBox_fQty.TabIndex = 47;
            this.textBox_fQty.Tag = "0";
            // 
            // textBox_cSpec
            // 
            this.textBox_cSpec.Location = new System.Drawing.Point(592, 26);
            this.textBox_cSpec.Name = "textBox_cSpec";
            this.textBox_cSpec.ReadOnly = true;
            this.textBox_cSpec.Size = new System.Drawing.Size(102, 21);
            this.textBox_cSpec.TabIndex = 46;
            this.textBox_cSpec.Tag = "0";
            // 
            // textBox_cMNo
            // 
            this.textBox_cMNo.Location = new System.Drawing.Point(62, 26);
            this.textBox_cMNo.Name = "textBox_cMNo";
            this.textBox_cMNo.ReadOnly = true;
            this.textBox_cMNo.Size = new System.Drawing.Size(100, 21);
            this.textBox_cMNo.TabIndex = 45;
            this.textBox_cMNo.Tag = "0";
            // 
            // textBox_cDept
            // 
            this.textBox_cDept.Location = new System.Drawing.Point(413, 2);
            this.textBox_cDept.Name = "textBox_cDept";
            this.textBox_cDept.ReadOnly = true;
            this.textBox_cDept.Size = new System.Drawing.Size(102, 21);
            this.textBox_cDept.TabIndex = 44;
            this.textBox_cDept.Tag = "0";
            // 
            // textBox_cBType
            // 
            this.textBox_cBType.Location = new System.Drawing.Point(221, 2);
            this.textBox_cBType.Name = "textBox_cBType";
            this.textBox_cBType.ReadOnly = true;
            this.textBox_cBType.Size = new System.Drawing.Size(109, 21);
            this.textBox_cBType.TabIndex = 42;
            this.textBox_cBType.Tag = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(168, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 41;
            this.label12.Text = "单据数量";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(552, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 36;
            this.label11.Text = "规格";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 34;
            this.label9.Text = "物料编码";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(168, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "物料名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(378, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "部门";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "单据号码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "出库类别";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "批　　号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(552, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "状态";
            // 
            // textBox_cStatus
            // 
            this.textBox_cStatus.Location = new System.Drawing.Point(592, 50);
            this.textBox_cStatus.Name = "textBox_cStatus";
            this.textBox_cStatus.ReadOnly = true;
            this.textBox_cStatus.Size = new System.Drawing.Size(102, 21);
            this.textBox_cStatus.TabIndex = 23;
            this.textBox_cStatus.Tag = "0";
            // 
            // textBox_cBNo
            // 
            this.textBox_cBNo.Location = new System.Drawing.Point(62, 2);
            this.textBox_cBNo.Name = "textBox_cBNo";
            this.textBox_cBNo.ReadOnly = true;
            this.textBox_cBNo.Size = new System.Drawing.Size(100, 21);
            this.textBox_cBNo.TabIndex = 21;
            this.textBox_cBNo.Tag = "0";
            // 
            // textBox_CMName
            // 
            this.textBox_CMName.Location = new System.Drawing.Point(221, 26);
            this.textBox_CMName.Name = "textBox_CMName";
            this.textBox_CMName.ReadOnly = true;
            this.textBox_CMName.Size = new System.Drawing.Size(294, 21);
            this.textBox_CMName.TabIndex = 18;
            this.textBox_CMName.Tag = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Info;
            this.tabPage2.Controls.Add(this.btn_UnDoCmd);
            this.tabPage2.Controls.Add(this.dataGridView_HasPallet);
            this.tabPage2.Controls.Add(this.button_WorkOrder);
            this.tabPage2.Controls.Add(this.button_CancelTastWork);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(729, 166);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已配盘";
            // 
            // dataGridView_HasPallet
            // 
            this.dataGridView_HasPallet.AllowUserToAddRows = false;
            this.dataGridView_HasPallet.AllowUserToDeleteRows = false;
            this.dataGridView_HasPallet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_HasPallet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nWorkId,
            this.nPalletId,
            this.Column11,
            this.Column9,
            this.Column6,
            this.colcName,
            this.Column7,
            this.Column8,
            this.grdPlt_nWKStatus,
            this.col_Plt_cWKStatusDesc,
            this.Column4,
            this.colcSpec,
            this.Column5,
            this.Column10,
            this.Column1,
            this.Column2});
            this.dataGridView_HasPallet.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView_HasPallet.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView_HasPallet.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_HasPallet.Name = "dataGridView_HasPallet";
            this.dataGridView_HasPallet.ReadOnly = true;
            this.dataGridView_HasPallet.RowHeadersVisible = false;
            this.dataGridView_HasPallet.RowTemplate.Height = 23;
            this.dataGridView_HasPallet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_HasPallet.Size = new System.Drawing.Size(729, 138);
            this.dataGridView_HasPallet.TabIndex = 13;
            this.dataGridView_HasPallet.Tag = "8";
            // 
            // nWorkId
            // 
            this.nWorkId.DataPropertyName = "nWorkId";
            this.nWorkId.HeaderText = "任务号";
            this.nWorkId.Name = "nWorkId";
            this.nWorkId.ReadOnly = true;
            this.nWorkId.Width = 65;
            // 
            // nPalletId
            // 
            this.nPalletId.DataPropertyName = "nPalletId";
            this.nPalletId.HeaderText = "托盘号";
            this.nPalletId.Name = "nPalletId";
            this.nPalletId.ReadOnly = true;
            this.nPalletId.Width = 50;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "cOptTypeDesc";
            this.Column11.HeaderText = "操作类型";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 65;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "cPosIdTo";
            this.Column9.HeaderText = "目标货位";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 65;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "cMNo";
            this.Column6.HeaderText = "物料号";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 80;
            // 
            // colcName
            // 
            this.colcName.DataPropertyName = "cName";
            this.colcName.HeaderText = "物料名";
            this.colcName.Name = "colcName";
            this.colcName.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "cBatchNo";
            this.Column7.HeaderText = "批号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 70;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "fQty";
            this.Column8.HeaderText = "数量";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 70;
            // 
            // grdPlt_nWKStatus
            // 
            this.grdPlt_nWKStatus.DataPropertyName = "nWKStatus";
            this.grdPlt_nWKStatus.HeaderText = "执行状态";
            this.grdPlt_nWKStatus.Name = "grdPlt_nWKStatus";
            this.grdPlt_nWKStatus.ReadOnly = true;
            this.grdPlt_nWKStatus.Visible = false;
            // 
            // col_Plt_cWKStatusDesc
            // 
            this.col_Plt_cWKStatusDesc.DataPropertyName = "cWKStatusDesc";
            this.col_Plt_cWKStatusDesc.HeaderText = "执行状态";
            this.col_Plt_cWKStatusDesc.Name = "col_Plt_cWKStatusDesc";
            this.col_Plt_cWKStatusDesc.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "cBNo";
            this.Column4.HeaderText = "单号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // colcSpec
            // 
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "nItem";
            this.Column5.HeaderText = "项次";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 65;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "cWKStatusDesc";
            this.Column10.HeaderText = "工作状态";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 65;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "nOptStation";
            this.Column1.HeaderText = "入库口";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 65;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cWHId";
            this.Column2.HeaderText = "仓库号码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 65;
            // 
            // pnl_PltByManual
            // 
            this.pnl_PltByManual.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_PltByManual.Controls.Add(this.dataGridView_Detail);
            this.pnl_PltByManual.Controls.Add(this.panel3);
            this.pnl_PltByManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_PltByManual.Location = new System.Drawing.Point(438, 132);
            this.pnl_PltByManual.Name = "pnl_PltByManual";
            this.pnl_PltByManual.Size = new System.Drawing.Size(737, 400);
            this.pnl_PltByManual.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_PltQty);
            this.panel3.Controls.Add(this.cmb_OptGroup);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.cmb_Port);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txt_PosId);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.groupBox_InType);
            this.panel3.Controls.Add(this.txt_Layer);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txt_Col);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.txt_Row);
            this.panel3.Controls.Add(this.cmb_cAreaId);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.txt_PalletId);
            this.panel3.Controls.Add(this.cmb_cWHId);
            this.panel3.Controls.Add(this.btn_OK);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.btn_SelPosId);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(737, 90);
            this.panel3.TabIndex = 85;
            // 
            // cmb_OptGroup
            // 
            this.cmb_OptGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_OptGroup.FormattingEnabled = true;
            this.cmb_OptGroup.Location = new System.Drawing.Point(544, 56);
            this.cmb_OptGroup.Name = "cmb_OptGroup";
            this.cmb_OptGroup.Size = new System.Drawing.Size(74, 20);
            this.cmb_OptGroup.TabIndex = 102;
            this.cmb_OptGroup.SelectedIndexChanged += new System.EventHandler(this.cmb_OptGroup_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(479, 60);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 12);
            this.label30.TabIndex = 101;
            this.label30.Text = "操作台组别";
            // 
            // cmb_Port
            // 
            this.cmb_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Port.FormattingEnabled = true;
            this.cmb_Port.Location = new System.Drawing.Point(643, 55);
            this.cmb_Port.Name = "cmb_Port";
            this.cmb_Port.Size = new System.Drawing.Size(37, 20);
            this.cmb_Port.TabIndex = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(618, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 99;
            this.label4.Text = "台号";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Blue;
            this.label25.Location = new System.Drawing.Point(91, 79);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(642, 1);
            this.label25.TabIndex = 85;
            // 
            // txt_Layer
            // 
            this.txt_Layer.Location = new System.Drawing.Point(662, 15);
            this.txt_Layer.Name = "txt_Layer";
            this.txt_Layer.Size = new System.Drawing.Size(32, 21);
            this.txt_Layer.TabIndex = 4;
            this.txt_Layer.Text = "0";
            this.txt_Layer.TextChanged += new System.EventHandler(this.txt_Row_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(622, 19);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 84;
            this.label23.Text = "层号";
            // 
            // txt_Col
            // 
            this.txt_Col.Location = new System.Drawing.Point(582, 15);
            this.txt_Col.Name = "txt_Col";
            this.txt_Col.Size = new System.Drawing.Size(32, 21);
            this.txt_Col.TabIndex = 3;
            this.txt_Col.Text = "0";
            this.txt_Col.TextChanged += new System.EventHandler(this.txt_Row_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(549, 19);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 82;
            this.label21.Text = "列号";
            // 
            // txt_Row
            // 
            this.txt_Row.Location = new System.Drawing.Point(474, 15);
            this.txt_Row.Name = "txt_Row";
            this.txt_Row.Size = new System.Drawing.Size(47, 21);
            this.txt_Row.TabIndex = 2;
            this.txt_Row.Text = "0";
            this.txt_Row.TextChanged += new System.EventHandler(this.txt_Row_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(437, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 80;
            this.label16.Text = "排号";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(91, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(642, 1);
            this.label13.TabIndex = 78;
            // 
            // cmb_cWHId
            // 
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new System.Drawing.Point(145, 15);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new System.Drawing.Size(95, 20);
            this.cmb_cWHId.TabIndex = 0;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.SelectedIndexChanged += new System.EventHandler(this.cmb_cWHId_SelectedIndexChanged);
            this.cmb_cWHId.TextChanged += new System.EventHandler(this.cmb_cWHId_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(94, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 77;
            this.label17.Text = "仓  库";
            // 
            // tabControl1_HasPallet
            // 
            this.tabControl1_HasPallet.Controls.Add(this.tabPage2);
            this.tabControl1_HasPallet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1_HasPallet.Location = new System.Drawing.Point(438, 532);
            this.tabControl1_HasPallet.Name = "tabControl1_HasPallet";
            this.tabControl1_HasPallet.SelectedIndex = 0;
            this.tabControl1_HasPallet.Size = new System.Drawing.Size(737, 192);
            this.tabControl1_HasPallet.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_OutByManual);
            this.panel2.Controls.Add(this.btn_OutByAuto);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(438, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(737, 32);
            this.panel2.TabIndex = 28;
            // 
            // FrmStockMPltWMSOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1175, 746);
            this.Controls.Add(this.pnl_PltByManual);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_Edit);
            this.Controls.Add(this.tabControl1_HasPallet);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tlbMain);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FrmStockMPltWMSOut";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "立库出库配盘";
            this.Load += new System.EventHandler(this.FrmStockMPltWMSOut_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Main)).EndInit();
            this.pnl_MainCount.ResumeLayout(false);
            this.pnl_MainCount.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_HasPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Detail)).EndInit();
            this.groupBox_InType.ResumeLayout(false);
            this.groupBox_InType.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_HasPallet)).EndInit();
            this.pnl_PltByManual.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1_HasPallet.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadAreaList(string sWHId)
        {
            StringBuilder builder = new StringBuilder("select A.*,WA.cAreaName from ( ");
            builder.Append("select distinct isnull(cAreaId,' ') cAreaId from TWC_WareCell ");
            if (sWHId.Trim().Length > 0)
            {
                builder.Append("  where cWHId='" + sWHId.Trim() + "'");
            }
            builder.Append(") A  inner join TWC_WArea WA on A.cAreaId=WA.cAreaId");
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TWC_WArea", 0, 0, "", out sErr);
            if (sErr.Trim().Length > 0)
            {
                MessageBox.Show("打开仓库区域数据时出错：" + sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开仓库区域数据时出错：" + sErr);
            }
            else
            {
                this.bIsOpenForArea = false;
                DataTable table = set.Tables["TWC_WArea"];
                this.cmb_cAreaId.DataSource = table;
                this.cmb_cAreaId.DisplayMember = "cAreaName";
                this.cmb_cAreaId.ValueMember = "cAreaId";
                this.bIsOpenForArea = true;
                this.cmb_cAreaId_TextChanged(null, null);
            }
        }

        private void LoadCombWare(ComboBox cmbX)
        {
            string sSql = "select cWHId,cName from TWC_WareHouse  where bUsed=1 ";
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select distinct cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId + "')";
            }
            string sErr = "";
            DataSet set = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TWC_WareHouse", 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
                return;
            }
            if ((sErr.Trim() != "") && (sErr.Trim() != "-1"))
            {
                MessageBox.Show(sErr);
            }
            this.bIsOpenForWH = false;
            cmbX.ValueMember = "cWHId";
            cmbX.DisplayMember = "cName";
            if (set != null)
            {
                DataTable table = set.Tables["TWC_WareHouse"];
                cmbX.DataSource = table;
            }
            this.bIsOpenForWH = true;
        }

        private void LoadCombWAreaList(string sWHId, ComboBox cmbX)
        {
            string sSql = "select cAreaId,cAreaName from TWC_WArea where bUsed=1 ";
            string sErr = "";
            DataSet set = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TWC_WArea", 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
                return;
            }
            if ((sErr.Trim() != "") && (sErr.Trim() != "-1"))
            {
                MessageBox.Show(sErr);
            }
            cmbX.ValueMember = "cAreaId";
            cmbX.DisplayMember = "cAreaName";
            if (set != null)
            {
                DataTable table = set.Tables["TWC_WArea"];
                cmbX.DataSource = table;
            }
        }

        private void LoadOptGroup(string sWHId)
        {
            string sSql = "select distinct cGroupName from TECS_HSInfo ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + sWHId.Trim() + "'";
            }
            string sErr = "";
            DataSet set = null;
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "OptGroup", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set != null)
            {
                DataTable table = set.Tables["OptGroup"];
                if (table != null)
                {
                    this.cmb_OptGroup.DisplayMember = "cGroupName";
                    this.cmb_OptGroup.ValueMember = "cGroupName";
                    this.cmb_OptGroup.DataSource = table;
                    string sValue = "";
                    if (MyConfigure.ReadMyArributeValue(base.AppInformation.AppPath + @"\" + base.AppInformation.AppConfigFile, "config/ECS/OptGroup", "Default", out sValue, out sErr))
                    {
                        if (this.cmb_OptGroup.Items.Count > 0)
                        {
                            if (sValue.Trim() != "")
                            {
                                this.cmb_OptGroup.SelectedValue = sValue;
                            }
                            else
                            {
                                this.cmb_OptGroup.SelectedIndex = 0;
                            }
                        }
                    }
                    else if (this.cmb_OptGroup.Items.Count > 0)
                    {
                        this.cmb_OptGroup.SelectedIndex = 0;
                    }
                }
            }
        }

        private void LoadOptNoList(string sWHId, string sOptGroup, int nRow)
        {
            int num = 1;
            if (nRow > 0)
            {
                num = nRow;
            }
            num = (num + 1) / 2;
            string sSql = "select nOptNo from TECS_HSInfo where 1=1 ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " and cWHId='" + sWHId.Trim() + "'";
            }
            if (sOptGroup.Trim() != "")
            {
                sSql = sSql + " and cGroupName='" + sOptGroup.Trim() + "'";
            }
            if (num > 0)
            {
                sSql = sSql + " and nLine=" + num.ToString();
            }
            string sErr = "";
            DataSet set = null;
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "OptNo", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set != null)
            {
                DataTable table = set.Tables["OptNo"];
                if (table != null)
                {
                    this.cmb_Port.DisplayMember = "nOptNo";
                    this.cmb_Port.ValueMember = "nOptNo";
                    this.cmb_Port.DataSource = table;
                    if (this.cmb_Port.Items.Count > 0)
                    {
                        this.cmb_Port.SelectedIndex = 0;
                    }
                }
            }
        }

        private void LoadUser()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("select cName from TPB_User where  bUsed = 1 and cCmptId='" + base.UserInformation.UnitId + "' ");
            if (base.UserInformation.UType == UserType.utNormal)
            {
                builder.Append(" and cName='" + base.UserInformation.UserName.Trim() + "'");
            }
            if (base.UserInformation.UType == UserType.utAdmin)
            {
                builder.Append(" and cDeptId='" + base.UserInformation.DeptId.Trim() + "'");
            }
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TPB_User", 0, 0, "", out sErr);
            if (sErr.Trim().Length > 0)
            {
                MessageBox.Show("打开用户数据时出错：" + sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开用户数据时出错：" + sErr);
            }
            else
            {
                try
                {
                    this.bIsOpenUser = false;
                    DataTable table = set.Tables["TPB_User"];
                    this.cmb_User.DisplayMember = "cName";
                    this.cmb_User.ValueMember = "cName";
                    this.cmb_User.DataSource = table;
                    this.bIsOpenUser = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                if (this.cmb_User.Items.Count > 0)
                {
                    if (base.UserInformation.UType == UserType.utSupervisor)
                    {
                        this.cmb_User.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cmb_User.SelectedValue = base.UserInformation.UserName.Trim();
                    }
                }
                this.cmb_User_TextChanged(null, null);
            }
        }

        private void LoadWareHouseList()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("select * from V_WareHouse where bUsed = 1 and cCmptId='" + base.UserInformation.UnitId + "' ");
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(" and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TWC_WareHouse", 0, 0, "", out sErr);
            if (sErr.Trim().Length > 0)
            {
                MessageBox.Show("打开仓库数据时出错：" + sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开仓库数据时出错：" + sErr);
            }
            else
            {
                this.bIsOpenForWH = false;
                try
                {
                    DataTable table = set.Tables["TWC_WareHouse"];
                    this.cmb_cWHId.DisplayMember = "cName";
                    this.cmb_cWHId.ValueMember = "cWHId";
                    this.cmb_cWHId.DataSource = table;
                    this.bIsOpenForWH = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                if (this.cmb_cWHId.Items.Count > 0)
                {
                    this.cmb_cWHId.SelectedIndex = -1;
                    this.cmb_cWHId.Text = "";
                    this.cmb_cAreaId.Text = "";
                }
            }
        }

        public DialogResult MyMessageBox(string tipInfo, string tipTitle)
        {
            return MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        private void radioButton_Hand_CheckedChanged(object sender, EventArgs e)
        {
            this.GetPalletList(false);
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.cmb_User_TextChanged(null, null);
            this.DoRefreshHasPallet();
        }

        private void tsmiLockPallInfo_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_Detail.SelectedRows.Count != 1)
            {
                this.MyMessageBox("请选择一条记录！", "提示");
            }
            else
            {
                string pallStr = this.dataGridView_Detail.SelectedRows[0].Cells["grdc_Dtl_cPalletId"].Value.ToString();
                new LockPallInfo { mydt = this.GetPallReceInfoData(pallStr) }.ShowDialog();
            }
        }

        private void txt_Row_TextChanged(object sender, EventArgs e)
        {
            this.GetPalletList(false);
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

