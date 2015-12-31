using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CommBase;
using System.IO;
using UI;

namespace UI
{
    public partial class FrmSTable : Form 
    {
        #region ˽�б���
            bool _IsEnterAsTabKey = true;  // �س����ƽ���
            private App.WMSAppInfo _AppInformation;
            private App.WMSUserInfo _UserInformation;
            private DataSet _DBDataSet;
            private string _ModuleRtsId = "";
            private string _ModuleRtsName = "";
        
        #endregion

        #region ˽�з���

        #endregion

        #region �������Լ�����
           [Description("Ӧ�ó����������")]
            public App.WMSAppInfo AppInformation
            {
                get { return (_AppInformation); }
                set { _AppInformation = value; }
            }
            [Description("��ǰ��¼�û���������")]
            public App.WMSUserInfo UserInformation
            {
                get { return (_UserInformation); }
                set { _UserInformation = value; }
            }
            /// <summary>
            /// ʹ�س�����Ϊ Tab��ʹ��
            /// </summary>
            [Description("���ûس�����Ϊ Tab��ʹ�ã�ͬʱ KeyPreview ����Ҳ��֮���ı�")]
            public bool IsEnterAsTabKey
            {
                get 
                {
                    return (_IsEnterAsTabKey); 
                }
                set
                {
                    _IsEnterAsTabKey = value;
                    KeyPreview = value;
                }
            }
            /// <summary>
            /// ��ǰ��������ݼ�
            /// </summary>
            [Description("��ǰ��������ݼ�����")]
            public DataSet DBDataSet
            {
                get { return (_DBDataSet); }
                set { _DBDataSet = value; }
            }

            [Description("��ǰ����ģ���Ȩ�ޱ��")]
            /// <summary>
            /// ��ǰ����ģ���Ȩ�ޱ��
            /// </summary>           
            public string ModuleRtsId
            {
                get { return (_ModuleRtsId); }
                set { _ModuleRtsId = value; }
            }
            [Description("��ǰ����ģ�������")]
            public string ModuleRtsName
            {
                get { return (_ModuleRtsName); }
                set { _ModuleRtsName = value; }
            }

            private Color _TextBackColorDisable = Color.FromName("Control");
            [Description("��ǰ������ı��򲻿���ʱ�ı�����ɫ")]
            public Color TextBackColorDisable
            {
                get { return _TextBackColorDisable; }
                set { _TextBackColorDisable = value; }
            }

            private Color _TextBackColorEnable = Color.White;
            [Description("��ǰ������ı������ʱ�ı�����ɫ")]
            public Color TextBackColorEnable
            {
                get { return _TextBackColorEnable; }
                set { _TextBackColorEnable = value; }
            }

            public int SetCombBoxValue(string sValue, ComboBox cmbX)
            {
                int iX = -1 ;
                string sSource = "";
                if (cmbX.DataSource != null)
                {
                    sSource = cmbX.DataSource.GetType().ToString();
                    if (cmbX.ValueMember != "")
                    {
                        switch (sSource)
                        {
                            case "System.Collections.ArrayList":
                                ArrayList arr = (ArrayList)cmbX.DataSource;
                                foreach (DictionaryEntry dct in arr)
                                {
                                    if (dct.Key.ToString().ToLower().Trim() == sValue.Trim().ToLower())
                                    {
                                        iX = arr.IndexOf(dct);
                                        cmbX.SelectedIndex = iX;
                                    }
                                }
                                break;
                                 
                            case "System.Data.DataTable":
                                //DataTable tbX = (DataTable)cmbX.DataSource;
                                //DataRow dr = tbX.Rows.Find(sValue.Trim());
                                //if (dr != null)
                                //{
                                //    iX = tbX.Rows.IndexOf(dr); 
                                //    cmbX.SelectedIndex = iX;
                                //}
                                cmbX.SelectedValue = sValue;
                                break ;
                            case "System.Data.DataView":
                                //DataView dvX = (DataView)cmbX.DataSource;
                                //if (dvX != null)
                                //{
                                //    iX = dvX.Find(sValue.Trim());
                                //    cmbX.SelectedIndex = iX;
                                //}
                                cmbX.SelectedValue = sValue;
                                break;
                        }
                    }
                    else
                    {
                        iX = cmbX.Items.IndexOf(sValue.Trim());
                        cmbX.SelectedIndex = iX;
                    }                
                }
                return iX;           
            }

            [Description("���Ƶ�ǰ�û��Ĳ���Ȩ��")]
            /// <summary>
            /// ��ǰ�û��Ĳ���Ȩ�޿���
            /// </summary>
            /// <param name="pnl"></param>
            /// <param name="tbRts"></param>           
            public virtual void CheckRights(Control pnl,DataTable tbRts)
            {
     
                DataRowView drv;
                int i = 0;
                int j = 0;                           
                //��ʼ��
                foreach( Control ctrlX in pnl.Controls)
                {
                    if (ctrlX.Tag != null)
                    {
                        ctrlX.Visible = false;
                    }
                }
                if (tbRts != null)
                {
                    if (tbRts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tbRts.Rows)
                        {
                            string sX = dr["cRId"].ToString();
                            foreach (Control ctrlX in pnl.Controls)
                            {
                                if (ctrlX.Tag != null)
                                {
                                    if (sX.Trim().ToUpper() == ModuleRtsId.Trim() + ctrlX.Tag.ToString().ToUpper())
                                    {
                                        ctrlX.Visible = true;
                                    }
                                }

                            }

                        }
                    }
                }
            }
            
            /// <summary>
            /// �����û�Ȩ��
            /// </summary>
            /// <param name="pnl"> ������</param>
            /// <param name="tbRts">�û�Ȩ�ޱ�</param>
            public virtual void CheckRights(ToolStrip pnl, DataTable tbRts)
            {

                DataRowView drv;
                int i = 0;
                int j = 0;
                //��ʼ��
                foreach (ToolStripItem ctrlX in pnl.Items)
                {
                    if (ctrlX.Tag != null)
                    {
                        ctrlX.Visible = false;
                    }
                }
                if (tbRts != null)
                {
                    if (tbRts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tbRts.Rows)
                        {
                            string sX = dr["cRId"].ToString();
                            foreach (ToolStripItem ctrlX in pnl.Items)
                            {
                                if (ctrlX.Tag != null)
                                {
                                    if (sX.Trim().ToUpper() == ModuleRtsId.Trim() + ctrlX.Tag.ToString().ToUpper())
                                    {
                                        ctrlX.Visible = true;
                                    }
                                }

                            }

                        }
                    }
                }
            }

            /// <summary>
            /// �����û�Ȩ��
            /// </summary>
            /// <param name="pnl">���˵�</param>
            /// <param name="tbRts">�û�Ȩ�ޱ�</param>
            public virtual void CheckRights(MenuStrip pnl, DataTable tbRts)
            {

                DataRowView drv;
                int i = 0;
                int j = 0;
                //��ʼ��
                foreach (ToolStripMenuItem ctrlX in pnl.Items )
                {
                    if (ctrlX.Tag != null)
                    {
                        ctrlX.Visible = false;
                    }
                }
                if (tbRts != null)
                {
                    if (tbRts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tbRts.Rows)
                        {
                            string sX = dr["cRId"].ToString();
                            foreach (ToolStripMenuItem ctrlX in pnl.Items)
                            {
                                if (ctrlX.Tag != null)
                                {
                                    if (sX.Trim().ToUpper() == ModuleRtsId.Trim() + ctrlX.Tag.ToString().ToUpper())
                                    {
                                        ctrlX.Visible = true;
                                    }
                                }

                            }

                        }
                    }
                }
            }
            
            /// <summary>
            /// �����û�Ȩ��
            /// </summary>
            /// <param name="pnl">��ݲ˵�</param>
            /// <param name="tbRts">�û�Ȩ�ޱ�</param>
            public virtual void CheckRights(ContextMenuStrip pnl, DataTable tbRts)
            {

                DataRowView drv;
                int i = 0;
                int j = 0;
                //��ʼ��
                try
                {
                    foreach (ToolStripItem ctrlX in pnl.Items)
                    {
                        if (ctrlX.Tag != null)
                        {
                            ctrlX.Visible = false;
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                if (tbRts != null)
                {
                    if (tbRts.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tbRts.Rows)
                        {
                            string sX = dr["cRId"].ToString();
                            foreach (ToolStripItem ctrlX in pnl.Items)
                            {
                                if (ctrlX.Tag != null)
                                {
                                    if (sX.Trim().ToUpper() == ModuleRtsId.Trim() + ctrlX.Tag.ToString().ToUpper())
                                    {
                                        ctrlX.Visible = true;
                                    }
                                }

                            }

                        }
                    }
                }
            }


            /// <summary>
            /// ���ݿؼ�����,�����ؼ�
            /// </summary>
            /// <param name="pnl"></param>
            /// <param name="sValue"></param>
            /// <returns></returns>
            public Control FindControlByName(Control pnl, string sValue)
            {
                string sX = "";
                bool bIsOK = false;
                Control ctrlResult=null;
                foreach (Control ctrlX in pnl.Controls)
                {
                    sX = ctrlX.Name.ToUpper().Trim();
                    if (sValue.Trim().ToUpper() == sX)
                    {
                        ctrlResult = ctrlX;
                        bIsOK = true;
                        break;
                    }
                    
                }
                if (!bIsOK)
                {
                    ctrlResult = null;
                }
                return (ctrlResult);

            }
            /// <summary>
            /// �����ݲ����Ŀ�������
            /// </summary>
            /// <param name="pnlBtns">��ť����</param>
            /// <param name="pnlEdit">�༭������</param>
            /// <param name="opt">��ǰ�Ĳ���</param>
            /// <param name="tbDB">��ǰ�����������ݱ����</param>
            public virtual void CtrlOptButtons(Control pnlBtns,Control pnlEdit, CommBase.OperateType opt,DataTable tbDB)
            {
                int i = 0;
                int j = 0;
                string sX = "";
                string[] sList;
                char[] spltChar = {'_'};
                sX = pnlBtns.GetType().ToString();
                i = pnlBtns.Controls.Count;
                //���ư�ť�Ŀɲ�����
                foreach (Control ctrlX in pnlBtns.Controls)
                {
                    sX = "";
                    sList = ctrlX.Name.ToString().Split(spltChar);
                    if (sList.Length > 0)
                    {
                        sX = sList[sList.Length - 1];
                    }
                    if (sX != "")
                    {
                        switch (sX.ToUpper())
                        {
                            case "NEW":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default: 
                                            ctrlX.Enabled = true;
                                            break;
                                    }                                    
                                }
                                break;
                            case "EDIT":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "UNDO":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = true;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = true;
                                            break;
                                        default:
                                            ctrlX.Enabled = false;
                                            break;
                                    }
                                }
                                break;
                            case "DELETE":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false ;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "SAVE":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = true ;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = true;
                                            break;
                                        default:
                                            ctrlX.Enabled = false ;
                                            break;
                                    }
                                }
                                break;
                            case "FIND":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false ;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = true ;
                                            break;
                                    }
                                }
                                break;
                            case "PRINT":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = true;
                                            break;
                                    }
                                }
                                break;
                            case "CHECK":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "UNCHECK":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "REFRESH":
                                //{
                                //    switch (opt)
                                //    {
                                //        case CommBase.OperateType.optEdit:
                                //            ctrlX.Enabled = false;
                                //            break;
                                //        case CommBase.OperateType.optNew:
                                //            ctrlX.Enabled = false;
                                //            break;
                                //        default:
                                //            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                //            break;
                                //    }
                                //}
                                ctrlX.Enabled = true;
                                break;
                            default:
                                {
                                    //switch (opt)
                                    //{
                                    //    case CommBase.OperateType.optEdit:
                                    //        ctrlX.Enabled = false;
                                    //        break;
                                    //    case CommBase.OperateType.optNew:
                                    //        ctrlX.Enabled = false;
                                    //        break;
                                    //    default:
                                    //        ctrlX.Enabled = tbDB.Rows.Count > 0;
                                    //        break;
                                    //}
                                    ctrlX.Enabled = true;
                                }
                                break;
                        }
                    }
                }                
            }
            /// <summary>
            /// �����ݲ����Ŀ�������
            /// </summary>
            /// <param name="pnlBtns">ToolStrip</param>
            /// <param name="pnlEdit">�༭������</param>
            /// <param name="opt">��ǰ�Ĳ���</param>
            /// <param name="tbDB">��ǰ�����������ݱ����</param>
            public virtual void CtrlOptButtons(System.Windows.Forms.ToolStrip pnlBtns, Control pnlEdit, CommBase.OperateType opt, DataTable tbDB)
            {
                int i = 0;
                int j = 0;
                string sX = "";
                string[] sList;
                char[] spltChar = { '_' };
                sX = pnlBtns.GetType().ToString();
                i = pnlBtns.Controls.Count;
                //���ư�ť�Ŀɲ�����
                foreach (ToolStripItem ctrlX in pnlBtns.Items)
                {
                    sX = "";
                    sList = ctrlX.Name.ToString().Split(spltChar);
                    if (sList.Length > 0)
                    {
                        sX = sList[sList.Length - 1];
                    }
                    if (sX != "")
                    {
                        switch (sX.ToUpper())
                        {
                            case "NEW":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = true;
                                            break;
                                    }
                                }
                                break;
                            case "EDIT":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "UNDO":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = true;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = true;
                                            break;
                                        default:
                                            ctrlX.Enabled = false;
                                            break;
                                    }
                                }
                                break;
                            case "DELETE":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "SAVE":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = true;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = true;
                                            break;
                                        default:
                                            ctrlX.Enabled = false;
                                            break;
                                    }
                                }
                                break;
                            case "FIND":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = true;
                                            break;
                                    }
                                }
                                break;
                            case "PRINT":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = true;
                                            break;
                                    }
                                }
                                break;
                            case "CHECK":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "UNCHECK":
                                {
                                    switch (opt)
                                    {
                                        case CommBase.OperateType.optEdit:
                                            ctrlX.Enabled = false;
                                            break;
                                        case CommBase.OperateType.optNew:
                                            ctrlX.Enabled = false;
                                            break;
                                        default:
                                            ctrlX.Enabled = tbDB.Rows.Count > 0;
                                            break;
                                    }
                                }
                                break;
                            case "REFRESH":
                                {
                                    //switch (opt)
                                    //{
                                    //    case CommBase.OperateType.optEdit:
                                    //        ctrlX.Enabled = false;
                                    //        break;
                                    //    case CommBase.OperateType.optNew:
                                    //        ctrlX.Enabled = false;
                                    //        break;
                                    //    default:
                                    //        ctrlX.Enabled = tbDB.Rows.Count > 0;
                                    //        break;
                                    //}
                                }
                                ctrlX.Enabled = true;
                                break;
                            default:
                                {
                                    //switch (opt)
                                    //{
                                    //    case CommBase.OperateType.optEdit:
                                    //        ctrlX.Enabled = false;
                                    //        break;
                                    //    case CommBase.OperateType.optNew:
                                    //        ctrlX.Enabled = false;
                                    //        break;
                                    //    default:
                                    //        ctrlX.Enabled = tbDB.Rows.Count > 0;
                                    //        break;
                                    //}
                                    ctrlX.Enabled = true;
                                }
                                break;
                        }
                    }
                }
            }
            public virtual void InitFormParameters()
            {
                Text = _ModuleRtsName;                
            }
            public virtual void InitFormTlbBtnTag(ToolStrip tlbX,string FrmRtsId)
            {
                string[] strArr = null;
                char[] splt = {'_'} ;
                foreach (ToolStripItem tlbBtn in tlbX.Items)
                {
                    strArr = tlbBtn.Name.Trim().Split(splt);
                    if (strArr.Length > 1)
                    {
                        /*
                        optNone = 0,
                        optNew = 1,
                        optEdit = 2,
                        optDelete = 3,
                        optUndo = 4,
                        optSave = 5,
                        optRefresh = 6,
                        optPrint = 7,
                        optCheck = 8,
                        optUnCheck = 9, 
                       
                         * */
                        string sOpt = strArr[strArr.Length -1];
                        switch (sOpt.ToUpper())
                        {
                            case "NEW":
                                tlbBtn.Tag = FrmRtsId + "01";
                                break;
                            case "EDIT":
                                tlbBtn.Tag = FrmRtsId + "02";
                                break;
                            case "DELETE":
                                tlbBtn.Tag = FrmRtsId + "03";
                                break;
                            case "FIND":
                                tlbBtn.Tag = FrmRtsId + "04";
                                break;
                            case "PRINT":
                                tlbBtn.Tag = FrmRtsId + "05";
                                break;
                            case "CHECK":
                                tlbBtn.Tag = FrmRtsId + "06";
                                break;
                            case "UNCHECK":
                                tlbBtn.Tag = FrmRtsId + "07";
                                break;

                        }
                    }
                }
            }

            public virtual void InitFormMemuItemTag( MainMenu MMenu, string FrmRtsId)
            {
                
                string[] strArr = null;
                char[] splt ={ '_' };
                foreach (MenuItem miX in MMenu.MenuItems)
                {                   
                    strArr = miX.Name.Trim().Split(splt);
                    if (strArr.Length > 1)
                    {
                        /*
                        optNone = 0,
                        optNew = 1,
                        optEdit = 2,
                        optDelete = 3,
                        optUndo = 4,
                        optSave = 5,
                        optRefresh = 6,
                        optPrint = 7,
                        optCheck = 8,
                        optUnCheck = 9,                            
                         * */
                        string sOpt = strArr[strArr.Length - 1];
                        switch (sOpt.ToUpper())
                        {
                            case "NEW":
                                miX.Tag = FrmRtsId + "01";
                                break;
                            case "EDIT":
                                miX.Tag = FrmRtsId + "02";
                                break;
                            case "DELETE":
                                miX.Tag = FrmRtsId + "03";
                                break;
                            case "FIND":
                                miX.Tag = FrmRtsId + "04";
                                break;
                            case "PRINT":
                                miX.Tag = FrmRtsId + "05";
                                break;
                            case "CHECK":
                                miX.Tag = FrmRtsId + "06";
                                break;
                            case "UNCHECK":
                                miX.Tag = FrmRtsId + "07";
                                break;

                        }
                    }
                }
            }


            /// <summary>
            /// ���Ʊ༭�ؼ��Ŀɱ༭�ԣ�ע�⣬�ɱ༭������Tag����ֵ���֣�
            /// 0 TextBox 1:ComboBox��Text 101:ComboBox��SelectedValue 102:ComboBox��SelectedIndex 
            /// 2:DateTimePicker 3:CheckBox 4:RadioButton 5:ListBox 6:CheckedListBox 8:DataGridView 9:RichTextBox��
            /// </summary>
            /// <param name="pnlX">�༭�ؼ��������ؼ�</param>
            /// <param name="bIsEdit">�Ƿ�ɱ༭</param>
            public virtual void CtrlControlReadOnly(Control pnlX, bool bIsEdit)
            {
                object objX = null;
                string strX = "";
                foreach (Control ctrlX in pnlX.Controls)
                {
                    Color clrBk;
                    clrBk =_TextBackColorDisable;
                    //clrBk = this.BackColor;
                    objX = ctrlX.Tag;
                    strX = ctrlX.Name;
                    if (objX != null)
                    {
                        strX = objX.ToString();
                        switch (strX.ToUpper())
                        {
                            case "NULL":
                                break;
                            case "0":                               
                                ((TextBoxBase)ctrlX).ReadOnly = !bIsEdit;
                                if (bIsEdit) ctrlX.BackColor =_TextBackColorEnable ;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "1":
                                ((ComboBox)ctrlX).Enabled = bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "101":
                                ((ComboBox)ctrlX).Enabled = bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "102":
                                ((ComboBox)ctrlX).Enabled = bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "2":
                                ((DateTimePicker)ctrlX).Enabled = bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "3":
                                ((CheckBox)ctrlX).Enabled = bIsEdit;
                                break;
                            case "4":
                                ((RadioButton)ctrlX).Enabled = bIsEdit;
                                break;
                            case "5":
                                ((ListBox)ctrlX).Enabled = bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "6":
                                ((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;

                                else ctrlX.BackColor = clrBk;
                                break;
                            case "7":
                                //((DataGridView)ctrlX).Enabled = bIsEdit;
                                break;
                            case "8":
                                ((DataGridView)ctrlX).ReadOnly = true;// bIsEdit;
                                break;
                            case "9":
                                ((RichTextBox)ctrlX).ReadOnly = !bIsEdit;
                                if (bIsEdit) ctrlX.BackColor = _TextBackColorEnable;
                                else ctrlX.BackColor = clrBk;
                                break;
                            case "10":
                                //((CheckBox)ctrlX).Enabled = bIsEdit;
                                break;
                            case "11":
                                //((CheckBox)ctrlX).Enabled = bIsEdit;
                                break;

                        }
                    }
                }
            }
            /// <summary>
            /// ���ݵ�ǰ�Ĳ�����ʾ��ǰ�Ĳ���״̬
            /// </summary>
            /// <param name="stbSt"></param>
            /// <param name="optX"></param>
            public virtual void DisplayState(ToolStripLabel stbSt, OperateType optX)
            {
                string strText = "��״̬��";
                if (stbSt != null)
                {
                    switch (optX)
                    {
                        case OperateType.optNew:
                            strText = strText + " �½�";
                            break;
                        case OperateType.optEdit:
                            strText = strText + " �޸�";
                            break;
                        default:
                            strText = strText + "    ";
                            break;
                    }
                    stbSt.Text = strText;
                    Update();
                }
            }

            public virtual bool DataSetBind(Control pnlX, BindingSource bsX)
            {
                object objX = null;
                string strX = "";
                string strFld = "";
                string[] strArrName;
                char[] chrArrSpliter = { '_' };
                bool bIsResult = false;
                try
                {
                    foreach (Control ctrlX in pnlX.Controls)
                    {
                        objX = ctrlX.Tag;
                        if (objX != null)
                        {
                            strX = objX.ToString();
                            strArrName = ctrlX.Name.Trim().Split(chrArrSpliter);
                            if (strArrName.Length == 1) continue; //�˳���ǰѭ��������һ��ѭ��
                            strFld = strArrName[strArrName.Length - 1];
                            try
                            {
                                switch (strX.ToUpper())
                                {
                                    case "NULL":
                                        break;
                                    case "0":
                                        ((TextBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "1":
                                        ((ComboBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "101":
                                        ((ComboBox)ctrlX).DataBindings.Add(new Binding("SelectedValue", bsX, strFld));
                                        break;
                                    case "102":
                                        ((ComboBox)ctrlX).DataBindings.Add(new Binding("SelectedIndex", bsX, strFld));
                                        break;
                                    case "2":
                                        ((DateTimePicker)ctrlX).DataBindings.Add(new Binding("Value", bsX, strFld));
                                        break;
                                    case "3":
                                        ((CheckBox)ctrlX).DataBindings.Add(new Binding("Checked", bsX, strFld));
                                        break;
                                    case "4":
                                        ((RadioButton)ctrlX).DataBindings.Add(new Binding("Checked", bsX, strFld));
                                        break;
                                    case "5":
                                        //((ListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "6":
                                        //((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "7":
                                        //((DataGridView)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "8":
                                        ((DataGridView)ctrlX).DataSource = bsX;
                                        break;
                                    case "9":
                                        ((RichTextBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "10":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "11":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;

                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(ctrlX.Name + " ������Դ����:" + e.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    bIsResult = true;
                }
                catch (Exception e)
                {                    
                    bIsResult = false;
                }
                return (bIsResult);
            }

            public virtual bool DataSetUnBind(Control pnlX)
            {
                bool bIsResult = false;
                object objX = null;
                string strX = "";
                string strFld = "";
                string[] strArrName;
                char[] chrArrSpliter = { '_' };
                try
                {
                    foreach (Control ctrlX in pnlX.Controls)
                    {
                        objX = ctrlX.Tag;
                        if (objX != null)
                        {
                            strX = objX.ToString();
                            strArrName = ctrlX.Name.Trim().Split(chrArrSpliter);
                            if (strArrName.Length == 1) continue; //�˳���ǰѭ��������һ��ѭ��
                            strFld = strArrName[strArrName.Length - 1];
                            switch (strX.ToUpper())
                            {
                                case "NULL":
                                    break;
                                /*
                                case "0":
                                    ((TextBox)ctrlX).DataBindings.Add("Text", bsX, strFld);
                                    break;
                                case "1":
                                    ((ComboBox)ctrlX).DataBindings.Add("Text", bsX, strFld);
                                    break;
                                case "2":
                                    ((DateTimePicker)ctrlX).DataBindings.Add("Value", bsX, strFld);
                                    break;
                                case "3":
                                    ((CheckBox)ctrlX).DataBindings.Add("Checked", bsX, strFld);
                                    break;
                                case "4":
                                    ((RadioButton)ctrlX).DataBindings.Add("Checked", bsX, strFld);
                                    break;
                                case "5":
                                    //((ListBox)ctrlX).Enabled = bIsEdit;
                                    break;
                                case "6":
                                    //((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                    break;
                                case "7":
                                    //((DataGridView)ctrlX).Enabled = bIsEdit;
                                    break;
                                */
                                case "8":
                                    ((DataGridView)ctrlX).DataSource = null;
                                    break;
                                default:
                                    ctrlX.DataBindings.Clear();
                                    break;

                            }
                        }
                    }
                    bIsResult = true;
                }
                catch (Exception e)
                {
                    bIsResult = false;
                }
                return (bIsResult);
            }

            /// <summary>
            /// ��DataRowView ������ֶε�ֵ ��ֵ �� pnlX�������Ӧ�����ݿؼ�ֵ���ؼ������� ctrlType_DataFieldName (�ֶ������ܰ����»���_)
            /// ע�⣬�ؼ�������������Tag����ֵ���֣�
            /// 0 TextBox��Text 1:ComboBox��Text 101:ComboBox��SelectedValue 102:ComboBox��SelectedIndex 
            /// 2:DateTimePicker 3:CheckBox 4:RadioButton 5:ListBox 6:CheckedListBox 8:DataGridView 9:RichTextBox
            /// </summary>
            /// <param name="drv">DataRowView ����</param>
            /// <param name="pnlX">���ݿؼ��������ؼ�</param>
            /// <returns>�Ƿ�ɹ�</returns>
            public virtual bool DataRowViewToUI(DataRowView drv, Control pnlX)
            {
                object objX = null;
                string strX = "";
                string strFld = "";
                string[] strArrName;
                char[] chrArrSpliter = { '_' };
                bool bIsResult = false;
                try
                {
                    foreach (Control ctrlX in pnlX.Controls)
                    {
                        objX = ctrlX.Tag;
                        if (objX != null)
                        {
                            strX = objX.ToString();
                            strArrName = ctrlX.Name.Trim().Split(chrArrSpliter);
                            if (strArrName.Length == 1) continue; //�˳���ǰѭ��������һ��ѭ��
                            strFld = strArrName[strArrName.Length - 1];
                            try
                            {
                                switch (strX.ToUpper())
                                {
                                    case "NULL":
                                        break;
                                    case "":
                                        break;
                                    case "0":
                                        ((TextBox)ctrlX).Text = drv[strFld].ToString();
                                        break;
                                    case "1":
                                        ((ComboBox)ctrlX).Text = drv[strFld].ToString();
                                        break;
                                    case "101":
                                        if (drv[strFld] == null)
                                        {
                                            ((ComboBox)ctrlX).SelectedIndex = -1;
                                        }
                                        else
                                        {
                                            SetCombBoxValue(drv[strFld].ToString(), (ComboBox)ctrlX);
                                        }
                                        break;
                                    case "102":
                                        if (drv[strFld] != null && drv[strFld].ToString() !="")
                                        {
                                            ((ComboBox)ctrlX).SelectedIndex = int.Parse(drv[strFld].ToString());
                                        }
                                        else
                                        {
                                            ((ComboBox)ctrlX).SelectedIndex = -1;
                                        }
                                        //SetCombBoxValue(drv[strFld].ToString(), (ComboBox)ctrlX);
                                        break;
                                    case "2":
                                        if (drv[strFld] == null || drv[strFld].ToString() == "")
                                        {
                                            ((DateTimePicker)ctrlX).Text = "1800-01-01";
                                        }
                                        else
                                        {
                                            ((DateTimePicker)ctrlX).Value = DateTime.Parse(drv[strFld].ToString());
                                        }
                                        break;
                                    case "3":
                                        ((CheckBox)ctrlX).Checked = bool.Parse(drv[strFld].ToString());
                                        break;
                                    case "4":
                                        ((RadioButton)ctrlX).Checked = bool.Parse(drv[strFld].ToString());
                                        break;
                                    case "5":
                                        //((ListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "6":
                                        //((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "7":
                                        //((DataGridView)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "8":
                                        //((DataGridView)ctrlX).DataSource = bsX;
                                        break;
                                    case "9":
                                        //((RichTextBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "10":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "11":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;

                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(ctrlX.Name + " DataRowViewToUI :" + e.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    bIsResult = true;
                }
                catch (Exception e)
                {
                    bIsResult = false;
                }
                return (bIsResult);
            }
            
            /// <summary>
            /// ��pnlX�������Ӧ�����ݿؼ�ֵ ��ֵ �� DataRowView ��Ӧ���ֶ�ֵ �ؼ������� ctrlType_DataFieldName (�ֶ������ܰ����»���_)
            /// ע�⣬�ؼ�������������Tag����ֵ���֣�
            /// 0 TextBox��Text 1:ComboBox��Text 101:ComboBox��SelectedValue 102:ComboBox��SelectedIndex 
            /// 2:DateTimePicker 3:CheckBox 4:RadioButton 5:ListBox 6:CheckedListBox 8:DataGridView 9:RichTextBox 
            /// </summary>
            /// <param name="drv"></param>
            /// <param name="pnlX"></param>
            /// <returns></returns>
            public virtual bool UIToDataRowView(DataRowView drv, Control pnlX)
            {
                object objX = null;
                string strX = "";
                string strFld = "";
                string[] strArrName;
                char[] chrArrSpliter = { '_' };
                bool bIsResult = false;
                if ((!drv.IsEdit) && (!drv.IsNew)) drv.BeginEdit();
                try
                {
                    foreach (Control ctrlX in pnlX.Controls)
                    {
                        objX = ctrlX.Tag;
                        if (objX != null)
                        {
                            strX = objX.ToString();
                            strArrName = ctrlX.Name.Trim().Split(chrArrSpliter);
                            if (strArrName.Length == 1) continue; //�˳���ǰѭ��������һ��ѭ��
                            strFld = strArrName[strArrName.Length - 1];
                            try
                            {
                                object objValue = null;
                                switch (strX.ToUpper())
                                {
                                    case "NULL":
                                        break;
                                    case "0":
                                        objValue = ((TextBox)ctrlX).Text;
                                        if (objValue != null )
                                            drv[strFld] = objValue.ToString();
                                        break;
                                    case "1":
                                        objValue = ((ComboBox)ctrlX).Text;
                                        if (objValue != null)
                                            drv[strFld] = objValue.ToString() ;
                                        break;
                                    case "101":
                                        objValue = ((ComboBox)ctrlX).SelectedValue;
                                        if (objValue != null)
                                            drv[strFld] = ((ComboBox)ctrlX).SelectedValue ;
                                        break;
                                    case "102":
                                        drv[strFld] = ((ComboBox)ctrlX).SelectedIndex ;
                                        break;
                                    case "2":
                                        drv[strFld] = ((DateTimePicker)ctrlX).Value ;
                                        break;
                                    case "3":
                                        drv[strFld] = ((CheckBox)ctrlX).Checked ;
                                        break;
                                    case "4":
                                        drv[strFld] = ((RadioButton)ctrlX).Checked;
                                        break;
                                    case "5":
                                        //((ListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "6":
                                        //((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "7":
                                        //((DataGridView)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "8":
                                        //((DataGridView)ctrlX).DataSource = bsX;
                                        break;
                                    case "9":
                                        //((RichTextBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "10":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "11":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;

                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(ctrlX.Name + " UIToDataRowView :" + e.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    bIsResult = true;
                    drv.EndEdit();
                }
                catch (Exception e)
                {
                    bIsResult = false;
                }
                return (bIsResult);
            }
            
            /// <summary>
            /// ��� pnlX ����������ݿؼ�������
            /// </summary>
            /// <param name="pnlX">���ݿؼ�������</param>
            /// <returns>ִ�н���Ƿ�ɹ�</returns>
            public virtual bool ClearUIValues(Control pnlX)
            {
                object objX = null;
                string strX = "";
                string strFld = "";
                string[] strArrName;
                char[] chrArrSpliter = { '_' };
                bool bIsResult = false;
                try
                {
                    foreach (Control ctrlX in pnlX.Controls)
                    {
                        objX = ctrlX.Tag;
                        if (objX != null)
                        {
                            strX = objX.ToString();
                            strArrName = ctrlX.Name.Trim().Split(chrArrSpliter);
                            if (strArrName.Length == 1) continue; //�˳���ǰѭ��������һ��ѭ��
                            strFld = strArrName[strArrName.Length - 1];
                            try
                            {
                                switch (strX.ToUpper())
                                {
                                    case "NULL":
                                        break;
                                    case "0":
                                        ((TextBox)ctrlX).Text = "";
                                        break;
                                    case "1":
                                        ((ComboBox)ctrlX).SelectedIndex  = -1;
                                        ctrlX.Text = "";
                                        break;
                                    case "101":
                                        ((ComboBox)ctrlX).SelectedIndex = -1;
                                        ctrlX.Text = "";
                                        break;
                                    case "102":
                                        ((ComboBox)ctrlX).SelectedIndex = -1;
                                        ctrlX.Text = "";
                                        break;
                                    case "2":
                                        ((DateTimePicker)ctrlX).Text ="";
                                        break;
                                    case "3":
                                        ((CheckBox)ctrlX).Checked = false  ;
                                        break;
                                    case "4":
                                        ((RadioButton)ctrlX).Checked =  false ;
                                        break;
                                    case "5":
                                        //((ListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "6":
                                        //((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "7":
                                        //((DataGridView)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "8":
                                        //((DataGridView)ctrlX).DataSource = bsX;
                                        break;
                                    case "9":
                                        //((RichTextBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "10":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "11":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;

                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(ctrlX.Name + " ClearUIValues :" + e.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    bIsResult = true;
                }
                catch (Exception e)
                {
                    bIsResult = false;
                }
                return (bIsResult);
            }

            public void ChangeTextBoxBkColorByReadOnly(object sender, Color clrReadOnly, Color clrCanWrite)
            {
                try
                {
                    TextBox txtBox = (TextBox)sender;
                    if (txtBox.ReadOnly)
                        txtBox.BackColor = this._TextBackColorDisable;
                    else
                        txtBox.BackColor = _TextBackColorEnable;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, Text);
                }
            }
            
            /// <summary>
            /// �ж��ַ����Ƿ�Ϊ��ֵ����
            /// </summary>
            /// <param name="sX">��ֵ�ַ���</param>
            /// <returns>�����Ƿ�</returns>
            public static bool IsNumberic(string sX)
            {
                bool bX = false;
                double nX = -0;
                if (sX == null) return(false);
                if (sX.Trim() == "") return(false);
                try
                {
                    nX = double.Parse(sX);
                    bX = true;
                }
                catch (Exception err)
                {
                    sX = "�Ƿ���ֵ";
                }
                return (bX);
            }
            
            /// <summary>
            /// �ж��ַ����Ƿ�Ϊ��������
            /// </summary>
            /// <param name="sX">��ֵ�ַ���</param>
            /// <returns>�����Ƿ�</returns>
            public static bool IsInteger(string sX)
            {
                    bool bX = false;
                    Int64 nX = -0;
                    if (sX == null) return(false);
                    if (sX.Trim() == "") return(false);
                    try
                    {
                        nX =Int64.Parse(sX);
                        bX = true;
                    }
                    catch (Exception err)
                    {
                        sX = "�Ƿ���ֵ";
                    }
                    return (bX);
            }
            
            /// <summary>
            /// �ж��ַ������Ƿ�Ϊ��������
            /// </summary>
            /// <param name="sX">�����ַ���</param>
            /// <returns></returns>
            public static bool IsDateTime(string sX)
                {
                    bool bX = false;
                    DateTime dX = DateTime.Now ;
                    if (sX == null) return(false);
                    if (sX.Trim() == "") return(false);
                    try
                    {
                        dX = DateTime.Parse(sX);
                        bX = true;
                    }
                    catch (Exception err)
                    {
                        sX = "�Ƿ�����ʱ����ֵ";
                    }
                    return (bX);
                }

            public bool ReadConfigXMLValue(string sFile, string sPath, string sNodeName, string sAttribute, string sDefaultValue, out string sValue)
            {
                bool bOK = false;
                sValue = "";
                if (!System.IO.File.Exists(sFile))
                {
                    MessageBox.Show("�Բ��𣬸��ļ������ڣ�" + sFile);
                    return bOK;
                }
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(sFile);
                }
                catch (Exception err)
                {
                    MessageBox.Show("�Բ��𣬼����ļ���" + sFile + " ʱ����" + err.Message);
                    return false;
                }
                XmlNodeList nodLst = null;
                string sX = sPath;
                if (sX[sX.Length - 1] == '/')
                {
                    sX = sX.Substring(0, sX.Length - 1);
                }

                nodLst = doc.SelectNodes(sX);
                if (nodLst == null)
                {
                    MessageBox.Show("�Բ���ָ��·������!");
                    return false;
                }
                string sNameSpace = doc.GetNamespaceOfPrefix("");
                XmlNode nodX = null;
                nodX = doc.SelectSingleNode(sX + "/" + sNodeName);
                if (nodX == null)
                {
                    nodX = doc.CreateNode(XmlNodeType.Element, sNodeName, sNameSpace);
                    if (sAttribute.Trim().Length == 0)
                    {
                        nodX.InnerText = sDefaultValue;
                    }
                    else
                    {
                        XmlAttribute xmlattr = doc.CreateAttribute(sAttribute);
                        xmlattr.Value = sDefaultValue;
                        nodX.Attributes.Append(xmlattr);
                    }
                    XmlNode ndP = doc.SelectSingleNode(sPath);
                    if (ndP != null)
                    {
                        ndP.AppendChild(nodX);
                    }

                    doc.Save(sFile);
                    sValue = sDefaultValue;
                    bOK = true;
                }
                else
                {
                    if (sAttribute.Trim().Length > 0)
                    {
                        XmlAttribute xmlattr = nodX.Attributes[sAttribute];
                        if (xmlattr == null)
                        {
                            xmlattr = doc.CreateAttribute(sAttribute);
                            xmlattr.Value = sDefaultValue;
                            nodX.Attributes.Append(xmlattr);
                            doc.Save(sFile);
                            sValue = sDefaultValue;
                            bOK = true;
                        }
                        else
                        {
                            sValue = xmlattr.Value;
                            bOK = true;
                        }
                    }
                    else
                    {
                        sValue = nodX.InnerText;
                        bOK = true;
                    }
                }
                return bOK;
            }

            public bool WriteConfigXMLValue(string sFile, string sPath, string sNodeName, string sAttribute, string sValue)
            {
                bool bOK = false;
                if (!System.IO.File.Exists(sFile))
                {
                    MessageBox.Show("�Բ��𣬸��ļ������ڣ�" + sFile);
                    return bOK;
                }
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(sFile);
                }
                catch (Exception err)
                {
                    MessageBox.Show("�Բ��𣬼����ļ���" + sFile + " ʱ����" + err.Message);
                    return false;
                }
                XmlNodeList nodLst = null;
                string sX = sPath;
                if (sX[sX.Length - 1] == '/')
                {
                    sX = sX.Substring(0, sX.Length - 1);
                }

                nodLst = doc.SelectNodes(sX);
                if (nodLst == null)
                {
                    MessageBox.Show("�Բ���ָ��·������!");
                    return false;
                }
                string sNameSpace = doc.GetNamespaceOfPrefix("");
                XmlNode nodX = null;
                nodX = doc.SelectSingleNode(sX + "/" + sNodeName);
                if (nodX == null)
                {
                    nodX = doc.CreateNode(XmlNodeType.Element, sNodeName, sNameSpace);
                    if (sAttribute.Trim().Length == 0)
                    {
                        nodX.InnerText = sValue;
                    }
                    else
                    {
                        XmlAttribute xmlattr = doc.CreateAttribute(sAttribute);
                        xmlattr.Value = sValue;
                        nodX.Attributes.Append(xmlattr);
                    }
                    XmlNode ndP = doc.SelectSingleNode(sPath);
                    if (ndP != null)
                    {
                        ndP.AppendChild(nodX);
                    }

                    doc.Save(sFile);
                    bOK = true;
                }
                else
                {
                    if (sAttribute.Trim().Length > 0)
                    {
                        XmlAttribute xmlattr = nodX.Attributes[sAttribute];
                        if (xmlattr == null)
                        {
                            xmlattr = doc.CreateAttribute(sAttribute);
                            //xmlattr.Value = sValue; //������
                            xmlattr.InnerText = sValue;
                            nodX.Attributes.Append(xmlattr);
                        }
                        else
                        {
                            //xmlattr.Value = sValue; //������
                            xmlattr.InnerText = sValue;
                        }
                    }
                    else
                    {
                        nodX.InnerText = sValue;
                        bOK = true;
                    }
                    doc.Save(sFile);
                    bOK = true;
                }
                return bOK;
            }
            
            /// <summary>
            /// ���¼�����ݵ������ԣ�����¼�������������
            /// </summary>
            /// <param name="drv">DataRowView ʵ��</param>
            /// <param name="pnlX">�༭�ռ�����</param>
            /// <param name="sMustInputFlds">����¼����ֶ��ַ������á������ָ�</param>
            /// <param name="sErr">������ʾ</param>
            /// <returns></returns>
            public bool CheckInputDataValues(DataRowView drv, Control pnlX, string sMustInputFlds,out string sErr)
            {
                object objX = null;
                string strX = "";
                string strFld = "";
                string[] strArrName;
                char[] chrArrSpliter = { '_' };
                bool bIsResult = true ;
                DataColumn col = null ;
                sErr = "";
                if (drv == null)
                {
                    sErr = "DataRowView ����Ϊ�գ���������������ʧ�ܣ�";
                    return false;
                }
                if (pnlX == null)
                {
                    sErr = "����ؼ����� ����Ϊ�գ���������������ʧ�ܣ�";
                    return false;
                }
                try
                {
                    #region foreach control  �ж��������� ���� �����������Ƿ�ƥ��
                    foreach (Control ctrlX in pnlX.Controls)
                    {
                        objX = ctrlX.Tag;
                        if (objX != null)
                        {
                            strX = objX.ToString();
                            strArrName = ctrlX.Name.Trim().Split(chrArrSpliter);
                            if (strArrName.Length == 1) continue; //�˳���ǰѭ��������һ��ѭ��
                            strFld = strArrName[strArrName.Length - 1];
                            try
                            {
                                #region swich
                                object objValue = null;
                                switch (strX.ToUpper())
                                {
                                    case "NULL":
                                        break;
                                    case "0":
                                        objValue = ((TextBox)ctrlX).Text;
                                        if (objValue != null)
                                        {
                                            col = drv.Row.Table.Columns[strFld] ;
                                            if(col == null)
                                            {
                                                sErr = strFld +" �ֶ� �����ڣ�";
                                                return false ;
                                            }
                                            string sX= objValue.ToString();
                                            if (col.DataType == System.Type.GetType("System.Double") || col.DataType == System.Type.GetType("System.Int32"))
                                            {
                                                if (IsNumberic(sX) == false && IsInteger(sX) == false)
                                                {
                                                    sErr = strFld +  " �������Ͳ�ƥ�䣬��������ȷ����ֵ��";
                                                    ((TextBox)ctrlX).SelectAll();
                                                    ((TextBox)ctrlX).Focus();
                                                    return false;
                                                }
                                            }
                                        }                            
                                        break;
                                    case "1":
                                        objValue = ((ComboBox)ctrlX).Text;
                                        if (objValue != null)
                                        {
                                            col = drv.Row.Table.Columns[strFld];
                                            if (col == null)
                                            {
                                                sErr = strFld + " �ֶ� �����ڣ�";
                                                return false;
                                            }
                                            string sX = objValue.ToString();
                                            if (col.DataType == System.Type.GetType("System.Double") || col.DataType == System.Type.GetType("System.Int32"))
                                            {
                                                if (IsNumberic(sX) == false && IsInteger(sX) == false)
                                                {
                                                    sErr = strFld + " �������Ͳ�ƥ�䣬��������ȷ����ֵ��";
                                                    ((ComboBox)ctrlX).SelectAll();
                                                    ((ComboBox)ctrlX).Focus();
                                                    return false;
                                                }
                                            }
                                        }
                                        break;
                                    case "101":
                                        objValue = ((ComboBox)ctrlX).SelectedValue;
                                        if (objValue != null)
                                        {
                                            col = drv.Row.Table.Columns[strFld];
                                            if (col == null)
                                            {
                                                sErr = strFld + " �ֶ� �����ڣ�";
                                                return false;
                                            }
                                            string sX = objValue.ToString();
                                            if (col.DataType == System.Type.GetType("System.Double") || col.DataType == System.Type.GetType("System.Int32"))
                                            {
                                                if (IsNumberic(sX) == false && IsInteger(sX) == false)
                                                {
                                                    sErr = strFld + " �������Ͳ�ƥ�䣬��������ȷ����ֵ��";
                                                    ((ComboBox)ctrlX).SelectAll();
                                                    ((ComboBox)ctrlX).Focus();
                                                    return false;
                                                }
                                            }
                                        }
                                        break;
                                    case "102":
                                        //objValue = ((ComboBox)ctrlX).SelectedValue;
                                        break;
                                    case "2":
                                        objValue =(object) ((DateTimePicker)ctrlX).Value;
                                        //drv[strFld] = ((DateTimePicker)ctrlX).Value;
                                        if (objValue != null)
                                        {
                                            col = drv.Row.Table.Columns[strFld];
                                            if (col == null)
                                            {
                                                sErr = strFld + " �ֶ� �����ڣ�";
                                                ((DateTimePicker)ctrlX).Focus();
                                                return false;
                                            }
                                            string sX = objValue.ToString();
                                            if (col.DataType == System.Type.GetType("System.DateTime"))
                                            {
                                                if (IsDateTime(sX) == false)
                                                {
                                                    sErr = strFld + " �������Ͳ�ƥ�䣬��������ȷ������ֵ��";
                                                    ((DateTimePicker)ctrlX).Focus();
                                                    return false;
                                                }
                                            }
                                        }
                                        break;
                                    case "3":
                                        //drv[strFld] = ((CheckBox)ctrlX).Checked;
                                        break;
                                    case "4":
                                        //drv[strFld] = ((RadioButton)ctrlX).Checked;
                                        break;
                                    case "5":
                                        //((ListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "6":
                                        //((CheckedListBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "7":
                                        //((DataGridView)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "8":
                                        //((DataGridView)ctrlX).DataSource = bsX;
                                        break;
                                    case "9":
                                        //((RichTextBox)ctrlX).DataBindings.Add(new Binding("Text", bsX, strFld));
                                        break;
                                    case "10":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;
                                    case "11":
                                        //((CheckBox)ctrlX).Enabled = bIsEdit;
                                        break;

                                }
                                #endregion
                            }
                            catch (Exception e)
                            {
                                sErr = e.Message;
                                MessageBox.Show(ctrlX.Name + " CheckInputDataValues :" + e.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bIsResult = false;
                                return bIsResult;
                            }
                        }
                    }
                    #endregion

                    #region �жϱ���¼����
                        char[] arrSpliter = {','};
                        string[] arrFlds = null;
                        arrFlds = sMustInputFlds.Split(arrSpliter);
                        if (arrFlds.Length > 0)
                        {
                            foreach (string sFld in arrFlds)
                            {
                                objX = drv[sFld];
                                if (objX == null)
                                {
                                    sErr = sFld + " �ֶβ����ڣ�";
                                    return false;
                                }
                                if (objX.ToString() == "")
                                {
                                    sErr = sFld + " �ֶ�ֵ����Ϊ�գ�";
                                    return false;
                                }
                            }
                        }
                    #endregion

                }
                catch (Exception e)
                {
                    sErr = e.Message;
                    bIsResult = false;
                }
                return (bIsResult);
            }
            
            public void  SaveFormUIConifg(Form frmX, string sPath)
            {
                string sSourcePath = sPath;
                string sFile = frmX.ProductName + "\\" + frmX.Name + ".xml";
                #region �ļ�·��
                if (sSourcePath.Trim() == "")
                {
                    sSourcePath = Application.StartupPath;
                }
                if (!sSourcePath.EndsWith("\\"))
                {
                    sSourcePath += "\\";
                }
                sSourcePath = sSourcePath + "UISource\\FormCfg\\" + sFile;
                #endregion 
                MessageBox.Show(sSourcePath);
                #region ��Ⲣ�����ļ�
                if (!File.Exists(sSourcePath))
                {

                    DirectoryInfo dir = Directory.GetParent(sSourcePath);
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    MessageBox.Show("·����" + dir.ToString() + " �����ɹ���");
                    FileStream fs = File.Create(sSourcePath);
                    {
                        string sX = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "\r\n" + "<" + frmX.Name + ">" + "\r\n" + "</" + frmX.Name + ">";
                        byte[] arr = null;
                        arr = System.Text.UnicodeEncoding.Unicode.GetBytes(sX);
                        fs.Write(arr, 0, arr.Length);
                        fs.Flush();
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                    MessageBox.Show("�ļ���" + sSourcePath + " �����ɹ���");
                }
                #endregion

                #region ��������
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    MessageBox.Show(sSourcePath + " ��ʼ����XML�ļ�");
                    xmlDoc.Load(sSourcePath);
                    MessageBox.Show(sSourcePath + " ����XML�ļ��ɹ�");
                    XmlNode ndParent = xmlDoc.SelectSingleNode(frmX.Name);
                    if (ndParent == null)
                    {
                        MessageBox.Show("��ʼ�����ڵ�");
                        ndParent = xmlDoc.CreateNode(XmlNodeType.Element, "", frmX.Name, "");
                        MessageBox.Show("�����ڵ�ɹ�");
                        XmlAttribute att = null;
                        att = xmlDoc.CreateAttribute("FormName");
                        att.Value = frmX.Name;
                        ndParent.Attributes.Append(att);
                        xmlDoc.AppendChild(ndParent);
                    }
                    SaveCtrolAttrsToXmlNode(frmX.Name,(Control) frmX, ndParent);
                    xmlDoc.Save(sSourcePath);
                    xmlDoc = null;
                }
                catch (Exception err)
                {
                    MessageBox.Show("����XML�ļ�ʱ����"+err.Message);
                    xmlDoc = null;
                }
                #endregion
            }
            public void SetFormUIFromConfig(Form frmX, string sPath)
            {
                string sSourcePath = sPath;
                string sFile = frmX.ProductName + "\\" + frmX.Name + ".cfg";
                string sSourcePathPic = sPath;
                #region �ļ�·��
                if (sSourcePath.Trim() == "")
                {
                    sSourcePath = Application.StartupPath;
                    sSourcePathPic = sSourcePath;
                }
                if (!sSourcePath.EndsWith("\\"))
                {
                    sSourcePath += "\\";
                    sSourcePathPic = sSourcePath;
                }
                sSourcePath = sSourcePath + "UISource\\FormCfg\\" + sFile;
                sSourcePathPic = sSourcePathPic + "UISource\\Pic";
                #endregion 
                if (!File.Exists(sSourcePath)) return;
                #region ��ȡ���ò����ÿؼ�����
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.Load(sSourcePath);
                    XmlNode ndParent = xmlDoc.SelectSingleNode(frmX.Name);
                    if (ndParent == null)
                    {
                        return;
                    }
                    SetCtrolAttrsFromXmlNode(sSourcePathPic, frmX.Name, (Control)frmX, ndParent);
                    xmlDoc = null;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    xmlDoc = null;
                }
                #endregion
            }
            public void SaveCtrolAttrsToXmlNode(string sParentNodePath, Control ctrlCurr, XmlNode ndParent)
            {
                if (ctrlCurr != null && ndParent != null)
                {
                    string sPath = sParentNodePath + "/" + ctrlCurr.Name;
                    XmlDocument doc = null;
                    XmlNode ndCurr = null ;
                    doc = ndParent.OwnerDocument;
                    ndCurr = doc.SelectSingleNode(sPath);
                    if (ndCurr == null)
                    {
                        ndCurr = doc.CreateNode(XmlNodeType.Element, ctrlCurr.Name, "");
                        ndParent.AppendChild(ndCurr);
                    }
                    //--��������
                    #region
                    XmlAttribute attr = null;
                    string AttrName = "";
                    string AttrValue = "";
  
                    #region ctrlClass
                    AttrName = "ctrlClass";
                    AttrValue = ctrlCurr.GetType().Name;
                    MessageBox.Show(AttrValue);
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    MessageBox.Show(AttrValue + " ��ӳɹ���");
                    #endregion
                    #region ctrlText
                    AttrName = "ctrlText";
                    AttrValue = ctrlCurr.Text;
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    #endregion
                    #region ctrlVisible
                    AttrName = "ctrlVisible";
                    AttrValue = ctrlCurr.Visible.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    #endregion
                    #region ctrlWidth
                    AttrName = "ctrlWidth";
                    AttrValue = ctrlCurr.Width.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    #endregion
                    #region ctrlHeight
                    AttrName = "ctrlHeight";
                    AttrValue = ctrlCurr.Height.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    #endregion
                    #region ctrlBKColor
                    AttrName = "ctrlBKColor";
                    AttrValue = ctrlCurr.BackColor.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    #endregion
                    #region ctrlBKPic
                    AttrName = "ctrlBKPic";
                    AttrValue = "";
                    if (ctrlCurr.BackgroundImage != null)
                    {
                        if (ctrlCurr.BackgroundImage.Tag != null)
                        {
                            AttrValue = ctrlCurr.BackgroundImage.Tag.ToString();
                        }
                    }
                    attr = ndCurr.Attributes[AttrName];
                    if (attr == null)
                    {
                        attr = doc.CreateAttribute(AttrName);
                        ndCurr.Attributes.Append(attr);
                    }
                    attr.Value = AttrValue;
                    #endregion
                    #endregion
                    //--�ݹ����
                    if (ctrlCurr.Controls.Count > 0)
                    {
                        foreach (Control ctrlX in ctrlCurr.Controls)
                        {
                            SaveCtrolAttrsToXmlNode(sPath, ctrlX, ndCurr);
                        }
                    }
                }
            }
            public void SetCtrolAttrsFromXmlNode(string sSourePath, string sParentNodePath, Control ctrlCurr, XmlNode ndParent)
            {
                if (ctrlCurr != null && ndParent != null)
                {
                    string sPath = sParentNodePath + "/" + ctrlCurr.Name;
                    XmlDocument doc = null;
                    XmlNode ndCurr = null;
                    doc = ndParent.OwnerDocument;
                    ndCurr = doc.SelectSingleNode(sPath);
                    if (ndCurr == null) return;
                    //--��������
                    #region
                    XmlAttribute attr = null;
                    string AttrName = "";
                    string AttrValue = "";

                    #region ctrlClass
                    AttrName = "ctrlClass";
                    AttrValue = ctrlCurr.GetType().Name;
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null)
                    {
                        //
                    }
                    #endregion
                    #region ctrlText
                    AttrName = "ctrlText";
                    AttrValue = ctrlCurr.Text;
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null)
                    {
                        if (AttrValue != attr.Value)
                        {
                            ctrlCurr.Text = attr.Value;
                        }
                    }
                    #endregion
                    #region ctrlVisible
                    AttrName = "ctrlVisible";
                    AttrValue = ctrlCurr.Visible.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null && attr.Value.Trim() !="")
                    {
                        bool bX = Convert.ToBoolean(attr.Value);
                        if (ctrlCurr.Visible != bX)
                        {
                            ctrlCurr.Visible = bX;
                        }
                    }
                    #endregion
                    #region ctrlWidth
                    AttrName = "ctrlWidth";
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null && attr.Value.Trim() != "")
                    {
                        if (ctrlCurr.Width.ToString() != attr.Value.Trim())
                        {
                            ctrlCurr.Width = Convert.ToInt32(attr.Value.Trim());
                        }
                    }
                    #endregion
                    #region ctrlHeight
                    AttrName = "ctrlHeight";
                    AttrValue = ctrlCurr.Height.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null && attr.Value.Trim() != "")
                    {
                        if (ctrlCurr.Height.ToString() != attr.Value.Trim())
                        {
                            ctrlCurr.Height = Convert.ToInt32(attr.Value.Trim());
                        }
                    }
                    #endregion
                    #region ctrlBKColor
                    AttrName = "ctrlBKColor";
                    AttrValue = ctrlCurr.BackColor.ToString();
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null && attr.Value.Trim() != "")
                    {
                        if (ctrlCurr.BackColor.ToString() != attr.Value.Trim())
                        {
                            ctrlCurr.BackColor = Color.FromName(attr.Value.Trim());
                        }
                    }
                    #endregion
                    #region ctrlBKPic
                    AttrName = "ctrlBKPic";
                    AttrValue = "";
                    attr = ndCurr.Attributes[AttrName];
                    if (attr != null && attr.Value.Trim() != "")
                    {
                        #region
                        string sFile = sSourePath;
                        if (sFile.IndexOf("\\") <= 0)
                        {
                            sFile += "\\";
                        }
                        sFile += attr.Value.Trim();
                        if(File.Exists(sFile))
                        {
                            try
                            {
                                if (ctrlCurr.BackgroundImage != null)
                                {
                                    ctrlCurr.BackgroundImage.Dispose();
                                    ctrlCurr.BackgroundImage = null;
                                }
                                ctrlCurr.BackgroundImage = Image.FromFile(sFile);
                                ctrlCurr.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show(err.Message);
                            }
                        }
                        #endregion
                    }
                    #endregion
                    #endregion
                    //--�ݹ����
                    if (ctrlCurr.Controls.Count > 0)
                    {
                        foreach (Control ctrlX in ctrlCurr.Controls)
                        {
                            SetCtrolAttrsFromXmlNode(sSourePath, sPath, ctrlX, ndCurr);
                        }
                    }
                }
            }

        #endregion

        #region �̳��¼�
            protected override void OnKeyDown(KeyEventArgs e)
            {
                if (_IsEnterAsTabKey)
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        SendKeys.Send("{Tab}");
                        e.Handled = true;
                        return;
                    }
                }
                base.OnKeyDown(e);
            }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_AppInformation != null)
            {
                Icon = _AppInformation.AppICON;
            }
            
        }
        #endregion

        public FrmSTable()
        {
            InitializeComponent();
            _IsEnterAsTabKey = true;
            _DBDataSet = new DataSet();
          
        }

        
    }


    
}