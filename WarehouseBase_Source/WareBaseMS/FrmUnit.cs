namespace WareBaseMS
{
    using SunEast;
    using SunEast.App;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using UI;
    using CommBase;
    using DBCommInfo;

    public class FrmUnit : FrmSTable
    {
        private BindingSource bdsMain;
        private bool bIsMainOpened = false;
        private ToolStripButton btn_M_Help;
        private ComboBox cmb_nUnitType;
        private DataGridViewTextBoxColumn colcUnitId;
        private IContainer components = null;
        private DataGridViewTextBoxColumn coscCName;
        private DataGridViewTextBoxColumn coscCreator;
        private DataGridViewTextBoxColumn coscEName;
        private DataGridViewTextBoxColumn cosdCreateDate;
        private DataGridViewTextBoxColumn cosnUnitType;
        public DataGridView grdList;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label label8;
        private OperateType optMain = OperateType.optNone;
        public Panel pnlEdit;
        public SplitContainer pnlSplit;
        private StringBuilder sbConndition = new StringBuilder("");
        public ToolStripStatusLabel stbDateTime;
        public StatusStrip stbMain;
        public ToolStripStatusLabel stbModul;
        public ToolStripStatusLabel stbState;
        public ToolStripStatusLabel stbUser;
        private string strConnFix = "";
        private string strKeyFld = "cUnitId";
        private string strTbNameMain = "TPC_Unit";
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
        public Timer tmrMain;
        public ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private TextBox txt_cCName;
        private TextBox txt_cCreator;
        private TextBox txt_cEName;
        private TextBox txt_cUnitId;
        private DateTimePicker txt_dCreateDate;

        public FrmUnit()
        {
            this.InitializeComponent();
        }

        public void BindDataSetToCtrls()
        {
            this.DataSetUnBind(this.pnlEdit);
            this.DataSetBind(this.pnlEdit, this.bdsMain);
            this.grdList.DataSource = null;
            this.grdList.DataSource = this.bdsMain;
        }

        private void DisplayState(ToolStripLabel stbSt, OperateType optX)
        {
            string str = "【状态】";
            if (stbSt != null)
            {
                switch (optX)
                {
                    case OperateType.optNew:
                        str = str + " 新建";
                        return;

                    case OperateType.optEdit:
                        str = str + " 修改";
                        return;
                }
                str = str + "    ";
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
            int optMain = -1;
            optMain = (int) this.optMain;
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current == null)
            {
                MessageBox.Show("对不起,无数据可删除!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((0 < optMain) && (optMain < 3))
            {
                MessageBox.Show("对不起,当前正处于编辑/新建状态,请先保存或取消操作!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (MessageBox.Show("系统将永久删除此数据，不能恢复，您确定要删除此数据吗？", "WMS", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
            {
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    SqlText = "delete " + this.strTbNameMain + " where " + this.strKeyFld + "='" + current[this.strKeyFld].ToString() + "'",
                    SqlType = SqlCommandType.sctSql,
                    PageIndex = 0,
                    PageSize = 0,
                    FromSysType = "dotnet",
                    DataTableName = this.strTbNameMain
                };
                SeDBClient client = new SeDBClient();
                string sErr = "";
                if (client.GetDataSet(cmdInfo, out sErr) != null)
                {
                    this.optMain = OperateType.optDelete;
                    this.OpenMainDataSet();
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.optMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, false);
                }
                else
                {
                    MessageBox.Show("对不起,删除操作失败!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        public void DoMEdit()
        {
            this.optMain = OperateType.optEdit;
            ((DataRowView) this.bdsMain.Current).BeginEdit();
            this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
            this.txt_cCName.Focus();
            this.DisplayState(this.stbState, this.optMain);
            this.CtrlControlReadOnly(this.pnlEdit, true);
            this.txt_cUnitId.ReadOnly = true;
        }

        public void DoMNew()
        {
            if (base.UserInformation.UType == UserType.utNormal)
            {
                MessageBox.Show("对不起，您无权限新建！");
            }
            else
            {
                this.optMain = OperateType.optNew;
                try
                {
                    DataRowView drv = (DataRowView) this.bdsMain.AddNew();
                    drv["cCreator"] = base.UserInformation.UserId;
                    drv["dCreateDate"] = DateTime.Today;
                    this.DataRowViewToUI(drv, this.pnlEdit);
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.txt_cCName.Focus();
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, true);
                    this.txt_cUnitId.ReadOnly = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void DoMSave()
        {
            string str = "";
            this.txt_cUnitId.Focus();
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current.IsEdit || current.IsNew)
            {
                this.UIToDataRowView(current, this.pnlEdit);
                if ((current[this.strKeyFld].ToString() == "") || (current[this.strKeyFld].ToString() == "-1"))
                {
                    current[this.strKeyFld] = PubDBCommFuns.GetNewId(this.strTbNameMain, this.strKeyFld, base.UserInformation.UnitId.Trim().Length + 4, base.UserInformation.UnitId.Trim());
                    str = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, true);
                }
                else
                {
                    str = DBSQLCommandInfo.GetSQLByDataRow(current, this.strTbNameMain, this.strKeyFld, false);
                }
                if (current.IsEdit)
                {
                    current.EndEdit();
                }
                DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                    SqlText = str,
                    FldsData = DBSQLCommandInfo.GetFieldsForDate(current),
                    SqlType = SqlCommandType.sctSql,
                    PageIndex = 0,
                    PageSize = 0,
                    FromSysType = "dotnet"
                };
                SeDBClient client = new SeDBClient();
                string sErr = "";
                if (client.GetDataSet(cmdInfo, out sErr).Tables[0].Rows[0][0].ToString() == "0")
                {
                    this.optMain = OperateType.optSave;
                    MessageBox.Show("保存数据成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.OpenMainDataSet();
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.optMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, false);
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
        }

        public void DoMUndo()
        {
            this.optMain = OperateType.optUndo;
            DataRowView current = (DataRowView) this.bdsMain.Current;
            if (current != null)
            {
                if (current.IsEdit)
                {
                    current.CancelEdit();
                }
                if (current.IsNew)
                {
                    current.Delete();
                }
                base.DBDataSet.Tables[this.strTbNameMain].AcceptChanges();
                current = (DataRowView) this.bdsMain.Current;
                if (current != null)
                {
                    this.DataRowViewToUI(current, this.pnlEdit);
                    this.CtrlOptButtons(this.tlbMain, this.pnlEdit, this.optMain, base.DBDataSet.Tables[this.strTbNameMain]);
                    this.optMain = OperateType.optNone;
                    this.DisplayState(this.stbState, this.optMain);
                    this.CtrlControlReadOnly(this.pnlEdit, false);
                }
            }
        }

        private void FrmUnit_Load(object sender, EventArgs e)
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
            this.InitCmb();
            this.OpenMainDataSet();
        }

        public void InitCmb()
        {
            ArrayList list = new ArrayList();
            list.Add(new DictionaryEntry("0", "计量单位"));
            list.Add(new DictionaryEntry("1", "长度单位"));
            list.Add(new DictionaryEntry("2", "重量单位"));
            list.Add(new DictionaryEntry("3", "体积单位"));
            list.Add(new DictionaryEntry("4", "货币单位"));
            this.cmb_nUnitType.DataSource = list;
            this.cmb_nUnitType.DisplayMember = "Value";
            this.cmb_nUnitType.ValueMember = "Key";
        }

        public override void InitFormParameters()
        {
            if (base.ModuleRtsId.Length > 0)
            {
                this.Text = base.ModuleRtsName;
            }
            this.stbModul.Text = "模块【" + this.Text + "】";
            if (base.UserInformation != null)
            {
                this.stbUser.Text = "用户【" + base.UserInformation.UserName + "】";
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmUnit));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.tlb_M_Delete = new ToolStripButton();
            this.stbDateTime = new ToolStripStatusLabel();
            this.stbModul = new ToolStripStatusLabel();
            this.stbMain = new StatusStrip();
            this.stbUser = new ToolStripStatusLabel();
            this.stbState = new ToolStripStatusLabel();
            this.label8 = new Label();
            this.txt_cCreator = new TextBox();
            this.pnlEdit = new Panel();
            this.txt_dCreateDate = new DateTimePicker();
            this.cmb_nUnitType = new ComboBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.txt_cEName = new TextBox();
            this.label4 = new Label();
            this.txt_cCName = new TextBox();
            this.label3 = new Label();
            this.txt_cUnitId = new TextBox();
            this.label2 = new Label();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tlbMain = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.tlb_M_New = new ToolStripButton();
            this.tlb_M_Edit = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.tlb_M_Undo = new ToolStripButton();
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
            this.tmrMain = new Timer(this.components);
            this.grdList = new DataGridView();
            this.colcUnitId = new DataGridViewTextBoxColumn();
            this.coscCName = new DataGridViewTextBoxColumn();
            this.coscEName = new DataGridViewTextBoxColumn();
            this.cosnUnitType = new DataGridViewTextBoxColumn();
            this.coscCreator = new DataGridViewTextBoxColumn();
            this.cosdCreateDate = new DataGridViewTextBoxColumn();
            this.pnlSplit = new SplitContainer();
            this.bdsMain = new BindingSource(this.components);
            this.stbMain.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.tlbMain.SuspendLayout();
            ((ISupportInitialize) this.grdList).BeginInit();
            this.pnlSplit.Panel1.SuspendLayout();
            this.pnlSplit.Panel2.SuspendLayout();
            this.pnlSplit.SuspendLayout();
            ((ISupportInitialize) this.bdsMain).BeginInit();
            base.SuspendLayout();
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
            this.stbDateTime.Name = "stbDateTime";
            this.stbDateTime.Size = new Size(0x23, 0x11);
            this.stbDateTime.Text = "时间:";
            this.stbModul.Name = "stbModul";
            this.stbModul.Size = new Size(0x23, 0x11);
            this.stbModul.Text = "模块:";
            this.stbMain.Items.AddRange(new ToolStripItem[] { this.stbModul, this.stbUser, this.stbState, this.stbDateTime });
            this.stbMain.Location = new Point(0, 0x1d3);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new Size(0x309, 0x16);
            this.stbMain.TabIndex = 0x12;
            this.stbMain.Text = "statusStrip1";
            this.stbUser.Name = "stbUser";
            this.stbUser.Size = new Size(0x2f, 0x11);
            this.stbUser.Text = "用户名:";
            this.stbState.Name = "stbState";
            this.stbState.Size = new Size(0x23, 0x11);
            this.stbState.Text = "状态:";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x25, 0xc1);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x41, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "创建日期：";
            this.txt_cCreator.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cCreator.Location = new Point(0x62, 160);
            this.txt_cCreator.Name = "txt_cCreator";
            this.txt_cCreator.ReadOnly = true;
            this.txt_cCreator.Size = new Size(0x126, 0x15);
            this.txt_cCreator.TabIndex = 6;
            this.txt_cCreator.Tag = "0";
            this.pnlEdit.BackColor = SystemColors.Info;
            this.pnlEdit.Controls.Add(this.txt_dCreateDate);
            this.pnlEdit.Controls.Add(this.cmb_nUnitType);
            this.pnlEdit.Controls.Add(this.label8);
            this.pnlEdit.Controls.Add(this.txt_cCreator);
            this.pnlEdit.Controls.Add(this.label7);
            this.pnlEdit.Controls.Add(this.label6);
            this.pnlEdit.Controls.Add(this.txt_cEName);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.txt_cCName);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.txt_cUnitId);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Location = new Point(20, 0x62);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new Size(500, 0x126);
            this.pnlEdit.TabIndex = 0;
            this.txt_dCreateDate.Location = new Point(0x62, 0xbd);
            this.txt_dCreateDate.Name = "txt_dCreateDate";
            this.txt_dCreateDate.Size = new Size(0x126, 0x15);
            this.txt_dCreateDate.TabIndex = 14;
            this.cmb_nUnitType.BackColor = SystemColors.Control;
            this.cmb_nUnitType.FormattingEnabled = true;
            this.cmb_nUnitType.Location = new Point(0x62, 130);
            this.cmb_nUnitType.Name = "cmb_nUnitType";
            this.cmb_nUnitType.Size = new Size(0x126, 20);
            this.cmb_nUnitType.TabIndex = 13;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x25, 0xa3);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x41, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "创 建 人：";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x25, 0x85);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "单位类别：";
            this.txt_cEName.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cEName.Location = new Point(0x62, 0x61);
            this.txt_cEName.Name = "txt_cEName";
            this.txt_cEName.ReadOnly = true;
            this.txt_cEName.Size = new Size(0x126, 0x15);
            this.txt_cEName.TabIndex = 3;
            this.txt_cEName.Tag = "0";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x25, 100);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "英文标志：";
            this.txt_cCName.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cCName.Location = new Point(0x62, 0x44);
            this.txt_cCName.Name = "txt_cCName";
            this.txt_cCName.ReadOnly = true;
            this.txt_cCName.Size = new Size(0x126, 0x15);
            this.txt_cCName.TabIndex = 2;
            this.txt_cCName.Tag = "0";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x25, 70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "中文标识：";
            this.txt_cUnitId.BorderStyle = BorderStyle.FixedSingle;
            this.txt_cUnitId.Location = new Point(0x62, 0x29);
            this.txt_cUnitId.Name = "txt_cUnitId";
            this.txt_cUnitId.ReadOnly = true;
            this.txt_cUnitId.Size = new Size(0x126, 0x15);
            this.txt_cUnitId.TabIndex = 1;
            this.txt_cUnitId.Tag = "0";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x25, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "单位编号：";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.tlbMain.Items.AddRange(new ToolStripItem[] { 
                this.toolStripLabel1, this.toolStripSeparator2, this.toolStripSeparator1, this.tlb_M_New, this.tlb_M_Edit, this.toolStripSeparator3, this.tlb_M_Undo, this.tlb_M_Delete, this.toolStripSeparator4, this.tlb_M_Save, this.toolStripSeparator5, this.tlb_M_Refresh, this.tlb_M_Find, this.tlb_M_Print, this.toolStripSeparator6, this.toolStripSeparator7, 
                this.btn_M_Help, this.tlb_M_Exit, this.toolStripSeparator8, this.tlbSaveSysRts
             });
            this.tlbMain.Location = new Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new Size(0x309, 0x19);
            this.tlbMain.TabIndex = 0x13;
            this.tlbMain.Text = "toolStrip1";
            this.toolStripLabel1.Font = new Font("宋体", 9f, FontStyle.Bold);
            this.toolStripLabel1.ForeColor = SystemColors.ActiveCaption;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0, 0x16);
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
            this.tlb_M_Print.Visible = false;
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
            this.tmrMain.Enabled = true;
            this.tmrMain.Interval = 0x1388;
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new DataGridViewColumn[] { this.colcUnitId, this.coscCName, this.coscEName, this.cosnUnitType, this.coscCreator, this.cosdCreateDate });
            this.grdList.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.grdList.Location = new Point(3, 0x1c);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = style;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 0x17;
            this.grdList.Size = new Size(0xeb, 0x1a6);
            this.grdList.TabIndex = 1;
            this.grdList.Tag = "8";
            this.colcUnitId.DataPropertyName = "cUnitId";
            this.colcUnitId.Frozen = true;
            this.colcUnitId.HeaderText = "单位编号";
            this.colcUnitId.Name = "colcUnitId";
            this.colcUnitId.ReadOnly = true;
            this.coscCName.DataPropertyName = "cCName";
            this.coscCName.HeaderText = "中文名称";
            this.coscCName.Name = "coscCName";
            this.coscCName.ReadOnly = true;
            this.coscEName.DataPropertyName = "cEName";
            this.coscEName.HeaderText = "英文名称";
            this.coscEName.Name = "coscEName";
            this.coscEName.ReadOnly = true;
            this.cosnUnitType.DataPropertyName = "nUnitType";
            this.cosnUnitType.HeaderText = "单位类别";
            this.cosnUnitType.Name = "cosnUnitType";
            this.cosnUnitType.ReadOnly = true;
            this.coscCreator.DataPropertyName = "cCreator";
            this.coscCreator.HeaderText = "创建人";
            this.coscCreator.Name = "coscCreator";
            this.coscCreator.ReadOnly = true;
            this.cosdCreateDate.DataPropertyName = "dCreateDate";
            this.cosdCreateDate.HeaderText = "创建日期";
            this.cosdCreateDate.Name = "cosdCreateDate";
            this.cosdCreateDate.ReadOnly = true;
            this.pnlSplit.Dock = DockStyle.Fill;
            this.pnlSplit.Location = new Point(0, 0);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Panel1.Controls.Add(this.grdList);
            this.pnlSplit.Panel2.Controls.Add(this.pnlEdit);
            this.pnlSplit.Panel2.ImeMode = ImeMode.NoControl;
            this.pnlSplit.Size = new Size(0x309, 0x1e9);
            this.pnlSplit.SplitterDistance = 0xf1;
            this.pnlSplit.TabIndex = 20;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x309, 0x1e9);
            base.Controls.Add(this.stbMain);
            base.Controls.Add(this.tlbMain);
            base.Controls.Add(this.pnlSplit);
            base.Name = "FrmUnit";
            base.Load += new EventHandler(this.FrmUnit_Load);
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((ISupportInitialize) this.grdList).EndInit();
            this.pnlSplit.Panel1.ResumeLayout(false);
            this.pnlSplit.Panel2.ResumeLayout(false);
            this.pnlSplit.ResumeLayout(false);
            ((ISupportInitialize) this.bdsMain).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public bool OpenMainDataSet()
        {
            bool flag = false;
            this.grdList.AutoGenerateColumns = false;
            this.grdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.DBDataSet.Clear();
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "select * from " + this.strTbNameMain + " where 1=1 ",
                SqlType = SqlCommandType.sctSql,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet",
                DataTableName = this.strTbNameMain
            };
            SeDBClient client = new SeDBClient();
            string sErr = "";
            base.DBDataSet = client.GetDataSet(cmdInfo, out sErr);
            flag = base.DBDataSet != null;
            if (!flag)
            {
                MessageBox.Show(sErr);
                return flag;
            }
            try
            {
                this.ClearUIValues(this.pnlEdit);
                this.bIsMainOpened = false;
                this.bdsMain.DataSource = base.DBDataSet.Tables[this.strTbNameMain];
                this.bIsMainOpened = true;
                this.BindDataSetToCtrls();
                this.grdList.DataSource = this.bdsMain;
                flag = true;
                this.optMain = OperateType.optNone;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            return flag;
        }

        private void tlb_M_Delete_Click(object sender, EventArgs e)
        {
            this.DoMDelete();
        }

        private void tlb_M_Edit_Click(object sender, EventArgs e)
        {
            this.DoMEdit();
        }

        private void tlb_M_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tlb_M_New_Click(object sender, EventArgs e)
        {
            this.DoMNew();
        }

        private void tlb_M_Refresh_Click(object sender, EventArgs e)
        {
            this.OpenMainDataSet();
        }

        private void tlb_M_Save_Click(object sender, EventArgs e)
        {
            this.DoMSave();
        }

        private void tlb_M_Undo_Click(object sender, EventArgs e)
        {
            this.DoMUndo();
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

