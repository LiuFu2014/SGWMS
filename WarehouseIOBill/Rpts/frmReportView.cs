using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WarehouseIOBill.Rpts
{
    public partial class frmReportView : Form
    {
        public frmReportView()
        {
            InitializeComponent();
        }

        private DataSet _DsData = null ;
        public DataSet DsData
        {
            get { return _DsData; }
            set { _DsData = value; }
        }
        private void frmReportView_Load(object sender, EventArgs e)
        {

            this.rptViewer.RefreshReport();
        }
    }
}