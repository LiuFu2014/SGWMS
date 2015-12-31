using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Comm.IF;
using CommBase;
using App;
using System.Data.SqlClient;
using System.Data;
using DBCommInfo;
using System.Reflection;

namespace SunEast.App
{
    public class QueryReports : IFApplication
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
                frmX = new SunEast.App.FrmStockDtl();
                frmX.ModuleRtsId = "5101";
                frmX.ModuleRtsName = "库存明细报表";
            }
            else
            {
                switch (objModule.Trim().ToLower())
                {
                    case "5101":
                        frmX = new SunEast.App.FrmStockDtl();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "库存明细报表";
                        break;
                    case "5102":
                        frmX = new SunEast.App.FrmStockCount();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "库存统计报表";
                        break;
                    case "5106":
                        frmX = new SunEast.App.FrmSysLog();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "系统日志";
                        break;
                    case "5103":
                        frmX = new SunEast.App.FrmStoreHisList();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "库存流水账";
                        break;
                    case "5104":
                        frmX = new SunEast.App.FrmSafeAlarm();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "安全库存报警";
                        break;
                    case "5105":
                        frmX = new SunEast.App.FrmUnkeepList();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "物料有效期报警";
                        break;
                    case "3503":
                        frmX = new SunEast.App.FrmUnkeepList();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "物料有效期报警";
                        break;
                    case "5107":
                        frmX = new SunEast.App.frmCountWareCell();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "货位统计";
                        break;
                    case "5108":
                        frmX = new SunEast.App.frmCountForIn();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "入库救灾物资统计";
                        break;
                    case "5109":
                        frmX = new SunEast.App.frmCountForOut();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "出库救灾物资统计";
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

        public static DataSet GetDataSetbySql(WMSAppInfo objApp, string select)
        {
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = select;
            cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";
            SunEast.SeDBClient sdcX = new SeDBClient();
            string sErr = "";
            DataSet ds = sdcX.GetDataSet(objApp.SvrSocket, cmdInfo,false , out sErr); //sdcX.GetDataSet(cmdInfo, out sErr);
            return ds;
        }
    }
}