namespace WareStoreMS
{
    partial class frmSelDoBadProdMode
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_DoMode = new System.Windows.Forms.ComboBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "����ʽ��";
            // 
            // cmb_DoMode
            // 
            this.cmb_DoMode.FormattingEnabled = true;
            this.cmb_DoMode.Items.AddRange(new object[] {
            "����",
            "�˻�",
            "����",
            "�벻��Ʒ��"});
            this.cmb_DoMode.Location = new System.Drawing.Point(84, 28);
            this.cmb_DoMode.Name = "cmb_DoMode";
            this.cmb_DoMode.Size = new System.Drawing.Size(208, 20);
            this.cmb_DoMode.TabIndex = 1;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(64, 73);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "ȷ��(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(178, 73);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "ȡ��(&C)";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmSelDoBadProdMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(317, 108);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.cmb_DoMode);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "frmSelDoBadProdMode";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ѡ����ʽ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_DoMode;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Close;
    }
}
