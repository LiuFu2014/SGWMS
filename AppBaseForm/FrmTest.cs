using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmTest : Form  
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            stbDateTime.Text = "��ʱ�䡿" + DateTime.Now.ToShortDateString();
        }
    }
}