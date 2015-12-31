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
using QueryReports.Impi;

namespace SunEast.App
{
    public partial class FrmSysLog : UI.FrmSTable
    {
        public FrmSysLog()
        {
            InitializeComponent();
        }
        private DataSet dsX = new DataSet();
        public void InitDataSet()
        {
            try
            {
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "SP_GETSYSLOG :pDateFrom,:pDateTo";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateFrom";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateTo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
               
                //------
                //ִ������
                SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                string sErr = "";
                cmdInfo.DataTableName = "SysLog";
                dsX.Clear();
                dsX = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                dataGridView1.DataSource = dsX.Tables["SysLog"];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            Close();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            InitDataSet();
        }

        private void FrmSysLog_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Today;
            InitDataSet();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            FrmRptShow.rptds = dsX;
            FrmRptShow.rptType = "SysLog";
            FrmRptShow frmrptshow = new FrmRptShow();
            frmrptshow.ShowDialog();
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

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            InitDataSet();
        }
    }
}

