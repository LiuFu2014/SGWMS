using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using SunEast;
using SunEast.App;

namespace WareStoreMS
{
    public partial class frmMoving : UI.FrmSTable
    {
        #region 私有方法
        private string GetCellId(int nState)
        {
            string sResult = "";
            FrmSelectCell frmX = new FrmSelectCell();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
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
        //private string GetPalletNo()
        //{
        //    string sResult = "";
        //    FrmSelectPallet frmX = new FrmSelectPallet();
        //    frmX.AppInformation = AppInformation;
        //    frmX.UserInformation = UserInformation;
        //    frmX.ShowDialog();
        //    if (frmX.BIsResult)
        //    {
        //        sResult = frmX.SelResult;
        //    }
        //    else sResult = "";
        //    frmX.Dispose();
        //    return (sResult);
        //}
        //private bool CellIdIsOK(string sCode)
        //{
        //    bool bOK = false;
        //    string sX = BI.BSIOBillBI.BSIOBillBI.JudgeCellIdIsOK(AppInformation.dbtApp, AppInformation.AppConn, sCode, out bOK);
        //    if (sX != "0")
        //        MessageBox.Show(sX);
        //    return (bOK);
        //}
        //private bool PalletDIdIsOK(string sCode)
        //{
        //    bool bOK = false;
        //    string sX = BI.BSIOBillBI.BSIOBillBI.JudgePalletDetailIdIsOK(AppInformation.dbtApp, AppInformation.AppConn, sCode, out bOK);
        //    if (sX != "0")
        //        MessageBox.Show(sX);
        //    return (bOK);
        //}
        #endregion
        public frmMoving()
        {
            InitializeComponent();
        }

        private void btn_C_Sel_From_Click(object sender, EventArgs e)
        {
            //(0:空位 1:空盘 2:有货)
            string sX = GetCellId(1);
            if (sX != "")
                txt_chg_CellFrom.Text = sX;
            txt_chg_CellFrom.Focus();
        }

        private void btn_C_Sel_To_Click(object sender, EventArgs e)
        {
            //(0:空位 1:空盘 2:有货)
            string sX = GetCellId(0);
            if (sX != "")
                txt_chg_CellTo.Text = sX;
            txt_chg_CellTo.Focus();
        }

        private void btn_Chg_Do_Click(object sender, EventArgs e)
        {
            //if (txt_chg_CellFrom.Text.Trim() == "")
            //{
            //    MessageBox.Show("对不起，源货位号不能为空！");
            //    txt_chg_CellFrom.Focus();
            //    return;
            //}
            //if (txt_chg_CellTo.Text.Trim() == "")
            //{
            //    MessageBox.Show("对不起，目标位号不能为空！");
            //    txt_chg_CellTo.Focus();
            //    return;
            //}
            string sX = "";
            string sErr = "";
            /*
             sp_Pack_DoWareCellMove
            (pSysType varchar2,pPosIdFrom varchar2,
              pPosIdTo varchar2,pUser varchar2,pCmptId varchar2:='101',
            Cur_Result out sys_refCursor
            ) 
            */
            int nIsDoNow = 0;
            if (chk_DoNow.Checked)
            {
                nIsDoNow = 1;
            }
            sX = DBFuns.sp_Pack_DoWareCellMove(AppInformation.SvrSocket, "WMS", txt_chg_CellFrom.Text.Trim(), txt_chg_CellTo.Text.Trim(), UserInformation.UserName, UserInformation.UnitId, nIsDoNow, out sErr);
           
            if (sX == "0" || sErr.Trim().Length ==0)
            {
                MessageBox.Show("下发执行操作成功！");
                Close();
            }
            else
            {
                MessageBox.Show(sErr);
            }
               
        }
    }
}