using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SunEast.App
{
    public partial class frmInnerAccont : UI.FrmSTable
    {
        string sBNo;
        int nItem;
        double fqty;
        double fpallet;
        double ffinished;
        double taskFinNum;

        object objValue;
        string sErr;
        string sSql;

        public frmInnerAccont(string _sBNo,int _nItem)
        {
            InitializeComponent();
            sBNo = _sBNo;
            nItem = _nItem;
        }

        private void frmInnerAccont_Load(object sender, EventArgs e)
        {
            DoRefreshQty();
            DoRefreshHasPallet();
        }

        private void DoRefreshQty()
        {
            sSql = "select fqty,fpallet,ffinished from TWB_BillInDtl where cBNo='" + sBNo + "' and nItem=" + nItem.ToString();
            DataSet ds = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "TWB_BillInDtl", 0, 0, "", out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
            }
            else if (ds == null)
            {
                MessageBox.Show("对不起，打开单据表数据失败！");
            }
            else
            {
                DataRow dr = ds.Tables["TWB_BillInDtl"].Rows[0];
                fqty = Convert.ToDouble(dr["fqty"]);
                lbfqty.Text = fqty.ToString();
                string fpalletStr = dr["fpallet"].ToString();
                fpalletStr = fpalletStr == "" ? "0" : fpalletStr;
                fpallet = Convert.ToDouble(fpalletStr);
                lbfpallet.Text = fpallet.ToString();
                ffinished = Convert.ToDouble(dr["ffinished"]);
                lbffinished.Text = ffinished.ToString();
            }
            sSql = "select sum(fQty) fQty from TWB_WorkTaskDtl where  cBNo='" + sBNo + "' and nItem=" + nItem.ToString()
                             + " and  nWorkId in (select nWorkId from TWB_WorkTask where nWKStatus in (99,109))";

            if (DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "fQty", out objValue, out sErr))
            {
                if (objValue != null && objValue.ToString() != "" && (sErr.Trim() == "" || sErr.Trim() == "0"))
                {
                    taskFinNum = Convert.ToDouble(objValue);
                    lbtaskFinNum.Text = taskFinNum.ToString();
                }
            }
        }

        private bool isBillOK()
        {
            string sSql = string.Format("select nbillstatus from TWB_BillIn where cBNo='{0}'", sBNo);
            if (DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "nbillstatus", out objValue, out sErr))
            {
                if (objValue != null && objValue.ToString() != "" && (sErr.Trim() == "" || sErr.Trim() == "0"))
                {
                    if (Convert.ToInt32(objValue) == 1)
                        return true;
                }
            }
            return false;
        }

        private void btn_Dtl_InnerAccont_Click(object sender, EventArgs e)
        {
            if (isBillOK())
            {
                sSql = "select count(0) num from TWB_WorkTaskDtl where  cBNo='" + sBNo + "' and nItem=" + nItem.ToString() +
                            " and  nWorkId in (select nWorkId from TWB_WorkTask where nWKStatus <99)";
                if (DBFuns.GetValueBySql(AppInformation.SvrSocket, sSql, "", "num", out objValue, out sErr))
                {
                    if (objValue != null && objValue.ToString() != "" && (sErr.Trim() == "" || sErr.Trim() == "0"))
                    {
                        if (Convert.ToInt32(objValue) == 0)
                        {
                            if (MessageBox.Show("确定整单无差异登记吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                int count = 0;
                                int badcount = 0;
                                foreach (DataGridViewRow item in dataGridView_HasPallet.Rows)
                                {
                                    int pWorkId = Convert.ToInt32(item.Cells["nWorkId"].Value);
                                    if (PubDBCommFuns.sp_DoAccont(base.AppInformation.SvrSocket, pWorkId, 1, "WMS", out sErr) == "0")
                                        count++;
                                    else
                                        badcount++;
                                }
                                DoRefreshQty();
                                DoRefreshHasPallet();
                                MessageBox.Show("成功登记了 " + count.ToString() + " 条任务,失败" + badcount.ToString() + " 条任务！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("对不起，该订单下还有没有上架完成的任务！");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("对不起，还没有收到PSCS发过来的收货入库单！");
            }
        }

        private void DoRefreshHasPallet()
        {
            string sErr = "";
            StringBuilder builder = new StringBuilder("");
            builder.Append("select * from V_WorkTaskDtl where nBClass = 1 and cBNo='" + sBNo + "'");
            builder.Append(" order by nWorkId desc ");
            Cursor.Current = Cursors.WaitCursor;
            DataSet dsHsPallet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, builder.ToString(), "V_WorkTaskDtl", 0, 0, "", out sErr);
            Cursor.Current = Cursors.Default;
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
            }
            else if (dsHsPallet == null)
            {
                MessageBox.Show("对不起，打开已配盘数据失败！");
            }
            else
            {
                this.dataGridView_HasPallet.AutoGenerateColumns = false;
                DataTable table = dsHsPallet.Tables["V_WorkTaskDtl"];
                this.bindingSource_HasPallet.DataSource = table;
                this.dataGridView_HasPallet.DataSource = this.bindingSource_HasPallet;
            }
        }

        private void dataGridView_HasPallet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (dataGridView_HasPallet.Rows[e.RowIndex].Cells["ColBad"].Value.ToString() == "待登记")
                    {
                        //if (MessageBox.Show("确定登记吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        //{
                            DataRowView item = (DataRowView)bindingSource_HasPallet.Current;
                            int pWorkId = Convert.ToInt32(item["nWorkId"]);
                            double sqty = Convert.ToDouble(item["fqty"]);
                            UI.frmInputMessage frmX = new UI.frmInputMessage();
                            frmX.InputValueType = UI.InputMsgType.ittReal;
                            frmX.PromptText = "请输入合格数量：";
                            frmX.TitleText = "录入合格数量";
                            frmX.DefaultValue = sqty.ToString();
                            frmX.ShowDialog();
                            if (frmX.ResultIsOK)
                            {
                                double sQtyNew = Convert.ToDouble(frmX.ResultValue.Trim());
                                if (sQtyNew > 0)
                                {
                                    if (sQtyNew <= sqty)
                                    {
                                        sSql = string.Format("update TWB_WorkTaskDtl set fqty={0} where cBNo='{1}' and nItem='{2}' and nWorkId={3} and fqty>={0}", sQtyNew, sBNo, nItem, pWorkId);
                                        if (PubDBCommFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, "", out sErr))
                                        {
                                            if (PubDBCommFuns.sp_DoAccont(base.AppInformation.SvrSocket, pWorkId, 1, "WMS", out sErr) == "0")
                                            {
                                                DoRefreshQty();
                                                DoRefreshHasPallet();
                                                MessageBox.Show("成功登记了1条任务！");
                                            }
                                            else
                                                MessageBox.Show(sErr);
                                        }
                                    }
                                    else
                                        MessageBox.Show("合格数量不能大于总数！");
                                }
                                else
                                    MessageBox.Show("合格数量必需大于0！");
                            }
                    }
                    else
                        MessageBox.Show("该任务状态不是待登记！");
                }
            }
        }

        private void dataGridView_HasPallet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int cWKStatus = Convert.ToInt32(row.Cells["nWKStatus"].Value);
                if (cWKStatus == 99 || cWKStatus == 109)
                {
                    row.Cells["ColBad"].Value = "待登记";
                }
                else if (cWKStatus < 99)
                {
                    row.Cells["ColBad"].Value = "未上架";
                }
                else if (cWKStatus == 110)
                {
                    row.Cells["ColBad"].Value = "已登记";
                }
                else
                {
                    row.Cells["ColBad"].Value = "未知";
                }
                object ob = (row.Cells[0] as DataGridViewButtonCell);
            }
            
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            DoRefreshQty();
            DoRefreshHasPallet();
        }

        /// <summary>
        /// 出库查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Check_Click(object sender, EventArgs e)
        {
            if (dataGridView_HasPallet.DataSource==null)
            {
                MessageBox.Show("没有可用的数据。");
            }
            else
            {
                if (dataGridView_HasPallet.SelectedRows.Count==0)
                {
                    MessageBox.Show("没有选定数据，请先选定数据在进行操作。");
                }
                else
                {
                    //组合，按逗号分隔
                    string workNums = "";
                    foreach (DataGridViewRow dr in dataGridView_HasPallet.SelectedRows)
                    {
                        workNums += dr.Cells["nWorkId"].Value.ToString().Trim();
                        workNums += ",";
                    }
                    //移除最后一个逗号
                    workNums= workNums.TrimEnd(',');
                    try
                    {
                        //执行存储过程
                        DataTable dt = PubDBCommFuns.SP_SG_OutCheck(base.AppInformation.SvrSocket, workNums);
                        if (dt == null)
                        {
                            MessageBox.Show("执行失败。");
                        }
                        else
                        {
                            if (dt.Rows[0][0].ToString() == "-1")
                            {
                                MessageBox.Show("执行出错，详情：" + dt.Rows[0][1].ToString());
                            }
                            else
                            {
                                MessageBox.Show("执行成功。");
                            }

                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("内部异常，可能已经失去网络连接。");
                    }
                }
            }
        }

    }
}
