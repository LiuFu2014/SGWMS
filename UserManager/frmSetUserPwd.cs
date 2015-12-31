using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserMS
{
    public partial class frmSetUserPwd : UI.FrmSTable
    {
        #region 私有变量
            int iCount = 0; //验证密码的次数
        #endregion
        #region 属性

            
            private bool isOK = false;
            /// <summary>
            /// 是否正确返回
            /// </summary>
            public bool IsOK
            {
                get { return isOK; }
                //set { isOK = value; }
            }

            private string newpwdvalue = "";            
            /// <summary>
            /// 新设置密码值
            /// </summary>
            public string NewPwdValue
            {
                get { return newpwdvalue; }
                set { newpwdvalue = value; }
            }
            
            private int _PwdLen = 0;
            /// <summary>
            /// 强制密码长度 0：表示不限制长度 但最长不超过50
            /// </summary>
            public int PwdLen
            {
                get { return _PwdLen; }
                set { _PwdLen = value; }
            }
        #endregion



        public frmSetUserPwd()
        {
            InitializeComponent();
        }

        private void frmSetUserPwd_Load(object sender, EventArgs e)
        {
            if (UserInformation != null)
            {
                txt_OperatorId.Text = UserInformation.UserId;
                txt_OperatorName.Text = UserInformation.UserName;                
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (txt_Pwd1.Text != txt_Pwd2.Text)
            {
                MessageBox.Show("对不起，录入的新密码与确认密码不一致，请重新录入！");
                txt_Pwd1.Focus();
                return;
            }
            if (_PwdLen > 0 && _PwdLen > txt_Pwd1.Text.Length)
            {
                MessageBox.Show("对不起，录入的新密码的长度必须介于 "+ _PwdLen.ToString() +" 与 " +  "50 之间 ！");
                txt_Pwd1.Focus();
                return;
            }
            iCount++;           
            if (Login.Login.CheckUserPwdIsOk(AppInformation, UserInformation, txt_OperatorPwd.Text))
            {
                newpwdvalue = txt_Pwd1.Text;
                isOK = true;
                Close();
            }
            else
            {
                MessageBox.Show("对不起，操作员的密码不对，请重新录入！");
                txt_OperatorPwd.SelectAll();
                txt_OperatorPwd.Focus();
                if (iCount >= 3)
                {
                    MessageBox.Show("对不起，操作员的身份验证失败次数超过3次，将退出设置！");
                    newpwdvalue = "";
                    isOK = false ;
                    Close();
                }
                return;
            }                
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            isOK = false;
            Close();
        }
    }
}