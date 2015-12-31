namespace WareBaseMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;
    using SunEast.App;

    public class frmNewPltId : FrmSTable
    {
        private Button btn_OK;
        private Button btnClose;
        private ComboBox cmb_Area;
        private ComboBox cmb_PltSpec;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txt_Qty;

        public frmNewPltId()
        {
            this.InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string sErr = "";
            if (!FrmSTable.IsInteger(this.txt_Qty.Text.Trim()))
            {
                MessageBox.Show("");
                this.txt_Qty.SelectAll();
                this.txt_Qty.FindForm();
            }
            else
            {
                int pQty = int.Parse(this.txt_Qty.Text.Trim());
                string pAreaId = "";
                if ((this.cmb_Area.Text.Trim() != "") && (this.cmb_Area.SelectedValue != null))
                {
                    pAreaId = this.cmb_Area.SelectedValue.ToString().Trim();
                }
                string pPltSpec = "";
                if ((this.cmb_PltSpec.Text.Trim() != "") && (this.cmb_PltSpec.SelectedValue != null))
                {
                    pPltSpec = this.cmb_PltSpec.SelectedValue.ToString().Trim();
                }
                string str4 = PubDBCommFuns.sp_CreatePaleltNo(base.AppInformation.SvrSocket, pQty, 6, "", pAreaId, pPltSpec, out sErr);
                MessageBox.Show(sErr);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
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

        private void frmNewPltId_Load(object sender, EventArgs e)
        {
            this.txt_Qty.Text = "1";
            this.LoadAreaList();
        }

        private void InitializeComponent()
        {
            this.btn_OK = new Button();
            this.label1 = new Label();
            this.txt_Qty = new TextBox();
            this.cmb_Area = new ComboBox();
            this.btnClose = new Button();
            this.label2 = new Label();
            this.cmb_PltSpec = new ComboBox();
            this.label3 = new Label();
            base.SuspendLayout();
            this.btn_OK.Location = new Point(0x27, 0x81);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数量：";
            this.txt_Qty.Location = new Point(0x42, 0x12);
            this.txt_Qty.Name = "txt_Qty";
            this.txt_Qty.Size = new Size(0x59, 0x15);
            this.txt_Qty.TabIndex = 2;
            this.cmb_Area.FormattingEnabled = true;
            this.cmb_Area.Location = new Point(0x42, 90);
            this.cmb_Area.Name = "cmb_Area";
            this.cmb_Area.Size = new Size(0x8a, 20);
            this.cmb_Area.TabIndex = 3;
            this.btnClose.Location = new Point(0x95, 0x81);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x5d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "货区：";
            this.cmb_PltSpec.FormattingEnabled = true;
            this.cmb_PltSpec.Location = new Point(0x42, 0x37);
            this.cmb_PltSpec.Name = "cmb_PltSpec";
            this.cmb_PltSpec.Size = new Size(0x8a, 20);
            this.cmb_PltSpec.TabIndex = 6;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x3a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "托盘规格：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x107, 0xa8);
            base.Controls.Add(this.cmb_PltSpec);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cmb_Area);
            base.Controls.Add(this.txt_Qty);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btn_OK);
            base.Name = "frmNewPltId";
            this.Text = "产生托盘号";
            base.Load += new EventHandler(this.frmNewPltId_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadAreaList()
        {
            string sErr = "";
            string sSql = "select * from TWC_WArea ";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "data", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if ((set != null) && (set.Tables["data"] != null))
            {
                DataTable table = set.Tables["data"];
                this.cmb_Area.DataSource = table;
                this.cmb_Area.DisplayMember = "cAreaName";
                this.cmb_Area.ValueMember = "cAreaId";
            }
            sSql = "select cPalletSpecId, cPalletSpec from TWC_PALLETSPEC";
            set = null;
            set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "data", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if ((set != null) && (set.Tables["data"] != null))
            {
                DataTable table2 = set.Tables["data"];
                this.cmb_PltSpec.DataSource = table2;
                this.cmb_PltSpec.DisplayMember = "cPalletSpec";
                this.cmb_PltSpec.ValueMember = "cPalletSpecId";
            }
        }
    }
}

