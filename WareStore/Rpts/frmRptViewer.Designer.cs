namespace WareStore.Rpts
{
    partial class frmRptViewer
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
            this.rptv_Main = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // rptv_Main
            // 
            this.rptv_Main.ActiveViewIndex = -1;
            this.rptv_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rptv_Main.DisplayGroupTree = false;
            this.rptv_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptv_Main.Location = new System.Drawing.Point(0, 0);
            this.rptv_Main.Name = "rptv_Main";
            this.rptv_Main.SelectionFormula = "";
            this.rptv_Main.ShowGroupTreeButton = false;
            this.rptv_Main.Size = new System.Drawing.Size(778, 503);
            this.rptv_Main.TabIndex = 0;
            this.rptv_Main.ViewTimeSelectionFormula = "";
            // 
            // frmRptViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 503);
            this.Controls.Add(this.rptv_Main);
            this.Name = "frmRptViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报表预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rptv_Main;
    }
}