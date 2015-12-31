namespace WareStoreMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using UI;

    public class frmDtlAjust : FrmSTable
    {
        private string _WHId = "";
        private bool bIsNew = false;
        private bool bIsResultOK = false;
        private Button btnClose;
        private Button btnOK;
        private IContainer components = null;
        private DoEditItemInfo doItem = null;
        private DataRowView drvItem = null;
        private GroupBox grdEdit;
        public Label label10;
        public Label label12;
        public Label label13;
        public Label label4;
        public Label label5;
        public Label label6;
        public Label label7;
        public Label label8;
        public Label lbl_BillIn;
        public Label lbl_MNo;
        public Label lbl_PosId;
        private ToolTip toolTip1;
        private TextBox txt_cBatchNo;
        private TextBox txt_cBNoIn;
        private TextBox txt_cBoxId;
        private TextBox txt_cMNo;
        private TextBox txt_cPosId;
        private TextBox txt_cSpec;
        private TextBox txt_cUnit;
        private TextBox txt_cWHId;
        private TextBox txt_fQty;
        private TextBox txt_nItemIn;
        private TextBox txt_nPalletId;

        public frmDtlAjust()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string pAjustNo = "";
            if (this.txt_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料编码不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_cMNo.Focus();
            }
            else if (this.txt_fQty.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料数量不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_fQty.Focus();
            }
            else if (!FrmSTable.IsNumberic(this.txt_fQty.Text.Trim()))
            {
                MessageBox.Show("对不起，物料数量为非法数值！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_fQty.SelectAll();
                this.txt_fQty.Focus();
            }
            else if (this.txt_cUnit.Text.Trim() == "")
            {
                MessageBox.Show("对不起，单位不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_cUnit.SelectAll();
                this.txt_cUnit.Focus();
            }
            else
            {
                pAjustNo = this.drvItem["cBNo"].ToString();
                this.UIToDataRowView(this.drvItem, this.grdEdit);
                string sErr = "";
                string str3 = DBFuns.sp_Chk_WriteAjustDtl(base.AppInformation.SvrSocket, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", pAjustNo, this.txt_cWHId.Text.Trim(), this.txt_cPosId.Text.Trim(), this.txt_nPalletId.Text.Trim(), this.txt_cBoxId.Text.Trim(), this.txt_cMNo.Text.Trim(), double.Parse(this.txt_fQty.Text.Trim()), this.txt_cBNoIn.Text.Trim(), int.Parse(this.txt_nItemIn.Text.Trim()), "", out sErr);
                if (((str3.Trim() != "") && (str3.Trim() != "0")) && (sErr.Trim() != ""))
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    base.Close();
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
            if (nBClass == 1)
            {
                this.txt_cBatchNo.Text = sBatchNo;
                this.txt_cBNoIn.Text = sBNoIn;
                this.txt_cSpec.Text = sSpec;
                this.txt_cUnit.Text = sUnit;
                this.txt_fQty.Text = fQty.ToString();
                this.txt_nItemIn.Text = nItemIn.ToString();
            }
        }

        private void frmDtlAjust_Load(object sender, EventArgs e)
        {
            if (this.drvItem != null)
            {
                this.DataRowViewToUI(this.drvItem, this.grdEdit);
            }
            Font font = null;
            if (this.bIsNew)
            {
                font = new Font("宋体", 9f, FontStyle.Underline, GraphicsUnit.Point, 0x86);
                this.lbl_BillIn.ForeColor = Color.Blue;
                this.lbl_BillIn.Font = font;
                this.lbl_BillIn.Enabled = true;
                this.lbl_MNo.ForeColor = Color.Blue;
                this.lbl_MNo.Font = font;
                this.lbl_MNo.Enabled = true;
                this.lbl_PosId.ForeColor = Color.Blue;
                this.lbl_PosId.Font = font;
                this.lbl_PosId.Enabled = true;
            }
            else
            {
                font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
                this.lbl_BillIn.ForeColor = Color.Black;
                this.lbl_BillIn.Font = font;
                this.lbl_BillIn.Enabled = false;
                this.lbl_MNo.ForeColor = Color.Black;
                this.lbl_MNo.Font = font;
                this.lbl_MNo.Enabled = false;
                this.lbl_PosId.ForeColor = Color.Black;
                this.lbl_PosId.Font = font;
                this.lbl_PosId.Enabled = false;
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.grdEdit = new GroupBox();
            this.label10 = new Label();
            this.txt_fQty = new TextBox();
            this.label13 = new Label();
            this.txt_cBatchNo = new TextBox();
            this.label4 = new Label();
            this.txt_cSpec = new TextBox();
            this.label8 = new Label();
            this.lbl_BillIn = new Label();
            this.txt_nItemIn = new TextBox();
            this.txt_cBNoIn = new TextBox();
            this.lbl_PosId = new Label();
            this.txt_cPosId = new TextBox();
            this.label7 = new Label();
            this.label12 = new Label();
            this.label6 = new Label();
            this.lbl_MNo = new Label();
            this.label5 = new Label();
            this.txt_cUnit = new TextBox();
            this.txt_cWHId = new TextBox();
            this.txt_nPalletId = new TextBox();
            this.txt_cMNo = new TextBox();
            this.txt_cBoxId = new TextBox();
            this.btnClose = new Button();
            this.btnOK = new Button();
            this.toolTip1 = new ToolTip(this.components);
            this.grdEdit.SuspendLayout();
            base.SuspendLayout();
            this.grdEdit.Controls.Add(this.label10);
            this.grdEdit.Controls.Add(this.txt_fQty);
            this.grdEdit.Controls.Add(this.label13);
            this.grdEdit.Controls.Add(this.txt_cBatchNo);
            this.grdEdit.Controls.Add(this.label4);
            this.grdEdit.Controls.Add(this.txt_cSpec);
            this.grdEdit.Controls.Add(this.label8);
            this.grdEdit.Controls.Add(this.lbl_BillIn);
            this.grdEdit.Controls.Add(this.txt_nItemIn);
            this.grdEdit.Controls.Add(this.txt_cBNoIn);
            this.grdEdit.Controls.Add(this.lbl_PosId);
            this.grdEdit.Controls.Add(this.txt_cPosId);
            this.grdEdit.Controls.Add(this.label7);
            this.grdEdit.Controls.Add(this.label12);
            this.grdEdit.Controls.Add(this.label6);
            this.grdEdit.Controls.Add(this.lbl_MNo);
            this.grdEdit.Controls.Add(this.label5);
            this.grdEdit.Controls.Add(this.txt_cUnit);
            this.grdEdit.Controls.Add(this.txt_cWHId);
            this.grdEdit.Controls.Add(this.txt_nPalletId);
            this.grdEdit.Controls.Add(this.txt_cMNo);
            this.grdEdit.Controls.Add(this.txt_cBoxId);
            this.grdEdit.Dock = DockStyle.Top;
            this.grdEdit.Location = new Point(0, 0);
            this.grdEdit.Name = "grdEdit";
            this.grdEdit.Size = new Size(0x1b1, 0xa3);
            this.grdEdit.TabIndex = 0x33;
            this.grdEdit.TabStop = false;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0xe5, 0x70);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 0x34;
            this.label10.Text = "物料规格";
            this.txt_fQty.Location = new Point(0x6d, 0x87);
            this.txt_fQty.Name = "txt_fQty";
            this.txt_fQty.Size = new Size(100, 0x15);
            this.txt_fQty.TabIndex = 0x2b;
            this.txt_fQty.Tag = "0";
            this.txt_fQty.Text = "0";
            this.toolTip1.SetToolTip(this.txt_fQty, "实盘数-账面数");
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x2c, 0x70);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 12);
            this.label13.TabIndex = 0x33;
            this.label13.Text = "物料批号";
            this.txt_cBatchNo.Enabled = false;
            this.txt_cBatchNo.Location = new Point(0x6d, 0x70);
            this.txt_cBatchNo.Name = "txt_cBatchNo";
            this.txt_cBatchNo.ReadOnly = true;
            this.txt_cBatchNo.Size = new Size(100, 0x15);
            this.txt_cBatchNo.TabIndex = 50;
            this.txt_cBatchNo.Tag = "0";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x2c, 0x87);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 50;
            this.label4.Text = "数量";
            this.txt_cSpec.Location = new Point(0x120, 0x70);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.ReadOnly = true;
            this.txt_cSpec.Size = new Size(100, 0x15);
            this.txt_cSpec.TabIndex = 0x31;
            this.txt_cSpec.Tag = "0";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xe5, 0x57);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x30;
            this.label8.Text = "明细项次";
            this.lbl_BillIn.AutoSize = true;
            this.lbl_BillIn.Enabled = false;
            this.lbl_BillIn.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lbl_BillIn.ForeColor = SystemColors.ControlText;
            this.lbl_BillIn.Location = new Point(0x2c, 0x57);
            this.lbl_BillIn.Name = "lbl_BillIn";
            this.lbl_BillIn.Size = new Size(0x35, 12);
            this.lbl_BillIn.TabIndex = 0x2f;
            this.lbl_BillIn.Text = "入库单号";
            this.toolTip1.SetToolTip(this.lbl_BillIn, "点击，对新增物料选择入库时的单号及明细序号");
            this.lbl_BillIn.Click += new EventHandler(this.lblBillIn_Click);
            this.txt_nItemIn.Enabled = false;
            this.txt_nItemIn.Location = new Point(0x120, 0x57);
            this.txt_nItemIn.Name = "txt_nItemIn";
            this.txt_nItemIn.ReadOnly = true;
            this.txt_nItemIn.Size = new Size(100, 0x15);
            this.txt_nItemIn.TabIndex = 0x2e;
            this.txt_nItemIn.Tag = "0";
            this.txt_cBNoIn.Location = new Point(0x6d, 0x57);
            this.txt_cBNoIn.Name = "txt_cBNoIn";
            this.txt_cBNoIn.ReadOnly = true;
            this.txt_cBNoIn.Size = new Size(100, 0x15);
            this.txt_cBNoIn.TabIndex = 0x2d;
            this.txt_cBNoIn.Tag = "0";
            this.lbl_PosId.AutoSize = true;
            this.lbl_PosId.Enabled = false;
            this.lbl_PosId.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lbl_PosId.ForeColor = SystemColors.ControlText;
            this.lbl_PosId.Location = new Point(0xe5, 15);
            this.lbl_PosId.Name = "lbl_PosId";
            this.lbl_PosId.Size = new Size(0x35, 12);
            this.lbl_PosId.TabIndex = 0x2c;
            this.lbl_PosId.Text = "货位号码";
            this.toolTip1.SetToolTip(this.lbl_PosId, "点击，可对新添物料选择货位号");
            this.lbl_PosId.Click += new EventHandler(this.lbl_PosId_Click);
            this.txt_cPosId.Location = new Point(0x120, 15);
            this.txt_cPosId.Name = "txt_cPosId";
            this.txt_cPosId.ReadOnly = true;
            this.txt_cPosId.Size = new Size(100, 0x15);
            this.txt_cPosId.TabIndex = 0x2b;
            this.txt_cPosId.Tag = "0";
            this.label7.AutoSize = true;
            this.label7.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label7.Location = new Point(0x2c, 15);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x2a;
            this.label7.Text = "仓库号码";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xe5, 0x3e);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x35, 12);
            this.label12.TabIndex = 0x27;
            this.label12.Text = "物料单位";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xe5, 40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "周转箱号";
            this.lbl_MNo.AutoSize = true;
            this.lbl_MNo.Enabled = false;
            this.lbl_MNo.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lbl_MNo.ForeColor = SystemColors.ControlText;
            this.lbl_MNo.Location = new Point(0x2c, 0x3e);
            this.lbl_MNo.Name = "lbl_MNo";
            this.lbl_MNo.Size = new Size(0x35, 12);
            this.lbl_MNo.TabIndex = 0x25;
            this.lbl_MNo.Text = "物料号码";
            this.toolTip1.SetToolTip(this.lbl_MNo, "点击，可增加现库存中不存在物料");
            this.lbl_MNo.Click += new EventHandler(this.lblMNo_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x2c, 40);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x26;
            this.label5.Text = "托盘号码";
            this.txt_cUnit.Enabled = false;
            this.txt_cUnit.Location = new Point(0x120, 0x3e);
            this.txt_cUnit.Name = "txt_cUnit";
            this.txt_cUnit.ReadOnly = true;
            this.txt_cUnit.Size = new Size(100, 0x15);
            this.txt_cUnit.TabIndex = 0x22;
            this.txt_cUnit.Tag = "0";
            this.txt_cWHId.Enabled = false;
            this.txt_cWHId.Location = new Point(0x6d, 15);
            this.txt_cWHId.Name = "txt_cWHId";
            this.txt_cWHId.ReadOnly = true;
            this.txt_cWHId.Size = new Size(100, 0x15);
            this.txt_cWHId.TabIndex = 0x20;
            this.txt_cWHId.Tag = "0";
            this.txt_nPalletId.Enabled = false;
            this.txt_nPalletId.Location = new Point(0x6d, 0x26);
            this.txt_nPalletId.Name = "txt_nPalletId";
            this.txt_nPalletId.ReadOnly = true;
            this.txt_nPalletId.Size = new Size(100, 0x15);
            this.txt_nPalletId.TabIndex = 0x1f;
            this.txt_nPalletId.Tag = "0";
            this.txt_cMNo.Location = new Point(0x6d, 0x3e);
            this.txt_cMNo.Name = "txt_cMNo";
            this.txt_cMNo.ReadOnly = true;
            this.txt_cMNo.Size = new Size(100, 0x15);
            this.txt_cMNo.TabIndex = 0x1d;
            this.txt_cMNo.Tag = "0";
            this.txt_cBoxId.Location = new Point(0x120, 40);
            this.txt_cBoxId.Name = "txt_cBoxId";
            this.txt_cBoxId.Size = new Size(100, 0x15);
            this.txt_cBoxId.TabIndex = 0x1b;
            this.txt_cBoxId.Tag = "0";
            this.btnClose.Location = new Point(0xee, 0xaf);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0x2e;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnOK.Location = new Point(120, 0xaf);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x2c;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x1b1, 0xd0);
            base.Controls.Add(this.grdEdit);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnOK);
            base.KeyPreview = true;
            base.MinimizeBox = false;
            base.Name = "frmDtlAjust";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "调整单明细";
            base.Load += new EventHandler(this.frmDtlAjust_Load);
            this.grdEdit.ResumeLayout(false);
            this.grdEdit.PerformLayout();
            base.ResumeLayout(false);
        }

        private void lbl_PosId_Click(object sender, EventArgs e)
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

        private void lblBillIn_Click(object sender, EventArgs e)
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
                    this.txt_fQty.SelectAll();
                    this.txt_fQty.Focus();
                }
                dtl.Dispose();
            }
        }

        private void lblMNo_Click(object sender, EventArgs e)
        {
            frmSelIOBillMat mat = new frmSelIOBillMat {
                BClass = 1,
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation,
                DoSelIOStoreMatBillData = new DoSelIOStoreMatBillDataEvent(this.doSelIOStoreMatBillData)
            };
            mat.ShowDialog();
            mat.Dispose();
        }

        public bool BIsNew
        {
            get
            {
                return this.bIsNew;
            }
            set
            {
                this.bIsNew = value;
                Font font = null;
                if (this.bIsNew)
                {
                    font = new Font("宋体", 9f, FontStyle.Underline, GraphicsUnit.Point, 0x86);
                    this.lbl_BillIn.ForeColor = Color.Blue;
                    this.lbl_BillIn.Font = font;
                    this.lbl_BillIn.Enabled = true;
                    this.lbl_MNo.ForeColor = Color.Blue;
                    this.lbl_MNo.Font = font;
                    this.lbl_MNo.Enabled = true;
                    this.lbl_PosId.ForeColor = Color.Blue;
                    this.lbl_PosId.Font = font;
                    this.lbl_PosId.Enabled = true;
                }
                else
                {
                    font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
                    this.lbl_BillIn.ForeColor = Color.Black;
                    this.lbl_BillIn.Font = font;
                    this.lbl_BillIn.Enabled = false;
                    this.lbl_MNo.ForeColor = Color.Black;
                    this.lbl_MNo.Font = font;
                    this.lbl_MNo.Enabled = false;
                    this.lbl_PosId.ForeColor = Color.Black;
                    this.lbl_PosId.Font = font;
                    this.lbl_PosId.Enabled = false;
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

        public DoEditItemInfo DoItem
        {
            get
            {
                return this.doItem;
            }
            set
            {
                this.doItem = value;
            }
        }

        public DataRowView DrvItem
        {
            get
            {
                return this.drvItem;
            }
            set
            {
                this.drvItem = value;
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
            }
        }

        public delegate bool DoEditItemInfo(DataRowView drvX);
    }
}

