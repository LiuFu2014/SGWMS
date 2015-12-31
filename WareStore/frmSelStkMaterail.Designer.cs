namespace WareStoreMS
{
    partial class frmSelStkMaterail
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
            this.grpConidtion = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Qry = new System.Windows.Forms.Button();
            this.cmb_nQCStatus = new System.Windows.Forms.ComboBox();
            this.dtp_dFrom = new System.Windows.Forms.DateTimePicker();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.txt_cBNoIn = new System.Windows.Forms.TextBox();
            this.dtp_To = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_QCDay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cMNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_MatType1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colcMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldBadDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcBNoIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnItemIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcQCStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bdsList = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpConidtion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConidtion
            // 
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
            this.grpConidtion.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpConidtion.Location = new System.Drawing.Point(0, 0);
            this.grpConidtion.Name = "grpConidtion";
            this.grpConidtion.Size = new System.Drawing.Size(777, 107);
            this.grpConidtion.TabIndex = 1;
            this.grpConidtion.TabStop = false;
            this.grpConidtion.Text = "条件";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(201, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 1);
            this.label7.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "质检状态：";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(656, 76);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(94, 23);
            this.btn_Reset.TabIndex = 14;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Qry
            // 
            this.btn_Qry.Location = new System.Drawing.Point(556, 76);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new System.Drawing.Size(94, 23);
            this.btn_Qry.TabIndex = 13;
            this.btn_Qry.Text = "查询";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new System.EventHandler(this.btn_Qry_Click);
            // 
            // cmb_nQCStatus
            // 
            this.cmb_nQCStatus.FormattingEnabled = true;
            this.cmb_nQCStatus.Location = new System.Drawing.Point(374, 44);
            this.cmb_nQCStatus.Name = "cmb_nQCStatus";
            this.cmb_nQCStatus.Size = new System.Drawing.Size(121, 20);
            this.cmb_nQCStatus.TabIndex = 12;
            // 
            // dtp_dFrom
            // 
            this.dtp_dFrom.CustomFormat = "";
            this.dtp_dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_dFrom.Location = new System.Drawing.Point(112, 44);
            this.dtp_dFrom.Name = "dtp_dFrom";
            this.dtp_dFrom.Size = new System.Drawing.Size(84, 21);
            this.dtp_dFrom.TabIndex = 2;
            // 
            // chk_Date
            // 
            this.chk_Date.AutoSize = true;
            this.chk_Date.Location = new System.Drawing.Point(23, 46);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.Size = new System.Drawing.Size(84, 16);
            this.chk_Date.TabIndex = 11;
            this.chk_Date.Text = "入库日期：";
            this.chk_Date.UseVisualStyleBackColor = true;
            // 
            // txt_cBNoIn
            // 
            this.txt_cBNoIn.Location = new System.Drawing.Point(112, 14);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.Size = new System.Drawing.Size(189, 21);
            this.txt_cBNoIn.TabIndex = 0;
            // 
            // dtp_To
            // 
            this.dtp_To.CustomFormat = "";
            this.dtp_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_To.Location = new System.Drawing.Point(217, 44);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new System.Drawing.Size(84, 21);
            this.dtp_To.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(733, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "天";
            // 
            // txt_QCDay
            // 
            this.txt_QCDay.Location = new System.Drawing.Point(588, 44);
            this.txt_QCDay.Name = "txt_QCDay";
            this.txt_QCDay.Size = new System.Drawing.Size(139, 21);
            this.txt_QCDay.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(505, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "离到期天数：";
            // 
            // txt_cMNo
            // 
            this.txt_cMNo.Location = new System.Drawing.Point(550, 14);
            this.txt_cMNo.Name = "txt_cMNo";
            this.txt_cMNo.Size = new System.Drawing.Size(200, 21);
            this.txt_cMNo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(505, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "物料：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "物料类别：";
            // 
            // cmb_MatType1
            // 
            this.cmb_MatType1.FormattingEnabled = true;
            this.cmb_MatType1.Location = new System.Drawing.Point(374, 14);
            this.cmb_MatType1.Name = "cmb_MatType1";
            this.cmb_MatType1.Size = new System.Drawing.Size(121, 20);
            this.cmb_MatType1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "库存入库单号：";
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcMNo,
            this.colcMName,
            this.colcSpec,
            this.colcBatchNo,
            this.coldBadDate,
            this.colcBNoIn,
            this.colnItemIn,
            this.colfQty,
            this.colcUnit,
            this.colcQCStatus});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 107);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowTemplate.Height = 23;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(777, 353);
            this.grdList.TabIndex = 2;
            this.grdList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            // 
            // colcMNo
            // 
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编号";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            // 
            // colcMName
            // 
            this.colcMName.DataPropertyName = "cMName";
            this.colcMName.HeaderText = "物料名称";
            this.colcMName.Name = "colcMName";
            this.colcMName.ReadOnly = true;
            // 
            // colcSpec
            // 
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "物料规格";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            // 
            // colcBatchNo
            // 
            this.colcBatchNo.DataPropertyName = "cBatchNo";
            this.colcBatchNo.HeaderText = "批号";
            this.colcBatchNo.Name = "colcBatchNo";
            this.colcBatchNo.ReadOnly = true;
            // 
            // coldBadDate
            // 
            this.coldBadDate.DataPropertyName = "dBadDate";
            this.coldBadDate.HeaderText = "有效期";
            this.coldBadDate.Name = "coldBadDate";
            this.coldBadDate.ReadOnly = true;
            // 
            // colcBNoIn
            // 
            this.colcBNoIn.DataPropertyName = "cBNoIn";
            this.colcBNoIn.HeaderText = "入库单号";
            this.colcBNoIn.Name = "colcBNoIn";
            this.colcBNoIn.ReadOnly = true;
            // 
            // colnItemIn
            // 
            this.colnItemIn.DataPropertyName = "nItemIn";
            this.colnItemIn.HeaderText = "入库单序";
            this.colnItemIn.Name = "colnItemIn";
            this.colnItemIn.ReadOnly = true;
            // 
            // colfQty
            // 
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            // 
            // colcUnit
            // 
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            // 
            // colcQCStatus
            // 
            this.colcQCStatus.DataPropertyName = "cQCStatus";
            this.colcQCStatus.HeaderText = "质检状态";
            this.colcQCStatus.Name = "colcQCStatus";
            this.colcQCStatus.ReadOnly = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(351, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 45);
            this.panel1.TabIndex = 15;
            // 
            // frmSelStkMaterail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(777, 505);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpConidtion);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmSelStkMaterail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "库存物料";
            this.Load += new System.EventHandler(this.frmSelStkMaterail_Load);
            this.grpConidtion.ResumeLayout(false);
            this.grpConidtion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConidtion;
        private System.Windows.Forms.TextBox txt_cBNoIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_MatType1;
        private System.Windows.Forms.DateTimePicker dtp_dFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cMNo;
        private System.Windows.Forms.TextBox txt_QCDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.BindingSource bdsList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtp_To;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.ComboBox cmb_nQCStatus;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btn_Qry;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldBadDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcBNoIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnItemIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colfQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcQCStatus;
        private System.Windows.Forms.Panel panel1;
    }
}
