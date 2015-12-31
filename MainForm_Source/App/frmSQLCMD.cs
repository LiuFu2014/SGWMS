using SunEast.App;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UI;
using SunEast;
namespace App
{

    public class frmSQLCMD : FrmSTable
    {
        private Button btnClose;
        private Button btnDoCmd;
        private IContainer components = null;
        private Label label1;
        private TextBox txtSQL;

        public frmSQLCMD()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnDoCmd_Click(object sender, EventArgs e)
        {
            if (this.txtSQL.Text.Trim() == "")
            {
                MessageBox.Show("SQL语句不能为空！");
                this.txtSQL.Focus();
            }
            else
            {
                string sErr = "";
                DataSet dataBySql = PubDBCommFuns.GetDataBySql(this.txtSQL.Text.Trim(), out sErr);
                if (dataBySql != null)
                {
                    dataBySql.Clear();
                }
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show("执行成功！");
                }
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

        private void frmSQLCMD_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.txtSQL = new TextBox();
            this.label1 = new Label();
            this.btnDoCmd = new Button();
            this.btnClose = new Button();
            base.SuspendLayout();
            this.txtSQL.Location = new Point(12, 0x26);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new Size(0x2db, 0x130);
            this.txtSQL.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ＳＱＬ";
            this.btnDoCmd.Location = new Point(0x102, 0x17d);
            this.btnDoCmd.Name = "btnDoCmd";
            this.btnDoCmd.Size = new Size(0x4b, 0x17);
            this.btnDoCmd.TabIndex = 2;
            this.btnDoCmd.Text = "执行语句";
            this.btnDoCmd.UseVisualStyleBackColor = true;
            this.btnDoCmd.Click += new EventHandler(this.btnDoCmd_Click);
            this.btnClose.Location = new Point(0x19d, 0x17d);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1e9);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnDoCmd);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtSQL);
            base.Name = "frmSQLCMD";
            this.Text = "SQL命令窗体";
            base.Load += new EventHandler(this.frmSQLCMD_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

