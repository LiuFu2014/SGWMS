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

    public class frmOutStoreByAuto : FrmSTable
    {
        private bool _IsOK = false;
        private DataRowView _PltDataRowView = null;
        private Button btn_Cancel;
        private Button btn_OK;
        private Button btn_Refresh;
        private CheckBox chk_EmptyAsWholeOut;
        private CheckBox chk_FirstInAndOut;
        private CheckBox chk_SameBatchNo;
        private ComboBox cmb_cAreaId;
        private ComboBox cmb_cWHId;
        private ComboBox cmb_OptGroup;
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
        private Label label1;
        private Label label17;
        private Label label20;
        private Label label26;
        private Label label3;
        private Label label4;
        private Label lbl_DataCount;
        private Panel pnl_Bottom;
        private Panel pnl_RecordCount;
        private ProgressBar prg_Data;
        private string sSql = "select dtl.*,(dtl.fQty-isnull(dtl.fPallet,0)-isnull(dtl.fFinished,0)) fUnPallet,mat.cName cMName,mat.cSpec from TWB_BillIn b  left join TWB_BillInDtl dtl on b.cBNo=dtl.cBNo  left join TPC_Material mat on dtl.cMNo=mat.cMNo where b.nBClass=2 and b.cBTypeId not in ('205') and  isnull(b.bIsChecked,0)=1 and isnull(b.bIsFinished,0)=0 and  (dtl.fQty-isnull(dtl.fPallet,0)-isnull(dtl.fFinished,0)) > 0";
        private TextBox txt_BillNo;
        private TextBox txt_MatName;

        public frmOutStoreByAuto()
        {
            this.InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this._IsOK = false;
            base.Close();
        }

        private void btn_Cancel_Click_1(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string pWHId = "";
            string pAreaId = "";
            string pOptGroup = "";
            int pIsSameBatchNo = 0;
            int pIsEmptAsWholeOut = 0;
            if (((this.cmb_cWHId.Text.Trim() == "") || (this.cmb_cWHId.SelectedValue == null)) || (this.cmb_cWHId.SelectedValue.ToString().Trim() == ""))
            {
                MessageBox.Show("请选择仓库！");
                this.cmb_cWHId.Focus();
            }
            else if (this.cmb_OptGroup.Text.Trim() == "")
            {
                MessageBox.Show("请录入操作台组别！");
                this.cmb_OptGroup.Focus();
            }
            else
            {
                pWHId = this.cmb_cWHId.SelectedValue.ToString();
                if ((this.cmb_cAreaId.Text.Trim() != "") && (this.cmb_cAreaId.SelectedValue != null))
                {
                    pAreaId = this.cmb_cAreaId.SelectedValue.ToString();
                }
                if (this.chk_EmptyAsWholeOut.Checked)
                {
                    pIsEmptAsWholeOut = 1;
                }
                if (this.chk_SameBatchNo.Checked)
                {
                    pIsSameBatchNo = 1;
                }
                pOptGroup = this.cmb_OptGroup.SelectedValue.ToString();
                string sErr = "";
                int count = this.grd_Data.SelectedRows.Count;
                if (count == 0)
                {
                    MessageBox.Show("请选择所要配盘的待配数据！");
                    this.grd_Data.Focus();
                }
                else
                {
                    this.prg_Data.Maximum = count;
                    this.prg_Data.Minimum = 0;
                    this.prg_Data.Value = 0;
                    this.prg_Data.Visible = true;
                    count = 0;
                    foreach (DataGridViewRow row in this.grd_Data.SelectedRows)
                    {
                        if (DBFuns.SP_Pack_DoPltDtlOutAuto(base.AppInformation.SvrSocket, base.UserInformation.UserId, base.UserInformation.UnitId, "WMS", row.Cells["col_cBNo"].Value.ToString(), row.Cells["col_nItem"].Value.ToString(), pWHId, pAreaId, pIsSameBatchNo, pIsEmptAsWholeOut, pOptGroup, out sErr) == "0")
                        {
                            count++;
                        }
                        this.prg_Data.Value++;
                    }
                    MessageBox.Show("成功自动配盘了 " + count.ToString() + " 条待配盘数据，请刷新配盘数据，以便完成剩余待配数据！");
                    this.OpenDataList();
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.OpenDataList();
        }

        private void cmb_cWHId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cmb_cWHId.SelectedValue != null) && (this.cmb_cWHId.Text.Trim() != ""))
            {
                this.LoadCombWAreaList(this.cmb_cWHId.SelectedValue.ToString(), this.cmb_cAreaId);
                this.LoadCombOptGroupList(this.cmb_cWHId.SelectedValue.ToString(), this.cmb_OptGroup);
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

        private void frmOutStoreByAuto_Load(object sender, EventArgs e)
        {
            this.grd_Data.AutoGenerateColumns = false;
            this.LoadCombWare(this.cmb_cWHId);
            this.OpenDataList();
            this.cmb_cAreaId.SelectedIndex = -1;
        }

        private void InitializeComponent()
        {
            this.pnl_Bottom = new Panel();
            this.prg_Data = new ProgressBar();
            this.btn_OK = new Button();
            this.btn_Cancel = new Button();
            this.grp_Condition = new GroupBox();
            this.cmb_OptGroup = new ComboBox();
            this.label4 = new Label();
            this.chk_EmptyAsWholeOut = new CheckBox();
            this.chk_SameBatchNo = new CheckBox();
            this.chk_FirstInAndOut = new CheckBox();
            this.label20 = new Label();
            this.cmb_cAreaId = new ComboBox();
            this.label17 = new Label();
            this.cmb_cWHId = new ComboBox();
            this.pnl_RecordCount = new Panel();
            this.lbl_DataCount = new Label();
            this.label1 = new Label();
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
            this.label26 = new Label();
            this.btn_Refresh = new Button();
            this.txt_BillNo = new TextBox();
            this.label3 = new Label();
            this.txt_MatName = new TextBox();
            this.pnl_Bottom.SuspendLayout();
            this.grp_Condition.SuspendLayout();
            this.pnl_RecordCount.SuspendLayout();
            ((ISupportInitialize) this.grd_Data).BeginInit();
            base.SuspendLayout();
            this.pnl_Bottom.Controls.Add(this.prg_Data);
            this.pnl_Bottom.Controls.Add(this.btn_OK);
            this.pnl_Bottom.Controls.Add(this.btn_Cancel);
            this.pnl_Bottom.Dock = DockStyle.Bottom;
            this.pnl_Bottom.Location = new Point(0, 0x1af);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new Size(0x332, 0x33);
            this.pnl_Bottom.TabIndex = 9;
            this.prg_Data.Location = new Point(12, 0x21);
            this.prg_Data.Name = "prg_Data";
            this.prg_Data.Size = new Size(0x31a, 0x11);
            this.prg_Data.TabIndex = 10;
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
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click_1);
            this.grp_Condition.Controls.Add(this.cmb_OptGroup);
            this.grp_Condition.Controls.Add(this.label4);
            this.grp_Condition.Controls.Add(this.chk_EmptyAsWholeOut);
            this.grp_Condition.Controls.Add(this.chk_SameBatchNo);
            this.grp_Condition.Controls.Add(this.chk_FirstInAndOut);
            this.grp_Condition.Controls.Add(this.label20);
            this.grp_Condition.Controls.Add(this.cmb_cAreaId);
            this.grp_Condition.Controls.Add(this.label17);
            this.grp_Condition.Controls.Add(this.cmb_cWHId);
            this.grp_Condition.Dock = DockStyle.Bottom;
            this.grp_Condition.Location = new Point(0, 0x158);
            this.grp_Condition.Name = "grp_Condition";
            this.grp_Condition.Size = new Size(0x332, 0x57);
            this.grp_Condition.TabIndex = 10;
            this.grp_Condition.TabStop = false;
            this.cmb_OptGroup.FormattingEnabled = true;
            this.cmb_OptGroup.Location = new Point(0x1bd, 0x35);
            this.cmb_OptGroup.Name = "cmb_OptGroup";
            this.cmb_OptGroup.Size = new Size(0x76, 20);
            this.cmb_OptGroup.TabIndex = 0x6a;
            this.cmb_OptGroup.Tag = "101";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(380, 0x39);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 0x69;
            this.label4.Text = "操作台组别";
            this.chk_EmptyAsWholeOut.AutoSize = true;
            this.chk_EmptyAsWholeOut.Location = new Point(0x184, 0x16);
            this.chk_EmptyAsWholeOut.Name = "chk_EmptyAsWholeOut";
            this.chk_EmptyAsWholeOut.Size = new Size(0x84, 0x10);
            this.chk_EmptyAsWholeOut.TabIndex = 0x67;
            this.chk_EmptyAsWholeOut.Text = "空盘按整盘出库处理";
            this.chk_EmptyAsWholeOut.UseVisualStyleBackColor = true;
            this.chk_SameBatchNo.AutoSize = true;
            this.chk_SameBatchNo.Location = new Point(0xcd, 0x16);
            this.chk_SameBatchNo.Name = "chk_SameBatchNo";
            this.chk_SameBatchNo.Size = new Size(0x6c, 0x10);
            this.chk_SameBatchNo.TabIndex = 0x66;
            this.chk_SameBatchNo.Text = "必须按批次出库";
            this.chk_SameBatchNo.UseVisualStyleBackColor = true;
            this.chk_FirstInAndOut.AutoSize = true;
            this.chk_FirstInAndOut.Checked = true;
            this.chk_FirstInAndOut.CheckState = CheckState.Checked;
            this.chk_FirstInAndOut.Enabled = false;
            this.chk_FirstInAndOut.Location = new Point(0x3b, 0x16);
            this.chk_FirstInAndOut.Name = "chk_FirstInAndOut";
            this.chk_FirstInAndOut.Size = new Size(0x48, 0x10);
            this.chk_FirstInAndOut.TabIndex = 0x65;
            this.chk_FirstInAndOut.Text = "先进先出";
            this.chk_FirstInAndOut.UseVisualStyleBackColor = true;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0xc3, 0x39);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x1d, 12);
            this.label20.TabIndex = 0x5c;
            this.label20.Text = "区域";
            this.cmb_cAreaId.FormattingEnabled = true;
            this.cmb_cAreaId.Location = new Point(0xe5, 0x35);
            this.cmb_cAreaId.Name = "cmb_cAreaId";
            this.cmb_cAreaId.Size = new Size(0x92, 20);
            this.cmb_cAreaId.TabIndex = 0x5b;
            this.cmb_cAreaId.Tag = "101";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(9, 0x39);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1d, 12);
            this.label17.TabIndex = 0x5e;
            this.label17.Text = "仓库";
            this.cmb_cWHId.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0x34, 0x35);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(0x87, 20);
            this.cmb_cWHId.TabIndex = 0x5d;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.SelectedIndexChanged += new EventHandler(this.cmb_cWHId_SelectedIndexChanged);
            this.pnl_RecordCount.Controls.Add(this.label3);
            this.pnl_RecordCount.Controls.Add(this.txt_MatName);
            this.pnl_RecordCount.Controls.Add(this.txt_BillNo);
            this.pnl_RecordCount.Controls.Add(this.btn_Refresh);
            this.pnl_RecordCount.Controls.Add(this.label26);
            this.pnl_RecordCount.Controls.Add(this.lbl_DataCount);
            this.pnl_RecordCount.Controls.Add(this.label1);
            this.pnl_RecordCount.Dock = DockStyle.Bottom;
            this.pnl_RecordCount.Location = new Point(0, 0x13a);
            this.pnl_RecordCount.Name = "pnl_RecordCount";
            this.pnl_RecordCount.Size = new Size(0x332, 30);
            this.pnl_RecordCount.TabIndex = 12;
            this.lbl_DataCount.AutoSize = true;
            this.lbl_DataCount.Location = new Point(80, 7);
            this.lbl_DataCount.Name = "lbl_DataCount";
            this.lbl_DataCount.Size = new Size(11, 12);
            this.lbl_DataCount.TabIndex = 1;
            this.lbl_DataCount.Text = "0";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据条数：";
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
            this.grd_Data.Size = new Size(0x332, 0x13a);
            this.grd_Data.TabIndex = 13;
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
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0x95, 9);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x29, 12);
            this.label26.TabIndex = 0x4e;
            this.label26.Text = "单号：";
            this.btn_Refresh.Location = new Point(0x1f0, 4);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new Size(0x4b, 0x17);
            this.btn_Refresh.TabIndex = 80;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new EventHandler(this.btn_Refresh_Click);
            this.txt_BillNo.Location = new Point(0xbb, 6);
            this.txt_BillNo.Name = "txt_BillNo";
            this.txt_BillNo.Size = new Size(0x74, 0x15);
            this.txt_BillNo.TabIndex = 0x54;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(310, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 0x56;
            this.label3.Text = "物料：";
            this.txt_MatName.Location = new Point(0x163, 5);
            this.txt_MatName.Name = "txt_MatName";
            this.txt_MatName.Size = new Size(0x74, 0x15);
            this.txt_MatName.TabIndex = 0x55;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x332, 0x1e2);
            base.Controls.Add(this.grd_Data);
            base.Controls.Add(this.pnl_RecordCount);
            base.Controls.Add(this.grp_Condition);
            base.Controls.Add(this.pnl_Bottom);
            base.MinimizeBox = false;
            base.Name = "frmOutStoreByAuto";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "自动出库";
            base.Load += new EventHandler(this.frmOutStoreByAuto_Load);
            this.pnl_Bottom.ResumeLayout(false);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            this.pnl_RecordCount.ResumeLayout(false);
            this.pnl_RecordCount.PerformLayout();
            ((ISupportInitialize) this.grd_Data).EndInit();
            base.ResumeLayout(false);
        }

        private void LoadCombOptGroupList(string sWHId, ComboBox cmbX)
        {
            string sSql = "select distinct cGroupName from TECS_HSInfo where bUsed=1 ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " and cWHId='" + sWHId + "'";
            }
            string sErr = "";
            DataSet set = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TWC_OptGroup", 0, 0, "", out sErr);
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
            cmbX.ValueMember = "cGroupName";
            cmbX.DisplayMember = "cGroupName";
            if (set != null)
            {
                DataTable table = set.Tables["TWC_OptGroup"];
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
                this.grd_Data.DataSource = set.Tables["tbBillDtl"];
                this.lbl_DataCount.Text = set.Tables["tbBillDtl"].Rows.Count.ToString();
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

