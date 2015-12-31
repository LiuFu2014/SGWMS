namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmOutEmpty : FrmSTable
    {
        private Button btn_Out_Do;
        private Button btn_Out_Sel;
        private CheckBox chk_nIsDoNow;
        private ComboBox cmb_OptGroup;
        private ComboBox cmb_Port;
        private IContainer components = null;
        private GroupBox grpCellEmptyOut;
        private Label label3;
        private Label label30;
        private Label label4;
        public Panel pnlOut;
        private TextBox txt_Out_Cell;

        public frmOutEmpty()
        {
            this.InitializeComponent();
        }

        private void btn_Out_Do_Click(object sender, EventArgs e)
        {
            object obj2 = null;
            if (this.txt_Out_Cell.Text.Trim() == "")
            {
                MessageBox.Show("对不起，空盘出库货位号不能为空！");
                this.txt_Out_Cell.Focus();
            }
            else
            {
                string sSql = "";
                string sErr = "";
                string pWHId = "10101";
                string pPalletId = "";
                sSql = "select * from TWC_WareCell where nStatusWork = 0 and  cPosId='" + this.txt_Out_Cell.Text.Trim() + "'";
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
                        obj2 = table.Rows[0]["returncode"];
                        if ((obj2 != null) && (obj2.ToString() == "-1"))
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
                                obj2 = table.Rows[0]["nPalletId"];
                                if ((obj2 == null) || (obj2.ToString().Trim().Length == 0))
                                {
                                    MessageBox.Show("对不起，该货位的无托盘号！");
                                }
                                else
                                {
                                    pPalletId = obj2.ToString().Trim();
                                    set.Clear();
                                    string str5 = "";
                                    if (this.chk_nIsDoNow.Checked)
                                    {
                                        str5 = PubDBCommFuns.sp_Pack_DoEmptyPalletIO(base.AppInformation.SvrSocket, "WMS", pWHId, 0, this.txt_Out_Cell.Text.Trim(), pPalletId, int.Parse(this.cmb_Port.Text.Trim()), 1, base.UserInformation.UserName, base.UserInformation.UnitId, out sErr);
                                    }
                                    else
                                    {
                                        str5 = PubDBCommFuns.sp_Pack_DoEmptyPalletIO(base.AppInformation.SvrSocket, "WMS", pWHId, 0, this.txt_Out_Cell.Text.Trim(), pPalletId, int.Parse(this.cmb_Port.Text.Trim()), 0, base.UserInformation.UserName, base.UserInformation.UnitId, out sErr);
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
                            else
                            {
                                MessageBox.Show("对不起，该货位：" + this.txt_Out_Cell.Text + "  不存在或被禁用或被占用！");
                                this.txt_Out_Cell.Focus();
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

        private void btn_Out_Sel_Click(object sender, EventArgs e)
        {
            this.txt_Out_Cell.Text = WareStore.GetCell(base.AppInformation, base.UserInformation, 1);
            if ((this.txt_Out_Cell.Text.Trim() != "") && (this.cmb_OptGroup.Text.Trim() != ""))
            {
                int nRow = 0;
                nRow = this.GetPosRowNo(this.txt_Out_Cell.Text.Trim());
                this.LoadOptNoList("", this.cmb_OptGroup.Text.Trim(), nRow);
            }
        }

        private void cmb_OptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_OptGroup.Text.Trim() != "")
            {
                int nRow = 0;
                if (this.txt_Out_Cell.Text.Trim() != "")
                {
                    nRow = this.GetPosRowNo(this.txt_Out_Cell.Text.Trim());
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

        private void frmOutEmpty_Load(object sender, EventArgs e)
        {
            this.LoadOptGroup("");
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
            this.pnlOut = new Panel();
            this.grpCellEmptyOut = new GroupBox();
            this.chk_nIsDoNow = new CheckBox();
            this.btn_Out_Sel = new Button();
            this.btn_Out_Do = new Button();
            this.txt_Out_Cell = new TextBox();
            this.label3 = new Label();
            this.cmb_OptGroup = new ComboBox();
            this.label30 = new Label();
            this.cmb_Port = new ComboBox();
            this.label4 = new Label();
            this.pnlOut.SuspendLayout();
            this.grpCellEmptyOut.SuspendLayout();
            base.SuspendLayout();
            this.pnlOut.Controls.Add(this.grpCellEmptyOut);
            this.pnlOut.Dock = DockStyle.Fill;
            this.pnlOut.Location = new Point(0, 0);
            this.pnlOut.Name = "pnlOut";
            this.pnlOut.Padding = new Padding(5);
            this.pnlOut.Size = new Size(0x176, 0x8f);
            this.pnlOut.TabIndex = 5;
            this.grpCellEmptyOut.Controls.Add(this.cmb_OptGroup);
            this.grpCellEmptyOut.Controls.Add(this.label30);
            this.grpCellEmptyOut.Controls.Add(this.cmb_Port);
            this.grpCellEmptyOut.Controls.Add(this.label4);
            this.grpCellEmptyOut.Controls.Add(this.chk_nIsDoNow);
            this.grpCellEmptyOut.Controls.Add(this.btn_Out_Sel);
            this.grpCellEmptyOut.Controls.Add(this.btn_Out_Do);
            this.grpCellEmptyOut.Controls.Add(this.txt_Out_Cell);
            this.grpCellEmptyOut.Controls.Add(this.label3);
            this.grpCellEmptyOut.Dock = DockStyle.Fill;
            this.grpCellEmptyOut.Location = new Point(5, 5);
            this.grpCellEmptyOut.Name = "grpCellEmptyOut";
            this.grpCellEmptyOut.Size = new Size(0x16c, 0x85);
            this.grpCellEmptyOut.TabIndex = 3;
            this.grpCellEmptyOut.TabStop = false;
            this.grpCellEmptyOut.Text = "空盘出库";
            this.chk_nIsDoNow.AutoSize = true;
            this.chk_nIsDoNow.Checked = true;
            this.chk_nIsDoNow.CheckState = CheckState.Checked;
            this.chk_nIsDoNow.Location = new Point(0x4a, 0x68);
            this.chk_nIsDoNow.Name = "chk_nIsDoNow";
            this.chk_nIsDoNow.Size = new Size(0x60, 0x10);
            this.chk_nIsDoNow.TabIndex = 0x56;
            this.chk_nIsDoNow.Text = "是否立刻执行";
            this.chk_nIsDoNow.UseVisualStyleBackColor = true;
            this.btn_Out_Sel.Location = new Point(0x10d, 0x19);
            this.btn_Out_Sel.Name = "btn_Out_Sel";
            this.btn_Out_Sel.Size = new Size(0x18, 0x17);
            this.btn_Out_Sel.TabIndex = 13;
            this.btn_Out_Sel.Tag = "4";
            this.btn_Out_Sel.Text = "…";
            this.btn_Out_Sel.UseVisualStyleBackColor = true;
            this.btn_Out_Sel.Click += new EventHandler(this.btn_Out_Sel_Click);
            this.btn_Out_Do.Location = new Point(0xbd, 0x65);
            this.btn_Out_Do.Name = "btn_Out_Do";
            this.btn_Out_Do.Size = new Size(0x68, 0x17);
            this.btn_Out_Do.TabIndex = 12;
            this.btn_Out_Do.Text = "下发执行(&D)";
            this.btn_Out_Do.UseVisualStyleBackColor = true;
            this.btn_Out_Do.Click += new EventHandler(this.btn_Out_Do_Click);
            this.txt_Out_Cell.Location = new Point(0x81, 0x1a);
            this.txt_Out_Cell.Name = "txt_Out_Cell";
            this.txt_Out_Cell.Size = new Size(0x7c, 0x15);
            this.txt_Out_Cell.TabIndex = 11;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x48, 30);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "货位：";
            this.cmb_OptGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_OptGroup.FormattingEnabled = true;
            this.cmb_OptGroup.Location = new Point(0x89, 0x43);
            this.cmb_OptGroup.Name = "cmb_OptGroup";
            this.cmb_OptGroup.Size = new Size(0x4a, 20);
            this.cmb_OptGroup.TabIndex = 0x66;
            this.cmb_OptGroup.SelectedIndexChanged += new EventHandler(this.cmb_OptGroup_SelectedIndexChanged);
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0x48, 0x47);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x41, 12);
            this.label30.TabIndex = 0x65;
            this.label30.Text = "操作台组别";
            this.cmb_Port.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Port.FormattingEnabled = true;
            this.cmb_Port.Location = new Point(0x100, 0x43);
            this.cmb_Port.Name = "cmb_Port";
            this.cmb_Port.Size = new Size(0x25, 20);
            this.cmb_Port.TabIndex = 100;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xe7, 0x48);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x63;
            this.label4.Text = "台号";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x176, 0x8f);
            base.Controls.Add(this.pnlOut);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmOutEmpty";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "空盘出库";
            base.Load += new EventHandler(this.frmOutEmpty_Load);
            this.pnlOut.ResumeLayout(false);
            this.grpCellEmptyOut.ResumeLayout(false);
            this.grpCellEmptyOut.PerformLayout();
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
    }
}

