using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App;

namespace UI
{
    public partial class FrmFindBase : Form
    {
        #region ˽�б���
            private bool _IsEnterAsTabKey = true;  // �س����ƽ���
            private WMSAppInfo _AppInformation;
            private WMSUserInfo _UserInformation;
            private bool _IsResultOK = false;
            private object _ResultUser;
        #endregion

        #region ˽�з���

        #endregion

        #region �������Լ�����
            public App.WMSAppInfo AppInformation
            {
                get { return(_AppInformation);}
                set { _AppInformation = value; }
            }
            public App.WMSUserInfo UserInformation
            {
                get { return (_UserInformation); }
                set { _UserInformation = value; }
            }
            /// <summary>
            /// ʹ�س�����Ϊ Tab��ʹ��
            /// </summary>
            public bool IsEnterAsTabKey
            {
                get { return (_IsEnterAsTabKey); }
                set
                {
                    _IsEnterAsTabKey = value;
                    //this.KeyPreview = value;
                }
            }
            public bool IsResultOK
            {
                get { return (_IsResultOK); }
                set
                {
                    _IsResultOK = value;
                }
            }
            public object ResultUser
            {
                get { return (_ResultUser); }
                set
                {
                    _ResultUser = value;
                }
            }
        #endregion
        #region �̳��¼�
            protected override void OnKeyDown(KeyEventArgs e)
            {
                if (_IsEnterAsTabKey)
                {
                    //this.SelectNextControl(ActiveControl, true, true, true, true);
                    SendKeys.Send("{Tab}");
                    e.Handled = true;
                }
                base.OnKeyDown(e);
            }
        #endregion
        public FrmFindBase()
        {
            InitializeComponent();
            _IsEnterAsTabKey = true;
        }
    }
}