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

        #region 私有变量

        #endregion

        #region 公共属性

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
                    fntX = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
                    fntX = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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

        #region 私有方法

        /// <summary>
        /// 选择出入库作业物料数据
        /// </summary>
        /// <param name="nBClass">单据类别(1入库2出库3盘点)</param>
        /// <param name="sBNo">单号</param>
        /// <param name="nItem">单明细序号</param>
        /// <param name="sMNo">物料号</param>
        /// <param name="sMName">物料名称</param>
        /// <param name="sSpec">规格型号</param>
        /// <param name="sMatStyle">物料款式</param>
        /// <param name="sMatQCLevel">物料质等</param>
        /// <param name="sMatOther">其他物料属性</param>
        /// <param name="sRemark">物料备注</param>
        /// <param name="sABC">物料ABC属性</param>
        /// <param name="fQty">数量</param>
        /// <param name="fWeight">物料单重</param>
        /// <param name="sUnit">计量单位</param>
        /// <param name="sCSId">供应商编号</param>
        /// <param name="sSupplier">供应商名</param>
        /// <param name="sBatchNo">物料批号</param>
        /// <param name="sBNoIn">库存单号</param>
        /// <param name="nItemIn">库存单序号</param>
        /// <param name="bDoOK">是否OK</param>
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
                MessageBox.Show("对不起，必须先选择物料，才能选择该物料的库存入库单数据！");
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
                fntX = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
                fntX = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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

            #region 检查完整性

            //检测是否录入全
            if (txt_cMNo.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料编码不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_cMNo.Focus();
                return;
            }

            if (this.txt_fQty.Text.Trim() == "")
            {
                MessageBox.Show("对不起，物料数量不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_fQty.Focus();
                return;
            }
            if (!IsNumberic(txt_fQty.Text.Trim()))
            {
                MessageBox.Show("对不起，物料数量为非法数值！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_fQty.SelectAll();
                txt_fQty.Focus();
                return;
            }
            if (txt_cUnit.Text.Trim() == "")
            {
                MessageBox.Show("对不起，单位不能为空！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

