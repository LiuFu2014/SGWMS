namespace WareStoreMS
{
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using UI;

    public class frmChkDtlWrite : FrmSTable
    {
        private double _BadQty = 0.0;
        private string _BatchNo = "";
        private string _BNoIn = "";
        private string _BoxId = "";
        private string _CheckNo = "";
        private double _Diff = 0.0;
        private bool _IsNewAddMat = false;
        private bool _IsOK = false;
        private int _ItemIn = 0;
        private string _MName = "";
        private string _MNo = "";
        private string _PalletId = "";
        private string _PosId = "";
        private int _QCStatus = 0;
        private double _Qty = 0.0;
        private string _Spec = "";
        private string _Unit = "";
        private string _WHId = "";
        private bool bIsNew = false;
        private Button btn_AddMat;
        private Button btn_SelBNoIn;
        private Button btnClose;
        private Button btnOK;
        private IContainer components = null;
        private GroupBox groupBox1;
        public Label label1;
        public Label label10;
        public Label label11;
        public Label label12;
        public Label label13;
        public Label label14;
        public Label label15;
        public Label label16;
        public Label label17;
        public Label label18;
        public Label label2;
        public Label label3;
        public Label label4;
        public Label label5;
        public Label label6;
        public Label label7;
        public Label label8;
        public Label label9;
        private ToolTip toolTip1;
        private TextBox txt_cBatchNo;
        private TextBox txt_cBNoIn;
        private TextBox txt_cBoxId;
        private TextBox txt_cMNo;
        private TextBox txt_cPosId;
        private TextBox txt_cSpec;
        private TextBox txt_cUnit;
        private TextBox txt_cWHId;
        private TextBox txt_fBad;
        private TextBox txt_fDiff;
        private TextBox txt_nItemIn;
        private TextBox txt_nPalletId;
        private TextBox txt_Qty;
        private TextBox txt_RQty;
        private TextBox txt_RTotal;

        public frmChkDtlWrite()
        {
            this.InitializeComponent();
        }

        private void btn_AddMat_Click(object sender, EventArgs e)
        {
            frmSelMaterial material = new frmSelMaterial {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation,
                DoSelMatEvent = new DoSelMaterialEvent(this.doSelMaterial)
            };
            material.ShowDialog();
            material.Dispose();
        }

        private void btn_SelBNoIn_Click(object sender, EventArgs e)
        {
            frmSelIOBillMat mat = new frmSelIOBillMat {
                BClass = 1,
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            mat.txt_cName.Text = this.txt_cMNo.Text;
            mat.DoSelIOStoreMatBillData = new DoSelIOStoreMatBillDataEvent(this.doSelIOStoreMatBillData);
            mat.ShowDialog();
            mat.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this._IsOK = false;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Exception exception;
            string sErr = "";
            double num = 0.0;
            double num2 = 0.0;
            double pDiff = 0.0;
            double pBad = 0.0;
            if (this.txt_Qty.Text.Trim() != "")
            {
                num = double.Parse(this.txt_Qty.Text.Trim());
            }
            if (this.txt_RQty.Text.Trim() != "")
            {
                try
                {
                    num2 = double.Parse(this.txt_RQty.Text.Trim());
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    MessageBox.Show("实盘数录入有误，数值非法！");
                    this.txt_RQty.SelectAll();
                    this.txt_RQty.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("对不起，实盘数不能为空！");
                this.txt_RQty.Focus();
                return;
            }
            if (this.txt_fBad.Text.Trim() != "")
            {
                try
                {
                    pBad = double.Parse(this.txt_fBad.Text.Trim());
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    MessageBox.Show("不良品数录入有误，数值非法！");
                    this.txt_fBad.SelectAll();
                    this.txt_fBad.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("对不起，不良品数不能为空！");
                this.txt_fBad.Focus();
                return;
            }
            if (pBad < 0.0)
            {
                MessageBox.Show("对不起不良品数不能为负数！");
                this.txt_fBad.SelectAll();
                this.txt_fBad.Focus();
            }
            else if (this.txt_fDiff.Text.Trim() == "")
            {
                MessageBox.Show("对不起，损溢数不能为空！");
                this.txt_fDiff.Focus();
            }
            else
            {
                if (FrmSTable.IsNumberic(this.txt_fDiff.Text.Trim()))
                {
                    pDiff = double.Parse(this.txt_fDiff.Text.Trim());
                }
                else
                {
                    MessageBox.Show("损溢数录入有误，数值非法！");
                    this.txt_fDiff.SelectAll();
                    this.txt_fDiff.Focus();
                    return;
                }
                if (num2 != (num + pDiff))
                {
                    MessageBox.Show("对不起，实盘数 不等于　 帐面数 + 损溢数");
                    this.txt_fDiff.SelectAll();
                    this.txt_fDiff.Focus();
                }
                else
                {
                    string str2 = PubDBCommFuns.sp_Chk_WriteChkDtl(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", this.txt_cWHId.Text.Trim(), this.txt_cPosId.Text.Trim(), this.txt_nPalletId.Text.Trim(), this.txt_cBoxId.Text.Trim(), this.txt_cMNo.Text.Trim(), pDiff, pBad, this.txt_cUnit.Text.Trim(), this.txt_cBNoIn.Text.Trim(), int.Parse(this.txt_nItemIn.Text.Trim()), this._CheckNo.Trim(), out sErr);
                    if (((str2.Trim() != "0") && (str2.Trim() != "B")) && (sErr.Trim() != ""))
                    {
                        MessageBox.Show(sErr);
                    }
                    else
                    {
                        MessageBox.Show("登记成功！");
                        this._IsOK = true;
                        base.Close();
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

        public void doSelIOStoreMatBillData(int nBClass, string sBNo, int nItem, string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther, string sRemark, string sABC, double fQty, double fWeight, string sUnit, string sCSId, string sSupplier, string sBatchNo, string sBNoIn, int nItemIn, out bool bDoOK)
        {
            bDoOK = false;
            if ((this.txt_cMNo.Text.Trim() != "") && (this.txt_cMNo.Text.Trim() != sMNo.Trim()))
            {
                MessageBox.Show("物料编码不一致！");
            }
            else if (nBClass == 1)
            {
                this.txt_cBatchNo.Text = sBatchNo;
                this.txt_cBNoIn.Text = sBNoIn;
                this.txt_cSpec.Text = sSpec;
                this.txt_cUnit.Text = sUnit;
                this.txt_RQty.Text = fQty.ToString();
                this.txt_nItemIn.Text = nItemIn.ToString();
                this.txt_RQty.SelectAll();
                this.txt_RQty.Focus();
            }
        }

        private void doSelMaterial(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther, string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1, string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId, string sSupplier, int _nMatClass, bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                this.txt_cMNo.Text = sMNo;
                this.txt_cUnit.Text = sUnit;
                this.txt_cSpec.Text = sSpec;
            }
        }

        private void frmChkDtlWrite_Load(object sender, EventArgs e)
        {
            this.txt_cBatchNo.Text = this._BatchNo.Trim();
            this.txt_cBNoIn.Text = this._BNoIn.Trim();
            this.txt_cBoxId.Text = this._BoxId.Trim();
            this.txt_cMNo.Text = this._MNo.Trim();
            this.txt_nPalletId.Text = this._PalletId.Trim();
            this.txt_cPosId.Text = this._PosId.Trim();
            this.txt_cSpec.Text = this._Spec.Trim();
            this.txt_cUnit.Text = this._Unit.Trim();
            this.txt_cWHId.Text = this._WHId.Trim();
            this.txt_nItemIn.Text = this._ItemIn.ToString();
            this.txt_Qty.Text = this._Qty.ToString();
            this.txt_RQty.Text = this.txt_Qty.Text;
            this.txt_fDiff.Text = "0";
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.txt_Qty = new TextBox();
            this.txt_RQty = new TextBox();
            this.btnOK = new Button();
            this.btnClose = new Button();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txt_fDiff = new TextBox();
            this.toolTip1 = new ToolTip(this.components);
            this.label9 = new Label();
            this.label1 = new Label();
            this.label11 = new Label();
            this.txt_fBad = new TextBox();
            this.label15 = new Label();
            this.txt_RTotal = new TextBox();
            this.label17 = new Label();
            this.label18 = new Label();
            this.btn_AddMat = new Button();
            this.btn_SelBNoIn = new Button();
            this.groupBox1 = new GroupBox();
            this.label10 = new Label();
            this.label13 = new Label();
            this.txt_cBatchNo = new TextBox();
            this.txt_cSpec = new TextBox();
            this.label8 = new Label();
            this.txt_nItemIn = new TextBox();
            this.txt_cBNoIn = new TextBox();
            this.txt_cPosId = new TextBox();
            this.label7 = new Label();
            this.label12 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.txt_cUnit = new TextBox();
            this.txt_cWHId = new TextBox();
            this.txt_nPalletId = new TextBox();
            this.txt_cMNo = new TextBox();
            this.txt_cBoxId = new TextBox();
            this.label14 = new Label();
            this.label16 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.txt_Qty.Enabled = false;
            this.txt_Qty.Location = new Point(0x74, 0x9a);
            this.txt_Qty.Name = "txt_Qty";
            this.txt_Qty.ReadOnly = true;
            this.txt_Qty.Size = new Size(0x117, 0x15);
            this.txt_Qty.TabIndex = 2;
            this.txt_RQty.Enabled = false;
            this.txt_RQty.Location = new Point(0x147, 0xd5);
            this.txt_RQty.Name = "txt_RQty";
            this.txt_RQty.ReadOnly = true;
            this.txt_RQty.Size = new Size(0x44, 0x15);
            this.txt_RQty.TabIndex = 2;
            this.txt_RQty.Text = "0";
            this.btnOK.Location = new Point(0x80, 0x124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnClose.Location = new Point(0xf6, 0x124);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x35, 0x9d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0x26;
            this.label2.Text = "帐面数";
            this.toolTip1.SetToolTip(this.label2, "当前的库存数");
            this.label3.AutoSize = true;
            this.label3.ForeColor = SystemColors.ControlText;
            this.label3.Location = new Point(0xe8, 0xd9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 0x27;
            this.label3.Text = "实际有效库存数";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x35, 0xd9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 0x29;
            this.label4.Text = "损溢数";
            this.txt_fDiff.Location = new Point(0x74, 0xd5);
            this.txt_fDiff.Name = "txt_fDiff";
            this.txt_fDiff.ReadOnly = true;
            this.txt_fDiff.Size = new Size(100, 0x15);
            this.txt_fDiff.TabIndex = 1;
            this.txt_fDiff.Text = "0";
            this.toolTip1.SetToolTip(this.txt_fDiff, "实盘数-账面数");
            this.txt_fDiff.TextChanged += new EventHandler(this.txt_fDiff_TextChanged);
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label9.ForeColor = SystemColors.ControlText;
            this.label9.Location = new Point(8, 0x5c);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 12);
            this.label9.TabIndex = 0x2f;
            this.label9.Text = "入库单号";
            this.toolTip1.SetToolTip(this.label9, "点击，对新增物料选择入库时的单号及明细序号");
            this.label9.Click += new EventHandler(this.label9_Click);
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = SystemColors.ControlText;
            this.label1.Location = new Point(0xf5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x2c;
            this.label1.Text = "货位号码";
            this.toolTip1.SetToolTip(this.label1, "点击，可对新添物料选择货位号");
            this.label1.Click += new EventHandler(this.label1_Click);
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label11.ForeColor = SystemColors.ControlText;
            this.label11.Location = new Point(8, 0x41);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x25;
            this.label11.Text = "物料号码";
            this.toolTip1.SetToolTip(this.label11, "点击，可增加现库存中不存在物料");
            this.txt_fBad.Location = new Point(0x127, 0xb9);
            this.txt_fBad.Name = "txt_fBad";
            this.txt_fBad.Size = new Size(100, 0x15);
            this.txt_fBad.TabIndex = 1;
            this.txt_fBad.Text = "0";
            this.toolTip1.SetToolTip(this.txt_fBad, "实盘数-账面数");
            this.txt_fBad.TextChanged += new EventHandler(this.txt_RTotal_TextChanged);
            this.label15.AutoSize = true;
            this.label15.ForeColor = SystemColors.ActiveCaption;
            this.label15.Location = new Point(0x2c, 0xef);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x83, 12);
            this.label15.TabIndex = 0x2c;
            this.label15.Text = "1.实盘数 包括不良品数";
            this.toolTip1.SetToolTip(this.label15, "当前的库存数");
            this.txt_RTotal.Location = new Point(0x74, 0xb9);
            this.txt_RTotal.Name = "txt_RTotal";
            this.txt_RTotal.Size = new Size(100, 0x15);
            this.txt_RTotal.TabIndex = 0;
            this.txt_RTotal.Text = "0";
            this.toolTip1.SetToolTip(this.txt_RTotal, "实盘数-账面数");
            this.txt_RTotal.TextChanged += new EventHandler(this.txt_RTotal_TextChanged);
            this.label17.AutoSize = true;
            this.label17.ForeColor = SystemColors.ActiveCaption;
            this.label17.Location = new Point(0x2c, 0xfd);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x155, 12);
            this.label17.TabIndex = 0x2f;
            this.label17.Text = "2.损溢数＝实盘数-帐面数-不良品数(不良品数包含在损溢数里)";
            this.toolTip1.SetToolTip(this.label17, "当前的库存数");
            this.label18.AutoSize = true;
            this.label18.ForeColor = SystemColors.ActiveCaption;
            this.label18.Location = new Point(0x2c, 0x10b);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x107, 12);
            this.label18.TabIndex = 0x30;
            this.label18.Text = "3.实际有效数＝实盘数-不良品数=帐面数+损溢数";
            this.toolTip1.SetToolTip(this.label18, "当前的库存数");
            this.btn_AddMat.Location = new Point(0xc4, 60);
            this.btn_AddMat.Name = "btn_AddMat";
            this.btn_AddMat.Size = new Size(0x1b, 0x17);
            this.btn_AddMat.TabIndex = 0x35;
            this.btn_AddMat.Text = "…";
            this.toolTip1.SetToolTip(this.btn_AddMat, "点击，选择新增加的物料");
            this.btn_AddMat.UseVisualStyleBackColor = true;
            this.btn_AddMat.Visible = false;
            this.btn_AddMat.Click += new EventHandler(this.btn_AddMat_Click);
            this.btn_SelBNoIn.Location = new Point(0xc4, 0x56);
            this.btn_SelBNoIn.Name = "btn_SelBNoIn";
            this.btn_SelBNoIn.Size = new Size(0x1b, 0x17);
            this.btn_SelBNoIn.TabIndex = 0x36;
            this.btn_SelBNoIn.Text = "…";
            this.toolTip1.SetToolTip(this.btn_SelBNoIn, "点击，选择库存入库单号");
            this.btn_SelBNoIn.UseVisualStyleBackColor = true;
            this.btn_SelBNoIn.Visible = false;
            this.btn_SelBNoIn.Click += new EventHandler(this.btn_SelBNoIn_Click);
            this.groupBox1.Controls.Add(this.btn_SelBNoIn);
            this.groupBox1.Controls.Add(this.btn_AddMat);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_cBatchNo);
            this.groupBox1.Controls.Add(this.txt_cSpec);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_nItemIn);
            this.groupBox1.Controls.Add(this.txt_cBNoIn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_cPosId);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_cUnit);
            this.groupBox1.Controls.Add(this.txt_cWHId);
            this.groupBox1.Controls.Add(this.txt_nPalletId);
            this.groupBox1.Controls.Add(this.txt_cMNo);
            this.groupBox1.Controls.Add(this.txt_cBoxId);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1c1, 0x95);
            this.groupBox1.TabIndex = 0x2a;
            this.groupBox1.TabStop = false;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0xf5, 120);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 0x34;
            this.label10.Text = "物料规格";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(8, 120);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x33;
            this.label13.Text = "物料批号";
            this.txt_cBatchNo.Enabled = false;
            this.txt_cBatchNo.Location = new Point(0x49, 0x74);
            this.txt_cBatchNo.Name = "txt_cBatchNo";
            this.txt_cBatchNo.ReadOnly = true;
            this.txt_cBatchNo.Size = new Size(0x7a, 0x15);
            this.txt_cBatchNo.TabIndex = 50;
            this.txt_cBatchNo.Tag = "0";
            this.txt_cSpec.Location = new Point(0x12f, 0x74);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.ReadOnly = true;
            this.txt_cSpec.Size = new Size(0x89, 0x15);
            this.txt_cSpec.TabIndex = 0x31;
            this.txt_cSpec.Tag = "0";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xf5, 0x5c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x30;
            this.label8.Text = "明细项次";
            this.txt_nItemIn.Enabled = false;
            this.txt_nItemIn.Location = new Point(0x12f, 0x58);
            this.txt_nItemIn.Name = "txt_nItemIn";
            this.txt_nItemIn.ReadOnly = true;
            this.txt_nItemIn.Size = new Size(0x89, 0x15);
            this.txt_nItemIn.TabIndex = 0x2e;
            this.txt_nItemIn.Tag = "0";
            this.txt_cBNoIn.Location = new Point(0x49, 0x58);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.ReadOnly = true;
            this.txt_cBNoIn.Size = new Size(0x7a, 0x15);
            this.txt_cBNoIn.TabIndex = 0x2d;
            this.txt_cBNoIn.Tag = "0";
            this.txt_cPosId.Location = new Point(0x12f, 11);
            this.txt_cPosId.Name = "txt_cPosId";
            this.txt_cPosId.ReadOnly = true;
            this.txt_cPosId.Size = new Size(0x89, 0x15);
            this.txt_cPosId.TabIndex = 0x2b;
            this.txt_cPosId.Tag = "0";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 15);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x2a;
            this.label7.Text = "仓库号码";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xf5, 0x41);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x35, 12);
            this.label12.TabIndex = 0x27;
            this.label12.Text = "物料单位";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xf5, 40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "周转箱号";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 40);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x26;
            this.label5.Text = "托盘号码";
            this.txt_cUnit.Enabled = false;
            this.txt_cUnit.Location = new Point(0x12f, 0x3d);
            this.txt_cUnit.Name = "txt_cUnit";
            this.txt_cUnit.ReadOnly = true;
            this.txt_cUnit.Size = new Size(0x89, 0x15);
            this.txt_cUnit.TabIndex = 0x22;
            this.txt_cUnit.Tag = "0";
            this.txt_cWHId.Enabled = false;
            this.txt_cWHId.Location = new Point(0x49, 11);
            this.txt_cWHId.Name = "txt_cWHId";
            this.txt_cWHId.ReadOnly = true;
            this.txt_cWHId.Size = new Size(0x7a, 0x15);
            this.txt_cWHId.TabIndex = 0x20;
            this.txt_cWHId.Tag = "0";
            this.txt_nPalletId.Enabled = false;
            this.txt_nPalletId.Location = new Point(0x49, 0x24);
            this.txt_nPalletId.Name = "txt_nPalletId";
            this.txt_nPalletId.ReadOnly = true;
            this.txt_nPalletId.Size = new Size(0x7a, 0x15);
            this.txt_nPalletId.TabIndex = 0x1f;
            this.txt_nPalletId.Tag = "0";
            this.txt_cMNo.Location = new Point(0x49, 0x3d);
            this.txt_cMNo.Name = "txt_cMNo";
            this.txt_cMNo.ReadOnly = true;
            this.txt_cMNo.Size = new Size(0x7a, 0x15);
            this.txt_cMNo.TabIndex = 0x1d;
            this.txt_cMNo.Tag = "0";
            this.txt_cBoxId.Location = new Point(0x12f, 0x24);
            this.txt_cBoxId.Name = "txt_cBoxId";
            this.txt_cBoxId.ReadOnly = true;
            this.txt_cBoxId.Size = new Size(0x89, 0x15);
            this.txt_cBoxId.TabIndex = 0x1b;
            this.txt_cBoxId.Tag = "0";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0xe8, 0xbd);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x35, 12);
            this.label14.TabIndex = 0x2b;
            this.label14.Text = "不良品数";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0x35, 0xbd);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x29, 12);
            this.label16.TabIndex = 0x2e;
            this.label16.Text = "实盘数";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x1c1, 330);
            base.Controls.Add(this.label18);
            base.Controls.Add(this.label17);
            base.Controls.Add(this.txt_RTotal);
            base.Controls.Add(this.label16);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.txt_Qty);
            base.Controls.Add(this.txt_RQty);
            base.Controls.Add(this.txt_fBad);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txt_fDiff);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnOK);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "frmChkDtlWrite";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "盘点登记";
            base.Load += new EventHandler(this.frmChkDtlWrite_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FrmSelectCell cell = new FrmSelectCell {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            cell.cmb_cWHId.Tag = this._WHId.Trim();
            cell.IsMultiSelect = false;
            cell.ShowDialog();
            if (cell.BIsResult)
            {
                this.txt_cPosId.Text = cell.SelResult;
                this.txt_nPalletId.Text = cell.SelPalletId;
            }
            cell.Dispose();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if ((this._WHId.Trim() == "") || (this.txt_cMNo.Text.Trim() == ""))
            {
                MessageBox.Show("对不起，必须先选择物料，才能选择该物料的库存入库单数据！");
            }
            else
            {
                frmSelBillInDtl dtl = new frmSelBillInDtl {
                    AppInformation = base.AppInformation,
                    UserInformation = base.UserInformation
                };
                dtl.lbl_cWHId.Text = this._WHId.Trim();
                dtl.lbl_cMNo.Text = this.txt_cMNo.Text.Trim();
                dtl.ShowDialog();
                if (dtl.IsSelected)
                {
                    this.txt_cBatchNo.Text = dtl.SelBatchNo.Trim();
                    this.txt_cBNoIn.Text = dtl.SelBNo.Trim();
                    this.txt_nItemIn.Text = dtl.SelItem.Trim();
                    this.txt_cUnit.Text = dtl.SelUnit.Trim();
                }
                dtl.Dispose();
            }
        }

        private void txt_fDiff_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_RTotal_TextChanged(object sender, EventArgs e)
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            if ((this.txt_RTotal.Text.Trim() != "") && FrmSTable.IsNumberic(this.txt_RTotal.Text.Trim()))
            {
                num2 = double.Parse(this.txt_RTotal.Text.Trim());
            }
            else if (!(!(this.txt_RTotal.Text.Trim() != "") || FrmSTable.IsNumberic(this.txt_RTotal.Text.Trim())))
            {
                MessageBox.Show("请录入合法数值！");
                this.txt_RTotal.SelectAll();
                this.txt_RTotal.Focus();
                return;
            }
            if ((this.txt_fBad.Text.Trim() != "") && FrmSTable.IsNumberic(this.txt_fBad.Text.Trim()))
            {
                num3 = double.Parse(this.txt_fBad.Text.Trim());
            }
            else if (!(!(this.txt_fBad.Text.Trim() != "") || FrmSTable.IsNumberic(this.txt_fBad.Text.Trim())))
            {
                MessageBox.Show("请录入合法数值！");
                this.txt_fBad.SelectAll();
                this.txt_fBad.Focus();
                return;
            }
            if (this.txt_Qty.Text.Trim() == "")
            {
                MessageBox.Show("对不起，帐面数不能为空！");
            }
            else
            {
                num = double.Parse(this.txt_Qty.Text.Trim());
                num4 = (num2 - num) - num3;
                num5 = num2 - num3;
                this.txt_fDiff.Text = num4.ToString();
                this.txt_RQty.Text = num5.ToString();
            }
        }

        public double BadQty
        {
            get
            {
                if ((this.txt_fBad.Text.Trim() != "") && FrmSTable.IsNumberic(this.txt_fBad.Text.Trim()))
                {
                    this._BadQty = double.Parse(this.txt_fBad.Text.Trim());
                }
                return this._BadQty;
            }
            set
            {
                this._BadQty = value;
                this.txt_fBad.Text = this._BadQty.ToString();
            }
        }

        public string BatchNo
        {
            set
            {
                this._BatchNo = value.Trim();
            }
        }

        public string BNoIn
        {
            set
            {
                this._BNoIn = value.Trim();
            }
        }

        public string BoxId
        {
            set
            {
                this._BoxId = value.Trim();
            }
        }

        public string CheckNo
        {
            get
            {
                return this._CheckNo.Trim();
            }
            set
            {
                this._CheckNo = value.Trim();
            }
        }

        public double Diff
        {
            get
            {
                if ((this.txt_fDiff.Text.Trim() != "") && FrmSTable.IsNumberic(this.txt_fDiff.Text.Trim()))
                {
                    this._Diff = double.Parse(this.txt_fDiff.Text.Trim());
                }
                return this._Diff;
            }
            set
            {
                this._Diff = value;
                this.txt_fDiff.Text = this._Diff.ToString();
            }
        }

        public bool IsNewAddMat
        {
            get
            {
                return this._IsNewAddMat;
            }
            set
            {
                this._IsNewAddMat = value;
                if (this._IsNewAddMat)
                {
                    this.btn_AddMat.Visible = true;
                    this.btn_SelBNoIn.Visible = true;
                    this._MNo = "";
                    this._MName = "";
                    this._Spec = "";
                    this._Unit = "";
                    this._BatchNo = "";
                    this._BNoIn = "库存初始化";
                    this._ItemIn = 0;
                    this._Qty = 0.0;
                    this._BadQty = 0.0;
                    this._Diff = 0.0;
                    this._BoxId = "";
                    this.txt_cBatchNo.Enabled = true;
                    this.txt_cBNoIn.Enabled = true;
                    this.txt_cBoxId.Enabled = true;
                    this.txt_nItemIn.Enabled = true;
                    this.frmChkDtlWrite_Load(null, null);
                }
            }
        }

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

        public int ItemIn
        {
            set
            {
                this._ItemIn = value;
            }
        }

        public string MName
        {
            set
            {
                this._MName = value.Trim();
            }
        }

        public string MNo
        {
            set
            {
                this._MNo = value.Trim();
            }
        }

        public string PalletId
        {
            set
            {
                this._PalletId = value.Trim();
            }
        }

        public string PosId
        {
            set
            {
                this._PosId = value.Trim();
            }
        }

        public int QCStatus
        {
            set
            {
                this._QCStatus = value;
            }
        }

        public double Qty
        {
            set
            {
                this._Qty = value;
            }
        }

        public string Spec
        {
            set
            {
                this._Spec = value.Trim();
            }
        }

        public string Unit
        {
            set
            {
                this._Unit = value.Trim();
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
                this.txt_cWHId.Text = this._WHId;
            }
        }
    }
}

