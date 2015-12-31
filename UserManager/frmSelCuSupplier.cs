using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SunEast.App;

namespace UserMS
{
    /// <summary>
    /// 处理选择客户供应商事件
    /// </summary>
    /// <param name="sCSId">编码</param>
    /// <param name="sCSNameJ">名称简称</param>
    /// <param name="sCSNameQ">全称</param>
    /// <param name="csType">客户供应商类型</param>
    /// <param name="sTel">联系电话</param>
    /// <param name="sFax">传真</param>
    /// <param name="sAddress">地址</param>
    /// <param name="sRemark">备注</param>
    /// <param name="cType">类型描述</param>
    /// <param name="nIsInner">是否内部单位（0 否，1 是）</param>
    /// <param name="nIsFactory">是否生产厂家（0 否，1 是）</param>
    /// <param name="sIsInner">是否内部单位描述</param>
    /// <param name="sIsFactory">是否生产厂家描述</param>
    /// <param name="bUsed">是否起用 0 停用 1 启用</param>
    /// <param name="sUsed">是否起用描述</param>
    /// <returns></returns>
    public delegate void DoSelCuSupplierEvent(string sCSId,string sCSNameJ,string sCSNameQ,CSType csType,string sTel,string sFax,string sAddress,
        string sRemark,string cType,int nIsInner,int nIsFactory,string sIsInner,string sIsFactory,int bUsed,string sUsed);

    public partial class frmSelCuSupplier : UI.FrmSTable
    {
        #region
        private CSType _CuSupplierType = CSType.cstAll;
        public CSType CuSupplierType
        {
            get { return _CuSupplierType; }
            set 
            {
                _CuSupplierType = value;
                SetText();
                lbl_Factory.Visible = _CuSupplierType == CSType.cstSupplier;
                cmb_nIsFactory.Visible = _CuSupplierType == CSType.cstSupplier;
                cmb_nIsFactory.SelectedIndex = -1;
            }
        }

        private DoSelCuSupplierEvent _DoSelCuSupplier = null;
        [Description("客户供应商选择事件")]
        public DoSelCuSupplierEvent DoSelCuSupplier
        {
            get { return _DoSelCuSupplier; }
            set { _DoSelCuSupplier = value; }
        }

        private int _IsInner = -1;
        [Description("是否内部单位")]
        public int IsInner
        {
            get { return _IsInner; }
            set
            {
                _IsInner = value;
                cmb_nIsInner.Enabled = _IsInner == -1;
                cmb_nIsInner.SelectedIndex = _IsInner;
            }
        }


        private int _IsFactory = -1;
        [Description("是否生产厂家")]
        public int IsFactory
        {
            get { return _IsFactory; }
            set
            {
                _IsFactory = value;
                cmb_nIsFactory.Enabled = _IsFactory == -1;
                cmb_nIsFactory.SelectedIndex = _IsFactory;
            }
        }

        #endregion

        #region
        /// <summary>
        /// 设置标题
        /// </summary>
        private void SetText()
        {
            switch (_CuSupplierType)
            {
                case CSType.cstAll:
                    Text = "选择客户供应商";
                    break;
                case CSType.cstCustomer:
                    Text = "选择客户";
                    break;
                case CSType.cstSupplier:
                    Text = "选择供应商";
                    break;
            }
        }

        
        #endregion

        public frmSelCuSupplier()
        {
            InitializeComponent();
        }

        private void frmSelCuSupplier_Load(object sender, EventArgs e)
        {
            SetText();
            btn_Reset_Click(null, null);
            //
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_cName.Text = "";
            if (cmb_nIsInner.Enabled)
                cmb_nIsInner.SelectedIndex = -1;
            if (cmb_nIsFactory.Enabled)
                cmb_nIsFactory.SelectedIndex = -1;
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            StringBuilder  sSql = new StringBuilder( "select * from V_CuSupplier where isnull(bUsed,1)=1 ");
            string sErr = "";
            string sX = "";
            if (_CuSupplierType != CSType.cstAll)
            {
                sSql.Append(" and nType=" + ((int) _CuSupplierType).ToString());
            }  
            sX = txt_cName.Text.Trim();
            if (sX != "")
            {
                sSql.Append(" and ((cCSId like '%" + sX + "%') or (cCSNameJ like '%" + sX + "%') or (cWBJM like '%" + sX + "%') or (cPYJM like '%" + sX + "%') )");
            }
            if (cmb_nIsFactory.Text.Trim() != "" && cmb_nIsFactory.SelectedIndex > -1)
            {
                sSql.Append(" and nIsFactory=" + cmb_nIsFactory.SelectedIndex.ToString());
            }
            if (cmb_nIsInner.Text.Trim() != "" && cmb_nIsInner.SelectedIndex > -1)
            {
                sSql.Append(" and nIsInner=" + cmb_nIsInner.SelectedIndex.ToString());
            }
            DataSet dsX = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql.ToString(), "CuSupplier", 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message);
            }
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            if (dsX != null)
            {
                DataTable tbX = dsX.Tables["CuSupplier"];
                bds_Data.DataSource = tbX.Copy();
            }
            dsX.Clear();

        }

        private void grd_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bds_Data.Count == 0)
            {
                MessageBox.Show("对不起，无数据可选择！");
                return;
            }
            DataRowView drv = (DataRowView)bds_Data.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起，没有选择数据！");
                return;
            }
            if (_DoSelCuSupplier!= null)
            {
                _DoSelCuSupplier(drv["cCSId"].ToString(), drv["cCSNameJ"].ToString(), drv["cCSNameQ"].ToString(), (CSType)Convert.ToInt16(drv["nType"]),
                    drv["cTel"].ToString(), drv["cFax"].ToString(), drv["cAddress"].ToString(), drv["cRemark"].ToString(), drv["cType"].ToString(),
                    Convert.ToInt16(drv["nIsInner"]), Convert.ToInt16(drv["nIsFactory"]), drv["cIsInner"].ToString(), drv["cIsFactory"].ToString(),
                    Convert.ToInt16(drv["bUsed"]), drv["cUsed"].ToString());
            }
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (bds_Data.Count == 0)
            {
                MessageBox.Show("对不起，无数据可选择！");
                return;
            }
            if (grd_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("对不起，没有选择数据！！");
                return;
            }
            prgMain.Maximum = grd_Data.SelectedRows.Count;
            prgMain.Minimum = 0;
            prgMain.Value = 0;
            prgMain.Visible = true;
            foreach (DataGridViewRow grdr in grd_Data.SelectedRows)
            {
                CSType csTypeX = CSType.cstAll;
                if (grdr.Cells["col_nType"].Value != null)
                {
                    csTypeX = (CSType)Convert.ToInt16(grdr.Cells["col_nType"].Value);
                }
                int nIsInner = 0;
                if (grdr.Cells["col_nIsInner"].Value != null)
                {
                    nIsInner = Convert.ToInt16(grdr.Cells["col_nIsInner"].Value);
                }
                int nIsFactory = 0;
                if (grdr.Cells["col_nIsFactory"].Value != null)
                {
                    nIsFactory = Convert.ToInt16(grdr.Cells["col_nIsFactory"].Value);
                }
                int bUsed = 1;                
                if (grdr.Cells["col_bUsed"].Value != null)
                {
                    bUsed = Convert.ToInt16(grdr.Cells["col_bUsed"].Value);
                }
                if (_DoSelCuSupplier != null)
                {
                    _DoSelCuSupplier(grdr.Cells["col_cCSId"].Value.ToString(), grdr.Cells["col_cCSNameJ"].Value.ToString(), grdr.Cells["col_cCSNameQ"].Value.ToString(), csTypeX,
                    grdr.Cells["col_cTel"].Value.ToString(), grdr.Cells["col_cFax"].Value.ToString(), grdr.Cells["col_cAddress"].Value.ToString(), grdr.Cells["col_cRemark"].Value.ToString(),
                    grdr.Cells["col_cType"].Value.ToString(), nIsInner, nIsFactory, grdr.Cells["col_cIsInner"].Value.ToString(),
                    grdr.Cells["col_cIsFactory"].Value.ToString(), bUsed, grdr.Cells["col_cUsed"].Value.ToString());
                }
                prgMain.Value++;
            }
            Close();
        }


    }
}

