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
        #region ˽�б���
            int iCount = 0; //��֤����Ĵ���
        #endregion
        #region ����

            
            private bool isOK = false;
            /// <summary>
            /// �Ƿ���ȷ����
            /// </summary>
            public bool IsOK
            {
                get { return isOK; }
                //set { isOK = value; }
            }

            private string newpwdvalue = "";            
            /// <summary>
            /// ����������ֵ
            /// </summary>
            public string NewPwdValue
            {
                get { return newpwdvalue; }
                set { newpwdvalue = value; }
            }
            
            private int _PwdLen = 0;
            /// <summary>
            /// ǿ�����볤�� 0����ʾ�����Ƴ��� ���������50
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
                MessageBox.Show("�Բ���¼�����������ȷ�����벻һ�£�������¼�룡");
                txt_Pwd1.Focus();
                return;
            }
            if (_PwdLen > 0 && _PwdLen > txt_Pwd1.Text.Length)
            {
                MessageBox.Show("�Բ���¼���������ĳ��ȱ������ "+ _PwdLen.ToString() +" �� " +  "50 ֮�� ��");
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
                MessageBox.Show("�Բ��𣬲���Ա�����벻�ԣ�������¼�룡");
                txt_OperatorPwd.SelectAll();
                txt_OperatorPwd.Focus();
                if (iCount >= 3)
                {
                    MessageBox.Show("�Բ��𣬲���Ա�������֤ʧ�ܴ�������3�Σ����˳����ã�");
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