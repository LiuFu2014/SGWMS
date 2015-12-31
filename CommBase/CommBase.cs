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
    /// ���԰汾����
    /// </summary>
    public enum LanguageType {ltChinese=0,ltChinese2=1,ltEnglish=2,ltJapan=3,ltHanYu=4};
    /// <summary>
    /// �û����ͣ�utNormal ����ͨ�����û� ��utAdmin ������Ա�û����з���Ȩ�� ��utSupervisor ����������Ա��ӵ������Ȩ��
    /// </summary>
    public enum UserType {utNormal=0,utAdmin = 1,utSupervisor = 2};
    /// <summary>
    /// ��������
    /// </summary>
    public enum OperateType { optNone = 0, optNew = 1, optEdit = 2, optDelete = 3, optUndo = 4, optSave = 5, optRefresh = 6, optPrint = 7, optCheck = 8, optUnCheck = 9 };

    /// <summary>
    /// ö�ٲֿ����ͣ�1=wt3D ����ֿ� 2=wt2D ƽ��ֿ� 2=wtDPS DPS�ֿ� 0=wtNone=ȫ����
    /// </summary>
    [Description("ö�ٲֿ����ͣ�1=wt3D ����ֿ� 2=wt2D ƽ��ֿ� 2=wtDPS DPS�ֿ� 0=ȫ����")]
    public enum WareType { wt3D = 1, wt2D = 2, wtDPS = 3 ,wtNone=0};

    public class AppInfo:Object
    {
        private string _AppTitle = "";
        private string _AppPath = "";
        private string _AppVersion = "";
        private string _AppConfigFile = "";
        private LanguageType _AppLanguage = LanguageType.ltChinese;
        
        /// <summary>
        /// Ӧ�ó������
        /// </summary>
        public string AppTitle
        {
            get { return (_AppTitle.Trim()); }
            set { _AppTitle = value.Trim(); }            
        }

        /// <summary>
        /// Ӧ�ó����·��
        /// </summary>
        public string AppPath
        {
            get { return (_AppPath.Trim()); }
            set { _AppPath = value.Trim(); }
        }

        /// <summary>
        /// Ӧ�ó���İ汾��
        /// </summary>
        public string AppVersion
        {
            get { return (_AppVersion.Trim()); }
            set { _AppVersion = value.Trim(); }
        }

        /// <summary>
        /// Ӧ�ó���������ļ�
        /// </summary>
        public string AppConfigFile
        {
            get { return (_AppConfigFile); }
            set { _AppConfigFile = value; }
        }

        /// <summary>
        /// Ӧ�ó��������
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
        /// �û�����
        /// </summary>
        public string UserId
        {
            get { return (_UserId.Trim()); }
            set { _UserId = value.Trim(); }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            get { return (_UserName.Trim()); }
            set { _UserName = value.Trim(); }
        }

        /// <summary>
        /// �û����ڵĵ�λ����
        /// </summary>
        public string UnitId
        {
            get { return (_UnitId.Trim()); }
            set { _UnitId = value.Trim(); }
        }

        /// <summary>
        /// �û����ڵĵ�λ����
        /// </summary>
        public string UnitName
        {
            get { return (_UnitName.Trim()); }
            set { _UnitName = value.Trim(); }
        }

        /// <summary>
        /// �û����ڵĲ��ű���
        /// </summary>
        public string DeptId
        {
            get { return (_DeptId.Trim()); }
            set { _DeptId = value.Trim(); }
        }

        /// <summary>
        /// �û����ڵĲ�������
        /// </summary>
        public string DeptName
        {
            get { return (_DeptName.Trim()); }
            set { _DeptName = value.Trim(); }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public UserType UType 
        {
            get { return (_UType); }
            set { _UType = value; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string Pwd
        {
            get { return (_Pwd); }
            set { _Pwd = value.Trim(); }
        }

        /// <summary>
        /// �û��Ƿ�����
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
        /// �ж��ַ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="sX">���жϵ��ַ���</param>
        /// <returns>�����Ƿ�Ϊ��������TRUE �� ���� Ϊ��</returns>
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
                sX = "�Ƿ���ֵ";
            }
            return (bX);
        }

        /// <summary>
        /// �ж��Ƿ�Ϊ����
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
                sX = "�Ƿ���ֵ";
            }
            return (bX);
        }

        /// <summary>
        /// �ж��ַ����Ƿ�Ϊ����
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
                sX = "�Ƿ�����ʱ����ֵ";
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
