using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Demo2;
using System.Threading;
using System.Threading.Tasks;

namespace Demo3
{
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

        #region 原始版
        private void button1_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = "Wilson";
            p.Age = 22;
            frm = null;
            Thread thread = new Thread(MyFunction);
            thread.IsBackground = true;
            frm = new FrmWait(thread);
            thread.Start(p);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("OK!");
            }
            else
            {
                MessageBox.Show("Cancelled!");
            }
        }

        public void MyFunction(object objP)
        {
            frm.SetText("开始计算...");
            Thread.Sleep(100);
            //
            Person p = objP as Person;
            Thread.Sleep(100);
            frm.SetText(string.Format("姓名：{0}；年龄：{1}",p.Name,p.Age));
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
            Thread.Sleep(100);
            if (frm != null)
            {
                frm.CloseForm();
            }
        }
        #endregion

        #region 标准版方法
        IProgressView view;
        private void button2_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = "Wilson";
            p.Age = 22;
            Thread thread = new Thread(Function2);
            thread.IsBackground = true;
            view = new FrmProgressDefault(ProgressType.Percent, thread,true, p);
            if (view.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("OK!");
            }
            else
            {
                MessageBox.Show("Cancelled!");
            }
        }

        public void Function2(object objP)
        {
            view.SetMessage("开始计算...");
            Thread.Sleep(100);
            //
            Person p = objP as Person;
            Thread.Sleep(100);
            view.SetMessage(string.Format("姓名：{0}；年龄：{1}", p.Name, p.Age));
            Thread.Sleep(1000);
            // Your background task goes here
            for (int i = 1; i <= 100; i++)
            {
                // Report progress to 'UI' thread
                if (view != null)
                {
                    view.SetMessage(string.Format("正在处理第{0}个，共{1}个", i, 100));
                    view.SetProgress(i);
                    //this.Text = i.ToString();无效，必须使用下面的方法
                    SetFormText(i.ToString());
                }
                // Simulate long task
                System.Threading.Thread.Sleep(100);
            }
            view.SetMessage("计算完成...");
            Thread.Sleep(100);
            //if (view != null)
            //{
            //    view.CloseForm();
            //}
        }
        #endregion

        #region 特殊版
        //ISpecialProgressView specialView;
        FrmProgressDefault specialView;
        public delegate DialogResult MyFunctionDelegate();
        //IAsyncResult result;
        private void button3_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = "Wilson";
            p.Age = 22;
            //必须是false 即不能用工作线程来关闭主线程
            specialView = new FrmProgressDefault(ProgressType.Percent, null, false, null);
            //MyFunctionDelegate myDelegate = new MyFunctionDelegate(specialView.ShowDialog);
            //result = myDelegate.BeginInvoke(new AsyncCallback(Completed), null);
            Thread thread = new Thread(specialView.ShowDialog2);
            thread.IsBackground = true;
            thread.Start();
            Function3(p);
            thread.Abort();
            //MessageBox.Show("Completed");
        }

        private void Completed(IAsyncResult result)
        {
            System.Runtime.Remoting.Messaging.AsyncResult asyncResult = result as System.Runtime.Remoting.Messaging.AsyncResult;
            MyFunctionDelegate myDelegate = (MyFunctionDelegate)asyncResult.AsyncDelegate;
            DialogResult dialogResult = myDelegate.EndInvoke(asyncResult);
            if (dialogResult == DialogResult.OK)
            {
                MessageBox.Show("OK!");
            }
            else
            {
                MessageBox.Show("Cancelled!");
            }
        }

        public void Function3(object objP)
        {
            try
            {
                specialView.SetMessage("开始计算...");
                Thread.Sleep(100);
                //
                Person p = objP as Person;
                Thread.Sleep(100);
                specialView.SetMessage(string.Format("姓名：{0}；年龄：{1}", p.Name, p.Age));
                Thread.Sleep(1000);
                // Your background task goes here
                for (int i = 1; i <= 100; i++)
                {
                    // Report progress to 'UI' thread
                    if (specialView != null)
                    {
                        specialView.SetMessage(string.Format("正在处理第{0}个，共{1}个", i, 100));
                        specialView.SetProgress(i);
                        //this.Text = i.ToString();无效，必须使用下面的方法
                        SetFormText(i.ToString());
                    }
                    // Simulate long task
                    System.Threading.Thread.Sleep(100);
                }
                specialView.SetMessage("计算完成...");
                Thread.Sleep(100);
                //if (specialView != null)
                //{
                //    specialView.CloseForm();
                //}
            }
            catch (ObjectDisposedException)//如果进度窗体有取消按钮，则必须监测该错误来中止函数
            {
                return;
            }
        }
        #endregion

        /// <summary>
        /// 使用.NET4.0的Task来执行任务，显得更加方便，且更容易控制
        /// 对于本例而言，可以使用ISpecialProgressView来定义等待窗口的接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Person p = new Person();
            p.Name = "Wilson";
            p.Age = 22;
            CancellationTokenSource cancellenTokenSource = new CancellationTokenSource();
            ISpecialProgressView view2 = new FrmProgressDefault4(ProgressType.Percent, cancellenTokenSource, true, p);
            //可以通过下面的语句提示取消，也可以使用taskCanceled来处理。
            //cancellenTokenSource.Token.Register(() => MessageBox.Show("您取消了任务的执行！"));
            Task task = Task.Factory.StartNew((object objP) =>
                {
                    view2.SetMessage("开始计算...");
                    Thread.Sleep(100);
                    //
                    Person pp = objP as Person;
                    Thread.Sleep(100);
                    view2.SetMessage(string.Format("姓名：{0}；年龄：{1}", pp.Name, pp.Age));
                    Thread.Sleep(1000);
                    // Your background task goes here
                    for (int i = 1; i <= 100; i++)
                    {
                        if (cancellenTokenSource.Token.IsCancellationRequested)
                        {
                            //使用下面的代码将通知task被取消
                            cancellenTokenSource.Token.ThrowIfCancellationRequested();
                            //使用下面的代码task则不会被通知取消
                            //return;
                        }
                        
                        if (view2 != null)
                        {
                            view2.SetMessage(string.Format("正在处理第{0}个，共{1}个", i, 100));
                            view2.SetProgress(i);
                            //使用this.Invoke来直接调用UI线程修改UI属性，可以避免InvokeRequired的判断
                            this.Invoke(new MethodInvoker(() => this.Text = i.ToString()));
                            //SetFormText(i.ToString());
                        }
                        
                        System.Threading.Thread.Sleep(100);
                    }
                    view2.SetMessage("计算完成...");
                    Thread.Sleep(100);
                    view2.CloseForm();
                }, p, cancellenTokenSource.Token);
            //使用ContinueWith添加异步任务显示信息
            Task taskOK = task.ContinueWith((antecedentTask) =>
                {
                    MessageBox.Show("执行完成！");
                    Thread.Sleep(2000);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
            Task taskCancel = task.ContinueWith(antecedentTask =>
                {
                    MessageBox.Show("执行被取消");
                    Thread.Sleep(2000);
                }, TaskContinuationOptions.OnlyOnCanceled);
            Task taskFaulted = task.ContinueWith(antecedentTask =>
                {
                    MessageBox.Show("执行出错！");
                }, TaskContinuationOptions.OnlyOnFaulted);
            view2.ShowDialog();
            Task.WaitAny(taskOK, taskFaulted,taskCancel);

            //除了上面的ContinueWith方式来异步显示信息外，本例中可以直接显示信息
            //view2.ShowDialog();
            //try
            //{
            //    task.Wait();
            //}
            //catch (Exception)//当取消时，会报AggregateException异常
            //{
            //}

            //if (task.IsCanceled)
            //{
            //    MessageBox.Show("执行被取消");
            //}
            //else if (task.IsCompleted)
            //{
            //    MessageBox.Show("执行完成！");
            //}
            //else if (task.IsFaulted)
            //{
            //    MessageBox.Show("执行出错！");
            //}

            this.Text = "测试完毕";
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
