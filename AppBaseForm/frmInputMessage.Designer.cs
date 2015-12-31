namespace UI
{
    partial class frmInputMessage
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lbl_Text = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lbl_Text);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(359, 49);
            this.pnlTop.TabIndex = 0;
            // 
            // lbl_Text
            // 
            this.lbl_Text.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Text.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_Text.Location = new System.Drawing.Point(3, 5);
            this.lbl_Text.Name = "lbl_Text";
            this.lbl_Text.Size = new System.Drawing.Size(352, 40);
            this.lbl_Text.TabIndex = 0;
            this.lbl_Text.Text = "请录入：";
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.txt_Value);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(0, 49);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(359, 26);
            this.pnlInput.TabIndex = 1;
            // 
            // txt_Value
            // 
            this.txt_Value.Location = new System.Drawing.Point(27, 3);
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Size = new System.Drawing.Size(327, 21);
            this.txt_Value.TabIndex = 0;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.button1);
            this.pnlButton.Controls.Add(this.btn_OK);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButton.Location = new System.Drawing.Point(0, 75);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(359, 42);
            this.pnlButton.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "关闭(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(93, 11);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // frmInputMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 121);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmInputMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInputMessage_KeyDown);
            this.Load += new System.EventHandler(this.frmInputMessage_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lbl_Text;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_OK;
    }
}