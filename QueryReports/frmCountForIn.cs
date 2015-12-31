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
    public partial class frmCountForIn : UI.FrmSTable
    {
        public frmCountForIn()
        {
            InitializeComponent();
        }

        #region 私有变量
        DataSet dsData = null;
        bool bIsEx = false;
        StringBuilder sCondition = new StringBuilder("");

        #endregion

        #region 私有方法
        private string GetSql()
        {
            string sX = "";
            sCondition.Remove(0, sCondition.Length);
            StringBuilder sSql = new StringBuilder("select cWHId,cWName,cAreaName,cWType,cPalletSpec,cStatusStore,count(*) nCount  from V_WareCellStoreState");

            sX = GetCondition();
            sSql.Append(sX);
            //--
            sSql.Append(" group by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore　order by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore");
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
                sCondition.Append(" 仓库：" + cmbWHId.Text.Trim());
            }
            else
            {
                if (UserInformation.UType != UserType.utSupervisor)
                {
                    sSql.Append(" and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')");
                }
            }
            sCondition.Append(" 入库日期：" + dtp_From.Value.ToString("yyyy-MM-dd") + " — " + dtp_To.Value.ToString("yyyy-MM-dd"));
            if (txt_cMNo.Text.Trim() != "")
            {
                sCondition.Append(" 物料：" + txt_cMNo.Text.Trim());
            }
            if (cmb_cBillType.Text.Trim() != "" && cmb_cBillType.SelectedValue != null)
            {
                sCondition.Append(" 入库类型：" + cmb_cBillType.Text.Trim());
            }
            if (txt_cBNo.Text.Trim() != "")
            {
                sCondition.Append(" 入库单号：" + txt_cBNo.Text.Trim());
            }
            if (cmb_cMatClass.Text.Trim() != "")
            {
                sCondition.Append(" 物资种类：" + cmb_cMatClass.SelectedValue.ToString());
            }
            if (txt_cFileNo.Text.Trim() != "")
            {
                sCondition.Append(" 批文：" + txt_cFileNo.Text.Trim());
            }
            sX = "";
            //--
            //sSql.Append(" group by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore　order by cWType,cWHId,cWName,cAreaName,cPalletSpec,cStatusStore");
            return sSql.ToString();
        }

        private DataSet GetDataTable(string sSql)
        {
            DataSet tbResult = null;
            try
            {
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = sSql;                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";
                #region  定义参数
                //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
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
                #endregion
                //执行命令
                SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "WareCellCount";
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);;//sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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



        /// <summary>
        /// 根据系统参数，确定是否启用扩展表
        /// </summary>
        private void DoIsUseExTable()
        {
            string sErr = "";
            object objValue = "";
            string sSql = "select cParValue from TPS_SysPar where cParId='nInBillIsEx'";
            PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "cParValue", out objValue, out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            else
            {
                if (objValue != null)
                {
                    bIsEx = objValue.ToString().Trim() != "0";
                }
            }
        }



        private void OpenWareAreaList(string sWHId)
        {
            //string strSql = "select * from TWC_WArea where bUsed=1 and cWHId='" + sWHId.Trim() + "'";
            //string err = "";
            //DataTable tbX = new DataTable();
            //try
            //{
            //    DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, strSql, "TWC_WArea", "", out err);
            //    if (err != "")
            //        MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    else
            //    {
            //        tbX = dsY.Tables["TWC_WArea"].Copy();
            //        cmb_Area.DisplayMember = "cAreaName";
            //        cmb_Area.ValueMember = "cAreaId";
            //        cmb_Area.DataSource = tbX;
            //    }
            //    cmb_Area.SelectedIndex = -1;
            //}
            //catch (Exception er)
            //{
            //    MessageBox.Show(er.Message);
            //}
        }

        private void LoadBaseItemFromDB()
        {
            //基本单位
            string strSql = "";
            string err = "";

            //仓库
            //dsX.Clear();
            DataSet dsY = null;
            #region 仓库数据
            strSql = "select * from TWC_WareHouse where 1=1 ";

            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql += "and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId + "')";
            }
            err = "";
            DataTable tbWare = new DataTable();
            dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                tbWare = dsY.Tables["data"].Copy();

                //
                this.cmbWHId.DisplayMember = "cName";
                cmbWHId.ValueMember = "cWHId";
                cmbWHId.DataSource = tbWare;

            }
            #endregion

            //出入库类型
            //dsX.Clear();
            #region 出入库类型
            strSql = "select * from TPB_BillType where nBClass=1 and bUsed=1 order by nBClass,nSort";
            err = "";
            DataTable tbBillType = new DataTable();
            //strSql = BI.BSIOBillBI.BSIOBillBI.GetBillIOTypeList(AppInformation.dbtApp, AppInformation.AppConn, dsX, UserInformation, " where nOperate=" + nOperator.ToString());
            DataSet dsZ = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                tbBillType = dsZ.Tables["data"].Copy();
                cmb_cBillType.DisplayMember = "cBType";
                cmb_cBillType.ValueMember = "cBtypeId";
                cmb_cBillType.DataSource = tbBillType;
            }
            #endregion

            //供货单位  类别（0:供应商 1:客户）
            #region 客户供应商数据
            //strSql = "select * from TPB_CuSupplier where  nType=1 ";
            //DataSet dsSupply = PubDBCommFuns.GetDataBySql(strSql, out err);
            //if (err != "")
            //    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //{

            //    this.cmb_cDept.DisplayMember = "cCSNameJ";
            //    cmb_cDept.ValueMember = "cCSNameJ";
            //    cmb_cDept.DataSource = dsSupply.Tables["data"];
            //}
            #endregion

            #region 扩展表的码表数据
            if (bIsEx)
            {
                

                DataSet dsZHType = null;
                //strSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='自然灾害种类' order by nSort,nId";
                //dsZHType = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, out err);
                //if (err.Trim() != "" && err.Trim() != "0")
                //    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else
                //{
                //    this.cmb_cEventType.DisplayMember = "cItemName";
                //    cmb_cEventType.ValueMember = "cItemName";
                //    cmb_cEventType.DataSource = dsZHType.Tables["data"];
                //}
                strSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='物资种类' order by nSort,nId";
                if(dsZHType != null)
                dsZHType.Clear();
                dsZHType = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, out err);
                if (err.Trim() != "" && err.Trim() != "0")
                    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    this.cmb_cMatClass.DisplayMember = "cItemName";
                    cmb_cMatClass.ValueMember = "cItemName";
                    cmb_cMatClass.DataSource = dsZHType.Tables["data"];
                }
                //if(dsZHType != null)
                //dsZHType.Clear();
                //strSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='启动级别' order by nSort,nId";
                //dsZHType = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, out err);
                //if (err.Trim() != "" && err.Trim() != "0")
                //    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else
                //{
                //    this.cmb_cStartLevel.DisplayMember = "cItemName";
                //    cmb_cStartLevel.ValueMember = "cItemName";
                //    cmb_cStartLevel.DataSource = dsZHType.Tables["data"];
                //}
            }
            #endregion
        }



        private void LoadBaseItem()
        {
            LoadBaseItemFromDB();
        }



        #endregion

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            //重置条件
            cmb_cBillType.SelectedIndex = -1;
            cmb_cMatClass.SelectedIndex = -1;
            cmbWHId.SelectedIndex = -1;
            txt_cBNo.Text = "";
            txt_cFileNo.Text = "";
            txt_cMNo.Text = "";
            dtp_From.Value = DateTime.Now.AddMonths(-3);
            dtp_To.Value = DateTime.Now;
            cmbWHId.Focus();
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            string sWHId = "";
            string sBTypeId = "";
            string sErr = "";
            if (cmbWHId.Text.Trim() != "" && cmbWHId.SelectedValue != null)
            {
                sWHId = cmbWHId.SelectedValue.ToString();
            }
            if (cmb_cBillType.Text.Trim() != "" && cmb_cBillType.SelectedValue != null)
            {
                sBTypeId = cmb_cBillType.SelectedValue.ToString();
            }
            
            Cursor.Current = Cursors.WaitCursor;
            dsData = PubDBCommFuns.sp_GetIOListExt(AppInformation.SvrSocket, 1, sWHId, txt_cMNo.Text.Trim(), sBTypeId, txt_cBNo.Text.Trim(), dtp_From.Value.ToString("yyyy-MM-dd 00:00:00"), dtp_To.Value.ToString("yyyy-MM-dd 23:59:59"),
                "", cmb_cMatClass.Text.Trim(), "", "", txt_cFileNo.Text.Trim(), out sErr);
            Cursor.Current = Cursors.Default;
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            grdData.DataSource = dsData.Tables[1];
            dsData.Tables[1].TableName = "IOList_Ext";
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

        private void frmCountForIn_Load(object sender, EventArgs e)
        {
            grdData.AutoGenerateColumns = false;
            DoIsUseExTable();
            LoadBaseItem();
            tlb_M_Save_Click(null, null);
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            FrmRptShow.rptds = dsData;
            FrmRptShow.rptType = "InList_Ext";
            FrmRptShow frmrptshow = new FrmRptShow();
            frmrptshow.SystemTitle = UserInformation.UnitName;
            frmrptshow.Condition = sCondition.ToString();
            frmrptshow.ShowDialog(); 
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

