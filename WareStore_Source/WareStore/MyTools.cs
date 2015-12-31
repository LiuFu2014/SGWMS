namespace WareStore
{
    using System;
    using System.Windows.Forms;

    internal class MyTools
    {
        public static DialogResult MessageBox(string tipInfo)
        {
            return MessageBox(tipInfo, "提示！");
        }

        public static DialogResult MessageBox(string tipInfo, string tipTitle)
        {
            return System.Windows.Forms.MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult MessageBoxByOkCancel(string tipInfo)
        {
            return MessageBoxByOkCancel(tipInfo, "提示！");
        }

        public static DialogResult MessageBoxByOkCancel(string tipInfo, string tipTitle)
        {
            return System.Windows.Forms.MessageBox.Show(tipInfo, tipTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
    }
}

