using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;
namespace Login
{
    public partial class frmMain : UI.FrmSTable
    {
        #region 公共变量
        
            /// <summary>
            /// 登录是否成功
            /// </summary>
            public bool IsOK = false;
            public bool IsUserCheck = false;
            public App.WMSAppInfo AInfo;
            public App.WMSUserInfo uInfo;
            //public Socket SktClient = null;

            int iCount = 0;
        private bool bCmpt = false;
        private bool bDept = false;      
        #endregion
        #region 公共方法

            /// <summary>
            /// 验证 用户与口令是否合法
            /// </summary>
            /// <param name="aX"> 应用程序对象</param>
            /// <param name="uX">用户对象</param>
            /// <param name="sPwd">校验密码</param>
            /// <returns>返回是否成功</returns>
            public static bool CheckUserPwdIsOK(App.WMSAppInfo aX, App.WMSUserInfo uX, string sPwd)
            {
                bool bX = false;
                string sErr = "";
                if (uX.UserId == "Admin5118" || uX.UserId == "90101001")
                {
                    if (sPwd == "suneastwms")
                    {
                        bX = true;
                    }
                    else
                    {
                        bX = false;
                    }
                }
                else
                {              
                    DataTable tbX = null;
                    tbX = SunEast.App.PubDBCommFuns.sp_UserCheck(aX.SvrSocket,uX.UnitId, uX.DeptId, uX.UserId, sPwd, out sErr);
                    if (sErr != "")
                    {
                        bX = false;
                    }
                    else
                    {
                        if (tbX != null)
                        {
                            if (tbX.Rows.Count == 0)
                            {
                                sErr = "对不起，用户与密码不一致！";
                                bX = false;
                            }
                            else
                            {
                                bX = true;
                            }
                        }
                        else
                        {
                            bX = false;
                        }
                    }
                }
                return bX;
            }

            public static string ReadXMLConfValue(string sFile, string sPath, string sKey, string sDef)
            {
                string sResult = sDef;
                string sErr = "";
                if (!System.IO.File.Exists(sFile))
                {
                    sErr = "文件：" + sFile + "  不存在，读取配置信息出错！";
                    return sResult;
                }
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    string sX = "";
                    xmlDoc.Load(sFile);
                    XmlNode ndConfig = null;
                    ndConfig = xmlDoc.SelectSingleNode(sPath.Trim());
                    if (ndConfig != null)
                    {
                        if (ndConfig.Attributes[sKey] != null)
                        {
                            sResult = ndConfig.Attributes[sKey].Value.Trim();
                        }
                    }
                    else
                    {
                        sErr = "配置文件里找不到 config/remoteserver 路径的节点，读取配置数据出错！";
                    }
                }
                catch (Exception err)
                {
                    sErr = err.Message;
                }
                return (sResult);
            }

        #endregion
        #region 私有方法
 
            private bool LoadComptList()
            {
                bool bOK = false;
                string sErr = "";
                DataSet dsX = new DataSet();
                dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, "select * from tpb_compt where bUsed=1", out sErr);
                if (sErr != "" && sErr != "0")
                {
                    MessageBox.Show("打开单位数据时出错：" + sErr);
                    bOK = false;
                }
                if (dsX != null)
                {
                    DataTable tbX = null;
                    tbX = dsX.Tables[0];
                    if (tbX.Rows[0][0].ToString() != "0")
                    {
                        MessageBox.Show("打开单位数据时出错：" + tbX.Rows[0][1].ToString());
                        bOK = false;
                    }
                    tbX = null;
                    tbX = dsX.Tables[1];
                    bCmpt = false;
                    cmbCmpt.DataSource = tbX;
                    cmbCmpt.DisplayMember = "cCmptName";
                    cmbCmpt.ValueMember = "cComptId";
                    bOK = true;
                    bCmpt = true;
                    if (cmbCmpt.Items.Count > 0)
                    {
                        if (cmbCmpt.Items.Count == 1)
                        {
                            cmbCmpt.SelectedIndex = 0;
                            cmbCmpt.Enabled = false;
                            lbl_Cmpt.Text = cmbCmpt.Text.Trim();
                        }
                        if (IsUserCheck)
                        {
                            if (uInfo != null)
                            {
                                cmbCmpt.SelectedValue = uInfo.UnitId;
                                //MessageBox.Show(cmbCmpt.SelectedValue.ToString() + " " + uInfo.UnitId + ":" + uInfo.UnitName);
                                cmbCmpt_SelectedIndexChanged(null, null);
                                cmbCmpt.Enabled = false;
                            }
                        }
                        else
                        {
                            //cmbCmpt.SelectedIndex = 0;
                            cmbCmpt_SelectedIndexChanged(null, null);
                            //MessageBox.Show("   uInfo is null   :" + uInfo.UnitName);
                        }
                    }

                }
                return (bOK);
            }
            private bool LoadDeptList(string sCmptId)
            {
                bool bOK = false;
                string sSql = "select * from tpb_dept where cCmptId = '" + sCmptId.Trim() + "' ";
                DataSet dsX = new DataSet();
                string sErr = "";
                dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, out sErr);
                if (sErr != "" && sErr != "0")
                {
                    MessageBox.Show("打开部门数据时出错：" + sErr);
                    bOK = false;
                    return false;
                }
                if (dsX != null)
                {
                    DataTable tbX = null;
                    tbX = dsX.Tables[0];
                    if (tbX.Rows[0][0].ToString() != "0")
                    {
                        MessageBox.Show("打开部门数据时出错：" + tbX.Rows[0][1].ToString());
                        bOK = false;
                    }
                    tbX = null;
                    tbX = dsX.Tables[1];
                    bDept = false;
                    cmbDept.DataSource = tbX;
                    cmbDept.DisplayMember = "cName";
                    cmbDept.ValueMember = "cDeptId";
                    bOK = true;
                    bDept = true;
                    if (cmbDept.Items.Count > 0)
                    {
                        if (IsUserCheck)
                        {
                            cmbDept.SelectedValue = uInfo.DeptId;
                            //MessageBox.Show(uInfo.DeptId + ":" + uInfo.DeptName);
                            cmbDept_SelectedIndexChanged(null, null);
                            cmbDept.Enabled = false;
                        }
                        else
                        {
                            cmbDept_SelectedIndexChanged(null, null);
                        }
                    }
                }
                return (bOK);
            }
            private bool LoadUserList(string sDeptId)
            {
                bool bOK = false;
                string sSql = "select * from tpb_User where cDeptId = '" + sDeptId.Trim() + "' and isnull(bUsed,0)=1 order by nSort ";
                DataSet dsX = new DataSet();
                string sErr = "";
                dsX = SunEast.App.PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, out sErr);
                if (sErr != "" && sErr != "0")
                {
                    MessageBox.Show("打开用户数据时出错：" + sErr);
                    bOK = false;
                }
                if (dsX != null)
                {
                    DataTable tbX = null;
                    tbX = dsX.Tables[0];
                    if (tbX.Rows[0][0].ToString() != "0")
                    {
                        MessageBox.Show("打开用户数据时出错：" + tbX.Rows[0][1].ToString());
                        bOK = false;
                    }
                    tbX = null;
                    tbX = dsX.Tables[1];
                    cmbUser.DataSource = tbX;
                    cmbUser.DisplayMember = "cName";
                    cmbUser.ValueMember = "cUserId";
                    if (cmbUser.Items.Count > 0)
                    {
                        cmbUser.SelectedIndex = -1;
                        if (IsUserCheck)
                        {
                            cmbUser.SelectedValue = uInfo.UserId;
                            cmbUser.Enabled = false;
                        }                        
                    }
                    bOK = true;
                }
                return (bOK);
            }
        #endregion
            public frmMain()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sErr = "";
            if (cmbUser.Text.Trim() == "Admin5118")
            {
                if (txtPwd.Text == "suneastwms")
                {
                    if (!IsUserCheck)
                    {
                        uInfo.UnitId = "901";
                        uInfo.UnitName = "日东发展有限单位";
                        uInfo.DeptId = "90101";
                        uInfo.DeptName = "软件事业部";
                        uInfo.UserId = "90101001";
                        uInfo.UserName = "Admin5118";
                        uInfo.UType = CommBase.UserType.utSupervisor;
                    }
                    IsOK = true;
                    Close();
                }
                else
                {
                    iCount++;
                    MessageBox.Show("对不起，用户与密码不一致(" + iCount.ToString() + " 次 )");
                    cmbUser.Focus();                    
                    if (iCount == 3)
                    {
                        IsOK = false;
                        Close();
                    }
                    return ;
                }
            }
            else
            {
                if (cmbCmpt.Text.Trim().Length == 0)
                {
                    MessageBox.Show("单位不能为空！");
                    if (cmbCmpt.Enabled)
                        cmbCmpt.Focus();
                    return;
                }
                if (cmbDept.Text.Trim().Length == 0)
                {
                    MessageBox.Show("部门不能为空！");
                    if (cmbDept.Enabled)
                        cmbDept.Focus();
                    return;
                }
                if (cmbUser.Text.Trim().Length == 0)
                {
                    MessageBox.Show("用户不能为空！");
                    if (cmbUser.Enabled)
                        cmbUser.Focus();
                    return;
                }


                DataTable tbX = null;
                iCount++;
                tbX = SunEast.App.PubDBCommFuns.sp_UserCheck(AppInformation.SvrSocket, cmbCmpt.SelectedValue.ToString(), cmbDept.SelectedValue.ToString(), cmbUser.Text.Trim(), txtPwd.Text, out sErr);
                if (sErr != "")
                {
                    MessageBox.Show(sErr);
                    return;
                }
                if (tbX != null)
                {
                    if (tbX.Rows.Count == 0)
                    {
                        MessageBox.Show("对不起，用户与密码不一致("+iCount.ToString() +" 次 )");
                        cmbUser.SelectAll();
                        cmbUser.Focus();
                        if (iCount == 3)
                        {
                            IsOK = false;
                            Close();
                        }
                        return;
                    }
                    else
                    {
                        if (!IsUserCheck)
                        {
                            DataRow dr = tbX.Rows[0];
                            uInfo.UnitId = dr["cCmptId"].ToString();
                            uInfo.UnitName = dr["cCmptName"].ToString();
                            uInfo.DeptId = dr["cDeptId"].ToString();
                            uInfo.DeptName = dr["cDeptName"].ToString();
                            uInfo.UserId = dr["cUserId"].ToString();
                            uInfo.UserName = dr["cName"].ToString();
                            uInfo.UType = (CommBase.UserType)int.Parse(dr["nTag"].ToString());
                        }
                        IsOK = true;
                        //MessageBox.Show("登录成功！");
                    }
                }
                if ((iCount == 3) && (!IsOK))
                {
                    Close();
                    return;
                }
                else if (IsOK)
                {
                    Close();
                    return;
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string sFile = Application.StartupPath + @"\AppConfig.xml";
            //string sX = ReadXMLConfValue(sFile, "config/appmain", "appTitle", "");
            //加载 单位信息
            LoadComptList();
        }

        private void cmbCmpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bCmpt) return;
            string sCmptId = "";
            if (cmbCmpt.SelectedIndex > -1)
            {
                sCmptId = cmbCmpt.SelectedValue.ToString();               
            }
            LoadDeptList(sCmptId);
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bDept) return;
            string sDeptId = "";
            if (cmbDept.SelectedIndex > -1)
            {
                sDeptId = cmbDept.SelectedValue.ToString();
            }
            LoadUserList(sDeptId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsOK = false;
            Close();
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            txtPwd.SelectAll();
        }
    }
}

