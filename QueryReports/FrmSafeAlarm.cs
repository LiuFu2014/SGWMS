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
                //DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                //cmdInfo.SqlText = "SP_GETSTOREUNSAFELIST :pWHId";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                //cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                //cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                //cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                //cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                ////cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                ////定义参数
                //ZqmParamter par = null;           //参数对象 变量                          
               
                ////------
                //par = new ZqmParamter();          //参数对象实例
                //par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
                //par.ParameterValue = cmbWHId.SelectedValue.ToString();            //参数值 可以为""空
                //par.DataType = ZqmDataType.String;  //参数的数据类型
                //par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                ////添加参数
                //cmdInfo.Parameters.Add(par);
                ////------

                ////------
                ////执行命令
                //SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
                ////sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                
                //cmdInfo.DataTableName = "SafeAlarm";
                //dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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
                fsdtl.SystemTitle = "仓库最小库存报警";
                fsdtl.Text = "仓库最小库存报警";
            }
            else
            {
                fsdtl.SystemTitle = "仓库最大库存报警";
                fsdtl.Text = "仓库最大库存报警";
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
             * --统计下线的仓库库存数
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
                //下线
                strSql += string.Format("  where ISNULL(st.FQTY,0) < ISNULL(mat.FSAFEQTYDN,0) ");
                if (this.cbxIsHasOther.Checked)
                {
                    strSql += string.Format(" or isnull(mat.FSAFEQTYDN,0) = 0 ");
                }
            }
            else
            {
                //上线
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

