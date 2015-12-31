namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using Zqm.CommBase;

    public class FrmSelectCell : FrmSTable
    {
        private string _AreaId = "";
        private bool _IsMultiSelect = false;
        private string _SelAreaId = "";
        private string _SelDpsAddr = "";
        private string _SelPalletId = "";
        private WareType _WareHouseType = WareType.wtNone;
        private string _WHId = "";
        private BindingSource bdsList;
        private bool bIsResultOK = false;
        private Button btn_Reset;
        private Button btnClose;
        private Button btnOK;
        private Button btnQry;
        private bool bWHIsOK = false;
        private ComboBox cmb_cABC;
        public ComboBox cmb_cAreaId;
        private ComboBox cmb_cTypeId1;
        public ComboBox cmb_cWHId;
        public ComboBox cmb_nState;
        private DataGridViewTextBoxColumn col_cBatchNo;
        private DataGridViewTextBoxColumn col_cBNoIn;
        private DataGridViewTextBoxColumn col_cDtlRemark;
        private DataGridViewTextBoxColumn col_cMatOther;
        private DataGridViewTextBoxColumn col_cMatQCLevel;
        private DataGridViewTextBoxColumn col_cMatRemark;
        private DataGridViewTextBoxColumn col_cMatStyle;
        private DataGridViewTextBoxColumn col_cMName;
        private DataGridViewTextBoxColumn col_cMNo;
        private DataGridViewTextBoxColumn col_cPltRemark;
        private DataGridViewTextBoxColumn col_cSpec;
        private DataGridViewTextBoxColumn col_cSupplier;
        private DataGridViewTextBoxColumn col_cUnit;
        private DataGridViewTextBoxColumn col_dDate;
        private DataGridViewTextBoxColumn col_fQty;
        private DataGridViewTextBoxColumn col_nItemIn;
        private DataGridViewTextBoxColumn colcAreaId;
        private DataGridViewTextBoxColumn colcCellId;
        private DataGridViewTextBoxColumn colcLockState;
        private DataGridViewTextBoxColumn colcState;
        private DataGridViewTextBoxColumn colnCol;
        private DataGridViewTextBoxColumn colnDpsAddr;
        private DataGridViewTextBoxColumn colnLayer;
        private DataGridViewTextBoxColumn colnPalletId;
        private DataGridViewTextBoxColumn colnRow;
        private IContainer components = null;
        public DataGridView grdCellList;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private Panel pnlTop;
        private string selResult = "";
        private string strTbNameMain = "TWC_WareCell";
        private ToolTip toolTip1;
        private TextBox txt_cDtlRemark;
        private TextBox txt_cMatOther;
        private TextBox txt_cMatQCLevel;
        private TextBox txt_cMatStyle;
        private TextBox txt_cName;
        private TextBox txt_cPltRemark;
        private TextBox txt_cRemark;
        private TextBox txt_cSpec;
        private TextBox txt_cSupplier;
        private TextBox txt_nCol;
        private TextBox txt_nLayer;
        private TextBox txt_nRow;

        public FrmSelectCell()
        {
            this.InitializeComponent();
        }

        private void BindCmd()
        {
            this.bWHIsOK = false;
            int num = (int) this._WareHouseType;
            string sSql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (num > 0)
            {
                sSql = sSql + " where nType=" + num.ToString();
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')";
            }
            string sErr = "";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            if ((sErr.Trim() == "") || (sErr.Trim() == "0"))
            {
                this.cmb_cWHId.DataSource = dataBySql.Tables["data"];
                this.cmb_cWHId.ValueMember = "cWHId";
                this.cmb_cWHId.DisplayMember = "cName";
                this.bWHIsOK = true;
                this.cmb_cWHId.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("仓库信息初始化失败 " + sErr);
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.cmb_cABC.SelectedIndex = -1;
            this.cmb_cWHId.SelectedIndex = -1;
            this.cmb_cAreaId.SelectedIndex = -1;
            this.cmb_cTypeId1.SelectedIndex = -1;
            this.cmb_nState.SelectedIndex = -1;
            this.txt_cDtlRemark.Text = "";
            this.txt_cMatOther.Text = "";
            this.txt_cMatQCLevel.Text = "";
            this.txt_cMatStyle.Text = "";
            this.txt_cName.Text = "";
            this.txt_cRemark.Text = "";
            this.txt_cSpec.Text = "";
            this.txt_cSupplier.Text = "";
            this.txt_nCol.Text = "0";
            this.txt_nLayer.Text = "";
            this.txt_nRow.Text = "";
            this.cmb_cWHId.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.bIsResultOK = false;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool flag = true;
            object obj2 = null;
            foreach (DataGridViewRow row in this.grdCellList.SelectedRows)
            {
                if (flag)
                {
                    this.selResult = "" + row.Cells[this.colcCellId.Name].Value.ToString() + "";
                    obj2 = row.Cells[this.colnPalletId.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelPalletId = obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelPalletId = "";
                    }
                    obj2 = row.Cells[this.colcAreaId.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelAreaId = obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelAreaId = "";
                    }
                    obj2 = row.Cells[this.colnDpsAddr.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelDpsAddr = obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelDpsAddr = "";
                    }
                    flag = false;
                }
                else
                {
                    this.selResult = this.selResult + "," + row.Cells[this.colcCellId.Name].Value.ToString() + "";
                    obj2 = row.Cells[this.colnPalletId.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelPalletId = this._SelPalletId + "," + obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelPalletId = this._SelPalletId + ",";
                    }
                    obj2 = row.Cells[this.colcAreaId.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelAreaId = this._SelAreaId + "," + obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelAreaId = this._SelAreaId + ",";
                    }
                    obj2 = row.Cells[this.colnDpsAddr.Name].Value;
                    if (obj2 != null)
                    {
                        this._SelDpsAddr = this._SelDpsAddr + "," + obj2.ToString().Trim();
                    }
                    else
                    {
                        this._SelDpsAddr = this._SelDpsAddr + ",";
                    }
                }
            }
            if ((this.selResult.Trim() == "") && (MessageBox.Show("没有选择数据，需要退出吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No))
            {
                this.bIsResultOK = false;
            }
            else
            {
                this.bIsResultOK = true;
                base.Close();
            }
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            this.QueryCellList();
        }

        private void cmb_cWHId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.bWHIsOK && (this.cmb_cWHId.SelectedValue != null))
            {
                string sWHId = this.cmb_cWHId.SelectedValue.ToString().Trim();
                this.LoadAreaList(sWHId);
            }
        }

        private void cmb_nState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{Tab}");
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

        private void FrmSelectCell_Load(object sender, EventArgs e)
        {
            if (!this._IsMultiSelect)
            {
                this.toolTip1.SetToolTip(this.grdCellList, "请双击或单击选择，再点确定按钮！");
            }
            this.grdCellList.MultiSelect = this._IsMultiSelect;
            this.BindCmd();
            if ((this._WHId.Trim() != "") && (this._AreaId.Trim() != ""))
            {
                if (this.cmb_cWHId.SelectedValue != null)
                {
                    this.LoadAreaList(this.cmb_cWHId.SelectedValue.ToString().Trim());
                    this.cmb_cAreaId.SelectedValue = this._AreaId.Trim();
                    this.cmb_cAreaId.Enabled = false;
                }
            }
            else if (this.cmb_cWHId.SelectedValue != null)
            {
                this.LoadAreaList(this.cmb_cWHId.SelectedValue.ToString().Trim());
            }
            this.cmb_cWHId.Focus();
        }

        private void grdCellList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnOK_Click(null, null);
        }

        private void grdCellList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Return))
            {
                this.grdCellList_CellDoubleClick(null, null);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.pnlTop = new Panel();
            this.btn_Reset = new Button();
            this.label17 = new Label();
            this.txt_cDtlRemark = new TextBox();
            this.label16 = new Label();
            this.txt_cSupplier = new TextBox();
            this.label15 = new Label();
            this.label14 = new Label();
            this.cmb_cABC = new ComboBox();
            this.label13 = new Label();
            this.cmb_cTypeId1 = new ComboBox();
            this.txt_cRemark = new TextBox();
            this.label11 = new Label();
            this.txt_cMatOther = new TextBox();
            this.label12 = new Label();
            this.txt_cMatQCLevel = new TextBox();
            this.label7 = new Label();
            this.txt_cMatStyle = new TextBox();
            this.label8 = new Label();
            this.txt_cSpec = new TextBox();
            this.label9 = new Label();
            this.txt_cName = new TextBox();
            this.label10 = new Label();
            this.cmb_cAreaId = new ComboBox();
            this.label6 = new Label();
            this.cmb_cWHId = new ComboBox();
            this.btnQry = new Button();
            this.txt_nLayer = new TextBox();
            this.label4 = new Label();
            this.txt_nCol = new TextBox();
            this.label3 = new Label();
            this.txt_nRow = new TextBox();
            this.label2 = new Label();
            this.cmb_nState = new ComboBox();
            this.label1 = new Label();
            this.label5 = new Label();
            this.grdCellList = new DataGridView();
            this.bdsList = new BindingSource(this.components);
            this.btnOK = new Button();
            this.btnClose = new Button();
            this.toolTip1 = new ToolTip(this.components);
            this.panel1 = new Panel();
            this.txt_cPltRemark = new TextBox();
            this.label18 = new Label();
            this.colcCellId = new DataGridViewTextBoxColumn();
            this.colnPalletId = new DataGridViewTextBoxColumn();
            this.colcState = new DataGridViewTextBoxColumn();
            this.colcLockState = new DataGridViewTextBoxColumn();
            this.col_cMNo = new DataGridViewTextBoxColumn();
            this.col_cMName = new DataGridViewTextBoxColumn();
            this.col_cSpec = new DataGridViewTextBoxColumn();
            this.col_cMatStyle = new DataGridViewTextBoxColumn();
            this.col_cBatchNo = new DataGridViewTextBoxColumn();
            this.col_fQty = new DataGridViewTextBoxColumn();
            this.col_dDate = new DataGridViewTextBoxColumn();
            this.col_cUnit = new DataGridViewTextBoxColumn();
            this.col_cSupplier = new DataGridViewTextBoxColumn();
            this.col_cMatOther = new DataGridViewTextBoxColumn();
            this.col_cMatRemark = new DataGridViewTextBoxColumn();
            this.col_cDtlRemark = new DataGridViewTextBoxColumn();
            this.colnDpsAddr = new DataGridViewTextBoxColumn();
            this.colcAreaId = new DataGridViewTextBoxColumn();
            this.colnRow = new DataGridViewTextBoxColumn();
            this.colnCol = new DataGridViewTextBoxColumn();
            this.colnLayer = new DataGridViewTextBoxColumn();
            this.col_cBNoIn = new DataGridViewTextBoxColumn();
            this.col_nItemIn = new DataGridViewTextBoxColumn();
            this.col_cMatQCLevel = new DataGridViewTextBoxColumn();
            this.col_cPltRemark = new DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            ((ISupportInitialize) this.grdCellList).BeginInit();
            ((ISupportInitialize) this.bdsList).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.pnlTop.Controls.Add(this.txt_cPltRemark);
            this.pnlTop.Controls.Add(this.label18);
            this.pnlTop.Controls.Add(this.btn_Reset);
            this.pnlTop.Controls.Add(this.label17);
            this.pnlTop.Controls.Add(this.txt_cDtlRemark);
            this.pnlTop.Controls.Add(this.label16);
            this.pnlTop.Controls.Add(this.txt_cSupplier);
            this.pnlTop.Controls.Add(this.label15);
            this.pnlTop.Controls.Add(this.label14);
            this.pnlTop.Controls.Add(this.cmb_cABC);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.cmb_cTypeId1);
            this.pnlTop.Controls.Add(this.txt_cRemark);
            this.pnlTop.Controls.Add(this.label11);
            this.pnlTop.Controls.Add(this.txt_cMatOther);
            this.pnlTop.Controls.Add(this.label12);
            this.pnlTop.Controls.Add(this.txt_cMatQCLevel);
            this.pnlTop.Controls.Add(this.label7);
            this.pnlTop.Controls.Add(this.txt_cMatStyle);
            this.pnlTop.Controls.Add(this.label8);
            this.pnlTop.Controls.Add(this.txt_cSpec);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.txt_cName);
            this.pnlTop.Controls.Add(this.label10);
            this.pnlTop.Controls.Add(this.cmb_cAreaId);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Controls.Add(this.cmb_cWHId);
            this.pnlTop.Controls.Add(this.btnQry);
            this.pnlTop.Controls.Add(this.txt_nLayer);
            this.pnlTop.Controls.Add(this.label4);
            this.pnlTop.Controls.Add(this.txt_nCol);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.txt_nRow);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.cmb_nState);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.label5);
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Location = new Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new Size(0x390, 0x7b);
            this.pnlTop.TabIndex = 0;
            this.btn_Reset.Location = new Point(0x34c, 0x5f);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new Size(0x3f, 0x17);
            this.btn_Reset.TabIndex = 0x22;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new EventHandler(this.btn_Reset_Click);
            this.label17.BackColor = SystemColors.ActiveCaption;
            this.label17.Location = new Point(9, 0x22);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x380, 1);
            this.label17.TabIndex = 0x21;
            this.txt_cDtlRemark.Location = new Point(0x1ff, 0x60);
            this.txt_cDtlRemark.Name = "txt_cDtlRemark";
            this.txt_cDtlRemark.Size = new Size(0xa2, 0x15);
            this.txt_cDtlRemark.TabIndex = 0x20;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(450, 0x63);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x35, 12);
            this.label16.TabIndex = 0x1f;
            this.label16.Text = "库存备注";
            this.txt_cSupplier.Location = new Point(0x3e, 0x60);
            this.txt_cSupplier.Name = "txt_cSupplier";
            this.txt_cSupplier.Size = new Size(0x167, 0x15);
            this.txt_cSupplier.TabIndex = 30;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(6, 100);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x35, 12);
            this.label15.TabIndex = 0x1d;
            this.label15.Text = "供 应 商";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x2cf, 0x48);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x17, 12);
            this.label14.TabIndex = 0x1c;
            this.label14.Text = "ABC";
            this.cmb_cABC.FormattingEnabled = true;
            this.cmb_cABC.Items.AddRange(new object[] { "A", "B", "C" });
            this.cmb_cABC.Location = new Point(0x30b, 0x44);
            this.cmb_cABC.Name = "cmb_cABC";
            this.cmb_cABC.Size = new Size(0x7e, 20);
            this.cmb_cABC.TabIndex = 0x1b;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x1c3, 0x48);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x1a;
            this.label13.Text = "物料类型";
            this.cmb_cTypeId1.FormattingEnabled = true;
            this.cmb_cTypeId1.Location = new Point(510, 0x44);
            this.cmb_cTypeId1.Name = "cmb_cTypeId1";
            this.cmb_cTypeId1.Size = new Size(0xa1, 20);
            this.cmb_cTypeId1.TabIndex = 0x19;
            this.txt_cRemark.Location = new Point(0x11f, 0x44);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.Size = new Size(0xa2, 0x15);
            this.txt_cRemark.TabIndex = 0x18;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xe8, 0x48);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x17;
            this.label11.Text = "物料备注";
            this.txt_cMatOther.Location = new Point(0x3e, 0x44);
            this.txt_cMatOther.Name = "txt_cMatOther";
            this.txt_cMatOther.Size = new Size(0xa2, 0x15);
            this.txt_cMatOther.TabIndex = 0x16;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(6, 0x48);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x35, 12);
            this.label12.TabIndex = 0x15;
            this.label12.Text = "其他属性";
            this.txt_cMatQCLevel.Location = new Point(0x30b, 0x2c);
            this.txt_cMatQCLevel.Name = "txt_cMatQCLevel";
            this.txt_cMatQCLevel.Size = new Size(0x7e, 0x15);
            this.txt_cMatQCLevel.TabIndex = 20;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x2cf, 0x30);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x13;
            this.label7.Text = "质量等级";
            this.txt_cMatStyle.Location = new Point(0x1fd, 0x2c);
            this.txt_cMatStyle.Name = "txt_cMatStyle";
            this.txt_cMatStyle.Size = new Size(0xa2, 0x15);
            this.txt_cMatStyle.TabIndex = 0x12;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x1c3, 0x30);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x11;
            this.label8.Text = "物料款式";
            this.txt_cSpec.Location = new Point(0x11f, 0x2c);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.Size = new Size(0xa2, 0x15);
            this.txt_cSpec.TabIndex = 0x10;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xe8, 0x30);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "物料规格";
            this.txt_cName.Location = new Point(0x3e, 0x2c);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.Size = new Size(0xa2, 0x15);
            this.txt_cName.TabIndex = 14;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(6, 0x30);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "物料名称";
            this.cmb_cAreaId.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cAreaId.FormattingEnabled = true;
            this.cmb_cAreaId.Location = new Point(0x11f, 7);
            this.cmb_cAreaId.Name = "cmb_cAreaId";
            this.cmb_cAreaId.Size = new Size(0x9e, 20);
            this.cmb_cAreaId.TabIndex = 10;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xe8, 11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "货  区：";
            this.cmb_cWHId.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0x3e, 7);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(0xa2, 20);
            this.cmb_cWHId.TabIndex = 8;
            this.cmb_cWHId.SelectedValueChanged += new EventHandler(this.cmb_cWHId_SelectedValueChanged);
            this.btnQry.Location = new Point(0x30d, 0x5f);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new Size(0x3a, 0x17);
            this.btnQry.TabIndex = 4;
            this.btnQry.Text = "查询(&Q)";
            this.btnQry.UseVisualStyleBackColor = true;
            this.btnQry.Click += new EventHandler(this.btnQry_Click);
            this.txt_nLayer.Location = new Point(0x2ba, 6);
            this.txt_nLayer.Name = "txt_nLayer";
            this.txt_nLayer.Size = new Size(0x13, 0x15);
            this.txt_nLayer.TabIndex = 3;
            this.txt_nLayer.KeyDown += new KeyEventHandler(this.cmb_nState_KeyDown);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x2a2, 10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "层：";
            this.txt_nCol.Location = new Point(0x28d, 6);
            this.txt_nCol.Name = "txt_nCol";
            this.txt_nCol.Size = new Size(0x13, 0x15);
            this.txt_nCol.TabIndex = 2;
            this.txt_nCol.KeyDown += new KeyEventHandler(this.cmb_nState_KeyDown);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x274, 10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "列：";
            this.txt_nRow.Location = new Point(0x25e, 6);
            this.txt_nRow.Name = "txt_nRow";
            this.txt_nRow.Size = new Size(0x13, 0x15);
            this.txt_nRow.TabIndex = 1;
            this.txt_nRow.KeyDown += new KeyEventHandler(this.cmb_nState_KeyDown);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x246, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "行：";
            this.cmb_nState.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_nState.FormattingEnabled = true;
            this.cmb_nState.Items.AddRange(new object[] { "空位", "空盘", "有货", "全部" });
            this.cmb_nState.Location = new Point(0x1fd, 7);
            this.cmb_nState.Name = "cmb_nState";
            this.cmb_nState.Size = new Size(0x43, 20);
            this.cmb_nState.TabIndex = 0;
            this.cmb_nState.KeyDown += new KeyEventHandler(this.cmb_nState_KeyDown);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x1c3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "状  态：";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(7, 11);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "仓  库：";
            this.grdCellList.AllowUserToAddRows = false;
            this.grdCellList.AllowUserToDeleteRows = false;
            this.grdCellList.AllowUserToOrderColumns = true;
            this.grdCellList.AutoGenerateColumns = false;
            this.grdCellList.Columns.AddRange(new DataGridViewColumn[] { 
                this.colcCellId, this.colnPalletId, this.colcState, this.colcLockState, this.col_cMNo, this.col_cMName, this.col_cSpec, this.col_cMatStyle, this.col_cBatchNo, this.col_fQty, this.col_dDate, this.col_cUnit, this.col_cSupplier, this.col_cMatOther, this.col_cMatRemark, this.col_cDtlRemark, 
                this.colnDpsAddr, this.colcAreaId, this.colnRow, this.colnCol, this.colnLayer, this.col_cBNoIn, this.col_nItemIn, this.col_cMatQCLevel, this.col_cPltRemark
             });
            this.grdCellList.DataSource = this.bdsList;
            this.grdCellList.Dock = DockStyle.Fill;
            this.grdCellList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdCellList.Location = new Point(0, 0x7b);
            this.grdCellList.Name = "grdCellList";
            this.grdCellList.ReadOnly = true;
            this.grdCellList.RowHeadersVisible = false;
            this.grdCellList.RowTemplate.Height = 0x17;
            this.grdCellList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdCellList.Size = new Size(0x390, 0x161);
            this.grdCellList.TabIndex = 0x49;
            this.grdCellList.Tag = "8";
            this.toolTip1.SetToolTip(this.grdCellList, "拖动鼠标可以多选");
            this.grdCellList.CellDoubleClick += new DataGridViewCellEventHandler(this.grdCellList_CellDoubleClick);
            this.grdCellList.KeyDown += new KeyEventHandler(this.grdCellList_KeyDown);
            this.btnOK.Location = new Point(0x16d, 0x12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x4a;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnClose.Location = new Point(0x1d8, 0x12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0x4b;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x1dc);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x390, 0x39);
            this.panel1.TabIndex = 0x4c;
            this.txt_cPltRemark.Location = new Point(0x30b, 8);
            this.txt_cPltRemark.Name = "txt_cPltRemark";
            this.txt_cPltRemark.Size = new Size(0x80, 0x15);
            this.txt_cPltRemark.TabIndex = 0x24;
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x2d2, 11);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x3b, 12);
            this.label18.TabIndex = 0x23;
            this.label18.Text = "托盘/备注";
            this.colcCellId.DataPropertyName = "cPosId";
            this.colcCellId.HeaderText = "货位号";
            this.colcCellId.Name = "colcCellId";
            this.colcCellId.ReadOnly = true;
            this.colcCellId.ToolTipText = "货位号";
            this.colnPalletId.DataPropertyName = "nPalletId";
            this.colnPalletId.HeaderText = "托盘号";
            this.colnPalletId.Name = "colnPalletId";
            this.colnPalletId.ReadOnly = true;
            this.colnPalletId.Width = 70;
            this.colcState.DataPropertyName = "cStatusStore";
            this.colcState.HeaderText = "状态";
            this.colcState.Name = "colcState";
            this.colcState.ReadOnly = true;
            this.colcState.ToolTipText = "状态";
            this.colcState.Width = 80;
            this.colcLockState.DataPropertyName = "cStatusWork";
            this.colcLockState.HeaderText = "锁定状态";
            this.colcLockState.Name = "colcLockState";
            this.colcLockState.ReadOnly = true;
            this.colcLockState.ToolTipText = "锁定状态";
            this.colcLockState.Width = 80;
            this.col_cMNo.DataPropertyName = "cMNo";
            this.col_cMNo.HeaderText = "物料编码";
            this.col_cMNo.Name = "col_cMNo";
            this.col_cMNo.ReadOnly = true;
            this.col_cMName.DataPropertyName = "cMName";
            this.col_cMName.HeaderText = "物料名称";
            this.col_cMName.Name = "col_cMName";
            this.col_cMName.ReadOnly = true;
            this.col_cSpec.DataPropertyName = "cSpec";
            this.col_cSpec.HeaderText = "物料规格";
            this.col_cSpec.Name = "col_cSpec";
            this.col_cSpec.ReadOnly = true;
            this.col_cMatStyle.DataPropertyName = "cMatStyle";
            this.col_cMatStyle.HeaderText = "物料款式";
            this.col_cMatStyle.Name = "col_cMatStyle";
            this.col_cMatStyle.ReadOnly = true;
            this.col_cMatStyle.Width = 80;
            this.col_cBatchNo.DataPropertyName = "cBatchNo";
            this.col_cBatchNo.HeaderText = "物料批号";
            this.col_cBatchNo.Name = "col_cBatchNo";
            this.col_cBatchNo.ReadOnly = true;
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "数量";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            this.col_dDate.DataPropertyName = "dDate";
            this.col_dDate.HeaderText = "日期";
            this.col_dDate.Name = "col_dDate";
            this.col_dDate.ReadOnly = true;
            this.col_cUnit.DataPropertyName = "cUnit";
            this.col_cUnit.HeaderText = "计量单位";
            this.col_cUnit.Name = "col_cUnit";
            this.col_cUnit.ReadOnly = true;
            this.col_cSupplier.DataPropertyName = "cSupplier";
            this.col_cSupplier.HeaderText = "供应商";
            this.col_cSupplier.Name = "col_cSupplier";
            this.col_cSupplier.ReadOnly = true;
            this.col_cMatOther.DataPropertyName = "cMatOther";
            this.col_cMatOther.HeaderText = "物料其他属性";
            this.col_cMatOther.Name = "col_cMatOther";
            this.col_cMatOther.ReadOnly = true;
            this.col_cMatRemark.DataPropertyName = "cRemark";
            this.col_cMatRemark.HeaderText = "物料备注";
            this.col_cMatRemark.Name = "col_cMatRemark";
            this.col_cMatRemark.ReadOnly = true;
            this.col_cDtlRemark.DataPropertyName = "cDtlRemark";
            this.col_cDtlRemark.HeaderText = "库存备注";
            this.col_cDtlRemark.Name = "col_cDtlRemark";
            this.col_cDtlRemark.ReadOnly = true;
            this.colnDpsAddr.DataPropertyName = "nDpsAddr";
            this.colnDpsAddr.HeaderText = "DPS地址";
            this.colnDpsAddr.Name = "colnDpsAddr";
            this.colnDpsAddr.ReadOnly = true;
            this.colnDpsAddr.Visible = false;
            this.colcAreaId.DataPropertyName = "cAreaId";
            this.colcAreaId.HeaderText = "货区号";
            this.colcAreaId.Name = "colcAreaId";
            this.colcAreaId.ReadOnly = true;
            this.colcAreaId.Width = 70;
            this.colnRow.DataPropertyName = "nRow";
            this.colnRow.HeaderText = "行";
            this.colnRow.Name = "colnRow";
            this.colnRow.ReadOnly = true;
            this.colnRow.ToolTipText = "行号";
            this.colnRow.Width = 0x23;
            this.colnCol.DataPropertyName = "nCol";
            this.colnCol.HeaderText = "列";
            this.colnCol.Name = "colnCol";
            this.colnCol.ReadOnly = true;
            this.colnCol.ToolTipText = "列号";
            this.colnCol.Width = 0x23;
            this.colnLayer.DataPropertyName = "nLayer";
            this.colnLayer.HeaderText = "层";
            this.colnLayer.Name = "colnLayer";
            this.colnLayer.ReadOnly = true;
            this.colnLayer.ToolTipText = "层号";
            this.colnLayer.Width = 0x23;
            this.col_cBNoIn.DataPropertyName = "cBNoIn";
            this.col_cBNoIn.HeaderText = "库存单号";
            this.col_cBNoIn.Name = "col_cBNoIn";
            this.col_cBNoIn.ReadOnly = true;
            this.col_nItemIn.DataPropertyName = "nItemIn";
            this.col_nItemIn.HeaderText = "库存单明细号";
            this.col_nItemIn.Name = "col_nItemIn";
            this.col_nItemIn.ReadOnly = true;
            this.col_cMatQCLevel.DataPropertyName = "cMatQCLevel";
            this.col_cMatQCLevel.HeaderText = "质量等级";
            this.col_cMatQCLevel.Name = "col_cMatQCLevel";
            this.col_cMatQCLevel.ReadOnly = true;
            this.col_cMatQCLevel.Width = 70;
            this.col_cPltRemark.DataPropertyName = "cPltRemark";
            this.col_cPltRemark.HeaderText = "托盘备注";
            this.col_cPltRemark.Name = "col_cPltRemark";
            this.col_cPltRemark.ReadOnly = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x390, 0x215);
            base.Controls.Add(this.grdCellList);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pnlTop);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "FrmSelectCell";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "货位查询";
            base.Load += new EventHandler(this.FrmSelectCell_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((ISupportInitialize) this.grdCellList).EndInit();
            ((ISupportInitialize) this.bdsList).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadAreaList(string sWHId)
        {
            string sErr = "";
            string sSql = "select cAreaId,cAreaName from TWC_WArea where  bused=1 and cWHId='" + sWHId.Trim() + "' ";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                this.cmb_cAreaId.DataSource = set.Tables["data"];
                this.cmb_cAreaId.DisplayMember = "cAreaName";
                this.cmb_cAreaId.ValueMember = "cAreaId";
                if ((this.cmb_cAreaId.Items.Count > 0) && ((this._WHId.Trim() != "") && (this._AreaId.Trim() != "")))
                {
                    this.cmb_cAreaId.SelectedValue = this._AreaId.Trim();
                }
            }
        }

        private bool QueryCellList()
        {
            string str;
            StringBuilder builder = new StringBuilder("");
            StringBuilder builder2 = new StringBuilder("");
            builder2.Append("select wc.cPosId,wc.nPalletId,wc.nStatusWork,wc.nRow,wc.nCol,wc.nLayer,isnull(wc.cRemark,'') cRemark,wc.cAreaId,");
            builder2.Append(" wc.cAreaName,wc.cWHId,wc.cWHName,wc.cStatusWork,wc.nStatusStore,wc.cStatusStore, ");
            builder2.Append(" isnull(st.cItemId,'') cMNo,isnull(st.cMName,'') cMName,isnull(st.cBatchNo,'') cBatchNo, sum(isnull(st.fQty,0)) fQty,");
            builder2.Append(" isnull(st.cUnit,'') cUnit,min(isnull(st.cDtlCSId,'')) cCSId,min(isnull(st.cDtlSupplier,'')) cSupplier,min(isnull(st.cStoreRemark,'')) ");
            builder2.Append(" cDtlRemark,min(isnull(st.cSpec,'')) cSpec,isnull(st.cBNoIn,'') cBNoIn,isnull(st.nItemIn,0) nItemIn,min(st.dDate) dDate,max(wc.cPltRemark) cPltRemark  ");
            builder2.Append(" from V_WareCellStatus wc ");
            builder2.Append(" left join V_StoreItemList st on isnull(wc.nPalletId,'')= st.nPalletId  ");
            builder.Append(" where wc.nStatusWork <=2 ");
            if (base.UserInformation.UserId != "90101001")
            {
                builder.Append(" and isnull(wc.cMAreaId,' ') in (select cMAreaId from V_UserMArea where cUserId='" + base.UserInformation.UserId + "')");
            }
            if (((this.cmb_cWHId.Text.Trim() != "") && (this.cmb_cWHId.SelectedValue != null)) && (this.cmb_cWHId.SelectedIndex > -1))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and wc.cWHId='" + this.cmb_cWHId.SelectedValue.ToString() + "'");
                }
                else
                {
                    builder.Append(" where wc.cWHId='" + this.cmb_cWHId.SelectedValue.ToString() + "'");
                }
            }
            if (((this.cmb_cAreaId.Text.Trim() != "") && (this.cmb_cAreaId.SelectedValue != null)) && (this.cmb_cAreaId.SelectedIndex > -1))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and wc.cAreaId='" + this.cmb_cAreaId.SelectedValue.ToString() + "'");
                }
                else
                {
                    builder.Append(" where wc.cAreaId='" + this.cmb_cAreaId.SelectedValue.ToString() + "'");
                }
            }
            int num = 0;
            if ((this.txt_nRow.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_nRow.Text.Trim()))
            {
                num = int.Parse(this.txt_nRow.Text.Trim());
                if (num > 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and wc.nRow=" + num.ToString() + "");
                    }
                    else
                    {
                        builder.Append(" where wc.nRow=" + num.ToString() + "");
                    }
                }
            }
            num = 0;
            if (((this.txt_nCol.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_nCol.Text.Trim())) && (int.Parse(this.txt_nCol.Text.Trim()) > 0))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and wc.nCol=" + this.txt_nCol.Text.Trim() + "");
                }
                else
                {
                    builder.Append(" where wc.nCol=" + this.txt_nCol.Text.Trim() + "");
                }
            }
            num = 0;
            if (((this.txt_nLayer.Text.Trim() != "") && FrmSTable.IsInteger(this.txt_nLayer.Text.Trim())) && (int.Parse(this.txt_nLayer.Text.Trim()) > 0))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and wc.nLayer=" + this.txt_nLayer.Text.Trim() + "");
                }
                else
                {
                    builder.Append(" where wc.nLayer=" + this.txt_nLayer.Text.Trim() + "");
                }
            }
            if (((this.cmb_nState.Text.Trim() != "") && (this.cmb_nState.SelectedIndex < 3)) && (this.cmb_nState.Text.Trim() != "全部"))
            {
                str = "";
                switch (this.cmb_nState.SelectedIndex)
                {
                    case 0:
                        str = "= -1";
                        break;

                    case 1:
                        str = " = 0 ";
                        break;

                    case 2:
                        str = ">=1";
                        break;
                }
                if (builder.Length > 0)
                {
                    builder.Append(" and wc.nStatusStore" + str + " ");
                }
                else
                {
                    builder.Append(" where wc.nStatusStore" + str + " ");
                }
            }
            if (this.txt_cPltRemark.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ( (isnull(wc.cPltRemark,' ') like '%" + this.txt_cPltRemark.Text.Trim() + "%') or (isnull(wc.nPalletId,' ')='" + this.txt_cPltRemark.Text.Trim() + "') )");
                }
                else
                {
                    builder.Append(" where ( (isnull(wc.cPltRemark,' ') like '%" + this.txt_cPltRemark.Text.Trim() + "%') or (isnull(wc.nPalletId,' ')='" + this.txt_cPltRemark.Text.Trim() + "') )");
                }
            }
            if (this.txt_cName.Text.Trim() != "")
            {
                str = this.txt_cName.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and ((isnull(st.cMNo,'~') like '%" + str + "%') or (isnull(st.cMName,'~') like '%" + str + "%') or (isnull(st.cWBJM,'~') like '%" + str + "%')  or (isnull(st.cPYJM,'~') like '%" + str + "%') )");
                }
                else
                {
                    builder.Append(" where ((isnull(st.cMNo,'~') like '%" + str + "%') or (isnull(st.cMName,'~') like '%" + str + "%') or (isnull(st.cWBJM,'~') like '%" + str + "%')  or (isnull(st.cPYJM,'~') like '%" + str + "%') )");
                }
            }
            if (this.txt_cSpec.Text.Trim() != "")
            {
                str = this.txt_cSpec.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cSpec,'~') like '%" + str + "%')");
                }
                else
                {
                    builder.Append(" where (isnull(st.cSpec,'~') like '%" + str + "%')");
                }
            }
            if (this.txt_cRemark.Text.Trim() != "")
            {
                str = this.txt_cRemark.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cRemark,'~') like '%" + str + "%')");
                }
                else
                {
                    builder.Append(" where (isnull(st.cRemark,'~') like '%" + str + "%')");
                }
            }
            if (this.txt_cMatStyle.Text.Trim() != "")
            {
                str = this.txt_cMatStyle.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cMatStyle,'~') like '%" + str + "%')");
                }
                else
                {
                    builder.Append(" where (isnull(st.cMatStyle,'~') like '%" + str + "%')");
                }
            }
            if (this.txt_cMatQCLevel.Text.Trim() != "")
            {
                str = this.txt_cMatQCLevel.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cMatQCLevel,'~') like '%" + str + "%')");
                }
                else
                {
                    builder.Append(" where (isnull(st.cMatQCLevel,'~') like '%" + str + "%')");
                }
            }
            if (this.txt_cMatOther.Text.Trim() != "")
            {
                str = this.txt_cMatOther.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cMatOther,'~') like '%" + str + "%')");
                }
                else
                {
                    builder.Append(" where (isnull(st.cMatOther,'~') like '%" + str + "%')");
                }
            }
            if (((this.cmb_cTypeId1.Text.Trim() != "") && (this.cmb_cTypeId1.SelectedValue != null)) && (this.cmb_cTypeId1.SelectedIndex > -1))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cTypeId1,'~') like '%" + this.cmb_cTypeId1.SelectedValue.ToString() + "%')");
                }
                else
                {
                    builder.Append(" where (isnull(st.cTypeId1,'~') like '%" + this.cmb_cTypeId1.SelectedValue.ToString() + "%')");
                }
            }
            if (this.cmb_cABC.Text.Trim() != "")
            {
                str = this.cmb_cABC.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cABC,'~') like '%" + str + "%') ");
                }
                else
                {
                    builder.Append(" where (isnull(st.cABC,'~') like '%" + str + "%') ");
                }
            }
            if (this.txt_cSupplier.Text.Trim() != "")
            {
                str = this.txt_cSupplier.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and ((isnull(st.cDtlCSId,'~') like '%" + str + "%') or (isnull(st.cDtlSupplier,'~') like '%" + str + "%') or (isnull(st.cDtlWBJM,'~') like '%" + str + "%') or (isnull(st.cDtlPYJM,'~') like '%" + str + "%'))");
                }
                else
                {
                    builder.Append(" where ((isnull(st.cDtlCSId,'~') like '%" + str + "%') or (isnull(st.cDtlSupplier,'~') like '%" + str + "%') or (isnull(st.cDtlWBJM,'~') like '%" + str + "%') or (isnull(st.cDtlPYJM,'~') like '%" + str + "%'))");
                }
            }
            if (this.txt_cDtlRemark.Text.Trim() != "")
            {
                str = this.txt_cDtlRemark.Text.Trim();
                if (builder.Length > 0)
                {
                    builder.Append(" and (isnull(st.cDtlRemark,'~') like '%" + str + "%') ");
                }
                else
                {
                    builder.Append(" where (isnull(st.cDtlRemark,'~') like '%" + str + "%') ");
                }
            }
            builder2.Append(builder.ToString());
            builder2.Append(" group by wc.cPosId,wc.nPalletId,wc.nStatusWork,wc.nRow,wc.nCol,wc.nLayer,isnull(wc.cRemark,''),wc.cAreaId, ");
            builder2.Append(" wc.cAreaName,wc.cWHId,wc.cWHName,wc.cStatusWork,wc.nStatusStore,wc.cStatusStore, ");
            builder2.Append("  isnull(st.cItemId,''),isnull(st.cMName,''),isnull(st.cBatchNo,''),isnull(st.cUnit,''),isnull(st.cBNoIn,''),isnull(st.nItemIn,0) ");
            bool flag = false;
            this.grdCellList.AutoGenerateColumns = false;
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(builder2.ToString(), "data", 0, 0, out sErr);
            flag = sErr == "";
            builder2.Remove(0, builder2.Length);
            builder.Remove(0, builder.Length);
            if (flag)
            {
                this.bdsList.DataSource = set.Tables["data"];
                this.grdCellList.Focus();
                this.grdCellList.DataSource = this.bdsList;
                return flag;
            }
            MessageBox.Show(sErr);
            return flag;
        }

        public string AreaId
        {
            get
            {
                return this._AreaId.Trim();
            }
            set
            {
                this._AreaId = value.Trim();
                if (this.cmb_cAreaId.Items.Count > 0)
                {
                    this.cmb_cAreaId.SelectedValue = this._AreaId.Trim();
                    this.cmb_cAreaId.Enabled = false;
                }
                else
                {
                    this.cmb_cAreaId.Enabled = true;
                }
            }
        }

        public bool BIsResult
        {
            get
            {
                return this.bIsResultOK;
            }
            set
            {
                this.bIsResultOK = value;
            }
        }

        public bool IsMultiSelect
        {
            get
            {
                return this._IsMultiSelect;
            }
            set
            {
                this._IsMultiSelect = value;
                if (!this._IsMultiSelect)
                {
                    this.toolTip1.SetToolTip(this.grdCellList, "请双击或单击选择，再点确定按钮！");
                }
                this.grdCellList.MultiSelect = value;
            }
        }

        public string SelAreaId
        {
            get
            {
                return this._SelAreaId.Trim();
            }
            set
            {
                this._SelAreaId = value.Trim();
            }
        }

        public string SelDpsAddr
        {
            get
            {
                return this._SelDpsAddr.Trim();
            }
            set
            {
                this._SelDpsAddr = value.Trim();
            }
        }

        public string SelPalletId
        {
            get
            {
                return this._SelPalletId.Trim();
            }
            set
            {
                this._SelPalletId = value.Trim();
            }
        }

        public string SelResult
        {
            get
            {
                return this.selResult;
            }
            set
            {
                this.selResult = value;
            }
        }

        public WareType WareHouseType
        {
            get
            {
                return this._WareHouseType;
            }
            set
            {
                this._WareHouseType = value;
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
                this._WHId = value.Trim();
                if (this.cmb_cWHId.Items.Count > 0)
                {
                    this.cmb_cWHId.SelectedValue = this._WHId.Trim();
                    this.cmb_cWHId.Enabled = false;
                }
                else
                {
                    this.cmb_cWHId.Enabled = true;
                }
            }
        }
    }
}

