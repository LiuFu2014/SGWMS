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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "sp_GetDoStoreHisList :pFromDate,:pToDate,:pWHId,:pMNo,:pUser,:pOrderValue";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pFromDate";           //参数名称 和实际定义的一致
                par.ParameterValue = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pToDate";           //参数名称 和实际定义的一致
                par.ParameterValue = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
                if (cmbWHId.SelectedValue != null)
                {
                    par.ParameterValue = cmbWHId.SelectedValue.ToString();            //参数值 可以为""空
                }
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
                par.ParameterValue = txtMNo.Text.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pUser";           //参数名称 和实际定义的一致
                par.ParameterValue = cmbUserId.Text.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pOrderValue";           //参数名称 和实际定义的一致
                par.ParameterValue = "";            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                //执行命令
                SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "StoreHisList";
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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
            //string sDateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd　00:00:00");
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

