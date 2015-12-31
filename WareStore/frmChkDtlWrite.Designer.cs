namespace WareStoreMS
{
    partial class frmChkDtlWrite
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
            this.txt_Qty = new System.Windows.Forms.TextBox();
            this.txt_RQty = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_fDiff = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_fBad = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_RTotal = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_AddMat = new System.Windows.Forms.Button();
            this.btn_SelBNoIn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_AreaIdErp = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_WHIdErp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_cBatchNo = new System.Windows.Forms.TextBox();
            this.txt_cSpec = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_nItemIn = new System.Windows.Forms.TextBox();
            this.txt_cBNoIn = new System.Windows.Forms.TextBox();
            this.txt_cPosId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cUnit = new System.Windows.Forms.TextBox();
            this.txt_cWHId = new System.Windows.Forms.TextBox();
            this.txt_nPalletId = new System.Windows.Forms.TextBox();
            this.txt_cMNo = new System.Windows.Forms.TextBox();
            this.txt_cBoxId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_PosIdErp = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Qty
            // 
            this.txt_Qty.Enabled = false;
            this.txt_Qty.Location = new System.Drawing.Point(164, 172);
            this.txt_Qty.Name = "txt_Qty";
            this.txt_Qty.ReadOnly = true;
            this.txt_Qty.Size = new System.Drawing.Size(279, 21);
            this.txt_Qty.TabIndex = 2;
            // 
            // txt_RQty
            // 
            this.txt_RQty.Enabled = false;
            this.txt_RQty.Location = new System.Drawing.Point(375, 231);
            this.txt_RQty.Name = "txt_RQty";
            this.txt_RQty.ReadOnly = true;
            this.txt_RQty.Size = new System.Drawing.Size(68, 21);
            this.txt_RQty.TabIndex = 2;
            this.txt_RQty.Text = "0";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(175, 302);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(293, 302);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "帐面数";
            this.toolTip1.SetToolTip(this.label2, "当前的库存数");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(280, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "实际有效库存数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 41;
            this.label4.Text = "损溢数";
            // 
            // txt_fDiff
            // 
            this.txt_fDiff.Location = new System.Drawing.Point(164, 231);
            this.txt_fDiff.Name = "txt_fDiff";
            this.txt_fDiff.ReadOnly = true;
            this.txt_fDiff.Size = new System.Drawing.Size(100, 21);
            this.txt_fDiff.TabIndex = 1;
            this.txt_fDiff.Text = "0";
            this.toolTip1.SetToolTip(this.txt_fDiff, "实盘数-账面数");
            this.txt_fDiff.TextChanged += new System.EventHandler(this.txt_fDiff_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(8, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 47;
            this.label9.Text = "入库单号";
            this.toolTip1.SetToolTip(this.label9, "点击，对新增物料选择入库时的单号及明细序号");
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(337, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "货位号码";
            this.toolTip1.SetToolTip(this.label1, "点击，可对新添物料选择货位号");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(8, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "物料号码";
            this.toolTip1.SetToolTip(this.label11, "点击，可增加现库存中不存在物料");
            // 
            // txt_fBad
            // 
            this.txt_fBad.Location = new System.Drawing.Point(343, 203);
            this.txt_fBad.Name = "txt_fBad";
            this.txt_fBad.Size = new System.Drawing.Size(100, 21);
            this.txt_fBad.TabIndex = 1;
            this.txt_fBad.Text = "0";
            this.toolTip1.SetToolTip(this.txt_fBad, "实盘数-账面数");
            this.txt_fBad.TextChanged += new System.EventHandler(this.txt_RTotal_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label15.Location = new System.Drawing.Point(92, 257);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "1.实盘数 包括不良品数";
            this.toolTip1.SetToolTip(this.label15, "当前的库存数");
            // 
            // txt_RTotal
            // 
            this.txt_RTotal.Location = new System.Drawing.Point(164, 203);
            this.txt_RTotal.Name = "txt_RTotal";
            this.txt_RTotal.Size = new System.Drawing.Size(100, 21);
            this.txt_RTotal.TabIndex = 0;
            this.txt_RTotal.Text = "0";
            this.toolTip1.SetToolTip(this.txt_RTotal, "实盘数-账面数");
            this.txt_RTotal.TextChanged += new System.EventHandler(this.txt_RTotal_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label17.Location = new System.Drawing.Point(92, 271);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(341, 12);
            this.label17.TabIndex = 47;
            this.label17.Text = "2.损溢数＝实盘数-帐面数-不良品数(不良品数包含在损溢数里)";
            this.toolTip1.SetToolTip(this.label17, "当前的库存数");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label18.Location = new System.Drawing.Point(92, 285);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(263, 12);
            this.label18.TabIndex = 48;
            this.label18.Text = "3.实际有效数＝实盘数-不良品数=帐面数+损溢数";
            this.toolTip1.SetToolTip(this.label18, "当前的库存数");
            // 
            // btn_AddMat
            // 
            this.btn_AddMat.Location = new System.Drawing.Point(196, 58);
            this.btn_AddMat.Name = "btn_AddMat";
            this.btn_AddMat.Size = new System.Drawing.Size(27, 23);
            this.btn_AddMat.TabIndex = 53;
            this.btn_AddMat.Text = "…";
            this.toolTip1.SetToolTip(this.btn_AddMat, "点击，选择新增加的物料");
            this.btn_AddMat.UseVisualStyleBackColor = true;
            this.btn_AddMat.Visible = false;
            this.btn_AddMat.Click += new System.EventHandler(this.btn_AddMat_Click);
            // 
            // btn_SelBNoIn
            // 
            this.btn_SelBNoIn.Location = new System.Drawing.Point(196, 83);
            this.btn_SelBNoIn.Name = "btn_SelBNoIn";
            this.btn_SelBNoIn.Size = new System.Drawing.Size(27, 23);
            this.btn_SelBNoIn.TabIndex = 54;
            this.btn_SelBNoIn.Text = "…";
            this.toolTip1.SetToolTip(this.btn_SelBNoIn, "点击，选择库存入库单号");
            this.btn_SelBNoIn.UseVisualStyleBackColor = true;
            this.btn_SelBNoIn.Visible = false;
            this.btn_SelBNoIn.Click += new System.EventHandler(this.btn_SelBNoIn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txt_PosIdErp);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txt_AreaIdErp);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txt_WHIdErp);
            this.groupBox1.Controls.Add(this.btn_SelBNoIn);
            this.groupBox1.Controls.Add(this.btn_AddMat);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_cBatchNo);
            this.groupBox1.Controls.Add(this.txt_cSpec);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_nItemIn);
            this.groupBox1.Controls.Add(this.txt_cBNoIn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_cPosId);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_cUnit);
            this.groupBox1.Controls.Add(this.txt_cWHId);
            this.groupBox1.Controls.Add(this.txt_nPalletId);
            this.groupBox1.Controls.Add(this.txt_cMNo);
            this.groupBox1.Controls.Add(this.txt_cBoxId);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 166);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(158, 138);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 12);
            this.label20.TabIndex = 58;
            this.label20.Text = "ERP货区";
            // 
            // txt_AreaIdErp
            // 
            this.txt_AreaIdErp.Location = new System.Drawing.Point(208, 137);
            this.txt_AreaIdErp.Name = "txt_AreaIdErp";
            this.txt_AreaIdErp.ReadOnly = true;
            this.txt_AreaIdErp.Size = new System.Drawing.Size(122, 21);
            this.txt_AreaIdErp.TabIndex = 57;
            this.txt_AreaIdErp.Tag = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 141);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 56;
            this.label19.Text = "ERP仓库号";
            // 
            // txt_WHIdErp
            // 
            this.txt_WHIdErp.Location = new System.Drawing.Point(73, 138);
            this.txt_WHIdErp.Name = "txt_WHIdErp";
            this.txt_WHIdErp.ReadOnly = true;
            this.txt_WHIdErp.Size = new System.Drawing.Size(79, 21);
            this.txt_WHIdErp.TabIndex = 55;
            this.txt_WHIdErp.Tag = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(337, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 52;
            this.label10.Text = "物料规格";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 115);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 51;
            this.label13.Text = "物料批号";
            // 
            // txt_cBatchNo
            // 
            this.txt_cBatchNo.Enabled = false;
            this.txt_cBatchNo.Location = new System.Drawing.Point(73, 110);
            this.txt_cBatchNo.Name = "txt_cBatchNo";
            this.txt_cBatchNo.ReadOnly = true;
            this.txt_cBatchNo.Size = new System.Drawing.Size(122, 21);
            this.txt_cBatchNo.TabIndex = 50;
            this.txt_cBatchNo.Tag = "0";
            // 
            // txt_cSpec
            // 
            this.txt_cSpec.Location = new System.Drawing.Point(395, 110);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.ReadOnly = true;
            this.txt_cSpec.Size = new System.Drawing.Size(137, 21);
            this.txt_cSpec.TabIndex = 49;
            this.txt_cSpec.Tag = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(337, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 48;
            this.label8.Text = "明细项次";
            // 
            // txt_nItemIn
            // 
            this.txt_nItemIn.Enabled = false;
            this.txt_nItemIn.Location = new System.Drawing.Point(395, 85);
            this.txt_nItemIn.Name = "txt_nItemIn";
            this.txt_nItemIn.ReadOnly = true;
            this.txt_nItemIn.Size = new System.Drawing.Size(137, 21);
            this.txt_nItemIn.TabIndex = 46;
            this.txt_nItemIn.Tag = "0";
            // 
            // txt_cBNoIn
            // 
            this.txt_cBNoIn.Location = new System.Drawing.Point(73, 85);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.ReadOnly = true;
            this.txt_cBNoIn.Size = new System.Drawing.Size(122, 21);
            this.txt_cBNoIn.TabIndex = 45;
            this.txt_cBNoIn.Tag = "0";
            // 
            // txt_cPosId
            // 
            this.txt_cPosId.Location = new System.Drawing.Point(395, 11);
            this.txt_cPosId.Name = "txt_cPosId";
            this.txt_cPosId.ReadOnly = true;
            this.txt_cPosId.Size = new System.Drawing.Size(137, 21);
            this.txt_cPosId.TabIndex = 43;
            this.txt_cPosId.Tag = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 42;
            this.label7.Text = "仓库号码";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 39;
            this.label12.Text = "物料单位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(337, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "周转箱号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "托盘号码";
            // 
            // txt_cUnit
            // 
            this.txt_cUnit.Enabled = false;
            this.txt_cUnit.Location = new System.Drawing.Point(395, 59);
            this.txt_cUnit.Name = "txt_cUnit";
            this.txt_cUnit.ReadOnly = true;
            this.txt_cUnit.Size = new System.Drawing.Size(137, 21);
            this.txt_cUnit.TabIndex = 34;
            this.txt_cUnit.Tag = "0";
            // 
            // txt_cWHId
            // 
            this.txt_cWHId.Enabled = false;
            this.txt_cWHId.Location = new System.Drawing.Point(73, 11);
            this.txt_cWHId.Name = "txt_cWHId";
            this.txt_cWHId.ReadOnly = true;
            this.txt_cWHId.Size = new System.Drawing.Size(122, 21);
            this.txt_cWHId.TabIndex = 32;
            this.txt_cWHId.Tag = "0";
            // 
            // txt_nPalletId
            // 
            this.txt_nPalletId.Enabled = false;
            this.txt_nPalletId.Location = new System.Drawing.Point(73, 35);
            this.txt_nPalletId.Name = "txt_nPalletId";
            this.txt_nPalletId.ReadOnly = true;
            this.txt_nPalletId.Size = new System.Drawing.Size(122, 21);
            this.txt_nPalletId.TabIndex = 31;
            this.txt_nPalletId.Tag = "0";
            // 
            // txt_cMNo
            // 
            this.txt_cMNo.Location = new System.Drawing.Point(73, 59);
            this.txt_cMNo.Name = "txt_cMNo";
            this.txt_cMNo.ReadOnly = true;
            this.txt_cMNo.Size = new System.Drawing.Size(122, 21);
            this.txt_cMNo.TabIndex = 29;
            this.txt_cMNo.Tag = "0";
            // 
            // txt_cBoxId
            // 
            this.txt_cBoxId.Location = new System.Drawing.Point(395, 35);
            this.txt_cBoxId.Name = "txt_cBoxId";
            this.txt_cBoxId.ReadOnly = true;
            this.txt_cBoxId.Size = new System.Drawing.Size(137, 21);
            this.txt_cBoxId.TabIndex = 27;
            this.txt_cBoxId.Tag = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(280, 207);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 43;
            this.label14.Text = "不良品数";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(101, 207);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 46;
            this.label16.Text = "实盘数";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(343, 138);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 12);
            this.label21.TabIndex = 60;
            this.label21.Text = "ERP货位";
            // 
            // txt_PosIdErp
            // 
            this.txt_PosIdErp.Location = new System.Drawing.Point(396, 135);
            this.txt_PosIdErp.Name = "txt_PosIdErp";
            this.txt_PosIdErp.ReadOnly = true;
            this.txt_PosIdErp.Size = new System.Drawing.Size(136, 21);
            this.txt_PosIdErp.TabIndex = 59;
            this.txt_PosIdErp.Tag = "0";
            // 
            // frmChkDtlWrite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(548, 330);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txt_RTotal);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_Qty);
            this.Controls.Add(this.txt_RQty);
            this.Controls.Add(this.txt_fBad);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_fDiff);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmChkDtlWrite";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "盘点登记";
            this.Load += new System.EventHandler(this.frmChkDtlWrite_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Qty;
        private System.Windows.Forms.TextBox txt_RQty;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_fDiff;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_cBatchNo;
        private System.Windows.Forms.TextBox txt_cSpec;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_nItemIn;
        private System.Windows.Forms.TextBox txt_cBNoIn;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_cPosId;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cUnit;
        private System.Windows.Forms.TextBox txt_cWHId;
        private System.Windows.Forms.TextBox txt_nPalletId;
        private System.Windows.Forms.TextBox txt_cMNo;
        private System.Windows.Forms.TextBox txt_cBoxId;
        public System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_fBad;
        public System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_RTotal;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btn_AddMat;
        private System.Windows.Forms.Button btn_SelBNoIn;
        public System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_AreaIdErp;
        public System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_WHIdErp;
        public System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_PosIdErp;
    }
}
