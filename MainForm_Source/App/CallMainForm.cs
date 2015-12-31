namespace App
{
    using System;

    public class CallMainForm
    {
        public static void DoShowMainForm(WMSAppInfo aInfo, WMSUserInfo uInfo, bool bIsMDIMode)
        {
            frmMain main = new frmMain();
            if (bIsMDIMode)
            {
                main.IsMdiContainer = bIsMDIMode;
                main.AInfo = aInfo;
                main.UserInfo = uInfo;
                main.ShowDialog();
                main.Dispose();
            }
        }
    }
}

