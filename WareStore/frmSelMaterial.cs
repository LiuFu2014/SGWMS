using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WareStoreMS
{
    public  delegate void DoSelMaterialEvent (string sMNo,string sMName,string sSpec,string sMatStyle,string sMatQCLevel,string sMatOther,
            string sRemark,string sABC,double fSafeQtyDn,double fSafeQtyUp,double fQtyBox,double fWeight,string sTypeId1,string sType1,
            string sTypeId2,string sType2,string sUnit, int nKeepDay,string sCSId,string sSupplier,int _nMatClass, bool bIsSelectOK) ;
    public partial class frmSelMaterial : UI.FrmSTable
    {
        public frmSelMaterial()
        {
            InitializeComponent();
        }

        #region  公共属性

        private string _MName = "";
        public string MName
        {
            get { return _MName; }
            set
            {
                _MName = value;
                txt_cName.Text = _MName;
            }
        }

        private string _Supplier = "";
        public string Supplier
        {
            get { return _Supplier; }
            set
            {
                _Supplier = value;
                this.txt_cSupplier.Text = _Supplier;
            }
        } 

        private DoSelMaterialEvent _DoSelMatEvent = null;
        public DoSelMaterialEvent DoSelMatEvent
        {
            get { return _DoSelMatEvent; }
            set { _DoSelMatEvent = value; }
        }



        #endregion

        #region

        private string GetCondition()
        {
            StringBuilder sCon = new StringBuilder("");
            string sX = "";
            sX = txt_cName.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ((mat.cMNo like '%" + sX + "%') or (mat.cName like '%" + sX + "%')  or (isnull(mat.cWBJM,'~') like '%" + sX + "%')  or (isnull(mat.cPYJM,'~') like '%" + sX + "%'))");
            }
            sX = txt_cMatOther.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ( isnull(mat.cMatOther,'~') like '%"+ sX  +"%')");
            }
            sX = this.txt_cMatQCLevel.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ( isnull(mat.cMatQCLevel,'~') like '%" + sX + "%')");
            }
            sX = this.txt_cMatStyle.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ( isnull(mat.cMatStyle,'~') like '%" + sX + "%')");
            }
            sX = this.txt_cRemark.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ( isnull(mat.cRemark,'~') like '%" + sX + "%')");
            }
            sX = this.txt_cSpec.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ( isnull(mat.cSpec,'~') like '%" + sX + "%')");
            }
            sX = this.cmb_cABC.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ( isnull(mat.cABC,'~') like '%" + sX + "%')");
            }
            sX = this.cmb_cTypeId1.Text.Trim();
            if (sX != "" && cmb_cTypeId1.SelectedIndex > -1 && cmb_cTypeId1.SelectedValue != null)
            {
                sX = cmb_cTypeId1.SelectedValue.ToString();
                sCon.Append(" and ( isnull(mat.cTypeId1,'~') = '" + sX + "')");
            }
            //sX = this.cmb_cTypeId2.Text.Trim();
            //if (sX != "" && cmb_cTypeId2.SelectedIndex > -1 && cmb_cTypeId2.SelectedValue != null)
            //{
            //    sX = cmb_cTypeId2.SelectedValue.ToString();
            //    sCon.Append(" and ( isnull(mat.cTypeId2,'~') = '" + sX + "')");
            //}
            sX = this.txt_cSupplier.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and ((isnull(st.cDtlCSId,' ') like '%" + sX + "%') or (isnull(st.cDtlSupplier,' ') like '%" + sX + "%')  or (isnull(st.cDtlWBJM,'~') like '%" + sX + "%')  or (isnull(st.cDtlPYJM,'~') like '%" + sX + "%'))");
            }
            sX = this.txt_cDtlRemark.Text.Trim();
            if (sX != "")
            {
                sCon.Append(" and (isnull(st.cDtlRemark,st.cStoreRemark) like '%" + sX + "%') ");
            }
            return sCon.ToString();
        }

        private void LoadBaseItem()
        {
            string sErr = "";
            //物料属性分类
            string sSql = "select cTypeId,cTypeName from TPC_MaterialType where cTypeMode = 0 ";
            DataSet dsX = null;
            dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "MatType", 0, 0, "", out sErr);
            if (sErr.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
            }
            if (dsX != null)
            {
                DataTable tbX = dsX.Tables["MatType"];
                cmb_cTypeId1.DisplayMember = "cTypeName";
                cmb_cTypeId1.ValueMember = "cTypeId";
                cmb_cTypeId1.DataSource = tbX;
                tbX.Clear();
            }
            dsX.Clear();
            sSql = "select cTypeId,cTypeName from TPC_MaterialType where cTypeMode = 1 ";
            dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, "ACNTType", 0, 0, "", out sErr);
            if (sErr.Trim() != "0" && sErr.Trim() != "")
            {
                MessageBox.Show(sErr);
            }
            if (dsX != null)
            {
                DataTable tbX = dsX.Tables["ACNTType"];
                //cmb_cTypeId2.DisplayMember = "cTypeName";
                //cmb_cTypeId2.ValueMember = "cTypeId";
                //cmb_cTypeId2.DataSource = tbX;
                tbX.Clear();
            }
            dsX.Clear();
        }

        #endregion

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_cMatOther.Text = "";
            txt_cMatQCLevel.Text = "";
            txt_cMatStyle.Text = "";
            txt_cName.Text = "";
            txt_cRemark.Text = "";
            txt_cSpec.Text = "";
            cmb_cABC.SelectedIndex = -1;
            cmb_cTypeId1.SelectedIndex = -1;
            //cmb_cTypeId2.SelectedIndex = -1;
            txt_cSupplier.Text = "";
            txt_cDtlRemark.Text = "";
            txt_cName.Focus();
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            string sErr = "";
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select  mat.cMNo,mat.cName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark,");
            sSql.Append("mat.fQtyBox,mat.fSafeQtyDn,mat.fSafeQtyUp,mat.fWeight,mat.cUnit,mat.cTypeId1,mat.cTypeId2,isnull(mat.cABC,'C') cABC,");
            sSql.Append("mt.cTypeName cType1,atp.cTypeName cType2,sum(isnull(st.fQty,0)) fQty,mat.nKeepDay,isnull(st.cDtlSupplier,' ') cSupplier,isnull(st.cDtlCSId,' ') cCSId,isnull(mat.nMatClass,0) nMatClass,isnull(isnull(st.cDtlRemark,st.cStoreRemark),' ') cDtlRemark ");
            sSql.Append("	from TPC_Material mat ");
            sSql.Append("  left join V_MaterialMatType mt on mat.cTypeId1=mt.cTypeId ");
            sSql.Append("  left join V_MaterialAcntType atp on mat.cTypeId2=atp.cTypeId ");
            sSql.Append("  left join V_StoreItemList st on mat.cMNo=st.cMNo ");
            sSql.Append(" where 1=1 ");
            //--条件
            sSql.Append(GetCondition());
            sSql.Append("  group by mat.cMNo,mat.cName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark, ");
            sSql.Append("  mat.fQtyBox,mat.fSafeQtyDn,mat.fSafeQtyUp,mat.fWeight,mat.cUnit,mat.cTypeId1,mat.cTypeId2,mt.cTypeName,");
            sSql.Append(" atp.cTypeName,mat.cABC,mat.nKeepDay,isnull(st.cDtlSupplier,' '),isnull(st.cDtlCSId,' ') ,isnull(mat.nMatClass,0),isnull(isnull(st.cDtlRemark,st.cStoreRemark),' ')");
            DataSet dsX = null;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dsX = DBFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql.ToString(), "TPC_Material", 0, 0, "", out sErr);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message);
                return;
            }
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            DataTable tbX = dsX.Tables["TPC_Material"];
            bds_Data.DataSource = tbX;

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grd_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(bds_Data.Count ==0) return ;
            DataRowView drv = (DataRowView)  bds_Data.Current ;
            if(drv == null) return ;
            if (_DoSelMatEvent != null)
            {
                double fSafeQtyDn = 0;
                if (drv["fSafeQtyDn"] != null && drv["fSafeQtyDn"].ToString() != "")
                {
                    fSafeQtyDn = Convert.ToDouble(drv["fSafeQtyDn"]);
                }
                double fSafeQtyUp = 0;
                if (drv["fSafeQtyUp"] != null && drv["fSafeQtyUp"].ToString() != "")
                {
                    fSafeQtyUp = Convert.ToDouble(drv["fSafeQtyUp"]);
                }
                double fQtyBox = 0;
                if (drv["fQtyBox"] != null && drv["fQtyBox"].ToString() != "")
                {
                    fQtyBox = Convert.ToDouble(drv["fQtyBox"]);
                }
                double fWeight = 0;
                if (drv["fWeight"] != null && drv["fWeight"].ToString() != "")
                {
                    fWeight = Convert.ToDouble(drv["fWeight"]);
                }
                int nKeepDay = 0;
                if (drv["nKeepDay"] != null && drv["nKeepDay"].ToString() != "")
                {
                    nKeepDay = Convert.ToInt32(drv["nKeepDay"]);
                }
                int nMatClass = 0;
                if (drv["nMatClass"] != null && drv["nMatClass"].ToString().Trim() != "")
                {
                    nMatClass = Convert.ToInt16(drv["nMatClass"]);
                }
                try
                {
                    _DoSelMatEvent(drv["cMNo"].ToString(), drv["cName"].ToString(), drv["cSpec"].ToString(), drv["cMatStyle"].ToString(), drv["cMatQCLevel"].ToString(),
                        drv["cMatOther"].ToString(), drv["cRemark"].ToString(), drv["cABC"].ToString(), fSafeQtyDn, fSafeQtyUp,
                        fQtyBox, fWeight, drv["cTypeId1"].ToString(), drv["cType1"].ToString(), drv["cTypeId2"].ToString(),
                        drv["cType2"].ToString(), drv["cUnit"].ToString(), nKeepDay, drv["cCSId"].ToString(),drv["cSupplier"].ToString(),nMatClass, true);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (bds_Data.Count == 0)
            {
                MessageBox.Show("无物料数据可选择！");
                return;
            }
            if (grd_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("没有选择物料数据！");
                return;
            }
            this.prgMain.Maximum = grd_Data.SelectedRows.Count;
            prgMain.Minimum = 0;
            prgMain.Value = 0;
            prgMain.Visible = true;
            foreach (DataGridViewRow grdv in grd_Data.SelectedRows)
            {
                if (_DoSelMatEvent != null)
                {
                    #region
                    double fSafeQtyDn = 0;
                    if (grdv.Cells["col_fSafeQtyDn"].Value != null && grdv.Cells["col_fSafeQtyDn"].Value.ToString() != "")
                    {
                        fSafeQtyDn = Convert.ToDouble(grdv.Cells["col_fSafeQtyDn"].Value);
                    }
                    double fSafeQtyUp = 0;
                    if (grdv.Cells["col_fSafeQtyUp"].Value != null && grdv.Cells["col_fSafeQtyUp"].Value.ToString() != "")
                    {
                        fSafeQtyUp = Convert.ToDouble(grdv.Cells["col_fSafeQtyUp"].Value);
                    }
                    double fQtyBox = 0;                    
                    if (grdv.Cells["col_fQtyBox"].Value != null && grdv.Cells["col_fQtyBox"].Value.ToString() != "")
                    {
                        fQtyBox = Convert.ToDouble(grdv.Cells["col_fQtyBox"].Value);
                    }
                    double fWeight = 0;
                    if (grdv.Cells["col_fWeight"].Value != null && grdv.Cells["col_fWeight"].Value.ToString() != "")
                    {
                        fWeight = Convert.ToDouble(grdv.Cells["col_fWeight"].Value);
                    }
                    int nKeepDay = 0;                    
                    if (grdv.Cells["col_nKeepDay"].Value != null && grdv.Cells["col_nKeepDay"].Value.ToString() != "")
                    {
                        nKeepDay = Convert.ToInt32(grdv.Cells["col_nKeepDay"].Value);
                    }
                    int nMatClass = 0;
                    if (grdv.Cells["col_nMatClass"].Value != null && grdv.Cells["col_nMatClass"].Value.ToString() != "")
                    {
                        nMatClass = Convert.ToInt16(grdv.Cells["col_nMatClass"].Value);
                    }
                    try
                    {
                        _DoSelMatEvent(grdv.Cells["col_cMNo"].Value.ToString(), grdv.Cells["col_cName"].Value.ToString(), grdv.Cells["col_cSpec"].Value.ToString(),
                            grdv.Cells["col_cMatStyle"].Value.ToString(), grdv.Cells["col_cMatQCLevel"].Value.ToString(),
                            grdv.Cells["col_cMatOther"].Value.ToString(), grdv.Cells["col_cRemark"].Value.ToString(), grdv.Cells["col_cABC"].Value.ToString(), fSafeQtyDn, fSafeQtyUp,
                            fQtyBox, fWeight, grdv.Cells["col_cTypeId1"].Value.ToString(), grdv.Cells["col_cType1"].Value.ToString(), grdv.Cells["col_cTypeId2"].Value.ToString(),
                            grdv.Cells["col_cType2"].Value.ToString(), grdv.Cells["col_cUnit"].Value.ToString(), nKeepDay,grdv.Cells["col_cCSId"].Value.ToString(), grdv.Cells["col_cSupplier"].Value.ToString(),nMatClass, true);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                    prgMain.Value ++;
                    #endregion
                }
            }
            Close();
        }

        private void frmSelMaterial_Load(object sender, EventArgs e)
        {
            LoadBaseItem();
            txt_cName.Focus();
        }

    
    }
}

