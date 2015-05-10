using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace Demo2
{
    public partial class FrmWait : Form
    {
        private IAsyncResult m_Result;
        private MyFunctionDelegate m_Delegate;

        public FrmWait(IAsyncResult result, MyFunctionDelegate d)
        {
            InitializeComponent();
            m_Result = result;
            m_Delegate = d;
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
                //this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AsyncResult asyncResult = m_Result as AsyncResult;
            MyFunctionDelegate d = asyncResult.AsyncDelegate as MyFunctionDelegate;
            d.EndInvoke(m_Result);
            this.CloseForm();
        }
    }
}
