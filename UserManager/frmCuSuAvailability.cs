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
    public partial class frmCuSuAvailability : UI.FrmSTable
    {
        #region ˽�б���

        string strTbNameMain = "TPB_CuSuAvailability";
        string strKeyFld = "cCSId,cItemName";
        string strConnFix = "";
        //�������
        OperateType optMain = OperateType.optNone;
        //��¼��ǰ�����б�� ����
        StringBuilder sbConndition = new StringBuilder("");
        bool bIsMainOpened = false;
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

        private void DisplayCSInfo(string sCSId)
        {
            string sErr = "";
            string sSql = "select * from TPB_CUSUPPLIER where CCSID='"+ sCSId.Trim() +"' ";
            DataSet dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "TPB_CUSUPPLIER", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbX = dsX.Tables["TPB_CUSUPPLIER"] ;
            if (tbX != null )
            {
                ClearUIValues(grp_CSInfo);
                DataRowView drv = null ;
                if (tbX.DefaultView.Count > 0)
                {
                    drv = tbX.DefaultView[0];
                    DataRowViewToUI(drv, grp_CSInfo);
                }
            }
        }

        private void LoadItemToComb()
        {
            string sErr = "";
            string sSql = "select distinct cItemName from TPB_CuSuAvailability ";
            DataSet dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "cItemName", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            cmb_cItemName.Items.Clear();
            cmb_cItemName.DisplayMember = "cItemName";
            cmb_cItemName.DataSource = dsX.Tables["cItemName"];
            dsX.Clear();
            dsX = null;
        }
        #endregion
        #region ��������

            private string _CSId = "";
            [Description("��Ӧ�̻�ͻ����")]
            public string CSId
            {
                get { return _CSId; }
                set {
                    _CSId = value.Trim();
                    strConnFix = " and cCSId='"+ _CSId.Trim() +"'";
                }
            }

        private int _CSType = 0;
        public int CSType
        {
            get { return _CSType; }
            set
            {
                _CSType = value;
                switch (_CSType)
                {
                    case 0:
                        Text = "��Ӧ��/��������" + "�����ʺϷ���ά��";
                        lbl_CSTypeDesc.Text = "��Ӧ��/��������";
                        break;
                    case 1:
                        Text = "�ͻ�" + "�����ʺϷ���ά��";
                        break;
                    default :
                        Text = "��Ӧ��/��������" + "�����ʺϷ���ά��";
                        break;
                }
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
        public bool OpenMainDataSet()
        {
            //if (UserInformation.UType != UserType.utSupervisor)
            //{
            //    strConnFix = " and cCmptId='" + UserInformation.UnitId + "'";
            //}
            bool bIsOK = false;
            string strX = "";
            string sErr = "";
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            string sSql = "select * from " + strTbNameMain + " where 1=1 and cCSId='" + _CSId.Trim() + "'";
            DBDataSet = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, strTbNameMain, 0, 0, "", out sErr);
            #region
            //DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            ////cmdInfo.SqlText = "select * from " + strTbNameMain + " where 1=1 " + sCondition + strConnFix;             //SQL���  �� �洢������ ���в����������ڲ�����������
            //cmdInfo.SqlText = "select * from " + strTbNameMain + " where 1=1 and cCSId='"+ _CSId.Trim() +"'";             //SQL���  �� �洢������ ���в����������ڲ�����������

            ////if (UserInformation.UType != UserType.utSupervisor)
            ////{
            ////    cmdInfo.SqlText = cmdInfo.SqlText + " where "+strKeyFld+"='"+ UserInformation.UnitId +"'"; //����ʾ��ǰ�û��ĵ�λ
            ////}
            //cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            //cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            //cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            //cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������
            //SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            ////sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            
            ////DataSet dsX = null;
            ////DataTable tbX= null ;
            //DBDataSet = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
            #endregion

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
                    ClearUIValues(pnlEdit);
                    bIsMainOpened = false;
                    this.bdsMain.DataSource = DBDataSet.Tables[strTbNameMain];
                    bIsMainOpened = true;
                    BindDataSetToCtrls();
                    grdList.DataSource = bdsMain;  
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
        public void DoMNew()
        {
            //if (UserInformation.UType != UserType.utSupervisor)
            //{
            //    MessageBox.Show("�Բ�������Ȩ���½���");
            //    return;
            //}
            optMain = OperateType.optNew;
            try
            {
                DataRowView drv = (DataRowView)bdsMain.AddNew();
                //��ʼ���ֶ�����(Ĭ��ֵ)
                drv["cCSId"] = _CSId.Trim();
                drv["cCmptId"] = UserInformation.UnitId;
                drv["cUser"] = UserInformation.UserName;
                DataRowViewToUI(drv, pnlEdit);
                //����¼������
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                this.txt_cItemValue.Focus();
                DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, true);
              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
        public void DoMEdit()
        {

            optMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsMain.Current;
            //��ʼ���ֶ�����(Ĭ��ֵ)
            drv.BeginEdit();
            //����¼������
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cItemValue.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
     
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
                OpenMainDataSet();
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
            this.cmb_cItemName.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (cmb_cItemName.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ����������Ʋ���Ϊ�գ�");
                    cmb_cItemName.Focus();
                    return;
                }
                if (txt_cItemValue.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ������ʱ��벻��Ϊ�գ�");
                    txt_cItemValue.Focus();
                    return;
                }
                if (this.dtp_dBeginDate.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ�����Ч���ڲ���Ϊ�գ�");
                    dtp_dBeginDate.Focus();
                    return;
                }
                if (this.dtp_dEndDate.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ��𣬽�ֹ���ڲ���Ϊ�գ�");
                    dtp_dEndDate.Focus();
                    return;
                }
                try
                {
                    UIToDataRowView(drvX, pnlEdit);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                if (optMain == OperateType.optNew) //��������Ҫ�����µĺ���
                {
                    //drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, UserInformation.UnitId.Trim().Length + 4, UserInformation.UnitId.Trim());
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, true);//���� insert ���

                }
                else
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, false);//����UPDATE ���
                bool bX = false;
                if (drvX.IsEdit) drvX.EndEdit();
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = sSql;             //SQL���  �� �洢������ ���в����������ڲ�����������
                cmdInfo.FldsData = DBCommInfo.DBSQLCommandInfo.GetFieldsForDate(drvX);
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
                    OpenMainDataSet();
                    //����¼������
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                    optMain = OperateType.optNone;
                    DisplayState(stbState, optMain);
                    CtrlControlReadOnly(pnlEdit, false);
                    LoadItemToComb();
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

        public frmCuSuAvailability()
        {
            InitializeComponent();
        }

        private void frmCuSuAvailability_Load(object sender, EventArgs e)
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
            LoadItemToComb();
            DisplayCSInfo(_CSId.Trim());
            OpenMainDataSet();
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

            #endregion
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            DoMEdit();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            DoMNew();
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
            OpenMainDataSet();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (!bIsMainOpened) return;
            if (bdsMain == null) return;
            DataRowView drv = null;
            try
            {
                drv=(DataRowView)bdsMain.Current;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
            if (drv == null) return;
            if (!drv.IsNew)
            {
                DataRowViewToUI(drv, pnlEdit);
            }
            else
            {
                ClearUIValues(pnlEdit);
            }
        }
    }
}

