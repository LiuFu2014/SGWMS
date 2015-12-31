using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Sockets;
using SunEast.App;
using SunEast;
using DBCommInfo;


namespace WareStoreMS
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

            /// <summary>
            /// 根据ＳＱＬ语句返回数据表集
            /// </summary>
            /// <param name="sktX"></param>
            /// <param name="bIsSaveDataxml"></param>
            /// <param name="sSql"></param>
            /// <param name="sTbName"></param>
            /// <param name="nPageSize"></param>
            /// <param name="nPageIndex"></param>
            /// <param name="sFldsDate"></param>
            /// <param name="sErr"></param>
            /// <returns></returns>
            public static DataTable GetDataTableBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, string sFldsDate, out string sErr)
            {
                sErr = "";
                DataTable tbData = null;
                DataSet dsX = GetDataBySql(sktX, bIsSaveDataxml, sSql, sTbName, nPageSize, nPageIndex, sFldsDate, out sErr);
                if (sErr.Trim() == "" || sErr.Trim() == "0")
                {
                    if (dsX != null && dsX.Tables.Count > 0)
                    {
                        DataTable tbX = dsX.Tables[0];
                        if (tbX.Rows.Count > 0)
                        {
                            sErr = "";
                            if (tbX.Rows[0][0].ToString() == "-1")
                            {
                                sErr = tbX.Rows[0][1].ToString();
                            }
                        }
                        if (dsX.Tables[sTbName] != null)
                        {
                            tbData = dsX.Tables[sTbName].Copy();
                        }

                    }
                }
                if (dsX != null)
                {
                    dsX.Clear();
                    dsX = null;
                }
                return tbData;
            }

        #endregion

        //--
       
        #region 过程
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

            public static DataTable sp_GetSlackMatCount(Socket sktX, string pWHId,string pDateFrom,string pDateTo, out string sErr)
            {
                //Procedure sp_GetSlackMatCount ( @pWHId varchar(30),@pDateFrom datetime,@pDateTo datetime)
                //select lst.cMNo,mat.cName cMName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark,mat.cUnit,sum(lst.fQty) fQty,max(lst.dLastDate) dLastDate
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "sp_GetSlackMatCount :pWHId ,:pDateFrom,:pDateTo ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
                par.ParameterValue = pWHId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateFrom";           //参数名称 和实际定义的一致
                par.ParameterValue =  pDateFrom;            //参数值 可以为""空
                par.DataType = ZqmDataType.DateTime;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateTo";           //参数名称 和实际定义的一致
                par.ParameterValue = pDateTo;            //参数值 可以为""空
                par.DataType = ZqmDataType.DateTime;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);

                //执行命令
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //获取服务器数据的类型对象
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                DataTable tbData = null;
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
                                tbData = dsX.Tables[1];
                                tbData.TableName = "SlackMatCount";
                            }
                        }

                    }

                }
                tbX.Clear();
                return tbData;
            }

            public static DataTable sp_GetSlackMatDtl(Socket sktX, string pWHId, string pMNo, string pDateFrom,string pDateTo, out string sErr)
            {
                //Procedure sp_GetSlackMatDtl ( @pWHId varchar(30),@pDateFrom datetime,@pDateTo datetime)
                //select lst.cMNo,mat.cName cMName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark,mat.cUnit,sum(lst.fQty) fQty,max(lst.dLastDate) dLastDate
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "sp_GetSlackMatDtl :pWHId ,:pMNo,:pDateFrom,:pDateTo ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
                par.ParameterValue = pWHId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
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
                par.ParameterName = "pDateFrom";           //参数名称 和实际定义的一致
                par.ParameterValue = pDateFrom;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateTo";           //参数名称 和实际定义的一致
                par.ParameterValue = pDateTo;            //参数值 可以为""空
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
                DataTable tbData = null;
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
                                tbData = dsX.Tables[1];
                                tbData.TableName = "SlackMatCount";
                            }
                        }

                    }

                }
                tbX.Clear();
                return tbData;
            }

            public static DataTable sp_GetSlackMatDtlList(Socket sktX, string pWHId, string pDateFrom, string pDateTo, out string sErr)
            {
                //Procedure sp_GetSlackMatDtlList ( @pWHId varchar(30),@pDateFrom datetime,@pDateTo datetime)
                //select lst.cMNo,mat.cName cMName,mat.cSpec,mat.cMatStyle,mat.cMatQCLevel,mat.cMatOther,mat.cRemark,mat.cUnit,sum(lst.fQty) fQty,max(lst.dLastDate) dLastDate
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "sp_GetSlackMatDtlList :pWHId ,:pDateFrom,:pDateTo ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
                par.ParameterValue = pWHId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateFrom";           //参数名称 和实际定义的一致
                par.ParameterValue = pDateFrom;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDateTo";           //参数名称 和实际定义的一致
                par.ParameterValue = pDateTo;            //参数值 可以为""空
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
                DataTable tbData = null;
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
                                tbData = dsX.Tables[1];
                                tbData.TableName = "SlackMatDtlList";
                            }
                        }

                    }

                }
                tbX.Clear();
                return tbData;
            }

            /// <summary>
            /// 直接修改库存数量
            /// </summary>
            /// <param name="sktX"></param>
            /// <param name="pUserId"></param>
            /// <param name="pCmptId"></param>
            /// <param name="pSysFrom"></param>
            /// <param name="pPalletId"></param>
            /// <param name="pBoxId"></param>
            /// <param name="pMNo"></param>
            /// <param name="pRealQty"></param>
            /// <param name="pBNoIn"></param>
            /// <param name="pItemIn"></param>
            /// <param name="pAjustBNo"></param>
            /// <param name="sErr"></param>
            /// <returns></returns>
            public static string SP_Ajust_UpdateStoreQty(Socket sktX, string pUserId, string pCmptId, string pSysFrom, string pPalletId, string pBoxId, string pMNo, double pRealQty,
                    string pBNoIn, string pItemIn, string pAjustBNo, out string sErr)
            {
                /*SP_Ajust_UpdateStoreQty 
                @pUserId varchar(20),@pCmptId varchar(10),@pSysFrom varchar(10), 
                @pPalletId varchar(30),@pBoxId varchar(30), @pMNo varchar(30),@pRealQty numeric(18,4) ,
                @pBNoIn varchar(30),@pItemIn int, @pAjustBNo varchar (30)
                 * */

                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "SP_Ajust_UpdateStoreQty :pUserId, :pCmptId, :pSysFrom , :pPalletId, :pBoxId, :pMNo, :pRealQty,:pBNoIn,:pItemIn,:pAjustBNo";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "rf";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                //定义参数
                #region par

                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pUserId";           //参数名称 和实际定义的一致
                par.ParameterValue = pUserId;            //参数值 可以为""空
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
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pSysFrom";           //参数名称 和实际定义的一致
                par.ParameterValue = pSysFrom;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pPalletId";           //参数名称 和实际定义的一致
                par.ParameterValue = pPalletId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                #endregion

                #region par
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pBoxId";           //参数名称 和实际定义的一致
                par.ParameterValue = pBoxId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
                par.ParameterValue = pMNo;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pRealQty";           //参数名称 和实际定义的一致
                par.ParameterValue = pRealQty.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.Double;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                ////---   
                #endregion

                #region par
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pBNoIn";           //参数名称 和实际定义的一致
                par.ParameterValue = pBNoIn;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pItemIn";           //参数名称 和实际定义的一致
                par.ParameterValue = pItemIn;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pAjustBNo";           //参数名称 和实际定义的一致
                par.ParameterValue = pAjustBNo;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                #endregion
                ////---

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
                                objX = tbX.Rows[0]["cResultId"];
                                if (objX != null)
                                {
                                    sId = objX.ToString();
                                }
                                objX = tbX.Rows[0]["cResult"];
                                if (objX != null)
                                {
                                    sErr = objX.ToString();
                                }
                            }
                        }

                    }
                    dsX.Clear();
                    dsX.Dispose();
                }

                return sId;
            }

            public static string sp_Chk_WriteAjustDtl(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pAjustNo, string pWHId, string pPosId,
                string pPalletId, string pBoxId, string pMNo, double pDiff, string pBNoIn, int pItemIn, string pCheckNo,
                string pWHIdErp,string pAreaIdErp,string pPosIdErp, out string sErr)
            {
                /*SP_CHK_WRITEAJUSTDTL 
                    (@pUser varchar(20),@pCmptId varchar(30),@pSysFrom varchar(10),@pAjustNo varchar(30), @pWHId varchar(30),@pPosId varchar(30),
                    @pPalletId varchar(30),@pBoxId varchar(30), @pMNo varchar(30), @pDiff numeric(18,4),@pBNoIn varchar(30),@pItemIn int,@pCheckNo varchar(30))
                   
                //select cResult,cResultId from dual ; 
                 **/
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
                cmdInfo.SqlText = "SP_CHK_WRITEAJUSTDTL :pUser,:pCmptId,:pSysFrom,:pAjustNo,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBNoIn,:pItemIn,:pCheckNo,:pWHIdErp,:pAreaIdErp,:pPosIdErp ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
                cmdInfo.PageIndex = 0;                                          //需要分页时的页号
                cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
                cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
                //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
                #region  ss
                //定义参数
                ZqmParamter par = null;           //参数对象 变量                          
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pUser";           //参数名称 和实际定义的一致
                par.ParameterValue = pUser;            //参数值 可以为""空
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
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pSysFrom";           //参数名称 和实际定义的一致
                par.ParameterValue = pSysFrom;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pAjustNo";           //参数名称 和实际定义的一致
                par.ParameterValue = pAjustNo;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
                par.ParameterValue = pWHId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pPosId";           //参数名称 和实际定义的一致
                par.ParameterValue = pPosId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                #endregion

                #region ss
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pPalletId";           //参数名称 和实际定义的一致
                par.ParameterValue = pPalletId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pBoxId";           //参数名称 和实际定义的一致
                par.ParameterValue = pBoxId;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
                par.ParameterValue = pMNo;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //---

                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pDiff";           //参数名称 和实际定义的一致
                par.ParameterValue = pDiff.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.Double;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------

                #endregion

                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pBNoIn";           //参数名称 和实际定义的一致
                par.ParameterValue = pBNoIn;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pItemIn";           //参数名称 和实际定义的一致
                par.ParameterValue = pItemIn.ToString();            //参数值 可以为""空
                par.DataType = ZqmDataType.Int;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pCheckNo";           //参数名称 和实际定义的一致
                par.ParameterValue = pCheckNo;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pWHIdErp";           //参数名称 和实际定义的一致
                par.ParameterValue = pWHIdErp;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pAreaIdErp";           //参数名称 和实际定义的一致
                par.ParameterValue = pAreaIdErp;            //参数值 可以为""空
                par.DataType = ZqmDataType.String;  //参数的数据类型
                par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
                //添加参数
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //参数对象实例
                par.ParameterName = "pPosIdErp";           //参数名称 和实际定义的一致
                par.ParameterValue = pPosIdErp;            //参数值 可以为""空
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
                                    if (sId != "0")
                                        sErr = sId;
                                    else sErr = "";
                                }
                            }
                        }

                    }

                }
                dsX.Clear();
                dsX.Dispose();
                return sId;
            }

        public static string sp_Chk_WriteChkDtl(Socket sktX, string pUser, string pCmptId, string pSysFrom, string pWHId, string pPosId,
           string pPalletId, string pBoxId, string pMNo, double pDiff, double pBad, string pUnit, string pBNoIn, int pItemIn, string pCheckNo, 
           string pWHIdErp,string pAreaIdErp,string pPosIdErp, out string sErr)
        {
            /*sp_Chk_WriteChkDtl
                (pUser varchar2,pCmptId varchar2,pSysFrom varchar2,pWHId varchar2, pPosId varchar2,
                pPalletId varchar2,pBoxId varchar2, pMNo varchar2, pDiff numeric ,pBad numeric,pUnit varchar2, pBNoIn varchar2,pItemIn integer,pCheckNo varchar2 :=' ',
                Cur_Result out sys_refCursor
                )
             * 
              sp_Chk_WriteChkDtl
                (pUserId varchar2,pCmptId varchar2,pSysFrom varchar2,pWHId varchar2, pPosId varchar2,
                pPalletId varchar2,pBoxId varchar2, pMNo varchar2, pDiff numeric ,pBad numeric,pUnit varchar2, pBNoIn varchar2,pItemIn integer,pCheckNo varchar2 :=' ',
                Cur_Result out sys_refCursor
                )
            //select cResult,cResultId from dual ; 
             **/
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_Chk_WriteChkDtl :pUserId,:pCmptId,:pSysFrom,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBad,:pUnit,:pBNoIn,:pItemIn,:pCheckNo,:pWHIdErp,:pAreaIdErp,:pPosIdErp ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            #region  ss
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pUserId";           //参数名称 和实际定义的一致
            par.ParameterValue = pUser;            //参数值 可以为""空
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
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pSysFrom";           //参数名称 和实际定义的一致
            par.ParameterValue = pSysFrom;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pWHId";           //参数名称 和实际定义的一致
            par.ParameterValue = pWHId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPosId";           //参数名称 和实际定义的一致
            par.ParameterValue = pPosId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            #endregion

            #region ss
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPalletId";           //参数名称 和实际定义的一致
            par.ParameterValue = pPalletId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBoxId";           //参数名称 和实际定义的一致
            par.ParameterValue = pBoxId;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pMNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pMNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pDiff";           //参数名称 和实际定义的一致
            par.ParameterValue = pDiff.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Double;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBad";           //参数名称 和实际定义的一致
            par.ParameterValue = pBad.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Double;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pUnit";           //参数名称 和实际定义的一致
            par.ParameterValue = pUnit;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            #endregion
            #region ss
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pBNoIn";           //参数名称 和实际定义的一致
            par.ParameterValue = pBNoIn;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pItemIn";           //参数名称 和实际定义的一致
            par.ParameterValue = pItemIn.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pCheckNo";           //参数名称 和实际定义的一致
            par.ParameterValue = pCheckNo;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            #endregion
            #region ss
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pWHIdErp";           //参数名称 和实际定义的一致
            par.ParameterValue = pWHIdErp;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pAreaIdErp";           //参数名称 和实际定义的一致
            par.ParameterValue = pAreaIdErp;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPosIdErp";           //参数名称 和实际定义的一致
            par.ParameterValue = pPosIdErp;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
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
                                if (sId != "0")
                                    sErr = sId;
                                else sErr = "";
                            }
                        }
                    }

                }

            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }

            public static string sp_Pack_DoWareCellMove(Socket sktX, string pSysType, string pPosIdFrom, string pPosIdTo, string pUser, string pCmptId,int pIsDoNow, out string sErr)
        {
            /*sp_Pack_DoWareCellMove
                (pSysType varchar2,pPosIdFrom varchar2,
                  pPosIdTo varchar2,pUser varchar2,pCmptId varchar2:='101',pIsDoNow int:=0
                Cur_Result out sys_refCursor
                )
            //select pResult cResult,pResultId cResultId from dual ; 
             **/
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_Pack_DoWareCellMove :pSysType, :pPosIdFrom,:pPosIdTo,:pUser,:pCmptId,:pIsDoNow ";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          

            //---
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pSysType";           //参数名称 和实际定义的一致
            par.ParameterValue = pSysType;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPosIdFrom";           //参数名称 和实际定义的一致
            par.ParameterValue = pPosIdFrom;            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "pPosIdTo";           //参数名称 和实际定义的一致
            par.ParameterValue = pPosIdTo;            //参数值 可以为""空
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
            par.ParameterName = "pIsDoNow";           //参数名称 和实际定义的一致
            par.ParameterValue = pIsDoNow.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.Int;  //参数的数据类型
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
                                if (sId != "0")
                                    sErr = sId;
                                else sErr = "";
                            }
                        }
                    }

                }

            }
            dsX.Clear();
            dsX.Dispose();
            return sId;
        }


        #endregion
    }
}
