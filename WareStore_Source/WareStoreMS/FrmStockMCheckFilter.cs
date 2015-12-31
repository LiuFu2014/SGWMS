namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;
    using WareBaseMS;
    using Zqm.CommBase;

    public class FrmStockMCheckFilter : FrmSTable
    {
        private Button btn_Mat;
        private Button btn_Pos;
        private Button btn_SelItemType;
        private Button btnOK;
        private Button btnRefresh;
        private Button button2;
        private CheckBox chk_Date;
        private ComboBox cmb_WHId;
        private ComboBox cmd_ErpNo;
        private IContainer components = null;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private GroupBox groupBox1;
        private GroupBox grpDepart;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lbl_Erp;
        private RadioButton rbtnAll;
        private RadioButton rbtnDepart;
        private ToolTip toolTip1;
        private TextBox txt_ItemType;
        private TextBox txt_Mat;
        private TextBox txt_Pos;

        public FrmStockMCheckFilter()
        {
            this.InitializeComponent();
        }

        private void btn_Mat_Click(object sender, EventArgs e)
        {
            this.txt_Mat.Text = "";
            this.txt_Mat.Tag = "";
            WarehouseBase.SelectMaterialInfo(base.AppInformation, base.UserInformation, new WareBaseMS.DoSelMaterialEvent(this.doSelectMatInfo));
        }

        private void btn_Pos_Click(object sender, EventArgs e)
        {
            FrmSelectCell cell = new FrmSelectCell {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            cell.cmb_cWHId.Tag = this.cmb_WHId.SelectedValue;
            cell.IsMultiSelect = true;
            cell.ShowDialog();
            if (cell.BIsResult)
            {
                this.txt_Pos.Text = cell.SelResult;
            }
            cell.Dispose();
        }

        private void btn_SelItemType_Click(object sender, EventArgs e)
        {
            DataSet set = null;
            string sErr = "";
            string sSql = "";
            string sFldsDate = "";
            Button button = (Button) sender;
            sSql = "select cTypeId,cTypeName from TPC_MaterialType where ctypemode=0";
            sFldsDate = "";
            Cursor.Current = Cursors.WaitCursor;
            set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, sSql, sFldsDate, out sErr);
            Cursor.Current = Cursors.Default;
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                frmSelItemList list = new frmSelItemList();
                DataTable table = set.Tables["data"];
                list.TableItem = table;
                list.TitleText = "物料类别选择";
                list.FldDesc = "cTypeName";
                list.FldKey = "cTypeId";
                list.ShowDialog();
                if (list.IsSelected)
                {
                    this.txt_ItemType.Text = list.SelectItemList;
                    this.txt_ItemType.Tag = list.SelectKeyList;
                }
                list.Dispose();
                set.Clear();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sErr = "";
            string pWHId = "";
            string pErpCheckId = this.cmd_ErpNo.Text.Trim();
            string pCheckType = "";
            string pCheckTypeDesc = "";
            string pMType = "";
            string pMTypeDesc = "";
            string pMNo = "";
            string pMNoDesc = "";
            string pPos = "";
            string pDateFrom = "";
            string pDateTo = "";
            if ((this.cmb_WHId.Items.Count > 0) && (this.cmb_WHId.SelectedValue != null))
            {
                pWHId = this.cmb_WHId.SelectedValue.ToString();
            }
            if (pWHId == "")
            {
                MessageBox.Show("对不起，仓库部能为空！");
                this.cmb_WHId.SelectAll();
                this.cmb_WHId.Focus();
            }
            if (this.rbtnAll.Checked)
            {
                pCheckType = "301";
                pCheckTypeDesc = "全盘";
            }
            else
            {
                pCheckType = "302";
                pCheckTypeDesc = "抽盘";
            }
            if (this.txt_ItemType.Tag != null)
            {
                pMType = this.txt_ItemType.Tag.ToString();
            }
            pMTypeDesc = this.txt_ItemType.Text.Trim();
            if (this.txt_Mat.Tag != null)
            {
                pMNo = this.txt_Mat.Tag.ToString().Trim();
            }
            pMNoDesc = this.txt_Mat.Text.Trim();
            pPos = this.txt_Pos.Text.Trim();
            if (this.chk_Date.Checked)
            {
                if (this.dtpFrom.Value > this.dtpTo.Value)
                {
                    MessageBox.Show("对不起，开始日期不能大于截止日期！");
                    this.dtpFrom.Focus();
                    return;
                }
                pDateFrom = this.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00");
                pDateTo = this.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59");
            }
            Cursor.Current = Cursors.WaitCursor;
            string str13 = PubDBCommFuns.sp_Chk_DoChkBuilder(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, pCheckType, pCheckTypeDesc, pErpCheckId, pWHId, pMType, pMTypeDesc, pMNo, pMNoDesc, pPos, pDateFrom, pDateTo, out sErr);
            Cursor.Current = Cursors.Default;
            if (str13 != "B")
            {
                MessageBox.Show("生成盘点单数据时出错：" + sErr);
            }
            else
            {
                MessageBox.Show("成功生成盘点单：" + sErr);
                base.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void chk_Date_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpFrom.Enabled = this.chk_Date.Checked;
            this.dtpTo.Enabled = this.chk_Date.Checked;
        }

        private void comboBox_cPosId_DragDrop(object sender, DragEventArgs e)
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

        private void doSelectMatInfo(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther, string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1, string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId, string sSupplier, int _nMatClass, bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                if (this.txt_Mat.Text.Trim().Trim() == "")
                {
                    this.txt_Mat.Text = sMName.Trim();
                    this.txt_Mat.Tag = sMNo;
                }
                else
                {
                    this.txt_Mat.Text = this.txt_Mat.Text + "、" + sMName.Trim();
                    this.txt_Mat.Tag = this.txt_Mat.Tag.ToString() + "," + sMNo;
                }
            }
        }

        private void FrmStockMCheckFilter_Load(object sender, EventArgs e)
        {
            bool isLinkErp = false;
            isLinkErp = this.GetIsLinkErp();
            this.lbl_Erp.Visible = isLinkErp;
            this.cmd_ErpNo.Visible = isLinkErp;
            this.btnRefresh.Visible = isLinkErp;
            this.LoadBaseItemList();
        }

        private bool GetIsLinkErp()
        {
            string sSql = "select isnull(max(cParValue),'0') cParValue from TPS_SysPar where  cParId='nIsLinkMis' ";
            string sErr = "";
            object objValue = null;
            PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "cParValue", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            return ((objValue != null) && (int.Parse(objValue.ToString()) > 0));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1 = new GroupBox();
            this.rbtnDepart = new RadioButton();
            this.rbtnAll = new RadioButton();
            this.btnRefresh = new Button();
            this.cmd_ErpNo = new ComboBox();
            this.lbl_Erp = new Label();
            this.cmb_WHId = new ComboBox();
            this.label1 = new Label();
            this.toolTip1 = new ToolTip(this.components);
            this.txt_Mat = new TextBox();
            this.txt_ItemType = new TextBox();
            this.grpDepart = new GroupBox();
            this.label2 = new Label();
            this.btn_Pos = new Button();
            this.btn_Mat = new Button();
            this.btn_SelItemType = new Button();
            this.dtpTo = new DateTimePicker();
            this.dtpFrom = new DateTimePicker();
            this.chk_Date = new CheckBox();
            this.txt_Pos = new TextBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.groupBox1.SuspendLayout();
            this.grpDepart.SuspendLayout();
            base.SuspendLayout();
            this.btnOK.Location = new Point(0x6c, 0x11d);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.Location = new Point(0xf6, 0x11d);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.groupBox1.BackColor = Color.White;
            this.groupBox1.Controls.Add(this.rbtnDepart);
            this.groupBox1.Controls.Add(this.rbtnAll);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.cmd_ErpNo);
            this.groupBox1.Controls.Add(this.lbl_Erp);
            this.groupBox1.Controls.Add(this.cmb_WHId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1ad, 0x4a);
            this.groupBox1.TabIndex = 0x1d;
            this.groupBox1.TabStop = false;
            this.rbtnDepart.AutoSize = true;
            this.rbtnDepart.Location = new Point(0x97, 0x2f);
            this.rbtnDepart.Name = "rbtnDepart";
            this.rbtnDepart.Size = new Size(0x2f, 0x10);
            this.rbtnDepart.TabIndex = 0x39;
            this.rbtnDepart.Tag = "302";
            this.rbtnDepart.Text = "抽盘";
            this.rbtnDepart.UseVisualStyleBackColor = true;
            this.rbtnDepart.CheckedChanged += new EventHandler(this.rbtnDepart_CheckedChanged);
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new Point(0x38, 0x2f);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new Size(0x2f, 0x10);
            this.rbtnAll.TabIndex = 0x38;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Tag = "301";
            this.rbtnAll.Text = "全盘";
            this.rbtnAll.UseVisualStyleBackColor = true;
            this.btnRefresh.Location = new Point(0x119, 0x2c);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(130, 0x17);
            this.btnRefresh.TabIndex = 0x37;
            this.btnRefresh.Tag = "1";
            this.btnRefresh.Text = "刷新";
            this.toolTip1.SetToolTip(this.btnRefresh, "刷新ERP单号数据");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.cmd_ErpNo.FormattingEnabled = true;
            this.cmd_ErpNo.Location = new Point(0x119, 15);
            this.cmd_ErpNo.Name = "cmd_ErpNo";
            this.cmd_ErpNo.Size = new Size(130, 20);
            this.cmd_ErpNo.TabIndex = 0x20;
            this.cmd_ErpNo.Tag = "102";
            this.lbl_Erp.AutoSize = true;
            this.lbl_Erp.Location = new Point(0xeb, 0x13);
            this.lbl_Erp.Name = "lbl_Erp";
            this.lbl_Erp.Size = new Size(0x2f, 12);
            this.lbl_Erp.TabIndex = 0x1f;
            this.lbl_Erp.Text = "ERP单号";
            this.cmb_WHId.FormattingEnabled = true;
            this.cmb_WHId.Location = new Point(0x38, 15);
            this.cmb_WHId.Name = "cmb_WHId";
            this.cmb_WHId.Size = new Size(0x94, 20);
            this.cmb_WHId.TabIndex = 30;
            this.cmb_WHId.Tag = "102";
            this.cmb_WHId.Text = "Bind SelectedIndex";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x15, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0x1d;
            this.label1.Text = "仓库";
            this.txt_Mat.Location = new Point(0x5f, 0x45);
            this.txt_Mat.Multiline = true;
            this.txt_Mat.Name = "txt_Mat";
            this.txt_Mat.ReadOnly = true;
            this.txt_Mat.ScrollBars = ScrollBars.Horizontal;
            this.txt_Mat.Size = new Size(280, 0x2d);
            this.txt_Mat.TabIndex = 0x31;
            this.toolTip1.SetToolTip(this.txt_Mat, "请单击右边按钮，进行选择或重置");
            this.txt_ItemType.Location = new Point(0x5f, 0x12);
            this.txt_ItemType.Multiline = true;
            this.txt_ItemType.Name = "txt_ItemType";
            this.txt_ItemType.ReadOnly = true;
            this.txt_ItemType.ScrollBars = ScrollBars.Horizontal;
            this.txt_ItemType.Size = new Size(280, 0x2d);
            this.txt_ItemType.TabIndex = 0x30;
            this.toolTip1.SetToolTip(this.txt_ItemType, "请单击右边按钮，进行选择或重置");
            this.grpDepart.Controls.Add(this.label2);
            this.grpDepart.Controls.Add(this.btn_Pos);
            this.grpDepart.Controls.Add(this.btn_Mat);
            this.grpDepart.Controls.Add(this.btn_SelItemType);
            this.grpDepart.Controls.Add(this.dtpTo);
            this.grpDepart.Controls.Add(this.dtpFrom);
            this.grpDepart.Controls.Add(this.chk_Date);
            this.grpDepart.Controls.Add(this.txt_Pos);
            this.grpDepart.Controls.Add(this.txt_Mat);
            this.grpDepart.Controls.Add(this.txt_ItemType);
            this.grpDepart.Controls.Add(this.label5);
            this.grpDepart.Controls.Add(this.label4);
            this.grpDepart.Controls.Add(this.label3);
            this.grpDepart.Dock = DockStyle.Top;
            this.grpDepart.Enabled = false;
            this.grpDepart.Location = new Point(0, 0x4a);
            this.grpDepart.Name = "grpDepart";
            this.grpDepart.Size = new Size(0x1ad, 0xd0);
            this.grpDepart.TabIndex = 30;
            this.grpDepart.TabStop = false;
            this.label2.BackColor = Color.Black;
            this.label2.Location = new Point(0xdb, 0xba);
            this.label2.Name = "label2";
            this.label2.Size = new Size(30, 1);
            this.label2.TabIndex = 0x39;
            this.label2.Text = "label2";
            this.btn_Pos.Location = new Point(0x17d, 0x77);
            this.btn_Pos.Name = "btn_Pos";
            this.btn_Pos.Size = new Size(30, 0x17);
            this.btn_Pos.TabIndex = 0x38;
            this.btn_Pos.Tag = "1";
            this.btn_Pos.Text = "…";
            this.btn_Pos.UseVisualStyleBackColor = true;
            this.btn_Pos.Click += new EventHandler(this.btn_Pos_Click);
            this.btn_Mat.Location = new Point(0x17d, 0x45);
            this.btn_Mat.Name = "btn_Mat";
            this.btn_Mat.Size = new Size(30, 0x17);
            this.btn_Mat.TabIndex = 0x37;
            this.btn_Mat.Tag = "1";
            this.btn_Mat.Text = "…";
            this.btn_Mat.UseVisualStyleBackColor = true;
            this.btn_Mat.Click += new EventHandler(this.btn_Mat_Click);
            this.btn_SelItemType.Location = new Point(0x17d, 0x12);
            this.btn_SelItemType.Name = "btn_SelItemType";
            this.btn_SelItemType.Size = new Size(30, 0x17);
            this.btn_SelItemType.TabIndex = 0x36;
            this.btn_SelItemType.Tag = "1";
            this.btn_SelItemType.Text = "…";
            this.btn_SelItemType.UseVisualStyleBackColor = true;
            this.btn_SelItemType.Click += new EventHandler(this.btn_SelItemType_Click);
            this.dtpTo.Enabled = false;
            this.dtpTo.Location = new Point(260, 0xb0);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new Size(0x73, 0x15);
            this.dtpTo.TabIndex = 0x35;
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Location = new Point(0x5f, 0xb0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new Size(0x73, 0x15);
            this.dtpFrom.TabIndex = 0x34;
            this.chk_Date.AutoSize = true;
            this.chk_Date.Location = new Point(20, 0xb2);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.Size = new Size(0x48, 0x10);
            this.chk_Date.TabIndex = 0x33;
            this.chk_Date.Text = "异动日期";
            this.chk_Date.UseVisualStyleBackColor = true;
            this.chk_Date.CheckedChanged += new EventHandler(this.chk_Date_CheckedChanged);
            this.txt_Pos.Location = new Point(0x5f, 0x77);
            this.txt_Pos.Multiline = true;
            this.txt_Pos.Name = "txt_Pos";
            this.txt_Pos.ScrollBars = ScrollBars.Horizontal;
            this.txt_Pos.Size = new Size(280, 0x2d);
            this.txt_Pos.TabIndex = 50;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(20, 0x45);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x47, 12);
            this.label5.TabIndex = 0x2f;
            this.label5.Text = "物       料";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(20, 0x77);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x47, 12);
            this.label4.TabIndex = 0x2e;
            this.label4.Text = "货       位";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(20, 0x12);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x47, 12);
            this.label3.TabIndex = 0x2d;
            this.label3.Text = "物 料 类 型";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x1ad, 0x13b);
            base.Controls.Add(this.grpDepart);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            this.ForeColor = SystemColors.ControlText;
            base.MinimizeBox = false;
            base.Name = "FrmStockMCheckFilter";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "盘点条件";
            base.Load += new EventHandler(this.FrmStockMCheckFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDepart.ResumeLayout(false);
            this.grpDepart.PerformLayout();
            base.ResumeLayout(false);
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void LoadBaseItemList()
        {
            this.LoadStockList("");
        }

        private void LoadStockList(string StockId)
        {
            string sErr = "";
            string sSql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (StockId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + StockId + "'";
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_WHId.DataSource = dataBySql.Tables["data"];
            this.cmb_WHId.DisplayMember = "cName";
            this.cmb_WHId.ValueMember = "cWHId";
        }

        private void rbtnDepart_CheckedChanged(object sender, EventArgs e)
        {
            this.grpDepart.Enabled = this.rbtnDepart.Checked;
        }

        private void tbpChkPart_Click(object sender, EventArgs e)
        {
        }
    }
}

