using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace DataImporter
{
    public class DataImporter
    {
        /// <summary>
        /// 导入入库单数据
        /// </summary>
        /// <param name="_conWms">WMS的连接对象</param>
        /// <param name="_conErp">ERP的连接对象</param>
        /// <param name="sUserId">用户编码</param>
        /// <param name="sUserName">用户名称</param>
        public static void DataImportForBillIn(OleDbConnection _conWms, OleDbConnection _conErp, string sUserId, string sUserName)
        {
            frmInBillImp frmX = new frmInBillImp();
            //frmX.ConErp = _conErp;
            frmX.WMSConn = _conWms;
            frmX.UserId = sUserId;
            frmX.IsBillIn = true;
            frmX.UserName = sUserName;
            frmX.ShowDialog();
            frmX.Dispose();
        }

        /// <summary>
        /// 导入出库单数据
        /// </summary>
        /// <param name="_conWms">WMS的连接对象</param>
        /// <param name="_conErp">ERP的连接对象</param>
        /// <param name="sUserId">用户编码</param>
        /// <param name="sUserName">用户名称</param>
        public static void DataImportForBillOut(OleDbConnection _conWms, OleDbConnection _conErp, string sUserId, string sUserName)
        {
            frmInBillImp frmX = new frmInBillImp();
            //frmX.ConErp = _conErp;
            frmX.WMSConn = _conWms;
            frmX.UserId = sUserId;
            frmX.UserName = sUserName;
            frmX.IsBillIn = false;
            frmX.ShowDialog();
            frmX.Dispose();
        }

    }
}
