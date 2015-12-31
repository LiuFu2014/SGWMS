namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;

    public class frmInEmpty : FrmSTable
    {
        private Button btn_FP_GetNew;
        private Button btn_In_Do;
        private Button btn_In_Sel;
        private Button btn_In_SelPallet;
        private Button btn_OKBtch;
        private Button btnBatch;
        private CheckBox chk_IsFirstRow;
        private CheckBox chk_nIsDoNow;
        private CheckBox chkBtchDo;
        private ComboBox cmb_FP_cPalletType;
        private ComboBox cmb_OptGroup;
        private ComboBox cmb_Port;
        private ComboBox cmb_WHId;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox grpCellEmptyIn;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label2;
        private Label label3;
        private Label label30;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txt_ColF;
        private TextBox txt_ColT;
        private TextBox txt_cPalletNo;
        private TextBox txt_In_Cell;
        private TextBox txt_LayerF;
        private TextBox txt_LayerT;
        private TextBox txt_PltId;
        private TextBox txt_PltQty;
        private TextBox txt_RowF;
        private TextBox txt_RowT;

        public frmInEmpty()
        {
            this.InitializeComponent();
        }

        private void btn_FP_GetNew_Click(object sender, EventArgs e)
        {
        }

        private void btn_In_Do_Click(object sender, EventArgs e)
        {
            object objValue = null;
            if (this.txt_In_Cell.Text.Trim() == "")
            {
                MessageBox.Show("对不起，空盘入库货位号不能为空！");
                this.txt_In_Cell.Focus();
            }
            else if (this.txt_cPalletNo.Text.Trim() == "")
            {
                MessageBox.Show("对不起，空盘入库的托盘号不能为空！");
                this.txt_cPalletNo.Focus();
            }
            else
            {
                string sSql = "";
                string sErr = "";
                string pWHId = "10101";
                sSql = "select * from TWC_WareCell where nStatusWork = 0 and  cPosId='" + this.txt_In_Cell.Text.Trim() + "'";
                DataSet set = null;
                set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
                DataTable table = null;
                if (sErr.Trim().Length == 0)
                {
                    if (set == null)
                    {
                        MessageBox.Show("对不起，获取仓位信息时出错：" + sErr);
                    }
                    else
                    {
                        table = set.Tables["result"];
                        objValue = table.Rows[0]["returncode"];
                        if ((objValue != null) && (objValue.ToString() == "-1"))
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                            set.Clear();
                            MessageBox.Show("对不起，获取仓位信息时出错：" + sErr);
                        }
                        else
                        {
                            table = set.Tables["data"];
                            if (table.Rows.Count > 0)
                            {
                                pWHId = table.Rows[0]["cWHId"].ToString();
                                objValue = table.Rows[0]["nPalletId"];
                                if ((objValue != null) && (objValue.ToString().Trim().Length > 0))
                                {
                                    MessageBox.Show("对不起，该货位已存在托盘：" + objValue.ToString());
                                }
                                else
                                {
                                    set.Clear();
                                    objValue = null;
                                    sErr = "";
                                    sSql = "select count(*) from TWC_PalletCell where nPalletId='" + this.txt_cPalletNo.Text.Trim() + "'";
                                    PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "", out objValue, out sErr);
                                    if (sErr.Length > 0)
                                    {
                                        MessageBox.Show("校验托盘号时出错：" + sErr);
                                        this.txt_cPalletNo.SelectAll();
                                        this.txt_cPalletNo.Focus();
                                    }
                                    else if ((objValue == null) || (int.Parse(objValue.ToString()) == 0))
                                    {
                                        MessageBox.Show("对不起，托盘号：" + this.txt_cPalletNo.Text + "  不存在！");
                                        this.txt_cPalletNo.SelectAll();
                                        this.txt_cPalletNo.Focus();
                                    }
                                    else
                                    {
                                        objValue = null;
                                        sErr = "";
                                        sSql = "select count(*) from TWC_WareCell where isnull(nPalletId,' ')='" + this.txt_cPalletNo.Text.Trim() + "'";
                                        PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "", out objValue, out sErr);
                                        if (sErr.Length > 0)
                                        {
                                            MessageBox.Show("校验托盘号是否在用时出错：" + sErr);
                                            this.txt_cPalletNo.SelectAll();
                                            this.txt_cPalletNo.Focus();
                                        }
                                        else if ((objValue == null) || (int.Parse(objValue.ToString()) > 0))
                                        {
                                            MessageBox.Show("对不起，托盘号：" + this.txt_cPalletNo.Text + "  已经被占用！");
                                            this.txt_cPalletNo.SelectAll();
                                            this.txt_cPalletNo.Focus();
                                        }
                                        else
                                        {
                                            string str5 = "";
                                            if (this.chk_nIsDoNow.Checked)
                                            {
                                                str5 = PubDBCommFuns.sp_Pack_DoEmptyPalletIO(base.AppInformation.SvrSocket, "WMS", pWHId, 1, this.txt_In_Cell.Text.Trim(), this.txt_cPalletNo.Text.Trim(), int.Parse(this.cmb_Port.Text.Trim()), 1, base.UserInformation.UserName, base.UserInformation.UnitId, out sErr);
                                            }
                                            else
                                            {
                                                str5 = PubDBCommFuns.sp_Pack_DoEmptyPalletIO(base.AppInformation.SvrSocket, "WMS", pWHId, 1, this.txt_In_Cell.Text.Trim(), this.txt_cPalletNo.Text.Trim(), int.Parse(this.cmb_Port.Text.Trim()), 0, base.UserInformation.UserName, base.UserInformation.UnitId, out sErr);
                                            }
                                            if (str5 != "0")
                                            {
                                                MessageBox.Show(sErr);
                                            }
                                            else
                                            {
                                                MessageBox.Show("下发指令成功！");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("对不起，该货位：" + this.txt_In_Cell.Text + "  不存在或被禁用或被占用！");
                                this.txt_In_Cell.Focus();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("对不起，获取仓位信息时出错：" + sErr);
                }
            }
        }

        private void btn_In_Sel_Click(object sender, EventArgs e)
        {
            this.txt_In_Cell.Text = WareStore.GetCell(base.AppInformation, base.UserInformation, 0);
            if ((this.txt_In_Cell.Text.Trim() != "") && (this.cmb_OptGroup.Text.Trim() != ""))
            {
                int nRow = 0;
                nRow = this.GetPosRowNo(this.txt_In_Cell.Text.Trim());
                this.LoadOptNoList("", this.cmb_OptGroup.Text.Trim(), nRow);
            }
        }

        private void btn_In_SelPallet_Click(object sender, EventArgs e)
        {
            this.txt_cPalletNo.Text = this.GetPalletNo();
        }

        private void btn_OKBtch_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            string str = "10101";
            if (this.txt_RowF.Text.Trim() == "")
            {
                MessageBox.Show("起始行号不能为空！");
                this.txt_RowF.Focus();
            }
            else if (!FrmSTable.IsInteger(this.txt_RowF.Text.Trim()))
            {
                MessageBox.Show("起始行号为非法数字！");
                this.txt_RowF.SelectAll();
                this.txt_RowF.Focus();
            }
            else
            {
                num4 = int.Parse(this.txt_RowF.Text.Trim());
                if (this.txt_RowT.Text.Trim() == "")
                {
                    MessageBox.Show("截止行号不能为空！");
                    this.txt_RowT.Focus();
                }
                else if (!FrmSTable.IsInteger(this.txt_RowT.Text.Trim()))
                {
                    MessageBox.Show("截止行号为非法数字！");
                    this.txt_RowT.SelectAll();
                    this.txt_RowT.Focus();
                }
                else
                {
                    num5 = int.Parse(this.txt_RowT.Text.Trim());
                    if (this.txt_ColF.Text.Trim() == "")
                    {
                        MessageBox.Show("起始列号不能为空！");
                        this.txt_ColF.Focus();
                    }
                    else if (!FrmSTable.IsInteger(this.txt_ColF.Text.Trim()))
                    {
                        MessageBox.Show("起始列号为非法数字！");
                        this.txt_ColF.SelectAll();
                        this.txt_ColF.Focus();
                    }
                    else
                    {
                        num6 = int.Parse(this.txt_ColF.Text.Trim());
                        if (this.txt_ColT.Text.Trim() == "")
                        {
                            MessageBox.Show("截止列号不能为空！");
                            this.txt_ColT.Focus();
                        }
                        else if (!FrmSTable.IsInteger(this.txt_ColT.Text.Trim()))
                        {
                            MessageBox.Show("截止列号为非法数字！");
                            this.txt_ColT.SelectAll();
                            this.txt_ColT.Focus();
                        }
                        else
                        {
                            num7 = int.Parse(this.txt_ColT.Text.Trim());
                            if (this.txt_LayerF.Text.Trim() == "")
                            {
                                MessageBox.Show("起始层号不能为空！");
                                this.txt_LayerF.Focus();
                            }
                            else if (!FrmSTable.IsInteger(this.txt_LayerF.Text.Trim()))
                            {
                                MessageBox.Show("起始层号为非法数字！");
                                this.txt_LayerF.SelectAll();
                                this.txt_LayerF.Focus();
                            }
                            else
                            {
                                num8 = int.Parse(this.txt_LayerF.Text.Trim());
                                if (this.txt_LayerT.Text.Trim() == "")
                                {
                                    MessageBox.Show("截止层号不能为空！");
                                    this.txt_LayerT.Focus();
                                }
                                else if (!FrmSTable.IsInteger(this.txt_LayerT.Text.Trim()))
                                {
                                    MessageBox.Show("截止层号为非法数字！");
                                    this.txt_LayerT.SelectAll();
                                    this.txt_LayerT.Focus();
                                }
                                else
                                {
                                    num9 = int.Parse(this.txt_LayerT.Text.Trim());
                                    if (this.txt_PltId.Text.Trim() == "")
                                    {
                                        MessageBox.Show("起始托盘号不能为空！");
                                        this.txt_PltId.Focus();
                                    }
                                    else if (!FrmSTable.IsInteger(this.txt_PltId.Text.Trim()))
                                    {
                                        MessageBox.Show("起始托盘号为非法数字！");
                                        this.txt_PltId.SelectAll();
                                        this.txt_PltId.Focus();
                                    }
                                    else
                                    {
                                        num12 = int.Parse(this.txt_PltId.Text.Trim());
                                        if (this.txt_PltQty.Text.Trim() == "")
                                        {
                                            MessageBox.Show("入库托盘数为空！");
                                            this.txt_PltQty.Focus();
                                        }
                                        else if (!FrmSTable.IsInteger(this.txt_PltQty.Text.Trim()))
                                        {
                                            MessageBox.Show("入库托盘数为非法数字！");
                                            this.txt_PltQty.SelectAll();
                                            this.txt_PltQty.Focus();
                                        }
                                        else
                                        {
                                            num11 = int.Parse(this.txt_PltQty.Text.Trim());
                                            if (this.cmb_WHId.Text.Trim() == "")
                                            {
                                                MessageBox.Show("仓库不能为空！");
                                                this.cmb_WHId.Focus();
                                            }
                                            else if (this.cmb_WHId.SelectedValue == null)
                                            {
                                                MessageBox.Show("仓库不能为空，请选择正确的仓库！");
                                                this.cmb_WHId.Focus();
                                            }
                                            else if (num4 > num5)
                                            {
                                                MessageBox.Show("起始行号不能大于截止行号！");
                                                this.txt_RowF.SelectAll();
                                                this.txt_RowF.Focus();
                                            }
                                            else if (num6 > num7)
                                            {
                                                MessageBox.Show("起始列号不能大于截止列号！");
                                                this.txt_ColF.SelectAll();
                                                this.txt_ColF.Focus();
                                            }
                                            else if (num8 > num9)
                                            {
                                                MessageBox.Show("起始层号不能大于截止层号！");
                                                this.txt_LayerF.SelectAll();
                                                this.txt_LayerF.Focus();
                                            }
                                            else
                                            {
                                                string str2;
                                                str = this.cmb_WHId.SelectedValue.ToString().Trim();
                                                if (this.chk_IsFirstRow.Checked)
                                                {
                                                    for (num3 = num8; num3 <= num9; num3++)
                                                    {
                                                        num2 = num6;
                                                        while (num2 <= num7)
                                                        {
                                                            num = num4;
                                                            while (num <= num5)
                                                            {
                                                                if (num10 < num11)
                                                                {
                                                                    str2 = str + "-" + num.ToString("D2") + "-" + num2.ToString("D3") + "-" + num3.ToString("D2");
                                                                    if (this.DoEmptIn(str2, num12.ToString(), this.chkBtchDo.Checked))
                                                                    {
                                                                        num10++;
                                                                    }
                                                                    num12++;
                                                                }
                                                                num++;
                                                            }
                                                            num2++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (num3 = num8; num3 <= num9; num3++)
                                                    {
                                                        for (num = num4; num <= num5; num++)
                                                        {
                                                            for (num2 = num6; num2 <= num7; num2++)
                                                            {
                                                                if (num10 < num11)
                                                                {
                                                                    str2 = str + "-" + num.ToString("D2") + "-" + num2.ToString("D3") + "-" + num3.ToString("D2");
                                                                    if (this.DoEmptIn(str2, num12.ToString(), this.chkBtchDo.Checked))
                                                                    {
                                                                        num10++;
                                                                    }
                                                                    num12++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                MessageBox.Show("成功下发 " + num10.ToString() + " 个空盘入任务！");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            int num = 0xad;
            int num2 = 0x163;
            if (base.Height > 0xad)
            {
                base.Height = num;
                this.btnBatch.Text = ">>";
            }
            else
            {
                base.Height = num2;
                this.btnBatch.Text = "<<";
            }
        }

        private void cmb_OptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_OptGroup.Text.Trim() != "")
            {
                int nRow = 0;
                if (this.txt_In_Cell.Text.Trim() != "")
                {
                    nRow = this.GetPosRowNo(this.txt_In_Cell.Text.Trim());
                }
                this.LoadOptNoList("", this.cmb_OptGroup.Text.Trim(), nRow);
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

        private bool DoEmptIn(string sCellId, string sPltId, bool bDoNow)
        {
            bool flag = false;
            object objValue = null;
            if (sCellId.Trim() == "")
            {
                return flag;
            }
            if (sPltId.Trim() == "")
            {
                return flag;
            }
            string sSql = "";
            string sErr = "";
            string pWHId = "10101";
            sSql = "select * from TWC_WareCell where nStatusWork = 0 and  cPosId='" + sCellId.Trim() + "'";
            DataSet set = null;
            set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, out sErr);
            DataTable table = null;
            if (sErr.Trim().Length != 0)
            {
                return flag;
            }
            if (set == null)
            {
                return flag;
            }
            table = set.Tables["result"];
            objValue = table.Rows[0]["returncode"];
            if ((objValue != null) && (objValue.ToString() == "-1"))
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                set.Clear();
                return flag;
            }
            table = set.Tables["data"];
            if (table.Rows.Count <= 0)
            {
                return flag;
            }
            pWHId = table.Rows[0]["cWHId"].ToString();
            objValue = table.Rows[0]["nPalletId"];
            if ((objValue != null) && (objValue.ToString().Trim().Length > 0))
            {
                return flag;
            }
            set.Clear();
            objValue = null;
            sErr = "";
            sSql = "select count(*) from TWC_PalletCell where nPalletId='" + sPltId.Trim() + "'";
            PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "", out objValue, out sErr);
            if (sErr.Length > 0)
            {
                return flag;
            }
            if ((objValue == null) || (int.Parse(objValue.ToString()) == 0))
            {
                return flag;
            }
            objValue = null;
            sErr = "";
            sSql = "select count(*) from TWC_WareCell where isnull(nPalletId,' ')='" + sPltId.Trim() + "'";
            PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "", out objValue, out sErr);
            if (sErr.Length > 0)
            {
                return flag;
            }
            if ((objValue == null) || (int.Parse(objValue.ToString()) > 0))
            {
                return flag;
            }
            string str5 = "";
            if (bDoNow)
            {
                str5 = PubDBCommFuns.sp_Pack_DoEmptyPalletIO(base.AppInformation.SvrSocket, "WMS", pWHId, 1, sCellId.Trim(), sPltId.Trim(), int.Parse(this.cmb_Port.Text.Trim()), 1, base.UserInformation.UserName, base.UserInformation.UnitId, out sErr);
            }
            else
            {
                str5 = PubDBCommFuns.sp_Pack_DoEmptyPalletIO(base.AppInformation.SvrSocket, "WMS", pWHId, 1, sCellId.Trim(), sPltId.Trim(), int.Parse(this.cmb_Port.Text.Trim()), 0, base.UserInformation.UserName, base.UserInformation.UnitId, out sErr);
            }
            if (str5 != "0")
            {
                return flag;
            }
            return true;
        }

        private void frmInEmpty_Load(object sender, EventArgs e)
        {
            base.Height = 0xad;
            this.LoadWareHouseList();
            this.LoadOptGroup("");
        }

        private string GetPalletNo()
        {
            string selResult = "";
            FrmSelectPallet pallet = new FrmSelectPallet {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            pallet.ShowDialog();
            if (pallet.BIsResult)
            {
                selResult = pallet.SelResult;
            }
            else
            {
                selResult = "";
            }
            pallet.Dispose();
            return selResult;
        }

        private int GetPosRowNo(string sPosId)
        {
            int num = 0;
            string sSql = "select nRow from TWC_WareCell where cPosId='" + sPosId.Trim() + "'";
            object objValue = null;
            string sErr = "";
            if (DBFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "nRow", out objValue, out sErr))
            {
                if (objValue != null)
                {
                    num = Convert.ToInt16(objValue);
                }
                return num;
            }
            MessageBox.Show("获取货位的行号时出错：" + sErr);
            return num;
        }

        private void InitializeComponent()
        {
            this.grpCellEmptyIn = new GroupBox();
            this.btnBatch = new Button();
            this.chk_nIsDoNow = new CheckBox();
            this.btn_In_SelPallet = new Button();
            this.cmb_FP_cPalletType = new ComboBox();
            this.label8 = new Label();
            this.label7 = new Label();
            this.txt_cPalletNo = new TextBox();
            this.btn_FP_GetNew = new Button();
            this.btn_In_Sel = new Button();
            this.btn_In_Do = new Button();
            this.txt_In_Cell = new TextBox();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.chk_IsFirstRow = new CheckBox();
            this.cmb_WHId = new ComboBox();
            this.label13 = new Label();
            this.txt_PltQty = new TextBox();
            this.label11 = new Label();
            this.txt_PltId = new TextBox();
            this.label12 = new Label();
            this.txt_LayerT = new TextBox();
            this.label9 = new Label();
            this.txt_LayerF = new TextBox();
            this.label10 = new Label();
            this.txt_ColT = new TextBox();
            this.label3 = new Label();
            this.txt_ColF = new TextBox();
            this.label6 = new Label();
            this.txt_RowT = new TextBox();
            this.label2 = new Label();
            this.txt_RowF = new TextBox();
            this.label1 = new Label();
            this.chkBtchDo = new CheckBox();
            this.btn_OKBtch = new Button();
            this.cmb_OptGroup = new ComboBox();
            this.label30 = new Label();
            this.cmb_Port = new ComboBox();
            this.label5 = new Label();
            this.grpCellEmptyIn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.grpCellEmptyIn.Controls.Add(this.cmb_OptGroup);
            this.grpCellEmptyIn.Controls.Add(this.label30);
            this.grpCellEmptyIn.Controls.Add(this.cmb_Port);
            this.grpCellEmptyIn.Controls.Add(this.label5);
            this.grpCellEmptyIn.Controls.Add(this.btnBatch);
            this.grpCellEmptyIn.Controls.Add(this.chk_nIsDoNow);
            this.grpCellEmptyIn.Controls.Add(this.btn_In_SelPallet);
            this.grpCellEmptyIn.Controls.Add(this.cmb_FP_cPalletType);
            this.grpCellEmptyIn.Controls.Add(this.label8);
            this.grpCellEmptyIn.Controls.Add(this.label7);
            this.grpCellEmptyIn.Controls.Add(this.txt_cPalletNo);
            this.grpCellEmptyIn.Controls.Add(this.btn_FP_GetNew);
            this.grpCellEmptyIn.Controls.Add(this.btn_In_Sel);
            this.grpCellEmptyIn.Controls.Add(this.btn_In_Do);
            this.grpCellEmptyIn.Controls.Add(this.txt_In_Cell);
            this.grpCellEmptyIn.Controls.Add(this.label4);
            this.grpCellEmptyIn.Dock = DockStyle.Top;
            this.grpCellEmptyIn.Location = new Point(10, 10);
            this.grpCellEmptyIn.Name = "grpCellEmptyIn";
            this.grpCellEmptyIn.Size = new Size(0x1a5, 0x6c);
            this.grpCellEmptyIn.TabIndex = 2;
            this.grpCellEmptyIn.TabStop = false;
            this.grpCellEmptyIn.Text = "空盘入库";
            this.btnBatch.Location = new Point(0x141, 0x4c);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new Size(0x25, 0x17);
            this.btnBatch.TabIndex = 0x56;
            this.btnBatch.Text = ">>";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new EventHandler(this.btnBatch_Click);
            this.chk_nIsDoNow.AutoSize = true;
            this.chk_nIsDoNow.Checked = true;
            this.chk_nIsDoNow.CheckState = CheckState.Checked;
            this.chk_nIsDoNow.Location = new Point(0x19, 0x4f);
            this.chk_nIsDoNow.Name = "chk_nIsDoNow";
            this.chk_nIsDoNow.Size = new Size(0x60, 0x10);
            this.chk_nIsDoNow.TabIndex = 0x55;
            this.chk_nIsDoNow.Text = "是否立刻执行";
            this.chk_nIsDoNow.UseVisualStyleBackColor = true;
            this.btn_In_SelPallet.Location = new Point(0x17f, 0x10);
            this.btn_In_SelPallet.Name = "btn_In_SelPallet";
            this.btn_In_SelPallet.Size = new Size(0x18, 0x17);
            this.btn_In_SelPallet.TabIndex = 0x54;
            this.btn_In_SelPallet.Tag = "3";
            this.btn_In_SelPallet.Text = "…";
            this.btn_In_SelPallet.UseVisualStyleBackColor = true;
            this.btn_In_SelPallet.Click += new EventHandler(this.btn_In_SelPallet_Click);
            this.cmb_FP_cPalletType.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cmb_FP_cPalletType.FormattingEnabled = true;
            this.cmb_FP_cPalletType.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "14", "16" });
            this.cmb_FP_cPalletType.Location = new Point(0x167, 0x2f);
            this.cmb_FP_cPalletType.Name = "cmb_FP_cPalletType";
            this.cmb_FP_cPalletType.Size = new Size(0x30, 0x17);
            this.cmb_FP_cPalletType.TabIndex = 0x52;
            this.cmb_FP_cPalletType.Visible = false;
            this.label8.AutoSize = true;
            this.label8.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label8.Location = new Point(0x10f, 50);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x52, 15);
            this.label8.TabIndex = 0x53;
            this.label8.Text = "托盘类型：";
            this.label8.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(220, 0x15);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x51;
            this.label7.Text = "托盘号：";
            this.txt_cPalletNo.Location = new Point(0x111, 0x11);
            this.txt_cPalletNo.Name = "txt_cPalletNo";
            this.txt_cPalletNo.Size = new Size(0x6c, 0x15);
            this.txt_cPalletNo.TabIndex = 80;
            this.btn_FP_GetNew.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.btn_FP_GetNew.Location = new Point(0xef, 0x4c);
            this.btn_FP_GetNew.Name = "btn_FP_GetNew";
            this.btn_FP_GetNew.Size = new Size(0x4c, 0x17);
            this.btn_FP_GetNew.TabIndex = 0x4f;
            this.btn_FP_GetNew.Text = "获新号码";
            this.btn_FP_GetNew.UseVisualStyleBackColor = true;
            this.btn_FP_GetNew.Visible = false;
            this.btn_FP_GetNew.Click += new EventHandler(this.btn_FP_GetNew_Click);
            this.btn_In_Sel.Location = new Point(0xbb, 0x11);
            this.btn_In_Sel.Name = "btn_In_Sel";
            this.btn_In_Sel.Size = new Size(0x18, 0x17);
            this.btn_In_Sel.TabIndex = 10;
            this.btn_In_Sel.Tag = "3";
            this.btn_In_Sel.Text = "…";
            this.btn_In_Sel.UseVisualStyleBackColor = true;
            this.btn_In_Sel.Click += new EventHandler(this.btn_In_Sel_Click);
            this.btn_In_Do.Location = new Point(0x7e, 0x4c);
            this.btn_In_Do.Name = "btn_In_Do";
            this.btn_In_Do.Size = new Size(0x55, 0x17);
            this.btn_In_Do.TabIndex = 9;
            this.btn_In_Do.Text = "下发执行";
            this.btn_In_Do.UseVisualStyleBackColor = true;
            this.btn_In_Do.Click += new EventHandler(this.btn_In_Do_Click);
            this.txt_In_Cell.Location = new Point(0x4d, 0x12);
            this.txt_In_Cell.Name = "txt_In_Cell";
            this.txt_In_Cell.Size = new Size(0x6c, 0x15);
            this.txt_In_Cell.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x18, 0x16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "货位号：";
            this.groupBox1.Controls.Add(this.chk_IsFirstRow);
            this.groupBox1.Controls.Add(this.cmb_WHId);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_PltQty);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_PltId);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_LayerT);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_LayerF);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_ColT);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_ColF);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_RowT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_RowF);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkBtchDo);
            this.groupBox1.Controls.Add(this.btn_OKBtch);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(10, 0x76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1a5, 0xd4);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "批量空盘入库";
            this.chk_IsFirstRow.AutoSize = true;
            this.chk_IsFirstRow.Checked = true;
            this.chk_IsFirstRow.CheckState = CheckState.Checked;
            this.chk_IsFirstRow.Location = new Point(50, 0xb6);
            this.chk_IsFirstRow.Name = "chk_IsFirstRow";
            this.chk_IsFirstRow.Size = new Size(0x48, 0x10);
            this.chk_IsFirstRow.TabIndex = 0x68;
            this.chk_IsFirstRow.Text = "先行循环";
            this.chk_IsFirstRow.UseVisualStyleBackColor = true;
            this.cmb_WHId.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cmb_WHId.FormattingEnabled = true;
            this.cmb_WHId.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "14", "16" });
            this.cmb_WHId.Location = new Point(0x5c, 0x19);
            this.cmb_WHId.Name = "cmb_WHId";
            this.cmb_WHId.Size = new Size(0x110, 0x17);
            this.cmb_WHId.TabIndex = 0x66;
            this.label13.AutoSize = true;
            this.label13.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label13.Location = new Point(0x15, 0x1d);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x34, 15);
            this.label13.TabIndex = 0x67;
            this.label13.Text = "仓库：";
            this.txt_PltQty.Location = new Point(0x134, 0x3d);
            this.txt_PltQty.Name = "txt_PltQty";
            this.txt_PltQty.Size = new Size(0x38, 0x15);
            this.txt_PltQty.TabIndex = 0x65;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xed, 0x41);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x29, 12);
            this.label11.TabIndex = 100;
            this.label11.Text = "个数：";
            this.txt_PltId.Location = new Point(0x5c, 0x3d);
            this.txt_PltId.Name = "txt_PltId";
            this.txt_PltId.Size = new Size(0x38, 0x15);
            this.txt_PltId.TabIndex = 0x63;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x15, 0x41);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x4d, 12);
            this.label12.TabIndex = 0x62;
            this.label12.Text = "起始托盘号：";
            this.txt_LayerT.Location = new Point(0x134, 0x8b);
            this.txt_LayerT.Name = "txt_LayerT";
            this.txt_LayerT.Size = new Size(0x38, 0x15);
            this.txt_LayerT.TabIndex = 0x61;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xed, 0x8f);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 0x60;
            this.label9.Text = "截止层号：";
            this.txt_LayerF.Location = new Point(0x5c, 0x8b);
            this.txt_LayerF.Name = "txt_LayerF";
            this.txt_LayerF.Size = new Size(0x38, 0x15);
            this.txt_LayerF.TabIndex = 0x5f;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x15, 0x8f);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x41, 12);
            this.label10.TabIndex = 0x5e;
            this.label10.Text = "起始层号：";
            this.txt_ColT.Location = new Point(0x134, 0x70);
            this.txt_ColT.Name = "txt_ColT";
            this.txt_ColT.Size = new Size(0x38, 0x15);
            this.txt_ColT.TabIndex = 0x5d;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xed, 0x74);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 0x5c;
            this.label3.Text = "截止列号：";
            this.txt_ColF.Location = new Point(0x5c, 0x70);
            this.txt_ColF.Name = "txt_ColF";
            this.txt_ColF.Size = new Size(0x38, 0x15);
            this.txt_ColF.TabIndex = 0x5b;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x15, 0x74);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x41, 12);
            this.label6.TabIndex = 90;
            this.label6.Text = "起始列号：";
            this.txt_RowT.Location = new Point(0x134, 0x55);
            this.txt_RowT.Name = "txt_RowT";
            this.txt_RowT.Size = new Size(0x38, 0x15);
            this.txt_RowT.TabIndex = 0x59;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xed, 0x59);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 0x58;
            this.label2.Text = "截止行号：";
            this.txt_RowF.Location = new Point(0x5c, 0x55);
            this.txt_RowF.Name = "txt_RowF";
            this.txt_RowF.Size = new Size(0x38, 0x15);
            this.txt_RowF.TabIndex = 0x57;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x15, 0x59);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 0x56;
            this.label1.Text = "起始行号：";
            this.chkBtchDo.AutoSize = true;
            this.chkBtchDo.Checked = true;
            this.chkBtchDo.CheckState = CheckState.Checked;
            this.chkBtchDo.Location = new Point(0x95, 0xb6);
            this.chkBtchDo.Name = "chkBtchDo";
            this.chkBtchDo.Size = new Size(0x60, 0x10);
            this.chkBtchDo.TabIndex = 0x55;
            this.chkBtchDo.Text = "是否立刻执行";
            this.chkBtchDo.UseVisualStyleBackColor = true;
            this.btn_OKBtch.Location = new Point(0x103, 0xb3);
            this.btn_OKBtch.Name = "btn_OKBtch";
            this.btn_OKBtch.Size = new Size(0x4b, 0x17);
            this.btn_OKBtch.TabIndex = 9;
            this.btn_OKBtch.Text = "下发执行";
            this.btn_OKBtch.UseVisualStyleBackColor = true;
            this.btn_OKBtch.Click += new EventHandler(this.btn_OKBtch_Click);
            this.cmb_OptGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_OptGroup.FormattingEnabled = true;
            this.cmb_OptGroup.Location = new Point(90, 0x2e);
            this.cmb_OptGroup.Name = "cmb_OptGroup";
            this.cmb_OptGroup.Size = new Size(0x4a, 20);
            this.cmb_OptGroup.TabIndex = 110;
            this.cmb_OptGroup.SelectedIndexChanged += new EventHandler(this.cmb_OptGroup_SelectedIndexChanged);
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0x19, 50);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x41, 12);
            this.label30.TabIndex = 0x6d;
            this.label30.Text = "操作台组别";
            this.cmb_Port.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Port.FormattingEnabled = true;
            this.cmb_Port.Location = new Point(0xde, 0x2e);
            this.cmb_Port.Name = "cmb_Port";
            this.cmb_Port.Size = new Size(0x25, 20);
            this.cmb_Port.TabIndex = 0x6c;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xb9, 50);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 0x6b;
            this.label5.Text = "台号";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x1b9, 340);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.grpCellEmptyIn);
            base.MinimizeBox = false;
            base.Name = "frmInEmpty";
            base.Padding = new Padding(10);
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "空盘入库";
            base.Load += new EventHandler(this.frmInEmpty_Load);
            this.grpCellEmptyIn.ResumeLayout(false);
            this.grpCellEmptyIn.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void LoadOptGroup(string sWHId)
        {
            string sSql = "select distinct cGroupName from TECS_HSInfo ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " where cWHId='" + sWHId.Trim() + "'";
            }
            string sErr = "";
            DataSet set = null;
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "OptGroup", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set != null)
            {
                DataTable table = set.Tables["OptGroup"];
                if (table != null)
                {
                    this.cmb_OptGroup.DisplayMember = "cGroupName";
                    this.cmb_OptGroup.ValueMember = "cGroupName";
                    this.cmb_OptGroup.DataSource = table;
                    string sValue = "";
                    if (MyConfigure.ReadMyArributeValue(base.AppInformation.AppPath + @"\" + base.AppInformation.AppConfigFile, "config/ECS/OptGroup", "Default", out sValue, out sErr))
                    {
                        if (this.cmb_OptGroup.Items.Count > 0)
                        {
                            if (sValue.Trim() != "")
                            {
                                this.cmb_OptGroup.SelectedValue = sValue;
                            }
                            else
                            {
                                this.cmb_OptGroup.SelectedIndex = 0;
                            }
                        }
                    }
                    else if (this.cmb_OptGroup.Items.Count > 0)
                    {
                        this.cmb_OptGroup.SelectedIndex = 0;
                    }
                }
            }
        }

        private void LoadOptNoList(string sWHId, string sOptGroup, int nRow)
        {
            int num = 0;
            if (nRow > 0)
            {
                num = nRow;
            }
            num = (num + 1) / 2;
            string sSql = "select nOptNo from TECS_HSInfo where 1=1 ";
            if (sWHId.Trim() != "")
            {
                sSql = sSql + " and cWHId='" + sWHId.Trim() + "'";
            }
            if (sOptGroup.Trim() != "")
            {
                sSql = sSql + " and cGroupName='" + sOptGroup.Trim() + "'";
            }
            if (num > 0)
            {
                sSql = sSql + " and nLine=" + num.ToString();
            }
            string sErr = "";
            DataSet set = null;
            set = DBFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "OptNo", 0, 0, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else if (set != null)
            {
                DataTable table = set.Tables["OptNo"];
                if (table != null)
                {
                    this.cmb_Port.DisplayMember = "nOptNo";
                    this.cmb_Port.ValueMember = "nOptNo";
                    this.cmb_Port.DataSource = table;
                    if (this.cmb_Port.Items.Count > 0)
                    {
                        this.cmb_Port.SelectedIndex = 0;
                    }
                }
            }
        }

        private void LoadWareHouseList()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("select * from V_WareHouse where bUsed = 1 and cCmptId='" + base.UserInformation.UnitId + "' ");
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(" and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            string sErr = "";
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "TWC_WareHouse", 0, 0, "", out sErr);
            if (sErr.Trim().Length > 0)
            {
                MessageBox.Show("打开仓库数据时出错：" + sErr);
            }
            else if (set == null)
            {
                MessageBox.Show("打开仓库数据时出错：" + sErr);
            }
            else
            {
                try
                {
                    DataTable table = set.Tables["TWC_WareHouse"];
                    this.cmb_WHId.DisplayMember = "cName";
                    this.cmb_WHId.ValueMember = "cWHId";
                    this.cmb_WHId.DataSource = table;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                if (this.cmb_WHId.Items.Count > 0)
                {
                    this.cmb_WHId.SelectedIndex = 0;
                }
            }
        }
    }
}

