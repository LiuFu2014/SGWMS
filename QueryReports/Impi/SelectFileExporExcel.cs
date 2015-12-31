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
        /// ͨ�����Ϊ�Ի���ѡ��Ҫ�����ļ���·��
        /// </summary>
        /// <returns></returns>
        public static string GetSaveFileNameByDiag()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            save.Title = "�����ļ����Ϊ";
            save.InitialDirectory = "d:\\";
            save.Filter = "�����ļ�|*.*|Excel�ļ�|*.xls";
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
