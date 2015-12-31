namespace WareStoreMS
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using UI;

    public class frmMoving : FrmSTable
    {
        private Button btn_C_Sel_From;
        private Button btn_C_Sel_To;
        private Button btn_Chg_Do;
        private CheckBox chk_DoNow;
        private IContainer components = null;
        private GroupBox grpCellChange;
        private Label label1;
        private Label label2;
        private TextBox txt_chg_CellFrom;
        private TextBox txt_chg_CellTo;

        public frmMoving()
        {
            this.InitializeComponent();
        }

        private void btn_C_Sel_From_Click(object sender, EventArgs e)
        {
            string cellId = this.GetCellId(1);
            if (cellId != "")
            {
                this.txt_chg_CellFrom.Text = cellId;
            }
            this.txt_chg_CellFrom.Focus();
        }

        private void btn_C_Sel_To_Click(object sender, EventArgs e)
        {
            string cellId = this.GetCellId(0);
            if (cellId != "")
            {
                this.txt_chg_CellTo.Text = cellId;
            }
            this.txt_chg_CellTo.Focus();
        }

        private void btn_Chg_Do_Click(object sender, EventArgs e)
        {
            string sErr = "";
            int pIsDoNow = 0;
            if (this.chk_DoNow.Checked)
            {
                pIsDoNow = 1;
            }
            if ((DBFuns.sp_Pack_DoWareCellMove(base.AppInformation.SvrSocket, "WMS", this.txt_chg_CellFrom.Text.Trim(), this.txt_chg_CellTo.Text.Trim(), base.UserInformation.UserName, base.UserInformation.UnitId, pIsDoNow, out sErr) == "0") || (sErr.Trim().Length == 0))
            {
                MessageBox.Show("下发执行操作成功！");
                base.Close();
            }
            else
            {
                MessageBox.Show(sErr);
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

        private string GetCellId(int nState)
        {
            string selResult = "";
            FrmSelectCell cell = new FrmSelectCell {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            if (nState < cell.cmb_nState.Items.Count)
            {
                cell.cmb_nState.SelectedIndex = nState;
            }
            cell.ShowDialog();
            if (cell.BIsResult)
            {
                selResult = cell.SelResult;
            }
            else
            {
                selResult = "";
            }
            cell.Dispose();
            return selResult;
        }

        private void InitializeComponent()
        {
            this.grpCellChange = new GroupBox();
            this.btn_C_Sel_To = new Button();
            this.btn_C_Sel_From = new Button();
            this.btn_Chg_Do = new Button();
            this.txt_chg_CellTo = new TextBox();
            this.label2 = new Label();
            this.txt_chg_CellFrom = new TextBox();
            this.label1 = new Label();
            this.chk_DoNow = new CheckBox();
            this.grpCellChange.SuspendLayout();
            base.SuspendLayout();
            this.grpCellChange.Controls.Add(this.chk_DoNow);
            this.grpCellChange.Controls.Add(this.btn_C_Sel_To);
            this.grpCellChange.Controls.Add(this.btn_C_Sel_From);
            this.grpCellChange.Controls.Add(this.btn_Chg_Do);
            this.grpCellChange.Controls.Add(this.txt_chg_CellTo);
            this.grpCellChange.Controls.Add(this.label2);
            this.grpCellChange.Controls.Add(this.txt_chg_CellFrom);
            this.grpCellChange.Controls.Add(this.label1);
            this.grpCellChange.Dock = DockStyle.Fill;
            this.grpCellChange.Location = new Point(10, 10);
            this.grpCellChange.Name = "grpCellChange";
            this.grpCellChange.Size = new Size(0x14c, 0xeb);
            this.grpCellChange.TabIndex = 2;
            this.grpCellChange.TabStop = false;
            this.btn_C_Sel_To.Location = new Point(0xf9, 0x58);
            this.btn_C_Sel_To.Name = "btn_C_Sel_To";
            this.btn_C_Sel_To.Size = new Size(0x18, 0x17);
            this.btn_C_Sel_To.TabIndex = 6;
            this.btn_C_Sel_To.Tag = "2";
            this.btn_C_Sel_To.Text = "…";
            this.btn_C_Sel_To.UseVisualStyleBackColor = true;
            this.btn_C_Sel_To.Click += new EventHandler(this.btn_C_Sel_To_Click);
            this.btn_C_Sel_From.Location = new Point(0xf9, 0x25);
            this.btn_C_Sel_From.Name = "btn_C_Sel_From";
            this.btn_C_Sel_From.Size = new Size(0x18, 0x17);
            this.btn_C_Sel_From.TabIndex = 5;
            this.btn_C_Sel_From.Tag = "1";
            this.btn_C_Sel_From.Text = "…";
            this.btn_C_Sel_From.UseVisualStyleBackColor = true;
            this.btn_C_Sel_From.Click += new EventHandler(this.btn_C_Sel_From_Click);
            this.btn_Chg_Do.Location = new Point(0x3d, 0x9f);
            this.btn_Chg_Do.Name = "btn_Chg_Do";
            this.btn_Chg_Do.Size = new Size(0xd4, 0x17);
            this.btn_Chg_Do.TabIndex = 4;
            this.btn_Chg_Do.Text = "下发执行";
            this.btn_Chg_Do.UseVisualStyleBackColor = true;
            this.btn_Chg_Do.Click += new EventHandler(this.btn_Chg_Do_Click);
            this.txt_chg_CellTo.Location = new Point(0x77, 0x59);
            this.txt_chg_CellTo.Name = "txt_chg_CellTo";
            this.txt_chg_CellTo.Size = new Size(0x7c, 0x15);
            this.txt_chg_CellTo.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x3b, 0x5d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目标货位：";
            this.txt_chg_CellFrom.Location = new Point(0x77, 0x26);
            this.txt_chg_CellFrom.Name = "txt_chg_CellFrom";
            this.txt_chg_CellFrom.Size = new Size(0x7c, 0x15);
            this.txt_chg_CellFrom.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(60, 0x2a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源货位：";
            this.chk_DoNow.AutoSize = true;
            this.chk_DoNow.Location = new Point(0x3e, 0x7d);
            this.chk_DoNow.Name = "chk_DoNow";
            this.chk_DoNow.Size = new Size(0x60, 0x10);
            this.chk_DoNow.TabIndex = 7;
            this.chk_DoNow.Text = "是否立即执行";
            this.chk_DoNow.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x160, 0xff);
            base.Controls.Add(this.grpCellChange);
            base.MinimizeBox = false;
            base.Name = "frmMoving";
            base.Padding = new Padding(10);
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "仓位调整";
            this.grpCellChange.ResumeLayout(false);
            this.grpCellChange.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

