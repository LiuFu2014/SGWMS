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
using SunEast.App;

namespace UserMS
{
    public partial class frmDept : UI.FrmSTable
    {
        public frmDept()
        {
            InitializeComponent();
        }

        #region ˽�б���

        string strTbNameMain = "TPB_Dept";
        string strKeyFld = "cDeptId";
        string strFix = "";
        bool bMainlstIsOpened = false;
        //�������
        OperateType optMain = OperateType.optNone;
        //��¼��ǰ�����б�� ����
        StringBuilder sbConndition = new StringBuilder("");
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
            }

        }

        private string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader)
        {
            //sp_GetNewId(@TbName varchar(50),@FldKey varchar(50),@Len int=0,@ReplaceChar varchar(2)='0',@Header varchar(10)='',
            //@FldCon varchar(50)='',@ValueCon varchar(50)='')
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "sp_GetNewId";                             //SQL���  �� �洢������ ���в����������ڲ�����������

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            //�������
            ZqmParamter par = null;           //�������� ����                          
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@TbName";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sTbName;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@FldKey";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sKeyFld;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@Len";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = nLength.ToString();            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.Int;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@ReplaceChar";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = "0";            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@Header";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = sHeader;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@FldCon";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = "";            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            ////---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "@ValueCon";           //�������� ��ʵ�ʶ����һ��
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
            if (dsX != null)
            {
                tbX = dsX.Tables["data"];
                if (tbX != null)
                    sId = tbX.Rows[0]["cNewId"].ToString();
            }
            dsX.Clear();
            return sId;
        }

        private void LoadBaseData()
        {
            string sErr = "";
            string sSql = "select * from TPB_Compt ";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " where cComptId='" + UserInformation.UnitId + "'";
            }
            DataSet dsX = null;
            dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "TPB_Compt", 0, 0, out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
                return;
            }
            cmb_Compt.DataSource = dsX.Tables["TPB_Compt"];
            cmb_Compt.DisplayMember = "cCmptName";
            cmb_Compt.ValueMember = "cComptId";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                cmb_Compt.SelectedValue = UserInformation.UnitId;
                if (cmb_Compt.SelectedIndex > -1)
                    cmb_Compt.Enabled = false;
            }
            else
            {
                cmb_Compt.Enabled = true;
            }

        }
        #endregion
        #region ��������
        public override void InitFormParameters()
        {
            //ModuleRtsId = "2101";
            //ModuleRtsName = "��λ��Ϣ����";
            if (ModuleRtsId.Length > 0)
            {
                Text = ModuleRtsName;

            }
            stbModul.Text = "ģ�顾" + Text + "��";
            if (UserInformation != null)
            {
                stbUser.Text = "�û���" + UserInformation.UserName + "��";
            }
            //��ʼ�����߰�ťȨ�ޱ�־
            //InitFormTlbBtnTag(tlbMain, ModuleRtsId);
        }
        public void BindDataSetToCtrls()
        {
            //�������
            DataSetUnBind(pnlEdit);
            //�����ݼ�
            DataSetBind(pnlEdit, this.bdsMain);
            grdList.DataSource = null;
            grdList.DataSource = bdsMain;
        }
        public bool OpenMainDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            bMainlstIsOpened = false;
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "select * from " + strTbNameMain + sCon;             //SQL���  �� �洢������ ���в����������ڲ�����������
            if (UserInformation.UType == UserType.utSupervisor)
            {
                cmdInfo.SqlText = "select * from " + strTbNameMain  ; //����ʾ��ǰ�û��ĵ�λ
            }
            cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������
            SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            string sErr = "";
            //DataSet dsX = null;
            //DataTable tbX= null ;
            DBDataSet = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            bIsOK = DBDataSet != null;
            //if (bIsOK)
            //{
            //    DBDataSet.Clear();
            //    tbX  = new DataTable(strTbNameMain);
            //    tbX = dsX.Tables["data"].Copy();
            //    DBDataSet.Tables.Add(tbX);
            //}
            if (!bIsOK)
                MessageBox.Show(sErr);
            else
            {
                try
                {
                    DataSetUnBind(pnlEdit);
                    this.bdsMain.DataSource = DBDataSet.Tables[strTbNameMain];
                    if (bdsMain != null && bdsMain.Count > 0)
                        BindDataSetToCtrls();
                    bIsOK = true;
                    optMain = OperateType.optNone;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsOK = false;
                }
            }
            bMainlstIsOpened = true;
            return (bIsOK);
        }
        public void DoMNew()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("�Բ�������Ȩ���½�������Ϣ��");
                return;
            }
            if (cmb_Compt.SelectedValue == null)
            {
                MessageBox.Show("�Բ�������ѡ��λ��");
                cmb_Compt.Focus();
                return;
            }
            optMain = OperateType.optNew;
            DataRowView drv = (DataRowView)bdsMain.AddNew();
            //��ʼ���ֶ�����(Ĭ��ֵ)
            drv["dCreateDate"] = DateTime.Now;
            drv["cCreator"] = UserInformation.UserName;
            drv["cCmptId"] = cmb_Compt.SelectedValue.ToString();
            txt_cCmptId.Text = drv["cCmptId"].ToString();
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cDeptId.ReadOnly = true;
        }
        public void DoMEdit()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("�Բ�������Ȩ���޸Ĳ�����Ϣ��");
                return;
            }
            optMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsMain.Current;
            //��ʼ���ֶ�����(Ĭ��ֵ)
            drv.BeginEdit();
            drv["dEditDate"] = DateTime.Now;
            drv["cEditor"] = UserInformation.UserName;
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cDeptId.ReadOnly = true;
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
            DBDataSet.Tables[strTbNameMain].AcceptChanges();
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            optMain = OperateType.optNone;
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, false);
        }
        public void DoMDelete()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("�Բ�������Ȩ��ɾ��������Ϣ��");
                return;
            }
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
            if (MessageBox.Show("ϵͳ������ɾ�������ݣ����ָܻ�����ȷ��Ҫɾ����������", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "delete " + strTbNameMain + " where " + strKeyFld + "='" + drv[strKeyFld].ToString() + "'";             //SQL���  �� �洢������ ���в����������ڲ�����������            
            cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������ Ĭ��Ϊ "data"
            SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            string sErr = "";
            DataSet dsX = null;
            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            bool bX = dsX != null;
            //DataMainToObjInfo(drv);
            //bX = BI.BasicPubInfo.BasicPubInfoBI.DoCompanyInfo(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true);
            if (bX)
            {
                optMain = OperateType.optDelete;
                OpenMainDataSet(sbConndition.ToString());
                //����¼������
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                optMain = OperateType.optNone;
                DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, false);
            }
            else
            {
                MessageBox.Show("�Բ���,ɾ������ʧ��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void DoMSave()
        {
            string sSql = "";
            txt_cDeptId.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (txt_cCmptId.Text.Trim() == "")
                {
                    MessageBox.Show("��λ���벻��Ϊ�գ�");
                    txt_cCmptId.Focus();
                    return;
                }
                //if (this.txt_cDeptId.Text.Trim() == "")
                //{
                //    MessageBox.Show("���ű��벻��Ϊ�գ�");
                //    txt_cDeptId.Focus();
                //    return;
                //}
                if (this.txt_cName.Text.Trim() == "")
                {
                    MessageBox.Show("�������Ʋ���Ϊ�գ�");
                    txt_cName.Focus();
                    return;
                }
                UIToDataRowView(drvX, pnlEdit);
                if (drvX[strKeyFld].ToString() == "" || drvX[strKeyFld].ToString() == "-1") //��������Ҫ�����µĺ���
                {

                    //drvX[strKeyFld]  = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, 3, "1");
                    string sXX = drvX["cCmptId"].ToString().Trim();
                    drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, 5, sXX);
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, true);//���� insert ���
                }
                else
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, false);//����UPDATE ���
                bool bX = false;
                if (drvX.IsEdit) drvX.EndEdit();
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = sSql;             //SQL���  �� �洢������ ���в����������ڲ�����������
                cmdInfo.FldsData = DBSQLCommandInfo.GetFieldsForDate(drvX);
                cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������
                SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                string sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                bX = dsX.Tables[0].Rows[0][0].ToString() == "0";
                if (bX)
                {
                    optMain = OperateType.optSave;
                    MessageBox.Show("�������ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //����ˢ������
                    OpenMainDataSet(sbConndition.ToString());
                    //����¼������
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                    optMain = OperateType.optNone;
                    DisplayState(stbState, optMain);
                    CtrlControlReadOnly(pnlEdit, false);
                }
                else
                {
                    MessageBox.Show("��������ʧ�ܣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("�Բ��𣬵�ǰû�д��ڱ༭״̬��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
         #endregion

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
            btnQry_Click(sender, e);
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            sbConndition.Remove(0, sbConndition.Length);
            sbConndition.Append(strFix);
            if (txtFindName.Text.Trim() != "")
            {
                if (sbConndition.Length > 0)
                {
                    sbConndition.Append(" and cName like '%" + txtFindName.Text.Trim() + "%'");
                }
                else
                {
                    sbConndition.Append(" where cName like '%" + txtFindName.Text.Trim() + "%'");
                }
            }
            OpenMainDataSet(sbConndition.ToString());
        }

        private void frmDept_Load(object sender, EventArgs e)
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

            if (UserInformation.UType != UserType.utSupervisor)
            {
                strFix = " where cCmptId='" + UserInformation.UnitId + "'";
            }
            LoadBaseData();   
            OpenMainDataSet(strFix);
        }

        private void cmb_Compt_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmb_Compt_TextChanged(object sender, EventArgs e)
        {
            if (!bMainlstIsOpened) return;
            if (cmb_Compt.Items.Count == 0) return;
            if (cmb_Compt.SelectedValue == null) return;
            if (cmb_Compt.Text.Trim().Length == 0)
            {
                OpenMainDataSet(strFix);
            }
            else
            {
                if (cmb_Compt.SelectedValue != null)
                {
                    string sX = cmb_Compt.SelectedValue.ToString();
                    OpenMainDataSet(" where cCmptId='" + sX.Trim() + "'");
                }
                else
                {
                    OpenMainDataSet(strFix);
                }
            }
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

    }
}

