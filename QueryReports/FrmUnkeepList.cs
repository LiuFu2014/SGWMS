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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "SP_GETMATERIALUNKEEPDAYLIST :pWHId,:pMNo,:pDay";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          

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
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
                par.ParameterValue = txtMNo.Text.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDay";           //参数名称 和实际定义的一致
                par.ParameterValue =txt_Day.Text.ToString().Trim();            //参数值 可以为""空
                par.DataType = ZqmDataType.Int;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------

                //------
                //执行命令
                SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "UnkeepList";
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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
                MessageBox.Show("距离有效天数不能为空！");
                txt_Day.Text = "0";
                txt_Day.SelectAll();
                txt_Day.Focus();
                return;
            }
            if(!IsInteger(txt_Day.Text.Trim()))
            {
                MessageBox.Show("距离有效天数为非法数字！");
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
            fsdtl.Text = "物料有效期报表";
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

