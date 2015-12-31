using App;
using Comm.IF;
using System;
using UI;
namespace SunEast.App
{


    public class WarehouseTask : IFApplication
    {
        public void LoadByChildForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
        }

        public void LoadByModeForm(WMSAppInfo objApp, WMSUserInfo objUser, string objModule)
        {
            FrmSTable table = null;
            if ((objModule == string.Empty) || (objModule.Trim() == ""))
            {
                table = new frmInEmpty {
                    ModuleRtsId = "3107",
                    ModuleRtsName = "空托盘入库"
                };
            }
            else
            {
                switch (objModule.Trim().ToLower())
                {
                    case "3102":
                        table = new FrmStockMPalletWMSIn {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "入库配盘"
                        };
                        break;

                    case "3202":
                        table = new FrmStockMPltWMSOut {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "出库配盘"
                        };
                        break;

                    case "3111":
                        table = new FrmStockMExceptionSet {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "指令异常处理"
                        };
                        break;

                    case "3104":
                        table = new frmInEmpty {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "空托盘入库"
                        };
                        break;

                    case "3204":
                        table = new frmOutEmpty {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "空托盘出库"
                        };
                        break;

                    case "3103":
                        table = new frmTask {
                            ModuleRtsId = objModule.Trim().ToLower()
                        };
                        ((frmTask) table).WorkTaskType = TaskType.ttTaskInOnly;
                        table.ModuleRtsName = "入库任务管理";
                        break;

                    case "3203":
                        table = new frmTask {
                            ModuleRtsId = objModule.Trim().ToLower()
                        };
                        ((frmTask) table).WorkTaskType = TaskType.ttTaskOutOnly;
                        table.ModuleRtsName = "出库任务管理";
                        break;

                    case "3405":
                        table = new frmTask {
                            ModuleRtsId = objModule.Trim().ToLower()
                        };
                        ((frmTask) table).WorkTaskType = TaskType.ttTaskAll;
                        table.ModuleRtsName = "任务管理";
                        break;

                    case "3406":
                        table = new frmWareCellState {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "平面仓位图"
                        };
                        break;

                    case "3205":
                        table = new frmOutAndSee {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "出库查看"
                        };
                        break;

                    case "3404":
                        table = new frmOutAndSee {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "出库查看"
                        };
                        break;

                    case "3407":
                        table = new frmTaskCheckOK {
                            ModuleRtsId = objModule.Trim().ToLower(),
                            ModuleRtsName = "任务确认"
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
    }
}

