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
using System.Collections;

namespace UserMS
{
    public partial class frmUserInfo : UI.FrmSTable
    {
        public frmUserInfo()
        {
            InitializeComponent();
        }
        #region ˽�б���

        int iPwdLength = 0;
        string strTbNameMain = "TPB_User";
        string strKeyFld = "cUserId";
        bool bDSIsOpenForMain = false;
        bool bCmptIsOpened = false;
        bool bDeptisOpened = false;
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
        private void LoadBaseItemFromArr()
        {
            //����״̬
            ArrayList ArrUseState = new ArrayList(); //�Ƿ�����
            ArrUseState.Add(new DictionaryEntry(0, "δ����"));
            ArrUseState.Add(new DictionaryEntry(1, "������"));
            ////

            cmb_bUsed.DisplayMember = "Value";
            cmb_bUsed.ValueMember = "Key";
            cmb_bUsed.DataSource = ArrUseState;

            ////��ϸִ��״̬
            ArrayList ArrTag = new ArrayList(); //�û�����0:��ͨ�û� 1:����Ա�û� 2:��������Ա�û�
            ArrTag.Add(new DictionaryEntry(0, "��ͨ�û�"));
            ArrTag.Add(new DictionaryEntry(1, "����Ա"));
            ArrTag.Add(new DictionaryEntry(2, "��������Ա"));
            ////

            cmb_nTag.DisplayMember = "Value";
            cmb_nTag.ValueMember = "Key";
            cmb_nTag.DataSource = ArrTag;
        }

        private void LoadDeptList(string strCmptId)
        {
            string sErr = "";
            string strSql = "select * from TPB_Dept";
            if (strCmptId.Trim() != "")
            {
                strSql += " where cCmptId='" + strCmptId + "'";
            }
            bDeptisOpened = false;
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out sErr); //UserManager.GetDataSetbySql(sql);

            cmb_Dept.DataSource = ds1.Tables["data"];
            cmb_Dept.DisplayMember = "cName";
            cmb_Dept.ValueMember = "cDeptId";
            bDeptisOpened = true;
            if (cmb_Dept.Items.Count > 0)
            {
                switch (UserInformation.UType)
                {
                    case UserType.utNormal:
                        cmb_Dept.SelectedValue = UserInformation.DeptId.Trim();
                        cmb_Dept.Enabled = false;
                        break;
                    case UserType.utAdmin:
                        cmb_Dept.SelectedValue = UserInformation.DeptId.Trim();
                        cmb_Dept.Enabled = false;
                        break;
                    case UserType.utSupervisor:
                        cmb_Dept.SelectedIndex = 0;
                        cmb_Dept.Enabled = true;
                        break;
                    default:
                        cmb_Dept.SelectedIndex = 0;
                        cmb_Dept.Enabled = false ;
                        break;
                }
            }
        }

        private void loadComptList()
        {
            string sErr = "";
            string strSql = "select * from tpb_Compt ";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql = strSql + " where cComptId='"+ UserInformation.UnitId.Trim() +"'";
            }
            bCmptIsOpened = false;
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out sErr); //UserManager.GetDataSetbySql(sql);

            cmb_Compt.DataSource = ds1.Tables["data"];
            cmb_Compt.DisplayMember = "cCmptName";
            cmb_Compt.ValueMember = "cComptId";
            bCmptIsOpened = true;
            if (cmb_Compt.Items.Count > 0)
            {
                switch (UserInformation.UType)
                {
                    case UserType.utNormal:
                        cmb_Compt.SelectedValue = UserInformation.UnitId.Trim();
                        cmb_Compt.Enabled = false;
                        break;
                    case UserType.utAdmin:
                        cmb_Compt.SelectedIndex = 0;
                        cmb_Compt.Enabled = true;
                        break;
                    //case UserType.utSupervisor:
                    //    cmb_Compt.SelectedIndex = 0;
                    //    cmb_Compt.Enabled = true;
                    //    break;
                    default:
                        cmb_Compt.SelectedIndex = 0;
                        cmb_Compt.Enabled = true;
                        break;
                }
            }
        }

        private int GetPwdLen()
        {
            int iLen = 0;
            string sErr = "";
            string strSql = "select * from tps_syspar where cParId='nStrongPwdLen'";
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket,false,strSql,"data",0,0,"",out sErr); //UserManager.GetDataSetbySql(sql);
            if ((sErr.Trim() == "" || sErr.Trim() == "0") && ds1 != null)
            {
                if (ds1.Tables.Count > 0 && ds1.Tables["data"] != null)
                {
                    DataTable tbX = ds1.Tables["data"];
                    if (tbX.Rows.Count > 0)
                    {
                        iLen = int.Parse(tbX.Rows[0]["cParValue"].ToString());
                    }
                }
                ds1.Clear();
            }
            else
            {
                MessageBox.Show(sErr);
            }
            return iLen;
        }

        private void LoadBaseItem()
        {
            loadComptList();
            LoadDeptList(cmb_Compt.SelectedValue.ToString());
            LoadBaseItemFromArr();
            iPwdLength = GetPwdLen();
        }

        private string GetDataCondition()
        {
            StringBuilder sCon = new StringBuilder("");
            if (cmb_Compt.Text.Trim().Length > 0)
            {
                if (cmb_Compt.Items.Count > 0)
                {
                    if (cmb_Compt.SelectedValue != null)
                    {
                        sCon.Append(" where cCmptId='" + cmb_Compt.SelectedValue.ToString().Trim() + "'");
                    }
                }
            }
            if (cmb_Dept.Text.Trim().Length > 0)
            {
                if (cmb_Dept.Items.Count > 0)
                {
                    if (cmb_Dept.SelectedValue != null)                   
                    {
                        if (sCon.Length > 0)
                        {
                            sCon.Append(" and cDeptId='" + cmb_Dept.SelectedValue.ToString().Trim() + "'");
                        }
                        else
                        {
                            sCon.Append(" where cDeptId='" + cmb_Dept.SelectedValue.ToString().Trim() + "'");
                        }
                    }
                }
            }
            switch (UserInformation.UType)
            {
                case UserType.utSupervisor:
                    break;
                case UserType.utAdmin:
                    //sCon.Append(" and cUserId in (select distinct cUserId from TPB_UserMgrArea where cMAreaId in (select distinct cMAreaId from TPB_UserMgrArea where cUserId='" + UserInformation.UserId + "'))");
                    sCon.Append(" and cDeptId='" + UserInformation.DeptId.Trim() + "'");
                    break;
                case UserType.utNormal:
                    sCon.Append(" and cUserId='" + UserInformation.UserId + "'");
                    break;
            }
            string sX =txtFindName.Text.Trim();
            if ( sX.Length > 0)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and ((cName like '%" + sX + "%') or (isnull(cPYJM,' ') like  '%" + sX + "%') or (isnull(cWBJM,' ') like  '%" + sX + "%'))");
                }
                else
                {
                    sCon.Append(" where ((cName like '%" + sX + "%') or (isnull(cPYJM,' ') like  '%" + sX + "%') or (isnull(cWBJM,' ') like  '%" + sX + "%'))");
                }
            }            
            return sCon.ToString();

        }
        #endregion

        #region ��������

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
            ////�������
            //DataSetUnBind(pnlEdit);
            ////�����ݼ�
            //DataSetBind(pnlEdit, this.bdsMain);
            grdList.DataSource = null;
            grdList.DataSource = bdsMain;
        }
        public  override void DisplayState(ToolStripLabel stbSt, OperateType optX)
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

        public bool OpenMainDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            string sSql = "";
            string sErr = "";
            bDSIsOpenForMain = false;
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            sSql = "select * from " + strTbNameMain + sCon;             //SQL���  �� �洢������ ���в����������ڲ�����������      
            DBDataSet = SunEast.App.PubDBCommFuns.GetDataBySql(sSql,strTbNameMain,0,0,out sErr);
            bIsOK = DBDataSet != null;
            if(bIsOK)
            {
                bIsOK = DBDataSet.Tables[0].Rows[0][0].ToString().Trim() == "0";
            }
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
                    bDSIsOpenForMain = true;
                    this.bdsMain.DataSource = DBDataSet.Tables[strTbNameMain];
                    BindDataSetToCtrls();
                    ClearUIValues(pnlEdit);
                    if (bdsMain.Count > 0)
                    {
                        DataRowViewToUI((DataRowView)bdsMain.Current, pnlEdit);
                    }
                    bIsOK = true;
                    optMain = OperateType.optNone;
                    btn_SetPwd.Visible = false;
                    btn_SetPwd.Enabled = false;
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
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("�Բ�������Ȩ���½��û���Ϣ��");
                return;
            }
            if (cmb_Compt.SelectedValue == null)
            {
                MessageBox.Show("�Բ��𣬵�λ����Ϊ�գ�");
                return;
            }
            if (cmb_Dept.SelectedValue == null)
            {
                MessageBox.Show("�Բ��𣬲��Ų���Ϊ�գ�");
                return;
            }
            optMain = OperateType.optNew;
            int iPos = bdsMain.Position;
            DataRowView drv = (DataRowView)bdsMain.AddNew();
            
            iPos = bdsMain.Position;
            //bdsMain.MoveLast();
            //drv = (DataRowView)bdsMain.Current;
            //��ʼ���ֶ�����(Ĭ��ֵ)
            bool bOK = drv.IsEdit;
            bOK = drv.IsNew;
            drv["cUserId"] = "-1";
            drv["nTag"] = 0;
            drv["bUsed"] = 0;
            drv["cCmptId"] = cmb_Compt.SelectedValue.ToString().Trim();
            drv["cDeptId"] = cmb_Dept.SelectedValue.ToString().Trim();
            drv["dCreateDate"] = DateTime.Now;
            drv["cCreator"] = UserInformation.UserName;
            drv["nSort"] = 0;
            DataRowViewToUI(drv, pnlEdit);
            //
            txt_cUserId.Text = drv["cUserId"].ToString();
            txt_cCmptId.Text = drv["cCmptId"].ToString();
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cUserId.ReadOnly = true;
            btn_SetPwd.Visible = true;
            btn_SetPwd.Enabled = true;
        }
        public void DoMEdit()
        {
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
            txt_cUserId.ReadOnly = true;
            btn_SetPwd.Visible = true;
            btn_SetPwd.Enabled = true;
        }
        public void DoMUndo()
        {
            optMain = OperateType.optUndo;
            DataRowView drv = null; 
            drv = (DataRowView)bdsMain.Current;
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
            drv = (DataRowView)bdsMain.Current;
            this.DataRowViewToUI(drv, pnlEdit);
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            optMain = OperateType.optNone;
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, false);
            btn_SetPwd.Visible = false;
            btn_SetPwd.Enabled = false;          
        }
        public void DoMDelete()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("�Բ�������Ȩ��ɾ���û���Ϣ��");
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
            bool bX = false;
            string sErr = "";
            string sSql = "delete " + strTbNameMain + " where " + strKeyFld + "='" + drv[strKeyFld].ToString() + "'";   
            DataSet dsX = null;
            //ִ�����
            dsX = SunEast.App.PubDBCommFuns.GetDataBySql(sSql, out sErr);
            bX = dsX != null;
            bX = dsX.Tables[0].Rows[0][0].ToString() == "0";
            if (bX)
            {
                MessageBox.Show("ɾ���ɹ���");
                optMain = OperateType.optDelete;
                OpenMainDataSet(sbConndition.ToString());
                //����¼������
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                optMain = OperateType.optNone;
                DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, false);
                btn_SetPwd.Visible = false;
                btn_SetPwd.Enabled = false;
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
            txt_cUserId.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            DataRowView drvX = (DataRowView)bdsMain.Current;
            
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (this.txt_cCmptId.Text.Trim() == "")
                {
                    MessageBox.Show("��λ���벻��Ϊ�գ�");
                    txt_cCmptId.Focus();
                    return;
                }
                if (this.txt_cName.Text.Trim() == "")
                {
                    MessageBox.Show("�û����Ʋ���Ϊ�գ�");
                    txt_cName.Focus();
                    return;
                }
                if (this.cmb_bUsed.Text.Trim() == "")
                {
                    MessageBox.Show("�Ƿ����ò���Ϊ�գ�");
                    cmb_bUsed.Focus();
                    return;
                }
                if (this.cmb_nTag.Text.Trim() == "")
                {
                    MessageBox.Show("�û����Ͳ���Ϊ�գ�");
                    cmb_nTag.Focus();
                    return;
                }
                if (this.txt_nSort.Text.Trim() == "")
                {
                    MessageBox.Show("����Ų���Ϊ�գ�");
                    txt_nSort.Focus();
                    return;
                }
                int iX = (int)UserInformation.UType;
                if (cmb_nTag.SelectedIndex > iX)
                {
                    MessageBox.Show("�Բ��𣬵�ǰ�û���Ȩ�޳���������Ȩ�޷�Χ������ѡ�û����ͣ�");
                    cmb_nTag.Focus();
                    return;
                }
                UIToDataRowView(drvX, pnlEdit);
                //����ƴ���������ʼ���
                string sX = "";
                if (drvX["cName"] != null)
                {
                    sX = drvX["cName"].ToString();
                    sX = Zqm.Text.TextPYWB.GetWBPY(sX, PYWBType.pwtPYFirst);
                }
                if (drvX["cPYJM"] != null)
                {
                    drvX["cPYJM"] = sX;
                }
                sX = "";
                if (drvX["cName"] != null)
                {
                    sX = drvX["cName"].ToString();
                    sX = Zqm.Text.TextPYWB.GetWBPY(sX, PYWBType.pwtWBFirst);
                }
                if (drvX["cWBJM"] != null)
                {
                    drvX["cWBJM"] = sX;
                }
                if (drvX[strKeyFld].ToString() == "" || drvX[strKeyFld].ToString() == "-1") //��������Ҫ�����µĺ���
                {
                    drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, 8, drvX["cDeptId"].ToString().Trim());
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, true);//���� insert ���
                }
                else
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, false);//����UPDATE ���
                bool bX = false;
                
                //���������
                
                string sErr = "";
                if (!CheckInputDataValues(drvX, pnlEdit, "cUserId,cName,bUsed,nTag,nSort", out sErr))
                {
                    MessageBox.Show(sErr);
                    return;
                }
                if (drvX.IsEdit) drvX.EndEdit();
                DataSet dsX = null;
                //ִ�����
                dsX =SunEast.App.PubDBCommFuns.GetDataBySql(sSql, DBSQLCommandInfo.GetFieldsForDate(drvX), out sErr);
                //dsX = SunEast.App.PubDBCommFuns.GetDataBySql(sSql, out sErr);
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

        private void btnQry_Click(object sender, EventArgs e)
        {
            sbConndition.Remove(0, sbConndition.Length);
            sbConndition.Append(GetDataCondition());
            OpenMainDataSet(sbConndition.ToString());
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
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

            string sCon = "";
            LoadBaseItem();
            sCon = GetDataCondition();
            OpenMainDataSet(sCon);
        }

        

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (!bDSIsOpenForMain) return;
            DataRowView drv = (DataRowView)bdsMain.Current;
            ClearUIValues(pnlEdit);
            if (drv != null)
            {
                if (!drv.IsNew)
                    DataRowViewToUI(drv, pnlEdit);
            }
        }

        //private void cmb_Compt_TextChanged(object sender, EventArgs e)
        //{
        //    string sX = "";
        //    if (!bCmptIsOpened) return;
        //    if (cmb_Compt.Items.Count > 0)
        //    {
        //        if (cmb_Compt.SelectedValue != null)
        //        {
        //            sX = cmb_Compt.SelectedValue.ToString().Trim();
        //        }
        //    }
        //    LoadDeptList(sX);
        //}

        private void cmb_Dept_TextChanged(object sender, EventArgs e)
        {
            string sX = "";
            if (!bDeptisOpened) return;
            btnQry_Click(sender, e);
        }

        private void cmb_nTag_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_SetPwd_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null) return;
            if (drv.IsNew == false && drv.IsEdit == false)
            {
                MessageBox.Show("�Բ���û�д��ڱ༭״̬�������������룡");
                return;
            }
            frmSetUserPwd frmX = new frmSetUserPwd();            
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.PwdLen = iPwdLength;
            frmX.ShowDialog();
            if (frmX.IsOK)
            {
                drv["cPwd"] = frmX.NewPwdValue;
                MessageBox.Show("�������óɹ�����Ҫ�������Ч��");
            }
            frmX.Dispose();

        }

        private void cmb_Compt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sX = "";
            if (!bCmptIsOpened) return;
            if (cmb_Compt.Items.Count > 0)
            {
                if (cmb_Compt.SelectedValue != null)
                {
                    sX = cmb_Compt.SelectedValue.ToString().Trim();
                }
            }
            LoadDeptList(sX);
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

        private void btn_SaveGridColum_Click(object sender, EventArgs e)
        {
            string sMyClassName = "";
            sMyClassName =  this.GetType().Namespace ;
            
            MessageBox.Show(sMyClassName);
           
        }
    }
}

