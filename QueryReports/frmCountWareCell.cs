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
    public partial class frmCountWareCell : UI.FrmSTable
    {
        public frmCountWareCell()
        {
            InitializeComponent();
        }

        #region ˽�б���
        DataSet dsData = null;

        StringBuilder sCondition = new StringBuilder("");

        #endregion

        #region ˽�з���
        private string GetSql()
        {
            string sX = "";
            sCondition.Remove(0, sCondition.Length);
            StringBuilder sSql  =  new StringBuilder("select cWHId,cWName,cAreaName,cWType,cPalletSpec,cStatusStore,count(*) nCount from V_WareCellStoreState");
            
            sX = GetCondition();
            sSql.Append(sX);
            //--
            sSql.Append(" group by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore  order by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore");
            return sSql.ToString();            
        }

        private string GetCondition()
        {
            string sX = "";
            sCondition.Remove(0, sCondition.Length);
            StringBuilder sSql = new StringBuilder("");
            sSql.Append(" where 1=1 ");
            if (cmbWHId.Text.Trim() != "" && cmbWHId.SelectedValue != null)
            {
                sSql.Append(" and cWHId='" + cmbWHId.SelectedValue.ToString().Trim() + "'");
                sCondition.Append(" �ֿ⣺" + cmbWHId.Text.Trim());
            }
            else
            {
                if (UserInformation.UType != UserType.utSupervisor)
                {
                    sSql.Append(" and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')");
                }
            }
            if (cmb_Area.Text.Trim() != "" && cmb_Area.SelectedValue != null)
            {
                sSql.Append(" and cAreaId='" + cmb_Area.SelectedValue.ToString().Trim() + "'");
                sCondition.Append(" ������" + cmb_Area.Text.Trim());
            }
            if (txt_Row_From.Text.Trim() != "" && IsInteger(txt_Row_From.Text.Trim()))
            {
                sX = " �У�";
                sSql.Append(" and nRow >=" + txt_Row_From.Text.Trim());
                sX += txt_Row_From.Text.Trim() + "��";
                //sCondition.Append(" �У�" + txt_Row_From.Text.Trim());
            }
            if (txt_Row_To.Text.Trim() != "" && IsInteger(txt_Row_To.Text.Trim()))
            {
                sSql.Append(" and nRow <=" + txt_Row_To.Text.Trim());
                if (sX.Trim() != "")
                {
                    sX += "��" + txt_Row_To.Text.Trim();
                }
                else
                {
                    sX = " �У� ��" + txt_Row_To.Text.Trim();
                }
            }
            if (sX.Trim() != "")
            {
                sCondition.Append(sX);
            }
            sX = "";
            //--
            if (txt_Col_From.Text.Trim() != "" && IsInteger(txt_Col_From.Text.Trim()))
            {

                sSql.Append(" and nCol >=" + txt_Col_From.Text.Trim());
                sX = " �У�";
                sX += txt_Col_From.Text.Trim() + "��";
            }
            if (txt_Col_To.Text.Trim() != "" && IsInteger(txt_Col_To.Text.Trim()))
            {
                sSql.Append(" and nCol <=" + txt_Col_To.Text.Trim());
                if (sX.Trim() != "")
                {
                    sX += "��" + txt_Col_To.Text.Trim();
                }
                else
                {
                    sX = " �У� ��" + txt_Col_To.Text.Trim();
                }
            }
            if (sX.Trim() != "")
            {
                sCondition.Append(sX);
            }
            sX = "";
            //--
            if (txt_Layer_From.Text.Trim() != "" && IsInteger(txt_Layer_From.Text.Trim()))
            {
                sSql.Append(" and nLayer >=" + txt_Layer_From.Text.Trim());
                sX = " �㣺";
                sX += txt_Layer_From.Text.Trim() + "��";
            }
            if (txt_Layer_To.Text.Trim() != "" && IsInteger(txt_Layer_To.Text.Trim()))
            {
                sSql.Append(" and nLayer <=" + txt_Layer_To.Text.Trim());
                if (sX.Trim() != "")
                {
                    sX += "��" + txt_Layer_To.Text.Trim();
                }
                else
                {
                    sX = " �㣺 ��" + txt_Layer_To.Text.Trim();
                }
            }
            if (sX.Trim() != "")
            {
                sCondition.Append(sX);
            }
            sX = "";
            //--
            //sSql.Append(" group by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore��order by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore");
            return sSql.ToString();
        }

        private DataSet GetDataTable(string sSql )
        {
            DataSet  tbResult = null;
            try
            {
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = sSql ;                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctSql ;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";
                #region  �������                               
                //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                //ZqmParamter par = null;           //�������� ����                          

                ////------
                //par = new ZqmParamter();          //��������ʵ��
                //par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                //par.ParameterValue = cmbWHId.SelectedValue.ToString();            //����ֵ ����Ϊ""��
                //par.DataType = ZqmDataType.String;  //��������������
                //par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                ////��Ӳ���
                //cmdInfo.Parameters.Add(par);
                ////------

                ////------
                #endregion
                //ִ������
                SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "WareCellCount";
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                tbResult = dsY;
                //return dsY;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return tbResult;
            }
            return tbResult;
        }

        private void CountWCellUsedRate()
        {
            int nUsed = 0 ;
            int nCount = 1 ;
            double fRate = 0 ;
            object objValue = null ;
            string sErr = "";
            string sCond = GetCondition();
            StringBuilder  sSql = new StringBuilder( "select count(*) nCount from V_WareCellStoreState");
            sSql.Append(sCond);
            sSql.Append(" and  nstatusstore >0 ");
            PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, sSql.ToString(),"", "nCount", out objValue, out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (objValue != null)
            {
                nUsed = int.Parse(objValue.ToString());
            }
            sSql.Remove(0, sSql.Length);
            sSql.Append("select count(*) nCount from V_WareCellStoreState");
            sSql.Append(sCond);
            sErr = "";
            PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, sSql.ToString(),"", "nCount", out objValue, out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (objValue != null)
            {
                nCount = int.Parse(objValue.ToString());
            }
            if (nCount == 0)
            {
                nCount = 1;
            }
            fRate = ((nUsed * 1.0) / nCount) * 100;
            lbl_UsedRate.Text = fRate.ToString("##.##") + " %";
        }

        private void InitWareHouse()
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
                DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, strSql,"TWC_WareHouse","", out err);
                if (err != "")
                    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    tbWare = dsY.Tables["TWC_WareHouse"].Copy();
                    cmbWHId.DisplayMember = "cName";
                    cmbWHId.ValueMember = "cWHId";
                    cmbWHId.DataSource = tbWare;
                }
                cmbWHId.SelectedIndex = -1;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void OpenWareAreaList(string sWHId)
        {
            string strSql = "select * from TWC_WArea where bUsed=1 and cWHId='" + sWHId.Trim() + "'";
            string err = "";
            DataTable tbX = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, strSql, "TWC_WArea", "", out err);
                if (err != "")
                    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    tbX = dsY.Tables["TWC_WArea"].Copy();
                    cmb_Area.DisplayMember = "cAreaName";
                    cmb_Area.ValueMember = "cAreaId";
                    cmb_Area.DataSource = tbX;
                }
                cmb_Area.SelectedIndex = -1;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        #endregion

        private void frmCountWareCell_Load(object sender, EventArgs e)
        {
            grdData.AutoGenerateColumns = false;
            InitWareHouse();

        }

        private void cmbWHId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWHId.Text.Trim() != "" && cmbWHId.SelectedValue != null)
            {
                OpenWareAreaList(cmbWHId.SelectedValue.ToString().Trim());
            }
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            tbcMain_SelectedIndexChanged(sender, e);
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            FrmRptShow.rptds = dsData;
            FrmRptShow.rptType = "WareCellCount";
            FrmRptShow frmrptshow = new FrmRptShow();
            frmrptshow.SystemTitle = AppInformation.AppTitle;
            frmrptshow.Condition = sCondition.ToString();
            frmrptshow.ShowDialog();
        }

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tbcMain.SelectedTab.Name)
            {
                     
                case "tbpCellCount":
                    //tlb_M_Find_Click(sender, e);
                    string sSql = GetSql();
                    if (dsData != null)
                    {
                        dsData.Clear();
                    }
                    dsData = GetDataTable(sSql);
                    grdData.DataSource = null;

                    grdData.DataSource = dsData.Tables["WareCellCount"]; 
                    break;
                case "tbpUsedRate":
                    CountWCellUsedRate();
                    break;
            }
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
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

        #region ���з���

        #endregion
    }
}

