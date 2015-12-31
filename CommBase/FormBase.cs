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
        #region 私有变量
            bool _IsEnterAsTabKey = true;  // 回车下移焦点
        #endregion

        #region 私有方法
            
        #endregion

        #region 公共属性
            /// <summary>
            /// 使回车键作为 Tab键使用
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

        #region 公共方法
            //
        #endregion

        #region 继承事件
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