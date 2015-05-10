using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Demo2
{
    public delegate void MyFunctionDelegate(Person p);

    public partial class Form1 : Form
    {
        FrmWait frm;
        public Form1()
        {
            InitializeComponent();
        }

        //非必须，只是为了验证取消按钮的效果
        private delegate void SetFormTextHandler(string msg);
        private void SetFormText(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new SetFormTextHandler(SetFormText), msg);
            }
            else
            {
                this.Text = msg;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = "Wilson";
            p.Age = 22;
            frm = null;
            MyFunctionDelegate myDelegate = new MyFunctionDelegate(MyFunction);
            IAsyncResult result = myDelegate.BeginInvoke(p,new AsyncCallback(Completed), null);
            frm = new FrmWait(result, myDelegate);
            frm.ShowDialog();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    MessageBox.Show("OK!");
            //}
            //else
            //{
            //    MessageBox.Show("Cancelled!");
            //}
        }

        public void MyFunction(Person p)
        {
            frm.SetText("开始计算...");
            Thread.Sleep(100);
            //
            frm.SetText(string.Format("姓名：{0}；年龄：{1}", p.Name, p.Age));
            Thread.Sleep(1000);
            // Your background task goes here
            for (int i = 1; i <= 100; i++)
            {
                // Report progress to 'UI' thread
                if (frm != null)
                {
                    frm.SetText(string.Format("正在处理第{0}个，共{1}个", i, 100));
                    frm.SetProgressValue(i);
                    //this.Text = i.ToString();无效，必须使用下面的方法
                    SetFormText(i.ToString());
                }
                // Simulate long task
                System.Threading.Thread.Sleep(100);
            }
            frm.SetText("计算完成...");
        }

        private void Completed(IAsyncResult result)
        {
            if (frm != null)
            {
                frm.CloseForm();
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
