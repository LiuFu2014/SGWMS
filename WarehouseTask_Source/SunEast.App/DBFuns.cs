    using SunEast;
    using System;
    using System.Data;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using DBCommInfo;
namespace SunEast.App
{


    public static class DBFuns
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

        public static string SP_Ajust_UpdateStoreQty(Socket sktX, string pUserId, string pCmptId, string pSysFrom, string pPalletId, string pBoxId, string pMNo, double pRealQty, string pBNoIn, string pItemIn, string pAjustBNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_Ajust_UpdateStoreQty :pUserId, :pCmptId, :pSysFrom , :pPalletId, :pBoxId, :pMNo, :pRealQty,:pBNoIn,:pItemIn,:pAjustBNo",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
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
                ParameterName = "pRealQty",
                ParameterValue = pRealQty.ToString(),
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
                ParameterValue = pItemIn,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pAjustBNo",
                ParameterValue = pAjustBNo,
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
                set.Clear();
                set.Dispose();
            }
            return str;
        }

        public static string sp_Pack_CheckPltOutQtyIsOK(Socket sktX, string pMNo, string pWHId, string pPalletId, string pBoxId, string pBNoIn, int pItemIn, double pPltQty, out string sErr)
        {
            sErr = "";
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_CheckPltOutQtyIsOK :pMNo ,:pWHId,:pPalletId,:pBoxId,:pBNoIn,:pItemIn,:pWorkId,:pPltQty ",
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
                ParameterValue = "0",
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPltQty",
                ParameterValue = pPltQty.ToString(),
                DataType = ZqmDataType.Double,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            try
            {
                set = client.GetDataSet(sktX, cmdInfo, false, out sErr);
            }
            catch (Exception exception)
            {
                sErr = exception.Message;
            }
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
                                obj2 = table.Rows[0]["cResult"];
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

        public static string sp_Pack_DoPltDtlInAuto(Socket sktX, string pUserId, string pSysType, string pBNo, string pItem, int pIsFirstEmptAfterHalfPlt, string pOptGroup, int pPltMatQty, string pWHId, int pRow, int pCol, int pLayer, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPltDtlInAuto :pUserId ,:pSysType,:pBNo,:pItem,:pIsFirstEmptAfterHalfPlt,:pOptGroup,:pPltMatQty,:pWHId,:pRow,:pCol,:pLayer ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
            paramter = new ZqmParamter {
                ParameterName = "pUserId",
                ParameterValue = pUserId,
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
                ParameterName = "pIsFirstEmptAfterHalfPlt",
                ParameterValue = pIsFirstEmptAfterHalfPlt.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptGroup",
                ParameterValue = pOptGroup,
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pPltMatQty",
                ParameterValue = pPltMatQty.ToString(),
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

        public static string SP_Pack_DoPltDtlOutAuto(Socket sktX, string pUserId, string pCmptId, string pSysType, string pBNo, string pItem, string pWHId, string pAreaId, int pIsSameBatchNo, int pIsEmptAsWholeOut, string pOptGroup, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_Pack_DoPltDtlOutAuto :pUserId ,:pCmptId,:pSysType,:pBNo,:pItem,:pWHId,:pAreaId,:pIsSameBatchNo,:pIsEmptAsWholeOut,:pOptGroup ",
                SqlType = SqlCommandType.sctProcedure,
                PageIndex = 0,
                PageSize = 0,
                FromSysType = "dotnet"
            };
            ZqmParamter paramter = null;
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
                ParameterName = "pSysType",
                ParameterValue = pSysType,
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
                ParameterName = "pWHId",
                ParameterValue = pWHId,
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
                ParameterName = "pIsSameBatchNo",
                ParameterValue = pIsSameBatchNo.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pIsEmptAsWholeOut",
                ParameterValue = pIsEmptAsWholeOut.ToString(),
                DataType = ZqmDataType.Int,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pOptGroup",
                ParameterValue = pOptGroup,
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

        public static string sp_Pack_DoPltDtlOutManual(Socket sktX, string pUser, int pIsDPS, string pPosId, int pOptType, int pOptStation, string pSysType, string pWHId, string pBNo, string pItem, string pBNoIn, string pItemIn, string pMNo, string pBatchNo, double pQty, string pPalletId, string pBoxId, string pCmptId, int pIsAllowAdd, int pWorkId, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoPltDtlOutManual :pUser, :pIsDPS, :pPosId , :pOptType, :pOptStation, :pSysType, :pWHId,:pBNo,:pItem,:pBNoIn,:pItemIn,:pMNo,:pBatchNo,:pQty,:pPalletId,:pBoxId,:pCmptId,:pIsAllowAdd,:pWorkId ",
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

        public static DataTable sp_Pack_GetPosLstForPltIn(Socket sktX, string pUser, int pOptType, string pMNo, string pBatchNo, string pWHId, string pWAreaId, int pRow, int pCol, int pLayer, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetPosLstForPltIn :pUser, :pOptType, :pMNo ,:pBatchNo, :pWHId ,:pWAreaId,:pRow,:pCol,:pLayer",
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
                ParameterName = "pBatchNo",
                ParameterValue = pBatchNo,
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

        public static DataTable sp_Pack_GetPosLstForPltOut(Socket sktX, string pWHId, string pMNo, double pQty, int pIsWhole, string pWAreaId, int pRow, int pCol, int pLayer, string pMatClass, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_GetPosLstForPltOut :pWHId, :pMNo ,:pQty,:pIsWhole ,:pWAreaId,:pRow,:pCol,:pLayer,:pMatClass",
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
    }
}

