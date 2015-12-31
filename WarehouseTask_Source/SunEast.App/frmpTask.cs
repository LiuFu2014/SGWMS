namespace SunEast.App
{
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

    public class frmpTask : FrmSTable
    {
        private BindingSource bdsMain;
        private ToolStripButton btn_M_Help;
        private Button btnDel;
        private Button btnExit;
        private Button btnRefresh;
        private Button btnTransfer;
        private DataGridViewTextBoxColumn colcBatchNo;
        private DataGridViewTextBoxColumn colcBNo;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn colcPosId;
        private DataGridViewTextBoxColumn colfFinished;
        private DataGridViewTextBoxColumn colfQty;
        private DataGridViewTextBoxColumn colnPalletId;
        private DataGridViewTextBoxColumn colnQCStatus;
        private DataGridViewTextBoxColumn colnStatus;
        private IContainer components = null;
        public DataGridView grdList;
        private OperateType optMain = OperateType.optNone;
        private Panel panel1;
        private Panel panel2;
        private StringBuilder sbConndition = new StringBuilder("");
        private string strKeyFld = "cBNo";
        private string strTbNameMain = "TWB_PWHWorkTask";
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
        private WareType wtWareType = WareType.wt3D;

        public frmpTask()
        {
            this.InitializeComponent();
        }

        public void BindDataSetToCtrls()
        {
            this.grdList.DataSource = null;
            this.grdList.DataSource = this.bdsMain;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.DoMDelete();
        }

        private void btnDel2_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    SqlText = "sp_pack_DelWKTskDtl :pnWorkId,:pBNo,:pItem",
                    SqlType = SqlCommandType.sctProcedure,
                    PageIndex = 0,
                    PageSize = 0,
                    FromSysType = "dotnet"
                };
                ZqmParamter paramter = null;
                paramter = new ZqmParamter {
                    ParameterName = "pnWorkId",
                    ParameterValue = current["nWorkId"].ToString().Trim(),
                    DataType = ZqmDataType.String,
                    ParameterDir = ZqmParameterDirction.Input
                };
                cmdInfo.Parameters.Add(paramter);
                paramter = new ZqmParamter {
                    ParameterName = "pBNo",
                    ParameterValue = current["cBNo"].ToString().Trim(),
                    DataType = ZqmDataType.String,
                    ParameterDir = ZqmParameterDirction.Input
                };
                cmdInfo.Parameters.Add(paramter);
                paramter = new ZqmParamter {
                    ParameterName = "pItem",
                    ParameterValue = current["nItem"].ToString().Trim(),
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
                }
                MessageBox.Show(table.Rows[0]["cResult"].ToString());
                dataSet.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.OpenMainDataSet();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current == null)
            {
                MessageBox.Show("对不起,无数据可过账!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (MessageBox.Show("确定要手动过账吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
                try
                {
                    string sErr = "";
                    string str2 = "-1";
                    if (int.Parse(current["nStatus"].ToString()) == 3)
                    {
                        MessageBox.Show("对不起，该任务已被过账了！");
                    }
                    else
                    {
                        string pBNo = current["cBNo"].ToString();
                        string s = current["nItem"].ToString();
                        string pPosId = current["cPosId"].ToString();
                        string pPalletId = current["nPalletId"].ToString();
                        string pBoxId = current["cBoxId"].ToString();
                        double pQty = double.Parse(current["fQty"].ToString());
                        int pOptType = int.Parse(current["nOptType"].ToString());
                        if (current["fQty"].ToString() != "")
                        {
                            pQty = double.Parse(current["fQty"].ToString());
                        }
                        string pBNoIn = current["cBNoIn"].ToString();
                        string str9 = current["nItemIn"].ToString();
                        string str10 = current["nWorkId"].ToString();
                        str2 = PubDBCommFuns.sp_DoPWHAccont(base.AppInformation.SvrSocket, "WMS", base.UserInformation.UserName, pBNo, int.Parse(s), pPosId, pPalletId, pBoxId, pQty, pBNoIn, int.Parse(str9), int.Parse(str10), base.UserInformation.UnitId, pOptType, out sErr);
                        if ((str2 == "") || (str2 == "-1"))
                        {
                            MessageBox.Show(sErr);
                        }
                        else
                        {
                            MessageBox.Show("过账成功！");
                            if (true)
                            {
                                this.optMain = OperateType.optDelete;
                                this.OpenMainDataSet();
                                this.optMain = OperateType.optNone;
                            }
                            else
                            {
                                MessageBox.Show("对不起,过账操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
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
            else if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
                try
                {
                    DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                        SqlText = "sp_Pack_DelPWHWKTskDtl :pBNo,:pBClass,:pItem,:pPosId,:pPalletId,:pBoxId,:pMNo,:pBatchNo",
                        SqlType = SqlCommandType.sctProcedure,
                        PageIndex = 0,
                        PageSize = 0,
                        FromSysType = "dotnet"
                    };
                    ZqmParamter paramter = null;
                    paramter = new ZqmParamter {
                        ParameterName = "pBNo",
                        ParameterValue = current["cBNo"].ToString(),
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pBClass",
                        ParameterValue = current["nBClass"].ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pItem",
                        ParameterValue = current["nItem"].ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pPosId",
                        ParameterValue = current["cPosId"].ToString(),
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pPalletId",
                        ParameterValue = current["nPalletId"].ToString(),
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pBoxId",
                        ParameterValue = current["cBoxId"].ToString(),
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pMNo",
                        ParameterValue = current["cMNo"].ToString(),
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pBatchNo",
                        ParameterValue = current["cBatchNo"].ToString(),
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
                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];
                            if (row != null)
                            {
                                if (row["cResultId"].ToString() == "0")
                                {
                                    MessageBox.Show("指令删除成功");
                                }
                                else
                                {
                                    MessageBox.Show(row["cResult"].ToString());
                                }
                            }
                        }
                    }
                    dataSet.Clear();
                    if (dataSet != null)
                    {
                        this.optMain = OperateType.optDelete;
                        this.OpenMainDataSet();
                        this.optMain = OperateType.optNone;
                    }
                    else
                    {
                        MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            this.OpenMainDataSet();
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmpTask));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tlb_M_New = new ToolStripButton();
            this.tlb_M_Edit = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tlb_M_Delete = new ToolStripButton();
            this.tlb_M_Undo = new ToolStripButton();
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
            this.grdList = new DataGridView();
            this.colcBNo = new DataGridViewTextBoxColumn();
            this.colcPosId = new DataGridViewTextBoxColumn();
            this.colcMNo = new DataGridViewTextBoxColumn();
            this.colnPalletId = new DataGridViewTextBoxColumn();
            this.colcBatchNo = new DataGridViewTextBoxColumn();
            this.colnQCStatus = new DataGridViewTextBoxColumn();
            this.colfQty = new DataGridViewTextBoxColumn();
            this.colfFinished = new DataGridViewTextBoxColumn();
            this.colnStatus = new DataGridViewTextBoxColumn();
            this.panel2 = new Panel();
            this.btnExit = new Button();
            this.btnTransfer = new Button();
            this.btnRefresh = new Button();
            this.btnDel = new Button();
            this.bdsMain = new BindingSource(this.components);
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.bdsMain).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Delete, this.tlb_M_Undo, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x367, 0x19);
            this.tlbMain.TabIndex = 14;
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
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Visible = false;
            this.tlb_M_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = (Image) manager.GetObject("tlb_M_Edit.Image");
            this.tlb_M_Edit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new Size(0x3d, 0x16);
            this.tlb_M_Edit.Text = "手动过账";
            this.tlb_M_Edit.Click += new EventHandler(this.btnTransfer_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.tlb_M_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = (Image) manager.GetObject("tlb_M_Delete.Image");
            this.tlb_M_Delete.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new Size(0x3d, 0x16);
            this.tlb_M_Delete.Text = "删除指令";
            this.tlb_M_Delete.Click += new EventHandler(this.btnDel_Click);
            this.tlb_M_Undo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = (Image) manager.GetObject("tlb_M_Undo.Image");
            this.tlb_M_Undo.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new Size(0x57, 0x16);
            this.tlb_M_Undo.Text = "取消未下指令";
            this.tlb_M_Undo.Visible = false;
            this.tlb_M_Undo.Click += new EventHandler(this.btnDel2_Click);
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
            this.tlb_M_Refresh.Click += new EventHandler(this.btnRefresh_Click);
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
            this.tlb_M_Exit.Click += new EventHandler(this.btnExit_Click);
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(6, 0x19);
            this.tlbSaveSysRts.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = (Image) manager.GetObject("tlbSaveSysRts.Image");
            this.tlbSaveSysRts.ImageTransparentColor = Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new Size(0x54, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2c2, 0x1d0);
            this.panel1.TabIndex = 15;
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colcBNo, this.colcPosId, this.colcMNo, this.colnPalletId, this.colcBatchNo, this.colnQCStatus, this.colfQty, this.colfFinished, this.colnStatus });
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new Point(0, 0);
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
            this.grdList.Size = new Size(0x2c2, 0x1d0);
            this.grdList.TabIndex = 2;
            this.grdList.Tag = "8";
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.Frozen = true;
            this.colcBNo.HeaderText = "单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            this.colcBNo.ToolTipText = "单号";
            this.colcPosId.DataPropertyName = "cPosId";
            this.colcPosId.HeaderText = "仓位";
            this.colcPosId.Name = "colcPosId";
            this.colcPosId.ReadOnly = true;
            this.colcPosId.ToolTipText = "仓位";
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编号";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编号";
            this.colnPalletId.DataPropertyName = "nPalletId";
            this.colnPalletId.HeaderText = "托盘号";
            this.colnPalletId.Name = "colnPalletId";
            this.colnPalletId.ReadOnly = true;
            this.colnPalletId.ToolTipText = "托盘号";
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            this.colcBatchNo.ToolTipText = "批号";
            this.colnQCStatus.DataPropertyName = "nQCStatus";
            this.colnQCStatus.HeaderText = "质检状态";
            this.colnQCStatus.Name = "colnQCStatus";
            this.colnQCStatus.ReadOnly = true;
            this.colnQCStatus.ToolTipText = "质检状态";
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "单据数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.ToolTipText = "单据数量";
            this.colfFinished.DataPropertyName = "fFinished";
            this.colfFinished.HeaderText = "实际完成数量";
            this.colfFinished.Name = "colfFinished";
            this.colfFinished.ReadOnly = true;
            this.colfFinished.ToolTipText = "实际完成数量";
            this.colnStatus.DataPropertyName = "nStatus";
            this.colnStatus.HeaderText = "执行状态";
            this.colnStatus.Name = "colnStatus";
            this.colnStatus.ReadOnly = true;
            this.colnStatus.ToolTipText = "执行状态";
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnTransfer);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0x2c2, 0x19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xa5, 0x1d0);
            this.panel2.TabIndex = 0x10;
            this.btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnExit.FlatStyle = FlatStyle.Flat;
            this.btnExit.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btnExit.ForeColor = Color.Blue;
            this.btnExit.Location = new Point(0x24, 0x171);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x66, 0x1f);
            this.btnExit.TabIndex = 0x79;
            this.btnExit.Text = "退  出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnTransfer.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnTransfer.FlatStyle = FlatStyle.Flat;
            this.btnTransfer.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btnTransfer.ForeColor = Color.Blue;
            this.btnTransfer.Location = new Point(0x24, 0xcd);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new Size(0x66, 0x1f);
            this.btnTransfer.TabIndex = 0x7b;
            this.btnTransfer.Text = "过  帐";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new EventHandler(this.btnTransfer_Click);
            this.btnRefresh.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btnRefresh.ForeColor = Color.Blue;
            this.btnRefresh.Location = new Point(0x24, 0x13c);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(0x66, 0x1f);
            this.btnRefresh.TabIndex = 0x7a;
            this.btnRefresh.Text = "刷  新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.btnDel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnDel.FlatStyle = FlatStyle.Flat;
            this.btnDel.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btnDel.ForeColor = Color.Blue;
            this.btnDel.Location = new Point(0x24, 0x103);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new Size(0x66, 0x1f);
            this.btnDel.TabIndex = 120;
            this.btnDel.Text = "删除指令";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new EventHandler(this.btnDel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x367, 0x1e9);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.MinimizeBox = false;
            base.Name = "frmpTask";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "平库任务管理";
            base.Load += new EventHandler(this.frmTask_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.grdList).EndInit();
            this.panel2.ResumeLayout(false);
            ((ISupportInitialize) this.bdsMain).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public bool OpenMainDataSet()
        {
            bool flag = false;
            this.grdList.AutoGenerateColumns = false;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            string sSql = "select * from " + this.strTbNameMain;
            string sErr = "";
            base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
            flag = sErr == "";
            if (!flag)
            {
                MessageBox.Show(sErr);
                return flag;
            }
            try
            {
                this.bdsMain.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
                this.BindDataSetToCtrls();
                flag = true;
                this.optMain = OperateType.optNone;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            return flag;
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

