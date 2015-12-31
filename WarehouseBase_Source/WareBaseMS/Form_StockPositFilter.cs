namespace WareBaseMS
{
    using SunEast;
    using SunEast.App;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DBCommInfo;

    public class Form_StockPositFilter : Form
    {
        public bool bIsOK = false;
        private Button btn_OK;
        private Button button2;
        private bool bWareIsOpened = false;
        private ComboBox cmb_cAreaId;
        private ComboBox cmb_cMAreaId;
        private ComboBox cmb_cPalletSpec;
        private ComboBox cmb_cWHId;
        private ComboBox cmb_Height;
        private ComboBox cmb_Weight;
        private IContainer components = null;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBox_ColFrom;
        private TextBox textBox_ColTo;
        private TextBox textBox_LayerFrom;
        private TextBox textBox_LayerTo;
        private TextBox textBox_RowFrom;
        private TextBox textBox_RowTo;
        private TextBox txt_nH;
        private TextBox txt_nL;
        private TextBox txt_nW;
        public string UserId = "";

        public Form_StockPositFilter()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cmb_cWHId.Text.Trim() == "")
            {
                MessageBox.Show("不好意思，仓库不能为空！");
                this.cmb_cWHId.Focus();
            }
            else if (this.cmb_cWHId.SelectedValue == null)
            {
                MessageBox.Show("不好意思，仓库不能为空，请选择");
                this.cmb_cWHId.Focus();
            }
            else
            {
                try
                {
                    string str = "";
                    DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                        SqlText = "sp_CreatePosId :pWHId,:pRowFrom,:pRowTo,:pColFrom,:pColTo,:pLayerFrom,:pLayerTo,:pRowLen,:pColLen,:pLayerLen,:pAreaId,:pPalletSpec,:pL,:pW,:pH,:pHeight,:pMAreaId,pWeight",
                        SqlType = SqlCommandType.sctProcedure,
                        PageIndex = 0,
                        PageSize = 0,
                        FromSysType = "dotnet"
                    };
                    ZqmParamter paramter = null;
                    paramter = new ZqmParamter {
                        ParameterName = "pWHId",
                        ParameterValue = this.cmb_cWHId.SelectedValue.ToString(),
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pRowFrom",
                        ParameterValue = this.textBox_RowFrom.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pRowTo",
                        ParameterValue = this.textBox_RowTo.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pColFrom",
                        ParameterValue = this.textBox_ColFrom.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pColTo",
                        ParameterValue = this.textBox_ColTo.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pLayerFrom",
                        ParameterValue = this.textBox_LayerFrom.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pLayerTo",
                        ParameterValue = this.textBox_LayerTo.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pRowLen",
                        ParameterValue = "2",
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pColLen",
                        ParameterValue = "3",
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pLayerLen",
                        ParameterValue = "2",
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    str = "";
                    if (this.cmb_cAreaId.SelectedValue != null)
                    {
                        str = this.cmb_cAreaId.SelectedValue.ToString().Trim();
                    }
                    paramter = new ZqmParamter {
                        ParameterName = "pAreaId",
                        ParameterValue = str,
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    str = "";
                    if (this.cmb_cPalletSpec.SelectedValue != null)
                    {
                        str = this.cmb_cPalletSpec.SelectedValue.ToString().Trim();
                    }
                    paramter = new ZqmParamter {
                        ParameterName = "pPalletSpec",
                        ParameterValue = str,
                        DataType = ZqmDataType.String,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pL",
                        ParameterValue = this.txt_nL.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pW",
                        ParameterValue = this.txt_nW.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pH",
                        ParameterValue = this.txt_nH.Text.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pHeight",
                        ParameterValue = this.cmb_Height.SelectedValue.ToString(),
                        DataType = ZqmDataType.Int,
                        ParameterDir = ZqmParameterDirction.Input
                    };
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pMAreaId"
                    };
                    str = "";
                    if ((this.cmb_cMAreaId.Text.Trim() != "") && (this.cmb_cMAreaId.SelectedValue != null))
                    {
                        str = this.cmb_cMAreaId.SelectedValue.ToString().Trim();
                    }
                    paramter.ParameterValue = str;
                    paramter.DataType = ZqmDataType.String;
                    paramter.ParameterDir = ZqmParameterDirction.Input;
                    cmdInfo.Parameters.Add(paramter);
                    paramter = new ZqmParamter {
                        ParameterName = "pWeight",
                        ParameterValue = this.cmb_Weight.SelectedValue.ToString(),
                        DataType = ZqmDataType.Int,
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
                        this.bIsOK = true;
                        if (table != null)
                        {
                            MessageBox.Show(table.Rows[0]["cResult"].ToString());
                        }
                        dataSet.Clear();
                    }
                }
                catch (Exception exception)
                {
                    this.bIsOK = false;
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmb_cWHId_TextChanged(object sender, EventArgs e)
        {
            if (this.bWareIsOpened && (this.cmb_cWHId.Text.Trim() != ""))
            {
                object selectedValue = this.cmb_cWHId.SelectedValue;
                if (selectedValue != null)
                {
                    this.LoadAreaIdList(selectedValue.ToString().Trim());
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

        private void Form_StockPositFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void Form_StockPositFilter_Load(object sender, EventArgs e)
        {
            this.LoadPalletSpecList();
            this.LoadStockList("");
            this.LoadMgrAreaList();
            this.LoadPosWHList();
        }

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.cmb_cWHId = new ComboBox();
            this.label1 = new Label();
            this.label6 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label7 = new Label();
            this.textBox_RowFrom = new TextBox();
            this.textBox_RowTo = new TextBox();
            this.textBox_ColTo = new TextBox();
            this.textBox_ColFrom = new TextBox();
            this.textBox_LayerTo = new TextBox();
            this.textBox_LayerFrom = new TextBox();
            this.btn_OK = new Button();
            this.button2 = new Button();
            this.cmb_cPalletSpec = new ComboBox();
            this.label11 = new Label();
            this.cmb_cAreaId = new ComboBox();
            this.label8 = new Label();
            this.txt_nW = new TextBox();
            this.txt_nL = new TextBox();
            this.label9 = new Label();
            this.label10 = new Label();
            this.txt_nH = new TextBox();
            this.label12 = new Label();
            this.label13 = new Label();
            this.cmb_cMAreaId = new ComboBox();
            this.label14 = new Label();
            this.label15 = new Label();
            this.cmb_Height = new ComboBox();
            this.cmb_Weight = new ComboBox();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 6);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "仓库：";
            this.cmb_cWHId.FormattingEnabled = true;
            this.cmb_cWHId.Location = new Point(0x35, 4);
            this.cmb_cWHId.Name = "cmb_cWHId";
            this.cmb_cWHId.Size = new Size(0xca, 20);
            this.cmb_cWHId.TabIndex = 15;
            this.cmb_cWHId.Tag = "101";
            this.cmb_cWHId.Text = "Bind SelectedValue";
            this.cmb_cWHId.TextChanged += new EventHandler(this.cmb_cWHId_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x1f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0x11;
            this.label1.Text = "行号：";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x8d, 0x1f);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x11, 12);
            this.label6.TabIndex = 0x15;
            this.label6.Text = "至";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x8d, 0x34);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "至";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x34);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 0x16;
            this.label4.Text = "列号：";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x8d, 0x4b);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 0x19;
            this.label5.Text = "至";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(12, 0x4b);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 0x18;
            this.label7.Text = "层号：";
            this.textBox_RowFrom.Location = new Point(0x35, 0x1a);
            this.textBox_RowFrom.Name = "textBox_RowFrom";
            this.textBox_RowFrom.Size = new Size(0x25, 0x15);
            this.textBox_RowFrom.TabIndex = 0x1a;
            this.textBox_RowFrom.Text = "88";
            this.textBox_RowTo.Location = new Point(0xd1, 0x1a);
            this.textBox_RowTo.Name = "textBox_RowTo";
            this.textBox_RowTo.Size = new Size(0x25, 0x15);
            this.textBox_RowTo.TabIndex = 0x1b;
            this.textBox_RowTo.Text = "89";
            this.textBox_ColTo.Location = new Point(0xd1, 0x2f);
            this.textBox_ColTo.Name = "textBox_ColTo";
            this.textBox_ColTo.Size = new Size(0x25, 0x15);
            this.textBox_ColTo.TabIndex = 0x1d;
            this.textBox_ColTo.Text = "89";
            this.textBox_ColFrom.Location = new Point(0x35, 0x2f);
            this.textBox_ColFrom.Name = "textBox_ColFrom";
            this.textBox_ColFrom.Size = new Size(0x25, 0x15);
            this.textBox_ColFrom.TabIndex = 0x1c;
            this.textBox_ColFrom.Text = "88";
            this.textBox_LayerTo.Location = new Point(0xd1, 70);
            this.textBox_LayerTo.Name = "textBox_LayerTo";
            this.textBox_LayerTo.Size = new Size(0x25, 0x15);
            this.textBox_LayerTo.TabIndex = 0x1f;
            this.textBox_LayerTo.Text = "89";
            this.textBox_LayerFrom.Location = new Point(0x35, 70);
            this.textBox_LayerFrom.Name = "textBox_LayerFrom";
            this.textBox_LayerFrom.Size = new Size(0x25, 0x15);
            this.textBox_LayerFrom.TabIndex = 30;
            this.textBox_LayerFrom.Text = "88";
            this.btn_OK.Location = new Point(0x23, 0xde);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new Size(0x43, 0x17);
            this.btn_OK.TabIndex = 0x20;
            this.btn_OK.Text = "确定(&O)";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(130, 0xde);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x3e, 0x17);
            this.button2.TabIndex = 0x21;
            this.button2.Text = "取消(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.cmb_cPalletSpec.FormattingEnabled = true;
            this.cmb_cPalletSpec.Location = new Point(70, 0x74);
            this.cmb_cPalletSpec.Name = "cmb_cPalletSpec";
            this.cmb_cPalletSpec.Size = new Size(0xaf, 20);
            this.cmb_cPalletSpec.TabIndex = 0x38;
            this.cmb_cPalletSpec.Tag = "101";
            this.cmb_cPalletSpec.Text = "Bind SelectedValue";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(11, 0x74);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x41, 12);
            this.label11.TabIndex = 0x37;
            this.label11.Text = "托盘规格：";
            this.cmb_cAreaId.FormattingEnabled = true;
            this.cmb_cAreaId.Location = new Point(0x47, 0x5e);
            this.cmb_cAreaId.Name = "cmb_cAreaId";
            this.cmb_cAreaId.Size = new Size(0xaf, 20);
            this.cmb_cAreaId.TabIndex = 0x3a;
            this.cmb_cAreaId.Tag = "101";
            this.cmb_cAreaId.Text = "Bind SelectedValue";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 0x5e);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x39;
            this.label8.Text = "分区号：";
            this.txt_nW.Location = new Point(0x8f, 160);
            this.txt_nW.Name = "txt_nW";
            this.txt_nW.Size = new Size(0x25, 0x15);
            this.txt_nW.TabIndex = 0x3e;
            this.txt_nW.Text = "1200";
            this.txt_nL.Location = new Point(0x47, 160);
            this.txt_nL.Name = "txt_nL";
            this.txt_nL.Size = new Size(0x25, 0x15);
            this.txt_nL.TabIndex = 0x3d;
            this.txt_nL.Text = "1600";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(120, 0xa3);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 60;
            this.label9.Text = "宽：";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(11, 0xa2);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x35, 12);
            this.label10.TabIndex = 0x3b;
            this.label10.Text = "托盘长：";
            this.txt_nH.Location = new Point(0xd0, 160);
            this.txt_nH.Name = "txt_nH";
            this.txt_nH.Size = new Size(0x25, 0x15);
            this.txt_nH.TabIndex = 0x40;
            this.txt_nH.Text = "170";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xb9, 0xa3);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x1d, 12);
            this.label12.TabIndex = 0x3f;
            this.label12.Text = "高：";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(12, 0xba);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x41, 12);
            this.label13.TabIndex = 0x41;
            this.label13.Text = "货位高度：";
            this.cmb_cMAreaId.FormattingEnabled = true;
            this.cmb_cMAreaId.Location = new Point(0x47, 0x8a);
            this.cmb_cMAreaId.Name = "cmb_cMAreaId";
            this.cmb_cMAreaId.Size = new Size(0xaf, 20);
            this.cmb_cMAreaId.TabIndex = 0x44;
            this.cmb_cMAreaId.Tag = "101";
            this.cmb_cMAreaId.Text = "Bind SelectedValue";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(12, 0x8a);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x41, 12);
            this.label14.TabIndex = 0x43;
            this.label14.Text = "管理货区：";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x79, 0xba);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x41, 12);
            this.label15.TabIndex = 0x45;
            this.label15.Text = "货位载重：";
            this.cmb_Height.FormattingEnabled = true;
            this.cmb_Height.Location = new Point(0x47, 0xba);
            this.cmb_Height.Name = "cmb_Height";
            this.cmb_Height.Size = new Size(0x33, 20);
            this.cmb_Height.TabIndex = 0x47;
            this.cmb_Height.Tag = "101";
            this.cmb_Height.Text = "Bind SelectedValue";
            this.cmb_Weight.FormattingEnabled = true;
            this.cmb_Weight.Location = new Point(0xc1, 0xba);
            this.cmb_Weight.Name = "cmb_Weight";
            this.cmb_Weight.Size = new Size(0x33, 20);
            this.cmb_Weight.TabIndex = 0x48;
            this.cmb_Weight.Tag = "101";
            this.cmb_Weight.Text = "Bind SelectedValue";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x10b, 0x106);
            base.Controls.Add(this.cmb_Weight);
            base.Controls.Add(this.cmb_Height);
            base.Controls.Add(this.label15);
            base.Controls.Add(this.cmb_cMAreaId);
            base.Controls.Add(this.label14);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.txt_nH);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.txt_nW);
            base.Controls.Add(this.txt_nL);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.cmb_cAreaId);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.cmb_cPalletSpec);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btn_OK);
            base.Controls.Add(this.textBox_LayerTo);
            base.Controls.Add(this.textBox_LayerFrom);
            base.Controls.Add(this.textBox_ColTo);
            base.Controls.Add(this.textBox_ColFrom);
            base.Controls.Add(this.textBox_RowTo);
            base.Controls.Add(this.textBox_RowFrom);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cmb_cWHId);
            base.KeyPreview = true;
            base.Name = "Form_StockPositFilter";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "创建货位条件";
            base.KeyPress += new KeyPressEventHandler(this.Form_StockPositFilter_KeyPress);
            base.Load += new EventHandler(this.Form_StockPositFilter_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadAreaIdList(string StockId)
        {
            string sErr = "";
            string sSql = "select * from TWC_WArea where bUsed=1 ";
            if (StockId.Trim() != "")
            {
                sSql = sSql + " and cWHId='" + StockId + "'";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cAreaId.DataSource = dataBySql.Tables["data"];
            this.cmb_cAreaId.DisplayMember = "cAreaName";
            this.cmb_cAreaId.ValueMember = "cAreaId";
        }

        private void LoadMgrAreaList()
        {
            string sErr = "";
            string sSql = "select cMAreaId,cMAName,bUsed from TWC_MgrArea where bUsed=1 ";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cMAreaId.DataSource = dataBySql.Tables["data"];
            this.cmb_cMAreaId.DisplayMember = "cMAName";
            this.cmb_cMAreaId.ValueMember = "cMAreaId";
        }

        private void LoadPalletSpecList()
        {
            string sErr = "";
            string sSql = "select * from twc_palletspec ";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.cmb_cPalletSpec.DataSource = dataBySql.Tables["data"];
            this.cmb_cPalletSpec.DisplayMember = "cPalletSpec";
            this.cmb_cPalletSpec.ValueMember = "cPalletSpec";
        }

        private void LoadPosWHList()
        {
            string sErr = "";
            string sSql = "select * from TWC_PosHLevel  ";
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            DataTable table = null;
            if (dataBySql.Tables["data"] != null)
            {
                table = dataBySql.Tables["data"].Copy();
            }
            dataBySql.Clear();
            this.cmb_Height.DataSource = table;
            this.cmb_Height.DisplayMember = "nHeight";
            this.cmb_Height.ValueMember = "nHLId";
            sSql = "select * from TWC_PosWLevel  ";
            dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            DataTable table2 = null;
            if (dataBySql.Tables["data"] != null)
            {
                table2 = dataBySql.Tables["data"].Copy();
            }
            dataBySql.Clear();
            this.cmb_Weight.DataSource = table2;
            this.cmb_Weight.DisplayMember = "nWeight";
            this.cmb_Weight.ValueMember = "nWLId";
        }

        private void LoadStockList(string StockId)
        {
            string sErr = "";
            string sSql = "select cWHId,cName from TWC_WareHouse where bUsed=1 ";
            if (StockId.Trim() != "")
            {
                sSql = sSql + " and cWHId='" + StockId + "'";
            }
            DataSet dataBySql = PubDBCommFuns.GetDataBySql(sSql, out sErr);
            this.bWareIsOpened = false;
            this.cmb_cWHId.DataSource = dataBySql.Tables["data"];
            this.cmb_cWHId.DisplayMember = "cName";
            this.cmb_cWHId.ValueMember = "cWHId";
            this.bWareIsOpened = true;
            if (this.cmb_cWHId.Items.Count > 0)
            {
                this.cmb_cWHId_TextChanged(null, null);
            }
        }
    }
}

