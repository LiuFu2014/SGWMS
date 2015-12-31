namespace WareStoreMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;

    public class frmSelIOBillMat : FrmSTable
    {
        private int _BClass = 0;
        private string _BillNo = "";
        private DoSelIOStoreMatBillDataEvent _DoSelIOStoreMatBillData = null;
        private string _MName = "";
        private BindingSource bds_Data;
        private Button btn_Cancel;
        private Button btn_OK;
        private Button btn_Qry;
        private Button btn_Reset;
        private CheckBox chk_Date;
        private ComboBox cmb_cABC;
        private DataGridViewTextBoxColumn col_cABC;
        private DataGridViewTextBoxColumn col_cBatchNo;
        private DataGridViewTextBoxColumn col_cBNo;
        private DataGridViewTextBoxColumn col_cBNoIn;
        private DataGridViewTextBoxColumn col_cCSId;
        private DataGridViewTextBoxColumn col_cMatOther;
        private DataGridViewTextBoxColumn col_cMatQCLevel;
        private DataGridViewTextBoxColumn col_cMatStyle;
        private DataGridViewTextBoxColumn col_cMName;
        private DataGridViewTextBoxColumn col_cMNo;
        private DataGridViewTextBoxColumn col_cRemark;
        private DataGridViewTextBoxColumn col_cSpec;
        private DataGridViewTextBoxColumn col_cSupplier;
        private DataGridViewTextBoxColumn col_cUnit;
        private DataGridViewTextBoxColumn col_fQty;
        private DataGridViewTextBoxColumn col_fWeight;
        private DataGridViewTextBoxColumn col_nBClass;
        private DataGridViewTextBoxColumn col_nItem;
        private DataGridViewTextBoxColumn col_nItemIn;
        private IContainer components = null;
        private DateTimePicker dtp_From;
        private DateTimePicker dtp_To;
        private DataGridView grd_Data;
        private GroupBox grp_Buttons;
        private GroupBox grp_Condition;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private ProgressBar prgMain;
        public TextBox txt_cBNo;
        private TextBox txt_cMatOther;
        private TextBox txt_cMatQCLevel;
        private TextBox txt_cMatStyle;
        public TextBox txt_cName;
        private TextBox txt_cRemark;
        private TextBox txt_cSpec;

        public frmSelIOBillMat()
        {
            this.InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.bds_Data.Count == 0)
            {
                MessageBox.Show("无物料数据可选择！");
            }
            else if (this.grd_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("没有选择物料数据！");
            }
            else
            {
                bool bDoOK = false;
                this.prgMain.Maximum = this.grd_Data.SelectedRows.Count;
                this.prgMain.Minimum = 0;
                this.prgMain.Value = 0;
                this.prgMain.Visible = true;
                foreach (DataGridViewRow row in this.grd_Data.SelectedRows)
                {
                    if (this._DoSelIOStoreMatBillData != null)
                    {
                        int nBClass = 0;
                        int nItem = 0;
                        double fQty = 0.0;
                        double fWeight = 0.0;
                        if ((row.Cells["col_nBClass"].Value != null) && (row.Cells["col_nBClass"].Value.ToString() != ""))
                        {
                            nBClass = Convert.ToInt16(row.Cells["col_nBClass"].Value);
                        }
                        if ((row.Cells["col_nItem"].Value != null) && (row.Cells["col_nItem"].Value.ToString() != ""))
                        {
                            nItem = Convert.ToInt16(row.Cells["col_nItem"].Value);
                        }
                        if ((row.Cells["col_fQty"].Value != null) && (row.Cells["col_fQty"].Value.ToString() != ""))
                        {
                            fQty = Convert.ToDouble(row.Cells["col_fQty"].Value);
                        }
                        if ((row.Cells["col_fWeight"].Value != null) && (row.Cells["col_fWeight"].Value.ToString() != ""))
                        {
                            fWeight = Convert.ToDouble(row.Cells["col_fWeight"].Value);
                        }
                        try
                        {
                            this._DoSelIOStoreMatBillData(nBClass, row.Cells["col_cBNo"].Value.ToString(), nItem, row.Cells["col_cMNo"].Value.ToString(), row.Cells["col_cMName"].Value.ToString(), row.Cells["col_cSpec"].Value.ToString(), row.Cells["col_cMatStyle"].Value.ToString(), row.Cells["col_cMatQCLevel"].Value.ToString(), row.Cells["col_cMatOther"].Value.ToString(), row.Cells["col_cRemark"].Value.ToString(), row.Cells["col_cABC"].Value.ToString(), fQty, fWeight, row.Cells["col_cUnit"].Value.ToString(), row.Cells["col_cCSId"].Value.ToString(), row.Cells["col_cSupplier"].Value.ToString(), row.Cells["col_cBatchNo"].Value.ToString(), row.Cells["col_cBNoIn"].Value.ToString(), Convert.ToInt16(row.Cells["col_nItemIn"].Value), out bDoOK);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                        this.prgMain.Value++;
                    }
                }
                if (bDoOK)
                {
                    base.Close();
                }
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            string sErr = "";
            string sql = this.GetSql();
            DataSet set = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sql, "IOStoreDtl", 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(exception.Message);
                return;
            }
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set != null)
            {
                this.bds_Data.DataSource = set.Tables["IOStoreDtl"].Copy();
                set.Clear();
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.txt_cBNo.Text = "";
            this.txt_cMatOther.Text = "";
            this.txt_cMatQCLevel.Text = "";
            this.txt_cMatStyle.Text = "";
            this.txt_cName.Text = "";
            this.txt_cRemark.Text = "";
            this.txt_cSpec.Text = "";
            this.cmb_cABC.SelectedIndex = -1;
            this.txt_cBNo.Focus();
        }

        private void chk_Date_CheckedChanged(object sender, EventArgs e)
        {
            this.dtp_From.Enabled = this.chk_Date.Checked;
            this.dtp_To.Enabled = this.chk_Date.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelIOBillMat_Load(object sender, EventArgs e)
        {
            this.chk_Date.Checked = false;
            this.dtp_From.Value = DateTime.Now.AddDays(-60.0);
            this.dtp_To.Value = DateTime.Now;
        }

        private string GetSql()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("select pck.nBClass, pck.cChildId cMNo,sum(pck.fQty) fQty,pck.cUnit,pck.cBNo,pck.nItem,isnull(pck.cBatchNo,' ') cBatchNo,");
            builder.Append(" pck.dProdDate,pck.dBadDate,isnull(dtl.cCSId,' ') cCSId,isnull(dtl.cSupplier,' ') cSupplier,mat.cName cMName,");
            builder.Append(" mat.cSpec,isnull(mat.cMatQCLevel,' ') cMatQCLevel,isnull(mat.cMatStyle,'') cMatStyle,isnull(mat.cMatOther,' ') cMatOther ,pck.cBNoIn,pck.nItemIn,isnull(mat.cABC,' ') cABC,isnull(mat.cRemark,' ') cRemark,isnull(mat.fWeight,0) fWeight");
            builder.Append(" from TPB_MaterialPackingHis pck ");
            builder.Append(" left join TWB_BillInDtl dtl on pck.cBNo=dtl.cBNo and pck.nItem=dtl.nItem ");
            builder.Append(" left join TPC_Material mat on pck.cChildId=mat.cMNo ");
            builder.Append(" where pck.nStatus=1 ");
            if (this._BClass > 0)
            {
                builder.Append(" and (pck.nBClass=" + this._BClass.ToString() + ")");
            }
            if (this.chk_Date.Checked)
            {
                string str = this.dtp_From.Value.ToString("yyyy-MM-dd hh:mm:ss");
                builder.Append(" and ( pck.dOperateTime >= '" + str + "' )");
                builder.Append(" and ( pck.dOperateTime <= '" + this.dtp_To.Value.ToString("yyyy-MM-dd hh:mm:ss") + "' )");
            }
            if (this.txt_cBNo.Text.Trim() != "")
            {
                builder.Append(" and (pck.cBNo like '%" + this.txt_cBNo.Text.Trim() + "%') ");
            }
            string str2 = this.txt_cName.Text.Trim();
            if (str2 != "")
            {
                builder.Append(" and ((isnull(mat.cMNo,' ') like '%" + str2 + "%') or (isnull(mat.cName,' ') like '%" + str2 + "%') or (isnull(mat.cWBJM,' ') like '%" + str2 + "%') or (isnull(mat.cPYJM,' ') like '%" + str2 + "%') ) ");
            }
            str2 = this.txt_cSpec.Text.Trim();
            if (str2 != "")
            {
                builder.Append(" and (isnull(mat.cSpec,' ') like '%" + str2 + "%') ");
            }
            str2 = this.txt_cMatStyle.Text.Trim();
            if (str2 != "")
            {
                builder.Append(" and (isnull(mat.cMatStyle,' ') like '%" + str2 + "%') ");
            }
            str2 = this.txt_cMatQCLevel.Text.Trim();
            if (str2 != "")
            {
                builder.Append(" and (isnull(mat.cMatQCLevel,' ') like '%" + str2 + "%') ");
            }
            str2 = this.txt_cMatOther.Text.Trim();
            if (str2 != "")
            {
                builder.Append(" and (isnull(mat.cMatOther,' ') like '%" + str2 + "%') ");
            }
            str2 = this.txt_cRemark.Text.Trim();
            if (str2 != "")
            {
                builder.Append(" and (isnull(mat.cRemark,' ') like '%" + str2 + "%') ");
            }
            if ((this.cmb_cABC.Text.Trim() != "") && (this.cmb_cABC.SelectedIndex >= 0))
            {
                builder.Append(" and ( isnull(mat.cABC,' ') like '%" + this.cmb_cABC.Text.Trim() + "%' )");
            }
            builder.Append(" group by pck.nBClass,pck.cChildId,pck.cUnit,pck.cBNo,pck.nItem,isnull(pck.cBatchNo,' '),");
            builder.Append(" pck.dProdDate,pck.dBadDate,isnull(dtl.cCSId,' '),isnull(dtl.cSupplier,' '),mat.cName,mat.cSpec, ");
            builder.Append(" isnull(mat.cMatQCLevel,' '),isnull(mat.cMatStyle,''),isnull(mat.cMatOther,' '),pck.cBNoIn,pck.nItemIn,isnull(mat.cABC,' '),isnull(mat.cRemark,' '),isnull(mat.fWeight,0)");
            return builder.ToString();
        }

        private void grd_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.bds_Data.Count == 0)
            {
                MessageBox.Show("对不起，无数据可选择！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bds_Data.Current;
                if (current == null)
                {
                    MessageBox.Show("对不起，没有选择数据！");
                }
                else
                {
                    bool bDoOK = false;
                    if (this._DoSelIOStoreMatBillData != null)
                    {
                        int nBClass = 0;
                        int nItem = 0;
                        double fQty = 0.0;
                        double fWeight = 0.0;
                        nBClass = Convert.ToInt16(current["nBClass"]);
                        nItem = Convert.ToInt16(current["nItem"]);
                        fQty = Convert.ToDouble(current["fQty"]);
                        if (current["fWeight"] != null)
                        {
                            fWeight = Convert.ToDouble(current["fWeight"]);
                        }
                        try
                        {
                            this._DoSelIOStoreMatBillData(nBClass, current["cBNo"].ToString(), nItem, current["cMNo"].ToString(), current["cMName"].ToString(), current["cSpec"].ToString(), current["cMatStyle"].ToString(), current["cMatQCLevel"].ToString(), current["cMatOther"].ToString(), current["cRemark"].ToString(), current["cABC"].ToString(), fQty, fWeight, current["cUnit"].ToString(), current["cCSId"].ToString(), current["cSupplier"].ToString(), current["cBatchNo"].ToString(), current["cBNoIn"].ToString(), Convert.ToInt16(current["nItemIn"]), out bDoOK);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }
                    if (bDoOK)
                    {
                        base.Close();
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.grp_Condition = new GroupBox();
            this.chk_Date = new CheckBox();
            this.label7 = new Label();
            this.dtp_To = new DateTimePicker();
            this.dtp_From = new DateTimePicker();
            this.txt_cBNo = new TextBox();
            this.label10 = new Label();
            this.label9 = new Label();
            this.cmb_cABC = new ComboBox();
            this.txt_cRemark = new TextBox();
            this.label6 = new Label();
            this.txt_cMatOther = new TextBox();
            this.label5 = new Label();
            this.txt_cMatQCLevel = new TextBox();
            this.label4 = new Label();
            this.txt_cMatStyle = new TextBox();
            this.label3 = new Label();
            this.txt_cSpec = new TextBox();
            this.label2 = new Label();
            this.txt_cName = new TextBox();
            this.label1 = new Label();
            this.btn_Reset = new Button();
            this.btn_Qry = new Button();
            this.prgMain = new ProgressBar();
            this.bds_Data = new BindingSource(this.components);
            this.btn_OK = new Button();
            this.btn_Cancel = new Button();
            this.grp_Buttons = new GroupBox();
            this.grd_Data = new DataGridView();
            this.col_cBNo = new DataGridViewTextBoxColumn();
            this.col_nItem = new DataGridViewTextBoxColumn();
            this.col_cMNo = new DataGridViewTextBoxColumn();
            this.col_cMName = new DataGridViewTextBoxColumn();
            this.col_cSpec = new DataGridViewTextBoxColumn();
            this.col_cMatStyle = new DataGridViewTextBoxColumn();
            this.col_cMatQCLevel = new DataGridViewTextBoxColumn();
            this.col_cMatOther = new DataGridViewTextBoxColumn();
            this.col_cABC = new DataGridViewTextBoxColumn();
            this.col_cRemark = new DataGridViewTextBoxColumn();
            this.col_cBatchNo = new DataGridViewTextBoxColumn();
            this.col_fQty = new DataGridViewTextBoxColumn();
            this.col_cUnit = new DataGridViewTextBoxColumn();
            this.col_cBNoIn = new DataGridViewTextBoxColumn();
            this.col_nItemIn = new DataGridViewTextBoxColumn();
            this.col_fWeight = new DataGridViewTextBoxColumn();
            this.col_nBClass = new DataGridViewTextBoxColumn();
            this.col_cCSId = new DataGridViewTextBoxColumn();
            this.col_cSupplier = new DataGridViewTextBoxColumn();
            this.grp_Condition.SuspendLayout();
            ((ISupportInitialize) this.bds_Data).BeginInit();
            this.grp_Buttons.SuspendLayout();
            ((ISupportInitialize) this.grd_Data).BeginInit();
            base.SuspendLayout();
            this.grp_Condition.Controls.Add(this.chk_Date);
            this.grp_Condition.Controls.Add(this.label7);
            this.grp_Condition.Controls.Add(this.dtp_To);
            this.grp_Condition.Controls.Add(this.dtp_From);
            this.grp_Condition.Controls.Add(this.txt_cBNo);
            this.grp_Condition.Controls.Add(this.label10);
            this.grp_Condition.Controls.Add(this.label9);
            this.grp_Condition.Controls.Add(this.cmb_cABC);
            this.grp_Condition.Controls.Add(this.txt_cRemark);
            this.grp_Condition.Controls.Add(this.label6);
            this.grp_Condition.Controls.Add(this.txt_cMatOther);
            this.grp_Condition.Controls.Add(this.label5);
            this.grp_Condition.Controls.Add(this.txt_cMatQCLevel);
            this.grp_Condition.Controls.Add(this.label4);
            this.grp_Condition.Controls.Add(this.txt_cMatStyle);
            this.grp_Condition.Controls.Add(this.label3);
            this.grp_Condition.Controls.Add(this.txt_cSpec);
            this.grp_Condition.Controls.Add(this.label2);
            this.grp_Condition.Controls.Add(this.txt_cName);
            this.grp_Condition.Controls.Add(this.label1);
            this.grp_Condition.Controls.Add(this.btn_Reset);
            this.grp_Condition.Controls.Add(this.btn_Qry);
            this.grp_Condition.Dock = DockStyle.Top;
            this.grp_Condition.Location = new Point(0, 0);
            this.grp_Condition.Name = "grp_Condition";
            this.grp_Condition.Size = new Size(0x39b, 0x6a);
            this.grp_Condition.TabIndex = 3;
            this.grp_Condition.TabStop = false;
            this.grp_Condition.Text = "条件";
            this.chk_Date.AutoSize = true;
            this.chk_Date.Location = new Point(0xf5, 0x16);
            this.chk_Date.Name = "chk_Date";
            this.chk_Date.Size = new Size(0x48, 0x10);
            this.chk_Date.TabIndex = 0x1c;
            this.chk_Date.Text = "操作日期";
            this.chk_Date.UseVisualStyleBackColor = true;
            this.chk_Date.CheckedChanged += new EventHandler(this.chk_Date_CheckedChanged);
            this.label7.BackColor = SystemColors.ControlText;
            this.label7.Location = new Point(0x1dd, 0x1c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x2b, 1);
            this.label7.TabIndex = 0x1b;
            this.label7.Text = "物料款式";
            this.dtp_To.Enabled = false;
            this.dtp_To.Location = new Point(0x210, 20);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new Size(0x86, 0x15);
            this.dtp_To.TabIndex = 0x1a;
            this.dtp_From.Enabled = false;
            this.dtp_From.Location = new Point(0x147, 20);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new Size(0x86, 0x15);
            this.dtp_From.TabIndex = 0x19;
            this.txt_cBNo.Location = new Point(0x47, 20);
            this.txt_cBNo.Name = "txt_cBNo";
            this.txt_cBNo.Size = new Size(0xa2, 0x15);
            this.txt_cBNo.TabIndex = 0x17;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(12, 0x18);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 0x16;
            this.label10.Text = "单号";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xf5, 80);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x17, 12);
            this.label9.TabIndex = 0x15;
            this.label9.Text = "ABC";
            this.cmb_cABC.FormattingEnabled = true;
            this.cmb_cABC.Items.AddRange(new object[] { "A", "B", "C" });
            this.cmb_cABC.Location = new Point(0x12b, 0x4c);
            this.cmb_cABC.Name = "cmb_cABC";
            this.cmb_cABC.Size = new Size(0xa2, 20);
            this.cmb_cABC.TabIndex = 20;
            this.txt_cRemark.Location = new Point(0x47, 0x4c);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.Size = new Size(0xa2, 0x15);
            this.txt_cRemark.TabIndex = 0x10;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 80);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "物料备注";
            this.txt_cMatOther.Location = new Point(0x2ed, 0x31);
            this.txt_cMatOther.Name = "txt_cMatOther";
            this.txt_cMatOther.Size = new Size(0xa2, 0x15);
            this.txt_cMatOther.TabIndex = 14;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(690, 0x35);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "其他属性";
            this.txt_cMatQCLevel.Location = new Point(0x210, 0x31);
            this.txt_cMatQCLevel.Name = "txt_cMatQCLevel";
            this.txt_cMatQCLevel.Size = new Size(0x86, 0x15);
            this.txt_cMatQCLevel.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1dd, 0x35);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "质量等级";
            this.txt_cMatStyle.Location = new Point(0x12b, 0x31);
            this.txt_cMatStyle.Name = "txt_cMatStyle";
            this.txt_cMatStyle.Size = new Size(0xa2, 0x15);
            this.txt_cMatStyle.TabIndex = 10;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xf5, 0x35);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "物料款式";
            this.txt_cSpec.Location = new Point(0x47, 0x31);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.Size = new Size(0xa2, 0x15);
            this.txt_cSpec.TabIndex = 8;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "物料规格";
            this.txt_cName.Location = new Point(0x2ed, 20);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.Size = new Size(0xa2, 0x15);
            this.txt_cName.TabIndex = 6;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(690, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "物料";
            this.btn_Reset.Location = new Point(0x29c, 0x4b);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new Size(0x68, 0x17);
            this.btn_Reset.TabIndex = 3;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new EventHandler(this.btn_Reset_Click);
            this.btn_Qry.Location = new Point(0x210, 0x4b);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x68, 0x17);
            this.btn_Qry.TabIndex = 2;
            this.btn_Qry.Text = "查询(&Q)";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.prgMain.Location = new Point(6, 0x27);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new Size(0x38d, 0x15);
            this.prgMain.TabIndex = 2;
            this.prgMain.Visible = false;
            this.bds_Data.AllowNew = false;
            this.btn_OK.Location = new Point(0x12f, 11);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.btn_Cancel.Location = new Point(0x20a, 11);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            this.grp_Buttons.Controls.Add(this.prgMain);
            this.grp_Buttons.Controls.Add(this.btn_Cancel);
            this.grp_Buttons.Controls.Add(this.btn_OK);
            this.grp_Buttons.Dock = DockStyle.Bottom;
            this.grp_Buttons.Location = new Point(0, 0x1e1);
            this.grp_Buttons.Name = "grp_Buttons";
            this.grp_Buttons.Size = new Size(0x39b, 0x41);
            this.grp_Buttons.TabIndex = 4;
            this.grp_Buttons.TabStop = false;
            this.grd_Data.AutoGenerateColumns = false;
            this.grd_Data.Columns.AddRange(new DataGridViewColumn[] { 
                this.col_cBNo, this.col_nItem, this.col_cMNo, this.col_cMName, this.col_cSpec, this.col_cMatStyle, this.col_cMatQCLevel, this.col_cMatOther, this.col_cABC, this.col_cRemark, this.col_cBatchNo, this.col_fQty, this.col_cUnit, this.col_cBNoIn, this.col_nItemIn, this.col_fWeight, 
                this.col_nBClass, this.col_cCSId, this.col_cSupplier
             });
            this.grd_Data.DataSource = this.bds_Data;
            this.grd_Data.Dock = DockStyle.Fill;
            this.grd_Data.Location = new Point(0, 0x6a);
            this.grd_Data.Name = "grd_Data";
            this.grd_Data.ReadOnly = true;
            this.grd_Data.RowHeadersVisible = false;
            this.grd_Data.RowTemplate.Height = 0x17;
            this.grd_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grd_Data.Size = new Size(0x39b, 0x177);
            this.grd_Data.TabIndex = 6;
            this.grd_Data.CellDoubleClick += new DataGridViewCellEventHandler(this.grd_Data_CellDoubleClick);
            this.col_cBNo.DataPropertyName = "cBNo";
            this.col_cBNo.HeaderText = "单号";
            this.col_cBNo.Name = "col_cBNo";
            this.col_cBNo.ReadOnly = true;
            this.col_nItem.DataPropertyName = "nItem";
            this.col_nItem.HeaderText = "单明细号";
            this.col_nItem.Name = "col_nItem";
            this.col_nItem.ReadOnly = true;
            this.col_nItem.Width = 0x41;
            this.col_cMNo.DataPropertyName = "cMNo";
            this.col_cMNo.HeaderText = "物料编码";
            this.col_cMNo.Name = "col_cMNo";
            this.col_cMNo.ReadOnly = true;
            this.col_cMName.DataPropertyName = "cMName";
            this.col_cMName.HeaderText = "物料名称";
            this.col_cMName.Name = "col_cMName";
            this.col_cMName.ReadOnly = true;
            this.col_cSpec.DataPropertyName = "cSpec";
            this.col_cSpec.HeaderText = "规格型号";
            this.col_cSpec.Name = "col_cSpec";
            this.col_cSpec.ReadOnly = true;
            this.col_cMatStyle.DataPropertyName = "cMatStyle";
            this.col_cMatStyle.HeaderText = "款式";
            this.col_cMatStyle.Name = "col_cMatStyle";
            this.col_cMatStyle.ReadOnly = true;
            this.col_cMatStyle.Width = 70;
            this.col_cMatQCLevel.DataPropertyName = "cMatQCLevel";
            this.col_cMatQCLevel.HeaderText = "质量等级";
            this.col_cMatQCLevel.Name = "col_cMatQCLevel";
            this.col_cMatQCLevel.ReadOnly = true;
            this.col_cMatQCLevel.Width = 70;
            this.col_cMatOther.DataPropertyName = "cMatOther";
            this.col_cMatOther.HeaderText = "其他物料属性";
            this.col_cMatOther.Name = "col_cMatOther";
            this.col_cMatOther.ReadOnly = true;
            this.col_cMatOther.Width = 70;
            this.col_cABC.DataPropertyName = "cABC";
            this.col_cABC.HeaderText = "ABC";
            this.col_cABC.Name = "col_cABC";
            this.col_cABC.ReadOnly = true;
            this.col_cRemark.DataPropertyName = "cRemark";
            this.col_cRemark.HeaderText = "物料备注";
            this.col_cRemark.Name = "col_cRemark";
            this.col_cRemark.ReadOnly = true;
            this.col_cRemark.Width = 70;
            this.col_cBatchNo.DataPropertyName = "cBatchNo";
            this.col_cBatchNo.HeaderText = "批号";
            this.col_cBatchNo.Name = "col_cBatchNo";
            this.col_cBatchNo.ReadOnly = true;
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "数量";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            this.col_fQty.Width = 80;
            this.col_cUnit.DataPropertyName = "cUnit";
            this.col_cUnit.HeaderText = "单位";
            this.col_cUnit.Name = "col_cUnit";
            this.col_cUnit.ReadOnly = true;
            this.col_cUnit.Width = 50;
            this.col_cBNoIn.DataPropertyName = "cBNoIn";
            this.col_cBNoIn.HeaderText = "库存入库单号";
            this.col_cBNoIn.Name = "col_cBNoIn";
            this.col_cBNoIn.ReadOnly = true;
            this.col_nItemIn.DataPropertyName = "nItemIn";
            this.col_nItemIn.HeaderText = "库存入库单明细号";
            this.col_nItemIn.Name = "col_nItemIn";
            this.col_nItemIn.ReadOnly = true;
            this.col_fWeight.DataPropertyName = "fWeight";
            this.col_fWeight.HeaderText = "单位重量";
            this.col_fWeight.Name = "col_fWeight";
            this.col_fWeight.ReadOnly = true;
            this.col_fWeight.Width = 70;
            this.col_nBClass.DataPropertyName = "nBClass";
            this.col_nBClass.HeaderText = "单据类别";
            this.col_nBClass.Name = "col_nBClass";
            this.col_nBClass.ReadOnly = true;
            this.col_nBClass.Visible = false;
            this.col_cCSId.DataPropertyName = "cCSId";
            this.col_cCSId.HeaderText = "供应商编码";
            this.col_cCSId.Name = "col_cCSId";
            this.col_cCSId.ReadOnly = true;
            this.col_cCSId.Visible = false;
            this.col_cSupplier.DataPropertyName = "cSupplier";
            this.col_cSupplier.HeaderText = "供应商/生产商";
            this.col_cSupplier.Name = "col_cSupplier";
            this.col_cSupplier.ReadOnly = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x39b, 0x222);
            base.Controls.Add(this.grd_Data);
            base.Controls.Add(this.grp_Condition);
            base.Controls.Add(this.grp_Buttons);
            base.MinimizeBox = false;
            base.Name = "frmSelIOBillMat";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "选择出入库单物料";
            base.Load += new EventHandler(this.frmSelIOBillMat_Load);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            ((ISupportInitialize) this.bds_Data).EndInit();
            this.grp_Buttons.ResumeLayout(false);
            ((ISupportInitialize) this.grd_Data).EndInit();
            base.ResumeLayout(false);
        }

        [Description("单据类型: 1入库 2出库 3盘点 4调整单 5仓位调整 6调拨单 7 不良品单 8质检请验单 9质检取样单 10质检报告单 11入库验收单")]
        public int BClass
        {
            get
            {
                return this._BClass;
            }
            set
            {
                this._BClass = value;
                switch (this._BClass)
                {
                    case 2:
                        this.Text = "选择出库物料单据";
                        break;

                    case 4:
                        this.Text = "选择库存调整物料单据";
                        break;
                }
            }
        }

        [Description("单号")]
        public string BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                this._BillNo = value;
                this.txt_cBNo.Text = this._BillNo;
            }
        }

        [Description("处理选择出入库物料单据数据")]
        public DoSelIOStoreMatBillDataEvent DoSelIOStoreMatBillData
        {
            get
            {
                return this._DoSelIOStoreMatBillData;
            }
            set
            {
                this._DoSelIOStoreMatBillData = value;
            }
        }

        [Description("物料的编码，名称，五笔简码，拼音简码")]
        public string MName
        {
            get
            {
                return this._MName;
            }
            set
            {
                this._MName = value;
                this.txt_cName.Text = this._MName;
            }
        }
    }
}

