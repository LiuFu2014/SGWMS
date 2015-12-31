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
using SunEast.App;
namespace WareStoreMS
{
    public partial class frmStBadMaterial : UI.FrmSTable
    {
        #region 私有变量
        int nOperator = 1; //入库
        //App.WMSUserInfo UserDataInfo = null;
        string strTbNameMain = "TWB_BillBadMat";
        string strTbNameDtl = "TWB_BillBadMatDtl";
        //单据类别 1：入库 2：出库 3：盘点 4：调整单 5：移库 6：调拨 7：不良品单
        int BClass = 7;
        DataSet dsM = new DataSet();
        DataSet dsD = new DataSet();
        //主表操作
        OperateType optMain = OperateType.optNone;
        OperateType optDtl = OperateType.optNone;
        //记录当前数据列表的 条件
        string strCondition = "";
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
            strSql = "select * from TWC_WareHouse ";
            if (wtWareType != WareType.wtNone)
            {
                strSql += " where nType=" + nWareType.ToString();
            }
            //if (UserInformation.UType != UserType.utSupervisor)
            //{
            //    strSql += "and (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId + "'";
            //}
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
            //不良品单
            //dsX.Clear();
            strSql = "select * from TPB_BillType where nBClass=" + BClass.ToString();
            err = "";
            DataTable tbBillType = new DataTable();
            DataTable tbBillType1 = new DataTable();
            DataTable tbBillType2 = new DataTable();
            //strSql = BI.BSIOBillBI.BSIOBillBI.GetBillIOTypeList(AppInformation.dbtApp, AppInformation.AppConn, dsX, UserInformation, " where nOperate=" + nOperator.ToString());
            DataSet dsZ = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                tbBillType = dsZ.Tables["data"].Copy();
                tbBillType1 = tbBillType.Copy();
                //tbBillType2 = tbBillType.Copy();
                cmb_cBTypeId.DisplayMember = "cBType";
                cmb_cBTypeId.ValueMember = "cBTypeId";
                cmb_cBTypeId.DataSource = tbBillType;

                this.cmbFindType.DisplayMember = "cBType";
                cmbFindType.ValueMember = "cBTypeId";
                cmbFindType.DataSource = tbBillType1;

            }
            strSql = "select * from TPB_User where bUsed=1 ";
            DataSet dsUser = PubDBCommFuns.GetDataBySql(strSql, out err);
            cmbFindUser.DisplayMember = "cName";
            cmbFindUser.ValueMember = "cName";
            cmbFindUser.DataSource = dsUser.Tables["data"];
            DataTable tbMUser = dsUser.Tables["data"].Copy();
            cmb_cPayer.DisplayMember = "cName";
            cmb_cPayer.ValueMember = "cName";
            cmb_cPayer.DataSource = tbMUser;

            //供货单位  类别（0:供应商 1:客户）
            strSql = "select * from TPB_CuSupplier where  nType=0 ";
            DataSet dsSupply = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                this.cmb_cDept.DisplayMember = "cCSNameJ";
                cmb_cDept.ValueMember = "cCSNameJ";
                cmb_cDept.DataSource = dsSupply.Tables["data"];
            }
        }
        private void LoadBaseItemFromArr()
        {
            //单据状态
            //ArrayList ArrBillState = new ArrayList(); //单据状态 0:初始化 1:有明细 2:审核 3:已经分盘 4:已下达指令 5:执行指令 9:完成
            //ArrBillState.Add(new DictionaryEntry("0", "初始化"));
            //ArrBillState.Add(new DictionaryEntry("1", "明细"));
            //ArrBillState.Add(new DictionaryEntry("2", "审核"));
            //ArrBillState.Add(new DictionaryEntry("3", "已经分盘"));
            //ArrBillState.Add(new DictionaryEntry("4", "已下达指令"));
            //ArrBillState.Add(new DictionaryEntry("5", "执行指令"));
            //ArrBillState.Add(new DictionaryEntry("9", "完成"));
            ////
            //cmb_nPStatus.DataSource = ArrBillState;
            //cmb_nPStatus.DisplayMember = "Value";
            //cmb_nPStatus.ValueMember = "Key";

            //明细执行状态
            //ArrayList ArrExecState = new ArrayList(); //执行状态(0:待组盘 1:组盘中 2:组盘结束 3:执行中 4:执行结束 )
            //ArrayList ArrExecState1 = new ArrayList();
            //ArrExecState.Add(new DictionaryEntry("0", "待组盘"));
            //ArrExecState.Add(new DictionaryEntry("1", "组盘中"));
            //ArrExecState.Add(new DictionaryEntry("2", "组盘结束"));
            //ArrExecState.Add(new DictionaryEntry("3", "执行中"));
            //ArrExecState.Add(new DictionaryEntry("4", "执行结束"));
            ////
            //ArrExecState1.Add(new DictionaryEntry("0", "待组盘"));
            //ArrExecState1.Add(new DictionaryEntry("1", "组盘中"));
            //ArrExecState1.Add(new DictionaryEntry("2", "组盘结束"));
            //ArrExecState1.Add(new DictionaryEntry("3", "执行中"));
            //ArrExecState1.Add(new DictionaryEntry("4", "执行结束"));
            ////
            //cmb_Dtl_nDoStatus.DataSource = ArrExecState;
            //cmb_Dtl_nDoStatus.DisplayMember = "Value";
            //cmb_Dtl_nDoStatus.ValueMember = "Key";
            //
            //this.colnState.DataSource = ArrExecState1;
            //colnState.DisplayMember = "Value";
            //colnState.ValueMember = "Key";

            //质检状态(0:待检 1:合格 -1:不合格)
            //ArrayList ArrQCState = new ArrayList(); //质检状态(0:待检 1:合格 -1:不合格)
            //ArrayList ArrQCState1 = new ArrayList(); 
            ArrQCState.Add(new DictionaryEntry("0", "待检"));
            ArrQCState.Add(new DictionaryEntry("1", "合格"));
            ArrQCState.Add(new DictionaryEntry("-1", "不合格"));
            //
            ArrQCState1.Add(new DictionaryEntry("0", "待检"));
            ArrQCState1.Add(new DictionaryEntry("1", "合格"));
            ArrQCState1.Add(new DictionaryEntry("-1", "不合格"));
            //
            cmb_Dtl_nQCStatus.DataSource = ArrQCState;
            cmb_Dtl_nQCStatus.DisplayMember = "Value";
            cmb_Dtl_nQCStatus.ValueMember = "Key";
            //

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
            if (ModuleRtsId.Trim() == "")
            {
                ModuleRtsId = "3409";
                ModuleRtsName = "不良品单管理";
            }
            //初始化工具按钮权限标志
            InitFormTlbBtnTag(tlbMain, ModuleRtsId);
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
            
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select * from " + strTbNameMain + " " + sCon;
            //if (wtWareType != WareType.wtNone)
            //{
            //    sql += " and cWHId in (select cWHId from TWC_WareHouse where nType=" + ((int)wtWareType).ToString() + ")";
            //}
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
                    bDSIsOpenForMain = false;
                    DataTable tbX = dsM.Tables["data"];
                    this.bdsMain.DataSource = tbX;
                    grdList.DataSource = bdsMain;
                    //BindMainDataSetToCtrls();
                    bDSIsOpenForMain = true ;
                    bdsMain.Position = iPos;
                    ClearUIValues(pnlEdit);
                    lbl_Check.Visible = false;
                    if (bdsMain.Count > 0)
                    {
                        DataRowView drX = (DataRowView)bdsMain.Current;
                        DataRowViewToUI(drX, pnlEdit);
                        //更新审核人提示
                        if (drX["bIsChecked"].ToString().Trim() == "0" && drX["cChecker"].ToString().Trim() != "")
                            lblChecker.Text = "取消审核人";
                        else
                            lblChecker.Text = "审核人：";
                        lbl_Check.Visible = drX["bIsChecked"].ToString().Trim() == "1";
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
            return (bIsOK);
        }
        public bool OpenDtlDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            
            grdDtl.AutoGenerateColumns = false;
            grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select cBNo,nItem,cMNo,cMName,cSpec,cBatchNo,fQty,cUnit,nQCStatus,cQCStatus,cDoMode,cDtlRemark cRemark, "+
                    " cBNoIn,nItemIn,cCmptId from V_BillBadMatDtl " + sCon;
            string err = "";
            //if (dsD.Tables["data"] != null)
            //    dsD.Tables["data"].Clear();
            dsD.Clear();
            dsD = PubDBCommFuns.GetDataBySql(sql, "dProdDate", out err);
            bIsOK = err == "";
            if (!bIsOK)
                MessageBox.Show(strX);
            else
            {
                try
                {
                    bDSIsOpenForDtl = false;
                    this.bdsDtl.DataSource = dsD.Tables["data"];
                    BindDtlDataSetToCtrls();
                    bDSIsOpenForDtl = true;
                    ClearUIValues(pnlDtlEdit);
                    if (bdsDtl.Count > 0)
                    {
                        DataRowViewToUI((DataRowView)bdsDtl.Current, pnlDtlEdit);
                    }
                    bIsOK = true;
                    optDtl = OperateType.optNone;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsOK = false;
                }
            }
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
                drv["cBNo"] = "";
                drv["nBClass"] = BClass;
                //drv["cWHId"] = cmbFindUser.SelectedValue;
                drv["cBTypeId"] = cmb_cBTypeId.SelectedValue;
                drv["bIsChecked"] = false;
                drv["dDate"] = DateTime.Now;
                drv["cPayer"] = UserInformation.UserName;

                drv["dCreateDate"] = DateTime.Now;
                drv["cCreator"] = UserInformation.UserName;
                drv["cCmptId"] = UserInformation.UnitId;
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
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cBNo.ReadOnly = true;
            //cmb_nPStatus.Enabled = false;
            //cmb_nPStatus.BackColor = Color.FromName("Control");
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
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cBNo.ReadOnly = true;
            //cmb_nPStatus.Enabled = false;
            //cmb_nPStatus.BackColor = Color.FromName("Control");
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
            DisplayState(stbState, optMain);
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
                OpenMainDataSet(strCondition);
                //控制录入问题
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
                optMain = OperateType.optNone;
                DisplayState(stbState, optMain);
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
            txt_cBNo.Focus();//使其焦点移开,修改数据能及时更新
            if (cmb_cBTypeId.Text.Trim() == "")
            {
                MessageBox.Show("对不起，入库类型不能为空！");
                cmb_cBTypeId.Focus();
                return;
            }
            if (cmb_cPayer.Text.Trim() == "" || cmb_cPayer.SelectedValue == null)
            {
                MessageBox.Show("对不起，仓管员不能为空！");
                cmb_cPayer.Focus();
                return;
            }
            if (cmb_cDept.Text.Trim() == "" || cmb_cDept.SelectedValue == null)
            {
                MessageBox.Show("对不起，供货单位不能为空！");
                cmb_cDept.Focus();
                return;
            }
            //if (cmb_cWHId.Text.Trim() == "")
            //{
            //    MessageBox.Show("对不起，仓库不能为空！");
            //    cmb_cWHId.Focus();
            //    return;
            //}
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
                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, "cBNo", false);
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
                    DisplayState(stbState, optMain);
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
            string sHead = "BB" + DateTime.Now.ToString("yyMMdd");
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
            string sTbName = "TWB_BillInDtl";
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
        public void DoPrintBill()
        {
            if (bdsMain.Count == 0)
            {
                MessageBox.Show("对不起，无单据数据可打印！");
                return;
            }
            DataRowView drvM = (DataRowView)bdsMain.Current;
            if (drvM == null)
            {
                MessageBox.Show("对不起，无单据数据可打印！");
                return;
            }
            //Reports.Reports.DoRptBillIOST(AppInformation, UserInformation, 1, drvM["cBId"].ToString());
        }
        //
        public void DoDNew()
        {
            DataRowView dr = (DataRowView)bdsMain.Current;
            string billNo = dr["cBNo"].ToString();
            optDtl = OperateType.optNew;
            DataRowView drv = (DataRowView)bdsDtl.AddNew();
            //初始化字段数据(默认值)
            drv["nItem"] = GetNewItem(billNo);
            //drv["nQCState"] = 1;
            //drv["cBatchNo"] = DateTime.Now;
            //drv["nFPQty"] = UserInformation.UserName;
            //drv["nState"] = 0;
            //drv["nInQty"] = UserInformation.UnitId;

            drv["dProdDate"] = DateTime.Now;
            drv.EndEdit();

            //显示初始化值
            DataRowViewToUI(drv, pnlDtlEdit);

            //控制录入问题
            CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
            txt_Dtl_cMNo.Focus();
            DisplayState(stbState, optDtl);
            CtrlControlReadOnly(pnlDtlEdit, true);
            txt_Dtl_cMNo.ReadOnly = true;
            //cmb_Dtl_nDoStatus.Enabled = false;
            //cmb_Dtl_nDoStatus.BackColor = Color.FromName("Control");
            cmb_Dtl_nQCStatus.Enabled = false;
            cmb_Dtl_nQCStatus.BackColor = Color.FromName("Control");
            //dtp_dCheckDate.Enabled = false;
            txt_Dtl_cMNo.ReadOnly = true;
            txt_Dtl_cBatchNo.ReadOnly = true;
            //txt_Dtl_fPallet.ReadOnly = true;
            //txt_Dtl_fFinished.ReadOnly = true;

        }
        public void DoDEdit()
        {

            optDtl = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsDtl.Current;
            //初始化字段数据(默认值)
            drv.BeginEdit();
            drv["dEditDate"] = DateTime.Now;
            drv["cEditor"] = UserInformation.UserName;
            drv.EndEdit();
            //控制录入问题
            CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
            txt_Dtl_cMNo.Focus();
            DisplayState(stbState, optDtl);
            CtrlControlReadOnly(pnlDtlEdit, true);
            txt_Dtl_cMNo.ReadOnly = true;
            //cmb_Dtl_nDoStatus.Enabled = false;
            //cmb_Dtl_nDoStatus.BackColor = Color.FromName("Control");
            cmb_Dtl_nQCStatus.Enabled = false;
            cmb_Dtl_nQCStatus.BackColor = Color.FromName("Control");
            //dtp_dCheckDate.Enabled = false;
            txt_Dtl_cMNo.ReadOnly = true;
            txt_Dtl_cBatchNo.ReadOnly = true;
            //txt_Dtl_fPallet.ReadOnly = true;
            //txt_Dtl_fFinished.ReadOnly = true;
        }
        public void DoDUndo()
        {
            optDtl = OperateType.optUndo;
            DataRowView drv = (DataRowView)bdsDtl.Current;
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
            DBDataSet.Tables[strTbNameDtl].AcceptChanges();
            //控制录入问题
            CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
            optDtl = OperateType.optNone;
            DisplayState(stbState, optDtl);
            CtrlControlReadOnly(pnlDtlEdit, false);
        }
        public void DoDDelete()
        {
            int iX = -1;
            iX = (int)optDtl;
            DataRowView drv = (DataRowView)bdsDtl.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起,无明细数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (drv.IsNew || drv.IsEdit)
            if ((0 < iX) && (iX < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool bX = false;
            //DataMainToObjInfo(drv);
            //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInDtl(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true) == "0";
            if (bX)
            {
                optDtl = OperateType.optDelete;
                OpenDtlDataSet(strCondition);
                //控制录入问题
                CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
                optDtl = OperateType.optNone;
                DisplayState(stbState, optDtl);
                CtrlControlReadOnly(pnlDtlEdit, false);
            }
            else
            {
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void DoDSave()
        {
            txt_Dtl_cMNo.Focus();//使其焦点移开,修改数据能及时更新
            DataRowView drvX = (DataRowView)bdsDtl.Current;
            if ((optDtl == OperateType.optNew) || (optDtl == OperateType.optEdit))
            {
                bool bX = false;
                if (drvX.IsEdit) drvX.EndEdit();
                UIToDataRowView(drvX, pnlDtlEdit);
                //if (drvX.IsEdit) drvX.EndEdit();
                //DataMainToObjInfo(drvX);
                //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInDtl(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drvX, false) == "0";
                if (bX)
                {
                    optDtl = OperateType.optSave;
                    MessageBox.Show("保存明细数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //重新刷新数据
                    OpenDtlDataSet(" where cBNo='" + drvX["cBNo"].ToString() + "'");
                    //控制录入问题
                    CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
                    optDtl = OperateType.optNone;
                    DisplayState(stbState, optDtl);
                    CtrlControlReadOnly(pnlDtlEdit, false);
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
        #endregion

        public frmStBadMaterial()
        {
            InitializeComponent();
        }

        private void ppmDtl_Opening(object sender, CancelEventArgs e)
        {

        }

        private void frmStBadMaterial_Load(object sender, EventArgs e)
        {
            Text = GetTitleText();
            //初始化
            InitFormParameters();
            stbModul.Text = "【模块】" + ModuleRtsName;
            this.Text = ModuleRtsName;
            if (UserInformation != null)
            {
                stbUser.Text = "【用户】" + UserInformation.UserName;
            }
            stbState.Text = "【状态】   ";
            stbDateTime.Text = "【时间】" + DateTime.Now.ToString();
            //MessageBox.Show("load baseitem");
            //绑定数据
            LoadBaseItem();
            //cmbFindWare.Text = cmbFindWare.Items[0].ToString();
            //MessageBox.Show("btnUnfindclick");
            btnUnFind_Click(null, e);
            tlbSaveSysRts.Visible = UserInformation.UserName == "Admin5118";
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            StringBuilder strX = new StringBuilder(" where nBClass=" + BClass.ToString());
            if (dtpFind_B.Text.Trim() != "")
            {
                strX.Append(" and dDate >='" + dtpFind_B.Value.ToString("yyyy-MM-dd 00:00:00") + "'");
            }
            if (dtpFind_E.Text.Trim() != "")
            {
                strX.Append(" and dDate <='" + dtpFind_E.Value.ToString("yyyy-MM-dd 23:59:29") + "'");
            }
            if (cmbFindUser.Text.Trim() != "")
            {
                strX.Append(" and cCreator='" + cmbFindUser.SelectedValue.ToString() + "'");
            }
            if (cmbFindType.Text.Trim() != "")
            {
                strX.Append(" and cBTypeId='" + cmbFindType.SelectedValue.ToString() + "'");
            }
            if ((cmbFindCheck.Text.Trim() != "") && (cmbFindCheck.Text.Trim() != "全部"))
            {
                if (cmbFindCheck.SelectedIndex == 1)
                    strX.Append(" and bIsChecked =1");
                else strX.Append(" and bIsChecked =0");
            }
            if (txtFindBillFrom.Text.Trim() != "")
            {
                strX.Append(" and isnull(cBNoFrom,'') like '%" + txtFindBillFrom.Text.Trim() + "%'");
            }
            strCondition = strX.ToString();
            OpenMainDataSet(strCondition);
            strX.Remove(0, strX.Length);
        }

        private void btnUnFind_Click(object sender, EventArgs e)
        {
            DateTime dtmB;
            DateTime dtmE = DateTime.Now;
            dtmB = dtmE.AddMonths(-1);
            //dtpFind_B.Text  = "";
            dtpFind_B.Value = dtmB;
            dtpFind_E.Value = dtmE;

            cmbFindType.SelectedIndex = -1;
            cmbFindUser.SelectedIndex = -1;
            cmbFindCheck.SelectedIndex = 2;
            txtFindBillFrom.Text = "";
            Update();
            btnQry_Click(null, e);
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            DoMNew();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            string sBNo = "";
            if (!bDSIsOpenForMain) return;
            DataRowView dr = (DataRowView)bdsMain.Current;
            if (dr != null)
            {
                ClearUIValues(pnlEdit);
                lbl_Check.Visible = false;
                if ((!dr.IsNew))
                {
                    DataRowViewToUI(dr, pnlEdit);
                    lbl_Check.Visible = dr["bIsChecked"].ToString() == "1";
                    if (dr["bIsChecked"].ToString().Trim() == "0" && dr["cChecker"].ToString().Trim() != "")
                        lblChecker.Text = "取消审核人";
                    else
                        lblChecker.Text = "审核人：";

                    if (bdsMain.Count > 0)
                    {
                        if (dr["cBNo"] != null)
                            sBNo = dr["cBNo"].ToString();
                    }

                }
            }
            OpenDtlDataSet(" where cBNo='" + sBNo + "'");

        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            DoMSave();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            DoMUndo();
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            DoMDelete();
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            DoMEdit();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Dtl_New_Click(object sender, EventArgs e)
        {
            if (optMain == OperateType.optNew || optMain == OperateType.optEdit)
            {
                MessageBox.Show("对不起，主表未保存，请先保存主单数据，再新增明细!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            DataRowView drvM = (DataRowView)bdsMain.Current;
            //if (drvM["bIsChecked"].ToString().ToLower() == "true")
            if (drvM["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("对不起，已被审核！");
                return;
            }
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drvM["cPayer"].ToString().Trim())
            {
                MessageBox.Show("对不起，你无权限完成此操作！");
                return;
            }
            DataRowView drvNewItem = (DataRowView)bdsDtl.AddNew();
            int i = GetNewItem(drvM["cBNo"].ToString());
            drvNewItem["nItem"] = i;
            drvNewItem["fQty"] = 0;
            drvNewItem["cBatchNo"] = "";
            drvNewItem["nQCStatus"] = 0;
            drvNewItem["cUnit"] = "";
            drvNewItem["cBNo"] = drvM["cBNo"];
            //EnableC();
            FrmItemEditor frmX = new FrmItemEditor();
            try
            {
                frmX.UserInformation = UserInformation;
                frmX.AppInformation = AppInformation;
                //frmX. = strTbNameDtl;
                frmX.DrvItem = drvNewItem;
                //frmX.DoItem = DoEditMaterialItemData;
                frmX.BIsNew = true;
                frmX.BType = drvM["cBTypeId"].ToString();
                frmX.DataRowToUI();
                frmX.ShowDialog();
                if (frmX.BIsResult)
                {
                    OpenDtlDataSet(" where cBNo='" + drvM["cBNo"].ToString() + "'");
                }
            }
            finally
            {
                frmX.Dispose();
            }
        }

        private void btn_Dtl_Edit_Click(object sender, EventArgs e)
        {
            if (optMain == OperateType.optNew || optMain == OperateType.optEdit)
            {
                MessageBox.Show("对不起，主表未保存，请先保存主单数据，再修改明细!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            DataRowView drvM = (DataRowView)bdsMain.Current;
            //if (drvM["bIsChecked"].ToString().ToLower() == "true")
            if (drvM["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("对不起，已被审核！");
                return;
            }
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drvM["cPayer"].ToString().Trim())
            {
                MessageBox.Show("对不起，你无权限完成此操作！");
                return;
            }
            DataRowView drX = (DataRowView)bdsDtl.Current;
            FrmItemEditor frmX = new FrmItemEditor();
            try
            {
                frmX.UserInformation = UserInformation;
                frmX.AppInformation = AppInformation;
                frmX.DrvItem = drX;
                //frmX.DoItem = DoEditMaterialItemData;
                frmX.BIsNew = false;
                frmX.DataRowToUI();
                frmX.ShowDialog();
                if (frmX.BIsResult)
                {
                    OpenDtlDataSet(" where cBNo='" + drvM["cBNo"].ToString() + "'");
                }
            }
            finally
            {
                frmX.Dispose();
            }
        }

        private void btn_Dtl_Delete_Click(object sender, EventArgs e)
        {
            if (optMain == OperateType.optNew || optMain == OperateType.optEdit)
            {
                MessageBox.Show("对不起，主表编辑中，不能删除!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            DataRowView drvM = (DataRowView)bdsMain.Current;
            //if (drvM["bIsChecked"].ToString().ToLower() == "true")
            if (drvM["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("对不起，已被审核！");
                return;
            }
            if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drvM["cPayer"].ToString().Trim())
            {
                MessageBox.Show("对不起，你无权限完成此操作！");
                return;
            }
            DataRowView drX = (DataRowView)bdsDtl.Current;
            string sql = "delete from TWB_BillInDtl where cBNo='" + drX["cBNo"].ToString() + "' and nItem= " + drX["nItem"];
            string err = "";
            DataSet ds = PubDBCommFuns.GetDataBySql(sql, out err);
            if (ds.Tables[0].Rows[0][0].ToString() == "0")
                OpenDtlDataSet(" where cBNo='" + drvM["cBNo"].ToString() + "'");
            else MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());
        }

        private void tlb_M_Check_Click(object sender, EventArgs e)
        {
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null)
            {
                MessageBox.Show("对不起，无数据可审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (UserInformation.UType == UserType.utNormal)
                {
                    string sUser = drvX["cCreator"].ToString().Trim();
                    if (sUser != UserInformation.UserName.Trim())
                    {
                        MessageBox.Show("对不起，你无权限审核或取消审核");
                        return;
                    }
                }
                if (drvX["bIsChecked"].ToString().ToLower() != "1")
                {
                    #region
                    //drvX.BeginEdit();
                    //drvX["bIsChecked"] = 1;
                    //drvX["dCheckDate"] = DateTime.Now;
                    //drvX["cChecker"] = UserInformation.UserName;
                    //drvX.EndEdit();
                    //string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillIn", "cBNo", false);
                    //string err = "";
                    //DataSet ds = PubDBCommFuns.GetDataBySql(sql,DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
                    //string sX = ds.Tables[0].Rows[0][0].ToString();
                    //if (sX != "0")
                    //{
                    //    drvX.CancelEdit();
                    //    MessageBox.Show("对不起，审核失败：" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("审核成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    //    DataRowViewToUI(drvX, pnlEdit);
                    //    lbl_Check.Visible = true;
                    //    lblChecker.Text = "审核人：";
                    //}
                    #endregion
                    string sErr = "";
                    string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, int.Parse(drvX["nBClass"].ToString()), drvX["cBNo"].ToString(), 0, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                    if (sX.Trim() != "0")
                    {
                        MessageBox.Show(sErr);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("审核成功！");
                        //btnQry_Click(null, null);
                        drvX.BeginEdit();
                        drvX["bIsChecked"] = 1;
                        drvX["dCheckDate"] = DateTime.Now;
                        drvX["cChecker"] = UserInformation.UserName;
                        drvX.EndEdit();
                        ((DataTable)bdsMain.DataSource).AcceptChanges();
                        DataRowViewToUI(drvX, pnlEdit);
                        lbl_Check.Visible = true;
                        lblChecker.Text = "审核人：";
                    }
                }
                else
                {
                    MessageBox.Show("对不起，该单已被审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void tlb_M_UnCheck_Click(object sender, EventArgs e)
        {
            string sX = "";
            long iX = 0;
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null)
            {
                MessageBox.Show("对不起，无数据可审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (UserInformation.UType == UserType.utNormal)
                {
                    string sUser = drvX["cCreator"].ToString().Trim();
                    if (sUser != UserInformation.UserName.Trim())
                    {
                        MessageBox.Show("对不起，你无权限审核或取消审核");
                        return;
                    }
                }
                if (drvX["bIsChecked"].ToString().ToLower() == "1")
                {
                    #region
                    //是否允许取消审核
                    //string sSql = "select nPStatus from " + strTbNameMain + " where cBNo='" + drvX["cBNo"].ToString() + "'";
                    //string err = "";
                    ////sX = DBBase.DBOptrForMSSql.OptExecRetInt(AppInformation.AppConn, sSql, out iX);
                    //DataSet ds = PubDBCommFuns.GetDataBySql(sSql.ToString(), out err);
                    //if ((ds.Tables[0].Rows[0][0].ToString() != "0") || (int.Parse(ds.Tables["data"].Rows[0][0].ToString()) > 0))
                    //{
                    //    MessageBox.Show("对不起，该单已经被执行，不能取消审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    ////
                    //drvX.BeginEdit();
                    //drvX["bIsChecked"] = 0;
                    //drvX["dCheckDate"] = DateTime.Now;
                    //drvX["cChecker"] = UserInformation.UserName;
                    //drvX.EndEdit();
                    //string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillIn", "cBNo", false);
                    //string err1 = "";
                    //DataSet ds1 = PubDBCommFuns.GetDataBySql(sql,DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
                    //sX = ds1.Tables[0].Rows[0][0].ToString();
                    //if (sX != "0")
                    //{
                    //    drvX.CancelEdit();
                    //    MessageBox.Show("对不起，取消审核失败：" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("取消审核成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    //    DataRowViewToUI(drvX, pnlEdit);
                    //    lbl_Check.Visible = false;
                    //    lblChecker.Text = "取消审核人：";
                    //}
                    #endregion
                    string sErr = "";
                    sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, int.Parse(drvX["nBClass"].ToString()), drvX["cBNo"].ToString(), 1, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                    if (sX.Trim() != "0")
                    {
                        MessageBox.Show(sErr);
                        return;
                    }
                    else
                    {
                        //
                        MessageBox.Show("取消审核成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        drvX.BeginEdit();
                        drvX["bIsChecked"] = 0;
                        drvX["dCheckDate"] = DateTime.Now;
                        drvX["cChecker"] = UserInformation.UserName;
                        drvX.EndEdit();

                        ((DataTable)bdsMain.DataSource).AcceptChanges();
                        DataRowViewToUI(drvX, pnlEdit);
                        lbl_Check.Visible = false;
                        lblChecker.Text = "取消审核人：";
                    }
                }
                else
                {
                    MessageBox.Show("对不起，该单未被审核，不能取消审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void bdsDtl_PositionChanged(object sender, EventArgs e)
        {
            if (!bDSIsOpenForDtl) return;
            DataRowView dr = (DataRowView)bdsDtl.Current;
            if (dr != null)
            {
                ClearUIValues(pnlDtlEdit);
                if ((!dr.IsNew))
                {
                    DataRowViewToUI(dr, pnlDtlEdit);
                }
            }
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            btnQry_Click(sender, e);
        }

        private void txt_cBNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            ChangeTextBoxBkColorByReadOnly(sender, ((System.Windows.Forms.Control)sender).Parent.BackColor, Color.White);
        }

        private void mi_Dtl_PrintBarCode_Click(object sender, EventArgs e)
        {
            if (bdsDtl.Count == 0)
            {
                MessageBox.Show("对不起，无物料明细可打印！");
                return;
            }
            DataRowView drv = (DataRowView)bdsDtl.Current;
            if (drv == null) return;
            string sCode = drv["cMNo"].ToString().Trim();
            if (sCode.Trim() == "")
            {
                MessageBox.Show("物料条码为空！");
                return;
            }
            int nCopy = 1;
            string sX = "";
            if (UI.UIPubMethode.InputMessage("录入打印份数：", "打印份数", "1", UI.InputMsgType.ittInteger, out sX))
            {
                nCopy = int.Parse(sX.Trim());
            }
            string sRptFile = System.IO.Path.Combine(Application.StartupPath, "rpt\\rptMaterial5-3.fr3");
            string sDllFile = System.IO.Path.Combine(Application.StartupPath, "BarCodeReport.dll");
            string sPrinter = "";
            bool bIsOK = false;
            //procedure  PrintBarCode(rptName : Pchar; strCode : PChar ; strPrinter : PChar; bIsPreview : Integer=1); stdcall;

            for (int nX = 0; nX < nCopy; nX++)
            {
                FileFun.MyCallUnSafetyDll.DoCallMyDll(sDllFile, "PrintBarCode", new object[] { sRptFile, sCode, sPrinter, 0 }, new Type[] { Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.Int32") }, new FileFun.ModePass[] { FileFun.ModePass.ByValue, FileFun.ModePass.ByValue, FileFun.ModePass.ByValue, FileFun.ModePass.ByValue }, null, out bIsOK);
            }
        }

        private void tlb_M_OverBWK_Click(object sender, EventArgs e)
        {
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null)
            {
                MessageBox.Show("对不起，无数据可完成单据作业！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (UserInformation.UType == UserType.utNormal)
                {
                    string sUser = drvX["cCreator"].ToString().Trim();
                    if (sUser != UserInformation.UserName.Trim())
                    {
                        MessageBox.Show("对不起，你无权限审核或取消审核");
                        return;
                    }
                }
                if (drvX["bIsFinished"].ToString().ToLower() != "1")
                {
                    string sErr = "";
                    string sX = PubDBCommFuns.sp_Pack_BillWKTskOver(AppInformation.SvrSocket, int.Parse(drvX["nBClass"].ToString()), drvX["cBNo"].ToString(), UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                    if (sX.Trim() != "0")
                    {
                        MessageBox.Show(sErr);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("完成单据作业成功！");
                        btnQry_Click(null, null);
                        //drvX.BeginEdit();
                        //drvX["bIsChecked"] = 1;
                        //drvX["dCheckDate"] = DateTime.Now;
                        //drvX["cChecker"] = UserInformation.UserName;
                        //drvX.EndEdit();
                        //((DataTable)bdsMain.DataSource).AcceptChanges();
                        //DataRowViewToUI(drvX, pnlEdit);
                        //lbl_Check.Visible = true;
                        //lblChecker.Text = "审核人：";
                    }
                }
                else
                {
                    MessageBox.Show("对不起，该单已完成单据作业！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void mi_Dtl_DoBadProd_Click(object sender, EventArgs e)
        {
            if (bdsDtl.Count == 0)
            {
                MessageBox.Show("无不良品物料数据！");
                return ;
            }
            DataRowView drv = (DataRowView) bdsDtl.Current ;
            if (drv == null) return ;
            string sBNo = drv["cBNo"].ToString();
            int nItem = int.Parse(drv["nItem"].ToString());
            string sErr = "";
            //处理不良产品
            frmSelDoBadProdMode frmX = new frmSelDoBadProdMode();
            frmX.ShowDialog();
            if (frmX.IsResultOK)
            {
                string sX = PubDBCommFuns.sp_Pack_DoBadMatMode(AppInformation.SvrSocket,sBNo,nItem,frmX.SelDoMode,UserInformation.UserId,UserInformation.UnitId,"WMS",out sErr);
                if (sErr.Trim() != "")
                    MessageBox.Show(sErr);
            }
            frmX.Dispose();
        }

        private void tlbSaveSysRts_Click(object sender, EventArgs e)
        {
            #region 工具栏
            foreach (ToolStripItem btnX in tlbMain.Items)
            {
                object objX = btnX.Tag;
                if (objX != null)
                {
                    string sErr = "";
                    string sCName = btnX.Text;
                    string sRCode = btnX.Name;
                    string sRID = ModuleRtsId + objX.ToString();
                    PubDBCommFuns.sp_SaveSysRight(AppInformation.SvrSocket, ModuleRtsId, sRID, sCName, "", sRCode, 3, "Sys", out sErr);
                }
            }
            #endregion

            #region 其他
            //foreach (Control ctrlX in pnlBtns.Controls)
            //{
            //    object objX = ctrlX.Tag;
            //    if (objX != null)
            //    {
            //        string sErr = "";
            //        string sCName = ctrlX.Text;
            //        string sRCode = ctrlX.Name;
            //        string sRID = ModuleRtsId + objX.ToString();
            //        PubDBCommFuns.sp_SaveSysRight(AppInformation.SvrSocket, ModuleRtsId, sRID, sCName, "", sRCode, 3, "Sys", out sErr);
            //    }
            //}
            #endregion
        }

      
    }
}

