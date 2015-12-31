using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommBase
{
    public partial class FormBase : Form
    {
        #region ˽�б���
            bool _IsEnterAsTabKey = true;  // �س����ƽ���
        #endregion

        #region ˽�з���
            
        #endregion

        #region ��������
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
        #endregion

        #region ��������
            //
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
        public FormBase()
        {
            InitializeComponent();
            _IsEnterAsTabKey = true;
        }
            
    }
}