namespace WareHouseTask.Rpts
{
    using CrystalDecisions.Windows.Forms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmReportView : Form
    {
        private IContainer components = null;
        public CrystalReportViewer rptViewer;

        public frmReportView()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmReportView_Load(object sender, EventArgs e)
        {
            this.rptViewer.RefreshReport();
        }

        private void InitializeComponent()
        {
            this.rptViewer = new CrystalReportViewer();
            base.SuspendLayout();
            this.rptViewer.ActiveViewIndex=-1;
            this.rptViewer.BorderStyle = BorderStyle.FixedSingle;
            this.rptViewer.DisplayGroupTree=false;
            this.rptViewer.Dock = DockStyle.Fill;
            this.rptViewer.Location = new Point(0, 0);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.SelectionFormula="";
            this.rptViewer.ShowGroupTreeButton=false;
            this.rptViewer.Size = new Size(0x2ec, 0x1a9);
            this.rptViewer.TabIndex = 0;
            this.rptViewer.ViewTimeSelectionFormula="";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2ec, 0x1a9);
            base.Controls.Add(this.rptViewer);
            base.MinimizeBox = false;
            base.Name = "frmReportView";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "frmReportView";
            base.Load += new EventHandler(this.frmReportView_Load);
            base.ResumeLayout(false);
        }
    }
}

