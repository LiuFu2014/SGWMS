using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Sockets;
using SunEast.App;
using SunEast;
using DBCommInfo;
using Zqm.Text;


namespace SunEast.App
{
    public static class DBFuns
    {
        #region 公共方法

       
            /// <summary>
            /// 执行SQL语句，得到记录集
            /// </summary>
            /// <param name="sSql"></param>
            /// <param name="sTbName"></param>
            /// <param name="nPageSize"></param>
            /// <param name="nPageIndex"></param>
            /// <param name="sErr"></param>
            /// <returns></returns>
            public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, string sFldsDate, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return null;
                }
                string sTb = "data";
                if (sTbName.Trim() != "")
                {
                    sTb = sTbName.Trim();
                }
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = sSql;             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = nPageIndex;                                          //需要分页时的页号
                cmdInfo.PageSize = nPageSize;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                cmdInfo.DataTableName = sTb;                          //指定结果数据记录集表名           
                cmdInfo.FldsData = sFldsDate.Trim();
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                //DataTable tbX = null;          
                dsX = sdcX.GetDataSet(sktX, cmdInfo, bIsSaveDataxml, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
                return dsX;
            }

            public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return dsX;
                }
                dsX = GetDataBySql(sktX, bIsSaveDataxml, sSql, sTbName, nPageSize, nPageIndex, "", out sErr);
                return dsX;
            }
            public static DataSet GetDataBySql(string sSql, string sTbName, int nPageSize, int nPageIndex, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return dsX;
                }
                return GetDataBySql(null, false, sSql, sTbName, nPageSize, nPageIndex, out sErr);
            }

            /// <summary>
            /// 执行SQL语句，得到记录集
            /// </summary>
            /// <param name="sSql"></param>
            /// <param name="sErr"></param>
            /// <returns></returns>
            public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return dsX;
                }
                return GetDataBySql(sktX, bIsSaveDataxml, sSql, "data", 0, 0, out sErr);
            }
            public static DataSet GetDataBySql(bool bIsSaveDataxml, string sSql, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return null;
                }
                dsX = GetDataBySql(null, bIsSaveDataxml, sSql, out sErr);
                return (dsX);
            }
            public static DataSet GetDataBySql(string sSql, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return null;
                }
                dsX = GetDataBySql(null, false, sSql, out sErr);
                return (dsX);
            }
            public static DataSet GetDataBySql(string sSql, string sFldsDate, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return null;
                }
                dsX = GetDataBySql(null, false, sSql, "data", 0, 0, sFldsDate, out sErr);
                return (dsX);
            }
            public static DataSet GetDataBySql(Socket sktX, string sSql, string sFldsDate, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return null;
                }
                dsX = GetDataBySql(sktX, false, sSql, "data", 0, 0, sFldsDate, out sErr);
                return (dsX);
            }
            public static DataSet GetDataBySql(Socket sktX, string sSql, string sTbName, string sFldsDate, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL语句不能为空！";
                    return null;
                }
                dsX = GetDataBySql(sktX, false, sSql, sTbName, 0, 0, sFldsDate, out sErr);
                return (dsX);
            }

            /// <summary>
            /// 根据SQL，获取其中一个字段的值，以object 变量返回
            /// </summary>
            /// <param name="sktX">Socket 对象</param>
            /// <param name="sSql">sql 语句，单行</param>
            /// <param name="sFldsDate">SQL语句中 所含有的日期类型的字段，以','隔开</param>
            /// <param name="sFldValue">所需返回值的字段名，当为空时，将返回第一列的值</param>
            /// <param name="objValue">返回字段值对象</param>
            /// <param name="sErr">错误提示信息 为空或0时表成功</param>
            /// <returns>程序调用是否成功</returns>        
            public static bool GetValueBySql(Socket sktX, string sSql, string sFldsDate, string sFldValue, out object objValue, out string sErr)
            {
                bool bX = false;
                sErr = "";
                objValue = null;
                DataSet dsX = GetDataBySql(sktX, sSql, sFldsDate, out sErr);
                if (sErr == "")
                {
                    DataTable tbX = null;
                    tbX = dsX.Tables["result"];
                    object objX = null;
                    objX = tbX.Rows[0]["returncode"];
                    if (objX != null)
                    {
                        if (objX.ToString() == "-1")
                        {
                            sErr = tbX.Rows[0]["returndesc"].ToString();
                            dsX.Clear();
                            return bX;
                        }
                    }
                    tbX = dsX.Tables["data"];
                    if (tbX.Rows.Count > 0)
                    {
                        if (sFldValue.Trim().Length > 0)
                            objValue = tbX.Rows[0][sFldValue.Trim()];
                        else
                            objValue = tbX.Rows[0][0];
                        bX = true;
                    }
                }


                return bX;
            }
            public static bool GetValueBySql(string sSql, string sFldsDate, string sFldValue, out object objValue, out string sErr)
            {
                objValue = null;
                sErr = "";
                return GetValueBySql(null, sSql, sFldsDate, sFldValue, out objValue, out sErr);
            }

            /// <summary>
            /// 对某表产生新的编号
            /// </summary>
            /// <param name="sktX">Socket 对象实例，可以为 null</param>
            /// <param name="sTbName">表名</param>
            /// <param name="sKeyFld">主键</param>
            /// <param name="nLength">编号长度</param>
            /// <param name="sHeader">编号前缀</param>
            /// <param name="sFldCon">条件字段</param>
            /// <param name="sValueCon">条件值</param>
            /// <returns></returns>
            public static string GetNewId(Socket sktX, string sTbName, string sKeyFld, int nLength, string sHeader, string sFldCon, string sValueCon, out string sErr)
            {
                //sp_GetNewId(@TbName varchar(50),@FldKey varchar(50),@Len int=0,@ReplaChar varchar(2)='0',@Header varchar(10)='',
                //@FldCon varchar(50)='',@ValueCon varchar(50)='')
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                /*sp_GetNewId(pTbName varchar2,pFldKey varchar2,pLen number,pReplaceChar varchar2,pHeader varchar2,pFldCon varchar2,pValueCon varchar2, 
                  Cur_Result out sys_refCursor)
                 * */
                cmdInfo.SqlText = "sp_GetNewId :pTbName, :pFldKey, :pLen , :pReplaceChar, :pHeader, :pFldCon, :pValueCon ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pTbName";           //参数名称 和实际定义的一致
                par.ParameterValue = sTbName;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pFldKey";           //参数名称 和实际定义的一致
                par.ParameterValue = sKeyFld;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pLen";           //参数名称 和实际定义的一致
                par.ParameterValue = nLength.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.Int;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pReplaceChar";           //参数名称 和实际定义的一致
                par.ParameterValue = "0";            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pHeader";           //参数名称 和实际定义的一致
                par.ParameterValue = sHeader;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pFldCon";           //参数名称 和实际定义的一致
                par.ParameterValue = "";            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pValueCon";           //参数名称 和实际定义的一致
                par.ParameterValue = "";            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);


                //执行命令
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;

                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
                if (dsX != null)
                {
                    tbX = dsX.Tables["data"];
                    if (tbX != null)
                        sId = tbX.Rows[0]["cNewId"].ToString();
                }
                dsX.Clear();
                return sId;
            }
            /// <summary>
            /// 对某表产生新的编号
            /// </summary>
            /// <param name="sktX">Socket 对象实例，可以为 null</param>
            /// <param name="sTbName">表名</param>
            /// <param name="sKeyFld">主键</param>
            /// <param name="nLength">编号长度</param>
            /// <param name="sHeader">编号前缀</param>
            /// <returns></returns>
            public static string GetNewId(Socket sktX, string sTbName, string sKeyFld, int nLength, string sHeader, out string sErr)
            {
                string sId = "";
                sErr = "";
                sId = GetNewId(sktX, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
                return sId;
            }
            /// <summary>
            /// 对某表产生新的编号
            /// </summary>
            /// <param name="sTbName">表名</param>
            /// <param name="sKeyFld">主键</param>
            /// <param name="nLength">编号长度</param>
            /// <param name="sHeader">编号前缀</param>
            /// <returns></returns>
            public static string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader, out string sErr)
            {
                string sId = "";
                sErr = "";
                sId = GetNewId(null, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
                return sId;
            }

            public static string GetNewId(string sTbName, string sKeyFld, int nLength, string sHeader)
            {
                string sId = "";

                string sErr = "";
                sId = GetNewId(null, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
                return sId;
            }

            /// <summary>
            /// 执行SQL语句但不返回记录集
            /// </summary>
            /// <param name="sktX">Socket  实例对象</param>
            /// <param name="sSql">Sql 语句</param>
            /// <param name="sFldsDate">在where 条件语句中包含 的时间字段，用，隔开</param>
            /// <param name="sErr">是否返回错误信息</param>
            /// <returns></returns>
            public static bool DoExecSql(Socket sktX, string sSql, string sFldsDate, out string sErr)
            {
                bool bRet = false;
                sErr = "";
                //DataSet dsT = GetDataBySql(sktX, sSql, "", sFldsDate, out sErr);
                DataSet dsX = GetDataBySql(sktX, sSql, "data", sFldsDate, out sErr);
                if (dsX == null || dsX.Tables["data"] == null || (sErr.Trim() != "" && sErr.Trim() != "0"))
                {
                    bRet = false;
                }
                else
                {
                    if (dsX.Tables["result"] == null || dsX.Tables["result"].Rows[0]["returncode"] == null)
                    {
                        bRet = false;
                    }
                    else
                    {
                        object objX = dsX.Tables["result"].Rows[0]["returncode"];
                        if (objX.ToString().Trim() != "0")
                        {
                            bRet = false;
                            sErr = dsX.Tables["result"].Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            bRet = true;
                        }
                    }
                }
                return bRet;
            }

        #endregion

        //--
        public static string sp_UpdatePalletStatus(Socket sktX, string pPalletId, out string sErr)
        {
            //sp_UpdatePalletStatus(@pPalletId varchar(30),@pPalletLevel int=9)
            //select @pResult cResult
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_UpdatePalletStatus :pPalletId ,:pPalletLevel ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPalletId";           //参数名称 和实际定义的一致
            par.ParameterValue = pPalletId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPalletLevel";           //参数名称 和实际定义的一致
            par.ParameterValue = "9";            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);

            //执行命令
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;

            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            if (dsX != null)
            {
                tbX = dsX.Tables["result"];
                if (tbX != null)
                {
                    object objX = null;
                    objX = tbX.Rows[0]["returncode"];
                    if (objX != null)
                    {
                        if (objX.ToString() != "0")
                        {
                            sErr = tbX.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            tbX = dsX.Tables[1];
                            objX = tbX.Rows[0]["cResult"];
                            if (objX != null)
                            {
                                sId = objX.ToString();
                            }
                        }
                    }

                }

            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }

        #region 过程

        /// <summary>
        /// 删除入库验收单据数据
        /// </summary>
        /// <param name="sktX">Socket 实例</param>
        /// <param name="pBNo">单号</param>
        /// <param name="pUser">当前用户编码</param>
        /// <param name="pCmptId">当前用户单位编码</param>
        /// <param name="pSysFrom">操作系统</param>
        /// <param name="sErr">输出 提示信息</param>
        /// <returns>返回执行结果 -1 出错，0 正常</returns>
        public static string SP_BillChkAcceptDel(Socket sktX, string pBNo, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            //SP_BillChkAcceptDel(@pBNo varchar(30),@pUser varchar(30),@pCmptId varchar(30),@pSysFrom varchar(20))
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "SP_BillChkAcceptDel :pBNo ,:pUser,:pCmptId,:pSysFrom ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pBNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pUser";           //参数名称 和实际定义的一致
            par.ParameterValue = pUser;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pCmptId";           //参数名称 和实际定义的一致
            par.ParameterValue = pCmptId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pSysFrom";           //参数名称 和实际定义的一致
            par.ParameterValue = pSysFrom;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
           

            //执行命令
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;

            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            if (dsX != null)
            {
                #region
                tbX = dsX.Tables["result"];
                if (tbX != null)
                {
                    object objX = null;
                    objX = tbX.Rows[0]["returncode"];
                    if (objX != null)
                    {
                        if (objX.ToString() != "0")
                        {
                            sErr = tbX.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            tbX = dsX.Tables[1];
                            objX = tbX.Rows[0]["cResultId"];
                            if (objX != null)
                            {
                                sId = objX.ToString();
                                objX = tbX.Rows[0]["cResult"];
                                sErr = objX.ToString();
                            }
                        }
                    }

                }
                #endregion
            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }

        /// <summary>
        /// 删除入库验收单据明细数据
        /// </summary>
        /// <param name="sktX">Socket 实例</param>
        /// <param name="pBNo">单号</param>
        /// <param name="pItem">单明细序号</param>
        /// <param name="pUser">当前用户编码</param>
        /// <param name="pCmptId">当前用户单位编码</param>
        /// <param name="pSysFrom">操作系统</param>
        /// <param name="sErr">输出 提示信息</param>
        /// <returns>返回执行结果 -1 出错，0 正常</returns>
        public static string sp_BillChkAcceptDtlDel(Socket sktX, string pBNo,int pItem, string pUser, string pCmptId, string pSysFrom, out string sErr)
        {
            /*sp_BillChkAcceptDtlDel
	            @pBNo varchar(30),@pItem int,@pUser varchar(30),@pCmptId varchar(30)='101',@pSysFrom varchar(20)='WMS'
            */
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_BillChkAcceptDtlDel :pBNo,:pItem ,:pUser,:pCmptId,:pSysFrom ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pBNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pItem";           //参数名称 和实际定义的一致
            par.ParameterValue = pItem.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pUser";           //参数名称 和实际定义的一致
            par.ParameterValue = pUser;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pCmptId";           //参数名称 和实际定义的一致
            par.ParameterValue = pCmptId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pSysFrom";           //参数名称 和实际定义的一致
            par.ParameterValue = pSysFrom;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------


            //执行命令
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;

            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            if (dsX != null)
            {
                #region
                tbX = dsX.Tables["result"];
                if (tbX != null)
                {
                    object objX = null;
                    objX = tbX.Rows[0]["returncode"];
                    if (objX != null)
                    {
                        if (objX.ToString() != "0")
                        {
                            sErr = tbX.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            tbX = dsX.Tables[1];
                            objX = tbX.Rows[0]["cResultId"];
                            if (objX != null)
                            {
                                sId = objX.ToString();
                                objX = tbX.Rows[0]["cResult"];
                                sErr = objX.ToString();
                            }
                        }
                    }

                }
                #endregion
            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }

        /// <summary>
        /// 根据物料号和生产日期获取该物料的批号
        /// </summary>
        /// <param name="sktX">Socket 实例</param>
        /// <param name="pMNo">物料编号</param>
        /// <param name="pDate">生产日期</param>
        /// <param name="sErr">输出信息提示</param>
        /// <returns>返回批号</returns>
        public static string sp_GetBatchNo(Socket sktX, string pMNo,string pDate, out string sErr)
        {
            /*sp_GetBatchNo 
                (pMNo varchar,,@pDate varchar(30)='',Cur_Result out sys_refCursor
                )
             select nBatchNo
             **/
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_GetBatchNo :pMNo,:pDate";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pMNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pDate";           //参数名称 和实际定义的一致
            par.ParameterValue = pDate;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            //---
            //执行命令
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;
            sId = "-1";
            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            if (dsX != null)
            {
                tbX = dsX.Tables["result"];
                if (tbX != null)
                {
                    object objX = null;
                    objX = tbX.Rows[0]["returncode"];
                    if (objX != null)
                    {
                        if (objX.ToString() != "0")
                        {
                            sErr = tbX.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            tbX = dsX.Tables[1];
                            objX = tbX.Rows[0]["nBatchNo"];
                            if (objX != null)
                            {
                                sId = objX.ToString().Trim();
                            }
                        }
                    }
                }
            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }

        /// <summary>
        /// 对退货入库的物料数据进行验证，退货客户及物料和数量是否合法
        /// </summary>
        /// <param name="sktX">Socket 实例</param>
        /// <param name="pBNo">退货入库单号</param>
        /// <param name="pBClass">单据类别</param>
        /// <param name="pMNo">退货物料号</param>
        /// <param name="pBatchNo">退货物料批号</param>
        /// <param name="pBackInQty">退货数量</param>
        /// <param name="pBNoForOut">出库单号</param>
        /// <param name="sErr">输出提示信息</param>
        /// <returns>返回校验结果： 0校验合法 -1 校验失败</returns>
        public static string sp_CheckBackInDtl(Socket sktX, string pBNo,int pBClass, string pMNo,string pBatchNo,double pBackInQty,string pBNoForOut, out string sErr)
        {
            /*sp_CheckBackInDtl 
            (
	        @pBNo varchar(30),@pMNo varchar(30),@pBClass int ,@pBatchNo varchar(50),@pBackInQty numeric(18,4),
	        @pBNoForOut varchar(30)
            )
             **/
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_CheckBackInDtl :pBNo,:pBClass,:pMNo,:pBatchNo,:pBackInQty,:pBNoForOut";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量 

            #region
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pBNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------ 
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBClass";           //参数名称 和实际定义的一致
            par.ParameterValue = pBClass.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------ 
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pMNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBatchNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pBatchNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            #endregion

            #region
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBackInQty";           //参数名称 和实际定义的一致
            par.ParameterValue = pBackInQty.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Double;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBNoForOut";           //参数名称 和实际定义的一致
            par.ParameterValue = pBNoForOut;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            #endregion

            //---
            //执行命令
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;
            sId = "-1";
            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            if (dsX != null)
            {
                #region
                tbX = dsX.Tables["result"];
                if (tbX != null)
                {
                    object objX = null;
                    objX = tbX.Rows[0]["returncode"];
                    if (objX != null)
                    {
                        sId = objX.ToString();
                        if (sId != "0")
                        {
                            sErr = tbX.Rows[0]["returndesc"].ToString();
                        }
                        else
                        {
                            tbX = dsX.Tables[1];
                            objX = tbX.Rows[0]["cResultId"];
                            if (objX != null)
                            {
                                sId = objX.ToString().Trim();
                                objX = tbX.Rows[0]["cResult"] ;
                                sErr = objX.ToString() ;
                            }
                        }
                    }
                }
                #endregion
            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }

         
	
         /// <summary>
         /// 保存用户操作日志
         /// </summary>
         /// <param name="sktX">Socket 对象</param>
         /// <param name="pUser">当前操作用户名</param>
         /// <param name="pSysFrom">数据来源系统WMS/RF/ECS/DPS</param>
         /// <param name="pOptType">操作类型</param>
         /// <param name="pOptDesc">操作描述</param>
         /// <param name="pCmptId">公司编码</param>
         /// <param name="sErr">输出错误提示信息</param>
         /// <returns>返回是否成功</returns>
         public static bool SP_INSERTUSERLOG(Socket sktX,string pUser,string pSysFrom,string pOptType,string pOptDesc,string pCmptId , out string sErr)
            {
                //
                
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                /*SP_INSERTUSERLOG @pUser varchar(20),@pSysFrom varchar(20),@pOptType varchar(20),@pOptDesc varchar(30),@pCmptId varchar(30)
                */
                cmdInfo.SqlText = "SP_INSERTUSERLOG :pUser, :pSysFrom, :pOptType , :pOptDesc, :pCmptId";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量  
                #region
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pUser";           //参数名称 和实际定义的一致
                par.ParameterValue = pUser;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pSysFrom";           //参数名称 和实际定义的一致
                par.ParameterValue = pSysFrom;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pOptType";           //参数名称 和实际定义的一致
                par.ParameterValue = pOptType;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                #endregion
                //---
                #region
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pOptDesc";           //参数名称 和实际定义的一致
                par.ParameterValue = pOptDesc;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pCmptId";           //参数名称 和实际定义的一致
                par.ParameterValue = pCmptId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                #endregion

                //执行命令
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                
                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
                if (dsX != null)
                {
                    tbX = dsX.Tables["data"];
                    if (tbX != null)
                        sId = tbX.Rows[0]["cResultId"].ToString();
                    if (sId.Trim() != "0" && sId.Trim() != "")
                    {
                        sErr = tbX.Rows[0]["cResult"].ToString();
                    }
                }
                bool bOK = false;
                bOK = (sId.Trim() =="" || sId.Trim() =="0");
                dsX.Clear();
                return bOK;
            }


        #endregion
    }
}
