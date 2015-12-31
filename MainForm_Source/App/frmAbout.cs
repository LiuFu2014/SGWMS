namespace App
{
    using MainForm.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmAbout : Form
    {
        private IContainer components = null;
        private Label label1;
        private Label lbl_OSVersion;
        private PictureBox pictureBox1;
        private TextBox txt_Desc;

        public frmAbout()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.lbl_OSVersion.Text = Environment.OSVersion.VersionString;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmAbout));
            this.label1 = new Label();
            this.lbl_OSVersion = new Label();
            this.txt_Desc = new TextBox();
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(2, 0xce);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前操作系统内部版本号：";
            this.lbl_OSVersion.AutoSize = true;
            this.lbl_OSVersion.Location = new Point(0x1a, 0xe4);
            this.lbl_OSVersion.Name = "lbl_OSVersion";
            this.lbl_OSVersion.Size = new Size(0x29, 12);
            this.lbl_OSVersion.TabIndex = 1;
            this.lbl_OSVersion.Text = "label2";
            this.txt_Desc.Location = new Point(4, 0x27);
            this.txt_Desc.Multiline = true;
            this.txt_Desc.Name = "txt_Desc";
            this.txt_Desc.ReadOnly = true;
            this.txt_Desc.Size = new Size(0x133, 0x7d);
            this.txt_Desc.TabIndex = 3;
            this.pictureBox1.Image = Resources.自动化仓储管理;
            this.pictureBox1.Location = new Point(3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x137, 0x1c);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x13c, 0x10c);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.txt_Desc);
            base.Controls.Add(this.lbl_OSVersion);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "frmAbout";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "关于";
            base.Load += new EventHandler(this.frmAbout_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

