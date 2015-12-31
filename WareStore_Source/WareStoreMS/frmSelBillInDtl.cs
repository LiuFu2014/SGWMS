namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmSelBillInDtl : FrmSTable
    {
        private bool _IsSelected = false;
        private string _SelBatchNo = "";
        private string _SelBNo = "";
        private string _SelItem = "";
        private string _SelQCStatus = "";
        private string _SelUnit = "";
        private BindingSource bsGrid;
        private Button btnClose;
        private Button btnOK;
        private DataGridViewTextBoxColumn colcBatchNo;
        private DataGridViewTextBoxColumn colcBNo;
        private DataGridViewTextBoxColumn colcUnit;
        private DataGridViewTextBoxColumn coldProdDate;
        private DataGridViewTextBoxColumn colnItem;
        private DataGridViewTextBoxColumn colnQCStatus;
        private IContainer components = null;
        private DataGridView grdData;
        private Label label1;
        private Label label3;
        public Label lbl_cMNo;
        public Label lbl_cWHId;
        private Panel pnlButton;
        private Panel pnlTop;
        private ToolTip toolTip1;

        public frmSelBillInDtl()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this._IsSelected = false;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool flag = true;
            object obj2 = null;
            foreach (DataGridViewRow row in this.grdData.SelectedRows)
            {
                if (flag)
                {
                    this._SelBNo = "" + row.Cells[this.colcBNo.Name].Value.ToString().Trim() + "";
                    this._SelItem = "" + row.Cells[this.colnItem.Name].Value.ToString().Trim() + "";
                    obj2 = row.Cells[this.colcBatchNo.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelBatchNo = obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelBatchNo = "";
                    }
                    obj2 = row.Cells[this.colcUnit.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelUnit = obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelUnit = "";
                    }
                    obj2 = row.Cells[this.colnQCStatus.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelQCStatus = obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelQCStatus = "";
                    }
                    flag = false;
                }
                else
                {
                    this._SelBNo = this._SelBNo + "," + row.Cells[this.colcBNo.Name].Value.ToString().Trim() + "";
                    this._SelItem = this._SelItem + "," + row.Cells[this.colnItem.Name].Value.ToString().Trim() + "";
                    obj2 = row.Cells[this.colcBatchNo.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelBatchNo = this._SelBatchNo + "," + obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelBatchNo = this._SelBatchNo + ",";
                    }
                    obj2 = row.Cells[this.colcUnit.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelUnit = this._SelUnit + "," + obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelUnit = this._SelUnit + ",";
                    }
                    obj2 = row.Cells[this.colnQCStatus.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelQCStatus = this._SelQCStatus + "," + obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelQCStatus = this._SelQCStatus + ",";
                    }
                }
            }
            if (this._SelBNo.Trim() == "")
            {
                if (MessageBox.Show("没有选择数据，需要退出吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }
            else if (MessageBox.Show("您确定你选择的是：" + this._SelBNo + " 吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            this._IsSelected = true;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelBillInDtl_Load(object sender, EventArgs e)
        {
            string pWHId = this.lbl_cWHId.Text.Trim();
            string pMNo = this.lbl_cMNo.Text.Trim();
            string sErr = "";
            DataTable table = PubDBCommFuns.sp_Chk_GetBillInDtlList(base.AppInformation.SvrSocket, pWHId, pMNo, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                this.bsGrid.DataSource = table;
                this.grdData.DataSource = this.bsGrid;
            }
        }

        private void grdData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK_Click(null, null);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.grdData = new DataGridView();
            this.colcBNo = new DataGridViewTextBoxColumn();
            this.colnItem = new DataGridViewTextBoxColumn();
            this.colcBatchNo = new DataGridViewTextBoxColumn();
            this.colcUnit = new DataGridViewTextBoxColumn();
            this.colnQCStatus = new DataGridViewTextBoxColumn();
            this.coldProdDate = new DataGridViewTextBoxColumn();
            this.pnlButton = new Panel();
            this.btnClose = new Button();
            this.btnOK = new Button();
            this.toolTip1 = new ToolTip(this.components);
            this.pnlTop = new Panel();
            this.lbl_cMNo = new Label();
            this.label3 = new Label();
            this.lbl_cWHId = new Label();
            this.label1 = new Label();
            this.bsGrid = new BindingSource(this.components);
            ((ISupportInitialize) this.grdData).BeginInit();
            this.pnlButton.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((ISupportInitialize) this.bsGrid).BeginInit();
            base.SuspendLayout();
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Columns.AddRange(new DataGridViewColumn[] { this.colcBNo, this.colnItem, this.colcBatchNo, this.colcUnit, this.colnQCStatus, this.coldProdDate });
            this.grdData.Dock = DockStyle.Fill;
            this.grdData.Location = new Point(0, 0x23);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.RowTemplate.Height = 0x17;
            this.grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new Size(0x25a, 280);
            this.grdData.TabIndex = 5;
            this.toolTip1.SetToolTip(this.grdData, "双击，选择");
            this.grdData.CellDoubleClick += new DataGridViewCellEventHandler(this.grdData_CellDoubleClick);
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.HeaderText = "单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            this.colnItem.DataPropertyName = "nItem";
            this.colnItem.HeaderText = "单明细序号";
            this.colnItem.Name = "colnItem";
            this.colnItem.ReadOnly = true;
            this.colnItem.Width = 90;
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "计量单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colnQCStatus.DataPropertyName = "nQCStatus";
            this.colnQCStatus.HeaderText = "质检状态";
            this.colnQCStatus.Name = "colnQCStatus";
            this.colnQCStatus.ReadOnly = true;
            this.colnQCStatus.Visible = false;
            this.coldProdDate.DataPropertyName = "dProdDate";
            this.coldProdDate.HeaderText = "生产日期";
            this.coldProdDate.Name = "coldProdDate";
            this.coldProdDate.ReadOnly = true;
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = DockStyle.Bottom;
            this.pnlButton.Location = new Point(0, 0x13b);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new Size(0x25a, 0x27);
            this.pnlButton.TabIndex = 4;
            this.btnClose.Location = new Point(0x148, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnOK.Location = new Point(0xc7, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.pnlTop.Controls.Add(this.lbl_cMNo);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.lbl_cWHId);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new Size(0x25a, 0x23);
            this.pnlTop.TabIndex = 3;
            this.lbl_cMNo.Location = new Point(0xc3, 11);
            this.lbl_cMNo.Name = "lbl_cMNo";
            this.lbl_cMNo.Size = new Size(130, 12);
            this.lbl_cMNo.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x91, 11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "物料号：";
            this.lbl_cWHId.Location = new Point(0x3e, 11);
            this.lbl_cWHId.Name = "lbl_cWHId";
            this.lbl_cWHId.Size = new Size(0x52, 12);
            this.lbl_cWHId.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库号：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x25a, 0x162);
            base.Controls.Add(this.grdData);
            base.Controls.Add(this.pnlButton);
            base.Controls.Add(this.pnlTop);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "frmSelBillInDtl";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "选择库存入库单明细";
            base.Load += new EventHandler(this.frmSelBillInDtl_Load);
            ((ISupportInitialize) this.grdData).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((ISupportInitialize) this.bsGrid).EndInit();
            base.ResumeLayout(false);
        }

        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                this._IsSelected = value;
            }
        }

        public string SelBatchNo
        {
            get
            {
                return this._SelBatchNo.Trim();
            }
            set
            {
                this._SelBatchNo = value.Trim();
            }
        }

        public string SelBNo
        {
            get
            {
                return this._SelBNo.Trim();
            }
            set
            {
                this._SelBNo = value.Trim();
            }
        }

        public string SelItem
        {
            get
            {
                return this._SelItem.Trim();
            }
            set
            {
                this._SelItem = value.Trim();
            }
        }

        public string SelQCStatus
        {
            get
            {
                return this._SelQCStatus.Trim();
            }
            set
            {
                this._SelQCStatus = value.Trim();
            }
        }

        public string SelUnit
        {
            get
            {
                return this._SelUnit.Trim();
            }
            set
            {
                this._SelUnit = value.Trim();
            }
        }
    }
}

