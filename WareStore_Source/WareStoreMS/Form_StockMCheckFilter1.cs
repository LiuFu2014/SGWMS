namespace WareStoreMS
{
    using SunEast;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Zqm.DBCommInfo;

    public class Form_StockMCheckFilter1 : Form
    {
        public Button button1;
        public Button button2;
        private IContainer components = null;
        public Label label1;
        public Label label11;
        public Label label12;
        public Label label13;
        public Label label14;
        public Label label2;
        public Label label5;
        public Label label6;
        public Label label7;
        public Label label8;
        public Label label9;
        public TextBox textBox_cBatchNo;
        public TextBox textBox_cBoxId;
        public TextBox textBox_cCheckNo;
        public TextBox textBox_cMNo;
        public TextBox textBox_cPalletId;
        public TextBox textBox_cUnit;
        public TextBox textBox_cUser;
        public TextBox textBox_cWHId;
        public TextBox textBox_fQty;
        public TextBox textBox_fRQty;
        public TextBox textBox_nQCStatus;

        public Form_StockMCheckFilter1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_WriteChkDtl :cUser,:cCheckNo,:cWHId,:cPalletId,:cBoxId,:cMNo,:cBatchNo,:nQCStatus,:fQty,:fRQty,:cUnit",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "cUser",
                ParameterValue = this.textBox_cUser.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cCheckNo",
                ParameterValue = this.textBox_cCheckNo.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cWHId",
                ParameterValue = this.textBox_cWHId.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cPalletId",
                ParameterValue = this.textBox_cPalletId.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cBoxId",
                ParameterValue = this.textBox_cBoxId.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cMNo",
                ParameterValue = this.textBox_cMNo.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cBatchNo",
                ParameterValue = this.textBox_cBatchNo.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "nQCStatus",
                ParameterValue = this.textBox_nQCStatus.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "fQty",
                ParameterValue = this.textBox_fQty.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "fRQty",
                ParameterValue = this.textBox_fRQty.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "cUnit",
                ParameterValue = this.textBox_cUnit.Text.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet dataSet = null;
            DataTable table = null;
            dataSet = client.GetDataSet(cmdInfo, out sErr);
            if (dataSet != null)
            {
                table = dataSet.Tables["data"];
            }
            if (table.Rows[0]["cResult"].ToString() == "0")
            {
                MessageBox.Show("实盘登记成功");
            }
            dataSet.Clear();
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form_StockMCheckFilter1_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.textBox_cUser = new TextBox();
            this.textBox_cBatchNo = new TextBox();
            this.textBox_cBoxId = new TextBox();
            this.textBox_fQty = new TextBox();
            this.textBox_cMNo = new TextBox();
            this.textBox_nQCStatus = new TextBox();
            this.textBox_cPalletId = new TextBox();
            this.textBox_cWHId = new TextBox();
            this.textBox_cCheckNo = new TextBox();
            this.textBox_fRQty = new TextBox();
            this.textBox_cUnit = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.label11 = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.label14 = new Label();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.textBox_cUser.Location = new Point(0x47, 0x11);
            this.textBox_cUser.Name = "textBox_cUser";
            this.textBox_cUser.ReadOnly = true;
            this.textBox_cUser.Size = new Size(100, 0x15);
            this.textBox_cUser.TabIndex = 6;
            this.textBox_cUser.Tag = "0";
            this.textBox_cBatchNo.Location = new Point(0x100, 0x11);
            this.textBox_cBatchNo.Name = "textBox_cBatchNo";
            this.textBox_cBatchNo.ReadOnly = true;
            this.textBox_cBatchNo.Size = new Size(100, 0x15);
            this.textBox_cBatchNo.TabIndex = 7;
            this.textBox_cBatchNo.Tag = "0";
            this.textBox_cBoxId.Location = new Point(0x47, 0x7d);
            this.textBox_cBoxId.Name = "textBox_cBoxId";
            this.textBox_cBoxId.ReadOnly = true;
            this.textBox_cBoxId.Size = new Size(100, 0x15);
            this.textBox_cBoxId.TabIndex = 8;
            this.textBox_cBoxId.Tag = "0";
            this.textBox_fQty.Location = new Point(0x100, 0x47);
            this.textBox_fQty.Name = "textBox_fQty";
            this.textBox_fQty.ReadOnly = true;
            this.textBox_fQty.Size = new Size(100, 0x15);
            this.textBox_fQty.TabIndex = 9;
            this.textBox_fQty.Tag = "0";
            this.textBox_cMNo.Location = new Point(0x100, 0x62);
            this.textBox_cMNo.Name = "textBox_cMNo";
            this.textBox_cMNo.ReadOnly = true;
            this.textBox_cMNo.Size = new Size(100, 0x15);
            this.textBox_cMNo.TabIndex = 10;
            this.textBox_cMNo.Tag = "0";
            this.textBox_nQCStatus.Location = new Point(0x100, 0x2c);
            this.textBox_nQCStatus.Name = "textBox_nQCStatus";
            this.textBox_nQCStatus.ReadOnly = true;
            this.textBox_nQCStatus.Size = new Size(100, 0x15);
            this.textBox_nQCStatus.TabIndex = 11;
            this.textBox_nQCStatus.Tag = "0";
            this.textBox_cPalletId.Location = new Point(0x47, 0x62);
            this.textBox_cPalletId.Name = "textBox_cPalletId";
            this.textBox_cPalletId.ReadOnly = true;
            this.textBox_cPalletId.Size = new Size(100, 0x15);
            this.textBox_cPalletId.TabIndex = 12;
            this.textBox_cPalletId.Tag = "0";
            this.textBox_cWHId.Location = new Point(0x47, 0x47);
            this.textBox_cWHId.Name = "textBox_cWHId";
            this.textBox_cWHId.ReadOnly = true;
            this.textBox_cWHId.Size = new Size(100, 0x15);
            this.textBox_cWHId.TabIndex = 13;
            this.textBox_cWHId.Tag = "0";
            this.textBox_cCheckNo.Location = new Point(0x47, 0x2c);
            this.textBox_cCheckNo.Name = "textBox_cCheckNo";
            this.textBox_cCheckNo.ReadOnly = true;
            this.textBox_cCheckNo.Size = new Size(100, 0x15);
            this.textBox_cCheckNo.TabIndex = 14;
            this.textBox_cCheckNo.Tag = "0";
            this.textBox_fRQty.BackColor = SystemColors.ActiveBorder;
            this.textBox_fRQty.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox_fRQty.ForeColor = Color.Red;
            this.textBox_fRQty.Location = new Point(0x47, 0x98);
            this.textBox_fRQty.Name = "textBox_fRQty";
            this.textBox_fRQty.Size = new Size(100, 0x15);
            this.textBox_fRQty.TabIndex = 15;
            this.textBox_fRQty.Tag = "0";
            this.textBox_cUnit.Location = new Point(0x100, 0x7d);
            this.textBox_cUnit.Name = "textBox_cUnit";
            this.textBox_cUnit.ReadOnly = true;
            this.textBox_cUnit.Size = new Size(100, 0x15);
            this.textBox_cUnit.TabIndex = 0x10;
            this.textBox_cUnit.Tag = "0";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x11;
            this.label1.Text = "用户代码";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x9c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x12;
            this.label2.Text = "实盘数量";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0x66);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x15;
            this.label5.Text = "托盘号码";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 0x81);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x16;
            this.label6.Text = "周转箱号";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(12, 0x4b);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x17;
            this.label7.Text = "仓库号码";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 0x30);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x18;
            this.label8.Text = "盘点单号";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xb8, 0x15);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 0x11;
            this.label9.Text = "批号代码";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xb8, 0x66);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x15;
            this.label11.Text = "物料号码";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xb8, 0x81);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x35, 12);
            this.label12.TabIndex = 0x16;
            this.label12.Text = "物料单位";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0xb8, 0x4b);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x17;
            this.label13.Text = "账面数量";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0xc4, 0x30);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x29, 12);
            this.label14.TabIndex = 0x18;
            this.label14.Text = "QC状态";
            this.button1.Location = new Point(0x52, 0xcd);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 0x19;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(0xd4, 0xcd);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 0x1a;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x170, 0xfc);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox_cUnit);
            base.Controls.Add(this.textBox_fRQty);
            base.Controls.Add(this.textBox_cCheckNo);
            base.Controls.Add(this.textBox_cWHId);
            base.Controls.Add(this.textBox_cPalletId);
            base.Controls.Add(this.textBox_nQCStatus);
            base.Controls.Add(this.textBox_cMNo);
            base.Controls.Add(this.textBox_fQty);
            base.Controls.Add(this.textBox_cBoxId);
            base.Controls.Add(this.textBox_cBatchNo);
            base.Controls.Add(this.textBox_cUser);
            base.MinimizeBox = false;
            base.Name = "Form_StockMCheckFilter1";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "实盘登记";
            base.Load += new EventHandler(this.Form_StockMCheckFilter1_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

