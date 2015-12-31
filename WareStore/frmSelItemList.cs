using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WareStoreMS
{
    public partial class frmSelItemList : UI.FrmSTable
    {
        #region ˽�б���
            string[] ArrKeyList = null;
        #endregion

        #region ˽�з���
            private void LoadItemList()
            {
                
                chkList.Items.Clear();
                if (_TableItem == null)
                    return;
                if (_TableItem.Rows.Count == 0)
                    return;

                Cursor.Current = Cursors.WaitCursor;
                chkList.BeginUpdate();
                ArrKeyList = new string[_TableItem.Rows.Count];
                for (int iX = 0; iX < _TableItem.Rows.Count;iX ++ )
                {
                    DataRow dr = _TableItem.Rows[iX];
                    chkList.Items.Add(dr[_FldDesc].ToString(), false);
                    ArrKeyList[iX] = dr[_FldKey].ToString();
                }
                chkList.EndUpdate();
                Cursor.Current = Cursors.Default;
            }

            private void SetItemCheckAll(bool bIsChecked)
            {
                if (chkList.Items.Count == 0) return ;
                Cursor.Current = Cursors.WaitCursor;
                chkList.BeginUpdate();
                for (int iX = 0;iX < chkList.Items.Count ;iX ++)
                {
                    chkList.SetItemChecked(iX, bIsChecked);
                }
                chkList.EndUpdate();
                Cursor.Current = Cursors.Default;
            }

            private int GetSelectResult()
            {
                int nResult = 0;
                _SelectItemList = "";
                _SelectKeyList = "";
                bool bIsFirst = true;
                if (chkList.Items.Count == 0) return 0;
                for (int iX = 0; iX < chkList.Items.Count; iX++)
                {
                    if (chkList.GetItemChecked(iX))
                    {
                        if (bIsFirst)
                        {
                            _SelectItemList = ""+chkList.Items[iX].ToString().Trim() + "";
                            _SelectKeyList = ""+ArrKeyList[iX].Trim()+"";
                            bIsFirst = false;
                        }
                        else
                        {
                            _SelectItemList = _SelectItemList + "," + "" + chkList.Items[iX].ToString().Trim() + "";
                            _SelectKeyList = _SelectKeyList + "," + "" + ArrKeyList[iX].Trim() + "";
                        }
                        nResult++;
                    }
                }
                return nResult;
            }
        #endregion

        #region ��������

        private string _SelectItemList = "";
            public  string SelectItemList
            {
                get { return _SelectItemList.Trim(); }
            }

            private string _FldKey = "";
            public string FldKey
            {
                set  { _FldKey = value.Trim(); }
            }

            private string _FldDesc = "";
            public string FldDesc
            {
                set  { _FldDesc = value.Trim(); }
            }
            private string _SelectKeyList = "";
            public string SelectKeyList
            {
                get { return _SelectKeyList.Trim(); }
            }

            private bool _IsSelected = false;
            public bool IsSelected
            {
                get { return _IsSelected; }            
            }

            private string _TitleText = "����ѡ��";
            public string TitleText
            {
                get { return _TitleText.Trim(); }
                set 
                { 
                    _TitleText = value.Trim();
                    Text = _TitleText;
                }
            }

            private DataTable _TableItem = null;
            public DataTable TableItem
            {
                set { _TableItem = value; }
            }
        #endregion

        #region ��������

        #endregion
        public frmSelItemList()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int nX = GetSelectResult();
            if (nX > 20)
            {
                MessageBox.Show("�Բ�����಻�ܳ���20�����ݣ�");
                return;
            }
            if (_SelectItemList.Trim() == "")
            {
                if (MessageBox.Show("û��ѡ�����ݣ���Ҫ�˳���", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("��ȷ����ѡ����ǣ�" + _SelectItemList + " ��", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
            }
            _IsSelected = true;
            Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            _IsSelected = false;
            Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            SetItemCheckAll(chkAll.Checked);
        }

        private void frmSelItemList_Load(object sender, EventArgs e)
        {
            LoadItemList();
        }

        private void chkList_Click(object sender, EventArgs e)
        {
            if (chkList.Items.Count > 0)
            {
                int iX = chkList.SelectedIndex;
                bool bX = !chkList.GetItemChecked(iX);
                chkList.SetItemChecked(iX, bX);
                chkList.Update();

            }
        }

        private void chkList_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}

