namespace WareStoreMS
{
    using SunEast;
    using System;
    using System.Data;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using Zqm.DBCommInfo;

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

        public static DataTable GetDataTableBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, string sFldsDate, out string sErr)
        {
            sErr = "";
            DataTable table = null;
            DataSet set = GetDataBySql(sktX, bIsSaveDataxml, sSql, sTbName, nPageSize, nPageIndex, sFldsDate, out sErr);
            if (((sErr.Trim() == "") || (sErr.Trim() == "0")) && ((set != null) && (set.Tables.Count > 0)))
            {
                DataTable table2 = set.Tables[0];
                if (table2.Rows.Count > 0)
                {
                    sErr = "";
                    if (table2.Rows[0][0].ToString() == "-1")
                    {
                        sErr = table2.Rows[0][1].ToString();
                    }
                }
                if (set.Tables[sTbName] != null)
                {
                    table = set.Tables[sTbName].Copy();
                }
            }
            if (set != null)
            {
                set.Clear();
                set = null;
            }
            return table;
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
                FromSysType = "rf"
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

        public static string sp_Chk_WriteAjustDtl(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pAjustNo, string pWHId, string pPosId, string pPalletId, string pBoxId, string pMNo, double pDiff, string pBNoIn, int pItemIn, string pCheckNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "SP_CHK_WRITEAJUSTDTL :pUser,:pCmptId,:pSysFrom,:pAjustNo,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBNoIn,:pItemIn,:pCheckNo ",
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

        public static DataTable sp_GetSlackMatCount(Socket sktX, string pWHId, string pDateFrom, string pDateTo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetSlackMatCount :pWHId ,:pDateFrom,:pDateTo ",
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
                ParameterName = "pDateFrom",
                ParameterValue = pDateFrom,
                DataType = ZqmDataType.DateTime,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pDateTo",
                ParameterValue = pDateTo,
                DataType = ZqmDataType.DateTime,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            SeDBClient client = new SeDBClient();
            sErr = "";
            DataSet set = null;
            DataTable table = null;
            DataTable table2 = null;
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
                            table2 = set.Tables[1];
                            table2.TableName = "SlackMatCount";
                        }
                    }
                }
            }
            table.Clear();
            return table2;
        }

        public static DataTable sp_GetSlackMatDtl(Socket sktX, string pWHId, string pMNo, string pDateFrom, string pDateTo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetSlackMatDtl :pWHId ,:pMNo,:pDateFrom,:pDateTo ",
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
            DataTable table2 = null;
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
                            table2 = set.Tables[1];
                            table2.TableName = "SlackMatCount";
                        }
                    }
                }
            }
            table.Clear();
            return table2;
        }

        public static DataTable sp_GetSlackMatDtlList(Socket sktX, string pWHId, string pDateFrom, string pDateTo, out string sErr)
        {
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_GetSlackMatDtlList :pWHId ,:pDateFrom,:pDateTo ",
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
            DataTable table2 = null;
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
                            table2 = set.Tables[1];
                            table2.TableName = "SlackMatDtlList";
                        }
                    }
                }
            }
            table.Clear();
            return table2;
        }

        public static string sp_Pack_DoWareCellMove(Socket sktX, string pSysType, string pPosIdFrom, string pPosIdTo, string pUser, string pCmptId, int pIsDoNow, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Pack_DoWareCellMove :pSysType, :pPosIdFrom,:pPosIdTo,:pUser,:pCmptId,pIsDoNow ",
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
            paramter = new ZqmParamter {
                ParameterName = "pIsDoNow",
                ParameterValue = pIsDoNow.ToString(),
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

