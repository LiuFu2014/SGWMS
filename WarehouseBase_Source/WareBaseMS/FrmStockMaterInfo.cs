namespace WareBaseMS
{
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using UserMS;
    using CommBase;
    using DBCommInfo;
    using FileFun;
    using Zqm.Text;
    using Zqm.Xml;

    public class FrmStockMaterInfo : FrmSTable
    {
        private bool bCodeIsManual = false;
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private DataGridViewTextBoxColumn bIsFromERP;
        private BindingSource bsGrid;
        private Button btn_Dtl_Delete;
        private Button btn_Dtl_Edit;
        private Button btn_Dtl_New;
        private Button btn_Dtl_Save;
        private Button btn_Dtl_Undo;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private Button btn_Reset;
        private Button btn_SelSupplier;
        private bool bUnitIsOpen = false;
        private DataGridViewTextBoxColumn cCreator;
        private ComboBox cmb_bIsAutoBatchNo;
        private ComboBox cmb_bIsBaseMedic;
        private ComboBox cmb_bIsCoolStoreMedic;
        private ComboBox cmb_bIsFromErp;
        private ComboBox cmb_bIsHightProfit;
        private ComboBox cmb_bIsMixBatchNo;
        private ComboBox cmb_bIsMixPalce;
        private ComboBox cmb_bIsNeedSpecStore;
        private ComboBox cmb_bIsPackage;
        private ComboBox cmb_bIsQc;
        private ComboBox cmb_bIsRaiseLayer;
        private ComboBox cmb_bIsSameMatClassIn;
        private ComboBox cmb_bIsSubQtyForQC;
        private ComboBox cmb_cABC;
        private ComboBox cmb_cAreaId;
        private ComboBox cmb_cDoseType;
        private ComboBox cmb_cPalletSpecId;
        private ComboBox cmb_cTypeId1;
        private ComboBox cmb_cTypeId2;
        private ComboBox cmb_cUnit;
        private ComboBox cmb_cWHId;
        private ComboBox cmb_Dtl_cChildUnit;
        private ComboBox cmb_Dtl_cParentUnit;
        private ComboBox cmb_nMatClass;
        private ComboBox cmb_nPlaceMode;
        private ComboBox cmbQ_cTypeId1;
        private ComboBox cmbQ_cTypeId2;
        private ComboBox cmbQ_cWHId;
        private DataGridViewTextBoxColumn cMNo;
        private DataGridViewTextBoxColumn cName;
        private DataGridViewTextBoxColumn col_Dtl_cChildUnit;
        private DataGridViewTextBoxColumn col_dtl_cParentUnit;
        private DataGridViewTextBoxColumn col_Dtl_fRate;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cParentId;
        private DataGridViewTextBoxColumn cSpec;
        private DataGridViewTextBoxColumn cTypeId1;
        private DataGridViewTextBoxColumn cTypeId2;
        private DataGridViewTextBoxColumn cWHId;
        private DataGridView dataGridView_Main;
        private DataGridViewTextBoxColumn dCreateDate;
        private DataGridViewTextBoxColumn fSafeQtyDn;
        private DataGridViewTextBoxColumn fSafeQtyUp;
        private DataGridViewTextBoxColumn fVolume;
        private DataGridViewTextBoxColumn fWeight;
        private DataGridView grdUnit;
        private GroupBox groupBox1;
        private GroupBox grp_EditOther;
        private GroupBox grpEdit;
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
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label4;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label48;
        private Label label49;
        private Label label5;
        private Label label50;
        private Label label51;
        private Label label52;
        private Label label53;
        private Label label54;
        private Label label55;
        private Label label56;
        private Label label57;
        private Label label58;
        private Label label59;
        private Label label6;
        private Label label60;
        private Label label61;
        private Label label62;
        private Label label63;
        private Label label7;
        private Label label8;
        private Label label9;
        private ToolStripMenuItem mmi_BatchUpdateItemValue;
        private ToolStripMenuItem mmi_UpdateMatPYWB;
        private ToolStripMenuItem mmiPrintBCInfo;
        private ToolStripMenuItem mmiPrintBCOnly;
        private DataGridViewTextBoxColumn nKeepDay;
        private DataGridViewTextBoxColumn nPlaceMode;
        private object objChildUnit = new object();
        private object objParentUnit = new object();
        private object objRate = new object();
        private OperateType OptDtl = OperateType.optNone;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private Panel pnl_Dtl_Buttons;
        private ContextMenuStrip ppmPrint;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        private ToolStripProgressBar stbProg;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cMNo";
        private string strTbNameMain = "TPC_Material";
        private TabControl tbcMain;
        private TabPage tbpInfo;
        private TabPage tbpPackSpec;
        private TextBox textBox_cBorCode;
        private TextBox textBox_cCreator;
        private TextBox textBox_cLinkId;
        private TextBox textBox_cMNo;
        private TextBox textBox_cName;
        private TextBox textBox_cNameQ;
        private TextBox textBox_fDPSInQtyDn;
        private TextBox textBox_fDPSInQtyUp;
        private TextBox textBox_fQtyBox;
        private TextBox textBox_fSafeQtyDn;
        private TextBox textBox_fSafeQtyUp;
        private TextBox textBox_fWeight;
        private TextBox textBox_nAutoPromptDay;
        private TextBox textBox_nKeepDay;
        private TextBox textBox1_cSpec;
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_Edit;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Find;
        public ToolStripButton tlb_M_New;
        public ToolStripButton tlb_M_Print;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Save;
        public ToolStripButton tlb_M_Undo;
        private ToolStripMenuItem tlb_Print_BCInfo;
        private ToolStripMenuItem tlb_Print_BCOnly;
        private ToolStripSplitButton tlb_PrintBC;
        public ToolStrip tlbMain;
        private ToolStripButton tlbSaveSysRts;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private ToolTip toolTip;
        private TextBox txt_cCSId;
        private TextBox txt_cMatOther;
        private TextBox txt_cMatQCLevel;
        private TextBox txt_cMatStyle;
        private TextBox txt_cRemark;
        private TextBox txt_cSupplier;
        private TextBox txt_Dtl_fRate;
        private TextBox txt_fPackageQty;
        private TextBox txt_nTag;

        public FrmStockMaterInfo()
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
            sSql = "SELECT * FROM " + this.strTbNameMain + " " + SqlStrConditon;
            base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
            flag = base.DBDataSet != null;
            this.bindingSource_Main.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
            FDataGridView.DataSource = this.bindingSource_Main;
            if (this.bindingSource_Main.Count > 0)
            {
                try
                {
                    this.bDSIsOpenForMain = true;
                    DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                    this.DataRowViewToUI(current, this.panel_Edit);
                    this.DataRowViewToUI(current, this.grp_EditOther);
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
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if (!current.IsNew)
                    {
                        this.DataRowViewToUI(current, this.panel_Edit);
                        this.DataRowViewToUI(current, this.grp_EditOther);
                        this.tbcMain_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        this.bsGrid.DataSource = null;
                    }
                }
            }
        }

        private void bsGrid_PositionChanged(object sender, EventArgs e)
        {
            if (this.bUnitIsOpen)
            {
                this.ClearUIValues(this.grpEdit);
                DataRowView current = (DataRowView) this.bsGrid.Current;
                if (current != null)
                {
                    if ((this.bsGrid.Count > 0) && !current.IsNew)
                    {
                        this.DataRowViewToUI(current, this.grpEdit);
                    }
                    if (current.IsNew)
                    {
                        this.CtrlOptButtons(this.pnl_Dtl_Buttons, this.grpEdit, OperateType.optNew, (DataTable) this.bsGrid.DataSource);
                    }
                    else
                    {
                        this.CtrlOptButtons(this.pnl_Dtl_Buttons, this.grpEdit, OperateType.optNone, (DataTable) this.bsGrid.DataSource);
                    }
                }
            }
        }

        private void btn_Dtl_Delete_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无物料信息数据！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
                    {
                        MessageBox.Show("对不起，请先保存物料的基本信息数据！");
                    }
                    else if (this.bsGrid.Count == 0)
                    {
                        MessageBox.Show("对不起，无数据可删除！");
                    }
                    else
                    {
                        DataRowView view2 = (DataRowView) this.bsGrid.Current;
                        if ((this.OptDtl == OperateType.optNew) || (this.OptDtl == OperateType.optEdit))
                        {
                            MessageBox.Show("数据正在编辑中，不能删除，请先保存或取消操作！");
                        }
                        else if (view2 != null)
                        {
                            string sSql = " delete TPC_MATPACKINGUNIT where cMNo = '" + view2["cMNo"].ToString().Trim() + "' and cChildUnit = '" + view2["cChildUnit"].ToString().Trim() + "' and cParentUnit = '" + view2["cParentUnit"].ToString().Trim() + "' and fRate = " + view2["fRate"].ToString();
                            string sErr = "";
                            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "data", 0, 0, "", out sErr);
                            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                            {
                                MessageBox.Show(sErr);
                            }
                            else
                            {
                                MessageBox.Show("删除成功！");
                            }
                            this.OptDtl = OperateType.optNone;
                            string sMNo = "";
                            if (current["cMNo"] != null)
                            {
                                sMNo = current["cMNo"].ToString().Trim();
                            }
                            this.OpenPackUnit(sMNo);
                        }
                    }
                }
            }
        }

        private void btn_Dtl_Edit_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无物料信息数据！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
                    {
                        MessageBox.Show("对不起，请先保存物料的基本信息数据！");
                    }
                    else if (this.bsGrid.Count == 0)
                    {
                        MessageBox.Show("对不起，无数据可修改！");
                    }
                    else
                    {
                        DataRowView view2 = (DataRowView) this.bsGrid.Current;
                        this.objParentUnit = view2["cParentUnit"];
                        this.objRate = view2["fRate"];
                        this.objChildUnit = view2["cChildUnit"];
                        view2.BeginEdit();
                        this.OptDtl = OperateType.optEdit;
                        if (!this.cmb_Dtl_cParentUnit.Enabled)
                        {
                            this.cmb_Dtl_cParentUnit.Enabled = true;
                        }
                        this.cmb_Dtl_cParentUnit.Focus();
                        this.cmb_Dtl_cChildUnit.Enabled = false;
                        this.CtrlOptButtons(this.pnl_Dtl_Buttons, this.grpEdit, OperateType.optEdit, (DataTable) this.bsGrid.DataSource);
                    }
                }
            }
        }

        private void btn_Dtl_New_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无物料信息数据！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
                    {
                        MessageBox.Show("对不起，请先保存物料的基本信息数据！");
                    }
                    else
                    {
                        string sMNo = "";
                        if (current["cMNo"] != null)
                        {
                            sMNo = current["cMNo"].ToString().Trim();
                        }
                        if (this.bsGrid.DataSource == null)
                        {
                            this.OpenPackUnit(sMNo);
                        }
                        if (sMNo.Trim() == "")
                        {
                            MessageBox.Show("对不起，物料号位空！");
                        }
                        else
                        {
                            DataRowView drv = (DataRowView) this.bsGrid.AddNew();
                            drv["cMNo"] = sMNo;
                            drv["cChildUnit"] = current["cUnit"].ToString();
                            drv["fRate"] = 1;
                            drv["nLevel"] = 1;
                            this.DataRowViewToUI(drv, this.grpEdit);
                            this.cmb_Dtl_cChildUnit.Enabled = false;
                            this.cmb_Dtl_cParentUnit.Enabled = true;
                            this.cmb_Dtl_cParentUnit.Focus();
                            this.OptDtl = OperateType.optNew;
                            this.CtrlOptButtons(this.pnl_Dtl_Buttons, this.grpEdit, OperateType.optNew, (DataTable) this.bsGrid.DataSource);
                        }
                    }
                }
            }
        }

        private void btn_Dtl_Save_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无物料信息数据！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
                    {
                        MessageBox.Show("对不起，请先保存物料的基本信息数据！");
                    }
                    else if ((this.OptDtl != OperateType.optNew) && (this.OptDtl != OperateType.optEdit))
                    {
                        MessageBox.Show("没有处于编辑状态，不能保存！");
                    }
                    else
                    {
                        DataRowView drv = (DataRowView) this.bsGrid.Current;
                        if (drv != null)
                        {
                            string sSql = "";
                            this.UIToDataRowView(drv, this.grpEdit);
                            string sErr = "";
                            object objValue = null;
                            sSql = "select count(*) nCount from TPC_MATPACKINGUNIT where cMNo='" + drv["cMNo"].ToString().Trim() + "' and cChildUnit='" + drv["cChildUnit"].ToString().Trim() + "' and cParentUnit = '" + drv["cParentUnit"].ToString().Trim() + "' and fRate=" + drv["fRate"].ToString().Trim();
                            if (PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "nCount", out objValue, out sErr))
                            {
                                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                                {
                                    MessageBox.Show(sErr);
                                    return;
                                }
                                if ((objValue != null) && ((objValue.ToString() != "") && (objValue.ToString() != "0")))
                                {
                                    MessageBox.Show("对不起，已经存在此数据，保存失败！");
                                    return;
                                }
                            }
                            if (this.OptDtl == OperateType.optNew)
                            {
                                sSql = DBSQLCommandInfo.GetSQLByDataRow(drv, "TPC_MATPACKINGUNIT", "cMNo,cChildUnit,cParentUnit,fRate", "", true);
                            }
                            else
                            {
                                sSql = "update TPC_MATPACKINGUNIT set cMNo='" + drv["cMNo"].ToString().Trim() + "',cChildUnit='" + drv["cChildUnit"].ToString().Trim() + "',cParentUnit = '" + drv["cParentUnit"].ToString().Trim() + "',fRate=" + drv["fRate"].ToString().Trim() + " where cMNo='" + drv["cMNo"].ToString().Trim() + "' and cChildUnit='" + this.objChildUnit.ToString().Trim() + "' and cParentUnit = '" + this.objParentUnit.ToString().Trim() + "' and fRate=" + this.objRate.ToString().Trim();
                            }
                            string sMNo = current["cMNo"].ToString().Trim();
                            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TPC_MATPACKINGUNIT", 0, 0, "", out sErr);
                            this.OpenPackUnit(sMNo);
                        }
                    }
                }
            }
        }

        private void btn_Dtl_Undo_Click(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无物料信息数据！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current != null)
                {
                    if ((this.OptMain == OperateType.optNew) || (this.OptMain == OperateType.optEdit))
                    {
                        MessageBox.Show("对不起，请先保存物料的基本信息数据！");
                    }
                    else if (this.bsGrid.Count == 0)
                    {
                        MessageBox.Show("无数据！");
                    }
                    else
                    {
                        DataRowView view2 = (DataRowView) this.bsGrid.Current;
                        if (view2.IsNew || view2.IsEdit)
                        {
                            view2.CancelEdit();
                            this.OptDtl = OperateType.optNone;
                        }
                        else
                        {
                            this.cmb_Dtl_cChildUnit.Enabled = false;
                            this.cmb_Dtl_cParentUnit.Enabled = false;
                            this.txt_Dtl_fRate.ReadOnly = true;
                            this.CtrlOptButtons(this.pnl_Dtl_Buttons, this.grpEdit, OperateType.optNone, (DataTable) this.bsGrid.DataSource);
                        }
                    }
                }
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.cmbQ_cTypeId1.SelectedIndex = -1;
            this.cmbQ_cTypeId2.SelectedIndex = -1;
            this.cmbQ_cWHId.SelectedIndex = -1;
            this.textBox_cNameQ.Text = "";
            this.cmbQ_cWHId.Focus();
        }

        private void btn_SelSupplier_Click(object sender, EventArgs e)
        {
            SunEast.App.UserManager.SelectCuSupplier(base.AppInformation, base.UserInformation, CSType.cstSupplier, -1, -1, "", new DoSelCuSupplierEvent(this.doSelCuSupplier));
        }

        private bool CheckCodeIsExists(string sTbName, string sFldCode, string sCode, out int nCount)
        {
            bool flag = false;
            string sErr = "";
            nCount = -1;
            DataSet dataBySql = PubDBCommFuns.GetDataBySql("Select count(*) nCount from " + sTbName + " where " + sFldCode + "='" + sCode.Trim() + "'", out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(" 检测 编码是否存在时报错：" + sErr);
                return flag;
            }
            DataTable table = null;
            table = dataBySql.Tables["result"];
            if (table.Rows[0]["returncode"].ToString() != "0")
            {
                MessageBox.Show(" 检测 编码是否存在时报错：" + table.Rows[0]["returndesc"].ToString());
                dataBySql.Clear();
                return flag;
            }
            table = dataBySql.Tables["data"];
            if (table != null)
            {
                nCount = int.Parse(table.Rows[0]["nCount"].ToString());
                flag = true;
            }
            if (dataBySql != null)
            {
                dataBySql.Clear();
            }
            return flag;
        }

        private void cmb_nMatClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cmb_nMatClass.SelectedIndex <= -1) || (this.cmb_nMatClass.SelectedValue == null))
            {
                this.grp_EditOther.Visible = false;
            }
            else
            {
                string str = this.cmb_nMatClass.SelectedValue.ToString();
                if (str != null)
                {
                    if (!(str == "0"))
                    {
                        if (str == "1")
                        {
                            this.grp_EditOther.Visible = true;
                        }
                        else if (str == "2")
                        {
                            this.grp_EditOther.Visible = false;
                        }
                        else if (str == "3")
                        {
                            this.grp_EditOther.Visible = false;
                        }
                    }
                    else
                    {
                        this.grp_EditOther.Visible = false;
                    }
                }
            }
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

        public void doBtchUpdateData(string sFieldName, string sFieldValue, string sDataType)
        {
            bool flag = false;
            int num = 0;
            string message = "";
            if ((this.bindingSource_Main.Count != 0) && (this.dataGridView_Main.SelectedRows.Count != 0))
            {
                this.stbProg.Maximum = this.dataGridView_Main.SelectedRows.Count;
                this.stbProg.Minimum = 0;
                this.stbProg.Value = 0;
                this.stbProg.Visible = true;
                this.stbProg.ToolTipText = "正在批量修改数据...";
                foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                {
                    string sErr = "";
                    string str3 = row.Cells["cMNo"].Value.ToString();
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        string sSql = "update TPC_Material set " + sFieldName + "='" + sFieldValue + "'  where cMNo='" + str3 + "'";
                        if (sDataType.Trim().ToUpper().IndexOf("DATE") > 0)
                        {
                            flag = WareBaseMS.DBFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, sFieldName, out sErr);
                        }
                        else
                        {
                            flag = WareBaseMS.DBFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, "", out sErr);
                        }
                        if (flag)
                        {
                            num++;
                        }
                        else if (message.Length == 0)
                        {
                            message = sErr;
                        }
                    }
                    catch (Exception exception)
                    {
                        message = exception.Message;
                    }
                    this.stbProg.Value++;
                }
                this.stbProg.Visible = false;
            }
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
                if ((this.bindingSource_Main.Count > 0) && (this.dataGridView_Main.SelectedRows.Count > 0))
                {
                    string message = "";
                    this.stbProg.Maximum = this.dataGridView_Main.SelectedRows.Count;
                    this.stbProg.Minimum = 0;
                    this.stbProg.Value = 0;
                    this.stbProg.Visible = true;
                    int num2 = 0;
                    foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                    {
                        string sErr = "";
                        string str3 = row.Cells["cName"].Value.ToString();
                        string str4 = row.Cells["cMNo"].Value.ToString();
                        string sSql = "delete " + this.strTbNameMain + " where " + this.strKeyFld + "='" + str4 + "'";
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            if (WareBaseMS.DBFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, "", out sErr))
                            {
                                num2++;
                            }
                            else if (message.Length == 0)
                            {
                                message = sErr;
                            }
                        }
                        catch (Exception exception)
                        {
                            message = exception.Message;
                        }
                        this.stbProg.Value++;
                    }
                    this.stbProg.Visible = false;
                    if (message.Length > 0)
                    {
                        MessageBox.Show("删除物料数据时出现错误：" + message);
                    }
                    MessageBox.Show("成功删除了：" + num2.ToString() + "/" + this.stbProg.Maximum.ToString() + " 数据！");
                }
                this.OptMain = OperateType.optDelete;
                this.BandDataSet(" ", this.dataGridView_Main);
                this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.OptMain = OperateType.optNone;
                this.DisplayState(this.stbState, this.OptMain);
                this.CtrlControlReadOnly(this.panel_Edit, false);
            }
            return flag;
        }

        private bool DoEdit()
        {
            bool flag = false;
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("对不起，无数据可供修改！");
                return flag;
            }
            if (this.cmb_bIsBaseMedic.Text.Trim() == "")
            {
                this.cmb_bIsBaseMedic.SelectedIndex = 0;
            }
            if (this.cmb_bIsRaiseLayer.Text.Trim() == "")
            {
                this.cmb_bIsRaiseLayer.SelectedIndex = 0;
            }
            if (this.cmb_bIsCoolStoreMedic.Text.Trim() == "")
            {
                this.cmb_bIsCoolStoreMedic.SelectedIndex = 0;
            }
            if (this.cmb_bIsNeedSpecStore.Text.Trim() == "")
            {
                this.cmb_bIsNeedSpecStore.SelectedIndex = 0;
            }
            if (this.cmb_bIsHightProfit.Text.Trim() == "")
            {
                this.cmb_bIsHightProfit.SelectedIndex = 0;
            }
            current.BeginEdit();
            this.OptMain = OperateType.optEdit;
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cMNo.ReadOnly = true;
            this.cmb_bIsFromErp.Enabled = false;
            this.textBox_cCreator.ReadOnly = true;
            this.cmb_nMatClass.Focus();
            return flag;
        }

        public bool DoNew()
        {
            this.OptMain = OperateType.optNew;
            DataRowView drv = (DataRowView) this.bindingSource_Main.AddNew();
            drv["cMNo"] = "";
            drv["nMatClass"] = 0;
            if (this.cmbQ_cWHId.SelectedValue != null)
            {
                drv["cWHId"] = this.cmbQ_cWHId.SelectedValue.ToString().Trim();
            }
            drv["fWeight"] = 0;
            drv["fVolume"] = 0;
            drv["fSafeQtyDn"] = 0;
            drv["fSafeQtyUp"] = 0;
            drv["nKeepDay"] = 360;
            drv["nAutoPromptDay"] = 0;
            drv["bIsFromERP"] = 0;
            drv["dCreateDate"] = DateTime.Now;
            drv["cCreator"] = base.UserInformation.UserName;
            drv["fQtyBox"] = 0;
            drv["fDPSInQtyDn"] = 0;
            drv["fDPSInQtyUp"] = 0;
            drv["nPlaceMode"] = 0;
            drv["bIsMixPalce"] = 0;
            drv["bIsSubQtyForQC"] = 0;
            drv["bIsQC"] = 0;
            drv["bIsAutoBatchNo"] = 0;
            drv["nTag"] = 0;
            drv["bIsBaseMedic"] = 0;
            drv["bIsRaiseLayer"] = 0;
            drv["bIsCoolStoreMedic"] = 0;
            drv["bIsNeedSpecStore"] = 0;
            drv["bIsHightProfit"] = 0;
            drv["fLayerRate"] = 0;
            drv["fPriceIn"] = 0;
            drv["fPriceOut"] = 0;
            drv["fPriceOutBatch"] = 0;
            drv["cABC"] = "C";
            this.DataRowViewToUI(drv, this.panel_Edit);
            this.DataRowViewToUI(drv, this.grp_EditOther);
            this.cmb_cTypeId1.SelectedIndex = 0;
            this.cmb_cTypeId2.SelectedIndex = 0;
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.DisplayState(this.stbState, this.OptMain);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cMNo.ReadOnly = !this.bCodeIsManual;
            this.cmb_bIsFromErp.Enabled = false;
            this.textBox_cCreator.ReadOnly = true;
            if (this.bCodeIsManual)
            {
                this.textBox_cMNo.Focus();
            }
            else
            {
                this.cmb_nMatClass.Focus();
            }
            return true;
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
            bool flag = false;
            string sSql = "";
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current != null)
            {
                if ((this.OptMain != OperateType.optNew) && (this.OptMain != OperateType.optEdit))
                {
                    MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
                if (this.bCodeIsManual && (this.textBox_cMNo.Text.Trim() == ""))
                {
                    MessageBox.Show("对不起，物料号不能为空！");
                    this.textBox_cMNo.Focus();
                    return false;
                }
                if ((this.cmb_nMatClass.Text.Trim() == "") || (this.cmb_nMatClass.SelectedValue == null))
                {
                    MessageBox.Show("对不起，物料类别不能为空！");
                    this.cmb_nMatClass.Focus();
                    return false;
                }
                if (this.textBox_cName.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，物料名不能为空！");
                    this.textBox_cName.Focus();
                    return false;
                }
                if (this.cmb_cUnit.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，物料单位不能为空！");
                    this.cmb_cUnit.Focus();
                    return false;
                }
                if (this.cmb_cTypeId1.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，物料分类不能为空！");
                    this.cmb_cTypeId1.Focus();
                    return false;
                }
                if (this.cmb_cTypeId1.SelectedValue == null)
                {
                    MessageBox.Show("对不起，物料分类不能为空！");
                    this.cmb_cTypeId1.Focus();
                    return false;
                }
                if (this.cmb_cTypeId2.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，会计分类不能为空！");
                    this.cmb_cTypeId2.Focus();
                    return false;
                }
                if (this.cmb_cTypeId2.SelectedValue == null)
                {
                    MessageBox.Show("对不起，会计分类不能为空！");
                    this.cmb_cTypeId2.Focus();
                    return false;
                }
                if (this.grp_EditOther.Visible)
                {
                    if (this.cmb_cDoseType.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，剂型不能为空！");
                        this.cmb_cDoseType.Focus();
                        return false;
                    }
                    if (this.cmb_bIsBaseMedic.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，是否基本药物不能为空！");
                        this.cmb_bIsBaseMedic.Focus();
                        return false;
                    }
                    if (this.cmb_bIsCoolStoreMedic.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，是否冷链品不能为空！");
                        this.cmb_bIsCoolStoreMedic.Focus();
                        return false;
                    }
                    if (this.cmb_bIsHightProfit.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，是否高利润不能为空！");
                        this.cmb_bIsHightProfit.Focus();
                        return false;
                    }
                    if (this.cmb_bIsNeedSpecStore.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，是否特殊存储不能为空！");
                        this.cmb_bIsNeedSpecStore.Focus();
                        return false;
                    }
                    if (this.cmb_bIsRaiseLayer.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，是否提层不能为空！");
                        this.cmb_bIsRaiseLayer.Focus();
                        return false;
                    }
                }
                if (this.textBox_cBorCode.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，条码不能为空！");
                    this.textBox_cBorCode.Focus();
                    return false;
                }
                if (this.txt_fPackageQty.Text.Trim() == "")
                {
                    this.txt_fPackageQty.Text = "0";
                }
                this.UIToDataRowView(current, this.panel_Edit);
                this.UIToDataRowView(current, this.grp_EditOther);
                string sText = current["cName"].ToString().Trim();
                string wBPY = TextPYWB.GetWBPY(sText, PYWBType.pwtPYFirst);
                current["cPYJM"] = wBPY;
                wBPY = TextPYWB.GetWBPY(sText, PYWBType.pwtWBFirst);
                current["cWBJM"] = wBPY;
                if (current["cSpec"].ToString().Trim() == "")
                {
                    MessageBox.Show("对不起，物料规格不能为空！");
                    this.textBox1_cSpec.Focus();
                    return false;
                }
                if (current["cBorCode"].ToString().Trim() == "")
                {
                    current["cBorCode"] = current["cMNo"];
                }
                if (this.OptMain != OperateType.optNew)
                {
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, false);
                    goto Label_0712;
                }
                if (!this.bCodeIsManual)
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 0x19, current["cTypeId1"].ToString());
                    goto Label_06E5;
                }
                sText = current[this.strKeyFld].ToString().Trim();
                if (sText != "")
                {
                    int nCount = 0;
                    this.CheckCodeIsExists(this.strTbNameMain, this.strKeyFld, sText, out nCount);
                    if (nCount < 0)
                    {
                        return false;
                    }
                    if (nCount > 0)
                    {
                        MessageBox.Show("对不起，该编码：" + sText + " 已经存在于表：" + this.strTbNameMain + " 中！");
                        this.textBox_cMNo.SelectAll();
                        this.textBox_cMNo.Focus();
                        return false;
                    }
                    goto Label_06E5;
                }
                MessageBox.Show("对不起，编码不能为空！");
                this.textBox_cMNo.SelectAll();
                this.textBox_cMNo.Focus();
            }
            return false;
        Label_06E5:
            sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, true);
        Label_0712:
            if (current.IsEdit)
            {
                current.EndEdit();
            }
            string fieldsForDate = DBSQLCommandInfo.GetFieldsForDate(current);
            if (PubDBCommFuns.GetDataBySql(sSql, fieldsForDate, out sErr).Tables[0].Rows[0][0].ToString() == "0")
            {
                this.OptMain = OperateType.optSave;
                this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.CtrlControlReadOnly(this.panel_Edit, false);
                MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.OptMain = OperateType.optNone;
                this.DoRefresh();
                return flag;
            }
            MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
        }

        private void doSelCuSupplier(string sCSId, string sCSNameJ, string sCSNameQ, CSType csType, string sTel, string sFax, string sAddress, string sRemark, string cType, int nIsInner, int nIsFactory, string sIsInner, string sIsFactory, int bUsed, string sUsed)
        {
            this.txt_cSupplier.Text = sCSNameJ.Trim();
            this.txt_cCSId.Text = sCSId.Trim();
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
                if (drv == null)
                {
                    return false;
                }
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
            this.grdUnit.AutoGenerateColumns = false;
            this.ReadSysPar();
            this.LoadStockList("");
            this.LoadCommboxItemByValue();
            this.LoadMaterialType();
            this.LoadDoseType();
            this.LoadUnit(-1);
            this.LoadCombWAreaList("", this.cmb_cAreaId);
            this.LoadCombPalletSpec(this.cmb_cPalletSpecId);
            this.DoRefresh();
            this.mmi_UpdateMatPYWB.Visible = base.UserInformation.UType == UserType.utSupervisor;
        }

        private string GetCondition()
        {
            object selectedValue;
            StringBuilder builder = new StringBuilder(" where 1=1 ");
            if (this.cmbQ_cTypeId1.Text.Trim() != "")
            {
                selectedValue = this.cmbQ_cTypeId1.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and isnull(cTypeId1,' ')='" + selectedValue.ToString().Trim() + "'");
                }
            }
            if (this.cmbQ_cTypeId2.Text.Trim() != "")
            {
                selectedValue = this.cmbQ_cTypeId2.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and isnull(cTypeId2,' ')='" + selectedValue.ToString().Trim() + "'");
                }
            }
            if (this.cmbQ_cWHId.Text.Trim() != "")
            {
                selectedValue = this.cmbQ_cWHId.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and isnull(cWHId,' ')='" + selectedValue.ToString().Trim() + "'");
                }
            }
            string str = this.textBox_cNameQ.Text.Trim();
            if (str != "")
            {
                builder.Append(" and (( cMNo like '%" + str + "%') or ( isnull(cName,' ') like '%" + str + "%') or  ( isnull(cBorCode,' ') like '%" + str + "%') or ( isnull(cPYJM,' ') like '%" + str + "%') or ( isnull(cWBJM,' ') like '%" + str + "%'))");
            }
            return builder.ToString();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockMaterInfo));
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
            this.tlb_PrintBC = new ToolStripSplitButton();
            this.tlb_Print_BCInfo = new ToolStripMenuItem();
            this.tlb_Print_BCOnly = new ToolStripMenuItem();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.panel1 = new Panel();
            this.dataGridView_Main = new DataGridView();
            this.cMNo = new DataGridViewTextBoxColumn();
            this.fVolume = new DataGridViewTextBoxColumn();
            this.cName = new DataGridViewTextBoxColumn();
            this.cSpec = new DataGridViewTextBoxColumn();
            this.cParentId = new DataGridViewTextBoxColumn();
            this.cWHId = new DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.nPlaceMode = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.cTypeId1 = new DataGridViewTextBoxColumn();
            this.cTypeId2 = new DataGridViewTextBoxColumn();
            this.fWeight = new DataGridViewTextBoxColumn();
            this.fSafeQtyDn = new DataGridViewTextBoxColumn();
            this.fSafeQtyUp = new DataGridViewTextBoxColumn();
            this.nKeepDay = new DataGridViewTextBoxColumn();
            this.bIsFromERP = new DataGridViewTextBoxColumn();
            this.dCreateDate = new DataGridViewTextBoxColumn();
            this.cCreator = new DataGridViewTextBoxColumn();
            this.ppmPrint = new ContextMenuStrip(this.components);
            this.mmiPrintBCInfo = new ToolStripMenuItem();
            this.mmiPrintBCOnly = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.mmi_UpdateMatPYWB = new ToolStripMenuItem();
            this.mmi_BatchUpdateItemValue = new ToolStripMenuItem();
            this.groupBox1 = new GroupBox();
            this.cmbQ_cWHId = new ComboBox();
            this.label26 = new Label();
            this.btn_Reset = new Button();
            this.btn_Qry = new Button();
            this.cmbQ_cTypeId2 = new ComboBox();
            this.label25 = new Label();
            this.cmbQ_cTypeId1 = new ComboBox();
            this.label7 = new Label();
            this.textBox_cNameQ = new TextBox();
            this.label8 = new Label();
            this.bindingSource_Main = new BindingSource(this.components);
            this.toolTip = new ToolTip(this.components);
            this.txt_Dtl_fRate = new TextBox();
            this.tbcMain = new TabControl();
            this.tbpInfo = new TabPage();
            this.panel_Edit = new Panel();
            this.cmb_bIsPackage = new ComboBox();
            this.label63 = new Label();
            this.cmb_bIsSameMatClassIn = new ComboBox();
            this.label62 = new Label();
            this.cmb_bIsMixBatchNo = new ComboBox();
            this.label61 = new Label();
            this.cmb_cAreaId = new ComboBox();
            this.label58 = new Label();
            this.cmb_cPalletSpecId = new ComboBox();
            this.label59 = new Label();
            this.txt_fPackageQty = new TextBox();
            this.label60 = new Label();
            this.label57 = new Label();
            this.label39 = new Label();
            this.grp_EditOther = new GroupBox();
            this.label54 = new Label();
            this.label53 = new Label();
            this.cmb_bIsHightProfit = new ComboBox();
            this.label52 = new Label();
            this.cmb_bIsNeedSpecStore = new ComboBox();
            this.label51 = new Label();
            this.cmb_bIsCoolStoreMedic = new ComboBox();
            this.label50 = new Label();
            this.cmb_bIsRaiseLayer = new ComboBox();
            this.label49 = new Label();
            this.cmb_cDoseType = new ComboBox();
            this.label47 = new Label();
            this.cmb_bIsBaseMedic = new ComboBox();
            this.label46 = new Label();
            this.label55 = new Label();
            this.cmb_nMatClass = new ComboBox();
            this.label56 = new Label();
            this.label48 = new Label();
            this.cmb_cABC = new ComboBox();
            this.label45 = new Label();
            this.txt_cRemark = new TextBox();
            this.txt_cCSId = new TextBox();
            this.txt_nTag = new TextBox();
            this.label44 = new Label();
            this.cmb_bIsAutoBatchNo = new ComboBox();
            this.label11 = new Label();
            this.btn_SelSupplier = new Button();
            this.label43 = new Label();
            this.txt_cMatOther = new TextBox();
            this.label42 = new Label();
            this.txt_cSupplier = new TextBox();
            this.txt_cMatQCLevel = new TextBox();
            this.label41 = new Label();
            this.txt_cMatStyle = new TextBox();
            this.label40 = new Label();
            this.label38 = new Label();
            this.textBox_nAutoPromptDay = new TextBox();
            this.label37 = new Label();
            this.cmb_bIsSubQtyForQC = new ComboBox();
            this.label36 = new Label();
            this.label31 = new Label();
            this.label32 = new Label();
            this.label29 = new Label();
            this.label28 = new Label();
            this.label27 = new Label();
            this.label30 = new Label();
            this.cmb_nPlaceMode = new ComboBox();
            this.cmb_bIsMixPalce = new ComboBox();
            this.cmb_bIsQc = new ComboBox();
            this.cmb_bIsFromErp = new ComboBox();
            this.cmb_cTypeId2 = new ComboBox();
            this.cmb_cTypeId1 = new ComboBox();
            this.cmb_cUnit = new ComboBox();
            this.cmb_cWHId = new ComboBox();
            this.textBox_fDPSInQtyUp = new TextBox();
            this.textBox_cLinkId = new TextBox();
            this.textBox_cBorCode = new TextBox();
            this.textBox_cCreator = new TextBox();
            this.textBox1_cSpec = new TextBox();
            this.textBox_fSafeQtyDn = new TextBox();
            this.textBox_fSafeQtyUp = new TextBox();
            this.textBox_nKeepDay = new TextBox();
            this.textBox_fQtyBox = new TextBox();
            this.textBox_fDPSInQtyDn = new TextBox();
            this.textBox_fWeight = new TextBox();
            this.label24 = new Label();
            this.label23 = new Label();
            this.label22 = new Label();
            this.label21 = new Label();
            this.label20 = new Label();
            this.label19 = new Label();
            this.label18 = new Label();
            this.label17 = new Label();
            this.label16 = new Label();
            this.label15 = new Label();
            this.label14 = new Label();
            this.label13 = new Label();
            this.label12 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cMNo = new TextBox();
            this.textBox_cName = new TextBox();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.stbProg = new ToolStripProgressBar();
            this.tbpPackSpec = new TabPage();
            this.pnl_Dtl_Buttons = new Panel();
            this.btn_Dtl_Save = new Button();
            this.btn_Dtl_Undo = new Button();
            this.btn_Dtl_Delete = new Button();
            this.btn_Dtl_Edit = new Button();
            this.btn_Dtl_New = new Button();
            this.grpEdit = new GroupBox();
            this.label35 = new Label();
            this.label34 = new Label();
            this.label33 = new Label();
            this.cmb_Dtl_cParentUnit = new ComboBox();
            this.cmb_Dtl_cChildUnit = new ComboBox();
            this.grdUnit = new DataGridView();
            this.col_dtl_cParentUnit = new DataGridViewTextBoxColumn();
            this.col_Dtl_cChildUnit = new DataGridViewTextBoxColumn();
            this.col_Dtl_fRate = new DataGridViewTextBoxColumn();
            this.bsGrid = new BindingSource(this.components);
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.ppmPrint.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            this.tbcMain.SuspendLayout();
            this.tbpInfo.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.grp_EditOther.SuspendLayout();
            this.stbMain.SuspendLayout();
            this.tbpPackSpec.SuspendLayout();
            this.pnl_Dtl_Buttons.SuspendLayout();
            this.grpEdit.SuspendLayout();
            ((ISupportInitialize) this.grdUnit).BeginInit();
            ((ISupportInitialize) this.bsGrid).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.tlb_PrintBC, this.toolStripSeparator6, 
                this.toolStripSeparator7, this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x410, 0x19);
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
            this.tlb_M_Print.Click += new EventHandler(this.tlb_M_Print_Click);
            this.tlb_PrintBC.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_PrintBC.DropDownItems.AddRange(new ToolStripItem[] { this.tlb_Print_BCInfo, this.tlb_Print_BCOnly });
            this.tlb_PrintBC.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_PrintBC.ForeColor = SystemColors.ActiveCaption;
            this.tlb_PrintBC.Image = (Image) manager.GetObject("tlb_PrintBC.Image");
            this.tlb_PrintBC.ImageTransparentColor = Color.Magenta;
            this.tlb_PrintBC.Name = "tlb_PrintBC";
            this.tlb_PrintBC.Size = new Size(0x49, 0x16);
            this.tlb_PrintBC.Text = "打印条码";
            this.tlb_Print_BCInfo.Name = "tlb_Print_BCInfo";
            this.tlb_Print_BCInfo.Size = new Size(0xaf, 0x16);
            this.tlb_Print_BCInfo.Text = "打印条码及信息";
            this.tlb_Print_BCInfo.Click += new EventHandler(this.mmiPrintBCInfo_Click);
            this.tlb_Print_BCOnly.Name = "tlb_Print_BCOnly";
            this.tlb_Print_BCOnly.Size = new Size(0xaf, 0x16);
            this.tlb_Print_BCOnly.Text = "打印条码(仅条码)";
            this.tlb_Print_BCOnly.Visible = false;
            this.tlb_Print_BCOnly.Click += new EventHandler(this.mmiPrintBCOnly_Click);
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
            this.panel1.Size = new Size(0x156, 0x257);
            this.panel1.TabIndex = 0x10;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { 
                this.cMNo, this.fVolume, this.cName, this.cSpec, this.cParentId, this.cWHId, this.Column1, this.Column2, this.Column3, this.nPlaceMode, this.Column5, this.Column6, this.Column7, this.Column8, this.cTypeId1, this.cTypeId2, 
                this.fWeight, this.fSafeQtyDn, this.fSafeQtyUp, this.nKeepDay, this.bIsFromERP, this.dCreateDate, this.cCreator
             });
            this.dataGridView_Main.ContextMenuStrip = this.ppmPrint;
            this.dataGridView_Main.Dock = DockStyle.Fill;
            this.dataGridView_Main.Location = new Point(0, 0x53);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Main.Size = new Size(0x156, 0x204);
            this.dataGridView_Main.TabIndex = 11;
            this.dataGridView_Main.Tag = "8";
            this.cMNo.DataPropertyName = "CMNo";
            this.cMNo.HeaderText = "物料编码";
            this.cMNo.Name = "cMNo";
            this.cMNo.ReadOnly = true;
            this.fVolume.HeaderText = "单位体积";
            this.fVolume.Name = "fVolume";
            this.fVolume.ReadOnly = true;
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "物料名称";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cSpec.DataPropertyName = "cSpec";
            this.cSpec.HeaderText = "物料规格";
            this.cSpec.Name = "cSpec";
            this.cSpec.ReadOnly = true;
            this.cParentId.DataPropertyName = "cUnit";
            this.cParentId.HeaderText = "物料单位";
            this.cParentId.Name = "cParentId";
            this.cParentId.ReadOnly = true;
            this.cWHId.DataPropertyName = "cWHId";
            this.cWHId.HeaderText = "仓库代码";
            this.cWHId.Name = "cWHId";
            this.cWHId.ReadOnly = true;
            this.Column1.DataPropertyName = "fQtyBox";
            this.Column1.HeaderText = "单位数量";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.DataPropertyName = "fDPSInQtyDn";
            this.Column2.HeaderText = "补货下限";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.DataPropertyName = "fDPSInQtyUp";
            this.Column3.HeaderText = "补货上限";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.nPlaceMode.DataPropertyName = "nPlaceMode";
            this.nPlaceMode.HeaderText = "放置模式";
            this.nPlaceMode.Name = "nPlaceMode";
            this.nPlaceMode.ReadOnly = true;
            this.Column5.DataPropertyName = "cLinkId";
            this.Column5.HeaderText = "关联编码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column6.DataPropertyName = "bIsMixPalce";
            this.Column6.HeaderText = "允许混放";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column7.DataPropertyName = "cBorCode";
            this.Column7.HeaderText = "条形编码";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.DataPropertyName = "bIsQc";
            this.Column8.HeaderText = "是否Qc";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.cTypeId1.DataPropertyName = "cTypeId1";
            this.cTypeId1.HeaderText = "物料分类";
            this.cTypeId1.Name = "cTypeId1";
            this.cTypeId1.ReadOnly = true;
            this.cTypeId2.DataPropertyName = "cTypeId2";
            this.cTypeId2.HeaderText = "会计分类";
            this.cTypeId2.Name = "cTypeId2";
            this.cTypeId2.ReadOnly = true;
            this.fWeight.DataPropertyName = "fWeight";
            this.fWeight.HeaderText = "单位重量";
            this.fWeight.Name = "fWeight";
            this.fWeight.ReadOnly = true;
            this.fSafeQtyDn.DataPropertyName = "fSafeQtyDn";
            this.fSafeQtyDn.HeaderText = "最小库存";
            this.fSafeQtyDn.Name = "fSafeQtyDn";
            this.fSafeQtyDn.ReadOnly = true;
            this.fSafeQtyUp.DataPropertyName = "fSafeQtyUp";
            this.fSafeQtyUp.HeaderText = "最大库存";
            this.fSafeQtyUp.Name = "fSafeQtyUp";
            this.fSafeQtyUp.ReadOnly = true;
            this.nKeepDay.DataPropertyName = "nKeepDay";
            this.nKeepDay.HeaderText = "质保天数";
            this.nKeepDay.Name = "nKeepDay";
            this.nKeepDay.ReadOnly = true;
            this.bIsFromERP.DataPropertyName = "bIsFromERP";
            this.bIsFromERP.HeaderText = "来源ERP";
            this.bIsFromERP.Name = "bIsFromERP";
            this.bIsFromERP.ReadOnly = true;
            this.dCreateDate.DataPropertyName = "dCreateDate";
            this.dCreateDate.HeaderText = "产生日期";
            this.dCreateDate.Name = "dCreateDate";
            this.dCreateDate.ReadOnly = true;
            this.cCreator.DataPropertyName = "cCreator";
            this.cCreator.HeaderText = "操作人员";
            this.cCreator.Name = "cCreator";
            this.cCreator.ReadOnly = true;
            this.ppmPrint.Items.AddRange(new ToolStripItem[] { this.mmiPrintBCInfo, this.mmiPrintBCOnly, this.toolStripMenuItem1, this.mmi_UpdateMatPYWB, this.mmi_BatchUpdateItemValue });
            this.ppmPrint.Name = "ppmPrint";
            this.ppmPrint.Size = new Size(0xcb, 0x62);
            this.mmiPrintBCInfo.Name = "mmiPrintBCInfo";
            this.mmiPrintBCInfo.Size = new Size(0xca, 0x16);
            this.mmiPrintBCInfo.Text = "打印条码及信息";
            this.mmiPrintBCInfo.Click += new EventHandler(this.mmiPrintBCInfo_Click);
            this.mmiPrintBCOnly.Name = "mmiPrintBCOnly";
            this.mmiPrintBCOnly.Size = new Size(0xca, 0x16);
            this.mmiPrintBCOnly.Text = "打印条码(仅条码)";
            this.mmiPrintBCOnly.Visible = false;
            this.mmiPrintBCOnly.Click += new EventHandler(this.mmiPrintBCOnly_Click);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(0xc7, 6);
            this.mmi_UpdateMatPYWB.Name = "mmi_UpdateMatPYWB";
            this.mmi_UpdateMatPYWB.Size = new Size(0xca, 0x16);
            this.mmi_UpdateMatPYWB.Text = "更新物料简拼和五笔简码";
            this.mmi_UpdateMatPYWB.ToolTipText = "更新物料简拼和五笔简码";
            this.mmi_UpdateMatPYWB.Visible = false;
            this.mmi_UpdateMatPYWB.Click += new EventHandler(this.mmi_UpdateMatPYWB_Click);
            this.mmi_BatchUpdateItemValue.Name = "mmi_BatchUpdateItemValue";
            this.mmi_BatchUpdateItemValue.Size = new Size(0xca, 0x16);
            this.mmi_BatchUpdateItemValue.Text = "批量修改其他数据";
            this.mmi_BatchUpdateItemValue.Click += new EventHandler(this.mmi_BatchUpdateItemValue_Click);
            this.groupBox1.Controls.Add(this.cmbQ_cWHId);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.btn_Reset);
            this.groupBox1.Controls.Add(this.btn_Qry);
            this.groupBox1.Controls.Add(this.cmbQ_cTypeId2);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.cmbQ_cTypeId1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox_cNameQ);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x156, 0x53);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            this.cmbQ_cWHId.FormattingEnabled = true;
            this.cmbQ_cWHId.Location = new Point(0x38, 12);
            this.cmbQ_cWHId.Name = "cmbQ_cWHId";
            this.cmbQ_cWHId.Size = new Size(0x8d, 20);
            this.cmbQ_cWHId.TabIndex = 0x4d;
            this.cmbQ_cWHId.Tag = "101";
            this.cmbQ_cWHId.Text = "Bind SelectedValue";
            this.label26.AutoSize = true;
            this.label26.Location = new Point(2, 0x11);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x35, 12);
            this.label26.TabIndex = 0x4c;
            this.label26.Text = "默认仓库";
            this.btn_Reset.Location = new Point(0x12b, 0x39);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new Size(0x2a, 0x17);
            this.btn_Reset.TabIndex = 0;
            this.btn_Reset.Text = "重置";
            this.toolTip.SetToolTip(this.btn_Reset, "重新将条件清空");
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new EventHandler(this.btn_Reset_Click);
            this.btn_Qry.Location = new Point(0xfc, 0x39);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x30, 0x17);
            this.btn_Qry.TabIndex = 0x4a;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.cmbQ_cTypeId2.FormattingEnabled = true;
            this.cmbQ_cTypeId2.Location = new Point(0xfc, 10);
            this.cmbQ_cTypeId2.Name = "cmbQ_cTypeId2";
            this.cmbQ_cTypeId2.Size = new Size(0x58, 20);
            this.cmbQ_cTypeId2.TabIndex = 0x49;
            this.cmbQ_cTypeId2.Tag = "101";
            this.cmbQ_cTypeId2.Text = "Bind SelectedValue";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0xc7, 0x10);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x35, 12);
            this.label25.TabIndex = 0x48;
            this.label25.Text = "会计分类";
            this.cmbQ_cTypeId1.FormattingEnabled = true;
            this.cmbQ_cTypeId1.Location = new Point(0x38, 0x23);
            this.cmbQ_cTypeId1.Name = "cmbQ_cTypeId1";
            this.cmbQ_cTypeId1.Size = new Size(0x11c, 20);
            this.cmbQ_cTypeId1.TabIndex = 0x47;
            this.cmbQ_cTypeId1.Tag = "101";
            this.cmbQ_cTypeId1.Text = "Bind SelectedValue";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(1, 0x27);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 70;
            this.label7.Text = "物料分类";
            this.textBox_cNameQ.Location = new Point(0x38, 0x3a);
            this.textBox_cNameQ.Name = "textBox_cNameQ";
            this.textBox_cNameQ.Size = new Size(0xc3, 0x15);
            this.textBox_cNameQ.TabIndex = 0x44;
            this.textBox_cNameQ.Tag = "0";
            this.toolTip.SetToolTip(this.textBox_cNameQ, "可以按物料名、编码、名称的拼音简码或五笔简码、条形码进行查询");
            this.label8.AutoSize = true;
            this.label8.Location = new Point(1, 0x3a);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x45;
            this.label8.Text = "物料名称";
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.txt_Dtl_fRate.Location = new Point(0x1ab, 0x1c);
            this.txt_Dtl_fRate.Name = "txt_Dtl_fRate";
            this.txt_Dtl_fRate.Size = new Size(0x36, 0x15);
            this.txt_Dtl_fRate.TabIndex = 2;
            this.txt_Dtl_fRate.Tag = "0";
            this.txt_Dtl_fRate.Text = "1";
            this.toolTip.SetToolTip(this.txt_Dtl_fRate, "大单位转小单位的换算率");
            this.tbcMain.Controls.Add(this.tbpInfo);
            this.tbcMain.Controls.Add(this.tbpPackSpec);
            this.tbcMain.Dock = DockStyle.Fill;
            this.tbcMain.Location = new Point(0x156, 0x19);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new Size(0x2ba, 0x257);
            this.tbcMain.TabIndex = 0x12;
            this.tbcMain.SelectedIndexChanged += new EventHandler(this.tbcMain_SelectedIndexChanged);
            this.tbpInfo.Controls.Add(this.panel_Edit);
            this.tbpInfo.Location = new Point(4, 0x15);
            this.tbpInfo.Name = "tbpInfo";
            this.tbpInfo.Padding = new Padding(3);
            this.tbpInfo.Size = new Size(690, 0x23e);
            this.tbpInfo.TabIndex = 0;
            this.tbpInfo.Text = "基本信息";
            this.tbpInfo.UseVisualStyleBackColor = true;
            this.panel_Edit.BackColor = SystemColors.Control;
            this.panel_Edit.Controls.Add(this.cmb_bIsPackage);
            this.panel_Edit.Controls.Add(this.label63);
            this.panel_Edit.Controls.Add(this.cmb_bIsSameMatClassIn);
            this.panel_Edit.Controls.Add(this.label62);
            this.panel_Edit.Controls.Add(this.cmb_bIsMixBatchNo);
            this.panel_Edit.Controls.Add(this.label61);
            this.panel_Edit.Controls.Add(this.cmb_cAreaId);
            this.panel_Edit.Controls.Add(this.label58);
            this.panel_Edit.Controls.Add(this.cmb_cPalletSpecId);
            this.panel_Edit.Controls.Add(this.label59);
            this.panel_Edit.Controls.Add(this.txt_fPackageQty);
            this.panel_Edit.Controls.Add(this.label60);
            this.panel_Edit.Controls.Add(this.label57);
            this.panel_Edit.Controls.Add(this.label39);
            this.panel_Edit.Controls.Add(this.grp_EditOther);
            this.panel_Edit.Controls.Add(this.label55);
            this.panel_Edit.Controls.Add(this.cmb_nMatClass);
            this.panel_Edit.Controls.Add(this.label56);
            this.panel_Edit.Controls.Add(this.label48);
            this.panel_Edit.Controls.Add(this.cmb_cABC);
            this.panel_Edit.Controls.Add(this.label45);
            this.panel_Edit.Controls.Add(this.txt_cRemark);
            this.panel_Edit.Controls.Add(this.txt_cCSId);
            this.panel_Edit.Controls.Add(this.txt_nTag);
            this.panel_Edit.Controls.Add(this.label44);
            this.panel_Edit.Controls.Add(this.cmb_bIsAutoBatchNo);
            this.panel_Edit.Controls.Add(this.label11);
            this.panel_Edit.Controls.Add(this.btn_SelSupplier);
            this.panel_Edit.Controls.Add(this.label43);
            this.panel_Edit.Controls.Add(this.txt_cMatOther);
            this.panel_Edit.Controls.Add(this.label42);
            this.panel_Edit.Controls.Add(this.txt_cSupplier);
            this.panel_Edit.Controls.Add(this.txt_cMatQCLevel);
            this.panel_Edit.Controls.Add(this.label41);
            this.panel_Edit.Controls.Add(this.txt_cMatStyle);
            this.panel_Edit.Controls.Add(this.label40);
            this.panel_Edit.Controls.Add(this.label38);
            this.panel_Edit.Controls.Add(this.textBox_nAutoPromptDay);
            this.panel_Edit.Controls.Add(this.label37);
            this.panel_Edit.Controls.Add(this.cmb_bIsSubQtyForQC);
            this.panel_Edit.Controls.Add(this.label36);
            this.panel_Edit.Controls.Add(this.label31);
            this.panel_Edit.Controls.Add(this.label32);
            this.panel_Edit.Controls.Add(this.label29);
            this.panel_Edit.Controls.Add(this.label28);
            this.panel_Edit.Controls.Add(this.label27);
            this.panel_Edit.Controls.Add(this.label30);
            this.panel_Edit.Controls.Add(this.cmb_nPlaceMode);
            this.panel_Edit.Controls.Add(this.cmb_bIsMixPalce);
            this.panel_Edit.Controls.Add(this.cmb_bIsQc);
            this.panel_Edit.Controls.Add(this.cmb_bIsFromErp);
            this.panel_Edit.Controls.Add(this.cmb_cTypeId2);
            this.panel_Edit.Controls.Add(this.cmb_cTypeId1);
            this.panel_Edit.Controls.Add(this.cmb_cUnit);
            this.panel_Edit.Controls.Add(this.cmb_cWHId);
            this.panel_Edit.Controls.Add(this.textBox_fDPSInQtyUp);
            this.panel_Edit.Controls.Add(this.textBox_cLinkId);
            this.panel_Edit.Controls.Add(this.textBox_cBorCode);
            this.panel_Edit.Controls.Add(this.textBox_cCreator);
            this.panel_Edit.Controls.Add(this.textBox1_cSpec);
            this.panel_Edit.Controls.Add(this.textBox_fSafeQtyDn);
            this.panel_Edit.Controls.Add(this.textBox_fSafeQtyUp);
            this.panel_Edit.Controls.Add(this.textBox_nKeepDay);
            this.panel_Edit.Controls.Add(this.textBox_fQtyBox);
            this.panel_Edit.Controls.Add(this.textBox_fDPSInQtyDn);
            this.panel_Edit.Controls.Add(this.textBox_fWeight);
            this.panel_Edit.Controls.Add(this.label24);
            this.panel_Edit.Controls.Add(this.label23);
            this.panel_Edit.Controls.Add(this.label22);
            this.panel_Edit.Controls.Add(this.label21);
            this.panel_Edit.Controls.Add(this.label20);
            this.panel_Edit.Controls.Add(this.label19);
            this.panel_Edit.Controls.Add(this.label18);
            this.panel_Edit.Controls.Add(this.label17);
            this.panel_Edit.Controls.Add(this.label16);
            this.panel_Edit.Controls.Add(this.label15);
            this.panel_Edit.Controls.Add(this.label14);
            this.panel_Edit.Controls.Add(this.label13);
            this.panel_Edit.Controls.Add(this.label12);
            this.panel_Edit.Controls.Add(this.label10);
            this.panel_Edit.Controls.Add(this.label9);
            this.panel_Edit.Controls.Add(this.label6);
            this.panel_Edit.Controls.Add(this.label5);
            this.panel_Edit.Controls.Add(this.label4);
            this.panel_Edit.Controls.Add(this.label3);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cMNo);
            this.panel_Edit.Controls.Add(this.textBox_cName);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(3, 3);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x2ac, 0x238);
            this.panel_Edit.TabIndex = 0;
            this.cmb_bIsPackage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsPackage.FormattingEnabled = true;
            this.cmb_bIsPackage.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsPackage.Location = new Point(90, 0x1b1);
            this.cmb_bIsPackage.Name = "cmb_bIsPackage";
            this.cmb_bIsPackage.Size = new Size(0x75, 20);
            this.cmb_bIsPackage.TabIndex = 0xce;
            this.cmb_bIsPackage.Tag = "102";
            this.label63.AutoSize = true;
            this.label63.Location = new Point(15, 0x1b1);
            this.label63.Name = "label63";
            this.label63.Size = new Size(0x47, 12);
            this.label63.TabIndex = 0xcf;
            this.label63.Text = "是否箱/袋装";
            this.cmb_bIsSameMatClassIn.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsSameMatClassIn.FormattingEnabled = true;
            this.cmb_bIsSameMatClassIn.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsSameMatClassIn.Location = new Point(0x247, 0x199);
            this.cmb_bIsSameMatClassIn.Name = "cmb_bIsSameMatClassIn";
            this.cmb_bIsSameMatClassIn.Size = new Size(0x3e, 20);
            this.cmb_bIsSameMatClassIn.TabIndex = 0xcc;
            this.cmb_bIsSameMatClassIn.Tag = "102";
            this.label62.AutoSize = true;
            this.label62.Location = new Point(440, 0x199);
            this.label62.Name = "label62";
            this.label62.Size = new Size(0x89, 12);
            this.label62.TabIndex = 0xcd;
            this.label62.Text = "是否仅按同类型物料混放";
            this.cmb_bIsMixBatchNo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsMixBatchNo.FormattingEnabled = true;
            this.cmb_bIsMixBatchNo.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsMixBatchNo.Location = new Point(0x16b, 0x199);
            this.cmb_bIsMixBatchNo.Name = "cmb_bIsMixBatchNo";
            this.cmb_bIsMixBatchNo.Size = new Size(0x3e, 20);
            this.cmb_bIsMixBatchNo.TabIndex = 0xca;
            this.cmb_bIsMixBatchNo.Tag = "102";
            this.label61.AutoSize = true;
            this.label61.Location = new Point(0xe5, 0x199);
            this.label61.Name = "label61";
            this.label61.Size = new Size(0x89, 12);
            this.label61.TabIndex = 0xcb;
            this.label61.Text = "是否同物料不同批次混放";
            this.cmb_cAreaId.FormattingEnabled = true;
            this.cmb_cAreaId.Location = new Point(90, 0x1ca);
            this.cmb_cAreaId.Name = "cmb_cAreaId";
            this.cmb_cAreaId.Size = new Size(0x75, 20);
            this.cmb_cAreaId.TabIndex = 0xc7;
            this.cmb_cAreaId.Tag = "101";
            this.label58.AutoSize = true;
            this.label58.Location = new Point(0x10, 0x1ca);
            this.label58.Name = "label58";
            this.label58.Size = new Size(0x35, 12);
            this.label58.TabIndex = 200;
            this.label58.Text = "存放货区";
            this.cmb_cPalletSpecId.FormattingEnabled = true;
            this.cmb_cPalletSpecId.Location = new Point(0x207, 0x1b3);
            this.cmb_cPalletSpecId.Name = "cmb_cPalletSpecId";
            this.cmb_cPalletSpecId.Size = new Size(0x7c, 20);
            this.cmb_cPalletSpecId.TabIndex = 0xc5;
            this.cmb_cPalletSpecId.Tag = "101";
            this.label59.AutoSize = true;
            this.label59.Location = new Point(0x1ba, 0x1b3);
            this.label59.Name = "label59";
            this.label59.Size = new Size(0x4d, 12);
            this.label59.TabIndex = 0xc6;
            this.label59.Text = "存放托盘规格";
            this.txt_fPackageQty.Location = new Point(0x131, 0x1b3);
            this.txt_fPackageQty.Name = "txt_fPackageQty";
            this.txt_fPackageQty.Size = new Size(120, 0x15);
            this.txt_fPackageQty.TabIndex = 0xc3;
            this.txt_fPackageQty.Tag = "0";
            this.txt_fPackageQty.Text = "0";
            this.label60.AutoSize = true;
            this.label60.Location = new Point(230, 0x1b3);
            this.label60.Name = "label60";
            this.label60.Size = new Size(0x47, 12);
            this.label60.TabIndex = 0xc4;
            this.label60.Text = "每箱/袋数量";
            this.label57.AutoSize = true;
            this.label57.Location = new Point(0x19, 0x185);
            this.label57.Name = "label57";
            this.label57.Size = new Size(0x4d, 12);
            this.label57.TabIndex = 0xbf;
            this.label57.Text = "入库配盘策略";
            this.label39.BackColor = SystemColors.ActiveCaption;
            this.label39.Location = new Point(0x12, 0x18c);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x272, 1);
            this.label39.TabIndex = 190;
            this.grp_EditOther.Controls.Add(this.label54);
            this.grp_EditOther.Controls.Add(this.label53);
            this.grp_EditOther.Controls.Add(this.cmb_bIsHightProfit);
            this.grp_EditOther.Controls.Add(this.label52);
            this.grp_EditOther.Controls.Add(this.cmb_bIsNeedSpecStore);
            this.grp_EditOther.Controls.Add(this.label51);
            this.grp_EditOther.Controls.Add(this.cmb_bIsCoolStoreMedic);
            this.grp_EditOther.Controls.Add(this.label50);
            this.grp_EditOther.Controls.Add(this.cmb_bIsRaiseLayer);
            this.grp_EditOther.Controls.Add(this.label49);
            this.grp_EditOther.Controls.Add(this.cmb_cDoseType);
            this.grp_EditOther.Controls.Add(this.label47);
            this.grp_EditOther.Controls.Add(this.cmb_bIsBaseMedic);
            this.grp_EditOther.Controls.Add(this.label46);
            this.grp_EditOther.Location = new Point(5, 0x1db);
            this.grp_EditOther.Name = "grp_EditOther";
            this.grp_EditOther.Size = new Size(640, 0x3f);
            this.grp_EditOther.TabIndex = 0xbd;
            this.grp_EditOther.TabStop = false;
            this.label54.AutoSize = true;
            this.label54.ForeColor = SystemColors.ActiveCaption;
            this.label54.Location = new Point(0x152, 0x11);
            this.label54.Name = "label54";
            this.label54.Size = new Size(11, 12);
            this.label54.TabIndex = 0xd1;
            this.label54.Text = "*";
            this.label53.AutoSize = true;
            this.label53.ForeColor = SystemColors.ActiveCaption;
            this.label53.Location = new Point(0xc1, 15);
            this.label53.Name = "label53";
            this.label53.Size = new Size(11, 12);
            this.label53.TabIndex = 0xd0;
            this.label53.Text = "*";
            this.cmb_bIsHightProfit.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsHightProfit.FormattingEnabled = true;
            this.cmb_bIsHightProfit.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsHightProfit.Location = new Point(0x121, 40);
            this.cmb_bIsHightProfit.Name = "cmb_bIsHightProfit";
            this.cmb_bIsHightProfit.Size = new Size(0x2c, 20);
            this.cmb_bIsHightProfit.TabIndex = 0xce;
            this.cmb_bIsHightProfit.Tag = "102";
            this.label52.AutoSize = true;
            this.label52.Location = new Point(0xd4, 0x29);
            this.label52.Name = "label52";
            this.label52.Size = new Size(0x4d, 12);
            this.label52.TabIndex = 0xcf;
            this.label52.Text = "是否高利润品";
            this.cmb_bIsNeedSpecStore.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsNeedSpecStore.FormattingEnabled = true;
            this.cmb_bIsNeedSpecStore.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsNeedSpecStore.Location = new Point(0x90, 0x26);
            this.cmb_bIsNeedSpecStore.Name = "cmb_bIsNeedSpecStore";
            this.cmb_bIsNeedSpecStore.Size = new Size(0x2c, 20);
            this.cmb_bIsNeedSpecStore.TabIndex = 0xcc;
            this.cmb_bIsNeedSpecStore.Tag = "102";
            this.label51.AutoSize = true;
            this.label51.Location = new Point(0x25, 40);
            this.label51.Name = "label51";
            this.label51.Size = new Size(0x65, 12);
            this.label51.TabIndex = 0xcd;
            this.label51.Text = "是否需要特殊存储";
            this.cmb_bIsCoolStoreMedic.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsCoolStoreMedic.FormattingEnabled = true;
            this.cmb_bIsCoolStoreMedic.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsCoolStoreMedic.Location = new Point(560, 0x10);
            this.cmb_bIsCoolStoreMedic.Name = "cmb_bIsCoolStoreMedic";
            this.cmb_bIsCoolStoreMedic.Size = new Size(0x2c, 20);
            this.cmb_bIsCoolStoreMedic.TabIndex = 0xca;
            this.cmb_bIsCoolStoreMedic.Tag = "102";
            this.label50.AutoSize = true;
            this.label50.Location = new Point(0x1e3, 0x11);
            this.label50.Name = "label50";
            this.label50.Size = new Size(0x41, 12);
            this.label50.TabIndex = 0xcb;
            this.label50.Text = "是否冷链品";
            this.cmb_bIsRaiseLayer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsRaiseLayer.FormattingEnabled = true;
            this.cmb_bIsRaiseLayer.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsRaiseLayer.Location = new Point(0x1a1, 0x10);
            this.cmb_bIsRaiseLayer.Name = "cmb_bIsRaiseLayer";
            this.cmb_bIsRaiseLayer.Size = new Size(0x2c, 20);
            this.cmb_bIsRaiseLayer.TabIndex = 200;
            this.cmb_bIsRaiseLayer.Tag = "102";
            this.label49.AutoSize = true;
            this.label49.Location = new Point(0x166, 0x13);
            this.label49.Name = "label49";
            this.label49.Size = new Size(0x35, 12);
            this.label49.TabIndex = 0xc9;
            this.label49.Text = "是否提层";
            this.cmb_cDoseType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cDoseType.FormattingEnabled = true;
            this.cmb_cDoseType.Location = new Point(0x42, 13);
            this.cmb_cDoseType.Name = "cmb_cDoseType";
            this.cmb_cDoseType.Size = new Size(0x79, 20);
            this.cmb_cDoseType.TabIndex = 0xc7;
            this.cmb_cDoseType.Tag = "1";
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x26, 15);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x1d, 12);
            this.label47.TabIndex = 0xc6;
            this.label47.Text = "剂型";
            this.cmb_bIsBaseMedic.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsBaseMedic.FormattingEnabled = true;
            this.cmb_bIsBaseMedic.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsBaseMedic.Location = new Point(0x121, 13);
            this.cmb_bIsBaseMedic.Name = "cmb_bIsBaseMedic";
            this.cmb_bIsBaseMedic.Size = new Size(0x2c, 20);
            this.cmb_bIsBaseMedic.TabIndex = 0xc4;
            this.cmb_bIsBaseMedic.Tag = "102";
            this.label46.AutoSize = true;
            this.label46.Location = new Point(0xd4, 0x10);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x4d, 12);
            this.label46.TabIndex = 0xc5;
            this.label46.Text = "是否基本药物";
            this.label55.AutoSize = true;
            this.label55.ForeColor = SystemColors.ActiveCaption;
            this.label55.Location = new Point(650, 0x12);
            this.label55.Name = "label55";
            this.label55.Size = new Size(11, 12);
            this.label55.TabIndex = 0xbc;
            this.label55.Text = "*";
            this.cmb_nMatClass.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_nMatClass.FormattingEnabled = true;
            this.cmb_nMatClass.Location = new Point(0x1b4, 11);
            this.cmb_nMatClass.Name = "cmb_nMatClass";
            this.cmb_nMatClass.Size = new Size(0xcf, 20);
            this.cmb_nMatClass.TabIndex = 0;
            this.cmb_nMatClass.Tag = "101";
            this.cmb_nMatClass.SelectedIndexChanged += new EventHandler(this.cmb_nMatClass_SelectedIndexChanged);
            this.label56.AutoSize = true;
            this.label56.Location = new Point(0x166, 0x10);
            this.label56.Name = "label56";
            this.label56.Size = new Size(0x35, 12);
            this.label56.TabIndex = 0xbb;
            this.label56.Text = "物料类型";
            this.label48.AutoSize = true;
            this.label48.Location = new Point(0x1d9, 0xb3);
            this.label48.Name = "label48";
            this.label48.Size = new Size(0x2f, 12);
            this.label48.TabIndex = 0xb9;
            this.label48.Text = "ABC属性";
            this.cmb_cABC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cABC.FormattingEnabled = true;
            this.cmb_cABC.Items.AddRange(new object[] { "A", "B", "C" });
            this.cmb_cABC.Location = new Point(0x20b, 0xb0);
            this.cmb_cABC.Name = "cmb_cABC";
            this.cmb_cABC.Size = new Size(0x79, 20);
            this.cmb_cABC.TabIndex = 0xb8;
            this.cmb_cABC.Tag = "1";
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0x11, 0xcb);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x1d, 12);
            this.label45.TabIndex = 0xb6;
            this.label45.Text = "备注";
            this.txt_cRemark.Location = new Point(0x65, 200);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.Size = new Size(0x21e, 0x15);
            this.txt_cRemark.TabIndex = 0xb5;
            this.txt_cRemark.Tag = "0";
            this.txt_cCSId.Enabled = false;
            this.txt_cCSId.Location = new Point(0x65, 0x98);
            this.txt_cCSId.Name = "txt_cCSId";
            this.txt_cCSId.Size = new Size(150, 0x15);
            this.txt_cCSId.TabIndex = 180;
            this.txt_cCSId.Tag = "0";
            this.txt_nTag.Location = new Point(0x19f, 0x171);
            this.txt_nTag.Name = "txt_nTag";
            this.txt_nTag.Size = new Size(0x2d, 0x15);
            this.txt_nTag.TabIndex = 0x1b;
            this.txt_nTag.Tag = "0";
            this.txt_nTag.Text = "0";
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0x169, 0x171);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x35, 12);
            this.label44.TabIndex = 0xb3;
            this.label44.Text = "自增批次";
            this.cmb_bIsAutoBatchNo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsAutoBatchNo.FormattingEnabled = true;
            this.cmb_bIsAutoBatchNo.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsAutoBatchNo.Location = new Point(0x143, 0x171);
            this.cmb_bIsAutoBatchNo.Name = "cmb_bIsAutoBatchNo";
            this.cmb_bIsAutoBatchNo.Size = new Size(0x24, 20);
            this.cmb_bIsAutoBatchNo.TabIndex = 0x1a;
            this.cmb_bIsAutoBatchNo.Tag = "102";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xde, 0x171);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x65, 12);
            this.label11.TabIndex = 0xb1;
            this.label11.Text = "是否自动递增批号";
            this.btn_SelSupplier.Location = new Point(620, 0x97);
            this.btn_SelSupplier.Name = "btn_SelSupplier";
            this.btn_SelSupplier.Size = new Size(0x18, 0x17);
            this.btn_SelSupplier.TabIndex = 0xaf;
            this.btn_SelSupplier.Text = "…";
            this.btn_SelSupplier.UseVisualStyleBackColor = true;
            this.btn_SelSupplier.Click += new EventHandler(this.btn_SelSupplier_Click);
            this.label43.AutoSize = true;
            this.label43.Location = new Point(0x11, 0xb3);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x35, 12);
            this.label43.TabIndex = 0xae;
            this.label43.Text = "其他属性";
            this.txt_cMatOther.Location = new Point(0x65, 0xb0);
            this.txt_cMatOther.Name = "txt_cMatOther";
            this.txt_cMatOther.Size = new Size(0x174, 0x15);
            this.txt_cMatOther.TabIndex = 11;
            this.txt_cMatOther.Tag = "0";
            this.label42.AutoSize = true;
            this.label42.Location = new Point(0x11, 0x9c);
            this.label42.Name = "label42";
            this.label42.Size = new Size(0x53, 12);
            this.label42.TabIndex = 0xac;
            this.label42.Text = "供应商/生产商";
            this.txt_cSupplier.Enabled = false;
            this.txt_cSupplier.Location = new Point(0x101, 0x98);
            this.txt_cSupplier.Name = "txt_cSupplier";
            this.txt_cSupplier.Size = new Size(0x165, 0x15);
            this.txt_cSupplier.TabIndex = 10;
            this.txt_cSupplier.Tag = "0";
            this.txt_cMatQCLevel.Location = new Point(0x1b4, 0x80);
            this.txt_cMatQCLevel.Name = "txt_cMatQCLevel";
            this.txt_cMatQCLevel.Size = new Size(0xd0, 0x15);
            this.txt_cMatQCLevel.TabIndex = 9;
            this.txt_cMatQCLevel.Tag = "0";
            this.label41.AutoSize = true;
            this.label41.Location = new Point(0x166, 0x80);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0x35, 12);
            this.label41.TabIndex = 170;
            this.label41.Text = "质量等级";
            this.txt_cMatStyle.Location = new Point(0x65, 0x80);
            this.txt_cMatStyle.Name = "txt_cMatStyle";
            this.txt_cMatStyle.Size = new Size(0xd8, 0x15);
            this.txt_cMatStyle.TabIndex = 8;
            this.txt_cMatStyle.Tag = "0";
            this.label40.AutoSize = true;
            this.label40.Location = new Point(0x12, 0x80);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x2f, 12);
            this.label40.TabIndex = 0xa8;
            this.label40.Text = "款   式";
            this.label38.AutoSize = true;
            this.label38.Location = new Point(0x252, 0x15b);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x11, 12);
            this.label38.TabIndex = 0xa5;
            this.label38.Text = "天";
            this.textBox_nAutoPromptDay.Location = new Point(0x1f9, 0x159);
            this.textBox_nAutoPromptDay.Name = "textBox_nAutoPromptDay";
            this.textBox_nAutoPromptDay.Size = new Size(0x55, 0x15);
            this.textBox_nAutoPromptDay.TabIndex = 0x18;
            this.textBox_nAutoPromptDay.Tag = "0";
            this.textBox_nAutoPromptDay.Text = "10";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(0x184, 0x159);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x71, 12);
            this.label37.TabIndex = 0xa4;
            this.label37.Text = "有效期自动提醒天数";
            this.cmb_bIsSubQtyForQC.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsSubQtyForQC.FormattingEnabled = true;
            this.cmb_bIsSubQtyForQC.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsSubQtyForQC.Location = new Point(0x20f, 0x142);
            this.cmb_bIsSubQtyForQC.Name = "cmb_bIsSubQtyForQC";
            this.cmb_bIsSubQtyForQC.Size = new Size(0x52, 20);
            this.cmb_bIsSubQtyForQC.TabIndex = 0x16;
            this.cmb_bIsSubQtyForQC.Tag = "102";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(0x184, 0x142);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x89, 12);
            this.label36.TabIndex = 0xa2;
            this.label36.Text = "质检取样时是否扣减数量";
            this.label31.AutoSize = true;
            this.label31.ForeColor = SystemColors.ActiveCaption;
            this.label31.Location = new Point(0x141, 0x3d);
            this.label31.Name = "label31";
            this.label31.Size = new Size(11, 12);
            this.label31.TabIndex = 160;
            this.label31.Text = "*";
            this.label32.AutoSize = true;
            this.label32.ForeColor = SystemColors.ActiveCaption;
            this.label32.Location = new Point(0x141, 0x53);
            this.label32.Name = "label32";
            this.label32.Size = new Size(11, 12);
            this.label32.TabIndex = 0x9f;
            this.label32.Text = "*";
            this.label29.AutoSize = true;
            this.label29.ForeColor = SystemColors.ActiveCaption;
            this.label29.Location = new Point(650, 0x3e);
            this.label29.Name = "label29";
            this.label29.Size = new Size(11, 12);
            this.label29.TabIndex = 0x9e;
            this.label29.Text = "*";
            this.label28.AutoSize = true;
            this.label28.ForeColor = SystemColors.ActiveCaption;
            this.label28.Location = new Point(650, 0x55);
            this.label28.Name = "label28";
            this.label28.Size = new Size(11, 12);
            this.label28.TabIndex = 0x9d;
            this.label28.Text = "*";
            this.label27.AutoSize = true;
            this.label27.ForeColor = SystemColors.ActiveCaption;
            this.label27.Location = new Point(650, 0x29);
            this.label27.Name = "label27";
            this.label27.Size = new Size(11, 12);
            this.label27.TabIndex = 0x9c;
            this.label27.Text = "*";
            this.label30.AutoSize = true;
            this.label30.ForeColor = SystemColors.ActiveCaption;
            this.label30.Location = new Point(0xf5, 0x13);
            this.label30.Name = "label30";
            this.label30.Size = new Size(11, 12);
            this.label30.TabIndex = 0x9b;
            this.label30.Text = "*";
            this.cmb_nPlaceMode.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_nPlaceMode.FormattingEnabled = true;
            this.cmb_nPlaceMode.Items.AddRange(new object[] { "托盘", "周转箱" });
            this.cmb_nPlaceMode.Location = new Point(0xad, 0xe0);
            this.cmb_nPlaceMode.Name = "cmb_nPlaceMode";
            this.cmb_nPlaceMode.Size = new Size(0x97, 20);
            this.cmb_nPlaceMode.TabIndex = 12;
            this.cmb_nPlaceMode.Tag = "102";
            this.cmb_bIsMixPalce.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsMixPalce.FormattingEnabled = true;
            this.cmb_bIsMixPalce.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsMixPalce.Location = new Point(90, 0x199);
            this.cmb_bIsMixPalce.Name = "cmb_bIsMixPalce";
            this.cmb_bIsMixPalce.Size = new Size(0x75, 20);
            this.cmb_bIsMixPalce.TabIndex = 0x11;
            this.cmb_bIsMixPalce.Tag = "102";
            this.cmb_bIsQc.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsQc.FormattingEnabled = true;
            this.cmb_bIsQc.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsQc.Location = new Point(0xac, 320);
            this.cmb_bIsQc.Name = "cmb_bIsQc";
            this.cmb_bIsQc.Size = new Size(0x98, 20);
            this.cmb_bIsQc.TabIndex = 0x15;
            this.cmb_bIsQc.Tag = "102";
            this.cmb_bIsFromErp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_bIsFromErp.FormattingEnabled = true;
            this.cmb_bIsFromErp.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsFromErp.Location = new Point(0xac, 0x16e);
            this.cmb_bIsFromErp.Name = "cmb_bIsFromErp";
            this.cmb_bIsFromErp.Size = new Size(0x2c, 20);
            this.cmb_bIsFromErp.TabIndex = 0x19;
            this.cmb_bIsFromErp.Tag = "102";
            this.cmb_cTypeId2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cTypeId2.FormattingEnabled = true;
            this.cmb_cTypeId2.Location = new Point(0x65, 0x53);
            this.cmb_cTypeId2.Name = "cmb_cTypeId2";
            this.cmb_cTypeId2.Size = new Size(0xd8, 20);
            this.cmb_cTypeId2.TabIndex = 4;
            this.cmb_cTypeId2.Tag = "101";
            this.cmb_cTypeId1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cTypeId1.FormattingEnabled = true;
            this.cmb_cTypeId1.Location = new Point(0x1b4, 0x53);
            this.cmb_cTypeId1.Name = "cmb_cTypeId1";
            this.cmb_cTypeId1.Size = new Size(0xcf, 20);
            this.cmb_cTypeId1.TabIndex = 5;
            this.cmb_cTypeId1.Tag = "101";
            this.cmb_cUnit.FormattingEnabled = true;
            this.cmb_cUnit.Location = new Point(0x65, 0x3d);
            this.cmb_cUnit.Name = "cmb_cUnit";
            this.cmb_cUnit.Size = new Size(0xd9, 20);
            this.cmb_cUnit.TabIndex = 2;
            this.cmb_cUnit.Tag = "101";
            this.cmb_cUnit.Text = "Bind SelectedValue";
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0x65, 0x69);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(0xd8, 20);
            this.cmb_cWHId.TabIndex = 6;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.Text = "Bind SelectedValue";
            this.textBox_fDPSInQtyUp.Location = new Point(0x1c6, 0xf8);
            this.textBox_fDPSInQtyUp.Name = "textBox_fDPSInQtyUp";
            this.textBox_fDPSInQtyUp.Size = new Size(0x9b, 0x15);
            this.textBox_fDPSInQtyUp.TabIndex = 15;
            this.textBox_fDPSInQtyUp.Tag = "0";
            this.textBox_cLinkId.Location = new Point(0x1c6, 0xe0);
            this.textBox_cLinkId.Name = "textBox_cLinkId";
            this.textBox_cLinkId.Size = new Size(0x9b, 0x15);
            this.textBox_cLinkId.TabIndex = 13;
            this.textBox_cLinkId.Tag = "0";
            this.textBox_cBorCode.Location = new Point(0x1c6, 0x110);
            this.textBox_cBorCode.Name = "textBox_cBorCode";
            this.textBox_cBorCode.Size = new Size(0x9b, 0x15);
            this.textBox_cBorCode.TabIndex = 0x13;
            this.textBox_cBorCode.Tag = "0";
            this.textBox_cCreator.Location = new Point(520, 370);
            this.textBox_cCreator.Name = "textBox_cCreator";
            this.textBox_cCreator.Size = new Size(0x59, 0x15);
            this.textBox_cCreator.TabIndex = 0x1c;
            this.textBox_cCreator.Tag = "0";
            this.textBox1_cSpec.Location = new Point(0x1b4, 0x69);
            this.textBox1_cSpec.Name = "textBox1_cSpec";
            this.textBox1_cSpec.Size = new Size(0xd0, 0x15);
            this.textBox1_cSpec.TabIndex = 7;
            this.textBox1_cSpec.Tag = "0";
            this.textBox_fSafeQtyDn.Location = new Point(0xad, 0x128);
            this.textBox_fSafeQtyDn.Name = "textBox_fSafeQtyDn";
            this.textBox_fSafeQtyDn.Size = new Size(0x97, 0x15);
            this.textBox_fSafeQtyDn.TabIndex = 0x12;
            this.textBox_fSafeQtyDn.Tag = "0";
            this.textBox_fSafeQtyUp.Location = new Point(0x1c6, 0x128);
            this.textBox_fSafeQtyUp.Name = "textBox_fSafeQtyUp";
            this.textBox_fSafeQtyUp.Size = new Size(0x9b, 0x15);
            this.textBox_fSafeQtyUp.TabIndex = 20;
            this.textBox_fSafeQtyUp.Tag = "0";
            this.textBox_nKeepDay.Location = new Point(0xac, 0x157);
            this.textBox_nKeepDay.Name = "textBox_nKeepDay";
            this.textBox_nKeepDay.Size = new Size(0x99, 0x15);
            this.textBox_nKeepDay.TabIndex = 0x17;
            this.textBox_nKeepDay.Tag = "0";
            this.textBox_fQtyBox.Location = new Point(0x1b4, 0x3d);
            this.textBox_fQtyBox.Name = "textBox_fQtyBox";
            this.textBox_fQtyBox.Size = new Size(0xd0, 0x15);
            this.textBox_fQtyBox.TabIndex = 3;
            this.textBox_fQtyBox.Tag = "0";
            this.textBox_fQtyBox.Text = "0";
            this.textBox_fDPSInQtyDn.Location = new Point(0xad, 0xf8);
            this.textBox_fDPSInQtyDn.Name = "textBox_fDPSInQtyDn";
            this.textBox_fDPSInQtyDn.Size = new Size(0x97, 0x15);
            this.textBox_fDPSInQtyDn.TabIndex = 14;
            this.textBox_fDPSInQtyDn.Tag = "0";
            this.textBox_fWeight.Location = new Point(0xad, 0x110);
            this.textBox_fWeight.Name = "textBox_fWeight";
            this.textBox_fWeight.Size = new Size(0x97, 0x15);
            this.textBox_fWeight.TabIndex = 0x10;
            this.textBox_fWeight.Tag = "0";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(0x166, 0x3d);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x47, 12);
            this.label24.TabIndex = 0x9a;
            this.label24.Text = "满盘/箱数量";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x63, 0xf8);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x35, 12);
            this.label23.TabIndex = 0x99;
            this.label23.Text = "最小补货";
            this.label22.AutoSize = true;
            this.label22.Location = new Point(0x184, 0xf8);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x35, 12);
            this.label22.TabIndex = 0x98;
            this.label22.Text = "最大补货";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x63, 0xe0);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x35, 12);
            this.label21.TabIndex = 0x97;
            this.label21.Text = "存货模式";
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0x184, 0xe0);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x35, 12);
            this.label20.TabIndex = 150;
            this.label20.Text = "关联编码";
            this.label19.AutoSize = true;
            this.label19.Location = new Point(15, 0x199);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x35, 12);
            this.label19.TabIndex = 0x95;
            this.label19.Text = "是否混放";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x63, 320);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x29, 12);
            this.label18.TabIndex = 0x94;
            this.label18.Text = "是否QC";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x184, 0x110);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x35, 12);
            this.label17.TabIndex = 0x93;
            this.label17.Text = "条形编码";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0x166, 0x53);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x35, 12);
            this.label16.TabIndex = 0x92;
            this.label16.Text = "物料分类";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x63, 0x128);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x35, 12);
            this.label15.TabIndex = 0x91;
            this.label15.Text = "最小库存";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x184, 0x128);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x35, 12);
            this.label14.TabIndex = 0x90;
            this.label14.Text = "最大库存";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x63, 0x157);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x8f;
            this.label13.Text = "质保天数";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x63, 0x16e);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x47, 12);
            this.label12.TabIndex = 0x8e;
            this.label12.Text = "是否来源ERP";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x1d0, 0x173);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 140;
            this.label10.Text = "操作人员";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x12, 0x69);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 0x8b;
            this.label9.Text = "默认仓库";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x12, 0x53);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x88;
            this.label6.Text = "会计分类";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x63, 0x110);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x86;
            this.label5.Text = "单位重量";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x12, 0x3d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 0x84;
            this.label4.Text = "物料单位";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x166, 0x69);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 0x80;
            this.label3.Text = "规格型号";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x11, 0x29);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x7e;
            this.label2.Text = "物料名称";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x11, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x7c;
            this.label1.Text = "物料编码";
            this.textBox_cMNo.Location = new Point(0x65, 15);
            this.textBox_cMNo.Name = "textBox_cMNo";
            this.textBox_cMNo.ReadOnly = true;
            this.textBox_cMNo.Size = new Size(140, 0x15);
            this.textBox_cMNo.TabIndex = 0;
            this.textBox_cMNo.Tag = "0";
            this.textBox_cMNo.ReadOnlyChanged += new EventHandler(this.textBox_cMNo_ReadOnlyChanged);
            this.textBox_cMNo.Leave += new EventHandler(this.textBox_cMNo_Leave);
            this.textBox_cName.Location = new Point(0x65, 0x26);
            this.textBox_cName.Name = "textBox_cName";
            this.textBox_cName.Size = new Size(0x21f, 0x15);
            this.textBox_cName.TabIndex = 1;
            this.textBox_cName.Tag = "0";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime, this.stbProg });
            this.stbMain.Location = new Point(0, 0x222);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x2ac, 0x16);
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
            this.stbProg.Name = "stbProg";
            this.stbProg.Size = new Size(300, 0x10);
            this.stbProg.Step = 1;
            this.stbProg.Visible = false;
            this.tbpPackSpec.BackColor = SystemColors.Control;
            this.tbpPackSpec.Controls.Add(this.pnl_Dtl_Buttons);
            this.tbpPackSpec.Controls.Add(this.grpEdit);
            this.tbpPackSpec.Controls.Add(this.grdUnit);
            this.tbpPackSpec.Location = new Point(4, 0x15);
            this.tbpPackSpec.Name = "tbpPackSpec";
            this.tbpPackSpec.Padding = new Padding(3);
            this.tbpPackSpec.Size = new Size(690, 0x23e);
            this.tbpPackSpec.TabIndex = 1;
            this.tbpPackSpec.Text = "规格单位换算";
            this.pnl_Dtl_Buttons.Controls.Add(this.btn_Dtl_Save);
            this.pnl_Dtl_Buttons.Controls.Add(this.btn_Dtl_Undo);
            this.pnl_Dtl_Buttons.Controls.Add(this.btn_Dtl_Delete);
            this.pnl_Dtl_Buttons.Controls.Add(this.btn_Dtl_Edit);
            this.pnl_Dtl_Buttons.Controls.Add(this.btn_Dtl_New);
            this.pnl_Dtl_Buttons.Location = new Point(0x56, 0x133);
            this.pnl_Dtl_Buttons.Name = "pnl_Dtl_Buttons";
            this.pnl_Dtl_Buttons.Size = new Size(0x1e4, 0x2f);
            this.pnl_Dtl_Buttons.TabIndex = 7;
            this.btn_Dtl_Save.Location = new Point(0x18b, 12);
            this.btn_Dtl_Save.Name = "btn_Dtl_Save";
            this.btn_Dtl_Save.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_Save.TabIndex = 4;
            this.btn_Dtl_Save.Text = "保存";
            this.btn_Dtl_Save.UseVisualStyleBackColor = true;
            this.btn_Dtl_Save.Click += new EventHandler(this.btn_Dtl_Save_Click);
            this.btn_Dtl_Undo.Location = new Point(0x125, 12);
            this.btn_Dtl_Undo.Name = "btn_Dtl_Undo";
            this.btn_Dtl_Undo.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_Undo.TabIndex = 3;
            this.btn_Dtl_Undo.Text = "取消";
            this.btn_Dtl_Undo.UseVisualStyleBackColor = true;
            this.btn_Dtl_Undo.Click += new EventHandler(this.btn_Dtl_Undo_Click);
            this.btn_Dtl_Delete.Location = new Point(0xc9, 12);
            this.btn_Dtl_Delete.Name = "btn_Dtl_Delete";
            this.btn_Dtl_Delete.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_Delete.TabIndex = 2;
            this.btn_Dtl_Delete.Text = "删除";
            this.btn_Dtl_Delete.UseVisualStyleBackColor = true;
            this.btn_Dtl_Delete.Click += new EventHandler(this.btn_Dtl_Delete_Click);
            this.btn_Dtl_Edit.Location = new Point(0x6d, 12);
            this.btn_Dtl_Edit.Name = "btn_Dtl_Edit";
            this.btn_Dtl_Edit.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_Edit.TabIndex = 1;
            this.btn_Dtl_Edit.Text = "修改";
            this.btn_Dtl_Edit.UseVisualStyleBackColor = true;
            this.btn_Dtl_Edit.Click += new EventHandler(this.btn_Dtl_Edit_Click);
            this.btn_Dtl_New.Location = new Point(15, 12);
            this.btn_Dtl_New.Name = "btn_Dtl_New";
            this.btn_Dtl_New.Size = new Size(0x4b, 0x17);
            this.btn_Dtl_New.TabIndex = 0;
            this.btn_Dtl_New.Text = "新增";
            this.btn_Dtl_New.UseVisualStyleBackColor = true;
            this.btn_Dtl_New.Click += new EventHandler(this.btn_Dtl_New_Click);
            this.grpEdit.Controls.Add(this.txt_Dtl_fRate);
            this.grpEdit.Controls.Add(this.label35);
            this.grpEdit.Controls.Add(this.label34);
            this.grpEdit.Controls.Add(this.label33);
            this.grpEdit.Controls.Add(this.cmb_Dtl_cParentUnit);
            this.grpEdit.Controls.Add(this.cmb_Dtl_cChildUnit);
            this.grpEdit.Location = new Point(0x3e, 0xd9);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new Size(0x214, 80);
            this.grpEdit.TabIndex = 1;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "编辑区";
            this.label35.AutoSize = true;
            this.label35.Location = new Point(0x17a, 0x20);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x2f, 12);
            this.label35.TabIndex = 0x77;
            this.label35.Text = "换算率:";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(0xd4, 0x1f);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0x2f, 12);
            this.label34.TabIndex = 0x76;
            this.label34.Text = "小单位:";
            this.label33.AutoSize = true;
            this.label33.Location = new Point(0x10, 0x1c);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x2f, 12);
            this.label33.TabIndex = 0x75;
            this.label33.Text = "大单位:";
            this.cmb_Dtl_cParentUnit.FormattingEnabled = true;
            this.cmb_Dtl_cParentUnit.Location = new Point(90, 0x1c);
            this.cmb_Dtl_cParentUnit.Name = "cmb_Dtl_cParentUnit";
            this.cmb_Dtl_cParentUnit.Size = new Size(0x62, 20);
            this.cmb_Dtl_cParentUnit.TabIndex = 0;
            this.cmb_Dtl_cParentUnit.Tag = "101";
            this.cmb_Dtl_cParentUnit.Text = "Bind SelectedValue";
            this.cmb_Dtl_cChildUnit.Enabled = false;
            this.cmb_Dtl_cChildUnit.FormattingEnabled = true;
            this.cmb_Dtl_cChildUnit.Location = new Point(0x109, 0x1c);
            this.cmb_Dtl_cChildUnit.Name = "cmb_Dtl_cChildUnit";
            this.cmb_Dtl_cChildUnit.Size = new Size(0x63, 20);
            this.cmb_Dtl_cChildUnit.TabIndex = 1;
            this.cmb_Dtl_cChildUnit.Tag = "101";
            this.cmb_Dtl_cChildUnit.Text = "Bind SelectedValue";
            this.grdUnit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUnit.Columns.AddRange(new DataGridViewColumn[] { this.col_dtl_cParentUnit, this.col_Dtl_cChildUnit, this.col_Dtl_fRate });
            this.grdUnit.Location = new Point(0x3e, 0x24);
            this.grdUnit.Name = "grdUnit";
            this.grdUnit.ReadOnly = true;
            this.grdUnit.RowTemplate.Height = 0x17;
            this.grdUnit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdUnit.Size = new Size(0x214, 0xa9);
            this.grdUnit.TabIndex = 0;
            this.col_dtl_cParentUnit.DataPropertyName = "cParentUnit";
            this.col_dtl_cParentUnit.HeaderText = "大单位";
            this.col_dtl_cParentUnit.Name = "col_dtl_cParentUnit";
            this.col_dtl_cParentUnit.ReadOnly = true;
            this.col_Dtl_cChildUnit.DataPropertyName = "cChildUnit";
            this.col_Dtl_cChildUnit.HeaderText = "小单位";
            this.col_Dtl_cChildUnit.Name = "col_Dtl_cChildUnit";
            this.col_Dtl_cChildUnit.ReadOnly = true;
            this.col_Dtl_fRate.DataPropertyName = "fRate";
            this.col_Dtl_fRate.HeaderText = "转换率";
            this.col_Dtl_fRate.Name = "col_Dtl_fRate";
            this.col_Dtl_fRate.ReadOnly = true;
            this.bsGrid.PositionChanged += new EventHandler(this.bsGrid_PositionChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x410, 0x270);
            base.Controls.Add(this.tbcMain);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.KeyPreview = true;
            base.Name = "FrmStockMaterInfo";
            this.Text = "物料基本资料";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.ppmPrint.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            this.tbcMain.ResumeLayout(false);
            this.tbpInfo.ResumeLayout(false);
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.grp_EditOther.ResumeLayout(false);
            this.grp_EditOther.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.tbpPackSpec.ResumeLayout(false);
            this.pnl_Dtl_Buttons.ResumeLayout(false);
            this.grpEdit.ResumeLayout(false);
            this.grpEdit.PerformLayout();
            ((ISupportInitialize) this.grdUnit).EndInit();
            ((ISupportInitialize) this.bsGrid).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void label22_Click(object sender, EventArgs e)
        {
        }

        private void LoadCombPalletSpec(ComboBox cmbX)
        {
            string sSql = "select cPalletSpecId,cPalletSpec from TWC_PalletSpec ";
            string sErr = "";
            DataSet set = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TWC_PalletSpec", 0, 0, "", out sErr);
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
            cmbX.ValueMember = "cPalletSpec";
            cmbX.DisplayMember = "cPalletSpec";
            if (set != null)
            {
                DataTable table = set.Tables["TWC_PalletSpec"];
                cmbX.DataSource = table;
            }
        }

        private void LoadCombWAreaList(string sWHId, ComboBox cmbX)
        {
            string sSql = "select cAreaId,cAreaName from TWC_WArea where bUsed=1 ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + sWHId.Trim() + "'";
            }
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

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(0, "普通物品"));
            list.Add(new DictionaryEntry(1, "医药物品"));
            list.Add(new DictionaryEntry(2, "食品"));
            list.Add(new DictionaryEntry(3, "化妆品"));
            this.cmb_nMatClass.DisplayMember = "Value";
            this.cmb_nMatClass.ValueMember = "Key";
            this.cmb_nMatClass.DataSource = list;
        }

        private void LoadDoseType()
        {
            string str = "select * from TWC_BaseItem where bUsed=1 and  cItemType='剂型'  ";
            string str2 = "";
            string sErr = "";
            DataSet set = WareBaseMS.DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, str + str2, "DoseType", 0, 0, "", out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
                if (set != null)
                {
                    set.Dispose();
                }
            }
            else
            {
                this.cmb_cDoseType.DataSource = set.Tables["data"];
                this.cmb_cDoseType.DisplayMember = "cItemName";
                this.cmb_cDoseType.ValueMember = "cItemNo";
                this.cmb_cDoseType.SelectedIndex = -1;
            }
        }

        private void LoadMaterialType()
        {
            string str = "select * from TPC_MaterialType ";
            string str2 = " where cTypeMode=0";
            string sErr = "";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(str + str2, out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
                if (dataBySql != null)
                {
                    dataBySql.Dispose();
                }
            }
            else
            {
                this.cmb_cTypeId1.DataSource = dataBySql.Tables["data"];
                this.cmb_cTypeId1.DisplayMember = "cTypeName";
                this.cmb_cTypeId1.ValueMember = "cTypeId";
                DataTable table = dataBySql.Tables["data"].Copy();
                this.cmbQ_cTypeId1.DataSource = table;
                this.cmbQ_cTypeId1.DisplayMember = "cTypeName";
                this.cmbQ_cTypeId1.ValueMember = "cTypeId";
                str2 = " where cTypeMode=1";
                sErr = "";
                DataSet set2 = PubDBCommFuns.GetDataBySql(str + str2, out sErr);
                if (sErr.Length > 0)
                {
                    MessageBox.Show(sErr);
                    if (set2 != null)
                    {
                        set2.Dispose();
                    }
                }
                else
                {
                    this.cmb_cTypeId2.DataSource = set2.Tables["data"];
                    this.cmb_cTypeId2.DisplayMember = "cTypeName";
                    this.cmb_cTypeId2.ValueMember = "cTypeId";
                    DataTable table2 = set2.Tables["data"].Copy();
                    this.cmbQ_cTypeId2.DataSource = table2;
                    this.cmbQ_cTypeId2.DisplayMember = "cTypeName";
                    this.cmbQ_cTypeId2.ValueMember = "cTypeId";
                }
            }
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
                sSql = sSql + " where cWHId='" + StockId + "'";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cWHId.DataSource = dataBySql.Tables["data"];
            this.cmb_cWHId.DisplayMember = "cName";
            this.cmb_cWHId.ValueMember = "cWHId";
            DataTable table = dataBySql.Tables["data"].Copy();
            this.cmbQ_cWHId.DataSource = table;
            this.cmbQ_cWHId.DisplayMember = "cName";
            this.cmbQ_cWHId.ValueMember = "cWHId";
        }

        private void LoadUnit(int nUnitType)
        {
            string sErr = "";
            string sSql = "select * from tpc_unit ";
            if (nUnitType >= 0)
            {
                sSql = sSql + " where nUnitType =" + nUnitType.ToString();
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cUnit.DataSource = dataBySql.Tables["data"];
            this.cmb_cUnit.DisplayMember = "cCName";
            this.cmb_cUnit.ValueMember = "cCName";
            DataTable table = dataBySql.Tables["data"].Copy();
            this.cmb_Dtl_cParentUnit.DataSource = table;
            this.cmb_Dtl_cParentUnit.DisplayMember = "cCName";
            this.cmb_Dtl_cParentUnit.ValueMember = "cCName";
            DataTable table2 = dataBySql.Tables["data"].Copy();
            this.cmb_Dtl_cChildUnit.DataSource = table2;
            this.cmb_Dtl_cChildUnit.DisplayMember = "cCName";
            this.cmb_Dtl_cChildUnit.ValueMember = "cCName";
        }

        private void mmi_BatchUpdateItemValue_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("cColName");
            table.Columns.Add("cColDesc");
            table.Columns.Add("cDataType");
            table.Rows.Add(new object[] { "cSpec", "物料规格", "string" });
            table.Rows.Add(new object[] { "cUnit", "计量单位", "string" });
            table.Rows.Add(new object[] { "fWeight", "单位重量", "double" });
            table.Rows.Add(new object[] { "fSafeQtyDn", "安全库存下限数", "double" });
            table.Rows.Add(new object[] { "fSafeQtyUp", "安全库存上限数", "double" });
            table.Rows.Add(new object[] { "nKeepDay", "保质天数", "int" });
            table.Rows.Add(new object[] { "cWHId", "仓库编码", "string" });
            table.Rows.Add(new object[] { "fQtyBox", "每盘数量", "double" });
            table.Rows.Add(new object[] { "bIsMixPalce", "是否混放", "int" });
            table.Rows.Add(new object[] { "bIsQC", "是否QC", "int" });
            table.Rows.Add(new object[] { "nTag", "当前自动批号数", "int" });
            table.Rows.Add(new object[] { "bIsSubQtyForQC", "取样是否扣库存", "int" });
            table.Rows.Add(new object[] { "nAutoPromptDay", "自动报警提醒天数", "int" });
            table.Rows.Add(new object[] { "bIsAutoBatchNo", "是否自动递增批号", "int" });
            table.Rows.Add(new object[] { "cMatStyle", "物料款式", "string" });
            table.Rows.Add(new object[] { "cMatOther", "物料其他属性", "string" });
            table.Rows.Add(new object[] { "cMatQCLevel", "质量等级", "string" });
            table.Rows.Add(new object[] { "cRemark", "物料备注", "string" });
            table.Rows.Add(new object[] { "cABC", "物料ABC属性", "string" });
            frmBtchUpdateEditor editor = new frmBtchUpdateEditor {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation,
                TbFields = table,
                doBatchUpdateData = new DoBatchUpdateData(this.doBtchUpdateData)
            };
            editor.ShowDialog();
            editor.Dispose();
        }

        private void mmi_UpdateMatPYWB_Click(object sender, EventArgs e)
        {
            if ((this.bindingSource_Main.Count > 0) && (this.dataGridView_Main.SelectedRows.Count > 0))
            {
                string message = "";
                this.stbProg.Maximum = this.dataGridView_Main.SelectedRows.Count;
                this.stbProg.Minimum = 0;
                this.stbProg.Value = 0;
                this.stbProg.Visible = true;
                int num = 0;
                foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                {
                    string sErr = "";
                    if (row.Cells["cName"].Value != null)
                    {
                        string sText = row.Cells["cName"].Value.ToString();
                        string str4 = row.Cells["cMNo"].Value.ToString();
                        string wBPY = TextPYWB.GetWBPY(sText, PYWBType.pwtPYFirst);
                        string str6 = TextPYWB.GetWBPY(sText, PYWBType.pwtWBFirst);
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            if (WareBaseMS.DBFuns.DoExecSql(base.AppInformation.SvrSocket, "update TPC_Material set cWBJM='" + str6 + "',cPYJM='" + wBPY + "' where cMNo='" + str4 + "'", "", out sErr))
                            {
                                num++;
                            }
                            else if (message.Length == 0)
                            {
                                message = sErr;
                            }
                        }
                        catch (Exception exception)
                        {
                            message = exception.Message;
                        }
                    }
                    this.stbProg.Value++;
                }
                this.stbProg.Visible = false;
                if (message.Length > 0)
                {
                    MessageBox.Show("更新物料拼音或五笔简码数据时出现错误：" + message);
                }
                MessageBox.Show("成功更新了：" + num.ToString() + "/" + this.stbProg.Maximum.ToString() + " 数据！");
            }
        }

        private void mmiPrintBCInfo_Click(object sender, EventArgs e)
        {
            string sValue = "";
            string str2 = "";
            string str3 = "0";
            string str4 = "1";
            string dllFile = Path.Combine(Application.StartupPath, "BarCodeReport.dll");
            XmlWriteReader reader = new XmlWriteReader();
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无数据可打印条码！");
            }
            else
            {
                string sFile = Path.Combine(Application.StartupPath, base.AppInformation.AppConfigFile);
                reader.OpenXMLFile(sFile);
                reader.GetNodeAtrribeValue("config/printers/BarCode_MaterialId", "RptName", out sValue);
                reader.GetNodeAtrribeValue("config/printers/BarCode_MaterialId", "PrinterName", out str2);
                reader.GetNodeAtrribeValue("config/printers/BarCode_MaterialId", "PrtMode", out str3);
                reader.GetNodeAtrribeValue("config/printers/BarCode_MaterialId", "Copies", out str4);
                if (str3.Trim() == "")
                {
                    str3 = "0";
                }
                if (str4.Trim() == "")
                {
                    str4 = "1";
                }
                if (sValue.Trim() == "")
                {
                    sValue = Path.Combine(Application.StartupPath, @"rpt\rptMaterialInfo6-4.5.fr3");
                }
                else
                {
                    sValue = Path.Combine(Application.StartupPath, @"rpt\" + sValue);
                }
                bool bIsOK = false;
                foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                {
                    string str7 = row.Cells["cMNo"].Value.ToString().Trim();
                    string str8 = row.Cells["cName"].Value.ToString().Trim();
                    string str9 = row.Cells["cSpec"].Value.ToString().Trim();
                    MyCallUnSafetyDll.DoCallMyDll(dllFile, "PrintBCMaterialInfo", new object[] { sValue, str7, str8, str9, str2, int.Parse(str3), int.Parse(str4) }, new System.Type[] { System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") }, new ModePass[] { ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue }, null, out bIsOK);
                }
            }
        }

        private void mmiPrintBCOnly_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无数据可打印条码！");
            }
            else
            {
                string str = current["cMNo"].ToString().Trim();
                string str2 = "";
                string str3 = Path.Combine(Application.StartupPath, @"rpt\rptMaterialInfo6-4.5.fr3");
                string dllFile = Path.Combine(Application.StartupPath, "BarCodeReport.dll");
                string str5 = "";
                bool bIsOK = false;
                MyCallUnSafetyDll.DoCallMyDll(dllFile, "PrintBarCode", new object[] { str3, str, str2, str5, 0, 1 }, new System.Type[] { System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") }, new ModePass[] { ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue }, null, out bIsOK);
            }
        }

        private void OpenPackUnit(string sMNo)
        {
            string sErr = "";
            string sSql = "select cMNo,cChildUnit,cParentUnit,fRate,nLevel from TPC_MatPackingUnit where cMNo='" + sMNo.Trim() + "'";
            Cursor.Current = Cursors.WaitCursor;
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            Cursor.Current = Cursors.Default;
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show("打开单位换算数据时出错：" + sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开单位换算数据时出错：");
            }
            else
            {
                DataTable table = set.Tables["data"];
                if (table == null)
                {
                    MessageBox.Show("打开单位换算数据时出错：");
                }
                else
                {
                    this.bUnitIsOpen = false;
                    this.bsGrid.DataSource = table;
                    this.grdUnit.DataSource = this.bsGrid;
                    this.bUnitIsOpen = true;
                    this.OptDtl = OperateType.optNone;
                    if (this.bsGrid.Count == 0)
                    {
                        this.CtrlOptButtons(this.pnl_Dtl_Buttons, this.grpEdit, OperateType.optNone, (DataTable) this.bsGrid.DataSource);
                    }
                    else
                    {
                        this.bsGrid_PositionChanged(null, null);
                    }
                }
            }
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ReadSysPar()
        {
            string sErr = "";
            string sSql = "select * from tps_syspar where cParId='MNoIsManual'";
            DataSet dataBySql = null;
            dataBySql = WareBaseMS.DBFuns.GetDataBySql(sSql, out sErr);
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

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bindingSource_Main.Count != 0)
            {
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if ((current != null) && (this.tbcMain.SelectedIndex == 1))
                {
                    string sMNo = current["cMNo"].ToString().Trim();
                    this.OpenPackUnit(sMNo);
                }
            }
        }

        private void textBox_cMNo_Leave(object sender, EventArgs e)
        {
            if (((this.OptMain == OperateType.optEdit) || (this.OptMain == OperateType.optNew)) && (this.textBox_cMNo.Text.Trim() != ""))
            {
                this.textBox_cBorCode.Text = this.textBox_cMNo.Text.Trim();
            }
        }

        private void textBox_cMNo_ReadOnlyChanged(object sender, EventArgs e)
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
            base.Close();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            this.DoNew();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
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

