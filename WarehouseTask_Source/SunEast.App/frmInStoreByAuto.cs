namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;

    public class frmInStoreByAuto : FrmSTable
    {
        private bool _IsOK = false;
        private DataRowView _PltDataRowView = null;
        private bool bDataIsOpened = false;
        private BindingSource bds_data;
        private Button btn_Cancel;
        private Button btn_Ex;
        private Button btn_MatSolutionSave;
        private Button btn_OK;
        private Button btn_Refresh;
        private CheckBox chk_BatchNoMixPut;
        private CheckBox chk_FirstEmptyPlt;
        private CheckBox chk_IsSameMatClass;
        private CheckBox chk_IsWholePackage;
        private CheckBox chk_MatMixPut;
        private ComboBox cmb_cWHId;
        private ComboBox cmb_OptGroup;
        private ComboBox cmb_PalletSpec;
        private ComboBox cmb_WArea;
        private DataGridViewTextBoxColumn col_cBNo;
        private DataGridViewTextBoxColumn col_cMName;
        private DataGridViewTextBoxColumn col_cMNo;
        private DataGridViewTextBoxColumn col_cRemark;
        private DataGridViewTextBoxColumn col_cSpec;
        private DataGridViewTextBoxColumn col_fFinished;
        private DataGridViewTextBoxColumn col_fPallet;
        private DataGridViewTextBoxColumn col_fQty;
        private DataGridViewTextBoxColumn col_fUnPallet;
        private DataGridViewTextBoxColumn col_nItem;
        private IContainer components = null;
        private DataGridView grd_Data;
        private GroupBox grp_Condition;
        private GroupBox grp_MatSolution;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label17;
        private Label label2;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl_DataCount;
        private Panel pnl_Bottom;
        private Panel pnl_RecordCount;
        private ProgressBar prg_Data;
        private string sSql = "select dtl.*,(dtl.fQty-isnull(dtl.fPallet,0)-isnull(dtl.fFinished,0)) fUnPallet,mat.cName cMName,mat.cSpec from TWB_BillIn b  left join TWB_BillInDtl dtl on b.cBNo=dtl.cBNo  left join TPC_Material mat on dtl.cMNo=mat.cMNo where b.nBClass=1 and b.cBTypeId not in ('104') and  isnull(b.bIsChecked,0)=1 and isnull(b.bIsFinished,0)=0 and  (dtl.fQty-isnull(dtl.fPallet,0)-isnull(dtl.fFinished,0)) > 0";
        private ToolTip ttpMain;
        private TextBox txt_BillNo;
        private TextBox txt_Col;
        private TextBox txt_Layer;
        private TextBox txt_MatName;
        private TextBox txt_MName;
        private TextBox txt_MNo;
        private TextBox txt_MNoQty;
        private TextBox txt_QtyBag;
        private TextBox txt_QtyPallet;
        private TextBox txt_Row;
        private TextBox txt_Spec;

        public frmInStoreByAuto()
        {
            this.InitializeComponent();
        }

        private void bds_data_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDataIsOpened && this.grp_MatSolution.Visible)
            {
                string sMNo = "";
                DataRowView current = null;
                if ((this.bds_data.Count > 0) && (this.bds_data.Current != null))
                {
                    current = (DataRowView) this.bds_data.Current;
                    if (current != null)
                    {
                        sMNo = current["cMNo"].ToString();
                        this.ShowMatSolution(sMNo);
                    }
                    else
                    {
                        this.ShowMatSolution(sMNo);
                    }
                }
                else
                {
                    this.ShowMatSolution(sMNo);
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_Ex_Click(object sender, EventArgs e)
        {
            if (this.grp_MatSolution.Visible)
            {
                this.grp_MatSolution.Visible = false;
                this.btn_Ex.Text = ">>";
                this.ttpMain.SetToolTip(this.btn_Ex, "点击，对物料自动入库配盘策略进行设置");
            }
            else
            {
                this.grp_MatSolution.Visible = true;
                this.btn_Ex.Text = "<<";
                this.ttpMain.SetToolTip(this.btn_Ex, "点击，收回“物料自动入库配盘策略面板”");
                this.bds_data_PositionChanged(null, null);
            }
        }

        private void btn_MatSolutionSave_Click(object sender, EventArgs e)
        {
            this.SaveMatSolution();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.grd_Data.Rows.Count == 0)
            {
                MessageBox.Show("对不起，无数据可配盘！");
            }
            else if (this.grd_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("对不起，无选择待配盘数据！");
            }
            else
            {
                int count = 0;
                count = this.grd_Data.SelectedRows.Count;
                this.prg_Data.Maximum = count;
                this.prg_Data.Minimum = 0;
                this.prg_Data.Value = 0;
                this.prg_Data.Visible = true;
                string pWHId = "";
                if ((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.SelectedValue != null))
                {
                    pWHId = this.cmb_cWHId.SelectedValue.ToString();
                }
                else
                {
                    MessageBox.Show("仓库不能为空！");
                    this.cmb_cWHId.Focus();
                    return;
                }
                int pIsFirstEmptAfterHalfPlt = 0;
                if (this.chk_FirstEmptyPlt.Checked)
                {
                    pIsFirstEmptAfterHalfPlt = 1;
                }
                string pOptGroup = "";
                if ((this.cmb_OptGroup.Text.Trim() != "") && (this.cmb_OptGroup.SelectedValue != null))
                {
                    pOptGroup = this.cmb_OptGroup.SelectedValue.ToString();
                }
                else
                {
                    MessageBox.Show("对不起，操作台组别不能为空！");
                    this.cmb_OptGroup.Focus();
                    return;
                }
                int pPltMatQty = 0;
                if ((this.txt_MNoQty.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_MNoQty.Text.Trim()))
                {
                    pPltMatQty = int.Parse(this.txt_MNoQty.Text.Trim());
                }
                else
                {
                    MessageBox.Show("请输入正确的数值！");
                    this.txt_MNoQty.SelectAll();
                    this.txt_MNoQty.Focus();
                    return;
                }
                int pRow = 0;
                if ((this.txt_Row.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_Row.Text.Trim()))
                {
                    pRow = int.Parse(this.txt_Row.Text.Trim());
                }
                else
                {
                    MessageBox.Show("请输入正确的数值！");
                    this.txt_Row.SelectAll();
                    this.txt_Row.Focus();
                    return;
                }
                int pCol = 0;
                if ((this.txt_Col.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_Col.Text.Trim()))
                {
                    pCol = int.Parse(this.txt_Col.Text.Trim());
                }
                else
                {
                    MessageBox.Show("请输入正确的数值！");
                    this.txt_Col.SelectAll();
                    this.txt_Col.Focus();
                    return;
                }
                int pLayer = 0;
                if ((this.txt_Layer.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_Layer.Text.Trim()))
                {
                    pLayer = int.Parse(this.txt_Layer.Text.Trim());
                }
                else
                {
                    MessageBox.Show("请输入正确的数值！");
                    this.txt_Layer.SelectAll();
                    this.txt_Layer.Focus();
                    return;
                }
                count = 0;
                foreach (DataGridViewRow row in this.grd_Data.SelectedRows)
                {
                    string sErr = "";
                    string pBNo = row.Cells["col_cBNo"].Value.ToString();
                    string pItem = row.Cells["col_nItem"].Value.ToString();
                    if (DBFuns.sp_Pack_DoPltDtlInAuto(base.AppInformation.SvrSocket, base.UserInformation.UserId, "WMS", pBNo, pItem, pIsFirstEmptAfterHalfPlt, pOptGroup, pPltMatQty, pWHId, pRow, pCol, pLayer, out sErr).Trim() == "0")
                    {
                        count++;
                    }
                    this.prg_Data.Value++;
                }
                MessageBox.Show("成功自动配盘了 " + count.ToString() + " 条数据，请注意查看需要手动配盘的数据！");
                this.OpenDataList();
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.OpenDataList();
        }

        private void ClearMatSolution()
        {
            this.txt_MNo.Text = "";
            this.txt_MName.Text = "";
            this.txt_Spec.Text = "";
            this.txt_QtyPallet.Text = "0";
            this.txt_QtyBag.Text = "0";
            this.chk_IsWholePackage.Checked = false;
            this.chk_MatMixPut.Checked = true;
            this.chk_IsSameMatClass.Checked = true;
            this.cmb_PalletSpec.SelectedIndex = -1;
            this.cmb_WArea.SelectedIndex = -1;
            this.chk_BatchNoMixPut.Checked = true;
        }

        private void cmb_cWHId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.SelectedValue != null))
            {
                this.LoadCombOptGroup(this.cmb_cWHId.SelectedValue.ToString());
            }
            else
            {
                this.LoadCombOptGroup("");
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

        private void frmInStoreByAuto_Load(object sender, EventArgs e)
        {
            this.grd_Data.AutoGenerateColumns = false;
            this.LoadCombOptGroup("");
            this.LoadCombWare(this.cmb_cWHId);
            this.LoadCombWAreaList("", this.cmb_WArea);
            this.LoadCombPalletSpec(this.cmb_PalletSpec);
            this.OpenDataList();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.pnl_Bottom = new Panel();
            this.prg_Data = new ProgressBar();
            this.btn_OK = new Button();
            this.btn_Cancel = new Button();
            this.grp_Condition = new GroupBox();
            this.txt_MNoQty = new TextBox();
            this.label8 = new Label();
            this.cmb_OptGroup = new ComboBox();
            this.label4 = new Label();
            this.txt_Layer = new TextBox();
            this.label23 = new Label();
            this.txt_Col = new TextBox();
            this.label24 = new Label();
            this.txt_Row = new TextBox();
            this.label25 = new Label();
            this.chk_FirstEmptyPlt = new CheckBox();
            this.label17 = new Label();
            this.cmb_cWHId = new ComboBox();
            this.bds_data = new BindingSource(this.components);
            this.grp_MatSolution = new GroupBox();
            this.txt_Spec = new TextBox();
            this.label11 = new Label();
            this.txt_MName = new TextBox();
            this.label10 = new Label();
            this.txt_MNo = new TextBox();
            this.label9 = new Label();
            this.chk_IsSameMatClass = new CheckBox();
            this.btn_MatSolutionSave = new Button();
            this.cmb_WArea = new ComboBox();
            this.label7 = new Label();
            this.cmb_PalletSpec = new ComboBox();
            this.label5 = new Label();
            this.txt_QtyBag = new TextBox();
            this.label6 = new Label();
            this.chk_IsWholePackage = new CheckBox();
            this.txt_QtyPallet = new TextBox();
            this.label1 = new Label();
            this.chk_BatchNoMixPut = new CheckBox();
            this.chk_MatMixPut = new CheckBox();
            this.grd_Data = new DataGridView();
            this.col_cBNo = new DataGridViewTextBoxColumn();
            this.col_nItem = new DataGridViewTextBoxColumn();
            this.col_cMNo = new DataGridViewTextBoxColumn();
            this.col_cMName = new DataGridViewTextBoxColumn();
            this.col_cSpec = new DataGridViewTextBoxColumn();
            this.col_fQty = new DataGridViewTextBoxColumn();
            this.col_fPallet = new DataGridViewTextBoxColumn();
            this.col_fFinished = new DataGridViewTextBoxColumn();
            this.col_fUnPallet = new DataGridViewTextBoxColumn();
            this.col_cRemark = new DataGridViewTextBoxColumn();
            this.pnl_RecordCount = new Panel();
            this.label3 = new Label();
            this.txt_BillNo = new TextBox();
            this.txt_MatName = new TextBox();
            this.btn_Ex = new Button();
            this.btn_Refresh = new Button();
            this.label26 = new Label();
            this.lbl_DataCount = new Label();
            this.label2 = new Label();
            this.ttpMain = new ToolTip(this.components);
            this.pnl_Bottom.SuspendLayout();
            this.grp_Condition.SuspendLayout();
            ((ISupportInitialize) this.bds_data).BeginInit();
            this.grp_MatSolution.SuspendLayout();
            ((ISupportInitialize) this.grd_Data).BeginInit();
            this.pnl_RecordCount.SuspendLayout();
            base.SuspendLayout();
            this.pnl_Bottom.Controls.Add(this.prg_Data);
            this.pnl_Bottom.Controls.Add(this.btn_OK);
            this.pnl_Bottom.Controls.Add(this.btn_Cancel);
            this.pnl_Bottom.Dock = DockStyle.Bottom;
            this.pnl_Bottom.Location = new Point(0, 0x1f7);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new Size(0x3bb, 0x33);
            this.pnl_Bottom.TabIndex = 13;
            this.prg_Data.Location = new Point(12, 0x1f);
            this.prg_Data.Name = "prg_Data";
            this.prg_Data.Size = new Size(0x3a3, 0x11);
            this.prg_Data.TabIndex = 10;
            this.prg_Data.Visible = false;
            this.btn_OK.Location = new Point(0x102, 6);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 8;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Cancel.Location = new Point(0x1e5, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            this.grp_Condition.Controls.Add(this.txt_MNoQty);
            this.grp_Condition.Controls.Add(this.label8);
            this.grp_Condition.Controls.Add(this.cmb_OptGroup);
            this.grp_Condition.Controls.Add(this.label4);
            this.grp_Condition.Controls.Add(this.txt_Layer);
            this.grp_Condition.Controls.Add(this.label23);
            this.grp_Condition.Controls.Add(this.txt_Col);
            this.grp_Condition.Controls.Add(this.label24);
            this.grp_Condition.Controls.Add(this.txt_Row);
            this.grp_Condition.Controls.Add(this.label25);
            this.grp_Condition.Controls.Add(this.chk_FirstEmptyPlt);
            this.grp_Condition.Controls.Add(this.label17);
            this.grp_Condition.Controls.Add(this.cmb_cWHId);
            this.grp_Condition.Dock = DockStyle.Bottom;
            this.grp_Condition.Location = new Point(0, 0x1cf);
            this.grp_Condition.Name = "grp_Condition";
            this.grp_Condition.Size = new Size(0x3bb, 40);
            this.grp_Condition.TabIndex = 0x10;
            this.grp_Condition.TabStop = false;
            this.txt_MNoQty.Location = new Point(0x21a, 13);
            this.txt_MNoQty.Name = "txt_MNoQty";
            this.txt_MNoQty.Size = new Size(0x33, 0x15);
            this.txt_MNoQty.TabIndex = 120;
            this.txt_MNoQty.Text = "5";
            this.label8.Location = new Point(0x1df, 11);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 0x1a);
            this.label8.TabIndex = 0x79;
            this.label8.Text = "每盘物料混放数";
            this.cmb_OptGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_OptGroup.FormattingEnabled = true;
            this.cmb_OptGroup.Location = new Point(0x173, 13);
            this.cmb_OptGroup.Name = "cmb_OptGroup";
            this.cmb_OptGroup.Size = new Size(0x56, 20);
            this.cmb_OptGroup.TabIndex = 0x77;
            this.cmb_OptGroup.Tag = "101";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x130, 15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 0x6f;
            this.label4.Text = "操作台组别";
            this.txt_Layer.Location = new Point(0x307, 13);
            this.txt_Layer.Name = "txt_Layer";
            this.txt_Layer.Size = new Size(0x18, 0x15);
            this.txt_Layer.TabIndex = 0x6c;
            this.txt_Layer.Text = "0";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x2ea, 0x11);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x1d, 12);
            this.label23.TabIndex = 0x6d;
            this.label23.Text = "层号";
            this.txt_Col.Location = new Point(0x2d2, 13);
            this.txt_Col.Name = "txt_Col";
            this.txt_Col.Size = new Size(0x18, 0x15);
            this.txt_Col.TabIndex = 0x6a;
            this.txt_Col.Text = "0";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(0x2b5, 0x11);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x1d, 12);
            this.label24.TabIndex = 0x6b;
            this.label24.Text = "列号";
            this.txt_Row.Location = new Point(0x29d, 13);
            this.txt_Row.Name = "txt_Row";
            this.txt_Row.Size = new Size(0x18, 0x15);
            this.txt_Row.TabIndex = 0x68;
            this.txt_Row.Text = "0";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(640, 0x11);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x1d, 12);
            this.label25.TabIndex = 0x69;
            this.label25.Text = "排号";
            this.chk_FirstEmptyPlt.AutoSize = true;
            this.chk_FirstEmptyPlt.Location = new Point(0x18, 13);
            this.chk_FirstEmptyPlt.Name = "chk_FirstEmptyPlt";
            this.chk_FirstEmptyPlt.Size = new Size(0x60, 0x10);
            this.chk_FirstEmptyPlt.TabIndex = 0x67;
            this.chk_FirstEmptyPlt.Text = "先空盘后半盘";
            this.chk_FirstEmptyPlt.UseVisualStyleBackColor = true;
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x8b, 15);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1d, 12);
            this.label17.TabIndex = 0x5e;
            this.label17.Text = "仓库";
            this.cmb_cWHId.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0xb0, 11);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(0x7a, 20);
            this.cmb_cWHId.TabIndex = 0x5d;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.SelectedIndexChanged += new EventHandler(this.cmb_cWHId_SelectedIndexChanged);
            this.bds_data.PositionChanged += new EventHandler(this.bds_data_PositionChanged);
            this.grp_MatSolution.BackColor = Color.Tan;
            this.grp_MatSolution.Controls.Add(this.txt_Spec);
            this.grp_MatSolution.Controls.Add(this.label11);
            this.grp_MatSolution.Controls.Add(this.txt_MName);
            this.grp_MatSolution.Controls.Add(this.label10);
            this.grp_MatSolution.Controls.Add(this.txt_MNo);
            this.grp_MatSolution.Controls.Add(this.label9);
            this.grp_MatSolution.Controls.Add(this.chk_IsSameMatClass);
            this.grp_MatSolution.Controls.Add(this.btn_MatSolutionSave);
            this.grp_MatSolution.Controls.Add(this.cmb_WArea);
            this.grp_MatSolution.Controls.Add(this.label7);
            this.grp_MatSolution.Controls.Add(this.cmb_PalletSpec);
            this.grp_MatSolution.Controls.Add(this.label5);
            this.grp_MatSolution.Controls.Add(this.txt_QtyBag);
            this.grp_MatSolution.Controls.Add(this.label6);
            this.grp_MatSolution.Controls.Add(this.chk_IsWholePackage);
            this.grp_MatSolution.Controls.Add(this.txt_QtyPallet);
            this.grp_MatSolution.Controls.Add(this.label1);
            this.grp_MatSolution.Controls.Add(this.chk_BatchNoMixPut);
            this.grp_MatSolution.Controls.Add(this.chk_MatMixPut);
            this.grp_MatSolution.Dock = DockStyle.Bottom;
            this.grp_MatSolution.Location = new Point(0, 0x165);
            this.grp_MatSolution.Name = "grp_MatSolution";
            this.grp_MatSolution.Size = new Size(0x3bb, 0x6a);
            this.grp_MatSolution.TabIndex = 0x11;
            this.grp_MatSolution.TabStop = false;
            this.grp_MatSolution.Text = "物料自动入库配盘策略";
            this.grp_MatSolution.Visible = false;
            this.txt_Spec.Location = new Point(0x2b7, 0x11);
            this.txt_Spec.Name = "txt_Spec";
            this.txt_Spec.ReadOnly = true;
            this.txt_Spec.Size = new Size(0xac, 0x15);
            this.txt_Spec.TabIndex = 0x8a;
            this.txt_Spec.Text = "999";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x282, 0x11);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x8b;
            this.label11.Text = "物料规格";
            this.txt_MName.Location = new Point(0x14b, 0x11);
            this.txt_MName.Name = "txt_MName";
            this.txt_MName.ReadOnly = true;
            this.txt_MName.Size = new Size(0x134, 0x15);
            this.txt_MName.TabIndex = 0x88;
            this.txt_MName.Text = "999";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x100, 0x11);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 0x89;
            this.label10.Text = "物料名称";
            this.txt_MNo.Location = new Point(0x3f, 20);
            this.txt_MNo.Name = "txt_MNo";
            this.txt_MNo.ReadOnly = true;
            this.txt_MNo.Size = new Size(0xac, 0x15);
            this.txt_MNo.TabIndex = 0x86;
            this.txt_MNo.Text = "999";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(10, 20);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 0x87;
            this.label9.Text = "物料编码";
            this.chk_IsSameMatClass.AutoSize = true;
            this.chk_IsSameMatClass.Location = new Point(0x284, 0x31);
            this.chk_IsSameMatClass.Name = "chk_IsSameMatClass";
            this.chk_IsSameMatClass.Size = new Size(0x84, 0x10);
            this.chk_IsSameMatClass.TabIndex = 0x85;
            this.chk_IsSameMatClass.Text = "仅按同类型物料混放";
            this.chk_IsSameMatClass.UseVisualStyleBackColor = true;
            this.btn_MatSolutionSave.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_MatSolutionSave.ForeColor = SystemColors.ActiveCaption;
            this.btn_MatSolutionSave.Location = new Point(0x285, 0x4e);
            this.btn_MatSolutionSave.Name = "btn_MatSolutionSave";
            this.btn_MatSolutionSave.Size = new Size(130, 0x17);
            this.btn_MatSolutionSave.TabIndex = 0x84;
            this.btn_MatSolutionSave.Text = "保存";
            this.ttpMain.SetToolTip(this.btn_MatSolutionSave, "保存物料策略");
            this.btn_MatSolutionSave.UseVisualStyleBackColor = true;
            this.btn_MatSolutionSave.Click += new EventHandler(this.btn_MatSolutionSave_Click);
            this.cmb_WArea.FormattingEnabled = true;
            this.cmb_WArea.Location = new Point(0x206, 0x4e);
            this.cmb_WArea.Name = "cmb_WArea";
            this.cmb_WArea.Size = new Size(0x79, 20);
            this.cmb_WArea.TabIndex = 130;
            this.cmb_WArea.Tag = "101";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x1e3, 0x4e);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 12);
            this.label7.TabIndex = 0x83;
            this.label7.Text = "货区";
            this.cmb_PalletSpec.FormattingEnabled = true;
            this.cmb_PalletSpec.Location = new Point(330, 0x4e);
            this.cmb_PalletSpec.Name = "cmb_PalletSpec";
            this.cmb_PalletSpec.Size = new Size(0x70, 20);
            this.cmb_PalletSpec.TabIndex = 0x80;
            this.cmb_PalletSpec.Tag = "101";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xfd, 0x4e);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x81;
            this.label5.Text = "托盘规格";
            this.txt_QtyBag.Location = new Point(330, 0x31);
            this.txt_QtyBag.Name = "txt_QtyBag";
            this.txt_QtyBag.Size = new Size(0x70, 0x15);
            this.txt_QtyBag.TabIndex = 0x7e;
            this.txt_QtyBag.Text = "999";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xfd, 0x31);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x47, 12);
            this.label6.TabIndex = 0x7f;
            this.label6.Text = "每件/袋数量";
            this.chk_IsWholePackage.AutoSize = true;
            this.chk_IsWholePackage.Location = new Point(150, 0x31);
            this.chk_IsWholePackage.Name = "chk_IsWholePackage";
            this.chk_IsWholePackage.Size = new Size(90, 0x10);
            this.chk_IsWholePackage.TabIndex = 0x7d;
            this.chk_IsWholePackage.Text = "是否箱/袋装";
            this.chk_IsWholePackage.UseVisualStyleBackColor = true;
            this.txt_QtyPallet.Location = new Point(0x3d, 0x31);
            this.txt_QtyPallet.Name = "txt_QtyPallet";
            this.txt_QtyPallet.Size = new Size(0x53, 0x15);
            this.txt_QtyPallet.TabIndex = 0x7b;
            this.txt_QtyPallet.Text = "999";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x31);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x7c;
            this.label1.Text = "满盘数量";
            this.chk_BatchNoMixPut.AutoSize = true;
            this.chk_BatchNoMixPut.Location = new Point(11, 0x4e);
            this.chk_BatchNoMixPut.Name = "chk_BatchNoMixPut";
            this.chk_BatchNoMixPut.Size = new Size(0x84, 0x10);
            this.chk_BatchNoMixPut.TabIndex = 0x68;
            this.chk_BatchNoMixPut.Text = "同物料不同批次混放";
            this.chk_BatchNoMixPut.UseVisualStyleBackColor = true;
            this.chk_MatMixPut.AutoSize = true;
            this.chk_MatMixPut.Checked = true;
            this.chk_MatMixPut.CheckState = CheckState.Checked;
            this.chk_MatMixPut.Enabled = false;
            this.chk_MatMixPut.Location = new Point(0x1e5, 0x31);
            this.chk_MatMixPut.Name = "chk_MatMixPut";
            this.chk_MatMixPut.Size = new Size(0x60, 0x10);
            this.chk_MatMixPut.TabIndex = 0x67;
            this.chk_MatMixPut.Text = "不同物料混放";
            this.chk_MatMixPut.UseVisualStyleBackColor = true;
            this.grd_Data.AllowUserToAddRows = false;
            this.grd_Data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_Data.Columns.AddRange(new DataGridViewColumn[] { this.col_cBNo, this.col_nItem, this.col_cMNo, this.col_cMName, this.col_cSpec, this.col_fQty, this.col_fPallet, this.col_fFinished, this.col_fUnPallet, this.col_cRemark });
            this.grd_Data.Dock = DockStyle.Fill;
            this.grd_Data.Location = new Point(0, 0);
            this.grd_Data.Name = "grd_Data";
            this.grd_Data.ReadOnly = true;
            this.grd_Data.RowHeadersVisible = false;
            this.grd_Data.RowTemplate.Height = 0x17;
            this.grd_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grd_Data.Size = new Size(0x3bb, 0x147);
            this.grd_Data.TabIndex = 20;
            this.col_cBNo.DataPropertyName = "cBNo";
            this.col_cBNo.HeaderText = "单号";
            this.col_cBNo.Name = "col_cBNo";
            this.col_cBNo.ReadOnly = true;
            this.col_cBNo.Width = 80;
            this.col_nItem.DataPropertyName = "nItem";
            this.col_nItem.HeaderText = "明细序号";
            this.col_nItem.Name = "col_nItem";
            this.col_nItem.ReadOnly = true;
            this.col_nItem.Width = 0x41;
            this.col_cMNo.DataPropertyName = "cMNo";
            this.col_cMNo.HeaderText = "物料编码";
            this.col_cMNo.Name = "col_cMNo";
            this.col_cMNo.ReadOnly = true;
            this.col_cMName.DataPropertyName = "cMName";
            this.col_cMName.HeaderText = "物料名称";
            this.col_cMName.Name = "col_cMName";
            this.col_cMName.ReadOnly = true;
            this.col_cSpec.DataPropertyName = "cSpec";
            this.col_cSpec.HeaderText = "规格";
            this.col_cSpec.Name = "col_cSpec";
            this.col_cSpec.ReadOnly = true;
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "单据数量";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            this.col_fPallet.DataPropertyName = "fPallet";
            this.col_fPallet.HeaderText = "已配盘数量";
            this.col_fPallet.Name = "col_fPallet";
            this.col_fPallet.ReadOnly = true;
            this.col_fFinished.DataPropertyName = "fFinished";
            this.col_fFinished.HeaderText = "已完成数";
            this.col_fFinished.Name = "col_fFinished";
            this.col_fFinished.ReadOnly = true;
            this.col_fUnPallet.DataPropertyName = "fUnPallet";
            this.col_fUnPallet.HeaderText = "待配盘数";
            this.col_fUnPallet.Name = "col_fUnPallet";
            this.col_fUnPallet.ReadOnly = true;
            this.col_cRemark.DataPropertyName = "cRemark";
            this.col_cRemark.HeaderText = "备注";
            this.col_cRemark.Name = "col_cRemark";
            this.col_cRemark.ReadOnly = true;
            this.pnl_RecordCount.Controls.Add(this.label3);
            this.pnl_RecordCount.Controls.Add(this.txt_BillNo);
            this.pnl_RecordCount.Controls.Add(this.txt_MatName);
            this.pnl_RecordCount.Controls.Add(this.btn_Ex);
            this.pnl_RecordCount.Controls.Add(this.btn_Refresh);
            this.pnl_RecordCount.Controls.Add(this.label26);
            this.pnl_RecordCount.Controls.Add(this.lbl_DataCount);
            this.pnl_RecordCount.Controls.Add(this.label2);
            this.pnl_RecordCount.Dock = DockStyle.Bottom;
            this.pnl_RecordCount.Location = new Point(0, 0x147);
            this.pnl_RecordCount.Name = "pnl_RecordCount";
            this.pnl_RecordCount.Size = new Size(0x3bb, 30);
            this.pnl_RecordCount.TabIndex = 0x13;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x116, 7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 0x54;
            this.label3.Text = "物料：";
            this.txt_BillNo.Location = new Point(0x9d, 4);
            this.txt_BillNo.Name = "txt_BillNo";
            this.txt_BillNo.Size = new Size(0x73, 0x15);
            this.txt_BillNo.TabIndex = 0x53;
            this.ttpMain.SetToolTip(this.txt_BillNo, "按单号进行模糊查询");
            this.txt_MatName.Location = new Point(0x143, 3);
            this.txt_MatName.Name = "txt_MatName";
            this.txt_MatName.Size = new Size(0x74, 0x15);
            this.txt_MatName.TabIndex = 0x52;
            this.ttpMain.SetToolTip(this.txt_MatName, "按物料编码、名称、规格 进行查询");
            this.btn_Ex.Location = new Point(0x24f, 3);
            this.btn_Ex.Name = "btn_Ex";
            this.btn_Ex.Size = new Size(0x21, 0x17);
            this.btn_Ex.TabIndex = 0x51;
            this.btn_Ex.Text = ">>";
            this.ttpMain.SetToolTip(this.btn_Ex, "点击，对物料自动入库配盘策略进行设置");
            this.btn_Ex.UseVisualStyleBackColor = true;
            this.btn_Ex.Click += new EventHandler(this.btn_Ex_Click);
            this.btn_Refresh.Location = new Point(0x1bd, 2);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new Size(0x4b, 0x17);
            this.btn_Refresh.TabIndex = 80;
            this.btn_Refresh.Text = "刷新";
            this.ttpMain.SetToolTip(this.btn_Refresh, "根据条件进行刷新数据");
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new EventHandler(this.btn_Refresh_Click);
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0x79, 7);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x29, 12);
            this.label26.TabIndex = 0x4e;
            this.label26.Text = "单号：";
            this.lbl_DataCount.AutoSize = true;
            this.lbl_DataCount.Location = new Point(80, 7);
            this.lbl_DataCount.Name = "lbl_DataCount";
            this.lbl_DataCount.Size = new Size(11, 12);
            this.lbl_DataCount.TabIndex = 1;
            this.lbl_DataCount.Text = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据条数：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x3bb, 0x22a);
            base.Controls.Add(this.grd_Data);
            base.Controls.Add(this.pnl_RecordCount);
            base.Controls.Add(this.grp_MatSolution);
            base.Controls.Add(this.grp_Condition);
            base.Controls.Add(this.pnl_Bottom);
            base.MinimizeBox = false;
            base.Name = "frmInStoreByAuto";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "自动入库";
            base.Load += new EventHandler(this.frmInStoreByAuto_Load);
            this.pnl_Bottom.ResumeLayout(false);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            ((ISupportInitialize) this.bds_data).EndInit();
            this.grp_MatSolution.ResumeLayout(false);
            this.grp_MatSolution.PerformLayout();
            ((ISupportInitialize) this.grd_Data).EndInit();
            this.pnl_RecordCount.ResumeLayout(false);
            this.pnl_RecordCount.PerformLayout();
            base.ResumeLayout(false);
        }

        private void LoadCombOptGroup(string sWHId)
        {
            string sErr = "";
            string sSql = "select distinct cGroupName from TECS_HSInfo ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + sWHId.Trim() + "'";
            }
            DataSet set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TECS_HSInfo", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                this.cmb_OptGroup.DisplayMember = "cGroupName";
                this.cmb_OptGroup.ValueMember = "cGroupName";
                this.cmb_OptGroup.DataSource = set.Tables["TECS_HSInfo"];
            }
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
            cmbX.ValueMember = "cWHId";
            cmbX.DisplayMember = "cName";
            if (set != null)
            {
                DataTable table = set.Tables["TWC_WareHouse"];
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

        private void OpenDataList()
        {
            string sErr = "";
            StringBuilder builder = new StringBuilder(this.sSql);
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(" and isnull(b.cChecker,'') in (select cName from TPB_User where cDeptId='" + base.UserInformation.DeptId + "')");
            }
            if (this.txt_BillNo.Text.Trim() != "")
            {
                builder.Append(" and b.cBNo like '%" + this.txt_BillNo.Text.Trim() + "%'");
            }
            if (this.txt_MatName.Text.Trim() != "")
            {
                builder.Append(" and ( dtl.cMNo like '%" + this.txt_MatName.Text.Trim() + "%' or isnull(mat.cName,'') like '%" + this.txt_MatName.Text.Trim() + "%' or isnull(mat.cWBJM,'') like '%" + this.txt_MatName.Text.Trim() + "%' ");
                builder.Append(" or isnull(mat.cPYJM,'') like '%" + this.txt_MatName.Text.Trim() + "%' or isnull(mat.cSpec,'') like '%" + this.txt_MatName.Text.Trim() + "%'");
                builder.Append(")");
            }
            DataSet set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "tbBillDtl", 0, 0, "", out sErr);
            if (((set != null) && (sErr.Trim() != "")) && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                this.bDataIsOpened = false;
                this.bds_data.DataSource = set.Tables["tbBillDtl"];
                this.grd_Data.DataSource = this.bds_data;
                this.bDataIsOpened = true;
                this.lbl_DataCount.Text = this.bds_data.Count.ToString();
            }
        }

        private void SaveMatSolution()
        {
            if (this.txt_MNo.Text.Trim() == "")
            {
                MessageBox.Show("无物料数据可保存！");
            }
            else if (!((this.txt_QtyBag.Text.Trim() != "") && FrmSTable.IsNumberic(this.txt_QtyBag.Text.Trim())))
            {
                MessageBox.Show("请输入正确的数量值！");
                this.txt_QtyBag.SelectAll();
                this.txt_QtyBag.Focus();
            }
            else if (!((this.txt_QtyPallet.Text.Trim() != "") && FrmSTable.IsNumberic(this.txt_QtyPallet.Text.Trim())))
            {
                MessageBox.Show("请输入正确的数量值！");
                this.txt_QtyPallet.SelectAll();
                this.txt_QtyPallet.Focus();
            }
            else
            {
                StringBuilder builder = new StringBuilder("");
                string sErr = "";
                builder.Append(" update TPC_Material set ");
                builder.Append("  fQtyBox=" + this.txt_QtyPallet.Text.Trim() + " , fPackageQty=" + this.txt_QtyBag.Text.Trim());
                if ((this.cmb_PalletSpec.Text.Trim() != "") && (this.cmb_PalletSpec.SelectedValue != null))
                {
                    builder.Append(" , cPalletSpecId='" + this.cmb_PalletSpec.SelectedValue.ToString() + "'");
                }
                if ((this.cmb_WArea.Text.Trim() != "") && (this.cmb_WArea.SelectedValue != null))
                {
                    builder.Append(" , cAreaId='" + this.cmb_WArea.SelectedValue.ToString() + "'");
                }
                if (this.chk_MatMixPut.Checked)
                {
                    builder.Append(" , bIsMixPalce= 1");
                }
                else
                {
                    builder.Append(" , bIsMixPalce= 0");
                }
                if (this.chk_IsWholePackage.Checked)
                {
                    builder.Append(" , bIsPackage= 1");
                }
                else
                {
                    builder.Append(" , bIsPackage= 0");
                }
                if (this.chk_BatchNoMixPut.Checked)
                {
                    builder.Append(" , bIsMixBatchNo= 1");
                }
                else
                {
                    builder.Append(" , bIsMixBatchNo= 0");
                }
                if (this.chk_IsSameMatClass.Checked)
                {
                    builder.Append(" , bIsSameMatClassIn= 1");
                }
                else
                {
                    builder.Append(" , bIsSameMatClassIn= 0");
                }
                builder.Append(" where cMNo='" + this.txt_MNo.Text.Trim() + "'");
                if (!DBFuns.DoExecSql(base.AppInformation.SvrSocket, builder.ToString(), "", out sErr))
                {
                    MessageBox.Show("保存物料自动入库策略信息失败：" + sErr);
                }
                else
                {
                    MessageBox.Show("保存物料自动入库策略信息成功！");
                }
            }
        }

        private void ShowMatSolution(string sMNo)
        {
            this.txt_MNo.Text = "";
            this.txt_MName.Text = "";
            this.txt_Spec.Text = "";
            this.txt_QtyPallet.Text = "0";
            this.txt_QtyBag.Text = "0";
            this.chk_IsWholePackage.Checked = false;
            this.chk_MatMixPut.Checked = true;
            this.chk_IsSameMatClass.Checked = true;
            this.cmb_PalletSpec.SelectedIndex = -1;
            this.cmb_WArea.SelectedIndex = -1;
            this.chk_BatchNoMixPut.Checked = true;
            if (sMNo.Trim() != "")
            {
                string sErr = "";
                string sSql = "select * from TPC_Material where cMNo='" + sMNo.Trim() + "' ";
                DataSet set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TPC_Material", 0, 0, "", out sErr);
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    DataTable table = set.Tables["TPC_Material"];
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        DataRow row = table.Rows[0];
                        this.txt_MNo.Text = row["cMNo"].ToString();
                        this.txt_MName.Text = row["cName"].ToString();
                        if (row["cSpec"] != null)
                        {
                            this.txt_Spec.Text = row["cSpec"].ToString();
                        }
                        if (row["fQtyBox"] != null)
                        {
                            this.txt_QtyPallet.Text = row["fQtyBox"].ToString();
                        }
                        else
                        {
                            this.txt_QtyPallet.Text = "0";
                        }
                        if (row["fPackageQty"] != null)
                        {
                            this.txt_QtyBag.Text = row["fPackageQty"].ToString();
                        }
                        else
                        {
                            this.txt_QtyBag.Text = "0";
                        }
                        if ((row["cAreaId"] != null) && (row["cAreaId"].ToString() != ""))
                        {
                            this.cmb_WArea.SelectedValue = row["cAreaId"].ToString();
                        }
                        if ((row["cPalletSpecId"] != null) && (row["cPalletSpecId"].ToString() != ""))
                        {
                            this.cmb_PalletSpec.SelectedValue = row["cPalletSpecId"].ToString();
                        }
                        if ((row["bIsMixPalce"] != null) && (row["bIsMixPalce"].ToString() != ""))
                        {
                            this.chk_MatMixPut.Checked = row["bIsMixPalce"].ToString() == "1";
                        }
                        if ((row["bIsPackage"] != null) && (row["bIsPackage"].ToString() != ""))
                        {
                            this.chk_IsWholePackage.Checked = row["bIsPackage"].ToString() == "1";
                        }
                        if ((row["bIsSameMatClassIn"] != null) && (row["bIsSameMatClassIn"].ToString() != ""))
                        {
                            this.chk_IsSameMatClass.Checked = row["bIsSameMatClassIn"].ToString() == "1";
                        }
                        if ((row["bIsMixBatchNo"] != null) && (row["bIsMixBatchNo"].ToString() != ""))
                        {
                            this.chk_BatchNoMixPut.Checked = row["bIsMixBatchNo"].ToString() == "1";
                        }
                    }
                    if (set != null)
                    {
                        set.Clear();
                    }
                }
            }
        }

        public bool IsOK
        {
            get
            {
                return this._IsOK;
            }
            set
            {
                this._IsOK = value;
            }
        }

        public DataRowView PltDataRowView
        {
            get
            {
                return this._PltDataRowView;
            }
            set
            {
                this._PltDataRowView = value;
            }
        }
    }
}

