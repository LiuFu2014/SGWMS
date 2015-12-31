using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WareStoreMS
{
    public partial class frmSelPosFromAndTo : UI.FrmSTable
    {
        #region 私有变量
        bool bIsOpenPosFrom = false  ;
        bool bIsOpenPosTo = false;
        #endregion

        #region 私有方法
        private DataTable GetStoreDtlFromByPalletId(string sPalletId)
        {
            string sSql = "select * from V_StoreItemList where nPalletId='" + sPalletId.Trim() + "'";
            DataTable tbX = null;
            string sErr = "";
            tbX = DBFuns.GetDataTableBySql(AppInformation.SvrSocket, false, sSql, "tbStoreDtlFrom", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            return tbX;
        }

        private DataTable GetStoreDtlToByPalletId(string sPalletId)
        {
            string sSql = "select * from V_StoreItemList where nPalletId='" + sPalletId.Trim() + "'";
            DataTable tbY = null;
            string sErr = "";
            tbY = DBFuns.GetDataTableBySql(AppInformation.SvrSocket, false, sSql, "tbStoreDtlTo", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            return tbY;
        }
        
        private bool bCheckIsOK()
        {
            bool bOK = false;
            #region 合盘检验
                if (bds_Pos_From.Count == 0)
                {
                    MessageBox.Show("对不起，无合盘的源货位数据！");
                    return false ;
                }
                if (grd_StockDtl_From.RowCount == 0)
                {
                    MessageBox.Show("对不起，无需要合盘的物料明细数据！");
                    return false ;
                }
                #region 检查是否选择合盘物料
                bOK = false;
                foreach (DataGridViewRow grdvr in grd_StockDtl_From.Rows)
                {
                    object objX = null ;
                    objX = grdvr.Cells[col_From_Selected.Name].Value  ;
                    if (Convert.ToBoolean(objX))
                    {
                        bOK = true;
                        break;
                    }
                }
                if (!bOK)
                {
                    MessageBox.Show("对不起，请选择需要合盘的库存物料数据！");
                    return false ;
                }
                bOK = false;
                #endregion
            #endregion

            if (bds_Pos_To.Count == 0)
            {
                MessageBox.Show("对不起，无合盘的目标货位数据！");
                return false;
            }
            bOK = true;
            return bOK;
        }
        #endregion

        #region 公共属性


        #endregion

        #region 公共方法

        #endregion

        public frmSelPosFromAndTo()
        {
            InitializeComponent();
        }

        private void btn_Reset_From_Click(object sender, EventArgs e)
        {
            cmb_Area_From.SelectedIndex = -1;
            txt_Col_From.Text = "0";
            txt_Layer_From.Text = "0";
            txt_Row_From.Text = "0";
            txt_PltId_From.Text = "";
            txt_Mat_From.Text = "";
            cmb_Area_From.Focus();
        }

        private void btn_Reset_To_Click(object sender, EventArgs e)
        {
            cmb_Area_To.SelectedIndex = -1;
            txt_Col_To.Text = "0";
            txt_Layer_To.Text = "0";
            txt_Row_To.Text = "0";
            txt_PltId_To.Text = "";
            txt_Mat_To.Text = "";
            cmb_Area_To.Focus();
        }

        private void btn_Qry_From_Click(object sender, EventArgs e)
        {
            StringBuilder sSql = new StringBuilder("select * from V_WARECELLSTATUS where 1=1 ");
            #region 获取查询条件
            if (cmb_Area_From.Text.Trim() != "" && cmb_Area_From.SelectedValue != null)
            {
                sSql.Append(" and cAreaId = '"+ cmb_Area_From.SelectedValue.ToString() +"'");
            }
            if (txt_Row_From.Text.Trim() != "")
            {
                sSql.Append(" and nRow=" + txt_Row_From.Text.Trim());
            }
            if (txt_Col_From.Text.Trim() != "")
            {
                sSql.Append(" and nCol=" + txt_Col_From.Text.Trim());
            }
            if (txt_Layer_From.Text.Trim() != "")
            {
                sSql.Append(" and nLayer=" + txt_Layer_From.Text.Trim());
            }
            if (txt_PltId_From.Text.Trim() != "")
            {
                sSql.Append(" and isnull(nPalletId,' ') like '%"+ txt_PltId_From.Text.Trim() +"%'");
            }
            if (txt_Mat_From.Text.Trim() != "")
            {
                sSql.Append(" and ( isnull(nPalletId,' ') in ( select distinct nPalletId from V_StoreItemList where (cMNo like '%" + txt_Mat_From.Text.Trim() + "%') or (cMName like '%" + txt_Mat_From.Text.Trim() + "%') or (cPYJM like '%" + txt_Mat_From.Text.Trim() + "%')  or (cWBJM like '%" + txt_Mat_From.Text.Trim() + "%') ))");
            }
            #endregion

            DataTable tbPosFrom = null;
            string sErr = "";
            tbPosFrom = DBFuns.GetDataTableBySql(AppInformation.SvrSocket, false, sSql.ToString(), "PosList", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            bIsOpenPosFrom = false;
            bds_Pos_From.DataSource = tbPosFrom;
            grd_Pos_From.DataSource = bds_Pos_From;
            //MessageBox.Show(bds_Pos_From.Count.ToString());
            bIsOpenPosFrom = true ;
            
        }

        private void bds_Pos_From_PositionChanged(object sender, EventArgs e)
        {
            if (!bIsOpenPosFrom) return;
            string sPalletId = "";
            if (bds_Pos_From.Count > 0)
            {
                DataRowView drvX = (DataRowView)bds_Pos_From.Current;
                if (drvX != null)
                {
                    sPalletId = drvX["nPalletId"].ToString();
                }
            }
            DataTable tbStoreFrom = null;
            tbStoreFrom = GetStoreDtlFromByPalletId(sPalletId);
            grd_StockDtl_From.DataSource = tbStoreFrom;
        }

        private void bds_Pos_To_PositionChanged(object sender, EventArgs e)
        {
            if (!bIsOpenPosTo) return;
            string sPalletId = "";
            if (bds_Pos_To.Count > 0)
            {
                DataRowView drvX = (DataRowView)bds_Pos_To.Current;
                if (drvX != null)
                {
                    sPalletId = drvX["nPalletId"].ToString();
                }
            }
            DataTable tbStoreTo = null;
            tbStoreTo = GetStoreDtlToByPalletId(sPalletId);
            grd_StockDtl_To.DataSource = tbStoreTo;
        }

        private void btn_Qry_To_Click(object sender, EventArgs e)
        {
            StringBuilder sSql = new StringBuilder("select * from V_WARECELLSTATUS where 1=1 ");
            #region 获取查询条件
            if (cmb_Area_To.Text.Trim() != "" && cmb_Area_To.SelectedValue != null)
            {
                sSql.Append(" and cAreaId = '" + cmb_Area_To.SelectedValue.ToString() + "'");
            }
            if (txt_Row_To.Text.Trim() != "")
            {
                sSql.Append(" and nRow=" + txt_Row_To.Text.Trim());
            }
            if (txt_Col_To.Text.Trim() != "")
            {
                sSql.Append(" and nCol=" + txt_Col_To.Text.Trim());
            }
            if (txt_Layer_To.Text.Trim() != "")
            {
                sSql.Append(" and nLayer=" + txt_Layer_To.Text.Trim());
            }
            if (txt_PltId_To.Text.Trim() != "")
            {
                sSql.Append(" and isnull(nPalletId,' ') like '%" + txt_PltId_To.Text.Trim() + "%'");
            }
            if (txt_Mat_To.Text.Trim() != "")
            {
                sSql.Append(" and ( isnull(nPalletId,' ') in ( select distinct nPalletId from V_StoreItemList where (cMNo like '%" + txt_Mat_To.Text.Trim() + "%') or (cMName like '%" + txt_Mat_To.Text.Trim() + "%') or (cPYJM like '%" + txt_Mat_To.Text.Trim() + "%')  or (cWBJM like '%" + txt_Mat_To.Text.Trim() + "%') ))");
            }
            #endregion

            DataTable tbPosTo = null;
            string sErr = "";
            tbPosTo = DBFuns.GetDataTableBySql(AppInformation.SvrSocket, false, sSql.ToString(), "PosListTo", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            bIsOpenPosTo = false;
            bds_Pos_To.DataSource = tbPosTo;
            grd_Pos_To.DataSource = bds_Pos_To;
            bIsOpenPosTo = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!bCheckIsOK()) return;
            string sPosFrom = "";
            string sPosTo = "";
            string sPltFrom = "";
            string sPltTo = "";
            #region
            DataRowView drvX = null;
            drvX = (DataRowView)bds_Pos_From.Current;
            if (drvX != null)
            {
                sPosFrom = drvX["cPosId"].ToString();
                sPltFrom = drvX["nPalletId"].ToString(); ;
            }
            drvX = (DataRowView)bds_Pos_To.Current;
            if (drvX != null)
            {
                sPosTo = drvX["cPosId"].ToString();
                sPltTo = drvX["nPalletId"].ToString(); ;
            }
            if (sPosFrom.Trim() == "" || sPltFrom.Trim() == "" || sPosTo.Trim() == "" || sPltTo.Trim() == "")
            {
                MessageBox.Show("对不起，合盘操作的源货位和托盘或者目标货位和托盘数据不能为空！");
                return;
            }
            if (sPltFrom.Trim() == sPltTo.Trim())
            {
                MessageBox.Show("对不起，合盘操作的源货位和目标货位不能相同！");
                return;
            }
            if (MessageBox.Show("您确定将 " + sPosFrom + " 里的所选物料合盘到 " + sPosTo + " 里去吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            #endregion

            #region
            StringBuilder sSql = new StringBuilder("");
            #region 主表
            //获取单号
            string sErr = "";
            string sBNo = "";
            sBNo = DBFuns.GetNewId(AppInformation.SvrSocket, "TWB_BillMergePlt", "cBNo", 12, ("BMP" + DateTime.Now.ToString("yyMMdd")), out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (sBNo.Trim() == "")
            {
                MessageBox.Show("对不起，获取单号失败！");
                return;
            }
            sSql.Append("insert into TWB_BillMergePlt(cBNo,nBClass, cPosIdFrom, cPosIdTo, nPalletIdFrom, nPalletIdTo,");
            sSql.Append(" nWorkIdFrom, nWorkIdTo, nWkTskFromIsEmptyOut, nWorkFromStatus, nWorkToStatus, cCreatorId,");
            sSql.Append(" cCreator, dCreateDate, bIsChecked, dCheckDate, cChecker, bIsFinished)");
            sSql.Append("Values('"+sBNo +"',12,'"+ sPosFrom +"','"+ sPosTo +"','"+ sPltFrom +"','"+ sPltTo +"',");
            sSql.Append("0,0,0,0,0,'"+ UserInformation.UserId +"','"+ UserInformation.UserName +"',getdate(),0,null,'',0 ) ");
            if (!DBFuns.DoExecSql(AppInformation.SvrSocket, sSql.ToString(), "dCreateDate,dCheckDate", out sErr))
            {
                MessageBox.Show("新建主表数据时，失败：" + sErr);
                return;
            }
            #endregion
            #region 明细表
            int nX = 0 ;
            grd_StockDtl_From.EndEdit();
            foreach (DataGridViewRow grdvr in grd_StockDtl_From.Rows)
            {
                object objX = null;
                objX = grdvr.Cells[col_From_Selected.Name].Value;
                if (Convert.ToBoolean(objX))
                {
                    nX ++ ;
                    sSql.Remove(0, sSql.Length);
                    sSql.Append("insert into TWB_BillMergePltDtl(cBNo,nItem,nPalletId,cPosId,cBoxId,cMNo,cBatchNo,cBNoIn,nItemIn,dProdDate,nQCStatus,");
                    sSql.Append("dValiDate,cUNit,bIsSample,cCSId,cSuppler,cDtlRemark,nStatus,cWHIdErp,cWHId,fQty,fFinished,bIsOut,cRemark)");
			        sSql.Append("select '"+ sBNo +"',"+ nX.ToString() +",nPalletId,cPosId,cBoxId,cMNo,cBatchNo,cBNoIn,nItemIn,dProdDate,nQCStatus,");
                    sSql.Append("dBadDate,cUnit,0,cDtlCSId,cDtlSupplier,cDtlRemark,1,cWHIdErp,cWHId,fQty,0,1,'' cRemark from V_StoreItemList ");
                    sSql.Append(" where nPalletId='"+ sPltFrom.Trim() +"' and cPosId='"+ sPosFrom.Trim() +"' and cMNo='" + grdvr.Cells[col_From_MNo.Name].Value.ToString() +"'");
                    sSql.Append(" and cBNoIn='"+ grdvr.Cells[col_From_BNOIn.Name].Value.ToString() +"' ");
                    sSql.Append(" and nItemIn=" + grdvr.Cells[col_From_ItemIn.Name].Value.ToString());
                    if (!DBFuns.DoExecSql(AppInformation.SvrSocket, sSql.ToString(), "", out sErr))
                    {
                        MessageBox.Show("新建明细表数据时，失败：" + sErr);
                        MessageBox.Show("成功增加了 " + (nX-1).ToString() + " 条明细数据！");
                        return;
                    }                    
                }
            }
           

            #endregion
            #endregion
            MessageBox.Show("保存合盘单据数据成功！");
            Close();
        }

        private void chk_SelAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bIsSelected = chk_SelAll.Checked;
            foreach (DataGridViewRow grdvr in grd_StockDtl_From.Rows)
            {
                grdvr.Cells[col_From_Selected.Name].Value = bIsSelected;                
            }
            grd_StockDtl_From.EndEdit();
        }

        private void frmSelPosFromAndTo_Load(object sender, EventArgs e)
        {
            grd_Pos_From.AutoGenerateColumns = false;
            grd_Pos_To.AutoGenerateColumns = false;
            grd_StockDtl_From.AutoGenerateColumns = false;
            grd_StockDtl_To.AutoGenerateColumns = false;
        }

       

    }
}

