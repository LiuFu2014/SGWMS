using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommBase;
using DBCommInfo;
using Zqm.Text;
using SunEast;

namespace WareStoreMS
{
    public partial class Form_StockMCheckFilter1 : Form
    {
        public Form_StockMCheckFilter1()
        {
            InitializeComponent();
        }

        private void Form_StockMCheckFilter1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sId = "";
            DBCommInfo.DBSQLCommandInfo cmdInfo = new DBSQLCommandInfo();//执行命令的对象
            cmdInfo.SqlText = "sp_Chk_WriteChkDtl :cUser,:cCheckNo,:cWHId,:cPalletId,:cBoxId,:cMNo,:cBatchNo,:nQCStatus,:fQty,:fRQty,:cUnit";                             //SQL语句  或 存储过程名 若有参数，另外在参数集里增加

            cmdInfo.SqlType = SqlCommandType.sctProcedure;                        //SQL命令类型  SqlCommandType.sctSql  SQL 语句 SqlCommandType.sctProcedure 表存储过程
            cmdInfo.PageIndex = 0;                                          //需要分页时的页号
            cmdInfo.PageSize = 0;                                           //需要分页时的每页记录条数
            cmdInfo.FromSysType = "dotnet";                                 //采用处理结果数据的方式：php 表按照<tr><td></td></tr> xml 否则 直接采用ado 的记录集方式
            //cmdInfo.DataTableName = strTbNameMain;                          //指定结果数据记录集表名  默认为 data
            //定义参数
            ZqmParamter par = null;           //参数对象 变量                          
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cUser";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_cUser.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cCheckNo";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_cCheckNo.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---

            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cWHId";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_cWHId.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cPalletId";           //参数名称 和实际定义的一致
            par.ParameterValue =textBox_cPalletId.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cBoxId";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_cBoxId.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cMNo";           //参数名称 和实际定义的一致
            par.ParameterValue =textBox_cMNo.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cBatchNo";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_cBatchNo.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "nQCStatus";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_nQCStatus.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "fQty";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_fQty.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------

            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "fRQty";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_fRQty.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---
            //------
            par = new ZqmParamter();          //参数对象实例
            par.ParameterName = "cUnit";           //参数名称 和实际定义的一致
            par.ParameterValue = textBox_cUnit.Text.ToString();            //参数值 可以为""空
            par.DataType = ZqmDataType.String;  //参数的数据类型
            par.ParameterDir = ZqmParameterDirction.Input;    //指定参数 为输入、输出类型
            //添加参数
            cmdInfo.Parameters.Add(par);
            //---

            //执行命令
            SunEast.SeDBClient sdcX = new SeDBClient();                     //获取服务器数据的类型对象
            //sdcX.DBSTServer = DBSocketServerType.dbsstNormal;  //自动根据配置文件读
            string sErr = "";
            DataSet dsX = null;
            DataTable tbX = null;

            dsX = sdcX.GetDataSet(cmdInfo, out sErr);               //通过获取服务器数据对象的GetDataSet方法获取数据
            if (dsX != null)
            {
                tbX = dsX.Tables["data"];
                //  if (tbX != null)
                //    sId = tbX.Rows[0]["cNewId"].ToString();
            }
            if ((tbX.Rows[0]["cResult"].ToString())=="0")
            {
                MessageBox.Show("实盘登记成功");
            }
            dsX.Clear();
            Close();


        }
    }
}