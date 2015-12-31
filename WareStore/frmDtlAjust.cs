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
   
    public partial class frmDtlAjust : UI.FrmSTable
    {
        public delegate bool DoEditItemInfo(DataRowView drvX);

        #region ˽�б���

        #endregion

        #region ��������

        private DataRowView drvItem = null;
        public DataRowView DrvItem
        {
            get { return (drvItem); }
            set { drvItem = value; }
        }

        private DoEditItemInfo doItem = null;
        public DoEditItemInfo DoItem
        {
            get { return (doItem); }
            set { doItem = value; }
        }

        private bool bIsNew = false;
        public bool BIsNew
        {
            get { return (bIsNew); }
            set
            {
                bIsNew = value;
                Font fntX = null;
                
                if (bIsNew)
                {
                    fntX = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbl_BillIn.ForeColor = Color.Blue;
                    lbl_BillIn.Font = fntX;
                    lbl_BillIn.Enabled = true;

                    this.lbl_MNo.ForeColor = Color.Blue;
                    lbl_MNo.Font = fntX;
                    lbl_MNo.Enabled = true;

                    this.lbl_PosId.ForeColor = Color.Blue;
                    lbl_PosId.Font = fntX;
                    lbl_PosId.Enabled = true;

                }
                else
                {
                    fntX = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lbl_BillIn.ForeColor = Color.Black;
                    lbl_BillIn.Font = fntX;
                    lbl_BillIn.Enabled = false ;

                    this.lbl_MNo.ForeColor = Color.Black;
                    lbl_MNo.Font = fntX;
                    lbl_MNo.Enabled = false;

                    this.lbl_PosId.ForeColor = Color.Black;
                    lbl_PosId.Font = fntX;
                    lbl_PosId.Enabled = false;
                }
            }
        }

        private string _WHId = "";
        public string WHId
        {
            get { return _WHId.Trim(); }
            set { _WHId = value.Trim(); }
        }

        private bool bIsResultOK = false;
        public bool BIsResult
        {
            get { return (bIsResultOK); }
            set { bIsResultOK = value; }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ѡ��������ҵ��������
        /// </summary>
        /// <param name="nBClass">�������(1���2����3�̵�)</param>
        /// <param name="sBNo">����</param>
        /// <param name="nItem">����ϸ���</param>
        /// <param name="sMNo">���Ϻ�</param>
        /// <param name="sMName">��������</param>
        /// <param name="sSpec">����ͺ�</param>
        /// <param name="sMatStyle">���Ͽ�ʽ</param>
        /// <param name="sMatQCLevel">�����ʵ�</param>
        /// <param name="sMatOther">������������</param>
        /// <param name="sRemark">���ϱ�ע</param>
        /// <param name="sABC">����ABC����</param>
        /// <param name="fQty">����</param>
        /// <param name="fWeight">���ϵ���</param>
        /// <param name="sUnit">������λ</param>
        /// <param name="sCSId">��Ӧ�̱��</param>
        /// <param name="sSupplier">��Ӧ����</param>
        /// <param name="sBatchNo">��������</param>
        /// <param name="sBNoIn">��浥��</param>
        /// <param name="nItemIn">��浥���</param>
        /// <param name="bDoOK">�Ƿ�OK</param>
        public void doSelIOStoreMatBillData(int nBClass, string sBNo, int nItem, string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
                string sRemark, string sABC, double fQty, double fWeight, string sUnit, string sCSId, string sSupplier, string sBatchNo, string sBNoIn, int nItemIn,
                string sWHIdErp,string sAreaIdErp,string sPosIdErp ,out bool bDoOK)
        {
            bDoOK = false;
            if (nBClass == 1)
            {
                txt_cBatchNo.Text = sBatchNo;
                txt_cBNoIn.Text = sBNoIn;
                txt_cSpec.Text = sSpec;
                txt_cUnit.Text = sUnit;
                txt_fQty.Text = fQty.ToString();                
                txt_nItemIn.Text = nItemIn.ToString();
                txt_cWHIdErp.Text = sWHIdErp.Trim();
                txt_cAreaIdErp.Text = sAreaIdErp.Trim();
                txt_cPosIdErp.Text = sPosIdErp.Trim();
            }
        }
        #endregion

        public frmDtlAjust()
        {
            InitializeComponent();
        }

        private void lblMNo_Click(object sender, EventArgs e)
        {
            //
            frmSelIOBillMat frmX = new frmSelIOBillMat();
            frmX.BClass = 1;
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.DoSelIOStoreMatBillData = doSelIOStoreMatBillData;
            frmX.ShowDialog();
            frmX.Dispose();
        }

        private void lblBillIn_Click(object sender, EventArgs e)
        {
            if (_WHId.Trim() == "" || txt_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("�Բ��𣬱�����ѡ�����ϣ�����ѡ������ϵĿ����ⵥ���ݣ�");
                return;
            }
            frmSelBillInDtl frmX = new frmSelBillInDtl();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.lbl_cWHId.Text = _WHId.Trim();
            frmX.lbl_cMNo.Text = txt_cMNo.Text.Trim();
            frmX.ShowDialog();
            if (frmX.IsSelected)
            {
                txt_cBatchNo.Text = frmX.SelBatchNo.Trim();
                txt_cBNoIn.Text = frmX.SelBNo.Trim();
                txt_nItemIn.Text = frmX.SelItem.Trim();
                txt_cUnit.Text = frmX.SelUnit.Trim();
                txt_fQty.SelectAll();
                txt_fQty.Focus();
            }
            frmX.Dispose();
        }

        private void lbl_PosId_Click(object sender, EventArgs e)
        {
            FrmSelectCell frmX = new FrmSelectCell();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.cmb_cWHId.Tag = _WHId.Trim();
            frmX.IsMultiSelect = false;
            frmX.ShowDialog();
            if (frmX.BIsResult)
            {
                txt_cPosId.Text = frmX.SelResult;
                txt_nPalletId.Text = frmX.SelPalletId;
            }
            frmX.Dispose();
        }

        private void frmDtlAjust_Load(object sender, EventArgs e)
        {
            if (drvItem != null)
            {
                DataRowViewToUI(drvItem, grdEdit);
            }
            Font fntX = null;
            if (bIsNew)
            {
                fntX = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl_BillIn.ForeColor = Color.Blue;
                lbl_BillIn.Font = fntX;
                lbl_BillIn.Enabled = true;

                this.lbl_MNo.ForeColor = Color.Blue;
                lbl_MNo.Font = fntX;
                lbl_MNo.Enabled = true;

                this.lbl_PosId.ForeColor = Color.Blue;
                lbl_PosId.Font = fntX;
                lbl_PosId.Enabled = true;

            }
            else
            {
                fntX = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl_BillIn.ForeColor = Color.Black;
                lbl_BillIn.Font = fntX;
                lbl_BillIn.Enabled = false;

                this.lbl_MNo.ForeColor = Color.Black;
                lbl_MNo.Font = fntX;
                lbl_MNo.Enabled = false;

                this.lbl_PosId.ForeColor = Color.Black;
                lbl_PosId.Font = fntX;
                lbl_PosId.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strBId = "";

            #region ���������

            //����Ƿ�¼��ȫ
            if (txt_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("�Բ������ϱ��벻��Ϊ�գ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_cMNo.Focus();
                return;
            }

            if (this.txt_fQty.Text.Trim() == "")
            {
                MessageBox.Show("�Բ���������������Ϊ�գ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_fQty.Focus();
                return;
            }
            if (!IsNumberic(txt_fQty.Text.Trim()))
            {
                MessageBox.Show("�Բ�����������Ϊ�Ƿ���ֵ��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_fQty.SelectAll();
                txt_fQty.Focus();
                return;
            }
            if (txt_cUnit.Text.Trim() == "")
            {
                MessageBox.Show("�Բ��𣬵�λ����Ϊ�գ�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_cUnit.SelectAll();
                txt_cUnit.Focus();
                return;
            }
            #endregion
            strBId = drvItem["cBNo"].ToString();
            UIToDataRowView(drvItem,grdEdit);
            string sErr = "";
            string sX = DBFuns.sp_Chk_WriteAjustDtl(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, "WMS", strBId, txt_cWHId.Text.Trim(),
                        txt_cPosId.Text.Trim(), txt_nPalletId.Text.Trim(), txt_cBoxId.Text.Trim(), txt_cMNo.Text.Trim(), double.Parse(txt_fQty.Text.Trim()),
                        txt_cBNoIn.Text.Trim(), int.Parse(txt_nItemIn.Text.Trim()), "",txt_cWHIdErp.Text.Trim(),txt_cAreaIdErp.Text.Trim(),txt_cPosIdErp.Text.Trim(), out sErr);
            if (sX.Trim() != "" && sX.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
            }
            else
            {
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

