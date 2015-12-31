using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public enum InputMsgType { ittString = 0,ittInteger = 1,ittReal = 2 ,ittPwd = 3 };
    public partial class frmInputMessage : Form
    {
        #region 私有变量
            
        #endregion

        #region 公共属性

            private InputMsgType _InputValueType = InputMsgType.ittString;
            public InputMsgType InputValueType
            {
                get { return _InputValueType; }
                set { _InputValueType = value; }
            }

            private string _DefaultValue = "";
            public string DefaultValue
            {
                get { return _DefaultValue.Trim(); }
                set { _DefaultValue = value.Trim(); }
            }

            private string _TitleText = "请录入";
            public string TitleText
            {
                get { return _TitleText.Trim(); }
                set { _TitleText = value.Trim(); }
            }

            private string _PromptText = "请录入：";
            public string PromptText
            {
                get { return _PromptText.Trim(); }
                set { _PromptText = value.Trim(); }
            }

            private string _ResultValue = "";
            public string ResultValue
            {
                get { return _ResultValue.Trim(); }
                set { _ResultValue = value.Trim(); }
            }

            private bool _ResultIsOK = false;
            public bool ResultIsOK
            {
                get { return _ResultIsOK; }
                set { _ResultIsOK = value; }
            }
            
        #endregion
        public frmInputMessage()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            _ResultIsOK = false;
            if (_InputValueType == InputMsgType.ittInteger || _InputValueType == InputMsgType.ittReal)
            {
                _ResultValue = txt_Value.Text.Trim();
                if (_ResultValue.Trim() == "")
                {
                    MessageBox.Show("数值不能为空！");
                    txt_Value.Focus();
                    return;
                }
                if (_InputValueType == InputMsgType.ittInteger && FrmSTable.IsInteger(_ResultValue.Trim()) == false)
                {
                    MessageBox.Show("对不起，请录入正确的整数！");
                    txt_Value.SelectAll();
                    txt_Value.Focus();
                    return;
                }
                if (_InputValueType == InputMsgType.ittReal && FrmSTable.IsNumberic(_ResultValue.Trim()) == false)
                {
                    MessageBox.Show("对不起，请录入正确的数值！");
                    txt_Value.SelectAll();
                    txt_Value.Focus();
                    return;
                }
            }
            else
            {
                if (txt_Value.Text == "")
                {
                    if (MessageBox.Show("您录入为空,需要重新录入码？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        txt_Value.Focus();
                        return;
                    }
                }
                _ResultValue = txt_Value.Text;
            }
            _ResultIsOK = true;
            Close();
        }

        private void frmInputMessage_Load(object sender, EventArgs e)
        {
            if (_TitleText.Trim() != "")
            {
                Text = _TitleText.Trim();
            }
            if (_PromptText.Trim() != "")
            {
                lbl_Text.Text = _PromptText.Trim();
            }
            if (_InputValueType == InputMsgType.ittPwd)
            {
                txt_Value.PasswordChar = '*';
            }
            txt_Value.Text = _DefaultValue;
            txt_Value.SelectAll();
            txt_Value.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ResultIsOK = false;
            Close();
        }

        private void frmInputMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{tab}");
            }
        }
    }
}