namespace WareBaseMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;

    public class frmBatchUpdatePosInfo : FrmSTable
    {
        private bool _bIsOK = false;
        private string _SqlText = "";
        private string _WHId = "";
        private Button btn_Cancel;
        private Button btn_OK;
        private CheckBox chk_Area;
        private CheckBox chk_MgrArea;
        private CheckBox chk_PltSpec;
        private ComboBox cmb_Area;
        private ComboBox cmb_MgrArea;
        private ComboBox cmb_PltSpec;
        private IContainer components = null;

        public frmBatchUpdatePosInfo()
        {
            this.InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this._bIsOK = false;
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string str = "";
            string str2 = "";
            if ((this.chk_Area.Checked && (this.cmb_Area.Text.Trim() != "")) && (this.cmb_Area.SelectedValue != null))
            {
                str = " set cAreaId='" + this.cmb_Area.SelectedValue.ToString().Trim() + "'";
                str2 = " 货位值";
            }
            if ((this.chk_MgrArea.Checked && (this.cmb_MgrArea.Text.Trim() != "")) && (this.cmb_MgrArea.SelectedValue != null))
            {
                if (str.Trim() != "")
                {
                    str = str + " set cMAreaId='" + this.cmb_MgrArea.SelectedValue.ToString().Trim() + "'";
                }
                else
                {
                    str = " set cMAreaId='" + this.cmb_MgrArea.SelectedValue.ToString().Trim() + "'";
                }
                str2 = str2 + " 管理货区值";
            }
            if ((this.chk_PltSpec.Checked && (this.cmb_PltSpec.Text.Trim() != "")) && (this.cmb_PltSpec.SelectedValue != null))
            {
                if (str.Trim() != "")
                {
                    str = str + " set cPalletSpec='" + this.cmb_PltSpec.Text.Trim() + "'";
                }
                else
                {
                    str = " set cPalletSpec='" + this.cmb_PltSpec.Text.Trim() + "'";
                }
                str2 = str2 + " 托盘规格值";
            }
            if (str.Trim() == "")
            {
                MessageBox.Show("没有选择修改项！");
            }
            else if (MessageBox.Show("您确定需要批量修改" + str2 + " 吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.No)
            {
                this._SqlText = " update TWC_WareCell " + str + " where cPosId='{0}'";
                this._bIsOK = true;
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

        private void frmBatchUpdatePosInfo_Load(object sender, EventArgs e)
        {
            if ((base.UserInformation.UserName == "Admin5118") && (base.UserInformation.UserId == "90101001"))
            {
                this.chk_MgrArea.Enabled = true;
                this.chk_PltSpec.Enabled = true;
                this.cmb_MgrArea.Enabled = true;
                this.cmb_PltSpec.Enabled = true;
            }
            this.LoadCombList();
        }

        private void InitializeComponent()
        {
            this.chk_PltSpec = new CheckBox();
            this.cmb_PltSpec = new ComboBox();
            this.cmb_Area = new ComboBox();
            this.chk_Area = new CheckBox();
            this.cmb_MgrArea = new ComboBox();
            this.chk_MgrArea = new CheckBox();
            this.btn_OK = new Button();
            this.btn_Cancel = new Button();
            base.SuspendLayout();
            this.chk_PltSpec.AutoSize = true;
            this.chk_PltSpec.Enabled = false;
            this.chk_PltSpec.Location = new Point(0x26, 0x15);
            this.chk_PltSpec.Name = "chk_PltSpec";
            this.chk_PltSpec.Size = new Size(0x54, 0x10);
            this.chk_PltSpec.TabIndex = 0;
            this.chk_PltSpec.Text = "托盘规格：";
            this.chk_PltSpec.UseVisualStyleBackColor = true;
            this.cmb_PltSpec.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_PltSpec.Enabled = false;
            this.cmb_PltSpec.FormattingEnabled = true;
            this.cmb_PltSpec.Location = new Point(0x7a, 0x15);
            this.cmb_PltSpec.Name = "cmb_PltSpec";
            this.cmb_PltSpec.Size = new Size(0xe4, 20);
            this.cmb_PltSpec.TabIndex = 1;
            this.cmb_Area.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Area.FormattingEnabled = true;
            this.cmb_Area.Location = new Point(0x7a, 0x3b);
            this.cmb_Area.Name = "cmb_Area";
            this.cmb_Area.Size = new Size(0xe4, 20);
            this.cmb_Area.TabIndex = 3;
            this.chk_Area.AutoSize = true;
            this.chk_Area.Location = new Point(0x26, 0x3b);
            this.chk_Area.Name = "chk_Area";
            this.chk_Area.Size = new Size(60, 0x10);
            this.chk_Area.TabIndex = 2;
            this.chk_Area.Text = "货区：";
            this.chk_Area.UseVisualStyleBackColor = true;
            this.cmb_MgrArea.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_MgrArea.Enabled = false;
            this.cmb_MgrArea.FormattingEnabled = true;
            this.cmb_MgrArea.Location = new Point(0x7a, 0x5e);
            this.cmb_MgrArea.Name = "cmb_MgrArea";
            this.cmb_MgrArea.Size = new Size(0xe4, 20);
            this.cmb_MgrArea.TabIndex = 7;
            this.chk_MgrArea.AutoSize = true;
            this.chk_MgrArea.Enabled = false;
            this.chk_MgrArea.Location = new Point(0x26, 0x5e);
            this.chk_MgrArea.Name = "chk_MgrArea";
            this.chk_MgrArea.Size = new Size(0x54, 0x10);
            this.chk_MgrArea.TabIndex = 6;
            this.chk_MgrArea.Text = "管理货区：";
            this.chk_MgrArea.UseVisualStyleBackColor = true;
            this.btn_OK.Location = new Point(0x65, 0x8f);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 8;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Cancel.Location = new Point(0xe3, 0x8f);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x187, 0xbf);
            base.Controls.Add(this.btn_Cancel);
            base.Controls.Add(this.btn_OK);
            base.Controls.Add(this.cmb_MgrArea);
            base.Controls.Add(this.chk_MgrArea);
            base.Controls.Add(this.cmb_Area);
            base.Controls.Add(this.chk_Area);
            base.Controls.Add(this.cmb_PltSpec);
            base.Controls.Add(this.chk_PltSpec);
            base.Name = "frmBatchUpdatePosInfo";
            this.Text = "批量修改货位信息";
            base.Load += new EventHandler(this.frmBatchUpdatePosInfo_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadCombList()
        {
            Exception exception;
            string sErr = "";
            DataSet set = null;
            StringBuilder builder = new StringBuilder("");
            builder.Append("select cAreaId,cAreaName from TWC_WArea where bUsed=1");
            if (this._WHId.Trim() != "")
            {
                builder.Append(" and cWHId='" + this._WHId.Trim() + "'");
            }
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TWC_WArea", 0, 0, "", out sErr);
                if (set != null)
                {
                    this.cmb_Area.DisplayMember = "cAreaName";
                    this.cmb_Area.ValueMember = "cAreaId";
                    this.cmb_Area.DataSource = set.Tables["TWC_WArea"].Copy();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception1)
            {
                exception = exception1;
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
            }
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            builder.Remove(0, builder.Length);
            builder.Append("select * from TWC_PalletSpec");
            if (set == null)
            {
                set.Clear();
            }
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TWC_PalletSpec", 0, 0, "", out sErr);
                this.cmb_PltSpec.DisplayMember = "cPalletSpec";
                this.cmb_PltSpec.ValueMember = "cPalletSpecId";
                this.cmb_PltSpec.DataSource = set.Tables["TWC_PalletSpec"].Copy();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
            }
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            builder.Remove(0, builder.Length);
            builder.Append("select * from TWC_MgrArea where bUsed=1");
            if (set == null)
            {
                set.Clear();
            }
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TWC_MgrArea", 0, 0, "", out sErr);
                this.cmb_MgrArea.DisplayMember = "cMAName";
                this.cmb_MgrArea.ValueMember = "cMAreaId";
                this.cmb_MgrArea.DataSource = set.Tables["TWC_MgrArea"].Copy();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception3)
            {
                exception = exception3;
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
            }
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
        }

        public bool bIsOK
        {
            get
            {
                return this._bIsOK;
            }
            set
            {
                this._bIsOK = value;
            }
        }

        public string SqlText
        {
            get
            {
                return this._SqlText.Trim();
            }
            set
            {
                this._SqlText = value;
            }
        }

        public string WHId
        {
            get
            {
                return this._WHId.Trim();
            }
            set
            {
                this._WHId = value;
            }
        }
    }
}

