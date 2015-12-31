using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SunEast.App;

namespace WareStore
{
    public partial class frmSlackMatCount : UI.FrmSTable
    {
        #region 私有变量
        DataTable tbMain = null;
        private bool bIsMainOpened = false  ;
        #endregion

        #region 私有方法
        private void LoadWareHouseList()
        {
            string strSql = "select cwhid,cName from TWC_WareHouse where bUsed=1";
            if (UserInformation.UType != CommBase.UserType.utSupervisor)
            {
                strSql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            string err = "";
            DataSet dsUser = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err == "" || err == "0")
            {
                this.cmbWHId.DisplayMember = "cName";
                this.cmbWHId.ValueMember = "cwhid";
                this.cmbWHId.DataSource = dsUser.Tables["data"];
            }
        }

        #endregion
        public frmSlackMatCount()
        {
            InitializeComponent();
        }


        private void btnFindInfo_Click(object sender, EventArgs e)
        {
            
            string sWHId = "";
            string sErr = "";
            if(dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("开始日期不能大于截止日期！");
                dtpFrom.Focus();
                return ;
            }
            if (cmbWHId.Text.Trim() != "" && cmbWHId.SelectedValue != null)
            {
                sWHId = cmbWHId.SelectedValue.ToString();
            }
            tbMain = WareStoreMS.DBFuns.sp_GetSlackMatCount(AppInformation.SvrSocket, sWHId, dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00"), dtpTo.Value.ToString("yyyy-MM-dd 23:59:59"), out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                MessageBox.Show(sErr);
                return;
            }
            if (tbMain != null)
            {
                bIsMainOpened = false;
                bdsMain.DataSource = tbMain;
                grdMain.DataSource = bdsMain;
                lblReceCount.Text = bdsMain.Count.ToString();
                bIsMainOpened = true;
            }
        }

        private void btnPrintSum_Click(object sender, EventArgs e)
        {
            if (tbMain == null || bdsMain.Count == 0)
            {
                MessageBox.Show("无数据记录记录！");
                return;
            }
            Rpts.frmRptViewer frmX = new Rpts.frmRptViewer();
            frmX.RptTitle = "呆滞物料汇总报表";
            Rpts.rptSlackMatCount rptX = new Rpts.rptSlackMatCount();
            rptX.SetDataSource(tbMain);
            frmX.RptObj = rptX;
            frmX.SetReport();
            frmX.ShowDialog();
            frmX.Dispose();
            rptX.Dispose();
        }

        private void frmSlackMatCount_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now.AddYears(-10);
            dtpTo.Value = DateTime.Now.AddMonths(-6);
            //
            LoadWareHouseList();
            grdMain.AutoGenerateColumns = false;
            grdDtl.AutoGenerateColumns = false;
        }

        private void bdsMain_PositionChanged(object sender, EventArgs e)
        {
            if (!bIsMainOpened) return;
            DataRowView drv = null;
            if (bdsMain.Count == 0) return;
            string sErr = "";
            drv = (DataRowView)bdsMain.Current;
            if (drv != null)
            {
                string sMNo = drv["cMNo"].ToString();
                string sWHId = "";
                if (cmbWHId.Text.Trim() != "" && cmbWHId.SelectedValue != null)
                {
                    sWHId = cmbWHId.SelectedValue.ToString();
                }
                string sDateFrom = dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00");
                string sDateTo = dtpTo.Value.ToString("yyyy-MM-dd 23:59:59");

                DataTable tbDtl = null;
                tbDtl = WareStoreMS.DBFuns.sp_GetSlackMatDtl(AppInformation.SvrSocket, sWHId, sMNo, sDateFrom,sDateTo, out sErr);
                if (sErr.Trim() != "" && sErr.Trim() != "0")
                {
                    MessageBox.Show(sErr);
                    return;
                }
                grdDtl.DataSource = tbDtl;
            }
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            if (this.bdsMain.Count == 0)
            {
                MyTools.MessageBox("当前没有数据可以进行导入！");
            }
            else
            {
                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    string sFile = dlgSave.FileName;
                    DataImpExp.DataIE.DataGridViewToExcel(this.grdMain, sFile, Text);
                }
            }
        }
    }
}

