namespace WareStoreMS
{
    partial class frmSelBillInDtl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.colcBNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnQCStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldProdDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lbl_cMNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_cWHId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bsGrid = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.pnlButton.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcBNo,
            this.colnItem,
            this.colcBatchNo,
            this.colcUnit,
            this.colnQCStatus,
            this.coldProdDate});
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 35);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(602, 280);
            this.grdData.TabIndex = 5;
            this.toolTip1.SetToolTip(this.grdData, "双击，选择");
            this.grdData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdData_CellDoubleClick);
            // 
            // colcBNo
            // 
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.HeaderText = "单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            // 
            // colnItem
            // 
            this.colnItem.DataPropertyName = "nItem";
            this.colnItem.HeaderText = "单明细序号";
            this.colnItem.Name = "colnItem";
            this.colnItem.ReadOnly = true;
            this.colnItem.Width = 90;
            // 
            // colcBatchNo
            // 
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            // 
            // colcUnit
            // 
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "计量单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            // 
            // colnQCStatus
            // 
            this.colnQCStatus.DataPropertyName = "nQCStatus";
            this.colnQCStatus.HeaderText = "质检状态";
            this.colnQCStatus.Name = "colnQCStatus";
            this.colnQCStatus.ReadOnly = true;
            this.colnQCStatus.Visible = false;
            // 
            // coldProdDate
            // 
            this.coldProdDate.DataPropertyName = "dProdDate";
            this.coldProdDate.HeaderText = "生产日期";
            this.coldProdDate.Name = "coldProdDate";
            this.coldProdDate.ReadOnly = true;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 315);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(602, 39);
            this.pnlButton.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(328, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(199, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lbl_cMNo);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.lbl_cWHId);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(602, 35);
            this.pnlTop.TabIndex = 3;
            // 
            // lbl_cMNo
            // 
            this.lbl_cMNo.Location = new System.Drawing.Point(195, 11);
            this.lbl_cMNo.Name = "lbl_cMNo";
            this.lbl_cMNo.Size = new System.Drawing.Size(130, 12);
            this.lbl_cMNo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "物料号：";
            // 
            // lbl_cWHId
            // 
            this.lbl_cWHId.Location = new System.Drawing.Point(62, 11);
            this.lbl_cWHId.Name = "lbl_cWHId";
            this.lbl_cWHId.Size = new System.Drawing.Size(82, 12);
            this.lbl_cWHId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "仓库号：";
            // 
            // frmSelBillInDtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(602, 354);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlTop);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmSelBillInDtl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "选择库存入库单明细";
            this.Load += new System.EventHandler(this.frmSelBillInDtl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource bsGrid;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnQCStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldProdDate;
        public System.Windows.Forms.Label lbl_cWHId;
        public System.Windows.Forms.Label lbl_cMNo;
    }
}
