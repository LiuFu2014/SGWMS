namespace MainForm
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSetSckClientApp : Form
    {
        private IContainer components = null;

        public frmSetSckClientApp()
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

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x300, 420);
            base.Name = "frmSetSckClientApp";
            this.Text = "设置应用程序参数";
            base.ResumeLayout(false);
        }
    }
}

