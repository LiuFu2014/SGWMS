    using System;
    using System.Data;
    using System.Windows.Forms;
    using WarehouseTask.Rpts;
namespace WareHouseTask.Rpts
{


    public static class RptTask
    {
        public static bool PrintTaskDtlList(DataSet dsRpt, string sysTile, string sConditon)
        {
            frmReportView view = new frmReportView {
                Text = "任务清单报表"
            };
            rptTaskList list = new rptTaskList();
            RptData data = new RptData();
            try
            {
                data.Tables.Clear();
                data.Tables.Add(dsRpt.Tables["tbTaskDtlList"].Copy());
                data.AcceptChanges();
                list.SetDataSource(data);
                if (list.ParameterFields["SystemName"] != null)
                {
                    list.SetParameterValue("SystemName", sysTile);
                }
                if (list.ParameterFields["Condition"] != null)
                {
                    list.SetParameterValue("Condition", sConditon);
                }
                view.rptViewer.ReportSource=list;
                view.ShowDialog();
                list.Dispose();
                view.Dispose();
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                if (list != null)
                {
                    list.Dispose();
                }
                if (view != null)
                {
                    view.Dispose();
                }
                return false;
            }
        }
    }
}

