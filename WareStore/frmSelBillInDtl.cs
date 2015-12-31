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
    public partial class frmSelBillInDtl : UI.FrmSTable
    {
        #region ˽�б���

        #endregion

        #region ��������

            private string _SelBNo = "";
            public string SelBNo
            {
                get { return _SelBNo.Trim(); }
                set { _SelBNo = value.Trim(); }
            }

            private string _SelItem = "";
            public string SelItem
            {
                get { return _SelItem.Trim(); }
                set { _SelItem = value.Trim(); }
            }

            private string _SelBatchNo = "";
            public string SelBatchNo
            {
                get { return _SelBatchNo.Trim(); }
                set { _SelBatchNo = value.Trim(); }
            }

            private string _SelUnit = "";
            public string SelUnit
            {
                get { return _SelUnit.Trim(); }
                set { _SelUnit = value.Trim(); }
            }

            private string _SelQCStatus = "";
            public string SelQCStatus
            {
                get { return _SelQCStatus.Trim(); }
                set { _SelQCStatus = value.Trim(); }
            }

            private bool _IsSelected = false;
            public bool IsSelected
            {
                get { return _IsSelected; }
                set { _IsSelected = value; }
            }


        #endregion

        #region ˽�з���

        #endregion

        #region ��������

        #endregion
        public frmSelBillInDtl()
        {
            InitializeComponent();
        }

        private void frmSelBillInDtl_Load(object sender, EventArgs e)
        {
            string sWHId = lbl_cWHId.Text.Trim();
            string sMNo = lbl_cMNo.Text.Trim();
            string sErr = "";
            DataTable tbX = PubDBCommFuns.sp_Chk_GetBillInDtlList(AppInformation.SvrSocket, sWHId, sMNo, out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            bsGrid.DataSource = tbX;
            grdData.DataSource = bsGrid;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool bIsFirst = true;
            object objX = null;
            foreach (DataGridViewRow grdr in grdData.SelectedRows)
            {
                if (bIsFirst)
                {
                    _SelBNo = "" + grdr.Cells[colcBNo.Name].Value.ToString().Trim() + "";
                    _SelItem = "" + grdr.Cells[colnItem.Name].Value.ToString().Trim() + "";

                    objX = grdr.Cells[colcBatchNo.Name].Value;
                    if (objX != null)
                        _SelBatchNo = objX.ToString().Trim();
                    else
                        _SelBatchNo = "";

                    objX = grdr.Cells[colcUnit.Name].Value;
                    if (objX != null)
                        _SelUnit = objX.ToString().Trim();
                    else
                        _SelUnit = "";

                    objX = grdr.Cells[colnQCStatus.Name].Value;
                    if (objX != null)
                        _SelQCStatus = objX.ToString().Trim();
                    else
                        _SelQCStatus = "";

                    bIsFirst = false;
                }
                else
                {
                    _SelBNo = _SelBNo + ","+grdr.Cells[colcBNo.Name].Value.ToString().Trim() + "";
                    _SelItem = _SelItem+ ","+ grdr.Cells[colnItem.Name].Value.ToString().Trim() + "";
                    //
                    objX = grdr.Cells[colcBatchNo.Name].Value;
                    if (objX != null)
                        _SelBatchNo += ","+ objX.ToString().Trim();
                    else
                        _SelBatchNo += "," + "";

                    objX = grdr.Cells[colcUnit.Name].Value;
                    if (objX != null)
                        _SelUnit += "," + objX.ToString().Trim();
                    else
                        _SelUnit += "," + "";

                    objX = grdr.Cells[colnQCStatus.Name].Value;
                    if (objX != null)
                        _SelQCStatus += "," + objX.ToString().Trim();
                    else
                        _SelQCStatus += "," + "";
                }
            }
            if (_SelBNo.Trim() == "")
            {
                if (MessageBox.Show("û��ѡ�����ݣ���Ҫ�˳���", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("��ȷ����ѡ����ǣ�" + _SelBNo + " ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }
            _IsSelected = true;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _IsSelected = false;
            Close();
        }

        private void grdData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOK_Click(null, null);
        }
    }
}

