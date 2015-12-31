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
    public partial class FrmItemEditor : UI.FrmSTable
    {
        public delegate bool DoEditItemInfo(DataRowView drvX);

        #region ˽�б���
        bool bIsNew = true ;
        private DataRowView drvItem;
        private DoEditItemInfo doItem;
        private bool bIsShowGrid = false;
        private DataSet dsItemList = new DataSet();
        private int nQCState = 1;//�ʼ�״̬ Ĭ��Ϊ1
        private bool bIsResultOK = false; //��ʾ�Ѿ��༭�ɹ�

        private double fUseQty = 0;
        #endregion

        #region ˽�з���

        /// <summary>
        /// �ж����ϱ����Ƿ�Ϸ�
        /// </summary>
        /// <param name="sMNo"></param>
        /// <returns></returns>
        private bool JudgeMNoIsOK(string sMNo)
        {
            bool bOK = false;
            string sSql = "select count(*) nCount from TPC_Material where cMNo='" + sMNo.Trim() + "'";
            string sErr = "";
            object objX = null;
            if (DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "nCount", out objX, out sErr))
            {
                if (sErr.Trim() != "" && sErr.Trim() != "0")
                {
                    MessageBox.Show(sErr);
                    return false;
                }
                else
                {
                    if (objX == null)
                    {
                        bOK = false;
                    }
                    else
                    {
                        bOK = objX.ToString() == "1";
                    }
                }
            }
            else
            {
                bOK = false;
            }
            return bOK;
        }


          private bool OpenItemList(string sItemValue)
          {
              bool bIsOK = false;
              StringBuilder  sSql = new StringBuilder("") ;
              sSql.Append("select * from TPC_Material ");
              if (sItemValue.Trim() != "")
              {
                  sSql.Append(" where (cMNo like '%" + sItemValue.Trim() + "%') or (cName like '%" + sItemValue.Trim() + "%') or (isnull(cPYJM,' ') like '%" + sItemValue.Trim().ToUpper() + "%') or (isnull(cWBJM,' ') like '%" + sItemValue.Trim().ToUpper() + "%')");
              }
              //dsItemList.Clear();
              if (dsItemList.Tables["data"] != null)
                  dsItemList.Tables["data"].Clear();
              //bdsItemList.DataSource = null;
              //grdDtl.DataSource = null;
              grdDtl.AutoGenerateColumns = false;
              string err = "";
              dsItemList = PubDBCommFuns.GetDataBySql(sSql.ToString(), out err);
              bIsOK = dsItemList.Tables[0].Rows[0][0].ToString() == "0";
              if (dsItemList.Tables[0].Rows[0][0].ToString()=="0")
              {
                  bdsItemList.DataSource = dsItemList.Tables["data"];
                  grdDtl.DataSource = bdsItemList;
              }
              else
                  MessageBox.Show(dsItemList.Tables[0].Rows[0][0].ToString());
              grdDtl.Visible = true;
              
              return (bIsOK);
          }

            private int GetMaterialKeepDay(string sMNo)
            {
                int nDay = 360;
                string sSql = "select isnull(nKeepDay,360) nKeepDay from TPC_Material where cMNo='" + sMNo.Trim() + "'";
                object objX = null;
                string sErr = "";
                PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "nKeepDay", out objX, out sErr);
                if (sErr.Trim() != "" && sErr.Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                else if (objX != null)
                {
                    nDay = int.Parse(objX.ToString());
                }
                return nDay;
            }

            private int GetMaterialQCState(string sMNo)
            {
                int nState = 1;
                string sSql = "select isnull(bIsQC,1) bIsQC from TPC_Material where cMNo='" + sMNo.Trim() + "'";
                object objX = null;
                string sErr = "";
                PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "bIsQC", out objX, out sErr);
                if (sErr.Trim() != "" && sErr.Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                else if (objX != null)
                {
                    nState = int.Parse(objX.ToString());
                    if (nState == 1)
                    {
                        nState = 0;
                    }
                    else
                    {
                        nState = 1;
                    }
                }
                return nState;
            }
/*
        string sMNo,string sMName,string sSpec,string sMatStyle,string sMatQCLevel,string sMatOther,
            string sRemark,string sABC,double fSafeQtyDn,double fSafeQtyUp,double fQtyBox,double fWeight,string sTypeId1,string sType1,
            string sTypeId2,string sType2,string sUnit, int nKeepDay,string sCSId,string sSupplier,int _nMatClass, bool bIsSelectOK
        */
        private void DoSelectMat(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
            string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1,
            string sTypeId2, string sType2, string sUnit, int nKeepDay,string sCSId,string sSupplier,int _nMatClass, bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                txt_Dtl_cMatOther.Text = sMatOther.Trim();
                txt_Dtl_cMatQCLevel.Text = sMatQCLevel.Trim();
                txt_Dtl_cMatStyle.Text = sMatStyle.Trim();
                txt_Dtl_cMName.Text = sMName.Trim();
                txt_Dtl_cMNo.Text = sMNo;
                //MessageBox.Show(sMNo.Trim().Length.ToString());
                txt_Dtl_cSpec.Text = sSpec.Trim();
                txt_Dtl_cSupplier.Text = sSupplier.Trim();
                txt_Dtl_cUnit.Text = sUnit;
                if (bIsNew && isOutBill)
                {
                    //��ȡ��������
                    string sErr = "";
                    double fQty = 0;
                    fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text, _WHId.Trim(), _QCStatus, "", 0,"","",0, out sErr);
                    if (sErr.Trim() == "" || sErr.Trim() == "0")
                    {
                        fUseQty = fQty;
                        lbl_Out.Text = "�ɳ�����" + fQty.ToString() + "  (�ɳ��� =�����-������)";
                    }
                    else
                    {
                        MessageBox.Show(sErr);
                    }
                }
            }
        }

        #endregion

        #region ��������
        
            public DataRowView DrvItem
            {
                get { return (drvItem); }
                set { drvItem = value; }
            }
            public bool BIsNew
            {
                get { return (bIsNew); }
                set 
                    { 
                        bIsNew = value;
                        txt_Dtl_cMNo.ReadOnly = !bIsNew;
                    }
            }
            public DoEditItemInfo DoItem
            {
                get { return (doItem); }
                set { doItem = value; }
            }

            public bool BIsResult
            {
                get { return (bIsResultOK); }
                set { bIsResultOK = value; }
            }

            private string _BType = "";
            public string BType
            {
                get { return _BType.Trim(); }
                set { _BType = value.Trim(); }
            }

            private  bool isOutBill = false;
            /// <summary>
            /// �Ƿ�Ϊ���ⵥ����¼��
            /// </summary>
            [Description("�Ƿ�Ϊ���ⵥ����¼��")]
            public bool IsOutBill
            {
                get { return isOutBill; }
                set 
                {                         
                    isOutBill = value;
                    if (isOutBill)
                    {
                        //dtp_Dtl_dProdDate.Visible = !isOutBill;
                        lbl_Tag_BachNo.Visible = !isOutBill;
                        //lbl_Tag_Prod.Visible = !isOutBill;
                        lbl_Out.Visible = isOutBill;
                        Text = "�������ϱ༭��";
                        //txt_Dtl_cBatchNo.Visible = !isOutBill;
                        //lbl_cBatchNo.Visible = !isOutBill;
                        //lbl_dProdDate.Visible = !isOutBill;
                    }
                    else
                    {
                        Text = "������ϱ༭��";
                    }
                }
            }

            private string _WHId = "";
            /// <summary>
            /// �ֿ��
            /// </summary>
            public string WHId
            {
                get { return _WHId.Trim(); }
                set { _WHId = value.Trim(); }
            }

            private int _QCStatus = 0;
            private int QCStatus
            {
                get { return _QCStatus; }
                set { _QCStatus = value; }
            }
        #endregion

        #region ��������
        public void DataRowToUI()
        {
            if (drvItem == null)
            {
                MessageBox.Show("�Բ���������ϸ�����ж���Ϊ�գ�");
            }
            else
            {
                bIsShowGrid = false;
                DataRowViewToUI(drvItem, pnlDtlEdit);
                bIsShowGrid = true;
                if ((!bIsNew) && isOutBill)
                {
                        //��ȡ��������
                    if(txt_Dtl_fQty.Text.Trim() != "")
                    {
                        string sErr = "";
                        double fQty = double.Parse(txt_Dtl_fQty.Text.Trim()) ;
                        fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text.Trim(),_WHId.Trim(),_QCStatus, "", fQty, out sErr);
                        if (sErr.Trim() == "" || sErr.Trim() == "0")
                        {

                            fUseQty = fQty;
                            lbl_Out.Text = "�ɳ�����" + fQty.ToString() + "  (�ɳ��� =�����-������)";
                        }
                        else
                        {
                            MessageBox.Show(sErr);
                        }
                    }                    
                }
            }
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
            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
        public FrmItemEditor()
        {
            InitializeComponent();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            //grdDtl.Visible = !grdDtl.Visible;   
            bIsShowGrid = false;
            App.WareStore.SelectStkMaterial(AppInformation, UserInformation, DoSelectMat);
            bIsShowGrid = true;
        }

        private void grdDtl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bdsItemList.Count > 0)
            {
                DataRowView drTmp = (DataRowView)bdsItemList.Current;
                if (drTmp != null)
                {
                    try
                    {
                        bIsShowGrid = false;
                        txt_Dtl_cMNo.Text = drTmp["cMNo"].ToString();
                        bIsShowGrid = true;
                        if (bIsNew && isOutBill)
                        {
                            //��ȡ��������
                            string sErr = "";
                            double fQty = 0;
                            fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text.Trim(), _WHId.Trim(), _QCStatus, "", 0, out sErr);
                            if (sErr.Trim() == "" || sErr.Trim() == "0")
                            {

                                fUseQty = fQty;
                                lbl_Out.Text = "�ɳ�����" + fQty.ToString() + "  (�ɳ��� =�����-������)";
                            }
                            else
                            {
                                MessageBox.Show(sErr);
                            }
                        }
                        //txt_Dtl_cItemName.Text = "FFFFF";
                        //objX = drTmp["cItemName"];
                        //sX = objX.ToString();
                        //txt_Dtl_cName.Text = sX  ;
                        //txt_Dtl_cName.ReadOnly = true;
                        //objX = drTmp["cItemSpecial"];
                        //if (objX != null)
                            //if (drX["cItemSpecial"].ToString() != "")
                            //txt_Dtl_cSpec.Text = drTmp["cItemSpecial"].ToString();
                        //else txt_Dtl_cSpec.Text = "";
                        //cmb_Dtl_cUnit.Items.Clear();
                        //cmb_Dtl_cUnit.Items.Add(drTmp["cUnit"].ToString());
                        txt_Dtl_cUnit.Text = drTmp["cUnit"].ToString();
                        txt_Dtl_cMName.Text = drTmp["cName"].ToString();
                        txt_Dtl_cSpec.Text = drTmp["cSpec"].ToString();
                        if (!isOutBill)
                        {
                            //dtp_Dtl_dProdDate.Value = DateTime.Now;
                            //dtp_Dtl_dProdDate.Focus();
                        }
                        else
                        {
                            txt_Dtl_fQty.SelectAll();
                            txt_Dtl_fQty.Focus();
                        }
                        grdDtl.Visible = false;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strBId = "";
            //����Ƿ�¼��ȫ
            if (txt_Dtl_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("�Բ������ϱ��벻��Ϊ�գ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_cMNo.Focus();
                return;
            }
            if (!JudgeMNoIsOK(txt_Dtl_cMNo.Text.Trim()))
            {
                MessageBox.Show("�Բ���¼���������ϱ��벻���ڣ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_cMNo.SelectAll();
                txt_Dtl_cMNo.Focus();
                return;
            }
            if (this.txt_Dtl_fQty.Text.Trim() == "")
            {
                MessageBox.Show("�Բ���������������Ϊ�գ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_fQty.Focus();
                return;
            }
            if (!IsNumberic(txt_Dtl_fQty.Text.Trim()))
            {
                MessageBox.Show("�Բ�����������Ϊ�Ƿ���ֵ��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_fQty.SelectAll();
                txt_Dtl_fQty.Focus();
                return;
            }
            if (double.Parse(txt_Dtl_fQty.Text.Trim()) == 0)
            {
                MessageBox.Show("�Բ�����������Ϊ0");
                txt_Dtl_fQty.SelectAll();
                txt_Dtl_fQty.Focus();
                return;
            }
            if (txt_Dtl_cUnit.Text.Trim() == "")
            {
                MessageBox.Show("�Բ��𣬵�λ����Ϊ�գ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_cUnit.SelectAll();
                txt_Dtl_cUnit.Focus();
                return;
            }
            if (!isOutBill)
            {
                if (txt_Dtl_cBatchNo.Text.Trim() == "")
                {
                    MessageBox.Show("�Բ������ʱ�����Ų���Ϊ�գ�");
                    txt_Dtl_cBatchNo.SelectAll();
                    txt_Dtl_cBatchNo.Focus();
                    return;
                }
            }
            if (isOutBill)
            {
                if (double.Parse(txt_Dtl_fQty.Text.Trim()) > fUseQty)
                {
                    MessageBox.Show("�Բ��𣬳��������ڿɳ�����", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_Dtl_fQty.SelectAll();
                    txt_Dtl_fQty.Focus();
                    return;
                }
            }


            UIToDataRowView(drvItem, pnlDtlEdit);
            if (bIsNew == true) //���������
            {
                strBId = drvItem["cBNo"].ToString();
                        bIsResultOK = true;
                        if (!isOutBill)
                        {
                            nQCState = GetMaterialQCState(drvItem["cMNo"].ToString().Trim());
                            drvItem.BeginEdit();
                            drvItem["nQCStatus"] = nQCState;
                            drvItem.EndEdit();
                        }
                        bIsShowGrid = false;
                        DataRowViewToUI(drvItem,pnlDtlEdit);
                        bIsShowGrid = true;
                        drvItem["nItem"] = GetNewItem(strBId);
                        string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvItem, "TWB_BillInDtl", "cBNo,nItem", "cMName,cSpec,cQCStatus,cPStatus,cMatStyle,cMatQCLevel,cMatOther", true);
                        string err = "";
                        DataSet ds = PubDBCommFuns.GetDataBySql(sql,DBCommInfo.DBSQLCommandInfo.GetFieldsForDate(drvItem), out err);
                        bIsResultOK = ds.Tables[0].Rows[0][0].ToString() == "0";
                        //this.Close();
                        if (bIsResultOK)
                        {
                            MessageBox.Show("������ϸ�ɹ���");
                            ClearUIValues(pnlDtlEdit);
                            drvItem["cBNo"] = strBId;
                            drvItem["cMNo"] = "";
                            DataRowViewToUI(drvItem,pnlDtlEdit);
                            txt_Dtl_cMNo.SelectAll();
                            txt_Dtl_cMNo.Focus();
                        }
            }
            else //�޸�
            {
                bIsShowGrid = false;
                DataRowViewToUI(drvItem, pnlDtlEdit);
                bIsShowGrid = true;
                string sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvItem, "TWB_BillInDtl", "cBNo,nItem", "cMName,cSpec,cQCStatus,cPStatus,cMatStyle,cMatQCLevel,cMatOther", false);
                string err = "";
                DataSet ds = PubDBCommFuns.GetDataBySql(sql, DBCommInfo.DBSQLCommandInfo.GetFieldsForDate(drvItem), out err);
                bIsResultOK = ds.Tables[0].Rows[0][0].ToString() == "0";
                this.Close();
            }
        }

        private void txt_Dtl_cItemId_TextChanged(object sender, EventArgs e)
        {
            if (txt_Dtl_cMNo.Text.ToString() == "")
                grdDtl.Visible = false;
            else
            {
                if ((bIsNew == true) && (bIsShowGrid == true))
                {
                    OpenItemList(((TextBox)sender).Text.Trim());
                }
                else
                {
                    if ((bIsNew == false) && (bIsShowGrid == true))
                    {
                        txt_Dtl_cMNo.ReadOnly = true;
                        grdDtl.Visible = false;
                    }
                }
            }
        }

        private void FrmItemEditor_Load(object sender, EventArgs e)
        {
            grdDtl.Top = pnlDtlEdit.Top + txt_Dtl_cMNo.Top + txt_Dtl_cMNo.Height;
            grdDtl.Left = pnlDtlEdit.Left + txt_Dtl_cMNo.Left;
            grdDtl.Visible = false;
            if (isOutBill)
            {
                lbl_Out.Visible = true ;

            }
            else
            {
                lbl_Out.Visible = false ;
  
            }
            txt_Dtl_cMNo.Focus();
        }

        private void cmb_Dtl_cUnit_Leave(object sender, EventArgs e)
        {
            if((txt_Dtl_cMNo.Text.Trim() != "") &&(txt_Dtl_fQty.Text.Trim() !="") && (txt_Dtl_cUnit.Text.Trim() != ""))
            btnOK_Click(sender, e);
        }

        private void txt_Dtl_cItemId_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down :
                    //if (bdsItemList.Count > 0)
                    //{
                        grdDtl.Focus();
                    //}
                    break;
                case Keys.Return :
                    SendKeys.Send("{Tab}");
                    break;
            }
        }

  

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdDtl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Return)
            {
                grdDtl_CellDoubleClick(null, null);
            }
        }

        private void txt_Dtl_cItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
                SendKeys.Send("{Tab}");
        }

        private void cmb_Dtl_cUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnOK.Focus();
        }

        private void txt_Dtl_cProductBatchNo_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txt_Dtl_cMNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            ChangeTextBoxBkColorByReadOnly(sender, ((System.Windows.Forms.Control)sender).Parent.BackColor, Color.White);
        }



        private void pnlDtlEdit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Dtl_cBatchNo_Enter(object sender, EventArgs e)
        {
            if (txt_Dtl_cMNo.Text.Trim() != "" && bIsNew && (!isOutBill) && (_BType.Trim() == "101"))
            {
                //object objX = null;
                //string sErr = "";
                //txt_Dtl_cBatchNo.Text = DBFuns.sp_GetBatchNo(AppInformation.SvrSocket, txt_Dtl_cMNo.Text.Trim(),dtp_Dtl_dProdDate.Value.ToString("yyyy-MM-dd"), out sErr);
                //if (sErr.Trim() != "" && sErr.Trim() != "0")
                //{
                //    MessageBox.Show(sErr);
                //}
                txt_Dtl_cBatchNo.SelectAll();                

            }
        }

    }
}