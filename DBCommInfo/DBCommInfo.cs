using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data ;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;

namespace DBCommInfo
{
    #region 公共类型
        /// 对表写或查询操作类型
        /// </summary>
        public enum DBOperateType { optNone = 0, optBrowser = 1, optNew = 2, optEdit = 3, optDelete = 4 }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DataBaseType { dbtMSSQL = 0, dbtMSAccess = 1, dbtOracle = 2, dbtMySQL = 3, dbtDBTwo = 4, dbtParadox = 5, dbtExcel = 6 };
        /// <summary>
        /// SQL命令类型 
        /// </summary>
        public enum SqlCommandType { sctSql = 0, sctProcedure = 1 }
        
        /// <summary>
        /// 自定义数据类型
        /// </summary>
        public enum ZqmDataType { String=0,Int =1,Double=2,DateTime = 3,Bool =4}
        /// <summary>
        /// 自定义参数输入输出方向
        /// </summary>
        public enum ZqmParameterDirction { Input = 0, InputOutput = 1, Output = 2, ReturnValue =3}
    #endregion

    [Serializable]
    public class ZqmParamter
    {
        #region 属性
            private string parameterName="" ;
            public string ParameterName
            {
                get { return parameterName.Trim(); }
                set { parameterName = value.Trim(); }
            }

            private ZqmDataType dataType = ZqmDataType.String;
            public ZqmDataType DataType
            {
                get { return dataType; }
                set { dataType = value; }
            }

            private string parameterValue ="";
            public string ParameterValue
            {
                get { return parameterValue; }
                set { parameterValue = value; }
            }

            private ZqmParameterDirction parameterDir = ZqmParameterDirction.Input;
            public ZqmParameterDirction ParameterDir
            {
                get { return parameterDir; }
                set { parameterDir = value; }
            }
        #endregion

        #region 公共方法
        public ZqmParamter()
        {

        }
        #endregion

    }

    public class ZqmParameterCollection : ArrayList
    {
        //private ArrayList paraList = new ArrayList();
        public int Add(ZqmParamter value)
        {
            return base.Add(value);
        }
        //public int Add(ZqmParamter para)
        //{
        //    if (para == null)
        //    {
        //        return -1;
        //    }
        //    return paraList.Add(para);
        //}
        public ZqmParamter Add(string paraName, ZqmDataType paraDataType)
        {
            ZqmParamter para = new ZqmParamter();
            para.ParameterName = paraName;
            para.ParameterValue = "";
            para.DataType = paraDataType;
            para.ParameterDir = ZqmParameterDirction.Input;
            this.Add(para);
            return para;
        }
        public ZqmParamter Add(string paraName, ZqmDataType paraDataType,ZqmParameterDirction paraDirc)
        {
            ZqmParamter para = new ZqmParamter();
            para.ParameterName = paraName;
            para.ParameterValue = "";
            para.DataType = paraDataType;
            para.ParameterDir = paraDirc;
            this.Add(para);
            return para;
        }
        public ZqmParamter ItemOf(int index)
        {
            if (index >= this.Count)
                return null;
            return (ZqmParamter)this[index];
        }
        public ZqmParamter ItemOf(string paraName)
        {
            ZqmParamter parResult = null;
            if (this.Count > 0)
            {
                foreach (ZqmParamter par in this)
                {
                    if (par.ParameterName.Trim().ToLower() == paraName.Trim().ToLower())
                    {
                        parResult = par;
                        break;
                    }
                }
            }
            return parResult ;
        }
        public void RemoveItem(int index)
        {
            if (this.Count == 0) return;
            if (index >= this.Count) return;           
            this.RemoveAt(index);
        }
        public void RemoveAll()
        {
            this.Clear();
        }
    }

    [Serializable]
    public class DBSQLCommandInfo
    {
        #region 变量

        #endregion
        #region 属性
            private string sqlText = "";
            public string SqlText
            {
                get { return sqlText.Trim(); }
                set { sqlText = value; }
            }
            
            private SqlCommandType sqlType = SqlCommandType.sctProcedure;
            public SqlCommandType SqlType
            {
                get { return sqlType; }
                set { sqlType = value; }
            }
            private int pageSize = 0;
            public int PageSize
            {
                get { return pageSize; }
                set { pageSize = value; }
            }

            private int pageIndex = 0;
            public int PageIndex
            {
                get { return pageIndex; }
                set { pageIndex = value; }
            }

            private string fromSysType = "php";
            public string FromSysType
            {
                get { return fromSysType.Trim(); }
                set { fromSysType = value.Trim(); }
            }

            private string dataTableName = "data";
            public string DataTableName
            {
                get { return dataTableName.Trim(); }
                set { dataTableName = value.Trim(); }
            }

            private System.Text.Encoding myEncoding = System.Text.Encoding.UTF8 ;
            public System.Text.Encoding MyEncoding
            {
                get { return myEncoding; }
                set { myEncoding = value; }
            }

            private ZqmParameterCollection parameters= new ZqmParameterCollection();
            public ZqmParameterCollection Parameters
            {
                get { return parameters; }
                set { parameters = value; }                
            }

            private string fldsDate = "";
            public string FldsData
            {
                get { return fldsDate.Trim(); }
                set 
                {
                    string sX = value.Trim();
                    sX = sX.Replace('，',',');
                    fldsDate = sX.Trim(); 
                }
            }

        #endregion

        #region  方法
        public static ZqmDataType  GetDataTypeByName(string sType)
        {

            ZqmDataType dtX =ZqmDataType.String;
            if (sType == string.Empty || sType.Trim() == "")
            {
                return dtX;
            }
            string sX = sType.Trim().ToLower();
            if (sX.IndexOf("int") > 0)
            {
                dtX = ZqmDataType.Int ;
            }
            else if (sX.IndexOf("bool") > 0 || sX.IndexOf("bit") > 0)
            {
                dtX = ZqmDataType.Bool ;
            }
            else if (sX.IndexOf("num") > 0)
            {
                dtX = ZqmDataType.Double ;
            }
            else if (sX.IndexOf("date") > 0 || sX.IndexOf("time") > 0)
            {
                dtX = ZqmDataType.DateTime ;
            }
            else
                dtX =  ZqmDataType.String ;
            return dtX;
        }
            public DBSQLCommandInfo()
            {            
            }
            ~DBSQLCommandInfo()
            {
            }
            public bool ClearParameters()
            {
                bool bOK = false;

                parameters.Clear();
                parameters = null;
                return bOK;
            }

            public string GetSqlXMLText()
            {
                int iCount = 0;
                if (parameters != null && parameters.Count > 0)
                {
                    iCount = parameters.Count;
                }
                string sText = "";
                string sTmp = sqlText;
                sTmp = sTmp.Replace("<", "&lt;");
                sTmp = sTmp.Replace(">", "&gt;");
                StringBuilder sb = new StringBuilder("<sql>");
                sb.Append("<setcommand name=\"c1\" pagesize=\"" + pageSize.ToString() + "\" pageindex=\"" +
                        pageIndex.ToString() + "\" sqltype=\"" + ((int) sqlType).ToString() +"\" fromsystype=\"" + fromSysType.Trim() + "\" datatablename=\"" + 
                        dataTableName.Trim() + "\" myencoding=\"" + myEncoding.BodyName + "\" datetimefields=\"" + fldsDate + "\">");
                sb.Append(sTmp);
                sb.Append("</setcommand>");
                //
                if (iCount > 0)
                {
                    foreach (ZqmParamter para in parameters)
                    {
                        sb.Append("<param name =\"" + para.ParameterName + "\" type=\"" + para.DataType.ToString() + "\" value=\"" + para.ParameterValue + "\" />");
                    }
                }
                //
                
                sb.Append("</sql>");
                sb.Append("\n\n");
                sText = sb.ToString();
                sb.Remove(0, sb.Length);
                return sText;
                /*
                 <sql>
                    <setcommand name="c1" pagesize="2">
                            SELECT * from guest.testtable where a >:1 and b>:2
                    </setcommand>
                    <param name = "1" type="int" value="1" />
                    <param name = "2" type="datetime" value="2010.08.26 08:10:02" />
                </sql> 
                */

            }

            public bool GetSqlXMLText( out MemoryStream msOut,out string sErr)
            {
                bool bOK = false;
                sErr = "";

                msOut = null;
                msOut = new MemoryStream();

                string sText = GetSqlXMLText();
                if (sText != null && sText.Length > 0)
                {
                    byte[] bArr = null;
                    bArr = myEncoding.GetBytes(sText);
                    msOut.Write(bArr, 0, bArr.Length);
                    bOK = true;
                }
                else
                {
                    msOut = null;
                    sErr = "获取对象的SQL命令文本不存在，保存失败！";
                    return false;
                }
                return bOK;
            }

            public bool SaveSqlXMLToFile(string sFile ,out string sErr)
            {
                bool bOK = false;
                sErr = "";
                if (sFile == null || sFile.Length == 0)
                {
                    sErr = "保存文件路径为空，保存失败！";
                    return false;
                }
                string sText = GetSqlXMLText();
                if (sText == null || sText.Length == 0)
                {
                    sErr = "获取对象的SQL命令文本不存在，保存失败！";
                    return false;
                }
                using (FileStream fs = new FileStream(sFile, FileMode.OpenOrCreate))
                {
                    byte[] bArr = null;
                    bArr = myEncoding.GetBytes(sText);
                    fs.Write(bArr, 0, bArr.Length);
                    fs.Close();
                    fs.Dispose();
                    bOK = true;
                }
                return bOK ;
            }

            ///

            public bool LoadSQLXmlCmd(string sFile, out string sErr)
            {
                bool bOK = false;
                byte[] bBuff = null ;
                sErr = "";
                if(!File.Exists(sFile.Trim()))
                {
                    sErr = sFile +  "  文件不存在，加载文件失败！";
                    return false;
                }
                System.IO.FileStream fs = new FileStream(sFile.Trim(), FileMode.Open);
                try
                {
                    if(fs.Length > 0)
                    {
                        bBuff = new byte[fs.Length];
                        fs.Read(bBuff, 0, bBuff.Length);
                        System.IO.MemoryStream ms = new MemoryStream();
                        ms.Write(bBuff, 0, bBuff.Length);
                        bOK = LoadSQLXmlCmd(ms, out sErr);
                        ms.Close();
                        ms.Dispose();
                        bBuff = null;
                    }
                    fs.Close();
                    fs.Dispose();
                }
                catch (Exception err)
                {
                    sErr = err.Message;
                    bOK = false;
                }
                return bOK;
            }


            public bool LoadSQLXmlCmd(MemoryStream ms, out string sErr)
            {
                bool bOK = false;
                System.Xml.XmlDocument xmldoc = null;
               
                System.Xml.XmlNode root = null;

                System.Xml.XmlNodeList ndList = null;
                System.Xml.XmlNode ndSql = null;
                sErr = "";
                if (ms == null || ms.Length == 0)
                {
                    sErr = "文件数据位空，加载数据失败！";
                    return false;
                }
                if (ms.Length > 0) ms.Position = 0;
                try
                {
                    xmldoc = new System.Xml.XmlDocument();
                    xmldoc.Load(ms);
                    //xmldoc.Save("sqlcmd.xml");
                    bOK = true;
                }
                catch (XmlException err)
                {
                    sErr = err.Message;
                    return false;
                }
                string sX = "";
                //获取SQL
                ndSql = xmldoc.SelectSingleNode("sql/setcommand");
                if (ndSql != null)
                {
                    string sTmp = ndSql.InnerXml.Trim();
                    sTmp = sTmp.Replace("&lt;", "<");
                    sTmp = sTmp.Replace("&gt;", ">");
                    sqlText = sTmp;
                    int iTT = -1 ;
                    iTT = sqlText.ToLower().IndexOf("select ",0);
                    if (iTT < 0)
                    {
                        iTT = sqlText.ToLower().IndexOf("update ",0);
                    }
                    if (iTT < 0)
                    {
                        iTT = sqlText.ToLower().IndexOf("insert ",0);
                    }
                    if (iTT >= 0)
                    {
                        sqlType = SqlCommandType.sctSql;
                    }
                    else
                    {
                        sqlType = SqlCommandType.sctProcedure;
                    }
                    if (ndSql.Attributes["pagesize"] != null)
                    {
                        sX = ndSql.Attributes["pagesize"].Value.Trim();
                        try
                        {
                            pageSize = int.Parse(sX);
                        }
                        catch (Exception err)
                        {
                            pageSize = 0;
                        }
                    }
                    if (ndSql.Attributes["pageindex"] != null)
                    {
                        sX = ndSql.Attributes["pageindex"].Value.Trim();
                        try
                        {
                            pageIndex = int.Parse(sX);
                        }
                        catch (Exception err)
                        {
                            pageIndex = 0;
                        }
                    }
                    if (ndSql.Attributes["sqltype"] != null)
                    {
                        sX = ndSql.Attributes["sqltype"].Value.Trim();
                        try
                        {
                            sqlType = (SqlCommandType)int.Parse(sX);
                        }
                        catch (Exception err)
                        {
                            SqlType = SqlCommandType.sctSql;
                        }
                    }
                    if (ndSql.Attributes["fromsystype"] != null)
                    {
                        fromSysType = ndSql.Attributes["fromsystype"].Value.Trim();
                    }
                    if (ndSql.Attributes["datatablename"] != null)
                    {
                        dataTableName = ndSql.Attributes["datatablename"].Value.Trim();
                    }
                    if (ndSql.Attributes["myencoding"] != null)
                    {
                        sX  = ndSql.Attributes["myencoding"].Value.Trim();
                        //myEncoding = System.Text.Encoding.GetEncoding(sX.Trim());
                        myEncoding = System.Text.Encoding.UTF8;
                    }
                    if (ndSql.Attributes["datetimefields"] != null)
                    {
                        sX = ndSql.Attributes["datetimefields"].Value.Trim();
                        try
                        {
                            fldsDate = sX;
                        }
                        catch (Exception err)
                        {
                            fldsDate = "";
                        }
                    }
                }
                //获取参数                
                ndList = xmldoc.SelectNodes("sql/param");
                if (ndList != null)
                {
                    if (parameters == null)
                    {
                        parameters = new ZqmParameterCollection();
                    }
                    foreach (System.Xml.XmlNode nd in ndList)
                    {
                        ZqmParamter par = new ZqmParamter();
                        par.ParameterName = nd.Attributes["name"].Value.Trim();
                        string sXX = nd.Attributes["type"].Value.Trim().ToLower();
                        par.DataType = GetDataTypeByName(sXX);
                        par.ParameterValue = nd.Attributes["value"].Value.Trim();
                        par.ParameterDir = ZqmParameterDirction.Input;
                        parameters.Add(par);
                    }
                    
                    bOK = true;
                    if (xmldoc != null)
                    {
                        xmldoc.RemoveAll();
                    }
                    xmldoc = null;                
                }
                return bOK;
            }

            public string GetFullSql()
            {
                string sSql = this.sqlText;
                if (Parameters != null && Parameters.Count > 0)
                {
                    //sArr = cmdInfo.SqlText.Split(new char[] { ' ',':'});
                    //string sSqlText = cmdInfo.SqlText.ToLower();
                    foreach (ZqmParamter para in Parameters)
                    {
                        string spara = ":" + para.ParameterName.Trim();
                        string sValue = para.ParameterValue;
                        switch (para.DataType)
                        {
                            case ZqmDataType.String:
                                sValue = "\'" + sValue + "\'";
                                break;
                            case ZqmDataType.DateTime:
                                sValue = "\'" + sValue + "\'";
                                break;
                            //case ZqmDataType.Bool :
                            //    if()
                            //    break;
                            default:
                                sValue = sValue.Trim();
                                break;
                        }
                        sSql = sSql.Replace(spara, sValue);
                        //sb.Append(cmdInfo.SqlText.ToLower().Replace(spara,sValue));
                    }
                }
                return sSql;
            }

            /// <summary>
            /// 根据DataRow 来自动获取 insert / update  Sql 语句
            /// </summary>
            /// <param name="drX"></param>
            /// <param name="sTable"></param>
            /// <param name="sKeyFld"></param>
            /// <param name="bIsNew"></param>
            /// <returns></returns>
            public static string GetSQLByDataRow(DataRowView drX, string sTable, string sKeyFlds, bool bIsNew)
            {
                StringBuilder sSql = new StringBuilder("");
                string sFld = "";
                string sX = "";
                object objX = null;
                StringBuilder sSqlValue = new StringBuilder("");
                string strResult = "";
                if (bIsNew)
                {
                    sSql.Append("Insert into " + sTable + "(");
                    foreach (DataColumn col in drX.Row.Table.Columns)
                    {
                        sFld = col.ColumnName;
                        if ((!col.AutoIncrement) && (col.Expression == ""))
                        {
                            objX = drX[sFld];
                            if (objX !=  null)
                            {

                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                        }
                                        break;
                                    case "int32":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append(drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "decimal":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append(drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append(drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    default: 
                                        sSql.Append(sFld + ",");
                                        sX = drX[sFld].ToString();
                                        sX = sX.Replace("'", "''");
                                        sSqlValue.Append("'" + sX + "',");
                                        break;
                                }
                            }
                        }

                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    sSqlValue.Remove(sSqlValue.Length - 1, 1); //去掉最后一个,号
                    strResult = sSql.ToString() + ") values(" + sSqlValue.ToString() + ")";
                }
                else
                {
                    sSql.Append("Update " + sTable + " set ");
                    foreach (DataColumn col in drX.Row.Table.Columns)
                    {
                        sFld = col.ColumnName;
                        if ((!col.AutoIncrement) && (col.Expression == ""))
                        {
                            objX = drX[sFld];
                            if (objX != null)
                            {
                                //sSql.Append(sFld + ",");
                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                        }
                                        break;
                                    case "int32":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "decimal":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    default:
                                        sSql.Append(sFld + "=" + "'" + drX[sFld].ToString().Replace("'", "''") + "',");
                                        break;
                                }
                            }
                        }

                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    //获取条件语句
                    string[] strArr = sKeyFlds.Split(new char[] { ',' });
                    if (strArr.Length > 0)
                    {
                        sSql.Append(" where 1=1 ");
                        foreach (string sF in strArr)
                        {
                            //strResult = sSql.ToString() + " where " + sKeyFld + "=" ;
                            switch (drX.Row.Table.Columns[sF].DataType.ToString())
                            {
                                //case "bool":
                                //    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                //    break;
                                case "int32":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "double":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "int64":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                default:
                                    sSql.Append(" and " + sF + "=" + "'" + drX[sF].ToString() + "'");
                                    break;
                            }
                        }
                    }
                    strResult = sSql.ToString();
                }
                return (strResult);
            }
            public static string GetSQLByDataRow(DataRow drX, string sTable, string sKeyFlds, bool bIsNew)
            {
                StringBuilder sSql = new StringBuilder("");
                string sFld = "";
                string sX = "";
                object objX = null;
                StringBuilder sSqlValue = new StringBuilder("");
                string strResult = "";
                if (bIsNew)
                {
                    sSql.Append("Insert into " + sTable + "(");
                    foreach (DataColumn col in drX.Table.Columns)
                    {
                        sFld = col.ColumnName;
                        if ((!col.AutoIncrement) && (col.Expression == ""))
                        {
                            objX = drX[sFld];
                            if (objX != null)
                            {

                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                        }
                                        break;
                                    case "int32":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append(drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "decimal":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append(drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + ",");
                                            sSqlValue.Append(drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    default:
                                        sSql.Append(sFld + ",");
                                        sX = drX[sFld].ToString();
                                        sX = sX.Replace("'", "''");
                                        sSqlValue.Append("'" + sX + "',");
                                        break;
                                }
                            }
                        }

                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    sSqlValue.Remove(sSqlValue.Length - 1, 1); //去掉最后一个,号
                    strResult = sSql.ToString() + ") values(" + sSqlValue.ToString() + ")";
                }
                else
                {
                    sSql.Append("Update " + sTable + " set ");
                    foreach (DataColumn col in drX.Table.Columns)
                    {
                        sFld = col.ColumnName;
                        if ((!col.AutoIncrement) && (col.Expression == ""))
                        {
                            objX = drX[sFld];
                            if (objX != null)
                            {
                                //sSql.Append(sFld + ",");
                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                        }
                                        break;
                                    case "int32":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "decimal":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        if (objX.ToString().Trim() != "")
                                        {
                                            sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        }
                                        break;
                                    default:
                                        sSql.Append(sFld + "=" + "'" + drX[sFld].ToString().Replace("'", "''") + "',");
                                        break;
                                }
                            }
                        }

                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    //获取条件语句
                    string[] strArr = sKeyFlds.Split(new char[] { ',' });
                    if (strArr.Length > 0)
                    {
                        sSql.Append(" where 1=1 ");
                        foreach (string sF in strArr)
                        {
                            //strResult = sSql.ToString() + " where " + sKeyFld + "=" ;
                            switch (drX.Table.Columns[sF].DataType.ToString())
                            {
                                //case "bool":
                                //    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                //    break;
                                case "int32":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "double":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "int64":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                default:
                                    sSql.Append(" and " + sF + "=" + "'" + drX[sF].ToString() + "'");
                                    break;
                            }
                        }
                    }
                    strResult = sSql.ToString();
                }
                return (strResult);
            }
            public static string GetSQLByDataRow(DataRowView drX, string sTable, string sKeyFlds, string sNoFlds, bool bIsNew)
            {
                StringBuilder sSql = new StringBuilder("");
                string sFld = "";
                string sX = "";
                object objX = null;
                bool bX = false;
                string[] ArrNoFlds = sNoFlds.Split(new char[] { ',' });
                bool bNoFlds = ArrNoFlds != null;
                if (bNoFlds)
                {
                    bNoFlds = ArrNoFlds.Length > 0;
                }
                StringBuilder sSqlValue = new StringBuilder("");
                string strResult = "";
                if (bIsNew)
                {
                    sSql.Append("Insert into " + sTable + "(");
                    foreach (DataColumn col in drX.Row.Table.Columns)
                    {
                        bX = false;
                        sFld = col.ColumnName;
                        if (bNoFlds)
                        {
                            for (int nX = 0; nX < ArrNoFlds.Length; nX++)
                            {
                                bX = sFld.Trim().ToUpper() == ArrNoFlds[nX].Trim().ToUpper();
                                if (bX) break;
                            }
                        }
                        if (!bX)
                        {
                            if ((!col.AutoIncrement) && (col.Expression == ""))
                            {
                                objX = drX[sFld];
                                if (objX != null)
                                {

                                    sX = col.DataType.Name.ToLower();
                                    switch (sX)
                                    {
                                        case "boolean":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                            }
                                            break;
                                        case "int32":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append(drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "decimal":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append(drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "byte[]":
                                            break;
                                        case "int64":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append(drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        default:
                                            sSql.Append(sFld + ",");
                                            sX = drX[sFld].ToString();
                                            sX = sX.Replace("'", "''");
                                            sSqlValue.Append("'" + sX + "',");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    sSqlValue.Remove(sSqlValue.Length - 1, 1); //去掉最后一个,号
                    strResult = sSql.ToString() + ") values(" + sSqlValue.ToString() + ")";
                }
                else
                {
                    sSql.Append("Update " + sTable + " set ");
                    foreach (DataColumn col in drX.Row.Table.Columns)
                    {
                        bX = false;
                        sFld = col.ColumnName;
                        if (bNoFlds)
                        {
                            for (int nX = 0; nX < ArrNoFlds.Length; nX++)
                            {
                                bX = sFld.Trim().ToUpper() == ArrNoFlds[nX].Trim().ToUpper();
                                if (bX) break;
                            }
                        }
                        if (!bX)
                        {
                            if ((!col.AutoIncrement) && (col.Expression == ""))
                            {
                                objX = drX[sFld];
                                if (objX != null)
                                {
                                    //sSql.Append(sFld + ",");
                                    sX = col.DataType.Name.ToLower();
                                    switch (sX)
                                    {
                                        case "boolean":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                            }
                                            break;
                                        case "int32":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "decimal":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "byte[]":
                                            break;
                                        case "int64":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        default:
                                            sSql.Append(sFld + "=" + "'" + drX[sFld].ToString().Replace("'","''") + "',");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    //获取条件语句
                    string[] strArr = sKeyFlds.Split(new char[] { ',' });
                    if (strArr.Length > 0)
                    {
                        sSql.Append(" where 1=1 ");
                        foreach (string sF in strArr)
                        {
                            //strResult = sSql.ToString() + " where " + sKeyFld + "=" ;
                            switch (drX.Row.Table.Columns[sF].DataType.ToString())
                            {
                                //case "bool":
                                //    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                //    break;
                                case "int32":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "double":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "int64":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                default:
                                    sSql.Append(" and " + sF + "=" + "'" + drX[sF].ToString() + "'");
                                    break;
                            }
                        }
                    }
                    strResult = sSql.ToString();
                }
                return (strResult);
            }
            public static string GetSQLByDataRow(DataRow drX, string sTable, string sKeyFlds, string sNoFlds, bool bIsNew)
            {
                StringBuilder sSql = new StringBuilder("");
                string sFld = "";
                string sX = "";
                object objX = null;
                bool bX = false; //存在非表字段
                string[] ArrNoFlds = sNoFlds.Split(new char[] { ',' });
                bool bNoFlds = ArrNoFlds != null;
                if (bNoFlds)
                {
                    bNoFlds = ArrNoFlds.Length > 0;
                }
                StringBuilder sSqlValue = new StringBuilder("");
                string strResult = "";
                if (bIsNew)
                {
                    sSql.Append("Insert into " + sTable + "(");
                    foreach (DataColumn col in drX.Table.Columns)
                    {
                        bX = false;
                        sFld = col.ColumnName;
                        if (bNoFlds)
                        {
                            for (int nX = 0; nX < ArrNoFlds.Length; nX++)
                            {
                                bX = sFld.Trim().ToUpper() == ArrNoFlds[nX].Trim().ToUpper();
                                if (bX) break;
                            }
                        }
                        if (!bX)
                        {
                            if ((!col.AutoIncrement) && (col.Expression == ""))
                            {
                                objX = drX[sFld];
                                if (objX != null)
                                {

                                    sX = col.DataType.Name.ToLower();
                                    switch (sX)
                                    {
                                        case "boolean":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                            }
                                            break;
                                        case "int32":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append(drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "decimal":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append(drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "byte[]":
                                            break;
                                        case "int64":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + ",");
                                                sSqlValue.Append(drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        default:
                                            sSql.Append(sFld + ",");
                                            sX = drX[sFld].ToString();
                                            sX = sX.Replace("'", "''");
                                            sSqlValue.Append("'" + sX + "',");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    sSqlValue.Remove(sSqlValue.Length - 1, 1); //去掉最后一个,号
                    strResult = sSql.ToString() + ") values(" + sSqlValue.ToString() + ")";
                }
                else
                {
                    sSql.Append("Update " + sTable + " set ");
                    foreach (DataColumn col in drX.Table.Columns)
                    {
                        bX = false;
                        sFld = col.ColumnName;
                        if (bNoFlds)
                        {
                            for (int nX = 0; nX < ArrNoFlds.Length; nX++)
                            {
                                bX = sFld.Trim().ToUpper() == ArrNoFlds[nX].Trim().ToUpper();
                                if (bX) break;
                            }
                        }
                        if (!bX)
                        {
                            if ((!col.AutoIncrement) && (col.Expression == ""))
                            {
                                objX = drX[sFld];
                                if (objX != null)
                                {
                                    //sSql.Append(sFld + ",");
                                    sX = col.DataType.Name.ToLower();
                                    switch (sX)
                                    {
                                        case "boolean":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                            }
                                            break;
                                        case "int32":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "decimal":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        case "byte[]":
                                            break;
                                        case "int64":
                                            if (objX.ToString().Trim() != "")
                                            {
                                                sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                            }
                                            break;
                                        default:
                                            sSql.Append(sFld + "=" + "'" + drX[sFld].ToString().Replace("'", "''") + "',");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    sSql.Remove(sSql.Length - 1, 1); //去掉最后一个,号
                    //获取条件语句
                    string[] strArr = sKeyFlds.Split(new char[] { ',' });
                    if (strArr.Length > 0)
                    {
                        sSql.Append(" where 1=1 ");
                        foreach (string sF in strArr)
                        {
                            //strResult = sSql.ToString() + " where " + sKeyFld + "=" ;
                            switch (drX.Table.Columns[sF].DataType.ToString())
                            {
                                //case "bool":
                                //    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                //    break;
                                case "int32":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "double":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                case "int64":
                                    sSql.Append(" and " + sF + "=" + drX[sF].ToString());
                                    break;
                                default:
                                    sSql.Append(" and " + sF + "=" + "'" + drX[sF].ToString() + "'");
                                    break;
                            }
                        }
                    }
                    strResult = sSql.ToString();
                }
                return (strResult);
            }
            public static string GetFieldsForDate(DataRow drX)
            {
                string sResutl = "";
                if (drX != null)
                {                   
                    foreach (DataColumn col in drX.Table.Columns)
                    {
                        if(col.DataType  == Type.GetType("System.DateTime"))
                        {
                            if (sResutl.Length == 0)
                            {
                                sResutl = col.ColumnName;
                            }
                            else
                            {
                                sResutl += ("," + col.ColumnName);
                            }
                        }
                    }
                }
                return (sResutl.Trim());
            }
            public static string GetFieldsForDate(DataRowView drX)
            {
                string sResutl = "";
                if (drX != null)
                {
                    foreach (DataColumn col in drX.Row.Table.Columns)
                    {
                        if (col.DataType == Type.GetType("System.DateTime"))
                        {
                            if (sResutl.Length == 0)
                            {
                                sResutl = col.ColumnName;
                            }
                            else
                            {
                                sResutl += ("," + col.ColumnName);
                            }
                        }
                    }
                }
                return (sResutl.Trim());
            }
        #endregion
    }
    
}
