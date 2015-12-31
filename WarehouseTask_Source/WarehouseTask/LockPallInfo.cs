namespace WarehouseTask
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class LockPallInfo : FrmSTable
    {
        private Button btnExit;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private IContainer components = null;
        private DataGridView dgvPallReceInfo;
        private Label label1;
        private Label lblSum;
        public DataTable mydt = new DataTable();
        private Panel panBtnInfo;

        public LockPallInfo()
        {
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
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

        private void InitializeComponent()
        {
            this.btnExit = new Button();
            this.panBtnInfo = new Panel();
            this.dgvPallReceInfo = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.label1 = new Label();
            this.lblSum = new Label();
            this.panBtnInfo.SuspendLayout();
            ((ISupportInitialize) this.dgvPallReceInfo).BeginInit();
            base.SuspendLayout();
            this.btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnExit.Location = new Point(0x215, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x4b, 0x17);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退出(&Q)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.panBtnInfo.Controls.Add(this.lblSum);
            this.panBtnInfo.Controls.Add(this.label1);
            this.panBtnInfo.Controls.Add(this.btnExit);
            this.panBtnInfo.Dock = DockStyle.Bottom;
            this.panBtnInfo.Location = new Point(0, 0xdf);
            this.panBtnInfo.Name = "panBtnInfo";
            this.panBtnInfo.Size = new Size(620, 0x29);
            this.panBtnInfo.TabIndex = 1;
            this.dgvPallReceInfo.AllowUserToAddRows = false;
            this.dgvPallReceInfo.AllowUserToDeleteRows = false;
            this.dgvPallReceInfo.AllowUserToOrderColumns = true;
            this.dgvPallReceInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPallReceInfo.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column7, this.Column2, this.Column8, this.Column3, this.Column4, this.Column5, this.Column9, this.Column6, this.Column10 });
            this.dgvPallReceInfo.Dock = DockStyle.Fill;
            this.dgvPallReceInfo.Location = new Point(0, 0);
            this.dgvPallReceInfo.Name = "dgvPallReceInfo";
            this.dgvPallReceInfo.RowHeadersVisible = false;
            this.dgvPallReceInfo.RowTemplate.Height = 0x17;
            this.dgvPallReceInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPallReceInfo.Size = new Size(620, 0xdf);
            this.dgvPallReceInfo.TabIndex = 2;
            this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.DataPropertyName = "nPalletId";
            this.Column1.HeaderText = "托盘号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 0x42;
            this.Column7.DataPropertyName = "cMNo";
            this.Column7.HeaderText = "物料号";
            this.Column7.Name = "Column7";
            this.Column7.Width = 0x42;
            this.Column2.DataPropertyName = "cname";
            this.Column2.HeaderText = "物料名称";
            this.Column2.Name = "Column2";
            this.Column2.Width = 0x4e;
            this.Column8.DataPropertyName = "cspec";
            this.Column8.HeaderText = "物料规格";
            this.Column8.Name = "Column8";
            this.Column8.Width = 0x4e;
            this.Column3.DataPropertyName = "fQty";
            this.Column3.HeaderText = "数量";
            this.Column3.Name = "Column3";
            this.Column3.Width = 0x36;
            this.Column4.DataPropertyName = "cUnit";
            this.Column4.HeaderText = "单位";
            this.Column4.Name = "Column4";
            this.Column4.Width = 0x36;
            this.Column5.DataPropertyName = "cSupplier";
            this.Column5.HeaderText = "供货商";
            this.Column5.Name = "Column5";
            this.Column9.DataPropertyName = "cmatstyle";
            this.Column9.HeaderText = "物料款式";
            this.Column9.Name = "Column9";
            this.Column6.DataPropertyName = "cDtlRemark";
            this.Column6.HeaderText = "备注";
            this.Column6.Name = "Column6";
            this.Column10.DataPropertyName = "cmatother";
            this.Column10.HeaderText = "其它属性";
            this.Column10.Name = "Column10";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(240, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数量总和：";
            this.lblSum.AutoSize = true;
            this.lblSum.Font = new Font("宋体", 15f);
            this.lblSum.Location = new Point(0x148, 10);
            this.lblSum.Name = "lblSum";
            this.lblSum.Size = new Size(0x27, 20);
            this.lblSum.TabIndex = 1;
            this.lblSum.Text = "0.0";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(620, 0x108);
            base.Controls.Add(this.dgvPallReceInfo);
            base.Controls.Add(this.panBtnInfo);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "LockPallInfo";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "托盘的详细物料列表";
            base.Load += new EventHandler(this.LockPallInfo_Load);
            this.panBtnInfo.ResumeLayout(false);
            this.panBtnInfo.PerformLayout();
            ((ISupportInitialize) this.dgvPallReceInfo).EndInit();
            base.ResumeLayout(false);
        }

        private void LockPallInfo_Load(object sender, EventArgs e)
        {
            if (this.mydt != null)
            {
                this.dgvPallReceInfo.AutoGenerateColumns = false;
                this.dgvPallReceInfo.DataSource = this.mydt;
                double num = 0.0;
                foreach (DataRow row in this.mydt.Rows)
                {
                    try
                    {
                        num += Convert.ToDouble(row["fQty"].ToString());
                    }
                    catch
                    {
                    }
                }
                this.lblSum.Text = num.ToString();
            }
        }
    }
}

