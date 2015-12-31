using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace WareStore.Rpts
{
    public partial class frmRptViewer : Form
    {
        #region 公共属性
        private string _RptTitle = "打印预览";
        public string RptTitle
        {
            get { return _RptTitle.Trim(); }
            set
            {
                _RptTitle = value;
                Text = _RptTitle;
            }
        }

        private Dictionary<string, object> _RptParameters = null;
        public Dictionary<string, object> RptParameters
        {
            get { return _RptParameters; }
            set
            {
                _RptParameters = value;
            }
        }

        private ReportClass _RptObj = null;
        public ReportClass RptObj
        {
            get { return _RptObj; }
            set
            {
                _RptObj = value; 
            }
        }
        #endregion

        #region 公共方法

        public bool SetReport()
        {
            bool bOK = false;
            if (_RptObj != null)
            {
                if (_RptParameters != null)
                {
                   foreach(string sKey in _RptParameters.Keys)
                   {
                       _RptObj.SetParameterValue(sKey, _RptParameters[sKey]);
                   }
                }
                rptv_Main.ReportSource = _RptObj;
                bOK = true;
            }
            return bOK;
        }

        #endregion
        public frmRptViewer()
        {
            InitializeComponent();
        }
    }
}