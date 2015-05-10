using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Demo3;

namespace Demo2
{
    public partial class FrmProgressDefault4 : Form,IProgressView,ISpecialProgressView
    {
        private CancellationTokenSource m_CancellationTokenSource;
        private object m_Parameter;
        private System.Timers.Timer m_Timer = new System.Timers.Timer();

        public FrmProgressDefault4(ProgressType type, CancellationTokenSource cancellationTokenSource, bool cancelEnabled = true, object parameter = null)
        {
            InitializeComponent();
            m_CancellationTokenSource = cancellationTokenSource;
            m_Parameter = parameter;
            if (type == ProgressType.Percent)
            {
                this.progressBar1.Style = ProgressBarStyle.Blocks;
            }
            else if (type == ProgressType.Continuous)
            {
                this.progressBar1.Style = ProgressBarStyle.Continuous;
            }
            if (!cancelEnabled)
            {
                button1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_CancellationTokenSource.Cancel();
            //if (m_CancellationToken != null)
            //{
            //    m_CancellationToken.Abort();
            //}
            this.DialogResult = DialogResult.Cancel;
        }

        private delegate void SetMessageHandler(string msg);
        private delegate void SetProgressHandler(int value);
        private delegate void CloseFormHandler();

        private void CloseForm()
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

        public void SetMessage(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new SetMessageHandler(SetMessage), msg);
            }
            else
            {
                lblState.Text = msg;
            }
        }

        public void SetProgress(int percent)
        {
            if (InvokeRequired)
            {
                Invoke(new SetProgressHandler(SetProgress), percent);
            }
            else
            {
                progressBar1.Value = percent;
            }
        }

        private void FrmProgressDefault_Load(object sender, EventArgs e)
        {
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
            //m_Timer.AutoReset = false;
            m_Timer.Interval = 100;
            GC.KeepAlive(m_Timer);
            //if (m_CancellationTokenSource != null)
            //{
            //    if (m_Parameter != null)
            //    {
            //        m_CancellationTokenSource.Start(m_Parameter);
            //    }
            //    else
            //    {
            //        m_CancellationTokenSource.Start();
            //    }
            //    m_Timer.Enabled = true;
            //}
            //m_Timer.Enabled = true;
        }

        void m_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!m_CancellationTokenSource.IsCancellationRequested)
            {
                m_Timer.Close();
                CloseForm();
            }
        }

        void ISpecialProgressView.CloseForm()
        {
            this.CloseForm();
        }

        public void ShowDialog2()
        {
            this.ShowDialog();
        }
    }

    public enum ProgressType
    {
        Percent,    //进度式的
        Continuous, //循环式的
    }
}
