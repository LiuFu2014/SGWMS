namespace SunEast.App
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmOutAndSee : FrmSTable
    {
        private Button btn_OutAndSee_Do;
        private Button btn_OutAndSee_Sel;
        private CheckBox chk_nIsDoNow;
        private ComboBox cmb_OptGroup;
        private ComboBox cmb_Port;
        private IContainer components = null;
        private GroupBox grpCellOutAndSee;
        private Label label3;
        private Label label30;
        private Label label4;
        public Panel pnlOut;
        private TextBox txt_OutAndSee_Cell;

        public frmOutAndSee()
        {
            this.InitializeComponent();
        }

        private void btn_OutAndSee_Do_Click(object sender, EventArgs e)
        {
            string sErr = "";
            if (this.txt_OutAndSee_Cell.Text.Trim() == "")
            {
                MessageBox.Show("对不起，货位号不能为空！");
                this.txt_OutAndSee_Cell.Focus();
            }
            else
            {
                string str2 = "";
                str2 = PubDBCommFuns.sp_Pack_DoOutAndSeeTask(base.AppInformation.SvrSocket, "WMS", this.txt_OutAndSee_Cell.Text.Trim(), base.UserInformation.UserName, base.UserInformation.UnitId, int.Parse(this.cmb_Port.Text.Trim()), out sErr);
                if (((str2.Trim() != "") && (str2.Trim() != "0")) && (sErr.Trim() != ""))
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    MessageBox.Show("下发指令成功！");
                }
            }
        }

        private void btn_OutAndSee_Sel_Click(object sender, EventArgs e)
        {
            this.txt_OutAndSee_Cell.Text = WareStore.GetCell(base.AppInformation, base.UserInformation, 1);
            if ((this.txt_OutAndSee_Cell.Text.Trim() != "") && (this.cmb_OptGroup.Text.Trim() != ""))
            {
                int nRow = 0;
                nRow = this.GetPosRowNo(this.txt_OutAndSee_Cell.Text.Trim());
                this.LoadOptNoList("", this.cmb_OptGroup.Text.Trim(), nRow);
            }
        }

        private void cmb_OptGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_OptGroup.Text.Trim() != "")
            {
                int nRow = 0;
                if (this.txt_OutAndSee_Cell.Text.Trim() != "")
                {
                    nRow = this.GetPosRowNo(this.txt_OutAndSee_Cell.Text.Trim());
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

        private void frmOutAndSee_Load(object sender, EventArgs e)
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
            this.btn_OutAndSee_Sel = new Button();
            this.btn_OutAndSee_Do = new Button();
            this.txt_OutAndSee_Cell = new TextBox();
            this.label3 = new Label();
            this.chk_nIsDoNow = new CheckBox();
            this.pnlOut = new Panel();
            this.grpCellOutAndSee = new GroupBox();
            this.cmb_OptGroup = new ComboBox();
            this.label30 = new Label();
            this.cmb_Port = new ComboBox();
            this.label4 = new Label();
            this.pnlOut.SuspendLayout();
            this.grpCellOutAndSee.SuspendLayout();
            base.SuspendLayout();
            this.btn_OutAndSee_Sel.Location = new Point(0x10a, 0x1c);
            this.btn_OutAndSee_Sel.Name = "btn_OutAndSee_Sel";
            this.btn_OutAndSee_Sel.Size = new Size(0x18, 0x17);
            this.btn_OutAndSee_Sel.TabIndex = 13;
            this.btn_OutAndSee_Sel.Tag = "4";
            this.btn_OutAndSee_Sel.Text = "…";
            this.btn_OutAndSee_Sel.UseVisualStyleBackColor = true;
            this.btn_OutAndSee_Sel.Click += new EventHandler(this.btn_OutAndSee_Sel_Click);
            this.btn_OutAndSee_Do.Location = new Point(0xc2, 0x6c);
            this.btn_OutAndSee_Do.Name = "btn_OutAndSee_Do";
            this.btn_OutAndSee_Do.Size = new Size(0x75, 0x17);
            this.btn_OutAndSee_Do.TabIndex = 12;
            this.btn_OutAndSee_Do.Text = "下发执行(&D)";
            this.btn_OutAndSee_Do.UseVisualStyleBackColor = true;
            this.btn_OutAndSee_Do.Click += new EventHandler(this.btn_OutAndSee_Do_Click);
            this.txt_OutAndSee_Cell.Location = new Point(0x7d, 0x1d);
            this.txt_OutAndSee_Cell.Name = "txt_OutAndSee_Cell";
            this.txt_OutAndSee_Cell.Size = new Size(0x7d, 0x15);
            this.txt_OutAndSee_Cell.TabIndex = 11;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x4a, 0x21);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "货位：";
            this.chk_nIsDoNow.Checked = true;
            this.chk_nIsDoNow.CheckState = CheckState.Checked;
            this.chk_nIsDoNow.Location = new Point(0x54, 0x6b);
            this.chk_nIsDoNow.Name = "chk_nIsDoNow";
            this.chk_nIsDoNow.Size = new Size(0x68, 0x18);
            this.chk_nIsDoNow.TabIndex = 0x56;
            this.chk_nIsDoNow.Text = "是否立刻执行";
            this.chk_nIsDoNow.UseVisualStyleBackColor = true;
            this.pnlOut.Controls.Add(this.grpCellOutAndSee);
            this.pnlOut.Dock = DockStyle.Fill;
            this.pnlOut.Location = new Point(0, 0);
            this.pnlOut.Name = "pnlOut";
            this.pnlOut.Padding = new Padding(5);
            this.pnlOut.Size = new Size(0x19e, 0x94);
            this.pnlOut.TabIndex = 6;
            this.grpCellOutAndSee.Controls.Add(this.cmb_OptGroup);
            this.grpCellOutAndSee.Controls.Add(this.label30);
            this.grpCellOutAndSee.Controls.Add(this.cmb_Port);
            this.grpCellOutAndSee.Controls.Add(this.label4);
            this.grpCellOutAndSee.Controls.Add(this.chk_nIsDoNow);
            this.grpCellOutAndSee.Controls.Add(this.btn_OutAndSee_Sel);
            this.grpCellOutAndSee.Controls.Add(this.btn_OutAndSee_Do);
            this.grpCellOutAndSee.Controls.Add(this.txt_OutAndSee_Cell);
            this.grpCellOutAndSee.Controls.Add(this.label3);
            this.grpCellOutAndSee.Dock = DockStyle.Fill;
            this.grpCellOutAndSee.Location = new Point(5, 5);
            this.grpCellOutAndSee.Name = "grpCellOutAndSee";
            this.grpCellOutAndSee.Size = new Size(0x194, 0x8a);
            this.grpCellOutAndSee.TabIndex = 3;
            this.grpCellOutAndSee.TabStop = false;
            this.grpCellOutAndSee.Text = "出库查看";
            this.cmb_OptGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_OptGroup.FormattingEnabled = true;
            this.cmb_OptGroup.Location = new Point(0x8a, 0x42);
            this.cmb_OptGroup.Name = "cmb_OptGroup";
            this.cmb_OptGroup.Size = new Size(0x4a, 20);
            this.cmb_OptGroup.TabIndex = 0x6a;
            this.cmb_OptGroup.SelectedIndexChanged += new EventHandler(this.cmb_OptGroup_SelectedIndexChanged);
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0x49, 70);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x41, 12);
            this.label30.TabIndex = 0x69;
            this.label30.Text = "操作台组别";
            this.cmb_Port.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_Port.FormattingEnabled = true;
            this.cmb_Port.Location = new Point(0xfd, 0x42);
            this.cmb_Port.Name = "cmb_Port";
            this.cmb_Port.Size = new Size(0x25, 20);
            this.cmb_Port.TabIndex = 0x68;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xdd, 70);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x67;
            this.label4.Text = "台号";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x19e, 0x94);
            base.Controls.Add(this.pnlOut);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmOutAndSee";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "出库查看";
            base.Load += new EventHandler(this.frmOutAndSee_Load);
            this.pnlOut.ResumeLayout(false);
            this.grpCellOutAndSee.ResumeLayout(false);
            this.grpCellOutAndSee.PerformLayout();
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

