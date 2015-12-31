namespace WareStore.Rpts
{
    using CrystalDecisions.Windows.Forms;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class FrmRptShow : Form
    {
        private IContainer components = null;
        public static string CountType = "0";
        private CrystalReportViewer crystalReportViewer1;
        public static DataSet dsRpt = new DataSet();
        public static string[] Paramets = null;
        public static string rpsTitleStr = "";

        public FrmRptShow()
        {
            this.InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                InOutReceAll all;
                string countType = CountType;
                if (countType != null)
                {
                    if (!(countType == "0"))
                    {
                        if (countType == "1")
                        {
                            goto Label_00E3;
                        }
                    }
                    else
                    {
                        InOutRece rece = new InOutRece();
                        rece.SetDataSource(dsRpt.Tables["InOutRece"]);
                        if (Paramets != null)
                        {
                            rece.SetParameterValue("statusTime", Paramets[0]);
                            rece.SetParameterValue("endTime", Paramets[1]);
                            rece.SetParameterValue("userName", Paramets[2]);
                            rece.SetParameterValue("WHName", Paramets[3]);
                            rece.SetParameterValue("matInfo", Paramets[4]);
                            rece.SetParameterValue("rpsTitleStr", rpsTitleStr);
                        }
                        this.crystalReportViewer1.ReportSource=rece;
                    }
                }
                return;
            Label_00E3:
                all = new InOutReceAll();
                all.SetDataSource(dsRpt.Tables["InOutReceAll"]);
                if (Paramets != null)
                {
                    all.SetParameterValue("statusTime", Paramets[0]);
                    all.SetParameterValue("endTime", Paramets[1]);
                    all.SetParameterValue("userName", Paramets[2]);
                    all.SetParameterValue("WHName", Paramets[3]);
                    all.SetParameterValue("matInfo", Paramets[4]);
                    all.SetParameterValue("rpsTitleStr", rpsTitleStr);
                }
                this.crystalReportViewer1.ReportSource=all;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalReportViewer();
            base.SuspendLayout();
            this.crystalReportViewer1.ActiveViewIndex=-1;
            this.crystalReportViewer1.BorderStyle = BorderStyle.FixedSingle;
            this.crystalReportViewer1.DisplayGroupTree=false;
            this.crystalReportViewer1.Dock = DockStyle.Fill;
            this.crystalReportViewer1.Location = new Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula="";
            this.crystalReportViewer1.ShowGroupTreeButton=false;
            this.crystalReportViewer1.Size = new Size(0x361, 0x1e4);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ViewTimeSelectionFormula="";
            this.crystalReportViewer1.Load += new EventHandler(this.crystalReportViewer1_Load);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x361, 0x1e4);
            base.Controls.Add(this.crystalReportViewer1);
            base.MinimizeBox = false;
            base.Name = "FrmRptShow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "RptInOutRece";
            base.ResumeLayout(false);
        }
    }
}

