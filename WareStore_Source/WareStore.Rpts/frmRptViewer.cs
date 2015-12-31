namespace WareStore.Rpts
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Windows.Forms;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmRptViewer : Form
    {
        private ReportClass _RptObj = null;
        private Dictionary<string, object> _RptParameters = null;
        private string _RptTitle = "打印预览";
        private IContainer components = null;
        private CrystalReportViewer rptv_Main;

        public frmRptViewer()
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

        private void InitializeComponent()
        {
            this.rptv_Main = new CrystalReportViewer();
            base.SuspendLayout();
            this.rptv_Main.ActiveViewIndex = -1;
            this.rptv_Main.BorderStyle = BorderStyle.FixedSingle;
            this.rptv_Main.DisplayGroupTree=false;
            this.rptv_Main.Dock = DockStyle.Fill;
            this.rptv_Main.Location = new Point(0, 0);
            this.rptv_Main.Name = "rptv_Main";
            this.rptv_Main.SelectionFormula="";
            this.rptv_Main.ShowGroupTreeButton=false;
            this.rptv_Main.Size = new Size(0x30a, 0x1f7);
            this.rptv_Main.TabIndex = 0;
            this.rptv_Main.ViewTimeSelectionFormula="";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x30a, 0x1f7);
            base.Controls.Add(this.rptv_Main);
            base.Name = "frmRptViewer";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "报表预览";
            base.WindowState = FormWindowState.Maximized;
            base.ResumeLayout(false);
        }

        public bool SetReport()
        {
            bool flag = false;
            if (this._RptObj == null)
            {
                return flag;
            }
            if (this._RptParameters != null)
            {
                foreach (string str in this._RptParameters.Keys)
                {
                    this._RptObj.SetParameterValue(str, this._RptParameters[str]);
                }
            }
            this.rptv_Main.ReportSource=this._RptObj;
            return true;
        }

        public ReportClass RptObj
        {
            get
            {
                return this._RptObj;
            }
            set
            {
                this._RptObj = value;
            }
        }

        public Dictionary<string, object> RptParameters
        {
            get
            {
                return this._RptParameters;
            }
            set
            {
                this._RptParameters = value;
            }
        }

        public string RptTitle
        {
            get
            {
                return this._RptTitle.Trim();
            }
            set
            {
                this._RptTitle = value;
                this.Text = this._RptTitle;
            }
        }
    }
}

