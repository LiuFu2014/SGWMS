using SunEast;
using System;
using System.Data;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using DBCommInfo;

namespace SunEast.App
{

    public static class PubDBCommFuns
    {
        public static bool DoExecSql(Socket sktX, string sSql, string sFldsDate, out string sErr)
        {
            bool flag = false;
            sErr = "";
            DataSet set = GetDataBySql(sktX, sSql, "data", sFldsDate, out sErr);
            if (((set == null) || (set.Tables["data"] == null)) || ((sErr.Trim() != "") && (sErr.Trim() != "0")))
            {
                return false;
            }
            if ((set.Tables["result"] == null) || (set.Tables["result"].Rows[0]["returncode"] == null))
            {
                return false;
            }
            object obj2 = set.Tables["result"].Rows[0]["returncode"];
            if (obj2.ToString().Trim() != "0")
            {
                flag = false;
                sErr = set.Tables["result"].Rows[0]["returndesc"].ToString();
                return flag;
            }
            return true;
        }

        public static DataSet GetDataBySql(string sSql, out string sErr)
        {
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return null;
            }
            return GetDataBySql(null, false, sSql, out sErr);
        }

        public static DataSet GetDataBySql(bool bIsSaveDataxml, string sSql, out string sErr)
        {
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return null;
            }
            return GetDataBySql(null, bIsSaveDataxml, sSql, out sErr);
        }

        public static DataSet GetDataBySql(string sSql, string sFldsDate, out string sErr)
        {
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return null;
            }
            return GetDataBySql(null, false, sSql, "data", 0, 0, sFldsDate, out sErr);
        }

        public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, out string sErr)
        {
            DataSet set = null;
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return set;
            }
            return GetDataBySql(sktX, bIsSaveDataxml, sSql, "data", 0, 0, out sErr);
        }

        public static DataSet GetDataBySql(Socket sktX, string sSql, string sFldsDate, out string sErr)
        {
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return null;
            }
            return GetDataBySql(sktX, false, sSql, "data", 0, 0, sFldsDate, out sErr);
        }

        public static DataSet GetDataBySql(Socket sktX, string sSql, string sTbName, string sFldsDate, out string sErr)
        {
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return null;
            }
            return GetDataBySql(sktX, false, sSql, sTbName, 0, 0, sFldsDate, out sErr);
        }

        public static DataSet GetDataBySql(string sSql, string sTbName, int nPageSize, int nPageIndex, out string sErr)
        {
            DataSet set = null;
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return set;
            }
            return GetDataBySql(null, false, sSql, sTbName, nPageSize, nPageIndex, out sErr);
        }

        public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, out string sErr)
        {
            DataSet set = null;
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return set;
            }
            return GetDataBySql(sktX, bIsSaveDataxml, sSql, sTbName, nPageSize, nPageIndex, "", out sErr);
        }

        public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, string sFldsDate, out string sErr)
        {
            sErr = "";
            if (sSql.Trim().Length == 0)
            {
                sErr = "SQL语句不能为空！";
                return null;
            }
            string str = "data";
            if (sTbName.Trim() != "")
            {
                str = sTbName.Trim();
            }
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = sSql,
                SqlType = SqlCommandType.sctSql,
                PageIndex = nPageIndex,
                PageSize = nPageSize,
                FromSysType = "dotnet",
                DataTableName = str,
                FldsData = sFldsDate.Trim()
            };
            SeDBClient client = new SeDBClient();
            return client.GetDataSet(sktX, cmdInfo, bIsSaveDataxml, out sErr);
        }

        public static string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader)
        {
            string sErr = "";
            return GetNewId(null, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
        }

        public static string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader, out string sErr)
        {
            sErr = "";
            return GetNewId(null, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
        }

        public static string GetNewId(Socket sktX, string sTbName, string sKeyFld, int nLength, string sHeader, out string sErr)
        {
            sErr = "";
            return GetNewId(sktX, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
        }

        public static string GetNewId(Socket sktX, string sTbName, string sKeyFld, int nLength, string sHeader, string sFldCon, string sValueCon, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetNewId :pTbName, :pFldKey, :pLen , :pReplaceChar, :pHeader, :pFldCon, :pValueCon ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pTbName",
                ParameterValue = sTbName,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pFldKey",
                ParameterValue = sKeyFld,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLen",
                ParameterValue = nLength.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pReplaceChar",
                ParameterValue = "0",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pHeader",
                ParameterValue = sHeader,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pFldCon",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pValueCon",
                ParameterValue = "",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["data"];
                if (table != null)
                {
                    str = table.Rows[0]["cNewId"].ToString();
                }
            }
            set.Clear();
            return str;
        }

        public static bool GetValueBySql(string sSql, string sFldsDate, string sFldValue, out object objValue, out string sErr)
        {
            objValue = null;
            sErr = "";
            return GetValueBySql(null, sSql, sFldsDate, sFldValue, out objValue, out sErr);
        }

        public static bool GetValueBySql(Socket sktX, string sSql, string sFldsDate, string sFldValue, out object objValue, out string sErr)
        {
            bool flag = false;
            sErr = "";
            objValue = null;
            DataSet set = GetDataBySql(sktX, sSql, sFldsDate, out sErr);
            if (sErr != "")
            {
                return flag;
            }
            DataTable table = null;
            table = set.Tables["result"];
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if ((obj2 != null) && (obj2.ToString() == "-1"))
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                set.Clear();
                return flag;
            }
            table = set.Tables["data"];
            if (table.Rows.Count <= 0)
            {
                return flag;
            }
            if (sFldValue.Trim().Length > 0)
            {
                objValue = table.Rows[0][sFldValue.Trim()];
            }
            else
            {
                objValue = table.Rows[0][0];
            }
            return true;
        }

        public static string p_Pack_DoPltDtlInAuto(Socket sktX, string pUser, int pIsDPS, int pIsDoHalfPallet, int pIsDoEmptyIn, int pOptStation, string pSysType, string pWHId, string pBNo, string pItem, string pMNo, string pBatchNo, double pQty, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "p_Pack_DoPltDtlInAuto :pUser, :pIsDPS, :pPalletLevel ,:pIsDoHalfPallet,:pIsDoEmptyIn, :pOptStation, :pSysType, :pWHId, :pBNo,:pItem,:pMNo,:pBatchNo,:pQty ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDPS",
                ParameterValue = pIsDPS.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDoHalfPallet",
                ParameterValue = pIsDoHalfPallet.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDoEmptyIn",
                ParameterValue = pIsDoEmptyIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptStation",
                ParameterValue = pOptStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Ajust_DeleteBillData(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Ajust_DeleteBillData :pUser,:pCmptId, :pSysFrom, :pBNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Ajust_DeleteBillDtl(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pBNo, int pItem, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Ajust_DeleteBillDtl :pUser,:pCmptId, :pSysFrom, :pBNo ,:pItem",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_DelChkB(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_DelChkB :pUser,:pCmptId, :pSysFrom, :pBNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_DoAjustFromChk(Socket sktX, string pUser, string pCheckNo, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_DoAjustFromChk :pUser, :pCheckNo,:pCmptId,:pSysFrom ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCheckNo",
                ParameterValue = pCheckNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_DoCheckTask(Socket sktX, string pUser, string pSysFrom, string pCmptId, string pPosId, int pStation, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_DoCheckTask :pUser,:pSysFrom,:pCmptId,:pPosId,:pStation,:pBNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStation",
                ParameterValue = pStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_DoChkBuilder(Socket sktX, string pUser, string pCmptId, string pCheckType, string pCheckTypeDesc, string pErpCheckId, string pWHId, string pMType, string pMTypeDesc, string pMNo, string pMNoDesc, string pPos, string pDateFrom, string pDateTo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_DoChkBuilder :pUser,:pCmptId,:pCheckType,:pCheckTypeDesc,:pErpCheckId,:pWHId , :pMType,:pMTypeDesc, :pMNo,:pMNoDesc, :pPos,:pDateFrom,:pDateTo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCheckType",
                ParameterValue = pCheckType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCheckTypeDesc",
                ParameterValue = pCheckTypeDesc,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pErpCheckId",
                ParameterValue = pErpCheckId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMType",
                ParameterValue = pMType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMTypeDesc",
                ParameterValue = pMTypeDesc,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNoDesc",
                ParameterValue = pMNoDesc,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPos",
                ParameterValue = pPos,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDateFrom",
                ParameterValue = pDateFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDateTo",
                ParameterValue = pDateTo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable sp_Chk_GetBillInDtlList(Socket sktX, string pWHId, string pMNo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_GetBillInDtlList :pWHId,:pMNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static DataTable sp_Chk_GetChkDtlByPltId(Socket sktX, string pPalletId, string pChkId, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_GetChkDtlByPltId :pPalletId, :pChkId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pChkId",
                ParameterValue = pChkId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static DataTable sp_Chk_GetChkPosList(Socket sktX, string pChkId, int pStatus, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_GetChkPosList :pChkId,:pStatus ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pChkId",
                ParameterValue = pChkId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static string sp_Chk_UpdtQtyFromAjust(Socket sktX, string pCmptId, string pUser, string pSysFrom, string pBNo, int pItem, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_UpdtQtyFromAjust :pCmptId,:pUser, :pSysFrom, :pBNo ,:pItem",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_UpdtQtyFromAjustB(Socket sktX, string pCmptId, string pUser, string pSysFrom, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_UpdtQtyFromAjustB :pCmptId,:pUser, :pSysFrom, :pBNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_WriteAjustDtl(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pAjustNo, string pWHId, string pPosId, string pPalletId, string pBoxId, string pMNo, double pDiff, string pBNoIn, int pItemIn, string pCheckNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_WriteChkDtl :pUser,:pCmptId,:pSysFrom,:pAjustNo,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBNoIn,:pItemIn,:pCheckNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pAjustNo",
                ParameterValue = pAjustNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDiff",
                ParameterValue = pDiff.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCheckNo",
                ParameterValue = pCheckNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                if (str != "0")
                                {
                                    sErr = str;
                                }
                                else
                                {
                                    sErr = "";
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Chk_WriteChkDtl(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pWHId, string pPosId, string pPalletId, string pBoxId, string pMNo, double pDiff, double pBad, string pUnit, string pBNoIn, int pItemIn, string pCheckNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_WriteChkDtl :pUserId,:pCmptId,:pSysFrom,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBad,:pUnit,:pBNoIn,:pItemIn,:pCheckNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDiff",
                ParameterValue = pDiff.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBad",
                ParameterValue = pBad.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUnit",
                ParameterValue = pUnit,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCheckNo",
                ParameterValue = pCheckNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                if (str != "0")
                                {
                                    sErr = str;
                                }
                                else
                                {
                                    sErr = "";
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_CreateBoxNo(Socket sktX, int pQty, int pNoLength, string pHeader, string pBoxType, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_CreateBoxNo :pQty, :pNoLength, :pHeader , :pBoxType ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pNoLength",
                ParameterValue = pNoLength.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pHeader",
                ParameterValue = pHeader,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxType",
                ParameterValue = pBoxType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_CreatePaleltNo(Socket sktX, int pQty, int pNoLength, string pHeader, string pAreaId, string pPltSpec, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_CreatePaleltNo :pQty, :pNoLength, :pHeader ,:pAreaId, :pPltSpec ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pNoLength",
                ParameterValue = pNoLength.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pHeader",
                ParameterValue = pHeader,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pAreaId",
                ParameterValue = pAreaId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPltSpec",
                ParameterValue = pPltSpec,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_CreatePosId(Socket sktX, string pWHId, int pRowFrom, int pRowTo, int pColFrom, int pColTo, int pLayerFrom, int pLayerTo, int pRowLen, int pColLen, int pLayerLen, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_CreatePosId :pWHId, :pRowFrom, :pRowTo , :pColFrom, :pColTo, :pLayerFrom, :pLayerTo,:pRowLen,:pColLen,:pLayerLen ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRowFrom",
                ParameterValue = pRowFrom.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRowTo",
                ParameterValue = pRowTo.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pColFrom",
                ParameterValue = pColFrom.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pColTo",
                ParameterValue = pColTo.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLayerFrom",
                ParameterValue = pLayerFrom.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLayerTo",
                ParameterValue = pLayerTo.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRowLen",
                ParameterValue = pRowLen.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pColLen",
                ParameterValue = pColLen.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLayerLen",
                ParameterValue = pLayerLen.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Do_WKTaskDtlIsOK(Socket sktX, int pWorkId, string pUser, string pCmptId, string pSysType, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Do_WKTaskDtlIsOK :pWorkId,:pUser,:pCmptId ,:pSysType",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DoAccont(Socket sktX, int pWorkId, int pIsDoModeByManual, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DoAccont :pWorkId, :pIsDoModeByManual, :pPalletLevel ,:pSysFrom ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDoModeByManual",
                ParameterValue = pIsDoModeByManual.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DoPWHAccont(Socket sktX, string pSysType, string pUser, string pBNo, int pItem, string pPosId, string pPalletId, string pBoxId, double pQty, string pBNoIn, int pItemIn, int pWorkId, string pCmptId, int pOptType, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DoPWHAccont :pSysType, :pUser, :pBNo , :pItem, :pPosId, :pPalletId,:pBoxId,:pQty,:pBNoIn , :pItemIn ,:pWorkId,:pCmptId,:pOptType",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DoTaskCMD(Socket sktX, int pWorkId, string pSysFrom, string pCmptId, string pUser, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DoTaskCMD :pWorkId, :pSysFrom,:pCmptId,:pUser ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                if (str != "0")
                                {
                                    sErr = table.Rows[0]["cResult"].ToString();
                                }
                                else
                                {
                                    sErr = "";
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DoTaskCMDForPW(Socket sktX, string pBNo, string pItem, string pPosId, string pPalletId, string pBoxId, string pMNo, string pBatchNo, string pBNoIn, string pItemIn, string pSysFrom, string pCmptId, string pUser, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DoTaskCMDForPW :pBNo,:pItem,:pPosId,:pPalletId,:pBoxId,:pMNo,:pBatchNo ,:pBNoIn,:pItemIn, :pSysFrom,:pCmptId,:pUser ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                if (str != "0")
                                {
                                    sErr = str;
                                }
                                else
                                {
                                    sErr = "";
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DPS_DeleteFillIn(Socket sktX, string pUser, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DPS_DeleteFillIn :pUser, :pBNo",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DPS_DoPWHOutTskFromOutB(Socket sktX, string pUser, string pBNo, int pIsBatchNoCtrl, int pIsQCCtrl, int pIsOutByReal, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DPS_DoPWHOutTskFromOutB :pUser, :pBNo, :pIsBatchNoCtrl , :pIsQCCtrl, :pIsOutByReal ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsBatchNoCtrl",
                ParameterValue = pIsBatchNoCtrl.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsQCCtrl",
                ParameterValue = pIsQCCtrl.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsOutByReal",
                ParameterValue = pIsOutByReal.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string SP_DPS_DPSDoAccount(Socket sktX, string pDpsAddr, string pUser, int pIsDoModeByManual, double pQty, int pMaxValue, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_DPS_DPSDoAccount :pDpsAddr, :pUser, :pIsDoModeByManual , :pQty, :pMaxValue ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pDpsAddr",
                ParameterValue = pDpsAddr,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDoModeByManual",
                ParameterValue = pIsDoModeByManual.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMaxValue",
                ParameterValue = pMaxValue.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable SP_DPS_DPSLoadTask(Socket sktX, string pAreaId, int pMaxValue, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_DPS_DPSLoadTask :pAreaId, :pMaxValue",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pAreaId",
                ParameterValue = pAreaId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMaxValue",
                ParameterValue = pMaxValue.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static string SP_DPS_DPSUpdateStatus(Socket sktX, string pBNo, int pBClass, int pItem, string pPosId, string pPalletId, string pBoxId, string pMNo, string pBatchNo, int pQCStatus, int pStatus, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_DPS_DPSUpdateStatus :pBNo, :pBClass, :pItem , :pPosId, :pPalletId, :pBoxId, :pMNo,:pBatchNo,:pQCStatus,:pStatus ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable sp_DPS_GetDPSRequireList(Socket sktX, string pWHId, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DPS_GetDPSRequireList :pWHId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static string sp_DPS_ToWMSBOutFromFIn(Socket sktX, string pUser, string pWHId, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DPS_ToWMSBOutFromFIn :pUser, :pWHId, :pBNo",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_DPS_UpdateFillInDtl(Socket sktX, string pUser, string pWHId, string pBNo, string pPosId, string pPalletId, string pMno, double pQty, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_DPS_UpdateFillInDtl :pUser, :pWHId, :pBNo , :pPosId, :pPalletId, :pMno, :pQty ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMno",
                ParameterValue = pMno,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_ECS_GetTskMaterial(Socket sktX, int pWorkId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_ECS_GetTskMaterial :pWorkId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                sErr = str;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_ECS_JudgeTaskIsOK(Socket sktX, int pWorkId, string pPalletId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_ECS_JudgeTaskIsOK :pWorkId, :pPalletId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                sErr = str;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable sp_ECS_LoadWorkTask(Socket sktX, int pLine, string pPalletId, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_ECS_LoadWorkTask :pLine, :pPalletId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pLine",
                ParameterValue = pLine.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static DataTable sp_ECS_ShowWKTskByLinStat(Socket sktX, int pLine, int pStatus, string pPalletId, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_ECS_ShowWKTskByLinStat :pLine, :pStatus, :pPalletId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pLine",
                ParameterValue = pLine.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static DataTable sp_ECS_ShowWorkTaskInfo(Socket sktX, int pWorkId, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_ECS_ShowWorkTaskInfo :pWorkId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static string sp_ECS_UpdateWorkStatus(Socket sktX, int pWorkId, int pStatus, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_ECS_UpdateWorkStatus :pWorkId, :pStatus",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_GetBatchNo(Socket sktX, string pMNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetBatchNo :pMNo",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            str = "-1";
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["nBatchNo"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString().Trim();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable sp_GetDoOutTaskItemList(Socket sktX, int pWorkId, string pWHId, string pPalletId, string pMNo, string pBatchNo, int pQCStatus, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetDoOutTaskItemList :pWorkId, :pWHId, :pPalletId , :pMNo, :pBatchNo, :pQCStatus ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static DataTable sp_GetDoStoreHisList(Socket sktX, string pFromDate, string pToDate, string pWHId, string pMNo, string pUser, string pOrderValue, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetDoStoreHisList :pFromDate, :pToDate, :pWHId , :pMNo, :pUser, :pOrderValue ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pFromDate",
                ParameterValue = pFromDate,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pToDate",
                ParameterValue = pToDate,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOrderValue",
                ParameterValue = pOrderValue,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static string sp_GetDtlSeq(Socket sktX, string TbName, string PFld, string SeqFld, string PValue, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetDtlSeq :TbName, :PFld, :SeqFld , :PValue ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "TbName",
                ParameterValue = TbName,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "PFld",
                ParameterValue = PFld,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "SeqFld",
                ParameterValue = SeqFld,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "PValue",
                ParameterValue = PValue,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["nSeq"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                sErr = str;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataSet sp_GetIOListExt(Socket sktX, int pIsIn, string pWHId, string pMNo, string pBTypeId, string pBNo, string pDateFrom, string pDateTo, string pEventType, string pMatClass, string pEventAddr, string pStartLevel, string pFileNo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetIOListExt :pIsIn,:pWHId,:pMNo,:pBTypeId,:pBNo,:pDateFrom,:pDateTo,:pEventType,:pMatClass,:pEventAddr,:pStartLevel,:pFileNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBTypeId",
                ParameterValue = pBTypeId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDateFrom",
                ParameterValue = pDateFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDateTo",
                ParameterValue = pDateTo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pEventType",
                ParameterValue = pEventType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMatClass",
                ParameterValue = pMatClass,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pEventAddr",
                ParameterValue = pEventAddr,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStartLevel",
                ParameterValue = pStartLevel,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pFileNo",
                ParameterValue = pFileNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return set;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        table = null;
                        return set;
                    }
                    table = set.Tables[1];
                    return set;
                }
                table = null;
                sErr = "执行出错！";
            }
            return set;
        }

        public static DataTable sp_GetMaterialUnKeepdayList(Socket sktX, string pWHId, string pMNo, int pDay, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetMaterialUnKeepdayList :pWHId, :pMNo, :pDay ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDay",
                ParameterValue = pDay.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static string sp_GetNWorkId(Socket sktX, int pIsWMS, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetNWorkId :pIsWMS ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsWMS",
                ParameterValue = pIsWMS.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_GetPubBarCode(Socket sktX, string pCodeName, int pCodeLen, string pHeader, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetPubBarCode :pCodeName, :pCodeLen, :pHeader ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pCodeName",
                ParameterValue = pCodeName,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCodeLen",
                ParameterValue = pCodeLen.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pHeader",
                ParameterValue = pHeader,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable sp_GetStoreUnSafeList(Socket sktX, string pWHId, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetStoreUnSafeList :pWHId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static DataTable sp_GetSyslog(Socket sktX, string pDateFrom, string pDateTo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetSyslog :pDateFrom, :pDateTo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pDateFrom",
                ParameterValue = pDateFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDateTo",
                ParameterValue = pDateTo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static DataTable sp_GetWareHouseItemList(Socket sktX, int nCountType, string pWHId, string pPalletId, string pMNo, string pBatchNo, int pQCStatus, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetWareHouseItemList :nCountType, :pWHId, :pPalletId , :pMNo, :pBatchNo, :pQCStatus ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "nCountType",
                ParameterValue = nCountType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static string SP_IF_DoChkData(Socket sktX, string pBNo, int pMode, string pUserId, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_IF_DoChkData :pBNo,:pMode,:pUserId,:pCmptId,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMode",
                ParameterValue = pMode.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            if (table.Rows.Count > 0)
                            {
                                obj2 = table.Rows[0][0];
                                if (obj2 != null)
                                {
                                    str = obj2.ToString();
                                }
                                obj2 = table.Rows[0][1];
                                if (obj2 != null)
                                {
                                    str = table.Rows[0][0].ToString();
                                    sErr = obj2.ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_IF_ImpBillInFromMid(Socket sktX, string pUser, string pBNo, int pIsImp, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_IF_ImpBillInFromMid :pUser,:pBNo,:pIsImp",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsImp",
                ParameterValue = pIsImp.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            if (table.Rows.Count > 0)
                            {
                                obj2 = table.Rows[0][0];
                                if (obj2 != null)
                                {
                                    str = obj2.ToString();
                                }
                                obj2 = table.Rows[0][1];
                                if (obj2 != null)
                                {
                                    str = table.Rows[0][0].ToString();
                                    sErr = obj2.ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_InsertUserLog(Socket sktX, string pUser, string pSysFrom, string pOptType, string pOptDesc, string pCmptId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_InsertUserLog :pUser, :pSysFrom, :pOptType , :pOptDesc ,:pCmptId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptDesc",
                ParameterValue = pOptDesc,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_IsBoxNo(Socket sktX, string pBoxId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_IsBoxNo :pBoxId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["nResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                                str = sErr;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_IsMaterialNo(Socket sktX, string pMNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_IsMaterialNo :pMNo",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["nResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                                str = sErr;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_IsPalletNo(Socket sktX, string pPalletId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_IsPalletNo :pPalletId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["nResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                                str = sErr;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_BillCheck(Socket sktX, int pBClass, string pBNo, int pIsUnCheck, string pUserId, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_BillCheck :pBClass,:pBNo,:pUserId,:pIsUnCheck,:pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsUnCheck",
                ParameterValue = pIsUnCheck.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_BillIODel(Socket sktX, string pBNo, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_BillIODel :pBNo, :pUser , :pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            str = "-1";
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_BillIODtlDel(Socket sktX, string pBNo, int pItem, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_BillIODtlDel :pBNo, :pItem, :pUser , :pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            str = "-1";
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_BillQCDel(Socket sktX, string pBNo, int pBClass, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_BillQCDel :pBNo,:pBClass, :pUser , :pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            str = "-1";
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_BillWKTskOver(Socket sktX, int pBClass, string pBNo, string pUserId, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_BillWKTskOver :pBClass,:pBNo,:pUserId,:pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_BldRemoveInData(Socket sktX, int pBClass, string pBNo, string pUserId, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_BldRemoveInData :pBClass,:pBNo,:pUserId,:pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_CheckPosIsdPltIdIsOK(Socket sktX, int pOptType, string pPosId, string pPalletId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_CheckPosIsdPltIdIsOK :pOptType, :pPosId , :pPalletId",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            str = "-1";
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_CheckPWPosPltIdIsOK(Socket sktX, int pOptType, string pPosId, string pPalletId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_CheckPWPosPltIdIsOK :pOptType, :pPosId , :pPalletId",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            str = "-1";
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_ChkBatchBill(Socket sktX, int pBClass, string pBNo, int pWFId, int pWFType, string pRemark, string pUserId, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_ChkBatchBill :pBClass,:pBNo,:pWFId,:pWFType,:pRemark,:pUserId,:pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWFId",
                ParameterValue = pWFId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWFType",
                ParameterValue = pWFType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRemark",
                ParameterValue = pRemark,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DelPWHWKTskDtl(Socket sktX, string pBNo, int pBClass, int pItem, string pPosId, string pPalletId, string pBoxId, string pMNo, string pBatchNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DelPWHWKTskDtl :pBNo, :pBClass, :pItem , :pPosId, :pPalletId, :pBoxId, :pMNo,:pBatchNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DelWKTskDtl(Socket sktX, int pnWorkId, string pBNo, string pItem, string pBNoIn, string pItemIn, string pBoxId, string pSysFrom, string pCmptId, string pUser, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DelWKTskDtl :pnWorkId, :pBNo, :pItem ,:pBNoIn, :pItemIn ,:pBoxId, :pSysFrom,:pCmptId,:pUser",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pnWorkId",
                ParameterValue = pnWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DelWKTskDtlForOver(Socket sktX, string pBNo, int pItem, string pMNo, int pWorkId, string pPltId, string pBoxId, string pPosId, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DelWKTskDtlForOver :pBNo,:pItem,:pMNo,:pWorkId,:pPltId,:pBoxId,:pPosId,:pUser,:pCmptId,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPltId",
                ParameterValue = pPltId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            if (table.Rows.Count > 0)
                            {
                                obj2 = table.Rows[0][0];
                                if (obj2 != null)
                                {
                                    str = obj2.ToString();
                                }
                                obj2 = table.Rows[0][1];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoBadMatMode(Socket sktX, string pBNo, int pItem, string pDoMode, string pUserId, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoBadMatMode :pBNo,:pItem,:pDoMode, :pUserId,:pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDoMode",
                ParameterValue = pDoMode,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoEmptyPalletIO(Socket sktX, string pSysType, string pWHId, int pIsIn, string pPosId, string pPalletId, int pStation, int pIsDoNow, string pUser, string pCmptId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoEmptyPalletIO :pSysType, :pWHId, :pIsIn , :pPosId, :pPalletId, :pStation, :pIsDoNow,:pUser,:pCmptId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStation",
                ParameterValue = pStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDoNow",
                ParameterValue = pIsDoNow.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            if (table.Rows.Count > 0)
                            {
                                obj2 = table.Rows[0][0];
                                if (obj2 != null)
                                {
                                    str = obj2.ToString();
                                }
                                obj2 = table.Rows[0][1];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoOutAndSeeTask(Socket sktX, string pSysType, string pPosId, string pUser, string pCmptId, int pStation, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoOutAndSeeTask :pSysType,:pPosId,:pUser,:pCmptId,:pStation ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStation",
                ParameterValue = pStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            if (table.Rows.Count > 0)
                            {
                                obj2 = table.Rows[0][0];
                                if (obj2 != null)
                                {
                                    str = obj2.ToString();
                                }
                                obj2 = table.Rows[0][1];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoPallet_Up(Socket sktX, string pPalletId, int pWorkId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPallet_Up :pPalletId, :pWorkId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                                str = sErr;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoPltDtlInManual(Socket sktX, string pUser, int pIsDPS, string pPosId, int pOptType, int pOptStation, string pSysType, string pWHId, string pBNo, string pItem, string pMNo, string pBatchNo, double pQty, string pPalletId, string pCmptId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPltDtlInManual :pUser , :pIsDPS , :pPosId , :pOptType ,:pOptStation ,:pSysType ,:pWHId ,:pBNo ,:pItem ,:pMNo , :pBatchNo ,:pQty ,:pPalletId,:pCmptId",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDPS",
                ParameterValue = pIsDPS.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptStation",
                ParameterValue = pOptStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        str = obj2.ToString();
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else if (set.Tables.Count > 1)
                        {
                            table = set.Tables[1];
                            str = table.Rows[0]["cResultId"].ToString();
                            sErr = table.Rows[0]["cResult"].ToString();
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoPltDtlOutAuto(Socket sktX, string pUser, int pIsDPS, int pOptStation, string pSysType, string pWHId, string pBNo, string pItem, string pMNo, string pBatchNo, double pQty, int pQCStatus, int pOptType, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPltDtlOutAuto :pUser, :pIsDPS, :pPalletLevel , :pOptStation, :pSysType, :pWHId, :pBNo,:pItem,:pMNo,:pBatchNo,:pQty,:pQCStatus,:pOptType ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDPS",
                ParameterValue = pIsDPS.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptStation",
                ParameterValue = pOptStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoPltDtlOutManual(Socket sktX, string pUser, int pIsDPS, string pPosId, int pOptType, int pOptStation, string pSysType, string pWHId, string pBNo, string pItem, string pBNoIn, string pItemIn, string pMNo, string pBatchNo, double pQty, string pPalletId, string pBoxId, string pCmptId, int pIsAllowAdd, string pMatClass, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPltDtlOutManual :pUser, :pIsDPS, :pPosId , :pOptType, :pOptStation, :pSysType, :pWHId,:pBNo,:pItem,:pBNoIn,:pItemIn,:pMNo,:pBatchNo,:pQty,:pPalletId,:pBoxId,:pCmptId,:pIsAllowAdd,:pMatClass ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsDPS",
                ParameterValue = pIsDPS.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptStation",
                ParameterValue = pOptStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsAllowAdd",
                ParameterValue = pIsAllowAdd.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMatClass",
                ParameterValue = pMatClass,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoPWPltDtlInManu(Socket sktX, string pUser, string pPosId, string pSysType, string pPalletId, string pBoxId, string pBNo, string pItem, double pQty, int pOptType, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPWPltDtlInManu :pUser, :pPosId, :pSysType , :pPalletId, :pBoxId, :pBNo, :pItem,:pQty,:pOptType ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoPWPltDtlOutManu(Socket sktX, string pUser, string pPosId, string pSysType, string pPalletId, string pBoxId, string pBNo, string pItem, string pBNoIn, string pItemIn, double pQty, int pOptType, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPWPltDtlOutManu :pUser, :pPosId, :pSysType , :pPalletId, :pBoxId, :pBNo, :pItem,:pBNoIn,:pItemIn, :pQty,:pOptType ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoSampleTask(Socket sktX, string pSysType, string pPosId, string pPalletId, string pWHId, string pBNo, string pBNoIn, int pItemIn, double pSample, string pUser, string pCmptId, int pStation, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoSampleTask :pSysType,:pPosId,:pPalletId,:pWHId,:pBNo,:pBNoIn,:pItemIn,:pSample,:pUser,:pCmptId,:pStation ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosId",
                ParameterValue = pPosId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSample",
                ParameterValue = pSample.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStation",
                ParameterValue = pStation.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            if (table.Rows.Count > 0)
                            {
                                obj2 = table.Rows[0][0];
                                if (obj2 != null)
                                {
                                    str = obj2.ToString();
                                }
                                obj2 = table.Rows[0][1];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_DoWareCellMove(Socket sktX, string pSysType, string pPosIdFrom, string pPosIdTo, string pUser, string pCmptId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoWareCellMove :pSysType, :pPosIdFrom,:pPosIdTo,:pUser,:pCmptId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pSysType",
                ParameterValue = pSysType,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosIdFrom",
                ParameterValue = pPosIdFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPosIdTo",
                ParameterValue = pPosIdTo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                if (str != "0")
                                {
                                    sErr = str;
                                }
                                else
                                {
                                    sErr = "";
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static DataTable sp_Pack_GetIOBDtlLstByMNo(Socket sktX, int pBClass, string pMNo, string pWHId, int pStatus, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetIOBDtlLstByMNo :pBClass, :pMNo, :pWHId , :pStatus ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static double sp_Pack_GetItemBillQty(Socket sktX, int pIsIn, string pMNo, string pWHId, int pQCStatus, string pBatchNo, double pCurUsedQty, out string sErr)
        {
            double num = -1.0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemBillQty :pIsIn, :pMNo, :pWHId , :pQCStatus, :pBatchNo,:pCurUsedQty",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCurUsedQty",
                ParameterValue = pCurUsedQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["fQty"];
                            if (obj2 != null)
                            {
                                if (obj2.ToString() == "")
                                {
                                    num = 0.0;
                                }
                                else
                                {
                                    num = double.Parse(obj2.ToString());
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static double sp_Pack_GetItemBillQty(Socket sktX, int pIsIn, string pMNo, string pWHId, int pQCStatus, string pBatchNo, double pCurUsedQty, string pWHIdErp, string pBNoIn, int pItemIn, out string sErr)
        {
            double num = -1.0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemBillQty :pIsIn, :pMNo, :pWHId , :pQCStatus, :pBatchNo,:pCurUsedQty,pWHIdErp,pBNoIn,pItemIn",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCurUsedQty",
                ParameterValue = pCurUsedQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHIdErp",
                ParameterValue = pWHIdErp,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["fQty"];
                            if (obj2 != null)
                            {
                                if (obj2.ToString() == "")
                                {
                                    num = 0.0;
                                }
                                else
                                {
                                    num = double.Parse(obj2.ToString());
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static DataTable sp_Pack_GetItemList(Socket sktX, string pParentId, string pWHId, string pMNo, int pStatus, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemList :pParentId, :pWHId, :pMNo , :pPalletLevel, :pStatus",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static double sp_Pack_GetItemPalletQty(Socket sktX, int pIsIn, string pMNo, string pWHId, string pParentId, string pMatClass, int pQCStatus, string pBatchNo, out string sErr)
        {
            double num = -1.0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemPalletQty :pIsIn, :pMNo, :pWHId , :pParentId,:pMatClass,:pQCStatus, :pBatchNo,:pPalletLevel",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMatClass",
                ParameterValue = pMatClass,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["fQty"];
                            if (obj2 != null)
                            {
                                num = double.Parse(obj2.ToString());
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static double sp_Pack_GetItemPalletQty(Socket sktX, int pIsIn, string pMNo, string pWHId, string pParentId, string pMatClass, int pQCStatus, string pBatchNo, double pCurUsedQty, string pWHIdErp, string pBNoIn, int pItemIn, out string sErr)
        {
            double num = -1.0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemPalletQty :pIsIn, :pMNo, :pWHId , :pParentId,:pMatClass,:pQCStatus, :pBatchNo,:pPalletLevel,pCurUsedQty,pWHIdErp,pBNoIn,pItemIn",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMatClass",
                ParameterValue = pMatClass,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCurUsedQty",
                ParameterValue = pCurUsedQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHIdErp",
                ParameterValue = pWHIdErp,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["fQty"];
                            if (obj2 != null)
                            {
                                num = double.Parse(obj2.ToString());
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static double sp_Pack_GetItemQty(Socket sktX, string pMNo, string pWHId, string pParentId, string pMatClass, int pQCStatus, string pBatchNo, string pBNoIn, int pItemIn, out string sErr)
        {
            double num = -1.0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemQty :pMNo, :pWHId, :pParentId ,:pMatClass, :pQCStatus, :pBatchNo, :pPalletLevel,:pBNoIn,:pItemIn ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMatClass",
                ParameterValue = pMatClass,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["fQty"];
                            if (obj2 != null)
                            {
                                num = double.Parse(obj2.ToString());
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static double sp_Pack_GetItemQty(Socket sktX, string pMNo, string pWHId, string pParentId, string pMatClass, int pQCStatus, string pBatchNo, string pBNoIn, int pItemIn, string pWHIdErp, out string sErr)
        {
            double num = -1.0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetItemQty :pMNo, :pWHId, :pParentId ,:pMatClass, :pQCStatus, :pBatchNo, :pPalletLevel,:pBNoIn,:pItemIn,pWHIdErp ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMatClass",
                ParameterValue = pMatClass,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHIdErp",
                ParameterValue = pWHIdErp,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["fQty"];
                            if (obj2 != null)
                            {
                                num = double.Parse(obj2.ToString());
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static DataTable sp_Pack_GetOutBItemList(Socket sktX, string pWHId, string pMNo, int pIsRemove, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetOutBItemList :pWHId,:pMNo,:pIsRemove ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsRemove",
                ParameterValue = pIsRemove.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static DataTable sp_Pack_GetPosLstForPltIn(Socket sktX, string pUser, int pOptType, string pMNo, string pWHId, string pWAreaId, int pRow, int pCol, int pLayer, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetPosLstForPltIn :pUser, :pOptType, :pMNo , :pWHId ,:pWAreaId,:pRow,:pCol,:pLayer",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptType",
                ParameterValue = pOptType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWAreaId",
                ParameterValue = pWAreaId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRow",
                ParameterValue = pRow.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCol",
                ParameterValue = pCol.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLayer",
                ParameterValue = pLayer.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return null;
            }
            return set.Tables[1];
        }

        public static DataTable sp_Pack_GetPosLstForPltOut(Socket sktX, string pWHId, string pMNo, double pQty, int pIsWhole, string pWAreaId, int pRow, int pCol, int pLayer, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetPosLstForPltOut :pWHId, :pMNo ,:pQty,:pIsWhole ,:pWAreaId,:pRow,:pCol,:pLayer",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsWhole",
                ParameterValue = pIsWhole.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWAreaId",
                ParameterValue = pWAreaId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRow",
                ParameterValue = pRow.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCol",
                ParameterValue = pCol.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pLayer",
                ParameterValue = pLayer.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set == null)
            {
                return table;
            }
            table = set.Tables["result"];
            if (table == null)
            {
                return table;
            }
            object obj2 = null;
            obj2 = table.Rows[0]["returncode"];
            if (obj2 == null)
            {
                return table;
            }
            if (obj2.ToString() != "0")
            {
                sErr = table.Rows[0]["returndesc"].ToString();
                return table;
            }
            return set.Tables[1];
        }

        public static DataTable sp_Pack_GetPosLstForSample(Socket sktX, string pSmpNo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetPosLstForSample :pSmpNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pSmpNo",
                ParameterValue = pSmpNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static int sp_Pack_GetWKIdByParentId(Socket sktX, string pParentId, out string sErr)
        {
            int num = 0;
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetWKIdByParentId :pParentId ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["nWorkId"];
                            if (obj2 != null)
                            {
                                num = int.Parse(obj2.ToString());
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return num;
        }

        public static DataTable sp_Pack_ShowBillFinishedDtl(Socket sktX, int pBClass, string pBNo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_ShowBillFinishedDtl :pBClass,:pBNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static DataTable sp_Pack_ShowBillTaskDtl(Socket sktX, int pBClass, string pBNo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_ShowBillTaskDtl :pBClass,:pBNo ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static string sp_Pack_SubtractQty(Socket sktX, string pChildId, double pQty, string pUnit, string pBFromNo, int pFromItem, string pParentId, DateTime pOperateTime, string pUser, int pStatus, string pWorkFlow, string pBNo, int pItem, string pRemark, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_SubtractQty :pChildId, :pQty, :pUnit , :pBFromNo, :pFromItem, :pParentId, :pLevel,:pOperateTime,:pUser,:pStatus,:pWorkFlow,:pBNo,:pItem,:pRemark ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pChildId",
                ParameterValue = pChildId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUnit",
                ParameterValue = pUnit,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBFromNo",
                ParameterValue = pBFromNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pFromItem",
                ParameterValue = pFromItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            paramter = new ZqmParamter {
                ParameterName = "pLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOperateTime",
                ParameterValue = pOperateTime.ToString("yyyy-MM-dd hh:mm:ss"),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWorkFlow",
                ParameterValue = pWorkFlow,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRemark",
                ParameterValue = pRemark,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            str = "0";
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_Pack_UpdtWKTskDtl(Socket sktX, int pWorkId, string pBNo, string pItem, double pQty, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_UpdtWKTskDtl :pWorkId, :pBNo, :pItem , :pQty ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_SaveSysRight(Socket sktX, string pPRId, string pRId, string pCName, string pEName, string pRCode, int pRType, string pUser, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_SaveSysRight :pPRId,:pRId,:pCName, :pEName,:pRCode ,:pRType,:pUser",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pPRId",
                ParameterValue = pPRId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRId",
                ParameterValue = pRId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCName",
                ParameterValue = pCName,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pEName",
                ParameterValue = pEName,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRCode",
                ParameterValue = pRCode,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRType",
                ParameterValue = pRType.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_SaveUserRight(Socket sktX, string pRId, string pUserId, string pCreator, int pIsChecked, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_SaveUserRight :pRId,:pUserId, :pCreator ,:pIsChecked",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pRId",
                ParameterValue = pRId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCreator",
                ParameterValue = pCreator,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsChecked",
                ParameterValue = pIsChecked.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_UnDoTaskCMD(Socket sktX, int pWorkId, string pSysFrom, string pCmptId, string pUser, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_UnDoTaskCMD :pWorkId,:pSysFrom,:pCmptId,:pUser",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                if (str != "0")
                                {
                                    sErr = table.Rows[0]["cResult"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_UpdateAjustDtlStatus(Socket sktX, string pBNo, string pWHId, string pMNo, string pPalletId, string pBoxId, string pBatchNo, string pUnit, int pStatus, int pQCStatus, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_UpdateAjustDtlStatus :pBNo,:pWHId,:pMNo ,:pPalletId,:pBoxId,:pBatchNo,:pUnit ,:pStatus,:pQCStatus",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWHId",
                ParameterValue = pWHId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pMNo",
                ParameterValue = pMNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBoxId",
                ParameterValue = pBoxId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            paramter = new ZqmParamter {
                ParameterName = "pUnit",
                ParameterValue = pUnit,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQCStatus",
                ParameterValue = pQCStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                sErr = str;
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_UpdateBillDtlPalletQty(Socket sktX, int pIsIn, string pBNo, string pItem, double pPQty, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_UpdateBillDtlPalletQty :pIsIn, :pBNo, :pItem , :pPQty ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsIn",
                ParameterValue = pIsIn.ToString(),
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPQty",
                ParameterValue = pPQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_UpdateChkBillStatus(Socket sktX, string pBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_UpdateChkBillStatus  :pBNo",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_UpdateMaterialQC(Socket sktX, string pBNo, int pItem, int pStatus, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoBadMatMode :pBNo,:pItem,:pStatus, :pUser,:pCmptId ,:pSysFrom",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            str = obj2.ToString();
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                                obj2 = table.Rows[0]["cResult"];
                                if (obj2 != null)
                                {
                                    sErr = obj2.ToString();
                                }
                                else
                                {
                                    sErr = str;
                                }
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        public static string sp_UpdatePalletStatus(Socket sktX, string pPalletId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_UpdatePalletStatus :pPalletId ,:pPalletLevel ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pPalletId",
                ParameterValue = pPalletId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPalletLevel",
                ParameterValue = "9",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }

        /// <summary>
        /// chukuchakan
        /// </summary>
        /// <param name="sktX"></param>
        /// <param name="workNumbs"></param>
        /// <returns></returns>
        public static DataTable SP_SG_OutCheck(Socket sktX, string workNumbs)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo
            {
                SqlText = "SP_SG_OUTCHECK :WORKNUMBS",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"

            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter
            {
                ParameterName = "WORKNUMBS",
                ParameterValue = workNumbs,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            string sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;



        }

        public static DataTable sp_UserCheck(Socket sktX, string pComptId, string pDeptId, string pUser, string pPassword, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_UserCheck :pComptId, :pDeptId,:pUser,:pPassword ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pComptId",
                ParameterValue = pComptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDeptId",
                ParameterValue = pDeptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPassword",
                ParameterValue = pPassword,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table == null)
                {
                    return table;
                }
                object obj2 = null;
                obj2 = table.Rows[0]["returncode"];
                if (obj2 != null)
                {
                    if (obj2.ToString() != "0")
                    {
                        sErr = table.Rows[0]["returndesc"].ToString();
                        return null;
                    }
                    return set.Tables[1];
                }
                table = null;
                sErr = "执行出错！";
            }
            return table;
        }

        public static string sp_WriteMatPack(Socket sktX, int pIsNew, string pCmptId, string pUser, string pSysFrom, string pChildId, string pParentId, double pQty, string pWorkFlow, string pBNo, int pItem, string pBNoIn, int pItemIn, string pRemark, int pWorkId, int pStatus, int pBClass, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_WriteMatPack :pIsNew, :pCmptId, :pUser , :pSysFrom, :pChildId, :pParentId, :pQty,:pWorkFlow,:pBNo,:pItem,:pBNoIn,:pItemIn,:pRemark,:pWorkId,:pStatus,:pBClass ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pIsNew",
                ParameterValue = pIsNew.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pCmptId",
                ParameterValue = pCmptId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pUser",
                ParameterValue = pUser,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pSysFrom",
                ParameterValue = pSysFrom,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pChildId",
                ParameterValue = pChildId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pParentId",
                ParameterValue = pParentId,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pQty",
                ParameterValue = pQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWorkFlow",
                ParameterValue = pWorkFlow,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNo",
                ParameterValue = pBNo,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItem",
                ParameterValue = pItem.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBNoIn",
                ParameterValue = pBNoIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pItemIn",
                ParameterValue = pItemIn.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pRemark",
                ParameterValue = pRemark,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pWorkId",
                ParameterValue = pWorkId.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pStatus",
                ParameterValue = pStatus.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pBClass",
                ParameterValue = pBClass.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            if (set != null)
            {
                table = set.Tables["result"];
                if (table != null)
                {
                    object obj2 = null;
                    obj2 = table.Rows[0]["returncode"];
                    if (obj2 != null)
                    {
                        if (obj2.ToString() != "0")
                        {
                            sErr = table.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            table = set.Tables[1];
                            obj2 = table.Rows[0]["cResultId"];
                            if (obj2 != null)
                            {
                                str = obj2.ToString();
                            }
                            obj2 = table.Rows[0]["cResult"];
                            if (obj2 != null)
                            {
                                sErr = obj2.ToString();
                            }
                        }
                    }
                }
            }
            set.Clear();
            set.Dispose();
            return str;
        }
    }
}

