namespace WareStoreMS
{
    partial class frmChkPosList
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmb_Station = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_ChkNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.colcBNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcPosId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnPalletId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsGrid = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmb_Station);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.lbl_ChkNo);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(420, 35);
            this.pnlTop.TabIndex = 0;
            // 
            // cmb_Station
            // 
            this.cmb_Station.FormattingEnabled = true;
            this.cmb_Station.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cmb_Station.Location = new System.Drawing.Point(282, 6);
            this.cmb_Station.Name = "cmb_Station";
            this.cmb_Station.Size = new System.Drawing.Size(87, 20);
            this.cmb_Station.TabIndex = 3;
            this.cmb_Station.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "拣选口号：";
            // 
            // lbl_ChkNo
            // 
            this.lbl_ChkNo.AutoSize = true;
            this.lbl_ChkNo.Location = new System.Drawing.Point(83, 10);
            this.lbl_ChkNo.Name = "lbl_ChkNo";
            this.lbl_ChkNo.Size = new System.Drawing.Size(65, 12);
            this.lbl_ChkNo.TabIndex = 1;
            this.lbl_ChkNo.Text = "盘点单号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "盘点单号：";
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 365);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(420, 39);
            this.pnlButton.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(237, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(108, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcBNo,
            this.colcPosId,
            this.colnPalletId,
            this.colcStatus,
            this.colnStatus});
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 35);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.RowHeadersVisible = false;
            this.grdData.RowTemplate.Height = 23;
            this.grdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdData.Size = new System.Drawing.Size(420, 330);
            this.grdData.TabIndex = 2;
            this.toolTip1.SetToolTip(this.grdData, "拖动鼠标，可以多选");
            // 
            // colcBNo
            // 
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.HeaderText = "单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            // 
            // colcPosId
            // 
            this.colcPosId.DataPropertyName = "cPosId";
            this.colcPosId.HeaderText = "货位号";
            this.colcPosId.Name = "colcPosId";
            this.colcPosId.ReadOnly = true;
            // 
            // colnPalletId
            // 
            this.colnPalletId.DataPropertyName = "nPalletId";
            this.colnPalletId.HeaderText = "托盘号";
            this.colnPalletId.Name = "colnPalletId";
            this.colnPalletId.ReadOnly = true;
            // 
            // colcStatus
            // 
            this.colcStatus.DataPropertyName = "cStatus";
            this.colcStatus.HeaderText = "货位状态";
            this.colcStatus.Name = "colcStatus";
            this.colcStatus.ReadOnly = true;
            // 
            // colnStatus
            // 
            this.colnStatus.DataPropertyName = "nStatus";
            this.colnStatus.HeaderText = "状态号";
            this.colnStatus.Name = "colnStatus";
            this.colnStatus.ReadOnly = true;
            // 
            // frmChkPosList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(420, 404);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlTop);
            this.MinimizeBox = false;
            this.Name = "frmChkPosList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "盘点任务列表";
            this.Load += new System.EventHandler(this.frmChkPosList_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.Label lbl_ChkNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource bsGrid;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Station;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcPosId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnPalletId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnStatus;
    }
}
