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
    public partial class frmChkDtlWrite : UI.FrmSTable
    {
        #region  ˽�б���
        private bool bIsNew = false;

        #endregion

        #region ��������

        private bool _IsNewAddMat = false;
        public bool IsNewAddMat
        {
            get { return _IsNewAddMat; }
            set
            {
                _IsNewAddMat = value;
                if (_IsNewAddMat)
                {
                    btn_AddMat.Visible = true;
                    btn_SelBNoIn.Visible = true;
                    _MNo = "";
                    _MName = "";
                    _Spec = "";
                    _Unit = "";
                    _BatchNo = "";
                    _BNoIn = "����ʼ��";
                    _ItemIn = 0;
                    _Qty = 0;
                    _BadQty = 0;
                    _Diff = 0;
                    _BoxId = "";
                    txt_cBatchNo.Enabled = true;
                    txt_cBNoIn.Enabled = true;
                    txt_cBoxId.Enabled = true;
                    txt_nItemIn.Enabled = true;                  
                    frmChkDtlWrite_Load(null, null);
                }
            }
        }

        private string _CheckNo = "";
        public string CheckNo
        {
            get { return _CheckNo.Trim(); }
            set { _CheckNo = value.Trim(); }
        }

        private string _WHId = "";
        public string WHId
        {
            get { return _WHId.Trim(); }
            set 
            { _WHId = value.Trim();
            txt_cWHId.Text = _WHId;
            }
        }

        private string _PosId = "";
        public string PosId
        {
            set { _PosId = value.Trim(); }
        }

        private string _PalletId = "";
        public string PalletId
        {
            set { _PalletId = value.Trim(); }
        }

        private string _BoxId = "";
        public string BoxId
        {
            set { _BoxId = value.Trim(); }
        }

        private string _MNo = "";
        public string MNo
        {
            set { _MNo = value.Trim(); }
        }
        
        private string _MName = "";
        public string MName
        {
            set { _MName = value.Trim(); }
        }

        private string _Spec = "";
        public string Spec
        {
            set { _Spec = value.Trim(); }
        }

        private string _BatchNo = "";
        public string BatchNo
        {
            set { _BatchNo = value.Trim(); }
        }

        private int _QCStatus = 0;
        public int QCStatus
        {
            set { _QCStatus = value; }
        }

        private string _Unit = "";
        public string Unit
        {
            set { _Unit = value.Trim(); }
        }

        private string _BNoIn = "";
        public string BNoIn
        {
            set { _BNoIn = value.Trim(); }
        }

        private int _ItemIn = 0;
        public int ItemIn
        {
            set { _ItemIn = value; }
        }

        private double _Qty = 0;
        public double Qty
        {
            set { _Qty = value; }
        }

        private bool _IsOK = false;
        public bool IsOK
        {
            get { return _IsOK; }
            set { _IsOK = value; }
        }

        private double _Diff = 0;
        public double Diff
        {
            get
            {
                if (txt_fDiff.Text.Trim() != "")
                {
                    if (IsNumberic(txt_fDiff.Text.Trim()))
                    {
                        _Diff = double.Parse(txt_fDiff.Text.Trim());
                    }
                }
                return _Diff;
            }
            set 
            { 
                _Diff = value;
                txt_fDiff.Text = _Diff.ToString();
            }
        }

        private double _BadQty = 0;
        public double BadQty
        {
            get
            {
                if (this.txt_fBad.Text.Trim() != "")
                {
                    if (IsNumberic(txt_fBad.Text.Trim()))
                    {
                        _BadQty = double.Parse(txt_fBad.Text.Trim());
                    }
                }
                return _BadQty;
            }
            set
            {
                _BadQty = value;
                txt_fBad.Text = _BadQty.ToString();
            }
        }

        private string _WHIdErp = "";
        public string WHIdErp
        {
            get
            {
                _WHIdErp = txt_WHIdErp.Text.Trim();
                return _WHIdErp;
            }
            set
            {
                _WHIdErp = value;
                txt_WHIdErp.Text = _WHIdErp;
            }
        }

        private string _AreaIdErp = "";
        public string AreaId
        {
            get
            {
                _AreaIdErp = txt_AreaIdErp.Text.Trim();
                return _AreaIdErp;
            }
            set
            {
                _AreaIdErp = value;
                txt_AreaIdErp.Text = _AreaIdErp;
            }
        }

        private string _PosIdErp = "";
        public string PosIdErp
        {
            get
            {
                _PosIdErp = txt_PosIdErp.Text.Trim();
                return _PosIdErp;
            }
            set
            {
                _PosIdErp = value;
                txt_PosIdErp.Text = _PosIdErp;
            }
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
                string sWHIdErp,string sAreaIdErp,string sPosIdErp, out bool bDoOK)
        {
            bDoOK = false;
            if (txt_cMNo.Text.Trim() != "")
            {
                if (txt_cMNo.Text.Trim() != sMNo.Trim())
                {
                    MessageBox.Show("���ϱ��벻һ�£�");
                    return;
                }
            }
            if (nBClass == 1)
            {
                txt_cBatchNo.Text = sBatchNo;
                txt_cBNoIn.Text = sBNoIn;
                txt_cSpec.Text = sSpec;
                txt_cUnit.Text = sUnit;
                this.txt_RQty.Text = fQty.ToString();
                txt_nItemIn.Text = nItemIn.ToString();
                txt_WHIdErp.Text = sWHIdErp.Trim();
                txt_AreaIdErp.Text = sAreaIdErp.Trim();
                txt_PosIdErp.Text = sPosIdErp.Trim();
                txt_RQty.SelectAll();
                txt_RQty.Focus();
            }
        }

        private void doSelMaterial(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
             string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1,
             string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId, string sSupplier, int _nMatClass, bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                txt_cMNo.Text = sMNo;
                txt_cUnit.Text = sUnit;
                txt_cSpec.Text = sSpec;
            }
        }

        #endregion

        #region ��������

        #endregion

        public frmChkDtlWrite()
        {
            InitializeComponent();
        }

        

        private void frmChkDtlWrite_Load(object sender, EventArgs e)
        {
            txt_cBatchNo.Text = _BatchNo.Trim();
            txt_cBNoIn.Text = _BNoIn.Trim();
            txt_cBoxId.Text = _BoxId.Trim();
            txt_cMNo.Text = _MNo.Trim();
            txt_nPalletId.Text = _PalletId.Trim();
            txt_cPosId.Text = _PosId.Trim();
            txt_cSpec.Text = _Spec.Trim();
            txt_cUnit.Text = _Unit.Trim();
            txt_cWHId.Text = _WHId.Trim();
            txt_nItemIn.Text = _ItemIn.ToString();
            txt_Qty.Text = _Qty.ToString();
            txt_RQty.Text = txt_Qty.Text;
            txt_fDiff.Text = "0";

        }

        private void txt_fDiff_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sErr = "";
            double fQty = 0;
            double fRQty = 0;
            double fDiff = 0;
            double fBad = 0;
            if (txt_Qty.Text.Trim() != "")
            {
                fQty = double.Parse(txt_Qty.Text.Trim());
            }
            if (txt_RQty.Text.Trim() != "")
            {
                try
                {
                    fRQty = double.Parse(txt_RQty.Text.Trim());
                }
                catch (Exception err)
                {
                    MessageBox.Show("ʵ����¼��������ֵ�Ƿ���");
                    txt_RQty.SelectAll();
                    txt_RQty.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("�Բ���ʵ��������Ϊ�գ�");
                txt_RQty.Focus();
                return;
            }
            if (txt_fBad.Text.Trim() != "")
            {
                try
                {
                    fBad = double.Parse(txt_fBad.Text.Trim());
                }
                catch (Exception err)
                {
                    MessageBox.Show("����Ʒ��¼��������ֵ�Ƿ���");
                    txt_fBad.SelectAll();
                    txt_fBad.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("�Բ��𣬲���Ʒ������Ϊ�գ�");
                txt_fBad.Focus();
                return;
            }
            if (fBad < 0)
            {
                MessageBox.Show("�Բ�����Ʒ������Ϊ������");
                txt_fBad.SelectAll();
                txt_fBad.Focus();
                return;
            }
            if (txt_fDiff.Text.Trim() == "")
            {
                MessageBox.Show("�Բ�������������Ϊ�գ�");
                txt_fDiff.Focus();
                return;
            }
            else
            {
                if (IsNumberic(txt_fDiff.Text.Trim()))
                {
                    fDiff = double.Parse(txt_fDiff.Text.Trim());
                }
                else
                {
                    MessageBox.Show("������¼��������ֵ�Ƿ���");
                    txt_fDiff.SelectAll();
                    txt_fDiff.Focus();
                    return;
                }
            }
            if (fRQty !=(fQty + fDiff))
            {
                MessageBox.Show("�Բ���ʵ���� �����ڡ� ������ + ������");
                txt_fDiff.SelectAll();
                txt_fDiff.Focus();
                return;
            }
            _WHIdErp = txt_WHIdErp.Text.Trim();
            _AreaIdErp = txt_AreaIdErp.Text.Trim();
            _PosIdErp = txt_PosIdErp.Text.Trim();
            string sX = DBFuns.sp_Chk_WriteChkDtl(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, "WMS", txt_cWHId.Text.Trim(), txt_cPosId.Text.Trim(),
                txt_nPalletId.Text.Trim(), txt_cBoxId.Text.Trim(), txt_cMNo.Text.Trim(), fDiff ,fBad, txt_cUnit.Text.Trim(), txt_cBNoIn.Text.Trim(), int.Parse(txt_nItemIn.Text.Trim()),
                _CheckNo.Trim(),_WHIdErp,_AreaIdErp,_PosIdErp, out sErr);
            if (sX.Trim() != "0" && sX.Trim() != "B" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
            }
            else
            {
                MessageBox.Show("�Ǽǳɹ���");
                _IsOK = true;
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _IsOK = false;
            Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //
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
            }
            frmX.Dispose();
        }

        private void txt_RTotal_TextChanged(object sender, EventArgs e)
        {
            double fQty = 0;
            double fRTotal = 0;
            double fBad = 0;
            double fDiff = 0;
            double fRQty = 0;
            if (txt_RTotal.Text.Trim() != "" && IsNumberic(txt_RTotal.Text.Trim()))
            {
                fRTotal = double.Parse(txt_RTotal.Text.Trim());
            }
            else if (txt_RTotal.Text.Trim() != "" && (!IsNumberic(txt_RTotal.Text.Trim())))
            {
                MessageBox.Show("��¼��Ϸ���ֵ��");
                txt_RTotal.SelectAll();
                txt_RTotal.Focus();
                return;
            }
            if (txt_fBad.Text.Trim() != "" && IsNumberic(txt_fBad.Text.Trim()))
            {
                fBad = double.Parse(txt_fBad.Text.Trim());
            }
            else if (txt_fBad.Text.Trim() != "" && (!IsNumberic(txt_fBad.Text.Trim())))
            {
                MessageBox.Show("��¼��Ϸ���ֵ��");
                txt_fBad.SelectAll();
                txt_fBad.Focus();
                return;
            }
            if (txt_Qty.Text.Trim() == "")
            {
                MessageBox.Show("�Բ�������������Ϊ�գ�");
                return;
            }
            fQty = double.Parse(txt_Qty.Text.Trim());
            fDiff = fRTotal - fQty - fBad;
            fRQty = fRTotal - fBad;
            txt_fDiff.Text = fDiff.ToString();
            txt_RQty.Text = fRQty.ToString();
        }

        private void btn_SelBNoIn_Click(object sender, EventArgs e)
        {
            frmSelIOBillMat frmX = new frmSelIOBillMat();
            frmX.BClass = 1;
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.txt_cName.Text = txt_cMNo.Text;
            frmX.DoSelIOStoreMatBillData = doSelIOStoreMatBillData;
            frmX.ShowDialog();
            frmX.Dispose();
        }

        private void btn_AddMat_Click(object sender, EventArgs e)
        {
            frmSelMaterial frmX = new frmSelMaterial();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.DoSelMatEvent = doSelMaterial ;
            frmX.ShowDialog();
            frmX.Dispose();
        }

    }
}

