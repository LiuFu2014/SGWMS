namespace UserMS
{
    partial class frmSelCuSupplier
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.prgMain = new System.Windows.Forms.ProgressBar();
            this.grp_Buttons = new System.Windows.Forms.GroupBox();
            this.btn_Qry = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_nIsFactory = new System.Windows.Forms.ComboBox();
            this.txt_cName = new System.Windows.Forms.TextBox();
            this.lbl_Factory = new System.Windows.Forms.Label();
            this.cmb_nIsInner = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grp_Condition = new System.Windows.Forms.GroupBox();
            this.bds_Data = new System.Windows.Forms.BindingSource(this.components);
            this.grd_Data = new System.Windows.Forms.DataGridView();
            this.col_cCSId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cCSNameJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cCSNameQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cTel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nIsInner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cIsInner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_bUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_nIsFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cIsFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_Buttons.SuspendLayout();
            this.grp_Condition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Data)).BeginInit();
            this.SuspendLayout();
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
            // prgMain
            // 
            this.prgMain.Location = new System.Drawing.Point(6, 39);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(909, 21);
            this.prgMain.TabIndex = 2;
            this.prgMain.Visible = false;
            // 
            // grp_Buttons
            // 
            this.grp_Buttons.Controls.Add(this.prgMain);
            this.grp_Buttons.Controls.Add(this.btn_Cancel);
            this.grp_Buttons.Controls.Add(this.btn_OK);
            this.grp_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_Buttons.Location = new System.Drawing.Point(0, 424);
            this.grp_Buttons.Name = "grp_Buttons";
            this.grp_Buttons.Size = new System.Drawing.Size(814, 65);
            this.grp_Buttons.TabIndex = 4;
            this.grp_Buttons.TabStop = false;
            // 
            // btn_Qry
            // 
            this.btn_Qry.Location = new System.Drawing.Point(547, 57);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new System.Drawing.Size(64, 23);
            this.btn_Qry.TabIndex = 2;
            this.btn_Qry.Text = "查询(&Q)";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new System.EventHandler(this.btn_Qry_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(617, 57);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(64, 23);
            this.btn_Reset.TabIndex = 3;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "名称：";
            // 
            // cmb_nIsFactory
            // 
            this.cmb_nIsFactory.FormattingEnabled = true;
            this.cmb_nIsFactory.Location = new System.Drawing.Point(241, 56);
            this.cmb_nIsFactory.Name = "cmb_nIsFactory";
            this.cmb_nIsFactory.Size = new System.Drawing.Size(60, 20);
            this.cmb_nIsFactory.TabIndex = 5;
            // 
            // txt_cName
            // 
            this.txt_cName.Location = new System.Drawing.Point(68, 19);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.Size = new System.Drawing.Size(613, 21);
            this.txt_cName.TabIndex = 6;
            // 
            // lbl_Factory
            // 
            this.lbl_Factory.AutoSize = true;
            this.lbl_Factory.Location = new System.Drawing.Point(168, 56);
            this.lbl_Factory.Name = "lbl_Factory";
            this.lbl_Factory.Size = new System.Drawing.Size(77, 12);
            this.lbl_Factory.TabIndex = 17;
            this.lbl_Factory.Text = "是否生产商：";
            // 
            // cmb_nIsInner
            // 
            this.cmb_nIsInner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_nIsInner.FormattingEnabled = true;
            this.cmb_nIsInner.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmb_nIsInner.Location = new System.Drawing.Point(105, 54);
            this.cmb_nIsInner.Name = "cmb_nIsInner";
            this.cmb_nIsInner.Size = new System.Drawing.Size(58, 20);
            this.cmb_nIsInner.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "是否属内部单位：";
            // 
            // grp_Condition
            // 
            this.grp_Condition.Controls.Add(this.cmb_nIsFactory);
            this.grp_Condition.Controls.Add(this.cmb_nIsInner);
            this.grp_Condition.Controls.Add(this.label9);
            this.grp_Condition.Controls.Add(this.lbl_Factory);
            this.grp_Condition.Controls.Add(this.txt_cName);
            this.grp_Condition.Controls.Add(this.label1);
            this.grp_Condition.Controls.Add(this.btn_Reset);
            this.grp_Condition.Controls.Add(this.btn_Qry);
            this.grp_Condition.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_Condition.Location = new System.Drawing.Point(0, 0);
            this.grp_Condition.Name = "grp_Condition";
            this.grp_Condition.Size = new System.Drawing.Size(814, 91);
            this.grp_Condition.TabIndex = 3;
            this.grp_Condition.TabStop = false;
            this.grp_Condition.Text = "条件";
            // 
            // bds_Data
            // 
            this.bds_Data.AllowNew = false;
            // 
            // grd_Data
            // 
            this.grd_Data.AutoGenerateColumns = false;
            this.grd_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_cCSId,
            this.col_cCSNameJ,
            this.col_cCSNameQ,
            this.col_nType,
            this.col_cType,
            this.col_cTel,
            this.col_cFax,
            this.col_cAddress,
            this.col_cRemark,
            this.col_nIsInner,
            this.col_cIsInner,
            this.col_bUsed,
            this.col_cUsed,
            this.col_nIsFactory,
            this.col_cIsFactory});
            this.grd_Data.DataSource = this.bds_Data;
            this.grd_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Data.Location = new System.Drawing.Point(0, 91);
            this.grd_Data.Name = "grd_Data";
            this.grd_Data.ReadOnly = true;
            this.grd_Data.RowHeadersVisible = false;
            this.grd_Data.RowTemplate.Height = 23;
            this.grd_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd_Data.Size = new System.Drawing.Size(814, 333);
            this.grd_Data.TabIndex = 6;
            this.grd_Data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Data_CellDoubleClick);
            // 
            // col_cCSId
            // 
            this.col_cCSId.DataPropertyName = "cCSId";
            this.col_cCSId.HeaderText = "编码";
            this.col_cCSId.Name = "col_cCSId";
            this.col_cCSId.ReadOnly = true;
            // 
            // col_cCSNameJ
            // 
            this.col_cCSNameJ.DataPropertyName = "cCSNameJ";
            this.col_cCSNameJ.HeaderText = "简称";
            this.col_cCSNameJ.Name = "col_cCSNameJ";
            this.col_cCSNameJ.ReadOnly = true;
            // 
            // col_cCSNameQ
            // 
            this.col_cCSNameQ.DataPropertyName = "cCSNameQ";
            this.col_cCSNameQ.HeaderText = "全称";
            this.col_cCSNameQ.Name = "col_cCSNameQ";
            this.col_cCSNameQ.ReadOnly = true;
            // 
            // col_nType
            // 
            this.col_nType.DataPropertyName = "nType";
            this.col_nType.HeaderText = "类型编码";
            this.col_nType.Name = "col_nType";
            this.col_nType.ReadOnly = true;
            // 
            // col_cType
            // 
            this.col_cType.DataPropertyName = "cType";
            this.col_cType.HeaderText = "类型";
            this.col_cType.Name = "col_cType";
            this.col_cType.ReadOnly = true;
            // 
            // col_cTel
            // 
            this.col_cTel.DataPropertyName = "cTel";
            this.col_cTel.HeaderText = "联系电话";
            this.col_cTel.Name = "col_cTel";
            this.col_cTel.ReadOnly = true;
            // 
            // col_cFax
            // 
            this.col_cFax.DataPropertyName = "cFax";
            this.col_cFax.HeaderText = "传真";
            this.col_cFax.Name = "col_cFax";
            this.col_cFax.ReadOnly = true;
            // 
            // col_cAddress
            // 
            this.col_cAddress.DataPropertyName = "cAddress";
            this.col_cAddress.HeaderText = "地址";
            this.col_cAddress.Name = "col_cAddress";
            this.col_cAddress.ReadOnly = true;
            // 
            // col_cRemark
            // 
            this.col_cRemark.DataPropertyName = "cRemark";
            this.col_cRemark.HeaderText = "备注";
            this.col_cRemark.Name = "col_cRemark";
            this.col_cRemark.ReadOnly = true;
            // 
            // col_nIsInner
            // 
            this.col_nIsInner.DataPropertyName = "nIsInner";
            this.col_nIsInner.HeaderText = "是否内部";
            this.col_nIsInner.Name = "col_nIsInner";
            this.col_nIsInner.ReadOnly = true;
            this.col_nIsInner.Visible = false;
            // 
            // col_cIsInner
            // 
            this.col_cIsInner.DataPropertyName = "cIsInner";
            this.col_cIsInner.HeaderText = "是否内部单位描述";
            this.col_cIsInner.Name = "col_cIsInner";
            this.col_cIsInner.ReadOnly = true;
            // 
            // col_bUsed
            // 
            this.col_bUsed.DataPropertyName = "bUsed";
            this.col_bUsed.HeaderText = "是否启用";
            this.col_bUsed.Name = "col_bUsed";
            this.col_bUsed.ReadOnly = true;
            this.col_bUsed.Visible = false;
            // 
            // col_cUsed
            // 
            this.col_cUsed.DataPropertyName = "cUsed";
            this.col_cUsed.HeaderText = "是否启用描述";
            this.col_cUsed.Name = "col_cUsed";
            this.col_cUsed.ReadOnly = true;
            this.col_cUsed.Visible = false;
            // 
            // col_nIsFactory
            // 
            this.col_nIsFactory.DataPropertyName = "nIsFactory";
            this.col_nIsFactory.HeaderText = "是否生产厂家";
            this.col_nIsFactory.Name = "col_nIsFactory";
            this.col_nIsFactory.ReadOnly = true;
            this.col_nIsFactory.Visible = false;
            // 
            // col_cIsFactory
            // 
            this.col_cIsFactory.DataPropertyName = "cIsFactory";
            this.col_cIsFactory.HeaderText = "是否生产厂家";
            this.col_cIsFactory.Name = "col_cIsFactory";
            this.col_cIsFactory.ReadOnly = true;
            // 
            // frmSelCuSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(814, 489);
            this.Controls.Add(this.grd_Data);
            this.Controls.Add(this.grp_Condition);
            this.Controls.Add(this.grp_Buttons);
            this.KeyPreview = true;
            this.Name = "frmSelCuSupplier";
            this.Text = "选择供应商";
            this.Load += new System.EventHandler(this.frmSelCuSupplier_Load);
            this.grp_Buttons.ResumeLayout(false);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bds_Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ProgressBar prgMain;
        private System.Windows.Forms.GroupBox grp_Buttons;
        private System.Windows.Forms.Button btn_Qry;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_nIsFactory;
        private System.Windows.Forms.Label lbl_Factory;
        private System.Windows.Forms.ComboBox cmb_nIsInner;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grp_Condition;
        private System.Windows.Forms.BindingSource bds_Data;
        private System.Windows.Forms.DataGridView grd_Data;
        public System.Windows.Forms.TextBox txt_cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cCSId;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cCSNameJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cCSNameQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nType;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cType;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cTel;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nIsInner;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cIsInner;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_bUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cUsed;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_nIsFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cIsFactory;
    }
}
