using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zqm.CommBase;
using Zqm.DBCommInfo;
using SunEast;
using System.Collections;

namespace SunEast.App
{


    public partial class frmpBillOut : UI.FrmSTable
    {
        public frmpBillOut()
        {
            InitializeComponent();
        }
        #region ˽�б���
        int nOperator = 1; //���
        //App.WMSUserInfo UserDataInfo = null;
        string strTbNameMain = "TWB_BillIn";
        string strTbNameDtl = "TWB_BillInDtl";
        DataSet dsM = new DataSet();
        DataSet dsD = new DataSet();
        //�������
        OperateType optMain = OperateType.optNone;
        OperateType optDtl = OperateType.optNone;
        //��¼��ǰ�����б�� ����
        string strCondition = "";
        bool bDSIsOpenForMain = false; //��¼���ݼ��Ƿ��
        bool bDSIsOpenForDtl = false; //��¼���ݼ��Ƿ��

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
            string strSql = "select * from TPC_Unit";
            string err = "";
            //DataSet dsX1 = new DataSet();
            //DataTable tbUnit = new DataTable();
            //dsX1 = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out err);
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
            strSql = "select * from TWC_WareHouse where nType=2";
            err = "";
            DataTable tbWare = new DataTable();
            DataTable tbWare1 = new DataTable();
            DataTable tbWare2 = new DataTable();
            //strSql = BI.BSIOBillBI.BSIOBillBI.GetItemWareHouseList(AppInformation.dbtApp, AppInformation.AppConn, dsX, UserInformation, "");
            DataSet dsY = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err != "")
                MessageBox.Show(strSql, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                tbWare = dsY.Tables["data"].Copy();
                tbWare1 = tbWare.Copy();
                tbWare2 = tbWare.Copy();
                cmb_cWHId.DisplayMember = "cName";
                cmb_cWHId.ValueMember = "cWHId";
                cmb_cWHId.DataSource = tbWare;

                //
                cmbFindWare.DisplayMember = "cName";
                cmbFindWare.ValueMember = "cWHId";
                cmbFindWare.DataSource = tbWare1;
                //
                colcWHId.DisplayMember = "cName";
                colcWHId.ValueMember = "cWHId";
                colcWHId.DataSource = tbWare2;
            }
            //���������
            //dsX.Clear();
            strSql = "select * from TPB_BillType where nBClass=2";
            err = "";
            DataTable tbBillType = new DataTable();
            DataTable tbBillType1 = new DataTable();
            DataTable tbBillType2 = new DataTable();
            //strSql = BI.BSIOBillBI.BSIOBillBI.GetBillIOTypeList(AppInformation.dbtApp, AppInformation.AppConn, dsX, UserInformation, " where nOperate=" + nOperator.ToString());
            DataSet dsZ = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out err);
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
                colcType.DisplayMember = "cBType";
                colcType.ValueMember = "cBTypeId";
                colcType.DataSource = tbBillType2;

            }
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
        //private bool CheckIsRightUser(string sUId, string sRtsId)
        //{
        //    bool bIsOK = false;
        //    if (sUId.Trim() == "2011001001")
        //    {
        //        bIsOK = true;
        //    }
        //    else
        //    {
        //        bIsOK = BI.RTS.RTSBI.CheckUserIsRights(AppInformation.dbtApp, AppInformation.AppConn, sUId, sRtsId);
        //    }
        //    return (bIsOK);
        //}

        //private bool DoEditMaterialItemData(DataRowView drvX)
        //{
        //    bool bX = false;
        //    string sX = "";
        //    sX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInDtl(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drvX, false);
        //    bX = sX == "0";
        //    if (bX == true)
        //    {
        //        DataRowView drvM = (DataRowView)bdsMain.Current;
        //        //OpenDtlDataSet(" where cBId='" +drvM["cBId"].ToString() + "'");
        //    }
        //    else
        //    {
        //        MessageBox.Show("����������ϸ���ݳ���");
        //    }
        //    return (bX);
        //}
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
            ModuleRtsId = "3203";
            ModuleRtsName = "ƽ��������";
            //��ʼ�����߰�ťȨ�ޱ�־
            InitFormTlbBtnTag(tlbMain, ModuleRtsId);
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
            bDSIsOpenForMain = false;
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string sql = "select * from TWB_BillIn " + sCon + " and cWHId in (select cWHId from TWC_WareHouse where nType=2)";
            string err = "";
            //if (dsM.Tables["data"] != null)
            //    dsM.Tables["data"].Clear();
            dsM.Clear();
            dsM = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out err);
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
                    ClearUIValues(pnlEdit);
                    if (bdsMain.Count > 0)
                    {
                        DataRowView drX = (DataRowView)bdsMain.Current;
                        DataRowViewToUI(drX, pnlEdit);
                        //�����������ʾ
                        if ((drX["cChecker"].ToString() != ""))
                            lblChecker.Text = "ȡ������ˣ�";
                        else lblChecker.Text = "����ˣ�";
                        sId = drX["cBNo"].ToString();
                    }
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
            string sql = "select * from TWB_BillInDtl " + sCon;
            string err = "";
            //if (dsD.Tables["data"] != null)
            //    dsD.Tables["data"].Clear();
            dsD.Clear();
            dsD = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out err);
            bIsOK = err == "";
            if (!bIsOK)
                MessageBox.Show(strX);
            else
            {
                try
                {
                    this.bdsDtl.DataSource = dsD.Tables["data"];
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
            bDSIsOpenForDtl = true;
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
                drv["cBNo"] = GetNewId();
                drv["nBClass"] = 2;
                drv["cWHId"] = 1;
                drv["cBTypeId"] = 3;
                drv["bIsChecked"] = false;
                drv["dDate"] = DateTime.Now;
                drv["cPayer"] = UserInformation.UserName;
                drv["nPStatus"] = 0;

                drv["dCreateDate"] = DateTime.Now;
                drv["cCreator"] = UserInformation.UserName;
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
            }
            else return;
            //DBDataSet.Tables[strTbNameMain].AcceptChanges();
            dsM.Tables["data"].AcceptChanges();
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, dsM.Tables["data"]);
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
            if (MessageBox.Show("ϵͳ������ɾ�����ݣ���ȷ��Ҫɾ����������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            bool bX = false;
            string sql = "delete from TWB_BillIn where cBNo='" + drv["cBNo"].ToString() + "'";
            DataSet ds = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out sX);
            //DataMainToObjInfo(drv);
            //sX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInMain(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true);
            bX = ds.Tables[0].Rows[0][0].ToString() == "0";
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
                MessageBox.Show(sX, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void DoMSave()
        {
            txt_cBNo.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            if (cmb_cBTypeId.Text.Trim() == "")
            {
                MessageBox.Show("�Բ���������Ͳ���Ϊ�գ�");
                cmb_cBTypeId.Focus();
                return;
            }
            if (cmb_cWHId.Text.Trim() == "")
            {
                MessageBox.Show("�Բ��𣬲ֿⲻ��Ϊ�գ�");
                cmb_cWHId.Focus();
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
                    sql = Zqm.DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, "cBNo", true);
                else
                    sql = Zqm.DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, "cBNo", false);
                string err = "";
                DataSet ds = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out err);
                //if (drvX.IsEdit) drvX.EndEdit();
                //DataMainToObjInfo(drvX);
                //bX = BI.BSIOBillBI.BSIOBillBI.DoIOBillInMain(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drvX, false) == "0";
                if (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "0")
                {
                    optMain = OperateType.optSave;
                    MessageBox.Show("�����������ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //����ˢ������
                    OpenMainDataSet(strCondition);
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
            Zqm.DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
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
            DataTable tbX = null;
            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            return dsX.Tables["data"].Rows[0][0].ToString();
        }
        public int GetNewItem(string billNo)
        {
            string sTbName = "TWB_BillInDtl";
            string sFldKey = "nItem";
            //string sHead = "BI" + DateTime.Now.ToString("yyMMdd");
            //int iNoLen = 12;
            Zqm.DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
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
            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            return (int)dsX.Tables["data"].Rows[0][0];
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
            dsD.AcceptChanges();
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

        private void frmBillIn_Load(object sender, EventArgs e)
        {
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

            btnUnFind_Click(null, e);
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            StringBuilder strX = new StringBuilder(" where nBClass=2 and ");
            if (dtpFind_B.Text.Trim() != "")
            {
                strX.Append(" dDate >='" + dtpFind_B.Value.ToString("yyyy-MM-dd 00:00:00") + "'");
            }
            if (dtpFind_E.Text.Trim() != "")
            {
                strX.Append(" and dDate <='" + dtpFind_E.Value.ToString("yyyy-MM-dd 23:59:29") + "'");
            }
            if (cmbFindWare.Text.Trim() != "")
            {
                strX.Append(" and cWHId='" + cmbFindWare.SelectedValue.ToString() + "'");
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
            cmbFindWare.SelectedIndex = -1;
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
            DataRowView dr = (DataRowView)bdsMain.Current;
            if (dr != null)
            {
                ClearUIValues(pnlEdit);
                if ((!dr.IsNew))
                {
                    DataRowViewToUI(dr, pnlEdit);
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
            if (drvM["bIsChecked"].ToString().ToLower() == "true")
            {
                MessageBox.Show("�Բ����ѱ���ˣ�");
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
            if (drvM["bIsChecked"].ToString().ToLower() == "true")
            {
                MessageBox.Show("�Բ����ѱ���ˣ�");
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
                MessageBox.Show("�Բ�������༭�У�����ɾ��!");
                return;
            }
            if (bdsDtl == null) return;
            if (bdsMain.Count == 0) return;
            DataRowView drvM = (DataRowView)bdsMain.Current;
            if (drvM["bIsChecked"].ToString().ToLower() == "true")
            {
                MessageBox.Show("�Բ����ѱ���ˣ�");
                return;
            }
            if (MessageBox.Show("ϵͳ������ɾ�������ݣ����ָܻ�����ȷ��Ҫɾ����������", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            DataRowView drX = (DataRowView)bdsDtl.Current;
            string sql = "delete from TWB_BillInDtl where cBNo='" + drX["cBNo"].ToString() + "' and nItem= " + drX["nItem"];
            string err = "";
            DataSet ds = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out err);
            if (ds.Tables[0].Rows[0][0].ToString() == "0")
                OpenDtlDataSet(" where cBNo='" + drvM["cBNo"].ToString() + "'");
            else MessageBox.Show(ds.Tables[0].Rows[0][0].ToString());
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
                if (drvX["bIsChecked"].ToString().ToLower() != "1")
                {
                            drvX.BeginEdit();
                            drvX["bIsChecked"] = 1;
                            drvX["dCheckDate"] = DateTime.Now;
                            drvX["cChecker"] = UserInformation.UserName;
                            drvX.EndEdit();
                            string sql = Zqm.DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillIn", "cBNo", false);
                            string err = "";
                            DataSet ds = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out err);
                            string sX = ds.Tables[0].Rows[0][0].ToString();
                            if (sX != "0")
                            {
                                MessageBox.Show("�Բ������ʧ�ܣ�" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("��˳ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DataRowViewToUI(drvX, pnlEdit);
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
            long iX = 0;
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null)
            {
                MessageBox.Show("�Բ��������ݿ���ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (drvX["bIsChecked"].ToString().ToLower() == "1")
                {
                            //�Ƿ�����ȡ�����
                            string sSql = "select nPStatus from " + strTbNameMain + " where cBNo='" + drvX["cBNo"].ToString() + "'";
                            string err = "";
                            //sX = Zqm.DBBase.DBOptrForMSSql.OptExecRetInt(AppInformation.AppConn, sSql, out iX);
                            DataSet ds = SunEast.App.PubDBCommFuns.GetDataBySql(sSql.ToString(), out err);
                            if ((ds.Tables[0].Rows[0][0].ToString() != "0") || (int.Parse(ds.Tables["data"].Rows[0][0].ToString()) > 0))
                            {
                                MessageBox.Show("�Բ��𣬸õ��Ѿ���ִ�У�����ȡ����ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //
                            drvX.BeginEdit();
                            drvX["bIsChecked"] = 0;
                            drvX["dCheckDate"] = DateTime.Now;
                            drvX["cChecker"] = UserInformation.UserName;
                            drvX.EndEdit();
                            string sql = Zqm.DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, "TWB_BillIn", "cBNo", false);
                            string err1 = "";
                            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(sql, out err);
                            sX = ds1.Tables[0].Rows[0][0].ToString();
                            if (sX != "0")
                            {
                                MessageBox.Show("�Բ���ȡ�����ʧ�ܣ�" + sX, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("ȡ����˳ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DataRowViewToUI(drvX, pnlEdit);
                            }
                }
                else
                {
                    MessageBox.Show("�Բ��𣬸õ�δ����ˣ�����ȡ����ˣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
                    DataRowViewToUI(dr, pnlDtlEdit);
                }
            }
        }

        private void txt_cBNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            ChangeTextBoxBkColorByReadOnly(sender, ((System.Windows.Forms.Control)sender).Parent.BackColor, Color.White);
        }
    }
}

