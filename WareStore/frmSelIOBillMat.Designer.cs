namespace WareStoreMS
{
    partial class frmSelIOBillMat
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
            this.grp_Condition = new System.Windows.Forms.GroupBox();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp_To = new System.Windows.Forms.DateTimePicker();
            this.dtp_From = new System.Windows.Forms.DateTimePicker();
            this.txt_cBNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_cABC = new System.Windows.Forms.ComboBox();
            this.txt_cRemark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_cMatOther = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cMatQCLevel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_cMatStyle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cSpec = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Qry = new System.Windows.Forms.Button();
            this.prgMain = new System.Windows.Forms.ProgressBar();
            this.bds_Data = new System.Windows.Forms.BindingSource(this.components);
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.grp_Buttons = new System.Windows.Forms.GroupBox();
            this.grd_Data = new System.Windows.Forms.DataGridView();
            this.col_cBNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMatStyle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMatQCLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMatOther = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cABC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cBNoIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nItemIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nBClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cCSId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_WHIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_AreaIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PosIdErp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_Condition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Data)).BeginInit();
            this.grp_Buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_Condition
            // 
            this.grp_Condition.Controls.Add(this.chk_Date);
            this.grp_Condition.Controls.Add(this.label7);
            this.grp_Condition.Controls.Add(this.dtp_To);
            this.grp_Condition.Controls.Add(this.dtp_From);
            this.grp_Condition.Controls.Add(this.txt_cBNo);
            this.grp_Condition.Controls.Add(this.label10);
            this.grp_Condition.Controls.Add(this.label9);
            this.grp_Condition.Controls.Add(this.cmb_cABC);
            this.grp_Condition.Controls.Add(this.txt_cRemark);
            this.grp_Condition.Controls.Add(this.label6);
            this.grp_Condition.Controls.Add(this.txt_cMatOther);
            this.grp_Condition.Controls.Add(this.label5);
            this.grp_Condition.Controls.Add(this.txt_cMatQCLevel);
            this.grp_Condition.Controls.Add(this.label4);
            this.grp_Condition.Controls.Add(this.txt_cMatStyle);
            this.grp_Condition.Controls.Add(this.label3);
            this.grp_Condition.Controls.Add(this.txt_cSpec);
            this.grp_Condition.Controls.Add(this.label2);
            this.grp_Condition.Controls.Add(this.txt_cName);
            this.grp_Condition.Controls.Add(this.label1);
            this.grp_Condition.Controls.Add(this.btn_Reset);
            this.grp_Condition.Controls.Add(this.btn_Qry);
            this.grp_Condition.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_Condition.Location = new System.Drawing.Point(0, 0);
            this.grp_Condition.Name = "grp_Condition";
            this.grp_Condition.Size = new System.Drawing.Size(923, 106);
            this.grp_Condition.TabIndex = 3;
            this.grp_Condition.TabStop = false;
            this.grp_Condition.Text = "条件";
            // 
            // chk_Date
            // 
            this.chk_Date.AutoSize = true;
            this.chk_Date.Location = new System.Drawing.Point(245, 22);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.Size = new System.Drawing.Size(72, 16);
            this.chk_Date.TabIndex = 28;
            this.chk_Date.Text = "操作日期";
            this.chk_Date.UseVisualStyleBackColor = true;
            this.chk_Date.CheckedChanged += new System.EventHandler(this.chk_Date_CheckedChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(477, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 1);
            this.label7.TabIndex = 27;
            this.label7.Text = "物料款式";
            // 
            // dtp_To
            // 
            this.dtp_To.Enabled = false;
            this.dtp_To.Location = new System.Drawing.Point(528, 20);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new System.Drawing.Size(134, 21);
            this.dtp_To.TabIndex = 26;
            // 
            // dtp_From
            // 
            this.dtp_From.Enabled = false;
            this.dtp_From.Location = new System.Drawing.Point(327, 20);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new System.Drawing.Size(134, 21);
            this.dtp_From.TabIndex = 25;
            // 
            // txt_cBNo
            // 
            this.txt_cBNo.Location = new System.Drawing.Point(71, 20);
            this.txt_cBNo.Name = "txt_cBNo";
            this.txt_cBNo.Size = new System.Drawing.Size(162, 21);
            this.txt_cBNo.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "单号";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(245, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "ABC";
            // 
            // cmb_cABC
            // 
            this.cmb_cABC.FormattingEnabled = true;
            this.cmb_cABC.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.cmb_cABC.Location = new System.Drawing.Point(299, 76);
            this.cmb_cABC.Name = "cmb_cABC";
            this.cmb_cABC.Size = new System.Drawing.Size(162, 20);
            this.cmb_cABC.TabIndex = 20;
            // 
            // txt_cRemark
            // 
            this.txt_cRemark.Location = new System.Drawing.Point(71, 76);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.Size = new System.Drawing.Size(162, 21);
            this.txt_cRemark.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "物料备注";
            // 
            // txt_cMatOther
            // 
            this.txt_cMatOther.Location = new System.Drawing.Point(749, 49);
            this.txt_cMatOther.Name = "txt_cMatOther";
            this.txt_cMatOther.Size = new System.Drawing.Size(162, 21);
            this.txt_cMatOther.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(690, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "其他属性";
            // 
            // txt_cMatQCLevel
            // 
            this.txt_cMatQCLevel.Location = new System.Drawing.Point(528, 49);
            this.txt_cMatQCLevel.Name = "txt_cMatQCLevel";
            this.txt_cMatQCLevel.Size = new System.Drawing.Size(134, 21);
            this.txt_cMatQCLevel.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "质量等级";
            // 
            // txt_cMatStyle
            // 
            this.txt_cMatStyle.Location = new System.Drawing.Point(299, 49);
            this.txt_cMatStyle.Name = "txt_cMatStyle";
            this.txt_cMatStyle.Size = new System.Drawing.Size(162, 21);
            this.txt_cMatStyle.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "物料款式";
            // 
            // txt_cSpec
            // 
            this.txt_cSpec.Location = new System.Drawing.Point(71, 49);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.Size = new System.Drawing.Size(162, 21);
            this.txt_cSpec.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "物料规格";
            // 
            // txt_cName
            // 
            this.txt_cName.Location = new System.Drawing.Point(749, 20);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.Size = new System.Drawing.Size(162, 21);
            this.txt_cName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(690, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "物料";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(668, 75);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(104, 23);
            this.btn_Reset.TabIndex = 3;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Qry
            // 
            this.btn_Qry.Location = new System.Drawing.Point(528, 75);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new System.Drawing.Size(104, 23);
            this.btn_Qry.TabIndex = 2;
            this.btn_Qry.Text = "查询(&Q)";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new System.EventHandler(this.btn_Qry_Click);
            // 
            // prgMain
            // 
            this.prgMain.Location = new System.Drawing.Point(6, 39);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(909, 21);
            this.prgMain.TabIndex = 2;
            this.prgMain.Visible = false;
            // 
            // bds_Data
            // 
            this.bds_Data.AllowNew = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(303, 11);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(522, 11);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // grp_Buttons
            // 
            this.grp_Buttons.Controls.Add(this.prgMain);
            this.grp_Buttons.Controls.Add(this.btn_Cancel);
            this.grp_Buttons.Controls.Add(this.btn_OK);
            this.grp_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_Buttons.Location = new System.Drawing.Point(0, 481);
            this.grp_Buttons.Name = "grp_Buttons";
            this.grp_Buttons.Size = new System.Drawing.Size(923, 65);
            this.grp_Buttons.TabIndex = 4;
            this.grp_Buttons.TabStop = false;
            // 
            // grd_Data
            // 
            this.grd_Data.AutoGenerateColumns = false;
            this.grd_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_cBNo,
            this.col_nItem,
            this.col_cMNo,
            this.col_cMName,
            this.col_cSpec,
            this.col_cMatStyle,
            this.col_cMatQCLevel,
            this.col_cMatOther,
            this.col_cABC,
            this.col_cRemark,
            this.col_cBatchNo,
            this.col_fQty,
            this.col_cUnit,
            this.col_cBNoIn,
            this.col_nItemIn,
            this.col_fWeight,
            this.col_nBClass,
            this.col_cCSId,
            this.col_cSupplier,
            this.col_WHIdErp,
            this.col_AreaIdErp,
            this.col_PosIdErp});
            this.grd_Data.DataSource = this.bds_Data;
            this.grd_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Data.Location = new System.Drawing.Point(0, 106);
            this.grd_Data.Name = "grd_Data";
            this.grd_Data.ReadOnly = true;
            this.grd_Data.RowHeadersVisible = false;
            this.grd_Data.RowTemplate.Height = 23;
            this.grd_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd_Data.Size = new System.Drawing.Size(923, 375);
            this.grd_Data.TabIndex = 6;
            this.grd_Data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Data_CellDoubleClick);
            // 
            // col_cBNo
            // 
            this.col_cBNo.DataPropertyName = "cBNo";
            this.col_cBNo.HeaderText = "单号";
            this.col_cBNo.Name = "col_cBNo";
            this.col_cBNo.ReadOnly = true;
            // 
            // col_nItem
            // 
            this.col_nItem.DataPropertyName = "nItem";
            this.col_nItem.HeaderText = "单明细号";
            this.col_nItem.Name = "col_nItem";
            this.col_nItem.ReadOnly = true;
            this.col_nItem.Width = 65;
            // 
            // col_cMNo
            // 
            this.col_cMNo.DataPropertyName = "cMNo";
            this.col_cMNo.HeaderText = "物料编码";
            this.col_cMNo.Name = "col_cMNo";
            this.col_cMNo.ReadOnly = true;
            // 
            // col_cMName
            // 
            this.col_cMName.DataPropertyName = "cMName";
            this.col_cMName.HeaderText = "物料名称";
            this.col_cMName.Name = "col_cMName";
            this.col_cMName.ReadOnly = true;
            // 
            // col_cSpec
            // 
            this.col_cSpec.DataPropertyName = "cSpec";
            this.col_cSpec.HeaderText = "规格型号";
            this.col_cSpec.Name = "col_cSpec";
            this.col_cSpec.ReadOnly = true;
            // 
            // col_cMatStyle
            // 
            this.col_cMatStyle.DataPropertyName = "cMatStyle";
            this.col_cMatStyle.HeaderText = "款式";
            this.col_cMatStyle.Name = "col_cMatStyle";
            this.col_cMatStyle.ReadOnly = true;
            this.col_cMatStyle.Width = 70;
            // 
            // col_cMatQCLevel
            // 
            this.col_cMatQCLevel.DataPropertyName = "cMatQCLevel";
            this.col_cMatQCLevel.HeaderText = "质量等级";
            this.col_cMatQCLevel.Name = "col_cMatQCLevel";
            this.col_cMatQCLevel.ReadOnly = true;
            this.col_cMatQCLevel.Width = 70;
            // 
            // col_cMatOther
            // 
            this.col_cMatOther.DataPropertyName = "cMatOther";
            this.col_cMatOther.HeaderText = "其他物料属性";
            this.col_cMatOther.Name = "col_cMatOther";
            this.col_cMatOther.ReadOnly = true;
            this.col_cMatOther.Width = 70;
            // 
            // col_cABC
            // 
            this.col_cABC.DataPropertyName = "cABC";
            this.col_cABC.HeaderText = "ABC";
            this.col_cABC.Name = "col_cABC";
            this.col_cABC.ReadOnly = true;
            // 
            // col_cRemark
            // 
            this.col_cRemark.DataPropertyName = "cRemark";
            this.col_cRemark.HeaderText = "物料备注";
            this.col_cRemark.Name = "col_cRemark";
            this.col_cRemark.ReadOnly = true;
            this.col_cRemark.Width = 70;
            // 
            // col_cBatchNo
            // 
            this.col_cBatchNo.DataPropertyName = "cBatchNo";
            this.col_cBatchNo.HeaderText = "批号";
            this.col_cBatchNo.Name = "col_cBatchNo";
            this.col_cBatchNo.ReadOnly = true;
            // 
            // col_fQty
            // 
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "数量";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            this.col_fQty.Width = 80;
            // 
            // col_cUnit
            // 
            this.col_cUnit.DataPropertyName = "cUnit";
            this.col_cUnit.HeaderText = "单位";
            this.col_cUnit.Name = "col_cUnit";
            this.col_cUnit.ReadOnly = true;
            this.col_cUnit.Width = 50;
            // 
            // col_cBNoIn
            // 
            this.col_cBNoIn.DataPropertyName = "cBNoIn";
            this.col_cBNoIn.HeaderText = "库存入库单号";
            this.col_cBNoIn.Name = "col_cBNoIn";
            this.col_cBNoIn.ReadOnly = true;
            // 
            // col_nItemIn
            // 
            this.col_nItemIn.DataPropertyName = "nItemIn";
            this.col_nItemIn.HeaderText = "库存入库单明细号";
            this.col_nItemIn.Name = "col_nItemIn";
            this.col_nItemIn.ReadOnly = true;
            // 
            // col_fWeight
            // 
            this.col_fWeight.DataPropertyName = "fWeight";
            this.col_fWeight.HeaderText = "单位重量";
            this.col_fWeight.Name = "col_fWeight";
            this.col_fWeight.ReadOnly = true;
            this.col_fWeight.Width = 70;
            // 
            // col_nBClass
            // 
            this.col_nBClass.DataPropertyName = "nBClass";
            this.col_nBClass.HeaderText = "单据类别";
            this.col_nBClass.Name = "col_nBClass";
            this.col_nBClass.ReadOnly = true;
            this.col_nBClass.Visible = false;
            // 
            // col_cCSId
            // 
            this.col_cCSId.DataPropertyName = "cCSId";
            this.col_cCSId.HeaderText = "供应商编码";
            this.col_cCSId.Name = "col_cCSId";
            this.col_cCSId.ReadOnly = true;
            this.col_cCSId.Visible = false;
            // 
            // col_cSupplier
            // 
            this.col_cSupplier.DataPropertyName = "cSupplier";
            this.col_cSupplier.HeaderText = "供应商/生产商";
            this.col_cSupplier.Name = "col_cSupplier";
            this.col_cSupplier.ReadOnly = true;
            // 
            // col_WHIdErp
            // 
            this.col_WHIdErp.DataPropertyName = "cWHIdErp";
            this.col_WHIdErp.HeaderText = "ERP仓库号";
            this.col_WHIdErp.Name = "col_WHIdErp";
            this.col_WHIdErp.ReadOnly = true;
            // 
            // col_AreaIdErp
            // 
            this.col_AreaIdErp.DataPropertyName = "cAreaIdErp";
            this.col_AreaIdErp.HeaderText = "ERP货区号";
            this.col_AreaIdErp.Name = "col_AreaIdErp";
            this.col_AreaIdErp.ReadOnly = true;
            // 
            // col_PosIdErp
            // 
            this.col_PosIdErp.DataPropertyName = "cPosIdErp";
            this.col_PosIdErp.HeaderText = "ERP货位号";
            this.col_PosIdErp.Name = "col_PosIdErp";
            this.col_PosIdErp.ReadOnly = true;
            // 
            // frmSelIOBillMat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(923, 546);
            this.Controls.Add(this.grd_Data);
            this.Controls.Add(this.grp_Condition);
            this.Controls.Add(this.grp_Buttons);
            this.MinimizeBox = false;
            this.Name = "frmSelIOBillMat";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "选择出入库单物料";
            this.Load += new System.EventHandler(this.frmSelIOBillMat_Load);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Data)).EndInit();
            this.grp_Buttons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Condition;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_cABC;
        private System.Windows.Forms.TextBox txt_cRemark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_cMatOther;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cMatQCLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_cMatStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cSpec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Qry;
        private System.Windows.Forms.ProgressBar prgMain;
        private System.Windows.Forms.BindingSource bds_Data;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox grp_Buttons;
        private System.Windows.Forms.DataGridView grd_Data;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_To;
        private System.Windows.Forms.DateTimePicker dtp_From;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chk_Date;
        public System.Windows.Forms.TextBox txt_cName;
        public System.Windows.Forms.TextBox txt_cBNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cBNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMatStyle;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMatQCLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMatOther;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cABC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cBNoIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nItemIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nBClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cCSId;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_WHIdErp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_AreaIdErp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_PosIdErp;
    }
}
