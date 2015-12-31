namespace WareStoreMS
{
    partial class frmSelMaterial
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
            this.label12 = new System.Windows.Forms.Label();
            this.txt_cDtlRemark = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_cSupplier = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_cABC = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_cTypeId1 = new System.Windows.Forms.ComboBox();
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
            this.grp_Buttons = new System.Windows.Forms.GroupBox();
            this.prgMain = new System.Windows.Forms.ProgressBar();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.grd_Data = new System.Windows.Forms.DataGridView();
            this.col_cMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMatStyle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMatQCLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cDtlRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fQtyBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fSafeQtyDn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fSafeQtyUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cTypeId1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cType1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cType2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cTypeId2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cABC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nKeepDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cCSId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nMatClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cMatOther = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bds_Data = new System.Windows.Forms.BindingSource(this.components);
            this.chk_DateIn = new System.Windows.Forms.CheckBox();
            this.dtp_From = new System.Windows.Forms.DateTimePicker();
            this.dtp_To = new System.Windows.Forms.DateTimePicker();
            this.grpErpCondition = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_ERPWHId = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmb_ERPUnitId = new System.Windows.Forms.ComboBox();
            this.grp_Condition.SuspendLayout();
            this.grp_Buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Data)).BeginInit();
            this.grpErpCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Condition
            // 
            this.grp_Condition.Controls.Add(this.label12);
            this.grp_Condition.Controls.Add(this.grpErpCondition);
            this.grp_Condition.Controls.Add(this.dtp_To);
            this.grp_Condition.Controls.Add(this.dtp_From);
            this.grp_Condition.Controls.Add(this.chk_DateIn);
            this.grp_Condition.Controls.Add(this.txt_cDtlRemark);
            this.grp_Condition.Controls.Add(this.label11);
            this.grp_Condition.Controls.Add(this.txt_cSupplier);
            this.grp_Condition.Controls.Add(this.label10);
            this.grp_Condition.Controls.Add(this.label9);
            this.grp_Condition.Controls.Add(this.cmb_cABC);
            this.grp_Condition.Controls.Add(this.label8);
            this.grp_Condition.Controls.Add(this.cmb_cTypeId1);
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
            this.grp_Condition.Size = new System.Drawing.Size(1159, 107);
            this.grp_Condition.TabIndex = 0;
            this.grp_Condition.TabStop = false;
            this.grp_Condition.Text = "条件";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Location = new System.Drawing.Point(14, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1128, 1);
            this.label12.TabIndex = 26;
            // 
            // txt_cDtlRemark
            // 
            this.txt_cDtlRemark.Location = new System.Drawing.Point(678, 41);
            this.txt_cDtlRemark.Name = "txt_cDtlRemark";
            this.txt_cDtlRemark.Size = new System.Drawing.Size(149, 21);
            this.txt_cDtlRemark.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(619, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "库存备注";
            // 
            // txt_cSupplier
            // 
            this.txt_cSupplier.Location = new System.Drawing.Point(71, 74);
            this.txt_cSupplier.Name = "txt_cSupplier";
            this.txt_cSupplier.Size = new System.Drawing.Size(176, 21);
            this.txt_cSupplier.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 25);
            this.label10.TabIndex = 22;
            this.label10.Text = "供应商/生产厂家：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(511, 43);
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
            this.cmb_cABC.Location = new System.Drawing.Point(537, 41);
            this.cmb_cABC.Name = "cmb_cABC";
            this.cmb_cABC.Size = new System.Drawing.Size(66, 20);
            this.cmb_cABC.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "物料类型";
            // 
            // cmb_cTypeId1
            // 
            this.cmb_cTypeId1.FormattingEnabled = true;
            this.cmb_cTypeId1.Location = new System.Drawing.Point(71, 41);
            this.cmb_cTypeId1.Name = "cmb_cTypeId1";
            this.cmb_cTypeId1.Size = new System.Drawing.Size(176, 20);
            this.cmb_cTypeId1.TabIndex = 18;
            // 
            // txt_cRemark
            // 
            this.txt_cRemark.Location = new System.Drawing.Point(309, 41);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.Size = new System.Drawing.Size(162, 21);
            this.txt_cRemark.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(253, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "物料备注";
            // 
            // txt_cMatOther
            // 
            this.txt_cMatOther.Location = new System.Drawing.Point(976, 15);
            this.txt_cMatOther.Name = "txt_cMatOther";
            this.txt_cMatOther.Size = new System.Drawing.Size(162, 21);
            this.txt_cMatOther.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(917, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "其他属性";
            // 
            // txt_cMatQCLevel
            // 
            this.txt_cMatQCLevel.Location = new System.Drawing.Point(739, 15);
            this.txt_cMatQCLevel.Name = "txt_cMatQCLevel";
            this.txt_cMatQCLevel.Size = new System.Drawing.Size(172, 21);
            this.txt_cMatQCLevel.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(684, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "质量等级";
            // 
            // txt_cMatStyle
            // 
            this.txt_cMatStyle.Location = new System.Drawing.Point(537, 15);
            this.txt_cMatStyle.Name = "txt_cMatStyle";
            this.txt_cMatStyle.Size = new System.Drawing.Size(141, 21);
            this.txt_cMatStyle.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(481, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "物料款式";
            // 
            // txt_cSpec
            // 
            this.txt_cSpec.Location = new System.Drawing.Point(309, 15);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.Size = new System.Drawing.Size(162, 21);
            this.txt_cSpec.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "物料规格";
            // 
            // txt_cName
            // 
            this.txt_cName.Location = new System.Drawing.Point(71, 15);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.Size = new System.Drawing.Size(176, 21);
            this.txt_cName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "物料名称";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(1064, 75);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 3;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Qry
            // 
            this.btn_Qry.Location = new System.Drawing.Point(985, 75);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new System.Drawing.Size(75, 23);
            this.btn_Qry.TabIndex = 2;
            this.btn_Qry.Text = "查询(&Q)";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new System.EventHandler(this.btn_Qry_Click);
            // 
            // grp_Buttons
            // 
            this.grp_Buttons.Controls.Add(this.prgMain);
            this.grp_Buttons.Controls.Add(this.btn_Cancel);
            this.grp_Buttons.Controls.Add(this.btn_OK);
            this.grp_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_Buttons.Location = new System.Drawing.Point(0, 473);
            this.grp_Buttons.Name = "grp_Buttons";
            this.grp_Buttons.Size = new System.Drawing.Size(1159, 65);
            this.grp_Buttons.TabIndex = 1;
            this.grp_Buttons.TabStop = false;
            // 
            // prgMain
            // 
            this.prgMain.Location = new System.Drawing.Point(6, 39);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(909, 21);
            this.prgMain.TabIndex = 2;
            this.prgMain.Visible = false;
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
            // grd_Data
            // 
            this.grd_Data.AutoGenerateColumns = false;
            this.grd_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_cMNo,
            this.col_cName,
            this.col_cSpec,
            this.col_cMatStyle,
            this.col_cMatQCLevel,
            this.col_cSupplier,
            this.col_fQty,
            this.col_cUnit,
            this.col_cDtlRemark,
            this.col_fWeight,
            this.col_fQtyBox,
            this.col_fSafeQtyDn,
            this.col_fSafeQtyUp,
            this.col_cTypeId1,
            this.col_cType1,
            this.col_cType2,
            this.col_cTypeId2,
            this.col_cABC,
            this.col_nKeepDay,
            this.col_cCSId,
            this.col_nMatClass,
            this.col_cMatOther,
            this.col_cRemark});
            this.grd_Data.DataSource = this.bds_Data;
            this.grd_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Data.Location = new System.Drawing.Point(0, 107);
            this.grd_Data.Name = "grd_Data";
            this.grd_Data.ReadOnly = true;
            this.grd_Data.RowHeadersVisible = false;
            this.grd_Data.RowTemplate.Height = 23;
            this.grd_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd_Data.Size = new System.Drawing.Size(1159, 366);
            this.grd_Data.TabIndex = 2;
            this.grd_Data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Data_CellDoubleClick);
            // 
            // col_cMNo
            // 
            this.col_cMNo.DataPropertyName = "cMNo";
            this.col_cMNo.HeaderText = "物料编码";
            this.col_cMNo.Name = "col_cMNo";
            this.col_cMNo.ReadOnly = true;
            // 
            // col_cName
            // 
            this.col_cName.DataPropertyName = "cName";
            this.col_cName.HeaderText = "物料名称";
            this.col_cName.Name = "col_cName";
            this.col_cName.ReadOnly = true;
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
            // 
            // col_cMatQCLevel
            // 
            this.col_cMatQCLevel.DataPropertyName = "cMatQCLevel";
            this.col_cMatQCLevel.HeaderText = "质量等级";
            this.col_cMatQCLevel.Name = "col_cMatQCLevel";
            this.col_cMatQCLevel.ReadOnly = true;
            this.col_cMatQCLevel.Width = 70;
            // 
            // col_cSupplier
            // 
            this.col_cSupplier.DataPropertyName = "cSupplier";
            this.col_cSupplier.HeaderText = "供应商/生产商";
            this.col_cSupplier.Name = "col_cSupplier";
            this.col_cSupplier.ReadOnly = true;
            // 
            // col_fQty
            // 
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "当前库存数";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            // 
            // col_cUnit
            // 
            this.col_cUnit.DataPropertyName = "cUnit";
            this.col_cUnit.HeaderText = "计量单位";
            this.col_cUnit.Name = "col_cUnit";
            this.col_cUnit.ReadOnly = true;
            this.col_cUnit.Width = 70;
            // 
            // col_cDtlRemark
            // 
            this.col_cDtlRemark.DataPropertyName = "cDtlRemark";
            this.col_cDtlRemark.HeaderText = "明细备注";
            this.col_cDtlRemark.Name = "col_cDtlRemark";
            this.col_cDtlRemark.ReadOnly = true;
            // 
            // col_fWeight
            // 
            this.col_fWeight.DataPropertyName = "fWeight";
            this.col_fWeight.HeaderText = "单位重量";
            this.col_fWeight.Name = "col_fWeight";
            this.col_fWeight.ReadOnly = true;
            // 
            // col_fQtyBox
            // 
            this.col_fQtyBox.DataPropertyName = "fQtyBox";
            this.col_fQtyBox.HeaderText = "每盘数量";
            this.col_fQtyBox.Name = "col_fQtyBox";
            this.col_fQtyBox.ReadOnly = true;
            this.col_fQtyBox.Width = 70;
            // 
            // col_fSafeQtyDn
            // 
            this.col_fSafeQtyDn.DataPropertyName = "fSaftQtyDn";
            this.col_fSafeQtyDn.HeaderText = "安全库存下限";
            this.col_fSafeQtyDn.Name = "col_fSafeQtyDn";
            this.col_fSafeQtyDn.ReadOnly = true;
            // 
            // col_fSafeQtyUp
            // 
            this.col_fSafeQtyUp.DataPropertyName = "fSafeQtyUp";
            this.col_fSafeQtyUp.HeaderText = "安全库存上限";
            this.col_fSafeQtyUp.Name = "col_fSafeQtyUp";
            this.col_fSafeQtyUp.ReadOnly = true;
            // 
            // col_cTypeId1
            // 
            this.col_cTypeId1.DataPropertyName = "cTypeId1";
            this.col_cTypeId1.HeaderText = "物料类型编码";
            this.col_cTypeId1.Name = "col_cTypeId1";
            this.col_cTypeId1.ReadOnly = true;
            // 
            // col_cType1
            // 
            this.col_cType1.DataPropertyName = "cType1";
            this.col_cType1.HeaderText = "物料类型";
            this.col_cType1.Name = "col_cType1";
            this.col_cType1.ReadOnly = true;
            // 
            // col_cType2
            // 
            this.col_cType2.DataPropertyName = "cType2";
            this.col_cType2.HeaderText = "会计类型";
            this.col_cType2.Name = "col_cType2";
            this.col_cType2.ReadOnly = true;
            // 
            // col_cTypeId2
            // 
            this.col_cTypeId2.DataPropertyName = "cTypeId2";
            this.col_cTypeId2.HeaderText = "会计类型编码";
            this.col_cTypeId2.Name = "col_cTypeId2";
            this.col_cTypeId2.ReadOnly = true;
            // 
            // col_cABC
            // 
            this.col_cABC.DataPropertyName = "cABC";
            this.col_cABC.HeaderText = "ABC属性";
            this.col_cABC.Name = "col_cABC";
            this.col_cABC.ReadOnly = true;
            // 
            // col_nKeepDay
            // 
            this.col_nKeepDay.DataPropertyName = "nKeepDay";
            this.col_nKeepDay.HeaderText = "保质天数";
            this.col_nKeepDay.Name = "col_nKeepDay";
            this.col_nKeepDay.ReadOnly = true;
            // 
            // col_cCSId
            // 
            this.col_cCSId.DataPropertyName = "cCSId";
            this.col_cCSId.HeaderText = "供应商编码";
            this.col_cCSId.Name = "col_cCSId";
            this.col_cCSId.ReadOnly = true;
            // 
            // col_nMatClass
            // 
            this.col_nMatClass.DataPropertyName = "nMatClass";
            this.col_nMatClass.HeaderText = "物料类别";
            this.col_nMatClass.Name = "col_nMatClass";
            this.col_nMatClass.ReadOnly = true;
            // 
            // col_cMatOther
            // 
            this.col_cMatOther.DataPropertyName = "cMatOther";
            this.col_cMatOther.HeaderText = "其他物料属性";
            this.col_cMatOther.Name = "col_cMatOther";
            this.col_cMatOther.ReadOnly = true;
            // 
            // col_cRemark
            // 
            this.col_cRemark.DataPropertyName = "cRemark";
            this.col_cRemark.HeaderText = "物料备注";
            this.col_cRemark.Name = "col_cRemark";
            this.col_cRemark.ReadOnly = true;
            // 
            // bds_Data
            // 
            this.bds_Data.AllowNew = false;
            // 
            // chk_DateIn
            // 
            this.chk_DateIn.AutoSize = true;
            this.chk_DateIn.Location = new System.Drawing.Point(839, 43);
            this.chk_DateIn.Name = "chk_DateIn";
            this.chk_DateIn.Size = new System.Drawing.Size(84, 16);
            this.chk_DateIn.TabIndex = 27;
            this.chk_DateIn.Text = "入库时间：";
            this.chk_DateIn.UseVisualStyleBackColor = true;
            // 
            // dtp_From
            // 
            this.dtp_From.Location = new System.Drawing.Point(919, 40);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new System.Drawing.Size(98, 21);
            this.dtp_From.TabIndex = 28;
            // 
            // dtp_To
            // 
            this.dtp_To.Location = new System.Drawing.Point(1041, 41);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new System.Drawing.Size(98, 21);
            this.dtp_To.TabIndex = 29;
            // 
            // grpErpCondition
            // 
            this.grpErpCondition.Controls.Add(this.cmb_ERPUnitId);
            this.grpErpCondition.Controls.Add(this.cmb_ERPWHId);
            this.grpErpCondition.Controls.Add(this.label13);
            this.grpErpCondition.Controls.Add(this.label7);
            this.grpErpCondition.Location = new System.Drawing.Point(261, 66);
            this.grpErpCondition.Name = "grpErpCondition";
            this.grpErpCondition.Size = new System.Drawing.Size(352, 38);
            this.grpErpCondition.TabIndex = 30;
            this.grpErpCondition.TabStop = false;
            this.grpErpCondition.Text = "ERP条件";
            this.grpErpCondition.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "仓库：";
            // 
            // cmb_ERPWHId
            // 
            this.cmb_ERPWHId.FormattingEnabled = true;
            this.cmb_ERPWHId.Location = new System.Drawing.Point(48, 11);
            this.cmb_ERPWHId.Name = "cmb_ERPWHId";
            this.cmb_ERPWHId.Size = new System.Drawing.Size(115, 20);
            this.cmb_ERPWHId.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(170, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 23;
            this.label13.Text = "厂别：";
            // 
            // cmb_ERPUnitId
            // 
            this.cmb_ERPUnitId.FormattingEnabled = true;
            this.cmb_ERPUnitId.Location = new System.Drawing.Point(213, 12);
            this.cmb_ERPUnitId.Name = "cmb_ERPUnitId";
            this.cmb_ERPUnitId.Size = new System.Drawing.Size(133, 20);
            this.cmb_ERPUnitId.TabIndex = 22;
            // 
            // frmSelMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1159, 538);
            this.Controls.Add(this.grd_Data);
            this.Controls.Add(this.grp_Buttons);
            this.Controls.Add(this.grp_Condition);
            this.Name = "frmSelMaterial";
            this.Text = "物料查询";
            this.Load += new System.EventHandler(this.frmSelMaterial_Load);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            this.grp_Buttons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Data)).EndInit();
            this.grpErpCondition.ResumeLayout(false);
            this.grpErpCondition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Condition;
        private System.Windows.Forms.GroupBox grp_Buttons;
        private System.Windows.Forms.DataGridView grd_Data;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_Qry;
        private System.Windows.Forms.TextBox txt_cName;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_cTypeId1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_cABC;
        private System.Windows.Forms.BindingSource bds_Data;
        private System.Windows.Forms.ProgressBar prgMain;
        private System.Windows.Forms.TextBox txt_cSupplier;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_cDtlRemark;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMatStyle;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMatQCLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cDtlRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fQtyBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fSafeQtyDn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fSafeQtyUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cTypeId1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cType1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cType2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cTypeId2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cABC;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nKeepDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cCSId;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nMatClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cMatOther;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cRemark;
        private System.Windows.Forms.DateTimePicker dtp_To;
        private System.Windows.Forms.DateTimePicker dtp_From;
        private System.Windows.Forms.CheckBox chk_DateIn;
        private System.Windows.Forms.GroupBox grpErpCondition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_ERPWHId;
        private System.Windows.Forms.ComboBox cmb_ERPUnitId;
        private System.Windows.Forms.Label label13;
    }
}
