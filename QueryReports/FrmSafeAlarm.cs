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
    public partial class FrmSafeAlarm : UI.FrmSTable
    {
        private DataSet dsX=new DataSet();
        public FrmSafeAlarm()
        {
            InitializeComponent();
        }  
        public void InitCmb()
        {
           // WareHouseImpi impi = new WareHouseImpi();
           //DataTable mydt= impi.GetData(UserInformation);
           // if (mydt.Rows.Count > 0)
           // {
           //     cmbWHId.DisplayMember = "cName";
           //     cmbWHId.ValueMember = "cWHId";
           //     cmbWHId.DataSource = mydt;
           //     cmbWHId.SelectedIndex = 0;
           // }
           // else
           // {
           //     cmbWHId.DataSource = null;
           // }
            
        }
        public DataSet GetDataSet()
        {
            string sErr = "";
            DataSet dsY = null;
            try
            {
                #region
                //DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                //cmdInfo.SqlText = "SP_GETSTOREUNSAFELIST :pWHId";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                //cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                //cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                //cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                //cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                ////cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                ////�������
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
                ////ִ������
                //SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                ////sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                
                //cmdInfo.DataTableName = "SafeAlarm";
                //dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                //FrmStockDtlRpt.dsRpt = dsY;
                #endregion
                return dsY;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
       

        private void FrmSafeAlarm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            this.cbxSaftType.SelectedIndex = 0;
            InitCmb();

            btnFind_Click(null, null);
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            //GetDataGridView();
            btnFind_Click(null, null);
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            //GetDataGridView();
            btnFind_Click(null, null);
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        { 
            FrmRptShow fsdtl = new FrmRptShow();
            FrmRptShow.rptds = new DataSet();
            FrmRptShow.rptType = "SafeAlarm";
            FrmRptShow.rptds.Tables.Add(dt.Copy());
            FrmRptShow.rptds.Tables[0].TableName = "SafeAlarm";
            if (this.cbxSaftType.SelectedIndex == 0)
            {
                fsdtl.SystemTitle = "�ֿ���С��汨��";
                fsdtl.Text = "�ֿ���С��汨��";
            }
            else
            {
                fsdtl.SystemTitle = "�ֿ�����汨��";
                fsdtl.Text = "�ֿ�����汨��";
            }
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
        DataTable dt = new DataTable();
        private void btnFind_Click(object sender, EventArgs e)
        {
            //
            if (this.cbxSaftType.SelectedIndex == 0)
            {
                  dt= GetDataByDnUp(true);
                this.dataGridView1.DataSource = dt;
            }
            else
            {
                dt = GetDataByDnUp(false);
                this.dataGridView1.DataSource = dt;
            }
        }

        private DataTable GetDataByDnUp(bool isDnSaft)
        {
            /*
             * --ͳ�����ߵĲֿ�����
                select mat.CMNO,mat.CNAME,mat.CSPEC,mat.CUNIT,mat.CTYPEID1,mat.CTYPEID2,mat.FSAFEQTYDN,mat.FSAFEQTYUP,
                mat.CWHID,mat.NKEEPDAY,mat.CMNO,mat.CUNIT,isnull(st.FQTY,0) FQTY 
                from TPC_MATERIAL mat left join
                     (select cMNo, sum(fQty) fQty from V_StoreItemList
	                group by cMNo    
                     ) st on mat.cMNo=st.cMNo
                    where ISNULL(st.FQTY,0) <= ISNULL(mat.FSAFEQTYDN,0)
             */

            string strSql =string.Format( "select mat.CMNO,mat.CNAME,mat.CSPEC,mat.CUNIT,mat.CTYPEID1,mat.CTYPEID2,mat.FSAFEQTYDN,mat.FSAFEQTYUP,mat.CWHID,mat.NKEEPDAY,isnull(st.FQTY,0) FQTY from TPC_MATERIAL mat left join (select cMNo, sum(fQty) fQty from V_StoreItemList group by cMNo) st on mat.cMNo=st.cMNo");
            if (isDnSaft)
            {
                //����
                strSql += string.Format("  where ISNULL(st.FQTY,0) < ISNULL(mat.FSAFEQTYDN,0) ");
                if (this.cbxIsHasOther.Checked)
                {
                    strSql += string.Format(" or isnull(mat.FSAFEQTYDN,0) = 0 ");
                }
            }
            else
            {
                //����
                strSql += string.Format("  where ISNULL(st.FQTY,0) > ISNULL(mat.FSAFEQTYUP,0) ");
                if (this.cbxIsHasOther.Checked)
                {
                    strSql += string.Format(" or isnull(mat.FSAFEQTYUP,0) = 0 ");
                }
            }
            
            string err = "";
            DataTable tbWare = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
                if (err != "" && err != "0")
                { 
                    MyTools.MessageBox(strSql);
                    return new DataTable();
                }
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    return tbWare;
                }
            }
            catch (Exception er)
            {
                MyTools.MessageBox(er.Message);
                return new DataTable();
            }
        }
    }
}

