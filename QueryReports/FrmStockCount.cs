using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using SunEast;
using System.Collections;
using QueryReports.Impi;

namespace SunEast.App
{
    public partial class FrmStockCount : UI.FrmSTable
    {
        public static string CountType="0";
        private string WHId="";
        private string Pallet="";
        private string MNo="";
        private string BatchNo="";
        private string Qc="";
        public static  DataSet dsX=new DataSet();
        DataTable mydt = new DataTable();
        DataTable mydtAll = new DataTable();

        private bool bIsOpenPltList = false;
        private bool bIsOpenWHList = false;


        public FrmStockCount()
        {
            InitializeComponent();
        }
        public void InitCmb()
        {
            string strSql = "select * from TWC_WareHouse where bUsed=1 ";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            string err = "";
            DataTable tbWare = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
                if (err != ""&&err!="0")
                {
                    MyTools.MessageBox(err);
                }
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    cmbWHId.DisplayMember = "cName";
                    cmbWHId.ValueMember = "cWHId";
                    cmbWHId.DataSource = tbWare;
                    cmbWHId.SelectedIndex = -1;
                }
            }
            catch (Exception er)
            {
                MyTools.MessageBox(er.Message);
            } 
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void BindMatType()
        {
            //select CTYPEID,CTYPENAME from TPC_MATERIALTYPE
            string strSql = string.Format("select CTYPEID,CTYPENAME from TPC_MATERIALTYPE ");
        
            string err = "";
            DataTable tbWare = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
                if (err != "" && err != "0")
                {
                    MyTools.MessageBox(err);
                }
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    this.cmb_cTypeId1.DisplayMember = "CTYPENAME";
                    this.cmb_cTypeId1.ValueMember = "CTYPEID";
                    this.cmb_cTypeId1.DataSource = tbWare;
                    this.cmb_cTypeId1.SelectedIndex = -1;
                }
            }
            catch (Exception er)
            {
                MyTools.MessageBox(er.Message);
            }
        }

        /// <summary>
        /// �󶨻�������Ϣ
        /// </summary>
        private void BindAreaName()
        {
            //select CAREAID,CAREANAME from TWC_WAREA
            string strSql = string.Format("select CAREAID,CAREANAME from TWC_WAREA ");

            string err = "";
            DataTable tbWare = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
                if (err != "" && err != "0")
                {
                    MyTools.MessageBox(err);
                }
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    this.cmbArea.DisplayMember = "CAREANAME";
                    this.cmbArea.ValueMember = "CAREAID";
                    this.cmbArea.DataSource = tbWare;
                    this.cmbArea.SelectedIndex = -1;
                }
            }
            catch (Exception er)
            {
                MyTools.MessageBox(er.Message);
            }
        }

        public DataSet GetDataSet()
        {
            if (cmbWHId.Text != "")
                WHId = cmbWHId.SelectedValue.ToString();
            else
            {
                //MessageBox.Show("�Բ��𣬲ֿⲻ��Ϊ�գ�");
                WHId = "";
            }
            Pallet = txtPalletId.Text.ToString();
            MNo = txtMNo.Text.ToString();
            BatchNo = txtBatchNo.Text.ToString();
            if (cmbQCStatus.Text != "")
                Qc = cmbQCStatus.SelectedValue.ToString();
            else
                Qc = "";
            try
            {
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "sp_GetWareHouseItemList :nCountType,:pWHId,:pPalletId,:pMNo,:pBatchNo,:pQCStatus";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "nCountType";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = CountType.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Int;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = WHId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = Pallet;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = MNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pBatchNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = BatchNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pQCStatus";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = Qc;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Int;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);

                //------
                //ִ������
                SunEast.SeDBClient sdcX = new SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                string sErr = "";
                DataSet dsY = null;
                cmdInfo.DataTableName = "StockDtl"+CountType;
                dsY = sdcX.GetDataSet(AppInformation.SvrSocket,cmdInfo,false, out sErr);; //sdcX.GetDataSet(cmdInfo, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                FrmStockDtlRpt.dsRpt = dsY;
                return dsY;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
       
        private void FrmStockDtl_Load(object sender, EventArgs e)
        { 
            grdPallet.AutoGenerateColumns = false; 
            grdAll.AutoGenerateColumns = false;
            dgvPalletRece.AutoGenerateColumns = false;
            dgvWhRece.AutoGenerateColumns = false;

            InitCmb();
            BindMatType();
            BindAreaName(); 
        }

        

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        { 
            btnFindInfo_Click(null, null);
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        { 
            btnFindInfo_Click(null,null);
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            btnPrintReceSum_Click(null,null);
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            string fileName= SelectFileExporExcel.GetSaveFileNameByDiag();
            if (fileName == "")
            {
                return;
            }

            switch (tbcMain.SelectedIndex.ToString())
            {
                case "0":
                    DataImpExp.DataIE.DataGridViewToExcel(grdPallet, fileName, tbcMain.SelectedTab.Text);
                    break;
                case "1":
                    DataImpExp.DataIE.DataGridViewToExcel(grdAll, fileName, tbcMain.SelectedTab.Text);
                    break;             
            }
        
        }
       
        private void btnFindInfo_Click(object sender, EventArgs e)
        {
            if (chk_Date.Checked && (dtp_To.Value.Date < dtp_From.Value.Date))
            {
                MessageBox.Show("�Բ�����ʼ���ڲ��ܴ��ڽ�ֹ���ڣ�");
                return;
            }
            switch (tbcMain.SelectedIndex.ToString())
            {
                case "0":
                    mydt = GetDataForPalletCount();
                    bIsOpenPltList = false;
                    bdsPalletList.DataSource = mydt;
                    this.grdPallet.DataSource = bdsPalletList;
                    bIsOpenPltList = true;
                    bdsPalletList_PositionChanged(null, null);
                    break;
                case "1":
                    mydtAll = GetDataForAllMatCount();//���е����ϣ��������̷���
                    bIsOpenWHList = false;
                    bdsWHList.DataSource = mydtAll;
                    this.grdAll.DataSource = bdsWHList;
                    bIsOpenWHList = true;
                    bdsWHList_PositionChanged(null, null);
                    break;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAllTxtValue();
        }

        private void btnPrintSum_Click(object sender, EventArgs e)
        {

            FrmRptShow fsdtl = new FrmRptShow();
            FrmRptShow.rptds = new DataSet();
            //������ͳ��
            FrmRptShow.rptType = "stockCountByPall";
            DataTable tempDt = GetDataForPalletCount();
            FrmRptShow.rptds.Tables.Add(tempDt.Copy());
            FrmRptShow.rptds.Tables[0].TableName = "stockCountByPall";

            fsdtl.Text = "���ͳ�ơ�������ͳ�ơ�";

            fsdtl.ShowDialog();
        }

        private void btnPrintReceSum_Click(object sender, EventArgs e)
        {
            FrmRptShow fsdtl = new FrmRptShow();
            FrmRptShow.rptds = new DataSet();
            //��������
            FrmRptShow.rptType = "stockCountByAll";
            DataTable tempDt = GetDataForAllMatCount();
            FrmRptShow.rptds.Tables.Add(tempDt.Copy());
            FrmRptShow.rptds.Tables[0].TableName = "stockCountByAll";

            fsdtl.Text = "���ͳ�ơ����ֿ�ͳ�ơ�";

            fsdtl.ShowDialog();
        }

        /// <summary>
        /// ������в�ѯ����
        /// </summary>
        private void clearAllTxtValue()
        {
            this.txt_cMatOther.Text = "";
            this.txt_cMatStyle.Text = "";
            this.txt_cName.Text = "";


            this.cmbArea.Text = "";
            this.txt_cSpec.Text = "";
            this.txt_cSupplier.Text = "";
            this.txtBatchNo.Text = "";
            this.txtMNo.Text = "";
            this.txtPalletId.Text = "";

            this.cmb_cTypeId1.Text = "";
            this.cmbQCStatus.Text = "";
            this.cmbWHId.Text = "";
        }
        /// <summary>
        /// ���ϵ����� 
        /// </summary>
        private string cMName = "";
        /// <summary>
        /// �����ұ���
        /// </summary>
        private string cItemId = "";
        /// <summary>
        /// ���
        /// </summary>
        private string cSpec = "";
        /// <summary>
        /// ����
        /// </summary>
        private string cBatchNo = "";
        /// <summary>
        /// ��������
        /// </summary>
        private string cMatOther = "";
        /// <summary>
        /// ��ʽ
        /// </summary>
        private string cMatStyle = "";
        /// <summary>
        /// ���
        /// </summary>
        private string cBoxId = "";
        /// <summary>
        /// ����״̬
        /// </summary>
        private string cQCStatus = "";
        /// <summary>
        /// ��ʽ
        /// </summary>
        private string cTypeId1 = "";
        /// <summary>
        /// �������
        /// </summary>
        private string cTypeId2 = "";
        /// <summary>
        /// ��Ӧ��
        /// </summary>
        private string cDept = "";
        /// <summary>
        /// ����
        /// </summary>
        private string nPalletId = "";
        /// <summary>
        /// �ֿ�
        /// </summary>
        private string cWhid = "";
        /// <summary>
        /// ��������
        /// </summary>
        private string cAreaName = "";
        /// <summary>
        /// ȡ�ÿؼ���ֵ
        /// </summary>
        private void GetTxtValue()
        {
            this.cMatOther = this.txt_cMatOther.Text.Trim();
            this.cMatStyle = this.txt_cMatStyle.Text.Trim();
            this.cMName = this.txt_cName.Text.Trim();
            this.cAreaName = this.cmbArea.Text.Trim();
            //if (cAreaName != "")
            //{
            //    if (this.cmbArea.SelectedIndex != -1)
            //    {
            //        cAreaName = this.cmbArea.SelectedValue.ToString();
            //    }
            //    cAreaName = this.cmbArea.Text.Trim();
            //}
            this.cSpec = this.txt_cSpec.Text.Trim();
            this.cDept = this.txt_cSupplier.Text.Trim();
            this.cItemId = this.txtMNo.Text.Trim();
            this.cBatchNo = this.txtBatchNo.Text.Trim();
            //this.cBoxId=this.txtb
   
          
            //this.cTypeId2=this.txt
            this.nPalletId = this.txtPalletId.Text.Trim();

            this.cWhid = this.cmbWHId.Text.Trim();
            if (cWhid != "")
            {
                if (this.cmbWHId.SelectedIndex != -1)
                {
                    this.cWhid = this.cmbWHId.SelectedValue.ToString();
                }
                
            }
            this.cQCStatus = this.cmbQCStatus.Text.Trim();            
            this.cTypeId1 = this.cmb_cTypeId1.Text.Trim();
            if (cTypeId1 != "")
            {
                if (this.cmb_cTypeId1.SelectedIndex != -1)
                {
                    cTypeId1 = this.cmb_cTypeId1.SelectedValue.ToString();
                }
               
            }
        }

        /// <summary>
        /// ������ͳ�Ƶ���ϸ����
        /// </summary>
        /// <param name="pallStr"></param>
        /// <returns></returns>
        private DataTable GetDataAtPalltDtl(string pallStr,string sMNo,string sBatchNo)
        {

            string err = "";
            string sql = "";
            sql = "select * from V_StoreItemList where nPalletId='" + pallStr + "' and cMNo='" + sMNo + "' ";
            if (cbxIscBatchNoGroup.Checked)
            {
                sql += (" and cBatchNo='" + sBatchNo + "' ");
            }
            
            
            DataSet dsY = PubDBCommFuns.GetDataBySql(sql, out err);
            if (err == "" || err == "0")
            {
                return dsY.Tables["data"];
            }
            else
            {
                MyTools.MessageBox(err);
                return new DataTable();
            }
        }

        /// <summary>
        /// ���ֿ�ͳ�����ϵ���ϸ����
        /// </summary>
        /// <param name="sMNo"></param>
        /// <param name="sBatchNo"></param>
        /// <returns></returns>
        private DataTable GetDataAtMatDtl(string sMNo, string sBatchNo)
        {
            GetTxtValue();
            string err = "";
            string sql = "";
            sql = "select * from V_StoreItemList where cMNo='" + sMNo + "' ";

            sql += whereInfoStr();
            //sql += whereInfoStr1();

            if (cbxIscBatchNoGroup.Checked)
            {
                sql += (" and cBatchNo='" + sBatchNo + "' ");
            }

            DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dInTime", out err);
            if (err == "" || err == "0")
            {
                return dsY.Tables["data"];
            }
            else
            {
                MyTools.MessageBox(err);
                return new DataTable();
            }
        }

        /// <summary>
        /// ������ͳ������
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataForPalletCount( )
        {
                       
            GetTxtValue();
            string err = "";
            string sql = "";
            if (cbxIscBatchNoGroup.Checked)
            {
                sql = string.Format("select * from (select cItemId ,max(cMName) cMName,max(cSpec) cSpec, cBatchNo,sum(fQty) fQty ,max(cUnit) cUnit,max(cMatOther) cMatOther,max(cMatStyle) cMatStyle, nPalletId,max(cBoxId) cBoxId, max(cQCStatus) cQCStatus,max(cTypeId1) cTypeId1,max(cTypeId2) cTypeId2,max(cDept) cDept from V_StoreItemList where 1=1 ");
            }
            else
            {
                sql = string.Format("select * from (select cItemId,max(cMName) cMName,max(cSpec) cSpec,' ' cBatchNo,sum(fQty) fQty ,max(cUnit) cUnit,max(cMatOther) cMatOther,max(cMatStyle) cMatStyle, nPalletId,max(cBoxId) cBoxId, max(cQCStatus) cQCStatus,max(cTypeId1) cTypeId1,max(cTypeId2) cTypeId2,max(cDept) cDept from V_StoreItemList where 1=1 ");
            }

            sql += whereInfoStr();

            //sql += whereInfoStr1();

            if (cbxIscBatchNoGroup.Checked)
            {
                sql += string.Format(" group by nPalletId,cItemId,cBatchNo  )  stoc ");
            }
            else
            {
                sql += string.Format(" group by nPalletId,cItemId  ) stoc ");
            }

            //sql += whereInfoStr1();

            DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dInTime", out err);
            if (err == "" || err == "0")
            {
                return dsY.Tables["data"];
            }
            else
            {
                MyTools.MessageBox(err);
                return new DataTable();
            }
        }


        private DataTable GetDataForAllMatCount()
        {

            GetTxtValue();
            string err = "";
            string sql = "";
            if (cbxIscBatchNoGroup.Checked)
            {
                sql = string.Format("select * from (select cItemId ,max(cMName) cMName,max(cSpec) cSpec, cBatchNo,sum(fQty) fQty ,max(cUnit) cUnit,max(cMatOther) cMatOther,max(cMatStyle) cMatStyle,  max(cQCStatus) cQCStatus,max(cTypeId1) cTypeId1,max(cTypeId2) cTypeId2,max(cDept) cDept from V_StoreItemList where 1=1 ");
            }
            else
            {
                sql = string.Format("select * from (select cItemId,max(cMName) cMName,max(cSpec) cSpec,' ' cBatchNo,sum(fQty) fQty ,max(cUnit) cUnit,max(cMatOther) cMatOther,max(cMatStyle) cMatStyle, max(cQCStatus) cQCStatus,max(cTypeId1) cTypeId1,max(cTypeId2) cTypeId2,max(cDept) cDept from V_StoreItemList where 1=1 ");
            }

            sql += whereInfoStr();

            //sql += whereInfoStr1();

            if (cbxIscBatchNoGroup.Checked)
            {
                sql += string.Format(" group by cItemId,cBatchNo  )  tt");
            }
            else
            {
                sql += string.Format(" group by cItemId  ) tt ");
            }



            DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dInTime", out err);
            if (err == "" || err == "0")
            {
                return dsY.Tables["data"];
            }
            else
            {
                MyTools.MessageBox(err);
                return new DataTable();
            }
        }

        private DataTable GetDataAtBalltToRpts( )
        {
            /*
             * ȡ�ð����̵ı�������
             */
            string err = "";
            string sql = string.Format("select * from (select cItemId,cMName,cSpec,cBatchNo,sum(fQty) fQty ,max(cUnit) cUnit,max(cMatOther) cMatOther,max(cMatStyle) cMatStyle, nPalletId,max(cBoxId) cBoxId, max(cQCStatus) cQCStatus,max(cTypeId1) cTypeId1,max(cTypeId2) cTypeId2,max(cDept) cDept from V_StoreItemList where 1=1  ");

            sql += whereInfoStr();

            //if (pallStr != "")
            //{
            //    sql += string.Format(" and nPalletId ='{0}' ", pallStr);
            //}
            //sql += whereInfoStr1();

            sql += string.Format(" group by cItemId,cMName,cSpec,cBatchNo,nPalletId) ");

            //sql += whereInfoStr1();

            DataSet dsY = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dInTime", out err);
            if (err == "" || err == "0")
            {
                return dsY.Tables["data"];
            }
            else
            {
                MyTools.MessageBox(err);
                return new DataTable();
            }
        }

        //private string whereInfoStr1()
        //{
        //    #region--�������

        //    string sWhere_User = "";
        //    if (UserInformation.UType != UserType.utSupervisor)
        //    {
        //        sWhere_User = " and cMAreaId in (select cMAreaId from TPB_UserMgrArea where cUserId='" + UserInformation.UserId + "')";
        //    }
        //    #endregion
        //    return sWhere_User;
        //}
        private string whereInfoStr()
        {
            string sql = "";
            #region--�������
            string sWHId = "";
            if (cmbWHId.Text.Trim() != "")
            {
                if (cmbWHId.SelectedValue != null)
                {
                    sWHId = cmbWHId.SelectedValue.ToString();
                }
            }
            if (sWHId != "")
            {
                sql += string.Format(" and cWHId = '{0}' ", sWHId);
            }
            if (cAreaName != "")
            {
                sql += string.Format(" and cAreaName = '{0}' ", cAreaName);
            }
            if (cMName != "")
            {
                sql += string.Format(" and cMName like '%{0}%' ", cMName);
            }
            if (cItemId != "")
            {
                sql += string.Format(" and cItemId like '%{0}%' ", cItemId);
            }
            if (cSpec != "")
            {
                sql += string.Format(" and cSpec like '%{0}%' ", cSpec);
            }
            if (cBatchNo != "")
            {
                sql += string.Format(" and cBatchNo like '%{0}%' ", cBatchNo);
            }
            if (cMatOther != "")
            {
                sql += string.Format(" and isnull(cMatOther,'') like '%{0}%' ", cMatOther);
            }
            if (cMatStyle != "")
            {
                sql += string.Format(" and isnull(cMatStyle,'') like '%{0}%' ", cMatStyle);
            }
            //if (cBoxId != "")
            //{
            //    sql += string.Format(" and isnull(cBoxId,'') like '%{0}%' ", cBoxId);
            //}
            if (cQCStatus != "")
            {
                sql += string.Format(" and cQCStatus = '{0}' ", cQCStatus);
            }
            if (cTypeId1 != "")
            {
                sql += string.Format(" and cTypeId1='{0}' ", cTypeId1);
            }
            if (cTypeId2 != "")
            {
                sql += string.Format(" and cTypeId2 ='{0}' ", cTypeId2);
            }
            if (cDept != "")
            {
                sql += string.Format(" and isnull(cDept,'') like '%{0}%' ", cDept);
            }
            if (nPalletId != "")
            {
                sql += string.Format(" and nPalletId ='{0}' ", nPalletId);
            }

            if (chk_Date.Checked)
            {
                sql = sql + " and ( dInTime >= '"+ dtp_From.Value.ToString("yyyy-MM-dd 00:00:00") +"'  and dInTime <='"+ dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") +"' )";
            }
            #endregion

            return sql;
        }

        private void grdPallet_SelectionChanged(object sender, EventArgs e)
        {
            ////
            //if (this.grdPallet.SelectedRows.Count != 0)
            //{
            //    if (this.grdPallet.SelectedRows[0].Cells["cPalletId"].Value != null)
            //    {
            //        string selPallStr = this.grdPallet.SelectedRows[0].Cells["cPalletId"].Value.ToString();

            //        if (selPallStr != "")
            //        {
            //            DataTable mydt = GetDataAtBalltRece(selPallStr);
            //            this.dgvPalletRece.DataSource = mydt;
            //        }
            //    }
            //}
            //else
            //{
            //    this.dgvPalletRece.DataSource = null;
            //}
        }

        private void grdAll_SelectionChanged(object sender, EventArgs e)
        {
            //
            //if (this.grdAll.SelectedRows.Count != 0)
            //{
            //    if (this.grdAll.SelectedRows[0].Cells["cItemIdAll"].Value != null)
            //    {
            //        string selPallStr = this.grdAll.SelectedRows[0].Cells["cItemIdAll"].Value.ToString();

            //        if (selPallStr != "")
            //        {
            //            DataTable mydt = GetDataAtAllRece(selPallStr);
            //            DisCountNum(mydt);
            //            this.dgvWhRece.DataSource = mydt;
            //        }
            //    }
            //}
            //else
            //{
            //    this.dgvWhRece.DataSource = null;
            //}
        }

        private void DisCountNum(DataTable mydt)
        {
            double count = 0;
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                count += Convert.ToDouble(mydt.Rows[i]["fQty"].ToString());
            }
            this.lblCountNum.Text = count + "";
            this.lblCount.Text = mydt.Rows.Count + "";
        }

        private void bdsPalletList_PositionChanged(object sender, EventArgs e)
        {
            string sPltNo = "";
            string sMNo = "";
            string sBatchNo = "";
            if (!bIsOpenPltList) return;
            DataRowView drvX = null;
            if (bdsPalletList.Count > 0)
            {
                drvX = (DataRowView)bdsPalletList.Current;
                if (drvX != null)
                {
                    sPltNo = drvX["nPalletId"].ToString();
                    sMNo = drvX["cItemId"].ToString();
                    if (drvX["cBatchNo"] != null)
                    {
                        sBatchNo = drvX["cBatchNo"].ToString();
                    }
                }
            }
            DataTable tbX = GetDataAtPalltDtl(sPltNo, sMNo, sBatchNo);
            dgvPalletRece.DataSource = tbX;

        }

        private void bdsWHList_PositionChanged(object sender, EventArgs e)
        {
            string sMNo = "";
            string sBatchNo = "";
            if (!bIsOpenWHList) return;
            DataRowView drvX = null;
            if (this.bdsWHList.Count > 0)
            {
                drvX = (DataRowView)bdsWHList.Current;
                if (drvX != null)
                {
                    sMNo = drvX["cItemId"].ToString();
                    if (drvX["cBatchNo"] != null)
                    {
                        sBatchNo = drvX["cBatchNo"].ToString();
                    }
                }
            }
            DataTable tbX = GetDataAtMatDtl( sMNo, sBatchNo);
            this.dgvWhRece.DataSource = tbX;
        }

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindInfo_Click(null, null);
        }
    }
}

