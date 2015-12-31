namespace WareBaseMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmBtchUpdateEditor : FrmSTable
    {
        private DoBatchUpdateData _doBatchUpdateData = null;
        private DataTable _TbFields = null;
        private BindingSource bds_Item;
        private Button btn_Cancel;
        private Button btn_OK;
        private ComboBox cmb_Item;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private TextBox txt_ItemValue;

        public frmBtchUpdateEditor()
        {
            this.InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.bds_Item.Count == 0)
            {
                MessageBox.Show("无字段字典表数据！");
            }
            else if (!(this.cmb_Item.Text.Trim() != "") || (this.cmb_Item.SelectedIndex <= -1))
            {
                MessageBox.Show("请选修改项目！");
            }
            else
            {
                bool flag = true;
                DataRowView current = null;
                current = (DataRowView) this.bds_Item.Current;
                if (current != null)
                {
                    string str = current["cColName"].ToString();
                    string text = this.txt_ItemValue.Text;
                    string str3 = current["cDataType"].ToString();
                    string str4 = str3.Trim();
                    if ((str4 != null) && (str4 != "string"))
                    {
                        if (!(str4 == "int"))
                        {
                            if (str4 == "double")
                            {
                                flag = FrmSTable.IsNumberic(text.Trim());
                            }
                            else if (str4 == "date")
                            {
                                flag = FrmSTable.IsDateTime(text.Trim());
                            }
                            else if (!(str4 == "bool"))
                            {
                            }
                        }
                        else
                        {
                            flag = FrmSTable.IsInteger(text.Trim());
                        }
                    }
                    if (!flag)
                    {
                        MessageBox.Show("录入项目值为非法数据，请录入正确的数据！");
                        this.txt_ItemValue.SelectAll();
                        this.txt_ItemValue.Focus();
                    }
                    else if (this._doBatchUpdateData != null)
                    {
                        this._doBatchUpdateData(str.Trim(), text, str3.Trim());
                    }
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

        private void frmBtchUpdateEditor_Load(object sender, EventArgs e)
        {
            if (this._TbFields == null)
            {
                this.btn_OK.Enabled = false;
            }
            else
            {
                this.bds_Item.DataSource = this._TbFields;
                this.cmb_Item.DisplayMember = "cColDesc";
                this.cmb_Item.ValueMember = "cColName";
                this.cmb_Item.DataSource = this.bds_Item;
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.cmb_Item = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txt_ItemValue = new TextBox();
            this.btn_OK = new Button();
            this.btn_Cancel = new Button();
            this.bds_Item = new BindingSource(this.components);
            ((ISupportInitialize) this.bds_Item).BeginInit();
            base.SuspendLayout();
            this.cmb_Item.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Item.FormattingEnabled = true;
            this.cmb_Item.Location = new Point(0x47, 0x1b);
            this.cmb_Item.Name = "cmb_Item";
            this.cmb_Item.Size = new Size(0xec, 20);
            this.cmb_Item.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "修改项目";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x47);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "项目值";
            this.txt_ItemValue.Location = new Point(0x47, 0x44);
            this.txt_ItemValue.Name = "txt_ItemValue";
            this.txt_ItemValue.Size = new Size(0x199, 0x15);
            this.txt_ItemValue.TabIndex = 3;
            this.btn_OK.Location = new Point(0x90, 0x6d);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Cancel.Location = new Point(0x11c, 0x6d);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ec, 0x90);
            base.Controls.Add(this.btn_Cancel);
            base.Controls.Add(this.btn_OK);
            base.Controls.Add(this.txt_ItemValue);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cmb_Item);
            base.Name = "frmBtchUpdateEditor";
            this.Text = "批量修改项目";
            base.Load += new EventHandler(this.frmBtchUpdateEditor_Load);
            ((ISupportInitialize) this.bds_Item).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [Description("处理更新数据事件")]
        public DoBatchUpdateData doBatchUpdateData
        {
            get
            {
                return this._doBatchUpdateData;
            }
            set
            {
                this._doBatchUpdateData = value;
            }
        }

        [Description("字段表 ：cColName,cColDesc,cDataType")]
        public DataTable TbFields
        {
            get
            {
                return this._TbFields;
            }
            set
            {
                this._TbFields = value;
            }
        }
    }
}

