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
    public partial class frmChkPosList : UI.FrmSTable
    {
        #region 私有变量

        #endregion

        #region 公共属性

        private string _CheckNo = "";
        public string CheckNo
        {
            get { return _CheckNo.Trim(); }
            set 
            { 
                _CheckNo = value.Trim();
                lbl_ChkNo.Text = _CheckNo;
            }
        }

        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private bool _IsOK = false;
        public bool IsOK
        {
            get { return _IsOK; }
        }
       
        #endregion

        public frmChkPosList()
        {
            InitializeComponent();
        }

        private void frmChkPosList_Load(object sender, EventArgs e)
        {
            string sErr = "";
            if (_CheckNo.Trim() == "")
                return;
            DataTable tbGrid = PubDBCommFuns.sp_Chk_GetChkPosList(AppInformation.SvrSocket, _CheckNo.Trim(), _Status, out sErr);
            if (sErr.Trim() != "" && sErr != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            bsGrid.DataSource  = tbGrid ;
            grdData.DataSource = bsGrid ;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (bsGrid.Count == 0)
            {
                MessageBox.Show("对不起，没有任务数据可下发！");
                return;
            }
            if (grdData.SelectedRows != null && grdData.SelectedRows.Count == 0)
            {
                MessageBox.Show("对不起，没有选择要下发的任务数据！");
                return;
            }
            int iX = 0;
            foreach (DataGridViewRow dr in grdData.SelectedRows)
            {
                if (int.Parse(dr.Cells[colnStatus.Name].Value.ToString()) < 1)
                {
                    string sErr = "";
                    string sX = PubDBCommFuns.sp_Chk_DoCheckTask(AppInformation.SvrSocket, UserInformation.UserName, "WMS", UserInformation.UnitId,
                        dr.Cells[colcPosId.Name].Value.ToString().Trim(), int.Parse(cmb_Station.Text.Trim()), _CheckNo, out sErr);
                    if (sX == "0" || sErr.Trim() == "")
                    {
                        iX++;
                    }

                }
            }
            MessageBox.Show("已经成功下发："  + iX.ToString() + " 条盘点任务！");
            _IsOK = iX  > 0;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _IsOK = false;
            Close();
        }
    }
}

