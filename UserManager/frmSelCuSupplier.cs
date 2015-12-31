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
    /// ����ѡ��ͻ���Ӧ���¼�
    /// </summary>
    /// <param name="sCSId">����</param>
    /// <param name="sCSNameJ">���Ƽ��</param>
    /// <param name="sCSNameQ">ȫ��</param>
    /// <param name="csType">�ͻ���Ӧ������</param>
    /// <param name="sTel">��ϵ�绰</param>
    /// <param name="sFax">����</param>
    /// <param name="sAddress">��ַ</param>
    /// <param name="sRemark">��ע</param>
    /// <param name="cType">��������</param>
    /// <param name="nIsInner">�Ƿ��ڲ���λ��0 ��1 �ǣ�</param>
    /// <param name="nIsFactory">�Ƿ��������ң�0 ��1 �ǣ�</param>
    /// <param name="sIsInner">�Ƿ��ڲ���λ����</param>
    /// <param name="sIsFactory">�Ƿ�������������</param>
    /// <param name="bUsed">�Ƿ����� 0 ͣ�� 1 ����</param>
    /// <param name="sUsed">�Ƿ���������</param>
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
        [Description("�ͻ���Ӧ��ѡ���¼�")]
        public DoSelCuSupplierEvent DoSelCuSupplier
        {
            get { return _DoSelCuSupplier; }
            set { _DoSelCuSupplier = value; }
        }

        private int _IsInner = -1;
        [Description("�Ƿ��ڲ���λ")]
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
        [Description("�Ƿ���������")]
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
        /// ���ñ���
        /// </summary>
        private void SetText()
        {
            switch (_CuSupplierType)
            {
                case CSType.cstAll:
                    Text = "ѡ��ͻ���Ӧ��";
                    break;
                case CSType.cstCustomer:
                    Text = "ѡ��ͻ�";
                    break;
                case CSType.cstSupplier:
                    Text = "ѡ��Ӧ��";
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
                MessageBox.Show("�Բ��������ݿ�ѡ��");
                return;
            }
            DataRowView drv = (DataRowView)bds_Data.Current;
            if (drv == null)
            {
                MessageBox.Show("�Բ���û��ѡ�����ݣ�");
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
                MessageBox.Show("�Բ��������ݿ�ѡ��");
                return;
            }
            if (grd_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("�Բ���û��ѡ�����ݣ���");
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

