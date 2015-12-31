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
    public partial class frmSelStkMaterail : UI.FrmSTable
    {
        public frmSelStkMaterail()
        {
            InitializeComponent();
        }

        #region 私有变量
            private StringBuilder _StrSql = new StringBuilder("");
            private StringBuilder _CondtionDesc = new StringBuilder("");
        #endregion
        
        #region 共有属性
        private bool _IsSelect = false;
        public bool IsSelect
        {
            get { return _IsSelect; }
            set 
            {
                _IsSelect = value;
                if (_IsSelect)
                {
                    btnOK.Visible = _IsSelect;
                    Text = "选择库存物料";
                }
                else
                {
                    btnOK.Visible = false;
                    Text = "库存物料";
                }

            }
        }

        private bool _IsResultOK = false;
        public bool IsResultOK
        {
            get { return _IsResultOK; }
        }

        public string StrSql
        {
            get { return _StrSql.ToString(); }
        }

        public string ConditionDesc
        {
            get { return _CondtionDesc.ToString(); }
        }

        private string _cBNo = "";
        public string  cBNo
        {
            get { return _cBNo.Trim(); }
        }

        private int _nItem = 0;
        public int nItem
        {
            get { return _nItem; }
        }

        private string _cMNo = "";
        public string cMNo
        {
            get { return _cMNo.Trim(); }
        }
        private string _cMName = "";
        public string cMName
        {
            get { return _cMName.Trim(); }
        }
        private DateTime _dProdDate;
        public DateTime dProdDate
        {
            get { return _dProdDate; }
        }

        private DateTime _dBadDate;
        public DateTime dBadDate
        {
            get { return _dBadDate; }
        }

        private int _nQCStatus = 0;
        public int nQCStatus
        {
            get { return _nQCStatus; }
        }
        private string _cBatchNo = "";
        public string cBatchNo
        {
            get { return _cBatchNo.Trim(); }
        }
        private string _cSpec = "";
        public string cSpec
        {
            get { return _cSpec.Trim(); }
        }

        private double _fQty = 0;
        public double fQty
        {
            get { return _fQty; }
        }

        private string _cUnit = "";
        public string cUnit
        {
            get { return _cUnit.Trim(); }
        }

        #endregion

        #region 私有方法
        private void ResetConiditon()
        {
            txt_cBNoIn.Text = "";
            txt_cMNo.Text = "";
            txt_QCDay.Text = "";
            chk_Date.Checked = false;
            cmb_MatType1.SelectedIndex = -1;
            cmb_nQCStatus.SelectedIndex = -1;
            dtp_dFrom.Value = DateTime.Now.AddDays(-30);
            dtp_To.Value = DateTime.Now;
            txt_cBNoIn.Focus();
        }

        private string GetSql()
        {
            _CondtionDesc.Remove(0, ConditionDesc.Length);
            StringBuilder sSql = new StringBuilder("Select * from V_STOREITEMLIST  where 1=1 ");
            if (txt_cBNoIn.Text.Trim() != "")
            {
                sSql.Append(" and cBNoIn like '%"+ txt_cBNoIn.Text.Trim() +"%'");
                _CondtionDesc.Append(" 入库单号：" + txt_cBNoIn.Text.Trim());
            }
            if (this.cmb_MatType1.Text.Trim() != "" && cmb_MatType1.SelectedValue != null)
            {
                sSql.Append(" and cTypeId1 = '" + cmb_MatType1.SelectedValue.ToString().Trim() + "'");
                _CondtionDesc.Append(" 物料类别：" + cmb_MatType1.Text.Trim());
            }
            if (this.txt_cMNo.Text.Trim() != "" )
            {
                sSql.Append(" and ( (cMNo like  '%" + txt_cMNo.Text.Trim() + "%') or (cMName like  '%" + txt_cMNo.Text.Trim() + "%') or (cPYJM like  '%" + txt_cMNo.Text.Trim() + "%') or (cWBJM like  '%" + txt_cMNo.Text.Trim() + "%') )");
                _CondtionDesc.Append(" 物料：" + txt_cMNo.Text.Trim());
            }
            if (this.cmb_nQCStatus.Text.Trim() != "" && cmb_nQCStatus.SelectedValue != null)
            {
                sSql.Append(" and nQCStatus = " + cmb_nQCStatus.SelectedValue.ToString().Trim());
                _CondtionDesc.Append(" 质检状态：" + cmb_nQCStatus.Text.Trim());
            }
            if (chk_Date.Checked)
            {
                sSql.Append(" and (dDate between  '" + dtp_dFrom.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '"+ dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") +"' )");
                _CondtionDesc.Append(" 入库日期：" + dtp_dFrom.Value.ToString("yyyy-MM-dd") + " — " + dtp_To.Value.ToString("yyyy-MM-dd"));
            }
            if (txt_QCDay.Text.Trim() != "")
            {
                sSql.Append(" and (dBadDate < '"+ DateTime.Now.AddDays(int.Parse(txt_QCDay.Text.Trim())) +"')");
            }
            sSql.Append(" order by nQCStatus,dBadDate,dDate");
            return sSql.ToString();
        }

        #endregion

        #region 公共方法

        #endregion

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            if (chk_Date.Checked)
            {
                if (dtp_dFrom.Value.Date > dtp_To.Value.Date)
                {
                    MessageBox.Show("对不起，起止日期不能大于截止日期！");
                    dtp_dFrom.Focus();
                    return;
                }
            }
            _StrSql.Remove(0, _StrSql.Length);
            _StrSql.Append(GetSql());
            string sErr = "";
            Cursor.Current = Cursors.WaitCursor;
            //_StrSql.Remove(0, _StrSql.Length);
            //_StrSql.Append("select * from V_STOREITEMLIST ");
            DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, _StrSql.ToString(), "V_STOREITEMLIST", "dDate,dProdDate,dBadDate", out sErr);
            Cursor.Current = Cursors.Default;
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            bdsList.DataSource = dsX.Tables[1];
            grdList.DataSource = bdsList;
        }

        private void frmSelStkMaterail_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            if (_IsSelect)
            {
                btnOK.Visible = _IsSelect;
                Text = "选择库存物料";
            }
            else
            {
                btnOK.Visible = false;
                Text = "库存物料";
            }
            ResetConiditon();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            ResetConiditon();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (bdsList.Count == 0)
            {
                MessageBox.Show("对不起，无数据可选择！");
                return;
            }
            DataRowView drv = (DataRowView)bdsList.Current;
            if (drv == null)
            {
                MessageBox.Show("对不起，无数据可选择！");
                return;
            }
            string sX = "1800-01-01 00:00:00";
            _cBNo = drv["cBNoIn"].ToString();
            _nItem = int.Parse(drv["nItemIn"].ToString());
            _cMNo = drv["cMNo"].ToString();
            _cMName = drv["cMName"].ToString().Trim();
            _cSpec = drv["cSpec"].ToString().Trim();
            _cBatchNo = drv["cBatchNo"].ToString().Trim();
            if (drv["dProdDate"].ToString().Trim() != "")
            {
                sX = drv["dProdDate"].ToString().Trim();
            }
            _dProdDate = DateTime.Parse(sX);
            sX = "1800-01-01 00:00:00";
            if (drv["dBadDate"].ToString() != "")
            {
                sX = drv["dBadDate"].ToString().Trim();
            }
            _dBadDate  = DateTime.Parse(sX);
            _fQty = double.Parse(drv["fQty"].ToString());
            _cUnit = drv["cUnit"].ToString();

            _IsResultOK = true;
            Close();
        }

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_IsSelect) return;
            btnOK_Click(null, null);
        }
    }
}

