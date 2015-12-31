namespace WareStoreMS
{
    using SunEast;
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using Zqm.DBCommInfo;

    public class FrmItemEditor : FrmSTable
    {
        private string _BType = "";
        private string _MatClass = "";
        private int _QCStatus = 0;
        private string _WHId = "";
        private BindingSource bdsItemList;
        private bool bIsNew = true;
        private bool bIsResultOK = false;
        private bool bIsShowGrid = false;
        private Button btnCancel;
        private Button btnOK;
        private Button btnSel;
        private Button button1;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn colcName;
        private DataGridViewTextBoxColumn colcSpec;
        private DataGridViewTextBoxColumn colcUnit;
        private IContainer components = null;
        private DoEditItemInfo doItem;
        private DataRowView drvItem;
        private DataSet dsItemList = new DataSet();
        private double fUseQty = 0.0;
        public DataGridView grdDtl;
        private bool isOutBill = false;
        private Label label1;
        private Label label12;
        private Label label19;
        private Label label2;
        private Label label22;
        private Label label25;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lbl_Out;
        private int nQCState = 1;
        private Panel panel1;
        private Panel pnlDtlEdit;
        private string strTbNameMain = "TPC_Material";
        private TextBox txt_Dtl_cMatOther;
        private TextBox txt_Dtl_cMatQCLevel;
        private TextBox txt_Dtl_cMatStyle;
        private TextBox txt_Dtl_cMName;
        private TextBox txt_Dtl_cMNo;
        private TextBox txt_Dtl_cSpec;
        private TextBox txt_Dtl_cSupplier;
        private TextBox txt_Dtl_cUnit;
        private TextBox txt_Dtl_fQty;

        public FrmItemEditor()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str = "";
            if (this.txt_Dtl_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料编码不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_Dtl_cMNo.Focus();
            }
            else if (this.txt_Dtl_fQty.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料数量不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_Dtl_fQty.Focus();
            }
            else if (!FrmSTable.IsNumberic(this.txt_Dtl_fQty.Text.Trim()))
            {
                MessageBox.Show("对不起，物料数量为非法数值！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_Dtl_fQty.SelectAll();
                this.txt_Dtl_fQty.Focus();
            }
            else if (double.Parse(this.txt_Dtl_fQty.Text.Trim()) == 0.0)
            {
                MessageBox.Show("对不起，数量不能为0");
                this.txt_Dtl_fQty.SelectAll();
                this.txt_Dtl_fQty.Focus();
            }
            else if (this.txt_Dtl_cUnit.Text.Trim() == "")
            {
                MessageBox.Show("对不起，单位不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txt_Dtl_cUnit.SelectAll();
                this.txt_Dtl_cUnit.Focus();
            }
            else
            {
                if (!this.isOutBill)
                {
                }
                if (this.isOutBill && (double.Parse(this.txt_Dtl_fQty.Text.Trim()) > this.fUseQty))
                {
                    MessageBox.Show("对不起，出库数大于可出数！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.txt_Dtl_fQty.SelectAll();
                    this.txt_Dtl_fQty.Focus();
                }
                else
                {
                    string str2;
                    string str3;
                    DataSet set;
                    this.UIToDataRowView(this.drvItem, this.pnlDtlEdit);
                    if (this.bIsNew)
                    {
                        str = this.drvItem["cBNo"].ToString();
                        this.bIsResultOK = true;
                        this.bIsShowGrid = false;
                        this.DataRowViewToUI(this.drvItem, this.pnlDtlEdit);
                        this.bIsShowGrid = true;
                        str2 = DBSQLCommandInfo.GetSQLByDataRow(this.drvItem, "TWB_BillRemoveDtl", "cBNo,cMNo", "cMName,cSpec,cMatStyle,cMatQCLevel,cMatOther,cCSId,cSupplier", true);
                        str3 = "";
                        set = PubDBCommFuns.GetDataBySql(str2, DBSQLCommandInfo.GetFieldsForDate(this.drvItem), out str3);
                        this.bIsResultOK = set.Tables[0].Rows[0][0].ToString() == "0";
                        if (this.bIsResultOK)
                        {
                            MessageBox.Show("增加明细成功！");
                            this.ClearUIValues(this.pnlDtlEdit);
                            this.drvItem["cBNo"] = str;
                            this.drvItem["cMNo"] = "";
                            this.DataRowViewToUI(this.drvItem, this.pnlDtlEdit);
                            this.txt_Dtl_cMNo.SelectAll();
                            this.txt_Dtl_cMNo.Focus();
                        }
                    }
                    else
                    {
                        this.bIsShowGrid = false;
                        this.DataRowViewToUI(this.drvItem, this.pnlDtlEdit);
                        this.bIsShowGrid = true;
                        str2 = DBSQLCommandInfo.GetSQLByDataRow(this.drvItem, "TWB_BillRemoveDtl", "cBNo,cMNo", "cMName,cSpec,cMatStyle,cMatQCLevel,cMatOther,cCSId,cSupplier", false);
                        str3 = "";
                        set = PubDBCommFuns.GetDataBySql(str2, DBSQLCommandInfo.GetFieldsForDate(this.drvItem), out str3);
                        this.bIsResultOK = set.Tables[0].Rows[0][0].ToString() == "0";
                        base.Close();
                    }
                }
            }
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            this.bIsShowGrid = false;
            WareStore.SelectStkMaterial(base.AppInformation, base.UserInformation, new DoSelMaterialEvent(this.DoSelectMat));
            this.bIsShowGrid = true;
        }

        private void cmb_Dtl_cUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.btnOK.Focus();
            }
        }

        private void cmb_Dtl_cUnit_Leave(object sender, EventArgs e)
        {
            if (((this.txt_Dtl_cMNo.Text.Trim() != "") && (this.txt_Dtl_fQty.Text.Trim() != "")) && (this.txt_Dtl_cUnit.Text.Trim() != ""))
            {
                this.btnOK_Click(sender, e);
            }
        }

        public void DataRowToUI()
        {
            if (this.drvItem == null)
            {
                MessageBox.Show("对不起，物料明细数据行对象为空！");
            }
            else
            {
                this.bIsShowGrid = false;
                this.DataRowViewToUI(this.drvItem, this.pnlDtlEdit);
                this.bIsShowGrid = true;
                if ((!this.bIsNew && this.isOutBill) && (this.txt_Dtl_fQty.Text.Trim() != ""))
                {
                    string sErr = "";
                    double pCurUsedQty = double.Parse(this.txt_Dtl_fQty.Text.Trim());
                    pCurUsedQty = PubDBCommFuns.sp_Pack_GetItemBillQty(base.AppInformation.SvrSocket, 0, this.txt_Dtl_cMNo.Text.Trim(), this._WHId.Trim(), this._MatClass.Trim(), this._QCStatus, "", pCurUsedQty, out sErr);
                    if ((sErr.Trim() == "") || (sErr.Trim() == "0"))
                    {
                        this.fUseQty = pCurUsedQty;
                        this.lbl_Out.Text = "可出数：" + pCurUsedQty.ToString() + "  (可出数 =库存数-待出数)";
                    }
                    else
                    {
                        MessageBox.Show(sErr);
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

        private void DoSelectMat(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther, string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1, string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId, string sSupplier, int _nMatClass, bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                this.txt_Dtl_cMatOther.Text = sMatOther.Trim();
                this.txt_Dtl_cMatQCLevel.Text = sMatQCLevel.Trim();
                this.txt_Dtl_cMatStyle.Text = sMatStyle.Trim();
                this.txt_Dtl_cMName.Text = sMName.Trim();
                this.txt_Dtl_cMNo.Text = sMNo;
                this.txt_Dtl_cSpec.Text = sSpec.Trim();
                this.txt_Dtl_cSupplier.Text = sSupplier.Trim();
                this.txt_Dtl_cUnit.Text = sUnit;
                if (this.bIsNew && this.isOutBill)
                {
                    string sErr = "";
                    double num = 0.0;
                    num = PubDBCommFuns.sp_Pack_GetItemBillQty(base.AppInformation.SvrSocket, 0, this.txt_Dtl_cMNo.Text, this._WHId.Trim(), this._MatClass.Trim(), this._QCStatus, "", 0.0, out sErr);
                    if ((sErr.Trim() == "") || (sErr.Trim() == "0"))
                    {
                        this.fUseQty = num;
                        this.lbl_Out.Text = "可出数：" + num.ToString() + "  (可出数 =库存数-待出数)";
                    }
                    else
                    {
                        MessageBox.Show(sErr);
                    }
                }
            }
        }

        private void dtp_Dtl_dProdDate_Leave(object sender, EventArgs e)
        {
            if (!(!this.bIsNew || this.isOutBill))
            {
            }
        }

        private void FrmItemEditor_Load(object sender, EventArgs e)
        {
            this.grdDtl.Visible = false;
            if (this.isOutBill)
            {
                this.lbl_Out.Visible = true;
            }
            else
            {
                this.lbl_Out.Visible = false;
            }
            this.txt_Dtl_cMNo.Focus();
        }

        private int GetMaterialKeepDay(string sMNo)
        {
            int num = 360;
            string sSql = "select isnull(nKeepDay,360) nKeepDay from TPC_Material where cMNo='" + sMNo.Trim() + "'";
            object objValue = null;
            string sErr = "";
            PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "nKeepDay", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
                return num;
            }
            if (objValue != null)
            {
                num = int.Parse(objValue.ToString());
            }
            return num;
        }

        private int GetMaterialQCState(string sMNo)
        {
            int num = 1;
            string sSql = "select isnull(bIsQC,1) bIsQC from TPC_Material where cMNo='" + sMNo.Trim() + "'";
            object objValue = null;
            string sErr = "";
            PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "bIsQC", out objValue, out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
                return num;
            }
            if (objValue == null)
            {
                return num;
            }
            if (int.Parse(objValue.ToString()) == 1)
            {
                return 0;
            }
            return 1;
        }

        public int GetNewItem(string billNo)
        {
            string str = "TWB_BillInDtl";
            string str2 = "nItem";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetDtlSeq :TbName,:PFld,:SeqFld,:PValue",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "TbName",
                ParameterValue = str,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "PFld",
                ParameterValue = "cBNo",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "SeqFld",
                ParameterValue = str2,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "PValue",
                ParameterValue = billNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet dataSet = null;
            DataTable table = null;
            dataSet = client.GetDataSet(cmdInfo, out sErr);
            table = dataSet.Tables["data"];
            if (table == null)
            {
                dataSet.Clear();
                MessageBox.Show(sErr);
                return -1;
            }
            if (table.Rows.Count == 0)
            {
                dataSet.Clear();
                MessageBox.Show(" 获取明细序号无结果数据：" + sErr);
                return -1;
            }
            object obj2 = table.Rows[0][0];
            dataSet.Clear();
            return int.Parse(obj2.ToString());
        }

        private void grdDtl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.bdsItemList.Count > 0)
            {
                DataRowView current = (DataRowView) this.bdsItemList.Current;
                if (current != null)
                {
                    try
                    {
                        this.bIsShowGrid = false;
                        this.txt_Dtl_cMNo.Text = current["cMNo"].ToString();
                        this.bIsShowGrid = true;
                        if (this.bIsNew && this.isOutBill)
                        {
                            string sErr = "";
                            double num = 0.0;
                            num = PubDBCommFuns.sp_Pack_GetItemBillQty(base.AppInformation.SvrSocket, 0, this.txt_Dtl_cMNo.Text.Trim(), this._WHId.Trim(), this._MatClass.Trim(), this._QCStatus, "", 0.0, out sErr);
                            if ((sErr.Trim() == "") || (sErr.Trim() == "0"))
                            {
                                this.fUseQty = num;
                                this.lbl_Out.Text = "可出数：" + num.ToString() + "  (可出数 =库存数-待出数)";
                            }
                            else
                            {
                                MessageBox.Show(sErr);
                            }
                        }
                        this.txt_Dtl_cUnit.Text = current["cUnit"].ToString();
                        this.txt_Dtl_cMName.Text = current["cName"].ToString();
                        this.txt_Dtl_cSpec.Text = current["cSpec"].ToString();
                        if (this.isOutBill)
                        {
                            this.txt_Dtl_fQty.SelectAll();
                            this.txt_Dtl_fQty.Focus();
                        }
                        this.grdDtl.Visible = false;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void grdDtl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Return))
            {
                this.grdDtl_CellDoubleClick(null, null);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.panel1 = new Panel();
            this.pnlDtlEdit = new Panel();
            this.grdDtl = new DataGridView();
            this.colcMNo = new DataGridViewTextBoxColumn();
            this.colcName = new DataGridViewTextBoxColumn();
            this.colcSpec = new DataGridViewTextBoxColumn();
            this.colcUnit = new DataGridViewTextBoxColumn();
            this.bdsItemList = new BindingSource(this.components);
            this.txt_Dtl_cMatOther = new TextBox();
            this.label9 = new Label();
            this.txt_Dtl_cMatQCLevel = new TextBox();
            this.label8 = new Label();
            this.txt_Dtl_cMatStyle = new TextBox();
            this.label7 = new Label();
            this.button1 = new Button();
            this.btnSel = new Button();
            this.label4 = new Label();
            this.txt_Dtl_cSupplier = new TextBox();
            this.label3 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label22 = new Label();
            this.lbl_Out = new Label();
            this.txt_Dtl_cUnit = new TextBox();
            this.txt_Dtl_fQty = new TextBox();
            this.label25 = new Label();
            this.txt_Dtl_cMNo = new TextBox();
            this.label19 = new Label();
            this.label12 = new Label();
            this.txt_Dtl_cSpec = new TextBox();
            this.txt_Dtl_cMName = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.panel1.SuspendLayout();
            this.pnlDtlEdit.SuspendLayout();
            ((ISupportInitialize) this.grdDtl).BeginInit();
            ((ISupportInitialize) this.bdsItemList).BeginInit();
            base.SuspendLayout();
            this.btnOK.Location = new Point(0xd7, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0x185, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Location = new Point(0x15, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x290, 0x24);
            this.panel1.TabIndex = 1;
            this.pnlDtlEdit.BackColor = SystemColors.Info;
            this.pnlDtlEdit.Controls.Add(this.grdDtl);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMatOther);
            this.pnlDtlEdit.Controls.Add(this.label9);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMatQCLevel);
            this.pnlDtlEdit.Controls.Add(this.label8);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMatStyle);
            this.pnlDtlEdit.Controls.Add(this.label7);
            this.pnlDtlEdit.Controls.Add(this.button1);
            this.pnlDtlEdit.Controls.Add(this.btnSel);
            this.pnlDtlEdit.Controls.Add(this.label4);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cSupplier);
            this.pnlDtlEdit.Controls.Add(this.label3);
            this.pnlDtlEdit.Controls.Add(this.label6);
            this.pnlDtlEdit.Controls.Add(this.label5);
            this.pnlDtlEdit.Controls.Add(this.label22);
            this.pnlDtlEdit.Controls.Add(this.lbl_Out);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cUnit);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_fQty);
            this.pnlDtlEdit.Controls.Add(this.label25);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMNo);
            this.pnlDtlEdit.Controls.Add(this.label19);
            this.pnlDtlEdit.Controls.Add(this.label12);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cSpec);
            this.pnlDtlEdit.Controls.Add(this.txt_Dtl_cMName);
            this.pnlDtlEdit.Controls.Add(this.label1);
            this.pnlDtlEdit.Controls.Add(this.label2);
            this.pnlDtlEdit.Location = new Point(0x15, 0x15);
            this.pnlDtlEdit.Name = "pnlDtlEdit";
            this.pnlDtlEdit.Size = new Size(0x29d, 0x114);
            this.pnlDtlEdit.TabIndex = 0;
            this.pnlDtlEdit.Paint += new PaintEventHandler(this.pnlDtlEdit_Paint);
            this.grdDtl.AllowUserToAddRows = false;
            this.grdDtl.AllowUserToDeleteRows = false;
            this.grdDtl.AllowUserToOrderColumns = true;
            this.grdDtl.AutoGenerateColumns = false;
            this.grdDtl.ColumnHeadersHeight = 0x23;
            this.grdDtl.Columns.AddRange(new DataGridViewColumn[] { this.colcMNo, this.colcName, this.colcSpec, this.colcUnit });
            this.grdDtl.DataSource = this.bdsItemList;
            this.grdDtl.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdDtl.Location = new Point(0x41, 0x27);
            this.grdDtl.MultiSelect = false;
            this.grdDtl.Name = "grdDtl";
            this.grdDtl.ReadOnly = true;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.grdDtl.RowHeadersDefaultCellStyle = style;
            this.grdDtl.RowHeadersVisible = false;
            this.grdDtl.RowTemplate.Height = 0x17;
            this.grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdDtl.Size = new Size(0x242, 0xeb);
            this.grdDtl.TabIndex = 1;
            this.grdDtl.Tag = "8";
            this.grdDtl.CellDoubleClick += new DataGridViewCellEventHandler(this.grdDtl_CellDoubleClick);
            this.grdDtl.KeyDown += new KeyEventHandler(this.grdDtl_KeyDown);
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.HeaderText = "物料编码";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编码";
            this.colcMNo.Width = 120;
            this.colcName.DataPropertyName = "cName";
            this.colcName.HeaderText = "物料名";
            this.colcName.Name = "colcName";
            this.colcName.ReadOnly = true;
            this.colcName.ToolTipText = "物料名";
            this.colcName.Width = 250;
            this.colcSpec.DataPropertyName = "cSpec";
            this.colcSpec.HeaderText = "规格型号";
            this.colcSpec.Name = "colcSpec";
            this.colcSpec.ReadOnly = true;
            this.colcSpec.ToolTipText = "规格型号";
            this.colcSpec.Width = 150;
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.HeaderText = "有限期(天)";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.ToolTipText = "有限期(天)";
            this.colcUnit.Width = 50;
            this.bdsItemList.AllowNew = false;
            this.txt_Dtl_cMatOther.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMatOther.Location = new Point(0xa7, 0x57);
            this.txt_Dtl_cMatOther.Name = "txt_Dtl_cMatOther";
            this.txt_Dtl_cMatOther.ReadOnly = true;
            this.txt_Dtl_cMatOther.Size = new Size(0x1f3, 0x15);
            this.txt_Dtl_cMatOther.TabIndex = 0x5d;
            this.txt_Dtl_cMatOther.Tag = "0";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x6d, 0x59);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 0x5e;
            this.label9.Text = "其他属性：";
            this.txt_Dtl_cMatQCLevel.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMatQCLevel.Location = new Point(0x42, 0x55);
            this.txt_Dtl_cMatQCLevel.Name = "txt_Dtl_cMatQCLevel";
            this.txt_Dtl_cMatQCLevel.ReadOnly = true;
            this.txt_Dtl_cMatQCLevel.Size = new Size(0x2b, 0x15);
            this.txt_Dtl_cMatQCLevel.TabIndex = 0x5b;
            this.txt_Dtl_cMatQCLevel.Tag = "0";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(7, 0x57);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x29, 12);
            this.label8.TabIndex = 0x5c;
            this.label8.Text = "质等：";
            this.txt_Dtl_cMatStyle.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMatStyle.Location = new Point(360, 0x33);
            this.txt_Dtl_cMatStyle.Name = "txt_Dtl_cMatStyle";
            this.txt_Dtl_cMatStyle.ReadOnly = true;
            this.txt_Dtl_cMatStyle.Size = new Size(0x132, 0x15);
            this.txt_Dtl_cMatStyle.TabIndex = 0x59;
            this.txt_Dtl_cMatStyle.Tag = "0";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x144, 0x33);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 90;
            this.label7.Text = "款式：";
            this.button1.Enabled = false;
            this.button1.Location = new Point(0x277, 0x87);
            this.button1.Name = "button1";
            this.button1.Size = new Size(20, 20);
            this.button1.TabIndex = 0x57;
            this.button1.Text = "…";
            this.button1.UseVisualStyleBackColor = true;
            this.btnSel.Location = new Point(0x12a, 15);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new Size(20, 20);
            this.btnSel.TabIndex = 0x44;
            this.btnSel.Text = "…";
            this.btnSel.UseVisualStyleBackColor = true;
            this.btnSel.Click += new EventHandler(this.btnSel_Click);
            this.label4.AutoSize = true;
            this.label4.ForeColor = SystemColors.ActiveCaption;
            this.label4.Location = new Point(0x26e, 0x8d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 12);
            this.label4.TabIndex = 0x58;
            this.label4.Text = "*";
            this.txt_Dtl_cSupplier.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cSupplier.Location = new Point(0x18f, 0x87);
            this.txt_Dtl_cSupplier.Name = "txt_Dtl_cSupplier";
            this.txt_Dtl_cSupplier.ReadOnly = true;
            this.txt_Dtl_cSupplier.Size = new Size(0xdd, 0x15);
            this.txt_Dtl_cSupplier.TabIndex = 0x55;
            this.txt_Dtl_cSupplier.Tag = "0";
            this.label3.Location = new Point(340, 0x83);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 0x1d);
            this.label3.TabIndex = 0x56;
            this.label3.Text = "供应商/生产商：";
            this.label6.AutoSize = true;
            this.label6.ForeColor = SystemColors.ActiveCaption;
            this.label6.Location = new Point(0xf2, 180);
            this.label6.Name = "label6";
            this.label6.Size = new Size(11, 12);
            this.label6.TabIndex = 0x52;
            this.label6.Text = "*";
            this.label5.AutoSize = true;
            this.label5.ForeColor = SystemColors.ActiveCaption;
            this.label5.Location = new Point(0xf1, 0x8a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(11, 12);
            this.label5.TabIndex = 0x51;
            this.label5.Text = "*";
            this.label22.AutoSize = true;
            this.label22.ForeColor = SystemColors.ActiveCaption;
            this.label22.Location = new Point(0x121, 0x15);
            this.label22.Name = "label22";
            this.label22.Size = new Size(11, 12);
            this.label22.TabIndex = 0x4e;
            this.label22.Text = "*";
            this.lbl_Out.Location = new Point(0x102, 0x87);
            this.lbl_Out.Name = "lbl_Out";
            this.lbl_Out.Size = new Size(0x169, 0x13);
            this.lbl_Out.TabIndex = 0x49;
            this.txt_Dtl_cUnit.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cUnit.Location = new Point(0x41, 0xb0);
            this.txt_Dtl_cUnit.Name = "txt_Dtl_cUnit";
            this.txt_Dtl_cUnit.Size = new Size(0xb0, 0x15);
            this.txt_Dtl_cUnit.TabIndex = 6;
            this.txt_Dtl_cUnit.Tag = "0";
            this.txt_Dtl_cUnit.KeyDown += new KeyEventHandler(this.cmb_Dtl_cUnit_KeyDown);
            this.txt_Dtl_fQty.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_fQty.Location = new Point(0x40, 0x86);
            this.txt_Dtl_fQty.Name = "txt_Dtl_fQty";
            this.txt_Dtl_fQty.Size = new Size(0xb0, 0x15);
            this.txt_Dtl_fQty.TabIndex = 5;
            this.txt_Dtl_fQty.Tag = "0";
            this.txt_Dtl_fQty.KeyDown += new KeyEventHandler(this.txt_Dtl_cItemName_KeyDown);
            this.txt_Dtl_fQty.Enter += new EventHandler(this.txt_Dtl_cProductBatchNo_Enter);
            this.label25.AutoSize = true;
            this.label25.Location = new Point(5, 0x86);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x29, 12);
            this.label25.TabIndex = 0x43;
            this.label25.Text = "数量：";
            this.txt_Dtl_cMNo.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMNo.Location = new Point(0x42, 15);
            this.txt_Dtl_cMNo.Name = "txt_Dtl_cMNo";
            this.txt_Dtl_cMNo.Size = new Size(0xdd, 0x15);
            this.txt_Dtl_cMNo.TabIndex = 0;
            this.txt_Dtl_cMNo.Tag = "0";
            this.txt_Dtl_cMNo.TextChanged += new EventHandler(this.txt_Dtl_cItemId_TextChanged);
            this.txt_Dtl_cMNo.ReadOnlyChanged += new EventHandler(this.txt_Dtl_cMNo_ReadOnlyChanged);
            this.txt_Dtl_cMNo.KeyDown += new KeyEventHandler(this.txt_Dtl_cItemId_KeyDown);
            this.label19.AutoSize = true;
            this.label19.Location = new Point(7, 0x10);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x41, 12);
            this.label19.TabIndex = 0x37;
            this.label19.Text = "物料编码：";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(7, 0xb3);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x29, 12);
            this.label12.TabIndex = 0x33;
            this.label12.Text = "单位：";
            this.txt_Dtl_cSpec.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cSpec.Location = new Point(0x42, 0x33);
            this.txt_Dtl_cSpec.Name = "txt_Dtl_cSpec";
            this.txt_Dtl_cSpec.ReadOnly = true;
            this.txt_Dtl_cSpec.Size = new Size(0xfc, 0x15);
            this.txt_Dtl_cSpec.TabIndex = 0x4a;
            this.txt_Dtl_cSpec.Tag = "0";
            this.txt_Dtl_cMName.BorderStyle = BorderStyle.FixedSingle;
            this.txt_Dtl_cMName.Location = new Point(360, 14);
            this.txt_Dtl_cMName.Name = "txt_Dtl_cMName";
            this.txt_Dtl_cMName.ReadOnly = true;
            this.txt_Dtl_cMName.Size = new Size(0x132, 0x15);
            this.txt_Dtl_cMName.TabIndex = 0x4c;
            this.txt_Dtl_cMName.Tag = "0";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x33);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0x4b;
            this.label1.Text = "规格：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x144, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0x4d;
            this.label2.Text = "名称：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b1, 0x198);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pnlDtlEdit);
            base.Name = "FrmItemEditor";
            this.Text = "出库物料编辑器";
            base.Load += new EventHandler(this.FrmItemEditor_Load);
            this.panel1.ResumeLayout(false);
            this.pnlDtlEdit.ResumeLayout(false);
            this.pnlDtlEdit.PerformLayout();
            ((ISupportInitialize) this.grdDtl).EndInit();
            ((ISupportInitialize) this.bdsItemList).EndInit();
            base.ResumeLayout(false);
        }

        private bool OpenItemList(string sItemValue)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder("");
            builder.Append("select * from TPC_Material ");
            if (sItemValue.Trim() != "")
            {
                builder.Append(" where (cMNo like '%" + sItemValue.Trim() + "%') or (cName like '%" + sItemValue.Trim() + "%') or (isnull(cPYJM,' ') like '%" + sItemValue.Trim().ToUpper() + "%') or (isnull(cWBJM,' ') like '%" + sItemValue.Trim().ToUpper() + "%')");
            }
            if (this.dsItemList.Tables["data"] != null)
            {
                this.dsItemList.Tables["data"].Clear();
            }
            this.grdDtl.AutoGenerateColumns = false;
            string sErr = "";
            this.dsItemList = PubDBCommFuns.GetDataBySql(builder.ToString(), out sErr);
            flag = this.dsItemList.Tables[0].Rows[0][0].ToString() == "0";
            if (this.dsItemList.Tables[0].Rows[0][0].ToString() == "0")
            {
                this.bdsItemList.DataSource = this.dsItemList.Tables["data"];
                this.grdDtl.DataSource = this.bdsItemList;
            }
            else
            {
                MessageBox.Show(this.dsItemList.Tables[0].Rows[0][0].ToString());
            }
            this.grdDtl.Visible = true;
            return flag;
        }

        private void pnlDtlEdit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txt_Dtl_cBatchNo_Enter(object sender, EventArgs e)
        {
            if ((((this.txt_Dtl_cMNo.Text.Trim() != "") && this.bIsNew) && !this.isOutBill) && (this._BType.Trim() == "101"))
            {
                string text = "";
                if ((text.Trim() != "") && (text.Trim() != "0"))
                {
                    MessageBox.Show(text);
                }
            }
        }

        private void txt_Dtl_cItemId_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode != Keys.Return)
            {
                if ((keyCode == Keys.Down) && (this.bdsItemList.Count > 0))
                {
                    this.grdDtl.Focus();
                }
            }
            else
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txt_Dtl_cItemId_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_Dtl_cMNo.Text.ToString() == "")
            {
                this.grdDtl.Visible = false;
            }
            else if (this.bIsNew && this.bIsShowGrid)
            {
                this.OpenItemList(((TextBox) sender).Text.Trim());
            }
            else if (!(this.bIsNew || !this.bIsShowGrid))
            {
                this.txt_Dtl_cMNo.ReadOnly = true;
                this.grdDtl.Visible = false;
            }
        }

        private void txt_Dtl_cItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txt_Dtl_cMNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            base.ChangeTextBoxBkColorByReadOnly(sender, ((Control) sender).Parent.BackColor, Color.White);
        }

        private void txt_Dtl_cProductBatchNo_Enter(object sender, EventArgs e)
        {
            ((TextBox) sender).SelectAll();
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
                this.txt_Dtl_cMNo.ReadOnly = !this.bIsNew;
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

        public string BType
        {
            get
            {
                return this._BType.Trim();
            }
            set
            {
                this._BType = value.Trim();
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

        [Description("是否为出库单物料录入")]
        public bool IsOutBill
        {
            get
            {
                return this.isOutBill;
            }
            set
            {
                this.isOutBill = value;
                if (this.isOutBill)
                {
                    this.lbl_Out.Visible = this.isOutBill;
                    this.Text = "出库物料编辑期";
                }
                else
                {
                    this.Text = "入库物料编辑期";
                }
            }
        }

        public string MatClass
        {
            get
            {
                return this._MatClass.Trim();
            }
            set
            {
                this._MatClass = value.Trim();
            }
        }

        private int QCStatus
        {
            get
            {
                return this._QCStatus;
            }
            set
            {
                this._QCStatus = value;
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

