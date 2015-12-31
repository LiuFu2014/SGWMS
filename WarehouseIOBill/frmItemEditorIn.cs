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

        #region 私有变量
        bool bIsNew = true;
        private DataRowView drvItem;
        private DoEditItemInfo doItem;
        private bool bIsShowGrid = false;
        private DataSet dsItemList = new DataSet();
        private int nQCState = 1;//质检状态 默认为1
        private bool bIsResultOK = false; //表示已经编辑成功
        /// <summary>
        /// 0 普通物料,1医药物品，2食品，3化妆品
        /// </summary>
        private int nMatClass = 0; //

        private double fUseQty = 0;
        #endregion

        #region 私有方法

        /// <summary>
        /// 判断物料编码是否合法
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
                MessageBox.Show("对不起，退货物料与出库单物料不一致！");
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

        #region 公共属性

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
                    case 11://入库验收
                        grp_ChkIn.Visible = true;
                        Text = "入库验收物料编辑器";
                        break ;
                    default :
                        grp_ChkIn.Visible = false;
                        Text = "入库物料编辑器";
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
                    case "102"://退货入库
                        grp_BackIn.Visible = true;
                        btn_SelSupplier.Enabled = false;
                        Text =  "退货"+Text;
                        break;
                    case "1102"://退货入库验收
                        grp_BackIn.Visible = true;
                        btn_SelSupplier.Enabled = false;
                        Text = "退货" + Text;
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
        /// 仓库号
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

        #region 公共方法
        public void DataRowToUI()
        {
            if (drvItem == null)
            {
                MessageBox.Show("对不起，物料明细数据行对象为空！");
            }
            else
            {
                bIsShowGrid = false;
                DataRowViewToUI(drvItem, pnlDtlEdit);
                bIsShowGrid = true;
                if ((!bIsNew))
                {
                    //获取可用数量
                    if (txt_Dtl_fQty.Text.Trim() != "")
                    {
                        string sErr = "";
                        double fQty = double.Parse(txt_Dtl_fQty.Text.Trim());
                        fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text.Trim(), _WHId.Trim(), _QCStatus, "", fQty, out sErr);
                        if (sErr.Trim() == "" || sErr.Trim() == "0")
                        {

                            fUseQty = fQty;
                            //lbl_Out.Text = "可出数：" + fQty.ToString() + "  (可出数 =库存数-待出数)";
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
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_GetDtlSeq :TbName,:PFld,:SeqFld,:PValue";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "TbName";           //参数名称 和实际定义的一致
            par.ParameterValue = sTbName;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "PFld";           //参数名称 和实际定义的一致
            par.ParameterValue = "cBNo";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "SeqFld";           //参数名称 和实际定义的一致
            par.ParameterValue = sFldKey;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "PValue";           //参数名称 和实际定义的一致
            par.ParameterValue = billNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---

            //执行命令
            SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            string sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;
            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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
                MessageBox.Show(" 获取明细序号无结果数据：" + sErr);
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
                        //    //获取可用数量
                        //    string sErr = "";
                        //    double fQty = 0;
                        //    fQty = PubDBCommFuns.sp_Pack_GetItemBillQty(AppInformation.SvrSocket, 0, txt_Dtl_cMNo.Text.Trim(), _WHId.Trim(), _MatClass.Trim(), _QCStatus, "", 0, out sErr);
                        //    if (sErr.Trim() == "" || sErr.Trim() == "0")
                        //    {

                        //        fUseQty = fQty;
                        //        //lbl_Out.Text = "可出数：" + fQty.ToString() + "  (可出数 =库存数-待出数)";
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
            //检测是否录入全
            if (txt_Dtl_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料编码不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_cMNo.Focus();
                return;
            }
            if (!JudgeMNoIsOK(txt_Dtl_cMNo.Text.Trim()))
            {
                MessageBox.Show("对不起，录入有误，物料编码不存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_cMNo.SelectAll();
                txt_Dtl_cMNo.Focus();
                return;
            }
            if (this.txt_Dtl_fQty.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料数量不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_fQty.Focus();
                return;
            }
            if (!IsNumberic(txt_Dtl_fQty.Text.Trim()))
            {
                MessageBox.Show("对不起，物料数量为非法数值！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_fQty.SelectAll();
                txt_Dtl_fQty.Focus();
                return;
            }
            if (double.Parse(txt_Dtl_fQty.Text.Trim()) == 0)
            {
                MessageBox.Show("对不起，数量不能为0");
                txt_Dtl_fQty.SelectAll();
                txt_Dtl_fQty.Focus();
                return;
            }
            if (txt_Dtl_cUnit.Text.Trim() == "")
            {
                MessageBox.Show("对不起，单位不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Dtl_cUnit.SelectAll();
                txt_Dtl_cUnit.Focus();
                return;
            }
            if (txt_Dtl_cBatchNo.Text.Trim() == "")
            {
                MessageBox.Show("对不起，批号不能为空！");
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
                case 11://入库验收单
                    #region
                    if (this.txt_Dtl_fAccept.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，入库验收的到货数量不能为空！");
                        txt_Dtl_fAccept.Focus();
                        return;
                    }
                    if (!IsNumberic(this.txt_Dtl_fAccept.Text.Trim()))
                    {
                        MessageBox.Show("对不起，入库验收的到货数量录入有误，请录入正确的数量！");
                        txt_Dtl_fAccept.SelectAll();
                        txt_Dtl_fAccept.Focus();
                        return;
                    }

                    if (this.txt_Dtl_fOK.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，入库验收的合格数量不能为空！");
                        txt_Dtl_fOK.Focus();
                        return;
                    }
                    if (!IsNumberic(this.txt_Dtl_fOK.Text.Trim()))
                    {
                        MessageBox.Show("对不起，入库验收的合格数量录入有误，请录入正确的数量！");
                        txt_Dtl_fOK.SelectAll();
                        txt_Dtl_fOK.Focus();
                        return;
                    }

                    if (this.txt_Dtl_fBad.Text.Trim() == "")
                    {
                        MessageBox.Show("对不起，入库验收的不合格数量不能为空！");
                        txt_Dtl_fBad.Focus();
                        return;
                    }
                    if (!IsNumberic(this.txt_Dtl_fBad.Text.Trim()))
                    {
                        MessageBox.Show("对不起，入库验收的不合格数量录入有误，请录入正确的数量！");
                        txt_Dtl_fBad.SelectAll();
                        txt_Dtl_fBad.Focus();
                        return;
                    }
                    if (double.Parse(txt_Dtl_fBad.Text.Trim()) < 0)
                    {
                        MessageBox.Show("对不起，入库验收的到合格数量不能大于到货数量！");
                        txt_Dtl_fOK.SelectAll();
                        txt_Dtl_fOK.Focus();
                        return;
                    }
                    if (double.Parse(txt_Dtl_fAccept.Text.Trim()) != (double.Parse(txt_Dtl_fOK.Text.Trim()) + double.Parse(txt_Dtl_fBad.Text.Trim())))
                    {
                        MessageBox.Show("对不起，入库验收的到货数量 不等于 合格数量 加上 不合格数量！");
                        txt_Dtl_fAccept.SelectAll();
                        txt_Dtl_fAccept.Focus();
                    }
                    #endregion
                    break;
            }
            //退货入库的情况
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
                MessageBox.Show("对不起，供应商或生产商不能为空，请选择！");
                return;
            }
            UIToDataRowView(drvItem, pnlDtlEdit);
            if (_BClass == 11)
            {
                UIToDataRowView(drvItem, grp_BackIn);
                UIToDataRowView(drvItem, grp_ChkIn);
            }
            if (bIsNew == true) //新增的情况
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
                if (_BType.Trim().ToLower() != "102" && _BType.Trim().ToLower() !="1102") //退货入库
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
                    MessageBox.Show("增加明细成功！");
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
            else //修改
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
                MessageBox.Show("到货数量为空！");
                txt_Dtl_fAccept.Focus();
                return;
            }
            if (!IsNumberic(txt_Dtl_fAccept.Text.Trim()))
            {
                MessageBox.Show("录入到货数量为非法数量，请重新录入！");
                txt_Dtl_fAccept.SelectAll();
                txt_Dtl_fAccept.Focus();
                return;
            }
            fQty0 = double.Parse(txt_Dtl_fAccept.Text.Trim());
            #endregion

            #region
            if (this.txt_Dtl_fOK.Text.Trim() == "")
            {
                MessageBox.Show("合格数量为空！");
                txt_Dtl_fOK.Focus();
                return;
            }
            if (!IsNumberic(txt_Dtl_fOK.Text.Trim()))
            {
                MessageBox.Show("录入合格数量为非法数量，请重新录入！");
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
                case "101"://采购入库
                    App.UserManager.SelectCuSupplier(AppInformation, UserInformation, UserMS.CSType.cstSupplier, 0, -1, "", doSelCuSupplier);
                    break;
                case "105"://生产入库
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

