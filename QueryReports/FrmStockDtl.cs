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
using System.Collections;
using QueryReports.Impi;

namespace SunEast.App
{
    public partial class FrmStockDtl : UI.FrmSTable
    {
        public static DataSet dsX = new DataSet();
        private string WHId = "";
        private string Pallet = "";
        private string MNo = "";
        private string BatchNo = "";
        private string Qc = "";
        public FrmStockDtl()
        {
            InitializeComponent();
        }
        #region

        public void InitCmb()
        {
            string strSql = "select * from TWC_WareHouse where bUsed=1 ";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            string err = "";
            DataTable tbWare = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
                if (err != "")
                {
                    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    cmb_cWHId.DisplayMember = "cName";
                    cmb_cWHId.ValueMember = "cWHId";
                    cmb_cWHId.DataSource = tbWare;
                    cmb_cWHId.SelectedIndex = -1;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            ArrayList ArrState = new ArrayList();
            ArrState.Add(new DictionaryEntry("0", "δ��"));
            ArrState.Add(new DictionaryEntry("1", "�ϸ�"));
            ArrState.Add(new DictionaryEntry("-1", "���ϸ�"));
            //cmbQCStatus.DataSource = ArrState;
            //cmbQCStatus.DisplayMember = "Value";
            //cmbQCStatus.ValueMember = "Key";
        }

        public void LoadAreaList(string sWHId)
        {
            string sErr = "";
            string sSql = "select * from TWC_WArea where bUsed=1 and cWHId='"+ sWHId +"'";
            DataSet dstt = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dstt = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "data", 0, 0, "", out sErr);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message);
                return; 
            }
            Cursor.Current = Cursors.Default;
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (dstt != null)
            {                
                DataTable tbX = dstt.Tables["data"];                
                cmb_cAreaId.DisplayMember = "cAreaName";
                cmb_cAreaId.ValueMember = "cAreaId";
                cmb_cAreaId.DataSource = tbX;
                cmb_cAreaId.SelectedIndex = -1;
            }

        }
        public void GetDataSet()
        {
            //if (cmb_cWHId.Text != "")
            //    WHId = cmb_cWHId.SelectedValue.ToString();
            //else
            //    WHId = "";
            //Pallet = txtPalletId.Text.ToString();
            //MNo = this.txt_cName.Text.ToString();
            //BatchNo = txt_cBatchNo.Text.ToString();
            ////if (cmb_nQCStatus.Text != "")
            ////    Qc = cmb_nQCStatus.SelectedValue.ToString();
            ////else
            ////    Qc = "";
            ////try
            //{
            //    DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            //    cmdInfo.SqlText = "sp_GetWareHouseItemList :nCountType,:pWHId,:pPalletId,:pMNo,:pBatchNo,:pQCStatus";                             //SQL���  �� �洢������ ���в����������ڲ�����������

            //    cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            //    cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            //    cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            //    cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //    //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            //    //�������
            //    ZqmParamter par = null;           //�������� ����                          
            //    par = new ZqmParamter();          //��������ʵ��
            //    par.ParameterName = "nCountType";           //�������� ��ʵ�ʶ����һ��
            //    par.ParameterValue = "-1";            //����ֵ ����Ϊ""��
            //    par.DataType = ZqmDataType.Int;  //��������������
            //    par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //    //��Ӳ���
            //    cmdInfo.Parameters.Add(par);
            //    //------
            //    par = new ZqmParamter();          //��������ʵ��
            //    par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
            //    par.ParameterValue = WHId;            //����ֵ ����Ϊ""��
            //    par.DataType = ZqmDataType.String;  //��������������
            //    par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //    //��Ӳ���
            //    cmdInfo.Parameters.Add(par);
            //    //------
            //    par = new ZqmParamter();          //��������ʵ��
            //    par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
            //    par.ParameterValue = Pallet;            //����ֵ ����Ϊ""��
            //    par.DataType = ZqmDataType.String;  //��������������
            //    par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //    //��Ӳ���
            //    cmdInfo.Parameters.Add(par);
            //    //------
            //    par = new ZqmParamter();          //��������ʵ��
            //    par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
            //    par.ParameterValue = MNo;            //����ֵ ����Ϊ""��
            //    par.DataType = ZqmDataType.String;  //��������������
            //    par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //    //��Ӳ���
            //    cmdInfo.Parameters.Add(par);
            //    //------
            //    par = new ZqmParamter();          //��������ʵ��
            //    par.ParameterName = "pBatchNo";           //�������� ��ʵ�ʶ����һ��
            //    par.ParameterValue = BatchNo;            //����ֵ ����Ϊ""��
            //    par.DataType = ZqmDataType.String;  //��������������
            //    par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //    //��Ӳ���
            //    cmdInfo.Parameters.Add(par);
            //    //------
            //    par = new ZqmParamter();          //��������ʵ��
            //    par.ParameterName = "pQCStatus";           //�������� ��ʵ�ʶ����һ��
            //    par.ParameterValue = Qc;            //����ֵ ����Ϊ""��
            //    par.DataType = ZqmDataType.Int;  //��������������
            //    par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //    //��Ӳ���
            //    cmdInfo.Parameters.Add(par);

            //    //------
            //    //ִ������
            //    SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //    //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            //    string sErr = "";
            //    cmdInfo.DataTableName = "StockDtl";
            //    dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }
        public void GetDataGridView()
        {
            grdData.DataSource = dsX.Tables["StockDtl"];
        }

        private bool QueryStoreDtlList()
        {
            StringBuilder sCon = new StringBuilder("");
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select st.cItemId cMNo,st.cMName,st.cSpec,st.cMatStyle,st.cMatQCLevel,st.cMatOther,st.cRemark,st.cBatchNo,st.fQty,st.cUnit,");
	        sSql.Append(" st.dProdDate,st.dBadDate,st.cDtlCSId,st.cDtlSupplier,st.cStoreRemark,st.cBNoIn,st.nItemIn,st.nQCStatus,st.cQCStatus, ");
            sSql.Append(" wc.nPalletId,wc.cPosId,wc.cWHId,wc.cAreaId,wa.cAreaName,st.cABC,wh.cName cWHName 	from V_StoreItemList st ");
	        sSql.Append(" left join TWC_WareCell wc on st.nPalletId=isnull(wc.nPalletId,' ') ");
            sSql.Append(" left join TWC_Warehouse wh on wc.cWHId=wh.cWHId ");
	        sSql.Append(" left join TWC_WArea wa on wc.cAreaId=wa.cAreaId");            

            #region ����
            #region
            if (cmb_cWHId.Text.Trim() != "" && cmb_cWHId.SelectedValue != null && cmb_cWHId.SelectedIndex > -1)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and isnull(wc.cWHId,' ')='" + cmb_cWHId.SelectedValue.ToString() + "'");
                }
                else
                {
                    sCon.Append(" where isnull(wc.cWHId,' ')='" + cmb_cWHId.SelectedValue.ToString() + "'");
                }
            }
            if (cmb_cAreaId.Text.Trim() != "" && cmb_cAreaId.SelectedValue != null && cmb_cAreaId.SelectedIndex > -1)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and isnull(wc.cAreaId,' ')='" + cmb_cAreaId.SelectedValue.ToString() + "'");
                }
                else
                {
                    sCon.Append(" where isnull(wc.cAreaId,' ')='" + cmb_cAreaId.SelectedValue.ToString() + "'");
                }
            }
            if (txtPalletId.Text.Trim() != "")
            {
                string sPalletId = txtPalletId.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and st.nPalletId like '%" + sPalletId + "'");
                }
                else
                {
                    sCon.Append(" where st.nPalletId like '%" + sPalletId + "'");
                }
            }
            if (this.txt_cPosId.Text.Trim() != "")
            {
                string sPosId = txt_cPosId.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and isnull(wc.cPosId,' ') like '%" + sPosId + "'");
                }
                else
                {
                    sCon.Append(" where isnull(wc.cPosId,' ') like '%" + sPosId + "'");
                }
            }
            #endregion
            int nX = 0;
           

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
            if (txt_cBatchNo.Text.Trim() != "")
            {
                string sX = txt_cBatchNo.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cBatchNo,'~') like '%" + sX + "%')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cBatchNo,'~') like '%" + sX + "%')");
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
            if (cmb_cABC.Text.Trim() != "")
            {
                string sX = cmb_cABC.Text.Trim();
                if (sCon.Length > 0)
                {
                    sCon.Append(" and (isnull(st.cABC,'~') = '" + sX + "')");
                }
                else
                {
                    sCon.Append(" where (isnull(st.cABC,'~') = '" + sX + "')");
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
            if (txt_cDtlRemark.Text.Trim() != "")
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
            
            bool bIsOK = false;
            //if (DBDataSet.Tables["TWC_WareCell"] != null)
            //    DBDataSet.Tables["TWC_WareCell"].Clear();
            grdData.AutoGenerateColumns = false;
            string sErr = "";
            //string sql = "select * from V_WareCellStatus " + strCon;
            //string sX = BI.BSIOBillBI.BSIOBillBI.QueryCellList(AppInformation.dbtApp, AppInformation.AppConn, DBDataSet, UserInformation, strCon);
            DataSet ds = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dsX.Clear();
                dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql.ToString(), "StockDtl", 0, 0, "", out sErr);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message);
                return false ;
            }
            Cursor.Current = Cursors.Default;
            bIsOK = sErr == "";
            sSql.Remove(0, sSql.Length);
            sCon.Remove(0, sCon.Length);
            if (bIsOK == true)
            {
                grdData.Focus();
                grdData.DataSource = dsX.Tables["StockDtl"];
            }
            else
                MessageBox.Show(sErr);
            return (bIsOK);
        }


        #endregion
        private void FrmStockDtl_Load(object sender, EventArgs e)
        {
            grdData.AutoGenerateColumns = false;
            InitCmb();
            BindCType1Data();
            cmb_cWHId.Focus();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            btnQry_Click(null, null);
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            btnQry_Click(null, null);
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            FrmStockDtlRpt.dsRpt = dsX;
            FrmStockCount.CountType = "99";
            FrmStockDtlRpt fsdtl = new FrmStockDtlRpt();
            fsdtl.Text = "�����ϸ����";
            fsdtl.ShowDialog();
            //fsdtl.Activate();
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            string fileName = SelectFileExporExcel.GetSaveFileNameByDiag();
            if (fileName == "")
            {
                return;
            }
            DataImpExp.DataIE.DataGridViewToExcel(grdData, fileName, Text);
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            QueryStoreDtlList();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            cmb_cABC.SelectedIndex = -1;
            this.cmb_cWHId.SelectedIndex = -1;
            this.cmb_cAreaId.SelectedIndex = -1;
            this.cmb_cTypeId1.SelectedIndex = -1;       
            txt_cDtlRemark.Text = "";
            txt_cMatOther.Text = "";
            txt_cMatQCLevel.Text = "";
            txt_cMatStyle.Text = "";
            txt_cName.Text = "";
            txt_cRemark.Text = "";
            txt_cSpec.Text = "";
            txt_cSupplier.Text = "";
            txtPalletId.Text = "";
            txt_cPosId.Text = "";
            txt_cBatchNo.Text = "";
            cmb_cWHId.Focus();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void BindCType1Data()
        {
            /*select CTYPEID,CTYPENAME from TPC_MATERIALTYPE*/
            string sErr = "";
            string sSql = string.Format("select CTYPEID,CTYPENAME from TPC_MATERIALTYPE");
            DataSet dstt = null;
            try
            {
                dstt = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "data", 0, 0, "", out sErr);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (dstt != null)
            {
                DataTable tbX = dstt.Tables["data"];
                cmb_cTypeId1.DisplayMember = "CTYPENAME";
                cmb_cTypeId1.ValueMember = "CTYPEID";
                cmb_cTypeId1.DataSource = tbX;
                cmb_cTypeId1.SelectedIndex = -1;
            }
        }

        private void cmb_cWHId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ѡ��ֿ��ʱ��ȥ���ض�Ӧ�Ļ�����
            /*select CAREAID,CAREANAME from TWC_WAREA where CWHID ='A'
             */
            if (this.cmb_cWHId.SelectedIndex != -1)
            {
                string whid = this.cmb_cWHId.SelectedValue.ToString();
                LoadAreaList(whid);
            }                
         
        }
    }
}

