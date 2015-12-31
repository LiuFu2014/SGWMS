namespace WareStoreMS
{
    partial class frmMoving
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.grpCellChange = new System.Windows.Forms.GroupBox();
            this.btn_C_Sel_To = new System.Windows.Forms.Button();
            this.btn_C_Sel_From = new System.Windows.Forms.Button();
            this.btn_Chg_Do = new System.Windows.Forms.Button();
            this.txt_chg_CellTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_chg_CellFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_DoNow = new System.Windows.Forms.CheckBox();
            this.grpCellChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCellChange
            // 
            this.grpCellChange.Controls.Add(this.chk_DoNow);
            this.grpCellChange.Controls.Add(this.btn_C_Sel_To);
            this.grpCellChange.Controls.Add(this.btn_C_Sel_From);
            this.grpCellChange.Controls.Add(this.btn_Chg_Do);
            this.grpCellChange.Controls.Add(this.txt_chg_CellTo);
            this.grpCellChange.Controls.Add(this.label2);
            this.grpCellChange.Controls.Add(this.txt_chg_CellFrom);
            this.grpCellChange.Controls.Add(this.label1);
            this.grpCellChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCellChange.Location = new System.Drawing.Point(10, 10);
            this.grpCellChange.Name = "grpCellChange";
            this.grpCellChange.Size = new System.Drawing.Size(332, 235);
            this.grpCellChange.TabIndex = 2;
            this.grpCellChange.TabStop = false;
            // 
            // btn_C_Sel_To
            // 
            this.btn_C_Sel_To.Location = new System.Drawing.Point(249, 88);
            this.btn_C_Sel_To.Name = "btn_C_Sel_To";
            this.btn_C_Sel_To.Size = new System.Drawing.Size(24, 23);
            this.btn_C_Sel_To.TabIndex = 6;
            this.btn_C_Sel_To.Tag = "2";
            this.btn_C_Sel_To.Text = "…";
            this.btn_C_Sel_To.UseVisualStyleBackColor = true;
            this.btn_C_Sel_To.Click += new System.EventHandler(this.btn_C_Sel_To_Click);
            // 
            // btn_C_Sel_From
            // 
            this.btn_C_Sel_From.Location = new System.Drawing.Point(249, 37);
            this.btn_C_Sel_From.Name = "btn_C_Sel_From";
            this.btn_C_Sel_From.Size = new System.Drawing.Size(24, 23);
            this.btn_C_Sel_From.TabIndex = 5;
            this.btn_C_Sel_From.Tag = "1";
            this.btn_C_Sel_From.Text = "…";
            this.btn_C_Sel_From.UseVisualStyleBackColor = true;
            this.btn_C_Sel_From.Click += new System.EventHandler(this.btn_C_Sel_From_Click);
            // 
            // btn_Chg_Do
            // 
            this.btn_Chg_Do.Location = new System.Drawing.Point(61, 159);
            this.btn_Chg_Do.Name = "btn_Chg_Do";
            this.btn_Chg_Do.Size = new System.Drawing.Size(212, 23);
            this.btn_Chg_Do.TabIndex = 4;
            this.btn_Chg_Do.Text = "下发执行";
            this.btn_Chg_Do.UseVisualStyleBackColor = true;
            this.btn_Chg_Do.Click += new System.EventHandler(this.btn_Chg_Do_Click);
            // 
            // txt_chg_CellTo
            // 
            this.txt_chg_CellTo.Location = new System.Drawing.Point(119, 89);
            this.txt_chg_CellTo.Name = "txt_chg_CellTo";
            this.txt_chg_CellTo.Size = new System.Drawing.Size(124, 21);
            this.txt_chg_CellTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目标货位：";
            // 
            // txt_chg_CellFrom
            // 
            this.txt_chg_CellFrom.Location = new System.Drawing.Point(119, 38);
            this.txt_chg_CellFrom.Name = "txt_chg_CellFrom";
            this.txt_chg_CellFrom.Size = new System.Drawing.Size(124, 21);
            this.txt_chg_CellFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源货位：";
            // 
            // chk_DoNow
            // 
            this.chk_DoNow.AutoSize = true;
            this.chk_DoNow.Location = new System.Drawing.Point(62, 125);
            this.chk_DoNow.Name = "chk_DoNow";
            this.chk_DoNow.Size = new System.Drawing.Size(96, 16);
            this.chk_DoNow.TabIndex = 7;
            this.chk_DoNow.Text = "是否立即执行";
            this.chk_DoNow.UseVisualStyleBackColor = true;
            // 
            // frmMoving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 255);
            this.Controls.Add(this.grpCellChange);
            this.MinimizeBox = false;
            this.Name = "frmMoving";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "仓位调整";
            this.grpCellChange.ResumeLayout(false);
            this.grpCellChange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCellChange;
        private System.Windows.Forms.Button btn_C_Sel_To;
        private System.Windows.Forms.Button btn_C_Sel_From;
        private System.Windows.Forms.Button btn_Chg_Do;
        private System.Windows.Forms.TextBox txt_chg_CellTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_chg_CellFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_DoNow;
    }
}

