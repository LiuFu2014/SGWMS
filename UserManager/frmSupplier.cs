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
    /// <summary>
    /// �ͻ���Ӧ������-1��ȫ�� 0����Ӧ�� 1���ͻ�
    /// </summary>
    public enum CSType { cstAll=-1, cstSupplier = 0,cstCustomer = 1 };

    public partial class frmSupplier : UI.FrmSTable
    {

        public static int type;
        public static string sCondition = "";
        public frmSupplier()
        {
            InitializeComponent();
        }
        #region ˽�б���

        string strTbNameMain = "TPB_CuSupplier";
        string strKeyFld = "cCsId";
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
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strConnFix = " and cCmptId='" + UserInformation.UnitId + "'";
            }
            bool bIsOK = false;
            string strX = "";
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "select * from " + strTbNameMain + " where 1=1 "+sCondition+strConnFix;             //SQL���  �� �洢������ ���в����������ڲ�����������
            //if (UserInformation.UType != UserType.utSupervisor)
            //{
            //    cmdInfo.SqlText = cmdInfo.SqlText + " where "+strKeyFld+"='"+ UserInformation.UnitId +"'"; //����ʾ��ǰ�û��ĵ�λ
            //}
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

                    //DBDataSet.Tables["data"].TableName = strTbNameMain;                   
                    //DataSetUnBind(pnlEdit);
                    ClearUIValues(pnlEdit);
                    bIsMainOpened = false;
                    this.bdsMain.DataSource = DBDataSet.Tables[strTbNameMain];
                    bIsMainOpened = true;                    
                    grdList.DataSource = bdsMain;
                    bdsMain_PositionChanged(null, null);
                    //if (bdsMain != null && bdsMain.Count > 0)
                    //{
                    //    DataRowView drv = (DataRowView)bdsMain.Current;
                    //    object objX = drv["cCmptId"];
                    //    //DataRowViewToUI(drv, pnlEdit);
                    //}
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
                drv["nType"] = type;
                drv["cCmptId"] = UserInformation.UnitId;
                DataRowViewToUI(drv, pnlEdit);
                //����¼������
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                this.txt_cCSNameJ.Focus();
                DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, true);
                txt_cCSId.ReadOnly = true;
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
            this.txt_cCSNameJ.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cCSId.ReadOnly = true;
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
            txt_cCSId.Focus();//ʹ�佹���ƿ�,�޸������ܼ�ʱ����
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (this.txt_cCSNameJ.Text.Trim() == "")
                {
                    MessageBox.Show("��Ʋ���Ϊ�գ�");
                    txt_cCSNameJ.Focus();
                    return;
                }
                if (this.cmb_bUsed.Text.Trim() == "")
                {
                    MessageBox.Show("�Ƿ����ò���Ϊ�գ�");
                    cmb_bUsed.Focus();
                    return;
                }
                if (this.cmb_nIsInner.Text.Trim() == "")
                {
                    MessageBox.Show("�Ƿ��ڲ���λ����Ϊ�գ�");
                    cmb_nIsInner.Focus();
                    return;
                }
                if (this.cmb_nIsFactory.Text.Trim() == "")
                {
                    MessageBox.Show("�Ƿ��������Ҳ���Ϊ�գ�");
                    cmb_nIsFactory.Focus();
                    return;
                }                
                UIToDataRowView(drvX, pnlEdit);
                //
                string sName = txt_cCSNameJ.Text.Trim();
                string sX = "";
                sX = Zqm.Text.TextPYWB.GetWBPY(sName, PYWBType.pwtPYFirst);
                if ((!drvX.IsEdit) && (!drvX.IsNew))
                drvX.BeginEdit();
                drvX["cPYJM"] = sX;
                sX = Zqm.Text.TextPYWB.GetWBPY(sName, PYWBType.pwtWBFirst);
                drvX["cWBJM"] = sX;
                //drvX.EndEdit();
                if (drvX[strKeyFld].ToString() == "" || drvX[strKeyFld].ToString() == "-1") //��������Ҫ�����µĺ���
                {
                    drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, UserInformation.UnitId.Trim().Length + 4, UserInformation.UnitId.Trim());
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

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            OpenMainDataSet();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            string sErr = "";
            #region Ȩ�޿���
            tlbSaveSysRts.Visible = UserInformation.UserName == "Admin5118";
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
            //�Զ����¿ͻ��͹�Ӧ�̵Ŀ�����
            string sX = DBFuns.sp_UpdateCuSupplyerUseful(AppInformation.SvrSocket , out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show("�Զ����¿ͻ�����Ӧ�̺Ϸ���ʱ����" + sErr);
            }
            else
            {
                if (sX.Trim() != "0")
                {
                    MessageBox.Show("������" + sX + " �� �����ÿͻ���Ӧ�̣����������̣�");
                }
            }
            OpenMainDataSet();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (!bIsMainOpened) return;
            DataRowView drv = (DataRowView)bdsMain.Current;

            if (drv!= null && !drv.IsNew)
            {
                DataRowViewToUI(drv, pnlEdit);
            }
            else
            {
                ClearUIValues(pnlEdit);
            }
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            DoMSave();
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

        private void tlb_M_Availability_Click(object sender, EventArgs e)
        {
            if (bdsMain.Count == 0)
            {
                MessageBox.Show("�Բ��������ݣ�");
                return;
            }
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null)
            {
                MessageBox.Show("�Բ���û��ѡ�����ݣ�");
                return;
            }
            frmCuSuAvailability frmX = new frmCuSuAvailability();
            frmX.ModuleRtsId = ModuleRtsId + tlb_M_Availability.Tag.ToString();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.CSId = drv["cCSId"].ToString();
            frmX.CSType = Convert.ToInt16(drv["nType"]);
            frmX.ShowDialog();
            frmX.Dispose();
            frmX = null;
        }

        private void grdList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (grdList.Rows.Count > 0)
            {
                object objX = null ;
                foreach (DataGridViewRow grdr in grdList.Rows)
                {
                    objX = grdr.Cells["colbUsed"].Value ;
                    if (objX != null && objX.ToString() =="0")
                    {
                        grdr.DefaultCellStyle.BackColor = Color.Red ;
                    }
                    else
                    {
                        grdr.DefaultCellStyle.BackColor = Color.Green;
                    }                    
                }
            }
        }

        private void tlb_M_AutoUpdateUseful_Click(object sender, EventArgs e)
        {
            
        }
    }
}

