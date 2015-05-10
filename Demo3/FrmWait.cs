using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Demo2
{
    public partial class FrmWait : Form
    {
        private Thread m_Thread;

        public FrmWait(Thread thread)
        {
            InitializeComponent();
            m_Thread = thread;
        }

        void m_bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Thread.Abort();
            this.DialogResult = DialogResult.Cancel;
        }

        private delegate void SetTextHandler(string msg);
        private delegate void SetProgressValueHandler(int value);
        private delegate void CloseFormHandler();

        public void SetText(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new SetTextHandler(SetText), msg);
            }
            else
            {
                lblState.Text = msg;
            }
        }

        public void SetProgressValue(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new SetProgressValueHandler(SetProgressValue), value);
            }
            else
            {
                progressBar1.Value = value;
            }
        }

        public void CloseForm()
        {
            if (InvokeRequired)
            {
                Invoke(new CloseFormHandler(CloseForm), null);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
