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

namespace SunEast.App
{
    public partial class frmBillOut : UI.FrmSTable
    {
        public frmBillOut()
        {
            InitializeComponent();
        }

        #region 私有变量
        string strTbNameMain = "TWB_BillIn";
        string strTbNameDtl = "TWB_BillInDtl";

        string strIODataConnErp = "";
        string strIODataConnWms = "";
        string strIODataDllFile = "";
        string strIODataDllClassName = "";
        int nCuSupplierType = 0; 

        DataSet dsM = new DataSet();
        DataSet dsD = new DataSet();
        DataSet dsMEx = new DataSet();
        //主表操作
        OperateType optMain = OperateType.optNone;
        OperateType optDtl = OperateType.optNone;
        //记录当前数据列表的 条件
        string strCondition = "";

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

            //仓库
            //dsX.Clear();
            #region
            DataSet dsY= null;
            #region 仓库数据
            int nWareType = (int)wtWareType;
            strSql = "select * from TWC_WareHouse where 1=1 ";
            if (wtWareType != WareType.wtNone)
            {
                strSql += " and nType=" + nWareType.ToString();
            }
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql += "and cWHId in (select cWHId from TPB_UserWHouse where cUserId='"+ UserInformation.UserId +"')";
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
                colcWHId.DisplayMember = "cName";
                colcWHId.ValueMember = "cWHId";
                colcWHId.DataSource = tbWare;

            }
            #endregion

            //出入库类型
            //dsX.Clear();
            #region 出入库类型
                strSql = "select * from TPB_BillType where nBClass=2";
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
                    tbBillType2 = tbBillType.Copy();
                    cmb_cBTypeId.DisplayMember = "cBType";
                    cmb_cBTypeId.ValueMember = "cBTypeId";
                    cmb_cBTypeId.DataSource = tbBillType;

                    //
                    cmbFindType.DisplayMember = "cBType";
                    cmbFindType.ValueMember = "cBTypeId";
                    cmbFindType.DataSource = tbBillType1;
                    //
                    colcBTypeId.DisplayMember = "cBType";
                    colcBTypeId.ValueMember = "cBTypeId";
                    colcBTypeId.DataSource = tbBillType2;

                }
            #endregion

            #region 用户数据
            strSql = "select * from TPB_User where bUsed=1 ";
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
            cmb_cPayer.DisplayMember = "cName";
            cmb_cPayer.ValueMember = "cName";
            cmb_cPayer.DataSource = tbMUser;
            #endregion

            //供货单位  类别（0:供应商 1:客户）
            #region 客户供应商数据
            strSql = "select * from TPB_CuSupplier where  nType=1 ";
            DataSet dsSupply = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                this.cmb_cDept.DisplayMember = "cCSNameJ";
                cmb_cDept.ValueMember = "cCSId";
                cmb_cDept.DataSource = dsSupply.Tables["data"];
            }
            #endregion

        }
        private void LoadBaseItemFromArr()
        {
            //单据状态
            //ArrayList ArrBillState = new ArrayList(); //单据状态 0:初始化 1:有明细 2:审核 3:已经分盘 4:已下达指令 5:执行指令 9:完成
            ArrBillState.Add(new DictionaryEntry("0", "初始化"));
            ArrBillState.Add(new DictionaryEntry("1", "明细"));
            ArrBillState.Add(new DictionaryEntry("2", "审核"));
            ArrBillState.Add(new DictionaryEntry("3", "已经分盘"));
            ArrBillState.Add(new DictionaryEntry("4", "已下达指令"));
            ArrBillState.Add(new DictionaryEntry("5", "执行指令"));
            ArrBillState.Add(new DictionaryEntry("9", "完成"));
            //
            cmb_nPStatus.DataSource = ArrBillState;
            cmb_nPStatus.DisplayMember = "Value";
            cmb_nPStatus.ValueMember = "Key";

            //明细执行状态
            //ArrayList ArrExecState = new ArrayList(); //执行状态(0:待组盘 1:组盘中 2:组盘结束 3:执行中 4:执行结束 )
            //ArrayList ArrExecState1 = new ArrayList();
            ArrExecState.Add(new DictionaryEntry("0", "待组盘"));
            ArrExecState.Add(new DictionaryEntry("1", "组盘中"));
            ArrExecState.Add(new DictionaryEntry("2", "组盘结束"));
            ArrExecState.Add(new DictionaryEntry("3", "执行中"));
            ArrExecState.Add(new DictionaryEntry("4", "执行结束"));
            //
            ArrExecState1.Add(new DictionaryEntry("0", "待组盘"));
            ArrExecState1.Add(new DictionaryEntry("1", "组盘中"));
            ArrExecState1.Add(new DictionaryEntry("2", "组盘结束"));
            ArrExecState1.Add(new DictionaryEntry("3", "执行中"));
            ArrExecState1.Add(new DictionaryEntry("4", "执行结束"));
            //
            cmb_Dtl_nDoStatus.DataSource = ArrExecState;
            cmb_Dtl_nDoStatus.DisplayMember = "Value";
            cmb_Dtl_nDoStatus.ValueMember = "Key";
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

        private bool ReadIOConfig(string sFile)
        {
            bool bOK = false;
            bOK = System.IO.File.Exists(sFile);
            if (bOK)
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.Load(sFile);
                    System.Xml.XmlNode ndX = null;
                    ndX = xmlDoc.SelectSingleNode("config/DataIOIF/DataIOFile");
                    #region  接口DLL文件
                    if (ndX != null)
                    {
                        System.Xml.XmlAttribute atrb = null;
                        atrb = ndX.Attributes["FileName"];
                        if (atrb != null)
                        {
                            strIODataDllFile = atrb.Value;
                        }
                        atrb = ndX.Attributes["ClassName"];
                        if (atrb != null)
                        {
                            strIODataDllClassName = atrb.Value;
                        }
                    }
                    #endregion
                    ndX = xmlDoc.SelectSingleNode("config/DataIOIF/LcCon");
                    #region 本地连接
                    //if (ndX != null)
                    //{
                    //    string sDataSource = "";
                    //    string sDBName = "";
                    //    string sUser = "";
                    //    string sPwd = "";
                    //    System.Xml.XmlAttribute atrb = null;
                    //    atrb = ndX.Attributes["DBT"];
                    //    if (atrb != null)
                    //    {
                    //        strIODataDBTypeWms = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["DtSource"];
                    //    if (atrb != null)
                    //    {
                    //        sDataSource = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["DBName"];
                    //    if (atrb != null)
                    //    {
                    //        sDBName = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["US"];
                    //    if (atrb != null)
                    //    {
                    //        sUser = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["PD"];
                    //    if (atrb != null)
                    //    {
                    //        sPwd = atrb.Value;
                    //    }
                    //    strIODataConnWms = DBBase.DBBase.GetConnectionString((DataBaseType)int.Parse(strIODataDBTypeWms), sDataSource, sDBName, sUser, sPwd,true);
                    //    bOK = true;
                    //}
                    #endregion

                    #region ERP连接
                    //ndX = xmlDoc.SelectSingleNode("config/DataIOIF/RtCon");
                    //if (ndX != null)
                    //{
                    //    string sDataSource = "";
                    //    string sDBName = "";
                    //    string sUser = "";
                    //    string sPwd = "";
                    //    System.Xml.XmlAttribute atrb = null;
                    //    atrb = ndX.Attributes["DBT"];
                    //    if (atrb != null)
                    //    {
                    //        strIODataDBTypeErp = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["DtSource"];
                    //    if (atrb != null)
                    //    {
                    //        sDataSource = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["DBName"];
                    //    if (atrb != null)
                    //    {
                    //        sDBName = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["US"];
                    //    if (atrb != null)
                    //    {
                    //        sUser = atrb.Value;
                    //    }
                    //    atrb = ndX.Attributes["PD"];
                    //    if (atrb != null)
                    //    {
                    //        sPwd = atrb.Value;
                    //    }
                    //    strIODataConnErp = DBBase.DBBase.GetConnectionString((DataBaseType)int.Parse(strIODataDBTypeErp), sDataSource, sDBName, sUser, sPwd,true);
                    //    bOK = true;
                    //}
                    #endregion
                
            }
            return bOK;
        }

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

        /// <summary>
        /// 加载客户或供应商数据
        /// </summary>
        /// <param name="nCSType"> 类别（-1:全部 0:供应商 1:客户） </param>
        private void LoadCuSupplier(int nCSType)
        {
            string strSql = "select * from TPB_CuSupplier where  1=1 ";
            string err = "";
            //供货单位  类别（0:供应商 1:客户）
            if (nCSType > -1)
            {
                strSql += " and nType in (" + nCSType.ToString() + ",2)";
            }
            DataSet dsSupply = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, "data", 0, 0, "", out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                this.cmb_cDept.DisplayMember = "cCSNameJ";
                cmb_cDept.ValueMember = "cCSId";
                cmb_cDept.DataSource = dsSupply.Tables["data"];
                nCuSupplierType = nCSType;
            }
        }


        private void doSelCuSupplier(string sCSId, string sCSNameJ, string sCSNameQ, UserMS.CSType csType, string sTel, string sFax, string sAddress,
       string sRemark, string cType, int nIsInner, int nIsFactory, string sIsInner, string sIsFactory, int bUsed, string sUsed)
        {
            //DataRowView drvM = (DataRowView)bdsMain.Current;
            //if (drvM == null) return;
            //if (optMain == OperateType.optNew || optMain == OperateType.optEdit)
            //{
            //    drvM["cCSId"] = sCSId;
            //    drvM["cSupplier"] = sCSNameJ;
            //}

            cmb_cDept.SelectedValue = sCSId;
        }


        #endregion

        #region 公共属性

        private CommBase.WareType wtWareType = WareType.wt3D;
        /// <summary>
        /// 仓库类型
        /// </summary>
        [Description("仓库类型")]
        public CommBase.WareType WTWareType
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
            //ModuleRtsId = "3201";
            //ModuleRtsName = "出库单管理";
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
                    lbl_Bill_Count.Text = tbX.Rows.Count.ToString();
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
                        sId = drX["cBNo"].ToString();
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
                    }
                    //bdsMain.Position = iPos;
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
            //bdsMain_PositionChanged(null, null);
            return (bIsOK);
        }
        public bool OpenDtlDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            grdDtl.AutoGenerateColumns = false;
            grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select cBNo,nItem,cMNo,cMName, cSpec,cBatchNo,fQty,fPallet,fFinished,nQCStatus,cQCStatus,nPStatus,cPStatus,nDoStatus,cUnit,dProdDate,cCmptId,nFromItem,cRemark ,dBadDate,cMatStyle,cMatQCLevel,cMatOther,cSupplier,cLinkId,cLinkItem,cFromNo,cCSId,cBNoIn,nItemIn,cWHIdErp,cAreaIdErp,cPosIdErp " +
                         " from v_iobilldetail " + sCon;
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
                    this.bdsDtl.DataSource = dsD.Tables["data"];
                    lbl_Dtl_Count.Text = bdsDtl.Count.ToString();
                    BindDtlDataSetToCtrls();
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
                drv["nBClass"] = 2;
                drv["cWHId"] = cmbFindUser.SelectedValue;
                drv["cBTypeId"] = cmb_cBTypeId.SelectedValue;
                drv["bIsChecked"] = false;
                drv["dDate"] = DateTime.Now;
                drv["cPayer"] = UserInformation.UserName;
                drv["nPStatus"] = 0;

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
            txt_cBNo.BackColor = Color.FromName("Control");
            //cmb_nPStatus.Enabled = false;
            cmb_nPStatus.BackColor = Color.FromName("Control");
            cmb_nPStatus.Enabled = false;
            txt_cChecker.ReadOnly = true;
            txt_cChecker.BackColor = Color.FromName("Control");

        }
        public void DoMEdit()
        {

            optMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv != null && drv["bIsChecked"].ToString() == "1")
            {
                MessageBox.Show("对不起，该单已被审核，不能修改！");
                return;
            }
            if (drv == null) return;
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
            txt_cBNo.BackColor = Color.FromName("Control");
            //cmb_nPStatus.Enabled = false;
            cmb_nPStatus.BackColor = Color.FromName("Control");
            cmb_nPStatus.Enabled = false;
            txt_cChecker.ReadOnly = true;
            txt_cChecker.BackColor = Color.FromName("Control");
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
            if (drv != null && drv["bIsChecked"].ToString() == "1")
            {
                MessageBox.Show("对不起，该单已被审核，不能删除！");
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
                MessageBox.Show("对不起，出库类型不能为空！");
                cmb_cBTypeId.Focus();
                return;
            }

            DataRowView drvX = (DataRowView)bdsMain.Current;
            if ((optMain == OperateType.optNew) || (optMain == OperateType.optEdit))
            {
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
            string sTbName = "TWB_BillIn";
            string sFldKey = "cBNo";
            string sHead = "BO" + DateTime.Now.ToString("yyMMdd");
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
            cmb_Dtl_nDoStatus.Enabled = false;
            cmb_Dtl_nDoStatus.BackColor = Color.FromName("Control");
            cmb_Dtl_nQCStatus.Enabled = false;
            cmb_Dtl_nQCStatus.BackColor = Color.FromName("Control");
            //dtp_dCheckDate.Enabled = false;
            txt_Dtl_cMNo.ReadOnly = true;
            txt_Dtl_cBatchNo.ReadOnly = true;
            txt_Dtl_fPallet.ReadOnly = true;
            txt_Dtl_fFinished.ReadOnly = true;

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
            cmb_Dtl_nDoStatus.Enabled = false;
            cmb_Dtl_nDoStatus.BackColor = Color.FromName("Control");
            cmb_Dtl_nQCStatus.Enabled = false;
            cmb_Dtl_nQCStatus.BackColor = Color.FromName("Control");
            //dtp_dCheckDate.Enabled = false;
            txt_Dtl_cMNo.ReadOnly = true;
            txt_Dtl_cBatchNo.ReadOnly = true;
            txt_Dtl_fPallet.ReadOnly = true;
            txt_Dtl_fFinished.ReadOnly = true;
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

        private void frmBillOut_Load(object sender, EventArgs e)
        {
            #region 权限控制
            tlbSaveSysRts.Visible = UserInformation.UserName == "Admin5118";
            string sErr = "";
            StringBuilder sSql = new StringBuilder("select * from TPB_Rights where cPRId ='" + ModuleRtsId.Trim() + "'");
            if (UserInformation.UserName != "Admin5118")
            {
                sSql.Append(" and cRId in (select cRId from TPB_URTS where cUserId='" + UserInformation.UserId.Trim() + "')");
            }
            DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "UserRights", "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            if (UserInformation.UserName != "Admin5118")
            {
                CheckRights(tlbMain, dsX.Tables["UserRights"]);
            }
            #endregion

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

            //绑定数据
            LoadBaseItem();
            //cmbFindWare.Text = cmbFindWare.Items[0].ToString();
            btnUnFind_Click(null, e);
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            StringBuilder strX = new StringBuilder(" where nBClass=2  ");
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
            if (cmb_FinishedStatus.Text.Trim() != "" && cmb_FinishedStatus.SelectedValue.ToString() != "全部")
            {
                if (cmb_FinishedStatus.SelectedIndex == 1)
                {
                    strX.Append(" and isnull(bIsFinished,0) =1");
                }
                else
                {
                    strX.Append(" and isnull(bIsFinished,0) =0");
                }
            }
            if ((cmbFindCheck.Text.Trim() != "") && (cmbFindCheck.Text.Trim() != "全部"))
            {
                if (cmbFindCheck.SelectedIndex == 1)
                    strX.Append(" and bIsChecked =1");
                else strX.Append(" and bIsChecked =0");
            }
            if (txtFindBillFrom.Text.Trim() != "")
            {
                strX.Append(" and (isnull(cBNo,'') like '%" + txtFindBillFrom.Text.Trim() + "%' or isnull(cBNoFrom,'') like '%" + txtFindBillFrom.Text.Trim() + "%' or isnull(cLinkId,'') like '%" + txtFindBillFrom.Text.Trim() + "%')");
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
            cmbFindCheck.SelectedIndex = -1;
            cmb_FinishedStatus.SelectedIndex = -1;
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
            int nBClass = 0;
            string cBillTypeId = "";
            string sBNo = "";
            DataRowView dr = (DataRowView)bdsMain.Current;
            if (dr != null)
            {
                ClearUIValues(pnlEdit);
                lbl_Check.Visible = false ;
                lbl_BillTskIsOver.Visible = false;

                if ((!dr.IsNew))
                {
                    nBClass = int.Parse(dr["nBClass"].ToString());
                    cBillTypeId = dr["cBTypeId"].ToString().Trim();
                    DataRowViewToUI(dr, pnlEdit);
                    lbl_Check.Visible = true;
                    lbl_BillTskIsOver.Visible = true;
                    if (dr["bIsChecked"].ToString() == "1")
                    {
                        lbl_Check.Text = "已审核";
                    }
                    else
                    {
                        lbl_Check.Text = "未审核";
                    }
                    if (dr["bIsFinished"].ToString() == "1")
                    {
                        this.lbl_BillTskIsOver.Text = "单据作业已完成";
                    }
                    else
                    {
                        lbl_BillTskIsOver.Text = "单据作业未完成";
                    }
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
            drvNewItem["dProdDate"] = DateTime.Now;
            drvNewItem["nQCStatus"] = 0;
            drvNewItem["nPStatus"] = 0;
            drvNewItem["nDoStatus"] = 0;
            drvNewItem["fPallet"] = 0;
            drvNewItem["fFinished"] = 0;
            drvNewItem["cUnit"] = "";
            drvNewItem["cBNo"] = drvM["cBNo"];
            //EnableC();
            FrmItemEditor frmX = new FrmItemEditor();
            try
            {
                frmX.UserInformation = UserInformation;
                frmX.AppInformation = AppInformation;
                frmX.DrvItem = drvNewItem;
                //frmX.DoItem = DoEditMaterialItemData;
                frmX.BIsNew = true;
                frmX.IsOutBill = true;
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
                frmX.IsOutBill = true;

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
            if (bdsDtl.Count == 0)
            {
                MessageBox.Show("对不起，无明细数据可删除!");
                return;
            }
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
            if (drX == null)
            {
                MessageBox.Show("对不起，没有选择需要删除的明细数据!");
                return;
            }
            string sql = "delete from TWB_BillInDtl where cBNo='" + drX["cBNo"].ToString() + "' and nItem= " + drX["nItem"];
            string err = "";
            DataSet ds = PubDBCommFuns.GetDataBySql(sql, out err);
            if (ds.Tables[0].Rows[0][0].ToString() == "0")
                OpenDtlDataSet(" where cBNo='" + drvM["cBNo"].ToString() + "'");
            else MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());
        }

        private void tlb_M_Check_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count > 0)
            {

                #region
                int flag = 0;
                int flag2 = 0;
                int flag3 = 0;
                int flag4 = 0;
                foreach (DataGridViewRow grdr in grdList.SelectedRows)
                {
                    #region
                    if (UserInformation.UType == UserType.utNormal)
                    {
                        string sUser = grdr.Cells["cCreator"].Value.ToString().Trim();
                        if (sUser != UserInformation.UserName.Trim())
                        {
                            MessageBox.Show("对不起，你无权限审核或取消审核");
                            flag4++;
                            return;
                        }
                    }
                    #endregion

                    string sBNo = "";
                    sBNo = grdr.Cells["colcBId"].Value.ToString();
                    int nBClass = 0;
                    nBClass = Convert.ToInt32(grdr.Cells["col_Main_nBClass"].Value);
                    if (sBNo.Trim() != "")
                    {
                        if (grdr.Cells["col_Main_bIsChecked"].Value.ToString().ToLower() == "1")
                        {
                            flag2++;
                            continue;
                        }
                        string sErr = "";
                        string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, nBClass, sBNo, 0, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                        if (sX.Trim() != "0")
                        {
                            MessageBox.Show(sErr);
                            flag3++;
                            continue;
                        }
                        else
                        {
                            flag++;
                        }
                    }
                }
                MessageBox.Show("共审核" + grdList.SelectedRows.Count + "条记录，其中" + flag2 + "条已经被审核过；" + flag + "条审核成功；"+flag4+"条无权限审核；" + flag3 + "条审核失败。");
                #endregion
            }
            else
            {
                MessageBox.Show("未选择数据。");
            }

            #region
            //DataRowView drvX = (DataRowView)bdsMain.Current;
            //if (drvX == null)
            //{
            //    MessageBox.Show("对不起，无数据可审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //{
            //    if (UserInformation.UType == UserType.utNormal)
            //    {
            //        string sUser = drvX["cCreator"].ToString().Trim();
            //        if (sUser != UserInformation.UserName.Trim())
            //        {
            //            MessageBox.Show("对不起，你无权限审核或取消审核");
            //            return;
            //        }
            //    }
            //    if (drvX["bIsChecked"].ToString().ToLower() != "1")
            //    {
            //        #region
            //        //drvX.BeginEdit();
            //        //drvX["bIsChecked"] = 1;
            //        //drvX["dCheckDate"] = DateTime.Now;
            //        //drvX["cChecker"] = UserInformation.UserName;
            //        //drvX.EndEdit();
            //        //string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillIn", "cBNo", false);
            //        //string err = "";
            //        //DataSet ds = PubDBCommFuns.GetDataBySql(sql,DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
            //        //string sX = ds.Tables[0].Rows[0][0].ToString();
            //        //if (sX != "0")
            //        //{
            //        //    drvX.CancelEdit();
            //        //    MessageBox.Show("对不起，审核失败：" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //    return;
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("审核成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        //    ((DataTable)bdsMain.DataSource).AcceptChanges();
            //        //    DataRowViewToUI(drvX, pnlEdit);
            //        //    lbl_Check.Visible = true;
            //        //    lblChecker.Text = "审核人：";
            //        //}
            //        #endregion
            //        string sErr = "";
            //        string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, int.Parse(drvX["nBClass"].ToString()), drvX["cBNo"].ToString(), 0, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
            //        if (sX.Trim() != "0")
            //        {
            //            MessageBox.Show(sErr);
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("审核成功！");
            //            //btnQry_Click(null, null);
            //            drvX.BeginEdit();
            //            drvX["bIsChecked"] = 1;
            //            drvX["dCheckDate"] = DateTime.Now;
            //            drvX["cChecker"] = UserInformation.UserName;
            //            drvX.EndEdit();
            //            ((DataTable)bdsMain.DataSource).AcceptChanges();
            //            DataRowViewToUI(drvX, pnlEdit);
            //            lbl_Check.Visible = true;
            //            lblChecker.Text = "审核人：";
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("对不起，该单已被审核！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            #endregion
        }

        private void tlb_M_UnCheck_Click(object sender, EventArgs e)
        {
            if (grdList.SelectedRows.Count > 0)
            {
                #region
                int flag = 0;
                int flag2 = 0;
                int flag3 = 0;
                int flag4 = 0;
                foreach (DataGridViewRow grdr in grdList.SelectedRows)
                {
                    #region
                    //hack:这段代码可能和pscs传递，多人审核有限制
                    if (UserInformation.UType == UserType.utNormal)
                    {
                        string sUser = grdr.Cells["cCreator"].Value.ToString().Trim();
                        if (sUser != UserInformation.UserName.Trim())
                        {
                            MessageBox.Show("对不起，你无权限审核或取消审核");
                            flag4++;
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
                            flag2++;
                            continue;
                        }
                        string sErr = "";
                        string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, nBClass, sBNo, 1, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                        if (sX.Trim() != "0")
                        {
                            MessageBox.Show(sErr);
                            flag3++;
                            continue;
                        }
                        else
                        {
                            flag++;
                        }
                    }
                }
                MessageBox.Show("共取消审核" + grdList.SelectedRows.Count + "条记录，其中" + flag2 + "条已经是未审核状态；" + flag + "条取消审核成功；"+flag4+"条无权限取消审核；" + flag3 + "条取消审核失败。");
                #endregion
            }
            else
            {
                MessageBox.Show("未选择数据。");
            }
        }

        private void bdsDtl_PositionChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)bdsDtl.Current;
            if (dr != null)
            {
                ClearUIValues(pnlDtlEdit);
                if ((!dr.IsNew))
                {
                    DataRowViewToUI(dr,pnlDtlEdit);
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

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void tlb_M_OverBWK_Click(object sender, EventArgs e)
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
                        if (grdr.Cells["col_Main_bIsFinished"].Value.ToString().ToLower() == "1")
                        {
                            continue;
                        }
                        string sErr = "";
                        string sX = PubDBCommFuns.sp_Pack_BillWKTskOver(AppInformation.SvrSocket, nBClass, sBNo , UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
                        if (sX.Trim() != "0")
                        {
                            MessageBox.Show(sErr);
                            continue;
                        }
                    }
                }
                #endregion
            }

            #region
            //DataRowView drvX = (DataRowView)bdsMain.Current;
            //if (drvX == null)
            //{
            //    MessageBox.Show("对不起，无数据可完成单据作业！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //{
            //    if (UserInformation.UType == UserType.utNormal)
            //    {
            //        string sUser = drvX["cCreator"].ToString().Trim();
            //        if (sUser != UserInformation.UserName.Trim())
            //        {
            //            MessageBox.Show("对不起，你无权限审核或取消审核");
            //            return;
            //        }
            //    }
            //    if (drvX["bIsFinished"].ToString().ToLower() != "1")
            //    {
            //        string sErr = "";
            //        string sX = PubDBCommFuns.sp_Pack_BillWKTskOver(AppInformation.SvrSocket, int.Parse(drvX["nBClass"].ToString()), drvX["cBNo"].ToString(), UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
            //        if (sX.Trim() != "0")
            //        {
            //            MessageBox.Show(sErr);
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("完成单据作业成功！");
            //            btnQry_Click(null, null);
            //            //drvX.BeginEdit();
            //            //drvX["bIsChecked"] = 1;
            //            //drvX["dCheckDate"] = DateTime.Now;
            //            //drvX["cChecker"] = UserInformation.UserName;
            //            //drvX.EndEdit();
            //            //((DataTable)bdsMain.DataSource).AcceptChanges();
            //            //DataRowViewToUI(drvX, pnlEdit);
            //            //lbl_Check.Visible = true;
            //            //lblChecker.Text = "审核人：";
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("对不起，该单已完成单据作业！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            #endregion
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
                    //MessageBox.Show("Form:" + ModuleRtsId + " cRId:" + sRID );
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

        private void cmb_cEventAddr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            string sSql = "";
            string sBillNo = "";
            string sErr = "";
            if (bdsMain.Count == 0)
            {
                MessageBox.Show("对不起，没有可打印的单据！");
                return;
            }
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起，没有可打印的单据！");
                return;
            }
            sBillNo = drv["cBNo"].ToString();
            sSql = "select cBNo,cBClass,cBType,cDept,cPayer,cRemark,cChecker,cStatus," +
                    "dDate,cIsChecked,cIsFinished,cLinkId,cFileNo,cFileName,cEventAddr," +
                    "dEventTime,cEventType,cEventLevel,cStartLevel,cMatClass,cMatUnit " +
                    " from V_IOBill_Ext where cBNo='" + sBillNo.Trim() + "'";
            DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql, "BillOut", "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            DataTable tbMain = dsX.Tables["BillOut"].Copy();
            tbMain.TableName = "BillOut";
            sSql = "select cMNo,cMName,cBNo,cSpec,cBatchNo,dProdDate,dBadDate,fQty,fFinished," +
                    "cQCStatus,cDtlRemark,cUnit from V_IOBillDetail_Ext where cBNo='" + sBillNo.Trim() + "'";
            DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql, "BillOutDtl", "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            DataTable tbDtl = dsY.Tables["BillOutDtl"].Copy();
            tbDtl.TableName = "BillOutDtl";
            DataSet dsRpt = new DataSet();
            dsRpt.Tables.Add(tbMain);
            dsRpt.Tables.Add(tbDtl);            
            Rpts.RptIOBill.PrintBillOut(dsRpt, UserInformation.UnitName, "");
            tbDtl.Clear();
            tbMain.Clear();
            dsRpt.Clear();
            dsX.Clear();
            dsY.Clear();
        }

        private void tlb_M_ErpImp_Click(object sender, EventArgs e)
        {
            //DataInFromMid.DataInFromMid.DataImpBillOut(AppInformation, UserInformation);
            #region 接口连接
            if (strIODataConnErp.Trim() == "" || strIODataConnWms.Trim() == "")
            {
                strIODataConnWms = "";
                strIODataConnErp = "";
                DataTable tbERPCN = null;
                string sErr = "";
                try
                {
                    DataSet dsX = null;
                    dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, "select * from TPB_ERPCN where bUsed=1", "tbERPCN", 0, 0, "", out sErr);
                    if (dsX == null || (sErr.Trim() != "" && sErr.Trim() != "0"))
                    {
                        MessageBox.Show("获取接口连接时，出错：" + sErr);
                        return;
                    }
                    if (dsX.Tables["tbERPCN"] != null)
                    {
                        tbERPCN = dsX.Tables["tbERPCN"];
                        DataRow[] drArr = tbERPCN.Select("nId=0");
                        if (drArr == null || drArr.Length == 0 )
                        {
                            MessageBox.Show("对不起，在接口连接中，不存在WMS本地连接数据！");
                        }
                        else
                        {
                            strIODataConnWms = drArr[0]["cConnStr"].ToString();
                        }
                        drArr= tbERPCN.Select("nId=1");
                        if (drArr == null || drArr.Length == 0)
                        {
                            //对于文件方式的对接可以不需要ERP的连接
                            //MessageBox.Show("对不起，在接口连接中，不存在ERP远程连接数据！");
                        }
                        else
                        {
                            strIODataConnErp = drArr[0]["cConnStr"].ToString();
                        }
                    }
                    dsX.Clear();
                }
                catch (Exception err)
                {
                    MessageBox.Show("获取接口连接数据时出错：" + err.Message);
                    return;
                }
            }
            #endregion
            if (ReadIOConfig(AppInformation.AppConfigFile))
            {
                if ( strIODataConnWms.Trim() != "")
                {
                    System.Data.OleDb.OleDbConnection conErp = null;
                    if (strIODataConnErp.Trim() != "")
                    {
                        conErp = new System.Data.OleDb.OleDbConnection();
                        conErp.ConnectionString = strIODataConnErp;
                    }
                    System.Data.OleDb.OleDbConnection conWms = new System.Data.OleDb.OleDbConnection();
                    conWms.ConnectionString = strIODataConnWms;
                    object[] pars = new object[] { conWms, conErp, UserInformation.UserId, UserInformation.UserName };
                    //首先导入物料基本信息
                    MyCallSafeDllFun(strIODataDllFile, strIODataDllClassName, "DataImportForMaterialInfo", pars);
                    MyCallSafeDllFun(strIODataDllFile, strIODataDllClassName, "DataImportForBillOut", pars);
                }
            }
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            frmQryIOBillDtl frmX = new frmQryIOBillDtl();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.IsInBill = false;
            frmX.ShowDialog();
            frmX.Dispose();
        }

        private void lbl_Customer_Click(object sender, EventArgs e)
        {

            #region
            if ((optMain != OperateType.optNew) && (optMain != OperateType.optEdit))
            {
                MessageBox.Show("对不起，没有处于编辑状态！");
                return;
            }
            if (cmb_cBTypeId.Text.Trim() == "" || cmb_cBTypeId.SelectedValue == null)
            {
                MessageBox.Show("请选择单据类型！");
                cmb_cBTypeId.SelectAll();
                cmb_cBTypeId.Focus();
                return;
            }
            #endregion
            /*
            201	销售出库
            202	领料出库
            203	调拨出库
            204	退货出库
            205	报损出库
            209	其他出库
            */
            string sX = cmb_cBTypeId.SelectedValue.ToString().Trim();
            switch (sX)
            {
                case "201"://销售出库
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstCustomer, 0, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "202"://领料出库
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstCustomer, 0, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "203"://调拨出库
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstCustomer, 1, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "204"://退货出库
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstAll, 1, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "209"://其他出库
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstAll, 1, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                default:
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstAll, 1, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
            }
        }

        private void cmb_cBTypeId_SelectedValueChanged(object sender, EventArgs e)
        {
            object objX = cmb_cBTypeId.SelectedValue;
            if (objX != null)
            {
                string sBType = objX.ToString();
                if (sBType.Trim() == "204")
                {
                    if (nCuSupplierType != 0)
                    {
                        LoadCuSupplier(0);
                    }
                }
                else
                {
                    if (nCuSupplierType != 1)
                    {
                        LoadCuSupplier(1);
                    }
                }
            }
        }

        private void tlb_M_UpdateDtlQtyAfterDo_Click(object sender, EventArgs e)
        {
            if (bdsDtl.Count == 0)
            {
                MessageBox.Show("对不起，无明细数据需要修改其数量！");
                return;
            }
            string sErr = "";
            object objValue = null;
            string sSql = "";
            DataRowView drvDtl = (DataRowView)bdsDtl.Current;
            string sBNo = drvDtl["cBNo"].ToString();
            int nItem = Convert.ToInt32(drvDtl["nItem"]);
            if (drvDtl == null) return;
            #region 获取实际的完成数量和 配盘数量
            double fQtyOld = Convert.ToDouble(drvDtl["fQty"]);
            double fFinished = 0;
            sSql = "select fFinished from TWB_BillInDtl where cBNo='"+ sBNo +"' and nItem="+ nItem.ToString();
            if (DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "fFinished", out objValue, out sErr))
            {
                if (objValue != null && objValue.ToString() != "" && (sErr.Trim() == "" || sErr.Trim() == "0"))
                {
                    fFinished = Convert.ToDouble(objValue);
                }
            }
            double fPallet = 0;
            sSql =  "select sum(fQty) fQty from TWB_WorkTaskDtl where  cBNo='"+ sBNo +"' and nItem="+ nItem.ToString() +
                            " and  nWorkId in (select nWorkId from TWB_WorkTask where nWKStatus < 99)";
            
            if (DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "fQty", out objValue, out sErr))
            {
                if (objValue != null && objValue.ToString() != "" && (sErr.Trim() == "" || sErr.Trim() == "0"))
                {
                    fPallet = Convert.ToDouble(objValue);
                }
            }
            #endregion

            string sQtyNew = "0";
            UI.frmInputMessage frmX = new UI.frmInputMessage();
            frmX.InputValueType = UI.InputMsgType.ittReal;
            frmX.PromptText = "请输入新的数量：";
            frmX.TitleText = "录入数量";
            frmX.ShowDialog();
            if (frmX.ResultIsOK)
            {
                #region
                sQtyNew = frmX.ResultValue.Trim();
                double fQtyNew = double.Parse(sQtyNew);
                if (fQtyNew < (fFinished + fPallet))
                {
                    MessageBox.Show("对不起，录入的新的单据数量(" + sQtyNew + ")不能小于已经完成的数量(" + fFinished.ToString() + ")+已配盘数量("+ fPallet.ToString() +")");
                }
                else if (MessageBox.Show("确认,需要将原单据数量：" + fQtyOld.ToString() + " 修改为：" + sQtyNew + " 吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                     sErr = "";
                     sSql = "Update TWB_BillInDtl set fQty=" + sQtyNew + " where cBNo='" + sBNo + "' and nItem= " + nItem.ToString();
                    bool bOK = DBFuns.DoExecSql(AppInformation.SvrSocket, sSql, "", out sErr);
                    if (bOK && (sErr.Trim() == "" || sErr.Trim() == "0"))
                    {
                        MessageBox.Show("修改单据明细数量成功！");
                        string sText = UserInformation.UserName + " 于 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + " 修改入库单据明细（" + drvDtl["cBNo"].ToString() + "-" + drvDtl["nItem"].ToString() + "） 的数量：" + fQtyOld.ToString() + " 修改为：" + sQtyNew;
                        if (!DBFuns.SP_INSERTUSERLOG(AppInformation.SvrSocket, UserInformation.UserName, "WMS", "修改出入库单明细数量", sText, UserInformation.UnitId, out sErr))
                        {
                            MessageBox.Show(sErr);
                        }

                    }
                    else
                    {
                        MessageBox.Show("修改单据明细数量失败！");
                    }
                    //刷新明细
                    bdsMain_PositionChanged(null, null);
                }
                #endregion
            }
            frmX.Dispose();
            frmX = null;
        }

      


    }
}

