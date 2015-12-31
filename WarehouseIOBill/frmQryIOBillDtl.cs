using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SunEast.App
{
    public partial class frmQryIOBillDtl : UI.FrmSTable
    {
        #region 私有变量
        /*
        select * from V_IOBillDetail where nBClass,cBTypeId,cDept,cPayer,cBillRemark,dDate,cBNo,
            cMNo,cMName,cSpec,cBatchNo,bIsChcked,bIsFinished,cDtlRemark,cLinkId
        */
        private string sSqlStr = "select * from V_IOBillDetail ";
        #endregion

        #region 属性
            private bool _IsInBill = true;
            public bool IsInBill
            {
                get { return _IsInBill; }
                set
                {
                    _IsInBill = value;
                    if (_IsInBill)
                    {
                        Text = "入库单物料查询";
                        lbl_Dept.Text = "供货单位";
                        col_cDept.HeaderText = lbl_Dept.Text;
                    }
                    else
                    {
                        Text = "出库单物料查询";
                        lbl_Dept.Text = "领料单位";
                        col_cDept.HeaderText = lbl_Dept.Text;
                    }
                }
            }
        #endregion

        #region 私有方法
        private void LoadBaseItem()
        {
            #region
            string sBClass = "";
            if (_IsInBill)
            {
                sBClass = "1";
            }
            else
            {
                sBClass = "2";
            }
            string sSql = "select cBTypeId,cBType,nBClass from tpb_billtype where nBClass="+ sBClass +
                "  and bUsed=1  order by nSort,cBTypeId";
            #endregion
            DataSet dsX = null;
            string sErr = "";
            #region 单据类型
            dsX  = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "btype", 0, 0, "", out sErr);
            if (sErr.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbBType = dsX.Tables["btype"].Copy();
            if (tbBType != null)
            {
                cmb_BillType.DisplayMember = "cBType";
                cmb_BillType.ValueMember = "cBTypeId";
                cmb_BillType.DataSource = tbBType;
                return;
            }
            #endregion

            #region 供货部门
            if (_IsInBill)
            {
                sSql = "select cCSId,cCSNameJ from TPB_CuSupplier where nType=0 and bUsed=1";
            }
            else
            {
                sSql = "select cCSId,cCSNameJ from TPB_CuSupplier where nType=1 and bUsed=1";
            }
            dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "TPB_CuSupplier", 0, 0, "", out sErr);
            if (sErr.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbCuSupplier = dsX.Tables["TPB_CuSupplier"].Copy();
            if (tbCuSupplier != null)
            {
                cmb_Dept.DisplayMember = "cCSNameJ";
                cmb_Dept.ValueMember = "cCSNameJ";
                cmb_Dept.DataSource = tbCuSupplier;
                return;
            }
            #endregion
        }
        #endregion  
        #region 方法
            private string GetCondition(out string sDesc)
        {
            sDesc = "";
            int nBClass = 1;
            if (!_IsInBill)
            {
                nBClass = 2;
            }
            StringBuilder sbDesc = new StringBuilder( "单据日期：" + dtp_From.Value.ToString("yyyy-MM-dd") + "——" + dtp_To.Value.ToString("yyyy-MM-dd") );
            StringBuilder sbCondition = new StringBuilder(" where nBClass = " + nBClass.ToString() + " and (dDate >= '" + dtp_From.Value.ToString("yyyy-MM-dd 00:00:00") + "' and  dDate <= '" + dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") + "' )");
            #region
            if (cmb_BillType.Text.Trim() != "" && cmb_BillType.SelectedValue != null)
            {
                sbCondition.Append(" and ( cBTypeId='"+ cmb_BillType.SelectedValue.ToString() +"' )");
                sbDesc.Append("  单据类型：" + cmb_BillType.Text);
            }

            if (this.cmb_CheckStatus.Text.Trim() != "" && cmb_CheckStatus.SelectedIndex > -1 && cmb_CheckStatus.SelectedIndex < 2)
            {
                sbCondition.Append(" and ( isnull(bIsChecked,0) =" + cmb_CheckStatus.SelectedIndex.ToString() + " )");
                sbDesc.Append("  审核状态：" + cmb_CheckStatus.Text.Trim());
            }
            if (this.cmb_Dept.Text.Trim() != "" && cmb_Dept.SelectedValue != null)
            {
                sbCondition.Append(" and ( cDept='" + cmb_Dept.SelectedValue.ToString() + "' )");
                sbDesc.Append("  "+ lbl_Dept.Text + "：" + cmb_Dept.SelectedValue.ToString());

            }
            if (this.cmb_FinishedStatus.Text.Trim() != "" && cmb_FinishedStatus.SelectedIndex > -1 && cmb_FinishedStatus.SelectedIndex < 2)
            {
                sbCondition.Append(" and ( isnull(bIsFinished,0) = " + cmb_FinishedStatus.SelectedIndex.ToString() + " )");
                sbDesc.Append("  完成状态：" + cmb_FinishedStatus.Text);
            }
            #endregion

            #region
            if (txt_BillNo.Text.Trim() != "")
            {
                sbCondition.Append(" and (cBNo like '%"+ txt_BillNo.Text.Trim() +"%')");
                sbDesc.Append("  单号：" + txt_BillNo.Text.Trim());
            }
            if (txt_cLinkId.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cLinkId,'') like '%" + txt_cLinkId.Text.Trim() + "%')");
                sbDesc.Append("  关联单号：" + txt_cLinkId.Text.Trim());
            }
            if (this.txt_BillNoFrom.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cFromNo,'') like '%" + txt_BillNoFrom.Text.Trim() + "%')");
                sbDesc.Append("  来源单号：" + txt_BillNoFrom.Text.Trim());
            }
            if (this.txt_Payer.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cPayer,'') like '%" + txt_Payer.Text.Trim() + "%')");
                sbDesc.Append("  制单人：" + txt_Payer.Text.Trim());
            }
            if (this.txt_BillRemark.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cBillRemark,'') like '%" + txt_BillRemark.Text.Trim() + "%')");
                sbDesc.Append("  单据备注：" + txt_BillRemark.Text.Trim());
            }
            #endregion
            #region
            if (this.txt_MNo.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cMNo,'') like '%" + txt_MNo.Text.Trim() + "%' or isnull(cMName,'') like '%"+ txt_MNo.Text.Trim() +
                    "%' or isnull(cWBJM,'') like '%"+ txt_MNo.Text.Trim() +"%' or isnull(cPYJM,'') like '"+ txt_MNo.Text.Trim() +"')");
                sbDesc.Append("  物料：" + txt_MNo.Text.Trim());
            }
            if (this.txt_Spec.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cSpec,'') like '%" + txt_Spec.Text.Trim() + "%')");
                sbDesc.Append("  物料规格：" + txt_Spec.Text.Trim());
            }
            if (this.txt_Style.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cMatStyle,'') like '%" + txt_Style.Text.Trim() + "%')");
                sbDesc.Append("  物料款式：" + txt_Style.Text.Trim());
            }
            if (this.txt_BatchNo.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cBatchNo,'') like '%" + txt_BatchNo.Text.Trim() + "%')");
                sbDesc.Append("  物料批号：" + txt_BatchNo.Text.Trim());
            }
            if (this.txt_DtlRemark.Text.Trim() != "")
            {
                sbCondition.Append(" and (isnull(cDtlRemark,'') like '%" + txt_DtlRemark.Text.Trim() + "%')");
                sbDesc.Append("  明细备注：" + txt_DtlRemark.Text.Trim());
            }
            #endregion
            sDesc = sbDesc.ToString();
            sbDesc.Remove(0, sbDesc.Length);
            return sbCondition.ToString();
        }
        #endregion
        public frmQryIOBillDtl()
        {
            InitializeComponent();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            dtp_From.Value = DateTime.Now.AddDays(-7);
            dtp_To.Value = DateTime.Now;
            cmb_BillType.SelectedIndex = -1;
            cmb_CheckStatus.SelectedIndex = -1;
            cmb_Dept.SelectedIndex = -1;
            cmb_FinishedStatus.SelectedIndex = -1;
            txt_BatchNo.Text = "";
            txt_BillNo.Text = "";
            txt_BillNoFrom.Text = "";
            txt_BillRemark.Text = "";
            txt_cLinkId.Text = "";
            txt_DtlRemark.Text = "";
            txt_MNo.Text = "";
            txt_Payer.Text = "";
            txt_Spec.Text = "";
            txt_Style.Text = "";
            dtp_From.Focus();
        }

        private void frmQryIOBillDtl_Load(object sender, EventArgs e)
        {
            btn_Reset_Click(null, null);
            grd_List.AutoGenerateColumns = false;
            LoadBaseItem();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            if (dtp_From.Value > dtp_To.Value)
            {
                MessageBox.Show("对不起，开始时间不能大于截止时间！");
                dtp_From.Focus();
                return;
            }
            string sDesc = "";
            string sErr = "";
            string sSql = sSqlStr + GetCondition(out sDesc);
            DataSet dsX = null;
            try
            {
                dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "ioDetail", 0, 0, "dDate", out sErr);
            }
            catch( Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (dsX == null) return;
            DataTable tbX = dsX.Tables["ioDetail"];
            bds_List.DataSource = tbX;
            grd_List.DataSource = bds_List;
            lbl_Count.Text = bds_List.Count.ToString();
        }

      
    }
}

