namespace SunEast.App
{
    using SunEast;
    using System;
    using System.Data;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using DBCommInfo;

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

        public static string sp_Chk_CreateDataFromMID(Socket sktX, string pUser, string pCmptId, string pChkNo, out string sErr)
        {
            string str = "";
            DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo {
                SqlText = "sp_Chk_CreateDataFromMID :pUser ,:pCmptId,:pChkNo ",
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
                ParameterValue = "pCmptId",
                DataType = ZqmDataType.String,
                ParameterDir = ZqmParameterDirction.Input
            };
            cmdInfo.Parameters.Add(paramter);
            paramter = new ZqmParamter {
                ParameterName = "pChkNo",
                ParameterValue = "pChkNo",
                DataType = ZqmDataType.String,
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
                return "-1";
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
                                sErr = table.Rows[0]["cResult"].ToString();
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

