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


namespace Rpts
{
    public static class RptIOBill
    {
        /// <summary>
        /// 打印入库单报表 
        /// </summary>
        /// <param name="dsRpt">主表，明细表的记录集 （数据集主表名：BillIn 明细表：BillInDtl）</param>
        /// <param name="sysTile">公司、单位名称</param>
        /// <param name="sConditon">数据条件描述</param>
        /// <returns>是否打印正确</returns>
        public static bool PrintBillIn(DataSet  dsRpt,string sysTile,string sConditon)
        {
            bool bOK = false;

            WarehouseIOBill.Rpts.frmReportView frmX = new WarehouseIOBill.Rpts.frmReportView();

            WarehouseIOBill.Rpts.rptBillIn rptIn = new WarehouseIOBill.Rpts.rptBillIn();
            WarehouseIOBill.Rpts.rptDataSet rptData = new WarehouseIOBill.Rpts.rptDataSet();
            try
            {
                //MessageBox.Show(rptData.Tables.Count.ToString());
                DataTable tbM = rptData.Tables["BillIn"];
                DataTable tbDtl = rptData.Tables["BillInDtl"];
                rptData.Tables.Clear();
                tbM = dsRpt.Tables["BillIn"].Copy();
                tbDtl = dsRpt.Tables["BillInDtl"].Copy();
                rptData.Tables.Add(tbM);
                rptData.Tables.Add(tbDtl);
                //MessageBox.Show(tbM.Rows.Count.ToString());
                //MessageBox.Show(tbDtl.Rows.Count.ToString());
                rptData.AcceptChanges();
                //MessageBox.Show(rptData.Tables["BillIn"].Rows.Count.ToString());
                //rptData.Tables.Add(dsRpt.Tables["BillIn"]);
                //rptData.Tables.Add(dsRpt.Tables["BillInDtl"]);
                rptIn.SetDataSource(rptData);
                if (rptIn.ParameterFields["SystemName"] != null)
                {
                    //MessageBox.Show(sysTile);
                    //rptIn.ParameterFields["SystemName"].CurrentValues.;
                    //rptIn.SetParameterValue("SystemName", "成都中央级救灾储备库--人");
                    rptIn.SetParameterValue("SystemName", sysTile);
                }
                if (rptIn.ParameterFields["Condition"] != null)
                {
                    rptIn.SetParameterValue("Condition", sConditon);
                }
                frmX.rptViewer.ReportSource = rptIn;
                frmX.ShowDialog();
                rptIn.Dispose();
                frmX.Dispose();
                bOK = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                if (rptIn != null)
                {
                    rptIn.Dispose();
                }
                if (frmX != null)
                {
                    frmX.Dispose();
                }
                bOK = false;
            }
            return bOK;
        }

        /// <summary>
        /// 打印出库单报表 
        /// </summary>
        /// <param name="dsRpt">主表，明细表的记录集 （数据集主表名：BillIn 明细表：BillInDtl）</param>
        /// <param name="sysTile">公司、单位名称</param>
        /// <param name="sConditon">数据条件描述</param>
        /// <returns>是否打印正确</returns>
        public static bool PrintBillOut(DataSet dsRpt, string sysTile, string sConditon)
        {
            bool bOK = false;
            WarehouseIOBill.Rpts.frmReportView frmX = new WarehouseIOBill.Rpts.frmReportView();

            WarehouseIOBill.Rpts.rptBillOut rptIn = new WarehouseIOBill.Rpts.rptBillOut();
            WarehouseIOBill.Rpts.rptDataSet rptData = new WarehouseIOBill.Rpts.rptDataSet();
            try
            {
                
                rptData.Tables.Clear();
                DataTable tbM = dsRpt.Tables["BillOut"].Copy();
                DataTable tbDtl = dsRpt.Tables["BillOutDtl"].Copy();
                //MessageBox.Show(tbDtl.Rows.Count.ToString());
                rptData.Tables.Add(tbM);
                rptData.Tables.Add(tbDtl);
                rptIn.SetDataSource(rptData);
                if (rptIn.ParameterFields["SystemName"] != null)
                {
                    rptIn.SetParameterValue("SystemName", sysTile);
                }
                if (rptIn.ParameterFields["Condition"] != null)
                {
                    rptIn.SetParameterValue("Condition", sConditon);
                }
                frmX.rptViewer.ReportSource = rptIn;
                frmX.ShowDialog();
                rptIn.Dispose();
                frmX.Dispose();
                bOK = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                if (rptIn != null)
                {
                    rptIn.Dispose();
                }
                if (frmX != null)
                {
                    frmX.Dispose();
                }
                bOK = false;
            }
            return bOK;
        }
    }
}
