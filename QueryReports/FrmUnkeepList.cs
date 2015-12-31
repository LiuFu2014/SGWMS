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
    public partial class FrmUnkeepList : UI.FrmSTable
    {
        private DataSet dsX = new DataSet();
        public FrmUnkeepList()
        {
            InitializeComponent();
        }
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
                    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    cmbWHId.DisplayMember = "cName";
                    cmbWHId.ValueMember = "cWHId";
                    cmbWHId.DataSource = tbWare;
                    cmbWHId.SelectedIndex = -1;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private DataTable GetKeepData()
        {
            /*
             * select lst.*,mat.cName,cell.cPosId,cell.cWhId,datediff(day,getdate(),lst.dValiDate) badDays
from 
(
select cMNo cItemId, sum(fQty) fQty,cUnit,cBatchNo,min(dProdDate) dProdDate,min(isnull(dValiDate,'1900-01-01')) dValiDate,max(nQCStatus) nQCStatus,cBNoIn,nItemIn,nPalletId,cBoxId,
	  max(isnull(cDtlRemark,' ')) cStoreRemark
    from TWB_StockDtl  where nstatus=1 
and cMNo like '%%'
and datediff(day,getdate(),dValiDate) < 400
  group by cMNo, cUnit,cBatchNo,cBNoIn,nItemIn,nPalletId,cBoxId
	   having sum(fQty)>0
) lst 
  left join TPC_Material mat on lst.cItemId=mat.cMNo
left join twc_warecell cell on lst.nPalletId=cell.nPalletId
where mat.cName like '%%'
             */
            string err = "";
            string sql = string.Format("select lst.*,mat.cName,mat.cSpec,cell.cPosId,cell.cWhId,cast((lst.dValiDate-sysdate) as int) badDays from (select cItemId, sum(fQty) fQty,cUnit,cBatchNo,min(dProdDate) dProdDate,min(dBadDate)	  dValiDate,max(nQCStatus) nQCStatus,nPalletId,cBoxId,max(isnull(cDtlRemark,' ')) cStoreRemark     from V_StoreItemList where 1=1 ");
 
            if (this.txt_Day.Text.Trim() == "")
            {
                this.txt_Day.Text = "0";              
            }
            sql += string.Format(" and round(to_number(dBadDate-sysdate))  <= '{0}' ", int.Parse(txt_Day.Text.Trim()));

            sql += string.Format(" group by cItemId , cUnit,cBatchNo, dBadDate,nPalletId,cBoxId ) lst    left join TPC_Material mat on lst.cItemId=mat.cMNo left join twc_warecell cell on lst.nPalletId=cell.nPalletId where 1=1 ");

            if (this.txtMNo.Text.Trim() != "")
            {
                sql += string.Format(" and mat.cName like '%{0}%' ", this.txtMNo.Text.Trim());
            }

            DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dBadDate", out err);
            if (err == "" || err == "0")
            {
                return dsY.Tables["data"];
            }
            else
            {
                MyTools.MessageBox(err);
                return new DataTable();
            }
        }
        public DataSet GetDataSet()
        {
            try
            {
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "SP_GETMATERIALUNKEEPDAYLIST :pWHId,:pMNo,:pDay";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          

                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                if (cmbWHId.SelectedValue != null)
                {
                    par.ParameterValue = cmbWHId.SelectedValue.ToString();            //����ֵ ����Ϊ""��
                }
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = txtMNo.Text.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDay";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue =txt_Day.Text.ToString().Trim();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Int;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------

                //------
                //ִ������
                SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "UnkeepList";
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                FrmStockDtlRpt.dsRpt = dsY;
                return dsY;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
        public void GetDataGridView()
        {
            if (txt_Day.Text.Trim() == "")
            {
                MessageBox.Show("������Ч��������Ϊ�գ�");
                txt_Day.Text = "0";
                txt_Day.SelectAll();
                txt_Day.Focus();
                return;
            }
            if(!IsInteger(txt_Day.Text.Trim()))
            {
                MessageBox.Show("������Ч����Ϊ�Ƿ����֣�");
                //txt_Day.Text = "0";
                txt_Day.SelectAll();
                txt_Day.Focus();
                return;
            }
            //dsX.Clear();
            //dsX = GetDataSet();
              dt = GetKeepData();
            dataGridView1.DataSource = dt;
        }
        DataTable dt = new DataTable();
        private void FrmUnkeepList_Load(object sender, EventArgs e)
        {
            InitCmb();
            this.dataGridView1.AutoGenerateColumns = false;
            GetDataGridView();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            GetDataGridView();
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            GetDataGridView();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            //FrmRptShow.rptds = dsX;
            //FrmRptShow.rptType = "UnKeepList";
            //FrmRptShow frmrptshow = new FrmRptShow();
            //frmrptshow.ShowDialog();

            FrmRptShow fsdtl = new FrmRptShow();
            FrmRptShow.rptds = new DataSet();
            FrmRptShow.rptType = "UnKeepList";
            FrmRptShow.rptds.Tables.Add(dt.Copy());
            FrmRptShow.rptds.Tables[0].TableName = "UnKeepList";
            fsdtl.Text = "������Ч�ڱ���";
            fsdtl.ShowDialog();
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            string fileName = SelectFileExporExcel.GetSaveFileNameByDiag();
            if (fileName == "")
            {
                return;
            }
            DataImpExp.DataIE.DataGridViewToExcel(dataGridView1, fileName, Text);
        }
    }
}

