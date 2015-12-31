namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;

    public class frmSelStkMaterail : FrmSTable
    {
        private string _cBatchNo = "";
        private string _cBNo = "";
        private string _cMName = "";
        private string _cMNo = "";
        private StringBuilder _CondtionDesc = new StringBuilder("");
        private string _cSpec = "";
        private string _cUnit = "";
        private DateTime _dBadDate;
        private DateTime _dProdDate;
        private double _fQty = 0.0;
        private bool _IsResultOK = false;
        private bool _IsSelect = false;
        private int _nItem = 0;
        private int _nQCStatus = 0;
        private StringBuilder _StrSql = new StringBuilder("");
        private BindingSource bdsList;
        private Button btn_Qry;
        private Button btn_Reset;
        private Button btnOK;
        private CheckBox chk_Date;
        private ComboBox cmb_MatType1;
        private ComboBox cmb_nQCStatus;
        private DataGridViewTextBoxColumn colcBatchNo;
        private DataGridViewTextBoxColumn colcBNoIn;
        private DataGridViewTextBoxColumn colcMName;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn colcQCStatus;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn colcUnit;
        private DataGridViewTextBoxColumn coldBadDate;
        private DataGridViewTextBoxColumn colfQty;
        private DataGridViewTextBoxColumn colnItemIn;
        private IContainer components = null;
        private DateTimePicker dtp_dFrom;
        private DateTimePicker dtp_To;
        private DataGridView grdList;
        private GroupBox grpConidtion;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Panel panel1;
        private TextBox txt_cBNoIn;
        private TextBox txt_cMNo;
        private TextBox txt_QCDay;

        public frmSelStkMaterail()
        {
            this.InitializeComponent();
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            if (this.chk_Date.Checked && (this.dtp_dFrom.Value.Date > this.dtp_To.Value.Date))
            {
                MessageBox.Show("对不起，起止日期不能大于截止日期！");
                this.dtp_dFrom.Focus();
            }
            else
            {
                this._StrSql.Remove(0, this._StrSql.Length);
                this._StrSql.Append(this.GetSql());
                string sErr = "";
                Cursor.Current = Cursors.WaitCursor;
                DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, this._StrSql.ToString(), "V_STOREITEMLIST", "dDate,dProdDate,dBadDate", out sErr);
                Cursor.Current = Cursors.Default;
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    this.bdsList.DataSource = set.Tables[1];
                    this.grdList.DataSource = this.bdsList;
                }
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.ResetConiditon();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.bdsList.Count == 0)
            {
                MessageBox.Show("对不起，无数据可选择！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsList.Current;
                if (current == null)
                {
                    MessageBox.Show("对不起，无数据可选择！");
                }
                else
                {
                    string s = "1800-01-01 00:00:00";
                    this._cBNo = current["cBNoIn"].ToString();
                    this._nItem = int.Parse(current["nItemIn"].ToString());
                    this._cMNo = current["cMNo"].ToString();
                    this._cMName = current["cMName"].ToString().Trim();
                    this._cSpec = current["cSpec"].ToString().Trim();
                    this._cBatchNo = current["cBatchNo"].ToString().Trim();
                    if (current["dProdDate"].ToString().Trim() != "")
                    {
                        s = current["dProdDate"].ToString().Trim();
                    }
                    this._dProdDate = DateTime.Parse(s);
                    s = "1800-01-01 00:00:00";
                    if (current["dBadDate"].ToString() != "")
                    {
                        s = current["dBadDate"].ToString().Trim();
                    }
                    this._dBadDate = DateTime.Parse(s);
                    this._fQty = double.Parse(current["fQty"].ToString());
                    this._cUnit = current["cUnit"].ToString();
                    this._IsResultOK = true;
                    base.Close();
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

        private void frmSelStkMaterail_Load(object sender, EventArgs e)
        {
            this.grdList.AutoGenerateColumns = false;
            if (this._IsSelect)
            {
                this.btnOK.Visible = this._IsSelect;
                this.Text = "选择库存物料";
            }
            else
            {
                this.btnOK.Visible = false;
                this.Text = "库存物料";
            }
            this.ResetConiditon();
        }

        private string GetSql()
        {
            this._CondtionDesc.Remove(0, this.ConditionDesc.Length);
            StringBuilder builder = new StringBuilder("Select * from V_STOREITEMLIST  where 1=1 ");
            if (this.txt_cBNoIn.Text.Trim() != "")
            {
                builder.Append(" and cBNoIn like '%" + this.txt_cBNoIn.Text.Trim() + "%'");
                this._CondtionDesc.Append(" 入库单号：" + this.txt_cBNoIn.Text.Trim());
            }
            if ((this.cmb_MatType1.Text.Trim() != "") && (this.cmb_MatType1.SelectedValue != null))
            {
                builder.Append(" and cTypeId1 = '" + this.cmb_MatType1.SelectedValue.ToString().Trim() + "'");
                this._CondtionDesc.Append(" 物料类别：" + this.cmb_MatType1.Text.Trim());
            }
            if (this.txt_cMNo.Text.Trim() != "")
            {
                builder.Append(" and ( (cMNo like  '%" + this.txt_cMNo.Text.Trim() + "%') or (cMName like  '%" + this.txt_cMNo.Text.Trim() + "%') or (cPYJM like  '%" + this.txt_cMNo.Text.Trim() + "%') or (cWBJM like  '%" + this.txt_cMNo.Text.Trim() + "%') )");
                this._CondtionDesc.Append(" 物料：" + this.txt_cMNo.Text.Trim());
            }
            if ((this.cmb_nQCStatus.Text.Trim() != "") && (this.cmb_nQCStatus.SelectedValue != null))
            {
                builder.Append(" and nQCStatus = " + this.cmb_nQCStatus.SelectedValue.ToString().Trim());
                this._CondtionDesc.Append(" 质检状态：" + this.cmb_nQCStatus.Text.Trim());
            }
            if (this.chk_Date.Checked)
            {
                builder.Append(" and (dDate between  '" + this.dtp_dFrom.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + this.dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") + "' )");
                this._CondtionDesc.Append(" 入库日期：" + this.dtp_dFrom.Value.ToString("yyyy-MM-dd") + " — " + this.dtp_To.Value.ToString("yyyy-MM-dd"));
            }
            if (this.txt_QCDay.Text.Trim() != "")
            {
                builder.Append(" and (dBadDate < '" + DateTime.Now.AddDays((double) int.Parse(this.txt_QCDay.Text.Trim())) + "')");
            }
            builder.Append(" order by nQCStatus,dBadDate,dDate");
            return builder.ToString();
        }

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this._IsSelect)
            {
                this.btnOK_Click(null, null);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.grpConidtion = new GroupBox();
            this.label7 = new Label();
            this.label4 = new Label();
            this.btn_Reset = new Button();
            this.btn_Qry = new Button();
            this.cmb_nQCStatus = new ComboBox();
            this.dtp_dFrom = new DateTimePicker();
            this.chk_Date = new CheckBox();
            this.txt_cBNoIn = new TextBox();
            this.dtp_To = new DateTimePicker();
            this.label6 = new Label();
            this.txt_QCDay = new TextBox();
            this.label5 = new Label();
            this.txt_cMNo = new TextBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.cmb_MatType1 = new ComboBox();
            this.label1 = new Label();
            this.grdList = new DataGridView();
            this.colcMNo = new DataGridViewTextBoxColumn();
            this.colcMName = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.colcBatchNo = new DataGridViewTextBoxColumn();
            this.coldBadDate = new DataGridViewTextBoxColumn();
            this.colcBNoIn = new DataGridViewTextBoxColumn();
            this.colnItemIn = new DataGridViewTextBoxColumn();
            this.colfQty = new DataGridViewTextBoxColumn();
            this.colcUnit = new DataGridViewTextBoxColumn();
            this.colcQCStatus = new DataGridViewTextBoxColumn();
            this.bdsList = new BindingSource(this.components);
            this.btnOK = new Button();
            this.panel1 = new Panel();
            this.grpConidtion.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            ((ISupportInitialize) this.bdsList).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.grpConidtion.Controls.Add(this.label7);
            this.grpConidtion.Controls.Add(this.label4);
            this.grpConidtion.Controls.Add(this.btn_Reset);
            this.grpConidtion.Controls.Add(this.btn_Qry);
            this.grpConidtion.Controls.Add(this.cmb_nQCStatus);
            this.grpConidtion.Controls.Add(this.dtp_dFrom);
            this.grpConidtion.Controls.Add(this.chk_Date);
            this.grpConidtion.Controls.Add(this.txt_cBNoIn);
            this.grpConidtion.Controls.Add(this.dtp_To);
            this.grpConidtion.Controls.Add(this.label6);
            this.grpConidtion.Controls.Add(this.txt_QCDay);
            this.grpConidtion.Controls.Add(this.label5);
            this.grpConidtion.Controls.Add(this.txt_cMNo);
            this.grpConidtion.Controls.Add(this.label3);
            this.grpConidtion.Controls.Add(this.label2);
            this.grpConidtion.Controls.Add(this.cmb_MatType1);
            this.grpConidtion.Controls.Add(this.label1);
            this.grpConidtion.Dock = DockStyle.Top;
            this.grpConidtion.Location = new Point(0, 0);
            this.grpConidtion.Name = "grpConidtion";
            this.grpConidtion.Size = new Size(0x309, 0x6b);
            this.grpConidtion.TabIndex = 1;
            this.grpConidtion.TabStop = false;
            this.grpConidtion.Text = "条件";
            this.label7.BackColor = SystemColors.ControlText;
            this.label7.Location = new Point(0xc9, 0x36);
            this.label7.Name = "label7";
            this.label7.Size = new Size(10, 1);
            this.label7.TabIndex = 0x10;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x135, 0x30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "质检状态：";
            this.btn_Reset.Location = new Point(0x290, 0x4c);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new Size(0x5e, 0x17);
            this.btn_Reset.TabIndex = 14;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new EventHandler(this.btn_Reset_Click);
            this.btn_Qry.Location = new Point(0x22c, 0x4c);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x5e, 0x17);
            this.btn_Qry.TabIndex = 13;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.cmb_nQCStatus.FormattingEnabled = true;
            this.cmb_nQCStatus.Location = new Point(0x176, 0x2c);
            this.cmb_nQCStatus.Name = "cmb_nQCStatus";
            this.cmb_nQCStatus.Size = new Size(0x79, 20);
            this.cmb_nQCStatus.TabIndex = 12;
            this.dtp_dFrom.CustomFormat = "";
            this.dtp_dFrom.Format = DateTimePickerFormat.Short;
            this.dtp_dFrom.Location = new Point(0x70, 0x2c);
            this.dtp_dFrom.Name = "dtp_dFrom";
            this.dtp_dFrom.Size = new Size(0x54, 0x15);
            this.dtp_dFrom.TabIndex = 2;
            this.chk_Date.AutoSize = true;
            this.chk_Date.Location = new Point(0x17, 0x2e);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.Size = new Size(0x54, 0x10);
            this.chk_Date.TabIndex = 11;
            this.chk_Date.Text = "入库日期：";
            this.chk_Date.UseVisualStyleBackColor = true;
            this.txt_cBNoIn.Location = new Point(0x70, 14);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.Size = new Size(0xbd, 0x15);
            this.txt_cBNoIn.TabIndex = 0;
            this.dtp_To.CustomFormat = "";
            this.dtp_To.Format = DateTimePickerFormat.Short;
            this.dtp_To.Location = new Point(0xd9, 0x2c);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new Size(0x54, 0x15);
            this.dtp_To.TabIndex = 10;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x2dd, 0x30);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x11, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "天";
            this.txt_QCDay.Location = new Point(0x24c, 0x2c);
            this.txt_QCDay.Name = "txt_QCDay";
            this.txt_QCDay.Size = new Size(0x8b, 0x15);
            this.txt_QCDay.TabIndex = 8;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x1f9, 0x30);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x4d, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "离到期天数：";
            this.txt_cMNo.Location = new Point(550, 14);
            this.txt_cMNo.Name = "txt_cMNo";
            this.txt_cMNo.Size = new Size(200, 0x15);
            this.txt_cMNo.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1f9, 0x12);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "物料：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x135, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "物料类别：";
            this.cmb_MatType1.FormattingEnabled = true;
            this.cmb_MatType1.Location = new Point(0x176, 14);
            this.cmb_MatType1.Name = "cmb_MatType1";
            this.cmb_MatType1.Size = new Size(0x79, 20);
            this.cmb_MatType1.TabIndex = 3;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x17, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "库存入库单号：";
            this.grdList.AllowUserToAddRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colcMNo, this.colcMName, this.colcSpec, this.colcBatchNo, this.coldBadDate, this.colcBNoIn, this.colnItemIn, this.colfQty, this.colcUnit, this.colcQCStatus });
            this.grdList.Dock = DockStyle.Fill;
            this.grdList.Location = new Point(0, 0x6b);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowTemplate.Height = 0x17;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new Size(0x309, 0x161);
            this.grdList.TabIndex = 2;
            this.grdList.CellDoubleClick += new DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编号";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "物料规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            this.coldBadDate.DataPropertyName = "dBadDate";
            this.coldBadDate.HeaderText = "有效期";
            this.coldBadDate.Name = "coldBadDate";
            this.coldBadDate.ReadOnly = true;
            this.colcBNoIn.DataPropertyName = "cBNoIn";
            this.colcBNoIn.HeaderText = "入库单号";
            this.colcBNoIn.Name = "colcBNoIn";
            this.colcBNoIn.ReadOnly = true;
            this.colnItemIn.DataPropertyName = "nItemIn";
            this.colnItemIn.HeaderText = "入库单序";
            this.colnItemIn.Name = "colnItemIn";
            this.colnItemIn.ReadOnly = true;
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcQCStatus.DataPropertyName = "cQCStatus";
            this.colcQCStatus.HeaderText = "质检状态";
            this.colcQCStatus.Name = "colcQCStatus";
            this.colcQCStatus.ReadOnly = true;
            this.btnOK.Font = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnOK.Location = new Point(0x15f, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x309, 0x2d);
            this.panel1.TabIndex = 15;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1f9);
            base.Controls.Add(this.grdList);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.grpConidtion);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "frmSelStkMaterail";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "库存物料";
            base.Load += new EventHandler(this.frmSelStkMaterail_Load);
            this.grpConidtion.ResumeLayout(false);
            this.grpConidtion.PerformLayout();
            ((ISupportInitialize) this.grdList).EndInit();
            ((ISupportInitialize) this.bdsList).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void ResetConiditon()
        {
            this.txt_cBNoIn.Text = "";
            this.txt_cMNo.Text = "";
            this.txt_QCDay.Text = "";
            this.chk_Date.Checked = false;
            this.cmb_MatType1.SelectedIndex = -1;
            this.cmb_nQCStatus.SelectedIndex = -1;
            this.dtp_dFrom.Value = DateTime.Now.AddDays(-30.0);
            this.dtp_To.Value = DateTime.Now;
            this.txt_cBNoIn.Focus();
        }

        public string cBatchNo
        {
            get
            {
                return this._cBatchNo.Trim();
            }
        }

        public string cBNo
        {
            get
            {
                return this._cBNo.Trim();
            }
        }

        public string cMName
        {
            get
            {
                return this._cMName.Trim();
            }
        }

        public string cMNo
        {
            get
            {
                return this._cMNo.Trim();
            }
        }

        public string ConditionDesc
        {
            get
            {
                return this._CondtionDesc.ToString();
            }
        }

        public string cSpec
        {
            get
            {
                return this._cSpec.Trim();
            }
        }

        public string cUnit
        {
            get
            {
                return this._cUnit.Trim();
            }
        }

        public DateTime dBadDate
        {
            get
            {
                return this._dBadDate;
            }
        }

        public DateTime dProdDate
        {
            get
            {
                return this._dProdDate;
            }
        }

        public double fQty
        {
            get
            {
                return this._fQty;
            }
        }

        public bool IsResultOK
        {
            get
            {
                return this._IsResultOK;
            }
        }

        public bool IsSelect
        {
            get
            {
                return this._IsSelect;
            }
            set
            {
                this._IsSelect = value;
                if (this._IsSelect)
                {
                    this.btnOK.Visible = this._IsSelect;
                    this.Text = "选择库存物料";
                }
                else
                {
                    this.btnOK.Visible = false;
                    this.Text = "库存物料";
                }
            }
        }

        public int nItem
        {
            get
            {
                return this._nItem;
            }
        }

        public int nQCStatus
        {
            get
            {
                return this._nQCStatus;
            }
        }

        public string StrSql
        {
            get
            {
                return this._StrSql.ToString();
            }
        }
    }
}

