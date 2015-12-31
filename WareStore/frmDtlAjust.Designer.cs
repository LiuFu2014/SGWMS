namespace WareStoreMS
{
    partial class frmDtlAjust
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
            this.grdEdit = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_fQty = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_cBatchNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_cSpec = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_BillIn = new System.Windows.Forms.Label();
            this.txt_nItemIn = new System.Windows.Forms.TextBox();
            this.txt_cBNoIn = new System.Windows.Forms.TextBox();
            this.lbl_PosId = new System.Windows.Forms.Label();
            this.txt_cPosId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_MNo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cUnit = new System.Windows.Forms.TextBox();
            this.txt_cWHId = new System.Windows.Forms.TextBox();
            this.txt_nPalletId = new System.Windows.Forms.TextBox();
            this.txt_cMNo = new System.Windows.Forms.TextBox();
            this.txt_cBoxId = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cWHIdErp = new System.Windows.Forms.TextBox();
            this.txt_cAreaIdErp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cPosIdErp = new System.Windows.Forms.TextBox();
            this.grdEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdEdit
            // 
            this.grdEdit.Controls.Add(this.label3);
            this.grdEdit.Controls.Add(this.txt_cPosIdErp);
            this.grdEdit.Controls.Add(this.label1);
            this.grdEdit.Controls.Add(this.label2);
            this.grdEdit.Controls.Add(this.txt_cWHIdErp);
            this.grdEdit.Controls.Add(this.txt_cAreaIdErp);
            this.grdEdit.Controls.Add(this.label10);
            this.grdEdit.Controls.Add(this.txt_fQty);
            this.grdEdit.Controls.Add(this.label13);
            this.grdEdit.Controls.Add(this.txt_cBatchNo);
            this.grdEdit.Controls.Add(this.label4);
            this.grdEdit.Controls.Add(this.txt_cSpec);
            this.grdEdit.Controls.Add(this.label8);
            this.grdEdit.Controls.Add(this.lbl_BillIn);
            this.grdEdit.Controls.Add(this.txt_nItemIn);
            this.grdEdit.Controls.Add(this.txt_cBNoIn);
            this.grdEdit.Controls.Add(this.lbl_PosId);
            this.grdEdit.Controls.Add(this.txt_cPosId);
            this.grdEdit.Controls.Add(this.label7);
            this.grdEdit.Controls.Add(this.label12);
            this.grdEdit.Controls.Add(this.label6);
            this.grdEdit.Controls.Add(this.lbl_MNo);
            this.grdEdit.Controls.Add(this.label5);
            this.grdEdit.Controls.Add(this.txt_cUnit);
            this.grdEdit.Controls.Add(this.txt_cWHId);
            this.grdEdit.Controls.Add(this.txt_nPalletId);
            this.grdEdit.Controls.Add(this.txt_cMNo);
            this.grdEdit.Controls.Add(this.txt_cBoxId);
            this.grdEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdEdit.Location = new System.Drawing.Point(0, 0);
            this.grdEdit.Name = "grdEdit";
            this.grdEdit.Size = new System.Drawing.Size(467, 190);
            this.grdEdit.TabIndex = 51;
            this.grdEdit.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 52;
            this.label10.Text = "物料规格";
            // 
            // txt_fQty
            // 
            this.txt_fQty.Location = new System.Drawing.Point(290, 164);
            this.txt_fQty.Name = "txt_fQty";
            this.txt_fQty.Size = new System.Drawing.Size(100, 21);
            this.txt_fQty.TabIndex = 43;
            this.txt_fQty.Tag = "0";
            this.txt_fQty.Text = "0";
            this.toolTip1.SetToolTip(this.txt_fQty, "实盘数-账面数");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(44, 112);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 51;
            this.label13.Text = "物料批号";
            // 
            // txt_cBatchNo
            // 
            this.txt_cBatchNo.Enabled = false;
            this.txt_cBatchNo.Location = new System.Drawing.Point(109, 112);
            this.txt_cBatchNo.Name = "txt_cBatchNo";
            this.txt_cBatchNo.ReadOnly = true;
            this.txt_cBatchNo.Size = new System.Drawing.Size(100, 21);
            this.txt_cBatchNo.TabIndex = 50;
            this.txt_cBatchNo.Tag = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 50;
            this.label4.Text = "数量";
            // 
            // txt_cSpec
            // 
            this.txt_cSpec.Location = new System.Drawing.Point(288, 112);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.ReadOnly = true;
            this.txt_cSpec.Size = new System.Drawing.Size(100, 21);
            this.txt_cSpec.TabIndex = 49;
            this.txt_cSpec.Tag = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 48;
            this.label8.Text = "明细项次";
            // 
            // lbl_BillIn
            // 
            this.lbl_BillIn.AutoSize = true;
            this.lbl_BillIn.Enabled = false;
            this.lbl_BillIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_BillIn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_BillIn.Location = new System.Drawing.Point(44, 87);
            this.lbl_BillIn.Name = "lbl_BillIn";
            this.lbl_BillIn.Size = new System.Drawing.Size(53, 12);
            this.lbl_BillIn.TabIndex = 47;
            this.lbl_BillIn.Text = "入库单号";
            this.toolTip1.SetToolTip(this.lbl_BillIn, "点击，对新增物料选择入库时的单号及明细序号");
            this.lbl_BillIn.Click += new System.EventHandler(this.lblBillIn_Click);
            // 
            // txt_nItemIn
            // 
            this.txt_nItemIn.Enabled = false;
            this.txt_nItemIn.Location = new System.Drawing.Point(288, 87);
            this.txt_nItemIn.Name = "txt_nItemIn";
            this.txt_nItemIn.ReadOnly = true;
            this.txt_nItemIn.Size = new System.Drawing.Size(100, 21);
            this.txt_nItemIn.TabIndex = 46;
            this.txt_nItemIn.Tag = "0";
            // 
            // txt_cBNoIn
            // 
            this.txt_cBNoIn.Location = new System.Drawing.Point(109, 87);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.ReadOnly = true;
            this.txt_cBNoIn.Size = new System.Drawing.Size(100, 21);
            this.txt_cBNoIn.TabIndex = 45;
            this.txt_cBNoIn.Tag = "0";
            // 
            // lbl_PosId
            // 
            this.lbl_PosId.AutoSize = true;
            this.lbl_PosId.Enabled = false;
            this.lbl_PosId.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PosId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_PosId.Location = new System.Drawing.Point(229, 15);
            this.lbl_PosId.Name = "lbl_PosId";
            this.lbl_PosId.Size = new System.Drawing.Size(53, 12);
            this.lbl_PosId.TabIndex = 44;
            this.lbl_PosId.Text = "货位号码";
            this.toolTip1.SetToolTip(this.lbl_PosId, "点击，可对新添物料选择货位号");
            this.lbl_PosId.Click += new System.EventHandler(this.lbl_PosId_Click);
            // 
            // txt_cPosId
            // 
            this.txt_cPosId.Location = new System.Drawing.Point(288, 15);
            this.txt_cPosId.Name = "txt_cPosId";
            this.txt_cPosId.ReadOnly = true;
            this.txt_cPosId.Size = new System.Drawing.Size(100, 21);
            this.txt_cPosId.TabIndex = 43;
            this.txt_cPosId.Tag = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(44, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 42;
            this.label7.Text = "仓库号码";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(229, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 39;
            this.label12.Text = "物料单位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "周转箱号";
            // 
            // lbl_MNo
            // 
            this.lbl_MNo.AutoSize = true;
            this.lbl_MNo.Enabled = false;
            this.lbl_MNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_MNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MNo.Location = new System.Drawing.Point(44, 62);
            this.lbl_MNo.Name = "lbl_MNo";
            this.lbl_MNo.Size = new System.Drawing.Size(53, 12);
            this.lbl_MNo.TabIndex = 37;
            this.lbl_MNo.Text = "物料号码";
            this.toolTip1.SetToolTip(this.lbl_MNo, "点击，可增加现库存中不存在物料");
            this.lbl_MNo.Click += new System.EventHandler(this.lblMNo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "托盘号码";
            // 
            // txt_cUnit
            // 
            this.txt_cUnit.Enabled = false;
            this.txt_cUnit.Location = new System.Drawing.Point(288, 62);
            this.txt_cUnit.Name = "txt_cUnit";
            this.txt_cUnit.ReadOnly = true;
            this.txt_cUnit.Size = new System.Drawing.Size(100, 21);
            this.txt_cUnit.TabIndex = 34;
            this.txt_cUnit.Tag = "0";
            // 
            // txt_cWHId
            // 
            this.txt_cWHId.Enabled = false;
            this.txt_cWHId.Location = new System.Drawing.Point(109, 15);
            this.txt_cWHId.Name = "txt_cWHId";
            this.txt_cWHId.ReadOnly = true;
            this.txt_cWHId.Size = new System.Drawing.Size(100, 21);
            this.txt_cWHId.TabIndex = 32;
            this.txt_cWHId.Tag = "0";
            // 
            // txt_nPalletId
            // 
            this.txt_nPalletId.Enabled = false;
            this.txt_nPalletId.Location = new System.Drawing.Point(109, 38);
            this.txt_nPalletId.Name = "txt_nPalletId";
            this.txt_nPalletId.ReadOnly = true;
            this.txt_nPalletId.Size = new System.Drawing.Size(100, 21);
            this.txt_nPalletId.TabIndex = 31;
            this.txt_nPalletId.Tag = "0";
            // 
            // txt_cMNo
            // 
            this.txt_cMNo.Location = new System.Drawing.Point(109, 62);
            this.txt_cMNo.Name = "txt_cMNo";
            this.txt_cMNo.ReadOnly = true;
            this.txt_cMNo.Size = new System.Drawing.Size(100, 21);
            this.txt_cMNo.TabIndex = 29;
            this.txt_cMNo.Tag = "0";
            // 
            // txt_cBoxId
            // 
            this.txt_cBoxId.Location = new System.Drawing.Point(288, 40);
            this.txt_cBoxId.Name = "txt_cBoxId";
            this.txt_cBoxId.Size = new System.Drawing.Size(100, 21);
            this.txt_cBoxId.TabIndex = 27;
            this.txt_cBoxId.Tag = "0";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(257, 196);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(139, 196);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 44;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "ERP货区号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "ERP仓库号";
            // 
            // txt_cWHIdErp
            // 
            this.txt_cWHIdErp.Enabled = false;
            this.txt_cWHIdErp.Location = new System.Drawing.Point(110, 138);
            this.txt_cWHIdErp.Name = "txt_cWHIdErp";
            this.txt_cWHIdErp.ReadOnly = true;
            this.txt_cWHIdErp.Size = new System.Drawing.Size(100, 21);
            this.txt_cWHIdErp.TabIndex = 54;
            this.txt_cWHIdErp.Tag = "0";
            // 
            // txt_cAreaIdErp
            // 
            this.txt_cAreaIdErp.Location = new System.Drawing.Point(289, 138);
            this.txt_cAreaIdErp.Name = "txt_cAreaIdErp";
            this.txt_cAreaIdErp.ReadOnly = true;
            this.txt_cAreaIdErp.Size = new System.Drawing.Size(100, 21);
            this.txt_cAreaIdErp.TabIndex = 53;
            this.txt_cAreaIdErp.Tag = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 58;
            this.label3.Text = "ERP货位号";
            // 
            // txt_cPosIdErp
            // 
            this.txt_cPosIdErp.Enabled = false;
            this.txt_cPosIdErp.Location = new System.Drawing.Point(109, 165);
            this.txt_cPosIdErp.Name = "txt_cPosIdErp";
            this.txt_cPosIdErp.ReadOnly = true;
            this.txt_cPosIdErp.Size = new System.Drawing.Size(100, 21);
            this.txt_cPosIdErp.TabIndex = 57;
            this.txt_cPosIdErp.Tag = "0";
            // 
            // frmDtlAjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(467, 226);
            this.Controls.Add(this.grdEdit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmDtlAjust";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "调整单明细";
            this.Load += new System.EventHandler(this.frmDtlAjust_Load);
            this.grdEdit.ResumeLayout(false);
            this.grdEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grdEdit;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_cBatchNo;
        private System.Windows.Forms.TextBox txt_cSpec;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label lbl_BillIn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txt_nItemIn;
        private System.Windows.Forms.TextBox txt_cBNoIn;
        public System.Windows.Forms.Label lbl_PosId;
        private System.Windows.Forms.TextBox txt_cPosId;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lbl_MNo;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cUnit;
        private System.Windows.Forms.TextBox txt_cWHId;
        private System.Windows.Forms.TextBox txt_nPalletId;
        private System.Windows.Forms.TextBox txt_cMNo;
        private System.Windows.Forms.TextBox txt_cBoxId;
        private System.Windows.Forms.TextBox txt_fQty;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cPosIdErp;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_cWHIdErp;
        private System.Windows.Forms.TextBox txt_cAreaIdErp;
    }
}
