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
        /// 弹出指定的信息
        /// </summary>
        /// <param name="tipInfo">要进行提示的信息</param>
        public static DialogResult MessageBox(string tipInfo)
        {
            return MessageBox(tipInfo, "提示！");
        }

        /// <summary>
        /// 弹出指定的信息
        /// </summary>
        /// <param name="tipInfo">要进行提示的信息</param>
        /// <param name="tipTitle">标题</param>
        public static DialogResult MessageBox(string tipInfo, string tipTitle)
        {
            return System.Windows.Forms.MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        }

        /// <summary>
        /// 弹出指定的信息 显示标题信息
        /// </summary>
        /// <param name="tipInfo">要进行提示的信息</param>
        public static DialogResult MessageBoxByOkCancel(string tipInfo)
        {
            return MessageBoxByOkCancel(tipInfo, "提示！");
        }

        /// <summary>
        /// 弹出指定的信息 显示标题信息
        /// </summary>
        /// <param name="tipInfo">要进行提示的信息</param>
        /// <param name="tipTitle">标题</param>
        public static DialogResult MessageBoxByOkCancel(string tipInfo, string tipTitle)
        {
            return System.Windows.Forms.MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        }


    }
}
