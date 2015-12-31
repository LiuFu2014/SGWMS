using App;
using Comm.IF;
using System;
using UI;
using WareBaseMS;
namespace SunEast.App
{
    public class WarehouseBase : IFApplication
    {
        public void LoadByChildForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
        }

        public void LoadByModeForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
            FrmSTable table = null;
            if ((objModule == string.Empty) || (objModule.Trim() == ""))
            {
                table = new FrmStockInfo {
                    ModuleRtsId = "2105",
                    ModuleRtsName = "仓库信息管理"
                };
            }
            else
            {
                switch (objModule.Trim().ToLower())
                {
                    case "2105":
                        table = new FrmStockInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "仓库设定"
                        };
                        break;

                    case "2106":
                        table = new FrmStockAreaInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "货区设定"
                        };
                        break;

                    case "2107":
                        table = new FrmStockPositInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "货位设定"
                        };
                        break;

                    case "2108":
                        table = new FrmStockPalletInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "托盘设定"
                        };
                        break;

                    case "2109":
                        table = new FrmStockBoxInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "周转箱设定"
                        };
                        break;

                    case "2110":
                        table = new FrmStockOrderTypeInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "单据类别"
                        };
                        break;

                    case "2111":
                        table = new FrmStockMaterTypeInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "物料类别"
                        };
                        break;

                    case "2112":
                        table = new FrmStockMaterInfo {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "物料信息"
                        };
                        break;

                    case "2116":
                        table = new FrmUnit {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "计量单位管理"
                        };
                        break;

                    case "2117":
                        table = new frmBaseItem {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "基本码表管理"
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

        public static void SelectMaterialInfo(WMSAppInfo objApp, WMSUserInfo objUser, DoSelMaterialEvent doSelMat)
        {
            frmSelMaterialInfo info = new frmSelMaterialInfo {
                AppInformation = objApp,
                UserInformation = objUser,
                DoSelMatEvent = doSelMat
            };
            info.ShowDialog();
            info.Dispose();
        }
    }
}

