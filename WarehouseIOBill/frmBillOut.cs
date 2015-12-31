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

        #region ˽�б���
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
        //�������
        OperateType optMain = OperateType.optNone;
        OperateType optDtl = OperateType.optNone;
        //��¼��ǰ�����б�� ����
        string strCondition = "";

        //
        ArrayList ArrBillState = new ArrayList(); //����״̬ 0:��ʼ�� 1:����ϸ 2:��� 3:�Ѿ����� 4:���´�ָ�� 5:ִ��ָ�� 9:���
        ArrayList ArrExecState = new ArrayList(); //ִ��״̬(0:������ 1:������ 2:���̽��� 3:ִ���� 4:ִ�н��� )
        ArrayList ArrExecState1 = new ArrayList();
        ArrayList ArrQCState = new ArrayList(); //�ʼ�״̬(0:���� 1:�ϸ� -1:���ϸ�)
        ArrayList ArrQCState1 = new ArrayList();

        #endregion

        #region ˽�з���
        /// <summary>
        /// ���ݵ�ǰ�Ĳ�����ʾ��ǰ�Ĳ���״̬
        /// </summary>
        /// <param name="stbSt"></param>
        /// <param name="optX"></param>
        private void DisplayState(ToolStripLabel stbSt, OperateType optX)
        {
            string strText = "��״̬��";
            if (stbSt != null)
            {
                switch (optX)
                {
                    case OperateType.optNew:
                        strText = strText + " �½�";
                        break;
                    case OperateType.optEdit:
                        strText = strText + " �޸�";
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
            //������λ
            string strSql = "";
            string err = "";

            //�ֿ�
            //dsX.Clear();
            #region
            DataSet dsY= null;
            #region �ֿ�����
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

            //���������
            //dsX.Clear();
            #region ���������
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

            #region �û�����
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

            //������λ  ���0:��Ӧ�� 1:�ͻ���
            #region �ͻ���Ӧ������
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
            //����״̬
            //ArrayList ArrBillState = new ArrayList(); //����״̬ 0:��ʼ�� 1:����ϸ 2:��� 3:�Ѿ����� 4:���´�ָ�� 5:ִ��ָ�� 9:���
            ArrBillState.Add(new DictionaryEntry("0", "��ʼ��"));
            ArrBillState.Add(new DictionaryEntry("1", "��ϸ"));
            ArrBillState.Add(new DictionaryEntry("2", "���"));
            ArrBillState.Add(new DictionaryEntry("3", "�Ѿ�����"));
            ArrBillState.Add(new DictionaryEntry("4", "���´�ָ��"));
            ArrBillState.Add(new DictionaryEntry("5", "ִ��ָ��"));
            ArrBillState.Add(new DictionaryEntry("9", "���"));
            //
            cmb_nPStatus.DataSource = ArrBillState;
            cmb_nPStatus.DisplayMember = "Value";
            cmb_nPStatus.ValueMember = "Key";

            //��ϸִ��״̬
            //ArrayList ArrExecState = new ArrayList(); //ִ��״̬(0:������ 1:������ 2:���̽��� 3:ִ���� 4:ִ�н��� )
            //ArrayList ArrExecState1 = new ArrayList();
            ArrExecState.Add(new DictionaryEntry("0", "������"));
            ArrExecState.Add(new DictionaryEntry("1", "������"));
            ArrExecState.Add(new DictionaryEntry("2", "���̽���"));
            ArrExecState.Add(new DictionaryEntry("3", "ִ����"));
            ArrExecState.Add(new DictionaryEntry("4", "ִ�н���"));
            //
            ArrExecState1.Add(new DictionaryEntry("0", "������"));
            ArrExecState1.Add(new DictionaryEntry("1", "������"));
            ArrExecState1.Add(new DictionaryEntry("2", "���̽���"));
            ArrExecState1.Add(new DictionaryEntry("3", "ִ����"));
            ArrExecState1.Add(new DictionaryEntry("4", "ִ�н���"));
            //
            cmb_Dtl_nDoStatus.DataSource = ArrExecState;
            cmb_Dtl_nDoStatus.DisplayMember = "Value";
            cmb_Dtl_nDoStatus.ValueMember = "Key";
            //
            //this.colnState.DataSource = ArrExecState1;
            //colnState.DisplayMember = "Value";
            //colnState.ValueMember = "Key";

            //�ʼ�״̬(0:���� 1:�ϸ� -1:���ϸ�)
            //ArrayList ArrQCState = new ArrayList(); //�ʼ�״̬(0:���� 1:�ϸ� -1:���ϸ�)
            //ArrayList ArrQCState1 = new ArrayList(); 
            ArrQCState.Add(new DictionaryEntry("0", "����"));
            ArrQCState.Add(new DictionaryEntry("1", "�ϸ�"));
            ArrQCState.Add(new DictionaryEntry("-1", "���ϸ�"));
            //
            ArrQCState1.Add(new DictionaryEntry("0", "����"));
            ArrQCState1.Add(new DictionaryEntry("1", "�ϸ�"));
            ArrQCState1.Add(new DictionaryEntry("-1", "���ϸ�"));
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
                    sX = "��������ֿ�";
                    break;
                case WareType.wt2D:
                    sX = "����ƽ��ֿ�";
                    break;
                case WareType.wtDPS:
                    sX = "����DPS�ֿ�";
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
                    #region  �ӿ�DLL�ļ�
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
                    #region ��������
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

                    #region ERP����
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
        /// �����̰߳�ȫDLL�ļ��ķ���
        /// </summary>
        /// <param name="sFile">DLL�ļ���������·����</param>
        /// <param name="sClassName">����</param>
        /// <param name="sFunName">��������</param>
        /// <param name="parms">�����Ĳ�������</param>
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
                MessageBox.Show(sFile + "  �����ڣ�");
            }
        }

        /// <summary>
        /// ���ؿͻ���Ӧ������
        /// </summary>
        /// <param name="nCSType"> ���-1:ȫ�� 0:��Ӧ�� 1:�ͻ��� </param>
        private void LoadCuSupplier(int nCSType)
        {
            string strSql = "select * from TPB_CuSupplier where  1=1 ";
            string err = "";
            //������λ  ���0:��Ӧ�� 1:�ͻ���
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

        #region ��������

        private CommBase.WareType wtWareType = WareType.wt3D;
        /// <summary>
        /// �ֿ�����
        /// </summary>
        [Description("�ֿ�����")]
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

        #region ��������
        public override void InitFormParameters()
        {
            //ModuleRtsId = "3201";
            //ModuleRtsName = "���ⵥ����";
            //��ʼ�����߰�ťȨ�ޱ�־
            //InitFormTlbBtnTag(tlbMain, ModuleRtsId);
        }
        public void BindMainDataSetToCtrls()
        {
            //�������
            //DataSetUnBind(pnlEdit);
            //�����ݼ�
            //DataSetBind(pnlEdit, this.bdsMain);
            grdList.DataSource = null;
            grdList.DataSource = bdsMain;
        }
        public void BindDtlDataSetToCtrls()
        {
            //�������
            //DataSetUnBind(pnlEdit);
            //�����ݼ�
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
                        //�����������ʾ
                        if (drX["bIsChecked"].ToString().Trim() == "0" && drX["cChecker"].ToString().Trim() != "")
                            lblChecker.Text = "ȡ�������";
                        else
                            lblChecker.Text = "����ˣ�";
                        sId = drX["cBNo"].ToString();
                        lbl_Check.Visible = true;
                        lbl_BillTskIsOver.Visible = true;
                        if (drX["bIsChecked"].ToString() == "1")
                        {
                            lbl_Check.Text = "�����";
                        }
                        else
                        {
                            lbl_Check.Text = "δ���";
                        }
                        if (drX["bIsFinished"].ToString() == "1")
                        {
                            this.lbl_BillTskIsOver.Text = "������ҵ�����";
                        }
                        else
                        {
                            lbl_BillTskIsOver.Text = "������ҵδ���";
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
            //��ʼ���ֶ�����(Ĭ��ֵ)
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
            lblChecker.Text = "����ˣ�";
            //����¼������
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
                MessageBox.Show("�Բ��𣬸õ��ѱ���ˣ������޸ģ�");
                return;
            }
            if (drv == null) return;
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drv["cPayer"].ToString().Trim())
            {
                MessageBox.Show("�Բ�������Ȩ���޸ģ�");
                return;
            }
            //��ʼ���ֶ�����(Ĭ��ֵ)
            drv.BeginEdit();
            drv["dEditDate"] = DateTime.Now;
            drv["cEditor"] = UserInformation.UserName;
            drv.EndEdit();

            //����¼������
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
            //����¼������
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
                MessageBox.Show("�Բ���,�����ݿ�ɾ��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            //if (drv.IsNew || drv.IsEdit)
            if ((0 < iX) && (iX < 3))
            {
                MessageBox.Show("�Բ���,��ǰ�����ڱ༭/�½�״̬,���ȱ����ȡ������!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (drv != null && drv["bIsChecked"].ToString() == "1")
            {
                MessageBox.Show("�Բ��𣬸õ��ѱ���ˣ�����ɾ����");
                return;
            }
            if (MessageBox.Show("ϵͳ������ɾ�����ݣ���ȷ��Ҫɾ����������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drv["cPayer"].ToString().Trim())
            {
                MessageBox.Show("�Բ�������Ȩ��ɾ����");
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
                //����¼������
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
            txt_cBNo.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            if (cmb_cBTypeId.Text.Trim() == "")
            {
                MessageBox.Show("�Բ��𣬳������Ͳ���Ϊ�գ�");
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
                    MessageBox.Show("�����������ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //����ˢ������
                    //btnQry_Click(null, null);
                    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    bdsMain_PositionChanged(null, null);
                    //����¼������
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
                    optMain = OperateType.optNone;
                    DisplayState(stbState, optMain);
                    CtrlControlReadOnly(pnlEdit, false);
                }
                else
                {
                    MessageBox.Show("������������ʧ�ܣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("�Բ��𣬵�ǰû�д��ڱ༭״̬��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //
        public string GetNewId()
        {
            string sTbName = "TWB_BillIn";
            string sFldKey = "cBNo";
            string sHead = "BO" + DateTime.Now.ToString("yyMMdd");
            int iNoLen = 12;
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "sp_GetNewId :pTbName,:pFldKey,:pLen,:pReplaceChar,:pHeader,:pFldCon,:pValueCon";                             //SQL���  �� �洢������ ���в����������ڲ�����������

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            //�������
            ZqmParamter par = null;           //�������� ����                          
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pTbName";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sTbName;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pFldKey";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sFldKey;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pLen";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = iNoLen.ToString();            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.Int;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pReplaceChar";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = "0";            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pHeader";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sHead;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pFldCon";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = "";            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pValueCon";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = "";            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---


            //ִ������
            SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            string sErr = "";
            DataSet dsX = null;
            dsX = sdcX.GetDataSet(AppInformation.SvrSocket, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            return dsX.Tables["data"].Rows[0][0].ToString();
        }
        public int GetNewItem(string billNo)
        {
            string sTbName = "TWB_BillInDtl";
            string sFldKey = "nItem";
            //string sHead = "BI" + DateTime.Now.ToString("yyMMdd");
            //int iNoLen = 12;
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "sp_GetDtlSeq :TbName,:PFld,:SeqFld,:PValue";                             //SQL���  �� �洢������ ���в����������ڲ�����������

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            //�������
            ZqmParamter par = null;           //�������� ����                          
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "TbName";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sTbName;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "PFld";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = "cBNo";            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "SeqFld";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sFldKey;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "PValue";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = billNo;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---

            //ִ������
            SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            string sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;
            dsX = sdcX.GetDataSet(AppInformation.SvrSocket, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
                MessageBox.Show(" ��ȡ��ϸ����޽�����ݣ�" + sErr);
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
                MessageBox.Show("�Բ����޵������ݿɴ�ӡ��");
                return;
            }
            DataRowView drvM = (DataRowView)bdsMain.Current;
            if (drvM == null)
            {
                MessageBox.Show("�Բ����޵������ݿɴ�ӡ��");
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
            //��ʼ���ֶ�����(Ĭ��ֵ)
            drv["nItem"] = GetNewItem(billNo);
            //drv["nQCState"] = 1;
            //drv["cBatchNo"] = DateTime.Now;
            //drv["nFPQty"] = UserInformation.UserName;
            //drv["nState"] = 0;
            //drv["nInQty"] = UserInformation.UnitId;

            drv["dProdDate"] = DateTime.Now;
            drv.EndEdit();

            //��ʾ��ʼ��ֵ
            DataRowViewToUI(drv, pnlDtlEdit);

            //����¼������
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
            //��ʼ���ֶ�����(Ĭ��ֵ)
            drv.BeginEdit();
            drv["dEditDate"] = DateTime.Now;
            drv["cEditor"] = UserInformation.UserName;
            drv.EndEdit();
            //����¼������
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
            //����¼������
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
                MessageBox.Show("�Բ���,����ϸ���ݿ�ɾ��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (drv.IsNew || drv.IsEdit)
            if ((0 < iX) && (iX < 3))
            {
                MessageBox.Show("�Բ���,��ǰ�����ڱ༭/�½�״̬,���ȱ����ȡ������!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool bX = false;
            //DataMainToObjInfo(drv);
            //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInDtl(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true) == "0";
            if (bX)
            {
                optDtl = OperateType.optDelete;
                OpenDtlDataSet(strCondition);
                //����¼������
                CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
                optDtl = OperateType.optNone;
                DisplayState(stbState, optDtl);
                CtrlControlReadOnly(pnlDtlEdit, false);
            }
            else
            {
                MessageBox.Show("�Բ���,ɾ������ʧ��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void DoDSave()
        {
            txt_Dtl_cMNo.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
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
                    MessageBox.Show("������ϸ���ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //����ˢ������
                    OpenDtlDataSet(" where cBNo='" + drvX["cBNo"].ToString() + "'");
                    //����¼������
                    CtrlOptButtons(this.pnlBtns, pnlDtlEdit, optDtl, (DataTable)bdsDtl.DataSource);
                    optDtl = OperateType.optNone;
                    DisplayState(stbState, optDtl);
                    CtrlControlReadOnly(pnlDtlEdit, false);
                }
                else
                {
                    MessageBox.Show("������������ʧ�ܣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("�Բ��𣬵�ǰû�д��ڱ༭״̬��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void frmBillOut_Load(object sender, EventArgs e)
        {
            #region Ȩ�޿���
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
            //��ʼ��
            InitFormParameters();
            stbModul.Text = "��ģ�顿" + ModuleRtsName;
            this.Text = ModuleRtsName;
            if (UserInformation != null)
            {
                stbUser.Text = "���û���" + UserInformation.UserName;
            }
            stbState.Text = "��״̬��   ";
            stbDateTime.Text = "��ʱ�䡿" + DateTime.Now.ToString();

            //������
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
            if (cmb_FinishedStatus.Text.Trim() != "" && cmb_FinishedStatus.SelectedValue.ToString() != "ȫ��")
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
            if ((cmbFindCheck.Text.Trim() != "") && (cmbFindCheck.Text.Trim() != "ȫ��"))
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
                        lbl_Check.Text = "�����";
                    }
                    else
                    {
                        lbl_Check.Text = "δ���";
                    }
                    if (dr["bIsFinished"].ToString() == "1")
                    {
                        this.lbl_BillTskIsOver.Text = "������ҵ�����";
                    }
                    else
                    {
                        lbl_BillTskIsOver.Text = "������ҵδ���";
                    }
                    if (dr["bIsChecked"].ToString().Trim() == "0" && dr["cChecker"].ToString().Trim() != "")
                        lblChecker.Text = "ȡ�������";
                    else
                        lblChecker.Text = "����ˣ�";
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
                MessageBox.Show("�Բ�������δ���棬���ȱ����������ݣ���������ϸ!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;           
            DataRowView drvM = (DataRowView)bdsMain.Current;
            if (drvM["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("�Բ����ѱ���ˣ�");
                return;
            }
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drvM["cPayer"].ToString().Trim())
            {
                MessageBox.Show("�Բ�������Ȩ����ɴ˲�����");
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
                MessageBox.Show("�Բ�������δ���棬���ȱ����������ݣ����޸���ϸ!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            DataRowView drvM = (DataRowView)bdsMain.Current;
            if (drvM["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("�Բ����ѱ���ˣ�");
                return;
            }
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drvM["cPayer"].ToString().Trim())
            {
                MessageBox.Show("�Բ�������Ȩ����ɴ˲�����");
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
                MessageBox.Show("�Բ�������༭�У�����ɾ��!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            if (bdsDtl.Count == 0)
            {
                MessageBox.Show("�Բ�������ϸ���ݿ�ɾ��!");
                return;
            }
            DataRowView drvM = (DataRowView)bdsMain.Current;
            //if (drvM["bIsChecked"].ToString().ToLower() == "true")
            if (drvM["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("�Բ����ѱ���ˣ�");
                return;
            }
            if (MessageBox.Show("ϵͳ������ɾ�������ݣ����ָܻ�����ȷ��Ҫɾ����������", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            if (UserInformation.UType == UserType.utNormal && UserInformation.UserName.Trim() != drvM["cPayer"].ToString().Trim())
            {
                MessageBox.Show("�Բ�������Ȩ����ɴ˲�����");
                return;
            }
            DataRowView drX = (DataRowView)bdsDtl.Current;
            if (drX == null)
            {
                MessageBox.Show("�Բ���û��ѡ����Ҫɾ������ϸ����!");
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
                            MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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
                MessageBox.Show("�����" + grdList.SelectedRows.Count + "����¼������" + flag2 + "���Ѿ�����˹���" + flag + "����˳ɹ���"+flag4+"����Ȩ����ˣ�" + flag3 + "�����ʧ�ܡ�");
                #endregion
            }
            else
            {
                MessageBox.Show("δѡ�����ݡ�");
            }

            #region
            //DataRowView drvX = (DataRowView)bdsMain.Current;
            //if (drvX == null)
            //{
            //    MessageBox.Show("�Բ��������ݿ���ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //{
            //    if (UserInformation.UType == UserType.utNormal)
            //    {
            //        string sUser = drvX["cCreator"].ToString().Trim();
            //        if (sUser != UserInformation.UserName.Trim())
            //        {
            //            MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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
            //        //    MessageBox.Show("�Բ������ʧ�ܣ�" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //    return;
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("��˳ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        //    ((DataTable)bdsMain.DataSource).AcceptChanges();
            //        //    DataRowViewToUI(drvX, pnlEdit);
            //        //    lbl_Check.Visible = true;
            //        //    lblChecker.Text = "����ˣ�";
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
            //            MessageBox.Show("��˳ɹ���");
            //            //btnQry_Click(null, null);
            //            drvX.BeginEdit();
            //            drvX["bIsChecked"] = 1;
            //            drvX["dCheckDate"] = DateTime.Now;
            //            drvX["cChecker"] = UserInformation.UserName;
            //            drvX.EndEdit();
            //            ((DataTable)bdsMain.DataSource).AcceptChanges();
            //            DataRowViewToUI(drvX, pnlEdit);
            //            lbl_Check.Visible = true;
            //            lblChecker.Text = "����ˣ�";
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("�Բ��𣬸õ��ѱ���ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    //hack:��δ�����ܺ�pscs���ݣ��������������
                    if (UserInformation.UType == UserType.utNormal)
                    {
                        string sUser = grdr.Cells["cCreator"].Value.ToString().Trim();
                        if (sUser != UserInformation.UserName.Trim())
                        {
                            MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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
                MessageBox.Show("��ȡ�����" + grdList.SelectedRows.Count + "����¼������" + flag2 + "���Ѿ���δ���״̬��" + flag + "��ȡ����˳ɹ���"+flag4+"����Ȩ��ȡ����ˣ�" + flag3 + "��ȡ�����ʧ�ܡ�");
                #endregion
            }
            else
            {
                MessageBox.Show("δѡ�����ݡ�");
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
            //    MessageBox.Show("�Բ��������ݿ���ɵ�����ҵ��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            //{
            //    if (UserInformation.UType == UserType.utNormal)
            //    {
            //        string sUser = drvX["cCreator"].ToString().Trim();
            //        if (sUser != UserInformation.UserName.Trim())
            //        {
            //            MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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
            //            MessageBox.Show("��ɵ�����ҵ�ɹ���");
            //            btnQry_Click(null, null);
            //            //drvX.BeginEdit();
            //            //drvX["bIsChecked"] = 1;
            //            //drvX["dCheckDate"] = DateTime.Now;
            //            //drvX["cChecker"] = UserInformation.UserName;
            //            //drvX.EndEdit();
            //            //((DataTable)bdsMain.DataSource).AcceptChanges();
            //            //DataRowViewToUI(drvX, pnlEdit);
            //            //lbl_Check.Visible = true;
            //            //lblChecker.Text = "����ˣ�";
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("�Բ��𣬸õ�����ɵ�����ҵ��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            #endregion
        }

        private void tlbSaveSysRts_Click(object sender, EventArgs e)
        {
            #region ������
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

            #region ����
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
                MessageBox.Show("�Բ���û�пɴ�ӡ�ĵ��ݣ�");
                return;
            }
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null)
            {
                MessageBox.Show("�Բ���û�пɴ�ӡ�ĵ��ݣ�");
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
            #region �ӿ�����
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
                        MessageBox.Show("��ȡ�ӿ�����ʱ������" + sErr);
                        return;
                    }
                    if (dsX.Tables["tbERPCN"] != null)
                    {
                        tbERPCN = dsX.Tables["tbERPCN"];
                        DataRow[] drArr = tbERPCN.Select("nId=0");
                        if (drArr == null || drArr.Length == 0 )
                        {
                            MessageBox.Show("�Բ����ڽӿ������У�������WMS�����������ݣ�");
                        }
                        else
                        {
                            strIODataConnWms = drArr[0]["cConnStr"].ToString();
                        }
                        drArr= tbERPCN.Select("nId=1");
                        if (drArr == null || drArr.Length == 0)
                        {
                            //�����ļ���ʽ�ĶԽӿ��Բ���ҪERP������
                            //MessageBox.Show("�Բ����ڽӿ������У�������ERPԶ���������ݣ�");
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
                    MessageBox.Show("��ȡ�ӿ���������ʱ����" + err.Message);
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
                    //���ȵ������ϻ�����Ϣ
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
                MessageBox.Show("�Բ���û�д��ڱ༭״̬��");
                return;
            }
            if (cmb_cBTypeId.Text.Trim() == "" || cmb_cBTypeId.SelectedValue == null)
            {
                MessageBox.Show("��ѡ�񵥾����ͣ�");
                cmb_cBTypeId.SelectAll();
                cmb_cBTypeId.Focus();
                return;
            }
            #endregion
            /*
            201	���۳���
            202	���ϳ���
            203	��������
            204	�˻�����
            205	�������
            209	��������
            */
            string sX = cmb_cBTypeId.SelectedValue.ToString().Trim();
            switch (sX)
            {
                case "201"://���۳���
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstCustomer, 0, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "202"://���ϳ���
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstCustomer, 0, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "203"://��������
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstCustomer, 1, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "204"://�˻�����
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstAll, 1, -1, cmb_cDept.Text.Trim(), doSelCuSupplier);
                    break;
                case "209"://��������
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
                MessageBox.Show("�Բ�������ϸ������Ҫ�޸���������");
                return;
            }
            string sErr = "";
            object objValue = null;
            string sSql = "";
            DataRowView drvDtl = (DataRowView)bdsDtl.Current;
            string sBNo = drvDtl["cBNo"].ToString();
            int nItem = Convert.ToInt32(drvDtl["nItem"]);
            if (drvDtl == null) return;
            #region ��ȡʵ�ʵ���������� ��������
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
            frmX.PromptText = "�������µ�������";
            frmX.TitleText = "¼������";
            frmX.ShowDialog();
            if (frmX.ResultIsOK)
            {
                #region
                sQtyNew = frmX.ResultValue.Trim();
                double fQtyNew = double.Parse(sQtyNew);
                if (fQtyNew < (fFinished + fPallet))
                {
                    MessageBox.Show("�Բ���¼����µĵ�������(" + sQtyNew + ")����С���Ѿ���ɵ�����(" + fFinished.ToString() + ")+����������("+ fPallet.ToString() +")");
                }
                else if (MessageBox.Show("ȷ��,��Ҫ��ԭ����������" + fQtyOld.ToString() + " �޸�Ϊ��" + sQtyNew + " ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                     sErr = "";
                     sSql = "Update TWB_BillInDtl set fQty=" + sQtyNew + " where cBNo='" + sBNo + "' and nItem= " + nItem.ToString();
                    bool bOK = DBFuns.DoExecSql(AppInformation.SvrSocket, sSql, "", out sErr);
                    if (bOK && (sErr.Trim() == "" || sErr.Trim() == "0"))
                    {
                        MessageBox.Show("�޸ĵ�����ϸ�����ɹ���");
                        string sText = UserInformation.UserName + " �� " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + " �޸���ⵥ����ϸ��" + drvDtl["cBNo"].ToString() + "-" + drvDtl["nItem"].ToString() + "�� ��������" + fQtyOld.ToString() + " �޸�Ϊ��" + sQtyNew;
                        if (!DBFuns.SP_INSERTUSERLOG(AppInformation.SvrSocket, UserInformation.UserName, "WMS", "�޸ĳ���ⵥ��ϸ����", sText, UserInformation.UnitId, out sErr))
                        {
                            MessageBox.Show(sErr);
                        }

                    }
                    else
                    {
                        MessageBox.Show("�޸ĵ�����ϸ����ʧ�ܣ�");
                    }
                    //ˢ����ϸ
                    bdsMain_PositionChanged(null, null);
                }
                #endregion
            }
            frmX.Dispose();
            frmX = null;
        }

      


    }
}

