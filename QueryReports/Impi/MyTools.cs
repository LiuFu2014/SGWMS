using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QueryReports.Impi
{
    class MyTools
    {
        public MyTools()
        { }
        /// <summary>
        /// ����ָ������Ϣ
        /// </summary>
        /// <param name="tipInfo">Ҫ������ʾ����Ϣ</param>
        public static DialogResult MessageBox(string tipInfo)
        {
            return MessageBox(tipInfo, "��ʾ��");
        }

        /// <summary>
        /// ����ָ������Ϣ
        /// </summary>
        /// <param name="tipInfo">Ҫ������ʾ����Ϣ</param>
        /// <param name="tipTitle">����</param>
        public static DialogResult MessageBox(string tipInfo, string tipTitle)
        {
            return System.Windows.Forms.MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        }

        /// <summary>
        /// ����ָ������Ϣ ��ʾ������Ϣ
        /// </summary>
        /// <param name="tipInfo">Ҫ������ʾ����Ϣ</param>
        public static DialogResult MessageBoxByOkCancel(string tipInfo)
        {
            return MessageBoxByOkCancel(tipInfo, "��ʾ��");
        }

        /// <summary>
        /// ����ָ������Ϣ ��ʾ������Ϣ
        /// </summary>
        /// <param name="tipInfo">Ҫ������ʾ����Ϣ</param>
        /// <param name="tipTitle">����</param>
        public static DialogResult MessageBoxByOkCancel(string tipInfo, string tipTitle)
        {
            return System.Windows.Forms.MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        }


    }
}
