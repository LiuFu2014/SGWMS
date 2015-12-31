namespace WareBaseMS
{
    using SunEast;
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

    public class FrmStockBoxInfo : FrmSTable
    {
        private bool bDSIsOpenForMain = false;
        private BindingSource bindingSource_Main;
        private DataGridViewTextBoxColumn bIsPrint;
        private ToolStripButton btn_M_Help;
        private DataGridViewTextBoxColumn cBoxId;
        private ComboBox cmb_cBoxType;
        private ComboBox cmbQ_cBoxType;
        private DataGridViewTextBoxColumn cName;
        private ComboBox comboBox_nStatusStore;
        private IContainer components = null;
        private DataGridView dataGridView_Main;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private DataGridViewTextBoxColumn nStatusStore;
        private OperateType OptMain = OperateType.optNone;
        private Panel panel_Edit;
        private Panel panel1;
        private StringBuilder sbCondition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strKeyFld = "cBoxId";
        private string strTbNameMain = "TWC_Box";
        private TextBox textBox_cBoxId;
        private TextBox textBox_cBoxIdQ;
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

        public FrmStockBoxInfo()
        {
            this.InitializeComponent();
        }

        private bool BandDataSet(string SqlStrConditon, DataGridView FDataGridView)
        {
            this.LoadBoxType();
            bool flag = true;
            string sSql = "";
            string sErr = "";
            this.bDSIsOpenForMain = false;
            FDataGridView.AutoGenerateColumns = false;
            FDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            sSql = "select * from " + this.strTbNameMain + " " + SqlStrConditon;
            base.DBDataSet = PubDBCommFuns.GetDataBySql(sSql, this.strTbNameMain, 0, 0, out sErr);
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
                catch (Exception exception)
                {
                    this.bDSIsOpenForMain = false;
                    MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
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
                }
                else
                {
                    MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                }
            }
            return flag;
        }

        private bool DoEdit()
        {
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("无可修改的数据！");
                return false;
            }
            this.OptMain = OperateType.optEdit;
            ((DataRowView) this.bindingSource_Main.Current).BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cBoxId.ReadOnly = true;
            return false;
        }

        public bool DoNew()
        {
            this.OptMain = OperateType.optNew;
            DataRowView view = (DataRowView) this.bindingSource_Main.AddNew();
            this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.DisplayState(this.stbState, this.OptMain);
            this.CtrlControlReadOnly(this.panel_Edit, true);
            this.textBox_cBoxId.ReadOnly = true;
            return false;
        }

        private bool DoPrint()
        {
            bool flag = false;
            if (this.bindingSource_Main.Count == 0)
            {
                MessageBox.Show("对不起，无数据可打印！");
                return false;
            }
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            string str = "";
            string sErr = "";
            string sValue = "";
            base.ReadConfigXMLValue(base.AppInformation.AppConfigFile, "config/printers", "BarCode_BoxId", "", "", out sValue);
            str = current["cBoxId"].ToString();
            if (flag)
            {
                PubDBCommFuns.GetDataBySql("update TWC_Box set bIsPrint=1 where cBoxId='" + str + "'", out sErr);
                if (sErr.Length > 0)
                {
                    MessageBox.Show(sErr);
                }
            }
            return flag;
        }

        private bool DoRefresh()
        {
            this.sbCondition.Remove(0, this.sbCondition.Length);
            this.sbCondition.Append(this.GetCondition());
            this.BandDataSet(this.sbCondition.ToString(), this.dataGridView_Main);
            return false;
        }

        private bool DoSave()
        {
            bool flag = false;
            string sSql = "";
            string sErr = "";
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current.IsEdit || current.IsNew)
            {
                this.UIToDataRowView(current, this.panel_Edit);
                if (current[this.strKeyFld].ToString() == "")
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, 7, "");
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, true);
                }
                else
                {
                    sSql = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, false);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                if (PubDBCommFuns.GetDataBySql(sSql, DBSQLCommandInfo.GetFieldsForDate(current), out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.OptMain = OperateType.optSave;
                    this.CtrlOptButtons(this.tlbMain, this.panel_Edit, this.OptMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.CtrlControlReadOnly(this.panel_Edit, false);
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OptMain = OperateType.optNone;
                    return flag;
                }
                MessageBox.Show("保存数据失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return flag;
            }
            MessageBox.Show("对不起，当前没有处于编辑状态！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return flag;
        }

        private bool DoUndo()
        {
            if (this.dataGridView_Main.RowCount < 2)
            {
                return true;
            }
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
            this.LoadBoxType();
            this.BandDataSet("", this.dataGridView_Main);
        }

        private string GetCondition()
        {
            StringBuilder builder = new StringBuilder(" where 1=1 ");
            if (this.cmbQ_cBoxType.Text.Trim() != "")
            {
                object selectedValue = this.cmbQ_cBoxType.SelectedValue;
                if (selectedValue != null)
                {
                    builder.Append(" and isnull(cBoxType,'')='" + selectedValue.ToString().Trim() + "'");
                }
            }
            if (this.textBox_cBoxIdQ.Text.Trim() != "")
            {
                builder.Append(" and cBoxId  like '%" + this.textBox_cBoxIdQ.Text.Trim() + "%'");
            }
            return builder.ToString();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmStockBoxInfo));
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
            this.groupBox1 = new GroupBox();
            this.cmbQ_cBoxType = new ComboBox();
            this.textBox_cBoxIdQ = new TextBox();
            this.label5 = new Label();
            this.label6 = new Label();
            this.dataGridView_Main = new DataGridView();
            this.cBoxId = new DataGridViewTextBoxColumn();
            this.cName = new DataGridViewTextBoxColumn();
            this.nStatusStore = new DataGridViewTextBoxColumn();
            this.bIsPrint = new DataGridViewTextBoxColumn();
            this.bindingSource_Main = new BindingSource(this.components);
            this.panel_Edit = new Panel();
            this.comboBox_nStatusStore = new ComboBox();
            this.cmb_cBoxType = new ComboBox();
            this.stbMain = new StatusStrip();
            this.stbModul = new ToolStripStatusLabel();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.stbDateTime = new ToolStripStatusLabel();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBox_cBoxId = new TextBox();
            this.tlbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView_Main).BeginInit();
            ((ISupportInitialize) this.bindingSource_Main).BeginInit();
            this.panel_Edit.SuspendLayout();
            this.stbMain.SuspendLayout();
            base.SuspendLayout();
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x309, 0x19);
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
            this.tlb_M_Find.Click += new EventHandler(this.tlb_M_Find_Click);
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
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dataGridView_Main);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x156, 0x1d0);
            this.panel1.TabIndex = 0x10;
            this.groupBox1.Controls.Add(this.cmbQ_cBoxType);
            this.groupBox1.Controls.Add(this.textBox_cBoxIdQ);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x14d, 0x36);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            this.cmbQ_cBoxType.FormattingEnabled = true;
            this.cmbQ_cBoxType.Location = new Point(0xd0, 13);
            this.cmbQ_cBoxType.Name = "cmbQ_cBoxType";
            this.cmbQ_cBoxType.Size = new Size(0x7c, 20);
            this.cmbQ_cBoxType.TabIndex = 60;
            this.cmbQ_cBoxType.Tag = "101";
            this.textBox_cBoxIdQ.Location = new Point(0x38, 13);
            this.textBox_cBoxIdQ.Name = "textBox_cBoxIdQ";
            this.textBox_cBoxIdQ.Size = new Size(100, 0x15);
            this.textBox_cBoxIdQ.TabIndex = 7;
            this.textBox_cBoxIdQ.Tag = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 0x16);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "周转箱号";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x9d, 0x13);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "箱体类型";
            this.label6.Click += new EventHandler(this.label2_Click);
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new DataGridViewColumn[] { this.cBoxId, this.cName, this.nStatusStore, this.bIsPrint });
            this.dataGridView_Main.Location = new Point(3, 0x41);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.ReadOnly = true;
            this.dataGridView_Main.RowTemplate.Height = 0x17;
            this.dataGridView_Main.Size = new Size(0x156, 0x19b);
            this.dataGridView_Main.TabIndex = 9;
            this.dataGridView_Main.Tag = "8";
            this.dataGridView_Main.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView_Main_CellContentClick);
            this.cBoxId.DataPropertyName = "cBoxId";
            this.cBoxId.HeaderText = "周转箱号";
            this.cBoxId.Name = "cBoxId";
            this.cBoxId.ReadOnly = true;
            this.cName.DataPropertyName = "cBoxType";
            this.cName.HeaderText = "箱体类型";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.nStatusStore.DataPropertyName = "nStatusStore";
            this.nStatusStore.HeaderText = "仓库类型";
            this.nStatusStore.Name = "nStatusStore";
            this.nStatusStore.ReadOnly = true;
            this.bIsPrint.DataPropertyName = "bIsPrint";
            this.bIsPrint.HeaderText = "是否打印";
            this.bIsPrint.Name = "bIsPrint";
            this.bIsPrint.ReadOnly = true;
            this.bIsPrint.Resizable = DataGridViewTriState.True;
            this.bindingSource_Main.PositionChanged += new EventHandler(this.bindingSource_Main_PositionChanged);
            this.panel_Edit.Controls.Add(this.comboBox_nStatusStore);
            this.panel_Edit.Controls.Add(this.cmb_cBoxType);
            this.panel_Edit.Controls.Add(this.stbMain);
            this.panel_Edit.Controls.Add(this.label3);
            this.panel_Edit.Controls.Add(this.label2);
            this.panel_Edit.Controls.Add(this.label1);
            this.panel_Edit.Controls.Add(this.textBox_cBoxId);
            this.panel_Edit.Dock = DockStyle.Fill;
            this.panel_Edit.Location = new Point(0x156, 0x19);
            this.panel_Edit.Name = "panel_Edit";
            this.panel_Edit.Size = new Size(0x1b3, 0x1d0);
            this.panel_Edit.TabIndex = 0x11;
            this.panel_Edit.Paint += new PaintEventHandler(this.panel_Edit_Paint);
            this.comboBox_nStatusStore.FormattingEnabled = true;
            this.comboBox_nStatusStore.Location = new Point(0x80, 0xeb);
            this.comboBox_nStatusStore.Name = "comboBox_nStatusStore";
            this.comboBox_nStatusStore.Size = new Size(0xae, 20);
            this.comboBox_nStatusStore.TabIndex = 60;
            this.comboBox_nStatusStore.Tag = "101";
            this.comboBox_nStatusStore.Text = "Bind SelectedValue";
            this.cmb_cBoxType.FormattingEnabled = true;
            this.cmb_cBoxType.Location = new Point(0x80, 190);
            this.cmb_cBoxType.Name = "cmb_cBoxType";
            this.cmb_cBoxType.Size = new Size(0xae, 20);
            this.cmb_cBoxType.TabIndex = 0x3b;
            this.cmb_cBoxType.Tag = "1";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x1ba);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x1b3, 0x16);
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
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x2f, 0xee);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "存货状态";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x2f, 0xc1);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "箱体类型";
            this.label2.Click += new EventHandler(this.label2_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2f, 0x94);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "周转箱号";
            this.textBox_cBoxId.Location = new Point(0x80, 0x8b);
            this.textBox_cBoxId.Name = "textBox_cBoxId";
            this.textBox_cBoxId.ReadOnly = true;
            this.textBox_cBoxId.Size = new Size(0xae, 0x15);
            this.textBox_cBoxId.TabIndex = 7;
            this.textBox_cBoxId.Tag = "0";
            this.textBox_cBoxId.ReadOnlyChanged += new EventHandler(this.textBox_cBoxId_ReadOnlyChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1e9);
            base.Controls.Add(this.panel_Edit);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.tlbMain);
            base.Name = "FrmStockBoxInfo";
            this.Text = "周转箱管理";
            base.Load += new EventHandler(this.FrmStockInfo_Load);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((ISupportInitialize) this.dataGridView_Main).EndInit();
            ((ISupportInitialize) this.bindingSource_Main).EndInit();
            this.panel_Edit.ResumeLayout(false);
            this.panel_Edit.PerformLayout();
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void LoadBoxType()
        {
            string sErr = "";
            string sSql = "select distinct cBoxType from twc_box ";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
                if (dataBySql != null)
                {
                    dataBySql.Clear();
                }
            }
            else
            {
                this.cmb_cBoxType.DataSource = dataBySql.Tables["data"];
                this.cmb_cBoxType.DisplayMember = "cBoxType";
                this.cmb_cBoxType.ValueMember = "cBoxType";
                DataTable table = null;
                table = dataBySql.Tables["data"].Copy();
                this.cmbQ_cBoxType.DataSource = table;
                this.cmbQ_cBoxType.DisplayMember = "cBoxType";
                this.cmbQ_cBoxType.ValueMember = "cBoxType";
            }
        }

        private void LoadCommboxItemByValue()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry(0, "无货"));
            list.Add(new DictionaryEntry(1, "半盘"));
            list.Add(new DictionaryEntry(2, "满盘"));
            this.comboBox_nStatusStore.DisplayMember = "Value";
            this.comboBox_nStatusStore.ValueMember = "Key";
            this.comboBox_nStatusStore.DataSource = list;
        }

        private void panel_Edit_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox_cBoxId_ReadOnlyChanged(object sender, EventArgs e)
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

        private void tlb_M_Find_Click(object sender, EventArgs e)
        {
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_CreateBoxNo :pQty ,:pNoLength,:pHeader,:pBoxType",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = "1",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pNoLength",
                ParameterValue = "7",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pHeader",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxType",
                ParameterValue = " ",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet dataSet = null;
            DataTable table = null;
            dataSet = client.GetDataSet(cmdInfo, out sErr);
            if (dataSet != null)
            {
                table = dataSet.Tables["data"];
                if (table != null)
                {
                    sErr = table.Rows[0]["cResult"].ToString();
                }
            }
            if (sErr.Length > 0)
            {
                MessageBox.Show(sErr);
            }
            dataSet.Clear();
        }

        private void tlb_M_Print_Click(object sender, EventArgs e)
        {
            DataRowView current = (DataRowView) this.bindingSource_Main.Current;
            if (current == null)
            {
                MessageBox.Show("无数据可打印条码！");
            }
            else
            {
                string str = current["nPalletId"].ToString().Trim();
                string str2 = Path.Combine(Application.StartupPath, @"rpt\rptMaterial5-3.fr3");
                string dllFile = Path.Combine(Application.StartupPath, "BarCodeReport.dll");
                string str4 = "";
                bool bIsOK = false;
                MyCallUnSafetyDll.DoCallMyDll(dllFile, "PrintBarCode", new object[] { str2, str, str4, 0 }, new System.Type[] { System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.String"), System.Type.GetType("System.Int32") }, new ModePass[] { ModePass.ByValue, ModePass.ByValue, ModePass.ByValue, ModePass.ByValue }, null, out bIsOK);
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
    }
}

