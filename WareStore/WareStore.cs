using System;
using System.Collections.Generic;
using System.Text;
using Comm.IF;
using CommBase;
using App;
using System.Data.SqlClient;
using System.Data;
using DBCommInfo;
using WareStoreMS;
using WareStore;

namespace SunEast.App
{
    public class WareStore : IFApplication
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
                frmX = new FrmStockMCheck();
                frmX.ModuleRtsId = "3401";
                frmX.ModuleRtsName = "库存盘点";
            }
            else
            {
                
                switch (objModule.Trim().ToLower())
                {
                    case "3401"://单位信息管理
                        frmX = new FrmStockMCheck();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "库存盘点";
                        break;
                    case "3402"://单位信息管理
                        frmX = new frmMoving();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "移库";
                        break;
                    case "3403"://单位信息管理
                        frmX = new FrmStockMAjust();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "库存调整";
                        break;
                    case "3408"://单位信息管理
                        frmX = new frmBillRemove();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "调拨单管理";
                        break;
                    case "3409"://单位信息管理
                        frmX = new frmStBadMaterial();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "不良品单管理";
                        break;
                    case "5110"://出入库汇总报表管理
                        frmX = new frmRptInOutRece();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "出入库汇总报表";
                        break;
                    case "5111"://呆滞物料汇总
                        frmX = new frmSlackMatCount();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "呆滞物料汇总";
                        break;
                    case "3411"://合盘管理
                        frmX = new frmMergePallet();
                        frmX.ModuleRtsId = objModule.Trim().ToLower();
                        frmX.ModuleRtsName = "合盘管理";
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

        public static void ShowSelect()
        {
            UI.FrmSTable frmX = null;
            frmX = new FrmSelectCell();
            frmX.InitFormParameters();
            frmX.ShowDialog();
            frmX.Dispose();
        }

        public static string GetCell(WMSAppInfo objApp, WMSUserInfo objUser, int nState)
        {
            string sResult = "";
            FrmSelectCell frmX = new FrmSelectCell();
            frmX.AppInformation = objApp;
            frmX.UserInformation = objUser;
            if (nState < frmX.cmb_nState.Items.Count)
                frmX.cmb_nState.SelectedIndex = nState;
            frmX.ShowDialog();
            if (frmX.BIsResult)
            {
                sResult = frmX.SelResult;
            }
            else sResult = "";
            frmX.Dispose();
            return (sResult);
        }
        public static void GetStkMaterial(WMSAppInfo objApp, WMSUserInfo objUser,out string cBNoIn,out int nItemIn,
            out string cMNo,out string cMName,out string cSpec ,out string cBatchNo,out double fQty,out string cUnit,
            out DateTime dProdDate,out DateTime dBadDate,out bool bIsOK)
        {
            cBNoIn = "";
            nItemIn = 0 ;
            cMNo = "";
            cMName = "";
            cSpec = "";
            cBatchNo = "";
            fQty = 0;
            cUnit = "";
            dProdDate = DateTime.MinValue;
            dBadDate = DateTime.MinValue;
            bIsOK = false;
            frmSelStkMaterail frmX = new frmSelStkMaterail();
            frmX.AppInformation = objApp;
            frmX.UserInformation = objUser;
            frmX.IsSelect = true;
            frmX.ShowDialog();
            if (frmX.IsResultOK)
            {
                cBNoIn = frmX.cBNo;
                nItemIn = frmX.nItem;
                cMNo = frmX.cMNo;
                cMName = frmX.cMName;
                cSpec = frmX.cSpec;
                cBatchNo = frmX.cBatchNo;
                fQty = frmX.fQty;
                cUnit = frmX.cUnit;
                dProdDate = frmX.dProdDate;
                dBadDate = frmX.dBadDate;
                bIsOK = true;

            }
            frmX.Dispose();

        }

        public static void SelectStkMaterial(WMSAppInfo objApp, WMSUserInfo objUser, DoSelMaterialEvent doSelStkMat)
        {
            frmSelMaterial frmX = new frmSelMaterial();
            frmX.AppInformation = objApp;
            frmX.UserInformation = objUser;
            frmX.DoSelMatEvent = doSelStkMat;
            frmX.ShowDialog();
            frmX.Dispose();
        }

        public static void SelectIOStoreBillData(WMSAppInfo objApp, WMSUserInfo objUser,int nBClass, string sBNo,string sMNo,DoSelIOStoreMatBillDataEvent doSelIOStoreMatBill)
        {
            frmSelIOBillMat frmX = new frmSelIOBillMat();
            frmX.AppInformation = objApp;
            frmX.UserInformation = objUser;
            frmX.BClass = nBClass;
            frmX.txt_cBNo.Text = sBNo;
            frmX.txt_cName.Text = sMNo;
            frmX.DoSelIOStoreMatBillData = doSelIOStoreMatBill;
            frmX.ShowDialog();
            frmX.Dispose();
        }
    }
}
