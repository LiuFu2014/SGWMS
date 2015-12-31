using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using Zqm.Text;
using SunEast;
using System.Collections;
using SunEast.App;

namespace WareStoreMS
{
    public partial class frmMergePallet : UI.FrmSTable
    {
        public frmMergePallet()
        {
            InitializeComponent();
        }

        #region 私有变量
        //App.WMSUserInfo UserDataInfo = null;
        string strTbNameMain = "TWB_BillMergePlt";
        string strTbNameDtl = "TWB_BillMergePltDtl";



        DataSet dsM = new DataSet();
        DataSet dsD = new DataSet();
        //主表操作
        OperateType optMain = OperateType.optNone;
        OperateType optDtl = OperateType.optNone;
        //记录当前数据列表的 条件
        bool bDSIsOpenForMain = false; //记录数据集是否打开
        bool bDSIsOpenForDtl = false; //记录数据集是否打开

        //
        ArrayList ArrBillState = new ArrayList(); //单据状态 0:初始化 1:有明细 2:审核 3:已经分盘 4:已下达指令 5:执行指令 9:完成
        ArrayList ArrExecState = new ArrayList(); //执行状态(0:待组盘 1:组盘中 2:组盘结束 3:执行中 4:执行结束 )
        ArrayList ArrExecState1 = new ArrayList();
        ArrayList ArrQCState = new ArrayList(); //质检状态(0:待检 1:合格 -1:不合格)
        ArrayList ArrQCState1 = new ArrayList();

        #endregion

        #region 私有方法
        /// <summary>
        /// 根据当前的操作显示当前的操作状态
        /// </summary>
        /// <param name="stbSt"></param>
        /// <param name="optX"></param>
        private void DisplayState(ToolStripLabel stbSt, OperateType optX)
        {
            string strText = "【状态】";
            if (stbSt != null)
            {
                switch (optX)
                {
                    case OperateType.optNew:
                        strText = strText + " 新建";
                        break;
                    case OperateType.optEdit:
                        strText = strText + " 修改";
                        break;
                    default:
                        strText = strText + "    ";
                        break;
                }
                Update();
            }

        }

   
        private void LoadBaseItemFromDB()
        {
            //基本单位
            string strSql = "";
            string err = "";
            //DataSet dsX1 = new DataSet();
            //DataTable tbUnit = new DataTable();
            //dsX1 = PubDBCommFuns.GetDataBySql(strSql, out err);
            //if (err != "")
            //    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //{
            //    tbUnit = dsX1.Tables["data"].Copy();
            //    cmb_Dtl_cUnit.DataSource = tbUnit;
            //    cmb_Dtl_cUnit.DisplayMember = "cCName";
            //    cmb_Dtl_cUnit.ValueMember = "cUnitId";
            //}

            //仓库
            //dsX.Clear();
            int nWareType = (int)wtWareType;
            strSql = "select * from TWC_WareHouse where 1=1 ";
            if (wtWareType != WareType.wtNone)
            {
                strSql += " and nType=" + nWareType.ToString();
            }
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql += "and cWHId in  (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId + "')";
            }
            err = "";
            //DataTable tbWare = new DataTable();
            //DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
            //if (err != "")
            //    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //{
            //    tbWare = dsY.Tables["data"].Copy();

            //    colcWHId.DisplayMember = "cName";
            //    colcWHId.ValueMember = "cWHId";
            //    colcWHId.DataSource = tbWare;
            //}
            
            strSql = "select cUserId,cName from TPB_User where bUsed=1 ";
            if (UserInformation.UType == UserType.utNormal)
            {
                strSql += " and cUserId='" + UserInformation.UserId + "'";
            }
            else if (UserInformation.UType == UserType.utAdmin)
            {
                strSql += " and cDeptId='" + UserInformation.DeptId.Trim() + "'";
            }
            DataSet dsUser = PubDBCommFuns.GetDataBySql(strSql, out err);
            cmbFindUser.DisplayMember = "cName";
            cmbFindUser.ValueMember = "cName";
            DataTable tbFUser = dsUser.Tables["data"];
            DataRow drX = null;
            drX = tbFUser.NewRow();
            drX["cUserId"] = "ERP";
            drX["cName"] = "ERP";
            tbFUser.Rows.Add(drX);
            cmbFindUser.DataSource = tbFUser;
            DataTable tbMUser = tbFUser.Copy();
            cmb_cCreator.DisplayMember = "cName";
            cmb_cCreator.ValueMember = "cName";
            cmb_cCreator.DataSource = tbMUser;
  

           
        }
        private void LoadBaseItemFromArr()
        {
            
            //colnQCState.DataSource = ArrQCState1;
            //colnQCState.DisplayMember = "Value";
            //colnQCState.ValueMember = "Key";
        }
        private void LoadBaseItem()
        {
            LoadBaseItemFromDB();
            LoadBaseItemFromArr();
        }

        private string GetTitleText()
        {
            string sX = "";
            switch (wtWareType)
            {
                case WareType.wt3D:
                    sX = "——立体仓库";
                    break;
                case WareType.wt2D:
                    sX = "——平面仓库";
                    break;
                case WareType.wtDPS:
                    sX = "——DPS仓库";
                    break;
            }
            return (ModuleRtsName + sX);
        }

        /// <summary>
        /// 根据系统参数，确定是否启用扩展表
        /// </summary>
       
       


        /// <summary>
        /// 调用线程安全DLL文件的方法
        /// </summary>
        /// <param name="sFile">DLL文件名（包括路径）</param>
        /// <param name="sClassName">类名</param>
        /// <param name="sFunName">方法名称</param>
        /// <param name="parms">方法的参数数组</param>
        private void MyCallSafeDllFun(string sFile, string sClassName, string sFunName, object[] parms)
        {
            bool bOK = false;
            if (System.IO.File.Exists(sFile))
            {
                //object[] parms = new object[] { ainfo, userInfo, mnItem.Name };
                FileFun.MyCallSafetyDll.DoCallMyDll(sFile, sClassName, sFunName, parms, out  bOK);
            }
            else
            {
                MessageBox.Show(sFile + "  不存在！");
            }
        }

        #endregion


        #region 公共属性

        private WareType wtWareType = WareType.wt3D;
        /// <summary>
        /// 仓库类型
        /// </summary>
        [Description("仓库类型")]
        public WareType WTWareType
        {
            get { return wtWareType; }
            set
            {
                wtWareType = value;
                Text = GetTitleText();
            }
        }

        #endregion

        #region 公共方法
        public override void InitFormParameters()
        {
            ModuleRtsId = "3411";
            ModuleRtsName = "合盘管理";
            //初始化工具按钮权限标志
            //InitFormTlbBtnTag(tlbMain, ModuleRtsId);
        }
        public void BindMainDataSetToCtrls()
        {
            //先清掉绑定
            //DataSetUnBind(pnlEdit);
            //绑定数据集
            //DataSetBind(pnlEdit, this.bdsMain);
            grdList.DataSource = null;
            grdList.DataSource = bdsMain;
        }
        public void BindDtlDataSetToCtrls()
        {
            //先清掉绑定
            //DataSetUnBind(pnlEdit);
            //绑定数据集
            //DataSetBind(pnlEdit, this.bdsMain);
            grdDtl.DataSource = null;
            grdDtl.DataSource = bdsDtl;
        }
        public bool OpenMainDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            string sId = "";
            bDSIsOpenForMain = false;
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select * from TWB_BillIn " + sCon;
            if (wtWareType != WareType.wtNone)
            {
                sql += " and cWHId in (select cWHId from TWC_WareHouse where nType=" + ((int)wtWareType).ToString() + ")";
            }
            sql += " order by cBNo desc";
            string err = "";
            //if (dsM.Tables["data"] != null)
            //    dsM.Tables["data"].Clear();
            int iPos = bdsMain.Position;
            dsM.Clear();
            dsM = PubDBCommFuns.GetDataBySql(sql, "dDate,dCheckDate,dCreateDate,dEditDate", out err);
            //DBDataSet.Tables[strTbNameMain] = ds.Tables[strTbNameMain].Copy();
            bIsOK = err == "";
            if (!bIsOK)
                MessageBox.Show(strX);
            else
            {
                try
                {

                    sId = "";
                    DataTable tbX = dsM.Tables["data"];
                    this.bdsMain.DataSource = tbX;
                    BindMainDataSetToCtrls();
                    bdsMain.Position = iPos;
                    ClearUIValues(pnlEdit);
                    lbl_Check.Visible = false;
                    lbl_BillTskIsOver.Visible = false;
                    if (bdsMain.Count > 0)
                    {
                        DataRowView drX = (DataRowView)bdsMain.Current;
                        DataRowViewToUI(drX, pnlEdit);

                        //更新审核人提示
                        if (drX["bIsChecked"].ToString().Trim() == "0" && drX["cChecker"].ToString().Trim() != "")
                            lblChecker.Text = "取消审核人";
                        else
                            lblChecker.Text = "审核人：";
                        lbl_Check.Visible = true;
                        lbl_BillTskIsOver.Visible = true;
                        if (drX["bIsChecked"].ToString() == "1")
                        {
                            lbl_Check.Text = "已审核";
                        }
                        else
                        {
                            lbl_Check.Text = "未审核";
                        }
                        if (drX["bIsFinished"].ToString() == "1")
                        {
                            this.lbl_BillTskIsOver.Text = "单据作业已完成";
                        }
                        else
                        {
                            lbl_BillTskIsOver.Text = "单据作业未完成";
                        }
                        sId = drX["cBNo"].ToString();
                    }
                    //bdsMain.Position = iPos ;
                    OpenDtlDataSet(" where cBNo='" + sId + "'");
                    bIsOK = true;
                    optMain = OperateType.optNone;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsOK = false;
                }
            }
            bDSIsOpenForMain = true;
            return (bIsOK);
        }
        public bool OpenDtlDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            bDSIsOpenForDtl = false;
            grdDtl.AutoGenerateColumns = false;
            grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select * from V_TWB_BillMergePltDtl " + sCon;
            string err = "";
            //if (dsD.Tables["data"] != null)
            //    dsD.Tables["data"].Clear();
            dsD.Clear();
            dsD = PubDBCommFuns.GetDataBySql(sql, "", out err);
            bIsOK = err == "";
            if (!bIsOK)
                MessageBox.Show(strX);
            else
            {
                try
                {
                    this.bdsDtl.DataSource = dsD.Tables["data"];
                    //BindDtlDataSetToCtrls();
                    grdDtl.DataSource = bdsDtl;
                    bIsOK = true;
                    lbl_D_Count.Text = bdsDtl.Count.ToString();
                    optDtl = OperateType.optNone;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsOK = false;
                }
            }
            bDSIsOpenForDtl = true;
            return (bIsOK);
        }

        public void DoMNew()
        {
            optMain = OperateType.optNew;
            DataTable tbX = (DataTable)bdsMain.DataSource;
            int iX = tbX.Columns.Count;
            DataRowView drv = (DataRowView)bdsMain.AddNew();
            //初始化字段数据(默认值)
            try
            {
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            //drv.EndEdit();

            //
            DataRowViewToUI(drv, pnlEdit);
            lblChecker.Text = "审核人：";
            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
            txt_cBNo.Focus();
           
            CtrlControlReadOnly(pnlEdit, true);
            txt_cBNo.ReadOnly = true;
            //cmb_nPStatus.Enabled = false;
          
            txt_cChecker.ReadOnly = true;

        }
        public void DoMEdit()
        {

            optMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null) return;
            if (drv["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("对不起，已经审核，不能修改！");
                return;
            }
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drv["cPayer"].ToString().Trim())
            {
                MessageBox.Show("对不起，你无权限修改！");
                return;
            }
            //初始化字段数据(默认值)
            drv.BeginEdit();
            drv["dEditDate"] = DateTime.Now;
            drv["cEditor"] = UserInformation.UserName;
            drv.EndEdit();

            

            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
            txt_cBNo.Focus();

            CtrlControlReadOnly(pnlEdit, true);
            txt_cBNo.ReadOnly = true;
            //cmb_nPStatus.Enabled = false;

            txt_cChecker.ReadOnly = true;
        }
        public void DoMUndo()
        {
            optMain = OperateType.optUndo;
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv != null)
            {
                if (drv.IsEdit)
                {
                    drv.CancelEdit();
                }
                if (drv.IsNew)
                {
                    drv.Delete();
                }
                
            }
            else return;
            //DBDataSet.Tables[strTbNameMain].AcceptChanges();
            dsM.Tables["data"].AcceptChanges();
            bdsMain_PositionChanged(null, null);
            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
            optMain = OperateType.optNone;
            //DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, false);
        }
        public void DoMDelete()
        {
            string sX = "";
            int iX = -1;
            iX = (int)optMain;
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (drv.IsNew || drv.IsEdit)
            if ((0 < iX) && (iX < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (drv == null) return;
            if (drv["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("对不起，已经审核，不能修改！");
                return;
            }
            if (MessageBox.Show("系统将永久删除数据，您确定要删除此数据吗？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drv["cPayer"].ToString().Trim())
            {
                MessageBox.Show("对不起，你无权限删除！");
                return;
            }
            bool bX = false;
            /*
            string sql = "delete from TWB_BillIn where cBNo='" + drv["cBNo"].ToString() + "'";
            DataSet ds = PubDBCommFuns.GetDataBySql(sql, out sX);
            //DataMainToObjInfo(drv);
            //sX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInMain(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true);
            bX = ds.Tables[0].Rows[0][0].ToString() == "0";
            */
            string sErr = "";
            sX = PubDBCommFuns.sp_Pack_BillIODel(AppInformation.SvrSocket, drv["cBNo"].ToString(), UserInformation.UserName, UserInformation.UnitId, "WMS", out sErr);
            bX = sX == "0";
            if (bX)
            {
                optMain = OperateType.optDelete;
                
                //OpenMainDataSet(strCondition);
                //控制录入问题
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
                optMain = OperateType.optNone;
                //DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, false);
            }
            else
            {
                MessageBox.Show(sErr, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void DoMSave()
        {
            DataRowView drvMEx = null;
            string sSqlMainEx = "";
            txt_cBNo.Focus();//使其焦点移开,修改数据能及时更新
            
            if (cmb_cCreator.Text.Trim() == "" || cmb_cCreator.SelectedValue == null)
            {
                MessageBox.Show("对不起，仓管员不能为空！");
                cmb_cCreator.Focus();
                return;
            }
           

            DataRowView drvX = (DataRowView)bdsMain.Current;
            if ((optMain == OperateType.optNew) || (optMain == OperateType.optEdit))
            {
                bool bX = false;
                if (drvX.IsEdit) drvX.EndEdit();
                UIToDataRowView(drvX, pnlEdit);
                string sql = "";
                if (optMain == OperateType.optNew)
                {
                    drvX["cBNo"] = GetNewId();
                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, "cBNo", true);
                    
                }
                else
                {
                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, "cBNo", false);
                    
                }

                string err = "";
                DataSet ds = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sql, DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
                

                //if (drvX.IsEdit) drvX.EndEdit();
                //DataMainToObjInfo(drvX);
                //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInMain(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drvX, false) == "0";
                if (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "0")
                {
                    optMain = OperateType.optSave;
                    MessageBox.Show("保存主表数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //重新刷新数据                    
                    //btnQry_Click(null, null);
                    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    bdsMain_PositionChanged(null, null);
                    //控制录入问题
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
                    optMain = OperateType.optNone;
                    //DisplayState(stbState, optMain);
                    CtrlControlReadOnly(pnlEdit, false);
                }
                else
                {
                    MessageBox.Show("保存主表数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //
        public string GetNewId()
        {
            string sTbName = strTbNameMain;
            string sFldKey = "cBNo";
            string sHead = "BMP" + DateTime.Now.ToString("yyMMdd");
            int iNoLen = 12;
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_GetNewId :pTbName,:pFldKey,:pLen,:pReplaceChar,:pHeader,:pFldCon,:pValueCon";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pTbName";           //参数名称 和实际定义的一致
            par.ParameterValue = sTbName;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pFldKey";           //参数名称 和实际定义的一致
            par.ParameterValue = sFldKey;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pLen";           //参数名称 和实际定义的一致
            par.ParameterValue = iNoLen.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pReplaceChar";           //参数名称 和实际定义的一致
            par.ParameterValue = "0";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pHeader";           //参数名称 和实际定义的一致
            par.ParameterValue = sHead;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pFldCon";           //参数名称 和实际定义的一致
            par.ParameterValue = "";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pValueCon";           //参数名称 和实际定义的一致
            par.ParameterValue = "";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---


            //执行命令
            SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            string sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;
            dsX = sdcX.GetDataSet(AppInformation.SvrSocket, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            return dsX.Tables["data"].Rows[0][0].ToString();
        }
        public int GetNewItem(string billNo)
        {
            string sTbName = strTbNameDtl;
            string sFldKey = "nItem";
            //string sHead = "BI" + DateTime.Now.ToString("yyMMdd");
            //int iNoLen = 12;
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_GetDtlSeq :TbName,:PFld,:SeqFld,:PValue";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "TbName";           //参数名称 和实际定义的一致
            par.ParameterValue = sTbName;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "PFld";           //参数名称 和实际定义的一致
            par.ParameterValue = "cBNo";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "SeqFld";           //参数名称 和实际定义的一致
            par.ParameterValue = sFldKey;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "PValue";           //参数名称 和实际定义的一致
            par.ParameterValue = billNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---

            //执行命令
            SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            string sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;
            dsX = sdcX.GetDataSet(AppInformation.SvrSocket, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            tbX = dsX.Tables["data"];
            if (tbX == null)
            {
                dsX.Clear();
                MessageBox.Show(sErr);
                return -1;
            }
            if (tbX.Rows.Count == 0)
            {
                dsX.Clear();
                MessageBox.Show(" 获取明细序号无结果数据：" + sErr);
                return -1;
            }
            object objX = tbX.Rows[0][0];
            dsX.Clear();
            return int.Parse(objX.ToString());
        }


        #endregion

        private void frmMergePallet_Load(object sender, EventArgs e)
        {
            dtpFind_B.Value = DateTime.Now.AddDays(-11);
            dtpFind_E.Value = DateTime.Now;
            grdList.AutoGenerateColumns = false;
            grdDtl.AutoGenerateColumns = false;
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            if (dtpFind_B.Value > dtpFind_E.Value)
            {
                MessageBox.Show("对不起，起始时间不能大于截止时间！");
                dtpFind_B.Focus();
                return;
            }
            StringBuilder strSql = new StringBuilder("select * from V_TWB_BillMergePlt where (dCreateDate >='" + dtpFind_B.Value.ToString("yyyy-MM-dd 00:00:00") + "' and dCreateDate <='" + dtpFind_E.Value.ToString("yyyy-MM-dd 23:59:59") + "')");
            #region 条件 
            if (cmbFindUser.Text.Trim() != "" && cmbFindUser.Text.Trim() != "全部" && cmbFindUser.SelectedValue != null)
            {
                strSql.Append(" and cCreator='" + cmbFindUser.SelectedValue.ToString().Trim() + "'");
            }
            if (txtFindBillNo.Text.Trim() != "")
            {
                strSql.Append(" and cBNo like '%" + txtFindBillNo.Text.Trim() + "%'");
            }
            if (this.cmbFindCheck.Text.Trim() != "" && cmbFindCheck.Text.Trim() != "全部" && cmbFindCheck.SelectedIndex >= 0 && cmbFindCheck.SelectedIndex <= 1)
            {
                strSql.Append(" and isnull(bIsChecked,0)='" + cmbFindCheck.SelectedIndex.ToString().Trim() + "'");
            }
            if (this.cmb_FinishedStatus.Text.Trim() != "" && cmb_FinishedStatus.Text.Trim() != "全部" && cmb_FinishedStatus.SelectedIndex >= 0 && cmb_FinishedStatus.SelectedIndex <= 1)
            {
                strSql.Append(" and isnull(bIsFinished,0)='" + cmb_FinishedStatus.SelectedIndex.ToString().Trim() + "'");
            }
            #endregion

            DataSet dsX = null;
            string sErr = "";
            Cursor.Current = Cursors.WaitCursor;
            dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql.ToString(), strTbNameMain, 0, 0, "dCreateDate,dCheckDate", out sErr);
            Cursor.Current = Cursors.Default;
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (dsX == null)
            {
                MessageBox.Show("获取数据时失败！");
                return;
            }
            DataTable tbData = null;
            if (dsX.Tables.Count > 0)
            {
                if (dsX.Tables[0].Rows[0][0].ToString() == "-1")
                {
                    MessageBox.Show(dsX.Tables[0].Rows[0][1].ToString());
                    return;
                }
                if (dsX.Tables[strTbNameMain] != null)
                {
                    tbData = dsX.Tables[strTbNameMain].Copy();
                }
                if (tbData == null)
                {
                    MessageBox.Show("获取数据时失败！");
                    return;
                }
            }
            Cursor.Current = Cursors.WaitCursor;
            bDSIsOpenForMain = false;
            bdsMain.DataSource = tbData;
            grdList.DataSource = bdsMain;
            bDSIsOpenForMain = true;
            lbl_M_Count.Text = bdsMain.Count.ToString();
            Cursor.Current = Cursors.Default;
            bdsMain_PositionChanged(null, null);
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (!bDSIsOpenForMain) return;
            string sBNo="";
            ClearUIValues(pnlEdit);
            DataRowView drvX = null;
            drvX = (DataRowView)bdsMain.Current;
            if (drvX != null)
            {
                if (!drvX.IsNew)
                {
                    DataRowViewToUI(drvX, pnlEdit);
                }
                sBNo = drvX["cBNo"].ToString();
            }
            OpenDtlDataSet(" where cBNo='"+ sBNo +"'");
           
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            frmSelPosFromAndTo frmX = new frmSelPosFromAndTo();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.ShowDialog();
            frmX.Dispose();
            frmX = null;
            btnQry_Click(null, null);
            
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            if (bdsMain.Count == 0)
            {
                MessageBox.Show("对不起，无数据可删除！");
                return;
            }
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null) return;
            string sBNo= drvX["cBNo"].ToString();
            if (drvX["bIsChecked"].ToString() == "1")
            {
                MessageBox.Show("对不起，改单已经审核，不能删除！");
                return;
            }
            if (MessageBox.Show("您确定要删除该单号：" +  sBNo + "  吗 ？","询问",MessageBoxButtons.YesNo ,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.No) return;
            string sErr = "";
            if (!DBFuns.DoExecSql(AppInformation.SvrSocket, "delete TWB_BillMergePltDtl where cBNo='" + sBNo + "'", "", out sErr))
            {
                MessageBox.Show("删除单据明细数据失败：" + sErr);
                return;
            }
            if (!DBFuns.DoExecSql(AppInformation.SvrSocket, "delete TWB_BillMergePlt where cBNo='" + sBNo + "'", "", out sErr))
            {
                MessageBox.Show("删除单据数据失败：" + sErr);
                return;
            }
            MessageBox.Show("删除单据数据成功！");
            btnQry_Click(null, null);
        }

        private void tlb_M_Check_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count > 0)
            {

                foreach (DataGridViewRow grdr in grdList.SelectedRows)
                {
                    #region
                    if (UserInformation.UType == UserType.utNormal)
                    {
                        string sUser = grdr.Cells["cCreator"].Value.ToString().Trim();
                        if (sUser != UserInformation.UserName.Trim())
                        {
                            MessageBox.Show("对不起，你无权限审核或取消审核");
                            continue;
                        }
                    }
                    #endregion

                    #region
                    string sBNo = "";
                    sBNo = grdr.Cells["colcBId"].Value.ToString();
                    int nBClass = 0;
                    nBClass = Convert.ToInt32(grdr.Cells["col_Main_nBClass"].Value);
                    if (sBNo.Trim() != "")
                    {
                        if (grdr.Cells["col_Main_bIsChecked"].Value.ToString().ToLower() == "1")
                        {
                            continue;
                        }
                        string sErr = "";
                        string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, nBClass, sBNo, 0, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                        if (sX.Trim() != "0")
                        {
                            MessageBox.Show(sErr);
                            continue;
                        }
                    }
                    #endregion
                }
            }
        }

        private void tlb_M_UnCheck_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count > 0)
            {
                #region
                foreach (DataGridViewRow grdr in grdList.SelectedRows)
                {
                    #region
                    if (UserInformation.UType == UserType.utNormal)
                    {
                        string sUser = grdr.Cells["cCreator"].Value.ToString().Trim();
                        if (sUser != UserInformation.UserName.Trim())
                        {
                            MessageBox.Show("对不起，你无权限审核或取消审核");
                            continue;
                        }
                    }
                    #endregion
                    string sBNo = "";
                    sBNo = grdr.Cells["colcBId"].Value.ToString();
                    int nBClass = 0;
                    nBClass = Convert.ToInt32(grdr.Cells["col_Main_nBClass"].Value);
                    if (sBNo.Trim() != "")
                    {
                        if (grdr.Cells["col_Main_bIsChecked"].Value.ToString().ToLower() == "0")
                        {
                            continue;
                        }
                        string sErr = "";
                        string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, nBClass, sBNo, 1, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                        if (sX.Trim() != "0")
                        {
                            MessageBox.Show(sErr);
                            continue;
                        }
                    }
                }
                #endregion
            }
           
        }

    }
}

