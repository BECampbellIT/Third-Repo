using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace StandAlonePackingApp
{
    public partial class BusyDialog : Form
    {
        private ManualResetEvent operationFinished;
        public delegate void Operation(BusyDialog bd);

        private Operation myOpr;
        delegate void UpdateCallback(string t);

        public BusyDialog(Operation opr)
        {
            InitializeComponent();
            myOpr = opr;
            this.Shown += BusyDialog_Shown;
        }

        private void BusyDialog_Shown(object sender, EventArgs e)
        {
            operationFinished = new ManualResetEvent(false);
            backgroundWorker1.RunWorkerAsync();
            while (!operationFinished.WaitOne(100))
            {
                Application.DoEvents();
            }
            Close();
        }

        public void SetTitle(string title)
        {
            if (this.InvokeRequired)
            {
                UpdateCallback d = new UpdateCallback(SetTitle);
                this.Invoke(d, title);
            }
            else
            {
                this.Text = title;
                txtMsg.Text = title + "...";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            myOpr(this);
            operationFinished.Set();
        }
    }
}
