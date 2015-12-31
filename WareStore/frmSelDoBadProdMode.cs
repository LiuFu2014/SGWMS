using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SunEast.App;


namespace WareStoreMS
{
    public partial class frmSelDoBadProdMode : UI.FrmSTable
    {
        #region ˽�б���

        #endregion

        #region ��������

        private bool _IsResultOK = false;
        public bool IsResultOK
        {
            get { return _IsResultOK; }
            set { _IsResultOK = value; }
        }

        private string _SelDoMode = "";
        public string SelDoMode
        {
            get { return _SelDoMode.Trim(); }
            set { _SelDoMode = value.Trim(); }
        }
        #endregion

        #region ˽�з���
        private void LoadDoBadMatMode()
        {
            string sErr = "";
            string sSql = "select * from TWC_BaseItem where bUsed=1 and cItemType=''";
            sSql += " order by nSort,nId";
            DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket,sSql,"TWC_BaseItem","",out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            else if (dsX != null && dsX.Tables["TWC_BaseItem"] != null)
            {
                //cItemNo,cItemName
                DataTable tbX = dsX.Tables["TWC_BaseItem"];
                cmb_DoMode.Items.Clear();
                cmb_DoMode.DataSource = tbX;
                cmb_DoMode.DisplayMember = "cItemName";
                cmb_DoMode.ValueMember = "cItemNo";
            }
        }
        #endregion

        #region ��������

        #endregion

        public frmSelDoBadProdMode()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (cmb_DoMode.Text.Trim() == "")
            {
                MessageBox.Show("�Բ��𣬴���ʽ����Ϊ�գ�");
                cmb_DoMode.Focus();
                return;
            }
            _SelDoMode = cmb_DoMode.Text.Trim();
            _IsResultOK = true;
            Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            _IsResultOK = false;
            Close();
        }
    }
}

