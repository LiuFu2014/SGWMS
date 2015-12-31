using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using SunEast;
using CrystalDecisions.CrystalReports.Engine;
using SunEast.App;
using QueryReports;
namespace SunEast.App
{
    public partial class FrmStockDtlRpt : UI.FrmSTable
    {
        public static DataSet dsRpt = new DataSet();
        public static string count = "";
        public FrmStockDtlRpt()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                switch (FrmStockCount.CountType)
                {
                    case "0":
                        StockDtlRpt0 S0 = new StockDtlRpt0();
                        S0.SetDataSource(dsRpt.Tables["StockDtl0"]);
                        this.crystalReportViewer1.ReportSource = S0;
                        break;
                    case "1":
                        StockDtlRpt1 S1 = new StockDtlRpt1();
                        S1.SetDataSource(dsRpt.Tables["StockDtl1"]);
                        this.crystalReportViewer1.ReportSource = S1;
                        break;
                    case "2":
                        StockDtlRpt2 S2 = new StockDtlRpt2();
                        S2.SetDataSource(dsRpt.Tables["StockDtl2"]);
                        this.crystalReportViewer1.ReportSource = S2;
                        break;
                    case "3":
                        StockDtlRpt3 S3 = new StockDtlRpt3();
                        S3.SetDataSource(dsRpt.Tables["StockDtl3"]);
                        this.crystalReportViewer1.ReportSource = S3;
                        break;
                    case "99":
                        StockDtl S = new StockDtl();
                        S.SetDataSource(dsRpt.Tables["StockDtl"]);
                        this.crystalReportViewer1.ReportSource = S;
                        break;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void FrmStockDtlRpt_Load(object sender, EventArgs e)
        {

        }
    }
}

