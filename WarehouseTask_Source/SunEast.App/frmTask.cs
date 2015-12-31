using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UI;
using WareHouseTask.Rpts;
using CommBase;
using WarehouseTask;
namespace SunEast.App
{


    public class frmTask : FrmSTable
    {
        private TaskType _WorkTaskType = TaskType.ttTaskAll;
        private BindingSource bdsMain;
        private Button btn_DelTskDtlForOver;
        private Button btn_DoTaskCmd;
        private ToolStripButton btn_M_Help;
        private Button btn_Print;
        private Button btn_Qry;
        private Button btn_Reset;
        private Button btn_UpdateDsn;
        private Button btn_UpdateOptStation;
        private Button btnDel;
        private Button btnDel2;
        private Button btnExit;
        private Button btnReDoTask;
        private Button btnRefresh;
        private Button btnTransfer;
        private ComboBox cmb_OptType;
        private ComboBox cmb_Status;
        private ComboBox cmb_User;
        private ComboBox cmb_WHId;
        private IContainer components = null;
        private DateTimePicker dtp_From;
        private DateTimePicker dtp_To;
        public DataGridView grdList;
        private GroupBox grpCondition;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblReceSum;
        private ToolStripMenuItem mi_DelAccountTaskDtl;
        private ToolStripMenuItem mi_pp_DoAccount;
        private ToolStripMenuItem mi_pp_DoTaskCmd;
        private ToolStripMenuItem mi_pp_UndoTaskCmd;
        private ToolStripMenuItem mi_pp_UnPallet;
        private ToolStripMenuItem mi_pp_UpdateOptStation;
        private ToolStripMenuItem mi_pp_UpdateTaskPri;
        private OperateType optMain = OperateType.optNone;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private ContextMenuStrip ppMain;
        private StringBuilder sbConndition = new StringBuilder("");
        private string strKeyFld = "nWorkId";
        private string strTbNameMain = "V_WorkTaskDtl";
        public ToolStripButton tlb_DelTskDtlForOver;
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_DoTask;
        public ToolStripButton tlb_M_Edit;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Undo;
        public ToolStripButton tlb_UpdateDsn;
        public ToolStripButton tlb_UpdateOptStation;
        public ToolStrip tlbMain;
        private ToolStripButton tlbSaveSysRts;
        public ToolStripButton toolStripButton1;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private ToolTip toolTip;
        private TextBox txt_BNo;
        private TextBox txt_MNo;
        private TextBox txt_PltId;
        private DataGridViewTextBoxColumn colnWorkId;
        private DataGridViewTextBoxColumn col_noptStation;
        private DataGridViewTextBoxColumn colnPalletId;
        private DataGridViewTextBoxColumn colcOptTypeDesc;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn colfQty;
        private DataGridViewTextBoxColumn colcUnit;
        private DataGridViewTextBoxColumn colcWKStatusDesc;
        private DataGridViewTextBoxColumn colcPosIdFrom;
        private DataGridViewTextBoxColumn colcPosIdTo;
        private DataGridViewTextBoxColumn colcUser;
        private DataGridViewTextBoxColumn colcMNo;
        private DataGridViewTextBoxColumn coldOptDate;
        private DataGridViewTextBoxColumn colcBNo;
        private DataGridViewTextBoxColumn colnWKStatus;
        private DataGridViewTextBoxColumn Column3;
        private TextBox txt_WkId;

        public frmTask()
        {
            this.InitializeComponent();
        }

        public void BindDataSetToCtrls()
        {
            this.grdList.DataSource = null;
            this.grdList.DataSource = this.bdsMain;
        }

        private void btn_DelTskDtlForOver_Click(object sender, EventArgs e)
        {
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("无任务数据可删除！");
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current == null)
                {
                    MessageBox.Show("无任务数据可删除！");
                }
                else if (int.Parse(current["nWKStatus"].ToString()) < 110)
                {
                    MessageBox.Show("对不起，该任务没有过账完成！");
                }
                else
                {
                    string pPosId = "";
                    if (current["cPosIdTo"].ToString().Trim() != "")
                    {
                        pPosId = current["cPosIdTo"].ToString().Trim();
                    }
                    else
                    {
                        pPosId = current["cPosIdFrom"].ToString().Trim();
                    }
                    string sErr = "";
                    Cursor.Current = Cursors.WaitCursor;
                    string str3 = PubDBCommFuns.sp_Pack_DelWKTskDtlForOver(base.AppInformation.SvrSocket, current["cBNo"].ToString().Trim(), int.Parse(current["nItem"].ToString()), current["cMNo"].ToString().Trim(), int.Parse(current["nWorkId"].ToString()), current["nPalletId"].ToString().Trim(), current["cBoxId"].ToString().Trim(), pPosId, base.UserInformation.UserName, base.UserInformation.UnitId, "WMS", out sErr);
                    Cursor.Current = Cursors.Default;
                    if (str3 == "0")
                    {
                        MessageBox.Show("删除成功！");
                    }
                    else
                    {
                        MessageBox.Show(sErr);
                    }
                }
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            this.tlb_Print_Click(sender, e);
        }

        private void btn_Qry_Click(object sender, EventArgs e)
        {
            this.OpenMainDataSet();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.ReSetConition();
            this.cmb_WHId.SelectAll();
            this.cmb_WHId.Focus();
        }

        private void btn_UpdateDsn_Click(object sender, EventArgs e)
        {
            this.tlb_UpdateDsn_Click(sender, e);
        }

        private void btn_UpdateOptStation_Click(object sender, EventArgs e)
        {
            bool resultIsOK = false;
            int num = 1;
            int num2 = 0;
            string sErr = "";
            frmInputMessage message = new frmInputMessage {
                InputValueType = InputMsgType.ittInteger,
                TitleText = "录入操作台号",
                PromptText = "请录入操作台号："
            };
            message.ShowDialog();
            resultIsOK = message.ResultIsOK;
            if (resultIsOK)
            {
                num = int.Parse(message.ResultValue.Trim());
            }
            if (resultIsOK)
            {
                foreach (DataGridViewRow row in this.grdList.SelectedRows)
                {
                    int num3 = 0;
                    int num4 = 0;
                    object obj2 = row.Cells["colnWKStatus"].Value;
                    if (obj2 != null)
                    {
                        num3 = Convert.ToInt16(obj2);
                    }
                    obj2 = row.Cells["colnWorkId"].Value;
                    if (obj2 != null)
                    {
                        num4 = Convert.ToInt16(obj2);
                    }
                    if (num3 < 2)
                    {
                        string sSql = "update TWB_WorkTask set nOptStation= " + num.ToString() + " where nWorkId='" + num4.ToString() + "' and nWKStatus < 2 ";
                        if (DBFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, "", out sErr))
                        {
                            num2++;
                        }
                        else
                        {
                            MessageBox.Show(sErr);
                        }
                    }
                }
                MessageBox.Show("成功修改了 " + num2.ToString() + " 条任务！");
                this.btn_Qry_Click(null, null);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.DoMDelete();
        }

        private void btnDel2_Click(object sender, EventArgs e)
        {
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("对不起,无任务数据!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                try
                {
                    if (Convert.ToInt32(current["nWkStatus"]) > 0)
                    {
                        MessageBox.Show("对不起，该任务已下发执行，不能取消！");
                    }
                    else
                    {
                        string str = "-1";
                        string sErr = "";
                        str = PubDBCommFuns.sp_Pack_DelWKTskDtl(base.AppInformation.SvrSocket, int.Parse(current["nWorkId"].ToString()), current["cBNo"].ToString().Trim(), current["nItem"].ToString().Trim(), current["cBNoIn"].ToString().Trim(), current["nItemIn"].ToString().Trim(), current["cBoxId"].ToString().Trim(), "WMS", base.UserInformation.UnitId.Trim(), base.UserInformation.UserName.Trim(), out sErr);
                        if ((str == "") || (str == "-1"))
                        {
                            MessageBox.Show(sErr);
                        }
                        else
                        {
                            MessageBox.Show("取消已配盘指令成功！");
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnReDoTask_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView current = (DataRowView) this.bdsMain.Current;
                if (current != null)
                {
                    string sErr = "";
                    if (int.Parse(current["nWKStatus"].ToString()) == 0)
                    {
                        MessageBox.Show("对不起，该任务未被下发，不能重新下发！");
                    }
                    else
                    {
                        PubDBCommFuns.DoExecSql(base.AppInformation.SvrSocket, "update TWB_WorkTask set nWKStatus=1 where nWorkId=" + current["nWorkId"].ToString(), "", out sErr);
                        if ((sErr == "0") || (sErr.Trim() == ""))
                        {
                            MessageBox.Show("重新下发执行操作成功！");
                        }
                        else
                        {
                            MessageBox.Show(sErr);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.OpenMainDataSet();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("对不起,无任务数据!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int count = this.grdList.SelectedRows.Count;
                if (count == 0)
                {
                    MessageBox.Show("请选择需要操作的任务数据!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (MessageBox.Show("确定要对所选择的" + count.ToString() + " 条任务进行手动执行完毕吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
                {
                    try
                    {
                        string sErr = "";
                        count = 0;
                        foreach (DataGridViewRow row in this.grdList.SelectedRows)
                        {
                            if (Convert.ToInt32(row.Cells["colnWKStatus"].Value) < 99)
                            {
                                int pWorkId = Convert.ToInt32(row.Cells["colnWorkId"].Value);
                                //PubDBCommFuns.sp_DoAccont(base.AppInformation.SvrSocket, pWorkId, 1, "WMS", out sErr)
                                if (PubDBCommFuns.sp_ECS_UpdateWorkStatus(base.AppInformation.SvrSocket, pWorkId, 109, out sErr) == "1")
                                    count++;
                            }
                        }
                        MessageBox.Show("成功手动执行完毕 " + count.ToString() + " 条任务！");
                        this.btnRefresh_Click(null, null);
                        this.grdList.Focus();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = this.grdList.SelectedRows.Count;
            if ((count != 0) && (MessageBox.Show("确定要对所选择的" + count.ToString() + " 条任务下发执行吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No))
            {
                try
                {
                    string sErr = "";
                    string str2 = "-1";
                    count = 0;
                    foreach (DataGridViewRow row in this.grdList.SelectedRows)
                    {
                        if (Convert.ToInt32(row.Cells["colnWKStatus"].Value) == 0)
                        {
                            int pWorkId = Convert.ToInt32(row.Cells["colnWorkId"].Value);
                            str2 = PubDBCommFuns.sp_DoTaskCMD(base.AppInformation.SvrSocket, pWorkId, "WMS", base.UserInformation.UnitId, base.UserInformation.UserName, out sErr);
                            if ((str2.Trim() == "") || (str2.Trim() == "0"))
                            {
                                count++;
                            }
                        }
                    }
                    MessageBox.Show("成功下发了 " + count.ToString() + " 条任务！");
                    this.btnRefresh_Click(null, null);
                    this.grdList.Focus();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DoMDelete()
        {
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("对不起，无任务数据！");
            }
            else if (this.grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("对不起，您没有选择需要操作的任务数据！");
            }
            else
            {
                int num2 = 0;
                if (MessageBox.Show("您确定要对您所选择的已下发的任务进行取消操作吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
                {
                    try
                    {
                        foreach (DataGridViewRow row in this.grdList.SelectedRows)
                        {
                            string sErr = "";
                            if (Convert.ToInt32(row.Cells["colnWKStatus"].Value) == 1)
                            {
                                int pWorkId = Convert.ToInt32(row.Cells["colnWorkId"].Value);
                                switch (PubDBCommFuns.sp_UnDoTaskCMD(base.AppInformation.SvrSocket, pWorkId, "WMS", base.UserInformation.UnitId, base.UserInformation.UserName.Trim(), out sErr))
                                {
                                    case "":
                                    case "-1":
                                        MessageBox.Show(sErr);
                                        return;
                                }
                                num2++;
                            }
                        }
                        MessageBox.Show("成功取消 " + num2.ToString() + " 条已下发任务！");
                        this.btnRefresh_Click(null, null);
                        this.grdList.Focus();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            this.tlbSaveSysRts.Visible = base.UserInformation.UserName == "Admin5118";
            string sErr = "";
            StringBuilder builder = new StringBuilder("select * from TPB_Rights where cPRId ='" + base.ModuleRtsId.Trim() + "'");
            if (base.UserInformation.UserName != "Admin5118")
            {
                builder.Append(" and cRId in (select cRId from TPB_URTS where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            DataSet set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, builder.ToString(), "UserRights", "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            if (base.UserInformation.UserName != "Admin5118")
            {
                this.CheckRights(this.tlbMain, set.Tables["UserRights"]);
                this.CheckRights(this.panel2, set.Tables["UserRights"]);
                this.CheckRights(this.ppMain, set.Tables["UserRights"]);
            }
            this.dtp_From.Value = DateTime.Now.AddDays(-30.0);
            this.dtp_To.Value = DateTime.Now;
            this.LoadBaseData();
            this.OpenMainDataSet();
        }

        private string GetCondition()
        {
            StringBuilder builder = new StringBuilder(" where (dOptDate >='" + this.dtp_From.Value.ToString("yyyy-MM-dd 00:00:00") + "'");
            builder.Append(" and dOptDate<='" + this.dtp_To.Value.ToString("yyyy-MM-dd 23:59:59") + "' )");
            switch (this._WorkTaskType)
            {
                case TaskType.ttTaskInOnly:
                    builder.Append(" and nOptType in (1,2,5,7)");
                    break;

                case TaskType.ttTaskOutOnly:
                    builder.Append(" and nOptType in (3,4,6,7,8,9)");
                    break;
            }
            if ((this.cmb_WHId.Text.Trim() != "") && ((this.cmb_WHId.Items.Count > 0) && (this.cmb_WHId.SelectedValue != null)))
            {
                builder.Append(" and cWHId='" + this.cmb_WHId.SelectedValue.ToString().Trim() + "'");
            }
            if (this.cmb_User.Text.Trim() != "")
            {
                builder.Append(" and isnull(cUser,' ') ='" + this.cmb_User.Text.Trim() + "'");
            }
            else if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(string.Format(" and cuser in (select cname from tpb_user where cdeptid='{0}') ", base.UserInformation.DeptId));
            }
            if ((this.cmb_OptType.Text.Trim() != "") && ((this.cmb_OptType.Items.Count > 0) && (this.cmb_OptType.SelectedValue != null)))
            {
                builder.Append(" and nOptType=" + this.cmb_OptType.SelectedValue.ToString());
            }
            if ((this.cmb_Status.Text.Trim() != "") && ((this.cmb_Status.Items.Count > 0) && (this.cmb_Status.SelectedValue != null)))
            {
                if (this.cmb_Status.SelectedIndex < 3)
                {
                    builder.Append(" and nWKStatus=" + this.cmb_Status.SelectedValue.ToString());
                }
                else
                {
                    string str = this.cmb_Status.SelectedValue.ToString();
                    builder.Append(" and (nWKStatus>1 and nWKStatus < 99)");
                }
            }
            if (this.txt_PltId.Text.Trim() != "")
            {
                builder.Append(" and (nPalletId  like '%" + this.txt_PltId.Text.Trim() + "%' or isnull(cPosIdFrom,'~') like '%" + this.txt_PltId.Text.Trim() + "%' or isnull(cPosIdTo,'~') like '%" + this.txt_PltId.Text.Trim() + "%')");
            }
            if (this.txt_WkId.Text.Trim() != "")
            {
                builder.Append(" and nWorkId =" + this.txt_WkId.Text.Trim());
            }
            if (this.txt_MNo.Text.Trim() != "")
            {
                builder.Append(" and (isnull(cMNo,'~') like '%" + this.txt_MNo.Text.Trim() + "%' or isnull(cName,'~') like '%" + this.txt_MNo.Text.Trim() + "%')");
            }
            if (this.txt_BNo.Text.Trim() != "")
            {
                builder.Append(" and isnull(cBNo,'~') like '%" + this.txt_BNo.Text.Trim() + "%'");
            }
            return builder.ToString();
        }

        private void grpCondition_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Delete = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Undo = new System.Windows.Forms.ToolStripButton();
            this.tlb_DelTskDtlForOver = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_DoTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_M_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tlb_UpdateDsn = new System.Windows.Forms.ToolStripButton();
            this.tlb_UpdateOptStation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_M_Help = new System.Windows.Forms.ToolStripButton();
            this.tlb_M_Exit = new System.Windows.Forms.ToolStripButton();
            this.tlbSaveSysRts = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.ppMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_pp_DoAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pp_DoTaskCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pp_UndoTaskCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pp_UnPallet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_DelAccountTaskDtl = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pp_UpdateTaskPri = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_pp_UpdateOptStation = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblReceSum = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_UpdateOptStation = new System.Windows.Forms.Button();
            this.btn_UpdateDsn = new System.Windows.Forms.Button();
            this.btn_DelTskDtlForOver = new System.Windows.Forms.Button();
            this.btnReDoTask = new System.Windows.Forms.Button();
            this.btn_DoTaskCmd = new System.Windows.Forms.Button();
            this.btnDel2 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.grpCondition = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_MNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_BNo = new System.Windows.Forms.TextBox();
            this.btn_Print = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_PltId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_WkId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_OptType = new System.Windows.Forms.ComboBox();
            this.cmb_WHId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Qry = new System.Windows.Forms.Button();
            this.cmb_Status = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_User = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_To = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_From = new System.Windows.Forms.DateTimePicker();
            this.bdsMain = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.colnWorkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_noptStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnPalletId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcOptTypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colfQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcWKStatusDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcPosIdFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcPosIdTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldOptDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcBNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnWKStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.ppMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tlbMain
            // 
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.tlb_M_Edit,
            this.toolStripSeparator3,
            this.tlb_M_Delete,
            this.tlb_M_Undo,
            this.tlb_DelTskDtlForOver,
            this.toolStripSeparator4,
            this.tlb_M_DoTask,
            this.toolStripSeparator5,
            this.tlb_M_Refresh,
            this.toolStripSeparator6,
            this.toolStripButton1,
            this.tlb_UpdateDsn,
            this.tlb_UpdateOptStation,
            this.toolStripSeparator7,
            this.toolStripSeparator8,
            this.btn_M_Help,
            this.tlb_M_Exit,
            this.tlbSaveSysRts});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(923, 25);
            this.tlbMain.TabIndex = 14;
            this.tlbMain.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // tlb_M_Edit
            // 
            this.tlb_M_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_Edit.Tag = "01";
            this.tlb_M_Edit.Text = "手动过账";
            this.tlb_M_Edit.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Delete
            // 
            this.tlb_M_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new System.Drawing.Size(87, 22);
            this.tlb_M_Delete.Tag = "02";
            this.tlb_M_Delete.Text = "取消已下指令";
            this.tlb_M_Delete.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // tlb_M_Undo
            // 
            this.tlb_M_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_Undo.Tag = "03";
            this.tlb_M_Undo.Text = "取消配盘";
            this.tlb_M_Undo.Click += new System.EventHandler(this.btnDel2_Click);
            // 
            // tlb_DelTskDtlForOver
            // 
            this.tlb_DelTskDtlForOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_DelTskDtlForOver.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_DelTskDtlForOver.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_DelTskDtlForOver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_DelTskDtlForOver.Name = "tlb_DelTskDtlForOver";
            this.tlb_DelTskDtlForOver.Size = new System.Drawing.Size(126, 22);
            this.tlb_DelTskDtlForOver.Tag = "06";
            this.tlb_DelTskDtlForOver.Text = "删除已过账任务明细";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_DoTask
            // 
            this.tlb_M_DoTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_DoTask.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_DoTask.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_DoTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_DoTask.Name = "tlb_M_DoTask";
            this.tlb_M_DoTask.Size = new System.Drawing.Size(61, 22);
            this.tlb_M_DoTask.Tag = "04";
            this.tlb_M_DoTask.Text = "下发任务";
            this.tlb_M_DoTask.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tlb_M_Refresh
            // 
            this.tlb_M_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(87, 22);
            this.toolStripButton1.Tag = "05";
            this.toolStripButton1.Text = "重新下发任务";
            // 
            // tlb_UpdateDsn
            // 
            this.tlb_UpdateDsn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_UpdateDsn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_UpdateDsn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_UpdateDsn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_UpdateDsn.Name = "tlb_UpdateDsn";
            this.tlb_UpdateDsn.Size = new System.Drawing.Size(74, 22);
            this.tlb_UpdateDsn.Tag = "07";
            this.tlb_UpdateDsn.Text = "修改优先权";
            this.tlb_UpdateDsn.Click += new System.EventHandler(this.tlb_UpdateDsn_Click);
            // 
            // tlb_UpdateOptStation
            // 
            this.tlb_UpdateOptStation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_UpdateOptStation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_UpdateOptStation.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_UpdateOptStation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_UpdateOptStation.Name = "tlb_UpdateOptStation";
            this.tlb_UpdateOptStation.Size = new System.Drawing.Size(87, 22);
            this.tlb_UpdateOptStation.Tag = "08";
            this.tlb_UpdateOptStation.Text = "修改操作台号";
            this.tlb_UpdateOptStation.Click += new System.EventHandler(this.btn_UpdateOptStation_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator8.Visible = false;
            // 
            // btn_M_Help
            // 
            this.btn_M_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_M_Help.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btn_M_Help.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_M_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_M_Help.Name = "btn_M_Help";
            this.btn_M_Help.Size = new System.Drawing.Size(35, 22);
            this.btn_M_Help.Text = "帮助";
            this.btn_M_Help.Visible = false;
            // 
            // tlb_M_Exit
            // 
            this.tlb_M_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlb_M_Exit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tlb_M_Exit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlb_M_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_M_Exit.Name = "tlb_M_Exit";
            this.tlb_M_Exit.Size = new System.Drawing.Size(35, 22);
            this.tlb_M_Exit.Text = "退出";
            this.tlb_M_Exit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tlbSaveSysRts
            // 
            this.tlbSaveSysRts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new System.Drawing.Size(84, 22);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new System.EventHandler(this.tlbSaveSysRts_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.grpCondition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(923, 544);
            this.panel1.TabIndex = 15;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colnWorkId,
            this.col_noptStation,
            this.colnPalletId,
            this.colcOptTypeDesc,
            this.Column1,
            this.Column2,
            this.colfQty,
            this.colcUnit,
            this.colcWKStatusDesc,
            this.colcPosIdFrom,
            this.colcPosIdTo,
            this.colcUser,
            this.colcMNo,
            this.coldOptDate,
            this.colcBNo,
            this.colnWKStatus,
            this.Column3});
            this.grdList.ContextMenuStrip = this.ppMain;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new System.Drawing.Point(0, 69);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 23;
            this.grdList.Size = new System.Drawing.Size(765, 439);
            this.grdList.TabIndex = 18;
            this.grdList.Tag = "8";
            // 
            // ppMain
            // 
            this.ppMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_pp_DoAccount,
            this.mi_pp_DoTaskCmd,
            this.mi_pp_UndoTaskCmd,
            this.mi_pp_UnPallet,
            this.toolStripMenuItem1,
            this.mi_DelAccountTaskDtl,
            this.mi_pp_UpdateTaskPri,
            this.mi_pp_UpdateOptStation});
            this.ppMain.Name = "ppMain";
            this.ppMain.Size = new System.Drawing.Size(161, 164);
            // 
            // mi_pp_DoAccount
            // 
            this.mi_pp_DoAccount.Name = "mi_pp_DoAccount";
            this.mi_pp_DoAccount.Size = new System.Drawing.Size(160, 22);
            this.mi_pp_DoAccount.Tag = "01";
            this.mi_pp_DoAccount.Text = "过帐";
            this.mi_pp_DoAccount.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // mi_pp_DoTaskCmd
            // 
            this.mi_pp_DoTaskCmd.Name = "mi_pp_DoTaskCmd";
            this.mi_pp_DoTaskCmd.Size = new System.Drawing.Size(160, 22);
            this.mi_pp_DoTaskCmd.Tag = "04";
            this.mi_pp_DoTaskCmd.Text = "下发任务";
            this.mi_pp_DoTaskCmd.Click += new System.EventHandler(this.button1_Click);
            // 
            // mi_pp_UndoTaskCmd
            // 
            this.mi_pp_UndoTaskCmd.Name = "mi_pp_UndoTaskCmd";
            this.mi_pp_UndoTaskCmd.Size = new System.Drawing.Size(160, 22);
            this.mi_pp_UndoTaskCmd.Tag = "02";
            this.mi_pp_UndoTaskCmd.Text = "取消下发";
            this.mi_pp_UndoTaskCmd.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // mi_pp_UnPallet
            // 
            this.mi_pp_UnPallet.Name = "mi_pp_UnPallet";
            this.mi_pp_UnPallet.Size = new System.Drawing.Size(160, 22);
            this.mi_pp_UnPallet.Tag = "03";
            this.mi_pp_UnPallet.Text = "取消配盘";
            this.mi_pp_UnPallet.Click += new System.EventHandler(this.btnDel2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // mi_DelAccountTaskDtl
            // 
            this.mi_DelAccountTaskDtl.Name = "mi_DelAccountTaskDtl";
            this.mi_DelAccountTaskDtl.Size = new System.Drawing.Size(160, 22);
            this.mi_DelAccountTaskDtl.Tag = "06";
            this.mi_DelAccountTaskDtl.Text = "删除已过帐明细";
            this.mi_DelAccountTaskDtl.Click += new System.EventHandler(this.btn_DelTskDtlForOver_Click);
            // 
            // mi_pp_UpdateTaskPri
            // 
            this.mi_pp_UpdateTaskPri.Name = "mi_pp_UpdateTaskPri";
            this.mi_pp_UpdateTaskPri.Size = new System.Drawing.Size(160, 22);
            this.mi_pp_UpdateTaskPri.Tag = "07";
            this.mi_pp_UpdateTaskPri.Text = "修改执行优先权";
            this.mi_pp_UpdateTaskPri.Click += new System.EventHandler(this.tlb_UpdateDsn_Click);
            // 
            // mi_pp_UpdateOptStation
            // 
            this.mi_pp_UpdateOptStation.Name = "mi_pp_UpdateOptStation";
            this.mi_pp_UpdateOptStation.Size = new System.Drawing.Size(160, 22);
            this.mi_pp_UpdateOptStation.Tag = "08";
            this.mi_pp_UpdateOptStation.Text = "修改操作台号";
            this.mi_pp_UpdateOptStation.Click += new System.EventHandler(this.btn_UpdateOptStation_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblReceSum);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 508);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(765, 36);
            this.panel3.TabIndex = 19;
            // 
            // lblReceSum
            // 
            this.lblReceSum.AutoSize = true;
            this.lblReceSum.Font = new System.Drawing.Font("宋体", 15F);
            this.lblReceSum.Location = new System.Drawing.Point(418, 8);
            this.lblReceSum.Name = "lblReceSum";
            this.lblReceSum.Size = new System.Drawing.Size(19, 20);
            this.lblReceSum.TabIndex = 0;
            this.lblReceSum.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(328, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "总记录条数：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_UpdateOptStation);
            this.panel2.Controls.Add(this.btn_UpdateDsn);
            this.panel2.Controls.Add(this.btn_DelTskDtlForOver);
            this.panel2.Controls.Add(this.btnReDoTask);
            this.panel2.Controls.Add(this.btn_DoTaskCmd);
            this.panel2.Controls.Add(this.btnDel2);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnTransfer);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(765, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 475);
            this.panel2.TabIndex = 17;
            // 
            // btn_UpdateOptStation
            // 
            this.btn_UpdateOptStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_UpdateOptStation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateOptStation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_UpdateOptStation.ForeColor = System.Drawing.Color.Blue;
            this.btn_UpdateOptStation.Location = new System.Drawing.Point(8, 277);
            this.btn_UpdateOptStation.Name = "btn_UpdateOptStation";
            this.btn_UpdateOptStation.Size = new System.Drawing.Size(143, 31);
            this.btn_UpdateOptStation.TabIndex = 129;
            this.btn_UpdateOptStation.Tag = "08";
            this.btn_UpdateOptStation.Text = "修改操作台号";
            this.btn_UpdateOptStation.UseVisualStyleBackColor = true;
            this.btn_UpdateOptStation.Click += new System.EventHandler(this.btn_UpdateOptStation_Click);
            // 
            // btn_UpdateDsn
            // 
            this.btn_UpdateDsn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_UpdateDsn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateDsn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_UpdateDsn.ForeColor = System.Drawing.Color.Blue;
            this.btn_UpdateDsn.Location = new System.Drawing.Point(8, 235);
            this.btn_UpdateDsn.Name = "btn_UpdateDsn";
            this.btn_UpdateDsn.Size = new System.Drawing.Size(143, 31);
            this.btn_UpdateDsn.TabIndex = 128;
            this.btn_UpdateDsn.Tag = "07";
            this.btn_UpdateDsn.Text = "修改优先权";
            this.btn_UpdateDsn.UseVisualStyleBackColor = true;
            this.btn_UpdateDsn.Click += new System.EventHandler(this.btn_UpdateDsn_Click);
            // 
            // btn_DelTskDtlForOver
            // 
            this.btn_DelTskDtlForOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DelTskDtlForOver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DelTskDtlForOver.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DelTskDtlForOver.ForeColor = System.Drawing.Color.Blue;
            this.btn_DelTskDtlForOver.Location = new System.Drawing.Point(8, 320);
            this.btn_DelTskDtlForOver.Name = "btn_DelTskDtlForOver";
            this.btn_DelTskDtlForOver.Size = new System.Drawing.Size(143, 31);
            this.btn_DelTskDtlForOver.TabIndex = 127;
            this.btn_DelTskDtlForOver.Tag = "06";
            this.btn_DelTskDtlForOver.Text = "删除已过账任务明细";
            this.btn_DelTskDtlForOver.UseVisualStyleBackColor = true;
            this.btn_DelTskDtlForOver.Click += new System.EventHandler(this.btn_DelTskDtlForOver_Click);
            // 
            // btnReDoTask
            // 
            this.btnReDoTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReDoTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReDoTask.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReDoTask.ForeColor = System.Drawing.Color.Blue;
            this.btnReDoTask.Location = new System.Drawing.Point(8, 369);
            this.btnReDoTask.Name = "btnReDoTask";
            this.btnReDoTask.Size = new System.Drawing.Size(143, 31);
            this.btnReDoTask.TabIndex = 126;
            this.btnReDoTask.Tag = "05";
            this.btnReDoTask.Text = "重新下发任务";
            this.btnReDoTask.UseVisualStyleBackColor = true;
            this.btnReDoTask.Click += new System.EventHandler(this.btnReDoTask_Click);
            // 
            // btn_DoTaskCmd
            // 
            this.btn_DoTaskCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DoTaskCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DoTaskCmd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DoTaskCmd.ForeColor = System.Drawing.Color.Blue;
            this.btn_DoTaskCmd.Location = new System.Drawing.Point(8, 60);
            this.btn_DoTaskCmd.Name = "btn_DoTaskCmd";
            this.btn_DoTaskCmd.Size = new System.Drawing.Size(143, 31);
            this.btn_DoTaskCmd.TabIndex = 125;
            this.btn_DoTaskCmd.Tag = "04";
            this.btn_DoTaskCmd.Text = "下发任务";
            this.btn_DoTaskCmd.UseVisualStyleBackColor = true;
            this.btn_DoTaskCmd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDel2
            // 
            this.btnDel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel2.ForeColor = System.Drawing.Color.Blue;
            this.btnDel2.Location = new System.Drawing.Point(8, 149);
            this.btnDel2.Name = "btnDel2";
            this.btnDel2.Size = new System.Drawing.Size(143, 31);
            this.btnDel2.TabIndex = 124;
            this.btnDel2.Tag = "03";
            this.btnDel2.Text = "取消配盘";
            this.btnDel2.UseVisualStyleBackColor = true;
            this.btnDel2.Click += new System.EventHandler(this.btnDel2_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.Blue;
            this.btnExit.Location = new System.Drawing.Point(8, 418);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(143, 31);
            this.btnExit.TabIndex = 121;
            this.btnExit.Text = "退  出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTransfer.ForeColor = System.Drawing.Color.Blue;
            this.btnTransfer.Location = new System.Drawing.Point(8, 15);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(143, 31);
            this.btnTransfer.TabIndex = 123;
            this.btnTransfer.Tag = "01";
            this.btnTransfer.Text = "手动执行完毕";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Blue;
            this.btnRefresh.Location = new System.Drawing.Point(8, 192);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(143, 31);
            this.btnRefresh.TabIndex = 122;
            this.btnRefresh.Text = "刷  新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.ForeColor = System.Drawing.Color.Blue;
            this.btnDel.Location = new System.Drawing.Point(8, 104);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(143, 31);
            this.btnDel.TabIndex = 120;
            this.btnDel.Tag = "02";
            this.btnDel.Text = "取消已下指令";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // grpCondition
            // 
            this.grpCondition.Controls.Add(this.label11);
            this.grpCondition.Controls.Add(this.txt_MNo);
            this.grpCondition.Controls.Add(this.label10);
            this.grpCondition.Controls.Add(this.txt_BNo);
            this.grpCondition.Controls.Add(this.btn_Print);
            this.grpCondition.Controls.Add(this.label8);
            this.grpCondition.Controls.Add(this.txt_PltId);
            this.grpCondition.Controls.Add(this.label7);
            this.grpCondition.Controls.Add(this.txt_WkId);
            this.grpCondition.Controls.Add(this.label3);
            this.grpCondition.Controls.Add(this.cmb_OptType);
            this.grpCondition.Controls.Add(this.cmb_WHId);
            this.grpCondition.Controls.Add(this.label6);
            this.grpCondition.Controls.Add(this.btn_Reset);
            this.grpCondition.Controls.Add(this.btn_Qry);
            this.grpCondition.Controls.Add(this.cmb_Status);
            this.grpCondition.Controls.Add(this.label5);
            this.grpCondition.Controls.Add(this.cmb_User);
            this.grpCondition.Controls.Add(this.label4);
            this.grpCondition.Controls.Add(this.label2);
            this.grpCondition.Controls.Add(this.dtp_To);
            this.grpCondition.Controls.Add(this.label1);
            this.grpCondition.Controls.Add(this.dtp_From);
            this.grpCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCondition.Location = new System.Drawing.Point(0, 0);
            this.grpCondition.Name = "grpCondition";
            this.grpCondition.Size = new System.Drawing.Size(923, 69);
            this.grpCondition.TabIndex = 0;
            this.grpCondition.TabStop = false;
            this.grpCondition.Text = "条件";
            this.grpCondition.Enter += new System.EventHandler(this.grpCondition_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(610, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "物料";
            // 
            // txt_MNo
            // 
            this.txt_MNo.Location = new System.Drawing.Point(642, 17);
            this.txt_MNo.Name = "txt_MNo";
            this.txt_MNo.Size = new System.Drawing.Size(112, 21);
            this.txt_MNo.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(610, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "单号";
            // 
            // txt_BNo
            // 
            this.txt_BNo.Location = new System.Drawing.Point(642, 43);
            this.txt_BNo.Name = "txt_BNo";
            this.txt_BNo.Size = new System.Drawing.Size(112, 21);
            this.txt_BNo.TabIndex = 21;
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(849, 16);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(70, 23);
            this.btn_Print.TabIndex = 20;
            this.btn_Print.Text = "打印";
            this.toolTip.SetToolTip(this.btn_Print, "查询");
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(481, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 24);
            this.label8.TabIndex = 19;
            this.label8.Text = "托盘/货位";
            // 
            // txt_PltId
            // 
            this.txt_PltId.Location = new System.Drawing.Point(521, 42);
            this.txt_PltId.Name = "txt_PltId";
            this.txt_PltId.Size = new System.Drawing.Size(89, 21);
            this.txt_PltId.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(481, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "任务号";
            // 
            // txt_WkId
            // 
            this.txt_WkId.Location = new System.Drawing.Point(521, 16);
            this.txt_WkId.Name = "txt_WkId";
            this.txt_WkId.Size = new System.Drawing.Size(89, 21);
            this.txt_WkId.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "指令类型";
            // 
            // cmb_OptType
            // 
            this.cmb_OptType.FormattingEnabled = true;
            this.cmb_OptType.Location = new System.Drawing.Point(376, 16);
            this.cmb_OptType.Name = "cmb_OptType";
            this.cmb_OptType.Size = new System.Drawing.Size(100, 20);
            this.cmb_OptType.TabIndex = 2;
            // 
            // cmb_WHId
            // 
            this.cmb_WHId.FormattingEnabled = true;
            this.cmb_WHId.Location = new System.Drawing.Point(58, 42);
            this.cmb_WHId.Name = "cmb_WHId";
            this.cmb_WHId.Size = new System.Drawing.Size(109, 20);
            this.cmb_WHId.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "仓    库";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(760, 16);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 11;
            this.btn_Reset.Text = "重置";
            this.toolTip.SetToolTip(this.btn_Reset, "恢复到初始条件");
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Qry
            // 
            this.btn_Qry.Location = new System.Drawing.Point(760, 41);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new System.Drawing.Size(160, 23);
            this.btn_Qry.TabIndex = 8;
            this.btn_Qry.Text = "查询";
            this.toolTip.SetToolTip(this.btn_Qry, "查询");
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new System.EventHandler(this.btn_Qry_Click);
            // 
            // cmb_Status
            // 
            this.cmb_Status.FormattingEnabled = true;
            this.cmb_Status.Location = new System.Drawing.Point(376, 42);
            this.cmb_Status.Name = "cmb_Status";
            this.cmb_Status.Size = new System.Drawing.Size(100, 20);
            this.cmb_Status.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "指令状态";
            // 
            // cmb_User
            // 
            this.cmb_User.FormattingEnabled = true;
            this.cmb_User.Location = new System.Drawing.Point(212, 42);
            this.cmb_User.Name = "cmb_User";
            this.cmb_User.Size = new System.Drawing.Size(107, 20);
            this.cmb_User.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "操作员";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(176, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 1);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // dtp_To
            // 
            this.dtp_To.Location = new System.Drawing.Point(211, 16);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new System.Drawing.Size(108, 21);
            this.dtp_To.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "操作日期";
            // 
            // dtp_From
            // 
            this.dtp_From.Location = new System.Drawing.Point(58, 16);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new System.Drawing.Size(109, 21);
            this.dtp_From.TabIndex = 0;
            // 
            // colnWorkId
            // 
            this.colnWorkId.DataPropertyName = "nWorkId";
            this.colnWorkId.FillWeight = 65F;
            this.colnWorkId.Frozen = true;
            this.colnWorkId.HeaderText = "任务号";
            this.colnWorkId.Name = "colnWorkId";
            this.colnWorkId.ReadOnly = true;
            this.colnWorkId.ToolTipText = "任务号";
            this.colnWorkId.Width = 65;
            // 
            // col_noptStation
            // 
            this.col_noptStation.DataPropertyName = "nOptStation";
            this.col_noptStation.HeaderText = "操作台号";
            this.col_noptStation.Name = "col_noptStation";
            this.col_noptStation.ReadOnly = true;
            this.col_noptStation.Width = 65;
            // 
            // colnPalletId
            // 
            this.colnPalletId.DataPropertyName = "nPalletId";
            this.colnPalletId.HeaderText = "托盘号";
            this.colnPalletId.Name = "colnPalletId";
            this.colnPalletId.ReadOnly = true;
            this.colnPalletId.ToolTipText = "托盘号";
            this.colnPalletId.Width = 75;
            // 
            // colcOptTypeDesc
            // 
            this.colcOptTypeDesc.DataPropertyName = "cOptDesc";
            this.colcOptTypeDesc.FillWeight = 75F;
            this.colcOptTypeDesc.HeaderText = "操作描述";
            this.colcOptTypeDesc.Name = "colcOptTypeDesc";
            this.colcOptTypeDesc.ReadOnly = true;
            this.colcOptTypeDesc.ToolTipText = "操作描述";
            this.colcOptTypeDesc.Width = 75;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "cName";
            this.Column1.HeaderText = "名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cSpec";
            this.Column2.HeaderText = "规格";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // colfQty
            // 
            this.colfQty.DataPropertyName = "fQty";
            this.colfQty.HeaderText = "数量";
            this.colfQty.Name = "colfQty";
            this.colfQty.ReadOnly = true;
            this.colfQty.Width = 75;
            // 
            // colcUnit
            // 
            this.colcUnit.DataPropertyName = "cUnit";
            this.colcUnit.FillWeight = 65F;
            this.colcUnit.HeaderText = "单位";
            this.colcUnit.Name = "colcUnit";
            this.colcUnit.ReadOnly = true;
            this.colcUnit.Width = 65;
            // 
            // colcWKStatusDesc
            // 
            this.colcWKStatusDesc.DataPropertyName = "cWKStatusDesc";
            this.colcWKStatusDesc.HeaderText = "指令状态";
            this.colcWKStatusDesc.Name = "colcWKStatusDesc";
            this.colcWKStatusDesc.ReadOnly = true;
            this.colcWKStatusDesc.ToolTipText = "指令状态";
            this.colcWKStatusDesc.Width = 80;
            // 
            // colcPosIdFrom
            // 
            this.colcPosIdFrom.DataPropertyName = "cPosIdFrom";
            this.colcPosIdFrom.HeaderText = "源仓位";
            this.colcPosIdFrom.Name = "colcPosIdFrom";
            this.colcPosIdFrom.ReadOnly = true;
            this.colcPosIdFrom.ToolTipText = "源仓位";
            this.colcPosIdFrom.Width = 80;
            // 
            // colcPosIdTo
            // 
            this.colcPosIdTo.DataPropertyName = "cPosIdTo";
            this.colcPosIdTo.HeaderText = "目标仓位";
            this.colcPosIdTo.Name = "colcPosIdTo";
            this.colcPosIdTo.ReadOnly = true;
            this.colcPosIdTo.ToolTipText = "目标仓位";
            this.colcPosIdTo.Width = 80;
            // 
            // colcUser
            // 
            this.colcUser.DataPropertyName = "cUser";
            this.colcUser.HeaderText = "操作员";
            this.colcUser.Name = "colcUser";
            this.colcUser.ReadOnly = true;
            this.colcUser.Width = 75;
            // 
            // colcMNo
            // 
            this.colcMNo.DataPropertyName = "cMNo";
            this.colcMNo.FillWeight = 85F;
            this.colcMNo.HeaderText = "物料编号";
            this.colcMNo.Name = "colcMNo";
            this.colcMNo.ReadOnly = true;
            this.colcMNo.ToolTipText = "物料编号";
            this.colcMNo.Width = 85;
            // 
            // coldOptDate
            // 
            this.coldOptDate.DataPropertyName = "dOptDate";
            this.coldOptDate.HeaderText = "操作时间";
            this.coldOptDate.Name = "coldOptDate";
            this.coldOptDate.ReadOnly = true;
            // 
            // colcBNo
            // 
            this.colcBNo.DataPropertyName = "cBNo";
            this.colcBNo.HeaderText = "来源单号";
            this.colcBNo.Name = "colcBNo";
            this.colcBNo.ReadOnly = true;
            // 
            // colnWKStatus
            // 
            this.colnWKStatus.DataPropertyName = "nWKStatus";
            this.colnWKStatus.HeaderText = "状态";
            this.colnWKStatus.Name = "colnWKStatus";
            this.colnWKStatus.ReadOnly = true;
            this.colnWKStatus.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "其它";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // frmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(923, 569);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tlbMain);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmTask";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "任务管理";
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ppMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.grpCondition.ResumeLayout(false);
            this.grpCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadBaseData()
        {
            string sErr = "";
            DataSet set = null;
            DataTable table = null;
            DataTable table2 = null;
            StringBuilder builder = new StringBuilder("");
            this.cmb_User.Items.Clear();
            this.cmb_WHId.Items.Clear();
            this.cmb_WHId.Text = "";
            this.cmb_User.Text = "";
            builder.Append("select * from TWC_WareHouse where bUsed=1");
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(" and cCmptId='" + base.UserInformation.UnitId.Trim() + "'");
            }
            if (base.UserInformation.UType != UserType.utSupervisor)
            {
                builder.Append(" and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + base.UserInformation.UserId.Trim() + "')");
            }
            set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, builder.ToString(), "TWC_WareHouse", "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                table = set.Tables["TWC_WareHouse"].Copy();
                this.cmb_WHId.DataSource = table;
                this.cmb_WHId.DisplayMember = "cName";
                this.cmb_WHId.ValueMember = "cWHId";
                this.cmb_WHId.SelectedIndex = -1;
            }
            this.cmb_User.Enabled = true;
            builder.Remove(0, builder.Length);
            builder.Append("select cUserId,cName from TPB_User where bUsed=1");
            if (base.UserInformation.UType == UserType.utNormal)
            {
                builder.Append(" and cuserid ='" + base.UserInformation.UserId.Trim() + "'");
                this.cmb_User.Enabled = false;
            }
            else if (base.UserInformation.UType == UserType.utAdmin)
            {
                builder.Append(" and cDeptId ='" + base.UserInformation.DeptId.Trim() + "'");
            }
            set = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, builder.ToString(), "TPB_User", "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
            else
            {
                table2 = set.Tables["TPB_User"].Copy();
                this.cmb_User.DataSource = table2;
                this.cmb_User.DisplayMember = "cName";
                this.cmb_User.ValueMember = "cUserId";
                this.cmb_User.SelectedIndex = -1;
                if ((base.UserInformation.UType != UserType.utSupervisor) && (this.cmb_User.Items.Count > 0))
                {
                    this.cmb_User.SelectedValue = base.UserInformation.UserId.Trim();
                }
            }
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry("0", "未下发"));
            list.Add(new DictionaryEntry("1", "已下发未执行"));
            list.Add(new DictionaryEntry("99", "执行结束"));
            list.Add(new DictionaryEntry("2", "执行中"));
            this.cmb_Status.DataSource = list;
            this.cmb_Status.DisplayMember = "Value";
            this.cmb_Status.ValueMember = "Key";
            ArrayList list2 = new ArrayList();
            if ((this._WorkTaskType == TaskType.ttTaskAll) || (this._WorkTaskType == TaskType.ttTaskInOnly))
            {
                list2.Add(new DictionaryEntry("1", "整盘入库"));
                list2.Add(new DictionaryEntry("2", "补充入库"));
                list2.Add(new DictionaryEntry("5", "空盘入库"));
            }
            if ((this._WorkTaskType == TaskType.ttTaskAll) || (this._WorkTaskType == TaskType.ttTaskOutOnly))
            {
                list2.Add(new DictionaryEntry("3", "整盘出库"));
                list2.Add(new DictionaryEntry("4", "分拣出库"));
                list2.Add(new DictionaryEntry("6", "空盘出库"));
                list2.Add(new DictionaryEntry("9", "取样出库"));
                list2.Add(new DictionaryEntry("8", "盘点/出库查看"));
            }
            if (this._WorkTaskType == TaskType.ttTaskAll)
            {
                list2.Add(new DictionaryEntry("7", "移库"));
            }
            this.cmb_OptType.DataSource = list2;
            this.cmb_OptType.DisplayMember = "Value";
            this.cmb_OptType.ValueMember = "Key";
        }

        public bool OpenMainDataSet()
        {
            if (this.dtp_To.Value < this.dtp_From.Value)
            {
                MessageBox.Show("对不起，截止日期不能小于起始日期！");
                this.dtp_From.Focus();
                return false;
            }
            bool flag = false;
            this.grdList.AutoGenerateColumns = false;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            string sSql = "select * from " + this.strTbNameMain + "" + this.GetCondition();
            string sErr = "";
            base.DBDataSet = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, this.strTbNameMain, 0, 0, "dOptDate", out sErr);
            flag = sErr == "";
            if (!flag)
            {
                MessageBox.Show(sErr);
            }
            else
            {
                try
                {
                    this.bdsMain.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
                    this.lblReceSum.Text = base.DBDataSet.Tables[this.strTbNameMain].Rows.Count + "";
                    this.BindDataSetToCtrls();
                    flag = true;
                    this.optMain = OperateType.optNone;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            return flag;
        }

        private void ReSetConition()
        {
            this.cmb_WHId.Text = "";
            this.cmb_Status.Text = "";
            this.cmb_OptType.Text = "";
            this.txt_BNo.Text = "";
            this.txt_MNo.Text = "";
            if ((base.UserInformation.UType != UserType.utSupervisor) && (this.cmb_User.Items.Count > 0))
            {
                this.cmb_User.Text = base.UserInformation.UserName.Trim();
            }
            this.txt_WkId.Text = "";
            this.txt_PltId.Text = "";
        }

        private void tlb_Print_Click(object sender, EventArgs e)
        {
            string sSql = "select cast(nOptStation as int) nOptStation,cWKStatusDesc,cWHId,nWorkId,cBNo,nItem,cMNo,cName,cSpec,cBatchNo,fQty,cUnit,cPosIdFrom,nPalletId,cOptDesc,dOptDate, cBNoIn, nItemIn from V_WorkTaskDtl ";
            sSql = sSql + this.GetCondition();
            string sErr = "";
            DataSet dsRpt = PubDBCommFuns.GetDataBySql(base.AppInformation.SvrSocket, false, sSql, "tbTaskDtlList", 0, 0, "dOptDate", out sErr);
            bool flag = sErr == "";
            if (!flag)
            {
                MessageBox.Show(sErr);
            }
            else
            {
                try
                {
                    RptTask.PrintTaskDtlList(dsRpt, base.UserInformation.UnitName, "");
                    dsRpt.Clear();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            if (dsRpt != null)
            {
                dsRpt.Clear();
            }
        }

        private void tlb_UpdateDsn_Click(object sender, EventArgs e)
        {
            bool isOK = false;
            if (this.bdsMain.Count == 0)
            {
                MessageBox.Show("无工作任务需修改优先权！");
            }
            else if (this.grdList.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择需修改优先权的工作任务！");
            }
            else
            {
                frmInputWorkDsn dsn = new frmInputWorkDsn {
                    AppInformation = base.AppInformation
                };
                dsn.ShowDialog();
                int resultValue = 0;
                isOK = dsn.IsOK;
                if (isOK)
                {
                    resultValue = dsn.ResultValue;
                }
                dsn.Dispose();
                if (isOK)
                {
                    int num2 = 0;
                    foreach (DataGridViewRow row in this.grdList.SelectedRows)
                    {
                        int num3 = 0;
                        if (row.Cells["colnWorkId"].Value != null)
                        {
                            num3 = Convert.ToInt32(row.Cells["colnWorkId"].Value);
                        }
                        string sErr = "";
                        string sSql = "update TWB_WorkTask set nPri=" + resultValue.ToString() + " where nWKStatus < 2  and nWorkId=" + num3.ToString() + " and nPri<>" + resultValue.ToString();
                        PubDBCommFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, "", out sErr);
                        if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                        {
                            MessageBox.Show(sErr);
                            return;
                        }
                        num2++;
                    }
                    if (num2 > 0)
                    {
                        MessageBox.Show("成功修改了 " + num2.ToString() + " 条任务！");
                    }
                }
            }
        }

        private void tlbSaveSysRts_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in this.tlbMain.Items)
            {
                if (item.Visible)
                {
                    object tag = item.Tag;
                    if (tag != null)
                    {
                        string sErr = "";
                        string text = item.Text;
                        string name = item.Name;
                        string pRId = base.ModuleRtsId + tag.ToString();
                        PubDBCommFuns.sp_SaveSysRight(base.AppInformation.SvrSocket, base.ModuleRtsId, pRId, text, "", name, 3, "Sys", out sErr);
                    }
                }
            }
        }

        public TaskType WorkTaskType
        {
            get
            {
                return this._WorkTaskType;
            }
            set
            {
                this._WorkTaskType = value;
            }
        }
    }
}

