namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;

    public class FrmSelectPallet : FrmSTable
    {
        private BindingSource bdsList;
        private bool bIsResultOK = false;
        private Button btnClose;
        private Button btnOK;
        private Button btnQry;
        public ComboBox cmb_nState;
        private DataGridViewTextBoxColumn col_nH;
        private DataGridViewTextBoxColumn col_nHeight;
        private DataGridViewTextBoxColumn col_nL;
        private DataGridViewTextBoxColumn col_nW;
        private DataGridViewTextBoxColumn col_nWeight;
        private DataGridViewTextBoxColumn colcPalletNo;
        private DataGridViewTextBoxColumn colnPalletType;
        private IContainer components = null;
        public DataGridView grdCellList;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel pnlTop;
        private string selResult = "";
        private string strTbNameMain = "TWC_PalletCell";
        private TextBox txt_cPalletNo;

        public FrmSelectPallet()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.bIsResultOK = false;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.grdCellList_CellDoubleClick(null, null);
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder(" where nPalletId not in(select nPalletId from TWC_WareCell where isnull(nPalletId,' ') <> ' 'union select distinct nPalletId from TWB_WorkTask where nWKStatus <99  ) ");
            if (this.cmb_nState.Text.Trim() != "")
            {
                builder.Append(" and  cPalletSpec ='" + this.cmb_nState.SelectedValue.ToString().Trim() + "'");
            }
            if (this.txt_cPalletNo.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and nPalletId like '%" + this.txt_cPalletNo.Text.Trim() + "%'");
                }
                else
                {
                    builder.Append(" and  nPalletId like '%" + this.txt_cPalletNo.Text.Trim() + "%'");
                }
            }
            this.QueryPalletList(builder.ToString());
        }

        private void cmb_nState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
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

        private void FrmSelectPallet_Load(object sender, EventArgs e)
        {
            this.LoadPalletSpecList();
            this.cmb_nState.Focus();
        }

        private void grdCellList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.bIsResultOK = false;
            if (this.bdsList.Count != 0)
            {
                DataRowView current = (DataRowView) this.bdsList.Current;
                if (current != null)
                {
                    this.selResult = current["nPalletId"].ToString();
                    this.bIsResultOK = true;
                    if (this.bIsResultOK)
                    {
                        base.Close();
                    }
                }
            }
        }

        private void grdCellList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Enter))
            {
                this.grdCellList_CellDoubleClick(null, null);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.bdsList = new BindingSource(this.components);
            this.label2 = new Label();
            this.cmb_nState = new ComboBox();
            this.txt_cPalletNo = new TextBox();
            this.btnQry = new Button();
            this.btnClose = new Button();
            this.btnOK = new Button();
            this.grdCellList = new DataGridView();
            this.label1 = new Label();
            this.pnlTop = new Panel();
            this.panel1 = new Panel();
            this.colcPalletNo = new DataGridViewTextBoxColumn();
            this.colnPalletType = new DataGridViewTextBoxColumn();
            this.col_nL = new DataGridViewTextBoxColumn();
            this.col_nW = new DataGridViewTextBoxColumn();
            this.col_nH = new DataGridViewTextBoxColumn();
            this.col_nHeight = new DataGridViewTextBoxColumn();
            this.col_nWeight = new DataGridViewTextBoxColumn();
            ((ISupportInitialize) this.bdsList).BeginInit();
            ((ISupportInitialize) this.grdCellList).BeginInit();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xf3, 14);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "托盘号：";
            this.cmb_nState.FormattingEnabled = true;
            this.cmb_nState.Location = new Point(0x43, 10);
            this.cmb_nState.Name = "cmb_nState";
            this.cmb_nState.Size = new Size(170, 20);
            this.cmb_nState.TabIndex = 0;
            this.cmb_nState.KeyDown += new KeyEventHandler(this.cmb_nState_KeyDown);
            this.txt_cPalletNo.Location = new Point(0x125, 10);
            this.txt_cPalletNo.Name = "txt_cPalletNo";
            this.txt_cPalletNo.Size = new Size(0x60, 0x15);
            this.txt_cPalletNo.TabIndex = 1;
            this.txt_cPalletNo.KeyDown += new KeyEventHandler(this.cmb_nState_KeyDown);
            this.btnQry.Location = new Point(0x199, 7);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new Size(0x53, 0x17);
            this.btnQry.TabIndex = 4;
            this.btnQry.Text = "查询(&F)";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new EventHandler(this.btnQry_Click);
            this.btnQry.TextChanged += new EventHandler(this.btnQry_Click);
            this.btnClose.Location = new Point(0x135, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0x4f;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnOK.Location = new Point(130, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x4e;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.grdCellList.AllowUserToAddRows = false;
            this.grdCellList.AllowUserToDeleteRows = false;
            this.grdCellList.AllowUserToOrderColumns = true;
            this.grdCellList.AutoGenerateColumns = false;
            this.grdCellList.Columns.AddRange(new DataGridViewColumn[] { this.colcPalletNo, this.colnPalletType, this.col_nL, this.col_nW, this.col_nH, this.col_nHeight, this.col_nWeight });
            this.grdCellList.DataSource = this.bdsList;
            this.grdCellList.Dock = DockStyle.Fill;
            this.grdCellList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdCellList.Location = new Point(0, 40);
            this.grdCellList.MultiSelect = false;
            this.grdCellList.Name = "grdCellList";
            this.grdCellList.ReadOnly = true;
            this.grdCellList.RowHeadersVisible = false;
            this.grdCellList.RowTemplate.Height = 0x17;
            this.grdCellList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdCellList.Size = new Size(570, 0x177);
            this.grdCellList.TabIndex = 0x4d;
            this.grdCellList.Tag = "8";
            this.grdCellList.KeyDown += new KeyEventHandler(this.grdCellList_KeyDown);
            this.grdCellList.CellDoubleClick += new DataGridViewCellEventHandler(this.grdCellList_CellDoubleClick);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "托盘类型：";
            this.pnlTop.Controls.Add(this.btnQry);
            this.pnlTop.Controls.Add(this.txt_cPalletNo);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.cmb_nState);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new Size(570, 40);
            this.pnlTop.TabIndex = 0x4c;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x19f);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(570, 0x31);
            this.panel1.TabIndex = 80;
            this.colcPalletNo.DataPropertyName = "nPalletId";
            this.colcPalletNo.HeaderText = "托盘号";
            this.colcPalletNo.Name = "colcPalletNo";
            this.colcPalletNo.ReadOnly = true;
            this.colcPalletNo.ToolTipText = "托盘号";
            this.colnPalletType.DataPropertyName = "cPalletSpecDesc";
            this.colnPalletType.HeaderText = "托盘类型";
            this.colnPalletType.Name = "colnPalletType";
            this.colnPalletType.ReadOnly = true;
            this.colnPalletType.ToolTipText = "托盘类型";
            this.col_nL.DataPropertyName = "nL";
            this.col_nL.HeaderText = "长";
            this.col_nL.Name = "col_nL";
            this.col_nL.ReadOnly = true;
            this.col_nL.Width = 60;
            this.col_nW.DataPropertyName = "nW";
            this.col_nW.HeaderText = "宽";
            this.col_nW.Name = "col_nW";
            this.col_nW.ReadOnly = true;
            this.col_nW.Width = 60;
            this.col_nH.DataPropertyName = "nH";
            this.col_nH.HeaderText = "高";
            this.col_nH.Name = "col_nH";
            this.col_nH.ReadOnly = true;
            this.col_nH.Width = 60;
            this.col_nHeight.DataPropertyName = "nHeight";
            this.col_nHeight.HeaderText = "货位高度";
            this.col_nHeight.Name = "col_nHeight";
            this.col_nHeight.ReadOnly = true;
            this.col_nHeight.Width = 0x4b;
            this.col_nWeight.DataPropertyName = "fWeight";
            this.col_nWeight.HeaderText = "货位载重";
            this.col_nWeight.Name = "col_nWeight";
            this.col_nWeight.ReadOnly = true;
            this.col_nWeight.Width = 0x4b;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(570, 0x1d0);
            base.Controls.Add(this.grdCellList);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pnlTop);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "FrmSelectPallet";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "选择托盘";
            base.Load += new EventHandler(this.FrmSelectPallet_Load);
            ((ISupportInitialize) this.bdsList).EndInit();
            ((ISupportInitialize) this.grdCellList).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadPalletSpecList()
        {
            string sErr = "";
            string sSql = "select * from TWC_PalletSpec ";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            this.cmb_nState.DataSource = set.Tables["data"];
            this.cmb_nState.DisplayMember = "cPalletSpec";
            this.cmb_nState.ValueMember = "cPalletSpecId";
            if (this.cmb_nState.Items.Count > 0)
            {
                this.cmb_nState.SelectedIndex = 0;
            }
        }

        private bool QueryPalletList(string strCon)
        {
            bool flag = false;
            if (base.DBDataSet.Tables[this.strTbNameMain] != null)
            {
                base.DBDataSet.Tables[this.strTbNameMain].Clear();
            }
            this.grdCellList.AutoGenerateColumns = false;
            string sSql = "select * from V_PalletCell " + strCon;
            string sErr = "";
            base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
            flag = sErr == "";
            if (flag)
            {
                this.bdsList.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
                this.grdCellList.Focus();
                this.grdCellList.DataSource = this.bdsList;
                return flag;
            }
            MessageBox.Show(sErr);
            return flag;
        }

        public bool BIsResult
        {
            get
            {
                return this.bIsResultOK;
            }
            set
            {
                this.bIsResultOK = value;
            }
        }

        public string SelResult
        {
            get
            {
                return this.selResult;
            }
            set
            {
                this.selResult = value;
            }
        }
    }
}

