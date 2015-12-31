using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QueryReports.Impi
{
    public class SelectFileExporExcel
    {
        public SelectFileExporExcel()
        { }

        /// <summary>
        /// 通过另存为对话框选择要保存文件的路径
        /// </summary>
        /// <returns></returns>
        public static string GetSaveFileNameByDiag()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            save.Title = "导出文件另存为";
            save.InitialDirectory = "d:\\";
            save.Filter = "所有文件|*.*|Excel文件|*.xls";
            save.FilterIndex = 0;

            if (save.ShowDialog() != DialogResult.OK)
            {
                return "";
            }
            return save.FileName;
            return "";
        }
    }
}
