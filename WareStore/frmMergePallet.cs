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

        #region ˽�б���
        //App.WMSUserInfo UserDataInfo = null;
        string strTbNameMain = "TWB_BillMergePlt";
        string strTbNameDtl = "TWB_BillMergePltDtl";



        DataSet dsM = new DataSet();
        DataSet dsD = new DataSet();
        //�������
        OperateType optMain = OperateType.optNone;
        OperateType optDtl = OperateType.optNone;
        //��¼��ǰ�����б�� ����
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
            ModuleRtsId = "3411";
            ModuleRtsName = "���̹���";
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

                        //�����������ʾ
                        if (drX["bIsChecked"].ToString().Trim() == "0" && drX["cChecker"].ToString().Trim() != "")
                            lblChecker.Text = "ȡ�������";
                        else
                            lblChecker.Text = "����ˣ�";
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
            //��ʼ���ֶ�����(Ĭ��ֵ)
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
            lblChecker.Text = "����ˣ�";
            //����¼������
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

            

            //����¼������
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
            //����¼������
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
                //����¼������
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
            txt_cBNo.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            
            if (cmb_cCreator.Text.Trim() == "" || cmb_cCreator.SelectedValue == null)
            {
                MessageBox.Show("�Բ��𣬲ֹ�Ա����Ϊ�գ�");
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
                    MessageBox.Show("�����������ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //����ˢ������                    
                    //btnQry_Click(null, null);
                    ((DataTable)bdsMain.DataSource).AcceptChanges();
                    bdsMain_PositionChanged(null, null);
                    //����¼������
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, (DataTable)bdsMain.DataSource);
                    optMain = OperateType.optNone;
                    //DisplayState(stbState, optMain);
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
            string sHead = "BMP" + DateTime.Now.ToString("yyMMdd");
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
            DataTable tbX = null;
            dsX = sdcX.GetDataSet(AppInformation.SvrSocket, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            return dsX.Tables["data"].Rows[0][0].ToString();
        }
        public int GetNewItem(string billNo)
        {
            string sTbName = strTbNameDtl;
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
                MessageBox.Show("�Բ�����ʼʱ�䲻�ܴ��ڽ�ֹʱ�䣡");
                dtpFind_B.Focus();
                return;
            }
            StringBuilder strSql = new StringBuilder("select * from V_TWB_BillMergePlt where (dCreateDate >='" + dtpFind_B.Value.ToString("yyyy-MM-dd 00:00:00") + "' and dCreateDate <='" + dtpFind_E.Value.ToString("yyyy-MM-dd 23:59:59") + "')");
            #region ���� 
            if (cmbFindUser.Text.Trim() != "" && cmbFindUser.Text.Trim() != "ȫ��" && cmbFindUser.SelectedValue != null)
            {
                strSql.Append(" and cCreator='" + cmbFindUser.SelectedValue.ToString().Trim() + "'");
            }
            if (txtFindBillNo.Text.Trim() != "")
            {
                strSql.Append(" and cBNo like '%" + txtFindBillNo.Text.Trim() + "%'");
            }
            if (this.cmbFindCheck.Text.Trim() != "" && cmbFindCheck.Text.Trim() != "ȫ��" && cmbFindCheck.SelectedIndex >= 0 && cmbFindCheck.SelectedIndex <= 1)
            {
                strSql.Append(" and isnull(bIsChecked,0)='" + cmbFindCheck.SelectedIndex.ToString().Trim() + "'");
            }
            if (this.cmb_FinishedStatus.Text.Trim() != "" && cmb_FinishedStatus.Text.Trim() != "ȫ��" && cmb_FinishedStatus.SelectedIndex >= 0 && cmb_FinishedStatus.SelectedIndex <= 1)
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
                MessageBox.Show("��ȡ����ʱʧ�ܣ�");
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
                    MessageBox.Show("��ȡ����ʱʧ�ܣ�");
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
                MessageBox.Show("�Բ��������ݿ�ɾ����");
                return;
            }
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX == null) return;
            string sBNo= drvX["cBNo"].ToString();
            if (drvX["bIsChecked"].ToString() == "1")
            {
                MessageBox.Show("�Բ��𣬸ĵ��Ѿ���ˣ�����ɾ����");
                return;
            }
            if (MessageBox.Show("��ȷ��Ҫɾ���õ��ţ�" +  sBNo + "  �� ��","ѯ��",MessageBoxButtons.YesNo ,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.No) return;
            string sErr = "";
            if (!DBFuns.DoExecSql(AppInformation.SvrSocket, "delete TWB_BillMergePltDtl where cBNo='" + sBNo + "'", "", out sErr))
            {
                MessageBox.Show("ɾ��������ϸ����ʧ�ܣ�" + sErr);
                return;
            }
            if (!DBFuns.DoExecSql(AppInformation.SvrSocket, "delete TWB_BillMergePlt where cBNo='" + sBNo + "'", "", out sErr))
            {
                MessageBox.Show("ɾ����������ʧ�ܣ�" + sErr);
                return;
            }
            MessageBox.Show("ɾ���������ݳɹ���");
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
                            MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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
                            MessageBox.Show("�Բ�������Ȩ����˻�ȡ�����");
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

