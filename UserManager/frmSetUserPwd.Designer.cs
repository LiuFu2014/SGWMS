namespace UserMS
{
    partial class frmSetUserPwd
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_OperatorPwd = new System.Windows.Forms.TextBox();
            this.txt_OperatorName = new System.Windows.Forms.TextBox();
            this.txt_OperatorId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Pwd2 = new System.Windows.Forms.TextBox();
            this.txt_Pwd1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_OperatorPwd);
            this.groupBox1.Controls.Add(this.txt_OperatorName);
            this.groupBox1.Controls.Add(this.txt_OperatorId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作员身份验证";
            // 
            // txt_OperatorPwd
            // 
            this.txt_OperatorPwd.Location = new System.Drawing.Point(86, 65);
            this.txt_OperatorPwd.Name = "txt_OperatorPwd";
            this.txt_OperatorPwd.PasswordChar = '*';
            this.txt_OperatorPwd.Size = new System.Drawing.Size(155, 21);
            this.txt_OperatorPwd.TabIndex = 5;
            // 
            // txt_OperatorName
            // 
            this.txt_OperatorName.Location = new System.Drawing.Point(86, 42);
            this.txt_OperatorName.Name = "txt_OperatorName";
            this.txt_OperatorName.ReadOnly = true;
            this.txt_OperatorName.Size = new System.Drawing.Size(155, 21);
            this.txt_OperatorName.TabIndex = 4;
            // 
            // txt_OperatorId
            // 
            this.txt_OperatorId.Location = new System.Drawing.Point(86, 18);
            this.txt_OperatorId.Name = "txt_OperatorId";
            this.txt_OperatorId.ReadOnly = true;
            this.txt_OperatorId.Size = new System.Drawing.Size(155, 21);
            this.txt_OperatorId.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "验证密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名　：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户编码：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_Pwd2);
            this.groupBox2.Controls.Add(this.txt_Pwd1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(5, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 69);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置密码";
            // 
            // txt_Pwd2
            // 
            this.txt_Pwd2.Location = new System.Drawing.Point(89, 41);
            this.txt_Pwd2.Name = "txt_Pwd2";
            this.txt_Pwd2.PasswordChar = '*';
            this.txt_Pwd2.Size = new System.Drawing.Size(155, 21);
            this.txt_Pwd2.TabIndex = 7;
            // 
            // txt_Pwd1
            // 
            this.txt_Pwd1.Location = new System.Drawing.Point(89, 14);
            this.txt_Pwd1.Name = "txt_Pwd1";
            this.txt_Pwd1.PasswordChar = '*';
            this.txt_Pwd1.Size = new System.Drawing.Size(155, 21);
            this.txt_Pwd1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "新密码确认：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "新密码　　：";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(58, 172);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "确认(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(154, 172);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // frmSetUserPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 202);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "frmSetUserPwd";
            this.Text = "设置密码";
            this.Load += new System.EventHandler(this.frmSetUserPwd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_OperatorName;
        private System.Windows.Forms.TextBox txt_OperatorId;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_OperatorPwd;
        private System.Windows.Forms.TextBox txt_Pwd2;
        private System.Windows.Forms.TextBox txt_Pwd1;
    }
}