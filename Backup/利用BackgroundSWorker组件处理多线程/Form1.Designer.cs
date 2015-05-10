namespace 利用BackgroundSWorker组件处理多线程
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.txtCompareString = new System.Windows.Forms.TextBox();
            this.txtWordsCounted = new System.Windows.Forms.TextBox();
            this.txtLinesCounted = new System.Windows.Forms.TextBox();
            this.lblSourceFile = new System.Windows.Forms.Label();
            this.lblCompareString = new System.Windows.Forms.Label();
            this.lblWordsCounted = new System.Windows.Forms.Label();
            this.lblLinesCounted = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(28, 224);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(162, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Location = new System.Drawing.Point(135, 46);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(100, 21);
            this.txtSourceFile.TabIndex = 2;
            // 
            // txtCompareString
            // 
            this.txtCompareString.Location = new System.Drawing.Point(135, 87);
            this.txtCompareString.Name = "txtCompareString";
            this.txtCompareString.Size = new System.Drawing.Size(100, 21);
            this.txtCompareString.TabIndex = 3;
            // 
            // txtWordsCounted
            // 
            this.txtWordsCounted.Location = new System.Drawing.Point(135, 130);
            this.txtWordsCounted.Name = "txtWordsCounted";
            this.txtWordsCounted.ReadOnly = true;
            this.txtWordsCounted.Size = new System.Drawing.Size(100, 21);
            this.txtWordsCounted.TabIndex = 4;
            this.txtWordsCounted.Text = "0";
            // 
            // txtLinesCounted
            // 
            this.txtLinesCounted.Location = new System.Drawing.Point(135, 169);
            this.txtLinesCounted.Name = "txtLinesCounted";
            this.txtLinesCounted.ReadOnly = true;
            this.txtLinesCounted.Size = new System.Drawing.Size(100, 21);
            this.txtLinesCounted.TabIndex = 5;
            this.txtLinesCounted.Text = "0";
            // 
            // lblSourceFile
            // 
            this.lblSourceFile.AutoSize = true;
            this.lblSourceFile.Location = new System.Drawing.Point(68, 46);
            this.lblSourceFile.Name = "lblSourceFile";
            this.lblSourceFile.Size = new System.Drawing.Size(41, 12);
            this.lblSourceFile.TabIndex = 6;
            this.lblSourceFile.Text = "源文件";
            // 
            // lblCompareString
            // 
            this.lblCompareString.AutoSize = true;
            this.lblCompareString.Location = new System.Drawing.Point(20, 87);
            this.lblCompareString.Name = "lblCompareString";
            this.lblCompareString.Size = new System.Drawing.Size(89, 12);
            this.lblCompareString.TabIndex = 7;
            this.lblCompareString.Text = "Compare String";
            // 
            // lblWordsCounted
            // 
            this.lblWordsCounted.AutoSize = true;
            this.lblWordsCounted.Location = new System.Drawing.Point(20, 133);
            this.lblWordsCounted.Name = "lblWordsCounted";
            this.lblWordsCounted.Size = new System.Drawing.Size(89, 12);
            this.lblWordsCounted.TabIndex = 8;
            this.lblWordsCounted.Text = "Matching Words";
            // 
            // lblLinesCounted
            // 
            this.lblLinesCounted.AutoSize = true;
            this.lblLinesCounted.Location = new System.Drawing.Point(26, 169);
            this.lblLinesCounted.Name = "lblLinesCounted";
            this.lblLinesCounted.Size = new System.Drawing.Size(83, 12);
            this.lblLinesCounted.TabIndex = 9;
            this.lblLinesCounted.Text = "Lines Counted";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 272);
            this.Controls.Add(this.lblLinesCounted);
            this.Controls.Add(this.lblWordsCounted);
            this.Controls.Add(this.lblCompareString);
            this.Controls.Add(this.lblSourceFile);
            this.Controls.Add(this.txtLinesCounted);
            this.Controls.Add(this.txtWordsCounted);
            this.Controls.Add(this.txtCompareString);
            this.Controls.Add(this.txtSourceFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.TextBox txtCompareString;
        private System.Windows.Forms.TextBox txtWordsCounted;
        private System.Windows.Forms.TextBox txtLinesCounted;
        private System.Windows.Forms.Label lblSourceFile;
        private System.Windows.Forms.Label lblCompareString;
        private System.Windows.Forms.Label lblWordsCounted;
        private System.Windows.Forms.Label lblLinesCounted;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

