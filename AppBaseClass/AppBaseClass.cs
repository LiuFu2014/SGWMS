using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common ;
using CommBase;
using DBBase;
using System.Data.OracleClient;
using DBCommInfo;
using System.Net;
using System.Net.Sockets;

namespace App
{
    public class WMSAppInfo : AppInfo 
    {
        private DataBaseType _dbtApp = DataBaseType.dbtMSSQL;
        private DataBaseType _dbtErp = DataBaseType.dbtMSSQL;
        private Socket _SvrSocket = null;
        private System.Data.Common.DbConnection _AppConn = null;
        private System.Data.Common.DbConnection _ErpConn = null;
        
        /// <summary>
        /// 初始化类
        /// </summary>
        public void InitClass()
        {
            if (_AppConn != null)
            {
                _AppConn.Dispose();
            }
            if (_ErpConn != null)
            {
                _ErpConn.Dispose();
            }

            /*
            this.AppPath = "";
            AppTitle = "自动化立库管理系统";
            AppVersion = "1.0.0.1";
            AppConfigFile = "WMSConfig.xml";
             * */
            switch (_dbtApp)
            {
                case DataBaseType.dbtMSSQL:
                    _AppConn = new System.Data.SqlClient.SqlConnection();
                    break;
                case DataBaseType.dbtOracle:
                    _AppConn = new System.Data.OracleClient.OracleConnection();
                    break;
                case DataBaseType.dbtMySQL:
                    _AppConn = new System.Data.OleDb.OleDbConnection();
                    break;
                case DataBaseType.dbtParadox:
                    _AppConn = new System.Data.OleDb.OleDbConnection();
                    break;
                case DataBaseType.dbtMSAccess:
                    _AppConn = new System.Data.OleDb.OleDbConnection();
                    break;
            }
            switch (_dbtErp)
            {
                case DataBaseType.dbtMSSQL:
                    _ErpConn = new System.Data.SqlClient.SqlConnection();
                    break;
                case DataBaseType.dbtOracle:
                    _ErpConn = new System.Data.OracleClient.OracleConnection();
                    break;
                case DataBaseType.dbtMySQL:
                    _ErpConn = new System.Data.OleDb.OleDbConnection();
                    break;
                case DataBaseType.dbtParadox:
                    _ErpConn = new System.Data.OleDb.OleDbConnection();
                    break;
                case DataBaseType.dbtMSAccess:
                    _ErpConn = new System.Data.OleDb.OleDbConnection();
                    break;
            }
            
        }
        public  WMSAppInfo()
        {
            InitClass();
        }

        /// <summary>
        /// 系统的SOCKET 通讯对象
        /// </summary>
        public Socket SvrSocket
        {
            get { return _SvrSocket; }
            set { _SvrSocket = value; }
        }
        public  WMSAppInfo(DataBaseType dbtApp, DataBaseType dbtErp)
        {
            _dbtApp = dbtApp;
            _dbtErp = dbtErp;
            InitClass();
        }
        ~WMSAppInfo()
        {
            //if (_SvrSocket != null)
            //{
            //    if (_SvrSocket.Connected)
            //    {
            //        _SvrSocket.Close();                    
                   
            //    }
            //}
            /*
            if (_AppConn != null)
            {
                switch(_dbtApp)
                {
                    case DataBaseType.dbtMSSQL :
                        ((System.Data.SqlClient.SqlConnection) _AppConn).Dispose();
                        break;
                    case DataBaseType.dbtOracle :
                        ((OracleConnection)_AppConn).Dispose();
                        break;
                }            
            }
            if (_ErpConn != null)
            {
                switch (_dbtErp)
                {
                    case DataBaseType.dbtMSSQL:
                        ((System.Data.SqlClient.SqlConnection)_ErpConn).Dispose();
                        break;
                    case DataBaseType.dbtOracle:
                        ((OracleConnection)_ErpConn).Dispose();
                        break;
                } 
            }
             * */
        }
        //

        /// <summary>
        /// 系统的连接对象
        /// </summary>
        public DbConnection AppConn
        {
            get { return( _AppConn); }
        }

        /// <summary>
        /// ERP系统的连接对象
        /// </summary>
        public DbConnection ErpConn
        {
            get { return (_ErpConn); }
        }

        /// <summary>
        /// 本系统的数据库类型
        /// </summary>
        public DataBaseType dbtApp
        {
            get { return (_dbtApp); }
            set 
            {
                _dbtApp = value;
                if (_AppConn != null)
                {
                    _AppConn.Dispose();
                }
                switch (_dbtApp)
                {
                    case DataBaseType.dbtMSSQL:
                        _AppConn = new System.Data.SqlClient.SqlConnection();
                        break;
                    case DataBaseType.dbtOracle:
                        _AppConn = new System.Data.OracleClient.OracleConnection();
                        break;
                    case DataBaseType.dbtMySQL:
                        _AppConn = new System.Data.OleDb.OleDbConnection();
                        break;
                    case DataBaseType.dbtParadox:
                        _AppConn = new System.Data.OleDb.OleDbConnection();
                        break;
                    case DataBaseType.dbtMSAccess:
                        _AppConn = new System.Data.OleDb.OleDbConnection();
                        break;
                }
            }
        }

        /// <summary>
        /// ERP系统的数据库类型
        /// </summary>
        public DataBaseType dbtErp
        {
            get { return (_dbtErp); }
            set
            {
                _dbtErp = value;
                if (_ErpConn != null)
                {
                    _ErpConn.Dispose();
                }
                switch (_dbtErp)
                {
                    case DataBaseType.dbtMSSQL:
                        _ErpConn = new System.Data.SqlClient.SqlConnection();
                        break;
                    case DataBaseType.dbtOracle:
                        _ErpConn = new System.Data.OracleClient.OracleConnection();
                        break;
                    case DataBaseType.dbtMySQL:
                        _ErpConn = new System.Data.OleDb.OleDbConnection();
                        break;
                    case DataBaseType.dbtParadox:
                        _ErpConn = new System.Data.OleDb.OleDbConnection();
                        break;
                    case DataBaseType.dbtMSAccess:
                        _ErpConn = new System.Data.OleDb.OleDbConnection();
                        break;
                }
            }
        }

        private System.Drawing.Icon _AppICON = null;
        /// <summary>
        /// 应用程序的图标
        /// </summary>
        public System.Drawing.Icon AppICON
        {
            get { return _AppICON; }
            set
            {
                _AppICON = value;
            }
        }
    }


    public class WMSUserInfo : UserInfo 
    {
        private string _LinkId = "";  //其他系统关联的用户编号
        private bool _bIsChecked = false; //是否身份验证通过
        /// <summary>
        /// 用于其他系统的关联编号
        /// </summary>
        public string LinkId
        {
            get { return (_LinkId); }
            set { _LinkId = value; }
        }
        /// <summary>
        /// 是否身份验证通过
        /// </summary>

        public bool bIsChecked
        {
            get { return (_bIsChecked); }
            set { _bIsChecked = value; }            
        }

        //
        public WMSUserInfo()
        {            
            _LinkId = "";
            _bIsChecked = false;           
        }
    }
}
