namespace WareBaseMS
{
    using SunEast;
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;
    using DBCommInfo;

    public class frmBaseItem : FrmSTable
    {
        private BindingSource bdsMain;
        private bool bMainlstIsOpened = false;
        private ToolStripButton btn_M_Help;
        private Button btnQry;
        private ComboBox cmb_bUsed;
        private ComboBox cmb_cItemType;
        private ComboBox cmbItemType;
        private DataGridViewTextBoxColumn colcItemName;
        private DataGridViewTextBoxColumn colcItemNo;
        private DataGridViewTextBoxColumn colcItemType;
        private DataGridViewTextBoxColumn colcUsed;
        private IContainer components = null;
        private DateTimePicker dtp_dDate;
        public DataGridView grdList;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private OperateType optMain = OperateType.optNone;
        public Panel pnlEdit;
        private Panel pnlLeftTop;
        public SplitContainer pnlSplit;
        private StringBuilder sbConndition = new StringBuilder("");
        public ToolStripStatusLabel stbDate;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModule;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strFix = "";
        private string strKeyFld = "NID";
        private string strTbNameMain = "TWC_BASEITEM";
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
        private TextBox txt_cItemName;
        private TextBox txt_cItemNo;
        private TextBox txt_nId;
        private TextBox txt_nSort;
        private TextBox txtFindName;

        public frmBaseItem()
        {
            this.InitializeComponent();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (this.bMainlstIsOpened)
            {
                this.ClearUIValues(this.pnlEdit);
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (!((this.bdsMain.Count <= 0) || current.IsNew))
                {
                    this.DataRowViewToUI(current, this.pnlEdit);
                }
            }
        }

        public void BindDataSetToCtrls()
        {
            this.DataSetUnBind(this.pnlEdit);
            this.DataSetBind(this.pnlEdit, this.bdsMain);
            this.grdList.DataSource = null;
            this.grdList.DataSource = this.bdsMain;
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            this.sbConndition.Remove(0, this.sbConndition.Length);
            if (this.cmbItemType.Text.Trim() != "")
            {
                this.sbConndition.Append(" where cItemType='" + this.cmbItemType.Text.Trim() + "'");
            }
            if (this.txtFindName.Text.Trim() != "")
            {
                if (this.sbConndition.Length > 0)
                {
                    this.sbConndition.Append(" and (cItemName like '%" + this.txtFindName.Text.Trim() + "%') or (cItemNo like '%" + this.txtFindName.Text.Trim() + "%')");
                }
                else
                {
                    this.sbConndition.Append(" where (cItemName like '%" + this.txtFindName.Text.Trim() + "%') or (cItemNo like '%" + this.txtFindName.Text.Trim() + "%')");
                }
            }
            this.OpenMainDataSet(this.sbConndition.ToString());
        }

        private void cmb_cItemType_Leave(object sender, EventArgs e)
        {
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
                        return;

                    case OperateType.optEdit:
                        str = str + " 修改";
                        return;
                }
                str = str + "    ";
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
            else if ((current["cUser"].ToString().Trim().ToLower() == "sys") && (base.UserInformation.UType != UserType.utSupervisor))
            {
                MessageBox.Show("对不起,此为系统数据，不能修改或删除！");
            }
            else if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    SqlText = "delete " + this.strTbNameMain + " where " + this.strKeyFld + "=" + current[this.strKeyFld].ToString(),
                    SqlType = SqlCommandType.sctSql,
                    PageIndex = 0,
                    PageSize = 0,
                    FromSysType = "dotnet",
                    DataTableName = this.strTbNameMain
                };
                SeDBClient client = new SeDBClient();
                string sErr = "";
                if (client.GetDataSet(cmdInfo, out sErr) != null)
                {
                    this.optMain = OperateType.optDelete;
                    this.OpenMainDataSet(this.sbConndition.ToString());
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.optMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, false);
                }
                else
                {
                    MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        public void DoMEdit()
        {
            this.optMain = OperateType.optEdit;
            DataRowView current = (DataRowView) this.bdsMain.Current;
            current.BeginEdit();
            current["dDate"] = DateTime.Now;
            current["cUser"] = base.UserInformation.UserName;
            this.cmb_cItemType.Enabled = false;
            this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.txt_cItemNo.Focus();
            this.DisplayState(this.stbState, this.optMain);
            this.CtrlControlReadOnly(this.pnlEdit, true);
            this.txt_nId.ReadOnly = true;
        }

        public void DoMNew()
        {
            DataRowView drv = null;
            if (this.bdsMain.DataSource != null)
            {
                try
                {
                    drv = (DataRowView) this.bdsMain.AddNew();
                    this.optMain = OperateType.optNew;
                    drv[this.strKeyFld] = -1;
                    drv["dDate"] = DateTime.Now;
                    drv["cUser"] = base.UserInformation.UserName;
                    drv["nSort"] = 0;
                    drv["bUsed"] = 1;
                    if (this.cmbItemType.Text.Trim() != "")
                    {
                        drv["cItemType"] = this.cmbItemType.Text.Trim();
                    }
                    this.cmb_cItemType.Enabled = true;
                    this.DataRowViewToUI(drv, this.pnlEdit);
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.txt_cItemNo.Focus();
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, true);
                    this.txt_nId.ReadOnly = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    this.bdsMain.CancelEdit();
                    this.optMain = OperateType.optNone;
                }
            }
        }

        public void DoMSave()
        {
            bool flag = false;
            string str = "";
            this.txt_nId.Focus();
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current.IsEdit || current.IsNew)
            {
                this.UIToDataRowView(current, this.pnlEdit);
                string str2 = this.cmbItemType.Text.Trim();
                string sText = this.cmb_cItemType.Text.Trim();
                if (this.FindCmbIndex(sText, this.cmb_cItemType, this.cmb_cItemType.DisplayMember) < 0)
                {
                    if (base.UserInformation.UType == UserType.utNormal)
                    {
                        MessageBox.Show("对不起，项目种类不存在，录入有误！");
                        this.cmb_cItemType.SelectAll();
                        this.cmb_cItemType.Focus();
                        return;
                    }
                    if (MessageBox.Show("项目种类不存在，是否增加？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                    flag = true;
                }
                if ((current[this.strKeyFld].ToString() == "") || (current[this.strKeyFld].ToString() == "-1"))
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 0, "");
                    if ((current["cItemNo"] == null) || (current["cItemNo"].ToString().Trim() == ""))
                    {
                        current["cItemNo"] = current[this.strKeyFld];
                    }
                    str = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, "cUsed", true);
                }
                else
                {
                    if ((current["cItemNo"] == null) || (current["cItemNo"].ToString().Trim() == ""))
                    {
                        if (!current.IsEdit)
                        {
                            current.BeginEdit();
                        }
                        current["cItemNo"] = current[this.strKeyFld];
                    }
                    str = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, "cUsed", false);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    SqlText = str,
                    FldsData = DBSQLCommandInfo.GetFieldsForDate(current),
                    SqlType = SqlCommandType.sctSql,
                    PageIndex = 0,
                    PageSize = 0,
                    FromSysType = "dotnet"
                };
                SeDBClient client = new SeDBClient();
                string sErr = "";
                if (client.GetDataSet(cmdInfo, out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.optMain = OperateType.optSave;
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OpenMainDataSet(this.sbConndition.ToString());
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.optMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, false);
                    if (flag)
                    {
                        this.LoadBaseData();
                        this.cmbItemType.Text = str2;
                    }
                }
                else
                {
                    MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                base.DBDataSet.Tables[this.strTbNameMain].AcceptChanges();
                this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.optMain = OperateType.optNone;
                this.DisplayState(this.stbState, this.optMain);
                this.CtrlControlReadOnly(this.pnlEdit, false);
            }
        }

        private int FindCmbIndex(string sText, ComboBox cmbX, string sFldValue)
        {
            int num2;
            int num = -1;
            object dataSource = cmbX.DataSource;
            if (dataSource != null)
            {
                if (dataSource.GetType().Name == "DataTable")
                {
                    DataTable table = (DataTable) dataSource;
                    for (num2 = 0; num2 < table.Rows.Count; num2++)
                    {
                        if (sText.Trim() == table.Rows[num2][sFldValue].ToString().Trim())
                        {
                            return num2;
                        }
                    }
                    return num;
                }
                if (dataSource.GetType().Name == "DataView")
                {
                    DataView view = (DataView) dataSource;
                    for (num2 = 0; num2 < view.Table.Rows.Count; num2++)
                    {
                        if (sText.Trim() == view.Table.Rows[num2][sFldValue].ToString().Trim())
                        {
                            return num2;
                        }
                    }
                }
                return num;
            }
            if (cmbX.Items.Count > 0)
            {
                for (num2 = 0; num2 < cmbX.Items.Count; num2++)
                {
                    if (sText.Trim() == cmbX.Items[num2].ToString().Trim())
                    {
                        return num2;
                    }
                }
            }
            return num;
        }

        private void frmBaseItem_Load(object sender, EventArgs e)
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
            this.LoadBaseData();
            string sCon = "";
            if (this.cmbItemType.Text.Trim() != "")
            {
                sCon = " where cItemType='" + this.cmbItemType.Text.Trim() + "'";
            }
            this.OpenMainDataSet(sCon);
        }

        private string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetNewId",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "@TbName",
                ParameterValue = sTbName,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "@FldKey",
                ParameterValue = sKeyFld,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "@Len",
                ParameterValue = nLength.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "@ReplaceChar",
                ParameterValue = "0",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "@Header",
                ParameterValue = sHeader,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "@FldCon",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            paramter = new ZqmParamter {
                ParameterName = "@ValueCon",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet dataSet = null;
            DataTable table = null;
            dataSet = client.GetDataSet(cmdInfo, out sErr);
            if (dataSet != null)
            {
                table = dataSet.Tables["data"];
                if (table != null)
                {
                    str = table.Rows[0]["cNewId"].ToString();
                }
            }
            dataSet.Clear();
            return str;
        }

        public override void InitFormParameters()
        {
            if (base.ModuleRtsId.Length > 0)
            {
                this.Text = base.ModuleRtsName;
            }
            this.stbModule.Text = "模块【" + this.Text + "】";
            if (base.UserInformation != null)
            {
                this.stbUser.Text = "用户【" + base.UserInformation.UserName + "】";
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmBaseItem));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
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
            this.stbDate = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.bdsMain = new BindingSource(this.components);
            this.stbModule = new ToolStripStatusLabel();
            this.stbMain = new StatusStrip();
            this.tmrMain = new Timer(this.components);
            this.pnlSplit = new SplitContainer();
            this.grdList = new DataGridView();
            this.colcItemType = new DataGridViewTextBoxColumn();
            this.colcItemName = new DataGridViewTextBoxColumn();
            this.colcItemNo = new DataGridViewTextBoxColumn();
            this.colcUsed = new DataGridViewTextBoxColumn();
            this.pnlLeftTop = new Panel();
            this.cmbItemType = new ComboBox();
            this.label8 = new Label();
            this.btnQry = new Button();
            this.txtFindName = new TextBox();
            this.label1 = new Label();
            this.pnlEdit = new Panel();
            this.label9 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.txt_nSort = new TextBox();
            this.dtp_dDate = new DateTimePicker();
            this.cmb_bUsed = new ComboBox();
            this.label5 = new Label();
            this.txt_cItemName = new TextBox();
            this.label4 = new Label();
            this.cmb_cItemType = new ComboBox();
            this.txt_cItemNo = new TextBox();
            this.label3 = new Label();
            this.txt_nId = new TextBox();
            this.label2 = new Label();
            this.tlbMain.SuspendLayout();
            ((ISupportInitialize) this.bdsMain).BeginInit();
            this.stbMain.SuspendLayout();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            this.pnlLeftTop.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x281, 0x19);
            this.tlbMain.TabIndex = 0x15;
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
            this.stbDate.Name = "stbDate";
            this.stbDate.Size = new Size(0x23, 0x11);
            this.stbDate.Text = "时间:";
            this.stbState.Name = "stbState";
            this.stbState.Size = new Size(0x23, 0x11);
            this.stbState.Text = "状态:";
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new Size(0x2f, 0x11);
            this.stbUser.Text = "用户名:";
            this.bdsMain.PositionChanged += new EventHandler(this.bdsMain_PositionChanged);
            this.stbModule.Name = "stbModule";
            this.stbModule.Size = new Size(0x23, 0x11);
            this.stbModule.Text = "模块:";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModule, this.stbUser, this.stbState, this.stbDate });
            this.stbMain.Location = new Point(0, 0x1fb);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x281, 0x16);
            this.stbMain.TabIndex = 20;
            this.stbMain.Text = "statusStrip1";
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 0x1388;
            this.pnlSplit.Dock = DockStyle.Fill;
            this.pnlSplit.Location = new Point(0, 0x19);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Panel1.Controls.Add(this.grdList);
            this.pnlSplit.Panel1.Controls.Add(this.pnlLeftTop);
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = ImeMode.NoControl;
            this.pnlSplit.Size = new Size(0x281, 0x1e2);
            this.pnlSplit.SplitterDistance = 0xf6;
            this.pnlSplit.TabIndex = 0;
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colcItemType, this.colcItemName, this.colcItemNo, this.colcUsed });
            this.grdList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new Point(3, 0x42);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = style;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 0x17;
            this.grdList.Size = new Size(230, 0x18e);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            this.colcItemType.DataPropertyName = "cItemType";
            this.colcItemType.Frozen = true;
            this.colcItemType.HeaderText = "项目类别";
            this.colcItemType.Name = "colcItemType";
            this.colcItemType.ReadOnly = true;
            this.colcItemType.ToolTipText = "项目类别";
            this.colcItemName.DataPropertyName = "cItemName";
            this.colcItemName.HeaderText = "项目名称";
            this.colcItemName.Name = "colcItemName";
            this.colcItemName.ReadOnly = true;
            this.colcItemName.ToolTipText = "项目名称";
            this.colcItemNo.DataPropertyName = "cItemNo";
            this.colcItemNo.HeaderText = "项目编号";
            this.colcItemNo.Name = "colcItemNo";
            this.colcItemNo.ReadOnly = true;
            this.colcItemNo.ToolTipText = "项目编号";
            this.colcUsed.DataPropertyName = "cUsed";
            this.colcUsed.HeaderText = "启用状态";
            this.colcUsed.Name = "colcUsed";
            this.colcUsed.ReadOnly = true;
            this.colcUsed.ToolTipText = "启用状态";
            this.pnlLeftTop.Controls.Add(this.cmbItemType);
            this.pnlLeftTop.Controls.Add(this.label8);
            this.pnlLeftTop.Controls.Add(this.btnQry);
            this.pnlLeftTop.Controls.Add(this.txtFindName);
            this.pnlLeftTop.Controls.Add(this.label1);
            this.pnlLeftTop.Location = new Point(3, 12);
            this.pnlLeftTop.Name = "pnlLeftTop";
            this.pnlLeftTop.Size = new Size(0xe8, 0x33);
            this.pnlLeftTop.TabIndex = 0;
            this.cmbItemType.CausesValidation = false;
            this.cmbItemType.FormattingEnabled = true;
            this.cmbItemType.Items.AddRange(new object[] { "普通用户", "管理员", "超级管理员" });
            this.cmbItemType.Location = new Point(50, 3);
            this.cmbItemType.Name = "cmbItemType";
            this.cmbItemType.Size = new Size(160, 20);
            this.cmbItemType.TabIndex = 0x1a;
            this.cmbItemType.Tag = "";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(3, 7);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x29, 12);
            this.label8.TabIndex = 0x19;
            this.label8.Text = "类别：";
            this.btnQry.Location = new Point(0xab, 0x1b);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new Size(0x27, 0x17);
            this.btnQry.TabIndex = 2;
            this.btnQry.Text = "查询";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new EventHandler(this.btnQry_Click);
            this.txtFindName.Location = new Point(0x36, 0x1b);
            this.txtFindName.Name = "txtFindName";
            this.txtFindName.Size = new Size(0x74, 0x15);
            this.txtFindName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名：";
            this.pnlEdit.BackColor = SystemColors.Info;
            this.pnlEdit.Controls.Add(this.label9);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.txt_nSort);
            this.pnlEdit.Controls.Add(this.dtp_dDate);
            this.pnlEdit.Controls.Add(this.cmb_bUsed);
            this.pnlEdit.Controls.Add(this.label5);
            this.pnlEdit.Controls.Add(this.txt_cItemName);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.cmb_cItemType);
            this.pnlEdit.Controls.Add(this.txt_cItemNo);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.txt_nId);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Location = new Point(7, 0x2b);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new Size(0x178, 0x11f);
            this.pnlEdit.TabIndex = 0;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x1c, 0x9f);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 0x12;
            this.label9.Text = "是否启用：";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x1c, 200);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 0x11;
            this.label7.Text = "日期：";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xef, 0x9f);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x10;
            this.label6.Text = "排序号：";
            this.txt_nSort.BorderStyle = BorderStyle.FixedSingle;
            this.txt_nSort.Location = new Point(310, 0x9f);
            this.txt_nSort.Name = "txt_nSort";
            this.txt_nSort.ReadOnly = true;
            this.txt_nSort.Size = new Size(0x2a, 0x15);
            this.txt_nSort.TabIndex = 5;
            this.txt_nSort.Tag = "0";
            this.txt_nSort.Text = "0";
            this.dtp_dDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_dDate.Format = DateTimePickerFormat.Custom;
            this.dtp_dDate.Location = new Point(0x73, 0xc4);
            this.dtp_dDate.Name = "dtp_dDate";
            this.dtp_dDate.Size = new Size(0x63, 0x15);
            this.dtp_dDate.TabIndex = 6;
            this.dtp_dDate.Tag = "2";
            this.cmb_bUsed.FormattingEnabled = true;
            this.cmb_bUsed.Items.AddRange(new object[] { "停用", "启用" });
            this.cmb_bUsed.Location = new Point(0x73, 0x9f);
            this.cmb_bUsed.Name = "cmb_bUsed";
            this.cmb_bUsed.Size = new Size(0x38, 20);
            this.cmb_bUsed.TabIndex = 4;
            this.cmb_bUsed.Tag = "102";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x1c, 0x7b);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "项目名称：";
            this.txt_cItemName.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cItemName.Location = new Point(0x73, 0x79);
            this.txt_cItemName.Name = "txt_cItemName";
            this.txt_cItemName.ReadOnly = true;
            this.txt_cItemName.Size = new Size(0xed, 0x15);
            this.txt_cItemName.TabIndex = 3;
            this.txt_cItemName.Tag = "0";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1c, 0x55);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "项目编号：";
            this.cmb_cItemType.BackColor = SystemColors.MenuBar;
            this.cmb_cItemType.FormattingEnabled = true;
            this.cmb_cItemType.Location = new Point(0x73, 0x2f);
            this.cmb_cItemType.Name = "cmb_cItemType";
            this.cmb_cItemType.Size = new Size(0x90, 20);
            this.cmb_cItemType.TabIndex = 1;
            this.cmb_cItemType.Tag = "1";
            this.cmb_cItemType.Text = "Bind Text";
            this.cmb_cItemType.Leave += new EventHandler(this.cmb_cItemType_Leave);
            this.txt_cItemNo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cItemNo.Location = new Point(0x73, 0x53);
            this.txt_cItemNo.Name = "txt_cItemNo";
            this.txt_cItemNo.ReadOnly = true;
            this.txt_cItemNo.Size = new Size(0xed, 0x15);
            this.txt_cItemNo.TabIndex = 2;
            this.txt_cItemNo.Tag = "0";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1c, 50);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "类别：";
            this.txt_nId.BorderStyle = BorderStyle.FixedSingle;
            this.txt_nId.Location = new Point(0x73, 12);
            this.txt_nId.Name = "txt_nId";
            this.txt_nId.ReadOnly = true;
            this.txt_nId.Size = new Size(0x90, 0x15);
            this.txt_nId.TabIndex = 0;
            this.txt_nId.Tag = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x1c, 12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "序号：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x281, 0x211);
            base.Controls.Add(this.pnlSplit);
            base.Controls.Add(this.tlbMain);
            base.Controls.Add(this.stbMain);
            base.KeyPreview = true;
            base.Name = "frmBaseItem";
            this.Text = "基础码表";
            base.Load += new EventHandler(this.frmBaseItem_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((ISupportInitialize) this.bdsMain).EndInit();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            ((ISupportInitialize) this.grdList).EndInit();
            this.pnlLeftTop.ResumeLayout(false);
            this.pnlLeftTop.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadBaseData()
        {
            string sErr = "";
            string sSql = "select distinct cItemType from TWC_BaseItem ";
            DataSet set = null;
            set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TWC_BaseItem", 0, 0, out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
            }
            else
            {
                DataTable table = set.Tables["TWC_BaseItem"];
                this.cmbItemType.DataSource = table;
                this.cmbItemType.DisplayMember = "cItemType";
                this.cmbItemType.ValueMember = "cItemType";
                DataTable table2 = table.Copy();
                this.cmb_cItemType.DataSource = table2;
                this.cmb_cItemType.DisplayMember = "cItemType";
                this.cmb_cItemType.ValueMember = "cItemType";
            }
        }

        public bool OpenMainDataSet(string sCon)
        {
            bool flag = false;
            this.bMainlstIsOpened = false;
            this.grdList.AutoGenerateColumns = false;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            string sSql = "select TWC_BaseItem.*,case bUsed when 0 then '停用' else '启用' end cUsed from " + this.strTbNameMain + sCon + " order by nSort,nId";
            string sErr = "";
            base.DBDataSet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, sSql, this.strTbNameMain, "dDate", out sErr);
            flag = base.DBDataSet != null;
            if (!flag)
            {
                MessageBox.Show(sErr);
            }
            else
            {
                try
                {
                    this.bdsMain.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
                    this.grdList.DataSource = this.bdsMain;
                    flag = true;
                    this.optMain = OperateType.optNone;
                    this.ClearUIValues(this.pnlEdit);
                    if (this.bdsMain.Count > 0)
                    {
                        this.DataRowViewToUI((DataRowView) this.bdsMain.Current, this.pnlEdit);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            this.bMainlstIsOpened = true;
            return flag;
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

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.btnQry_Click(null, null);
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            this.DoMSave();
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
    }
}

