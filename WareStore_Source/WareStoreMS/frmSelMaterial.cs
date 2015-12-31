namespace WareStoreMS
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;

    public class frmSelMaterial : FrmSTable
    {
        private DoSelMaterialEvent _DoSelMatEvent = null;
        private string _MName = "";
        private string _Supplier = "";
        private BindingSource bds_Data;
        private Button btn_Cancel;
        private Button btn_OK;
        private Button btn_Qry;
        private Button btn_Reset;
        private CheckBox chk_DateIn;
        private ComboBox cmb_cABC;
        private ComboBox cmb_cTypeId1;
        private ComboBox cmb_ERPUnitId;
        private ComboBox cmb_ERPWHId;
        private DataGridViewTextBoxColumn col_cABC;
        private DataGridViewTextBoxColumn col_cCSId;
        private DataGridViewTextBoxColumn col_cDtlRemark;
        private DataGridViewTextBoxColumn col_cMatOther;
        private DataGridViewTextBoxColumn col_cMatQCLevel;
        private DataGridViewTextBoxColumn col_cMatStyle;
        private DataGridViewTextBoxColumn col_cMNo;
        private DataGridViewTextBoxColumn col_cName;
        private DataGridViewTextBoxColumn col_cRemark;
        private DataGridViewTextBoxColumn col_cSpec;
        private DataGridViewTextBoxColumn col_cSupplier;
        private DataGridViewTextBoxColumn col_cType1;
        private DataGridViewTextBoxColumn col_cType2;
        private DataGridViewTextBoxColumn col_cTypeId1;
        private DataGridViewTextBoxColumn col_cTypeId2;
        private DataGridViewTextBoxColumn col_cUnit;
        private DataGridViewTextBoxColumn col_fQty;
        private DataGridViewTextBoxColumn col_fQtyBox;
        private DataGridViewTextBoxColumn col_fSafeQtyDn;
        private DataGridViewTextBoxColumn col_fSafeQtyUp;
        private DataGridViewTextBoxColumn col_fWeight;
        private DataGridViewTextBoxColumn col_nKeepDay;
        private DataGridViewTextBoxColumn col_nMatClass;
        private IContainer components = null;
        private DateTimePicker dtp_From;
        private DateTimePicker dtp_To;
        private DataGridView grd_Data;
        private GroupBox grp_Buttons;
        private GroupBox grp_Condition;
        private GroupBox grpErpCondition;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ProgressBar prgMain;
        private TextBox txt_cDtlRemark;
        private TextBox txt_cMatOther;
        private TextBox txt_cMatQCLevel;
        private TextBox txt_cMatStyle;
        private TextBox txt_cName;
        private TextBox txt_cRemark;
        private TextBox txt_cSpec;
        private TextBox txt_cSupplier;

        public frmSelMaterial()
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
                this.prgMain.Maximum = this.grd_Data.SelectedRows.Count;
                this.prgMain.Minimum = 0;
                this.prgMain.Value = 0;
                this.prgMain.Visible = true;
                foreach (DataGridViewRow row in this.grd_Data.SelectedRows)
                {
                    if (this._DoSelMatEvent != null)
                    {
                        double fSafeQtyDn = 0.0;
                        if ((row.Cells["col_fSafeQtyDn"].Value != null) && (row.Cells["col_fSafeQtyDn"].Value.ToString() != ""))
                        {
                            fSafeQtyDn = Convert.ToDouble(row.Cells["col_fSafeQtyDn"].Value);
                        }
                        double fSafeQtyUp = 0.0;
                        if ((row.Cells["col_fSafeQtyUp"].Value != null) && (row.Cells["col_fSafeQtyUp"].Value.ToString() != ""))
                        {
                            fSafeQtyUp = Convert.ToDouble(row.Cells["col_fSafeQtyUp"].Value);
                        }
                        double fQtyBox = 0.0;
                        if ((row.Cells["col_fQtyBox"].Value != null) && (row.Cells["col_fQtyBox"].Value.ToString() != ""))
                        {
                            fQtyBox = Convert.ToDouble(row.Cells["col_fQtyBox"].Value);
                        }
                        double fWeight = 0.0;
                        if ((row.Cells["col_fWeight"].Value != null) && (row.Cells["col_fWeight"].Value.ToString() != ""))
                        {
                            fWeight = Convert.ToDouble(row.Cells["col_fWeight"].Value);
                        }
                        int nKeepDay = 0;
                        if ((row.Cells["col_nKeepDay"].Value != null) && (row.Cells["col_nKeepDay"].Value.ToString() != ""))
                        {
                            nKeepDay = Convert.ToInt32(row.Cells["col_nKeepDay"].Value);
                        }
                        int num6 = 0;
                        if ((row.Cells["col_nMatClass"].Value != null) && (row.Cells["col_nMatClass"].Value.ToString() != ""))
                        {
                            num6 = Convert.ToInt16(row.Cells["col_nMatClass"].Value);
                        }
                        try
                        {
                            this._DoSelMatEvent(row.Cells["col_cMNo"].Value.ToString(), row.Cells["col_cName"].Value.ToString(), row.Cells["col_cSpec"].Value.ToString(), row.Cells["col_cMatStyle"].Value.ToString(), row.Cells["col_cMatQCLevel"].Value.ToString(), row.Cells["col_cMatOther"].Value.ToString(), row.Cells["col_cRemark"].Value.ToString(), row.Cells["col_cABC"].Value.ToString(), fSafeQtyDn, fSafeQtyUp, fQtyBox, fWeight, row.Cells["col_cTypeId1"].Value.ToString(), row.Cells["col_cType1"].Value.ToString(), row.Cells["col_cTypeId2"].Value.ToString(), row.Cells["col_cType2"].Value.ToString(), row.Cells["col_cUnit"].Value.ToString(), nKeepDay, row.Cells["col_cCSId"].Value.ToString(), row.Cells["col_cSupplier"].Value.ToString(), num6, true);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                        this.prgMain.Value++;
                    }
                }
                base.Close();
            }
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            string sErr = "";
            StringBuilder builder = new StringBuilder("");
            builder.Append("select  mat.cMNo,mat.cName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark,");
            builder.Append("mat.fQtyBox,mat.fSafeQtyDn,mat.fSafeQtyUp,mat.fWeight,mat.cUnit,mat.cTypeId1,mat.cTypeId2,isnull(mat.cABC,'C') cABC,");
            builder.Append("mt.cTypeName cType1,atp.cTypeName cType2,sum(isnull(st.fQty,0)) fQty,mat.nKeepDay,isnull(st.cDtlSupplier,' ') cSupplier,isnull(st.cDtlCSId,' ') cCSId,isnull(mat.nMatClass,0) nMatClass,isnull(isnull(st.cDtlRemark,st.cStoreRemark),' ') cDtlRemark ");
            builder.Append("\tfrom TPC_Material mat ");
            builder.Append("  left join V_MaterialMatType mt on mat.cTypeId1=mt.cTypeId ");
            builder.Append("  left join V_MaterialAcntType atp on mat.cTypeId2=atp.cTypeId ");
            builder.Append("  left join V_StoreItemList st on mat.cMNo=st.cMNo ");
            builder.Append(" where 1=1 ");
            builder.Append(this.GetCondition());
            builder.Append("  group by mat.cMNo,mat.cName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark, ");
            builder.Append("  mat.fQtyBox,mat.fSafeQtyDn,mat.fSafeQtyUp,mat.fWeight,mat.cUnit,mat.cTypeId1,mat.cTypeId2,mt.cTypeName,");
            builder.Append(" atp.cTypeName,mat.cABC,mat.nKeepDay,isnull(st.cDtlSupplier,' '),isnull(st.cDtlCSId,' ') ,isnull(mat.nMatClass,0),isnull(isnull(st.cDtlRemark,st.cStoreRemark),' ')");
            DataSet set = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TPC_Material", 0, 0, "", out sErr);
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
            else
            {
                DataTable table = set.Tables["TPC_Material"];
                this.bds_Data.DataSource = table;
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.txt_cMatOther.Text = "";
            this.txt_cMatQCLevel.Text = "";
            this.txt_cMatStyle.Text = "";
            this.txt_cName.Text = "";
            this.txt_cRemark.Text = "";
            this.txt_cSpec.Text = "";
            this.cmb_cABC.SelectedIndex = -1;
            this.cmb_cTypeId1.SelectedIndex = -1;
            this.txt_cSupplier.Text = "";
            this.txt_cDtlRemark.Text = "";
            this.txt_cName.Focus();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelMaterial_Load(object sender, EventArgs e)
        {
            this.LoadBaseItem();
            this.txt_cName.Focus();
        }

        private string GetCondition()
        {
            StringBuilder builder = new StringBuilder("");
            string str = "";
            str = this.txt_cName.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ((mat.cMNo like '%" + str + "%') or (mat.cName like '%" + str + "%')  or (isnull(mat.cWBJM,'~') like '%" + str + "%')  or (isnull(mat.cPYJM,'~') like '%" + str + "%'))");
            }
            str = this.txt_cMatOther.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ( isnull(mat.cMatOther,'~') like '%" + str + "%')");
            }
            str = this.txt_cMatQCLevel.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ( isnull(mat.cMatQCLevel,'~') like '%" + str + "%')");
            }
            str = this.txt_cMatStyle.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ( isnull(mat.cMatStyle,'~') like '%" + str + "%')");
            }
            str = this.txt_cRemark.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ( isnull(mat.cRemark,'~') like '%" + str + "%')");
            }
            str = this.txt_cSpec.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ( isnull(mat.cSpec,'~') like '%" + str + "%')");
            }
            str = this.cmb_cABC.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ( isnull(mat.cABC,'~') like '%" + str + "%')");
            }
            if (((this.cmb_cTypeId1.Text.Trim() != "") && (this.cmb_cTypeId1.SelectedIndex > -1)) && (this.cmb_cTypeId1.SelectedValue != null))
            {
                builder.Append(" and ( isnull(mat.cTypeId1,'~') = '" + this.cmb_cTypeId1.SelectedValue.ToString() + "')");
            }
            str = this.txt_cSupplier.Text.Trim();
            if (str != "")
            {
                builder.Append(" and ((isnull(st.cDtlCSId,' ') like '%" + str + "%') or (isnull(st.cDtlSupplier,' ') like '%" + str + "%')  or (isnull(st.cDtlWBJM,'~') like '%" + str + "%')  or (isnull(st.cDtlPYJM,'~') like '%" + str + "%'))");
            }
            str = this.txt_cDtlRemark.Text.Trim();
            if (str != "")
            {
                builder.Append(" and (isnull(st.cDtlRemark,st.cStoreRemark) like '%" + str + "%') ");
            }
            return builder.ToString();
        }

        private void grd_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.bds_Data.Count != 0)
            {
                DataRowView current = (DataRowView) this.bds_Data.Current;
                if (current != null)
                {
                    if (this._DoSelMatEvent != null)
                    {
                        double fSafeQtyDn = 0.0;
                        if ((current["fSafeQtyDn"] != null) && (current["fSafeQtyDn"].ToString() != ""))
                        {
                            fSafeQtyDn = Convert.ToDouble(current["fSafeQtyDn"]);
                        }
                        double fSafeQtyUp = 0.0;
                        if ((current["fSafeQtyUp"] != null) && (current["fSafeQtyUp"].ToString() != ""))
                        {
                            fSafeQtyUp = Convert.ToDouble(current["fSafeQtyUp"]);
                        }
                        double fQtyBox = 0.0;
                        if ((current["fQtyBox"] != null) && (current["fQtyBox"].ToString() != ""))
                        {
                            fQtyBox = Convert.ToDouble(current["fQtyBox"]);
                        }
                        double fWeight = 0.0;
                        if ((current["fWeight"] != null) && (current["fWeight"].ToString() != ""))
                        {
                            fWeight = Convert.ToDouble(current["fWeight"]);
                        }
                        int nKeepDay = 0;
                        if ((current["nKeepDay"] != null) && (current["nKeepDay"].ToString() != ""))
                        {
                            nKeepDay = Convert.ToInt32(current["nKeepDay"]);
                        }
                        int num6 = 0;
                        if ((current["nMatClass"] != null) && (current["nMatClass"].ToString().Trim() != ""))
                        {
                            num6 = Convert.ToInt16(current["nMatClass"]);
                        }
                        try
                        {
                            this._DoSelMatEvent(current["cMNo"].ToString(), current["cName"].ToString(), current["cSpec"].ToString(), current["cMatStyle"].ToString(), current["cMatQCLevel"].ToString(), current["cMatOther"].ToString(), current["cRemark"].ToString(), current["cABC"].ToString(), fSafeQtyDn, fSafeQtyUp, fQtyBox, fWeight, current["cTypeId1"].ToString(), current["cType1"].ToString(), current["cTypeId2"].ToString(), current["cType2"].ToString(), current["cUnit"].ToString(), nKeepDay, current["cCSId"].ToString(), current["cSupplier"].ToString(), num6, true);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }
                    base.Close();
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.grp_Condition = new GroupBox();
            this.label12 = new Label();
            this.txt_cDtlRemark = new TextBox();
            this.label11 = new Label();
            this.txt_cSupplier = new TextBox();
            this.label10 = new Label();
            this.label9 = new Label();
            this.cmb_cABC = new ComboBox();
            this.label8 = new Label();
            this.cmb_cTypeId1 = new ComboBox();
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
            this.grp_Buttons = new GroupBox();
            this.prgMain = new ProgressBar();
            this.btn_Cancel = new Button();
            this.btn_OK = new Button();
            this.grd_Data = new DataGridView();
            this.col_cMNo = new DataGridViewTextBoxColumn();
            this.col_cName = new DataGridViewTextBoxColumn();
            this.col_cSpec = new DataGridViewTextBoxColumn();
            this.col_cMatStyle = new DataGridViewTextBoxColumn();
            this.col_cMatQCLevel = new DataGridViewTextBoxColumn();
            this.col_cSupplier = new DataGridViewTextBoxColumn();
            this.col_fQty = new DataGridViewTextBoxColumn();
            this.col_cUnit = new DataGridViewTextBoxColumn();
            this.col_cDtlRemark = new DataGridViewTextBoxColumn();
            this.col_fWeight = new DataGridViewTextBoxColumn();
            this.col_fQtyBox = new DataGridViewTextBoxColumn();
            this.col_fSafeQtyDn = new DataGridViewTextBoxColumn();
            this.col_fSafeQtyUp = new DataGridViewTextBoxColumn();
            this.col_cTypeId1 = new DataGridViewTextBoxColumn();
            this.col_cType1 = new DataGridViewTextBoxColumn();
            this.col_cType2 = new DataGridViewTextBoxColumn();
            this.col_cTypeId2 = new DataGridViewTextBoxColumn();
            this.col_cABC = new DataGridViewTextBoxColumn();
            this.col_nKeepDay = new DataGridViewTextBoxColumn();
            this.col_cCSId = new DataGridViewTextBoxColumn();
            this.col_nMatClass = new DataGridViewTextBoxColumn();
            this.col_cMatOther = new DataGridViewTextBoxColumn();
            this.col_cRemark = new DataGridViewTextBoxColumn();
            this.bds_Data = new BindingSource(this.components);
            this.chk_DateIn = new CheckBox();
            this.dtp_From = new DateTimePicker();
            this.dtp_To = new DateTimePicker();
            this.grpErpCondition = new GroupBox();
            this.label7 = new Label();
            this.cmb_ERPWHId = new ComboBox();
            this.label13 = new Label();
            this.cmb_ERPUnitId = new ComboBox();
            this.grp_Condition.SuspendLayout();
            this.grp_Buttons.SuspendLayout();
            ((ISupportInitialize) this.grd_Data).BeginInit();
            ((ISupportInitialize) this.bds_Data).BeginInit();
            this.grpErpCondition.SuspendLayout();
            base.SuspendLayout();
            this.grp_Condition.Controls.Add(this.label12);
            this.grp_Condition.Controls.Add(this.grpErpCondition);
            this.grp_Condition.Controls.Add(this.dtp_To);
            this.grp_Condition.Controls.Add(this.dtp_From);
            this.grp_Condition.Controls.Add(this.chk_DateIn);
            this.grp_Condition.Controls.Add(this.txt_cDtlRemark);
            this.grp_Condition.Controls.Add(this.label11);
            this.grp_Condition.Controls.Add(this.txt_cSupplier);
            this.grp_Condition.Controls.Add(this.label10);
            this.grp_Condition.Controls.Add(this.label9);
            this.grp_Condition.Controls.Add(this.cmb_cABC);
            this.grp_Condition.Controls.Add(this.label8);
            this.grp_Condition.Controls.Add(this.cmb_cTypeId1);
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
            this.grp_Condition.Size = new Size(0x487, 0x6b);
            this.grp_Condition.TabIndex = 0;
            this.grp_Condition.TabStop = false;
            this.grp_Condition.Text = "条件";
            this.label12.BackColor = SystemColors.ActiveCaption;
            this.label12.Location = new Point(14, 0x41);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x468, 1);
            this.label12.TabIndex = 0x1a;
            this.txt_cDtlRemark.Location = new Point(0x2a6, 0x29);
            this.txt_cDtlRemark.Name = "txt_cDtlRemark";
            this.txt_cDtlRemark.Size = new Size(0x95, 0x15);
            this.txt_cDtlRemark.TabIndex = 0x19;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x26b, 0x2b);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x18;
            this.label11.Text = "库存备注";
            this.txt_cSupplier.Location = new Point(0x47, 0x4a);
            this.txt_cSupplier.Name = "txt_cSupplier";
            this.txt_cSupplier.Size = new Size(0xb0, 0x15);
            this.txt_cSupplier.TabIndex = 0x17;
            this.label10.Location = new Point(12, 0x47);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 0x19);
            this.label10.TabIndex = 0x16;
            this.label10.Text = "供应商/生产厂家：";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x1ff, 0x2b);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x17, 12);
            this.label9.TabIndex = 0x15;
            this.label9.Text = "ABC";
            this.cmb_cABC.FormattingEnabled = true;
            this.cmb_cABC.Items.AddRange(new object[] { "A", "B", "C" });
            this.cmb_cABC.Location = new Point(0x219, 0x29);
            this.cmb_cABC.Name = "cmb_cABC";
            this.cmb_cABC.Size = new Size(0x42, 20);
            this.cmb_cABC.TabIndex = 20;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 0x2c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x13;
            this.label8.Text = "物料类型";
            this.cmb_cTypeId1.FormattingEnabled = true;
            this.cmb_cTypeId1.Location = new Point(0x47, 0x29);
            this.cmb_cTypeId1.Name = "cmb_cTypeId1";
            this.cmb_cTypeId1.Size = new Size(0xb0, 20);
            this.cmb_cTypeId1.TabIndex = 0x12;
            this.txt_cRemark.Location = new Point(0x135, 0x29);
            this.txt_cRemark.Name = "txt_cRemark";
            this.txt_cRemark.Size = new Size(0xa2, 0x15);
            this.txt_cRemark.TabIndex = 0x10;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xfd, 0x29);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "物料备注";
            this.txt_cMatOther.Location = new Point(0x3d0, 15);
            this.txt_cMatOther.Name = "txt_cMatOther";
            this.txt_cMatOther.Size = new Size(0xa2, 0x15);
            this.txt_cMatOther.TabIndex = 14;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x395, 15);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "其他属性";
            this.txt_cMatQCLevel.Location = new Point(0x2e3, 15);
            this.txt_cMatQCLevel.Name = "txt_cMatQCLevel";
            this.txt_cMatQCLevel.Size = new Size(0xac, 0x15);
            this.txt_cMatQCLevel.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x2ac, 0x11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "质量等级";
            this.txt_cMatStyle.Location = new Point(0x219, 15);
            this.txt_cMatStyle.Name = "txt_cMatStyle";
            this.txt_cMatStyle.Size = new Size(0x8d, 0x15);
            this.txt_cMatStyle.TabIndex = 10;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1e1, 15);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "物料款式";
            this.txt_cSpec.Location = new Point(0x135, 15);
            this.txt_cSpec.Name = "txt_cSpec";
            this.txt_cSpec.Size = new Size(0xa2, 0x15);
            this.txt_cSpec.TabIndex = 8;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xfd, 15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "物料规格";
            this.txt_cName.Location = new Point(0x47, 15);
            this.txt_cName.Name = "txt_cName";
            this.txt_cName.Size = new Size(0xb0, 0x15);
            this.txt_cName.TabIndex = 6;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "物料名称";
            this.btn_Reset.Location = new Point(0x428, 0x4b);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new Size(0x4b, 0x17);
            this.btn_Reset.TabIndex = 3;
            this.btn_Reset.Text = "重置(&R)";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new EventHandler(this.btn_Reset_Click);
            this.btn_Qry.Location = new Point(0x3d9, 0x4b);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x4b, 0x17);
            this.btn_Qry.TabIndex = 2;
            this.btn_Qry.Text = "查询(&Q)";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.btn_Qry_Click);
            this.grp_Buttons.Controls.Add(this.prgMain);
            this.grp_Buttons.Controls.Add(this.btn_Cancel);
            this.grp_Buttons.Controls.Add(this.btn_OK);
            this.grp_Buttons.Dock = DockStyle.Bottom;
            this.grp_Buttons.Location = new Point(0, 0x1d9);
            this.grp_Buttons.Name = "grp_Buttons";
            this.grp_Buttons.Size = new Size(0x487, 0x41);
            this.grp_Buttons.TabIndex = 1;
            this.grp_Buttons.TabStop = false;
            this.prgMain.Location = new Point(6, 0x27);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new Size(0x38d, 0x15);
            this.prgMain.TabIndex = 2;
            this.prgMain.Visible = false;
            this.btn_Cancel.Location = new Point(0x20a, 11);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            this.btn_OK.Location = new Point(0x12f, 11);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.grd_Data.AutoGenerateColumns = false;
            this.grd_Data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_Data.Columns.AddRange(new DataGridViewColumn[] { 
                this.col_cMNo, this.col_cName, this.col_cSpec, this.col_cMatStyle, this.col_cMatQCLevel, this.col_cSupplier, this.col_fQty, this.col_cUnit, this.col_cDtlRemark, this.col_fWeight, this.col_fQtyBox, this.col_fSafeQtyDn, this.col_fSafeQtyUp, this.col_cTypeId1, this.col_cType1, this.col_cType2, 
                this.col_cTypeId2, this.col_cABC, this.col_nKeepDay, this.col_cCSId, this.col_nMatClass, this.col_cMatOther, this.col_cRemark
             });
            this.grd_Data.DataSource = this.bds_Data;
            this.grd_Data.Dock = DockStyle.Fill;
            this.grd_Data.Location = new Point(0, 0x6b);
            this.grd_Data.Name = "grd_Data";
            this.grd_Data.ReadOnly = true;
            this.grd_Data.RowHeadersVisible = false;
            this.grd_Data.RowTemplate.Height = 0x17;
            this.grd_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grd_Data.Size = new Size(0x487, 0x16e);
            this.grd_Data.TabIndex = 2;
            this.grd_Data.CellDoubleClick += new DataGridViewCellEventHandler(this.grd_Data_CellDoubleClick);
            this.col_cMNo.DataPropertyName = "cMNo";
            this.col_cMNo.HeaderText = "物料编码";
            this.col_cMNo.Name = "col_cMNo";
            this.col_cMNo.ReadOnly = true;
            this.col_cName.DataPropertyName = "cName";
            this.col_cName.HeaderText = "物料名称";
            this.col_cName.Name = "col_cName";
            this.col_cName.ReadOnly = true;
            this.col_cSpec.DataPropertyName = "cSpec";
            this.col_cSpec.HeaderText = "规格型号";
            this.col_cSpec.Name = "col_cSpec";
            this.col_cSpec.ReadOnly = true;
            this.col_cMatStyle.DataPropertyName = "cMatStyle";
            this.col_cMatStyle.HeaderText = "款式";
            this.col_cMatStyle.Name = "col_cMatStyle";
            this.col_cMatStyle.ReadOnly = true;
            this.col_cMatQCLevel.DataPropertyName = "cMatQCLevel";
            this.col_cMatQCLevel.HeaderText = "质量等级";
            this.col_cMatQCLevel.Name = "col_cMatQCLevel";
            this.col_cMatQCLevel.ReadOnly = true;
            this.col_cMatQCLevel.Width = 70;
            this.col_cSupplier.DataPropertyName = "cSupplier";
            this.col_cSupplier.HeaderText = "供应商/生产商";
            this.col_cSupplier.Name = "col_cSupplier";
            this.col_cSupplier.ReadOnly = true;
            this.col_fQty.DataPropertyName = "fQty";
            this.col_fQty.HeaderText = "当前库存数";
            this.col_fQty.Name = "col_fQty";
            this.col_fQty.ReadOnly = true;
            this.col_cUnit.DataPropertyName = "cUnit";
            this.col_cUnit.HeaderText = "计量单位";
            this.col_cUnit.Name = "col_cUnit";
            this.col_cUnit.ReadOnly = true;
            this.col_cUnit.Width = 70;
            this.col_cDtlRemark.DataPropertyName = "cDtlRemark";
            this.col_cDtlRemark.HeaderText = "明细备注";
            this.col_cDtlRemark.Name = "col_cDtlRemark";
            this.col_cDtlRemark.ReadOnly = true;
            this.col_fWeight.DataPropertyName = "fWeight";
            this.col_fWeight.HeaderText = "单位重量";
            this.col_fWeight.Name = "col_fWeight";
            this.col_fWeight.ReadOnly = true;
            this.col_fQtyBox.DataPropertyName = "fQtyBox";
            this.col_fQtyBox.HeaderText = "每盘数量";
            this.col_fQtyBox.Name = "col_fQtyBox";
            this.col_fQtyBox.ReadOnly = true;
            this.col_fQtyBox.Width = 70;
            this.col_fSafeQtyDn.DataPropertyName = "fSaftQtyDn";
            this.col_fSafeQtyDn.HeaderText = "安全库存下限";
            this.col_fSafeQtyDn.Name = "col_fSafeQtyDn";
            this.col_fSafeQtyDn.ReadOnly = true;
            this.col_fSafeQtyUp.DataPropertyName = "fSafeQtyUp";
            this.col_fSafeQtyUp.HeaderText = "安全库存上限";
            this.col_fSafeQtyUp.Name = "col_fSafeQtyUp";
            this.col_fSafeQtyUp.ReadOnly = true;
            this.col_cTypeId1.DataPropertyName = "cTypeId1";
            this.col_cTypeId1.HeaderText = "物料类型编码";
            this.col_cTypeId1.Name = "col_cTypeId1";
            this.col_cTypeId1.ReadOnly = true;
            this.col_cType1.DataPropertyName = "cType1";
            this.col_cType1.HeaderText = "物料类型";
            this.col_cType1.Name = "col_cType1";
            this.col_cType1.ReadOnly = true;
            this.col_cType2.DataPropertyName = "cType2";
            this.col_cType2.HeaderText = "会计类型";
            this.col_cType2.Name = "col_cType2";
            this.col_cType2.ReadOnly = true;
            this.col_cTypeId2.DataPropertyName = "cTypeId2";
            this.col_cTypeId2.HeaderText = "会计类型编码";
            this.col_cTypeId2.Name = "col_cTypeId2";
            this.col_cTypeId2.ReadOnly = true;
            this.col_cABC.DataPropertyName = "cABC";
            this.col_cABC.HeaderText = "ABC属性";
            this.col_cABC.Name = "col_cABC";
            this.col_cABC.ReadOnly = true;
            this.col_nKeepDay.DataPropertyName = "nKeepDay";
            this.col_nKeepDay.HeaderText = "保质天数";
            this.col_nKeepDay.Name = "col_nKeepDay";
            this.col_nKeepDay.ReadOnly = true;
            this.col_cCSId.DataPropertyName = "cCSId";
            this.col_cCSId.HeaderText = "供应商编码";
            this.col_cCSId.Name = "col_cCSId";
            this.col_cCSId.ReadOnly = true;
            this.col_nMatClass.DataPropertyName = "nMatClass";
            this.col_nMatClass.HeaderText = "物料类别";
            this.col_nMatClass.Name = "col_nMatClass";
            this.col_nMatClass.ReadOnly = true;
            this.col_cMatOther.DataPropertyName = "cMatOther";
            this.col_cMatOther.HeaderText = "其他物料属性";
            this.col_cMatOther.Name = "col_cMatOther";
            this.col_cMatOther.ReadOnly = true;
            this.col_cRemark.DataPropertyName = "cRemark";
            this.col_cRemark.HeaderText = "物料备注";
            this.col_cRemark.Name = "col_cRemark";
            this.col_cRemark.ReadOnly = true;
            this.bds_Data.AllowNew = false;
            this.chk_DateIn.AutoSize = true;
            this.chk_DateIn.Location = new Point(0x347, 0x2b);
            this.chk_DateIn.Name = "chk_DateIn";
            this.chk_DateIn.Size = new Size(0x54, 0x10);
            this.chk_DateIn.TabIndex = 0x1b;
            this.chk_DateIn.Text = "入库时间：";
            this.chk_DateIn.UseVisualStyleBackColor = true;
            this.dtp_From.Location = new Point(0x397, 40);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new Size(0x62, 0x15);
            this.dtp_From.TabIndex = 0x1c;
            this.dtp_To.Location = new Point(0x411, 0x29);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new Size(0x62, 0x15);
            this.dtp_To.TabIndex = 0x1d;
            this.grpErpCondition.Controls.Add(this.cmb_ERPUnitId);
            this.grpErpCondition.Controls.Add(this.cmb_ERPWHId);
            this.grpErpCondition.Controls.Add(this.label13);
            this.grpErpCondition.Controls.Add(this.label7);
            this.grpErpCondition.Location = new Point(0x105, 0x42);
            this.grpErpCondition.Name = "grpErpCondition";
            this.grpErpCondition.Size = new Size(0x160, 0x26);
            this.grpErpCondition.TabIndex = 30;
            this.grpErpCondition.TabStop = false;
            this.grpErpCondition.Text = "ERP条件";
            this.grpErpCondition.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(5, 15);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 0x15;
            this.label7.Text = "仓库：";
            this.cmb_ERPWHId.FormattingEnabled = true;
            this.cmb_ERPWHId.Location = new Point(0x30, 11);
            this.cmb_ERPWHId.Name = "cmb_ERPWHId";
            this.cmb_ERPWHId.Size = new Size(0x73, 20);
            this.cmb_ERPWHId.TabIndex = 20;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(170, 0x10);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x29, 12);
            this.label13.TabIndex = 0x17;
            this.label13.Text = "厂别：";
            this.cmb_ERPUnitId.FormattingEnabled = true;
            this.cmb_ERPUnitId.Location = new Point(0xd5, 12);
            this.cmb_ERPUnitId.Name = "cmb_ERPUnitId";
            this.cmb_ERPUnitId.Size = new Size(0x85, 20);
            this.cmb_ERPUnitId.TabIndex = 0x16;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x487, 0x21a);
            base.Controls.Add(this.grd_Data);
            base.Controls.Add(this.grp_Buttons);
            base.Controls.Add(this.grp_Condition);
            base.Name = "frmSelMaterial";
            this.Text = "物料查询";
            base.Load += new EventHandler(this.frmSelMaterial_Load);
            this.grp_Condition.ResumeLayout(false);
            this.grp_Condition.PerformLayout();
            this.grp_Buttons.ResumeLayout(false);
            ((ISupportInitialize) this.grd_Data).EndInit();
            ((ISupportInitialize) this.bds_Data).EndInit();
            this.grpErpCondition.ResumeLayout(false);
            this.grpErpCondition.PerformLayout();
            base.ResumeLayout(false);
        }

        private void LoadBaseItem()
        {
            string sErr = "";
            string sSql = "select cTypeId,cTypeName from TPC_MaterialType where cTypeMode = 0 ";
            DataSet set = null;
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "MatType", 0, 0, "", out sErr);
            if ((sErr.Trim() != "0") && (sErr.Trim() != ""))
            {
                MessageBox.Show(sErr);
            }
            if (set != null)
            {
                DataTable table = set.Tables["MatType"];
                this.cmb_cTypeId1.DisplayMember = "cTypeName";
                this.cmb_cTypeId1.ValueMember = "cTypeId";
                this.cmb_cTypeId1.DataSource = table;
                table.Clear();
            }
            set.Clear();
            sSql = "select cTypeId,cTypeName from TPC_MaterialType where cTypeMode = 1 ";
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "ACNTType", 0, 0, "", out sErr);
            if ((sErr.Trim() != "0") && (sErr.Trim() != ""))
            {
                MessageBox.Show(sErr);
            }
            if (set != null)
            {
                set.Tables["ACNTType"].Clear();
            }
            set.Clear();
        }

        public DoSelMaterialEvent DoSelMatEvent
        {
            get
            {
                return this._DoSelMatEvent;
            }
            set
            {
                this._DoSelMatEvent = value;
            }
        }

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

        public string Supplier
        {
            get
            {
                return this._Supplier;
            }
            set
            {
                this._Supplier = value;
                this.txt_cSupplier.Text = this._Supplier;
            }
        }
    }
}

