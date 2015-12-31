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
    public partial class frmItemEditorIn : UI.FrmSTable
    {
        public delegate bool DoEditItemInfo(DataRowView drvX);

        #region ˽�б���
        bool bIsNew = true;
        private DataRowView drvItem;
        private DoEditItemInfo doItem;
        private bool bIsShowGrid = false;
        private DataSet dsItemList = new DataSet();
        private int nQCState = 1;//�ʼ�״̬ Ĭ��Ϊ1
        private bool bIsResultOK = false; //��ʾ�Ѿ��༭�ɹ�
        /// <summary>
        /// 0 ��ͨ����,1ҽҩ��Ʒ��2ʳƷ��3��ױƷ
        /// </summary>
        private int nMatClass = 0; //

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
            string sSql = "select count(*) nCount from TPC_Material where cMNo='"+ sMNo.Trim() +"'";
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
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select mat.cMNo,mat.cName,mat.cSpec,mat.cUnit,mat.cMatStyle,mat.cMatOther,mat.cMatQCLevel,mat.cSupplier,mat.cABC,mat.bIsBaseMedic,mat.cDoseType,isnull(mat.nMatClass,0) nMatClass,isnull(sum(isnull(v.fQty,0)),0) fQty ");
	        sSql.Append(" from TPC_Material mat left join V_StoreItemList v on mat.cMNo=v.cMNo ");
	       
            if (sItemValue.Trim() != "")
            {
                sSql.Append(" where (mat.cMNo like '%" + sItemValue.Trim() + "%') or (mat.cName like '%" + sItemValue.Trim() + "%') or (isnull(mat.cPYJM,' ') like '%" + sItemValue.Trim().ToUpper() + "%') or (isnull(mat.cWBJM,' ') like '%" + sItemValue.Trim().ToUpper() + "%')");
            }
            sSql.Append(" group by mat.cMNo,mat.cName,mat.cSpec,mat.cUnit,mat.cMatStyle,mat.cMatOther,mat.cMatQCLevel,mat.cSupplier,mat.cABC,mat.bIsBaseMedic,mat.cDoseType,isnull(mat.nMatClass,0)");
            //dsItemList.Clear();
            if (dsItemList.Tables["data"] != null)
                dsItemList.Tables["data"].Clear();
            //bdsItemList.DataSource = null;
            //grdDtl.DataSource = null;
            grdDtl.AutoGenerateColumns = false;
            string err = "";
            dsItemList = PubDBCommFuns.GetDataBySql(sSql.ToString(), out err);
            bIsOK = dsItemList.Tables[0].Rows[0][0].ToString() == "0";
            if (dsItemList.Tables[0].Rows[0][0].ToString() == "0")
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


        private void DoSelectMat(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
           string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1,
           string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId,string sSupplier,int _nMatClass ,bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                nMatClass = _nMatClass;
                txt_Dtl_cMatOther.Text = sMatOther.Trim();
                txt_Dtl_cMatQCLevel.Text = sMatQCLevel.Trim();
                txt_Dtl_cMatStyle.Text = sMatStyle.Trim();
                txt_Dtl_cMName.Text = sMName.Trim();
                txt_Dtl_cMNo.Text = sMNo.Trim();
                txt_Dtl_cSpec.Text = sSpec.Trim();                               
                txt_Dtl_cUnit.Text = sUnit;
                if (sCSId.Trim() != "" && nMatClass != 0 )
                {
                    txt_Dtl_cCSId.Text = sCSId.Trim();
                    txt_Dtl_cSupplier.Text = sSupplier.Trim();
                }
                
            }
        }

        private void doSelCuSupplier(string sCSId, string sCSNameJ, string sCSNameQ, UserMS.CSType csType, string sTel, string sFax, string sAddress,
        string sRemark, string cType, int nIsInner, int nIsFactory, string sIsInner, string sIsFactory, int bUsed, string sUsed)
        {
            this.txt_Dtl_cSupplier.Text = sCSNameJ.Trim();
            txt_Dtl_cCSId.Text = sCSId.Trim();
        }

        private void doSelIOStoreMatBillData(int nBClass, string sBNo, int nItem, string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
            string sRemark, string sABC, double fQty, double fWeight, string sUnit, string sCSId, string sSupplier,string sFromBatchNo,string sBNoIn,int nItemIn, 
            string sWHIdErp,string sAreaIdErp,string sPosIdErp,out bool bDoOK)
        {
            bDoOK = false;
            bIsShowGrid = false;
            if (txt_Dtl_cMNo.Text.Trim() != "" && txt_Dtl_cMNo.Text.Trim() != sMNo.Trim())
            {
                bIsShowGrid = true ;
                MessageBox.Show("�Բ����˻���������ⵥ���ϲ�һ�£�");
                return;
            }
            txt_Dtl_cMNo.Text = sMNo;
            txt_Dtl_cMName.Text = sMName;
            txt_Dtl_cMatStyle.Text = sMatStyle;
            txt_Dtl_cMatQCLevel.Text = sMatQCLevel;
            txt_Dtl_cMatOther.Text = sMatOther;
            txt_Dtl_cSpec.Text = sSpec;
            txt_Dtl_cSupplier.Text = sSupplier;
            txt_Dtl_cUnit.Text = sUnit;
            txt_Dtl_cCSId.Text = sCSId;
            txt_Dtl_cFromNo.Text = sBNo;
            txt_Dtl_cBatchNo.Text = sFromBatchNo;
            txt_Dtl_nFromItem.Text = nItem.ToString();
            txt_Dtl_cFromDept.Text = sSupplier;
            txt_Dtl_cFromBatchNo.Text = sFromBatchNo;
            txt_Dtl_fFromQty.Text = fQty.ToString();
            drvItem["cBNoIn"] = sBNoIn.Trim();
            drvItem["nItemIn"] = nItemIn;
            bIsShowGrid = true;
            bDoOK = true;
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
                lbl_Dtl_FromBatchNo.Visible = bIsNew;
                this.lbl_Dtl_FromDept.Visible = bIsNew;
                this.lbl_Dtl_FromQty.Visible = bIsNew;
                this.txt_Dtl_cFromBatchNo.Visible = bIsNew;
                this.txt_Dtl_cFromDept.Visible = bIsNew;
                this.txt_Dtl_fFromQty.Visible = bIsNew;
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

        private int _BClass = 0;
        public int BClass
        {
            get { return _BClass; }
            set 
            {
                _BClass = value;
                switch (_BClass)
                {
                    case 11://�������
                        grp_ChkIn.Visible = true;
                        Text = "����������ϱ༭��";
                        break ;
                    default :
                        grp_ChkIn.Visible = false;
                        Text = "������ϱ༭��";
                        break;
                }
            }
        }

        private string _BType = "";
        public string BType
        {
            get { return _BType.Trim(); }
            set {
                _BType = value.Trim();
                switch (_BType.Trim())
                {
                    case "102"://�˻����
                        grp_BackIn.Visible = true;
                        btn_SelSupplier.Enabled = false;
                        Text =  "�˻�"+Text;
                        break;
                    case "1102"://�˻��������
                        grp_BackIn.Visible = true;
                        btn_SelSupplier.Enabled = false;
                        Text = "�˻�" + Text;
                        break;
                    default :
                        grp_BackIn.Visible = false;
                        btn_SelSupplier.Enabled = true;
                        break;
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
                if ((!bIsNew))
                {
                    //��ȡ��������
                    if (txt_Dtl_fQty.Text.Trim() != "")
                    {
                        string sErr = "";
                        double fQty = double.Parse(txt_Dtl_fQty.Text.Trim());
                        fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text.Trim(), _WHId.Trim(), _QCStatus, "", fQty, out sErr);
                        if (sErr.Trim() == "" || sErr.Trim() == "0")
                        {

                            fUseQty = fQty;
                            //lbl_Out.Text = "�ɳ�����" + fQty.ToString() + "  (�ɳ��� =�����-������)";
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
            if (_BClass == 11)
            {
                sTbName = "TWB_BillChkAcceptDtl";
            }
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

        public frmItemEditorIn()
        {
            InitializeComponent();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            //grdDtl.Visible = !grdDtl.Visible;   
            bIsShowGrid = false;
            App.WarehouseBase.SelectMaterialInfo(AppInformation, UserInformation, DoSelectMat);
            txt_Dtl_cMNo.Focus();
            bIsShowGrid = true;
        }

        private void txt_Dtl_cMNo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    //if (bdsItemList.Count > 0)
                    //{
                        grdDtl.Focus();
                    //}
                    break;
                case Keys.Return:
                    SendKeys.Send("{Tab}");
                    break;
            }
        }

        private void dtp_Dtl_dProdDate_Leave(object sender, EventArgs e)
        {
            if (bIsNew )
            {
                //txt_Dtl_cBatchNo.Text = dtp_Dtl_dProdDate.Value.ToString("yyyyMMdd");
                dtp_Dtl_dBadDate.Value = dtp_Dtl_dProdDate.Value.AddDays(GetMaterialKeepDay(txt_Dtl_cMNo.Text.Trim()));
            }   
        }

        private void txt_Dtl_cBatchNo_Enter(object sender, EventArgs e)
        {
            if (txt_Dtl_cMNo.Text.Trim() != "" && bIsNew && (_BType.Trim() != "102" && _BType.Trim() != "103" && _BType.Trim() != "104") && txt_Dtl_cBatchNo.Text.Trim() == "")
            {
                dtp_Dtl_dProdDate_Leave(null, null);
                string sErr = "";
                txt_Dtl_cBatchNo.Text = DBFuns.sp_GetBatchNo(AppInformation.SvrSocket, txt_Dtl_cMNo.Text.Trim(), dtp_Dtl_dProdDate.Value.ToString("yyyy-MM-dd"), out sErr);
                if (sErr.Trim() != "" && sErr.Trim() != "0")
                {
                    MessageBox.Show(sErr);
                }
                txt_Dtl_cBatchNo.SelectAll();

            }
        }

        private void txt_Dtl_fQty_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txt_Dtl_cUnit_KeyDown(object sender, KeyEventArgs e)
        {

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
                        txt_Dtl_cMatStyle.Text = drTmp["cMatStyle"].ToString();
                        txt_Dtl_cMatQCLevel.Text = drTmp["cMatQCLevel"].ToString();
                        txt_Dtl_cMatOther.Text = drTmp["cMatOther"].ToString();
                        if (drTmp["nMatClass"] != null && drTmp["nMatClass"].ToString() != "")
                        {
                            nMatClass = Convert.ToInt16(drTmp["nMatClass"]);
                        }
                        else
                        {
                            nMatClass = 0;
                        }
                        //if (bIsNew && isOutBill)
                        //{
                        //    //��ȡ��������
                        //    string sErr = "";
                        //    double fQty = 0;
                        //    fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text.Trim(), _WHId.Trim(), _MatClass.Trim(), _QCStatus, "", 0, out sErr);
                        //    if (sErr.Trim() == "" || sErr.Trim() == "0")
                        //    {

                        //        fUseQty = fQty;
                        //        //lbl_Out.Text = "�ɳ�����" + fQty.ToString() + "  (�ɳ��� =�����-������)";
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show(sErr);
                        //    }
                        //}
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
                        txt_Dtl_cUnit.Text = drTmp["cUnit"].ToString().Trim();
                        txt_Dtl_cMName.Text = drTmp["cName"].ToString();
                        txt_Dtl_cSpec.Text = drTmp["cSpec"].ToString();
                        dtp_Dtl_dProdDate.Value = DateTime.Now;
                        dtp_Dtl_dProdDate.Focus();    
                        grdDtl.Visible = false;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void grdDtl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Return)
            {
                grdDtl_CellDoubleClick(null, null);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strBId = "";
            strBId = drvItem["cBNo"].ToString();
            string sCSId = txt_Dtl_cCSId.Text.Trim();
            string sSupplier = txt_Dtl_cSupplier.Text.Trim();
            string sErr = "";
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
            if (txt_Dtl_cBatchNo.Text.Trim() == "")
            {
                MessageBox.Show("�Բ������Ų���Ϊ�գ�");
                txt_Dtl_cBatchNo.SelectAll();
                txt_Dtl_cBatchNo.Focus();
                return;
            }
            if (bIsNew)
            {
                dtp_Dtl_dBadDate.Value = dtp_Dtl_dProdDate.Value.AddDays(GetMaterialKeepDay(txt_Dtl_cMNo.Text.Trim()));
            }
            switch (_BClass)
            {
                case 11://������յ�
                    #region
                    if (this.txt_Dtl_fAccept.Text.Trim() == "")
                    {
                        MessageBox.Show("�Բ���������յĵ�����������Ϊ�գ�");
                        txt_Dtl_fAccept.Focus();
                        return;
                    }
                    if (!IsNumberic(this.txt_Dtl_fAccept.Text.Trim()))
                    {
                        MessageBox.Show("�Բ���������յĵ�������¼��������¼����ȷ��������");
                        txt_Dtl_fAccept.SelectAll();
                        txt_Dtl_fAccept.Focus();
                        return;
                    }

                    if (this.txt_Dtl_fOK.Text.Trim() == "")
                    {
                        MessageBox.Show("�Բ���������յĺϸ���������Ϊ�գ�");
                        txt_Dtl_fOK.Focus();
                        return;
                    }
                    if (!IsNumberic(this.txt_Dtl_fOK.Text.Trim()))
                    {
                        MessageBox.Show("�Բ���������յĺϸ�����¼��������¼����ȷ��������");
                        txt_Dtl_fOK.SelectAll();
                        txt_Dtl_fOK.Focus();
                        return;
                    }

                    if (this.txt_Dtl_fBad.Text.Trim() == "")
                    {
                        MessageBox.Show("�Բ���������յĲ��ϸ���������Ϊ�գ�");
                        txt_Dtl_fBad.Focus();
                        return;
                    }
                    if (!IsNumberic(this.txt_Dtl_fBad.Text.Trim()))
                    {
                        MessageBox.Show("�Բ���������յĲ��ϸ�����¼��������¼����ȷ��������");
                        txt_Dtl_fBad.SelectAll();
                        txt_Dtl_fBad.Focus();
                        return;
                    }
                    if (double.Parse(txt_Dtl_fBad.Text.Trim()) < 0)
                    {
                        MessageBox.Show("�Բ���������յĵ��ϸ��������ܴ��ڵ���������");
                        txt_Dtl_fOK.SelectAll();
                        txt_Dtl_fOK.Focus();
                        return;
                    }
                    if (double.Parse(txt_Dtl_fAccept.Text.Trim()) != (double.Parse(txt_Dtl_fOK.Text.Trim()) + double.Parse(txt_Dtl_fBad.Text.Trim())))
                    {
                        MessageBox.Show("�Բ���������յĵ������� ������ �ϸ����� ���� ���ϸ�������");
                        txt_Dtl_fAccept.SelectAll();
                        txt_Dtl_fAccept.Focus();
                    }
                    #endregion
                    break;
            }
            //�˻��������
            if (_BType.Trim() == "102" || _BType.Trim() == "1102")
            {
                if (DBFuns.sp_CheckBackInDtl(AppInformation.SvrSocket, strBId,_BClass, txt_Dtl_cMNo.Text.Trim(), txt_Dtl_cBatchNo.Text.Trim(),
                    double.Parse(txt_Dtl_fQty.Text.Trim()), txt_Dtl_cFromNo.Text.Trim(), out sErr) == "-1")
                {
                    MessageBox.Show(sErr);
                    txt_Dtl_fQty.SelectAll();
                    txt_Dtl_fQty.Focus();
                    return;
                }
            }
            if (txt_Dtl_cCSId.Text.Trim() == "" && nMatClass != 0 )
            {
                MessageBox.Show("�Բ��𣬹�Ӧ�̻������̲���Ϊ�գ���ѡ��");
                return;
            }
            UIToDataRowView(drvItem, pnlDtlEdit);
            if (_BClass == 11)
            {
                UIToDataRowView(drvItem, grp_BackIn);
                UIToDataRowView(drvItem, grp_ChkIn);
            }
            if (bIsNew == true) //���������
            {
               
                bIsResultOK = true;
                nQCState = GetMaterialQCState(drvItem["cMNo"].ToString().Trim());
                drvItem.BeginEdit();
                if (_BClass == 1)
                {
                    drvItem["nQCStatus"] = nQCState;                    
                }
                drvItem.EndEdit();
                bIsShowGrid = false;
                DataRowViewToUI(drvItem, pnlDtlEdit);
                bIsShowGrid = true;
                drvItem["nItem"] = GetNewItem(strBId);
                if (_BType.Trim().ToLower() != "102" && _BType.Trim().ToLower() !="1102") //�˻����
                {
                    drvItem["cBNoIn"] = drvItem["cBNo"];
                    drvItem["nItemIn"] = drvItem["nItem"];
                }
                string sql = "";
                if (_BClass == 11)
                {
                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvItem, "TWB_BillChkAcceptDtl", "cBNo,nItem", "cMName,cSpec,cQCStatus,cPStatus,cMatStyle,cMatQCLevel,cMatOther,cMRemark", true);
                }
                else
                {

                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvItem, "TWB_BillInDtl", "cBNo,nItem", "cMName,cSpec,cQCStatus,cPStatus,cMatStyle,cMatQCLevel,cMatOther", true);
                }
                string err = "";
                DataSet ds = PubDBCommFuns.GetDataBySql(sql, DBCommInfo.DBSQLCommandInfo.GetFieldsForDate(drvItem), out err);
                bIsResultOK = ds.Tables[0].Rows[0][0].ToString() == "0";
                //this.Close();
                if (bIsResultOK)
                {
                    MessageBox.Show("������ϸ�ɹ���");
                    bIsNew = true;
                    ClearUIValues(pnlDtlEdit);
                    drvItem["cBNo"] = strBId;
                    drvItem["cMNo"] = "";
                    DataRowViewToUI(drvItem, pnlDtlEdit);
                    txt_Dtl_cSupplier.Text = sSupplier;
                    txt_Dtl_cCSId.Text = sCSId;
                    txt_Dtl_cMNo.SelectAll();
                    txt_Dtl_cMNo.Focus();
                }
            }
            else //�޸�
            {
                bIsShowGrid = false;
                DataRowViewToUI(drvItem, pnlDtlEdit);
                bIsShowGrid = true;
                string sql = "";
                if (_BClass == 11)
                {
                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvItem, "TWB_BillChkAcceptDtl", "cBNo,nItem", "cMName,cSpec,cQCStatus,cPStatus,cMatStyle,cMatQCLevel,cMatOther,cMRemark", false);
                }
                else
                {

                    sql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvItem, "TWB_BillInDtl", "cBNo,nItem", "cMName,cSpec,cQCStatus,cPStatus,cMatStyle,cMatQCLevel,cMatOther", false);
                }
                string err = "";
                DataSet ds = PubDBCommFuns.GetDataBySql(sql, DBCommInfo.DBSQLCommandInfo.GetFieldsForDate(drvItem), out err);
                bIsResultOK = ds.Tables[0].Rows[0][0].ToString() == "0";
                this.Close();
            }
        }

        private void txt_Dtl_cMNo_ReadOnlyChanged(object sender, EventArgs e)
        {
            ChangeTextBoxBkColorByReadOnly(sender, ((System.Windows.Forms.Control)sender).Parent.BackColor, Color.White);
        }

        private void txt_Dtl_cMNo_TextChanged(object sender, EventArgs e)
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

        private void txt_Dtl_fOK_TextChanged(object sender, EventArgs e)
        {
            double fQty0 = 0;
            double fQtyOK = 0;
            double fQtyBad = 0;
            #region
            if (txt_Dtl_fAccept.Text.Trim() == "")
            {
                MessageBox.Show("��������Ϊ�գ�");
                txt_Dtl_fAccept.Focus();
                return;
            }
            if (!IsNumberic(txt_Dtl_fAccept.Text.Trim()))
            {
                MessageBox.Show("¼�뵽������Ϊ�Ƿ�������������¼�룡");
                txt_Dtl_fAccept.SelectAll();
                txt_Dtl_fAccept.Focus();
                return;
            }
            fQty0 = double.Parse(txt_Dtl_fAccept.Text.Trim());
            #endregion

            #region
            if (this.txt_Dtl_fOK.Text.Trim() == "")
            {
                MessageBox.Show("�ϸ�����Ϊ�գ�");
                txt_Dtl_fOK.Focus();
                return;
            }
            if (!IsNumberic(txt_Dtl_fOK.Text.Trim()))
            {
                MessageBox.Show("¼��ϸ�����Ϊ�Ƿ�������������¼�룡");
                txt_Dtl_fOK.SelectAll();
                txt_Dtl_fOK.Focus();
                return;
            }
            fQtyOK = double.Parse(txt_Dtl_fOK.Text.Trim());
            #endregion

            fQtyBad = fQty0 - fQtyOK;
            txt_Dtl_fBad.Text = fQtyBad.ToString();

        }

        private void btn_SelSupplier_Click(object sender, EventArgs e)
        {
            string sX = _BType.Trim();
            switch (sX)
            {
                case "101"://�ɹ����
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstSupplier, 0, -1, "", doSelCuSupplier);
                    break;
                case "105"://�������
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstSupplier, 1, -1, "", doSelCuSupplier);
                    break;
            }
        }

        private void btn_SelFromNo_Click(object sender, EventArgs e)
        {
            WareStore.SelectIOStoreBillData(AppInformation, UserInformation, 2, "", drvItem["cMNo"].ToString(), new WareStoreMS.DoSelIOStoreMatBillDataEvent(doSelIOStoreMatBillData));
        }

        private void frmItemEditorIn_Load(object sender, EventArgs e)
        {
            grdDtl.Top =pnlDtlEdit.Top +  txt_Dtl_cMNo.Top + txt_Dtl_cMNo.Height;
            grdDtl.Left =pnlDtlEdit.Left+ txt_Dtl_cMNo.Left;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_cRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
               
            }
        }

        private void txt_cRemark_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char) 13)
            {
                 if (grp_ChkIn.Visible)
                {
                    txt_Dtl_fAccept.Focus();
                    txt_Dtl_fAccept.SelectAll();
                }
                else
                {                    
                    if (btnOK.Enabled)  btnOK.Select();                                       
                }
            }
        }

        private void txt_cRemark_Leave(object sender, EventArgs e)
        {
            IsEnterAsTabKey = true;
        }

        private void txt_cRemark_Enter(object sender, EventArgs e)
        {
            IsEnterAsTabKey = false;
        }
    }
}

