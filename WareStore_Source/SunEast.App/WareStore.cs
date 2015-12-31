using App;
using Comm.IF;
using System;
using System.Runtime.InteropServices;
using UI;
using WareStoreMS;
using WareStore;

namespace SunEast.App
{
    public class WareStore : IFApplication
    { 
        public static string GetCell(WMSAppInfo objApp, WMSUserInfo objUser, int nState)
        {
            string selResult = "";
            FrmSelectCell cell = new FrmSelectCell {
                AppInformation = objApp,
                UserInformation = objUser
            };
            if (nState < cell.cmb_nState.Items.Count)
            {
                cell.cmb_nState.SelectedIndex = nState;
            }
            cell.ShowDialog();
            if (cell.BIsResult)
            {
                selResult = cell.SelResult;
            }
            else
            {
                selResult = "";
            }
            cell.Dispose();
            return selResult;
        }

        public static void GetStkMaterial(WMSAppInfo objApp, WMSUserInfo objUser, out string cBNoIn, out int nItemIn, out string cMNo, out string cMName, out string cSpec, out string cBatchNo, out double fQty, out string cUnit, out DateTime dProdDate, out DateTime dBadDate, out bool bIsOK)
        {
            cBNoIn = "";
            nItemIn = 0;
            cMNo = "";
            cMName = "";
            cSpec = "";
            cBatchNo = "";
            fQty = 0.0;
            cUnit = "";
            dProdDate = DateTime.MinValue;
            dBadDate = DateTime.MinValue;
            bIsOK = false;
            frmSelStkMaterail materail = new frmSelStkMaterail {
                AppInformation = objApp,
                UserInformation = objUser,
                IsSelect = true
            };
            materail.ShowDialog();
            if (materail.IsResultOK)
            {
                cBNoIn = materail.cBNo;
                nItemIn = materail.nItem;
                cMNo = materail.cMNo;
                cMName = materail.cMName;
                cSpec = materail.cSpec;
                cBatchNo = materail.cBatchNo;
                fQty = materail.fQty;
                cUnit = materail.cUnit;
                dProdDate = materail.dProdDate;
                dBadDate = materail.dBadDate;
                bIsOK = true;
            }
            materail.Dispose();
        }

        public void LoadByChildForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
        }

        public void LoadByModeForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
            FrmSTable table = null;
            if ((objModule == string.Empty) || (objModule.Trim() == ""))
            {
                table = new FrmStockMCheck {
                    ModuleRtsId = "3401",
                    ModuleRtsName = "库存盘点"
                };
            }
            else
            {
                switch (objModule.Trim().ToLower())
                {
                    case "3401":
                        table = new FrmStockMCheck {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "库存盘点"
                        };
                        break;

                    case "3402":
                        table = new frmMoving {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "移库"
                        };
                        break;

                    case "3403":
                        table = new FrmStockMAjust {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "库存调整"
                        };
                        break;

                    case "3408":
                        table = new frmBillRemove {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "调拨单管理"
                        };
                        break;

                    case "3409":
                        table = new frmStBadMaterial {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "不良品单管理"
                        };
                        break;

                    case "5110":
                        table = new frmRptInOutRece {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "出入库汇总报表"
                        };
                        break;

                    case "5111":
                        table = new frmSlackMatCount {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "呆滞物料汇总"
                        };
                        break;

                    case "3411":
                        table = new frmMergePallet {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "合盘管理"
                        };
                        break;
                }
                table.AppInformation = objApp;
                table.UserInformation = objUser;
                table.InitFormParameters();
                table.ShowDialog();
                table.Dispose();
            }
        }

        public static void SelectIOStoreBillData(WMSAppInfo objApp, WMSUserInfo objUser, int nBClass, string sBNo, string sMNo, DoSelIOStoreMatBillDataEvent doSelIOStoreMatBill)
        {
            frmSelIOBillMat mat = new frmSelIOBillMat {
                AppInformation = objApp,
                UserInformation = objUser,
                BClass = nBClass
            };
            mat.txt_cBNo.Text = sBNo;
            mat.txt_cName.Text = sMNo;
            mat.DoSelIOStoreMatBillData = doSelIOStoreMatBill;
            mat.ShowDialog();
            mat.Dispose();
        }

        public static void SelectStkMaterial(WMSAppInfo objApp, WMSUserInfo objUser, DoSelMaterialEvent doSelStkMat)
        {
            frmSelMaterial material = new frmSelMaterial {
                AppInformation = objApp,
                UserInformation = objUser,
                DoSelMatEvent = doSelStkMat
            };
            material.ShowDialog();
            material.Dispose();
        }

        public static void ShowSelect()
        {
            FrmSTable table = null;
            table = new FrmSelectCell();
            table.InitFormParameters();
            table.ShowDialog();
            table.Dispose();
        }
    }
}

