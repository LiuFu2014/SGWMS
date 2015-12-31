namespace UserMS
{
    partial class frmUserRight
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdUser = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.prgRTS = new System.Windows.Forms.ProgressBar();
            this.trvRight = new System.Windows.Forms.TreeView();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Right = new System.Windows.Forms.Button();
            this.btn_SaveRTS = new System.Windows.Forms.Button();
            this.chk_SelAll = new System.Windows.Forms.CheckBox();
            this.bdsUser = new System.Windows.Forms.BindingSource(this.components);
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpFun = new System.Windows.Forms.TabPage();
            this.tbpUserWHouse = new System.Windows.Forms.TabPage();
            this.prgUW = new System.Windows.Forms.ProgressBar();
            this.trvUWRights = new System.Windows.Forms.TreeView();
            this.btn_UWCancel = new System.Windows.Forms.Button();
            this.btnUWClose = new System.Windows.Forms.Button();
            this.btn_UWRights = new System.Windows.Forms.Button();
            this.btnUWSave = new System.Windows.Forms.Button();
            this.chkUWCheckAll = new System.Windows.Forms.CheckBox();
            this.tbp_UserMgrArea = new System.Windows.Forms.TabPage();
            this.prg_UMA = new System.Windows.Forms.ProgressBar();
            this.trv_MgrArea = new System.Windows.Forms.TreeView();
            this.btn_UMAUndo = new System.Windows.Forms.Button();
            this.btn_UMAClose = new System.Windows.Forms.Button();
            this.btn_UMARights = new System.Windows.Forms.Button();
            this.btn_UMASave = new System.Windows.Forms.Button();
            this.chk_UMAAll = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colcUserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcLinkID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colcDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUser)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsUser)).BeginInit();
            this.tbcMain.SuspendLayout();
            this.tbpFun.SuspendLayout();
            this.tbpUserWHouse.SuspendLayout();
            this.tbp_UserMgrArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdUser);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 518);
            this.panel2.TabIndex = 11;
            // 
            // grdUser
            // 
            this.grdUser.AllowUserToAddRows = false;
            this.grdUser.AllowUserToDeleteRows = false;
            this.grdUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colcUserId,
            this.colcName,
            this.colcLinkID,
            this.colcDept});
            this.grdUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUser.Location = new System.Drawing.Point(0, 50);
            this.grdUser.MultiSelect = false;
            this.grdUser.Name = "grdUser";
            this.grdUser.ReadOnly = true;
            this.grdUser.RowHeadersVisible = false;
            this.grdUser.RowTemplate.Height = 23;
            this.grdUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdUser.Size = new System.Drawing.Size(345, 468);
            this.grdUser.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.prgRTS);
            this.panel3.Controls.Add(this.trvRight);
            this.panel3.Controls.Add(this.btn_Cancel);
            this.panel3.Controls.Add(this.btn_Close);
            this.panel3.Controls.Add(this.btn_Right);
            this.panel3.Controls.Add(this.btn_SaveRTS);
            this.panel3.Controls.Add(this.chk_SelAll);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(563, 486);
            this.panel3.TabIndex = 12;
            // 
            // prgRTS
            // 
            this.prgRTS.Location = new System.Drawing.Point(27, 454);
            this.prgRTS.Name = "prgRTS";
            this.prgRTS.Size = new System.Drawing.Size(530, 23);
            this.prgRTS.TabIndex = 11;
            this.prgRTS.Visible = false;
            // 
            // trvRight
            // 
            this.trvRight.Location = new System.Drawing.Point(27, 46);
            this.trvRight.Name = "trvRight";
            this.trvRight.Size = new System.Drawing.Size(530, 390);
            this.trvRight.TabIndex = 10;
            this.trvRight.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvRight_AfterCheck);
            this.trvRight.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvRight_BeforeCollapse);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(127, 17);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "取消设定";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(451, 17);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "退出";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Right
            // 
            this.btn_Right.Location = new System.Drawing.Point(27, 17);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(75, 23);
            this.btn_Right.TabIndex = 5;
            this.btn_Right.Text = "设置权限";
            this.btn_Right.UseVisualStyleBackColor = true;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            // 
            // btn_SaveRTS
            // 
            this.btn_SaveRTS.Location = new System.Drawing.Point(291, 17);
            this.btn_SaveRTS.Name = "btn_SaveRTS";
            this.btn_SaveRTS.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveRTS.TabIndex = 8;
            this.btn_SaveRTS.Text = "保存权限";
            this.btn_SaveRTS.UseVisualStyleBackColor = true;
            this.btn_SaveRTS.Click += new System.EventHandler(this.btn_SaveRTS_Click);
            // 
            // chk_SelAll
            // 
            this.chk_SelAll.AutoSize = true;
            this.chk_SelAll.Location = new System.Drawing.Point(223, 21);
            this.chk_SelAll.Name = "chk_SelAll";
            this.chk_SelAll.Size = new System.Drawing.Size(48, 16);
            this.chk_SelAll.TabIndex = 6;
            this.chk_SelAll.Text = "全部";
            this.chk_SelAll.UseVisualStyleBackColor = true;
            this.chk_SelAll.Click += new System.EventHandler(this.chk_SelAll_Click);
            // 
            // bdsUser
            // 
            this.bdsUser.PositionChanged += new System.EventHandler(this.bdsUser_PositionChanged);
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpFun);
            this.tbcMain.Controls.Add(this.tbpUserWHouse);
            this.tbcMain.Controls.Add(this.tbp_UserMgrArea);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbcMain.Location = new System.Drawing.Point(347, 0);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(577, 518);
            this.tbcMain.TabIndex = 13;
            this.tbcMain.TabIndexChanged += new System.EventHandler(this.tbcMain_TabIndexChanged);
            this.tbcMain.SelectedIndexChanged += new System.EventHandler(this.tbcMain_SelectedIndexChanged);
            // 
            // tbpFun
            // 
            this.tbpFun.Controls.Add(this.panel3);
            this.tbpFun.Location = new System.Drawing.Point(4, 22);
            this.tbpFun.Name = "tbpFun";
            this.tbpFun.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFun.Size = new System.Drawing.Size(569, 492);
            this.tbpFun.TabIndex = 0;
            this.tbpFun.Text = "操作权限";
            this.tbpFun.UseVisualStyleBackColor = true;
            // 
            // tbpUserWHouse
            // 
            this.tbpUserWHouse.Controls.Add(this.prgUW);
            this.tbpUserWHouse.Controls.Add(this.trvUWRights);
            this.tbpUserWHouse.Controls.Add(this.btn_UWCancel);
            this.tbpUserWHouse.Controls.Add(this.btnUWClose);
            this.tbpUserWHouse.Controls.Add(this.btn_UWRights);
            this.tbpUserWHouse.Controls.Add(this.btnUWSave);
            this.tbpUserWHouse.Controls.Add(this.chkUWCheckAll);
            this.tbpUserWHouse.Location = new System.Drawing.Point(4, 22);
            this.tbpUserWHouse.Name = "tbpUserWHouse";
            this.tbpUserWHouse.Padding = new System.Windows.Forms.Padding(3);
            this.tbpUserWHouse.Size = new System.Drawing.Size(569, 492);
            this.tbpUserWHouse.TabIndex = 1;
            this.tbpUserWHouse.Text = "仓库权限";
            this.tbpUserWHouse.UseVisualStyleBackColor = true;
            // 
            // prgUW
            // 
            this.prgUW.Location = new System.Drawing.Point(21, 330);
            this.prgUW.Name = "prgUW";
            this.prgUW.Size = new System.Drawing.Size(474, 23);
            this.prgUW.TabIndex = 16;
            this.prgUW.Visible = false;
            // 
            // trvUWRights
            // 
            this.trvUWRights.Location = new System.Drawing.Point(21, 48);
            this.trvUWRights.Name = "trvUWRights";
            this.trvUWRights.Size = new System.Drawing.Size(499, 261);
            this.trvUWRights.TabIndex = 15;
            // 
            // btn_UWCancel
            // 
            this.btn_UWCancel.Location = new System.Drawing.Point(121, 19);
            this.btn_UWCancel.Name = "btn_UWCancel";
            this.btn_UWCancel.Size = new System.Drawing.Size(75, 23);
            this.btn_UWCancel.TabIndex = 14;
            this.btn_UWCancel.Text = "取消设定";
            this.btn_UWCancel.UseVisualStyleBackColor = true;
            this.btn_UWCancel.Click += new System.EventHandler(this.btn_UWCancel_Click);
            // 
            // btnUWClose
            // 
            this.btnUWClose.Location = new System.Drawing.Point(445, 19);
            this.btnUWClose.Name = "btnUWClose";
            this.btnUWClose.Size = new System.Drawing.Size(75, 23);
            this.btnUWClose.TabIndex = 12;
            this.btnUWClose.Text = "退出";
            this.btnUWClose.UseVisualStyleBackColor = true;
            this.btnUWClose.Click += new System.EventHandler(this.btnUWClose_Click);
            // 
            // btn_UWRights
            // 
            this.btn_UWRights.Location = new System.Drawing.Point(21, 19);
            this.btn_UWRights.Name = "btn_UWRights";
            this.btn_UWRights.Size = new System.Drawing.Size(75, 23);
            this.btn_UWRights.TabIndex = 10;
            this.btn_UWRights.Text = "设置权限";
            this.btn_UWRights.UseVisualStyleBackColor = true;
            this.btn_UWRights.Click += new System.EventHandler(this.btn_UWRights_Click);
            // 
            // btnUWSave
            // 
            this.btnUWSave.Location = new System.Drawing.Point(285, 19);
            this.btnUWSave.Name = "btnUWSave";
            this.btnUWSave.Size = new System.Drawing.Size(75, 23);
            this.btnUWSave.TabIndex = 13;
            this.btnUWSave.Text = "保存权限";
            this.btnUWSave.UseVisualStyleBackColor = true;
            this.btnUWSave.Click += new System.EventHandler(this.btnUWSave_Click);
            // 
            // chkUWCheckAll
            // 
            this.chkUWCheckAll.AutoSize = true;
            this.chkUWCheckAll.Location = new System.Drawing.Point(217, 23);
            this.chkUWCheckAll.Name = "chkUWCheckAll";
            this.chkUWCheckAll.Size = new System.Drawing.Size(48, 16);
            this.chkUWCheckAll.TabIndex = 11;
            this.chkUWCheckAll.Text = "全部";
            this.chkUWCheckAll.UseVisualStyleBackColor = true;
            this.chkUWCheckAll.Click += new System.EventHandler(this.chkUWCheckAll_Click);
            // 
            // tbp_UserMgrArea
            // 
            this.tbp_UserMgrArea.Controls.Add(this.prg_UMA);
            this.tbp_UserMgrArea.Controls.Add(this.trv_MgrArea);
            this.tbp_UserMgrArea.Controls.Add(this.btn_UMAUndo);
            this.tbp_UserMgrArea.Controls.Add(this.btn_UMAClose);
            this.tbp_UserMgrArea.Controls.Add(this.btn_UMARights);
            this.tbp_UserMgrArea.Controls.Add(this.btn_UMASave);
            this.tbp_UserMgrArea.Controls.Add(this.chk_UMAAll);
            this.tbp_UserMgrArea.Location = new System.Drawing.Point(4, 22);
            this.tbp_UserMgrArea.Name = "tbp_UserMgrArea";
            this.tbp_UserMgrArea.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_UserMgrArea.Size = new System.Drawing.Size(569, 492);
            this.tbp_UserMgrArea.TabIndex = 3;
            this.tbp_UserMgrArea.Text = "管理货区权限";
            this.tbp_UserMgrArea.UseVisualStyleBackColor = true;
            // 
            // prg_UMA
            // 
            this.prg_UMA.Location = new System.Drawing.Point(40, 336);
            this.prg_UMA.Name = "prg_UMA";
            this.prg_UMA.Size = new System.Drawing.Size(474, 23);
            this.prg_UMA.TabIndex = 23;
            this.prg_UMA.Visible = false;
            // 
            // trv_MgrArea
            // 
            this.trv_MgrArea.Location = new System.Drawing.Point(40, 54);
            this.trv_MgrArea.Name = "trv_MgrArea";
            this.trv_MgrArea.Size = new System.Drawing.Size(499, 261);
            this.trv_MgrArea.TabIndex = 22;
            // 
            // btn_UMAUndo
            // 
            this.btn_UMAUndo.Enabled = false;
            this.btn_UMAUndo.Location = new System.Drawing.Point(140, 25);
            this.btn_UMAUndo.Name = "btn_UMAUndo";
            this.btn_UMAUndo.Size = new System.Drawing.Size(75, 23);
            this.btn_UMAUndo.TabIndex = 21;
            this.btn_UMAUndo.Text = "取消设定";
            this.btn_UMAUndo.UseVisualStyleBackColor = true;
            this.btn_UMAUndo.Click += new System.EventHandler(this.btn_UMAUndo_Click);
            // 
            // btn_UMAClose
            // 
            this.btn_UMAClose.Location = new System.Drawing.Point(464, 25);
            this.btn_UMAClose.Name = "btn_UMAClose";
            this.btn_UMAClose.Size = new System.Drawing.Size(75, 23);
            this.btn_UMAClose.TabIndex = 19;
            this.btn_UMAClose.Text = "退出";
            this.btn_UMAClose.UseVisualStyleBackColor = true;
            // 
            // btn_UMARights
            // 
            this.btn_UMARights.Location = new System.Drawing.Point(40, 25);
            this.btn_UMARights.Name = "btn_UMARights";
            this.btn_UMARights.Size = new System.Drawing.Size(75, 23);
            this.btn_UMARights.TabIndex = 17;
            this.btn_UMARights.Text = "设置权限";
            this.btn_UMARights.UseVisualStyleBackColor = true;
            this.btn_UMARights.Click += new System.EventHandler(this.btn_UMARights_Click);
            // 
            // btn_UMASave
            // 
            this.btn_UMASave.Enabled = false;
            this.btn_UMASave.Location = new System.Drawing.Point(304, 25);
            this.btn_UMASave.Name = "btn_UMASave";
            this.btn_UMASave.Size = new System.Drawing.Size(75, 23);
            this.btn_UMASave.TabIndex = 20;
            this.btn_UMASave.Text = "保存权限";
            this.btn_UMASave.UseVisualStyleBackColor = true;
            this.btn_UMASave.Click += new System.EventHandler(this.btn_UMASave_Click);
            // 
            // chk_UMAAll
            // 
            this.chk_UMAAll.AutoSize = true;
            this.chk_UMAAll.Enabled = false;
            this.chk_UMAAll.Location = new System.Drawing.Point(236, 29);
            this.chk_UMAAll.Name = "chk_UMAAll";
            this.chk_UMAAll.Size = new System.Drawing.Size(48, 16);
            this.chk_UMAAll.TabIndex = 18;
            this.chk_UMAAll.Text = "全部";
            this.chk_UMAAll.UseVisualStyleBackColor = true;
            this.chk_UMAAll.Click += new System.EventHandler(this.chk_UMAAll_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 50);
            this.panel1.TabIndex = 10;
            // 
            // colcUserId
            // 
            this.colcUserId.DataPropertyName = "cUserId";
            this.colcUserId.HeaderText = "用户编码";
            this.colcUserId.Name = "colcUserId";
            this.colcUserId.ReadOnly = true;
            this.colcUserId.Width = 80;
            // 
            // colcName
            // 
            this.colcName.DataPropertyName = "cName";
            this.colcName.HeaderText = "工号";
            this.colcName.Name = "colcName";
            this.colcName.ReadOnly = true;
            this.colcName.Width = 80;
            // 
            // colcLinkID
            // 
            this.colcLinkID.DataPropertyName = "cLinkID";
            this.colcLinkID.HeaderText = "姓名";
            this.colcLinkID.Name = "colcLinkID";
            this.colcLinkID.ReadOnly = true;
            this.colcLinkID.Width = 80;
            // 
            // colcDept
            // 
            this.colcDept.DataPropertyName = "cDeptName";
            this.colcDept.HeaderText = "部门";
            this.colcDept.Name = "colcDept";
            this.colcDept.ReadOnly = true;
            this.colcDept.Width = 80;
            // 
            // frmUserRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(924, 518);
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.panel2);
            this.Name = "frmUserRight";
            this.Text = "用户权限设置";
            this.Load += new System.EventHandler(this.frmUserRight_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUser)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsUser)).EndInit();
            this.tbcMain.ResumeLayout(false);
            this.tbpFun.ResumeLayout(false);
            this.tbpUserWHouse.ResumeLayout(false);
            this.tbpUserWHouse.PerformLayout();
            this.tbp_UserMgrArea.ResumeLayout(false);
            this.tbp_UserMgrArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView grdUser;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Right;
        private System.Windows.Forms.Button btn_SaveRTS;
        private System.Windows.Forms.CheckBox chk_SelAll;
        private System.Windows.Forms.TreeView trvRight;
        private System.Windows.Forms.BindingSource bdsUser;
        private System.Windows.Forms.ProgressBar prgRTS;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpFun;
        private System.Windows.Forms.TabPage tbpUserWHouse;
        private System.Windows.Forms.Button btn_UWCancel;
        private System.Windows.Forms.Button btnUWClose;
        private System.Windows.Forms.Button btn_UWRights;
        private System.Windows.Forms.Button btnUWSave;
        private System.Windows.Forms.CheckBox chkUWCheckAll;
        private System.Windows.Forms.TreeView trvUWRights;
        private System.Windows.Forms.ProgressBar prgUW;
        private System.Windows.Forms.TabPage tbp_UserMgrArea;
        private System.Windows.Forms.ProgressBar prg_UMA;
        private System.Windows.Forms.TreeView trv_MgrArea;
        private System.Windows.Forms.Button btn_UMAUndo;
        private System.Windows.Forms.Button btn_UMAClose;
        private System.Windows.Forms.Button btn_UMARights;
        private System.Windows.Forms.Button btn_UMASave;
        private System.Windows.Forms.CheckBox chk_UMAAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcLinkID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colcDept;
        public System.Windows.Forms.Panel panel1;

    }
}
