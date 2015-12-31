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
    public partial class frmDept : UI.FrmSTable
    {
        public frmDept()
        {
            InitializeComponent();
        }

        #region 私有变量

        string strTbNameMain = "TPB_Dept";
        string strKeyFld = "cDeptId";
        string strFix = "";
        bool bMainlstIsOpened = false;
        //主表操作
        OperateType optMain = OperateType.optNone;
        //记录当前数据列表的 条件
        StringBuilder sbConndition = new StringBuilder("");
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

        private string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader)
        {
            //sp_GetNewId(@TbName varchar(50),@FldKey varchar(50),@Len int=0,@ReplaceChar varchar(2)='0',@Header varchar(10)='',
            //@FldCon varchar(50)='',@ValueCon varchar(50)='')
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_GetNewId";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@TbName";           //参数名称 和实际定义的一致
            par.ParameterValue = sTbName;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@FldKey";           //参数名称 和实际定义的一致
            par.ParameterValue = sKeyFld;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@Len";           //参数名称 和实际定义的一致
            par.ParameterValue = nLength.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@ReplaceChar";           //参数名称 和实际定义的一致
            par.ParameterValue = "0";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@Header";           //参数名称 和实际定义的一致
            par.ParameterValue = sHeader;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@FldCon";           //参数名称 和实际定义的一致
            par.ParameterValue = "";            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            ////---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "@ValueCon";           //参数名称 和实际定义的一致
            par.ParameterValue = "";            //参数值 可以为""空
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
            if (dsX != null)
            {
                tbX = dsX.Tables["data"];
                if (tbX != null)
                    sId = tbX.Rows[0]["cNewId"].ToString();
            }
            dsX.Clear();
            return sId;
        }

        private void LoadBaseData()
        {
            string sErr = "";
            string sSql = "select * from TPB_Compt ";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                sSql = sSql + " where cComptId='" + UserInformation.UnitId + "'";
            }
            DataSet dsX = null;
            dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "TPB_Compt", 0, 0, out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
                return;
            }
            cmb_Compt.DataSource = dsX.Tables["TPB_Compt"];
            cmb_Compt.DisplayMember = "cCmptName";
            cmb_Compt.ValueMember = "cComptId";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                cmb_Compt.SelectedValue = UserInformation.UnitId;
                if (cmb_Compt.SelectedIndex > -1)
                    cmb_Compt.Enabled = false;
            }
            else
            {
                cmb_Compt.Enabled = true;
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
        public bool OpenMainDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            bMainlstIsOpened = false;
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "select * from " + strTbNameMain + sCon;             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加
            if (UserInformation.UType == UserType.utSupervisor)
            {
                cmdInfo.SqlText = "select * from " + strTbNameMain  ; //仅显示当前用户的单位
            }
            cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名
            SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            string sErr = "";
            //DataSet dsX = null;
            //DataTable tbX= null ;
            DBDataSet = sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
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
                    DataSetUnBind(pnlEdit);
                    this.bdsMain.DataSource = DBDataSet.Tables[strTbNameMain];
                    if (bdsMain != null && bdsMain.Count > 0)
                        BindDataSetToCtrls();
                    bIsOK = true;
                    optMain = OperateType.optNone;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsOK = false;
                }
            }
            bMainlstIsOpened = true;
            return (bIsOK);
        }
        public void DoMNew()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("对不起，您无权限新建部门信息！");
                return;
            }
            if (cmb_Compt.SelectedValue == null)
            {
                MessageBox.Show("对不起，请先选择单位！");
                cmb_Compt.Focus();
                return;
            }
            optMain = OperateType.optNew;
            DataRowView drv = (DataRowView)bdsMain.AddNew();
            //初始化字段数据(默认值)
            drv["dCreateDate"] = DateTime.Now;
            drv["cCreator"] = UserInformation.UserName;
            drv["cCmptId"] = cmb_Compt.SelectedValue.ToString();
            txt_cCmptId.Text = drv["cCmptId"].ToString();
            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cDeptId.ReadOnly = true;
        }
        public void DoMEdit()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("对不起，您无权限修改部门信息！");
                return;
            }
            optMain = OperateType.optEdit;
            DataRowView drv = (DataRowView)bdsMain.Current;
            //初始化字段数据(默认值)
            drv.BeginEdit();
            drv["dEditDate"] = DateTime.Now;
            drv["cEditor"] = UserInformation.UserName;
            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cDeptId.ReadOnly = true;
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
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("对不起，您无权限删除部门信息！");
                return;
            }
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
                OpenMainDataSet(sbConndition.ToString());
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
            txt_cDeptId.Focus();//使其焦点移开,修改数据能及时更新
            DataRowView drvX = (DataRowView)bdsMain.Current;
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (txt_cCmptId.Text.Trim() == "")
                {
                    MessageBox.Show("单位编码不能为空！");
                    txt_cCmptId.Focus();
                    return;
                }
                //if (this.txt_cDeptId.Text.Trim() == "")
                //{
                //    MessageBox.Show("部门编码不能为空！");
                //    txt_cDeptId.Focus();
                //    return;
                //}
                if (this.txt_cName.Text.Trim() == "")
                {
                    MessageBox.Show("部门名称不能为空！");
                    txt_cName.Focus();
                    return;
                }
                UIToDataRowView(drvX, pnlEdit);
                if (drvX[strKeyFld].ToString() == "" || drvX[strKeyFld].ToString() == "-1") //新增，需要产生新的号码
                {

                    //drvX[strKeyFld]  = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, 3, "1");
                    string sXX = drvX["cCmptId"].ToString().Trim();
                    drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, 5, sXX);
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, true);//产生 insert 语句
                }
                else
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, false);//产生UPDATE 语句
                bool bX = false;
                if (drvX.IsEdit) drvX.EndEdit();
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = sSql;             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加
                cmdInfo.FldsData = DBSQLCommandInfo.GetFieldsForDate(drvX);
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
                    OpenMainDataSet(sbConndition.ToString());
                    //控制录入问题
                    CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
                    optMain = OperateType.optNone;
                    DisplayState(stbState, optMain);
                    CtrlControlReadOnly(pnlEdit, false);
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

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            sbConndition.Remove(0, sbConndition.Length);
            sbConndition.Append(strFix);
            if (txtFindName.Text.Trim() != "")
            {
                if (sbConndition.Length > 0)
                {
                    sbConndition.Append(" and cName like '%" + txtFindName.Text.Trim() + "%'");
                }
                else
                {
                    sbConndition.Append(" where cName like '%" + txtFindName.Text.Trim() + "%'");
                }
            }
            OpenMainDataSet(sbConndition.ToString());
        }

        private void frmDept_Load(object sender, EventArgs e)
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

            if (UserInformation.UType != UserType.utSupervisor)
            {
                strFix = " where cCmptId='" + UserInformation.UnitId + "'";
            }
            LoadBaseData();   
            OpenMainDataSet(strFix);
        }

        private void cmb_Compt_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmb_Compt_TextChanged(object sender, EventArgs e)
        {
            if (!bMainlstIsOpened) return;
            if (cmb_Compt.Items.Count == 0) return;
            if (cmb_Compt.SelectedValue == null) return;
            if (cmb_Compt.Text.Trim().Length == 0)
            {
                OpenMainDataSet(strFix);
            }
            else
            {
                if (cmb_Compt.SelectedValue != null)
                {
                    string sX = cmb_Compt.SelectedValue.ToString();
                    OpenMainDataSet(" where cCmptId='" + sX.Trim() + "'");
                }
                else
                {
                    OpenMainDataSet(strFix);
                }
            }
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

