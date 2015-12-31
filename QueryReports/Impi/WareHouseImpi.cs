using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SunEast.App;
 

namespace QueryReports.Impi
{
    class WareHouseImpi
    {
        public WareHouseImpi()
        {
 
        }

        /// <summary>
        /// 取得仓库的信息
        /// </summary>
        /// <param name="UserInformation"></param>
        /// <returns></returns>
        public DataTable GetData(App.WMSUserInfo UserInformation)
        {
            string strSql = "select * from TWC_WareHouse where bUsed=1 ";
            if (UserInformation.UType != CommBase.UserType.utSupervisor)
            {
                strSql += " and cWHId in (select cWHId from TPB_UserWHouse where cUserId='" + UserInformation.UserId.Trim() + "')";
            }
            string err = "";
            DataTable tbWare = new DataTable();
            try
            {
                DataSet dsY = PubDBCommFuns.GetDataBySql(strSql, out err);
                if (err != "")
                { 
                    MyTools.MessageBox(strSql);
                    return new DataTable();
                }
                else
                {
                    tbWare = dsY.Tables["data"].Copy();
                    return tbWare;
                }
            }
            catch (Exception er)
            {                
                MyTools.MessageBox(er.Message);
                return new DataTable();
            }
        }
    }
}
