namespace WareStoreMS
{
    partial class FrmStockMCheckFilter
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
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnDepart = new System.Windows.Forms.RadioButton();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmd_ErpNo = new System.Windows.Forms.ComboBox();
            this.lbl_Erp = new System.Windows.Forms.Label();
            this.cmb_WHId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txt_Mat = new System.Windows.Forms.TextBox();
            this.txt_ItemType = new System.Windows.Forms.TextBox();
            this.grpDepart = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Pos = new System.Windows.Forms.Button();
            this.btn_Mat = new System.Windows.Forms.Button();
            this.btn_SelItemType = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.chk_Date = new System.Windows.Forms.CheckBox();
            this.txt_Pos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_PosClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpDepart.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(108, 285);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(246, 285);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.rbtnDepart);
            this.groupBox1.Controls.Add(this.rbtnAll);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.cmd_ErpNo);
            this.groupBox1.Controls.Add(this.lbl_Erp);
            this.groupBox1.Controls.Add(this.cmb_WHId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 74);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // rbtnDepart
            // 
            this.rbtnDepart.AutoSize = true;
            this.rbtnDepart.Location = new System.Drawing.Point(151, 47);
            this.rbtnDepart.Name = "rbtnDepart";
            this.rbtnDepart.Size = new System.Drawing.Size(47, 16);
            this.rbtnDepart.TabIndex = 57;
            this.rbtnDepart.Tag = "302";
            this.rbtnDepart.Text = "抽盘";
            this.rbtnDepart.UseVisualStyleBackColor = true;
            this.rbtnDepart.CheckedChanged += new System.EventHandler(this.rbtnDepart_CheckedChanged);
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(56, 47);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(47, 16);
            this.rbtnAll.TabIndex = 56;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Tag = "301";
            this.rbtnAll.Text = "全盘";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(281, 44);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 23);
            this.btnRefresh.TabIndex = 55;
            this.btnRefresh.Tag = "1";
            this.btnRefresh.Text = "刷新";
            this.toolTip1.SetToolTip(this.btnRefresh, "刷新ERP单号数据");
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // cmd_ErpNo
            // 
            this.cmd_ErpNo.FormattingEnabled = true;
            this.cmd_ErpNo.Location = new System.Drawing.Point(281, 15);
            this.cmd_ErpNo.Name = "cmd_ErpNo";
            this.cmd_ErpNo.Size = new System.Drawing.Size(130, 20);
            this.cmd_ErpNo.TabIndex = 32;
            this.cmd_ErpNo.Tag = "102";
            // 
            // lbl_Erp
            // 
            this.lbl_Erp.AutoSize = true;
            this.lbl_Erp.Location = new System.Drawing.Point(235, 19);
            this.lbl_Erp.Name = "lbl_Erp";
            this.lbl_Erp.Size = new System.Drawing.Size(47, 12);
            this.lbl_Erp.TabIndex = 31;
            this.lbl_Erp.Text = "ERP单号";
            // 
            // cmb_WHId
            // 
            this.cmb_WHId.FormattingEnabled = true;
            this.cmb_WHId.Location = new System.Drawing.Point(56, 15);
            this.cmb_WHId.Name = "cmb_WHId";
            this.cmb_WHId.Size = new System.Drawing.Size(148, 20);
            this.cmb_WHId.TabIndex = 30;
            this.cmb_WHId.Tag = "102";
            this.cmb_WHId.Text = "Bind SelectedIndex";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "仓库";
            // 
            // txt_Mat
            // 
            this.txt_Mat.Location = new System.Drawing.Point(95, 69);
            this.txt_Mat.Multiline = true;
            this.txt_Mat.Name = "txt_Mat";
            this.txt_Mat.ReadOnly = true;
            this.txt_Mat.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Mat.Size = new System.Drawing.Size(280, 45);
            this.txt_Mat.TabIndex = 49;
            this.toolTip1.SetToolTip(this.txt_Mat, "请单击右边按钮，进行选择或重置");
            // 
            // txt_ItemType
            // 
            this.txt_ItemType.Location = new System.Drawing.Point(95, 18);
            this.txt_ItemType.Multiline = true;
            this.txt_ItemType.Name = "txt_ItemType";
            this.txt_ItemType.ReadOnly = true;
            this.txt_ItemType.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_ItemType.Size = new System.Drawing.Size(280, 45);
            this.txt_ItemType.TabIndex = 48;
            this.toolTip1.SetToolTip(this.txt_ItemType, "请单击右边按钮，进行选择或重置");
            // 
            // grpDepart
            // 
            this.grpDepart.Controls.Add(this.btn_PosClear);
            this.grpDepart.Controls.Add(this.label2);
            this.grpDepart.Controls.Add(this.btn_Pos);
            this.grpDepart.Controls.Add(this.btn_Mat);
            this.grpDepart.Controls.Add(this.btn_SelItemType);
            this.grpDepart.Controls.Add(this.dtpTo);
            this.grpDepart.Controls.Add(this.dtpFrom);
            this.grpDepart.Controls.Add(this.chk_Date);
            this.grpDepart.Controls.Add(this.txt_Pos);
            this.grpDepart.Controls.Add(this.txt_Mat);
            this.grpDepart.Controls.Add(this.txt_ItemType);
            this.grpDepart.Controls.Add(this.label5);
            this.grpDepart.Controls.Add(this.label4);
            this.grpDepart.Controls.Add(this.label3);
            this.grpDepart.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDepart.Enabled = false;
            this.grpDepart.Location = new System.Drawing.Point(0, 74);
            this.grpDepart.Name = "grpDepart";
            this.grpDepart.Size = new System.Drawing.Size(429, 208);
            this.grpDepart.TabIndex = 30;
            this.grpDepart.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(219, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 1);
            this.label2.TabIndex = 57;
            this.label2.Text = "label2";
            // 
            // btn_Pos
            // 
            this.btn_Pos.Location = new System.Drawing.Point(381, 119);
            this.btn_Pos.Name = "btn_Pos";
            this.btn_Pos.Size = new System.Drawing.Size(42, 23);
            this.btn_Pos.TabIndex = 56;
            this.btn_Pos.Tag = "1";
            this.btn_Pos.Text = "…";
            this.btn_Pos.UseVisualStyleBackColor = true;
            this.btn_Pos.Click += new System.EventHandler(this.btn_Pos_Click);
            // 
            // btn_Mat
            // 
            this.btn_Mat.Location = new System.Drawing.Point(381, 69);
            this.btn_Mat.Name = "btn_Mat";
            this.btn_Mat.Size = new System.Drawing.Size(42, 23);
            this.btn_Mat.TabIndex = 55;
            this.btn_Mat.Tag = "1";
            this.btn_Mat.Text = "…";
            this.btn_Mat.UseVisualStyleBackColor = true;
            this.btn_Mat.Click += new System.EventHandler(this.btn_Mat_Click);
            // 
            // btn_SelItemType
            // 
            this.btn_SelItemType.Location = new System.Drawing.Point(381, 18);
            this.btn_SelItemType.Name = "btn_SelItemType";
            this.btn_SelItemType.Size = new System.Drawing.Size(42, 23);
            this.btn_SelItemType.TabIndex = 54;
            this.btn_SelItemType.Tag = "1";
            this.btn_SelItemType.Text = "…";
            this.btn_SelItemType.UseVisualStyleBackColor = true;
            this.btn_SelItemType.Click += new System.EventHandler(this.btn_SelItemType_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Location = new System.Drawing.Point(260, 176);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(115, 21);
            this.dtpTo.TabIndex = 53;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Location = new System.Drawing.Point(95, 176);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(115, 21);
            this.dtpFrom.TabIndex = 52;
            // 
            // chk_Date
            // 
            this.chk_Date.AutoSize = true;
            this.chk_Date.Location = new System.Drawing.Point(20, 178);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.Size = new System.Drawing.Size(72, 16);
            this.chk_Date.TabIndex = 51;
            this.chk_Date.Text = "异动日期";
            this.chk_Date.UseVisualStyleBackColor = true;
            this.chk_Date.CheckedChanged += new System.EventHandler(this.chk_Date_CheckedChanged);
            // 
            // txt_Pos
            // 
            this.txt_Pos.Location = new System.Drawing.Point(95, 119);
            this.txt_Pos.Multiline = true;
            this.txt_Pos.Name = "txt_Pos";
            this.txt_Pos.ReadOnly = true;
            this.txt_Pos.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Pos.Size = new System.Drawing.Size(280, 45);
            this.txt_Pos.TabIndex = 50;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "物       料";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "货       位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 45;
            this.label3.Text = "物 料 类 型";
            // 
            // btn_PosClear
            // 
            this.btn_PosClear.Location = new System.Drawing.Point(381, 141);
            this.btn_PosClear.Name = "btn_PosClear";
            this.btn_PosClear.Size = new System.Drawing.Size(42, 23);
            this.btn_PosClear.TabIndex = 58;
            this.btn_PosClear.Tag = "1";
            this.btn_PosClear.Text = "清空";
            this.btn_PosClear.UseVisualStyleBackColor = true;
            this.btn_PosClear.Click += new System.EventHandler(this.btn_PosClear_Click);
            // 
            // FrmStockMCheckFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(429, 315);
            this.Controls.Add(this.grpDepart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOK);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimizeBox = false;
            this.Name = "FrmStockMCheckFilter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "盘点条件";
            this.Load += new System.EventHandler(this.FrmStockMCheckFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDepart.ResumeLayout(false);
            this.grpDepart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_WHId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbl_Erp;
        private System.Windows.Forms.ComboBox cmd_ErpNo;
        private System.Windows.Forms.GroupBox grpDepart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Pos;
        private System.Windows.Forms.Button btn_Mat;
        private System.Windows.Forms.Button btn_SelItemType;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.CheckBox chk_Date;
        private System.Windows.Forms.TextBox txt_Pos;
        private System.Windows.Forms.TextBox txt_Mat;
        private System.Windows.Forms.TextBox txt_ItemType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbtnDepart;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.Button btn_PosClear;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               