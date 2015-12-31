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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "SP_GETSYSLOG :pDateFrom,:pDateTo";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateFrom";           //参数名称 和实际定义的一致
                par.ParameterValue = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateTo";           //参数名称 和实际定义的一致
                par.ParameterValue = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
               
                //------
                //执行命令
                SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                string sErr = "";
                cmdInfo.DataTableName = "SysLog";
                dsX.Clear();
                dsX = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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

