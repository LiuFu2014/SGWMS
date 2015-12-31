namespace SunEast.App
{
    partial class frmInnerAccont
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView_HasPallet = new System.Windows.Forms.DataGridView();
            this.ColBad = new System.Windows.Forms.DataGridViewButtonColumn();
            this.nWorkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nPalletId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_plt_cWKStatusDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nWKStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lbfqty = new System.Windows.Forms.Label();
            this.lbtaskFinNum = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Dtl_InnerAccont = new System.Windows.Forms.Button();
            this.bindingSource_HasPallet = new System.Windows.Forms.BindingSource(this.components);
            this.lbffinished = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbfpallet = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btn_Check = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_HasPallet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_HasPallet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_HasPallet
            // 
            this.dataGridView_HasPallet.AllowUserToAddRows = false;
            this.dataGridView_HasPallet.AllowUserToDeleteRows = false;
            this.dataGridView_HasPallet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_HasPallet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColBad,
            this.nWorkId,
            this.Column11,
            this.nPalletId,
            this.Column9,
            this.Column6,
            this.colcName,
            this.Column8,
            this.col_plt_cWKStatusDesc,
            this.Column4,
            this.Column5,
            this.colcSpec,
            this.Column7,
            this.nWKStatus,
            this.Column1,
            this.Column2});
            this.dataGridView_HasPallet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView_HasPallet.Location = new System.Drawing.Point(0, 89);
            this.dataGridView_HasPallet.Name = "dataGridView_HasPallet";
            this.dataGridView_HasPallet.ReadOnly = true;
            this.dataGridView_HasPallet.RowHeadersVisible = false;
            this.dataGridView_HasPallet.RowTemplate.Height = 23;
            this.dataGridView_HasPallet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_HasPallet.Size = new System.Drawing.Size(983, 473);
            this.dataGridView_HasPallet.TabIndex = 14;
            this.dataGridView_HasPallet.Tag = "8";
            this.dataGridView_HasPallet.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_HasPallet_DataBindingComplete);
            this.dataGridView_HasPallet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_HasPallet_CellContentClick);
            // 
            // ColBad
            // 
            this.ColBad.HeaderText = "合格登记";
            this.ColBad.Name = "ColBad";
            this.ColBad.ReadOnly = true;
            this.ColBad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColBad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColBad.Text = "";
            this.ColBad.Width = 60;
            // 
            // nWorkId
            // 
            this.nWorkId.DataPropertyName = "nWorkId";
            this.nWorkId.HeaderText = "任务号";
            this.nWorkId.Name = "nWorkId";
            this.nWorkId.ReadOnly = true;
            this.nWorkId.Width = 65;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "cOptTypeDesc";
            this.Column11.HeaderText = "操作类型";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 65;
            // 
            // nPalletId
            // 
            this.nPalletId.DataPropertyName = "nPalletId";
            this.nPalletId.HeaderText = "托盘号";
            this.nPalletId.Name = "nPalletId";
            this.nPalletId.ReadOnly = true;
            this.nPalletId.Width = 50;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "cPosIdTo";
            this.Column9.HeaderText = "目标货位";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 65;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "cMNo";
            this.Column6.HeaderText = "物料号";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 80;
            // 
            // colcName
            // 
            this.colcName.DataPropertyName = "cName";
            this.colcName.HeaderText = "物料名";
            this.colcName.Name = "colcName";
            this.colcName.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "fQty";
            this.Column8.HeaderText = "数量";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 70;
            // 
            // col_plt_cWKStatusDesc
            // 
            this.col_plt_cWKStatusDesc.DataPropertyName = "cWKStatusDesc";
            this.col_plt_cWKStatusDesc.HeaderText = "执行状态";
            this.col_plt_cWKStatusDesc.Name = "col_plt_cWKStatusDesc";
            this.col_plt_cWKStatusDesc.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "cBNo";
            this.Column4.HeaderText = "单号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "nItem";
            this.Column5.HeaderText = "项次";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 65;
            // 
            // colcSpec
            // 
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "cBatchNo";
            this.Column7.HeaderText = "批号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 70;
            // 
            // nWKStatus
            // 
            this.nWKStatus.DataPropertyName = "nWKStatus";
            this.nWKStatus.HeaderText = "执行状态";
            this.nWKStatus.Name = "nWKStatus";
            this.nWKStatus.ReadOnly = true;
            this.nWKStatus.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "nOptStation";
            this.Column1.HeaderText = "入库口";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 65;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cWHId";
            this.Column2.HeaderText = "仓库号码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "入库单数：";
            // 
            // lbfqty
            // 
            this.lbfqty.AutoSize = true;
            this.lbfqty.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbfqty.ForeColor = System.Drawing.Color.Red;
            this.lbfqty.Location = new System.Drawing.Point(128, 19);
            this.lbfqty.Name = "lbfqty";
            this.lbfqty.Size = new System.Drawing.Size(17, 16);
            this.lbfqty.TabIndex = 16;
            this.lbfqty.Text = "0";
            // 
            // lbtaskFinNum
            // 
            this.lbtaskFinNum.AutoSize = true;
            this.lbtaskFinNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbtaskFinNum.ForeColor = System.Drawing.Color.Red;
            this.lbtaskFinNum.Location = new System.Drawing.Point(283, 57);
            this.lbtaskFinNum.Name = "lbtaskFinNum";
            this.lbtaskFinNum.Size = new System.Drawing.Size(17, 16);
            this.lbtaskFinNum.TabIndex = 18;
            this.lbtaskFinNum.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(29, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "配盘数：";
            // 
            // btn_Dtl_InnerAccont
            // 
            this.btn_Dtl_InnerAccont.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Dtl_InnerAccont.Location = new System.Drawing.Point(399, 19);
            this.btn_Dtl_InnerAccont.Name = "btn_Dtl_InnerAccont";
            this.btn_Dtl_InnerAccont.Size = new System.Drawing.Size(124, 45);
            this.btn_Dtl_InnerAccont.TabIndex = 59;
            this.btn_Dtl_InnerAccont.Text = "整单无差异登记";
            this.btn_Dtl_InnerAccont.UseVisualStyleBackColor = true;
            this.btn_Dtl_InnerAccont.Click += new System.EventHandler(this.btn_Dtl_InnerAccont_Click);
            // 
            // lbffinished
            // 
            this.lbffinished.AutoSize = true;
            this.lbffinished.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbffinished.ForeColor = System.Drawing.Color.Red;
            this.lbffinished.Location = new System.Drawing.Point(283, 18);
            this.lbffinished.Name = "lbffinished";
            this.lbffinished.Size = new System.Drawing.Size(17, 16);
            this.lbffinished.TabIndex = 61;
            this.lbffinished.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(184, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "已登记数：";
            // 
            // lbfpallet
            // 
            this.lbfpallet.AutoSize = true;
            this.lbfpallet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbfpallet.ForeColor = System.Drawing.Color.Red;
            this.lbfpallet.Location = new System.Drawing.Point(128, 57);
            this.lbfpallet.Name = "lbfpallet";
            this.lbfpallet.Size = new System.Drawing.Size(17, 16);
            this.lbfpallet.TabIndex = 63;
            this.lbfpallet.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(184, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 62;
            this.label6.Text = "待登记数：";
            // 
            // btRefresh
            // 
            this.btRefresh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btRefresh.Location = new System.Drawing.Point(598, 19);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(61, 45);
            this.btRefresh.TabIndex = 64;
            this.btRefresh.Text = "刷新";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btn_Check
            // 
            this.btn_Check.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Check.Location = new System.Drawing.Point(675, 19);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(82, 45);
            this.btn_Check.TabIndex = 64;
            this.btn_Check.Text = "出库查看";
            this.btn_Check.UseVisualStyleBackColor = true;
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // frmInnerAccont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 562);
            this.Controls.Add(this.btn_Check);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.lbfpallet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbffinished);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Dtl_InnerAccont);
            this.Controls.Add(this.lbtaskFinNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbfqty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_HasPallet);
            this.Name = "frmInnerAccont";
            this.Text = "入库差异登记";
            this.Load += new System.EventHandler(this.frmInnerAccont_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_HasPallet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_HasPallet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_HasPallet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbfqty;
        private System.Windows.Forms.Label lbtaskFinNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Dtl_InnerAccont;
        private System.Windows.Forms.BindingSource bindingSource_HasPallet;
        private System.Windows.Forms.Label lbffinished;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbfpallet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.DataGridViewButtonColumn ColBad;
        private System.Windows.Forms.DataGridViewTextBoxColumn nWorkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn nPalletId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_plt_cWKStatusDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn nWKStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btn_Check;
    }
}