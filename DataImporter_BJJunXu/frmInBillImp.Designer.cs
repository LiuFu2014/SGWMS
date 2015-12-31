namespace DataImporter
{
    partial class frmInBillImp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInBillImp));
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.tlb_Setup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_SelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlb_UnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Import = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Exit = new System.Windows.Forms.ToolStripButton();
            this.dlg_OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tlbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlbMain
            // 
            this.tlbMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Setup,
            this.toolStripSeparator1,
            this.tlb_SelectAll,
            this.tlb_UnSelectAll,
            this.toolStripSeparator2,
            this.tlb_Import,
            this.toolStripSeparator3,
            this.tlb_Refresh,
            this.toolStripSeparator4,
            this.tlb_Delete,
            this.toolStripSeparator5,
            this.tlb_Exit});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(687, 25);
            this.tlbMain.TabIndex = 11;
            this.tlbMain.Text = "toolStrip1";
            this.tlbMain.Visible = false;
            // 
            // tlb_Setup
            // 
            this.tlb_Setup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_Setup.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_Setup.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Setup.Image")));
            this.tlb_Setup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Setup.Name = "tlb_Setup";
            this.tlb_Setup.Size = new System.Drawing.Size(61, 22);
            this.tlb_Setup.Text = "参数设置";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_SelectAll
            // 
            this.tlb_SelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_SelectAll.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_SelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SelectAll.Image")));
            this.tlb_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SelectAll.Name = "tlb_SelectAll";
            this.tlb_SelectAll.Size = new System.Drawing.Size(49, 22);
            this.tlb_SelectAll.Text = "全  选";
            // 
            // tlb_UnSelectAll
            // 
            this.tlb_UnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_UnSelectAll.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_UnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tlb_UnSelectAll.Image")));
            this.tlb_UnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_UnSelectAll.Name = "tlb_UnSelectAll";
            this.tlb_UnSelectAll.Size = new System.Drawing.Size(61, 22);
            this.tlb_UnSelectAll.Text = "取消全选";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_Import
            // 
            this.tlb_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_Import.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_Import.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Import.Image")));
            this.tlb_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Import.Name = "tlb_Import";
            this.tlb_Import.Size = new System.Drawing.Size(49, 22);
            this.tlb_Import.Text = "导  入";
            this.tlb_Import.Click += new System.EventHandler(this.tlb_Import_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_Refresh
            // 
            this.tlb_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_Refresh.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Refresh.Image")));
            this.tlb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Refresh.Name = "tlb_Refresh";
            this.tlb_Refresh.Size = new System.Drawing.Size(56, 22);
            this.tlb_Refresh.Text = "刷   新";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_Delete
            // 
            this.tlb_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
            this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Delete.Name = "tlb_Delete";
            this.tlb_Delete.Size = new System.Drawing.Size(56, 22);
            this.tlb_Delete.Text = "删   除";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_Exit
            // 
            this.tlb_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_Exit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Exit.Image")));
            this.tlb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Exit.Name = "tlb_Exit";
            this.tlb_Exit.Size = new System.Drawing.Size(56, 22);
            this.tlb_Exit.Text = "退   出";
            this.tlb_Exit.Click += new System.EventHandler(this.tlb_Exit_Click);
            // 
            // dlg_OpenFile
            // 
            this.dlg_OpenFile.Filter = "Excel 文件(*.xls)|*.xls";
            this.dlg_OpenFile.Multiselect = true;
            this.dlg_OpenFile.Title = "选择单据文件";
            // 
            // btn_Import
            // 
            this.btn_Import.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Import.Location = new System.Drawing.Point(138, 36);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(137, 43);
            this.btn_Import.TabIndex = 12;
            this.btn_Import.Text = "导入(&I)";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.tlb_Import_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Close.Location = new System.Drawing.Point(390, 36);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(137, 43);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.Text = "退出(&C)";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.tlb_Exit_Click);
            // 
            // frmInBillImp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 139);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.tlbMain);
            this.Name = "frmInBillImp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "入库单导入(Excel)";
            this.Load += new System.EventHandler(this.frmInBillImp_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tlbMain;
        private System.Windows.Forms.ToolStripButton tlb_Setup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlb_SelectAll;
        private System.Windows.Forms.ToolStripButton tlb_UnSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tlb_Import;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tlb_Refresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tlb_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tlb_Exit;
        private System.Windows.Forms.OpenFileDialog dlg_OpenFile;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_Close;
    }
}