using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using WareStore.Rpts;
using SunEast.App;

namespace WareStore
{
    public partial class frmRptInOutRece : UI.FrmSTable
    {
        public frmRptInOutRece()
        {
            InitializeComponent();
        }

        private App.WMSAppInfo objApp;
        private App.WMSUserInfo objUser;
        string timeStatus = "";
        string timeEnd = "";
        string receType = "";
        string maillerStr = "";
        string WHId = "";
        string WHName = "";
        string userId = "";
        string userName = "";
        private DataTable mydt = new DataTable();
        private DataTable mydtAll = new DataTable();
        private void frmInOutRece_Load(object sender, EventArgs e)
        {
            LoadBillClass();
            objApp = AppInformation;
            objUser = UserInformation;
            BindData();

         
            this.dgvInOutRece.AutoGenerateColumns = false;
            this.dgvRillInfo.AutoGenerateColumns = false;
            this.cmbBillType.SelectedIndex = 0;

            RefeashData();
        }

        private void BindData()
        {
            this.dtpStatus.Value = DateTime.Now.AddDays(-1);
            this.dtpEnd.Value = DateTime.Now;

            BindDataWHInfo();
            bindDataUserList();
        }
        private void BindDataWHInfo()
        {
            string strSql = "select cwhid,cName from TWC_WareHouse where bUsed=1";
            if (objUser.UType != CommBase.UserType.utSupervisor)
            {
                strSql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + objUser.UserId.Trim() + "')";
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
        private void bindDataUserList()
        {
            // 只能显示同一个部门的所有成员            
            string strSql = string.Format("select cName,cUserId  from TPB_User where bUsed=1 ", objUser.DeptId);
            string err="";
            if (objUser.UType == CommBase.UserType.utAdmin)
            {
                strSql += string.Format(" and cDeptid='{0}' ",objUser.DeptId);
            }
            else if (objUser.UType == CommBase.UserType.utNormal)            
            {
                strSql += string.Format(" and cUserId='{0}' ", objUser.UserId);
            }
            DataSet dsUser = PubDBCommFuns.GetDataBySql(strSql, out err);
            if (err == "" || err == "0")
            {
                this.cmbUserId.DisplayMember = "cName";
                this.cmbUserId.ValueMember = "cUserId";
                this.cmbUserId.DataSource = dsUser.Tables["data"];
            }
        }

        private void LoadBillClass()
        {
            ArrayList ArrBillClass = new ArrayList();
            ArrBillClass.Add(new DictionaryEntry("0", "全部单据"));
            ArrBillClass.Add(new DictionaryEntry("1", "入库单"));
            ArrBillClass.Add(new DictionaryEntry("2", "出库单"));
            ArrBillClass.Add(new DictionaryEntry("4", "盘点调整单"));
            //
            this.cmbBillType.DataSource = ArrBillClass;
            cmbBillType.DisplayMember = "Value";
            cmbBillType.ValueMember = "Key";
        }

        private DataTable GetData()
        {
            /*
             select his.*,mat.cname,mat.cspec,mat.cunit from 
            (
            select a.cmno,sum(fqty) fqty
            from twb_stockdtl_his a inner join twb_billin b 
            on a.cbno=b.cbno
            where dintime >= '2012-04-01 00:00:00' and dintime <= '2012-04-01 23:59:59'
            and b.cpayer='茹小红'
            and a.cwhid='a'
            --and a.nbclass=2
            group by a.cmno) his inner join tpc_material mat on his.cmno=mat.cmno
            where mat.cname like '%%'
             */
            GetUIValue();
            string sql = string.Format("select his.*,mat.cname,mat.cspec,mat.cunit from (select a.cmno,sum(fqty) fqty from twb_stockdtl_his a inner join twb_billin b on a.cbno=b.cbno where dintime >= '{0}' and dintime <= '{1}' ", timeStatus, timeEnd);
            if (userName != "")
            {
                sql += string.Format("  and b.cpayer='{0}' ", userName);
            }
            else
            {
                //找到这个组的
                sql += string.Format("  and b.cpayer in (select CNAME from TPB_USER where CDEPTID ='{0}') ", UserInformation.DeptId);
            }
            if (WHId != "")
            {
                sql += string.Format("  and a.cwhid='{0}' ", WHId);
            }           
            if (receType != "0")
            {
                sql += string.Format(" and a.nBClass={0} ",receType);
            }

            sql += string.Format(" group by a.cmno) his inner join tpc_material mat on his.cmno=mat.cmno where 0=0 ");

            if (maillerStr != "")
            {
                sql += string.Format(" and (mat.cname like '%{0}%' or mat.cMNo like '%{1}%' or mat.cPYJM like '%{2}%' or mat.cWBJM like '%{3}%') ", maillerStr, maillerStr, maillerStr, maillerStr);
            }

            string err = "";

            DataSet dsM = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dintime", out err);
            if (err != "" && err != "0")
            {
                MyTools.MessageBox(err);
                return new DataTable();

            }
            return dsM.Tables["data"];
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            RefeashData();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
            RefeashData();
        }
        
        private void RefeashData()
        {
            if (this.dtpStatus.Value > this.dtpEnd.Value)
            {
                MyTools.MessageBox("查询的开始日期不能大于结束日期！");
                this.dtpStatus.Focus();
                return;
            }
             mydt = GetData();
            DisReceSun();
            this.dgvInOutRece.DataSource = mydt;
 
        }
        private void DisReceSun()
        {
            double snm = 0;
            int count = mydt.Rows.Count;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    snm += Convert.ToDouble(mydt.Rows[i]["fqty"].ToString());
                }
            }
            catch
            { }
            this.lblReceCount.Text = count+"";
            this.lblSumNum.Text = snm + "";

        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {

            btnPrintSum_Click(null,null);
        }

        private void btn_M_Help_Click(object sender, EventArgs e)
        {
            //MyTools.MessageBox("导出");
            if (this.dgvInOutRece.Rows.Count == 0)
            {
                MyTools.MessageBox("当前没有数据可以进行导入！");
            }
            else
            {
                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    string sFile = dlgSave.FileName;
                    DataImpExp.DataIE.DataGridViewToExcel(this.dgvInOutRece, sFile, Text);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtMNo.Text = "";
            this.cmbUserId.SelectedIndex = -1;
            this.cmbUserId.Text = "";
            this.cmbWHId.SelectedIndex = -1;
            this.cmbWHId.Text = "";
        }

        private void btnFindInfo_Click(object sender, EventArgs e)
        {
            RefeashData();
        }

        private void dgvInOutRece_SelectionChanged(object sender, EventArgs e)
        {
            //MyTools.MessageBox(this.dgvInOutRece.SelectedRows[0].Index.ToString());
            //this.dgvInOutRece.SelectedRows
            if (this.dgvInOutRece.SelectedRows.Count == 0)
            { 
                this.dgvRillInfo.DataSource = null;
                return;
            }
            string matId = "";
            if (this.dgvInOutRece.SelectedRows[0].Cells["cMNo"].Value != null)
            {
                matId = this.dgvInOutRece.SelectedRows[0].Cells["cMNo"].Value.ToString();
                loadReceInfo(matId);
            }
            
        }

   

        private void GetUIValue()
        {
            timeStatus = this.dtpStatus.Value.ToString("yyyy-MM-dd 00:00:00");
            timeEnd = this.dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59");
            receType = "0";
            //0为所有的单据，1为入库的单据，2为出库的单据
            if (cmbBillType.Text.Trim() != "" && cmbBillType.SelectedValue != null && cmbBillType.SelectedValue.ToString() != "0")
            {
                receType = this.cmbBillType.SelectedValue.ToString();
            }
            
            maillerStr = this.txtMNo.Text.Trim();
            WHId = "";
            if (this.cmbWHId.SelectedIndex != -1)
            {
                WHId = this.cmbWHId.SelectedValue.ToString();
            }

            WHName = this.cmbWHId.Text.ToString();
            userId = "";
            if (this.cmbUserId.SelectedIndex != -1)
            {
                userId = this.cmbUserId.SelectedValue.ToString();
            }
            userName = this.cmbUserId.Text.ToString();
        }

        private void loadReceInfo(string matId)
        {
            loadReceInfo(matId, false);
        }
        private void loadReceInfo(string matId, bool isAllRece)
        {

            /*
             * select a.nPalletId,a.cBoxId,a.cMNo,a.cWhId,a.dInTime,a.fQty,a.cBNo,a.cReMark,a.cUnit,
            cBTypeNew=case a.nBClass when 1 then '入库单据' when 2 then '出库单据' else '' end
            from twb_stockdtl_his a inner join twb_billin b 
            on a.cbno=b.cbno
            where dintime >= '2012-04-01 00:00:00' and dintime <= '2012-04-01 23:59:59'
            and b.cpayer='茹小红'
            and a.cwhid='a'
            --and a.nbclass=2
             and a.cmno='1.01.004'
             */
            GetUIValue();
            string sql = string.Format("select a.nPalletId,a.cBoxId,a.cMNo,a.cWhId,a.dInTime,a.fQty,a.cBNo,a.cReMark,a.cUnit,b.cpayer,case a.nBClass when 1 then '入库单据' when 2 then '出库单据' else '' end cBTypeNew from twb_stockdtl_his a inner join twb_billin b on a.cbno=b.cbno where dintime >= '{0}' and dintime <= '{1}' ", timeStatus, timeEnd);
            if (userName != "")
            {
                sql += string.Format("  and b.cpayer='{0}' ", userName);
            }
            if (WHId != "")
            {
                sql += string.Format("  and a.cwhid='{0}' ", WHId);
            }
            if (receType != "0")
            {
                sql += string.Format(" and a.nBClass={0} ", receType);
            }
            if (!isAllRece)
            {
                sql += string.Format(" and a.cmno='{0}' ", matId);
            }

            string err = "";

            DataSet dsM = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sql, "data", 0, 0, "dintime", out err);

            if (err != "" && err != "0")
            {
                MyTools.MessageBox(err);
                return;
            }
            mydtAll = dsM.Tables["data"];

            if (!isAllRece)
            {
                this.dgvRillInfo.DataSource = mydtAll;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadReceInfo("", true);//取得所有单据的明细记录
            FrmRptShow.dsRpt = new DataSet();
            FrmRptShow.dsRpt.Tables.Add(mydtAll.Copy());
            FrmRptShow.dsRpt.Tables[0].TableName = "InOutReceAll";
            FrmRptShow.CountType = "1";
            FrmRptShow.Paramets = new string[5];
            FrmRptShow.Paramets[0] = timeStatus;
            FrmRptShow.Paramets[1] = timeEnd;
            FrmRptShow.Paramets[2] = userName;
            FrmRptShow.Paramets[3] = WHName;
            FrmRptShow.Paramets[4] = maillerStr;
            if (receType == "1")
            {
                FrmRptShow.rpsTitleStr = "入库物料汇总报表";
            }
            else if (receType == "2")
            {
                FrmRptShow.rpsTitleStr = "出库物料汇总报表";
            }
            else
            {
                FrmRptShow.rpsTitleStr = "出 / 入库物料汇总报表";
            }
            FrmRptShow fsdtl = new FrmRptShow();
            fsdtl.Text = "出入库物料明细汇总";
            fsdtl.ShowDialog();
        }

        private void btnPrintSum_Click(object sender, EventArgs e)
        {
            //MyTools.MessageBox("打印");
            FrmRptShow.dsRpt = new DataSet();
            FrmRptShow.dsRpt.Tables.Add(mydt.Copy());
            FrmRptShow.dsRpt.Tables[0].TableName = "InOutRece";
            FrmRptShow.CountType = "0";
            FrmRptShow.Paramets = new string[5];
            FrmRptShow.Paramets[0] = timeStatus;
            FrmRptShow.Paramets[1] = timeEnd;
            FrmRptShow.Paramets[2] = userName;
            FrmRptShow.Paramets[3] = WHName;
            FrmRptShow.Paramets[4] = maillerStr;
            if (receType == "1")
            {
                FrmRptShow.rpsTitleStr = "入库物料汇总报表";
            }
            else if (receType == "2")
            {
                FrmRptShow.rpsTitleStr = "出库物料汇总报表";
            }
            else
            {
                FrmRptShow.rpsTitleStr = "出 / 入库物料汇总报表";
            }
            FrmRptShow fsdtl = new FrmRptShow();
            fsdtl.Text = "出入库物料汇总";
            fsdtl.ShowDialog();
        }
    }
}