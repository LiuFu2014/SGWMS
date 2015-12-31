namespace WareStoreMS
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;

    public class frmSelPosFromAndTo : FrmSTable
    {
        private BindingSource bds_Pos_From;
        private BindingSource bds_Pos_To;
        private bool bIsOpenPosFrom = false;
        private bool bIsOpenPosTo = false;
        private Button btn_Cancel;
        private Button btn_OK;
        private Button btn_Qry_From;
        private Button btn_Qry_To;
        private Button btn_Reset_From;
        private Button btn_Reset_To;
        private CheckBox chk_SelAll;
        private ComboBox cmb_Area_From;
        private ComboBox cmb_Area_To;
        private DataGridViewTextBoxColumn col_From_Area;
        private DataGridViewTextBoxColumn col_From_BatchNo;
        private DataGridViewTextBoxColumn col_From_BNOIn;
        private DataGridViewTextBoxColumn col_From_Col;
        private DataGridViewTextBoxColumn col_From_Hight;
        private DataGridViewTextBoxColumn col_From_ItemIn;
        private DataGridViewTextBoxColumn col_From_Layer;
        private DataGridViewTextBoxColumn col_From_MName;
        private DataGridViewTextBoxColumn col_From_MNo;
        private DataGridViewTextBoxColumn col_From_PalletId;
        private DataGridViewTextBoxColumn col_From_PosId;
        private DataGridViewTextBoxColumn col_From_Qty;
        private DataGridViewTextBoxColumn col_From_Row;
        private DataGridViewCheckBoxColumn col_From_Selected;
        private DataGridViewTextBoxColumn col_From_Spec;
        private DataGridViewTextBoxColumn col_From_Unit;
        private DataGridViewTextBoxColumn col_From_Weight;
        private DataGridViewTextBoxColumn col_From_WHId;
        private DataGridViewTextBoxColumn col_To_Area;
        private DataGridViewTextBoxColumn col_To_BatchNo;
        private DataGridViewTextBoxColumn col_To_BNoIn;
        private DataGridViewTextBoxColumn col_To_Col;
        private DataGridViewTextBoxColumn col_To_fQty;
        private DataGridViewTextBoxColumn col_To_Height;
        private DataGridViewTextBoxColumn col_To_ItemIn;
        private DataGridViewTextBoxColumn col_To_Layer;
        private DataGridViewTextBoxColumn col_To_MName;
        private DataGridViewTextBoxColumn col_To_MNo;
        private DataGridViewTextBoxColumn col_To_PalletId;
        private DataGridViewTextBoxColumn col_To_PosId;
        private DataGridViewTextBoxColumn col_To_Row;
        private DataGridViewTextBoxColumn col_To_Spec;
        private DataGridViewTextBoxColumn col_To_Unit;
        private DataGridViewTextBoxColumn col_To_Weight;
        private DataGridViewTextBoxColumn col_To_WHId;
        private IContainer components = null;
        private DataGridView grd_Pos_From;
        private DataGridView grd_Pos_To;
        private DataGridView grd_StockDtl_From;
        private DataGridView grd_StockDtl_To;
        private GroupBox grp_From;
        private GroupBox grp_To;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private Panel pnl_Button;
        private Panel pnl_FromTop;
        private TextBox txt_Col_From;
        private TextBox txt_Col_To;
        private TextBox txt_Layer_From;
        private TextBox txt_Layer_To;
        private TextBox txt_Mat_From;
        private TextBox txt_Mat_To;
        private TextBox txt_PltId_From;
        private TextBox txt_PltId_To;
        private TextBox txt_Row_From;
        private TextBox txt_Row_To;

        public frmSelPosFromAndTo()
        {
            this.InitializeComponent();
        }

        private bool bCheckIsOK()
        {
            bool flag = false;
            if (this.bds_Pos_From.Count == 0)
            {
                MessageBox.Show("对不起，无合盘的源货位数据！");
                return false;
            }
            if (this.grd_StockDtl_From.RowCount == 0)
            {
                MessageBox.Show("对不起，无需要合盘的物料明细数据！");
                return false;
            }
            flag = false;
            foreach (DataGridViewRow row in (IEnumerable) this.grd_StockDtl_From.Rows)
            {
                if (Convert.ToBoolean(row.Cells[this.col_From_Selected.Name].Value))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                MessageBox.Show("对不起，请选择需要合盘的库存物料数据！");
                return false;
            }
            flag = false;
            if (this.bds_Pos_To.Count == 0)
            {
                MessageBox.Show("对不起，无合盘的目标货位数据！");
                return false;
            }
            return true;
        }

        private void bds_Pos_From_PositionChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenPosFrom)
            {
                string sPalletId = "";
                if (this.bds_Pos_From.Count > 0)
                {
                    DataRowView current = (DataRowView) this.bds_Pos_From.Current;
                    if (current != null)
                    {
                        sPalletId = current["nPalletId"].ToString();
                    }
                }
                DataTable storeDtlFromByPalletId = null;
                storeDtlFromByPalletId = this.GetStoreDtlFromByPalletId(sPalletId);
                this.grd_StockDtl_From.DataSource = storeDtlFromByPalletId;
            }
        }

        private void bds_Pos_To_PositionChanged(object sender, EventArgs e)
        {
            if (this.bIsOpenPosTo)
            {
                string sPalletId = "";
                if (this.bds_Pos_To.Count > 0)
                {
                    DataRowView current = (DataRowView) this.bds_Pos_To.Current;
                    if (current != null)
                    {
                        sPalletId = current["nPalletId"].ToString();
                    }
                }
                DataTable storeDtlToByPalletId = null;
                storeDtlToByPalletId = this.GetStoreDtlToByPalletId(sPalletId);
                this.grd_StockDtl_To.DataSource = storeDtlToByPalletId;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.bCheckIsOK())
            {
                string str = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                DataRowView current = null;
                current = (DataRowView) this.bds_Pos_From.Current;
                if (current != null)
                {
                    str = current["cPosId"].ToString();
                    str3 = current["nPalletId"].ToString();
                }
                current = (DataRowView) this.bds_Pos_To.Current;
                if (current != null)
                {
                    str2 = current["cPosId"].ToString();
                    str4 = current["nPalletId"].ToString();
                }
                if ((((str.Trim() == "") || (str3.Trim() == "")) || (str2.Trim() == "")) || (str4.Trim() == ""))
                {
                    MessageBox.Show("对不起，合盘操作的源货位和托盘或者目标货位和托盘数据不能为空！");
                }
                else if (str3.Trim() == str4.Trim())
                {
                    MessageBox.Show("对不起，合盘操作的源货位和目标货位不能相同！");
                }
                else if (MessageBox.Show("您确定将 " + str + " 里的所选物料合盘到 " + str2 + " 里去吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.No)
                {
                    StringBuilder builder = new StringBuilder("");
                    string sErr = "";
                    string str6 = "";
                    str6 = DBFuns.GetNewId(base.AppInformation.SvrSocket, "TWB_BillMergePlt", "cBNo", 12, "BMP" + DateTime.Now.ToString("yyMMdd"), out sErr);
                    if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                    {
                        MessageBox.Show(sErr);
                    }
                    else if (str6.Trim() == "")
                    {
                        MessageBox.Show("对不起，获取单号失败！");
                    }
                    else
                    {
                        builder.Append("insert into TWB_BillMergePlt(cBNo,nBClass, cPosIdFrom, cPosIdTo, nPalletIdFrom, nPalletIdTo,");
                        builder.Append(" nWorkIdFrom, nWorkIdTo, nWkTskFromIsEmptyOut, nWorkFromStatus, nWorkToStatus, cCreatorId,");
                        builder.Append(" cCreator, dCreateDate, bIsChecked, dCheckDate, cChecker, bIsFinished)");
                        builder.Append("Values('" + str6 + "',12,'" + str + "','" + str2 + "','" + str3 + "','" + str4 + "',");
                        builder.Append("0,0,0,0,0,'" + base.UserInformation.UserId + "','" + base.UserInformation.UserName + "',getdate(),0,null,'',0 ) ");
                        if (!DBFuns.DoExecSql(base.AppInformation.SvrSocket, builder.ToString(), "dCreateDate,dCheckDate", out sErr))
                        {
                            MessageBox.Show("新建主表数据时，失败：" + sErr);
                        }
                        else
                        {
                            int num = 0;
                            this.grd_StockDtl_From.EndEdit();
                            foreach (DataGridViewRow row in (IEnumerable) this.grd_StockDtl_From.Rows)
                            {
                                if (Convert.ToBoolean(row.Cells[this.col_From_Selected.Name].Value))
                                {
                                    num++;
                                    builder.Remove(0, builder.Length);
                                    builder.Append("insert into TWB_BillMergePltDtl(cBNo,nItem,nPalletId,cPosId,cBoxId,cMNo,cBatchNo,cBNoIn,nItemIn,dProdDate,nQCStatus,");
                                    builder.Append("dValiDate,cUNit,bIsSample,cCSId,cSuppler,cDtlRemark,nStatus,cWHIdErp,cWHId,fQty,fFinished,bIsOut,cRemark)");
                                    builder.Append("select '" + str6 + "'," + num.ToString() + ",nPalletId,cPosId,cBoxId,cMNo,cBatchNo,cBNoIn,nItemIn,dProdDate,nQCStatus,");
                                    builder.Append("dBadDate,cUnit,0,cDtlCSId,cDtlSupplier,cDtlRemark,1,cWHIdErp,cWHId,fQty,0,1,'' cRemark from V_StoreItemList ");
                                    builder.Append(" where nPalletId='" + str3.Trim() + "' and cPosId='" + str.Trim() + "' and cMNo='" + row.Cells[this.col_From_MNo.Name].Value.ToString() + "'");
                                    builder.Append(" and cBNoIn='" + row.Cells[this.col_From_BNOIn.Name].Value.ToString() + "' ");
                                    builder.Append(" and nItemIn=" + row.Cells[this.col_From_ItemIn.Name].Value.ToString());
                                    if (!DBFuns.DoExecSql(base.AppInformation.SvrSocket, builder.ToString(), "", out sErr))
                                    {
                                        MessageBox.Show("新建明细表数据时，失败：" + sErr);
                                        MessageBox.Show("成功增加了 " + ((num - 1)).ToString() + " 条明细数据！");
                                        return;
                                    }
                                }
                            }
                            MessageBox.Show("保存合盘单据数据成功！");
                            base.Close();
                        }
                    }
                }
            }
        }

        private void btn_Qry_From_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("select * from V_WARECELLSTATUS where 1=1 ");
            if ((this.cmb_Area_From.Text.Trim() != "") && (this.cmb_Area_From.SelectedValue != null))
            {
                builder.Append(" and cAreaId = '" + this.cmb_Area_From.SelectedValue.ToString() + "'");
            }
            if (this.txt_Row_From.Text.Trim() != "")
            {
                builder.Append(" and nRow=" + this.txt_Row_From.Text.Trim());
            }
            if (this.txt_Col_From.Text.Trim() != "")
            {
                builder.Append(" and nCol=" + this.txt_Col_From.Text.Trim());
            }
            if (this.txt_Layer_From.Text.Trim() != "")
            {
                builder.Append(" and nLayer=" + this.txt_Layer_From.Text.Trim());
            }
            if (this.txt_PltId_From.Text.Trim() != "")
            {
                builder.Append(" and isnull(nPalletId,' ') like '%" + this.txt_PltId_From.Text.Trim() + "%'");
            }
            if (this.txt_Mat_From.Text.Trim() != "")
            {
                builder.Append(" and ( isnull(nPalletId,' ') in ( select distinct nPalletId from V_StoreItemList where (cMNo like '%" + this.txt_Mat_From.Text.Trim() + "%') or (cMName like '%" + this.txt_Mat_From.Text.Trim() + "%') or (cPYJM like '%" + this.txt_Mat_From.Text.Trim() + "%')  or (cWBJM like '%" + this.txt_Mat_From.Text.Trim() + "%') ))");
            }
            DataTable table = null;
            string sErr = "";
            table = DBFuns.GetDataTableBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "PosList", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            this.bIsOpenPosFrom = false;
            this.bds_Pos_From.DataSource = table;
            this.grd_Pos_From.DataSource = this.bds_Pos_From;
            this.bIsOpenPosFrom = true;
        }

        private void btn_Qry_To_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder("select * from V_WARECELLSTATUS where 1=1 ");
            if ((this.cmb_Area_To.Text.Trim() != "") && (this.cmb_Area_To.SelectedValue != null))
            {
                builder.Append(" and cAreaId = '" + this.cmb_Area_To.SelectedValue.ToString() + "'");
            }
            if (this.txt_Row_To.Text.Trim() != "")
            {
                builder.Append(" and nRow=" + this.txt_Row_To.Text.Trim());
            }
            if (this.txt_Col_To.Text.Trim() != "")
            {
                builder.Append(" and nCol=" + this.txt_Col_To.Text.Trim());
            }
            if (this.txt_Layer_To.Text.Trim() != "")
            {
                builder.Append(" and nLayer=" + this.txt_Layer_To.Text.Trim());
            }
            if (this.txt_PltId_To.Text.Trim() != "")
            {
                builder.Append(" and isnull(nPalletId,' ') like '%" + this.txt_PltId_To.Text.Trim() + "%'");
            }
            if (this.txt_Mat_To.Text.Trim() != "")
            {
                builder.Append(" and ( isnull(nPalletId,' ') in ( select distinct nPalletId from V_StoreItemList where (cMNo like '%" + this.txt_Mat_To.Text.Trim() + "%') or (cMName like '%" + this.txt_Mat_To.Text.Trim() + "%') or (cPYJM like '%" + this.txt_Mat_To.Text.Trim() + "%')  or (cWBJM like '%" + this.txt_Mat_To.Text.Trim() + "%') ))");
            }
            DataTable table = null;
            string sErr = "";
            table = DBFuns.GetDataTableBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "PosListTo", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            this.bIsOpenPosTo = false;
            this.bds_Pos_To.DataSource = table;
            this.grd_Pos_To.DataSource = this.bds_Pos_To;
            this.bIsOpenPosTo = true;
        }

        private void btn_Reset_From_Click(object sender, EventArgs e)
        {
            this.cmb_Area_From.SelectedIndex = -1;
            this.txt_Col_From.Text = "0";
            this.txt_Layer_From.Text = "0";
            this.txt_Row_From.Text = "0";
            this.txt_PltId_From.Text = "";
            this.txt_Mat_From.Text = "";
            this.cmb_Area_From.Focus();
        }

        private void btn_Reset_To_Click(object sender, EventArgs e)
        {
            this.cmb_Area_To.SelectedIndex = -1;
            this.txt_Col_To.Text = "0";
            this.txt_Layer_To.Text = "0";
            this.txt_Row_To.Text = "0";
            this.txt_PltId_To.Text = "";
            this.txt_Mat_To.Text = "";
            this.cmb_Area_To.Focus();
        }

        private void chk_SelAll_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.chk_SelAll.Checked;
            foreach (DataGridViewRow row in (IEnumerable) this.grd_StockDtl_From.Rows)
            {
                row.Cells[this.col_From_Selected.Name].Value = flag;
            }
            this.grd_StockDtl_From.EndEdit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelPosFromAndTo_Load(object sender, EventArgs e)
        {
            this.grd_Pos_From.AutoGenerateColumns = false;
            this.grd_Pos_To.AutoGenerateColumns = false;
            this.grd_StockDtl_From.AutoGenerateColumns = false;
            this.grd_StockDtl_To.AutoGenerateColumns = false;
        }

        private DataTable GetStoreDtlFromByPalletId(string sPalletId)
        {
            string sSql = "select * from V_StoreItemList where nPalletId='" + sPalletId.Trim() + "'";
            DataTable table = null;
            string sErr = "";
            table = DBFuns.GetDataTableBySql(base.AppInformation.SvrSocket, false, sSql, "tbStoreDtlFrom", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            return table;
        }

        private DataTable GetStoreDtlToByPalletId(string sPalletId)
        {
            string sSql = "select * from V_StoreItemList where nPalletId='" + sPalletId.Trim() + "'";
            DataTable table = null;
            string sErr = "";
            table = DBFuns.GetDataTableBySql(base.AppInformation.SvrSocket, false, sSql, "tbStoreDtlTo", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            return table;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.pnl_Button = new Panel();
            this.btn_Cancel = new Button();
            this.btn_OK = new Button();
            this.grp_From = new GroupBox();
            this.grd_Pos_From = new DataGridView();
            this.col_From_WHId = new DataGridViewTextBoxColumn();
            this.col_From_PosId = new DataGridViewTextBoxColumn();
            this.col_From_PalletId = new DataGridViewTextBoxColumn();
            this.col_From_Row = new DataGridViewTextBoxColumn();
            this.col_From_Col = new DataGridViewTextBoxColumn();
            this.col_From_Layer = new DataGridViewTextBoxColumn();
            this.col_From_Area = new DataGridViewTextBoxColumn();
            this.col_From_Hight = new DataGridViewTextBoxColumn();
            this.col_From_Weight = new DataGridViewTextBoxColumn();
            this.label2 = new Label();
            this.grd_StockDtl_From = new DataGridView();
            this.col_From_Selected = new DataGridViewCheckBoxColumn();
            this.col_From_MNo = new DataGridViewTextBoxColumn();
            this.col_From_MName = new DataGridViewTextBoxColumn();
            this.col_From_Spec = new DataGridViewTextBoxColumn();
            this.col_From_Qty = new DataGridViewTextBoxColumn();
            this.col_From_Unit = new DataGridViewTextBoxColumn();
            this.col_From_BatchNo = new DataGridViewTextBoxColumn();
            this.col_From_BNOIn = new DataGridViewTextBoxColumn();
            this.col_From_ItemIn = new DataGridViewTextBoxColumn();
            this.label1 = new Label();
            this.pnl_FromTop = new Panel();
            this.txt_Mat_From = new TextBox();
            this.txt_PltId_From = new TextBox();
            this.txt_Layer_From = new TextBox();
            this.txt_Col_From = new TextBox();
            this.txt_Row_From = new TextBox();
            this.cmb_Area_From = new ComboBox();
            this.btn_Reset_From = new Button();
            this.btn_Qry_From = new Button();
            this.label8 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.grp_To = new GroupBox();
            this.grd_Pos_To = new DataGridView();
            this.col_To_WHId = new DataGridViewTextBoxColumn();
            this.col_To_PosId = new DataGridViewTextBoxColumn();
            this.col_To_PalletId = new DataGridViewTextBoxColumn();
            this.col_To_Row = new DataGridViewTextBoxColumn();
            this.col_To_Col = new DataGridViewTextBoxColumn();
            this.col_To_Layer = new DataGridViewTextBoxColumn();
            this.col_To_Area = new DataGridViewTextBoxColumn();
            this.col_To_Height = new DataGridViewTextBoxColumn();
            this.col_To_Weight = new DataGridViewTextBoxColumn();
            this.label9 = new Label();
            this.grd_StockDtl_To = new DataGridView();
            this.col_To_MNo = new DataGridViewTextBoxColumn();
            this.col_To_MName = new DataGridViewTextBoxColumn();
            this.col_To_Spec = new DataGridViewTextBoxColumn();
            this.col_To_fQty = new DataGridViewTextBoxColumn();
            this.col_To_Unit = new DataGridViewTextBoxColumn();
            this.col_To_BatchNo = new DataGridViewTextBoxColumn();
            this.col_To_BNoIn = new DataGridViewTextBoxColumn();
            this.col_To_ItemIn = new DataGridViewTextBoxColumn();
            this.label10 = new Label();
            this.panel1 = new Panel();
            this.txt_Mat_To = new TextBox();
            this.txt_PltId_To = new TextBox();
            this.txt_Layer_To = new TextBox();
            this.txt_Col_To = new TextBox();
            this.txt_Row_To = new TextBox();
            this.cmb_Area_To = new ComboBox();
            this.btn_Reset_To = new Button();
            this.btn_Qry_To = new Button();
            this.label11 = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.label14 = new Label();
            this.label15 = new Label();
            this.label16 = new Label();
            this.bds_Pos_From = new BindingSource(this.components);
            this.chk_SelAll = new CheckBox();
            this.bds_Pos_To = new BindingSource(this.components);
            this.pnl_Button.SuspendLayout();
            this.grp_From.SuspendLayout();
            ((ISupportInitialize) this.grd_Pos_From).BeginInit();
            ((ISupportInitialize) this.grd_StockDtl_From).BeginInit();
            this.pnl_FromTop.SuspendLayout();
            this.grp_To.SuspendLayout();
            ((ISupportInitialize) this.grd_Pos_To).BeginInit();
            ((ISupportInitialize) this.grd_StockDtl_To).BeginInit();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.bds_Pos_From).BeginInit();
            ((ISupportInitialize) this.bds_Pos_To).BeginInit();
            base.SuspendLayout();
            this.pnl_Button.Controls.Add(this.chk_SelAll);
            this.pnl_Button.Controls.Add(this.btn_Cancel);
            this.pnl_Button.Controls.Add(this.btn_OK);
            this.pnl_Button.Dock = DockStyle.Bottom;
            this.pnl_Button.Location = new Point(0, 0x1e8);
            this.pnl_Button.Name = "pnl_Button";
            this.pnl_Button.Size = new Size(0x44c, 0x20);
            this.pnl_Button.TabIndex = 2;
            this.btn_Cancel.Location = new Point(0x207, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_OK.Location = new Point(0x18b, 6);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x4b, 0x17);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.btn_OK_Click);
            this.grp_From.Controls.Add(this.grd_Pos_From);
            this.grp_From.Controls.Add(this.label2);
            this.grp_From.Controls.Add(this.grd_StockDtl_From);
            this.grp_From.Controls.Add(this.label1);
            this.grp_From.Controls.Add(this.pnl_FromTop);
            this.grp_From.Dock = DockStyle.Left;
            this.grp_From.Location = new Point(0, 0);
            this.grp_From.Name = "grp_From";
            this.grp_From.Size = new Size(0x228, 0x1e8);
            this.grp_From.TabIndex = 3;
            this.grp_From.TabStop = false;
            this.grp_From.Text = "源货位";
            this.grd_Pos_From.AllowUserToAddRows = false;
            this.grd_Pos_From.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_Pos_From.Columns.AddRange(new DataGridViewColumn[] { this.col_From_WHId, this.col_From_PosId, this.col_From_PalletId, this.col_From_Row, this.col_From_Col, this.col_From_Layer, this.col_From_Area, this.col_From_Hight, this.col_From_Weight });
            this.grd_Pos_From.Dock = DockStyle.Fill;
            this.grd_Pos_From.Location = new Point(3, 0x6b);
            this.grd_Pos_From.Name = "grd_Pos_From";
            this.grd_Pos_From.RowHeadersVisible = false;
            this.grd_Pos_From.RowTemplate.Height = 0x17;
            this.grd_Pos_From.Size = new Size(0x222, 0x10a);
            this.grd_Pos_From.TabIndex = 7;
            this.col_From_WHId.DataPropertyName = "cWHId";
            this.col_From_WHId.HeaderText = "仓库号";
            this.col_From_WHId.Name = "col_From_WHId";
            this.col_From_WHId.Width = 0x41;
            this.col_From_PosId.DataPropertyName = "cPosId";
            this.col_From_PosId.HeaderText = "货位号";
            this.col_From_PosId.Name = "col_From_PosId";
            this.col_From_PalletId.DataPropertyName = "nPalletId";
            this.col_From_PalletId.HeaderText = "托盘号";
            this.col_From_PalletId.Name = "col_From_PalletId";
            this.col_From_PalletId.Width = 0x41;
            this.col_From_Row.DataPropertyName = "nRow";
            this.col_From_Row.HeaderText = "行";
            this.col_From_Row.Name = "col_From_Row";
            this.col_From_Row.Width = 30;
            this.col_From_Col.DataPropertyName = "nCol";
            this.col_From_Col.HeaderText = "列";
            this.col_From_Col.Name = "col_From_Col";
            this.col_From_Col.Width = 30;
            this.col_From_Layer.DataPropertyName = "nLayer";
            this.col_From_Layer.HeaderText = "层";
            this.col_From_Layer.Name = "col_From_Layer";
            this.col_From_Layer.Width = 30;
            this.col_From_Area.DataPropertyName = "cAreaDesc";
            this.col_From_Area.HeaderText = "货区";
            this.col_From_Area.Name = "col_From_Area";
            this.col_From_Hight.DataPropertyName = "fHight";
            this.col_From_Hight.HeaderText = "高度";
            this.col_From_Hight.Name = "col_From_Hight";
            this.col_From_Hight.Width = 60;
            this.col_From_Weight.DataPropertyName = "fWeight";
            this.col_From_Weight.HeaderText = "重量";
            this.col_From_Weight.Name = "col_From_Weight";
            this.col_From_Weight.Width = 60;
            this.label2.AutoSize = true;
            this.label2.Dock = DockStyle.Bottom;
            this.label2.Location = new Point(3, 0x175);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "库存物料：";
            this.grd_StockDtl_From.AllowUserToAddRows = false;
            this.grd_StockDtl_From.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_StockDtl_From.Columns.AddRange(new DataGridViewColumn[] { this.col_From_Selected, this.col_From_MNo, this.col_From_MName, this.col_From_Spec, this.col_From_Qty, this.col_From_Unit, this.col_From_BatchNo, this.col_From_BNOIn, this.col_From_ItemIn });
            this.grd_StockDtl_From.Dock = DockStyle.Bottom;
            this.grd_StockDtl_From.Location = new Point(3, 0x181);
            this.grd_StockDtl_From.Name = "grd_StockDtl_From";
            this.grd_StockDtl_From.RowHeadersVisible = false;
            this.grd_StockDtl_From.RowTemplate.Height = 0x17;
            this.grd_StockDtl_From.Size = new Size(0x222, 100);
            this.grd_StockDtl_From.TabIndex = 5;
            this.col_From_Selected.HeaderText = "选择";
            this.col_From_Selected.Name = "col_From_Selected";
            this.col_From_Selected.Width = 0x2d;
            this.col_From_MNo.DataPropertyName = "cMNo";
            this.col_From_MNo.HeaderText = "物料号";
            this.col_From_MNo.Name = "col_From_MNo";
            this.col_From_MNo.Width = 80;
            this.col_From_MName.DataPropertyName = "cMName";
            this.col_From_MName.HeaderText = "物料名称";
            this.col_From_MName.Name = "col_From_MName";
            this.col_From_Spec.DataPropertyName = "cSpec";
            this.col_From_Spec.HeaderText = "规格型号";
            this.col_From_Spec.Name = "col_From_Spec";
            this.col_From_Spec.Width = 70;
            this.col_From_Qty.DataPropertyName = "fQty";
            this.col_From_Qty.HeaderText = "数量";
            this.col_From_Qty.Name = "col_From_Qty";
            this.col_From_Qty.Width = 70;
            this.col_From_Unit.DataPropertyName = "cUnit";
            this.col_From_Unit.HeaderText = "单位";
            this.col_From_Unit.Name = "col_From_Unit";
            this.col_From_Unit.Width = 60;
            this.col_From_BatchNo.DataPropertyName = "cBatchNo";
            this.col_From_BatchNo.HeaderText = "批号";
            this.col_From_BatchNo.Name = "col_From_BatchNo";
            this.col_From_BatchNo.Width = 70;
            this.col_From_BNOIn.DataPropertyName = "cBNoIn";
            this.col_From_BNOIn.HeaderText = "入库单号";
            this.col_From_BNOIn.Name = "col_From_BNOIn";
            this.col_From_BNOIn.Visible = false;
            this.col_From_ItemIn.DataPropertyName = "nItemIn";
            this.col_From_ItemIn.HeaderText = "入库单序号";
            this.col_From_ItemIn.Name = "col_From_ItemIn";
            this.col_From_ItemIn.Visible = false;
            this.label1.AutoSize = true;
            this.label1.Dock = DockStyle.Top;
            this.label1.Location = new Point(3, 0x5f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "货位：";
            this.pnl_FromTop.Controls.Add(this.txt_Mat_From);
            this.pnl_FromTop.Controls.Add(this.txt_PltId_From);
            this.pnl_FromTop.Controls.Add(this.txt_Layer_From);
            this.pnl_FromTop.Controls.Add(this.txt_Col_From);
            this.pnl_FromTop.Controls.Add(this.txt_Row_From);
            this.pnl_FromTop.Controls.Add(this.cmb_Area_From);
            this.pnl_FromTop.Controls.Add(this.btn_Reset_From);
            this.pnl_FromTop.Controls.Add(this.btn_Qry_From);
            this.pnl_FromTop.Controls.Add(this.label8);
            this.pnl_FromTop.Controls.Add(this.label7);
            this.pnl_FromTop.Controls.Add(this.label6);
            this.pnl_FromTop.Controls.Add(this.label5);
            this.pnl_FromTop.Controls.Add(this.label4);
            this.pnl_FromTop.Controls.Add(this.label3);
            this.pnl_FromTop.Dock = DockStyle.Top;
            this.pnl_FromTop.Location = new Point(3, 0x11);
            this.pnl_FromTop.Name = "pnl_FromTop";
            this.pnl_FromTop.Size = new Size(0x222, 0x4e);
            this.pnl_FromTop.TabIndex = 0;
            this.txt_Mat_From.Location = new Point(0x29, 0x2d);
            this.txt_Mat_From.Name = "txt_Mat_From";
            this.txt_Mat_From.Size = new Size(0xb3, 0x15);
            this.txt_Mat_From.TabIndex = 13;
            this.txt_PltId_From.Location = new Point(0x18e, 14);
            this.txt_PltId_From.Name = "txt_PltId_From";
            this.txt_PltId_From.Size = new Size(0x76, 0x15);
            this.txt_PltId_From.TabIndex = 12;
            this.txt_Layer_From.Location = new Point(310, 14);
            this.txt_Layer_From.Name = "txt_Layer_From";
            this.txt_Layer_From.Size = new Size(30, 0x15);
            this.txt_Layer_From.TabIndex = 11;
            this.txt_Col_From.Location = new Point(0xf9, 14);
            this.txt_Col_From.Name = "txt_Col_From";
            this.txt_Col_From.Size = new Size(30, 0x15);
            this.txt_Col_From.TabIndex = 10;
            this.txt_Row_From.Location = new Point(190, 14);
            this.txt_Row_From.Name = "txt_Row_From";
            this.txt_Row_From.Size = new Size(30, 0x15);
            this.txt_Row_From.TabIndex = 9;
            this.cmb_Area_From.FormattingEnabled = true;
            this.cmb_Area_From.Location = new Point(0x29, 14);
            this.cmb_Area_From.Name = "cmb_Area_From";
            this.cmb_Area_From.Size = new Size(0x79, 20);
            this.cmb_Area_From.TabIndex = 8;
            this.btn_Reset_From.Location = new Point(0x1b9, 0x30);
            this.btn_Reset_From.Name = "btn_Reset_From";
            this.btn_Reset_From.Size = new Size(0x4b, 0x17);
            this.btn_Reset_From.TabIndex = 7;
            this.btn_Reset_From.Text = "重置(&R)";
            this.btn_Reset_From.UseVisualStyleBackColor = true;
            this.btn_Reset_From.Click += new EventHandler(this.btn_Reset_From_Click);
            this.btn_Qry_From.Location = new Point(360, 0x30);
            this.btn_Qry_From.Name = "btn_Qry_From";
            this.btn_Qry_From.Size = new Size(0x4b, 0x17);
            this.btn_Qry_From.TabIndex = 6;
            this.btn_Qry_From.Text = "查询(&F)";
            this.btn_Qry_From.UseVisualStyleBackColor = true;
            this.btn_Qry_From.Click += new EventHandler(this.btn_Qry_From_Click);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(5, 0x2d);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x29, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "物料：";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x15f, 14);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "托盘号：";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x11d, 14);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "层：";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xe0, 14);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "列：";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xa6, 14);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "行：";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(5, 14);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "货区：";
            this.grp_To.Controls.Add(this.grd_Pos_To);
            this.grp_To.Controls.Add(this.label9);
            this.grp_To.Controls.Add(this.grd_StockDtl_To);
            this.grp_To.Controls.Add(this.label10);
            this.grp_To.Controls.Add(this.panel1);
            this.grp_To.Dock = DockStyle.Right;
            this.grp_To.Location = new Point(0x22e, 0);
            this.grp_To.Name = "grp_To";
            this.grp_To.Size = new Size(0x21e, 0x1e8);
            this.grp_To.TabIndex = 4;
            this.grp_To.TabStop = false;
            this.grp_To.Text = "目标货位";
            this.grd_Pos_To.AllowUserToAddRows = false;
            this.grd_Pos_To.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_Pos_To.Columns.AddRange(new DataGridViewColumn[] { this.col_To_WHId, this.col_To_PosId, this.col_To_PalletId, this.col_To_Row, this.col_To_Col, this.col_To_Layer, this.col_To_Area, this.col_To_Height, this.col_To_Weight });
            this.grd_Pos_To.Dock = DockStyle.Fill;
            this.grd_Pos_To.Location = new Point(3, 0x6b);
            this.grd_Pos_To.Name = "grd_Pos_To";
            this.grd_Pos_To.RowHeadersVisible = false;
            this.grd_Pos_To.RowTemplate.Height = 0x17;
            this.grd_Pos_To.Size = new Size(0x218, 0x10a);
            this.grd_Pos_To.TabIndex = 10;
            this.col_To_WHId.DataPropertyName = "cWHId";
            this.col_To_WHId.HeaderText = "仓库号";
            this.col_To_WHId.Name = "col_To_WHId";
            this.col_To_WHId.Width = 0x41;
            this.col_To_PosId.DataPropertyName = "cPosId";
            this.col_To_PosId.HeaderText = "货位号";
            this.col_To_PosId.Name = "col_To_PosId";
            this.col_To_PalletId.DataPropertyName = "nPalletId";
            this.col_To_PalletId.HeaderText = "托盘号";
            this.col_To_PalletId.Name = "col_To_PalletId";
            this.col_To_PalletId.Width = 0x41;
            this.col_To_Row.DataPropertyName = "nRow";
            this.col_To_Row.HeaderText = "行";
            this.col_To_Row.Name = "col_To_Row";
            this.col_To_Row.Width = 30;
            this.col_To_Col.DataPropertyName = "nCol";
            this.col_To_Col.HeaderText = "列";
            this.col_To_Col.Name = "col_To_Col";
            this.col_To_Col.Width = 30;
            this.col_To_Layer.DataPropertyName = "nLayer";
            this.col_To_Layer.HeaderText = "层";
            this.col_To_Layer.Name = "col_To_Layer";
            this.col_To_Layer.Width = 30;
            this.col_To_Area.DataPropertyName = "cAreaDesc";
            this.col_To_Area.HeaderText = "货区";
            this.col_To_Area.Name = "col_To_Area";
            this.col_To_Height.DataPropertyName = "fHight";
            this.col_To_Height.HeaderText = "高度";
            this.col_To_Height.Name = "col_To_Height";
            this.col_To_Height.Width = 60;
            this.col_To_Weight.DataPropertyName = "fWeight";
            this.col_To_Weight.HeaderText = "重量";
            this.col_To_Weight.Name = "col_To_Weight";
            this.col_To_Weight.Width = 60;
            this.label9.AutoSize = true;
            this.label9.Dock = DockStyle.Bottom;
            this.label9.Location = new Point(3, 0x175);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "库存物料：";
            this.grd_StockDtl_To.AllowUserToAddRows = false;
            this.grd_StockDtl_To.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_StockDtl_To.Columns.AddRange(new DataGridViewColumn[] { this.col_To_MNo, this.col_To_MName, this.col_To_Spec, this.col_To_fQty, this.col_To_Unit, this.col_To_BatchNo, this.col_To_BNoIn, this.col_To_ItemIn });
            this.grd_StockDtl_To.Dock = DockStyle.Bottom;
            this.grd_StockDtl_To.Location = new Point(3, 0x181);
            this.grd_StockDtl_To.Name = "grd_StockDtl_To";
            this.grd_StockDtl_To.RowHeadersVisible = false;
            this.grd_StockDtl_To.RowTemplate.Height = 0x17;
            this.grd_StockDtl_To.Size = new Size(0x218, 100);
            this.grd_StockDtl_To.TabIndex = 8;
            this.col_To_MNo.DataPropertyName = "cMNo";
            this.col_To_MNo.HeaderText = "物料号";
            this.col_To_MNo.Name = "col_To_MNo";
            this.col_To_MNo.Width = 80;
            this.col_To_MName.DataPropertyName = "cMName";
            this.col_To_MName.HeaderText = "物料名称";
            this.col_To_MName.Name = "col_To_MName";
            this.col_To_Spec.DataPropertyName = "cSpec";
            this.col_To_Spec.HeaderText = "规格型号";
            this.col_To_Spec.Name = "col_To_Spec";
            this.col_To_Spec.Width = 70;
            this.col_To_fQty.DataPropertyName = "fQty";
            this.col_To_fQty.HeaderText = "数量";
            this.col_To_fQty.Name = "col_To_fQty";
            this.col_To_fQty.Width = 70;
            this.col_To_Unit.DataPropertyName = "cUnit";
            this.col_To_Unit.HeaderText = "单位";
            this.col_To_Unit.Name = "col_To_Unit";
            this.col_To_Unit.Width = 60;
            this.col_To_BatchNo.DataPropertyName = "cBatchNo";
            this.col_To_BatchNo.HeaderText = "批号";
            this.col_To_BatchNo.Name = "col_To_BatchNo";
            this.col_To_BatchNo.Width = 70;
            this.col_To_BNoIn.DataPropertyName = "cBNoIn";
            this.col_To_BNoIn.HeaderText = "入库单号";
            this.col_To_BNoIn.Name = "col_To_BNoIn";
            this.col_To_BNoIn.Visible = false;
            this.col_To_ItemIn.DataPropertyName = "nItemIn";
            this.col_To_ItemIn.HeaderText = "入库单序号";
            this.col_To_ItemIn.Name = "col_To_ItemIn";
            this.col_To_ItemIn.Visible = false;
            this.label10.AutoSize = true;
            this.label10.Dock = DockStyle.Top;
            this.label10.Location = new Point(3, 0x5f);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x29, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "货位：";
            this.panel1.Controls.Add(this.txt_Mat_To);
            this.panel1.Controls.Add(this.txt_PltId_To);
            this.panel1.Controls.Add(this.txt_Layer_To);
            this.panel1.Controls.Add(this.txt_Col_To);
            this.panel1.Controls.Add(this.txt_Row_To);
            this.panel1.Controls.Add(this.cmb_Area_To);
            this.panel1.Controls.Add(this.btn_Reset_To);
            this.panel1.Controls.Add(this.btn_Qry_To);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(3, 0x11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x218, 0x4e);
            this.panel1.TabIndex = 0;
            this.txt_Mat_To.Location = new Point(0x29, 0x2d);
            this.txt_Mat_To.Name = "txt_Mat_To";
            this.txt_Mat_To.Size = new Size(0xb3, 0x15);
            this.txt_Mat_To.TabIndex = 13;
            this.txt_PltId_To.Location = new Point(0x188, 14);
            this.txt_PltId_To.Name = "txt_PltId_To";
            this.txt_PltId_To.Size = new Size(110, 0x15);
            this.txt_PltId_To.TabIndex = 12;
            this.txt_Layer_To.Location = new Point(310, 13);
            this.txt_Layer_To.Name = "txt_Layer_To";
            this.txt_Layer_To.Size = new Size(30, 0x15);
            this.txt_Layer_To.TabIndex = 11;
            this.txt_Col_To.Location = new Point(0xf9, 13);
            this.txt_Col_To.Name = "txt_Col_To";
            this.txt_Col_To.Size = new Size(30, 0x15);
            this.txt_Col_To.TabIndex = 10;
            this.txt_Row_To.Location = new Point(190, 14);
            this.txt_Row_To.Name = "txt_Row_To";
            this.txt_Row_To.Size = new Size(30, 0x15);
            this.txt_Row_To.TabIndex = 9;
            this.cmb_Area_To.FormattingEnabled = true;
            this.cmb_Area_To.Location = new Point(0x29, 14);
            this.cmb_Area_To.Name = "cmb_Area_To";
            this.cmb_Area_To.Size = new Size(0x79, 20);
            this.cmb_Area_To.TabIndex = 8;
            this.btn_Reset_To.Location = new Point(0x1ab, 0x2d);
            this.btn_Reset_To.Name = "btn_Reset_To";
            this.btn_Reset_To.Size = new Size(0x4b, 0x17);
            this.btn_Reset_To.TabIndex = 7;
            this.btn_Reset_To.Text = "重置(&R)";
            this.btn_Reset_To.UseVisualStyleBackColor = true;
            this.btn_Reset_To.Click += new EventHandler(this.btn_Reset_To_Click);
            this.btn_Qry_To.Location = new Point(0x15a, 0x2d);
            this.btn_Qry_To.Name = "btn_Qry_To";
            this.btn_Qry_To.Size = new Size(0x4b, 0x17);
            this.btn_Qry_To.TabIndex = 6;
            this.btn_Qry_To.Text = "查询(&F)";
            this.btn_Qry_To.UseVisualStyleBackColor = true;
            this.btn_Qry_To.Click += new EventHandler(this.btn_Qry_To_Click);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(5, 0x30);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x29, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "物料：";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x158, 0x10);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x35, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "托盘号：";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x11d, 0x11);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x1d, 12);
            this.label13.TabIndex = 3;
            this.label13.Text = "层：";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0xe0, 14);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "列：";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0xa6, 0x11);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x1d, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "行：";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(5, 0x11);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x29, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "货区：";
            this.bds_Pos_From.PositionChanged += new EventHandler(this.bds_Pos_From_PositionChanged);
            this.chk_SelAll.AutoSize = true;
            this.chk_SelAll.Location = new Point(12, 6);
            this.chk_SelAll.Name = "chk_SelAll";
            this.chk_SelAll.Size = new Size(0x30, 0x10);
            this.chk_SelAll.TabIndex = 2;
            this.chk_SelAll.Text = "全部";
            this.chk_SelAll.UseVisualStyleBackColor = true;
            this.chk_SelAll.CheckedChanged += new EventHandler(this.chk_SelAll_CheckedChanged);
            this.bds_Pos_To.PositionChanged += new EventHandler(this.bds_Pos_To_PositionChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x44c, 520);
            base.Controls.Add(this.grp_To);
            base.Controls.Add(this.grp_From);
            base.Controls.Add(this.pnl_Button);
            base.Name = "frmSelPosFromAndTo";
            this.Text = "选择合盘货位";
            base.Load += new EventHandler(this.frmSelPosFromAndTo_Load);
            this.pnl_Button.ResumeLayout(false);
            this.pnl_Button.PerformLayout();
            this.grp_From.ResumeLayout(false);
            this.grp_From.PerformLayout();
            ((ISupportInitialize) this.grd_Pos_From).EndInit();
            ((ISupportInitialize) this.grd_StockDtl_From).EndInit();
            this.pnl_FromTop.ResumeLayout(false);
            this.pnl_FromTop.PerformLayout();
            this.grp_To.ResumeLayout(false);
            this.grp_To.PerformLayout();
            ((ISupportInitialize) this.grd_Pos_To).EndInit();
            ((ISupportInitialize) this.grd_StockDtl_To).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.bds_Pos_From).EndInit();
            ((ISupportInitialize) this.bds_Pos_To).EndInit();
            base.ResumeLayout(false);
        }
    }
}

