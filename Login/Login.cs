using System;
using System.Collections.Generic;
using System.Text;
using Comm.IF;
using CommBase;
using App;
namespace Login
{
    public class Login :IFApplication 
    {
        /// <summary>
        /// 以模式方式加载主窗体
        /// </summary>
        /// <param name="objApp"> 应用程序对象 </param>
        /// <param name="objUser"> 当前登录用户对象 </param>
        //public void LoadByModeForm(WMSAppInfo objApp, WMSUserInfo objUser)
        //{
        //    LoadByModeForm(objApp, objUser, null);
        //}

        /// <summary>
        /// 以模式方式加载主窗体
        /// </summary>
        /// <param name="objApp">应用程序对象</param>
        /// <param name="objUser">当前登录用户对象</param>
        /// <param name="objModule">调用模块</param>       
        public void LoadByModeForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
            frmMain frmX = new frmMain();
            frmX.uInfo = objUser;
            frmX.AppInformation = objApp;
            //frmX.SktClient = objApp.SvrSocket;
            frmX.ShowDialog();
            frmX.Dispose();
        }
        public static bool UserLogin(WMSAppInfo objApp, WMSUserInfo objUser)
        {
            bool bOK = false;
            frmMain frmX = new frmMain();
            frmX.uInfo = objUser;
            frmX.AppInformation = objApp;
            //frmX.SktClient = objApp.SvrSocket;
            frmX.cmbCmpt.Enabled = true;
            frmX.cmbDept.Enabled = true;
            frmX.cmbUser.Enabled = true;
            frmX.ShowDialog();
            bOK = frmX.IsOK;
            frmX.Dispose();
            return (bOK);

        }

        public static bool UserCheck(WMSAppInfo objApp, WMSUserInfo objUser)
        {
            bool bOK = false;
            frmMain frmX = new frmMain();
            frmX.Text = "用户验证";
            frmX.uInfo = objUser;
            frmX.AppInformation = objApp;
            //frmX.SktClient = objApp.SvrSocket;
            //frmX.cmbCmpt.SelectedValue = objUser.UnitId;
            //frmX.cmbCmpt.Enabled = false ;
            //frmX.cmbDept.SelectedValue = objUser.DeptId; ;
            //frmX.cmbDept.Enabled = false;
            //frmX.cmbUser.SelectedValue = objUser.UserId;
            //frmX.cmbUser.Enabled = true ;
            frmX.IsUserCheck = true;
            frmX.ShowDialog();
            bOK = frmX.IsOK;
            frmX.Dispose();
            return (bOK);
        }

        /// <summary>
        /// 验证 用户与口令是否合法
        /// </summary>
        /// <param name="aX"> 应用程序对象</param>
        /// <param name="uX">用户对象</param>
        /// <param name="sPwd">校验密码</param>
        /// <returns>返回是否成功</returns>
        public static bool CheckUserPwdIsOk(App.WMSAppInfo aX, App.WMSUserInfo uX, string sPwd)
        {
            return frmMain.CheckUserPwdIsOK(aX, uX, sPwd);
        }

        /// <summary>
        /// 以子窗体方式加载主窗体
        /// </summary>
        /// <param name="objApp"> 应用程序对象 </param>
        /// <param name="objUser"> 当前登录用户对象 </param>
        //public void LoadByChildForm(WMSAppInfo objApp, WMSUserInfo objUser)
        //{
        //    LoadByChildForm(objApp, objUser, null);
        //}

        /// <summary>
        /// 以子窗体方式加载主窗体
        /// </summary>
        /// <param name="objApp">应用程序对象</param>
        /// <param name="objUser">当前登录用户对象</param>
        /// <param name="objModule">调用模块</param>
        public void LoadByChildForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
        }
    }
}
