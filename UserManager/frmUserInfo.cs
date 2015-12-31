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
using System.Collections;

namespace UserMS
{
    public partial class frmUserInfo : UI.FrmSTable
    {
        public frmUserInfo()
        {
            InitializeComponent();
        }
        #region 私有变量

        int iPwdLength = 0;
        string strTbNameMain = "TPB_User";
        string strKeyFld = "cUserId";
        bool bDSIsOpenForMain = false;
        bool bCmptIsOpened = false;
        bool bDeptisOpened = false;
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
        private void LoadBaseItemFromArr()
        {
            //单据状态
            ArrayList ArrUseState = new ArrayList(); //是否启用
            ArrUseState.Add(new DictionaryEntry(0, "未启用"));
            ArrUseState.Add(new DictionaryEntry(1, "已启用"));
            ////

            cmb_bUsed.DisplayMember = "Value";
            cmb_bUsed.ValueMember = "Key";
            cmb_bUsed.DataSource = ArrUseState;

            ////明细执行状态
            ArrayList ArrTag = new ArrayList(); //用户类型0:普通用户 1:管理员用户 2:超级管理员用户
            ArrTag.Add(new DictionaryEntry(0, "普通用户"));
            ArrTag.Add(new DictionaryEntry(1, "管理员"));
            ArrTag.Add(new DictionaryEntry(2, "超级管理员"));
            ////

            cmb_nTag.DisplayMember = "Value";
            cmb_nTag.ValueMember = "Key";
            cmb_nTag.DataSource = ArrTag;
        }

        private void LoadDeptList(string strCmptId)
        {
            string sErr = "";
            string strSql = "select * from TPB_Dept";
            if (strCmptId.Trim() != "")
            {
                strSql += " where cCmptId='" + strCmptId + "'";
            }
            bDeptisOpened = false;
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out sErr); //UserManager.GetDataSetbySql(sql);

            cmb_Dept.DataSource = ds1.Tables["data"];
            cmb_Dept.DisplayMember = "cName";
            cmb_Dept.ValueMember = "cDeptId";
            bDeptisOpened = true;
            if (cmb_Dept.Items.Count > 0)
            {
                switch (UserInformation.UType)
                {
                    case UserType.utNormal:
                        cmb_Dept.SelectedValue = UserInformation.DeptId.Trim();
                        cmb_Dept.Enabled = false;
                        break;
                    case UserType.utAdmin:
                        cmb_Dept.SelectedValue = UserInformation.DeptId.Trim();
                        cmb_Dept.Enabled = false;
                        break;
                    case UserType.utSupervisor:
                        cmb_Dept.SelectedIndex = 0;
                        cmb_Dept.Enabled = true;
                        break;
                    default:
                        cmb_Dept.SelectedIndex = 0;
                        cmb_Dept.Enabled = false ;
                        break;
                }
            }
        }

        private void loadComptList()
        {
            string sErr = "";
            string strSql = "select * from tpb_Compt ";
            if (UserInformation.UType != UserType.utSupervisor)
            {
                strSql = strSql + " where cComptId='"+ UserInformation.UnitId.Trim() +"'";
            }
            bCmptIsOpened = false;
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(strSql, out sErr); //UserManager.GetDataSetbySql(sql);

            cmb_Compt.DataSource = ds1.Tables["data"];
            cmb_Compt.DisplayMember = "cCmptName";
            cmb_Compt.ValueMember = "cComptId";
            bCmptIsOpened = true;
            if (cmb_Compt.Items.Count > 0)
            {
                switch (UserInformation.UType)
                {
                    case UserType.utNormal:
                        cmb_Compt.SelectedValue = UserInformation.UnitId.Trim();
                        cmb_Compt.Enabled = false;
                        break;
                    case UserType.utAdmin:
                        cmb_Compt.SelectedIndex = 0;
                        cmb_Compt.Enabled = true;
                        break;
                    //case UserType.utSupervisor:
                    //    cmb_Compt.SelectedIndex = 0;
                    //    cmb_Compt.Enabled = true;
                    //    break;
                    default:
                        cmb_Compt.SelectedIndex = 0;
                        cmb_Compt.Enabled = true;
                        break;
                }
            }
        }

        private int GetPwdLen()
        {
            int iLen = 0;
            string sErr = "";
            string strSql = "select * from tps_syspar where cParId='nStrongPwdLen'";
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket,false,strSql,"data",0,0,"",out sErr); //UserManager.GetDataSetbySql(sql);
            if ((sErr.Trim() == "" || sErr.Trim() == "0") && ds1 != null)
            {
                if (ds1.Tables.Count > 0 && ds1.Tables["data"] != null)
                {
                    DataTable tbX = ds1.Tables["data"];
                    if (tbX.Rows.Count > 0)
                    {
                        iLen = int.Parse(tbX.Rows[0]["cParValue"].ToString());
                    }
                }
                ds1.Clear();
            }
            else
            {
                MessageBox.Show(sErr);
            }
            return iLen;
        }

        private void LoadBaseItem()
        {
            loadComptList();
            LoadDeptList(cmb_Compt.SelectedValue.ToString());
            LoadBaseItemFromArr();
            iPwdLength = GetPwdLen();
        }

        private string GetDataCondition()
        {
            StringBuilder sCon = new StringBuilder("");
            if (cmb_Compt.Text.Trim().Length > 0)
            {
                if (cmb_Compt.Items.Count > 0)
                {
                    if (cmb_Compt.SelectedValue != null)
                    {
                        sCon.Append(" where cCmptId='" + cmb_Compt.SelectedValue.ToString().Trim() + "'");
                    }
                }
            }
            if (cmb_Dept.Text.Trim().Length > 0)
            {
                if (cmb_Dept.Items.Count > 0)
                {
                    if (cmb_Dept.SelectedValue != null)                   
                    {
                        if (sCon.Length > 0)
                        {
                            sCon.Append(" and cDeptId='" + cmb_Dept.SelectedValue.ToString().Trim() + "'");
                        }
                        else
                        {
                            sCon.Append(" where cDeptId='" + cmb_Dept.SelectedValue.ToString().Trim() + "'");
                        }
                    }
                }
            }
            switch (UserInformation.UType)
            {
                case UserType.utSupervisor:
                    break;
                case UserType.utAdmin:
                    //sCon.Append(" and cUserId in (select distinct cUserId from TPB_UserMgrArea where cMAreaId in (select distinct cMAreaId from TPB_UserMgrArea where cUserId='" + UserInformation.UserId + "'))");
                    sCon.Append(" and cDeptId='" + UserInformation.DeptId.Trim() + "'");
                    break;
                case UserType.utNormal:
                    sCon.Append(" and cUserId='" + UserInformation.UserId + "'");
                    break;
            }
            string sX =txtFindName.Text.Trim();
            if ( sX.Length > 0)
            {
                if (sCon.Length > 0)
                {
                    sCon.Append(" and ((cName like '%" + sX + "%') or (isnull(cPYJM,' ') like  '%" + sX + "%') or (isnull(cWBJM,' ') like  '%" + sX + "%'))");
                }
                else
                {
                    sCon.Append(" where ((cName like '%" + sX + "%') or (isnull(cPYJM,' ') like  '%" + sX + "%') or (isnull(cWBJM,' ') like  '%" + sX + "%'))");
                }
            }            
            return sCon.ToString();

        }
        #endregion

        #region 公共属性

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
            ////先清掉绑定
            //DataSetUnBind(pnlEdit);
            ////绑定数据集
            //DataSetBind(pnlEdit, this.bdsMain);
            grdList.DataSource = null;
            grdList.DataSource = bdsMain;
        }
        public  override void DisplayState(ToolStripLabel stbSt, OperateType optX)
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

        public bool OpenMainDataSet(string sCon)
        {
            bool bIsOK = false;
            string strX = "";
            string sSql = "";
            string sErr = "";
            bDSIsOpenForMain = false;
            grdList.AutoGenerateColumns = false;
            grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBDataSet.Clear();
            sSql = "select * from " + strTbNameMain + sCon;             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加      
            DBDataSet = SunEast.App.PubDBCommFuns.GetDataBySql(sSql,strTbNameMain,0,0,out sErr);
            bIsOK = DBDataSet != null;
            if(bIsOK)
            {
                bIsOK = DBDataSet.Tables[0].Rows[0][0].ToString().Trim() == "0";
            }
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
                    bDSIsOpenForMain = true;
                    this.bdsMain.DataSource = DBDataSet.Tables[strTbNameMain];
                    BindDataSetToCtrls();
                    ClearUIValues(pnlEdit);
                    if (bdsMain.Count > 0)
                    {
                        DataRowViewToUI((DataRowView)bdsMain.Current, pnlEdit);
                    }
                    bIsOK = true;
                    optMain = OperateType.optNone;
                    btn_SetPwd.Visible = false;
                    btn_SetPwd.Enabled = false;
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
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("对不起，您无权限新建用户信息！");
                return;
            }
            if (cmb_Compt.SelectedValue == null)
            {
                MessageBox.Show("对不起，单位不能为空！");
                return;
            }
            if (cmb_Dept.SelectedValue == null)
            {
                MessageBox.Show("对不起，部门不能为空！");
                return;
            }
            optMain = OperateType.optNew;
            int iPos = bdsMain.Position;
            DataRowView drv = (DataRowView)bdsMain.AddNew();
            
            iPos = bdsMain.Position;
            //bdsMain.MoveLast();
            //drv = (DataRowView)bdsMain.Current;
            //初始化字段数据(默认值)
            bool bOK = drv.IsEdit;
            bOK = drv.IsNew;
            drv["cUserId"] = "-1";
            drv["nTag"] = 0;
            drv["bUsed"] = 0;
            drv["cCmptId"] = cmb_Compt.SelectedValue.ToString().Trim();
            drv["cDeptId"] = cmb_Dept.SelectedValue.ToString().Trim();
            drv["dCreateDate"] = DateTime.Now;
            drv["cCreator"] = UserInformation.UserName;
            drv["nSort"] = 0;
            DataRowViewToUI(drv, pnlEdit);
            //
            txt_cUserId.Text = drv["cUserId"].ToString();
            txt_cCmptId.Text = drv["cCmptId"].ToString();
            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            this.txt_cName.Focus();
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, true);
            txt_cUserId.ReadOnly = true;
            btn_SetPwd.Visible = true;
            btn_SetPwd.Enabled = true;
        }
        public void DoMEdit()
        {
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
            txt_cUserId.ReadOnly = true;
            btn_SetPwd.Visible = true;
            btn_SetPwd.Enabled = true;
        }
        public void DoMUndo()
        {
            optMain = OperateType.optUndo;
            DataRowView drv = null; 
            drv = (DataRowView)bdsMain.Current;
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
            drv = (DataRowView)bdsMain.Current;
            this.DataRowViewToUI(drv, pnlEdit);
            //控制录入问题
            CtrlOptButtons(this.tlbMain, pnlEdit, optMain, DBDataSet.Tables[strTbNameMain]);
            optMain = OperateType.optNone;
            DisplayState(stbState, optMain);
            CtrlControlReadOnly(pnlEdit, false);
            btn_SetPwd.Visible = false;
            btn_SetPwd.Enabled = false;          
        }
        public void DoMDelete()
        {
            if (UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("对不起，您无权限删除用户信息！");
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
            bool bX = false;
            string sErr = "";
            string sSql = "delete " + strTbNameMain + " where " + strKeyFld + "='" + drv[strKeyFld].ToString() + "'";   
            DataSet dsX = null;
            //执行语句
            dsX = SunEast.App.PubDBCommFuns.GetDataBySql(sSql, out sErr);
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
                btn_SetPwd.Visible = false;
                btn_SetPwd.Enabled = false;
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
            txt_cUserId.Focus();//使其焦点移开,修改数据能及时更新
            DataRowView drvX = (DataRowView)bdsMain.Current;
            
            if (drvX.IsEdit || drvX.IsNew)
            {
                if (this.txt_cCmptId.Text.Trim() == "")
                {
                    MessageBox.Show("单位编码不能为空！");
                    txt_cCmptId.Focus();
                    return;
                }
                if (this.txt_cName.Text.Trim() == "")
                {
                    MessageBox.Show("用户名称不能为空！");
                    txt_cName.Focus();
                    return;
                }
                if (this.cmb_bUsed.Text.Trim() == "")
                {
                    MessageBox.Show("是否启用不能为空！");
                    cmb_bUsed.Focus();
                    return;
                }
                if (this.cmb_nTag.Text.Trim() == "")
                {
                    MessageBox.Show("用户类型不能为空！");
                    cmb_nTag.Focus();
                    return;
                }
                if (this.txt_nSort.Text.Trim() == "")
                {
                    MessageBox.Show("排序号不能为空！");
                    txt_nSort.Focus();
                    return;
                }
                int iX = (int)UserInformation.UType;
                if (cmb_nTag.SelectedIndex > iX)
                {
                    MessageBox.Show("对不起，当前用户的权限超出里您的权限范围，请重选用户类型！");
                    cmb_nTag.Focus();
                    return;
                }
                UIToDataRowView(drvX, pnlEdit);
                //保存拼音简码和五笔简码
                string sX = "";
                if (drvX["cName"] != null)
                {
                    sX = drvX["cName"].ToString();
                    sX = Zqm.Text.TextPYWB.GetWBPY(sX, PYWBType.pwtPYFirst);
                }
                if (drvX["cPYJM"] != null)
                {
                    drvX["cPYJM"] = sX;
                }
                sX = "";
                if (drvX["cName"] != null)
                {
                    sX = drvX["cName"].ToString();
                    sX = Zqm.Text.TextPYWB.GetWBPY(sX, PYWBType.pwtWBFirst);
                }
                if (drvX["cWBJM"] != null)
                {
                    drvX["cWBJM"] = sX;
                }
                if (drvX[strKeyFld].ToString() == "" || drvX[strKeyFld].ToString() == "-1") //新增，需要产生新的号码
                {
                    drvX[strKeyFld] = SunEast.App.PubDBCommFuns.GetNewId(strTbNameMain, strKeyFld, 8, drvX["cDeptId"].ToString().Trim());
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, true);//产生 insert 语句
                }
                else
                    sSql = DBCommInfo.DBSQLCommandInfo.GetSQLByDataRow(drvX, strTbNameMain, strKeyFld, false);//产生UPDATE 语句
                bool bX = false;
                
                //检测完整性
                
                string sErr = "";
                if (!CheckInputDataValues(drvX, pnlEdit, "cUserId,cName,bUsed,nTag,nSort", out sErr))
                {
                    MessageBox.Show(sErr);
                    return;
                }
                if (drvX.IsEdit) drvX.EndEdit();
                DataSet dsX = null;
                //执行语句
                dsX =SunEast.App.PubDBCommFuns.GetDataBySql(sSql, DBSQLCommandInfo.GetFieldsForDate(drvX), out sErr);
                //dsX = SunEast.App.PubDBCommFuns.GetDataBySql(sSql, out sErr);
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

        private void btnQry_Click(object sender, EventArgs e)
        {
            sbConndition.Remove(0, sbConndition.Length);
            sbConndition.Append(GetDataCondition());
            OpenMainDataSet(sbConndition.ToString());
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
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

            string sCon = "";
            LoadBaseItem();
            sCon = GetDataCondition();
            OpenMainDataSet(sCon);
        }

        

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (!bDSIsOpenForMain) return;
            DataRowView drv = (DataRowView)bdsMain.Current;
            ClearUIValues(pnlEdit);
            if (drv != null)
            {
                if (!drv.IsNew)
                    DataRowViewToUI(drv, pnlEdit);
            }
        }

        //private void cmb_Compt_TextChanged(object sender, EventArgs e)
        //{
        //    string sX = "";
        //    if (!bCmptIsOpened) return;
        //    if (cmb_Compt.Items.Count > 0)
        //    {
        //        if (cmb_Compt.SelectedValue != null)
        //        {
        //            sX = cmb_Compt.SelectedValue.ToString().Trim();
        //        }
        //    }
        //    LoadDeptList(sX);
        //}

        private void cmb_Dept_TextChanged(object sender, EventArgs e)
        {
            string sX = "";
            if (!bDeptisOpened) return;
            btnQry_Click(sender, e);
        }

        private void cmb_nTag_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_SetPwd_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)bdsMain.Current;
            if (drv == null) return;
            if (drv.IsNew == false && drv.IsEdit == false)
            {
                MessageBox.Show("对不起，没有处于编辑状态，不能设置密码！");
                return;
            }
            frmSetUserPwd frmX = new frmSetUserPwd();            
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.PwdLen = iPwdLength;
            frmX.ShowDialog();
            if (frmX.IsOK)
            {
                drv["cPwd"] = frmX.NewPwdValue;
                MessageBox.Show("密码设置成功，需要保存后方生效！");
            }
            frmX.Dispose();

        }

        private void cmb_Compt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sX = "";
            if (!bCmptIsOpened) return;
            if (cmb_Compt.Items.Count > 0)
            {
                if (cmb_Compt.SelectedValue != null)
                {
                    sX = cmb_Compt.SelectedValue.ToString().Trim();
                }
            }
            LoadDeptList(sX);
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

        private void btn_SaveGridColum_Click(object sender, EventArgs e)
        {
            string sMyClassName = "";
            sMyClassName =  this.GetType().Namespace ;
            
            MessageBox.Show(sMyClassName);
           
        }
    }
}

