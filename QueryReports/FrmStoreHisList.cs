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
    public partial class FrmStoreHisList : UI.FrmSTable
    {
        private DataSet dsX = new DataSet();
        public FrmStoreHisList()
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
            string strSql1 = "select cName from TPB_User where  bUsed = 1 and cCmptId='" + UserInformation.UnitId + "' ";
 
            if (UserInformation.UType == UserType.utNormal)
            {
               strSql1+= " and cName='" + UserInformation.UserName.Trim() + "'";
            }
            if (UserInformation.UType == UserType.utAdmin)
            {
                 strSql1+=" and cDeptId='" + UserInformation.DeptId.Trim() + "'";
            }

            string err1 = "";
            DataTable tbWare1 = new DataTable();
            try
            {
                DataSet ds = PubDBCommFuns.GetDataBySql(strSql1, out err);
                if (err1 != "")
                    MessageBox.Show(strSql1, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    tbWare1 = ds.Tables["data"].Copy();
                    cmbUserId.DisplayMember = "cName";
                    cmbUserId.ValueMember = "cName";
                    cmbUserId.DataSource = tbWare1;
                    if (UserInformation.UserName != "Admin5118")
                    {
                        cmbUserId.SelectedValue = UserInformation.UserName;
                    }
                }
            }
            catch (Exception er1)
            {
                MessageBox.Show(er1.Message);
            }
        }
        public DataSet GetDataSet()
        {
            try
            {
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "sp_GetDoStoreHisList :pFromDate,:pToDate,:pWHId,:pMNo,:pUser,:pOrderValue";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pFromDate";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pToDate";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
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
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = txtMNo.Text.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pUser";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = cmbUserId.Text.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pOrderValue";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = "";            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                //ִ������
                SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "StoreHisList";
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                string sX = dsY.Tables[1].TableName;
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
            //string sX = "";
            //string sErr = "";
            //string sDateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd��00:00:00");
            //string sDateTo = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");
            //string sWHId = "";
            //if (cmbWHId.Text.Trim() != "" && cmbWHId.SelectedValue != null)
            //{
            //    sWHId = cmbWHId.SelectedValue.ToString().Trim();
            //}
            //DataTable tbX = PubDBCommFuns.sp_GetDoStoreHisList(AppInformation.SvrSocket, sDateFrom, sDateTo, sWHId, txtMNo.Text.Trim(), cmbUserId.Text.Trim(), "", out sErr);
            //if (sErr.Trim() != "" && sErr.Trim() != "0")
            //{
            //    MessageBox.Show(sErr);
            //    return ;
            //}
            //if (tbX != null)
            //{
            //    tbX.TableName = "StoreHisList";
            //    dataGridView1.DataSource = tbX;
            //}
            
            dsX.Clear();
            dsX = GetDataSet();
            dataGridView1.DataSource = dsX.Tables["StoreHisList"];
           
        }

        private void FrmStoreHisList_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            InitCmb();
            dateTimePicker2.Value = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            //GetDataGridView();
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
            FrmRptShow.rptds = dsX;
            FrmRptShow.rptType = "StoreHisList";
            FrmRptShow frmrptshow = new FrmRptShow();
            frmrptshow.ShowDialog();
            //frmrptshow.Activate();
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

