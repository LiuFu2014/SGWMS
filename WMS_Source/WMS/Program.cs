namespace WMS
{
    using App;
    using Login;
    using System;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WMSUserInfo objUser = new WMSUserInfo();
            WMSAppInfo objApp = new WMSAppInfo {
                AppPath = Application.StartupPath,
                AppConfigFile = "AppConfig.xml"
            };
            App.frmMain mainForm = new App.frmMain();
            frmFlash flash = new frmFlash {
                SktClient = objApp.SvrSocket
            };
            flash.ShowDialog();
            objApp.SvrSocket = flash.SktClient;
            if (!flash.bIsOK)
            {
                Application.Exit();
            }
            else if (!Login.UserLogin(objApp, objUser))
            {
                Application.Exit();
            }
            else
            {
                objApp.AppTitle = objUser.UnitName + " — 自动化仓库管理系统";
                mainForm.UserInfo = objUser;
                mainForm.AInfo = objApp;
                mainForm.Text = objApp.AppTitle;
                Application.Run(mainForm);
            }
        }
    }
}

