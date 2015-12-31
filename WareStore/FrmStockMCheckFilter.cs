using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using Zqm.Text;
using SunEast.App;

namespace WareStoreMS
{
    public partial class FrmStockMCheckFilter : UI.FrmSTable
    {
        public FrmStockMCheckFilter()
        {
            InitializeComponent();
        }

        #region 私有方法

        private void doSelectMatInfo(string sMNo, string sMName, string sSpec, string sMatStyle, string sMatQCLevel, string sMatOther,
          string sRemark, string sABC, double fSafeQtyDn, double fSafeQtyUp, double fQtyBox, double fWeight, string sTypeId1, string sType1,
          string sTypeId2, string sType2, string sUnit, int nKeepDay, string sCSId, string sSupplier, int _nMatClass, bool bIsSelectOK)
        {
            if (bIsSelectOK)
            {
                string sX = txt_Mat.Text.Trim();
                if (sX.Trim() == "")
                {
                    txt_Mat.Text = sMName.Trim();
                    txt_Mat.Tag = sMNo;
                }
                else
                {
                    txt_Mat.Text = txt_Mat.Text + "、"+sMName.Trim();
                    txt_Mat.Tag = txt_Mat.Tag.ToString()+"," + sMNo;
                }
            }
        }
        #endregion

        private bool GetIsLinkErp()
        {
            bool bOK = false;
            string sSql = "select isnull(max(cParValue),'0') cParValue from TPS_SysPar where  cParId='nIsLinkMis' ";
            string sErr = "";
            object objX = null;
            PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "cParValue", out objX, out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
            }
            bOK = objX != null && int.Parse(objX.ToString()) > 0;
            return bOK;
        }

        private void LoadStockList(string StockId)
        {
            string errStr = "";
            string sqlStr = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (StockId.Trim() != "")
            {
                sqlStr += " where cWHId='" + StockId + "'";
            }
            if (UserInformation.UType != UserType.utSupervisor)
            {
                sqlStr += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            DataSet ds = PubDBCommFuns.GetDataBySql(sqlStr, out errStr); //UserManager.GetDataSetbySql(sql);
            cmb_WHId.DataSource = ds.Tables["data"];
            cmb_WHId.DisplayMember = "cName";
            cmb_WHId.ValueMember = "cWHId";
        }

 
        private void LoadBaseItemList()
        {
            LoadStockList("");
        }

        private void FrmStockMCheckFilter_Load(object sender, EventArgs e)
        {
            //控制是否显示 连接ERP的操作
            bool bIsErp = false;
            bIsErp = GetIsLinkErp();
            lbl_Erp.Visible = bIsErp;
            cmd_ErpNo.Visible = bIsErp;
            btnRefresh.Visible = bIsErp;
            LoadBaseItemList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sErr = "";
            string sWHId = "";
            string sErpChkId = cmd_ErpNo.Text.Trim();
            string sChkType = "";
            string sChkTypeDesc = "";
            string sMType = "";
            string sMTypeDesc = "";
            string sMNo = "";
            string sMNoDesc = "";
            string sPos = "";
            string sDateFrom = "";
            string sDateTo = "";
            if(cmb_WHId.Items.Count > 0 && cmb_WHId.SelectedValue !=null)
                sWHId = cmb_WHId.SelectedValue.ToString();
            if (sWHId == "")
            {
                MessageBox.Show("对不起，仓库部能为空！");
                cmb_WHId.SelectAll();
                cmb_WHId.Focus();
            }
            if (rbtnAll.Checked)
            {
                sChkType = "301";
                sChkTypeDesc = "全盘";
            }
            else
            {
                sChkType = "302";
                sChkTypeDesc = "抽盘";
            }
            if (txt_ItemType.Tag != null)
                sMType = txt_ItemType.Tag.ToString();
            sMTypeDesc = txt_ItemType.Text.Trim();
            //if (sMTypeDesc.Trim().Length > 20)
            //{
            //    sMTypeDesc = sMType;
            //}
            if (txt_Mat.Tag != null)
                sMNo = txt_Mat.Tag.ToString().Trim();
            sMNoDesc = txt_Mat.Text.Trim();
            //if (sMNoDesc.Trim().Length > 20)
            //{
            //    sMNoDesc = sMNo;
            //}
            sPos = txt_Pos.Text.Trim();
            if (chk_Date.Checked)
            {
                if (dtpFrom.Value.Date > dtpTo.Value.Date)
                {
                    MessageBox.Show("对不起，开始日期不能大于截止日期！");
                    dtpFrom.Focus();
                    return;
                }
                sDateFrom = dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00");
                sDateTo = dtpTo.Value.ToString("yyyy-MM-dd 23:59:59");
            }
            Cursor.Current = Cursors.WaitCursor;
            string sX = PubDBCommFuns.sp_Chk_DoChkBuilder(AppInformation.SvrSocket, UserInformation.UserName, UserInformation.UnitId, sChkType, sChkTypeDesc, sErpChkId, sWHId, sMType, sMTypeDesc, sMNo, sMNoDesc, sPos, sDateFrom, sDateTo, out sErr);
            Cursor.Current = Cursors.Default;
            if (sX != "B")
            {
                MessageBox.Show("生成盘点单数据时出错：" + sErr);
            }
            else
            {
                MessageBox.Show("成功生成盘点单：" + sErr);
                Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void comboBox_cPosId_DragDrop(object sender, DragEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbpChkPart_Click(object sender, EventArgs e)
        {

        }

        private void btn_SelItemType_Click(object sender, EventArgs e)
        {
            DataSet dsX = null;
            string sErr = "";
            string sSql = "";
            string sDateFld = "";
            Button btnX = (Button)sender;
            
            sSql = "select cTypeId,cTypeName from TPC_MaterialType where ctypemode=0";
            sDateFld = "";
            Cursor.Current = Cursors.WaitCursor;
            dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql, sDateFld, out sErr);
            Cursor.Current = Cursors.Default;
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            frmSelItemList frmX = new frmSelItemList();
            //frmX.FldDesc = "cTypeName";
            //frmX.FldKey = "cTypeId";
            DataTable tbX = dsX.Tables["data"];
            frmX.TableItem = tbX;
            frmX.TitleText = "物料类别选择";
            frmX.FldDesc = "cTypeName";
            frmX.FldKey = "cTypeId";           
            frmX.ShowDialog();
            if (frmX.IsSelected)
            {
                txt_ItemType.Text = frmX.SelectItemList;
                txt_ItemType.Tag = frmX.SelectKeyList;
            }
            frmX.Dispose();
            dsX.Clear();
        }

        private void btn_Mat_Click(object sender, EventArgs e)
        {
            txt_Mat.Text = "";
            txt_Mat.Tag = "";
            SunEast.App.WarehouseBase.SelectMaterialInfo(AppInformation, UserInformation, doSelectMatInfo);
        }

        private void btn_Pos_Click(object sender, EventArgs e)
        {
            FrmSelectCell frmX = new FrmSelectCell();
            frmX.AppInformation = AppInformation;
            frmX.UserInformation = UserInformation;
            frmX.cmb_cWHId.Tag = cmb_WHId.SelectedValue;
            frmX.IsMultiSelect = true;
            frmX.ShowDialog();
            if (frmX.BIsResult)
            {
                string[] selResult=frmX.SelResult.Split(',');
                if (txt_Pos.Text.Split(',').Length + selResult.Length <= 50)
                {
                    foreach (var item in selResult)
                    {
                        if (!txt_Pos.Text.Contains(item))
                        {
                            if (string.IsNullOrEmpty(txt_Pos.Text))
                                txt_Pos.Text = item;
                            else
                                txt_Pos.Text += string.Format(",{0}", item);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("同时选择的货位不能超过50个，如需盘点多盘物料请选择物料名称盘点。");
                }
            }
            frmX.Dispose();
        }

        private void chk_Date_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = chk_Date.Checked;
            dtpTo.Enabled = chk_Date.Checked;
        }

        private void rbtnDepart_CheckedChanged(object sender, EventArgs e)
        {
            grpDepart.Enabled = rbtnDepart.Checked;
        }

        private void btn_PosClear_Click(object sender, EventArgs e)
        {
            txt_Pos.Text = "";
        }
    }
}