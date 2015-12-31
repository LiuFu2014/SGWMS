using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace RF.Server
{
    partial class WMSService : ServiceBase
    {
        public WMSService()
        {
            InitializeComponent();
            tm.Elapsed += new System.Timers.ElapsedEventHandler(tm_ElapsedEvent);
        }

        Thread threadForm;
        System.Timers.Timer tm = new System.Timers.Timer(500);
        bool Is_stop;

        protected override void OnStart(string[] args)
        {
            threadForm = new Thread(new ThreadStart(FormShow));
            threadForm.Start();
            tm.Start();
        }

        void FormShow()
        {
            frmMain frm = new frmMain();
            frm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormClosed);
            System.Windows.Forms.Application.Run(frm);
        }

        void FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Is_stop = true;
        }

        void tm_ElapsedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Is_stop)
                this.Stop();
        }

        protected override void OnStop()
        {
            if (threadForm != null)
            {
                if (threadForm.IsAlive)
                {
                    threadForm.Abort();
                    threadForm = null;
                }
            }
        }
    }
}
