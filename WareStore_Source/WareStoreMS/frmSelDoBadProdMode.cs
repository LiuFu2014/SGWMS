namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmSelDoBadProdMode : FrmSTable
    {
        private bool _IsResultOK = false;
        private string _SelDoMode = "";
        private Button btn_Close;
        private Button btn_OK;
        private ComboBox cmb_DoMode;
        private IContainer components = null;
        private Label label1;

        public frmSelDoBadProdMode()
        {
            this.InitializeComponent();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this._IsResultOK = false;
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.cmb_DoMode.Text.Trim() == "")
            {
                MessageBox.Show("对不起，处理方式不能为空！");
                this.cmb_DoMode.Focus();
            }
            else
            {
                this._SelDoMode = this.cmb_DoMode.Text.Trim();
                this._IsResultOK = true;
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cmb_DoMode = new ComboBox();
            this.btn_OK = new Button();
            this.btn_Close = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x19, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "处理方式：";
            this.cmb_DoMode.FormattingEnabled = true;
            this.cmb_DoMode.Items.AddRange(new object[] { "报损", "退货", "销毁", "入不良品仓" });
            this.cmb_DoMode.Location = new Point(0x54, 0x1c);
            this.cmb_DoMode.Name = "cmb_DoMode";
            this.cmb_DoMode.Size = new Size(0xd0, 20);
            this.cmb_DoMode.TabIndex = 1;
            this.btn_OK.Location = new Point(0x40, 0x49);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Close.Location = new Point(0xb2, 0x49);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new Size(0x4b, 0x17);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "取消(&C)";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new EventHandler(this.btn_Close_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x13d, 0x6c);
            base.Controls.Add(this.btn_Close);
            base.Controls.Add(this.btn_OK);
            base.Controls.Add(this.cmb_DoMode);
            base.Controls.Add(this.label1);
            base.MinimizeBox = false;
            base.Name = "frmSelDoBadProdMode";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "选择处理方式";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadDoBadMatMode()
        {
            string sErr = "";
            string sSql = "select * from TWC_BaseItem where bUsed=1 and cItemType=''";
            sSql = sSql + " order by nSort,nId";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, sSql, "TWC_BaseItem", "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if ((set != null) && (set.Tables["TWC_BaseItem"] != null))
            {
                DataTable table = set.Tables["TWC_BaseItem"];
                this.cmb_DoMode.Items.Clear();
                this.cmb_DoMode.DataSource = table;
                this.cmb_DoMode.DisplayMember = "cItemName";
                this.cmb_DoMode.ValueMember = "cItemNo";
            }
        }

        public bool IsResultOK
        {
            get
            {
                return this._IsResultOK;
            }
            set
            {
                this._IsResultOK = value;
            }
        }

        public string SelDoMode
        {
            get
            {
                return this._SelDoMode.Trim();
            }
            set
            {
                this._SelDoMode = value.Trim();
            }
        }
    }
}

