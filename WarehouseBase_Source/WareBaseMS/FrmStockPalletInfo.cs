namespace WareBaseMS
{
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;
    using DBCommInfo;
    using FileFun;
    using Zqm.Xml;

    public class FrmStockPalletInfo : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private ToolStripButton btn_M_Help;
        private Button btn_Qry;
        private Button btn_ReSet;
        private ComboBox cmb_bIsPrint;
        private ComboBox cmb_bIsRealPlt;
        private ComboBox cmb_cPalletSpec;
        private ComboBox cmbQ_cPalletSpec;
        private ComboBox cmbQ_IsRealPlt;
        private ComboBox cmbQ_StoreStatus;
        private ComboBox comboBox_nStatusStore;
        private IContainer components = null;
        private DataGridViewTextBoxColumn cPalletSpec;
        private DataGridViewTextBoxColumn cPCId;
        private DataGridViewTextBoxColumn cRemark;
        private DataGridView dataGridView_Main;
        private DataGridViewTextBoxColumn grdCol_bIsPrint;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ToolStripMenuItem mi_PrintPltRemark;
        private ToolStripMenuItem miPrintBC;
        private DataGridViewTextBoxColumn nPalletId;
        private DataGridViewTextBoxColumn nType;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private ContextMenuStrip ppPrint;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cPCId";
        private string strTbNameMain = "V_PalletCell";
        private TextBox textBox_cPCId;
        private TextBox textBox_cPCIdQ;
        private TextBox textBox_cRemark;
        private TextBox textBox_nPalletId;
        public ToolStripButton tlb_M_Delete;
        public ToolStripButton tlb_M_Edit;
        private ToolStripButton tlb_M_Exit;
        public ToolStripButton tlb_M_Find;
        public ToolStripButton tlb_M_New;
        public ToolStripButton tlb_M_Print;
        public ToolStripButton tlb_M_Refresh;
        public ToolStripButton tlb_M_Save;
        public ToolStripButton tlb_M_Undo;
        public ToolStrip tlbMain;
        private ToolStripButton tlbSaveSysRts;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private TextBox txtQ_cRemark;

        public FrmStockPalletInfo()
        {
            this.InitializeComponent();
        }

        private bool BandDataSet(string SqlStrConditon, DataGridView FDataGridView)
        {
            bool flag = true;
            string sSql = "";
            string sErr = "";
            this.bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            sSql = ("select cPCId,nPalletId,nStatusStore,cAreaId,cRemark,bIsPrint,cPalletSpec,bIsRealPlt,cIsPrint,cPalletSpecDesc,cIsRealPlt,cStatusStore from  " + this.strTbNameMain + " " + SqlStrConditon) + " order by cPalletSpec,cAreaId,bIsPrint,nPalletId";
            try
            {
                base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            flag = base.DBDataSet != null;
            this.bindingSource_Main.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
            FDataGridView.DataSource = this.bindingSource_Main;
            if (this.bindingSource_Main.Count > 0)
            {
                try
                {
                    this.bDSIsOpenForMain = true;
                    this.DataRowViewToUI((DataRowView) this.bindingSource_Main.Current, this.panel_Edit);
                    this.OptMain = OperateType.optNone;
                }
                catch (Exception exception2)
                {
                    this.bDSIsOpenForMain = false;
                    MessageBox.Show(exception2.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
            }
            return flag;
        }

        private void bindingSource_Main_PositionChanged(object sender, EventArgs e)
        {
            if (this.bDSIsOpenForMain)
            {
                this.ClearUIValues(this.panel_Edit);
                if (!((DataRowView) this.bindingSource_Main.Current).IsNew)
                {
                    DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                    this.DataRowViewToUI(current, this.panel_Edit);
                }
            }
        }

        private void btn_ReSet_Click(object sender, EventArgs e)
        {
            this.txtQ_cRemark.Text = "";
            this.textBox_cPCIdQ.Text = "";
            this.cmbQ_cPalletSpec.SelectedIndex = -1;
            this.cmbQ_IsRealPlt.SelectedIndex = -1;
            this.cmbQ_StoreStatus.SelectedIndex = -1;
            this.textBox_cPCIdQ.Focus();
        }

        private void cmb_SelectedValue_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoDelete()
        {
            if (this.dataGridView_Main.RowCount >= 2)
            {
                bool flag = false;
                int optMain = -1;
                optMain = (int) this.OptMain;
                DataRowView current = (DataRowView) this.bindingSource_Main.Current;
                if (current == null)
                {
                    flag = true;
                    MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return flag;
                }
                if ((optMain > 0) && (optMain < 3))
                {
                    MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                }
                if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return flag;
                }
                string sSql = "delete " + this.strTbNameMain + " where " + this.strKeyFld + "='" + current[this.strKeyFld].ToString() + "'";
                if (PubDBCommFuns.GetDataBySql(sSql, out sSql).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optDelete;
                    this.BandDataSet(" ", this.dataGridView_Main);
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.OptMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.OptMain);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("删除成功！");
                    return flag;
                }
                MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return true;
        }

        private bool DoEdit()
        {
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
            this.OptMain = OperateType.optEdit;
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                return false;
            }
            current.BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.cmb_bIsRealPlt.Enabled = false;
            return false;
        }

        public bool DoNew()
        {
            this.OptMain = OperateType.optNew;
            DataRowView view = (DataRowView) this.bindingSource_Main.AddNew();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.DisplayState(this.stbState, this.OptMain);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.cmb_bIsRealPlt.Enabled = false;
            return false;
        }

        private bool DoRefresh()
        {
            this.sbCondition.Remove(0, this.sbCondition.Length);
            this.sbCondition.Append(this.GetCondition());
            this.BandDataSet(this.sbCondition.ToString(), this.dataGridView_Main);
            return true;
        }

        private bool DoSave()
        {
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
            string sSql = "";
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current.IsEdit || current.IsNew)
            {
                this.UIToDataRowView(current, this.panel_Edit);
                if (current[this.strKeyFld].ToString() == "")
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 6, current["cWHId"].ToString());
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, "cStatusStore,cIsPrint,cUsed,cIsRealPlt,cPalletSpecDesc,nL,nW,nH,nHeight,fWeight", true);
                }
                else
                {
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, "cStatusStore,cIsPrint,cUsed,cIsRealPlt,cPalletSpecDesc,nL,nW,nH,nHeight,fWeight", false);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                if (PubDBCommFuns.GetDataBySql(sSql, out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optSave;
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OptMain = OperateType.optNone;
                }
                else
                {
                    MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.cmb_bIsRealPlt.Enabled = false;
            return false;
        }

        private bool DoUndo()
        {
            this.OptMain = OperateType.optUndo;
            DataRowView drv = null;
            drv = (DataRowView) this.bindingSource_Main.Current;
            if (drv != null)
            {
                if (drv.IsEdit)
                {
                    drv.CancelEdit();
                }
                if (drv.IsNew)
                {
                    drv.Delete();
                }
                base.DBDataSet.Tables[this.strTbNameMain].AcceptChanges();
                drv = (DataRowView) this.bindingSource_Main.Current;
                this.DataRowViewToUI(drv, this.panel_Edit);
                this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                this.OptMain = OperateType.optNone;
                this.DisplayState(this.stbState, this.OptMain);
                this.CtrlControlReadOnly(this.panel_Edit, false);
                this.cmb_bIsRealPlt.Enabled = false;
            }
            return false;
        }

        private void FrmStockInfo_Load(object sender, EventArgs e)
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
            }
            this.LoadCommboxItemByValue();
            this.LoadPalletSpecList();
            this.LoadAreaList("");
            this.BandDataSet("", this.dataGridView_Main);
            if ((base.UserInformation.UType == UserType.utSupervisor) && (base.UserInformation.UserId == "90101001"))
            {
                this.tlb_M_New.Visible = true;
            }
            else
            {
                this.tlb_M_New.Visible = false;
            }
        }

        private string GetCondition()
        {
            object selectedValue;
            StringBuilder builder = new StringBuilder(" where 1=1 ");
            if (((this.cmbQ_IsRealPlt.Text.Trim() != "") && (this.cmbQ_IsRealPlt.Text.Trim() != "全部")) && ((this.cmbQ_IsRealPlt.SelectedIndex == 0) || (this.cmbQ_IsRealPlt.SelectedIndex == 1)))
            {
                builder.Append(" and isnull(bIsRealPlt,0)=" + this.cmbQ_IsRealPlt.SelectedIndex.ToString());
            }
            if (this.cmbQ_StoreStatus.Text.Trim() != "")
            {
                selectedValue = this.cmbQ_StoreStatus.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and isnull(nStatusStore,0)=" + selectedValue.ToString().Trim() + "");
                }
            }
            if (this.textBox_cPCIdQ.Text.Trim() != "")
            {
                builder.Append(" and cPCId  like '%" + this.textBox_cPCIdQ.Text.Trim() + "%'");
            }
            if (this.cmbQ_cPalletSpec.Text.Trim() != "")
            {
                selectedValue = this.cmbQ_cPalletSpec.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and isnull(cPalletSpec,' ')='" + selectedValue.ToString().Trim() + "'");
                }
            }
            if (this.txtQ_cRemark.Text.Trim() != "")
            {
                builder.Append(" and isnull(cRemark,' ') like '%" + this.txtQ_cRemark.Text.Trim() + "%'");
            }
            return builder.ToString();
        }

        private string GetPltSpec(string sSpecId)
        {
            string str = "";
            if (sSpecId.Trim() != "")
            {
                object objValue = null;
                string sErr = "";
                string sSql = "select isnull(max(cPalletSpec),' ') cPalletSpec from TWC_PalletSpec where cPalletSpecId='" + sSpecId.Trim() + "'";
                PubDBCommFuns.GetValueBySql(base.AppInformation.SvrSocket, sSql, "", "cPalletSpec", out objValue, out sErr);
                if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
                {
                    MessageBox.Show(sErr);
                }
                else if (objValue == null)
                {
                    MessageBox.Show("获取托盘规格时出错！");
                }
                else
                {
                    str = objValue.ToString().Trim();
                }
            }
            return str.Trim();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockPalletInfo));
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tlb_M_New = new ToolStripButton();
            this.tlb_M_Edit = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tlb_M_Undo = new ToolStripButton();
            this.tlb_M_Delete = new ToolStripButton();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.tlb_M_Save = new ToolStripButton();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.tlb_M_Refresh = new ToolStripButton();
            this.tlb_M_Find = new ToolStripButton();
            this.tlb_M_Print = new ToolStripButton();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.btn_M_Help = new ToolStripButton();
            this.tlb_M_Exit = new ToolStripButton();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.tlbSaveSysRts = new ToolStripButton();
            this.panel1 = new Panel();
            this.btn_ReSet = new Button();
            this.label13 = new Label();
            this.txtQ_cRemark = new TextBox();
            this.label12 = new Label();
            this.cmbQ_IsRealPlt = new ComboBox();
            this.btn_Qry = new Button();
            this.label6 = new Label();
            this.cmbQ_StoreStatus = new ComboBox();
            this.cmbQ_cPalletSpec = new ComboBox();
            this.dataGridView_Main = new DataGridView();
            this.ppPrint = new ContextMenuStrip(this.components);
            this.miPrintBC = new ToolStripMenuItem();
            this.mi_PrintPltRemark = new ToolStripMenuItem();
            this.label4 = new Label();
            this.textBox_cPCIdQ = new TextBox();
            this.label5 = new Label();
            this.panel_Edit = new Panel();
            this.cmb_bIsPrint = new ComboBox();
            this.label10 = new Label();
            this.cmb_bIsRealPlt = new ComboBox();
            this.cmb_cPalletSpec = new ComboBox();
            this.label11 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.comboBox_nStatusStore = new ComboBox();
            this.textBox_cRemark = new TextBox();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cPCId = new TextBox();
            this.textBox_nPalletId = new TextBox();
            this.bindingSource_Main = new BindingSource(this.components);
            this.nPalletId = new DataGridViewTextBoxColumn();
            this.grdCol_bIsPrint = new DataGridViewTextBoxColumn();
            this.cPalletSpec = new DataGridViewTextBoxColumn();
            this.nType = new DataGridViewTextBoxColumn();
            this.cRemark = new DataGridViewTextBoxColumn();
            this.cPCId = new DataGridViewTextBoxColumn();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            this.ppPrint.SuspendLayout();
            this.panel_Edit.SuspendLayout();
            this.stbMain.SuspendLayout();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x360, 0x19);
            this.tlbMain.TabIndex = 15;
            this.tlbMain.Text = "toolStrip1";
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.tlb_M_New.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_New.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_New.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_New.Image = (Image) manager.GetObject("tlb_M_New.Image");
            this.tlb_M_New.ImageTransparentColor = Color.Magenta;
            this.tlb_M_New.Name = "tlb_M_New";
            this.tlb_M_New.Size = new Size(0x23, 0x16);
            this.tlb_M_New.Tag = "01";
            this.tlb_M_New.Text = "新建";
            this.tlb_M_New.Visible = false;
            this.tlb_M_New.Click += new EventHandler(this.tlb_M_New_Click);
            this.tlb_M_Edit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Edit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Edit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Edit.Image = (Image) manager.GetObject("tlb_M_Edit.Image");
            this.tlb_M_Edit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Edit.Name = "tlb_M_Edit";
            this.tlb_M_Edit.Size = new Size(0x23, 0x16);
            this.tlb_M_Edit.Tag = "02";
            this.tlb_M_Edit.Text = "修改";
            this.tlb_M_Edit.Click += new EventHandler(this.tlb_M_Edit_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.tlb_M_Undo.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Undo.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Undo.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Undo.Image = (Image) manager.GetObject("tlb_M_Undo.Image");
            this.tlb_M_Undo.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Undo.Name = "tlb_M_Undo";
            this.tlb_M_Undo.Size = new Size(0x23, 0x16);
            this.tlb_M_Undo.Tag = "03";
            this.tlb_M_Undo.Text = "取消";
            this.tlb_M_Undo.Click += new EventHandler(this.tlb_M_Undo_Click);
            this.tlb_M_Delete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Delete.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Delete.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Delete.Image = (Image) manager.GetObject("tlb_M_Delete.Image");
            this.tlb_M_Delete.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Delete.Name = "tlb_M_Delete";
            this.tlb_M_Delete.Size = new Size(0x23, 0x16);
            this.tlb_M_Delete.Tag = "04";
            this.tlb_M_Delete.Text = "删除";
            this.tlb_M_Delete.Click += new EventHandler(this.tlb_M_Delete_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(6, 0x19);
            this.tlb_M_Save.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Save.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Save.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Save.Image = (Image) manager.GetObject("tlb_M_Save.Image");
            this.tlb_M_Save.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Save.Name = "tlb_M_Save";
            this.tlb_M_Save.Size = new Size(0x23, 0x16);
            this.tlb_M_Save.Tag = "05";
            this.tlb_M_Save.Text = "保存";
            this.tlb_M_Save.Click += new EventHandler(this.tlb_M_Save_Click);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(6, 0x19);
            this.tlb_M_Refresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Refresh.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Refresh.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Refresh.Image = (Image) manager.GetObject("tlb_M_Refresh.Image");
            this.tlb_M_Refresh.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Refresh.Name = "tlb_M_Refresh";
            this.tlb_M_Refresh.Size = new Size(0x23, 0x16);
            this.tlb_M_Refresh.Text = "刷新";
            this.tlb_M_Refresh.Click += new EventHandler(this.tlb_M_Refresh_Click);
            this.tlb_M_Find.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Find.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Find.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Find.Image = (Image) manager.GetObject("tlb_M_Find.Image");
            this.tlb_M_Find.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Find.Name = "tlb_M_Find";
            this.tlb_M_Find.Size = new Size(0x23, 0x16);
            this.tlb_M_Find.Text = "查找";
            this.tlb_M_Find.Visible = false;
            this.tlb_M_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Print.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Print.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Print.Image = (Image) manager.GetObject("tlb_M_Print.Image");
            this.tlb_M_Print.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Print.Name = "tlb_M_Print";
            this.tlb_M_Print.Size = new Size(0x23, 0x16);
            this.tlb_M_Print.Tag = "06";
            this.tlb_M_Print.Text = "打印";
            this.tlb_M_Print.ToolTipText = "打印条码";
            this.tlb_M_Print.Click += new EventHandler(this.tlb_M_Print_Click);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(6, 0x19);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new Size(6, 0x19);
            this.btn_M_Help.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btn_M_Help.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.btn_M_Help.ForeColor = SystemColors.ActiveCaption;
            this.btn_M_Help.Image = (Image) manager.GetObject("btn_M_Help.Image");
            this.btn_M_Help.ImageTransparentColor = Color.Magenta;
            this.btn_M_Help.Name = "btn_M_Help";
            this.btn_M_Help.Size = new Size(0x23, 0x16);
            this.btn_M_Help.Text = "帮助";
            this.btn_M_Help.Visible = false;
            this.tlb_M_Exit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlb_M_Exit.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.tlb_M_Exit.ForeColor = SystemColors.ActiveCaption;
            this.tlb_M_Exit.Image = (Image) manager.GetObject("tlb_M_Exit.Image");
            this.tlb_M_Exit.ImageTransparentColor = Color.Magenta;
            this.tlb_M_Exit.Name = "tlb_M_Exit";
            this.tlb_M_Exit.Size = new Size(0x23, 0x16);
            this.tlb_M_Exit.Text = "退出";
            this.tlb_M_Exit.Click += new EventHandler(this.tlb_M_Exit_Click);
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(6, 0x19);
            this.tlbSaveSysRts.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tlbSaveSysRts.Image = (Image) manager.GetObject("tlbSaveSysRts.Image");
            this.tlbSaveSysRts.ImageTransparentColor = Color.Magenta;
            this.tlbSaveSysRts.Name = "tlbSaveSysRts";
            this.tlbSaveSysRts.Size = new Size(0x51, 0x16);
            this.tlbSaveSysRts.Text = "保存系统权限";
            this.tlbSaveSysRts.Visible = false;
            this.tlbSaveSysRts.Click += new EventHandler(this.tlbSaveSysRts_Click);
            this.panel1.Controls.Add(this.btn_ReSet);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtQ_cRemark);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.cmbQ_IsRealPlt);
            this.panel1.Controls.Add(this.btn_Qry);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbQ_StoreStatus);
            this.panel1.Controls.Add(this.cmbQ_cPalletSpec);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_cPCIdQ);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x18d, 0x28b);
            this.panel1.TabIndex = 0x10;
            this.btn_ReSet.Location = new Point(0x146, 0x3b);
            this.btn_ReSet.Name = "btn_ReSet";
            this.btn_ReSet.Size = new Size(0x41, 0x17);
            this.btn_ReSet.TabIndex = 0x43;
            this.btn_ReSet.Text = "重置(&R)";
            this.btn_ReSet.UseVisualStyleBackColor = true;
            this.btn_ReSet.Click += new EventHandler(this.btn_ReSet_Click);
            this.label13.AutoSize = true;
            this.label13.Location = new Point(3, 0x3f);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x29, 12);
            this.label13.TabIndex = 0x42;
            this.label13.Text = "备注：";
            this.txtQ_cRemark.Location = new Point(0x3e, 0x3b);
            this.txtQ_cRemark.Name = "txtQ_cRemark";
            this.txtQ_cRemark.Size = new Size(0xbd, 0x15);
            this.txtQ_cRemark.TabIndex = 0x41;
            this.txtQ_cRemark.Tag = "0";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xd7, 0x25);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x35, 12);
            this.label12.TabIndex = 0x40;
            this.label12.Text = "是否实盘";
            this.cmbQ_IsRealPlt.FormattingEnabled = true;
            this.cmbQ_IsRealPlt.Items.AddRange(new object[] { "否", "是", "全部" });
            this.cmbQ_IsRealPlt.Location = new Point(0x112, 0x22);
            this.cmbQ_IsRealPlt.Name = "cmbQ_IsRealPlt";
            this.cmbQ_IsRealPlt.Size = new Size(0x3e, 20);
            this.cmbQ_IsRealPlt.TabIndex = 0x3f;
            this.cmbQ_IsRealPlt.Tag = "101";
            this.cmbQ_IsRealPlt.Text = "是";
            this.btn_Qry.Location = new Point(0x101, 60);
            this.btn_Qry.Name = "btn_Qry";
            this.btn_Qry.Size = new Size(0x41, 0x17);
            this.btn_Qry.TabIndex = 0x3e;
            this.btn_Qry.Text = "查询(Q)";
            this.btn_Qry.UseVisualStyleBackColor = true;
            this.btn_Qry.Click += new EventHandler(this.tlb_M_Refresh_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xda, 14);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 0x3d;
            this.label6.Text = "存储状态";
            this.cmbQ_StoreStatus.FormattingEnabled = true;
            this.cmbQ_StoreStatus.Location = new Point(0x112, 7);
            this.cmbQ_StoreStatus.Name = "cmbQ_StoreStatus";
            this.cmbQ_StoreStatus.Size = new Size(110, 20);
            this.cmbQ_StoreStatus.TabIndex = 60;
            this.cmbQ_StoreStatus.Tag = "101";
            this.cmbQ_StoreStatus.Text = "Bind SelectedValue";
            this.cmbQ_cPalletSpec.FormattingEnabled = true;
            this.cmbQ_cPalletSpec.Location = new Point(0x3e, 0x21);
            this.cmbQ_cPalletSpec.Name = "cmbQ_cPalletSpec";
            this.cmbQ_cPalletSpec.Size = new Size(0x84, 20);
            this.cmbQ_cPalletSpec.TabIndex = 0x3b;
            this.cmbQ_cPalletSpec.Tag = "101";
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.nPalletId, this.grdCol_bIsPrint, this.cPalletSpec, this.nType, this.cRemark, this.cPCId });
            this.dataGridView_Main.ContextMenuStrip = this.ppPrint;
            this.dataGridView_Main.Location = new Point(5, 0x59);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.RowHeadersVisible = false;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x17b, 0x224);
            this.dataGridView_Main.TabIndex = 9;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            this.ppPrint.Items.AddRange(new ToolStripItem[] { this.miPrintBC, this.mi_PrintPltRemark });
            this.ppPrint.Name = "ppPrint";
            this.ppPrint.Size = new Size(0xa7, 0x30);
            this.miPrintBC.Name = "miPrintBC";
            this.miPrintBC.Size = new Size(0xa6, 0x16);
            this.miPrintBC.Tag = "06";
            this.miPrintBC.Text = "打印条码";
            this.miPrintBC.Click += new EventHandler(this.miPrintBC_Click);
            this.mi_PrintPltRemark.Name = "mi_PrintPltRemark";
            this.mi_PrintPltRemark.Size = new Size(0xa6, 0x16);
            this.mi_PrintPltRemark.Text = "打印带备注的条码";
            this.mi_PrintPltRemark.Click += new EventHandler(this.mi_PrintPltRemark_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "托盘号码";
            this.textBox_cPCIdQ.Location = new Point(0x3e, 6);
            this.textBox_cPCIdQ.Name = "textBox_cPCIdQ";
            this.textBox_cPCIdQ.Size = new Size(0x84, 0x15);
            this.textBox_cPCIdQ.TabIndex = 7;
            this.textBox_cPCIdQ.Tag = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 0x24);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x1b;
            this.label5.Text = "托盘规格";
            this.panel_Edit.Controls.Add(this.cmb_bIsPrint);
            this.panel_Edit.Controls.Add(this.label10);
            this.panel_Edit.Controls.Add(this.cmb_bIsRealPlt);
            this.panel_Edit.Controls.Add(this.cmb_cPalletSpec);
            this.panel_Edit.Controls.Add(this.label11);
            this.panel_Edit.Controls.Add(this.label9);
            this.panel_Edit.Controls.Add(this.label8);
            this.panel_Edit.Controls.Add(this.label7);
            this.panel_Edit.Controls.Add(this.comboBox_nStatusStore);
            this.panel_Edit.Controls.Add(this.textBox_cRemark);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cPCId);
            this.panel_Edit.Controls.Add(this.textBox_nPalletId);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(0x18d, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x1d3, 0x28b);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.cmb_bIsPrint.FormattingEnabled = true;
            this.cmb_bIsPrint.Items.AddRange(new object[] { "否", "是" });
            this.cmb_bIsPrint.Location = new Point(0x74, 0x92);
            this.cmb_bIsPrint.Name = "cmb_bIsPrint";
            this.cmb_bIsPrint.Size = new Size(140, 20);
            this.cmb_bIsPrint.TabIndex = 3;
            this.cmb_bIsPrint.Tag = "101";
            this.cmb_bIsPrint.Text = "Bind SelectedValue";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x115, 0x1a);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 60;
            this.label10.Text = "是否实盘";
            this.cmb_bIsRealPlt.Enabled = false;
            this.cmb_bIsRealPlt.FormattingEnabled = true;
            this.cmb_bIsRealPlt.Location = new Point(0x150, 0x17);
            this.cmb_bIsRealPlt.Name = "cmb_bIsRealPlt";
            this.cmb_bIsRealPlt.Size = new Size(0x3e, 20);
            this.cmb_bIsRealPlt.TabIndex = 0x3b;
            this.cmb_bIsRealPlt.Tag = "101";
            this.cmb_bIsRealPlt.Text = "Bind SelectedValue";
            this.cmb_cPalletSpec.FormattingEnabled = true;
            this.cmb_cPalletSpec.Location = new Point(0x74, 0xb7);
            this.cmb_cPalletSpec.Name = "cmb_cPalletSpec";
            this.cmb_cPalletSpec.Size = new Size(140, 20);
            this.cmb_cPalletSpec.TabIndex = 4;
            this.cmb_cPalletSpec.Tag = "101";
            this.cmb_cPalletSpec.Text = "Bind SelectedValue";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x23, 0xba);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 12);
            this.label11.TabIndex = 0x39;
            this.label11.Text = "托盘规格";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x23, 0x97);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x41, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "是否已打印";
            this.label9.Click += new EventHandler(this.label9_Click);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x23, 0xeb);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x1d;
            this.label8.Text = "备    注";
            this.label8.Click += new EventHandler(this.label8_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x23, 0x70);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 0x1c;
            this.label7.Text = "存储状态";
            this.comboBox_nStatusStore.FormattingEnabled = true;
            this.comboBox_nStatusStore.Location = new Point(0x74, 0x68);
            this.comboBox_nStatusStore.Name = "comboBox_nStatusStore";
            this.comboBox_nStatusStore.Size = new Size(140, 20);
            this.comboBox_nStatusStore.TabIndex = 2;
            this.comboBox_nStatusStore.Tag = "101";
            this.comboBox_nStatusStore.Text = "Bind SelectedValue";
            this.textBox_cRemark.Location = new Point(0x74, 0xe2);
            this.textBox_cRemark.Name = "textBox_cRemark";
            this.textBox_cRemark.Size = new Size(140, 0x15);
            this.textBox_cRemark.TabIndex = 5;
            this.textBox_cRemark.Tag = "0";
            this.textBox_cRemark.ReadOnlyChanged += new EventHandler(this.textBox_cPCId_ReadOnlyChanged);
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x275);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x1d3, 0x16);
            this.stbMain.TabIndex = 0x10;
            this.stbMain.Text = "statusStrip1";
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new Size(0x23, 0x11);
            this.stbModul.Text = "模块:";
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new Size(0x2f, 0x11);
            this.stbUser.Text = "用户名:";
            this.stbState.Name = "stbState";
            this.stbState.Size = new Size(0x23, 0x11);
            this.stbState.Text = "状态:";
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new Size(0x23, 0x11);
            this.stbDateTime.Text = "时间:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x23, 0x47);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "托盘号码";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x23, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "托盘序号";
            this.textBox_cPCId.Location = new Point(0x74, 0x17);
            this.textBox_cPCId.Name = "textBox_cPCId";
            this.textBox_cPCId.ReadOnly = true;
            this.textBox_cPCId.Size = new Size(140, 0x15);
            this.textBox_cPCId.TabIndex = 7;
            this.textBox_cPCId.Tag = "0";
            this.textBox_cPCId.ReadOnlyChanged += new EventHandler(this.textBox_cPCId_ReadOnlyChanged);
            this.textBox_nPalletId.Location = new Point(0x74, 0x3e);
            this.textBox_nPalletId.Name = "textBox_nPalletId";
            this.textBox_nPalletId.Size = new Size(140, 0x15);
            this.textBox_nPalletId.TabIndex = 0;
            this.textBox_nPalletId.Tag = "0";
            this.textBox_nPalletId.ReadOnlyChanged += new EventHandler(this.textBox_cPCId_ReadOnlyChanged);
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.nPalletId.DataPropertyName = "nPalletId";
            this.nPalletId.HeaderText = "托盘号码";
            this.nPalletId.Name = "nPalletId";
            this.grdCol_bIsPrint.DataPropertyName = "cIsPrint";
            this.grdCol_bIsPrint.FillWeight = 65f;
            this.grdCol_bIsPrint.HeaderText = "是否已打印";
            this.grdCol_bIsPrint.Name = "grdCol_bIsPrint";
            this.grdCol_bIsPrint.Width = 0x41;
            this.cPalletSpec.DataPropertyName = "cPalletSpecDesc";
            this.cPalletSpec.HeaderText = "托盘规格";
            this.cPalletSpec.Name = "cPalletSpec";
            this.nType.DataPropertyName = "cStatusStore";
            this.nType.HeaderText = "存储状态";
            this.nType.Name = "nType";
            this.nType.Width = 0x41;
            this.cRemark.DataPropertyName = "cRemark";
            this.cRemark.HeaderText = "备注";
            this.cRemark.Name = "cRemark";
            this.cPCId.DataPropertyName = "cPCId";
            this.cPCId.HeaderText = "托盘序号";
            this.cPCId.Name = "cPCId";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x360, 0x2a4);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.KeyPreview = true;
            base.Name = "FrmStockPalletInfo";
            this.Text = "托盘管理";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            this.ppPrint.ResumeLayout(false);
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void LoadAreaList(string AreaId)
        {
            string sErr = "";
            string sSql = "select cAreaId,cAreaName,cWHId from TWC_WArea";
            if (AreaId.Trim() != "")
            {
                sSql = " where cAreaId='" + AreaId + "'";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(0, "空盘"));
            list.Add(new DictionaryEntry(1, "半盘"));
            list.Add(new DictionaryEntry(2, "满盘"));
            ArrayList list2 = (ArrayList) list.Clone();
            this.comboBox_nStatusStore.DisplayMember = "Value";
            this.comboBox_nStatusStore.ValueMember = "Key";
            this.comboBox_nStatusStore.DataSource = list;
            this.cmbQ_StoreStatus.DisplayMember = "Value";
            this.cmbQ_StoreStatus.ValueMember = "Key";
            this.cmbQ_StoreStatus.DataSource = list2;
            ArrayList list3 = new ArrayList();
            list3.Add(new DictionaryEntry(0, "否"));
            list3.Add(new DictionaryEntry(1, "是"));
            this.cmb_bIsRealPlt.DisplayMember = "Value";
            this.cmb_bIsRealPlt.ValueMember = "Key";
            this.cmb_bIsRealPlt.DataSource = list3;
            ArrayList list4 = new ArrayList();
            list4.Add(new DictionaryEntry(0, "否"));
            list4.Add(new DictionaryEntry(1, "是"));
            this.cmb_bIsPrint.DisplayMember = "Value";
            this.cmb_bIsPrint.ValueMember = "Key";
            this.cmb_bIsPrint.DataSource = list4;
        }

        private void LoadPalletSpecList()
        {
            string sErr = "";
            string sSql = "select * from twc_palletspec ";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cPalletSpec.DataSource = dataBySql.Tables["data"];
            this.cmb_cPalletSpec.DisplayMember = "cPalletSpec";
            this.cmb_cPalletSpec.ValueMember = "cPalletSpecId";
            DataTable table = dataBySql.Tables["data"].Copy();
            this.cmbQ_cPalletSpec.DataSource = table;
            this.cmbQ_cPalletSpec.DisplayMember = "cPalletSpec";
            this.cmbQ_cPalletSpec.ValueMember = "cPalletSpecId";
        }

        private void mi_PrintPltRemark_Click(object sender, EventArgs e)
        {
            string sValue = "";
            string str2 = "";
            string str3 = "0";
            string str4 = "1";
            XmlWriteReader reader = new XmlWriteReader();
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无托盘数据可打印条码！");
            }
            else
            {
                string sFile = Path.Combine(Application.StartupPath, base.AppInformation.AppConfigFile);
                reader.OpenXMLFile(sFile);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "RptName", out sValue);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "PrinterName", out str2);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "PrtMode", out str3);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "Copies", out str4);
                if (str3.Trim() == "")
                {
                    str3 = "0";
                }
                if (str4.Trim() == "")
                {
                    str4 = "2";
                }
                if (sValue.Trim() == "")
                {
                    sValue = Path.Combine(Application.StartupPath, @"rpt\rptPallet5-3.fr3");
                }
                else
                {
                    sValue = Path.Combine(Application.StartupPath, @"rpt\" + sValue);
                }
                string dllFile = Path.Combine(Application.StartupPath, "BarCodeReport.dll");
                bool bIsOK = false;
                foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                {
                    bIsOK = false;
                    string str7 = row.Cells["nPalletId"].Value.ToString().Trim();
                    string pltSpec = "";
                    if ((row.Cells["cPalletSpec"] != null) && (row.Cells["cPalletSpec"].Value != null))
                    {
                        pltSpec = this.GetPltSpec(row.Cells["cPalletSpec"].Value.ToString().Trim());
                    }
                    string str9 = "";
                    if ((row.Cells["cRemark"] != null) && (row.Cells["cRemark"].Value != null))
                    {
                        str9 = row.Cells["cRemark"].Value.ToString();
                    }
                    MyCallUnSafetyDll.DoCallMyDll(dllFile, "PrintBarCode_Ex", new object[] { sValue, str7, pltSpec, str9, str2, int.Parse(str3), int.Parse(str4) }, new System.Type[] { System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") }, new ModePass[] { ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue }, null, out bIsOK);
                    if (bIsOK)
                    {
                        this.UpdatePltIsPrinted(str7.Trim());
                    }
                }
                this.DoRefresh();
            }
        }

        private void miPrintBC_Click(object sender, EventArgs e)
        {
            this.tlb_M_Print_Click(null, null);
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PrintBarCode(DataGridView grd, string sFldBarCode, string rptName, string sPrinter)
        {
            string path = Application.StartupPath + @"\Reports\" + rptName;
            string str2 = Application.StartupPath + @"\BarCodeD7.dll";
            if (this.dataGridView_Main.SelectedRows != null)
            {
                if (this.dataGridView_Main.SelectedRows.Count == 0)
                {
                    MessageBox.Show("没有选择需要打印的数据！");
                }
                else
                {
                    IntPtr zero = IntPtr.Zero;
                    IntPtr hFun = IntPtr.Zero;
                    if (!File.Exists(str2))
                    {
                        MessageBox.Show("对不起，文件：" + str2 + "  不存在！");
                    }
                    else if (!File.Exists(path))
                    {
                        MessageBox.Show("对不起，文件：" + path + "  不存在！");
                    }
                    else
                    {
                        zero = new IntPtr(MyCallUnSafetyDll.LoadLibrary(str2));
                        if (zero == IntPtr.Zero)
                        {
                            MessageBox.Show("对不起，加载文件：" + path + "  时出错！");
                        }
                        else
                        {
                            hFun = new IntPtr(MyCallUnSafetyDll.GetProcAddress(zero.ToInt32(), "PrintBarCodeSingleValue"));
                            if (hFun == IntPtr.Zero)
                            {
                                MessageBox.Show("对不起，获取方法：PrintBarCodeSingleValue  时出错！");
                                MyCallUnSafetyDll.FreeLibrary(zero.ToInt32());
                                zero = IntPtr.Zero;
                            }
                            else
                            {
                                foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                                {
                                    string str3 = row.Cells[sFldBarCode].Value.ToString();
                                    if (str3.Trim() != "")
                                    {
                                        MyCallUnSafetyDll.Invoke(zero, hFun, "", new object[] { str3 }, new System.Type[] { typeof(string) }, new ModePass[] { ModePass.ByValue }, typeof(int));
                                    }
                                }
                                MyCallUnSafetyDll.FreeLibrary(zero.ToInt32());
                                zero = IntPtr.Zero;
                                hFun = IntPtr.Zero;
                            }
                        }
                    }
                }
            }
        }

        private void textBox_cPCId_ReadOnlyChanged(object sender, EventArgs e)
        {
            base.ChangeTextBoxBkColorByReadOnly(sender, this.panel_Edit.BackColor, Color.White);
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            this.DoEdit();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            frmNewPltId id = new frmNewPltId {
                AppInformation = base.AppInformation,
                UserInformation = base.UserInformation
            };
            id.ShowDialog();
            id.Dispose();
            this.DoRefresh();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            string sValue = "";
            string str2 = "";
            string str3 = "0";
            string str4 = "1";
            XmlWriteReader reader = new XmlWriteReader();
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无托盘数据可打印条码！");
            }
            else
            {
                string sFile = Path.Combine(Application.StartupPath, base.AppInformation.AppConfigFile);
                reader.OpenXMLFile(sFile);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "RptName", out sValue);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "PrinterName", out str2);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "PrtMode", out str3);
                reader.GetNodeAtrribeValue("config/printers/BarCode_PalletId", "Copies", out str4);
                if (str3.Trim() == "")
                {
                    str3 = "0";
                }
                if (str4.Trim() == "")
                {
                    str4 = "2";
                }
                if (sValue.Trim() == "")
                {
                    sValue = Path.Combine(Application.StartupPath, @"rpt\rptPallet5-3.fr3");
                }
                else
                {
                    sValue = Path.Combine(Application.StartupPath, @"rpt\" + sValue);
                }
                string dllFile = Path.Combine(Application.StartupPath, "BarCodeReport.dll");
                bool bIsOK = false;
                foreach (DataGridViewRow row in this.dataGridView_Main.SelectedRows)
                {
                    bIsOK = false;
                    string str7 = row.Cells["nPalletId"].Value.ToString().Trim();
                    string pltSpec = "";
                    if (row.Cells["cPalletSpec"] != null)
                    {
                        pltSpec = this.GetPltSpec(row.Cells["cPalletSpec"].Value.ToString().Trim());
                    }
                    MyCallUnSafetyDll.DoCallMyDll(dllFile, "PrintBarCode", new object[] { sValue, str7, pltSpec, str2, int.Parse(str3), int.Parse(str4) }, new System.Type[] { System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") }, new ModePass[] { ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue }, null, out bIsOK);
                    if (bIsOK)
                    {
                        this.UpdatePltIsPrinted(str7.Trim());
                    }
                }
                this.DoRefresh();
            }
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.DoRefresh();
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            this.DoSave();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            this.DoUndo();
        }

        private void tlbSaveSysRts_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in this.tlbMain.Items)
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

        private void UpdatePltIsPrinted(string sPltId)
        {
            string sSql = "update TWC_PalletCell set bIsPrint=1 where nPalletId='" + sPltId.Trim() + "'";
            string sErr = "";
            PubDBCommFuns.DoExecSql(base.AppInformation.SvrSocket, sSql, "", out sErr);
            if ((sErr.Trim() != "") && (sErr.Trim() != "0"))
            {
                MessageBox.Show(sErr);
            }
        }
    }
}

