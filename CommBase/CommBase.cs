using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Runtime.InteropServices;
namespace CommBase
{
    /// <summary>
    /// 语言版本类型
    /// </summary>
    public enum LanguageType {ltChinese=0,ltChinese2=1,ltEnglish=2,ltJapan=3,ltHanYu=4};
    /// <summary>
    /// 用户类型：utNormal ：普通操作用户 ；utAdmin ：管理员用户，有分配权限 ；utSupervisor ：超级管理员，拥有所有权限
    /// </summary>
    public enum UserType {utNormal=0,utAdmin = 1,utSupervisor = 2};
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperateType { optNone = 0, optNew = 1, optEdit = 2, optDelete = 3, optUndo = 4, optSave = 5, optRefresh = 6, optPrint = 7, optCheck = 8, optUnCheck = 9 };

    /// <summary>
    /// 枚举仓库类型（1=wt3D 立库仓库 2=wt2D 平面仓库 2=wtDPS DPS仓库 0=wtNone=全部）
    /// </summary>
    [Description("枚举仓库类型（1=wt3D 立库仓库 2=wt2D 平面仓库 2=wtDPS DPS仓库 0=全部）")]
    public enum WareType { wt3D = 1, wt2D = 2, wtDPS = 3 ,wtNone=0};

    public class AppInfo:Object
    {
        private string _AppTitle = "";
        private string _AppPath = "";
        private string _AppVersion = "";
        private string _AppConfigFile = "";
        private LanguageType _AppLanguage = LanguageType.ltChinese;
        
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string AppTitle
        {
            get { return (_AppTitle.Trim()); }
            set { _AppTitle = value.Trim(); }            
        }

        /// <summary>
        /// 应用程序的路径
        /// </summary>
        public string AppPath
        {
            get { return (_AppPath.Trim()); }
            set { _AppPath = value.Trim(); }
        }

        /// <summary>
        /// 应用程序的版本号
        /// </summary>
        public string AppVersion
        {
            get { return (_AppVersion.Trim()); }
            set { _AppVersion = value.Trim(); }
        }

        /// <summary>
        /// 应用程序的配置文件
        /// </summary>
        public string AppConfigFile
        {
            get { return (_AppConfigFile); }
            set { _AppConfigFile = value; }
        }

        /// <summary>
        /// 应用程序的语言
        /// </summary>
        public LanguageType AppLanguage
        {
            get { return _AppLanguage; }
            set { _AppLanguage = value; }
        }
        
    }
    public class UserInfo:Object
    {
        private string _UserId = "";
        private string _UserName = "";
        private string _UnitId = "";
        private string _UnitName = "";
        private string _DeptId = "";
        private string _DeptName = "";
        private string _Pwd = "";
        private bool _IsUsed = true;
        private UserType _UType = UserType.utNormal;
        
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserId
        {
            get { return (_UserId.Trim()); }
            set { _UserId = value.Trim(); }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return (_UserName.Trim()); }
            set { _UserName = value.Trim(); }
        }

        /// <summary>
        /// 用户所在的单位编码
        /// </summary>
        public string UnitId
        {
            get { return (_UnitId.Trim()); }
            set { _UnitId = value.Trim(); }
        }

        /// <summary>
        /// 用户所在的单位名称
        /// </summary>
        public string UnitName
        {
            get { return (_UnitName.Trim()); }
            set { _UnitName = value.Trim(); }
        }

        /// <summary>
        /// 用户所在的部门编码
        /// </summary>
        public string DeptId
        {
            get { return (_DeptId.Trim()); }
            set { _DeptId = value.Trim(); }
        }

        /// <summary>
        /// 用户所在的部门名称
        /// </summary>
        public string DeptName
        {
            get { return (_DeptName.Trim()); }
            set { _DeptName = value.Trim(); }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UType 
        {
            get { return (_UType); }
            set { _UType = value; }
        }

        /// <summary>
        /// 用户口令
        /// </summary>
        public string Pwd
        {
            get { return (_Pwd); }
            set { _Pwd = value.Trim(); }
        }

        /// <summary>
        /// 用户是否被启用
        /// </summary>
        public bool IsUsed
        {
            get { return (_IsUsed); }
            set { _IsUsed = value; }
        }
    }

    public static class PubFuns
    {

        /// <summary>
        /// 判断字符串是否为浮点数
        /// </summary>
        /// <param name="sX">被判断的字符串</param>
        /// <returns>返回是否为浮点数，TRUE 是 否则 为否</returns>
        public static  bool IsNumberic(string sX)
        {
            bool bX = false;
            double nX = -0;
            if (sX == null) return (false);
            if (sX.Trim() == "") return (false);
            try
            {
                nX = double.Parse(sX);
                bX = true;
            }
            catch (Exception err)
            {
                sX = "非法数值";
            }
            return (bX);
        }

        /// <summary>
        /// 判断是否为整数
        /// </summary>
        /// <param name="sX"></param>
        /// <returns></returns>
        public static bool IsInteger(string sX)
        {
            bool bX = false;
            Int64 nX = -0;
            if (sX == null) return (false);
            if (sX.Trim() == "") return (false);
            try
            {
                nX = Int64.Parse(sX);
                bX = true;
            }
            catch (Exception err)
            {
                sX = "非法数值";
            }
            return (bX);
        }

        /// <summary>
        /// 判断字符串是否为日期
        /// </summary>
        /// <param name="sX"></param>
        /// <returns></returns>
        public static bool IsDateTime(string sX)
        {
            bool bX = false;
            DateTime dX = DateTime.Now;
            if (sX == null) return (false);
            if (sX.Trim() == "") return (false);
            try
            {
                dX = DateTime.Parse(sX);
                bX = true;
            }
            catch (Exception err)
            {
                sX = "非法日期时间数值";
            }
            return (bX);
        }

        [DllImport("CoreDll.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
        byte bVk,
        byte bScan,
        int dwFlags,
        int dwExtraInfo
        );
    }
}
