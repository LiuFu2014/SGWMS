using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Sockets;
using SunEast.App;
using SunEast;
using DBCommInfo;


namespace UserMS
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

        #endregion

        //--
        /// <summary>
        /// �Զ����¹�Ӧ�̼��ͻ������ʵĺϷ���
        /// </summary>
        /// <param name="sErr"></param>
        /// <returns></returns>
        public static string sp_UpdateCuSupplyerUseful(Socket sktX, out string sErr)
        {
            //sp_UpdateCuSupplyerUseful()
            //select @pResult cResult
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//ִ������Ķ���
            //cmdInfo.SqlText = "sp_UpdatePalletStatus :pPalletId ,:pPalletLevel ";                             //SQL���  �� �洢������ ���в����������ڲ�����������
            cmdInfo.SqlText = "sp_UpdateCuSupplyerUseful "; 
            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL��������  SqlCommandType.sctSql  SQL ��� SqlCommandType.sctProcedure ��洢����
            cmdInfo.PageIndex = 0;                                          //��Ҫ��ҳʱ��ҳ��
            cmdInfo.PageSize = 0;                                           //��Ҫ��ҳʱ��ÿҳ��¼����
            cmdInfo.FromSysType = "dotnet";                                 //���ô��������ݵķ�ʽ��php ����<tr><td></td></tr> xml ���� ֱ�Ӳ���ado �ļ�¼����ʽ
            //cmdInfo.DataTableName = strTbNameMain;                          //ָ��������ݼ�¼������  Ĭ��Ϊ data
            //�������
            ZqmParamter par = null;           //�������� ����                          
            //par = new ZqmParamter();          //��������ʵ��
            //par.ParameterName = "pPalletId";           //�������� ��ʵ�ʶ����һ��
            //par.ParameterValue = pPalletId;            //����ֵ ����Ϊ""��
            //par.DataType = ZqmDataType.String;  //��������������
            //par.ParameterDir = ZqmParameterDirction.Input;    //ָ������ Ϊ���롢�������
            ////��Ӳ���
            //cmdInfo.Parameters.Add(par);
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
                            objX = tbX.Rows[0]["nCount"];
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

        #region ����

        #endregion
    }
}
