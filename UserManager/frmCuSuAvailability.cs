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
        #region 私有变量

        string strTbNameMain = "TPB_CuSuAvailability";
        string strKeyFld = "cCSId,cItemName";
        string strConnFix = "";
        //主表操作
        OperateType optMain = OperateType.optNone;
        //记录当前数据列表的 条件
        StringBuilder sbConndition = new StringBuilder("");
        bool bIsMainOpened = false;
        #endregion
        #region 私有方法
        /// <summary>
        /// 根据当前的操作显示当前的操作状态
        /// </summary>
        /// <param name="stbSt"></param>
        /// <param name="optX"></param>
        private void DisplayState(ToolStripLabel stbSt, OperateType optX)
        {
            string strText = "【状态】";
            if (stbSt != null)
            {
                switch (optX)
                {
                    case OperateType.optNew:
                        strText = strText + " 新建";
                        break;
                    case OperateType.optEdit:
                        strText = strText + " 修改";
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
        #region 公共属性

            private string _CSId = "";
            [Description("供应商或客户编号")]
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
                        Text = "供应商/生产厂家" + "—资质合法性维护";
                        lbl_CSTypeDesc.Text = "供应商/生产厂家";
                        break;
                    case 1:
                        Text = "客户" + "—资质合法性维护";
                        break;
                    default :
                        Text = "供应商/生产厂家" + "—资质合法性维护";
                        break;
                }
            }
        }

        #endregion
        #region 公共方法
        public override void InitFormParameters()
        {
            //ModuleRtsId = "2101";
            //ModuleRtsName = "单位信息管理";
            if (ModuleRtsId.Length > 0)
            {
                Text = ModuleRtsName;

            }
            stbModul.Text = "模块【" + Text + "】";
            if (UserInformation != null)
            {
                stbUser.Text = "用户【" + UserInformation.UserName + "】";
            }
            //初始化工具按钮权限标志
            //InitFormTlbBtnTag(tlbMain, ModuleRtsId);
        }
        public void BindDataSetToCtrls()
        {
            //先清掉绑定
            DataSetUnBind(pnlEdit);
            //绑定数据集
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
            //DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            ////cmdInfo.SqlText = "select * from " + strTbNameMain + " where 1=1 " + sCondition + strConnFix;             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加
            //cmdInfo.SqlText = "select * from " + strTbNameMain + " where 1=1 and cCSId='"+ _CSId.Trim() +"'";             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            ////if (UserInformation.UType != UserType.utSupervisor)
            ////{
            ////    cmdInfo.SqlText = cmdInfo.SqlText + " where "+strKeyFld+"='"+ UserInformation.UnitId +"'"; //仅显示当前用户的单位
            ////}
            //cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            //cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            //cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            //cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名
            //SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            ////sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            
            ////DataSet dsX = null;
            ////DataTable tbX= null ;
            //DBDataSet = sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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
            //    MessageBox.Show("对不起，您无权限新建！");
            //    return;
            //}
            optMain = OperateType.optNew;
            try
            {
                DataRowView drv = (DataRowView)bdsMain.AddNew();
                //初始化字段数据(默认值)
                drv["cCSId"] = _CSId.Trim();
                drv["cCmptId"] = UserInformation.UnitId;
                drv["cUser"] = UserInformation.UserName;
                DataRowViewToUI(drv, pnlEdit);
                //控制录入问题
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
            //初始化字段数据(默认值)
            drv.BeginEdit();
            //控制录入问题
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
            //控制录入问题
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
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "delete " + strTbNameMain + " where " + strKeyFld + "='" + drv[strKeyFld].ToString() + "'";             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加            
            cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名 默认为 "data"
            SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            string sErr = "";
            DataSet dsX = null;
            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            bool bX = dsX != null;
            //DataMainToObjInfo(drv);
            //bX = BI.BasicPubInfo.BasicPubInfoBI.DoCompanyInfo(AppInformation.dbtApp, AppInformation.AppConn, UserInformation, drv, true);
            if (bX)
            {
                optMain = OperateType.optDelete;
                OpenMainDataSet();
                //控制录入问题
                CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                optMain = OperateType.optNone;
                DisplayState(stbState, optMain);
                CtrlControlReadOnly(pnlEdit, false);
            }
            else
            {
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public void DoMSave()
        {
            string sSql = "";
            this.cmb_cItemName.Focus();//使其焦点移开,修改数据能及时更新
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (cmb_cItemName.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，资质名称不能为空！");
                    cmb_cItemName.Focus();
                    return;
                }
                if (txt_cItemValue.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，资质编码不能为空！");
                    txt_cItemValue.Focus();
                    return;
                }
                if (this.dtp_dBeginDate.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，生效日期不能为空！");
                    dtp_dBeginDate.Focus();
                    return;
                }
                if (this.dtp_dEndDate.Text.Trim() == "")
                {
                    MessageBox.Show("对不起，截止日期不能为空！");
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
                if (optMain == OperateType.optNew) //新增，需要产生新的号码
                {
                    //drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, UserInformation.UnitId.Trim().Length + 4, UserInformation.UnitId.Trim());
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, true);//产生 insert 语句

                }
                else
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, false);//产生UPDATE 语句
                bool bX = false;
                if (drvX.IsEdit) drvX.EndEdit();
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = sSql;             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加
                cmdInfo.FldsData = DBCommInfo.DBSQLCommandInfo.GetFieldsForDate(drvX);
                cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名
                SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                string sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
                bX = dsX.Tables[0].Rows[0][0].ToString() == "0";
                if (bX)
                {
                    optMain = OperateType.optSave;
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //重新刷新数据
                    OpenMainDataSet();
                    //控制录入问题
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                    optMain = OperateType.optNone;
                    DisplayState(stbState, optMain);
                    CtrlControlReadOnly(pnlEdit, false);
                    LoadItemToComb();
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
        }
        #endregion

        public frmCuSuAvailability()
        {
            InitializeComponent();
        }

        private void frmCuSuAvailability_Load(object sender, EventArgs e)
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

