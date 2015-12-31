namespace WareStoreMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmSelItemList : FrmSTable
    {
        private string _FldDesc = "";
        private string _FldKey = "";
        private bool _IsSelected = false;
        private string _SelectItemList = "";
        private string _SelectKeyList = "";
        private DataTable _TableItem = null;
        private string _TitleText = "多项选择";
        private string[] ArrKeyList = null;
        private Button btn_Close;
        private Button btn_OK;
        private CheckBox chkAll;
        private CheckedListBox chkList;
        private IContainer components = null;
        private Label label1;
        private Panel panel1;

        public frmSelItemList()
        {
            this.InitializeComponent();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this._IsSelected = false;
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.GetSelectResult();
            if (this._SelectItemList.Trim() == "")
            {
                if (MessageBox.Show("没有选择数据，需要退出吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }
            else if (MessageBox.Show("您确定你选择的是：" + this._SelectItemList + " 吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            this._IsSelected = true;
            base.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.SetItemCheckAll(this.chkAll.Checked);
        }

        private void chkList_Click(object sender, EventArgs e)
        {
            if (this.chkList.Items.Count > 0)
            {
                int selectedIndex = this.chkList.SelectedIndex;
                bool flag = !this.chkList.GetItemChecked(selectedIndex);
                this.chkList.SetItemChecked(selectedIndex, flag);
                this.chkList.Update();
            }
        }

        private void chkList_DoubleClick(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelItemList_Load(object sender, EventArgs e)
        {
            this.LoadItemList();
        }

        private void GetSelectResult()
        {
            this._SelectItemList = "";
            this._SelectKeyList = "";
            bool flag = true;
            if (this.chkList.Items.Count != 0)
            {
                for (int i = 0; i < this.chkList.Items.Count; i++)
                {
                    if (this.chkList.GetItemChecked(i))
                    {
                        if (flag)
                        {
                            this._SelectItemList = "" + this.chkList.Items[i].ToString().Trim() + "";
                            this._SelectKeyList = "" + this.ArrKeyList[i].Trim() + "";
                            flag = false;
                        }
                        else
                        {
                            this._SelectItemList = this._SelectItemList + "," + this.chkList.Items[i].ToString().Trim() + "";
                            this._SelectKeyList = this._SelectKeyList + "," + this.ArrKeyList[i].Trim() + "";
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.chkList = new CheckedListBox();
            this.btn_OK = new Button();
            this.btn_Close = new Button();
            this.chkAll = new CheckBox();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(400, 0x23);
            this.panel1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 11f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "请勾选";
            this.chkList.ColumnWidth = 2;
            this.chkList.Dock = DockStyle.Top;
            this.chkList.FormattingEnabled = true;
            this.chkList.HorizontalScrollbar = true;
            this.chkList.Location = new Point(0, 0x23);
            this.chkList.Name = "chkList";
            this.chkList.ScrollAlwaysVisible = true;
            this.chkList.Size = new Size(400, 0xc4);
            this.chkList.TabIndex = 2;
            this.chkList.DoubleClick += new EventHandler(this.chkList_DoubleClick);
            this.chkList.Click += new EventHandler(this.chkList_Click);
            this.btn_OK.Location = new Point(0x62, 0xed);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Close.Location = new Point(200, 0xed);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new Size(0x4b, 0x17);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new EventHandler(this.btn_Close_Click);
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new Point(8, 240);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new Size(0x30, 0x10);
            this.chkAll.TabIndex = 5;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new EventHandler(this.chkAll_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(400, 0x108);
            base.Controls.Add(this.chkAll);
            base.Controls.Add(this.btn_Close);
            base.Controls.Add(this.btn_OK);
            base.Controls.Add(this.chkList);
            base.Controls.Add(this.panel1);
            base.MinimizeBox = false;
            base.Name = "frmSelItemList";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "多选项目";
            base.Load += new EventHandler(this.frmSelItemList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadItemList()
        {
            this.chkList.Items.Clear();
            if ((this._TableItem != null) && (this._TableItem.Rows.Count != 0))
            {
                Cursor.Current = Cursors.WaitCursor;
                this.chkList.BeginUpdate();
                this.ArrKeyList = new string[this._TableItem.Rows.Count];
                for (int i = 0; i < this._TableItem.Rows.Count; i++)
                {
                    DataRow row = this._TableItem.Rows[i];
                    this.chkList.Items.Add(row[this._FldDesc].ToString(), false);
                    this.ArrKeyList[i] = row[this._FldKey].ToString();
                }
                this.chkList.EndUpdate();
                Cursor.Current = Cursors.Default;
            }
        }

        private void SetItemCheckAll(bool bIsChecked)
        {
            if (this.chkList.Items.Count != 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.chkList.BeginUpdate();
                for (int i = 0; i < this.chkList.Items.Count; i++)
                {
                    this.chkList.SetItemChecked(i, bIsChecked);
                }
                this.chkList.EndUpdate();
                Cursor.Current = Cursors.Default;
            }
        }

        public string FldDesc
        {
            set
            {
                this._FldDesc = value.Trim();
            }
        }

        public string FldKey
        {
            set
            {
                this._FldKey = value.Trim();
            }
        }

        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
        }

        public string SelectItemList
        {
            get
            {
                return this._SelectItemList.Trim();
            }
        }

        public string SelectKeyList
        {
            get
            {
                return this._SelectKeyList.Trim();
            }
        }

        public DataTable TableItem
        {
            set
            {
                this._TableItem = value;
            }
        }

        public string TitleText
        {
            get
            {
                return this._TitleText.Trim();
            }
            set
            {
                this._TitleText = value.Trim();
                this.Text = this._TitleText;
            }
        }
    }
}

