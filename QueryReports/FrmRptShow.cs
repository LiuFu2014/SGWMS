using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBCommInfo;
using SunEast;
using CrystalDecisions.CrystalReports.Engine;
using SunEast.App;
using QueryReports;

namespace SunEast.App
{
    public partial class FrmRptShow : UI.FrmSTable
    {
        public FrmRptShow()
        {
            InitializeComponent();
        }
        #region  Ù–‘
        private string _SystemTitle = "";
        public string SystemTitle
        {
            get { return _SystemTitle ; }
            set { _SystemTitle = value; }
        }

        private string _Condition = "";
        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }
        #endregion
        public static DataSet rptds = new DataSet();
        public static string rptType="";
        private void FrmRptShow_Load(object sender, EventArgs e)
        {
            try
            {
                switch (rptType)
                {
                    case "StoreHisList":
                        StoreHisList S0 = new StoreHisList();
                        S0.SetDataSource(rptds.Tables["StoreHisList"]);
                        if (S0.ParameterFields["SystemName"] != null)
                        {
                            S0.SetParameterValue("SystemName", _SystemTitle);
                        }
                        if (S0.ParameterFields["Condition"] != null)
                        {
                            S0.SetParameterValue("Condition", _Condition);
                        }
                        this.crystalReportViewer1.ReportSource = S0;
                        break;
                    case "UnKeepList":
                        UnKeepList S1 = new UnKeepList();
                        S1.SetDataSource(rptds.Tables["UnKeepList"]);
                        //if (S1.ParameterFields["SystemName"] != null)
                        //{
                        //    S1.SetParameterValue("SystemName", _SystemTitle);
                        //}
                        //if (S1.ParameterFields["Condition"] != null)
                        //{
                        //    S1.SetParameterValue("Condition", _Condition);
                        //}
                        this.crystalReportViewer1.ReportSource = S1;
                        break;
                    case "SysLog":
                        SysLog S2 = new SysLog();
                        
                        this.crystalReportViewer1.ReportSource = S2;
                        if (S2.ParameterFields["SystemName"] != null)
                        {
                            S2.SetParameterValue("SystemName", _SystemTitle);
                        }
                        if (S2.ParameterFields["Condition"] != null)
                        {
                            S2.SetParameterValue("Condition", _Condition);
                        }
                        S2.SetDataSource(rptds.Tables["SysLog"]);
                        break;
                    case "SafeAlarm":
                        SafeAlarm S = new SafeAlarm();
                        S.SetDataSource(rptds.Tables["SafeAlarm"]);
                        S.SetParameterValue("safeTitel", SystemTitle);
                        //if (S.ParameterFields["SystemName"] != null)
                        //{
                        //    S.SetParameterValue("SystemName", _SystemTitle);
                        //}
                        //if (S.ParameterFields["Condition"] != null)
                        //{
                        //    S.SetParameterValue("Condition", _Condition);
                        //}
                        this.crystalReportViewer1.ReportSource = S;
                        break;
                    case "WareCellCount":
                        CountWareCell S3 = new CountWareCell();
                        S3.SetDataSource(rptds.Tables["WareCellCount"]);
                        if (S3.ParameterFields["SystemName"] != null)
                        {
                            S3.SetParameterValue("SystemName", _SystemTitle);
                        }
                        if (S3.ParameterFields["Condition"] != null)
                        {
                            S3.SetParameterValue("Condition", _Condition );
                        }
                        this.crystalReportViewer1.ReportSource = S3;
                        break;
                    case "OutList_Ext":
                        rptOutListExt S4 = new rptOutListExt();
                        S4.SetDataSource(rptds.Tables["IOList_Ext"]);
                        if (S4.ParameterFields["SystemName"] != null)
                        {
                            S4.SetParameterValue("SystemName", _SystemTitle);
                        }
                        if (S4.ParameterFields["Condition"] != null)
                        {
                            S4.SetParameterValue("Condition", _Condition);
                        }
                        this.crystalReportViewer1.ReportSource = S4;
                        break;
                    case "InList_Ext":
                        rptInListExt S5 = new rptInListExt();
                        S5.SetDataSource(rptds.Tables["IOList_Ext"]);
                        if (S5.ParameterFields["SystemName"] != null)
                        {
                            S5.SetParameterValue("SystemName", _SystemTitle);
                        }
                        if (S5.ParameterFields["Condition"] != null)
                        {
                            S5.SetParameterValue("Condition", _Condition);
                        }
                        this.crystalReportViewer1.ReportSource = S5;
                        break;
                    case "stockCountByPall":
                        StockDtlRpt0 S6 = new StockDtlRpt0();
                        S6.SetDataSource(rptds.Tables["stockCountByPall"]);                        
                        this.crystalReportViewer1.ReportSource = S6;
                        break;
                    case "stockCountByAll":
                        StockDtlRpt1 S7 = new StockDtlRpt1();
                        S7.SetDataSource(rptds.Tables["stockCountByAll"]);
                        this.crystalReportViewer1.ReportSource = S7;
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

