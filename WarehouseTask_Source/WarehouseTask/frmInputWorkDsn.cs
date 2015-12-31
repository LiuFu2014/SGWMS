namespace WarehouseTask
{
    using App;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using CommBase;

    public class frmInputWorkDsn : Form
    {
        private WMSAppInfo _AppInformation = null;
        private bool _IsOK = false;
        private int _ResultValue = 0;
        private Button btn_Cancel;
        private Button btn_OK;
        private ComboBox cmb_Dsn;
        private IContainer components = null;
        private Label label1;

        public frmInputWorkDsn()
        {
            this.InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this._IsOK = false;
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!PubFuns.IsInteger(this.cmb_Dsn.Text.Trim()))
            {
                MessageBox.Show("请录入正确的数值！");
                this.cmb_Dsn.Focus();
                this._IsOK = false;
            }
            else
            {
                this._IsOK = true;
                this._ResultValue = int.Parse(this.cmb_Dsn.Text.Trim());
                base.Close();
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

        private void frmInputWorkDsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void frmInputWorkDsn_Load(object sender, EventArgs e)
        {
            if ((this._AppInformation != null) && (this._AppInformation.AppICON != null))
            {
                base.Icon = this._AppInformation.AppICON;
            }
            this.cmb_Dsn.SelectedIndex = 0;
            this.cmb_Dsn.Focus();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmInputWorkDsn));
            this.btn_OK = new Button();
            this.label1 = new Label();
            this.cmb_Dsn = new ComboBox();
            this.btn_Cancel = new Button();
            base.SuspendLayout();
            this.btn_OK.Location = new Point(0x26, 0x40);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "优先级：";
            this.cmb_Dsn.FormattingEnabled = true;
            this.cmb_Dsn.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            this.cmb_Dsn.Location = new Point(0x44, 0x15);
            this.cmb_Dsn.Name = "cmb_Dsn";
            this.cmb_Dsn.Size = new Size(0xb2, 20);
            this.cmb_Dsn.TabIndex = 2;
            this.btn_Cancel.Location = new Point(0x93, 0x40);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(260, 0x69);
            base.Controls.Add(this.btn_Cancel);
            base.Controls.Add(this.cmb_Dsn);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btn_OK);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "frmInputWorkDsn";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "修改优先权";
            base.Load += new EventHandler(this.frmInputWorkDsn_Load);
            base.KeyDown += new KeyEventHandler(this.frmInputWorkDsn_KeyDown);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [Description("应用程序对象")]
        public WMSAppInfo AppInformation
        {
            get
            {
                return this._AppInformation;
            }
            set
            {
                this._AppInformation = value;
            }
        }

        [Description("是否录入确定")]
        public bool IsOK
        {
            get
            {
                return this._IsOK;
            }
            set
            {
                this._IsOK = value;
            }
        }

        [Description("录入结果值")]
        public int ResultValue
        {
            get
            {
                return this._ResultValue;
            }
            set
            {
                this._ResultValue = value;
            }
        }
    }
}

