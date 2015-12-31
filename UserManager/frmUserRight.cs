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
    public partial class frmUserRight : UI.FrmSTable
    {
        #region 私有变量
        bool bIsGrdOpend = false;
        bool bIsAllowChangeCheckState = true;
        bool bIsAllowSetRts = false; //是否允许设置操作权限
        bool bIsAllowSetUW = false;  //是否允许设置用户仓库权限
        bool bIsAllowSetUWF = false; //是否允许设置用户工作流程权限
        bool bIsAllowSetUMA = false; //是否允许设置用户管理货区权限
        DataSet dsSysRts = new DataSet();
        DataSet dsUserRts = new DataSet();
        DataTable tbSysUW = new DataTable();
        DataTable tbSysUWF = new DataTable ();
        DataTable tbSysMgrArea = new DataTable();
        #endregion
        #region 私有方法

        private void OpenUserData()
        {
            bIsGrdOpend = false;
            string sId = "";
            string err1 = "";
            string sql1 = "select T1.*,T2.cName as cDeptName from TPB_User T1 left join TPB_DEPT T2 on T1.cdeptid=T2.cdeptid where T1.bUsed=1 ";
            switch (UserInformation.UType)
            {
                case UserType.utSupervisor:
                    break;
                case UserType.utAdmin :
                    //sql1 += " and cUserId in (select distinct cUserId from TPB_UserMgrArea where cMAreaId in (select distinct cMAreaId from TPB_UserMgrArea where cUserId='"+ UserInformation.UserId +"'))";
                    sql1 += " and T1.cDeptId='" + UserInformation.DeptId.Trim() + "'";
                    break;
                case UserType.utNormal :
                    sql1 += " and cUserId='"+ UserInformation.UserId +"'";
                    break;
            }
           
            sql1 += " order by T1.cDeptId,nSort desc,cUserId";
            DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(sql1, "TPB_User", 0, 0, out err1);
            if (err1 == "")
            {
                DataTable tbX = ds1.Tables["TPB_User"];
                grdUser.AutoGenerateColumns = false;
                bdsUser.DataSource = tbX;
                grdUser.DataSource = bdsUser;
                bIsGrdOpend = true;
                sId = "";
                if (bdsUser.Count > 0)
                {
                    DataRowView drvX = (DataRowView)bdsUser.Current;
                    if (drvX != null)
                    {
                        sId = drvX["cUserId"].ToString();
                    }
                }

            }
            else
            {
                bIsGrdOpend = true;
                MessageBox.Show(err1);
            }
        }
        private void OpenUserRights(string sUserId)
        {
            bool bX = false;
            object objX = null;

            bIsAllowSetRts = false;
            btn_Cancel.Enabled = bIsAllowSetRts;
            btn_SaveRTS.Enabled = bIsAllowSetRts;
            chk_SelAll.Enabled = bIsAllowSetRts;
            
            StringBuilder sSql = new StringBuilder("");

            //超级管理员
            if (sUserId.Trim() == "90101001")
            {
                sSql.Append("select '90101001' cUserId,* from TPB_Rights order by cPRId,nSort,cRId");
            }
            else
            {
                sSql.Append("Select u.*,s.cName,s.cRCode,s.cPRId from TPB_URTS u inner join TPB_Rights s on u.cRId=s.cRId where cUserId ='" + sUserId + "'");
                sSql.Append(" order by s.cPRId,s.nSort,s.cRId ");

                //if (sModId.Trim() != "")
                //{
                //    sSql.Append(" and u.cRId in (select cRId from TPB_Rights where cPRId=" + sModId.Trim() + ")");
                //}
            }
            string err="";
            dsUserRts.Clear();
            dsUserRts = SunEast.App.PubDBCommFuns.GetDataBySql(sSql.ToString(), out err);
            DataTable tbX = dsUserRts.Tables["data"];
            DataTable tbSys = dsSysRts.Tables["TPB_Rights"];
            bIsAllowChangeCheckState = false;
            //trvRight.Nodes.Clear();
            if (tbX.Rows.Count>0)
            {
                prgRTS.Maximum = tbSys.Rows.Count;
                prgRTS.Minimum = 0;
                prgRTS.Value = 0;
                prgRTS.Visible = true;
                bIsAllowSetRts = true;
                foreach (DataRow drX in tbSys.Rows)
                {

                    TreeNode[] nds = trvRight.Nodes.Find(drX["cRId"].ToString(), true);
                    if (nds != null)
                    {
                        if (nds.Length == 1)
                        {
                            nds[0].Checked = tbX.Select("cRId='" + drX["cRId"].ToString() + "'").Length > 0;
                        }
                    }
                    prgRTS.Value++;
                }
                bIsAllowSetRts = false;
                prgRTS.Visible = false;
            }
            else
            {
                bIsAllowSetRts = true;
                foreach (DataRow drX in tbSys.Rows)
                {

                    TreeNode[] nds = trvRight.Nodes.Find(drX["cRId"].ToString(), true);
                    if (nds != null)
                    {
                        if (nds.Length == 1)
                        {
                            nds[0].Checked = false;
                        }
                    }
                }
                bIsAllowSetRts = false;
            }
            bIsAllowChangeCheckState = true;
        }
        private TreeNode FindNode(TreeNode ndCurrent, string sKey)
        {
            int nIndex = 0;
            TreeNode ndResult = null;
            if (ndCurrent != null)
            {
                if (ndCurrent.Nodes.Count > 0)
                {
                    nIndex = ndCurrent.Nodes.IndexOfKey(sKey);
                    if (nIndex > -1)
                    {
                        ndResult = ndCurrent.Nodes[nIndex];
                    }
                    else
                    {
                        foreach (TreeNode ndT in ndCurrent.Nodes)
                        {
                            ndResult = FindNode(ndT, sKey);
                            if (ndResult != null)
                                break;
                        }
                    }
                }

            }
            return (ndResult);
        }
        private void LoadSysRights(string sAdminId)
        {
            bool bX = false;
            object objX = null;
            TreeNode ndParent = null;
            TreeNode trNd = null;

            //StringBuilder sSql = new StringBuilder("select cRId,cName,cRCode,cPRId  from TPB_Rights ");
            //if (sAdminId == "2011001001")
            //    sSql.Append(" where cRId in (select cRId from TPB_Rights) ");
            //else
            //    sSql.Append(" where cRId in (select cRId from TPB_URTS where cUserId='20110022')");
            //sSql.Append("  order by cRId");
            //DataTable tbX = dsSysRts.Tables["TPB_Rights"];
            //if (tbX != null) tbX.Clear();
            //string sX = Zqm.DBBase.DBOptrForComm.OptOpenDataSet(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), "TPB_Rights", dsSysRts);
            string sX = "";
            string sSql = "select * from TPB_Rights";
            if (sAdminId != "90101001")
                sSql +=" where cRId in (select cRId from TPB_URTS where cUserId='"+ sAdminId.Trim()+"')";
            sSql +="  order by cRId";
            dsSysRts = SunEast.App.PubDBCommFuns.GetDataBySql(sSql, "TPB_Rights", 0, 0, out sX);
            DataTable tbX = dsSysRts.Tables["TPB_Rights"];
            if (sX == "")
            {
                trvRight.LabelEdit = false;
                trvRight.CheckBoxes = true;
                trvRight.BeginUpdate();
                try
                {
                    trvRight.Nodes.Clear();
                    tbX = dsSysRts.Tables["TPB_Rights"];
                    if (tbX != null)
                    {
                        prgRTS.Maximum = tbX.Rows.Count;
                        prgRTS.Minimum = 0;
                        prgRTS.Value = 0;
                        prgRTS.Visible = true;
                        foreach (DataRow drX in tbX.Rows)
                        {
                            ndParent = null;
                            int nPIndex = trvRight.Nodes.IndexOfKey(drX["cPRId"].ToString());
                            if (nPIndex > -1)
                                ndParent = trvRight.Nodes[nPIndex];
                            else
                            {
                                foreach (TreeNode ndX in trvRight.Nodes)
                                {
                                    ndParent = FindNode(ndX, drX["cPRId"].ToString());
                                    if (ndParent != null)
                                        break;
                                }
                            }
                            if (ndParent == null)
                            {
                                trNd = trvRight.Nodes.Add(drX["cRId"].ToString(), drX["cName"].ToString());
                            }
                            else
                            {
                                trNd = ndParent.Nodes.Add(drX["cRId"].ToString(), drX["cName"].ToString());
                            }
                            trNd.Tag = drX["cRId"].ToString();
                            prgRTS.Value++;
                        }
                        prgRTS.Visible = false;
                    }
                }
                finally
                {
                    trvRight.ExpandAll();
                    trvRight.EndUpdate();
                }
            }
        }
        private bool SaveUserRigts()
        {
            bool bIsOK = false;
            DataRowView drvX = (DataRowView)bdsUser.Current;
            if (drvX == null)
            {
                MessageBox.Show("对不起，无用户，保存权限失败！");
                return (false);
            }
            if (trvRight.Nodes.Count == 0)
            {
                MessageBox.Show("对不起，无系统功能数据，保存权限失败！");
                return (false);
            }
            StringBuilder sSql = new StringBuilder("");
            string sUserId = drvX["cUserId"].ToString();
            prgRTS.Maximum = dsSysRts.Tables["TPB_Rights"].Rows.Count;
            prgRTS.Minimum = 0;
            prgRTS.Value = 0;
            prgRTS.Visible = true;
            string sX = "";
            long nX = 0;
            bIsOK = true;
            foreach (TreeNode ndX in trvRight.Nodes)
            {
                SaveNodeRights(ndX, sUserId);
            }
            prgRTS.Visible = false;
            //
            if (bIsOK)
            {
                bIsAllowSetRts = false;
                btn_Cancel.Enabled = bIsAllowSetRts;
                btn_SaveRTS.Enabled = bIsAllowSetRts;
                chk_SelAll.Enabled = bIsAllowSetRts;
            }
            btn_Right.Enabled = true;
            return (bIsOK);
        }

        private bool SaveUserUWRigts()
        {
            bool bIsOK = false;
            DataRowView drvX = (DataRowView)bdsUser.Current;
            if (drvX == null)
            {
                MessageBox.Show("对不起，无用户，保存权限失败！");
                return (false);
            }
            if (trvUWRights.Nodes.Count == 0)
            {
                MessageBox.Show("对不起，无系统功能数据，保存权限失败！");
                return (false);
            }
            StringBuilder sSql = new StringBuilder("");
            string sUserId = drvX["cUserId"].ToString();
            prgUW.Maximum = tbSysUW.Rows.Count;
            prgUW.Minimum = 0;
            prgUW.Value = 0;
            prgUW.Visible = true;
            string sX = "";
            long nX = 0;
            bIsOK = true;
            foreach (TreeNode ndX in trvUWRights.Nodes)
            {
                SaveUWRights(ndX, sUserId);
            }
            prgUW.Visible = false;
            //
            if (bIsOK)
            {
                bIsAllowSetUW  = false;
                this.btn_UWCancel.Enabled = bIsAllowSetUW;
                this.btnUWSave.Enabled = bIsAllowSetUW;
                this.chkUWCheckAll.Enabled = bIsAllowSetUW;
            }
            return (bIsOK);
        }

        private bool SaveUserUMARigts()
        {
            bool bIsOK = false;
            DataRowView drvX = (DataRowView)bdsUser.Current;
            if (drvX == null)
            {
                MessageBox.Show("对不起，无用户，保存权限失败！");
                return (false);
            }
            if (this.trv_MgrArea.Nodes.Count == 0)
            {
                MessageBox.Show("对不起，无系统功能数据，保存权限失败！");
                return (false);
            }
            StringBuilder sSql = new StringBuilder("");
            string sUserId = drvX["cUserId"].ToString();
            prg_UMA.Maximum = this.tbSysMgrArea.Rows.Count;
            prg_UMA.Minimum = 0;
            prg_UMA.Value = 0;
            prg_UMA.Visible = true;
            string sX = "";
            long nX = 0;
            bIsOK = true;
            foreach (TreeNode ndX in trv_MgrArea.Nodes)
            {
                SaveUMgrAreaRights(ndX, sUserId);
            }
            this.prg_UMA.Visible = false;
            //
            if (bIsOK)
            {
                bIsAllowSetUMA = false;
                this.btn_UMAUndo.Enabled = bIsAllowSetUMA;
                this.btn_UMASave.Enabled = bIsAllowSetUMA;
                this.chk_UMAAll.Enabled = bIsAllowSetUMA;
            }
            this.btn_UMARights.Enabled = bIsOK;
            return (bIsOK);
        }

        private void LoadWHouseRights(string sAdminId)
        {
            bool bX = false;
            object objX = null;
            TreeNode ndParent = null;
            TreeNode trNd = null;
            string sX = "";
            string sSql = "select * from TWC_WareHouse where bUsed = 1";
            sSql += "  order by cWHId";
            DataSet dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "TWC_WareHouse", 0, 0, out sX);
            DataTable tbX = dsX.Tables["TWC_WareHouse"];
            tbSysUW = tbX.Copy();
            if (sX == "")
            {
                this.trvUWRights.LabelEdit = false;
                trvUWRights.CheckBoxes = true;
                trvUWRights.BeginUpdate();
                try
                {
                    trvUWRights.Nodes.Clear();
                    if (tbX != null)
                    {
                        prgUW.Maximum = tbX.Rows.Count;
                        prgUW.Minimum = 0;
                        prgUW.Value = 0;
                        prgUW.Visible = true;
                        foreach (DataRow drX in tbX.Rows)
                        {
                            trNd = trvUWRights.Nodes.Add(drX["cWHId"].ToString(), drX["cName"].ToString());
                            trNd.Tag = drX["cWHId"].ToString();
                            prgUW.Value++;
                        }
                        prgUW.Visible = false;
                    }
                }
                finally
                {
                    trvUWRights.ExpandAll();
                    trvUWRights.EndUpdate();
                }
            }
        }

        private void LoadMgrAreaRights(string sAdminId)
        {
            bool bX = false;
            object objX = null;
            TreeNode ndParent = null;
            TreeNode trNd = null;
            string sX = "";
            string sSql = "select * from TWC_MgrArea where bUsed = 1";
            sSql += "  order by cMAreaId ";
            DataSet dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "TWC_MgrArea", 0, 0,"", out sX);
            DataTable tbX = dsX.Tables["TWC_MgrArea"];
            tbSysMgrArea = tbX.Copy();
            if (sX == "")
            {
                this.trv_MgrArea.LabelEdit = false;
                trv_MgrArea.CheckBoxes = true;
                trv_MgrArea.BeginUpdate();
                try
                {
                    trv_MgrArea.Nodes.Clear();
                    if (tbX != null)
                    {
                        prg_UMA.Maximum = tbX.Rows.Count;
                        prg_UMA.Minimum = 0;
                        prg_UMA.Value = 0;
                        prg_UMA.Visible = true;
                        foreach (DataRow drX in tbX.Rows)
                        {
                            trNd = trv_MgrArea.Nodes.Add(drX["cMAreaId"].ToString(), drX["cMAName"].ToString());
                            trNd.Tag = drX["cMAreaId"].ToString();
                            prg_UMA.Value++;
                        }
                        prg_UMA.Visible = false;
                    }
                }
                finally
                {
                    trv_MgrArea.ExpandAll();
                    trv_MgrArea.EndUpdate();
                }
            }
        }

        private bool SaveUWRights(TreeNode ndCurr, string sUserId)
        {
            bool bIsOK = false;
            string sRtId = "";
            string sX = "";
            long nX = 0;
            StringBuilder sSql = new StringBuilder("");
            if (ndCurr != null)
            {
                sRtId = ndCurr.Tag.ToString();//
                //先保存权限
                if (ndCurr.Checked)
                {
                    string err1 = "";
                    sSql.Append("select count(*) nCount from  TPB_UserWHouse where cUserId='" + sUserId + "' and cWHId='" + sRtId + "'");
                    //sX = Zqm.DBBase.DBOptrForComm.OptExecRetInt(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), out nX);
                    DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "TPB_UserWHouse", "", out err1);
                    sX = ds1.Tables["TPB_UserWHouse"].Rows[0].ItemArray[0].ToString();
                    bIsOK = sX == "0";
                    if (sX == "0")
                    {
                        if (nX == 0)
                        {
                            string err2 = "";
                            sSql.Remove(0, sSql.Length);
                            sSql.Append("Insert into TPB_UserWHouse(cUserId,cWHId,cAreaId) values('" + sUserId + "','" + sRtId + "',' ')");
                            DataSet ds2 = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "TPB_UserWHouse", "", out err2);
                            sX = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                            bIsOK = sX == "0";
                        }
                    }
                }
                else
                {
                    string err3 = "";
                    sSql.Remove(0, sSql.Length);
                    sSql.Append("Delete TPB_UserWHouse where cUserId='" + sUserId + "' and cWHId='" + sRtId + "'");
                    DataSet ds3 = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "TPB_UserWHouse", "", out err3);
                    sX = ds3.Tables[0].Rows[0].ItemArray[0].ToString();
                    bIsOK = sX == "0";
                }
                if (prgUW.Value < prgUW.Maximum)
                    prgUW.Value++;

            }
            return (bIsOK);
        }

        private bool SaveUMgrAreaRights(TreeNode ndCurr, string sUserId)
        {
            bool bIsOK = false;
            string sRtId = "";
            string sX = "";
            long nX = 0;
            object objX = null;
            StringBuilder sSql = new StringBuilder("");
            if (ndCurr != null)
            {
                sRtId = ndCurr.Tag.ToString();//
                //先保存权限
                if (ndCurr.Checked)
                {
                    string err1 = "";
                    sSql.Append("select count(*) nCount from  TPB_UserMgrArea where cUserId='" + sUserId + "' and cMAreaId='"+ sRtId +"'");
                    //sX = Zqm.DBBase.DBOptrForComm.OptExecRetInt(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), out nX);
                    //DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "TPB_UserWHouse", "", out err1);
                    bIsOK = DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql.ToString(), "", "nCount", out objX, out err1);
                    if (bIsOK)
                    {
                        if (objX.ToString() == "0")
                        {
                            string err2 = "";
                            sSql.Remove(0, sSql.Length);
                            sSql.Append("Insert into TPB_UserMgrArea(cUserId,cMAreaId) values('" + sUserId + "','" + sRtId + "')");
                            bIsOK = DBFuns.DoExecSql(AppInformation.SvrSocket, sSql.ToString(), "", out err2);
                        }
                    }
                }
                else
                {
                    string err3 = "";
                    sSql.Remove(0, sSql.Length);
                    sSql.Append("Delete TPB_UserMgrArea where cUserId='" + sUserId + "' and cMAreaId='" + sRtId + "'");
                    bIsOK = DBFuns.DoExecSql(AppInformation.SvrSocket, sSql.ToString(), "", out err3);
                }
                if (this.prg_UMA.Value < prg_UMA.Maximum)
                    prg_UMA.Value++;

            }
            return (bIsOK);
        }


        private void OpenUserUWRights(string sUserId)
        {
            bool bX = false;
            object objX = null;

            bIsAllowSetUW = false;
            this.btn_UWCancel.Enabled = bIsAllowSetUW;
            this.btnUWSave.Enabled = bIsAllowSetUW;
            this.chkUWCheckAll.Enabled = bIsAllowSetUW;

            StringBuilder sSql = new StringBuilder("");

            sSql.Append("Select * from TPB_UserWHouse  where cUserId ='" + sUserId + "'");

            string err = "";
            DataSet dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "TPB_UserWHouse", "", out err);
            DataTable tbX = dsX.Tables["TPB_UserWHouse"];
            bIsAllowChangeCheckState = false;
            //trvRight.Nodes.Clear();
            if (tbX.Rows.Count > 0)
            {
                prgUW.Maximum = this.tbSysUW.Rows.Count;
                prgUW.Minimum = 0;
                prgUW.Value = 0;
                prgUW.Visible = true;
                bIsAllowSetUW = true;
                
                foreach (DataRow drX in this.tbSysUW.Rows)
                {

                    TreeNode[] nds = this.trvUWRights.Nodes.Find(drX["cWHId"].ToString(), true);
                    if (nds != null)
                    {
                        if (nds.Length == 1)
                        {
                            nds[0].Checked = tbX.Select("cWHId='" + drX["cWHId"].ToString() + "'").Length > 0;
                        }
                    }
                    this.prgUW.Value++;
                }
                bIsAllowSetUW = false;
                prgUW.Visible = false;
            }
            else
            {
                bIsAllowSetUW = true;
                foreach (DataRow drX in tbSysUW.Rows)
                {

                    TreeNode[] nds = trvUWRights.Nodes.Find(drX["cWHId"].ToString(), true);
                    if (nds != null)
                    {
                        if (nds.Length == 1)
                        {
                            nds[0].Checked = false;
                        }
                    }
                }
                bIsAllowSetUW = false;
            }
            btn_UWRights.Enabled = true;
            bIsAllowChangeCheckState = true;
        }

        private void OpenUserUMgrAreaRights(string sUserId)
        {
            bool bX = false;
            object objX = null;

            bIsAllowSetUMA = false;
            this.btn_UMAClose.Enabled = bIsAllowSetUMA;
            this.btn_UMASave.Enabled = bIsAllowSetUMA;
            this.chk_UMAAll.Enabled = bIsAllowSetUMA;

            StringBuilder sSql = new StringBuilder("");

            sSql.Append("Select * from TPB_UserMgrArea  where cUserId ='" + sUserId + "'");

            string err = "";
            DataSet dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql.ToString(), "TPB_UserMgrArea", "", out err);
            DataTable tbX = dsX.Tables["TPB_UserMgrArea"];
            bIsAllowChangeCheckState = false;
            //trvRight.Nodes.Clear();
            if (tbX.Rows.Count > 0)
            {
                this.prg_UMA.Maximum = this.tbSysMgrArea.Rows.Count;
                prg_UMA.Minimum = 0;
                prg_UMA.Value = 0;
                prg_UMA.Visible = true;
                bIsAllowSetUMA = true;

                foreach (DataRow drX in this.tbSysMgrArea.Rows)
                {

                    TreeNode[] nds = this.trv_MgrArea.Nodes.Find(drX["cMAreaId"].ToString(), true);
                    if (nds != null)
                    {
                        if (nds.Length == 1)
                        {
                            nds[0].Checked = tbX.Select("cMAreaId='" + drX["cMAreaId"].ToString() + "'").Length > 0;
                        }
                    }
                    this.prg_UMA.Value++;
                }
                bIsAllowSetUMA = false;
                prg_UMA.Visible = false;
            }
            else
            {
                bIsAllowSetUMA = true;
                foreach (DataRow drX in tbSysMgrArea.Rows)
                {

                    TreeNode[] nds = trv_MgrArea.Nodes.Find(drX["cMAreaId"].ToString(), true);
                    if (nds != null)
                    {
                        if (nds.Length == 1)
                        {
                            nds[0].Checked = false;
                        }
                    }
                }
                bIsAllowSetUMA = false;
            }
            this.btn_UMARights.Enabled = true;
            bIsAllowChangeCheckState = true;
        }

        private void SetTreeNodeChildrenChecked(TreeNode ndCurr, bool bChecked)
        {
            bool bX = false ;
            if (ndCurr != null)
            {
                //ndCurr.Checked = bChecked;
                if (ndCurr.Nodes.Count > 0)
                {
                    foreach (TreeNode ndC in ndCurr.Nodes)
                    {
                        ndC.Checked = ndCurr.Checked;
                        SetTreeNodeChildrenChecked(ndC,bChecked);
                    }
                }
            }  
        }

        /// <summary>
        /// 递归调用保存用户权限设置
        /// </summary>
        /// <param name="ndCurr">当前树节点</param>
        /// <param name="sUserId">用户编号</param>
        /// <returns>返回是否保存成功</returns>
        private bool SaveNodeRights(TreeNode ndCurr, string sUserId)
        {
            bool bIsOK = false;
            string sRtId = "";
            string sX = "";
            long nX = 0;
            StringBuilder sSql = new StringBuilder("");
            if (ndCurr != null)
            {
                sRtId = ndCurr.Name;//
                //先保存权限
                if (ndCurr.Checked)
                {
                    string err1 = "";                                               
                    sSql.Append("select count(*) nCount from  TPB_URTS where cUserId='" + sUserId + "' and cRId='" + sRtId + "'");
                    //sX = Zqm.DBBase.DBOptrForComm.OptExecRetInt(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), out nX);
                    DataSet ds1 = SunEast.App.PubDBCommFuns.GetDataBySql(sSql.ToString(),out err1);
                    sX = ds1.Tables[1].Rows[0].ItemArray[0].ToString();
                    bIsOK = sX == "0";
                    if (sX == "0")
                    {
                        if (nX == 0)
                        {
                            string err2 = "";
                            sSql.Remove(0, sSql.Length);
                            sSql.Append("Insert into TPB_URTS(cUserId,cRId,cCreator) values('" + sUserId + "','" + sRtId + "','" + UserInformation.UserName + "')");
                            DataSet ds2 = SunEast.App.PubDBCommFuns.GetDataBySql(sSql.ToString(), out err2);
                            sX = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                            bIsOK = sX == "0";
                        }
                    }
                }
                else
                {
                    string err3 = "";
                    sSql.Remove(0, sSql.Length);
                    sSql.Append("Delete TPB_URTS where cUserId='" + sUserId + "' and cRId='" + sRtId + "'");
                    DataSet ds3 = SunEast.App.PubDBCommFuns.GetDataBySql(sSql.ToString(), out err3);
                    sX = ds3.Tables[0].Rows[0].ItemArray[0].ToString();
                    bIsOK = sX == "0";
                }
                if (prgRTS.Value < prgRTS.Maximum)
                    prgRTS.Value++;
                //递归保存其所有子节点权限
                if (ndCurr.Nodes.Count > 0)
                {
                    foreach (TreeNode ndX in ndCurr.Nodes)
                    {
                        SaveNodeRights(ndX, sUserId);
                    }
                }

            }
            return (bIsOK);
        }


        #endregion
        public frmUserRight()
        {
            InitializeComponent();
        }

        private void frmUserRight_Load(object sender, EventArgs e)
        {
            //
            string sErr = "";
            OpenUserData();
            LoadSysRights(UserInformation.UserId);
            LoadWHouseRights(UserInformation.UserId);
            LoadMgrAreaRights(UserInformation.UserId);
            DataRowView drvX = (DataRowView)bdsUser.Current;
            //if (drvX == null) return;
            OpenUserRights(drvX["cUserId"].ToString());
            
        }

        private void bdsUser_PositionChanged(object sender, EventArgs e)
        {
            tbcMain_SelectedIndexChanged(null, null);
        }

        private void trvRight_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!bIsAllowChangeCheckState) return;
            TreeNode ndX = e.Node;
            TreeNode ndP = null;
            if (ndX == null) return;
            ndX.TreeView.BeginUpdate();
            SetTreeNodeChildrenChecked(ndX, ndX.Checked);
            ndX.TreeView.EndUpdate();
        }

        private void trvRight_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            bIsAllowSetRts = true;
            btn_Cancel.Enabled = bIsAllowSetRts;
            btn_SaveRTS.Enabled = bIsAllowSetRts;
            chk_SelAll.Enabled = bIsAllowSetRts;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            bdsUser_PositionChanged(null, null);
        }

        private void chk_SelAll_Click(object sender, EventArgs e)
        {
            if (trvRight.Nodes.Count > 0)
            {
                trvRight.BeginUpdate();
                foreach (TreeNode ndY in trvRight.Nodes)
                {
                    ndY.Checked = chk_SelAll.Checked;
                    //if (ndY.Nodes.Count > 0)
                    //{
                    //    foreach (TreeNode ndX in ndY.Nodes)
                    //    {
                    //        ndX.Checked = chk_SelAll.Checked;
                    //    }
                    //}
                }
                trvRight.EndUpdate();
            }
        }

        private void btn_SaveRTS_Click(object sender, EventArgs e)
        {
            if (!bIsAllowSetRts)
            {
                MessageBox.Show("对不起，请先点“设置权限”按钮");
                return;
            }
            SaveUserRigts();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_UWRights_Click(object sender, EventArgs e)
        {
            bIsAllowSetUW  = true;

            btn_UWCancel.Enabled = bIsAllowSetUW;
            this.btnUWSave.Enabled = bIsAllowSetUW;
            this.chkUWCheckAll.Enabled = bIsAllowSetUW;
            btn_UWRights.Enabled = !bIsAllowSetUW;
        }

        private void btn_UWCancel_Click(object sender, EventArgs e)
        {
            bdsUser_PositionChanged(null, null);
        }

        private void btnUWFCancel_Click(object sender, EventArgs e)
        {
            bdsUser_PositionChanged(null, null);
        }


        private void chkUWCheckAll_Click(object sender, EventArgs e)
        {
            if (this.trvUWRights.Nodes.Count > 0)
            {
                trvUWRights.BeginUpdate();
                foreach (TreeNode ndY in trvUWRights.Nodes)
                {
                    ndY.Checked = chkUWCheckAll.Checked;
                    if (ndY.Nodes.Count > 0)
                    {
                        foreach (TreeNode ndX in ndY.Nodes)
                        {
                            ndX.Checked = chkUWCheckAll.Checked;
                        }
                    }
                }
                trvUWRights.EndUpdate();
            }
        }

        private void tbcMain_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnUWSave_Click(object sender, EventArgs e)
        {
            SaveUserUWRigts();
        }

        private void btnUWFClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUWClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsGrdOpend) return;
            string sUserId = "";
            DataRowView drvX = (DataRowView)bdsUser.Current;
            if (drvX == null) return;
            sUserId = drvX["cUserId"].ToString();
            switch (tbcMain.SelectedTab.Name)
            {
                case "tbpFun":
                    OpenUserRights(sUserId.Trim());
                    break;
                case "tbpUserWHouse":
                    OpenUserUWRights(sUserId.Trim());
                    break;
                case "tbp_UserMgrArea":
                    OpenUserUMgrAreaRights(sUserId.Trim());
                    break;
            }
        }

        private void btn_UMARights_Click(object sender, EventArgs e)
        {
            bIsAllowSetUMA = true;

            this.btn_UMAUndo.Enabled = bIsAllowSetUMA;
            this.btn_UMASave.Enabled = bIsAllowSetUMA;
            this.chk_UMAAll.Enabled = bIsAllowSetUMA;
            this.btn_UWRights.Enabled = !bIsAllowSetUMA;
        }

        private void chk_UMAAll_Click(object sender, EventArgs e)
        {
            if (this.trv_MgrArea.Nodes.Count > 0)
            {
                trv_MgrArea.BeginUpdate();
                foreach (TreeNode ndY in trv_MgrArea.Nodes)
                {
                    ndY.Checked = chk_UMAAll.Checked;
                    if (ndY.Nodes.Count > 0)
                    {
                        foreach (TreeNode ndX in ndY.Nodes)
                        {
                            ndX.Checked = chk_UMAAll.Checked;
                        }
                    }
                }
                trv_MgrArea.EndUpdate();
            }
        }

        private void btn_UMAUndo_Click(object sender, EventArgs e)
        {
            btn_UMARights.Enabled = true;
            bdsUser_PositionChanged(null, null);
        }

        private void btn_UMASave_Click(object sender, EventArgs e)
        {
            this.SaveUserUMARigts();
        }
    }
}

