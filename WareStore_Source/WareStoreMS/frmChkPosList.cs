namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmChkPosList : FrmSTable
    {
        private string _CheckNo = "";
        private bool _IsOK = false;
        private int _Status = 0;
        private BindingSource bsGrid;
        private Button btnClose;
        private Button btnOK;
        private ComboBox cmb_Station;
        private DataGridViewTextBoxColumn colcBNo;
        private DataGridViewTextBoxColumn colcPosId;
        private DataGridViewTextBoxColumn colcStatus;
        private DataGridViewTextBoxColumn colnPalletId;
        private DataGridViewTextBoxColumn colnStatus;
        private IContainer components = null;
        private DataGridView grdData;
        private Label label1;
        private Label label2;
        private Label lbl_ChkNo;
        private Panel pnlButton;
        private Panel pnlTop;
        private ToolTip toolTip1;

        public frmChkPosList()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this._IsOK = false;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.bsGrid.Count == 0)
            {
                MessageBox.Show("对不起，没有任务数据可下发！");
            }
            else if ((this.grdData.SelectedRows != null) && (this.grdData.SelectedRows.Count == 0))
            {
                MessageBox.Show("对不起，没有选择要下发的任务数据！");
            }
            else
            {
                int num = 0;
                foreach (DataGridViewRow row in this.grdData.SelectedRows)
                {
                    if (int.Parse(row.Cells[this.colnStatus.Name].Value.ToString()) < 1)
                    {
                        string sErr = "";
                        if ((PubDBCommFuns.sp_Chk_DoCheckTask(base.AppInformation.SvrSocket, base.UserInformation.UserName, "WMS", base.UserInformation.UnitId, row.Cells[this.colcPosId.Name].Value.ToString().Trim(), int.Parse(this.cmb_Station.Text.Trim()), this._CheckNo, out sErr) == "0") || (sErr.Trim() == ""))
                        {
                            num++;
                        }
                    }
                }
                MessageBox.Show("已经成功下发：" + num.ToString() + " 条盘点任务！");
                this._IsOK = num > 0;
                base.Close();
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

        private void frmChkPosList_Load(object sender, EventArgs e)
        {
            string sErr = "";
            if (this._CheckNo.Trim() != "")
            {
                DataTable table = PubDBCommFuns.sp_Chk_GetChkPosList(base.AppInformation.SvrSocket, this._CheckNo.Trim(), this._Status, out sErr);
                if ((sErr.Trim() != "") && (sErr != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    this.bsGrid.DataSource = table;
                    this.grdData.DataSource = this.bsGrid;
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.pnlTop = new Panel();
            this.cmb_Station = new ComboBox();
            this.label2 = new Label();
            this.lbl_ChkNo = new Label();
            this.label1 = new Label();
            this.pnlButton = new Panel();
            this.btnClose = new Button();
            this.btnOK = new Button();
            this.grdData = new DataGridView();
            this.colcBNo = new DataGridViewTextBoxColumn();
            this.colcPosId = new DataGridViewTextBoxColumn();
            this.colnPalletId = new DataGridViewTextBoxColumn();
            this.colcStatus = new DataGridViewTextBoxColumn();
            this.colnStatus = new DataGridViewTextBoxColumn();
            this.bsGrid = new BindingSource(this.components);
            this.toolTip1 = new ToolTip(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlButton.SuspendLayout();
            ((ISupportInitialize) this.grdData).BeginInit();
            ((ISupportInitialize) this.bsGrid).BeginInit();
            base.SuspendLayout();
            this.pnlTop.Controls.Add(this.cmb_Station);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.lbl_ChkNo);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new Size(420, 0x23);
            this.pnlTop.TabIndex = 0;
            this.cmb_Station.FormattingEnabled = true;
            this.cmb_Station.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6" });
            this.cmb_Station.Location = new Point(0x11a, 6);
            this.cmb_Station.Name = "cmb_Station";
            this.cmb_Station.Size = new Size(0x57, 20);
            this.cmb_Station.TabIndex = 3;
            this.cmb_Station.Text = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xdd, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "拣选口号：";
            this.lbl_ChkNo.AutoSize = true;
            this.lbl_ChkNo.Location = new Point(0x53, 10);
            this.lbl_ChkNo.Name = "lbl_ChkNo";
            this.lbl_ChkNo.Size = new Size(0x41, 12);
            this.lbl_ChkNo.TabIndex = 1;
            this.lbl_ChkNo.Text = "盘点单号：";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "盘点单号：";
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = DockStyle.Bottom;
            this.pnlButton.Location = new Point(0, 0x16d);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new Size(420, 0x27);
            this.pnlButton.TabIndex = 1;
            this.btnClose.Location = new Point(0xed, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnOK.Location = new Point(0x6c, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.Columns.AddRange(new DataGridViewColumn[] { this.colcBNo, this.colcPosId, this.colnPalletId, this.colcStatus, this.colnStatus });
            this.grdData.Dock = DockStyle.Fill;
            this.grdData.Location = new Point(0, 0x23);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.RowTemplate.Height = 0x17;
            this.grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new Size(420, 330);
            this.grdData.TabIndex = 2;
            this.toolTip1.SetToolTip(this.grdData, "拖动鼠标，可以多选");
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.HeaderText = "单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            this.colcPosId.DataPropertyName = "cPosId";
            this.colcPosId.HeaderText = "货位号";
            this.colcPosId.Name = "colcPosId";
            this.colcPosId.ReadOnly = true;
            this.colnPalletId.DataPropertyName = "nPalletId";
            this.colnPalletId.HeaderText = "托盘号";
            this.colnPalletId.Name = "colnPalletId";
            this.colnPalletId.ReadOnly = true;
            this.colcStatus.DataPropertyName = "cStatus";
            this.colcStatus.HeaderText = "货位状态";
            this.colcStatus.Name = "colcStatus";
            this.colcStatus.ReadOnly = true;
            this.colnStatus.DataPropertyName = "nStatus";
            this.colnStatus.HeaderText = "状态号";
            this.colnStatus.Name = "colnStatus";
            this.colnStatus.ReadOnly = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(420, 0x194);
            base.Controls.Add(this.grdData);
            base.Controls.Add(this.pnlButton);
            base.Controls.Add(this.pnlTop);
            base.MinimizeBox = false;
            base.Name = "frmChkPosList";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "盘点任务列表";
            base.Load += new EventHandler(this.frmChkPosList_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            ((ISupportInitialize) this.grdData).EndInit();
            ((ISupportInitialize) this.bsGrid).EndInit();
            base.ResumeLayout(false);
        }

        public string CheckNo
        {
            get
            {
                return this._CheckNo.Trim();
            }
            set
            {
                this._CheckNo = value.Trim();
                this.lbl_ChkNo.Text = this._CheckNo;
            }
        }

        public bool IsOK
        {
            get
            {
                return this._IsOK;
            }
        }

        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
    }
}

