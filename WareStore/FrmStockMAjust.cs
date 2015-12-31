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
    public partial class FrmStockMAjust : UI.FrmSTable
    {
        public FrmStockMAjust()
        {
            InitializeComponent();
        }
        #region 私有变量
        string strTbNameMain = "TWB_BillAjust";
        string strKeyFld = "cBNo";
        string strTbNameDetail = "TWB_BillAjustDtl";
        string strKeyFldDetail = "cBNo";
        DataSet DBDateSetDetail = null;
        bool bDSIsOpenForMain = false;
        OperateType OptMain = OperateType.optNone;
        OperateType OptDetail= OperateType.optNone;
        StringBuilder sbCondition = new StringBuilder("");
        #endregion

        private void LoadCommboxItemByValue()
        {
            ArrayList ArrStatus = new ArrayList();
            ArrStatus.Add(new DictionaryEntry(0, "未过账"));
            ArrStatus.Add(new DictionaryEntry(1, "部分过账"));
            ArrStatus.Add(new DictionaryEntry(2, "过账完成"));

            comboBox_nStatus.DisplayMember = "Value";
            comboBox_nStatus.ValueMember = "Key";
            comboBox_nStatus.DataSource = ArrStatus;

            ArrayList ArrState2 = new ArrayList();
            ArrState2.Add(new DictionaryEntry(0, "未过账"));
            ArrState2.Add(new DictionaryEntry(1, "部分过账"));
            ArrState2.Add(new DictionaryEntry(2, "过账完成"));
            this.cmbQ_Status.DisplayMember = "Value";
            cmbQ_Status.ValueMember = "Key";
            cmbQ_Status.DataSource = ArrState2;

            //ArrayList ArrIsChecked= new ArrayList();
            //ArrIsChecked.Add(new DictionaryEntry(0, "未审核"));
            //ArrIsChecked.Add(new DictionaryEntry(1, "已审核"));

            //comboBox_bIsChecked.DisplayMember = "Value";
            //comboBox_bIsChecked.ValueMember = "Key";
            //comboBox_bIsChecked.DataSource = ArrIsChecked;

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
            DataSet ds = PubDBCommFuns.GetDataBySql(sqlStr, out errStr); //UserManager.GetDataSetbySql(sql);

            comboBox_cWHId.DataSource = ds.Tables["data"];
            comboBox_cWHId.DisplayMember = "cName";
            comboBox_cWHId.ValueMember = "cWHId";

            DataTable tbX = ds.Tables["data"].Copy();
            this.cmbQ_WHId.DataSource = tbX ;
            cmbQ_WHId.DisplayMember = "cName";
            cmbQ_WHId.ValueMember = "cWHId";

        }

        private void LoadCheckType(string TypeId)
        {
            //string errStr = "";
            //string sqlStr = "select cBTypeId,cBType  from  TPB_BillType where isnull(bused,1)=1 and nBClass=3";
            //if (TypeId.Trim() != "")
            //{
            //    sqlStr += " where cBTypeId='" + TypeId + "'";
            //}
            //DataSet ds = PubDBCommFuns.GetDataBySql(sqlStr, out errStr); //UserManager.GetDataSetbySql(sql);
            //comboBox_cCheckType.DataSource = ds.Tables["data"];
            //comboBox_cCheckType.DisplayMember = "cBType";
            //comboBox_cCheckType.ValueMember = "cBTypeId";
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
            //控制录入问题     
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
            if (dataGridView_Detail.RowCount < 2) return true;
            bool Result = false;
            string sqlStr = "";
            string errStr = "";

            OptDetail = OperateType.optEdit;
            DataRowView drv = (DataRowView)bindingSource_Detail.Current;
            drv.BeginEdit();
           // CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
            //CtrlControlReadOnly(panel_Edit, true);
            return Result;
        }



        private bool DoDelete()
        {
            bool Result = false;
            int i = -1;
            i = (int)OptMain;
            DataRowView drv = (DataRowView)bindingSource_Main.Current;
            if (drv == null)
            {
                Result = true;
                MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return Result;
            }
            if (i >0 && i < 3)
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Result = true;
                return Result;
            }
            if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.No) return Result;
            if (int.Parse(drv["nStatus"].ToString()) > 0)
            {
                MessageBox.Show("对不起，该调整单已经开始过账，不能删除！");
                return false ;
            }
            //采用存储过程删除单据以保存日志
            bool b = true;
            string sErr = "";
            string sX = PubDBCommFuns.sp_Ajust_DeleteBillData(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, "WMS", drv["cBNo"].ToString().Trim(), out sErr);
            if (sX.Trim() != "" && sX.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
                return false;
            }
               
            if (b)
            {
                OptMain = OperateType.optDelete;
                DoRefresh();
                CtrlOptButtons(this.tlbMain, panel_Edit, OptMain, DBDataSet.Tables[strTbNameMain]);
                OptMain = OperateType.optNone;
                DisplayState(stbState, OptMain);
                CtrlControlReadOnly(panel_Edit, false);
                MessageBox.Show("删除成功！");
            }
            return Result;
            /*
                 int iX = -1;
            iX = (int)optMain;
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (drv.IsNew || drv.IsEdit)
            if ((0 < iX) && (iX < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            bool bX = false;
            string sErr = "";
            string sSql = "delete " + strTbNameMain + " where " + strKeyFld + "='" + drv[strKeyFld].ToString() + "'";   
            DataSet dsX = null;
            //执行语句
            dsX = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            bX = dsX != null;
            bX = dsX.Tables[0].Rows[0][0].ToString() == "0";
            if (bX)
            {
                MessageBox.Show("删除成功！");
                optMain = OperateType.optDelete;
                OpenMainDataSet(sbConndition.ToString());
                //控制录入问题
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                optMain = OperateType.optNone;
                DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, false);
                btn_SetPwd.Enabled = false;
            }
            else
            {
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //控制录入问题
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
                ds = PubDBCommFuns.GetDataBySql(sqlStr, out errStr);
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
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OptMain = OperateType.optNone;
                } 
                else
                {
                    MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }       
            return Result;
        }
        private bool DoSaveDetail()
        {
            if (dataGridView_Detail.RowCount < 2) return true;
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
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OptDetail = OperateType.optNone;
                }
                else
                {
                    MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return Result;
        }



        private bool DoRefresh()
        {

            if (dtp_From.Value > dtp_To.Value)
            {
                MessageBox.Show("对不起，开始日期不能大于截止日期！");
                dtp_From.Focus();
                return false ;
            }
            sbCondition.Remove(0, sbCondition.Length);
            sbCondition.Append(" where (dDate >='" + dtp_From.Value.ToString("yyyy-MM-dd 00:00:00") + "' and dDate <='" + dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") + "')");
            if (cmbQ_WHId.Text.Trim() != "")
            {
                if (cmbQ_WHId.Items.Count > 0)
                {
                    if (cmbQ_WHId.SelectedValue != null)
                    {
                        sbCondition.Append(" and (cWHId='" + cmbQ_WHId.SelectedValue.ToString().Trim() + "')");
                    }
                }
            }
            if (this.cmbQ_Status.Text.Trim() != "")
            {
                if (cmbQ_Status.Items.Count > 0)
                {
                    if (cmbQ_Status.SelectedValue != null)
                    {
                        sbCondition.Append(" and (nStatus =" + cmbQ_Status.SelectedValue.ToString().Trim() + ")");
                    }
                }
            }
            if (txtQ_BNo.Text.Trim() != "")
            {
                sbCondition.Append(" and ( cBNo like '%" + txtQ_BNo.Text.Trim() + "%')");
            }
            BandDataSet(sbCondition.ToString(), dataGridView_Main);
            return true;
        }

        /*
        #region 私有变量
        string strTbNameMain = "TWC_WareHouse";
        string strKeyFld = "cWHId";
        bool bDSIsOpenForMain = false;
        //主表操作
        OperateType optMain = OperateType.optNone;
        //记录当前数据列表的 条件
        StringBuilder sbConndition = new StringBuilder("");
        #endregion
         */
        //public

        private bool BandDataSet(string SqlStrConditon, DataGridView FDataGridView)
        { 
            bool Result = true;
            try
            {
               
                string SqlStr = "";
                string ErrStr = "";
                bDSIsOpenForMain = false;
                FDataGridView.AutoGenerateColumns = false;
                FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DBDataSet.Clear();
                SqlStr = "SELECT * FROM  " + strTbNameMain + " " + SqlStrConditon;
                Cursor.Current = Cursors.WaitCursor;
                DBDataSet = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket,false, SqlStr, strTbNameMain, 0, 0,"dDate,dCreateDate,dCheckDate", out ErrStr);
                
                Result = DBDataSet != null;
                bindingSource_Main.DataSource = DBDataSet.Tables[strTbNameMain]; ;
                FDataGridView.DataSource = bindingSource_Main;
                Cursor.Current = Cursors.Default;
                string sId = "";
                if (bindingSource_Main.Count > 0)
                {
                    DataRowView drv = (DataRowView)bindingSource_Main.Current;
                    sId = drv[strKeyFld].ToString().Trim();
                    try
                    {
                        bDSIsOpenForMain = true;
                        DataRowViewToUI(drv, panel_Edit);
                        OptMain = OperateType.optNone;
                    }
                    catch (Exception e)
                    {
                        bDSIsOpenForMain = false;
                        MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Result = false;
                    }
                }
                BandDataSetDetail(" where cBNo='"+sId.Trim()+"'", dataGridView_Detail);
             
            }
            catch (Exception ei)
            {
                MessageBox.Show(ei.Message);
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
            DBDateSetDetail = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket,false , SqlStr, strTbNameDetail, 0, 0,"", out ErrStr);
            Result = DBDateSetDetail != null;
            bindingSource_Detail.DataSource = DBDateSetDetail.Tables[strTbNameDetail]; ;
            FDataGridView.DataSource = bindingSource_Detail;
            Cursor.Current = Cursors.Default;
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
            #region 权限控制
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
            CheckRights(tlbMain, dsX.Tables["UserRights"]);
            #endregion

            LoadCommboxItemByValue();
            LoadStockList("");
            LoadCheckType("");
            dtp_To.Value = DateTime.Now;
            dtp_From.Value = DateTime.Now.AddDays(-30);
            BandDataSet("", this.dataGridView_Main);
        }

        private void dataGridView_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (!bDSIsOpenForMain) return;
            ClearUIValues(panel_Edit);
            if (!((DataRowView)bindingSource_Main.Current).IsNew)
            {
                DataRowView drv = (DataRowView)bindingSource_Main.Current;
                DataRowViewToUI(drv, panel_Edit);
            }

            DataRowView drvDetail = (DataRowView)bindingSource_Main.Current;
            BandDataSetDetail(" where cBNo='" + drvDetail["cBNo"] + "'", this.dataGridView_Detail);

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

            //FrmStockMCheckFilter frmFilter = new FrmStockMCheckFilter();
            //frmFilter.textBox_UserId.Text = UserInformation.UserId;
            //frmFilter.ShowDialog();
             //;
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            DoSave();
            DoSaveDetail();

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

        private void bindingSource_Main_CurrentChanged(object sender, EventArgs e)
        {
            //if (!bDSIsOpenForMain) return;
            //ClearUIValues(panel_Edit);
            //if (!((DataRowView)bindingSource_Main.Current).IsNew)
            //{
            //    DataRowView drv = (DataRowView)bindingSource_Main.Current;
            //    DataRowViewToUI(drv, panel_Edit);
            //}

            //DataRowView drvDetail = (DataRowView)bindingSource_Main.Current;
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {

        }

        private void bindingSource_Detail_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton_Audit_Click(object sender, EventArgs e)
        {
            mi_DoAccountAll_Click(null, null);

        }



        private void mi_Edit_Click(object sender, EventArgs e)
        {
            DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
            if (drvMain == null) return;
            if (int.Parse(drvMain["nStatus"].ToString()) == 3)
            {
                MessageBox.Show("对不起，该调整单已经被过账，不能增加明细数据！");
                return;
            }
            string sWHId = drvMain["cWHId"].ToString().Trim();
            string sBNo = drvMain["cBNo"].ToString().Trim();
            if (sWHId.Trim() == "")
            {
                MessageBox.Show("对不起，调整单的仓库不能为空！");
                return;
            }
            if (sBNo.Trim() == "" || sBNo.Trim() == "-1" || sBNo.Trim() == "0")
            {
                MessageBox.Show("对不起，请先建调整单并保存后，再建明细！");
                return;
            }
            DataRowView drvDtl = (DataRowView)bindingSource_Detail.Current;
            frmDtlAjust frmX = new frmDtlAjust();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.WHId = sWHId.Trim();
            frmX.DrvItem = drvDtl;
            frmX.BIsNew = false ;
            frmX.ShowDialog();

            //
            if (frmX.BIsResult)
            {
                BandDataSetDetail(" where cBNo='" + sBNo.Trim() + "'", dataGridView_Detail);
            }
            frmX.Dispose();

        }

        private void mi_New_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
                if (drvMain == null) return;
                if (int.Parse(drvMain["nStatus"].ToString()) == 3)
                {
                    MessageBox.Show("对不起，该调整单已经被过账，不能增加明细数据！");
                    return;
                }
                string sWHId = drvMain["cWHId"].ToString().Trim();
                string sBNo = drvMain["cBNo"].ToString().Trim();
                if (sWHId.Trim() == "")
                {
                    MessageBox.Show("对不起，调整单的仓库不能为空！");
                    return;
                }
                if (sBNo.Trim() == "" || sBNo.Trim() == "-1" || sBNo.Trim() == "0")
                {
                    MessageBox.Show("对不起，请先建调整单并保存后，再建明细！");
                    return;
                }
                DataRowView drvDtl =( DataRowView) bindingSource_Detail.AddNew();
                drvDtl["cWHId"] = sWHId;
                drvDtl["cBNo"] = sBNo;
                drvDtl["cUser"] = UserInformation.UserName;
                frmDtlAjust frmX = new frmDtlAjust();
                frmX.AppInformation = AppInformation;
                frmX.UserInformation = UserInformation;
                frmX.WHId = sWHId.Trim();
                frmX.DrvItem = drvDtl;
                frmX.BIsNew = true;
                frmX.ShowDialog();
                
                //
                if (frmX.BIsResult)
                {
                    BandDataSetDetail(" where cBNo='" + sBNo.Trim() + "'", dataGridView_Detail);
                }
                frmX.Dispose();
            }
            catch (Exception ei)
            {
                MessageBox.Show(ei.Message);
            }

        }

        private void tlbMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mi_DoAccount_Click(object sender, EventArgs e)
        {
            DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
            if (drvMain == null)
            {
                MessageBox.Show("无调整单数据可过账！");
                return;
            }
            if (int.Parse(drvMain["nStatus"].ToString()) == 2)
            {
                MessageBox.Show("该调整单：" + drvMain["cBNo"].ToString() +"已经过账");
                return;
            }
            if (bindingSource_Detail.Count == 0)
            {
                MessageBox.Show("无调整明细数据可过账！");
                return;
            }
            DataRowView drvDtl = (DataRowView)bindingSource_Detail.Current;
            if (drvDtl == null)
            {
                MessageBox.Show("无调整明细数据可过账！");
                return;
            }
            string sErr = "";
            string sBNo = drvDtl["cBNo"].ToString().Trim();
            int nItme = int.Parse(drvDtl["nItem"].ToString());
            int nState = int.Parse(drvDtl["nStatus"].ToString());
            if (nState == 1)
            {
                MessageBox.Show("对不起，该明细已经过账！");
                return;
            }
            string sX = PubDBCommFuns.sp_Chk_UpdtQtyFromAjust(AppInformation.SvrSocket, UserInformation.UnitId, UserInformation.UserName, "WMS", sBNo, nItme, out sErr);
            if (sX.Trim() != "" && sX.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
                return;
            }
            BandDataSetDetail(" where cBNo='"+ sBNo +"'", dataGridView_Detail);
        }

        private void mi_DoAccountAll_Click(object sender, EventArgs e)
        {
            DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
            if (drvMain == null)
            {
                MessageBox.Show("无调整单数据可过账！");
                return;
            }
            if (int.Parse(drvMain["nStatus"].ToString()) == 2)
            {
                MessageBox.Show("该调整单：" + drvMain["cBNo"].ToString() + "已经被审核");
                return;
            }
            string sErr = "";
            //string sX = PubDBCommFuns.sp_Chk_UpdtQtyFromAjustB(AppInformation.SvrSocket, UserInformation.UnitId, UserInformation.UserName, "WMS", drvMain["cBNo"].ToString().Trim(), out sErr);
            string sX = PubDBCommFuns.sp_Pack_BillCheck(AppInformation.SvrSocket, int.Parse(drvMain["nBClass"].ToString()), drvMain["cBNo"].ToString(), 0, UserInformation.UserId, UserInformation.UnitId, "WMS", out sErr);            
            if (sX.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            else
            {
                MessageBox.Show("审核成功！");
            }
            DoRefresh();
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbQ_Status.SelectedIndex = -1;
            cmbQ_WHId.SelectedIndex = -1;
            txtQ_BNo.Text = "";
            dtp_To.Value = DateTime.Now;
            dtp_From.Value = DateTime.Now.AddDays(-30);
        }

        private void mi_Delete_Click(object sender, EventArgs e)
        {
            DataRowView drvMain = (DataRowView)bindingSource_Main.Current;
            if (drvMain == null)
            {
                MessageBox.Show("无调整单数据可删除！");
                return;
            }
            if (int.Parse(drvMain["nStatus"].ToString()) == 2)
            {
                MessageBox.Show("该调整单：" + drvMain["cBNo"].ToString() + "已经过账，不能删除");
                return;
            }
            if (bindingSource_Detail.Count == 0)
            {
                MessageBox.Show("无调整明细数据可删除！");
                return;
            }
            DataRowView drvDtl = (DataRowView)bindingSource_Detail.Current;
            if (drvDtl == null)
            {
                MessageBox.Show("无调整明细数据可删除！");
                return;
            }
            string sErr = "";
            string sBNo = drvDtl["cBNo"].ToString().Trim();
            int nItme = int.Parse(drvDtl["nItem"].ToString());
            int nState = int.Parse(drvDtl["nStatus"].ToString());
            if (nState == 1)
            {
                MessageBox.Show("对不起，该明细已经过账，不能删除！");
                return;
            }
            string sX = PubDBCommFuns.sp_Ajust_DeleteBillDtl(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, "WMS", sBNo, nItme, out sErr);
            if (sX.Trim() != "" && sX.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
                return;
            }
            BandDataSetDetail(" where cBNo='" + sBNo + "'", dataGridView_Detail);
        }

        private void tlbSaveSysRts_Click(object sender, EventArgs e)
        {
            #region 工具栏
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

            #region 其他
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

