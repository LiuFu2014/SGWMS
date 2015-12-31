using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using System.Collections;
using SunEast.App;

namespace WareStoreMS
{
    public partial class FrmSelectCell : UI.FrmSTable
    {
        #region 私有变量
        string strTbNameMain = "TWC_WareCell";
        //
        //
        private bool bIsResultOK = false; //表示已经编辑成功
        private string selResult = "";

        private bool bWHIsOK = false;

        #endregion

        #region 私有方法
        private bool QueryCellList()
        {
            StringBuilder sCon = new StringBuilder("");
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select wc.cPosId,wc.nPalletId,wc.nStatusWork,wc.nRow,wc.nCol,wc.nLayer,isnull(wc.cRemark,'') cRemark,wc.cAreaId,");
            sSql.Append(" wc.cAreaName,wc.cWHId,wc.cWHName,wc.cStatusWork,wc.nStatusStore,wc.cStatusStore, ");
            sSql.Append(" isnull(st.cItemId,'') cMNo,isnull(st.cMName,'') cMName,isnull(st.cBatchNo,'') cBatchNo, sum(isnull(st.fQty,0)) fQty,");
            sSql.Append(" isnull(st.cUnit,'') cUnit,min(isnull(st.cDtlCSId,'')) cCSId,min(isnull(st.cDtlSupplier,'')) cSupplier,min(isnull(st.cStoreRemark,'')) ");
            sSql.Append(" cDtlRemark,min(isnull(st.cSpec,'')) cSpec,isnull(st.cBNoIn,'') cBNoIn,isnull(st.nItemIn,0) nItemIn,min(st.dDate) dDate,max(wc.cPltRemark) cPltRemark  ");
	        sSql.Append(" from V_WareCellStatus wc ");
	        sSql.Append(" left join V_StoreItemList st on isnull(wc.nPalletId,'')= st.nPalletId  ");
            //--
            sCon.Append(" where wc.nStatusWork <=2 ");
            if(UserInformation.UserId !="90101001")
            {
                sCon.Append(" and isnull(wc.cMAreaId,' ') in (select cMAreaId from V_UserMArea where cUserId='" + UserInformation.UserId + "')");
            }
            #region 条件
            #region
            if (cmb_cWHId.Text.Trim() != "" && cmb_cWHId.SelectedValue != null && cmb_cWHId.SelectedIndex > -1)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and wc.cWHId='"+ cmb_cWHId.SelectedValue.ToString() +"'");
                }
                else
                {
                    sCon.Append(" where wc.cWHId='" + cmb_cWHId.SelectedValue.ToString() + "'");
                }                
            }
            if (cmb_cAreaId.Text.Trim() != "" && cmb_cAreaId.SelectedValue != null && cmb_cAreaId.SelectedIndex > -1)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and wc.cAreaId='" + cmb_cAreaId.SelectedValue.ToString() + "'");
                }
                else
                {
                    sCon.Append(" where wc.cAreaId='" + cmb_cAreaId.SelectedValue.ToString() + "'");
                }
            }
            #endregion
            int nX = 0 ;
            #region
            if (txt_nRow.Text.Trim() != "" && IsInteger(txt_nRow.Text.Trim()))
            {
                nX = int.Parse(txt_nRow.Text.Trim());
                if (nX > 0)
                {
                    if (sCon.Length > 0)
                    {
                        sCon.Append(" and wc.nRow=" + nX.ToString() + "");
                    }
                    else
                    {
                        sCon.Append(" where wc.nRow=" + nX.ToString() + "");
                    }
                }
            }
            nX = 0 ;
            if (txt_nCol.Text.Trim() != "" && IsInteger(txt_nCol.Text.Trim()))
            {
                nX = int.Parse(txt_nCol.Text.Trim());
                if (nX > 0)
                {
                    if (sCon.Length > 0)
                    {
                        sCon.Append(" and wc.nCol=" + txt_nCol.Text.Trim() + "");
                    }
                    else
                    {
                        sCon.Append(" where wc.nCol=" + txt_nCol.Text.Trim() + "");
                    }
                }
            }
            nX = 0 ;
            if (txt_nLayer.Text.Trim() != "" && IsInteger(txt_nLayer.Text.Trim()))
            {
                nX = int.Parse(txt_nLayer.Text.Trim());
                if (nX > 0)
                {
                    if (sCon.Length > 0)
                    {
                        sCon.Append(" and wc.nLayer=" + txt_nLayer.Text.Trim() + "");
                    }
                    else
                    {
                        sCon.Append(" where wc.nLayer=" + txt_nLayer.Text.Trim() + "");
                    }
                }
            }

            if (cmb_nState.Text.Trim() != "" && cmb_nState.SelectedIndex < 3 && cmb_nState.Text.Trim() !="全部" )
            {
                string sX = "";
                int nState = 0;
                /*
                    空位
                    空盘
                    有货
                    全部
                 * */
                #region
                switch (cmb_nState.SelectedIndex)
                {
                    case 0:
                        sX = "= -1";
                        break;
                    case 1:
                        sX = " = 0 ";
                        break;
                    case 2:
                        sX = ">=1";
                        break;
                }
                #endregion

                if (sCon.Length > 0)
                {
                    sCon.Append(" and wc.nStatusStore" + sX + " ");
                }
                else
                {
                    sCon.Append(" where wc.nStatusStore" + sX + " ");
                }
            }
            if (txt_cPltRemark.Text.Trim() != "")
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and ( (isnull(wc.cPltRemark,' ') like '%" + txt_cPltRemark.Text.Trim() + "%') or (isnull(wc.nPalletId,' ')='" + txt_cPltRemark.Text.Trim() + "') )");
                }
                else
                {
                    sCon.Append(" where ( (isnull(wc.cPltRemark,' ') like '%" + txt_cPltRemark.Text.Trim() + "%') or (isnull(wc.nPalletId,' ')='" + txt_cPltRemark.Text.Trim() + "') )");
                }
                
            }
            #endregion

            #region
            if (txt_cName.Text.Trim() != "")
            {
                string sX = txt_cName.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and ((isnull(st.cMNo,'~') like '%" + sX + "%') or (isnull(st.cMName,'~') like '%" + sX + "%') or (isnull(st.cWBJM,'~') like '%" + sX + "%')  or (isnull(st.cPYJM,'~') like '%" + sX + "%') )");
                }
                else
                {
                    sCon.Append(" where ((isnull(st.cMNo,'~') like '%" + sX + "%') or (isnull(st.cMName,'~') like '%" + sX + "%') or (isnull(st.cWBJM,'~') like '%" + sX + "%')  or (isnull(st.cPYJM,'~') like '%" + sX + "%') )");
                }
            }
            if (this.txt_cSpec.Text.Trim() != "")
            {
                string sX = txt_cSpec.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cSpec,'~') like '%" + sX + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cSpec,'~') like '%" + sX + "%')");
                }
            }
            if (this.txt_cRemark.Text.Trim() != "")
            {
                string sX = txt_cRemark.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cRemark,'~') like '%" + sX + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cRemark,'~') like '%" + sX + "%')");
                }
            }
            if (this.txt_cMatStyle.Text.Trim() != "")
            {
                string sX = txt_cMatStyle.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cMatStyle,'~') like '%" + sX + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cMatStyle,'~') like '%" + sX + "%')");
                }
            }
            if (this.txt_cMatQCLevel.Text.Trim() != "")
            {
                string sX = txt_cMatQCLevel.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cMatQCLevel,'~') like '%" + sX + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cMatQCLevel,'~') like '%" + sX + "%')");
                }
            }
            if (this.txt_cMatOther.Text.Trim() != "")
            {
                string sX = txt_cMatOther.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cMatOther,'~') like '%" + sX + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cMatOther,'~') like '%" + sX + "%')");
                }
            }
            #endregion

            #region
            if (cmb_cTypeId1.Text.Trim() != "" && cmb_cTypeId1.SelectedValue != null && cmb_cTypeId1.SelectedIndex > -1)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cTypeId1,'~') like '%" + cmb_cTypeId1.SelectedValue.ToString() + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cTypeId1,'~') like '%" + cmb_cTypeId1.SelectedValue.ToString() + "%')");
                }          
            }

            if (cmb_cABC.Text.Trim() != "")
            {
                string sX = cmb_cABC.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cABC,'~') like '%" + sX + "%') ");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cABC,'~') like '%" + sX + "%') ");
                }
            }
            if (this.txt_cSupplier.Text.Trim() != "")
            {
                string sX = txt_cSupplier.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and ((isnull(st.cDtlCSId,'~') like '%" + sX + "%') or (isnull(st.cDtlSupplier,'~') like '%" + sX + "%') or (isnull(st.cDtlWBJM,'~') like '%" + sX + "%') or (isnull(st.cDtlPYJM,'~') like '%" + sX + "%'))");
                }
                else
                {
                    sCon.Append(" where ((isnull(st.cDtlCSId,'~') like '%" + sX + "%') or (isnull(st.cDtlSupplier,'~') like '%" + sX + "%') or (isnull(st.cDtlWBJM,'~') like '%" + sX + "%') or (isnull(st.cDtlPYJM,'~') like '%" + sX + "%'))");
                }
            }
            if(txt_cDtlRemark.Text.Trim()!="")
            {
                  string sX = txt_cDtlRemark.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cDtlRemark,'~') like '%" + sX + "%') ");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cDtlRemark,'~') like '%" + sX + "%') ");
                }
            }
            #endregion

            #endregion

            sSql.Append(sCon.ToString());
            sSql.Append(" group by wc.cPosId,wc.nPalletId,wc.nStatusWork,wc.nRow,wc.nCol,wc.nLayer,isnull(wc.cRemark,''),wc.cAreaId, ");
            sSql.Append(" wc.cAreaName,wc.cWHId,wc.cWHName,wc.cStatusWork,wc.nStatusStore,wc.cStatusStore, ");
            sSql.Append("  isnull(st.cItemId,''),isnull(st.cMName,''),isnull(st.cBatchNo,''),isnull(st.cUnit,''),isnull(st.cBNoIn,''),isnull(st.nItemIn,0) ");
            bool bIsOK = false;
            //if (DBDataSet.Tables["TWC_WareCell"] != null)
            //    DBDataSet.Tables["TWC_WareCell"].Clear();
            grdCellList.AutoGenerateColumns = false;
            string err = "";
            //string sql = "select * from V_WareCellStatus " + strCon;
            //string sX = BI.BSIOBillBI.BSIOBillBI.QueryCellList(AppInformation.dbtApp, AppInformation.AppConn, DBDataSet, UserInformation, strCon);
            DataSet ds = PubDBCommFuns.GetDataBySql(sSql.ToString(), "data", 0, 0, out err);
            bIsOK = err == "";
            sSql.Remove(0, sSql.Length);
            sCon.Remove(0, sCon.Length);
            if (bIsOK == true)
            {
                bdsList.DataSource = ds.Tables["data"];
                grdCellList.Focus();
                grdCellList.DataSource = bdsList;
                //grdCellList.Focus();
                //grdDtl.DataSource = bdsDtl;
            }
            else
                MessageBox.Show(err);
            return (bIsOK);
        }

        private void LoadAreaList(string sWHId)
        {
            string sErr = "";
            string sSql = "select cAreaId,cAreaName from TWC_WArea where  bused=1 and cWHId='"+ sWHId.Trim() +"' ";
            DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            cmb_cAreaId.DataSource = dsX.Tables["data"];
            cmb_cAreaId.DisplayMember = "cAreaName";
            cmb_cAreaId.ValueMember = "cAreaId";
            if (cmb_cAreaId.Items.Count > 0)
            {
                if (_WHId.Trim() !="" && _AreaId.Trim() != "")
                {
                    cmb_cAreaId.SelectedValue = _AreaId.Trim();
                }
            }
        }

        private void BindCmd()
        {
            bWHIsOK = false;
            int iX = (int)_WareHouseType;
           
            string sql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (iX >0)
                sql = sql + " where nType=" + iX.ToString();
            if (UserInformation.UType != UserType.utSupervisor)
            {
                sql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            string err = "";
            DataSet ds = PubDBCommFuns.GetDataBySql(sql, out err);            
            if (err.Trim() == "" || err.Trim()=="0")
            {
                cmb_cWHId.DataSource = ds.Tables["data"];
                cmb_cWHId.ValueMember = "cWHId";
                cmb_cWHId.DisplayMember = "cName";
                bWHIsOK = true;
                cmb_cWHId.SelectedIndex = -1;
            }
            else
                MessageBox.Show("仓库信息初始化失败 "+err);
        }

        #endregion

        #region 公共属性
        public bool BIsResult
        {
            get { return (bIsResultOK); }
            set { bIsResultOK = value; }
        }
        public string SelResult
        {
            get { return (selResult); }
            set { selResult = value; }
        }

        private bool _IsMultiSelect = false;
        public bool IsMultiSelect
        {
            get { return _IsMultiSelect; }
            set
            {
                _IsMultiSelect = value;
                if (!_IsMultiSelect)
                {
                    toolTip1.SetToolTip(grdCellList, "请双击或单击选择，再点确定按钮！");
                }
                grdCellList.MultiSelect = value;
            }
        }

        private string _SelPalletId = "";
        public string SelPalletId
        {
            get { return _SelPalletId.Trim(); }
            set { _SelPalletId = value.Trim(); }
        }

        private string _SelAreaId = "";
        public string SelAreaId
        {
            get { return _SelAreaId.Trim(); }
            set { _SelAreaId = value.Trim(); }
        }

        private string _SelDpsAddr = "";
        public string SelDpsAddr
        {
            get { return _SelDpsAddr.Trim(); }
            set { _SelDpsAddr = value.Trim(); }
        }

        private string _WHId = "";
        public string WHId
        {
            get { return _WHId.Trim(); }
            set
            { 
                _WHId = value.Trim();
                if (cmb_cWHId.Items.Count > 0)
                {
                    cmb_cWHId.SelectedValue = _WHId.Trim();
                    cmb_cWHId.Enabled = false;
                }
                else
                {
                    cmb_cWHId.Enabled = true;
                }
            }
        }

        private string _AreaId = "";
        public string AreaId
        {
            get { return _AreaId.Trim(); }
            set
            {
                _AreaId = value.Trim();
                if (cmb_cAreaId.Items.Count > 0)
                {
                    cmb_cAreaId.SelectedValue = _AreaId.Trim();
                    cmb_cAreaId.Enabled = false;
                }
                else
                {
                    cmb_cAreaId.Enabled = true;
                }
            }
        }

        private WareType _WareHouseType = WareType.wtNone;
        public WareType WareHouseType
        {
            get { return _WareHouseType; }
            set { _WareHouseType = value; }

        }
        
        #endregion

        #region 公共方法

        #endregion

        public FrmSelectCell()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            bIsResultOK = false;
            Close();
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            
            QueryCellList();
        }

        private void grdCellList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //bIsResultOK = false;
            //if (bdsList.Count == 0) return;
            //DataRowView drvX = (DataRowView)bdsList.Current;
            //if (drvX == null) return;
            //selResult = drvX["cPosId"].ToString();           
            //bIsResultOK = true;
            //if (bIsResultOK) Close();
            btnOK_Click(null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //grdCellList_CellDoubleClick(null, null);
            bool bIsFirst = true;
            object objX = null;
            foreach (DataGridViewRow grdr in grdCellList.SelectedRows)
            {
                if (bIsFirst)
                {
                    selResult = "" + grdr.Cells[colcCellId.Name].Value.ToString() + "";

                    objX = grdr.Cells[colnPalletId.Name].Value;
                    if (objX != null)
                        _SelPalletId = objX.ToString().Trim();
                    else
                        _SelPalletId = "";

                    objX = grdr.Cells[colcAreaId.Name].Value;
                    if (objX != null)
                        _SelAreaId = objX.ToString().Trim();
                    else
                        _SelAreaId = "";

                    objX = grdr.Cells[colnDpsAddr.Name].Value;
                    if (objX != null)
                        _SelDpsAddr = objX.ToString().Trim();
                    else
                        _SelDpsAddr = "";

                    bIsFirst = false;
                }
                else
                {
                    selResult = selResult + "," + "" + grdr.Cells[colcCellId.Name].Value.ToString() + "";

                    objX = grdr.Cells[colnPalletId.Name].Value;
                    if (objX != null)
                        _SelPalletId +=","+ objX.ToString().Trim();
                    else
                        _SelPalletId += "," + "";

                    objX = grdr.Cells[colcAreaId.Name].Value;
                    if (objX != null)
                        _SelAreaId += "," + objX.ToString().Trim();
                    else
                        _SelAreaId += "," + "";

                    objX = grdr.Cells[colnDpsAddr.Name].Value;
                    if (objX != null)
                        _SelDpsAddr += "," + objX.ToString().Trim();
                    else
                        _SelDpsAddr += "," + "";
                }
            }
            if (selResult.Trim() == "")
            {
                if (MessageBox.Show("没有选择数据，需要退出吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    bIsResultOK = false;
                    return;
                }
            }
            bIsResultOK = true;
            Close();
        }

        private void grdCellList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && (e.KeyCode == Keys.Return))
                grdCellList_CellDoubleClick(null, null);
        }

        private void FrmSelectCell_Load(object sender, EventArgs e)
        {
            if (!_IsMultiSelect)
            {
                toolTip1.SetToolTip(grdCellList, "请双击或单击选择，再点确定按钮！");
            }
            grdCellList.MultiSelect = _IsMultiSelect;

            BindCmd();
            if (_WHId.Trim() != "" && _AreaId.Trim() != "")
            {
                if (cmb_cWHId.SelectedValue != null)
                {
                    LoadAreaList(cmb_cWHId.SelectedValue.ToString().Trim());
                    cmb_cAreaId.SelectedValue = _AreaId.Trim();
                    cmb_cAreaId.Enabled = false;
                }
            }
            else
            {
                if (cmb_cWHId.SelectedValue != null)
                {
                    LoadAreaList(cmb_cWHId.SelectedValue.ToString().Trim());
                }
            }
            //cmb_nState.SelectedIndex = -1;
            //btnQry_Click(null, null);
            cmb_cWHId.Focus();
        }

        private void cmb_nState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SendKeys.Send("{Tab}");
        }

        private void cmb_cWHId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!bWHIsOK) return;
            if (cmb_cWHId.SelectedValue == null)
                return;
            string sId = cmb_cWHId.SelectedValue.ToString().Trim();
            LoadAreaList(sId);

        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            cmb_cABC.SelectedIndex = -1;
            this.cmb_cWHId.SelectedIndex = -1;
            this.cmb_cAreaId.SelectedIndex = -1;
            this.cmb_cTypeId1.SelectedIndex = -1;
            this.cmb_nState.SelectedIndex = -1;
            txt_cDtlRemark.Text = "";
            txt_cMatOther.Text = "";
            txt_cMatQCLevel.Text = "";
            txt_cMatStyle.Text = "";
            txt_cName.Text = "";
            txt_cRemark.Text = "";
            txt_cSpec.Text = "";
            txt_cSupplier.Text = "";
            txt_nCol.Text = "0";
            txt_nLayer.Text = "";
            txt_nRow.Text = "";
            cmb_cWHId.Focus();
        }
    }
}