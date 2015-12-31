using System;
using System.Collections.Generic;
using System.Text;
using Comm.IF;
using CommBase;
using App;
using System.Data.SqlClient;
using System.Data;
using DBCommInfo;
using UserMS;

namespace SunEast.App
{
    public class UserManager : IFApplication 
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
            UI.FrmSTable frmX = null;
            if (objModule == string.Empty || objModule.Trim() == "") //默认为 User
            {
                frmX = new frmCompany();
                frmX.ModuleRtsId = "2101";
                frmX.ModuleRtsName = "单位信息管理";
            }
            else
            {
                /*
                    2101	单位信息管理
                    2102	部门信息管理
                    2103	用户信息管理
                    2104	权限管理

                */
                switch (objModule.Trim().ToLower())
                {
                    case "2101"://单位信息管理
                        frmX = new frmCompany();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "单位信息管理";
                        break;
                   case "2102"://部门信息管理
                       frmX = new frmDept();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "部门信息管理";
                        break;
                    case "2103"://用户信息管理
                        frmX = new frmUserInfo();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "用户信息管理";
                        break;
                    case "2104"://权限管理
                        frmX = new frmUserRight();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "权限管理";
                        break;
                    case "2114"://权限管理
                        frmSupplier.sCondition = " and nType=0 ";
                        frmSupplier.type = 0;
                        frmX = new frmSupplier();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "供应商管理";
                        break;
                    case "2115"://权限管理
                        frmSupplier.sCondition = " and nType=1 ";
                        frmSupplier.type = 1;
                        frmX = new frmSupplier();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "客户管理";
                        break;
                }
                frmX.AppInformation = objApp;
                frmX.UserInformation = objUser;
                frmX.InitFormParameters();
                frmX.ShowDialog();
                frmX.Dispose();
            }
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

        //public static DataSet GetDataSetbySql(string select)
        //{
        //    DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
        //    cmdInfo.SqlText = select;
        //    cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
        //    cmdInfo.PageIndex = 0;                                          //需要分页时的页号
        //    cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
        //    cmdInfo.FromSysType = "dotnet";
        //    SunEast.SeDBClient sdcX = new SeDBClient();
        //    string sErr = "";
        //    DataSet ds = sdcX.GetDataSet(cmdInfo, out sErr);
        //    return ds;
        //}

        /// <summary>
        /// 选择客户或供应商方法
        /// </summary>
        /// <param name="objApp">应用程序对象</param>
        /// <param name="objUser">用户对象</param>
        /// <param name="csType">客户供应商类型</param>
        /// <param name="nIsInner">是否内部单位 -1 全部 0 否 1 是</param>
        /// <param name="nIsFacctory">是否生产厂家 -1 全部 0 否 1 是（仅对供应商而言）</param>
        /// <param name="sCSName">搜索的名称</param>
        /// <param name="doSelCuSpplier">处理选择的事件方法</param>
        public static void SelectCuSupplier( WMSAppInfo objApp,WMSUserInfo objUser, CSType csType,int nIsInner,int nIsFacctory,string sCSName, DoSelCuSupplierEvent doSelCuSpplier)
        {
            frmSelCuSupplier frmX = new frmSelCuSupplier();
            frmX.AppInformation = objApp;
            frmX.UserInformation = objUser;
            frmX.DoSelCuSupplier = doSelCuSpplier;
            frmX.CuSupplierType = csType;
            frmX.IsInner = nIsInner;
            frmX.IsFactory = nIsFacctory;
            frmX.txt_cName.Text = sCSName.Trim();
            frmX.ShowDialog();
            frmX.Dispose();
            
        }
    }
}
