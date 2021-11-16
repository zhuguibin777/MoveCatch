namespace MoveCatchTest
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSync = new System.Windows.Forms.Button();
            this.btnAsync = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAsyncAdvanced = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(89, 108);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(140, 64);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "同步线程";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnAsync
            // 
            this.btnAsync.Location = new System.Drawing.Point(89, 224);
            this.btnAsync.Name = "btnAsync";
            this.btnAsync.Size = new System.Drawing.Size(140, 59);
            this.btnAsync.TabIndex = 1;
            this.btnAsync.Text = "异步线程";
            this.btnAsync.UseVisualStyleBackColor = true;
            this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(552, 130);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 2;
            // 
            // btnAsyncAdvanced
            // 
            this.btnAsyncAdvanced.Location = new System.Drawing.Point(89, 332);
            this.btnAsyncAdvanced.Name = "btnAsyncAdvanced";
            this.btnAsyncAdvanced.Size = new System.Drawing.Size(140, 59);
            this.btnAsyncAdvanced.TabIndex = 3;
            this.btnAsyncAdvanced.Text = "异步进阶";
            this.btnAsyncAdvanced.UseVisualStyleBackColor = true;
            this.btnAsyncAdvanced.Click += new System.EventHandler(this.btnAsyncAdvanced_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 693);
            this.Controls.Add(this.btnAsyncAdvanced);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnAsync);
            this.Controls.Add(this.btnSync);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnAsync;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAsyncAdvanced;
    }
}

