using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WareStore.Rpts
{
    public partial class FrmRptShow : Form
    {
        public static DataSet dsRpt = new DataSet();       
        public static string CountType = "0";
        public static string[] Paramets = null;
        public static string rpsTitleStr = "";
        public FrmRptShow()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                switch (FrmRptShow.CountType)
                {
                    case "0":
                        InOutRece S0 = new InOutRece();
                        S0.SetDataSource(dsRpt.Tables["InOutRece"]);
                        if (Paramets != null)
                        {
                            S0.SetParameterValue("statusTime",Paramets[0]);
                            S0.SetParameterValue("endTime", Paramets[1]);
                            S0.SetParameterValue("userName", Paramets[2]);
                            S0.SetParameterValue("WHName", Paramets[3]);
                            S0.SetParameterValue("matInfo", Paramets[4]);
                            S0.SetParameterValue("rpsTitleStr", rpsTitleStr);
                        }
                        this.crystalReportViewer1.ReportSource = S0;
                        break;
                    case "1":
                        InOutReceAll S0All = new InOutReceAll();
                        S0All.SetDataSource(dsRpt.Tables["InOutReceAll"]);
                        if (Paramets != null)
                        {
                            S0All.SetParameterValue("statusTime", Paramets[0]);
                            S0All.SetParameterValue("endTime", Paramets[1]);
                            S0All.SetParameterValue("userName", Paramets[2]);
                            S0All.SetParameterValue("WHName", Paramets[3]);
                            S0All.SetParameterValue("matInfo", Paramets[4]);
                            S0All.SetParameterValue("rpsTitleStr", rpsTitleStr);
                        }
                        this.crystalReportViewer1.ReportSource = S0All;
                        break;
                 
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}