using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using SunEast;
using System.Collections.Generic;

namespace SunEast.App
{
    public partial class frmWareCellState : UI.FrmSTable
    {
        #region  Const
        const int cnst_nCols = 28;  //列数
        const int cnst_nRows = 2;   //行数
        const int cnst_nLayer = 6;  //层
        //
        const int btnHeight = 38;//33
        const int btnWidth = 38;//33
        //
        const int imgIndex_Empty = 0;
        const int imgIndex_EmptyPallet = 1;
        const int imgIndex_Goods = 2;
        const int imgIndex_EmptyLocked = 3;
        const int imgIndex_EmptyPalletLocked = 4;
        const int imgIndex_GoodsLocked = 5;

        #endregion
        #region 私有变量

        string sBNoAjust = "";

        bool bWHIsOpen = false;
        bool bWAreaIsOpen = false;
        bool bRowIsOK = false;
        Button[,] btnCells = null; //二维按钮数组
        Button btnCurr = null;//记录当前按下的按钮
        //
        Color clrUnActive = Color.FromName("Lime");
        Color clrActive = Color.FromName("Blue");
        Color clrLabel = Color.FromName("ActiveCaption");
        //
        private bool bIsSelectCell = false; //是否是选择货位
        private bool bIsResultOK = false;   //是否选择OK
        private string selectResult = "";   //选择结果
        private string selectOtherValue = "";   //选择结果其他值
        #endregion
        #region 私有方法
            private string GetStateToolTip(string sCellId, string sPalletNo, int nR, int nC, int nL, int nState, int nLock)
            {
                //0:空位 1:空盘 2:有货
                //0:可用 1:配盘 2:执行中 3:锁定
                string sState = "";
                switch (nState)
                {
                    case 0:
                        sState = "存货状态： 空位";
                        break;
                    case 1:
                        sState = "存货状态： 空盘";
                        break;
                    case 2:
                        sState = "存货状态： 有货";
                        break;
                    default:
                        sState = "";
                        break;
                }
                switch (nLock)
                {
                    case 0:
                        sState = sState + "   使用状态： 可用";
                        break;
                    case 1:
                        sState = sState + "   使用状态： 配盘中...";
                        break;
                    case 2:
                        sState = sState + "   使用状态： 执行中...";
                        break;
                    case 3:
                        sState = sState + "   使用状态： 系统锁定不可使用";
                        break;
                    default:
                        break;
                }
                string sX = "";
                sX = "货位号：" + sCellId + "  托盘号：" + sPalletNo + "\n" +
                    "行/排：" + nR.ToString() + "  列：" + nC.ToString() + "  层：" + nL.ToString() + "\n" + sState;
                return (sX);
            }
            private bool CreateCellButtons(string sWHId,Panel pnlCs, int nCols,int nLayers)
            {
                Cursor curOld = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                pnlCs.Controls.Clear();
                
                int nSpace = 2;//间隔距离
                int lblW = 28;
                int lblLR_W = 20;
                int lblTB_H = 15;
                bool bOK = false;
                int nR = 0;
                int nC = 0;
                int nL = 0;
                int nX = 0;
                //
                int nLeft = 0;
                int nTop = 0;
                Button btnX = null;
                try
                {
                    
                    pnlCs.Width = (lblLR_W + nSpace + 1) * 2 + (btnWidth + nSpace) * nCols + nSpace;
                    pnlCs.Height = (lblTB_H + nSpace + 1) * 2 + (btnHeight + nSpace) * nLayers + nSpace;
                    if (pnlCs.Width < pnlContainer.Width)
                    {
                        pnlCs.Left = (int)(pnlContainer.Width - pnlCs.Width) / 2;
                    }
                    else pnlCs.Left = 0;
                    if (pnlCs.Height < pnlContainer.Height)
                    {
                        pnlCs.Top = (int)(pnlContainer.Height - pnlCs.Height) / 2;
                    }
                    else pnlCs.Top = 0;

                    //添加四条边框 lbl
                    Label lblLeft = new Label();
                    Label lblRight = new Label();
                    Label lblTop = new Label();
                    Label lblButtom = new Label();
                    lblLeft.AutoSize = false;
                    lblRight.AutoSize = false;
                    lblTop.AutoSize = false;
                    lblButtom.AutoSize = false;
                    //
                    lblLeft.Height = pnlCs.Height;
                    lblRight.Height = pnlCs.Height;
                    lblLeft.Width = 1;
                    lblRight.Width = 1;
                    //
                    lblTop.Width = pnlCs.Width;
                    lblButtom.Width = pnlCs.Width;
                    lblTop.Height = 1;
                    lblButtom.Height = 1;
                    //
                    lblLeft.BackColor = clrLabel;
                    lblRight.BackColor = clrLabel;
                    lblTop.BackColor = clrLabel;
                    lblButtom.BackColor = clrLabel;
                    //

                    lblLeft.Top = 0;
                    lblRight.Top = 0;
                    lblLeft.Left = lblLR_W + 2;
                    lblRight.Left = pnlCs.Width - lblLR_W - 2 + 1;
                    //
                    lblTop.Top = lblTB_H + 2;
                    lblButtom.Top = pnlCs.Height - lblTB_H - 2;
                    lblTop.Left = 0;
                    lblButtom.Left = 0;
                    pnlCs.Controls.Add(lblTop);
                    pnlCs.Controls.Add(lblButtom);
                    pnlCs.Controls.Add(lblLeft);
                    pnlCs.Controls.Add(lblRight);
                    //
                    nTop = lblTop.Top + (nLayers) * (btnHeight + 2) + 5;
                    btnCells = new Button[nLayers , nCols];
                    for (nL = 0; nL < nLayers ; nL++)
                    {
                        nLeft = lblLeft.Left + -1 * btnWidth;
                        nTop = nTop - btnHeight - 2;
                        Label lblL = new Label();
                        lblL.Name = "blLeft" + (nL + 1).ToString();
                        lblL.Left = 0;
                        lblL.Width = lblLR_W;
                        lblL.Height = lblTB_H;
                        lblL.ForeColor = clrLabel;
                        lblL.Text = (nL + 1).ToString("D2");
                        lblL.Top = nTop + (btnHeight - lblTB_H);
                        Label lblR = new Label();
                        lblR.Name = "blRight" + (nL + 1).ToString();
                        lblR.Left = lblRight.Left + 2;
                        lblR.Width = lblW;
                        lblR.Height = lblW;
                        lblR.ForeColor = clrLabel;
                        lblR.Text = (nL + 1).ToString("D2");
                        lblR.Top = lblL.Top;
                        nLeft = lblLeft.Left - btnWidth;
                        pnlCs.Controls.Add(lblL);
                        pnlCs.Controls.Add(lblR);
                        for (nC = 0; nC < nCols; nC++)
                        {
                            nX++;
                            nLeft = nLeft + 2 + btnWidth;
                            //
                            if (nL == 0) //只能循环一次
                            {
                                Label lblT = new Label();
                                Label lblB = new Label();
                                lblT.Width = lblW;
                                lblT.Height = lblTB_H;
                                lblB.Width = lblW;
                                lblB.Height = lblTB_H;
                                lblT.ForeColor = clrLabel;
                                lblB.ForeColor = clrLabel;
                                lblB.Left = nLeft + btnHeight - lblLR_W - 5;
                                lblT.Left = lblB.Left;
                                lblT.Top = 0;
                                lblB.Top = lblButtom.Top + 1;
                                lblT.Text = (nC + 1).ToString("D3");
                                lblB.Text = lblT.Text;
                                pnlCs.Controls.Add(lblT);
                                pnlCs.Controls.Add(lblB);
                            }
                            btnX = new Button();
                            btnCells[nL, nC] = btnX;
                            btnX.Name = "btnCell" + nX.ToString();
                            btnX.ImageList = imgLstState;
                            btnX.ImageIndex = imgLstState.Images.IndexOfKey("-10.png"); ;
                            btnX.Width = btnWidth;
                            btnX.Height = btnHeight;
                            btnX.Left = nLeft;
                            btnX.Top = nTop;
                            btnX.FlatStyle = FlatStyle.Flat;
                            btnX.FlatAppearance.BorderSize = 2;
                            btnX.FlatAppearance.BorderColor = clrUnActive;
                            btnX.Click += new EventHandler(btnCell_Click);
                            btnX.Tag = sWHId+"-" + (nC + 1).ToString("D3") + "-" + (nL + 1).ToString("D2");
                            toolTip.SetToolTip(btnX, GetStateToolTip("", "", 0, (nC + 1), (nL + 1), 0, 0));
                            //btnX.BackColor = Color.Yellow;
                            pnlCs.Controls.Add(btnX);
                        }
                    }
                    Cursor.Current = curOld;
                    bOK = true;
                }
                catch (Exception err)
                {
                    bOK = false;
                    Cursor.Current = curOld;
                    MessageBox.Show(err.Message);
                }
                bOK = true;
                return (bOK);
            }
            private bool SetCellStates(string sWHId,int nR)
            {
                bool bOK = false;
                int nC = 0;
                int nL = 0;
                int nState = 0;
                int nLock = 0;
                int nIndex = 0;
                int nColor = 0;
                int nOldColor = 0;
                Color clrArea = pnlCells.BackColor;
                nOldColor = clrArea.ToArgb();
                if (sWHId.Trim() == "" || nR < 1)
                    return false;

                string sCellId = "";
                string sPalletNo = "";
                string sErr = "";
                string sSql = "select cel.cPosId,isnull(cel.npalletid,' ') nPalletId,cel.nRow,cel.nCol,cel.nLayer,isnull(cel.nStatusWork,0) nStatusWork,"+
                                "case isnull(cel.nStatusWork,0) when 0 then '空闲' when 1 then '预定' when 2 then '工作' when 3 then '暂锁' when 4 then '禁用' end as cStatusWork, "+
                                "isnull(plt.nStatusStore,-1) nStatusStore,case isnull(plt.nStatusStore,-1) when -1 then '空位' when 0 then '空盘' when 1 then '半盘' when 2 then '满盘' end as cStatusStore,  "+
                                " Wa.cAreaId,Wa.cAreaName,Wa.nColorValue " +
                                " FROM TWC_WareCell cel left join TWC_PalletCell plt on isnull(rtrim(cel.nPalletId),' ')=rtrim(plt.nPalletId)  left join TWC_WArea Wa on cel.cAreaId=Wa.cAreaId " +
                                " where cel.cWHId='" + sWHId.Trim() +"' and cel.nRow=" + nR.ToString();
                DataSet dsX = new DataSet();
                Button btnX = null;
                dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, out sErr);
                if (sErr == "0" || sErr.Trim() =="")
                {
                    if (btnCurr != null)
                    {
                        btnCurr.FlatAppearance.BorderColor = clrUnActive;
                    }
                    btnCurr = null;
                    DataTable tbX = dsX.Tables["data"] ;
                    if (tbX != null)
                    {
                        for (int iL = 0; iL <= btnCells.GetUpperBound(0); iL++)
                        {
                            for (int iC = 0; iC <= btnCells.GetUpperBound(1); iC++)
                            {
                                string sFilter = "nCol=" + (iC+1).ToString() + " and nLayer=" + (iL+1).ToString();
                                DataRow [] drA = tbX.Select(sFilter);
                                if (drA.Length == 0)
                                {
                                    //nIndex = imgLstState.Images.IndexOfKey(nState.ToString() + nLock.ToString() + ".png");
                                    nIndex = imgLstState.Images.IndexOfKey("-14.png");
                                    btnX = btnCells[iL, iC];
                                    btnX.Tag = null;
                                    btnX.ImageIndex = nIndex;
                                    toolTip.SetToolTip(btnX, "虚拟货位");
                                    btnX.BackColor = Color.FromArgb(nOldColor);
                                    btnX.Enabled = false;
                                }
                                else
                                {
                                    DataRow drX = drA[0];
                                    nState = int.Parse(drX["nStatusStore"].ToString());
                                    nLock = int.Parse(drX["nStatusWork"].ToString());
                                    sCellId = drX["cPosId"].ToString();
                                    sPalletNo = drX["nPalletId"].ToString();
                                    string sStatusStore = drX["cStatusStore"].ToString();
                                    string sStatusWK = drX["cStatusWork"].ToString();
                                    string sColor = drX["nColorValue"].ToString().Trim();
                                    nColor = nOldColor;
                                    if (sColor.Trim() != "")
                                    {
                                        nColor = int.Parse(sColor);
                                    }
                                    //nIndex = GetImgIndex(nState, nLock);
                                    nIndex = imgLstState.Images.IndexOfKey(nState.ToString() + nLock.ToString() + ".png");
                                    btnX = btnCells[iL, iC];
                                    btnX.Tag = sCellId;
                                    btnX.ImageIndex = nIndex;
                                    btnX.BackColor = Color.FromArgb(nColor);
                                    toolTip.SetToolTip(btnX, "  货区：" + drX["cAreaId"].ToString() + "—" + drX["cAreaName"].ToString() + "\n" + "  货位：" + sCellId + "  — " + drX["cStatusWork"].ToString() + "\n" +"  托盘：" + sPalletNo  + " — " + sStatusStore + "  " + sStatusWK);
                                    if (nLock > 2)
                                        btnX.Enabled = false;
                                    else
                                        btnX.Enabled = true;
                                }
                            }
                        }
                    }
                    /*
                    foreach (DataRow drX in tbX.Rows)
                    {
                        nC = int.Parse(drX["nCol"].ToString());
                        nL = int.Parse(drX["nLayer"].ToString());
                        nState = int.Parse(drX["nState"].ToString());
                        nLock = int.Parse(drX["nLockState"].ToString());
                        sCellId = drX["cCellId"].ToString();
                        sPalletNo = drX["cPalletNo"].ToString();
                        nIndex = GetImgIndex(nState, nLock);
                        btnX = btnCells[nL - 1, nC - 1];
                        btnX.Tag = sCellId;
                        btnX.ImageIndex = nIndex;
                        toolTip.SetToolTip(btnX, GetStateToolTip(sCellId, sPalletNo, nR, nC, nL, nState, nLock));
                        if (nLock == 3)
                            btnX.Enabled = false;
                    }
                    */
                    bOK = true;
                }
                else
                {
                    MessageBox.Show(sErr);
                }
                return (bOK);
            }
            private int GetImgIndex(int nState, int nLock)
            {
                int nX = 0;
                //0:可用 1:配盘 2:执行中 3:锁定)
                if (nLock == 0)
                {
                    //状态(0:空位 1:空盘 2:有货)
                    switch (nState)
                    {
                        case 0:
                            nX = imgIndex_Empty;
                            break;
                        case 1:
                            nX = imgIndex_EmptyPallet;
                            break;
                        case 2:
                            nX = imgIndex_Goods;
                            break;
                    }
                }
                else if (nLock == 3)
                {
                    nX = imgIndex_Empty;
                }
                else
                {
                    switch (nState)
                    {
                        case 0:
                            nX = imgIndex_EmptyLocked;
                            break;
                        case 1:
                            nX = imgIndex_EmptyPalletLocked;
                            break;
                        case 2:
                            nX = imgIndex_GoodsLocked;
                            break;
                    }
                }
                return (nX);
            }
            private void OpenCellItemList(string sCellId)
            {
                string sPalletId = "";
                string sErr = "";
                object objX = null;
                PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, "select isnull(nPalletId,'') nPalletId from TWC_WareCell where cPosId='"+ sCellId.Trim() +"'", "", "nPalletId", out objX, out sErr);
                if (sErr.Trim() != "" && sErr.Trim() == "0")
                {
                    MessageBox.Show(sErr);
                    return;
                }
                if (objX != null)
                    sPalletId = objX.ToString();
                if (sPalletId.Trim() == "") sPalletId = "~";
                string sWHId = cmb_cWHId.SelectedValue.ToString();
                //StringBuilder sSql = new StringBuilder("select * from V_StoreItemList");
                DataTable tbX  = PubDBCommFuns.sp_Pack_GetItemList(AppInformation.SvrSocket, sPalletId, sWHId, "", 1, out sErr);
                if ((sErr != "0") && (sErr.Trim().Length !=0))
                {
                    MessageBox.Show(sErr);
                }
                else
                {                    
                    bdsList.DataSource = tbX;
                    grdList.DataSource = bdsList;
                }
            }

            private string GetCellPalletId(string sCellId)
            {
                string sPalletId = "";
                string sErr = "";
                object objX = null;
                PubDBCommFuns.GetValueBySql(AppInformation.SvrSocket, "select isnull(nPalletId,' ') nPalletId from TWC_WareCell where cPosId='" + sCellId.Trim() + "'", "", "nPalletId", out objX, out sErr);
                if (sErr.Trim() != "" && sErr.Trim() == "0")
                {
                    MessageBox.Show(sErr);
                    return "";
                }
                if (objX != null)
                {
                    sPalletId = objX.ToString();
                }
                return sPalletId.Trim();
            }

            private void UpdateWareCellState()
            {
                /*
                 update TWC_WareCell set nState=0 where isnull(cPalletNo,'')='' and nState<>0
                update TWC_WareCell set nState =1  where  nState <> 1 and isnull(cPalletNo,'')<>'' and cPalletNo not in (select distinct cPalletNo from TWB_Stock )
                update TWC_WareCell set nState =2  where  nState <> 2 and isnull(cPalletNo,'')<>'' and cPalletNo in (select distinct cPalletNo from TWB_Stock )
                */
                StringBuilder sSql = new StringBuilder("");
                long nX = 0;
                string sX = "";
                sSql.Append("update TWC_WareCell set nState=0 where isnull(cPalletNo,'')='' and nState<>0");
                sX = DBBase.DBOptrForComm.OptExecRetLineCount(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), out nX);
                sSql.Remove(0, sSql.Length);
                sSql.Append("update TWC_WareCell set nState =1  where  nState <> 1 and isnull(cPalletNo,'')<>'' and cPalletNo not in (select distinct cPalletNo from TWB_Stock )");
                sX = DBBase.DBOptrForComm.OptExecRetLineCount(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), out nX);
                sSql.Remove(0, sSql.Length);
                sSql.Append("update TWC_WareCell set nState =2  where  nState <> 2 and isnull(cPalletNo,'')<>'' and cPalletNo in (select distinct cPalletNo from TWB_Stock )");
                sX = DBBase.DBOptrForComm.OptExecRetLineCount(AppInformation.dbtApp, AppInformation.AppConn, sSql.ToString(), out nX);

            }

            private void OpenWareHouseList(string sCmptId,int nWType)
            {
                string sErr = "";
                string sSql = "select * from TWC_Warehouse where bUsed=1";
                if (sCmptId.Trim().Length > 0)
                {
                    sSql += " and cCmptId='" + sCmptId.Trim() + "'";
                }
                if (nWType > 0)
                {
                    sSql += " and nType=" + nWType.ToString();
                }
                if (UserInformation.UType != UserType.utSupervisor)
                {
                    sSql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
                }
                DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, out sErr);
                if (sErr.Trim() != "0" && sErr.Trim() != "")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    DataTable tbX = null;
                    tbX = dsX.Tables["result"];
                    if (tbX != null)
                    {
                        object objX = tbX.Rows[0][1] ;
                        if (objX != null)
                        {
                            if (objX.ToString() == "0" || objX.ToString().Trim() == "")
                            {
                                bWHIsOpen = false;
                                tbX = dsX.Tables["data"];
                                cmb_cWHId.DataSource = tbX;
                                cmb_cWHId.DisplayMember = "cName";
                                cmb_cWHId.ValueMember = "cWHId";
                                bWHIsOpen = true;
                                if (cmb_cWHId.Items.Count == 1)
                                {
                                    cmb_cWHId_SelectedValueChanged(null, null);
                                }
                                else
                                {
                                    cmb_cWHId.SelectedIndex = -1;
                                }
                            }
                        }
                    }
                }
            }

            private void OpenWAreaData(string sCmptId, string sWHId)
            {
                string sW = sWHId;
                string sErr = "";
                if (sW.Trim() == "")
                {
                    sW = "%";
                }
                string sSql = "select * from TWC_WArea where cWHId like '" + sW.Trim() + "' and bUsed=1";
                if (sCmptId.Trim().Length > 0)
                {
                    sSql += " and cCmptId='" + sCmptId.Trim() + "'";
                }                
                DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, false, sSql, out sErr);
                if (sErr.Trim() != "0" && sErr.Trim() != "")
                {
                    MessageBox.Show(sErr);
                }
                else
                {
                    DataTable tbX = null;
                    tbX = dsX.Tables["result"];
                    if (tbX != null)
                    {
                        object objX = tbX.Rows[0][1];
                        if (objX != null)
                        {
                            if (objX.ToString() == "0" || objX.ToString().Trim() == "")
                            {
                                bWHIsOpen = false;
                                tbX = dsX.Tables["data"];
                                grdAreaColor.DataSource = null;
                                grdAreaColor.DataSource = tbX;
                            }
                        }
                    }
                }
            }

            private string GetFormTitle(string sMode, WareType whX)
            {
                string sX = "立库";
                switch (whX)
                {
                    case WareType.wt3D:
                        sX = "立库";
                        break;
                    case WareType.wt2D:
                        sX = "平库";
                        break;
                    case WareType.wtDPS:
                        sX = "DPS库";
                        break;
                    default :
                        sX = "立库";
                        break;
                }
                if(sMode.Trim() != "")
                {
                    sX = sX + "—" + sMode ;
                }
                return sX ;
            }

        #endregion
        #region 公共属性
            public bool BIsSelectCell //是否是选择货位
            {
                get { return (bIsSelectCell); }
                set
                {
                    bIsSelectCell = value;
                    if (bIsSelectCell)
                        Text = "选择货位";
                    else Text = "货位状态查看";
                    btnOK.Visible = bIsSelectCell;
                }
            }
            public bool BIsResultOK   //是否选择OK
            {
                get { return (bIsResultOK); }
                set { bIsResultOK = false; }
            }
            public string SelectResult   //选择结果
            {
                get { return (selectResult); }
                set { selectResult = value; }
            }
            
            /// <summary>
            /// selectOtherValue = PalletId-BoxId-MNo-BatchNo-Qty-Unit-BNoIn-ItemIn
            /// </summary>
            public string SelectOtherValue   //选择结果
            {
                get { return (selectOtherValue); }
                set { selectOtherValue = value; }
            }

            private string _CmptId = "101";
            public string CmptId
            {
                get { return _CmptId.Trim(); }
                set { _CmptId = value.Trim(); }
            }

            private WareType _WHType = WareType.wtNone;
            public WareType WTWareType
            {
                get { return _WHType; }
                set
                {
                    _WHType = value;
                    Text = GetFormTitle("",_WHType) + "—" +"平面仓位状态图";
                }
            }

            private string _WHId = "";
            public string WHId
            {
                get { return _WHId.Trim(); }
                set { _WHId = value.Trim(); }
            }

            private string _PosId = "";
            public string PosId
            {
                get { return _PosId.Trim(); }
            }

            private string _PalletId = "";
            public string PalletId
            {
                get { return _PalletId.Trim(); }
            }

        #endregion
        #region 公共方法
        #endregion

        public frmWareCellState()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            bIsResultOK = false;
            Close();
        }

        private void cmb_cWHId_SelectedValueChanged(object sender, EventArgs e)
        {
            pnlCells.Controls.Clear();
            if (cmb_cWHId.Items.Count < 0) return;
            if (!bWHIsOpen) return;
            object objX = cmb_cWHId.SelectedValue;
            if (objX == null) return;
            string sErr = "";
            string sWHId = objX.ToString().Trim();
            if (sWHId.Length == 0) return;
            string sSql = "select max(nRow) nRow ,max(nCol) nCol,max(nLayer) nLayer from TWC_WareCell where cWHId='"+ sWHId +"'";
            DataSet dsX = PubDBCommFuns.GetDataBySql(AppInformation.SvrSocket, sSql, "data", "", out sErr);
            if (sErr.Trim() != "" && sErr.Trim() != "0")
            {
                if (dsX != null)
                    dsX.Clear();
                return;
            }
            DataTable tbX = dsX.Tables["data"];
            if (tbX == null) return;
            if (tbX.Rows.Count == 0) return;
            if (tbX.Rows[0]["nRow"].ToString() == "")
            {
                dsX.Clear();
                return;
            }
            int nRow = int.Parse(tbX.Rows[0]["nRow"].ToString());
            bRowIsOK = false;
            cmb_nRow.Items.Clear();
            for (int iX = 1; iX <= nRow; iX++)
            {
                cmb_nRow.Items.Add(iX);
            }
            bRowIsOK = true;
            int nCol = int.Parse(tbX.Rows[0]["nCol"].ToString());
            int nLayer = int.Parse(tbX.Rows[0]["nLayer"].ToString());
            CreateCellButtons(sWHId,pnlCells,nCol,nLayer);
            //
            string sCmptId = "101";
            if (UserInformation.UserId != "90101001")
            {
                sCmptId = UserInformation.UnitId;
            }
            //OpenWAreaData(sCmptId, sWHId.Trim());
        }

        private void cmb_nRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_nRow.SelectedIndex < 0) return;
            if (!bRowIsOK) return;
            SetCellStates(cmb_cWHId.SelectedValue.ToString().Trim(), int.Parse(cmb_nRow.Text.Trim()));
        }

        private void frmWareCellState_Load(object sender, EventArgs e)
        {
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
            btnOK.Visible = bIsSelectCell;
            grdAreaColor.AutoGenerateColumns = false;
            grdList.AutoGenerateColumns = false;
            Text = GetFormTitle("", _WHType) + "—" + "平面仓位状态图";
            OpenWareHouseList(_CmptId, (int)_WHType);
            if (cmb_cWHId.Items.Count > 0)
            {
                if (_WHId.Trim() != "")
                {
                    cmb_cWHId.SelectedValue = _WHId.Trim();
                    cmb_cWHId_SelectedValueChanged(null, null);
                }
            }
            if (Application.RenderWithVisualStyles)
            {
                System.Windows.Forms.VisualStyles.VisualStyleState vsX = Application.VisualStyleState;

                string sX = vsX.ToString();
            }
        }

        private void btnCell_Click(object sender, EventArgs e)
        {
            object objX = null;
            if (btnCurr == null)
            {
                btnCurr = (Button)sender;
                btnCurr.FlatAppearance.BorderColor = clrActive;
            }
            else
            {
                btnCurr.FlatAppearance.BorderColor = clrUnActive;
                btnCurr = (Button)sender;
                btnCurr.FlatAppearance.BorderColor = clrActive;
            }
            objX = btnCurr.Tag;
            if (objX == null) return;
            //MessageBox.Show(objX.ToString());
            OpenCellItemList(objX.ToString());

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnCurr == null)
            {
                bIsResultOK = false;
                MessageBox.Show("当前没有选择！");
                return;
            }
            if (btnCurr.Tag == null)
            {
                bIsResultOK = false;
                MessageBox.Show("该仓位不可用！");
                return;
            }
            _PosId = btnCurr.Tag.ToString().Trim();
            _PalletId = GetCellPalletId(_PosId.Trim());
            if (_PosId.Trim() == "" || _PosId.Trim() == "0")
            {
                MessageBox.Show("所选择货位为空，请选择货位！");
                return;
            }
            if (cmb_nRow.Text.Trim() == "" || cmb_nRow.Text.Trim() == "0")
            {
                MessageBox.Show("请先选择排号后，再选择对应的货位！");
                cmb_nRow.Focus();
                return;
            }
            if (bdsList.Count > 0)
            {
                DataRowView drv = (DataRowView) bdsList.Current ;
                if (drv != null)
                {
                    bIsResultOK = true;
                    selectResult = btnCurr.Tag.ToString();
                    selectOtherValue = drv["nPalletId"].ToString().Trim() + "-" + drv["cBoxId"].ToString().Trim() + "-" +
                        drv["cMNo"].ToString().Trim() + "-" + drv["cBatchNo"].ToString().Trim() + "-" + drv["fQty"].ToString() + "-" +
                        drv["cUnit"].ToString() + "-" + drv["cBNoIn"].ToString().Trim() + "-" + drv["nItemIn"].ToString();
                }
                Close();
            }
            else
            {
                bIsResultOK = true;
                selectResult = btnCurr.Tag.ToString();
                Close();
            }
        }

        private void grdAreaColor_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                string sX = "0";
                if (e.Value != null)
                {
                    sX = e.Value.ToString();
                }
                if (sX.Trim() == "") sX = "0";
                Color clrCell = Color.FromArgb(int.Parse(sX));
                e.CellStyle.BackColor = clrCell;
                e.CellStyle.ForeColor = clrCell;
                e.CellStyle.SelectionBackColor = clrCell;
                e.CellStyle.SelectionForeColor = clrCell;
                //using (SolidBrush brush = new SolidBrush(color))
                //{
                //    e.Graphics.FillRectangle(brush, e.CellBounds);
                //    e.Handled = true;
                //} 


                //pen.Dispose();
            }
        }

        private void mi_EditStoreQty_Click(object sender, EventArgs e)
        {
            if (bdsList.Count == 0)
            {
                MessageBox.Show("对不起，无库存数据可修改！");
                return;
            }
            DataRowView drv = null;
            drv = (DataRowView)bdsList.Current;
            if (drv == null) return;

            string sPosId = btnCurr.Tag.ToString();
            //MessageBox.Show(sPosId);
            string sPalletId = drv["nPalletId"].ToString();
            string sBoxId = drv["cBoxId"].ToString();
            string sBNoIn = drv["cBNoIn"].ToString();
            string sItemIn = drv["nItemIn"].ToString();
            string sWHId = drv["cWHId"].ToString();
            string sMNo = drv["cItemId"].ToString();
            string sBatchNo = drv["cBatchNo"].ToString();
            string sWHIdErp = drv["cWHIdErp"].ToString();
            string sAreaIdErp = drv["cAreaIdErp"].ToString();
            string sPosIdErp = drv["cPosIdErp"].ToString();
            double fQty = 0;
            fQty = Convert.ToDouble(drv["fQty"]);
            string sReal = "";
            double fReal = 0;
            if (UI.UIPubMethode.InputMessage("请录入实际库存数量：", "修改库存数量", fQty.ToString(), UI.InputMsgType.ittReal, out sReal))
            {
                fReal = double.Parse(sReal);
            }
            else
            {
                return;
            }
            if (fReal < 0)
            {
                MessageBox.Show("对不起，修改的数量不能为负数！");
                return;
            }
            string sErr = "";
            if (MessageBox.Show("您确定要将库存数量：" + fQty.ToString() + " 改为：" + fReal.ToString() + " 吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sId = DBFuns.SP_Ajust_UpdateStoreQty(AppInformation.SvrSocket, UserInformation.UserId, UserInformation.UnitId, "WMS", sPalletId, sBoxId, sMNo, fReal, sBNoIn, sItemIn, sBNoAjust, out sErr);
                if (sId.Trim() == "B")
                {
                    sBNoAjust = sErr;
                    MessageBox.Show("已修改成功，请注意审核调整单：" + sBNoAjust);
                }
                else
                {
                    MessageBox.Show(sErr);
                }
            }
           
        }
    }
}

