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
        #region ��������

       
            /// <summary>
            /// ִ��SQL��䣬�õ���¼��
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
                    sErr = "SQL��䲻��Ϊ�գ�";
                    return null;
                }
                string sTb = "data";
                if (sTbName.Trim() != "")
                {
                    sTb = sTbName.Trim();
                }
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = sSql;             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctSql;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = nPageIndex;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = nPageSize;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                cmdInfo.DataTableName = sTb;                          //ָ��������ݼ�¼������           
                cmdInfo.FldsData = sFldsDate.Trim();
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                //DataTable tbX = null;          
                dsX = sdcX.GetDataSet(sktX, cmdInfo, bIsSaveDataxml, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
                return dsX;
            }

            public static DataSet GetDataBySql(Socket sktX, bool bIsSaveDataxml, string sSql, string sTbName, int nPageSize, int nPageIndex, out string sErr)
            {
                DataSet dsX = null;
                sErr = "";
                if (sSql.Trim().Length == 0)
                {
                    sErr = "SQL��䲻��Ϊ�գ�";
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
                    sErr = "SQL��䲻��Ϊ�գ�";
                    return dsX;
                }
                return GetDataBySql(null, false, sSql, sTbName, nPageSize, nPageIndex, out sErr);
            }

            /// <summary>
            /// ִ��SQL��䣬�õ���¼��
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
                    sErr = "SQL��䲻��Ϊ�գ�";
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
                    sErr = "SQL��䲻��Ϊ�գ�";
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
                    sErr = "SQL��䲻��Ϊ�գ�";
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
                    sErr = "SQL��䲻��Ϊ�գ�";
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
                    sErr = "SQL��䲻��Ϊ�գ�";
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
                    sErr = "SQL��䲻��Ϊ�գ�";
                    return null;
                }
                dsX = GetDataBySql(sktX, false, sSql, sTbName, 0, 0, sFldsDate, out sErr);
                return (dsX);
            }

            /// <summary>
            /// ����SQL����ȡ����һ���ֶε�ֵ����object ��������
            /// </summary>
            /// <param name="sktX">Socket ����</param>
            /// <param name="sSql">sql ��䣬����</param>
            /// <param name="sFldsDate">SQL����� �����е��������͵��ֶΣ���','����</param>
            /// <param name="sFldValue">���践��ֵ���ֶ�������Ϊ��ʱ�������ص�һ�е�ֵ</param>
            /// <param name="objValue">�����ֶ�ֵ����</param>
            /// <param name="sErr">������ʾ��Ϣ Ϊ�ջ�0ʱ��ɹ�</param>
            /// <returns>��������Ƿ�ɹ�</returns>        
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
            /// ��ĳ������µı��
            /// </summary>
            /// <param name="sktX">Socket ����ʵ��������Ϊ null</param>
            /// <param name="sTbName">����</param>
            /// <param name="sKeyFld">����</param>
            /// <param name="nLength">��ų���</param>
            /// <param name="sHeader">���ǰ׺</param>
            /// <param name="sFldCon">�����ֶ�</param>
            /// <param name="sValueCon">����ֵ</param>
            /// <returns></returns>
            public static string GetNewId(Socket sktX, string sTbName, string sKeyFld, int nLength, string sHeader, string sFldCon, string sValueCon, out string sErr)
            {
                //sp_GetNewId(@TbName varchar(50),@FldKey varchar(50),@Len int=0,@ReplaChar varchar(2)='0',@Header varchar(10)='',
                //@FldCon varchar(50)='',@ValueCon varchar(50)='')
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                /*sp_GetNewId(pTbName varchar2,pFldKey varchar2,pLen number,pReplaceChar varchar2,pHeader varchar2,pFldCon varchar2,pValueCon varchar2, 
                  Cur_Result out sys_refCursor)
                 * */
                cmdInfo.SqlText = "sp_GetNewId :pTbName, :pFldKey, :pLen , :pReplaceChar, :pHeader, :pFldCon, :pValueCon ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pTbName";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = sTbName;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pFldKey";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = sKeyFld;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pLen";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = nLength.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Int;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pReplaceChar";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = "0";            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pHeader";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = sHeader;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pFldCon";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = "";            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pValueCon";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = "";            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);


                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;

                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
            /// ��ĳ������µı��
            /// </summary>
            /// <param name="sktX">Socket ����ʵ��������Ϊ null</param>
            /// <param name="sTbName">����</param>
            /// <param name="sKeyFld">����</param>
            /// <param name="nLength">��ų���</param>
            /// <param name="sHeader">���ǰ׺</param>
            /// <returns></returns>
            public static string GetNewId(Socket sktX, string sTbName, string sKeyFld, int nLength, string sHeader, out string sErr)
            {
                string sId = "";
                sErr = "";
                sId = GetNewId(sktX, sTbName, sKeyFld, nLength, sHeader, "", "", out sErr);
                return sId;
            }
            /// <summary>
            /// ��ĳ������µı��
            /// </summary>
            /// <param name="sTbName">����</param>
            /// <param name="sKeyFld">����</param>
            /// <param name="nLength">��ų���</param>
            /// <param name="sHeader">���ǰ׺</param>
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
            /// ִ��SQL��䵫�����ؼ�¼��
            /// </summary>
            /// <param name="sktX">Socket  ʵ������</param>
            /// <param name="sSql">Sql ���</param>
            /// <param name="sFldsDate">��where ��������а��� ��ʱ���ֶΣ��ã�����</param>
            /// <param name="sErr">�Ƿ񷵻ش�����Ϣ</param>
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
            /// ���ݣӣѣ���䷵�����ݱ�
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
       
        #region ����
            public static string sp_UpdatePalletStatus(Socket sktX, string pPalletId, out string sErr)
            {
                //sp_UpdatePalletStatus(@pPalletId varchar(30),@pPalletLevel int=9)
                //select @pResult cResult
                string sId = "";
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "sp_UpdatePalletStatus :pPalletId ,:pPalletLevel ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pPalletId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPalletLevel";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = "9";            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Int;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);

                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;

                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "sp_GetSlackMatCount :pWHId ,:pDateFrom,:pDateTo ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pWHId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateFrom";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue =  pDateFrom;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.DateTime;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateTo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pDateTo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.DateTime;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);

                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                DataTable tbData = null;
                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "sp_GetSlackMatDtl :pWHId ,:pMNo,:pDateFrom,:pDateTo ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pWHId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pMNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateFrom";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pDateFrom;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateTo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pDateTo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);

                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                DataTable tbData = null;
                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "sp_GetSlackMatDtlList :pWHId ,:pDateFrom,:pDateTo ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pWHId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateFrom";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pDateFrom;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDateTo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pDateTo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);

                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;
                DataTable tbData = null;
                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
            /// ֱ���޸Ŀ������
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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "SP_Ajust_UpdateStoreQty :pUserId, :pCmptId, :pSysFrom , :pPalletId, :pBoxId, :pMNo, :pRealQty,:pBNoIn,:pItemIn,:pAjustBNo";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "rf";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                //�������
                #region par

                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pUserId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pUserId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pCmptId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pCmptId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pSysFrom";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pSysFrom;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pPalletId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                #endregion

                #region par
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pBoxId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pBoxId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pMNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pRealQty";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pRealQty.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Double;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                ////---   
                #endregion

                #region par
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pBNoIn";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pBNoIn;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pItemIn";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pItemIn;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                ////---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pAjustBNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pAjustBNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                #endregion
                ////---

                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;

                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
                DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
                cmdInfo.SqlText = "SP_CHK_WRITEAJUSTDTL :pUser,:pCmptId,:pSysFrom,:pAjustNo,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBNoIn,:pItemIn,:pCheckNo,:pWHIdErp,:pAreaIdErp,:pPosIdErp ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

                cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
                cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
                cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
                cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
                //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
                #region  ss
                //�������
                ZqmParamter par = null;           //�������� ����                          
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pUser";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pUser;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pCmptId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pCmptId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pSysFrom";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pSysFrom;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pAjustNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pAjustNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pWHId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPosId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pPosId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                #endregion

                #region ss
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pPalletId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pBoxId";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pBoxId;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pMNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //---

                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pDiff";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pDiff.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Double;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------

                #endregion

                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pBNoIn";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pBNoIn;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pItemIn";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pItemIn.ToString();            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.Int;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pCheckNo";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pCheckNo;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pWHIdErp";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pWHIdErp;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pAreaIdErp";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pAreaIdErp;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------
                par = new ZqmParamter();          //��������ʵ��
                par.ParameterName = "pPosIdErp";           //�������� ��ʵ�ʶ����һ��
                par.ParameterValue = pPosIdErp;            //����ֵ ����Ϊ""��
                par.DataType = ZqmDataType.String;  //��������������
                par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
                //��Ӳ���
                cmdInfo.Parameters.Add(par);
                //------

                //ִ������
                SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
                //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
                sErr = "";
                DataSet dsX = null;
                DataTable tbX = null;

                dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "sp_Chk_WriteChkDtl :pUserId,:pCmptId,:pSysFrom,:pWHId,:pPosId,:pPalletId,:pBoxId ,:pMNo,:pDiff,:pBad,:pUnit,:pBNoIn,:pItemIn,:pCheckNo,:pWHIdErp,:pAreaIdErp,:pPosIdErp ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            #region  ss
            //�������
            ZqmParamter par = null;           //�������� ����                          
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pUserId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pUser;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pCmptId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pCmptId;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pSysFrom";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pSysFrom;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pWHId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pWHId;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pPosId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pPosId;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            #endregion

            #region ss
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pPalletId;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pBoxId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pBoxId;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pMNo";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pMNo;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //---

            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pDiff";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pDiff.ToString();            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.Double;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pBad";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pBad.ToString();            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.Double;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pUnit";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pUnit;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            #endregion
            #region ss
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pBNoIn";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pBNoIn;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pItemIn";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pItemIn.ToString();            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.Int;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pCheckNo";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pCheckNo;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            #endregion
            #region ss
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pWHIdErp";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pWHIdErp;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pAreaIdErp";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pAreaIdErp;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pPosIdErp";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pPosIdErp;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            #endregion
            //ִ������
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;

            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            cmdInfo.SqlText = "sp_Pack_DoWareCellMove :pSysType, :pPosIdFrom,:pPosIdTo,:pUser,:pCmptId,:pIsDoNow ";                             //SQL���  �� �洢������ ���в����������ڲ�����������

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            //�������
            ZqmParamter par = null;           //�������� ����                          

            //---
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pSysType";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pSysType;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pPosIdFrom";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pPosIdFrom;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pPosIdTo";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pPosIdTo;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pUser";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pUser;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pCmptId";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pCmptId;            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.String;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //��������ʵ��
            par.ParameterName = "pIsDoNow";           //�������� ��ʵ�ʶ����һ��
            par.ParameterValue = pIsDoNow.ToString();            //����ֵ ����Ϊ""��
            par.DataType = ZqmDataType.Int;  //��������������
            par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            //��Ӳ���
            cmdInfo.Parameters.Add(par);
            //------

            //ִ������
            SunEast.SeDBClient sdcX = new SunEast.SeDBClient();                     //��ȡ���������ݵ����Ͷ���
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //�Զ����������ļ���
            sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;

            dsX = sdcX.GetDataSet(sktX, cmdInfo, false, out sErr);               //ͨ����ȡ���������ݶ����GetDataSet������ȡ����
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
