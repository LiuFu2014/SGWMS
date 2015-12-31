namespace SunEast.App
{
    using SunEast;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DBCommInfo;

    public class FrmStockMExceptionSetFilter : Form
    {
        private Button button1;
        private IContainer components = null;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        public TextBox textBox_cBatchNo;
        public TextBox textBox_cBNo;
        public TextBox textBox_cChildId;
        public TextBox textBox_cParentId;
        public TextBox textBox_dOperateTime;
        public TextBox textBox_fQty;
        public TextBox textBox_nItem;
        public TextBox textBox_nWorkId;
        public TextBox textBox1;

        public FrmStockMExceptionSetFilter()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = this.textBox_cBNo.Text.ToString().Trim();
                string str2 = this.textBox_nItem.Text.ToString().Trim();
                string str3 = this.textBox_nWorkId.Text.ToString().Trim();
                string str4 = this.textBox_fQty.Text.ToString().Trim();
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    SqlText = "sp_Pack_UpdtExceptQty :pWorkId,:pBNo,:pItem,:pQty",
                    SqlType = SqlCommandType.sctProcedure,
                    PageIndex = 0,
                    PageSize = 0,
                    FromSysType = "dotnet"
                };
                ZqmParamter paramter = null;
                paramter = new ZqmParamter {
                    ParameterName = "pWorkId",
                    ParameterValue = str3,
                    DataType = ZqmDataType.String,
                    ParameterDir = ZqmParameterDirction.Input
                };
                cmdInfo.Parameters.Add(paramter);
                paramter = new ZqmParamter {
                    ParameterName = "pBNo",
                    ParameterValue = str,
                    DataType = ZqmDataType.String,
                    ParameterDir = ZqmParameterDirction.Input
                };
                cmdInfo.Parameters.Add(paramter);
                paramter = new ZqmParamter {
                    ParameterName = "pItem",
                    ParameterValue = str2,
                    DataType = ZqmDataType.String,
                    ParameterDir = ZqmParameterDirction.Input
                };
                cmdInfo.Parameters.Add(paramter);
                paramter = new ZqmParamter {
                    ParameterName = "pQty",
                    ParameterValue = str4,
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
                MessageBox.Show(table.Rows[0]["cResult"].ToString());
                dataSet.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FrmStockMExceptionSetFilter_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.textBox_cChildId = new TextBox();
            this.textBox_fQty = new TextBox();
            this.textBox_cParentId = new TextBox();
            this.textBox_dOperateTime = new TextBox();
            this.textBox_cBNo = new TextBox();
            this.textBox_nItem = new TextBox();
            this.textBox_cBatchNo = new TextBox();
            this.textBox_nWorkId = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.label9 = new Label();
            this.label10 = new Label();
            base.SuspendLayout();
            this.textBox_cChildId.Location = new Point(0x4a, 40);
            this.textBox_cChildId.Name = "textBox_cChildId";
            this.textBox_cChildId.Size = new Size(100, 0x15);
            this.textBox_cChildId.TabIndex = 0;
            this.textBox_fQty.Location = new Point(0x4a, 0x3e);
            this.textBox_fQty.Name = "textBox_fQty";
            this.textBox_fQty.Size = new Size(100, 0x15);
            this.textBox_fQty.TabIndex = 0;
            this.textBox_cParentId.Location = new Point(0x4a, 0x5e);
            this.textBox_cParentId.Name = "textBox_cParentId";
            this.textBox_cParentId.Size = new Size(100, 0x15);
            this.textBox_cParentId.TabIndex = 0;
            this.textBox_dOperateTime.Location = new Point(0x4a, 0x79);
            this.textBox_dOperateTime.Name = "textBox_dOperateTime";
            this.textBox_dOperateTime.Size = new Size(100, 0x15);
            this.textBox_dOperateTime.TabIndex = 0;
            this.textBox_cBNo.Location = new Point(0x4a, 0x94);
            this.textBox_cBNo.Name = "textBox_cBNo";
            this.textBox_cBNo.Size = new Size(100, 0x15);
            this.textBox_cBNo.TabIndex = 0;
            this.textBox_nItem.Location = new Point(0x4a, 0xaf);
            this.textBox_nItem.Name = "textBox_nItem";
            this.textBox_nItem.Size = new Size(100, 0x15);
            this.textBox_nItem.TabIndex = 0;
            this.textBox_cBatchNo.Location = new Point(0x4a, 0xca);
            this.textBox_cBatchNo.Name = "textBox_cBatchNo";
            this.textBox_cBatchNo.Size = new Size(100, 0x15);
            this.textBox_cBatchNo.TabIndex = 0;
            this.textBox_cBatchNo.TextChanged += new EventHandler(this.textBox_cBatchNo_TextChanged);
            this.textBox_nWorkId.Location = new Point(0x4a, 0xe5);
            this.textBox_nWorkId.Name = "textBox_nWorkId";
            this.textBox_nWorkId.Size = new Size(100, 0x15);
            this.textBox_nWorkId.TabIndex = 0;
            this.textBox_nWorkId.TextChanged += new EventHandler(this.textBox_cBatchNo_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x2c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "物料编码";
            this.label1.Click += new EventHandler(this.label1_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 0x42);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数    量";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(15, 0x62);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "上级编码";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(15, 0x7d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "异动日期";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(15, 0x98);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "单据号码";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(15, 0xb3);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "单据项次";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(15, 0xce);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "批次代号";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(15, 0xe9);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "任务号码";
            this.textBox1.Location = new Point(180, 0x3e);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 0x15);
            this.textBox1.TabIndex = 2;
            this.button1.Location = new Point(180, 0x5d);
            this.button1.Name = "button1";
            this.button1.Size = new Size(100, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "确认修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(180, 0x2c);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "修改数量";
            this.label10.AutoSize = true;
            this.label10.ForeColor = Color.Red;
            this.label10.Location = new Point(0x2e, 9);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0xdd, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "此界面涉及重要信息更改，请谨慎操作！";
            this.label10.Click += new EventHandler(this.label1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x127, 0x109);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox_nWorkId);
            base.Controls.Add(this.textBox_cBatchNo);
            base.Controls.Add(this.textBox_nItem);
            base.Controls.Add(this.textBox_cBNo);
            base.Controls.Add(this.textBox_dOperateTime);
            base.Controls.Add(this.textBox_cParentId);
            base.Controls.Add(this.textBox_fQty);
            base.Controls.Add(this.textBox_cChildId);
            base.MinimizeBox = false;
            base.Name = "FrmStockMExceptionSetFilter";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "异常处理界面";
            base.Load += new EventHandler(this.FrmStockMExceptionSetFilter_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox_cBatchNo_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

