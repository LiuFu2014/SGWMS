using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;


namespace DataImporter
{
    public partial class frmInBillImp : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID); 

        #region 私有变量
            int nRows = 0;
            int nCols = 0;
            int nCurrR = 0;
            int nCurrC = 0;
            //
            int nTagCol = 7; //区分物料款式的列号
            int nStartRowGrid=4;//网格的开始行号
            int nStartColGrid = 1;//网格开始的列号
            int nEndRowForStyle = 0;//记录物料款式的结束行号
            //
            private ApplicationClass appExcel = null;
            private Workbook wkBook = null;
            private Worksheet wkSheet = null;
            private Range range = null;

            DateTime dBegin;
            DateTime dEnd;
        #endregion

        #region 私有方法

        /// <summary>
        /// 获取款式结束的行号
        /// </summary>
        /// <returns></returns>
        private int GetStyleEndRowNo()
        {
            int nRow = 0;
            if (wkSheet == null) return 0;
            int nX = nStartRowGrid;
            bool bIsFind = false;
            while (!bIsFind && nX < 100)
            {
                string sX = "";
                range = (Range)wkSheet.Cells[nX, nTagCol];
                object objX = range.Text;
                if (objX != null)
                {
                    sX = objX.ToString();
                }
                if (sX.Trim() == "")
                {
                    nRow = nX;
                    break;
                }
                nX++;
            }
            return nRow;
        }

        /// <summary>
        /// 获取某物料对应的款式的行号
        /// </summary>
        /// <param name="sStyleTag"></param>
        /// <returns></returns>
        private int GetMatStyleRowNo(string sStyleTag)
        {
            int nRow = 0;
            if (wkSheet == null) return 0;
            for (int nX = nStartRowGrid; nX < nEndRowForStyle; nX++)
            {
                string sX = "";
                range  = (Range) wkSheet.Cells[nX, nTagCol];
                object objX = range.Text ;
                if (objX != null)
                {
                    sX = objX.ToString().Trim();
                }
                if (sStyleTag.Trim().ToLower() == sX.Trim().ToLower())
                {
                    nRow = nX;
                    break;
                }
            }
            return nRow;
        }

        private bool CheckConnectionIsOK(OleDbConnection conX)
        {
            bool bOK = false;
            if (conX == null) return false;
            if(conX.ConnectionString.Trim() == "") 
            {
                MessageBox.Show("对不起，连接字符串为空！");
                return  false ;
            }
            try
            {
                if (conX.State == ConnectionState.Closed)
                {
                    conX.Open();
                }
                bOK = true;
            }
            catch (Exception err)
            {
                bOK = false;
                MessageBox.Show(err.Message);
            }
            return bOK;
        }

        private bool CheckWMSMNoIsExists(string sMNo, string sStyle)
        {
            bool bOK = false;
            string sSql = "select count(*) nCount from TPC_Material where cMNo like '"+ sMNo.Trim() +"%' and cSpec='"+ sStyle.Trim() +"'";
            using (OleDbCommand cmdX = new OleDbCommand(sSql))
            {
                try
                {
                    cmdX.Connection = wmsConn;
                    if (wmsConn.State == ConnectionState.Closed)
                    {
                        wmsConn.Open();
                    }
                    //cmdX.CommandText = sSql;
                    cmdX.CommandType = CommandType.Text;
                    object objX = cmdX.ExecuteScalar();
                    if (objX == null)
                    {
                        bOK = false;
                    }
                    else 
                    {
                        bOK = Convert.ToInt32(objX) > 0;
                    }
                }
                catch (Exception err)
                {
                    bOK = false;
                    MessageBox.Show(err.Message);
                }
            }
            return bOK;
        }

        private string GetNewWMSNo(string sMNo)
        {
            // MNo = No + 000001
            string sWMSNo = "";
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select nvl(max(cast(nvl(substr(cMNo,-6,6),0) as int)),0) nMax from TPC_Material where cMNo like '" + sMNo + "%'");
            using (OleDbCommand cmdX = new OleDbCommand(sSql.ToString()))
            {
                try
                {
                    cmdX.Connection = wmsConn;
                    if (wmsConn.State == ConnectionState.Closed)
                    {
                        wmsConn.Open();
                    }
                    //cmdX.CommandText = sStyle;
                    cmdX.CommandType = CommandType.Text;
                    object objX = cmdX.ExecuteScalar();
                    if (objX == null)
                    {
                        sWMSNo = "";
                    }
                    else
                    {
                        int nMax  = Convert.ToInt32(objX);
                        sWMSNo = sMNo + (nMax+1).ToString("D6");
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            return sWMSNo;
        }

        /// <summary>
        /// 物料类型（物质属性分类）
        /// </summary>
        /// <param name="sMNo"></param>
        /// <returns></returns>
        private string GetMatType1(string sMNo)
        {
            string sMType = "";
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select cTypeId from TPC_MaterialType where cTypeMode=0 and  cTypeId like '"+ sMNo +"%' and rownum=1");
            using (OleDbCommand cmdX = new OleDbCommand(sSql.ToString()))
            {
                try
                {
                    #region
                    cmdX.Connection = wmsConn;
                    if (wmsConn.State == ConnectionState.Closed)
                    {
                        wmsConn.Open();
                    }
                    //cmdX.CommandText = sStyle;
                    cmdX.CommandType = CommandType.Text;
                    object objX = null;
                    objX = cmdX.ExecuteScalar();
                    if (objX == null)
                    {
                        sMType = "";
                    }
                    else
                    {
                        sMType = objX.ToString();
                    }
                    #endregion

                    if (sMType.Trim() == "")
                    {
                        sSql.Remove(0, sSql.Length);
                        sSql.Append("select cTypeId from TPC_MaterialType where cTypeMode=0  and rownum=1");
                        cmdX.CommandType = CommandType.Text;
                        cmdX.CommandText = sSql.ToString();
                        objX = cmdX.ExecuteScalar();
                        sMType = objX.ToString();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            return sMType;
        }

        /// <summary>
        /// 会计属性分类
        /// </summary>
        /// <param name="sMNo"></param>
        /// <returns></returns>
        private string GetMatType2(string sMNo)
        {
            string sMType = "";
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select cTypeId from TPC_MaterialType where cTypeMode=1 and rownum=1");
            using (OleDbCommand cmdX = new OleDbCommand(sSql.ToString()))
            {
                try
                {
                    #region
                    cmdX.Connection = wmsConn;
                    if (wmsConn.State == ConnectionState.Closed)
                    {
                        wmsConn.Open();
                    }
                    //cmdX.CommandText = sStyle;
                    cmdX.CommandType = CommandType.Text;
                    object objX = null;
                    objX = cmdX.ExecuteScalar();
                    if (objX == null)
                    {
                        sMType = "";
                    }
                    else
                    {
                        sMType = objX.ToString();
                    }
                    #endregion                    
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            return sMType;
        }

        private string GetMatMNo(string sMNo,string sStyle)
        {
            string sWMSNo = "";
            StringBuilder sSql = new StringBuilder("");
            sSql.Append("select cMNo from TPC_Material where cMNo like '"+ sMNo.Trim() +"%' and cSpec='"+ sStyle.Trim() +"'");
            using (OleDbCommand cmdX = new OleDbCommand(sSql.ToString()))
            {
                try
                {
                    #region
                    cmdX.Connection = wmsConn;
                    if (wmsConn.State == ConnectionState.Closed)
                    {
                        wmsConn.Open();
                    }
                    //cmdX.CommandText = sStyle;
                    cmdX.CommandType = CommandType.Text;
                    object objX = null;
                    objX = cmdX.ExecuteScalar();
                    if (objX == null)
                    {
                        sWMSNo = "";
                    }
                    else
                    {
                        sWMSNo = objX.ToString();
                    }
                    #endregion
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            return sWMSNo;
        }
        private string GetNewBillNo()
        {
            string sTbName = "TWB_BillIn";
            string sFldKey = "cBNo";
            string sHead = "";
            string sBNo = "";
            if (isBillIn)
            {
                sHead="BI" + DateTime.Now.ToString("yyMMdd");
            }
            else
            {
                sHead = "BO" + DateTime.Now.ToString("yyMMdd");
            }            
            using (OleDbCommand cmdX = new OleDbCommand())
            {
                try
                {
                    #region
                    cmdX.Connection = wmsConn;
                    if (wmsConn.State == ConnectionState.Closed)
                    {
                        wmsConn.Open();
                    }
                    cmdX.CommandText = "sp_GetNewId";
                    cmdX.CommandType = CommandType.StoredProcedure;
                    OleDbParameter par = null;
                    #region
                    par = cmdX.Parameters.Add("pTbName", OleDbType.VarChar) ;
                    par.Direction = ParameterDirection.Input;
                    par.Value = sTbName;
                    //--
                    par = cmdX.Parameters.Add("pFldKey", OleDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = sFldKey;
                    //--
                    par = cmdX.Parameters.Add("pLen", OleDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = 12;
                    //--
                    par = cmdX.Parameters.Add("pReplaceChar", OleDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = "0";
                    //--
                    par = cmdX.Parameters.Add("pHeader", OleDbType.VarChar);
                    par.Direction = ParameterDirection.Input;                    
                    par.Value = sHead;
                    //--
                    par = cmdX.Parameters.Add("pFldCon", OleDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = "";
                    //--
                    par = cmdX.Parameters.Add("pValueCon", OleDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = "";
                    //--
                    #endregion
                    //--
                    OleDbDataReader dtReader = null;
                    dtReader = cmdX.ExecuteReader();
                    if (dtReader.HasRows)
                    {
                        dtReader.Read();
                        sBNo = dtReader[0].ToString();
                        dtReader.Close();
                    }
                    #endregion
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            return sBNo;
        }

        public static void KillProgress(IntPtr hHwnd)
        {
            int k = 0;
            try
            {
                GetWindowThreadProcessId(hHwnd, out k); //得到本进程唯一标志k             
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); //得到对进程k的引用 
                p.Kill(); //关闭进程k 
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        #endregion

        #region 公共属性
        private OleDbConnection wmsConn = null;
        public OleDbConnection WMSConn
        {
            get { return wmsConn; }
            set { wmsConn = value; }
        }

        private bool isBillIn = true;
        public bool IsBillIn
        {
            get { return isBillIn; }
            set
            {
                isBillIn = value;                
            }
        }

        private string userId = "";
        public string UserId
        {
            get { return userId; }
            set { userId = value;  }
        }

        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion

        public frmInBillImp()
        {
            InitializeComponent();
        }

        private void tlb_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlb_Import_Click(object sender, EventArgs e)
        {
            #region
            if (dlg_OpenFile.ShowDialog() == DialogResult.No)
            {
                return;
            }
            if (dlg_OpenFile.FileNames.Length == 0)
            {
                return;
            }
            #endregion
            StringBuilder sSql = new StringBuilder("");
            dBegin = DateTime.Now;
            //if (appExcel == null)
            //{
                appExcel = new ApplicationClass();
             
            //}
            foreach (string sFileName in dlg_OpenFile.FileNames)
            {
                #region 打开Excel WorkBook 对象
                if (wkBook != null)
                {
                    wkBook.Close(Missing.Value, Missing.Value, Missing.Value);
                    wkBook = null;
                }
                wkBook =(WorkbookClass) appExcel.Workbooks.Open(sFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                #endregion

                #region 处理数据
                string sBillNo = ""; //一个文件一张单据
                int nItem = 1;  //一张单的明细序号
                if (wkBook != null)
                {
                    int nX = wkBook.Sheets.Count;
                    try
                    {
                        wkSheet = (Worksheet)wkBook.Sheets.get_Item(1); ;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                    nRows= wkSheet.Rows.Count;
                    //
                    nCols = wkSheet.Columns.Count ;
                    if (nCols > 100)
                    {
                        nCols = 100;
                    }
                    nEndRowForStyle = GetStyleEndRowNo();
                    //开始读取物料
                    nCurrR = nEndRowForStyle + 1;
                    bool bIsEnd = false ;
                    while (!bIsEnd && nCurrR < nRows)
                    {
                        #region 处理一行物料数据
                        object objX = null;
                        range =(Range) wkSheet.Cells[nCurrR, nStartColGrid];
                        objX = range.Text;
                        string sMNo = "";
                        #region 物料编号
                        if (objX != null)
                        {
                            sMNo = objX.ToString().Trim();
                        }
                        if (sMNo == "")
                        {
                            bIsEnd = true;
                            break;
                        }
                        #endregion

                        string sMName = "";
                        #region 物料名称
                        objX = null;
                        range = (Range)wkSheet.Cells[nCurrR, nStartColGrid + 1];
                        objX = range.Text;
                        if (objX != null)
                        {
                            sMName = objX.ToString().Trim();
                        }
                        if (sMName.Trim() == "")
                        {
                            bIsEnd = true;
                            break;
                        }
                        #endregion

                        string sUnit = "";
                        #region 计量单位
                        objX = null;
                        range = (Range)wkSheet.Cells[nCurrR, nStartColGrid + 2];
                        objX = range.Text;
                        if (objX != null)
                        {
                            sUnit = objX.ToString().Trim();
                        }
                        if (sUnit.Trim() == "")
                        {
                            bIsEnd = true;
                            break;
                        }
                        #endregion

                        string sQtyBox = "";
                        #region 满盘数量
                        objX = null;
                        range = (Range)wkSheet.Cells[nCurrR, nStartColGrid + 4];
                        objX = range.Text;
                        if (objX != null)
                        {
                            sQtyBox = objX.ToString().Trim();
                        }
                        if (sQtyBox.Trim() == "")
                        {
                            bIsEnd = true;
                            break;
                        }
                        #endregion

                        string sStyleTag = "";
                        #region 款式标记
                        objX = null;
                        range = (Range)wkSheet.Cells[nCurrR, nTagCol];
                        objX = range.Text;
                        if (objX != null)
                        {
                            sStyleTag = objX.ToString().Trim();
                        }
                        if (sStyleTag.Trim() == "")
                        {
                            bIsEnd = true;
                            break;
                        }
                        #endregion

                        //根据款式标记，确定款式的行号
                        int nStyleRow = GetMatStyleRowNo(sStyleTag);
                        if (nStyleRow == 0)
                        {
                            bIsEnd = true;
                            break;
                        }

                        #endregion

                        //开始读取物料数量
                        bool bWMSMNoIsOK = false;                        
                        
                        int nBClass = 1;
                        string sBType = "101";
                        #region 确定单据
                        if (isBillIn)
                        {
                            nBClass = 1;
                            sBType = "101";
                        }
                        else
                        {
                            nBClass = 2;
                            sBType = "201";
                        }
                        #endregion
                        string sLinkId = "";
                        range = (Range)wkSheet.Cells[nStartRowGrid - 1, 2];
                        objX = range.Text;
                        if (objX != null)
                        {
                            sLinkId = objX.ToString();
                        }
                        #region 处理物料的各条款式数量
                        for (int nCX = nTagCol + 1; nCX < nCols;nCX ++ )
                        {
                            #region 获取数量
                            objX = null;
                            range = (Range)wkSheet.Cells[nCurrR, nCX];
                            objX = range.Text;
                            if (objX == null)
                            {
                                continue;
                            }
                            double fQty = 0;
                            try
                            {
                                fQty = Convert.ToDouble(objX);
                            }
                            catch (Exception err)
                            {
                                continue;
                            }
                            #endregion
                            if (fQty == 0) continue;
                            #region
                            //对应的款式
                            string sStyleCurr = "";
                            range = (Range)wkSheet.Cells[nStyleRow, nCX];
                            objX = range.Text;
                            if (objX == null) continue;
                            sStyleCurr = objX.ToString().Trim();
                            if (sStyleCurr == "") continue;
                            //检测物料是否存在（此物料 由原物料+款式号，共同确定WMS的物料）
                            string sWMSMNo = "";
                            bWMSMNoIsOK = CheckWMSMNoIsExists(sMNo, sStyleCurr);
                            if (!bWMSMNoIsOK)
                            {
                                sWMSMNo = GetNewWMSNo(sMNo);
                            }
                            #endregion
                            if (bWMSMNoIsOK && sWMSMNo.Trim() == "")
                            {
                                sWMSMNo = GetMatMNo(sMNo,sStyleCurr);
                            }
                            using (OleDbCommand cmdX = new OleDbCommand())
                            {
                                #region
                                if (!bWMSMNoIsOK)
                                {
                                    #region 插入物料信息表 SQL
                                    string sPYJM = "";
                                    string sWBJM = "";
                                    string sType1 = "";
                                    sType1 = GetMatType1(sMNo);
                                    string sType2 = "";
                                    sType2 = GetMatType2(sMNo);
                                    sPYJM = Text.TextPYWB.GetWBPY(sMName, Text.PYWBType.pwtPYFirst);
                                    sWBJM = Text.TextPYWB.GetWBPY(sMName, Text.PYWBType.pwtWBFirst);
                                    sSql.Remove(0, sSql.Length);
                                    sSql.Append("insert into TPC_Material(cMNo,cName,cSpec,cUnit,cTypeId1,cTypeId2,fWeight,fVolume,fSafeQtyDN,fSafeQtyUp,nKeepDay,");
                                    sSql.Append("bIsFromErp,dCreateDate,cCreator,fQtyBox,fDPSInQtyDn,fDPSInQtyUp,nPLaceMode,cLinkId,bIsMixPalce,cBorCode,bIsQC,");
                                    sSql.Append("cPYJM,cWBJM,nTag,bIsSubQtyForQC,nAutoPromptDay,bIsAutoBatchNo,cMatStyle,cMatOther,cSupplier,cMatQCLevel,");
                                    sSql.Append("cRemark,cABC,cCSId,nMatClass,bIsPackage,fPackgeQty,bIsMixBatchNo,cPalletSpecId,cAreaId,bIsSameMatClassIn,");
                                    sSql.Append("bDataIsIntType) ");
                                    //
                                    sSql.Append("Values(");
                                    // 
                                    sSql.Append("'" + sWMSMNo + "','" + sMName + "','" + sStyleCurr + "','" + sUnit + "','" + sType1 + "','" + sType2 + "',0,0,100,5000,720,");
                                    sSql.Append("1,sysdate,'ERP','" + sQtyBox + "',0,0,0,'" + sMNo + "',0,'" + sMNo + "',0,");
                                    sSql.Append("'" + sPYJM + "','" + sWBJM + "',0,0,30,0,'','','','',");
                                    sSql.Append("'','','',0,1,12,1,'','',1,1");
                                    sSql.Append(")");
                                    #endregion

                                    #region 插入物料信息表执行语句
                                    cmdX.Connection = wmsConn;
                                    if (wmsConn.State != ConnectionState.Open)
                                    {
                                        wmsConn.Open();
                                    }
                                    cmdX.CommandText = sSql.ToString();
                                    cmdX.CommandType = CommandType.Text;
                                    try
                                    {
                                        cmdX.ExecuteNonQuery();
                                    }
                                    catch (Exception err)
                                    {
                                        MessageBox.Show(err.Message);
                                    }
                                    #endregion
                                }
                                //生成入库单据数据
                                if (sBillNo.Trim() == "")
                                {
                                    #region 产生并插入新的主单表
                                    sBillNo = GetNewBillNo();
                                    sSql.Remove(0, sSql.Length);
                                    sSql.Append("insert into TWB_BillIn(nBClass,cBTypeId,cBNo,cLinkId,dDate,cWHId,cDept,cPayer,cRemark,bIsChecked,dCreateDate,");
	                                sSql.Append("cCreator,nPStatus,cCmptId,bIsFinished,cUserId)");
                                    sSql.Append("Values(");
                                    sSql.Append(nBClass.ToString() + ",'"+ sBType +"','"+ sBillNo +"','"+ sLinkId +"',sysdate,'','仓库中心','"+ userName +"','',0,sysdate,");
                                    sSql.Append("'"+ userName +"',0,'101',0,'"+ userId +"'");
                                    sSql.Append(")");
                                    cmdX.CommandText = sSql.ToString();
                                    cmdX.CommandType = CommandType.Text;
                                    cmdX.Connection = wmsConn;
                                    try
                                    {
                                        if (wmsConn.State == ConnectionState.Closed)
                                        {
                                            wmsConn.Open();
                                        }
                                        cmdX.ExecuteNonQuery();
                                    }
                                    catch (Exception err)
                                    {
                                        MessageBox.Show("新建单失败：" + err.Message);
                                    }
                                    #endregion
                                }
                                //明细
                                if (sBillNo != "")
                                {
                                    #region
                                    sSql.Remove(0, sSql.Length);
                                    sSql.Append("insert into TWB_BillInDtl(cBNo,nItem,cMNo,cBatchNo,fQty,fPallet,fFinished,nQCStatus,nPStatus,nDoStatus,cUnit,dProdDate,");
	                                sSql.Append("cCmptId,nFromItem,cRemark,dBadDate,cSysBatchNo,cBNoIn,nItemIn,cFromNo,cLinkId,cLinkItem,cSupplier,cCSId)");
                                    sSql.Append("Values(");
                                    sSql.Append("'"+ sBillNo +"',"+ nItem.ToString() +",'"+ sWMSMNo +"','',"+ fQty.ToString() +",0,0,1,0,0,'"+ sUnit +"',sysdate,");
                                    if (isBillIn)
                                    {
                                        sSql.Append("'101',0,'导入生成',(sysdate + 720),'" + DateTime.Now.ToString("yyyyMMdd") + "','"+ sBillNo +"',"+ nItem.ToString() +",'','"+ sLinkId +"','"+ nItem.ToString() +"','',''");
                                    }
                                    else
                                    {
                                        sSql.Append("'101',0,'导入生成',(sysdate + 720),'','',0,'','" + sLinkId + "','" + nItem.ToString() + "','',''");
                                    }
                                    sSql.Append(")");
                                    cmdX.CommandText = sSql.ToString();
                                    cmdX.CommandType = CommandType.Text;
                                    cmdX.Connection = wmsConn;
                                    try
                                    {
                                        if (wmsConn.State == ConnectionState.Closed)
                                        {
                                            wmsConn.Open();
                                        }
                                        cmdX.ExecuteNonQuery();
                                    }
                                    catch (Exception err)
                                    {
                                        MessageBox.Show(err.Message);
                                    }
                                    #endregion
                                    nItem++;
                                }
                                #endregion
                            }
                        }
                        #endregion

                        nCurrR++;
                        
                    }
                }
                #endregion

                //关闭
                //导完后，另存为已导文件，并将原文件删除
                if (!sFileName.Contains("_over"))
                {
                    if (wkBook != null)
                    {
                        string sFinishedFile = sFileName;
                        sFinishedFile = sFinishedFile.Replace(".xls", "_over.xls");
                        try
                        {
                            wkBook.SaveAs(sFinishedFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    }
                    System.IO.File.Delete(sFileName);
                }
                wkBook.Close(Missing.Value, Missing.Value, Missing.Value);
                wkBook = null;
            }
            //关掉Excel应用程序
            #region
            if (appExcel.Workbooks.Count > 0)
            {
                appExcel.Workbooks.Close();
            }
            appExcel.Quit();
            KillProgress((IntPtr)appExcel.Hwnd);
            #endregion

        }

        private void frmInBillImp_Load(object sender, EventArgs e)
        {
            if (isBillIn)
            {
                Text = "入库单导入（Excel）";
            }
            else 
            {
                Text = "出库单导入（Excel）";
            }
        }
    }
}