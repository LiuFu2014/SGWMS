namespace DataImpExp
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class DataIE
    {
        public static void DataGridViewToExcel(DataGridView grdX, string sFile, string sTitle)
        {
            frmProgressDataToExcel excel = new frmProgressDataToExcel {
                DataSourceType = IEDataSourceType.dstDataGridView,
                GrdData = grdX,
                RptTitle = sTitle.Trim(),
                FileName = sFile
            };
            excel.ShowDialog();
            excel.Dispose();
        }

        public static void DataTableToExcel(DataTable tbData, string sFile, string sTitle)
        {
            frmProgressDataToExcel excel = new frmProgressDataToExcel {
                DataSourceType = IEDataSourceType.dstDataTable,
                TbData = tbData,
                RptTitle = sTitle.Trim(),
                FileName = sFile
            };
            excel.ShowDialog();
            excel.Dispose();
        }

        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void KillProgress(IntPtr hHwnd)
        {
            int iD = 0;
            try
            {
                GetWindowThreadProcessId(hHwnd, out iD);
                Process.GetProcessById(iD).Kill();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void KillProgress(string progressName, DateTime dBegin, DateTime dEnd)
        {
            Process[] processes = null;
            if (Process.GetProcessesByName(progressName).Length == 0)
            {
                processes = Process.GetProcesses();
            }
            processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (!process.HasExited)
                {
                    DateTime startTime = process.StartTime;
                    if ((startTime >= dBegin) && (startTime <= dEnd))
                    {
                        process.Kill();
                        break;
                    }
                }
            }
        }
    }
}

