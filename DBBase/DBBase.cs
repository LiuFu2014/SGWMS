using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Text;
using CommBase;
using DBCommInfo;

namespace DBBase
{
    /// <summary>
    /// ���ݿ�����
    /// </summary>
    //public enum DataBaseType {dbtMSSQL=0,dbtMSAccess=1,dbtOracle=2,dbtMySQL=3,dbtDBTwo=4,dbtParadox=5,dbtExcel=6};
    /// <summary>
    
    /// <summary>
    /// �����ݿ�Ļ�������
    /// </summary>
    public  class DBBase
    {
        public const string ConnStrToMSSQLBySSPI = "server={0};Persist Security Info=false;Integrated Security=SSPI;Initial Catalog={1}";
        public const string ConnStrToMSSQLByUP = "Data Source={0};Persist Security Info=true;Initial Catalog={1};User Id={2};PassWord={3}";
        //Provider=SQLOLEDB.1;Password=123;Persist Security Info=True;User ID=SA;Initial Catalog=AutoWarehouse;Data Source=PC-201107230103\SQL2005
        public const string ConnStrToMSSQLForOleDb = "Provider=SQLOLEDB.1;Data Source={0};Persist Security Info=true;Initial Catalog={1};User Id={2};PassWord={3}";
        //Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AutoWarehouse;Data Source=PC-201107230103\SQL2005
        public const string ConnStrToMSSQLForOleDbBySSPI = "Provider=SQLOLEDB.1;Data Source={0};Integrated Security=SSPI;Persist Security Info=False;Initial Catalog={1}";
        //
        public const string ConnStrToMSAccess = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Innfo=true;User Id={1};Jet OLEDB:Database Password={2}";
        //const string ConnStrToOracle = "Provider=MSDAORA; Data Source=ORACLE8i7;Persist Security Info=False;Integrated Security=Yes";
        public const string ConnStrToOracleBySSPI = "Data Source={0};Persist Security Info=False;Integrated Security=Yes";
        public const string ConnStrToOracleByUP = "Data Source={0};Persist Security Info=true;User Id={1};PassWord={2}";
        public const string ConnStrToOracleForOleDb = "Provider=MSDAORA;Data Source={0};Persist Security Info=true;User Id={1};PassWord={2}";
        public const string ConnStrToMySQL = "";
        public const string ConnStrToDBTwo = "";
        public const string ConnStrotoParadox = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Paradox 7.X;";
        public const string ConnStrToExcel = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;HDR=Yes;IMEX=1";
        
        /// <summary>
        /// �������ݿ����ͣ���ȡ�����ַ���
        /// </summary>
        /// <param name="dbtType"></param>
        /// <param name="sSvr"></param>
        /// <returns></returns>
        public static string GetConnectionString(DataBaseType dbtType, string sSvr)
        {
            string sConn = string.Empty ;
            switch (dbtType)
            {
                case DataBaseType.dbtMSSQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtMSAccess:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtMySQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtOracle:
                    sConn = String.Format(ConnStrToOracleBySSPI, sSvr.Trim());
                    break;
                case DataBaseType.dbtParadox:
                    sConn = String.Format(ConnStrotoParadox, sSvr.Trim());
                    break;
                case DataBaseType.dbtExcel:
                    sConn = String.Format(ConnStrToExcel, sSvr.Trim());
                    break;
                case DataBaseType.dbtDBTwo:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
            }
            return (sConn);
        }
        
        /// <summary>
        ///  �������ݿ����ͣ���ȡ�����ַ���
        /// </summary>
        /// <param name="dbtType"></param>
        /// <param name="sSvr"></param>
        /// <param name="sDB"></param>
        /// <returns></returns>
        public static string GetConnectionString(DataBaseType dbtType, string sSvr,string sDB)
        {
            string sConn = string.Empty;
            switch (dbtType)
            {
                case DataBaseType.dbtMSSQL:
                    sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtMSAccess:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtMySQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtOracle:
                    sConn = String.Format(ConnStrToOracleBySSPI, sSvr.Trim());
                    break;
                case DataBaseType.dbtParadox:
                    sConn = String.Format(ConnStrotoParadox, sSvr.Trim());
                    break;
                case DataBaseType.dbtExcel:
                    sConn = String.Format(ConnStrToExcel, sSvr.Trim());
                    break;
                case DataBaseType.dbtDBTwo:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
            }
            return (sConn);
        }

        public static string GetConnectionString(DataBaseType dbtType, string sSvr, string sDB,bool bIsOleDbConn)
        {
            string sConn = string.Empty;
            switch (dbtType)
            {
                case DataBaseType.dbtMSSQL:
                    if (!bIsOleDbConn)
                    {
                        sConn = String.Format(ConnStrToMSSQLBySSPI, sSvr.Trim(), sDB.Trim());
                    }
                    else
                    {
                        sConn = String.Format(ConnStrToMSSQLForOleDbBySSPI, sSvr.Trim(), sDB.Trim());
                    }
                    break;
                case DataBaseType.dbtMSAccess:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtMySQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtOracle:
                    sConn = String.Format(ConnStrToOracleBySSPI, sSvr.Trim());
                    break;
                case DataBaseType.dbtParadox:
                    sConn = String.Format(ConnStrotoParadox, sSvr.Trim());
                    break;
                case DataBaseType.dbtExcel:
                    sConn = String.Format(ConnStrToExcel, sSvr.Trim());
                    break;
                case DataBaseType.dbtDBTwo:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
            }
            return (sConn);
        }
        
        /// <summary>
        ///  �������ݿ����ͣ���ȡ�����ַ���
        /// </summary>
        /// <param name="dbtType"></param>
        /// <param name="sSvr"></param>
        /// <param name="sUser"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public static string GetConnectionString(DataBaseType dbtType, string sSvr, string sUser, string sPwd, bool bIsOleDbConn)
        {
            string sConn = string.Empty;
            switch (dbtType)
            {
                case DataBaseType.dbtMSSQL:
                    //sConn = String.Format(ConnStrToMSSQLByUP, sSvr.Trim(), sDB.Trim(),sUser.Trim(),sPwd);
                    break;
                case DataBaseType.dbtMSAccess:
                    sConn = String.Format(ConnStrToMSAccess, sSvr.Trim(), sUser.Trim(),sPwd);
                    break;
                case DataBaseType.dbtMySQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtOracle:
                    if (!bIsOleDbConn)
                    {
                        sConn = String.Format(ConnStrToOracleByUP, sSvr.Trim(), sUser.Trim(), sPwd);
                    }
                    else
                    {
                        sConn = String.Format(ConnStrToOracleForOleDb, sSvr.Trim(), sUser.Trim(), sPwd);
                    }
                    break;
                case DataBaseType.dbtParadox:
                    sConn = String.Format(ConnStrotoParadox, sSvr.Trim());
                    break;
                case DataBaseType.dbtExcel:
                    sConn = String.Format(ConnStrToExcel, sSvr.Trim());
                    break;
                case DataBaseType.dbtDBTwo:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
            }
            return (sConn);
        }

        public static string GetConnectionString(DataBaseType dbtType, string sSvr, string sUser, string sPwd)
        {
            string sConn = string.Empty;
            sConn = GetConnectionString(dbtType, sSvr, sUser, false);
            return (sConn);
        }
        /// <summary>
        ///  �������ݿ����ͣ���ȡ�����ַ���
        /// </summary>
        /// <param name="dbtType"></param>
        /// <param name="sSvr"></param>
        /// <param name="sDB"></param>
        /// <param name="sUser"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public static string GetConnectionString(DataBaseType dbtType, string sSvr, string sDB,string sUser, string sPwd)
        {
            string sConn = string.Empty;
            switch (dbtType)
            {
                case DataBaseType.dbtMSSQL:
                    sConn = String.Format(ConnStrToMSSQLByUP, sSvr.Trim(), sDB.Trim(),sUser.Trim(),sPwd);
                    break;
                case DataBaseType.dbtMSAccess:
                    sConn = String.Format(ConnStrToMSAccess, sSvr.Trim(), sUser.Trim(), sPwd);
                    break;
                case DataBaseType.dbtMySQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtOracle:
                    sConn = String.Format(ConnStrToOracleByUP, sSvr.Trim(), sUser.Trim(), sPwd);
                    break;
                case DataBaseType.dbtParadox:
                    sConn = String.Format(ConnStrotoParadox, sSvr.Trim());
                    break;
                case DataBaseType.dbtExcel:
                    sConn = String.Format(ConnStrToExcel, sSvr.Trim());
                    break;
                case DataBaseType.dbtDBTwo:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
            }
            return (sConn);
        }

        public static string GetConnectionString(DataBaseType dbtType, string sSvr, string sDB, string sUser, string sPwd,bool bIsOleDbObj)
        {
            string sConn = string.Empty;
            switch (dbtType)
            {
                case DataBaseType.dbtMSSQL:
                    if (bIsOleDbObj)
                    {
                        sConn = String.Format(ConnStrToMSSQLForOleDb, sSvr.Trim(), sDB.Trim(), sUser.Trim(), sPwd);
                    }
                    else
                    {
                        sConn = String.Format(ConnStrToMSSQLByUP, sSvr.Trim(), sDB.Trim(), sUser.Trim(), sPwd);
                    }
                    break;
                case DataBaseType.dbtMSAccess:
                    sConn = String.Format(ConnStrToMSAccess, sSvr.Trim(), sUser.Trim(), sPwd);
                    break;
                case DataBaseType.dbtMySQL:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
                case DataBaseType.dbtOracle:
                    if (bIsOleDbObj)
                    {                      
                        sConn = String.Format(ConnStrToOracleForOleDb, sSvr.Trim(), sUser.Trim(), sPwd);
                    }
                    else
                    {
                        sConn = String.Format(ConnStrToOracleByUP, sSvr.Trim(), sUser.Trim(), sPwd);
                    }                    
                    break;
                case DataBaseType.dbtParadox:
                    sConn = String.Format(ConnStrotoParadox, sSvr.Trim());
                    break;
                case DataBaseType.dbtExcel:
                    sConn = String.Format(ConnStrToExcel, sSvr.Trim());
                    break;
                case DataBaseType.dbtDBTwo:
                    //sConn = String.Format(ConnStrToMSSQLBySSPI,sSvr.Trim(),sDB.Trim());
                    break;
            }
            return (sConn);
        }

        /// <summary>
        /// ����DataRow ���Զ���ȡ insert / update  Sql ���
        /// </summary>
        /// <param name="drX"></param>
        /// <param name="sTable"></param>
        /// <param name="sKeyFld"></param>
        /// <param name="bIsNew"></param>
        /// <returns></returns>
        public static string GetSQLByDataRow(DataRowView drX, string sTable, string sKeyFld, bool bIsNew)
        {
            StringBuilder sSql = new StringBuilder("");
            string sFld = "";
            string sX = "";
            object objX = null;
            StringBuilder sSqlValue = new StringBuilder("");
            string strResult = "";
            if (bIsNew)
            {
                sSql.Append("Insert into " + sTable +"(");                
                foreach(DataColumn col in drX.Row.Table.Columns)
                {
                    sFld = col.ColumnName;
                    if (!col.AutoIncrement)
                    {
                        objX = drX[sFld] ;
                        if (objX.ToString().ToLower() != "")
                        {
                            
                            sX = col.DataType.Name.ToLower();
                            switch (sX )
                            {
                                case "boolean":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString()+"," );
                                    break;
                                case "int32":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append(drX[sFld].ToString() + ",");
                                    break;
                                case "decimal":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append(drX[sFld].ToString() + ",");
                                    break;
                                case "byte[]" :
                                    break;
                                case "int64":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append(drX[sFld].ToString() + ",");
                                    break;
                                default:
                                    sSql.Append(sFld + ",");
                                    sX = drX[sFld].ToString();
                                    sX = sX.Replace("'", "''");
                                    sSqlValue.Append("'"+sX + "',");
                                    break;
                            }
                        }
                    }
                    
                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                sSqlValue.Remove(sSqlValue.Length - 1, 1); //ȥ�����һ��,��
                strResult = sSql.ToString() + ") values(" + sSqlValue.ToString() + ")";
            }
            else
            {
                sSql.Append("Update " + sTable + " set ");
                foreach (DataColumn col in drX.Row.Table.Columns)
                {
                    sFld = col.ColumnName;
                    if (!col.AutoIncrement)
                    {
                        objX = drX[sFld] ;
                        if (objX.ToString().ToLower() != "")                        
                        {
                            //sSql.Append(sFld + ",");
                            sX = col.DataType.Name.ToLower();
                            switch (sX)
                            {
                                case "boolean":
                                    sSql.Append( sFld + "=" +  Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                    break;
                                case "int32":
                                    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                    break;
                                case "decimal":
                                    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                    break;
                                case "byte[]":
                                    break;
                                case "int64":
                                    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                    break;
                                default:
                                    sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                    break;
                            }
                        }
                    }

                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                //��ȡ�������
                string[] strArr = sKeyFld.Split(new char[] { ',' });
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
                                sSql.Append(" and " + sF  + "=" + drX[sF].ToString());
                                break;
                            case "int64":
                                sSql.Append(" and " + sF  + "=" + drX[sF].ToString());
                                break;
                            default:
                                sSql.Append(" and " + sF  + "=" + "'" + drX[sF].ToString() + "'");
                                break;
                        }
                    }
                }
                strResult = sSql.ToString();
            }
            return (strResult);
        }
        public static string GetSQLByDataRow(DataRow drX, string sTable, string sKeyFld,bool bIsNew)
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
                    if (!col.AutoIncrement)
                    {
                        objX = drX[sFld];
                        if (objX.ToString().ToLower() != "")
                        {

                            sX = col.DataType.Name.ToLower();
                            switch (sX)
                            {
                                case "boolean":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                    break;
                                case "int32":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append(drX[sFld].ToString() + ",");
                                    break;
                                case "decimal":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append(drX[sFld].ToString() + ",");
                                    break;
                                case "byte[]":
                                    break;
                                case "int64":
                                    sSql.Append(sFld + ",");
                                    sSqlValue.Append(drX[sFld].ToString() + ",");
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
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                sSqlValue.Remove(sSqlValue.Length - 1, 1); //ȥ�����һ��,��
                strResult = sSql.ToString() + ") values(" + sSqlValue.ToString() + ")";
            }
            else
            {
                sSql.Append("Update " + sTable + " set ");
                foreach (DataColumn col in drX.Table.Columns)
                {
                    sFld = col.ColumnName;
                    if (!col.AutoIncrement)
                    {
                        objX = drX[sFld];
                        if (objX.ToString().ToLower() != "")
                        {
                            //sSql.Append(sFld + ",");
                            sX = col.DataType.Name.ToLower();
                            switch (sX)
                            {
                                case "boolean":
                                    sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                    break;
                                case "int32":
                                    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                    break;
                                case "decimal":
                                    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                    break;
                                case "byte[]":
                                    break;
                                case "int64":
                                    sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                    break;
                                default:
                                    sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                    break;
                            }
                        }
                    }

                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                //��ȡ�������
                string[] strArr = sKeyFld.Split(new char[] { ',' });
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
        public static string GetSQLByDataRow(DataRowView drX, string sTable, string sKeyFld, string sNoFlds, bool bIsNew)
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {

                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
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
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                sSqlValue.Remove(sSqlValue.Length - 1, 1); //ȥ�����һ��,��
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {
                                //sSql.Append(sFld + ",");
                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    default:
                                        sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                        break;
                                }
                            }
                        }
                    }
                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                //��ȡ�������
                string[] strArr = sKeyFld.Split(new char[] { ',' });
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
        public static string GetSQLByDataRow(DataRow drX, string sTable, string sKeyFld, string sNoFlds, bool bIsNew)
        {
            StringBuilder sSql = new StringBuilder("");
            string sFld = "";
            string sX = "";
            object objX = null;
            bool bX = false; //���ڷǱ��ֶ�
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {

                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
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
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                sSqlValue.Remove(sSqlValue.Length - 1, 1); //ȥ�����һ��,��
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {
                                //sSql.Append(sFld + ",");
                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    default:
                                        sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                        break;
                                }
                            }
                        }
                    }
                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                //��ȡ�������
                string[] strArr = sKeyFld.Split(new char[] { ',' });
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

        public static string GetSQLByDataRow(DataRowView drX, string sTable, string sKeyFld, string sNoFlds, bool bIsNew,int nDbType)
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {

                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "datetime":
                                        if ((DataBaseType)nDbType == DataBaseType.dbtOracle)
                                        {
                                            sSql.Append(sFld + ",");
                                            sX = drX[sFld].ToString();
                                            sX = sX.Replace("'", "''");
                                            //"to_date('" + values[ifld].Trim() + "','yyyy-MM-dd hh24;mi:ss')"
                                            sSqlValue.Append("to_date('" + sX + "','yyyy-MM-dd hh24;mi:ss'),");
                                        }
                                        else
                                        {
                                            sSql.Append(sFld + ",");
                                            sX = drX[sFld].ToString();
                                            sX = sX.Replace("'", "''");
                                            sSqlValue.Append("'" + sX + "',");
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
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                sSqlValue.Remove(sSqlValue.Length - 1, 1); //ȥ�����һ��,��
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {
                                //sSql.Append(sFld + ",");
                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "datetime":
                                        if ((DataBaseType)nDbType == DataBaseType.dbtOracle)
                                        {
                                            sSql.Append(sFld + "=to_date('" + drX[sFld].ToString() + "','yyyy-MM-dd hh24;mi:ss'),");
                                            //"to_date('" + values[ifld].Trim() + "','yyyy-MM-dd hh24;mi:ss')"
                                        }
                                        else
                                        {
                                            sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                        }
                                        break;
                                    default:
                                        sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                        break;
                                }
                            }
                        }
                    }
                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                //��ȡ�������
                string[] strArr = sKeyFld.Split(new char[] { ',' });
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
                            case "datetime":
                                if ((DataBaseType)nDbType == DataBaseType.dbtOracle)
                                {
                                    sSql.Append(" and " + sFld + "=to_date('" + drX[sF].ToString() + "','yyyy-MM-dd hh24;mi:ss')");
                                    //"to_date('" + values[ifld].Trim() + "','yyyy-MM-dd hh24;mi:ss')"
                                }
                                else
                                {
                                    sSql.Append(" and " + sFld + "=" + "'" + drX[sF].ToString() + "'");
                                }
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
        public static string GetSQLByDataRow(DataRow drX, string sTable, string sKeyFld, string sNoFlds, bool bIsNew,int nDbType)
        {
            StringBuilder sSql = new StringBuilder("");
            string sFld = "";
            string sX = "";
            object objX = null;
            bool bX = false; //���ڷǱ��ֶ�
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {

                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append((Convert.ToInt16(bool.Parse(drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + ",");
                                        sSqlValue.Append(drX[sFld].ToString() + ",");
                                        break;
                                    case "datetime":
                                        if ((DataBaseType)nDbType == DataBaseType.dbtOracle)
                                        {
                                            sSql.Append(sFld + ",");
                                            sX = drX[sFld].ToString();
                                            sX = sX.Replace("'", "''");
                                            //"to_date('" + values[ifld].Trim() + "','yyyy-MM-dd hh24;mi:ss')"
                                            sSqlValue.Append("to_date('" + sX + "','yyyy-MM-dd hh24;mi:ss'),");
                                        }
                                        else
                                        {
                                            sSql.Append(sFld + ",");
                                            sX = drX[sFld].ToString();
                                            sX = sX.Replace("'", "''");
                                            sSqlValue.Append("'" + sX + "',");
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
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                sSqlValue.Remove(sSqlValue.Length - 1, 1); //ȥ�����һ��,��
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
                        if (!col.AutoIncrement)
                        {
                            objX = drX[sFld];
                            if (objX.ToString().ToLower() != "")
                            {
                                //sSql.Append(sFld + ",");
                                sX = col.DataType.Name.ToLower();
                                switch (sX)
                                {
                                    case "boolean":
                                        sSql.Append(sFld + "=" + Convert.ToInt16(bool.Parse((drX[sFld].ToString()))).ToString() + ",");
                                        break;
                                    case "int32":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "decimal":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "byte[]":
                                        break;
                                    case "int64":
                                        sSql.Append(sFld + "=" + drX[sFld].ToString() + ",");
                                        break;
                                    case "datetime":
                                        if ((DataBaseType)nDbType == DataBaseType.dbtOracle)
                                        {
                                            sSql.Append(sFld + "=to_date('" + drX[sFld].ToString() + "','yyyy-MM-dd hh24;mi:ss'),");
                                            //"to_date('" + values[ifld].Trim() + "','yyyy-MM-dd hh24;mi:ss')"
                                        }
                                        else
                                        {
                                            sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                        }
                                        break;
                                    default:
                                        sSql.Append(sFld + "=" + "'" + drX[sFld].ToString() + "',");
                                        break;
                                }
                            }
                        }
                    }
                }
                sSql.Remove(sSql.Length - 1, 1); //ȥ�����һ��,��
                //��ȡ�������
                string[] strArr = sKeyFld.Split(new char[] { ',' });
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
                            case "datetime":
                                if ((DataBaseType)nDbType == DataBaseType.dbtOracle)
                                {
                                    sSql.Append(" and " + sFld + "=to_date('" + drX[sF].ToString() + "','yyyy-MM-dd hh24;mi:ss')");
                                    //"to_date('" + values[ifld].Trim() + "','yyyy-MM-dd hh24;mi:ss')"
                                }
                                else
                                {
                                    sSql.Append(" and " + sFld + "=" + "'" + drX[sF].ToString() + "'");
                                }
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
    }

    public class DBOptrForMSSql
    {
        #region  ���Ӷ������
            public static string ConnOpenByConnStr(DbConnection objConn, string strCon)
            {
                string sRet = "-1";
                SqlConnection Conn = (SqlConnection)objConn;
                if (Conn.State == ConnectionState.Open) Conn.Close();
                Conn.ConnectionString = strCon;
                try
                {
                    Conn.Open();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            public static string ConnOpen(DbConnection objConn, string sSvr, string sDb)
            {
                string sRet = "-1";
                string strCon = "";
                strCon = DBBase.GetConnectionString(DataBaseType.dbtMSSQL, sSvr, sDb);
                sRet = ConnOpenByConnStr(objConn, strCon);
                return (sRet);
            }
            public static string ConnOpen(DbConnection objConn, string sSvr, string sDb, string sUser, string sPwd)
            {
                string sRet = "-1";
                string strCon = "";
                strCon = DBBase.GetConnectionString(DataBaseType.dbtMSSQL, sSvr, sDb, sUser, sPwd);
                sRet = ConnOpenByConnStr(objConn, strCon);
                return (sRet);
            }
        #endregion
        #region ִ��SQL���
            /// <summary>
            /// �������Ӷ���SQL��䣬���ؼ�¼���е�һ���������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="sTbName"></param>
            /// <param name="dsX"></param>
            /// <returns></returns>
            public static string OptOpenDataSet(DbConnection objConn, string sSql, string sTbName, DataSet dsX)
            {
                string sRet = "-1";
                if (objConn == null)
                {
                    sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                if (dsX == null)
                {
                    sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlDataAdapter dtpter = new SqlDataAdapter(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    dtpter.Fill(dsX, sTbName);
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����һDataReader�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="sTbName"></param>
            /// <param name="dtRder"></param>
            /// <returns></returns>
            public static string OptOpenDataReader(DbConnection objConn, string sSql, string sTbName, SqlDataReader dtRder)
            {
                string sRet = "-1";
                if (objConn == null)
                {
                    sRet = "OptOpenDataReader �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                if (dtRder == null)
                {
                    sRet = "OptOpenDataReader �������ó��� ���� dtRder ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    dtRder = cmd.ExecuteReader();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL�����ִ�е�Ӱ�������������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iLines"></param>
            /// <returns></returns>
            public static string OptExecRetLineCount(DbConnection objConn, string sSql,out Int64 iLines)
            {
                string sRet = "-1";
                iLines = -1;
                if (objConn == null)
                {
                    sRet = "OptExecRetLineCount �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    iLines = cmd.ExecuteNonQuery();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�н���ĵ�һ�еĵ�һ�е�OBJECTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="objResult"></param>
            /// <returns></returns>
            public static string OptExecRetObj(DbConnection objConn, string sSql,out object objResult)
            {
                object objX = null;
                string sRet = "-1";
                if (objConn == null)
                {
                    sRet = "OptExecRetObj �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    objResult = objX;
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objX = cmd.ExecuteScalar();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                objResult = objX;
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�INTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iResult"></param>
            /// <returns></returns>
            public static string OptExecRetInt(DbConnection objConn, string sSql, out Int64 iResult)
            {
                string sRet = "-1";
                iResult = 0;
                if (objConn == null)
                {                
                    sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                object objRet = null;
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objRet = cmd.ExecuteScalar();
                    if (objRet != null)
                    {
                        string sX = "";
                        sX = objRet.ToString();
                        if (sX.ToUpper() != "NULL")
                        {
                            iResult = Int64.Parse(sX);
                        }
                    }
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�doubleֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iResult"></param>
            /// <returns></returns>
            public static string OptExecRetDouble(DbConnection objConn, string sSql, out double iResult)
            {
                string sRet = "-1";
                iResult = 0;
                if (objConn == null)
                {
                    sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                object objRet = null;
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objRet = cmd.ExecuteScalar();
                    if (objRet != null)
                    {
                        string sX = "";
                        sX = objRet.ToString();
                        if (sX.ToUpper() != "NULL")
                        {
                            iResult = Double.Parse(sX);
                        }
                    }
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�boolֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iResult"></param>
            /// <returns></returns>
            public static string OptExecRetBool(DbConnection objConn, string sSql, out bool iResult)
            {
                string sRet = "-1";
                iResult = false;
                if (objConn == null)
                {
                    sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                object objRet = null;
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objRet = cmd.ExecuteScalar();
                    if (objRet != null)
                    {
                        string sX = "";
                        sX = objRet.ToString();
                        if (sX.ToUpper() != "NULL")
                        {
                            iResult = int.Parse(sX) > 0;
                        }
                    }
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }


            /// <summary>
            /// �������Ӷ���SQL��䣬����һDataReader�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="sTbName"></param>
            /// <param name="dtRder"></param>
            /// <returns></returns>
            public static string OptOpenDataReader(DbConnection objConn, SqlTransaction trn, string sSql, string sTbName, SqlDataReader dtRder)
            {
                string sRet = "-1";
                if (objConn == null)
                {
                    sRet = "OptOpenDataReader �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                if (dtRder == null)
                {
                    sRet = "OptOpenDataReader �������ó��� ���� dtRder ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                cmd.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    dtRder = cmd.ExecuteReader();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL�����ִ�е�Ӱ�������������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iLines"></param>
            /// <returns></returns>
            public static string OptExecRetLineCount(DbConnection objConn, SqlTransaction trn, string sSql, out Int64 iLines)
            {
                string sRet = "-1";
                iLines = -1;
                if (objConn == null)
                {
                    sRet = "OptExecRetLineCount �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                cmd.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    iLines = cmd.ExecuteNonQuery();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�н���ĵ�һ�еĵ�һ�е�OBJECTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="objResult"></param>
            /// <returns></returns>
            public static string OptExecRetObj(DbConnection objConn, SqlTransaction trn, string sSql, out object objResult)
            {
                object objX = null;
                string sRet = "-1";
                if (objConn == null)
                {
                    sRet = "OptExecRetObj �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    objResult = objX;
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                cmd.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objX = cmd.ExecuteScalar();
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                objResult = objX;
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�INTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iResult"></param>
            /// <returns></returns>
            public static string OptExecRetInt(DbConnection objConn, SqlTransaction trn, string sSql, out Int64 iResult)
            {
                string sRet = "-1";
                iResult = 0;
                if (objConn == null)
                {
                    sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                object objRet = null;
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                cmd.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objRet = cmd.ExecuteScalar();
                    if (objRet != null)
                    {
                        string sX = "";
                        sX = objRet.ToString();
                        if (sX.ToUpper() != "NULL")
                        {
                            iResult = Int64.Parse(sX);
                        }
                    }
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�doubleֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iResult"></param>
            /// <returns></returns>
            public static string OptExecRetDouble(DbConnection objConn, SqlTransaction trn, string sSql, out double iResult)
            {
                string sRet = "-1";
                iResult = 0;
                if (objConn == null)
                {
                    sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                object objRet = null;
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                cmd.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objRet = cmd.ExecuteScalar();
                    if (objRet != null)
                    {
                        string sX = "";
                        sX = objRet.ToString();
                        if (sX.ToUpper() != "NULL")
                        {
                            iResult = Double.Parse(sX);
                        }
                    }
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            /// <summary>
            /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�boolֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
            /// </summary>
            /// <param name="objConn"></param>
            /// <param name="sSql"></param>
            /// <param name="iResult"></param>
            /// <returns></returns>
            public static string OptExecRetBool(DbConnection objConn, SqlTransaction trn, string sSql, out bool iResult)
            {
                string sRet = "-1";
                iResult = false;
                if (objConn == null)
                {
                    sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                object objRet = null;
                SqlConnection Conn = (SqlConnection)objConn;
                SqlCommand cmd = new SqlCommand(sSql, Conn);
                cmd.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    cmd.CommandTimeout = 0;
                    objRet = cmd.ExecuteScalar();
                    if (objRet != null)
                    {
                        string sX = "";
                        sX = objRet.ToString();
                        if (sX.ToUpper() != "NULL")
                        {
                            iResult = int.Parse(sX) > 0;
                        }
                    }
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
            public static string OptOpenDataSet(DbConnection objConn,SqlTransaction trn, string sSql, string sTbName, DataSet dsX)
            {
                string sRet = "-1";
                if (objConn == null)
                {
                    sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                if (dsX == null)
                {
                    sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                    return (sRet);
                }
                SqlConnection Conn = (SqlConnection)objConn;
                SqlDataAdapter dtpter = new SqlDataAdapter(sSql, Conn);
                dtpter.SelectCommand.Transaction = trn;
                try
                {
                    if (Conn.State == ConnectionState.Closed) Conn.Open();
                    dtpter.Fill(dsX, sTbName);
                    sRet = "0";
                }
                catch (Exception ex)
                {
                    sRet = ex.Message;
                }
                return (sRet);
            }
        #endregion
    }
    public class DBOptrForOracle
    {
        #region  ���Ӷ������
        public static string ConnOpenByConnStr(DbConnection objConn, string strCon)
        {
            string sRet = "-1";
            OracleConnection Conn = (OracleConnection)objConn;
            if (Conn.State == ConnectionState.Open) Conn.Close();
            Conn.ConnectionString = strCon;
            try
            {
                Conn.Open();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        public static string ConnOpen(DbConnection objConn, string sSvr)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(DataBaseType.dbtOracle, sSvr);
            sRet = ConnOpenByConnStr(objConn, strCon);
            return (sRet);
        }
        public static string ConnOpen(DbConnection objConn, string sSvr, string sUser, string sPwd)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(DataBaseType.dbtOracle, sSvr, sUser, sPwd);
            sRet = ConnOpenByConnStr(objConn, strCon);
            return (sRet);
        }
        #endregion
        #region ִ��SQL���
        /// <summary>
        /// �������Ӷ���SQL��䣬���ؼ�¼���е�һ���������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="sTbName"></param>
        /// <param name="dsX"></param>
        /// <returns></returns>
        public static string OptOpenDataSet(DbConnection objConn, string sSql, string sTbName, DataSet dsX)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dsX == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleDataAdapter dtpter = new OracleDataAdapter(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                dtpter.Fill(dsX, sTbName);
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����һDataReader�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="sTbName"></param>
        /// <param name="dtRder"></param>
        /// <returns></returns>
        public static string OptOpenDataReader(DbConnection objConn, string sSql, string sTbName, OracleDataReader dtRder)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dtRder == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� dtRder ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                dtRder = cmd.ExecuteReader();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL�����ִ�е�Ӱ�������������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iLines"></param>
        /// <returns></returns>
        public static string OptExecRetLineCount(DbConnection objConn, string sSql, out Int64 iLines)
        {
            string sRet = "-1";
            iLines = -1;
            if (objConn == null)
            {
                sRet = "OptExecRetLineCount �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                iLines = cmd.ExecuteNonQuery();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�н���ĵ�һ�еĵ�һ�е�OBJECTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public static string OptExecRetObj(DbConnection objConn, string sSql, out object objResult)
        {
            object objX = null;
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptExecRetObj �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                objResult = null;
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objX = cmd.ExecuteScalar();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            objResult = objX;
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�INTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetInt(DbConnection objConn, string sSql, out Int64 iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = Int64.Parse(sX);
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�doubleֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetDouble(DbConnection objConn, string sSql, out double iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = Double.Parse(sX);
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�boolֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetBool(DbConnection objConn, string sSql, out bool iResult)
        {
            string sRet = "-1";
            iResult = false;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = int.Parse(sX) > 0;
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }


        public static string OptOpenDataReader(DbConnection objConn, OracleTransaction trn, string sSql, string sTbName, OracleDataReader dtRder)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dtRder == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� dtRder ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            cmd.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                dtRder = cmd.ExecuteReader();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL�����ִ�е�Ӱ�������������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iLines"></param>
        /// <returns></returns>
        public static string OptExecRetLineCount(DbConnection objConn, OracleTransaction trn, string sSql, out Int64 iLines)
        {
            string sRet = "-1";
            iLines = -1;
            if (objConn == null)
            {
                sRet = "OptExecRetLineCount �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            cmd.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                iLines = cmd.ExecuteNonQuery();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�н���ĵ�һ�еĵ�һ�е�OBJECTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public static string OptExecRetObj(DbConnection objConn, OracleTransaction trn, string sSql, out object objResult)
        {
            object objX = null;
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptExecRetObj �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                objResult = null;
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            cmd.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objX = cmd.ExecuteScalar();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            objResult = objX;
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�INTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetInt(DbConnection objConn, OracleTransaction trn, string sSql, out Int64 iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            cmd.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = Int64.Parse(sX);
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�doubleֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetDouble(DbConnection objConn, OracleTransaction trn, string sSql, out double iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            cmd.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = Double.Parse(sX);
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�boolֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetBool(DbConnection objConn, OracleTransaction trn, string sSql, out bool iResult)
        {
            string sRet = "-1";
            iResult = false;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OracleConnection Conn = (OracleConnection)objConn;
            OracleCommand cmd = new OracleCommand(sSql, Conn);
            cmd.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = int.Parse(sX) > 0;
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        public static string OptOpenDataSet(DbConnection objConn, OracleTransaction trn, string sSql, string sTbName, DataSet dsX)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dsX == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OracleConnection Conn = (OracleConnection)objConn;
            OracleDataAdapter dtpter = new OracleDataAdapter(sSql, Conn);
            dtpter.SelectCommand.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                dtpter.Fill(dsX, sTbName);
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        #endregion
    }
    public class DBOptrForOleDB
    {
        #region  ���Ӷ������
        public static string ConnOpenByConnStr(DbConnection objConn, string strCon)
        {
            
            string sRet = "-1";
            OleDbConnection Conn = (OleDbConnection)objConn;
            if (Conn.State == ConnectionState.Open) Conn.Close();
            Conn.ConnectionString = strCon;
            try
            {
                Conn.Open();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        public static string ConnOpen(DbConnection objConn, DataBaseType dbtX, string sSvr)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(dbtX , sSvr);
            sRet = ConnOpenByConnStr(objConn, strCon);
            return (sRet);
        }
        public static string ConnOpen(DbConnection objConn, DataBaseType dbtX, string sSvr, string sUser, string sPwd)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(dbtX, sSvr, sUser, sPwd);
            sRet = ConnOpenByConnStr(objConn, strCon);
            return (sRet);
        }
        public static string ConnOpen(DbConnection objConn, DataBaseType dbtX, string sSvr,string sDB, string sUser, string sPwd)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(dbtX, sSvr,sDB, sUser, sPwd);
            sRet = ConnOpenByConnStr(objConn, strCon);
            return (sRet);
        }
        #endregion
        #region ִ��SQL���
        /// <summary>
        /// �������Ӷ���SQL��䣬���ؼ�¼���е�һ���������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="sTbName"></param>
        /// <param name="dsX"></param>
        /// <returns></returns>
        public static string OptOpenDataSet(DbConnection objConn, string sSql, string sTbName, DataSet dsX)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dsX == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OleDbConnection Conn = (OleDbConnection)objConn;
           
            OleDbDataAdapter dtpter = new OleDbDataAdapter(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                dtpter.Fill(dsX, sTbName);
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����һDataReader�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="sTbName"></param>
        /// <param name="dtRder"></param>
        /// <returns></returns>
        public static string OptOpenDataReader(DbConnection objConn, string sSql, string sTbName, OleDbDataReader dtRder)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dtRder == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� dtRder ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OleDbConnection Conn = (OleDbConnection)objConn;
            
            OleDbCommand cmd = new OleDbCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                dtRder = cmd.ExecuteReader();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL�����ִ�е�Ӱ�������������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iLines"></param>
        /// <returns></returns>
        public static string OptExecRetLineCount(DbConnection objConn, string sSql, out Int64 iLines)
        {
            string sRet = "-1";
            iLines = -1;
            if (objConn == null)
            {
                sRet = "OptExecRetLineCount �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OleDbConnection Conn = (OleDbConnection)objConn;
            OleDbCommand cmd = new OleDbCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                iLines = cmd.ExecuteNonQuery();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�н���ĵ�һ�еĵ�һ�е�OBJECTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public static string OptExecRetObj(DbConnection objConn, string sSql, out object objResult)
        {
            object objX = null;
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptExecRetObj �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                objResult = null;
                return (sRet);
            }
            OleDbConnection Conn = (OleDbConnection)objConn;
            OleDbCommand cmd = new OleDbCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objX = cmd.ExecuteScalar();
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            objResult = objX;
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�INTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetInt(DbConnection objConn, string sSql, out Int64 iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OleDbConnection Conn = (OleDbConnection)objConn;
            OleDbCommand cmd = new OleDbCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = Int64.Parse(sX);
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�doubleֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetDouble(DbConnection objConn, string sSql, out double iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OleDbConnection Conn = (OleDbConnection)objConn;
            OleDbCommand cmd = new OleDbCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = Double.Parse(sX);
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�boolֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetBool(DbConnection objConn, string sSql, out bool iResult)
        {
            string sRet = "-1";
            iResult = false;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            object objRet = null;
            OleDbConnection Conn = (OleDbConnection)objConn;
            OleDbCommand cmd = new OleDbCommand(sSql, Conn);
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                cmd.CommandTimeout = 0;
                objRet = cmd.ExecuteScalar();
                if (objRet != null)
                {
                    string sX = "";
                    sX = objRet.ToString();
                    if (sX.ToUpper() != "NULL")
                    {
                        iResult = int.Parse(sX) > 0;
                    }
                }
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        public static string OptOpenDataSet(DbConnection objConn, OleDbTransaction trn ,string sSql, string sTbName, DataSet dsX)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dsX == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            OleDbConnection Conn = (OleDbConnection)objConn;

            OleDbDataAdapter dtpter = new OleDbDataAdapter(sSql, Conn);
            dtpter.SelectCommand.Transaction = trn;
            try
            {
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                dtpter.Fill(dsX, sTbName);
                sRet = "0";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return (sRet);
        }
        #endregion
    }
    public class DBOptrForComm
    {
        #region  ���Ӷ������
        public static string ConnOpenByConnStr(DataBaseType dbtX, DbConnection objConn, string strCon)
        {
            string sRet = "-1";
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL :
                    sRet = DBOptrForMSSql.ConnOpenByConnStr(objConn, strCon);
                    break;
                case DataBaseType.dbtOracle :
                    sRet = DBOptrForOracle.ConnOpenByConnStr(objConn, strCon);
                    break;
                default :
                    sRet = DBOptrForOleDB.ConnOpenByConnStr(objConn, strCon);
                    break;
            }
            return (sRet);
        }
        public static string ConnOpen(DataBaseType dbtX, DbConnection objConn, string sSvr)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(dbtX, sSvr);
            sRet = ConnOpenByConnStr(dbtX,objConn, strCon);
            return (sRet);
        }
        public static string ConnOpen(DataBaseType dbtX, DbConnection objConn, string sSvr, string sUser, string sPwd)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(dbtX, sSvr, sUser, sPwd);
            sRet = ConnOpenByConnStr(dbtX,objConn, strCon);
            return (sRet);
        }
        public static string ConnOpen(DataBaseType dbtX, DbConnection objConn, string sSvr,string sDB, string sUser, string sPwd)
        {
            string sRet = "-1";
            string strCon = "";
            strCon = DBBase.GetConnectionString(dbtX, sSvr,sDB, sUser, sPwd);
            sRet = ConnOpenByConnStr(dbtX, objConn, strCon);
            return (sRet);
        }
        #endregion
        #region ִ��SQL���
        /// <summary>
        /// �������Ӷ���SQL��䣬���ؼ�¼���е�һ���������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="sTbName"></param>
        /// <param name="dsX"></param>
        /// <returns></returns>
        public static string OptOpenDataSet(DataBaseType dbtX, DbConnection objConn, string sSql, string sTbName, DataSet dsX)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dsX == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL :
                    sRet = DBOptrForMSSql.OptOpenDataSet(objConn, sSql, sTbName, dsX);
                    break;
                case DataBaseType.dbtOracle :
                    sRet = DBOptrForOracle.OptOpenDataSet(objConn, sSql, sTbName, dsX);
                    break;
                default :
                    sRet = DBOptrForOleDB.OptOpenDataSet(objConn, sSql, sTbName, dsX);
                    break ;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����һDataReader�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="sTbName"></param>
        /// <param name="dtRder"></param>
        /// <returns></returns>
        public static string OptOpenDataReader(DataBaseType dbtX, DbConnection objConn, string sSql, string sTbName, DbDataReader dtRder)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dtRder == null)
            {
                sRet = "OptOpenDataReader �������ó��� ���� dtRder ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptOpenDataReader(objConn, sSql, sTbName, (SqlDataReader)dtRder);
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptOpenDataReader(objConn, sSql, sTbName, (OracleDataReader) dtRder);
                    break;
                default:
                    sRet = DBOptrForOleDB.OptOpenDataReader(objConn, sSql, sTbName, (OleDbDataReader)dtRder);
                    break;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL�����ִ�е�Ӱ�������������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iLines"></param>
        /// <returns></returns>
        public static string OptExecRetLineCount(DataBaseType dbtX, DbConnection objConn, string sSql, out Int64 iLines)
        {
            string sRet = "-1";
            iLines = -1;
            if (objConn == null)
            {
                sRet = "OptExecRetLineCount �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptExecRetLineCount(objConn, sSql, out iLines);
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptExecRetLineCount(objConn, sSql, out iLines);
                    break;
                default:
                    sRet = DBOptrForOleDB.OptExecRetLineCount(objConn, sSql, out iLines);
                    break;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�н���ĵ�һ�еĵ�һ�е�OBJECTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="objResult"></param>
        /// <returns></returns>
        public static string OptExecRetObj(DataBaseType dbtX, DbConnection objConn, string sSql, out object objResult)
        {
            object objX = null;
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptExecRetObj �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                objResult = null;
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptExecRetObj(objConn, sSql, out objX );
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptExecRetObj(objConn, sSql, out objX );
                    break;
                default:
                    sRet = DBOptrForOleDB.OptExecRetObj(objConn, sSql, out objX);
                    break;
            }
            objResult = objX;
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�INTֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetInt(DataBaseType dbtX, DbConnection objConn, string sSql, out Int64 iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptExecRetInt(objConn, sSql, out iResult);
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptExecRetInt(objConn, sSql, out iResult);
                    break;
                default:
                    sRet = DBOptrForOleDB.OptExecRetInt(objConn, sSql, out iResult);
                    break;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�doubleֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetDouble(DataBaseType dbtX, DbConnection objConn, string sSql, out double iResult)
        {
            string sRet = "-1";
            iResult = 0;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptExecRetDouble(objConn, sSql, out iResult);
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptExecRetDouble(objConn, sSql, out iResult);
                    break;
                default:
                    sRet = DBOptrForOleDB.OptExecRetDouble(objConn, sSql, out iResult);
                    break;
            }
            return (sRet);
        }
        /// <summary>
        /// �������Ӷ���SQL��䣬����SQL���ִ�еĵ�һ�е�һ�е�boolֵ�������ķ���ֵ 0����ִ�гɹ��� ����Ϊ���ش�����Ϣ
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="sSql"></param>
        /// <param name="iResult"></param>
        /// <returns></returns>
        public static string OptExecRetBool(DataBaseType dbtX, DbConnection objConn, string sSql, out bool iResult)
        {
            string sRet = "-1";
            iResult = false;
            if (objConn == null)
            {
                sRet = "OptExecRetInt �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptExecRetBool(objConn, sSql, out iResult);
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptExecRetBool(objConn, sSql, out iResult);
                    break;
                default:
                    sRet = DBOptrForOleDB.OptExecRetBool(objConn, sSql, out iResult);
                    break;
            }
            return (sRet);
        }

        public static string OptOpenDataSet(DataBaseType dbtX, DbConnection objConn,DbTransaction trn, string sSql, string sTbName, DataSet dsX)
        {
            string sRet = "-1";
            if (objConn == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� objConn ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            if (dsX == null)
            {
                sRet = "OptOpenDataSet �������ó��� ���� dsX ���Ӷ�����Ϊ�գ�";
                return (sRet);
            }
            switch (dbtX)
            {
                case DataBaseType.dbtMSSQL:
                    sRet = DBOptrForMSSql.OptOpenDataSet(objConn,(SqlTransaction) trn, sSql, sTbName, dsX);
                    break;
                case DataBaseType.dbtOracle:
                    sRet = DBOptrForOracle.OptOpenDataSet(objConn, (OracleTransaction)trn, sSql, sTbName, dsX);
                    break;
                default:
                    sRet = DBOptrForOleDB.OptOpenDataSet(objConn, (OleDbTransaction)trn, sSql, sTbName, dsX);
                    break;
            }
            return (sRet);
        }

        #endregion
    }
}
