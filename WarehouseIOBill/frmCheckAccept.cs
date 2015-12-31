using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using System.Collections;
using DBCommInfo;

namespace SunEast.App
{
    public partial class frmCheckAccept : UI.FrmSTable
    {
        public frmCheckAccept()
        {
            InitializeComponent();
        }


        #region ˽�б���
        string strTbNameMain = "TWB_BillChkAccept";
        string strTbNameDtl = "TWB_BillChkAcceptDtl";
        //
        bool bIsEx = false; //�Ƿ�������չ��TWB_BillChkAccept_Ex��
        string strTbMainEx = "TWB_BillChkAccept_Ex"; //������չ��ı���

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

            //�ֿ�
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
            DataTable tbWare = new DataTable();
            DataSet dsY = DBFuns.GetDataBySql(AppInformation.SvrSocket,false, strSql,"data",0,0,"", out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                tbWare = dsY.Tables["data"].Copy();

                colcWHId.DisplayMember = "cName";
                colcWHId.ValueMember = "cWHId";
                colcWHId.DataSource = tbWare;
            }
            //���������
            //dsX.Clear();
            strSql = "select * from TPB_BillType where nBClass=1";
            err = "";
            DataTable tbBillType = new DataTable();
            DataTable tbBillType1 = new DataTable();
            DataTable tbBillType2 = new DataTable();
            //strSql = BI.BSIOBillBI.BSIOBillBI.GetBillIOTypeList(AppInformation.dbtApp, AppInformation.AppConn, dsX, UserInformation, " where nOperate=" + nOperator.ToString());
            DataSet dsZ = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, "data", 0, 0, "", out err);
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
            strSql = "select * from TPB_User where bUsed=1 ";
            if (UserInformation.UType == UserType.utNormal)
            {
                strSql += " and cUserId='" + UserInformation.UserId + "'";
            }
            else if (UserInformation.UType == CommBase.UserType.utAdmin)
            {
                strSql += " and cDeptId='" + UserInformation.DeptId.Trim() + "'";
            }
            else if (UserInformation.UserId != "90101001")
            {
                //strSql += " and cDeptId='" + UserInformation.DeptId.Trim() + "'";
                strSql += " and cUserId in (select cUserId from TPB_UserMgrArea where cMAreaId in(select cMAreaId from TPB_UserMgrArea where cUserId='" + UserInformation.UserId + "'))";
            }

            DataSet dsUser = DBFuns.GetDataBySql(AppInformation.SvrSocket,false, strSql,"data",0,0,"", out err);
            cmbFindUser.DisplayMember = "cName";
            cmbFindUser.ValueMember = "cName";
            cmbFindUser.DataSource = dsUser.Tables["data"];
            DataTable tbMUser = dsUser.Tables["data"].Copy();
            cmb_cPayer.DisplayMember = "cName";
            cmb_cPayer.ValueMember = "cName";
            cmb_cPayer.DataSource = tbMUser;

            //������λ  ���0:��Ӧ�� 1:�ͻ���
            strSql = "select * from TPB_CuSupplier where  nType=0 ";
            DataSet dsSupply = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, "data", 0, 0, "", out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                this.cmb_cDept.DisplayMember = "cCSNameJ";
                cmb_cDept.ValueMember = "cCSNameJ";
                cmb_cDept.DataSource = dsSupply.Tables["data"];
            }
            #region ��չ����������
            if (bIsEx)
            {
                //strSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='��Ȼ�ֺ�����' order by nSort,nId";

                DataSet dsZHType = null;
                //dsZHType = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, out err);
                //if (err.Trim() != "" && err.Trim() != "0")
                //    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else
                //{
                //    this.cmb_cEventType.DisplayMember = "cItemName";
                //    cmb_cEventType.ValueMember = "cItemName";
                //    cmb_cEventType.DataSource = dsZHType.Tables["data"];
                //}
                strSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='��������' order by nSort,nId";
                if (dsZHType != null)
                    dsZHType.Clear();
                dsZHType = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, strSql, "data", 0, 0, "", out err);
                if (err.Trim() != "" && err.Trim() != "0")
                    MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    this.cmb_cMatClass.DisplayMember = "cItemName";
                    cmb_cMatClass.ValueMember = "cItemName";
                    cmb_cMatClass.DataSource = dsZHType.Tables["data"];
                }
                //if (dsZHType != null)
                //dsZHType.Clear();
                //strSql = "select cItemNo,cItemName from TWC_BaseItem  where bUsed=1 and cItemType='��������' order by nSort,nId";
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
            //ArrExecState.Add(new DictionaryEntry("0", "������"));
            //ArrExecState.Add(new DictionaryEntry("1", "������"));
            //ArrExecState.Add(new DictionaryEntry("2", "���̽���"));
            //ArrExecState.Add(new DictionaryEntry("3", "ִ����"));
            //ArrExecState.Add(new DictionaryEntry("4", "ִ�н���"));
            ////
            //ArrExecState1.Add(new DictionaryEntry("0", "������"));
            //ArrExecState1.Add(new DictionaryEntry("1", "������"));
            //ArrExecState1.Add(new DictionaryEntry("2", "���̽���"));
            //ArrExecState1.Add(new DictionaryEntry("3", "ִ����"));
            //ArrExecState1.Add(new DictionaryEntry("4", "ִ�н���"));
            //
            //cmb_Dtl_nDoStatus.DataSource = ArrExecState;
            //cmb_Dtl_nDoStatus.DisplayMember = "Value";
            //cmb_Dtl_nDoStatus.ValueMember = "Key";
            //
            //this.colnState.DataSource = ArrExecState1;
            //colnState.DisplayMember = "Value";
            //colnState.ValueMember = "Key";

            //�ʼ�״̬(0:���� 1:�ϸ� -1:���ϸ�)
            //ArrayList ArrQCState = new ArrayList(); //�ʼ�״̬(0:���� 1:�ϸ� -1:���ϸ�)
            //ArrayList ArrQCState1 = new ArrayList(); 
            //ArrQCState.Add(new DictionaryEntry("0", "����"));
            //ArrQCState.Add(new DictionaryEntry("1", "�ϸ�"));
            //ArrQCState.Add(new DictionaryEntry("-1", "���ϸ�"));
            ////
            //ArrQCState1.Add(new DictionaryEntry("0", "����"));
            //ArrQCState1.Add(new DictionaryEntry("1", "�ϸ�"));
            //ArrQCState1.Add(new DictionaryEntry("-1", "���ϸ�"));
            ////
            //cmb_Dtl_nQCStatus.DataSource = ArrQCState;
            //cmb_Dtl_nQCStatus.DisplayMember = "Value";
            //cmb_Dtl_nQCStatus.ValueMember = "Key";
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

        /// <summary>
        /// ����ϵͳ������ȷ���Ƿ�������չ��
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


        #region ��������չ�����ݣ�����ʾ
        /// <summary>
        /// ��������չ�����ݣ�����ʾ
        /// </summary>
        /// <param name="nBClass">��������</param>
        /// <param name="cBTypeId">��������</param>
        /// <param name="cBNo">����</param>
        private void OpenMainEx(int nBClass, string cBTypeId, string cBNo)
        {
            string sErr = "";
            string sSql = "select * from " + strTbMainEx + " where nBClass=" + nBClass.ToString() + " and cBTypeId='" + cBTypeId + "'" + " and cBNo='" + cBNo + "'";
            dsMEx = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql, strTbMainEx, "dEventTime", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show("����չ������ʱ����" + sErr);
                return;
            }
            else if (dsMEx == null || dsMEx.Tables[strTbMainEx] == null)
            {
                MessageBox.Show("����չ������ʱ����!");
                return;
            }
            DataTable tbX = dsMEx.Tables[strTbMainEx];
            bdsMainEx.DataSource = tbX;
            //�����
            ClearUIValues(pnlEditEx);
            if (bdsMainEx.DataSource != null)
            {
                DataRowView drvEx = (DataRowView)bdsMainEx.Current;
                if (drvEx != null)
                    DataRowViewToUI(drvEx, pnlEditEx);
            }
            CtrlControlReadOnly(pnlEditEx, false);
        }

        #endregion

        #endregion


        #region ��������

        private WareType wtWareType = WareType.wt3D;
        /// <summary>
        /// �ֿ�����
        /// </summary>
        [Description("�ֿ�����")]
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

        #region ��������
        public override void InitFormParameters()
        {
            ModuleRtsId = "3107";
            ModuleRtsName = "������չ���";
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
            string sql = "select * from " + strTbNameMain + sCon;
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
            dsM = DBFuns.GetDataBySql(AppInformation.SvrSocket,false, sql,strTbNameMain,0,0, "dDate,dCheckDate,dCreateDate,dEditDate", out err);
            //DBDataSet.Tables[strTbNameMain] = ds.Tables[strTbNameMain].Copy();
            bIsOK = err == "";
            if (!bIsOK)
                MessageBox.Show(strX);
            else
            {
                try
                {

                    sId = "";
                    DataTable tbX = dsM.Tables[strTbNameMain];
                    this.bdsMain.DataSource = tbX;
                    BindMainDataSetToCtrls();
                    bdsMain.Position = iPos;
                    ClearUIValues(pnlEdit);
                    lbl_Check.Visible = false;
                    if (bdsMain.Count > 0)
                    {
                        DataRowView drX = (DataRowView)bdsMain.Current;
                        DataRowViewToUI(drX, pnlEdit);

                        //�����������ʾ
                        if (drX["bIsChecked"].ToString().Trim() == "0" && drX["cChecker"].ToString().Trim() != "")
                            lblChecker.Text = "ȡ�������";
                        else
                            lblChecker.Text = "����ˣ�";
                        lbl_Check.Visible = true;
                        if (drX["bIsChecked"].ToString() == "1")
                        {
                            lbl_Check.Text = "�����";
                        }
                        else
                        {
                            lbl_Check.Text = "δ���";
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
            return (bIsOK);
        }
        public bool OpenDtlDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            grdDtl.AutoGenerateColumns = false;
            grdDtl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select cBNo,nItem,cMNo,cMName, cSpec,cMatStyle,cMatQCLevel,cSupplier,cMatOther, cBatchNo,fQty,fAccept,fOK,fBad,"+
	            "cUnit,dProdDate ,dBadDate,cBNoIn,nItemIn,cFromNo,nFromItem,cLinkId,cLinkItem,cCmptId,cRemark,cCSId,cMRemark "+
	            " from V_BillChkAcceptDtl" + sCon;
            string err = "";
            //if (dsD.Tables["data"] != null)
            //    dsD.Tables["data"].Clear();
            dsD.Clear();
            dsD = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sql,strTbNameDtl, 0, 0, "dDate,dProdDate,dBadDate", out err);
            bIsOK = err == "";
            if (!bIsOK)
                MessageBox.Show(strX);
            else
            {
                try
                {
                    this.bdsDtl.DataSource = dsD.Tables[strTbNameDtl];
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
                drv["nBClass"] = 11;
                //drv["cWHId"] = cmbFindUser.SelectedValue;
                drv["cBTypeId"] = cmb_cBTypeId.SelectedValue;
                drv["bIsChecked"] = false;
                drv["dDate"] = DateTime.Now;
                drv["cPayer"] = UserInformation.UserName;
                drv["dCreateDate"] = DateTime.Now;
                drv["cCreator"] = UserInformation.UserName;
                drv["cUserId"] = UserInformation.UserId;
                drv["cCmptId"] = UserInformation.UnitId;
                #region �½���չ��
                if (bIsEx)
                {
                    DataRowView drvEx = (DataRowView)bdsMainEx.AddNew();
                    drvEx["nBClass"] = drv["nBClass"];
                    drvEx["cBTypeId"] = drv["cBTypeId"];
                    drvEx["cBNo"] = drv["cBNo"];
                    drvEx["cMatClass"] = "��������";
                    drvEx["cMatUnit"] = "������";
                    DataRowViewToUI(drvEx, pnlEditEx);
                    CtrlControlReadOnly(pnlEditEx, true);
                }
                #endregion
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
            //cmb_nPStatus.Enabled = false;
            cmb_nPStatus.BackColor = Color.FromName("Control");
            txt_cChecker.ReadOnly = true;

        }
        public void DoMEdit()
        {

            optMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null) return;
            if (drv["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("�Բ����Ѿ���ˣ������޸ģ�");
                return;
            }
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

            #region �޸���չ��
            if (bIsEx)
            {
                DataRowView drvEx = (DataRowView)bdsMainEx.Current;
                drvEx.BeginEdit();
                //drvEx["nBClass"] = drv["nBClass"];
                //drvEx["cBTypeId"] = drv["cBTypeId"];
                //drvEx["cBNo"] = drv["cBNo"];
                //drvEx["cEventType"] = "����";
                //drvEx["cEventLevel"] = "���";
                //drvEx["dEventTime"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                //DataRowViewToUI(drvEx, pnlEditEx);
                CtrlControlReadOnly(pnlEditEx, true);
            }
            #endregion

            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
            txt_cBNo.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cBNo.ReadOnly = true;
            //cmb_nPStatus.Enabled = false;
            cmb_nPStatus.BackColor = Color.FromName("Control");
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
                #region ������չ��
                if (bIsEx)
                {
                    DataRowView drvEx = (DataRowView)bdsMainEx.Current;
                    if (drvEx.IsEdit)
                    {
                        drvEx.CancelEdit();
                    }
                    if (drvEx.IsNew)
                    {
                        drvEx.Delete();
                    }
                    CtrlControlReadOnly(pnlEditEx, false);
                }
                #endregion
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
            if (drv == null) return;
            if (drv["bIsChecked"].ToString().ToLower() == "1")
            {
                MessageBox.Show("�Բ����Ѿ���ˣ������޸ģ�");
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
            string sql = "delete from TWB_BillChkAccept where cBNo='" + drv["cBNo"].ToString() + "'";
            DataSet ds = PubDBCommFuns.GetDataBySql(sql, out sX);
            //DataMainToObjInfo(drv);
            //sX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInMain(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true);
            bX = ds.Tables[0].Rows[0][0].ToString() == "0";
            */
            string sErr = "";
            sX = DBFuns.SP_BillChkAcceptDel(AppInformation.SvrSocket, drv["cBNo"].ToString(), UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
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
            DataRowView drvMEx = null;
            string sSqlMainEx = "";
            txt_cBNo.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            if (cmb_cBTypeId.Text.Trim() == "")
            {
                MessageBox.Show("�Բ���������Ͳ���Ϊ�գ�");
                cmb_cBTypeId.Focus();
                return;
            }
            if (cmb_cPayer.Text.Trim() == "" || cmb_cPayer.SelectedValue == null)
            {
                MessageBox.Show("�Բ��𣬲ֹ�Ա����Ϊ�գ�");
                cmb_cPayer.Focus();
                return;
            }
            if (cmb_cDept.Text.Trim() == "" || cmb_cDept.SelectedValue == null)
            {
                MessageBox.Show("�Բ��𣬹�����λ����Ϊ�գ�");
                cmb_cDept.Focus();
                return;
            }
            // �����չ��
            #region �����չ��
            if (bIsEx)
            {
                if (cmb_cMatClass.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ������������Ϊ�գ�");
                    cmb_cMatClass.Focus();
                    return;
                }
                if (txt_cFileNo.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ����ļ����Ų���Ϊ�գ�");
                    txt_cFileNo.Focus();
                    return;
                }
                if (this.txt_cFileName.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ����ļ�������Ϊ�գ�");
                    txt_cFileName.Focus();
                    return;
                }
            }
            #endregion

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
                    // ������չ��
                    if (bIsEx)
                    {
                        drvMEx = (DataRowView)bdsMainEx.Current;
                        UIToDataRowView(drvMEx, pnlEditEx);
                        drvMEx["nBClass"] = drvX["nBClass"];
                        drvMEx["cBTypeId"] = drvX["cBTypeId"];
                        drvMEx["cBNo"] = drvX["cBNo"];
                        sSqlMainEx = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvMEx, strTbMainEx, "cBNo", true);

                    }
                }
                else
                {
                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, "cBNo", false);
                    // ������չ��
                    if (bIsEx)
                    {
                        drvMEx = (DataRowView)bdsMainEx.Current;
                        UIToDataRowView(drvMEx, pnlEditEx);
                        sSqlMainEx = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvMEx, strTbMainEx, "cBNo", false);
                    }
                }

                string err = "";
                DataSet ds =DBFuns.GetDataBySql(AppInformation.SvrSocket, sql, DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
                if (bIsEx)
                {
                    dsMEx = DBFuns.GetDataBySql(AppInformation.SvrSocket, sSqlMainEx, DBSQLCommandInfo.GetFieldsForDate(drvMEx), out err);
                }

                //if (drvX.IsEdit) drvX.EndEdit();
                //DataMainToObjInfo(drvX);
                //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInMain(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drvX, false) == "0";
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
            string sTbName = strTbNameMain;
            string sFldKey = "cBNo";
            string sHead = "BCA" + DateTime.Now.ToString("yyMMdd");
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
            #region
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

            #endregion
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
            string sTbName = strTbNameDtl ;
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
            #region
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
            #endregion

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
            //cmb_Dtl_nDoStatus.Enabled = false;
            //cmb_Dtl_nDoStatus.BackColor = Color.FromName("Control");
            //cmb_Dtl_nQCStatus.Enabled = false;
            //cmb_Dtl_nQCStatus.BackColor = Color.FromName("Control");
            //dtp_dCheckDate.Enabled = false;
            txt_Dtl_cMNo.ReadOnly = true;
            txt_Dtl_cBatchNo.ReadOnly = true;
            txt_Dtl_fAccept.ReadOnly = true;
            txt_Dtl_fOK.ReadOnly = true;

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
            //cmb_Dtl_nDoStatus.Enabled = false;
            //cmb_Dtl_nDoStatus.BackColor = Color.FromName("Control");
            //cmb_Dtl_nQCStatus.Enabled = false;
            //cmb_Dtl_nQCStatus.BackColor = Color.FromName("Control");
            //dtp_dCheckDate.Enabled = false;
            txt_Dtl_cMNo.ReadOnly = true;
            txt_Dtl_cBatchNo.ReadOnly = true;
            txt_Dtl_fAccept.ReadOnly = true;
            txt_Dtl_fOK.ReadOnly = true;
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
            string sErr = "";
            //DataMainToObjInfo(drv);
            //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInDtl(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true) == "0";
            bX = DBFuns.sp_BillChkAcceptDtlDel(AppInformation.SvrSocket,drv["cBNo"].ToString(),Convert.ToInt32(drv["nItem"]),UserInformation.UserId,UserInformation.UnitId,"WMS",out sErr) == "0";
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

        private void btn_Dtl_Delete_Click(object sender, EventArgs e)
        {
            if (optMain == OperateType.optNew || optMain == OperateType.optEdit)
            {
                MessageBox.Show("�Բ�������༭�У�����ɾ��!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
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
            string sql = "delete from TWB_BillChkAcceptDtl where cBNo='" + drX["cBNo"].ToString() + "' and nItem= " + drX["nItem"];
            string err = "";
            DataSet ds = PubDBCommFuns.GetDataBySql(sql, out err);
            if (ds.Tables[0].Rows[0][0].ToString() == "0")
                OpenDtlDataSet(" where cBNo='" + drvM["cBNo"].ToString() + "'");
            else MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());
        }

        private void frmCheckAccept_Load(object sender, EventArgs e)
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

            DoIsUseExTable();
            pnlEditEx.Visible = bIsEx;
            //������
            LoadBaseItem();
            //cmbFindWare.Text = cmbFindWare.Items[0].ToString();
            btnUnFind_Click(null, e);
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            DoMNew();
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            DoMEdit();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            DoMUndo();
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            DoMDelete();
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            DoMSave();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            btnQry_Click(null, null);
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            StringBuilder strX = new StringBuilder(" where nBClass=11  ");
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
            if ((cmbFindCheck.Text.Trim() != "") && (cmbFindCheck.Text.Trim() != "ȫ��"))
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

        private void tlb_M_Check_Click(object sender, EventArgs e)
        {
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null)
            {
                MessageBox.Show("�Բ��������ݿ���ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (UserInformation.UType == UserType.utNormal)
                {
                    string sUser = drvX["cCreator"].ToString().Trim();
                    if (sUser != UserInformation.UserName.Trim())
                    {
                        MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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
                    //string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillChkAccept", "cBNo", false);
                    //string err = "";
                    //DataSet ds = PubDBCommFuns.GetDataBySql(sql,DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
                    //string sX = ds.Tables[0].Rows[0][0].ToString();
                    //if (sX != "0")
                    //{
                    //    drvX.CancelEdit();
                    //    MessageBox.Show("�Բ������ʧ�ܣ�" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("��˳ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    //    DataRowViewToUI(drvX, pnlEdit);
                    //    lbl_Check.Visible = true;
                    //    lblChecker.Text = "����ˣ�";
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
                        MessageBox.Show("��˳ɹ���");
                        //btnQry_Click(null, null);
                        drvX.BeginEdit();
                        drvX["bIsChecked"] = 1;
                        drvX["dCheckDate"] = DateTime.Now;
                        drvX["cChecker"] = UserInformation.UserName;
                        drvX.EndEdit();
                        ((DataTable)bdsMain.DataSource).AcceptChanges();
                        DataRowViewToUI(drvX, pnlEdit);
                        lbl_Check.Visible = true;
                        lblChecker.Text = "����ˣ�";
                    }
                }
                else
                {
                    MessageBox.Show("�Բ��𣬸õ��ѱ���ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void tlb_M_UnCheck_Click(object sender, EventArgs e)
        {
            string sX = "";
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null)
            {
                MessageBox.Show("�Բ��������ݿ���ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (UserInformation.UType == UserType.utNormal)
                {
                    string sUser = drvX["cCreator"].ToString().Trim();
                    if (sUser != UserInformation.UserName.Trim())
                    {
                        MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
                        return;
                    }
                }
                if (drvX["bIsChecked"].ToString().ToLower() == "1")
                {
                    #region
                    //�Ƿ�����ȡ�����
                    //string sSql = "select nPStatus from " + strTbNameMain + " where cBNo='" + drvX["cBNo"].ToString() + "'";
                    //string err = "";
                    ////sX = DBBase.DBOptrForMSSql.OptExecRetInt(AppInformation.AppConn, sSql, out iX);
                    //DataSet ds = PubDBCommFuns.GetDataBySql(sSql.ToString(), out err);
                    //if ((ds.Tables[0].Rows[0][0].ToString() != "0") || (int.Parse(ds.Tables["data"].Rows[0][0].ToString()) > 0))
                    //{
                    //    MessageBox.Show("�Բ��𣬸õ��Ѿ���ִ�У�����ȡ����ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    ////
                    //drvX.BeginEdit();
                    //drvX["bIsChecked"] = 0;
                    //drvX["dCheckDate"] = DateTime.Now;
                    //drvX["cChecker"] = UserInformation.UserName;
                    //drvX.EndEdit();
                    //string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillChkAccept", "cBNo", false);
                    //string err1 = "";
                    //DataSet ds1 = PubDBCommFuns.GetDataBySql(sql,DBSQLCommandInfo.GetFieldsForDate(drvX), out err);
                    //sX = ds1.Tables[0].Rows[0][0].ToString();
                    //if (sX != "0")
                    //{
                    //    drvX.CancelEdit();
                    //    MessageBox.Show("�Բ���ȡ�����ʧ�ܣ�" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ȡ����˳ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    //    DataRowViewToUI(drvX, pnlEdit);
                    //    lbl_Check.Visible = false;
                    //    lblChecker.Text = "ȡ������ˣ�";
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
                        MessageBox.Show("ȡ����˳ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        drvX.BeginEdit();
                        drvX["bIsChecked"] = 0;
                        drvX["dCheckDate"] = DateTime.Now;
                        drvX["cChecker"] = UserInformation.UserName;
                        drvX.EndEdit();

                        ((DataTable)bdsMain.DataSource).AcceptChanges();
                        DataRowViewToUI(drvX, pnlEdit);
                        lbl_Check.Visible = false;
                        lblChecker.Text = "ȡ������ˣ�";
                    }
                }
                else
                {
                    MessageBox.Show("�Բ��𣬸õ�δ����ˣ�����ȡ����ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
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
            //if (drvM["bIsChecked"].ToString().ToLower() == "true")
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
            drvNewItem["fAccept"] = 0;
            drvNewItem["fOK"] = 0;
            drvNewItem["fBad"] = 0;
            drvNewItem["cUnit"] = "";
            drvNewItem["cBNo"] = drvM["cBNo"];
            //EnableC();
            frmItemEditorIn frmX = new frmItemEditorIn();
            try
            {
                frmX.UserInformation = UserInformation;
                frmX.AppInformation = AppInformation;
                frmX.DrvItem = drvNewItem;
                //frmX.DoItem = DoEditMaterialItemData;
                frmX.BIsNew = true;
                frmX.BClass = Convert.ToInt16(drvM["nBClass"]);
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
                MessageBox.Show("�Բ�������δ���棬���ȱ����������ݣ����޸���ϸ!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            DataRowView drvM = (DataRowView)bdsMain.Current;
            //if (drvM["bIsChecked"].ToString().ToLower() == "true")
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
            frmItemEditorIn frmX = new frmItemEditorIn();
            try
            {
                frmX.UserInformation = UserInformation;
                frmX.AppInformation = AppInformation;
                frmX.DrvItem = drX;
                //frmX.DoItem = DoEditMaterialItemData;
                frmX.BClass = Convert.ToInt16(drvM["nBClass"]);
                frmX.BType = drvM["cBTypeId"].ToString();
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

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            int nBClass = 0;
            string cBillTypeId = "";
            string sBNo = "";
            DataRowView dr = (DataRowView)bdsMain.Current;
            if (dr != null)
            {
                ClearUIValues(pnlEdit);
                lbl_Check.Visible = false;
                if ((!dr.IsNew))
                {
                    nBClass = int.Parse(dr["nBClass"].ToString());
                    cBillTypeId = dr["cBTypeId"].ToString().Trim();
                    DataRowViewToUI(dr, pnlEdit);
                    lbl_Check.Visible = true;
                    if (dr["bIsChecked"].ToString() == "1")
                    {
                        lbl_Check.Text = "�����";
                    }
                    else
                    {
                        lbl_Check.Text = "δ���";
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
            //���ƴ���չ������
            if (bIsEx)
            {
                OpenMainEx(nBClass, cBillTypeId, sBNo);
            }
            OpenDtlDataSet(" where cBNo='" + sBNo + "'");

        }

        private void bdsDtl_PositionChanged(object sender, EventArgs e)
        {
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

    }
}

