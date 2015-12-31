using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WareStoreMS
{
    /// <summary>
    /// 选择出入库作业物料数据
    /// </summary>
    /// <param name="nBClass">单据类别(1入库2出库3盘点)</param>
    /// <param name="sBNo">单号</param>
    /// <param name="nItem">单明细序号</param>
    /// <param name="sMNo">物料号</param>
    /// <param name="sMName">物料名称</param>
    /// <param name="sSpec">规格型号</param>
    /// <param name="sMatStyle">物料款式</param>
    /// <param name="sMatQCLevel">物料质等</param>
    /// <param name="sMatOther">其他物料属性</param>
    /// <param name="sRemark">物料备注</param>
    /// <param name="sABC">物料ABC属性</param>
    /// <param name="fQty">数量</param>
    /// <param name="fWeight">物料单重</param>
    /// <param name="sUnit">计量单位</param>
    /// <param name="sCSId">供应商编号</param>
    /// <param name="sSupplier">供应商名</param>
    /// <param name="sBatchNo">物料批号</param>
    /// <param name="sBNoIn">库存单号</param>
    /// <param name="nItemIn">库存单序号</param>
    /// <param name="sWHIdErp">ERP仓库号</param>
    /// <param name="sAreaIdErp">ERP货区号</param>
    /// <param name="sPosIdErp">ERP货位号</param>
    /// <param name="bDoOK">是否OK</param>
    public delegate void DoSelIOStoreMatBillDataEvent(int nBClass,string sBNo,int nItem, string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
            string sRemark, string sABC, double fQty, double fWeight, string sUnit, string sCSId, string sSupplier, string sBatchNo, string sBNoIn, int nItemIn,
            string sWHIdErp,string sAreaIdErp,string sPosIdErp,out bool bDoOK);

    public partial class frmSelIOBillMat : UI.FrmSTable
    {
        #region 私有变量
        
        #endregion
        #region 私有方法

        private string GetSql()
        {
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select pck.nBClass, pck.cMNo,sum(pck.fQty) fQty,pck.cUnit,pck.cBNo,pck.nItem,isnull(pck.cBatchNo,' ') cBatchNo,");
	        sSql.Append(" pck.dProdDate,pck.dBadDate,isnull(dtl.cCSId,' ') cCSId,isnull(dtl.cSupplier,' ') cSupplier,mat.cName cMName,");
            sSql.Append(" mat.cSpec,isnull(mat.cMatQCLevel,' ') cMatQCLevel,isnull(mat.cMatStyle,'') cMatStyle,isnull(mat.cMatOther,' ') cMatOther ,pck.cBNoIn,pck.nItemIn,isnull(mat.cABC,' ') cABC,isnull(mat.cRemark,' ') cRemark,isnull(mat.fWeight,0) fWeight");
	        sSql.Append(" from TWB_StockDtl_His pck ");
	        sSql.Append(" left join TWB_BillInDtl dtl on pck.cBNo=dtl.cBNo and pck.nItem=dtl.nItem ");
	        sSql.Append(" left join TPC_Material mat on pck.cMNo=mat.cMNo ");
            sSql.Append(" where pck.nStatus=1 ");
            //
            if (_BClass > 0)
            {
                sSql.Append(" and (pck.nBClass=" + _BClass.ToString() + ")");
            }
            #region 条件 
            if (chk_Date.Checked)
            {
                string sDate = dtp_From.Value.ToString("yyyy-MM-dd hh:mm:ss");
                sSql.Append(" and ( pck.dInTime >= '" + sDate + "' )");
                sDate = dtp_To.Value.ToString("yyyy-MM-dd hh:mm:ss");
                sSql.Append(" and ( pck.dInTime <= '" + sDate + "' )");
            }
            if (txt_cBNo.Text.Trim() != "")
            {
                sSql.Append(" and (pck.cBNo like '%"+ txt_cBNo.Text.Trim() +"%') ");
            }
            string sX = txt_cName.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and ((isnull(mat.cMNo,' ') like '%" + sX + "%') or (isnull(mat.cName,' ') like '%" + sX + "%') or (isnull(mat.cWBJM,' ') like '%" + sX + "%') or (isnull(mat.cPYJM,' ') like '%" + sX + "%') ) ");
            }
            sX = txt_cSpec.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and (isnull(mat.cSpec,' ') like '%"+ sX +"%') ");
            }
            sX = this.txt_cMatStyle.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and (isnull(mat.cMatStyle,' ') like '%" + sX + "%') ");
            }
            sX = this.txt_cMatQCLevel.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and (isnull(mat.cMatQCLevel,' ') like '%" + sX + "%') ");
            }
            sX = this.txt_cMatOther.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and (isnull(mat.cMatOther,' ') like '%" + sX + "%') ");
            }
            sX = this.txt_cRemark.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and (isnull(mat.cRemark,' ') like '%" + sX + "%') ");
            }
            if (cmb_cABC.Text.Trim() != "" && cmb_cABC.SelectedIndex >= 0)
            {
                sSql.Append(" and ( isnull(mat.cABC,' ') like '%"+ cmb_cABC.Text.Trim() +"%' )");
            }
            #endregion
            //
            sSql.Append(" group by pck.nBClass,pck.cMNo,pck.cUnit,pck.cBNo,pck.nItem,isnull(pck.cBatchNo,' '),");
	        sSql.Append(" pck.dProdDate,pck.dBadDate,isnull(dtl.cCSId,' '),isnull(dtl.cSupplier,' '),mat.cName,mat.cSpec, ");
            sSql.Append(" isnull(mat.cMatQCLevel,' '),isnull(mat.cMatStyle,''),isnull(mat.cMatOther,' '),pck.cBNoIn,pck.nItemIn,isnull(mat.cABC,' '),isnull(mat.cRemark,' '),isnull(mat.fWeight,0)");
            return sSql.ToString();
        }

        #endregion

        #region 属性

        private int _BClass = 0;
        [Description ("单据类型: 1入库 2出库 3盘点 4调整单 5仓位调整 6调拨单 7 不良品单 8质检请验单 9质检取样单 10质检报告单 11入库验收单")]        
        public int BClass
        {
            get { return _BClass; }
            set { 
                _BClass = value;
                switch (_BClass)
                {
                    case 1:
                        break;
                        Text = "选择入库物料单据";
                    case 2:
                        Text = "选择出库物料单据";
                        break;
                    case 4:
                        Text = "选择库存调整物料单据";
                        break;
                    default :
                        break;
                }
            }
        }

        private DoSelIOStoreMatBillDataEvent _DoSelIOStoreMatBillData = null;
        [Description("处理选择出入库物料单据数据")]
        public DoSelIOStoreMatBillDataEvent DoSelIOStoreMatBillData
        {
            get { return _DoSelIOStoreMatBillData; }
            set { _DoSelIOStoreMatBillData = value; }
        }

        private string _BillNo = "";
        /// <summary>
        /// 单号
        /// </summary>
        [Description("单号")]
        public string BillNo
        {
            get { return _BillNo; }
            set
            {
                _BillNo = value;
                txt_cBNo.Text = _BillNo;
            }
        }

        private string _MName = "";
        /// <summary>
        /// 物料的编码，名称，五笔简码，拼音简码
        /// </summary>
        [Description("物料的编码，名称，五笔简码，拼音简码")]
        public string MName
        {
            get { return _MName; }
            set
            {
                _MName = value;
                this.txt_cName.Text = _MName;
            }
        }

        #endregion

        public frmSelIOBillMat()
        {
            InitializeComponent();
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            string sErr = "";
            string sSql = GetSql();            
            DataSet dsX = null ;
            Cursor.Current = Cursors.WaitCursor ;
            try
            {
                dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "IOStoreDtl", 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch(Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message);
                return;
            }
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (dsX != null)
            {
                DataTable tbX = dsX.Tables["IOStoreDtl"];
                bds_Data.DataSource = tbX.Copy();
                dsX.Clear();
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_cBNo.Text = "";
            txt_cMatOther.Text = "";
            txt_cMatQCLevel.Text = "";
            txt_cMatStyle.Text = "";
            txt_cName.Text = "";
            txt_cRemark.Text = "";
            txt_cSpec.Text = "";
            cmb_cABC.SelectedIndex = -1;
            txt_cBNo.Focus();
        }

        private void grd_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            #region
            if (bds_Data.Count == 0)
            {
                MessageBox.Show("对不起，无数据可选择！");
                return;
            }
            DataRowView drv = (DataRowView)bds_Data.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起，没有选择数据！");
                return;
            }
            #endregion

            #region
            bool bOK = false;
            if (_DoSelIOStoreMatBillData != null)
            {
                int nBClass = 0;
                int nItem = 0;
                double fQty = 0;
                double fWeight = 0;
                
                nBClass = Convert.ToInt16(drv["nBClass"]);
                nItem = Convert.ToInt16(drv["nItem"]);
                fQty = Convert.ToDouble(drv["fQty"]);
                if (drv["fWeight"] != null)
                {
                    fWeight = Convert.ToDouble(drv["fWeight"]);
                }
                try
                {
                    _DoSelIOStoreMatBillData(nBClass, drv["cBNo"].ToString(), nItem, drv["cMNo"].ToString(), drv["cMName"].ToString(), drv["cSpec"].ToString(),
                        drv["cMatStyle"].ToString(), drv["cMatQCLevel"].ToString(), drv["cMatOther"].ToString(), drv["cRemark"].ToString(), drv["cABC"].ToString(),
                        fQty, fWeight, drv["cUnit"].ToString(), drv["cCSId"].ToString(), drv["cSupplier"].ToString(), drv["cBatchNo"].ToString(),
                        drv["cBNoIn"].ToString(), Convert.ToInt16(drv["nItemIn"]),drv["cWHIdErp"].ToString(),drv["cAreaIdErp"].ToString(),drv["cPosIdErp"].ToString(), out bOK);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }

            #endregion
            if (bOK)
            {
                Close();
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (bds_Data.Count == 0)
            {
                MessageBox.Show("无物料数据可选择！");
                return;
            }
            if (grd_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("没有选择物料数据！");
                return;
            }
            bool bOK = false;
            this.prgMain.Maximum = grd_Data.SelectedRows.Count;
            prgMain.Minimum = 0;
            prgMain.Value = 0;
            prgMain.Visible = true;
            foreach (DataGridViewRow grdv in grd_Data.SelectedRows)
            {
                if (_DoSelIOStoreMatBillData != null)
                {
                    #region
                    int nBClass = 0;
                    int nItem = 0;
                    double fQty = 0;
                    double fWeight = 0;
                    double fSafeQtyDn = 0;
                    if (grdv.Cells["col_nBClass"].Value != null && grdv.Cells["col_nBClass"].Value.ToString() != "")
                    {
                        nBClass = Convert.ToInt16(grdv.Cells["col_nBClass"].Value);
                    }
                    if (grdv.Cells["col_nItem"].Value != null && grdv.Cells["col_nItem"].Value.ToString() != "")
                    {
                        nItem = Convert.ToInt16(grdv.Cells["col_nItem"].Value);
                    }
                    if (grdv.Cells["col_fQty"].Value != null && grdv.Cells["col_fQty"].Value.ToString() != "")
                    {
                        fQty = Convert.ToDouble(grdv.Cells["col_fQty"].Value);
                    }
                    if (grdv.Cells["col_fWeight"].Value != null && grdv.Cells["col_fWeight"].Value.ToString() != "")
                    {
                        fWeight = Convert.ToDouble(grdv.Cells["col_fWeight"].Value);
                    }                    
                    try
                    {
                        _DoSelIOStoreMatBillData(nBClass, grdv.Cells["col_cBNo"].Value.ToString(), nItem, grdv.Cells["col_cMNo"].Value.ToString(), grdv.Cells["col_cMName"].Value.ToString(), 
                            grdv.Cells["col_cSpec"].Value.ToString(),grdv.Cells["col_cMatStyle"].Value.ToString(), grdv.Cells["col_cMatQCLevel"].Value.ToString(),
                            grdv.Cells["col_cMatOther"].Value.ToString(), grdv.Cells["col_cRemark"].Value.ToString(), grdv.Cells["col_cABC"].Value.ToString(), fQty, fWeight,
                            grdv.Cells["col_cUnit"].Value.ToString(), grdv.Cells["col_cCSId"].Value.ToString(), grdv.Cells["col_cSupplier"].Value.ToString(), grdv.Cells["col_cBatchNo"].Value.ToString(),
                            grdv.Cells["col_cBNoIn"].Value.ToString(), Convert.ToInt16(grdv.Cells["col_nItemIn"].Value),grdv.Cells[col_WHIdErp.Name].Value.ToString(),grdv.Cells[col_AreaIdErp.Name].Value.ToString(),grdv.Cells[col_PosIdErp.Name].Value.ToString(), out bOK);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                    prgMain.Value++;
                    #endregion
                }
            }
            if (bOK)
            {
                Close();
            }
        }

        private void chk_Date_CheckedChanged(object sender, EventArgs e)
        {
            dtp_From.Enabled = chk_Date.Checked;
            dtp_To.Enabled = chk_Date.Checked;
        }

        private void frmSelIOBillMat_Load(object sender, EventArgs e)
        {
            chk_Date.Checked = false;
            dtp_From.Value = DateTime.Now.AddDays(-60);
            dtp_To.Value = DateTime.Now;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
