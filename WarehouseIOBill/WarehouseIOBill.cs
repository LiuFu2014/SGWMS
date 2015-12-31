using System;
using System.Collections.Generic;
using System.Text;
using Comm.IF;
using CommBase;
using App;
using System.Data.SqlClient;
using System.Data;
using DBCommInfo;

namespace SunEast.App
{
    public class WarehouseIOBill : IFApplication
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
            //SunEast.App.frmBillOut frmtt = new frmBillOut();
            
            UI.FrmSTable frmX = null;
            if (objModule == string.Empty || objModule.Trim() == "") //默认为 User
            {
                frmX = new SunEast.App.frmBillIn();
                frmX.ModuleRtsId = "3101";
                frmX.ModuleRtsName = "入库单据管理";
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
                    case "3101"://入库
                        frmX = new SunEast.App.frmBillIn();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "入库单据管理";
                        ((SunEast.App.frmBillIn)frmX).WTWareType = WareType.wtNone;
                        break;
                    case "3201"://出库单
                        frmX = new SunEast.App.frmBillOut();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "出库单据管理";                        
                        ((SunEast.App.frmBillOut)frmX).WTWareType = WareType.wtNone;
                        break;
                    case "3107"://DPS
                        frmX = new SunEast.App.frmCheckAccept();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "入库验收单管理";
                        ((SunEast.App.frmCheckAccept)frmX).WTWareType = WareType.wtNone;
                        break;
                    //case "3305"://出库单据管理  DPS
                    //    frmX = new SunEast.App.frmpBillOut();
                    //    frmX.ModuleRtsId = objModule.Trim().ToLower();
                    //    frmX.ModuleRtsName = "出库单据管理";
                    //    ((SunEast.App.frmpBillOut)frmX).WTWareType = WareType.wtDPS;
                    //    break;
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
    }
}
