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
    public partial class FrmStockMCheck : UI.FrmSTable
    {
        public FrmStockMCheck()
        {
            InitializeComponent();
        }
        #region ˽�б���
        string strTbNameMain = "TWB_BillCheck";
        string strKeyFld = "cBNo";
        string strTbNameDetail = "TWB_BillCheckDtl";
        string strTbNameList = "TWB_BillCheckList";
        string strKeyFldDetail = "cBNo";
        DataSet DBDateSetDetail = null;
        DataSet DBDateSetList = null;
        bool bDSIsOpenForMain = false;
        OperateType OptMain = OperateType.optNone;
        OperateType OptDetail= OperateType.optNone;
        StringBuilder sbCondition = new StringBuilder("");
        #endregion

        private void LoadCommboxItemByValue()
        {
            ArrayList ArrStatus = new ArrayList();
            ArrayList ArrStatus1 = new ArrayList();
            ArrStatus.Add(new DictionaryEntry(0, "δ�̵�"));
            ArrStatus.Add(new DictionaryEntry(1, "�̵���"));
            ArrStatus.Add(new DictionaryEntry(2, "�̵�Ǽ����"));
            ArrStatus.Add(new DictionaryEntry(3, "�̵����"));
            //--
            ArrStatus1.Add(new DictionaryEntry(0, "δ�̵�"));
            ArrStatus1.Add(new DictionaryEntry(1, "�̵���"));
            ArrStatus1.Add(new DictionaryEntry(2, "�̵�Ǽ����"));
            ArrStatus1.Add(new DictionaryEntry(3, "�̵����"));

            comboBox_nStatus.DisplayMember = "Value";
            comboBox_nStatus.ValueMember = "Key";
            comboBox_nStatus.DataSource = ArrStatus;
            //--
            cmbQ_nState.DisplayMember = "Value";
            cmbQ_nState.ValueMember = "Key";
            cmbQ_nState.DataSource = ArrStatus1;
            cmbQ_nState.SelectedValue = 0;

            ArrayList ArrIsChecked= new ArrayList();
            ArrIsChecked.Add(new DictionaryEntry(0, "δ���"));
            ArrIsChecked.Add(new DictionaryEntry(1, "�����"));

            comboBox_bIsChecked.DisplayMember = "Value";
            comboBox_bIsChecked.ValueMember = "Key";
            comboBox_bIsChecked.DataSource = ArrIsChecked;

        }

        private void LoadStockList(string StockId)
        {
            string errStr = "";
            string sqlStr = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (StockId.Trim() != "")
            {
                sqlStr += " where cWHId='" + StockId + "'";
            }
            if (UserInformation.UType != UserType.utSupervisor)
            {
                sqlStr += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            DataSet ds = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket,false, sqlStr, out errStr); //UserManager.GetDataSetbySql(sql);
            DataTable tbX = ds.Tables["data"];
            comboBox_cWHId.DataSource = tbX ;            
            comboBox_cWHId.DisplayMember = "cName";
            comboBox_cWHId.ValueMember = "cWHId";
            //
            DataTable tb2 = tbX.Copy();
            this.cmbQ_Ware.DataSource = tb2;
            cmbQ_Ware.DisplayMember = "cName";
            cmbQ_Ware.ValueMember = "cWHId";
            cmbQ_Ware.SelectedIndex = -1;
        }

        private void LoadCheckType(string TypeId)
        {
            string errStr = "";
            string sqlStr = "select cBTypeId,cBType  from  TPB_BillType where  nBClass=3";
            if (TypeId.Trim() != "")
            {
                sqlStr += " where cBTypeId='" + TypeId + "'";
            }
            DataSet ds = PubDBCommFuns.GetDataBySql(sqlStr, out errStr); //UserManager.GetDataSetbySql(sql);
            DataTable tbX = ds.Tables["data"] ;
            comboBox_cCheckType.DataSource = tbX;
            comboBox_cCheckType.DisplayMember = "cBType";
            comboBox_cCheckType.ValueMember = "cBTypeId";
            //
            DataTable tb2 = tbX.Copy();
            cmbQ_CheckType.DataSource = tb2;
            cmbQ_CheckType.DisplayMember = "cBType";
            cmbQ_CheckType.ValueMember = "cBTypeId";
            cmbQ_CheckType.SelectedIndex = -1;
        }

        public bool  DoNew()
        {
            bool  Result = false;
            OptMain = OperateType.optNew;
            DataRowView drv = (DataRowView)bindingSource_Main.AddNew();
            CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
            DisplayState(stbState, OptMain);
            CtrlControlReadOnly(panel_Edit, true);
            return Result;

            /*
            optMain = OperateType.optNew;
            DataRowView drv = (DataRowView)bdsMain.AddNew();
            bool bOK = drv.IsEdit;
            bOK = drv.IsNew;
            drv["cUserId"] = "-1";
            drv["nTag"] = 0;
            drv["bUsed"] = 0;
            drv["cCmptId"] = UserInformation.UnitId;
            drv["dCreateDate"] = DateTime.Now;
            drv["cCreator"] = UserInformation.UserName;
            //
            txt_cUserId.Text = drv["cUserId"].ToString();
            txt_cCmptId.Text = drv["cCmptId"].ToString();
            //����¼������     
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cUserId.ReadOnly = true;
            btn_SetPwd.Enabled = true;
             */
        }
        private bool DoEdit()
        {
           if (dataGridView_Main.RowCount<2) return true;
            bool Result = false;
            string sqlStr = "";
            string errStr = "";

            OptMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            drv.BeginEdit();
            CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
            CtrlControlReadOnly(panel_Edit, true);
            return Result;
        }
        private bool DoEditDetail()
        {
            if (grdList.RowCount < 2) return true;
            bool Result = false;
            string sqlStr = "";
            string errStr = "";

            OptDetail = OperateType.optEdit;
            DataRowView drv = (DataRowView)bindingSource_Detail.Current;
            if (drv == null) return false;
            drv.BeginEdit();
           // CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
            //CtrlControlReadOnly(panel_Edit, true);
            return Result;
        }



        private bool DoDelete()
        {
            bool Result = false;
            if (bindingSource_Main.Count == 0)
            {
                MessageBox.Show("���̵㵥���ݿ�ɾ����");
                return false ;
            }
            int i = -1;
            i = (int)OptMain;
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            if (drv == null)
            {
                Result = true;
                MessageBox.Show("�Բ���,�����ݿ�ɾ��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return Result;
            }
            if (i >0 && i < 3)
            {
                MessageBox.Show("�Բ���,��ǰ�����ڱ༭/�½�״̬,���ȱ����ȡ������!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Result = true;
                return Result;
            }
            if (MessageBox.Show("ϵͳ������ɾ�������ݣ����ָܻ�����ȷ��Ҫɾ����������", "WMS",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.No) return Result;
            if (int.Parse(drv["nStatus"].ToString()) > 0)
            {
                MessageBox.Show("�Բ��𣬸��̵㵥�Ѿ���ʼ�̵㣬����ɾ����");
                return false;
            }
            //���ô洢����ɾ�������Ա�����־
            bool b = true ;
            string sErr = "";
            string sX = PubDBCommFuns.sp_Chk_DelChkB(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, "WMS", drv["cBNo"].ToString().Trim(), out sErr);
            if (sX.Trim() != "" && sX.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
                return false ;
            }
                        
            if (b)
            {
                OptMain = OperateType.optDelete;
                DoRefresh();
                CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
                OptMain = OperateType.optNone;
                DisplayState(stbState, OptMain);
                CtrlControlReadOnly(panel_Edit, false);
                MessageBox.Show("ɾ���ɹ���");
            }
            return Result;
            /*
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
            dsX = PubDBCommFuns.GetDataBySql(sSql, out sErr);
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
                btn_SetPwd.Enabled = false;
            }
            else
            {
                MessageBox.Show("�Բ���,ɾ������ʧ��!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
             */
        }

        private bool DoUndo()
        {
            if (dataGridView_Main.RowCount < 2) return true;
            bool Result = false;
            OptMain = OperateType.optUndo;
            DataRowView drv = null;
            drv = (DataRowView)bindingSource_Main.Current;
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
            else return Result;
            DBDataSet.Tables[strTbNameMain].AcceptChanges();
            drv = (DataRowView)bindingSource_Main.Current;
            this.DataRowViewToUI(drv, panel_Edit);
            //����¼������
            CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
            OptMain = OperateType.optNone;
            DisplayState(stbState, OptMain);
            CtrlControlReadOnly(panel_Edit, false);
            return Result;
   
        }
        private bool DoSave()
        {
            if (dataGridView_Main.RowCount < 2) return true;
            bool Result = false;
            string sqlStr="";
            string errStr="";
           
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            if (drv.IsEdit || drv.IsNew)
            {
                UIToDataRowView(drv, panel_Edit);
                if (drv[strKeyFld].ToString()=="")
                {
                    drv[strKeyFld]=PubDBCommFuns.GetNewId(strTbNameMain,strKeyFld,5,UserInformation.UnitId);
                    sqlStr = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drv,strTbNameMain,strKeyFld,true);
                }
                else
                {
                     sqlStr = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drv,strTbNameMain,strKeyFld,false);
                }
                if (drv.IsEdit)
                    drv.EndEdit();
                DataSet ds=null;
                ds = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sqlStr, "dDate,dCreateDate,dCheckDate", out errStr);
                bool b = DBDataSet.Tables[0].Rows[0][0].ToString() == "0";
                if (b)
                {
                    OptMain = OperateType.optSave;
                    try
                    {
                        CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    CtrlControlReadOnly(panel_Edit, false);
                    MessageBox.Show("�������ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OptMain = OperateType.optNone;
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
            return Result;
        }
        private bool DoSaveDetail()
        {
            if (grdList.RowCount < 2) return true;
            bool Result = false;
            string sqlStr = "";
            string errStr = "";

            DataRowView drv = (DataRowView)bindingSource_Detail.Current;
            if (drv.IsEdit || drv.IsNew)
            {
                //UIToDataRowView(drv, panel_Edit);
                if (drv[strKeyFld].ToString() == "")
                {
                    drv[strKeyFld] = PubDBCommFuns.GetNewId(strTbNameDetail, strKeyFld, 5, UserInformation.UnitId);
                    sqlStr = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drv, strTbNameDetail, strKeyFld, true);
                }
                else
                {
                    //sqlStr = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drv, strTbNameDetail, strKeyFld, false);
                    sqlStr = "Update TWB_BillCheckDtl set fRQty=" + drv["fRQty"] +",fDiff="+drv["fQty"]+"-"+drv["fRQty"]+" where cBNo='" + drv["cBNo"] + "' and nPalletId='" + drv["nPalletId"] + "' and cBoxId='" + drv["cBoxId"] + "' and cMNo='" + drv["cMNo"] + "' and cBatchNo='" + drv["cBatchNo"] + "' and nQCStatus='" + drv["nQCStatus"]+"'";

                }
                if (drv.IsEdit)
                    drv.EndEdit();
                DataSet ds = null;
                ds = PubDBCommFuns.GetDataBySql(sqlStr, out errStr);
                bool b = ds.Tables[0].Rows[0][0].ToString() == "0";
                if (b)
                {
                    OptDetail = OperateType.optSave;
                   // CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
                  //  CtrlControlReadOnly(panel_Edit, false);
                    MessageBox.Show("�������ݳɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OptDetail = OperateType.optNone;
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
            return Result;
        }



        private bool DoRefresh()
        {
          //  if (dataGridView_Main.RowCount < 2) return true;
            bool Result=false;
            sbCondition.Remove(0, sbCondition.Length);
            sbCondition.Append(" where 1=1 ");
            /*if (txtFindName.Text.Trim() != "")
            {
                sbConndition.Append(" where cName like '%" + txtFindName.Text.Trim() + "%'"); ;
            }
            OpenMainDataSet(sbConndition.ToString());
              */
            if (textBox_cBNoQ.Text.Trim() != "")
            {
                sbCondition.Append(" and  cBNo like '%" + textBox_cBNoQ.Text.ToString().Trim() + "%'");
            }
            if (cmbQ_Ware.Text.Trim() != "")
            {
                if (cmbQ_Ware.Items.Count > 0 && cmbQ_Ware.SelectedValue != null)
                {
                    sbCondition.Append(" and  cWHId = '" + cmbQ_Ware.SelectedValue.ToString().Trim() + "'");
                }
            }
            if (cmbQ_nState.Text.Trim() != "" && cmbQ_nState.SelectedValue != null)
            {
                sbCondition.Append(" and nStatus =" + cmbQ_nState.SelectedValue.ToString());
            }
            if (this.cmbQ_CheckType.Text.Trim() != "")
            {
                if (cmbQ_CheckType.Items.Count > 0 && cmbQ_CheckType.SelectedValue != null)
                {
                    sbCondition.Append(" and  cCheckType = '" + cmbQ_CheckType.SelectedValue.ToString().Trim() + "'");
                }
            }
            if (chkDate.Checked)
            {
                if (dateTimePicker_From.Value > dateTimePicker_To.Value)
                {
                    MessageBox.Show("�Բ��𣬿�ʼ���ڲ��ܴ��ڽ�ֹ���ڣ�");
                    dateTimePicker_From.Focus();
                    return false ;
                }
                sbCondition = sbCondition.Append(" and  dDate between '" + dateTimePicker_From.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dateTimePicker_To.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            }
            

          //  sbCondition = sbCondition.Append(" and cCheckType like '%" + textBox_cCheckTypeQ.Text.ToString().Trim() + "%'");

            BandDataSet(sbCondition.ToString(), this.dataGridView_Main);
            return Result;
            
        }

        /*
        #region ˽�б���
        string strTbNameMain = "TWC_WareHouse";
        string strKeyFld = "cWHId";
        bool bDSIsOpenForMain = false;
        //�������
        OperateType optMain = OperateType.optNone;
        //��¼��ǰ�����б�� ����
        StringBuilder sbConndition = new StringBuilder("");
        #endregion
         */
        //public

        private bool BandDataSet(string SqlStrConditon,DataGridView FDataGridView)
        {
            bool Result = true;
            string SqlStr = "";
            string ErrStr = "";
            bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            SqlStr = "SELECT * FROM  " + strTbNameMain + " " + SqlStrConditon;
            Cursor.Current = Cursors.WaitCursor;
            DBDataSet = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, SqlStr, strTbNameMain, 0, 0, "dDate,dCreateDate,dCheckDate", out ErrStr);
            Cursor.Current = Cursors.Default;
            Result = DBDataSet != null;
            bindingSource_Main.DataSource = DBDataSet.Tables[strTbNameMain]; ;
            FDataGridView.DataSource = bindingSource_Main;
            if (bindingSource_Main.Count > 0)
            {
                try
                {
                    bDSIsOpenForMain = true;
                    DataRowViewToUI((DataRowView)bindingSource_Main.Current, panel_Edit);
                    OptMain = OperateType.optNone;
                    bindingSource_Main_PositionChanged(null, null);
                }
                catch (Exception e)
                {
                    bDSIsOpenForMain = false;
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Result = false;
                }
            }
            else
            {
                tbcMain_SelectedIndexChanged(null, null);
            }
            return Result;
        }

        private bool BandDataSetDetail(string SqlStrConditon, DataGridView FDataGridView)
        {
            bool Result = true;
            string SqlStr = "";
            string ErrStr = "";
           // bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            SqlStr = "SELECT * FROM   " + strTbNameDetail + " " + SqlStrConditon;
            Cursor.Current = Cursors.WaitCursor;
            DBDateSetDetail = PubDBCommFuns.GetDataBySql( AppInformation.SvrSocket ,false, SqlStr, strTbNameDetail, 0, 0, out ErrStr);
            Cursor.Current = Cursors.Default;
            Result = DBDateSetDetail != null;
            bindingSource_Detail.DataSource = DBDateSetDetail.Tables[strTbNameDetail]; ;
            FDataGridView.DataSource = bindingSource_Detail;
            if (bindingSource_Detail.Count > 0)
            {
                try
                {
                  //  bDSIsOpenForMain = true;
                    //DataRowViewToUI((DataRowView)bindingSource_Main.Current, panel_Edit);
                   // OptMain = OperateType.optNone;
                }
                catch (Exception e)
                {
                  //  bDSIsOpenForMain = false;
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Result = false;
                }
            }
            return Result;
        }

        private bool BandDataSetList(string SqlStrConditon, DataGridView FDataGridView)
        {
            bool Result = true;
            string SqlStr = "";
            string ErrStr = "";
            // bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //SqlStr = "SELECT t.*,(isnull(t.fQty,0)-isnull(t.fErpQty,0)) fSysDiff,(isnull(t.fQty,0)+isnull(t.fDiff,0) ) fRQty FROM  " + strTbNameList +" t " + SqlStrConditon;
            SqlStr = "SELECT t.*,(isnull(t.fQty,0)-isnull(t.fErpQty,0)) fSysDiff,fRQty FROM  " + strTbNameList + " t " + SqlStrConditon;
            Cursor.Current = Cursors.WaitCursor;
            DBDateSetList = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, SqlStr, strTbNameList, 0, 0, out ErrStr);
            Cursor.Current = Cursors.Default;
            Result = DBDateSetList != null;
            this.bdsList.DataSource = DBDateSetList.Tables[strTbNameList]; ;
            FDataGridView.DataSource = bdsList;
            if (bindingSource_Detail.Count > 0)
            {
                try
                {
                    //  bDSIsOpenForMain = true;
                    //DataRowViewToUI((DataRowView)bindingSource_Main.Current, panel_Edit);
                    // OptMain = OperateType.optNone;
                }
                catch (Exception e)
                {
                    //  bDSIsOpenForMain = false;
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Result = false;
                }
            }
            return Result;
        }

        private void FrmStockInfo_Load(object sender, EventArgs e)
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

            LoadCommboxItemByValue();
            LoadStockList("");
            LoadCheckType("");
            dateTimePicker_From.Value = DateTime.Now.AddDays(-30);
            BandDataSet("", this.dataGridView_Main);
        }

        private void dataGridView_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (!bDSIsOpenForMain) return;
            ClearUIValues(panel_Edit);
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            if (drv == null) return;
            if (drv.IsNew)
            {
                return;
            }

            DataRowViewToUI(drv, panel_Edit);
            tbcMain_SelectedIndexChanged(null, null);

            //if (!bDSIsOpenForMain) return;
            //ClearUIValues(panel_Edit);
            //if (!((DataRowView)bindingSource_Main.Current).IsNew)
            //{
            //    DataRowView drv = (DataRowView)bindingSource_Main.Current;
            //    DataRowViewToUI(drv, panel_Edit);
            //}
           
            /*
              DataRowView drv = (DataRowView)bdsMain.Current;
           
            if (drv != null)
            {
                if (!drv.IsNew)
                    DataRowViewToUI(drv, pnlEdit);
            }
             */
        }

        private void cmb_SelectedValue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            //DoNew();

            FrmStockMCheckFilter frmFilter = new FrmStockMCheckFilter();
            frmFilter.AppInformation = AppInformation;
            frmFilter.UserInformation = UserInformation;
            frmFilter.ShowDialog();
            
             //;
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            DoSave();
            //DoSaveDetail();

        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            DoEdit();
            DoEditDetail();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            DoUndo();
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            DoDelete();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_nType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }


        private void tlb_M_Find_Click(object sender, EventArgs e)
        {

        }

        private void bindingSource_Detail_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton_Audit_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView) bindingSource_Main.Current ;
            if (drv == null)
            {
                MessageBox.Show("���̵����ݣ�");
                return ;
            }
            if (int.Parse(drv["nStatus"].ToString()) > 0)
            {
                MessageBox.Show("�Բ��𣬸õ��Ѿ���ʼ�̵㣬������ˣ�");
                return ;
            }
            string sErr = "";
            string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, int.Parse(drv["nBClass"].ToString()), drv["cBNo"].ToString(), 0, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
            if (sX.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            else
            {
                MessageBox.Show("��˳ɹ���");
                //btnQry_Click(null, null);
                drv.BeginEdit();
                drv["bIsChecked"] = 1;
                drv["dCheckDate"] = DateTime.Now;
                drv["cChecker"] = UserInformation.UserName;
                drv.EndEdit();
                ((DataTable)bindingSource_Main.DataSource).AcceptChanges();
                DataRowViewToUI(drv, panel_Edit);
                //lbl_Check.Visible = true;
                //lblChecker.Text = "����ˣ�";
            }
            DoRefresh();
            //string sErr = "";
            //string sX = PubDBCommFuns.sp_Chk_DoAjustFromChk(AppInformation.SvrSocket, UserInformation.UserName, drv["cBNo"].ToString().Trim(), UserInformation.UnitId, "WMS", out sErr);
            //if (sX.Trim() != "" && sX.Trim() != "0" && sX.Trim() != "B" && sErr.Trim() != "")
            //{
            //    MessageBox.Show(sErr);
            //    return;
            //}
            //else
            //{
            //    string sT = "��˳ɹ�";
            //    if (sX.Trim() == "B")
            //        sT += "�������ɵ�������" + sErr.Trim();
            //    MessageBox.Show(sT);
            //}
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (bindingSource_Detail.Count == 0) return;
            DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
            if (drvMain == null) return;
            int nStatus = int.Parse(drvMain["nStatus"].ToString());
            int bIsChecked = int.Parse(drvMain["bIsChecked"].ToString());
            miDoTask.Enabled = nStatus < 3 && bIsChecked == 1;
            miRegDtl.Enabled = nStatus < 3 && bIsChecked == 1;
            miAddMatDtl.Enabled = nStatus < 3 && bIsChecked == 1;
            miRegBatchNoDiff.Enabled = nStatus < 3 && bIsChecked == 1;
            

        }

        private void miDoTask_Click(object sender, EventArgs e)
        {//�·�����
            DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
            if (drvMain == null)
            {
                MessageBox.Show("���̵�����ϸ���ݣ�");
                return;
            }
            frmChkPosList frmX = new frmChkPosList();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.CheckNo = drvMain["cBNo"].ToString();
            frmX.ShowDialog();
            if (frmX.IsOK)
            {
                //ˢ��
                tbcMain_SelectedIndexChanged(null, null);
            }

            frmX.Dispose();

            #region ss
            /*
            for (int iRow =0; iRow < grdList.SelectedRows.Count; iRow++)
            {
                {
                   try
                   {
                      //  str = dataGridView_Detail.SelectedRows[iRow].Cells[3].Value.ToString();
                        string sId = "";
                        DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                        cmdInfo.SqlText = "sp_Check_DoTastCMD :cSysType,:cBNo,:cPalletId,:nStation";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                        cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                        cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                        cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                        cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                        //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                        //�������
                        ZqmParamter par = null;           //�������� ����                          
                        par = new ZqmParamter();          //��������ʵ��
                        par.ParameterName = "cSysType";           //�������� ��ʵ�ʶ����һ��
                        par.ParameterValue = "WMS";            //����ֵ ����Ϊ""��
                        par.DataType = ZqmDataType.String;  //��������������
                        par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                        //��Ӳ���
                        cmdInfo.Parameters.Add(par);
                        //------
                        par = new ZqmParamter();          //��������ʵ��
                        par.ParameterName = "cBNo";           //�������� ��ʵ�ʶ����һ��
                        par.ParameterValue = grdList.SelectedRows[iRow].Cells[0].Value.ToString();       //����ֵ ����Ϊ""��
                        par.DataType = ZqmDataType.String;  //��������������
                        par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                        //��Ӳ���
                        cmdInfo.Parameters.Add(par);
                        //---

                        //------
                   
                        par = new ZqmParamter();          //��������ʵ��
                        par.ParameterName = "cPalletId";           //�������� ��ʵ�ʶ����һ��
                        par.ParameterValue = grdList.SelectedRows[iRow].Cells[1].Value.ToString();     //����ֵ ����Ϊ""��
                        par.DataType = ZqmDataType.String;  //��������������
                        par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                        //��Ӳ���
                        cmdInfo.Parameters.Add(par);
                        //---

                        //------
                        DataRowView drv = (DataRowView)bindingSource_Main.Current;
                        par = new ZqmParamter();          //��������ʵ��
                        par.ParameterName = "nStation";           //�������� ��ʵ�ʶ����һ��
                        par.ParameterValue ="0";       //����ֵ ����Ϊ""��
                        par.DataType = ZqmDataType.String;  //��������������
                        par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                        //��Ӳ���
                        cmdInfo.Parameters.Add(par);

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
                            //  if (tbX != null)
                            //    sId = tbX.Rows[0]["cNewId"].ToString();
                        } MessageBox.Show(tbX.Rows[0]["cResult"].ToString());
                        dsX.Clear();
                   }

                   catch (Exception ei)
                    {
                        MessageBox.Show(ei.Message);
                    } 
                    // MessageBox.Show((dataGridView_Detail.SelectedRows[iRow].Cells[0]["cMNo"].ToString()));
                   
                }            
            }
             */
            #endregion

        }

        private void miRegDtl_Click(object sender, EventArgs e)
        {
            int nState = 2;
            try
            {
                DataRowView drvDetail = (DataRowView)bindingSource_Detail.Current;
                DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
                if (drvDetail == null)
                {
                    MessageBox.Show("���̵�����ϸ���ݣ�");
                    return;
                }
                nState = int.Parse(drvDetail["nStatus"].ToString());
                //���״̬(0δ������ 1 �������� 2�Ǽ����)

                frmChkDtlWrite frmX = new frmChkDtlWrite();
                frmX.AppInformation = AppInformation;
                frmX.UserInformation = UserInformation;
                frmX.IsNewAddMat = false;
                frmX.WHId = drvMain["cWHId"].ToString().Trim();
                frmX.CheckNo = drvMain["cBNo"].ToString().Trim();
                frmX.PosId = drvDetail["cPosId"].ToString().Trim();
                frmX.PalletId = drvDetail["nPalletId"].ToString().Trim();
                frmX.BoxId = drvDetail["cBoxId"].ToString().Trim();
                frmX.MNo = drvDetail["cMNo"].ToString().Trim();
                frmX.MName = drvDetail["cMName"].ToString().Trim();
                frmX.BatchNo = drvDetail["cBatchNo"].ToString().Trim();
                frmX.Spec = drvDetail["cSpec"].ToString().Trim();
                frmX.Unit = drvDetail["cUnit"].ToString().Trim();
                frmX.BNoIn = drvDetail["cBNoIn"].ToString().Trim();
                frmX.ItemIn = int.Parse(drvDetail["nItemIn"].ToString().Trim());
                frmX.QCStatus = int.Parse(drvDetail["nQCStatus"].ToString());
                frmX.Qty = double.Parse(drvDetail["fQty"].ToString());
                frmX.ShowDialog();
                if (frmX.IsOK)
                {
                    tbcMain_SelectedIndexChanged(null, null);
                }
                frmX.Dispose();
            }
            catch (Exception ei)
            {
                MessageBox.Show(ei.Message);
            }

        }
        private void miAddMatDtl_Click(object sender, EventArgs e)
        {
            int nState = 2;
            try
            {
                DataRowView drvDetail = (DataRowView)bindingSource_Detail.Current;
                DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
                if (drvDetail == null)
                {
                    MessageBox.Show("���̵�����ϸ���ݣ�");
                    return;
                }
                nState = int.Parse(drvDetail["nStatus"].ToString());
                //���״̬(0δ������ 1 �������� 2�Ǽ����)

                frmChkDtlWrite frmX = new frmChkDtlWrite();
                frmX.AppInformation = AppInformation;
                frmX.UserInformation = UserInformation;
                frmX.IsNewAddMat = true;
                frmX.WHId = drvMain["cWHId"].ToString().Trim();
                frmX.CheckNo = drvMain["cBNo"].ToString().Trim();
                frmX.PosId = drvDetail["cPosId"].ToString().Trim();
                frmX.PalletId = drvDetail["nPalletId"].ToString().Trim();
                frmX.BoxId = drvDetail["cBoxId"].ToString().Trim();
                frmX.MNo = "";// drvDetail["cMNo"].ToString().Trim();
                frmX.MName = "";// drvDetail["cMName"].ToString().Trim();
                frmX.BatchNo = "";// drvDetail["cBatchNo"].ToString().Trim();
                frmX.Spec = "";// drvDetail["cSpec"].ToString().Trim();
                frmX.Unit = "";// drvDetail["cUnit"].ToString().Trim();
                frmX.BNoIn = "����ʼ��";// drvDetail["cBNoIn"].ToString().Trim();
                frmX.ItemIn =0;// int.Parse(drvDetail["nItemIn"].ToString().Trim());
                frmX.QCStatus = 1;// int.Parse(drvDetail["nQCStatus"].ToString());
                frmX.Qty = 0;// double.Parse(drvDetail["fQty"].ToString());
                frmX.ShowDialog();
                if (frmX.IsOK)
                {
                    //drvDetail.BeginEdit();
                    //if (nState < 2)
                    //{
                    //    drvDetail["nStatus"] = 2;
                    //}
                    //else
                    //{
                    //    drvDetail["nStatus"] = 3; //����
                    //}
                    //drvDetail["fDiff"] = frmX.Diff;
                    //drvDetail["fRQty"] = double.Parse(drvDetail["fQty"].ToString()) + frmX.Diff;
                    //drvDetail.EndEdit();
                    //drvDetail.Row.Table.AcceptChanges();
                    tbcMain_SelectedIndexChanged(null, null);
                }
                frmX.Dispose();

                //ˢ�µ�������
                //DoRefresh();
                /*
                Form_StockMCheckFilter1 frm = new Form_StockMCheckFilter1();
                frm.textBox_cUser.Text = UserInformation.UserId;
                frm.textBox_cCheckNo.Text = drvDetail["cBNo"].ToString();
                frm.textBox_cWHId.Text = drvMain["cWHId"].ToString();
                frm.textBox_cPalletId.Text = drvDetail["nPalletId"].ToString();
                frm.textBox_cBoxId.Text = drvDetail["cBoxId"].ToString();
                frm.textBox_cMNo.Text = drvDetail["cMNo"].ToString();
                frm.textBox_cBatchNo.Text = drvDetail["cBatchNo"].ToString();
                frm.textBox_nQCStatus.Text = drvDetail["nQCStatus"].ToString();
                frm.textBox_fQty.Text = drvDetail["fQty"].ToString();
                frm.textBox_fRQty.Text = drvDetail["fQty"].ToString();
                frm.ShowDialog();
                */
            }
            catch (Exception ei)
            {
                MessageBox.Show(ei.Message);
            }

        }
        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sBNo = "";            
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            if (drv != null && (!drv.IsNew))
                sBNo = drv["cBNo"].ToString();
            switch (tbcMain.SelectedTab.Name)
            {
                case "tbpChkDtl":
                    BandDataSetDetail(" where cBNo='" + sBNo + "'", grdDtl);
                    break;
                case "tbpChkList" :
                    BandDataSetList(" where cBNo='" + sBNo + "'", this.grdList);
                    break;

            }            
            
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbQ_CheckType.SelectedIndex = -1;
            cmbQ_Ware.SelectedIndex = -1;
            textBox_cBNoQ.Text = "";
            chkDate.Checked = false;
        }

        private void tlb_M_Ajust_Click(object sender, EventArgs e)
        {
            int nStatus = 0;
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            if (drv == null)
            {
                MessageBox.Show("���̵����ݣ�");
                return;
            }
            nStatus = int.Parse(drv["nStatus"].ToString());
            if (nStatus < 2)
            {
               if( MessageBox.Show("���̵㵥������δ�̵�Ǽ�����,ȷ���̵������","ѯ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)== DialogResult.No)
                return;
            }
            else if (nStatus == 3)
            {
                MessageBox.Show("�Բ��𣬸õ����̵������");
                return;
            }

            string sErr = "";
            //string sX = PubDBCommFuns.sp_Chk_DoAjustFromChk(AppInformation.SvrSocket, UserInformation.UserId, drv["cBNo"].ToString(), UserInformation.UnitId, "WMS", out sErr);
            string sX = PubDBCommFuns.sp_Pack_BillWKTskOver(AppInformation.SvrSocket, int.Parse(drv["nBClass"].ToString()), drv["cBNo"].ToString(), UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);
            if (sX.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            else
            {
                MessageBox.Show("�̵�����ɹ���");
                //btnQry_Click(null, null);
                drv.BeginEdit();
                drv["nStatus"] = 3;
                drv.EndEdit();
                ((DataTable)bindingSource_Main.DataSource).AcceptChanges();
                DataRowViewToUI(drv, panel_Edit);
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

        private void tlb_M_ErpImp_Click(object sender, EventArgs e)
        {
            DataInFromMid.DataInFromMid.DataImpBillCheck(AppInformation, UserInformation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WareStore.frmSlackMatCount frmX = new WareStore.frmSlackMatCount();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.ShowDialog();
            frmX.Dispose();
        }

        private void mi_Print_ChkDiffList_Click(object sender, EventArgs e)
        {

        }

        private void mi_Print_ChkMatList_Click(object sender, EventArgs e)
        {
            string sSql_Main = "select bil.cBNo,bil.cWHId,bil.dDate,case bil.bIsChecked when 0 then 'δ���' else '�����' end cIsChecked,"+
                    " case bil.bIsFinished when 0 then '�̵�δ����' else '�̵��ѽ���' end cIsFinished,bil.cCreator cUser,bil.cRemark, "+
                    " bil.cBNoAjust,bil.cBNoBad, bt.cBType from TWB_BillCheck bil left join TPB_BillType bt on bil.cCheckType=bt.cBTypeId ";
            string sSql_Dtl = "select cMNo,cMName,cSpec,cBatchNo,case nQCStatus when -1 then '���ϸ�' else '�ϸ�' end cQCStatus,"+
                    " fQty,fDiff,fBad,fErpQty,cUnit from TWB_BillCheckList";
            string sBNo = "";
            if (bindingSource_Main.Count == 0)
            {
                MessageBox.Show("�Բ������̵㵥���ݿɴ�ӡ��");
                return;
            }
            DataRowView drv = null;
            drv = (DataRowView)bindingSource_Main.Current;
            if (drv == null) return;
            string sErr = "";
            sBNo = drv["cBNo"].ToString();
            sSql_Main = sSql_Main + " where bil.cBNo='"+ sBNo +"'";
            sSql_Dtl = sSql_Dtl + " where cBNo='" + sBNo + "'";
            DataSet dsX = null;
            dsX = WareStoreMS.DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql_Main, "tbBillCheck", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbMain = null;
            if (dsX.Tables["tbBillCheck"] != null)
            {
                tbMain = dsX.Tables["tbBillCheck"].Copy();
            }
            if (tbMain == null) return;
            dsX = WareStoreMS.DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql_Dtl, "tbBillCheckList", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbDtl = null;
            if (dsX.Tables["tbBillCheckList"] != null)
            {
                tbDtl = dsX.Tables["tbBillCheckList"].Copy();
            }
            if (tbDtl == null) return;
            dsX.Clear();
            DataSet dsRpt = new DataSet();
            dsRpt.Tables.Add(tbMain);
            dsRpt.Tables.Add(tbDtl);
            WareStore.Rpts.rptCheckList rptX = new WareStore.Rpts.rptCheckList();
            WareStore.Rpts.frmRptViewer frmX = new WareStore.Rpts.frmRptViewer();
            frmX.RptObj = rptX;
            rptX.SetDataSource(dsRpt);
            frmX.RptTitle = "�̵������嵥����";
            frmX.SetReport();
            frmX.ShowDialog();
            frmX.Dispose();
            rptX.Dispose();
            dsRpt.Clear();
        }

        private void mi_Print_StkDtl_Click(object sender, EventArgs e)
        {
            string sSql_Main = "select bil.cBNo,bil.cWHId,bil.dDate,case bil.bIsChecked when 0 then 'δ���' else '�����' end cIsChecked," +
        " case bil.bIsFinished when 0 then '�̵�δ����' else '�̵��ѽ���' end cIsFinished,bil.cCreator cUser,bil.cRemark, " +
        " bil.cBNoAjust,bil.cBNoBad, bt.cBType from TWB_BillCheck bil left join TPB_BillType bt on bil.cCheckType=bt.cBTypeId ";
            string sSql_Dtl = "select cPosId,nPalletId,cBoxId,cMNo,cMName,cSpec,cBatchNo,case nQCStatus when -1 then '���ϸ�' else '�ϸ�' end cQCStatus," +
                    " fQty,fDiff,fBad,cUnit,cBNoIn,nItemIn from TWB_BillCheckDtl ";
            string sBNo = "";
            if (bindingSource_Main.Count == 0)
            {
                MessageBox.Show("�Բ������̵㵥���ݿɴ�ӡ��");
                return;
            }
            DataRowView drv = null;
            drv = (DataRowView)bindingSource_Main.Current;
            if (drv == null) return;
            string sErr = "";
            sBNo = drv["cBNo"].ToString();
            sSql_Main = sSql_Main + " where bil.cBNo='" + sBNo + "'";
            sSql_Dtl = sSql_Dtl + " where cBNo='" + sBNo + "'";
            DataSet dsX = null;
            dsX = WareStoreMS.DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql_Main, "tbBillCheck", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbMain = null;
            if (dsX.Tables["tbBillCheck"] != null)
            {
                tbMain = dsX.Tables["tbBillCheck"].Copy();
            }
            if (tbMain == null) return;
            dsX = WareStoreMS.DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql_Dtl, "tbBillCheckDtl", 0, 0, "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbDtl = null;
            if (dsX.Tables["tbBillCheckDtl"] != null)
            {
                tbDtl = dsX.Tables["tbBillCheckDtl"].Copy();      
            }
            if (tbDtl == null) return;
            dsX.Clear();
            DataSet dsRpt = new DataSet();
            dsRpt.Tables.Add(tbMain);
            dsRpt.Tables.Add(tbDtl);
            WareStore.Rpts.rptCheckDtl rptX = new WareStore.Rpts.rptCheckDtl();
            WareStore.Rpts.frmRptViewer frmX = new WareStore.Rpts.frmRptViewer();
            frmX.RptObj = rptX;
            rptX.SetDataSource(dsRpt);
            frmX.RptTitle = "�̵�����ϸ�嵥����";
            frmX.SetReport();
            frmX.ShowDialog();
            frmX.Dispose();
            rptX.Dispose();
            dsRpt.Clear();
        }

        private void miRegBatchNoDiff_Click(object sender, EventArgs e)
        {
            if (bdsList.Count == 0)
            {
                MessageBox.Show("�Բ������̵����ݣ�");
                return;
            }
            int nCount = grdDtl.SelectedRows.Count;
            if (nCount == 0)
            {
                MessageBox.Show("�Բ������̵���ϸ���ݣ�");
                return ;
            }
            string sWHId = "";
            string sCheckNo = "";
            DataRowView drV = null;
            drV = (DataRowView)bdsList.Current;
            if (drV == null) return;
            sWHId = drV["cWHId"].ToString();
            sCheckNo = drV["cBNo"].ToString();
            if (MessageBox.Show("��ȷ����Ҫ������ѡ���" + nCount.ToString() + "�����ݽ������̲���Ǽ���", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No) return;
            double fDiff = 0;
            double fBad = 0;
            int nX = 0;
            pgDtl.Maximum = nCount;
            pgDtl.Minimum = 0;
            pgDtl.Value = 0;
            pgDtl.Visible = true;
            foreach (DataGridViewRow grdrX in grdDtl.SelectedRows)
            {
                string sErr="";
                string sX = DBFuns.sp_Chk_WriteChkDtl(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, "WMS", sWHId, grdrX.Cells[colDtlPosId.Name].Value.ToString().Trim(),
                    grdrX.Cells[colDtlPalletId.Name].Value.ToString().Trim(), grdrX.Cells[colDtlcBoxId.Name].Value.ToString().Trim(), grdrX.Cells[colDtlMNo.Name].Value.ToString().Trim(), fDiff, fBad,
                    grdrX.Cells[colDtlUnit.Name].Value.ToString().Trim(), grdrX.Cells[colDtlcBNoIn.Name].Value.ToString().Trim(), Convert.ToInt32(grdrX.Cells[colDtlnItemIn.Name].Value),
                    sCheckNo, "", "", "", out sErr);
                if (sX.Trim() != "0" && sX.Trim() != "B" && sErr.Trim() != "")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    nX++;                   
                }
                pgDtl.Value++;
            }
            pgDtl.Visible = false;

            MessageBox.Show("�ɹ��Ǽ��ˣ�" + nX.ToString() + " �����ݣ�");
        }
    }
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                